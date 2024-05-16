using BAL.Helper;
using BAL.Models;
using BAL.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CurrencyConverter.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class CurrencyController : ControllerBase
    {
        private readonly IFrankFurter frankFurter;
        public CurrencyController(IFrankFurter _frankFurter)
        {
            frankFurter = _frankFurter;
        }
        [HttpGet]
        public async Task<IActionResult> LatestCurrency(string baseCurrency)
        {
            var latestCurrency = await frankFurter.LatestRates(baseCurrency);
            if (latestCurrency.ContainsKey(HttpStatusCode.OK))
            {
                return Ok(latestCurrency.Values);
            }
            else 
            {
                return NotFound(latestCurrency.Values);
            }
            
        }
        [HttpGet]
        public async Task<IActionResult> CurrencyConversion(decimal amount, string baseCurrency, string conversionCurrency)
        {
            bool checkCurrency = UtilityHelper.checkCurrency(conversionCurrency);
            if (checkCurrency)
            {
                var convertedCurrency = await frankFurter.CurrencyConversion(amount, baseCurrency, conversionCurrency);
                if (convertedCurrency.ContainsKey(HttpStatusCode.OK))
                {
                    return Ok(convertedCurrency.Values);
                }
                else
                {
                    return NotFound(convertedCurrency.Values);
                }
            }
            else 
            {
                return BadRequest();
            }
            
        }
        [HttpGet]
        public async Task<IActionResult> HistoricalRates(string baseCurrency, string fromDate, string toDate) 
        {
            var historicalRates = await frankFurter.HistoricalRates(baseCurrency, fromDate, toDate);
            if (historicalRates.ContainsKey(HttpStatusCode.OK))
            {
                return Ok(historicalRates.Values);
            }
            else
            {
                return NotFound(historicalRates.Values);
            }
        }

    }
}
