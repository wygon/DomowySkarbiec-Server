using TukanTomek.Server.DTOs.UserDtos;

namespace TukanTomek.Server.DTOs.FamilyDtos
{
    public class FamilyWithUsersDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Wage { get; set; }
        public List<UserWithTransactionsDto> Users { get; set; } = new();
    }
}
