using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using DashBoard.Models.Dto;
using DashBoard.Models.Regular;

namespace DashBoard.ApiDecoder
{
    public class TicketCore : ApiController
    {
        private HttpClient _httpClient;

        public TicketCore(string jwtToken)
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("api/TicketCore"));
            _httpClient.BaseAddress = new Uri("http://localhost:13253/");
            _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + jwtToken);
        }
        public async Task<TblTicket> AddTicket(TblTicket ticket)
        {
            HttpResponseMessage httpResponseMessage = await _httpClient.PostAsJsonAsync("api/TicketCore/AddTicket", ticket);
            TblTicket ans = await httpResponseMessage.Content.ReadAsAsync<TblTicket>();
            return ans;
        }
        public async Task<bool> DeleteTicket(int id)
        {
            HttpResponseMessage httpResponseMessage = await _httpClient.PostAsJsonAsync($"api/DeleteTicket/DeleteTicket?id={id}", id);
            bool ans = await httpResponseMessage.Content.ReadAsAsync<bool>();
            return ans;
        }
        public async Task<bool> UpdateTicket(TblTicket ticket, int logId)
        {
            List<object> ticketAndLogId = new List<object>();
            ticketAndLogId.Add(ticket);
            ticketAndLogId.Add(logId);
            HttpResponseMessage httpResponseMessage = await _httpClient.PostAsJsonAsync("api/TicketCore/UpdateTicket", ticketAndLogId);
            bool ans = await httpResponseMessage.Content.ReadAsAsync<bool>();
            return ans;
        }
        public async Task<List<DtoTblTicket>> SelectAllTickets()
        {
            HttpResponseMessage httpResponseMessage = await _httpClient.GetAsync("api/TicketCore/SelectAllTickets");
            List<DtoTblTicket> ans = await httpResponseMessage.Content.ReadAsAsync<List<DtoTblTicket>>();
            return ans;
        }
        public async Task<TblTicket> SelectTicketById(int id)
        {
            HttpResponseMessage httpResponseMessage = await _httpClient.PostAsJsonAsync($"api/TicketCore/SelectTicketById?id={id}", id);
            TblTicket ans = await httpResponseMessage.Content.ReadAsAsync<TblTicket>();
            return ans;
        }
        public async Task<List<DtoTblTicket>> SelectTicketByIsRegistered(bool isRegistered)
        {
            HttpResponseMessage httpResponseMessage = await _httpClient.PostAsJsonAsync($"api/TicketCore/SelectTicketByIsRegistered?isRegistered={isRegistered}", isRegistered);
            List<DtoTblTicket> ans = await httpResponseMessage.Content.ReadAsAsync<List<DtoTblTicket>>();
            return ans;
        }
        public async Task<List<DtoTblTicket>> SelectTicketByUserPassId(int userPassId)
        {
            HttpResponseMessage httpResponseMessage = await _httpClient.PostAsJsonAsync($"api/TicketCore/SelectTicketByUserPassId?userPassId={userPassId}", userPassId);
            List<DtoTblTicket> ans = await httpResponseMessage.Content.ReadAsAsync<List<DtoTblTicket>>();
            return ans;
        }
        public async Task<DtoTblTicket> SelectTicketByEmail(string email)
        {
            HttpResponseMessage httpResponseMessage = await _httpClient.PostAsJsonAsync($"api/TicketCore/SelectTicketByEmail?email={email}", email);
            DtoTblTicket ans = await httpResponseMessage.Content.ReadAsAsync<DtoTblTicket>();
            return ans;
        }
        public async Task<DtoTblTicket> SelectTicketByTellNo(string tellNo)
        {
            HttpResponseMessage httpResponseMessage = await _httpClient.PostAsJsonAsync($"api/TicketCore/SelectTicketByTellNo?tellNo={tellNo}", tellNo);
            DtoTblTicket ans = await httpResponseMessage.Content.ReadAsAsync<DtoTblTicket>();
            return ans;
        }
        public async Task<List<DtoTblImage>> SelectImageByTicketId(int ticketId)
        {
            HttpResponseMessage httpResponseMessage = await _httpClient.PostAsJsonAsync($"api/ImageCore/SelectImageByTicketId?ticketId={ticketId}", ticketId);
            List<DtoTblImage> ans = await httpResponseMessage.Content.ReadAsAsync<List<DtoTblImage>>();
            return ans;
        }

    }
}