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
    public partial class R_ListofUnpostedTransactionV2 : DevExpress.XtraReports.UI.XtraReport
    {
        public R_ListofUnpostedTransactionV2()
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

        private void R_ListofUnpostedTransaction_DataSourceDemanded(object sender, EventArgs e)
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
                this.xrTableCell19.WidthF = 75.00F;

                // this.xrTableCell19.Padding = new DevExpress.XtraPrinting.PaddingInfo(10, 0, 0, 0, 100F);
                this.xrTableCell27.Text = "Employee:";

                this.xrTableCell19.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] { new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ListofUnpostedTransaction_ver2.TransType") });

                this.xrTableCell20.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] 
                { new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ListofUnpostedTransaction_ver2.Prompt") });
            }

            if (report.Parameters["SummaryBy"].Value.ToString() == "Transaction Type")
            {

                this.xrTableCell19.WidthF = 300.00F;

                this.xrTableCell27.Text = "Transaction:";
                this.xrTableCell19.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] { new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ListofUnpostedTransaction_ver2.FullName") });
                // this.xrTableCell19.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                // this.xrTableCell19.Padding = new DevExpress.XtraPrinting.PaddingInfo(10, 0, 0, 0, 100F);

                this.xrTableCell20.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] 
                { new DevExpress.XtraReports.UI.XRBinding("Text", null, "") });
            }

            this.xrLabel1.Text = "Report Period From:  [Parameters.DateFrom!MM/dd/yy] To: [Parameters.DateTo!MM/dd/yy] / FuncGroup:  " + funcgroup + " / SummaryBy:  [Parameters.SummaryBy]";
        }

        private void R_ListofUnpostedTransaction_ParametersRequestBeforeShow(object sender, DevExpress.XtraReports.Parameters.ParametersRequestEventArgs e)
        {
            XtraReport report = (XtraReport)Report;
            DataTable getYear = Gears.RetriveData2("SELECT Value FROM it.systemsettings where Code = 'CYEAR'", HttpContext.Current.Session["ConnString"].ToString());
            DataTable getMonth = Gears.RetriveData2("SELECT Value FROM it.systemsettings where Code = 'CMONTH'", HttpContext.Current.Session["ConnString"].ToString());
            //int x = 0;

            int Year =  Convert.ToInt32(getYear.Rows[0]["Value"].ToString());
            int Month = Convert.ToInt32(getMonth.Rows[0]["Value"].ToString());
            //2017
            DateTime DateFr = new DateTime(Year, Month, 1);
            DateTime DateTo = DateFr.AddMonths(1).AddDays(-1);

            report.Parameters["DateFrom"].Value = DateFr;
            report.Parameters["DateTo"].Value = DateTo;
            //2017

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
