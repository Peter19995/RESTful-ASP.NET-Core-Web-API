using MagicVill_VillaAPI.Models;
using MagicVill_VillaAPI.Models.DTO;
using MagicVill_VillaAPI.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;


namespace MagicVill_VillaAPI.Controllers.v1
{
    [Route("api/v{version:apiVersion}/IsersAuth")]
    [ApiController]
    [ApiVersionNeutral]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        protected APIResponse _response;
        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _response = new();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Loging([FromBody] LoginRequestDTO model)
        {
            var loginResponse = await _userRepository.Login(model);
            if (loginResponse.User == null || string.IsNullOrEmpty(loginResponse.Token))
            {

                _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                _response.IsSucess = false;
                _response.ErrorMessages.Add("UserName Or Password is incorrect");
                return BadRequest(_response);

            }
            _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
            _response.IsSucess = true;
            _response.Result = loginResponse;
            return BadRequest(_response);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegistrationRequestDTO model)
        {
            bool ifUserNameUnique = await _userRepository.IsUniqueUser(model.UserName);
            if (!ifUserNameUnique)
            {
                _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                _response.IsSucess = false;
                _response.ErrorMessages.Add("User Already Existis");
                return BadRequest(_response);
            }
            var user = await _userRepository.Register(model);
            if (user == null)
            {
                _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                _response.IsSucess = false;
                _response.ErrorMessages.Add("Error While Registering");
                return BadRequest(_response);
            }
            _response.StatusCode = System.Net.HttpStatusCode.OK;
            _response.IsSucess = true;
            return Ok(_response);

        }
    
    }
}
