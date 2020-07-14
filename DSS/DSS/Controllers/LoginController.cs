using Microsoft.SqlServer.Server;
using System;
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
            MySqlConnection connection = new MySqlConnection("Server=localhost;Database=dss;Uid=sab;Pwd=user;");
            MySqlCommand cmd;
            connection.Open();
            try
            {
                cmd = connection.CreateCommand();
                cmd.CommandText = "select * from login WHERE username=@user&&pasword=@pass";
                cmd.Parameters.AddWithValue("@user", username);
                cmd.Parameters.AddWithValue("@pass", password);
                cmd.ExecuteNonQuery();
                Debug.WriteLine(cmd);
                MySqlDataReader sqlDataReader = cmd.ExecuteReader();
                if (sqlDataReader.HasRows)
                {
                    System.Web.Security.FormsAuthentication.SetAuthCookie(username, true);
                    Session["IsAuthenticated"] = true;
                    if(username.Equals("Admin786"))
                        return RedirectToAction("Register", "Login");
                    else
                        return RedirectToAction("Welcome", "Details");
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            catch(Exception)
            {
                return RedirectToAction("Index", "Home");
            }
            finally
            {
                if(connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }
        
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RegisterValidation(string sponsor_id, string sponsor_name, string fname, string lname, string pass, string cpass, string email, string phone)
        {
            MySqlConnection connection = new MySqlConnection("Server=localhost;Database=dss;Uid=sab;Pwd=user;");
            MySqlCommand cmd;
            connection.Open();
            try
            {
                cmd = connection.CreateCommand();
                //cmd.CommandText = "INSERT INTO Register(RefferalId,RefferalName,FirstName,LastName,Password,ConfirmPassword,Email,PhoneNo)VALUES(\"sd\",\"fdf\",\"dfdsf\",\"dfd\",\"sdf\",\"fdsf\",\"dfd\",\"dfdf\")";
                cmd.CommandText = "INSERT INTO Register(RefferalId,RefferalName,FirstName,LastName,Password,ConfirmPassword,Email,PhoneNo)VALUES(@rid,@rname,@fname,@lname,@pass,@cpass,@email,@pno)";
                cmd.Parameters.AddWithValue("@rid", sponsor_id);
                cmd.Parameters.AddWithValue("@rname", sponsor_name);
                cmd.Parameters.AddWithValue("@fname", fname);
                cmd.Parameters.AddWithValue("@lname", lname);
                cmd.Parameters.AddWithValue("@pass", pass);
                cmd.Parameters.AddWithValue("@cpass", cpass);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@pno", phone);
                cmd.ExecuteNonQuery();
                return RedirectToAction("Login", "Login");
            }
            catch (Exception)
            {
                return RedirectToAction("Register", "Login");
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }

        }

        public ActionResult ForgotPassword()
        {
            return View();
        }

    }
}