using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TukanTomek.Server.Data;
using TukanTomek.Server.DTOs;
using TukanTomek.Server.DTOs.FamilyDtos;
using TukanTomek.Server.DTOs.UserDtos;
using TukanTomek.Server.Models;
using TukanTomek.Server.Services;
using TukanTomek.Server.Services.Interfaces;

namespace TukanTomek.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FamilyController : ControllerBase
    {
        private readonly IFamilyService _service;
        public FamilyController(IFamilyService service)
        {
            _service = service;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<FamilyDto>>> GetAll()
        {
            var families = await _service.GetAllAsync();
            return Ok(families);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<FamilyDto>> GetById(int id)
        {
            var family = await _service.GetByIdAsync(id);
            if (family == null) return NotFound();

            return Ok(family);
        }

        [HttpGet("{familyId}/users")]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetFamilyUsers(int familyId)
        {
            var users = await _service.GetAllFamilyUsersAsync(familyId);
            if (users == null || !users.Any())
                return NotFound();

            return Ok(users);
        }
        [HttpGet("{familyId}/users-with-transactions")]
        public async Task<ActionResult> GetFamilyUsersWithTransactions(int familyId)
        {
            var users = await _service.GetFamilyUsersWithTransactionsAsync(familyId);
            if (users == null)
                return NotFound();

            return Ok(users);
        }

        [HttpPost]
        public async Task<ActionResult<FamilyDto>> Create(FamilyDto dto)
        {
            var family = await _service.CreateAsync(dto);
            if (family == null) return BadRequest("Cant create family.");

            return CreatedAtAction(nameof(GetById), new { id = family.Id }, family);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<FamilyDto>> UpdateAsync(int id, FamilyDto dto)
        {
            if (id != dto.Id) return BadRequest("ID error");

            var updatedFamily = await _service.UpdateAsync(id, dto);

            if (updatedFamily == null) return NotFound($"Cant find family with id {id}");

            return Ok(updatedFamily);
        }
        
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await _service.DeleteAsync(id);
            if (!result) return NotFound();

            return NoContent();
        }
    }
}