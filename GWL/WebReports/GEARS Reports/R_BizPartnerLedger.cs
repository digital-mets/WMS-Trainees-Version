using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using DevExpress.XtraReports;
using System.Web;

namespace GWL.WebReports.GEARS_Reports
{
    public partial class R_BizPartnerLedger : DevExpress.XtraReports.UI.XtraReport
    {
        public R_BizPartnerLedger()
        {
            InitializeComponent();

            if (HttpContext.Current.Session["ConnString"] != null)
                sqlDataSource1.Connection.ConnectionString = HttpContext.Current.Session["ConnString"].ToString();


            this.BizPartner.Type = typeof(System.String);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void ReportHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }

        private void R_BizPartnerLedger_DataSourceDemanded(object sender, EventArgs e)
        {
            XtraReport report = (XtraReport)Report;
            string glaccountcode = "";
            string bizaccountcode = "";
            string bizpartnercode = "";
            string outstandingonly = "";

            //GL Account Code
            string[] paramValues = report.Parameters["GLAccount"].Value as string[];
            //xrTableCell13.Text = string.Empty;
            for (int i = 0; i < paramValues.Length; i++)
            {
                //xrTableCell13.Text += paramValues[i].ToString();
                glaccountcode += paramValues[i].ToString();
                if (i < paramValues.Length - 1)
                    //xrTableCell13.Text += ",";
                    glaccountcode += ",";
            }

            //Business Account Code
            string[] paramValues2 = report.Parameters["BizAccount"].Value as string[];
            //xrTableCell18.Text = string.Empty;
            for (int i = 0; i < paramValues2.Length; i++)
            {
                //xrTableCell18.Text += paramValues2[i].ToString();
                bizaccountcode += paramValues2[i].ToString();
                if (i < paramValues2.Length - 1)
                    //xrTableCell18.Text += ",";
                    bizaccountcode += ",";
            }

            //Business Partner Code
            string[] paramValues3 = report.Parameters["BizPartner"].Value as string[];
            //xrTableCell22.Text = string.Empty;
            for (int i = 0; i < paramValues3.Length; i++)
            {
                //xrTableCell22.Text += paramValues3[i].ToString();
                bizpartnercode += paramValues3[i].ToString();
                if (i < paramValues3.Length - 1)
                    //xrTableCell22.Text += ",";
                    bizpartnercode += ",";
            }

            if (report.Parameters["GLAccount"].Value.ToString() == "System.String[]")
            {
                //report.Parameters["GLAccount"].Value = xrTableCell13.Text;
                report.Parameters["GLAccount"].Value = glaccountcode;
            }

            if (report.Parameters["BizAccount"].Value.ToString() == "System.String[]")
            {
                //report.Parameters["BizAccount"].Value = xrTableCell18.Text;
                report.Parameters["BizAccount"].Value = bizaccountcode;
            }

            if (report.Parameters["BizPartner"].Value.ToString() == "System.String[]")
            {
                //report.Parameters["BizPartner"].Value = xrTableCell22.Text;
                report.Parameters["BizPartner"].Value = bizpartnercode;
            }

            if (report.Parameters["OutstandingOnly"].Value.ToString() == "True")
            {
                outstandingonly = "Yes";
            }

            if (report.Parameters["OutstandingOnly"].Value.ToString() == "False")
            {
                outstandingonly = "No";
            }


            xrLabel1.Text = "From: [Parameters.DateFrom!MM/dd/yyyy] To: [Parameters.DateTo!MM/dd/yyyy] / BizPartnerCode:  " + bizpartnercode + " / BizAccountCode:  " + bizaccountcode + " / GLAccountCode:  " + glaccountcode + " / OutstandingOnly:  " + outstandingonly;

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
