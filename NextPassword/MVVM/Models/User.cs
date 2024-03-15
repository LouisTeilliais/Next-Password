using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextPassword.MVVM.Models
{
    public class User
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string NormalizedUserName { get; set; }
        public required string Email { get; set; }
        public string NormalizedEmail { get; set; }
        public required bool EmailConfirmed { get; set; }
        public required string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        public string ConcurrencyStamp { get; set; }
        public string PhoneNumber { get; set; }
        public required bool PhoneNumberConfirmed { get; set; }
    }
}
