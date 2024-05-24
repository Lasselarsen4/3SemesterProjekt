using System.Net.Http;
using System.Threading.Tasks;


namespace DesktopApplication.ServiceLayer;

public class ServiceConnection : IServiceConnection
{
    private readonly HttpClient _httpClient;

    public ServiceConnection()
    {
        _httpClient = new HttpClient();
    }

    public async Task<HttpResponseMessage> CallServiceGet(string url)
    {
        return await _httpClient.GetAsync(url);
    }

    public async Task<HttpResponseMessage> CallServicePost(string url, StringContent postJson)
    {
        return await _httpClient.PostAsync(url, postJson);
    }

    public async Task<HttpResponseMessage> CallServicePut(string url, StringContent postJson)
    {
        return await _httpClient.PutAsync(url, postJson);
    }

    public async Task<HttpResponseMessage> CallServiceDelete(string url)
    {
        return await _httpClient.DeleteAsync(url);
    }
}
