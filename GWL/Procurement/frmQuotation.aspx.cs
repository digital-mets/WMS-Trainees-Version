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
    public partial class frmQuotation : System.Web.UI.Page
    {
        Boolean error = false;//Boolean for grid validation
        Boolean view = false;//Boolean for view state
        Boolean check = false;//Boolean for grid validation

        private static string Connection;

        Entity.Quotation _Entity = new Quotation();//Calls entity Quotation
        Entity.Quotation.QuotationDetail _EntityDetail = new Quotation.QuotationDetail();//Call entity Quotationdetails

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

            gv1.KeyFieldName = "DocNumber;LineNumber";

             
            if (Request.QueryString["entry"].ToString() == "V" || Request.QueryString["entry"].ToString() == "D") {
                view = true; 
            }

            dtpDocDate.MinDate = Convert.ToDateTime(GearsCommon.GCommon.SystemSetting("BookDate", Session["ConnString"].ToString()).ToString());

            if (!IsPostBack)
            {
                Connection = Session["ConnString"].ToString();
                Session["PQUMFilterExpression"] = null;
                WordStatus();
                
                txtDocnumber.ReadOnly = true;
                txtDocnumber.Value = Request.QueryString["docnumber"].ToString();
                dtpDocDate.Text = DateTime.Now.ToShortDateString();
                dtpValidFrom.Text = DateTime.Now.ToShortDateString();
                dtpValidTo.Text = DateTime.Now.ToShortDateString();

                _Entity.getdata(txtDocnumber.Text, Session["ConnString"].ToString());//ADD CONN
                dtpDocDate.Value = String.IsNullOrEmpty(_Entity.DocDate) ? DateTime.Now : Convert.ToDateTime(_Entity.DocDate);
                dtpSupplierTargetDate.Value = String.IsNullOrEmpty(_Entity.SupplierTargetDate) ? DateTime.Now : Convert.ToDateTime(_Entity.SupplierTargetDate);
                seLeadTime.Value = Convert.ToString(_Entity.LeadTime);
                seTerms.Text = Convert.ToString(_Entity.Terms);
                memFileNameOfQuotation.Text = _Entity.FileNameOfQuotation;
                txtContactPerson.Value = _Entity.ContactPerson;
                glSupplierCode.Value = _Entity.Supplier;
                txtName.Text = _Entity.Name; 
                dtpValidFrom.Value = String.IsNullOrEmpty(_Entity.ValidFrom) ? DateTime.Now : Convert.ToDateTime(_Entity.ValidFrom);
                dtpValidTo.Value = String.IsNullOrEmpty(_Entity.ValidTo) ? DateTime.Now : Convert.ToDateTime(_Entity.ValidTo);

                txtHAddedBy.Value = _Entity.AddedBy;
                txtHAddedDate.Value = _Entity.AddedDate;
                txtHLastEditedBy.Value = _Entity.LastEditedBy;
                txtHLastEditedDate.Value = _Entity.LastEditedDate;
                txtHSubmittedBy.Value = _Entity.SubmittedBy;
                txtHSubmittedDate.Value = _Entity.SubmittedDate;
                txtHUnApprovedBy.Value = _Entity.UnApprovedBy;
                txtHUnApprovedDate.Value = _Entity.UnApprovedDate;
                txtHCancelledBy.Value = _Entity.CancelledBy;
                txtHCancelledDate.Value = _Entity.CancelledDate;

                txtHField1.Value = _Entity.Field1;
                txtHField2.Value = _Entity.Field2;
                txtHField3.Value = _Entity.Field3;
                txtHField4.Value = _Entity.Field4;
                txtHField5.Value = _Entity.Field5;
                txtHField6.Value = _Entity.Field6;
                txtHField7.Value = _Entity.Field7;
                txtHField8.Value = _Entity.Field8;
                txtHField9.Value = _Entity.Field9;


                frmlayout1.FindItemOrGroupByName("ReferenceTransaction").ClientVisible = true;
                updateBtn.Text = "Add";
                if (!string.IsNullOrEmpty(_Entity.LastEditedBy)) {
                    updateBtn.Text = "Update";
                }
                switch (Request.QueryString["entry"].ToString())
                {
                    case "N":
                        frmlayout1.FindItemOrGroupByName("ReferenceTransaction").ClientVisible = false;
                        break;
                    case "E":
                        updateBtn.Text = "Update";
                        break;
                    case "V":
                        glcheck.ClientVisible = false;
                        updateBtn.Text = "Close";
                        break;
                    case "D":
                        glcheck.ClientVisible = false;
                        updateBtn.Text = "Delete";
                        break;
                }

                gvRef.DataSourceID = "odsReference";
                this.gvRef.Columns["CommandString"].Width = 0;
                this.gvRef.Columns["RCommandString"].Width = 0;

                DataTable dtbldetail = Gears.RetriveData2("SELECT DISTINCT DocNumber FROM Procurement.QuotationDetail WHERE DocNumber ='" + txtDocnumber.Text + "'", Session["ConnString"].ToString());//ADD CONN
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
            gparam._TransType = "PRCQUM"; 

            string strresult = GearsProcurement.GProcurement.Quotation_Validate(gparam);
            cp.JSProperties["cp_valmsg"] = strresult; 
        }
        #endregion

        #region Set controls' state/behavior/etc...
        protected void TextboxLoad(object sender, EventArgs e)//Control for all textbox
        {
            if (Request.QueryString["entry"].ToString() == "V" || Request.QueryString["entry"].ToString() == "D")
            {
                ASPxTextBox text = sender as ASPxTextBox;
                text.ReadOnly = view;
            }
        }
        protected void LookupLoad(object sender, EventArgs e)//Control for all lookup in header
        {
            if (Request.QueryString["entry"].ToString() == "V" || Request.QueryString["entry"].ToString() == "D")
            {
                var look = sender as ASPxGridLookup;
                if (look != null)
                {
                    look.ReadOnly = true;
                    look.DropDownButton.Enabled = false;
                }
            }
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
                date.ReadOnly = true;
                date.DropDownButton.Enabled = false;
            }
        }
        protected void SpinEdit_Load(object sender, EventArgs e)//Control for all numeric entries in header
        {
            if (Request.QueryString["entry"].ToString() == "V" || Request.QueryString["entry"].ToString() == "D")
            {
                ASPxSpinEdit spinedit = sender as ASPxSpinEdit;
                spinedit.ReadOnly = true;
            }
        }
        protected void gv_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
        {  
            if (!IsPostBack)
            {
                if (Request.QueryString["entry"].ToString() == "V" || Request.QueryString["entry"].ToString() == "D")
                {
                    if (e.ButtonType == ColumnCommandButtonType.Edit || e.ButtonType == ColumnCommandButtonType.Delete ||
                        e.ButtonType == ColumnCommandButtonType.New || e.ButtonType == ColumnCommandButtonType.Cancel ||
                        e.ButtonType == ColumnCommandButtonType.Update)
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
            if (Session["PQUMFilterExpression"] != null)
            {
                gridLookup.GridView.DataSourceID = "Masterfileitemdetail";
                Masterfileitemdetail.FilterExpression = Session["PQUMFilterExpression"].ToString();
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
                //COLOR CODE LOOKUP
                DataTable getColor = Gears.RetriveData2("Select DISTINCT ColorCode FROM masterfile.item a " +
                          "left join masterfile.itemdetail b on a.itemcode = b.itemcode where a.itemcode = '" + itemcode + "'", Session["ConnString"].ToString());//ADD CONN
                if (getColor.Rows.Count > 1){
                    codes = "" + ";";
                }
                else{
                    foreach (DataRow dt in getColor.Rows){
                        codes = dt["ColorCode"].ToString() + ";";
                    }
                }

                //CLASS CODE LOOKUP
                DataTable getClass = Gears.RetriveData2("Select DISTINCT ClassCode FROM masterfile.item a " +
                          "left join masterfile.itemdetail b on a.itemcode = b.itemcode where a.itemcode = '" + itemcode + "'", Session["ConnString"].ToString());//ADD CONN
                if (getClass.Rows.Count > 1){
                    codes += "" + ";";
                }
                else{
                    foreach (DataRow dt in getClass.Rows){
                        codes += dt["ClassCode"].ToString() + ";";
                    }
                }

                //SIZE CODE LOOKUP
                DataTable getSize = Gears.RetriveData2("Select DISTINCT SizeCode FROM masterfile.item a " +
                          "left join masterfile.itemdetail b on a.itemcode = b.itemcode where a.itemcode = '" + itemcode + "'", Session["ConnString"].ToString());//ADD CONN
                if (getSize.Rows.Count > 1){
                    codes += "" + ";";
                }
                else{
                    foreach (DataRow dt in getSize.Rows){
                        codes += dt["SizeCode"].ToString() + ";";
                    }
                }

                //UNIT BASE LOOKUP
                DataTable getUnit = Gears.RetriveData2("Select DISTINCT FullDesc AS ItemDescription FROM masterfile.item a where a.itemcode = '" + itemcode + "'", Session["ConnString"].ToString());//ADD CONN
                if (getUnit.Rows.Count > 1){
                    codes += "" + ";";
                }
                else{
                    foreach (DataRow dt in getUnit.Rows){
                        codes += dt["ItemDescription"].ToString() + ";";
                    }
                }

                DataTable getIsVAT = Gears.RetriveData2("SELECT DISTINCT CASE ISNULL(B.TCode,'NONV') WHEN 'NONV' THEN 0 ELSE 1 END AS VATable from Masterfile.BPSupplierInfo A "
                                                      + "INNER JOIN Masterfile.Tax B ON A.TaxCode = B.TCode WHERE A.SupplierCode = '" + glSupplierCode.Text + "'", Session["ConnString"].ToString());//ADD CONN
                if (getIsVAT.Rows.Count > 1 || getIsVAT.Rows.Count == 0)
                {
                    codes += "0" + ";";
                }
                else
                {
                    foreach (DataRow dt in getIsVAT.Rows)
                    {
                        codes += dt["VATable"].ToString() + ";";
                    }
                }
                itemlookup.JSProperties["cp_codes"] = codes;
            }
            else
            {
                if (e.Parameters.Contains("ColorCode"))
                {
                    Masterfileitemdetail.SelectCommand = "SELECT DISTINCT ItemCode, [ColorCode], '' AS [ClassCode], '' AS [SizeCode] FROM Masterfile.ItemDetail WHERE ItemCode = '" + itemcode +  "'";
                }
                else if (e.Parameters.Contains("ClassCode"))
                {
                    Masterfileitemdetail.SelectCommand = "SELECT DISTINCT ItemCode, '' AS [ColorCode], [ClassCode], '' AS [SizeCode] FROM Masterfile.ItemDetail WHERE ItemCode = '" + itemcode + "'";
                }
                else if (e.Parameters.Contains("SizeCode"))
                {
                    Masterfileitemdetail.SelectCommand = "SELECT DISTINCT ItemCode, '' AS [ColorCode], '' AS [ClassCode], [SizeCode] FROM Masterfile.ItemDetail WHERE ItemCode = '" + itemcode + "'";
                }
                Masterfileitemdetail.DataBind();

                ASPxGridView grid = sender as ASPxGridView;
                //ASPxGridLookup lookup = (ASPxGridLookup)gv1.FindEditRowCellTemplateControl((GridViewDataColumn)gv1.Columns[2], "glItemCode");
                //var selectedValues = itemcode;
                //CriteriaOperator selectionCriteria = new InOperator(lookup.KeyFieldName, new string[] { itemcode });
                //Masterfileitemdetail.PQUMFilterExpression = (GroupOperator.Combine(GroupOperatorType.And, selectionCriteria)).ToString();
                Session["PQUMFilterExpression"] = Masterfileitemdetail.FilterExpression;
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
            _Entity.DocNumber = txtDocnumber.Text;
            _Entity.DocDate = dtpDocDate.Text;
            _Entity.SupplierTargetDate = dtpSupplierTargetDate.Text;
            _Entity.LeadTime = String.IsNullOrEmpty(seLeadTime.Text) ? 0 : Convert.ToInt32(seLeadTime.Text);
            _Entity.Terms = String.IsNullOrEmpty(seTerms.Text) ? 0 : Convert.ToInt32(seTerms.Text);
            _Entity.FileNameOfQuotation = memFileNameOfQuotation.Text;
            _Entity.ContactPerson = txtContactPerson.Text;
            _Entity.Supplier = glSupplierCode.Text;
            _Entity.Name = txtName.Text;
            _Entity.ValidFrom = dtpValidFrom.Text;
            _Entity.ValidTo = dtpValidTo.Text;

            _Entity.LastEditedBy = Session["userid"].ToString();
            _Entity.LastEditedDate = DateTime.Now.ToString();

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

                    gv1.UpdateEdit();//Initiate to call gridvalidation/batchupdate method

                    string strError = Functions.Submitted(_Entity.DocNumber, "Procurement.Quotation", 1, Connection);//NEWADD factor 1 if submit, 2 if approve
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
                        _Entity.InsertData(_Entity);//Method of inserting for header
                        _Entity.AddedBy = Session["userid"].ToString();
                        _Entity.UpdateData(_Entity);


                        gv1.DataSourceID = "odsDetail";
                        odsDetail.SelectParameters["DocNumber"].DefaultValue = txtDocnumber.Text;
                        gv1.UpdateEdit();//2nd initiation to insert grid
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
                    gv1.UpdateEdit();
                    string strError1 = Functions.Submitted(_Entity.DocNumber,"Procurement.Quotation",1,Connection);//NEWADD factor 1 if submit, 2 if approve
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

                        _Entity.LastEditedDate = DateTime.Now.ToString();
                        _Entity.UpdateData(_Entity); 

                        gv1.DataSourceID = "odsDetail";
                        odsDetail.SelectParameters["DocNumber"].DefaultValue = txtDocnumber.Text; 
                        gv1.UpdateEdit(); 
                        Validate();
                        cp.JSProperties["cp_message"] = "Successfully Updated!";
                        cp.JSProperties["cp_success"] = true;
                        cp.JSProperties["cp_close"] = true;
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
                    break;

                case "SupplierCodeCase":
                    SqlDataSource ds = SupplierCodelookup;

                    ds.SelectCommand = string.Format("SELECT ContactPerson,SupplierCode,ISNULL(APTerms,0), Name FROM Masterfile.BPSupplierInfo where SupplierCode = '" + glSupplierCode.Text + "'");
                    DataView inb = (DataView)ds.Select(DataSourceSelectArguments.Empty);
                    if (inb.Count > 0)
                    {
                        txtContactPerson.Value = inb[0][0].ToString();
                        seTerms.Text = inb[0][2].ToString();
                        txtName.Text = inb[0][3].ToString();
                    }
                    break;
            }
        }

        protected void gv1_BatchUpdate(object sender, ASPxDataBatchUpdateEventArgs e)
        {
            if ((error == true || error == false) && check == false)
            {
                e.Handled = true;
                e.InsertValues.Clear();
                e.UpdateValues.Clear();
            }
        }
        #endregion

        protected void WordStatus()
        {
            DataTable mod = new DataTable();
            mod = Gears.RetriveData2("SELECT TOP 1 Status FROM Procurement.Quotation WHERE DocNumber = '" + Request.QueryString["docnumber"].ToString() + "'", Session["ConnString"].ToString());//ADD CONN

            foreach (DataRow md in mod.Rows)
            {
                switch (md["Status"].ToString())
                {
                    case "N":
                        txtStatus.Text = "NEW";
                        break;
                    case "A":
                        txtStatus.Text = "APPROVED";
                        break;
                    case "C":
                        txtStatus.Text = "CANCELLED";
                        break;
                    case "U":
                        txtStatus.Text = "UNAPPROVED";
                        break;
                }
            }
        }

        protected void ConnectionInit_Init(object sender, EventArgs e)
        {
            ((SqlDataSource)sender).ConnectionString = Session["ConnString"].ToString();
        }
    }
}