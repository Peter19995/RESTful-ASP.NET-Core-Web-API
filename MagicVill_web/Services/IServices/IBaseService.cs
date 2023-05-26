using MagicVill_web.Models;

namespace MagicVill_web.Services.IServices
{
    public interface IBaseService
    {
        APIRequest responseModel { get; set; }
        Task<T> SendAsync<T>(APIRequest apiRequest);
    }
}
