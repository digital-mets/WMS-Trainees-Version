namespace GWL.WebReports.GEARS_Printout
{
    partial class P_CashReplenish
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
            DevExpress.DataAccess.Sql.StoredProcQuery storedProcQuery1 = new DevExpress.DataAccess.Sql.StoredProcQuery();
            DevExpress.DataAccess.Sql.QueryParameter queryParameter1 = new DevExpress.DataAccess.Sql.QueryParameter();
            DevExpress.DataAccess.Sql.QueryParameter queryParameter2 = new DevExpress.DataAccess.Sql.QueryParameter();
            DevExpress.DataAccess.Sql.QueryParameter queryParameter3 = new DevExpress.DataAccess.Sql.QueryParameter();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(P_CashReplenish));
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.xrPageInfo4 = new DevExpress.XtraReports.UI.XRPageInfo();
            this.xrLabel26 = new DevExpress.XtraReports.UI.XRLabel();
            this.DocNumber = new DevExpress.XtraReports.Parameters.Parameter();
            this.xrPageInfo3 = new DevExpress.XtraReports.UI.XRPageInfo();
            this.sqlDataSource1 = new DevExpress.DataAccess.Sql.SqlDataSource(this.components);
            this.UserID = new DevExpress.XtraReports.Parameters.Parameter();
            this.xrLabel6 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel34 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel35 = new DevExpress.XtraReports.UI.XRLabel();
            this.formattingRule1 = new DevExpress.XtraReports.UI.FormattingRule();
            this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
            this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrPivotGrid1 = new DevExpress.XtraReports.UI.XRPivotGrid();
            this.fieldDocdate1 = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.fieldRemarks1 = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.fieldDocnumber1 = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.fieldExpenseDescription1 = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.fieldAmount1 = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.pivotGridField1 = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.PrintNote = new DevExpress.XtraReports.UI.FormattingRule();
            this.IsPrinted = new DevExpress.XtraReports.Parameters.Parameter();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.Detail.HeightF = 24.99999F;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.Detail.StylePriority.UseBorders = false;
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // TopMargin
            // 
            this.TopMargin.HeightF = 38.54166F;
            this.TopMargin.Name = "TopMargin";
            this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // BottomMargin
            // 
            this.BottomMargin.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPageInfo4,
            this.xrLabel26,
            this.xrPageInfo3});
            this.BottomMargin.HeightF = 52.24065F;
            this.BottomMargin.Name = "BottomMargin";
            this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrPageInfo4
            // 
            this.xrPageInfo4.Font = new System.Drawing.Font("Times New Roman", 8F);
            this.xrPageInfo4.Format = "Page {0} of {1}";
            this.xrPageInfo4.LocationFloat = new DevExpress.Utils.PointFloat(463.579F, 13.24998F);
            this.xrPageInfo4.Name = "xrPageInfo4";
            this.xrPageInfo4.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrPageInfo4.SizeF = new System.Drawing.SizeF(586.421F, 16.75F);
            this.xrPageInfo4.StylePriority.UseFont = false;
            this.xrPageInfo4.StylePriority.UseTextAlignment = false;
            this.xrPageInfo4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            // 
            // xrLabel26
            // 
            this.xrLabel26.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding(this.DocNumber, "Text", "")});
            this.xrLabel26.Font = new System.Drawing.Font("Times New Roman", 8F);
            this.xrLabel26.LocationFloat = new DevExpress.Utils.PointFloat(312.0932F, 13.25003F);
            this.xrLabel26.Name = "xrLabel26";
            this.xrLabel26.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel26.SizeF = new System.Drawing.SizeF(151.4859F, 16.75001F);
            this.xrLabel26.StylePriority.UseFont = false;
            this.xrLabel26.StylePriority.UseTextAlignment = false;
            this.xrLabel26.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // DocNumber
            // 
            this.DocNumber.Description = "DocNumber";
            this.DocNumber.Name = "DocNumber";
            // 
            // xrPageInfo3
            // 
            this.xrPageInfo3.Font = new System.Drawing.Font("Times New Roman", 8F);
            this.xrPageInfo3.Format = "Run Date & Time: {0:MMMM dd,yyyy / hh:mm tt}";
            this.xrPageInfo3.LocationFloat = new DevExpress.Utils.PointFloat(10.75012F, 13.24998F);
            this.xrPageInfo3.Name = "xrPageInfo3";
            this.xrPageInfo3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrPageInfo3.PageInfo = DevExpress.XtraPrinting.PageInfo.DateTime;
            this.xrPageInfo3.SizeF = new System.Drawing.SizeF(301.3431F, 16.75F);
            this.xrPageInfo3.StylePriority.UseFont = false;
            // 
            // sqlDataSource1
            // 
            this.sqlDataSource1.ConnectionName = "GEARS-METSITConnectionString";
            this.sqlDataSource1.Name = "sqlDataSource1";
            customSqlQuery1.Name = "CompanyName";
            customSqlQuery1.Sql = "SELECT Value FROM IT.SystemSettings WHERE Code = \'COMPNAME\'";
            storedProcQuery1.Name = "sp_printout_CashReplenishment";
            queryParameter1.Name = "@DocNumber";
            queryParameter1.Type = typeof(DevExpress.DataAccess.Expression);
            queryParameter1.Value = new DevExpress.DataAccess.Expression("[Parameters.DocNumber]", typeof(string));
            queryParameter2.Name = "@UserID";
            queryParameter2.Type = typeof(DevExpress.DataAccess.Expression);
            queryParameter2.Value = new DevExpress.DataAccess.Expression("[Parameters.UserID]", typeof(string));
            queryParameter3.Name = "@IsPrinted";
            queryParameter3.Type = typeof(DevExpress.DataAccess.Expression);
            queryParameter3.Value = new DevExpress.DataAccess.Expression("[Parameters.IsPrinted]", typeof(bool));
            storedProcQuery1.Parameters.Add(queryParameter1);
            storedProcQuery1.Parameters.Add(queryParameter2);
            storedProcQuery1.Parameters.Add(queryParameter3);
            storedProcQuery1.StoredProcName = "sp_printout_CashReplenishment";
            this.sqlDataSource1.Queries.AddRange(new DevExpress.DataAccess.Sql.SqlQuery[] {
            customSqlQuery1,
            storedProcQuery1});
            this.sqlDataSource1.ResultSchemaSerializable = resources.GetString("sqlDataSource1.ResultSchemaSerializable");
            // 
            // UserID
            // 
            this.UserID.Description = "UserID";
            this.UserID.Name = "UserID";
            this.UserID.ValueInfo = "UserID";
            // 
            // xrLabel6
            // 
            this.xrLabel6.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding(this.DocNumber, "Text", "Doc. Number: {0}")});
            this.xrLabel6.Font = new System.Drawing.Font("Arial Narrow", 11F, System.Drawing.FontStyle.Bold);
            this.xrLabel6.LocationFloat = new DevExpress.Utils.PointFloat(20.50005F, 72.60939F);
            this.xrLabel6.Name = "xrLabel6";
            this.xrLabel6.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel6.SizeF = new System.Drawing.SizeF(1029.5F, 23F);
            this.xrLabel6.StylePriority.UseFont = false;
            // 
            // xrLabel34
            // 
            this.xrLabel34.Font = new System.Drawing.Font("Arial Narrow", 16F, System.Drawing.FontStyle.Bold);
            this.xrLabel34.LocationFloat = new DevExpress.Utils.PointFloat(20.50005F, 28.69272F);
            this.xrLabel34.Name = "xrLabel34";
            this.xrLabel34.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel34.SizeF = new System.Drawing.SizeF(1029.5F, 23.00002F);
            this.xrLabel34.StylePriority.UseFont = false;
            this.xrLabel34.StylePriority.UseTextAlignment = false;
            this.xrLabel34.Text = "Cash Replenishment";
            this.xrLabel34.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel35
            // 
            this.xrLabel35.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "CompanyName.Value")});
            this.xrLabel35.Font = new System.Drawing.Font("Arial Narrow", 11F, System.Drawing.FontStyle.Bold);
            this.xrLabel35.LocationFloat = new DevExpress.Utils.PointFloat(20.50005F, 51.69277F);
            this.xrLabel35.Name = "xrLabel35";
            this.xrLabel35.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel35.SizeF = new System.Drawing.SizeF(1029.5F, 20.91663F);
            this.xrLabel35.StylePriority.UseFont = false;
            this.xrLabel35.StylePriority.UseTextAlignment = false;
            this.xrLabel35.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // formattingRule1
            // 
            // 
            // 
            // 
            this.formattingRule1.Formatting.Visible = DevExpress.Utils.DefaultBoolean.False;
            this.formattingRule1.Name = "formattingRule1";
            // 
            // ReportHeader
            // 
            this.ReportHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel2,
            this.xrLabel1,
            this.xrPivotGrid1,
            this.xrLabel35,
            this.xrLabel34,
            this.xrLabel6});
            this.ReportHeader.HeightF = 199.4584F;
            this.ReportHeader.Name = "ReportHeader";
            // 
            // xrLabel2
            // 
            this.xrLabel2.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_printout_CashReplenishment.CRFundSource", "Fund Source: {0}")});
            this.xrLabel2.Font = new System.Drawing.Font("Arial Narrow", 11F, System.Drawing.FontStyle.Bold);
            this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(20.50005F, 118.6094F);
            this.xrLabel2.Name = "xrLabel2";
            this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel2.SizeF = new System.Drawing.SizeF(1029.5F, 23.00001F);
            this.xrLabel2.StylePriority.UseFont = false;
            // 
            // xrLabel1
            // 
            this.xrLabel1.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_printout_CashReplenishment.CRDocDate", "Document Date: {0:MM/dd/yyyy}")});
            this.xrLabel1.Font = new System.Drawing.Font("Arial Narrow", 11F, System.Drawing.FontStyle.Bold);
            this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(20.50005F, 95.60938F);
            this.xrLabel1.Name = "xrLabel1";
            this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel1.SizeF = new System.Drawing.SizeF(1029.5F, 23F);
            this.xrLabel1.StylePriority.UseFont = false;
            // 
            // xrPivotGrid1
            // 
            this.xrPivotGrid1.Appearance.Cell.WordWrap = true;
            this.xrPivotGrid1.Appearance.FieldHeader.BackColor = System.Drawing.Color.White;
            this.xrPivotGrid1.Appearance.FieldHeader.BorderColor = System.Drawing.Color.Black;
            this.xrPivotGrid1.Appearance.FieldHeader.Font = new System.Drawing.Font("Arial Narrow", 9F);
            this.xrPivotGrid1.Appearance.FieldHeader.ForeColor = System.Drawing.Color.Black;
            this.xrPivotGrid1.Appearance.FieldHeader.WordWrap = true;
            this.xrPivotGrid1.Appearance.FieldValue.BackColor = System.Drawing.Color.White;
            this.xrPivotGrid1.Appearance.FieldValue.BorderColor = System.Drawing.Color.Black;
            this.xrPivotGrid1.Appearance.FieldValue.Font = new System.Drawing.Font("Arial Narrow", 9F);
            this.xrPivotGrid1.Appearance.FieldValue.ForeColor = System.Drawing.Color.Black;
            this.xrPivotGrid1.Appearance.FieldValue.WordWrap = true;
            this.xrPivotGrid1.Appearance.FieldValueGrandTotal.Font = new System.Drawing.Font("Arial Narrow", 9F);
            this.xrPivotGrid1.Appearance.FieldValueGrandTotal.WordWrap = true;
            this.xrPivotGrid1.Appearance.FieldValueTotal.Font = new System.Drawing.Font("Arial Narrow", 9F);
            this.xrPivotGrid1.Appearance.FieldValueTotal.WordWrap = true;
            this.xrPivotGrid1.Appearance.GrandTotalCell.BackColor = System.Drawing.Color.White;
            this.xrPivotGrid1.Appearance.GrandTotalCell.BorderColor = System.Drawing.Color.Black;
            this.xrPivotGrid1.Appearance.GrandTotalCell.Font = new System.Drawing.Font("Arial Narrow", 9F);
            this.xrPivotGrid1.Appearance.GrandTotalCell.ForeColor = System.Drawing.Color.Black;
            this.xrPivotGrid1.Appearance.GrandTotalCell.WordWrap = true;
            this.xrPivotGrid1.DataMember = "sp_printout_CashReplenishment";
            this.xrPivotGrid1.DataSource = this.sqlDataSource1;
            this.xrPivotGrid1.Fields.AddRange(new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField[] {
            this.fieldDocdate1,
            this.fieldRemarks1,
            this.fieldDocnumber1,
            this.fieldExpenseDescription1,
            this.fieldAmount1,
            this.pivotGridField1});
            this.xrPivotGrid1.LocationFloat = new DevExpress.Utils.PointFloat(20.50005F, 149.4584F);
            this.xrPivotGrid1.Name = "xrPivotGrid1";
            this.xrPivotGrid1.OptionsPrint.FilterSeparatorBarPadding = 3;
            this.xrPivotGrid1.OptionsPrint.MergeColumnFieldValues = false;
            this.xrPivotGrid1.OptionsPrint.MergeRowFieldValues = false;
            this.xrPivotGrid1.OptionsPrint.PrintColumnHeaders = DevExpress.Utils.DefaultBoolean.False;
            this.xrPivotGrid1.OptionsPrint.PrintDataHeaders = DevExpress.Utils.DefaultBoolean.False;
            this.xrPivotGrid1.OptionsPrint.PrintFilterHeaders = DevExpress.Utils.DefaultBoolean.True;
            this.xrPivotGrid1.OptionsPrint.PrintHeadersOnEveryPage = true;
            this.xrPivotGrid1.OptionsPrint.PrintHorzLines = DevExpress.Utils.DefaultBoolean.True;
            this.xrPivotGrid1.OptionsPrint.PrintRowHeaders = DevExpress.Utils.DefaultBoolean.True;
            this.xrPivotGrid1.OptionsPrint.PrintVertLines = DevExpress.Utils.DefaultBoolean.True;
            this.xrPivotGrid1.OptionsView.ShowColumnHeaders = false;
            this.xrPivotGrid1.OptionsView.ShowColumnTotals = false;
            this.xrPivotGrid1.OptionsView.ShowDataHeaders = false;
            this.xrPivotGrid1.OptionsView.ShowFilterHeaders = false;
            this.xrPivotGrid1.OptionsView.ShowFilterSeparatorBar = false;
            this.xrPivotGrid1.OptionsView.ShowRowTotals = false;
            this.xrPivotGrid1.SizeF = new System.Drawing.SizeF(1029.5F, 50F);
            // 
            // fieldDocdate1
            // 
            this.fieldDocdate1.Appearance.Cell.BackColor = System.Drawing.Color.White;
            this.fieldDocdate1.Appearance.Cell.BorderColor = System.Drawing.Color.Black;
            this.fieldDocdate1.Appearance.Cell.Font = new System.Drawing.Font("Arial Narrow", 8.25F);
            this.fieldDocdate1.Appearance.FieldHeader.BackColor = System.Drawing.Color.White;
            this.fieldDocdate1.Appearance.FieldHeader.BorderColor = System.Drawing.Color.Black;
            this.fieldDocdate1.Appearance.FieldHeader.ForeColor = System.Drawing.Color.Black;
            this.fieldDocdate1.Appearance.FieldHeader.WordWrap = true;
            this.fieldDocdate1.Appearance.FieldValue.BackColor = System.Drawing.Color.White;
            this.fieldDocdate1.Appearance.FieldValue.BorderColor = System.Drawing.Color.Black;
            this.fieldDocdate1.Appearance.FieldValue.ForeColor = System.Drawing.Color.Black;
            this.fieldDocdate1.Appearance.FieldValue.WordWrap = true;
            this.fieldDocdate1.Appearance.FieldValueGrandTotal.BackColor = System.Drawing.Color.White;
            this.fieldDocdate1.Appearance.FieldValueGrandTotal.BorderColor = System.Drawing.Color.Black;
            this.fieldDocdate1.Appearance.FieldValueGrandTotal.ForeColor = System.Drawing.Color.Black;
            this.fieldDocdate1.Appearance.FieldValueGrandTotal.WordWrap = true;
            this.fieldDocdate1.Appearance.FieldValueTotal.BackColor = System.Drawing.Color.White;
            this.fieldDocdate1.Appearance.FieldValueTotal.BorderColor = System.Drawing.Color.Black;
            this.fieldDocdate1.Appearance.FieldValueTotal.ForeColor = System.Drawing.Color.Black;
            this.fieldDocdate1.Appearance.GrandTotalCell.BackColor = System.Drawing.Color.White;
            this.fieldDocdate1.Appearance.GrandTotalCell.BorderColor = System.Drawing.Color.Black;
            this.fieldDocdate1.Appearance.GrandTotalCell.ForeColor = System.Drawing.Color.Black;
            this.fieldDocdate1.Appearance.TotalCell.BackColor = System.Drawing.Color.White;
            this.fieldDocdate1.Appearance.TotalCell.BorderColor = System.Drawing.Color.Black;
            this.fieldDocdate1.Appearance.TotalCell.ForeColor = System.Drawing.Color.Black;
            this.fieldDocdate1.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.fieldDocdate1.AreaIndex = 1;
            this.fieldDocdate1.Caption = "DOCDATE";
            this.fieldDocdate1.CellFormat.FormatString = "d";
            this.fieldDocdate1.CellFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.fieldDocdate1.FieldName = "Docdate";
            this.fieldDocdate1.Name = "fieldDocdate1";
            this.fieldDocdate1.SortMode = DevExpress.XtraPivotGrid.PivotSortMode.None;
            this.fieldDocdate1.ValueFormat.FormatString = "MM/dd/yyyy";
            this.fieldDocdate1.ValueFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.fieldDocdate1.Width = 80;
            // 
            // fieldRemarks1
            // 
            this.fieldRemarks1.Appearance.Cell.BackColor = System.Drawing.Color.White;
            this.fieldRemarks1.Appearance.Cell.BorderColor = System.Drawing.Color.Black;
            this.fieldRemarks1.Appearance.Cell.Font = new System.Drawing.Font("Arial Narrow", 8.25F);
            this.fieldRemarks1.Appearance.FieldHeader.BackColor = System.Drawing.Color.White;
            this.fieldRemarks1.Appearance.FieldHeader.BorderColor = System.Drawing.Color.Black;
            this.fieldRemarks1.Appearance.FieldHeader.ForeColor = System.Drawing.Color.Black;
            this.fieldRemarks1.Appearance.FieldHeader.WordWrap = true;
            this.fieldRemarks1.Appearance.FieldValue.BackColor = System.Drawing.Color.White;
            this.fieldRemarks1.Appearance.FieldValue.BorderColor = System.Drawing.Color.Black;
            this.fieldRemarks1.Appearance.FieldValue.ForeColor = System.Drawing.Color.Black;
            this.fieldRemarks1.Appearance.FieldValue.WordWrap = true;
            this.fieldRemarks1.Appearance.FieldValueGrandTotal.BackColor = System.Drawing.Color.White;
            this.fieldRemarks1.Appearance.FieldValueGrandTotal.BorderColor = System.Drawing.Color.Black;
            this.fieldRemarks1.Appearance.FieldValueGrandTotal.ForeColor = System.Drawing.Color.Black;
            this.fieldRemarks1.Appearance.FieldValueGrandTotal.WordWrap = true;
            this.fieldRemarks1.Appearance.FieldValueTotal.BackColor = System.Drawing.Color.White;
            this.fieldRemarks1.Appearance.FieldValueTotal.BorderColor = System.Drawing.Color.Black;
            this.fieldRemarks1.Appearance.FieldValueTotal.ForeColor = System.Drawing.Color.Black;
            this.fieldRemarks1.Appearance.FieldValueTotal.WordWrap = true;
            this.fieldRemarks1.Appearance.GrandTotalCell.BackColor = System.Drawing.Color.White;
            this.fieldRemarks1.Appearance.GrandTotalCell.BorderColor = System.Drawing.Color.Black;
            this.fieldRemarks1.Appearance.GrandTotalCell.ForeColor = System.Drawing.Color.Black;
            this.fieldRemarks1.Appearance.GrandTotalCell.WordWrap = true;
            this.fieldRemarks1.Appearance.TotalCell.BackColor = System.Drawing.Color.White;
            this.fieldRemarks1.Appearance.TotalCell.BorderColor = System.Drawing.Color.Black;
            this.fieldRemarks1.Appearance.TotalCell.ForeColor = System.Drawing.Color.Black;
            this.fieldRemarks1.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.fieldRemarks1.AreaIndex = 2;
            this.fieldRemarks1.Caption = "REMARKS";
            this.fieldRemarks1.FieldName = "Remarks";
            this.fieldRemarks1.Name = "fieldRemarks1";
            this.fieldRemarks1.SortMode = DevExpress.XtraPivotGrid.PivotSortMode.None;
            // 
            // fieldDocnumber1
            // 
            this.fieldDocnumber1.Appearance.Cell.BackColor = System.Drawing.Color.White;
            this.fieldDocnumber1.Appearance.Cell.BorderColor = System.Drawing.Color.Black;
            this.fieldDocnumber1.Appearance.Cell.Font = new System.Drawing.Font("Times New Roman", 8.25F);
            this.fieldDocnumber1.Appearance.FieldHeader.BackColor = System.Drawing.Color.White;
            this.fieldDocnumber1.Appearance.FieldHeader.BorderColor = System.Drawing.Color.Black;
            this.fieldDocnumber1.Appearance.FieldHeader.ForeColor = System.Drawing.Color.Black;
            this.fieldDocnumber1.Appearance.FieldHeader.WordWrap = true;
            this.fieldDocnumber1.Appearance.FieldValue.BackColor = System.Drawing.Color.White;
            this.fieldDocnumber1.Appearance.FieldValue.BorderColor = System.Drawing.Color.Black;
            this.fieldDocnumber1.Appearance.FieldValue.ForeColor = System.Drawing.Color.Black;
            this.fieldDocnumber1.Appearance.FieldValue.WordWrap = true;
            this.fieldDocnumber1.Appearance.FieldValueGrandTotal.BackColor = System.Drawing.Color.White;
            this.fieldDocnumber1.Appearance.FieldValueGrandTotal.BorderColor = System.Drawing.Color.Black;
            this.fieldDocnumber1.Appearance.FieldValueGrandTotal.ForeColor = System.Drawing.Color.Black;
            this.fieldDocnumber1.Appearance.FieldValueGrandTotal.WordWrap = true;
            this.fieldDocnumber1.Appearance.FieldValueTotal.BackColor = System.Drawing.Color.White;
            this.fieldDocnumber1.Appearance.FieldValueTotal.BorderColor = System.Drawing.Color.Black;
            this.fieldDocnumber1.Appearance.FieldValueTotal.ForeColor = System.Drawing.Color.Black;
            this.fieldDocnumber1.Appearance.FieldValueTotal.WordWrap = true;
            this.fieldDocnumber1.Appearance.GrandTotalCell.BackColor = System.Drawing.Color.White;
            this.fieldDocnumber1.Appearance.GrandTotalCell.BorderColor = System.Drawing.Color.Black;
            this.fieldDocnumber1.Appearance.GrandTotalCell.ForeColor = System.Drawing.Color.Black;
            this.fieldDocnumber1.Appearance.GrandTotalCell.WordWrap = true;
            this.fieldDocnumber1.Appearance.TotalCell.BackColor = System.Drawing.Color.White;
            this.fieldDocnumber1.Appearance.TotalCell.BorderColor = System.Drawing.Color.Black;
            this.fieldDocnumber1.Appearance.TotalCell.ForeColor = System.Drawing.Color.Black;
            this.fieldDocnumber1.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.fieldDocnumber1.AreaIndex = 0;
            this.fieldDocnumber1.Caption = "DOCNUMBER";
            this.fieldDocnumber1.FieldName = "Docnumber";
            this.fieldDocnumber1.Name = "fieldDocnumber1";
            this.fieldDocnumber1.SortMode = DevExpress.XtraPivotGrid.PivotSortMode.None;
            // 
            // fieldExpenseDescription1
            // 
            this.fieldExpenseDescription1.Appearance.FieldHeader.BackColor = System.Drawing.Color.White;
            this.fieldExpenseDescription1.Appearance.FieldHeader.BorderColor = System.Drawing.Color.Black;
            this.fieldExpenseDescription1.Appearance.FieldHeader.ForeColor = System.Drawing.Color.Black;
            this.fieldExpenseDescription1.Appearance.FieldHeader.WordWrap = true;
            this.fieldExpenseDescription1.Appearance.FieldValue.BackColor = System.Drawing.Color.White;
            this.fieldExpenseDescription1.Appearance.FieldValue.BorderColor = System.Drawing.Color.Black;
            this.fieldExpenseDescription1.Appearance.FieldValue.ForeColor = System.Drawing.Color.Black;
            this.fieldExpenseDescription1.Appearance.FieldValue.WordWrap = true;
            this.fieldExpenseDescription1.Appearance.FieldValueGrandTotal.BackColor = System.Drawing.Color.White;
            this.fieldExpenseDescription1.Appearance.FieldValueGrandTotal.BorderColor = System.Drawing.Color.Black;
            this.fieldExpenseDescription1.Appearance.FieldValueGrandTotal.ForeColor = System.Drawing.Color.Black;
            this.fieldExpenseDescription1.Appearance.FieldValueGrandTotal.WordWrap = true;
            this.fieldExpenseDescription1.Appearance.FieldValueTotal.BackColor = System.Drawing.Color.White;
            this.fieldExpenseDescription1.Appearance.FieldValueTotal.BorderColor = System.Drawing.Color.Black;
            this.fieldExpenseDescription1.Appearance.FieldValueTotal.ForeColor = System.Drawing.Color.Black;
            this.fieldExpenseDescription1.Appearance.FieldValueTotal.WordWrap = true;
            this.fieldExpenseDescription1.Appearance.GrandTotalCell.BackColor = System.Drawing.Color.White;
            this.fieldExpenseDescription1.Appearance.GrandTotalCell.BorderColor = System.Drawing.Color.Black;
            this.fieldExpenseDescription1.Appearance.GrandTotalCell.ForeColor = System.Drawing.Color.Black;
            this.fieldExpenseDescription1.Appearance.GrandTotalCell.WordWrap = true;
            this.fieldExpenseDescription1.Appearance.TotalCell.BackColor = System.Drawing.Color.White;
            this.fieldExpenseDescription1.Appearance.TotalCell.BorderColor = System.Drawing.Color.Black;
            this.fieldExpenseDescription1.Appearance.TotalCell.ForeColor = System.Drawing.Color.Black;
            this.fieldExpenseDescription1.Area = DevExpress.XtraPivotGrid.PivotArea.ColumnArea;
            this.fieldExpenseDescription1.AreaIndex = 0;
            this.fieldExpenseDescription1.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.fieldExpenseDescription1.FieldName = "ExpenseDescription";
            this.fieldExpenseDescription1.Name = "fieldExpenseDescription1";
            this.fieldExpenseDescription1.Options.AllowDrag = DevExpress.Utils.DefaultBoolean.False;
            this.fieldExpenseDescription1.Options.AllowDragInCustomizationForm = DevExpress.Utils.DefaultBoolean.False;
            this.fieldExpenseDescription1.Options.AllowExpand = DevExpress.Utils.DefaultBoolean.True;
            this.fieldExpenseDescription1.Options.ShowCustomTotals = false;
            this.fieldExpenseDescription1.Options.ShowGrandTotal = false;
            this.fieldExpenseDescription1.Options.ShowTotals = false;
            this.fieldExpenseDescription1.SortMode = DevExpress.XtraPivotGrid.PivotSortMode.None;
            this.fieldExpenseDescription1.ValueFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.fieldExpenseDescription1.Width = 50;
            // 
            // fieldAmount1
            // 
            this.fieldAmount1.Appearance.Cell.BackColor = System.Drawing.Color.White;
            this.fieldAmount1.Appearance.Cell.BorderColor = System.Drawing.Color.Black;
            this.fieldAmount1.Appearance.Cell.Font = new System.Drawing.Font("Arial Narrow", 8.25F);
            this.fieldAmount1.Appearance.FieldHeader.BackColor = System.Drawing.Color.White;
            this.fieldAmount1.Appearance.FieldHeader.BorderColor = System.Drawing.Color.Black;
            this.fieldAmount1.Appearance.FieldHeader.ForeColor = System.Drawing.Color.Black;
            this.fieldAmount1.Appearance.FieldHeader.WordWrap = true;
            this.fieldAmount1.Appearance.FieldValue.BackColor = System.Drawing.Color.White;
            this.fieldAmount1.Appearance.FieldValue.BorderColor = System.Drawing.Color.Black;
            this.fieldAmount1.Appearance.FieldValue.ForeColor = System.Drawing.Color.Black;
            this.fieldAmount1.Appearance.FieldValue.WordWrap = true;
            this.fieldAmount1.Appearance.FieldValueGrandTotal.BackColor = System.Drawing.Color.White;
            this.fieldAmount1.Appearance.FieldValueGrandTotal.BorderColor = System.Drawing.Color.Black;
            this.fieldAmount1.Appearance.FieldValueGrandTotal.ForeColor = System.Drawing.Color.Black;
            this.fieldAmount1.Appearance.FieldValueGrandTotal.WordWrap = true;
            this.fieldAmount1.Appearance.FieldValueTotal.BackColor = System.Drawing.Color.White;
            this.fieldAmount1.Appearance.FieldValueTotal.BorderColor = System.Drawing.Color.Black;
            this.fieldAmount1.Appearance.FieldValueTotal.ForeColor = System.Drawing.Color.Black;
            this.fieldAmount1.Appearance.FieldValueTotal.WordWrap = true;
            this.fieldAmount1.Appearance.GrandTotalCell.BackColor = System.Drawing.Color.White;
            this.fieldAmount1.Appearance.GrandTotalCell.BorderColor = System.Drawing.Color.Black;
            this.fieldAmount1.Appearance.GrandTotalCell.ForeColor = System.Drawing.Color.Black;
            this.fieldAmount1.Appearance.GrandTotalCell.WordWrap = true;
            this.fieldAmount1.Appearance.TotalCell.BackColor = System.Drawing.Color.White;
            this.fieldAmount1.Appearance.TotalCell.BorderColor = System.Drawing.Color.Black;
            this.fieldAmount1.Appearance.TotalCell.ForeColor = System.Drawing.Color.Black;
            this.fieldAmount1.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea;
            this.fieldAmount1.AreaIndex = 0;
            this.fieldAmount1.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.fieldAmount1.FieldName = "Amount";
            this.fieldAmount1.Name = "fieldAmount1";
            this.fieldAmount1.SortMode = DevExpress.XtraPivotGrid.PivotSortMode.None;
            this.fieldAmount1.ValueFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.fieldAmount1.Width = 50;
            // 
            // pivotGridField1
            // 
            this.pivotGridField1.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.pivotGridField1.AreaIndex = 3;
            this.pivotGridField1.Caption = "CostCenter";
            this.pivotGridField1.FieldName = "CostCenterDesc";
            this.pivotGridField1.Name = "pivotGridField1";
            // 
            // PrintNote
            // 
            this.PrintNote.Condition = "[Parameters.IsPrinted] == False";
            // 
            // 
            // 
            this.PrintNote.Formatting.Visible = DevExpress.Utils.DefaultBoolean.False;
            this.PrintNote.Name = "PrintNote";
            // 
            // IsPrinted
            // 
            this.IsPrinted.Description = "IsPrinted";
            this.IsPrinted.Name = "IsPrinted";
            this.IsPrinted.Type = typeof(bool);
            this.IsPrinted.ValueInfo = "False";
            this.IsPrinted.Visible = false;
            // 
            // P_CashReplenish
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.ReportHeader});
            this.ComponentStorage.AddRange(new System.ComponentModel.IComponent[] {
            this.sqlDataSource1});
            this.DataMember = "sp_printout_CashReplenishment";
            this.DataSource = this.sqlDataSource1;
            this.FilterString = "[DocNumber] = ?DocNumber";
            this.FormattingRuleSheet.AddRange(new DevExpress.XtraReports.UI.FormattingRule[] {
            this.formattingRule1,
            this.PrintNote});
            this.Landscape = true;
            this.Margins = new System.Drawing.Printing.Margins(20, 20, 39, 52);
            this.PageHeight = 850;
            this.PageWidth = 1100;
            this.Parameters.AddRange(new DevExpress.XtraReports.Parameters.Parameter[] {
            this.DocNumber,
            this.UserID,
            this.IsPrinted});
            this.RequestParameters = false;
            this.Scripts.OnAfterPrint = "P_CashReplenish_AfterPrint";
            this.ScriptsSource = resources.GetString("$this.ScriptsSource");
            this.Version = "15.1";
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private DevExpress.DataAccess.Sql.SqlDataSource sqlDataSource1;
        private DevExpress.XtraReports.Parameters.Parameter DocNumber;
        private DevExpress.XtraReports.Parameters.Parameter UserID;
        private DevExpress.XtraReports.UI.XRLabel xrLabel34;
        private DevExpress.XtraReports.UI.XRLabel xrLabel35;
        private DevExpress.XtraReports.UI.XRLabel xrLabel6;
        private DevExpress.XtraReports.UI.FormattingRule formattingRule1;
        private DevExpress.XtraReports.UI.ReportHeaderBand ReportHeader;
        private DevExpress.XtraReports.Parameters.Parameter IsPrinted;
        private DevExpress.XtraReports.UI.FormattingRule PrintNote;
        private DevExpress.XtraReports.UI.XRPageInfo xrPageInfo4;
        private DevExpress.XtraReports.UI.XRLabel xrLabel26;
        private DevExpress.XtraReports.UI.XRPageInfo xrPageInfo3;
        private DevExpress.XtraReports.UI.XRPivotGrid xrPivotGrid1;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField fieldDocdate1;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField fieldRemarks1;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField fieldDocnumber1;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField fieldExpenseDescription1;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField fieldAmount1;
        private DevExpress.XtraReports.UI.XRLabel xrLabel2;
        private DevExpress.XtraReports.UI.XRLabel xrLabel1;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField pivotGridField1;
    }
}
