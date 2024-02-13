using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using DevExpress.XtraReports;
using System.Web;

namespace GWL.WebReports.GEARS_Reports
{
    public partial class R_SLGLVariance : DevExpress.XtraReports.UI.XtraReport
    {
        public R_SLGLVariance()
        {
            InitializeComponent();

            if (HttpContext.Current.Session["ConnString"] != null)
                sqlDataSource1.Connection.ConnectionString = HttpContext.Current.Session["ConnString"].ToString();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //XtraReport report = (XtraReport)Report;

            ////GL Account Code
            ////string Holder;
            //string[] paramValues = report.Parameters["GLAccount"].Value as string[];
            //xrTableCell13.Text = string.Empty;
            //for (int i = 0; i < paramValues.Length; i++)
            //{
            //    xrTableCell13.Text += paramValues[i].ToString();
            //    if (i < paramValues.Length - 1)
            //        xrTableCell13.Text += ",";
            //}

            ////globalholder = xrTableCell13.Text;

            //if (report.Parameters["GLAccount"].Value.ToString() == "System.String[]")
            //{
            //    report.Parameters["GLAccount"].Value = xrTableCell13.Text;
            //}


            //if (report.Parameters["Type"].Value.ToString() == "Summary")
            //{

            //    xrTableCell4.Text = "GL Code";
            //    this.xrTableCell19.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            //new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_SLGLVarianceReport.GLCode")});

            //    xrTableCell5.Text = "GL Account Description";
            //    this.xrTableCell20.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            //new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_SLGLVarianceReport.GLAccountDescription")});


            //    xrTableCell1.Text = "SubsiCode";
            //    this.xrTableCell7.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            //new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_SLGLVarianceReport.SubsiCode")});


            //    xrTableCell3.Text = "ProfitCenter";
            //    this.xrTableCell23.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            //new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_SLGLVarianceReport.ProfitCenterCode")});

            //    xrTableCell6.Text = "CostCenter";
            //    this.xrTableCell26.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            //new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_SLGLVarianceReport.CostCenterCode")});

            //    xrTableCell11.Text = "SL Amount";
            //    this.xrTableCell28.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            //new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_SLGLVarianceReport.SLAmount")});


            //    xrTableCell12.Text = "GL Amount";
            //    this.xrTableCell29.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            //new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_SLGLVarianceReport.GLAmount", "{0:n}")});


            //    xrTableCell15.Text = "Variance";
            //    this.xrTableCell27.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            //new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_SLGLVarianceReport.Variance")});


            //}

            //if (report.Parameters["Type"].Value.ToString() == "Detail")
            //{

            //    xrTableCell4.Text = "SubsiCode";
            //    this.xrTableCell19.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            //new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_SLGLVarianceReport.SubsiCode")});


            //    xrTableCell5.Text = "ProfitCenter";
            //    this.xrTableCell20.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            //new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_SLGLVarianceReport.ProfitCenterCode")});


            //    xrTableCell1.Text = "CostCenter";
            //    this.xrTableCell7.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            //new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_SLGLVarianceReport.CostCenterCode")});



            //    xrTableCell3.Text = "TransType";
            //    this.xrTableCell23.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            //new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_SLGLVarianceReport.TransType")});


            //    xrTableCell6.Text = "DocNumber";
            //    this.xrTableCell26.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            //new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_SLGLVarianceReport.DocNumber")});


            //    xrTableCell11.Text = "SL Amount";
            //    this.xrTableCell28.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            //new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_SLGLVarianceReport.SLAmount")});


            //    xrTableCell12.Text = "GL Amount";
            //    this.xrTableCell29.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            //new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_SLGLVarianceReport.GLAmount", "{0:n}")});


            //    xrTableCell15.Text = "Variance";
            //    this.xrTableCell27.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            //new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_SLGLVarianceReport.Variance")});


           // }
        }



        private void R_SLGLVariance_ParametersRequestSubmit(object sender, DevExpress.XtraReports.Parameters.ParametersRequestEventArgs e)
        {
            


        }

        private void R_SLGLVariance_DataSourceDemanded(object sender, EventArgs e)
        {
            XtraReport report = (XtraReport)Report;

            //GL Account Code
            string accountcode = "";
            string[] paramValues = report.Parameters["GLAccount"].Value as string[];
            //xrTableCell13.Text = string.Empty;
            for (int i = 0; i < paramValues.Length; i++)
            {
                //xrTableCell13.Text += paramValues[i].ToString();
                accountcode += paramValues[i].ToString();
                if (i < paramValues.Length - 1)
                    //xrTableCell13.Text += ",";
                    accountcode += ",";
            }

            //globalholder = xrTableCell13.Text;

            if (report.Parameters["GLAccount"].Value.ToString() == "System.String[]")
            {
                report.Parameters["GLAccount"].Value = accountcode;
            }


            if (report.Parameters["Type"].Value.ToString() == "Summary")
            {

                xrTableCell4.Text = "GL Code";
                this.xrTableCell19.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_SLGLVarianceReport.GLCode")});

                xrTableCell5.Text = "GL Account Description";
                this.xrTableCell20.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_SLGLVarianceReport.GLAccountDescription")});


                xrTableCell1.Text = "SubsiCode";
                this.xrTableCell7.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_SLGLVarianceReport.SubsiCode")});


                xrTableCell3.Text = "ProfitCenter";
                this.xrTableCell23.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_SLGLVarianceReport.ProfitCenterCode")});

                xrTableCell6.Text = "CostCenter";
                this.xrTableCell26.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_SLGLVarianceReport.CostCenterCode")});

                xrTableCell11.Text = "SL Amount";
                this.xrTableCell28.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_SLGLVarianceReport.SLAmount")});


                xrTableCell12.Text = "GL Amount";
                this.xrTableCell29.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_SLGLVarianceReport.GLAmount", "{0:n}")});


                xrTableCell15.Text = "Variance";
                this.xrTableCell27.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_SLGLVarianceReport.Variance")});


            }

            if (report.Parameters["Type"].Value.ToString() == "Detail")
            {

                xrTableCell4.Text = "SubsiCode";
                this.xrTableCell19.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_SLGLVarianceReport.SubsiCode")});


                xrTableCell5.Text = "ProfitCenter";
                this.xrTableCell20.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_SLGLVarianceReport.ProfitCenterCode")});


                xrTableCell1.Text = "CostCenter";
                this.xrTableCell7.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_SLGLVarianceReport.CostCenterCode")});



                xrTableCell3.Text = "TransType";
                this.xrTableCell23.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_SLGLVarianceReport.TransType")});


                xrTableCell6.Text = "DocNumber";
                this.xrTableCell26.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_SLGLVarianceReport.DocNumber")});


                xrTableCell11.Text = "SL Amount";
                this.xrTableCell28.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_SLGLVarianceReport.SLAmount")});


                xrTableCell12.Text = "GL Amount";
                this.xrTableCell29.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_SLGLVarianceReport.GLAmount", "{0:n}")});


                xrTableCell15.Text = "Variance";
                this.xrTableCell27.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_SLGLVarianceReport.Variance")});


            }






            string x = report.Parameters["Month"].Value.ToString();
            switch (x)
            {
                case "1":
                    xrLabel1.Text = "For the Period January [Parameters.Year] / GL AccountCode:  " + accountcode + " / Type:  [Parameters.Type";
                    //BilddatenPictureBox.DataBindings.Add(New XRBinding("Text", Nothing, "Kopf.Federbild")
                    //BilddatenPictureBox.DataBindings.Add(New XtraReport.UI.XRBinding[]("Text", Nothing, "Kopf.Federbild"));

                    //this.xrTableCell20.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
                    //new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_CollectionPeriodDetail.TransDoc", "{0:MM/dd/yy}")});

                    break;
                case "2":
                    xrLabel1.Text = "For the Period February [Parameters.Year] / GL AccountCode:  " + accountcode + " / Type:  [Parameters.Type]";
                    break;
                case "3":
                    xrLabel1.Text = "For the Period March [Parameters.Year] / GL AccountCode:  " + accountcode + " / Type:  [Parameters.Type]";
                    break;
                case "4":
                    xrLabel1.Text = "For the Period April [Parameters.Year] / GL AccountCode:  " + accountcode + " / Type:  [Parameters.Type]";
                    break;
                case "5":
                    xrLabel1.Text = "For the Period May [Parameters.Year] / GL AccountCode:  " + accountcode + " / Type:  [Parameters.Type]";
                    break;
                case "6":
                    xrLabel1.Text = "For the Period June [Parameters.Year] / GL AccountCode:  " + accountcode + " / Type:  [Parameters.Type]";
                    break;
                case "7":
                    xrLabel1.Text = "For the Period July [Parameters.Year] / GL AccountCode:  " + accountcode + " / Type:  [Parameters.Type]";
                    break;
                case "8":
                    xrLabel1.Text = "For the Period August [Parameters.Year] / GL AccountCode:  " + accountcode + " / Type:  [Parameters.Type]";
                    break;
                case "9":
                    xrLabel1.Text = "For the Period September [Parameters.Year] / GL AccountCode:  " + accountcode + " / Type:  [Parameters.Type]";
                    break;
                case "10":
                    xrLabel1.Text = "For the Period October [Parameters.Year] / GL AccountCode:  " + accountcode + " / Type:  [Parameters.Type]";
                    break;
                case "11":
                    xrLabel1.Text = "For the Period November [Parameters.Year] / GL AccountCode:  " + accountcode + " / Type:  [Parameters.Type]";
                    break;
                default:
                    xrLabel1.Text = "For the Period December [Parameters.Year] / GL AccountCode:  " + accountcode + " / Type:  [Parameters.Type]";
                    break;

            }
	

        }

        private void xrTableCell19_HtmlItemCreated(object sender, HtmlEventArgs e)
        {

            e.ContentCell.Style.Add("border-left", "1px solid #000000 !important");
            e.ContentCell.Style.Add("border-bottom", "1px solid #dcdcdc !important");  
        }

        private void xrTableCell27_HtmlItemCreated(object sender, HtmlEventArgs e)
        {

            e.ContentCell.Style.Add("border-right", "1px solid #000000 !important");
            e.ContentCell.Style.Add("border-bottom", "1px solid #dcdcdc !important");  
        }
        //void xrTableCell17_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        //{
        //    //XRTableCell label = sender as XRLabel;
        //    XtraReport rootReport = xrTableCell17.Report as XtraReport;
        //    string[] paramValues = rootReport.Parameters["BizPartner"].Value as string[];
        //    xrTableCell17.Text = string.Empty;
        //    for (int i = 0; i < paramValues.Length; i++)
        //    {
        //        xrTableCell17.Text += paramValues[i].ToString();
        //        if (i < paramValues.Length - 1)
        //            xrTableCell17.Text += ",";
        //    }
        //}


    }
}
