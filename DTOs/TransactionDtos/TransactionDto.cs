using TukanTomek.Server.DTOs.UserDtos;

namespace TukanTomek.Server.DTOs.TransactionDtos
{
    public class TransactionDto
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public double Value { get; set; }
        public string Description { get; set; }
        public string TransactionDate { get; set; }
        public int UserId { get; set; }
    }
}
