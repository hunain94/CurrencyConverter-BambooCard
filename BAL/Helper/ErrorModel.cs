using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BAL.Helper
{
    public class ErrorDetails
    {
        public int responseCode { get; set; }
        public string responseMessage { get; set; }
    }
}
