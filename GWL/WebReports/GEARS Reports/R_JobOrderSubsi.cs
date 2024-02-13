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
    public partial class R_JobOrderSubsi : DevExpress.XtraReports.UI.XtraReport
    {
        public R_JobOrderSubsi()
        {
            InitializeComponent();

            if (HttpContext.Current.Session["ConnString"] != null)
                sqlDataSource1.Connection.ConnectionString = HttpContext.Current.Session["ConnString"].ToString();

            DevExpress.DataAccess.Sql.SqlDataSource.DisableCustomQueryValidation = true;

            //this.GLAccount.Value = HttpContext.Current.Session["ConnString"].ToString();

        }

        private void R_ServiceOrderSubsi_DataSourceDemanded(object sender, EventArgs e)
        {
            XtraReport report = (XtraReport)Report;
            string productcategory = "";

            //Business Sub Class
            string[] paramValues = report.Parameters["ProductCategory"].Value as string[];
            for (int i = 0; i < paramValues.Length; i++)
            {
                productcategory += paramValues[i].ToString();
                if (i < paramValues.Length - 1)
                    productcategory += ",";
            }

            if (report.Parameters["ProductCategory"].Value.ToString() == "System.String[]")
            {
                report.Parameters["ProductCategory"].Value = productcategory;
            }

            if (report.Parameters["SVODue"].Value.ToString() == "YES")
            {
                xrTableCell78.Visible = true;
                xrTableCell105.Visible = true;
                xrTableCell105.Text = "JO Due Date";
                xrTableCell106.Visible = false;
                xrTableCell78.WidthF = 67;
                Detail1.Visible = false;
                Detail3.Visible = false;
                Detail4.Visible = false;
                Detail2.Visible = true;
            }
            if (report.Parameters["ItemCode"].Value.ToString() == "YES")
            {
                xrTableCell78.Visible = true;
                xrTableCell105.Visible = true;

                xrTableCell106.Visible = false;
                xrTableCell105.Text = "Stock Code";
                xrTableCell78.WidthF = 67;
                Detail1.Visible = false;
                Detail2.Visible = false;
                Detail4.Visible = false;
                Detail3.Visible = true;
            }
            if (report.Parameters["SVODue"].Value.ToString() == "NO" && report.Parameters["ItemCode"].Value.ToString() == "NO")
            {
                xrTableCell78.Visible = false;
                xrTableCell105.Visible = false;
                xrTableCell106.Visible = false;

                Detail2.Visible = false;
                Detail3.Visible = false;
                Detail4.Visible = false;
                Detail1.Visible = true;
          
            }
            if (report.Parameters["SVODue"].Value.ToString() == "YES" && report.Parameters["ItemCode"].Value.ToString() == "YES")
            {
                xrTableCell105.Text = "JO Due Date";
                xrTableCell106.Text = "Stock Code";
                xrTableCell78.WidthF = 134;
                xrTableCell78.Visible = true;
                xrTableCell105.Visible = true;
                xrTableCell106.Visible = true;
                Detail4.Visible = true;
                Detail2.Visible = false;
                Detail3.Visible = false;
                Detail1.Visible = false;
            }
        }



    


    }
}
