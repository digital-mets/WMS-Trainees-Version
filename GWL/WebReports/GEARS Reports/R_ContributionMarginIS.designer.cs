namespace GWL.WebReports.GEARS_Reports
{
    partial class R_ContributionMarginIS
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
            DevExpress.DataAccess.Sql.StoredProcQuery storedProcQuery2 = new DevExpress.DataAccess.Sql.StoredProcQuery();
            DevExpress.DataAccess.Sql.QueryParameter queryParameter3 = new DevExpress.DataAccess.Sql.QueryParameter();
            DevExpress.DataAccess.Sql.QueryParameter queryParameter4 = new DevExpress.DataAccess.Sql.QueryParameter();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(R_ContributionMarginIS));
            DevExpress.XtraReports.UI.XRSummary xrSummary1 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.Parameters.StaticListLookUpSettings staticListLookUpSettings1 = new DevExpress.XtraReports.Parameters.StaticListLookUpSettings();
            this.sqlDataSource1 = new DevExpress.DataAccess.Sql.SqlDataSource(this.components);
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.xrLabel3 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrPageInfo1 = new DevExpress.XtraReports.UI.XRPageInfo();
            this.xrPageInfo2 = new DevExpress.XtraReports.UI.XRPageInfo();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
            this.xrLabel15 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel8 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel18 = new DevExpress.XtraReports.UI.XRLabel();
            this.directorySearcher1 = new System.DirectoryServices.DirectorySearcher();
            this.calculatedField1 = new DevExpress.XtraReports.UI.CalculatedField();
            this.GroupHeader1 = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.xrPivotGrid2 = new DevExpress.XtraReports.UI.XRPivotGrid();
            this.xrPivotGridField1 = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.xrPivotGridField2 = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.xrPivotGridField3 = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.xrPivotGridField4 = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.xrPivotGridField5 = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.xrPivotGridField6 = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.xrPivotGridField7 = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.xrPivotGridField8 = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.xrPivotGridField9 = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.xrPivotGridField10 = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.xrPivotGridField11 = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
            this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
            this.PostBack = new DevExpress.XtraReports.Parameters.Parameter();
            this.Year = new DevExpress.XtraReports.Parameters.Parameter();
            this.Reportstyle = new DevExpress.XtraReports.Parameters.Parameter();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // sqlDataSource1
            // 
            this.sqlDataSource1.ConnectionName = "GEARS-METSITConnectionString";
            this.sqlDataSource1.Name = "sqlDataSource1";
            storedProcQuery1.Name = "Values";
            queryParameter1.Name = "@year";
            queryParameter1.Type = typeof(DevExpress.DataAccess.Expression);
            queryParameter1.Value = new DevExpress.DataAccess.Expression("[Parameters.Year]", typeof(int));
            queryParameter2.Name = "@Reportstyle";
            queryParameter2.Type = typeof(DevExpress.DataAccess.Expression);
            queryParameter2.Value = new DevExpress.DataAccess.Expression("[Parameters.Reportstyle]", typeof(string));
            storedProcQuery1.Parameters.Add(queryParameter1);
            storedProcQuery1.Parameters.Add(queryParameter2);
            storedProcQuery1.StoredProcName = "sp_report_ContributionMarginIS";
            customSqlQuery1.Name = "CompanyName";
            customSqlQuery1.Sql = "SELECT Value FROM IT.SystemSettings WHERE Code = \'COMPNAME\'";
            customSqlQuery2.Name = "CompanyAddress";
            customSqlQuery2.Sql = "SELECT Value FROM IT.SystemSettings WHERE Code = \'COMPADDR\'";
            storedProcQuery2.Name = "sp_Report_ContributionMargin";
            queryParameter3.Name = "@year";
            queryParameter3.Type = typeof(DevExpress.DataAccess.Expression);
            queryParameter3.Value = new DevExpress.DataAccess.Expression("[Parameters.Year]", typeof(int));
            queryParameter4.Name = "@Reportstyle";
            queryParameter4.Type = typeof(DevExpress.DataAccess.Expression);
            queryParameter4.Value = new DevExpress.DataAccess.Expression("[Parameters.Reportstyle]", typeof(string));
            storedProcQuery2.Parameters.Add(queryParameter3);
            storedProcQuery2.Parameters.Add(queryParameter4);
            storedProcQuery2.StoredProcName = "sp_report_ContributionMarginIS";
            this.sqlDataSource1.Queries.AddRange(new DevExpress.DataAccess.Sql.SqlQuery[] {
            storedProcQuery1,
            customSqlQuery1,
            customSqlQuery2,
            storedProcQuery2});
            this.sqlDataSource1.ResultSchemaSerializable = resources.GetString("sqlDataSource1.ResultSchemaSerializable");
            // 
            // Detail
            // 
            this.Detail.Dpi = 100F;
            this.Detail.Expanded = false;
            this.Detail.HeightF = 102.0833F;
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
            this.xrLabel3.SizeF = new System.Drawing.SizeF(70F, 20F);
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
            this.xrPageInfo1.LocationFloat = new DevExpress.Utils.PointFloat(69.99992F, 23.25001F);
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
            this.xrPageInfo2.LocationFloat = new DevExpress.Utils.PointFloat(1561.678F, 23.25001F);
            this.xrPageInfo2.Name = "xrPageInfo2";
            this.xrPageInfo2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrPageInfo2.SizeF = new System.Drawing.SizeF(91.53033F, 16.75F);
            this.xrPageInfo2.StylePriority.UseFont = false;
            this.xrPageInfo2.StylePriority.UseTextAlignment = false;
            this.xrPageInfo2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            // 
            // BottomMargin
            // 
            this.BottomMargin.Dpi = 100F;
            this.BottomMargin.HeightF = 34F;
            this.BottomMargin.Name = "BottomMargin";
            this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // ReportHeader
            // 
            this.ReportHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel15,
            this.xrLabel8,
            this.xrLabel18});
            this.ReportHeader.Dpi = 100F;
            this.ReportHeader.HeightF = 99.79172F;
            this.ReportHeader.Name = "ReportHeader";
            // 
            // xrLabel15
            // 
            this.xrLabel15.Dpi = 100F;
            this.xrLabel15.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel15.LocationFloat = new DevExpress.Utils.PointFloat(10.00004F, 33.00006F);
            this.xrLabel15.Name = "xrLabel15";
            this.xrLabel15.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel15.SizeF = new System.Drawing.SizeF(1643.208F, 23.00001F);
            this.xrLabel15.StylePriority.UseFont = false;
            this.xrLabel15.StylePriority.UseTextAlignment = false;
            this.xrLabel15.Text = "Contribution Margin Income Statement";
            this.xrLabel15.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel8
            // 
            this.xrLabel8.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "Values.HeaderReport")});
            this.xrLabel8.Dpi = 100F;
            this.xrLabel8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel8.LocationFloat = new DevExpress.Utils.PointFloat(10.00023F, 56.00007F);
            this.xrLabel8.Multiline = true;
            this.xrLabel8.Name = "xrLabel8";
            this.xrLabel8.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel8.SizeF = new System.Drawing.SizeF(1643.208F, 23F);
            this.xrLabel8.StylePriority.UseFont = false;
            this.xrLabel8.StylePriority.UseTextAlignment = false;
            this.xrLabel8.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // xrLabel18
            // 
            this.xrLabel18.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "CompanyName.Value")});
            this.xrLabel18.Dpi = 100F;
            this.xrLabel18.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel18.LocationFloat = new DevExpress.Utils.PointFloat(9.999974F, 10.00001F);
            this.xrLabel18.Name = "xrLabel18";
            this.xrLabel18.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel18.SizeF = new System.Drawing.SizeF(1643.208F, 23F);
            this.xrLabel18.StylePriority.UseFont = false;
            this.xrLabel18.StylePriority.UseTextAlignment = false;
            this.xrLabel18.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // directorySearcher1
            // 
            this.directorySearcher1.ClientTimeout = System.TimeSpan.Parse("-00:00:01");
            this.directorySearcher1.ServerPageTimeLimit = System.TimeSpan.Parse("-00:00:01");
            this.directorySearcher1.ServerTimeLimit = System.TimeSpan.Parse("-00:00:01");
            // 
            // calculatedField1
            // 
            this.calculatedField1.DataMember = "Values";
            this.calculatedField1.Name = "calculatedField1";
            // 
            // GroupHeader1
            // 
            this.GroupHeader1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPivotGrid2});
            this.GroupHeader1.Dpi = 100F;
            this.GroupHeader1.GroupFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});
            this.GroupHeader1.HeightF = 72.29166F;
            this.GroupHeader1.Name = "GroupHeader1";
            // 
            // xrPivotGrid2
            // 
            this.xrPivotGrid2.DataMember = "Values";
            this.xrPivotGrid2.DataSource = this.sqlDataSource1;
            this.xrPivotGrid2.Dpi = 100F;
            this.xrPivotGrid2.Fields.AddRange(new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField[] {
            this.xrPivotGridField1,
            this.xrPivotGridField2,
            this.xrPivotGridField3,
            this.xrPivotGridField4,
            this.xrPivotGridField5,
            this.xrPivotGridField6,
            this.xrPivotGridField7,
            this.xrPivotGridField8,
            this.xrPivotGridField9,
            this.xrPivotGridField10,
            this.xrPivotGridField11});
            this.xrPivotGrid2.LocationFloat = new DevExpress.Utils.PointFloat(10.00023F, 0F);
            this.xrPivotGrid2.Name = "xrPivotGrid2";
            this.xrPivotGrid2.OptionsPrint.FilterSeparatorBarPadding = 3;
            this.xrPivotGrid2.OptionsPrint.PrintColumnHeaders = DevExpress.Utils.DefaultBoolean.False;
            this.xrPivotGrid2.OptionsPrint.PrintDataHeaders = DevExpress.Utils.DefaultBoolean.False;
            this.xrPivotGrid2.OptionsPrint.PrintFilterHeaders = DevExpress.Utils.DefaultBoolean.False;
            this.xrPivotGrid2.OptionsView.ShowColumnGrandTotalHeader = false;
            this.xrPivotGrid2.OptionsView.ShowColumnGrandTotals = false;
            this.xrPivotGrid2.OptionsView.ShowColumnTotals = false;
            this.xrPivotGrid2.OptionsView.ShowRowGrandTotalHeader = false;
            this.xrPivotGrid2.OptionsView.ShowRowGrandTotals = false;
            this.xrPivotGrid2.OptionsView.ShowRowTotals = false;
            this.xrPivotGrid2.SizeF = new System.Drawing.SizeF(428.5417F, 72.29166F);
            // 
            // xrPivotGridField1
            // 
            this.xrPivotGridField1.AreaIndex = 0;
            this.xrPivotGridField1.FieldName = "calculatedField1";
            this.xrPivotGridField1.Name = "xrPivotGridField1";
            // 
            // xrPivotGridField2
            // 
            this.xrPivotGridField2.AreaIndex = 1;
            this.xrPivotGridField2.FieldName = "Report";
            this.xrPivotGridField2.Name = "xrPivotGridField2";
            // 
            // xrPivotGridField3
            // 
            this.xrPivotGridField3.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.xrPivotGridField3.AreaIndex = 0;
            this.xrPivotGridField3.FieldName = "Orderby";
            this.xrPivotGridField3.MinWidth = 0;
            this.xrPivotGridField3.Name = "xrPivotGridField3";
            this.xrPivotGridField3.Width = 0;
            // 
            // xrPivotGridField4
            // 
            this.xrPivotGridField4.AreaIndex = 2;
            this.xrPivotGridField4.FieldName = "Title1";
            this.xrPivotGridField4.Name = "xrPivotGridField4";
            // 
            // xrPivotGridField5
            // 
            this.xrPivotGridField5.AreaIndex = 3;
            this.xrPivotGridField5.FieldName = "Title2";
            this.xrPivotGridField5.Name = "xrPivotGridField5";
            // 
            // xrPivotGridField6
            // 
            this.xrPivotGridField6.AreaIndex = 4;
            this.xrPivotGridField6.FieldName = "Title3";
            this.xrPivotGridField6.Name = "xrPivotGridField6";
            // 
            // xrPivotGridField7
            // 
            this.xrPivotGridField7.Appearance.FieldValue.BackColor = System.Drawing.Color.White;
            this.xrPivotGridField7.Appearance.FieldValue.BorderColor = System.Drawing.Color.White;
            this.xrPivotGridField7.Appearance.FieldValue.ForeColor = System.Drawing.Color.White;
            this.xrPivotGridField7.Area = DevExpress.XtraPivotGrid.PivotArea.ColumnArea;
            this.xrPivotGridField7.AreaIndex = 0;
            this.xrPivotGridField7.FieldName = "Monthgroup";
            this.xrPivotGridField7.Name = "xrPivotGridField7";
            // 
            // xrPivotGridField8
            // 
            this.xrPivotGridField8.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea;
            this.xrPivotGridField8.AreaIndex = 0;
            this.xrPivotGridField8.CellFormat.FormatString = "{0:N}";
            this.xrPivotGridField8.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.xrPivotGridField8.FieldName = "Amount";
            this.xrPivotGridField8.Name = "xrPivotGridField8";
            this.xrPivotGridField8.TotalCellFormat.FormatString = "{0:N}";
            this.xrPivotGridField8.TotalCellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.xrPivotGridField8.ValueFormat.FormatString = "{0:N}";
            this.xrPivotGridField8.ValueFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            // 
            // xrPivotGridField9
            // 
            this.xrPivotGridField9.AreaIndex = 5;
            this.xrPivotGridField9.FieldName = "Accountcode";
            this.xrPivotGridField9.Name = "xrPivotGridField9";
            // 
            // xrPivotGridField10
            // 
            this.xrPivotGridField10.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.xrPivotGridField10.AreaIndex = 1;
            this.xrPivotGridField10.FieldName = "Title";
            this.xrPivotGridField10.Name = "xrPivotGridField10";
            this.xrPivotGridField10.Width = 314;
            // 
            // xrPivotGridField11
            // 
            this.xrPivotGridField11.Appearance.Cell.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.xrPivotGridField11.Appearance.Cell.TextVerticalAlignment = DevExpress.Utils.VertAlignment.Center;
            this.xrPivotGridField11.Appearance.CustomTotalCell.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.xrPivotGridField11.Appearance.CustomTotalCell.TextVerticalAlignment = DevExpress.Utils.VertAlignment.Center;
            this.xrPivotGridField11.Appearance.FieldValue.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.xrPivotGridField11.Appearance.FieldValue.TextVerticalAlignment = DevExpress.Utils.VertAlignment.Center;
            this.xrPivotGridField11.Area = DevExpress.XtraPivotGrid.PivotArea.ColumnArea;
            this.xrPivotGridField11.AreaIndex = 1;
            this.xrPivotGridField11.FieldName = "Month";
            this.xrPivotGridField11.Name = "xrPivotGridField11";
            this.xrPivotGridField11.Width = 97;
            // 
            // ReportFooter
            // 
            this.ReportFooter.Dpi = 100F;
            this.ReportFooter.HeightF = 101.125F;
            this.ReportFooter.Name = "ReportFooter";
            // 
            // PageHeader
            // 
            this.PageHeader.Dpi = 100F;
            this.PageHeader.HeightF = 25F;
            this.PageHeader.Name = "PageHeader";
            // 
            // PostBack
            // 
            this.PostBack.Description = "PostBack";
            this.PostBack.Name = "PostBack";
            this.PostBack.Type = typeof(bool);
            this.PostBack.ValueInfo = "True";
            this.PostBack.Visible = false;
            // 
            // Year
            // 
            this.Year.Description = "Year";
            this.Year.Name = "Year";
            this.Year.Type = typeof(int);
            this.Year.ValueInfo = "2019";
            // 
            // Reportstyle
            // 
            staticListLookUpSettings1.LookUpValues.Add(new DevExpress.XtraReports.Parameters.LookUpValue("Summary", "Summary"));
            staticListLookUpSettings1.LookUpValues.Add(new DevExpress.XtraReports.Parameters.LookUpValue("Detail", "Detail"));
            this.Reportstyle.LookUpSettings = staticListLookUpSettings1;
            this.Reportstyle.Name = "Reportstyle";
            // 
            // R_ContributionMarginIS
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.ReportHeader,
            this.GroupHeader1,
            this.ReportFooter,
            this.PageHeader});
            this.CalculatedFields.AddRange(new DevExpress.XtraReports.UI.CalculatedField[] {
            this.calculatedField1});
            this.ComponentStorage.AddRange(new System.ComponentModel.IComponent[] {
            this.sqlDataSource1});
            this.DataMember = "Values";
            this.DataSource = this.sqlDataSource1;
            this.Margins = new System.Drawing.Printing.Margins(9, 8, 40, 34);
            this.PageWidth = 1700;
            this.PaperKind = System.Drawing.Printing.PaperKind.Ledger;
            this.Parameters.AddRange(new DevExpress.XtraReports.Parameters.Parameter[] {
            this.PostBack,
            this.Year,
            this.Reportstyle});
            this.Scripts.OnParametersRequestBeforeShow = "R_RemainingInv2_ParametersRequestBeforeShow";
            this.ScriptsSource = "\r\n";
            this.Version = "15.2";
            this.VerticalContentSplitting = DevExpress.XtraPrinting.VerticalContentSplitting.Smart;
            this.DataSourceDemanded += new System.EventHandler<System.EventArgs>(this.R_RemainingInv_DataSourceDemanded);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private DevExpress.DataAccess.Sql.SqlDataSource sqlDataSource1;
        private DevExpress.XtraReports.UI.XRPageInfo xrPageInfo2;
        //private DevExpress.XtraTabbedMdi.XtraTabbedMdiManager xtraTabbedMdiManager1;
        //private DevExpress.XtraBars.Ribbon.GalleryDropDown galleryDropDown1;
        //private DevExpress.XtraBars.Ribbon.GalleryDropDown galleryDropDown2;
        private DevExpress.XtraReports.UI.ReportHeaderBand ReportHeader;
        //private DevExpress.XtraBars.ToastNotifications.ToastNotificationsManager toastNotificationsManager1;
        private System.DirectoryServices.DirectorySearcher directorySearcher1;
        private DevExpress.XtraReports.UI.XRLabel xrLabel15;
        private DevExpress.XtraReports.UI.XRLabel xrLabel8;
        private DevExpress.XtraReports.UI.XRLabel xrLabel18;
        private DevExpress.XtraReports.UI.CalculatedField calculatedField1;
        private DevExpress.XtraReports.UI.GroupHeaderBand GroupHeader1;
        private DevExpress.XtraReports.UI.ReportFooterBand ReportFooter;
        private DevExpress.XtraReports.UI.XRLabel xrLabel3;
        private DevExpress.XtraReports.UI.XRPageInfo xrPageInfo1;
        private DevExpress.XtraReports.UI.PageHeaderBand PageHeader;
        private DevExpress.XtraReports.Parameters.Parameter PostBack;
        private DevExpress.XtraReports.Parameters.Parameter Year;
        private DevExpress.XtraReports.UI.XRPivotGrid xrPivotGrid2;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField xrPivotGridField1;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField xrPivotGridField2;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField xrPivotGridField3;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField xrPivotGridField4;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField xrPivotGridField5;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField xrPivotGridField6;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField xrPivotGridField7;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField xrPivotGridField8;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField xrPivotGridField9;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField xrPivotGridField10;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField xrPivotGridField11;
        private DevExpress.XtraReports.Parameters.Parameter Reportstyle;
    }
}
