using MagicVill_VillaAPI.Data;
using MagicVill_VillaAPI.Models;
using MagicVill_VillaAPI.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MagicVill_VillaAPI.Repository
{

    
    public class Repository<T> : IRepository <T> where T : class
    {
        private readonly ApplicationDbContext _db;
        internal DbSet<T> dbSet;
        public Repository(ApplicationDbContext db)
        {
            _db = db;
            //_db.VillaNumbers.Include(u => u.Villa).ToList();
            this.dbSet = _db.Set<T>();

        }


        public async Task CreateAsync(T entity)
        {
            await dbSet.AddAsync(entity);
            await SaveAsync();
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> filter = null, bool track = true, string? includeProperties = null)
        {
            IQueryable<T> query = dbSet;
            if (!track)
            {
                query = query.AsNoTracking(); 
            }
            if (filter != null)
            {
                query = query.Where(filter);
            }
            if(includeProperties != null)
            {
                foreach(var property in includeProperties.Split(new char[] {','}, StringSplitOptions.RemoveEmptyEntries) )
                {
                    query  = query.Include(property);
                }
            }
            return await query.FirstOrDefaultAsync();
        }

        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null, int pageSize = 3, int PageNumber = 1, string? includeProperties = null)
        {
            IQueryable<T> query = dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            if(pageSize > 0)
            {
                if(pageSize > 100)
                {
                    pageSize = 100;
                }
                query = query.Skip(pageSize * (PageNumber - 1)).Take(pageSize);
            }
            if (includeProperties != null)
            {
                foreach (var property in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(property);
                }
            }
            return await query.ToListAsync();

        }

        public async Task RemoveAsync(T entity)
        {
            dbSet.Remove(entity);
            await SaveAsync();
        }

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}
