using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;

namespace GWL.WebReports
{
    public partial class SSRSViewer : System.Web.UI.Page
    {
        static string _viewerHeight;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ReportViewer1.ServerReport.ReportPath = "/GWL Report/" + Request.QueryString["val"].ToString().Substring(1);
                ReportViewer1.ServerReport.SetParameters(new ReportParameter("ConnectionString", Session["ConnString"].ToString(), true));
            }
        }

        protected void ReportViewer1_ReportRefresh(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Microsoft.Reporting.WebForms.ReportViewer viewer = sender as Microsoft.Reporting.WebForms.ReportViewer;
            
            viewer.Style.Add("height", _viewerHeight);
        }

        [System.Web.Services.WebMethod]
        public static void SetViewerHeight(string strHeight)
        {
            _viewerHeight = strHeight;
        }
    }
}