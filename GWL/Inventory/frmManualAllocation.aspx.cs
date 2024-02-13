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
    public partial class frmManualAllocation : System.Web.UI.Page
    {
        Boolean error = false;//Boolean for grid validation
        Boolean view = false;//Boolean for view state
        Boolean check = false;//Boolean for grid validation
        string filter = "";

        private static string Connection;

        Entity.ManualAllocation _Entity = new ManualAllocation();//Calls entity odsHeader
        Entity.ManualAllocation.ManualAllocationDetail _EntityDetail = new ManualAllocation.ManualAllocationDetail();//Call entity sdsDetail

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

            if (!IsPostBack)
            {
                Connection = Session["ConnString"].ToString();
                Session["FilterItems"] = null;
                //WordStatus();
                if (Request.QueryString["entry"].ToString() == "N")
                {
                    gv1.DataSourceID = null;
                    popup.ShowOnPageLoad = false;
                    gv1.KeyFieldName = "LineNumber";
                    //dtpCancellationDate.Value = DateTime.Now;
                    //dtpDeliveryDate.Value = DateTime.Now;
                }
                _Entity.getdata(txtDocNumber.Text, Session["ConnString"].ToString());//ADD CONN
                dtpDocDate.Date = String.IsNullOrEmpty(_Entity.DocDate) ? DateTime.Now : Convert.ToDateTime(_Entity.DocDate);
                //dtpDeliveryDate.Date = String.IsNullOrEmpty(_Entity.DeliveryDate) ? DateTime.Now : Convert.ToDateTime(_Entity.DeliveryDate);
                //dtpCancellationDate.Date = String.IsNullOrEmpty(_Entity.CancellationDate) ? DateTime.Now : Convert.ToDateTime(_Entity.CancellationDate);
                glCustomer.Value = _Entity.Customer;
                memRemarks.Value = _Entity.Remarks;
                //txtStatus.Value = _Entity.Status;
                //txtDRNumber.Value = _Entity.DRNumber;
                txtTotalQty.Value = _Entity.TotalQty;
                //.Value = _Entity.TotalAmount;
                //glWarehouseCode.Value = _Entity.Warehouse;

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
                txtHCancelledBy.Text = _Entity.CancelledBy;
                txtHCancelledDate.Text = _Entity.CancelledDate;

                gv1.KeyFieldName = "LineNumber";

                frmlayout1.FindItemOrGroupByName("ReferenceTransaction").ClientVisible = false;

                updateBtn.Text = "Add";
                if (!string.IsNullOrEmpty(_Entity.LastEditedBy))
                {
                    updateBtn.Text = "Update";
                    frmlayout1.FindItemOrGroupByName("ReferenceTransaction").ClientVisible = true;
                }
                switch (Request.QueryString["entry"].ToString())
                {
                    case "N":
                        //updateBtn.Text = "Add";
                        break;
                    case "E":
                        updateBtn.Text = "Update";
                        frmlayout1.FindItemOrGroupByName("ReferenceTransaction").ClientVisible = true;
                        break;
                    case "V":
                        view = true;//sets view mode for entry
                        updateBtn.Text = "Close";
                        frmlayout1.FindItemOrGroupByName("ReferenceTransaction").ClientVisible = true;
                        glcheck.ClientVisible = false;
                        break;
                    case "D":
                        view = true;
                        frmlayout1.FindItemOrGroupByName("ReferenceTransaction").ClientVisible = true;
                        updateBtn.Text = "Delete";
                        break;
                }

                gvRef.DataSourceID = "odsReference";
                this.gvRef.Columns["CommandString"].Width = 0;
                this.gvRef.Columns["RCommandString"].Width = 0;

                DataTable dtbldetail = Gears.RetriveData2("SELECT DISTINCT DocNumber FROM Inventory.ManualAllocationDetail WHERE DocNumber ='" + txtDocNumber.Text + "'", Session["ConnString"].ToString());//ADD CONN
                gv1.DataSourceID = (dtbldetail.Rows.Count > 0 ? "odsDetail" : "sdsDetail");
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
            gparam._TransType = Request.QueryString["transtype"].ToString();
            string strresult = GearsInventory.GInventory.ManualAllocation_Validate(gparam);
            if (strresult != " ")
            {
                cp.JSProperties["cp_valmsg"] = strresult;//Message variable to client side
            }
        }
        #endregion

        #region Posting
        private void Post()
        {
            GearsLibrary.Gears.GearsParameter gparam = new Gears.GearsParameter();
            gparam._Connection = Session["ConnString"].ToString();
            gparam._DocNo = _Entity.DocNumber;
            gparam._UserId = Session["Userid"].ToString();
            gparam._TransType = Request.QueryString["transtype"].ToString();
            gparam._Table = "Inventory.ManualAllocation";
            gparam._Factor = -1;
            string strresult = GearsInventory.GInventory.ManualAllocation_Post(gparam);
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
        protected void MemoLoad(object sender, EventArgs e)
        {
            var memo = sender as ASPxMemo;
            memo.ReadOnly = view;
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
        protected void gv1_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)//Control for grid
        {}

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
            //Sets icon for grid/Control for Enabling/Disabling of buttons on grid.
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
            {
                e.Visible = false;
            }
            if (Request.QueryString["entry"] != "N" || Request.QueryString["entry"] != "E")
            {
                if (e.ButtonType == ColumnCommandButtonType.Edit ||
                    e.ButtonType == ColumnCommandButtonType.Cancel)
                    e.Visible = false;
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
            //if (e.Parameters.Contains("ItemCode"))
            //{
            //    DataTable countcolor = Gears.RetriveData2("Select c.ColorCode,c.ClassCode,c.SizeCode,UnitBase,A.FullDesc, A.UnitBulk, A.IsByBulk, A.UpdatedPrice AS Price "
            //                                            + " from masterfile.item a left join masterfile.ItemDetail c on a.ItemCode = c.ItemCode left join masterfile.ItemCustomerPrice b "
            //                                            + " on A.ItemCode = B.ItemCode where a.itemcode = '" + itemcode + "'", Session["ConnString"].ToString());//ADD CONN

            //    foreach (DataRow dt in countcolor.Rows)
            //    {
            //        codes = dt["ColorCode"].ToString() + ";";
            //        codes += dt["ClassCode"].ToString() + ";";
            //        codes += dt["SizeCode"].ToString() + ";";
            //        codes += dt["UnitBase"].ToString() + ";";
            //        codes += dt["UnitBulk"].ToString() + ";";
            //        codes += dt["FullDesc"].ToString() + ";";
            //        codes += dt["IsByBulk"].ToString() + ";";
            //        codes += dt["Price"].ToString() + ";";
            //    }
            //    itemlookup.JSProperties["cp_codes"] = codes;
            //}
            //else
            //{
            //    ASPxGridView grid = sender as ASPxGridView;
            //    //ASPxGridLookup lookup = (ASPxGridLookup)gv1.FindEditRowCellTemplateControl((GridViewDataColumn)gv1.Columns[3], "glItemCode");
            //    var selectedValues = itemcode;
            //    CriteriaOperator selectionCriteria = new InOperator("ItemCode", new string[] { itemcode });
            //    sdsItemDetail.FilterExpression = (GroupOperator.Combine(GroupOperatorType.And, selectionCriteria)).ToString();
            //    Session["FilterExpression"] = sdsItemDetail.FilterExpression;
            //    grid.DataSourceID = "sdsItemDetail";
            //    grid.DataBind();

            //    for (int i = 0; i < grid.VisibleRowCount; i++)
            //    {
            //        if (grid.GetRowValues(i, column) != null)
            //            if (grid.GetRowValues(i, column).ToString() == val)
            //            {
            //                grid.Selection.SelectRow(i);
            //                string key = grid.GetRowValues(i, column).ToString();
            //                grid.MakeRowVisible(key);
            //                break;
            //            }
            //    }
            //}


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
            _Entity.Transaction = Request.QueryString["transtype"].ToString();
            _Entity.Connection = Session["ConnString"].ToString(); //ADD CONN
            _Entity.DocNumber = txtDocNumber.Value.ToString();
            _Entity.DocDate = dtpDocDate.Text;
            //_Entity.DeliveryDate = dtpDeliveryDate.Text;
            //_Entity.CancellationDate = dtpCancellationDate.Text;
            _Entity.Customer = String.IsNullOrEmpty(glCustomer.Text) ? null : glCustomer.Text;
            _Entity.Remarks = String.IsNullOrEmpty(memRemarks.Text) ? null : memRemarks.Text;
            //_Entity.DRNumber = String.IsNullOrEmpty(txtDRNumber.Text) ? null : txtDRNumber.Text;
            _Entity.TotalQty = String.IsNullOrEmpty(txtTotalQty.Text) ? 0 : Convert.ToDecimal(txtTotalQty.Value.ToString());
            //_Entity.TotalAmount = String.IsNullOrEmpty(txtTotalAmount.Text) ? 0 : Convert.ToDecimal(txtTotalAmount.Value.ToString());
            //_Entity.Warehouse = String.IsNullOrEmpty(glWarehouseCode.Text) ? null : glWarehouseCode.Text;
            
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

            _Entity.Transaction = Request.QueryString["transtype"].ToString();

            DataTable LastEditedCheck = new DataTable();
            LastEditedCheck = Gears.RetriveData2("SELECT LastEditedDate AS LastEdited FROM Inventory.ManualAllocation WHERE DocNumber = '" + _Entity.DocNumber + "'", Session["ConnString"].ToString());



            switch (e.Parameter)
            {
                case "Add":

                    if (txtHLastEditedDate.Text != (LastEditedCheck.Rows[0]["LastEdited"].ToString() == "1/1/1900 12:00:00 AM" || String.IsNullOrEmpty(LastEditedCheck.Rows[0]["LastEdited"].ToString()) ? "" : LastEditedCheck.Rows[0]["LastEdited"].ToString()))
                    {
                        cp.JSProperties["cp_message"] = "The transaction has been modified by other user(s)." + Environment.NewLine + "Changes you made will not be saved!";
                        cp.JSProperties["cp_success"] = true;
                        cp.JSProperties["cp_forceclose"] = true;
                        return;
                    }


                    if (error == false)
                    {
                        check = true;
                        _Entity.LastEditedBy = Session["userid"].ToString();
                        _Entity.UpdateData(_Entity);//Method of inserting for header
                        gv1.DataSourceID = "odsDetail";//Renew datasourceID to entity
                        odsDetail.SelectParameters["DocNumber"].DefaultValue = txtDocNumber.Text;//Set select parameter to prevent error
                        gv1.UpdateEdit();//2nd initiation to insert grid
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
                    
                    string strError = Functions.Submitted(_Entity.DocNumber,"Inventory.ManualAllocation",1,Connection);//NEWADD factor 1 if submit, 2 if approve
                        if (!string.IsNullOrEmpty(strError))
                        {
                            cp.JSProperties["cp_message"] = strError;
                            cp.JSProperties["cp_success"] = true;
                            cp.JSProperties["cp_forceclose"] = true;
                            return;
                        }


                        if (txtHLastEditedDate.Text != (LastEditedCheck.Rows[0]["LastEdited"].ToString() == "1/1/1900 12:00:00 AM" || String.IsNullOrEmpty(LastEditedCheck.Rows[0]["LastEdited"].ToString()) ? "" : LastEditedCheck.Rows[0]["LastEdited"].ToString()))
                        {
                            cp.JSProperties["cp_message"] = "The transaction has been modified by other user(s)." + Environment.NewLine + "Changes you made will not be saved!";
                            cp.JSProperties["cp_success"] = true;
                            cp.JSProperties["cp_forceclose"] = true;
                            return;
                        }



                    if (error == false)
                    {
                        check = true;
                        _Entity.LastEditedBy = Session["userid"].ToString();
                        _Entity.UpdateData(_Entity);//Method of inserting for header
                            gv1.DataSourceID = "odsDetail";//Renew datasourceID to entity
                            odsDetail.SelectParameters["DocNumber"].DefaultValue = txtDocNumber.Text;//Set select parameter to prevent error
                            gv1.UpdateEdit();//2nd initiation to insert grid
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
                //case "IssuedNumber":
                //    gv1.KeyFieldName = "LineNumber";
                //    SqlDataSource ds = sdsIssuanceNumber;
                //    ds.SelectCommand = string.Format("select DocNumber, IssuedTo, WarehouseCode, ISNULL(ReqJONumber,'N/A') AS ReqJONumber, ISNULL(ReqJOStep,'N/A') AS ReqJONumber, CostCenter from Inventory.Issuance WHERE ISNULL(SubmittedBy,'') != '' AND DocNumber = '" + glIssuanceNumber.Text + "'");
                //    DataView inb = (DataView)ds.Select(DataSourceSelectArguments.Empty);
                //    if (inb.Count > 0)
                //    {
                //        txtReturnedBy.Text = inb[0][1].ToString();
                //        glWarehouseCode.Text = inb[0][2].ToString();
                //        txtJONumber.Text = inb[0][3].ToString();
                //        txtJOStep.Text = inb[0][4].ToString();
                //        txtCostCenter.Text = inb[0][5].ToString();
                //    }
                //    cp.JSProperties["cp_generated"] = true;
                //    break;
            }
        }        
        protected void grid_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        {}

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
        }
        #endregion

        protected void gv1_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["referencedetail"] = null;
            }
            if (Session["referencedetail"] != null)
            {
                gv1.DataSourceID = sdsRefIssuanceDetail.ID;
                sdsRefIssuanceDetail.FilterExpression = Session["referencedetail"].ToString();
            }
        }

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
            gridLookup.DataBind();
        }

        //protected void WordStatus()
        //{
        //    DataTable mod = new DataTable();
        //    mod = Gears.RetriveData2("SELECT Status FROM Inventory.ManualAllocation WHERE DocNumber = '" + Request.QueryString["docnumber"].ToString() + "'", Session["ConnString"].ToString());//ADD CONN

        //    foreach (DataRow md in mod.Rows)
        //    {
        //        switch (md["Status"].ToString())
        //        {
        //            case "N":
        //                txtStatus.Text = "New";
        //                break;
        //            case "C":
        //                txtStatus.Text = "Closed";
        //                break;
        //            case "X":
        //                txtStatus.Text = "ManualClosed";
        //                break;
        //            case "L":
        //                txtStatus.Text = "Cancelled";
        //                break;
        //            case "P":
        //                txtStatus.Text = "Partial";
        //                break;
        //        }
        //    }
        //}

        public void item_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            if (e.Parameters.Contains("GLP_AIC") || e.Parameters.Contains("GLP_AC") || e.Parameters.Contains("GLP_F")) return;
            ASPxGridView grid = sender as ASPxGridView;
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
    }
}