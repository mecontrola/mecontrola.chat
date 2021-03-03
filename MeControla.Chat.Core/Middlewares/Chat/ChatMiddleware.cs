using MeControla.Chat.Core.Commands;
using MeControla.Chat.Core.Executor;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Concurrent;
using System.IO;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MeControla.Chat.Core.Middlewares.Chat
{
    public class ChatMiddleware
    {
        private const int BUFFER_SIZE = 8192;
        private const string SOCKET_STATUS_CLOSING_DESCRIPTION = "Closing";

        private readonly ConcurrentDictionary<string, WebSocket> sockets;
        private readonly RequestDelegate next;

        public ChatMiddleware(RequestDelegate next)
        {
            sockets = new ConcurrentDictionary<string, WebSocket>();

            this.next = next;
        }

        public async Task Invoke(HttpContext context,
                                 ICommandFactory comandFactory,
                                 IConnectExecutor connectExecutor,
                                 IResponseExecutor responseExecutor)
        {
            if (!context.WebSockets.IsWebSocketRequest)
            {
                await next.Invoke(context);
                return;
            }

            var ct = context.RequestAborted;
            var currentSocket = await context.WebSockets.AcceptWebSocketAsync();
            var socketId = context.Connection.Id;

            sockets.TryAdd(socketId, currentSocket);

            while (true)
            {
                if (ct.IsCancellationRequested)
                    break;

                var request = await ReceiveStringAsync(currentSocket, ct);
                if (IsValidMessage(request))
                {
                    if (IsNotWebSocketOpen(currentSocket))
                        break;

                    continue;
                }

                var command = comandFactory.GetCommand(request);
                var result = await connectExecutor.Execute(socketId, command);
                var response = responseExecutor.Generate(result);

                foreach (var message in response)
                {
                    var socket = sockets[message.ConnectionId];

                    if (IsNotWebSocketOpen(socket))
                        continue;

                    await SendResponseAsync(socket, message.Message, ct);
                }

                if (result is ExitResult)
                    break;
            }


            sockets.TryRemove(socketId, out currentSocket);

            await currentSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, SOCKET_STATUS_CLOSING_DESCRIPTION, ct);
            currentSocket.Dispose();
        }

        private static async Task SendResponseAsync(WebSocket socket, string data, CancellationToken ct)
        {
            var buffer = Encoding.UTF8.GetBytes(data);
            var segment = new ArraySegment<byte>(buffer);
            await socket.SendAsync(segment, WebSocketMessageType.Text, true, ct);
        }


        private static async Task<string> ReceiveStringAsync(WebSocket socket, CancellationToken ct)
        {
            using var ms = new MemoryStream();
            return await GetReceiveResult(ms, socket, ct);
        }

        private static async Task<string> GetReceiveResult(MemoryStream ms, WebSocket socket, CancellationToken ct)
        {
            var buffer = new ArraySegment<byte>(new byte[BUFFER_SIZE]);
            WebSocketReceiveResult result;
            do
            {
                ct.ThrowIfCancellationRequested();

                result = await socket.ReceiveAsync(buffer, ct);
                ms.Write(buffer.Array, buffer.Offset, result.Count);
            }
            while (!result.EndOfMessage);

            ms.Seek(0, SeekOrigin.Begin);
            return IsMessageText(result)
                 ? await GetContentByStream(ms)
                 : null;
        }

        private static bool IsValidMessage(string message)
            => string.IsNullOrWhiteSpace(message);

        private static bool IsNotWebSocketOpen(WebSocket webSocket)
            => !IsWebSocketOpen(webSocket);

        private static bool IsWebSocketOpen(WebSocket webSocket)
            => webSocket.State.Equals(WebSocketState.Open);

        private static bool IsMessageText(WebSocketReceiveResult result)
            => result.MessageType.Equals(WebSocketMessageType.Text);

        private static async Task<string> GetContentByStream(MemoryStream ms)
        {
            using var reader = new StreamReader(ms, Encoding.UTF8);
            return await reader.ReadToEndAsync();
        }
    }
}