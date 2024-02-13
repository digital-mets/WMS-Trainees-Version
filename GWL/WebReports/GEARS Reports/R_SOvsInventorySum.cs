using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using DevExpress.XtraReports;
using System.Web;
using DevExpress.XtraPivotGrid;

namespace GWL.WebReports.GEARS_Reports
{
    // 05 - 23 - 2016 LGE Add Page Header Band.
    public partial class R_SOvsInventorySum : DevExpress.XtraReports.UI.XtraReport
    {
        public R_SOvsInventorySum()
        {
            InitializeComponent();
            if (HttpContext.Current.Session["ConnString"] != null)
                sqlDataSource1.Connection.ConnectionString = HttpContext.Current.Session["ConnString"].ToString();
        }

        private void ReportHeader_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }

        private void xrTableCell5_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }


        //private void pivotGridControl1_FieldValueDisplayText(object sender, DevExpress.XtraPivotGrid.PivotFieldDisplayTextEventArgs e)
        //{
        //    if (e.ValueType == DevExpress.XtraPivotGrid.PivotGridValueType.GrandTotal && e.DisplayText == "Grand Total")
        //    {
        //        if (e.IsColumn)
        //            e.DisplayText = "Column Grand Total";
        //        else
        //            e.DisplayText = "Row Grand Total";
        //    }
        //}

    }
}
