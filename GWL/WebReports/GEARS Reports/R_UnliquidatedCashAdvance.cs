using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using DevExpress.XtraReports;
using System.Web;

namespace GWL.WebReports.GEARS_Reports
{
    public partial class R_UnliquidatedCashAdvance : DevExpress.XtraReports.UI.XtraReport
    {
        public R_UnliquidatedCashAdvance()
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

        private void R_UnliquidatedCashAdvance_DataSourceDemanded(object sender, EventArgs e)
        {
            XtraReport report = (XtraReport)Report;
            string receiver = "";
            string requestor = "";
            string costcenter = "";
            string fundsource = "";

            //Receiver
            string[] paramValues = report.Parameters["Receiver"].Value as string[];
            //xrTableCell13.Text = string.Empty;
            for (int i = 0; i < paramValues.Length; i++)
            {
                //xrTableCell13.Text += paramValues[i].ToString();
                receiver += paramValues[i].ToString();
                if (i < paramValues.Length - 1)
                    //xrTableCell13.Text += ",";
                    receiver += ",";
            }

            //Requestor
            string[] paramValues2 = report.Parameters["Requestor"].Value as string[];
            //xrTableCell18.Text = string.Empty;
            for (int i = 0; i < paramValues2.Length; i++)
            {
                //xrTableCell18.Text += paramValues2[i].ToString();
                requestor += paramValues2[i].ToString();
                if (i < paramValues2.Length - 1)
                    //xrTableCell18.Text += ",";
                    requestor += ",";
            }

            //CostCenter
            string[] paramValues3 = report.Parameters["CostCenter"].Value as string[];
            //xrTableCell22.Text = string.Empty;
            for (int i = 0; i < paramValues3.Length; i++)
            {
                //xrTableCell22.Text += paramValues3[i].ToString();
                costcenter += paramValues3[i].ToString();
                if (i < paramValues3.Length - 1)
                    //xrTableCell22.Text += ",";
                    costcenter += ",";
            }


            //CostCenter
            string[] paramValues4 = report.Parameters["FundSource"].Value as string[];
            //xrTableCell17.Text = string.Empty;
            for (int i = 0; i < paramValues4.Length; i++)
            {
                //xrTableCell17.Text += paramValues4[i].ToString();
                fundsource += paramValues4[i].ToString();
                if (i < paramValues4.Length - 1)
                    //xrTableCell17.Text += ",";
                    fundsource += ",";
            }





            if (report.Parameters["Receiver"].Value.ToString() == "System.String[]")
            {
                //report.Parameters["Receiver"].Value = xrTableCell13.Text;
                report.Parameters["Receiver"].Value = receiver;
            }

            if (report.Parameters["Requestor"].Value.ToString() == "System.String[]")
            {
                //report.Parameters["Requestor"].Value = xrTableCell18.Text;
                report.Parameters["Requestor"].Value = requestor;
            }

            if (report.Parameters["CostCenter"].Value.ToString() == "System.String[]")
            {
                //report.Parameters["CostCenter"].Value = xrTableCell22.Text;
                report.Parameters["CostCenter"].Value = costcenter;
            }

            if (report.Parameters["FundSource"].Value.ToString() == "System.String[]")
            {
                //report.Parameters["FundSource"].Value = xrTableCell17.Text;
                report.Parameters["FundSource"].Value = fundsource;
            }

            xrLabel1.Text = "Cut-Off Date: [Parameters.CutOff!MM/dd/yy] / Receiver:  " + receiver + " / Requestor: " + requestor + " / CostCenter: " + costcenter + " / FundSource: " + fundsource;

        }

        private void R_UnliquidatedCashAdvance_AfterPrint(object sender, EventArgs e)
        {
            //XtraReport report = (XtraReport)Report;

            //string[] Receiver = xrTableCell13.Text.Split(',');
            //string[] Requestor = xrTableCell18.Text.Split(',');
            //string[] CostCenter = xrTableCell22.Text.Split(',');
            //string[] FundSource = xrTableCell17.Text.Split(',');


            //report.Parameters["FundSource"].ValueInfo = "FundSource";
            //report.Parameters["Receiver"].ValueInfo = "Receiver";
            //report.Parameters["Requestor"].ValueInfo = "Requestor";
            //report.Parameters["CostCenter"].ValueInfo = "CostCenter";

        }

        private void xrTableCell19_HtmlItemCreated(object sender, HtmlEventArgs e)
        {
            e.ContentCell.Style.Add("border-left", "1px solid #000000 !important");
            e.ContentCell.Style.Add("border-bottom", "1px solid #dcdcdc !important");  
        }

        private void xrTableCell30_HtmlItemCreated(object sender, HtmlEventArgs e)
        {
            e.ContentCell.Style.Add("border-right", "1px solid #000000 !important");
            e.ContentCell.Style.Add("border-bottom", "1px solid #dcdcdc !important");  
        }



    }
}
