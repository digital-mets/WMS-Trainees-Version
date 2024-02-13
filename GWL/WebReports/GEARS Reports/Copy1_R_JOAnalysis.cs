using System;
using System.Web;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using DevExpress.XtraReports;
using DevExpress.XtraPivotGrid;
using DevExpress.Data.PivotGrid;

namespace GWL.WebReports.GEARS_Reports
{
    public partial class R_JOAnalysis : DevExpress.XtraReports.UI.XtraReport
    {
        public R_JOAnalysis()
        {
            InitializeComponent();
            if (HttpContext.Current.Session["ConnString"] != null)
                sqlDataSource1.Connection.ConnectionString = HttpContext.Current.Session["ConnString"].ToString();
        }

        private void ReportHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }
        //PivotGridField field = pivotGridControl1.Fields["CategoryName"];
        //pivotGridControl1.BeginUpdate();
        //try {
        //   // Clear the custom total collection.
        //   field.CustomTotals.Clear();
        //   // Add four items to the custom total collection to calculate the Average, 
        //   // Sum, Max and Min summaries.
        //   field.CustomTotals.Add(PivotSummaryType.Average);
        //   field.CustomTotals.Add(PivotSummaryType.Sum);
        //   field.CustomTotals.Add(PivotSummaryType.Max);
        //   field.CustomTotals.Add(PivotSummaryType.Min);
        //   // Make the custom totals visible for this field.
        //   field.TotalsVisibility = PivotTotalsVisibility.CustomTotals; 
        //}
        //finally {
        //   pivotGridControl1.EndUpdate();
        //}
    }
}
