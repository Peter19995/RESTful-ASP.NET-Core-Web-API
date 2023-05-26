using MagicVill_VillaAPI.Models;
using MagicVill_VillaAPI.Models.DTO;

namespace MagicVill_VillaAPI.Repository.IRepository
{
    public interface IUserRepository
    {

        Task< bool> IsUniqueUser(string username); 
        Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO);
        Task<dynamic> Register(RegistrationRequestDTO registrationRequestDTO);

    }
}
