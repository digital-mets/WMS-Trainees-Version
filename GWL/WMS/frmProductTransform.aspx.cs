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


namespace GWL
{
    public partial class frmProductTransform : System.Web.UI.Page
    {
        Boolean error = false;//Boolean for grid validation
        Boolean view = false;//Boolean for view state
        Boolean check = false;//Boolean for grid validation

        private static string Connection;

        Entity.ProductTransform _Entity = new ProductTransform();//Calls entity odsHeader
        Entity.ProductTransform.ProductTransformDetail _EntityDetail = new ProductTransform.ProductTransformDetail();

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

            gv1.KeyFieldName = "DocNumber;LineNumber";

            //Details checking. changes datasourceid of gridview if there's no details for a certain docnumber; to enable adding of details.
            if (Request.QueryString["entry"].ToString() == "V")
            {
                view = true;//sets view mode for entry
            }
            if (!IsPostBack)
            {
                Session["Datatable"] = null;
                Session["ptdetail"] = null;
                //Connection = Session["ConnString"].ToString();
                Connection = Session["ConnString"].ToString();

                switch (Request.QueryString["entry"].ToString())
                {
                    case "N":
                        updateBtn.Text = "Add";
                        break;
                    case "E":
                        updateBtn.Text = "Update";
                        txtDocnumber.ReadOnly = true;
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

                if (Request.QueryString["entry"].ToString() == "N")
                {

                    gv1.DataSourceID = "sdsDetail";

                    //popup.ShowOnPageLoad = false;
                }



                //txtDocnumber.Value = Session["DocNumber"].ToString(); //sets docnumber from session
                txtDocnumber.ReadOnly = true;
                txtDocnumber.Value = Request.QueryString["docnumber"].ToString(); //sets docnumber from session

                //ADD CONN //Method for retrieving data from entity
                _Entity.getdata(txtDocnumber.Text, Session["ConnString"].ToString());
                deDocDate.Text = String.IsNullOrEmpty(_Entity.DocDate) ? DateTime.Now.ToShortDateString() : Convert.ToDateTime(_Entity.DocDate).ToShortDateString();
                glStorerKey.Text = _Entity.StorerKey;
                glDocnumber.Text = _Entity.OCNdocnumber;
                refDocDate.Text = String.IsNullOrEmpty(_Entity.RefDocDate) ? DateTime.Now.ToShortDateString() : Convert.ToDateTime(_Entity.RefDocDate).ToShortDateString();

                //set the gv1 datasource
                gv1.DataSourceID = "odsDetail";



            }


        }
        #endregion

        #region Validation
        private void Validate()
        {
            //GearsLibrary.Gears.GearsParameter gparam = new Gears.GearsParameter();
            //gparam._Connection = Connection;
            //gparam._DocNo = _Entity.Docnumber;
            //gparam._UserId = Session["Userid"].ToString();
            //gparam._TransType = Request.QueryString["transtype"].ToString();

            //string strresult = GWarehouseManagement.ICN_Validate(gparam);

            //cp.JSProperties["cp_valmsg"] = strresult;//Message variable to client side
        }
        #endregion

        #region Set controls' state/behavior/etc...
        protected void TextboxLoad(object sender, EventArgs e)//Control for all textbox
        {
            ASPxTextBox text = sender as ASPxTextBox;
            text.ReadOnly = view;
        }
        protected void CheckboxLoad(object sender, EventArgs e)//Control for all textbox
        {
            ASPxCheckBox text = sender as ASPxCheckBox;
            text.ReadOnly = view;
        }
        protected void LookupLoad(object sender, EventArgs e)//Control for all lookup in header
        {
            var look = sender as ASPxGridLookup;
            if (look != null)
            {
                look.ReadOnly = view;
            }
        }
        protected void gvLookupLoad(object sender, EventArgs e)//Control for all lookup in details/grid
        {
            if (!IsPostBack && !IsCallback)
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
        }
        protected void gv1_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)//Control for grid
        {
            if (!IsPostBack && !IsCallback)
            {
                if (Request.QueryString["entry"].ToString() == "V" || Request.QueryString["entry"].ToString() == "D")
                {
                    ASPxGridView grid = sender as ASPxGridView;
                    //grid.SettingsBehavior.AllowGroup = false;
                    //grid.SettingsBehavior.AllowSort = false;
                    e.Editor.ReadOnly = view;
                }
            }

        }
        protected void Date_Load(object sender, EventArgs e)//Control for all date editor
        {
            ASPxDateEdit date = sender as ASPxDateEdit;
            date.ReadOnly = view;
        }
        protected void SpinEdit_Load(object sender, EventArgs e)//Control for all numeric entries in header
        {
            ASPxSpinEdit spinedit = sender as ASPxSpinEdit;
            spinedit.ReadOnly = view;
        }
        protected void gv_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
        {   //Sets icon for grid/Control for Enabling/Disabling of buttons on grid.
            if (!IsPostBack && !IsCallback)
            {
                if (Request.QueryString["entry"].ToString() == "V")
                {
                    if (e.ButtonType == ColumnCommandButtonType.Edit || e.ButtonType == ColumnCommandButtonType.Delete ||
                        e.ButtonType == ColumnCommandButtonType.New || e.ButtonType == ColumnCommandButtonType.Update ||
                        e.ButtonType == ColumnCommandButtonType.Cancel)
                        e.Visible = false;
                }
            }
            if (e.ButtonType == ColumnCommandButtonType.Update)
                e.Visible = false;
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
        public void cleardata()
        {



            gv1.DataSource = null;

            gv1.Columns.Clear();
        }


        public void gridView_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            string column = e.Parameters.Split('|')[0];//Set column name
            if (column.Contains("GLP_AIC") || column.Contains("GLP_AC") || column.Contains("GLP_F")) return;//Traps the callback
            string itemcode = e.Parameters.Split('|')[1];//Set Item Code
            string customerCode = e.Parameters.Split('|')[1];//set the customer code
            string val = e.Parameters.Split('|')[2];//Set column value
            string val2 = "";
            try
            {
                val2 = e.Parameters.Split('|')[3];
            }
            catch (Exception)
            {
                val2 = "";
            }
            if (val.Contains("GLP_AIC") || val.Contains("GLP_AC") || val.Contains("GLP_F")) return;//Traps the callback
            var itemlookup = sender as ASPxGridView;
            string qty = "";
            decimal totalqty = 0;
            string codes = "";

            if (val2 == "")
            {
                val2 = "0";
            }

            if (e.Parameters.Contains("Customer"))
            {
                DataTable getCustomer = Gears.RetriveData2("Select bizpartnercode from masterfile.bizpartner where bizpartnercode = '" + customerCode + "'", Session["ConnString"].ToString());
                foreach (DataRow dt in getCustomer.Rows)
                {
                    codes = dt["Customer"].ToString();
                }
                itemlookup.JSProperties["cp_codes"] = codes;
            }
            else if (e.Parameters.Contains("DocDate"))
            {
                DataTable getCustomer = Gears.RetriveData2("Select bizpartnercode from masterfile.bizpartner where bizpartnercode = '" + customerCode + "'", Session["ConnString"].ToString());
                foreach (DataRow dt in getCustomer.Rows)
                {
                    codes = dt["Customer"].ToString();
                }
                itemlookup.JSProperties["cp_codes"] = codes;
            }
            else
            {
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

        }

        #endregion

        #region Callback functions(Inserting/Updating/Deleting/Validating)
        protected void cp_Callback(object sender, CallbackEventArgsBase e)
        {
            //else
            //{

            _Entity.Connection = Session["ConnString"].ToString(); //ADD CONN
            _Entity.Docnumber = txtDocnumber.Text;
            _Entity.DocDate = deDocDate.Text;
            _Entity.RefDocDate = refDocDate.Text;
            _Entity.StorerKey = String.IsNullOrEmpty(glStorerKey.Text) ? null : glStorerKey.Value.ToString();
            _Entity.OCNdocnumber = String.IsNullOrEmpty(glDocnumber.Text) ? null : glDocnumber.Value.ToString();
            _Entity.LastEditedBy = Session["userid"].ToString();
            _Entity.LastEditedDate = DateTime.Now.ToString();
            _Entity.AddedBy = Session["userid"].ToString();
            _Entity.AddedDate = DateTime.Now.ToString();
            string strError = "";
            switch (e.Parameter)
            {
                case "Add":


                    if (error == false)
                    {
                        check = true;
                        _Entity.UpdateData(_Entity);//Method of Updating header
                        if (Session["Datatable"] == "1")
                        {
                            gv1.DataSourceID = sdsDetail.ID;
                            gv1.UpdateEdit();

                        }
                        else
                        {
                            gv1.DataSourceID = "odsDetail";
                            odsDetail.SelectParameters["DocNumber"].DefaultValue = glDocnumber.Text;
                            gv1.UpdateEdit();//2nd Initiation to update grid
                        }

                        Validate();
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

                        _Entity.UpdateData(_Entity);//Method of Updating header
                        if(Session["Datatable"] == "1"){
                            gv1.DataSourceID = sdsDetail.ID;
                            gv1.UpdateEdit();

                           }
                        else {
                            gv1.DataSourceID = "odsDetail";
                            odsDetail.SelectParameters["DocNumber"].DefaultValue = glDocnumber.Text;
                            gv1.UpdateEdit();//2nd Initiation to update grid
                        }
                        
                        Validate();
                        cp.JSProperties["cp_message"] = "Successfully Updated!";
                        cp.JSProperties["cp_success"] = true;
                        cp.JSProperties["cp_close"] = true;
                        Session["Datatable"] = null;
                        gv1.DataSource = null;
                        Session["Refresh"] = "1";
                    }
                    else
                    {
                        cp.JSProperties["cp_message"] = "Please check all the fields!";
                        cp.JSProperties["cp_success"] = true;
                    }

                    break;

                case "Delete":
                    strError = Functions.Submitted(_Entity.Docnumber, "WMS.ProductTransform", 1, Connection);//NEWADD factor 1 if submit, 2 if approve

                    if (!string.IsNullOrEmpty(strError))
                    {
                        cp.JSProperties["cp_message"] = strError;
                        cp.JSProperties["cp_success"] = true;
                        cp.JSProperties["cp_forceclose"] = true;
                        return;
                    }

                    cp.JSProperties["cp_delete"] = true;
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
                case "CC":
                    //filters the ocn docnumber after you select a value from customer 
                    CriteriaOperator selectionCriteria = new InOperator("StorerKey", new string[] { glStorerKey.Text });
                    OCN.FilterExpression = (GroupOperator.Combine(GroupOperatorType.And, selectionCriteria)).ToString();
                    glDocnumber.DataSourceID = "OCN";
                    glDocnumber.DataBind();
                    break;
                case "Search":
                    GetInfo();
                    cp.JSProperties["cp_search"] = true;
                    break;



            }
            //}
        }
        protected void grid_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        { //Validation for gridview, set all validate conditions here.. (sample here is checking of empty cells)
            //string ItemCode = "";
            //string ColorCode = "";
            //string ClassCode = "";
            //string SizeCode = "";
            foreach (GridViewColumn column in gv1.Columns)
            {
                GridViewDataColumn dataColumn = column as GridViewDataColumn;
                if (dataColumn == null) continue;
                if ((e.NewValues[dataColumn.FieldName] == null || e.NewValues[dataColumn.FieldName].ToString() == "") && dataColumn.FieldName != "Docnumber" && dataColumn.FieldName != "LineNumber" && dataColumn.FieldName != "MfgDate" && dataColumn.FieldName != "ExpiryDate"
                     && dataColumn.FieldName != "Quantity" && dataColumn.FieldName != "Itemcode" && dataColumn.FieldName != "UOM" && dataColumn.FieldName != "Kilos" && dataColumn.FieldName != "PalletID" && dataColumn.FieldName != "Batchno")
                {
                    e.Errors[dataColumn] = "Value can't be null.";//Sets error tooltip message
                }
            }

            if (e.Errors.Count > 0)
            {
                error = true; //bool to cancel adding/updating if true
            }
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
                //e.DeleteValues.Clear();
                e.InsertValues.Clear();
                e.UpdateValues.Clear();
            }
            if (Session["Datatable"] == "1" && check == true)
            {
                e.Handled = true;
                DataTable source = GetInfo();

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

                 int i = 0;
                 foreach (ASPxDataInsertValues values in e.InsertValues)
                 {
                     var LineNumber = i;
                     var DocNumber = values.NewValues["DocNumber"];
                     var MfgDate = values.NewValues["MfgDate"];
                     var ExpiryDate = values.NewValues["ExpiryDate"];
                     var Itemcode = values.NewValues["Itemcode"];
                     var PalletID = values.NewValues["PalletID"];
                     var Quantity = values.NewValues["Quantity"];
                     var Kilos = values.NewValues["Kilos"];
                     var Batchno = values.NewValues["Batchno"];
                     var UOM = values.NewValues["UOM"];

                     //var Field1 = values.NewValues["Field1"];
                     //var Field2 = values.NewValues["Field2"];
                     //var Field3 = values.NewValues["Field3"];
                     //var Field4 = values.NewValues["Field4"];
                     //var Field5 = values.NewValues["Field5"];
                     //var Field6 = values.NewValues["Field6"];
                     //var Field7 = values.NewValues["Field7"];
                     //var Field8 = values.NewValues["Field8"];
                     //var Field9 = values.NewValues["Field9"];
                 }
                 foreach (DataRow dtRow in source.Rows)//This is where the data of product transform detail will be inserted into db
                 {
                     //check if there is already existing value base from the query so that it wont contain any duplicates inside the sql
                     DataTable checkData = Gears.RetriveData2("Select * from wms.[ProductTransformDetail] where ReferenceDocNumber ='" + txtDocnumber.Text + "' and DocNumber ='" + dtRow["DocNumber"].ToString() + "' "+
                                                                        "and PalletID = '" + dtRow["PalletID"].ToString() + "' and Customercode = '" + glStorerKey.Text + "'", _Entity.Connection);
                     if (checkData.Rows.Count > 0)
                     {
                         continue;
                     } 

                     string[] array = {"",null};
                     bool qty = Array.Exists(array,element => element == dtRow["Quantity"].ToString());
                     bool kilo = Array.Exists(array,element => element == dtRow["Kilos"].ToString());

                     if (qty)
                     {
                         dtRow["Quantity"] = 0;
                     }
                     if (kilo)
                     {
                         dtRow["Kilos"] = 0;
                     }
                     _EntityDetail.RefDoc = txtDocnumber.Text;//current Docnumber
                     _EntityDetail.DocNumber = dtRow["DocNumber"].ToString();//OCN docnumber in Table
                     _EntityDetail.Customercode = glStorerKey.Text;
                     _EntityDetail.PalletID =  dtRow["PalletID"].ToString();
                     _EntityDetail.Itemcode = dtRow["ItemCode"].ToString();
                     _EntityDetail.Batchno =dtRow["Batchno"].ToString() ;
                     _EntityDetail.UOM =  dtRow["UOM"].ToString();
                     _EntityDetail.Kilos = Convert.ToDecimal(dtRow["Kilos"].ToString());
                     _EntityDetail.Quantity = Convert.ToDecimal(dtRow["Quantity"].ToString()) ;
                     _EntityDetail.ExpiryDate = Convert.ToDateTime(dtRow["ExpiryDate"].ToString()) ;
                     _EntityDetail.MfgDate = Convert.ToDateTime(dtRow["MfgDate"].ToString());

                     _EntityDetail.AddProductDetail(_EntityDetail);
                 
                 
                 }
            }
            
        }
        #endregion
        protected void gv1_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["ptdetail"] = null;
            }

            if (Session["ptdetail"] != null)
            {
                gv1.DataSourceID = sdsDetail.ID;
                //sdsDetail = Session["ptdetail"].ToString();
                //gridview.DataSourceID = null;
            }
        }
        protected void Connection_Init(object sender, EventArgs e)
        {

            ((SqlDataSource)sender).ConnectionString = Session["ConnString"].ToString();

            //sdsDetail.ConnectionString = Session["ConnString"].ToString();
            //Masterfileitem.ConnectionString = Session["ConnString"].ToString();
            //Masterfileitemdetail.ConnectionString = Session["ConnString"].ToString();
            //Warehouse.ConnectionString = Session["ConnString"].ToString();
            //Masterfilebiz.ConnectionString = Session["ConnString"].ToString();
            //Masterfilebizcustomer.ConnectionString = Session["ConnString"].ToString();
            //OCN.ConnectionString = Session["ConnString"].ToString();
            //Unit.ConnectionString = Session["ConnString"].ToString();
            //Plant.ConnectionString = Session["ConnString"].ToString();
        }

        protected void glItemCode_Init(object sender, EventArgs e)
        {
            //10/5/2016 emc add filter by customer
            ASPxGridLookup gridLookup = sender as ASPxGridLookup;
            if (IsCallback && Request.Params["__CALLBACKID"].Contains("gv1")
                && Request.Params["__CALLBACKID"].Contains(gridLookup.ID))
            {
                gridLookup.GridView.CustomCallback += new ASPxGridViewCustomCallbackEventHandler(ItemCodeLookup_CustomCallback);
                if (Session["itemsql"] != null)
                {
                    Masterfileitem.SelectCommand = Session["itemsql"].ToString();
                    Masterfileitem.DataBind();
                }

                //gridLookup.GridView.DataSourceID = Masterfileitem.ID; 
                //gridLookup.GridView.DataSource = Masterfileitem;

            }
        }
        private DataTable GetInfo()
        {
            gv1.DataSourceID = null;
            gv1.DataBind();
            DataTable data = new DataTable();
            // changes the query of the data sdsdetail inside the product transform
            sdsDetail.SelectCommand = "Select a.[DocNumber],a.[DocDate],a.[StorerKey] as Customercode,e.[LineNumber]," +
                                                               "c.[ItemCode] as Itemcode,c.[BulkQty] as Quantity,c.[BulkUnit] as UOM,c.[Qty] as Kilos," +
                                                                  " e.[BatchNo] as Batchno,e.[ExpiryDate],e.[PalletID],e.[Mkfgdate] as MfgDate from wms.[ocn] a " +
                                                                   "left join wms.[OCNDetail] c on a.[DocNumber] = c.[DocNumber] " +
                                                                   "left join wms.[PicklistDetail] e on a.[DocNumber] = e.[DocNumber] AND C.LineNumber = E.LineNumber " +
                                                                   "where a.[StorerKey] = '" + glStorerKey.Text + "' and a.[DocNumber] = '" + glDocnumber.Text + "' and a.[DocDate] = '" + refDocDate.Text + "' and a.[SubmittedDate] is not null";
            Session["ptdetail"] = sdsDetail;
            gv1.DataSourceID = sdsDetail.ID;
            gv1.DataBind();

            Session["Datatable"] = "1";

            foreach (GridViewColumn col in gv1.VisibleColumns)
            {
                GridViewDataColumn dataColumn = col as GridViewDataColumn;
                if (dataColumn == null) continue;
                data.Columns.Add(dataColumn.FieldName);
            }
            for (int i = 0; i < gv1.VisibleRowCount; i++)
            {
                DataRow row = data.Rows.Add();
                foreach (DataColumn col in data.Columns)
                    row[col.ColumnName] = gv1.GetRowValues(i, col.ColumnName);
            }
            data.PrimaryKey = new DataColumn[] { //Sets datatable's primary key for batchupdate reference during adding.
            data.Columns["LineNumber"]};

            return data;
        }


        public void ItemCodeLookup_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            //10/5/2016 emc add filter by customer
            if (e.Parameters.Contains("GLP_AIC") || e.Parameters.Contains("GLP_AC") || e.Parameters.Contains("GLP_F")) return;//Traps the callback
            ASPxGridView ItemcodeList = sender as ASPxGridView;
            string CustomerCode = e.Parameters.Split('|')[1];//Set Customer

            if (e.Parameters.Contains("ItemCodeDropDown"))
            {

                DataTable getCustomer = Gears.RetriveData2("SELECT ISNULL(Field1,'') as  Field1 FROM Masterfile.BizPartner WHERE ISNULL(IsInactive,0)=0 AND BizPartnerCode = '" + CustomerCode + "' AND ISNULL(Field1,'')!='' ", Session["ConnString"].ToString()); //ADD CONN

                if (getCustomer.Rows.Count > 0)
                {
                    CustomerCode = getCustomer.Rows[0][0].ToString();
                }


                Masterfileitem.SelectCommand = "SELECT [ItemCode], [FullDesc], [ShortDesc],Customer FROM Masterfile.[Item] where isnull(IsInactive,'')=0 AND Customer IN ('" + CustomerCode + "') ";
                Session["itemsql"] = Masterfileitem.SelectCommand;
                //ItemcodeList.DataSource = Masterfileitem;
                ItemcodeList.DataBind();
            }
            if (e.Parameters.Contains("OCNDropdown"))
            {

                DataTable getCustomer = Gears.RetriveData2("SELECT ISNULL(Field1,'') as  Field1 FROM Masterfile.BizPartner WHERE ISNULL(IsInactive,0)=0 AND BizPartnerCode = '" + CustomerCode + "' AND ISNULL(Field1,'')!='' ", Session["ConnString"].ToString()); //ADD CONN

                if (getCustomer.Rows.Count > 0)
                {
                    CustomerCode = getCustomer.Rows[0][0].ToString();
                }


                Masterfileitem.SelectCommand = "SELECT [Docnumber], [WarehouseCode] FROM wms.[ocn] where  SubmittedDate is not null AND StorerKey IN ('" + CustomerCode + "') ";
                Session["itemsql"] = Masterfileitem.SelectCommand;
                //ItemcodeList.DataSource = Masterfileitem;
                ItemcodeList.DataBind();
            }
            if (e.Parameters.Contains("DocDate"))
            {

                DataTable getCustomer = Gears.RetriveData2("SELECT ISNULL(Field1,'') as  Field1 FROM Masterfile.BizPartner WHERE ISNULL(IsInactive,0)=0 AND BizPartnerCode = '" + CustomerCode + "' AND ISNULL(Field1,'')!='' ", Session["ConnString"].ToString()); //ADD CONN

                if (getCustomer.Rows.Count > 0)
                {
                    CustomerCode = getCustomer.Rows[0][0].ToString();
                }


                Masterfileitem.SelectCommand = "SELECT [DocDate] FROM wms.[inbound] where SubmittedDate is not null AND Customer IN ('" + CustomerCode + "') ";
                Session["itemsql"] = Masterfileitem.SelectCommand;
                //ItemcodeList.DataSource = Masterfileitem;
                ItemcodeList.DataBind();
            }
        }


    }
}