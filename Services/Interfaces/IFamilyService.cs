using TukanTomek.Server.DTOs.FamilyDtos;
using TukanTomek.Server.DTOs.UserDtos;

namespace TukanTomek.Server.Services.Interfaces
{
    public interface IFamilyService
    {
        Task<IEnumerable<FamilyDto>> GetAllAsync();
        Task<IEnumerable<UserDto>> GetAllFamilyUsersAsync(int id);
        Task<FamilyWithUsersDto> GetFamilyUsersWithTransactionsAsync(int id);
        Task<FamilyDto?> GetByIdAsync(int id);
        Task<FamilyDto?> CreateAsync(FamilyDto dto);
        Task<FamilyDto?> UpdateAsync(int id, FamilyDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
