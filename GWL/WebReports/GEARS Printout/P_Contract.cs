using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Web;

namespace GWL.WebReports.GEARS_Printout
{
    public partial class P_Contract : DevExpress.XtraReports.UI.XtraReport
    {
        public P_Contract()
        {
            InitializeComponent();
            if (HttpContext.Current.Session["ConnString"] != null)
                sqlDataSource1.Connection.ConnectionString = HttpContext.Current.Session["ConnString"].ToString();
        }

        private void P_Contract_DataSourceDemanded(object sender, EventArgs e)
        {
            
        }

        private void P_Contract_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //XtraReport report = (XtraReport)Report;
            //string x = report.Parameters["DocNumber"].ToString();
        }

        private void P_Contract_DesignerLoaded(object sender, DevExpress.XtraReports.UserDesigner.DesignerLoadedEventArgs e)
        {
            //if (xrLabel14.Text == "NONSTORAGE")
            //{
            //    xrTableCell1.Visible = false;
            //}
        }

    }
}
