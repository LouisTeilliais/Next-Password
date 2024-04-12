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
        public T Results { get; set; }
        public string Headers { get; set; }

        public void SetApiResponse(int NewStatusCode, T NewResults)
        {
            StatusCode = NewStatusCode;
            Results = NewResults;
        }
    }
}
