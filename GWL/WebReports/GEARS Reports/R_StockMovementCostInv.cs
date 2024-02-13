using System;
using System.Web;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using DevExpress.XtraReports;

namespace GWL.WebReports.GEARS_Reports
{
    public partial class R_StockMovementCostInv : DevExpress.XtraReports.UI.XtraReport
    {
        // 05 - 23 - 2016 LGE Add Page Header Band.
        public R_StockMovementCostInv()
        {
            InitializeComponent();
            if (HttpContext.Current.Session["ConnString"] != null)
                sqlDataSource1.Connection.ConnectionString = HttpContext.Current.Session["ConnString"].ToString();
        }

    }
}
