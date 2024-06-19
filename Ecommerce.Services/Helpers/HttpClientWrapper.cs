using System.Text;
using System.Text.Json;
using Ecommerce.DataAccessLayer.Models;
using Ecommerce.Services.Models;

namespace Ecommerce.Services.Helpers
{
    public class HttpClientWrapper
    {
        private readonly HttpClient _httpClient;
        //private readonly IAuthenticator _authenticator;

        public HttpClientWrapper(HttpClient httpClient)
        {
            _httpClient = httpClient;
            // _authenticator = authenticator;
            // _authenticator.SetBasicAuthHeader(_httpClient);
        }

        public async Task<ServiceResponse<T>> GetAsync<T>(string url)
        {
            var response = await _httpClient.GetAsync(url);
            return await ProcessResponse<T>(response);
        }

        public async Task<ServiceResponse<T>> PostAsync<T>(string url, object body)
        {
            var content = new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(url, content);
            return await ProcessResponse<T>(response);
        }

        public async Task<ServiceResponse<T>> DeleteAsync<T>(string url)
        {
            var response = await _httpClient.DeleteAsync(url);
            return await ProcessResponse<T>(response);
        }

        private static async Task<ServiceResponse<T>> ProcessResponse<T>(HttpResponseMessage response)
        {
            var result = new ServiceResponse<T>();

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                result.Data = JsonSerializer.Deserialize<T>(content);
            }
            else
            {
                result.Errors.Add(new ServiceError { ErrorCode = response.StatusCode.ToString(), ErrorMessage = response.ReasonPhrase });
            }

            return result;
        }
    }
}
