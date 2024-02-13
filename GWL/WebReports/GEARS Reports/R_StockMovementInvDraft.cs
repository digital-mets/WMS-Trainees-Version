using System;
using System.Web;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using DevExpress.XtraReports;

namespace GWL.WebReports.GEARS_Reports
{
    public partial class R_StockMovementInvDraft : DevExpress.XtraReports.UI.XtraReport
    {
        // 05 - 23 - 2016 LGE Add Page Header Band.
        public R_StockMovementInvDraft()
        {
            InitializeComponent();
            if (HttpContext.Current.Session["ConnString"] != null)
                sqlDataSource1.Connection.ConnectionString = HttpContext.Current.Session["ConnString"].ToString();
        }

        private void R_StockMovementInvDraft_AfterPrint(object sender, EventArgs e)
        {
            Common.Common.RawData(this.DataSource as DevExpress.DataAccess.Sql.SqlDataSource, (this.Report.ToString().Split('.'))[2] + "." + (this.Report.ToString().Split('.'))[3].ToString(), sqlDataSource1.Connection.ConnectionString);   

        }

    }
}
