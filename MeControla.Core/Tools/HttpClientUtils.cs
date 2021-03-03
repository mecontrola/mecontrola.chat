using MeControla.Core.Tools.Rest;
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MeControla.Core.Tools
{
    public static class HttpClientUtils
    {
        public static string MountUrl(string urlBase, string urlPath)
            => MountUrl(urlBase, urlPath, null);

        public static string MountUrl(string urlBase, string urlPath, Parameters parameter)
        {
            var urlTmp = urlPath;

            if (parameter != null && parameter.HasAny())
            {
                foreach (var itm in parameter)
                    urlTmp = urlTmp.Replace($"{{{itm.Key}}}", itm.Value);
            }

            return $"{urlBase}{urlTmp}";
        }

        #region NewtonJson Serialize/Deserialize

        public static string Serialize<T>(T data)
            => JsonSerializer.Serialize<T>(data);

        public static T Deserialize<T>(HttpResponseMessage response)
        {
            var t = Task.Run<string>(() => response.Content.ReadAsStringAsync());
            t.Wait();

            return Deserialize<T>(t.Result);
        }

        public static T Deserialize<T>(string response)
            => (typeof(T) == typeof(string))
             ? (T)Convert.ChangeType(response, typeof(string))
             : JsonSerializer.Deserialize<T>(response);
        #endregion

        #region HttpContent
        public static HttpContent CreateHttpContent(string content)
            => CreateHttpContent(content, Encoding.UTF8, "application/json");

        public static HttpContent CreateHttpContent(string content, string mediaType)
            => CreateHttpContent(content, Encoding.UTF8, mediaType);

        public static HttpContent CreateHttpContent(string content, Encoding encoding)
            => CreateHttpContent(content, encoding, "application/json");

        public static HttpContent CreateHttpContent(string content, Encoding encoding, string mediaType)
            => new StringContent(content, encoding, mediaType);
        #endregion
    }
}
