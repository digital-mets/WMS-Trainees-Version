using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Web;

namespace GWL.WebReports.GEARS_Reports
{
    public partial class RMerchandisingChart : DevExpress.XtraReports.UI.XtraReport
    {
        public RMerchandisingChart()
        {
            InitializeComponent();
            if (HttpContext.Current.Session["ConnString"] != null)
                sqlDataSource1.Connection.ConnectionString = HttpContext.Current.Session["ConnString"].ToString();

            XtraReport report = (XtraReport)Report;
            DateTime today = DateTime.Now;

            //report.Parameters["DocDateFr"].Value = answer.ToShortDateString();
            report.Parameters["DateTo"].Value = today.ToShortDateString();
        }

        private void RMerchandisingChart_DataSourceDemanded(object sender, EventArgs e)
        {
            XtraReport report = (XtraReport)Report;

            string cvdatefrom = (string)report.Parameters["DateFr"].Value;
            if (String.IsNullOrEmpty(cvdatefrom))
            {
                report.Parameters["DateFrom"].Value = null;
                //xrLabel14.Text = "From: NULL";
            }
            else
            {
                DateTime cvdatefrom2 = DateTime.Parse(cvdatefrom);
                report.Parameters["DateFrom"].Value = cvdatefrom2;
                //xrLabel14.Text = "From: " + cvdatefrom2.ToShortDateString();
            }
        }

    }
}
