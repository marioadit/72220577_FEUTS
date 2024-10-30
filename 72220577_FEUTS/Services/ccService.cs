using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using _72220577_FEUTS.Model;

namespace _72220577_FEUTS.Services
{
    public class ccService
    {
        private readonly HttpClient _httpClient;

        public ccService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<course>> GetCoursesAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<course>>("https://actualbackendapp.azurewebsites.net/api/Courses");
        }

        public async Task<course> GetCourseByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<course>($"https://actualbackendapp.azurewebsites.net/api/Courses/{id}");
        }

        public async Task AddCourseAsync(course course)
        {
            await _httpClient.PostAsJsonAsync("https://actualbackendapp.azurewebsites.net/api/Courses", course);
        }

        public async Task UpdateCourseAsync(course course)
        {
            await _httpClient.PutAsJsonAsync($"https://actualbackendapp.azurewebsites.net/api/Courses/{course.courseId}", course);
        }

        public async Task DeleteCourseAsync(int id)
        {
            await _httpClient.DeleteAsync($"https://actualbackendapp.azurewebsites.net/api/Courses/{id}");
        }

        public async Task<List<category>> GetCategoriesAsync()
        {
            var response = await _httpClient.GetAsync("https://actualbackendapp.azurewebsites.net/api/v1/Categories"); // Replace with your actual API endpoint
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<List<category>>();
        }

        public async Task<category> GetCategoryByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<category>($"https://actualbackendapp.azurewebsites.net/api/v1/Categories/{id}");
        }

        public async Task AddCategoryAsync(category category)
        {
            await _httpClient.PostAsJsonAsync("https://actualbackendapp.azurewebsites.net/api/v1/Categories", category);
        }

        public async Task UpdateCategoryAsync(category category)
        {
            await _httpClient.PutAsJsonAsync($"https://actualbackendapp.azurewebsites.net/api/v1/Categories/{category.categoryId}", category);
        }

        public async Task DeleteCategoryAsync(int id)
        {
            await _httpClient.DeleteAsync($"https://actualbackendapp.azurewebsites.net/api/v1/Categories/{id}");
        }


    }
}