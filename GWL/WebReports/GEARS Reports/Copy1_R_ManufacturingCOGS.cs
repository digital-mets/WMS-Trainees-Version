using System;
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
    public partial class R_ManufacturingCOGS : DevExpress.XtraReports.UI.XtraReport
    {
        public R_ManufacturingCOGS()
        {
            InitializeComponent();

            if (HttpContext.Current.Session["ConnString"] != null)
                sqlDataSource1.Connection.ConnectionString = HttpContext.Current.Session["ConnString"].ToString();
            DevExpress.DataAccess.Sql.SqlDataSource.DisableCustomQueryValidation = true;
          
            

            //this.GLAccount.Value = HttpContext.Current.Session["ConnString"].ToString();

        }

        private void R_ServiceOrderSubsi_DataSourceDemanded(object sender, EventArgs e)
        {
            XtraReport report = (XtraReport)Report;
            string productcategory = "";

          

           
        }



    


    }
}
