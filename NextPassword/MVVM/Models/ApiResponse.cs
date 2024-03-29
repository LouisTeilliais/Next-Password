using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextPassword.MVVM.Models
{
    public class ApiResponse<T>
    {
        public int StatusCode { get; set; }
#pragma warning disable CS8618 // Un champ non-nullable doit contenir une valeur non-null lors de la fermeture du constructeur. Envisagez de déclarer le champ comme nullable.
        public T Results { get; set; }
#pragma warning restore CS8618 // Un champ non-nullable doit contenir une valeur non-null lors de la fermeture du constructeur. Envisagez de déclarer le champ comme nullable.

        public void SetApiResponse(int NewStatusCode, T NewResults)
        {
            StatusCode = NewStatusCode;
            Results = NewResults;
        }
    }
}
