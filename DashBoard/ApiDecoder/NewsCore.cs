using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using DashBoard.Models.Dto;
using DashBoard.Models.Regular;

namespace DashBoard.ApiDecoder
{
    public class NewsCore : ApiController
    {
        private HttpClient _httpClient;

        public NewsCore(string jwtToken)
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("api/NewsCore"));
            _httpClient.BaseAddress = new Uri("http://localhost:13253/");
            _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + jwtToken);
        }
        public async Task<TblNews> AddNews(TblNews news)
        {
            HttpResponseMessage httpResponseMessage = await _httpClient.PostAsJsonAsync("api/NewsCore/AddNews", news);
            TblNews ans = await httpResponseMessage.Content.ReadAsAsync<TblNews>();
            return ans;
        }
        public async Task<TblNews> DeleteNews(int id)
        {
            HttpResponseMessage httpResponseMessage = await _httpClient.PostAsJsonAsync($"api/DeleteNews/DeleteNews?id={id}", id);
            TblNews ans = await httpResponseMessage.Content.ReadAsAsync<TblNews>();
            return ans;
        }
        public async Task<bool> UpdateNews(TblNews news, int logId)
        {
            List<object> newsAndLogId = new List<object>();
            newsAndLogId.Add(news);
            newsAndLogId.Add(logId);
            HttpResponseMessage httpResponseMessage = await _httpClient.PostAsJsonAsync("api/NewsCore/UpdateNews", newsAndLogId);
            bool ans = await httpResponseMessage.Content.ReadAsAsync<bool>();
            return ans;
        }
        public async Task<List<DtoTblNews>> SelectAllNewss()
        {
            HttpResponseMessage httpResponseMessage = await _httpClient.GetAsync("api/NewsCore/SelectAllNewss");
            List<DtoTblNews> ans = await httpResponseMessage.Content.ReadAsAsync<List<DtoTblNews>>();
            return ans;
        }
        public async Task<bool> SelectNewsById(int id)
        {
            HttpResponseMessage httpResponseMessage = await _httpClient.PostAsJsonAsync($"api/NewsCore/SelectNewsById?id={id}", id);
            bool ans = await httpResponseMessage.Content.ReadAsAsync<bool>();
            return ans;
        }
        public async Task<DtoTblNews> SelectNewsByTitle(string title)
        {
            HttpResponseMessage httpResponseMessage = await _httpClient.PostAsJsonAsync($"api/NewsCore/SelectNewsByTitle?title={title}", title);
            DtoTblNews ans = await httpResponseMessage.Content.ReadAsAsync<DtoTblNews>();
            return ans;
        }
        public async Task<List<DtoTblImage>> SelectImageByNewsId(int newsId)
        {
            HttpResponseMessage httpResponseMessage = await _httpClient.PostAsJsonAsync($"api/ImageCore/SelectImageByNewsId?newsId={newsId}", newsId);
            List<DtoTblImage> ans = await httpResponseMessage.Content.ReadAsAsync<List<DtoTblImage>>();
            return ans;
        }

    }
}