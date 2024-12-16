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
                // Om nyckeln inte finns, sätt den här
                await SecureStorage.Default.SetAsync("api_key", "YOUR-API-KEY");

            }
            else
            {
                // Om nyckeln redan finns, använd den
                apiKey = await SecureStorage.Default.GetAsync("api_key");
            }
            var url = "https://api.api-ninjas.com/v1/trivia";
            if (!string.IsNullOrEmpty(category))
            {
               url = $"https://api.api-ninjas.com/v1/trivia?category={category}";
            }
            
            // Lägger till API-nyckeln i headern samt rensar så det inte blir dubletter vid nya requests
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
