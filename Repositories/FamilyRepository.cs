using Microsoft.EntityFrameworkCore;
using TukanTomek.Server.Data;
using TukanTomek.Server.DTOs.UserDtos;
using TukanTomek.Server.Models;
using TukanTomek.Server.Repositories.Interfaces;

namespace TukanTomek.Server.Repositories
{
    public class FamilyRepository : IFamilyRepository
    {
        private readonly AppDbContext _context;
        public FamilyRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Family>> GetAllAsync()
        {
            return await _context.Families.ToListAsync();
        }
        public async Task<IEnumerable<User>> GetAllFamilyUsersAsync(int familyId)
        {
            var users =  await _context.Users
                .Where(u => u.FamilyId == familyId)
                .ToListAsync();

            return users;
        }
        public async Task<Family?> GetByIdAsync(int id)
        {
            return await _context.Families.FindAsync(id);
            //return await _context.Families
            //    .Include(f => f.Users)
            //    .FirstOrDefaultAsync();
        }

        public async Task<Family?> GetByNameAsync(string name)
        {
            return await _context.Families.FindAsync(name);
        }

        public async Task AddAsync(Family family)
        {
            await _context.Families.AddAsync(family);
        }

        public void Update(Family family) 
        {
            _context.Families.Update(family);
        }
        public void Delete(Family family)
        {
            _context.Families.Remove(family);
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }   
}
