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
        Task<Dictionary<HttpStatusCode, object>> LatestRates(string baseCurrency);
        Task<Dictionary<HttpStatusCode, object>> CurrencyConversion(decimal amount, string baseCurremcy, string toCurrency);
        Task<Dictionary<HttpStatusCode, object>> HistoricalRates(string fromDate, string toDate, string baseCurrency);
    }
}
