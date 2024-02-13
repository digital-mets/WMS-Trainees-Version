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



namespace GWL
{
    public partial class frmPalletMaster : System.Web.UI.Page
    {
        Boolean error = false;//Boolean for grid validation
        Boolean view = false;//Boolean for view state
        Boolean check = false;//Boolean for grid validation

        private static string Connection;

        Entity.PalletMaster _Entity = new PalletMaster();//Calls entity odsHeader
        Entity.PalletMaster.PalletDetail _EntityDetail = new PalletMaster.PalletDetail();

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

            gv1.KeyFieldName = "PalletID;LineNumber";

            //Details checking. changes datasourceid of gridview if there's no details for a certain docnumber; to enable adding of details.
            if (Request.QueryString["entry"].ToString() == "V")
            {
                view = true;//sets view mode for entry
            }
            if (!IsPostBack)
            {
                Connection = Session["ConnString"].ToString();
                switch (Request.QueryString["entry"].ToString())
                {
                    case "N":
                        updateBtn.Text = "Add";
                        break;
                    case "E":
                        updateBtn.Text = "Update";
                        CustomerCode.ClientEnabled = false;
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
                else
                {
                    txtPallet.Value = Request.QueryString["docnumber"].ToString(); //sets docnumber from session

                    _Entity.getdata(txtPallet.Text, Session["ConnString"].ToString());//ADD CONN //Method for retrieving data from entity

                    txtPallet.Text = _Entity.PalletID;
                    CustomerCode.Value = _Entity.AreaCode;
                    txtPackaging.Value = _Entity.Packaging;
                    txtCaseTier.Text = _Entity.CaseTier.ToString();
                    txtTierPallet.Text = _Entity.TierPallet.ToString();
                    txtWidth.Text = _Entity.Width.ToString();
                    txtLength.Text = _Entity.Length.ToString();
                    txtHeight.Text = _Entity.Height.ToString();
                    txtUnitWeight.Text = _Entity.UnitWeight.ToString();
                    txtPalletType.Text = _Entity.PalletType.ToString();
                    //txtFirstName.Text = _Entity.FirstName.ToString();
                    //txtLastName.Text = _Entity.LastName.ToString();
                    //txtMidName.Text = _Entity.MidName.ToString();
                    //txtNickName.Text = _Entity.NickName.ToString();
                    //txtAge.Text = _Entity.Age.ToString();
                    //txtBirthday.Value = _Entity.BirthDay;

                    DataTable checkCount = Gears.RetriveData2("Select PalletID from wms.PalletDetail where PalletID = '" + txtPallet.Text + "'", Connection);
                    
                    if (checkCount.Rows.Count > 0)
                    {

                        gv1.DataSourceID = "odsDetail";
                    }
                    else
                    {
                        gv1.DataSourceID = "sdsDetail";
                    }
                }

            }   


        }
        #endregion

        #region Validation
        private void Validate()
        {
            //GearsLibrary.Gears.GearsParameter gparam = new Gears.GearsParameter();
            //gparam._DocNo = _Entity.DocNumber;
            //gparam._UserId = Session["Userid"].ToString();
            //gparam._TransType = "REFLOC";

            //string strresult =GWarehouseManagement.Pallet_Validate(gparam);


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
            //Control look = (Control)sender;
            //((ASPxGridLookup)look).ReadOnly = view;
            var look = sender as ASPxGridLookup;
            if (look != null)
            {
                look.ReadOnly = view;
            }
        }
        protected void gvLookupLoad(object sender, EventArgs e)//Control for all lookup in details/grid
        {
            if (!IsPostBack && IsCallback)
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
            //CustomerCode.ReadOnly = view;
        }
        protected void gv_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
        {   //Sets icon for grid/Control for Enabling/Disabling of buttons on grid.
            if (!IsPostBack && !IsCallback)
            {
                if (Request.QueryString["entry"].ToString() == "V")
                {
                    if (e.ButtonType == ColumnCommandButtonType.Edit || e.ButtonType == ColumnCommandButtonType.Delete ||
                        e.ButtonType == ColumnCommandButtonType.New || e.ButtonType == ColumnCommandButtonType.Cancel ||
                        e.ButtonType == ColumnCommandButtonType.Update)
                        e.Visible = false;
                }
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
        protected void lookup_Init(object sender, EventArgs e)
        {
            ASPxGridLookup gridLookup = sender as ASPxGridLookup;
            gridLookup.GridView.CustomCallback += new ASPxGridViewCustomCallbackEventHandler(gridView_CustomCallback);
            if (Session["FilterExpression"] != null)
            {
                gridLookup.GridView.DataSourceID = "Masterfileitemdetail";
                Masterfileitemdetail.FilterExpression = Session["FilterExpression"].ToString();

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
            string column = e.Parameters.Split('|')[0];
            if (column.Contains("GLP_AIC") || column.Contains("GLP_AC") || column.Contains("GLP_F")) return;
            string itemcode = e.Parameters.Split('|')[1];
            string customerCode = e.Parameters.Split('|')[1];
            string val = e.Parameters.Split('|')[2];
            string val2 = "";
            try
            {
                val2 = e.Parameters.Split('|')[3];
            }
            catch (Exception)
            {
                val2 = "";
            }
            if (val.Contains("GLP_AIC") || val.Contains("GLP_AC") || val.Contains("GLP_F")) return;
            var itemlookup = sender as ASPxGridView;
            string qty = "";
            decimal totalqty = 0;
            string codes = "";

            if (val2 == "")
            {
                val2 = "0";
            }
            if (e.Parameters.Contains("ItemCode"))
            {
                DataTable getall = Gears.RetriveData2("Select top 1 ColorCode,ClassCode,SizeCode,FullDesc,isnull(StandardQty, 0) as StandardQty,UnitBulk from masterfile.item a" +
                    "left join masterfile.itemdetail b on a.itemcode = b.itemcode where a.itemcode = '" + itemcode + "'" +
                    "and isnull (a.IsInactive, 0)=0", Session["ConnString"].ToString());
                
                foreach (DataRow dt in getall.Rows)
                {
                    codes = dt["ColorCode"].ToString() + ";";
                    codes += dt["ClassCode"].ToString() + ";";
                    codes += dt["SizeCode"].ToString() + ";";
                    codes += dt["FullDesc"].ToString() + ";";
                    codes += dt["UnitBulk"].ToString() + ";";
                    qty = dt["StandardQty"].ToString();
                    qty = string.IsNullOrEmpty(qty) ? "0" : qty;
                }
                if (Convert.ToDecimal(qty) > 0)
                {
                    totalqty = (Convert.ToDecimal(qty) * Convert.ToDecimal(val2));
                    codes += totalqty + ";";
                }
                itemlookup.JSProperties["cp_codes"] = codes;
            }
            else if (e.Parameters.Contains("BulkQty"))
            {
                DataTable getqty = Gears.RetriveData2("Select isnull(StandardQty, 0) as StandardQty from masterfile.item where itemcode = '" + itemcode + "'",
                    Session["ConnString"].ToString());

                if (getqty.Rows.Count == 1)
                {
                    foreach (DataRow dt in getqty.Rows)
                    {
                        qty = dt["StandardQty"].ToString();
                    }
                }
                qty = String.IsNullOrEmpty(qty) ? "0" : qty;
                if (Convert.ToDecimal(qty) > 0)
                {
                    totalqty = (Convert.ToDecimal(qty) * Convert.ToDecimal(val));
                }
                itemlookup.JSProperties["cp_codes"] = totalqty + ";";
            }
            else if (e.Parameters.Contains("Customer"))
            {
                DataTable getCustomer = Gears.RetriveData2("Select bizpartnercode from masterfile.bizpartner where bizpartnercode = '" + customerCode + "'",
                    Session["ConnString"].ToString());
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
                CriteriaOperator selectionCriteria = new InOperator(lookup.KeyFieldName, new String[] { itemcode });
                Masterfileitemdetail.FilterExpression = (GroupOperator.Combine(GroupOperatorType.And, selectionCriteria)).ToString();
                Session["FilterExpression"] = "Masterfileitemdetail.FilterExpression";
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

        #region Callback functions(Inserting/Updating/Deleting/Validating)
        protected void cp_Callback(object sender, CallbackEventArgsBase e)
        {
            _Entity.Connection = Session["ConnString"].ToString(); //ADD CONN
            _Entity.PalletID = txtPallet.Text;
            _Entity.AreaCode = String.IsNullOrEmpty(CustomerCode.Text) ? null : CustomerCode.Value.ToString();
            _Entity.Packaging = String.IsNullOrEmpty(txtPackaging.Text) ? null : txtPackaging.Value.ToString();
            _Entity.CaseTier = String.IsNullOrEmpty(txtCaseTier.Text) ? 0 : Convert.ToDecimal(txtCaseTier.Text);
            _Entity.TierPallet = String.IsNullOrEmpty(txtTierPallet.Text) ? 0 : Convert.ToDecimal(txtTierPallet.Text);
            _Entity.Width = String.IsNullOrEmpty(txtWidth.Text) ? 0 : Convert.ToDecimal(txtWidth.Text);
            _Entity.Length = String.IsNullOrEmpty(txtLength.Text) ? 0 : Convert.ToDecimal(txtLength.Text);
            _Entity.Height = String.IsNullOrEmpty(txtHeight.Text) ? 0 : Convert.ToDecimal(txtHeight.Text);
            _Entity.UnitWeight = String.IsNullOrEmpty(txtUnitWeight.Text) ? 0 : Convert.ToDecimal(txtUnitWeight.Text);
            _Entity.PalletType = String.IsNullOrEmpty(txtPalletType.Text) ? null : txtPalletType.Value.ToString();
            _Entity.LastEditedBy = Session["userid"].ToString();
            _Entity.LastEditedDate = DateTime.Now.ToString();
            _Entity.AddedBy = Session["userid"].ToString();
            _Entity.AddedDate = DateTime.Now.ToString();
            //_Entity.FirstName = String.IsNullOrEmpty(txtFirstName.Text) ? null : txtFirstName.Value.ToString();
            //_Entity.LastName = String.IsNullOrEmpty(txtLastName.Text) ? null : txtLastName.Value.ToString();
            //_Entity.MidName = String.IsNullOrEmpty(txtMidName.Text) ? null : txtMidName.Value.ToString();
            //_Entity.NickName = String.IsNullOrEmpty(txtNickName.Text) ? null : txtNickName.Value.ToString();
            //_Entity.Age = String.IsNullOrEmpty(txtAge.Text) ? 0 : Convert.ToDecimal(txtAge.Text);
            //_Entity.BirthDay = String.IsNullOrEmpty(txtBirthday.Text) ? null : txtBirthday.Value.ToString();
            //_Entity.ActivatedBy = txtActivatedBy.Text;
            //_Entity.ActivatedDate = txtActivatedDate.Text;
            //_Entity.DeActivatedBy = txtDeActivatedBy.Text;
            //_Entity.DeActivatedDate = txtDeActivatedDate.Text;

            switch (e.Parameter)
            {
                case "Add":

                    if (error == false)
                    {
                        check = true;
                        //_Entity.InsertData(_Entity);//Method of inserting for header
                        _Entity.InsertData(_Entity);
                        Validate();
                        cp.JSProperties["cp_message"] = "Successfully Added!";//Message variable to client side
                        cp.JSProperties["cp_success"] = true;//Success bool variable  to client side

                        SqlDataSource ss = Customer;
                        ss.SelectCommand = string.Format("DECLARE @PalletID VARCHAR(20), " +
                                                         "@SeriesNumber INT " +
                                                         "UPDATE PORTAL.IT.PalletInfo SET SeriesNumber = SeriesNumber + 1,PalletId= ( SELECT Prefix + LEFT(Storagetype, 1) + CASE WHEN Prefix = 'PI' OR Prefix = 'LI' THEN RIGHT(REPLICATE('0', 12) + SeriesNumber, 12) ELSE RIGHT(REPLICATE('0', 6) + SeriesNumber, 6)" +
                                                         " END FROM PORTAL.IT.PalletInfo WHERE CustomerCode = '" + CustomerCode.Text + "' ) where CustomerCode = '" + CustomerCode.Text + "'; " +
                                                         "SELECT @PalletID = PalletId, @SeriesNumber = SeriesNumber - 1 FROM PORTAL.IT.PalletInfo Where CustomerCode = '" + CustomerCode.Text + "' " +
                                                         "INSERT INTO PORTAL.IT.PalletInfoDetails(PalletId, WarehouseCode, CustomerCode, SeriesNumber, DocNumber) " +
                                                         "SELECT PalletID, 'MLICAV',AreaCode, @SeriesNumber, '14'  FROM Masterfile.Pallet WHERE PalletID = @PalletID");
                        DataView trans = (DataView)ss.Select(DataSourceSelectArguments.Empty);
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
                        Validate();
                        gv1.DataSourceID = "odsDetail";
                        odsDetail.SelectParameters["PalletID"].DefaultValue = txtPallet.Text;
                        gv1.UpdateEdit();
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
                    Session["Refresh"] = "1";
                    break;
                case "Close":
                    cp.JSProperties["cp_close"] = true;
                    Session["Refresh"] = "1";
                    break;
                case "SUP":
                    SqlDataSource ds = Customer;
                    ds.SelectCommand = string.Format("DECLARE @NewPalletId varchar(20)" +
                                                     "SELECT @NewPalletId = Prefix +LEFT(Storagetype, 1)+ CASE WHEN Prefix = 'PI' OR Prefix = 'LI' THEN RIGHT(REPLICATE('0',12)+SeriesNumber,12) ELSE RIGHT(REPLICATE('0',6)+SeriesNumber,6) END " +
                                                     "FROM PORTAL.IT.PalletInfo Where CustomerCode = '" + CustomerCode.Text + "' SELECT @NewPalletId");
                    DataView tran = (DataView)ds.Select(DataSourceSelectArguments.Empty);
                    if (tran.Count > 0)
                    {
                        txtPallet.Value = tran[0][0].ToString();

                    }
                    break;
            }
        }
        protected void grid_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        {
            foreach (GridViewColumn column in gv1.Columns)
            {
                GridViewDataColumn dataColumn = column as GridViewDataColumn;
                if (dataColumn == null) continue;
                if ((e.NewValues[dataColumn.FieldName] == null || e.NewValues[dataColumn.FieldName].ToString() == "") && dataColumn.FieldName != "LineNumber" && dataColumn.FieldName !=
                    "PalletID" && dataColumn.FieldName != "Batch" && dataColumn.FieldName != "lot" 
                    && dataColumn.FieldName != "ExpiryDate" && dataColumn.FieldName != "PlateNumber" && dataColumn.FieldName != "UOM" && dataColumn.FieldName != "checker" && 
                    dataColumn.FieldName != "weight" && dataColumn.FieldName != "Item" && dataColumn.FieldName != "Customer")
                {
                    e.Errors[dataColumn] = "Value can't be null"; 
                }
            }

            if (e.Errors.Count > 0)
            {
                error = true;
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
        }

        protected void Connection_Init(object sender, EventArgs e)
        {
            //sdsDetail.ConnectionString = Session["ConnString"].ToString();
            //sdsItem.ConnectionString = Session["ConnString"].ToString();
            //sdsItemDetail.ConnectionString = Session["ConnString"].ToString();
            //Masterfileitem.ConnectionString = Session["ConnString"].ToString();
            //Warehouse.ConnectionString = Session["ConnString"].ToString();
            //PlantCode.ConnectionString = Session["ConnString"].ToString();
            //Masterfilebizcustomer.ConnectionString = Session["ConnString"].ToString();
            //RoomCode.ConnectionString = Session["ConnString"].ToString();
            //PalletType.ConnectionString = Session["ConnString"].ToString();
            //Storage.ConnectionString = Session["ConnString"].ToString();
            //PalletID.ConnectionString = Session["ConnString"].ToString();
        }
        protected void glItemCode_Init(object sender, EventArgs e)
        {
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
        public void ItemCodeLookup_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
            {
                if (e.Parameters.Contains("GLP_AIC") || e.Parameters.Contains("GLP_AC") || e.Parameters.Contains("GLP_F")) return;
                ASPxGridView ItemcodeList = sender as ASPxGridView;
                string CustomerCode = e.Parameters.Split('|')[1];

                if (e.Parameters.Contains("ItemCodeDropDown"))
                {
                    DataTable getCustomer = Gears.RetriveData2("SELECT ISNULL(Field1,'') as Field1 FROM Masterfile.BizPartner WHERE ISNULL(IsInactive,0)=0 AND BizPartnerCode = '" +
     CustomerCode + "' AND ISNULL(Field1,'')!='' ", Session["ConnString"].ToString());

                    if (getCustomer.Rows.Count > 0)
                    {
                        CustomerCode = getCustomer.Rows[0][0].ToString();
                    }

                    Masterfileitem.SelectCommand = "SELECT [ItemCode], [FullDesc], [ShortDesc], Customer FROM Masterfile.[Item] where isnull(IsInactive, '')=0 AND Customer IN ('" + CustomerCode + "')";
                    Session["itemsql"] = Masterfileitem.SelectCommand;
                    //ItemcodeList.DataSource = Masterfileitem;
                    ItemcodeList.DataBind(); 
                }
            }

        #endregion
    }
}