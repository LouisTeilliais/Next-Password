using Newtonsoft.Json;
using NextPassword.MVVM._utils.Interface;
using NextPassword.MVVM.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using System.Text.Json;
using System.Windows;

namespace NextPassword.MVVM._utils
{
    public class Api<T>
    {
        static string baseUrl = "http://localhost:5017";

        static HttpClient client = new HttpClient();

        public async Task<ApiResponse<T>> GetItemsAsync(string path)
        {
            ApiResponse<T> apiResponse = new ApiResponse<T>();
            var cookies = CookieManager.GetCookies();

            foreach (var cookie in cookies)
            {
                client.DefaultRequestHeaders.Add("Cookie", cookie);
            }

            HttpResponseMessage response = await client.GetAsync(baseUrl + path);

            if (response.IsSuccessStatusCode)
            {
                string itemStr = await response.Content.ReadAsStringAsync();
                T item = default;

                if (!string.IsNullOrEmpty(itemStr))
                {
                    item = JsonConvert.DeserializeObject<T>(itemStr);
                }

                apiResponse.SetApiResponse((int)response.StatusCode, item);
            }
            else
            {
                // Set API response if not success
                apiResponse.SetApiResponse((int)response.StatusCode, default);
            }

            return apiResponse;
        }

        public async Task<ApiResponse<T>> CreateItemsAsync(string path, object items)
        {
            ApiResponse<T> apiResponse = new ApiResponse<T>();
            HttpResponseMessage response;

            try
            {
                string jsonItems = JsonConvert.SerializeObject(items, Formatting.None, new JsonSerializerSettings
                {
                    ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver()
                });
                var data = new StringContent(jsonItems, Encoding.UTF8, "application/json");

                response = await client.PostAsync(baseUrl + path, data);

                if (response.IsSuccessStatusCode) {
                    string createdItemStr = await response.Content.ReadAsStringAsync();
                    var cookies = response.Headers.GetValues("Set-Cookie");
                    T createdItem = default;

                    if (!string.IsNullOrEmpty(createdItemStr)) {
                        createdItem = JsonConvert.DeserializeObject<T>(createdItemStr);
                    }

                    apiResponse.SetApiResponse((int)response.StatusCode, createdItem);
                    CookieManager.SetCookies(cookies);
                } else {
                    // Si la réponse n'est pas un succès, définissez la réponse API avec le code d'état de la réponse et la valeur par défaut
                    apiResponse.SetApiResponse((int)response.StatusCode, default);
                }
                
            } 
            catch (Exception ex)
            {
                throw new Exception($"Post request internal server error : {ex.Message}");
            }


            // Return URI of the created resource.
            return apiResponse;
        }

        protected async Task<ApiResponse<T>> UpdateItemsAsync(string path, object items) {
            ApiResponse<T> apiResponse = new ApiResponse<T>();
            if (items == null) {
                throw new ArgumentNullException(nameof(items), "L'argument items ne peut pas être null.");
            }

            // Cast items to IHasId interface to access the ID property
            if (items is IHasId hasIdItem)
            {
                HttpResponseMessage response = await client.PutAsJsonAsync($"{path}/{hasIdItem.ID}", items);

                if (response.IsSuccessStatusCode)
                {
                    string updatedItemStr = await response.Content.ReadAsStringAsync();
                    T updatedItem = default;

                    if (!string.IsNullOrEmpty(updatedItemStr))
                    {
                        updatedItem = JsonConvert.DeserializeObject<T>(updatedItemStr);
                    }

                    apiResponse.SetApiResponse((int)response.StatusCode, updatedItem);
                }
                else
                {
                    // Si la réponse n'est pas un succès, définissez la réponse API avec le code d'état de la réponse et la valeur par défaut
                    apiResponse.SetApiResponse((int)response.StatusCode, default);
                }
            }
            else
            {
                // Throw an exception or handle the case where items doesn't implement IHasId
                throw new ArgumentException("The items argument must implement the IHasId interface.", nameof(items));
            }

            return apiResponse;
        }
    }
}
