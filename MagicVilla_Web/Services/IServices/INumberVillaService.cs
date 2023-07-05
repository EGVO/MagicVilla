using MagicVilla_Web.Models.Dto;

namespace MagicVilla_Web.Services.IServices
{
    public interface INumberVillaService
    {
        Task<T> GetAll<T>();

        Task<T> Get<T>(int id);

        Task<T> Create<T>(NumberVillaCreateDto dto);

        Task<T> Update<T>(NumberVillaUpdateDto dto);

        Task<T> Delete<T>(int id);
    }
}
