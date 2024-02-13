using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Web;

namespace GWL.WebReports.GEARS_Reports
{
    public partial class R_JOClosure : DevExpress.XtraReports.UI.XtraReport
    {
        public R_JOClosure()
        {
            InitializeComponent();

            if (HttpContext.Current.Session["ConnString"] != null)
                sqlDataSource1.Connection.ConnectionString = HttpContext.Current.Session["ConnString"].ToString();
        }

    }
}
