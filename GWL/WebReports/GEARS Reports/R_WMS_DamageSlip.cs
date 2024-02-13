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
    public partial class R_WMS_DamageSlip : DevExpress.XtraReports.UI.XtraReport
    {
        public R_WMS_DamageSlip()
        {
            InitializeComponent();
            if (HttpContext.Current.Session["ConnString"] != null)
                sqlDataSource1.Connection.ConnectionString = HttpContext.Current.Session["ConnString"].ToString();
        }

        private void xrPivotGrid1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRPivotGrid grid = (XRPivotGrid)sender;
            var groupValue = GetCurrentColumnValue("GenderCode");
            var groupValue2 = GetCurrentColumnValue("ProductCategoryCode");
            //CriteriaOperator groupbyitem = new BinaryOperator(new OperandProperty("ItemCode"), new OperandValue(groupValue.ToString()), BinaryOperatorType.Equal);
            //CriteriaOperator groupbydocnumber = new BinaryOperator(new OperandProperty("DRNo"), new OperandValue(groupValue2.ToString()), BinaryOperatorType.Equal);
            grid.Prefilter.Criteria = GroupOperator.And(new BinaryOperator(new OperandProperty("GenderCode"), new OperandValue(groupValue.ToString()), BinaryOperatorType.Equal)
                    , new BinaryOperator(new OperandProperty("ProductCategoryCode"), new OperandValue(groupValue2.ToString()), BinaryOperatorType.Equal));

        }


    }
}
