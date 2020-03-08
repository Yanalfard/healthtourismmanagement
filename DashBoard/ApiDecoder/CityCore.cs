using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using DashBoard.Models.Dto;
using DashBoard.Models.Regular;

namespace DashBoard.ApiDecoder
{
    public class CityCore : ApiController
    {
        private HttpClient _httpClient;

        public CityCore(string jwtToken)
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("api/CityCore"));
            _httpClient.BaseAddress = new Uri("http://localhost:13253/");
            _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + jwtToken);
        }
        public async Task<DtoTblCity> AddCity(TblCity city)
        {
            HttpResponseMessage httpResponseMessage = await _httpClient.PostAsJsonAsync("api/CityCore/AddCity", city);
            DtoTblCity ans = await httpResponseMessage.Content.ReadAsAsync<DtoTblCity>();
            return ans;
        }
        public async Task<bool> DeleteCity(int id)
        {
            HttpResponseMessage httpResponseMessage = await _httpClient.PostAsJsonAsync($"api/DeleteCity/DeleteCity?id={id}", id);
            bool ans = await httpResponseMessage.Content.ReadAsAsync<bool>();
            return ans;
        }
        public async Task<bool> UpdateCity(TblCity city, int logId)
        {
            List<object> cityAndLogId = new List<object>();
            cityAndLogId.Add(city);
            cityAndLogId.Add(logId);
            HttpResponseMessage httpResponseMessage = await _httpClient.PostAsJsonAsync("api/CityCore/UpdateCity", cityAndLogId);
            bool ans = await httpResponseMessage.Content.ReadAsAsync<bool>();
            return ans;
        }
        public async Task<List<DtoTblCity>> SelectAllCitys()
        {
            HttpResponseMessage httpResponseMessage = await _httpClient.GetAsync("api/CityCore/SelectAllCitys");
            List<DtoTblCity> ans = await httpResponseMessage.Content.ReadAsAsync<List<DtoTblCity>>();
            return ans;
        }
        public async Task<DtoTblCity> SelectCityById(int id)
        {
            HttpResponseMessage httpResponseMessage = await _httpClient.PostAsJsonAsync($"api/CityCore/SelectCityById?id={id}", id);
            DtoTblCity ans = await httpResponseMessage.Content.ReadAsAsync<DtoTblCity>();
            return ans;
        }
        public async Task<DtoTblCity> SelectCityByName(string name)
        {
            HttpResponseMessage httpResponseMessage = await _httpClient.PostAsJsonAsync($"api/CityCore/SelectCityByName?name={name}", name);
            DtoTblCity ans = await httpResponseMessage.Content.ReadAsAsync<DtoTblCity>();
            return ans;
        }
        public async Task<List<DtoTblCity>> SelectCityByCountryId(int countryId)
        {
            HttpResponseMessage httpResponseMessage = await _httpClient.PostAsJsonAsync($"api/CityCore/SelectCityByCountryId?countryId={countryId}", countryId);
            List<DtoTblCity> ans = await httpResponseMessage.Content.ReadAsAsync<List<DtoTblCity>>();
            return ans;
        }

    }
}