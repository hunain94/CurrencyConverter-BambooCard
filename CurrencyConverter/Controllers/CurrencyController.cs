using BAL.Helper;
using BAL.Models;
using BAL.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Globalization;
using System.Net;
namespace CurrencyConverter.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class CurrencyController : ControllerBase
    {
        private readonly IFrankFurter _frankFurter;
        public CurrencyController(IFrankFurter frankFurter)
        {
            _frankFurter = frankFurter;
        }
        [HttpGet]
        public async Task<IActionResult> LatestCurrency(string? baseCurrency)
        {
            var latestCurrency = await _frankFurter.LatestRates(baseCurrency);
            if (latestCurrency.ContainsKey(HttpStatusCode.OK))
            {
                return StatusCode((int)HttpStatusCode.OK, latestCurrency.Values);
            }
            else
            {
                return StatusCode((int)latestCurrency.Keys.First());
            }

        }
        [HttpGet]
        public async Task<IActionResult> CurrencyConversion(decimal amount, string? baseCurrency, string conversionCurrency)
        {
            if (String.IsNullOrEmpty(conversionCurrency))
            {
                return StatusCode((int)HttpStatusCode.BadRequest, "Conversion currency is required");
            }
            bool checkCurrency = UtilityHelper.checkCurrency(conversionCurrency);
            if (checkCurrency)
            {
                var convertedCurrency = await _frankFurter.CurrencyConversion(amount, baseCurrency, conversionCurrency);
                if (convertedCurrency.ContainsKey(HttpStatusCode.OK))
                {
                    return StatusCode((int)HttpStatusCode.OK, convertedCurrency.Values);
                }
                else
                {
                    return StatusCode((int)convertedCurrency.Keys.First());
                }
            }
            else
            {
                return StatusCode((int)HttpStatusCode.BadRequest, "Bad Request");
            }

        }
        [HttpGet]
        public async Task<IActionResult> HistoricalRates(string? baseCurrency, string fromDate, string toDate, int pageNumber = 1, int pageSize = 10)
        {
            if (String.IsNullOrEmpty(fromDate) || String.IsNullOrEmpty(toDate)) 
            {
                return StatusCode((int)HttpStatusCode.BadRequest, "From Date cannot be greater then currentDate or to date");
            }
            DateTime FromDate = DateTime.ParseExact(fromDate, UtilityHelper.DATETIME_FORMAT_yyyy_MM_dd, CultureInfo.InvariantCulture);
            DateTime ToDate = DateTime.ParseExact(toDate, UtilityHelper.DATETIME_FORMAT_yyyy_MM_dd, CultureInfo.InvariantCulture);

            if (FromDate > DateTime.Now || FromDate > ToDate) 
            {
                return StatusCode((int)HttpStatusCode.BadRequest, "From Date cannot be greater then currentDate or to date");
            }
            var historicalRates = await _frankFurter.HistoricalRates(baseCurrency, fromDate, toDate, pageNumber, pageSize);
            if (historicalRates.ContainsKey(HttpStatusCode.OK))
            {
                return StatusCode((int)HttpStatusCode.OK, historicalRates.Values);
            }
            else
            {
                return StatusCode((int)historicalRates.Keys.First());
            }
        }

    }
}
