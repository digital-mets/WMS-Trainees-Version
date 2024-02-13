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
    public partial class R_DailyProductionReport2Summary : DevExpress.XtraReports.UI.XtraReport
    {
        public R_DailyProductionReport2Summary()
        {
            InitializeComponent();

            if (HttpContext.Current.Session["ConnString"] != null)
                sqlDataSource1.Connection.ConnectionString = HttpContext.Current.Session["ConnString"].ToString();
            
            // 10-21-2021 TA Added Dyanmics default parameters
            XtraReport report = (XtraReport)Report;

            string startDate = "";
            DataTable getDate = Gears.RetriveData2("SELECT Value FROM IT.SystemSettings WHERE Code='BOOKDATE'", HttpContext.Current.Session["ConnString"].ToString());
            foreach (DataRow dtRow in getDate.Rows)
            {
                startDate = dtRow[0].ToString();
            }

            DateTime answer = Convert.ToDateTime(startDate);
            DateTime today = Convert.ToDateTime(startDate);

            //report.Parameters["DateFrom"].Value = answer.ToShortDateString();
            //report.Parameters["DateTo"].Value = today.ToShortDateString();
        }

        private void ReportHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
         
        }

        private void R_DailyProductionReport_DataSourceDemanded(object sender, EventArgs e)
        {
            XtraReport report = (XtraReport)Report;

          
             RCommon.RCommon.RawData(this.DataSource as DevExpress.DataAccess.Sql.SqlDataSource, (this.Report.ToString().Split('.'))[2] + "." + (this.Report.ToString().Split('.'))[3].ToString(), sqlDataSource1.Connection.ConnectionString);
           
           
        }

    


    }
}
