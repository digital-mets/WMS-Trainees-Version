using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using DevExpress.XtraReports;
using DevExpress.XtraReports.Parameters;
using System.Web;

namespace GWL.WebReports.GEARS_Reports
{
    public partial class R_ReceivingReportSummary : DevExpress.XtraReports.UI.XtraReport
    {
        public R_ReceivingReportSummary()
        {
            InitializeComponent();
            if (HttpContext.Current.Session["ConnString"] != null)
                sqlDataSource1.Connection.ConnectionString = HttpContext.Current.Session["ConnString"].ToString();
        }

        private void R_ReceivingReportSummary_ParametersRequestBeforeShow(object sender, ParametersRequestEventArgs e)
        {

        }
    }
}
