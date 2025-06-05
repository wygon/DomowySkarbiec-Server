using TukanTomek.Server.DTOs.FamilyDtos;
using TukanTomek.Server.DTOs.UserDtos;

namespace TukanTomek.Server.Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAllAsync();
        Task<UserDto?> GetByIdAsync(int id);
        Task<FamilyDto?> GetFamilyByUserIdAsync(int id);
        Task<UserDto?> CreateAsync(UserDto dto);
        Task<UserDto?> UpdateAsync(int id, UserDto dto);
        Task<bool> DeleteAsync(int id);

    }
}
