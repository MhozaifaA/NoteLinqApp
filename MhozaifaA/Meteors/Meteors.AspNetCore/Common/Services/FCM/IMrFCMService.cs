using System.Net.Http;
using System.Threading.Tasks;

namespace Meteors.AspNetCore.Common.Services.FCM
{
    public interface IMrFCMService
    {
        Task<HttpResponseMessage> FireAsync(string deviceId, string title, string body);
        Task<HttpResponseMessage> FireAsync(string applicationId, string deviceId, string title, string body);
        Task<HttpResponseMessage> FireAsync(string applicationId, string deviceId, string title, string body, object data);
        Task<HttpResponseMessage> FireAsync<T>(string applicationId, string deviceId, string title, string body, T data) where T : DefaultDataFCM;
        Task<HttpResponseMessage> FireAsync<T>(string deviceId, string title, string body, T data) where T : DefaultDataFCM;
    }
}
