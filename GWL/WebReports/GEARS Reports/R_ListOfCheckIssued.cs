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
    public partial class R_ListOfCheckIssued : DevExpress.XtraReports.UI.XtraReport
    {
        public R_ListOfCheckIssued()
        {
            InitializeComponent();

            if (HttpContext.Current.Session["ConnString"] != null)
                sqlDataSource1.Connection.ConnectionString = HttpContext.Current.Session["ConnString"].ToString();
        }

        private void R_ListOfCheckIssued_DataSourceDemanded(object sender, EventArgs e)
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



            string cvdatefrom = (string)report.Parameters["CVDateFrom"].Value;
            string cvdatefromheader = "";
            if (String.IsNullOrEmpty(cvdatefrom))
            {
                //report.Parameters["CVDateFrom_Actual"].Type(string);
                report.Parameters["CVDateFrom_Actual"].Value = "01/01/0001";
                cvdatefromheader = "CVDateFrom: NULL";
            }
            else
            {
                DateTime cvdatefromactual = DateTime.Parse(cvdatefrom);
                report.Parameters["CVDateFrom_Actual"].Value = cvdatefromactual;
                cvdatefromheader = "CVDateFrom: [Parameters.CVDateFrom_Actual!MMMM dd, yyyy]";
            }


            string cvdateto = (string)report.Parameters["CVDateTo"].Value;
            string cvdatetoheader = "";
            if (String.IsNullOrEmpty(cvdateto))
            {
                report.Parameters["CVDateTo_Actual"].Value = "01/01/0001";
                cvdatetoheader = "CVDateTo: NULL";
            }
            else
            {
                DateTime cvdatetoactual = DateTime.Parse(cvdateto);
                report.Parameters["CVDateTo_Actual"].Value = cvdatetoactual;
                cvdatetoheader = "CVDateTo: [Parameters.CVDateTo_Actual!MMMM dd, yyyy]";
            }


            string checkdatefrom = (string)report.Parameters["CheckDateFrom"].Value;
            string checkdatefromheader = "";
            if (String.IsNullOrEmpty(checkdatefrom))
            {
                report.Parameters["CheckDateFrom_Actual"].Value = "01/01/0001";
                checkdatefromheader = "CheckDateFrom: NULL";
            }
            else
            {
                DateTime checkdatefromactual = DateTime.Parse(checkdatefrom);
                report.Parameters["CheckDateFrom_Actual"].Value = checkdatefromactual;
                checkdatefromheader = "CheckDateFrom: [Parameters.CheckDateFrom_Actual!MMMM dd, yyyy]";
            }


            string checkdateto = (string)report.Parameters["CheckDateTo"].Value;
            string checkdatetoheader = "";
            if (String.IsNullOrEmpty(checkdateto))
            {
                report.Parameters["CheckDateTo_Actual"].Value = "01/01/0001";
                checkdatetoheader = "CheckDateTo: NULL";
            }
            else
            {
                DateTime checkdatetoactual = DateTime.Parse(checkdateto);
                report.Parameters["CheckDateTo_Actual"].Value = checkdatetoactual;
                checkdatetoheader = "CheckDateTo: [Parameters.CheckDateTo_Actual!MMMM dd, yyyy]";
            }


            string releasedatefrom = (string)report.Parameters["ReleaseDateFrom"].Value;
            string releasedatefromheader = "";
            if (String.IsNullOrEmpty(releasedatefrom))
            {
                report.Parameters["ReleaseDateFrom_Actual"].Value = "01/01/0001";
                releasedatefromheader = "ReleaseDateFrom: NULL";
            }
            else
            {
                DateTime releasedatefromactual = DateTime.Parse(releasedatefrom);
                report.Parameters["ReleaseDateFrom_Actual"].Value = releasedatefromactual;
                releasedatefromheader = "ReleaseDateFrom: [Parameters.ReleaseDateFrom_Actual!MMMM dd, yyyy]";
            }


            string releasedateto = (string)report.Parameters["ReleaseDateTo"].Value;
            string releasedatetoheader = "";
            if (String.IsNullOrEmpty(releasedateto))
            {
                report.Parameters["ReleaseDateTo_Actual"].Value = "01/01/0001";
                releasedatetoheader = "ReleaseDateTo: NULL";
            }
            else
            {
                DateTime releasedatetoactual = DateTime.Parse(releasedateto);
                report.Parameters["ReleaseDateTo_Actual"].Value = releasedatetoactual;
                releasedatetoheader = "ReleaseDateTo: [Parameters.ReleaseDateTo_Actual!MMMM dd, yyyy]";
            }


            string dateclearedfrom = (string)report.Parameters["DateClearedFrom"].Value;
            string dateclearedfromheader = "";
            if (String.IsNullOrEmpty(dateclearedfrom))
            {
                report.Parameters["DateClearedFrom_Actual"].Value = "01/01/0001";
                dateclearedfromheader = "DateClearedFrom: NULL";
            }
            else
            {
                DateTime dateclearedfromactual = DateTime.Parse(dateclearedfrom);
                report.Parameters["DateClearedFrom_Actual"].Value = dateclearedfromactual;
                dateclearedfromheader = "DateClearedFrom: [Parameters.DateClearedFrom_Actual!MMMM dd, yyyy]";
            }

            string dateclearedto = (string)report.Parameters["DateClearedTo"].Value;
            string dateclearedtoheader = "";
            if (String.IsNullOrEmpty(dateclearedto))
            {
                report.Parameters["DateClearedTo_Actual"].Value = "01/01/0001";
                dateclearedtoheader = "DateClearedTo: NULL";
            }
            else
            {
                DateTime dateclearedtoactual = DateTime.Parse(dateclearedto);
                report.Parameters["DateClearedTo_Actual"].Value = dateclearedtoactual;
                dateclearedtoheader = "DateClearedTo: [Parameters.DateClearedTo_Actual!MMMM dd, yyyy]";
            }

            //xrLabel1.Text = "Cut-Off Date: [Parameters.CutOff!MMMM dd, yyyy] / BusinessAccount:  " + businessaccount + " / Customer:  " + customer + " / Salesman:  " + salesman + " / GL Account:  " + glaraccount + " / GroupBy:  [Parameters.GroupBy] / ShowDueDate:  " + showduedate + " / ShowTerms:  " + showterms;
            //xrLabel4.Text = "CheckStatus: [Parameters.CheckStatus] / ClearingStatus: [Parameters.ClearingStatus] / ReleaseStatus: [Parameters.ReleaseStatus] / Supplier: " + supplier + " / BankAccount: [Parameters.BankAccount / OrderBy: [Parameters.OrderBy] / CVDocDateFrom: [Parameters.CVDateFrom!MMMM dd, yyyy] / CVDocDateTo: [Parameters.CVDateTo!MMMM dd, yyyy] / CheckDateFrom: [Parameters.CheckDateFrom!MMMM dd, yyyy] / CheckDateTo: [Parameters.CheckDateTo!MMMM dd, yyyy] / ReleaseDateFrom: [Parameters.ReleaseDateFrom!MMMM dd, yyyy] / ReleaseDateTo: [Parameters.ReleaseDateTo!MMMM dd, yyyy] / DateClearedFrom: [Parameters.DateClearedFrom!MMMM dd, yyyy] / DateClearedTo: [Parameters.DateClearedTo!MMMM dd, yyyy] / CheckNumberFrom: [Parameters.CheckNumberFrom!MMMM dd, yyyy] / CheckNumberTo: [Parameters.CheckNumberTo]";
            xrLabel4.Text = "CheckStatus: [Parameters.CheckStatus] / ClearingStatus: [Parameters.ClearingStatus] / ReleaseStatus: [Parameters.ReleaseStatus] / Supplier: " + supplier + " / BankAccount: [Parameters.BankAccount] / OrderBy: [Parameters.OrderBy] / " + cvdatefromheader + " / " + cvdatetoheader + " / " + checkdatefromheader + " / " + checkdatetoheader + " / " + releasedatefromheader + " / " + releasedatetoheader + " / " + dateclearedfromheader + " / " + dateclearedtoheader + " / CheckNumberFrom: [Parameters.CheckNumberFrom!MMMM dd, yyyy] / CheckNumberTo: [Parameters.CheckNumberTo]";
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
    }
}
