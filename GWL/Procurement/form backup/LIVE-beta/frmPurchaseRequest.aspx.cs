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
    public partial class frmPurchaseRequest : System.Web.UI.Page
    {
        Boolean error = false;//Boolean for grid validation
        Boolean view = false;//Boolean for view state
        Boolean check = false;//Boolean for grid validation
        Boolean edit = false; //Bolean for edit state
        Boolean dateValidation = false;

        private static string strError;

        Entity.PurchaseRequest _Entity = new PurchaseRequest();//Calls entity ICN
        Entity.PurchaseRequest.PurchaseRequestDetail _EntityDetail = new PurchaseRequest.PurchaseRequestDetail();//Call entity POdetails

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


            gvJODetails.KeyFieldName = "LineNumber;ItemCode;ColorCode;ClassCode;SizeCode;RequestQty;UnitBase";

            if (!IsPostBack)
            {
                Session["preqsqljo"] = null;
                //txtDocnumber.Value = Session["DocNumber"].ToString(); //sets docnumber from session
                txtDocnumber.ReadOnly = true;
                txtDocnumber.Value = Request.QueryString["docnumber"].ToString();
                _Entity.getdata(txtDocnumber.Text, Session["ConnString"].ToString());//ADD CONN

                InitControls();
                switch (Request.QueryString["entry"].ToString())
                {
                    case "N":
                        if (string.IsNullOrEmpty(_Entity.LastEditedBy))
                        {
                            updateBtn.Text = "Add";
                            frmlayout1.FindItemOrGroupByName("ReferenceTransaction").ClientVisible = false;
                        }
                        else
                        {
                            updateBtn.Text = "Update";
                        }
                        popup.ShowOnPageLoad = false;
                        dtDocDate.Text = DateTime.Now.ToShortDateString();
                        break;
                    case "E":
                        updateBtn.Text = "Update";
                        edit = true;
                        break;
                    case "V":
                        view = true;//sets view mode for entry
                        updateBtn.Text = "Close";
                        glcheck.ClientVisible = false;
                        break;
                    case "D":
                        glcheck.ClientVisible = false;
                        view = true;
                        updateBtn.Text = "Delete";
                        break;
                }

                    glCustomerCode.Value = _Entity.CustomerCode;
                    glStockNumber.Value = _Entity.StockNumber;
                    glCostCenter.Value = _Entity.CostCenter;
                    memoRemarks.Value = _Entity.Remarks;
                    txtStatus.Value = _Entity.Status;
                    chkIsPrinted.Value = _Entity.IsPrinted;
                    txtPODocNumber.Value = _Entity.PODocNumber;


                    dtDocDate.Value = String.IsNullOrEmpty(_Entity.DocDate) ? DateTime.Now : Convert.ToDateTime(_Entity.DocDate);
                    dtTargetDate.Value = String.IsNullOrEmpty(_Entity.TargetDate) ? DateTime.Now : Convert.ToDateTime(_Entity.TargetDate);


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
                    txtApprovedBy.Text = _Entity.ApprovedBy;
                    txtApprovedDate.Text = _Entity.ApprovedDate;
                    txtCancelledBy.Text = _Entity.CancelledBy;
                    txtCancelledDate.Text = _Entity.CancelledDate;
                    txtForceClosedBy.Text = _Entity.ForceClosedBy;
                    txtForceClosedDate.Text = _Entity.ForceClosedDate;

                    SetStatus();
                    setEnables();


                    DataTable dtblDetail = Gears.RetriveData2("Select DocNumber FROM Procurement.PurchaseRequestDetail WHERE DocNumber = '" + txtDocnumber.Text + "'", Session["ConnString"].ToString());//ADD CONN
                    gv1.DataSourceID = (dtblDetail.Rows.Count > 0 ? "odsDetail" : "sdsDetail");

                    dtblDetail = Gears.RetriveData2("Select DocNumber FROM Procurement.PurchaseRequestService WHERE DocNumber = '" + txtDocnumber.Text + "'", Session["ConnString"].ToString());//ADD CONN
                    gvService.DataSourceID = (dtblDetail.Rows.Count > 0 ? "odsDetail2" : "sdsDetail2");

                    gvRef.DataSourceID = "odsReference";
                    this.gvRef.Columns["CommandString"].Width = 0;
                    this.gvRef.Columns["RCommandString"].Width = 0;
            }
        }
        #endregion
        
        protected void setEnables()
        {
            if (String.IsNullOrEmpty(glCustomerCode.Text))
                glStockNumber.ClientEnabled = false;
            else
                glStockNumber.ClientEnabled = true;

        }

        #region Validation
        private void Validate()
        {
            GearsLibrary.Gears.GearsParameter gparam = new Gears.GearsParameter();
            gparam._Connection = Session["ConnString"].ToString();
            gparam._DocNo = _Entity.DocNumber;
            gparam._UserId = Session["Userid"].ToString();
            gparam._TransType = "PRCPRM";

            string strresult = GearsProcurement.GProcurement.PurchaseRequest_Validate(gparam);

            cp.JSProperties["cp_valmsg"] = strresult;//Message variable to client side
            
        }
        #endregion

        #region Set controls' state/behavior/etc...
        protected void ButtonLoad(object sender, EventArgs e)//Control for all textbox
        {
            ASPxButton btn = sender as ASPxButton;
            btn.ClientVisible = !view;
        }
        protected void TextboxLoad(ASPxEdit sender)
        {
            ASPxTextBox text = sender as ASPxTextBox;
            text.ReadOnly = view;
        }
        protected void LookupLoad(ASPxEdit sender)//Control for all lookup in header
        {
            ASPxGridLookup look = sender as ASPxGridLookup;
            look.DropDownButton.Enabled = !view;
            look.ReadOnly = view;
        }
        protected void CheckBoxLoad(ASPxEdit sender)
        {
            var check = sender as ASPxCheckBox;
            check.ReadOnly = view;
        }
        protected void ComboBoxLoad(ASPxEdit sender)
        {
            var combo = sender as ASPxComboBox;
            combo.ReadOnly = view;
        }
        protected void MemoLoad(ASPxEdit sender)
        {
            var memo = sender as ASPxMemo;
            memo.ReadOnly = view;
        }
        protected void Date_Load(ASPxEdit sender)//Control for all date editor
        {
            ASPxDateEdit date = sender as ASPxDateEdit;
            date.DropDownButton.Enabled = !view;
            date.ReadOnly = view;
        }
        protected void SpinEdit_Load(ASPxEdit sender)//Control for all numeric entries in header
        {
            ASPxSpinEdit spinedit = sender as ASPxSpinEdit;
            spinedit.ReadOnly = view;
        }
        protected void gv_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
        {   //Sets icon for grid/Control for Enabling/Disabling of buttons on grid.
            ASPxGridView grid = sender as ASPxGridView;
            if (!IsPostBack || Request.Params["__CALLBACKID"].Contains(grid.ID))
            {
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
        }
        protected void gv1_CustomButtonInitialize(object sender, ASPxGridViewCustomButtonEventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
            if (!IsPostBack || Request.Params["__CALLBACKID"].Contains(grid.ID))
            {
                if (Request.QueryString["entry"] == "N")
                {
                    if (e.ButtonID == "Details")
                    {
                        e.Visible = DevExpress.Utils.DefaultBoolean.False;
                    }
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

            _Entity.DocNumber     = txtDocnumber.Text;
            _Entity.Connection = Session["ConnString"].ToString(); //ADD CONN
            _Entity.DocDate       = dtDocDate.Text;
            _Entity.CustomerCode = glCustomerCode.Text;
            _Entity.StockNumber = glStockNumber.Text;
            _Entity.CostCenter    = glCostCenter.Text;
            _Entity.Remarks = memoRemarks.Text;
            _Entity.Status        = txtStatus.Text;

            _Entity.IsPrinted     = Convert.ToBoolean(chkIsPrinted.Value);

            _Entity.TargetDate = dtTargetDate.Text;

            _Entity.Field1 = txtHField1.Text;
            _Entity.Field2 = txtHField2.Text;
            _Entity.Field3 = txtHField3.Text;
            _Entity.Field4 = txtHField4.Text;
            _Entity.Field5 = txtHField5.Text;
            _Entity.Field6 = txtHField6.Text;
            _Entity.Field7 = txtHField7.Text;
            _Entity.Field8 = txtHField8.Text;
            _Entity.Field9 = txtHField9.Text;

            SaveStatus();

            switch (e.Parameter)
            {
                case "Add":

                    DateValidation();
                    gv1.UpdateEdit();
                    strError = Functions.Submitted(_Entity.DocNumber,"Procurement.PurchaseRequest",2,Session["ConnString"].ToString());//NEWADD factor 1 if submit, 2 if approve
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
                        _Entity.UpdateData(_Entity);//Method of Updating header


                        gv1.DataSourceID = "odsDetail";
                        odsDetail.SelectParameters["DocNumber"].DefaultValue = txtDocnumber.Text; 
                        gv1.UpdateEdit();//2nd Initiation to update grid
                        gvService.DataSourceID = "odsDetail2";
                        odsDetail2.SelectParameters["DocNumber"].DefaultValue = txtDocnumber.Text;
                        gvService.UpdateEdit();
                        Validate();
                        cp.JSProperties["cp_message"] = "Successfully Added!";
                        cp.JSProperties["cp_close"] = true;
                        Session["Refresh"] = "1";
                    }
                    else
                    {
                        if(dateValidation == true)
                        {
                            cp.JSProperties["cp_message"] = "Target Date is less than Document Date, should be equal or greater than doc date!";
                            cp.JSProperties["cp_success"] = true;
                        }
                        else
                        {
                            cp.JSProperties["cp_message"] = "Please check all the fields!";
                            cp.JSProperties["cp_success"] = true;
                        }

                    }

                    break;

                case "Update":

                    DateValidation();
                    gv1.UpdateEdit();
                    strError = Functions.Submitted(_Entity.DocNumber,"Procurement.PurchaseRequest",2,Session["ConnString"].ToString());//NEWADD factor 1 if submit, 2 if approve
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
                        _Entity.UpdateData(_Entity);//Method of Updating header


                        gv1.DataSourceID = "odsDetail";
                        odsDetail.SelectParameters["DocNumber"].DefaultValue = txtDocnumber.Text; 
                        gv1.UpdateEdit();//2nd Initiation to update grid
                        gvService.DataSourceID = "odsDetail2";
                        odsDetail2.SelectParameters["DocNumber"].DefaultValue = txtDocnumber.Text;
                        gvService.UpdateEdit();
                        Validate();
                        cp.JSProperties["cp_message"] = "Successfully Updated!";
                        cp.JSProperties["cp_close"] = true;
                        Session["Refresh"] = "1";
                    }
                    else
                    {
                        if (dateValidation == true)
                        {
                            cp.JSProperties["cp_message"] = "Target Date is less than Document Date, should be equal or greater than doc date!";
                            cp.JSProperties["cp_success"] = true;
                        }
                        else
                        {
                            cp.JSProperties["cp_message"] = "Please check all the fields!";
                            cp.JSProperties["cp_success"] = true;
                        }
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

                case "CustomerCodeCase":
                    SetText();
                    setEnables();
                    break;

                case "GetJODetails":
                    GetJODetails();
                    break;

                case "POPUPGetJODetail":
                    //POPUPGetJODetail();
                    break;
                    
            }
        }

        private DataTable GetJODetails()
        {

            gvJODetails.DataSourceID = null;
            // string SQL = null;

            DataTable dt = new DataTable();

            JODetailsds.SelectCommand = "SELECT DISTINCT ISNULL(JOBOM.ItemCode,'') AS ItemCode"
                                    + " , ISNULL(JOBOM.ColorCode,'') AS ColorCode"
                                    + " , ISNULL(JOBOM.ClassCode,'') AS ClassCode"
                                    + " , ISNULL(JOBOM.SizeCode,'') AS SizeCode"
                                    + " , CASE WHEN ISNULL(IC.Allocation,'') = 'Allocation By Order'"
                                        + " THEN CASE WHEN (ID.OnHand + ID.InTransit + ID.OnOrder - ID.OnAlloc) - (JOBOM.Consumption + JOBOM.AllowanceQty) < 0"
                                            + " THEN (ID.OnHand + ID.InTransit + ID.OnOrder - ID.OnAlloc) - (JOBOM.Consumption + JOBOM.AllowanceQty) ELSE 0 END"
                                    + " WHEN ISNULL(IC.Allocation,'') = 'Allocation By Onhand'"
                                        + " THEN CASE WHEN ID.OnHand + ID.InTransit - ID.OnAlloc - (JOBOM.Consumption + JOBOM.AllowanceQty) < 0"
                                            + " THEN ID.OnHand + ID.InTransit - ID.OnAlloc - (JOBOM.Consumption + JOBOM.AllowanceQty) ELSE 0 END"
                                    + " ELSE"
                                        + " CASE WHEN ID.OnHand + ID.InTransit - (JOBOM.Consumption + JOBOM.AllowanceQty) < 0"
                                            + " THEN ID.OnHand + ID.InTransit - (JOBOM.Consumption + JOBOM.AllowanceQty) ELSE 0 END"
                                    
                                    + " END AS RequestQty"
                                    + " , I.UnitBase"
                                    + " INTO #FINAL"
                                    + " FROM Production.JobOrder JO"
                                    + " LEFT JOIN Production.JOBillOfMaterial JOBOM"
                                    + " ON JO.DocNumber = JOBOM.DocNumber"
                                    + " LEFT JOIN Production.JOStep JOS"
                                    + " ON JO.DocNumber = JOS.DocNumber"
                                    + " LEFT JOIN Masterfile.ItemDetail ID"
                                    + " ON JOBOM.ItemCode = ID.ItemCode"
                                    + " AND JOBOM.ColorCode = ID.ColorCode"
                                    + " AND JOBOM.ClassCode = ID.ClassCode"
                                    + " AND JOBOM.SizeCode = ID.SizeCode"
                                    + " LEFT JOIN Masterfile.Item I"
                                    + " ON ID.ItemCode = I.ItemCode"
                                    + " LEFT JOIN Masterfile.ItemCategory IC"
                                    + " ON I.ItemCategoryCode = IC.ItemCategoryCode"
                                    + " WHERE JOBOM.DocNumber = '" + glJONumber.Value + "' and Isnull(IsItemSupp,0)!=1 "
                                    //+ " GROUP BY JO.DocNumber,JOBOM.ItemCode,JOBOM.ColorCode,JOBOM.ClassCode,JOBOM.SizeCode,I.ItemCategoryCode,IC.Allocation,I.UnitBase"
                                    + " SELECT ItemCode, ColorCode, ClassCode, SizeCode, SUM(RequestQty) AS RequestQty, UnitBase "
	                                + " INTO #FINALFINAL"
	                                + " FROM #FINAL "
	                                + " WHERE RequestQty !=0"
                                    + " GROUP BY ItemCode, ColorCode, ClassCode, SizeCode, UnitBase"

                                    + " SELECT RIGHT('00000'+ CAST(ROW_NUMBER() OVER (ORDER BY ItemCode) AS VARCHAR(5)),5) AS LineNumber,* FROM #FINALFINAL"
	                                + " WHERE RequestQty !=0";


            gvJODetails.DataSource = JODetailsds;
            gvJODetails.DataBind();
            Session["Datatable"] = "1";
            Session["preqsqljo"] = JODetailsds.SelectCommand;

            foreach (GridViewColumn col in gvJODetails.VisibleColumns)
            {
                GridViewDataColumn dataColumn = col as GridViewDataColumn;
                if (dataColumn == null) continue;
                dt.Columns.Add(dataColumn.FieldName);
            }
            for (int i = 0; i < gvJODetails.VisibleRowCount; i++)
            {
                DataRow row = dt.Rows.Add();
                foreach (DataColumn col in dt.Columns)
                    row[col.ColumnName] = gvJODetails.GetRowValues(i, col.ColumnName);
            }

            dt.PrimaryKey = new DataColumn[] { //Sets datatable's primary key for batchupdate reference during adding.
            //dt.Columns["DocNumber"],dt.Columns["LineNumber"]};
            dt.Columns["LineNumber"], dt.Columns["ItemCode"], dt.Columns["ColorCode"], dt.Columns["ClassCode"], dt.Columns["SizeCode"], dt.Columns["RequestQty"], dt.Columns["UnitBase"]};

            //btnMultiplePR.ClientEnabled = true;
            return dt;
        }


        protected void grid_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        { //Validation for gridview, set all validate conditions here.. (sample here is checking of empty cells)
            //string ItemCode = "";
            //string ColorCode = "";
            //string ClassCode = "";
            //string SizeCode = "";
            //foreach (GridViewColumn column in gv1.Columns)
            //{
            //    GridViewDataColumn dataColumn = column as GridViewDataColumn;
            //    if (dataColumn == null) continue;
            //    if (e.NewValues[dataColumn.FieldName] == null && dataColumn.FieldName != "LineNumber" && dataColumn.FieldName != "DocNumber" && dataColumn.FieldName != "Type"
            //        && dataColumn.FieldName != "IncomingDocType" && dataColumn.FieldName != "Remarks" && dataColumn.FieldName != "OrderQty"
            //        && dataColumn.FieldName != "Field1" && dataColumn.FieldName != "Field2" && dataColumn.FieldName != "Field3" && dataColumn.FieldName != "Field4"
            //        && dataColumn.FieldName != "Field5" && dataColumn.FieldName != "Field6" && dataColumn.FieldName != "Field7" && dataColumn.FieldName != "Field8"
            //        && dataColumn.FieldName != "Field9" && dataColumn.FieldName != "IsAllowPartial")
            //    {
            //        e.Errors[dataColumn] = "Value can't be null.";//Sets error tooltip message
            //    }
            //    //Checking for non existing Codes..

            //    ItemCode = String.IsNullOrEmpty(Convert.ToString(e.NewValues["ItemCode"])) ? "" : e.NewValues["ItemCode"].ToString();
            //    ColorCode = String.IsNullOrEmpty(Convert.ToString(e.NewValues["ColorCode"])) ? "" : e.NewValues["ColorCode"].ToString(); 
            //    ClassCode = String.IsNullOrEmpty(Convert.ToString(e.NewValues["ClassCode"])) ? "" : e.NewValues["ClassCode"].ToString(); 
            //    SizeCode = String.IsNullOrEmpty(Convert.ToString(e.NewValues["SizeCode"])) ? "" : e.NewValues["SizeCode"].ToString();

            //    DataTable item = Gears.RetriveData2("SELECT ItemCode FROM Masterfile.[ItemDetail] WHERE ItemCode = '" + ItemCode + "'", Session["ConnString"].ToString());//ADD CONN
            //    if (item.Rows.Count < 1)
            //    {
            //        AddError(e.Errors, gv1.Columns["ItemCode"], "ItemCode doesn't exist!");
            //    }
            //    DataTable color = Gears.RetriveData2("SELECT ColorCode FROM Masterfile.[ItemDetail] WHERE ColorCode = '" + ColorCode + "'", Session["ConnString"].ToString());//ADD CONN
            //    if (color.Rows.Count < 1)
            //    {
            //        AddError(e.Errors, gv1.Columns["ColorCode"], "ColorCode doesn't exist!");
            //    }
            //    DataTable clss = Gears.RetriveData2("SELECT ClassCode FROM Masterfile.[ItemDetail] WHERE ClassCode = '" + ClassCode + "'", Session["ConnString"].ToString());//ADD CONN;
            //    if (clss.Rows.Count < 1)
            //    {
            //        AddError(e.Errors, gv1.Columns["ClassCode"], "ClassCode doesn't exist!");
            //    }
            //    DataTable size = Gears.RetriveData2("SELECT SizeCode FROM Masterfile.[ItemDetail] WHERE SizeCode = '" + SizeCode + "'", Session["ConnString"].ToString());//ADD CONN
            //    if (size.Rows.Count < 1)
            //    {
            //        AddError(e.Errors, gv1.Columns["SizeCode"], "SizeCode doesn't exist!");
            //    }
            //}

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
        }
        #endregion
        protected void SetStatus()
        {
            if (_Entity.Status == "P")
                txtStatus.Value = "Partial";
            else if (_Entity.Status == "C")
                txtStatus.Value = "Closed";
            else if (_Entity.Status == "L")
                txtStatus.Value = "Cancelled";
            else if (_Entity.Status == "X")
                txtStatus.Value = "Manual Closed";
            else if (_Entity.Status == "A")
                txtStatus.Value = "Partial Closed";
            else
            {
                txtStatus.Value = "New";
            }
        }
        protected void SaveStatus()
        {
            if (txtStatus.Text == "Partial")
                _Entity.Status = "P";
            else if (txtStatus.Text == "Closed")
                _Entity.Status = "C";
            else if (txtStatus.Text == "Cancelled")
                _Entity.Status = "L";
            else if (txtStatus.Text == "Manual Closed")
                _Entity.Status = "X";
            else if (txtStatus.Text == "Partial Closed")
                _Entity.Status = "A";
            else
                _Entity.Status = "N";

        }
        protected void SetText()
        {
            DataTable CostCenterCode = Gears.RetriveData2("SELECT CostCenterCode FROM Masterfile.[BPCustomerInfo] WHERE BizPartnerCode = '" + glCustomerCode.Text + "'", Session["ConnString"].ToString());//ADD CONN
            foreach (DataRow dt in CostCenterCode.Rows)
            {
                glCostCenter.Value = dt[0].ToString();
            }
        }

        // Date Validation      LGE - 02 - 15 - 2016
        protected void DateValidation()
        {
            DateTime DocDate = Convert.ToDateTime(dtDocDate.Value);
            //string DocDate = DocDateTime.ToShortDateString();
            DateTime TargetDate = Convert.ToDateTime(dtTargetDate.Value);
            //string TargetDate = TargetDateTime.ToShortDateString();
            //int result = DateTime.Compare(DocDateTime.Date, TargetDateTime.Date);

            if(TargetDate.Date < DocDate.Date)
            {
                dateValidation = true;
                error = true;
            }
        }
        // End of Date Validation

        protected void Connection_Init(object sender, EventArgs e)
        {
            //Session["userid"] = "1828";
            //Session["ConnString"] = "Data Source=192.168.201.115;Initial Catalog=GEARS-METSIT;Persist Security Info=True;User ID=sa;Password=mets123*;connection timeout=1800;";
            //sdsDetail.ConnectionString = Session["ConnString"].ToString();
            //Masterfileitem.ConnectionString = Session["ConnString"].ToString();
            //Masterfileitemdetail.ConnectionString = Session["ConnString"].ToString();
            //ReceivingWarehouselookup.ConnectionString = Session["ConnString"].ToString();
            //CustomerCodelookup.ConnectionString = Session["ConnString"].ToString();
            //CostCenterlookup.ConnectionString = Session["ConnString"].ToString();

            ((SqlDataSource)sender).ConnectionString = Session["ConnString"].ToString();
        }

        protected void glItemCode_Init(object sender, EventArgs e)
        {
            ASPxGridLookup gridLookup = sender as ASPxGridLookup;
            if (IsCallback && Request.Params["__CALLBACKID"].Contains("glItemCode")
                && Request.Params["__CALLBACKID"].Contains(gridLookup.ID))
            {
                //gridLookup.GridView.CustomCallback += new ASPxGridViewCustomCallbackEventHandler(item_CustomCallback);
                Masterfileitem.SelectCommand = "SELECT [ItemCode], [FullDesc], [ShortDesc] FROM Masterfile.Item I INNER JOIN Masterfile.ItemCategory IC ON I.ItemCategoryCode = IC.ItemCategoryCode WHERE ISNULL(I.IsInactive,'')=0 AND ISNULL(IsCore,0)=0 AND (ISNULL(IsAsset,0) = 1 OR ISNULL(IsService,0) = 1 OR ISNULL(IsStock,0) = 1)";
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

                gridLookup.GridView.JSProperties["cp_identifier"] = "ItemCode";
                gridLookup.GridView.JSProperties["cp_codes"] = codes;
                gridLookup.GridView.JSProperties["cp_valch"] = true;
            }
        }
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
        protected void InitControls()
        {
            foreach (var item in frmlayout1.Items)
            {
                if (item is LayoutGroupBase)
                    (item as LayoutGroupBase).ForEach(GetNestedControls);
            }
        }
        protected void GetNestedControls(LayoutItemBase item)
        {
            if (item is LayoutItem)
                SetViewState(item as LayoutItem);
        }
        protected void SetViewState(LayoutItem c)
        {
            if (c.ParentGroup.Caption != "Audit Trail")
            foreach (Control control in c.Controls)
            {
                ASPxEdit editor = control as ASPxEdit;
                if (editor != null)
                {
                    if (editor.GetType().ToString() == "DevExpress.Web.ASPxTextBox")
                    {
                        TextboxLoad(editor);
                    }
                    if (editor.GetType().ToString() == "DevExpress.Web.ASPxGridLookup")
                    {
                        LookupLoad(editor);
                    }
                    if (editor.GetType().ToString() == "DevExpress.Web.ASPxDateEdit")
                    {
                        Date_Load(editor);
                    }
                    if (editor.GetType().ToString() == "DevExpress.Web.ASPxMemo")
                    {
                        MemoLoad(editor);
                    }
                }
            }
        }
        protected void glStockNumber_Init(object sender, EventArgs e)
        {
            ASPxGridLookup gridLookup = sender as ASPxGridLookup;
            if (Request.Params["__CALLBACKID"] != null)
                if (Request.Params["__CALLBACKID"].Contains(gridLookup.ID))
                {
                    StockNumberLookup.SelectCommand = "SELECT ItemCode, FullDesc, ShortDesc FROM Masterfile.Item WHERE ISNULL(IsInactive,0)=0";
                    gridLookup.DataBind();
                }
        }

        protected void gvJODetails_Init(object sender, EventArgs e)
        {
            if (IsCallback)
            if (Request.Params["__CALLBACKID"].Contains("gvJODetails") && Session["preqsqljo"] != null)
            {
                JODetailsds.SelectCommand = Session["preqsqljo"].ToString();
                gvJODetails.DataSource = JODetailsds;
                gvJODetails.DataBind();
            }
        }

        protected void gvService_InitNewRow(object sender, ASPxDataInitNewRowEventArgs e)
        {
            e.NewValues["IsAllowProgressBilling"] = false;
        }

    }
}