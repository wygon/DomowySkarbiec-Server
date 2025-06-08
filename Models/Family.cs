using TukanTomek.Server.DTOs.UserDtos;

namespace TukanTomek.Server.Models
{
    public class Family
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Wage { get; set; }

        public IEnumerable<User>? Users { get; set; }

    }
}
