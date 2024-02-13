using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using DevExpress.XtraReports;
using System.Web;

namespace GWL.WebReports.GEARS_Reports
{
    public partial class R_ReplenishPettyCashExpense : DevExpress.XtraReports.UI.XtraReport
    {
        public R_ReplenishPettyCashExpense()
        {
            InitializeComponent();

            if (HttpContext.Current.Session["ConnString"] != null)
                sqlDataSource1.Connection.ConnectionString = HttpContext.Current.Session["ConnString"].ToString();



        }


        private void ReportHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }

        private void R_ReplenishPettyCashExpense_DataSourceDemanded(object sender, EventArgs e)
        {
            XtraReport report = (XtraReport)Report;
            
            //if (report.Parameters["ShowDetails"].Value.ToString() == String.Empty)
            //{
            //    //_filterString = FilterString;
            //    report.FilterString = "true";
            //}


            Header1.Visible = false;
            Header2.Visible = false;
            Header3.Visible = false;
            Header4.Visible = false;
            Header5.Visible = false;
            Header6.Visible = false;
            Header7.Visible = false;
            Header8.Visible = false;

            //Group2.Visible = false;
            //Group3.Visible = false;
            //Group4.Visible = false;
            //Group5.Visible = false;
            //Group6.Visible = false;
            //Group7.Visible = false;


            Detail1.Visible = false;
            Detail2.Visible = false;
            Detail3.Visible = false;
            Detail4.Visible = false;
            Detail5.Visible = false;
            Detail6.Visible = false;
            Detail7.Visible = false;
            Detail8.Visible = false;

            //Footer2.Visible = false;
            //Footer3.Visible = false;
            //Footer4.Visible = false;
            //Footer5.Visible = false;
            //Footer6.Visible = false;
            //Footer7.Visible = false;



            // Unbind All Data
            this.Detail1.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
     			new DevExpress.XtraReports.UI.XRBinding("Text", null, "")});
            this.Detail2.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
     			new DevExpress.XtraReports.UI.XRBinding("Text", null, "")});
            this.Detail3.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
     			new DevExpress.XtraReports.UI.XRBinding("Text", null, "")});
            this.Detail4.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
     			new DevExpress.XtraReports.UI.XRBinding("Text", null, "")});
            this.Detail5.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
     			new DevExpress.XtraReports.UI.XRBinding("Text", null, "")});
            this.Detail6.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
     			new DevExpress.XtraReports.UI.XRBinding("Text", null, "")});
            this.Detail7.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
     			new DevExpress.XtraReports.UI.XRBinding("Text", null, "")});
            this.Detail8.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
     			new DevExpress.XtraReports.UI.XRBinding("Text", null, "")});


            //report.Parameters["ShowDetails"].Value = test;
            string showdetail = "";
            string[] paramValues = report.Parameters["ShowDetails"].Value as string[];
            //string holder = string.Empty;
            for (int i = 0; i < paramValues.Length; i++)
            {
                showdetail += paramValues[i].ToString();
                if (i < paramValues.Length - 1)
                    showdetail += ",";
            }
            //string brk = holder.Replace(",", System.Environment.NewLine);
            //xrTableCell13.Text = brk;
            //xrTableCell13.Multiline = true;
            xrLabel1.Text = "Replenishment Date:  [Parameters.DateFrom!MM/dd/yy]  -  [Parameters.DateTo!MM/dd/yy] / ShowDetails:  " + showdetail;

            bool[] truefalse = new bool[9];
            string[] Name = new string[9];
            string condition = "false";
            int position = 0;
            int length = 0;
            //6 Dahil ito yung pinaka length ng parameter
            if (paramValues == null || paramValues.Length == 0)
            {
                position = 7 * 40;
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

                if(length != 0)
                    position = (7 - length) * 40;
                else
                {
                    if (paramValues.Length == 1 && paramValues[0] == "Summary")
                        length = 0;
                    else
                        length = paramValues.Length;
                    position = (7 - length) * 40;
                }
                    
            }
            this.xrTable2.LocationFloat = new DevExpress.Utils.PointFloat(20.14086F + position, 0F);
            this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(20.14086F + position, 0F);
            this.xrTable3.LocationFloat = new DevExpress.Utils.PointFloat(20.14086F + position, 2.119277E-05F);
            this.xrTable4.LocationFloat = new DevExpress.Utils.PointFloat(20.14086F + position, 0F);
            this.Labelxxx.WidthF = 698.4F - (80F * (7 - length));
            this.Labelyyy.WidthF = 80F;
            this.xrTableCell24.WidthF = 63.83F;
            this.xrTableCell35.WidthF = 714.57F - (80F * (7 - length));
            //this.xrTable5.LocationFloat = new DevExpress.Utils.PointFloat(62.65094F + position, 0F);

            this.GroupB7.LocationFloat = new DevExpress.Utils.PointFloat(760.0679F - position, 0F);
            this.DetailB7.LocationFloat = new DevExpress.Utils.PointFloat(760.0679F - position, 0F);
            //this.GroupB7.LocationFloat = new DevExpress.Utils.PointFloat(760.0679F - position, 0F);
            //this.DetailB7.LocationFloat = new DevExpress.Utils.PointFloat(760.0679F - position, 0F);

            // Kung Sakaling May "ALL" Na Parameters. Reusable Pa Din Yung Code
            //for (int i = 0; i < paramValues.Length; i++)
            //{
            //if (paramValues[i] == "ALL")
            //{
            //		Header1.Visible = true;
            //		Header2.Visible = true;
            //		Header3.Visible = true;
            //		Header4.Visible = true;
            //		Header5.Visible = true;
            //		Header6.Visible = true;
            //		Header7.Visible = true;
            //		condition = "true";
            //		break;
            //}		
            //}

            if (condition == "false")
            {
                for (int i = 0; i < paramValues.Length; i++)
                {
                    if (paramValues[i] == "CostCenter")
                    {
                        truefalse[i] = true;
                        Name[i] = "CostCenter";
                    }

                    else if (paramValues[i] == "DocumentDate")
                    {
                        truefalse[i] = true;
                        Name[i] = "DocDate";
                    }

                    else if (paramValues[i] == "DocumentNumber")
                    {
                        truefalse[i] = true;
                        Name[i] = "DocNumber";
                    }

                    else if (paramValues[i] == "FundSource")
                    {
                        truefalse[i] = true;
                        Name[i] = "FundSource";
                    }

                    else if (paramValues[i] == "Receiver")
                    {
                        truefalse[i] = true;
                        Name[i] = "Receiver";
                    }

                    else if (paramValues[i] == "Requestor")
                    {
                        truefalse[i] = true;
                        Name[i] = "Requestor";
                    }

                    else if (paramValues[i] == "Remarks")
                    {
                        truefalse[i] = true;
                        Name[i] = "Remarks";
                    }
                }
                //TRY LANG
                //if(paramValues.Length > 0)
                //{
                for (int i = 0; i < length; i++)
                {
                    string NameHolder = "";
                    bool truefalseHolder;
                    if(Name[i] == null)
                    {
                        NameHolder = Name[i + 1];
                        Name[i + 1] = Name[i];
                        Name[i] = NameHolder;

                        truefalseHolder = truefalse[i + 1];
                        truefalse[i + 1] = truefalse[i];
                        truefalse[i] = truefalseHolder;
                    }
                }

                truefalse[length] = true;
                Name[length] = "Amount";
                    //}
                    //else
                    //{ 
                    //truefalse[paramValues.Length] = true;
                    //Name[paramValues.Length] = "Amount";
                    //}
                    //-------
                    for (int i = 0; i < 8; i++)
                    {
                        switch (i)
                        {
                            case 0:	// Header1
                                Header1.Visible = truefalse[i];
                                Header1.Text = Name[i];
                                Detail1.Visible = truefalse[i];
                                if (Name[i] == "DocDate")
                                {
                                    this.Detail1.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            				new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ReplenishedPettyCashExpense.DocDate", "{0:MM/dd/yy}")});
                                    this.Detail1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                                    this.Detail1.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
                                }
                                else if (Name[i] == "Amount")
                                {
                                    this.Detail1.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            				new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ReplenishedPettyCashExpense."+Name[i])});
                                    this.Detail1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                    this.Detail1.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 100F);
                                }
                                //else if (Name[i] == "Remarks")
                                //{
                                //    this.Detail1.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
                                //        new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ReplenishedPettyCashExpense."+Name[i])});
                                //    this.Detail1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                //    this.Detail1.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 100F);
                                //}
                                else
                                {
                                    this.Detail1.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            				            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ReplenishedPettyCashExpense."+Name[i])});
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
                                if (Name[i] == "DocDate")
                                {
                                    this.Detail2.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            				            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ReplenishedPettyCashExpense.DocDate", "{0:MM/dd/yy}")});
                                }
                                //else if (Name[i] == "Remarks")
                                //{
                                //    this.Detail2.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
                                //        new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ReplenishedPettyCashExpense."+Name[i])});
                                //    this.Detail2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                //    this.Detail2.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 100F);
                                //}
                                else
                                {
                                    this.Detail2.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            				        new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ReplenishedPettyCashExpense."+Name[i])});
                                }
                                break;
                            case 2:	// Header3
                                Header3.Visible = truefalse[i];
                                Header3.Text = Name[i];
                                Detail3.Visible = truefalse[i];
                                //Group3.Visible = truefalse[i];
                                //Footer3.Visible = truefalse[i];

                                if (Name[i] == "Amount")
                                {
                                    this.Detail3.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            				            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ReplenishedPettyCashExpense."+Name[i])});
                                    this.Detail3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                    this.Detail3.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 100F);
                                }
                                else if (Name[i] == "DocDate")
                                {
                                    this.Detail3.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            				            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ReplenishedPettyCashExpense.DocDate", "{0:MM/dd/yy}")});
                                }
                                //else if (Name[i] == "Remarks")
                                //{
                                //    this.Detail3.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
                                //        new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ReplenishedPettyCashExpense."+Name[i])});
                                //    this.Detail3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                //    this.Detail3.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 100F);
                                //}
                                else
                                {
                                    this.Detail3.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            				            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ReplenishedPettyCashExpense."+Name[i])});
                                    //this.Detail3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                    //this.Detail3.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 100F);
                                }

                                break;
                            case 3:	// Header4
                                Header4.Visible = truefalse[i];
                                Header4.Text = Name[i];
                                Detail4.Visible = truefalse[i];
                                //Group4.Visible = truefalse[i];
                                //Footer4.Visible = truefalse[i];

                                if (Name[i] == "Amount")
                                {
                                    this.Detail4.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            				new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ReplenishedPettyCashExpense."+Name[i])});
                                    this.Detail4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                    this.Detail4.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 100F);
                                }
                                else if (Name[i] == "DocDate")
                                {
                                    this.Detail4.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            				            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ReplenishedPettyCashExpense.DocDate", "{0:MM/dd/yy}")});
                                }
                                //else if (Name[i] == "Remarks")
                                //{
                                //    this.Detail4.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
                                //        new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ReplenishedPettyCashExpense."+Name[i])});
                                //    this.Detail4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                //    this.Detail4.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 100F);
                                //}
                                else
                                {
                                    this.Detail4.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            				            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ReplenishedPettyCashExpense."+Name[i])});
                                    //this.Detail4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                    //this.Detail4.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 100F);
                                }
                                break;
                            case 4:	// Header5
                                Header5.Visible = truefalse[i];
                                Header5.Text = Name[i];
                                Detail5.Visible = truefalse[i];
                                //Group5.Visible = truefalse[i];
                                //Footer5.Visible = truefalse[i];

                                if (Name[i] == "Amount")
                                {
                                    this.Detail5.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            				            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ReplenishedPettyCashExpense."+Name[i])});
                                    this.Detail5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                    this.Detail5.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 100F);
                                }
                                else if (Name[i] == "DocDate")
                                {
                                    this.Detail5.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            				        new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ReplenishedPettyCashExpense.DocDate", "{0:MM/dd/yy}")});
                                }
                                //else if (Name[i] == "Remarks")
                                //{
                                //    this.Detail5.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
                                //        new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ReplenishedPettyCashExpense."+Name[i])});
                                //    this.Detail5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                //    this.Detail5.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 100F);
                                //}
                                else
                                {
                                    this.Detail5.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            				            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ReplenishedPettyCashExpense."+Name[i])});
                                    //this.Detail5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                    //this.Detail5.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 100F);
                                }
                                break;
                            case 5:	// Header6
                                Header6.Visible = truefalse[i];
                                Header6.Text = Name[i];
                                Detail6.Visible = truefalse[i];

                                if (Name[i] == "Amount")
                                {
                                    this.Detail6.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            				            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ReplenishedPettyCashExpense."+Name[i])});
                                    this.Detail6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                    this.Detail6.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 100F);
                                }
                                else if (Name[i] == "DocDate")
                                {
                                    this.Detail6.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            				            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ReplenishedPettyCashExpense.DocDate", "{0:MM/dd/yy}")});
                                }
                                //else if (Name[i] == "Remarks")
                                //{
                                //    this.Detail6.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
                                //        new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ReplenishedPettyCashExpense."+Name[i])});
                                //    this.Detail6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                //    this.Detail6.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 100F);
                                //}
                                else
                                {
                                    this.Detail6.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            				            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ReplenishedPettyCashExpense."+Name[i])});
                                }
                                break;

                            case 6:	// Header7
                                Header7.Visible = truefalse[i];
                                Header7.Text = Name[i];
                                Detail7.Visible = truefalse[i];

                                if (Name[i] == "Amount")
                                {
                                    this.Detail7.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            				            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ReplenishedPettyCashExpense."+Name[i])});
                                    this.Detail7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                                    this.Detail7.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 100F);
                                }
                                else if (Name[i] == "DocDate")
                                {
                                    this.Detail7.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            				            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ReplenishedPettyCashExpense.DocDate", "{0:MM/dd/yy}")});
                                }
                                //else if (Name[i] == "Remarks")
                                //{
                                //    this.Detail7.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
                                //        new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ReplenishedPettyCashExpense."+Name[i])});
                                //    this.Detail7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                                //    this.Detail7.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 100F);
                                //}
                                else
                                {
                                    this.Detail7.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            				            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ReplenishedPettyCashExpense."+Name[i])});
                                }
                                break;


                            default:	// Header7
                                Header8.Visible = truefalse[i];
                                Header8.Text = Name[i];
                                Detail8.Visible = truefalse[i];
                                //Group7.Visible = truefalse[i];
                                //Footer7.Visible = truefalse[i];

                                this.Detail8.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            				        new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ReplenishedPettyCashExpense."+Name[i])});
                                this.Detail8.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;

                                break;
                        }
                    }
            } // End Of Else condition == false

        }

        private void xrTableCell24_HtmlItemCreated(object sender, HtmlEventArgs e)
        {

            e.ContentCell.Style.Add("border-left", "1px solid #000000 !important");
            e.ContentCell.Style.Add("border-bottom", "1px solid #dcdcdc !important");  
        }

        private void xrTableCell19_HtmlItemCreated(object sender, HtmlEventArgs e)
        {

            e.ContentCell.Style.Add("border-left", "1px solid #000000 !important");
            e.ContentCell.Style.Add("border-bottom", "1px solid #dcdcdc !important");  
        }

        private void Group2_HtmlItemCreated(object sender, HtmlEventArgs e)
        {

        }

        private void Group3_HtmlItemCreated(object sender, HtmlEventArgs e)
        {

        }

        private void Group4_HtmlItemCreated(object sender, HtmlEventArgs e)
        {

        }

        private void Group5_HtmlItemCreated(object sender, HtmlEventArgs e)
        {

        }

        private void Group6_HtmlItemCreated(object sender, HtmlEventArgs e)
        {

        }

        private void Group7_HtmlItemCreated(object sender, HtmlEventArgs e)
        {

        }

        private void Detail2_HtmlItemCreated(object sender, HtmlEventArgs e)
        {

        }

        private void Detail3_HtmlItemCreated(object sender, HtmlEventArgs e)
        {

        }

        private void Detail4_HtmlItemCreated(object sender, HtmlEventArgs e)
        {

        }

        private void Detail5_HtmlItemCreated(object sender, HtmlEventArgs e)
        {
            XtraReport report = (XtraReport)Report;
            string[] paramValues = report.Parameters["ShowDetails"].Value as string[];
            if (paramValues.Length == 4)
            {
                e.ContentCell.Style.Add("border-right", "1px solid #000000 !important");
                e.ContentCell.Style.Add("border-bottom", "1px solid #dcdcdc !important");
            }
        }

        private void Detail6_HtmlItemCreated(object sender, HtmlEventArgs e)
        {

        }

        private void Detail7_HtmlItemCreated(object sender, HtmlEventArgs e)
        {

        }

        private void R_ReplenishPettyCashExpense_ParametersRequestBeforeShow(object sender, DevExpress.XtraReports.Parameters.ParametersRequestEventArgs e)
        {
            //XtraReport report = (XtraReport)Report;
            //report.FilterString = "false";
            //report.RequestParameters = false;
        }
    }
}
