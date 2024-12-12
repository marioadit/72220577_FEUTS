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
        private readonly string baseUrl = "https://actbackendseervices.azurewebsites.net";
        private readonly string CoursesBaseUrl;
        private readonly string CategoriesBaseUrl;
        private readonly string UsersBaseUrl;
        private readonly string RerolsBaseUrl;
        private readonly string LoginBaseUrl;
        public string token;

        public ccService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            CoursesBaseUrl = $"{baseUrl}/api/courses";
            CategoriesBaseUrl = $"{baseUrl}/api/categories";
            UsersBaseUrl = $"{baseUrl}/api/users";
            RerolsBaseUrl = $"{baseUrl}/api/registeruserrole";
            LoginBaseUrl = $"{baseUrl}/api/login";
        }

        // Generic HTTP methods for reuse
        private async Task<T> GetAsync<T>(string url)
        {
            EnsureBearerToken();
            return await _httpClient.GetFromJsonAsync<T>(url);
        }

        private async Task PostAsync<T>(string url, T data)
        {
            EnsureBearerToken();
            await _httpClient.PostAsJsonAsync(url, data);
        }

        private async Task PutAsync<T>(string url, T data)
        {
            EnsureBearerToken();
            await _httpClient.PutAsJsonAsync(url, data);
        }

        private async Task DeleteAsync(string url)
        {
            EnsureBearerToken();
            await _httpClient.DeleteAsync(url);
        }

        // Ensures the Bearer Token is set for every request to Categories endpoints
        private void EnsureBearerToken()
        {
            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }
        }

        // Course-related methods
        public async Task<List<course>> GetCoursesAsync() =>
            await GetAsync<List<course>>(CoursesBaseUrl);

        public async Task<course> GetCourseByIdAsync(int id) =>
            await GetAsync<course>($"{CoursesBaseUrl}/{id}");

        public async Task AddCourseAsync(course course) =>
            await PostAsync(CoursesBaseUrl, course);

        public async Task UpdateCourseAsync(course course) =>
            await PutAsync($"{CoursesBaseUrl}/{course.courseId}", course);

        public async Task DeleteCourseAsync(int id) =>
            await DeleteAsync($"{CoursesBaseUrl}/{id}");

        public async Task<List<course>> GetCoursesByNameAsync(string courseName) =>
            await GetAsync<List<course>>($"{CoursesBaseUrl}/search/{courseName}");

        // Category-related methods
        public async Task<List<category>> GetCategoriesAsync() =>
            await GetAsync<List<category>>(CategoriesBaseUrl);

        public async Task<category> GetCategoryByIdAsync(int id) =>
            await GetAsync<category>($"{CategoriesBaseUrl}/{id}");

        public async Task AddCategoryAsync(category category) =>
            await PostAsync(CategoriesBaseUrl, category);

        public async Task UpdateCategoryAsync(category category) =>
            await PutAsync($"{CategoriesBaseUrl}/{category.categoryId}", category);

        public async Task DeleteCategoryAsync(int id) =>
            await DeleteAsync($"{CategoriesBaseUrl}/{id}");

        // User-related methods
        public async Task AddUserAsync(user user) =>
            await PostAsync(UsersBaseUrl, user);

        // Register user role-related methods
        public async Task AddRegisterUserRoleAsync(registeruserrole registeruserrole) =>
            await PostAsync(RerolsBaseUrl, registeruserrole);

        // Login-related methods
        public async Task<string> LoginAsync(login login)
        {
            var response = await _httpClient.PostAsJsonAsync(LoginBaseUrl, login);

            if (response.IsSuccessStatusCode)
            {
                var loginResponse = await response.Content.ReadFromJsonAsync<loginresponse>();

                // Validate if the token exists in the response
                if (loginResponse?.token != null)
                {
                    this.token = loginResponse.token;

                    // Add the token to the HttpClient headers for authenticated requests
                    _httpClient.DefaultRequestHeaders.Authorization =
                        new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", this.token);

                    return token; // Return the token for further use if needed
                }
                else
                {
                    throw new Exception("Login successful, but token is missing in the response.");
                }
            }
            else
            {
                // Handle error response and throw detailed exception
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"Login failed: {response.ReasonPhrase}. Details: {errorContent}");
            }
        }
    }
}
