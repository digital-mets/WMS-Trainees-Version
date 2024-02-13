using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using DevExpress.XtraReports;
using System.Web;
namespace GWL.WebReports.GEARS_Reports
{
    // 05 - 23 - 2016 LGE Add Page Header Band.
    public partial class R_ProfitAnalysis : DevExpress.XtraReports.UI.XtraReport
    {
        public R_ProfitAnalysis()
        {
            InitializeComponent();
            if (HttpContext.Current.Session["ConnString"] != null)
                sqlDataSource1.Connection.ConnectionString = HttpContext.Current.Session["ConnString"].ToString();

        }

        private void ReportHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }

 

      

    }
}
