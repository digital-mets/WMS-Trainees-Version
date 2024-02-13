using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using DevExpress.XtraReports;
using System.Web;

using GearsLibrary;
using System.Data;

namespace GWL.WebReports.GEARS_Reports
{
    public partial class R_APAgingDetail : DevExpress.XtraReports.UI.XtraReport
    {
        public R_APAgingDetail()
        {
            InitializeComponent();

            if (HttpContext.Current.Session["ConnString"] != null)
                sqlDataSource1.Connection.ConnectionString = HttpContext.Current.Session["ConnString"].ToString();


            

            //GLAPAccount.Value = "2006";

        }



        private void ReportHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            
        }

        private void R_APAgingDetail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }

        private void R_APAgingDetail_ParametersRequestBeforeShow(object sender, DevExpress.XtraReports.Parameters.ParametersRequestEventArgs e)
        {
            XtraReport report = (XtraReport)Report;
            DataTable getAP = Gears.RetriveData2("SELECT Value FROM it.systemsettings where Code = 'APACCT'", HttpContext.Current.Session["ConnString"].ToString());

            string[] APCode = { getAP.Rows[0]["Value"].ToString() };
            report.Parameters["GLAPAccount"].Value = APCode;
        }

        private void R_APAgingDetail_DataSourceDemanded(object sender, EventArgs e)
        {
            XtraReport report = (XtraReport)Report;

            string businesssubclass = "";
            string supplier = "";
            string glapaccount = "";
            string showduedate = "";
            string showterms = "";

            //Business Sub Class
            string[] paramValues = report.Parameters["BusinessSubClass"].Value as string[];
            //xrTableCell13.Text = string.Empty;
            for (int i = 0; i < paramValues.Length; i++)
            {
                //xrTableCell13.Text += paramValues[i].ToString();
                businesssubclass += paramValues[i].ToString();
                if (i < paramValues.Length - 1)
                    //xrTableCell13.Text += ",";
                    businesssubclass += ",";
            }

            //GL AP Account
            string[] paramValues2 = report.Parameters["GLAPAccount"].Value as string[];
            //xrTableCell18.Text = string.Empty;
            for (int i = 0; i < paramValues2.Length; i++)
            {
                glapaccount += paramValues2[i].ToString();
                if (i < paramValues2.Length - 1)
                    glapaccount += ",";
            }

            //Supplier Code
            string[] paramValues3 = report.Parameters["Supplier"].Value as string[];
            //xrTableCell25.Text = string.Empty;
            for (int i = 0; i < paramValues3.Length; i++)
            {
                supplier += paramValues3[i].ToString();
                if (i < paramValues3.Length - 1)
                    supplier += ",";
            }

            if (report.Parameters["BusinessSubClass"].Value.ToString() == "System.String[]")
            {
                report.Parameters["BusinessSubClass"].Value = businesssubclass;
            }

            if (report.Parameters["GLAPAccount"].Value.ToString() == "System.String[]")
            {
                report.Parameters["GLAPAccount"].Value = glapaccount;
            }

            if (report.Parameters["Supplier"].Value.ToString() == "System.String[]")
            {
                report.Parameters["Supplier"].Value = supplier;
            }

            if (report.Parameters["ShowTerms"].Value.ToString() == "True")
            {
                showterms = "Yes";
            }
            if (report.Parameters["ShowTerms"].Value.ToString() == "False")
            {
                showterms = "No";
            }
            if (report.Parameters["ShowDueDate"].Value.ToString() == "True")
            {
                showduedate = "Yes";
            }
            if (report.Parameters["ShowDueDate"].Value.ToString() == "False")
            {
                showduedate = "No";
            }


            xrLabel1.Text = "Cut-Off Date: [Parameters.CutOff!MMMM dd, yyyy] / BusinessSubClass:  " + businesssubclass + " / Supplier:  " + supplier + " / GLAPAccount:  " + glapaccount + " / ShowDueDate:  " + showduedate + " / ShowTerms:  " + showterms;


            //Start of Hiding
            //-----------------------------------
            if (report.Parameters["ShowTerms"].Value.ToString() == "True"
               && report.Parameters["ShowDueDate"].Value.ToString() == "True")
            {
                Header1.Visible = true;
                Header2.Visible = true;
                GroupH1.Visible = true;
                GroupH2.Visible = true;
                Detail1.Visible = true;
                Detail2.Visible = true;
                GroupF1.Visible = true;
                GroupF2.Visible = true;
                ReportF1.Visible = true;
                ReportF2.Visible = true;
                ReportFF1.Visible = true;
                ReportFF2.Visible = true;

                //Setting Header Text
                Header1.Text = "DocDate";
                Header2.Text = "Trans";
                Header3.Text = "DocNumber";
                Header4.Text = "Terms";
                Header5.Text = "DueDate";

                GroupH1.Text = "Supplier:";
                GroupH2.Text = "[SupplierCode]";
                GroupH4.Visible = true;
                GroupH5.Visible = true;


                //Detail Binding
                this.Detail1.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
                    new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_APAgingDetail.DocDate", "{0:MM/dd/yy}")});
                this.Detail2.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
                    new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_APAgingDetail.TransType")});
                this.Detail3.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
                    new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_APAgingDetail.DocNumber")});
                this.Detail4.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
                    new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_APAgingDetail.Terms")});
                this.Detail5.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
                    new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_APAgingDetail.DueDate", "{0:MM/dd/yy}")});

                //Position
                this.Table1.LocationFloat = new DevExpress.Utils.PointFloat(73.55262F, 0F);
                this.Table2.LocationFloat = new DevExpress.Utils.PointFloat(73.55262F, 0F);
                this.Table3.LocationFloat = new DevExpress.Utils.PointFloat(73.55262F, 0F);
                this.Table4.LocationFloat = new DevExpress.Utils.PointFloat(73.55261F, 0F);
                this.Table5.LocationFloat = new DevExpress.Utils.PointFloat(73.55262F, 25.00003F);

            }

            if (report.Parameters["ShowTerms"].Value.ToString() == "False"
               && report.Parameters["ShowDueDate"].Value.ToString() == "False")
            {

                Header1.Visible = false;
                Header2.Visible = false;
                //GroupH1.Visible = false;
                //GroupH2.Visible = false;
                Detail1.Visible = false;
                Detail2.Visible = false;
                GroupF1.Visible = false;
                GroupF2.Visible = false;
                ReportF1.Visible = false;
                ReportF2.Visible = false;
                ReportFF1.Visible = false;
                ReportFF2.Visible = false;

                GroupH4.Visible = false;
                GroupH5.Visible = false;

                //Setting Header Text
                Header3.Text = "DocDate";
                Header4.Text = "Trans";
                Header5.Text = "DocNumber";

                GroupH1.Text = "Supplier:";
                GroupH2.Text = "[SupplierCode]";
                GroupH4.Visible = false;
                GroupH5.Visible = false;


                //Detail Binding
                this.Detail3.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_APAgingDetail.DocDate", "{0:MM/dd/yy}")});
                this.Detail4.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_APAgingDetail.TransType")});
                this.Detail5.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_APAgingDetail.DocNumber")});

                //Position Setup
                //this.Table2.LocationFloat = new DevExpress.Utils.PointFloat(73.55F, 0F);

                this.Table1.LocationFloat = new DevExpress.Utils.PointFloat(3.55262F, 0F);
                this.Table2.LocationFloat = new DevExpress.Utils.PointFloat(143.55262F, 0F);
                this.Table3.LocationFloat = new DevExpress.Utils.PointFloat(3.55262F, 0F);
                this.Table4.LocationFloat = new DevExpress.Utils.PointFloat(3.55262F, 0F);
                this.Table5.LocationFloat = new DevExpress.Utils.PointFloat(3.55262F, 25.00003F);
                this.Table6.LocationFloat = new DevExpress.Utils.PointFloat(3.55262F, 0F);
            }

            if (report.Parameters["ShowTerms"].Value.ToString() == "True"
               && report.Parameters["ShowDueDate"].Value.ToString() == "False")
            {
                Header1.Visible = false;
                Header2.Visible = true;
                //GroupH1.Visible = false;
                //GroupH2.Visible = true;
                Detail1.Visible = false;
                Detail2.Visible = true;
                GroupF1.Visible = false;
                GroupF2.Visible = true;
                ReportF1.Visible = false;
                ReportF2.Visible = true;
                ReportFF1.Visible = false;
                ReportFF2.Visible = true;

            
                //Setting Header Text
                Header2.Text = "DocDate";
                Header3.Text = "Trans";
                Header4.Text = "DocNumber";
                Header5.Text = "Terms";

                GroupH1.Text = "Supplier:";
                GroupH2.Text = "[SupplierCode]";
                GroupH4.Visible = true;
                GroupH5.Visible = false;



                //Detail Binding
                this.Detail2.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_APAgingDetail.DocDate", "{0:MM/dd/yy}")});
                this.Detail3.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_APAgingDetail.TransType")});
                this.Detail4.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_APAgingDetail.DocNumber")});
                this.Detail5.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_APAgingDetail.Terms")});


              

                //Position
                //this.Table2.LocationFloat = new DevExpress.Utils.PointFloat(73.55F, 0F);
                this.Table1.LocationFloat = new DevExpress.Utils.PointFloat(38.55262F, 0F);
                //this.Table2.LocationFloat = new DevExpress.Utils.PointFloat(38.55262F, 0F);
                this.Table2.LocationFloat = new DevExpress.Utils.PointFloat(108.55F, 0F);
                this.Table3.LocationFloat = new DevExpress.Utils.PointFloat(38.55262F, 0F);
                this.Table4.LocationFloat = new DevExpress.Utils.PointFloat(38.55262F, 0F);
                this.Table5.LocationFloat = new DevExpress.Utils.PointFloat(38.55262F, 25.00003F);
                this.Table6.LocationFloat = new DevExpress.Utils.PointFloat(38.55262F, 0F);

            }


            if (report.Parameters["ShowTerms"].Value.ToString() == "False"
               && report.Parameters["ShowDueDate"].Value.ToString() == "True")
            {
                Header1.Visible = false;
                Header2.Visible = true;
                //GroupH1.Visible = false;
                //GroupH2.Visible = true;
                Detail1.Visible = false;
                Detail2.Visible = true;
                GroupF1.Visible = false;
                GroupF2.Visible = true;
                ReportF1.Visible = false;
                ReportF2.Visible = true;
                ReportFF1.Visible = false;
                ReportFF2.Visible = true;

                //Setting Header Text
                Header2.Text = "DocDate";
                Header3.Text = "Trans";
                Header4.Text = "DocNumber";
                Header5.Text = "DueDate";

                GroupH1.Text = "Supplier:";
                GroupH2.Text = "[SupplierCode]";
                GroupH4.Visible = true;
                GroupH5.Visible = false;


                //Detail Binding
                this.Detail2.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
                    new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_APAgingDetail.DocDate", "{0:MM/dd/yy}")});
                this.Detail3.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
                    new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_APAgingDetail.TransType")});
                this.Detail4.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
                    new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_APAgingDetail.DocNumber")});
                this.Detail5.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
                    new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_APAgingDetail.DueDate", "{0:MM/dd/yy}")});



                //Position
                this.Table1.LocationFloat = new DevExpress.Utils.PointFloat(38.55262F, 0F);
                this.Table2.LocationFloat = new DevExpress.Utils.PointFloat(108.55F, 0F);
                //this.Table2.LocationFloat = new DevExpress.Utils.PointFloat(38.55262F, 0F);
                this.Table3.LocationFloat = new DevExpress.Utils.PointFloat(38.55262F, 0F);
                this.Table4.LocationFloat = new DevExpress.Utils.PointFloat(38.55262F, 0F);
                this.Table5.LocationFloat = new DevExpress.Utils.PointFloat(38.55262F, 25.00003F);
                this.Table6.LocationFloat = new DevExpress.Utils.PointFloat(38.55262F, 0F);
            }



        }

        private void ReportFF2_HtmlItemCreated(object sender, HtmlEventArgs e)
        {

        }

        private void ReportFF3_HtmlItemCreated(object sender, HtmlEventArgs e)
        {

        }

        private void Header2_HtmlItemCreated(object sender, HtmlEventArgs e)
        {

        }

        private void GroupH2_HtmlItemCreated(object sender, HtmlEventArgs e)
        {

        }

        private void Detail2_HtmlItemCreated(object sender, HtmlEventArgs e)
        {

        }

        private void GroupF2_HtmlItemCreated(object sender, HtmlEventArgs e)
        {
            XtraReport report = (XtraReport)Report;

            if (report.Parameters["ShowDueDate"].Value.ToString() == "True" && report.Parameters["ShowTerms"].Value.ToString() == "False"
                || report.Parameters["ShowDueDate"].Value.ToString() == "False" && report.Parameters["ShowTerms"].Value.ToString() == "True")
            {
                e.ContentCell.Style.Add("border-left", "1px solid #000000 !important");
            }
        }

        private void ReportF2_HtmlItemCreated(object sender, HtmlEventArgs e)
        {

        }

        private void Header3_HtmlItemCreated(object sender, HtmlEventArgs e)
        {

        }

        private void GroupH3_HtmlItemCreated(object sender, HtmlEventArgs e)
        {

        }

        private void Detail3_HtmlItemCreated(object sender, HtmlEventArgs e)
        {

        }

        private void GroupF3_HtmlItemCreated(object sender, HtmlEventArgs e)
        {

        }

        private void ReportF3_HtmlItemCreated(object sender, HtmlEventArgs e)
        {

        }

        private void Header2_HtmlItemCreated_1(object sender, HtmlEventArgs e)
        {
            XtraReport report = (XtraReport)Report;

            if (report.Parameters["ShowDueDate"].Value.ToString() == "True" && report.Parameters["ShowTerms"].Value.ToString() == "False"
                || report.Parameters["ShowDueDate"].Value.ToString() == "False" && report.Parameters["ShowTerms"].Value.ToString() == "True")
            {
                e.ContentCell.Style.Add("border-left", "1px solid #000000 !important"); 
            }
        }

        private void Header3_HtmlItemCreated_1(object sender, HtmlEventArgs e)
        {
            XtraReport report = (XtraReport)Report;

            if (report.Parameters["ShowDueDate"].Value.ToString() == "False" && report.Parameters["ShowTerms"].Value.ToString() == "False")
            {
                e.ContentCell.Style.Add("border-left", "1px solid #000000 !important");
            }
        }

        private void GroupH2_HtmlItemCreated_1(object sender, HtmlEventArgs e)
        {
            XtraReport report = (XtraReport)Report;

            if (report.Parameters["ShowDueDate"].Value.ToString() == "False" && report.Parameters["ShowTerms"].Value.ToString() == "False")
            {
                e.ContentCell.Style.Add("border-right", "1px solid #000000 !important");
            }
        }

        private void GroupH3_HtmlItemCreated_1(object sender, HtmlEventArgs e)
        {
            XtraReport report = (XtraReport)Report;

            if (report.Parameters["ShowDueDate"].Value.ToString() == "False" && report.Parameters["ShowTerms"].Value.ToString() == "False")
            {
                e.ContentCell.Style.Add("border-right", "1px solid #000000 !important");
            }
        }

        private void Detail2_HtmlItemCreated_1(object sender, HtmlEventArgs e)
        {
            XtraReport report = (XtraReport)Report;

            if (report.Parameters["ShowDueDate"].Value.ToString() == "True" && report.Parameters["ShowTerms"].Value.ToString() == "False"
                || report.Parameters["ShowDueDate"].Value.ToString() == "False" && report.Parameters["ShowTerms"].Value.ToString() == "True")
            {
                e.ContentCell.Style.Add("border-left", "1px solid #000000 !important");
            }
        }

        private void Detail3_HtmlItemCreated_1(object sender, HtmlEventArgs e)
        {
            XtraReport report = (XtraReport)Report;

            if (report.Parameters["ShowDueDate"].Value.ToString() == "False" && report.Parameters["ShowTerms"].Value.ToString() == "False")
            {
                e.ContentCell.Style.Add("border-left", "1px solid #000000 !important");
            }
        }

        private void GroupF3_HtmlItemCreated_1(object sender, HtmlEventArgs e)
        {
            XtraReport report = (XtraReport)Report;

            if (report.Parameters["ShowDueDate"].Value.ToString() == "False" && report.Parameters["ShowTerms"].Value.ToString() == "False")
            {
                e.ContentCell.Style.Add("border-left", "1px solid #000000 !important");
            }
        }

        private void ReportFF2_HtmlItemCreated_1(object sender, HtmlEventArgs e)
        {
            XtraReport report = (XtraReport)Report;

            if (report.Parameters["ShowDueDate"].Value.ToString() == "True" && report.Parameters["ShowTerms"].Value.ToString() == "False"
                || report.Parameters["ShowDueDate"].Value.ToString() == "False" && report.Parameters["ShowTerms"].Value.ToString() == "True")
            {
                e.ContentCell.Style.Add("border-left", "1px solid #000000 !important");
            }
        }

        private void ReportFF3_HtmlItemCreated_1(object sender, HtmlEventArgs e)
        {
            XtraReport report = (XtraReport)Report;

            if (report.Parameters["ShowDueDate"].Value.ToString() == "False" && report.Parameters["ShowTerms"].Value.ToString() == "False")
            {
                e.ContentCell.Style.Add("border-left", "1px solid #000000 !important");
            }
        }

        private void ReportF2_HtmlItemCreated_1(object sender, HtmlEventArgs e)
        {
            XtraReport report = (XtraReport)Report;

            if (report.Parameters["ShowDueDate"].Value.ToString() == "True" && report.Parameters["ShowTerms"].Value.ToString() == "False"
                || report.Parameters["ShowDueDate"].Value.ToString() == "False" && report.Parameters["ShowTerms"].Value.ToString() == "True")
            {
                e.ContentCell.Style.Add("border-left", "1px solid #000000 !important");
            }
        }

        private void ReportF3_HtmlItemCreated_1(object sender, HtmlEventArgs e)
        {
            XtraReport report = (XtraReport)Report;

            if (report.Parameters["ShowDueDate"].Value.ToString() == "False" && report.Parameters["ShowTerms"].Value.ToString() == "False")
            {
                e.ContentCell.Style.Add("border-left", "1px solid #000000 !important");
            }
        }

        private void GroupH4_HtmlItemCreated(object sender, HtmlEventArgs e)
        {
            XtraReport report = (XtraReport)Report;

            if (report.Parameters["ShowDueDate"].Value.ToString() == "True" && report.Parameters["ShowTerms"].Value.ToString() == "False"
                || report.Parameters["ShowDueDate"].Value.ToString() == "False" && report.Parameters["ShowTerms"].Value.ToString() == "True")
            {
                e.ContentCell.Style.Add("border-right", "1px solid #000000 !important");
            }
        }

        private void xrTableCell59_HtmlItemCreated(object sender, HtmlEventArgs e)
        {
            e.ContentCell.Style.Add("border-right", "1px solid #000000 !important");
        }

        private void Detail1_HtmlItemCreated(object sender, HtmlEventArgs e)
        {
            e.ContentCell.Style.Add("border-left", "1px solid #000000 !important");
        }


    }
}
