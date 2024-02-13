﻿using System;
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
    public partial class R_APAgingSummary : DevExpress.XtraReports.UI.XtraReport
    {
        public R_APAgingSummary()
        {
            InitializeComponent();

            if (HttpContext.Current.Session["ConnString"] != null)
                sqlDataSource1.Connection.ConnectionString = HttpContext.Current.Session["ConnString"].ToString();

            

            //this.GLAccount.Value = HttpContext.Current.Session["ConnString"].ToString();

        }



        private void ReportHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }

        private void R_APAgingSummary_ParametersRequestBeforeShow(object sender, DevExpress.XtraReports.Parameters.ParametersRequestEventArgs e)
        {
            XtraReport report = (XtraReport)Report;
            DataTable getAP = Gears.RetriveData2("SELECT Value FROM it.systemsettings where Code = 'APACCT'", HttpContext.Current.Session["ConnString"].ToString());

            string[] APCode = { getAP.Rows[0]["Value"].ToString() };
            report.Parameters["GLAPAccount"].Value = APCode;


        }

        private void R_APAgingSummary_DataSourceDemanded(object sender, EventArgs e)
        {
            XtraReport report = (XtraReport)Report;

            string businesssubclass = "";
            string glapaaccount = "";
            string supplier = "";
            

            //Business Sub Class
            string[] paramValues = report.Parameters["BusinessSubClass"].Value as string[];
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
            string[] paramValues2 = report.Parameters["GLAPAccount"].Value as string[];
            //xrTableCell18.Text = string.Empty;
            for (int i = 0; i < paramValues2.Length; i++)
            {
                glapaaccount += paramValues2[i].ToString();
                if (i < paramValues2.Length - 1)
                    glapaaccount += ",";
            }

            //Supplier Code
            string[] paramValues3 = report.Parameters["Supplier"].Value as string[];
            //xrTableCell41.Text = string.Empty;
            for (int i = 0; i < paramValues3.Length; i++)
            {
                supplier += paramValues3[i].ToString();
                if (i < paramValues3.Length - 1)
                    supplier += ",";
            }

            if (report.Parameters["BusinessSubClass"].Value.ToString() == "System.String[]")
            {
                report.Parameters["BusinessSubClass"].Value = businesssubclass;
            }

            if (report.Parameters["GLAPAccount"].Value.ToString() == "System.String[]")
            {
                report.Parameters["GLAPAccount"].Value = glapaaccount;
            }

            if (report.Parameters["Supplier"].Value.ToString() == "System.String[]")
            {
                report.Parameters["Supplier"].Value = supplier;
            }

            xrLabel1.Text = "Cut-Off Date: [Parameters.CutOff!MMMM dd, yyyy] / BusinessSubClass:  " + businesssubclass + " / GPAPAccount:  " + glapaaccount + " / Supplier:  " + supplier + " / SummaryBy:  [Parameters.SummaryBy]";

        }

        private void xrTableCell19_HtmlItemCreated(object sender, HtmlEventArgs e)
        {
            e.ContentCell.Style.Add("border-left", "1px solid #000000 !important");
            e.ContentCell.Style.Add("border-bottom", "1px solid #dcdcdc !important");   
        }

        private void xrTableCell17_HtmlItemCreated(object sender, HtmlEventArgs e)
        {
            e.ContentCell.Style.Add("border-right", "1px solid #000000 !important");
            e.ContentCell.Style.Add("border-bottom", "1px solid #dcdcdc !important");   
        }

        private void xrTableCell17_HtmlItemCreated_1(object sender, HtmlEventArgs e)
        {
            e.ContentCell.Style.Add("border-right", "1px solid #000000 !important");  
        }

        private void xrTableCell19_HtmlItemCreated_1(object sender, HtmlEventArgs e)
        {
            e.ContentCell.Style.Add("border-left", "1px solid #000000 !important");  
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
