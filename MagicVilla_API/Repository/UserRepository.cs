using AutoMapper;
using MagicVilla_API.Data;
using MagicVilla_API.Models;
using MagicVilla_API.Models.Dto;
using MagicVilla_API.Repository.IRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MagicVilla_API.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<UserApplication> _userManager;
        private string secretKey;

        public UserRepository(ApplicationDbContext db, IConfiguration configuration, UserManager<UserApplication> userManager, IMapper mapper, RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _mapper = mapper;
            _roleManager = roleManager;
            _userManager = userManager;
            secretKey = configuration.GetValue<string>("APISettings:Secret");
        }

        public bool IsSingleUser(string userName)
        {
            var user = _db.UserApplication.FirstOrDefault(u => u.UserName.ToLower() == userName.ToLower());
            if (user == null)
            {
                return true;
            }

            return false;
        }

        public async Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO)
        {
            var user = await _db.UserApplication.FirstOrDefaultAsync(u => u.UserName.ToLower() == loginRequestDTO.UserName.ToLower());

            bool IsValid = await _userManager.CheckPasswordAsync(user, loginRequestDTO.Password);

            if (user == null || IsValid == false) 
            {
                return new LoginResponseDTO()
                { 
                    Token = "",
                    User = null
                };
            }

            //If exist User generate JW Token
            var roles = await _userManager.GetRolesAsync(user);
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Role, roles.FirstOrDefault())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            LoginResponseDTO loginResponseDTO = new()
            {
                Token = tokenHandler.WriteToken(token),
                User = _mapper.Map<UserDto>(user)
            };

            return loginResponseDTO;
        }

        public async Task<UserDto> Register(RegisterRequestDTO registerRequestDTO)
        {
            UserApplication user = new()
            {
                UserName = registerRequestDTO.UserName,
                Email = registerRequestDTO.UserName,
                NormalizedEmail = registerRequestDTO.UserName.ToUpper(),
                Name = registerRequestDTO.Name
            };

            try
            {
                var result = await _userManager.CreateAsync(user, registerRequestDTO.Password);
                if (result.Succeeded) 
                {
                    if (!_roleManager.RoleExistsAsync("admin").GetAwaiter().GetResult()) 
                    {
                        await _roleManager.CreateAsync(new IdentityRole("admin"));
                        await _roleManager.CreateAsync(new IdentityRole("client"));
                    }

                    await _userManager.AddToRoleAsync(user, "admin");
                    var userApp = _db.UserApplication.FirstOrDefault(u => u.UserName == registerRequestDTO.UserName);

                    return _mapper.Map<UserDto>(userApp);
                }
            }
            catch (Exception)
            {

                throw;
            }

            return new UserDto();
        }
    }
}
