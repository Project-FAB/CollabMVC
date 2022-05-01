using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LoginMVC.Models;
using LoginMVC.futureDLL;
using System.Data.SqlClient;
using System.Web.Mvc;
using System.Data;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
namespace LoginMVC.Controllers
{
    public class HomeController : Controller
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
        // GET: Account
        public ActionResult Home()
        {
            if (Session["SessionID"] == null)
            {
                return Redirect("../Login/Index");
            }
            else
            {
                string xName = "";
                string xValuesTR = "";
                string xValuesTD = "";
                int ctr = 1;
             
                DataTable dt = dl.getTableValue("SELECT Code, Description FROM RM.TEST", makeNewConnection());
                DataTable dtName = dl.getTableValue("SELECT column_name FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = N'test' and table_schema = 'RM'", makeNewConnection());
                int xNameLen = dtName.Columns.Count;

                if (dt.Rows.Count > 0 && dtName.Rows.Count > 0)
                {
                    foreach (DataRow dtRowCol in dtName.Rows)
                    {
                        xName += "<th>" + dtRowCol[0].ToString() + "</th>";
                    }

                    foreach (DataRow dtRowData in dt.Rows)
                    {
                        xValuesTD = "";
                        if (ctr == 1)
                        {
                            xValuesTR += "<tr>" + xName + "</tr>";
                        }
                            for (int x = 0; x <= xNameLen; x++)
                            {
                               xValuesTD += "<td>" + dtRowData[x].ToString()+"</td>";
                            }
                        xValuesTR += "<tr>" + xValuesTD + "</tr>";
                        ctr++;
                    }
                }

                    var info = new AccountInfo() {accountID = Session["accountID"].ToString(), sessionID = Session["SessionID"].ToString() };
                    Session["Datatable"] = xValuesTR;
                    return View(info);
            }
        }
        public ActionResult Logout()
        {
            Session.Remove("username");
            return RedirectToAction("Index");
        }

        //sample Save
        [HttpPost]
        public ActionResult Save(Login avm)
        {

            try
            {
                sqlCon.ConnectionString = connection;
                sqlCon.Open();
                sqlTran = sqlCon.BeginTransaction();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = sqlCon;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Transaction = sqlTran; // Need to specify for every command
                cmd.Parameters.Clear();
                cmd.CommandText = "SPName";
                cmd.Parameters.AddWithValue("@name", "name");
                cmd.Parameters.AddWithValue("@lastname", "lastname");
                cmd.Parameters.AddWithValue("@QueryType", 1);
                cmd.ExecuteNonQuery();
            }
            catch (Exception err)
            {

            }
            return View("Home");
        }

    }
}