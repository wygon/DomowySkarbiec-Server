﻿namespace TukanTomek.Server.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public int? FamilyId { get; set; }
        public Family? Family { get; set; }
        //public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
    }
}
