using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TukanTomek.Server.Data;
using TukanTomek.Server.DTOs.FamilyDtos;
using TukanTomek.Server.DTOs;
using TukanTomek.Server.Models;
using TukanTomek.Server.Services.Interfaces;
using TukanTomek.Server.DTOs.TransactionDtos;
using TukanTomek.Server.Services;

namespace TukanTomek.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _service;
        public TransactionController(ITransactionService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Transaction>>> GetAll()
        {
            var transactions = await _service.GetAllAsync();
            return Ok(transactions);
        }
        
        [HttpGet("user/{id}")]
        public async Task<ActionResult<IEnumerable<Transaction>>> GetByUserIdAsync(int id)
        {
            var transactions = await _service.GetByUserIdAsync(id);
            return Ok(transactions);
        }        

        [HttpGet("family/{id}")]
        public async Task<ActionResult<IEnumerable<Transaction>>> GetFamilyByUserIdAsync(int id)
        {
            var transactions = await _service.GetFamilyByUserIdAsync(id);
            return Ok(transactions);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Transaction>> GetById(int id)
        {
            var transaction = await _service.GetByIdAsync(id);
            if (transaction == null) return NotFound();

            return Ok(transaction);
        }

        [HttpPost]
        public async Task<ActionResult<Transaction>> Create(TransactionDto dto)
        {
            var transaction = await _service.CreateAsync(dto);
            if (transaction == null) return BadRequest("Transaction cant be created.");

            return CreatedAtAction(nameof(GetById), new { id = transaction.Id }, transaction);
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
