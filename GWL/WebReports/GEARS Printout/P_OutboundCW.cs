using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Web;


namespace GWL.WebReports.GEARS_Printout
{
    public partial class P_OutboundCW : DevExpress.XtraReports.UI.XtraReport
    {
        public P_OutboundCW()
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
