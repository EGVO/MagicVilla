using MagicVilla_utility;
using MagicVilla_Web.Models;
using MagicVilla_Web.Models.Dto;
using MagicVilla_Web.Services.IServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace MagicVilla_Web.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginRequestDTO model)
        {
            var response = await _userService.Login<APIResponse>(model);
            if (response != null && response.IsSuccessful == true)
            {
                LoginResponseDTO loginResponse = JsonConvert.DeserializeObject<LoginResponseDTO>(Convert.ToString(response.Result));

                var handler = new JwtSecurityTokenHandler();
                var jwt = handler.ReadJwtToken(loginResponse.Token);

                //Claims
                var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                identity.AddClaim(new Claim(ClaimTypes.Name, jwt.Claims.FirstOrDefault(c => c.Type == "unique_name").Value));
                identity.AddClaim(new Claim(ClaimTypes.Role, jwt.Claims.FirstOrDefault(c => c.Type == "role").Value));
                var principal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                //Session
                HttpContext.Session.SetString(SD.SessionToken, loginResponse.Token);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("ErrorMessages", response.ErrorMessages.FirstOrDefault());
                return View(model);
            }
        }

        public IActionResult Register() 
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterRequestDto model) 
        {
            var response = await _userService.Register<APIResponse>(model);
            if (response != null && response.IsSuccessful)
            {
                return RedirectToAction("login");
            }
            return View();
        }

        public async Task<IActionResult> Logout() 
        {
            await HttpContext.SignOutAsync();
            HttpContext.Session.SetString(SD.SessionToken, "");

            return RedirectToAction("Index", "Home");
        }

        public IActionResult AccessDenied() 
        { 
            return View();        
        }
    }
}
