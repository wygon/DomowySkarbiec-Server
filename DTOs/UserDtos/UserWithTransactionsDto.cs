using TukanTomek.Server.DTOs.TransactionDtos;

namespace TukanTomek.Server.DTOs.UserDtos
{
    public class UserWithTransactionsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public List<TransactionDto> Transactions { get; set; } = new();
    }
}
