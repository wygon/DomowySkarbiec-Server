using TukanTomek.Server.DTOs.FamilyDtos;
using TukanTomek.Server.DTOs.TransactionDtos;
using TukanTomek.Server.DTOs.UserDtos;
using TukanTomek.Server.Models;
using TukanTomek.Server.Repositories.Interfaces;
using TukanTomek.Server.Services.Interfaces;

namespace TukanTomek.Server.Services
{
    public class FamilyService : IFamilyService
    {
        private readonly IFamilyRepository _repository;
        private readonly IUserRepository _userRepository;
        private readonly ITransactionRepository _transactionRepository;

        public FamilyService(IFamilyRepository repository, IUserRepository userRepository, ITransactionRepository transactionRepository)
        {
            _repository = repository;
            _userRepository = userRepository;
            _transactionRepository = transactionRepository;
        }

        public async Task<IEnumerable<FamilyDto>> GetAllAsync()
        {
            var families = await _repository.GetAllAsync();
            return families.Select(u => MapToReadDto(u)).ToList();
        }
        public async Task<IEnumerable<UserDto>> GetAllFamilyUsersAsync(int id)
        {
            var users = await _repository.GetAllFamilyUsersAsync(id);
            return users.Select(u => 
            new UserDto
            {
                Id = u.Id,
                Name = u.Name,
                Email = u.Email,
                FamilyId = u.FamilyId,
            }
            ).ToList();
        }
        public async Task<FamilyDto?> GetByIdAsync(int id)
        {
            var family = await _repository.GetByIdAsync(id);

            return family != null ? MapToReadDto(family) : null;
        }
        public async Task<FamilyWithUsersDto?> GetFamilyUsersWithTransactionsAsync(int familyId)
        {
            var family = await _repository.GetByIdAsync(familyId);
            if (family == null) return null;
            var familyMembers = await _repository.GetAllFamilyUsersAsync(family.Id);
            if(familyMembers == null) return null;
            var usersWithTransaction = new List<UserWithTransactionsDto>();

            foreach(var member in familyMembers)
            {
                var transactions = await _transactionRepository.GetByUserIdAsync(member.Id);

                var userDto = new UserWithTransactionsDto
                {
                    Id = member.Id,
                    Name = member.Name,
                    Email = member.Email,
                    FamilyRole = member.FamilyRole,
                    Transactions = transactions.Select(t => new TransactionDto
                    {
                        Id = t.Id,
                        Type = t.Type,
                        Value = t.Value,
                        Description = t.Description,
                        TransactionDate = t.TransactionDate.ToString("yyyy-MM-dd"),
                        UserId = t.UserId,
                    }).ToList()
                };
              usersWithTransaction.Add(userDto);  
            }
            var result =  new FamilyWithUsersDto
            {
                Id = family.Id,
                Name = family.Name,
                Wage = family.Wage,
                Users = usersWithTransaction,
            };
            return result;
        }

        public async Task<FamilyDto?> CreateAsync(FamilyDto dto)
        {
            var family = new Family
            {
                Name = dto.Name,
                Wage = dto.Wage
            };

            await _repository.AddAsync(family);
            await _repository.SaveChangesAsync();

            return MapToReadDto(family);
        }

        public async Task<FamilyDto?> UpdateAsync(int id, FamilyDto dto)
        {
            var family = await _repository.GetByIdAsync(id);
            if (family == null) return null;

            family.Name = dto.Name;
            family.Wage = dto.Wage;

            _repository.Update(family);
            await _repository.SaveChangesAsync();

            return MapToReadDto(family);
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var family = await _repository.GetByIdAsync(id);
            if (family == null) return false;

            _repository.Delete(family);
            await _repository.SaveChangesAsync();

            return true;
        }
        FamilyDto MapToReadDto(Family family)
        {
            return new FamilyDto
            {
                Id = family.Id,
                Name = family.Name,
                Wage = family.Wage,
                UsersId = family.Users?.Select(u => u.Id).ToList() ?? new List<int>(),
                Users = family.Users?.Select(u => new UserDto
                {
                    Id = u.Id,
                    Name = u.Name,
                    Email = u.Email,
                    FamilyId = u.FamilyId,
                }).ToList() ?? new List<UserDto>(),
            };
        }
    }
}
