using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using GearsLibrary;
using DevExpress.Web;
using System.Web.UI.WebControls;
using DevExpress.Export;
using DevExpress.XtraPrinting;
using Entity;

namespace GWL.WMS
{
    #region Changes
    //-- ====================================================
    //-- Created By:		Terence Leonard A. Vergara
    //-- Creation Date:     05/13/2016
    //-- Description:	    Form TRR Countsheet (Subsi/Setup)
    //-- ====================================================
    #endregion
    public partial class frmTRRSetup : System.Web.UI.Page
    {
        Entity.TRRSubsi _Subsi = new TRRSubsi();
        Entity.TRRSetup _Setup = new TRRSetup();

        //public static decimal UsedQty;
        public static string docnum = "";
        public static string transtype = "";
        public static string linenum = "";
        //public static string rrdocnum = "";
        private static string Conn = "";
        protected void Page_Load(object sender, EventArgs e)
        {
	        Gears.UseConnectionString(Session["ConnString"].ToString());
            Conn = Session["ConnString"].ToString();
            if (Request.QueryString["entry"] == "V")
            {
                updateBtn.Visible = false;
            }

            docnum = Request.QueryString[1].ToString();
            transtype = Request.QueryString[2].ToString();
            linenum = Request.QueryString[3].ToString();
            //rrdocnum = Request.QueryString[4].ToString();
            string Status = "";

            string identifier = "";
            txtDocNumber.Text = Request.QueryString[1].ToString();
            string ttype = Request.QueryString[2].ToString() == "PRCWOTA" || Request.QueryString[2].ToString() == "PRCWOTB" ? "PRCWOT" : Request.QueryString[2].ToString();
            txtTransType.Text = ttype;

            DataTable Subsiorsetup = Gears.RetriveData2("SELECT Code FROM IT.SystemSettings WHERE Value like '%" + ttype + "%'", Session["ConnString"].ToString());
            foreach (DataRow dt in Subsiorsetup.Rows)
            {
                identifier = dt["Code"].ToString();
            }
            if (identifier == "TRRCSUBSI")
            {
                countsheetsubsi.ClientVisible = true;
            }
            else if (identifier == "TRRCSETUP")
            {
                countsheetsetup.ClientVisible = true;
                gridExport.GridViewID = "countsheetsetup";
            }

            switch (Request.QueryString[2].ToString())
            {
                case "PRCRCR":
                    countsheetheader.SelectCommand = "SELECT ISNULL(ItemCode,'') AS ItemCode, ISNULL(ColorCode,'') AS ColorCode, ISNULL(ClassCode,'') AS ClassCode, ISNULL(SizeCode,'') AS SizeCode, ISNULL(ReceivedQty,0) AS ReceivedQty, ISNULL(BulkQty,0) AS BulkQty, ISNULL(Unit,'') AS Unit FROM Procurement.ReceivingReportDetailPO WHERE DocNumber = '" + Request.QueryString[1].ToString() + "' and LineNumber = '" + Request.QueryString[3].ToString() + "'";
                    break;
                case "PRCWOTB":
                    countsheetheader.SelectCommand = "SELECT A.DocNumber, B.LineNumber, ISNULL(C.ItemCode,'') AS ItemCode, ISNULL(C.ColorCode,'') AS ColorCode, "
                                                   + "ISNULL(B.ClassCode,'') AS ClassCode, ISNULL(B.SizeCodes,'') AS SizeCode, "
                                                   + "ISNULL(B.Quantity,0) AS ReceivedQty, ISNULL(B.BulkQuantity,0) AS BulkQty "
                                                   + "INTO #B FROM Procurement.WIPOut A "
                                                   + "INNER JOIN Procurement.WOClassBreakdown B "
                                                   + "ON A.DocNumber = B.DocNumber "
                                                   + "INNER JOIN Procurement.ServiceOrder C "
                                                   + "ON A.ServiceOrder = C.DocNumber "

                                                   + "SELECT A.ItemCode, A.ColorCode, A.ClassCode, A.SizeCode, "
                                                       + "ReceivedQty, BulkQty, B.UnitBase FROM "
                                                   + "#B A INNER JOIN Masterfile.Item B "
                                                   + "ON A.ItemCode = B.ItemCode "
                                                   + "WHERE DocNumber = '" + Request.QueryString[1].ToString() + "' and LineNumber = '" + Request.QueryString[3].ToString() + "' DROP TABLE #B";
                    break;
                case "PRCWOTA":
                    countsheetheader.SelectCommand = "SELECT A.DocNumber, B.LineNumber, ISNULL(C.ItemCode,'') AS ItemCode, ISNULL(C.ColorCode,'') AS ColorCode, "
                                                   + "ISNULL(B.ClassCodes,'') AS ClassCode, ISNULL(B.SizeCode,'') AS SizeCode, "
                                                   + "ISNULL(B.Qty,0) AS ReceivedQty, ISNULL(B.BulkQty,0) AS BulkQty "
                                                   + "INTO #B FROM Procurement.WIPOut A "
                                                   + "INNER JOIN Procurement.WOSizeBreakdown B "
                                                   + "ON A.DocNumber = B.DocNumber "
                                                   + "INNER JOIN Procurement.ServiceOrder C "
                                                   + "ON A.ServiceOrder = C.DocNumber "

                                                   + "SELECT A.ItemCode, A.ColorCode, A.ClassCode, A.SizeCode, "
                                                       + "ReceivedQty, BulkQty, B.UnitBase FROM "
                                                   + "#B A INNER JOIN Masterfile.Item B "
                                                   + "ON A.ItemCode = B.ItemCode "
                                                   + "WHERE DocNumber = '" + Request.QueryString[1].ToString() + "' and LineNumber = '" + Request.QueryString[3].ToString() + "' DROP TABLE #B";
                    break;
                case "INVSAR":
                case "INVJOR":
                case "INVOSR":
                case "INVCOR":
                    countsheetheader.SelectCommand = "SELECT ISNULL(ItemCode,'') AS ItemCode, ISNULL(ColorCode,'') AS ColorCode, ISNULL(ClassCode,'') AS ClassCode, ISNULL(SizeCode,'') AS SizeCode, ISNULL(RequestedQty,0) AS TotalRequired, ISNULL(RequestedBulkQty,0) AS BulkQty, ISNULL(Unit,'') AS Unit FROM Inventory.RequestDetail WHERE DocNumber = '" + Request.QueryString[1].ToString() + "' and LineNumber = '" + Request.QueryString[3].ToString() + "'";
                    _Subsi.GetTableName("Inventory.RequestDetail", "RequestedQty", Session["ConnString"].ToString());
                    break;
                case "INVSAI":
                case "INVJOI":
                case "INVOSI":
                case "INVCOI":
                    countsheetheader.SelectCommand = "SELECT ISNULL(ItemCode,'') AS ItemCode, ISNULL(ColorCode,'') AS ColorCode, ISNULL(ClassCode,'') AS ClassCode, ISNULL(SizeCode,'') AS SizeCode, ISNULL(IssuedQty,0) AS TotalRequired, ISNULL(IssuedBulkQty,0) AS BulkQty, ISNULL(Unit,'') AS Unit FROM Inventory.IssuanceDetail WHERE DocNumber = '" + Request.QueryString[1].ToString() + "' and LineNumber = '" + Request.QueryString[3].ToString() + "'";
                    _Subsi.GetTableName("Inventory.IssuanceDetail", "IssuedQty", Session["ConnString"].ToString());
                    break;
                case "INVJON":
                case "INVSAN":
                    countsheetheader.SelectCommand = "SELECT ISNULL(ItemCode,'') AS ItemCode, ISNULL(ColorCode,'') AS ColorCode, ISNULL(ClassCode,'') AS ClassCode, ISNULL(SizeCode,'') AS SizeCode, ISNULL(ReturnedQty,0) AS TotalRequired, ISNULL(ReturnedBulkQty,0) AS BulkQty, ISNULL(Unit,'') AS Unit FROM Inventory.ReturnDetail WHERE DocNumber = '" + Request.QueryString[1].ToString() + "' and LineNumber = '" + Request.QueryString[3].ToString() + "'";
                    _Subsi.GetTableName("Inventory.ReturnDetail", "ReturnedQty", Session["ConnString"].ToString());
                    break;
                case "INVSTR":
                    countsheetheader.SelectCommand = "SELECT ISNULL(ItemCode,'') AS ItemCode, ISNULL(ColorCode,'') AS ColorCode, ISNULL(ClassCode,'') AS ClassCode, ISNULL(SizeCode,'') AS SizeCode, CASE ISNULL(Type,'D') WHEN 'D' THEN ISNULL(DispatchQty,0.00) ELSE ISNULL(ReceivedQty,0.00) END AS TotalRequired, CASE ISNULL(Type,'D') WHEN 'D' THEN ISNULL(DispatchBulkQty,0.00) ELSE ISNULL(ReceivedBulkQty,0.00) END AS BulkQty, ISNULL(Unit,'') AS Unit FROM Inventory.StockTransferDetail A INNER JOIN Inventory.StockTransfer B ON A.DocNumber = B.DocNumber WHERE A.DocNumber = '" + Request.QueryString[1].ToString() + "' and LineNumber = '" + Request.QueryString[3].ToString() + "'";
                    _Subsi.GetTableName("Inventory.StockTransferDetail", "", Session["ConnString"].ToString());
                    break;
                case "INVMAN":
                    countsheetheader.SelectCommand = "SELECT ISNULL(ItemCode,'') AS ItemCode, ISNULL(ColorCode,'') AS ColorCode, ISNULL(ClassCode,'') AS ClassCode, ISNULL(SizeCode,'') AS SizeCode, ISNULL(Qty,0) AS TotalRequired, 0.00 AS BulkQty, ISNULL(Unit,'') AS Unit FROM Inventory.ManualAllocationDetail WHERE DocNumber = '" + Request.QueryString[1].ToString() + "' and LineNumber = '" + Request.QueryString[3].ToString() + "'";
                    _Subsi.GetTableName("Inventory.ManualAllocationDetail", "Qty", Session["ConnString"].ToString());
                    break;
                case "INVADJT":
                    countsheetheader.SelectCommand = "SELECT ISNULL(ItemCode,'') AS ItemCode, ISNULL(ColorCode,'') AS ColorCode, ISNULL(ClassCode,'') AS ClassCode, ISNULL(SizeCode,'') AS SizeCode, ISNULL(AdjustedQty,0) AS TotalRequired, ISNULL(AdjustedBulkQty,0) AS BulkQty, ISNULL(Unit,'') AS Unit FROM Inventory.ItemAdjustmentDetail WHERE DocNumber = '" + Request.QueryString[1].ToString() + "' and LineNumber = '" + Request.QueryString[3].ToString() + "'";
                    _Subsi.GetTableName("Inventory.ItemAdjustmentDetail", "AdjutedQty", Session["ConnString"].ToString());
                    break;
                case "SLSDRC":
                    countsheetheader.SelectCommand = "SELECT ISNULL(ItemCode,'') AS ItemCode, ISNULL(ColorCode,'') AS ColorCode, ISNULL(ClassCode,'') AS ClassCode, ISNULL(SizeCode,'') AS SizeCode, ISNULL(DeliveredQty,0) AS TotalRequired, ISNULL(BulkQty,0) AS BulkQty, ISNULL(Unit,'') AS Unit FROM Sales.DeliveryReceiptDetail WHERE DocNumber = '" + Request.QueryString[1].ToString() + "' and LineNumber = '" + Request.QueryString[3].ToString() + "'";
                    _Subsi.GetTableName("Sales.DeliveryReceiptDetail", "DeliveredQty", Session["ConnString"].ToString());
                    break;
                case "INVCNT":
                    countsheetheader.SelectCommand = "SELECT ISNULL(ItemCode,'') AS ItemCode, ISNULL(ColorCode,'') AS ColorCode, ISNULL(ClassCode,'') AS ClassCode, ISNULL(SizeCode,'') AS SizeCode, ISNULL(ActualQty,0) AS TotalRequired, ISNULL(ActualBulkQty,0) AS BulkQty, ISNULL(Unit,'') AS Unit FROM  Inventory.PhysicalCountDetail WHERE DocNumber = '" + Request.QueryString[1].ToString() + "' and LineNumber = '" + Request.QueryString[3].ToString() + "'";
                    _Subsi.GetTableName("Inventory.PhysicalCountDetail", "ActualQty", Session["ConnString"].ToString());
                    break;
                case "SLSSRN":
                    countsheetheader.SelectCommand = "SELECT ISNULL(ItemCode,'') AS ItemCode, ISNULL(ColorCode,'') AS ColorCode, ISNULL(ClassCode,'') AS ClassCode, ISNULL(SizeCode,'') AS SizeCode, ISNULL(ReturnedQty,0) AS TotalRequired, ISNULL(ReturnedBulkQty,0) AS BulkQty, ISNULL(Unit,'') AS Unit FROM Sales.SalesReturnDetail WHERE DocNumber = '" + Request.QueryString[1].ToString() + "' and LineNumber = '" + Request.QueryString[3].ToString() + "'";
                    _Subsi.GetTableName("Sales.SalesReturnDetail", "ReturnedQty", Session["ConnString"].ToString());
                    break;
                case "PRPRT":
                    countsheetheader.SelectCommand = "SELECT ISNULL(ItemCode,'') AS ItemCode, ISNULL(ColorCode,'') AS ColorCode, ISNULL(ClassCode,'') AS ClassCode, ISNULL(SizeCode,'') AS SizeCode, ISNULL(ReturnedQty,0) AS TotalRequired, ISNULL(ReturnedBulkQty,0) AS BulkQty, ISNULL(Unit,'') AS Unit FROM Procurement.PurchaseReturnDetail WHERE DocNumber = '" + Request.QueryString[1].ToString() + "' and LineNumber = '" + Request.QueryString[3].ToString() + "'";
                    _Subsi.GetTableName("Procurement.PurchaseReturnDetail", "ReturnedQty", Session["ConnString"].ToString());
                    break;
                case "ACTARM":
                    countsheetheader.SelectCommand = "SELECT ISNULL(ItemCode,'') AS ItemCode, ISNULL(ColorCode,'') AS ColorCode, ISNULL(ClassCode,'') AS ClassCode, ISNULL(SizeCode,'') AS SizeCode, ISNULL(Qty,0) AS TotalRequired, ISNULL(BulkQty,0) AS BulkQty, ISNULL(Unit,'') AS Unit FROM Accounting.ARMemoDetail WHERE DocNumber = '" + Request.QueryString[1].ToString() + "' and LineNumber = '" + Request.QueryString[3].ToString() + "'";
                    _Subsi.GetTableName("Accounting.ARMemoDetail", "Qty", Session["ConnString"].ToString());
                    break;
            }
        }

        protected void headerline_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
            grid.SettingsBehavior.AllowGroup = false;
            grid.SettingsBehavior.AllowSort = false;
            e.Editor.ReadOnly = true;
        }
        protected void gv_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
        {
            if (e.ButtonType == ColumnCommandButtonType.Cancel || e.ButtonType == ColumnCommandButtonType.Update || 
                e.ButtonType == ColumnCommandButtonType.Update || e.ButtonType == ColumnCommandButtonType.Cancel)
                e.Visible = false;
        }

        protected void cp_Callback(object sender, CallbackEventArgsBase e)
        {
            switch (e.Parameter)
            {
                case "Update":
                    if (countsheetsubsi.ClientVisible == true)
                    {
                        countsheetsubsi.UpdateEdit();
                    }
                    else
                    {
                        countsheetsetup.UpdateEdit();
                    }
                    cp.JSProperties["cp_message"] = "Successfully updated!";
                    break;
            }

        }

        protected void countsheetsetup_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        {
            if (Request.QueryString["entry"].ToString() == "V")
            {
                ASPxGridView grid = sender as ASPxGridView;
                grid.SettingsBehavior.AllowGroup = false;
                grid.SettingsBehavior.AllowSort = false;
                e.Editor.ReadOnly = true;
            }
        }

        protected void countsheetsubsi_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        {
            if (Request.QueryString["entry"].ToString() == "V")
            {
                ASPxGridView grid = sender as ASPxGridView;
                grid.SettingsBehavior.AllowGroup = false;
                grid.SettingsBehavior.AllowSort = false;
                e.Editor.ReadOnly = true;
            }
            else
            {
                if ((e.Column.FieldName) == "UsedQty" || (e.Column.FieldName) == "MfgDate" || 
                    (e.Column.FieldName) == "BatchNo" || (e.Column.FieldName) == "LotNo")
                {
                    e.Editor.ReadOnly = false;
                }
                else
                    e.Editor.ReadOnly = true;
            }
        }

        //protected void jhkhjk_BatchUpdate(object sender, ASPxDataBatchUpdateEventArgs e)
        //{
        //        e.Handled = true;
        //        DataTable source = GetSelectedVal();
        //        foreach (DataRow dtRow in source.Rows)//This is where the data will be inserted into db
        //        {
        //            _EntityDetail.ItemCode = dtRow["ItemCode"].ToString();
        //            _EntityDetail.ColorCode = dtRow["ColorCode"].ToString();
        //            _EntityDetail.ClassCode = dtRow["ClassCode"].ToString();
        //            _EntityDetail.SizeCode = dtRow["SizeCode"].ToString();
        //            _EntityDetail.PODocNumber = dtRow["PODocNumber"].ToString();
        //            _EntityDetail.ReturnedQty = Convert.ToDecimal(string.IsNullOrEmpty(dtRow["ReturnedQty"].ToString()) ? "0" : dtRow["ReturnedQty"].ToString());
        //            _EntityDetail.Unit = dtRow["Unit"].ToString();
        //            _EntityDetail.UnitCost = Convert.ToDecimal(string.IsNullOrEmpty(dtRow["UnitCost"].ToString()) ? "0" : dtRow["UnitCost"].ToString());
        //            _EntityDetail.BaseQty = Convert.ToDecimal(string.IsNullOrEmpty(dtRow["BaseQty"].ToString()) ? "0" : dtRow["BaseQty"].ToString());
        //            _EntityDetail.AverageCost = Convert.ToDecimal(string.IsNullOrEmpty(dtRow["AverageCost"].ToString()) ? "0" : dtRow["AverageCost"].ToString());
        //            _EntityDetail.StatusCode = dtRow["StatusCode"].ToString();
        //            _EntityDetail.ReturnedBulkQty = Convert.ToDecimal(string.IsNullOrEmpty(dtRow["ReturnedBulkQty"].ToString()) ? "0" : dtRow["ReturnedBulkQty"].ToString());
        //            //_EntityDetail.CountSheet = dtRow["CountSheet"].ToString();
        //            //_EntityDetail.PropertyNumber = dtRow["PropertyNumber"].ToString();
        //            _EntityDetail.IsVat = Convert.ToBoolean(Convert.IsDBNull(dtRow["IsVat"]) ? false : dtRow["IsVat"]);
        //            _EntityDetail.VatCode = dtRow["VatCode"].ToString();
        //            _EntityDetail.Field1 = dtRow["Field1"].ToString();
        //            _EntityDetail.Field2 = dtRow["Field2"].ToString();
        //            _EntityDetail.Field3 = dtRow["Field3"].ToString();
        //            _EntityDetail.Field4 = dtRow["Field4"].ToString();
        //            _EntityDetail.Field5 = dtRow["Field5"].ToString();
        //            _EntityDetail.Field6 = dtRow["Field6"].ToString();
        //            _EntityDetail.Field7 = dtRow["Field7"].ToString();
        //            _EntityDetail.Field8 = dtRow["Field8"].ToString();
        //            _EntityDetail.Field9 = dtRow["Field9"].ToString();

        //            _EntityDetail.AddPurchaseReturnDetail(_EntityDetail);
        //        }
        //    }
        //}

        protected void Connection_Init(object sender, EventArgs e)
        {
            countsheetheader.ConnectionString = Session["ConnString"].ToString();
        }

        protected void countsheetsubsi_DataBound(object sender, EventArgs e)
        {
            if (linenum == "null") {
                gridExport.WriteXlsxToResponse(new XlsxExportOptionsEx() { ExportType = ExportType.WYSIWYG }); 
            }
        }

    }
}