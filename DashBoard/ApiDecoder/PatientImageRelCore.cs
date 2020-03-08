using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using DashBoard.Models.Dto;
using DashBoard.Models.Regular;

namespace DashBoard.ApiDecoder
{
    public class PatientImageRelCore : ApiController
    {
        private HttpClient _httpClient;

        public PatientImageRelCore(string jwtToken)
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("api/PatientImageRelCore"));
            _httpClient.BaseAddress = new Uri("http://localhost:13253/");
            _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + jwtToken);
        }
        public async Task<TblPatientImageRel> AddPatientImageRel(TblPatientImageRel patientImageRel)
        {
            HttpResponseMessage httpResponseMessage = await _httpClient.PostAsJsonAsync("api/PatientImageRelCore/AddPatientImageRel", patientImageRel);
            TblPatientImageRel ans = await httpResponseMessage.Content.ReadAsAsync<TblPatientImageRel>();
            return ans;
        }
        public async Task<bool> DeletePatientImageRel(int id)
        {
            HttpResponseMessage httpResponseMessage = await _httpClient.PostAsJsonAsync($"api/DeletePatientImageRel/DeletePatientImageRel?id={id}", id);
            bool ans = await httpResponseMessage.Content.ReadAsAsync<bool>();
            return ans;
        }
        public async Task<bool> UpdatePatientImageRel(TblPatientImageRel patientImageRel, int logId)
        {
            List<object> patientImageRelAndLogId = new List<object>();
            patientImageRelAndLogId.Add(patientImageRel);
            patientImageRelAndLogId.Add(logId);
            HttpResponseMessage httpResponseMessage = await _httpClient.PostAsJsonAsync("api/PatientImageRelCore/UpdatePatientImageRel", patientImageRelAndLogId);
            bool ans = await httpResponseMessage.Content.ReadAsAsync<bool>();
            return ans;
        }
        public async Task<List<DtoTblPatientImageRel>> SelectAllPatientImageRels()
        {
            HttpResponseMessage httpResponseMessage = await _httpClient.GetAsync("api/PatientImageRelCore/SelectAllPatientImageRels");
            List<DtoTblPatientImageRel> ans = await httpResponseMessage.Content.ReadAsAsync<List<DtoTblPatientImageRel>>();
            return ans;
        }
        public async Task<DtoTblPatientImageRel> SelectPatientImageRelById(int id)
        {
            HttpResponseMessage httpResponseMessage = await _httpClient.PostAsJsonAsync($"api/PatientImageRelCore/SelectPatientImageRelById?id={id}", id);
            DtoTblPatientImageRel ans = await httpResponseMessage.Content.ReadAsAsync<DtoTblPatientImageRel>();
            return ans;
        }
        public async Task<List<TblPatientImageRel>> SelectPatientImageRelByPatientId(int patientId)
        {
            HttpResponseMessage httpResponseMessage = await _httpClient.PostAsJsonAsync($"api/PatientImageRelCore/SelectPatientImageRelsByPatientId?patientId={patientId}", patientId);
            List<TblPatientImageRel> ans = await httpResponseMessage.Content.ReadAsAsync<List<TblPatientImageRel>>();
            return ans;
        }
        public async Task<List<TblPatientImageRel>> SelectPatientImageRelByImageId(int imageId)
        {
            HttpResponseMessage httpResponseMessage = await _httpClient.PostAsJsonAsync($"api/PatientImageRelCore/SelectPatientImageRelsByImageId?imageId={imageId}", imageId);
            List<TblPatientImageRel> ans = await httpResponseMessage.Content.ReadAsAsync<List<TblPatientImageRel>>();
            return ans;
        }

    }
}