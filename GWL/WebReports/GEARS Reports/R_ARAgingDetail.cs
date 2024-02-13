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
    public partial class R_ARAgingDetail : DevExpress.XtraReports.UI.XtraReport
    {
        public R_ARAgingDetail()
        {
            InitializeComponent();

            if (HttpContext.Current.Session["ConnString"] != null)
                sqlDataSource1.Connection.ConnectionString = HttpContext.Current.Session["ConnString"].ToString();

            

            //this.GLAccount.Value = HttpContext.Current.Session["ConnString"].ToString();

        }



        private void ReportHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }

        private void R_ARAgingDetail_DataSourceDemanded(object sender, EventArgs e)
        {
            XtraReport report = (XtraReport)Report;

            string businessaccount = "";
            string customer = "";
            string salesman = "";
            string glaraccount = "";
            string showduedate = "";
            string showterms = "";

            //BusinessAccount
            string[] paramValues = report.Parameters["BusinessAccount"].Value as string[];
            //xrTableCell13.Text = string.Empty;
            for (int i = 0; i < paramValues.Length; i++)
            {
                //xrTableCell13.Text += paramValues[i].ToString();
                businessaccount += paramValues[i].ToString();
                if (i < paramValues.Length - 1)
                    //xrTableCell13.Text += ",";
                    businessaccount += ",";
            }

            //Customer
            string[] paramValues2 = report.Parameters["Customer"].Value as string[];
            //xrTableCell18.Text = string.Empty;
            for (int i = 0; i < paramValues2.Length; i++)
            {
                //.Text += paramValues2[i].ToString();
                customer += paramValues2[i].ToString();
                if (i < paramValues2.Length - 1)
                    //xrTableCell18.Text += ",";
                    customer += ",";
            }

            //GL AR Account
            string[] paramValues3 = report.Parameters["GLARAccount"].Value as string[];
            //xrTableCell25.Text = string.Empty;
            for (int i = 0; i < paramValues3.Length; i++)
            {
                //xrTableCell25.Text += paramValues3[i].ToString();
                glaraccount += paramValues3[i].ToString();
                if (i < paramValues3.Length - 1)
                    //xrTableCell25.Text += ",";
                    glaraccount += ",";
            }

            //Salesman
            string[] paramValues4 = report.Parameters["Salesman"].Value as string[];
            //xrTableCell31.Text = string.Empty;
            for (int i = 0; i < paramValues4.Length; i++)
            {
                //xrTableCell31.Text += paramValues4[i].ToString();
                salesman += paramValues4[i].ToString();
                if (i < paramValues4.Length - 1)
                    //xrTableCell31.Text += ",";
                    salesman += ",";
            }



            if (report.Parameters["BusinessAccount"].Value.ToString() == "System.String[]")
            {
                //report.Parameters["BusinessAccount"].Value = xrTableCell13.Text;
                report.Parameters["BusinessAccount"].Value = businessaccount;
            }

            if (report.Parameters["Customer"].Value.ToString() == "System.String[]")
            {
                //report.Parameters["Customer"].Value = xrTableCell18.Text;
                report.Parameters["Customer"].Value = customer;
            }

            if (report.Parameters["GLARAccount"].Value.ToString() == "System.String[]")
            {
                //report.Parameters["GLARAccount"].Value = xrTableCell25.Text;
                report.Parameters["GLARAccount"].Value = glaraccount;
            }

            if (report.Parameters["Salesman"].Value.ToString() == "System.String[]")
            {
                //report.Parameters["Salesman"].Value = xrTableCell31.Text;
                report.Parameters["Salesman"].Value = salesman;
            }

            if (report.Parameters["ShowDueDate"].Value.ToString() == "True")
            {
                showduedate = "Yes";
            }
            if (report.Parameters["ShowDueDate"].Value.ToString() == "False")
            {
                showduedate = "No";
            }
            if (report.Parameters["ShowTerms"].Value.ToString() == "True")
            {
                showterms = "Yes";
            }
            if (report.Parameters["ShowTerms"].Value.ToString() == "False")
            {
                showterms = "No";
            }


            
            if (report.Parameters["ShowTerms"].Value.ToString() == "False"
               && report.Parameters["ShowDueDate"].Value.ToString() == "False")
            {
                this.Table1.DeleteColumn(Header4);
                this.Table1.DeleteColumn(Header5);
                this.Table3.DeleteColumn(Detail4);
                this.Table3.DeleteColumn(Detail5);
            }
            if (report.Parameters["ShowTerms"].Value.ToString() == "True"
              && report.Parameters["ShowDueDate"].Value.ToString() == "False")
            {

                this.Table1.DeleteColumn(Header5);
                this.Table3.DeleteColumn(Detail5);
            }
            if (report.Parameters["ShowTerms"].Value.ToString() == "False"
              && report.Parameters["ShowDueDate"].Value.ToString() == "True")
            {

                this.Table1.DeleteColumn(Header4);
                this.Table3.DeleteColumn(Detail4);
            }
            ////Start of Hiding
            ////-----------------------------------
            //if (report.Parameters["ShowTerms"].Value.ToString() == "True"
            //   && report.Parameters["ShowDueDate"].Value.ToString() == "True")
            //{
            //    Header1.Visible = true;
            //    Header2.Visible = true;
            //    //GroupH1.Visible = true;
            //    //GroupH2.Visible = true;
            //    Detail1.Visible = true;
            //    Detail2.Visible = true;
            //    GroupF1.Visible = true;
            //    GroupF2.Visible = true;
            //    ReportF1.Visible = true;
            //    ReportF2.Visible = true;
            //    ReportFF1.Visible = true;
            //    ReportFF2.Visible = true;

            //    //Setting Header Text
            //    Header1.Text = "DocDate";
            //    Header2.Text = "Trans";
            //    Header3.Text = "DocNumber";
            //    Header4.Text = "Terms";
            //    Header5.Text = "DueDate";

            //    GroupH1.Text = "Customer:";
            //    GroupH2.Text = "   [CustomerName]  ([CustomerCode])";
                


            //    //Detail Binding
            //    this.Detail1.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            //        new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ARAgingDetail.DocDate", "{0:MM/dd/yy}")});
            //    this.Detail2.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            //        new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ARAgingDetail.TransType")});
            //    this.Detail3.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            //        new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ARAgingDetail.DocNumber")});
            //    this.Detail4.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            //        new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ARAgingDetail.Terms")});
            //    this.Detail5.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            //        new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ARAgingDetail.DueDate", "{0:MM/dd/yy}")});

            //    //Setting Border
            //    //this.Header2.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;

            //    //this.Header2.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Top)));
            //    //this.Header3.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Top)));

            //    //this.ReportF2.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;

            //    //Setting Border
            //    //this.Header1.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Top)));
            //    //this.GroupH1.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            //    //this.Detail1.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            //    //this.GroupF1.Borders = DevExpress.XtraPrinting.BorderSide.Left;
            //    //this.ReportF1.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));

            //    ////Group Header
            //    //this.GroupH2.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            //    //this.GroupH3.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;

            //    ////Detail
            //    //this.Detail2.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            //    //this.Detail3.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;



            //    //this.GroupF2.Borders = DevExpress.XtraPrinting.BorderSide.None;
            //    //this.GroupF3.Borders = DevExpress.XtraPrinting.BorderSide.None;

            //    //Position
            //    this.Table1.LocationFloat = new DevExpress.Utils.PointFloat(63.54175F, 0F);
            //    this.Table2.LocationFloat = new DevExpress.Utils.PointFloat(63.54167F, 0F);
            //    this.Table3.LocationFloat = new DevExpress.Utils.PointFloat(63.54159F, 0F);
            //    this.Table4.LocationFloat = new DevExpress.Utils.PointFloat(63.54159F, 0F);
            //    this.Table5.LocationFloat = new DevExpress.Utils.PointFloat(63.54145F, 25.00001F);
            //    this.Table6.LocationFloat = new DevExpress.Utils.PointFloat(63.54145F, 0F);

            //}

            //if (report.Parameters["ShowTerms"].Value.ToString() == "False"
            //   && report.Parameters["ShowDueDate"].Value.ToString() == "False")
            //{

            //    Header1.Visible = false;
            //    Header2.Visible = false;
            //    //GroupH1.Visible = false;
            //    //GroupH2.Visible = false;
            //    Detail1.Visible = false;
            //    Detail2.Visible = false;
            //    GroupF1.Visible = false;
            //    GroupF2.Visible = false;
            //    ReportF1.Visible = false;
            //    ReportF2.Visible = false;
            //    ReportFF1.Visible = false;
            //    ReportFF2.Visible = false;

            //    //Setting Header Text
            //    Header3.Text = "DocDate";
            //    Header4.Text = "Trans";
            //    Header5.Text = "DocNumber";

            //    GroupH1.Text = "Customer:";
            //    GroupH2.Text = "   [CustomerName]  ([CustomerCode])";
            //    GroupH4.Visible = false;
            //    GroupH5.Visible = false;

            //    //Detail Binding
            //    this.Detail3.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            //new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ARAgingDetail.DocDate", "{0:MM/dd/yy}")});
            //    this.Detail4.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            //new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ARAgingDetail.TransType")});
            //    this.Detail5.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            //new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ARAgingDetail.DocNumber")});


            //    //Setting Border
            //    //this.Header3.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Top)));
            //    //this.GroupH3.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            //    //this.Detail3.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            //    //this.GroupF3.Borders = DevExpress.XtraPrinting.BorderSide.Left;
            //    //this.ReportF3.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));


            //    //Position Setup
            //    this.Table1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            //    this.Table2.LocationFloat = new DevExpress.Utils.PointFloat(140F, 0F);
            //    this.Table3.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            //    this.Table4.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            //    this.Table5.LocationFloat = new DevExpress.Utils.PointFloat(0F, 25.00001F);
            //    this.Table6.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            //}

            //if (report.Parameters["ShowTerms"].Value.ToString() == "True"
            //   && report.Parameters["ShowDueDate"].Value.ToString() == "False")
            //{
            //    Header1.Visible = false;
            //    Header2.Visible = true;
            //    //GroupH1.Visible = false;
            //    //GroupH2.Visible = true;
            //    Detail1.Visible = false;
            //    Detail2.Visible = true;
            //    GroupF1.Visible = false;
            //    GroupF2.Visible = true;
            //    ReportF1.Visible = false;
            //    ReportF2.Visible = true;
            //    ReportFF1.Visible = false;
            //    ReportFF2.Visible = true;

            //    //Setting Header Text
            //    Header2.Text = "DocDate";
            //    Header3.Text = "Trans";
            //    Header4.Text = "DocNumber";
            //    Header5.Text = "Terms";

            //    GroupH1.Text = "Customer:";
            //    GroupH2.Text = "   [CustomerName]  ([CustomerCode])";
            //    GroupH4.Visible = true;
            //    GroupH5.Visible = false;


            //    //Detail Binding
            //    this.Detail2.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            //new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ARAgingDetail.DocDate", "{0:MM/dd/yy}")});
            //    this.Detail3.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            //new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ARAgingDetail.TransType")});
            //    this.Detail4.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            //new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ARAgingDetail.DocNumber")});
            //    this.Detail5.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            //new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ARAgingDetail.Terms")});


            //    ////Setting Border
            //    //this.Header2.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Top)));
            //    //this.GroupH2.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            //    //this.Detail2.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            //    //this.GroupF2.Borders = DevExpress.XtraPrinting.BorderSide.Left;
            //    //this.ReportF2.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));


            //    //Setting Border
            //    //this.Header3.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            //    //this.Detail3.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            //    ////this.GroupF3.Borders =  DevExpress.XtraPrinting.BorderSide.Bottom;
            //    //this.ReportF3.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            //    //this.Header3.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Top)));


            //    //this.GroupH3.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            //    //this.GroupF3.Borders = DevExpress.XtraPrinting.BorderSide.None;


            //    //Position
            //    this.Table1.LocationFloat = new DevExpress.Utils.PointFloat(35F, 0F);
            //    this.Table2.LocationFloat = new DevExpress.Utils.PointFloat(105F, 0F);
            //    this.Table3.LocationFloat = new DevExpress.Utils.PointFloat(35F, 0F);
            //    this.Table4.LocationFloat = new DevExpress.Utils.PointFloat(35F, 0F);
            //    this.Table5.LocationFloat = new DevExpress.Utils.PointFloat(35F, 25.00001F);
            //    this.Table6.LocationFloat = new DevExpress.Utils.PointFloat(35F, 0F);

            //}


            //if (report.Parameters["ShowTerms"].Value.ToString() == "False"
            //   && report.Parameters["ShowDueDate"].Value.ToString() == "True")
            //{
            //    Header1.Visible = false;
            //    Header2.Visible = true;
            //    //GroupH1.Visible = false;
            //    //GroupH2.Visible = true;
            //    Detail1.Visible = false;
            //    Detail2.Visible = true;
            //    GroupF1.Visible = false;
            //    GroupF2.Visible = true;
            //    ReportF1.Visible = false;
            //    ReportF2.Visible = true;
            //    ReportFF1.Visible = false;
            //    ReportFF2.Visible = true;

            //    //Setting Header Text
            //    Header2.Text = "DocDate";
            //    Header3.Text = "Trans";
            //    Header4.Text = "DocNumber";
            //    Header5.Text = "DueDate";

            //    GroupH1.Text = "Customer:";
            //    GroupH2.Text = "   [CustomerName]  ([CustomerCode])";
            //    GroupH4.Visible = true;
            //    GroupH5.Visible = false;

            //    //Detail Binding
            //    this.Detail2.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            //        new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ARAgingDetail.DocDate", "{0:MM/dd/yy}")});
            //    this.Detail3.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            //        new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ARAgingDetail.TransType")});
            //    this.Detail4.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            //        new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ARAgingDetail.DocNumber")});
            //    this.Detail5.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            //        new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ARAgingDetail.DueDate", "{0:MM/dd/yy}")});


            //    //Setting Border
            //    //this.Header2.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Top)));
            //    //this.GroupH2.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            //    //this.Detail2.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            //    //this.GroupF2.Borders = DevExpress.XtraPrinting.BorderSide.Left;
            //    //this.ReportF2.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));


            //    //Setting Border
            //    //this.Header3.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;

            //    //this.Detail3.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            //    ////this.GroupF3.Borders =  DevExpress.XtraPrinting.BorderSide.Bottom;
            //    //this.ReportF3.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            //    //this.Header3.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Top)));


            //    //this.GroupH3.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            //    //this.GroupF3.Borders = DevExpress.XtraPrinting.BorderSide.None;

            //    //Position
            //    this.Table1.LocationFloat = new DevExpress.Utils.PointFloat(35F, 0F);
            //    this.Table2.LocationFloat = new DevExpress.Utils.PointFloat(105F, 0F);
            //    this.Table3.LocationFloat = new DevExpress.Utils.PointFloat(35F, 0F);
            //    this.Table4.LocationFloat = new DevExpress.Utils.PointFloat(35F, 0F);
            //    this.Table5.LocationFloat = new DevExpress.Utils.PointFloat(35F, 25.00001F);
            //    this.Table6.LocationFloat = new DevExpress.Utils.PointFloat(35F, 0F);
            //}


            xrLabel1.Text = "Cut-Off Date: [Parameters.CutOff!MMMM dd, yyyy] / BusinessAccount:  " + businessaccount + " / Customer:  " + customer + " / Salesman:  " + salesman + " / GL Account:  " + glaraccount + " / GroupBy:  [Parameters.GroupBy] / ShowDueDate:  " + showduedate + " / ShowTerms:  " + showterms;
        }

        private void R_ARAgingDetail_ParametersRequestBeforeShow(object sender, DevExpress.XtraReports.Parameters.ParametersRequestEventArgs e)
        {
            XtraReport report = (XtraReport)Report;
            DataTable getAR = Gears.RetriveData2("SELECT Value FROM it.systemsettings where Code = 'ARACCT'", HttpContext.Current.Session["ConnString"].ToString());

            string[] ARCode = { getAR.Rows[0]["Value"].ToString() };
            report.Parameters["GLARAccount"].Value = ARCode;
        }
        private void ReportFF2_HtmlItemCreated(object sender, HtmlEventArgs e)
        {
            //XtraReport report = (XtraReport)Report;

            //if (report.Parameters["ShowTerms"].Value.ToString() == "True" && report.Parameters["ShowDueDate"].Value.ToString() == "False"
            // || report.Parameters["ShowTerms"].Value.ToString() == "False" && report.Parameters["ShowDueDate"].Value.ToString() == "True")
            //{
            //    e.ContentCell.Style.Add("border-left", "1px solid #000000 !important");

            //}
        }

        private void ReportFF3_HtmlItemCreated(object sender, HtmlEventArgs e)
        {
            //XtraReport report = (XtraReport)Report;

            //if (report.Parameters["ShowTerms"].Value.ToString() == "False" && report.Parameters["ShowDueDate"].Value.ToString() == "False")
            //{
            //    e.ContentCell.Style.Add("border-left", "1px solid #000000 !important");

            //}
        }

        private void Header2_HtmlItemCreated(object sender, HtmlEventArgs e)
        {
            //XtraReport report = (XtraReport)Report;

            //if (report.Parameters["ShowTerms"].Value.ToString() == "True" && report.Parameters["ShowDueDate"].Value.ToString() == "False"
            // || report.Parameters["ShowTerms"].Value.ToString() == "False" && report.Parameters["ShowDueDate"].Value.ToString() == "True")
            //{
            //    e.ContentCell.Style.Add("border-left", "1px solid #000000 !important");

            //}
        }

        private void GroupH2_HtmlItemCreated(object sender, HtmlEventArgs e)
        {
           
        }

        private void Detail2_HtmlItemCreated(object sender, HtmlEventArgs e)
        {
            //XtraReport report = (XtraReport)Report;

            //if (report.Parameters["ShowTerms"].Value.ToString() == "True" && report.Parameters["ShowDueDate"].Value.ToString() == "False"
            // || report.Parameters["ShowTerms"].Value.ToString() == "False" && report.Parameters["ShowDueDate"].Value.ToString() == "True")
            //{
            //    e.ContentCell.Style.Add("border-left", "1px solid #000000 !important");

            //}
        }

        private void GroupF2_HtmlItemCreated(object sender, HtmlEventArgs e)
        {
            //XtraReport report = (XtraReport)Report;

            //if (report.Parameters["ShowTerms"].Value.ToString() == "True" && report.Parameters["ShowDueDate"].Value.ToString() == "False"
            // || report.Parameters["ShowTerms"].Value.ToString() == "False" && report.Parameters["ShowDueDate"].Value.ToString() == "True")
            //{
            //    e.ContentCell.Style.Add("border-left", "1px solid #000000 !important");

            //}
        }

        private void ReportF2_HtmlItemCreated(object sender, HtmlEventArgs e)
        {
            //XtraReport report = (XtraReport)Report;

            //if (report.Parameters["ShowTerms"].Value.ToString() == "True" && report.Parameters["ShowDueDate"].Value.ToString() == "False"
            // || report.Parameters["ShowTerms"].Value.ToString() == "False" && report.Parameters["ShowDueDate"].Value.ToString() == "True")
            //{
            //    e.ContentCell.Style.Add("border-left", "1px solid #000000 !important");

            //}
        }

        private void Header3_HtmlItemCreated(object sender, HtmlEventArgs e)
        {
            //XtraReport report = (XtraReport)Report;

            //if (report.Parameters["ShowTerms"].Value.ToString() == "False" && report.Parameters["ShowDueDate"].Value.ToString() == "False")
            //{
            //    e.ContentCell.Style.Add("border-left", "1px solid #000000 !important");

            //}
        }

        private void GroupH3_HtmlItemCreated(object sender, HtmlEventArgs e)
        {
            //XtraReport report = (XtraReport)Report;

            //if (report.Parameters["ShowDueDate"].Value.ToString() == "False" && report.Parameters["ShowTerms"].Value.ToString() == "False")
            //{
            //    e.ContentCell.Style.Add("border-right", "1px solid #000000 !important");
            //}
        }

        private void Detail3_HtmlItemCreated(object sender, HtmlEventArgs e)
        {
            //XtraReport report = (XtraReport)Report;

            //if (report.Parameters["ShowTerms"].Value.ToString() == "False" && report.Parameters["ShowDueDate"].Value.ToString() == "False")
            //{
            //    e.ContentCell.Style.Add("border-left", "1px solid #000000 !important");

            //}
        }

        private void GroupF3_HtmlItemCreated(object sender, HtmlEventArgs e)
        {
            //XtraReport report = (XtraReport)Report;

            //if (report.Parameters["ShowTerms"].Value.ToString() == "False" && report.Parameters["ShowDueDate"].Value.ToString() == "False")
            //{
            //    e.ContentCell.Style.Add("border-left", "1px solid #000000 !important");

            //}
        }

        private void ReportF3_HtmlItemCreated(object sender, HtmlEventArgs e)
        {
            //XtraReport report = (XtraReport)Report;

            //if (report.Parameters["ShowTerms"].Value.ToString() == "False" && report.Parameters["ShowDueDate"].Value.ToString() == "False")
            //{
            //    e.ContentCell.Style.Add("border-left", "1px solid #000000 !important");

            //}
        }

        private void Detail1_HtmlItemCreated(object sender, HtmlEventArgs e)
        {
            e.ContentCell.Style.Add("border-left", "1px solid #000000 !important");
        }

        private void xrTableCell59_HtmlItemCreated(object sender, HtmlEventArgs e)
        {
            e.ContentCell.Style.Add("border-right", "1px solid #000000 !important");
        }

        private void GroupH4_HtmlItemCreated(object sender, HtmlEventArgs e)
        {
        //    XtraReport report = (XtraReport)Report;

        //    if (report.Parameters["ShowDueDate"].Value.ToString() == "True" && report.Parameters["ShowTerms"].Value.ToString() == "False"
        //        || report.Parameters["ShowDueDate"].Value.ToString() == "False" && report.Parameters["ShowTerms"].Value.ToString() == "True")
        //    {
        //        e.ContentCell.Style.Add("border-right", "1px solid #000000 !important");
        //    }
        }
    }
}
