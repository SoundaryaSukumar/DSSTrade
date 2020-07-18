using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;

namespace DSS.Models
{
    public class AdminPayout
    {
        public string UserId { set; get; }
        public int Days { set; get; }
        public int Amount { set; get; }

    }
}