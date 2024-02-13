using System;
using System.Web;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using DevExpress.XtraReports;
using DevExpress.XtraReports.Parameters;

namespace GWL.WebReports.GEARS_Reports
{
    // 05 - 23 - 2016 LGE Add Page Header Band.
    public partial class R_TopSalesByCustomer : DevExpress.XtraReports.UI.XtraReport
    {
        public R_TopSalesByCustomer()
        {
            InitializeComponent();
            if (HttpContext.Current.Session["ConnString"] != null)
                sqlDataSource1.Connection.ConnectionString = HttpContext.Current.Session["ConnString"].ToString();
        }

        private void ReportHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }

        private void xrTableCell34_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {

        }

        private void xrTableCell17_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            e.Result = Convert.ToDouble(xrTableCell16.Summary.GetResult()) / Convert.ToDouble(xrTableCell16.Summary.GetResult());
            e.Handled = true;
            
        }

        private void R_TopSalesByCustomer_DataSourceDemanded(object sender, EventArgs e)
        {

            XtraReport report = (XtraReport)Report;

            //string datefrom = (string)report.Parameters["DateFrom"].Value;
        }

    }
}
