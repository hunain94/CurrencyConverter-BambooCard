using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace BAL.Models
{

    public class Rates
    {
        public double AUD { get; set; }
        public double BGN { get; set; }
        public double BRL { get; set; }
        public double CAD { get; set; }
        public double CHF { get; set; }
        public double CNY { get; set; }
        public double CZK { get; set; }
        public double DKK { get; set; }
        public double GBP { get; set; }
        public double HKD { get; set; }
        public double HUF { get; set; }
        public int IDR { get; set; }
        public double ILS { get; set; }
        public double INR { get; set; }
        public double ISK { get; set; }
        public double JPY { get; set; }
        public double KRW { get; set; }
        public double MXN { get; set; }
        public double MYR { get; set; }
        public double NOK { get; set; }
        public double NZD { get; set; }
        public double PHP { get; set; }
        public double PLN { get; set; }
        public double RON { get; set; }
        public double SEK { get; set; }
        public double SGD { get; set; }
        public double THB { get; set; }
        public double TRY { get; set; }
        public double USD { get; set; }
        public double ZAR { get; set; }
    }
    public class ConvertedRate
    {
        public double USD { get; set; }
    }

    public class ConversionModel
    {
        public decimal amount { get; set; }
        public string @base { get; set; }
        public string date { get; set; }
        public ConvertedRate rates { get; set; }
    }


    public class LatestCurrencyRatesModel
    {
        public double amount { get; set; }
        public string @base { get; set; }
        public string date { get; set; }
        public Rates rates { get; set; }
    }
    public class HistoricalRates
    {
        public double amount { get; set; }
        public string @base { get; set; }
        public string start_date { get; set; }
        public string end_date { get; set; }
        public int TotalRecords { get; set; }
        public IPagedList<CurrencyDateModel> rates { get; set; }
    }
    public class CurrencyDateModel
    {
        public string currencyDate { get; set; }
        public Rates currencyRates { get; set; }
    }
    public class CurrencyRatesModel
    {
        public string currencyCode { get; set; }
        public string currencyValue { get; set; }
    }
    public class HistoryDataModel 
    {
        public double amount { get; set; }
        public string @base { get; set; }
        public string start_date { get; set; }
        public string end_date { get; set; }
    }

}
