using System.Net.Http.Json;
using System.Text.Json.Serialization;
//using AFStudiumApp.Models;
using Microsoft.VisualBasic;
using Newtonsoft.Json;

public class ApiService
{
    private readonly HttpClient _httpClient;

    public ApiService()
    {
        _httpClient = new HttpClient()
        {
            //BaseAddress = new Uri("https://localhost:7123/api/User")
        };
    }

    /*public async Task<List<string>> GetEntitiesAsync()
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<List<string>>("/api/User");
        }
        catch (Exception ex)
        {
            // Handle/log exception
            return new List<string>();
        }
    }*/
   /* public async Task<List<User>> GetUsersAsync()
    {
        //var response = await _httpClient.GetAsync(new Uri(string.Format(Constants.RestUrl,String.Empty)));

        /*if (response.IsSuccessStatusCode)
        {
            var users = await response.Content.ReadFromJsonAsync<List<User>>();
            return users;
        }
        else
        {
            // Log status code and content for debugging
            var errorMessage = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Request failed: {response.StatusCode}, {errorMessage}");
            return null;
        }
    }*/


   /* public async Task PostEntityAsync(User user)
    {
        await _httpClient.PostAsJsonAsync("/api/User", user);
    }*/
}
