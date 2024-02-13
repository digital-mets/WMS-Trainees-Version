using System;
using System.Windows.Forms;
using DevExpress.XtraReports.UI;
using DevExpress.XtraReports.Parameters;

namespace GWL.WebReports.GEARS_Reports
{
    partial class R_ReplenishPettyCashExpense

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(R_ReplenishPettyCashExpense));
            DevExpress.DataAccess.Sql.StoredProcQuery storedProcQuery1 = new DevExpress.DataAccess.Sql.StoredProcQuery();
            DevExpress.DataAccess.Sql.QueryParameter queryParameter1 = new DevExpress.DataAccess.Sql.QueryParameter();
            DevExpress.DataAccess.Sql.QueryParameter queryParameter2 = new DevExpress.DataAccess.Sql.QueryParameter();
            DevExpress.XtraReports.UI.XRSummary xrSummary1 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary2 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.Parameters.DynamicListLookUpSettings dynamicListLookUpSettings1 = new DevExpress.XtraReports.Parameters.DynamicListLookUpSettings();
            DevExpress.XtraReports.UI.XRSummary xrSummary3 = new DevExpress.XtraReports.UI.XRSummary();
            this.sqlDataSource1 = new DevExpress.DataAccess.Sql.SqlDataSource();
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.xrTable3 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow5 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell19 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell20 = new DevExpress.XtraReports.UI.XRTableCell();
            this.Detail1 = new DevExpress.XtraReports.UI.XRTableCell();
            this.Detail2 = new DevExpress.XtraReports.UI.XRTableCell();
            this.Detail3 = new DevExpress.XtraReports.UI.XRTableCell();
            this.Detail4 = new DevExpress.XtraReports.UI.XRTableCell();
            this.Detail5 = new DevExpress.XtraReports.UI.XRTableCell();
            this.Detail6 = new DevExpress.XtraReports.UI.XRTableCell();
            this.Detail7 = new DevExpress.XtraReports.UI.XRTableCell();
            this.Detail8 = new DevExpress.XtraReports.UI.XRTableCell();
            this.DetailB7 = new DevExpress.XtraReports.UI.XRLabel();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.xrPageInfo3 = new DevExpress.XtraReports.UI.XRPageInfo();
            this.xrLabel3 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrPageInfo2 = new DevExpress.XtraReports.UI.XRPageInfo();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.xrLabel18 = new DevExpress.XtraReports.UI.XRLabel();
            this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
            this.xrLabel4 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel15 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrTable2 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell34 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell33 = new DevExpress.XtraReports.UI.XRTableCell();
            this.Header1 = new DevExpress.XtraReports.UI.XRTableCell();
            this.Header2 = new DevExpress.XtraReports.UI.XRTableCell();
            this.Header3 = new DevExpress.XtraReports.UI.XRTableCell();
            this.Header4 = new DevExpress.XtraReports.UI.XRTableCell();
            this.Header5 = new DevExpress.XtraReports.UI.XRTableCell();
            this.Header6 = new DevExpress.XtraReports.UI.XRTableCell();
            this.Header7 = new DevExpress.XtraReports.UI.XRTableCell();
            this.Header8 = new DevExpress.XtraReports.UI.XRTableCell();
            this.formattingRule1 = new DevExpress.XtraReports.UI.FormattingRule();
            this.DateFrom = new DevExpress.XtraReports.Parameters.Parameter();
            this.DateTo = new DevExpress.XtraReports.Parameters.Parameter();
            this.GroupHeader1 = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell24 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell35 = new DevExpress.XtraReports.UI.XRTableCell();
            this.GroupB7 = new DevExpress.XtraReports.UI.XRLabel();
            this.ShowDetails = new DevExpress.XtraReports.Parameters.Parameter();
            this.GroupFooter1 = new DevExpress.XtraReports.UI.GroupFooterBand();
            this.xrTable4 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow3 = new DevExpress.XtraReports.UI.XRTableRow();
            this.Labelxxx = new DevExpress.XtraReports.UI.XRTableCell();
            this.Labelyyy = new DevExpress.XtraReports.UI.XRTableCell();
            this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
            this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
            this.xrLabel8 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel9 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel6 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel7 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel5 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable4)).BeginInit();
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
            customSqlQuery3.Name = "ShowDetails";
            customSqlQuery3.Sql = resources.GetString("customSqlQuery3.Sql");
            storedProcQuery1.Name = "sp_report_ReplenishedPettyCashExpense";
            queryParameter1.Name = "@DateFrom";
            queryParameter1.Type = typeof(DevExpress.DataAccess.Expression);
            queryParameter1.Value = new DevExpress.DataAccess.Expression("[Parameters.DateFrom]", typeof(System.DateTime));
            queryParameter2.Name = "@DateTo";
            queryParameter2.Type = typeof(DevExpress.DataAccess.Expression);
            queryParameter2.Value = new DevExpress.DataAccess.Expression("[Parameters.DateTo]", typeof(System.DateTime));
            storedProcQuery1.Parameters.Add(queryParameter1);
            storedProcQuery1.Parameters.Add(queryParameter2);
            storedProcQuery1.StoredProcName = "sp_report_ReplenishedPettyCashExpense";
            this.sqlDataSource1.Queries.AddRange(new DevExpress.DataAccess.Sql.SqlQuery[] {
            customSqlQuery1,
            customSqlQuery2,
            customSqlQuery3,
            storedProcQuery1});
            this.sqlDataSource1.ResultSchemaSerializable = resources.GetString("sqlDataSource1.ResultSchemaSerializable");
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable3,
            this.DetailB7});
            this.Detail.HeightF = 25.00004F;
            this.Detail.MultiColumn.Layout = DevExpress.XtraPrinting.ColumnLayout.AcrossThenDown;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrTable3
            // 
            this.xrTable3.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable3.Font = new System.Drawing.Font("Arial Narrow", 8F);
            this.xrTable3.LocationFloat = new DevExpress.Utils.PointFloat(20.14086F, 0F);
            this.xrTable3.Name = "xrTable3";
            this.xrTable3.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow5});
            this.xrTable3.SizeF = new System.Drawing.SizeF(778.4009F, 25F);
            this.xrTable3.StylePriority.UseBorders = false;
            this.xrTable3.StylePriority.UseFont = false;
            this.xrTable3.StylePriority.UseTextAlignment = false;
            this.xrTable3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrTableRow5
            // 
            this.xrTableRow5.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell19,
            this.xrTableCell20,
            this.Detail1,
            this.Detail2,
            this.Detail3,
            this.Detail4,
            this.Detail5,
            this.Detail6,
            this.Detail7,
            this.Detail8});
            this.xrTableRow5.Name = "xrTableRow5";
            this.xrTableRow5.Weight = 1D;
            // 
            // xrTableCell19
            // 
            this.xrTableCell19.BorderColor = System.Drawing.Color.Black;
            this.xrTableCell19.Borders = DevExpress.XtraPrinting.BorderSide.Left;
            this.xrTableCell19.BorderWidth = 1F;
            this.xrTableCell19.Name = "xrTableCell19";
            this.xrTableCell19.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 0, 0, 0, 100F);
            this.xrTableCell19.Scripts.OnBeforePrint = "xrTableCell19_BeforePrint";
            this.xrTableCell19.StylePriority.UseBorderColor = false;
            this.xrTableCell19.StylePriority.UseBorders = false;
            this.xrTableCell19.StylePriority.UseBorderWidth = false;
            this.xrTableCell19.StylePriority.UsePadding = false;
            this.xrTableCell19.StylePriority.UseTextAlignment = false;
            this.xrTableCell19.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell19.Weight = 0.64436527889071527D;
            // 
            // xrTableCell20
            // 
            this.xrTableCell20.BorderColor = System.Drawing.Color.Black;
            this.xrTableCell20.Borders = DevExpress.XtraPrinting.BorderSide.Right;
            this.xrTableCell20.BorderWidth = 1F;
            this.xrTableCell20.Name = "xrTableCell20";
            this.xrTableCell20.StylePriority.UseBorderColor = false;
            this.xrTableCell20.StylePriority.UseBorders = false;
            this.xrTableCell20.StylePriority.UseBorderWidth = false;
            this.xrTableCell20.StylePriority.UseTextAlignment = false;
            this.xrTableCell20.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell20.Weight = 0.75268759028956012D;
            // 
            // Detail1
            // 
            this.Detail1.BorderColor = System.Drawing.Color.Black;
            this.Detail1.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.Detail1.BorderWidth = 1F;
            this.Detail1.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ReplenishedPettyCashExpense.DocNumber", "{0:MM/dd/yy}")});
            this.Detail1.Name = "Detail1";
            this.Detail1.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.Detail1.Scripts.OnBeforePrint = "xrTableCell2_BeforePrint";
            this.Detail1.StylePriority.UseBorderColor = false;
            this.Detail1.StylePriority.UseBorders = false;
            this.Detail1.StylePriority.UseBorderWidth = false;
            this.Detail1.StylePriority.UsePadding = false;
            this.Detail1.StylePriority.UseTextAlignment = false;
            this.Detail1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.Detail1.Weight = 0.80754120545114771D;
            // 
            // Detail2
            // 
            this.Detail2.BorderColor = System.Drawing.Color.Black;
            this.Detail2.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.Detail2.BorderWidth = 1F;
            this.Detail2.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ReplenishedPettyCashExpense.DocDate")});
            this.Detail2.Name = "Detail2";
            this.Detail2.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.Detail2.Scripts.OnBeforePrint = "xrTableCell7_BeforePrint";
            this.Detail2.StylePriority.UseBorderColor = false;
            this.Detail2.StylePriority.UseBorders = false;
            this.Detail2.StylePriority.UseBorderWidth = false;
            this.Detail2.StylePriority.UsePadding = false;
            this.Detail2.StylePriority.UseTextAlignment = false;
            this.Detail2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.Detail2.Weight = 0.8075410478129037D;
            // 
            // Detail3
            // 
            this.Detail3.BorderColor = System.Drawing.Color.Black;
            this.Detail3.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.Detail3.BorderWidth = 1F;
            this.Detail3.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ReplenishedPettyCashExpense.FundSource")});
            this.Detail3.Name = "Detail3";
            this.Detail3.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.Detail3.Scripts.OnBeforePrint = "xrTableCell23_BeforePrint";
            this.Detail3.StylePriority.UseBorderColor = false;
            this.Detail3.StylePriority.UseBorders = false;
            this.Detail3.StylePriority.UseBorderWidth = false;
            this.Detail3.StylePriority.UsePadding = false;
            this.Detail3.StylePriority.UseTextAlignment = false;
            this.Detail3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.Detail3.Weight = 0.80754102597579225D;
            // 
            // Detail4
            // 
            this.Detail4.BorderColor = System.Drawing.Color.Black;
            this.Detail4.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.Detail4.BorderWidth = 1F;
            this.Detail4.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ReplenishedPettyCashExpense.Requestor")});
            this.Detail4.Name = "Detail4";
            this.Detail4.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.Detail4.Scripts.OnBeforePrint = "xrTableCell26_BeforePrint";
            this.Detail4.StylePriority.UseBorderColor = false;
            this.Detail4.StylePriority.UseBorders = false;
            this.Detail4.StylePriority.UseBorderWidth = false;
            this.Detail4.StylePriority.UsePadding = false;
            this.Detail4.StylePriority.UseTextAlignment = false;
            this.Detail4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.Detail4.Weight = 0.80754133096919112D;
            // 
            // Detail5
            // 
            this.Detail5.BorderColor = System.Drawing.Color.Black;
            this.Detail5.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.Detail5.BorderWidth = 1F;
            this.Detail5.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ReplenishedPettyCashExpense.Receiver")});
            this.Detail5.Multiline = true;
            this.Detail5.Name = "Detail5";
            this.Detail5.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.Detail5.Scripts.OnBeforePrint = "xrTableCell29_BeforePrint";
            this.Detail5.StylePriority.UseBorderColor = false;
            this.Detail5.StylePriority.UseBorders = false;
            this.Detail5.StylePriority.UseBorderWidth = false;
            this.Detail5.StylePriority.UsePadding = false;
            this.Detail5.StylePriority.UseTextAlignment = false;
            this.Detail5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.Detail5.Weight = 0.80753979455035407D;
            // 
            // Detail6
            // 
            this.Detail6.BorderColor = System.Drawing.Color.Black;
            this.Detail6.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.Detail6.BorderWidth = 1F;
            this.Detail6.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ReplenishedPettyCashExpense.CostCenter")});
            this.Detail6.Name = "Detail6";
            this.Detail6.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.Detail6.Scripts.OnBeforePrint = "xrTableCell14_BeforePrint";
            this.Detail6.StylePriority.UseBorderColor = false;
            this.Detail6.StylePriority.UseBorders = false;
            this.Detail6.StylePriority.UseBorderWidth = false;
            this.Detail6.StylePriority.UsePadding = false;
            this.Detail6.StylePriority.UseTextAlignment = false;
            this.Detail6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.Detail6.Weight = 0.80754161363798616D;
            // 
            // Detail7
            // 
            this.Detail7.BorderColor = System.Drawing.Color.Black;
            this.Detail7.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.Detail7.BorderWidth = 1F;
            this.Detail7.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ReplenishedPettyCashExpense.UDF1")});
            this.Detail7.Name = "Detail7";
            this.Detail7.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 100F);
            this.Detail7.StylePriority.UseBorderColor = false;
            this.Detail7.StylePriority.UseBorders = false;
            this.Detail7.StylePriority.UseBorderWidth = false;
            this.Detail7.StylePriority.UsePadding = false;
            this.Detail7.StylePriority.UseTextAlignment = false;
            this.Detail7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.Detail7.Weight = 0.80754192152238513D;
            // 
            // Detail8
            // 
            this.Detail8.BorderColor = System.Drawing.Color.Black;
            this.Detail8.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.Detail8.BorderWidth = 1F;
            this.Detail8.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ReplenishedPettyCashExpense.Amount")});
            this.Detail8.Name = "Detail8";
            this.Detail8.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 100F);
            this.Detail8.StylePriority.UseBorderColor = false;
            this.Detail8.StylePriority.UseBorders = false;
            this.Detail8.StylePriority.UseBorderWidth = false;
            this.Detail8.StylePriority.UsePadding = false;
            this.Detail8.StylePriority.UseTextAlignment = false;
            this.Detail8.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.Detail8.Weight = 0.80754192152238513D;
            // 
            // DetailB7
            // 
            this.DetailB7.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.DetailB7.LocationFloat = new DevExpress.Utils.PointFloat(805.2125F, 0F);
            this.DetailB7.Name = "DetailB7";
            this.DetailB7.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.DetailB7.SizeF = new System.Drawing.SizeF(14.78748F, 25.00001F);
            this.DetailB7.StylePriority.UseBorders = false;
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
            this.xrPageInfo3.LocationFloat = new DevExpress.Utils.PointFloat(732.2916F, 23.24999F);
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
            this.xrPageInfo2.LocationFloat = new DevExpress.Utils.PointFloat(69.99998F, 23.25001F);
            this.xrPageInfo2.Name = "xrPageInfo2";
            this.xrPageInfo2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrPageInfo2.PageInfo = DevExpress.XtraPrinting.PageInfo.DateTime;
            this.xrPageInfo2.SizeF = new System.Drawing.SizeF(320.4167F, 16.75F);
            this.xrPageInfo2.StylePriority.UseFont = false;
            // 
            // BottomMargin
            // 
            this.BottomMargin.HeightF = 40F;
            this.BottomMargin.Name = "BottomMargin";
            this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
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
            this.xrLabel18.SizeF = new System.Drawing.SizeF(820F, 23F);
            this.xrLabel18.StylePriority.UseFont = false;
            this.xrLabel18.StylePriority.UseTextAlignment = false;
            this.xrLabel18.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // ReportHeader
            // 
            this.ReportHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel4,
            this.xrLabel1,
            this.xrLabel18,
            this.xrLabel15});
            this.ReportHeader.HeightF = 136.2374F;
            this.ReportHeader.Name = "ReportHeader";
            this.ReportHeader.Scripts.OnBeforePrint = "ReportHeader_BeforePrint";
            this.ReportHeader.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.ReportHeader_BeforePrint);
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
            // xrLabel1
            // 
            this.xrLabel1.Font = new System.Drawing.Font("Arial Narrow", 8F, System.Drawing.FontStyle.Bold);
            this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(0.000222524F, 86.00006F);
            this.xrLabel1.Name = "xrLabel1";
            this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel1.SizeF = new System.Drawing.SizeF(819.9997F, 21.16176F);
            this.xrLabel1.StylePriority.UseFont = false;
            this.xrLabel1.StylePriority.UseTextAlignment = false;
            this.xrLabel1.Text = "Replenishment Date:  [Parameters.DateFrom!MM/dd/yy]  -  [Parameters.DateTo!MM/dd/" +
    "yy]";
            this.xrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel15
            // 
            this.xrLabel15.Font = new System.Drawing.Font("Arial Narrow", 13F, System.Drawing.FontStyle.Bold);
            this.xrLabel15.LocationFloat = new DevExpress.Utils.PointFloat(0.000222524F, 62.99998F);
            this.xrLabel15.Multiline = true;
            this.xrLabel15.Name = "xrLabel15";
            this.xrLabel15.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel15.SizeF = new System.Drawing.SizeF(819.9997F, 23F);
            this.xrLabel15.StylePriority.UseFont = false;
            this.xrLabel15.StylePriority.UseTextAlignment = false;
            this.xrLabel15.Text = "REPLENISHED PETTY CASH EXPENSE\r\n";
            this.xrLabel15.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrTable2
            // 
            this.xrTable2.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable2.Font = new System.Drawing.Font("Arial Narrow", 8F, System.Drawing.FontStyle.Bold);
            this.xrTable2.LocationFloat = new DevExpress.Utils.PointFloat(20.14086F, 0F);
            this.xrTable2.Name = "xrTable2";
            this.xrTable2.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow2});
            this.xrTable2.Scripts.OnBeforePrint = "xrTableRow2_BeforePrint";
            this.xrTable2.SizeF = new System.Drawing.SizeF(778.4007F, 25.00002F);
            this.xrTable2.StylePriority.UseBorders = false;
            this.xrTable2.StylePriority.UseFont = false;
            this.xrTable2.StylePriority.UseTextAlignment = false;
            this.xrTable2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrTableRow2
            // 
            this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell34,
            this.xrTableCell33,
            this.Header1,
            this.Header2,
            this.Header3,
            this.Header4,
            this.Header5,
            this.Header6,
            this.Header7,
            this.Header8});
            this.xrTableRow2.Name = "xrTableRow2";
            this.xrTableRow2.Weight = 1D;
            // 
            // xrTableCell34
            // 
            this.xrTableCell34.Name = "xrTableCell34";
            this.xrTableCell34.StylePriority.UseTextAlignment = false;
            this.xrTableCell34.Text = "DocDate";
            this.xrTableCell34.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell34.Weight = 0.65230067834644934D;
            // 
            // xrTableCell33
            // 
            this.xrTableCell33.Name = "xrTableCell33";
            this.xrTableCell33.StylePriority.UseTextAlignment = false;
            this.xrTableCell33.Text = "ReplenishNo";
            this.xrTableCell33.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell33.Weight = 0.76195648288633855D;
            // 
            // Header1
            // 
            this.Header1.Name = "Header1";
            this.Header1.StylePriority.UseTextAlignment = false;
            this.Header1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.Header1.Weight = 0.81748565715402322D;
            // 
            // Header2
            // 
            this.Header2.Name = "Header2";
            this.Header2.StylePriority.UseTextAlignment = false;
            this.Header2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.Header2.Weight = 0.8174856571540231D;
            // 
            // Header3
            // 
            this.Header3.Name = "Header3";
            this.Header3.StylePriority.UseTextAlignment = false;
            this.Header3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.Header3.Weight = 0.81748565715402322D;
            // 
            // Header4
            // 
            this.Header4.Name = "Header4";
            this.Header4.StylePriority.UseTextAlignment = false;
            this.Header4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.Header4.Weight = 0.81748565715402322D;
            // 
            // Header5
            // 
            this.Header5.Name = "Header5";
            this.Header5.StylePriority.UseTextAlignment = false;
            this.Header5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.Header5.Weight = 0.81748565715402322D;
            // 
            // Header6
            // 
            this.Header6.Name = "Header6";
            this.Header6.StylePriority.UseTextAlignment = false;
            this.Header6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.Header6.Weight = 0.81748565715402322D;
            // 
            // Header7
            // 
            this.Header7.Name = "Header7";
            this.Header7.StylePriority.UseTextAlignment = false;
            this.Header7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.Header7.Weight = 0.81748565715402322D;
            // 
            // Header8
            // 
            this.Header8.Name = "Header8";
            this.Header8.StylePriority.UseTextAlignment = false;
            this.Header8.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.Header8.Weight = 0.81748565715402322D;
            // 
            // formattingRule1
            // 
            this.formattingRule1.Name = "formattingRule1";
            // 
            // DateFrom
            // 
            this.DateFrom.Description = "DateFrom";
            this.DateFrom.Name = "DateFrom";
            this.DateFrom.Type = typeof(System.DateTime);
            // 
            // DateTo
            // 
            this.DateTo.Description = "DateTo";
            this.DateTo.Name = "DateTo";
            this.DateTo.Type = typeof(System.DateTime);
            // 
            // GroupHeader1
            // 
            this.GroupHeader1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable1,
            this.GroupB7});
            this.GroupHeader1.GroupFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("ReplenishDocNumber", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});
            this.GroupHeader1.HeightF = 25.00002F;
            this.GroupHeader1.Name = "GroupHeader1";
            // 
            // xrTable1
            // 
            this.xrTable1.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable1.Font = new System.Drawing.Font("Arial Narrow", 8F, System.Drawing.FontStyle.Bold);
            this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(20.14086F, 0F);
            this.xrTable1.Name = "xrTable1";
            this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1});
            this.xrTable1.Scripts.OnBeforePrint = "xrTableRow2_BeforePrint";
            this.xrTable1.SizeF = new System.Drawing.SizeF(778.4006F, 25.00002F);
            this.xrTable1.StylePriority.UseBorders = false;
            this.xrTable1.StylePriority.UseFont = false;
            this.xrTable1.StylePriority.UseTextAlignment = false;
            this.xrTable1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrTableRow1
            // 
            this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell24,
            this.xrTableCell35});
            this.xrTableRow1.Name = "xrTableRow1";
            this.xrTableRow1.Weight = 1D;
            // 
            // xrTableCell24
            // 
            this.xrTableCell24.BorderColor = System.Drawing.Color.Black;
            this.xrTableCell24.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell24.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ReplenishedPettyCashExpense.ReplenishDocDate", "{0:MM/dd/yy}")});
            this.xrTableCell24.Name = "xrTableCell24";
            this.xrTableCell24.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 100F);
            this.xrTableCell24.Scripts.OnBeforePrint = "xrTableCell4_BeforePrint";
            this.xrTableCell24.Scripts.OnHtmlItemCreated = "xrTableCell4_HtmlItemCreated";
            this.xrTableCell24.StylePriority.UseBorderColor = false;
            this.xrTableCell24.StylePriority.UseBorders = false;
            this.xrTableCell24.StylePriority.UseFont = false;
            this.xrTableCell24.StylePriority.UsePadding = false;
            this.xrTableCell24.StylePriority.UseTextAlignment = false;
            this.xrTableCell24.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell24.Weight = 0.65230065566309436D;
            // 
            // xrTableCell35
            // 
            this.xrTableCell35.BorderColor = System.Drawing.Color.Black;
            this.xrTableCell35.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell35.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ReplenishedPettyCashExpense.ReplenishDocNumber")});
            this.xrTableCell35.Name = "xrTableCell35";
            this.xrTableCell35.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 100F);
            this.xrTableCell35.StylePriority.UseBorderColor = false;
            this.xrTableCell35.StylePriority.UseBorders = false;
            this.xrTableCell35.StylePriority.UsePadding = false;
            this.xrTableCell35.StylePriority.UseTextAlignment = false;
            this.xrTableCell35.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell35.Weight = 7.3018406269005913D;
            // 
            // GroupB7
            // 
            this.GroupB7.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.GroupB7.LocationFloat = new DevExpress.Utils.PointFloat(805.2125F, 0F);
            this.GroupB7.Name = "GroupB7";
            this.GroupB7.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.GroupB7.SizeF = new System.Drawing.SizeF(14.78748F, 25.00002F);
            this.GroupB7.StylePriority.UseBorders = false;
            // 
            // ShowDetails
            // 
            this.ShowDetails.Description = "ShowDetails";
            dynamicListLookUpSettings1.DataAdapter = null;
            dynamicListLookUpSettings1.DataMember = "ShowDetails";
            dynamicListLookUpSettings1.DataSource = this.sqlDataSource1;
            dynamicListLookUpSettings1.DisplayMember = "ShowDetails";
            dynamicListLookUpSettings1.ValueMember = "ShowDetails";
            this.ShowDetails.LookUpSettings = dynamicListLookUpSettings1;
            this.ShowDetails.MultiValue = true;
            this.ShowDetails.Name = "ShowDetails";
            this.ShowDetails.ValueInfo = "Summary";
            // 
            // GroupFooter1
            // 
            this.GroupFooter1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable4});
            this.GroupFooter1.HeightF = 25F;
            this.GroupFooter1.Name = "GroupFooter1";
            // 
            // xrTable4
            // 
            this.xrTable4.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable4.Font = new System.Drawing.Font("Arial Narrow", 8F);
            this.xrTable4.LocationFloat = new DevExpress.Utils.PointFloat(20.14086F, 0F);
            this.xrTable4.Name = "xrTable4";
            this.xrTable4.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow3});
            this.xrTable4.SizeF = new System.Drawing.SizeF(778.4005F, 25F);
            this.xrTable4.StylePriority.UseBorders = false;
            this.xrTable4.StylePriority.UseFont = false;
            this.xrTable4.StylePriority.UseTextAlignment = false;
            this.xrTable4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrTableRow3
            // 
            this.xrTableRow3.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.Labelxxx,
            this.Labelyyy});
            this.xrTableRow3.Name = "xrTableRow3";
            this.xrTableRow3.Weight = 1D;
            // 
            // Labelxxx
            // 
            this.Labelxxx.BorderColor = System.Drawing.Color.Black;
            this.Labelxxx.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.Labelxxx.BorderWidth = 1F;
            this.Labelxxx.Font = new System.Drawing.Font("Arial Narrow", 8F, System.Drawing.FontStyle.Bold);
            this.Labelxxx.Name = "Labelxxx";
            this.Labelxxx.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 100F);
            this.Labelxxx.Scripts.OnBeforePrint = "xrTableCell14_BeforePrint";
            this.Labelxxx.StylePriority.UseBorderColor = false;
            this.Labelxxx.StylePriority.UseBorders = false;
            this.Labelxxx.StylePriority.UseBorderWidth = false;
            this.Labelxxx.StylePriority.UseFont = false;
            this.Labelxxx.StylePriority.UsePadding = false;
            this.Labelxxx.StylePriority.UseTextAlignment = false;
            this.Labelxxx.Text = "Daily Total ([ReplenishDocDate!M/d/yyyy]) :";
            this.Labelxxx.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.Labelxxx.Weight = 7.0498385699246073D;
            // 
            // Labelyyy
            // 
            this.Labelyyy.BorderColor = System.Drawing.Color.Black;
            this.Labelyyy.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.Labelyyy.BorderWidth = 1F;
            this.Labelyyy.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ReplenishedPettyCashExpense.Amount")});
            this.Labelyyy.Font = new System.Drawing.Font("Arial Narrow", 8F, System.Drawing.FontStyle.Bold);
            this.Labelyyy.Name = "Labelyyy";
            this.Labelyyy.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 100F);
            this.Labelyyy.StylePriority.UseBorderColor = false;
            this.Labelyyy.StylePriority.UseBorders = false;
            this.Labelyyy.StylePriority.UseBorderWidth = false;
            this.Labelyyy.StylePriority.UseFont = false;
            this.Labelyyy.StylePriority.UsePadding = false;
            this.Labelyyy.StylePriority.UseTextAlignment = false;
            xrSummary3.FormatString = "{0:#,0.00;(#,0.00);}";
            xrSummary3.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            this.Labelyyy.Summary = xrSummary3;
            this.Labelyyy.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.Labelyyy.Weight = 0.80753945710302844D;
            // 
            // PageHeader
            // 
            this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable2});
            this.PageHeader.HeightF = 25.00002F;
            this.PageHeader.Name = "PageHeader";
            // 
            // ReportFooter
            // 
            this.ReportFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel8,
            this.xrLabel9,
            this.xrLabel6,
            this.xrLabel7,
            this.xrLabel5,
            this.xrLabel2});
            this.ReportFooter.HeightF = 204.2026F;
            this.ReportFooter.Name = "ReportFooter";
            // 
            // xrLabel8
            // 
            this.xrLabel8.LocationFloat = new DevExpress.Utils.PointFloat(475.5058F, 78.55678F);
            this.xrLabel8.Name = "xrLabel8";
            this.xrLabel8.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel8.SizeF = new System.Drawing.SizeF(100F, 23F);
            this.xrLabel8.StylePriority.UseTextAlignment = false;
            this.xrLabel8.Text = "Checked By:";
            this.xrLabel8.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel9
            // 
            this.xrLabel9.LocationFloat = new DevExpress.Utils.PointFloat(475.5059F, 101.5568F);
            this.xrLabel9.Name = "xrLabel9";
            this.xrLabel9.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel9.SizeF = new System.Drawing.SizeF(344.4941F, 23.00002F);
            this.xrLabel9.StylePriority.UseTextAlignment = false;
            this.xrLabel9.Text = "________________________________________________";
            this.xrLabel9.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel6
            // 
            this.xrLabel6.LocationFloat = new DevExpress.Utils.PointFloat(0.0002564806F, 158.2027F);
            this.xrLabel6.Name = "xrLabel6";
            this.xrLabel6.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel6.SizeF = new System.Drawing.SizeF(100F, 23F);
            this.xrLabel6.StylePriority.UseTextAlignment = false;
            this.xrLabel6.Text = "Approved By:";
            this.xrLabel6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel7
            // 
            this.xrLabel7.LocationFloat = new DevExpress.Utils.PointFloat(0.0004479379F, 181.2026F);
            this.xrLabel7.Name = "xrLabel7";
            this.xrLabel7.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel7.SizeF = new System.Drawing.SizeF(400F, 23F);
            this.xrLabel7.StylePriority.UseTextAlignment = false;
            this.xrLabel7.Text = "________________________________________________";
            this.xrLabel7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel5
            // 
            this.xrLabel5.LocationFloat = new DevExpress.Utils.PointFloat(0.0004479379F, 101.5568F);
            this.xrLabel5.Name = "xrLabel5";
            this.xrLabel5.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel5.SizeF = new System.Drawing.SizeF(400F, 23F);
            this.xrLabel5.StylePriority.UseTextAlignment = false;
            this.xrLabel5.Text = "________________________________________________";
            this.xrLabel5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel2
            // 
            this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(0.0002239689F, 78.55681F);
            this.xrLabel2.Name = "xrLabel2";
            this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel2.SizeF = new System.Drawing.SizeF(100F, 23F);
            this.xrLabel2.StylePriority.UseTextAlignment = false;
            this.xrLabel2.Text = "Prepared By:";
            this.xrLabel2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // R_ReplenishPettyCashExpense
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.ReportHeader,
            this.GroupHeader1,
            this.GroupFooter1,
            this.PageHeader,
            this.ReportFooter});
            this.ComponentStorage.AddRange(new System.ComponentModel.IComponent[] {
            this.sqlDataSource1});
            this.DataMember = "sp_report_ReplenishedPettyCashExpense";
            this.DataSource = this.sqlDataSource1;
            this.FormattingRuleSheet.AddRange(new DevExpress.XtraReports.UI.FormattingRule[] {
            this.formattingRule1});
            this.Margins = new System.Drawing.Printing.Margins(15, 15, 40, 40);
            this.Parameters.AddRange(new DevExpress.XtraReports.Parameters.Parameter[] {
            this.DateFrom,
            this.DateTo,
            this.ShowDetails});
            this.Scripts.OnBeforePrint = "R_BizPartnerLedger_BeforePrint";
            this.Scripts.OnParametersRequestBeforeShow = "R_UnreplenishPettyCashExpense_ParametersRequestBeforeShow";
            this.Scripts.OnParametersRequestSubmit = "R_BizPartnerLedger_ParametersRequestSubmit";
            this.Scripts.OnParametersRequestValueChanged = "R_UnreplenishPettyCashExpense_ParametersRequestValueChanged";
            this.ScriptsSource = resources.GetString("$this.ScriptsSource");
            this.Version = "15.1";
            this.ParametersRequestBeforeShow += new System.EventHandler<DevExpress.XtraReports.Parameters.ParametersRequestEventArgs>(this.R_ReplenishPettyCashExpense_ParametersRequestBeforeShow);
            this.DataSourceDemanded += new System.EventHandler<System.EventArgs>(this.R_ReplenishPettyCashExpense_DataSourceDemanded);
            ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable4)).EndInit();
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
        private DevExpress.XtraReports.UI.ReportHeaderBand ReportHeader;
        private DevExpress.XtraReports.UI.XRTable xrTable3;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow5;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell19;
        private DevExpress.XtraReports.UI.XRTableCell Detail3;
        private DevExpress.XtraReports.UI.XRTableCell Detail4;
        private DevExpress.XtraReports.UI.XRTableCell Detail5;
        private DevExpress.XtraReports.UI.XRLabel xrLabel15;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell20;
        private DevExpress.XtraReports.UI.XRTable xrTable2;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow2;
        private DevExpress.XtraReports.UI.XRLabel xrLabel1;
        private DevExpress.XtraReports.UI.XRTableCell Detail1;
        private DevExpress.XtraReports.UI.XRTableCell Detail2;
        private FormattingRule formattingRule1;
        private XRTableCell Detail6;
        private Parameter DateFrom;
        private Parameter DateTo;
        private XRTableCell xrTableCell34;
        private XRTableCell xrTableCell33;
        private GroupHeaderBand GroupHeader1;
        private XRTable xrTable1;
        private XRTableRow xrTableRow1;
        private XRTableCell xrTableCell24;
        private XRTableCell xrTableCell35;
        private Parameter ShowDetails;
        private XRTableCell Detail7;
        private XRTableCell Header1;
        private XRTableCell Header2;
        private XRTableCell Header3;
        private XRTableCell Header4;
        private XRTableCell Header5;
        private XRTableCell Header6;
        private XRTableCell Header7;
        private XRLabel DetailB7;
        private XRLabel GroupB7;
        private XRLabel xrLabel3;
        private XRPageInfo xrPageInfo2;
        private XRPageInfo xrPageInfo3;
        private XRLabel xrLabel4;
        private GroupFooterBand GroupFooter1;
        private XRTable xrTable4;
        private XRTableRow xrTableRow3;
        private XRTableCell Labelxxx;
        private XRTableCell Labelyyy;
        private PageHeaderBand PageHeader;
        private XRTableCell Header8;
        private XRTableCell Detail8;
        private ReportFooterBand ReportFooter;
        private XRLabel xrLabel8;
        private XRLabel xrLabel9;
        private XRLabel xrLabel6;
        private XRLabel xrLabel7;
        private XRLabel xrLabel5;
        private XRLabel xrLabel2;

    }
}
