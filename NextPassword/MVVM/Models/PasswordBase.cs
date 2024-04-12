using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace NextPassword.MVVM.Models
{
    public class PasswordBase
    {
        public string? Id;
        public string PasswordHash { get; set; }
        public string Title { get; set; }

        public PasswordBase(string? id, string title, string password)
        {
            Id = id;
            Title = title;
            PasswordHash = password;
        }
    }
}
