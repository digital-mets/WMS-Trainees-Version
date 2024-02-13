using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using DevExpress.XtraReports;
using System.Web;

namespace GWL.WebReports.GEARS_Reports
{
    public partial class R_WMS_DRForSM : DevExpress.XtraReports.UI.XtraReport
    {
        public R_WMS_DRForSM()
        {
            InitializeComponent();
            if (HttpContext.Current.Session["ConnString"] != null)
                sqlDataSource1.Connection.ConnectionString = HttpContext.Current.Session["ConnString"].ToString();
        }

        private void DetailReport_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (this.Parameters["Client"].Value.ToString().ToUpper() == "MJI"
                || this.Parameters["Client"].Value.ToString().ToUpper() == "JLI")
                e.Cancel = false;
            else
                e.Cancel = true;
        }

        private void DetailReport1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (this.Parameters["Client"].Value.ToString().ToUpper() == "JEI"
                || this.Parameters["Client"].Value.ToString().ToUpper() == "JGI"
                || this.Parameters["Client"].Value.ToString().ToUpper() == "JZI")
                e.Cancel = false;
            else
                e.Cancel = true;
        }
    }
}
