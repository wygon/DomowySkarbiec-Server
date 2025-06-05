using TukanTomek.Server.DTOs.FamilyDtos;
using TukanTomek.Server.DTOs.UserDtos;
using TukanTomek.Server.Models;
using TukanTomek.Server.Repositories.Interfaces;
using TukanTomek.Server.Services.Interfaces;

namespace TukanTomek.Server.Services
{
    public class FamilyService : IFamilyService
    {
        private readonly IFamilyRepository _repository;

        public FamilyService(IFamilyRepository repository)
        {
            _repository = repository;
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
