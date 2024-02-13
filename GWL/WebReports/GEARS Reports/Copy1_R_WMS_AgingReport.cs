using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using DevExpress.XtraReports;
using System.Web;
using DevExpress.XtraReports.Parameters;

namespace GWL.WebReports.GEARS_Reports
{
    public partial class R_WMS_AgingReport : DevExpress.XtraReports.UI.XtraReport
    {
        public R_WMS_AgingReport()
        {
            InitializeComponent();
            if (HttpContext.Current.Session["ConnString"] != null)
                sqlDataSource1.Connection.ConnectionString = HttpContext.Current.Session["ConnString"].ToString();
            
            //DocDateFrom.Value = DateTime.Today.AddDays(-7);
            //DocDateTo.Value = DateTime.Today;
        }

        private void R_WMS_AgingReport_DataSourceDemanded(object sender, EventArgs e)
        {
            //XtraReport report = (XtraReport)Report;

            //string client = "";
            //string warehouse = "";

            //// Brand
            //string[] paramValues2 = report.Parameters["Client"].Value as string[];
            //for (int i = 0; i < paramValues2.Length; i++)
            //{
            //    client += paramValues2[i].ToString();
            //    if (i < paramValues2.Length - 1)
            //        client += ",";
            //}

            //// Warehouse
            //string[] paramValues3 = report.Parameters["WarehouseCode"].Value as string[];
            //for (int i = 0; i < paramValues3.Length; i++)
            //{
            //    warehouse += paramValues3[i].ToString();
            //    if (i < paramValues3.Length - 1)
            //        warehouse += ",";
            //}

            //// Passing Value Of Labels To Parameter.
            //if (report.Parameters["Client"].Value.ToString() == "System.String[]")
            //{
            //    report.Parameters["Client"].Value = client;
            //}
            //// Passing Value Of Labels To Parameter.
            //if (report.Parameters["WarehouseCode"].Value.ToString() == "System.String[]")
            //{
            //    report.Parameters["WarehouseCode"].Value = warehouse;
            //}
        }


    }
}
