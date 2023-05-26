using MagicVill_VillaAPI.Models;
using System.Linq.Expressions;

namespace MagicVill_VillaAPI.Repository.IRepository
{
    public interface IVillaRepository : IRepository<Villa>
    {
       
        Task<Villa> UpdateAsync(Villa entity);
       
    }
}
