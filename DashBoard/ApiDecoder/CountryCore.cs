using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using DashBoard.Models.Dto;
using DashBoard.Models.Regular;

namespace DashBoard.ApiDecoder
{
    public class CountryCore : ApiController
    {
        private HttpClient _httpClient;

        public CountryCore(string jwtToken)
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("api/CountryCore"));
            _httpClient.BaseAddress = new Uri("http://localhost:13253/");
            _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + jwtToken);
        }
        public async Task<TblCountry> AddCountry(TblCountry country)
        {
            HttpResponseMessage httpResponseMessage = await _httpClient.PostAsJsonAsync("api/CountryCore/AddCountry", country);
            TblCountry ans = await httpResponseMessage.Content.ReadAsAsync<TblCountry>();
            return ans;
        }
        public async Task<bool> DeleteCountry(int id)
        {
            HttpResponseMessage httpResponseMessage = await _httpClient.PostAsJsonAsync($"api/DeleteCountry/DeleteCountry?id={id}", id);
            bool ans = await httpResponseMessage.Content.ReadAsAsync<bool>();
            return ans;
        }
        public async Task<bool> UpdateCountry(TblCountry country, int logId)
        {
            List<object> countryAndLogId = new List<object>();
            countryAndLogId.Add(country);
            countryAndLogId.Add(logId);
            HttpResponseMessage httpResponseMessage = await _httpClient.PostAsJsonAsync("api/CountryCore/UpdateCountry", countryAndLogId);
            bool ans = await httpResponseMessage.Content.ReadAsAsync<bool>();
            return ans;
        }
        public async Task<List<DtoTblCountry>> SelectAllCountrys()
        {
            HttpResponseMessage httpResponseMessage = await _httpClient.GetAsync("api/CountryCore/SelectAllCountrys");
            List<DtoTblCountry> ans = await httpResponseMessage.Content.ReadAsAsync<List<DtoTblCountry>>();
            return ans;
        }
        public async Task<DtoTblCountry> SelectCountryById(int id)
        {
            HttpResponseMessage httpResponseMessage = await _httpClient.PostAsJsonAsync($"api/CountryCore/SelectCountryById?id={id}", id);
            DtoTblCountry ans = await httpResponseMessage.Content.ReadAsAsync<DtoTblCountry>();
            return ans;
        }
        public async Task<DtoTblCountry> SelectCountryByName(string name)
        {
            HttpResponseMessage httpResponseMessage = await _httpClient.PostAsJsonAsync($"api/CountryCore/SelectCountryByName?name={name}", name);
            DtoTblCountry ans = await httpResponseMessage.Content.ReadAsAsync<DtoTblCountry>();
            return ans;
        }

    }
}