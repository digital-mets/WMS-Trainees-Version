using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using DevExpress.XtraReports;
using System.Web;

namespace GWL.WebReports.GEARS_Reports
{
    public partial class R_JobOrderMaterialsReport : DevExpress.XtraReports.UI.XtraReport
    {
        public R_JobOrderMaterialsReport()
        {
            InitializeComponent();

            if (HttpContext.Current.Session["ConnString"] != null)
                sqlDataSource1.Connection.ConnectionString = HttpContext.Current.Session["ConnString"].ToString();
        }

        private void ReportHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }
        private void R_JobOrderMaterialsReport_DataSourceDemanded(object sender, EventArgs e)
        {
            XtraReport report = (XtraReport)Report;
            xrSubreport1.Visible = Convert.ToBoolean(report.Parameters["ShowDetail"].Value);
            //if (Convert.ToBoolean(report.Parameters["ShowDetail"].Value) == true)
            //{
            //    xrSubreport1.Visible = true;
            //}

            //else
            //{
            //    xrSubreport1.Visible = false;
            //}
            //if (report.Parameters["ShowDetail"].Value = true)
            //{
            
            //}
        }



    }
}
