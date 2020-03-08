using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using DashBoard.Models.Dto;
using DashBoard.Models.Regular;

namespace DashBoard.ApiDecoder
{
    public class ImageCore : ApiController
    {
        private HttpClient _httpClient;

        public ImageCore(string jwtToken)
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("api/ImageCore"));
            _httpClient.BaseAddress = new Uri("http://localhost:13253/");
            _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + jwtToken);
        }
        public async Task<TblImage> AddImage(TblImage image)
        {
            HttpResponseMessage httpResponseMessage = await _httpClient.PostAsJsonAsync("api/ImageCore/AddImage", image);
            TblImage ans = await httpResponseMessage.Content.ReadAsAsync<TblImage>();
            return ans;
        }
        public async Task<bool> DeleteImage(int id)
        {
            HttpResponseMessage httpResponseMessage = await _httpClient.PostAsJsonAsync($"api/DeleteImage/DeleteImage?id={id}", id);
            bool ans = await httpResponseMessage.Content.ReadAsAsync<bool>();
            return ans;
        }
        public async Task<bool> UpdateImage(TblImage image, int logId)
        {
            List<object> imageAndLogId = new List<object>();
            imageAndLogId.Add(image);
            imageAndLogId.Add(logId);
            HttpResponseMessage httpResponseMessage = await _httpClient.PostAsJsonAsync("api/ImageCore/UpdateImage", imageAndLogId);
            bool ans = await httpResponseMessage.Content.ReadAsAsync<bool>();
            return ans;
        }
        public async Task<List<DtoTblImage>> SelectAllImages()
        {
            HttpResponseMessage httpResponseMessage = await _httpClient.GetAsync("api/ImageCore/SelectAllImages");
            List<DtoTblImage> ans = await httpResponseMessage.Content.ReadAsAsync<List<DtoTblImage>>();
            return ans;
        }
        public async Task<DtoTblImage> SelectImageById(int id)
        {
            HttpResponseMessage httpResponseMessage = await _httpClient.PostAsJsonAsync($"api/ImageCore/SelectImageById?id={id}", id);
            DtoTblImage ans = await httpResponseMessage.Content.ReadAsAsync<DtoTblImage>();
            return ans;
        }
        public async Task<DtoTblImage> SelectImageByImage(string image)
        {
            HttpResponseMessage httpResponseMessage = await _httpClient.PostAsJsonAsync($"api/ImageCore/SelectImageByImage?image={image}", image);
            DtoTblImage ans = await httpResponseMessage.Content.ReadAsAsync<DtoTblImage>();
            return ans;
        }

    }
}