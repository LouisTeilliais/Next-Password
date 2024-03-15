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
using System.Threading.Tasks;

namespace NextPassword.MVVM._utils
{
    public class Api<T>
    {
        static string BaseUrl = "http://localhost:5017";

        static HttpClient client = new HttpClient();
        protected async Task<ApiResponse<T>> GetItemsAsync(string path)
        {
            ApiResponse<T> apiResponse = new ApiResponse<T>();

            

            HttpResponseMessage response = await client.GetAsync(BaseUrl + path);
            /*            if (response != null)
                        {
                            result = await response.Content.ReadFromJsonAsync<T>();
                        }*/

            T result = await response.Content.ReadFromJsonAsync<T>();

            apiResponse.SetApiResponse(((int)response.StatusCode), result);
            return apiResponse;
        }

        public async Task<ApiResponse<T>> CreateItemsAsync(string path, object items)
        {
            ApiResponse<T> apiResponse = new ApiResponse<T>();

            try
            {
                /* Serialize JSON object, change name to respect camelCase for API */
                string jsonItems = JsonConvert.SerializeObject(items, Formatting.None, new JsonSerializerSettings
                {
                    ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver()
                });

                var data = new StringContent(jsonItems, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(BaseUrl + path, data);

                // Check if the request was successful
                if (response.IsSuccessStatusCode)
                {
                    // Deserialize the response content into an object of type T
                    T createdItem = await response.Content.ReadFromJsonAsync<T>();

                    // Set the API response with the status code and created item
                    apiResponse.SetApiResponse((int)response.StatusCode, createdItem);
                }
            } 
            catch (Exception ex)
            {
                throw new Exception($"Post request internal server error : {ex.Message}");
            }
            

            // Return URI of the created resource.
            return apiResponse;
            /*            ApiResponse<T> apiResponse = new ApiResponse<T>();
                        string jsonItems = JsonConvert.SerializeObject(items);
                        Trace.WriteLine(jsonItems);
                        HttpResponseMessage response = await client.PostAsJsonAsync(BaseUrl + path, jsonItems);
                        response.EnsureSuccessStatusCode();

                        items = await response.Content.ReadFromJsonAsync<T>();

                        apiResponse.SetApiResponse((int)response.StatusCode, (List<T>)items);

                        // return URI of the created resource.
                        return apiResponse;*/
        }

        protected async Task<ApiResponse<T>> UpdateItemsAsync(string path, object items)
        {
            ApiResponse<T> apiResponse = new ApiResponse<T>();
            if (items == null)
            {
                // Lève une exception appropriée ici
                throw new ArgumentNullException(nameof(items), "L'argument items ne peut pas être null.");
            }

            // Cast items to IHasId interface to access the ID property
            if (items is IHasId hasIdItem)
            {
                HttpResponseMessage response = await client.PutAsJsonAsync($"{path}/{hasIdItem.ID}", items);
                response.EnsureSuccessStatusCode();

                T result = await response.Content.ReadFromJsonAsync<T>();
                apiResponse.SetApiResponse((int)response.StatusCode, result);
            }
            else
            {
                // Throw an exception or handle the case where items doesn't implement IHasId
                throw new ArgumentException("The items argument must implement the IHasId interface.", nameof(items));
            }

           /* HttpResponseMessage response = await client.PutAsJsonAsync($"{path}/{items.ID.ToString()}", items);
            response.EnsureSuccessStatusCode();

            items = await response.Content.ReadFromJsonAsync<T>();
            apiResponse.SetApiResponse((int)response.StatusCode, items);*/
            return apiResponse;
        }
    }
}
