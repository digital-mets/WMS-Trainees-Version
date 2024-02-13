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
using GearsAccounting;

namespace GWL
{
    public partial class frmJournalVoucher : System.Web.UI.Page
    {
        Boolean error = false;//Boolean for grid validation
        Boolean view = false;//Boolean for view state
        Boolean check = false;//Boolean for grid validation
        private static string Connection;

        Entity.JournalVoucher _Entity = new JournalVoucher();//Calls entity Customer
        Entity.JournalVoucher.JournalVoucherDetail _EntityDetail = new JournalVoucher.JournalVoucherDetail();//Call entity POdetails

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


            //Details checking. changes datasourceid of gridview if there's no details for a certain docnumber; to enable adding of details.
            if (Request.QueryString["entry"].ToString() == "V")
            {
                view = true;//sets view mode for entry
            }

            if (!IsPostBack)
            {
                Connection = (Session["ConnString"].ToString());
                Session["RRses"] = null;
                Session["Datatable"] = null;
                Session["FilterExpression"] = null;
                switch (Request.QueryString["entry"].ToString())
                {
                    case "N":
                        updateBtn.Text = "Add";
                        //glcheck.ClientVisible = false;
                        
                        break;
                    case "E":
                        updateBtn.Text = "Update";
                        //glRef.ClientEnabled = false;
                        deDocDate.ReadOnly = true;
                        //cboType.ClientEnabled = false;
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

                gv1.KeyFieldName = "DocNumber;LineNumber";
                //txtDocnumber.Value = Session["DocNumber"].ToString(); //sets docnumber from session

                txtDoc.Value = Request.QueryString["docnumber"].ToString();
                //if (Request.QueryString["entry"].ToString() == "N")
                //{


                //}
                //else
                //{
                    //gvBiz.ReadOnly = true;
                   


                    //_Entity.getdata(txtDoc.Text);

                    _Entity.getdata(txtDoc.Text, Session["ConnString"].ToString());//ADD CONN

                    
                    deDocDate.Text = String.IsNullOrEmpty(_Entity.DocDate) ? "" : Convert.ToDateTime(_Entity.DocDate).ToShortDateString();
                    glRef.Value = _Entity.RefTemCode;
                    cboType.Text = _Entity.TemplateType;
                    txtMemo.Value = _Entity.Memo;
                    spTotalDebit.Value = _Entity.TotalDebit;
                    spTotalCredit.Value = _Entity.TotalCredit;

                    txtAddedDate.Text = _Entity.AddedDate;
                    txtLastEditedDate.Text = _Entity.LastEditedDate;
                    txtAddedBy.Text = _Entity.AddedBy;
                    txtLastEditedBy.Text = _Entity.LastEditedBy;
                    txtSubmittedDate.Text = _Entity.SubmittedDate;
                    txtSubmittedBy.Text = _Entity.SubmittedBy;
                    txtHField1.Text = _Entity.Field1;
                    txtHField2.Text = _Entity.Field2;
                    txtHField3.Text = _Entity.Field3;
                    txtHField4.Text = _Entity.Field4;
                    txtHField5.Text = _Entity.Field5;
                    txtHField6.Text = _Entity.Field6;
                    txtHField7.Text = _Entity.Field7;
                    txtHField8.Text = _Entity.Field8;
                    txtHField9.Text = _Entity.Field9;


                //}

                DataTable checkCount = Gears.RetriveData2("Select DocNumber from Accounting.journalvoucherdetail where docnumber = '" + txtDoc.Text + "'",
                    Connection);//ADD CONN
                if (checkCount.Rows.Count > 0)
                {
                    gv1.DataSourceID = "odsDetail";
                }
                else
                {
                    gv1.DataSourceID = "sdsDetail";
                }
                gvJournal.DataSourceID = "odsJournalEntry";
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
            //gparam._Factor = 1;
            // gparam._Action = "Validate";
            //here
            string strresult = GAccounting.JournalVoucher_Validate(gparam);
            cp.JSProperties["cp_valmsg"] = strresult;//Message variable to client side

        }
        #endregion

        #region Post
        private void Post()
        {
            GearsLibrary.Gears.GearsParameter gparam = new Gears.GearsParameter();
            gparam._DocNo = _Entity.DocNumber;
            gparam._UserId = Session["Userid"].ToString();
            gparam._TransType = "ACTJOV";
            gparam._Table = "Accounting.JournalVoucher";
            gparam._Connection = Session["ConnString"].ToString();
            gparam._Factor = -1;
            string strresult = GearsAccounting.GAccounting.JournalVoucher_Post(gparam);
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
        protected void Comboboxload(object sender, EventArgs e)
        {
            ASPxComboBox combobox = sender as ASPxComboBox;
            combobox.ReadOnly = view;
        }
        protected void LookupLoad(object sender, EventArgs e)//Control for all lookup in header
        {
            var look = sender as ASPxGridLookup;
            if (look != null)
            {
                look.ReadOnly = view;
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
        protected void gv1_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)//Control for grid
        {
            if (Request.QueryString["entry"].ToString() == "V" || Request.QueryString["entry"].ToString() == "D")
            {
                ASPxGridView grid = sender as ASPxGridView;
                //grid.SettingsBehavior.AllowGroup = false;
                //grid.SettingsBehavior.AllowSort = false;
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

        //protected void gv1_CustomButtonInitialize(object sender, ASPxGridViewCustomButtonEventArgs e)
        //{
        //    if (Request.QueryString["entry"] == "N")
        //    {
        //        if (e.ButtonID == "Details")
        //        {
        //            e.Visible = DevExpress.Utils.DefaultBoolean.False;
        //        }
        //    }
        //}

        //protected void gv_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
        //{   //Sets icon for grid/Control for Enabling/Disabling of buttons on grid.
        //    if (!IsPostBack && !IsCallback)
        //    {
        //        if (Request.QueryString["entry"].ToString() == "V" || Request.QueryString["entry"].ToString() == "D")
        //        {
        //            if (e.ButtonType == ColumnCommandButtonType.Edit || e.ButtonType == ColumnCommandButtonType.Delete ||
        //                e.ButtonType == ColumnCommandButtonType.New || e.ButtonType == ColumnCommandButtonType.Update ||
        //                e.ButtonType == ColumnCommandButtonType.Cancel)
        //                e.Visible = false;
        //        }
        //    }
        //    if (e.ButtonType == ColumnCommandButtonType.Update)
        //        e.Visible = false;
        //}

        protected void gv1_CustomButtonInitialize(object sender, ASPxGridViewCustomButtonEventArgs e)
        {
            if (Request.QueryString["entry"] == "N")
            {
                if (e.ButtonID == "Details")
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
                gridLookup.GridView.DataSourceID = "SubsiCode";
                SubsiCode.FilterExpression = Session["FilterExpression"].ToString();
            }
        }
        public void gridView_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            string column = e.Parameters.Split('|')[0];//Set column name
            if (column.Contains("GLP_AIC") || column.Contains("GLP_AC") || column.Contains("GLP_F")) return;//Traps the callback
            string code = e.Parameters.Split('|')[1];//Set Item Code
            var gridlookup = sender as ASPxGridView;
            string codes = "";

            if (e.Parameters.Contains("SubsiGetCode"))
            {
                ASPxGridView grid = sender as ASPxGridView;
                ASPxGridLookup lookup = (ASPxGridLookup)gv1.FindEditRowCellTemplateControl((GridViewDataColumn)gv1.Columns[4], "glSubsiCode");
                var selectedValues = code;
                CriteriaOperator selectionCriteria = new InOperator("AccountCode", new string[] { code });
                SubsiCode.FilterExpression = (GroupOperator.Combine(GroupOperatorType.And, selectionCriteria)).ToString();
                Session["FilterExpression"] = SubsiCode.FilterExpression;
                grid.DataSourceID = "SubsiCode";
                grid.DataBind();

                for (int i = 0; i < grid.VisibleRowCount; i++)
                {
                    var col = "SubsiCode";
                    if (grid.GetRowValues(i, col) != null)
                        if (grid.GetRowValues(i, col).ToString() == code)
                        {
                            grid.Selection.SelectRow(i);
                            string key = grid.GetRowValues(i, col).ToString();
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
            _Entity.DocNumber = txtDoc.Text;
            _Entity.DocDate = deDocDate.Text;
            _Entity.RefTemCode = String.IsNullOrEmpty(glRef.Text) ? null : glRef.Value.ToString();
            _Entity.TemplateType = String.IsNullOrEmpty(cboType.Text) ? null : cboType.Value.ToString();
            _Entity.Memo = txtMemo.Text;
            _Entity.TotalDebit = String.IsNullOrEmpty(spTotalDebit.Text) ? 0 : Convert.ToDecimal(spTotalDebit.Text);
            _Entity.TotalCredit = String.IsNullOrEmpty(spTotalCredit.Text) ? 0 : Convert.ToDecimal(spTotalCredit.Text);
           
            //_Entity.AddedBy = txtAddedBy.Text;
            //_Entity.AddedDate = txtAddedDate.Text;
            //_Entity.SubmittedBy = txtSubmittedBy.Text;
            //_Entity.SubmittedDate = txtSubmittedDate.Text;

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
            _Entity.AddedBy = Session["userid"].ToString();
            _Entity.AddedDate = DateTime.Now.ToString();


            switch (e.Parameter)
            {

                 
                case "Add":
                case "Update":
                    gv1.UpdateEdit();

                    
                    string strError = Functions.Submitted(_Entity.DocNumber,"Accounting.JournalVoucher",1,Connection);//NEWADD factor 1 if submit, 2 if approve
                    if (error == false)
                    {
                        check = true;

                        _Entity.UpdateData(_Entity);//Method of inserting for header

                        if (Session["Datatable"] == "1")
                        {
                            gv1.DataSourceID = sdsJVTDetail.ID;
                            gv1.UpdateEdit();
                        }
                        else
                        {
                            gv1.DataSourceID = odsDetail.ID;//Renew datasourceID to entity
                            odsDetail.SelectParameters["DocNumber"].DefaultValue = txtDoc.Text;//Set select parameter to prevent error
                            gv1.UpdateEdit();//2nd initiation to insert grid
                        }
                        Post();
                        Validate();
                        //if (cp.JSProperties["cp_valmsg"].ToString() != "")
                        //{
                        //    cp.JSProperties["cp_message"] = "Please check all the fields!";
                        //    cp.JSProperties["cp_success"] = true;
                        //}
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
                case "Generate":
                    
                    break;

                case "JVT":
                    SqlDataSource ds = Currency;
                    ds.SelectCommand = string.Format("select TotalDebit, TotalCredit from Accounting.JVTemplate where docnumber = '" + glRef.Text + "'");
                    DataView tran = (DataView)ds.Select(DataSourceSelectArguments.Empty);
                    if (tran.Count > 0)
                    {
                        //deDocDate.Text =  Convert.ToDateTime(tran[0][0].ToString()).ToShortDateString();
                        spTotalDebit.Text = tran[0][0].ToString();
                        spTotalCredit.Text = tran[0][1].ToString();

                    }

                    GetSelectedVal();
                    cp.JSProperties["cp_generated"] = true;
                    break;

            }
        }
        protected void grid_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        {

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
            if (Session["Datatable"] == "1" && check == true)
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

                int i = 0;
                foreach (ASPxDataInsertValues values in e.InsertValues)
                {
                    var LineNumber = i;
                    var DocNumber = "";
                    var AccountCode = values.NewValues["AccountCode"];
                    var SubsiCode = values.NewValues["SubsiCode"];
                    var ProfitCenterCode = values.NewValues["ProfitCenterCode"];
                    var CostCenterCode = values.NewValues["CostCenterCode"];
                    var BizPartnerCode = values.NewValues["BizPartnerCode"];
                    var Debit = values.NewValues["Debit"];
                    var Credit = values.NewValues["Credit"];
                    var Field1 = values.NewValues["Field1"];
                    var Field2 = values.NewValues["Field2"];
                    var Field3 = values.NewValues["Field3"];
                    var Field4 = values.NewValues["Field4"];
                    var Field5 = values.NewValues["Field5"];
                    var Field6 = values.NewValues["Field6"];
                    var Field7 = values.NewValues["Field7"];
                    var Field8 = values.NewValues["Field8"];
                    var Field9 = values.NewValues["Field9"];
                    

                    source.Rows.Add(DocNumber, LineNumber, AccountCode, SubsiCode, ProfitCenterCode, CostCenterCode, BizPartnerCode, Debit, Credit, 
                        Field1, Field2, Field3, Field4, Field5, Field6, Field7, Field8, Field9);

                    i++;
                    //if (string.IsNullOrEmpty(OrderQty.ToString()))
                    //{
                    //    OrderQty = 0;
                    //}
                }

                // Updating required rows
                foreach (ASPxDataUpdateValues values in e.UpdateValues)
                {
                    object[] keys = { values.NewValues["DocNumber"], values.NewValues["LineNumber"] };
                    DataRow row = source.Rows.Find(keys);
                    row["AccountCode"] = values.NewValues["AccountCode"];
                    row["SubsiCode"] = values.NewValues["SubsiCode"];
                    row["ProfitCenterCode"] = values.NewValues["ProfitCenterCode"];
                    row["CostCenterCode"] = values.NewValues["CostCenterCode"];
                    row["BizPartnerCode"] = values.NewValues["BizPartnerCode"];
                    row["Debit"] = values.NewValues["Debit"];
                    row["Credit"] = values.NewValues["Credit"];
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

                Gears.RetriveData2("Delete from Accounting.JournalVoucherDetail where DocNumber = '" + txtDoc.Text + "'", Session["ConnString"].ToString());

                foreach (DataRow dtRow in source.Rows)//This is where the data will be inserted into db
                {
                    _EntityDetail.AccountCode = dtRow["AccountCode"].ToString();
                    _EntityDetail.SubsiCode = dtRow["SubsiCode"].ToString();
                    _EntityDetail.ProfitCenterCode = dtRow["ProfitCenterCode"].ToString();
                    _EntityDetail.CostCenterCode = dtRow["CostCenterCode"].ToString();
                    _EntityDetail.BizPartnerCode = dtRow["BizPartnerCode"].ToString();
                    _EntityDetail.Debit = Convert.ToDecimal(dtRow["Debit"].ToString() == "" ? "0" : dtRow["Debit"].ToString());
                    _EntityDetail.Credit = Convert.ToDecimal(dtRow["Credit"].ToString() == "" ? "0" : dtRow["Credit"].ToString());
                    _EntityDetail.Field1 = dtRow["Field1"].ToString();
                    _EntityDetail.Field2 = dtRow["Field2"].ToString();
                    _EntityDetail.Field3 = dtRow["Field3"].ToString();
                    _EntityDetail.Field4 = dtRow["Field4"].ToString();
                    _EntityDetail.Field5 = dtRow["Field5"].ToString();
                    _EntityDetail.Field6 = dtRow["Field6"].ToString();
                    _EntityDetail.Field7 = dtRow["Field7"].ToString();
                    _EntityDetail.Field8 = dtRow["Field8"].ToString();
                    _EntityDetail.Field9 = dtRow["Field9"].ToString();

                    _EntityDetail.AddJournalVoucherDetail(_EntityDetail);

                    //_EntityDetail.DeleteOutboundDetail(_EntityDetail);
                }
            }
        }
        private DataTable GetSelectedVal()
        {
            DataTable dt = new DataTable();
            string[] selectedValues = glRef.Text.Split(';');
            CriteriaOperator selectionCriteria = new InOperator(glRef.KeyFieldName, selectedValues);
            sdsJVTDetail.FilterExpression = (GroupOperator.Combine(GroupOperatorType.And, selectionCriteria)).ToString();
            Session["RRses"] = sdsJVTDetail.FilterExpression;
            gv1.DataSourceID = sdsJVTDetail.ID;
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

        protected void gv1_Init(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                Session["RRses"] = null;
            }

            if (Session["RRses"] != null)
            {
                gv1.DataSource = sdsJVTDetail;
                sdsJVTDetail.FilterExpression = Session["RRses"].ToString();
                //gridview.DataSourceID = null;
            }
        }
    
        #endregion

        protected void glWarehouseCOde_TextChanged(object sender, EventArgs e)
        {

        }

        protected void txtDocnumber_TextChanged(object sender, EventArgs e)
        {

        }

        protected void glBizPartnerCode_TextChanged(object sender, EventArgs e)
        {

        }

        protected void txtpickType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void txtPlant_TextChanged(object sender, EventArgs e)
        {

        }
        protected void Connection_Init(object sender, EventArgs e)
        {

            AccountCode.ConnectionString = Session["ConnString"].ToString();
            SubsiCode.ConnectionString = Session["ConnString"].ToString();
            CostCenterCode.ConnectionString = Session["ConnString"].ToString();
            ProfitCenterCode.ConnectionString = Session["ConnString"].ToString();
            sdsDetail.ConnectionString = Session["ConnString"].ToString();
            BizPartnerCode.ConnectionString = Session["ConnString"].ToString();
            BizAccount.ConnectionString = Session["ConnString"].ToString();
            Currency.ConnectionString = Session["ConnString"].ToString();
            ItemCategory.ConnectionString = Session["ConnString"].ToString();
            JVTemplate.ConnectionString = Session["ConnString"].ToString();
            sdsJVTDetail.ConnectionString = Session["ConnString"].ToString();
        }



        protected void deDocDate_Init(object sender, EventArgs e)
        {
            if (Request.QueryString["entry"] == "N")
            {
                deDocDate.Date = DateTime.Now;
            }
        }
    }
}