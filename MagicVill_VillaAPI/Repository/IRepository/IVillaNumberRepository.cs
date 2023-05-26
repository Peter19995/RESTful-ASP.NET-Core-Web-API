using MagicVill_VillaAPI.Models;
using System.Linq.Expressions;

namespace MagicVill_VillaAPI.Repository.IRepository
{
    public interface IVillaNumberRepository : IRepository<VillaNumber>
    {
       
        Task<VillaNumber> UpdateAsync(VillaNumber entity);
       
    }
}
