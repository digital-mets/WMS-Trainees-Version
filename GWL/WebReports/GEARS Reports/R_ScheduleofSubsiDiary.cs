using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using DevExpress.XtraReports;
using System.Web;

namespace GWL.WebReports.GEARS_Reports
{
    public partial class R_ScheduleofSubsiDiary : DevExpress.XtraReports.UI.XtraReport
    {
        public R_ScheduleofSubsiDiary()
        {
            InitializeComponent();

            if (HttpContext.Current.Session["ConnString"] != null)
                sqlDataSource1.Connection.ConnectionString = HttpContext.Current.Session["ConnString"].ToString();
            DevExpress.DataAccess.Sql.SqlDataSource.DisableCustomQueryValidation = true;
        }


        private void xrTableCell12_HtmlItemCreated(object sender, HtmlEventArgs e)
        {
            e.ContentCell.Style.Add("border-left", "1px solid #000000 !important");
            e.ContentCell.Style.Add("border-bottom", "1px solid #dcdcdc !important");  
        }

        private void xrTableCell31_HtmlItemCreated(object sender, HtmlEventArgs e)
        {
            e.ContentCell.Style.Add("border-right", "1px solid #000000 !important");
            e.ContentCell.Style.Add("border-bottom", "1px solid #dcdcdc !important");  
        }

        private void xrTableCell19_HtmlItemCreated(object sender, HtmlEventArgs e)
        {
            e.ContentCell.Style.Add("border-left", "1px solid #000000 !important");
            e.ContentCell.Style.Add("border-bottom", "1px solid #dcdcdc !important");  
        }

        private void xrTableCell26_HtmlItemCreated(object sender, HtmlEventArgs e)
        {
            e.ContentCell.Style.Add("border-right", "1px solid #000000 !important");
            e.ContentCell.Style.Add("border-bottom", "1px solid #dcdcdc !important");  
        }

        private void R_ScheduleofSubsiDiary_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XtraReport report = (XtraReport)Report;

            string x = report.Parameters["Month"].Value.ToString();
            switch (x)
            {
                case "1":
                    xrLabel1.Text = "Report Period: Month: January Year: [Parameters.Year] / AccountCode:  [Parameters.AccountCode] / SubsiCode:  [Parameters.SubsiCode] / ProfitCenterCode:  [Parameters.ProfitCenterCode] / BizPartnerCode:  [Parameters.BizPartnerCode] / BizAccountCode:  [Parameters.BizAccountCode]";
                    break;
                case "2":
                    xrLabel1.Text = "Report Period: Month: February	Year: [Parameters.Year] / AccountCode:  [Parameters.AccountCode] / SubsiCode:  [Parameters.SubsiCode] / ProfitCenterCode:  [Parameters.ProfitCenterCode] / BizPartnerCode:  [Parameters.BizPartnerCode] / BizAccountCode:  [Parameters.BizAccountCode]";
                    break;
                case "3":
                    xrLabel1.Text = "Report Period: Month: March Year: [Parameters.Year] / AccountCode:  [Parameters.AccountCode] / SubsiCode:  [Parameters.SubsiCode] / ProfitCenterCode:  [Parameters.ProfitCenterCode] / BizPartnerCode:  [Parameters.BizPartnerCode] / BizAccountCode:  [Parameters.BizAccountCode]";
                    break;
                case "4":
                    xrLabel1.Text = "Report Period: Month: April Year: [Parameters.Year] / AccountCode:  [Parameters.AccountCode] / SubsiCode:  [Parameters.SubsiCode] / ProfitCenterCode:  [Parameters.ProfitCenterCode] / BizPartnerCode:  [Parameters.BizPartnerCode] / BizAccountCode:  [Parameters.BizAccountCode]";
                    break;
                case "5":
                    xrLabel1.Text = "Report Period: Month: May Year: [Parameters.Year] / AccountCode:  [Parameters.AccountCode] / SubsiCode:  [Parameters.SubsiCode] / ProfitCenterCode:  [Parameters.ProfitCenterCode] / BizPartnerCode:  [Parameters.BizPartnerCode] / BizAccountCode:  [Parameters.BizAccountCode]";
                    break;
                case "6":
                    xrLabel1.Text = "Report Period: Month: June Year: [Parameters.Year] / AccountCode:  [Parameters.AccountCode] / SubsiCode:  [Parameters.SubsiCode] / ProfitCenterCode:  [Parameters.ProfitCenterCode] / BizPartnerCode:  [Parameters.BizPartnerCode] / BizAccountCode:  [Parameters.BizAccountCode]";
                    break;
                case "7":
                    xrLabel1.Text = "Report Period: Month: July Year: [Parameters.Year] / AccountCode:  [Parameters.AccountCode] / SubsiCode:  [Parameters.SubsiCode] / ProfitCenterCode:  [Parameters.ProfitCenterCode] / BizPartnerCode:  [Parameters.BizPartnerCode] / BizAccountCode:  [Parameters.BizAccountCode]";
                    break;
                case "8":
                    xrLabel1.Text = "Report Period: Month: August Year: [Parameters.Year] / AccountCode:  [Parameters.AccountCode] / SubsiCode:  [Parameters.SubsiCode] / ProfitCenterCode:  [Parameters.ProfitCenterCode] / BizPartnerCode:  [Parameters.BizPartnerCode] / BizAccountCode:  [Parameters.BizAccountCode]";
                    break;
                case "9":
                    xrLabel1.Text = "Report Period: Month: September Year: [Parameters.Year] / AccountCode:  [Parameters.AccountCode] / SubsiCode:  [Parameters.SubsiCode] / ProfitCenterCode:  [Parameters.ProfitCenterCode] / BizPartnerCode:  [Parameters.BizPartnerCode] / BizAccountCode:  [Parameters.BizAccountCode]";
                    break;
                case "10":
                    xrLabel1.Text = "Report Period: Month: October Year: [Parameters.Year] / AccountCode:  [Parameters.AccountCode] / SubsiCode:  [Parameters.SubsiCode] / ProfitCenterCode:  [Parameters.ProfitCenterCode] / BizPartnerCode:  [Parameters.BizPartnerCode] / BizAccountCode:  [Parameters.BizAccountCode]";
                    break;
                case "11":
                    xrLabel1.Text = "Report Period: Month: November	Year: [Parameters.Year] / AccountCode:  [Parameters.AccountCode] / SubsiCode:  [Parameters.SubsiCode] / ProfitCenterCode:  [Parameters.ProfitCenterCode] / BizPartnerCode:  [Parameters.BizPartnerCode] / BizAccountCode:  [Parameters.BizAccountCode]";
                    break;
                default:
                    xrLabel1.Text = "Report Period: Month: December Year: [Parameters.Year] / AccountCode:  [Parameters.AccountCode] / SubsiCode:  [Parameters.SubsiCode] / ProfitCenterCode:  [Parameters.ProfitCenterCode] / BizPartnerCode:  [Parameters.BizPartnerCode] / BizAccountCode:  [Parameters.BizAccountCode]";
                    break;
            }

            //xrTableCell4.Text = "[Parameters.GroupBy]";
        }



    }
}
