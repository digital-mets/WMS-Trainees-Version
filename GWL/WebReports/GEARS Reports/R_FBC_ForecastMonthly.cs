using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using DevExpress.XtraReports;
using System.Web;

namespace GWL.WebReports.GEARS_Reports
{
    public partial class R_FBC_ForecastMonthly : DevExpress.XtraReports.UI.XtraReport
    {
        public R_FBC_ForecastMonthly()
        {
            InitializeComponent();
            if (HttpContext.Current.Session["ConnString"] != null)
                dsReport.Connection.ConnectionString = HttpContext.Current.Session["ConnString"].ToString();
            Year.Value = DateTime.Today.AddMonths(-1).Year;
        }

        private void ReportHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }

    }
}
