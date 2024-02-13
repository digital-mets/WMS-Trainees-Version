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
using DevExpress.Data.Filtering;
using GearsInventory;

namespace GWL
{
    public partial class frmInvPhysicalCount : System.Web.UI.Page
    {
        Boolean error = false;//Boolean for grid validation
        Boolean view = false;//Boolean for view state
        Boolean check = false;//Boolean for grid validation

        Entity.InvPhysicalCount _Entity = new InvPhysicalCount();//Calls entity odsHeader
        Entity.InvPhysicalCount.InvPhysicalCountDetail _EntityDetail = new InvPhysicalCount.InvPhysicalCountDetail();//Call entity sdsDetail

        #region page load/entry
        protected void Page_Load(object sender, EventArgs e)
        {
            Gears.UseConnectionString(Session["ConnString"].ToString());
            
            string referer;
            try
            {
                referer = Request.ServerVariables["http_referer"];
            }
            catch
            {
                referer = "";
            }

            if (referer == null)
            {
                Response.Redirect("~/error.aspx");
            }

            dtpDocDate.MinDate = Convert.ToDateTime(GearsCommon.GCommon.SystemSetting("BookDate", Session["ConnString"].ToString()).ToString());

            txtDocNumber.Value = Request.QueryString["docnumber"].ToString();
            //gv1.DataSource = null;

            if (!IsPostBack)
            {

                Session["FilterExpression"] = null;

                if (Request.QueryString["entry"].ToString() == "N")
                {
                    popup.ShowOnPageLoad = false;
                }
                //else
                //{
                _Entity.getdata(txtDocNumber.Text, Session["ConnString"].ToString()); //Method for retrieving data from entity
                dtpDocDate.Text = Convert.ToDateTime(_Entity.DocDate.ToString()).ToShortDateString();
                aglPCType.Value = _Entity.Type.ToString();
                dtpCutOff.Text = String.IsNullOrEmpty(_Entity.CutOffDate.ToString()) ? null : Convert.ToDateTime(_Entity.CutOffDate.ToString()).ToShortDateString();
                chkIsFinal.Value = _Entity.IsFinal;
                aglWarehouseCode.Value = _Entity.WarehouseCode.ToString();
                aglWHType.Value = _Entity.WarehouseType.ToString();
                switch (_Entity.Status.ToString().Trim())
                {
                    case "N":
                    case null:
                    case "":
                        txtStatus.Text = "NEW";
                        break;
                    case "G":
                        txtStatus.Text = "GENERATED";
                        break;
                    case "A":
                        txtStatus.Text = "APPROVED";
                        break;
                    case "FA":
                        txtStatus.Text = "FOR APPLICATION";
                        break;
                    case "AP":
                        txtStatus.Text = "APPLIED";
                        break;
                    case "C":
                        txtStatus.Text = "APPLIED";
                        break;
                }
                memRemarks.Value = _Entity.Remarks.ToString();

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
                txtHGeneratedBy.Text = _Entity.GeneratedBy;
                txtHGeneratedDate.Text = _Entity.GeneratedDate;
                txtHSubmittedBy.Text = _Entity.SubmittedBy;
                txtHSubmittedDate.Text = _Entity.SubmittedDate;
                txtHCancelledBy.Text = _Entity.CancelledBy;
                txtHCancelledDate.Text = _Entity.CancelledDate;
                //}

                gv1.KeyFieldName = "LineNumber;DocNumber";

                //V=View, E=Edit, N=New
                switch (Request.QueryString["entry"].ToString())
                {
                    case "N":
                        if (!String.IsNullOrEmpty(_Entity.LastEditedBy))
                        {
                            updateBtn.Text = "Update";
                            if (!String.IsNullOrEmpty(aglPCType.Text))
                            {
                                aglPCType.ReadOnly = true;
                                aglPCType.DropDownButton.Enabled = false;
                            }
                        }
                        else
                        {
                            updateBtn.Text = "Add";
                            txtStatus.Text = "NEW";
                            aglPCType.ReadOnly = false;
                            aglPCType.DropDownButton.Enabled = true;
                        }
                        break;
                    case "E":
                        updateBtn.Text = "Update";
                        if (!String.IsNullOrEmpty(aglPCType.Text))
                        {
                            aglPCType.ReadOnly = true;
                            aglPCType.DropDownButton.Enabled = false;
                        }
                        break;
                    case "V":
                        view = true;//sets view mode for entry
                        updateBtn.Text = "Close";
                        glcheck.ClientVisible = false;
                        aglPCType.ReadOnly = true;
                        aglPCType.DropDownButton.Enabled = false;
                        break;
                    case "D":
                        view = true;
                        updateBtn.Text = "Delete";
                        aglPCType.ReadOnly = true;
                        aglPCType.DropDownButton.Enabled = false;
                        break;
                }

                DataTable dtbldetail = Gears.RetriveData2("SELECT DISTINCT DocNumber FROM Inventory.PhysicalCountDetail WHERE DocNumber ='" + txtDocNumber.Text + "'", Session["ConnString"].ToString());

                gv1.DataSourceID = (dtbldetail.Rows.Count > 0 ? "odsDetail" : "sdsDetail");

            }
        }
        #endregion

        #region Validation
        string strresult = "";
        private void Validate()
        {
            GearsLibrary.Gears.GearsParameter gparam = new Gears.GearsParameter();
            gparam._DocNo = _Entity.DocNumber;
            gparam._UserId = Session["Userid"].ToString();
            gparam._TransType = Request.QueryString["transtype"].ToString().Trim();
            gparam._Connection = Session["ConnString"].ToString();
            strresult += GearsInventory.GInventory.PhysicalCount_Validate(gparam);
            if (strresult.Length > 8)
            {
                cp.JSProperties["cp_valmsg"] = strresult;//Message variable to client side
            }
        }
        #endregion

        #region Post
        private void Post()
        {
            GearsLibrary.Gears.GearsParameter gparam = new Gears.GearsParameter();
            gparam._DocNo = _Entity.DocNumber;
            gparam._UserId = Session["Userid"].ToString();
            gparam._TransType = "INVCNT";
            gparam._Table = "Inventory.PhysicalCount";
            gparam._Factor = -1;
            gparam._Connection = Session["ConnString"].ToString();
            strresult += GearsInventory.GInventory.PhysicalCount_Post(gparam);
            if (strresult != " ")
            {
                cp.JSProperties["cp_valmsg"] = strresult;//Message variable to client side
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
        protected void gvLookupLoad(object sender, EventArgs e)//Control for all lookup in details/grid
        {
            if (Request.QueryString["entry"].ToString() == "V" || Request.QueryString["entry"].ToString() == "D")
            {
                ASPxGridLookup look = sender as ASPxGridLookup;
                look.Enabled = false;
            }
            else
            {
                ASPxGridLookup look = sender as ASPxGridLookup;
                look.Enabled = true;

            }
        }

        protected void gvTextBoxLoad(object sender, EventArgs e)//Control for all lookup in details/grid
        {
            if (Request.QueryString["entry"].ToString() == "V" || Request.QueryString["entry"].ToString() == "D")
            {
                GridViewDataTextColumn text = sender as GridViewDataTextColumn;
                text.ReadOnly = true;
            }
            else
            {
                GridViewDataTextColumn text = sender as GridViewDataTextColumn;
                text.ReadOnly = false;
            }
        }

        protected void CheckBoxLoad(object sender, EventArgs e)
        {
            var check = sender as ASPxCheckBox;
            check.ReadOnly = view;
        }

        protected void ComboBoxLoad(object sender, EventArgs e)
        {
            var combo = sender as ASPxComboBox;
            combo.ReadOnly = view;
        }

        protected void MemoLoad(object sender, EventArgs e)
        {
            var memo = sender as ASPxMemo;
            memo.ReadOnly = view;
        }
        protected void gv1_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)//Control for grid
        {
            //if (Request.QueryString["entry"].ToString() == "V" || Request.QueryString["entry"].ToString() == "D")
            //{
            //    ASPxGridView grid = sender as ASPxGridView;
            //    grid.SettingsBehavior.AllowGroup = false;
            //    grid.SettingsBehavior.AllowSort = false;
            //    e.Editor.ReadOnly = view;
            //}
            //else
            //{
            //    ASPxGridView grid = sender as ASPxGridView;
            //    grid.SettingsBehavior.AllowGroup = false;
            //    grid.SettingsBehavior.AllowSort = false;
            //    e.Editor.ReadOnly = false;
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
        {   //Sets icon for grid/Control for Enabling/Disabling of buttons on grid.
            if (e.ButtonType == ColumnCommandButtonType.Delete)
            {
                e.Image.IconID = "actions_cancel_16x16";
            }
            if (e.ButtonType == ColumnCommandButtonType.New)
            {
                e.Image.IconID = "actions_addfile_16x16";

            }
            if (e.ButtonType == ColumnCommandButtonType.Edit)
            {
                e.Image.IconID = "actions_addfile_16x16";
            }
            if (e.ButtonType == ColumnCommandButtonType.Update)
                e.Visible = false;

            if (Request.QueryString["entry"] != "N" || Request.QueryString["entry"] != "E")
            {
                if (e.ButtonType == ColumnCommandButtonType.Edit ||
                    e.ButtonType == ColumnCommandButtonType.Cancel)
                    e.Visible = false;
            }

            if (Request.QueryString["entry"] == "N" || Request.QueryString["entry"] == "E")
            {
                if (e.ButtonType == ColumnCommandButtonType.New || e.ButtonType == ColumnCommandButtonType.Delete)
                {
                    e.Visible = true;
                }
            }

            if (Request.QueryString["entry"] == "V" || Request.QueryString["entry"] == "D")
            {
                if (e.ButtonType == ColumnCommandButtonType.New || e.ButtonType == ColumnCommandButtonType.Delete)
                {
                    e.Visible = false;
                }
            }
        }

        protected void gv1_CustomButtonInitialize(object sender, ASPxGridViewCustomButtonEventArgs e)
        {
            if (Request.QueryString["entry"] == "N")
            {
                if (e.ButtonID == "Details" || e.ButtonID == "CountSheet")
                {
                    e.Visible = DevExpress.Utils.DefaultBoolean.False;
                }
            }

            if (Request.QueryString["entry"] == "V" || Request.QueryString["entry"] == "D")
            {
                if (e.ButtonID == "Delete")
                {
                    e.Visible = DevExpress.Utils.DefaultBoolean.False;
                }
            }
        }
        #endregion

        #region Lookup Settings
        protected void lookup_Init(object sender, EventArgs e)
        {
            ASPxGridLookup gridLookup = sender as ASPxGridLookup;
            gridLookup.GridView.CustomCallback += new ASPxGridViewCustomCallbackEventHandler(gridView_CustomCallback);
            if (Session["FilterExpression"] != null)
            {
                gridLookup.GridView.DataSourceID = "sdsItemDetail";
                sdsItemDetail.FilterExpression = Session["FilterExpression"].ToString();
            }
        }
        public void gridView_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            string column = e.Parameters.Split('|')[0];//Set column name
            if (column.Contains("GLP_AIC") || column.Contains("GLP_AC") || column.Contains("GLP_F")) return;//Traps the callback
            string itemcode = e.Parameters.Split('|')[1];//Set Item Code
            string val = e.Parameters.Split('|')[2];//Set column value
            if (val.Contains("GLP_AIC") || val.Contains("GLP_AC") || val.Contains("GLP_F")) return;//Traps the callback
            var itemlookup = sender as ASPxGridView;
            string codes = "";
            if (e.Parameters.Contains("ItemCode"))
            {
                int cntclr = 0;
                int cntcls = 0;
                int cntsze = 0;

                DataTable countitem = Gears.RetriveData2("Select DISTINCT ColorCode,ClassCode,SizeCode,UnitBase,FullDesc, UnitBulk, ISNULL(A.IsByBulk,0) AS IsByBulk FROM Masterfile.Item A " +
                                                          "LEFT JOIN Masterfile.ItemDetail B ON A.ItemCode = B.ItemCode WHERE A.ItemCode = '" + itemcode + "' AND (ISNULL(A.IsInActive,0) = 0 OR ISNULL(B.IsInActive,0) = 0)", Session["ConnString"].ToString());

                DataTable desc = Gears.RetriveData2("SELECT DISTINCT FullDesc, ISNULL(IsByBulk,0) AS IsByBulk, UnitBase  FROM Masterfile.Item WHERE ItemCode = '" + itemcode + "'", Session["ConnString"].ToString());

                DataTable countcolor = Gears.RetriveData2("SELECT DISTINCT ColorCode FROM Masterfile.Item A " +
                                                          "LEFT JOIN Masterfile.ItemDetail B ON A.ItemCode = B.ItemCode " + 
                                                          "WHERE A.ItemCode = '" + itemcode + "' AND (ISNULL(A.IsInActive,0) = 0 OR " + 
                                                          "ISNULL(B.IsInActive,0) = 0)", Session["ConnString"].ToString());

                cntclr = countcolor.Rows.Count;

                DataTable countclass = Gears.RetriveData2("SELECT DISTINCT ClassCode FROM Masterfile.Item A " +
                                                          "LEFT JOIN Masterfile.ItemDetail B ON A.ItemCode = B.ItemCode " +
                                                          "WHERE A.ItemCode = '" + itemcode + "' AND (ISNULL(A.IsInActive,0) = 0 OR " +
                                                          "ISNULL(B.IsInActive,0) = 0)", Session["ConnString"].ToString());

                cntcls = countclass.Rows.Count;

                DataTable countsize = Gears.RetriveData2("SELECT DISTINCT SizeCode FROM Masterfile.Item A " +
                                                          "LEFT JOIN Masterfile.ItemDetail B ON A.ItemCode = B.ItemCode " +
                                                          "WHERE A.ItemCode = '" + itemcode + "' AND (ISNULL(A.IsInActive,0) = 0 OR " +
                                                          "ISNULL(B.IsInActive,0) = 0)", Session["ConnString"].ToString());

                cntsze = countsize.Rows.Count;

                if (countitem.Rows.Count == 1)
                {
                    foreach (DataRow dt in countitem.Rows)
                    {
                        codes = dt["ColorCode"].ToString() + ";";
                        codes += dt["ClassCode"].ToString() + ";";
                        codes += dt["SizeCode"].ToString() + ";";
                        codes += dt["UnitBase"].ToString() + ";";
                        codes += dt["FullDesc"].ToString() + ";";
                        //codes += dt["UnitBulk"].ToString() + ";";
                        codes += dt["IsByBulk"].ToString() + ";";
                    }
                }
                else
                {
                    if (cntclr > 1 && cntcls == 1 && cntsze == 1)
                    {
                        //foreach (DataRow dt in countitem.Rows)
                        //{
                            codes = ";";
                            codes += countclass.Rows[0]["ClassCode"].ToString() + ";";
                            codes += countsize.Rows[0]["SizeCode"].ToString() + ";";
                            codes += desc.Rows[0]["UnitBase"].ToString() + ";";
                            codes += desc.Rows[0]["FullDesc"].ToString() + ";";
                            //codes += dt["UnitBulk"].ToString() + ";";
                            codes += desc.Rows[0]["IsByBulk"].ToString() + ";";
                        //}
                    }
                    else if (cntcls > 1 && cntclr == 1 && cntsze == 1)
                    {
                        //foreach (DataRow dt in countitem.Rows)
                        //{
                            codes = countcolor.Rows[0]["ColorCode"].ToString() + ";";
                            codes += ";";
                            codes += countsize.Rows[0]["SizeCode"].ToString() + ";";
                            codes += desc.Rows[0]["UnitBase"].ToString() + ";";
                            codes += desc.Rows[0]["FullDesc"].ToString() + ";";
                            //codes += dt["UnitBulk"].ToString() + ";";
                            codes += desc.Rows[0]["IsByBulk"].ToString() + ";";
                        //}
                    }
                    else if (cntsze > 1 && cntclr == 1 && cntcls == 1)
                    {
                        //foreach (DataRow dt in countitem.Rows)
                        //{
                            codes = countcolor.Rows[0]["ColorCode"].ToString() + ";";
                            codes += countclass.Rows[0]["ClassCode"].ToString() + ";";
                            codes += ";";
                            codes += desc.Rows[0]["UnitBase"].ToString() + ";";
                            codes += desc.Rows[0]["FullDesc"].ToString() + ";";
                            //codes += dt["UnitBulk"].ToString() + ";";
                            codes += desc.Rows[0]["IsByBulk"].ToString() + ";";
                        //}
                    }
                    else
                    {
                        //foreach (DataRow dt in countcolor.Rows)
                        //{
                            codes = ";;;" + desc.Rows[0]["UnitBase"].ToString() + ";" + desc.Rows[0]["FullDesc"].ToString() + ";" + desc.Rows[0]["IsByBulk"].ToString() + ";";
                        //}
                    }

                }

                itemlookup.JSProperties["cp_codes"] = codes;
            }
            else
            {
                if (e.Parameters.Contains("ColorCode"))
                {
                    sdsItemDetail.SelectCommand = "SELECT DISTINCT ItemCode, [ColorCode], '' AS [ClassCode], '' AS [SizeCode] FROM Masterfile.ItemDetail";
                }
                else if (e.Parameters.Contains("ClassCode"))
                {
                    sdsItemDetail.SelectCommand = "SELECT DISTINCT ItemCode, '' AS [ColorCode], [ClassCode], '' AS [SizeCode] FROM Masterfile.ItemDetail";
                }
                else if (e.Parameters.Contains("SizeCode"))
                {
                    sdsItemDetail.SelectCommand = "SELECT DISTINCT ItemCode, '' AS [ColorCode], '' AS [ClassCode], [SizeCode] FROM Masterfile.ItemDetail";
                }

                ASPxGridView grid = sender as ASPxGridView;
                //ASPxGridLookup lookup = (ASPxGridLookup)gv1.FindEditRowCellTemplateControl((GridViewDataColumn)gv1.Columns[2], "glItemCode");
                var selectedValues = itemcode;
                CriteriaOperator selectionCriteria = new InOperator("ItemCode", new string[] { itemcode });
                sdsItemDetail.FilterExpression = (GroupOperator.Combine(GroupOperatorType.And, selectionCriteria)).ToString();
                Session["FilterExpression"] = sdsItemDetail.FilterExpression;
                grid.DataSourceID = "sdsItemDetail";
                grid.DataBind();

                for (int i = 0; i < grid.VisibleRowCount; i++)
                {
                    if (grid.GetRowValues(i, column) != null)
                        if (grid.GetRowValues(i, column).ToString() == val)
                        {
                            grid.Selection.SelectRow(i);
                            string key = grid.GetRowValues(i, column).ToString();
                            grid.MakeRowVisible(key);
                            break;
                        }
                }
            }
        }

        #endregion

        #region Callback functions(Inserting/Updating/Deleting/Validating)
        protected void cp_Callback(object sender, CallbackEventArgsBase e)
        {
            _Entity.DocNumber = txtDocNumber.Value.ToString();
            _Entity.DocDate = dtpDocDate.Text;
            _Entity.Type = String.IsNullOrEmpty(aglPCType.Text) ? null : aglPCType.Value.ToString();
            _Entity.CutOffDate = String.IsNullOrEmpty(dtpCutOff.Text) ? DateTime.Now.ToString() : dtpCutOff.Text;
            _Entity.IsFinal = Convert.ToBoolean(chkIsFinal.Value.ToString());
            _Entity.WarehouseCode = String.IsNullOrEmpty(aglWarehouseCode.Text) ? null : aglWarehouseCode.Value.ToString();
            _Entity.WarehouseType = String.IsNullOrEmpty(aglWHType.Text) ? null : aglWHType.Value.ToString();
            _Entity.Connection = Session["ConnString"].ToString();

            switch (txtStatus.Text)
            {
                case "NEW":
                case null:
                    _Entity.Status = "N";
                    break;
                case "GENERATED":
                    _Entity.Status = "G";
                    break;
                case "APPROVED":
                    _Entity.Status = "A";
                    break;
                case "FOR APPLICATION":
                    _Entity.Status = "FA";
                    break;
                case "APPLIED":
                    _Entity.Status = "AP";
                    break;
            }
            _Entity.Remarks = String.IsNullOrEmpty(memRemarks.Text) ? null : memRemarks.Text;
           
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

                    gv1.UpdateEdit();
                    
                    string strError = Functions.Submitted(_Entity.DocNumber, "Inventory.PhysicalCount", 1, Session["ConnString"].ToString());//NEWADD factor 1 if submit, 2 if approve
                    if (!string.IsNullOrEmpty(strError))
                    {
                        cp.JSProperties["cp_message"] = strError;
                        cp.JSProperties["cp_success"] = true;
                        cp.JSProperties["cp_forceclose"] = true;
                        return;
                    }

                    if (error == false)
                    {
                        check = true;
                        _Entity.LastEditedBy = Session["userid"].ToString();
                        _Entity.UpdateData(_Entity);
                        _Entity.UpdateIsGenerated(txtDocNumber.Text, Session["ConnString"].ToString());
                        gv1.DataSourceID = "odsDetail";
                        odsDetail.SelectParameters["DocNumber"].DefaultValue = txtDocNumber.Text;
                        gv1.UpdateEdit();
                        Validate();
                        cp.JSProperties["cp_message"] = "Successfully Added!";
                        cp.JSProperties["cp_success"] = true;
                        cp.JSProperties["cp_close"] = true;
                        Session["Refresh"] = "1";
                    }
                    else
                    {
                        cp.JSProperties["cp_message"] = "Please check all the fields!";
                        cp.JSProperties["cp_success"] = true;
                    }

                    break;

                case "Update":

                    gv1.UpdateEdit();
                    
                    string strError1 = Functions.Submitted(_Entity.DocNumber, "Inventory.PhysicalCount", 1, Session["ConnString"].ToString());//NEWADD factor 1 if submit, 2 if approve
                    if (!string.IsNullOrEmpty(strError1))
                    {
                        cp.JSProperties["cp_message"] = strError1;
                        cp.JSProperties["cp_success"] = true;
                        cp.JSProperties["cp_forceclose"] = true;
                        return;
                    }

                    if (error == false)
                    {
                        check = true;
                        _Entity.LastEditedBy = Session["userid"].ToString();
                        _Entity.UpdateData(_Entity);
                        _Entity.UpdateIsGenerated(txtDocNumber.Text, Session["ConnString"].ToString());
                        gv1.DataSourceID = "odsDetail";
                        odsDetail.SelectParameters["DocNumber"].DefaultValue = txtDocNumber.Text;
                        gv1.UpdateEdit();
                        Validate();
                        cp.JSProperties["cp_message"] = "Successfully Updated!";
                        cp.JSProperties["cp_success"] = true;
                        cp.JSProperties["cp_close"] = true;
                        Session["Refresh"] = "1";
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
                                    
                case "RefGrid":
                    gv1.DataBind();
                    break;

                case "CallbackPCType":
                    gv1.DataSource = sdsDetail;
                    if (gv1.DataSourceID != "")
                    {
                        gv1.DataSourceID = null;
                    }
                    gv1.DataBind();
                    Values();
                    break;


                case "CallbackTRR":
                    cp.JSProperties["cp_TRR"] = true;
                    break;
            }
        }
        protected void grid_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        {
        }
        //dictionary method to hold error 
        void AddError(Dictionary<GridViewColumn, string> errors, GridViewColumn column, string errorText)
        {
            if (errors.ContainsKey(column)) return;
            errors[column] = errorText;
        }
        protected void gv1_BatchUpdate(object sender, ASPxDataBatchUpdateEventArgs e)
        {
            if ((error == true || error == false) && check == false)//Prevents updating of grid to enable validation
            {
                e.Handled = true;
                e.InsertValues.Clear();
                e.UpdateValues.Clear();
            }

            //if (Session["Datatable"] == "1" && check == true)
            //{
            //    e.Handled = true;
            //    DataTable source = GetSelectedVal();

            //    foreach (ASPxDataDeleteValues values in e.DeleteValues)
            //    {
            //        try
            //        {
            //            object[] keys = { values.Keys[0] };
            //            source.Rows.Remove(source.Rows.Find(keys));
            //        }
            //        catch (Exception)
            //        {
            //            continue;
            //        }
            //    }

            //    foreach (ASPxDataInsertValues values in e.InsertValues)//for insert chu chu
            //    {
            //        var ItemCode = values.NewValues["ItemCode"];
            //        var FullDesc = values.NewValues["FullDesc"];
            //        var ColorCode = values.NewValues["ColorCode"];
            //        var ClassCode = values.NewValues["ClassCode"];
            //        var SizeCode = values.NewValues["SizeCode"];
            //        var IssuedQty = values.NewValues["IssuedQty"];
            //        var Unit = values.NewValues["Unit"];
            //        var IssuedBulkQty = values.NewValues["IssuedBulkQty"];
            //        var IssuedBulkUnit = values.NewValues["IssuedBulkUnit"];
            //        var RequestedQty = values.NewValues["RequestedQty"];
            //        var IsByBulk = values.NewValues["IsByBulk"];
            //        var Cost = values.NewValues["Cost"];
            //        var ItemPrice = values.NewValues["ItemPrice"];
            //        var ReturnedQty = values.NewValues["ReturnedQty"];
            //        var ReturnedBulkQty = values.NewValues["ReturnedBulkQty"];
            //        var ReturnedBulkUnit = values.NewValues["ReturnedBulkUnit"];
            //        var StatusCode = values.NewValues["StatusCode"];
            //        var BaseQty = values.NewValues["BaseQty"];
            //        var BarcodeNo = values.NewValues["BarcodeNo"];
            //        var UnitFactor = values.NewValues["UnitFactor"];
            //        var Field1 = values.NewValues["Field1"];
            //        var Field2 = values.NewValues["Field2"];
            //        var Field3 = values.NewValues["Field3"];
            //        var Field4 = values.NewValues["Field4"];
            //        var Field5 = values.NewValues["Field5"];
            //        var Field6 = values.NewValues["Field6"];
            //        var Field7 = values.NewValues["Field7"];
            //        var Field8 = values.NewValues["Field8"];
            //        var Field9 = values.NewValues["Field9"];
            //        source.Rows.Add(ItemCode, FullDesc, ColorCode, ClassCode, SizeCode, IssuedQty, Unit, IssuedBulkQty, IssuedBulkUnit, RequestedQty, IsByBulk, Cost, ItemPrice, ReturnedQty, ReturnedBulkQty, ReturnedBulkUnit, StatusCode, BaseQty, BarcodeNo, UnitFactor, Field1, Field2, Field3, Field4, Field5, Field6, Field7, Field8, Field9);

            //    }

            //    foreach (ASPxDataUpdateValues values in e.UpdateValues)
            //    {
            //        object[] keys = { values.NewValues["LineNumber"],values.NewValues["DocNumber"] };
            //        DataRow row = source.Rows.Find(keys);
            //        row["ItemCode"] = values.NewValues["ItemCode"];
            //        row["FullDesc"] = values.NewValues["FullDesc"];
            //        row["ColorCode"] = values.NewValues["ColorCode"];
            //        row["ClassCode"] = values.NewValues["ClassCode"];
            //        row["SizeCode"] = values.NewValues["SizeCode"];
            //        row["IssuedQty"] = values.NewValues["IssuedQty"];
            //        row["Unit"] = values.NewValues["Unit"];
            //        row["IssuedBulkQty"] = values.NewValues["IssuedBulkQty"];
            //        row["IssuedBulkUnit"] = values.NewValues["IssuedBulkUnit"];
            //        row["RequestedQty"] = values.NewValues["RequestedQty"];
            //        row["IsByBulk"] = values.NewValues["IsByBulk"];
            //        row["Cost"] = values.NewValues["Cost"];
            //        row["ItemPrice"] = values.NewValues["ItemPrice"];
            //        row["ReturnedQty"] = values.NewValues["ReturnedQty"];
            //        row["ReturnedBulkQty"] = values.NewValues["ReturnedBulkQty"];
            //        row["ReturnedBulkUnit"] = values.NewValues["ReturnedBulkUnit"];
            //        row["StatusCode"] = values.NewValues["StatusCode"];
            //        row["BaseQty"] = values.NewValues["BaseQty"];
            //        row["BarcodeNo"] = values.NewValues["BarcodeNo"];
            //        row["UnitFactor"] = values.NewValues["UnitFactor"];
            //        row["Field1"] = values.NewValues["Field1"];
            //        row["Field2"] = values.NewValues["Field2"];
            //        row["Field3"] = values.NewValues["Field3"];
            //        row["Field4"] = values.NewValues["Field4"];
            //        row["Field5"] = values.NewValues["Field5"];
            //        row["Field6"] = values.NewValues["Field6"];
            //        row["Field7"] = values.NewValues["Field7"];
            //        row["Field8"] = values.NewValues["Field8"];
            //        row["Field9"] = values.NewValues["Field9"];
            //    }

            //    foreach (DataRow dtRow in source.Rows)//This is where the data will be inserted into db
            //    {
            //        _EntityDetail.ItemCode = dtRow["ItemCode"].ToString();
            //        _EntityDetail.FullDesc = dtRow["FullDesc"].ToString();
            //        _EntityDetail.ColorCode = dtRow["ColorCode"].ToString();
            //        _EntityDetail.ClassCode = dtRow["ClassCode"].ToString();
            //        _EntityDetail.SizeCode = dtRow["SizeCode"].ToString();
            //        _EntityDetail.IssuedQty = Convert.ToDecimal(dtRow["IssuedQty"].ToString());
            //        _EntityDetail.Unit = dtRow["Unit"].ToString();
            //        _EntityDetail.IssuedBulkQty = Convert.ToDecimal(dtRow["IssuedBulkQty"].ToString());
            //        _EntityDetail.IssuedBulkUnit = dtRow["IssuedBulkUnit"].ToString();
            //        _EntityDetail.RequestedQty = Convert.ToDecimal(dtRow["RequestedQty"].ToString());
            //        _EntityDetail.IsByBulk = Convert.ToBoolean(dtRow["IsByBulk"].ToString());
            //        _EntityDetail.Cost = Convert.ToDecimal(dtRow["Cost"].ToString());
            //        _EntityDetail.ItemPrice = Convert.ToDecimal(dtRow["ItemPrice"].ToString());
            //        _EntityDetail.ReturnedQty = Convert.ToDecimal(dtRow["ReturnedQty"].ToString());
            //        _EntityDetail.ReturnedBulkQty = Convert.ToDecimal(dtRow["ReturnedBulkQty"].ToString());
            //        _EntityDetail.ReturnedBulkUnit = dtRow["ReturnedBulkUnit"].ToString();
            //        _EntityDetail.StatusCode = dtRow["StatusCode"].ToString();
            //        _EntityDetail.BaseQty = Convert.ToDecimal(dtRow["BaseQty"].ToString());
            //        _EntityDetail.BarcodeNo = dtRow["BarcodeNo"].ToString();
            //        _EntityDetail.UnitFactor = Convert.ToDecimal(dtRow["UnitFactor"].ToString());

            //        _EntityDetail.Field1 = dtRow["Field1"].ToString();
            //        _EntityDetail.Field2 = dtRow["Field2"].ToString();
            //        _EntityDetail.Field3 = dtRow["Field3"].ToString();
            //        _EntityDetail.Field4 = dtRow["Field4"].ToString();
            //        _EntityDetail.Field5 = dtRow["Field5"].ToString();
            //        _EntityDetail.Field6 = dtRow["Field6"].ToString();
            //        _EntityDetail.Field7 = dtRow["Field7"].ToString();
            //        _EntityDetail.Field8 = dtRow["Field8"].ToString();
            //        _EntityDetail.Field9 = dtRow["Field9"].ToString();

            //        _EntityDetail.AddIssuanceDetail(_EntityDetail);
            //    }
            //}
        }
        #endregion

        protected void gv1_Init(object sender, EventArgs e)
        {
            //if (!IsPostBack)
            //{
            //    Session["detail"] = null;
            //}

            //if (Session["detail"] != null)
            //{
            //    gv1.DataSourceID = sdsReqDetail.ID;
            //}
        }

        //private DataTable GetSelectedVal()
        //{
        //    Session["Datatable"] = "0";
        //    if (gv1.DataSourceID != "")
        //    {
        //        gv1.DataSourceID = null;
        //    }
        //    gv1.DataBind();
        //    DataTable dt = new DataTable();
        //    string[] selectedValues = aglRefNum.Text.Split(';');
        //    CriteriaOperator selectionCriteria = new InOperator("DocNumber", selectedValues);
        //    string RefSO = selectionCriteria.ToString();

        //    sdsReqDetail.SelectCommand = "SELECT DocNumber, RIGHT('00000'+ CAST(ROW_NUMBER() OVER (ORDER BY DocNumber) AS VARCHAR(5)),5) AS LineNumber, A.ItemCode, FullDesc, ColorCode, ClassCode, SizeCode, 0.00 AS IssuedQty, Unit, 0.00 AS IssuedBulkQty, '' AS IssuedBulkUnit, RequestedBulkQty, "
        //    + " RequestedQty, IsByBulk, ISNULL(Cost,0) AS Cost, ISNULL(ItemPrice,0) ItemPrice, 0.00 AS ReturnedQty, '' AS ReturnedBulkQty, '' AS ReturnedBulkUnit, '' AS StatusCode, 0.00 AS BaseQty, ISNULL(A.BarcodeNo,'') AS BarcodeNo, ISNULL(A.UnitFactor,'0.0000') UnitFactor, "
        //    + " A.Field1, A.Field2, A.Field3, A.Field4, A.Field5, A.Field6, A.Field7, A.Field8, A.Field9, " 
        //    + " '2' AS Version FROM Inventory.RequestDetail A INNER JOIN Masterfile.Item B ON A.ItemCode = B.ItemCode WHERE DocNumber = '" + aglRefNum.Text.Trim() + "'";

        //    gv1.DataSource = sdsReqDetail;
        //    if (gv1.DataSourceID != "")
        //    {
        //        gv1.DataSourceID = null;
        //    }
        //    gv1.DataBind();
        //    Session["Datatable"] = "1";

        //    foreach (GridViewColumn col in gv1.VisibleColumns)
        //    {
        //        GridViewDataColumn dataColumn = col as GridViewDataColumn;
        //        if (dataColumn == null) continue;
        //        dt.Columns.Add(dataColumn.FieldName);
        //    }
        //    for (int i = 0; i < gv1.VisibleRowCount; i++)
        //    {
        //        DataRow row = dt.Rows.Add();
        //        foreach (DataColumn col in dt.Columns)
        //            row[col.ColumnName] = gv1.GetRowValues(i, col.ColumnName);
        //    }

        //    dt.PrimaryKey = new DataColumn[] { //Sets datatable's primary key for batchupdate reference during adding.
        //    dt.Columns["LineNumber"], dt.Columns["DocNumber"]};

        //    return dt;
        //}
        protected void dtpDocDate_Init(object sender, EventArgs e)
        {
            if (Request.QueryString["entry"] == "N")
            {
                dtpDocDate.Date = DateTime.Now;
            }
        }
        protected void Connection_Init(object sender, EventArgs e)
        {
            ((SqlDataSource)sender).ConnectionString = Session["ConnString"].ToString();
        }
        protected void itemcode_Init(object sender, EventArgs e)
        {
            ASPxGridLookup gridLookup = sender as ASPxGridLookup;
            gridLookup.GridView.CustomCallback += new ASPxGridViewCustomCallbackEventHandler(item_CustomCallback);
            FilterItem();
            gridLookup.DataBind();
        }
        public void FilterItem()
        {
            sdsItem.SelectCommand = "SELECT [ItemCode], [FullDesc], [ShortDesc] FROM Masterfile.[Item] WHERE ISNULL(IsInactive,0) = 0 ORDER BY ItemCode ASC";
        }

        public void item_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            if (e.Parameters.Contains("GLP_AIC") || e.Parameters.Contains("GLP_AC") || e.Parameters.Contains("GLP_F")) return;
            ASPxGridView grid = sender as ASPxGridView;
            FilterItem();
            grid.DataSourceID = "sdsItem";
            grid.DataBind();

            for (int i = 0; i < grid.VisibleRowCount; i++)
            {
                if (grid.GetRowValues(i, "ItemCode") != null)
                    if (grid.GetRowValues(i, "ItemCode").ToString() == e.Parameters)
                    {
                        grid.Selection.SelectRow(i);
                        string key = grid.GetRowValues(i, "ItemCode").ToString();
                        grid.MakeRowVisible(key);
                        break;
                    }
            }
        }

        private void Values()
        {            
            switch (aglPCType.Value.ToString().Trim())
            {
                case "P":
                case "C":
                    aglWHType.Text = "Warehouse";
                    break;
                case "O":
                    aglWHType.Text = "Outlet";
                    break;
            }
        }

        protected void glItemCode_Init(object sender, EventArgs e)
        {
            ASPxGridLookup gridLookup = sender as ASPxGridLookup;
            if (IsCallback && Request.Params["__CALLBACKID"].Contains("glItemCode")
                && Request.Params["__CALLBACKID"].Contains(gridLookup.ID))
            {
                string test = Request.Params["__CALLBACKID"];
                FilterItem();
            }
            if (IsCallback && Request.Params["__CALLBACKID"].Contains("glItemCode")
                && Request.Params["__CALLBACKPARAM"].Contains("GLP_AC")
                && Request.Params["__CALLBACKPARAM"].Contains("CUSTOMCALLBACK"))
            {
                string codes = "";
                string param = Request.Params["__CALLBACKPARAM"].ToString().Substring(Request.Params["__CALLBACKPARAM"].ToString().LastIndexOf('|') + 1);
                //string changes = param.Split(';')[0];
                string itemcode = param.Split(';')[1];
                if (string.IsNullOrEmpty(itemcode)) return;


                DataTable getColor = Gears.RetriveData2("Select DISTINCT ColorCode FROM masterfile.item a " +
                                                                     "left join masterfile.itemdetail b on a.itemcode = b.itemcode where a.itemcode = '" + itemcode + "'", Session["ConnString"].ToString());//ADD CONN

                if (getColor.Rows.Count > 1)
                {
                    codes = "" + ";";
                    Session["ColorCode"] = "";
                }
                else
                {
                    foreach (DataRow dt in getColor.Rows)
                    {
                        codes = dt["ColorCode"].ToString() + ";";
                        Session["ColorCode"] = dt["ColorCode"].ToString();
                    }
                }

                DataTable getClass = Gears.RetriveData2("Select DISTINCT ClassCode FROM masterfile.item a " +
                                                                         "left join masterfile.itemdetail b on a.itemcode = b.itemcode where a.itemcode = '" + itemcode + "'", Session["ConnString"].ToString());//ADD CONN

                if (getClass.Rows.Count > 1)
                {
                    codes += "" + ";";
                    Session["ClassCode"] = "";
                }
                else
                {
                    foreach (DataRow dt in getClass.Rows)
                    {
                        codes += dt["ClassCode"].ToString() + ";";
                        Session["ClassCode"] = dt["ClassCode"].ToString();
                    }
                }

                DataTable getSize = Gears.RetriveData2("Select DISTINCT SizeCode FROM masterfile.item a " +
                                                                             "left join masterfile.itemdetail b on a.itemcode = b.itemcode where a.itemcode = '" + itemcode + "'", Session["ConnString"].ToString());//ADD CONN

                if (getSize.Rows.Count > 1)
                {
                    codes += "" + ";";
                    Session["SizeCode"] = "";
                }
                else
                {
                    foreach (DataRow dt in getSize.Rows)
                    {
                        codes += dt["SizeCode"].ToString() + ";";
                        Session["SizeCode"] = dt["SizeCode"].ToString();
                    }
                }


                DataTable getUnitBase = Gears.RetriveData2("SELECT DISTINCT UnitBase FROM Masterfile.item WHERE ItemCode = '" + itemcode + "'", Session["ConnString"].ToString());//ADD CONN
                if (getUnitBase.Rows.Count == 0)
                {
                    codes += "" + ";";
                }
                else
                {
                    foreach (DataRow dt in getUnitBase.Rows)
                    {
                        codes += dt["UnitBase"].ToString() + ";";
                    }
                }

                DataTable getFullDesc = Gears.RetriveData2("select * from masterfile.Item where ItemCode = '" + itemcode + "'", Session["ConnString"].ToString());

                if (getFullDesc.Rows.Count == 0)
                {
                    codes += "" + ";";
                }
                else
                {
                    foreach (DataRow dt in getFullDesc.Rows)
                    {
                        codes += dt["FullDesc"].ToString() + ";";
                        Session["FullDesc"] = dt["FullDesc"].ToString();
                    }

                }

                DataTable getIsByBulk = Gears.RetriveData2("select ISNULL(IsByBulk,0) AS IsByBulk from masterfile.Item where ItemCode = '" + itemcode + "'", Session["ConnString"].ToString());

                if (getIsByBulk.Rows.Count == 0)
                {
                    codes += "" + ";";
                    Session["IsByBulk"] = "";
                }
                else
                {
                    foreach (DataRow dt in getFullDesc.Rows)
                    {
                        codes += dt["IsByBulk"].ToString() + ";";
                        Session["IsByBulk"] = dt["IsByBulk"].ToString();
                    }

                }
                gridLookup.GridView.JSProperties["cp_codes"] = codes;
                gridLookup.GridView.JSProperties["cp_valch"] = true;
            }
        }
    }
}