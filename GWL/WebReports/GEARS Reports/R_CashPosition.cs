using System;
using System.Web;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using DevExpress.XtraReports;

namespace GWL.WebReports.GEARS_Reports
{
    public partial class R_CashPosition : DevExpress.XtraReports.UI.XtraReport
    {
        public R_CashPosition()
        {
            InitializeComponent();
            if (HttpContext.Current.Session["ConnString"] != null)
                sqlDataSource1.Connection.ConnectionString = HttpContext.Current.Session["ConnString"].ToString();
        }

        private void ReportHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }

        private void xrTableCell51_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }

        private void xrTableCell59_SummaryCalculated(object sender, TextFormatEventArgs e)
        {

        }
        private void xrTableCell59_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            
        }

        private void xrTableCell59_SummaryReset(object sender, EventArgs e)
        {
           
        }

        private void xrTableCell59_SummaryRowChanged(object sender, EventArgs e)
        {
           
        }

    }
}
