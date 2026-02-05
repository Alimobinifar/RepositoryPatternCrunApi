using Application.Users;
using AutoMapper;
using Domain;
using Domain.UserVMS;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly UserService _service;
        public UsersController(UserService service)
        {
            _service = service;
        }

        // -------------------- CREATE --------------------
        // POST: api/users/Create
        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] UserCreateDto dto)
            => Ok(await _service.CreateUserAsync(dto));

        // -------------------- UPDATE --------------------
        // PUT: api/users/Update/1
        [HttpPut("Update/{id:int}")]
        public async Task<IActionResult> Update(int id, UserUpdateDto dto)
        {
            dto.Id = id;
            return Ok(await _service.UpdateUserAsync(dto));
        }

        // -------------------- GET ALL --------------------
        // GET: api/users/GetAll
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
            => Ok(await _service.GetAllAsync());

        // -------------------- GET BY ID --------------------
        // GET: api/users/GetById/1
        [HttpGet("GetById/{id:int}")]
        public async Task<IActionResult> GetById(int id)
            => Ok(await _service.GetByIdAsync(id));

        // -------------------- DELETE --------------------
        // DELETE: api/users/Delete/1
        [HttpDelete("Delete/{id:int}")]
        public async Task<IActionResult> Delete(int id)
            => Ok(await _service.DeleteAsync(id));
        class A
        {

        }
    }
}
