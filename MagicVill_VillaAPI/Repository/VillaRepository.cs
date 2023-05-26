using MagicVill_VillaAPI.Data;
using MagicVill_VillaAPI.Models;
using MagicVill_VillaAPI.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Linq;
using System.Linq.Expressions;

namespace MagicVill_VillaAPI.Repository
{
    public class VillaRepository : Repository<Villa>, IVillaRepository
    {
        private readonly ApplicationDbContext _db;
        public VillaRepository(ApplicationDbContext db): base(db) 
        {
            _db = db;
        }

        public async Task<Villa> UpdateAsync(Villa entity)
        {
            entity.UpdateDate = DateTime.Now;   
            _db.Update(entity);
            await _db.SaveChangesAsync();   
            return entity;  
        }

       
    }
}
