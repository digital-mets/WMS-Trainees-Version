namespace GWL.WebReports.GEARS_Reports
{
    partial class R_WMS_DRForSMJGI

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
            DevExpress.DataAccess.Sql.QueryParameter queryParameter2 = new DevExpress.DataAccess.Sql.QueryParameter();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(R_WMS_DRForSMJGI));
            DevExpress.XtraReports.UI.XRSummary xrSummary1 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary2 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary3 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary4 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary5 = new DevExpress.XtraReports.UI.XRSummary();
            this.sqlDataSource1 = new DevExpress.DataAccess.Sql.SqlDataSource();
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
            this.xrLabel11 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel12 = new DevExpress.XtraReports.UI.XRLabel();
            this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
            this.xrLabel7 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel10 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel9 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel6 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel4 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel5 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel8 = new DevExpress.XtraReports.UI.XRLabel();
            this.Remarks = new DevExpress.XtraReports.UI.CalculatedField();
            this.DamageType = new DevExpress.XtraReports.UI.CalculatedField();
            this.DocNumberFrom = new DevExpress.XtraReports.Parameters.Parameter();
            this.DocNumberTo = new DevExpress.XtraReports.Parameters.Parameter();
            this.GroupHeader1 = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel27 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel26 = new DevExpress.XtraReports.UI.XRLabel();
            this.GroupHeader2 = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.xrLabel24 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel20 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel19 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel17 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel16 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel13 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrPivotGrid2 = new DevExpress.XtraReports.UI.XRPivotGrid();
            this.fieldItemCode1 = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.fieldColorCode1 = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.fieldSizeCode1 = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.fieldQty1 = new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField();
            this.xrLabel23 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel21 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel22 = new DevExpress.XtraReports.UI.XRLabel();
            this.GroupFooter1 = new DevExpress.XtraReports.UI.GroupFooterBand();
            this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
            this.GroupFooter2 = new DevExpress.XtraReports.UI.GroupFooterBand();
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
            storedProcQuery1.Name = "sp_wms_report_DR5thCopySMNONEVDR";
            queryParameter1.Name = "@DocNumberFrom";
            queryParameter1.Type = typeof(DevExpress.DataAccess.Expression);
            queryParameter1.Value = new DevExpress.DataAccess.Expression("[Parameters.DocNumberFrom]", typeof(string));
            queryParameter2.Name = "@DocNumberTo";
            queryParameter2.Type = typeof(DevExpress.DataAccess.Expression);
            queryParameter2.Value = new DevExpress.DataAccess.Expression("[Parameters.DocNumberTo]", typeof(string));
            storedProcQuery1.Parameters.Add(queryParameter1);
            storedProcQuery1.Parameters.Add(queryParameter2);
            storedProcQuery1.StoredProcName = "sp_wms_report_DR5thCopySMNONEVDR";
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
            this.TopMargin.HeightF = 0F;
            this.TopMargin.Name = "TopMargin";
            this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // BottomMargin
            // 
            this.BottomMargin.HeightF = 20F;
            this.BottomMargin.Name = "BottomMargin";
            this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // ReportHeader
            // 
            this.ReportHeader.HeightF = 0F;
            this.ReportHeader.Name = "ReportHeader";
            // 
            // xrLabel11
            // 
            this.xrLabel11.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_wms_report_DR5thCopySMNONEVDR.DocNumber", "{0:#,#.000}")});
            this.xrLabel11.Font = new System.Drawing.Font("Arial Narrow", 10F, System.Drawing.FontStyle.Bold);
            this.xrLabel11.LocationFloat = new DevExpress.Utils.PointFloat(684F, 48F);
            this.xrLabel11.Name = "xrLabel11";
            this.xrLabel11.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 100F);
            this.xrLabel11.Scripts.OnBeforePrint = "xrLabel1_BeforePrint";
            this.xrLabel11.SizeF = new System.Drawing.SizeF(131.91F, 14F);
            this.xrLabel11.StylePriority.UseFont = false;
            this.xrLabel11.StylePriority.UsePadding = false;
            this.xrLabel11.StylePriority.UseTextAlignment = false;
            this.xrLabel11.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel12
            // 
            this.xrLabel12.Font = new System.Drawing.Font("Arial Narrow", 10F);
            this.xrLabel12.LocationFloat = new DevExpress.Utils.PointFloat(624F, 48F);
            this.xrLabel12.Name = "xrLabel12";
            this.xrLabel12.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 100F);
            this.xrLabel12.Scripts.OnBeforePrint = "xrLabel1_BeforePrint";
            this.xrLabel12.SizeF = new System.Drawing.SizeF(60F, 14F);
            this.xrLabel12.StylePriority.UseFont = false;
            this.xrLabel12.StylePriority.UsePadding = false;
            this.xrLabel12.StylePriority.UseTextAlignment = false;
            this.xrLabel12.Text = "DR#";
            this.xrLabel12.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // PageHeader
            // 
            this.PageHeader.HeightF = 0F;
            this.PageHeader.Name = "PageHeader";
            // 
            // xrLabel7
            // 
            this.xrLabel7.Font = new System.Drawing.Font("Arial Narrow", 10F);
            this.xrLabel7.LocationFloat = new DevExpress.Utils.PointFloat(648F, 157.6F);
            this.xrLabel7.Name = "xrLabel7";
            this.xrLabel7.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 100F);
            this.xrLabel7.Scripts.OnBeforePrint = "xrLabel1_BeforePrint";
            this.xrLabel7.SizeF = new System.Drawing.SizeF(79.35F, 14F);
            this.xrLabel7.StylePriority.UseFont = false;
            this.xrLabel7.StylePriority.UsePadding = false;
            this.xrLabel7.StylePriority.UseTextAlignment = false;
            this.xrLabel7.Text = "Bales:";
            this.xrLabel7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel10
            // 
            this.xrLabel10.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_wms_report_DR5thCopySMNONEVDR.Bales", "{0:#,#}")});
            this.xrLabel10.Font = new System.Drawing.Font("Arial Narrow", 10F);
            this.xrLabel10.LocationFloat = new DevExpress.Utils.PointFloat(727.35F, 157.6F);
            this.xrLabel10.Name = "xrLabel10";
            this.xrLabel10.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 100F);
            this.xrLabel10.Scripts.OnBeforePrint = "xrLabel1_BeforePrint";
            this.xrLabel10.SizeF = new System.Drawing.SizeF(114.56F, 14F);
            this.xrLabel10.StylePriority.UseFont = false;
            this.xrLabel10.StylePriority.UsePadding = false;
            this.xrLabel10.StylePriority.UseTextAlignment = false;
            this.xrLabel10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel9
            // 
            this.xrLabel9.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_wms_report_DR5thCopySMNONEVDR.NetWeight", "{0:n}")});
            this.xrLabel9.Font = new System.Drawing.Font("Arial Narrow", 10F);
            this.xrLabel9.LocationFloat = new DevExpress.Utils.PointFloat(727.35F, 143.6F);
            this.xrLabel9.Name = "xrLabel9";
            this.xrLabel9.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 100F);
            this.xrLabel9.Scripts.OnBeforePrint = "xrLabel1_BeforePrint";
            this.xrLabel9.SizeF = new System.Drawing.SizeF(114.56F, 14F);
            this.xrLabel9.StylePriority.UseFont = false;
            this.xrLabel9.StylePriority.UsePadding = false;
            this.xrLabel9.StylePriority.UseTextAlignment = false;
            this.xrLabel9.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel6
            // 
            this.xrLabel6.Font = new System.Drawing.Font("Arial Narrow", 10F);
            this.xrLabel6.LocationFloat = new DevExpress.Utils.PointFloat(648F, 143.6F);
            this.xrLabel6.Name = "xrLabel6";
            this.xrLabel6.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 100F);
            this.xrLabel6.Scripts.OnBeforePrint = "xrLabel1_BeforePrint";
            this.xrLabel6.SizeF = new System.Drawing.SizeF(79.35F, 14F);
            this.xrLabel6.StylePriority.UseFont = false;
            this.xrLabel6.StylePriority.UsePadding = false;
            this.xrLabel6.StylePriority.UseTextAlignment = false;
            this.xrLabel6.Text = "Net Wt.:";
            this.xrLabel6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel4
            // 
            this.xrLabel4.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_wms_report_DR5thCopySMNONEVDR.DocDate", "{0:yyyy-MM-dd}")});
            this.xrLabel4.Font = new System.Drawing.Font("Arial Narrow", 10F);
            this.xrLabel4.LocationFloat = new DevExpress.Utils.PointFloat(648F, 129.6F);
            this.xrLabel4.Name = "xrLabel4";
            this.xrLabel4.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 100F);
            this.xrLabel4.Scripts.OnBeforePrint = "xrLabel1_BeforePrint";
            this.xrLabel4.SizeF = new System.Drawing.SizeF(193.91F, 14F);
            this.xrLabel4.StylePriority.UseFont = false;
            this.xrLabel4.StylePriority.UsePadding = false;
            this.xrLabel4.StylePriority.UseTextAlignment = false;
            this.xrLabel4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel1
            // 
            this.xrLabel1.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_wms_report_DR5thCopySMNONEVDR.DeliveryAddress")});
            this.xrLabel1.Font = new System.Drawing.Font("Arial Narrow", 10F);
            this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(72F, 143.6F);
            this.xrLabel1.Name = "xrLabel1";
            this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 100F);
            this.xrLabel1.Scripts.OnBeforePrint = "xrLabel1_BeforePrint";
            this.xrLabel1.SizeF = new System.Drawing.SizeF(450F, 14F);
            this.xrLabel1.StylePriority.UseFont = false;
            this.xrLabel1.StylePriority.UsePadding = false;
            this.xrLabel1.StylePriority.UseTextAlignment = false;
            this.xrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel5
            // 
            this.xrLabel5.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_wms_report_DR5thCopySMNONEVDR.Customer")});
            this.xrLabel5.Font = new System.Drawing.Font("Arial Narrow", 10F);
            this.xrLabel5.LocationFloat = new DevExpress.Utils.PointFloat(96F, 129.6F);
            this.xrLabel5.Name = "xrLabel5";
            this.xrLabel5.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 100F);
            this.xrLabel5.Scripts.OnBeforePrint = "xrLabel1_BeforePrint";
            this.xrLabel5.SizeF = new System.Drawing.SizeF(100F, 14F);
            this.xrLabel5.StylePriority.UseFont = false;
            this.xrLabel5.StylePriority.UsePadding = false;
            this.xrLabel5.StylePriority.UseTextAlignment = false;
            this.xrLabel5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel8
            // 
            this.xrLabel8.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_wms_report_DR5thCopySMNONEVDR.Name")});
            this.xrLabel8.Font = new System.Drawing.Font("Arial Narrow", 10F);
            this.xrLabel8.LocationFloat = new DevExpress.Utils.PointFloat(196F, 129.6F);
            this.xrLabel8.Name = "xrLabel8";
            this.xrLabel8.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 100F);
            this.xrLabel8.Scripts.OnBeforePrint = "xrLabel1_BeforePrint";
            this.xrLabel8.SizeF = new System.Drawing.SizeF(310F, 14F);
            this.xrLabel8.StylePriority.UseFont = false;
            this.xrLabel8.StylePriority.UsePadding = false;
            this.xrLabel8.StylePriority.UseTextAlignment = false;
            this.xrLabel8.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
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
            // DocNumberFrom
            // 
            this.DocNumberFrom.Description = "DocNumberFrom";
            this.DocNumberFrom.Name = "DocNumberFrom";
            // 
            // DocNumberTo
            // 
            this.DocNumberTo.Description = "DocNumberTo";
            this.DocNumberTo.Name = "DocNumberTo";
            // 
            // GroupHeader1
            // 
            this.GroupHeader1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel2,
            this.xrLabel27,
            this.xrLabel26,
            this.xrLabel12,
            this.xrLabel11,
            this.xrLabel4,
            this.xrLabel6,
            this.xrLabel7,
            this.xrLabel9,
            this.xrLabel10,
            this.xrLabel1,
            this.xrLabel8,
            this.xrLabel5});
            this.GroupHeader1.GroupFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("DocNumber", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});
            this.GroupHeader1.HeightF = 248F;
            this.GroupHeader1.Level = 1;
            this.GroupHeader1.Name = "GroupHeader1";
            this.GroupHeader1.PageBreak = DevExpress.XtraReports.UI.PageBreak.BeforeBandExceptFirstEntry;
            // 
            // xrLabel2
            // 
            this.xrLabel2.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_wms_report_DR5thCopySMNONEVDR.Brand")});
            this.xrLabel2.Font = new System.Drawing.Font("Arial Narrow", 11F);
            this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(312F, 157.6F);
            this.xrLabel2.Name = "xrLabel2";
            this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.xrLabel2.Scripts.OnBeforePrint = "xrLabel1_BeforePrint";
            this.xrLabel2.SizeF = new System.Drawing.SizeF(150F, 14F);
            this.xrLabel2.StylePriority.UseFont = false;
            this.xrLabel2.StylePriority.UsePadding = false;
            this.xrLabel2.StylePriority.UseTextAlignment = false;
            this.xrLabel2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel27
            // 
            this.xrLabel27.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_wms_report_DR5thCopySMNONEVDR.SMDeptSub")});
            this.xrLabel27.Font = new System.Drawing.Font("Arial Narrow", 10F);
            this.xrLabel27.LocationFloat = new DevExpress.Utils.PointFloat(515.6F, 115.6F);
            this.xrLabel27.Name = "xrLabel27";
            this.xrLabel27.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 100F);
            this.xrLabel27.Scripts.OnBeforePrint = "xrLabel1_BeforePrint";
            this.xrLabel27.SizeF = new System.Drawing.SizeF(87.71F, 14F);
            this.xrLabel27.StylePriority.UseFont = false;
            this.xrLabel27.StylePriority.UsePadding = false;
            this.xrLabel27.StylePriority.UseTextAlignment = false;
            this.xrLabel27.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel26
            // 
            this.xrLabel26.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_wms_report_DR5thCopySMNONEVDR.VendorCode")});
            this.xrLabel26.Font = new System.Drawing.Font("Arial Narrow", 10F);
            this.xrLabel26.LocationFloat = new DevExpress.Utils.PointFloat(417.47F, 115.6F);
            this.xrLabel26.Name = "xrLabel26";
            this.xrLabel26.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 100F);
            this.xrLabel26.Scripts.OnBeforePrint = "xrLabel1_BeforePrint";
            this.xrLabel26.SizeF = new System.Drawing.SizeF(98.13F, 14F);
            this.xrLabel26.StylePriority.UseFont = false;
            this.xrLabel26.StylePriority.UsePadding = false;
            this.xrLabel26.StylePriority.UseTextAlignment = false;
            this.xrLabel26.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // GroupHeader2
            // 
            this.GroupHeader2.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel24,
            this.xrLabel20,
            this.xrLabel19,
            this.xrLabel17,
            this.xrLabel16,
            this.xrLabel13,
            this.xrPivotGrid2});
            this.GroupHeader2.GroupFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("ItemCode", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});
            this.GroupHeader2.HeightF = 27F;
            this.GroupHeader2.Name = "GroupHeader2";
            // 
            // xrLabel24
            // 
            this.xrLabel24.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_wms_report_DR5thCopySMNONEVDR.Qty")});
            this.xrLabel24.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Bold);
            this.xrLabel24.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrLabel24.Name = "xrLabel24";
            this.xrLabel24.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 10, 0, 0, 100F);
            this.xrLabel24.Scripts.OnBeforePrint = "xrLabel1_BeforePrint";
            this.xrLabel24.SizeF = new System.Drawing.SizeF(90F, 12F);
            this.xrLabel24.StylePriority.UseFont = false;
            this.xrLabel24.StylePriority.UsePadding = false;
            this.xrLabel24.StylePriority.UseTextAlignment = false;
            xrSummary1.FormatString = "{0:#,0}";
            xrSummary1.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            this.xrLabel24.Summary = xrSummary1;
            this.xrLabel24.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            // 
            // xrLabel20
            // 
            this.xrLabel20.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_wms_report_DR5thCopySMNONEVDR.Amount")});
            this.xrLabel20.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Bold);
            this.xrLabel20.LocationFloat = new DevExpress.Utils.PointFloat(180F, 0F);
            this.xrLabel20.Name = "xrLabel20";
            this.xrLabel20.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 10, 0, 0, 100F);
            this.xrLabel20.Scripts.OnBeforePrint = "xrLabel1_BeforePrint";
            this.xrLabel20.SizeF = new System.Drawing.SizeF(90F, 12F);
            this.xrLabel20.StylePriority.UseFont = false;
            this.xrLabel20.StylePriority.UsePadding = false;
            this.xrLabel20.StylePriority.UseTextAlignment = false;
            xrSummary2.FormatString = "{0:n}";
            xrSummary2.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            this.xrLabel20.Summary = xrSummary2;
            this.xrLabel20.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            // 
            // xrLabel19
            // 
            this.xrLabel19.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_wms_report_DR5thCopySMNONEVDR.Price")});
            this.xrLabel19.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Bold);
            this.xrLabel19.LocationFloat = new DevExpress.Utils.PointFloat(90F, 0F);
            this.xrLabel19.Name = "xrLabel19";
            this.xrLabel19.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 10, 0, 0, 100F);
            this.xrLabel19.Scripts.OnBeforePrint = "xrLabel1_BeforePrint";
            this.xrLabel19.SizeF = new System.Drawing.SizeF(90F, 12F);
            this.xrLabel19.StylePriority.UseFont = false;
            this.xrLabel19.StylePriority.UsePadding = false;
            this.xrLabel19.StylePriority.UseTextAlignment = false;
            xrSummary3.FormatString = "{0:#,0}";
            xrSummary3.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            this.xrLabel19.Summary = xrSummary3;
            this.xrLabel19.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            // 
            // xrLabel17
            // 
            this.xrLabel17.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_wms_report_DR5thCopySMNONEVDR.ShortDesc")});
            this.xrLabel17.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Bold);
            this.xrLabel17.LocationFloat = new DevExpress.Utils.PointFloat(421.2499F, 0F);
            this.xrLabel17.Name = "xrLabel17";
            this.xrLabel17.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 100F);
            this.xrLabel17.Scripts.OnBeforePrint = "xrLabel1_BeforePrint";
            this.xrLabel17.SizeF = new System.Drawing.SizeF(410.75F, 10F);
            this.xrLabel17.StylePriority.UseFont = false;
            this.xrLabel17.StylePriority.UsePadding = false;
            this.xrLabel17.StylePriority.UseTextAlignment = false;
            this.xrLabel17.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel16
            // 
            this.xrLabel16.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_wms_report_DR5thCopySMNONEVDR.ColorCode")});
            this.xrLabel16.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Bold);
            this.xrLabel16.LocationFloat = new DevExpress.Utils.PointFloat(281.2499F, 12F);
            this.xrLabel16.Name = "xrLabel16";
            this.xrLabel16.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 100F);
            this.xrLabel16.Scripts.OnBeforePrint = "xrLabel1_BeforePrint";
            this.xrLabel16.SizeF = new System.Drawing.SizeF(140F, 15F);
            this.xrLabel16.StylePriority.UseFont = false;
            this.xrLabel16.StylePriority.UsePadding = false;
            this.xrLabel16.StylePriority.UseTextAlignment = false;
            this.xrLabel16.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel13
            // 
            this.xrLabel13.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_wms_report_DR5thCopySMNONEVDR.ItemCode")});
            this.xrLabel13.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Bold);
            this.xrLabel13.LocationFloat = new DevExpress.Utils.PointFloat(281.2499F, 0F);
            this.xrLabel13.Name = "xrLabel13";
            this.xrLabel13.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 100F);
            this.xrLabel13.Scripts.OnBeforePrint = "xrLabel1_BeforePrint";
            this.xrLabel13.SizeF = new System.Drawing.SizeF(140F, 12F);
            this.xrLabel13.StylePriority.UseFont = false;
            this.xrLabel13.StylePriority.UsePadding = false;
            this.xrLabel13.StylePriority.UseTextAlignment = false;
            this.xrLabel13.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrPivotGrid2
            // 
            this.xrPivotGrid2.Appearance.Cell.BorderWidth = 5F;
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
            this.xrPivotGrid2.DataMember = "sp_wms_report_DR5thCopySMNONEVDR";
            this.xrPivotGrid2.DataSource = this.sqlDataSource1;
            this.xrPivotGrid2.Fields.AddRange(new DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField[] {
            this.fieldItemCode1,
            this.fieldColorCode1,
            this.fieldSizeCode1,
            this.fieldQty1});
            this.xrPivotGrid2.LocationFloat = new DevExpress.Utils.PointFloat(421.2495F, 9.999994F);
            this.xrPivotGrid2.Name = "xrPivotGrid2";
            this.xrPivotGrid2.OptionsPrint.FilterSeparatorBarPadding = 3;
            this.xrPivotGrid2.OptionsPrint.PrintFilterHeaders = DevExpress.Utils.DefaultBoolean.False;
            this.xrPivotGrid2.OptionsPrint.PrintHeadersOnEveryPage = true;
            this.xrPivotGrid2.OptionsPrint.PrintHorzLines = DevExpress.Utils.DefaultBoolean.False;
            this.xrPivotGrid2.OptionsPrint.PrintVertLines = DevExpress.Utils.DefaultBoolean.False;
            this.xrPivotGrid2.OptionsView.RowTotalsLocation = DevExpress.XtraPivotGrid.PivotRowTotalsLocation.Near;
            this.xrPivotGrid2.OptionsView.ShowColumnGrandTotalHeader = false;
            this.xrPivotGrid2.OptionsView.ShowColumnGrandTotals = false;
            this.xrPivotGrid2.OptionsView.ShowColumnHeaders = false;
            this.xrPivotGrid2.OptionsView.ShowColumnTotals = false;
            this.xrPivotGrid2.OptionsView.ShowDataHeaders = false;
            this.xrPivotGrid2.OptionsView.ShowFilterHeaders = false;
            this.xrPivotGrid2.OptionsView.ShowRowGrandTotalHeader = false;
            this.xrPivotGrid2.OptionsView.ShowRowGrandTotals = false;
            this.xrPivotGrid2.OptionsView.ShowRowTotals = false;
            this.xrPivotGrid2.Scripts.OnBeforePrint = "xrPivotGrid2_BeforePrint";
            this.xrPivotGrid2.Scripts.OnFieldValueDisplayText = "xrPivotGrid1_FieldValueDisplayText";
            this.xrPivotGrid2.SizeF = new System.Drawing.SizeF(410.7504F, 17F);
            this.xrPivotGrid2.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrPivotGrid2_BeforePrint);
            // 
            // fieldItemCode1
            // 
            this.fieldItemCode1.Appearance.FieldValue.Font = new System.Drawing.Font("Tahoma", 6.75F);
            this.fieldItemCode1.AreaIndex = 0;
            this.fieldItemCode1.Caption = "Item Code";
            this.fieldItemCode1.FieldName = "ItemCode";
            this.fieldItemCode1.Name = "fieldItemCode1";
            this.fieldItemCode1.Width = 50;
            // 
            // fieldColorCode1
            // 
            this.fieldColorCode1.Appearance.Cell.BackColor = System.Drawing.Color.Transparent;
            this.fieldColorCode1.Appearance.FieldHeader.BackColor = System.Drawing.Color.Transparent;
            this.fieldColorCode1.Appearance.FieldValue.BackColor = System.Drawing.Color.Transparent;
            this.fieldColorCode1.Appearance.FieldValue.Font = new System.Drawing.Font("Tahoma", 6.75F);
            this.fieldColorCode1.AreaIndex = 1;
            this.fieldColorCode1.Caption = "Color Code";
            this.fieldColorCode1.FieldName = "ColorCode";
            this.fieldColorCode1.Name = "fieldColorCode1";
            this.fieldColorCode1.Width = 50;
            // 
            // fieldSizeCode1
            // 
            this.fieldSizeCode1.Appearance.FieldValue.BackColor = System.Drawing.Color.Transparent;
            this.fieldSizeCode1.Appearance.FieldValue.BorderColor = System.Drawing.Color.Transparent;
            this.fieldSizeCode1.Appearance.FieldValue.BorderWidth = 0F;
            this.fieldSizeCode1.Appearance.FieldValue.Font = new System.Drawing.Font("Tahoma", 6.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))));
            this.fieldSizeCode1.Appearance.FieldValue.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.fieldSizeCode1.Appearance.FieldValue.TextVerticalAlignment = DevExpress.Utils.VertAlignment.Center;
            this.fieldSizeCode1.Area = DevExpress.XtraPivotGrid.PivotArea.ColumnArea;
            this.fieldSizeCode1.AreaIndex = 0;
            this.fieldSizeCode1.Caption = "Size Code";
            this.fieldSizeCode1.FieldName = "SizeCode";
            this.fieldSizeCode1.Name = "fieldSizeCode1";
            this.fieldSizeCode1.Width = 30;
            // 
            // fieldQty1
            // 
            this.fieldQty1.Appearance.Cell.BackColor = System.Drawing.Color.Transparent;
            this.fieldQty1.Appearance.Cell.BorderColor = System.Drawing.Color.Transparent;
            this.fieldQty1.Appearance.Cell.BorderWidth = 0F;
            this.fieldQty1.Appearance.Cell.Font = new System.Drawing.Font("Vladimir Script", 12F);
            this.fieldQty1.Appearance.Cell.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.fieldQty1.Appearance.Cell.TextVerticalAlignment = DevExpress.Utils.VertAlignment.Center;
            this.fieldQty1.Appearance.FieldValue.BackColor = System.Drawing.Color.Silver;
            this.fieldQty1.Appearance.FieldValue.BorderColor = System.Drawing.Color.Transparent;
            this.fieldQty1.Appearance.FieldValue.Font = new System.Drawing.Font("Tahoma", 6.75F);
            this.fieldQty1.Appearance.FieldValue.ForeColor = System.Drawing.Color.Transparent;
            this.fieldQty1.Appearance.FieldValue.TextHorizontalAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.fieldQty1.Appearance.FieldValue.TextVerticalAlignment = DevExpress.Utils.VertAlignment.Center;
            this.fieldQty1.Area = DevExpress.XtraPivotGrid.PivotArea.DataArea;
            this.fieldQty1.AreaIndex = 0;
            this.fieldQty1.Caption = "Qty";
            this.fieldQty1.CellFormat.FormatString = "{0:#,#}";
            this.fieldQty1.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.fieldQty1.FieldName = "Qty";
            this.fieldQty1.Name = "fieldQty1";
            this.fieldQty1.Width = 30;
            // 
            // xrLabel23
            // 
            this.xrLabel23.Font = new System.Drawing.Font("Tahoma", 10F);
            this.xrLabel23.LocationFloat = new DevExpress.Utils.PointFloat(2.00026F, 32.29167F);
            this.xrLabel23.Multiline = true;
            this.xrLabel23.Name = "xrLabel23";
            this.xrLabel23.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.xrLabel23.Scripts.OnBeforePrint = "xrLabel1_BeforePrint";
            this.xrLabel23.SizeF = new System.Drawing.SizeF(832F, 15F);
            this.xrLabel23.StylePriority.UseFont = false;
            this.xrLabel23.StylePriority.UsePadding = false;
            this.xrLabel23.StylePriority.UseTextAlignment = false;
            this.xrLabel23.Text = "****Nothing Follows****";
            this.xrLabel23.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel21
            // 
            this.xrLabel21.Borders = DevExpress.XtraPrinting.BorderSide.Top;
            this.xrLabel21.BorderWidth = 2F;
            this.xrLabel21.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_wms_report_DR5thCopySMNONEVDR.Qty")});
            this.xrLabel21.Font = new System.Drawing.Font("Arial Narrow", 10F, System.Drawing.FontStyle.Bold);
            this.xrLabel21.LocationFloat = new DevExpress.Utils.PointFloat(9.999998F, 0F);
            this.xrLabel21.Name = "xrLabel21";
            this.xrLabel21.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 10, 0, 0, 100F);
            this.xrLabel21.Scripts.OnBeforePrint = "xrLabel1_BeforePrint";
            this.xrLabel21.SizeF = new System.Drawing.SizeF(80F, 20F);
            this.xrLabel21.StylePriority.UseBorders = false;
            this.xrLabel21.StylePriority.UseBorderWidth = false;
            this.xrLabel21.StylePriority.UseFont = false;
            this.xrLabel21.StylePriority.UsePadding = false;
            this.xrLabel21.StylePriority.UseTextAlignment = false;
            xrSummary4.FormatString = "{0:#,#}";
            xrSummary4.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            this.xrLabel21.Summary = xrSummary4;
            this.xrLabel21.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            // 
            // xrLabel22
            // 
            this.xrLabel22.Borders = DevExpress.XtraPrinting.BorderSide.Top;
            this.xrLabel22.BorderWidth = 2F;
            this.xrLabel22.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_wms_report_DR5thCopySMNONEVDR.Amount")});
            this.xrLabel22.Font = new System.Drawing.Font("Arial Narrow", 10F, System.Drawing.FontStyle.Bold);
            this.xrLabel22.LocationFloat = new DevExpress.Utils.PointFloat(170F, 0F);
            this.xrLabel22.Name = "xrLabel22";
            this.xrLabel22.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 10, 0, 0, 100F);
            this.xrLabel22.Scripts.OnBeforePrint = "xrLabel1_BeforePrint";
            this.xrLabel22.SizeF = new System.Drawing.SizeF(100F, 20F);
            this.xrLabel22.StylePriority.UseBorders = false;
            this.xrLabel22.StylePriority.UseBorderWidth = false;
            this.xrLabel22.StylePriority.UseFont = false;
            this.xrLabel22.StylePriority.UsePadding = false;
            this.xrLabel22.StylePriority.UseTextAlignment = false;
            xrSummary5.FormatString = "{0:n}";
            xrSummary5.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            this.xrLabel22.Summary = xrSummary5;
            this.xrLabel22.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            // 
            // GroupFooter1
            // 
            this.GroupFooter1.HeightF = 0F;
            this.GroupFooter1.KeepTogether = true;
            this.GroupFooter1.Name = "GroupFooter1";
            // 
            // ReportFooter
            // 
            this.ReportFooter.HeightF = 0F;
            this.ReportFooter.Name = "ReportFooter";
            // 
            // GroupFooter2
            // 
            this.GroupFooter2.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel21,
            this.xrLabel22,
            this.xrLabel23});
            this.GroupFooter2.HeightF = 57.29167F;
            this.GroupFooter2.KeepTogether = true;
            this.GroupFooter2.Level = 1;
            this.GroupFooter2.Name = "GroupFooter2";
            // 
            // R_WMS_DRForSMJGI
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.ReportHeader,
            this.PageHeader,
            this.GroupHeader1,
            this.GroupHeader2,
            this.GroupFooter1,
            this.ReportFooter,
            this.GroupFooter2});
            this.CalculatedFields.AddRange(new DevExpress.XtraReports.UI.CalculatedField[] {
            this.Remarks,
            this.DamageType});
            this.ComponentStorage.AddRange(new System.ComponentModel.IComponent[] {
            this.sqlDataSource1});
            this.DataMember = "sp_wms_report_DR5thCopySMNONEVDR";
            this.DataSource = this.sqlDataSource1;
            this.Landscape = true;
            this.Margins = new System.Drawing.Printing.Margins(0, 0, 0, 20);
            this.PaperKind = System.Drawing.Printing.PaperKind.Custom;
            this.Parameters.AddRange(new DevExpress.XtraReports.Parameters.Parameter[] {
            this.DocNumberFrom,
            this.DocNumberTo});
            this.Scripts.OnParametersRequestBeforeShow = "R_WMS_PendingForPutaway_ParametersRequestBeforeShow";
            this.Version = "15.1";
            this.DataSourceDemanded += new System.EventHandler<System.EventArgs>(this.R_WMS_PendingForPutaway_DataSourceDemanded);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private DevExpress.DataAccess.Sql.SqlDataSource sqlDataSource1;
        //private DevExpress.XtraTabbedMdi.XtraTabbedMdiManager xtraTabbedMdiManager1;
        //private DevExpress.XtraBars.Ribbon.GalleryDropDown galleryDropDown1;
        //private DevExpress.XtraBars.Ribbon.GalleryDropDown galleryDropDown2;
        private DevExpress.XtraReports.UI.ReportHeaderBand ReportHeader;
        //private DevExpress.XtraEditors.DateTimeChartRangeControlClient dateTimeChartRangeControlClient1;
        private DevExpress.XtraReports.UI.PageHeaderBand PageHeader;
        private DevExpress.XtraReports.UI.CalculatedField Remarks;
        private DevExpress.XtraReports.UI.CalculatedField DamageType;
        private DevExpress.XtraReports.Parameters.Parameter DocNumberFrom;
        private DevExpress.XtraReports.Parameters.Parameter DocNumberTo;
        private DevExpress.XtraReports.UI.XRLabel xrLabel1;
        private DevExpress.XtraReports.UI.XRLabel xrLabel5;
        private DevExpress.XtraReports.UI.XRLabel xrLabel8;
        private DevExpress.XtraReports.UI.XRLabel xrLabel4;
        private DevExpress.XtraReports.UI.XRLabel xrLabel7;
        private DevExpress.XtraReports.UI.XRLabel xrLabel10;
        private DevExpress.XtraReports.UI.XRLabel xrLabel9;
        private DevExpress.XtraReports.UI.XRLabel xrLabel6;
        private DevExpress.XtraReports.UI.XRLabel xrLabel11;
        private DevExpress.XtraReports.UI.XRLabel xrLabel12;
        private DevExpress.XtraReports.UI.GroupHeaderBand GroupHeader1;
        private DevExpress.XtraReports.UI.GroupHeaderBand GroupHeader2;
        private DevExpress.XtraReports.UI.XRPivotGrid xrPivotGrid2;
        private DevExpress.XtraReports.UI.XRLabel xrLabel16;
        private DevExpress.XtraReports.UI.XRLabel xrLabel13;
        private DevExpress.XtraReports.UI.XRLabel xrLabel17;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField fieldSizeCode1;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField fieldQty1;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField fieldItemCode1;
        private DevExpress.XtraReports.UI.PivotGrid.XRPivotGridField fieldColorCode1;
        private DevExpress.XtraReports.UI.XRLabel xrLabel19;
        private DevExpress.XtraReports.UI.XRLabel xrLabel20;
        private DevExpress.XtraReports.UI.XRLabel xrLabel21;
        private DevExpress.XtraReports.UI.XRLabel xrLabel22;
        private DevExpress.XtraReports.UI.XRLabel xrLabel23;
        private DevExpress.XtraReports.UI.GroupFooterBand GroupFooter1;
        private DevExpress.XtraReports.UI.XRLabel xrLabel24;
        private DevExpress.XtraReports.UI.XRLabel xrLabel26;
        private DevExpress.XtraReports.UI.XRLabel xrLabel27;
        private DevExpress.XtraReports.UI.ReportFooterBand ReportFooter;
        private DevExpress.XtraReports.UI.GroupFooterBand GroupFooter2;
        private DevExpress.XtraReports.UI.XRLabel xrLabel2;
    }
}
