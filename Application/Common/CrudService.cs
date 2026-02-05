using Domain;
using Domain.BaseAndMainModels;
using Domain.ResponseModels;
using Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Application.Common
{
    public class CrudService<TEntity> where TEntity : BaseEntity
    {
        protected readonly AppDbContext _context;

        public CrudService(AppDbContext context)
        {
            _context = context;
        }

        private async Task<ResponseModel<T>> ExecuteAsync<T>(Func<Task<T>> action, string operation)
        {
            var response = new ResponseModel<T>();
            try
            {
                var data = await action();
                response.Success = true;
                response.Data = data;
                response.Message = $"{operation} executed successfully.";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Error = $"Error in {operation}: {ex.Message}";
            }

            return response;
        }

        public virtual async Task<ResponseModel<List<TEntity>>> GetAllAsync()
        {
            return await ExecuteAsync(async () =>
            {
                return await _context.Set<TEntity>()
                                     .Where(t => !t.IsDeleted)
                                     .ToListAsync();
            }, nameof(GetAllAsync));
        }

        public virtual async Task<ResponseModel<TEntity?>> GetByIdAsync(int id)
        {
            return await ExecuteAsync(async () =>
            {
                return await _context.Set<TEntity>()
                                     .Where(t => !t.IsDeleted && t.Id == id )
                                     .FirstOrDefaultAsync();
                
            }, nameof(GetByIdAsync));
        }

        public virtual async Task<ResponseModel<TEntity>> CreateAsync(TEntity entity)
        {
            return await ExecuteAsync(async () =>
            {
                _context.Set<TEntity>().Add(entity);
                await _context.SaveChangesAsync();
                return entity;
            }, nameof(CreateAsync));
        }

        public virtual async Task<ResponseModel<bool>> UpdateAsync(TEntity entity)
        {
            return await ExecuteAsync(async () =>
            {
                var tracked = await _context.Set<TEntity>().FindAsync(entity.Id);
                if (tracked == null) return false;

                _context.Entry(tracked).CurrentValues.SetValues(entity);
                await _context.SaveChangesAsync();
                return true;
            }, nameof(UpdateAsync));
        }

        public virtual async Task<ResponseModel<bool>> DeleteAsync(int id)
        {
            return await ExecuteAsync(async () =>
            {
                var entity = await _context.Set<TEntity>().FindAsync(id);
                if (entity == null) return false;

                entity.IsDeleted = true;
                _context.Set<TEntity>().Update(entity);
                await _context.SaveChangesAsync();
                return true;
            }, nameof(DeleteAsync));
        }

    }
}
