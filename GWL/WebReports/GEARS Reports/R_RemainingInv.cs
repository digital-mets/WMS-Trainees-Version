using System;
using System.Web;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using DevExpress.XtraReports;

namespace GWL.WebReports.GEARS_Reports
{
    public partial class R_RemainingInv : DevExpress.XtraReports.UI.XtraReport
    {
        // 05 - 23 - 2016 LGE Add Page Header Band.
        public R_RemainingInv()
        {
            InitializeComponent();
            if (HttpContext.Current.Session["ConnString"] != null)
                sqlDataSource1.Connection.ConnectionString = HttpContext.Current.Session["ConnString"].ToString();
        }

        private void GroupFooter1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            
        }

        private void R_RemainingInv_DataSourceDemanded(object sender, EventArgs e)
        {
            XtraReport report = (XtraReport)Report;

            string itemcategory = "";

            //Item Category
            string[] paramValues = report.Parameters["ItemCategory"].Value as string[];
            //xrTableCell13.Text = string.Empty;
            for (int i = 0; i < paramValues.Length; i++)
            {
                //xrTableCell13.Text += paramValues[i].ToString();
                itemcategory += paramValues[i].ToString();
                if (i < paramValues.Length - 1)
                    //xrTableCell13.Text += ",";
                    itemcategory += ",";
            }

            if (report.Parameters["ItemCategory"].Value.ToString() == "System.String[]")
            {
                //report.Parameters["BusinessAccount"].Value = xrTableCell13.Text;
                report.Parameters["ItemCategory"].Value = itemcategory;
            }
        }

    }
}
