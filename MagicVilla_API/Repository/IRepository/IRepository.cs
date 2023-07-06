using MagicVilla_API.Models.Specifications;
using System.Linq.Expressions;

namespace MagicVilla_API.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        Task Create(T entity);

        Task<List<T>> GetAll(Expression<Func<T, bool>>? filter = null, string? IncludeProperties = null);
        
        PageList<T> GetAllPaged(Parameters parameters, Expression<Func<T, bool>>? filter = null, string? IncludeProperties = null);

        Task<T> Get(Expression<Func<T, bool>>? filter = null, bool tracked = true, string? IncludeProperties = null);

        Task Delete(T entity);

        Task Save();
    }
}
