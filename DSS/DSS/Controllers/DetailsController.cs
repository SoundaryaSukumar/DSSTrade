using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DSS.Controllers
{
    public class DetailsController : Controller
    {
        // GET: Details
        public ActionResult UserProfile()
        {
            return View();
            //if (User.Identity.IsAuthenticated)
            //{
            //    return View();
            //}
            //else
            //{
            //    return RedirectToAction("Index","Home");
            //}
        }
        public ActionResult EditProfile() {
            return View();
        }
        public ActionResult Welcome()
        {
            return View();
        }
        public ActionResult BankDetails()
        {
            return View();
        }
        public ActionResult ChangePassword()
        {
            return View();
        }
        public ActionResult MyInvestment()
        {
            return View();
        }
        public ActionResult InvestmentUpdate()
        {
            return View();
        }
        public ActionResult BankDetailsUpdate()
        {
            return View();
        }
    }
}