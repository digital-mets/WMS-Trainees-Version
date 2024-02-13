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
    //-- =============================================
    //-- Created By:		Erastian George Sy
    //-- Creation Date:     06/24/2015
    //-- Description:	    Form Countsheet (Subsi/Setup)
    // -- 2016-01-07    RA  0px ColorCode,ClassCode,SizeCode
    // --                   Rearange Columns Grid and SQL Command
    //-- =============================================
    #endregion
    public partial class frmCountSheet : System.Web.UI.Page
    {
        Entity.CountSheetSubsi _Subsi = new CountSheetSubsi();
        Entity.CountSheetSetup _Setup = new CountSheetSetup();

        public static decimal UsedQty;
        public static string docnum = "";
        public static string transtype = "";
        public static string linenum = "";
        private static string Conn = "";
        protected void Page_Load(object sender, EventArgs e)
        {
	    Gears.UseConnectionString(Session["ConnString"].ToString());
        Conn = Session["ConnString"].ToString();
            if (Request.QueryString["entry"] == "V")
            {
                updateBtn.Visible = false;
                genbtn.Visible = false;
            }
            if (!Convert.IsDBNull(Request.QueryString["status"]))
            {
                if (Request.QueryString["status"] == "S")
                {
                    updateBtn.Visible = false;
                    genbtn.Visible = false;
                }
            }

            docnum = Request.QueryString[1].ToString();
            transtype = Request.QueryString[2].ToString();
            linenum = Request.QueryString[3].ToString();
            string Status = "";

            string identifier = "";
            txtDocNumber.Text = Request.QueryString[1].ToString();
            txtTransType.Text = Request.QueryString[2].ToString();

            DataTable Subsiorsetup = Gears.RetriveData2("Select Code from it.systemsettings where value like '%" + Request.QueryString[2].ToString() + "%'", Session["ConnString"].ToString());
            foreach (DataRow dt in Subsiorsetup.Rows)
            {
                identifier = dt["Code"].ToString();
            }
            if (identifier == "CSUBSI")
            {
                if (linenum == "null")
                {
                    countsheetsubsi2.Visible = true;
                }
                else
                {
                    countsheetsubsi.Visible = true;
                }
                gridExport.GridViewID = "countsheetsubsi2";
            }
            else if (identifier == "CSETUP")
            {
                countsheetsetup.Visible = true;
                gridExport.GridViewID = "countsheetsetup";
            }

            switch (txtTransType.Text)
            {
                case "WMSOUT":
                    countsheetheader.SelectCommand = "SELECT DocNumber,LineNumber,A.ItemCode,FullDesc as Description,BulkQty as Qty,BulkUnit,PicklistQty as Kilos,Unit "
                                                        + "FROM wms.OutboundDetail A "
                                                        + "INNER JOIN Masterfile.Item B "
                                                        + "ON A.ItemCode = B.ItemCode "
                                                        + "WHERE ISNULL(IsInactive,0)=0 AND docnumber = '" + Request.QueryString[1].ToString() + "' and LineNumber = '" + Request.QueryString[3].ToString() + "'";
                    _Subsi.GetTableName("wms.Outbounddetail", "PicklistQty", Session["ConnString"].ToString());
                    break;

                case "WMSPUT":
                case "WMSINB":
                    countsheetheader.SelectCommand = "SELECT DocNumber,LineNumber,A.ItemCode,FullDesc as Description,BulkQty as Qty,BulkUnit,ReceivedQty as Kilos,Unit,PalletID "
                                                        + ",BatchNumber,ManufacturingDate,ExpiryDate ,ToLocation,RRDocDate "
                                                        + " FROM wms.inbounddetail A "
                                                        + " INNER JOIN Masterfile.Item B "
                                                        + " ON A.ItemCode = B.ItemCode "
                                                        + " WHERE ISNULL(IsInactive,0)=0 AND docnumber = '" + Request.QueryString[1].ToString() + "' and LineNumber = '" + Request.QueryString[3].ToString() + "'";
                     _Setup.GetTableName("wms.inbounddetail", "ReceivedQty", Session["ConnString"].ToString());
                    break;

                case "WMSPICK":
                    countsheetheader.SelectCommand = "SELECT DocNumber,LineNumber,A.ItemCode,FullDesc as Description,BulkQty as Qty,BulkUnit,Qty as Kilos,Unit,PalletID "
                                                         + ",Location,ToLocation,BatchNo,Mkfgdate,ExpiryDate,RRDocDate "
                                                         + "FROM wms.PicklistDetail A "
                                                         + "INNER JOIN Masterfile.Item B "
                                                         + "ON A.ItemCode = B.ItemCode "
                                                         + "WHERE ISNULL(IsInactive,0)=0 AND docnumber = '" + Request.QueryString[1].ToString() + "' and LineNumber = '" + Request.QueryString[3].ToString() + "'";
                    _Subsi.GetTableName("wms.picklistdetail", "Qty", Session["ConnString"].ToString());
                    break;

                case "WMSREL":
                    countsheetheader.SelectCommand = "SELECT * from wms.ItemRelocationDetail where docnumber = '" + Request.QueryString[1].ToString() + "' and LineNumber = '" + Request.QueryString[3].ToString() + "'";
                    _Subsi.GetTableName("wms.ItemRelocationDetail", "Qty", Session["ConnString"].ToString());
                    break;

                case "WMSRSV":
                    countsheetheader.SelectCommand = "SELECT * from wms.ItemRelocationDetail where docnumber = '" + Request.QueryString[1].ToString() + "' and LineNumber = '" + Request.QueryString[3].ToString() + "'";
                    _Subsi.GetTableName("wms.ItemRelocationDetail", "Qty", Session["ConnString"].ToString());
                    break;

                case "WMSPHC":
                    countsheetheader.SelectCommand = "SELECT * from wms.PhysicalCountDetail where docnumber = '" + Request.QueryString[1].ToString() + "' and LineNumber = '" + Request.QueryString[3].ToString() + "'";
                    _Subsi.GetTableName("wms.PhysicalCountDetail", "ActualQty", Session["ConnString"].ToString());
                    break;

                case "WMSIA":
                    countsheetheader.SelectCommand = "SELECT * from wms.ItemAdjustmentDetail where docnumber = '" + Request.QueryString[1].ToString() + "' and LineNumber = '" + Request.QueryString[3].ToString() + "'";
                    _Subsi.GetTableName("wms.ItemAdjustmentDetail", "Qty", Session["ConnString"].ToString());
                    break;

                case "SLSSRN":
                    countsheetheader.SelectCommand = "SELECT * from Sales.SalesReturnDetail where docnumber = '" + Request.QueryString[1].ToString() + "' and LineNumber = '" + Request.QueryString[3].ToString() + "'";
                    _Subsi.GetTableName("Sales.SalesReturnDetail", "ReturnedQty", Session["ConnString"].ToString());
                    break;

                case "SLSDRC":
                    countsheetheader.SelectCommand = "SELECT * from Sales.DeliveryReceiptDetail where docnumber = '" + Request.QueryString[1].ToString() + "' and LineNumber = '" + Request.QueryString[3].ToString() + "'";
                    _Subsi.GetTableName("Sales.DeliveryReceiptDetail", "DeliveredQty", Session["ConnString"].ToString());
                    break;
            }

            DataTable getPlant = Gears.RetriveData2("Select Plant from wms.inbound where DocNumber = '" + Request.QueryString["docnumber"].ToString() + "'", Session["ConnString"].ToString());
            foreach (DataRow dtrow in getPlant.Rows)
            {
                locationsql.SelectParameters["Plant"].DefaultValue = dtrow["Plant"].ToString();
            }

            if (Request.QueryString["type"] != null)
            {
                if (Request.QueryString["type"] == "Putaway")
                {
                    updateBtn.Visible = false;
                }
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
            if (e.ButtonType == ColumnCommandButtonType.Cancel || e.ButtonType == ColumnCommandButtonType.Update)
                e.Visible = false;
        }

        protected void cp_Callback(object sender, CallbackEventArgsBase e)
        {
            switch (e.Parameter)
            {

                case "Update":
                    if (countsheetsubsi.Visible == true)
                    {
                        countsheetsubsi.UpdateEdit();
                    }
                    else
                    {
                        countsheetsetup.UpdateEdit();
                    }
                    cp.JSProperties["cp_message"] = "Successfully updated!";
                    break;

                case "Generate":
                    if (txtTransType.Text == "WMSINB")
                    {
                        if (!String.IsNullOrEmpty(txtMfgDate.Text))
                        {
                            DataTable getdocdate = Gears.RetriveData2("Select docdate from wms.inbound where docnumber = '" + docnum + "'", Session["ConnString"].ToString());
                            string docdate = "";
                            foreach (DataRow dtRow in getdocdate.Rows)
                            {
                                docdate = dtRow[0].ToString();
                            }
                            if (Convert.ToDateTime(txtMfgDate.Text) > Convert.ToDateTime(docdate))
                            {
                                cp.JSProperties["cp_error"] = "Manufacturing date must not be after the document date!";
                            }
                            else
                            {
                                _Setup.Generate(docnum, linenum, Convert.ToInt32(txtFrom.Text), Convert.ToInt32(txtTo.Text), txtPallet.Text, txtExpDate.Text, txtMfgDate.Text, txtQty.Text, txtTransType.Text);
                                cp.JSProperties["cp_gensuccess"] = true;
                            }
                        }
                        else
                        {
                            _Setup.Generate(docnum, linenum, Convert.ToInt32(txtFrom.Text), Convert.ToInt32(txtTo.Text), txtPallet.Text, txtExpDate.Text, txtMfgDate.Text, txtQty.Text, txtTransType.Text);
                            cp.JSProperties["cp_gensuccess"] = true;
                        }
                    }
                    else
                    {
                        if (countsheetsetup.Visible == true)
                        {
                            _Setup.Generate(docnum, linenum, Convert.ToInt32(txtFrom.Text), Convert.ToInt32(txtTo.Text), txtPallet.Text, txtExpDate.Text, txtMfgDate.Text, txtQty.Text, txtTransType.Text); 
                            cp.JSProperties["cp_gensuccess"] = true;
                        }
                        else
                        {
                            _Subsi.Generate(docnum, linenum, Convert.ToInt32(txtFrom.Text), Convert.ToInt32(txtTo.Text), txtPallet.Text, txtExpDate.Text, txtMfgDate.Text, txtQty.Text, txtTransType.Text);
                            cp.JSProperties["cp_gensuccess"] = true;
                        }
                    }

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

            if (Request.QueryString["type"] != null)
            {
                if (Request.QueryString["type"] == "Putaway")
                {
                    if ((e.Column.FieldName) == "PalletID" || (e.Column.FieldName) == "Location" || (e.Column.FieldName) == "OriginalBulkQty"
                        || (e.Column.FieldName) == "OriginalBaseQty" || (e.Column.FieldName) == "ExpirationDate"
                        || (e.Column.FieldName) == "MfgDate" || (e.Column.FieldName) == "RRdate")
                    {
                        e.Editor.ReadOnly = true;
                    }
                }
                else
                {
                    if ((e.Column.FieldName) == "PalletID" || (e.Column.FieldName) == "OriginalBulkQty"
                        || (e.Column.FieldName) == "OriginalBaseQty" || (e.Column.FieldName) == "ExpirationDate"
                        || (e.Column.FieldName) == "MfgDate" || (e.Column.FieldName) == "RRdate")
                    {
                        e.Editor.ReadOnly = true;
                        e.Editor.Width = 0;
                    }
                }
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
            //if (Request.QueryString["transtype"] != "WMSPICK" && Request.QueryString["transtype"] != "WMSIA")
            //{
            //    if ((e.Column.FieldName) == "PalletID" || (e.Column.FieldName) == "Location" || (e.Column.FieldName) == "VarianceQty"
            //        || (e.Column.FieldName) == "UsedQty" || (e.Column.FieldName) == "ExpirationDate" || (e.Column.FieldName) == "RRdate"
            //        || (e.Column.FieldName) == "MfgDate")
            //    {
            //        e.Editor.ReadOnly = true;
            //    }
            //}
            else if (Request.QueryString["transtype"] == "WMSIA")
            {

                if ((e.Column.FieldName) == "PalletID" || (e.Column.FieldName) == "ToPalletID" || (e.Column.FieldName) == "Location" || (e.Column.FieldName) == "VarianceQty"
                    || (e.Column.FieldName) == "SystemQty" || (e.Column.FieldName) == "ExpirationDate" || (e.Column.FieldName) == "MfgDate" || (e.Column.FieldName) == "RRDate")
                {
                    e.Editor.ReadOnly = true;
                }
            }
            else if (Request.QueryString["transtype"] == "WMSPHC")
            {

                if ((e.Column.FieldName) == "PalletID" || (e.Column.FieldName) == "ToPalletID" || (e.Column.FieldName) == "DocBulkQty" || (e.Column.FieldName) == "Location" || (e.Column.FieldName) == "VarianceQty"
                    || (e.Column.FieldName) == "SystemQty" || (e.Column.FieldName) == "RRdate" || (e.Column.FieldName) == "ToLoc")
                {
                    e.Editor.ReadOnly = true;
                }
            }
            else if (Request.QueryString["transtype"] == "WMSREL" || Request.QueryString["transtype"] == "WMSRSV")
            {

                if ((e.Column.FieldName) == "PalletID" || (e.Column.FieldName) == "Location" || (e.Column.FieldName) == "VarianceQty"
                    || (e.Column.FieldName) == "SystemQty" || (e.Column.FieldName) == "RRdate" || (e.Column.FieldName) == "ToLoc")
                {
                    e.Editor.ReadOnly = true;
                }
            }
            else if (Request.QueryString["transtype"] == "WMSPICK")
            {
                if (((e.Column.FieldName) == "VarianceQty"   || (e.Column.FieldName) == "SystemQty" ))
                {
                    e.Editor.ReadOnly = true;
                    
                }
                if (Request.QueryString["type"] == "Putaway")
                {
                    if ((e.Column.FieldName) == "PalletID" || (e.Column.FieldName) == "ToPalletID"
                        || (e.Column.FieldName) == "ExpirationDate"
                        || (e.Column.FieldName) == "MfgDate")
                    {
                        e.Editor.ReadOnly = true;
                    }
                }
            }
            else if (Request.QueryString["transtype"] == "WMSOUT")
            {
                if ((e.Column.FieldName) == "PalletID" || (e.Column.FieldName) == "ToPalletID" || (e.Column.FieldName) == "DocBulkQty" || (e.Column.FieldName) == "Location" || (e.Column.FieldName) == "VarianceQty"
                    || (e.Column.FieldName) == "ExpirationDate" || (e.Column.FieldName) == "RRdate"
                    || (e.Column.FieldName) == "MfgDate" | (e.Column.FieldName) == "UsedQty")
                {
                    e.Editor.ReadOnly = true;
                }
            }
            else
            {
                if ((e.Column.FieldName) == "PalletID" || (e.Column.FieldName) == "ToPalletID" || (e.Column.FieldName) == "DocBulkQty" || (e.Column.FieldName) == "Location" || (e.Column.FieldName) == "VarianceQty"
                    || (e.Column.FieldName) == "ExpirationDate" || (e.Column.FieldName) == "RRdate"
                    || (e.Column.FieldName) == "MfgDate")
                {
                    e.Editor.ReadOnly = true;
                }
            }
        }

        protected void glLocation_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["type"] != null)
            {
                if (Request.QueryString["type"] == "PutawayM")
                {
                    ASPxGridLookup look = sender as ASPxGridLookup;
                    look.Enabled = true;
                }
            }
        }

        protected void Connection_Init(object sender, EventArgs e)
        {
            countsheetheader.ConnectionString = Session["ConnString"].ToString();
            locationsql.ConnectionString = Session["ConnString"].ToString();
        }

        protected void countsheetsubsi_DataBound(object sender, EventArgs e)
        {
            if (linenum == "null") {
                gridExport.WriteXlsxToResponse(new XlsxExportOptionsEx() { ExportType = ExportType.WYSIWYG }); 
            }
        }

    }
}