using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using DevExpress.XtraReports;
using System.Web;

namespace GWL.WebReports.GEARS_Reports
{
    public partial class R_WMS_ReceivingReportForSupplierDel : DevExpress.XtraReports.UI.XtraReport
    {
        public R_WMS_ReceivingReportForSupplierDel()
        {
            InitializeComponent();
            if (HttpContext.Current.Session["ConnString"] != null)
                sqlDataSource1.Connection.ConnectionString = HttpContext.Current.Session["ConnString"].ToString();
        }

        //private void xrTableRow2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        //{
        //    XRTableRow row = (XRTableRow)sender;
        //    if (P_ICN.Parameters.Value == "MfgDate")
        //    {
        //        row.Cells[3].Text = "Mfg Date";
        //    }
        //    else
        //    {
        //        row.Cells[3].Text = "Expiry Date";
        //    }
//}


    }
}
