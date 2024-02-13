using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using DevExpress.XtraReports;
using System.Web;
using System.Data;

namespace GWL.WebReports.GEARS_Reports
{
    public partial class R_AutoCharge : DevExpress.XtraReports.UI.XtraReport
    {
        public R_AutoCharge()
        {
            InitializeComponent();

            if (HttpContext.Current.Session["ConnString"] != null)
                sqlDataSource1.Connection.ConnectionString = HttpContext.Current.Session["ConnString"].ToString();
        }

        private void ReportHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }

        private void xrTableCell28_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
          
            //table.WidthF = this.colWidthF * dt.Columns.Count;
        }

        private void xrTable3_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
           //XRTable table = ((XRTable)sender);
           // DataTable dt = (DataTable)DataSource;
        }

        private void R_AutoCharge_DataSourceDemanded(object sender, EventArgs e)
        {
            Common.Common.RawData(this.DataSource as DevExpress.DataAccess.Sql.SqlDataSource, (this.Report.ToString().Split('.'))[2] + "." + (this.Report.ToString().Split('.'))[3].ToString(), sqlDataSource1.Connection.ConnectionString);
        }

    }
}
