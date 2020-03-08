using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using DashBoard.Models.Dto;
using DashBoard.Models.Regular;

namespace DashBoard.ApiDecoder
{
    public class HospitalImageRelCore : ApiController
    {
        private HttpClient _httpClient;

        public HospitalImageRelCore(string jwtToken)
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("api/HospitalImageRelCore"));
            _httpClient.BaseAddress = new Uri("http://localhost:13253/");
            _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + jwtToken);
        }
        public async Task<TblHospitalImageRel> AddHospitalImageRel(TblHospitalImageRel hospitalImageRel)
        {
            HttpResponseMessage httpResponseMessage = await _httpClient.PostAsJsonAsync("api/HospitalImageRelCore/AddHospitalImageRel", hospitalImageRel);
            TblHospitalImageRel ans = await httpResponseMessage.Content.ReadAsAsync<TblHospitalImageRel>();
            return ans;
        }
        public async Task<TblHospitalImageRel> DeleteHospitalImageRel(int id)
        {
            HttpResponseMessage httpResponseMessage = await _httpClient.PostAsJsonAsync($"api/DeleteHospitalImageRel/DeleteHospitalImageRel?id={id}", id);
            TblHospitalImageRel ans = await httpResponseMessage.Content.ReadAsAsync<TblHospitalImageRel>();
            return ans;
        }
        public async Task<bool> UpdateHospitalImageRel(TblHospitalImageRel hospitalImageRel, int logId)
        {
            List<object> hospitalImageRelAndLogId = new List<object>();
            hospitalImageRelAndLogId.Add(hospitalImageRel);
            hospitalImageRelAndLogId.Add(logId);
            HttpResponseMessage httpResponseMessage = await _httpClient.PostAsJsonAsync("api/HospitalImageRelCore/UpdateHospitalImageRel", hospitalImageRelAndLogId);
            bool ans = await httpResponseMessage.Content.ReadAsAsync<bool>();
            return ans;
        }
        public async Task<List<DtoTblHospitalImageRel>> SelectAllHospitalImageRels()
        {
            HttpResponseMessage httpResponseMessage = await _httpClient.GetAsync("api/HospitalImageRelCore/SelectAllHospitalImageRels");
            List<DtoTblHospitalImageRel> ans = await httpResponseMessage.Content.ReadAsAsync<List<DtoTblHospitalImageRel>>();
            return ans;
        }
        public async Task<bool> SelectHospitalImageRelById(int id)
        {
            HttpResponseMessage httpResponseMessage = await _httpClient.PostAsJsonAsync($"api/HospitalImageRelCore/SelectHospitalImageRelById?id={id}", id);
            bool ans = await httpResponseMessage.Content.ReadAsAsync<bool>();
            return ans;
        }
        public async Task<List<TblHospitalImageRel>> SelectHospitalImageRelByHospitalId(int hospitalId)
        {
            HttpResponseMessage httpResponseMessage = await _httpClient.PostAsJsonAsync($"api/HospitalImageRelCore/SelectHospitalImageRelsByHospitalId?hospitalId={hospitalId}", hospitalId);
            List<TblHospitalImageRel> ans = await httpResponseMessage.Content.ReadAsAsync<List<TblHospitalImageRel>>();
            return ans;
        }
        public async Task<List<TblHospitalImageRel>> SelectHospitalImageRelByImageId(int imageId)
        {
            HttpResponseMessage httpResponseMessage = await _httpClient.PostAsJsonAsync($"api/HospitalImageRelCore/SelectHospitalImageRelsByImageId?imageId={imageId}", imageId);
            List<TblHospitalImageRel> ans = await httpResponseMessage.Content.ReadAsAsync<List<TblHospitalImageRel>>();
            return ans;
        }

    }
}