using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using DashBoard.Models.Dto;
using DashBoard.Models.Regular;

namespace DashBoard.ApiDecoder
{
    public class DoctorCore : ApiController
    {
        private HttpClient _httpClient;

        public DoctorCore(string jwtToken)
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("api/DoctorCore"));
            _httpClient.BaseAddress = new Uri("http://localhost:13253/");
            _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + jwtToken);
        }
        public async Task<TblDoctor> AddDoctor(TblDoctor doctor)
        {
            HttpResponseMessage httpResponseMessage = await _httpClient.PostAsJsonAsync("api/DoctorCore/AddDoctor", doctor);
            TblDoctor ans = await httpResponseMessage.Content.ReadAsAsync<TblDoctor>();
            return ans;
        }
        public async Task<bool> DeleteDoctor(int id)
        {
            HttpResponseMessage httpResponseMessage = await _httpClient.PostAsJsonAsync($"api/DeleteDoctor/DeleteDoctor?id={id}", id);
            bool ans = await httpResponseMessage.Content.ReadAsAsync<bool>();
            return ans;
        }
        public async Task<bool> UpdateDoctor(TblDoctor doctor, int logId)
        {
            List<object> doctorAndLogId = new List<object>();
            doctorAndLogId.Add(doctor);
            doctorAndLogId.Add(logId);
            HttpResponseMessage httpResponseMessage = await _httpClient.PostAsJsonAsync("api/DoctorCore/UpdateDoctor", doctorAndLogId);
            bool ans = await httpResponseMessage.Content.ReadAsAsync<bool>();
            return ans;
        }
        public async Task<List<DtoTblDoctor>> SelectAllDoctors()
        {
            HttpResponseMessage httpResponseMessage = await _httpClient.GetAsync("api/DoctorCore/SelectAllDoctors");
            List<DtoTblDoctor> ans = await httpResponseMessage.Content.ReadAsAsync<List<DtoTblDoctor>>();
            return ans;
        }
        public async Task<DtoTblDoctor> SelectDoctorById(int id)
        {
            HttpResponseMessage httpResponseMessage = await _httpClient.PostAsJsonAsync($"api/DoctorCore/SelectDoctorById?id={id}", id);
            DtoTblDoctor ans = await httpResponseMessage.Content.ReadAsAsync<DtoTblDoctor>();
            return ans;
        }
        public async Task<DtoTblDoctor> SelectDoctorByName(string name)
        {
            HttpResponseMessage httpResponseMessage = await _httpClient.PostAsJsonAsync($"api/DoctorCore/SelectDoctorByName?name={name}", name);
            DtoTblDoctor ans = await httpResponseMessage.Content.ReadAsAsync<DtoTblDoctor>();
            return ans;
        }
        public async Task<List<DtoTblDoctor>> SelectDoctorBySectionId(int sectionId)
        {
            HttpResponseMessage httpResponseMessage = await _httpClient.PostAsJsonAsync($"api/DoctorCore/SelectDoctorBySectionId?sectionId={sectionId}", sectionId);
            List<DtoTblDoctor> ans = await httpResponseMessage.Content.ReadAsAsync<List<DtoTblDoctor>>();
            return ans;
        }
        public async Task<List<DtoTblDoctor>> SelectDoctorByNowActive(bool nowActive)
        {
            HttpResponseMessage httpResponseMessage = await _httpClient.PostAsJsonAsync($"api/DoctorCore/SelectDoctorByNowActive?nowActive={nowActive}", nowActive);
            List<DtoTblDoctor> ans = await httpResponseMessage.Content.ReadAsAsync<List<DtoTblDoctor>>();
            return ans;
        }

    }
}