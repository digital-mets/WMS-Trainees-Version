using System;
using System.Web;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using GearsLibrary;

namespace GWL.WebReports.GEARS_Printout
{
    public partial class P_AssetDisposalSPH : DevExpress.XtraReports.UI.XtraReport
    {
        public P_AssetDisposalSPH()
        {
            InitializeComponent();
            if (HttpContext.Current.Session["ConnString"] != null)
                sqlDataSource1.Connection.ConnectionString = HttpContext.Current.Session["ConnString"].ToString();
            DevExpress.DataAccess.Sql.SqlDataSource.DisableCustomQueryValidation = true; 
        }

        private void P_AssetDisposalSPH_DataSourceDemanded(object sender, EventArgs e)
        { 
            XtraReport report = (XtraReport)Report;
            DataTable dt = Gears.RetriveData2("SELECT ISNULL(DisposalType,'Sales') AS DisposalType FROM Accounting.AssetDisposal WHERE DocNumber = '" + report.Parameters["DocNumber"].Value.ToString() + "'", HttpContext.Current.Session["ConnString"].ToString());
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0][0].ToString() == "Sales") 
                {
                    this.DetailReport.Visible = false;
                    this.DetailReport1.Visible = true;
                }
                else
                {
                    this.DetailReport.Visible = true;
                    this.DetailReport1.Visible = false;
                }
            }
            else
            {
                this.DetailReport.Visible = true;
                this.DetailReport1.Visible = false;
            }
        }  

    }
}
