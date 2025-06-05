using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TukanTomek.Server.Data;
using TukanTomek.Server.DTOs.FamilyDtos;
using TukanTomek.Server.DTOs.UserDtos;
using TukanTomek.Server.Models;
using TukanTomek.Server.Services.Interfaces;

namespace TukanTomek.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetAll()
        {
            var users = await _userService.GetAllAsync();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetById(int id)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user == null) return NotFound();

            return Ok(user);
        }        
        [HttpGet("{id}/family")]
        public async Task<ActionResult<FamilyDto>> GetFamilyByUserId(int id)
        {
            var family = await _userService.GetFamilyByUserIdAsync(id);
            if (family == null) return NotFound();

            return Ok(family);
        }

        [HttpPost]
        public async Task<ActionResult<UserDto>> Create(UserDto dto)
        {
            var createdUser = await _userService.CreateAsync(dto);
            if (createdUser == null) return BadRequest("User with this email exist");

            return CreatedAtAction(nameof(GetById), new { id = createdUser.Id }, createdUser);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, UserDto dto)
        {
            if (id != dto.Id) return BadRequest("ID error");

            var updatedUser = await _userService.UpdateAsync(id, dto);

            if (updatedUser == null) return NotFound($"Cant find player with id {id}");

            return Ok(updatedUser);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await _userService.DeleteAsync(id);
            if (!result) return NotFound();

            return NoContent();
        }
    }
}
