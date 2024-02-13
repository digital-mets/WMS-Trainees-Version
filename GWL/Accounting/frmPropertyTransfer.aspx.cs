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
//using GearsProcurement;
//using GearsSales;
using GearsAccounting;

namespace GWL
{
    public partial class frmPropertyTransfer : System.Web.UI.Page
    {
        Boolean error = false;//Boolean for grid validation
        Boolean view = false;//Boolean for view state
        Boolean check = false;//Boolean for grid validation
        Boolean edit = false; //Bolean for edit state

        private static string strError;

        Entity.PropertyTransfer _Entity = new PropertyTransfer();//Calls entity ICN

        #region page load/entry
        protected void Page_Load(object sender, EventArgs e)
        {
            Gears.UseConnectionString(Session["ConnString"].ToString());
            string referer;
            try //Validation to restrict user to browse/type directly to browser's address
            {
                referer = Request.ServerVariables["http_referer"];
            }
            catch
            {
                referer = "";
            }

            if (referer == null && Common.Common.SystemSetting("URLCHECK", Session["ConnString"].ToString()) != "NO")
            {
                Response.Redirect("~/error.aspx");
            }

            // gv1.KeyFieldName = "DocNumber;LineNumber";
            
            dtDocDate.MinDate = Convert.ToDateTime(GearsCommon.GCommon.SystemSetting("BookDate", Session["ConnString"].ToString()).ToString());

            if (!IsPostBack)
            {
                Session["ProTranFilterExpression"] = null;
                txtDocNumber.Value = Request.QueryString["docnumber"].ToString();

                _Entity.getdata(txtDocNumber.Text, Session["ConnString"].ToString());//ADD CONN

                dtDocDate.Value = String.IsNullOrEmpty(_Entity.DocDate) ? DateTime.Now : Convert.ToDateTime(_Entity.DocDate);

                cbTransferType.Value = _Entity.TransferType;
                if (string.IsNullOrEmpty(cbTransferType.Text))
                    cbTransferType.SelectedIndex = 0;

                //txtItemCode.Value = _Entity.ItemCode;
                //txtFullDesc.Value = _Entity.FullDesc;
                //txtColorCode.Value = _Entity.ColorCode;
                //txtClassCode.Value = _Entity.ClassCode;
                //txtSizeCode.Value = _Entity.SizeCode;
                //spinQty.Value = _Entity.Qty;
                //glNewLocation.Value = _Entity.NewLocation;
                //txtLocation.Value = _Entity.Location;
                //glNewDepartment.Value = _Entity.NewDepartment;
                //txtDepartment.Value = _Entity.Department;
                //glNewAccountablePerson.Value = _Entity.NewAccountablePerson;
                //txtAccountablePerson.Value = _Entity.AccountablePerson;
                //glNewWarehouseCode.Value = _Entity.NewWarehouseCode;
                //txtWarehouseCode.Value = _Entity.WarehouseCode;

                txtHField1.Value = _Entity.Field1;
                txtHField2.Value = _Entity.Field2;
                txtHField3.Value = _Entity.Field3;
                txtHField4.Value = _Entity.Field4;
                txtHField5.Value = _Entity.Field5;
                txtHField6.Value = _Entity.Field6;
                txtHField7.Value = _Entity.Field7;
                txtHField8.Value = _Entity.Field8;
                txtHField9.Value = _Entity.Field9;

                txtHAddedBy.Text = _Entity.AddedBy;
                txtHAddedDate.Text = _Entity.AddedDate;
                txtHLastEditedBy.Text = _Entity.LastEditedBy;
                txtHLastEditedDate.Text = _Entity.LastEditedDate;
                txtHSubmittedBy.Text = _Entity.SubmittedBy;
                txtHSubmittedDate.Text = _Entity.SubmittedDate;
                txtCancelledBy.Text = _Entity.CancelledBy;
                txtCancelledDate.Text = _Entity.CancelledDate;

                switch (Request.QueryString["entry"].ToString())
                {
                    case "N":
                        if (String.IsNullOrEmpty(_Entity.LastEditedBy))
                        {
                            updateBtn.Text = "Add";
                            frmlayout1.FindItemOrGroupByName("ReferenceTransaction").ClientVisible = false;
                        }
                        else
                        {
                            updateBtn.Text = "Update";
                            popup.ShowOnPageLoad = false;
                        }
                        break;
                    case "E":
                        updateBtn.Text = "Update";
                        edit = true;
                        break;
                    case "V":
                        view = true;//sets view mode for entry
                        glcheck.ClientVisible = false;
                        //txtDocnumber.Enabled = true;
                        //txtDocnumber.ReadOnly = false;
                        updateBtn.Text = "Close";
                        break;
                    case "D":
                        view = true;
                        updateBtn.Text = "Delete";
                        break;
                }

                gvRef.DataSourceID = "odsReference";
                this.gvRef.Columns["CommandString"].Width = 0;
                this.gvRef.Columns["RCommandString"].Width = 0;

                DataTable checkDetail = Gears.RetriveData2("Select DocNumber FROM Accounting.PropertyTransferDetail WHERE DocNumber = '" + txtDocNumber.Text + "'", Session["ConnString"].ToString());//ADD CONN
                gv1.DataSourceID = (checkDetail.Rows.Count > 0 ? "gv1ODS" : "gv1DS");

                HideNShow();
            }


        }


        #endregion

        #region Validation
        private void Validate()
        {
            GearsLibrary.Gears.GearsParameter gparam = new Gears.GearsParameter();
            gparam._Connection = Session["ConnString"].ToString();
            gparam._DocNo = _Entity.DocNumber;
            gparam._UserId = Session["Userid"].ToString();
            gparam._TransType = "ACTPRT";
            string strresult = GearsAccounting.GAccounting.PropertyTransfer_Validate(gparam);
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

        protected void SpinEdit_Load(object sender, EventArgs e)//Control for all numeric entries in header
        {
            ASPxSpinEdit spinedit = sender as ASPxSpinEdit;
            spinedit.ReadOnly = view;
        }
        protected void LookupLoad(object sender, EventArgs e)//Control for all lookup in header
        {
            ASPxGridLookup look = sender as ASPxGridLookup;
            look.DropDownButton.Enabled = !view;
            look.ReadOnly = view;
        }

        protected void ComboBoxLoad(object sender, EventArgs e)//Control for all lookup in header
        {
            ASPxComboBox combobox = sender as ASPxComboBox;
            combobox.DropDownButton.Enabled = !view;
            combobox.Enabled = !view;
        }

        protected void CostCenterLookupLoad(object sender, EventArgs e)
        {
            if (edit != false)
            {
                ASPxGridLookup look = sender as ASPxGridLookup;
                look.DropDownButton.Enabled = !edit;
                look.ReadOnly = edit;
            }
            else
            {
                ASPxGridLookup look = sender as ASPxGridLookup;
                look.DropDownButton.Enabled = !view;
                look.ReadOnly = view;
            }

        }

        protected void gvLookupLoad(object sender, EventArgs e)//Control for all lookup in details/grid
        {
            if (Request.QueryString["entry"].ToString() == "V" || Request.QueryString["entry"].ToString() == "D")
            {
                ASPxGridLookup look = sender as ASPxGridLookup;
                look.Enabled = false;
            }
        }

        protected void CheckBoxLoad(object sender, EventArgs e)
        {
            var check = sender as ASPxCheckBox;
            check.ReadOnly = view;
        }

        protected void gv1_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)//Control for grid
        {
            if (Request.QueryString["entry"].ToString() == "V" || Request.QueryString["entry"].ToString() == "D")
            {
                ASPxGridView grid = sender as ASPxGridView;
                grid.SettingsBehavior.AllowGroup = false;
                grid.SettingsBehavior.AllowSort = false;
                e.Editor.ReadOnly = view;
            }
        }
        protected void Date_Load(object sender, EventArgs e)//Control for all date editor
        {
            ASPxDateEdit date = sender as ASPxDateEdit;
            date.DropDownButton.Enabled = !view;
            date.ReadOnly = view;
        }


        protected void gv_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
        {
            var gridsender = sender as ASPxGridView;

            if (e.ButtonType == ColumnCommandButtonType.Delete)
            {
                e.Image.IconID = "actions_cancel_16x16";
            }
            if (e.ButtonType == ColumnCommandButtonType.Edit)
            {
                e.Image.IconID = "actions_addfile_16x16";
            }
            if (e.ButtonType == ColumnCommandButtonType.New)
            {
                e.Image.IconID = "actions_addfile_16x16";
            }


            if (Request.QueryString["entry"].ToString() == "V")
            {
                if (e.ButtonType == ColumnCommandButtonType.Edit || e.ButtonType == ColumnCommandButtonType.Delete ||
                    e.ButtonType == ColumnCommandButtonType.New || e.ButtonType == ColumnCommandButtonType.Cancel ||
                    e.ButtonType == ColumnCommandButtonType.Update)
                    e.Visible = false;
            }
            if (e.ButtonType == ColumnCommandButtonType.Update)
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

            _Entity.DocNumber = txtDocNumber.Text;
            _Entity.Connection = Session["ConnString"].ToString(); //ADD CONN
            _Entity.DocDate = dtDocDate.Text;
            //_Entity.PropertyNumber = glPropertyNumber.Text;
            _Entity.TransferType = cbTransferType.Text;


            //_Entity.ItemCode = txtItemCode.Text;
            //_Entity.FullDesc = txtFullDesc.Text;
            //_Entity.ColorCode = txtColorCode.Text;
            //_Entity.ClassCode = txtClassCode.Text;
            //_Entity.SizeCode = txtSizeCode.Text;
            //_Entity.Qty = Convert.ToDecimal(spinQty.Value);
            //_Entity.NewLocation = glNewLocation.Text;
            //_Entity.Location = txtLocation.Text;
            //_Entity.NewDepartment = glNewDepartment.Text;
            //_Entity.Department = txtDepartment.Text;
            //_Entity.NewAccountablePerson = glNewAccountablePerson.Text;
            //_Entity.AccountablePerson = txtAccountablePerson.Text;
            //_Entity.NewWarehouseCode = glNewWarehouseCode.Text;
            //_Entity.WarehouseCode = txtWarehouseCode.Text;



            _Entity.Field1 = txtHField1.Text;
            _Entity.Field2 = txtHField2.Text;
            _Entity.Field3 = txtHField3.Text;
            _Entity.Field4 = txtHField4.Text;
            _Entity.Field5 = txtHField5.Text;
            _Entity.Field6 = txtHField6.Text;
            _Entity.Field7 = txtHField7.Text;
            _Entity.Field8 = txtHField8.Text;
            _Entity.Field9 = txtHField9.Text;
            _Entity.LastEditedBy = Session["userid"].ToString();
            _Entity.LastEditedDate = DateTime.Now.ToString();


            switch (e.Parameter)
            {
                case "Add":
                case "Update":

                    strError = Functions.Submitted(_Entity.DocNumber, "Accounting.PropertyTransfer", 1, Session["ConnString"].ToString());//NEWADD factor 1 if submit, 2 if approve
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

                        gv1.DataSourceID = "gv1ODS";
                        gv1ODS.SelectParameters["DocNumber"].DefaultValue = txtDocNumber.Text;
                        gv1.UpdateEdit();

                        Validate();

                        if (e.Parameter == "Add")
                        {
                            cp.JSProperties["cp_message"] = "Successfully Added!";
                        }
                        else if (e.Parameter == "Update")
                        {
                            cp.JSProperties["cp_message"] = "Successfully Updated!";
                        }

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
                    cp.JSProperties["cp_message"] = "Successfully deleted!";
                    Session["Refresh"] = "1";
                    break;

                case "Close":
                    cp.JSProperties["cp_close"] = true;
                    Session["Refresh"] = "1";
                    break;

                case "PropertyNumberCase":
                    setText();
                    setEnables();
                    break;

                case "Transfer":
                    //Session["transfertype"] = "Transfer";
                    cbTransferType.SelectedIndex = 1;
                    //cbTransferType.ClientEnabled = true;
                    HideNShow();
                    break;
                case "Return To Warehouse":
                    //Session["transfertype"] = "Return To Warehouse";
                    cbTransferType.SelectedIndex = 0;
                    // cbTransferType.ClientEnabled = true;
                    HideNShow();
                    break;

            }
        }

        protected void setText()
        {
            //DataTable propertydata = Gears.RetriveData2("SELECT ItemCode,ColorCode,ClassCode,SizeCode,ISNULL(Qty,0) AS Qty,Location,Department,AssignedTo,Description,WarehouseCode,PropertyNumber FROM Accounting.AssetInv where PropertyNumber = '" + glPropertyNumber.Text + "'", Session["ConnString"].ToString());//ADD CONN
            //foreach (DataRow dt in propertydata.Rows)
            //{
            //    txtItemCode.Value = dt[0].ToString();
            //    txtColorCode.Value = dt[1].ToString();
            //    txtClassCode.Value = dt[2].ToString();
            //    txtSizeCode.Value = dt[3].ToString();
            //    spinQty.Value = dt[4].ToString();
            //    txtLocation.Value = dt[5].ToString();
            //    txtDepartment.Value = dt[6].ToString();
            //    txtAccountablePerson.Value = dt[7].ToString();
            //    txtFullDesc.Value = dt[8].ToString();
            //    txtWarehouseCode.Value = dt[9].ToString();
            //}


            //cbTransferType.Text = "";
            //glNewLocation.Text = "";
            //glNewDepartment.Text = "";
            //glNewAccountablePerson.Text = "";
            //glNewWarehouseCode.Text = "";
        }


        protected void setEnables()
        {
            //if (!String.IsNullOrEmpty(glPropertyNumber.Text))
            //{
            //    cbTransferType.ClientEnabled = true;
            //    if (cbTransferType.Text == "Return To Warehouse")
            //        WarehouseEnables();
            //    else if (cbTransferType.Text == "Transfer")
            //        TransferEnables();
            //    else
            //    {
            //        glNewLocation.ClientEnabled = false;
            //        glNewDepartment.ClientEnabled = false;
            //        glNewAccountablePerson.ClientEnabled = false;
            //        glNewWarehouseCode.ClientEnabled = false;
            //    }
            //}
            //else
            //{
            //    cbTransferType.ClientEnabled = false;
            //    txtItemCode.Text = "";
            //    txtColorCode.Text = "";
            //    txtClassCode.Text = "";
            //    txtSizeCode.Text = "";
            //    txtFullDesc.Text = "";
            //    spinQty.Text = "";
            //    txtLocation.Text = "";
            //    txtDepartment.Text = "";
            //    txtAccountablePerson.Text = "";
            //    txtWarehouseCode.Text = "";


            //    glNewLocation.ClientEnabled = false;
            //    glNewDepartment.ClientEnabled = false;
            //    glNewAccountablePerson.ClientEnabled = false;
            //    glNewWarehouseCode.ClientEnabled = false;
            //}
        }

        protected void dtpDocDate_Init(object sender, EventArgs e)
        {
            if (Request.QueryString["entry"] == "N")
            {
                dtDocDate.Date = DateTime.Now;
            }
        }

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
                //e.DeleteValues.Clear();
                e.InsertValues.Clear();
                e.UpdateValues.Clear();

            }
        }
        #endregion

        public void HideNShow()
        {
            if (cbTransferType.SelectedIndex == 1)
            {
                gv1.Columns["NewLocation"].Visible = true;
                gv1.Columns["NewAccountablePerson"].Visible = true;
                gv1.Columns["NewDepartment"].Visible = true;
                gv1.Columns["NewWarehouseCode"].Visible = false;
            }
            else
            {
                gv1.Columns["NewLocation"].Visible = false;
                gv1.Columns["NewAccountablePerson"].Visible = false;
                gv1.Columns["NewDepartment"].Visible = false;
                gv1.Columns["NewWarehouseCode"].Visible = true;
            }
        }

        protected void Connection_Init(object sender, EventArgs e)
        {
            ((SqlDataSource)sender).ConnectionString = Session["ConnString"].ToString();
        }

        protected void glPropertyNumber_Init(object sender, EventArgs e)
        {
            ASPxGridLookup gridLookup = sender as ASPxGridLookup;
            gridLookup.GridView.CustomCallback += new ASPxGridViewCustomCallbackEventHandler(gridView_CustomCallback);
            if (Session["ProTranFilterExpression"] != null)
            {
                gridLookup.GridView.DataSourceID = "PropertyNumberLookup";
                PropertyNumberLookup.FilterExpression = Session["ProTranFilterExpression"].ToString();
                //Session["ProTranFilterExpression"] = null;
            }
            else
            {
                gridLookup.GridView.DataSourceID = "PropertyNumberLookup";
            }
        }

        public void gridView_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            string column = e.Parameters.Split('|')[0];//Set column name
            if (column.Contains("GLP_AIC") || column.Contains("GLP_AC") || column.Contains("GLP_F")) return;//Traps the callback
            string propertynumber = e.Parameters.Split('|')[1];//Set Item Code
            string val = e.Parameters.Split('|')[2];//Set column value
            if (val.Contains("GLP_AIC") || val.Contains("GLP_AC") || val.Contains("GLP_F")) return;//Traps the callback
            var propertynumberlookup = sender as ASPxGridView;
            string codes = "";

            if (e.Parameters.Contains("PropertyNumber"))
            {
                DataTable countcolor = Gears.RetriveData2("SELECT ItemCode, Description, ColorCode, ClassCode, SizeCode, AccumulatedCostCenter, DepreciationCostCenter, Qty, Location, Department, AssignedTo, WarehouseCode FROM Accounting.AssetInv WHERE propertynumber = '" + propertynumber + "'", Session["ConnString"].ToString());//ADD CONN

                foreach (DataRow dt in countcolor.Rows)
                {
                    codes = dt["ItemCode"].ToString() + ";";
                    codes += dt["Description"].ToString() + ";";
                    codes += dt["ColorCode"].ToString() + ";";
                    codes += dt["ClassCode"].ToString() + ";";
                    codes += dt["SizeCode"].ToString() + ";";
                    codes += dt["AccumulatedCostCenter"].ToString() + ";";
                    codes += dt["DepreciationCostCenter"].ToString() + ";";
                    codes += dt["Qty"].ToString() + ";";
                    codes += dt["Location"].ToString() + ";";
                    codes += dt["Department"].ToString() + ";";
                    codes += dt["AssignedTo"].ToString() + ";";
                    codes += dt["WarehouseCode"].ToString() + ";";

                }


                propertynumberlookup.JSProperties["cp_identifier"] = "PropertyNumber";
                propertynumberlookup.JSProperties["cp_codes"] = codes;
            }

            //else if (e.Parameters.Contains("CheckPNumber"))
            //{
            //    ASPxGridView grid = sender as ASPxGridView;
            //    string[] transdoc = propertynumber.Split(';');
            //    var selectedValues = transdoc;
            //    CriteriaOperator selectionCriteria = new InOperator("PropertyNumber", transdoc);
            //    CriteriaOperator not = new NotOperator(selectionCriteria);
            //    PropertyNumberLookup.FilterExpression = (GroupOperator.Combine(GroupOperatorType.And, not)).ToString();
            //    Session["ProTranFilterExpression"] = PropertyNumberLookup.FilterExpression;
            //    grid.DataSourceID = "PropertyNumberLookup";
            //    grid.DataBind();

            //    for (int i = 0; i < grid.VisibleRowCount; i++)
            //    {
            //        if (grid.GetRowValues(i, "PropertyNumber") != null)
            //            if (grid.GetRowValues(i, "PropertyNumber").ToString() == val)
            //            {
            //                grid.Selection.SelectRow(i);
            //                string key = grid.GetRowValues(i, "PropertyNumber").ToString();
            //                grid.MakeRowVisible(key);
            //                break;
            //            }
            //    }
            //}


            //else if (e.Parameters.Contains("VATCode"))
            //{
            //    DataTable vat = Gears.RetriveData2("SELECT DISTINCT ISNULL(Rate,1) AS Rate FROM Masterfile.Tax WHERE TCode = '" + propertynumber + "'", Session["ConnString"].ToString());//ADD CONN

            //    if (vat.Rows.Count > 0)
            //    {
            //        foreach (DataRow dt in vat.Rows)
            //        {
            //            codes = dt["Rate"].ToString();
            //        }
            //    }

            //    propertynumberlookup.JSProperties["cp_identifier"] = "VAT";
            //    propertynumberlookup.JSProperties["cp_codes"] = Convert.ToDecimal(codes) + ";";
            //}
        }

    }
}