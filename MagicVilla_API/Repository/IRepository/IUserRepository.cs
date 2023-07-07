using MagicVilla_API.Models.Dto;

namespace MagicVilla_API.Repository.IRepository
{
    public interface IUserRepository
    {
        bool IsSingleUser(string userName);

        Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO);

        Task<UserDto> Register(RegisterRequestDTO registerRequestDTO);
    }
}
