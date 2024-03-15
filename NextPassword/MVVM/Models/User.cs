using System.Diagnostics.CodeAnalysis;

namespace NextPassword.MVVM.Models
{
    public class User : UserBase
    {
        public string? Id { get; set; }
        public string? Username { get; set; }
        public string? NormalizedUserName { get; set; }
        public string? NormalizedEmail { get; set; }
        public bool? EmailConfirmed { get; set; }
        public string? SecurityStamp { get; set; }
        public string? ConcurrencyStamp { get; set; }
        public string? PhoneNumber { get; set; }
        public bool? PhoneNumberConfirmed { get; set; }

        public User(string email, string password) : base(email, password)
        {
        }

    }
}
