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
    public partial class frmPurchaseOrderRevision : System.Web.UI.Page
    {
        Boolean error = false;//Boolean for grid validation
        Boolean view = false;//Boolean for view state
        Boolean check = false;//Boolean for grid validation

        private static string Connection;

        Entity.PurchaseOrderRevision _Entity = new PurchaseOrderRevision();//Calls entity PurchaseOrderRevision
        Entity.PurchaseOrderRevision.PurchaseOrderRevisionDetail _EntityDetail = new PurchaseOrderRevision.PurchaseOrderRevisionDetail();//Call entity POdetails
        Entity.PurchaseOrderRevision.PurchaseOrderRevisionService _EntityDetailServ = new PurchaseOrderRevision.PurchaseOrderRevisionService();//Call entity POdetails

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
            
            dtpDate.MinDate = Convert.ToDateTime(GearsCommon.GCommon.SystemSetting("BookDate", Session["ConnString"].ToString()).ToString());

            //Details checking. changes datasourceid of gridview if there's no details for a certain docnumber; to enable adding of details.
           // if (Request.QueryString["entry"].ToString() == "V")
            //{
              //  view = true;//sets view mode for entry
            //}

            gv1.KeyFieldName = "DocNumber;LineNumber";
            //txtDocnumber.Value = Session["DocNumber"].ToString(); //sets docnumber from session
            txtDoc.ReadOnly = true;
            txtDoc.Value = Request.QueryString["docnumber"].ToString();
            if (!IsPostBack)
            {
                Session["FilterExpression"] = null;
                Session["DataTable"] = null;
                Connection = (Session["ConnString"].ToString()); 
                _Entity.getdata(txtDoc.Text, Session["ConnString"].ToString());//ADD CONN

                dtpDate.Text = String.IsNullOrEmpty(_Entity.DocDate)? "" :Convert.ToDateTime(_Entity.DocDate).ToShortDateString();
                dtpTarget.Text = String.IsNullOrEmpty(_Entity.TargetDelDate) ? "" : Convert.ToDateTime(_Entity.TargetDelDate).ToShortDateString();
                dtpComm.Text = String.IsNullOrEmpty(_Entity.CommDate) ? "" : Convert.ToDateTime(_Entity.CommDate).ToShortDateString();
                dtpCancel.Text = String.IsNullOrEmpty(_Entity.CancellationDate) ? "" : Convert.ToDateTime(_Entity.CancellationDate).ToShortDateString();
                txtBroker.Value = _Entity.Broker;
                txtOldSupplierCode.Value = _Entity.OldSupplier;
                glSupplierCode.Value = _Entity.Supplier;
                gvPODoc.Value = _Entity.PODocNumber;
                txtQty.Value = _Entity.TotalQty;
                txtRemarks.Value = _Entity.Remarks.ToString();
                txtPQ.Value = _Entity.PQReference;
                txtHField1.Text = _Entity.Field1;
                txtHField2.Text = _Entity.Field2;
                txtHField3.Text = _Entity.Field3;
                txtHField4.Text = _Entity.Field4;
                txtHField5.Text = _Entity.Field5;
                txtHField6.Text = _Entity.Field6;
                txtHField7.Text = _Entity.Field7;
                txtHField8.Text = _Entity.Field8;
                txtHField9.Text = _Entity.Field9;
                txtHAddedBy.Text = _Entity.AddedBy;
                txtHAddedDate.Text = _Entity.AddedDate;
                txtHLastEditedBy.Text = _Entity.LastEditedBy;
                txtHLastEditedDate.Text = _Entity.LastEditedDate;
                txtHSubmittedBy.Text = _Entity.SubmittedBy;
                txtHSubmittedDate.Text = _Entity.SubmittedDate;
                txtHCancelledBy.Text = _Entity.CancelledBy;
                txtHCancelledDate.Text = _Entity.CancelledDate;

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
                        updateBtn.Text = "Add";
                        //glcheck.ClientVisible = false;
                        break;
                    case "E":
                        updateBtn.Text = "Update";
                        //gvPODoc.ClientEnabled = false;
                        break;
                    case "V":
                        view = true;
                        updateBtn.Text = "Close";
                        glcheck.ClientVisible = false;
                        break;
                    case "D":
                        view = true;//sets view mode for entry
                        updateBtn.Text = "Delete";
                        glcheck.ClientVisible = false;
                        break;
                }
     
            
                //Details checking. changes datasourceid of gridview if there's no details for a certain docnumber; to enable adding of details.
                DataTable checkCount = Gears.RetriveData2("Select DocNumber from procurement.purchaseorderrevisiondetail where docnumber = '" + txtDoc.Text + "'",Connection);//ADD CONN
                gv1.DataSourceID = checkCount.Rows.Count > 0 ? odsDetail.ID : sdsDetail.ID;    
                     
                checkCount = Gears.RetriveData2("Select DocNumber from procurement.purchaseorderrevisionservice where docnumber = '" + txtDoc.Text + "'", Connection);//ADD CONN
                gvService.DataSourceID = checkCount.Rows.Count > 0 ? odsDetail2.ID : sdsDetail2.ID;

                if (Request.QueryString["entry"].ToString() != "N")
                {
                    gvRef.DataSourceID = "odsReference";
                    odsReference.SelectParameters["DocNumber"].DefaultValue = txtDoc.Text;
                    this.gvRef.Columns["CommandString"].Width = 0; 
                    this.gvRef.Columns["RCommandString"].Width = 0; 
                } 
            }
        }
        #endregion

        #region Validation
        private void Validate()
        {
            GearsLibrary.Gears.GearsParameter gparam = new Gears.GearsParameter();
            gparam._DocNo = _Entity.DocNumber;
            gparam._UserId = Session["Userid"].ToString();
            gparam._TransType = Request.QueryString["transtype"].ToString();
            gparam._Connection = Session["ConnString"].ToString();
            string strresult = GProcurement.PORevision_Validate(gparam);

            cp.JSProperties["cp_valmsg"] = strresult;//Message variable to client side
            
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
          
            var look = sender as ASPxGridLookup;
            if (look != null)
            {
                if (Request.QueryString["entry"].ToString() == "V" || Request.QueryString["entry"].ToString() == "D")
                {
                    look.ReadOnly = view;
                    look.DropDownButton.Enabled = false;
                }
            }
        }
        protected void memoremarks_Load(object sender, EventArgs e)
        {
            ((ASPxMemo)sender).ReadOnly = view;

        }
        protected void gvLookupLoad(object sender, EventArgs e)//Control for all lookup in details/grid
        {
            if (Request.QueryString["entry"].ToString() == "V" || Request.QueryString["entry"].ToString() == "D")
            {
                
                    ASPxGridLookup look = sender as ASPxGridLookup;
                    if (look != null)
                    {
                    look.Enabled = false;
                }
            }
        }
        //protected void gvTextBoxLoad(object sender, EventArgs e)//Control for all lookup in details/grid
        //{
        //    if (Request.QueryString["entry"].ToString() == "V" || Request.QueryString["entry"].ToString() == "D")
        //    {
        //        GridViewDataTextColumn text = sender as GridViewDataTextColumn;
        //        text.ReadOnly = true;
        //    }
        //    else
        //    {
        //        GridViewDataTextColumn text = sender as GridViewDataTextColumn;
        //        text.ReadOnly = false;

        //    }
        //}
        protected void gv1_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)//Control for grid
        {
            ASPxGridView grid = sender as ASPxGridView; 
            grid.SettingsBehavior.AllowGroup = false;
            grid.SettingsBehavior.AllowSort = false;
            grid.SettingsBehavior.AllowDragDrop = false;
            e.Editor.ReadOnly = view; 
        }
        protected void Date_Load(object sender, EventArgs e)//Control for all date editor
        {
            if (Request.QueryString["entry"].ToString() == "V" || Request.QueryString["entry"].ToString() == "D")
            {
                ASPxDateEdit date = sender as ASPxDateEdit;
                date.ReadOnly = view;
                date.DropDownButton.Enabled = false;
            }
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
            if (Request.QueryString["entry"].ToString() == "V")
            {
                if (e.ButtonType == ColumnCommandButtonType.Edit || e.ButtonType == ColumnCommandButtonType.Delete ||
                    e.ButtonType == ColumnCommandButtonType.New || e.ButtonType == ColumnCommandButtonType.Cancel ||
                    e.ButtonType == ColumnCommandButtonType.Update)
                    e.Visible = false;
            }
            if (e.ButtonType == ColumnCommandButtonType.Update || e.ButtonType == ColumnCommandButtonType.Cancel)
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
        }
        public void gridView_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            string column = e.Parameters.Split('|')[0];//Set column name
            if (column.Contains("GLP_AIC") || column.Contains("GLP_AC") || column.Contains("GLP_F")) return;//Traps the callback
            string itemcode = e.Parameters.Split('|')[1];//Set Item Code
            string val = e.Parameters.Split('|')[2];//Set column value
            if (val.Contains("GLP_AIC") || val.Contains("GLP_AC") || val.Contains("GLP_F")) return;//Traps the callback

            ASPxGridView grid = sender as ASPxGridView;
            ASPxGridLookup lookup = (ASPxGridLookup)gv1.FindEditRowCellTemplateControl((GridViewDataColumn)gv1.Columns[2], "glItemCode");
            var selectedValues = itemcode;
            CriteriaOperator selectionCriteria = new InOperator(lookup.KeyFieldName, new string[] { itemcode });
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

        #endregion

        #region Callback functions(Inserting/Updating/Deleting/Validating)
        protected void cp_Callback(object sender, CallbackEventArgsBase e)
        {
            _Entity.Connection = Session["ConnString"].ToString(); //ADD CONN
            _Entity.DocNumber = txtDoc.Text;
            _Entity.DocDate = String.IsNullOrEmpty(dtpDate.Text) ? null : Convert.ToDateTime(dtpDate.Text).ToShortDateString();
            _Entity.TargetDelDate = String.IsNullOrEmpty(dtpTarget.Text) ? null : Convert.ToDateTime(dtpTarget.Text).ToShortDateString();
            _Entity.CommDate = String.IsNullOrEmpty(dtpComm.Text) ? null : Convert.ToDateTime(dtpComm.Text).ToShortDateString();
            _Entity.CancellationDate = String.IsNullOrEmpty(dtpCancel.Text) ? null : Convert.ToDateTime(dtpCancel.Text).ToShortDateString();
            _Entity.Broker = txtBroker.Text;
            _Entity.OldSupplier = txtOldSupplierCode.Text;
            _Entity.Supplier = String.IsNullOrEmpty(glSupplierCode.Text) ? null : glSupplierCode.Text.ToString();
            _Entity.PODocNumber = gvPODoc.Text;
            _Entity.TotalQty = String.IsNullOrEmpty(txtQty.Text) ? 0 : Convert.ToDecimal(txtQty.Text);
            _Entity.Remarks = txtRemarks.Text;
            _Entity.PQReference = txtPQ.Text;
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
                    //string strError = Functions.Submitted(_Entity.DocNumber,"Procurement.PurchaseOrderRevision",1,Connection);//NEWADD factor 1 if submit, 2 if approve

                    //gv1.UpdateEdit();//Initiate to call gridvalidation/batchupdate method

                    //if (error == false)
                    //{
                    //    check = true;
                    //    //_Entity.InsertData(_Entity);//Method of inserting for header
                    //    _Entity.AddedBy = Session["userid"].ToString();
                    //    _Entity.UpdateData(_Entity);
                    //    gv1.DataSourceID = "odsDetail";
                    //    odsDetail.SelectParameters["DocNumber"].DefaultValue = txtDoc.Text;
                    //    gv1.UpdateEdit();//2nd initiation to insert grid
                    //    Validate();
                    //    cp.JSProperties["cp_message"] = "Successfully Added!";//Message variable to client side
                    //    cp.JSProperties["cp_success"] = true;//Success bool variable  to client side
                    //    cp.JSProperties["cp_close"] = true;//Close window variable to client side
                    //    Session["Refresh"] = "1";
                    //}
                    //else
                    //{
                    //    cp.JSProperties["cp_message"] = "Please check all the fields!";
                    //    cp.JSProperties["cp_success"] = true;
                    //}

                    //break;

                case "Update":
                  string strError = Functions.Submitted(_Entity.DocNumber,"Procurement.PurchaseOrderRevision",1,Connection);//NEWADD factor 1 if submit, 2 if approve
                  strError += Functions.Submitted(_Entity.DocNumber,"Procurement.PurchaseOrderRevision",3,Connection);//NEWADD factor 1 if submit, 2 if approve
                  if (!string.IsNullOrEmpty(strError))
                  {
                      cp.JSProperties["cp_message"] = strError;
                      cp.JSProperties["cp_success"] = true;
                      cp.JSProperties["cp_forceclose"] = true;
                      return;
                  }

                    gv1.UpdateEdit();

                    if (error == false)
                    {
                        check = true;

                        _Entity.LastEditedBy = Session["userid"].ToString();
                        _Entity.UpdateData(_Entity);//Method of inserting for header

                        gv1.KeyFieldName = "DocNumber;LineNumber";
                        if (Session["Datatable"] == "1")
                        {
                            gv1.DataSourceID = sdsPODetail.ID;
                            gv1.UpdateEdit(); 
                            //gvService.DataSourceID = odsDetail2.ID;//Renew datasourceID to entity
                            //odsDetail2.SelectParameters["DocNumber"].DefaultValue = txtDoc.Text;//Set select parameter to prevent error
                            gvService.DataSourceID = sdsPOServDetail.ID;
                            gvService.UpdateEdit();
                        }
                        else
                        {
                            gv1.DataSourceID = odsDetail.ID;//Renew datasourceID to entity
                            odsDetail.SelectParameters["DocNumber"].DefaultValue = txtDoc.Text;//Set select parameter to prevent error
                            gv1.UpdateEdit();//2nd initiation to insert grid
                            gvService.DataSourceID = odsDetail2.ID;//Renew datasourceID to entity
                            odsDetail2.SelectParameters["DocNumber"].DefaultValue = txtDoc.Text;//Set select parameter to prevent error
                            gvService.UpdateEdit();
                        }
                        Validate();
                        if (cp.JSProperties["cp_valmsg"].ToString() != "")
                        {
                            cp.JSProperties["cp_message"] = "Please check all the fields!";
                            cp.JSProperties["cp_success"] = true;
                        }
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
                    
                    break;
 

                case "PO":
                    SqlDataSource ds = Masterfilebizcustomer;
                    ds.SelectCommand = string.Format("SELECT A.DocNumber,A.SupplierCode as BizPartnerCode, B.Name, Broker from Procurement.PurchaseOrder A left join Masterfile.BizPartner B on A.SupplierCode = B. BizPartnerCode where docnumber = '" + gvPODoc.Text + "'");
                    DataView tran = (DataView)ds.Select(DataSourceSelectArguments.Empty);
                    if (tran.Count > 0)
                    {
                        glSupplierCode.Text = tran[0][1].ToString();
                        txtOldSupplierCode.Text = tran[0][1].ToString();
                        txtBroker.Text = tran[0][3].ToString();
                    }

                    ds.SelectCommand = string.Format("select TargetDeliveryDate as TargetDelDate, CommitmentDate as CommDate, CancellationDate as CancellationDate from Procurement.PurchaseOrder where docnumber = '" + gvPODoc.Text + "'");
                    DataView inb = (DataView)ds.Select(DataSourceSelectArguments.Empty); 
                    if (inb.Count > 0)
                    {
                        dtpTarget.Text = Convert.ToDateTime(inb[0][0].ToString()).ToShortDateString();
                        dtpComm.Text = Convert.ToDateTime(inb[0][1].ToString()).ToShortDateString();
                        dtpCancel.Text = Convert.ToDateTime(inb[0][2].ToString()).ToShortDateString();

                            //inb[0][0].ToString();
                            //Convert.ToDateTime(inb[0][0].ToString()).ToShortDateString();

                    }
                    GetSelectedVal();
                    GetSelectedValService();
                    cp.JSProperties["cp_generated"] = true;
                    break;
            }
        }
        //protected void grid_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        //{ //Validation for gridview, set all validate conditions here.. (sample here is checking of empty cells)
        //    //string ItemCode = "";
        //    //string ColorCode = "";
        //    //string ClassCode = "";
        //    //string SizeCode = "";
            

        //    if (e.Errors.Count > 0)
        //    {
        //        error = true; //bool to cancel adding/updating if true
        //    }
        //}
        //dictionary method to hold error 
        //void AddError(Dictionary<GridViewColumn, string> errors, GridViewColumn column, string errorText)
        //{
        //    if (errors.ContainsKey(column)) return;
        //    errors[column] = errorText;
        //}   
        protected void gv1_BatchUpdate(object sender, ASPxDataBatchUpdateEventArgs e)
        {
            ASPxGridView Grid = sender as ASPxGridView;
            if ((error == true || error == false) && check == false)//Prevents updating of grid to enable validation
            {
                e.Handled = true;
                //e.DeleteValues.Clear();
                //e.InsertValues.Clear();
                //e.UpdateValues.Clear();
            }
            if (Session["Datatable"] == "1" && check == true && Grid.ID == "gv1")
            {
                e.Handled = true;
                DataTable source = GetSelectedVal();

                // Removing all deleted rows from the data source(Excel file)
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

                // Updating required rows
                foreach (ASPxDataUpdateValues values in e.UpdateValues)
                {
                    object[] keys = { values.NewValues["DocNumber"],values.NewValues["LineNumber"] };
                    DataRow row = source.Rows.Find(keys); 
                    row["OldUnitCost"] = values.NewValues["OldUnitCost"];
                    row["NewUnitCost"] = values.NewValues["NewUnitCost"];
                    row["IsVAT"] = values.NewValues["IsVAT"];
                    row["VATCode"] = values.NewValues["VATCode"];
                    row["OldOrderQty"] = values.NewValues["OldOrderQty"];
                    row["OrderQty"] = values.NewValues["OrderQty"];
                    row["Unit"] = values.NewValues["Unit"];
                    row["UnitCost"] = values.NewValues["UnitCost"];
                    row["BaseQty"] = values.NewValues["BaseQty"];
                   // row["AverageCost"] = values.NewValues["AverageCost"];
                    row["StatusCode"] = values.NewValues["StatusCode"];
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

                Gears.RetriveData2("Delete from procurement.PurchaseOrderRevisionDetail where DocNumber = '" + txtDoc.Text + "'", Session["ConnString"].ToString());

                foreach (DataRow dtRow in source.Rows)//This is where the data will be inserted into db
                {
                    _EntityDetail.PRNumber = dtRow["PRNumber"].ToString();
                    _EntityDetail.ItemCode = dtRow["ItemCode"].ToString();
                    _EntityDetail.ColorCode = dtRow["ColorCode"].ToString();
                    _EntityDetail.ClassCode = dtRow["ClassCode"].ToString();
                    _EntityDetail.SizeCode = dtRow["SizeCode"].ToString();
                    _EntityDetail.OldOrderQty = Convert.ToDecimal(string.IsNullOrEmpty(dtRow["OldOrderQty"].ToString()) ? "0" : dtRow["OldOrderQty"].ToString());
                    _EntityDetail.OrderQty = Convert.ToDecimal(string.IsNullOrEmpty(dtRow["OrderQty"].ToString()) ? "0" : dtRow["OrderQty"].ToString());
                    _EntityDetail.Unit = dtRow["Unit"].ToString();
                    _EntityDetail.UnitCost = Convert.ToDecimal(string.IsNullOrEmpty(dtRow["UnitCost"].ToString()) ? "0" : dtRow["UnitCost"].ToString());
                    _EntityDetail.BaseQty = Convert.ToDecimal(string.IsNullOrEmpty(dtRow["BaseQty"].ToString()) ? "0" : dtRow["BaseQty"].ToString());
                    
                   // _EntityDetail.AverageCost = Convert.ToDecimal(string.IsNullOrEmpty(dtRow["AverageCost"].ToString()) ? "0" : dtRow["AverageCost"].ToString());
                    _EntityDetail.StatusCode = dtRow["StatusCode"].ToString();
                    _EntityDetail.OldUnitCost = Convert.ToDecimal(string.IsNullOrEmpty(dtRow["OldUnitCost"].ToString()) ? "0" : dtRow["OldUnitCost"].ToString());
                    _EntityDetail.NewUnitCost = Convert.ToDecimal(string.IsNullOrEmpty(dtRow["NewUnitCost"].ToString()) ? "0" : dtRow["NewUnitCost"].ToString());
                    _EntityDetail.IsVAT = Convert.ToBoolean(Convert.IsDBNull(dtRow["IsVAT"]) ? false : dtRow["IsVAT"]);
                    _EntityDetail.VATCode = dtRow["VATCode"].ToString();
                    _EntityDetail.Field1 = dtRow["Field1"].ToString();
                    _EntityDetail.Field2 = dtRow["Field2"].ToString();
                    _EntityDetail.Field3 = dtRow["Field3"].ToString();
                    _EntityDetail.Field4 = dtRow["Field4"].ToString();
                    _EntityDetail.Field5 = dtRow["Field5"].ToString();
                    _EntityDetail.Field6 = dtRow["Field6"].ToString();
                    _EntityDetail.Field7 = dtRow["Field7"].ToString();
                    _EntityDetail.Field8 = dtRow["Field8"].ToString();
                    _EntityDetail.Field9 = dtRow["Field9"].ToString();
                    
                    _EntityDetail.AddPurchaseOrderRevisionDetail(_EntityDetail);

                    //_EntityDetail.DeleteOutboundDetail(_EntityDetail);
                }
            }

            if (Session["Datatable"] == "1" && check == true && Grid.ID == "gvService")
            {
                e.Handled = true;
                DataTable source = GetSelectedValService();

                // Removing all deleted rows from the data source(Excel file)
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

                // Updating required rows
                foreach (ASPxDataUpdateValues values in e.UpdateValues)
                {
                    object[] keys = { values.NewValues["DocNumber"], values.NewValues["LineNumber"] };
                    DataRow row = source.Rows.Find(keys);
                    row["Service"] = values.NewValues["Service"];
                    row["Description"] = values.NewValues["Description"];
                    row["OldServiceQty"] = values.NewValues["OldServiceQty"];
                    row["NewServiceQty"] = values.NewValues["NewServiceQty"];
                    row["Unit"] = values.NewValues["Unit"];
                    row["OldUnitCost"] = values.NewValues["OldUnitCost"];
                    row["NewUnitCost"] = values.NewValues["NewUnitCost"];
                    row["OldTotalCost"] = values.NewValues["OldTotalCost"];
                    row["NewTotalCost"] = values.NewValues["NewTotalCost"];
                    row["AllowProgressBilling"] = values.NewValues["AllowProgressBilling"];
                    row["VatLiable"] = values.NewValues["VatLiable"];
                    row["VatCode"] = values.NewValues["VatCode"];
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

                Gears.RetriveData2("Delete from procurement.PurchaseOrderRevisionService where DocNumber = '" + txtDoc.Text + "'", Session["ConnString"].ToString());

                foreach (DataRow dtRow in source.Rows)//This is where the data will be inserted into db
                {
                    _EntityDetailServ.PRNumber = dtRow["PRNumber"].ToString();
                    _EntityDetailServ.Service = dtRow["Service"].ToString();
                    _EntityDetailServ.Description = dtRow["Description"].ToString();
                    _EntityDetailServ.OldServiceQty = Convert.ToDecimal(string.IsNullOrEmpty(dtRow["OldServiceQty"].ToString()) ? "0" : dtRow["OldServiceQty"].ToString());
                    _EntityDetailServ.NewServiceQty = Convert.ToDecimal(string.IsNullOrEmpty(dtRow["NewServiceQty"].ToString()) ? "0" : dtRow["NewServiceQty"].ToString());
                    _EntityDetailServ.Unit = dtRow["Unit"].ToString();
                    _EntityDetailServ.OldUnitCost = Convert.ToDecimal(string.IsNullOrEmpty(dtRow["OldUnitCost"].ToString()) ? "0" : dtRow["OldUnitCost"].ToString());
                    _EntityDetailServ.NewUnitCost = Convert.ToDecimal(string.IsNullOrEmpty(dtRow["NewUnitCost"].ToString()) ? "0" : dtRow["NewUnitCost"].ToString());
                    _EntityDetailServ.OldTotalCost = Convert.ToDecimal(string.IsNullOrEmpty(dtRow["OldTotalCost"].ToString()) ? "0" : dtRow["OldTotalCost"].ToString());
                    _EntityDetailServ.NewTotalCost = Convert.ToDecimal(string.IsNullOrEmpty(dtRow["NewTotalCost"].ToString()) ? "0" : dtRow["NewTotalCost"].ToString());
                    _EntityDetailServ.AllowProgressBilling = Convert.ToBoolean(Convert.IsDBNull(dtRow["AllowProgressBilling"]) ? false : dtRow["AllowProgressBilling"]);
                    _EntityDetailServ.VatLiable = Convert.ToBoolean(Convert.IsDBNull(dtRow["VatLiable"]) ? false : dtRow["VatLiable"]);
                    _EntityDetailServ.VatCode = dtRow["VatCode"].ToString();
                      
                    _EntityDetailServ.Field1 = dtRow["Field1"].ToString();
                    _EntityDetailServ.Field2 = dtRow["Field2"].ToString();
                    _EntityDetailServ.Field3 = dtRow["Field3"].ToString();
                    _EntityDetailServ.Field4 = dtRow["Field4"].ToString();
                    _EntityDetailServ.Field5 = dtRow["Field5"].ToString();
                    _EntityDetailServ.Field6 = dtRow["Field6"].ToString();
                    _EntityDetailServ.Field7 = dtRow["Field7"].ToString();
                    _EntityDetailServ.Field8 = dtRow["Field8"].ToString();
                    _EntityDetailServ.Field9 = dtRow["Field9"].ToString();

                    _EntityDetailServ.AddPurchasedOrderService(_EntityDetailServ); 
                }
            }
        }
        #endregion

        protected void gvSupplier_TextChanged(object sender, EventArgs e)
        {

        }


        private DataTable GetSelectedVal()
        {
            DataTable dt = new DataTable();
            string[] selectedValues =  gvPODoc.Text.Split(';');
            CriteriaOperator selectionCriteria = new InOperator(gvPODoc.KeyFieldName, selectedValues);
            sdsPODetail.SelectCommand = "SELECT A.DocNumber,LineNumber,A.PRNumber, A.ItemCode,ColorCode,ClassCode,SizeCode,UnitCost as OldUnitCost,UnitCost as NewUnitCost," +
                                        "OrderQty AS OldOrderQty, OrderQty,Unit,StatusCode,UnitCost,BaseQty,IsVAT,VATCode,C.FullDesc AS ItemDescription," +
                                        "A.Field1,A.Field2,A.Field3,A.Field4,A.Field5,A.Field6,A.Field7,A.Field8,A.Field9 " +
                                        "FROM Procurement.PurchaseOrderDetail A with (nolock)  INNER JOIN Procurement.PurchaseOrder B " +
                                        "ON A.DocNumber = B.DocNumber LEFT JOIN Masterfile.Item C on A.ItemCode = C.ItemCode WHERE ISNULL(SubmittedBy,'')!=''" +
                                        "and a.DocNumber = '" + gvPODoc.Text+"'";
            Session["RRses"] = sdsPODetail.SelectCommand;
            gv1.DataSourceID = sdsPODetail.ID;
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
            dt.Columns["DocNumber"],dt.Columns["LineNumber"]};

            return dt;
        }

        private DataTable GetSelectedValService()
        {
            DataTable dt = new DataTable();
            sdsPOServDetail.SelectCommand = "SELECT A.DocNumber,RIGHT('00000'+ CAST(ROW_NUMBER() OVER (ORDER BY A.DocNumber) AS VARCHAR(5)),5) AS LineNumber, A.PRNumber," +
                                "A.ServiceType as Service,A.Description," +
                                "ServiceQty as OldServiceQty,ServiceQty as NewServiceQty,A.Unit,UnitCost as OldUnitCost," +
                                "UnitCost as NewUnitCost,TotalCost as OldTotalCost,TotalCost as NewTotalCost," +
                                "IsAllowProgressBilling as AllowProgressBilling,IsVat as VatLiable,VatCode," +
                                "A.Field1,A.Field2,A.Field3,A.Field4,A.Field5,A.Field6,A.Field7," +
                                "A.Field8,A.Field9 FROM Procurement.PurchaseOrderService A " +
                                "INNER JOIN Procurement.PurchaseOrder B ON A.DocNumber = B.DocNumber " +
                                "WHERE A.DocNumber = '" + gvPODoc.Text + "'";

            gvService.DataSourceID = sdsPOServDetail.ID;
            gvService.DataBind();

            Session["Datatable"] = "1";

            foreach (GridViewColumn col in gvService.VisibleColumns)
            {
                GridViewDataColumn dataColumn = col as GridViewDataColumn;
                if (dataColumn == null) continue;
                dt.Columns.Add(dataColumn.FieldName);
            }
            for (int i = 0; i < gvService.VisibleRowCount; i++)
            {
                DataRow row = dt.Rows.Add();
                foreach (DataColumn col in dt.Columns)
                    row[col.ColumnName] = gvService.GetRowValues(i, col.ColumnName);
            }

            dt.PrimaryKey = new DataColumn[] { //Sets datatable's primary key for batchupdate reference during adding.
            dt.Columns["DocNumber"],dt.Columns["LineNumber"]};

            return dt;
        }

        protected void dtpDate_Init(object sender, EventArgs e)
        {
            if (Request.QueryString["entry"] == "N")
            {
                dtpDate.Date = DateTime.Now;
            }
        }

        protected void dtpTarget_Init(object sender, EventArgs e)
        {
            if (Request.QueryString["entry"] == "N")
            {
                dtpTarget.Date = DateTime.Now;
            }
        }

        protected void dtpComm_Init(object sender, EventArgs e)
        {
            if (Request.QueryString["entry"] == "N")
            {
                dtpComm.Date = DateTime.Now;
            }
        }
        protected void dtpCancel_Init(object sender, EventArgs e)
        {
            if (Request.QueryString["entry"] == "N")
            {
                dtpCancel.Date = DateTime.Now;
            }
        }
        protected void Connection_Init(object sender, EventArgs e)
        { 
            ((SqlDataSource)sender).ConnectionString = Session["ConnString"].ToString();

        } 

        protected void gvPRDetails_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        { 
            //string PRSelect = "SELECT A.DocNumber,RIGHT('00000'+ CAST(ROW_NUMBER() OVER (ORDER BY A.DocNumber) AS VARCHAR(5)),5) AS LineNumber," +
            //                    "A.ServiceType as Service,A.Description," +
            //                    "ServiceQty as OldServiceQty,ServiceQty as NewServiceQty,A.Unit,UnitCost as OldUnitCost," +
            //                    "UnitCost as NewUnitCost,TotalCost as OldTotalCost,TotalCost as NewTotalCost," +
            //                    "IsAllowProgressBilling as AllowProgressBilling,IsVat as VatLiable,VatCode," +
            //                    "A.Field1,A.Field2,A.Field3,A.Field4,A.Field5,A.Field6,A.Field7," +
            //                    "A.Field8,A.Field9 FROM Procurement.PurchaseOrderService A " +
            //                    "INNER JOIN Procurement.PurchaseOrder B ON A.DocNumber = B.DocNumber " +
            //                    "WHERE A.DocNumber = '" + gvPODoc.Text + "'";

            //DataTable gvPR = Gears.RetriveData2(PRSelect, Session["ConnString"].ToString());
            //gvPRDetails.DataSource = gvPR;
            //gvPRDetails.DataBind(); 
        }
    }
}