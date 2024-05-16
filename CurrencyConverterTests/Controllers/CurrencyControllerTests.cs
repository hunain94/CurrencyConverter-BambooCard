using Microsoft.VisualStudio.TestTools.UnitTesting;
using CurrencyConverter.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BAL.Repository.IRepository;
using Moq;
using BAL.Models;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace CurrencyConverter.Controllers.Tests
{
    [TestClass()]
    public class CurrencyControllerTests
    {
        private CurrencyController _currencyController;
        private Mock<IFrankFurter> _frankfurt;

        [TestInitialize]
        public void TestInitialize()
        {
            _frankfurt = new Mock<IFrankFurter>();
            _currencyController = new CurrencyController(_frankfurt.Object);
        }
        [TestMethod()]
        public async Task LatestCurrencyTest()
        {
            //Arrange
            string currency = "EUR";
            Dictionary<HttpStatusCode, LatestCurrencyRatesModel> rates = new Dictionary<HttpStatusCode, LatestCurrencyRatesModel>
            {
                {HttpStatusCode.OK, new LatestCurrencyRatesModel
                {
                    amount = 1,
                    @base = currency,
                    date = "2024-05-15",
                    rates= new Rates{
                        AUD= 1.6308
                    }
                } }
            };

            _frankfurt.Setup(x => x.LatestRates(currency)).ReturnsAsync(rates);
            //Act
            var response = await _currencyController.LatestCurrency(currency);

            //Assert
            Assert.IsNotNull(response);
            Assert.IsInstanceOfType(response, typeof(object));
        }

        [TestMethod()]
        public async Task CurrencyConversionTest()
        {
            decimal amount = 10;
            string currency = "EUR";
            string tocurrency = "USD";

            Dictionary<HttpStatusCode, object> conversion = new Dictionary<HttpStatusCode, object>
            {
                {HttpStatusCode.OK, new ConversionModel{
                    amount = amount,
                    @base = currency,
                    date = "2024-05-16",
                    rates = new ConvertedRate
                    {
                        USD = 12.657
                    }
                } }
            };

            _frankfurt.Setup(x => x.CurrencyConversion(amount, currency, tocurrency)).ReturnsAsync(conversion);
            //Act
            var response = await _currencyController.CurrencyConversion(amount, currency, tocurrency);

            //Assert
            Assert.IsNotNull(response);
            Assert.IsInstanceOfType(response, typeof(object));

        }

        [TestMethod()]
        public async Task HistoricalRatesTest()
        {
            string currency = "USD";
            string fromDate = "2024-01-01";
            string toDate = "2024-01-02";
            int pageNumber = 1;
            int pageSize = 1;
            //Arrange
            List<CurrencyDateModel> currencyDates = new List<CurrencyDateModel>
            {
               new CurrencyDateModel
               {
                   currencyDate = "2024-01-02",
                   currencyRates = new Rates
                   {
                        AUD= 1.4738,
                        BGN= 1.7851,
                        BRL= 4.8888,
                        CAD= 1.3294,
                        CHF= 0.84931,
                        CNY= 7.1435,
                        CZK= 22.533,
                        DKK= 6.8046,
                        GBP= 0.79085,
                        HKD= 7.8139,
                        HUF= 348.76,
                        IDR= 15524,
                        ILS= 3.624,
                        INR= 83.32,
                        ISK= 137.55,
                        JPY= 142.1,
                        KRW= 1313.23,
                        MXN= 17.058,
                        MYR= 4.6025,
                        NOK= 10.2971,
                        NZD= 1.5947,
                        PHP= 55.66,
                        PLN= 3.9894,
                        RON= 4.5368,
                        SEK= 10.1812,
                        SGD= 1.3265,
                        THB= 34.285,
                        TRY= 29.726,
                        USD= 0,
                        ZAR= 18.5889
                   }
               }
            };

            Dictionary<HttpStatusCode, HistoricalRates> history = new Dictionary<HttpStatusCode, HistoricalRates>
            {
                {
                    HttpStatusCode.OK, new HistoricalRates{
                    amount = 1,
                    @base = currency,
                    start_date = "2024-01-02",
                    end_date = "2024-01-02",
                    TotalRecords = 1,
                    rates = currencyDates.ToList().ToPagedList(pageNumber, pageSize)
                } }
            };
            _frankfurt.Setup(x => x.HistoricalRates(currency, fromDate, toDate, pageNumber, pageSize)).ReturnsAsync(history);
            //Act
            var response = await _currencyController.HistoricalRates(currency, fromDate, toDate, pageNumber, pageSize);

            //Assert
            Assert.IsNotNull(response);
            Assert.IsInstanceOfType(response, typeof(object));
        }
    }
}