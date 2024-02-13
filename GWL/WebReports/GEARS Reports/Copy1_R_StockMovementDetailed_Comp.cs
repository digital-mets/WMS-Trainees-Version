using System;
using System.Web;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using DevExpress.XtraReports;

namespace GWL.WebReports.GEARS_Reports
{
    // 05 - 23 - 2016 LGE Add Page Header Band.
    public partial class R_StockMovementDetailed_Comp : DevExpress.XtraReports.UI.XtraReport
    {
        public R_StockMovementDetailed_Comp()
        {
            InitializeComponent();
            if (HttpContext.Current.Session["ConnString"] != null)
                sqlDataSource1.Connection.ConnectionString = HttpContext.Current.Session["ConnString"].ToString();

            if (HttpContext.Current.Session["CustomerCode"] != null)
                Customer.Value = HttpContext.Current.Session["CustomerCode"].ToString();
        }

    }
}
