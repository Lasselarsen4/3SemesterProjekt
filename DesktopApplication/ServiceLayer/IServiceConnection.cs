
namespace DesktopApplication.ServiceLayer
{
    public interface IServiceConnection
    {
        Task<HttpResponseMessage> CallServiceGet(string uri);
        Task<HttpResponseMessage> CallServicePost(string uri, HttpContent content);
        Task<HttpResponseMessage> CallServicePut(string uri, HttpContent content);
        Task<HttpResponseMessage> CallServiceDelete(string uri);
    }
}