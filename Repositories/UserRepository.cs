using Microsoft.EntityFrameworkCore;
using TukanTomek.Server.Data;
using TukanTomek.Server.Models;
using TukanTomek.Server.Repositories.Interfaces;

namespace TukanTomek.Server.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;
        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }        
        public async Task<Family?> GetFamilyByUserIdAsync(int id)
        {
            var user = await _context.Users
                .Include(u => u.Family)
                .ThenInclude(f => f.Users)
                .FirstOrDefaultAsync(u => u.Id == id);

            return user?.Family;
        }

        public async Task<User?> GetByNameAsync(string name)
        {
            return await _context.Users
                .FirstOrDefaultAsync(user => user.Name == name);
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.Users
                .FirstOrDefaultAsync(user => user.Email == email);
        }

        public async Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
        }

        public void Update(User user) 
        {
            _context.Users.Update(user);
        }
        public void Delete(User user)
        {
            _context.Users.Remove(user);
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

    }
}
