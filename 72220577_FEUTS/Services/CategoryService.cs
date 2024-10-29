using _72220577_FEUTS.Model;
using System.Text.Json;

namespace _72220577_FEUTS.Services
{
    public class CategoryService
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "https://actualbackendapp.azurewebsites.net/api/v1/";

        public CategoryService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<category>> GetCategoriesAsync()
        {
            var response = await _httpClient.GetAsync($"{BaseUrl}Categories");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<category>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
    }

}
