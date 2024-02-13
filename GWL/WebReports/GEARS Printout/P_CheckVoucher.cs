using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Web;


namespace GWL.WebReports.GEARS_Printout
{
    public partial class P_CheckVoucher : DevExpress.XtraReports.UI.XtraReport
    {
        public P_CheckVoucher ()
        {
            InitializeComponent();
            if (HttpContext.Current.Session["ConnString"] != null)
                sqlDataSource1.Connection.ConnectionString = HttpContext.Current.Session["ConnString"].ToString();


        }

    }
}
