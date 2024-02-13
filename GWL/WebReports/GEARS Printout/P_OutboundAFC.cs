using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Web;
using System.Data;
using GearsLibrary;


namespace GWL.WebReports.GEARS_Printout
{
    public partial class P_OutboundAFC : DevExpress.XtraReports.UI.XtraReport
    {
        public P_OutboundAFC()
        {
            InitializeComponent();
            if (HttpContext.Current.Session["ConnString"] != null)
                sqlDataSource1.Connection.ConnectionString = HttpContext.Current.Session["ConnString"].ToString();

            DataTable dtfullname = Gears.RetriveData2("Select UserName from it.users where UserID='" + HttpContext.Current.Session["userid"].ToString() + "'", HttpContext.Current.Session["ConnString"].ToString());
            DataRow _ret = dtfullname.Rows[0];
            UserID.Value = _ret[0].ToString();
        }

        private void ReportHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }

    }
}
