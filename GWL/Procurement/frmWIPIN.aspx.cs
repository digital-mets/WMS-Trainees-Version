using DevExpress.Spreadsheet;
using DevExpress.Spreadsheet.Export;
using DevExpress.Web;
using DevExpress.Web.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GearsLibrary;
using Entity;
using System.Data;
using GearsTrading;
using DevExpress.Data.Filtering;
using GearsWarehouseManagement;
using GearsProcurement;

namespace GWL
{
    public partial class frmWIPIN : System.Web.UI.Page
    {
        Boolean error = false;//Boolean for grid validation
        Boolean view = false;//Boolean for view state
        Boolean check = false;//Boolean for grid validation

        Entity.WipIN _Entity = new WipIN();//Calls entity odsHeader
        Entity.WipIN.WISizeBreakDown _EntityDetail = new WipIN.WISizeBreakDown();//Call entity sdsDetail

        #region page load/entry
        protected void Page_Load(object sender, EventArgs e)
        {
            Gears.UseConnectionString(Session["ConnString"].ToString()); 
            string referer;
            try { referer = Request.ServerVariables["http_referer"]; }
            catch { referer = ""; }

            if (referer == null) { Response.Redirect("~/error.aspx"); }

            dtpDocDate.MinDate = Convert.ToDateTime(GearsCommon.GCommon.SystemSetting("BookDate", Session["ConnString"].ToString()).ToString());

            if (!IsPostBack)
            {  
                switch (Request.QueryString["entry"].ToString())
                {
                    case "N": 
                        updateBtn.Text = "Add";
                        glserviceorder.Visible = true;
                        break;
                    case "E": 
                        updateBtn.Text = "Update";
                        txtDocNumber.ReadOnly = true;
                        break;
                    case "V":
                        view = true; 
                        updateBtn.Text = "Close";
                        glcheck.ClientVisible = false;

                        break;
                    case "D":
                        view = true;
                        updateBtn.Text = "Delete";
                        break;
                }

                txtDocNumber.Value = Request.QueryString["docnumber"].ToString(); 
                _Entity.getdata(txtDocNumber.Text, Session["ConnString"].ToString()); 
                dtpDocDate.Text = Convert.ToDateTime(_Entity.DocDate.ToString()).ToShortDateString();
                txtType.Text = _Entity.Type.ToString(); 
                txtDrNumber.Text = _Entity.DRDocNumber.ToString();
                glserviceorder.Value = _Entity.ServiceOrder.ToString();
                txtWorkCenter.Text = _Entity.WorkCenter.ToString(); 
                seTotalQty.Value = _Entity.TotalQuantity.ToString();
                txtDrNumber.Text = _Entity.DRDocNumber;
                memRemarks.Text = _Entity.Remarks; 
                txtHField1.Value = _Entity.Field1.ToString();
                txtHField2.Value = _Entity.Field2.ToString();
                txtHField3.Value = _Entity.Field3.ToString();
                txtHField4.Value = _Entity.Field4.ToString();
                txtHField5.Value = _Entity.Field5.ToString();
                txtHField6.Value = _Entity.Field6.ToString();
                txtHField7.Value = _Entity.Field7.ToString();
                txtHField8.Value = _Entity.Field8.ToString();
                txtHField9.Value = _Entity.Field9.ToString(); 
                txtHAddedBy.Text = _Entity.AddedBy;
                txtHAddedDate.Text = _Entity.AddedDate;
                txtHLastEditedBy.Text = _Entity.LastEditedBy;
                txtHLastEditedDate.Text = _Entity.LastEditedDate;
                txtHSubmittedBy.Text = _Entity.SubmittedBy;
                txtHSubmittedDate.Text = _Entity.SubmittedDate; 
                txtType.Text = "Service IN";

                if (Request.QueryString["entry"].ToString() == "N")
                {
                    gv1.DataSourceID = null;
                    popup.ShowOnPageLoad = false;
                    glserviceorder.ClientEnabled = true;
                    frmlayout1.FindItemOrGroupByName("ReferenceTransaction").ClientVisible = false; 
                }
                else
                { 
                    gvRef.DataSourceID = "odsReference";
                    odsReference.SelectParameters["DocNumber"].DefaultValue = txtDocNumber.Text;
                    this.gvRef.Columns["CommandString"].Width = 0; 
                    this.gvRef.Columns["RCommandString"].Width = 0;
                }
                
                if (Request.QueryString["IsWithDetail"].ToString() == "False" && Request.QueryString["entry"].ToString() != "V")
                {
                    gv1.DataSourceID = null;
                }

                DataTable dtrr1 = Gears.RetriveData2("Select DocNumber from Procurement.WIPIN where docnumber = '" + txtDocNumber.Text + "' and ISNULL(ServiceOrder,'')!='' ", Session["ConnString"].ToString());
                if (dtrr1.Rows.Count > 0 && Request.QueryString["entry"].ToString() == "N")
                {
                    updateBtn.Text = "Update";
                } 

                DataTable dtdetail = _EntityDetail.getdetail(txtDocNumber.Text, Session["ConnString"].ToString());
                if (dtdetail.Rows.Count > 0)
                { 
                    gv1.DataSourceID = "odsDetail"; 
                }
                else
                {
                    gv1.DataSource = sdsDetail;
                    if (gv1.DataSourceID != "")
                    {
                        gv1.DataSourceID = null;
                    }
                    gv1.DataBind();
                } 
            } 
        }
        #endregion

        #region Validation
        private void Validate()
        {
            GearsLibrary.Gears.GearsParameter gparam = new Gears.GearsParameter();
            gparam._DocNo = _Entity.DocNumber;
            gparam._TransType = "PRCWIN";
            gparam._Connection = Session["ConnString"].ToString();
            string strresult = GearsProcurement.GProcurement.WIPIN_Validate(gparam); 
            if (strresult != " ")
            {
                cp.JSProperties["cp_valmsg"] = strresult + "\r\n" ; 
            }
        }
        #endregion

        #region Set controls' state/behavior/etc...
        protected void TextboxLoad(object sender, EventArgs e)//Control for all textbox
        {
            ASPxTextBox text = sender as ASPxTextBox;
            text.ReadOnly = view; 
        }
        protected void LookupLoad(object sender, EventArgs e)//Control for all lookup in header
        {
            ASPxGridLookup look = sender as ASPxGridLookup;
            look.DropDownButton.Enabled = !view;
            look.ReadOnly = view; 
        } 
        protected void gv1_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)//Control for grid
        {
            //if (Request.QueryString["entry"].ToString() == "V" || Request.QueryString["entry"].ToString() == "D")
            //{
                ASPxGridView grid = sender as ASPxGridView;
                grid.SettingsBehavior.AllowGroup = false;
                grid.SettingsBehavior.AllowSort = false;
                e.Editor.ReadOnly = view;
            //}
        }
        protected void Date_Load(object sender, EventArgs e)//Control for all date editor
        {
            ASPxDateEdit date = sender as ASPxDateEdit;
            date.DropDownButton.Enabled = !view;
            date.ReadOnly = view;
        }
        protected void SpinEdit_Load(object sender, EventArgs e)//Control for all numeric entries in header
        {
            ASPxSpinEdit spinedit = sender as ASPxSpinEdit;
            spinedit.ReadOnly = view;
        }
        protected void gv_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
        {  
            if (e.ButtonType == ColumnCommandButtonType.Delete)
            {
                e.Image.IconID = "actions_cancel_16x16";
            }
            if (e.ButtonType == ColumnCommandButtonType.Edit)
            {
                e.Image.IconID = "actions_addfile_16x16";
            } 
            if (e.ButtonType == ColumnCommandButtonType.Edit ||
                e.ButtonType == ColumnCommandButtonType.Cancel ||
                e.ButtonType == ColumnCommandButtonType.Update)
                e.Visible = false;
        } 
        protected void gv1_CustomButtonInitialize(object sender, ASPxGridViewCustomButtonEventArgs e)
        {
            if (Request.QueryString["entry"] == "N")
            {
                if (e.ButtonID == "Details")
                {
                    e.Visible = DevExpress.Utils.DefaultBoolean.False;
                }
            }
        }
        #endregion 

        #region Callback functions(Inserting/Updating/Deleting/Validating)
        protected void cp_Callback(object sender, CallbackEventArgsBase e)
        {
            _Entity.Connection = Session["ConnString"].ToString(); 
            _Entity.DocNumber = txtDocNumber.Value.ToString();
            _Entity.DocDate = dtpDocDate.Text;
            _Entity.Type = txtType.Text;
            _Entity.ServiceOrder = glserviceorder.Text;
            _Entity.WorkCenter = txtWorkCenter.Text;
            _Entity.TotalQuantity = Convert.ToDecimal(Convert.IsDBNull(seTotalQty.Value) ? "0.00" : seTotalQty.Value);
            _Entity.DRDocNumber = txtDrNumber.Text;
            _Entity.Remarks = memRemarks.Text; 
            _Entity.Field1 = String.IsNullOrEmpty(txtHField1.Text) ? null : txtHField1.Text;
            _Entity.Field2 = String.IsNullOrEmpty(txtHField2.Text) ? null : txtHField2.Text;
            _Entity.Field3 = String.IsNullOrEmpty(txtHField3.Text) ? null : txtHField3.Text;
            _Entity.Field4 = String.IsNullOrEmpty(txtHField4.Text) ? null : txtHField4.Text;
            _Entity.Field5 = String.IsNullOrEmpty(txtHField5.Text) ? null : txtHField5.Text;
            _Entity.Field6 = String.IsNullOrEmpty(txtHField6.Text) ? null : txtHField6.Text;
            _Entity.Field7 = String.IsNullOrEmpty(txtHField7.Text) ? null : txtHField7.Text;
            _Entity.Field8 = String.IsNullOrEmpty(txtHField8.Text) ? null : txtHField8.Text;
            _Entity.Field9 = String.IsNullOrEmpty(txtHField9.Text) ? null : txtHField9.Text;
            _Entity.LastEditedBy = Session["userid"].ToString();
            _Entity.LastEditedDate = DateTime.Now.ToString(); 

            switch (e.Parameter)
            {
                case "Add":
                case "Update":
                    gv1.UpdateEdit(); 
                    if (error == false)
                    {
                        check = true;
                        _Entity.UpdateData(_Entity); 
                        if (Session["Datatable"] == "1")
                        {
                            gv1.DataSource = GetSelectedVal();
                            gv1.UpdateEdit();
                        }
                        else
                        {
                            gv1.DataSourceID = "odsDetail";
                            odsDetail.SelectParameters["DocNumber"].DefaultValue = txtDocNumber.Text; 
                            gv1.UpdateEdit();
                        } 
                        Validate();
                        cp.JSProperties["cp_message"] = "Successfully Updated!";
                        cp.JSProperties["cp_success"] = true;
                        cp.JSProperties["cp_close"] = true;
                        Session["Datatable"] = null;
                        gv1.DataSource = null;
                    }
                    else
                    {
                        cp.JSProperties["cp_message"] = "Please check all the fields!";
                        cp.JSProperties["cp_success"] = true;
                    }
                    break; 
                case "Delete": 
                    cp.JSProperties["cp_delete"] = true;
                    break; 
                case "ConfDelete":
                    _Entity.DeleteData(_Entity);
                    cp.JSProperties["cp_close"] = true;
                    cp.JSProperties["cp_message"] = "Successfully deleted";
                    break; 
                case "Close":
                    cp.JSProperties["cp_close"] = true;
                    gv1.DataSource = null;
                    break; 
                case "Generate":
                    GetSelectedVal(); 
                    OtherDetail();
                    cp.JSProperties["cp_generated"] = true; 
                    break;  
            }
        } 

        protected void gv1_BatchUpdate(object sender, ASPxDataBatchUpdateEventArgs e)
        {
            if ((error == true || error == false) && check == false) 
            {
                e.Handled = true;
                e.UpdateValues.Clear();
            }

            if (Session["Datatable"] == "1" && check == true)
             {
                e.Handled = true;
                DataTable source = GetSelectedVal();

                foreach (ASPxDataUpdateValues values in e.UpdateValues)
                {
                    object[] keys = { values.Keys["DocNumber"], values.Keys["LineNumber"] };
                    DataRow row = source.Rows.Find(keys);
                    row["SizeCode"] = values.NewValues["SizeCode"];
                    row["Qty"] = values.NewValues["Qty"];
                    row["SVOBreakdown"] = values.NewValues["SVOBreakdown"];
                    row["Field1"] = values.NewValues["Field1"];
                    row["Field2"] = values.NewValues["Field2"];
                    row["Field3"] = values.NewValues["Field3"];
                    row["Field4"] = values.NewValues["Field4"];
                    row["Field5"] = values.NewValues["Field5"];
                    row["Field6"] = values.NewValues["Field6"];
                    row["Field7"] = values.NewValues["Field7"];
                    row["Field8"] = values.NewValues["Field8"];
                    row["Field9"] = values.NewValues["Field9"];
                }

                foreach (ASPxDataDeleteValues values in e.DeleteValues)
                {
                    try
                    {
                        object[] keys = { values.Keys[0], values.Keys[1] };
                        source.Rows.Remove(source.Rows.Find(keys));
                    }
                    catch (Exception)
                    {
                        continue;
                    }
                }
                 
                Gears.RetriveData2("DELETE FROM Procurement.WISizeBreakdown where Docnumber='" + txtDocNumber.Text + "'", Session["ConnString"].ToString());
                
                foreach (DataRow dtRow in source.Rows) 
                {
                    _EntityDetail.SizeCode = dtRow["SizeCode"].ToString(); 
                    _EntityDetail.Qty = Convert.ToDecimal(Convert.IsDBNull(dtRow["Qty"]) ? 0 : dtRow["Qty"]);
                    _EntityDetail.SVOBreakdown = Convert.ToDecimal(Convert.IsDBNull(dtRow["SVOBreakdown"]) ? 0 : dtRow["SVOBreakdown"]); 
                    _EntityDetail.Field1 = dtRow["Field1"].ToString();
                    _EntityDetail.Field2 = dtRow["Field2"].ToString();
                    _EntityDetail.Field3 = dtRow["Field3"].ToString();
                    _EntityDetail.Field4 = dtRow["Field4"].ToString();
                    _EntityDetail.Field5 = dtRow["Field5"].ToString();
                    _EntityDetail.Field6 = dtRow["Field6"].ToString();
                    _EntityDetail.Field7 = dtRow["Field7"].ToString();
                    _EntityDetail.Field8 = dtRow["Field8"].ToString();
                    _EntityDetail.Field9 = dtRow["Field9"].ToString();
                    _EntityDetail.AddWISizeBreakDown(_EntityDetail);
                }
            }
        }
        #endregion

        private DataTable GetSelectedVal()
        {
            gv1.DataSourceID = null;
            gv1.DataSource = null;
            gv1.DataBind();
            DataTable dt = new DataTable();
            
            sdsServiceOrderdetail.SelectCommand = "SELECT '" + txtDocNumber.Text + "' DocNumber ,LineNumber ,StockSize AS SizeCode ,SVOQty AS SVOBreakdown , 0.0000 AS Qty "
                                                + ",'' AS Field1,'' AS Field2,'' AS Field3,'' AS Field4,'' AS Field5,'' AS Field6 "
                                                + ",'' AS Field7,'' AS Field8,'' AS Field9 FROM Procurement.ServiceOrder A "
                                                + "INNER JOIN Procurement.SOSizeBreakdown B ON A.DocNumber = B.DocNumber WHERE A.DocNumber = '" + glserviceorder.Text + "'";
            gv1.DataSource = sdsServiceOrderdetail;
            gv1.DataBind();

            Session["Datatable"] = "1";
            foreach (GridViewColumn col in gv1.VisibleColumns)
            {
                GridViewDataColumn dataColumn = col as GridViewDataColumn;
                if (dataColumn == null) continue;
                dt.Columns.Add(dataColumn.FieldName);
            }
            for (int i = 0; i < gv1.VisibleRowCount; i++)
            {
                DataRow row = dt.Rows.Add();
                foreach (DataColumn col in dt.Columns)
                    row[col.ColumnName] = gv1.GetRowValues(i, col.ColumnName);
            } 
            dt.PrimaryKey = new DataColumn[] { dt.Columns["DocNumber"], dt.Columns["LineNumber"] };
            return dt;
        }

        private void OtherDetail()
        {
            string pick = glserviceorder.Text; 
            DataTable ret = Gears.RetriveData2("SELECT ISNULL(WorkCenter,'') AS WorkCenter FROM Procurement.SOWorkOrder WHERE DocNumber = '" + pick + "'", Session["ConnString"].ToString());
            foreach (DataRow dt in ret.Rows)
            {
                txtWorkCenter.Text = dt["WorkCenter"].ToString();
            }
        }
    
        protected void dtpDocDate_Init(object sender, EventArgs e)
        {
            if (Request.QueryString["entry"] == "N") 
                dtpDocDate.Date = DateTime.Now; 
        }

        protected void Connection_Init(object sender, EventArgs e)
        {
            ((SqlDataSource)sender).ConnectionString = Session["ConnString"].ToString();
        } 
    }
}