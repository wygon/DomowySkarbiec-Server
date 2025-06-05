using TukanTomek.Server.Models;

namespace TukanTomek.Server.Repositories.Interfaces
{
    public interface ITransactionRepository
    {
        Task<IEnumerable<Transaction>> GetAllAsync();
        Task<Transaction?> GetByIdAsync(int id);
        Task<IEnumerable<Transaction>> GetByUserIdAsync(int id);
        Task<IEnumerable<Transaction>> GetFamilyByUserIdAsync(int id);
        Task<Transaction?> GetByTypeAsync(string type);
        Task AddAsync(Transaction transaction);
        void Update(Transaction transaction);
        void Delete(Transaction transaction);
        Task SaveChangesAsync();
    }
}
