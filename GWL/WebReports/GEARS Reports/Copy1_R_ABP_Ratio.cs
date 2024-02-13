using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using DevExpress.XtraReports;
using System.Web;

namespace GWL.WebReports.GEARS_Reports
{
    public partial class R_ABP_Ratio : DevExpress.XtraReports.UI.XtraReport
    {
        public R_ABP_Ratio()
        {
            InitializeComponent();
            if (HttpContext.Current.Session["ConnString"] != null)
            {
                dsReport.Connection.ConnectionString = HttpContext.Current.Session["ConnString"].ToString();
            }
        }

        private void ReportHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }

    }
}
