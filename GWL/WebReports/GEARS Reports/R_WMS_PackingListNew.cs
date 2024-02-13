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
    public partial class R_WMS_PackingListNew : DevExpress.XtraReports.UI.XtraReport
    {
        public R_WMS_PackingListNew()
        {
            InitializeComponent();
            if (HttpContext.Current.Session["ConnString"] != null)
                sqlDataSource1.Connection.ConnectionString = HttpContext.Current.Session["ConnString"].ToString();
        }

        private void xrPivotGrid1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            XRPivotGrid grid = (XRPivotGrid)sender;
            var groupValue = GetCurrentColumnValue("ItemCode");
            var groupValue2 = GetCurrentColumnValue("DRNo");
            var groupValue3 = GetCurrentColumnValue("ColorCode");
            CriteriaOperator groupbyitem = new BinaryOperator(new OperandProperty("ItemCode"), new OperandValue(groupValue.ToString()), BinaryOperatorType.Equal);
            CriteriaOperator groupbydocnumber = new BinaryOperator(new OperandProperty("DRNo"), new OperandValue(groupValue2.ToString()), BinaryOperatorType.Equal);
            //grid.Prefilter.CriteriaString = string.Format("[ItemCode] == {0}", groupValue);
            //grid.Prefilter.Criteria = CriteriaOperator.Parse("[ItemCode] == " + groupValue.ToString());

            //grid.Prefilter.Criteria = new BinaryOperator(new OperandProperty("ItemCode"), new OperandValue(groupValue.ToString()), BinaryOperatorType.Equal);
            //grid.Prefilter.Criteria = new BinaryOperator(new OperandProperty("DRNo"), new OperandValue(groupValue2.ToString()), BinaryOperatorType.Equal);
            
            //grid.Prefilter.Criteria = new BinaryOperator(new OperandProperty("ColorCode"), new OperandValue(groupValue3.ToString()), BinaryOperatorType.Equal);
            //grid.Prefilter.Criteria = (GroupOperator.Combine(GroupOperatorType.And, groupbydocnumber, groupbyitem)).ToString();
            grid.Prefilter.Criteria = GroupOperator.And(new BinaryOperator(new OperandProperty("DRNo"), new OperandValue(groupValue2.ToString()), BinaryOperatorType.Equal)
                    , new BinaryOperator(new OperandProperty("ItemCode"), new OperandValue(groupValue.ToString()), BinaryOperatorType.Equal)
                    , new BinaryOperator(new OperandProperty("ColorCode"), new OperandValue(groupValue3.ToString()), BinaryOperatorType.Equal));

            //Masterfileitemdetail.FilterExpression = (GroupOperator.Combine(GroupOperatorType.And, selectionCriteria)).ToString();
            //CriteriaOperator suppcrit = new BinaryOperator("SupplierCode","%"+glSupplierCode.Text+"%",BinaryOperatorType.Like);
            //sdsPicklistDetail.FilterExpression = (GroupOperator.Combine(GroupOperatorType.And, selectionCriteria, suppcrit)).ToString();
        }
    }
}
