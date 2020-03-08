using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using DashBoard.Models.Dto;
using DashBoard.Models.Regular;

namespace DashBoard.ApiDecoder
{
    public class TicketImageRelCore : ApiController
    {
        private HttpClient _httpClient;

        public TicketImageRelCore(string jwtToken)
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("api/TicketImageRelCore"));
            _httpClient.BaseAddress = new Uri("http://localhost:13253/");
            _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + jwtToken);
        }
        public async Task<TblTicketImageRel> AddTicketImageRel(TblTicketImageRel ticketImageRel)
        {
            HttpResponseMessage httpResponseMessage = await _httpClient.PostAsJsonAsync("api/TicketImageRelCore/AddTicketImageRel", ticketImageRel);
            TblTicketImageRel ans = await httpResponseMessage.Content.ReadAsAsync<TblTicketImageRel>();
            return ans;
        }
        public async Task<bool> DeleteTicketImageRel(int id)
        {
            HttpResponseMessage httpResponseMessage = await _httpClient.PostAsJsonAsync($"api/DeleteTicketImageRel/DeleteTicketImageRel?id={id}", id);
            bool ans = await httpResponseMessage.Content.ReadAsAsync<bool>();
            return ans;
        }
        public async Task<bool> UpdateTicketImageRel(TblTicketImageRel ticketImageRel, int logId)
        {
            List<object> ticketImageRelAndLogId = new List<object>();
            ticketImageRelAndLogId.Add(ticketImageRel);
            ticketImageRelAndLogId.Add(logId);
            HttpResponseMessage httpResponseMessage = await _httpClient.PostAsJsonAsync("api/TicketImageRelCore/UpdateTicketImageRel", ticketImageRelAndLogId);
            bool ans = await httpResponseMessage.Content.ReadAsAsync<bool>();
            return ans;
        }
        public async Task<List<DtoTblTicketImageRel>> SelectAllTicketImageRels()
        {
            HttpResponseMessage httpResponseMessage = await _httpClient.GetAsync("api/TicketImageRelCore/SelectAllTicketImageRels");
            List<DtoTblTicketImageRel> ans = await httpResponseMessage.Content.ReadAsAsync<List<DtoTblTicketImageRel>>();
            return ans;
        }
        public async Task<DtoTblTicketImageRel> SelectTicketImageRelById(int id)
        {
            HttpResponseMessage httpResponseMessage = await _httpClient.PostAsJsonAsync($"api/TicketImageRelCore/SelectTicketImageRelById?id={id}", id);
            DtoTblTicketImageRel ans = await httpResponseMessage.Content.ReadAsAsync<DtoTblTicketImageRel>();
            return ans;
        }
        public async Task<List<TblTicketImageRel>> SelectTicketImageRelByTicketId(int ticketId)
        {
            HttpResponseMessage httpResponseMessage = await _httpClient.PostAsJsonAsync($"api/TicketImageRelCore/SelectTicketImageRelsByTicketId?ticketId={ticketId}", ticketId);
            List<TblTicketImageRel> ans = await httpResponseMessage.Content.ReadAsAsync<List<TblTicketImageRel>>();
            return ans;
        }
        public async Task<List<TblTicketImageRel>> SelectTicketImageRelByImageId(int imageId)
        {
            HttpResponseMessage httpResponseMessage = await _httpClient.PostAsJsonAsync($"api/TicketImageRelCore/SelectTicketImageRelsByImageId?imageId={imageId}", imageId);
            List<TblTicketImageRel> ans = await httpResponseMessage.Content.ReadAsAsync<List<TblTicketImageRel>>();
            return ans;
        }

    }
}