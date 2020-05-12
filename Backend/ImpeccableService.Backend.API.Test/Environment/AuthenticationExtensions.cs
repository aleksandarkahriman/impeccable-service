using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using ImpeccableService.Backend.API.UserManagement.Dto;
using ImpeccableService.Backend.Domain.UserManagement;
using Newtonsoft.Json;

namespace ImpeccableService.Backend.API.Test.Environment
{
    public static class AuthenticationExtensions
    {
        public static async Task<HttpClient> Authenticate(this HttpClient client, User user)
        {
            var emailLoginDto = new EmailLoginDto(user.Email, "12345678");
            var requestBody = JsonConvert.SerializeObject(emailLoginDto);
            var requestContent = new StringContent(requestBody, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("/api/authentication/login", requestContent);
            var responseBody = await response.Content.ReadAsStringAsync();
            var authenticationCredentials =
                JsonConvert.DeserializeObject<AuthenticationCredentialsDto>(responseBody);

            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", authenticationCredentials.AccessToken);

            return client;
        }
    }
}
