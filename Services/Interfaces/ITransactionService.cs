using TukanTomek.Server.DTOs.TransactionDtos;

namespace TukanTomek.Server.Services.Interfaces
{
    public interface ITransactionService
    {
        Task<IEnumerable<TransactionDto>> GetAllAsync();
        Task<TransactionDto?> GetByIdAsync(int id);
        Task<IEnumerable<TransactionDto>> GetByUserIdAsync(int id);
        Task<IEnumerable<TransactionDto>> GetFamilyByUserIdAsync(int id);
        Task<TransactionDto?> CreateAsync(TransactionDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
