using Domain;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common
{
    public class CrudService<TEntity> where TEntity : BaseEntity
    {
        protected readonly AppDbContext _context;
        public CrudService(AppDbContext context)
        {
            _context = context;
        }

        public virtual async Task<List<TEntity>> GetAllAsync()
            => await _context.Set<TEntity>().Where(t=>!t.IsDeleted).ToListAsync();

        public virtual async Task<TEntity?> GetByIdAsync(int id)
              => await _context.Set<TEntity>()
                     .Where(c => !c.IsDeleted && c.Id == id)
                     .FirstOrDefaultAsync();

        public virtual async Task<TEntity> CreateAsync(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public virtual async Task<bool> UpdateAsync(TEntity entity)
        {
            var trackedRecord = await _context.Set<TEntity>().FindAsync(entity.Id);
            if (trackedRecord == null) return false;
            _context.Entry(trackedRecord).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public virtual async Task DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity is null) return;
            entity.IsDeleted = true;
            _context.Set<TEntity>().Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
