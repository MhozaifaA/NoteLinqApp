using Meteors.AspNetCore.Helper.ExtensionMethods.String;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Meteors.AspNetCore.Common.Services.FCM
{
    public class MrFCMService : IMrFCMService
    {
        private readonly IHttpClientFactory httpClient;
        private readonly MrFCMOptions options;

        public MrFCMService(IHttpClientFactory httpClient,IOptions<MrFCMOptions> options  = null)
        {
            this.httpClient = httpClient;
            this.options = options?.Value ?? new MrFCMOptions();
        }


        public async Task<HttpResponseMessage> FireAsync(string applicationId, string deviceId,string title, string body, object data)
        {

            var _fcmHttp = httpClient.CreateClient(MrFCMOptions.Name);


            var request = new
            {
                to = deviceId,
                priority = "high",
                notification = new
                {
                    title = title,
                    body = body,
                    sound= true
                },
                data = data
            };

            var json = JsonSerializer.Serialize(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            _fcmHttp.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", $"key={applicationId}");

            _fcmHttp.DefaultRequestHeaders.TryAddWithoutValidation("Sender", $"id={applicationId}");

            return await _fcmHttp.PostAsync("fcm/send", httpContent);
        }


        public Task<HttpResponseMessage> FireAsync(string applicationId, string deviceId, string title, string body)
        {
            if (options.ApplicationId.IsNullOrEmpty())
                throw new ArgumentNullException($"Mr-FCM {nameof(options.ApplicationId)} can't be null or empty");

            return FireAsync(applicationId, deviceId, title, body, options.EnableBaseData? new BaseDataFCM() :null);
        }

        public Task<HttpResponseMessage> FireAsync(string deviceId, string title, string body)
        {
            return  FireAsync(options.ApplicationId, deviceId, title, body);
        }

       


        public Task<HttpResponseMessage> FireAsync<T>(string applicationId, string deviceId, string title, string body,T data) where T : DefaultDataFCM
        {
            if (options.ApplicationId.IsNullOrEmpty())
                throw new ArgumentNullException($"Mr-FCM {nameof(options.ApplicationId)} can't be null or empty");

            return FireAsync(applicationId, deviceId, title, body, (object)data);
        }

        public Task<HttpResponseMessage> FireAsync<T>(string deviceId, string title, string body, T data) where T : DefaultDataFCM
        {
            return FireAsync(options.ApplicationId, deviceId, title, body, data);
        }
    }
}
