using TukanTomek.Server.Models;

namespace TukanTomek.Server.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task<User?> GetByIdAsync(int id);
        Task<User?> GetByNameAsync(string name);
        Task<User?> GetByEmailAsync(string email);
        Task<Family?> GetFamilyByUserIdAsync(int id);
        Task AddAsync(User user);
        void Update(User user);
        void Delete(User user);
        Task SaveChangesAsync();
    }
}
