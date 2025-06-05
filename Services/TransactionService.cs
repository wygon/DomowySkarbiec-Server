using Microsoft.AspNetCore.Http.HttpResults;
using TukanTomek.Server.DTOs.FamilyDtos;
using TukanTomek.Server.DTOs.TransactionDtos;
using TukanTomek.Server.Models;
using TukanTomek.Server.Repositories.Interfaces;
using TukanTomek.Server.Services.Interfaces;

namespace TukanTomek.Server.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _repository;
        private readonly IUserRepository _userRepository;
        public TransactionService(ITransactionRepository repository, IUserRepository userRepository)
        {
            _repository = repository;
            _userRepository = userRepository;
        }
        public async Task<IEnumerable<TransactionDto>> GetAllAsync()
        {
            var transaction = await _repository.GetAllAsync();
            return transaction.Select(t => MapToReadDto(t)).ToList();
        }
        public async Task<TransactionDto?> GetByIdAsync(int id)
        {
            var transaction = await _repository.GetByIdAsync(id);
            return transaction != null ? MapToReadDto(transaction) : null;
        }
        public async Task<IEnumerable<TransactionDto>> GetByUserIdAsync(int id)
        {
            var transactions = await _repository.GetByUserIdAsync(id);
            return transactions.Select(t => MapToReadDto(t)).ToList();
        }
        public async Task<IEnumerable<TransactionDto>> GetFamilyByUserIdAsync(int id)
        {
            var transactions = await _repository.GetFamilyByUserIdAsync(id);
            return transactions.Select(t => MapToReadDto(t)).ToList();
        }
        public async Task<TransactionDto?> CreateAsync(TransactionDto dto)
        {
            var user = await _userRepository.GetByIdAsync(dto.UserId);
            if (user == null) return null;
            var transaction = new Transaction
            {
                Id = dto.Id,
                Type = dto.Type,
                Value = dto.Value,
                Description = dto.Description,
                TransactionDate = DateTime.Parse(dto.TransactionDate),
                UserId = dto.UserId,
                User = user,
            };

            await _repository.AddAsync(transaction);
            await _repository.SaveChangesAsync();

            return MapToReadDto(transaction);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var transaction = await _repository.GetByIdAsync(id);
            if (transaction == null) return false;

            _repository.Delete(transaction);
            await _repository.SaveChangesAsync();

            return true;
        }

        TransactionDto MapToReadDto(Transaction transaction)
        {
            return new TransactionDto
            {
                Id = transaction.Id,
                Type = transaction.Type,
                Value = transaction.Value,
                Description = transaction.Description,
                TransactionDate = transaction.TransactionDate.ToString("yyyy-MM-dd"),
                UserId = transaction.UserId
            };
        }
    }
}
