using Microsoft.AspNetCore.Http.HttpResults;
using TukanTomek.Server.DTOs.FamilyDtos;
using TukanTomek.Server.DTOs.UserDtos;
using TukanTomek.Server.Models;
using TukanTomek.Server.Repositories.Interfaces;
using TukanTomek.Server.Services.Interfaces;

namespace TukanTomek.Server.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<UserDto>> GetAllAsync()
        {
            var users = await _repository.GetAllAsync();
            return users.Select(u => MapToReadDto(u)).ToList();
        }
        public async Task<UserDto?> GetByIdAsync(int id)
        {
            var user = await _repository.GetByIdAsync(id);

            return user != null ? MapToReadDto(user) : null;
        }        
        public async Task<FamilyDto?> GetFamilyByUserIdAsync(int id)
        {
            var family = await _repository.GetFamilyByUserIdAsync(id);

            return family != null ? new FamilyDto
            {
                Id = family.Id,
                Name = family.Name,
                Wage = family.Wage,
                Users = family.Users.Select(u => new UserDto
                {
                    Id = u.Id,
                    Name = u.Name,
                    Email = u.Email,
                    FamilyId = u.FamilyId,
                }),
                UsersId = family.Users.Select(u => u.Id),
            } 
            : null;
        }

        public async Task<UserDto?> CreateAsync(UserDto dto)
        {
            if (await _repository.GetByEmailAsync(dto.Email) != null) return null;

            var user = new User
            {
                Name = dto.Name,
                Email = dto.Email,
                FamilyId = dto.FamilyId,
            };

            await _repository.AddAsync(user);
            await _repository.SaveChangesAsync();

            return MapToReadDto(user);
        }

        public async Task<UserDto?> UpdateAsync(int id, UserDto dto)
        {
            var user = await _repository.GetByIdAsync(id);
            if(user == null) return null;   
            
            user.Name = dto.Name;
            user.Email = dto.Email;
            user.FamilyId = dto.FamilyId;

            _repository.Update(user);
            await _repository.SaveChangesAsync();

            return MapToReadDto(user);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var user = await _repository.GetByIdAsync(id);
            if (user == null) return false;

            _repository.Delete(user);
            await _repository.SaveChangesAsync();

            return true;
        }
        UserDto MapToReadDto(User user)
        {
            return new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                FamilyId = user.FamilyId,
            };
        }
    }
}
