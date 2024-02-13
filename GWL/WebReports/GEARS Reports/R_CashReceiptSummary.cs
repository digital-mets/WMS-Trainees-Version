using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using DevExpress.XtraReports;
using System.Web;

namespace GWL.WebReports.GEARS_Reports
{
    public partial class R_CashReceiptSummary : DevExpress.XtraReports.UI.XtraReport
    {
        public R_CashReceiptSummary()
        {
            InitializeComponent();

            if (HttpContext.Current.Session["ConnString"] != null)
                sqlDataSource1.Connection.ConnectionString = HttpContext.Current.Session["ConnString"].ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //R_BizPartnerLedger rep = new R_BizPartnerLedger();
            //rep.ShowPreviewDialog();
        }

        private void ReportHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }

        private void xrTableCell19_HtmlItemCreated(object sender, HtmlEventArgs e)
        {
            e.ContentCell.Style.Add("border-left", "1px solid #000000 !important");
            e.ContentCell.Style.Add("border-bottom", "1px solid #dcdcdc !important");   
        }

        private void xrTableCell23_HtmlItemCreated(object sender, HtmlEventArgs e)
        {
            e.ContentCell.Style.Add("border-right", "1px solid #000000 !important");
            e.ContentCell.Style.Add("border-bottom", "1px solid #dcdcdc !important");  
        }

        private void xrTableCell21_HtmlItemCreated(object sender, HtmlEventArgs e)
        {
            e.ContentCell.Style.Add("border-top", "1px solid #000000 !important");
            e.ContentCell.Style.Add("border-right", "1px solid #000000 !important");
            e.ContentCell.Style.Add("border-bottom", "3px double #000000 !important");  
        }



    }
}
