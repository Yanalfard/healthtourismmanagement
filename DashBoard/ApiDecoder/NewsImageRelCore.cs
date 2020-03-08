using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using DashBoard.Models.Dto;
using DashBoard.Models.Regular;

namespace DashBoard.ApiDecoder
{
    public class NewsImageRelCore : ApiController
    {
        private HttpClient _httpClient;

        public NewsImageRelCore(string jwtToken)
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("api/NewsImageRelCore"));
            _httpClient.BaseAddress = new Uri("http://localhost:13253/");
            _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + jwtToken);
        }
        public async Task<TblNewsImageRel> AddNewsImageRel(TblNewsImageRel newsImageRel)
        {
            HttpResponseMessage httpResponseMessage = await _httpClient.PostAsJsonAsync("api/NewsImageRelCore/AddNewsImageRel", newsImageRel);
            TblNewsImageRel ans = await httpResponseMessage.Content.ReadAsAsync<TblNewsImageRel>();
            return ans;
        }
        public async Task<bool> DeleteNewsImageRel(int id)
        {
            HttpResponseMessage httpResponseMessage = await _httpClient.PostAsJsonAsync($"api/DeleteNewsImageRel/DeleteNewsImageRel?id={id}", id);
            bool ans = await httpResponseMessage.Content.ReadAsAsync<bool>();
            return ans;
        }
        public async Task<bool> UpdateNewsImageRel(TblNewsImageRel newsImageRel, int logId)
        {
            List<object> newsImageRelAndLogId = new List<object>();
            newsImageRelAndLogId.Add(newsImageRel);
            newsImageRelAndLogId.Add(logId);
            HttpResponseMessage httpResponseMessage = await _httpClient.PostAsJsonAsync("api/NewsImageRelCore/UpdateNewsImageRel", newsImageRelAndLogId);
            bool ans = await httpResponseMessage.Content.ReadAsAsync<bool>();
            return ans;
        }
        public async Task<List<DtoTblNewsImageRel>> SelectAllNewsImageRels()
        {
            HttpResponseMessage httpResponseMessage = await _httpClient.GetAsync("api/NewsImageRelCore/SelectAllNewsImageRels");
            List<DtoTblNewsImageRel> ans = await httpResponseMessage.Content.ReadAsAsync<List<DtoTblNewsImageRel>>();
            return ans;
        }
        public async Task<DtoTblNewsImageRel> SelectNewsImageRelById(int id)
        {
            HttpResponseMessage httpResponseMessage = await _httpClient.PostAsJsonAsync($"api/NewsImageRelCore/SelectNewsImageRelById?id={id}", id);
            DtoTblNewsImageRel ans = await httpResponseMessage.Content.ReadAsAsync<DtoTblNewsImageRel>();
            return ans;
        }
        public async Task<List<TblNewsImageRel>> SelectNewsImageRelByNewsId(int newsId)
        {
            HttpResponseMessage httpResponseMessage = await _httpClient.PostAsJsonAsync($"api/NewsImageRelCore/SelectNewsImageRelsByNewsId?newsId={newsId}", newsId);
            List<TblNewsImageRel> ans = await httpResponseMessage.Content.ReadAsAsync<List<TblNewsImageRel>>();
            return ans;
        }
        public async Task<List<TblNewsImageRel>> SelectNewsImageRelByImageId(int imageId)
        {
            HttpResponseMessage httpResponseMessage = await _httpClient.PostAsJsonAsync($"api/NewsImageRelCore/SelectNewsImageRelsByImageId?imageId={imageId}", imageId);
            List<TblNewsImageRel> ans = await httpResponseMessage.Content.ReadAsAsync<List<TblNewsImageRel>>();
            return ans;
        }

    }
}