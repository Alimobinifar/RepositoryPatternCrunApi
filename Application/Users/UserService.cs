using Application.Common;
using Domain.ResponseModels;
using Domain.UserModels;
using Domain.UserVMS;
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

        public async Task<ResponseModel<User>> CreateUserAsync(UserCreateDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name) || string.IsNullOrWhiteSpace(dto.Email))
                return new ResponseModel<User>
                {
                    Success = false,
                    Error = "Name and Email are required"
                };

            var user = new User
            {
                Name = dto.Name,
                Family = dto.Family,
                Email = dto.Email
            };

            var response = await base.CreateAsync(user);

            if (response.Success)
                response.Message = "User created successfully";

            return response;
        }

        public async Task<ResponseModel<bool>> UpdateUserAsync(UserUpdateDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name) || string.IsNullOrWhiteSpace(dto.Email))
                return new ResponseModel<bool>
                {
                    Success = false,
                    Error = "Name and Email are required"
                };

            var user = new User
            {
                Id = dto.Id,
                Name = dto.Name,
                Family = dto.Family,
                Email = dto.Email
            };
            var response =  await base.UpdateAsync(user);
            if (response.Success)
                response.Message = "User created succesfully";
            return response;
        }

        public async Task<ResponseModel<List<User>>> GetAllUsersAsync()
        {
            var response = await base.GetAllAsync();

            response.Message = response.Success
                ? "All users retrieved successfully"
                : response.Message ?? "Failed to retrieve users";

            return response;
        }

        public async Task<ResponseModel<User?>> GetUserByIdAsync(int id)
        {
            var response = await base.GetByIdAsync(id);

            if (response.Success && response.Data != null)
                response.Message = $"User {response.Data.Name} retrieved successfully";
            else if (!response.Success)
                response.Message ??= "Failed to retrieve user"; // fallback پیام خطا

            return response;
        }

        public async Task<ResponseModel<bool>> DeleteUserAsync(int id)
        {
            var response = await base.DeleteAsync(id);
            if (response.Success && response.Data)
                response.Message = "User deleted successfully";
            else if (response.Success && !response.Data)
                response.Message = "User not found or already deleted";
            else
                response.Message ??= "Failed to delete user";

            return response;
        }

    }
}
