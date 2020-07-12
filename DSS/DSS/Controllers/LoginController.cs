using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DSS.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LoginValidation(string username, string password)
        {
            if(username.Equals("ABC123") && password.Equals("ABC123"))
            {
                System.Web.Security.FormsAuthentication.SetAuthCookie(username,true);
                Session["IsAuthenticated"] = true;
                return RedirectToAction("Welcome", "Details");
            }
            else
            {
                return RedirectToAction("Index","Home");
            }
        }
        
        public ActionResult Register()
        {
            return View();
        }

        public ActionResult ForgotPassword()
        {
            return View();
        }

    }
}