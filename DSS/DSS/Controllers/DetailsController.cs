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
            List<Payout> payouts = new List<Payout>();
            int initialValue = 0;
            var daysCount = 1;
            DateTime dateNow = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            DateTime checkDate = Convert.ToDateTime(val["date"]);
            int count = Convert.ToInt32(val["count"]);
            var sta = "Not Eligible";
            for (int i=0; i<23; i++)
            {
                if(i==1)
                {
                    daysCount = 0;
                }
                if (i!=0 && i < 5)
                {
                    daysCount = daysCount + 10;
                }
                else
                {
                    daysCount = daysCount + 5;
                }
                if (i < 8)
                {
                    initialValue = initialValue + (50 * count);
                } else
                {
                    initialValue = initialValue + (100 * count);
                }
                if (checkDate.Equals(dateNow.AddDays(-weekEndCount(daysCount))))
                {
                    sta = "Eligible";
                }
                else if (checkDate < dateNow.AddDays(-weekEndCount(daysCount)))
                {
                    sta = "Withdrawn";
                }
                else if(checkDate > dateNow.AddDays(-weekEndCount(daysCount)))
                {
                    sta = "Not Eligible";
                }
                payouts.Add(new Payout
                {
                    table = initialValue,
                    status = sta
                }) ;

            }
            int weekEndCount(int counting)
            {
                DateTime currentDate = checkDate;
                var weekCount = 0;
                for (int i=1; i<=counting; i++)
                {
                    if(checkDate.AddDays(+i).DayOfWeek.ToString()=="Sunday" || checkDate.AddDays(+i).DayOfWeek.ToString() == "Saturday")
                    {
                        weekCount++;
                    }
                }
                if (checkDate.AddDays(+(weekCount + counting)).DayOfWeek.ToString() == "Sunday")
                {
                    weekCount++;
                }
                if (checkDate.AddDays(+(weekCount + counting)).DayOfWeek.ToString() == "Saturday")
                {
                    weekCount = weekCount + 2;
                }
                return counting + weekCount;
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
        public ActionResult PayoutDetails()
        {
            if (Session["userId"] != null)
            {
                MySqlConnection connection = new MySqlConnection("Server=localhost;Database=dss;Uid=dsstrade;Pwd=user;");
                MySqlCommand cmd;
                connection.Open();
                try
                {
                    cmd = connection.CreateCommand();
                    cmd.CommandText = "select transactionId, userId, packageCount, packageAmount, Date from investment";
                    cmd.ExecuteNonQuery();
                    List<AdminPayout> adminPayoutList = new List<AdminPayout>();
                    MySqlDataReader sqlDataReader1 = cmd.ExecuteReader();
                    DateTime checkDate;
                    DateTime dateNow = new DateTime(DateTime.Now.Year,DateTime.Now.Month, DateTime.Now.Day);
                    int count = 0;
                    while (sqlDataReader1.Read())
                    {
                        checkDate = Convert.ToDateTime(sqlDataReader1.GetString(4));
                        if (checkDate.Equals(dateNow.AddDays(-weekEndCount(1))))
                        {
                            AdminPayout adminPayout = new AdminPayout();
                            count++;
                            adminPayout.TransactionId = sqlDataReader1.GetString(0);
                            adminPayout.UserId = sqlDataReader1.GetString(1);
                            adminPayout.Days = 1;
                            adminPayout.Amount = 50 * sqlDataReader1.GetInt32(2);
                            adminPayoutList.Add(adminPayout);
                        }
                        else if (checkDate.Equals(dateNow.AddDays(-weekEndCount(10))))
                        {
                            AdminPayout adminPayout = new AdminPayout();
                            count++;
                            adminPayout.TransactionId = sqlDataReader1.GetString(0);
                            adminPayout.UserId = sqlDataReader1.GetString(1);
                            adminPayout.Days = 10;
                            adminPayout.Amount = 100 * sqlDataReader1.GetInt32(2);
                            adminPayoutList.Add(adminPayout);
                        }
                        else if (checkDate.Equals(dateNow.AddDays(-weekEndCount(20))))
                        {
                            AdminPayout adminPayout = new AdminPayout();
                            count++;
                            adminPayout.TransactionId = sqlDataReader1.GetString(0);
                            adminPayout.UserId = sqlDataReader1.GetString(1);
                            adminPayout.Days = 20;
                            adminPayout.Amount = 150 * sqlDataReader1.GetInt32(2);
                            adminPayoutList.Add(adminPayout);
                        }
                        else if (checkDate.Equals(dateNow.AddDays(-weekEndCount(30))))
                        {
                            AdminPayout adminPayout = new AdminPayout();
                            count++;
                            adminPayout.TransactionId = sqlDataReader1.GetString(0);
                            adminPayout.UserId = sqlDataReader1.GetString(1);
                            adminPayout.Days = 30;
                            adminPayout.Amount = 200 * sqlDataReader1.GetInt32(2);
                            adminPayoutList.Add(adminPayout);
                        }
                        else if (checkDate.Equals(dateNow.AddDays(-weekEndCount(60))))
                        {
                            AdminPayout adminPayout = new AdminPayout();
                            count++;
                            adminPayout.Days = 60;
                            adminPayout.TransactionId = sqlDataReader1.GetString(0);
                            adminPayout.UserId = sqlDataReader1.GetString(1);
                            adminPayout.Amount = 250 * sqlDataReader1.GetInt32(2);
                            adminPayoutList.Add(adminPayout);
                        }
                        else if (checkDate.Equals(dateNow.AddDays(-weekEndCount(90))))
                        {
                            AdminPayout adminPayout = new AdminPayout();
                            count++;
                            adminPayout.Days = 90;
                            adminPayout.TransactionId = sqlDataReader1.GetString(0);
                            adminPayout.UserId = sqlDataReader1.GetString(1);
                            adminPayout.Amount = 300 * sqlDataReader1.GetInt32(2);
                            adminPayoutList.Add(adminPayout);
                        }
                        else if (checkDate.Equals(dateNow.AddDays(-weekEndCount(95))))
                        {
                            AdminPayout adminPayout = new AdminPayout();
                            count++;
                            adminPayout.Days = 95;
                            adminPayout.TransactionId = sqlDataReader1.GetString(0);
                            adminPayout.UserId = sqlDataReader1.GetString(1);
                            adminPayout.Amount = 350 * sqlDataReader1.GetInt32(2);
                            adminPayoutList.Add(adminPayout);
                        }
                        else if (checkDate.Equals(dateNow.AddDays(-weekEndCount(100))))
                        {
                            AdminPayout adminPayout = new AdminPayout();
                            count++;
                            adminPayout.Days = 100;
                            adminPayout.TransactionId = sqlDataReader1.GetString(0);
                            adminPayout.UserId = sqlDataReader1.GetString(1);
                            adminPayout.Amount = 400 * sqlDataReader1.GetInt32(2);
                            adminPayoutList.Add(adminPayout);
                        }
                        else if (checkDate.Equals(dateNow.AddDays(-weekEndCount(105))))
                        {
                            AdminPayout adminPayout = new AdminPayout();
                            count++;
                            adminPayout.Days = 105;
                            adminPayout.TransactionId = sqlDataReader1.GetString(0);
                            adminPayout.UserId = sqlDataReader1.GetString(1);
                            adminPayout.Amount = 500 * sqlDataReader1.GetInt32(2);
                            adminPayoutList.Add(adminPayout);
                        }
                        else if (checkDate.Equals(dateNow.AddDays(-weekEndCount(110))))
                        {
                            AdminPayout adminPayout = new AdminPayout();
                            count++;
                            adminPayout.Days = 110;
                            adminPayout.TransactionId = sqlDataReader1.GetString(0);
                            adminPayout.UserId = sqlDataReader1.GetString(1);
                            adminPayout.Amount = 600 * sqlDataReader1.GetInt32(2);
                            adminPayoutList.Add(adminPayout);
                        }
                        else if (checkDate.Equals(dateNow.AddDays(-weekEndCount(115))))
                        {
                            AdminPayout adminPayout = new AdminPayout();
                            count++;
                            adminPayout.Days = 115;
                            adminPayout.TransactionId = sqlDataReader1.GetString(0);
                            adminPayout.UserId = sqlDataReader1.GetString(1);
                            adminPayout.Amount = 700 * sqlDataReader1.GetInt32(2);
                            adminPayoutList.Add(adminPayout);
                        }
                        else if (checkDate.Equals(dateNow.AddDays(-weekEndCount(120))))
                        {
                            AdminPayout adminPayout = new AdminPayout();
                            count++;
                            adminPayout.Days = 120;
                            adminPayout.TransactionId = sqlDataReader1.GetString(0);
                            adminPayout.UserId = sqlDataReader1.GetString(1);
                            adminPayout.Amount = 800 * sqlDataReader1.GetInt32(2);
                            adminPayoutList.Add(adminPayout);
                        }
                        else if (checkDate.Equals(dateNow.AddDays(-weekEndCount(125))))
                        {
                            AdminPayout adminPayout = new AdminPayout();
                            count++;
                            adminPayout.Days = 125;
                            adminPayout.TransactionId = sqlDataReader1.GetString(0);
                            adminPayout.UserId = sqlDataReader1.GetString(1);
                            adminPayout.Amount = 900 * sqlDataReader1.GetInt32(2);
                            adminPayoutList.Add(adminPayout);
                        }
                        else if (checkDate.Equals(dateNow.AddDays(-weekEndCount(130))))
                        {
                            AdminPayout adminPayout = new AdminPayout();
                            count++;
                            adminPayout.Days = 130;
                            adminPayout.TransactionId = sqlDataReader1.GetString(0);
                            adminPayout.UserId = sqlDataReader1.GetString(1);
                            adminPayout.Amount = 1000 * sqlDataReader1.GetInt32(2);
                            adminPayoutList.Add(adminPayout);
                        }
                        else if (checkDate.Equals(dateNow.AddDays(-weekEndCount(135))))
                        {
                            AdminPayout adminPayout = new AdminPayout();
                            count++;
                            adminPayout.Days = 135;
                            adminPayout.TransactionId = sqlDataReader1.GetString(0);
                            adminPayout.UserId = sqlDataReader1.GetString(1);
                            adminPayout.Amount = 1100 * sqlDataReader1.GetInt32(2);
                            adminPayoutList.Add(adminPayout);
                        }
                        else if (checkDate.Equals(dateNow.AddDays(-weekEndCount(140))))
                        {
                            AdminPayout adminPayout = new AdminPayout();
                            count++;
                            adminPayout.Days = 140;
                            adminPayout.TransactionId = sqlDataReader1.GetString(0);
                            adminPayout.UserId = sqlDataReader1.GetString(1);
                            adminPayout.Amount = 1200 * sqlDataReader1.GetInt32(2);
                            adminPayoutList.Add(adminPayout);
                        }
                        else if (checkDate.Equals(dateNow.AddDays(-weekEndCount(145))))
                        {
                            AdminPayout adminPayout = new AdminPayout();
                            count++;
                            adminPayout.Days = 145;
                            adminPayout.TransactionId = sqlDataReader1.GetString(0);
                            adminPayout.UserId = sqlDataReader1.GetString(1);
                            adminPayout.Amount = 1300 * sqlDataReader1.GetInt32(2);
                            adminPayoutList.Add(adminPayout);
                        }
                        else if (checkDate.Equals(dateNow.AddDays(-weekEndCount(150))))
                        {
                            AdminPayout adminPayout = new AdminPayout();
                            count++;
                            adminPayout.Days = 150;
                            adminPayout.TransactionId = sqlDataReader1.GetString(0);
                            adminPayout.UserId = sqlDataReader1.GetString(1);
                            adminPayout.Amount = 1400 * sqlDataReader1.GetInt32(2);
                            adminPayoutList.Add(adminPayout);
                        }
                        else if (checkDate.Equals(dateNow.AddDays(-weekEndCount(155))))
                        {
                            AdminPayout adminPayout = new AdminPayout();
                            count++;
                            adminPayout.Days = 155;
                            adminPayout.TransactionId = sqlDataReader1.GetString(0);
                            adminPayout.UserId = sqlDataReader1.GetString(1);
                            adminPayout.Amount = 1500 * sqlDataReader1.GetInt32(2);
                            adminPayoutList.Add(adminPayout);
                        }
                        else if (checkDate.Equals(dateNow.AddDays(-weekEndCount(160))))
                        {
                            AdminPayout adminPayout = new AdminPayout();
                            count++;
                            adminPayout.Days = 160;
                            adminPayout.TransactionId = sqlDataReader1.GetString(0);
                            adminPayout.UserId = sqlDataReader1.GetString(1);
                            adminPayout.Amount = 1600 * sqlDataReader1.GetInt32(2);
                            adminPayoutList.Add(adminPayout);
                        }
                        else if (checkDate.Equals(dateNow.AddDays(-weekEndCount(165))))
                        {
                            AdminPayout adminPayout = new AdminPayout();
                            count++;
                            adminPayout.Days = 165;
                            adminPayout.TransactionId = sqlDataReader1.GetString(0);
                            adminPayout.UserId = sqlDataReader1.GetString(1);
                            adminPayout.Amount = 1700 * sqlDataReader1.GetInt32(2);
                            adminPayoutList.Add(adminPayout);
                        }
                        else if (checkDate.Equals(dateNow.AddDays(-weekEndCount(170))))
                        {
                            AdminPayout adminPayout = new AdminPayout();
                            count++;
                            adminPayout.Days = 170;
                            adminPayout.TransactionId = sqlDataReader1.GetString(0);
                            adminPayout.UserId = sqlDataReader1.GetString(1);
                            adminPayout.Amount = 1800 * sqlDataReader1.GetInt32(2);
                            adminPayoutList.Add(adminPayout);
                        }
                        else if (checkDate.Equals(dateNow.AddDays(-weekEndCount(175))))
                        {
                            AdminPayout adminPayout = new AdminPayout();
                            count++;
                            adminPayout.Days = 175;
                            adminPayout.TransactionId = sqlDataReader1.GetString(0);
                            adminPayout.UserId = sqlDataReader1.GetString(1);
                            adminPayout.Amount = 1900 * sqlDataReader1.GetInt32(2);
                            adminPayoutList.Add(adminPayout);
                        }
                        else if (checkDate.Equals(dateNow.AddDays(-weekEndCount(180))))
                        {
                            AdminPayout adminPayout = new AdminPayout();
                            count++;
                            adminPayout.Days = 180;
                            adminPayout.TransactionId = sqlDataReader1.GetString(0);
                            adminPayout.UserId = sqlDataReader1.GetString(1);
                            adminPayout.Amount = 2000 * sqlDataReader1.GetInt32(2);
                            adminPayoutList.Add(adminPayout);
                        }
                    }
                    int weekEndCount(int counting)
                    {
                        DateTime currentDate = checkDate;
                        var weekCount = 0;
                        for (int i = 1; i <= counting; i++)
                        {
                            if (checkDate.AddDays(+i).DayOfWeek.ToString() == "Sunday" || checkDate.AddDays(+i).DayOfWeek.ToString() == "Saturday")
                            {
                                weekCount++;
                            }
                        }
                        if(checkDate.AddDays(+(weekCount+counting)).DayOfWeek.ToString() == "Sunday")
                        {
                            weekCount++;
                        }
                        if (checkDate.AddDays(+(weekCount + counting)).DayOfWeek.ToString() == "Saturday")
                        {
                            weekCount = weekCount + 2;
                        }
                            return counting + weekCount;
                    }
                    connection.Close();
                    ViewBag.adminPayoutList = adminPayoutList.ToArray();
                    ViewBag.count = count;
                    return View(ViewBag);
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
                cmd.Parameters.AddWithValue("@date", aDate.ToString("yyyy/MM/dd"));
                cmd.ExecuteNonQuery();
                connection.Close();
                return RedirectToAction("Login", "Login");
            }
            catch (Exception)
            {
                return RedirectToAction("Register", "Login");
            }
        }
        public ActionResult DisplayId(UserProfile userProfile)
        {
            return View(userProfile);
        }
        public ActionResult Downline()
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
                    cmd.CommandText = "SELECT myrefferalId from register where userid=@uid";
                    cmd.Parameters.AddWithValue("@uid", Session["userId"].ToString());
                    cmd.ExecuteNonQuery();
                    MySqlDataReader sqlDataReader = cmd.ExecuteReader();
                    string refferalId = "";
                    while (sqlDataReader.Read())
                    {
                        refferalId = sqlDataReader.GetString(0);
                    }
                    connection.Close();
                    connection.Open();
                    cmd = connection.CreateCommand();
                    cmd.CommandText = "SELECT firstname, lastname, email from register where refferalId=@rid";
                    cmd.Parameters.AddWithValue("@rid", refferalId);
                    cmd.ExecuteNonQuery();
                    MySqlDataReader sqlDataReader1 = cmd.ExecuteReader();
                    int i = 1;
                    List<UserProfile> userProfileList = new List<UserProfile>();
                    while (sqlDataReader1.Read())
                    {
                        UserProfile userProfile = new UserProfile();
                        userProfile.FirstName = sqlDataReader1.GetString(0);
                        userProfile.LastName = sqlDataReader1.GetString(1);
                        userProfile.Email = sqlDataReader1.GetString(2);
                        userProfile.list = i;
                        i++;
                        userProfileList.Add(userProfile);
                    }
                    ViewBag.userProfile = userProfileList.ToArray();
                    ViewBag.count = i - 1;
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
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        public ActionResult CustomerDetails()
        {
            if (Session["userId"] != null)
            {
                MySqlConnection connection = new MySqlConnection("Server=localhost;Database=dss;Uid=dsstrade;Pwd=user;");
                MySqlCommand cmd;
                connection.Open();
                List<UserProfile> userProfileList = new List<UserProfile>();
                try
                {
                    cmd = connection.CreateCommand();
                    cmd.CommandText = "SELECT UserId,RefferalId,RefferalName,FirstName,LastName,Password,Email,PhoneNo,AadharNo,PanNo,MyRefferalId from register where userid != @admin";
                    cmd.Parameters.AddWithValue("@admin", "Admin786");
                    cmd.ExecuteNonQuery();
                    MySqlDataReader sqlDataReader = cmd.ExecuteReader();
                    int i = 1;
                    while (sqlDataReader.Read())
                    {
                        UserProfile userProfile = new UserProfile();
                        userProfile.UserId = sqlDataReader.GetString(0);
                        userProfile.RefferalId = sqlDataReader.GetString(1);
                        userProfile.RefferalName = sqlDataReader.GetString(2);
                        userProfile.FirstName = sqlDataReader.GetString(3);
                        userProfile.LastName = sqlDataReader.GetString(4);
                        userProfile.Password = sqlDataReader.GetString(5);
                        userProfile.Email = sqlDataReader.GetString(6);
                        userProfile.PhoneNo = sqlDataReader.GetString(7);
                        userProfile.AadharNo = sqlDataReader.GetString(8);
                        userProfile.PanNo = sqlDataReader.GetString(9);
                        userProfile.MyRefferalId = sqlDataReader.GetString(10);
                        userProfile.list = i;

                        i++;
                        userProfileList.Add(userProfile);
                    }
                    ViewBag.userProfile = userProfileList.ToArray();
                    ViewBag.count = i - 1;
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
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}