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
using NextPassword.MVVM._utils.Validator;

namespace NextPassword.MVVM._utils
{
    public class Api<T>
    {
        static string baseUrl = "http://localhost:5017";

        static HttpClient client = new HttpClient();

        public async Task<ApiResponse<T>> GetItemsAsync(string path, bool? isCookieNecessary = false)
        {
            ApiResponse<T> apiResponse = new ApiResponse<T>();

            try
            {
                // Token added in header if necessary
                if (isCookieNecessary == true)
                {
                    var cookies = CookieManager.GetCookies();

                    foreach (var cookie in cookies)
                    {
                        client.DefaultRequestHeaders.Add("Cookie", cookie);
                    }
                }

                HttpResponseMessage response = await client.GetAsync(baseUrl + path);

                if (response.IsSuccessStatusCode)
                {
                    string itemStr = await response.Content.ReadAsStringAsync();

                    bool isResponseParsable = TypeValidator.TryParseJSON(itemStr);

                    T item = default;

                    if (isResponseParsable)
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
            }
            catch (Exception ex)
            {
                throw new Exception($"Get request internal server error : {ex.Message}");
            }

            return apiResponse;
        }

        public async Task<ApiResponse<T>> CreateItemsAsync(string path, object items, bool? isCookieNecessary = false)
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

                // Token added in header if necessary
                if (isCookieNecessary == true)
                {
                    var cookies = CookieManager.GetCookies();

                    foreach (var cookie in cookies)
                    {
                        client.DefaultRequestHeaders.Add("Cookie", cookie);
                    }
                }

                response = await client.PostAsync(baseUrl + path, data);

                if (response.IsSuccessStatusCode) {
                    string? createdItemStr = await response.Content.ReadAsStringAsync();

                    bool isResponseParsable = TypeValidator.TryParseJSON(createdItemStr);
                    
                    T createdItem = default;

                    if (isResponseParsable) {
                        createdItem = JsonConvert.DeserializeObject<T>(createdItemStr);
                    }

                    apiResponse.SetApiResponse((int)response.StatusCode, createdItem);

                    // Adds the connection token to the cookies (Cookies table) during connection
                    if (isCookieNecessary == false && response.Headers.Contains("Set-Cookie"))
                    {
                        var cookies = response.Headers.GetValues("Set-Cookie");
                        CookieManager.SetCookies(cookies);
                    }
                    
                } else {
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

        public async Task<ApiResponse<T>> UpdateItemsAsync(string path, object items, bool? isCookieNecessary = false) {
            if (items == null)
            {
                throw new ArgumentNullException(nameof(items), "L'argument items ne peut pas être null.");
            }

            ApiResponse<T> apiResponse = new ApiResponse<T>();

            try
            {
                // Token added in header if necessary
                if (isCookieNecessary == true)
                {
                    var cookies = CookieManager.GetCookies();

                    foreach (var cookie in cookies)
                    {
                        client.DefaultRequestHeaders.Add("Cookie", cookie);
                    }
                }

                HttpResponseMessage response = await client.PutAsJsonAsync(baseUrl + path, items);

                if (response.IsSuccessStatusCode)
                {
                    string? updatedItemStr = await response.Content.ReadAsStringAsync();

                    bool isResponseParsable = TypeValidator.TryParseJSON(updatedItemStr);

                    T updatedItem = default;

                    if (isResponseParsable)
                    {
                        updatedItem = JsonConvert.DeserializeObject<T>(updatedItemStr);
                    }

                    apiResponse.SetApiResponse((int)response.StatusCode, updatedItem);

                    // Adds the connection token to the cookies (Cookies table) during connection
                    if (isCookieNecessary == false && response.Headers.Contains("Set-Cookie"))
                    {
                        var cookies = response.Headers.GetValues("Set-Cookie");
                        CookieManager.SetCookies(cookies);
                    }
                }
                else
                {
                    // If the response is not successful, set the API response with the response status code and the default value
                    apiResponse.SetApiResponse((int)response.StatusCode, default);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Update request internal server error {ex.Message}");
            }
            
            return apiResponse;
        }

        public async Task<ApiResponse<T>> DeleteItemsAsync(string path, bool? isCookieNecessary = false)
        {
            ApiResponse<T> apiResponse = new ApiResponse<T>();

            if (path.Length == 0)
            {
                return null;
            }

            try
            {
                // Token added in header if necessary
                if (isCookieNecessary == true)
                {
                    var cookies = CookieManager.GetCookies();

                    foreach (var cookie in cookies)
                    {
                        client.DefaultRequestHeaders.Add("Cookie", cookie);
                    }
                }

                HttpResponseMessage response = await client.DeleteAsync($"{baseUrl}{path}");

                if (response.IsSuccessStatusCode)
                {
                    string deleteItemStr = await response.Content.ReadAsStringAsync();

                    bool isResponseParsable = TypeValidator.TryParseJSON(deleteItemStr);

                    T deletedItem = default;

                    if (isResponseParsable)
                    {
                        deletedItem = JsonConvert.DeserializeObject<T>(deleteItemStr);
                    }

                    apiResponse.SetApiResponse((int)response.StatusCode, deletedItem);

                    // Adds the connection token to the cookies (Cookies table) during connection
                    if (isCookieNecessary == false && response.Headers.Contains("Set-Cookie"))
                    {
                        var cookies = response.Headers.GetValues("Set-Cookie");
                        CookieManager.SetCookies(cookies);
                    }
                }
                else
                {
                    // Set API response if not success
                    apiResponse.SetApiResponse((int)response.StatusCode, default);
                }

            } 
            catch (Exception ex)
            {
                throw new Exception($"Delete request internal server error {ex.Message}");
            }
            return apiResponse;

        }
    }
}
