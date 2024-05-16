using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Helper
{
    public class UtilityHelper
    {
        public static string DATETIME_FORMAT_yyyy_MM_dd = "yyyy-MM-dd";
        public static bool checkCurrency(string currency)
        {
            List<string> exceptionList = new List<string> { "TRY", "PLN", "THB", "MXN" };

            if (exceptionList.Contains(currency))
                return false;

            else
                return true;

        }
    }
}
