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
    public partial class R_SubConSummary : DevExpress.XtraReports.UI.XtraReport
    {
        public R_SubConSummary()
        {
            InitializeComponent();

            if (HttpContext.Current.Session["ConnString"] != null)
                sqlDataSource1.Connection.ConnectionString = HttpContext.Current.Session["ConnString"].ToString();

            

            //this.GLAccount.Value = HttpContext.Current.Session["ConnString"].ToString();

        }



        private void ReportHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }

        private void R_APDueDetail_ParametersRequestBeforeShow(object sender, DevExpress.XtraReports.Parameters.ParametersRequestEventArgs e)
        {
            //XtraReport report = (XtraReport)Report;
            //DataTable getAP = Gears.RetriveData2("SELECT Value FROM it.systemsettings where Code = 'APACCT'", HttpContext.Current.Session["ConnString"].ToString());

            //if (getAP.Rows.Count > 0)
            //{
            //    string[] APCode = { getAP.Rows[0]["Value"].ToString() };
            //    report.Parameters["GLAPAccount"].Value = APCode;
            //}
        }

        private void R_APDueDetail_DataSourceDemanded(object sender, EventArgs e)
        {
            XtraReport report = (XtraReport)Report;

            string businesssubclass = "";
            
            string glapaccount = "";

            //Business Sub Class
            string[] paramValues = report.Parameters["StepCode"].Value as string[];
            //xrTableCell13.Text = string.Empty;
            for (int i = 0; i < paramValues.Length; i++)
            {
                //xrTableCell13.Text += paramValues[i].ToString();
                businesssubclass += paramValues[i].ToString();
                if (i < paramValues.Length - 1)
                    //xrTableCell13.Text += ",";
                    businesssubclass += ",";
            }

            //GL AP Account
            string[] paramValues2 = report.Parameters["JobOrder"].Value as string[];
            //xrTableCell18.Text = string.Empty;
            for (int i = 0; i < paramValues2.Length; i++)
            {
                glapaccount += paramValues2[i].ToString();
                if (i < paramValues2.Length - 1)
                    glapaccount += ",";
            }



            if (report.Parameters["StepCode"].Value.ToString() == "System.String[]")
            {
                report.Parameters["StepCode"].Value = businesssubclass;
            }

            if (report.Parameters["JobOrder"].Value.ToString() == "System.String[]")
            {
                report.Parameters["JobOrder"].Value = glapaccount;
            }

            

          //  xrLabel1.Text = "Cut-Off Date: [Parameters.CutOff!MMMM dd, yyyy] / BusinessSubClass:  " + businesssubclass + " / Supplier:  " + supplier + " / GLAPAccount:  " + glapaccount;
        }

        private void xrTableCell64_HtmlItemCreated(object sender, HtmlEventArgs e)
        {
            e.ContentCell.Style.Add("border-left", "1px solid #000000 !important");
            e.ContentCell.Style.Add("border-bottom", "1px solid #dcdcdc !important");  
        }

        private void xrTableCell65_HtmlItemCreated(object sender, HtmlEventArgs e)
        {
            e.ContentCell.Style.Add("border-right", "1px solid #000000 !important");
            e.ContentCell.Style.Add("border-bottom", "1px solid #dcdcdc !important");  
        }

        private void xrTableCell19_HtmlItemCreated(object sender, HtmlEventArgs e)
        {
            e.ContentCell.Style.Add("border-left", "1px solid #000000 !important");
            e.ContentCell.Style.Add("border-bottom", "1px solid #dcdcdc !important");  
        }

        private void xrTableCell3_HtmlItemCreated(object sender, HtmlEventArgs e)
        {
            e.ContentCell.Style.Add("border-right", "1px solid #000000 !important");
            e.ContentCell.Style.Add("border-bottom", "1px solid #dcdcdc !important");  
        }
    }
}
