using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace DesktopApplication.ServiceLayer
{
    public class ServiceConnection : IServiceConnection
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public ServiceConnection(string baseUrl)
        {
            _httpClient = new HttpClient();
            _baseUrl = baseUrl.TrimEnd('/');
        }

        public Task<HttpResponseMessage> CallServiceGet(string uri)
        {
            return _httpClient.GetAsync($"{_baseUrl}/{uri}");
        }

        public Task<HttpResponseMessage> CallServicePost(string uri, HttpContent content)
        {
            return _httpClient.PostAsync($"{_baseUrl}/{uri}", content);
        }

        public Task<HttpResponseMessage> CallServicePut(string uri, HttpContent content)
        {
            return _httpClient.PutAsync($"{_baseUrl}/{uri}", content);
        }

        public Task<HttpResponseMessage> CallServiceDelete(string uri)
        {
            return _httpClient.DeleteAsync($"{_baseUrl}/{uri}");
        }
    }
}