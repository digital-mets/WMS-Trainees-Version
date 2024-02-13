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
    public partial class R_APListingDetailed : DevExpress.XtraReports.UI.XtraReport
    {
        public R_APListingDetailed()
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

        private void R_APListingDetailed_DataSourceDemanded(object sender, EventArgs e)
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



            string docdatefrom = (string)report.Parameters["DocDateFrom"].Value;
            string docdatefromheader = "";
            if (String.IsNullOrEmpty(docdatefrom))
            {
                //report.Parameters["CVDateFrom_Actual"].Type(string);
                report.Parameters["DocDateFrom_Actual"].Value = "01/01/0001";
                docdatefromheader = "DocDateFrom: NULL";
            }
            else
            {
                DateTime docdatefromactual = DateTime.Parse(docdatefrom);
                report.Parameters["DocDateFrom_Actual"].Value = docdatefromactual;
                docdatefromheader = "DocDateFrom: [Parameters.DocDateFrom_Actual!MMMM dd, yyyy]";
            }


            string docdateto = (string)report.Parameters["DocDateTo"].Value;
            string docdatetoheader = "";
            if (String.IsNullOrEmpty(docdateto))
            {
                report.Parameters["DocDateTo_Actual"].Value = "01/01/0001";
                docdatetoheader = "DocDateTo: NULL";
            }
            else
            {
                DateTime docdatetoactual = DateTime.Parse(docdateto);
                report.Parameters["DocDateTo_Actual"].Value = docdatetoactual;
                docdatetoheader = "DocDateTo: [Parameters.DocDateTo_Actual!MMMM dd, yyyy]";
            }


            string duedatefrom = (string)report.Parameters["DueDateFrom"].Value;
            string duedatefromheader = "";
            if (String.IsNullOrEmpty(duedatefrom))
            {
                report.Parameters["DueDateFrom_Actual"].Value = "01/01/0001";
                duedatefromheader = "DueDateFrom: NULL";
            }
            else
            {
                DateTime duedatefromactual = DateTime.Parse(duedatefrom);
                report.Parameters["DueDateFrom_Actual"].Value = duedatefromactual;
                duedatefromheader = "DueDateFrom: [Parameters.DueDateFrom_Actual!MMMM dd, yyyy]";
            }


            string duedateto = (string)report.Parameters["DueDateTo"].Value;
            string duedatetoheader = "";
            if (String.IsNullOrEmpty(duedateto))
            {
                report.Parameters["DueDateTo_Actual"].Value = "01/01/0001";
                duedatetoheader = "DueDateTo: NULL";
            }
            else
            {
                DateTime duedatetoactual = DateTime.Parse(duedateto);
                report.Parameters["DueDateTo_Actual"].Value = duedatetoactual;
                duedatetoheader = "DueDateTo: [Parameters.DueDateTo_Actual!MMMM dd, yyyy]";
            }

            //xrLabel1.Text = "Cut-Off Date: [Parameters.CutOff!MMMM dd, yyyy] / BusinessAccount:  " + businessaccount + " / Customer:  " + customer + " / Salesman:  " + salesman + " / GL Account:  " + glaraccount + " / GroupBy:  [Parameters.GroupBy] / ShowDueDate:  " + showduedate + " / ShowTerms:  " + showterms;
            //xrLabel4.Text = "CheckStatus: [Parameters.CheckStatus] / ClearingStatus: [Parameters.ClearingStatus] / ReleaseStatus: [Parameters.ReleaseStatus] / Supplier: " + supplier + " / BankAccount: [Parameters.BankAccount / OrderBy: [Parameters.OrderBy] / CVDocDateFrom: [Parameters.CVDateFrom!MMMM dd, yyyy] / CVDocDateTo: [Parameters.CVDateTo!MMMM dd, yyyy] / CheckDateFrom: [Parameters.CheckDateFrom!MMMM dd, yyyy] / CheckDateTo: [Parameters.CheckDateTo!MMMM dd, yyyy] / ReleaseDateFrom: [Parameters.ReleaseDateFrom!MMMM dd, yyyy] / ReleaseDateTo: [Parameters.ReleaseDateTo!MMMM dd, yyyy] / DateClearedFrom: [Parameters.DateClearedFrom!MMMM dd, yyyy] / DateClearedTo: [Parameters.DateClearedTo!MMMM dd, yyyy] / CheckNumberFrom: [Parameters.CheckNumberFrom!MMMM dd, yyyy] / CheckNumberTo: [Parameters.CheckNumberTo]";
            
            //xrLabel4.Text = docdatefromheader + " / " + docdatetoheader + " / " + duedatefromheader + " / " + duedatetoheader + 
            //    Environment.NewLine + "Supplier: " + supplier + " / TransType: [Parameters.TransType] / ReportInformation: [Parameters.ReportInfo] / ShowDetail: [Parameters.ShowDetail] / Summary: [Parameters.Summary]";
            xrLabel4.Text = docdatefromheader + " / " + docdatetoheader + " / " + duedatefromheader + " / " + duedatetoheader +
                Environment.NewLine + "Supplier: " + supplier + " / TransType: [Parameters.TransType] / ReportInformation: [Parameters.ReportInfo] ";

            //if(report.Parameters["Summary"].Value.ToString().ToUpper() == "TRUE")
            //{
            //    Detail1.DeleteColumn(Detail1Col1);
            //    Detail1.DeleteColumn(Detail1Col2);
            //    Detail1.DeleteColumn(Detail1Col3);
            //    Detail1.DeleteColumn(Detail1Col4);
            //    Detail1.DeleteColumn(Detail1Col5);
            //    Detail1.DeleteColumn(Detail1Col6);
            //    Detail.HeightF = 25.00F;
            //    //this.Detail1.LocationFloat = new DevExpress.Utils.PointFloat(84.99977F, 0F);


            //}


            //if (report.Parameters["ShowDetail"].Value.ToString().ToUpper() == "FALSE")
            //{
            //    Detail.HeightF = 25.00F;
            //}

            //if(String.IsNullOrEmpty(xrTableCell30.Text) && String.IsNullOrEmpty(xrTableCell31.Text) && 
            //    String.IsNullOrEmpty(xrTableCell32.Text) && String.IsNullOrEmpty(xrTableCell33.Text) && 
            //    String.IsNullOrEmpty(xrTableCell34.Text))
            //{
            //    Detail.HeightF = 0F;
            //}
            //else
            //{
            //    Detail.HeightF = 25.00F;
            //}

        }
    }
}
