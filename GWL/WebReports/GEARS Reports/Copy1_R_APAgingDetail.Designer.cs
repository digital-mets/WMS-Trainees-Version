using System;
using System.Windows.Forms;
using DevExpress.XtraReports.UI;
using DevExpress.XtraReports.Parameters;


using GearsLibrary;
using System.Data;

namespace GWL.WebReports.GEARS_Reports
{
    partial class R_APAgingDetail

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
        /// 
         
        private void InitializeComponent()
        {
            DevExpress.DataAccess.Sql.CustomSqlQuery customSqlQuery1 = new DevExpress.DataAccess.Sql.CustomSqlQuery();
            DevExpress.DataAccess.Sql.CustomSqlQuery customSqlQuery2 = new DevExpress.DataAccess.Sql.CustomSqlQuery();
            DevExpress.DataAccess.Sql.CustomSqlQuery customSqlQuery3 = new DevExpress.DataAccess.Sql.CustomSqlQuery();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(R_APAgingDetail));
            DevExpress.DataAccess.Sql.CustomSqlQuery customSqlQuery4 = new DevExpress.DataAccess.Sql.CustomSqlQuery();
            DevExpress.DataAccess.Sql.CustomSqlQuery customSqlQuery5 = new DevExpress.DataAccess.Sql.CustomSqlQuery();
            DevExpress.DataAccess.Sql.StoredProcQuery storedProcQuery1 = new DevExpress.DataAccess.Sql.StoredProcQuery();
            DevExpress.DataAccess.Sql.QueryParameter queryParameter1 = new DevExpress.DataAccess.Sql.QueryParameter();
            DevExpress.DataAccess.Sql.QueryParameter queryParameter2 = new DevExpress.DataAccess.Sql.QueryParameter();
            DevExpress.DataAccess.Sql.QueryParameter queryParameter3 = new DevExpress.DataAccess.Sql.QueryParameter();
            DevExpress.DataAccess.Sql.QueryParameter queryParameter4 = new DevExpress.DataAccess.Sql.QueryParameter();
            DevExpress.XtraReports.UI.XRSummary xrSummary1 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary2 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary3 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary4 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary5 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary6 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary7 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary8 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary9 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary10 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary11 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary12 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary13 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary14 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary15 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary16 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary17 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary18 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary19 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary20 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary21 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary22 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary23 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary24 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary25 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary26 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.Parameters.DynamicListLookUpSettings dynamicListLookUpSettings1 = new DevExpress.XtraReports.Parameters.DynamicListLookUpSettings();
            DevExpress.XtraReports.Parameters.DynamicListLookUpSettings dynamicListLookUpSettings2 = new DevExpress.XtraReports.Parameters.DynamicListLookUpSettings();
            DevExpress.XtraReports.Parameters.DynamicListLookUpSettings dynamicListLookUpSettings3 = new DevExpress.XtraReports.Parameters.DynamicListLookUpSettings();
            this.sqlDataSource1 = new DevExpress.DataAccess.Sql.SqlDataSource();
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.Table3 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow5 = new DevExpress.XtraReports.UI.XRTableRow();
            this.Detail1 = new DevExpress.XtraReports.UI.XRTableCell();
            this.Detail2 = new DevExpress.XtraReports.UI.XRTableCell();
            this.Detail3 = new DevExpress.XtraReports.UI.XRTableCell();
            this.Detail4 = new DevExpress.XtraReports.UI.XRTableCell();
            this.Detail5 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell28 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell29 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell27 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell22 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell56 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell57 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell58 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell59 = new DevExpress.XtraReports.UI.XRTableCell();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.xrPageInfo3 = new DevExpress.XtraReports.UI.XRPageInfo();
            this.xrLabel3 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrPageInfo2 = new DevExpress.XtraReports.UI.XRPageInfo();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.xrLabel18 = new DevExpress.XtraReports.UI.XRLabel();
            this.Head = new DevExpress.XtraReports.UI.ReportHeaderBand();
            this.xrLabel4 = new DevExpress.XtraReports.UI.XRLabel();
            this.Table1 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
            this.Header1 = new DevExpress.XtraReports.UI.XRTableCell();
            this.Header2 = new DevExpress.XtraReports.UI.XRTableCell();
            this.Header3 = new DevExpress.XtraReports.UI.XRTableCell();
            this.Header4 = new DevExpress.XtraReports.UI.XRTableCell();
            this.Header5 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell6 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell11 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell12 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell15 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell34 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel15 = new DevExpress.XtraReports.UI.XRLabel();
            this.formattingRule1 = new DevExpress.XtraReports.UI.FormattingRule();
            this.GroupFooter1 = new DevExpress.XtraReports.UI.GroupFooterBand();
            this.Table4 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow7 = new DevExpress.XtraReports.UI.XRTableRow();
            this.GroupF1 = new DevExpress.XtraReports.UI.XRTableCell();
            this.GroupF2 = new DevExpress.XtraReports.UI.XRTableCell();
            this.GroupF3 = new DevExpress.XtraReports.UI.XRTableCell();
            this.GroupF4 = new DevExpress.XtraReports.UI.XRTableCell();
            this.GroupF5 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell21 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell36 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell37 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell38 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell39 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell40 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell62 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell63 = new DevExpress.XtraReports.UI.XRTableCell();
            this.CutOff = new DevExpress.XtraReports.Parameters.Parameter();
            this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
            this.Table6 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow12 = new DevExpress.XtraReports.UI.XRTableRow();
            this.ReportFF1 = new DevExpress.XtraReports.UI.XRTableCell();
            this.ReportFF2 = new DevExpress.XtraReports.UI.XRTableCell();
            this.ReportFF3 = new DevExpress.XtraReports.UI.XRTableCell();
            this.ReportFF4 = new DevExpress.XtraReports.UI.XRTableCell();
            this.ReportFF5 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell19 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell20 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell23 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell26 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell33 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell35 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell41 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell42 = new DevExpress.XtraReports.UI.XRTableCell();
            this.Table5 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow13 = new DevExpress.XtraReports.UI.XRTableRow();
            this.ReportF1 = new DevExpress.XtraReports.UI.XRTableCell();
            this.ReportF2 = new DevExpress.XtraReports.UI.XRTableCell();
            this.ReportF3 = new DevExpress.XtraReports.UI.XRTableCell();
            this.ReportF4 = new DevExpress.XtraReports.UI.XRTableCell();
            this.ReportF5 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell47 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell48 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell50 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell51 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell52 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell53 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell60 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell61 = new DevExpress.XtraReports.UI.XRTableCell();
            this.GroupField = new DevExpress.XtraReports.UI.CalculatedField();
            this.BusinessSubClass = new DevExpress.XtraReports.Parameters.Parameter();
            this.Supplier = new DevExpress.XtraReports.Parameters.Parameter();
            this.GLAPAccount = new DevExpress.XtraReports.Parameters.Parameter();
            this.Table2 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow10 = new DevExpress.XtraReports.UI.XRTableRow();
            this.GroupH1 = new DevExpress.XtraReports.UI.XRTableCell();
            this.GroupH2 = new DevExpress.XtraReports.UI.XRTableCell();
            this.GroupH3 = new DevExpress.XtraReports.UI.XRTableCell();
            this.GroupH4 = new DevExpress.XtraReports.UI.XRTableCell();
            this.GroupH5 = new DevExpress.XtraReports.UI.XRTableCell();
            this.GroupHeader1 = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.ShowDueDate = new DevExpress.XtraReports.Parameters.Parameter();
            this.ShowTerms = new DevExpress.XtraReports.Parameters.Parameter();
            this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
            ((System.ComponentModel.ISupportInitialize)(this.Table3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Table1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Table4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Table6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Table5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Table2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // sqlDataSource1
            // 
            this.sqlDataSource1.ConnectionName = "GEARS-METSITConnectionString";
            this.sqlDataSource1.Name = "sqlDataSource1";
            customSqlQuery1.Name = "CompanyName";
            customSqlQuery1.Sql = "SELECT Value FROM IT.SystemSettings WHERE Code = \'COMPNAMEAC\'";
            customSqlQuery2.Name = "CompanyAddress";
            customSqlQuery2.Sql = "SELECT Value FROM IT.SystemSettings WHERE Code = \'COMPADDR\'";
            customSqlQuery3.Name = "BusinessSubClass";
            customSqlQuery3.Sql = resources.GetString("customSqlQuery3.Sql");
            customSqlQuery4.Name = "Supplier";
            customSqlQuery4.Sql = resources.GetString("customSqlQuery4.Sql");
            customSqlQuery5.Name = "GLAPAccount";
            customSqlQuery5.Sql = resources.GetString("customSqlQuery5.Sql");
            storedProcQuery1.Name = "sp_report_APAgingDetail";
            queryParameter1.Name = "@CutOff";
            queryParameter1.Type = typeof(DevExpress.DataAccess.Expression);
            queryParameter1.Value = new DevExpress.DataAccess.Expression("[Parameters.CutOff]", typeof(System.DateTime));
            queryParameter2.Name = "@BusinessSub";
            queryParameter2.Type = typeof(DevExpress.DataAccess.Expression);
            queryParameter2.Value = new DevExpress.DataAccess.Expression("ToStr([Parameters.BusinessSubClass])", typeof(string));
            queryParameter3.Name = "@Supplier";
            queryParameter3.Type = typeof(DevExpress.DataAccess.Expression);
            queryParameter3.Value = new DevExpress.DataAccess.Expression("ToStr([Parameters.Supplier])", typeof(string));
            queryParameter4.Name = "@APGLAccount";
            queryParameter4.Type = typeof(DevExpress.DataAccess.Expression);
            queryParameter4.Value = new DevExpress.DataAccess.Expression("ToStr([Parameters.GLAPAccount])", typeof(string));
            storedProcQuery1.Parameters.Add(queryParameter1);
            storedProcQuery1.Parameters.Add(queryParameter2);
            storedProcQuery1.Parameters.Add(queryParameter3);
            storedProcQuery1.Parameters.Add(queryParameter4);
            storedProcQuery1.StoredProcName = "sp_report_APAgingDetail";
            this.sqlDataSource1.Queries.AddRange(new DevExpress.DataAccess.Sql.SqlQuery[] {
            customSqlQuery1,
            customSqlQuery2,
            customSqlQuery3,
            customSqlQuery4,
            customSqlQuery5,
            storedProcQuery1});
            this.sqlDataSource1.ResultSchemaSerializable = resources.GetString("sqlDataSource1.ResultSchemaSerializable");
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.Table3});
            this.Detail.HeightF = 25F;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // Table3
            // 
            this.Table3.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.Table3.Font = new System.Drawing.Font("Arial Narrow", 8F);
            this.Table3.LocationFloat = new DevExpress.Utils.PointFloat(73.55004F, 0F);
            this.Table3.Name = "Table3";
            this.Table3.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow5});
            this.Table3.SizeF = new System.Drawing.SizeF(910F, 25F);
            this.Table3.StylePriority.UseBorders = false;
            this.Table3.StylePriority.UseFont = false;
            this.Table3.StylePriority.UseTextAlignment = false;
            this.Table3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrTableRow5
            // 
            this.xrTableRow5.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.Detail1,
            this.Detail2,
            this.Detail3,
            this.Detail4,
            this.Detail5,
            this.xrTableCell28,
            this.xrTableCell29,
            this.xrTableCell27,
            this.xrTableCell22,
            this.xrTableCell56,
            this.xrTableCell57,
            this.xrTableCell58,
            this.xrTableCell59});
            this.xrTableRow5.Name = "xrTableRow5";
            this.xrTableRow5.Weight = 1D;
            // 
            // Detail1
            // 
            this.Detail1.BorderColor = System.Drawing.Color.Gainsboro;
            this.Detail1.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.Detail1.BorderWidth = 2F;
            this.Detail1.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_APAgingDetail.DocDate", "{0:MM/dd/yy}")});
            this.Detail1.Name = "Detail1";
            this.Detail1.StylePriority.UseBorderColor = false;
            this.Detail1.StylePriority.UseBorders = false;
            this.Detail1.StylePriority.UseBorderWidth = false;
            this.Detail1.StylePriority.UseTextAlignment = false;
            this.Detail1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.Detail1.Weight = 0.70659821054114D;
            this.Detail1.HtmlItemCreated += new DevExpress.XtraReports.UI.HtmlEventHandler(this.Detail1_HtmlItemCreated);
            // 
            // Detail2
            // 
            this.Detail2.BorderColor = System.Drawing.Color.Gainsboro;
            this.Detail2.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.Detail2.BorderWidth = 2F;
            this.Detail2.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_APAgingDetail.TransType")});
            this.Detail2.Name = "Detail2";
            this.Detail2.StylePriority.UseBorderColor = false;
            this.Detail2.StylePriority.UseBorders = false;
            this.Detail2.StylePriority.UseBorderWidth = false;
            this.Detail2.StylePriority.UseTextAlignment = false;
            this.Detail2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.Detail2.Weight = 0.70659823358743723D;
            this.Detail2.HtmlItemCreated += new DevExpress.XtraReports.UI.HtmlEventHandler(this.Detail2_HtmlItemCreated_1);
            // 
            // Detail3
            // 
            this.Detail3.BorderColor = System.Drawing.Color.Gainsboro;
            this.Detail3.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.Detail3.BorderWidth = 2F;
            this.Detail3.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_APAgingDetail.DocNumber")});
            this.Detail3.Name = "Detail3";
            this.Detail3.StylePriority.UseBorderColor = false;
            this.Detail3.StylePriority.UseBorders = false;
            this.Detail3.StylePriority.UseBorderWidth = false;
            this.Detail3.StylePriority.UseTextAlignment = false;
            this.Detail3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.Detail3.Weight = 0.706598240018434D;
            this.Detail3.HtmlItemCreated += new DevExpress.XtraReports.UI.HtmlEventHandler(this.Detail3_HtmlItemCreated_1);
            // 
            // Detail4
            // 
            this.Detail4.BorderColor = System.Drawing.Color.Gainsboro;
            this.Detail4.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.Detail4.BorderWidth = 2F;
            this.Detail4.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_APAgingDetail.Terms")});
            this.Detail4.Name = "Detail4";
            this.Detail4.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.Detail4.StylePriority.UseBorderColor = false;
            this.Detail4.StylePriority.UseBorders = false;
            this.Detail4.StylePriority.UseBorderWidth = false;
            this.Detail4.StylePriority.UsePadding = false;
            this.Detail4.StylePriority.UseTextAlignment = false;
            this.Detail4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.Detail4.Weight = 0.706598219261775D;
            // 
            // Detail5
            // 
            this.Detail5.BorderColor = System.Drawing.Color.Gainsboro;
            this.Detail5.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.Detail5.BorderWidth = 2F;
            this.Detail5.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_APAgingDetail.DueDate", "{0:MM/dd/yy}")});
            this.Detail5.Name = "Detail5";
            this.Detail5.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.Detail5.StylePriority.UseBorderColor = false;
            this.Detail5.StylePriority.UseBorders = false;
            this.Detail5.StylePriority.UseBorderWidth = false;
            this.Detail5.StylePriority.UsePadding = false;
            this.Detail5.StylePriority.UseTextAlignment = false;
            this.Detail5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.Detail5.Weight = 0.70659822628668811D;
            // 
            // xrTableCell28
            // 
            this.xrTableCell28.BorderColor = System.Drawing.Color.Gainsboro;
            this.xrTableCell28.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrTableCell28.BorderWidth = 2F;
            this.xrTableCell28.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_APAgingDetail.Day1to30", "{0:#,0.00;(#,0.00);}")});
            this.xrTableCell28.Name = "xrTableCell28";
            this.xrTableCell28.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 100F);
            this.xrTableCell28.StylePriority.UseBorderColor = false;
            this.xrTableCell28.StylePriority.UseBorders = false;
            this.xrTableCell28.StylePriority.UseBorderWidth = false;
            this.xrTableCell28.StylePriority.UsePadding = false;
            this.xrTableCell28.StylePriority.UseTextAlignment = false;
            this.xrTableCell28.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell28.Weight = 0.70659825048003733D;
            // 
            // xrTableCell29
            // 
            this.xrTableCell29.BorderColor = System.Drawing.Color.Gainsboro;
            this.xrTableCell29.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrTableCell29.BorderWidth = 2F;
            this.xrTableCell29.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_APAgingDetail.Day31to60", "{0:#,0.00;(#,0.00);}")});
            this.xrTableCell29.Multiline = true;
            this.xrTableCell29.Name = "xrTableCell29";
            this.xrTableCell29.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 100F);
            this.xrTableCell29.StylePriority.UseBorderColor = false;
            this.xrTableCell29.StylePriority.UseBorders = false;
            this.xrTableCell29.StylePriority.UseBorderWidth = false;
            this.xrTableCell29.StylePriority.UsePadding = false;
            this.xrTableCell29.StylePriority.UseTextAlignment = false;
            this.xrTableCell29.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell29.Weight = 0.7065982521781542D;
            // 
            // xrTableCell27
            // 
            this.xrTableCell27.BorderColor = System.Drawing.Color.Gainsboro;
            this.xrTableCell27.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrTableCell27.BorderWidth = 2F;
            this.xrTableCell27.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_APAgingDetail.Day61to90", "{0:#,0.00;(#,0.00);}")});
            this.xrTableCell27.Name = "xrTableCell27";
            this.xrTableCell27.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 100F);
            this.xrTableCell27.StylePriority.UseBorderColor = false;
            this.xrTableCell27.StylePriority.UseBorders = false;
            this.xrTableCell27.StylePriority.UseBorderWidth = false;
            this.xrTableCell27.StylePriority.UsePadding = false;
            this.xrTableCell27.StylePriority.UseTextAlignment = false;
            this.xrTableCell27.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell27.Weight = 0.70659762699230955D;
            // 
            // xrTableCell22
            // 
            this.xrTableCell22.BorderColor = System.Drawing.Color.Gainsboro;
            this.xrTableCell22.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrTableCell22.BorderWidth = 2F;
            this.xrTableCell22.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_APAgingDetail.Day91to120", "{0:#,0.00;(#,0.00);}")});
            this.xrTableCell22.Name = "xrTableCell22";
            this.xrTableCell22.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 100F);
            this.xrTableCell22.StylePriority.UseBorderColor = false;
            this.xrTableCell22.StylePriority.UseBorders = false;
            this.xrTableCell22.StylePriority.UseBorderWidth = false;
            this.xrTableCell22.StylePriority.UsePadding = false;
            this.xrTableCell22.StylePriority.UseTextAlignment = false;
            this.xrTableCell22.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell22.Weight = 0.70659947740918683D;
            // 
            // xrTableCell56
            // 
            this.xrTableCell56.BorderColor = System.Drawing.Color.Gainsboro;
            this.xrTableCell56.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrTableCell56.BorderWidth = 2F;
            this.xrTableCell56.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_APAgingDetail.Day121to150", "{0:#,0.00;(#,0.00);}")});
            this.xrTableCell56.Name = "xrTableCell56";
            this.xrTableCell56.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 100F);
            this.xrTableCell56.StylePriority.UseBorderColor = false;
            this.xrTableCell56.StylePriority.UseBorders = false;
            this.xrTableCell56.StylePriority.UseBorderWidth = false;
            this.xrTableCell56.StylePriority.UsePadding = false;
            this.xrTableCell56.StylePriority.UseTextAlignment = false;
            this.xrTableCell56.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell56.Weight = 0.70659824876644151D;
            // 
            // xrTableCell57
            // 
            this.xrTableCell57.BorderColor = System.Drawing.Color.Gainsboro;
            this.xrTableCell57.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrTableCell57.BorderWidth = 2F;
            this.xrTableCell57.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_APAgingDetail.Day151to180", "{0:#,0.00;(#,0.00);}")});
            this.xrTableCell57.Name = "xrTableCell57";
            this.xrTableCell57.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 100F);
            this.xrTableCell57.StylePriority.UseBorderColor = false;
            this.xrTableCell57.StylePriority.UseBorders = false;
            this.xrTableCell57.StylePriority.UseBorderWidth = false;
            this.xrTableCell57.StylePriority.UsePadding = false;
            this.xrTableCell57.StylePriority.UseTextAlignment = false;
            this.xrTableCell57.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell57.Weight = 0.70659759463862792D;
            // 
            // xrTableCell58
            // 
            this.xrTableCell58.BorderColor = System.Drawing.Color.Gainsboro;
            this.xrTableCell58.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrTableCell58.BorderWidth = 2F;
            this.xrTableCell58.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_APAgingDetail.Day180More", "{0:#,0.00;(#,0.00);}")});
            this.xrTableCell58.Name = "xrTableCell58";
            this.xrTableCell58.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 100F);
            this.xrTableCell58.StylePriority.UseBorderColor = false;
            this.xrTableCell58.StylePriority.UseBorders = false;
            this.xrTableCell58.StylePriority.UseBorderWidth = false;
            this.xrTableCell58.StylePriority.UsePadding = false;
            this.xrTableCell58.StylePriority.UseTextAlignment = false;
            this.xrTableCell58.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell58.Weight = 0.70659867175692181D;
            // 
            // xrTableCell59
            // 
            this.xrTableCell59.BorderColor = System.Drawing.Color.Gainsboro;
            this.xrTableCell59.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrTableCell59.BorderWidth = 2F;
            this.xrTableCell59.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_APAgingDetail.APTotal", "{0:#,0.00;(#,0.00);}")});
            this.xrTableCell59.Name = "xrTableCell59";
            this.xrTableCell59.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 100F);
            this.xrTableCell59.StylePriority.UseBorderColor = false;
            this.xrTableCell59.StylePriority.UseBorders = false;
            this.xrTableCell59.StylePriority.UseBorderWidth = false;
            this.xrTableCell59.StylePriority.UsePadding = false;
            this.xrTableCell59.StylePriority.UseTextAlignment = false;
            this.xrTableCell59.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell59.Weight = 0.706597706940227D;
            this.xrTableCell59.HtmlItemCreated += new DevExpress.XtraReports.UI.HtmlEventHandler(this.xrTableCell59_HtmlItemCreated);
            // 
            // TopMargin
            // 
            this.TopMargin.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPageInfo3,
            this.xrLabel3,
            this.xrPageInfo2});
            this.TopMargin.HeightF = 40.00001F;
            this.TopMargin.Name = "TopMargin";
            this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrPageInfo3
            // 
            this.xrPageInfo3.Font = new System.Drawing.Font("Times New Roman", 8F);
            this.xrPageInfo3.Format = "Page {0} of {1}";
            this.xrPageInfo3.LocationFloat = new DevExpress.Utils.PointFloat(972.2917F, 23.25001F);
            this.xrPageInfo3.Name = "xrPageInfo3";
            this.xrPageInfo3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrPageInfo3.SizeF = new System.Drawing.SizeF(87.70837F, 16.75F);
            this.xrPageInfo3.StylePriority.UseFont = false;
            this.xrPageInfo3.StylePriority.UseTextAlignment = false;
            this.xrPageInfo3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            // 
            // xrLabel3
            // 
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
            // xrPageInfo2
            // 
            this.xrPageInfo2.Font = new System.Drawing.Font("Times New Roman", 8F);
            this.xrPageInfo2.Format = "Run Date & Time : {0:MMMM dd, yyyy / h:mm tt}";
            this.xrPageInfo2.LocationFloat = new DevExpress.Utils.PointFloat(69.99995F, 23.25001F);
            this.xrPageInfo2.Name = "xrPageInfo2";
            this.xrPageInfo2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrPageInfo2.PageInfo = DevExpress.XtraPrinting.PageInfo.DateTime;
            this.xrPageInfo2.SizeF = new System.Drawing.SizeF(320.4167F, 16.75F);
            this.xrPageInfo2.StylePriority.UseFont = false;
            // 
            // BottomMargin
            // 
            this.BottomMargin.HeightF = 41.04166F;
            this.BottomMargin.Name = "BottomMargin";
            this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.BottomMargin.Scripts.OnAfterPrint = "BottomMargin_AfterPrint";
            this.BottomMargin.StylePriority.UseBorders = false;
            this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrLabel18
            // 
            this.xrLabel18.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "CompanyName.Value")});
            this.xrLabel18.Font = new System.Drawing.Font("Arial Narrow", 9.75F);
            this.xrLabel18.LocationFloat = new DevExpress.Utils.PointFloat(0F, 39.99999F);
            this.xrLabel18.Name = "xrLabel18";
            this.xrLabel18.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel18.SizeF = new System.Drawing.SizeF(1060F, 23.00002F);
            this.xrLabel18.StylePriority.UseFont = false;
            this.xrLabel18.StylePriority.UseTextAlignment = false;
            this.xrLabel18.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // Head
            // 
            this.Head.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel4,
            this.xrLabel1,
            this.xrLabel18,
            this.xrLabel15});
            this.Head.HeightF = 148.6817F;
            this.Head.Name = "Head";
            this.Head.Scripts.OnBeforePrint = "ReportHeader_BeforePrint";
            this.Head.StylePriority.UseBorderWidth = false;
            this.Head.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.ReportHeader_BeforePrint);
            // 
            // xrLabel4
            // 
            this.xrLabel4.Font = new System.Drawing.Font("Arial Narrow", 8F, System.Drawing.FontStyle.Bold);
            this.xrLabel4.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrLabel4.Name = "xrLabel4";
            this.xrLabel4.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel4.Scripts.OnBeforePrint = "xrLabel5_BeforePrint";
            this.xrLabel4.SizeF = new System.Drawing.SizeF(70F, 40F);
            this.xrLabel4.StylePriority.UseFont = false;
            this.xrLabel4.StylePriority.UseTextAlignment = false;
            xrSummary2.FormatString = "{0:M/d/yy}";
            xrSummary2.Func = DevExpress.XtraReports.UI.SummaryFunc.Custom;
            this.xrLabel4.Summary = xrSummary2;
            this.xrLabel4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // Table1
            // 
            this.Table1.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.Table1.Font = new System.Drawing.Font("Arial Narrow", 8F, System.Drawing.FontStyle.Bold);
            this.Table1.LocationFloat = new DevExpress.Utils.PointFloat(73.55008F, 0F);
            this.Table1.Name = "Table1";
            this.Table1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow2});
            this.Table1.Scripts.OnBeforePrint = "xrTableRow2_BeforePrint";
            this.Table1.SizeF = new System.Drawing.SizeF(910F, 25.00002F);
            this.Table1.StylePriority.UseBorders = false;
            this.Table1.StylePriority.UseFont = false;
            this.Table1.StylePriority.UseTextAlignment = false;
            this.Table1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrTableRow2
            // 
            this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.Header1,
            this.Header2,
            this.Header3,
            this.Header4,
            this.Header5,
            this.xrTableCell1,
            this.xrTableCell3,
            this.xrTableCell6,
            this.xrTableCell11,
            this.xrTableCell12,
            this.xrTableCell15,
            this.xrTableCell2,
            this.xrTableCell34});
            this.xrTableRow2.Name = "xrTableRow2";
            this.xrTableRow2.Weight = 1D;
            // 
            // Header1
            // 
            this.Header1.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.Header1.Name = "Header1";
            this.Header1.StylePriority.UseBorders = false;
            this.Header1.StylePriority.UseFont = false;
            this.Header1.StylePriority.UseTextAlignment = false;
            this.Header1.Text = "DocDate";
            this.Header1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.Header1.Weight = 0.83010694571165222D;
            // 
            // Header2
            // 
            this.Header2.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.Header2.Name = "Header2";
            this.Header2.StylePriority.UseBorders = false;
            this.Header2.StylePriority.UseTextAlignment = false;
            this.Header2.Text = "Trans";
            this.Header2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.Header2.Weight = 0.83010694571165211D;
            this.Header2.HtmlItemCreated += new DevExpress.XtraReports.UI.HtmlEventHandler(this.Header2_HtmlItemCreated_1);
            // 
            // Header3
            // 
            this.Header3.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.Header3.Name = "Header3";
            this.Header3.StylePriority.UseBorders = false;
            this.Header3.StylePriority.UseTextAlignment = false;
            this.Header3.Text = "DocNumber";
            this.Header3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.Header3.Weight = 0.83010694041194144D;
            this.Header3.HtmlItemCreated += new DevExpress.XtraReports.UI.HtmlEventHandler(this.Header3_HtmlItemCreated_1);
            // 
            // Header4
            // 
            this.Header4.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.Header4.Name = "Header4";
            this.Header4.StylePriority.UseBorders = false;
            this.Header4.StylePriority.UseFont = false;
            this.Header4.Text = "Terms";
            this.Header4.Weight = 0.83010686338021888D;
            // 
            // Header5
            // 
            this.Header5.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.Header5.Name = "Header5";
            this.Header5.StylePriority.UseBorders = false;
            this.Header5.Text = "DueDate";
            this.Header5.Weight = 0.83010695385469568D;
            // 
            // xrTableCell1
            // 
            this.xrTableCell1.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell1.Name = "xrTableCell1";
            this.xrTableCell1.StylePriority.UseBorders = false;
            this.xrTableCell1.StylePriority.UseFont = false;
            this.xrTableCell1.Text = "1-30Days";
            this.xrTableCell1.Weight = 0.83010703085822213D;
            // 
            // xrTableCell3
            // 
            this.xrTableCell3.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell3.Name = "xrTableCell3";
            this.xrTableCell3.StylePriority.UseBorders = false;
            this.xrTableCell3.StylePriority.UseFont = false;
            this.xrTableCell3.Text = "31-60Days";
            this.xrTableCell3.Weight = 0.83010691934383285D;
            // 
            // xrTableCell6
            // 
            this.xrTableCell6.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell6.Name = "xrTableCell6";
            this.xrTableCell6.StylePriority.UseBorders = false;
            this.xrTableCell6.StylePriority.UseFont = false;
            this.xrTableCell6.StylePriority.UseTextAlignment = false;
            this.xrTableCell6.Text = "61-90Days";
            this.xrTableCell6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell6.Weight = 0.83010694540428009D;
            // 
            // xrTableCell11
            // 
            this.xrTableCell11.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell11.Name = "xrTableCell11";
            this.xrTableCell11.Scripts.OnBeforePrint = "xrTableCell11_BeforePrint";
            this.xrTableCell11.StylePriority.UseBorders = false;
            this.xrTableCell11.StylePriority.UseFont = false;
            this.xrTableCell11.StylePriority.UseTextAlignment = false;
            this.xrTableCell11.Text = "91-120Days";
            this.xrTableCell11.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell11.Weight = 0.83010699151836631D;
            // 
            // xrTableCell12
            // 
            this.xrTableCell12.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell12.Name = "xrTableCell12";
            this.xrTableCell12.StylePriority.UseBorders = false;
            this.xrTableCell12.StylePriority.UseFont = false;
            this.xrTableCell12.StylePriority.UseTextAlignment = false;
            this.xrTableCell12.Text = "121-150Days";
            this.xrTableCell12.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell12.Weight = 0.830106981730603D;
            // 
            // xrTableCell15
            // 
            this.xrTableCell15.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell15.Name = "xrTableCell15";
            this.xrTableCell15.StylePriority.UseBorders = false;
            this.xrTableCell15.StylePriority.UseFont = false;
            this.xrTableCell15.StylePriority.UseTextAlignment = false;
            this.xrTableCell15.Text = "151-180Days";
            this.xrTableCell15.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell15.Weight = 0.83010695453533634D;
            // 
            // xrTableCell2
            // 
            this.xrTableCell2.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell2.Name = "xrTableCell2";
            this.xrTableCell2.StylePriority.UseBorders = false;
            this.xrTableCell2.StylePriority.UseTextAlignment = false;
            this.xrTableCell2.Text = "181 - Over";
            this.xrTableCell2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell2.Weight = 0.83010695453533612D;
            // 
            // xrTableCell34
            // 
            this.xrTableCell34.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell34.Name = "xrTableCell34";
            this.xrTableCell34.StylePriority.UseBorders = false;
            this.xrTableCell34.StylePriority.UseTextAlignment = false;
            this.xrTableCell34.Text = "AP Total";
            this.xrTableCell34.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell34.Weight = 0.83010688771448149D;
            // 
            // xrLabel1
            // 
            this.xrLabel1.Font = new System.Drawing.Font("Arial Narrow", 8F, System.Drawing.FontStyle.Bold);
            this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(0.0002066294F, 86.00006F);
            this.xrLabel1.Name = "xrLabel1";
            this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel1.Scripts.OnBeforePrint = "xrLabel1_BeforePrint";
            this.xrLabel1.SizeF = new System.Drawing.SizeF(1060F, 21.16175F);
            this.xrLabel1.StylePriority.UseFont = false;
            this.xrLabel1.StylePriority.UsePadding = false;
            this.xrLabel1.StylePriority.UseTextAlignment = false;
            this.xrLabel1.Text = "Cut-Off Date: [Parameters.CutOff!MMMM dd, yyyy]";
            this.xrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel15
            // 
            this.xrLabel15.Font = new System.Drawing.Font("Arial Narrow", 13F, System.Drawing.FontStyle.Bold);
            this.xrLabel15.LocationFloat = new DevExpress.Utils.PointFloat(0.0002066294F, 62.99998F);
            this.xrLabel15.Name = "xrLabel15";
            this.xrLabel15.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel15.SizeF = new System.Drawing.SizeF(1060F, 23.00002F);
            this.xrLabel15.StylePriority.UseFont = false;
            this.xrLabel15.StylePriority.UseTextAlignment = false;
            this.xrLabel15.Text = "AGING OF ACCOUNTS PAYABLE(DETAIL)";
            this.xrLabel15.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // formattingRule1
            // 
            this.formattingRule1.Name = "formattingRule1";
            // 
            // GroupFooter1
            // 
            this.GroupFooter1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.Table4});
            this.GroupFooter1.HeightF = 25.00002F;
            this.GroupFooter1.Name = "GroupFooter1";
            this.GroupFooter1.Scripts.OnBeforePrint = "GroupFooter1_BeforePrint";
            // 
            // Table4
            // 
            this.Table4.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.Table4.Font = new System.Drawing.Font("Arial Narrow", 8F, System.Drawing.FontStyle.Bold);
            this.Table4.LocationFloat = new DevExpress.Utils.PointFloat(73.54999F, 0F);
            this.Table4.Name = "Table4";
            this.Table4.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow7});
            this.Table4.Scripts.OnBeforePrint = "xrTableRow2_BeforePrint";
            this.Table4.SizeF = new System.Drawing.SizeF(910F, 25.00002F);
            this.Table4.StylePriority.UseBorders = false;
            this.Table4.StylePriority.UseFont = false;
            this.Table4.StylePriority.UseTextAlignment = false;
            this.Table4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrTableRow7
            // 
            this.xrTableRow7.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.GroupF1,
            this.GroupF2,
            this.GroupF3,
            this.GroupF4,
            this.GroupF5,
            this.xrTableCell21,
            this.xrTableCell36,
            this.xrTableCell37,
            this.xrTableCell38,
            this.xrTableCell39,
            this.xrTableCell40,
            this.xrTableCell62,
            this.xrTableCell63});
            this.xrTableRow7.Name = "xrTableRow7";
            this.xrTableRow7.Weight = 1D;
            // 
            // GroupF1
            // 
            this.GroupF1.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.GroupF1.Name = "GroupF1";
            this.GroupF1.Padding = new DevExpress.XtraPrinting.PaddingInfo(10, 0, 0, 0, 100F);
            this.GroupF1.StylePriority.UseBorders = false;
            this.GroupF1.StylePriority.UsePadding = false;
            this.GroupF1.StylePriority.UseTextAlignment = false;
            this.GroupF1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.GroupF1.Weight = 0.71530000507534208D;
            // 
            // GroupF2
            // 
            this.GroupF2.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.GroupF2.Name = "GroupF2";
            this.GroupF2.Padding = new DevExpress.XtraPrinting.PaddingInfo(10, 0, 0, 0, 100F);
            this.GroupF2.StylePriority.UseBorders = false;
            this.GroupF2.StylePriority.UsePadding = false;
            this.GroupF2.StylePriority.UseTextAlignment = false;
            this.GroupF2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.GroupF2.Weight = 0.71530000507534208D;
            this.GroupF2.HtmlItemCreated += new DevExpress.XtraReports.UI.HtmlEventHandler(this.GroupF2_HtmlItemCreated);
            // 
            // GroupF3
            // 
            this.GroupF3.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.GroupF3.Name = "GroupF3";
            this.GroupF3.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.GroupF3.StylePriority.UseBorders = false;
            this.GroupF3.StylePriority.UsePadding = false;
            this.GroupF3.StylePriority.UseTextAlignment = false;
            this.GroupF3.Text = "SUB TOTAL";
            this.GroupF3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.GroupF3.Weight = 1.1410737211881992D;
            this.GroupF3.HtmlItemCreated += new DevExpress.XtraReports.UI.HtmlEventHandler(this.GroupF3_HtmlItemCreated_1);
            // 
            // GroupF4
            // 
            this.GroupF4.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.GroupF4.Name = "GroupF4";
            this.GroupF4.Padding = new DevExpress.XtraPrinting.PaddingInfo(10, 0, 0, 0, 100F);
            this.GroupF4.StylePriority.UseBorders = false;
            this.GroupF4.StylePriority.UseFont = false;
            this.GroupF4.StylePriority.UsePadding = false;
            this.GroupF4.StylePriority.UseTextAlignment = false;
            this.GroupF4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.GroupF4.Weight = 0.28952633543428097D;
            // 
            // GroupF5
            // 
            this.GroupF5.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.GroupF5.Multiline = true;
            this.GroupF5.Name = "GroupF5";
            this.GroupF5.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 100F);
            this.GroupF5.StylePriority.UseBorders = false;
            this.GroupF5.StylePriority.UseFont = false;
            this.GroupF5.StylePriority.UsePadding = false;
            this.GroupF5.StylePriority.UseTextAlignment = false;
            this.GroupF5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.GroupF5.Weight = 0.715300007015786D;
            // 
            // xrTableCell21
            // 
            this.xrTableCell21.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell21.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_APAgingDetail.Day1to30")});
            this.xrTableCell21.Name = "xrTableCell21";
            this.xrTableCell21.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 100F);
            this.xrTableCell21.StylePriority.UseBorders = false;
            this.xrTableCell21.StylePriority.UsePadding = false;
            this.xrTableCell21.StylePriority.UseTextAlignment = false;
            xrSummary3.FormatString = "{0:#,0.00;(#,0.00);}";
            xrSummary3.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            this.xrTableCell21.Summary = xrSummary3;
            this.xrTableCell21.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell21.Weight = 0.71530002685633831D;
            // 
            // xrTableCell36
            // 
            this.xrTableCell36.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell36.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_APAgingDetail.Day31to60")});
            this.xrTableCell36.Name = "xrTableCell36";
            this.xrTableCell36.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 100F);
            this.xrTableCell36.StylePriority.UseBorders = false;
            this.xrTableCell36.StylePriority.UsePadding = false;
            this.xrTableCell36.StylePriority.UseTextAlignment = false;
            xrSummary4.FormatString = "{0:#,0.00;(#,0.00);}";
            xrSummary4.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            this.xrTableCell36.Summary = xrSummary4;
            this.xrTableCell36.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell36.Weight = 0.715300025993137D;
            // 
            // xrTableCell37
            // 
            this.xrTableCell37.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell37.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_APAgingDetail.Day61to90")});
            this.xrTableCell37.Name = "xrTableCell37";
            this.xrTableCell37.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 100F);
            this.xrTableCell37.StylePriority.UseBorders = false;
            this.xrTableCell37.StylePriority.UsePadding = false;
            this.xrTableCell37.StylePriority.UseTextAlignment = false;
            xrSummary5.FormatString = "{0:#,0.00;(#,0.00);}";
            xrSummary5.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            this.xrTableCell37.Summary = xrSummary5;
            this.xrTableCell37.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell37.Weight = 0.71530002968899553D;
            // 
            // xrTableCell38
            // 
            this.xrTableCell38.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell38.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_APAgingDetail.Day91to120")});
            this.xrTableCell38.Name = "xrTableCell38";
            this.xrTableCell38.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 100F);
            this.xrTableCell38.StylePriority.UseBorders = false;
            this.xrTableCell38.StylePriority.UsePadding = false;
            this.xrTableCell38.StylePriority.UseTextAlignment = false;
            xrSummary6.FormatString = "{0:#,0.00;(#,0.00);}";
            xrSummary6.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            this.xrTableCell38.Summary = xrSummary6;
            this.xrTableCell38.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell38.Weight = 0.71530003445864931D;
            // 
            // xrTableCell39
            // 
            this.xrTableCell39.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell39.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_APAgingDetail.Day121to150")});
            this.xrTableCell39.Name = "xrTableCell39";
            this.xrTableCell39.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 100F);
            this.xrTableCell39.StylePriority.UseBorders = false;
            this.xrTableCell39.StylePriority.UsePadding = false;
            this.xrTableCell39.StylePriority.UseTextAlignment = false;
            xrSummary7.FormatString = "{0:#,0.00;(#,0.00);}";
            xrSummary7.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            this.xrTableCell39.Summary = xrSummary7;
            this.xrTableCell39.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell39.Weight = 0.71530003074024728D;
            // 
            // xrTableCell40
            // 
            this.xrTableCell40.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell40.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_APAgingDetail.Day151to180")});
            this.xrTableCell40.Name = "xrTableCell40";
            this.xrTableCell40.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 100F);
            this.xrTableCell40.StylePriority.UseBorders = false;
            this.xrTableCell40.StylePriority.UsePadding = false;
            this.xrTableCell40.StylePriority.UseTextAlignment = false;
            xrSummary8.FormatString = "{0:#,0.00;(#,0.00);}";
            xrSummary8.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            this.xrTableCell40.Summary = xrSummary8;
            this.xrTableCell40.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell40.Weight = 0.7153000302987198D;
            // 
            // xrTableCell62
            // 
            this.xrTableCell62.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell62.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_APAgingDetail.Day180More")});
            this.xrTableCell62.Name = "xrTableCell62";
            this.xrTableCell62.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 100F);
            this.xrTableCell62.StylePriority.UseBorders = false;
            this.xrTableCell62.StylePriority.UsePadding = false;
            this.xrTableCell62.StylePriority.UseTextAlignment = false;
            xrSummary9.FormatString = "{0:#,0.00;(#,0.00);}";
            xrSummary9.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            this.xrTableCell62.Summary = xrSummary9;
            this.xrTableCell62.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell62.Weight = 0.71530003128178432D;
            // 
            // xrTableCell63
            // 
            this.xrTableCell63.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell63.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_APAgingDetail.APTotal")});
            this.xrTableCell63.Name = "xrTableCell63";
            this.xrTableCell63.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 100F);
            this.xrTableCell63.StylePriority.UseBorders = false;
            this.xrTableCell63.StylePriority.UsePadding = false;
            this.xrTableCell63.StylePriority.UseTextAlignment = false;
            xrSummary10.FormatString = "{0:#,0.00;(#,0.00);}";
            xrSummary10.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            this.xrTableCell63.Summary = xrSummary10;
            this.xrTableCell63.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell63.Weight = 0.71529995526655121D;
            // 
            // CutOff
            // 
            this.CutOff.Description = "CutOff";
            this.CutOff.Name = "CutOff";
            this.CutOff.Type = typeof(System.DateTime);
            // 
            // ReportFooter
            // 
            this.ReportFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.Table6,
            this.Table5});
            this.ReportFooter.HeightF = 76.04166F;
            this.ReportFooter.Name = "ReportFooter";
            // 
            // Table6
            // 
            this.Table6.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.Table6.Font = new System.Drawing.Font("Arial Narrow", 8F, System.Drawing.FontStyle.Bold);
            this.Table6.LocationFloat = new DevExpress.Utils.PointFloat(73.54999F, 0F);
            this.Table6.Name = "Table6";
            this.Table6.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow12});
            this.Table6.Scripts.OnBeforePrint = "xrTableRow2_BeforePrint";
            this.Table6.SizeF = new System.Drawing.SizeF(910F, 25.00002F);
            this.Table6.StylePriority.UseBorders = false;
            this.Table6.StylePriority.UseFont = false;
            this.Table6.StylePriority.UseTextAlignment = false;
            this.Table6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrTableRow12
            // 
            this.xrTableRow12.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.ReportFF1,
            this.ReportFF2,
            this.ReportFF3,
            this.ReportFF4,
            this.ReportFF5,
            this.xrTableCell19,
            this.xrTableCell20,
            this.xrTableCell23,
            this.xrTableCell26,
            this.xrTableCell33,
            this.xrTableCell35,
            this.xrTableCell41,
            this.xrTableCell42});
            this.xrTableRow12.Name = "xrTableRow12";
            this.xrTableRow12.Weight = 1D;
            // 
            // ReportFF1
            // 
            this.ReportFF1.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.ReportFF1.Name = "ReportFF1";
            this.ReportFF1.Padding = new DevExpress.XtraPrinting.PaddingInfo(10, 0, 0, 0, 100F);
            this.ReportFF1.StylePriority.UseBorders = false;
            this.ReportFF1.StylePriority.UsePadding = false;
            this.ReportFF1.StylePriority.UseTextAlignment = false;
            this.ReportFF1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.ReportFF1.Weight = 0.715300014721331D;
            // 
            // ReportFF2
            // 
            this.ReportFF2.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.ReportFF2.Name = "ReportFF2";
            this.ReportFF2.Padding = new DevExpress.XtraPrinting.PaddingInfo(10, 0, 0, 0, 100F);
            this.ReportFF2.StylePriority.UseBorders = false;
            this.ReportFF2.StylePriority.UsePadding = false;
            this.ReportFF2.StylePriority.UseTextAlignment = false;
            this.ReportFF2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.ReportFF2.Weight = 0.71530001472133109D;
            this.ReportFF2.HtmlItemCreated += new DevExpress.XtraReports.UI.HtmlEventHandler(this.ReportFF2_HtmlItemCreated_1);
            // 
            // ReportFF3
            // 
            this.ReportFF3.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.ReportFF3.Name = "ReportFF3";
            this.ReportFF3.Padding = new DevExpress.XtraPrinting.PaddingInfo(10, 0, 0, 0, 100F);
            this.ReportFF3.StylePriority.UseBorders = false;
            this.ReportFF3.StylePriority.UsePadding = false;
            this.ReportFF3.StylePriority.UseTextAlignment = false;
            this.ReportFF3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.ReportFF3.Weight = 0.715300015192441D;
            this.ReportFF3.HtmlItemCreated += new DevExpress.XtraReports.UI.HtmlEventHandler(this.ReportFF3_HtmlItemCreated_1);
            // 
            // ReportFF4
            // 
            this.ReportFF4.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.ReportFF4.Name = "ReportFF4";
            this.ReportFF4.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 100F);
            this.ReportFF4.StylePriority.UseBorders = false;
            this.ReportFF4.StylePriority.UseFont = false;
            this.ReportFF4.StylePriority.UsePadding = false;
            this.ReportFF4.StylePriority.UseTextAlignment = false;
            this.ReportFF4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.ReportFF4.Weight = 0.71530002935082138D;
            // 
            // ReportFF5
            // 
            this.ReportFF5.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.ReportFF5.Multiline = true;
            this.ReportFF5.Name = "ReportFF5";
            this.ReportFF5.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 100F);
            this.ReportFF5.StylePriority.UseBorders = false;
            this.ReportFF5.StylePriority.UseFont = false;
            this.ReportFF5.StylePriority.UsePadding = false;
            this.ReportFF5.StylePriority.UseTextAlignment = false;
            this.ReportFF5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.ReportFF5.Weight = 0.71530000455577436D;
            // 
            // xrTableCell19
            // 
            this.xrTableCell19.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrTableCell19.Name = "xrTableCell19";
            this.xrTableCell19.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 100F);
            this.xrTableCell19.StylePriority.UseBorders = false;
            this.xrTableCell19.StylePriority.UsePadding = false;
            this.xrTableCell19.StylePriority.UseTextAlignment = false;
            xrSummary11.FormatString = "{0:n}";
            this.xrTableCell19.Summary = xrSummary11;
            this.xrTableCell19.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell19.Weight = 0.7153000302442436D;
            // 
            // xrTableCell20
            // 
            this.xrTableCell20.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrTableCell20.Name = "xrTableCell20";
            this.xrTableCell20.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 100F);
            this.xrTableCell20.StylePriority.UseBorders = false;
            this.xrTableCell20.StylePriority.UsePadding = false;
            this.xrTableCell20.StylePriority.UseTextAlignment = false;
            xrSummary12.FormatString = "{0:n}";
            this.xrTableCell20.Summary = xrSummary12;
            this.xrTableCell20.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell20.Weight = 0.71530002896377887D;
            // 
            // xrTableCell23
            // 
            this.xrTableCell23.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrTableCell23.Name = "xrTableCell23";
            this.xrTableCell23.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 100F);
            this.xrTableCell23.StylePriority.UseBorders = false;
            this.xrTableCell23.StylePriority.UsePadding = false;
            this.xrTableCell23.StylePriority.UseTextAlignment = false;
            xrSummary13.FormatString = "{0:n}";
            this.xrTableCell23.Summary = xrSummary13;
            this.xrTableCell23.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell23.Weight = 0.71529995245026223D;
            // 
            // xrTableCell26
            // 
            this.xrTableCell26.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrTableCell26.Name = "xrTableCell26";
            this.xrTableCell26.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 100F);
            this.xrTableCell26.StylePriority.UseBorders = false;
            this.xrTableCell26.StylePriority.UsePadding = false;
            this.xrTableCell26.StylePriority.UseTextAlignment = false;
            xrSummary14.FormatString = "{0:n}";
            this.xrTableCell26.Summary = xrSummary14;
            this.xrTableCell26.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell26.Weight = 0.71529995710114014D;
            // 
            // xrTableCell33
            // 
            this.xrTableCell33.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrTableCell33.Name = "xrTableCell33";
            this.xrTableCell33.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 100F);
            this.xrTableCell33.StylePriority.UseBorders = false;
            this.xrTableCell33.StylePriority.UsePadding = false;
            this.xrTableCell33.StylePriority.UseTextAlignment = false;
            xrSummary15.FormatString = "{0:n}";
            this.xrTableCell33.Summary = xrSummary15;
            this.xrTableCell33.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell33.Weight = 0.71529995290832793D;
            // 
            // xrTableCell35
            // 
            this.xrTableCell35.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrTableCell35.Name = "xrTableCell35";
            this.xrTableCell35.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 100F);
            this.xrTableCell35.StylePriority.UseBorders = false;
            this.xrTableCell35.StylePriority.UsePadding = false;
            this.xrTableCell35.StylePriority.UseTextAlignment = false;
            xrSummary16.FormatString = "{0:n}";
            this.xrTableCell35.Summary = xrSummary16;
            this.xrTableCell35.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell35.Weight = 0.71529995241716571D;
            // 
            // xrTableCell41
            // 
            this.xrTableCell41.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrTableCell41.Name = "xrTableCell41";
            this.xrTableCell41.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 100F);
            this.xrTableCell41.StylePriority.UseBorders = false;
            this.xrTableCell41.StylePriority.UsePadding = false;
            this.xrTableCell41.StylePriority.UseTextAlignment = false;
            xrSummary17.FormatString = "{0:n}";
            this.xrTableCell41.Summary = xrSummary17;
            this.xrTableCell41.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell41.Weight = 0.71529995390830958D;
            // 
            // xrTableCell42
            // 
            this.xrTableCell42.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell42.Name = "xrTableCell42";
            this.xrTableCell42.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 100F);
            this.xrTableCell42.StylePriority.UseBorders = false;
            this.xrTableCell42.StylePriority.UsePadding = false;
            this.xrTableCell42.StylePriority.UseTextAlignment = false;
            xrSummary18.FormatString = "{0:n}";
            this.xrTableCell42.Summary = xrSummary18;
            this.xrTableCell42.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell42.Weight = 0.71530042313263342D;
            // 
            // Table5
            // 
            this.Table5.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.Table5.Font = new System.Drawing.Font("Arial Narrow", 8F, System.Drawing.FontStyle.Bold);
            this.Table5.LocationFloat = new DevExpress.Utils.PointFloat(73.55002F, 25.00002F);
            this.Table5.Name = "Table5";
            this.Table5.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow13});
            this.Table5.Scripts.OnBeforePrint = "xrTableRow2_BeforePrint";
            this.Table5.SizeF = new System.Drawing.SizeF(910F, 25.00002F);
            this.Table5.StylePriority.UseBorders = false;
            this.Table5.StylePriority.UseFont = false;
            this.Table5.StylePriority.UseTextAlignment = false;
            this.Table5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrTableRow13
            // 
            this.xrTableRow13.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.ReportF1,
            this.ReportF2,
            this.ReportF3,
            this.ReportF4,
            this.ReportF5,
            this.xrTableCell47,
            this.xrTableCell48,
            this.xrTableCell50,
            this.xrTableCell51,
            this.xrTableCell52,
            this.xrTableCell53,
            this.xrTableCell60,
            this.xrTableCell61});
            this.xrTableRow13.Name = "xrTableRow13";
            this.xrTableRow13.Weight = 1D;
            // 
            // ReportF1
            // 
            this.ReportF1.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.ReportF1.Name = "ReportF1";
            this.ReportF1.Padding = new DevExpress.XtraPrinting.PaddingInfo(10, 0, 0, 0, 100F);
            this.ReportF1.StylePriority.UseBorders = false;
            this.ReportF1.StylePriority.UsePadding = false;
            this.ReportF1.StylePriority.UseTextAlignment = false;
            this.ReportF1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.ReportF1.Weight = 0.71530001697226908D;
            // 
            // ReportF2
            // 
            this.ReportF2.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.ReportF2.Name = "ReportF2";
            this.ReportF2.Padding = new DevExpress.XtraPrinting.PaddingInfo(10, 0, 0, 0, 100F);
            this.ReportF2.StylePriority.UseBorders = false;
            this.ReportF2.StylePriority.UsePadding = false;
            this.ReportF2.StylePriority.UseTextAlignment = false;
            this.ReportF2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.ReportF2.Weight = 0.71530001697226908D;
            this.ReportF2.HtmlItemCreated += new DevExpress.XtraReports.UI.HtmlEventHandler(this.ReportF2_HtmlItemCreated_1);
            // 
            // ReportF3
            // 
            this.ReportF3.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.ReportF3.Name = "ReportF3";
            this.ReportF3.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.ReportF3.StylePriority.UseBorders = false;
            this.ReportF3.StylePriority.UseFont = false;
            this.ReportF3.StylePriority.UsePadding = false;
            this.ReportF3.StylePriority.UseTextAlignment = false;
            this.ReportF3.Text = "GRAND TOTAL";
            this.ReportF3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.ReportF3.Weight = 1.141073763354991D;
            this.ReportF3.HtmlItemCreated += new DevExpress.XtraReports.UI.HtmlEventHandler(this.ReportF3_HtmlItemCreated_1);
            // 
            // ReportF4
            // 
            this.ReportF4.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.ReportF4.Multiline = true;
            this.ReportF4.Name = "ReportF4";
            this.ReportF4.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 100F);
            this.ReportF4.StylePriority.UseBorders = false;
            this.ReportF4.StylePriority.UseFont = false;
            this.ReportF4.StylePriority.UsePadding = false;
            this.ReportF4.StylePriority.UseTextAlignment = false;
            this.ReportF4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.ReportF4.Weight = 0.28952643093064D;
            // 
            // ReportF5
            // 
            this.ReportF5.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.ReportF5.Name = "ReportF5";
            this.ReportF5.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 100F);
            this.ReportF5.StylePriority.UseBorders = false;
            this.ReportF5.StylePriority.UsePadding = false;
            this.ReportF5.StylePriority.UseTextAlignment = false;
            this.ReportF5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.ReportF5.Weight = 0.71530008483275376D;
            // 
            // xrTableCell47
            // 
            this.xrTableCell47.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrTableCell47.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_APAgingDetail.Day1to30")});
            this.xrTableCell47.Name = "xrTableCell47";
            this.xrTableCell47.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 100F);
            this.xrTableCell47.StylePriority.UseBorders = false;
            this.xrTableCell47.StylePriority.UsePadding = false;
            this.xrTableCell47.StylePriority.UseTextAlignment = false;
            xrSummary19.FormatString = "{0:#,0.00;(#,0.00);}";
            xrSummary19.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
            this.xrTableCell47.Summary = xrSummary19;
            this.xrTableCell47.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell47.Weight = 0.71530003137716758D;
            // 
            // xrTableCell48
            // 
            this.xrTableCell48.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrTableCell48.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_APAgingDetail.Day31to60")});
            this.xrTableCell48.Name = "xrTableCell48";
            this.xrTableCell48.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 100F);
            this.xrTableCell48.StylePriority.UseBorders = false;
            this.xrTableCell48.StylePriority.UsePadding = false;
            this.xrTableCell48.StylePriority.UseTextAlignment = false;
            xrSummary20.FormatString = "{0:#,0.00;(#,0.00);}";
            xrSummary20.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
            this.xrTableCell48.Summary = xrSummary20;
            this.xrTableCell48.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell48.Weight = 0.71530010862184412D;
            // 
            // xrTableCell50
            // 
            this.xrTableCell50.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrTableCell50.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_APAgingDetail.Day61to90")});
            this.xrTableCell50.Name = "xrTableCell50";
            this.xrTableCell50.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 100F);
            this.xrTableCell50.StylePriority.UseBorders = false;
            this.xrTableCell50.StylePriority.UsePadding = false;
            this.xrTableCell50.StylePriority.UseTextAlignment = false;
            xrSummary21.FormatString = "{0:#,0.00;(#,0.00);}";
            xrSummary21.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
            this.xrTableCell50.Summary = xrSummary21;
            this.xrTableCell50.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell50.Weight = 0.71530011052059816D;
            // 
            // xrTableCell51
            // 
            this.xrTableCell51.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrTableCell51.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_APAgingDetail.Day91to120")});
            this.xrTableCell51.Name = "xrTableCell51";
            this.xrTableCell51.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 100F);
            this.xrTableCell51.StylePriority.UseBorders = false;
            this.xrTableCell51.StylePriority.UsePadding = false;
            this.xrTableCell51.StylePriority.UseTextAlignment = false;
            xrSummary22.FormatString = "{0:#,0.00;(#,0.00);}";
            xrSummary22.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
            this.xrTableCell51.Summary = xrSummary22;
            this.xrTableCell51.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell51.Weight = 0.71530003685737409D;
            // 
            // xrTableCell52
            // 
            this.xrTableCell52.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrTableCell52.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_APAgingDetail.Day121to150")});
            this.xrTableCell52.Name = "xrTableCell52";
            this.xrTableCell52.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 100F);
            this.xrTableCell52.StylePriority.UseBorders = false;
            this.xrTableCell52.StylePriority.UsePadding = false;
            this.xrTableCell52.StylePriority.UseTextAlignment = false;
            xrSummary23.FormatString = "{0:#,0.00;(#,0.00);}";
            xrSummary23.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
            this.xrTableCell52.Summary = xrSummary23;
            this.xrTableCell52.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell52.Weight = 0.71530010921638343D;
            // 
            // xrTableCell53
            // 
            this.xrTableCell53.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrTableCell53.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_APAgingDetail.Day151to180")});
            this.xrTableCell53.Name = "xrTableCell53";
            this.xrTableCell53.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 100F);
            this.xrTableCell53.StylePriority.UseBorders = false;
            this.xrTableCell53.StylePriority.UsePadding = false;
            this.xrTableCell53.StylePriority.UseTextAlignment = false;
            xrSummary24.FormatString = "{0:#,0.00;(#,0.00);}";
            xrSummary24.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
            this.xrTableCell53.Summary = xrSummary24;
            this.xrTableCell53.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell53.Weight = 0.715300108577859D;
            // 
            // xrTableCell60
            // 
            this.xrTableCell60.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrTableCell60.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_APAgingDetail.Day180More")});
            this.xrTableCell60.Name = "xrTableCell60";
            this.xrTableCell60.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 100F);
            this.xrTableCell60.StylePriority.UseBorders = false;
            this.xrTableCell60.StylePriority.UsePadding = false;
            this.xrTableCell60.StylePriority.UseTextAlignment = false;
            xrSummary25.FormatString = "{0:#,0.00;(#,0.00);}";
            xrSummary25.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
            this.xrTableCell60.Summary = xrSummary25;
            this.xrTableCell60.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell60.Weight = 0.71530011401506843D;
            // 
            // xrTableCell61
            // 
            this.xrTableCell61.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell61.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_APAgingDetail.APTotal")});
            this.xrTableCell61.Name = "xrTableCell61";
            this.xrTableCell61.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 100F);
            this.xrTableCell61.StylePriority.UseBorders = false;
            this.xrTableCell61.StylePriority.UsePadding = false;
            this.xrTableCell61.StylePriority.UseTextAlignment = false;
            xrSummary26.FormatString = "{0:#,0.00;(#,0.00);}";
            xrSummary26.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
            this.xrTableCell61.Summary = xrSummary26;
            this.xrTableCell61.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell61.Weight = 0.71529964189189343D;
            // 
            // GroupField
            // 
            this.GroupField.DataMember = "sp_report_ARDueDetail";
            this.GroupField.Expression = "Iif([Parameters.SummaryBy] == [BusinessAccount], [BusinessAccountCode], [BizPartn" +
    "erCode])";
            this.GroupField.Name = "GroupField";
            // 
            // BusinessSubClass
            // 
            this.BusinessSubClass.Description = "BusinessSubClass";
            dynamicListLookUpSettings1.DataAdapter = null;
            dynamicListLookUpSettings1.DataMember = "BusinessSubClass";
            dynamicListLookUpSettings1.DataSource = this.sqlDataSource1;
            dynamicListLookUpSettings1.DisplayMember = "Name";
            dynamicListLookUpSettings1.ValueMember = "BizSubtypeCode";
            this.BusinessSubClass.LookUpSettings = dynamicListLookUpSettings1;
            this.BusinessSubClass.MultiValue = true;
            this.BusinessSubClass.Name = "BusinessSubClass";
            this.BusinessSubClass.ValueInfo = "ALL";
            // 
            // Supplier
            // 
            this.Supplier.Description = "Supplier";
            dynamicListLookUpSettings2.DataAdapter = null;
            dynamicListLookUpSettings2.DataMember = "Supplier";
            dynamicListLookUpSettings2.DataSource = this.sqlDataSource1;
            dynamicListLookUpSettings2.DisplayMember = "Name";
            dynamicListLookUpSettings2.ValueMember = "SupplierCode";
            this.Supplier.LookUpSettings = dynamicListLookUpSettings2;
            this.Supplier.MultiValue = true;
            this.Supplier.Name = "Supplier";
            this.Supplier.ValueInfo = "ALL";
            // 
            // GLAPAccount
            // 
            this.GLAPAccount.Description = "GLAPAccount";
            dynamicListLookUpSettings3.DataAdapter = null;
            dynamicListLookUpSettings3.DataMember = "GLAPAccount";
            dynamicListLookUpSettings3.DataSource = this.sqlDataSource1;
            dynamicListLookUpSettings3.DisplayMember = "Description";
            dynamicListLookUpSettings3.ValueMember = "AccountCode";
            this.GLAPAccount.LookUpSettings = dynamicListLookUpSettings3;
            this.GLAPAccount.MultiValue = true;
            this.GLAPAccount.Name = "GLAPAccount";
            // 
            // Table2
            // 
            this.Table2.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.Table2.Font = new System.Drawing.Font("Arial Narrow", 8F, System.Drawing.FontStyle.Bold);
            this.Table2.LocationFloat = new DevExpress.Utils.PointFloat(73.55F, 0F);
            this.Table2.Name = "Table2";
            this.Table2.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow10});
            this.Table2.Scripts.OnBeforePrint = "xrTableRow2_BeforePrint";
            this.Table2.SizeF = new System.Drawing.SizeF(910F, 25.00002F);
            this.Table2.StylePriority.UseBorders = false;
            this.Table2.StylePriority.UseFont = false;
            this.Table2.StylePriority.UseTextAlignment = false;
            this.Table2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrTableRow10
            // 
            this.xrTableRow10.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.GroupH1,
            this.GroupH2,
            this.GroupH3,
            this.GroupH4,
            this.GroupH5});
            this.xrTableRow10.Name = "xrTableRow10";
            this.xrTableRow10.Weight = 1D;
            // 
            // GroupH1
            // 
            this.GroupH1.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.GroupH1.Name = "GroupH1";
            this.GroupH1.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 100F);
            this.GroupH1.StylePriority.UseBorders = false;
            this.GroupH1.StylePriority.UsePadding = false;
            this.GroupH1.StylePriority.UseTextAlignment = false;
            this.GroupH1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.GroupH1.Weight = 0.71529999708725933D;
            // 
            // GroupH2
            // 
            this.GroupH2.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.GroupH2.Name = "GroupH2";
            this.GroupH2.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 100F);
            this.GroupH2.StylePriority.UseBorders = false;
            this.GroupH2.StylePriority.UsePadding = false;
            this.GroupH2.StylePriority.UseTextAlignment = false;
            this.GroupH2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.GroupH2.Weight = 6.4377011446055281D;
            // 
            // GroupH3
            // 
            this.GroupH3.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.GroupH3.Name = "GroupH3";
            this.GroupH3.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 100F);
            this.GroupH3.StylePriority.UseBorders = false;
            this.GroupH3.StylePriority.UsePadding = false;
            this.GroupH3.StylePriority.UseTextAlignment = false;
            this.GroupH3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.GroupH3.Weight = 0.715299340917471D;
            this.GroupH3.HtmlItemCreated += new DevExpress.XtraReports.UI.HtmlEventHandler(this.GroupH3_HtmlItemCreated_1);
            // 
            // GroupH4
            // 
            this.GroupH4.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.GroupH4.Name = "GroupH4";
            this.GroupH4.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 100F);
            this.GroupH4.Scripts.OnBeforePrint = "xrTableCell41_BeforePrint";
            this.GroupH4.StylePriority.UseBorders = false;
            this.GroupH4.StylePriority.UseFont = false;
            this.GroupH4.StylePriority.UsePadding = false;
            this.GroupH4.StylePriority.UseTextAlignment = false;
            this.GroupH4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.GroupH4.Weight = 0.71529871850091808D;
            this.GroupH4.HtmlItemCreated += new DevExpress.XtraReports.UI.HtmlEventHandler(this.GroupH4_HtmlItemCreated);
            // 
            // GroupH5
            // 
            this.GroupH5.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.GroupH5.Multiline = true;
            this.GroupH5.Name = "GroupH5";
            this.GroupH5.StylePriority.UseBorders = false;
            this.GroupH5.StylePriority.UseFont = false;
            this.GroupH5.StylePriority.UseTextAlignment = false;
            this.GroupH5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.GroupH5.Weight = 0.71530062816998541D;
            // 
            // GroupHeader1
            // 
            this.GroupHeader1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.Table2});
            this.GroupHeader1.GroupFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("SupplierCode", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});
            this.GroupHeader1.HeightF = 25.00002F;
            this.GroupHeader1.Name = "GroupHeader1";
            // 
            // ShowDueDate
            // 
            this.ShowDueDate.Description = "ShowDueDate";
            this.ShowDueDate.Name = "ShowDueDate";
            this.ShowDueDate.Type = typeof(bool);
            this.ShowDueDate.ValueInfo = "False";
            // 
            // ShowTerms
            // 
            this.ShowTerms.Description = "ShowTerms";
            this.ShowTerms.Name = "ShowTerms";
            this.ShowTerms.Type = typeof(bool);
            this.ShowTerms.ValueInfo = "False";
            // 
            // PageHeader
            // 
            this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.Table1});
            this.PageHeader.HeightF = 25.00002F;
            this.PageHeader.Name = "PageHeader";
            // 
            // R_APAgingDetail
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.Head,
            this.GroupFooter1,
            this.ReportFooter,
            this.GroupHeader1,
            this.PageHeader});
            this.CalculatedFields.AddRange(new DevExpress.XtraReports.UI.CalculatedField[] {
            this.GroupField});
            this.ComponentStorage.AddRange(new System.ComponentModel.IComponent[] {
            this.sqlDataSource1});
            this.DataMember = "sp_report_APAgingDetail";
            this.DataSource = this.sqlDataSource1;
            this.FormattingRuleSheet.AddRange(new DevExpress.XtraReports.UI.FormattingRule[] {
            this.formattingRule1});
            this.Landscape = true;
            this.Margins = new System.Drawing.Printing.Margins(20, 20, 40, 41);
            this.PageHeight = 850;
            this.PageWidth = 1100;
            this.Parameters.AddRange(new DevExpress.XtraReports.Parameters.Parameter[] {
            this.CutOff,
            this.BusinessSubClass,
            this.Supplier,
            this.GLAPAccount,
            this.ShowDueDate,
            this.ShowTerms});
            this.Scripts.OnAfterPrint = "R_APAgingDetail_AfterPrint";
            this.Scripts.OnParametersRequestBeforeShow = "R_ARDueDetail_ParametersRequestBeforeShow";
            this.Scripts.OnParametersRequestSubmit = "R_ARDueDetail_ParametersRequestSubmit";
            this.ScriptsSource = resources.GetString("$this.ScriptsSource");
            this.Version = "15.1";
            this.ParametersRequestBeforeShow += new System.EventHandler<DevExpress.XtraReports.Parameters.ParametersRequestEventArgs>(this.R_APAgingDetail_ParametersRequestBeforeShow);
            this.DataSourceDemanded += new System.EventHandler<System.EventArgs>(this.R_APAgingDetail_DataSourceDemanded);
            this.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.R_APAgingDetail_BeforePrint);
            ((System.ComponentModel.ISupportInitialize)(this.Table3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Table1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Table4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Table6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Table5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Table2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private DevExpress.DataAccess.Sql.SqlDataSource sqlDataSource1;
        private DevExpress.XtraReports.UI.XRLabel xrLabel18;
        //private DevExpress.XtraTabbedMdi.XtraTabbedMdiManager xtraTabbedMdiManager1;
        //private DevExpress.XtraBars.Ribbon.GalleryDropDown galleryDropDown1;
        //private DevExpress.XtraBars.Ribbon.GalleryDropDown galleryDropDown2;
        private DevExpress.XtraReports.UI.ReportHeaderBand Head;
        private DevExpress.XtraReports.UI.XRTable Table3;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow5;
        private DevExpress.XtraReports.UI.XRTableCell Detail1;
        private DevExpress.XtraReports.UI.XRTableCell Detail4;
        private DevExpress.XtraReports.UI.XRTableCell Detail5;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell28;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell29;
        private DevExpress.XtraReports.UI.XRLabel xrLabel15;
        private DevExpress.XtraReports.UI.XRTableCell Detail2;
        private DevExpress.XtraReports.UI.XRLabel xrLabel1;
        private DevExpress.XtraReports.UI.XRTableCell Detail3;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell27;
        private FormattingRule formattingRule1;
        private GroupFooterBand GroupFooter1;
        private XRTable Table4;
        private XRTableRow xrTableRow7;
        private XRTableCell GroupF3;
        private XRTableCell GroupF4;
        private XRTableCell GroupF5;
        private XRTableCell xrTableCell21;
        private Parameter CutOff;
        private XRTableCell xrTableCell22;
        private XRTableCell xrTableCell36;
        private XRTableCell xrTableCell37;
        private XRTableCell xrTableCell38;
        private XRTableCell xrTableCell39;
        private XRTableCell xrTableCell40;
        private ReportFooterBand ReportFooter;
        private XRTable Table5;
        private XRTableRow xrTableRow13;
        private XRTableCell ReportF2;
        private XRTableCell ReportF3;
        private XRTableCell ReportF4;
        private XRTableCell xrTableCell47;
        private XRTableCell xrTableCell48;
        private XRTableCell xrTableCell50;
        private XRTableCell xrTableCell51;
        private XRTableCell xrTableCell52;
        private XRTableCell xrTableCell53;
        private CalculatedField GroupField;
        private Parameter BusinessSubClass;
        private Parameter Supplier;
        private Parameter GLAPAccount;
        private XRTable Table1;
        private XRTableRow xrTableRow2;
        private XRTableCell Header1;
        private XRTableCell Header4;
        private XRTableCell xrTableCell1;
        private XRTableCell xrTableCell3;
        private XRTableCell xrTableCell6;
        private XRTableCell xrTableCell11;
        private XRTableCell xrTableCell12;
        private XRTableCell xrTableCell15;
        private XRTableCell xrTableCell2;
        private XRTableCell xrTableCell34;
        private XRTableCell xrTableCell56;
        private XRTableCell xrTableCell57;
        private XRTableCell xrTableCell58;
        private XRTableCell xrTableCell59;
        private XRTableCell Header2;
        private XRTableCell Header3;
        private XRTableCell Header5;
        private XRTableCell xrTableCell60;
        private XRTableCell xrTableCell61;
        private XRTableCell xrTableCell62;
        private XRTableCell xrTableCell63;
        private XRTable Table2;
        private XRTableRow xrTableRow10;
        private XRTableCell GroupH4;
        private XRTableCell GroupH5;
        private GroupHeaderBand GroupHeader1;
        private Parameter ShowDueDate;
        private Parameter ShowTerms;
        private XRTableCell GroupH1;
        private XRTableCell GroupH2;
        private XRTableCell GroupH3;
        private XRTableCell GroupF2;
        private XRTableCell GroupF1;
        private XRTable Table6;
        private XRTableRow xrTableRow12;
        private XRTableCell ReportFF1;
        private XRTableCell ReportFF2;
        private XRTableCell ReportFF3;
        private XRTableCell ReportFF4;
        private XRTableCell ReportFF5;
        private XRTableCell xrTableCell19;
        private XRTableCell xrTableCell20;
        private XRTableCell xrTableCell23;
        private XRTableCell xrTableCell26;
        private XRTableCell xrTableCell33;
        private XRTableCell xrTableCell35;
        private XRTableCell xrTableCell41;
        private XRTableCell xrTableCell42;
        private XRTableCell ReportF1;
        private XRTableCell ReportF5;
        private XRLabel xrLabel3;
        private XRPageInfo xrPageInfo2;
        private XRPageInfo xrPageInfo3;
        private XRLabel xrLabel4;
        private PageHeaderBand PageHeader;

    }
}
