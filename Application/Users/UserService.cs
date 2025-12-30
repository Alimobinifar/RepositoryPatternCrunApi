using Application.Common;
using Domain;
using Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users
{
    public class UserService : CrudService<User>
    {
        public UserService(AppDbContext context) : base(context) { }

        public async Task<User> CreateAsync(UserCreateDto dto)
        {
            var user = new User
            {
                Name = dto.Name,
                Family = dto.Family,
                Email = dto.Email
            };

            return await base.CreateAsync(user);
        }

        public async Task<bool> UpdateAsync(UserUpdateDto dto)
        {
            var user = new User
            {
                Id = dto.Id,
                Name = dto.Name,
                Family = dto.Family,
                Email = dto.Email
            };
            return await base.UpdateAsync(user);
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await base.GetAllAsync();
        }

        public async Task<User> GetByIdAsync(int id )
        {
            var result =  await base.GetByIdAsync(id);
            if (result != null)
            {
                return result;
            }
            return null;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            await base.DeleteAsync(id);
            return true;
        }
    }
}
