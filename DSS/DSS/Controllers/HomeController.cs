using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DSS.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            Session.Clear();
            return View();
        }

        public ActionResult About()
        {
            Session.Clear();
            return View();
        }

        public ActionResult Contact()
        {
            Session.Clear();
            return View();
        }
        
       
    }
}