using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LoginMVC.Models;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using LoginMVC.futureDLL;
namespace LoginMVC.Controllers
{
    public class LoginController : Controller
    {
        sytemDLL dl = new sytemDLL();

        #region Public variable
        public string connection = "";
        private SqlConnection sqlCon = new SqlConnection();
        private SqlTransaction sqlTran;
        #endregion
        #region Set Connection
        public string makeNewConnection()
        {
           return connection = dl.connectionString().ToString();
        }
        #endregion
        [HttpGet]
        public ActionResult Index()
        {
            if (Session["ID"] == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Homepage");
            }
        }
        [HttpPost]
        public ActionResult Login(Login avm)
        {
                DataTable name = dl.getTableValue("SELECT Code, Description FROM RM.TEST", makeNewConnection());
                if (name.Rows.Count > 0)
                {
                Session["Fullname"] = name.Rows[0]["Description"].ToString();
                Session["SessionID"] = Session.SessionID;
                Session["accountID"] = name.Rows[0]["Code"].ToString();
                return RedirectToAction("Home","Home");
                }
                else
                {
                    ViewBag.Error = "Account is Invalid";
                    return View("Index");
                }          
        }

        public ActionResult Homepage()
        {
            string name = Session["Fullname"].ToString();
            if (name != "" || !name.Contains("error"))
            {
                Session["Fullname"] = name;
                return Redirect("../Home/Home");
            }
            else
            {
                return View("Index");
            }
        }   
    }
}