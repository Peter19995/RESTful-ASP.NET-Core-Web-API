using MagicVill_web.Models;
using MagicVill_web.Services.IServices;
using Magicvilla_Utility;

namespace MagicVill_web.Services
{
    public class VillaService : BaseService, IVillaService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private string villaUrl;
        string endpoint = "api/v1/villaAPI";
        public VillaService(IHttpClientFactory httpClientFactory,IConfiguration configuration): base(httpClientFactory)
        {

            _httpClientFactory = httpClientFactory;
            villaUrl = configuration.GetValue<string>("ServiceUrls:VillaAPI");

        }
        public Task<T> CreateAsync<T>(VillaCreateDTO dto)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = dto,
                Url = villaUrl + endpoint
            }); 
        }

        public Task<T> UpdateAsync<T>(VillaUpdateDTO dto)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.PUT,
                Data = dto,
                Url = villaUrl + endpoint+"/"+dto.Id
            });
        }

        public Task<T> GetAllAsync<T>()
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = villaUrl + endpoint
            });
        }

        public Task<T> GetAsync<T>(int Id)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = villaUrl + endpoint + "/" + Id
            });
        }

        public Task<T> GetDeleteAsync<T>(int Id)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.DELETE,
                Url = villaUrl + endpoint + "/" + Id
            });
        }
    }
}
