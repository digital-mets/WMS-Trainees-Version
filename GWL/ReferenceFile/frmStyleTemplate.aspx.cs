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
    public partial class frmStyleTemplate : System.Web.UI.Page
    {
        Boolean error = false;//Boolean for grid validation
        Boolean view = false;//Boolean for view state
        Boolean check = false;//Boolean for grid validation
        Boolean edit = false; //Bolean for edit state
        Boolean dateValidation = false;

        private static string strError;

        Entity.StyleTemplate _Entity = new StyleTemplate();//Calls entity ICN
        Entity.StyleTemplate.StyleTemplateDetail _EntityDetail = new StyleTemplate.StyleTemplateDetail();//Call entity POdetails

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

            if (referer == null)
            {
                Response.Redirect("~/error.aspx");
            }
            chkIsInactive.Enabled = false;

            //Details checking. changes datasourceid of gridview if there's no details for a certain docnumber; to enable adding of details.
            if (Request.QueryString["entry"].ToString() == "V")
            {
                view = true;//sets view mode for entry
            }

            if (!IsPostBack)
            {
                Session["RateCode"] = null;
                Session["GLRateCode"] = null;

                switch (Request.QueryString["entry"].ToString())
                {
                    case "N":
                        updateBtn.Text = "Add";

                        break;
                    case "E":
                        updateBtn.Text = "Update";

                        break;
                    case "V":
                        view = true;//sets view mode for entry
                        updateBtn.Text = "Close";
                        glcheck.ClientVisible = false;
                        break;
                    case "D":
                        view = true;//sets view mode for entry
                        updateBtn.Text = "Delete";
                        glcheck.ClientVisible = false;
                        break;
                }


                //txtDocnumber.Value = Session["DocNumber"].ToString(); //sets docnumber from session


                //if (Request.QueryString["entry"].ToString() == "N")
                //{


                //}
                //else
                //{
                //gvBiz.ReadOnly = true;
                if (Request.QueryString["entry"].ToString() != "N")
                {

                    txtStyleCode.Value = Request.QueryString["docnumber"].ToString();

                    _Entity.getdata(txtStyleCode.Text, Session["ConnString"].ToString());//ADD CONN
                    //_Entity.getdata(gvBiz.Text);

                    // txtOHCode.Text = _Entity.OHCode;
                    //txtOverheadCost.Text = _Entity.OverheadCost.ToString();
                    //txtOverheadType.Text = _Entity.OverheadType;
                    txtDesc.Text = _Entity.Description;
                    
                    chkIsInactive.Value = _Entity.IsInActive;
                    txtActivatedDate.Text = _Entity.ActivatedDate;
                    txtAddedDate.Text = _Entity.AddedDate;
                    txtLastEditedDate.Text = _Entity.LastEditedDate;
                    txtDeActivatedDate.Text = _Entity.DeActivatedDate;
                    txtDeActivatedBy.Text = _Entity.DeActivatedBy;
                    txtActivatedBy.Text = _Entity.ActivatedBy;
                    txtAddedBy.Text = _Entity.AddedBy;
                    txtLastEditedBy.Text = _Entity.LastEditedBy;
                    txtHField1.Text = _Entity.Field1;
                    txtHField2.Text = _Entity.Field2;
                    txtHField3.Text = _Entity.Field3;
                    txtHField4.Text = _Entity.Field4;
                    txtHField5.Text = _Entity.Field5;
                    txtHField6.Text = _Entity.Field6;
                    txtHField7.Text = _Entity.Field7;
                    txtHField8.Text = _Entity.Field8;
                    txtHField9.Text = _Entity.Field9;

                    odsDetail.SelectParameters["Trans"].DefaultValue = txtStyleCode.Text;

                }


                //Details checking. changes datasourceid of gridview if there's no details for a certain docnumber; to enable adding of details.
                DataTable checkCount = Gears.RetriveData2("Select StyleCode from masterfile.StyleTemplateDetail where StyleCode = '" + txtStyleCode.Text + "'", Session["ConnString"].ToString());
                if (checkCount.Rows.Count > 0)
                {
                    gv1.KeyFieldName = "StyleCode;LineNumber";
                    gv1.DataSourceID = "odsDetail";
                }
                else
                {
                    gv1.KeyFieldName = "StyleCode;LineNumber";
                    gv1.DataSourceID = "sdsDetail";
                }

            }
        }
        #endregion

        #region Validation
        ////private void Validate()
        ////{
        ////    GearsLibrary.Gears.GearsParameter gparam = new Gears.GearsParameter();
        ////    gparam._Connection = Session["ConnString"].ToString();
        ////    gparam._DocNo = _Entity.DocNumber;
        ////    gparam._UserId = Session["Userid"].ToString();
        ////    gparam._TransType = "PRCPRM";

        ////    string strresult = GearsProcurement.GProcurement.PurchaseRequest_Validate(gparam);

        ////    cp.JSProperties["cp_valmsg"] = strresult;//Message variable to client side
            
        ////}
        #endregion

        #region Set controls' state/behavior/etc...
        protected void TextboxLoad(object sender, EventArgs e)//Control for all textbox
        {
            ASPxTextBox text = sender as ASPxTextBox;
            text.ReadOnly = view;
        }

        protected void MemoLoad(object sender, EventArgs e)//Control for all textbox
        {
            ASPxMemo text = sender as ASPxMemo;
            text.ReadOnly = view;
        }
        protected void LookupLoad(object sender, EventArgs e)//Control for all lookup in header
        {

            ASPxGridLookup look = sender as ASPxGridLookup;
            look.DropDownButton.Enabled = !view;
            look.ReadOnly = view;
            //var look = sender as ASPxGridLookup;
            //if (look != null)
            //{
            //    look.ReadOnly = view;
            //}
        }

        protected void CostCenterLookupLoad(object sender, EventArgs e)
        {
            if(edit != false)
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
            //if (Request.QueryString["entry"].ToString() == "V" || Request.QueryString["entry"].ToString() == "D")
            //{
            //    ASPxGridView grid = sender as ASPxGridView;
            //    grid.SettingsBehavior.AllowGroup = false;
            //    grid.SettingsBehavior.AllowSort = false;
            //    e.Editor.ReadOnly = view;
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
                if (e.ButtonType == ColumnCommandButtonType.Edit)
                {
                    e.Image.IconID = "actions_addfile_16x16";
                }
                if (e.ButtonType == ColumnCommandButtonType.New)
                {
                    e.Image.IconID = "actions_addfile_16x16";
                }
                if (Request.QueryString["entry"].ToString() == "V" || Request.QueryString["entry"].ToString() == "D")
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

        #region Lookup Settings
        protected void lookup_Init(object sender, EventArgs e)
        {
            ASPxGridLookup gridLookup = sender as ASPxGridLookup;
            gridLookup.GridView.CustomCallback += new ASPxGridViewCustomCallbackEventHandler(gridView_CustomCallback);
            if (Session["FilterExpression"] != null)
            {
                gridLookup.GridView.DataSourceID = "Masterfileitemdetail";
                Masterfileitemdetail.FilterExpression = Session["FilterExpression"].ToString();
                //Session["FilterExpression"] = null;
            }
            else
            {
                gridLookup.GridView.DataSourceID = "Masterfileitemdetail";
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
                //Get Distinct Color Code
                        DataTable getColor = Gears.RetriveData2("Select DISTINCT ColorCode FROM masterfile.item a " +
                                                                  "left join masterfile.itemdetail b on a.itemcode = b.itemcode where a.itemcode = '" + itemcode + "'", Session["ConnString"].ToString());//ADD CONN
                
                            if (getColor.Rows.Count > 1)
                            {
                                codes = "" + ";";
                            }
                            else
                            {
                                foreach (DataRow dt in getColor.Rows)
                                {
                                    codes = dt["ColorCode"].ToString() + ";";
                                }
                            }
                    
                        DataTable getClass = Gears.RetriveData2("Select DISTINCT ClassCode FROM masterfile.item a " +
                                                              "left join masterfile.itemdetail b on a.itemcode = b.itemcode where a.itemcode = '" + itemcode + "'", Session["ConnString"].ToString());//ADD CONN

                            if (getClass.Rows.Count > 1)
                            {
                                codes += "" + ";";
                            }
                            else
                            {
                                foreach (DataRow dt in getClass.Rows)
                                {
                                    codes += dt["ClassCode"].ToString() + ";";
                                }
                            }

                        DataTable getSize = Gears.RetriveData2("Select DISTINCT SizeCode FROM masterfile.item a " +
                                                                         "left join masterfile.itemdetail b on a.itemcode = b.itemcode where a.itemcode = '" + itemcode + "'", Session["ConnString"].ToString());//ADD CONN

                            if (getSize.Rows.Count > 1)
                            {
                                codes += "" + ";";
                            }
                            else
                            {
                                foreach (DataRow dt in getSize.Rows)
                                {
                                    codes += dt["SizeCode"].ToString() + ";";
                                }
                            }

                            //DataTable getUnit = Gears.RetriveData2("Select DISTINCT UnitBase FROM masterfile.item a " +
                            //                                                 "left join masterfile.itemdetail b on a.itemcode = b.itemcode where a.itemcode = '" + itemcode + "'", Session["ConnString"].ToString());//ADD CONN

                            //if (getUnit.Rows.Count > 1)
                            //{
                            //    codes += "" + ";";
                            //}
                            //else
                            //{
                            //    foreach (DataRow dt in getUnit.Rows)
                            //    {
                            //        codes += dt["UnitBase"].ToString() + ";";
                            //    }
                            //}
          

                itemlookup.JSProperties["cp_codes"] = codes;
            }
            else
            {



                if (e.Parameters.Contains("ColorCode"))
                {
                    Masterfileitemdetail.SelectCommand = "SELECT DISTINCT ItemCode, ColorCode FROM Masterfile.ItemDetail where ItemCode = '" + itemcode + "'";
                }
                else if (e.Parameters.Contains("ClassCode"))
                {
                    Masterfileitemdetail.SelectCommand = "SELECT DISTINCT ItemCode, ClassCode FROM Masterfile.ItemDetail where ItemCode = '" + itemcode + "'";
                    //Masterfileitemdetail.SelectCommand = "SELECT DISTINCT ItemCode, '' AS ColorCode, ClassCode, '' AS SizeCode FROM Masterfile.ItemDetail where ItemCode = '" + itemcode + "'";
                }
                else if (e.Parameters.Contains("SizeCode"))
                {
                    Masterfileitemdetail.SelectCommand = "SELECT DISTINCT ItemCode, SizeCode FROM Masterfile.ItemDetail where ItemCode = '" + itemcode + "'";
                    //Masterfileitemdetail.SelectCommand = "SELECT DISTINCT ItemCode, '' AS [ColorCode], '' AS [ClassCode], [SizeCode] FROM Masterfile.ItemDetail where ItemCode = '" + itemcode + "'";
                }
                else if (e.Parameters.Contains("UnitBase"))
                {
                    Masterfileitemdetail.SelectCommand = "SELECT DISTINCT ItemCode, UnitBase FROM Masterfile.Item where ItemCode = '" + itemcode + "'";
                }


                ASPxGridView grid = sender as ASPxGridView;
                ASPxGridLookup lookup = (ASPxGridLookup)gv1.FindEditRowCellTemplateControl((GridViewDataColumn)gv1.Columns[1], "glItemCode");
                var selectedValues = itemcode;
                CriteriaOperator selectionCriteria = new InOperator("ItemCode", new string[] { itemcode });
                Masterfileitemdetail.FilterExpression = (GroupOperator.Combine(GroupOperatorType.And, selectionCriteria)).ToString();
                Session["FilterExpression"] = Masterfileitemdetail.FilterExpression;
                grid.DataSourceID = "Masterfileitemdetail";
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
            _Entity.Connection = Session["ConnString"].ToString(); //ADD CONN
            _Entity.StyleCode = txtStyleCode.Text;
            _Entity.Description = txtDesc.Text;
            //_Entity.OverheadCost = Convert.ToDecimal(txtOverheadCost.Text);
            //_Entity.OverheadType = txtOverheadType.Text;
            //     _Entity.SizeType = String.IsNullOrEmpty(cboType.Text) ? null : cboType.Value.ToString();

            _Entity.IsInActive = Convert.ToBoolean(chkIsInactive.Checked);

            _Entity.LastEditedBy = Session["userid"].ToString();
            _Entity.LastEditedDate = DateTime.Now.ToString();
            _Entity.AddedBy = Session["userid"].ToString();
            _Entity.AddedDate = DateTime.Now.ToString();

            _Entity.Field1 = txtHField1.Text;
            _Entity.Field2 = txtHField2.Text;
            _Entity.Field3 = txtHField3.Text;
            _Entity.Field4 = txtHField4.Text;
            _Entity.Field5 = txtHField5.Text;
            _Entity.Field6 = txtHField6.Text;
            _Entity.Field7 = txtHField7.Text;
            _Entity.Field8 = txtHField8.Text;
            _Entity.Field9 = txtHField9.Text;



            switch (e.Parameter)
            {
                case "Add":

                    if (error == false)
                    {
                        check = true;
                        //     _Entity.InsertData(_Entity);//Method of inserting for header
                        _Entity.AddedBy = Session["userid"].ToString();

                        _Entity.InsertData(_Entity);
                        gv1.DataSourceID = "odsDetail";
                        odsDetail.SelectParameters["Trans"].DefaultValue = txtStyleCode.Text;
                        gv1.UpdateEdit();
                        //Validate();
                        cp.JSProperties["cp_message"] = "Successfully Added!";//Message variable to client side
                        cp.JSProperties["cp_success"] = true;//Success bool variable  to client side
                        cp.JSProperties["cp_close"] = true;//Close window variable to client side
                        Session["Refresh"] = "1";
                    }
                    else
                    {
                        cp.JSProperties["cp_message"] = "Please check all the fields!";
                        cp.JSProperties["cp_success"] = true;
                    }

                    break;

                case "Update":


                    if (error == false)
                    {
                        check = true;
                        //     _Entity.InsertData(_Entity);//Method of inserting for header
                        _Entity.AddedBy = Session["userid"].ToString();

                        _Entity.UpdateData(_Entity);
                        gv1.DataSourceID = "odsDetail";
                        odsDetail.SelectParameters["Trans"].DefaultValue = txtStyleCode.Text;
                        gv1.UpdateEdit();
                        //Validate();
                        cp.JSProperties["cp_message"] = "Successfully Updated!";//Message variable to client side
                        cp.JSProperties["cp_success"] = true;//Success bool variable  to client side
                        cp.JSProperties["cp_close"] = true;//Close window variable to client side
                        Session["Refresh"] = "1";
                    }
                    else
                    {
                        cp.JSProperties["cp_message"] = "Please check all the fields!";
                        cp.JSProperties["cp_success"] = true;
                    }

                    break;
                case "Delete":
                    check = true;
                    cp.JSProperties["cp_delete"] = true;
                    _Entity.DeleteData(_Entity);
                    cp.JSProperties["cp_message"] = "Successfully Deleted!";
                    cp.JSProperties["cp_success"] = true;
                    cp.JSProperties["cp_close"] = true;
                    Session["Refresh"] = "1";
                    break;
                case "ConfDelete":
                    _Entity.DeleteData(_Entity);
                    cp.JSProperties["cp_close"] = true;
                    cp.JSProperties["cp_message"] = "Successfully deleted";
                    Session["Refresh"] = "1";
                    break;
                case "Close":
                    cp.JSProperties["cp_close"] = true;
                    Session["Refresh"] = "1";
                    break;
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
                e.InsertValues.Clear();
                e.UpdateValues.Clear();
            }
        }
        #endregion

        protected void glWarehouseCOde_TextChanged(object sender, EventArgs e)
        {

        }

        protected void glOCN_TextChanged(object sender, EventArgs e)
        {

        }

        protected void sup_cp_Callback(object sender, CallbackEventArgsBase e)
        {
       //       //This is the datasource where we will get the connection string
       //     SqlDataSource ds = OCN;
       //     //This is the sql command that we will initiate to find the data that we want to set to the textbox.
       //// (the e.Parameter is the callback item that we sent to the server)
       //     ds.SelectCommand = string.Format("SELECT OutgoingDocType FROM WMS.[ICN] WHERE DocNumber = '" + e.Parameter + "'");
       // //This is where we now initiate the command and get the data from it using dataview	
       //     DataView biz = (DataView)ds.Select(DataSourceSelectArguments.Empty);
       //     if (biz.Count > 0)
       //     {
       // //Now, this is the part where we assign the following data to the textbox
       //         //txttype.Text = biz[0][0].ToString();
       //     }
        }

        //protected void SetStatus()
        //{
        //    if (_Entity.Status == "P")
        //        txtStatus.Value = "Partial";
        //    else if (_Entity.Status == "C")
        //        txtStatus.Value = "Closed";
        //    else if (_Entity.Status == "L")
        //        txtStatus.Value = "Cancelled";
        //    else if (_Entity.Status == "X")
        //        txtStatus.Value = "Manual Closed";
        //    else if (_Entity.Status == "A")
        //        txtStatus.Value = "Partial Closed";
        //    else
        //    {
        //        txtStatus.Value = "New";
        //    }
        //}

        //protected void SaveStatus()
        //{
        //    if (txtStatus.Text == "Partial")
        //        _Entity.Status = "P";
        //    else if (txtStatus.Text == "Closed")
        //        _Entity.Status = "C";
        //    else if (txtStatus.Text == "Cancelled")
        //        _Entity.Status = "L";
        //    else if (txtStatus.Text == "Manual Closed")
        //        _Entity.Status = "X";
        //    else if (txtStatus.Text == "Partial Closed")
        //        _Entity.Status = "A";
        //    else
        //        _Entity.Status = "N";

        //}

        protected void SetText()
        {
            //SqlDataSource ds = SupplierCodelookup;
            //ds.SelectCommand = string.Format("SELECT SupplierCode from Masterfile.BPSupplierInfo where SupplierCode = '" + glSupplierCode.Text + "'");
            //DataView tran = (DataView)ds.Select(DataSourceSelectArguments.Empty);
            //if (tran.Count > 0)
            //{
            //    glSupplierCode.Text = tran[0][0].ToString();
            //}

            //ds.SelectCommand = string.Format("SELECT BizPartnerCode,CostCenterCode FROM Masterfile.[BPCustomerInfo] WHERE BizPartnerCode = '" + glCustomerCode.Text + "'");
            //DataView inb = (DataView)ds.Select(DataSourceSelectArguments.Empty);
            //if (inb.Count > 0)
            //{
            //    glCostCenter.Value = inb[0][0].ToString();
            //}

            //DataTable CostCenterCode = Gears.RetriveData2("SELECT CostCenterCode FROM Masterfile.[BPCustomerInfo] WHERE BizPartnerCode = '" + glCustomerCode.Text + "'", Session["ConnString"].ToString());//ADD CONN
            //foreach (DataRow dt in CostCenterCode.Rows)
            //{
            //    glCostCenter.Value = dt[0].ToString();
            //}

            //DataTable Life = Gears.RetriveData2("SELECT Life FROM Accounting.AssetInv WHERE PropertyNumber = '" + PropertyNumber + "'");
            //foreach (DataRow dt in Life.Rows)
            //{
            //    childLife = Convert.ToInt32(dt[0].ToString());
            //}
        }

        // Date Validation      LGE - 02 - 15 - 2016
        //protected void DateValidation()
        //{
        //    DateTime DocDate = Convert.ToDateTime(dtDocDate.Value);
        //    //string DocDate = DocDateTime.ToShortDateString();
        //    DateTime TargetDate = Convert.ToDateTime(dtTargetDate.Value);
        //    //string TargetDate = TargetDateTime.ToShortDateString();
        //    //int result = DateTime.Compare(DocDateTime.Date, TargetDateTime.Date);

        //    if(TargetDate.Date < DocDate.Date)
        //    {
        //        dateValidation = true;
        //        error = true;
        //    }
        //}
        // End of Date Validation


        protected void glComponent_Init(object sender, EventArgs e)
        {
            ASPxGridLookup gridLookup = sender as ASPxGridLookup;
            //gridLookup.GridView.CustomCallback += new ASPxGridViewCustomCallbackEventHandler(rate_CustomCallback);
            if (Session["Component"] != null)
            {
                gridLookup.GridView.DataSourceID = "sdsComponent";
                sdsComponent.FilterExpression = Session["Component"].ToString();
                gridLookup.DataBind();
            }
        }
        protected void glSizeCode_Init(object sender, EventArgs e)
        {
            ASPxGridLookup gridLookup = sender as ASPxGridLookup;
            //gridLookup.GridView.CustomCallback += new ASPxGridViewCustomCallbackEventHandler(rate_CustomCallback);
            if (Session["RateCode"] != null)
            {
                gridLookup.GridView.DataSourceID = "sdsRateCode";
                sdsRateCode.FilterExpression = Session["StyleTemplateCode"].ToString();
                gridLookup.DataBind();
            }
        }
        protected void Connection_Init(object sender, EventArgs e)
        {
            sdsDetail.ConnectionString = Session["ConnString"].ToString();
            Masterfileitem.ConnectionString = Session["ConnString"].ToString();
            Masterfileitemdetail.ConnectionString = Session["ConnString"].ToString();
            ReceivingWarehouselookup.ConnectionString = Session["ConnString"].ToString();
            CustomerCodelookup.ConnectionString = Session["ConnString"].ToString();
            CostCenterlookup.ConnectionString = Session["ConnString"].ToString();
            sdsComponent.ConnectionString = Session["ConnString"].ToString();
            sdsRateCode.ConnectionString = Session["ConnString"].ToString();
        }

    }
}