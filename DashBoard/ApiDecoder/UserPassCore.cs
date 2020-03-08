using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using DashBoard.Models.Dto;
using DashBoard.Models.Regular;

namespace DashBoard.ApiDecoder
{
    public class UserPassCore : ApiController
    {
        private HttpClient _httpClient;

        public UserPassCore(string jwtToken)
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("api/UserPassCore"));
            _httpClient.BaseAddress = new Uri("http://localhost:13253/");
            _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + jwtToken);
        }
        public async Task<TblUserPass> AddUserPass(TblUserPass userPass)
        {
            HttpResponseMessage httpResponseMessage = await _httpClient.PostAsJsonAsync("api/UserPassCore/AddUserPass", userPass);
            TblUserPass ans = await httpResponseMessage.Content.ReadAsAsync<TblUserPass>();
            return ans;
        }
        public async Task<bool> DeleteUserPass(int id)
        {
            HttpResponseMessage httpResponseMessage = await _httpClient.PostAsJsonAsync($"api/DeleteUserPass/DeleteUserPass?id={id}", id);
            bool ans = await httpResponseMessage.Content.ReadAsAsync<bool>();
            return ans;
        }
        public async Task<bool> UpdateUserPass(TblUserPass userPass, int logId)
        {
            List<object> userPassAndLogId = new List<object>();
            userPassAndLogId.Add(userPass);
            userPassAndLogId.Add(logId);
            HttpResponseMessage httpResponseMessage = await _httpClient.PostAsJsonAsync("api/UserPassCore/UpdateUserPass", userPassAndLogId);
            bool ans = await httpResponseMessage.Content.ReadAsAsync<bool>();
            return ans;
        }
        public async Task<List<DtoTblUserPass>> SelectAllUserPasss()
        {
            HttpResponseMessage httpResponseMessage = await _httpClient.GetAsync("api/UserPassCore/SelectAllUserPasss");
            List<DtoTblUserPass> ans = await httpResponseMessage.Content.ReadAsAsync<List<DtoTblUserPass>>();
            return ans;
        }
        public async Task<DtoTblUserPass> SelectUserPassById(int id)
        {
            HttpResponseMessage httpResponseMessage = await _httpClient.PostAsJsonAsync($"api/UserPassCore/SelectUserPassById?id={id}", id);
            DtoTblUserPass ans = await httpResponseMessage.Content.ReadAsAsync<DtoTblUserPass>();
            return ans;
        }
        public async Task<DtoTblUserPass> SelectUserPassByUsernameAndPassword(string username ,string password)
        {
            List<object> obj = new List<object>();
            obj.Add(username);
            obj.Add(password);
            HttpResponseMessage httpResponseMessage = await _httpClient.PostAsJsonAsync($"api/UserPassCore/SelectUserPassByUsernameAndPassword", obj);
            DtoTblUserPass ans = await httpResponseMessage.Content.ReadAsAsync<DtoTblUserPass>();
            return ans;
        }
        public async Task<DtoTblUserPass> SelectUserPassByUsername(string username)
        {
            HttpResponseMessage httpResponseMessage = await _httpClient.PostAsJsonAsync($"api/UserPassCore/SelectUserPassByUsername?username={username}", username);
            DtoTblUserPass ans = await httpResponseMessage.Content.ReadAsAsync<DtoTblUserPass>();
            return ans;
        }
        public async Task<DtoTblUserPass> SelectUserPassByPassword(string password)
        {
            HttpResponseMessage httpResponseMessage = await _httpClient.PostAsJsonAsync($"api/UserPassCore/SelectUserPassByPassword?password={password}", password);
            DtoTblUserPass ans = await httpResponseMessage.Content.ReadAsAsync<DtoTblUserPass>();
            return ans;
        }
        public async Task<List<DtoTblUserPass>> SelectUserPassByIsHelthApple(bool isHelthApple)
        {
            HttpResponseMessage httpResponseMessage = await _httpClient.PostAsJsonAsync($"api/UserPassCore/SelectUserPassByIsHelthApple?isHelthApple={isHelthApple}", isHelthApple);
            List<DtoTblUserPass> ans = await httpResponseMessage.Content.ReadAsAsync<List<DtoTblUserPass>>();
            return ans;
        }

    }
}