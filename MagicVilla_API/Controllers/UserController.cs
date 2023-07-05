using MagicVilla_API.Models;
using MagicVilla_API.Models.Dto;
using MagicVilla_API.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace MagicVilla_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepo;
        private APIResponse _response;

        public UserController(IUserRepository userRepo)
        {
            _userRepo = userRepo;
            _response = new();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO model)
        {
            var loginResponse = await _userRepo.Login(model);
            if (loginResponse.User == null || string.IsNullOrEmpty(loginResponse.Token)) 
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccessful = false;
                _response.ErrorMessages.Add("UserName or Password incorrects");

                return BadRequest(_response);
            }
            _response.IsSuccessful = true;
            _response.StatusCode = HttpStatusCode.OK;
            _response.Result = loginResponse;

            return Ok(_response);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDTO model)
        {
            bool isSingleUser = _userRepo.IsSingleUser(model.UserName);

            if (!isSingleUser) 
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccessful = false;
                _response.ErrorMessages.Add("User already exists!");

                return BadRequest(_response);
            }
            var user = await _userRepo.Register(model);
            if (user == null) 
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccessful = false;
                _response.ErrorMessages.Add("Failed to register User!");

                return BadRequest(_response);
            }
            _response.StatusCode= HttpStatusCode.OK;
            _response.IsSuccessful = true;

            return Ok(_response);
        }
    }
}
