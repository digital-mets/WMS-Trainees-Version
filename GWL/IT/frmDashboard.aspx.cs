using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;
using DevExpress.DataAccess;
using DevExpress.DataAccess.ConnectionParameters;
using GearsLibrary;
using Entity;
using System.Data;
using DevExpress.Data.Filtering;
using GearsWarehouseManagement;
using System.Web.Script.Services;
using System.Web.Services;
using System.Configuration;
using System.Globalization;
using System.Data.SqlClient;

namespace GWL
{
    public partial class frmDashboard : System.Web.UI.Page
    {
        public static string strTotalProj;
        protected void Page_Load(object sender, EventArgs e)
        {
            string schemaname = Session["schemaname"].ToString();

            DataTable dtblPO = Gears.RetriveData2(" SELECT COUNT(DISTINCT Docnumber) OpenProj " +
                                          " FROM Procurement.PurchaseOrder MS       " +
                                          " WHERE    " +
                                          " (ISNULL(MS.SubmittedBy,'') = '')"

      , Session["ConnString"].ToString());
            if (dtblPO.Rows.Count > 0)
            {
                strTotalProj = dtblPO.Rows[0][0].ToString();
            }
            else
            {
                strTotalProj = "0";
            }

        }

        public class Dashboards
        {

            public string RoleID { get; set; }
            public string DashboardName { get; set; }
            public string Caption { get; set; }
            public string Active { get; set; }
          
        }


        [WebMethod]
        public static string GetProjects(string name, string value)
        {
            string strReturn = "0";
            strReturn = strTotalProj;
            return strReturn;
        }


        [WebMethod]
        public static List<Dashboards> RetrieveDashboard()
        {
            
            string UseriD = HttpContext.Current.Session["userid"].ToString();

            List<Dashboards> GetDashboard = new List<Dashboards>();
            SqlConnection con;
            using (con = new SqlConnection(HttpContext.Current.Session["ConnString"].ToString()))
            {
                con.Open();
                string query = " EXEC  dbo.sp_Dashboard_DefaultDashboard '" + HttpContext.Current.Session["userid"] + "'";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader reader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                while (reader.Read())
                {
                    GetDashboard.Add(new Dashboards
                    {
                        RoleID = reader["RoleID"].ToString(),
                        DashboardName = reader["DashboardName"].ToString(),
                        Caption = reader["Caption"].ToString(),
                        Active = reader["Active"].ToString()

                    });
                }
                return GetDashboard;
            }
        }


    }
}