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
    public partial class R_ARAgingSummary : DevExpress.XtraReports.UI.XtraReport
    {
        public R_ARAgingSummary()
        {
            InitializeComponent();

            if (HttpContext.Current.Session["ConnString"] != null)
                sqlDataSource1.Connection.ConnectionString = HttpContext.Current.Session["ConnString"].ToString();

            

            //this.GLAccount.Value = HttpContext.Current.Session["ConnString"].ToString();

        }



        private void ReportHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }

        private void R_ARAgingSummary_ParametersRequestBeforeShow(object sender, DevExpress.XtraReports.Parameters.ParametersRequestEventArgs e)
        {
            XtraReport report = (XtraReport)Report;
            DataTable getAR = Gears.RetriveData2("SELECT Value FROM it.systemsettings where Code = 'ARACCT'", HttpContext.Current.Session["ConnString"].ToString());

            string[] ARCode = { getAR.Rows[0]["Value"].ToString() };
            report.Parameters["GLARAccount"].Value = ARCode;


            DevExpress.DataAccess.Sql.CustomSqlQuery customSqlQuery7 = new DevExpress.DataAccess.Sql.CustomSqlQuery();
            customSqlQuery7.Name = "GLARAccount";
            customSqlQuery7.Sql = "SELECT AccountCode FROM Accounting.ChartOfAccount WHERE AccountCode\r\n\t\t\t BETWEEN " +
             "\'1000\' AND \'2999\'";
        }


        private void R_ARAgingSummary_DataSourceDemanded(object sender, EventArgs e)
        {
            XtraReport report = (XtraReport)Report;

            string businessaccount = "";
            string customer = "";
            string salesman = "";
            string glaraccount = "";
            string groupbysalesman = "";
            string showpdc = "";
            string summaryby = "";
            string codtermsonly = "";
            string includeap = "";


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
                //xrTableCell18.Text += paramValues2[i].ToString();
                customer += paramValues2[i].ToString();
                if (i < paramValues2.Length - 1)
                    //xrTableCell18.Text += ",";
                    customer += ",";
            }

            //GL AR Account
            string[] paramValues3 = report.Parameters["Salesman"].Value as string[];
            //xrTableCell41.Text = string.Empty;
            for (int i = 0; i < paramValues3.Length; i++)
            {
                //xrTableCell41.Text += paramValues3[i].ToString();
                salesman += paramValues3[i].ToString();
                if (i < paramValues3.Length - 1)
                    //xrTableCell41.Text += ",";
                    salesman += ",";
            }

            //Salesman
            string[] paramValues4 = report.Parameters["GLARAccount"].Value as string[];
            //xrTableCell43.Text = string.Empty;
            for (int i = 0; i < paramValues4.Length; i++)
            {
                //xrTableCell43.Text += paramValues4[i].ToString();
                glaraccount += paramValues4[i].ToString();
                if (i < paramValues4.Length - 1)
                    //xrTableCell43.Text += ",";
                    glaraccount += ",";
            }

            report.Parameters["BusinessAccountParam"].Value = businessaccount;
            report.Parameters["CustomerParam"].Value = customer;
            report.Parameters["SalesmanParam"].Value = salesman;
            report.Parameters["GLARAccountParam"].Value = glaraccount;

            if (report.Parameters["GroupBySalesman"].Value.ToString() == "True")
            {
                groupbysalesman = "Yes";
            }

            if (report.Parameters["GroupBySalesman"].Value.ToString() == "False")
            {
                groupbysalesman = "No";
            }
            if (report.Parameters["CODTermsOnly"].Value.ToString() == "True")
            {
                codtermsonly = "Yes";
            }

            if (report.Parameters["CODTermsOnly"].Value.ToString() == "False")
            {
                codtermsonly = "No";
            }



            if (report.Parameters["ShowPDC"].Value.ToString() == "True")
            {
                showpdc = "Yes";
                PDCHead.Visible = true;
                PDCGroupH.Visible = true;
                PDCDetail.Visible = true;
                PDCGroupF.Visible = true;
                PDCReportF.Visible = true;
                PDCReportF2.Visible = true;

                //40.986341
                //Cell Movement
                //this.Table1.LocationFloat = new DevExpress.Utils.PointFloat(74.54213F, 210.1234F);
                //this.Table2.LocationFloat = new DevExpress.Utils.PointFloat(74.54213F, 0F);
                //this.Table3.LocationFloat = new DevExpress.Utils.PointFloat(74.54213F, 0F);
                //this.Table4.LocationFloat = new DevExpress.Utils.PointFloat(74.54213F, 3.178914E-05F);
            }

            if (report.Parameters["ShowPDC"].Value.ToString() == "False")
            {
                showpdc = "No";
                PDCHead.Visible = false;
                PDCGroupH.Visible = false;
                PDCDetail.Visible = false;
                PDCGroupF.Visible = false;
                PDCReportF.Visible = false;
                PDCReportF2.Visible = false;

                //Cell Movement
                //this.Table1.LocationFloat = new DevExpress.Utils.PointFloat(114.54213F, 210.1234F);
                //this.Table2.LocationFloat = new DevExpress.Utils.PointFloat(114.54213F, 0F);
                //this.Table3.LocationFloat = new DevExpress.Utils.PointFloat(114.54213F, 0F);
                //this.Table4.LocationFloat = new DevExpress.Utils.PointFloat(114.54213F, 3.178914E-05F);
                //this.Table5.LocationFloat = new DevExpress.Utils.PointFloat(114.54213F, 0F);
                //Border Movement
                //this.Label1.LocationFloat = new DevExpress.Utils.PointFloat(925.0645F, 210.1234F);
                //this.Label2.LocationFloat = new DevExpress.Utils.PointFloat(945.0645F, 6.357829E-05F);
                //this.Label3.LocationFloat = new DevExpress.Utils.PointFloat(945.0645F, 0F);
                //this.Label4.LocationFloat = new DevExpress.Utils.PointFloat(945.0645F, 6.357829E-05F);
                //this.Label5.LocationFloat = new DevExpress.Utils.PointFloat(945.0645F, 3.178914E-05F);

            }


            if (this.Parameters["GroupBySalesman"].Value.ToString() == "False")
            {
                Table2.Visible = false;
                Table4.Visible = false;
            }
            else
            {
                Table2.Visible = true;
                Table4.Visible = true;
            }


            xrLabel1.Text = "Cut-Off Date: [Parameters.CutOff!MMMM dd, yyyy] / GLAccount:  " + glaraccount + " / BusinessAccount:  " + businessaccount + " / Customer:  " + customer + " / Salesman:  " + salesman + " / SummaryBy:  [Parameters.SummaryBy] / GroupBySalesman:  " + groupbysalesman + " / IncludeAP:  " + includeap + " / ShowPDC:  " + showpdc + " / CODTermsOnly:  " + codtermsonly; 
        }

        private void xrTableCell19_HtmlItemCreated(object sender, HtmlEventArgs e)
        {
            e.ContentCell.Style.Add("border-left", "1px solid #000000 !important");
            e.ContentCell.Style.Add("border-bottom", "1px solid #dcdcdc !important");    
        }

        private void PDCDetail_HtmlItemCreated(object sender, HtmlEventArgs e)
        {
            e.ContentCell.Style.Add("border-right", "1px solid #000000 !important");
            e.ContentCell.Style.Add("border-bottom", "1px solid #dcdcdc !important");    
        }

        private void xrTableCell51_HtmlItemCreated(object sender, HtmlEventArgs e)
        {

        }

        private void PDCGroupH_HtmlItemCreated(object sender, HtmlEventArgs e)
        {

        }

        private void xrTableCell56_HtmlItemCreated(object sender, HtmlEventArgs e)
        {

        }

        private void PDCGroupF_HtmlItemCreated(object sender, HtmlEventArgs e)
        {

        }

        private void xrTableCell55_HtmlItemCreated(object sender, HtmlEventArgs e)
        {

        }

        private void xrTableCell21_HtmlItemCreated(object sender, HtmlEventArgs e)
        {

        }

        private void xrTableCell49_HtmlItemCreated(object sender, HtmlEventArgs e)
        {
            XtraReport report = (XtraReport)Report;

            if (report.Parameters["ShowPDC"].Value.ToString() == "False")
            {
                e.ContentCell.Style.Add("border-right", "1px solid #000000 !important");
            }  
        }

        private void xrTableCell56_HtmlItemCreated_1(object sender, HtmlEventArgs e)
        {

            XtraReport report = (XtraReport)Report;

            if (report.Parameters["ShowPDC"].Value.ToString() == "False")
            {
                e.ContentCell.Style.Add("border-right", "1px solid #000000 !important");
            }  
        }

        private void xrTableCell19_HtmlItemCreated_1(object sender, HtmlEventArgs e)
        {
            e.ContentCell.Style.Add("border-left", "1px solid #000000 !important");  
        }

        private void PDCDetail_HtmlItemCreated_1(object sender, HtmlEventArgs e)
        {
            e.ContentCell.Style.Add("border-right", "1px solid #000000 !important");  
        }

        private void xrTableCell51_HtmlItemCreated_1(object sender, HtmlEventArgs e)
        {
            XtraReport report = (XtraReport)Report;

            if (report.Parameters["ShowPDC"].Value.ToString() == "False")
            {
                e.ContentCell.Style.Add("border-right", "1px solid #000000 !important");
            } 
        }

        private void xrTableCell55_HtmlItemCreated_1(object sender, HtmlEventArgs e)
        {
            XtraReport report = (XtraReport)Report;

            if (report.Parameters["ShowPDC"].Value.ToString() == "False")
            {
                e.ContentCell.Style.Add("border-right", "1px solid #000000 !important");
            } 
        }

        private void xrTableCell21_HtmlItemCreated_1(object sender, HtmlEventArgs e)
        {
            XtraReport report = (XtraReport)Report;

            if (report.Parameters["ShowPDC"].Value.ToString() == "False")
            {
                e.ContentCell.Style.Add("border-right", "1px solid #000000 !important");
            } 
        }

        private void xrTableCell44_HtmlItemCreated(object sender, HtmlEventArgs e)
        {
            XtraReport report = (XtraReport)Report;

            if (report.Parameters["ShowPDC"].Value.ToString() == "False")
            {
                e.ContentCell.Style.Add("border-right", "1px solid #000000 !important");
            } 
        }

    }
}
