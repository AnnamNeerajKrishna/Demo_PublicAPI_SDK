using Public_Api_Demo.Interfaces;
using Public_Api_Demo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Public_Api_Demo.Services
{
    public class UserService : IUserService
    {
        private readonly HttpClient _httpClient;
        public UserService()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://reqres.in/")
            };
            _httpClient.DefaultRequestHeaders.Add("x-api-key", "reqres-free-v1");
        }

    

        public async Task<User> GetUserById(int id)
        {
            var res = await _httpClient.GetAsync($"api/users/{id}");
            //Console.WriteLine("Raw JSON from API:\n" + res);
            if (!res.IsSuccessStatusCode)
            {
                    Console.WriteLine($"Error: {res.StatusCode} - {res.ReasonPhrase}");
                    return null;
                
            }
            if (res.IsSuccessStatusCode) 
            {
                var json = res.Content.ReadAsStringAsync();
                //Console.WriteLine("Raw JSON from API:\n" + json);

                var userResponse = JsonSerializer.Deserialize<UserResponse>(await json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                return userResponse.Data;
            }
            return null;
        }
        public async Task<List<User>> GetAllUsers(int page = 1)
        {
            var response = await _httpClient.GetAsync($"/api/users?page={page}");

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
                return new List<User>();
            }

            var json = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<UserListWrapper>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return result?.Data ?? new List<User>();
        }
      


    }
}
