using MagicVilla_Web.Models.Dto;

namespace MagicVilla_Web.Services.IServices
{
    public interface INumberVillaService
    {
        Task<T> GetAll<T>(string token);

        Task<T> Get<T>(int id, string token);

        Task<T> Create<T>(NumberVillaCreateDto dto, string token);

        Task<T> Update<T>(NumberVillaUpdateDto dto, string token);

        Task<T> Delete<T>(int id, string token);
    }
}
