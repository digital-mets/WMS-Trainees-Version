using System;
using System.Web;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace GWL.WebReports.GEARS_Printout
{
    public partial class P_JOMaterialsRequest : DevExpress.XtraReports.UI.XtraReport
    {
        public P_JOMaterialsRequest()
        {
            InitializeComponent();
            if (HttpContext.Current.Session["ConnString"] != null)
                sqlDataSource1.Connection.ConnectionString = HttpContext.Current.Session["ConnString"].ToString();
        }

    }
}
