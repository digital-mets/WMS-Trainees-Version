using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using DevExpress.XtraReports;
using System.Web;

namespace GWL.WebReports.GEARS_Reports
{
    public partial class R_UnreplenishPettyCashExpense : DevExpress.XtraReports.UI.XtraReport
    {
        public R_UnreplenishPettyCashExpense()
        {
            InitializeComponent();

            if (HttpContext.Current.Session["ConnString"] != null)
                sqlDataSource1.Connection.ConnectionString = HttpContext.Current.Session["ConnString"].ToString();



        }

        private void button1_Click(object sender, EventArgs e)
        {
           // R_BizPartnerLedger rep = new R_BizPartnerLedger();
           // rep.ShowPreviewDialog();
        }

        private void ReportHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }

        private void R_UnreplenishPettyCashExpense_DataSourceDemanded(object sender, EventArgs e)
        {
            XtraReport report = (XtraReport)Report;
            string x = report.Parameters["Requestor"].ToString();
            string receiver = "";
            string requestor = "";
            string costcenter = "";
            string fundsource = "";
            string viewdetail = "";

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


            //FundSource
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


            if (report.Parameters["ViewDetails"].Value.ToString() == "True")
            {
                viewdetail = "Yes";
            }
            if (report.Parameters["ViewDetails"].Value.ToString() == "False")
            {
                viewdetail = "No";
            }
            xrLabel1.Text = "As Of: [Parameters.CutOff!MMMM dd, yyyy] / Receiver:  " + receiver + " / Requestor:  " + requestor + " / CostCenter:  " + costcenter + " / FundSource:  " + fundsource + " / ViewDetail:  " + viewdetail;


            //this.xrTableCell2.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            //      new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_UnreplenishedPettyCashExpense.Amount")});
            //this.xrTableCell2.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 100F);
            //this.xrTableCell2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;


            //Header1.Visible = false;
            Header2.Visible = false;
            Header3.Visible = false;
            Header4.Visible = false;
            Header5.Visible = false;

            

            //Detail1.Visible = false;
            Detail2.Visible = false;
            Detail3.Visible = false;
            Detail4.Visible = false;
            Detail5.Visible = false;
            
            //Footer1.Visible = false;
            //Footer2.Visible = false;
            //Footer3.Visible = false;
            //Footer4.Visible = false;
            //Footer5.Visible = false;

            

            if (report.Parameters["ViewDetails"].Value.ToString() == "False")
            {
                //xrTableCell1.Text = "Amount";
                //xrTableCell31.Text = "Total Amount";

                //this.xrTableCell24.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
                // new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_UnreplenishedPettyCashExpense.Amount")});
                Header2.WidthF = 2;
                Header3.WidthF = 2;
                Header4.WidthF = 2;
                Header5.WidthF = 2;
                Detail2.WidthF = 2;
                Detail3.WidthF = 2;
                Detail4.WidthF = 2;
                Detail5.WidthF = 2;


                Footer4.WidthF = 149.69F;
                Footer5.WidthF = 85.0F;


                Header1.Text = "Amount";
                
                this.Detail1.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
                    new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_UnreplenishedPettyCashExpense.Amount", "{0:#,0.00;(#,0.00);}")});
                this.Detail1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                this.Detail1.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 100F);
                //this.Footer1.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
                //    new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_UnreplenishedPettyCashExpense.Amount", "{0:#,0.00;(#,0.00);}")});


                this.xrTable2.LocationFloat = new DevExpress.Utils.PointFloat(275.526F, 0F);
                this.xrTable3.LocationFloat = new DevExpress.Utils.PointFloat(275.526F, 0F);
                this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(275.526F, 0F);
            }

            if (report.Parameters["ViewDetails"].Value.ToString() == "True")
            {
                Header1.Text = "FoundSource";


                this.Detail1.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
                    new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_UnreplenishedPettyCashExpense.FundSource")});
                //this.Footer1.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
                //    new DevExpress.XtraReports.UI.XRBinding("Text", null, "")});
                
                Header1.Visible = true;
                Header2.Visible = true;
                Header3.Visible = true;
                Header4.Visible = true;
                Header5.Visible = true;

                Header2.WidthF = 85.0F;
                Header3.WidthF = 85.0F;
                Header4.WidthF = 85.0F;
                Header5.WidthF = 85.0F;

                Detail1.Visible = true;
                Detail2.Visible = true;
                Detail3.Visible = true;
                Detail4.Visible = true;
                Detail5.Visible = true;

                Detail2.WidthF = 85.0F;
                Detail3.WidthF = 85.0F;
                Detail4.WidthF = 85.0F;
                Detail5.WidthF = 85.0F;

                //Footer1.Visible = true;
                //Footer2.Visible = true;
                //Footer3.Visible = true;
                //Footer4.Visible = true;
                //Footer5.Visible = true;

                //Footer2.WidthF = 85.0F;
                //Footer3.WidthF = 85.0F;
                Footer4.WidthF = 489.69F;
                Footer5.WidthF = 85.0F;


                this.xrTable2.LocationFloat = new DevExpress.Utils.PointFloat(124.526F, 0F);
                this.xrTable3.LocationFloat = new DevExpress.Utils.PointFloat(124.526F, 0F);
                this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(124.526F, 0F);

            }

        }

        private void R_UnreplenishPettyCashExpense_AfterPrint(object sender, EventArgs e)
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

        private void Detail5_HtmlItemCreated(object sender, HtmlEventArgs e)
        {
            e.ContentCell.Style.Add("border-right", "1px solid #000000 !important");
            e.ContentCell.Style.Add("border-bottom", "1px solid #dcdcdc !important");  
        }

        private void Detail1_HtmlItemCreated(object sender, HtmlEventArgs e)
        {

        }

        private void Footer1_HtmlItemCreated(object sender, HtmlEventArgs e)
        {

        }

        private void Header1_HtmlItemCreated(object sender, HtmlEventArgs e)
        {
            XtraReport report = (XtraReport)Report;

            if (report.Parameters["ViewDetails"].Value.ToString() == "False")
            {
                e.ContentCell.Style.Add("border-right", "1px solid #000000 !important");
            }
        }
    }
}
