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
    public partial class R_ARDueDetail : DevExpress.XtraReports.UI.XtraReport
    {
        public R_ARDueDetail()
        {
            InitializeComponent();

            if (HttpContext.Current.Session["ConnString"] != null)
                sqlDataSource1.Connection.ConnectionString = HttpContext.Current.Session["ConnString"].ToString();

            

            //this.GLAccount.Value = HttpContext.Current.Session["ConnString"].ToString();

        }



        private void ReportHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }

        private void R_ARDueDetail_DataSourceDemanded(object sender, EventArgs e)
        {
            XtraReport report = (XtraReport)Report;
            string businessaccount = "";
            string customer = "";
            string salesman = "";
            string glaraccount = "";

            //BusinessAccount
            string[] paramValues = report.Parameters["BusinessAccount"].Value as string[];
            //xrTableCell13.Text = string.Empty;
            for (int i = 0; i < paramValues.Length; i++)
            {
                //xrTableCell13.Text += paramValues[i].ToString();
                businessaccount += paramValues[i].ToString();
                if (i < paramValues.Length - 1)
                    //xrTableCell13.Text += ",";
                    businessaccount += ",";
            }

            //Customer
            string[] paramValues2 = report.Parameters["Customer"].Value as string[];
            //xrTableCell18.Text = string.Empty;
            for (int i = 0; i < paramValues2.Length; i++)
            {
                customer += paramValues2[i].ToString();
                if (i < paramValues2.Length - 1)
                    customer += ",";
            }

            //GL AR Account
            string[] paramValues3 = report.Parameters["GLARAccount"].Value as string[];
            //xrTableCell25.Text = string.Empty;
            for (int i = 0; i < paramValues3.Length; i++)
            {
                glaraccount += paramValues3[i].ToString();
                if (i < paramValues3.Length - 1)
                    glaraccount += ",";
            }

            //Salesman
            string[] paramValues4 = report.Parameters["Salesman"].Value as string[];
            //xrTableCell31.Text = string.Empty;
            for (int i = 0; i < paramValues4.Length; i++)
            {
                salesman += paramValues4[i].ToString();
                if (i < paramValues4.Length - 1)
                    salesman += ",";
            }

            if (report.Parameters["BusinessAccount"].Value.ToString() == "System.String[]")
            {
                report.Parameters["BusinessAccount"].Value = businessaccount;
            }

            if (report.Parameters["Customer"].Value.ToString() == "System.String[]")
            {
                report.Parameters["Customer"].Value = customer;
            }

            if (report.Parameters["GLARAccount"].Value.ToString() == "System.String[]")
            {
                report.Parameters["GLARAccount"].Value = glaraccount;
            }

            if (report.Parameters["Salesman"].Value.ToString() == "System.String[]")
            {
                report.Parameters["Salesman"].Value = salesman;
            }


            xrLabel1.Text = "Cut-Off Date: [Parameters.CutOff!MMMM dd, yyyy] / BusinessAccount:  " + businessaccount + " / Customer:  " + customer + " / Salesman:  " + salesman + " / GLARAccount:  " + glaraccount + " / SummaryBy:  [Parameters.SummaryBy]";

        }

        private void R_ARDueDetail_ParametersRequestBeforeShow(object sender, DevExpress.XtraReports.Parameters.ParametersRequestEventArgs e)
        {
            XtraReport report = (XtraReport)Report;
            DataTable getAR = Gears.RetriveData2("SELECT Value FROM it.systemsettings where Code = 'ARACCT'", HttpContext.Current.Session["ConnString"].ToString());

            string[] ARCode = { getAR.Rows[0]["Value"].ToString() };
            report.Parameters["GLARAccount"].Value = ARCode;
        }

        private void xrTableCell19_HtmlItemCreated(object sender, HtmlEventArgs e)
        {
            e.ContentCell.Style.Add("border-left", "1px solid #000000 !important");
            e.ContentCell.Style.Add("border-bottom", "1px solid #dcdcdc !important"); 
        }

        private void xrTableCell22_HtmlItemCreated(object sender, HtmlEventArgs e)
        {
            e.ContentCell.Style.Add("border-right", "1px solid #000000 !important");
            e.ContentCell.Style.Add("border-bottom", "1px solid #dcdcdc !important"); 
        }

        private void xrTableCell19_HtmlItemCreated_1(object sender, HtmlEventArgs e)
        {
            e.ContentCell.Style.Add("border-left", "1px solid #000000 !important");  
        }

        private void xrTableCell22_HtmlItemCreated_1(object sender, HtmlEventArgs e)
        {
            e.ContentCell.Style.Add("border-right", "1px solid #000000 !important");  
        }
    }
}
