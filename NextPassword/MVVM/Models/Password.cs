using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextPassword.MVVM.Models
{
    public class Password
    {
#pragma warning disable CS8618 // Un champ non-nullable doit contenir une valeur non-null lors de la fermeture du constructeur. Envisagez de déclarer le champ comme nullable.
        public string Id { get; set; }
#pragma warning restore CS8618 // Un champ non-nullable doit contenir une valeur non-null lors de la fermeture du constructeur. Envisagez de déclarer le champ comme nullable.
        public required string Title { get; set;}
        public required string PasswordHash { get; set; }
        public required string Notes { get; set;}
        public required string Username { get; set; }
        public required string Url { get; set; }
    }
}
