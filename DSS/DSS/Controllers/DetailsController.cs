﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MySql.Data.MySqlClient;
using MySql.Data;
using System.Diagnostics;
using System.Data;

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
        public ActionResult UpdateBankDetails(string user_id, string branch, string bank_name, string account_no, string ifsc, string gpay, string account_holder, string phpay)
        {
            MySqlConnection connection = new MySqlConnection("Server=localhost;Database=dss;Uid=sab;Pwd=user;");
            MySqlCommand cmd;
            connection.Open();
            try
            {
                cmd = connection.CreateCommand();
                //cmd.CommandText = "INSERT INTO Register(RefferalId,RefferalName,FirstName,LastName,Password,ConfirmPassword,Email,PhoneNo)VALUES(\"sd\",\"fdf\",\"dfdsf\",\"dfd\",\"sdf\",\"fdsf\",\"dfd\",\"dfdf\")";
                cmd.CommandText = "INSERT INTO BankDetails(UserId,BankName,BranchName,AccountNo,IFSC,AccountHolder,GPay,PhPhe)VALUES(@uid,@bname,@branchname,@accno,@ifsc,@accholder,@gpay,@phphe)";
                cmd.Parameters.AddWithValue("@uid", user_id);
                cmd.Parameters.AddWithValue("@bname", bank_name);
                cmd.Parameters.AddWithValue("@branchname", branch);
                cmd.Parameters.AddWithValue("@accno", account_no);
                cmd.Parameters.AddWithValue("@ifsc", ifsc);
                cmd.Parameters.AddWithValue("@accholder", account_holder);
                cmd.Parameters.AddWithValue("@gpay", gpay);
                cmd.Parameters.AddWithValue("@phphe", phpay);
                cmd.ExecuteNonQuery();
                return RedirectToAction("Register", "Login");
            }
            catch (Exception)
            {
                return View();
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }
        public ActionResult Payout()
        {
            return View();
        }
    }
}