using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using DashBoard.Models.Dto;
using DashBoard.Models.Regular;

namespace DashBoard.ApiDecoder
{
    public class HospitalSectionRelCore : ApiController
    {
        private HttpClient _httpClient;

        public HospitalSectionRelCore(string jwtToken)
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("api/HospitalSectionRelCore"));
            _httpClient.BaseAddress = new Uri("http://localhost:13253/");
            _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + jwtToken);
        }
        public async Task<TblHospitalSectionRel> AddHospitalSectionRel(TblHospitalSectionRel hospitalSectionRel)
        {
            HttpResponseMessage httpResponseMessage = await _httpClient.PostAsJsonAsync("api/HospitalSectionRelCore/AddHospitalSectionRel", hospitalSectionRel);
            TblHospitalSectionRel ans = await httpResponseMessage.Content.ReadAsAsync<TblHospitalSectionRel>();
            return ans;
        }
        public async Task<bool> DeleteHospitalSectionRel(int id)
        {
            HttpResponseMessage httpResponseMessage = await _httpClient.PostAsJsonAsync($"api/DeleteHospitalSectionRel/DeleteHospitalSectionRel?id={id}", id);
            bool ans = await httpResponseMessage.Content.ReadAsAsync<bool>();
            return ans;
        }
        public async Task<bool> UpdateHospitalSectionRel(TblHospitalSectionRel hospitalSectionRel, int logId)
        {
            List<object> hospitalSectionRelAndLogId = new List<object>();
            hospitalSectionRelAndLogId.Add(hospitalSectionRel);
            hospitalSectionRelAndLogId.Add(logId);
            HttpResponseMessage httpResponseMessage = await _httpClient.PostAsJsonAsync("api/HospitalSectionRelCore/UpdateHospitalSectionRel", hospitalSectionRelAndLogId);
            bool ans = await httpResponseMessage.Content.ReadAsAsync<bool>();
            return ans;
        }
        public async Task<List<DtoTblHospitalSectionRel>> SelectAllHospitalSectionRels()
        {
            HttpResponseMessage httpResponseMessage = await _httpClient.GetAsync("api/HospitalSectionRelCore/SelectAllHospitalSectionRels");
            List<DtoTblHospitalSectionRel> ans = await httpResponseMessage.Content.ReadAsAsync<List<DtoTblHospitalSectionRel>>();
            return ans;
        }
        public async Task<bool> SelectHospitalSectionRelById(int id)
        {
            HttpResponseMessage httpResponseMessage = await _httpClient.PostAsJsonAsync($"api/HospitalSectionRelCore/SelectHospitalSectionRelById?id={id}", id);
            bool ans = await httpResponseMessage.Content.ReadAsAsync<bool>();
            return ans;
        }
        public async Task<List<TblHospitalSectionRel>> SelectHospitalSectionRelByHospitalId(int hospitalId)
        {
            HttpResponseMessage httpResponseMessage = await _httpClient.PostAsJsonAsync($"api/HospitalSectionRelCore/SelectHospitalSectionRelsByHospitalId?hospitalId={hospitalId}", hospitalId);
            List<TblHospitalSectionRel> ans = await httpResponseMessage.Content.ReadAsAsync<List<TblHospitalSectionRel>>();
            return ans;
        }
        public async Task<List<TblHospitalSectionRel>> SelectHospitalSectionRelBySectionId(int sectionId)
        {
            HttpResponseMessage httpResponseMessage = await _httpClient.PostAsJsonAsync($"api/HospitalSectionRelCore/SelectHospitalSectionRelsBySectionId?sectionId={sectionId}", sectionId);
            List<TblHospitalSectionRel> ans = await httpResponseMessage.Content.ReadAsAsync<List<TblHospitalSectionRel>>();
            return ans;
        }
        public async Task<List<TblHospitalSectionRel>> SelectHospitalSectionRelByDoctorId(int doctorId)
        {
            HttpResponseMessage httpResponseMessage = await _httpClient.PostAsJsonAsync($"api/HospitalSectionRelCore/SelectHospitalSectionRelsByDoctorId?doctorId={doctorId}", doctorId);
            List<TblHospitalSectionRel> ans = await httpResponseMessage.Content.ReadAsAsync<List<TblHospitalSectionRel>>();
            return ans;
        }

    }
}