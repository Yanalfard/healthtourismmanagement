using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using DashBoard.Models.Dto;
using DashBoard.Models.Regular;

namespace DashBoard.ApiDecoder
{
    public class SectionCore : ApiController
    {
        private HttpClient _httpClient;

        public SectionCore(string jwtToken)
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("api/SectionCore"));
            _httpClient.BaseAddress = new Uri("http://localhost:13253/");
            _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + jwtToken);
        }
        public async Task<TblSection> AddSection(TblSection section)
        {
            HttpResponseMessage httpResponseMessage = await _httpClient.PostAsJsonAsync("api/SectionCore/AddSection", section);
            TblSection ans = await httpResponseMessage.Content.ReadAsAsync<TblSection>();
            return ans;
        }
        public async Task<bool> DeleteSection(int id)
        {
            HttpResponseMessage httpResponseMessage = await _httpClient.PostAsJsonAsync($"api/DeleteSection/DeleteSection?id={id}", id);
            bool ans = await httpResponseMessage.Content.ReadAsAsync<bool>();
            return ans;
        }
        public async Task<bool> UpdateSection(TblSection section, int logId)
        {
            List<object> sectionAndLogId = new List<object>();
            sectionAndLogId.Add(section);
            sectionAndLogId.Add(logId);
            HttpResponseMessage httpResponseMessage = await _httpClient.PostAsJsonAsync("api/SectionCore/UpdateSection", sectionAndLogId);
            bool ans = await httpResponseMessage.Content.ReadAsAsync<bool>();
            return ans;
        }
        public async Task<List<DtoTblSection>> SelectAllSections()
        {
            HttpResponseMessage httpResponseMessage = await _httpClient.GetAsync("api/SectionCore/SelectAllSections");
            List<DtoTblSection> ans = await httpResponseMessage.Content.ReadAsAsync<List<DtoTblSection>>();
            return ans;
        }
        public async Task<DtoTblSection> SelectSectionById(int id)
        {
            HttpResponseMessage httpResponseMessage = await _httpClient.PostAsJsonAsync($"api/SectionCore/SelectSectionById?id={id}", id);
            DtoTblSection ans = await httpResponseMessage.Content.ReadAsAsync<DtoTblSection>();
            return ans;
        }
        public async Task<DtoTblSection> SelectSectionBySectionName(string sectionName)
        {
            HttpResponseMessage httpResponseMessage = await _httpClient.PostAsJsonAsync($"api/SectionCore/SelectSectionBySectionName?sectionName={sectionName}", sectionName);
            DtoTblSection ans = await httpResponseMessage.Content.ReadAsAsync<DtoTblSection>();
            return ans;
        }
        public async Task<List<DtoTblOperation>> SelectOperationBySectionId(int sectionId)
        {
            HttpResponseMessage httpResponseMessage = await _httpClient.PostAsJsonAsync($"api/OperationCore/SelectOperationBySectionId?sectionId={sectionId}", sectionId);
            List<DtoTblOperation> ans = await httpResponseMessage.Content.ReadAsAsync<List<DtoTblOperation>>();
            return ans;
        }

    }
}