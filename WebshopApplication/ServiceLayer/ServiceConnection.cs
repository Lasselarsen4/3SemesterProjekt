using System.Net.Http;
using System.Threading.Tasks;

namespace WebshopApplication.ServiceLayer
{
    public class ServiceConnection : IServiceConnection
    {
        public ServiceConnection(string inBaseUrl)
        {
            HttpEnabler = new HttpClient();
            BaseUrl = inBaseUrl;
            UseUrl = BaseUrl;
        }

        public HttpClient HttpEnabler { private get; init; }
        public string BaseUrl { get; init; }
        public string UseUrl { get; set; }

        public async Task<HttpResponseMessage> CallServiceGet()
        {
            if (UseUrl != null)
            {
                return await HttpEnabler.GetAsync(UseUrl);
            }
            return null;
        }

        public async Task<HttpResponseMessage> CallServicePost(StringContent postJson)
        {
            if (UseUrl != null)
            {
                return await HttpEnabler.PostAsync(UseUrl, postJson);
            }
            return null;
        }

        public async Task<HttpResponseMessage> CallServicePut(StringContent postJson)
        {
            if (UseUrl != null)
            {
                return await HttpEnabler.PutAsync(UseUrl, postJson);
            }
            return null;
        }

        public async Task<HttpResponseMessage> CallServiceDelete()
        {
            if (UseUrl != null)
            {
                return await HttpEnabler.DeleteAsync(UseUrl);
            }
            return null;
        }
    }
}