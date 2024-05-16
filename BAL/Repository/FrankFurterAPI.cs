using BAL.Models;
using BAL.Repository.IRepository;
using Newtonsoft.Json;
using Polly;
using Polly.Contrib.WaitAndRetry;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;

namespace BAL.Repository
{
    public class FrankFurterAPI : IFrankFurter
    {
        public const string baseURL = "frankfurter";
        private readonly IHttpClientFactory _httpClientFactory;
        public FrankFurterAPI(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<Dictionary<HttpStatusCode, object>> LatestRates(string baseCurrency)
        {
            Dictionary<HttpStatusCode, object> latestRates = new Dictionary<HttpStatusCode, object>();
            var client = _httpClientFactory.CreateClient(baseURL);
            var request = "/latest?from=" + baseCurrency + "";
            HttpResponseMessage response = await client.GetAsync(request);
            dynamic data = await response.Content.ReadFromJsonAsync<dynamic>();
            latestRates.Add(response.StatusCode, data);
            return latestRates;
        }
        public async Task<Dictionary<HttpStatusCode, object>> CurrencyConversion(decimal amount, string baseCurremcy, string toCurrency)
        {
            Dictionary<HttpStatusCode, object> convertedCurrency = new Dictionary<HttpStatusCode, object>();
            var client = _httpClientFactory.CreateClient(baseURL);
            var request = "/latest?amount=" + amount + "&from=" + baseCurremcy + "&to=" + toCurrency + "";
            HttpResponseMessage response = await client.GetAsync(request);
            dynamic data = await response.Content.ReadFromJsonAsync<dynamic>();
            convertedCurrency.Add(response.StatusCode, data);
            return convertedCurrency;
        }
        public async Task<Dictionary<HttpStatusCode, object>> HistoricalRates(string baseCurrency, string fromDate, string toDate)
        {
            Dictionary<HttpStatusCode, object> historicalRates = new Dictionary<HttpStatusCode, object>();
            var client = _httpClientFactory.CreateClient(baseURL);
            var request = "/" + fromDate + ".." + toDate + "?from=" + baseCurrency + "";
            HttpResponseMessage response = await client.GetAsync(request);
            dynamic data = await response.Content.ReadFromJsonAsync<dynamic>();
            historicalRates.Add(response.StatusCode, data);
            return historicalRates;
        }

    }
}
