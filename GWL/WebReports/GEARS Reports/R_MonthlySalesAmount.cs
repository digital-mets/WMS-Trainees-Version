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
    public partial class R_MonthlySalesAmount : DevExpress.XtraReports.UI.XtraReport
    {
        public R_MonthlySalesAmount()
        {
            InitializeComponent();
            if (HttpContext.Current.Session["ConnString"] != null)
                sqlDataSource1.Connection.ConnectionString = HttpContext.Current.Session["ConnString"].ToString();
            DevExpress.DataAccess.Sql.SqlDataSource.DisableCustomQueryValidation = true;
          
           
        }

        private void ReportHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }

        private void xrTableCell51_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }

        //private void xrTableCell26_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        //{
        //    XRTableCell xrTableCell26_ = (XRTableCell)sender;

        //    string parameter = GroupBy.ToString();

        //    if (parameter == "Customer")
        //    {
        //        xrTableCell26_.ForeColor = Color.White;
        //        xrTableCell26_.BackColor = Color.Green;
        //    }
        //}

        //private void xrTableCell26_PrintOnPage(object sender, PrintOnPageEventArgs e)
        //{
        //    XRTableCell xrTableCell26_ = (XRTableCell)sender;

        //    string parameter = GroupBy.ToString();

        //    if (parameter == "Customer")
        //    {
        //        xrTableCell26_.ForeColor = Color.White;
        //        xrTableCell26_.BackColor = Color.Green;
        //    }
        //}

    }
}
