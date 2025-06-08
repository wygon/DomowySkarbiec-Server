using TukanTomek.Server.DTOs.UserDtos;

namespace TukanTomek.Server.DTOs.FamilyDtos
{
    public class FamilyDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Wage { get; set; }

        public IEnumerable<int>? UsersId { get; set; }
        public IEnumerable<UserDto>? Users { get; set; }

    }
}
