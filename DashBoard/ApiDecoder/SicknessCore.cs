using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using DashBoard.Models.Dto;
using DashBoard.Models.Regular;

namespace DashBoard.ApiDecoder
{
    public class SicknessCore : ApiController
    {
        private HttpClient _httpClient;

        public SicknessCore(string jwtToken)
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("api/SicknessCore"));
            _httpClient.BaseAddress = new Uri("http://localhost:13253/");
            _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + jwtToken);
        }
        public async Task<TblSickness> AddSickness(TblSickness sickness)
        {
            HttpResponseMessage httpResponseMessage = await _httpClient.PostAsJsonAsync("api/SicknessCore/AddSickness", sickness);
            TblSickness ans = await httpResponseMessage.Content.ReadAsAsync<TblSickness>();
            return ans;
        }
        public async Task<bool> DeleteSickness(int id)
        {
            HttpResponseMessage httpResponseMessage = await _httpClient.PostAsJsonAsync($"api/DeleteSickness/DeleteSickness?id={id}", id);
            bool ans = await httpResponseMessage.Content.ReadAsAsync<bool>();
            return ans;
        }
        public async Task<bool> UpdateSickness(TblSickness sickness, int logId)
        {
            List<object> sicknessAndLogId = new List<object>();
            sicknessAndLogId.Add(sickness);
            sicknessAndLogId.Add(logId);
            HttpResponseMessage httpResponseMessage = await _httpClient.PostAsJsonAsync("api/SicknessCore/UpdateSickness", sicknessAndLogId);
            bool ans = await httpResponseMessage.Content.ReadAsAsync<bool>();
            return ans;
        }
        public async Task<List<DtoTblSickness>> SelectAllSicknesss()
        {
            HttpResponseMessage httpResponseMessage = await _httpClient.GetAsync("api/SicknessCore/SelectAllSicknesss");
            List<DtoTblSickness> ans = await httpResponseMessage.Content.ReadAsAsync<List<DtoTblSickness>>();
            return ans;
        }
        public async Task<TblSickness> SelectSicknessById(int id)
        {
            HttpResponseMessage httpResponseMessage = await _httpClient.PostAsJsonAsync($"api/SicknessCore/SelectSicknessById?id={id}", id);
            TblSickness ans = await httpResponseMessage.Content.ReadAsAsync<TblSickness>();
            return ans;
        }
        public async Task<DtoTblSickness> SelectSicknessByName(string name)
        {
            HttpResponseMessage httpResponseMessage = await _httpClient.PostAsJsonAsync($"api/SicknessCore/SelectSicknessByName?name={name}", name);
            DtoTblSickness ans = await httpResponseMessage.Content.ReadAsAsync<DtoTblSickness>();
            return ans;
        }
        public async Task<List<DtoTblSickness>> SelectSicknessBySectionId(int sectionId)
        {
            HttpResponseMessage httpResponseMessage = await _httpClient.PostAsJsonAsync($"api/SicknessCore/SelectSicknessBySectionId?sectionId={sectionId}", sectionId);
            List<DtoTblSickness> ans = await httpResponseMessage.Content.ReadAsAsync<List<DtoTblSickness>>();
            return ans;
        }

    }
}