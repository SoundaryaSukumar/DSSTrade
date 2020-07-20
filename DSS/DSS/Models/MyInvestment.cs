using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DSS.Models
{
    public class MyInvestment
    {
        public int list { get; set; }
        public string transactionId { get; set; }
        public int NoOfPackage { get; set; }
        public int Amount { get; set; }
        public string transactionDate { get; set; }
        public string userId { get; set; }
    }
}