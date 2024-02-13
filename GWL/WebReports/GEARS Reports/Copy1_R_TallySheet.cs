using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using DevExpress.XtraReports;
using System.Web;
using System.Data;
using GearsLibrary;

namespace GWL.WebReports.GEARS_Reports
{
    public partial class R_TallySheet : DevExpress.XtraReports.UI.XtraReport
    {
        public R_TallySheet()
        {
            InitializeComponent();

            if (HttpContext.Current.Session["ConnString"] != null)
                sqlDataSource1.Connection.ConnectionString = HttpContext.Current.Session["ConnString"].ToString();
            DevExpress.DataAccess.Sql.SqlDataSource.DisableCustomQueryValidation = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void ReportHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }

        private void R_SubconLiquidation_DataSourceDemanded(object sender, EventArgs e)
        {
            XtraReport report = (XtraReport)Report;

            string datefrom = (string)report.Parameters["DateFrom"].Value;
            string datefromheader = "";
            string summarybyjo = "";
            if (String.IsNullOrEmpty(datefrom))
            {
                //report.Parameters["CVDateFrom_Actual"].Type(string);
                report.Parameters["DateFrom_Actual"].Value = "01/01/0001";
                datefromheader = "DateFrom: NULL";
            }
            else
            {
                DateTime datefromactual = DateTime.Parse(datefrom);
                report.Parameters["DateFrom_Actual"].Value = datefromactual;
                datefromheader = "DateFrom: [Parameters.DateFrom_Actual!MMMM dd, yyyy]";
            }


            string dateto = (string)report.Parameters["DateTo"].Value;
            string datetoheader = "";
            if (String.IsNullOrEmpty(dateto))
            {
                report.Parameters["DateTo_Actual"].Value = "01/01/0001";
                datetoheader = "DateTo: NULL";
            }
            else
            {
                DateTime datetoactual = DateTime.Parse(dateto);
                report.Parameters["DateTo_Actual"].Value = datetoactual;
                datetoheader = "DateTo: [Parameters.DateTo_Actual!MMMM dd, yyyy]";
            }

            if (report.Parameters["SummaryByJO"].Value.ToString().ToUpper() == "TRUE")
                summarybyjo = "YES";
            else
                summarybyjo = "FALSE";

            this.xrLabel1.Text = "DateFrom: " + datefromheader + " / DateTo: " + datetoheader + " / JobOrderNo: [Parameters.JobOrderNo] / Status: [Parameters.Status] / SummaryByJO: " + summarybyjo;
        }

        private void xrTableRow5_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (((XRControl)xrTableCell23).Text.ToString() == "WIPIN")
            {
                e.Cancel = true;
            }
        }
    }
}
