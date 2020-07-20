using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DSS.Models
{
    public class UserProfile
    {
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNo { get; set; }
        public string AadharNo { get; set; }
        public string PanNo { get; set; }
        public string MyRefferalId { get; set; }
        public string Password { get; set; }

        //Bank Details
        public string BankName { get; set; }
        public string Branch { get; set; }
        public string IFSC { get; set; }
        public string AccountNo { get; set; }
        public string AccountHolder { get; set; }
        public string GooglePay { get; set; }
        public string PhonePe { get; set; }

        public int list { get; set; }
    }
}