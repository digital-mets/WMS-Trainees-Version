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
    public partial class R_WMS_OCNmonitoring : DevExpress.XtraReports.UI.XtraReport
    {
        public R_WMS_OCNmonitoring()
        {
            InitializeComponent();

            if (HttpContext.Current.Session["ConnString"] != null)
                sqlDataSource1.Connection.ConnectionString = HttpContext.Current.Session["ConnString"].ToString();

            

            //this.GLAccount.Value = HttpContext.Current.Session["ConnString"].ToString();

        }



        private void xrTableCell19_HtmlItemCreated(object sender, HtmlEventArgs e)
        {
            //e.ContentCell.Style.Add("border-left", "1px solid #000000 !important");
            //e.ContentCell.Style.Add("border-bottom", "1px solid #dcdcdc !important");  
        }

        private void xrTableCell27_HtmlItemCreated(object sender, HtmlEventArgs e)
        {
            //e.ContentCell.Style.Add("border-right", "1px solid #000000 !important");
            //e.ContentCell.Style.Add("border-bottom", "1px solid #dcdcdc !important");  
        }

        private void xrTableCell19_HtmlItemCreated_1(object sender, HtmlEventArgs e)
        {
            //e.ContentCell.Style.Add("border-left", "1px solid #000000 !important");  
        }

        private void xrTableCell27_HtmlItemCreated_1(object sender, HtmlEventArgs e)
        {
            //e.ContentCell.Style.Add("border-right", "1px solid #000000 !important");  
        }

        private void ReportHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }

    }
}
