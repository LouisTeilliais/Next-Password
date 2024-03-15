﻿using NextPassword.MVVM.Models;
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
        static string BaseUrl = "http://localhost:5017";

        static HttpClient client = new HttpClient();
        protected async Task<object> GetItemsAsync(string path)
        {
            object result = null;
            HttpResponseMessage response = await client.GetAsync(BaseUrl + path);
            if (response != null)
            {
                result = await response.Content.ReadAsStringAsync();
            }
            return result;
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
