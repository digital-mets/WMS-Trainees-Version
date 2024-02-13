using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Web;

namespace GWL.WebReports.GEARS_Printout
{
    public partial class P_PurchaseOrder2 : DevExpress.XtraReports.UI.XtraReport
    {
      
        public P_PurchaseOrder2()
        {

            InitializeComponent();
            if (HttpContext.Current.Session["ConnString"] != null)
                sqlDataSource1.Connection.ConnectionString = HttpContext.Current.Session["ConnString"].ToString();
        }

        private void xrSubreport1_AfterPrint(object sender, EventArgs e)
        {
           //string a= xrTableCell2.Text;

        }

        private void xrTable2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            
            //string a = xrTableCell2.Text;
            //e.Cancel = true;
        }

        private void xrTable1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }

        private void xrTable2_AfterPrint(object sender, EventArgs e)
        {

        }

    }
}
