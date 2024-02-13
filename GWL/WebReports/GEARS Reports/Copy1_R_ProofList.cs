using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using DevExpress.XtraReports;
using System.Web;

namespace GWL.WebReports.GEARS_Reports
{
    public partial class R_ProofList : DevExpress.XtraReports.UI.XtraReport
    {
        public R_ProofList()
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

        private void R_ProofList_DataSourceDemanded(object sender, EventArgs e)
        {
            XtraReport report = (XtraReport)Report;

            string profitcentecode = "";
            string bizpartnercode = "";
            string subsicode = "";
            string docnumber = "";

            if(report.Parameters["ShowBizPartner"].Value.ToString() == "True")
            {
                bizpartnercode = "Yes";
            }
             
            if(report.Parameters["ShowBizPartner"].Value.ToString() == "False")
            {
                bizpartnercode = "No";
            }
           
            if(report.Parameters["ShowSubsiCode"].Value.ToString() == "True")
            {
                subsicode = "Yes";
            }
            
            if (report.Parameters["ShowSubsiCode"].Value.ToString() == "False")
            {
                subsicode = "No";
            }
            
            if (report.Parameters["ShowProfitCenter"].Value.ToString() == "True")
            {
                profitcentecode = "Yes";
            }
            
            if (report.Parameters["ShowProfitCenter"].Value.ToString() == "False")
            {
                profitcentecode = "No";
            }
            if (report.Parameters["DocNumber"].Value.ToString() == "")
            {
                docnumber = "NULL";
            }

            xrLabel1.Text = "From: [Parameters.DateFrom!MM/dd/yyyy] To: [Parameters.DateTo!MM/dd/yyyy] / DocNumber:  " + docnumber + " / Show BizPartnerCode:  " + bizpartnercode + " / Show SubsiCode:  " + subsicode + " / Show ProfitCenterCode:  " + profitcentecode;

                



 


            //XtraReport report = (XtraReport)Report;

            if (report.Parameters["ShowSubsiCode"].Value.ToString() == "True")
            {
                //xrTableCell39.Text = "Yes";
                xrTableCell1.Text = "SubsiCode";
                this.xrTableCell2.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ProofList.SubsiCode")});

                xrTableCell3.Text = "SubsiCodeDesc";
                this.xrTableCell7.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ProofList.SubSiDescription")});

                //Empty Cells na nalagyan ng data
                this.xrTableCell33.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "")});
                this.xrTableCell2.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 100F);
                this.xrTableCell2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;


                this.xrTableCell36.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "")});
                this.xrTableCell7.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 100F);
                this.xrTableCell7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;


                this.xrTableCell31.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "")});
                this.xrTableCell23.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 100F);
                this.xrTableCell23.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;


                this.xrTableCell22.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "")});
                this.xrTableCell26.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 100F);
                this.xrTableCell26.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;



                this.xrTableCell37.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "")});
                this.xrTableCell28.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 100F);
                this.xrTableCell28.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;


                this.xrTableCell21.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "")});
                this.xrTableCell29.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 100F);
                this.xrTableCell29.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;




                this.xrTable2.LocationFloat = new DevExpress.Utils.PointFloat(0.0002900759F, 0F);
                this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
                this.xrTable3.LocationFloat = new DevExpress.Utils.PointFloat(0.0001490116F, 0F);

            }

            if (report.Parameters["ShowSubsiCode"].Value.ToString() == "False")
            {
                //xrTableCell39.Text = "No";
            }

            if (report.Parameters["ShowProfitCenter"].Value.ToString() == "True")
            {
                //xrTableCell41.Text = "Yes";
                xrTableCell6.Text = "ProfitCenterCode";
                this.xrTableCell23.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ProofList.ProfitCenterCode")});

                xrTableCell11.Text = "SubsiCodeDesc";
                this.xrTableCell26.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ProofList.ProfitCenterCodeDescription")});

            }

            if (report.Parameters["ShowProfitCenter"].Value.ToString() == "False")
            {
               // xrTableCell41.Text = "No";
            }

            if (report.Parameters["ShowBizPartner"].Value.ToString() == "True")
            {
                //xrTableCell17.Text = "Yes";
                xrTableCell12.Text = "BizPartnerCode";
                this.xrTableCell28.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ProofList.BizPartnerCode")});

                xrTableCell15.Text = "Name";
                this.xrTableCell29.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ProofList.BizPartnerDescription")});
            }

            if (report.Parameters["ShowBizPartner"].Value.ToString() == "False")
            {
               // xrTableCell17.Text = "No";
            }





            // --------------------------------------- 
            // HIDING SubsiCode
            // ---------------------------------------
            if (report.Parameters["ShowSubsiCode"].Value.ToString() == "False"
                && report.Parameters["ShowProfitCenter"].Value.ToString() == "True"
                && report.Parameters["ShowBizPartner"].Value.ToString() == "True")
            {
                xrTableCell1.Text = "ProfitCenter";
                this.xrTableCell2.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ProofList.ProfitCenterCode")});

                xrTableCell3.Text = "ProfitCenterDesc";
                this.xrTableCell7.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ProofList.ProfitCenterCodeDescription")});


                xrTableCell6.Text = "BizPartnerCode";
                this.xrTableCell23.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ProofList.BizPartnerCode")});

                xrTableCell11.Text = "Name";
                this.xrTableCell26.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ProofList.BizPartnerDescription")});

                //Move Total Debit To BizPartner
                xrTableCell12.Text = "TotalDebit";
                this.xrTableCell28.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ProofList.TotalDebitAmount", "{0:#,0.00;(#,0.00);}")});

                xrTableCell15.Text = "TotalCredit";
                this.xrTableCell29.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ProofList.TotalCreditAmount", "{0:#,0.00;(#,0.00);}")});


                //Moving Total Debit Amount To SubsiCode Field and Setting Up!	
                this.xrTableCell37.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ProofList.TotalDebitAmount", "{0:#,0.00;(#,0.00);}")});

                this.xrTableCell28.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 100F);
                this.xrTableCell28.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;

                //Moving Total Credit Amount To SubsiCode Description and Setting Up!	
                this.xrTableCell21.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ProofList.TotalCreditAmount", "{0:#,0.00;(#,0.00);}")});

                this.xrTableCell29.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 100F);
                this.xrTableCell29.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;


                //Tanggal value sa hindi kailangan na cell
                this.xrTableCell33.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "")});
                this.xrTableCell2.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 100F);
                this.xrTableCell2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;


                this.xrTableCell36.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "")});
                this.xrTableCell7.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 100F);
                this.xrTableCell7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;


                this.xrTableCell31.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "")});
                this.xrTableCell23.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 100F);
                this.xrTableCell23.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;


                this.xrTableCell22.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "")});
                this.xrTableCell26.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 100F);
                this.xrTableCell26.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;


                //Adjusting Positions
                this.xrTable2.LocationFloat = new DevExpress.Utils.PointFloat(87.33331F, 0F);
                this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(87.33318F, 0F);
                this.xrTable3.LocationFloat = new DevExpress.Utils.PointFloat(87.33334F, 0F);
            }



            //--------------------------
            //Hiding Profit Center Code
            //--------------------------
            if (report.Parameters["ShowSubsiCode"].Value.ToString() == "True"
                && report.Parameters["ShowProfitCenter"].Value.ToString() == "False"
                && report.Parameters["ShowBizPartner"].Value.ToString() == "True")
            {


                xrTableCell1.Text = "SubsiCode";
                this.xrTableCell2.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ProofList.SubsiCode")});

                xrTableCell3.Text = "SubsiCodeDesc";
                this.xrTableCell7.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ProofList.SubSiDescription")});



                xrTableCell6.Text = "BizPartnerCode";
                this.xrTableCell23.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ProofList.BizPartnerCode")});

                xrTableCell11.Text = "Name";
                this.xrTableCell26.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ProofList.BizPartnerDescription")});

                //Move Total Debit To BizPartner
                xrTableCell12.Text = "TotalDebit";
                this.xrTableCell28.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ProofList.TotalDebitAmount", "{0:#,0.00;(#,0.00);}")});

                xrTableCell15.Text = "TotalCredit";
                this.xrTableCell29.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ProofList.TotalCreditAmount", "{0:#,0.00;(#,0.00);}")});


                //Moving Total Debit Amount To SubsiCode Field and Setting Up!	
                this.xrTableCell37.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ProofList.TotalDebitAmount", "{0:#,0.00;(#,0.00);}")});

                this.xrTableCell28.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 100F);
                this.xrTableCell28.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;

                //Moving Total Credit Amount To SubsiCode Description and Setting Up!	
                this.xrTableCell21.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ProofList.TotalCreditAmount", "{0:#,0.00;(#,0.00);}")});

                this.xrTableCell29.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 100F);
                this.xrTableCell29.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;


                //Tanggal value sa hindi kailangan na cell
                this.xrTableCell33.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "")});
                this.xrTableCell2.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 100F);
                this.xrTableCell2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;


                this.xrTableCell36.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "")});
                this.xrTableCell7.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 100F);
                this.xrTableCell7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;


                this.xrTableCell31.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "")});
                this.xrTableCell23.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 100F);
                this.xrTableCell23.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;


                this.xrTableCell22.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "")});
                this.xrTableCell26.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 100F);
                this.xrTableCell26.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;





                this.xrTable2.LocationFloat = new DevExpress.Utils.PointFloat(87.33331F, 0F);
                this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(87.33318F, 0F);
                this.xrTable3.LocationFloat = new DevExpress.Utils.PointFloat(87.33334F, 0F);
            }




            //----------------------
            //Hiding BizPartner Code
            //----------------------
            if (report.Parameters["ShowSubsiCode"].Value.ToString() == "True"
                && report.Parameters["ShowProfitCenter"].Value.ToString() == "True"
                && report.Parameters["ShowBizPartner"].Value.ToString() == "False")
            {
                //Move Total Debit To BizPartner
                xrTableCell12.Text = "TotalDebit";
                this.xrTableCell28.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ProofList.TotalDebitAmount", "{0:#,0.00;(#,0.00);}")});

                xrTableCell15.Text = "TotalCredit";
                this.xrTableCell29.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ProofList.TotalCreditAmount", "{0:#,0.00;(#,0.00);}")});



                //Moving Total Debit Amount To SubsiCode Field and Setting Up!	
                this.xrTableCell37.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ProofList.TotalDebitAmount", "{0:#,0.00;(#,0.00);}")});

                this.xrTableCell28.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 100F);
                this.xrTableCell28.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;

                //Moving Total Credit Amount To SubsiCode Description and Setting Up!	
                this.xrTableCell21.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ProofList.TotalCreditAmount", "{0:#,0.00;(#,0.00);}")});

                this.xrTableCell29.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 100F);
                this.xrTableCell29.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;


                //Tanggal value sa hindi kailangan na cell
                this.xrTableCell33.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "")});
                this.xrTableCell2.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 100F);
                this.xrTableCell2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;


                this.xrTableCell36.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "")});
                this.xrTableCell7.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 100F);
                this.xrTableCell7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;


                this.xrTableCell31.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "")});
                this.xrTableCell23.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 100F);
                this.xrTableCell23.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;


                this.xrTableCell22.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "")});
                this.xrTableCell26.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 100F);
                this.xrTableCell26.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;


                //Adjusting Positions
                this.xrTable2.LocationFloat = new DevExpress.Utils.PointFloat(87.33331F, 0F);
                this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(87.33318F, 0F);
                this.xrTable3.LocationFloat = new DevExpress.Utils.PointFloat(87.33334F, 0F);

            }





            //---------------------------------------
            // Hide SubsiCode and Profit Center Code
            //---------------------------------------
            if (report.Parameters["ShowSubsiCode"].Value.ToString() == "False"
                && report.Parameters["ShowProfitCenter"].Value.ToString() == "False"
                && report.Parameters["ShowBizPartner"].Value.ToString() == "True")
            {
                xrTableCell1.Text = "BizPartnerCode";
                this.xrTableCell2.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ProofList.BizPartnerCode")});

                xrTableCell3.Text = "Name";
                this.xrTableCell7.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ProofList.BizPartnerDescription")});


                xrTableCell6.Text = "TotalDebit";
                this.xrTableCell23.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ProofList.TotalDebitAmount", "{0:#,0.00;(#,0.00);}")});

                xrTableCell11.Text = "TotalCredit";
                this.xrTableCell26.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ProofList.TotalCreditAmount", "{0:#,0.00;(#,0.00);}")});




                //Moving Total Debit Amount To SubsiCode Field and Setting Up!	
                this.xrTableCell31.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ProofList.TotalDebitAmount", "{0:#,0.00;(#,0.00);}")});

                this.xrTableCell23.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 100F);
                this.xrTableCell23.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;

                //Moving Total Credit Amount To SubsiCode Description and Setting Up!	
                this.xrTableCell22.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ProofList.TotalCreditAmount", "{0:#,0.00;(#,0.00);}")});

                this.xrTableCell26.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 100F);
                this.xrTableCell26.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;


                //Tanggal value sa hindi kailangan na cell
                this.xrTableCell33.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "")});
                this.xrTableCell2.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 100F);
                this.xrTableCell2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;


                this.xrTableCell36.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "")});
                this.xrTableCell7.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 100F);
                this.xrTableCell7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;





                //Adjusting Positions
                this.xrTable2.LocationFloat = new DevExpress.Utils.PointFloat(174.6668F, 0F);
                this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(174.6668F, 0F);
                this.xrTable3.LocationFloat = new DevExpress.Utils.PointFloat(174.6667F, 0F);
            }





            //---------------------------------------
            // Hide SubsiCode and BizPartner Code
            //---------------------------------------
            if (report.Parameters["ShowSubsiCode"].Value.ToString() == "False"
                && report.Parameters["ShowProfitCenter"].Value.ToString() == "True"
                && report.Parameters["ShowBizPartner"].Value.ToString() == "False")
            {
                xrTableCell1.Text = "ProfitCenter";
                this.xrTableCell2.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ProofList.ProfitCenterCode")});

                xrTableCell3.Text = "ProfitCenterDesc";
                this.xrTableCell7.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ProofList.ProfitCenterDescription")});


                xrTableCell6.Text = "TotalDebit";
                this.xrTableCell23.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ProofList.TotalDebitAmount", "{0:#,0.00;(#,0.00);}")});

                xrTableCell11.Text = "TotalCredit";
                this.xrTableCell26.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ProofList.TotalCreditAmount", "{0:#,0.00;(#,0.00);}")});





                //Moving Total Debit Amount To SubsiCode Field and Setting Up!	
                this.xrTableCell31.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ProofList.TotalDebitAmount", "{0:#,0.00;(#,0.00);}")});

                this.xrTableCell23.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 100F);
                this.xrTableCell23.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;

                //Moving Total Credit Amount To SubsiCode Description and Setting Up!	
                this.xrTableCell22.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ProofList.TotalCreditAmount", "{0:#,0.00;(#,0.00);}")});

                this.xrTableCell26.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 100F);
                this.xrTableCell26.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;


                //Tanggal value sa hindi kailangan na cell
                this.xrTableCell33.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "")});
                this.xrTableCell2.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 100F);
                this.xrTableCell2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;


                this.xrTableCell36.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "")});
                this.xrTableCell7.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 100F);
                this.xrTableCell7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;


                //Adjusting Positions
                this.xrTable2.LocationFloat = new DevExpress.Utils.PointFloat(174.6668F, 0F);
                this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(174.6668F, 0F);
                this.xrTable3.LocationFloat = new DevExpress.Utils.PointFloat(174.6667F, 0F);


            }


            //---------------------------------------
            // Hide ProfitCode and BizPartner Code
            //---------------------------------------
            if (report.Parameters["ShowSubsiCode"].Value.ToString() == "True"
                && report.Parameters["ShowProfitCenter"].Value.ToString() == "False"
                && report.Parameters["ShowBizPartner"].Value.ToString() == "False")
            {
                xrTableCell1.Text = "SubsiCode";
                this.xrTableCell2.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ProofList.SubsiCode")});

                xrTableCell3.Text = "SubsiCodeDesc";
                this.xrTableCell7.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ProofList.SubSiDescription")});


                xrTableCell6.Text = "TotalDebit";
                this.xrTableCell23.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ProofList.TotalDebitAmount", "{0:#,0.00;(#,0.00);}")});

                xrTableCell11.Text = "TotalCredit";
                this.xrTableCell26.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ProofList.TotalCreditAmount", "{0:#,0.00;(#,0.00);}")});



                //Moving Total Debit Amount To SubsiCode Field and Setting Up!	
                this.xrTableCell31.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ProofList.TotalDebitAmount", "{0:#,0.00;(#,0.00);}")});

                this.xrTableCell23.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 100F);
                this.xrTableCell23.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;

                //Moving Total Credit Amount To SubsiCode Description and Setting Up!	
                this.xrTableCell22.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ProofList.TotalCreditAmount", "{0:#,0.00;(#,0.00);}")});

                this.xrTableCell26.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 100F);
                this.xrTableCell26.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;


                //Tanggal value sa hindi kailangan na cell
                this.xrTableCell33.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "")});
                this.xrTableCell2.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 100F);
                this.xrTableCell2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;


                this.xrTableCell36.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "")});
                this.xrTableCell7.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 100F);
                this.xrTableCell7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;


                //Adjusting Positions
                this.xrTable2.LocationFloat = new DevExpress.Utils.PointFloat(174.6668F, 0F);
                this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(174.6668F, 0F);
                this.xrTable3.LocationFloat = new DevExpress.Utils.PointFloat(174.6667F, 0F);


            }


            //---------------------------------------
            // Hide ALL Code
            //---------------------------------------
            if (report.Parameters["ShowSubsiCode"].Value.ToString() == "False"
                && report.Parameters["ShowProfitCenter"].Value.ToString() == "False"
                && report.Parameters["ShowBizPartner"].Value.ToString() == "False")
            {
                xrTableCell1.Text = "TotalDebit";
                this.xrTableCell2.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ProofList.TotalDebitAmount", "{0:#,0.00;(#,0.00);}")});

                xrTableCell3.Text = "TotalCredit";
                this.xrTableCell7.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ProofList.TotalCreditAmount", "{0:#,0.00;(#,0.00);}")});


                //Moving Total Debit Amount To SubsiCode Field and Setting Up!	
                this.xrTableCell33.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ProofList.TotalDebitAmount", "{0:#,0.00;(#,0.00);}")});


                this.xrTableCell2.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 100F);
                this.xrTableCell2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;

                //Moving Total Credit Amount To SubsiCode Description and Setting Up!	
                this.xrTableCell36.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ProofList.TotalCreditAmount", "{0:#,0.00;(#,0.00);}")});

                this.xrTableCell7.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 100F);
                this.xrTableCell7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;

                //Adjusting Positions
                //this.xrTable2.LocationFloat = new DevExpress.Utils.PointFloat(250.3103F, 0F);
                //		//
                this.xrTable2.LocationFloat = new DevExpress.Utils.PointFloat(250.3103F, 0F);
                this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(250.3103F, 0F);
                this.xrTable3.LocationFloat = new DevExpress.Utils.PointFloat(250.3105F, 0F);
            }
        }

        private void xrTableCell21_HtmlItemCreated(object sender, HtmlEventArgs e)
        {
            XtraReport report = (XtraReport)Report;

            if (report.Parameters["ShowSubsiCode"].Value.ToString() == "True" && report.Parameters["ShowProfitCenter"].Value.ToString() == "True" && report.Parameters["ShowBizPartner"].Value.ToString() == "False"
                || report.Parameters["ShowSubsiCode"].Value.ToString() == "False" && report.Parameters["ShowProfitCenter"].Value.ToString() == "True" && report.Parameters["ShowBizPartner"].Value.ToString() == "True"
                || report.Parameters["ShowSubsiCode"].Value.ToString() == "True" && report.Parameters["ShowProfitCenter"].Value.ToString() == "False" && report.Parameters["ShowBizPartner"].Value.ToString() == "True")
            {
                e.ContentCell.Style.Add("border-right", "1px solid #000000 !important");
            }
        }

        private void xrTableCell22_HtmlItemCreated(object sender, HtmlEventArgs e)
        {
            XtraReport report = (XtraReport)Report;

            if (report.Parameters["ShowSubsiCode"].Value.ToString() == "True" && report.Parameters["ShowProfitCenter"].Value.ToString() == "False" && report.Parameters["ShowBizPartner"].Value.ToString() == "False"
                || report.Parameters["ShowSubsiCode"].Value.ToString() == "False" && report.Parameters["ShowProfitCenter"].Value.ToString() == "True" && report.Parameters["ShowBizPartner"].Value.ToString() == "False"
                || report.Parameters["ShowSubsiCode"].Value.ToString() == "False" && report.Parameters["ShowProfitCenter"].Value.ToString() == "False" && report.Parameters["ShowBizPartner"].Value.ToString() == "True")
            {
                e.ContentCell.Style.Add("border-right", "1px solid #000000 !important");
            }
        }


        private void xrTableCell36_HtmlItemCreated(object sender, HtmlEventArgs e)
        {
            XtraReport report = (XtraReport)Report;

            if (report.Parameters["ShowSubsiCode"].Value.ToString() == "False" && report.Parameters["ShowProfitCenter"].Value.ToString() == "False" && report.Parameters["ShowBizPartner"].Value.ToString() == "False")
            {
                e.ContentCell.Style.Add("border-right", "1px solid #000000 !important");
            }
        }
    }
}
