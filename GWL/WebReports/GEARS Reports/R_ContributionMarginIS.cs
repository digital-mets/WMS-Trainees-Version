using System;
using System.Web;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using DevExpress.XtraReports;
using System.Web;
using System.Data;
using GearsLibrary;
namespace GWL.WebReports.GEARS_Reports
{
    public partial class R_ContributionMarginIS : DevExpress.XtraReports.UI.XtraReport
    {
        // 05 - 23 - 2016 LGE Add Page Header Band.
        public R_ContributionMarginIS()
        {
            InitializeComponent();
            if (HttpContext.Current.Session["ConnString"] != null)
                sqlDataSource1.Connection.ConnectionString = HttpContext.Current.Session["ConnString"].ToString();

            DataTable dtmonth = Gears.RetriveData2("SELECT CONVERT (int, Value) AS Month FROM IT.SystemSettings WHERE Code = 'CMONTH'", HttpContext.Current.Session["ConnString"].ToString());
            DataRow _ret = dtmonth.Rows[0];
            //Month.Value = _ret[0].ToString();

            DataTable dtyear = Gears.RetriveData2("SELECT CONVERT (int, Value) as Year FROM IT.SystemSettings WHERE Code = 'CYEAR'", HttpContext.Current.Session["ConnString"].ToString());
            DataRow _ret2 = dtyear.Rows[0];
            //Year.Description = _ret2[0].ToString();
            Year.Value = _ret2[0].ToString();
              
        
        
        }


        private void R_RemainingInv_DataSourceDemanded(object sender, EventArgs e)
        {
            XtraReport report = (XtraReport)Report;
            report.Parameters["PostBack"].Value = false;
  
        }

    }
}
