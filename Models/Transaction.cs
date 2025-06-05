namespace TukanTomek.Server.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public double Value { get; set; }
        public string Description { get; set; }
        public DateTime TransactionDate { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}
