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
    public partial class R_ListofUnsubmittedTransaction : DevExpress.XtraReports.UI.XtraReport
    {
        public R_ListofUnsubmittedTransaction()
        {
            InitializeComponent();

            if (HttpContext.Current.Session["ConnString"] != null)
                sqlDataSource1.Connection.ConnectionString = HttpContext.Current.Session["ConnString"].ToString();



        }

        private void button1_Click(object sender, EventArgs e)
        {
            //R_BizPartnerLedger rep = new R_BizPartnerLedger();
            //rep.ShowPreviewDialog();
        }

        private void ReportHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }

        private void R_ListofUnsubmittedTransaction_DataSourceDemanded(object sender, EventArgs e)
        {
            XtraReport report = (XtraReport)Report;

            //GL Account Code
            string funcgroup = "";
            string[] paramValues = report.Parameters["FuncGroup"].Value as string[];
            //xrTableCell13.Text = string.Empty;
            for (int i = 0; i < paramValues.Length; i++)
            {
                //xrTableCell13.Text += paramValues[i].ToString();
                funcgroup += paramValues[i].ToString();
                if (i < paramValues.Length - 1)
                    funcgroup += ",";
                    //xrTableCell13.Text += ",";
            }


            if (report.Parameters["FuncGroup"].Value.ToString() == "System.String[]")
            {
                //report.Parameters["FuncGroup"].Value = xrTableCell13.Text;
                report.Parameters["FuncGroup"].Value = funcgroup;
            }


            if (report.Parameters["SummaryBy"].Value.ToString() == "Added By User")
            {
                this.xrTableCell19.WidthF = 115.00F;

                this.xrTableCell19.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
                //this.xrTableCell20.WidthF = 240.00F;
                xrTableCell27.Text = "Employee Name:";

                this.xrTableCell19.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ListofUnsubmittedTransaction.TransType")});

                this.xrTableCell20.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ListofUnsubmittedTransaction.Prompt")});

                
            }

            if (report.Parameters["SummaryBy"].Value.ToString() == "Transaction Type")
            {

                this.xrTableCell19.WidthF = 300.00F;

                xrTableCell27.Text = "Transaction:";
                this.xrTableCell19.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ListofUnsubmittedTransaction.FullName")});
                this.xrTableCell19.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                this.xrTableCell19.Padding = new DevExpress.XtraPrinting.PaddingInfo(10, 0, 0, 0, 100F);

                this.xrTableCell20.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "")});
            }



            xrLabel1.Text = "Report Period From:  [Parameters.DateFrom!MM/dd/yy] To: [Parameters.DateTo!MM/dd/yy] / FuncGroup:  " + funcgroup + " / SummaryBy:  [Parameters.SummaryBy]";

        }

        private void R_ListofUnsubmittedTransaction_ParametersRequestBeforeShow(object sender, DevExpress.XtraReports.Parameters.ParametersRequestEventArgs e)
        {
            XtraReport report = (XtraReport)Report;
            DataTable getYear = Gears.RetriveData2("SELECT Value FROM it.systemsettings where Code = 'CYEAR'", HttpContext.Current.Session["ConnString"].ToString());
            DataTable getMonth = Gears.RetriveData2("SELECT Value FROM it.systemsettings where Code = 'CMONTH'", HttpContext.Current.Session["ConnString"].ToString());
            int x = 0;
            


            int Year =  Convert.ToInt32(getYear.Rows[0]["Value"].ToString());
            int Month = Convert.ToInt32(getMonth.Rows[0]["Value"].ToString());
            if (Month == 12)
            {
                x = 1;
            }
            if (Month != 12)
            {
                x = Month + 1;
            }

            DateTime AddMonth = new DateTime(Year, x, 1);
            DateTime LastDay = AddMonth.AddDays(-1);
            report.Parameters["DateFrom"].Value = Month + "/01/" + Year;
            report.Parameters["DateTo"].Value = LastDay;
            //DateTime AddMonth2 = new DateTime(year, x, 1);
            //DateTime LastDayCurrent = AddMonth2.AddDays(-1);

            


            //DevExpress.DataAccess.Sql.CustomSqlQuery customSqlQuery7 = new DevExpress.DataAccess.Sql.CustomSqlQuery();
            //customSqlQuery7.Name = "GLARAccount";
            //customSqlQuery7.Sql = "SELECT AccountCode FROM Accounting.ChartOfAccount WHERE AccountCode\r\n\t\t\t BETWEEN " +
            // "\'1000\' AND \'2999\'";
        }

        private void xrTableCell19_HtmlItemCreated(object sender, HtmlEventArgs e)
        {
            XtraReport report = (XtraReport)Report;

            if (report.Parameters["SummaryBy"].Value.ToString() == "Added By User")
            {
                e.ContentCell.Style.Add("border-right", "1px solid #000000 !important");
            } 
        }
    }
}
