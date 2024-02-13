using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using DevExpress.XtraReports;
using System.Web;

namespace GWL.WebReports.GEARS_Reports
{
    public partial class R_CodingToBoxing : DevExpress.XtraReports.UI.XtraReport
    {
        public R_CodingToBoxing()
        {
            InitializeComponent();

            if (HttpContext.Current.Session["ConnString"] != null)
                sqlDataSource1.Connection.ConnectionString = HttpContext.Current.Session["ConnString"].ToString();
        }

        private void ReportHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }

        private void R_CodingToBoxing_DataSourceDemanded(object sender, EventArgs e)
        {
             RCommon.RCommon.RawData(this.DataSource as DevExpress.DataAccess.Sql.SqlDataSource, (this.Report.ToString().Split('.'))[2] + "." + (this.Report.ToString().Split('.'))[3].ToString(), sqlDataSource1.Connection.ConnectionString);
        }

    }
}
