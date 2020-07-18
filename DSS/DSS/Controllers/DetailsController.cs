using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MySql.Data.MySqlClient;
using MySql.Data;
using System.Diagnostics;
using System.Data;
using DSS.Models;

namespace DSS.Controllers
{
    public class DetailsController : Controller
    {
        // GET: Details
        public ActionResult UserProfile()
        {
            if (Session["userId"] != null)
            {
                MySqlConnection connection = new MySqlConnection("Server=localhost;Database=dss;Uid=dsstrade;Pwd=user;");
                MySqlCommand cmd;
                connection.Open();
                try
                {
                    cmd = connection.CreateCommand();
                    cmd.CommandText = "select userid,firstname,lastname,email,phoneno,aadharno,panno from register WHERE UserId=@userid";
                    cmd.Parameters.AddWithValue("@userid", Session["userId"].ToString());
                    cmd.ExecuteNonQuery();
                    MySqlDataReader sqlDataReader = cmd.ExecuteReader();
                    var userProfile = new UserProfile();
                    while (sqlDataReader.Read())
                    {
                        userProfile.UserId = sqlDataReader.GetString(0);
                        userProfile.FirstName = sqlDataReader.GetString(1);
                        userProfile.LastName = sqlDataReader.GetString(2);
                        userProfile.Email = sqlDataReader.GetString(3);
                        userProfile.PhoneNo = sqlDataReader.GetString(4);
                        userProfile.AadharNo = sqlDataReader.GetString(5);
                        userProfile.PanNo = sqlDataReader.GetString(6);
                    }
                    connection.Close();
                    connection.Open();
                    cmd = connection.CreateCommand();
                    cmd.CommandText = "select bankname,branchname,accountno,ifsc,accountholder,gpay,phphe from bankdetails WHERE UserId=@userid";
                    cmd.Parameters.AddWithValue("@userid", Session["userId"].ToString());
                    cmd.ExecuteNonQuery();
                    MySqlDataReader sqlDataReader1 = cmd.ExecuteReader();
                    while (sqlDataReader1.Read())
                    {
                        userProfile.BankName = sqlDataReader1.GetString(0);
                        userProfile.Branch = sqlDataReader1.GetString(1);
                        userProfile.AccountNo = sqlDataReader1.GetString(2);
                        userProfile.IFSC = sqlDataReader1.GetString(3);
                        userProfile.AccountHolder = sqlDataReader1.GetString(4);
                        userProfile.GooglePay = sqlDataReader1.GetString(5);
                        userProfile.PhonePe = sqlDataReader1.GetString(6);
                    }
                    connection.Close();
                    return View(userProfile);
                }
                catch (Exception)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
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
            if (Session["userId"] != null)
            {
                return View();
            } else
            {
                return RedirectToAction("Login", "Login");
            }
        }
        public ActionResult ChangepasswordValidation(String cur_pass, string new_pass)
        {
            MySqlConnection connection = new MySqlConnection("Server=localhost;Database=dss;Uid=dsstrade;Pwd=user;");
            MySqlCommand cmd;
            connection.Open();
            try
            {
                cmd = connection.CreateCommand();
                cmd.CommandText = "update register set Password=@newpass WHERE UserId=@userid &&Password=@pass";
                cmd.Parameters.AddWithValue("@userid", Session["userId"].ToString());
                cmd.Parameters.AddWithValue("@pass", cur_pass);
                cmd.Parameters.AddWithValue("@newpass", new_pass);
                int isUpdateSuccess = cmd.ExecuteNonQuery();
                

                if(isUpdateSuccess > 0) 
                {
                    return RedirectToAction("Welcome", "Details");
                } else
                {
                    return RedirectToAction("Index", "Home");
                }

            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Home");
            }

        }
        public ActionResult MyInvestment()
        {
            if (Session["userId"] != null)
            {
                MySqlConnection connection = new MySqlConnection("Server=localhost;Database=dss;Uid=dsstrade;Pwd=user;");
                MySqlCommand cmd;
                connection.Open();
                List<MyInvestment> investment = new List<MyInvestment>();
                try
                {
                    cmd = connection.CreateCommand();
                    //cmd.CommandText = "INSERT INTO Register(RefferalId,RefferalName,FirstName,LastName,Password,ConfirmPassword,Email,PhoneNo)VALUES(\"sd\",\"fdf\",\"dfdsf\",\"dfd\",\"sdf\",\"fdsf\",\"dfd\",\"dfdf\")";
                    cmd.CommandText = "SELECT transactionid,packagecount,packageamount,date from investment where userid=@uid";
                    cmd.Parameters.AddWithValue("@uid", Session["userId"].ToString());
                    cmd.ExecuteNonQuery();
                    MySqlDataReader sqlDataReader = cmd.ExecuteReader();
                    int i = 1;
                    while (sqlDataReader.Read())
                    {
                        investment.Add(new MyInvestment
                        {
                            list = i,
                            transactionId = sqlDataReader.GetString(0),
                            NoOfPackage = sqlDataReader.GetInt32(1),
                            Amount = sqlDataReader.GetInt32(2) * sqlDataReader.GetInt32(1),
                            transactionDate = sqlDataReader.GetString(3)
                    });
                        
                        i++;
                    }
                    ViewBag.Investment = investment.ToArray();
                    ViewBag.count = i-1;
                    return View(ViewBag);
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
            } else
            {
                return RedirectToAction("Index", "Home");
            }
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
            MySqlConnection connection = new MySqlConnection("Server=localhost;Database=dss;Uid=dsstrade;Pwd=user;");
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
            var val = Request.QueryString;
            int amount = Convert.ToInt32(val["count"]);
            List<Payout> payouts = new List<Payout>();
            int initialValue = 0;
            int count = Convert.ToInt32(val["count"]);
            for(int i=0; i<23; i++)
            {
                String tableFinal = "table" + i;
                if(i < 8)
                {
                    initialValue = initialValue + (50 * count);
                } else
                {
                    initialValue = initialValue + (100 * count);
                }
                payouts.Add(new Payout
                {
                    table = initialValue
                }) ;

            }
            ViewBag.payout = payouts.ToArray();
            return View(ViewBag);
        }
        public ActionResult ViewBankDetail()
        {
            return View();
        }
        public ActionResult ViewBankInfo(string userid)
        {
            if (Session["userId"] != null)
            {
                MySqlConnection connection = new MySqlConnection("Server=localhost;Database=dss;Uid=dsstrade;Pwd=user;");
                MySqlCommand cmd;
                connection.Open();
                try
                {
                    cmd = connection.CreateCommand();
                    cmd.CommandText = "select bankname,branchname,accountno,ifsc,accountholder,gpay,phphe from bankdetails WHERE UserId=@userid";
                    cmd.Parameters.AddWithValue("@userid", userid);
                    cmd.ExecuteNonQuery();
                    UserProfile userProfile = new UserProfile();
                    MySqlDataReader sqlDataReader1 = cmd.ExecuteReader();
                    while (sqlDataReader1.Read())
                    {
                        userProfile.BankName = sqlDataReader1.GetString(0);
                        userProfile.Branch = sqlDataReader1.GetString(1);
                        userProfile.AccountNo = sqlDataReader1.GetString(2);
                        userProfile.IFSC = sqlDataReader1.GetString(3);
                        userProfile.AccountHolder = sqlDataReader1.GetString(4);
                        userProfile.GooglePay = sqlDataReader1.GetString(5);
                        userProfile.PhonePe = sqlDataReader1.GetString(6);
                    }
                    connection.Close();
                    return View(userProfile);
                }
                catch (Exception)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }
        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }
        public ActionResult InvestmentAdd(string userid, string packages)
        {
            MySqlConnection connection = new MySqlConnection("Server=localhost;Database=dss;Uid=dsstrade;Pwd=user");
            MySqlCommand cmd;
            connection.Open();
            try
            {
                cmd = connection.CreateCommand();
                cmd.CommandText = "SELECT TransactionId FROM Investment ORDER BY TransactionId DESC LIMIT 1;";
                int count = cmd.ExecuteNonQuery();
                MySqlDataReader sqldatareader = cmd.ExecuteReader();
                string transaction= null;
                DateTime aDate = DateTime.Now;
                while (sqldatareader.Read())
                {
                    transaction = sqldatareader.GetString(0);
                }
                if (transaction == null)
                {
                    transaction = "TX78600000000001";
                }
                else
                {
                    String[] spearator = { "TX" };
                    String[] strlist = transaction.Split(spearator, StringSplitOptions.RemoveEmptyEntries);
                    long finalVal = Convert.ToInt64(strlist[0]) + 1;
                    Debug.WriteLine(finalVal);
                    transaction = string.Concat("TX", finalVal);
                    Debug.WriteLine(transaction);
                }
                connection.Close();
                connection.Open();
                //cmd.CommandText = "INSERT INTO Register(RefferalId,RefferalName,FirstName,LastName,Password,ConfirmPassword,Email,PhoneNo)VALUES(\"sd\",\"fdf\",\"dfdsf\",\"dfd\",\"sdf\",\"fdsf\",\"dfd\",\"dfdf\")";
                cmd.CommandText = "INSERT INTO Investment(TransactionId,userId,packageCount,PackageAmount,Date)VALUES(@tid,@uid,@packagecount,@packageamount,@date)";
                cmd.Parameters.AddWithValue("@tid", transaction);
                cmd.Parameters.AddWithValue("@uid", userid);
                cmd.Parameters.AddWithValue("@packagecount", packages);
                cmd.Parameters.AddWithValue("@packageamount", 1300);
                cmd.Parameters.AddWithValue("@date", aDate.ToString("MM/dd/yyyy"));
                cmd.ExecuteNonQuery();
                connection.Close();
                return RedirectToAction("Login", "Login");
            }
            catch (Exception)
            {
                return RedirectToAction("Register", "Login");
            }
        }
    }
}