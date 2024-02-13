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
    public partial class R_WMS_DRForSMJLI : DevExpress.XtraReports.UI.XtraReport
    {
        public R_WMS_DRForSMJLI()
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
            //XRPivotGrid grid = (XRPivotGrid)sender;
            //var groupValue = GetCurrentColumnValue("ItemCode");
            ////grid.Prefilter.CriteriaString = string.Format("[ItemCode] == {0}", groupValue);
            ////grid.Prefilter.Criteria = CriteriaOperator.Parse("[ItemCode] == " + groupValue.ToString());
            //grid.Prefilter.Criteria = new BinaryOperator(new OperandProperty("ItemCode"), new OperandValue(groupValue.ToString()), BinaryOperatorType.Equal);

            XRPivotGrid grid = (XRPivotGrid)sender;
            var groupValue = GetCurrentColumnValue("ItemCode");
            var groupValue2 = GetCurrentColumnValue("DocNumber");
            var groupValue3 = GetCurrentColumnValue("ColorCode");
            //grid.Prefilter.CriteriaString = string.Format("[ItemCode] == {0}", groupValue);
            //grid.Prefilter.Criteria = CriteriaOperator.Parse("[ItemCode] == " + groupValue.ToString());
            //grid.Prefilter.Criteria = new BinaryOperator(new OperandProperty("ItemCode"), new OperandValue(groupValue.ToString()), BinaryOperatorType.Equal;

            grid.Prefilter.Criteria = GroupOperator.And(new BinaryOperator(new OperandProperty("ItemCode"), new OperandValue(groupValue.ToString()), BinaryOperatorType.Equal)
                    , new BinaryOperator(new OperandProperty("DocNumber"), new OperandValue(groupValue2.ToString()), BinaryOperatorType.Equal)
                    , new BinaryOperator(new OperandProperty("ColorCode"), new OperandValue(groupValue3.ToString()), BinaryOperatorType.Equal));
        }

        private Graphics gr = Graphics.FromHwnd(IntPtr.Zero);
        private void xrPivotGrid2_CustomRowHeight(object sender, DevExpress.XtraReports.UI.PivotGrid.PivotCustomRowHeightEventArgs e)
        {
            e.RowHeight = 0;
            for (int i = 0; i <= e.ColumnCount - 1; i++)
            {
                var rowCellValue = e.GetRowCellValue(i);
                if (rowCellValue == null)
                    continue;
                string value = rowCellValue.ToString();
                SizeF size = gr.MeasureString(value, e.DataField.Appearance.Cell.Font, e.DataField.Width);
                int height = Convert.ToInt32(size.Height + 0.5);
                e.RowHeight = e.RowHeight > height ? e.RowHeight : height;
            }
        }
    }
}
