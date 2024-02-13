using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using DevExpress.XtraReports;
using System.Web;

namespace GWL.WebReports.GEARS_Reports
{
    public partial class R_CollectionPeriodSummary : DevExpress.XtraReports.UI.XtraReport
    {
        public R_CollectionPeriodSummary()
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

        private void R_CollectionPeriodSummary_DataSourceDemanded(object sender, EventArgs e)
        {
            XtraReport report = (XtraReport)Report;
            string salesman = "";
            //GL Account Code
            string[] paramValues = report.Parameters["Salesman"].Value as string[];
            //xrTableCell18.Text = string.Empty;
            for (int i = 0; i < paramValues.Length; i++)
            {
                //xrTableCell18.Text += paramValues[i].ToString();
                salesman += paramValues[i].ToString();
                if (i < paramValues.Length - 1)
                    //xrTableCell18.Text += ",";
                    salesman += ",";
            }
            if (report.Parameters["Salesman"].Value.ToString() == "System.String[]")
            {
                //report.Parameters["Salesman"].Value = xrTableCell18.Text;
                report.Parameters["Salesman"].Value = salesman;
            }



            string x = report.Parameters["Month"].Value.ToString();
            switch (x)
            {
                case "1":
                    xrLabel1.Text = "January [Parameters.Year] / Item Category:  [Parameters.ItemCategory] / Salesman:  " + salesman +" / Group By:  [Parameters.GroupBy]";
                    //BilddatenPictureBox.DataBindings.Add(New XRBinding("Text", Nothing, "Kopf.Federbild")
                    //BilddatenPictureBox.DataBindings.Add(New XtraReport.UI.XRBinding[]("Text", Nothing, "Kopf.Federbild"));

                    //this.xrTableCell20.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
                    //new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_CollectionPeriodDetail.TransDoc", "{0:MM/dd/yy}")});

                    break;
                case "2":
                    xrLabel1.Text = "February [Parameters.Year] / Item Category:  [Parameters.ItemCategory] / Salesman:  " + salesman + " / Group By:  [Parameters.GroupBy]";
                    break;
                case "3":
                    xrLabel1.Text = "March [Parameters.Year] / Item Category:  [Parameters.ItemCategory] / Salesman:  " + salesman + " / Group By:  [Parameters.GroupBy]";
                    break;
                case "4":
                    xrLabel1.Text = "April [Parameters.Year] / Item Category:  [Parameters.ItemCategory] / Salesman:  " + salesman + " / Group By:  [Parameters.GroupBy]";
                    break;
                case "5":
                    xrLabel1.Text = "May [Parameters.Year] / Item Category:  [Parameters.ItemCategory] / Salesman:  " + salesman + " / Group By:  [Parameters.GroupBy]";
                    break;
                case "6":
                    xrLabel1.Text = "June [Parameters.Year] / Item Category:  [Parameters.ItemCategory] / Salesman:  " + salesman + " / Group By:  [Parameters.GroupBy]";
                    break;
                case "7":
                    xrLabel1.Text = "July [Parameters.Year] / Item Category:  [Parameters.ItemCategory] / Salesman:  " + salesman + " / Group By:  [Parameters.GroupBy]";
                    break;
                case "8":
                    xrLabel1.Text = "August [Parameters.Year] / Item Category:  [Parameters.ItemCategory] / Salesman:  " + salesman + " / Group By:  [Parameters.GroupBy]";
                    break;
                case "9":
                    xrLabel1.Text = "September [Parameters.Year] / Item Category:  [Parameters.ItemCategory] / Salesman:  " + salesman + " / Group By:  [Parameters.GroupBy]";
                    break;
                case "10":
                    xrLabel1.Text = "October [Parameters.Year] / Item Category:  [Parameters.ItemCategory] / Salesman:  " + salesman + " / Group By:  [Parameters.GroupBy]";
                    break;
                case "11":
                    xrLabel1.Text = "November [Parameters.Year] / Item Category:  [Parameters.ItemCategory] / Salesman:  " + salesman + " / Group By:  [Parameters.GroupBy]";
                    break;
                default:
                    xrLabel1.Text = "December [Parameters.Year] / Item Category:  [Parameters.ItemCategory] / Salesman:  " + salesman + " / Group By:  [Parameters.GroupBy]";
                    break;
            }

        }
    }
}
