using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Web;
using System.Data;
using GearsLibrary;

namespace GWL.WebReports.GEARS_Printout
{
    public partial class P_ARMemo : DevExpress.XtraReports.UI.XtraReport
    {
        public P_ARMemo()
        {
            InitializeComponent();
            if (HttpContext.Current.Session["ConnString"] != null)
            sqlDataSource1.Connection.ConnectionString = HttpContext.Current.Session["ConnString"].ToString();
            DataTable dtfullname = Gears.RetriveData2("SELECT UserName FROM IT.Users WHERE UserID='" + HttpContext.Current.Session["userid"].ToString() + "'", HttpContext.Current.Session["ConnString"].ToString());
            DataRow _ret = dtfullname.Rows[0];
            parameter1.Value = _ret[0].ToString();

        }

    }
}
