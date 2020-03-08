using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using DashBoard.Models.Dto;
using DashBoard.Models.Regular;

namespace DashBoard.ApiDecoder
{
    public class OperationCore : ApiController
    {
        private HttpClient _httpClient;

        public OperationCore(string jwtToken)
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("api/OperationCore"));
            _httpClient.BaseAddress = new Uri("http://localhost:13253/");
            _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + jwtToken);
        }
        public async Task<TblOperation> AddOperation(TblOperation operation)
        {
            HttpResponseMessage httpResponseMessage = await _httpClient.PostAsJsonAsync("api/OperationCore/AddOperation", operation);
            TblOperation ans = await httpResponseMessage.Content.ReadAsAsync<TblOperation>();
            return ans;
        }

        {
            HttpResponseMessage httpResponseMessage = await _httpClient.PostAsJsonAsync($"api/DeleteOperation/DeleteOperation?id={id}", id);
            TblOperation ans = await httpResponseMessage.Content.ReadAsAsync<TblOperation>();
            return ans;
        }

        {
            List<object> operationAndLogId = new List<object>();
            operationAndLogId.Add(operation);
            operationAndLogId.Add(logId);
            HttpResponseMessage httpResponseMessage = await _httpClient.PostAsJsonAsync("api/OperationCore/UpdateOperation", operationAndLogId);
            bool ans = await httpResponseMessage.Content.ReadAsAsync<bool>();
            return ans;
        }

        {
            HttpResponseMessage httpResponseMessage = await _httpClient.GetAsync("api/OperationCore/SelectAllOperations");
            List<DtoTblOperation> ans = await httpResponseMessage.Content.ReadAsAsync<List<DtoTblOperation>>();
            return ans;
        }

        {
            HttpResponseMessage httpResponseMessage = await _httpClient.PostAsJsonAsync($"api/OperationCore/SelectOperationById?id={id}", id);
            bool ans = await httpResponseMessage.Content.ReadAsAsync<bool>();
            return ans;
        }

        {
            HttpResponseMessage httpResponseMessage = await _httpClient.PostAsJsonAsync($"api/OperationCore/SelectOperationByOperationName?operationName={operationName}", operationName);
            DtoTblOperation ans = await httpResponseMessage.Content.ReadAsAsync<DtoTblOperation>();
            return ans;
        }

        {
            HttpResponseMessage httpResponseMessage = await _httpClient.PostAsJsonAsync($"api/ImageCore/SelectImageByOperationId?operationId={operationId}", operationId);
            List<DtoTblImage> ans = await httpResponseMessage.Content.ReadAsAsync<List<DtoTblImage>>();
            return ans;
        }

    }
}