using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using DevExpress.XtraReports;
using System.Web;

namespace GWL.WebReports.GEARS_Printout
{
    public partial class P_PrintPicklist : DevExpress.XtraReports.UI.XtraReport
    {
        public P_PrintPicklist()
        {
            InitializeComponent();

            if (HttpContext.Current.Session["ConnString"] != null)
                sqlDataSource1.Connection.ConnectionString = HttpContext.Current.Session["ConnString"].ToString();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //R_BizPartnerLedger rep = new R_BizPartnerLedger();
            //rep.ShowPreviewDialog();
        }

        private void R_PrintPicklist_DataSourceDemanded(object sender, EventArgs e)
        {
            XtraReport report = (XtraReport)Report;

            // GL Account Code
            string Holder = "";
            string[] paramValues = report.Parameters["DocNumber"].Value as string[];
            //xrLabel1.Text = string.Empty;
            for (int i = 0; i < paramValues.Length; i++)
            {
                Holder += paramValues[i].ToString();
                if (i < paramValues.Length - 1)
                    Holder += ",";
            }

            //globalholder = xrTableCell13.Text;

            //if (report.Parameters["DocNumber"].Value.ToString() == "System.String[]")
            //{
                //report.Parameters["DocNumber"].MultiValue = false;
                report.Parameters["DocNumber2"].Value = Holder;
            //}
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
