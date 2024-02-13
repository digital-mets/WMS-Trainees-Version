using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using DevExpress.XtraReports;
using System.Web;

namespace GWL.WebReports.GEARS_Reports
{
    public partial class R_TrialBalance : DevExpress.XtraReports.UI.XtraReport
    {
        public R_TrialBalance()
        {
            InitializeComponent();

            if (HttpContext.Current.Session["ConnString"] != null)
                sqlDataSource1.Connection.ConnectionString = HttpContext.Current.Session["ConnString"].ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
          //  R_BizPartnerLedger rep = new R_BizPartnerLedger();
          //  rep.ShowPreviewDialog();
        }

        private void ReportHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }

        private void R_TrialBalance_DataSourceDemanded(object sender, EventArgs e)
        {
            XtraReport report = (XtraReport)Report;

            if (report.Parameters["YTD"].Value.ToString() == "True")
            {
                

                int year = Convert.ToInt32(report.Parameters["Year"].Value.ToString());
                int month = Convert.ToInt32(report.Parameters["Month"].Value.ToString());
                int x = 0;
                if (month == 12)
                    x = 1;
                else
                    x = month + 1;
                DateTime firstDay = new DateTime(year, 1, 1);
                DateTime AddMonth = new DateTime(year, x, 1);
                DateTime LastDay = AddMonth.AddDays(-1);

                xrLabel1.Text = "For The Period From:  " + firstDay.ToString("MMMM/dd/yyyy") + " To: " + LastDay.ToString("MMMM/dd/yyyy") + " / Year To Date(YTD):  Yes";
            }

            if (report.Parameters["YTD"].Value.ToString() == "False")
            {
                
                xrLabel1.Text = "For The Period:  [Parameters.Month]";


                string x = report.Parameters["Month"].Value.ToString();
                switch (x)
                {
                    case "1":
                        xrLabel1.Text = "For The Period:  January [Parameters.Year] / Year To Date(YTD):  No";
                        break;
                    case "2":
                        xrLabel1.Text = "For the Period February [Parameters.Year] / Year To Date(YTD):  No";
                        break;
                    case "3":
                        xrLabel1.Text = "For the Period March [Parameters.Year] / Year To Date(YTD):  No";
                        break;
                    case "4":
                        xrLabel1.Text = "For the Period April [Parameters.Year] / Year To Date(YTD):  No";
                        break;
                    case "5":
                        xrLabel1.Text = "For the Period May [Parameters.Year / Year To Date(YTD):  No]";
                        break;
                    case "6":
                        xrLabel1.Text = "For the Period June [Parameters.Year] / Year To Date(YTD):  No";
                        break;
                    case "7":
                        xrLabel1.Text = "For the Period July [Parameters.Year] / Year To Date(YTD):  No";
                        break;
                    case "8":
                        xrLabel1.Text = "For the Period August [Parameters.Year] / Year To Date(YTD):  No";
                        break;
                    case "9":
                        xrLabel1.Text = "For the Period September [Parameters.Year] / Year To Date(YTD):  No";
                        break;
                    case "10":
                        xrLabel1.Text = "For the Period October [Parameters.Year] / Year To Date(YTD):  No";
                        break;
                    case "11":
                        xrLabel1.Text = "For the Period November [Parameters.Year] / Year To Date(YTD):  No";
                        break;
                    default:
                        xrLabel1.Text = "For the Period December [Parameters.Year] / Year To Date(YTD):  No";
                        break;
                }
            }

        }

        private void xrTableCell20_HtmlItemCreated(object sender, HtmlEventArgs e)
        {
            e.ContentCell.Style.Add("border-left", "1px solid #000000 !important");
            e.ContentCell.Style.Add("border-bottom", "1px solid #dcdcdc !important");  
        }

        private void xrTableCell2_HtmlItemCreated(object sender, HtmlEventArgs e)
        {
            e.ContentCell.Style.Add("border-right", "1px solid #000000 !important");
            e.ContentCell.Style.Add("border-bottom", "1px solid #dcdcdc !important");  
        }



    }
}
