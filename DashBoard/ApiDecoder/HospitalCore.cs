using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using DashBoard.Models.Dto;
using DashBoard.Models.Regular;

namespace DashBoard.ApiDecoder
{
    public class HospitalCore : ApiController
    {
        private HttpClient _httpClient;

        public HospitalCore(string jwtToken)
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("api/HospitalCore"));
            _httpClient.BaseAddress = new Uri("http://localhost:13253/");
            _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + jwtToken);
        }
        public async Task<TblHospital> AddHospital(TblHospital hospital)
        {
            HttpResponseMessage httpResponseMessage = await _httpClient.PostAsJsonAsync("api/HospitalCore/AddHospital", hospital);
            TblHospital ans = await httpResponseMessage.Content.ReadAsAsync<TblHospital>();
            return ans;
        }
        public async Task<bool> DeleteHospital(int id)
        {
            HttpResponseMessage httpResponseMessage = await _httpClient.PostAsJsonAsync($"api/DeleteHospital/DeleteHospital?id={id}", id);
            bool ans = await httpResponseMessage.Content.ReadAsAsync<bool>();
            return ans;
        }
        public async Task<bool> UpdateHospital(TblHospital hospital, int logId)
        {
            List<object> hospitalAndLogId = new List<object>();
            hospitalAndLogId.Add(hospital);
            hospitalAndLogId.Add(logId);
            HttpResponseMessage httpResponseMessage = await _httpClient.PostAsJsonAsync("api/HospitalCore/UpdateHospital", hospitalAndLogId);
            bool ans = await httpResponseMessage.Content.ReadAsAsync<bool>();
            return ans;
        }
        public async Task<List<DtoTblHospital>> SelectAllHospitals()
        {
            HttpResponseMessage httpResponseMessage = await _httpClient.GetAsync("api/HospitalCore/SelectAllHospitals");
            List<DtoTblHospital> ans = await httpResponseMessage.Content.ReadAsAsync<List<DtoTblHospital>>();
            return ans;
        }
        public async Task<DtoTblHospital> SelectHospitalById(int id)
        {
            HttpResponseMessage httpResponseMessage = await _httpClient.PostAsJsonAsync($"api/HospitalCore/SelectHospitalById?id={id}", id);
            DtoTblHospital ans = await httpResponseMessage.Content.ReadAsAsync<DtoTblHospital>();
            return ans;
        }
        public async Task<DtoTblHospital> SelectHospitalByName(string name)
        {
            HttpResponseMessage httpResponseMessage = await _httpClient.PostAsJsonAsync($"api/HospitalCore/SelectHospitalByName?name={name}", name);
            DtoTblHospital ans = await httpResponseMessage.Content.ReadAsAsync<DtoTblHospital>();
            return ans;
        }
        public async Task<List<DtoTblHospital>> SelectHospitalByUserPassId(int userPassId)
        {
            HttpResponseMessage httpResponseMessage = await _httpClient.PostAsJsonAsync($"api/HospitalCore/SelectHospitalByUserPassId?userPassId={userPassId}", userPassId);
            List<DtoTblHospital> ans = await httpResponseMessage.Content.ReadAsAsync<List<DtoTblHospital>>();
            return ans;
        }
        public async Task<List<DtoTblImage>> SelectImageByHospitalId(int hospitalId)
        {
            HttpResponseMessage httpResponseMessage = await _httpClient.PostAsJsonAsync($"api/ImageCore/SelectImageByHospitalId?hospitalId={hospitalId}", hospitalId);
            List<DtoTblImage> ans = await httpResponseMessage.Content.ReadAsAsync<List<DtoTblImage>>();
            return ans;
        }
        public async Task<List<DtoTblSection>> SelectSectionByHospitalId(int hospitalId)
        {
            HttpResponseMessage httpResponseMessage = await _httpClient.PostAsJsonAsync($"api/SectionCore/SelectSectionByHospitalId?hospitalId={hospitalId}", hospitalId);
            List<DtoTblSection> ans = await httpResponseMessage.Content.ReadAsAsync<List<DtoTblSection>>();
            return ans;
        }

    }
}