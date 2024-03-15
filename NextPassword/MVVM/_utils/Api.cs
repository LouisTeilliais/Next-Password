using NextPassword.MVVM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace NextPassword.MVVM._utils
{
    public class Api
    {
        static string BaseUrl = "http://localhost:5000";

        static HttpClient client = new HttpClient();
        static async Task<object> GetItemsAsync(string path)
        {
            object result = null;
            HttpResponseMessage response = await client.GetAsync(BaseUrl + path);
            if (response != null)
            {
                result = await response.Content.ReadAsStringAsync();
            }
            return result;
        }

        static async Task<object> UpdateItemsAsync(string path, Password password)
        {
            HttpResponseMessage response = await client.PutAsJsonAsync($"{path}/{password.Id}", password);
            response.EnsureSuccessStatusCode();

            password = await response.Content.ReadFromJsonAsync<Password>();
            return password;
        }
    }
}
