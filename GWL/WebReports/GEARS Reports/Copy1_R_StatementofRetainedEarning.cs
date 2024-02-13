using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using DevExpress.XtraReports;
using System.Web;

namespace GWL.WebReports.GEARS_Reports
{
    public partial class R_StatementofRetainedEarning : DevExpress.XtraReports.UI.XtraReport
    {
        public R_StatementofRetainedEarning()
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
        //void xrTableCell17_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        //{
        //    //XRTableCell label = sender as XRLabel;
        //    XtraReport rootReport = xrTableCell17.Report as XtraReport;
        //    string[] paramValues = rootReport.Parameters["BizPartner"].Value as string[];
        //    xrTableCell17.Text = string.Empty;
        //    for (int i = 0; i < paramValues.Length; i++)
        //    {
        //        xrTableCell17.Text += paramValues[i].ToString();
        //        if (i < paramValues.Length - 1)
        //            xrTableCell17.Text += ",";
        //    }
        //}


    }
}
