using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using DevExpress.XtraReports;
using System.Web;

namespace GWL.WebReports.GEARS_Reports
{
    public partial class R_CollectionPeriodDetail : DevExpress.XtraReports.UI.XtraReport
    {
        public R_CollectionPeriodDetail()
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

        private void R_CollectionPeriodDetail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XtraReport report = (XtraReport)Report;

            string x = report.Parameters["Month"].Value.ToString();
            switch (x)
            {
                case "1":
                    xrLabel1.Text = "January [Parameters.Year] / ItemCategory:  [Parameters.ItemCategory] / Customer:  [Parameters.Customer] / Period:  [Parameters.Period]";
                    break;
                case "2":
                    xrLabel1.Text = "February [Parameters.Year] / ItemCategory:  [Parameters.ItemCategory] / Customer:  [Parameters.Customer] / Period:  [Parameters.Period]";
                    break;
                case "3":
                    xrLabel1.Text = "March [Parameters.Year] / ItemCategory:  [Parameters.ItemCategory] / Customer:  [Parameters.Customer] / Period:  [Parameters.Period]";
                    break;
                case "4":
                    xrLabel1.Text = "April [Parameters.Year] / ItemCategory:  [Parameters.ItemCategory] / Customer:  [Parameters.Customer] / Period:  [Parameters.Period]";
                    break;
                case "5":
                    xrLabel1.Text = "May [Parameters.Year] / ItemCategory:  [Parameters.ItemCategory] / Customer:  [Parameters.Customer] / Period:  [Parameters.Period]";
                    break;
                case "6":
                    xrLabel1.Text = "June [Parameters.Year] / ItemCategory:  [Parameters.ItemCategory] / Customer:  [Parameters.Customer] / Period:  [Parameters.Period]";
                    break;
                case "7":
                    xrLabel1.Text = "July [Parameters.Year] / ItemCategory:  [Parameters.ItemCategory] / Customer:  [Parameters.Customer] / Period:  [Parameters.Period]";
                    break;
                case "8":
                    xrLabel1.Text = "August [Parameters.Year] / ItemCategory:  [Parameters.ItemCategory] / Customer:  [Parameters.Customer] / Period:  [Parameters.Period]";
                    break;
                case "9":
                    xrLabel1.Text = "September [Parameters.Year] / ItemCategory:  [Parameters.ItemCategory] / Customer:  [Parameters.Customer] / Period:  [Parameters.Period]";
                    break;
                case "10":
                    xrLabel1.Text = "October [Parameters.Year] / ItemCategory:  [Parameters.ItemCategory] / Customer:  [Parameters.Customer] / Period:  [Parameters.Period]";
                    break;
                case "11":
                    xrLabel1.Text = "November [Parameters.Year] / ItemCategory:  [Parameters.ItemCategory] / Customer:  [Parameters.Customer] / Period:  [Parameters.Period]";
                    break;
                default:
                    xrLabel1.Text = "December [Parameters.Year] / ItemCategory:  [Parameters.ItemCategory] / Customer:  [Parameters.Customer] / Period:  [Parameters.Period]";
                    break;
            }
        }

        private void xrTableCell19_HtmlItemCreated(object sender, HtmlEventArgs e)
        {

            e.ContentCell.Style.Add("border-left", "1px solid #000000 !important");
            e.ContentCell.Style.Add("border-bottom", "1px solid #dcdcdc !important");  
        }

        private void xrTableCell14_HtmlItemCreated(object sender, HtmlEventArgs e)
        {

            e.ContentCell.Style.Add("border-right", "1px solid #000000 !important");
            e.ContentCell.Style.Add("border-bottom", "1px solid #dcdcdc !important");  
        }
    }
}
