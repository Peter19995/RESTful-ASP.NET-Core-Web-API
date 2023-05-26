using MagicVill_web.Models;
using MagicVill_web.Services.IServices;
using Magicvilla_Utility;

namespace MagicVill_web.Services
{
    public class VillaNumberService : BaseService, IVillaNumberService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private string villaUrl;
        string endpoint = "api/v1/villarNumber";
        public VillaNumberService(IHttpClientFactory httpClientFactory,IConfiguration configuration): base(httpClientFactory)
        {

            _httpClientFactory = httpClientFactory;
            villaUrl = configuration.GetValue<string>("ServiceUrls:VillaAPI");

        }
        public Task<T> CreateAsync<T>(VillaNumberCreateDTO dto)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = dto,
                Url = villaUrl + endpoint
            }); 
        }

        public Task<T> UpdateAsync<T>(VillaNumberUpdateDTO dto)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.PUT,
                Data = dto,
                Url = villaUrl + endpoint+"/"+dto.VillaNo
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

        public Task<T> DeleteAsync<T>(int Id)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = SD.ApiType.DELETE,
                Url = villaUrl + endpoint + "/" + Id
            });
        }
    }
}
