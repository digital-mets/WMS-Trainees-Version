using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Web;

namespace GWL.WebReports.GEARS_Reports
{
    public partial class R_CostingReport : DevExpress.XtraReports.UI.XtraReport
    {
        public R_CostingReport()
        {
            InitializeComponent();
            if (HttpContext.Current.Session["ConnString"] != null)
                sqlDataSource1.Connection.ConnectionString = HttpContext.Current.Session["ConnString"].ToString();
        }

        private void R_CostingReport_DataSourceDemanded(object sender, EventArgs e)
        {
            //XtraReport report = (XtraReport)Report;
            //xrSubreport1.Visible = Convert.ToBoolean(report.Parameters["ShowTRR"].Value);
            //xrSubreport1.ReportSource = "";
          
        }

        private void xrSubreport1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XtraReport report = (XtraReport)Report;
            ((XRSubreport)sender).ReportSource.Parameters["ItemCategory"].Value = report.Parameters["ItemCategory"].Value;
            ((XRSubreport)sender).ReportSource.Parameters["ItemCode"].Value = report.Parameters["ItemCode"].Value;
            ((XRSubreport)sender).ReportSource.Parameters["Color"].Value = report.Parameters["Color"].Value;
            ((XRSubreport)sender).ReportSource.Parameters["Class"].Value = report.Parameters["Class"].Value;
            ((XRSubreport)sender).ReportSource.Parameters["Size"].Value = report.Parameters["Size"].Value;
            ((XRSubreport)sender).ReportSource.Parameters["DateFrom"].Value = report.Parameters["DateFrom"].Value;
        }

    }
}
