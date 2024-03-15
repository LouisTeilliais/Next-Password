using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextPassword.MVVM.Models
{
    public class Password
    {
        public string Id { get; set; }
        public required string Title { get; set;}
        public required string PasswordHash { get; set; }
        public required string Notes { get; set;}
        public required string Username { get; set; }
        public required string Url { get; set; }
    }
}
