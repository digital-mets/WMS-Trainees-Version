using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using DevExpress.XtraReports;
using System.Web;

namespace GWL.WebReports.GEARS_Reports
{
    public partial class R_SubconLiquidation : DevExpress.XtraReports.UI.XtraReport
    {
        public R_SubconLiquidation()
        {
            InitializeComponent();

            if (HttpContext.Current.Session["ConnString"] != null)
                sqlDataSource1.Connection.ConnectionString = HttpContext.Current.Session["ConnString"].ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void ReportHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }

        private void R_SubconLiquidation_DataSourceDemanded(object sender, EventArgs e)
        {
            var showforreturnonly = "";
            if (this.Parameters["ShowForReturnOnly"].Value.ToString().ToUpper() == "TRUE")
                showforreturnonly = "Yes";
            else
                showforreturnonly = "No";

            this.xrLabel1.Text = "JONumber: [Parameters.JobOrderNo] / WorkCenter: [Parameters.WorkCenter] / ShowForReturnOnly: " + showforreturnonly;
        }
    }
}
