using MagicVill_VillaAPI.Models;
using System.Linq.Expressions;

namespace MagicVill_VillaAPI.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null,int pageSize = 3, int PageNumber = 1, string? includeProperties = null);
        Task<T> GetAsync(Expression<Func<T, bool>>  filter = null, bool track = true, string? includeProperties = null);
        Task CreateAsync(T entity);
        Task RemoveAsync(T entity);

        Task SaveAsync();
    }
}

