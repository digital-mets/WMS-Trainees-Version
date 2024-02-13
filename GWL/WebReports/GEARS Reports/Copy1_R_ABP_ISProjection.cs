using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using DevExpress.XtraReports;
using System.Web;

namespace GWL.WebReports.GEARS_Reports
{
    public partial class R_ABP_ISProjection : DevExpress.XtraReports.UI.XtraReport
    {
        public R_ABP_ISProjection()
        {
            InitializeComponent();
            if (HttpContext.Current.Session["ConnString"] != null)
            {
                dsReport.Connection.ConnectionString = HttpContext.Current.Session["ConnString"].ToString();
            }
            Year.Value = DateTime.Today.Year + 1;
        }

        private void ReportHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }

        private void ParameterChanged(object sender, DevExpress.XtraReports.Parameters.ParametersRequestValueChangedEventArgs e)
        {
            sender.ToString();
            e.ToString();
        }

    }
}
