using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace HelthTourismServer.ApiDecoder
{
    class SecurityCore : ApiController
    {
        private HttpClient httpClient;

        public SecurityCore()
        {
            httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("api/SecurityCore"));
            httpClient.BaseAddress = new Uri("http://localhost:13253/");

        }

        public async Task<string> GenerateToken(string username, string password)
        {
            List<object> usernameAndPassword = new List<object>();
            usernameAndPassword.Add(username);
            usernameAndPassword.Add(password);
            var byteArray = Encoding.ASCII.GetBytes($"{username}:{password}");
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
            HttpResponseMessage httpResponseMessage = await httpClient.PostAsJsonAsync("api/GenerateJwtToken", usernameAndPassword);
            string ans = await httpResponseMessage.Content.ReadAsAsync<string>();
            return ans;
        }
    }
}
