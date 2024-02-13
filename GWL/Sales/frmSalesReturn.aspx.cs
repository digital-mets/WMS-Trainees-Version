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
using GearsSales;

namespace GWL
{
    public partial class frmSalesReturn : System.Web.UI.Page
    {
        Boolean error = false;//Boolean for grid validation
        Boolean view = false;//Boolean for view state
        Boolean check = false;//Boolean for grid validation
        Boolean edit = false;   //Boolean for Edit Mode

        Entity.SalesReturn _Entity = new SalesReturn();//Calls entity odsHeader
        Entity.SalesReturn.SalesReturnDetail _EntityDetail = new SalesReturn.SalesReturnDetail();//Call entity sdsDetail

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

            dtDocDate.MinDate = Convert.ToDateTime(GearsCommon.GCommon.SystemSetting("BookDate", Session["ConnString"].ToString()).ToString());

            switch (Request.QueryString["entry"].ToString())
            {
                case "N":
                    updateBtn.Text = "Add";
                    glReferenceDR.Visible = true;
                    RefCheck();
                    break;
                case "E":
                    updateBtn.Text = "Update";
                    FilterDR();
                     RefCheck();
                    txtDocNumber.ReadOnly = true;
                    edit = true;
                    break;
                case "V":
                    view = true;//sets view mode for entry
                    glcheck.ClientVisible = false;
                    updateBtn.Text = "Close";
                    break;
                case "D":
                    view = true;
                    updateBtn.Text = "Delete";
                    break;
            }
            gvRef.Columns["CommandString"].Width = 0;
            gvRef.Columns["RCommandString"].Width = 0;

            if (!IsPostBack)
            {
                if (String.IsNullOrEmpty(glCustomer.Text))
                {
                    Session["customoutbound"] = null;
                }
                //Session["iswithpr"] = null;

                txtDocNumber.Value = Request.QueryString["docnumber"].ToString(); //sets docnumber from session

                _Entity.getdata(txtDocNumber.Text, Session["ConnString"].ToString());

                cmbReason.Value = _Entity.Reason.ToString();
                glCustomer.Value = _Entity.CustomerCode.ToString();
                FilterDR();
                glReferenceDR.Value = _Entity.ReferenceDRNo.ToString();
                txtRRDocNumber.Text = _Entity.RRDocNumber.ToString();
                dtDocDate.Text = String.IsNullOrEmpty(_Entity.DocDate.ToString()) ? null : Convert.ToDateTime(_Entity.DocDate.ToString()).ToShortDateString();
                glReceivingWarehouse.Value = _Entity.WarehouseCode.ToString();
                AMRemarks.Value = _Entity.Remarks.ToString();
                //txtCurrency.Value = _Entity.Currency.ToString();
                //txtTotalAmount.Value = _Entity.TotalAmount.ToString();
                 memTotalQty.Text = _Entity.TotalQuantity.ToString();
                txtCounterDocNo.Value = _Entity.CounterDocNo.ToString();
                speTotalBulk.Value = _Entity.TotalBulkQty;


                txtHField1.Value = _Entity.Field1.ToString();
                txtHField2.Value = _Entity.Field2.ToString();
                txtHField3.Value = _Entity.Field3.ToString();
                txtHField4.Value = _Entity.Field4.ToString();
                txtHField5.Value = _Entity.Field5.ToString();
                txtHField6.Value = _Entity.Field6.ToString();
                txtHField7.Value = _Entity.Field7.ToString();
                txtHField8.Value = _Entity.Field8.ToString();
                txtHField9.Value = _Entity.Field9.ToString();

                chkIsWithDR.Value = _Entity.IsWithDR;
                chkReclass.Value = _Entity.IsReclass;
                txtHAddedBy.Text = _Entity.AddedBy;
                txtHAddedDate.Text = _Entity.AddedDate;
                txtHLastEditedBy.Text = _Entity.LastEditedBy;
                txtHLastEditedDate.Text = _Entity.LastEditedDate;
                txtHSubmittedBy.Text = _Entity.SubmittedBy;
                txtHSubmittedDate.Text = _Entity.SubmittedDate;
                
                txtHCancelledBy.Text = _Entity.CancelledBy;
                txtHCancelledDate.Text = _Entity.CancelledDate;

                //V=View, E=Edit, N=New
                

                if (Request.QueryString["entry"].ToString() == "N")
                {
                    gv1.KeyFieldName = "LineNumber";
                    gv1.DataSourceID = null;
                    popup.ShowOnPageLoad = false;

                    gv1.DataSourceID = "sdsDetail";
                    Session["Datatable"] = "0";
                    glReferenceDR.ClientEnabled = false;
                    //glPRNumber.ClientEnabled = true;
                    //Generatebtn.ClientVisible = true;
                    //gv1.SettingsPager.Mode = GridViewPagerMode.ShowAllRecords;
                    //gv1.Settings.VerticalScrollableHeight = 200;
                    //gv1.KeyFieldName = "LineNumber";

                }
                else
                {
                 
                    gvRef.DataSourceID = "odsReference";
                    odsReference.SelectParameters["DocNumber"].DefaultValue = txtDocNumber.Text;
                    this.gvRef.Columns["CommandString"].Width = 0;

                    this.gvRef.Columns["RCommandString"].Width = 0;
                }
                

                //Details checking. changes datasourceid of gridview if there's no details for a certain docnumber; to enable adding of details.
                DataTable dtbom = _EntityDetail.getdetail(txtDocNumber.Text, Session["ConnString"].ToString());
                if (dtbom.Rows.Count > 0)
                {
                    //Details checking. changes datasourceid of gridview if there's no details for a certain docnumber; to enable adding of details.
                   
                    odsDetail.SelectParameters["DocNumber"].DefaultValue = txtDocNumber.Text;
                    gv1.DataSourceID = "odsDetail";
                }
                else
                {
                    gv1.DataSourceID = "sdsDetail";
                }
                gv1.KeyFieldName = "LineNumber";


                DataTable dtrr1 = Gears.RetriveData2("Select DocNumber from Sales.SalesReturn where docnumber = '" + txtDocNumber.Text + "' and ISNULL(Reason,'')!='' ", Session["ConnString"].ToString());
                if (dtrr1.Rows.Count > 0 && Request.QueryString["entry"].ToString() == "N")
                {
                    updateBtn.Text = "Update";
                }

                //if (Request.QueryString["entry"].ToString() != "N")
                //{
                //    if (Request.QueryString["iswithdetail"].ToString() == "true" && (Session["iswithdr"] == "1" || chkIsWithDR.Value.ToString() == "True"))
                //    {
                //        gv1.KeyFieldName = "LineNumber";
                //        Generatebtn.Enabled = false;
                //        chkIsWithDR.Enabled = false;
                //    }
                //    else if (Request.QueryString["iswithdetail"].ToString() == "false" && (Session["iswithdr"] == "1" || chkIsWithDR.Value.ToString() == "True"))
                //    {
                //        gv1.KeyFieldName = "LineNumber";
                //        Generatebtn.Enabled = true;
                //        chkIsWithDR.ReadOnly = false;

                //       //RA glReferenceDR.ClientEnabled = true;
                //    }
                //    else if (Request.QueryString["iswithdetail"].ToString() == "false" && (Session["iswithdr"] == "0" || chkIsWithDR.Value.ToString() == "False"))
                //    {
                //        gv1.KeyFieldName = "LineNumber";
                //        Generatebtn.Enabled = true;
                //        chkIsWithDR.ReadOnly = false;
                //    }
                //    else if (Request.QueryString["iswithdetail"].ToString() == "true" && (Session["iswithdr"] == "0" || chkIsWithDR.Value.ToString() == "False"))
                //    {
                //        gv1.KeyFieldName = "LineNumber";
                //        Generatebtn.Enabled = false;
                //        chkIsWithDR.Enabled = false;

                //    }
                //}
            }


        }
        #endregion

        #region Validation
        string strresult = "";
        private void Validate()
        {
            GearsLibrary.Gears.GearsParameter gparam = new Gears.GearsParameter();
            gparam._DocNo = _Entity.DocNumber;
            gparam._Connection = Session["ConnString"].ToString();
            gparam._UserId = Session["Userid"].ToString();
            gparam._TransType = Request.QueryString["transtype"].ToString();
            strresult += (GearsSales.GSales.SalesReturn_Validate(gparam));
            if (strresult != " ")
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
            gparam._TransType = "SLSSRN";
            gparam._Table = "Sales.SalesReturn";
            gparam._Factor = -1;
            gparam._Connection = Session["ConnString"].ToString();
            strresult += GearsSales.GSales.SalesReturn_Post(gparam);
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

        protected void MemoLoad(object sender, EventArgs e)//Control for all textbox
        {
            ASPxMemo text = sender as ASPxMemo;
            text.ReadOnly = view;
        }

        protected void Comboboxload(object sender, EventArgs e)
        {
            ASPxComboBox combobox = sender as ASPxComboBox;
            combobox.ReadOnly = view;
        }

        protected void Generate_Btn(object sender, EventArgs e)//Control for all textbox
        {
            ASPxButton btn = sender as ASPxButton;
            Generatebtn.ClientVisible = !view;
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
        protected void gv1_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)//Control for grid
        {
           // if (Request.QueryString["entry"].ToString() == "V" || Request.QueryString["entry"].ToString() == "D")
           // {
           //     ASPxGridView grid = sender as ASPxGridView;
           //     grid.SettingsBehavior.AllowGroup = false;
           //     grid.SettingsBehavior.AllowSort = false;
           //     e.Editor.ReadOnly = view;
           // }
           //else
           // {
           //     ASPxGridView grid = sender as ASPxGridView;
           //     grid.SettingsBehavior.AllowGroup = false;
           //     grid.SettingsBehavior.AllowSort = false;
           //     e.Editor.ReadOnly = false;
           // }
        
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

                if (chkIsWithDR.Value.ToString() == "True")
                {
                    if (e.ButtonType == ColumnCommandButtonType.New || e.ButtonType == ColumnCommandButtonType.Delete)
                    {
                        e.Visible = false;
                    }
                }
                if (chkIsWithDR.Value.ToString() == "False")
                {
                    if (e.ButtonType == ColumnCommandButtonType.New || e.ButtonType == ColumnCommandButtonType.Delete)
                    {
                        e.Visible = true;
                    }
                }
            }

            if (Request.QueryString["entry"] == "V" || Request.QueryString["entry"] == "D")
            {
                if (e.ButtonType == ColumnCommandButtonType.New || e.ButtonType == ColumnCommandButtonType.Delete)
                {
                    e.Visible = false;
                }
            }

            if (e.ButtonType == ColumnCommandButtonType.Update || e.ButtonType == ColumnCommandButtonType.Cancel)
                e.Visible = false;

            
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

            if (Request.QueryString["entry"].ToString() == "V" || Request.QueryString["entry"].ToString() == "D" )
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
                //Session["FilterExpression"] = null;
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
                DataTable countcolor = Gears.RetriveData2("Select DISTINCT ColorCode,ClassCode,SizeCode,UnitBase,FullDesc,StatusCode from masterfile.item a " +
                                                          "inner join masterfile.itemdetail b on a.itemcode = b.itemcode where a.itemcode = '" + itemcode + "'", Session["ConnString"].ToString());

                    foreach (DataRow dt in countcolor.Rows)
                    {
                        codes = dt["ColorCode"].ToString() + ";";
                        codes += dt["ClassCode"].ToString() + ";";
                        codes += dt["SizeCode"].ToString() + ";";
                        codes += dt["UnitBase"].ToString() + ";";
                        codes += dt["FullDesc"].ToString() + ";";
                        codes += dt["StatusCode"].ToString() + ";";
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
                ASPxGridLookup lookup = (ASPxGridLookup)gv1.FindEditRowCellTemplateControl((GridViewDataColumn)gv1.Columns[4], "glItemCode");
                var selectedValues = itemcode;
                CriteriaOperator selectionCriteria = new InOperator(lookup.KeyFieldName, new string[] { itemcode });
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
            _Entity.Connection = Session["ConnString"].ToString(); //ADD CONN
            _Entity.Reason = cmbReason.Text;
            _Entity.CustomerCode = glCustomer.Text;
            _Entity.ReferenceDRNo = glReferenceDR.Text;
            _Entity.RRDocNumber = txtRRDocNumber.Text;
            _Entity.DocDate = dtDocDate.Text;
            _Entity.WarehouseCode = glReceivingWarehouse.Text;
            _Entity.Remarks = AMRemarks.Text;
          //s  _Entity.Currency = txtCurrency.Text;
       
            _Entity.Remarks = AMRemarks.Text;
            _Entity.CounterDocNo = txtCounterDocNo.Text;
            _Entity.IsWithDR = Convert.ToBoolean(chkIsWithDR.Value);
            _Entity.IsReclass = Convert.ToBoolean(chkReclass.Value);

            _Entity.DocDate = dtDocDate.Text;
            _Entity.TransType = Request.QueryString["transtype"].ToString();
            _Entity.TotalQuantity = memTotalQty.Text;
            _Entity.TotalBulkQty = Convert.ToDecimal(Convert.IsDBNull(speTotalBulk.Value) ? 0 : Convert.ToDecimal(speTotalBulk.Value)); 
           // _Entity.TotalAmount = Convert.ToDecimal(Convert.IsDBNull(txtTotalAmount.Value) ? 0 : Convert.ToDecimal(txtTotalAmount.Value));
    
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

               // _Entity.Transtype = Request.QueryString["transtype"].ToString();

            switch (e.Parameter)
            {
                //case "Add":

                //    gv1.UpdateEdit();

                //    if (error == false)
                //    {
                //        check = true;
                //        _Entity.UpdateData(_Entity); //Method of inserting for header
                //        //_Entity.SubsiEntry(txtDocNumber.Text);
                //        if (Session["Datatable"] == "1")
                //        {
                //            gv1.UpdateEdit();
                //        }
                //        else 
                //        { 
                //        gv1.DataSourceID = "odsDetail";//Renew datasourceID to entity
                //        odsDetail.SelectParameters["DocNumber"].DefaultValue = txtDocNumber.Text;//Set select parameter to prevent error
                //        gv1.UpdateEdit();//2nd initiation to insert grid
                //        }
                //        Validate();
                //        cp.JSProperties["cp_message"] = "Successfully Updated!";
                //        cp.JSProperties["cp_success"] = true;
                //        cp.JSProperties["cp_close"] = true;
                //        Session["Datatable"] = null;
                //    }
                //    else
                //    {
                //        cp.JSProperties["cp_message"] = "Please check all the fields!";
                //        cp.JSProperties["cp_success"] = true;
                //    }
                //    break;

                case "Update":
                case "Add":
                     _Entity.UpdateData(_Entity);//Method of inserting for header
                    gv1.UpdateEdit();

                    if (error == false)
                    {
                        check = true;
                       
                        if (Session["Datatable"] == "1")
                        {
                            //gv1.DataSourceID = sdsPicklistDetail.ID;
                            gv1.UpdateEdit();
                        }
                        else 
                        { 
                        gv1.DataSourceID = "odsDetail";//Renew datasourceID to entity
                        odsDetail.SelectParameters["DocNumber"].DefaultValue = txtDocNumber.Text;//Set select parameter to prevent error
                         gv1.UpdateEdit();//2nd initiation to insert grid
                        }
                      //  _Entity.SubsiEntry(txtDocNumber.Text);
                        Validate();
                        Post();
                        cp.JSProperties["cp_message"] = "Successfully Updated!";
                        cp.JSProperties["cp_success"] = true;
                        cp.JSProperties["cp_close"] = true;
                   //     Session["Refresh"] = "1";
                    
                    }
                    else
                    {
                        cp.JSProperties["cp_message"] = "Please check all the fields!";
                        cp.JSProperties["cp_success"] = true;
                    }
                    break;


                case "Delete":
                    //GetSelectedVal(); 
                    cp.JSProperties["cp_delete"] = true;
                    break;

                case "ConfDelete":
                    _Entity.DeleteData(_Entity);
                    cp.JSProperties["cp_close"] = true;
                    cp.JSProperties["cp_message"] = "Successfully deleted";
                    break;

                case "Close":
                        cp.JSProperties["cp_close"] = true;
                    break;

                case "Generate":
                    GetSelectedVal();

                    cp.JSProperties["cp_generated"] = true;
                     
		            GridViewDataColumn c = gv1.Columns["ItemCode"] as GridViewDataColumn;
		            if (c != null) {
			            c.ReadOnly = true;
		            } 

                    break;

                case "SupplierCodeCase":
              
                    break;

              //  case "iswithdrtrue":
              //      Session["iswithdr"] = "1";
              //      Session["Datatable"] = "0";
                  
              //      cp.JSProperties["cp_iswithdr"] = "1";
             
              //      gv1.DataSource = null;
              //      gv1.DataBind();
              //    break;
              //case "iswithdrfalse":
              //      Session["iswithdr"] = "0";
              //      Session["Datatable"] = "0";
         
              //      cp.JSProperties["cp_iswithdr"] = "0";
        
              //      glReferenceDR.Text = "";

              //      gv1.DataSourceID = "sdsDetail";
              //      break;

                case "refgrid":

                    gv1.DataBind();
                    FilterDR();
                    //cp.JSProperties["cp_generated"] = true;
                  
                    break;

                case "CallbackCustomer":

                    Session["Datatable"] = "0";
           
                   
                    gv1.DataSource = null;
                    gv1.DataBind();
                  
                    FilterDR();
                    RefCheck();

                    cp.JSProperties["cp_generated"] = true;

                    break;

                case "CallbackCheck":

                    Session["Datatable"] = "0";
                    gv1.DataSource = sdsDetail;
                    if (gv1.DataSourceID != "")
                    {
                        gv1.DataSourceID = null;
                    }
                    gv1.DataBind();

                    glReferenceDR.Text = "";
                    RefCheck();
                    FilterDR();
                    cp.JSProperties["cp_generated"] = true;
                

                    break;

                //case "customercode":
                //    sdsPicklist.SelectParameters["customercode"].DefaultValue = aglCustomer.Text;
                //    break;

            }
        }

        protected void FilterDR()
        {
            
            if (!String.IsNullOrEmpty(glCustomer.Text))
            {
                PRNumberlookup.SelectCommand = "SELECT DocNumber,CustomerCode FROM Sales.[DeliveryReceipt] WHERE ISNULL (SubmittedBy,'')!='' and ISNULL(InvoiceDocNum,'') ='' and ISNULL(CancelledBy,'') ='' and ISNULL(CancelledDate,'')='' AND CustomerCode ='" + glCustomer.Text + "'"; 
                glReferenceDR.DataBind();
            }
            if (String.IsNullOrEmpty(glCustomer.Text))
            {

                PRNumberlookup.SelectCommand = "SELECT DocNumber,CustomerCode FROM Sales.[DeliveryReceipt] WHERE ISNULL (SubmittedBy,'')!='' and ISNULL(InvoiceDocNum,'') ='' and ISNULL(CancelledBy,'') ='' and ISNULL(CancelledDate,'')=''  AND CustomerCode ='" + glCustomer.Text + "' "; 
                glReferenceDR.DataBind();
            }
        }

        protected void RefCheck()
       
        {//{
            if (!String.IsNullOrEmpty(glCustomer.Text))
            {

                if (chkIsWithDR.Checked == true)
                {

                    glReferenceDR.ClientEnabled = true;
                    gv1.Columns["SONumber"].Width = 80;
                }
                else
                {
                     glReferenceDR.ClientEnabled = false;
                    gv1.Columns["SONumber"].Width = 0;
                }
            }
            else
            {
                glReferenceDR.ClientEnabled = true;
                gv1.Columns["SONumber"].Width = 80;
            }
        }
        protected void grid_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        { //Validation for gridview, set all validate conditions here.. (sample here is checking of empty cells)
            //string ItemCode = "";
            //string ColorCode = "";
            //string ClassCode = "";
            //string SizeCode = "";
            //if (error == false && check == false)
            //{
            //    foreach (GridViewColumn column in gv1.Columns)
            //    {
            //        GridViewDataColumn dataColumn = column as GridViewDataColumn;
            //        if (dataColumn == null) continue;
            //        if (e.NewValues[dataColumn.FieldName] == null && dataColumn.FieldName != "LineNumber" && dataColumn.FieldName != "SONumber"  && dataColumn.FieldName != "DocNumber"
            //            && dataColumn.FieldName != "PRNumber" && dataColumn.FieldName != "ReturnedQty" && dataColumn.FieldName != "UnitPrice" && dataColumn.FieldName != "ReturnedBulkQty"
            //            && dataColumn.FieldName != "StatusCode" && dataColumn.FieldName != "DiscountRate" && dataColumn.FieldName != "AverageCost"
            //            && dataColumn.FieldName != "VATCode" && dataColumn.FieldName != "IsVat" && dataColumn.FieldName != "IsAllowPartial"
            //            && dataColumn.FieldName != "Field1" && dataColumn.FieldName != "Field2" && dataColumn.FieldName != "Field3"
            //            && dataColumn.FieldName != "Field4" && dataColumn.FieldName != "Field5" && dataColumn.FieldName != "Field6"
            //            && dataColumn.FieldName != "Field7" && dataColumn.FieldName != "Field8" && dataColumn.FieldName != "Field9")
            //        {
            //            e.Errors[dataColumn] = "Value can't be null.";//Sets error tooltip message
            //        }
            //    }

            //    //Checking for non existing Codes..
            //    ItemCode = e.NewValues["ItemCode"].ToString();
            //    ColorCode = e.NewValues["ColorCode"].ToString();
            //    ClassCode = e.NewValues["ClassCode"].ToString();
            //    SizeCode = e.NewValues["SizeCode"].ToString();
            //    DataTable item = Gears.RetriveData2("SELECT ItemCode FROM Masterfile.[ItemDetail] WHERE ItemCode = '" + ItemCode + "'");
            //    if (item.Rows.Count < 1)
            //    {
            //        AddError(e.Errors, gv1.Columns["ItemCode"], "ItemCode doesn't exist!");
            //    }
            //    DataTable color = Gears.RetriveData2("SELECT ColorCode FROM Masterfile.[ItemDetail] WHERE ColorCode = '" + ColorCode + "'");
            //    if (color.Rows.Count < 1)
            //    {
            //        AddError(e.Errors, gv1.Columns["ColorCode"], "ColorCode doesn't exist!");
            //    }
            //    DataTable clss = Gears.RetriveData2("SELECT ClassCode FROM Masterfile.[ItemDetail] WHERE ClassCode = '" + ClassCode + "'");
            //    if (clss.Rows.Count < 1)
            //    {
            //        AddError(e.Errors, gv1.Columns["ClassCode"], "ClassCode doesn't exist!");
            //    }
            //    DataTable size = Gears.RetriveData2("SELECT SizeCode FROM Masterfile.[ItemDetail] WHERE SizeCode = '" + SizeCode + "'");
            //    if (size.Rows.Count < 1)
            //    {
            //        AddError(e.Errors, gv1.Columns["SizeCode"], "SizeCode doesn't exist!");
            //    }


            //    if (e.Errors.Count > 0)
            //    {
            //        error = true; //bool to cancel adding/updating if true
            //    }
            //}
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
                e.UpdateValues.Clear();
                e.InsertValues.Clear();
            }

            if (Session["Datatable"] == "1" )
             {
                e.Handled = true;
                DataTable source = GetSelectedVal();

                // Removing all deleted rows from the data source(Excel file)
                foreach (ASPxDataDeleteValues values in e.DeleteValues)
                {
                    try
                    {

                        object[] keys = { values.Keys[0] };
                        source.Rows.Remove(source.Rows.Find(keys));
                    }
                    catch (Exception)
                    {
                        continue;
                    }
                }

                int ln = 1;
                foreach (ASPxDataInsertValues values in e.InsertValues)//for insert chu chu
                {
                    var LineNumber = ln;
                    var SONumber = values.NewValues["SONumber"];
                    var ItemCode = values.NewValues["ItemCode"];
                    var FullDesc = values.NewValues["FullDesc"];
                    var ColorCode = values.NewValues["ColorCode"];
                    var ClassCode = values.NewValues["ClassCode"];
                    var SizeCode = values.NewValues["SizeCode"];
                    var ReturnedQty = values.NewValues["ReturnedQty"];
                    var ReturnedBulkQty = values.NewValues["ReturnedBulkQty"];
                    var Unit = values.NewValues["Unit"];
                    var StatusCode = values.NewValues["StatusCode"];
                    var AverageCost = values.NewValues["AverageCost"];
                
                    var Field1 = values.NewValues["Field1"];
                    var Field2 = values.NewValues["Field2"];
                    var Field3 = values.NewValues["Field3"];
                    var Field4 = values.NewValues["Field4"];
                    var Field5 = values.NewValues["Field5"];
                    var Field6 = values.NewValues["Field6"];
                    var Field7 = values.NewValues["Field7"];
                    var Field8 = values.NewValues["Field8"];
                    var Field9 = values.NewValues["Field9"];
                    var ExpDate = values.NewValues["ExpDate"];
                    var MfgDate = values.NewValues["MfgDate"];
                    var BatchNo = values.NewValues["BatchNo"];
                    var LotNo = values.NewValues["LotNo"];

                    source.Rows.Add(LineNumber, SONumber, ItemCode, FullDesc, ColorCode, ClassCode, SizeCode,
                                    ReturnedQty, Unit, ReturnedBulkQty, StatusCode, AverageCost, ExpDate,
                                    MfgDate, BatchNo, LotNo, Field1, Field2, Field3, Field4, Field5, Field6, Field7, Field8, Field9);
                    ln++;
                }
       
                // Updating required rows
                foreach (ASPxDataUpdateValues values in e.UpdateValues)
                {
                    object[] keys = { values.NewValues["LineNumber"] };
                    DataRow row = source.Rows.Find(keys);

                    //row["ItemCode"] = values.NewValues["ItemCode"];

                    //row["ColorCode"] = values.NewValues["ColorCode"];
                    //row["ClassCode"] = values.NewValues["ClassCode"];
                    //row["SizeCode"] = values.NewValues["SizeCode"];
                    row["ReturnedQty"] = values.NewValues["ReturnedQty"];
                    row["ReturnedBulkQty"] = values.NewValues["ReturnedBulkQty"];
                    row["Unit"] = values.NewValues["Unit"];
                    //row["UnitPrice"] = values.NewValues["UnitPrice"];
                    row["StatusCode"] = values.NewValues["StatusCode"];
                    //row["DiscountRate"] = values.NewValues["DiscountRate"];
                    row["AverageCost"] = values.NewValues["AverageCost"];


                    row["Field1"] = values.NewValues["Field1"];
                    row["Field2"] = values.NewValues["Field2"];
                    row["Field3"] = values.NewValues["Field3"];
                    row["Field4"] = values.NewValues["Field4"];
                    row["Field5"] = values.NewValues["Field5"];
                    row["Field6"] = values.NewValues["Field6"];
                    row["Field7"] = values.NewValues["Field7"];
                    row["Field8"] = values.NewValues["Field8"];
                    row["Field9"] = values.NewValues["Field9"];
                    row["ExpDate"] = values.NewValues["ExpDate"];
                    row["MfgDate"] = values.NewValues["MfgDate"];
                    row["BatchNo"] = values.NewValues["BatchNo"];
                    row["LotNo"] = values.NewValues["LotNo"];
                }

                Gears.RetriveData2("DELETE FROM Sales.SalesReturnDetail where Docnumber='" + txtDocNumber.Text + "'", Session["ConnString"].ToString());
                foreach (DataRow dtRow in source.Rows)//This is where the data will be inserted into db
                {
                    _EntityDetail.SONumber = dtRow["SONumber"].ToString();
           
                    _EntityDetail.ItemCode = dtRow["ItemCode"].ToString();
                    _EntityDetail.FullDesc = dtRow["FullDesc"].ToString();
           
                    _EntityDetail.ColorCode = dtRow["ColorCode"].ToString();
                    _EntityDetail.ClassCode = dtRow["ClassCode"].ToString();
                    _EntityDetail.SizeCode = dtRow["SizeCode"].ToString();

                    _EntityDetail.ReturnedQty = Convert.ToDecimal(Convert.IsDBNull(dtRow["ReturnedQty"]) ? 0 : dtRow["ReturnedQty"]);
                    _EntityDetail.ReturnedBulkQty = Convert.ToDecimal(Convert.IsDBNull(dtRow["ReturnedBulkQty"]) ? 0 : dtRow["ReturnedBulkQty"]);
                    _EntityDetail.Unit = dtRow["Unit"].ToString();
                    //_EntityDetail.UnitPrice = Convert.ToDecimal(Convert.IsDBNull(dtRow["UnitPrice"]) ? 0 : dtRow["UnitPrice"]);


            
                    _EntityDetail.StatusCode = dtRow["StatusCode"].ToString();
                    //_EntityDetail.DiscountRate = Convert.ToDecimal(Convert.IsDBNull(dtRow["DiscountRate"]) ? 0 : dtRow["DiscountRate"]);

                    _EntityDetail.AverageCost = Convert.ToDecimal(Convert.IsDBNull(dtRow["AverageCost"]) ? 0 : dtRow["AverageCost"]);
                    _EntityDetail.Field3 = dtRow["Field1"].ToString();
                    _EntityDetail.Field3 = dtRow["Field2"].ToString();
                    _EntityDetail.Field3 = dtRow["Field3"].ToString();
                    _EntityDetail.Field4 = dtRow["Field4"].ToString();
                    _EntityDetail.Field5 = dtRow["Field5"].ToString();
                    _EntityDetail.Field6 = dtRow["Field6"].ToString();
                    _EntityDetail.Field7 = dtRow["Field7"].ToString();
                    _EntityDetail.Field8 = dtRow["Field8"].ToString();
                    _EntityDetail.Field9 = dtRow["Field9"].ToString();
                    _EntityDetail.ExpDate = Convert.ToDateTime(Convert.IsDBNull(dtRow["ExpDate"]) ? null : dtRow["ExpDate"]);
                    _EntityDetail.MfgDate = Convert.ToDateTime(Convert.IsDBNull(dtRow["MfgDate"]) ? null : dtRow["MfgDate"]);
                    _EntityDetail.BatchNo = dtRow["BatchNo"].ToString();
                    _EntityDetail.LotNo = dtRow["LotNo"].ToString();
                    _EntityDetail.AddSalesReturnDetail(_EntityDetail);
                }
            }
        }
        #endregion



        //protected void aglcustomerinit(object sender, EventArgs e)
        //{
        //    ASPxGridLookup gridLookup = sender as ASPxGridLookup;
        //    gridLookup.GridView.CustomCallback += new ASPxGridViewCustomCallbackEventHandler(glPickList_CustomCallback);
        //    if (Session["customoutbound"] != null)
        //    {
        //        gridLookup.GridView.DataSource = PRNumberlookup.ID;
        //        PRNumberlookup.FilterExpression = Session["customoutbound"].ToString();
        //    }
        //}

        //public void glPickList_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        //{
          

        //    string whcode = e.Parameters;//Set column name
        //    if (whcode.Contains("GLP_AIC") || whcode.Contains("GLP_AC") || whcode.Contains("GLP_F")) return;//Traps the callback

        //    ASPxGridView grid = sender as ASPxGridView;
        //    grid.DataSourceID = null;
          
        //    CriteriaOperator selectionCriteria = new InOperator("CustomerCode", new string[] { whcode });
        //    PRNumberlookup.FilterExpression = (GroupOperator.Combine(GroupOperatorType.And, selectionCriteria)).ToString();
        //    Session["customoutbound"] = PRNumberlookup.FilterExpression;
        //    grid.DataSourceID = PRNumberlookup.ID;
        //    grid.DataBind();
        //}
        protected void gv1_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["salesreturndetail"] = null;
            }

            if (Session["salesreturndetail"] != null)
            {
                gv1.DataSourceID = sdsPicklistDetail.ID;
                sdsPicklistDetail.FilterExpression = Session["salesreturndetail"].ToString();
                //gridview.DataSourceID = null;
            }
        }

        private DataTable GetSelectedVal()
        {
            gv1.DataSource = null;
            DataTable dt = new DataTable();
            string[] selectedValues = glReferenceDR.Text.Split(';');
            CriteriaOperator selectionCriteria = new InOperator(glReferenceDR.KeyFieldName, selectedValues);

            sdsPicklistDetail.FilterExpression = (GroupOperator.Combine(GroupOperatorType.And, selectionCriteria)).ToString();
            Session["salesreturndetail"] = sdsPicklistDetail.FilterExpression;
            gv1.DataSourceID = sdsPicklistDetail.ID;
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

            dt.PrimaryKey = new DataColumn[] { //Sets datatable's primary key for batchupdate reference during adding.
            dt.Columns["LineNumber"]};

          //  glReferenceDR.ClientEnabled = false;

            return dt;
        }
       

        protected void dtpDocDate_Init(object sender, EventArgs e)
        {
            if (Request.QueryString["entry"] == "N")
            {
                dtDocDate.Date = DateTime.Now;
            }
        }

        protected void Connection_Init(object sender, EventArgs e)
        {

            sdsDetail.ConnectionString = Session["ConnString"].ToString();
            sdsItem.ConnectionString = Session["ConnString"].ToString();
         
            sdsItemDetail.ConnectionString = Session["ConnString"].ToString();
            sdsPicklistDetail.ConnectionString = Session["ConnString"].ToString();
            SupplierCodelookup.ConnectionString = Session["ConnString"].ToString();
            ContactPersonlookup.ConnectionString = Session["ConnString"].ToString();
            Currencylookup.ConnectionString = Session["ConnString"].ToString();

            ReceivingWarehouselookup.ConnectionString = Session["ConnString"].ToString();
            PRNumberlookup.ConnectionString = Session["ConnString"].ToString();

        }
        
        
    }
}