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
    public partial class R_APListing : DevExpress.XtraReports.UI.XtraReport
    {
        public R_APListing()
        {
            InitializeComponent();

            if (HttpContext.Current.Session["ConnString"] != null)
                sqlDataSource1.Connection.ConnectionString = HttpContext.Current.Session["ConnString"].ToString();
        }

        private void R_ListOfCheckIssued_ParametersRequestBeforeShow(object sender, DevExpress.XtraReports.Parameters.ParametersRequestEventArgs e)
        {
            //XtraReport report = (XtraReport)Report;

            //DataTable getBegDate = Gears.RetriveData2("SELECT Value FROM IT.SystemSettings WHERE Code = 'SYSBEGDATE'", HttpContext.Current.Session["ConnString"].ToString());

            //string[] BegDate = { getBegDate.Rows[0]["Value"].ToString() };
            //DateTime BeginningDate = Convert.ToDateTime(BegDate[0]);

            //report.Parameters["CheckDateFrom"].Value = BeginningDate;
            //report.Parameters["CheckDateTo"].Value = DateTime.Now.ToString();

            ////report.Parameters["CVDateFrom"].Value = BeginningDate;
            //report.Parameters["CVDateTo"].Value = DateTime.Now.ToString();

            //report.Parameters["DateClearedFrom"].Value = BeginningDate;
            //report.Parameters["DateClearedTo"].Value = DateTime.Now.ToString();

            //report.Parameters["ReleaseDateFrom"].Value = BeginningDate;
            //report.Parameters["ReleaseDateTo"].Value = DateTime.Now.ToString();
        }

        private void R_APListing_DataSourceDemanded(object sender, EventArgs e)
        {
            XtraReport report = (XtraReport)Report;
            string supplier = "";

            //Bank
            string[] paramValues2 = report.Parameters["Supplier"].Value as string[];
            //xrTableCell68.Text = string.Empty;
            for (int i = 0; i < paramValues2.Length; i++)
            {
                supplier += paramValues2[i].ToString();
                if (i < paramValues2.Length - 1)
                    supplier += ",";
            }

            // Passing Value Of Labels To Parameter.
            if (report.Parameters["Supplier"].Value.ToString() == "System.String[]")
            {
                report.Parameters["Supplier"].Value = supplier;
            }



            //string docdatefrom = (string)report.Parameters["DocDateFrom"].Value;
            //string docdatefromheader = "";
            //if (String.IsNullOrEmpty(docdatefrom))
            //{
            //    //report.Parameters["CVDateFrom_Actual"].Type(string);
            //    report.Parameters["DocDateFrom_Actual"].Value = "01/01/0001";
            //    docdatefromheader = "DocDateFrom: NULL";
            //}
            //else
            //{
            //    DateTime docdatefromactual = DateTime.Parse(docdatefrom);
            //    report.Parameters["DocDateFrom_Actual"].Value = docdatefromactual;
            //    docdatefromheader = "DocDateFrom: [Parameters.DocDateFrom_Actual!MMMM dd, yyyy]";
            //}


            //string docdateto = (string)report.Parameters["DocDateTo"].Value;
            //string docdatetoheader = "";
            //if (String.IsNullOrEmpty(docdateto))
            //{
            //    report.Parameters["DocDateTo_Actual"].Value = "01/01/0001";
            //    docdatetoheader = "DocDateTo: NULL";
            //}
            //else
            //{
            //    DateTime docdatetoactual = DateTime.Parse(docdateto);
            //    report.Parameters["DocDateTo_Actual"].Value = docdatetoactual;
            //    docdatetoheader = "DocDateTo: [Parameters.DocDateTo_Actual!MMMM dd, yyyy]";
            //}


            //string duedatefrom = (string)report.Parameters["DueDateFrom"].Value;
            //string duedatefromheader = "";
            //if (String.IsNullOrEmpty(duedatefrom))
            //{
            //    report.Parameters["DueDateFrom_Actual"].Value = "01/01/0001";
            //    duedatefromheader = "DueDateFrom: NULL";
            //}
            //else
            //{
            //    DateTime duedatefromactual = DateTime.Parse(duedatefrom);
            //    report.Parameters["DueDateFrom_Actual"].Value = duedatefromactual;
            //    duedatefromheader = "DueDateFrom: [Parameters.DueDateFrom_Actual!MMMM dd, yyyy]";
            //}


            //string duedateto = (string)report.Parameters["DueDateTo"].Value;
            //string duedatetoheader = "";
            //if (String.IsNullOrEmpty(duedateto))
            //{
            //    report.Parameters["DueDateTo_Actual"].Value = "01/01/0001";
            //    duedatetoheader = "DueDateTo: NULL";
            //}
            //else
            //{
            //    DateTime duedatetoactual = DateTime.Parse(duedateto);
            //    report.Parameters["DueDateTo_Actual"].Value = duedatetoactual;
            //    duedatetoheader = "DueDateTo: [Parameters.DueDateTo_Actual!MMMM dd, yyyy]";
            //}

            //string docdatefrom = (string)report.Parameters["DocDateFrom"].Value;
            string docdatefromheader = "";
            if (String.IsNullOrEmpty(report.Parameters["DocDateFrom"].Value.ToString()))
            {
                //report.Parameters["CVDateFrom_Actual"].Type(string);
                report.Parameters["DocDateFrom"].Value = "01/01/0001";
                docdatefromheader = "DocDateFrom: NULL";
            }
            else
            {
                //DateTime docdatefromactual = DateTime.Parse(docdatefrom);
                //report.Parameters["DocDateFrom_Actual"].Value = docdatefromactual;
                docdatefromheader = "DocDateFrom: [Parameters.DocDateFrom!MMMM dd, yyyy]";
            }

            string docdatetoheader = "";
            if (String.IsNullOrEmpty(report.Parameters["DocDateTo"].Value.ToString()))
            {
                report.Parameters["DocDateTo"].Value = "01/01/0001";
                docdatetoheader = "DocDateTo: NULL";
            }
            else
            {
                docdatetoheader = "DocDateTo: [Parameters.DocDateTo!MMMM dd, yyyy]";
            }

            string duedatefromheader = "";
            if (String.IsNullOrEmpty(report.Parameters["DueDateFrom"].Value.ToString()))
            {
                report.Parameters["DueDateFrom"].Value = "01/01/0001";
                duedatefromheader = "DueDateFrom: NULL";
            }
            else
            {
                duedatefromheader = "DueDateFrom: [Parameters.DueDateFrom!MMMM dd, yyyy]";
            }

            string duedatetoheader = "";
            if (String.IsNullOrEmpty(report.Parameters["DueDateTo"].Value.ToString()))
            {
                report.Parameters["DueDateTo"].Value = "01/01/0001";
                duedatetoheader = "DueDateTo: NULL";
            }
            else
            {
                duedatetoheader = "DueDateTo: [Parameters.DueDateTo!MMMM dd, yyyy]";
            }

            //xrLabel1.Text = "Cut-Off Date: [Parameters.CutOff!MMMM dd, yyyy] / BusinessAccount:  " + businessaccount + " / Customer:  " + customer + " / Salesman:  " + salesman + " / GL Account:  " + glaraccount + " / GroupBy:  [Parameters.GroupBy] / ShowDueDate:  " + showduedate + " / ShowTerms:  " + showterms;
            //xrLabel4.Text = "CheckStatus: [Parameters.CheckStatus] / ClearingStatus: [Parameters.ClearingStatus] / ReleaseStatus: [Parameters.ReleaseStatus] / Supplier: " + supplier + " / BankAccount: [Parameters.BankAccount / OrderBy: [Parameters.OrderBy] / CVDocDateFrom: [Parameters.CVDateFrom!MMMM dd, yyyy] / CVDocDateTo: [Parameters.CVDateTo!MMMM dd, yyyy] / CheckDateFrom: [Parameters.CheckDateFrom!MMMM dd, yyyy] / CheckDateTo: [Parameters.CheckDateTo!MMMM dd, yyyy] / ReleaseDateFrom: [Parameters.ReleaseDateFrom!MMMM dd, yyyy] / ReleaseDateTo: [Parameters.ReleaseDateTo!MMMM dd, yyyy] / DateClearedFrom: [Parameters.DateClearedFrom!MMMM dd, yyyy] / DateClearedTo: [Parameters.DateClearedTo!MMMM dd, yyyy] / CheckNumberFrom: [Parameters.CheckNumberFrom!MMMM dd, yyyy] / CheckNumberTo: [Parameters.CheckNumberTo]";
            xrLabel4.Text = docdatefromheader + " / " + docdatetoheader + " / " + duedatefromheader + " / " + duedatetoheader +
                Environment.NewLine + "Supplier: " + supplier + " / TransType: [Parameters.TransType] / ReportInformation: [Parameters.ReportInfo] / ShowDetail: [Parameters.ShowDetail] / Summary: [Parameters.Summary]";
       

            //if(report.Parameters["Summary"].Value.ToString().ToUpper() == "TRUE")
            //{
            //    Detail1.DeleteColumn(Detail1Col1);
            //    Detail1.DeleteColumn(Detail1Col2);
            //    Detail1.DeleteColumn(Detail1Col3);
            //    Detail1.DeleteColumn(Detail1Col4);
            //    Detail1.DeleteColumn(Detail1Col5);
            //    Detail1.DeleteColumn(Detail1Col6);
            //    this.Detail2.LocationFloat = new DevExpress.Utils.PointFloat(85F, 0F);
            //    Detail.HeightF = 25.00F;
            //    //this.Detail1.LocationFloat = new DevExpress.Utils.PointFloat(84.99977F, 0F);


            //}

            //if (report.Parameters["ShowDetail"].Value.ToString().ToUpper() == "FALSE")
            //{
            //    Detail2.DeleteColumn(Detail2Col1);
            //    Detail2.DeleteColumn(Detail2Col2);
            //    Detail.HeightF = 25.00F;
            //}
            //if (report.Parameters["ShowDetail"].Value.ToString().ToUpper() == "TRUE")
            //{

            //    this.Detail2Col2.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            //        new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_AccountsPayableListing.UseDetail")});
            //    Detail.HeightF = 50.00F;
            //}

        }

        private void Detail2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XtraReport report = (XtraReport)Report;

            if (report.Parameters["ShowDetail"].Value.ToString().ToUpper() == "FALSE")
            {
                //Detail2.DeleteColumn(Detail2Col1);
                //Detail2.DeleteColumn(Detail2Col2);
                //Detail2.DeleteRow();
                e.Cancel = true;
                Detail2.LocationFloat = new DevExpress.Utils.PointFloat(50F, 0F);
                Detail.HeightF = 25.00F;
            }
            if (report.Parameters["ShowDetail"].Value.ToString().ToUpper() == "TRUE")
            {

                //this.Detail2Col2.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
                //    new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_AccountsPayableListing.UseDetail")});

                e.Cancel = false;
                if (report.Parameters["Summary"].Value.ToString().ToUpper() == "FALSE")
                {
                    Detail2.LocationFloat = new DevExpress.Utils.PointFloat(50F, 25F);
                    Detail.HeightF = 50.00F;
                }
                else
                {
                    Detail2.LocationFloat = new DevExpress.Utils.PointFloat(50F, 0F);
                    Detail.HeightF = 25.00F;
                }
            }
        }

        private void Detail1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XtraReport report = (XtraReport)Report;

            if (report.Parameters["Summary"].Value.ToString().ToUpper() == "FALSE")
            {
                //Detail2.DeleteColumn(Detail2Col1);
                //Detail2.DeleteColumn(Detail2Col2);
                //Detail2.DeleteRow();
                e.Cancel = false;
            }
            if (report.Parameters["Summary"].Value.ToString().ToUpper() == "TRUE")
            {
                //this.Detail2Col2.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
                //    new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_AccountsPayableListing.UseDetail")});

                e.Cancel = true;
            }
        }
    }
}
