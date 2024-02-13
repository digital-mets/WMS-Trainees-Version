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

namespace GWL
{
    public partial class frmDashboard_IT : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                dbvMainViewer.DashboardSource = "";

                DataTable DTdash = Gears.RetriveData2(
                    "SELECT Description, Field1 AS URL FROM IT.Genericlookup " +
                    " WHERE lookupkey = 'DASHB' AND ISNULL(IsInactive,0) = 0 ORDER BY Code",
                    Session["ConnString"].ToString());

                DropDownList1.DataSource = DTdash;
                DropDownList1.DataBind();
                DropDownList1.DataTextField = "Description";
                DropDownList1.DataValueField = "URL";
                DropDownList1.DataBind();
            }

            dbvMainViewer.DashboardSource = DropDownList1.SelectedValue.ToString();
                    
        }

        protected void dbvMainViewer_ConfigureDataConnection(object sender, DevExpress.DashboardWeb.ConfigureDataConnectionWebEventArgs e)
        {

            System.Data.SqlClient.SqlConnectionStringBuilder sql = new System.Data.SqlClient.SqlConnectionStringBuilder();
            sql.ConnectionString = Session["ConnString"].ToString();
            
            SqlServerConnectionParametersBase s = (SqlServerConnectionParametersBase)e.ConnectionParameters;

            s.ServerName = sql.DataSource;
            s.DatabaseName = sql.InitialCatalog;
            s.UserName = sql.UserID;
            s.Password = sql.Password;
            e.ConnectionParameters = s;
        }

        protected void dbvMainViewer_CustomParameters(object sender, DevExpress.DashboardWeb.CustomParametersWebEventArgs e)
        {
            string strDummy = e.DashboardId;
        }

        protected void dbvMainViewer_DashboardLoaded(object sender, DevExpress.DashboardWeb.DashboardLoadedWebEventArgs e)
        {
            DateTime dteRefMonthEnd = DateTime.Today.AddDays(-DateTime.Today.Day);
            if (e.DashboardId.Contains("ResponseTime.xml") 
                || e.DashboardId.Contains("ResolutionTime.xml")
                || e.DashboardId.Contains("OverdueTicket.xml"))
            {
                e.Dashboard.Parameters["Year"].Value = dteRefMonthEnd.Year;
                //e.Dashboard.Parameters["MonthFr"].Value = dteRefMonthEnd.Month;
                e.Dashboard.Parameters["MonthTo"].Value = dteRefMonthEnd.Month;
            }
            else if (e.DashboardId.Contains("Project Budget.xml")
                     || e.DashboardId.Contains("Utilization - Company.xml")
                     || e.DashboardId.Contains("Utilization - Team.xml"))
            {
                e.Dashboard.Parameters["DateFr"].Value = Convert.ToDateTime(dteRefMonthEnd.Year.ToString() + "-01-01");
                e.Dashboard.Parameters["DateTo"].Value = dteRefMonthEnd;
            }
            else if (e.DashboardId.Contains("Project Revenue.xml"))
            {
                e.Dashboard.Parameters["Year"].Value = DateTime.Today.Year;
                e.Dashboard.Parameters["MonthFr"].Value = 1;
                e.Dashboard.Parameters["MonthTo"].Value = DateTime.Today.Month;
            }
            else if (e.DashboardId.Contains("Ticket Count By Category.xml")
                     || e.DashboardId.Contains("Ticket Count By Company.xml")
                     || e.DashboardId.Contains("Ticket Trend By Month"))
            {
                if (DateTime.Today.Month > 1)
                {
                    e.Dashboard.Parameters["Year"].Value = DateTime.Today.Year;
                    e.Dashboard.Parameters["Month"].Value = DateTime.Today.Month - 1;
                }
                else
                {
                    e.Dashboard.Parameters["Year"].Value = DateTime.Today.Year - 1;
                    e.Dashboard.Parameters["Month"].Value = 12;
                }
            }
            else if (e.DashboardId.Contains("Ticket Trend By Week.xml"))
            {
                e.Dashboard.Parameters["Cutoff"].Value = dteRefMonthEnd;
            }
        }
    }
}