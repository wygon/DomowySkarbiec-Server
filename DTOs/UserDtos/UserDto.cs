namespace TukanTomek.Server.DTOs.UserDtos
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string? FamilyRole { get; set; }
        public int? FamilyId { get; set; }
    }
}
