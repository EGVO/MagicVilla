using MagicVilla_Web.Models.Dto;

namespace MagicVilla_Web.Services.IServices
{
    public interface IUserService
    {
        Task<T> Login<T>(LoginRequestDTO dto);

        Task<T> Register<T>(RegisterRequestDto dto);
    }
}
