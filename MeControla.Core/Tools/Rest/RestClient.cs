using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace MeControla.Core.Tools.Rest
{
    public class RestClient : IRestClient
    {
        private string UrlBase { get; set; }

        public enum Type
        {
            DELETE,
            GET,
            PATCH,
            POST,
            PUT
        }

        public RestClient()
        {
            UrlBase = string.Empty;
        }

        public T SendGet<T>(string url)
            => SendGet<T>(url, null);

        public T SendGet<T>(string url, Parameters parameter)
        {
            var uri = new Uri(HttpClientUtils.MountUrl(UrlBase, url, parameter));

            var t = Task.Run(() => GetURI(uri));
            t.Wait();

            return HttpClientUtils.Deserialize<T>(t.Result);
        }

        private async Task<HttpResponseMessage> GetURI(Uri u)
            => await CallURI(u, null, Type.GET);

        public HttpResponseMessage SendPost<T>(string url, T content)
            => SendPost<T>(url, null, content);

        public HttpResponseMessage SendPost<T>(string url, Parameters parameter, T content)
        {
            var uri = new Uri(HttpClientUtils.MountUrl(UrlBase, url, parameter));
            var json = HttpClientUtils.Serialize<T>(content);

            var t = Task.Run(() => PostURI(uri, HttpClientUtils.CreateHttpContent(json)));
            t.Wait();

            return t.Result;
        }

        private async Task<HttpResponseMessage> PostURI(Uri u, HttpContent c)
            => await CallURI(u, c, Type.POST);

        public T SendPut<T, U>(string url, U content)
            => SendPut<T, U>(url, null, content);

        public T SendPut<T, U>(string url, Parameters parameter, U content)
        {
            var uri = new Uri(HttpClientUtils.MountUrl(UrlBase, url, parameter));
            var json = HttpClientUtils.Serialize<U>(content);

            var t = Task.Run(() => PutURI(uri, HttpClientUtils.CreateHttpContent(json)));
            t.Wait();

            return HttpClientUtils.Deserialize<T>(t.Result);
        }

        private async Task<HttpResponseMessage> PutURI(Uri u, HttpContent c)
            => await CallURI(u, c, Type.PUT);

        public HttpResponseMessage SendDelete(string url)
            => SendDelete(url, null);

        public HttpResponseMessage SendDelete(string url, Parameters parameter)
        {
            var uri = new Uri(HttpClientUtils.MountUrl(UrlBase, url, parameter));

            var t = Task.Run(() => DeleteURI(uri));
            t.Wait();

            return t.Result;
        }

        private async Task<HttpResponseMessage> DeleteURI(Uri u)
            => await CallURI(u, null, Type.DELETE);

        private static async Task<HttpResponseMessage> CallURI(Uri u, HttpContent c, Type type)
        {
            using var client = new HttpClient();
            return type switch
            {
                Type.DELETE => await client.DeleteAsync(u),
                Type.PATCH => await client.PatchAsync(u, c),
                Type.POST => await client.PostAsync(u, c),
                Type.PUT => await client.PutAsync(u, c),
                _ => await client.GetAsync(u),
            };
        }
    }
}