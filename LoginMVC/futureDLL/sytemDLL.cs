using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LoginMVC.Models;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace LoginMVC.futureDLL
{
    public class sytemDLL
    {
        public string connectionString(){
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
                return con.ConnectionString;
        }
        private string SQLTransform(string sql)
        {
                sql = sql.Replace("'", "''");
                sql = string.Format("BEGIN TRAN\r\n                    BEGIN TRY  \r\n                     EXEC(\r\n                    '{0}'\r\n                    )\r\n                    COMMIT\r\n                    END TRY  \r\n                    BEGIN CATCH  \r\n                        ROLLBACK TRAN\r\n                        DECLARE @msg nvarchar(max) = ERROR_MESSAGE() \r\n\t                    RAISERROR (@msg,16,1)\r\n                    END CATCH \r\n                    ", (object)sql);
            return sql;
        }
        public string getSingleValue(string param,string con)
        {
            string valuex = "";
            SqlConnection nwCon = new SqlConnection(con);
            try
            {
                nwCon.Open();
                SqlCommand com = new SqlCommand(SQLTransform(param), nwCon);
                DataTable dt = new DataTable();
                new SqlDataAdapter(com).Fill(dt);

                if (dt.Rows.Count >= 1)
                {
                    valuex = dt.Rows[0][0].ToString();
                }
            }
            catch (Exception err)
            {
                valuex = " error :" + err.Message;
                nwCon.Close();
                nwCon.Dispose();
            }

            return valuex;
        }

        public DataTable getTableValue(string param, string con)
        {
            SqlConnection nwCon = new SqlConnection(con);
            DataTable dt = new DataTable();
            try
            {
                nwCon.Open();
                SqlCommand com = new SqlCommand(SQLTransform(param), nwCon);
                new SqlDataAdapter(com).Fill(dt);
            }
            catch (Exception err)
            {
                nwCon.Close();
                nwCon.Dispose();
            }
            return dt;
        }

        public DataSet getDataSetValue(string param, string con)
        {
            SqlConnection nwCon = new SqlConnection(con);
            DataSet dts = new DataSet();
            try
            {
                nwCon.Open();
                SqlCommand com = new SqlCommand(SQLTransform(param), nwCon);
                new SqlDataAdapter(com).Fill(dts);
            }catch (Exception err)
            {
                nwCon.Close();
                nwCon.Dispose();
            }
            return dts;
        }

        public string ExecuteProcedure(SqlCommand sqlCmd, string con)
        {
            string result;
            SqlConnection nwCon = new SqlConnection(con);
            try
            {
                nwCon.Open();
                int row = sqlCmd.ExecuteNonQuery();
                nwCon.Close();
                result = "Posted Succesfully";
            }
            catch(Exception err)
            {
                result = err.ToString();
            }
            return result;
        }
    }
}