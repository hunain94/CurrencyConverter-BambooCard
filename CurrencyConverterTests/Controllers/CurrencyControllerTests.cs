using Microsoft.VisualStudio.TestTools.UnitTesting;
using CurrencyConverter.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BAL.Repository.IRepository;
using BAL.Repository;
using BAL.Models;

namespace CurrencyConverter.Controllers.Tests
{
    [TestClass()]
    public class CurrencyControllerTests
    {
        [TestMethod()]
        public void LatestCurrencyTest()
        {
            //FrankFurterAPI frankFurter = new FrankFurterAPI();
            string currency = "USD";
            //var data = frankFurter.LatestRates(currency).GetAwaiter().GetResult();

            Assert.IsNotNull(currency);

        }

        [TestMethod()]
        public void CurrencyConversionTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void HistoricalRatesTest()
        {
            Assert.Fail();
        }
    }
}