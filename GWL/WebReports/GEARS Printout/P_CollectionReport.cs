using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Web;

namespace GWL.WebReports.GEARS_Printout
{
    public partial class P_CollectionReport : DevExpress.XtraReports.UI.XtraReport
    {
        public P_CollectionReport()
        {
            InitializeComponent();
            if (HttpContext.Current.Session["ConnString"] != null)
                sqlDataSource1.Connection.ConnectionString = HttpContext.Current.Session["ConnString"].ToString();
        }

        private void xrTableCell13_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
        }

        private void P_CollectionReport_DataSourceDemanded(object sender, EventArgs e)
        {
            XtraReport report = (XtraReport)Report;

            string Holder = "";//
                string[] paramValues = report.Parameters["DocNumber"].Value as string[];
                //xrLabel1.Text = string.Empty;
                for (int i = 0; i < paramValues.Length; i++)
                {
                    Holder += paramValues[i].ToString();
                    if (i < paramValues.Length - 1)
                        Holder += ",";
                }

            //if (report.Parameters["DocNumber"].Value.ToString() == "System.String[]")
            //{
                report.Parameters["DocNumber1"].Value = Holder;
            //}
        }

        private void xrTableCell9_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRLabel label = sender as XRLabel;
            if (label.Text == "Adjustments:")
            {
                label.Font = new Font(label.Font.FontFamily, label.Font.Size, FontStyle.Bold);
                label.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            }
            else
            {
                label.Font = new Font(label.Font.FontFamily, label.Font.Size, FontStyle.Regular);
                //label.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleJustify;
            }		

        }

    }
}
