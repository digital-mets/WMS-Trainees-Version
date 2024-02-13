using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.IO;

/// <summary>
/// Summary description for P_TrimcardDetails
/// </summary>
public class P_TrimcardDetails : DevExpress.XtraReports.UI.XtraReport
{
    private DevExpress.XtraReports.UI.DetailBand Detail;
    private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
    private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
    private DevExpress.DataAccess.Sql.SqlDataSource sqlDataSource1;
    private DevExpress.XtraReports.Parameters.Parameter DocNumber;
    private DevExpress.XtraReports.Parameters.Parameter UserID;
    private DevExpress.XtraReports.Parameters.Parameter IsPrinted;
    private GroupHeaderBand GroupHeader1;
    private XRTable xrTable1;
    private XRTableRow xrTableRow1;
    private XRTableCell xrTableCell3;
    private XRTableRow xrTableRow2;
    private XRTableCell xrTableCell1;
    private XRTable xrTable2;
    private XRTableRow xrTableRow3;
    private XRTableCell xrTableCell2;
    private XRTableCell xrTableCell4;
    private XRTableCell xrTableCell5;
    private XRTableCell xrTableCell6;
    private XRTableCell xrTableCell7;
    private XRPictureBox xrPictureBox10;
    private XRPictureBox xrPictureBox9;
    private XRPictureBox xrPictureBox8;
    private XRPictureBox xrPictureBox7;
    private XRPictureBox xrPictureBox1;
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    public P_TrimcardDetails()
    {
        InitializeComponent();
        //
        // TODO: Add constructor logic here
        //
    }

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

    private void xrPictureBox1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        if (!(this.GetCurrentColumnValue("ItemImageC1").ToString() == ""))
        {
            byte[] str = Convert.FromBase64String(this.GetCurrentColumnValue("ItemImageC1") as string);
            MemoryStream ms = new MemoryStream(str);
            ms.Seek(0, SeekOrigin.Begin);
            Image bmp = new Bitmap(Bitmap.FromStream(ms, false, false));
            ((XRPictureBox)sender).Image = bmp;
            ms.Close();
        }
        
    }

    private void xrPictureBox7_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        if (!(this.GetCurrentColumnValue("ItemImageC2").ToString() == ""))
        {
            byte[] str = Convert.FromBase64String(this.GetCurrentColumnValue("ItemImageC2") as string);
            MemoryStream ms = new MemoryStream(str);
            ms.Seek(0, SeekOrigin.Begin);
            Image bmp = new Bitmap(Bitmap.FromStream(ms, false, false));
            ((XRPictureBox)sender).Image = bmp;
            ms.Close();
        }
    }

    private void xrPictureBox8_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        if (!(this.GetCurrentColumnValue("ItemImageC3").ToString() == ""))
        {
            byte[] str = Convert.FromBase64String(this.GetCurrentColumnValue("ItemImageC3") as string);
            MemoryStream ms = new MemoryStream(str);
            ms.Seek(0, SeekOrigin.Begin);
            Image bmp = new Bitmap(Bitmap.FromStream(ms, false, false));
            ((XRPictureBox)sender).Image = bmp;
            ms.Close();
        }
    }

    private void xrPictureBox9_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        if (!(this.GetCurrentColumnValue("ItemImageC4").ToString() == ""))
        {
            byte[] str = Convert.FromBase64String(this.GetCurrentColumnValue("ItemImageC4") as string);
            MemoryStream ms = new MemoryStream(str);
            ms.Seek(0, SeekOrigin.Begin);
            Image bmp = new Bitmap(Bitmap.FromStream(ms, false, false));
            ((XRPictureBox)sender).Image = bmp;
            ms.Close();
        }
        else
        {
            ((XRPictureBox)sender).Image = null;

        }
    }

    private void xrPictureBox10_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        if (!(this.GetCurrentColumnValue("ItemImageC5").ToString() == ""))
        {
            byte[] str = Convert.FromBase64String(this.GetCurrentColumnValue("ItemImageC5") as string);
            MemoryStream ms = new MemoryStream(str);
            ms.Seek(0, SeekOrigin.Begin);
            Image bmp = new Bitmap(Bitmap.FromStream(ms, false, false));
            ((XRPictureBox)sender).Image = bmp;
            ms.Close();
        }
    }

    #region Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
            DevExpress.DataAccess.Sql.StoredProcQuery storedProcQuery1 = new DevExpress.DataAccess.Sql.StoredProcQuery();
            DevExpress.DataAccess.Sql.QueryParameter queryParameter1 = new DevExpress.DataAccess.Sql.QueryParameter();
            DevExpress.DataAccess.Sql.QueryParameter queryParameter2 = new DevExpress.DataAccess.Sql.QueryParameter();
            DevExpress.DataAccess.Sql.QueryParameter queryParameter3 = new DevExpress.DataAccess.Sql.QueryParameter();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(P_TrimcardDetails));
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.xrPictureBox10 = new DevExpress.XtraReports.UI.XRPictureBox();
            this.xrPictureBox9 = new DevExpress.XtraReports.UI.XRPictureBox();
            this.xrPictureBox8 = new DevExpress.XtraReports.UI.XRPictureBox();
            this.xrPictureBox7 = new DevExpress.XtraReports.UI.XRPictureBox();
            this.xrPictureBox1 = new DevExpress.XtraReports.UI.XRPictureBox();
            this.xrTable2 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow3 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell4 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell5 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell6 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell7 = new DevExpress.XtraReports.UI.XRTableCell();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.sqlDataSource1 = new DevExpress.DataAccess.Sql.SqlDataSource();
            this.DocNumber = new DevExpress.XtraReports.Parameters.Parameter();
            this.UserID = new DevExpress.XtraReports.Parameters.Parameter();
            this.IsPrinted = new DevExpress.XtraReports.Parameters.Parameter();
            this.GroupHeader1 = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPictureBox10,
            this.xrPictureBox9,
            this.xrPictureBox8,
            this.xrPictureBox7,
            this.xrPictureBox1,
            this.xrTable2});
            this.Detail.HeightF = 188.0024F;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrPictureBox10
            // 
            this.xrPictureBox10.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrPictureBox10.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Image", null, "sp_printout_TrimcardDetail.ItemImageC5"),
            new DevExpress.XtraReports.UI.XRBinding("ImageUrl", null, "sp_printout_TrimcardDetail.ItemImage")});
            this.xrPictureBox10.LocationFloat = new DevExpress.Utils.PointFloat(637.8628F, 24.99998F);
            this.xrPictureBox10.Name = "xrPictureBox10";
            this.xrPictureBox10.Scripts.OnBeforePrint = "xrPictureBox1_BeforePrint";
            this.xrPictureBox10.SizeF = new System.Drawing.SizeF(159.4653F, 163.0024F);
            this.xrPictureBox10.Sizing = DevExpress.XtraPrinting.ImageSizeMode.StretchImage;
            this.xrPictureBox10.StylePriority.UseBorders = false;
            this.xrPictureBox10.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrPictureBox10_BeforePrint);
            // 
            // xrPictureBox9
            // 
            this.xrPictureBox9.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrPictureBox9.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Image", null, "sp_printout_TrimcardDetail.ItemImageC4"),
            new DevExpress.XtraReports.UI.XRBinding("ImageUrl", null, "sp_printout_TrimcardDetail.ItemImage")});
            this.xrPictureBox9.LocationFloat = new DevExpress.Utils.PointFloat(478.3974F, 24.99998F);
            this.xrPictureBox9.Name = "xrPictureBox9";
            this.xrPictureBox9.Scripts.OnBeforePrint = "xrPictureBox1_BeforePrint";
            this.xrPictureBox9.SizeF = new System.Drawing.SizeF(159.4653F, 163.0024F);
            this.xrPictureBox9.Sizing = DevExpress.XtraPrinting.ImageSizeMode.StretchImage;
            this.xrPictureBox9.StylePriority.UseBorders = false;
            this.xrPictureBox9.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrPictureBox9_BeforePrint);
            // 
            // xrPictureBox8
            // 
            this.xrPictureBox8.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrPictureBox8.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Image", null, "sp_printout_TrimcardDetail.ItemImageC3"),
            new DevExpress.XtraReports.UI.XRBinding("ImageUrl", null, "sp_printout_TrimcardDetail.ItemImage")});
            this.xrPictureBox8.LocationFloat = new DevExpress.Utils.PointFloat(318.932F, 24.99998F);
            this.xrPictureBox8.Name = "xrPictureBox8";
            this.xrPictureBox8.Scripts.OnBeforePrint = "xrPictureBox1_BeforePrint";
            this.xrPictureBox8.SizeF = new System.Drawing.SizeF(159.4653F, 163.0024F);
            this.xrPictureBox8.Sizing = DevExpress.XtraPrinting.ImageSizeMode.StretchImage;
            this.xrPictureBox8.StylePriority.UseBorders = false;
            this.xrPictureBox8.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrPictureBox8_BeforePrint);
            // 
            // xrPictureBox7
            // 
            this.xrPictureBox7.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrPictureBox7.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Image", null, "sp_printout_TrimcardDetail.ItemImageC2"),
            new DevExpress.XtraReports.UI.XRBinding("ImageUrl", null, "sp_printout_TrimcardDetail.ItemImage")});
            this.xrPictureBox7.LocationFloat = new DevExpress.Utils.PointFloat(159.4667F, 24.99998F);
            this.xrPictureBox7.Name = "xrPictureBox7";
            this.xrPictureBox7.Scripts.OnBeforePrint = "xrPictureBox1_BeforePrint";
            this.xrPictureBox7.SizeF = new System.Drawing.SizeF(159.4653F, 163.0024F);
            this.xrPictureBox7.Sizing = DevExpress.XtraPrinting.ImageSizeMode.StretchImage;
            this.xrPictureBox7.StylePriority.UseBorders = false;
            this.xrPictureBox7.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrPictureBox7_BeforePrint);
            // 
            // xrPictureBox1
            // 
            this.xrPictureBox1.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrPictureBox1.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("ImageUrl", null, "sp_printout_TrimcardDetail.ItemImage"),
            new DevExpress.XtraReports.UI.XRBinding("Image", null, "sp_printout_TrimcardDetail.ItemImageC1")});
            this.xrPictureBox1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 24.99997F);
            this.xrPictureBox1.Name = "xrPictureBox1";
            this.xrPictureBox1.Scripts.OnBeforePrint = "xrPictureBox1_BeforePrint";
            this.xrPictureBox1.SizeF = new System.Drawing.SizeF(159.4653F, 163.0024F);
            this.xrPictureBox1.Sizing = DevExpress.XtraPrinting.ImageSizeMode.StretchImage;
            this.xrPictureBox1.StylePriority.UseBorders = false;
            this.xrPictureBox1.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrPictureBox1_BeforePrint);
            // 
            // xrTable2
            // 
            this.xrTable2.Font = new System.Drawing.Font("Arial Narrow", 9F);
            this.xrTable2.LocationFloat = new DevExpress.Utils.PointFloat(0.6735166F, 0F);
            this.xrTable2.Name = "xrTable2";
            this.xrTable2.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow3});
            this.xrTable2.SizeF = new System.Drawing.SizeF(796.6547F, 24.99998F);
            this.xrTable2.StylePriority.UseFont = false;
            // 
            // xrTableRow3
            // 
            this.xrTableRow3.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell2,
            this.xrTableCell4,
            this.xrTableCell5,
            this.xrTableCell6,
            this.xrTableCell7});
            this.xrTableRow3.Name = "xrTableRow3";
            this.xrTableRow3.Weight = 1D;
            // 
            // xrTableCell2
            // 
            this.xrTableCell2.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell2.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_printout_TrimcardDetail.StockNumberC1")});
            this.xrTableCell2.Name = "xrTableCell2";
            this.xrTableCell2.StylePriority.UseBorders = false;
            this.xrTableCell2.StylePriority.UseTextAlignment = false;
            this.xrTableCell2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell2.Weight = 1D;
            // 
            // xrTableCell4
            // 
            this.xrTableCell4.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell4.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_printout_TrimcardDetail.StockNumberC2")});
            this.xrTableCell4.Name = "xrTableCell4";
            this.xrTableCell4.StylePriority.UseBorders = false;
            this.xrTableCell4.StylePriority.UseTextAlignment = false;
            this.xrTableCell4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell4.Weight = 1D;
            // 
            // xrTableCell5
            // 
            this.xrTableCell5.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell5.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_printout_TrimcardDetail.StockNumberC3")});
            this.xrTableCell5.Name = "xrTableCell5";
            this.xrTableCell5.StylePriority.UseBorders = false;
            this.xrTableCell5.StylePriority.UseTextAlignment = false;
            this.xrTableCell5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell5.Weight = 1D;
            // 
            // xrTableCell6
            // 
            this.xrTableCell6.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell6.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_printout_TrimcardDetail.StockNumberC4")});
            this.xrTableCell6.Name = "xrTableCell6";
            this.xrTableCell6.StylePriority.UseBorders = false;
            this.xrTableCell6.StylePriority.UseTextAlignment = false;
            this.xrTableCell6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell6.Weight = 1D;
            // 
            // xrTableCell7
            // 
            this.xrTableCell7.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell7.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_printout_TrimcardDetail.StockNumberC5")});
            this.xrTableCell7.Name = "xrTableCell7";
            this.xrTableCell7.StylePriority.UseBorders = false;
            this.xrTableCell7.StylePriority.UseTextAlignment = false;
            this.xrTableCell7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell7.Weight = 0.99578708355874046D;
            // 
            // TopMargin
            // 
            this.TopMargin.HeightF = 100F;
            this.TopMargin.Name = "TopMargin";
            this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // BottomMargin
            // 
            this.BottomMargin.HeightF = 100F;
            this.BottomMargin.Name = "BottomMargin";
            this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // sqlDataSource1
            // 
            this.sqlDataSource1.ConnectionName = "GEARS-METSITConnectionString";
            this.sqlDataSource1.Name = "sqlDataSource1";
            storedProcQuery1.Name = "sp_printout_TrimcardDetail";
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
            storedProcQuery1.StoredProcName = "sp_printout_TrimcardDetail";
            this.sqlDataSource1.Queries.AddRange(new DevExpress.DataAccess.Sql.SqlQuery[] {
            storedProcQuery1});
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
            // 
            // IsPrinted
            // 
            this.IsPrinted.Description = "IsPrinted";
            this.IsPrinted.Name = "IsPrinted";
            this.IsPrinted.Type = typeof(bool);
            this.IsPrinted.ValueInfo = "False";
            // 
            // GroupHeader1
            // 
            this.GroupHeader1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable1});
            this.GroupHeader1.GroupFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("StepCode", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});
            this.GroupHeader1.HeightF = 49.99997F;
            this.GroupHeader1.Name = "GroupHeader1";
            // 
            // xrTable1
            // 
            this.xrTable1.Font = new System.Drawing.Font("Arial Narrow", 9F);
            this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTable1.Name = "xrTable1";
            this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1,
            this.xrTableRow2});
            this.xrTable1.SizeF = new System.Drawing.SizeF(797.3281F, 49.99997F);
            this.xrTable1.StylePriority.UseFont = false;
            // 
            // xrTableRow1
            // 
            this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell3});
            this.xrTableRow1.Name = "xrTableRow1";
            this.xrTableRow1.Weight = 1D;
            // 
            // xrTableCell3
            // 
            this.xrTableCell3.BackColor = System.Drawing.Color.LightBlue;
            this.xrTableCell3.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell3.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_printout_TrimcardDetail.StepCode")});
            this.xrTableCell3.Font = new System.Drawing.Font("Arial Narrow", 10F);
            this.xrTableCell3.Name = "xrTableCell3";
            this.xrTableCell3.StylePriority.UseBackColor = false;
            this.xrTableCell3.StylePriority.UseBorders = false;
            this.xrTableCell3.StylePriority.UseFont = false;
            this.xrTableCell3.StylePriority.UseTextAlignment = false;
            this.xrTableCell3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell3.Weight = 1D;
            // 
            // xrTableRow2
            // 
            this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell1});
            this.xrTableRow2.Name = "xrTableRow2";
            this.xrTableRow2.Weight = 1D;
            // 
            // xrTableCell1
            // 
            this.xrTableCell1.BackColor = System.Drawing.Color.LightGray;
            this.xrTableCell1.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right)));
            this.xrTableCell1.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_printout_TrimcardDetail.Instruction", "INSTRUCTION: {0}")});
            this.xrTableCell1.Font = new System.Drawing.Font("Arial Narrow", 10F, System.Drawing.FontStyle.Bold);
            this.xrTableCell1.Name = "xrTableCell1";
            this.xrTableCell1.StylePriority.UseBackColor = false;
            this.xrTableCell1.StylePriority.UseBorders = false;
            this.xrTableCell1.StylePriority.UseFont = false;
            this.xrTableCell1.StylePriority.UseTextAlignment = false;
            this.xrTableCell1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTableCell1.Weight = 1D;
            // 
            // P_TrimcardDetails
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.GroupHeader1});
            this.ComponentStorage.AddRange(new System.ComponentModel.IComponent[] {
            this.sqlDataSource1});
            this.DataMember = "sp_printout_TrimcardDetail";
            this.DataSource = this.sqlDataSource1;
            this.Margins = new System.Drawing.Printing.Margins(23, 29, 100, 100);
            this.Parameters.AddRange(new DevExpress.XtraReports.Parameters.Parameter[] {
            this.DocNumber,
            this.UserID,
            this.IsPrinted});
            this.Version = "15.1";
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }

    #endregion

    

    

    

    
}
