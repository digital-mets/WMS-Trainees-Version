using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using DevExpress.XtraReports;
using System.Web;

namespace GWL.WebReports.GEARS_Reports
{
    public partial class R_ProductionStatusbyJO : DevExpress.XtraReports.UI.XtraReport
    {
        public R_ProductionStatusbyJO()
        {
            InitializeComponent();

            if (HttpContext.Current.Session["ConnString"] != null)
                sqlDataSource1.Connection.ConnectionString = HttpContext.Current.Session["ConnString"].ToString();
        }

        private void ReportHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }

        private void R_ProductionStatusbyJO_DataSourceDemanded(object sender, EventArgs e)
        {
            string from;
	    XtraReport report = (XtraReport)Report;

            from = (string)report.Parameters["DateFromDisplay"].Value;
            if (String.IsNullOrEmpty(from))
                report.Parameters["JODateFrom"].Value = "01/01/1900";
            else
                report.Parameters["JODateFrom"].Value = DateTime.Parse(from);

            from = (string)report.Parameters["DateToDisplay"].Value;
            if (String.IsNullOrEmpty(from))
                report.Parameters["JODateTo"].Value = "01/01/1900";
            else
                report.Parameters["JODateTo"].Value = DateTime.Parse(from);

            from = (string)report.Parameters["DateDueFromDisplay"].Value;
            if (String.IsNullOrEmpty(from))
                report.Parameters["JODueFrom"].Value = "01/01/1900";
            else
                report.Parameters["JODueFrom"].Value = DateTime.Parse(from);

            from = (string)report.Parameters["DateDueToDisplay"].Value;
            if (String.IsNullOrEmpty(from))
                report.Parameters["JODueTo"].Value = "01/01/1900";
            else
                report.Parameters["JODueTo"].Value = DateTime.Parse(from);

            from = (string)report.Parameters["DateSubconFromDisplay"].Value;
            if (String.IsNullOrEmpty(from))
                report.Parameters["JOSubconFrom"].Value = "01/01/1900";
            else
                report.Parameters["JOSubconFrom"].Value = DateTime.Parse(from);

            from = (string)report.Parameters["DateSubconToDisplay"].Value;
            if (String.IsNullOrEmpty(from))
                report.Parameters["JOSubconTo"].Value = "01/01/1900";
            else
                report.Parameters["JOSubconTo"].Value = DateTime.Parse(from);





        }

        private void R_ProductionStatusbyJO_ParametersRequestSubmit(object sender, DevExpress.XtraReports.Parameters.ParametersRequestEventArgs e)
        {

        }

        private void R_ProductionStatusbyJO_ParametersRequestBeforeShow(object sender, DevExpress.XtraReports.Parameters.ParametersRequestEventArgs e)
        {
           
        }

    }
}
