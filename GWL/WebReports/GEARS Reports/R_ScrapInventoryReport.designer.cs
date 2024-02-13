namespace GWL.WebReports.GEARS_Reports
{
    partial class R_ScrapInventoryReport

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
            this.components = new System.ComponentModel.Container();
            DevExpress.DataAccess.Sql.StoredProcQuery storedProcQuery1 = new DevExpress.DataAccess.Sql.StoredProcQuery();
            DevExpress.DataAccess.Sql.QueryParameter queryParameter1 = new DevExpress.DataAccess.Sql.QueryParameter();
            DevExpress.DataAccess.Sql.QueryParameter queryParameter2 = new DevExpress.DataAccess.Sql.QueryParameter();
            DevExpress.DataAccess.Sql.CustomSqlQuery customSqlQuery1 = new DevExpress.DataAccess.Sql.CustomSqlQuery();
            DevExpress.DataAccess.Sql.CustomSqlQuery customSqlQuery2 = new DevExpress.DataAccess.Sql.CustomSqlQuery();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(R_ScrapInventoryReport));
            DevExpress.XtraReports.UI.XRSummary xrSummary1 = new DevExpress.XtraReports.UI.XRSummary();
            this.sqlDataSource1 = new DevExpress.DataAccess.Sql.SqlDataSource(this.components);
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.xrLabel3 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrPageInfo1 = new DevExpress.XtraReports.UI.XRPageInfo();
            this.xrPageInfo2 = new DevExpress.XtraReports.UI.XRPageInfo();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.DateTo = new DevExpress.XtraReports.Parameters.Parameter();
            this.DateFrom = new DevExpress.XtraReports.Parameters.Parameter();
            this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
            this.HideTotal = new DevExpress.XtraReports.UI.FormattingRule();
            this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
            this.xrPivotGrid1 = new DevExpress.XtraReports.UI.XRPivotGrid();
            this.fieldFilter1 = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.fieldSKU1 = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.fieldItemDesc1 = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.fieldUOM1 = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.fieldMaterialType1 = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.fieldDate1 = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.fieldBeginningInventory1 = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.fieldReceiving1 = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.fieldConsumption1 = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.fieldBackload1 = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.fieldItotal1 = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.fieldAReceiving1 = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.fieldAConsumption1 = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.fieldPhysicalCount1 = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.fieldATotal1 = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.fieldEndingInventory1 = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.xrLabel7 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel15 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel11 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrPictureBox1 = new DevExpress.XtraReports.UI.XRPictureBox();
            this.DetailReport = new DevExpress.XtraReports.UI.DetailReportBand();
            this.Detail1 = new DevExpress.XtraReports.UI.DetailBand();
            this.xrControlStyle1 = new DevExpress.XtraReports.UI.XRControlStyle();
            this.xrControlStyle2 = new DevExpress.XtraReports.UI.XRControlStyle();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // sqlDataSource1
            // 
            this.sqlDataSource1.ConnectionName = "GEARS-METSITConnectionString";
            this.sqlDataSource1.Name = "sqlDataSource1";
            storedProcQuery1.Name = "sp_report_DIRSummaryReport";
            queryParameter1.Name = "@DateFrom";
            queryParameter1.Type = typeof(DevExpress.DataAccess.Expression);
            queryParameter1.Value = new DevExpress.DataAccess.Expression("[Parameters.DateFrom]", typeof(System.DateTime));
            queryParameter2.Name = "@DateTo";
            queryParameter2.Type = typeof(DevExpress.DataAccess.Expression);
            queryParameter2.Value = new DevExpress.DataAccess.Expression("[Parameters.DateTo]", typeof(System.DateTime));
            storedProcQuery1.Parameters.Add(queryParameter1);
            storedProcQuery1.Parameters.Add(queryParameter2);
            storedProcQuery1.StoredProcName = "sp_report_ScrapInventoryReportDaily";
            customSqlQuery1.Name = "Company Name";
            customSqlQuery1.Sql = "SELECT Value FROM IT.SystemSettings WHERE Code=\'FRABELLECORP\'";
            customSqlQuery2.Name = "Company METS FLOOR";
            customSqlQuery2.Sql = "SELECT Value FROM IT.SystemSettings WHERE Code=\'METSTPFLOOR\'";
            this.sqlDataSource1.Queries.AddRange(new DevExpress.DataAccess.Sql.SqlQuery[] {
            storedProcQuery1,
            customSqlQuery1,
            customSqlQuery2});
            this.sqlDataSource1.ResultSchemaSerializable = resources.GetString("sqlDataSource1.ResultSchemaSerializable");
            // 
            // Detail
            // 
            this.Detail.Dpi = 100F;
            this.Detail.Expanded = false;
            this.Detail.HeightF = 0F;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // TopMargin
            // 
            this.TopMargin.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel3,
            this.xrPageInfo1,
            this.xrPageInfo2});
            this.TopMargin.Dpi = 100F;
            this.TopMargin.HeightF = 40.00001F;
            this.TopMargin.Name = "TopMargin";
            this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrLabel3
            // 
            this.xrLabel3.Dpi = 100F;
            this.xrLabel3.Font = new System.Drawing.Font("Arial Narrow", 8F, System.Drawing.FontStyle.Bold);
            this.xrLabel3.LocationFloat = new DevExpress.Utils.PointFloat(0F, 20F);
            this.xrLabel3.Name = "xrLabel3";
            this.xrLabel3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel3.Scripts.OnBeforePrint = "xrLabel5_BeforePrint";
            this.xrLabel3.SizeF = new System.Drawing.SizeF(50F, 20F);
            this.xrLabel3.StylePriority.UseFont = false;
            this.xrLabel3.StylePriority.UseTextAlignment = false;
            xrSummary1.FormatString = "{0:M/d/yy}";
            xrSummary1.Func = DevExpress.XtraReports.UI.SummaryFunc.Custom;
            this.xrLabel3.Summary = xrSummary1;
            this.xrLabel3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrPageInfo1
            // 
            this.xrPageInfo1.Dpi = 100F;
            this.xrPageInfo1.Font = new System.Drawing.Font("Times New Roman", 8F);
            this.xrPageInfo1.Format = "Run Date & Time : {0:MMMM dd, yyyy / h:mm tt}";
            this.xrPageInfo1.LocationFloat = new DevExpress.Utils.PointFloat(50F, 23.25001F);
            this.xrPageInfo1.Name = "xrPageInfo1";
            this.xrPageInfo1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrPageInfo1.PageInfo = DevExpress.XtraPrinting.PageInfo.DateTime;
            this.xrPageInfo1.SizeF = new System.Drawing.SizeF(320.4167F, 16.75F);
            this.xrPageInfo1.StylePriority.UseFont = false;
            // 
            // xrPageInfo2
            // 
            this.xrPageInfo2.Dpi = 100F;
            this.xrPageInfo2.Font = new System.Drawing.Font("Times New Roman", 8F);
            this.xrPageInfo2.Format = "Page {0} of {1}";
            this.xrPageInfo2.LocationFloat = new DevExpress.Utils.PointFloat(994.2916F, 23.25001F);
            this.xrPageInfo2.Name = "xrPageInfo2";
            this.xrPageInfo2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrPageInfo2.SizeF = new System.Drawing.SizeF(87.70837F, 16.75F);
            this.xrPageInfo2.StylePriority.UseFont = false;
            this.xrPageInfo2.StylePriority.UseTextAlignment = false;
            this.xrPageInfo2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            // 
            // BottomMargin
            // 
            this.BottomMargin.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)));
            this.BottomMargin.Dpi = 100F;
            this.BottomMargin.HeightF = 41F;
            this.BottomMargin.Name = "BottomMargin";
            this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.BottomMargin.StylePriority.UseBorders = false;
            this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // DateTo
            // 
            this.DateTo.Description = "DateTo";
            this.DateTo.Name = "DateTo";
            this.DateTo.Type = typeof(System.DateTime);
            // 
            // DateFrom
            // 
            this.DateFrom.Description = "DateFrom";
            this.DateFrom.Name = "DateFrom";
            this.DateFrom.Type = typeof(System.DateTime);
            // 
            // ReportFooter
            // 
            this.ReportFooter.Dpi = 100F;
            this.ReportFooter.Expanded = false;
            this.ReportFooter.HeightF = 13.88542F;
            this.ReportFooter.Name = "ReportFooter";
            // 
            // HideTotal
            // 
            this.HideTotal.Condition = "[].Sum([OrderQty]) ==  0";
            // 
            // 
            // 
            this.HideTotal.Formatting.ForeColor = System.Drawing.Color.White;
            this.HideTotal.Formatting.Visible = DevExpress.Utils.DefaultBoolean.True;
            this.HideTotal.Name = "HideTotal";
            // 
            // PageHeader
            // 
            this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPivotGrid1,
            this.xrLabel7,
            this.xrLabel15,
            this.xrLabel2,
            this.xrLabel11,
            this.xrPictureBox1});
            this.PageHeader.Dpi = 100F;
            this.PageHeader.HeightF = 233.7685F;
            this.PageHeader.Name = "PageHeader";
            // 
            // xrPivotGrid1
            // 
            this.xrPivotGrid1.Appearance.FieldValue.Font = new System.Drawing.Font("Cambria", 10F, System.Drawing.FontStyle.Bold);
            this.xrPivotGrid1.Appearance.FieldValue.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.xrPivotGrid1.Appearance.FieldValue.TextVerticalAlignment = DevExpress.Utils.VertAlignment.Center;
            this.xrPivotGrid1.DataMember = "sp_report_DIRSummaryReport";
            this.xrPivotGrid1.DataSource = this.sqlDataSource1;
            this.xrPivotGrid1.Dpi = 100F;
            this.xrPivotGrid1.FieldHeaderStyleName = "xrControlStyle1";
            this.xrPivotGrid1.Fields.AddRange(new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField[] {
            this.fieldFilter1,
            this.fieldSKU1,
            this.fieldItemDesc1,
            this.fieldUOM1,
            this.fieldMaterialType1,
            this.fieldDate1,
            this.fieldBeginningInventory1,
            this.fieldReceiving1,
            this.fieldConsumption1,
            this.fieldBackload1,
            this.fieldItotal1,
            this.fieldAReceiving1,
            this.fieldAConsumption1,
            this.fieldPhysicalCount1,
            this.fieldATotal1,
            this.fieldEndingInventory1});
            this.xrPivotGrid1.FieldValueGrandTotalStyleName = "xrControlStyle2";
            this.xrPivotGrid1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 117.1019F);
            this.xrPivotGrid1.Name = "xrPivotGrid1";
            this.xrPivotGrid1.OptionsPrint.FilterSeparatorBarPadding = 3;
            this.xrPivotGrid1.OptionsPrint.PrintDataHeaders = DevExpress.Utils.DefaultBoolean.False;
            this.xrPivotGrid1.OptionsPrint.PrintFilterHeaders = DevExpress.Utils.DefaultBoolean.False;
            this.xrPivotGrid1.OptionsView.ShowColumnGrandTotalHeader = false;
            this.xrPivotGrid1.OptionsView.ShowColumnGrandTotals = false;
            this.xrPivotGrid1.SizeF = new System.Drawing.SizeF(1237.209F, 116.6666F);
            // 
            // fieldFilter1
            // 
            this.fieldFilter1.AreaIndex = 0;
            this.fieldFilter1.FieldName = "Filter";
            this.fieldFilter1.Name = "fieldFilter1";
            // 
            // fieldSKU1
            // 
            this.fieldSKU1.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.fieldSKU1.AreaIndex = 0;
            this.fieldSKU1.FieldName = "SKU";
            this.fieldSKU1.Name = "fieldSKU1";
            this.fieldSKU1.Width = 75;
            // 
            // fieldItemDesc1
            // 
            this.fieldItemDesc1.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.fieldItemDesc1.AreaIndex = 1;
            this.fieldItemDesc1.FieldName = "ItemDesc";
            this.fieldItemDesc1.Name = "fieldItemDesc1";
            this.fieldItemDesc1.Width = 150;
            // 
            // fieldUOM1
            // 
            this.fieldUOM1.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.fieldUOM1.AreaIndex = 2;
            this.fieldUOM1.FieldName = "UOM";
            this.fieldUOM1.Name = "fieldUOM1";
            // 
            // fieldMaterialType1
            // 
            this.fieldMaterialType1.AreaIndex = 1;
            this.fieldMaterialType1.FieldName = "MaterialType";
            this.fieldMaterialType1.Name = "fieldMaterialType1";
            // 
            // fieldDate1
            // 
            this.fieldDate1.Appearance.FieldHeader.BackColor = System.Drawing.Color.White;
            this.fieldDate1.Appearance.FieldHeader.BorderColor = System.Drawing.Color.White;
            this.fieldDate1.Appearance.FieldHeader.ForeColor = System.Drawing.Color.White;
            this.fieldDate1.Area = DevExpress.XtraPivotGrid.PivotArea.ColumnArea;
            this.fieldDate1.AreaIndex = 0;
            this.fieldDate1.FieldName = "Date";
            this.fieldDate1.Name = "fieldDate1";
            this.fieldDate1.ValueFormat.FormatString = "MM-dd-yyyy";
            this.fieldDate1.ValueFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            // 
            // fieldBeginningInventory1
            // 
            this.fieldBeginningInventory1.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.fieldBeginningInventory1.AreaIndex = 3;
            this.fieldBeginningInventory1.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.fieldBeginningInventory1.FieldName = "BeginningInventory";
            this.fieldBeginningInventory1.Name = "fieldBeginningInventory1";
            this.fieldBeginningInventory1.Width = 150;
            // 
            // fieldReceiving1
            // 
            this.fieldReceiving1.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea;
            this.fieldReceiving1.AreaIndex = 0;
            this.fieldReceiving1.Caption = "IReceiving";
            this.fieldReceiving1.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.fieldReceiving1.FieldName = "Receiving";
            this.fieldReceiving1.Name = "fieldReceiving1";
            this.fieldReceiving1.TotalCellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.fieldReceiving1.TotalValueFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.fieldReceiving1.ValueFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.fieldReceiving1.Width = 80;
            // 
            // fieldConsumption1
            // 
            this.fieldConsumption1.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea;
            this.fieldConsumption1.AreaIndex = 1;
            this.fieldConsumption1.Caption = "IConsumption";
            this.fieldConsumption1.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.fieldConsumption1.FieldName = "Consumption";
            this.fieldConsumption1.Name = "fieldConsumption1";
            this.fieldConsumption1.TotalCellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.fieldConsumption1.TotalValueFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.fieldConsumption1.ValueFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            // 
            // fieldBackload1
            // 
            this.fieldBackload1.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea;
            this.fieldBackload1.AreaIndex = 2;
            this.fieldBackload1.Caption = "IBackload";
            this.fieldBackload1.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.fieldBackload1.FieldName = "Backload";
            this.fieldBackload1.Name = "fieldBackload1";
            this.fieldBackload1.TotalCellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.fieldBackload1.TotalValueFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.fieldBackload1.ValueFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.fieldBackload1.Width = 70;
            // 
            // fieldItotal1
            // 
            this.fieldItotal1.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea;
            this.fieldItotal1.AreaIndex = 3;
            this.fieldItotal1.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.fieldItotal1.FieldName = "Itotal";
            this.fieldItotal1.Name = "fieldItotal1";
            this.fieldItotal1.TotalCellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.fieldItotal1.TotalValueFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.fieldItotal1.ValueFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.fieldItotal1.Width = 70;
            // 
            // fieldAReceiving1
            // 
            this.fieldAReceiving1.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea;
            this.fieldAReceiving1.AreaIndex = 4;
            this.fieldAReceiving1.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.fieldAReceiving1.FieldName = "AReceiving";
            this.fieldAReceiving1.Name = "fieldAReceiving1";
            this.fieldAReceiving1.TotalCellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.fieldAReceiving1.TotalValueFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.fieldAReceiving1.ValueFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.fieldAReceiving1.Width = 85;
            // 
            // fieldAConsumption1
            // 
            this.fieldAConsumption1.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea;
            this.fieldAConsumption1.AreaIndex = 5;
            this.fieldAConsumption1.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.fieldAConsumption1.FieldName = "AConsumption";
            this.fieldAConsumption1.Name = "fieldAConsumption1";
            this.fieldAConsumption1.TotalCellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.fieldAConsumption1.TotalValueFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.fieldAConsumption1.ValueFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            // 
            // fieldPhysicalCount1
            // 
            this.fieldPhysicalCount1.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea;
            this.fieldPhysicalCount1.AreaIndex = 6;
            this.fieldPhysicalCount1.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.fieldPhysicalCount1.FieldName = "PhysicalCount";
            this.fieldPhysicalCount1.Name = "fieldPhysicalCount1";
            this.fieldPhysicalCount1.TotalCellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.fieldPhysicalCount1.TotalValueFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.fieldPhysicalCount1.ValueFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            // 
            // fieldATotal1
            // 
            this.fieldATotal1.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea;
            this.fieldATotal1.AreaIndex = 7;
            this.fieldATotal1.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.fieldATotal1.FieldName = "ATotal";
            this.fieldATotal1.Name = "fieldATotal1";
            this.fieldATotal1.TotalCellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.fieldATotal1.TotalValueFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.fieldATotal1.ValueFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.fieldATotal1.Width = 70;
            // 
            // fieldEndingInventory1
            // 
            this.fieldEndingInventory1.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea;
            this.fieldEndingInventory1.AreaIndex = 8;
            this.fieldEndingInventory1.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.fieldEndingInventory1.FieldName = "EndingInventory";
            this.fieldEndingInventory1.Name = "fieldEndingInventory1";
            this.fieldEndingInventory1.TotalCellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.fieldEndingInventory1.TotalValueFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.fieldEndingInventory1.ValueFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.fieldEndingInventory1.Width = 150;
            // 
            // xrLabel7
            // 
            this.xrLabel7.BackColor = System.Drawing.Color.Gold;
            this.xrLabel7.Dpi = 100F;
            this.xrLabel7.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel7.LocationFloat = new DevExpress.Utils.PointFloat(448.4697F, 31.10182F);
            this.xrLabel7.Name = "xrLabel7";
            this.xrLabel7.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel7.Scripts.OnBeforePrint = "xrLabel1_BeforePrint";
            this.xrLabel7.SizeF = new System.Drawing.SizeF(100.1898F, 23F);
            this.xrLabel7.StylePriority.UseBackColor = false;
            this.xrLabel7.StylePriority.UseFont = false;
            this.xrLabel7.StylePriority.UseTextAlignment = false;
            this.xrLabel7.Text = "TP FLOOR";
            this.xrLabel7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel15
            // 
            this.xrLabel15.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Company Name.Value")});
            this.xrLabel15.Dpi = 100F;
            this.xrLabel15.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Bold);
            this.xrLabel15.ForeColor = System.Drawing.Color.Red;
            this.xrLabel15.LocationFloat = new DevExpress.Utils.PointFloat(135.1389F, 0F);
            this.xrLabel15.Name = "xrLabel15";
            this.xrLabel15.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel15.SizeF = new System.Drawing.SizeF(313.3308F, 31.10185F);
            this.xrLabel15.StylePriority.UseFont = false;
            this.xrLabel15.StylePriority.UseForeColor = false;
            this.xrLabel15.StylePriority.UseTextAlignment = false;
            this.xrLabel15.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel2
            // 
            this.xrLabel2.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_DIRSummaryReport.Filter")});
            this.xrLabel2.Dpi = 100F;
            this.xrLabel2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(135.1389F, 54.10185F);
            this.xrLabel2.Name = "xrLabel2";
            this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel2.Scripts.OnBeforePrint = "xrLabel1_BeforePrint";
            this.xrLabel2.SizeF = new System.Drawing.SizeF(313.3308F, 23F);
            this.xrLabel2.StylePriority.UseFont = false;
            this.xrLabel2.StylePriority.UseTextAlignment = false;
            this.xrLabel2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel11
            // 
            this.xrLabel11.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Company METS FLOOR.Value")});
            this.xrLabel11.Dpi = 100F;
            this.xrLabel11.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel11.LocationFloat = new DevExpress.Utils.PointFloat(135.1389F, 31.10182F);
            this.xrLabel11.Name = "xrLabel11";
            this.xrLabel11.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel11.Scripts.OnBeforePrint = "xrLabel1_BeforePrint";
            this.xrLabel11.SizeF = new System.Drawing.SizeF(313.3308F, 23F);
            this.xrLabel11.StylePriority.UseFont = false;
            this.xrLabel11.StylePriority.UseTextAlignment = false;
            this.xrLabel11.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrPictureBox1
            // 
            this.xrPictureBox1.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrPictureBox1.Dpi = 100F;
            this.xrPictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("xrPictureBox1.Image")));
            this.xrPictureBox1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrPictureBox1.Name = "xrPictureBox1";
            this.xrPictureBox1.SizeF = new System.Drawing.SizeF(135.1389F, 77.10184F);
            this.xrPictureBox1.Sizing = DevExpress.XtraPrinting.ImageSizeMode.StretchImage;
            this.xrPictureBox1.StylePriority.UseBorders = false;
            // 
            // DetailReport
            // 
            this.DetailReport.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail1});
            this.DetailReport.DataSource = this.sqlDataSource1;
            this.DetailReport.Dpi = 100F;
            this.DetailReport.Expanded = false;
            this.DetailReport.Level = 0;
            this.DetailReport.Name = "DetailReport";
            // 
            // Detail1
            // 
            this.Detail1.Dpi = 100F;
            this.Detail1.HeightF = 0F;
            this.Detail1.Name = "Detail1";
            // 
            // xrControlStyle1
            // 
            this.xrControlStyle1.BackColor = System.Drawing.Color.DarkBlue;
            this.xrControlStyle1.Font = new System.Drawing.Font("Cambria", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrControlStyle1.ForeColor = System.Drawing.Color.White;
            this.xrControlStyle1.Name = "xrControlStyle1";
            this.xrControlStyle1.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.xrControlStyle1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrControlStyle2
            // 
            this.xrControlStyle2.BackColor = System.Drawing.Color.LightGray;
            this.xrControlStyle2.Font = new System.Drawing.Font("Cambria", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrControlStyle2.Name = "xrControlStyle2";
            this.xrControlStyle2.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.xrControlStyle2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // R_ScrapInventoryReport
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.ReportFooter,
            this.PageHeader,
            this.DetailReport});
            this.ComponentStorage.AddRange(new System.ComponentModel.IComponent[] {
            this.sqlDataSource1});
            this.DataSource = this.sqlDataSource1;
            this.FormattingRuleSheet.AddRange(new DevExpress.XtraReports.UI.FormattingRule[] {
            this.HideTotal});
            this.Landscape = true;
            this.Margins = new System.Drawing.Printing.Margins(10, 9, 40, 41);
            this.PageHeight = 1488;
            this.PageWidth = 50000;
            this.PaperKind = System.Drawing.Printing.PaperKind.Custom;
            this.Parameters.AddRange(new DevExpress.XtraReports.Parameters.Parameter[] {
            this.DateFrom,
            this.DateTo});
            this.RollPaper = true;
            this.Scripts.OnParametersRequestBeforeShow = "R_MonthlySummaryReport_ParametersRequestBeforeShow";
            this.ScriptsSource = resources.GetString("$this.ScriptsSource");
            this.StyleSheet.AddRange(new DevExpress.XtraReports.UI.XRControlStyle[] {
            this.xrControlStyle1,
            this.xrControlStyle2});
            this.Version = "15.2";
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private DevExpress.DataAccess.Sql.SqlDataSource sqlDataSource1;
        private DevExpress.XtraReports.UI.XRPageInfo xrPageInfo2;
        private DevExpress.XtraReports.Parameters.Parameter DateFrom;
        private DevExpress.XtraReports.Parameters.Parameter DateTo;
        private DevExpress.XtraReports.UI.ReportFooterBand ReportFooter;
        private DevExpress.XtraReports.UI.FormattingRule HideTotal;
        private DevExpress.XtraReports.UI.XRLabel xrLabel3;
        private DevExpress.XtraReports.UI.XRPageInfo xrPageInfo1;
        private DevExpress.XtraReports.UI.PageHeaderBand PageHeader;
        private DevExpress.XtraReports.UI.XRLabel xrLabel7;
        private DevExpress.XtraReports.UI.XRLabel xrLabel15;
        private DevExpress.XtraReports.UI.XRLabel xrLabel2;
        private DevExpress.XtraReports.UI.XRLabel xrLabel11;
        private DevExpress.XtraReports.UI.XRPictureBox xrPictureBox1;
        private DevExpress.XtraReports.UI.DetailReportBand DetailReport;
        private DevExpress.XtraReports.UI.DetailBand Detail1;
        private DevExpress.XtraReports.UI.XRPivotGrid xrPivotGrid1;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField fieldFilter1;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField fieldSKU1;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField fieldItemDesc1;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField fieldUOM1;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField fieldMaterialType1;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField fieldDate1;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField fieldBeginningInventory1;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField fieldReceiving1;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField fieldConsumption1;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField fieldBackload1;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField fieldItotal1;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField fieldAReceiving1;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField fieldAConsumption1;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField fieldPhysicalCount1;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField fieldATotal1;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField fieldEndingInventory1;
        private DevExpress.XtraReports.UI.XRControlStyle xrControlStyle1;
        private DevExpress.XtraReports.UI.XRControlStyle xrControlStyle2;
    }
}
