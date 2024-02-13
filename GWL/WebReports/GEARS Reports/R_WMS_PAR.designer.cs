namespace GWL.WebReports.GEARS_Reports
{
    partial class R_WMS_PAR

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
            DevExpress.DataAccess.Sql.CustomSqlQuery customSqlQuery1 = new DevExpress.DataAccess.Sql.CustomSqlQuery();
            DevExpress.DataAccess.Sql.CustomSqlQuery customSqlQuery2 = new DevExpress.DataAccess.Sql.CustomSqlQuery();
            DevExpress.DataAccess.Sql.StoredProcQuery storedProcQuery1 = new DevExpress.DataAccess.Sql.StoredProcQuery();
            DevExpress.DataAccess.Sql.QueryParameter queryParameter1 = new DevExpress.DataAccess.Sql.QueryParameter();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(R_WMS_PAR));
            DevExpress.XtraReports.UI.XRSummary xrSummary1 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary2 = new DevExpress.XtraReports.UI.XRSummary();
            this.sqlDataSource1 = new DevExpress.DataAccess.Sql.SqlDataSource();
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrPageInfo1 = new DevExpress.XtraReports.UI.XRPageInfo();
            this.xrPageInfo2 = new DevExpress.XtraReports.UI.XRPageInfo();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.xrLabel18 = new DevExpress.XtraReports.UI.XRLabel();
            this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
            this.xrLabel15 = new DevExpress.XtraReports.UI.XRLabel();
            this.Remarks = new DevExpress.XtraReports.UI.CalculatedField();
            this.DamageType = new DevExpress.XtraReports.UI.CalculatedField();
            this.MFG = new DevExpress.XtraReports.UI.FormattingRule();
            this.SD = new DevExpress.XtraReports.UI.FormattingRule();
            this.WD = new DevExpress.XtraReports.UI.FormattingRule();
            this.NWD = new DevExpress.XtraReports.UI.FormattingRule();
            this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
            this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell4 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTable5 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow4 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell13 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell14 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell15 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
            this.calculatedField1 = new DevExpress.XtraReports.UI.CalculatedField();
            this.fieldNoOfItem1 = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.fieldSizeCode1 = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.fieldItemCode1 = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.xrPivotGrid1 = new DevExpress.XtraReports.UI.XRPivotGrid();
            this.DocNumber = new DevExpress.XtraReports.Parameters.Parameter();
            this.xrPivotGrid2 = new DevExpress.XtraReports.UI.XRPivotGrid();
            this.fieldName1 = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.fieldReceivedQty1 = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.xrPivotGridField1 = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.GroupHeader1 = new DevExpress.XtraReports.UI.GroupHeaderBand();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable5)).BeginInit();
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
            storedProcQuery1.Name = "sp_wms_report_PAR";
            queryParameter1.Name = "@DocNumber";
            queryParameter1.Type = typeof(DevExpress.DataAccess.Expression);
            queryParameter1.Value = new DevExpress.DataAccess.Expression("[Parameters.DocNumber]", typeof(string));
            storedProcQuery1.Parameters.Add(queryParameter1);
            storedProcQuery1.StoredProcName = "sp_wms_report_PAR";
            this.sqlDataSource1.Queries.AddRange(new DevExpress.DataAccess.Sql.SqlQuery[] {
            customSqlQuery1,
            customSqlQuery2,
            storedProcQuery1});
            this.sqlDataSource1.ResultSchemaSerializable = resources.GetString("sqlDataSource1.ResultSchemaSerializable");
            // 
            // Detail
            // 
            this.Detail.HeightF = 0F;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // TopMargin
            // 
            this.TopMargin.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel1,
            this.xrLabel2,
            this.xrPageInfo1,
            this.xrPageInfo2});
            this.TopMargin.HeightF = 40.00002F;
            this.TopMargin.Name = "TopMargin";
            this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrLabel1
            // 
            this.xrLabel1.Font = new System.Drawing.Font("Arial Narrow", 8F, System.Drawing.FontStyle.Bold);
            this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(781.9994F, 20F);
            this.xrLabel1.Name = "xrLabel1";
            this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel1.Scripts.OnBeforePrint = "xrLabel5_BeforePrint";
            this.xrLabel1.SizeF = new System.Drawing.SizeF(50F, 20F);
            this.xrLabel1.StylePriority.UseFont = false;
            this.xrLabel1.StylePriority.UseTextAlignment = false;
            xrSummary1.FormatString = "{0:M/d/yy}";
            xrSummary1.Func = DevExpress.XtraReports.UI.SummaryFunc.Custom;
            this.xrLabel1.Summary = xrSummary1;
            this.xrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel2
            // 
            this.xrLabel2.Font = new System.Drawing.Font("Arial Narrow", 8F, System.Drawing.FontStyle.Bold);
            this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 20F);
            this.xrLabel2.Name = "xrLabel2";
            this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel2.Scripts.OnBeforePrint = "xrLabel5_BeforePrint";
            this.xrLabel2.SizeF = new System.Drawing.SizeF(50F, 20F);
            this.xrLabel2.StylePriority.UseFont = false;
            this.xrLabel2.StylePriority.UseTextAlignment = false;
            xrSummary2.FormatString = "{0:M/d/yy}";
            xrSummary2.Func = DevExpress.XtraReports.UI.SummaryFunc.Custom;
            this.xrLabel2.Summary = xrSummary2;
            this.xrLabel2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrPageInfo1
            // 
            this.xrPageInfo1.Font = new System.Drawing.Font("Times New Roman", 8F);
            this.xrPageInfo1.Format = "Run Date & Time : {0:MMMM dd, yyyy / h:mm tt}";
            this.xrPageInfo1.LocationFloat = new DevExpress.Utils.PointFloat(50F, 23.25001F);
            this.xrPageInfo1.Name = "xrPageInfo1";
            this.xrPageInfo1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrPageInfo1.PageInfo = DevExpress.XtraPrinting.PageInfo.DateTime;
            this.xrPageInfo1.SizeF = new System.Drawing.SizeF(320.4167F, 16.75F);
            this.xrPageInfo1.StylePriority.UseFont = false;
            this.xrPageInfo1.StylePriority.UseTextAlignment = false;
            this.xrPageInfo1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrPageInfo2
            // 
            this.xrPageInfo2.Font = new System.Drawing.Font("Times New Roman", 8F);
            this.xrPageInfo2.Format = "Page {0} of {1}";
            this.xrPageInfo2.LocationFloat = new DevExpress.Utils.PointFloat(694.291F, 23.25001F);
            this.xrPageInfo2.Name = "xrPageInfo2";
            this.xrPageInfo2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrPageInfo2.SizeF = new System.Drawing.SizeF(87.70837F, 16.75F);
            this.xrPageInfo2.StylePriority.UseFont = false;
            this.xrPageInfo2.StylePriority.UseTextAlignment = false;
            this.xrPageInfo2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            // 
            // BottomMargin
            // 
            this.BottomMargin.HeightF = 39.99999F;
            this.BottomMargin.Name = "BottomMargin";
            this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrLabel18
            // 
            this.xrLabel18.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "CompanyName.Value")});
            this.xrLabel18.Font = new System.Drawing.Font("Arial Narrow", 9.75F);
            this.xrLabel18.LocationFloat = new DevExpress.Utils.PointFloat(0F, 19.16667F);
            this.xrLabel18.Name = "xrLabel18";
            this.xrLabel18.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel18.SizeF = new System.Drawing.SizeF(832F, 17.79167F);
            this.xrLabel18.StylePriority.UseFont = false;
            this.xrLabel18.StylePriority.UseTextAlignment = false;
            this.xrLabel18.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // ReportHeader
            // 
            this.ReportHeader.HeightF = 0F;
            this.ReportHeader.Name = "ReportHeader";
            // 
            // xrLabel15
            // 
            this.xrLabel15.Font = new System.Drawing.Font("Arial Narrow", 13F, System.Drawing.FontStyle.Bold);
            this.xrLabel15.LocationFloat = new DevExpress.Utils.PointFloat(0.0002914005F, 36.95834F);
            this.xrLabel15.Name = "xrLabel15";
            this.xrLabel15.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel15.SizeF = new System.Drawing.SizeF(831.9991F, 23F);
            this.xrLabel15.StylePriority.UseFont = false;
            this.xrLabel15.StylePriority.UseTextAlignment = false;
            this.xrLabel15.Text = "ALLOCATION DETAILS";
            this.xrLabel15.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // Remarks
            // 
            this.Remarks.DataMember = "sp_wms_report_ReturnSummary";
            this.Remarks.Expression = resources.GetString("Remarks.Expression");
            this.Remarks.Name = "Remarks";
            // 
            // DamageType
            // 
            this.DamageType.DataMember = "sp_wms_report_ReturnSummary";
            this.DamageType.Expression = "Iif(Substring(\'[Field9]\',0,1) == \'R\',  \'Retail Damage\', \r\n Iif([Field9] == \'\', \'N" +
    "/A\',\r\n \'MFG Damage\' ))";
            this.DamageType.Name = "DamageType";
            // 
            // MFG
            // 
            this.MFG.Condition = resources.GetString("MFG.Condition");
            // 
            // 
            // 
            this.MFG.Formatting.BackColor = System.Drawing.Color.DimGray;
            this.MFG.Name = "MFG";
            // 
            // SD
            // 
            this.SD.Condition = resources.GetString("SD.Condition");
            // 
            // 
            // 
            this.SD.Formatting.BackColor = System.Drawing.Color.DimGray;
            this.SD.Name = "SD";
            // 
            // WD
            // 
            this.WD.Condition = resources.GetString("WD.Condition");
            // 
            // 
            // 
            this.WD.Formatting.BackColor = System.Drawing.Color.DimGray;
            this.WD.Name = "WD";
            // 
            // NWD
            // 
            this.NWD.Condition = resources.GetString("NWD.Condition");
            // 
            // 
            // 
            this.NWD.Formatting.BackColor = System.Drawing.Color.DimGray;
            this.NWD.Name = "NWD";
            // 
            // PageHeader
            // 
            this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable1,
            this.xrTable5,
            this.xrLabel15,
            this.xrLabel18});
            this.PageHeader.HeightF = 134.3201F;
            this.PageHeader.Name = "PageHeader";
            // 
            // xrTable1
            // 
            this.xrTable1.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable1.Font = new System.Drawing.Font("Arial Narrow", 8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(12.16733F, 83.95833F);
            this.xrTable1.Name = "xrTable1";
            this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1});
            this.xrTable1.SizeF = new System.Drawing.SizeF(147.6389F, 15F);
            this.xrTable1.StylePriority.UseBorders = false;
            this.xrTable1.StylePriority.UseFont = false;
            this.xrTable1.StylePriority.UseTextAlignment = false;
            this.xrTable1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrTableRow1
            // 
            this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell2,
            this.xrTableCell4});
            this.xrTableRow1.Name = "xrTableRow1";
            this.xrTableRow1.Weight = 1D;
            // 
            // xrTableCell2
            // 
            this.xrTableCell2.BorderColor = System.Drawing.Color.Black;
            this.xrTableCell2.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTableCell2.BorderWidth = 1F;
            this.xrTableCell2.Font = new System.Drawing.Font("Arial Narrow", 10F, System.Drawing.FontStyle.Bold);
            this.xrTableCell2.Name = "xrTableCell2";
            this.xrTableCell2.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 100F);
            this.xrTableCell2.StylePriority.UseBorderColor = false;
            this.xrTableCell2.StylePriority.UseBorders = false;
            this.xrTableCell2.StylePriority.UseBorderWidth = false;
            this.xrTableCell2.StylePriority.UseFont = false;
            this.xrTableCell2.StylePriority.UsePadding = false;
            this.xrTableCell2.StylePriority.UseTextAlignment = false;
            this.xrTableCell2.Text = "DOC#";
            this.xrTableCell2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell2.Weight = 0.53380399366188336D;
            // 
            // xrTableCell4
            // 
            this.xrTableCell4.BorderColor = System.Drawing.Color.Black;
            this.xrTableCell4.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTableCell4.BorderWidth = 1F;
            this.xrTableCell4.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_wms_report_PAR.InvoiceNo")});
            this.xrTableCell4.Font = new System.Drawing.Font("Arial Narrow", 10F, System.Drawing.FontStyle.Bold);
            this.xrTableCell4.Name = "xrTableCell4";
            this.xrTableCell4.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 100F);
            this.xrTableCell4.StylePriority.UseBorderColor = false;
            this.xrTableCell4.StylePriority.UseBorders = false;
            this.xrTableCell4.StylePriority.UseBorderWidth = false;
            this.xrTableCell4.StylePriority.UseFont = false;
            this.xrTableCell4.StylePriority.UsePadding = false;
            this.xrTableCell4.StylePriority.UseTextAlignment = false;
            this.xrTableCell4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell4.Weight = 0.95650140959136265D;
            // 
            // xrTable5
            // 
            this.xrTable5.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable5.Font = new System.Drawing.Font("Arial Narrow", 8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.xrTable5.LocationFloat = new DevExpress.Utils.PointFloat(12.16732F, 98.95833F);
            this.xrTable5.Name = "xrTable5";
            this.xrTable5.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow4});
            this.xrTable5.SizeF = new System.Drawing.SizeF(235F, 15F);
            this.xrTable5.StylePriority.UseBorders = false;
            this.xrTable5.StylePriority.UseFont = false;
            this.xrTable5.StylePriority.UseTextAlignment = false;
            this.xrTable5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrTableRow4
            // 
            this.xrTableRow4.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell13,
            this.xrTableCell14,
            this.xrTableCell15,
            this.xrTableCell1});
            this.xrTableRow4.Name = "xrTableRow4";
            this.xrTableRow4.Weight = 1D;
            // 
            // xrTableCell13
            // 
            this.xrTableCell13.BorderColor = System.Drawing.Color.Black;
            this.xrTableCell13.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTableCell13.BorderWidth = 1F;
            this.xrTableCell13.Font = new System.Drawing.Font("Arial Narrow", 8F, System.Drawing.FontStyle.Bold);
            this.xrTableCell13.Name = "xrTableCell13";
            this.xrTableCell13.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 100F);
            this.xrTableCell13.StylePriority.UseBorderColor = false;
            this.xrTableCell13.StylePriority.UseBorders = false;
            this.xrTableCell13.StylePriority.UseBorderWidth = false;
            this.xrTableCell13.StylePriority.UseFont = false;
            this.xrTableCell13.StylePriority.UsePadding = false;
            this.xrTableCell13.StylePriority.UseTextAlignment = false;
            this.xrTableCell13.Text = "SKU";
            this.xrTableCell13.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell13.Weight = 0.35820609484322896D;
            // 
            // xrTableCell14
            // 
            this.xrTableCell14.BorderColor = System.Drawing.Color.Black;
            this.xrTableCell14.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTableCell14.BorderWidth = 1F;
            this.xrTableCell14.Font = new System.Drawing.Font("Arial Narrow", 8F, System.Drawing.FontStyle.Bold);
            this.xrTableCell14.Name = "xrTableCell14";
            this.xrTableCell14.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.xrTableCell14.StylePriority.UseBorderColor = false;
            this.xrTableCell14.StylePriority.UseBorders = false;
            this.xrTableCell14.StylePriority.UseBorderWidth = false;
            this.xrTableCell14.StylePriority.UseFont = false;
            this.xrTableCell14.StylePriority.UsePadding = false;
            this.xrTableCell14.StylePriority.UseTextAlignment = false;
            this.xrTableCell14.Text = ":";
            this.xrTableCell14.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell14.Weight = 0.074655505243655537D;
            // 
            // xrTableCell15
            // 
            this.xrTableCell15.BorderColor = System.Drawing.Color.Black;
            this.xrTableCell15.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTableCell15.BorderWidth = 1F;
            this.xrTableCell15.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_wms_report_PAR.ItemCode")});
            this.xrTableCell15.Name = "xrTableCell15";
            this.xrTableCell15.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 100F);
            this.xrTableCell15.StylePriority.UseBorderColor = false;
            this.xrTableCell15.StylePriority.UseBorders = false;
            this.xrTableCell15.StylePriority.UseBorderWidth = false;
            this.xrTableCell15.StylePriority.UsePadding = false;
            this.xrTableCell15.StylePriority.UseTextAlignment = false;
            this.xrTableCell15.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell15.Weight = 1.1317490527884193D;
            // 
            // xrTableCell1
            // 
            this.xrTableCell1.BorderColor = System.Drawing.Color.Black;
            this.xrTableCell1.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTableCell1.BorderWidth = 1F;
            this.xrTableCell1.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_wms_report_PAR.ColorCode")});
            this.xrTableCell1.Name = "xrTableCell1";
            this.xrTableCell1.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 100F);
            this.xrTableCell1.StylePriority.UseBorderColor = false;
            this.xrTableCell1.StylePriority.UseBorders = false;
            this.xrTableCell1.StylePriority.UseBorderWidth = false;
            this.xrTableCell1.StylePriority.UsePadding = false;
            this.xrTableCell1.StylePriority.UseTextAlignment = false;
            this.xrTableCell1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell1.Weight = 0.80754099572949589D;
            // 
            // calculatedField1
            // 
            this.calculatedField1.DataMember = "sp_wms_report_DamageSlip";
            this.calculatedField1.Expression = "Iif([BrandCode] == \'W\', \'WRANGLER\'\r\n\t, Iif([BrandCode] == \'B\', \'BOBSON\'\r\n\t, Iif([" +
    "BrandCode] == \'F\', \'FREEGO\'\r\n\t, Iif([BrandCode] == \'G\', \'FRESHGEAR\', \'N/A\'))))";
            this.calculatedField1.Name = "calculatedField1";
            // 
            // fieldNoOfItem1
            // 
            this.fieldNoOfItem1.Appearance.Cell.BackColor = System.Drawing.Color.Transparent;
            this.fieldNoOfItem1.Appearance.Cell.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.fieldNoOfItem1.Appearance.FieldValue.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fieldNoOfItem1.Appearance.FieldValue.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.fieldNoOfItem1.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea;
            this.fieldNoOfItem1.AreaIndex = 0;
            this.fieldNoOfItem1.Caption = "No Of Item";
            this.fieldNoOfItem1.EmptyCellText = "0";
            this.fieldNoOfItem1.EmptyValueText = "0";
            this.fieldNoOfItem1.FieldName = "NoOfItem";
            this.fieldNoOfItem1.Name = "fieldNoOfItem1";
            this.fieldNoOfItem1.Width = 50;
            // 
            // fieldSizeCode1
            // 
            this.fieldSizeCode1.Appearance.FieldValue.BackColor = System.Drawing.Color.Transparent;
            this.fieldSizeCode1.Appearance.FieldValue.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fieldSizeCode1.Appearance.FieldValue.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.fieldSizeCode1.Area = DevExpress.XtraPivotGrid.PivotArea.ColumnArea;
            this.fieldSizeCode1.AreaIndex = 0;
            this.fieldSizeCode1.Caption = "Size Code";
            this.fieldSizeCode1.FieldName = "SizeCode";
            this.fieldSizeCode1.Name = "fieldSizeCode1";
            this.fieldSizeCode1.Width = 50;
            // 
            // fieldItemCode1
            // 
            this.fieldItemCode1.Appearance.FieldHeader.BackColor = System.Drawing.Color.Transparent;
            this.fieldItemCode1.Appearance.FieldHeader.TextVerticalAlignment = DevExpress.Utils.VertAlignment.Center;
            this.fieldItemCode1.Appearance.FieldValue.BackColor = System.Drawing.Color.Transparent;
            this.fieldItemCode1.Appearance.FieldValue.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fieldItemCode1.Appearance.FieldValue.TextVerticalAlignment = DevExpress.Utils.VertAlignment.Center;
            this.fieldItemCode1.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.fieldItemCode1.AreaIndex = 0;
            this.fieldItemCode1.Caption = "Item Code";
            this.fieldItemCode1.FieldName = "ItemCode";
            this.fieldItemCode1.Name = "fieldItemCode1";
            this.fieldItemCode1.Width = 150;
            // 
            // xrPivotGrid1
            // 
            this.xrPivotGrid1.Appearance.Cell.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrPivotGrid1.Appearance.FieldHeader.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrPivotGrid1.Appearance.FieldValue.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrPivotGrid1.Appearance.FieldValueGrandTotal.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrPivotGrid1.Appearance.FieldValueTotal.BackColor = System.Drawing.Color.Transparent;
            this.xrPivotGrid1.Appearance.FieldValueTotal.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrPivotGrid1.Appearance.FieldValueTotal.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.xrPivotGrid1.Appearance.FieldValueTotal.TextVerticalAlignment = DevExpress.Utils.VertAlignment.Center;
            this.xrPivotGrid1.Appearance.GrandTotalCell.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrPivotGrid1.Appearance.TotalCell.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrPivotGrid1.DataMember = "sp_wms_report_DamageSlip";
            this.xrPivotGrid1.DataSource = this.sqlDataSource1;
            this.xrPivotGrid1.Fields.AddRange(new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField[] {
            this.fieldItemCode1,
            this.fieldSizeCode1,
            this.fieldNoOfItem1});
            this.xrPivotGrid1.LocationFloat = new DevExpress.Utils.PointFloat(12.16732F, 65.97224F);
            this.xrPivotGrid1.Name = "xrPivotGrid1";
            this.xrPivotGrid1.OptionsPrint.FilterSeparatorBarPadding = 3;
            this.xrPivotGrid1.OptionsView.ShowColumnHeaders = false;
            this.xrPivotGrid1.OptionsView.ShowDataHeaders = false;
            this.xrPivotGrid1.OptionsView.ShowGrandTotalsForSingleValues = true;
            this.xrPivotGrid1.OptionsView.ShowRowHeaders = false;
            this.xrPivotGrid1.Scripts.OnFieldValueDisplayText = "xrPivotGrid1_FieldValueDisplayText";
            this.xrPivotGrid1.SizeF = new System.Drawing.SizeF(217.8326F, 49.94025F);
            // 
            // DocNumber
            // 
            this.DocNumber.Description = "DocNumber";
            this.DocNumber.Name = "DocNumber";
            // 
            // xrPivotGrid2
            // 
            this.xrPivotGrid2.Appearance.Cell.Font = new System.Drawing.Font("Arial Narrow", 9.75F);
            this.xrPivotGrid2.Appearance.FieldHeader.Font = new System.Drawing.Font("Arial Narrow", 9.75F);
            this.xrPivotGrid2.Appearance.FieldValue.Font = new System.Drawing.Font("Arial Narrow", 9.75F);
            this.xrPivotGrid2.Appearance.FieldValueGrandTotal.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold);
            this.xrPivotGrid2.Appearance.FieldValueTotal.BackColor = System.Drawing.Color.Transparent;
            this.xrPivotGrid2.Appearance.FieldValueTotal.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold);
            this.xrPivotGrid2.Appearance.FieldValueTotal.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.xrPivotGrid2.Appearance.FieldValueTotal.TextVerticalAlignment = DevExpress.Utils.VertAlignment.Center;
            this.xrPivotGrid2.Appearance.GrandTotalCell.Font = new System.Drawing.Font("Arial Narrow", 9.75F);
            this.xrPivotGrid2.Appearance.TotalCell.Font = new System.Drawing.Font("Arial Narrow", 9.75F);
            this.xrPivotGrid2.DataMember = "sp_wms_report_PAR";
            this.xrPivotGrid2.DataSource = this.sqlDataSource1;
            this.xrPivotGrid2.Fields.AddRange(new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField[] {
            this.fieldName1,
            this.fieldReceivedQty1,
            this.xrPivotGridField1});
            this.xrPivotGrid2.LocationFloat = new DevExpress.Utils.PointFloat(10F, 0F);
            this.xrPivotGrid2.Name = "xrPivotGrid2";
            this.xrPivotGrid2.OptionsPrint.FilterSeparatorBarPadding = 3;
            this.xrPivotGrid2.OptionsPrint.PrintHeadersOnEveryPage = true;
            this.xrPivotGrid2.OptionsView.ShowColumnHeaders = false;
            this.xrPivotGrid2.OptionsView.ShowDataHeaders = false;
            this.xrPivotGrid2.OptionsView.ShowFilterHeaders = false;
            this.xrPivotGrid2.OptionsView.ShowGrandTotalsForSingleValues = true;
            this.xrPivotGrid2.OptionsView.ShowRowHeaders = false;
            this.xrPivotGrid2.Scripts.OnFieldValueDisplayText = "xrPivotGrid1_FieldValueDisplayText";
            this.xrPivotGrid2.SizeF = new System.Drawing.SizeF(418.982F, 47.9032F);
            // 
            // fieldName1
            // 
            this.fieldName1.Appearance.Cell.BackColor = System.Drawing.Color.Transparent;
            this.fieldName1.Appearance.FieldHeader.BackColor = System.Drawing.Color.Transparent;
            this.fieldName1.Appearance.FieldHeader.Font = new System.Drawing.Font("Arial Narrow", 9.75F);
            this.fieldName1.Appearance.FieldValue.BackColor = System.Drawing.Color.Transparent;
            this.fieldName1.Appearance.FieldValue.Font = new System.Drawing.Font("Arial Narrow", 9F, System.Drawing.FontStyle.Bold);
            this.fieldName1.Appearance.FieldValue.TextVerticalAlignment = DevExpress.Utils.VertAlignment.Center;
            this.fieldName1.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
            this.fieldName1.AreaIndex = 0;
            this.fieldName1.Caption = "Name";
            this.fieldName1.FieldName = "Name";
            this.fieldName1.Name = "fieldName1";
            this.fieldName1.Width = 300;
            // 
            // fieldReceivedQty1
            // 
            this.fieldReceivedQty1.Appearance.Cell.BackColor = System.Drawing.Color.Transparent;
            this.fieldReceivedQty1.Appearance.Cell.Font = new System.Drawing.Font("Arial Narrow", 9.75F);
            this.fieldReceivedQty1.Appearance.Cell.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.fieldReceivedQty1.Appearance.Cell.TextVerticalAlignment = DevExpress.Utils.VertAlignment.Center;
            this.fieldReceivedQty1.Appearance.FieldHeader.BackColor = System.Drawing.Color.Transparent;
            this.fieldReceivedQty1.Appearance.FieldHeader.Font = new System.Drawing.Font("Arial Narrow", 9.75F);
            this.fieldReceivedQty1.Appearance.FieldHeader.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.fieldReceivedQty1.Appearance.FieldHeader.TextVerticalAlignment = DevExpress.Utils.VertAlignment.Center;
            this.fieldReceivedQty1.Appearance.FieldValue.BackColor = System.Drawing.Color.Transparent;
            this.fieldReceivedQty1.Appearance.FieldValue.Font = new System.Drawing.Font("Arial Narrow", 6F);
            this.fieldReceivedQty1.Appearance.FieldValue.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.fieldReceivedQty1.Appearance.FieldValue.TextVerticalAlignment = DevExpress.Utils.VertAlignment.Center;
            this.fieldReceivedQty1.Appearance.GrandTotalCell.Font = new System.Drawing.Font("Arial Narrow", 9F, System.Drawing.FontStyle.Bold);
            this.fieldReceivedQty1.Appearance.TotalCell.Font = new System.Drawing.Font("Arial Narrow", 9F, System.Drawing.FontStyle.Bold);
            this.fieldReceivedQty1.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea;
            this.fieldReceivedQty1.AreaIndex = 0;
            this.fieldReceivedQty1.Caption = "Received Qty";
            this.fieldReceivedQty1.CellFormat.FormatString = "{0:#,#}";
            this.fieldReceivedQty1.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.fieldReceivedQty1.FieldName = "ReceivedQty";
            this.fieldReceivedQty1.Name = "fieldReceivedQty1";
            this.fieldReceivedQty1.Width = 35;
            // 
            // xrPivotGridField1
            // 
            this.xrPivotGridField1.Appearance.FieldHeader.BackColor = System.Drawing.Color.Transparent;
            this.xrPivotGridField1.Appearance.FieldHeader.Font = new System.Drawing.Font("Arial Narrow", 9.75F);
            this.xrPivotGridField1.Appearance.FieldValue.BackColor = System.Drawing.Color.Transparent;
            this.xrPivotGridField1.Appearance.FieldValue.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold);
            this.xrPivotGridField1.Appearance.FieldValue.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.xrPivotGridField1.Appearance.FieldValue.TextVerticalAlignment = DevExpress.Utils.VertAlignment.Center;
            this.xrPivotGridField1.Area = DevExpress.XtraPivotGrid.PivotArea.ColumnArea;
            this.xrPivotGridField1.AreaIndex = 0;
            this.xrPivotGridField1.Caption = "Size Code";
            this.xrPivotGridField1.FieldName = "SizeCode";
            this.xrPivotGridField1.Name = "xrPivotGridField1";
            this.xrPivotGridField1.Width = 35;
            // 
            // GroupHeader1
            // 
            this.GroupHeader1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPivotGrid2});
            this.GroupHeader1.HeightF = 124.3201F;
            this.GroupHeader1.Name = "GroupHeader1";
            // 
            // R_WMS_PAR
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.ReportHeader,
            this.PageHeader,
            this.GroupHeader1});
            this.CalculatedFields.AddRange(new DevExpress.XtraReports.UI.CalculatedField[] {
            this.Remarks,
            this.DamageType,
            this.calculatedField1});
            this.ComponentStorage.AddRange(new System.ComponentModel.IComponent[] {
            this.sqlDataSource1});
            this.DataMember = "sp_wms_report_PAR";
            this.DataSource = this.sqlDataSource1;
            this.FormattingRuleSheet.AddRange(new DevExpress.XtraReports.UI.FormattingRule[] {
            this.MFG,
            this.SD,
            this.WD,
            this.NWD});
            this.Margins = new System.Drawing.Printing.Margins(9, 9, 40, 40);
            this.Parameters.AddRange(new DevExpress.XtraReports.Parameters.Parameter[] {
            this.DocNumber});
            this.Scripts.OnParametersRequestBeforeShow = "R_WMS_DamageSlip_ParametersRequestBeforeShow";
            this.ScriptsSource = resources.GetString("$this.ScriptsSource");
            this.Version = "15.1";
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private DevExpress.DataAccess.Sql.SqlDataSource sqlDataSource1;
        private DevExpress.XtraReports.UI.XRPageInfo xrPageInfo2;
        private DevExpress.XtraReports.UI.XRLabel xrLabel18;
        //private DevExpress.XtraTabbedMdi.XtraTabbedMdiManager xtraTabbedMdiManager1;
        //private DevExpress.XtraBars.Ribbon.GalleryDropDown galleryDropDown1;
        //private DevExpress.XtraBars.Ribbon.GalleryDropDown galleryDropDown2;
        private DevExpress.XtraReports.UI.ReportHeaderBand ReportHeader;
        private DevExpress.XtraReports.UI.XRLabel xrLabel15;
        //private DevExpress.XtraEditors.DateTimeChartRangeControlClient dateTimeChartRangeControlClient1;
        private DevExpress.XtraReports.UI.XRLabel xrLabel2;
        private DevExpress.XtraReports.UI.XRPageInfo xrPageInfo1;
        private DevExpress.XtraReports.UI.CalculatedField Remarks;
        private DevExpress.XtraReports.UI.CalculatedField DamageType;
        private DevExpress.XtraReports.UI.FormattingRule MFG;
        private DevExpress.XtraReports.UI.FormattingRule SD;
        private DevExpress.XtraReports.UI.FormattingRule WD;
        private DevExpress.XtraReports.UI.FormattingRule NWD;
        private DevExpress.XtraReports.UI.PageHeaderBand PageHeader;
        private DevExpress.XtraReports.UI.CalculatedField calculatedField1;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField fieldNoOfItem1;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField fieldSizeCode1;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField fieldItemCode1;
        private DevExpress.XtraReports.UI.XRPivotGrid xrPivotGrid1;
        private DevExpress.XtraReports.Parameters.Parameter DocNumber;
        private DevExpress.XtraReports.UI.XRPivotGrid xrPivotGrid2;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField fieldName1;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField fieldReceivedQty1;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField xrPivotGridField1;
        private DevExpress.XtraReports.UI.XRTable xrTable5;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow4;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell13;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell14;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell15;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell1;
        private DevExpress.XtraReports.UI.XRTable xrTable1;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow1;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell2;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell4;
        private DevExpress.XtraReports.UI.XRLabel xrLabel1;
        private DevExpress.XtraReports.UI.GroupHeaderBand GroupHeader1;
    }
}
