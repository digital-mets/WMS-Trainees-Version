namespace GWL.WebReports.GEARS_Reports
{
    partial class R_SOvsInventorySum

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
            DevExpress.DataAccess.Sql.CustomSqlQuery customSqlQuery1 = new DevExpress.DataAccess.Sql.CustomSqlQuery();
            DevExpress.DataAccess.Sql.CustomSqlQuery customSqlQuery2 = new DevExpress.DataAccess.Sql.CustomSqlQuery();
            DevExpress.DataAccess.Sql.CustomSqlQuery customSqlQuery3 = new DevExpress.DataAccess.Sql.CustomSqlQuery();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(R_SOvsInventorySum));
            DevExpress.DataAccess.Sql.CustomSqlQuery customSqlQuery4 = new DevExpress.DataAccess.Sql.CustomSqlQuery();
            DevExpress.DataAccess.Sql.CustomSqlQuery customSqlQuery5 = new DevExpress.DataAccess.Sql.CustomSqlQuery();
            DevExpress.DataAccess.Sql.CustomSqlQuery customSqlQuery6 = new DevExpress.DataAccess.Sql.CustomSqlQuery();
            DevExpress.DataAccess.Sql.CustomSqlQuery customSqlQuery7 = new DevExpress.DataAccess.Sql.CustomSqlQuery();
            DevExpress.DataAccess.Sql.CustomSqlQuery customSqlQuery8 = new DevExpress.DataAccess.Sql.CustomSqlQuery();
            DevExpress.DataAccess.Sql.CustomSqlQuery customSqlQuery9 = new DevExpress.DataAccess.Sql.CustomSqlQuery();
            DevExpress.DataAccess.Sql.StoredProcQuery storedProcQuery1 = new DevExpress.DataAccess.Sql.StoredProcQuery();
            DevExpress.DataAccess.Sql.QueryParameter queryParameter1 = new DevExpress.DataAccess.Sql.QueryParameter();
            DevExpress.DataAccess.Sql.QueryParameter queryParameter2 = new DevExpress.DataAccess.Sql.QueryParameter();
            DevExpress.DataAccess.Sql.QueryParameter queryParameter3 = new DevExpress.DataAccess.Sql.QueryParameter();
            DevExpress.DataAccess.Sql.QueryParameter queryParameter4 = new DevExpress.DataAccess.Sql.QueryParameter();
            DevExpress.DataAccess.Sql.QueryParameter queryParameter5 = new DevExpress.DataAccess.Sql.QueryParameter();
            DevExpress.DataAccess.Sql.QueryParameter queryParameter6 = new DevExpress.DataAccess.Sql.QueryParameter();
            DevExpress.DataAccess.Sql.QueryParameter queryParameter7 = new DevExpress.DataAccess.Sql.QueryParameter();
            DevExpress.XtraReports.UI.XRSummary xrSummary1 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.Parameters.DynamicListLookUpSettings dynamicListLookUpSettings1 = new DevExpress.XtraReports.Parameters.DynamicListLookUpSettings();
            DevExpress.XtraReports.Parameters.DynamicListLookUpSettings dynamicListLookUpSettings2 = new DevExpress.XtraReports.Parameters.DynamicListLookUpSettings();
            DevExpress.XtraReports.Parameters.DynamicListLookUpSettings dynamicListLookUpSettings3 = new DevExpress.XtraReports.Parameters.DynamicListLookUpSettings();
            DevExpress.XtraReports.Parameters.DynamicListLookUpSettings dynamicListLookUpSettings4 = new DevExpress.XtraReports.Parameters.DynamicListLookUpSettings();
            DevExpress.XtraReports.Parameters.DynamicListLookUpSettings dynamicListLookUpSettings5 = new DevExpress.XtraReports.Parameters.DynamicListLookUpSettings();
            DevExpress.XtraReports.Parameters.DynamicListLookUpSettings dynamicListLookUpSettings6 = new DevExpress.XtraReports.Parameters.DynamicListLookUpSettings();
            DevExpress.XtraReports.Parameters.DynamicListLookUpSettings dynamicListLookUpSettings7 = new DevExpress.XtraReports.Parameters.DynamicListLookUpSettings();
            this.sqlDataSource1 = new DevExpress.DataAccess.Sql.SqlDataSource(this.components);
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel15 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel18 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrPageInfo2 = new DevExpress.XtraReports.UI.XRPageInfo();
            this.xrPageInfo1 = new DevExpress.XtraReports.UI.XRPageInfo();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.xrPivotGrid1 = new DevExpress.XtraReports.UI.XRPivotGrid();
            this.fieldHEADER1 = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.fieldItemCode1 = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.fieldColorCode1 = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.fieldClassCode1 = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.fieldSizeCode1 = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.fieldQty1 = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.fieldOrd1 = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.formattingRule1 = new DevExpress.XtraReports.UI.FormattingRule();
            this.ItemCode = new DevExpress.XtraReports.Parameters.Parameter();
            this.Color = new DevExpress.XtraReports.Parameters.Parameter();
            this.Class = new DevExpress.XtraReports.Parameters.Parameter();
            this.Size = new DevExpress.XtraReports.Parameters.Parameter();
            this.ItemCategory = new DevExpress.XtraReports.Parameters.Parameter();
            this.ProductCategory = new DevExpress.XtraReports.Parameters.Parameter();
            this.ProductSubCategory = new DevExpress.XtraReports.Parameters.Parameter();
            this.GroupHeader1 = new DevExpress.XtraReports.UI.GroupHeaderBand();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // sqlDataSource1
            // 
            this.sqlDataSource1.ConnectionName = "GEARS-METSITConnectionString";
            this.sqlDataSource1.Name = "sqlDataSource1";
            customSqlQuery1.Name = "CompanyName";
            customSqlQuery1.Sql = "SELECT Value FROM IT.SystemSettings WHERE Code = \'COMPNAME\'";
            customSqlQuery2.Name = "CompanyAddress";
            customSqlQuery2.Sql = "SELECT Value FROM IT.SystemSettings WHERE Code = \'COMPADDR\'";
            customSqlQuery3.Name = "ItemCategory";
            customSqlQuery3.Sql = resources.GetString("customSqlQuery3.Sql");
            customSqlQuery4.Name = "ProductCategory";
            customSqlQuery4.Sql = resources.GetString("customSqlQuery4.Sql");
            customSqlQuery5.Name = "ProductCategorySub";
            customSqlQuery5.Sql = resources.GetString("customSqlQuery5.Sql");
            customSqlQuery6.Name = "ItemCode";
            customSqlQuery6.Sql = resources.GetString("customSqlQuery6.Sql");
            customSqlQuery7.Name = "ColorCode";
            customSqlQuery7.Sql = resources.GetString("customSqlQuery7.Sql");
            customSqlQuery8.Name = "ClassCode";
            customSqlQuery8.Sql = resources.GetString("customSqlQuery8.Sql");
            customSqlQuery9.Name = "SizeCode";
            customSqlQuery9.Sql = resources.GetString("customSqlQuery9.Sql");
            storedProcQuery1.Name = "sp_report_SOvsInventorySummary";
            queryParameter1.Name = "@ItemCategory";
            queryParameter1.Type = typeof(DevExpress.DataAccess.Expression);
            queryParameter1.Value = new DevExpress.DataAccess.Expression("[Parameters.ItemCategory]", typeof(string));
            queryParameter2.Name = "@ProductCategory";
            queryParameter2.Type = typeof(DevExpress.DataAccess.Expression);
            queryParameter2.Value = new DevExpress.DataAccess.Expression("[Parameters.ProductCategory]", typeof(string));
            queryParameter3.Name = "@ProductSubCategory";
            queryParameter3.Type = typeof(DevExpress.DataAccess.Expression);
            queryParameter3.Value = new DevExpress.DataAccess.Expression("[Parameters.ProductSubCategory]", typeof(string));
            queryParameter4.Name = "@Item";
            queryParameter4.Type = typeof(DevExpress.DataAccess.Expression);
            queryParameter4.Value = new DevExpress.DataAccess.Expression("[Parameters.ItemCode]", typeof(string));
            queryParameter5.Name = "@Color";
            queryParameter5.Type = typeof(DevExpress.DataAccess.Expression);
            queryParameter5.Value = new DevExpress.DataAccess.Expression("[Parameters.Color]", typeof(string));
            queryParameter6.Name = "@Class";
            queryParameter6.Type = typeof(DevExpress.DataAccess.Expression);
            queryParameter6.Value = new DevExpress.DataAccess.Expression("[Parameters.Class]", typeof(string));
            queryParameter7.Name = "@Size";
            queryParameter7.Type = typeof(DevExpress.DataAccess.Expression);
            queryParameter7.Value = new DevExpress.DataAccess.Expression("[Parameters.Size]", typeof(string));
            storedProcQuery1.Parameters.Add(queryParameter1);
            storedProcQuery1.Parameters.Add(queryParameter2);
            storedProcQuery1.Parameters.Add(queryParameter3);
            storedProcQuery1.Parameters.Add(queryParameter4);
            storedProcQuery1.Parameters.Add(queryParameter5);
            storedProcQuery1.Parameters.Add(queryParameter6);
            storedProcQuery1.Parameters.Add(queryParameter7);
            storedProcQuery1.StoredProcName = "sp_report_SOvsInventorySummary";
            this.sqlDataSource1.Queries.AddRange(new DevExpress.DataAccess.Sql.SqlQuery[] {
            customSqlQuery1,
            customSqlQuery2,
            customSqlQuery3,
            customSqlQuery4,
            customSqlQuery5,
            customSqlQuery6,
            customSqlQuery7,
            customSqlQuery8,
            customSqlQuery9,
            storedProcQuery1});
            this.sqlDataSource1.ResultSchemaSerializable = resources.GetString("sqlDataSource1.ResultSchemaSerializable");
            // 
            // Detail
            // 
            this.Detail.Dpi = 100F;
            this.Detail.HeightF = 24.99997F;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // TopMargin
            // 
            this.TopMargin.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel2,
            this.xrLabel15,
            this.xrLabel18,
            this.xrPageInfo2,
            this.xrPageInfo1});
            this.TopMargin.Dpi = 100F;
            this.TopMargin.HeightF = 149F;
            this.TopMargin.Name = "TopMargin";
            this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrLabel2
            // 
            this.xrLabel2.Dpi = 100F;
            this.xrLabel2.Font = new System.Drawing.Font("Arial Narrow", 8F, System.Drawing.FontStyle.Bold);
            this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 9.166673F);
            this.xrLabel2.Name = "xrLabel2";
            this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel2.Scripts.OnBeforePrint = "xrLabel5_BeforePrint";
            this.xrLabel2.SizeF = new System.Drawing.SizeF(50F, 20F);
            this.xrLabel2.StylePriority.UseFont = false;
            this.xrLabel2.StylePriority.UseTextAlignment = false;
            xrSummary1.FormatString = "{0:M/d/yy}";
            xrSummary1.Func = DevExpress.XtraReports.UI.SummaryFunc.Custom;
            this.xrLabel2.Summary = xrSummary1;
            this.xrLabel2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel15
            // 
            this.xrLabel15.Dpi = 100F;
            this.xrLabel15.Font = new System.Drawing.Font("Arial Narrow", 13F, System.Drawing.FontStyle.Bold);
            this.xrLabel15.LocationFloat = new DevExpress.Utils.PointFloat(0F, 86.02082F);
            this.xrLabel15.Name = "xrLabel15";
            this.xrLabel15.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel15.SizeF = new System.Drawing.SizeF(1082F, 23F);
            this.xrLabel15.StylePriority.UseFont = false;
            this.xrLabel15.StylePriority.UseTextAlignment = false;
            this.xrLabel15.Text = "Sales Order vs Inventory Summary";
            this.xrLabel15.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel18
            // 
            this.xrLabel18.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "CompanyName.Value")});
            this.xrLabel18.Dpi = 100F;
            this.xrLabel18.Font = new System.Drawing.Font("Arial Narrow", 9.75F);
            this.xrLabel18.LocationFloat = new DevExpress.Utils.PointFloat(0F, 63.02083F);
            this.xrLabel18.Name = "xrLabel18";
            this.xrLabel18.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel18.SizeF = new System.Drawing.SizeF(1082F, 23F);
            this.xrLabel18.StylePriority.UseFont = false;
            this.xrLabel18.StylePriority.UseTextAlignment = false;
            this.xrLabel18.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrPageInfo2
            // 
            this.xrPageInfo2.Dpi = 100F;
            this.xrPageInfo2.Font = new System.Drawing.Font("Times New Roman", 8F);
            this.xrPageInfo2.Format = "Page {0} of {1}";
            this.xrPageInfo2.LocationFloat = new DevExpress.Utils.PointFloat(994.2917F, 12.41667F);
            this.xrPageInfo2.Name = "xrPageInfo2";
            this.xrPageInfo2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrPageInfo2.SizeF = new System.Drawing.SizeF(87.70837F, 16.75F);
            this.xrPageInfo2.StylePriority.UseFont = false;
            this.xrPageInfo2.StylePriority.UseTextAlignment = false;
            this.xrPageInfo2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            // 
            // xrPageInfo1
            // 
            this.xrPageInfo1.Dpi = 100F;
            this.xrPageInfo1.Font = new System.Drawing.Font("Times New Roman", 8F);
            this.xrPageInfo1.Format = "Run Date & Time: {0:MMMM dd, yyyy / h:mm tt}";
            this.xrPageInfo1.LocationFloat = new DevExpress.Utils.PointFloat(50F, 12.41667F);
            this.xrPageInfo1.Name = "xrPageInfo1";
            this.xrPageInfo1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrPageInfo1.PageInfo = DevExpress.XtraPrinting.PageInfo.DateTime;
            this.xrPageInfo1.SizeF = new System.Drawing.SizeF(320.4167F, 16.75F);
            this.xrPageInfo1.StylePriority.UseFont = false;
            // 
            // BottomMargin
            // 
            this.BottomMargin.Dpi = 100F;
            this.BottomMargin.HeightF = 40F;
            this.BottomMargin.Name = "BottomMargin";
            this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrPivotGrid1
            // 
            this.xrPivotGrid1.Appearance.Cell.BackColor = System.Drawing.Color.White;
            this.xrPivotGrid1.Appearance.Cell.Font = new System.Drawing.Font("Arial Narrow", 8F);
            this.xrPivotGrid1.Appearance.Cell.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.xrPivotGrid1.Appearance.Cell.TextVerticalAlignment = DevExpress.Utils.VertAlignment.Center;
            this.xrPivotGrid1.Appearance.FieldHeader.BackColor = System.Drawing.Color.White;
            this.xrPivotGrid1.Appearance.FieldValue.BackColor = System.Drawing.Color.White;
            this.xrPivotGrid1.Appearance.FieldValue.Font = new System.Drawing.Font("Arial Narrow", 8.25F);
            this.xrPivotGrid1.Appearance.FieldValue.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.xrPivotGrid1.Appearance.FieldValue.TextVerticalAlignment = DevExpress.Utils.VertAlignment.Center;
            this.xrPivotGrid1.Appearance.FieldValueGrandTotal.BackColor = System.Drawing.Color.LightGray;
            this.xrPivotGrid1.Appearance.FieldValueTotal.BackColor = System.Drawing.Color.LightGray;
            this.xrPivotGrid1.Appearance.FieldValueTotal.Font = new System.Drawing.Font("Arial Narrow", 9F, System.Drawing.FontStyle.Bold);
            this.xrPivotGrid1.Appearance.GrandTotalCell.BackColor = System.Drawing.Color.LightGray;
            this.xrPivotGrid1.Appearance.TotalCell.BackColor = System.Drawing.Color.LightGray;
            this.xrPivotGrid1.Appearance.TotalCell.Font = new System.Drawing.Font("Arial Narrow", 10F, System.Drawing.FontStyle.Bold);
            this.xrPivotGrid1.DataMember = "sp_report_SOvsInventorySummary";
            this.xrPivotGrid1.DataSource = this.sqlDataSource1;
            this.xrPivotGrid1.Dpi = 100F;
            this.xrPivotGrid1.Fields.AddRange(new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField[] {
            this.fieldHEADER1,
            this.fieldItemCode1,
            this.fieldColorCode1,
            this.fieldClassCode1,
            this.fieldSizeCode1,
            this.fieldQty1,
            this.fieldOrd1});
            this.xrPivotGrid1.LocationFloat = new DevExpress.Utils.PointFloat(2.499958F, 0F);
            this.xrPivotGrid1.Name = "xrPivotGrid1";
            this.xrPivotGrid1.OptionsPrint.FilterSeparatorBarPadding = 3;
            this.xrPivotGrid1.OptionsPrint.PrintHeadersOnEveryPage = true;
            this.xrPivotGrid1.OptionsView.ShowColumnGrandTotals = false;
            this.xrPivotGrid1.OptionsView.ShowColumnHeaders = false;
            this.xrPivotGrid1.OptionsView.ShowColumnTotals = false;
            this.xrPivotGrid1.OptionsView.ShowDataHeaders = false;
            this.xrPivotGrid1.OptionsView.ShowFilterHeaders = false;
            this.xrPivotGrid1.OptionsView.ShowRowGrandTotals = false;
            this.xrPivotGrid1.OptionsView.ShowTotalsForSingleValues = true;
            this.xrPivotGrid1.Scripts.OnBeforePrint = "xrPivotGrid1_BeforePrint";
            this.xrPivotGrid1.Scripts.OnPrintHeader = "xrPivotGrid1_PrintHeader";
            this.xrPivotGrid1.SizeF = new System.Drawing.SizeF(1069.5F, 43.75F);
            // 
            // fieldHEADER1
            // 
            this.fieldHEADER1.Appearance.FieldHeader.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.fieldHEADER1.Appearance.FieldHeader.TextVerticalAlignment = DevExpress.Utils.VertAlignment.Center;
            this.fieldHEADER1.Appearance.FieldValue.Font = new System.Drawing.Font("Arial Narrow", 10F, System.Drawing.FontStyle.Bold);
            this.fieldHEADER1.Appearance.FieldValue.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.fieldHEADER1.Appearance.FieldValue.TextVerticalAlignment = DevExpress.Utils.VertAlignment.Center;
            this.fieldHEADER1.Appearance.FieldValue.WordWrap = true;
            this.fieldHEADER1.Area = DevExpress.XtraPivotGrid.PivotArea.ColumnArea;
            this.fieldHEADER1.AreaIndex = 1;
            this.fieldHEADER1.Caption = "HEADER";
            this.fieldHEADER1.FieldName = "HEADER";
            this.fieldHEADER1.Name = "fieldHEADER1";
            // 
            // fieldItemCode1
            // 
            this.fieldItemCode1.Appearance.FieldHeader.Font = new System.Drawing.Font("Arial Narrow", 10F, System.Drawing.FontStyle.Bold);
            this.fieldItemCode1.Appearance.FieldHeader.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.fieldItemCode1.Appearance.FieldHeader.TextVerticalAlignment = DevExpress.Utils.VertAlignment.Center;
            this.fieldItemCode1.Appearance.FieldValue.Font = new System.Drawing.Font("Arial Narrow", 8F);
            this.fieldItemCode1.Appearance.FieldValue.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.fieldItemCode1.Appearance.FieldValue.TextVerticalAlignment = DevExpress.Utils.VertAlignment.Center;
            this.fieldItemCode1.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.fieldItemCode1.AreaIndex = 0;
            this.fieldItemCode1.Caption = "Item Code";
            this.fieldItemCode1.FieldName = "ItemCode";
            this.fieldItemCode1.Name = "fieldItemCode1";
            // 
            // fieldColorCode1
            // 
            this.fieldColorCode1.Appearance.FieldHeader.Font = new System.Drawing.Font("Arial Narrow", 10F, System.Drawing.FontStyle.Bold);
            this.fieldColorCode1.Appearance.FieldHeader.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.fieldColorCode1.Appearance.FieldHeader.TextVerticalAlignment = DevExpress.Utils.VertAlignment.Center;
            this.fieldColorCode1.Appearance.FieldValue.Font = new System.Drawing.Font("Arial Narrow", 8F);
            this.fieldColorCode1.Appearance.FieldValue.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.fieldColorCode1.Appearance.FieldValue.TextVerticalAlignment = DevExpress.Utils.VertAlignment.Center;
            this.fieldColorCode1.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.fieldColorCode1.AreaIndex = 1;
            this.fieldColorCode1.Caption = "Color Code";
            this.fieldColorCode1.FieldName = "ColorCode";
            this.fieldColorCode1.Name = "fieldColorCode1";
            this.fieldColorCode1.Options.ShowTotals = false;
            this.fieldColorCode1.Width = 70;
            // 
            // fieldClassCode1
            // 
            this.fieldClassCode1.Appearance.FieldHeader.Font = new System.Drawing.Font("Arial Narrow", 10F, System.Drawing.FontStyle.Bold);
            this.fieldClassCode1.Appearance.FieldHeader.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.fieldClassCode1.Appearance.FieldHeader.TextVerticalAlignment = DevExpress.Utils.VertAlignment.Center;
            this.fieldClassCode1.Appearance.FieldValue.Font = new System.Drawing.Font("Arial Narrow", 8F);
            this.fieldClassCode1.Appearance.FieldValue.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.fieldClassCode1.Appearance.FieldValue.TextVerticalAlignment = DevExpress.Utils.VertAlignment.Center;
            this.fieldClassCode1.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.fieldClassCode1.AreaIndex = 2;
            this.fieldClassCode1.Caption = "Class Code";
            this.fieldClassCode1.FieldName = "ClassCode";
            this.fieldClassCode1.Name = "fieldClassCode1";
            this.fieldClassCode1.Options.ShowTotals = false;
            this.fieldClassCode1.Width = 70;
            // 
            // fieldSizeCode1
            // 
            this.fieldSizeCode1.Appearance.FieldHeader.Font = new System.Drawing.Font("Arial Narrow", 10F, System.Drawing.FontStyle.Bold);
            this.fieldSizeCode1.Appearance.FieldHeader.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.fieldSizeCode1.Appearance.FieldHeader.TextVerticalAlignment = DevExpress.Utils.VertAlignment.Center;
            this.fieldSizeCode1.Appearance.FieldValue.Font = new System.Drawing.Font("Arial Narrow", 8F);
            this.fieldSizeCode1.Appearance.FieldValue.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.fieldSizeCode1.Appearance.FieldValue.TextVerticalAlignment = DevExpress.Utils.VertAlignment.Center;
            this.fieldSizeCode1.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.fieldSizeCode1.AreaIndex = 3;
            this.fieldSizeCode1.Caption = "Size Code";
            this.fieldSizeCode1.FieldName = "SizeCode";
            this.fieldSizeCode1.Name = "fieldSizeCode1";
            this.fieldSizeCode1.Options.ShowTotals = false;
            this.fieldSizeCode1.Width = 70;
            // 
            // fieldQty1
            // 
            this.fieldQty1.Appearance.FieldValue.Font = new System.Drawing.Font("Arial Narrow", 8F);
            this.fieldQty1.Appearance.FieldValue.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.fieldQty1.Appearance.FieldValue.TextVerticalAlignment = DevExpress.Utils.VertAlignment.Center;
            this.fieldQty1.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea;
            this.fieldQty1.AreaIndex = 0;
            this.fieldQty1.Caption = "Qty";
            this.fieldQty1.CellFormat.FormatString = "#,#.00";
            this.fieldQty1.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.fieldQty1.FieldName = "Qty";
            this.fieldQty1.Name = "fieldQty1";
            // 
            // fieldOrd1
            // 
            this.fieldOrd1.Appearance.FieldHeader.BackColor = System.Drawing.Color.White;
            this.fieldOrd1.Appearance.FieldValue.BackColor = System.Drawing.Color.White;
            this.fieldOrd1.Appearance.FieldValue.BorderColor = System.Drawing.Color.White;
            this.fieldOrd1.Appearance.FieldValue.ForeColor = System.Drawing.Color.White;
            this.fieldOrd1.Area = DevExpress.XtraPivotGrid.PivotArea.ColumnArea;
            this.fieldOrd1.AreaIndex = 0;
            this.fieldOrd1.Caption = "Ord";
            this.fieldOrd1.FieldName = "Ord";
            this.fieldOrd1.Name = "fieldOrd1";
            // 
            // formattingRule1
            // 
            // 
            // 
            // 
            this.formattingRule1.Formatting.BackColor = System.Drawing.Color.White;
            this.formattingRule1.Name = "formattingRule1";
            // 
            // ItemCode
            // 
            this.ItemCode.Description = "ItemCode";
            dynamicListLookUpSettings1.DataAdapter = null;
            dynamicListLookUpSettings1.DataMember = "ItemCode";
            dynamicListLookUpSettings1.DataSource = this.sqlDataSource1;
            dynamicListLookUpSettings1.DisplayMember = "ShortDesc";
            dynamicListLookUpSettings1.ValueMember = "ItemCode";
            this.ItemCode.LookUpSettings = dynamicListLookUpSettings1;
            this.ItemCode.Name = "ItemCode";
            this.ItemCode.ValueInfo = "N/A";
            // 
            // Color
            // 
            this.Color.Description = "Color";
            dynamicListLookUpSettings2.DataAdapter = null;
            dynamicListLookUpSettings2.DataMember = "ColorCode";
            dynamicListLookUpSettings2.DataSource = this.sqlDataSource1;
            dynamicListLookUpSettings2.DisplayMember = "Description";
            dynamicListLookUpSettings2.ValueMember = "ColorCode";
            this.Color.LookUpSettings = dynamicListLookUpSettings2;
            this.Color.Name = "Color";
            this.Color.ValueInfo = "N/A";
            // 
            // Class
            // 
            this.Class.Description = "Class";
            dynamicListLookUpSettings3.DataAdapter = null;
            dynamicListLookUpSettings3.DataMember = "ClassCode";
            dynamicListLookUpSettings3.DataSource = this.sqlDataSource1;
            dynamicListLookUpSettings3.DisplayMember = "Description";
            dynamicListLookUpSettings3.ValueMember = "ClassCode";
            this.Class.LookUpSettings = dynamicListLookUpSettings3;
            this.Class.Name = "Class";
            this.Class.ValueInfo = "N/A";
            // 
            // Size
            // 
            this.Size.Description = "Size";
            dynamicListLookUpSettings4.DataAdapter = null;
            dynamicListLookUpSettings4.DataMember = "SizeCode";
            dynamicListLookUpSettings4.DataSource = this.sqlDataSource1;
            dynamicListLookUpSettings4.DisplayMember = "Description";
            dynamicListLookUpSettings4.ValueMember = "SizeCode";
            this.Size.LookUpSettings = dynamicListLookUpSettings4;
            this.Size.Name = "Size";
            this.Size.ValueInfo = "N/A";
            // 
            // ItemCategory
            // 
            this.ItemCategory.Description = "ItemCategory";
            dynamicListLookUpSettings5.DataAdapter = null;
            dynamicListLookUpSettings5.DataMember = "ItemCategory";
            dynamicListLookUpSettings5.DataSource = this.sqlDataSource1;
            dynamicListLookUpSettings5.DisplayMember = "Description";
            dynamicListLookUpSettings5.ValueMember = "ItemCategoryCode";
            this.ItemCategory.LookUpSettings = dynamicListLookUpSettings5;
            this.ItemCategory.Name = "ItemCategory";
            this.ItemCategory.ValueInfo = "N/A";
            // 
            // ProductCategory
            // 
            this.ProductCategory.Description = "ProductCategory";
            dynamicListLookUpSettings6.DataAdapter = null;
            dynamicListLookUpSettings6.DataMember = "ProductCategory";
            dynamicListLookUpSettings6.DataSource = this.sqlDataSource1;
            dynamicListLookUpSettings6.DisplayMember = "Description";
            dynamicListLookUpSettings6.ValueMember = "ProductCategoryCode";
            this.ProductCategory.LookUpSettings = dynamicListLookUpSettings6;
            this.ProductCategory.Name = "ProductCategory";
            this.ProductCategory.ValueInfo = "N/A";
            // 
            // ProductSubCategory
            // 
            this.ProductSubCategory.Description = "ProductSubCategory";
            dynamicListLookUpSettings7.DataAdapter = null;
            dynamicListLookUpSettings7.DataMember = "ProductCategorySub";
            dynamicListLookUpSettings7.DataSource = this.sqlDataSource1;
            dynamicListLookUpSettings7.DisplayMember = "Description";
            dynamicListLookUpSettings7.ValueMember = "ProductSubCatCode";
            this.ProductSubCategory.LookUpSettings = dynamicListLookUpSettings7;
            this.ProductSubCategory.Name = "ProductSubCategory";
            this.ProductSubCategory.ValueInfo = "N/A";
            // 
            // GroupHeader1
            // 
            this.GroupHeader1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPivotGrid1});
            this.GroupHeader1.Dpi = 100F;
            this.GroupHeader1.HeightF = 43.75F;
            this.GroupHeader1.Name = "GroupHeader1";
            // 
            // R_SOvsInventorySum
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.GroupHeader1});
            this.ComponentStorage.AddRange(new System.ComponentModel.IComponent[] {
            this.sqlDataSource1});
            this.DataMember = "sp_report_SOvsInventorySummary";
            this.DataSource = this.sqlDataSource1;
            this.FormattingRuleSheet.AddRange(new DevExpress.XtraReports.UI.FormattingRule[] {
            this.formattingRule1});
            this.Landscape = true;
            this.Margins = new System.Drawing.Printing.Margins(9, 9, 149, 40);
            this.PageHeight = 850;
            this.PageWidth = 1100;
            this.PaperKind = System.Drawing.Printing.PaperKind.Custom;
            this.Parameters.AddRange(new DevExpress.XtraReports.Parameters.Parameter[] {
            this.ItemCategory,
            this.ProductCategory,
            this.ProductSubCategory,
            this.ItemCode,
            this.Color,
            this.Class,
            this.Size});
            this.Version = "15.2";
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private DevExpress.DataAccess.Sql.SqlDataSource sqlDataSource1;
        private DevExpress.XtraReports.UI.XRPageInfo xrPageInfo2;
        private DevExpress.XtraReports.UI.XRPageInfo xrPageInfo1;
        //private DevExpress.XtraTabbedMdi.XtraTabbedMdiManager xtraTabbedMdiManager1;
        //private DevExpress.XtraBars.Ribbon.GalleryDropDown galleryDropDown1;
        //private DevExpress.XtraBars.Ribbon.GalleryDropDown galleryDropDown2;
        private DevExpress.XtraReports.UI.FormattingRule formattingRule1;
        private DevExpress.XtraReports.Parameters.Parameter ItemCode;
        private DevExpress.XtraReports.Parameters.Parameter Color;
        private DevExpress.XtraReports.Parameters.Parameter Class;
        private DevExpress.XtraReports.Parameters.Parameter Size;
        private DevExpress.XtraReports.Parameters.Parameter ItemCategory;
        private DevExpress.XtraReports.Parameters.Parameter ProductCategory;
        private DevExpress.XtraReports.Parameters.Parameter ProductSubCategory;
        private DevExpress.XtraReports.UI.XRPivotGrid xrPivotGrid1;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField fieldHEADER1;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField fieldItemCode1;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField fieldColorCode1;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField fieldClassCode1;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField fieldSizeCode1;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField fieldOrd1;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField fieldQty1;
        private DevExpress.XtraReports.UI.XRLabel xrLabel15;
        private DevExpress.XtraReports.UI.XRLabel xrLabel18;
        private DevExpress.XtraReports.UI.GroupHeaderBand GroupHeader1;
        private DevExpress.XtraReports.UI.XRLabel xrLabel2;
    }
}
