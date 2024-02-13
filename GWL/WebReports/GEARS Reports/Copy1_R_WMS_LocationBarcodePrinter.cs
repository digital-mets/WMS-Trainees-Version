using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using DevExpress.XtraReports;
using System.Web;
using DevExpress.XtraReports.Parameters;

namespace GWL.WebReports.GEARS_Reports
{
    public partial class R_WMS_LocationBarcodePrinter : DevExpress.XtraReports.UI.XtraReport
    {
        public R_WMS_LocationBarcodePrinter()
        {
            InitializeComponent();
            if (HttpContext.Current.Session["ConnString"] != null)
                sqlDataSource1.Connection.ConnectionString = HttpContext.Current.Session["ConnString"].ToString();
            
            //DocDateFrom.Value = DateTime.Today.AddDays(-7);
            //DocDateTo.Value = DateTime.Today;
        }


    }
}
