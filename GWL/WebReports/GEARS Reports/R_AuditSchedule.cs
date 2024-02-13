using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using DevExpress.XtraReports;
using System.Web;

namespace GWL.WebReports.GEARS_Reports
{
    public partial class R_AuditSchedule : DevExpress.XtraReports.UI.XtraReport
    {
        public R_AuditSchedule()
        {
            InitializeComponent();

            if (HttpContext.Current.Session["ConnString"] != null)
                sqlDataSource1.Connection.ConnectionString = HttpContext.Current.Session["ConnString"].ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void ReportHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }

        private void xrTableCell19_HtmlItemCreated(object sender, HtmlEventArgs e)
        {
            e.ContentCell.Style.Add("border-left", "1px solid #000000 !important");
            e.ContentCell.Style.Add("border-bottom", "1px solid #dcdcdc !important");
            e.ContentCell.Style.Add("border-right", "1px solid #000000 !important");  
        }

        private void xrTableCell28_HtmlItemCreated(object sender, HtmlEventArgs e)
        {
            e.ContentCell.Style.Add("border-right", "1px solid #000000 !important");
            e.ContentCell.Style.Add("border-bottom", "1px solid #dcdcdc !important");  
        }

        private void xrTableCell20_HtmlItemCreated(object sender, HtmlEventArgs e)
        {

            e.ContentCell.Style.Add("border-bottom", "1px solid #dcdcdc !important");
            e.ContentCell.Style.Add("border-right", "1px solid #000000 !important");  
        }

        private void xrTableCell2_HtmlItemCreated(object sender, HtmlEventArgs e)
        {

            e.ContentCell.Style.Add("border-bottom", "1px solid #dcdcdc !important");
            e.ContentCell.Style.Add("border-right", "1px solid #000000 !important");  
        }

        private void xrTableCell7_HtmlItemCreated(object sender, HtmlEventArgs e)
        {

            e.ContentCell.Style.Add("border-bottom", "1px solid #dcdcdc !important");
            e.ContentCell.Style.Add("border-right", "1px solid #000000 !important");  
        }

        private void xrTableCell23_HtmlItemCreated(object sender, HtmlEventArgs e)
        {

            e.ContentCell.Style.Add("border-bottom", "1px solid #dcdcdc !important");
            e.ContentCell.Style.Add("border-right", "1px solid #000000 !important");  
        }

        private void xrTableCell26_HtmlItemCreated(object sender, HtmlEventArgs e)
        {

            e.ContentCell.Style.Add("border-bottom", "1px solid #dcdcdc !important");
            e.ContentCell.Style.Add("border-right", "1px solid #000000 !important");  
        }

        private void xrTableCell24_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            
           
        }



    }
}
