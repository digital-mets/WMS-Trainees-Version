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
    public partial class R_ListofCheckReceived : DevExpress.XtraReports.UI.XtraReport
    {
        public R_ListofCheckReceived()
        {
            InitializeComponent();

            if (HttpContext.Current.Session["ConnString"] != null)
                sqlDataSource1.Connection.ConnectionString = HttpContext.Current.Session["ConnString"].ToString();


        }

        private void R_ListofCheckReceived_ParametersRequestBeforeShow(object sender, DevExpress.XtraReports.Parameters.ParametersRequestEventArgs e)
        {
            //XtraReport report = (XtraReport)Report;

            //DataTable getBegDate = Gears.RetriveData2("SELECT Value FROM IT.SystemSettings WHERE Code = 'SYSBEGDATE'", HttpContext.Current.Session["ConnString"].ToString());

            //string[] BegDate = { getBegDate.Rows[0]["Value"].ToString() };
            //DateTime BeginningDate = Convert.ToDateTime(BegDate[0]);

            //report.Parameters["DocDateFrom"].Value = BeginningDate;
            //report.Parameters["DocDateTo"].Value = DateTime.Now.ToString();

            //report.Parameters["CheckDateFrom"].Value = BeginningDate;
            //report.Parameters["CheckDateTo"].Value = DateTime.Now.ToString();

            //report.Parameters["DepositDateFrom"].Value = BeginningDate;
            //report.Parameters["DepositDateTo"].Value = DateTime.Now.ToString();
        }

        private void R_ListofCheckReceived_DataSourceDemanded(object sender, EventArgs e)
        {
            XtraReport report = (XtraReport)Report;
            string bank = "";
            string businessaccount = "";
            string customer = "";
            string salesman = "";

            Header1.Visible = false;
            Header2.Visible = false;
            Header3.Visible = false;


            Detail1.Visible = false;
            Detail2.Visible = false;
            Detail3.Visible = false;

            // Unbind All Data
            this.Detail1.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
     			new DevExpress.XtraReports.UI.XRBinding("Text", null, "")});
            this.Detail2.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
     			new DevExpress.XtraReports.UI.XRBinding("Text", null, "")});
            this.Detail3.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
     			new DevExpress.XtraReports.UI.XRBinding("Text", null, "")});

            //Bank
            string[] paramValues2 = report.Parameters["Bank"].Value as string[];
            //xrTableCell64.Text = string.Empty;
            for (int i = 0; i < paramValues2.Length; i++)
            {
                bank += paramValues2[i].ToString();
                if (i < paramValues2.Length - 1)
                    bank += ",";
            }

            //Business Account
            string[] paramValues3 = report.Parameters["BusinessAccount"].Value as string[];
            //xrTableCell65.Text = string.Empty;
            for (int i = 0; i < paramValues3.Length; i++)
            {
                businessaccount += paramValues3[i].ToString();
                if (i < paramValues3.Length - 1)
                    businessaccount += ",";
            }

            //Customer
            string[] paramValues4 = report.Parameters["Customer"].Value as string[];
            //xrTableCell66.Text = string.Empty;
            for (int i = 0; i < paramValues4.Length; i++)
            {
                customer += paramValues4[i].ToString();
                if (i < paramValues4.Length - 1)
                    customer += ",";
            }


            //Salesman
            string[] paramValues5 = report.Parameters["Salesman"].Value as string[];
            //xrTableCell67.Text = string.Empty;
            for (int i = 0; i < paramValues5.Length; i++)
            {
                salesman += paramValues5[i].ToString();
                if (i < paramValues5.Length - 1)
                    salesman += ",";
            }


            //report.Parameters["DepositDateFrom"].Value = "<NULL>";
            // Passing Value Of Labels To Parameter.
            if (report.Parameters["Bank"].Value.ToString() == "System.String[]")
            {
                report.Parameters["Bank"].Value = bank;
            }

            if (report.Parameters["BusinessAccount"].Value.ToString() == "System.String[]")
            {
                report.Parameters["BusinessAccount"].Value = businessaccount;
            }

            if (report.Parameters["Customer"].Value.ToString() == "System.String[]")
            {
                report.Parameters["Customer"].Value = customer;
            }

            if (report.Parameters["Salesman"].Value.ToString() == "System.String[]")
            {
                report.Parameters["Salesman"].Value = salesman;
            }

            //if (report.Parameters["OrderBy"].Value.ToString() == "Customer")
            //{
            //    xrTableCell14.Text = "Customer:";
            //}

            //if (report.Parameters["OrderBy"].Value.ToString() == "DocumentDate")
            //{
            //    xrTableCell14.Text = "DocDate:";
            //}

            //if (report.Parameters["OrderBy"].Value.ToString() == "CheckDate")
            //{
            //    xrTableCell14.Text = "CheckDate:";
            //}

            //if (report.Parameters["OrderBy"].Value.ToString() == "DepositDate")
            //{
            //    xrTableCell14.Text = "DepositDate:";
            //}

            //if (report.Parameters["OrderBy"].Value.ToString() == "Bank")
            //{
            //    xrTableCell14.Text = "Bank:";
            //}


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
                //report.Parameters["CVDateFrom_Actual"].Type(string);
                report.Parameters["DocDateTo_Actual"].Value = "01/01/0001";
                docdatetoheader = "DocDateTo: NULL";
            }
            else
            {
                DateTime docdatetoactual = DateTime.Parse(docdateto);
                report.Parameters["DocDateTo_Actual"].Value = docdatetoactual;
                docdatetoheader = "DocDateTo: [Parameters.DocDateTo_Actual!MMMM dd, yyyy]";
            }

            string checkdatefrom = (string)report.Parameters["CheckDateFrom"].Value;
            string checkdatefromheader = "";
            if (String.IsNullOrEmpty(checkdatefrom))
            {
                //report.Parameters["CVDateFrom_Actual"].Type(string);
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
                //report.Parameters["CVDateFrom_Actual"].Type(string);
                report.Parameters["CheckDateTo_Actual"].Value = "01/01/0001";
                checkdatetoheader = "CheckDateTo: NULL";
            }
            else
            {
                DateTime checkdatetoactual = DateTime.Parse(checkdateto);
                report.Parameters["CheckDateTo_Actual"].Value = checkdatetoactual;
                checkdatetoheader = "CheckDateTo: [Parameters.CheckDateTo_Actual!MMMM dd, yyyy]";
            }


            string depositdatefrom = (string)report.Parameters["DepositDateFrom"].Value;
            string depositdatefromheader = "";
            if (String.IsNullOrEmpty(depositdatefrom))
            {
                //report.Parameters["CVDateFrom_Actual"].Type(string);
                report.Parameters["DepositDateFrom_Actual"].Value = "01/01/0001";
                depositdatefromheader = "DepositDateFrom: NULL";
            }
            else
            {
                DateTime depositdatefromactual = DateTime.Parse(depositdatefrom);
                report.Parameters["DepositDateFrom_Actual"].Value = depositdatefromactual;
                depositdatefromheader = "DepositDateFrom: [Parameters.DepositDateFrom_Actual!MMMM dd, yyyy]";
            }


            string depositdateto = (string)report.Parameters["DepositDateTo"].Value;
            string depositdatetoheader = "";
            if (String.IsNullOrEmpty(depositdateto))
            {
                //report.Parameters["CVDateFrom_Actual"].Type(string);
                report.Parameters["DepositDateTo_Actual"].Value = "01/01/0001";
                depositdatetoheader = "DepositDateTo: NULL";
            }
            else
            {
                DateTime depositdatetoactual = DateTime.Parse(depositdateto);
                report.Parameters["DepositDateTo_Actual"].Value = depositdatetoactual;
                depositdatetoheader = "DepositDateTo: [Parameters.DepositDateTo_Actual!MMMM dd, yyyy]";
            }



            //xrLabel4.Text = "CheckStatus: [Parameters.CheckStatus] / ClearingStatus: [Parameters.ClearingStatus] / ReleaseStatus: [Parameters.ReleaseStatus] / Supplier: " + supplier + " / BankAccount: [Parameters.BankAccount / OrderBy: [Parameters.OrderBy] / CVDocDateFrom: [Parameters.CVDateFrom!MMMM dd, yyyy] / CVDocDateTo: [Parameters.CVDateTo!MMMM dd, yyyy] / CheckDateFrom: [Parameters.CheckDateFrom!MMMM dd, yyyy] / CheckDateTo: [Parameters.CheckDateTo!MMMM dd, yyyy] / ReleaseDateFrom: [Parameters.ReleaseDateFrom!MMMM dd, yyyy] / ReleaseDateTo: [Parameters.ReleaseDateTo!MMMM dd, yyyy] / DateClearedFrom: [Parameters.DateClearedFrom!MMMM dd, yyyy] / DateClearedTo: [Parameters.DateClearedTo!MMMM dd, yyyy] / CheckNumberFrom: [Parameters.CheckNumberFrom!MMMM dd, yyyy] / CheckNumberTo: [Parameters.CheckNumberTo]";
            //xrLabel4.Text = "DocDateFrom: [Parameters.DocDateFrom!MMMM dd, yyyy] / DocDateTo: [Parameters.DocDateTo!MMMM dd, yyyy] / CheckDateFrom: [Parameters.CheckDateFrom!MMMM dd, yyyy] / CheckDateTo: [Parameters.CheckDateTo!MMMM dd, yyyy] / DepositDateFrom: [Parameters.DepositDateFrom!MMMM dd, yyyy] / DepositDateTo: [Parameters.DepositDateTo!MMMM dd, yyyy] / CheckNumber: [Parameters.CheckNumber] / CheckStatus: [Parameters.CheckStatus] / Bank: " + bank + " / BusinessAccount: " + businessaccount + " / Customer: " + customer + " / Salesman: " + salesman + " / OrderBy: [Parameters.OrderBy] ";
            xrLabel4.Text = docdatefromheader + " / " + docdatetoheader + " / " + checkdatefromheader + " / " + checkdatetoheader + " / " + depositdatefromheader + " / " + depositdatetoheader + " / CheckNumber: [Parameters.CheckNumber] / CheckStatus: [Parameters.CheckStatus] / Bank: " + bank + " / BusinessAccount: " + businessaccount + " / Customer: " + customer + " / Salesman: " + salesman + " / OrderBy: [Parameters.OrderBy] ";

            string showdetail = "";
            string[] paramValues = report.Parameters["ShowDetails"].Value as string[];
            //string holder = string.Empty;
            for (int i = 0; i < paramValues.Length; i++)
            {
                showdetail += paramValues[i].ToString();
                if (i < paramValues.Length - 1)
                    showdetail += ",";
            }


            bool[] truefalse = new bool[4];
            string[] Name = new string[4];
            string condition = "false";
            int position = 0;
            int length = 0;
            //4 Dahil ito yung pinaka length ng parameter
            if (paramValues == null || paramValues.Length == 0)
            {
                position = 4 * 40;
            }
            else
            {
                for (int i = 0; i < paramValues.Length; i++)
                {
                    if (paramValues[i] == "Summary")
                    {
                        length = paramValues.Length - 1;
                    }
                }

                if (length != 0)
                    position = (4 - length) * 40;
                else
                {
                    if (paramValues.Length == 1 && paramValues[0] == "Summary")
                        length = 0;
                    else
                        length = paramValues.Length;
                    position = (4 - length) * 40;
                }
            }

            //this.xrTable2.LocationFloat = new DevExpress.Utils.PointFloat(20.14086F + position, 0F);
            //this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(20.14086F + position, 0F);
            //this.xrTable3.LocationFloat = new DevExpress.Utils.PointFloat(20.14086F + position, 2.119277E-05F);
            //this.xrTable4.LocationFloat = new DevExpress.Utils.PointFloat(20.14086F + position, 0F);
            //this.Labelxxx.WidthF = 698.4F - (80F * (7 - length));
            //this.Labelyyy.WidthF = 80F;
            //this.xrTableCell24.WidthF = 63.83F;
            //this.xrTableCell35.WidthF = 714.57F - (80F * (7 - length));


            if (condition == "false")
            {
                for (int i = 0; i < paramValues.Length; i++)
                {
                    if (paramValues[i] == "Bank")
                    {
                        truefalse[i] = true;
                        Name[i] = "Bank";
                    }

                    else if (paramValues[i] == "DepositNo")
                    {
                        truefalse[i] = true;
                        Name[i] = "DepositNo";
                    }

                    else if (paramValues[i] == "DepositDate")
                    {
                        truefalse[i] = true;
                        Name[i] = "DepositDate";
                    }
                }
                //TRY LANG
                //if(paramValues.Length > 0)
                //{
                for (int i = 0; i < length; i++)
                {
                    string NameHolder = "";
                    bool truefalseHolder;
                    if (Name[i] == null)
                    {
                        NameHolder = Name[i + 1];
                        Name[i + 1] = Name[i];
                        Name[i] = NameHolder;

                        truefalseHolder = truefalse[i + 1];
                        truefalse[i + 1] = truefalse[i];
                        truefalse[i] = truefalseHolder;
                    }
                }

                //truefalse[length] = true;
                //Name[length] = "Amount";
                //}
                //else
                //{ 
                //truefalse[paramValues.Length] = true;
                //Name[paramValues.Length] = "Amount";
                //}
                //-------
                for (int i = 0; i < 3; i++)
                {
                    switch (i)
                    {
                        case 0:	// Header1
                            Header1.Visible = truefalse[i];
                            Header1.Text = Name[i];
                            Detail1.Visible = truefalse[i];
                            if (Name[i] == "DepositDate")
                            {
                                this.Detail1.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            				        new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ListofCheckReceived.DepositDate", "{0:MM/dd/yy}")});
                                this.Detail1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                                this.Detail1.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
                            }
                            else if (Name[i] == "Bank")
                            {
                                this.Detail1.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            				        new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ListofCheckReceived.BankAccountCode")});
                                this.Detail1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                                this.Detail1.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
                            }
                            else if (Name[i] == "DepositNo")
                            {
                                this.Detail1.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            				        new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ListofCheckReceived.DepositNumber")});
                                this.Detail1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                                this.Detail1.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
                            }
                            else
                            {
                                this.Detail1.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            				            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ListofCheckReceived."+Name[i])});
                                //this.Detail1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                //this.Detail1.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 100F);
                            }
                            break;
                        case 1:	// Header2
                            Header2.Visible = truefalse[i];
                            Header2.Text = Name[i];
                            Detail2.Visible = truefalse[i];
                            //Group2.Visible = truefalse[i];
                            //Footer2.Visible = truefalse[i];
                            if (Name[i] == "DepositDate")
                            {
                                this.Detail2.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            				        new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ListofCheckReceived.DepositDate", "{0:MM/dd/yy}")});
                                this.Detail2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                                this.Detail2.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
                            }
                            else if (Name[i] == "Bank")
                            {
                                this.Detail2.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            				        new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ListofCheckReceived.BankAccountCode")});
                                this.Detail2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                                this.Detail2.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
                            }
                            else if (Name[i] == "DepositNo")
                            {
                                this.Detail2.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            				        new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ListofCheckReceived.DepositNumber")});
                                this.Detail2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                                this.Detail2.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
                            }
                            else
                            {
                                this.Detail2.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            				            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ListofCheckReceived."+Name[i])});
                                //this.Detail1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                //this.Detail1.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 100F);
                            }
                            break;
                        default:	// Header7
                            Header3.Visible = truefalse[i];
                            Header3.Text = Name[i];
                            Detail3.Visible = truefalse[i];
                            //Group7.Visible = truefalse[i];
                            //Footer7.Visible = truefalse[i];
                            if (Name[i] == "DepositDate")
                            {
                                this.Detail3.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            				        new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ListofCheckReceived.DepositDate", "{0:MM/dd/yy}")});
                                this.Detail3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                                this.Detail3.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
                            }
                            else if (Name[i] == "Bank")
                            {
                                this.Detail3.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            				        new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ListofCheckReceived.BankAccountCode")});
                                this.Detail3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                                this.Detail3.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
                            }
                            else if (Name[i] == "DepositNo")
                            {
                                this.Detail3.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            				        new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ListofCheckReceived.DepositNumber")});
                                this.Detail3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                                this.Detail3.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
                            }
                            else
                            {
                                this.Detail3.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            				            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ListofCheckReceived."+Name[i])});
                                //this.Detail1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                //this.Detail1.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 100F);
                            }

                            break;
                    }
                }
            } // End Of Else condition == false

        }

        private void R_ListofCheckReceived_ParametersRequestSubmit(object sender, DevExpress.XtraReports.Parameters.ParametersRequestEventArgs e)
        {



        }
    }
}
