using ConsumeAPI.Models;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Net.Http;

namespace ConsumeAPI
{
    public class CallAPI
    {
        private static HttpClient _httpClient = new HttpClient();
        public static async Task<string> GetToken()
        {
            if (_httpClient.BaseAddress == null)
            {
                _httpClient.BaseAddress = new Uri("https://localhost:44318/api/");
            }

            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            User user = new User() { email = "jwt@gmail.co", password = "123456" };
            string token = "";
            try
            {
                //Get the JWT  
                token = await AuthenticateAsync(user);
                Debug.WriteLine("Token = " + token);

                ////Call a secured endpoint  
                //var message = await Secured(token);
                //Console.WriteLine($"Response: {message}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Console.Read();
            }
            return token;
        }
        private static async Task<string> AuthenticateAsync(User user)
        {
            var response = await _httpClient.PostAsJsonAsync(@"Authentication/Login", user);
            Debug.WriteLine("response = ", response);
            TokenResponse token = await response.Content.ReadFromJsonAsync<TokenResponse>();

            return token.Token;
        }

    }
    public class TokenResponse
    {
        public string Token { get; set; } = string.Empty;
    }
}
