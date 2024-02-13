﻿using System;
using System.Windows.Forms;
using DevExpress.XtraReports.UI;
using DevExpress.XtraReports.Parameters;

namespace GWL.WebReports.GEARS_Reports
{
    partial class R_APListing

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(R_APListing));
            DevExpress.DataAccess.Sql.CustomSqlQuery customSqlQuery4 = new DevExpress.DataAccess.Sql.CustomSqlQuery();
            DevExpress.DataAccess.Sql.CustomSqlQuery customSqlQuery5 = new DevExpress.DataAccess.Sql.CustomSqlQuery();
            DevExpress.DataAccess.Sql.StoredProcQuery storedProcQuery1 = new DevExpress.DataAccess.Sql.StoredProcQuery();
            DevExpress.DataAccess.Sql.QueryParameter queryParameter1 = new DevExpress.DataAccess.Sql.QueryParameter();
            DevExpress.DataAccess.Sql.QueryParameter queryParameter2 = new DevExpress.DataAccess.Sql.QueryParameter();
            DevExpress.DataAccess.Sql.QueryParameter queryParameter3 = new DevExpress.DataAccess.Sql.QueryParameter();
            DevExpress.DataAccess.Sql.QueryParameter queryParameter4 = new DevExpress.DataAccess.Sql.QueryParameter();
            DevExpress.DataAccess.Sql.QueryParameter queryParameter5 = new DevExpress.DataAccess.Sql.QueryParameter();
            DevExpress.DataAccess.Sql.QueryParameter queryParameter6 = new DevExpress.DataAccess.Sql.QueryParameter();
            DevExpress.DataAccess.Sql.QueryParameter queryParameter7 = new DevExpress.DataAccess.Sql.QueryParameter();
            DevExpress.XtraReports.UI.XRSummary xrSummary1 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary2 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary3 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.Parameters.DynamicListLookUpSettings dynamicListLookUpSettings1 = new DevExpress.XtraReports.Parameters.DynamicListLookUpSettings();
            DevExpress.XtraReports.Parameters.DynamicListLookUpSettings dynamicListLookUpSettings2 = new DevExpress.XtraReports.Parameters.DynamicListLookUpSettings();
            DevExpress.XtraReports.Parameters.DynamicListLookUpSettings dynamicListLookUpSettings3 = new DevExpress.XtraReports.Parameters.DynamicListLookUpSettings();
            DevExpress.XtraReports.UI.XRSummary xrSummary4 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary5 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary6 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary7 = new DevExpress.XtraReports.UI.XRSummary();
            this.sqlDataSource1 = new DevExpress.DataAccess.Sql.SqlDataSource();
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.Detail1 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow5 = new DevExpress.XtraReports.UI.XRTableRow();
            this.Detail1Col1 = new DevExpress.XtraReports.UI.XRTableCell();
            this.Detail1Col2 = new DevExpress.XtraReports.UI.XRTableCell();
            this.Detail1Col3 = new DevExpress.XtraReports.UI.XRTableCell();
            this.Detail1Col4 = new DevExpress.XtraReports.UI.XRTableCell();
            this.Detail1Col5 = new DevExpress.XtraReports.UI.XRTableCell();
            this.Detail1Col6 = new DevExpress.XtraReports.UI.XRTableCell();
            this.Detail2 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow6 = new DevExpress.XtraReports.UI.XRTableRow();
            this.Detail2Col1 = new DevExpress.XtraReports.UI.XRTableCell();
            this.Detail2Col2 = new DevExpress.XtraReports.UI.XRTableCell();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.xrPageInfo3 = new DevExpress.XtraReports.UI.XRPageInfo();
            this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrPageInfo2 = new DevExpress.XtraReports.UI.XRPageInfo();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.DocDateFrom = new DevExpress.XtraReports.Parameters.Parameter();
            this.DocDateTo = new DevExpress.XtraReports.Parameters.Parameter();
            this.DueDateFrom = new DevExpress.XtraReports.Parameters.Parameter();
            this.DueDateTo = new DevExpress.XtraReports.Parameters.Parameter();
            this.xrLabel18 = new DevExpress.XtraReports.UI.XRLabel();
            this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
            this.xrLabel5 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel4 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel3 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel15 = new DevExpress.XtraReports.UI.XRLabel();
            this.formattingRule1 = new DevExpress.XtraReports.UI.FormattingRule();
            this.Supplier = new DevExpress.XtraReports.Parameters.Parameter();
            this.DocDateFrom_Actual = new DevExpress.XtraReports.Parameters.Parameter();
            this.DocDateTo_Actual = new DevExpress.XtraReports.Parameters.Parameter();
            this.DueDateFrom_Actual = new DevExpress.XtraReports.Parameters.Parameter();
            this.DueDateTo_Actual = new DevExpress.XtraReports.Parameters.Parameter();
            this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
            this.xrTable4 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow3 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell8 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell9 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell10 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell13 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell14 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell16 = new DevExpress.XtraReports.UI.XRTableCell();
            this.TransType = new DevExpress.XtraReports.Parameters.Parameter();
            this.ReportInfo = new DevExpress.XtraReports.Parameters.Parameter();
            this.ShowDetail = new DevExpress.XtraReports.Parameters.Parameter();
            this.Summary = new DevExpress.XtraReports.Parameters.Parameter();
            this.GroupHeader1 = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.xrTable5 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow4 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell5 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell15 = new DevExpress.XtraReports.UI.XRTableCell();
            this.GroupFooter1 = new DevExpress.XtraReports.UI.GroupFooterBand();
            this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell11 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell4 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell6 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell12 = new DevExpress.XtraReports.UI.XRTableCell();
            this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
            this.xrTable2 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell17 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell18 = new DevExpress.XtraReports.UI.XRTableCell();
            ((System.ComponentModel.ISupportInitialize)(this.Detail1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Detail2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).BeginInit();
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
            customSqlQuery3.Name = "SupplierCode";
            customSqlQuery3.Sql = resources.GetString("customSqlQuery3.Sql");
            customSqlQuery4.Name = "TransType";
            customSqlQuery4.Sql = resources.GetString("customSqlQuery4.Sql");
            customSqlQuery5.Name = "ReportInfo";
            customSqlQuery5.Sql = "SELECT \'Outstanding\' AS ReportInfo\r\nUNION ALL\r\nSELECT \'All Records\' AS ReportInfo" +
    "";
            storedProcQuery1.Name = "sp_report_AccountsPayableListing";
            queryParameter1.Name = "@DocDateFr";
            queryParameter1.Type = typeof(DevExpress.DataAccess.Expression);
            queryParameter1.Value = new DevExpress.DataAccess.Expression("[Parameters.DocDateFrom]", typeof(System.DateTime));
            queryParameter2.Name = "@DocDateTo";
            queryParameter2.Type = typeof(DevExpress.DataAccess.Expression);
            queryParameter2.Value = new DevExpress.DataAccess.Expression("[Parameters.DocDateTo]", typeof(System.DateTime));
            queryParameter3.Name = "@DueDateFr";
            queryParameter3.Type = typeof(DevExpress.DataAccess.Expression);
            queryParameter3.Value = new DevExpress.DataAccess.Expression("[Parameters.DueDateFrom]", typeof(System.DateTime));
            queryParameter4.Name = "@DueDateTo";
            queryParameter4.Type = typeof(DevExpress.DataAccess.Expression);
            queryParameter4.Value = new DevExpress.DataAccess.Expression("[Parameters.DueDateTo]", typeof(System.DateTime));
            queryParameter5.Name = "@TransType";
            queryParameter5.Type = typeof(DevExpress.DataAccess.Expression);
            queryParameter5.Value = new DevExpress.DataAccess.Expression("[Parameters.TransType]", typeof(string));
            queryParameter6.Name = "@Vendor";
            queryParameter6.Type = typeof(DevExpress.DataAccess.Expression);
            queryParameter6.Value = new DevExpress.DataAccess.Expression("ToStr([Parameters.Supplier])", typeof(string));
            queryParameter7.Name = "@ReportType";
            queryParameter7.Type = typeof(DevExpress.DataAccess.Expression);
            queryParameter7.Value = new DevExpress.DataAccess.Expression("[Parameters.ReportInfo]", typeof(string));
            storedProcQuery1.Parameters.Add(queryParameter1);
            storedProcQuery1.Parameters.Add(queryParameter2);
            storedProcQuery1.Parameters.Add(queryParameter3);
            storedProcQuery1.Parameters.Add(queryParameter4);
            storedProcQuery1.Parameters.Add(queryParameter5);
            storedProcQuery1.Parameters.Add(queryParameter6);
            storedProcQuery1.Parameters.Add(queryParameter7);
            storedProcQuery1.StoredProcName = "sp_report_AccountsPayableListing";
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
            this.Detail1,
            this.Detail2});
            this.Detail.HeightF = 50F;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // Detail1
            // 
            this.Detail1.BorderColor = System.Drawing.Color.Gainsboro;
            this.Detail1.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.Detail1.Font = new System.Drawing.Font("Arial Narrow", 8F);
            this.Detail1.LocationFloat = new DevExpress.Utils.PointFloat(50F, 0F);
            this.Detail1.Name = "Detail1";
            this.Detail1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow5});
            this.Detail1.SizeF = new System.Drawing.SizeF(706.0834F, 25F);
            this.Detail1.StylePriority.UseBorderColor = false;
            this.Detail1.StylePriority.UseBorders = false;
            this.Detail1.StylePriority.UseFont = false;
            this.Detail1.StylePriority.UseTextAlignment = false;
            this.Detail1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.Detail1.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.Detail1_BeforePrint);
            // 
            // xrTableRow5
            // 
            this.xrTableRow5.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.Detail1Col1,
            this.Detail1Col2,
            this.Detail1Col3,
            this.Detail1Col4,
            this.Detail1Col5,
            this.Detail1Col6});
            this.xrTableRow5.Name = "xrTableRow5";
            this.xrTableRow5.Weight = 1D;
            // 
            // Detail1Col1
            // 
            this.Detail1Col1.BorderColor = System.Drawing.Color.Black;
            this.Detail1Col1.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.Detail1Col1.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_AccountsPayableListing.DocDate", "{0:MM/dd/yy}")});
            this.Detail1Col1.Name = "Detail1Col1";
            this.Detail1Col1.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 100F);
            this.Detail1Col1.StylePriority.UseBorderColor = false;
            this.Detail1Col1.StylePriority.UseBorders = false;
            this.Detail1Col1.StylePriority.UsePadding = false;
            this.Detail1Col1.StylePriority.UseTextAlignment = false;
            this.Detail1Col1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.Detail1Col1.Weight = 1.009426285412248D;
            // 
            // Detail1Col2
            // 
            this.Detail1Col2.BorderColor = System.Drawing.Color.Black;
            this.Detail1Col2.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.Detail1Col2.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_AccountsPayableListing.DocNumber")});
            this.Detail1Col2.Name = "Detail1Col2";
            this.Detail1Col2.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 100F);
            this.Detail1Col2.StylePriority.UseBorderColor = false;
            this.Detail1Col2.StylePriority.UseBorders = false;
            this.Detail1Col2.StylePriority.UsePadding = false;
            this.Detail1Col2.StylePriority.UseTextAlignment = false;
            this.Detail1Col2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.Detail1Col2.Weight = 1.0094266939510452D;
            // 
            // Detail1Col3
            // 
            this.Detail1Col3.BorderColor = System.Drawing.Color.Black;
            this.Detail1Col3.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.Detail1Col3.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_AccountsPayableListing.AuxilaryReference", "{0:MM/dd/yy}")});
            this.Detail1Col3.Name = "Detail1Col3";
            this.Detail1Col3.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.Detail1Col3.StylePriority.UseBorderColor = false;
            this.Detail1Col3.StylePriority.UseBorders = false;
            this.Detail1Col3.StylePriority.UsePadding = false;
            this.Detail1Col3.StylePriority.UseTextAlignment = false;
            this.Detail1Col3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.Detail1Col3.Weight = 1.5351683805298115D;
            // 
            // Detail1Col4
            // 
            this.Detail1Col4.BorderColor = System.Drawing.Color.Black;
            this.Detail1Col4.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.Detail1Col4.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_AccountsPayableListing.Amount", "{0:#,0.00;(#,0.00);}")});
            this.Detail1Col4.Name = "Detail1Col4";
            this.Detail1Col4.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 100F);
            this.Detail1Col4.SnapLineMargin = new DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 100F);
            this.Detail1Col4.StylePriority.UseBorderColor = false;
            this.Detail1Col4.StylePriority.UseBorders = false;
            this.Detail1Col4.StylePriority.UsePadding = false;
            this.Detail1Col4.StylePriority.UseTextAlignment = false;
            this.Detail1Col4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.Detail1Col4.Weight = 1.1911228533770042D;
            // 
            // Detail1Col5
            // 
            this.Detail1Col5.BorderColor = System.Drawing.Color.Black;
            this.Detail1Col5.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.Detail1Col5.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_AccountsPayableListing.Balance", "{0:#,0.00;(#,0.00);}")});
            this.Detail1Col5.Name = "Detail1Col5";
            this.Detail1Col5.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 100F);
            this.Detail1Col5.SnapLineMargin = new DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 100F);
            this.Detail1Col5.StylePriority.UseBorderColor = false;
            this.Detail1Col5.StylePriority.UseBorders = false;
            this.Detail1Col5.StylePriority.UsePadding = false;
            this.Detail1Col5.StylePriority.UseTextAlignment = false;
            this.Detail1Col5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.Detail1Col5.Weight = 1.1911228331189856D;
            // 
            // Detail1Col6
            // 
            this.Detail1Col6.BorderColor = System.Drawing.Color.Black;
            this.Detail1Col6.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.Detail1Col6.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_AccountsPayableListing.DueDate", "{0:MM/dd/yy}")});
            this.Detail1Col6.Name = "Detail1Col6";
            this.Detail1Col6.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 100F);
            this.Detail1Col6.StylePriority.UseBorderColor = false;
            this.Detail1Col6.StylePriority.UseBorders = false;
            this.Detail1Col6.StylePriority.UsePadding = false;
            this.Detail1Col6.StylePriority.UseTextAlignment = false;
            this.Detail1Col6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.Detail1Col6.Weight = 1.1911230875797454D;
            // 
            // Detail2
            // 
            this.Detail2.BorderColor = System.Drawing.Color.Gainsboro;
            this.Detail2.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.Detail2.Font = new System.Drawing.Font("Arial Narrow", 8F);
            this.Detail2.LocationFloat = new DevExpress.Utils.PointFloat(50.00022F, 25F);
            this.Detail2.Name = "Detail2";
            this.Detail2.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow6});
            this.Detail2.SizeF = new System.Drawing.SizeF(706.0832F, 25F);
            this.Detail2.StylePriority.UseBorderColor = false;
            this.Detail2.StylePriority.UseBorders = false;
            this.Detail2.StylePriority.UseFont = false;
            this.Detail2.StylePriority.UseTextAlignment = false;
            this.Detail2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.Detail2.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.Detail2_BeforePrint);
            // 
            // xrTableRow6
            // 
            this.xrTableRow6.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.Detail2Col1,
            this.Detail2Col2});
            this.xrTableRow6.Name = "xrTableRow6";
            this.xrTableRow6.Weight = 1D;
            // 
            // Detail2Col1
            // 
            this.Detail2Col1.BorderColor = System.Drawing.Color.Black;
            this.Detail2Col1.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.Detail2Col1.Name = "Detail2Col1";
            this.Detail2Col1.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 100F);
            this.Detail2Col1.StylePriority.UseBorderColor = false;
            this.Detail2Col1.StylePriority.UseBorders = false;
            this.Detail2Col1.StylePriority.UsePadding = false;
            this.Detail2Col1.StylePriority.UseTextAlignment = false;
            this.Detail2Col1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.Detail2Col1.Weight = 2.0188523250099548D;
            // 
            // Detail2Col2
            // 
            this.Detail2Col2.BorderColor = System.Drawing.Color.Black;
            this.Detail2Col2.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.Detail2Col2.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_AccountsPayableListing.UseDetail")});
            this.Detail2Col2.Name = "Detail2Col2";
            this.Detail2Col2.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 100F);
            this.Detail2Col2.StylePriority.UseBorderColor = false;
            this.Detail2Col2.StylePriority.UseBorders = false;
            this.Detail2Col2.StylePriority.UsePadding = false;
            this.Detail2Col2.StylePriority.UseTextAlignment = false;
            this.Detail2Col2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.Detail2Col2.Weight = 5.1085349598817036D;
            // 
            // TopMargin
            // 
            this.TopMargin.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPageInfo3,
            this.xrLabel2,
            this.xrPageInfo2});
            this.TopMargin.HeightF = 40.00002F;
            this.TopMargin.Name = "TopMargin";
            this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrPageInfo3
            // 
            this.xrPageInfo3.Font = new System.Drawing.Font("Times New Roman", 8F);
            this.xrPageInfo3.Format = "Page {0} of {1}";
            this.xrPageInfo3.LocationFloat = new DevExpress.Utils.PointFloat(668.3749F, 23.25002F);
            this.xrPageInfo3.Name = "xrPageInfo3";
            this.xrPageInfo3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrPageInfo3.SizeF = new System.Drawing.SizeF(87.70837F, 16.75F);
            this.xrPageInfo3.StylePriority.UseFont = false;
            this.xrPageInfo3.StylePriority.UseTextAlignment = false;
            this.xrPageInfo3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
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
            xrSummary1.FormatString = "{0:M/d/yy}";
            xrSummary1.Func = DevExpress.XtraReports.UI.SummaryFunc.Custom;
            this.xrLabel2.Summary = xrSummary1;
            this.xrLabel2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrPageInfo2
            // 
            this.xrPageInfo2.Font = new System.Drawing.Font("Times New Roman", 8F);
            this.xrPageInfo2.Format = "Run Date & Time : {0:MMMM dd, yyyy / h:mm tt}";
            this.xrPageInfo2.LocationFloat = new DevExpress.Utils.PointFloat(50F, 23.25001F);
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
            // DocDateFrom
            // 
            this.DocDateFrom.Description = "DocDateFrom";
            this.DocDateFrom.Name = "DocDateFrom";
            this.DocDateFrom.Type = typeof(System.DateTime);
            // 
            // DocDateTo
            // 
            this.DocDateTo.Description = "DocDateTo";
            this.DocDateTo.Name = "DocDateTo";
            this.DocDateTo.Type = typeof(System.DateTime);
            // 
            // DueDateFrom
            // 
            this.DueDateFrom.Description = "DueDateFrom";
            this.DueDateFrom.Name = "DueDateFrom";
            this.DueDateFrom.Type = typeof(System.DateTime);
            // 
            // DueDateTo
            // 
            this.DueDateTo.Description = "DueDateTo";
            this.DueDateTo.Name = "DueDateTo";
            this.DueDateTo.Type = typeof(System.DateTime);
            // 
            // xrLabel18
            // 
            this.xrLabel18.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "CompanyName.Value")});
            this.xrLabel18.Font = new System.Drawing.Font("Arial Narrow", 9.75F);
            this.xrLabel18.LocationFloat = new DevExpress.Utils.PointFloat(50F, 39.99996F);
            this.xrLabel18.Name = "xrLabel18";
            this.xrLabel18.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel18.SizeF = new System.Drawing.SizeF(706.0833F, 25.00002F);
            this.xrLabel18.StylePriority.UseFont = false;
            this.xrLabel18.StylePriority.UseTextAlignment = false;
            this.xrLabel18.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // ReportHeader
            // 
            this.ReportHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel5,
            this.xrLabel4,
            this.xrLabel3,
            this.xrLabel18,
            this.xrLabel15});
            this.ReportHeader.HeightF = 165F;
            this.ReportHeader.Name = "ReportHeader";
            this.ReportHeader.Scripts.OnBeforePrint = "ReportHeader_BeforePrint";
            // 
            // xrLabel5
            // 
            this.xrLabel5.Font = new System.Drawing.Font("Arial Narrow", 8F, System.Drawing.FontStyle.Bold);
            this.xrLabel5.LocationFloat = new DevExpress.Utils.PointFloat(50F, 115F);
            this.xrLabel5.Name = "xrLabel5";
            this.xrLabel5.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel5.Scripts.OnBeforePrint = "xrLabel5_BeforePrint";
            this.xrLabel5.SizeF = new System.Drawing.SizeF(706.0833F, 50.00001F);
            this.xrLabel5.StylePriority.UseFont = false;
            this.xrLabel5.StylePriority.UseTextAlignment = false;
            xrSummary2.FormatString = "{0:M/d/yy}";
            xrSummary2.Func = DevExpress.XtraReports.UI.SummaryFunc.Custom;
            this.xrLabel5.Summary = xrSummary2;
            this.xrLabel5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel4
            // 
            this.xrLabel4.Font = new System.Drawing.Font("Arial Narrow", 8F, System.Drawing.FontStyle.Bold);
            this.xrLabel4.LocationFloat = new DevExpress.Utils.PointFloat(50F, 89.99996F);
            this.xrLabel4.Multiline = true;
            this.xrLabel4.Name = "xrLabel4";
            this.xrLabel4.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel4.SizeF = new System.Drawing.SizeF(706.0833F, 25.00002F);
            this.xrLabel4.StylePriority.UseFont = false;
            this.xrLabel4.StylePriority.UseTextAlignment = false;
            this.xrLabel4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel3
            // 
            this.xrLabel3.Font = new System.Drawing.Font("Arial Narrow", 8F, System.Drawing.FontStyle.Bold);
            this.xrLabel3.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrLabel3.Name = "xrLabel3";
            this.xrLabel3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel3.Scripts.OnBeforePrint = "xrLabel5_BeforePrint";
            this.xrLabel3.SizeF = new System.Drawing.SizeF(776.6671F, 40F);
            this.xrLabel3.StylePriority.UseFont = false;
            this.xrLabel3.StylePriority.UseTextAlignment = false;
            xrSummary3.FormatString = "{0:M/d/yy}";
            xrSummary3.Func = DevExpress.XtraReports.UI.SummaryFunc.Custom;
            this.xrLabel3.Summary = xrSummary3;
            this.xrLabel3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel15
            // 
            this.xrLabel15.Font = new System.Drawing.Font("Arial Narrow", 13F, System.Drawing.FontStyle.Bold);
            this.xrLabel15.LocationFloat = new DevExpress.Utils.PointFloat(50F, 64.99996F);
            this.xrLabel15.Name = "xrLabel15";
            this.xrLabel15.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel15.SizeF = new System.Drawing.SizeF(706.0833F, 25F);
            this.xrLabel15.StylePriority.UseFont = false;
            this.xrLabel15.StylePriority.UseTextAlignment = false;
            this.xrLabel15.Text = "ACCOUNTS PAYABLE LISTING";
            this.xrLabel15.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // formattingRule1
            // 
            this.formattingRule1.Name = "formattingRule1";
            // 
            // Supplier
            // 
            this.Supplier.Description = "Supplier";
            dynamicListLookUpSettings1.DataAdapter = null;
            dynamicListLookUpSettings1.DataMember = "SupplierCode";
            dynamicListLookUpSettings1.DataSource = this.sqlDataSource1;
            dynamicListLookUpSettings1.DisplayMember = "Name";
            dynamicListLookUpSettings1.ValueMember = "SupplierCode";
            this.Supplier.LookUpSettings = dynamicListLookUpSettings1;
            this.Supplier.MultiValue = true;
            this.Supplier.Name = "Supplier";
            this.Supplier.ValueInfo = "ALL";
            // 
            // DocDateFrom_Actual
            // 
            this.DocDateFrom_Actual.Description = "DocDateFrom_Actual";
            this.DocDateFrom_Actual.Name = "DocDateFrom_Actual";
            this.DocDateFrom_Actual.Type = typeof(System.DateTime);
            this.DocDateFrom_Actual.ValueInfo = "2016-05-17";
            this.DocDateFrom_Actual.Visible = false;
            // 
            // DocDateTo_Actual
            // 
            this.DocDateTo_Actual.Description = "DocDateTo_Actual";
            this.DocDateTo_Actual.Name = "DocDateTo_Actual";
            this.DocDateTo_Actual.Type = typeof(System.DateTime);
            this.DocDateTo_Actual.ValueInfo = "2016-05-17";
            this.DocDateTo_Actual.Visible = false;
            // 
            // DueDateFrom_Actual
            // 
            this.DueDateFrom_Actual.Description = "DueDateFrom_Actual";
            this.DueDateFrom_Actual.Name = "DueDateFrom_Actual";
            this.DueDateFrom_Actual.Type = typeof(System.DateTime);
            this.DueDateFrom_Actual.ValueInfo = "2016-05-17";
            this.DueDateFrom_Actual.Visible = false;
            // 
            // DueDateTo_Actual
            // 
            this.DueDateTo_Actual.Description = "DueDateTo_Actual";
            this.DueDateTo_Actual.Name = "DueDateTo_Actual";
            this.DueDateTo_Actual.Type = typeof(System.DateTime);
            this.DueDateTo_Actual.ValueInfo = "2016-05-17";
            this.DueDateTo_Actual.Visible = false;
            // 
            // PageHeader
            // 
            this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable4});
            this.PageHeader.HeightF = 25.00002F;
            this.PageHeader.Name = "PageHeader";
            // 
            // xrTable4
            // 
            this.xrTable4.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable4.Font = new System.Drawing.Font("Arial Narrow", 8F, System.Drawing.FontStyle.Bold);
            this.xrTable4.LocationFloat = new DevExpress.Utils.PointFloat(50F, 0F);
            this.xrTable4.Name = "xrTable4";
            this.xrTable4.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow3});
            this.xrTable4.Scripts.OnBeforePrint = "xrTableRow2_BeforePrint";
            this.xrTable4.SizeF = new System.Drawing.SizeF(706.0834F, 25.00002F);
            this.xrTable4.StylePriority.UseBorders = false;
            this.xrTable4.StylePriority.UseFont = false;
            this.xrTable4.StylePriority.UseTextAlignment = false;
            this.xrTable4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrTableRow3
            // 
            this.xrTableRow3.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell8,
            this.xrTableCell9,
            this.xrTableCell10,
            this.xrTableCell13,
            this.xrTableCell14,
            this.xrTableCell16});
            this.xrTableRow3.Name = "xrTableRow3";
            this.xrTableRow3.Weight = 1D;
            // 
            // xrTableCell8
            // 
            this.xrTableCell8.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell8.Name = "xrTableCell8";
            this.xrTableCell8.StylePriority.UseBorders = false;
            this.xrTableCell8.StylePriority.UseFont = false;
            this.xrTableCell8.StylePriority.UseTextAlignment = false;
            this.xrTableCell8.Text = "DocDate";
            this.xrTableCell8.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell8.Weight = 1.0218572800763341D;
            // 
            // xrTableCell9
            // 
            this.xrTableCell9.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell9.Name = "xrTableCell9";
            this.xrTableCell9.StylePriority.UseBorders = false;
            this.xrTableCell9.StylePriority.UseFont = false;
            this.xrTableCell9.Text = "DocNumber";
            this.xrTableCell9.Weight = 1.0218572742382728D;
            // 
            // xrTableCell10
            // 
            this.xrTableCell10.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell10.Name = "xrTableCell10";
            this.xrTableCell10.StylePriority.UseBorders = false;
            this.xrTableCell10.StylePriority.UseFont = false;
            this.xrTableCell10.Text = "Aux. Ref / DR No. / SI No.";
            this.xrTableCell10.Weight = 1.5540746674925916D;
            // 
            // xrTableCell13
            // 
            this.xrTableCell13.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell13.Name = "xrTableCell13";
            this.xrTableCell13.StylePriority.UseBorders = false;
            this.xrTableCell13.StylePriority.UseFont = false;
            this.xrTableCell13.Text = "Amount";
            this.xrTableCell13.Weight = 1.2057915960343282D;
            // 
            // xrTableCell14
            // 
            this.xrTableCell14.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell14.Name = "xrTableCell14";
            this.xrTableCell14.StylePriority.UseBorders = false;
            this.xrTableCell14.StylePriority.UseFont = false;
            this.xrTableCell14.StylePriority.UseTextAlignment = false;
            this.xrTableCell14.Text = "Balance";
            this.xrTableCell14.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell14.Weight = 1.2057915677615647D;
            // 
            // xrTableCell16
            // 
            this.xrTableCell16.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell16.Name = "xrTableCell16";
            this.xrTableCell16.StylePriority.UseBorders = false;
            this.xrTableCell16.StylePriority.UseFont = false;
            this.xrTableCell16.StylePriority.UseTextAlignment = false;
            this.xrTableCell16.Text = "DueDate";
            this.xrTableCell16.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell16.Weight = 1.2057918658968339D;
            // 
            // TransType
            // 
            this.TransType.Description = "TransType";
            dynamicListLookUpSettings2.DataAdapter = null;
            dynamicListLookUpSettings2.DataMember = "TransType";
            dynamicListLookUpSettings2.DataSource = this.sqlDataSource1;
            dynamicListLookUpSettings2.DisplayMember = "TransName";
            dynamicListLookUpSettings2.ValueMember = "TransType";
            this.TransType.LookUpSettings = dynamicListLookUpSettings2;
            this.TransType.Name = "TransType";
            this.TransType.ValueInfo = "ALL";
            // 
            // ReportInfo
            // 
            this.ReportInfo.Description = "ReportInfo";
            dynamicListLookUpSettings3.DataAdapter = null;
            dynamicListLookUpSettings3.DataMember = "ReportInfo";
            dynamicListLookUpSettings3.DataSource = this.sqlDataSource1;
            dynamicListLookUpSettings3.DisplayMember = "ReportInfo";
            dynamicListLookUpSettings3.ValueMember = "ReportInfo";
            this.ReportInfo.LookUpSettings = dynamicListLookUpSettings3;
            this.ReportInfo.Name = "ReportInfo";
            this.ReportInfo.ValueInfo = "All Records";
            // 
            // ShowDetail
            // 
            this.ShowDetail.Description = "ShowDetail";
            this.ShowDetail.Name = "ShowDetail";
            this.ShowDetail.Type = typeof(bool);
            this.ShowDetail.ValueInfo = "False";
            // 
            // Summary
            // 
            this.Summary.Description = "Summary";
            this.Summary.Name = "Summary";
            this.Summary.Type = typeof(bool);
            this.Summary.ValueInfo = "False";
            // 
            // GroupHeader1
            // 
            this.GroupHeader1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable5});
            this.GroupHeader1.GroupFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("Name", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});
            this.GroupHeader1.HeightF = 25.00002F;
            this.GroupHeader1.Name = "GroupHeader1";
            // 
            // xrTable5
            // 
            this.xrTable5.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable5.Font = new System.Drawing.Font("Arial Narrow", 8F, System.Drawing.FontStyle.Bold);
            this.xrTable5.LocationFloat = new DevExpress.Utils.PointFloat(50F, 0F);
            this.xrTable5.Name = "xrTable5";
            this.xrTable5.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow4});
            this.xrTable5.Scripts.OnBeforePrint = "xrTableRow2_BeforePrint";
            this.xrTable5.SizeF = new System.Drawing.SizeF(706.0833F, 25.00002F);
            this.xrTable5.StylePriority.UseBorders = false;
            this.xrTable5.StylePriority.UseFont = false;
            this.xrTable5.StylePriority.UseTextAlignment = false;
            this.xrTable5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrTableRow4
            // 
            this.xrTableRow4.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell5,
            this.xrTableCell15});
            this.xrTableRow4.Name = "xrTableRow4";
            this.xrTableRow4.Weight = 1D;
            // 
            // xrTableCell5
            // 
            this.xrTableCell5.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell5.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_AccountsPayableListing.Code")});
            this.xrTableCell5.Name = "xrTableCell5";
            this.xrTableCell5.Padding = new DevExpress.XtraPrinting.PaddingInfo(10, 0, 0, 0, 100F);
            this.xrTableCell5.StylePriority.UseBorders = false;
            this.xrTableCell5.StylePriority.UseFont = false;
            this.xrTableCell5.StylePriority.UsePadding = false;
            this.xrTableCell5.StylePriority.UseTextAlignment = false;
            this.xrTableCell5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell5.Weight = 4.8240192347564665D;
            // 
            // xrTableCell15
            // 
            this.xrTableCell15.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell15.Name = "xrTableCell15";
            this.xrTableCell15.StylePriority.UseBorders = false;
            this.xrTableCell15.StylePriority.UseFont = false;
            this.xrTableCell15.Weight = 2.3911441751725016D;
            // 
            // GroupFooter1
            // 
            this.GroupFooter1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable1});
            this.GroupFooter1.HeightF = 25F;
            this.GroupFooter1.Name = "GroupFooter1";
            // 
            // xrTable1
            // 
            this.xrTable1.BorderColor = System.Drawing.Color.Gainsboro;
            this.xrTable1.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTable1.Font = new System.Drawing.Font("Arial Narrow", 8F);
            this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(50.00022F, 0F);
            this.xrTable1.Name = "xrTable1";
            this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1});
            this.xrTable1.SizeF = new System.Drawing.SizeF(706.0831F, 25F);
            this.xrTable1.StylePriority.UseBorderColor = false;
            this.xrTable1.StylePriority.UseBorders = false;
            this.xrTable1.StylePriority.UseFont = false;
            this.xrTable1.StylePriority.UseTextAlignment = false;
            this.xrTable1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrTableRow1
            // 
            this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell11,
            this.xrTableCell4,
            this.xrTableCell6,
            this.xrTableCell12});
            this.xrTableRow1.Name = "xrTableRow1";
            this.xrTableRow1.Weight = 1D;
            // 
            // xrTableCell11
            // 
            this.xrTableCell11.BorderColor = System.Drawing.Color.Black;
            this.xrTableCell11.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell11.Name = "xrTableCell11";
            this.xrTableCell11.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.xrTableCell11.StylePriority.UseBorderColor = false;
            this.xrTableCell11.StylePriority.UseBorders = false;
            this.xrTableCell11.StylePriority.UsePadding = false;
            this.xrTableCell11.StylePriority.UseTextAlignment = false;
            this.xrTableCell11.Text = "SUBTOTAL";
            this.xrTableCell11.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell11.Weight = 3.55402018838413D;
            // 
            // xrTableCell4
            // 
            this.xrTableCell4.BorderColor = System.Drawing.Color.Black;
            this.xrTableCell4.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrTableCell4.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_AccountsPayableListing.Amount")});
            this.xrTableCell4.Name = "xrTableCell4";
            this.xrTableCell4.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 100F);
            this.xrTableCell4.StylePriority.UseBorderColor = false;
            this.xrTableCell4.StylePriority.UseBorders = false;
            this.xrTableCell4.StylePriority.UsePadding = false;
            this.xrTableCell4.StylePriority.UseTextAlignment = false;
            xrSummary4.FormatString = "{0:#,0.00;(#,0.00);}";
            xrSummary4.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            this.xrTableCell4.Summary = xrSummary4;
            this.xrTableCell4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell4.Weight = 1.1911229858296613D;
            // 
            // xrTableCell6
            // 
            this.xrTableCell6.BorderColor = System.Drawing.Color.Black;
            this.xrTableCell6.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrTableCell6.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_AccountsPayableListing.Balance")});
            this.xrTableCell6.Name = "xrTableCell6";
            this.xrTableCell6.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 100F);
            this.xrTableCell6.SnapLineMargin = new DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 100F);
            this.xrTableCell6.StylePriority.UseBorderColor = false;
            this.xrTableCell6.StylePriority.UseBorders = false;
            this.xrTableCell6.StylePriority.UsePadding = false;
            this.xrTableCell6.StylePriority.UseTextAlignment = false;
            xrSummary5.FormatString = "{0:#,0.00;(#,0.00);}";
            xrSummary5.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            this.xrTableCell6.Summary = xrSummary5;
            this.xrTableCell6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell6.Weight = 1.1911229924677258D;
            // 
            // xrTableCell12
            // 
            this.xrTableCell12.BorderColor = System.Drawing.Color.Black;
            this.xrTableCell12.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell12.Name = "xrTableCell12";
            this.xrTableCell12.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 100F);
            this.xrTableCell12.StylePriority.UseBorderColor = false;
            this.xrTableCell12.StylePriority.UseBorders = false;
            this.xrTableCell12.StylePriority.UsePadding = false;
            this.xrTableCell12.StylePriority.UseTextAlignment = false;
            this.xrTableCell12.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell12.Weight = 1.1911218372710337D;
            // 
            // ReportFooter
            // 
            this.ReportFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable2});
            this.ReportFooter.HeightF = 40F;
            this.ReportFooter.Name = "ReportFooter";
            // 
            // xrTable2
            // 
            this.xrTable2.BorderColor = System.Drawing.Color.Gainsboro;
            this.xrTable2.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTable2.Font = new System.Drawing.Font("Arial Narrow", 8F);
            this.xrTable2.LocationFloat = new DevExpress.Utils.PointFloat(50F, 0F);
            this.xrTable2.Name = "xrTable2";
            this.xrTable2.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow2});
            this.xrTable2.SizeF = new System.Drawing.SizeF(706.0833F, 25F);
            this.xrTable2.StylePriority.UseBorderColor = false;
            this.xrTable2.StylePriority.UseBorders = false;
            this.xrTable2.StylePriority.UseFont = false;
            this.xrTable2.StylePriority.UseTextAlignment = false;
            this.xrTable2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrTableRow2
            // 
            this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell17,
            this.xrTableCell1,
            this.xrTableCell3,
            this.xrTableCell18});
            this.xrTableRow2.Name = "xrTableRow2";
            this.xrTableRow2.Weight = 1D;
            // 
            // xrTableCell17
            // 
            this.xrTableCell17.BorderColor = System.Drawing.Color.Black;
            this.xrTableCell17.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell17.Name = "xrTableCell17";
            this.xrTableCell17.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.xrTableCell17.StylePriority.UseBorderColor = false;
            this.xrTableCell17.StylePriority.UseBorders = false;
            this.xrTableCell17.StylePriority.UsePadding = false;
            this.xrTableCell17.StylePriority.UseTextAlignment = false;
            this.xrTableCell17.Text = "GRANDTOTAL";
            this.xrTableCell17.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell17.Weight = 3.5540227815467791D;
            // 
            // xrTableCell1
            // 
            this.xrTableCell1.BorderColor = System.Drawing.Color.Black;
            this.xrTableCell1.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrTableCell1.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_AccountsPayableListing.Amount")});
            this.xrTableCell1.Name = "xrTableCell1";
            this.xrTableCell1.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 100F);
            this.xrTableCell1.StylePriority.UseBorderColor = false;
            this.xrTableCell1.StylePriority.UseBorders = false;
            this.xrTableCell1.StylePriority.UsePadding = false;
            this.xrTableCell1.StylePriority.UseTextAlignment = false;
            xrSummary6.FormatString = "{0:#,0.00;(#,0.00);}";
            xrSummary6.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
            this.xrTableCell1.Summary = xrSummary6;
            this.xrTableCell1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell1.Weight = 1.191123137698441D;
            // 
            // xrTableCell3
            // 
            this.xrTableCell3.BorderColor = System.Drawing.Color.Black;
            this.xrTableCell3.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrTableCell3.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_AccountsPayableListing.Balance")});
            this.xrTableCell3.Name = "xrTableCell3";
            this.xrTableCell3.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 5, 0, 0, 100F);
            this.xrTableCell3.StylePriority.UseBorderColor = false;
            this.xrTableCell3.StylePriority.UseBorders = false;
            this.xrTableCell3.StylePriority.UsePadding = false;
            this.xrTableCell3.StylePriority.UseTextAlignment = false;
            xrSummary7.FormatString = "{0:#,0.00;(#,0.00);}";
            xrSummary7.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
            this.xrTableCell3.Summary = xrSummary7;
            this.xrTableCell3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCell3.Weight = 1.1911231463111065D;
            // 
            // xrTableCell18
            // 
            this.xrTableCell18.BorderColor = System.Drawing.Color.Black;
            this.xrTableCell18.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell18.Name = "xrTableCell18";
            this.xrTableCell18.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 100F);
            this.xrTableCell18.StylePriority.UseBorderColor = false;
            this.xrTableCell18.StylePriority.UseBorders = false;
            this.xrTableCell18.StylePriority.UsePadding = false;
            this.xrTableCell18.StylePriority.UseTextAlignment = false;
            this.xrTableCell18.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell18.Weight = 1.1911221643935794D;
            // 
            // R_APListing
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.ReportHeader,
            this.PageHeader,
            this.GroupHeader1,
            this.GroupFooter1,
            this.ReportFooter});
            this.ComponentStorage.AddRange(new System.ComponentModel.IComponent[] {
            this.sqlDataSource1});
            this.DataMember = "sp_report_AccountsPayableListing";
            this.DataSource = this.sqlDataSource1;
            this.FormattingRuleSheet.AddRange(new DevExpress.XtraReports.UI.FormattingRule[] {
            this.formattingRule1});
            this.Margins = new System.Drawing.Printing.Margins(20, 20, 40, 40);
            this.Parameters.AddRange(new DevExpress.XtraReports.Parameters.Parameter[] {
            this.DocDateFrom_Actual,
            this.DocDateFrom,
            this.DocDateTo_Actual,
            this.DocDateTo,
            this.DueDateFrom_Actual,
            this.DueDateFrom,
            this.DueDateTo_Actual,
            this.DueDateTo,
            this.Supplier,
            this.TransType,
            this.ReportInfo,
            this.ShowDetail,
            this.Summary});
            this.Scripts.OnAfterPrint = "R_ListOfCheckIssued_AfterPrint";
            this.Scripts.OnParametersRequestBeforeShow = "R_ListOfCheckIssued_ParametersRequestBeforeShow";
            this.Scripts.OnParametersRequestSubmit = "R_ListofCheckReceived_ParametersRequestSubmit";
            this.Version = "15.1";
            this.ParametersRequestBeforeShow += new System.EventHandler<DevExpress.XtraReports.Parameters.ParametersRequestEventArgs>(this.R_ListOfCheckIssued_ParametersRequestBeforeShow);
            this.DataSourceDemanded += new System.EventHandler<System.EventArgs>(this.R_APListing_DataSourceDemanded);
            ((System.ComponentModel.ISupportInitialize)(this.Detail1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Detail2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).EndInit();
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
        private DevExpress.XtraReports.UI.XRLabel xrLabel15;
        private FormattingRule formattingRule1;
        private Parameter Supplier;
        private Parameter DocDateFrom;
        private Parameter DocDateTo;
        private Parameter DueDateFrom;
        private Parameter DueDateTo;
        private XRLabel xrLabel2;
        private XRPageInfo xrPageInfo2;
        private XRPageInfo xrPageInfo3;
        private XRLabel xrLabel3;
        private XRLabel xrLabel4;
        private XRLabel xrLabel5;
        private Parameter DocDateFrom_Actual;
        private Parameter DocDateTo_Actual;
        private Parameter DueDateFrom_Actual;
        private Parameter DueDateTo_Actual;
        private PageHeaderBand PageHeader;
        private XRTable xrTable4;
        private XRTableRow xrTableRow3;
        private XRTableCell xrTableCell8;
        private XRTableCell xrTableCell9;
        private XRTableCell xrTableCell10;
        private XRTableCell xrTableCell13;
        private XRTableCell xrTableCell14;
        private XRTableCell xrTableCell16;
        private Parameter TransType;
        private Parameter ReportInfo;
        private Parameter ShowDetail;
        private Parameter Summary;
        private XRTable Detail1;
        private XRTableRow xrTableRow5;
        private XRTableCell Detail1Col2;
        private XRTableCell Detail1Col3;
        private XRTableCell Detail1Col4;
        private XRTableCell Detail1Col5;
        private XRTableCell Detail1Col6;
        private GroupHeaderBand GroupHeader1;
        private GroupFooterBand GroupFooter1;
        private XRTable xrTable1;
        private XRTableRow xrTableRow1;
        private XRTableCell xrTableCell4;
        private XRTableCell xrTableCell6;
        private ReportFooterBand ReportFooter;
        private XRTable xrTable2;
        private XRTableRow xrTableRow2;
        private XRTableCell xrTableCell1;
        private XRTableCell xrTableCell3;
        private XRTable xrTable5;
        private XRTableRow xrTableRow4;
        private XRTableCell xrTableCell5;
        private XRTableCell xrTableCell15;
        private XRTableCell xrTableCell11;
        private XRTableCell xrTableCell12;
        private XRTableCell xrTableCell17;
        private XRTableCell xrTableCell18;
        private XRTable Detail2;
        private XRTableRow xrTableRow6;
        private XRTableCell Detail2Col1;
        private XRTableCell Detail2Col2;
        private XRTableCell Detail1Col1;

    }
}
