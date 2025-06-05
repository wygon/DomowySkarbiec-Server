using TukanTomek.Server.Models;

namespace TukanTomek.Server.Repositories.Interfaces
{
    public interface IFamilyRepository
    {
        Task<IEnumerable<Family>> GetAllAsync();
        Task<Family?> GetByIdAsync(int id);
        Task<Family?> GetByNameAsync(string name);
        Task<IEnumerable<User>> GetAllFamilyUsersAsync(int familyId);
        Task AddAsync(Family family);
        void Update(Family family);
        void Delete(Family family);
        Task SaveChangesAsync();
    }
}
