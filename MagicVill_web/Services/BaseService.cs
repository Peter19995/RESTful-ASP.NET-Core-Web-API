using AutoMapper.Execution;
using MagicVill_web.Models;
using MagicVill_web.Services.IServices;
using Magicvilla_Utility;
using Newtonsoft.Json;
using System.Text;

namespace MagicVill_web.Services
{
    public class BaseService : IBaseService
    {
        public APIRequest responseModel { get; set; }
        public IHttpClientFactory httpClient { get; set; }
        public BaseService(IHttpClientFactory httpClient)
        {
            this.responseModel = new();
            this.httpClient = httpClient;

        }
        public async Task<T> SendAsync<T>(APIRequest apiRequest)
        {
            try
            {
                var client = httpClient.CreateClient("MagicAPI");
                HttpRequestMessage message = new HttpRequestMessage();
                message.Headers.Add("Accept", "application/json");
                message.RequestUri = new Uri(apiRequest.Url);
                if (apiRequest.Data != null)
                {
                    message.Content = new StringContent(JsonConvert.SerializeObject(apiRequest.Data), Encoding.UTF8, "application/json");
                }
                switch (apiRequest.ApiType)
                {
                    case SD.ApiType.POST:
                        message.Method = HttpMethod.Post;
                        break;
                    case SD.ApiType.DELETE:
                        message.Method = HttpMethod.Delete;
                        break;
                    case SD.ApiType.PUT:
                        message.Method = HttpMethod.Put;
                        break;
                    default:
                        message.Method = HttpMethod.Get;
                        break;
                }
                HttpResponseMessage apiResponse = null;
                apiResponse = await client.SendAsync(message);
                var apiContent  = await apiResponse.Content.ReadAsStringAsync();    
                try
                {
                    APIResponse AIPResponse = JsonConvert.DeserializeObject<APIResponse>(apiContent);
                    if(AIPResponse.StatusCode == System.Net.HttpStatusCode.BadRequest || AIPResponse.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        AIPResponse.StatusCode = System.Net.HttpStatusCode.BadRequest;
                        AIPResponse.IsSucess = false;
                        var res = JsonConvert.SerializeObject(AIPResponse);
                        var returnObj = JsonConvert.DeserializeObject<T>(res);
                        return returnObj;
                    }
                   
                }
                catch (Exception ex) {
                    var exResponse = JsonConvert.DeserializeObject<T>(apiContent);
                    return exResponse;
                }
                var AIPResponses = JsonConvert.DeserializeObject<T>(apiContent);
                return AIPResponses;


            }
            catch (Exception ex)
            {
                var dto = new APIResponse
                {
                    ErrorMessages = new List<string> { Convert.ToString(ex.Message) },
                    IsSucess = false,
                };
                var res = JsonConvert.SerializeObject(dto);
                var APIResponse = JsonConvert.DeserializeObject<T>(res);
                return APIResponse;
            }
        }
    }
}
