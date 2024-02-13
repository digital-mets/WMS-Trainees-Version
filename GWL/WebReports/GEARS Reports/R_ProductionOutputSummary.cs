using System;
using System.Web;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using DevExpress.XtraReports;
using System.Data;
using GearsLibrary;

namespace GWL.WebReports.GEARS_Reports
{
    public partial class R_ProductionOutputSummary : DevExpress.XtraReports.UI.XtraReport
    {
        public R_ProductionOutputSummary()
        {
            InitializeComponent();
            if (HttpContext.Current.Session["ConnString"] != null)
                sqlDataSource1.Connection.ConnectionString = HttpContext.Current.Session["ConnString"].ToString();

            XtraReport report = (XtraReport)Report;

            //ADDED BY TA
            string startDate = "";
            DataTable getDate = Gears.RetriveData2("SELECT Value FROM IT.SystemSettings WHERE Code='BOOKDATE'", HttpContext.Current.Session["ConnString"].ToString());
            foreach (DataRow dtRow in getDate.Rows)
            {
                startDate = dtRow[0].ToString();
            }

            DateTime answer = Convert.ToDateTime(startDate);
            DateTime today = Convert.ToDateTime(startDate);

            report.Parameters["DateFrom"].Value = answer.ToShortDateString();
            report.Parameters["DateTo"].Value = today.ToShortDateString();
        }

        private void ReportHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }

    }
}
