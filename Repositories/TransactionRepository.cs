using Microsoft.EntityFrameworkCore;
using TukanTomek.Server.Data;
using TukanTomek.Server.Models;
using TukanTomek.Server.Repositories.Interfaces;

namespace TukanTomek.Server.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly AppDbContext _context;
        public TransactionRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Transaction>> GetAllAsync()
        {
            return await _context.Transactions.ToListAsync();
        }        
        public async Task<IEnumerable<Transaction>> GetByUserIdAsync(int id)
        {
            return await _context.Transactions
                .Where(t => t.UserId == id).ToListAsync();
        }        
        public async Task<IEnumerable<Transaction>> GetFamilyByUserIdAsync(int familyId)
        {
            return await _context.Transactions
                .Include(t => t.User)
                .Where(t => t.User.FamilyId == familyId)
                .ToListAsync();
        }

        public async Task<Transaction?> GetByIdAsync(int id)
        {
            return await _context.Transactions.FindAsync(id);
        }

        public async Task<Transaction?> GetByTypeAsync(string type)
        {
            return await _context.Transactions
                .Where(t => t.Type == type)
                .FirstOrDefaultAsync();
        }

        public async Task AddAsync(Transaction transaction)
        {
            await _context.Transactions.AddAsync(transaction);
        }

        public void Update(Transaction transaction) 
        {
            _context.Transactions.Update(transaction);
        }
        public void Delete(Transaction transaction)
        {
            _context.Transactions.Remove(transaction);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
