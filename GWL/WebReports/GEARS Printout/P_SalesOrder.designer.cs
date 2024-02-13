namespace GWL.WebReports.GEARS_Printout
{
    partial class P_SalesOrder
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DevExpress.XtraReports.UI.XRTable xrTable1;
            DevExpress.DataAccess.Sql.CustomSqlQuery customSqlQuery1 = new DevExpress.DataAccess.Sql.CustomSqlQuery();
            DevExpress.DataAccess.Sql.StoredProcQuery storedProcQuery1 = new DevExpress.DataAccess.Sql.StoredProcQuery();
            DevExpress.DataAccess.Sql.QueryParameter queryParameter1 = new DevExpress.DataAccess.Sql.QueryParameter();
            DevExpress.DataAccess.Sql.QueryParameter queryParameter2 = new DevExpress.DataAccess.Sql.QueryParameter();
            DevExpress.DataAccess.Sql.StoredProcQuery storedProcQuery2 = new DevExpress.DataAccess.Sql.StoredProcQuery();
            DevExpress.DataAccess.Sql.QueryParameter queryParameter3 = new DevExpress.DataAccess.Sql.QueryParameter();
            DevExpress.DataAccess.Sql.QueryParameter queryParameter4 = new DevExpress.DataAccess.Sql.QueryParameter();
            DevExpress.DataAccess.Sql.QueryParameter queryParameter5 = new DevExpress.DataAccess.Sql.QueryParameter();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(P_SalesOrder));
            this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.xrPageInfo2 = new DevExpress.XtraReports.UI.XRPageInfo();
            this.xrPageInfo1 = new DevExpress.XtraReports.UI.XRPageInfo();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.sqlDataSource1 = new DevExpress.DataAccess.Sql.SqlDataSource();
            this.DocNumber = new DevExpress.XtraReports.Parameters.Parameter();
            this.UserID = new DevExpress.XtraReports.Parameters.Parameter();
            this.GroupHeader1 = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.xrLabel3 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrPivotGrid1 = new DevExpress.XtraReports.UI.XRPivotGrid();
            this.fieldItemColorClass1 = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.fieldSizeCode1 = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.fieldQuantity1 = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.fieldUnit1 = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.fieldPrice1 = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.fieldAmount1 = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.fieldSizetype1 = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.fieldSortOrder1 = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel6 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel9 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel10 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel12 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel15 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel4 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel5 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel7 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel8 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel11 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel13 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel14 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel18 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel19 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel34 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel35 = new DevExpress.XtraReports.UI.XRLabel();
            this.GroupFooter1 = new DevExpress.XtraReports.UI.GroupFooterBand();
            this.xrLabel20 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel21 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel22 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel17 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel25 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel26 = new DevExpress.XtraReports.UI.XRLabel();
            this.IsPrinted = new DevExpress.XtraReports.Parameters.Parameter();
            xrTable1 = new DevExpress.XtraReports.UI.XRTable();
            ((System.ComponentModel.ISupportInitialize)(xrTable1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // xrTable1
            // 
            xrTable1.BackColor = System.Drawing.Color.White;
            xrTable1.Borders = DevExpress.XtraPrinting.BorderSide.None;
            xrTable1.BorderWidth = 1F;
            xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(12.00001F, 186.4167F);
            xrTable1.Name = "xrTable1";
            xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1});
            xrTable1.SizeF = new System.Drawing.SizeF(198.4929F, 63.67703F);
            xrTable1.StylePriority.UseBackColor = false;
            xrTable1.StylePriority.UseBorders = false;
            xrTable1.StylePriority.UseBorderWidth = false;
            xrTable1.StylePriority.UseTextAlignment = false;
            xrTable1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // xrTableRow1
            // 
            this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell1});
            this.xrTableRow1.Name = "xrTableRow1";
            this.xrTableRow1.Weight = 1D;
            // 
            // xrTableCell1
            // 
            this.xrTableCell1.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right)));
            this.xrTableCell1.CanGrow = false;
            this.xrTableCell1.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold);
            this.xrTableCell1.Name = "xrTableCell1";
            this.xrTableCell1.StylePriority.UseBorders = false;
            this.xrTableCell1.StylePriority.UseFont = false;
            this.xrTableCell1.StylePriority.UseTextAlignment = false;
            this.xrTableCell1.Text = "Item / Color / Class";
            this.xrTableCell1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell1.TextTrimming = System.Drawing.StringTrimming.None;
            this.xrTableCell1.Weight = 1D;
            this.xrTableCell1.WordWrap = false;
            // 
            // Detail
            // 
            this.Detail.HeightF = 0F;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.Detail.SortFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // TopMargin
            // 
            this.TopMargin.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPageInfo2,
            this.xrPageInfo1});
            this.TopMargin.HeightF = 53.54166F;
            this.TopMargin.Name = "TopMargin";
            this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrPageInfo2
            // 
            this.xrPageInfo2.Font = new System.Drawing.Font("Times New Roman", 8F);
            this.xrPageInfo2.Format = "Page {0} of {1}";
            this.xrPageInfo2.LocationFloat = new DevExpress.Utils.PointFloat(712.2916F, 21.79166F);
            this.xrPageInfo2.Name = "xrPageInfo2";
            this.xrPageInfo2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrPageInfo2.SizeF = new System.Drawing.SizeF(87.70837F, 16.75F);
            this.xrPageInfo2.StylePriority.UseFont = false;
            this.xrPageInfo2.StylePriority.UseTextAlignment = false;
            this.xrPageInfo2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            // 
            // xrPageInfo1
            // 
            this.xrPageInfo1.Font = new System.Drawing.Font("Times New Roman", 8F);
            this.xrPageInfo1.Format = "Run Date & Time: {0:MMMM dd,yyyy / hh:mm tt}";
            this.xrPageInfo1.LocationFloat = new DevExpress.Utils.PointFloat(9.999998F, 23.24999F);
            this.xrPageInfo1.Name = "xrPageInfo1";
            this.xrPageInfo1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrPageInfo1.PageInfo = DevExpress.XtraPrinting.PageInfo.DateTime;
            this.xrPageInfo1.SizeF = new System.Drawing.SizeF(320.4167F, 16.75F);
            this.xrPageInfo1.StylePriority.UseFont = false;
            // 
            // BottomMargin
            // 
            this.BottomMargin.HeightF = 40F;
            this.BottomMargin.Name = "BottomMargin";
            this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // sqlDataSource1
            // 
            this.sqlDataSource1.ConnectionName = "GEARS-METSITConnectionString";
            this.sqlDataSource1.Name = "sqlDataSource1";
            customSqlQuery1.Name = "CompanyName";
            customSqlQuery1.Sql = "SELECT Value FROM IT.SystemSettings WHERE Code = \'COMPNAME\'";
            storedProcQuery1.Name = "sp_printout_SODetail";
            queryParameter1.Name = "@DocNumber";
            queryParameter1.Type = typeof(DevExpress.DataAccess.Expression);
            queryParameter1.Value = new DevExpress.DataAccess.Expression("[Parameters.DocNumber]", typeof(string));
            queryParameter2.Name = "@UserID";
            queryParameter2.Type = typeof(DevExpress.DataAccess.Expression);
            queryParameter2.Value = new DevExpress.DataAccess.Expression("[Parameters.UserID]", typeof(string));
            storedProcQuery1.Parameters.Add(queryParameter1);
            storedProcQuery1.Parameters.Add(queryParameter2);
            storedProcQuery1.StoredProcName = "sp_printout_SalesOrderDetail";
            storedProcQuery2.Name = "sp_printout_SalesOrderHeader";
            queryParameter3.Name = "@DocNumber";
            queryParameter3.Type = typeof(DevExpress.DataAccess.Expression);
            queryParameter3.Value = new DevExpress.DataAccess.Expression("[Parameters.DocNumber]", typeof(string));
            queryParameter4.Name = "@UserID";
            queryParameter4.Type = typeof(DevExpress.DataAccess.Expression);
            queryParameter4.Value = new DevExpress.DataAccess.Expression("[Parameters.UserID]", typeof(string));
            queryParameter5.Name = "@IsPrinted";
            queryParameter5.Type = typeof(DevExpress.DataAccess.Expression);
            queryParameter5.Value = new DevExpress.DataAccess.Expression("[Parameters.IsPrinted]", typeof(bool));
            storedProcQuery2.Parameters.Add(queryParameter3);
            storedProcQuery2.Parameters.Add(queryParameter4);
            storedProcQuery2.Parameters.Add(queryParameter5);
            storedProcQuery2.StoredProcName = "sp_printout_SalesOrderHeader";
            this.sqlDataSource1.Queries.AddRange(new DevExpress.DataAccess.Sql.SqlQuery[] {
            customSqlQuery1,
            storedProcQuery1,
            storedProcQuery2});
            this.sqlDataSource1.ResultSchemaSerializable = resources.GetString("sqlDataSource1.ResultSchemaSerializable");
            // 
            // DocNumber
            // 
            this.DocNumber.Description = "DocNumber";
            this.DocNumber.Name = "DocNumber";
            // 
            // UserID
            // 
            this.UserID.Description = "UserID";
            this.UserID.Name = "UserID";
            this.UserID.ValueInfo = "UserID";
            // 
            // GroupHeader1
            // 
            this.GroupHeader1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel3,
            this.xrPivotGrid1,
            this.xrLabel2,
            this.xrLabel6,
            this.xrLabel9,
            this.xrLabel10,
            this.xrLabel12,
            this.xrLabel15,
            this.xrLabel1,
            this.xrLabel4,
            this.xrLabel5,
            this.xrLabel7,
            this.xrLabel8,
            this.xrLabel11,
            this.xrLabel13,
            this.xrLabel14,
            this.xrLabel18,
            this.xrLabel19,
            this.xrLabel34,
            this.xrLabel35,
            xrTable1});
            this.GroupHeader1.GroupUnion = DevExpress.XtraReports.UI.GroupUnion.WholePage;
            this.GroupHeader1.HeightF = 348.7076F;
            this.GroupHeader1.Name = "GroupHeader1";
            this.GroupHeader1.PageBreak = DevExpress.XtraReports.UI.PageBreak.BeforeBandExceptFirstEntry;
            this.GroupHeader1.Scripts.OnAfterPrint = "GroupHeader1_AfterPrint";
            // 
            // xrLabel3
            // 
            this.xrLabel3.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_printout_SalesOrderHeader.Printed")});
            this.xrLabel3.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel3.ForeColor = System.Drawing.Color.Red;
            this.xrLabel3.LocationFloat = new DevExpress.Utils.PointFloat(701.483F, 0F);
            this.xrLabel3.Name = "xrLabel3";
            this.xrLabel3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel3.SizeF = new System.Drawing.SizeF(98.51715F, 17F);
            this.xrLabel3.StylePriority.UseFont = false;
            this.xrLabel3.StylePriority.UseForeColor = false;
            this.xrLabel3.StylePriority.UseTextAlignment = false;
            this.xrLabel3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrPivotGrid1
            // 
            this.xrPivotGrid1.Appearance.Cell.BackColor = System.Drawing.Color.White;
            this.xrPivotGrid1.Appearance.Cell.BorderColor = System.Drawing.Color.Black;
            this.xrPivotGrid1.Appearance.Cell.Font = new System.Drawing.Font("Arial Narrow", 9F);
            this.xrPivotGrid1.Appearance.Cell.WordWrap = true;
            this.xrPivotGrid1.Appearance.CustomTotalCell.BackColor = System.Drawing.Color.White;
            this.xrPivotGrid1.Appearance.CustomTotalCell.BorderColor = System.Drawing.Color.Black;
            this.xrPivotGrid1.Appearance.CustomTotalCell.Font = new System.Drawing.Font("Arial Narrow", 9F, System.Drawing.FontStyle.Bold);
            this.xrPivotGrid1.Appearance.CustomTotalCell.WordWrap = true;
            this.xrPivotGrid1.Appearance.FieldHeader.BackColor = System.Drawing.Color.White;
            this.xrPivotGrid1.Appearance.FieldHeader.BorderColor = System.Drawing.Color.White;
            this.xrPivotGrid1.Appearance.FieldHeader.Font = new System.Drawing.Font("Arial Narrow", 9F, System.Drawing.FontStyle.Bold);
            this.xrPivotGrid1.Appearance.FieldHeader.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.xrPivotGrid1.Appearance.FieldHeader.TextVerticalAlignment = DevExpress.Utils.VertAlignment.Bottom;
            this.xrPivotGrid1.Appearance.FieldHeader.WordWrap = true;
            this.xrPivotGrid1.Appearance.FieldValue.BackColor = System.Drawing.Color.White;
            this.xrPivotGrid1.Appearance.FieldValue.BorderColor = System.Drawing.Color.Black;
            this.xrPivotGrid1.Appearance.FieldValue.Font = new System.Drawing.Font("Arial Narrow", 9F);
            this.xrPivotGrid1.Appearance.FieldValue.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.xrPivotGrid1.Appearance.FieldValue.TextVerticalAlignment = DevExpress.Utils.VertAlignment.Top;
            this.xrPivotGrid1.Appearance.FieldValue.WordWrap = true;
            this.xrPivotGrid1.Appearance.FieldValueGrandTotal.BackColor = System.Drawing.Color.White;
            this.xrPivotGrid1.Appearance.FieldValueGrandTotal.BorderColor = System.Drawing.Color.Black;
            this.xrPivotGrid1.Appearance.FieldValueGrandTotal.Font = new System.Drawing.Font("Arial Narrow", 9F, System.Drawing.FontStyle.Bold);
            this.xrPivotGrid1.Appearance.FieldValueGrandTotal.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.xrPivotGrid1.Appearance.FieldValueGrandTotal.TextVerticalAlignment = DevExpress.Utils.VertAlignment.Center;
            this.xrPivotGrid1.Appearance.FieldValueGrandTotal.WordWrap = true;
            this.xrPivotGrid1.Appearance.FieldValueTotal.BackColor = System.Drawing.Color.White;
            this.xrPivotGrid1.Appearance.FieldValueTotal.BorderColor = System.Drawing.Color.Black;
            this.xrPivotGrid1.Appearance.FieldValueTotal.Font = new System.Drawing.Font("Arial Narrow", 9F);
            this.xrPivotGrid1.Appearance.GrandTotalCell.Font = new System.Drawing.Font("Arial Narrow", 9F);
            this.xrPivotGrid1.Appearance.Lines.Font = new System.Drawing.Font("Arial Narrow", 9F);
            this.xrPivotGrid1.Appearance.TotalCell.Font = new System.Drawing.Font("Arial Narrow", 9F);
            this.xrPivotGrid1.DataMember = "sp_printout_SODetail";
            this.xrPivotGrid1.DataSource = this.sqlDataSource1;
            this.xrPivotGrid1.Fields.AddRange(new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField[] {
            this.fieldItemColorClass1,
            this.fieldSizeCode1,
            this.fieldQuantity1,
            this.fieldUnit1,
            this.fieldPrice1,
            this.fieldAmount1,
            this.fieldSizetype1,
            this.fieldSortOrder1});
            this.xrPivotGrid1.LocationFloat = new DevExpress.Utils.PointFloat(12.00001F, 186.8333F);
            this.xrPivotGrid1.Name = "xrPivotGrid1";
            this.xrPivotGrid1.OptionsPrint.FilterSeparatorBarPadding = 3;
            this.xrPivotGrid1.OptionsPrint.MergeRowFieldValues = false;
            this.xrPivotGrid1.OptionsPrint.PrintColumnHeaders = DevExpress.Utils.DefaultBoolean.False;
            this.xrPivotGrid1.OptionsPrint.PrintDataHeaders = DevExpress.Utils.DefaultBoolean.False;
            this.xrPivotGrid1.OptionsPrint.PrintFilterHeaders = DevExpress.Utils.DefaultBoolean.False;
            this.xrPivotGrid1.OptionsPrint.PrintRowHeaders = DevExpress.Utils.DefaultBoolean.False;
            this.xrPivotGrid1.OptionsView.ShowColumnGrandTotalHeader = false;
            this.xrPivotGrid1.OptionsView.ShowColumnHeaders = false;
            this.xrPivotGrid1.OptionsView.ShowCustomTotalsForSingleValues = true;
            this.xrPivotGrid1.OptionsView.ShowDataHeaders = false;
            this.xrPivotGrid1.OptionsView.ShowFilterHeaders = false;
            this.xrPivotGrid1.OptionsView.ShowGrandTotalsForSingleValues = true;
            this.xrPivotGrid1.OptionsView.ShowRowGrandTotalHeader = false;
            this.xrPivotGrid1.OptionsView.ShowRowGrandTotals = false;
            this.xrPivotGrid1.OptionsView.ShowRowHeaders = false;
            this.xrPivotGrid1.OptionsView.ShowTotalsForSingleValues = true;
            this.xrPivotGrid1.Scripts.OnFieldValueDisplayText = "xrPivotGrid1_FieldValueDisplayText";
            this.xrPivotGrid1.SizeF = new System.Drawing.SizeF(474.9033F, 100.4166F);
            // 
            // fieldItemColorClass1
            // 
            this.fieldItemColorClass1.Appearance.Cell.Font = new System.Drawing.Font("Times New Roman", 9F);
            this.fieldItemColorClass1.Appearance.FieldValue.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.fieldItemColorClass1.Appearance.FieldValue.TextVerticalAlignment = DevExpress.Utils.VertAlignment.Center;
            this.fieldItemColorClass1.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.fieldItemColorClass1.AreaIndex = 0;
            this.fieldItemColorClass1.Caption = "Item / Color / Class";
            this.fieldItemColorClass1.FieldName = "Item / Color / Class";
            this.fieldItemColorClass1.Name = "fieldItemColorClass1";
            this.fieldItemColorClass1.Width = 190;
            // 
            // fieldSizeCode1
            // 
            this.fieldSizeCode1.Appearance.Cell.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold);
            this.fieldSizeCode1.Appearance.CustomTotalCell.Font = new System.Drawing.Font("Arial Narrow", 9.75F);
            this.fieldSizeCode1.Appearance.FieldHeader.Font = new System.Drawing.Font("Arial Narrow", 9.75F);
            this.fieldSizeCode1.Appearance.FieldValue.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold);
            this.fieldSizeCode1.Appearance.FieldValueGrandTotal.Font = new System.Drawing.Font("Arial Narrow", 9.75F);
            this.fieldSizeCode1.Appearance.FieldValueTotal.Font = new System.Drawing.Font("Arial Narrow", 9.75F);
            this.fieldSizeCode1.Appearance.GrandTotalCell.Font = new System.Drawing.Font("Arial Narrow", 9F, System.Drawing.FontStyle.Bold);
            this.fieldSizeCode1.Appearance.TotalCell.Font = new System.Drawing.Font("Arial Narrow", 9F, System.Drawing.FontStyle.Bold);
            this.fieldSizeCode1.Area = DevExpress.XtraPivotGrid.PivotArea.ColumnArea;
            this.fieldSizeCode1.AreaIndex = 1;
            this.fieldSizeCode1.Caption = "Size Code";
            this.fieldSizeCode1.FieldName = "SizeCode";
            this.fieldSizeCode1.Name = "fieldSizeCode1";
            this.fieldSizeCode1.SummaryType = DevExpress.Data.PivotGrid.PivotSummaryType.Max;
            this.fieldSizeCode1.Width = 64;
            // 
            // fieldQuantity1
            // 
            this.fieldQuantity1.Appearance.Cell.Font = new System.Drawing.Font("Arial Narrow", 11F);
            this.fieldQuantity1.Appearance.GrandTotalCell.Font = new System.Drawing.Font("Arial Narrow", 11.25F, System.Drawing.FontStyle.Bold);
            this.fieldQuantity1.Appearance.TotalCell.Font = new System.Drawing.Font("Arial Narrow", 11F);
            this.fieldQuantity1.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea;
            this.fieldQuantity1.AreaIndex = 2;
            this.fieldQuantity1.Caption = "Total Qty";
            this.fieldQuantity1.CellFormat.FormatString = "n";
            this.fieldQuantity1.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.fieldQuantity1.FieldName = "Quantity";
            this.fieldQuantity1.GrandTotalCellFormat.FormatString = "n";
            this.fieldQuantity1.GrandTotalCellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.fieldQuantity1.Name = "fieldQuantity1";
            this.fieldQuantity1.TotalCellFormat.FormatString = "n";
            this.fieldQuantity1.TotalCellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.fieldQuantity1.TotalValueFormat.FormatString = "n";
            this.fieldQuantity1.TotalValueFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.fieldQuantity1.ValueFormat.FormatString = "n";
            this.fieldQuantity1.ValueFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.fieldQuantity1.Width = 62;
            // 
            // fieldUnit1
            // 
            this.fieldUnit1.Appearance.Cell.Font = new System.Drawing.Font("Arial Narrow", 9F);
            this.fieldUnit1.Appearance.Cell.TextVerticalAlignment = DevExpress.Utils.VertAlignment.Center;
            this.fieldUnit1.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea;
            this.fieldUnit1.AreaIndex = 0;
            this.fieldUnit1.Caption = "Unit";
            this.fieldUnit1.FieldName = "Unit";
            this.fieldUnit1.Name = "fieldUnit1";
            this.fieldUnit1.Options.ShowValues = false;
            this.fieldUnit1.SummaryType = DevExpress.Data.PivotGrid.PivotSummaryType.Max;
            this.fieldUnit1.Width = 43;
            // 
            // fieldPrice1
            // 
            this.fieldPrice1.Appearance.GrandTotalCell.Font = new System.Drawing.Font("Arial Narrow", 11F);
            this.fieldPrice1.Appearance.TotalCell.Font = new System.Drawing.Font("Arial Narrow", 11F);
            this.fieldPrice1.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea;
            this.fieldPrice1.AreaIndex = 1;
            this.fieldPrice1.Caption = "Price";
            this.fieldPrice1.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.fieldPrice1.FieldName = "Price";
            this.fieldPrice1.Name = "fieldPrice1";
            this.fieldPrice1.Options.ShowValues = false;
            this.fieldPrice1.SummaryType = DevExpress.Data.PivotGrid.PivotSummaryType.Max;
            this.fieldPrice1.Width = 54;
            // 
            // fieldAmount1
            // 
            this.fieldAmount1.Appearance.GrandTotalCell.Font = new System.Drawing.Font("Arial Narrow", 11F);
            this.fieldAmount1.Appearance.TotalCell.Font = new System.Drawing.Font("Arial Narrow", 11F);
            this.fieldAmount1.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea;
            this.fieldAmount1.AreaIndex = 3;
            this.fieldAmount1.Caption = "Amount";
            this.fieldAmount1.CellFormat.FormatString = "n";
            this.fieldAmount1.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.fieldAmount1.FieldName = "Amount";
            this.fieldAmount1.GrandTotalCellFormat.FormatString = "n";
            this.fieldAmount1.GrandTotalCellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.fieldAmount1.Name = "fieldAmount1";
            this.fieldAmount1.Options.ShowValues = false;
            this.fieldAmount1.TotalCellFormat.FormatString = "n";
            this.fieldAmount1.TotalCellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.fieldAmount1.TotalValueFormat.FormatString = "n";
            this.fieldAmount1.TotalValueFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.fieldAmount1.ValueFormat.FormatString = "n";
            this.fieldAmount1.ValueFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.fieldAmount1.Width = 67;
            // 
            // fieldSizetype1
            // 
            this.fieldSizetype1.AreaIndex = 0;
            this.fieldSizetype1.Caption = "Sizetype";
            this.fieldSizetype1.FieldName = "Sizetype";
            this.fieldSizetype1.Name = "fieldSizetype1";
            // 
            // fieldSortOrder1
            // 
            this.fieldSortOrder1.Appearance.Cell.BackColor = System.Drawing.Color.White;
            this.fieldSortOrder1.Appearance.Cell.BorderColor = System.Drawing.Color.White;
            this.fieldSortOrder1.Appearance.Cell.ForeColor = System.Drawing.Color.White;
            this.fieldSortOrder1.Appearance.FieldValue.ForeColor = System.Drawing.Color.Transparent;
            this.fieldSortOrder1.Area = DevExpress.XtraPivotGrid.PivotArea.ColumnArea;
            this.fieldSortOrder1.AreaIndex = 0;
            this.fieldSortOrder1.Caption = "Sort Order";
            this.fieldSortOrder1.FieldName = "SortOrder";
            this.fieldSortOrder1.Name = "fieldSortOrder1";
            this.fieldSortOrder1.Options.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.fieldSortOrder1.Options.AllowSortBySummary = DevExpress.Utils.DefaultBoolean.False;
            this.fieldSortOrder1.Options.ShowGrandTotal = false;
            this.fieldSortOrder1.Options.ShowTotals = false;
            this.fieldSortOrder1.Options.ShowValues = false;
            this.fieldSortOrder1.SummaryType = DevExpress.Data.PivotGrid.PivotSummaryType.Max;
            this.fieldSortOrder1.TotalsVisibility = DevExpress.XtraPivotGrid.PivotTotalsVisibility.None;
            // 
            // xrLabel2
            // 
            this.xrLabel2.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_printout_SalesOrderHeader.DocDate", "{0:MM/dd/yyyy}")});
            this.xrLabel2.Font = new System.Drawing.Font("Arial Narrow", 9F);
            this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(543.834F, 85.20802F);
            this.xrLabel2.Name = "xrLabel2";
            this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel2.SizeF = new System.Drawing.SizeF(147.571F, 21.95831F);
            this.xrLabel2.StylePriority.UseFont = false;
            this.xrLabel2.StylePriority.UseTextAlignment = false;
            this.xrLabel2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel6
            // 
            this.xrLabel6.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_printout_SalesOrderHeader.Terms")});
            this.xrLabel6.Font = new System.Drawing.Font("Arial Narrow", 9F);
            this.xrLabel6.LocationFloat = new DevExpress.Utils.PointFloat(543.834F, 107.2493F);
            this.xrLabel6.Name = "xrLabel6";
            this.xrLabel6.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel6.SizeF = new System.Drawing.SizeF(147.5712F, 21.95831F);
            this.xrLabel6.StylePriority.UseFont = false;
            this.xrLabel6.StylePriority.UseTextAlignment = false;
            this.xrLabel6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel9
            // 
            this.xrLabel9.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_printout_SalesOrderHeader.TotalQty", "{0:n}")});
            this.xrLabel9.Font = new System.Drawing.Font("Arial Narrow", 9F);
            this.xrLabel9.LocationFloat = new DevExpress.Utils.PointFloat(543.834F, 129.2076F);
            this.xrLabel9.Name = "xrLabel9";
            this.xrLabel9.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel9.SizeF = new System.Drawing.SizeF(147.571F, 21.95833F);
            this.xrLabel9.StylePriority.UseFont = false;
            this.xrLabel9.StylePriority.UseTextAlignment = false;
            this.xrLabel9.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel10
            // 
            this.xrLabel10.Font = new System.Drawing.Font("Arial Narrow", 9F, System.Drawing.FontStyle.Bold);
            this.xrLabel10.LocationFloat = new DevExpress.Utils.PointFloat(59.6875F, 151.0414F);
            this.xrLabel10.Name = "xrLabel10";
            this.xrLabel10.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel10.SizeF = new System.Drawing.SizeF(110.5538F, 21.95833F);
            this.xrLabel10.StylePriority.UseFont = false;
            this.xrLabel10.StylePriority.UseTextAlignment = false;
            this.xrLabel10.Text = "Status:";
            this.xrLabel10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel12
            // 
            this.xrLabel12.Font = new System.Drawing.Font("Arial Narrow", 9F, System.Drawing.FontStyle.Bold);
            this.xrLabel12.LocationFloat = new DevExpress.Utils.PointFloat(59.6875F, 129.0831F);
            this.xrLabel12.Name = "xrLabel12";
            this.xrLabel12.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel12.SizeF = new System.Drawing.SizeF(110.5538F, 21.95831F);
            this.xrLabel12.StylePriority.UseFont = false;
            this.xrLabel12.StylePriority.UseTextAlignment = false;
            this.xrLabel12.Text = "Customer PO:";
            this.xrLabel12.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel15
            // 
            this.xrLabel15.Font = new System.Drawing.Font("Arial Narrow", 9F, System.Drawing.FontStyle.Bold);
            this.xrLabel15.LocationFloat = new DevExpress.Utils.PointFloat(458.2802F, 129.0831F);
            this.xrLabel15.Name = "xrLabel15";
            this.xrLabel15.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel15.SizeF = new System.Drawing.SizeF(85.55374F, 21.95834F);
            this.xrLabel15.StylePriority.UseFont = false;
            this.xrLabel15.StylePriority.UseTextAlignment = false;
            this.xrLabel15.Text = "Total Quantity:";
            this.xrLabel15.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel1
            // 
            this.xrLabel1.Font = new System.Drawing.Font("Arial Narrow", 9F, System.Drawing.FontStyle.Bold);
            this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(458.2802F, 151.0414F);
            this.xrLabel1.Name = "xrLabel1";
            this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel1.SizeF = new System.Drawing.SizeF(85.55374F, 21.95834F);
            this.xrLabel1.StylePriority.UseFont = false;
            this.xrLabel1.StylePriority.UseTextAlignment = false;
            this.xrLabel1.Text = "Total Amount:";
            this.xrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel4
            // 
            this.xrLabel4.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_printout_SalesOrderHeader.TotalAmount", "{0:n}")});
            this.xrLabel4.Font = new System.Drawing.Font("Arial Narrow", 9F);
            this.xrLabel4.LocationFloat = new DevExpress.Utils.PointFloat(543.834F, 151.1659F);
            this.xrLabel4.Name = "xrLabel4";
            this.xrLabel4.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel4.SizeF = new System.Drawing.SizeF(147.5714F, 21.95833F);
            this.xrLabel4.StylePriority.UseFont = false;
            this.xrLabel4.StylePriority.UseTextAlignment = false;
            this.xrLabel4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel5
            // 
            this.xrLabel5.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_printout_SalesOrderHeader.CustomerPO")});
            this.xrLabel5.Font = new System.Drawing.Font("Arial Narrow", 9F);
            this.xrLabel5.LocationFloat = new DevExpress.Utils.PointFloat(170.2413F, 129.0416F);
            this.xrLabel5.Name = "xrLabel5";
            this.xrLabel5.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel5.SizeF = new System.Drawing.SizeF(190.2083F, 21.95831F);
            this.xrLabel5.StylePriority.UseFont = false;
            this.xrLabel5.StylePriority.UseTextAlignment = false;
            this.xrLabel5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel7
            // 
            this.xrLabel7.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_printout_SalesOrderHeader.Status")});
            this.xrLabel7.Font = new System.Drawing.Font("Arial Narrow", 9F);
            this.xrLabel7.LocationFloat = new DevExpress.Utils.PointFloat(170.2413F, 151.0414F);
            this.xrLabel7.Name = "xrLabel7";
            this.xrLabel7.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel7.SizeF = new System.Drawing.SizeF(190.2083F, 21.95831F);
            this.xrLabel7.StylePriority.UseFont = false;
            this.xrLabel7.StylePriority.UseTextAlignment = false;
            this.xrLabel7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel8
            // 
            this.xrLabel8.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_printout_SalesOrderHeader.Customer", "{0:MM/dd/yy}")});
            this.xrLabel8.Font = new System.Drawing.Font("Arial Narrow", 9F);
            this.xrLabel8.LocationFloat = new DevExpress.Utils.PointFloat(170.2413F, 107.0833F);
            this.xrLabel8.Name = "xrLabel8";
            this.xrLabel8.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel8.SizeF = new System.Drawing.SizeF(190.2083F, 21.95831F);
            this.xrLabel8.StylePriority.UseFont = false;
            this.xrLabel8.StylePriority.UseTextAlignment = false;
            this.xrLabel8.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel11
            // 
            this.xrLabel11.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_printout_SalesOrderHeader.DocNumber")});
            this.xrLabel11.Font = new System.Drawing.Font("Arial Narrow", 10F);
            this.xrLabel11.LocationFloat = new DevExpress.Utils.PointFloat(170.2413F, 85.12502F);
            this.xrLabel11.Name = "xrLabel11";
            this.xrLabel11.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel11.SizeF = new System.Drawing.SizeF(190.2083F, 21.95832F);
            this.xrLabel11.StylePriority.UseFont = false;
            this.xrLabel11.StylePriority.UseTextAlignment = false;
            this.xrLabel11.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel13
            // 
            this.xrLabel13.Font = new System.Drawing.Font("Arial Narrow", 9F, System.Drawing.FontStyle.Bold);
            this.xrLabel13.LocationFloat = new DevExpress.Utils.PointFloat(458.2802F, 107.0833F);
            this.xrLabel13.Name = "xrLabel13";
            this.xrLabel13.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel13.SizeF = new System.Drawing.SizeF(85.5538F, 21.95834F);
            this.xrLabel13.StylePriority.UseFont = false;
            this.xrLabel13.StylePriority.UseTextAlignment = false;
            this.xrLabel13.Text = "Terms:";
            this.xrLabel13.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel14
            // 
            this.xrLabel14.Font = new System.Drawing.Font("Arial Narrow", 9F, System.Drawing.FontStyle.Bold);
            this.xrLabel14.LocationFloat = new DevExpress.Utils.PointFloat(59.6875F, 107.0833F);
            this.xrLabel14.Name = "xrLabel14";
            this.xrLabel14.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel14.SizeF = new System.Drawing.SizeF(110.5538F, 21.95833F);
            this.xrLabel14.StylePriority.UseFont = false;
            this.xrLabel14.StylePriority.UseTextAlignment = false;
            this.xrLabel14.Text = "Customer:";
            this.xrLabel14.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel18
            // 
            this.xrLabel18.Font = new System.Drawing.Font("Arial Narrow", 9F, System.Drawing.FontStyle.Bold);
            this.xrLabel18.LocationFloat = new DevExpress.Utils.PointFloat(458.2802F, 85.12502F);
            this.xrLabel18.Name = "xrLabel18";
            this.xrLabel18.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel18.SizeF = new System.Drawing.SizeF(85.5538F, 21.95832F);
            this.xrLabel18.StylePriority.UseFont = false;
            this.xrLabel18.StylePriority.UseTextAlignment = false;
            this.xrLabel18.Text = "Document Date:";
            this.xrLabel18.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel19
            // 
            this.xrLabel19.Font = new System.Drawing.Font("Arial Narrow", 9F, System.Drawing.FontStyle.Bold);
            this.xrLabel19.LocationFloat = new DevExpress.Utils.PointFloat(59.6875F, 85.12502F);
            this.xrLabel19.Name = "xrLabel19";
            this.xrLabel19.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel19.SizeF = new System.Drawing.SizeF(110.5538F, 21.95833F);
            this.xrLabel19.StylePriority.UseFont = false;
            this.xrLabel19.StylePriority.UseTextAlignment = false;
            this.xrLabel19.Text = "Document Number:";
            this.xrLabel19.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel34
            // 
            this.xrLabel34.Font = new System.Drawing.Font("Arial Narrow", 13F);
            this.xrLabel34.LocationFloat = new DevExpress.Utils.PointFloat(11.50013F, 43.125F);
            this.xrLabel34.Name = "xrLabel34";
            this.xrLabel34.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel34.SizeF = new System.Drawing.SizeF(788.5F, 23.00002F);
            this.xrLabel34.StylePriority.UseFont = false;
            this.xrLabel34.StylePriority.UseTextAlignment = false;
            this.xrLabel34.Text = "Sales Order";
            this.xrLabel34.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel35
            // 
            this.xrLabel35.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "CompanyName.Value", "{0}")});
            this.xrLabel35.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold);
            this.xrLabel35.LocationFloat = new DevExpress.Utils.PointFloat(11.50002F, 17F);
            this.xrLabel35.Name = "xrLabel35";
            this.xrLabel35.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel35.SizeF = new System.Drawing.SizeF(788.5001F, 26.12499F);
            this.xrLabel35.StylePriority.UseFont = false;
            this.xrLabel35.StylePriority.UseTextAlignment = false;
            this.xrLabel35.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // GroupFooter1
            // 
            this.GroupFooter1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel20,
            this.xrLabel21,
            this.xrLabel22,
            this.xrLabel17,
            this.xrLabel25,
            this.xrLabel26});
            this.GroupFooter1.Font = new System.Drawing.Font("Arial Narrow", 9F);
            this.GroupFooter1.HeightF = 228.125F;
            this.GroupFooter1.Name = "GroupFooter1";
            this.GroupFooter1.StylePriority.UseFont = false;
            // 
            // xrLabel20
            // 
            this.xrLabel20.Font = new System.Drawing.Font("Arial Narrow", 9F, System.Drawing.FontStyle.Bold);
            this.xrLabel20.LocationFloat = new DevExpress.Utils.PointFloat(440.1554F, 88.04166F);
            this.xrLabel20.Multiline = true;
            this.xrLabel20.Name = "xrLabel20";
            this.xrLabel20.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel20.SizeF = new System.Drawing.SizeF(198.3011F, 23F);
            this.xrLabel20.StylePriority.UseFont = false;
            this.xrLabel20.Text = "Acknowledge By:";
            // 
            // xrLabel21
            // 
            this.xrLabel21.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrLabel21.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_printout_SalesOrderHeader.ApprovedBy")});
            this.xrLabel21.Font = new System.Drawing.Font("Arial Narrow", 9F);
            this.xrLabel21.LocationFloat = new DevExpress.Utils.PointFloat(440.1554F, 111.0417F);
            this.xrLabel21.Multiline = true;
            this.xrLabel21.Name = "xrLabel21";
            this.xrLabel21.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel21.SizeF = new System.Drawing.SizeF(198.3011F, 23F);
            this.xrLabel21.StylePriority.UseBorders = false;
            this.xrLabel21.StylePriority.UseFont = false;
            this.xrLabel21.StylePriority.UseTextAlignment = false;
            this.xrLabel21.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // xrLabel22
            // 
            this.xrLabel22.Font = new System.Drawing.Font("Arial Narrow", 8F, System.Drawing.FontStyle.Bold);
            this.xrLabel22.LocationFloat = new DevExpress.Utils.PointFloat(440.1554F, 134.0417F);
            this.xrLabel22.Name = "xrLabel22";
            this.xrLabel22.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel22.SizeF = new System.Drawing.SizeF(198.3011F, 23F);
            this.xrLabel22.StylePriority.UseFont = false;
            this.xrLabel22.StylePriority.UseTextAlignment = false;
            this.xrLabel22.Text = "(Printed Name and Signature)";
            this.xrLabel22.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // xrLabel17
            // 
            this.xrLabel17.Font = new System.Drawing.Font("Arial Narrow", 8F, System.Drawing.FontStyle.Bold);
            this.xrLabel17.LocationFloat = new DevExpress.Utils.PointFloat(37.01831F, 134.0417F);
            this.xrLabel17.Name = "xrLabel17";
            this.xrLabel17.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel17.SizeF = new System.Drawing.SizeF(186.8428F, 23F);
            this.xrLabel17.StylePriority.UseFont = false;
            this.xrLabel17.StylePriority.UseTextAlignment = false;
            this.xrLabel17.Text = "(Printed Name and Signature)";
            this.xrLabel17.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // xrLabel25
            // 
            this.xrLabel25.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrLabel25.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_printout_SalesOrderHeader.PreparedBy")});
            this.xrLabel25.Font = new System.Drawing.Font("Arial Narrow", 9F);
            this.xrLabel25.LocationFloat = new DevExpress.Utils.PointFloat(37.01831F, 111.0417F);
            this.xrLabel25.Multiline = true;
            this.xrLabel25.Name = "xrLabel25";
            this.xrLabel25.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel25.SizeF = new System.Drawing.SizeF(186.8428F, 23F);
            this.xrLabel25.StylePriority.UseBorders = false;
            this.xrLabel25.StylePriority.UseFont = false;
            this.xrLabel25.StylePriority.UseTextAlignment = false;
            this.xrLabel25.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // xrLabel26
            // 
            this.xrLabel26.Font = new System.Drawing.Font("Arial Narrow", 9F, System.Drawing.FontStyle.Bold);
            this.xrLabel26.LocationFloat = new DevExpress.Utils.PointFloat(37.01831F, 88.04166F);
            this.xrLabel26.Multiline = true;
            this.xrLabel26.Name = "xrLabel26";
            this.xrLabel26.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel26.SizeF = new System.Drawing.SizeF(186.8428F, 23F);
            this.xrLabel26.StylePriority.UseFont = false;
            this.xrLabel26.Text = "Prepared By:\r\n";
            // 
            // IsPrinted
            // 
            this.IsPrinted.Name = "IsPrinted";
            this.IsPrinted.Type = typeof(bool);
            this.IsPrinted.ValueInfo = "False";
            this.IsPrinted.Visible = false;
            // 
            // P_SalesOrder
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.GroupHeader1,
            this.GroupFooter1});
            this.ComponentStorage.AddRange(new System.ComponentModel.IComponent[] {
            this.sqlDataSource1});
            this.DataMember = "sp_printout_SalesOrderHeader";
            this.DataSource = this.sqlDataSource1;
            this.FilterString = "[DocNumber] = ?DocNumber";
            this.Margins = new System.Drawing.Printing.Margins(20, 20, 54, 40);
            this.Parameters.AddRange(new DevExpress.XtraReports.Parameters.Parameter[] {
            this.DocNumber,
            this.UserID,
            this.IsPrinted});
            this.RequestParameters = false;
            this.Scripts.OnAfterPrint = "P_SalesOrder_AfterPrint";
            this.ScriptsSource = resources.GetString("$this.ScriptsSource");
            this.Version = "15.1";
            ((System.ComponentModel.ISupportInitialize)(xrTable1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private DevExpress.DataAccess.Sql.SqlDataSource sqlDataSource1;
        private DevExpress.XtraReports.Parameters.Parameter DocNumber;
        private DevExpress.XtraReports.Parameters.Parameter UserID;
        private DevExpress.XtraReports.UI.GroupHeaderBand GroupHeader1;
        private DevExpress.XtraReports.UI.XRPageInfo xrPageInfo2;
        private DevExpress.XtraReports.UI.XRPageInfo xrPageInfo1;
        private DevExpress.XtraReports.UI.GroupFooterBand GroupFooter1;
        private DevExpress.XtraReports.UI.XRLabel xrLabel34;
        private DevExpress.XtraReports.UI.XRLabel xrLabel35;
        private DevExpress.XtraReports.UI.XRLabel xrLabel2;
        private DevExpress.XtraReports.UI.XRLabel xrLabel6;
        private DevExpress.XtraReports.UI.XRLabel xrLabel9;
        private DevExpress.XtraReports.UI.XRLabel xrLabel10;
        private DevExpress.XtraReports.UI.XRLabel xrLabel12;
        private DevExpress.XtraReports.UI.XRLabel xrLabel15;
        private DevExpress.XtraReports.UI.XRLabel xrLabel1;
        private DevExpress.XtraReports.UI.XRLabel xrLabel4;
        private DevExpress.XtraReports.UI.XRLabel xrLabel5;
        private DevExpress.XtraReports.UI.XRLabel xrLabel7;
        private DevExpress.XtraReports.UI.XRLabel xrLabel8;
        private DevExpress.XtraReports.UI.XRLabel xrLabel11;
        private DevExpress.XtraReports.UI.XRLabel xrLabel13;
        private DevExpress.XtraReports.UI.XRLabel xrLabel14;
        private DevExpress.XtraReports.UI.XRLabel xrLabel18;
        private DevExpress.XtraReports.UI.XRLabel xrLabel19;
        private DevExpress.XtraReports.UI.XRLabel xrLabel17;
        private DevExpress.XtraReports.UI.XRLabel xrLabel25;
        private DevExpress.XtraReports.UI.XRLabel xrLabel26;
        private DevExpress.XtraReports.UI.XRLabel xrLabel20;
        private DevExpress.XtraReports.UI.XRLabel xrLabel21;
        private DevExpress.XtraReports.UI.XRLabel xrLabel22;
        private DevExpress.XtraReports.UI.XRPivotGrid xrPivotGrid1;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow1;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell1;
        private DevExpress.XtraReports.UI.XRLabel xrLabel3;
        private DevExpress.XtraReports.Parameters.Parameter IsPrinted;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField fieldItemColorClass1;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField fieldSizeCode1;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField fieldQuantity1;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField fieldUnit1;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField fieldPrice1;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField fieldAmount1;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField fieldSizetype1;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField fieldSortOrder1;
    }
}
