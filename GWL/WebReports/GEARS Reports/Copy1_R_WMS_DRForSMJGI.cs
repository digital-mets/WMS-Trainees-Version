using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using DevExpress.XtraReports;
using System.Web;
using DevExpress.Data.Filtering;

namespace GWL.WebReports.GEARS_Reports
{
    public partial class R_WMS_DRForSMJGI : DevExpress.XtraReports.UI.XtraReport
    {
        public R_WMS_DRForSMJGI()
        {
            InitializeComponent();

            //XtraReport report = new XtraReport();
            //string PrintSession = "dotmatrix";
            //if (PrintSession == "dotmatrix")
            //{
            //    int OldWidth = this.PageWidth;
            //    this.Landscape = !this.Landscape;
            //    this.PrintingSystem.Document.AutoFitToPagesWidth = 1;
            //}
            //else if (PrintSession == "normal")
            //{
            //    this.Landscape = this.Landscape;
            //}

            if (HttpContext.Current.Session["ConnString"] != null)
                sqlDataSource1.Connection.ConnectionString = HttpContext.Current.Session["ConnString"].ToString();
        }

        private void R_WMS_PendingForPutaway_DataSourceDemanded(object sender, EventArgs e)
        {
        }

        private void xrPivotGrid2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRPivotGrid grid = (XRPivotGrid)sender;
            var groupValue = GetCurrentColumnValue("ItemCode");
            //grid.Prefilter.CriteriaString = string.Format("[ItemCode] == {0}", groupValue);
            //grid.Prefilter.Criteria = CriteriaOperator.Parse("[ItemCode] == " + groupValue.ToString());
            grid.Prefilter.Criteria = new BinaryOperator(new OperandProperty("ItemCode"), new OperandValue(groupValue.ToString()), BinaryOperatorType.Equal); 
        }
    }
}
