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
    public partial class frmItemRevaluation : System.Web.UI.Page
    {
        Boolean error = false;//Boolean for grid validation
        Boolean view = false;//Boolean for view state
        Boolean check = false;//Boolean for grid validation
        Boolean edit = false;   //Boolean for Edit Mode
        string filter = "";

        Entity.ItemRevaluation _Entity = new ItemRevaluation();//Calls entity odsHeader
        Entity.ItemRevaluation.ItemRevaluationDetail _EntityDetail = new ItemRevaluation.ItemRevaluationDetail();//Call entity sdsDetail

        #region page load/entry
        protected void Page_Load(object sender, EventArgs e)
        {
            Gears.UseConnectionString(Session["ConnString"].ToString());

            dtpDocDate.MinDate = Convert.ToDateTime(GearsCommon.GCommon.SystemSetting("BookDate", Session["ConnString"].ToString()).ToString());

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

            txtDocNumber.Value = Request.QueryString["docnumber"].ToString();
            //gv1.DataSource = null;

            if (!IsPostBack)
            {
                Session["FilterExpression"] = null;
                Session["PRitemID"] = null;
                Session["Datatable"] = null;

                if (Request.QueryString["entry"].ToString() == "N")
                {
                    popup.ShowOnPageLoad = false;
                }
                //else
                //{
                _Entity.getdata(txtDocNumber.Text, Session["ConnString"].ToString()); //Method for retrieving data from entity
                dtpDocDate.Text = Convert.ToDateTime(_Entity.DocDate.ToString()).ToShortDateString();

                glReferenceNumber.Value = _Entity.RefNumber.ToString();
                chkIsPrinted.Value = _Entity.IsPrinted;
                memRemarks.Value = _Entity.Remarks.ToString();

                txtHField1.Value = _Entity.Field1.ToString();
                txtHField2.Value = _Entity.Field2.ToString();
                txtHField3.Value = _Entity.Field3.ToString();
                txtHField4.Value = _Entity.Field4.ToString();
                txtHField5.Value = _Entity.Field5.ToString();
                txtHField6.Value = _Entity.Field6.ToString();
                txtHField7.Value = _Entity.Field7.ToString();
                txtHField8.Value = _Entity.Field8.ToString();
                txtHField9.Value = _Entity.Field9.ToString();

                txtHAddedBy.Text = _Entity.AddedBy;
                txtHAddedDate.Text = _Entity.AddedDate;
                txtHLastEditedBy.Text = _Entity.LastEditedBy;
                txtHLastEditedDate.Text = _Entity.LastEditedDate;
                txtHSubmittedBy.Text = _Entity.SubmittedBy;
                txtHSubmittedDate.Text = _Entity.SubmittedDate;
                txtHCancelledBy.Text = _Entity.CancelledBy;
                txtHCancelledDate.Text = _Entity.CancelledDate;
                //}
                
                gv1.KeyFieldName = "LineNumber;DocNumber";

                //V=View, E=Edit, N=New
                switch (Request.QueryString["entry"].ToString())
                {
                    case "N":
                        if (!String.IsNullOrEmpty(_Entity.LastEditedBy))
                        {
                            updateBtn.Text = "Update";
                        }
                        else
                        {
                            updateBtn.Text = "Add";
                        gvRef.DataSourceID = "odsReference";
                        odsReference.SelectParameters["DocNumber"].DefaultValue = txtDocNumber.Text;
                        this.gvRef.Columns["CommandString"].Width = 0;

                        this.gvRef.Columns["RCommandString"].Width = 0;
                  
                        }
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
                        view = true;
                        updateBtn.Text = "Delete";
                        break;
                }

                DataTable dtbldetail = Gears.RetriveData2("SELECT DISTINCT DocNumber FROM Inventory.ItemRevaluationDetail WHERE DocNumber ='" + txtDocNumber.Text + "'", Session["ConnString"].ToString());

                gv1.DataSourceID = (dtbldetail.Rows.Count > 0 ? "odsDetail" : "sdsDetail");
                gvJournal.DataSourceID = "odsJournalEntry";

            }
        }
        #endregion

        #region Validation
        string strresult = "";
        private void Validate()
        {
            GearsLibrary.Gears.GearsParameter gparam = new Gears.GearsParameter();
            gparam._DocNo = _Entity.DocNumber;
            gparam._UserId = Session["Userid"].ToString();
            gparam._TransType = "INVREV";
            gparam._Connection = Session["ConnString"].ToString();
            strresult += GearsInventory.GInventory.ItemRevaluation_Validate(gparam);
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
            gparam._TransType = "INVREV";
            gparam._Table = "Inventory.ItemRevaluation";
            gparam._Factor = -1;
            gparam._Connection = Session["ConnString"].ToString();
            strresult += GearsInventory.GInventory.ItemRevaluation_Post(gparam);
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

        protected void MemoLoad(object sender, EventArgs e)
        {
            var memo = sender as ASPxMemo;
            memo.ReadOnly = view;
        }
        protected void gv1_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)//Control for grid
        {
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
            if (glReferenceNumber.Text =="")
            {
                if (Request.QueryString["entry"] == "N" || Request.QueryString["entry"] == "E")
                {
                    if (e.ButtonType == ColumnCommandButtonType.New || e.ButtonType == ColumnCommandButtonType.Delete)
                    {
                        e.Visible = true;
                    }
                }
            }
            else
            {

                if (Request.QueryString["entry"] == "N" || Request.QueryString["entry"] == "E")
                {
                    if (e.ButtonType == ColumnCommandButtonType.New || e.ButtonType == ColumnCommandButtonType.Delete)
                    {
                        e.Visible = false;
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
            if (IsCallback && (Request.Params["__CALLBACKID"].Contains("gv1") && Request.Params["__CALLBACKID"].Contains(gridLookup.ID)))
            {
                gridLookup.GridView.CustomCallback += new ASPxGridViewCustomCallbackEventHandler(gridView_CustomCallback);
                if (Session["PRitemID"] != null)
                {
                    if (Session["PRitemID"].ToString() == glIDFinder(gridLookup.ID))
                    {
                        gridLookup.GridView.DataSourceID = "Masterfileitemdetail";
                        Masterfileitemdetail.SelectCommand = Session["PRsql"].ToString();
                        //Masterfileitemdetail.FilterExpression = Session["FilterExpression"].ToString();
                        //Session["FilterExpression"] = null;
                        gridLookup.DataBind();
                    }
                }
            }
            //else
            //{
            //    gridLookup.GridView.DataSourceID = "Masterfileitemdetail";
            //}
        }

        public string glIDFinder(string glID)
        {
            if (glID.Contains("ColorCode"))
                return "ColorCode";
            else if (glID.Contains("ClassCode"))
                return "ClassCode";
            else if (glID.Contains("UnitBase"))
                return "UnitBase";
            else
                return "SizeCode";
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

                DataTable getUnit = Gears.RetriveData2("SELECT DISTINCT UnitBase FROM Masterfile.Item  WHERE ItemCode = '" + itemcode + "'", Session["ConnString"].ToString());//ADD CONN

                if (getUnit.Rows.Count == 0)
                {
                    codes += "" + ";";
                }
                else
                {
                    foreach (DataRow dt in getUnit.Rows)
                    {
                        codes += dt["UnitBase"].ToString() + ";";
                    }
                }

                itemlookup.JSProperties["cp_identifier"] = "ItemCode";
                itemlookup.JSProperties["cp_codes"] = codes;
            }
            else
            {
                if (e.Parameters.Contains("ColorCode"))
                {
                    Masterfileitemdetail.SelectCommand = "SELECT DISTINCT ItemCode, ColorCode FROM Masterfile.ItemDetail where ItemCode = '" + itemcode + "'";
                    Session["PRitemID"] = "ColorCode";
                }
                else if (e.Parameters.Contains("ClassCode"))
                {
                    Masterfileitemdetail.SelectCommand = "SELECT DISTINCT ItemCode, ClassCode FROM Masterfile.ItemDetail where ItemCode = '" + itemcode + "'";
                    Session["PRitemID"] = "ClassCode";
                    //Masterfileitemdetail.SelectCommand = "SELECT DISTINCT ItemCode, '' AS ColorCode, ClassCode, '' AS SizeCode FROM Masterfile.ItemDetail where ItemCode = '" + itemcode + "'";
                }
                else if (e.Parameters.Contains("SizeCode"))
                {
                    Masterfileitemdetail.SelectCommand = "SELECT DISTINCT ItemCode, SizeCode FROM Masterfile.ItemDetail where ItemCode = '" + itemcode + "'";
                    Session["PRitemID"] = "SizeCode";
                    //Masterfileitemdetail.SelectCommand = "SELECT DISTINCT ItemCode, '' AS [ColorCode], '' AS [ClassCode], [SizeCode] FROM Masterfile.ItemDetail where ItemCode = '" + itemcode + "'";
                }
                else if (e.Parameters.Contains("UnitBase"))
                {
                    Masterfileitemdetail.SelectCommand = "SELECT DISTINCT ItemCode, UnitBase FROM Masterfile.Item where ItemCode = '" + itemcode + "'";
                    Session["PRitemID"] = "UnitBase";
                }

                ASPxGridView grid = sender as ASPxGridView;
                //ASPxGridLookup lookup = (ASPxGridLookup)gv1.FindEditRowCellTemplateControl((GridViewDataColumn)gv1.Columns[1], "glItemCode");
                //var selectedValues = itemcode;
                //CriteriaOperator selectionCriteria = new InOperator(lookup.KeyFieldName, new string[] { itemcode });
                //Masterfileitemdetail.FilterExpression = (GroupOperator.Combine(GroupOperatorType.And, selectionCriteria)).ToString();
                Session["PRsql"] = Masterfileitemdetail.SelectCommand;
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
            _Entity.DocNumber = txtDocNumber.Value.ToString();
            _Entity.DocDate = dtpDocDate.Text;
            _Entity.RefNumber = String.IsNullOrEmpty(glReferenceNumber.Text) ? null : glReferenceNumber.Text;
            _Entity.Remarks = String.IsNullOrEmpty(memRemarks.Text) ? null : memRemarks.Text;
            _Entity.Connection = Session["ConnString"].ToString();
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


            switch (e.Parameter)
            {
                case "Add":

                    gv1.UpdateEdit();
                    
                    string strError = Functions.Submitted(_Entity.DocNumber, "Inventory.ItemRevaluation", 1, Session["ConnString"].ToString());//NEWADD factor 1 if submit, 2 if approve
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
                        _Entity.UpdateData(_Entity);//Method of inserting for header


                        if (Session["Datatable"] == "1")
                        {
                            Gears.RetriveData2("DELETE FROM Inventory.ItemRevaluationDetail where Docnumber='" + txtDocNumber.Text + "'", Session["ConnString"].ToString());
                          
                            gv1.DataSource = GetSelectedVal();
                            gv1.UpdateEdit();
                           
                        }
                        else
                        {
                            gv1.DataSourceID = odsDetail.ID;//Renew datasourceID to entity
                            odsDetail.SelectParameters["DocNumber"].DefaultValue = txtDocNumber.Text;//Set select parameter to prevent error
                            gv1.UpdateEdit();//2nd initiation to insert grid
                          
                        }


                        Validate();
                        _Entity.UpdateUnitFactor(txtDocNumber.Text.Trim(), Session["ConnString"].ToString());
                        Post();
                        cp.JSProperties["cp_message"] = "Successfully Added!";
                        cp.JSProperties["cp_success"] = true;
                        cp.JSProperties["cp_close"] = true;
                        Session["Refresh"] = "1";
                    }
                    else
                    {
                        cp.JSProperties["cp_message"] = "Please check all the fields!";
                        cp.JSProperties["cp_success"] = true;
                        edit = false;
                    }

                    break;

                case "Update":

                    gv1.UpdateEdit();
                    
                    string strError1 = Functions.Submitted(_Entity.DocNumber, "Inventory.ItemRevaluation", 1, Session["ConnString"].ToString());//NEWADD factor 1 if submit, 2 if approve
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
                        check = true;
                        _Entity.LastEditedBy = Session["userid"].ToString();
                        _Entity.UpdateData(_Entity);//Method of inserting for header


                        if (Session["Datatable"] == "1")
                        {
                            Gears.RetriveData2("DELETE FROM Inventory.ItemRevaluationDetail where Docnumber='" + txtDocNumber.Text + "'", Session["ConnString"].ToString());

                            gv1.DataSource = GetSelectedVal();
                            gv1.UpdateEdit();

                        }
                        else
                        {
                            //Gears.RetriveData2("DELETE FROM Inventory.ItemRevaluationDetail where Docnumber='" + txtDocNumber.Text + "'", Session["ConnString"].ToString());

                            gv1.DataSourceID = odsDetail.ID;//Renew datasourceID to entity
                            odsDetail.SelectParameters["DocNumber"].DefaultValue = txtDocNumber.Text;//Set select parameter to prevent error
                            gv1.UpdateEdit();//2nd initiation to insert grid

                        }

                        Validate();
                        _Entity.UpdateUnitFactor(txtDocNumber.Text.Trim(), Session["ConnString"].ToString());
                        Post();
                        cp.JSProperties["cp_message"] = "Successfully Updated!";
                        cp.JSProperties["cp_success"] = true;
                        cp.JSProperties["cp_close"] = true;
                        Session["Refresh"] = "1";
                    }
                    else
                    {
                        cp.JSProperties["cp_message"] = "Please check all the fields!";
                        cp.JSProperties["cp_success"] = true;
                        edit = false;
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

                case "RefGrid":
                    gv1.DataBind();
                    break;

                case "Generate":
                    GetSelectedVal();
                
                    break;

                case "ItemRev":
                    gv1.DataSourceID = "sdsDetail";
                    if(glReferenceNumber.Text == "")
                    {
                        Session["Datatable"] = "";
                    }
                    break;

                    
            }
        }

        private DataTable GetSelectedVal()
        {
            gv1.DataSourceID = null;
            
            DataTable dt = new DataTable();
     


            sdsRRDetail.SelectCommand = "select DocNumber,LineNumber,A.ItemCode,B.FullDesc,ColorCode,ClassCode,SizeCode,RemainingQty as OnHandQty,UnitCost as OldCost,0.0000 as NewCost,0.000 as BaseQty,1 as UnitFactor,'' as StatusCode,'' as BarcodeNo ,'' as Field1,'' as Field2,'' as Field3,'' as Field4,'' as Field5,'' as Field6,'' as Field7,'' as Field8,'' as Field29,'1' as Version,UnitBase as Unit from Accounting.CostUse A"
                                                + " INNER JOIN Masterfile.Item B "
                                                + " ON A.ItemCode = B.ItemCode "
                                                + " WHERE ISNULL(RemainingQty,0)>0 "
                                                + " AND DocNumber='"+glReferenceNumber.Text+"'"
                                                 + "order by DocDate desc";


            gv1.DataSource = sdsRRDetail;
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
            //dt.Columns["LineNumber"]};

            return dt;
        }
     
        protected void grid_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        {
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
                e.InsertValues.Clear();
                e.UpdateValues.Clear();
            }

            if (Session["Datatable"] == "1" && check == true)
            {
                e.Handled = true;



                DataTable source = GetSelectedVal();


                foreach (ASPxDataUpdateValues values in e.UpdateValues)
                {
                    object[] keys = { values.NewValues["DocNumber"], values.NewValues["LineNumber"] };
                    DataRow row = source.Rows.Find(keys);
                    row["ItemCode"] = values.NewValues["ItemCode"];
                    row["FullDesc"] = values.NewValues["FullDesc"];
                    row["ColorCode"] = values.NewValues["ColorCode"];
                    row["ClassCode"] = values.NewValues["ClassCode"];
                    row["SizeCode"] = values.NewValues["SizeCode"];
                    row["Unit"] = values.NewValues["Unit"];
                    row["OnHandQty"] = values.NewValues["OnHandQty"];
                    row["NewCost"] = values.NewValues["NewCost"];
                    row["OldCost"] = values.NewValues["OldCost"];
                    row["BaseQty"] = values.NewValues["BaseQty"];
                    row["UnitFactor"] = values.NewValues["UnitFactor"];
                    row["StatusCode"] = values.NewValues["StatusCode"];
                    row["BarcodeNo"] = values.NewValues["BarcodeNo"];
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



                foreach (ASPxDataDeleteValues values in e.DeleteValues)
                {
                    try
                    {
                        object[] keys = { values.Keys["DocNumber"], values.Keys["LineNumber"] };
                        source.Rows.Remove(source.Rows.Find(keys));
                    }
                    catch (Exception)
                    {
                        continue;
                    }
                }



                foreach (ASPxDataInsertValues values in e.InsertValues)//for insert chu chu
                {
                    var DocNumber = values.NewValues["DocNumber"];
                    var LineNumber = values.NewValues["LineNumber"];
                    var ItemCode = values.NewValues["ItemCode"];
                    var FullDesc = values.NewValues["FullDesc"];
                    var ColorCode = values.NewValues["ColorCode"];
                    var ClassCode = values.NewValues["ClassCode"];
                    var SizeCode = values.NewValues["SizeCode"];
                    var Unit = values.NewValues["Unit"];
                    var OnHandQty = values.NewValues["OnHandQty"];
                    var NewCost = values.NewValues["NewCost"];
                    var OldCost = values.NewValues["OldCost"];
                    var BaseQty = values.NewValues["BaseQty"];
                    var UnitFactor = values.NewValues["UnitFactor"];
                    var StatusCode = values.NewValues["StatusCode"];
                    var BarcodeNo = values.NewValues["BarcodeNo"];
                    var Field1 = values.NewValues["Field1"];
                    var Field2 = values.NewValues["Field2"];
                    var Field3 = values.NewValues["Field3"];
                    var Field4 = values.NewValues["Field4"];
                    var Field5 = values.NewValues["Field5"];
                    var Field6 = values.NewValues["Field6"];
                    var Field7 = values.NewValues["Field7"];
                    var Field8 = values.NewValues["Field8"];
                    var Field9 = values.NewValues["Field9"];
                    source.Rows.Add(DocNumber, LineNumber, ItemCode, FullDesc, ColorCode, ClassCode, SizeCode, Unit, OnHandQty, OldCost, NewCost, BaseQty, UnitFactor, StatusCode, BarcodeNo, Field1, Field2, Field3, Field4, Field5, Field6, Field7, Field8, Field9);

                }

                foreach (DataRow dtRow in source.Rows)//This is where the data will be inserted into db
                {
                    _EntityDetail.ItemCode = dtRow["ItemCode"].ToString();
                    _EntityDetail.FullDesc = dtRow["FullDesc"].ToString();
                    _EntityDetail.ColorCode = dtRow["ColorCode"].ToString();
                    _EntityDetail.ClassCode = dtRow["ClassCode"].ToString();
                    _EntityDetail.SizeCode = dtRow["SizeCode"].ToString();
                    _EntityDetail.Unit = dtRow["Unit"].ToString();
                    _EntityDetail.OnHandQty = Convert.ToDecimal(Convert.IsDBNull(dtRow["OnHandQty"]) ? 0 : dtRow["OnHandQty"]);
                    _EntityDetail.OldCost = Convert.ToDecimal(Convert.IsDBNull(dtRow["OldCost"]) ? 0 : dtRow["OldCost"]);
                    _EntityDetail.NewCost = Convert.ToDecimal(Convert.IsDBNull(dtRow["NewCost"]) ? 0 : dtRow["NewCost"]);
                    _EntityDetail.BaseQty = Convert.ToDecimal(Convert.IsDBNull(dtRow["BaseQty"]) ? 0 : dtRow["BaseQty"]); 
                   _EntityDetail.StatusCode = dtRow["StatusCode"].ToString();
                    _EntityDetail.BarcodeNo = dtRow["BarcodeNo"].ToString();
                    _EntityDetail.UnitFactor = Convert.ToDecimal(Convert.IsDBNull(dtRow["UnitFactor"]) ? 0 : dtRow["UnitFactor"]); 

                    _EntityDetail.Field1 = dtRow["Field1"].ToString();
                    _EntityDetail.Field2 = dtRow["Field2"].ToString();
                    _EntityDetail.Field3 = dtRow["Field3"].ToString();
                    _EntityDetail.Field4 = dtRow["Field4"].ToString();
                    _EntityDetail.Field5 = dtRow["Field5"].ToString();
                    _EntityDetail.Field6 = dtRow["Field6"].ToString();
                    _EntityDetail.Field7 = dtRow["Field7"].ToString();
                    _EntityDetail.Field8 = dtRow["Field8"].ToString();
                    _EntityDetail.Field9 = dtRow["Field9"].ToString();

                    _EntityDetail.AddItemRevaluationDetail(_EntityDetail);
                }
            }
        }
        #endregion

        protected void gv1_Init(object sender, EventArgs e)
        {
            //if (!IsPostBack)
            //{
            //    Session["detail"] = null;
            //}

            //if (Session["detail"] != null)
            //{
            //    gv1.DataSourceID = sdsReqDetail.ID;
            //}
        }

        //private DataTable GetSelectedVal()
        //{
        //    Session["Datatable"] = "0";
        //    if (gv1.DataSourceID != "")
        //    {
        //        gv1.DataSourceID = null;
        //    }
        //    gv1.DataBind();
        //    DataTable dt = new DataTable();
        //    string[] selectedValues = aglRefNum.Text.Split(';');
        //    CriteriaOperator selectionCriteria = new InOperator("DocNumber", selectedValues);
        //    string RefSO = selectionCriteria.ToString();

        //    sdsReqDetail.SelectCommand = "SELECT DocNumber, RIGHT('00000'+ CAST(ROW_NUMBER() OVER (ORDER BY DocNumber) AS VARCHAR(5)),5) AS LineNumber, A.ItemCode, FullDesc, ColorCode, ClassCode, SizeCode, 0.00 AS IssuedQty, Unit, 0.00 AS IssuedBulkQty, '' AS IssuedBulkUnit, RequestedBulkQty, "
        //    + " RequestedQty, IsByBulk, ISNULL(Cost,0) AS Cost, ISNULL(ItemPrice,0) ItemPrice, 0.00 AS ReturnedQty, '' AS ReturnedBulkQty, '' AS ReturnedBulkUnit, '' AS StatusCode, 0.00 AS BaseQty, ISNULL(A.BarcodeNo,'') AS BarcodeNo, ISNULL(A.UnitFactor,'0.0000') UnitFactor, "
        //    + " A.Field1, A.Field2, A.Field3, A.Field4, A.Field5, A.Field6, A.Field7, A.Field8, A.Field9, " 
        //    + " '2' AS Version FROM Inventory.RequestDetail A INNER JOIN Masterfile.Item B ON A.ItemCode = B.ItemCode WHERE DocNumber = '" + aglRefNum.Text.Trim() + "'";

        //    gv1.DataSource = sdsReqDetail;
        //    if (gv1.DataSourceID != "")
        //    {
        //        gv1.DataSourceID = null;
        //    }
        //    gv1.DataBind();
        //    Session["Datatable"] = "1";

        //    foreach (GridViewColumn col in gv1.VisibleColumns)
        //    {
        //        GridViewDataColumn dataColumn = col as GridViewDataColumn;
        //        if (dataColumn == null) continue;
        //        dt.Columns.Add(dataColumn.FieldName);
        //    }
        //    for (int i = 0; i < gv1.VisibleRowCount; i++)
        //    {
        //        DataRow row = dt.Rows.Add();
        //        foreach (DataColumn col in dt.Columns)
        //            row[col.ColumnName] = gv1.GetRowValues(i, col.ColumnName);
        //    }

        //    dt.PrimaryKey = new DataColumn[] { //Sets datatable's primary key for batchupdate reference during adding.
        //    dt.Columns["LineNumber"], dt.Columns["DocNumber"]};

        //    return dt;
        //}
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
            //sdsDetail.ConnectionString = Session["ConnString"].ToString();
            //sdsItem.ConnectionString = Session["ConnString"].ToString();
            //sdsItemDetail.ConnectionString = Session["ConnString"].ToString();
            //sdsUnit.ConnectionString = Session["ConnString"].ToString();
            //sdsBulkUnit.ConnectionString = Session["ConnString"].ToString();
            //sdsColor.ConnectionString = Session["ConnString"].ToString();
            //sdsClass.ConnectionString = Session["ConnString"].ToString();
            //sdsSize.ConnectionString = Session["ConnString"].ToString();
        }
        protected void glItemCode_Init(object sender, EventArgs e)
        {
            ASPxGridLookup gridLookup = sender as ASPxGridLookup;
            if (IsCallback && Request.Params["__CALLBACKID"].Contains("glItemCode")
                && Request.Params["__CALLBACKID"].Contains(gridLookup.ID))
            {
                //gridLookup.GridView.CustomCallback += new ASPxGridViewCustomCallbackEventHandler(item_CustomCallback);
                Masterfileitem.SelectCommand = "select  DISTINCT  A.ItemCode,FullDesc,ShortDesc  from Accounting.CostUse A INNER JOIN Masterfile.Item B ON A.ItemCode = B.ItemCode where  ISNULL(RemainingQty,0)>0";
                gridLookup.DataBind();
            }
            if (IsCallback && Request.Params["__CALLBACKID"].Contains("glItemCode")
               && Request.Params["__CALLBACKPARAM"].Contains("GLP_AC")
               && Request.Params["__CALLBACKPARAM"].Contains("CUSTOMCALLBACK"))
            {
                string codes = "";
                string param = Request.Params["__CALLBACKPARAM"].ToString().Substring(Request.Params["__CALLBACKPARAM"].ToString().LastIndexOf('|') + 1);
                //string changes = param.Split(';')[0];
                string itemcode = param.Split(';')[1];
                if (string.IsNullOrEmpty(itemcode)) return;

                DataTable getColor = Gears.RetriveData2("Select DISTINCT ColorCode FROM Accounting.CostUse  where itemcode = '" + itemcode + "' and  ISNULL(RemainingQty,0)>0 ", Session["ConnString"].ToString());//ADD CONN

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

                DataTable getClass = Gears.RetriveData2("Select DISTINCT ClassCode FROM Accounting.CostUse  where itemcode = '" + itemcode + "' and  ISNULL(RemainingQty,0)>0 ", Session["ConnString"].ToString());//ADD CONN

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

                DataTable getSize = Gears.RetriveData2("Select DISTINCT Sizecode FROM Accounting.CostUse  where itemcode = '" + itemcode + "' and  ISNULL(RemainingQty,0)>0 ", Session["ConnString"].ToString());//ADD CONN

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

                DataTable getUnit = Gears.RetriveData2("SELECT DISTINCT UnitBase FROM Masterfile.Item  WHERE ItemCode = '" + itemcode + "'", Session["ConnString"].ToString());//ADD CONN

                if (getUnit.Rows.Count == 0)
                {
                    codes += "" + ";";
                }
                else
                {
                    foreach (DataRow dt in getUnit.Rows)
                    {
                        codes += dt["UnitBase"].ToString() + ";";
                    }
                }

                DataTable getOnhandCost = Gears.RetriveData2("Select DISTINCT RemainingQty,UnitCost FROM Accounting.CostUse  where itemcode = '" + itemcode + "' and  ISNULL(RemainingQty,0)>0 ", Session["ConnString"].ToString());//ADD CONN


                if (getOnhandCost.Rows.Count > 1)
                {
                    codes += "" + ";";
                    codes += "" + ";";
                }
                else
                {
                    foreach (DataRow dt in getOnhandCost.Rows)
                    {
                        codes += dt["RemainingQty"].ToString() + ";";
                        codes += dt["UnitCost"].ToString() + ";";
                    }
                }

                DataTable getFullDesc = Gears.RetriveData2("SELECT DISTINCT FullDesc FROM Masterfile.Item  WHERE ItemCode = '" + itemcode + "'", Session["ConnString"].ToString());//ADD CONN

                if (getFullDesc.Rows.Count == 0)
                {
                    codes += "" + ";";
                }
                else
                {
                    foreach (DataRow dt in getFullDesc.Rows)
                    {
                        codes += dt["FullDesc"].ToString() + ";";
                    }
                }

                gridLookup.GridView.JSProperties["cp_identifier"] = "ItemCode";
                gridLookup.GridView.JSProperties["cp_codes"] = codes;
                gridLookup.GridView.JSProperties["cp_valch"] = true;
            }
        }
    
     
        public void CheckSubmit()
        {
            string strError = Functions.Submitted(_Entity.DocNumber, "Inventory.ItemRevaluation", 1, Session["ConnString"].ToString());
            if (!string.IsNullOrEmpty(strError))
            {
                cp.JSProperties["cp_message"] = strError;
                cp.JSProperties["cp_success"] = true;
                cp.JSProperties["cp_forceclose"] = true;
                return;
            }
        }
    }
}