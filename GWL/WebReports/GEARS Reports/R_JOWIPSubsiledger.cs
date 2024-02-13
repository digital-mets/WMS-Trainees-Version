using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using DevExpress.XtraReports;
using System.Web;
using System.Data;
using GearsLibrary;

namespace GWL.WebReports.GEARS_Reports
{
    public partial class R_JOWIPSubsiledger : DevExpress.XtraReports.UI.XtraReport
    {
        public R_JOWIPSubsiledger()
        {
            InitializeComponent();

            if (HttpContext.Current.Session["ConnString"] != null)
                sqlDataSource1.Connection.ConnectionString = HttpContext.Current.Session["ConnString"].ToString();

            DevExpress.DataAccess.Sql.SqlDataSource.DisableCustomQueryValidation = true;

            DataTable dtmonth = Gears.RetriveData2("SELECT CONVERT (int, Value) AS Month FROM IT.SystemSettings WHERE Code = 'CMONTH'", HttpContext.Current.Session["ConnString"].ToString());
            DataRow _ret = dtmonth.Rows[0];
            Month.Value = _ret[0].ToString();

            DataTable dtyear = Gears.RetriveData2("SELECT CONVERT (int, Value) as Year FROM IT.SystemSettings WHERE Code = 'CYEAR'", HttpContext.Current.Session["ConnString"].ToString());
            DataRow _ret2 = dtyear.Rows[0];
            Year.Value = _ret2[0].ToString();

            //this.GLAccount.Value = HttpContext.Current.Session["ConnString"].ToString();

        }

        //private void R_ServiceOrderSubsi_DataSourceDemanded(object sender, EventArgs e)
        //{
            //XtraReport report = (XtraReport)Report;
            //string productcategory = "";

            //Business Sub Class
            //string[] paramValues = report.Parameters["ProductCategory"].Value as string[];
            //for (int i = 0; i < paramValues.Length; i++)
            //{
            //    productcategory += paramValues[i].ToString();
            //    if (i < paramValues.Length - 1)
            //        productcategory += ",";
            //}

            //if (report.Parameters["ProductCategory"].Value.ToString() == "System.String[]")
            //{
            //    report.Parameters["ProductCategory"].Value = productcategory;
            //}

            //if (report.Parameters["SVODue"].Value.ToString() == "YES")
            //{ 
            //    Detail1.Visible = false;
            //    Detail3.Visible = false;
            //    Detail4.Visible = false;
            //    Detail2.Visible = true;
            //}
            //if (report.Parameters["ItemCode"].Value.ToString() == "YES")
            //{ 
            //    Detail1.Visible = false;
            //    Detail2.Visible = false;
            //    Detail4.Visible = false;
            //    Detail3.Visible = true;
            //}
            //if (report.Parameters["SVODue"].Value.ToString() == "NO" && report.Parameters["ItemCode"].Value.ToString() == "NO")
            //{ 
            //    Detail2.Visible = false;
            //    Detail3.Visible = false;
            //    Detail4.Visible = false;
            //    Detail1.Visible = true;
          
            //}
            //if (report.Parameters["SVODue"].Value.ToString() == "YES" && report.Parameters["ItemCode"].Value.ToString() == "YES")
            //{ 
            //    Detail4.Visible = true;
            //    Detail2.Visible = false;
            //    Detail3.Visible = false;
            //    Detail1.Visible = false;
            //}
        //}



    


    }
}
