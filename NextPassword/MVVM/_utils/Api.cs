using NextPassword.MVVM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace NextPassword.MVVM._utils
{
    public class Api<T>
    {
        static string BaseUrl = "http://localhost:5017";

        static HttpClient client = new HttpClient();
        public async Task<ApiResponse<T>> GetPasswordsAsync<T>(string path)
        {
            ApiResponse<T> apiResponse = new ApiResponse<T>();

            try
            {
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, BaseUrl + path);

                HttpResponseMessage response = await client.SendAsync(request);
                apiResponse.StatusCode = (int)response.StatusCode;

                if (response.IsSuccessStatusCode)
                {
                    apiResponse.Results = await response.Content.ReadFromJsonAsync<List<T>>();
                }
                else
                {
                    // Gérer les erreurs de requête ici si nécessaire
                    apiResponse.Results = new List<T>(); // Ou null si vous préférez
                }
            }
            catch (Exception ex)
            {
                // Gérer les exceptions ici
                Console.WriteLine($"Une erreur s'est produite lors de la récupération des données de l'API : {ex.Message}");
                apiResponse.StatusCode = 500; // Utilisez un code d'erreur approprié
                apiResponse.Results = new List<T>(); // Ou null si vous préférez
            }
            return apiResponse;
        }


        protected async Task<object> CreateItemsAsync(string path, object items)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync(BaseUrl + path, items);
            response.EnsureSuccessStatusCode();

            items = await response.Content.ReadFromJsonAsync<object>();

            // return URI of the created resource.
            return items;
        }

        protected async Task<object> UpdateItemsAsync(string path, Password password)
        {
            HttpResponseMessage response = await client.PutAsJsonAsync($"{path}/{password.Id}", password);
            response.EnsureSuccessStatusCode();

            password = await response.Content.ReadFromJsonAsync<Password>();
            return password;
        }
    }
}
