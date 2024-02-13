using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using GearsLibrary;
using DevExpress.Web;
using System.Web.UI.WebControls;
using DevExpress.Spreadsheet;
using DevExpress.Spreadsheet.Export;
using DevExpress.XtraEditors;
using DevExpress.Export;
using DevExpress.XtraPrinting;
using System.Reflection;
using Entity;

namespace GWL.WMS
{
    public partial class frmExport : System.Web.UI.Page
    {
        public static decimal UsedQty;
        public static string docnum = "";
        public static string transtype = "";
        public static string linenum = "";
        protected void Page_Load(object sender, EventArgs e)
        {
	        Gears.UseConnectionString(Session["ConnString"].ToString());

            txtDocNumber.Text = Request.QueryString[1].ToString();
            txtTransType.Text = Request.QueryString[2].ToString();


            switch (txtTransType.Text)
            {
                case "INVCNT":
                    sdsExport.SelectCommand = " SELECT DocNumber, LineNumber, ItemCode, ColorCode, ClassCode, SizeCode, Unit, "
                                            + " ActualQty AS Quantity, ActualBulkQty AS BulkQuantity, SystemQty, SystemBulkQty, VarianceQty, VarianceBulkQty "
                                            + " FROM Inventory.PhysicalCountDetail  WHERE DocNumber = '" + Request.QueryString[1].ToString() + "'";
                    break;
                case "WMSINB":
                    sdsExport.SelectCommand = "SELECT DocNumber,LineNumber,A.ItemCode,FullDesc as Description,BulkQty as Qty,BulkUnit,ReceivedQty as Kilos,Unit,PalletID "
                                                        + ",BatchNumber,ManufacturingDate,ExpiryDate ,ToLocation,RRDocDate,A.Field2 as ClientName "
                                                        + " FROM wms.inbounddetail A "
                                                        + " INNER JOIN Masterfile.Item B "
                                                        + " ON A.ItemCode = B.ItemCode "
                                                        + " WHERE ISNULL(IsInactive,0)=0 AND  DocNumber = '" + Request.QueryString[1].ToString() + "'";

                    break;   
                 case "WMSPICK":
                    sdsExport.SelectCommand = "SELECT DocNumber,LineNumber,A.ItemCode,FullDesc as Description,BulkQty as Qty,BulkUnit,Qty as Kilos,Unit,PalletID "
                                                        + ",Location,ToLocation,BatchNo,Mkfgdate,ExpiryDate,RRDocDate,A.Field2 as ClientName "
                                                        + "FROM wms.PicklistDetail A "
                                                        + "INNER JOIN Masterfile.Item B "
                                                        + "ON A.ItemCode = B.ItemCode "
                                                        + "WHERE ISNULL(IsInactive,0)=0 AND docnumber = '" + Request.QueryString[1].ToString() + "'";
                    break;

                case "WMSOUT":
                    sdsExport.SelectCommand = "SELECT DocNumber,LineNumber,A.ItemCode,FullDesc as Description,BulkQty as Qty,BulkUnit,PicklistQty as Kilos,Unit,A.Field2 as ClientName "
                                                      + "FROM wms.OutboundDetail A "
                                                      + "INNER JOIN Masterfile.Item B "
                                                      + "ON A.ItemCode = B.ItemCode "
                                                      + "WHERE ISNULL(IsInactive,0)=0 AND docnumber = '" + Request.QueryString[1].ToString() + "'";


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
            if (e.ButtonType == ColumnCommandButtonType.Cancel || e.ButtonType == ColumnCommandButtonType.Update)
                e.Visible = false;
        }

        protected void cp_Callback(object sender, CallbackEventArgsBase e)
        {

        }
        protected void Connection_Init(object sender, EventArgs e)
        {
            sdsExport.ConnectionString = Session["ConnString"].ToString();
        }       

        protected void genbtn_Click(object sender, EventArgs e)
        {
            if (txtTransType.Text == "INVCNT")
            {
                gridExport.FileName = "PhysicalCount_ExportedFile(" + Request.QueryString[1].ToString() + ")";
            }
            else if (txtTransType.Text == "WMSINB")
            {
                gridExport.FileName = "Inbound_ExportedFile(" + Request.QueryString[1].ToString() + ")";
            }
            else if (txtTransType.Text == "WMSPICK")
            {
                gridExport.FileName = "Picklist_ExportedFile(" + Request.QueryString[1].ToString() + ")";
            }
            else if (txtTransType.Text == "WMSOUT")
            {
                gridExport.FileName = "Outbound_ExportedFile(" + Request.QueryString[1].ToString() + ")";
            }
            switch (cmbOutput.Text)
            {
                case ".Xls":
                    gridExport.WriteXlsToResponse(new XlsExportOptionsEx { ExportType = ExportType.WYSIWYG });
                    break;
                case ".Xlsx":
                    gridExport.WriteXlsxToResponse(new XlsxExportOptionsEx { ExportType = ExportType.WYSIWYG });
                    break;
                case ".Rtf":
                    gridExport.WriteRtfToResponse();
                    break;
                case ".Csv":
                    gridExport.WriteCsvToResponse(new CsvExportOptionsEx() { ExportType = ExportType.WYSIWYG });
                    break;
                case ".Pdf":
                    gridExport.WritePdfToResponse();
                    break;
            } 
        }

    }
}