using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Web;

namespace GWL.WebReports.GEARS_Reports
{
    public partial class R_JOOnhandInventory : DevExpress.XtraReports.UI.XtraReport
    {
        public R_JOOnhandInventory()
        {
            InitializeComponent();
            XtraReport report = (XtraReport)Report;

            if (HttpContext.Current.Session["ConnString"] != null)
                sqlDataSource1.Connection.ConnectionString = HttpContext.Current.Session["ConnString"].ToString();
            DateTime today = DateTime.Now;
            report.Parameters["CutOffDate"].Value = today.ToShortDateString();
        }

        private void R_JOOnhandInventory_DataSourceDemanded(object sender, EventArgs e)
        {
            XtraReport report = (XtraReport)Report;
            string docnumber = "";
            string[] paramValues3 = report.Parameters["JONumber"].Value as string[];
            //xrTableCell25.Text = string.Empty;
            for (int i = 0; i < paramValues3.Length; i++)
            {
                docnumber += paramValues3[i].ToString();
                if (i < paramValues3.Length - 1)
                    docnumber += ",";
            }

            if (report.Parameters["JONumber"].Value.ToString() == "System.String[]")
            {
                //report.Parameters["Customer"].Value = xrTableCell18.Text;
                report.Parameters["JONumber"].Value = docnumber;
            }
        }

    }
}
