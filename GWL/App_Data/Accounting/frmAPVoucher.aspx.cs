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


namespace GWL
{
    public partial class frmAPVoucher : System.Web.UI.Page
    {
        Boolean error = false;//Boolean for grid validation
        Boolean view = false;//Boolean for view state
        Boolean check = false;//Boolean for grid validation
        Boolean checkdel = false;
        Entity.APVoucher _Entity = new APVoucher();//Calls entity PO
        Entity.APVoucher.APVoucherDetail _EntityDetail = new APVoucher.APVoucherDetail();//Call entity POdetails

        private static string Connection;

        #region page load/entry
        protected void Page_Load(object sender, EventArgs e)
        {
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

            if (!IsPostBack)
            {
                Session["Datatable"] = null;
                Session["FilterExpression"] = null;
                Session["icndetail"] = null;
                Session["SI"] = null;
                Session["SI2"] = null;
                Connection = Session["ConnString"].ToString();
                switch (Request.QueryString["entry"].ToString())
                {
                    case "N":
                        view = false;//sets view mode for entry
                        updateBtn.Text = "Add";
                        txtDocNumber.ReadOnly = true;
                        break;
                    case "E":
                        view = false;//sets view mode for entry
                        updateBtn.Text = "Update";
                        txtDocNumber.ReadOnly = true;
                        break;
                    case "V":
                        view = true;//sets view mode for entry
                        updateBtn.Text = "Close";
                        glInvoice.DataSourceID = "refnum2";
                        glInvoice.KeyFieldName = "DocNumber;TransType";
                        glInvoice.DataBind();
                        break;
                    case "D":
                        view = true;//sets view mode for entry
                        updateBtn.Text = "Delete";
                        glInvoice.DataSourceID = "refnum2";
                        glInvoice.KeyFieldName = "DocNumber;TransType";
                        glInvoice.DataBind();
                        break;
                }

                    txtDocNumber.Value = Request.QueryString["docnumber"].ToString(); //sets docnumber from session
                
                    _Entity.getdata(txtDocNumber.Text,Connection); //Method for retrieving data from entity
                    DocDate.Text = String.IsNullOrEmpty(_Entity.DocDate) ? DateTime.Now.ToShortDateString() : Convert.ToDateTime(_Entity.DocDate).ToShortDateString();
                    gvSupplier.Value = _Entity.SupplierCode;
                    SupplierName.Text = _Entity.SupplierName;
                    //BrokerCode.Value = _Entity.BrokerCode;
                    //BrokerName.Text = _Entity.BrokerName;
                    glInvoice.Text = _Entity.ReferenceNumber;
                    txtInvoice.Text = _Entity.InvoiceNumber;
                    InvoiceDate.Text = String.IsNullOrEmpty(_Entity.ReferenceDate) ? DateTime.Now.ToShortDateString() : Convert.ToDateTime(_Entity.ReferenceDate).ToShortDateString();
                    DueDate.Text = String.IsNullOrEmpty(_Entity.DueDate) ? DateTime.Now.ToShortDateString() : Convert.ToDateTime(_Entity.DueDate).ToShortDateString();
                    TotalAP.Text = _Entity.TotalAPAmount.ToString();
                    TotalVAT.Text = _Entity.TotalVATAmount.ToString();
                    TotalEWT.Text = _Entity.TotalEWTAmount.ToString();
                    Remarks.Text = _Entity.Remarks;
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
                    txtPostedBy.Text = _Entity.PostedBy;
                    txtPostedDate.Text = _Entity.PostedDate;
                    txtCancelledBy.Text = _Entity.CancelledBy;
                    txtCancelledDate.Text = _Entity.CancelledDate;
                    txtPostedBy.Text = _Entity.PostedBy;
                    txtPostedDate.Text = _Entity.PostedDate;

                //string val = "";
                //DataTable dt = Gears.RetriveData2("select TransDocNumber from accounting.apvoucherdetail where docnumber = '" + txtDocNumber.Text + "'", Connection);
                //foreach (DataRow dtrow in dt.Rows)
                //{
                //    val += dtrow[0].ToString() + ";";
                //}
                //glInvoice.Text = val;
                //glInvoice.Value = _Entity.ReferenceNumber;


                DataTable checkCount = Gears.RetriveData2("Select DocNumber from accounting.apvoucherdetail where docnumber = '" + txtDocNumber.Text + "'", Connection);
                if (checkCount.Rows.Count > 0)
                {
                    gv1.KeyFieldName = "TransDocNumber";
                    //gv1.DataSourceID = GetSelectedVal();
                    gv1.DataSourceID = "odsDetail";
                }
                else if (checkCount.Rows.Count == 0)
                {
                    gv1.KeyFieldName = "TransDocNumber";
                    gv1.DataSourceID = "sdsDetail";
                }

                gvJournal.DataSourceID = "odsJournalEntry";

                if (!string.IsNullOrEmpty(_Entity.LastEditedBy) && (Request.QueryString["entry"].ToString() == "E" || Request.QueryString["entry"].ToString() == "N"))
                {
                    updateBtn.Text = "Update";
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
            gparam._TransType = "ACTAPV";
            gparam._Connection = Connection;
            string strresult = GearsAccounting.GAccounting.APVoucher_Validate(gparam);
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
            gparam._TransType = "ACTAPV";
            gparam._Table = "Accounting.APVoucher";
            gparam._Factor = -1;
            gparam._Connection = Connection;
            string strresult = GearsAccounting.GAccounting.APVoucher_Post(gparam);
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
            look.ReadOnly = view;
            look.DropDownButton.Visible = !view;
           
        }
        protected void gvLookupLoad(object sender, EventArgs e)//Control for all lookup in details/grid
        {
            ASPxGridLookup look = sender as ASPxGridLookup;
            look.ReadOnly = view;
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
            date.ReadOnly = view;
        }
        protected void SpinEdit_Load(object sender, EventArgs e)//Control for all numeric entries in header
        {
            ASPxSpinEdit spinedit = sender as ASPxSpinEdit;
            spinedit.ReadOnly = view;
        }
        protected void Memo_Load(object sender, EventArgs e)//Control for all numeric entries in header
        {
            ASPxMemo memo = sender as ASPxMemo;
            memo.ReadOnly = view;
        }
        protected void gv_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
        {   //Sets icon for grid/Control for Enabling/Disabling of buttons on grid.
            
            if (view == true)
            {
                if (e.ButtonType == ColumnCommandButtonType.Edit || e.ButtonType == ColumnCommandButtonType.Delete ||
                    e.ButtonType == ColumnCommandButtonType.New || e.ButtonType == ColumnCommandButtonType.Cancel)
                    e.Visible = false;
            }
            if (e.ButtonType == ColumnCommandButtonType.Update || e.ButtonType == ColumnCommandButtonType.New || e.ButtonType == ColumnCommandButtonType.Cancel)
                e.Visible = false;
        }

        protected void gv1_CustomButtonInitialize(object sender, ASPxGridViewCustomButtonEventArgs e)
        {
           if (Request.QueryString["entry"] == "V")
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
        }

        public void gridView_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            string column = e.Parameters.Split('|')[0];//Set column name
            if (column.Contains("GLP_AIC") || column.Contains("GLP_AC") || column.Contains("GLP_F")) return;//Traps the callback
            string code = e.Parameters.Split('|')[1];//Set Item Code
            var itemlookup = sender as ASPxGridView;
            string qty = "";

            if (e.Parameters.Contains("ATCCode"))
            {
                DataTable getqty = Gears.RetriveData2("Select Rate from masterfile.ATC where ATCcode = '" + code + "'",Connection);
                if (getqty.Rows.Count == 1)
                {
                    foreach (DataRow dt in getqty.Rows)
                    {
                        qty = dt["Rate"].ToString();
                    }
                }

                itemlookup.JSProperties["cp_identifier"] = "ATC";
                itemlookup.JSProperties["cp_codes"] = Convert.ToDecimal(qty) + ";";
            }
        }

        #endregion

        #region Callback functions(Inserting/Updating/Deleting/Validating)
        protected void cp_Callback(object sender, CallbackEventArgsBase e)
        {
            //int i;
            //bool result = int.TryParse(e.Parameter, out i);
            //if (result == true)
            //{
            //    rowcount = i;
            //}
            _Entity.Connection = Connection;
            _Entity.DocNumber = txtDocNumber.Text;
            _Entity.DocDate = DocDate.Text;
            _Entity.SupplierCode = gvSupplier.Text;
            _Entity.SupplierName = SupplierName.Text;
            _Entity.InvoiceNumber = txtInvoice.Text;
            //_Entity.BrokerCode = BrokerCode.Text;
            //_Entity.BrokerName = BrokerName.Text;
            _Entity.ReferenceNumber = glInvoice.Text;
            _Entity.ReferenceDate = InvoiceDate.Text;
            _Entity.DueDate = DueDate.Text;
            _Entity.TotalAPAmount = Convert.ToDecimal(string.IsNullOrEmpty(TotalAP.Text) ? "0" : TotalAP.Text);
            _Entity.TotalVATAmount = Convert.ToDecimal(string.IsNullOrEmpty(TotalVAT.Text) ? "0" : TotalVAT.Text);
            _Entity.TotalEWTAmount = Convert.ToDecimal(string.IsNullOrEmpty(TotalEWT.Text) ? "0" : TotalEWT.Text);
            _Entity.Remarks = Remarks.Text;
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
                    gv1.UpdateEdit();

                    if (error == false)
                    {
                        check = true;

                        _Entity.UpdateData(_Entity);

                        //if (Session["Datatable"] == "1")
                        //{
                            gv1.KeyFieldName = "DocNumber;LineNumber";
                            gv1.DataSourceID = "odsDetail";
                            gv1.UpdateEdit();
                        //}
                        //else
                        //{
                        //    gv1.KeyFieldName = "DocNumber;LineNumber";
                        //    gv1.DataSourceID = "odsDetail";//Renew datasourceID to entity
                        //    odsDetail.SelectParameters["DocNumber"].DefaultValue = txtDocNumber.Text;//Set select parameter to prevent error
                        //    gv1.UpdateEdit();//2nd initiation to insert grid
                        //}
                        Post();
                        Validate();
                        cp.JSProperties["cp_message"] = "Successfully Added!";
                        cp.JSProperties["cp_success"] = true;
                        cp.JSProperties["cp_close"] = true;
                    }
                    else
                    {
                        cp.JSProperties["cp_message"] = "Please check all the fields!";
                        cp.JSProperties["cp_success"] = true;
                    }
                break;

                case "Update":
                    
                    gv1.UpdateEdit();

                    if (error == false)
                    {
                        check = true;

                        _Entity.UpdateData(_Entity);

                        //if (Session["Datatable"] == "1")
                        //{
                            gv1.KeyFieldName = "DocNumber;LineNumber";
                            gv1.DataSourceID = "odsDetail";
                            gv1.UpdateEdit();
                        //}
                        //else
                        //{
                        //    gv1.KeyFieldName = "DocNumber;LineNumber";
                        //    gv1.DataSourceID = "odsDetail";//Renew datasourceID to entity
                        //    odsDetail.SelectParameters["DocNumber"].DefaultValue = txtDocNumber.Text;//Set select parameter to prevent error
                        //    gv1.UpdateEdit();//2nd initiation to insert grid
                        //}
                        Post();
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

                case "ZeroDetail":

                    gv1.UpdateEdit();

                    if (error == false)
                    {
                        check = true;

                        _Entity.UpdateData(_Entity);
                        _Entity.DeleteFirstData(txtDocNumber.Text.Trim(), Connection);

                        ////if (Session["Datatable"] == "1")
                        ////{
                        //    gv1.KeyFieldName = "DocNumber;LineNumber";
                        //    gv1.DataSourceID = "odsDetail";
                        //    gv1.UpdateEdit();
                        ////}
                        ////else
                        ////{
                        ////    gv1.KeyFieldName = "DocNumber;LineNumber";
                        ////    gv1.DataSourceID = "odsDetail";//Renew datasourceID to entity
                        ////    odsDetail.SelectParameters["DocNumber"].DefaultValue = txtDocNumber.Text;//Set select parameter to prevent error
                        ////    gv1.UpdateEdit();//2nd initiation to insert grid
                        ////}
                        Post();
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

                case "Supp":
                    checkdel = true;
                    gv1.DataSourceID = null;
                    gv1.UpdateEdit();
                    SqlDataSource ds = Masterfilebiz;

                    ds.SelectCommand = string.Format("SELECT SupplierCode, Name FROM Masterfile.[BPSupplierInfo] where SupplierCode = '" + gvSupplier.Text + "'");
                    DataView inb = (DataView)ds.Select(DataSourceSelectArguments.Empty);
                    if (inb.Count > 0)
                    {
                        SupplierName.Text = inb[0][1].ToString();
                    }
                    ds.SelectCommand = string.Format("SELECT SupplierCode, Name FROM Masterfile.[BPSupplierInfo]");

                    glInvoice.Value = "";
                    //string f_expression = GetFilterExpression();
                    //refnum.FilterExpression = f_expression;
                    //Session["FilterExpression"] = refnum.FilterExpression;
                    //glInvoice.DataBind();
                    cp.JSProperties["cp_suppjs"] = true;
                break;

                //case "Broker":
                //    SqlDataSource ds2 = Masterfilebiz;

                //    ds2.SelectCommand = string.Format("SELECT SupplierCode, Name FROM Masterfile.[BPSupplierInfo] where SupplierCode = '" + BrokerCode.Text + "'");
                //    DataView inb2 = (DataView)ds2.Select(DataSourceSelectArguments.Empty);
                //    if (inb2.Count > 0)
                //    {
                //        BrokerName.Text = inb2[0][1].ToString();
                //    }
                //    ds2.SelectCommand = string.Format("SELECT SupplierCode, Name FROM Masterfile.[BPSupplierInfo]");
                //break;

                case "Close":
                    cp.JSProperties["cp_close"] = true;
                break;

                case "SI":
                    gv1.KeyFieldName = "TransDocNumber";
                    //GetSelectedVal();
                break;
            }
        }
        protected void grid_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        { //Validation for gridview, set all validate conditions here.. (sample here is checking of empty cells)
            string ItemCode = "";
            string ColorCode = "";
            string ClassCode = "" ;
            string SizeCode = "";
            
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

            //if ((error == true || error == false) && checkdel == true)//Prevents updating of grid to enable validation
            //{
            //    e.Handled = true;
            //    DataTable source = GetSelectedVal();

            //    // Removing all deleted rows from the data source(Excel file)
            //    //foreach (ASPxDataDeleteValues values in e.DeleteValues)
            //    //{
            //    //    try
            //    //    {
            //    //        object[] keys = { values.Keys[0] };
            //    //        source.Rows.Remove(source.Rows.Find(keys));
            //    //    }
            //    //    catch (Exception)
            //    //    {
            //    //        continue;
            //    //    }
            //    //}

            //    //e.DeleteValues.Clear();
            //    e.InsertValues.Clear();
            //    e.UpdateValues.Clear();
            //    checkdel = false;
            //}
        }
        #endregion

        private DataTable GetSelectedVal()
        {
            string val = "";
            DataTable dt = new DataTable();

            string[] selectedValues = glInvoice.Text.Split(';');


            SqlDataSource ds = getsales;
            CriteriaOperator docnum = new BinaryOperator(new OperandProperty("DocNumber"), new ConstantValue(txtDocNumber.Text), BinaryOperatorType.Equal);
            CriteriaOperator selectionCriteria2 = new InOperator("TransDocNumber", selectedValues);
            ds.FilterExpression = (GroupOperator.Combine(GroupOperatorType.And, selectionCriteria2, docnum)).ToString();
            DataView apvouch = (DataView)ds.Select(DataSourceSelectArguments.Empty);
            DataTable getval = apvouch.ToTable();
            foreach (DataRow drow in getval.Rows)
            {
                val += drow[0].ToString()+";";
            }

            string[] notselectedValues = val.Split(';');
            
            CriteriaOperator selectionCriteria = new InOperator(glInvoice.KeyFieldName, selectedValues);
            CriteriaOperator notselectionCriteria = new InOperator(glInvoice.KeyFieldName, notselectedValues);
            CriteriaOperator not = new NotOperator(notselectionCriteria);
            SalesInvoicedet.FilterExpression = (GroupOperator.Combine(GroupOperatorType.And, selectionCriteria, not)).ToString();
            Session["SI"] = SalesInvoicedet.FilterExpression;
            gv1.DataSource = SalesInvoicedet;
            if (gv1.DataSourceID != "")
            {
                gv1.DataSourceID = null;
            }
            gv1.DataBind();
            Session["Datatable"] = "1";



            SqlDataSource ds2 = SalesInvoicedet2;
            CriteriaOperator selectionCriteria3 = new InOperator("TransDocNumber", notselectedValues);
            ds2.FilterExpression = (GroupOperator.Combine(GroupOperatorType.And, selectionCriteria3, docnum)).ToString();
            Session["SI2"] = ds2.FilterExpression;
            DataView inb = (DataView)ds2.Select(DataSourceSelectArguments.Empty);
            DataTable dt2 = inb.ToTable();
            dt2.PrimaryKey = new DataColumn[] { 
            dt2.Columns["TransDocNumber"]};

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
                {
                    if (col.ColumnName == "TransDate")
                    {
                        row[col.ColumnName] = DateTime.Parse(gv1.GetRowValues(i, col.ColumnName).ToString()).ToShortDateString();
                    }
                    else if (col.ColumnName == "TransAPAmount" || col.ColumnName == "TransVatAmount" || col.ColumnName == "TransEWTAmount")
                    {
                        row[col.ColumnName] = string.Format("{0:N}",Convert.ToDecimal(gv1.GetRowValues(i, col.ColumnName).ToString()));
                        gv1.Columns[col.ColumnName].CellStyle.HorizontalAlign = HorizontalAlign.Right;
                    }
                    else
                    {
                        row[col.ColumnName] = gv1.GetRowValues(i, col.ColumnName);
                    }
                }
                    
            }

            dt.PrimaryKey = new DataColumn[] { //Sets datatable's primary key for batchupdate reference during adding.
            dt.Columns["TransDocNumber"]};

            //for (int i = 0; i < dt2.Rows.Count; i++)
            //{
                foreach (DataRow _row in dt2.Rows)
                {
                    DataRow row = dt.NewRow();
                    row["LineNumber"] = _row["LineNumber"].ToString();
                    row["Transtype"] = _row["Transtype"].ToString();
                    row["TransDocNumber"] = _row["TransDocNumber"].ToString();
                    row["TransDate"] = DateTime.Parse(_row["TransDate"].ToString()).ToShortDateString();
                    row["TransAPAmount"] = string.Format("{0:N}",Convert.ToDecimal(_row["TransAPAmount"].ToString()));
                    row["TransVatAmount"] = string.Format("{0:N}",Convert.ToDecimal(_row["TransVatAmount"].ToString()));
                    row["TransEWTAmount"] = string.Format("{0:N}",Convert.ToDecimal(_row["TransEWTAmount"].ToString()));
                    row["Currency"] = _row["Currency"].ToString();
                    row["Field1"] = _row["Field1"].ToString();
                    row["Field2"] = _row["Field2"].ToString();
                    row["Field3"] = _row["Field3"].ToString();
                    row["Field4"] = _row["Field4"].ToString();
                    row["Field5"] = _row["Field5"].ToString();
                    row["Field6"] = _row["Field6"].ToString();
                    row["Field7"] = _row["Field7"].ToString();
                    row["Field8"] = _row["Field8"].ToString();
                    row["Field9"] = _row["Field9"].ToString();

                    dt.Rows.Add(row);
                }
            //}

            gv1.DataSource = dt;
            if (gv1.DataSourceID != "")
            {
                gv1.DataSourceID = null;
            }
            gv1.DataBind();

            return dt;
        }

        protected void gv1_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["SI"] = null;
            }

            if (Session["SI"] != null)
            {
                //gv1.DataSourceID = SalesInvoicedet.ID;
                SalesInvoicedet.FilterExpression = Session["SI"].ToString();
                SalesInvoicedet2.FilterExpression = Session["SI2"].ToString();
                //gridview.DataSourceID = null;
            }
        }

        protected void Connection_Init(object sender, EventArgs e)
        {
            sdsDetail.ConnectionString = Session["ConnString"].ToString();
            Masterfilebiz.ConnectionString = Session["ConnString"].ToString();
            refnum.ConnectionString = Session["ConnString"].ToString();
            SalesInvoicedet.ConnectionString = Session["ConnString"].ToString();
            SalesInvoicedet2.ConnectionString = Session["ConnString"].ToString();
            getsales.ConnectionString = Session["ConnString"].ToString();
            MasterfileATC.ConnectionString = Session["ConnString"].ToString();
            refnum2.ConnectionString = Session["ConnString"].ToString();
            getSI.ConnectionString = Session["ConnString"].ToString();
        }

        private string GetFilterExpression()
        {
            string res_str = string.Empty;
            List<CriteriaOperator> lst_operator = new List<CriteriaOperator>();

            if (!string.IsNullOrEmpty(gvSupplier.Text))
                lst_operator.Add(new BinaryOperator("Supplier/Broker", string.Format("%{0}%", gvSupplier.Text), BinaryOperatorType.Like));

            if (lst_operator.Count > 0)
            {
                CriteriaOperator[] op = new CriteriaOperator[lst_operator.Count];
                for (int i = 0; i < lst_operator.Count; i++)
                    op[i] = lst_operator[i];
                CriteriaOperator res_operator = new GroupOperator(GroupOperatorType.And, op);
                res_str = res_operator.ToString();
            }

            return res_str;
        }

        protected void glInvoice_Init(object sender, EventArgs e)
        {

            ASPxGridLookup gridLookup = sender as ASPxGridLookup;
            gridLookup.GridView.CustomCallback += new ASPxGridViewCustomCallbackEventHandler(gridView_CustomCallback2);
            if (Session["FilterExpressionAPVoucher"] != null)
            {
                gridLookup.GridView.DataSourceID = "refnum";
                refnum.FilterExpression = Session["FilterExpressionAPVoucher"].ToString();
            }
        }

        public void gridView_CustomCallback2(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            string column = e.Parameters;
            if (column.Contains("GLP_AIC") || column.Contains("GLP_F") || column.Contains("GLP_AC")) return;//Traps the callback  

            ASPxGridView grid = sender as ASPxGridView;
            //ASPxGridLookup lookup = (ASPxGridLookup)gv3.FindEditRowCellTemplateControl((GridViewDataColumn)gv3.Columns[5], "glSubsidiaryCode");
            //var selectedValues = gvSupplier.Text;
            CriteriaOperator selectionCriteria = new InOperator("Supplier/Broker", new string[] { gvSupplier.Text });
            refnum.FilterExpression = (GroupOperator.Combine(GroupOperatorType.And, selectionCriteria)).ToString();
            Session["FilterExpressionAPVoucher"] = refnum.FilterExpression;
            grid.DataSourceID = "refnum";
            grid.DataBind();
        }
    }
}