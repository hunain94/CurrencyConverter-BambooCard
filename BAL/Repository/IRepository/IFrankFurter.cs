using BAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Repository.IRepository
{
    public interface IFrankFurter
    {
        Task<Dictionary<HttpStatusCode, LatestCurrencyRatesModel>> LatestRates(string baseCurrency);
        Task<Dictionary<HttpStatusCode, object>> CurrencyConversion(decimal amount, string baseCurremcy, string toCurrency);
        Task<Dictionary<HttpStatusCode, HistoricalRates>> HistoricalRates(string baseCurrency, string fromDate, string toDate, int pageNumber, int pageLength);
    }
}
