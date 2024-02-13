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
    public partial class R_WMS_TransferSummary : DevExpress.XtraReports.UI.XtraReport
    {
        public R_WMS_TransferSummary()
        {
            InitializeComponent();

            if (HttpContext.Current.Session["ConnString"] != null)
                sqlDataSource1.Connection.ConnectionString = HttpContext.Current.Session["ConnString"].ToString();

            

            //this.GLAccount.Value = HttpContext.Current.Session["ConnString"].ToString();

        }


        private void R_APDueSummary_DataSourceDemanded(object sender, EventArgs e)
        {
            XtraReport report = (XtraReport)Report;

            //string businesssubclass = "";
            //string glapaccount = "";
            //string supplier = "";

            ////Business Sub Class
            //string[] paramValues = report.Parameters["BusinessSubClass"].Value as string[];
            ////xrTableCell13.Text = string.Empty;
            //for (int i = 0; i < paramValues.Length; i++)
            //{
            //    //xrTableCell13.Text += paramValues[i].ToString();
            //    businesssubclass += paramValues[i].ToString();
            //    if (i < paramValues.Length - 1)
            //        //xrTableCell13.Text += ",";
            //        businesssubclass += ",";
            //}

            ////GL AP Account
            //string[] paramValues2 = report.Parameters["GLAPAccount"].Value as string[];
            ////xrTableCell18.Text = string.Empty;
            //for (int i = 0; i < paramValues2.Length; i++)
            //{
            //    glapaccount += paramValues2[i].ToString();
            //    if (i < paramValues2.Length - 1)
            //        glapaccount += ",";
            //}

            ////Supplier Code
            //string[] paramValues3 = report.Parameters["Supplier"].Value as string[];
            ////rTableCell41.Text = string.Empty;
            //for (int i = 0; i < paramValues3.Length; i++)
            //{
            //    supplier += paramValues3[i].ToString();
            //    if (i < paramValues3.Length - 1)
            //        supplier += ",";
            //}

            //if (report.Parameters["BusinessSubClass"].Value.ToString() == "System.String[]")
            //{
            //    report.Parameters["BusinessSubClass"].Value = businesssubclass;
            //}

            //if (report.Parameters["GLAPAccount"].Value.ToString() == "System.String[]")
            //{
            //    report.Parameters["GLAPAccount"].Value = glapaccount;
            //}

            //if (report.Parameters["Supplier"].Value.ToString() == "System.String[]")
            //{
            //    report.Parameters["Supplier"].Value = supplier;
            //}

            //xrLabel1.Text = "DateFrom: [Parameters.DateFrom!MMMM dd, yyyy] / DateTo: [Parameters.DateTo!MMMM dd, yyyy] / Client: [Parameters.Client] / Branch: [Parameters.Branch]";

        }

        private void xrTableCell19_HtmlItemCreated(object sender, HtmlEventArgs e)
        {
            //e.ContentCell.Style.Add("border-left", "1px solid #000000 !important");
            //e.ContentCell.Style.Add("border-bottom", "1px solid #dcdcdc !important");  
        }

        private void xrTableCell27_HtmlItemCreated(object sender, HtmlEventArgs e)
        {
            //e.ContentCell.Style.Add("border-right", "1px solid #000000 !important");
            //e.ContentCell.Style.Add("border-bottom", "1px solid #dcdcdc !important");  
        }

        private void xrTableCell19_HtmlItemCreated_1(object sender, HtmlEventArgs e)
        {
            //e.ContentCell.Style.Add("border-left", "1px solid #000000 !important");  
        }

        private void xrTableCell27_HtmlItemCreated_1(object sender, HtmlEventArgs e)
        {
            //e.ContentCell.Style.Add("border-right", "1px solid #000000 !important");  
        }

        private void ReportHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }

        private void R_WMS_TransferSummary_DataSourceDemanded(object sender, EventArgs e)
        {
            //XtraReport report = (XtraReport)Report;
            //string warehouse = "";

            ////Business Sub Class
            //string[] paramValues = report.Parameters["Branch"].Value as string[];
            ////xrTableCell13.Text = string.Empty;
            //for (int i = 0; i < paramValues.Length; i++)
            //{
            //    //xrTableCell13.Text += paramValues[i].ToString();
            //    warehouse += paramValues[i].ToString();
            //    if (i < paramValues.Length - 1)
            //        //xrTableCell13.Text += ",";
            //        warehouse += ",";
            //}

            //if (report.Parameters["Branch"].Value.ToString() == "System.String[]")
            //{
            //    report.Parameters["Branch"].Value = warehouse;
            //}

            xrLabel1.Text = "DateFrom: [Parameters.DateFrom!MMMM dd, yyyy] / DateTo: [Parameters.DateTo!MMMM dd, yyyy] / Client: [Parameters.Client] / Branch: [Parameters.Branch]";
        }

    }
}
