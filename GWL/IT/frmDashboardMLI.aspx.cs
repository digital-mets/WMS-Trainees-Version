using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;
using DevExpress.DataAccess;
using DevExpress.DataAccess.ConnectionParameters;

namespace GWL
{
    public partial class frmDashboardMLI : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ASPxDashboardViewer1_ConfigureDataConnection(object sender, DevExpress.DashboardWeb.ConfigureDataConnectionWebEventArgs e)
        {
            SqlServerConnectionParametersBase s = (SqlServerConnectionParametersBase)e.ConnectionParameters;
            s.ServerName = "192.168.180.9";
            s.DatabaseName = "GWL-LOGISTICS";
            s.UserName = "tech";
            s.Password = "p@ssw0rdtech";

            e.ConnectionParameters = s;
        }
    }
}