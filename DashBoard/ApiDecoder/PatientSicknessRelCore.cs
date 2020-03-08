using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using DashBoard.Models.Dto;
using DashBoard.Models.Regular;

namespace DashBoard.ApiDecoder
{
    public class PatientSicknessRelCore : ApiController
    {
        private HttpClient _httpClient;

        public PatientSicknessRelCore(string jwtToken)
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("api/PatientSicknessRelCore"));
            _httpClient.BaseAddress = new Uri("http://localhost:13253/");
            _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + jwtToken);
        }
        public async Task<TblPatientSicknessRel> AddPatientSicknessRel(TblPatientSicknessRel patientSicknessRel)
        {
            HttpResponseMessage httpResponseMessage = await _httpClient.PostAsJsonAsync("api/PatientSicknessRelCore/AddPatientSicknessRel", patientSicknessRel);
            TblPatientSicknessRel ans = await httpResponseMessage.Content.ReadAsAsync<TblPatientSicknessRel>();
            return ans;
        }

        {
            HttpResponseMessage httpResponseMessage = await _httpClient.PostAsJsonAsync($"api/DeletePatientSicknessRel/DeletePatientSicknessRel?id={id}", id);
            bool ans = await httpResponseMessage.Content.ReadAsAsync<bool>();
            return ans;
        }

        {
            List<object> patientSicknessRelAndLogId = new List<object>();
            patientSicknessRelAndLogId.Add(patientSicknessRel);
            patientSicknessRelAndLogId.Add(logId);
            HttpResponseMessage httpResponseMessage = await _httpClient.PostAsJsonAsync("api/PatientSicknessRelCore/UpdatePatientSicknessRel", patientSicknessRelAndLogId);
            bool ans = await httpResponseMessage.Content.ReadAsAsync<bool>();
            return ans;
        }

        {
            HttpResponseMessage httpResponseMessage = await _httpClient.GetAsync("api/PatientSicknessRelCore/SelectAllPatientSicknessRels");
            List<DtoTblPatientSicknessRel> ans = await httpResponseMessage.Content.ReadAsAsync<List<DtoTblPatientSicknessRel>>();
            return ans;
        }

        {
            HttpResponseMessage httpResponseMessage = await _httpClient.PostAsJsonAsync($"api/PatientSicknessRelCore/SelectPatientSicknessRelById?id={id}", id);
            TblPatientSicknessRel ans = await httpResponseMessage.Content.ReadAsAsync<TblPatientSicknessRel>();
            return ans;
        }

        {
            HttpResponseMessage httpResponseMessage = await _httpClient.PostAsJsonAsync($"api/PatientSicknessRelCore/SelectPatientSicknessRelsByPatientId?patientId={patientId}", patientId);
            List<TblPatientSicknessRel> ans = await httpResponseMessage.Content.ReadAsAsync<List<TblPatientSicknessRel>>();
            return ans;
        }

        {
            HttpResponseMessage httpResponseMessage = await _httpClient.PostAsJsonAsync($"api/PatientSicknessRelCore/SelectPatientSicknessRelsBySicknessId?sicknessId={sicknessId}", sicknessId);
            List<TblPatientSicknessRel> ans = await httpResponseMessage.Content.ReadAsAsync<List<TblPatientSicknessRel>>();
            return ans;
        }

        {
            HttpResponseMessage httpResponseMessage = await _httpClient.PostAsJsonAsync($"api/PatientSicknessRelCore/SelectPatientSicknessRelsByDoctorId?doctorId={doctorId}", doctorId);
            List<TblPatientSicknessRel> ans = await httpResponseMessage.Content.ReadAsAsync<List<TblPatientSicknessRel>>();
            return ans;
        }

        {
            HttpResponseMessage httpResponseMessage = await _httpClient.PostAsJsonAsync($"api/PatientSicknessRelCore/SelectPatientSicknessRelsByHospitalId?hospitalId={hospitalId}", hospitalId);
            List<TblPatientSicknessRel> ans = await httpResponseMessage.Content.ReadAsAsync<List<TblPatientSicknessRel>>();
            return ans;
        }

        {
            HttpResponseMessage httpResponseMessage = await _httpClient.PostAsJsonAsync($"api/PatientSicknessRelCore/SelectPatientSicknessRelsByBeforeCureDesc?beforeCureDesc={beforeCureDesc}", beforeCureDesc);
            List<TblPatientSicknessRel> ans = await httpResponseMessage.Content.ReadAsAsync<List<TblPatientSicknessRel>>();
            return ans;
        }

        {
            HttpResponseMessage httpResponseMessage = await _httpClient.PostAsJsonAsync($"api/PatientSicknessRelCore/SelectPatientSicknessRelsByAfterCureDesc?afterCureDesc={afterCureDesc}", afterCureDesc);
            List<TblPatientSicknessRel> ans = await httpResponseMessage.Content.ReadAsAsync<List<TblPatientSicknessRel>>();
            return ans;
        }

    }
}