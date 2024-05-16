using BAL.Models;
using BAL.Repository.IRepository;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Polly;
using Polly.Contrib.WaitAndRetry;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using System.Web.Configuration;
using X.PagedList;

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
        public async Task<Dictionary<HttpStatusCode, LatestCurrencyRatesModel>> LatestRates(string baseCurrency)
        {
            if (String.IsNullOrEmpty(baseCurrency))
                baseCurrency = "EUR";

            Dictionary<HttpStatusCode, LatestCurrencyRatesModel> latestRates = new Dictionary<HttpStatusCode, LatestCurrencyRatesModel>();
            var client = _httpClientFactory.CreateClient(baseURL);
            var request = "/latest?from=" + baseCurrency + "";
            HttpResponseMessage response = await client.GetAsync(request);
            LatestCurrencyRatesModel data = await response.Content.ReadFromJsonAsync<LatestCurrencyRatesModel>();
            latestRates.Add(response.StatusCode, data);
            return latestRates;
        }
        public async Task<Dictionary<HttpStatusCode, object>> CurrencyConversion(decimal amount, string baseCurrency, string toCurrency)
        {
            if (String.IsNullOrEmpty(baseCurrency))
                baseCurrency = "EUR";

            Dictionary<HttpStatusCode, object> convertedCurrency = new Dictionary<HttpStatusCode, object>();
            var client = _httpClientFactory.CreateClient(baseURL);
            var request = "/latest?amount=" + amount + "&from=" + baseCurrency + "&to=" + toCurrency + "";
            HttpResponseMessage response = await client.GetAsync(request);
            dynamic data = await response.Content.ReadFromJsonAsync<dynamic>();
            convertedCurrency.Add(response.StatusCode, data);
            return convertedCurrency;
        }
        public async Task<Dictionary<HttpStatusCode, HistoricalRates>> HistoricalRates(string baseCurrency, string fromDate, string toDate, int pageNumber, int pageLength)
        {
            if (String.IsNullOrEmpty(baseCurrency))
                baseCurrency = "EUR";

            Dictionary<HttpStatusCode, HistoricalRates> historicalRates = new Dictionary<HttpStatusCode, HistoricalRates>();
            var client = _httpClientFactory.CreateClient(baseURL);
            var request = "/" + fromDate + ".." + toDate + "?from=" + baseCurrency + "";
            HttpResponseMessage response = await client.GetAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var jsonResult = response.Content.ReadAsStringAsync().Result;
                JObject result = JObject.Parse(jsonResult);

                HistoricalRates historical = new HistoricalRates();
                
                List<CurrencyDateModel> rates = new List<CurrencyDateModel>();
                historical.amount = Convert.ToDouble(result.ContainsKey("amount"));
                historical.@base = Convert.ToString(result.ContainsKey("base"));
                historical.start_date = Convert.ToString(result.ContainsKey("start_date"));
                historical.end_date = Convert.ToString(result.ContainsKey("end_date"));
                foreach (var item in ((Newtonsoft.Json.Linq.JContainer)((Newtonsoft.Json.Linq.JProperty)result.Last).Value))
                {
                    CurrencyDateModel model = new CurrencyDateModel();
                    model.currencyDate = ((Newtonsoft.Json.Linq.JProperty)item).Name;
                    model.currencyRates = JsonConvert.DeserializeObject<Rates>(item.First.ToString());
                    rates.Add(model);

                }
                historical.TotalRecords = rates.Count();
                historical.rates = rates.ToList().ToPagedList(pageNumber, pageLength);
                historicalRates.Add(response.StatusCode, historical);
            }
            else 
            {
                historicalRates.Add(response.StatusCode, new Models.HistoricalRates());
            }
            
            return historicalRates;
        }

    }
}
