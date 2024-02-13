using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Web;

namespace GWL.WebReports.GEAR_Reports
{
    public partial class AppliedDocNumber : DevExpress.XtraReports.UI.XtraReport
    {
        public AppliedDocNumber()
        {
            InitializeComponent();
        }

        private void AppliedDocNumber_DataSourceDemanded(object sender, EventArgs e)
        {
            if (HttpContext.Current.Session["ConnString"] != null)
                sqlDataSource1.Connection.ConnectionString = HttpContext.Current.Session["ConnString"].ToString();
        }

    }
}
