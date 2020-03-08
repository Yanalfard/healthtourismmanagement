using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace DashBoard.ApiDecoder
{
    public class ImageUploadCore
    {
        private HttpClient _httpClient;

        public ImageUploadCore(string jwtToken)
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("api/HospitalImageRelCore"));
            _httpClient.BaseAddress = new Uri("http://localhost:13253/");
            _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + jwtToken);
        }

        public async Task<bool> Upload(string base64String, string fileName)
        {
            List<string> obj = new List<string>();
            obj.Add(base64String);
            obj.Add(fileName);
            HttpResponseMessage httpResponseMessage = await _httpClient.PostAsJsonAsync("api/ImageUpload/Upload", obj);
            bool ans = await httpResponseMessage.Content.ReadAsAsync<bool>();
            return ans;
        }
    }
}