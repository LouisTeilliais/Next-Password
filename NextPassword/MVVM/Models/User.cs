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

        public User(string email, string password, string? userName, string? normalizedUserName, string? normalizedEmail, bool? emailConfirmed, string? securityStamp, string? concurrencyStamp, string? phoneNumber, bool? phoneNumberConfirmed) : base(email, password)
        {
            Username = userName;
            NormalizedUserName = normalizedUserName;
            NormalizedEmail = normalizedEmail;
            EmailConfirmed = emailConfirmed;
            SecurityStamp = securityStamp;
            PhoneNumber = phoneNumber;
            PhoneNumberConfirmed = phoneNumberConfirmed;
            EmailConfirmed = emailConfirmed;
            ConcurrencyStamp = concurrencyStamp;

        }

    }
}
