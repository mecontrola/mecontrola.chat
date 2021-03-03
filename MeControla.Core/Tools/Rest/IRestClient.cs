using System.Net.Http;

namespace MeControla.Core.Tools.Rest
{
    public interface IRestClient
    {
        HttpResponseMessage SendDelete(string url);
        HttpResponseMessage SendDelete(string url, Parameters parameter);
        T SendGet<T>(string url);
        T SendGet<T>(string url, Parameters parameter);
        HttpResponseMessage SendPost<T>(string url, Parameters parameter, T content);
        HttpResponseMessage SendPost<T>(string url, T content);
        T SendPut<T, U>(string url, Parameters parameter, U content);
        T SendPut<T, U>(string url, U content);
    }
}