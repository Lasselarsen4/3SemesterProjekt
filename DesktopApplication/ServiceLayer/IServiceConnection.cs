using System.Net.Http;
using System.Threading.Tasks;

namespace DesktopApplication.ServiceLayer;

public interface IServiceConnection
{
    Task<HttpResponseMessage> CallServiceGet(string url);
    Task<HttpResponseMessage> CallServicePost(string url, StringContent postJson);
    Task<HttpResponseMessage> CallServicePut(string url, StringContent postJson);
    Task<HttpResponseMessage> CallServiceDelete(string url);
}
