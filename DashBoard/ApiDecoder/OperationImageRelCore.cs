using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using DashBoard.Models.Dto;
using DashBoard.Models.Regular;

namespace DashBoard.ApiDecoder
{
    public class OperationImageRelCore : ApiController
    {
        private HttpClient _httpClient;

        public OperationImageRelCore(string jwtToken)
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("api/OperationImageRelCore"));
            _httpClient.BaseAddress = new Uri("http://localhost:13253/");
            _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + jwtToken);
        }
        public async Task<TblOperationImageRel> AddOperationImageRel(TblOperationImageRel operationImageRel)
        {
            HttpResponseMessage httpResponseMessage = await _httpClient.PostAsJsonAsync("api/OperationImageRelCore/AddOperationImageRel", operationImageRel);
            TblOperationImageRel ans = await httpResponseMessage.Content.ReadAsAsync<TblOperationImageRel>();
            return ans;
        }
        public async Task<bool> DeleteOperationImageRel(int id)
        {
            HttpResponseMessage httpResponseMessage = await _httpClient.PostAsJsonAsync($"api/DeleteOperationImageRel/DeleteOperationImageRel?id={id}", id);
            bool ans = await httpResponseMessage.Content.ReadAsAsync<bool>();
            return ans;
        }
        public async Task<bool> UpdateOperationImageRel(TblOperationImageRel operationImageRel, int logId)
        {
            List<object> operationImageRelAndLogId = new List<object>();
            operationImageRelAndLogId.Add(operationImageRel);
            operationImageRelAndLogId.Add(logId);
            HttpResponseMessage httpResponseMessage = await _httpClient.PostAsJsonAsync("api/OperationImageRelCore/UpdateOperationImageRel", operationImageRelAndLogId);
            bool ans = await httpResponseMessage.Content.ReadAsAsync<bool>();
            return ans;
        }
        public async Task<List<DtoTblOperationImageRel>> SelectAllOperationImageRels()
        {
            HttpResponseMessage httpResponseMessage = await _httpClient.GetAsync("api/OperationImageRelCore/SelectAllOperationImageRels");
            List<DtoTblOperationImageRel> ans = await httpResponseMessage.Content.ReadAsAsync<List<DtoTblOperationImageRel>>();
            return ans;
        }
        public async Task<TblOperationImageRel> SelectOperationImageRelById(int id)
        {
            HttpResponseMessage httpResponseMessage = await _httpClient.PostAsJsonAsync($"api/OperationImageRelCore/SelectOperationImageRelById?id={id}", id);
            TblOperationImageRel ans = await httpResponseMessage.Content.ReadAsAsync<TblOperationImageRel>();
            return ans;
        }
        public async Task<List<TblOperationImageRel>> SelectOperationImageRelByOperationId(int operationId)
        {
            HttpResponseMessage httpResponseMessage = await _httpClient.PostAsJsonAsync($"api/OperationImageRelCore/SelectOperationImageRelsByOperationId?operationId={operationId}", operationId);
            List<TblOperationImageRel> ans = await httpResponseMessage.Content.ReadAsAsync<List<TblOperationImageRel>>();
            return ans;
        }
        public async Task<List<TblOperationImageRel>> SelectOperationImageRelByImageId(int imageId)
        {
            HttpResponseMessage httpResponseMessage = await _httpClient.PostAsJsonAsync($"api/OperationImageRelCore/SelectOperationImageRelsByImageId?imageId={imageId}", imageId);
            List<TblOperationImageRel> ans = await httpResponseMessage.Content.ReadAsAsync<List<TblOperationImageRel>>();
            return ans;
        }

    }
}