using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace GWL.WebReports.GEARS_Reports
{
    public partial class RPlanningMaster : DevExpress.XtraReports.UI.XtraReport
    {
        public RPlanningMaster()
        {
            InitializeComponent();

            DateTime today = DateTime.Now;

            //report.Parameters["DocDateFr"].Value = answer.ToShortDateString();
            this.Parameters["DateFrom"].Value = today.ToShortDateString();
            this.Parameters["DateTo"].Value = today.ToShortDateString();
        }

        private void RPlanningMaster_DataSourceDemanded(object sender, EventArgs e)
        {
            XtraReport report = (XtraReport)Report;

            if (report.Parameters["View"].Value.ToString() == "Monthly")
            {
                DetailReport.Visible = false;
                DetailReport1.Visible = true;
            }
            else
            {
                DetailReport.Visible = true;
                DetailReport1.Visible = false;
            }

            if (report.Parameters["Include"].Value.ToString() == "True")
            {
                this.PageWidth = 1850;
            }
            else
            {
                this.PageWidth = 1160;
                xrSubreport3.Visible = false;
                xrSubreport6.Visible = false;
            }

            string Holder = "";//
            string[] paramValues = report.Parameters["WorkCenter"].Value as string[];
            //xrLabel1.Text = string.Empty;
            for (int i = 0; i < paramValues.Length; i++)
            {
                Holder += paramValues[i].ToString();
                if (i < paramValues.Length - 1)
                    Holder += ",";
            }
            //if (report.Parameters["DocNumber"].Value.ToString() == "System.String[]")
            //{
            report.Parameters["WorkCenter1"].Value = Holder;
            //}
        }

    }
}
