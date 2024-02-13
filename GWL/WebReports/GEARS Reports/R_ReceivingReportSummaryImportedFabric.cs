using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using DevExpress.XtraReports;
using DevExpress.XtraReports.Parameters;
using System.Web;

namespace GWL.WebReports.GEARS_Reports
{
    public partial class R_ReceivingReportSummaryImportedFabric : DevExpress.XtraReports.UI.XtraReport
    {
        public R_ReceivingReportSummaryImportedFabric()
        {
            InitializeComponent();
            if (HttpContext.Current.Session["ConnString"] != null)
                sqlDataSource1.Connection.ConnectionString = HttpContext.Current.Session["ConnString"].ToString();
        }

        private void R_ReceivingReportSummaryImportedFabric_DataSourceDemanded(object sender, EventArgs e)
        {
            XtraReport report = (XtraReport)Report;

            string Passitemcode = "";

            string[] paramValues = report.Parameters["ItemCode"].Value as string[];
            if (paramValues != null)
            {
                for (int i = 0; i < paramValues.Length; i++)
                {
                    Passitemcode += paramValues[i].ToString();
                    if (i < paramValues.Length - 1)
                        Passitemcode += ",";
                }



                report.Parameters["ItemCode"].Value = Passitemcode;
            }
            else {
               Passitemcode= report.Parameters["ItemCode"].Value.ToString();
               report.Parameters["ItemCodeParam"].Value = Passitemcode;
            }

            string ColorCode = "";

            string[] paramValues1 = report.Parameters["ColorCode"].Value as string[];
            if (paramValues1 != null)
            {
                for (int i = 0; i < paramValues1.Length; i++)
                {
                    ColorCode += paramValues1[i].ToString();
                    if (i < paramValues1.Length - 1)
                        ColorCode += ",";
                }



                report.Parameters["ColorCode"].Value = ColorCode;
            }
            else {
                ColorCode = report.Parameters["ColorCode"].Value.ToString();
                report.Parameters["ColorCodeParam"].Value = ColorCode;
            }

            string PONumber = "";

            string[] paramValues2 = report.Parameters["PONumber"].Value as string[];
            if (paramValues2 != null)
            {
                for (int i = 0; i < paramValues2.Length; i++)
                {
                    PONumber += paramValues2[i].ToString();
                    if (i < paramValues2.Length - 1)
                        PONumber += ",";
                }



                report.Parameters["PONumberParam"].Value = PONumber;

            }
            else {
               PONumber= report.Parameters["PONumber"].Value.ToString();
               report.Parameters["PONumberParam"].Value = PONumber;
            }

        }

        private void R_ReceivingReportSummaryImportedFabric_ParametersRequestBeforeShow(object sender, ParametersRequestEventArgs e)
        {


        }

    }
}
