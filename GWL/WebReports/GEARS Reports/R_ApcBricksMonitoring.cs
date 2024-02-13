using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Web;


namespace GWL.WebReports.GEARS_Reports
{
    public partial class R_ApcBricksMonitoring : DevExpress.XtraReports.UI.XtraReport
    {


        public static XRLabel label;
        public static double c = 0.00;
        public static double d = 0.00;


        public R_ApcBricksMonitoring()
        {
            InitializeComponent();
            if (HttpContext.Current.Session["ConnString"] != null)
                sqlDataSource1.Connection.ConnectionString = HttpContext.Current.Session["ConnString"].ToString();
        }

        private void R_ApcBricksMonitoring_DataSourceDemanded(object sender, EventArgs e)
        {
            XtraReport report = (XtraReport)Report;

            string ProdDate = "";

            string[] paramValues = report.Parameters["ProductionDate"].Value as string[];
            //xrTableCell13.Text = string.Empty;
            for (int i = 0; i < paramValues.Length; i++)
            {
                //xrTableCell13.Text += paramValues[i].ToString();
                ProdDate += paramValues[i].ToString();
                if (i < paramValues.Length - 1)
                    //xrTableCell13.Text += ",";
                    ProdDate += ",";
            }

            if (report.Parameters["ProductionDate"].Value.ToString() == "System.String[]")
            {
                report.Parameters["ProductionDate"].Value = ProdDate;
            }

             RCommon.RCommon.RawData(this.DataSource as DevExpress.DataAccess.Sql.SqlDataSource, (this.Report.ToString().Split('.'))[2] + "." + (this.Report.ToString().Split('.'))[3].ToString(), sqlDataSource1.Connection.ConnectionString);
            //double a = 0.00;
            //double b = 0.00;

            //if (xrLabel8.Text == "")
            //{
            //    xrLabel10.Text = "";
            //}
            //else
            //{
            //    a = Double.Parse(xrLabel8.Text);
            //    b = Double.Parse(xrLabel9.Text);

            //    xrLabel10.Text = (a - b).ToString();
            //}
        }
        
        private void R_ApcBricksMonitoring_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }

        private void ReportFooter_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            
        }

        private void ReportFooter_AfterPrint(object sender, EventArgs e)
        {
            
        }

        private void R_ApcBricksMonitoring_AfterPrint(object sender, EventArgs e)
        {

            
            
        }
        private void xrTableCell10_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //XRLabel label1 = (XRLabel)sender;
            //if (label1.Text == "")
            //{

            //}
            //else
            //{
            //    c += Double.Parse(label1.Text);
            //    xrLabel8.Text = c.ToString();
            //}
        }

        private void xrTableCell12_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //XRLabel label1 = (XRLabel)sender;
            //if (label1.Text == "")
            //{

            //}
            //else
            //{
            //    d += Double.Parse(label1.Text);
            //    xrLabel9.Text = d.ToString();
            //}
        }
        private void xrLabel8_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }

        private void xrLabel8_AfterPrint(object sender, EventArgs e)
        {
          
        }

        private void xrTableCell41_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

            label = (XRLabel)sender;

            if (label.Text == "")
            {

            }
            else
            {
                string date1 = xrTableCell6.Text;
                string date2 = label.Text;
                DateTime dt1 = Convert.ToDateTime(date1);
                DateTime dt2 = Convert.ToDateTime(date2);

                if (dt2 > dt1)
                {
                    label.ForeColor = Color.Red;

                }
                else 
                {
                    label.ForeColor = Color.Black;
                }

            }
        }

        private void xrTableCell42_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRLabel label1 = (XRLabel)sender;

            if (label1.Text == "" || xrTableCell6.Text == "" || label.Text == "")
            {

            }
            else
            {
                string date1 = xrTableCell6.Text;
                string date2 = label.Text;
                DateTime dt1 = Convert.ToDateTime(date1);
                DateTime dt2 = Convert.ToDateTime(date2);

                if (dt2 > dt1)
                {
                    label1.ForeColor = Color.Red;

                }
                else
                {
                    label1.ForeColor = Color.Black;
                }

            }
        }

        private void xrTableCell14_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRLabel label1 = (XRLabel)sender;

            if (label1.Text == "" || xrTableCell6.Text == "" || label.Text == "")
            {

            }
            else
            {
                string date1 = xrTableCell6.Text;
                string date2 = label.Text;
                DateTime dt1 = Convert.ToDateTime(date1);
                DateTime dt2 = Convert.ToDateTime(date2);

                if (dt2 > dt1)
                {
                    label1.ForeColor = Color.Red;

                }
                else
                {
                    label1.ForeColor = Color.Black;
                }

            }
        }


        private void xrLabel10_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

            
        }

        private void xrLabel10_AfterPrint(object sender, EventArgs e)
        {
            c = 0.00;
            d = 0.00;
        }

        private void GroupFooter1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
           
        }

        private void xrTableCell12_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            
            //d += Convert.ToDouble(xrTableCell12.Text);
        }

        private void xrTableCell13_SummaryGetResult(object sender, SummaryGetResultEventArgs e)
        {
            
        }

        private void xrTableCell12_SummaryCalculated(object sender, TextFormatEventArgs e)
        {
                //d = Convert.ToDouble(e.Value);
        }

        private void xrTableCell13_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //XRLabel label1 = (XRLabel)sender;

            //if(xrTableCell10.Text != "")
            //{

            //    d = Convert.ToDouble(xrTableCell12.Summary);
            //    c = Double.Parse(xrTableCell10.Text);

            //    double f = c - d;

            //    label1.Text = f.ToString();

            //}

            

        }

        
        

        

    }
}
