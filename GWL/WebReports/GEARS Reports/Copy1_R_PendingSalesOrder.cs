using System;
using System.Web;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using DevExpress.XtraReports;
using DevExpress.XtraReports.Parameters;

namespace GWL.WebReports.GEARS_Reports
{
    // 05 - 23 - 2016 LGE Add Page Header Band.
    public partial class R_PendingSalesOrder : DevExpress.XtraReports.UI.XtraReport
    {
        public R_PendingSalesOrder()
        {
            InitializeComponent();
            if (HttpContext.Current.Session["ConnString"] != null)
                sqlDataSource1.Connection.ConnectionString = HttpContext.Current.Session["ConnString"].ToString();
        }


    }
}
