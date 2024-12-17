using Microsoft.Maui.Controls.PlatformConfiguration;
using MAUI_Quiz.Models;
using System.Net.Http.Json;

namespace MAUI_Quiz.Services
{
    public class TriviaService
    {
        HttpClient httpClient;
        List<Trivia> trivias = new List<Trivia>();

        public TriviaService()
        {
            httpClient = new HttpClient();
        }

        public async Task<List<Trivia>> GetTriviaAsync(string category = "")
        {
            string apiKey = await SecureStorage.Default.GetAsync("api_key");

            if (string.IsNullOrEmpty(apiKey))
            {
                // If key doesnt exist, set it
                await SecureStorage.Default.SetAsync("api_key", "YOUR-API-KEY");

            }
            else
            {
                // If key exists, get it
                apiKey = await SecureStorage.Default.GetAsync("api_key");
            }

            var url = "https://api.api-ninjas.com/v1/trivia";

            if (!string.IsNullOrEmpty(category))
            {
               url = $"https://api.api-ninjas.com/v1/trivia?category={category}";
            }
            
            // Adds key to header and clears header before a new request is done to avoid duplicates
            httpClient.DefaultRequestHeaders.Clear();
            httpClient.DefaultRequestHeaders.Add("X-Api-Key", apiKey);
            var response = await httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                trivias = await response.Content.ReadFromJsonAsync<List<Trivia>>();
            }            
            return trivias;
        }
    }
}
