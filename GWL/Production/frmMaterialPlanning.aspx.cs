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
using GearsAccounting;

namespace GWL
{
    public partial class frmMaterialPlanning : System.Web.UI.Page
    {
        Boolean error = false;//Boolean for grid validation
        Boolean view = false;//Boolean for view state
        Boolean check = false;//Boolean for grid validation
        Boolean edit = false; //Bolean for edit state

        private static string strError;

        Entity.MaterialPlanning _Entity = new MaterialPlanning();//Calls entity ICN
        Entity.MaterialPlanning.MaterialPlanningDetail _EntityDetail = new MaterialPlanning.MaterialPlanningDetail();//Call entity POdetails

        #region page load/entry
        protected void Page_Load(object sender, EventArgs e)
        {
            string referer;
            try //Validation to restrict user to browse/type directly to browser's address
            {
                referer = Request.ServerVariables["http_referer"];
            }
            catch
            {
                referer = "";
            }

            if (referer == null && Common.Common.SystemSetting("URLCHECK", Session["ConnString"].ToString()) != "NO")
            {
                Response.Redirect("~/error.aspx");
            }

            gv1.KeyFieldName = "LineNumber;ItemCode;ColorCode;ClassCode;SizeCode;NeededQty";
            gvdetailgrid.KeyFieldName = "Supplier;KeySupplier;ForOrder;PendingOrder;DeliveryDate";
            gvMultiplePR.KeyFieldName = "LineNumber;ItemCode;ColorCode;ClassCode;SizeCode;NeededQty";

            //Details checking. changes datasourceid of gridview if there's no details for a certain docnumber; to enable adding of details.  
            if (!IsPostBack)
            {

                cp.JSProperties["cp_clicked"] = "JO";

                //txtDocnumber.Value = Session["DocNumber"].ToString(); //sets docnumber from session
                txtDocnumber.ReadOnly = true;
                txtDocnumber.Value = Request.QueryString["docnumber"].ToString();
                
                if (Request.QueryString["entry"].ToString() == "N")
                {

                    //gv1.DataSourceID = "sdsDetail";
                    popup.ShowOnPageLoad = false;
                    dtDocDate.Text = DateTime.Now.ToShortDateString();
                    //deExpDelDate.Text = DateTime.Now.ToShortDateString();
                }
                //else
                //{
                    // gv1.DataSourceID = "odsDetail";
                _Entity.getdata(txtDocnumber.Text, Session["ConnString"].ToString());//ADD CONN


                    // Moved inside !IsPostBack   LGE 03 - 02 - 2016
                    // V=View, E=Edit, N=New
                    switch (Request.QueryString["entry"].ToString())
                    {
                        case "N":
                            if (string.IsNullOrEmpty(_Entity.LastEditedBy))
                            {
                                updateBtn.Text = "Save";
                            }
                            else
                            {
                                updateBtn.Text = "Update";
                            }
                            break;
                        case "E":
                            updateBtn.Text = "Update";
                            edit = true;
                            break;
                        case "V":
                            view = true;//sets view mode for entry
                            txtDocnumber.Enabled = true;
                            txtDocnumber.ReadOnly = false;
                            updateBtn.Text = "Close";
                            glcheck.ClientVisible = false;
                            break;
                        case "D":
                            view = true;
                            updateBtn.Text = "Delete";
                            break;
                    }
                    //--------------------------------------------------
                    //--------------------------------------------------

                    //txtInvoiceDocNumber.Value = _Entity.InvoiceDocNumber;
                    dtDocDate.Value = String.IsNullOrEmpty(_Entity.DocDate) ? DateTime.Now : Convert.ToDateTime(_Entity.DocDate);
                    glJobOrder.Value = _Entity.JONumber;
                    CustomerLookup.SelectCommand = "select distinct BizPartnerCode,Name From Production.JobOrder a " +
                                             "inner join Masterfile.BPCustomerInfo b " +
                                             "on a.CustomerCode = b.BizPartnerCode where a.DocNumber = '" + glJobOrder.Text + "'";
                    glCustomer.Value = _Entity.CustomerCode;
                    //dtDueDateFrom.Value = String.IsNullOrEmpty(_Entity.DueDateFrom) ? DateTime.Now : Convert.ToDateTime(_Entity.DueDateFrom);
                    //dtDueDateTo.Value = String.IsNullOrEmpty(_Entity.DueDateTo) ? DateTime.Now : Convert.ToDateTime(_Entity.DueDateTo);

                    //String.IsNullOrEmpty(_Entity.DocDate.ToString()) ? null : Convert.ToDateTime(_Entity.DocDate.ToString()).ToShortDateString();
                    //dtDueDateFrom.Value = Convert.ToDateTime(_Entity.DueDateFrom);
                    dtDueDateFrom.Value = String.IsNullOrEmpty(_Entity.DueDateFrom.ToString()) ? null : Convert.ToDateTime(_Entity.DueDateFrom.ToString()).ToShortDateString();
                    //dtDueDateTo.Value = Convert.ToDateTime(_Entity.DueDateTo);
                    dtDueDateTo.Value = String.IsNullOrEmpty(_Entity.DueDateTo.ToString()) ? null : Convert.ToDateTime(_Entity.DueDateTo.ToString()).ToShortDateString();

                    //dtDocDate.Value = String.IsNullOrEmpty(_Entity.DocDate) ? DateTime.Now : Convert.ToDateTime(_Entity.DocDate);
                    //dtTargetDate.Value = String.IsNullOrEmpty(_Entity.TargetDate) ? DateTime.Now : Convert.ToDateTime(_Entity.TargetDate);


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
                    //txtHSubmittedBy.Text = _Entity.SubmittedBy;
                    //txtHSubmittedDate.Text = _Entity.SubmittedDate;
                    //txtCancelledBy.Text = _Entity.CancelledBy;
                    //txtCancelledDate.Text = _Entity.CancelledDate;
                    //txtPostedBy.Text = _Entity.PostedBy;
                    //txtPostedDate.Text = _Entity.PostedDate;

                    
                    btnMultiplePR.ClientEnabled = false;

                //}
                //if (Request.QueryString["IsWithDetail"].ToString() == "false" && Request.QueryString["entry"].ToString() != "V")
                //{
                //    gv1.DataSourceID = "sdsDetail";

                //    sdsDetail.SelectCommand = "SELECT * FROM Accounting.AssetDisposaldetail WHERE DocNumber is null";
                //    sdsDetail.SelectParameters.Remove(sdsDetail.SelectParameters["DocNumber"]);
                //}

                    //DataTable dtblDetail = Gears.RetriveData2("Select DocNumber FROM Accounting.AssetDisposalDetail WHERE DocNumber = '" + txtDocnumber.Text + "'", Session["ConnString"].ToString());//ADD CONN
                    //gv1.DataSourceID = (dtblDetail.Rows.Count > 0 ? "odsDetail" : "sdsDetail");

                    GetSelectedVal();
                    Freeze();

                    //gvRef.DataSourceID = "odsReference";
                    //this.gvRef.Columns["CommandString"].Width = 0;
                    //this.gvRef.Columns["RCommandString"].Width = 0;
            }
            if (glJobOrder.Text != "")
            {
                CustomerLookup.SelectCommand = "select distinct BizPartnerCode,Name From Production.JobOrder a " +
                                             "inner join Masterfile.BPCustomerInfo b " +
                                             "on a.CustomerCode = b.BizPartnerCode where a.DocNumber = '" + glJobOrder.Text + "'";
            }
            else
            {
                CustomerLookup.SelectCommand = "select distinct BizPartnerCode,Name From Production.JobOrder a " +
                                             "inner join Masterfile.BPCustomerInfo b " +
                                             "on a.CustomerCode = b.BizPartnerCode";
            }

            if (glCustomer.Text != "") {
                JobOrderLookup.SelectCommand = "select DocNumber from Production.JobOrder WHERE Status ='N' and CustomerCode = '" + glCustomer.Text + "'";
            }
            else
            {
                JobOrderLookup.SelectCommand = "select DocNumber from Production.JobOrder WHERE Status ='N'";
            }

            //if (Request.Params["__CALLBACKID"].Contains(grid.ID))
            glJobOrder.DataBind();
            glCustomer.DataBind();

            if (Session["MatPlanDatatable"] != null)
            {
                gv1.DataSource = Session["MatPlanDatatable"];
                gv1.DataBind();
            }
         
        }
        #endregion




        #region Validation
        private void Validate()
        {
            //GearsLibrary.Gears.GearsParameter gparam = new Gears.GearsParameter();
            //gparam._Connection = Session["ConnString"].ToString();
            //gparam._DocNo = _Entity.DocNumber;
            //gparam._UserId = Session["Userid"].ToString();
            //gparam._TransType = "ACTADI";

            //string strresult = GearsAccounting.GAccounting.AssetDisposal_Validate(gparam);

            //cp.JSProperties["cp_valmsg"] = strresult;//Message variable to client side
            
        }
        #endregion

        #region Post
        private void Post()
        {
            GearsLibrary.Gears.GearsParameter gparam = new Gears.GearsParameter();
            gparam._Connection = Session["ConnString"].ToString();
            gparam._DocNo = _Entity.DocNumber;
            gparam._UserId = Session["Userid"].ToString();
            gparam._TransType = "ACTADI";
            gparam._Table = "Accounting.AssetDisposal";
            gparam._Factor = -1;
            string strresult = GearsAccounting.GAccounting.AssetDisposal_Post(gparam);
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
            //else
            //{
            //    ASPxGridView grid = sender as ASPxGridView;
            //    grid.SettingsBehavior.AllowGroup = false;
            //    grid.SettingsBehavior.AllowSort = false;
            //    e.Editor.ReadOnly = false;
            ////}
            //if (Request.QueryString["entry"].ToString() == "V")
            //{
            //    ASPxGridView grid = sender as ASPxGridView;
            //    grid.SettingsBehavior.AllowGroup = false;
            //    grid.SettingsBehavior.AllowSort = false;
            //    e.Editor.ReadOnly = true;
            //}
        }

        protected void Generate_Btn(object sender, EventArgs e)//Control for all textbox
        {
            ASPxButton btn = sender as ASPxButton;
            btn.ClientVisible = !view;
        }
        protected void Date_Load(object sender, EventArgs e)//Control for all date editor
        {
            ASPxDateEdit date = sender as ASPxDateEdit;
            date.DropDownButton.Enabled = !view;
            date.ReadOnly = view;
        }
        protected void ComboBoxLoad(object sender, EventArgs e)//Control for all lookup in header
        {

            ASPxComboBox combobox = sender as ASPxComboBox;
            combobox.DropDownButton.Enabled = !view;
            combobox.Enabled = !view;
        }

        protected void dtpDocDate_Init(object sender, EventArgs e)
        {
            if (Request.QueryString["entry"] == "N")
            {
                dtDocDate.Date = DateTime.Now;
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
                if (Request.QueryString["entry"].ToString() == "N" || Request.QueryString["entry"].ToString() == "E")
                {
                    
                            if (e.ButtonType == ColumnCommandButtonType.New)
                            {
                                e.Visible = true;
                            }
                   
                }
                if (Request.QueryString["entry"].ToString() == "V" || Request.QueryString["entry"].ToString() == "D")
                {
                    if (e.ButtonType == ColumnCommandButtonType.Edit || e.ButtonType == ColumnCommandButtonType.Delete ||
                        e.ButtonType == ColumnCommandButtonType.New || e.ButtonType == ColumnCommandButtonType.Cancel ||
                        e.ButtonType == ColumnCommandButtonType.Update ||
                        e.ButtonType == ColumnCommandButtonType.SelectCheckbox)
                        e.Visible = false;
                }
                if (e.ButtonType == ColumnCommandButtonType.Update)
                    e.Visible = false;
                if (e.ButtonType == ColumnCommandButtonType.Cancel)
                    e.Visible = false;
            
        }

        protected void Freeze()
        {
           
            if(gv1.Columns["JorOrder"].Width.ToString() == "0")
            {
                int consfreeze = 0;
                foreach (GridViewColumn column in gv1.VisibleColumns)
                {
                    if (column is GridViewColumn)
                    {
                        GridViewColumn dataColumn = (GridViewColumn)column;
                        if (dataColumn.Visible)
                            dataColumn.FixedStyle = GridViewColumnFixedStyle.Left;
                        consfreeze++;
                    }
                    if (consfreeze == 8)
                        break;
                }
            }
            else
            {
                int consfreeze = 0;
                foreach (GridViewColumn column in gv1.VisibleColumns)
                {
                    if (column is GridViewColumn)
                    {
                        GridViewColumn dataColumn = (GridViewColumn)column;
                        if (dataColumn.Visible)
                            dataColumn.FixedStyle = GridViewColumnFixedStyle.Left;
                        consfreeze++;
                    }
                    if (consfreeze == 5)
                        break;
                }
            }



            
        }

        private DataTable GetSelectedVal()
        {

            gv1.DataSourceID = null;
            string SQL = null;

            DataTable dt = new DataTable();

            if (String.IsNullOrEmpty(glJobOrder.Text) && String.IsNullOrEmpty(dtDueDateFrom.Text)
                && String.IsNullOrEmpty(dtDueDateTo.Text) && String.IsNullOrEmpty(glCustomer.Text))
            {
                SQL = "WHERE JO.DocNumber = '" + glJobOrder.Text + "'";
            }
            else
            {
                SQL = "WHERE Status ='N'";
                if (!String.IsNullOrEmpty(glJobOrder.Text))
                    SQL += " AND JO.DocNumber = '" + glJobOrder.Text + "'";
                if (!String.IsNullOrEmpty(dtDueDateFrom.Text))
                    SQL += " AND JO.DueDate >= '" + dtDueDateFrom.Text + "'";
                if (!String.IsNullOrEmpty(dtDueDateTo.Text))
                    SQL += " AND JO.DueDate <= '" + dtDueDateTo.Text + "'";
                if (!String.IsNullOrEmpty(glCustomer.Text))
                    SQL += " AND JO.CustomerCode = '" + glCustomer.Text + "'";
            }
            //{
            //    //if (!String.IsNullOrEmpty(SQL))
            //        SQL = SQL + " AND JO.CustomerCode = '" + glCustomer.Text + "'";
            //    //else
            //        //SQL = SQL + "WHERE JO.CustomerCode = '" + glCustomer.Text + "'";
            //}

            DataTable getDetail = Gears.RetriveData2("select RIGHT('00000'+ CAST(ROW_NUMBER() OVER (ORDER BY JO.DocNumber) AS VARCHAR(5)),5) AS LineNumber"
                    + " ,JO.DocNumber AS JobOrder "
                    + " , JOBOM.ProductCode AS StockNumber"
                    + " , CAST(JO.DueDate AS VARCHAR(10)) AS JODueDate"
                    + " , ISNULL(JOBOM.StepCode,'') AS Step"
                    + " , ISNULL(JOBOM.ItemCode,'') AS ItemCode"
                    + " , ISNULL(JOBOM.ColorCode,'') AS ColorCode"
                    + " , ISNULL(JOBOM.ClassCode,'') AS ClassCode"
                    + " , ISNULL(JOBOM.SizeCode,'') AS SizeCode"
                    + " , SUM(ISNULL(JOBOM.Consumption,0) + ISNULL(JOBOM.AllowanceQty,0)) AS RequiredQty"
                //+ " , SUM(((ISNULL(ID.OnHand,0) + ISNULL(ID.OnOrder,0)) - ISNULL(ID.OnAlloc,0))) AS UnallocatedQty"
                    + " , CASE WHEN ISNULL(IC.Allocation,'') = 'Allocation By Order'"
                    + " THEN SUM(((ISNULL(ID.OnHand,0) + ISNULL(ID.OnOrder,0)) - ISNULL(ID.OnAlloc,0)))"
                    + " WHEN ISNULL(IC.Allocation,'') = 'Allocation By Onhand'"
                    + " THEN SUM((ISNULL(ID.OnHand,0) - ISNULL(ID.OnAlloc,0)))"
                    + " ELSE SUM(ISNULL(ID.OnHand,0)) END AS UnallocatedQty"
                //+ " , CASE WHEN SUM((ISNULL(JOBOM.Consumption,0) + ISNULL(JOBOM.AllowanceQty,0)) - ((ISNULL(ID.OnHand,0) + ISNULL(ID.OnOrder,0)) - ISNULL(ID.OnAlloc,0))) < 0 THEN 0"
                //+ "     ELSE SUM((ISNULL(JOBOM.Consumption,0) + ISNULL(JOBOM.AllowanceQty,0)) - ((ISNULL(ID.OnHand,0) + ISNULL(ID.OnOrder,0)) - ISNULL(ID.OnAlloc,0))) END AS NeededQty"
                    + " , SUM(ISNULL(ID.OnHand,0)) AS OnhandQty"
                    + " , SUM(ISNULL(ID.OnOrder,0)) AS OrderQty"
                    + " , SUM(ISNULL(ID.OnAlloc,0)) AS AllocatedQty"
                    + " , '' AS ETC"
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
                    + " " + SQL
                    + " GROUP BY JO.DocNumber,JOBOM.ProductCode,JO.DueDate,JOBOM.StepCode,JOBOM.ItemCode,JOBOM.ColorCode,JOBOM.ClassCode,JOBOM.SizeCode,IC.Allocation"
                    + " SELECT *, CASE WHEN RequiredQty - UnallocatedQty > 0 THEN RequiredQty - UnallocatedQty ELSE 0 END AS NeededQty FROM #FINAL", Session["ConnString"].ToString());




            //sdsPicklistDetail.FilterExpression = (GroupOperator.Combine(GroupOperatorType.And, selectionCriteria)).ToString();
            //Session["purchaserequestdetail"] = sdsPicklistDetail.FilterExpression;
            gv1.DataSource = getDetail;
            gv1.DataBind();
            Session["MatPlanDatatable"] = getDetail;

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
            //dt.Columns["DocNumber"],dt.Columns["LineNumber"]};
            dt.Columns["LineNumber"], dt.Columns["ItemCode"], dt.Columns["ColorCode"], dt.Columns["ClassCode"], dt.Columns["SizeCode"], dt.Columns["NeededQty"]};

            
            return dt;
        }

        private DataTable GenerateMultiplePR()
        {

            gvMultiplePR.DataSourceID = null;
           // string SQL = null;

            DataTable dt = new DataTable();

            MultiplePRsds.SelectCommand = "select RIGHT('00000'+ CAST(ROW_NUMBER() OVER (ORDER BY JO.DocNumber) AS VARCHAR(5)),5) AS LineNumber"
                    + " , ISNULL(JOBOM.ItemCode,'') AS ItemCode"
                    + " , ISNULL(JOBOM.ColorCode,'') AS ColorCode"
                    + " , ISNULL(JOBOM.ClassCode,'') AS ClassCode"
                    + " , ISNULL(JOBOM.SizeCode,'') AS SizeCode"
                    + " , SUM(ISNULL(JOBOM.Consumption,0) + ISNULL(JOBOM.AllowanceQty,0)) AS RequiredQty "
                    + " , CASE WHEN ISNULL(IC.Allocation,'') = 'Allocation By Order' "
	                + " THEN SUM(((ISNULL(ID.OnHand,0) + ISNULL(ID.OnOrder,0)) - ISNULL(ID.OnAlloc,0))) "
	                + " WHEN ISNULL(IC.Allocation,'') = 'Allocation By Onhand' "
	                + " THEN SUM((ISNULL(ID.OnHand,0) - ISNULL(ID.OnAlloc,0))) "
                    + " ELSE SUM(ISNULL(ID.OnHand,0)) END AS UnallocatedQty"
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
                    + " LEFT JOIN Masterfile.Item I "
                    + " ON ID.ItemCode = I.ItemCode "
                    + " LEFT JOIN Masterfile.ItemCategory IC "
                    + " ON I.ItemCategoryCode = IC.ItemCategoryCode "
                    + " WHERE JOBOM.DocNumber = '" + glJobOrder.Text + "' GROUP BY JO.DocNumber,JOBOM.ItemCode,JOBOM.ColorCode,JOBOM.ClassCode,JOBOM.SizeCode,IC.Allocation"
                    + " SELECT *, CASE WHEN RequiredQty - UnallocatedQty > 0 THEN RequiredQty - UnallocatedQty ELSE 0 END AS NeededQty "
                    + " FROM #FINAL WHERE CASE WHEN RequiredQty - UnallocatedQty > 0 THEN RequiredQty - UnallocatedQty ELSE 0 END != 0";
                    



            gvMultiplePR.DataSource = MultiplePRsds;
            gvMultiplePR.DataBind();

            foreach (GridViewColumn col in gvMultiplePR.VisibleColumns)
            {
                GridViewDataColumn dataColumn = col as GridViewDataColumn;
                if (dataColumn == null) continue;
                dt.Columns.Add(dataColumn.FieldName);
            }
            for (int i = 0; i < gvMultiplePR.VisibleRowCount; i++)
            {
                DataRow row = dt.Rows.Add();
                foreach (DataColumn col in dt.Columns)
                    row[col.ColumnName] = gvMultiplePR.GetRowValues(i, col.ColumnName);
            }

            dt.PrimaryKey = new DataColumn[] { //Sets datatable's primary key for batchupdate reference during adding.
            //dt.Columns["DocNumber"],dt.Columns["LineNumber"]};
            dt.Columns["LineNumber"], dt.Columns["ItemCode"], dt.Columns["ColorCode"], dt.Columns["ClassCode"], dt.Columns["SizeCode"], dt.Columns["NeededQty"]};

            btnMultiplePR.ClientEnabled = true;
            return dt;
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

            if (Request.QueryString["entry"].ToString() == "V" || Request.QueryString["entry"].ToString() == "D")
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
                gridLookup.GridView.DataSourceID = "AssetAcquisitionLookup";
                AssetAcquisitionLookup.FilterExpression = Session["FilterExpression"].ToString();
                //Session["FilterExpression"] = null;
            }
            else
            {
                gridLookup.GridView.DataSourceID = "AssetAcquisitionLookup";
            }
        }
        public void gridView_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            string column = e.Parameters.Split('|')[0];//Set column name
            if (column.Contains("GLP_AIC") || column.Contains("GLP_AC") || column.Contains("GLP_F")) return;//Traps the callback
            string propertynumber = e.Parameters.Split('|')[1];//Set Item Code
            string val = e.Parameters.Split('|')[2];//Set column value
            if (val.Contains("GLP_AIC") || val.Contains("GLP_AC") || val.Contains("GLP_F")) return;//Traps the callback
            var propertynumberlookup = sender as ASPxGridView;
            string codes = "";

            if (e.Parameters.Contains("PropertyNumber"))
            {
                DataTable countcolor = Gears.RetriveData2("Select DISTINCT ColorCode,ClassCode,SizeCode,ItemCode,Qty,UnitCost,AccumulatedDepreciation,Status from Accounting.AssetInv where propertynumber = '" + propertynumber + "'", Session["ConnString"].ToString());//ADD CONN
                
                    foreach (DataRow dt in countcolor.Rows)
                    {
                        codes = dt["ColorCode"].ToString() + ";";
                        codes += dt["ClassCode"].ToString() + ";";
                        codes += dt["SizeCode"].ToString() + ";";
                        codes += dt["ItemCode"].ToString() + ";";
                        codes += dt["Qty"].ToString() + ";";
                        codes += dt["UnitCost"].ToString() + ";";
                        codes += dt["AccumulatedDepreciation"].ToString() + ";";
                        codes += dt["Status"].ToString() + ";";

                    }


                    propertynumberlookup.JSProperties["cp_identifier"] = "ItemCode";
                    propertynumberlookup.JSProperties["cp_codes"] = codes;
            }

            else if (e.Parameters.Contains("CheckPNumber"))
            {
                ASPxGridView grid = sender as ASPxGridView;
                string[] transdoc = propertynumber.Split(';');
                var selectedValues = transdoc;
                CriteriaOperator selectionCriteria = new InOperator("PropertyNumber", transdoc);
                CriteriaOperator not = new NotOperator(selectionCriteria);
                PropertyNumberLookup.FilterExpression = (GroupOperator.Combine(GroupOperatorType.And, not)).ToString();
                Session["FilterExpression"] = PropertyNumberLookup.FilterExpression;
                grid.DataSourceID = "PropertyNumberLookup";
                grid.DataBind();

                for (int i = 0; i < grid.VisibleRowCount; i++)
                {
                    if (grid.GetRowValues(i, "PropertyNumber") != null)
                        if (grid.GetRowValues(i, "PropertyNumber").ToString() == val)
                        {
                            grid.Selection.SelectRow(i);
                            string key = grid.GetRowValues(i, "PropertyNumber").ToString();
                            grid.MakeRowVisible(key);
                            break;
                        }
                }
            }


            else if (e.Parameters.Contains("VATCode"))
            {
                DataTable vat = Gears.RetriveData2("SELECT DISTINCT ISNULL(Rate,1) AS Rate FROM Masterfile.Tax WHERE TCode = '" + propertynumber + "'", Session["ConnString"].ToString());//ADD CONN

                if (vat.Rows.Count > 0)
                {
                    foreach (DataRow dt in vat.Rows)
                    {
                        codes = dt["Rate"].ToString();
                    }
                }

                propertynumberlookup.JSProperties["cp_identifier"] = "VAT";
                propertynumberlookup.JSProperties["cp_codes"] = Convert.ToDecimal(codes) + ";";
            }

            


            else
            {
                ASPxGridView grid = sender as ASPxGridView;
                ASPxGridLookup lookup = (ASPxGridLookup)gv1.FindEditRowCellTemplateControl((GridViewDataColumn)gv1.Columns[1], "glPropertyNumber");
                var selectedValues = propertynumber;
                CriteriaOperator selectionCriteria = new InOperator(lookup.KeyFieldName, new string[] { propertynumber });
                AssetAcquisitionLookup.FilterExpression = (GroupOperator.Combine(GroupOperatorType.And, selectionCriteria)).ToString();
                Session["FilterExpression"] = AssetAcquisitionLookup.FilterExpression;
                grid.DataSourceID = "AssetAcquisitionLookup";
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


            _Entity.JONumber = glJobOrder.Text;
            _Entity.CustomerCode = glCustomer.Text;
            _Entity.DueDateFrom = dtDueDateFrom.Text;
            _Entity.DueDateTo = dtDueDateTo.Text;

            //_Entity.TotalAmountSold = Convert.ToDecimal(Convert.IsDBNull(txtTotalAmountSold.Value) ? 0 : txtTotalAmountSold.Value);
            //_Entity.GrossVATAmount = Convert.ToDecimal(Convert.IsDBNull(txtTotalGrossVatableAmount.Value) ? 0 : txtTotalGrossVatableAmount.Value);
            //_Entity.GrossNonVATAmount = Convert.ToDecimal(Convert.IsDBNull(txtTotalNonGrossVatableAmount.Value) ? 0 : txtTotalNonGrossVatableAmount.Value);


            //_Entity.DisposalType = cbDisposalType.Text;
            //_Entity.SoldTo = glSoldTo.Text;
            //_Entity.Remarks       = txtRemarks.Text;

            //_Entity.TotalAssetCost = Convert.ToDecimal(Convert.IsDBNull(txtTotalCostAsset.Value) ? 0 : txtTotalCostAsset.Value);
            //_Entity.TotalAccumulatedDepreciation = Convert.ToDecimal(Convert.IsDBNull(txtTotalAccumulatedDepreciationRecord.Value) ? 0 : txtTotalAccumulatedDepreciationRecord.Value);
            //_Entity.NetBookValue = Convert.ToDecimal(Convert.IsDBNull(txtNetBookValue.Value) ? 0 : txtNetBookValue.Value);
            //_Entity.TotalGainLoss = Convert.ToDecimal(Convert.IsDBNull(txtTotalGainLoss.Value) ? 0 : txtTotalGainLoss.Value);
            //_Entity.Status        = txtStatus.Text;

            //_Entity.IsPrinted     = Convert.ToBoolean(chkIsPrinted.Value);

            //_Entity.TargetDate = dtTargetDate.Text;

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
                case "Save":

                    gv1.UpdateEdit();
                    
                    if (error == false)
                    {
                        check = true;

                        _Entity.LastEditedBy = Session["userid"].ToString();
                        _Entity.UpdateData(_Entity);//Method of Updating header


                        gv1.DataSourceID = "odsDetail";
                        odsDetail.SelectParameters["DocNumber"].DefaultValue = txtDocnumber.Text; 
                        gv1.UpdateEdit();//2nd Initiation to update grid
                        Post();
                        Validate();
                        cp.JSProperties["cp_message"] = "Successfully Saved!";
                        cp.JSProperties["cp_success"] = true;
                        cp.JSProperties["cp_close"] = true;
                        Session["MatPlanDatatable"] = null;
                        Session["Refresh"] = "1";
                    }
                    else
                    {
                        cp.JSProperties["cp_message"] = "Please check all the fields!";
                        cp.JSProperties["cp_success"] = true;
                    }

                    break;
                case "Update":

                    //gv1.UpdateEdit();
                    //strError = Functions.Submitted(_Entity.DocNumber,"Accounting.AssetDisposal",1,Session["ConnString"].ToString());//NEWADD factor 1 if submit, 2 if approve
                    //    if (!string.IsNullOrEmpty(strError))
                    //    {
                    //        cp.JSProperties["cp_message"] = strError;
                    //        cp.JSProperties["cp_success"] = true;
                    //        cp.JSProperties["cp_forceclose"] = true;
                    //        return;
                    //    }
                    if (error == false)
                    {
                        check = true;

                        _Entity.LastEditedBy = Session["userid"].ToString();
                        _Entity.UpdateData(_Entity);//Method of Updating header


                        //gv1.DataSourceID = "odsDetail";
                        //odsDetail.SelectParameters["DocNumber"].DefaultValue = txtDocnumber.Text; 
                        //gv1.UpdateEdit();//2nd Initiation to update grid
                        //Post();
                        //Validate();
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

                //case "getvat":
                //    GetVat();
                //    break;

                case "joclickedcase":
                    gv1.Columns["JorOrder"].Width = 150;
                    gv1.Columns["StockNumber"].Width = 150;
                    //gv1.Columns["JODueDate"].Width = 150;
                    gv1.Columns["JODueDate"].Visible = true;
                    gv1.Columns["Step"].Width = 150;

                    cp.JSProperties["cp_clicked"] = "JO";

                    btnMultiplePR.ClientEnabled = true;
                    Freeze();
                    break;

                case "consolidatedclickedcase":
                    gv1.Columns["LineNumber"].Width = 0;
                    gv1.Columns["JorOrder"].Width = 0;
                    gv1.Columns["StockNumber"].Width = 0;
                    //gv1.Columns["JODueDate"].Width = 0;
                    gv1.Columns["JODueDate"].Visible = false;
                    gv1.Columns["Step"].Width = 0;

                    cp.JSProperties["cp_clicked"] = "CONSOLIDATED";

                    btnMultiplePR.ClientEnabled = true;
                    Freeze();
                    break;

                case "Generate":
                    GetSelectedVal();
                    Freeze();
                    break;

                case "MultiplePR":
                    GenerateMultiplePR();
                    Freeze();
                    cp.JSProperties["cp_multiplepr"] = true;
                    break;
    

            }
        }

        //protected void GetVat()
        //{
        //    DataTable getvat = Gears.RetriveData2("Select DISTINCT Rate from masterfile.BPCustomerInfo BPCI LEFT JOIN Masterfile.Tax T ON BPCI.TaxCode = T.TCode where BizPartnerCode =  '" + glSoldTo.Text + "'");

        //    if (getvat.Rows.Count > 0)
        //    {
        //        cp.JSProperties["cp_vatrate"] = getvat.Rows[0]["Rate"].ToString();
        //    }

        //    else
        //    {
        //        cp.JSProperties["cp_vatrate"] = "0ite
        //    }

        //}

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
            //        && dataColumn.FieldName != "UnitPrice" && dataColumn.FieldName != "Remarks" && dataColumn.FieldName != "Qty"
            //        && dataColumn.FieldName != "Field1" && dataColumn.FieldName != "Field2" && dataColumn.FieldName != "Field3" && dataColumn.FieldName != "Field4"
            //        && dataColumn.FieldName != "Field5" && dataColumn.FieldName != "Field6" && dataColumn.FieldName != "Field7" && dataColumn.FieldName != "Field8"
            //        && dataColumn.FieldName != "Field9" && dataColumn.FieldName != "VatRate" && dataColumn.FieldName != "IsVatable" && dataColumn.FieldName != "UnitCost"
            //        && dataColumn.FieldName != "ItemCode" && dataColumn.FieldName != "ColorCode" && dataColumn.FieldName != "SizeCode" && dataColumn.FieldName != "ClassCode"
            //        && dataColumn.FieldName != "AccumulatedDepreciation" && dataColumn.FieldName != "VatRate")
            //    {
            //        e.Errors[dataColumn] = "Value can't be null.";//Sets error tooltip message
            //    }
            //    //Checking for non existing Codes..

            //    ItemCode = String.IsNullOrEmpty(Convert.ToString(e.NewValues["ItemCode"])) ? "" : e.NewValues["ItemCode"].ToString();
            //    ColorCode = String.IsNullOrEmpty(Convert.ToString(e.NewValues["ColorCode"])) ? "" : e.NewValues["ColorCode"].ToString(); 
            //    ClassCode = String.IsNullOrEmpty(Convert.ToString(e.NewValues["ClassCode"])) ? "" : e.NewValues["ClassCode"].ToString(); 
            //    SizeCode = String.IsNullOrEmpty(Convert.ToString(e.NewValues["SizeCode"])) ? "" : e.NewValues["SizeCode"].ToString();
             
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

        protected void Connection_Init(object sender, EventArgs e)
        {
            //sdsDetail.ConnectionString = Session["ConnString"].ToString();
            //Masterfileitem.ConnectionString = Session["ConnString"].ToString();
            //Masterfileitemdetail.ConnectionString = Session["ConnString"].ToString();
            //ReceivingWarehouselookup.ConnectionString = Session["ConnString"].ToString();
            //CustomerCodelookup.ConnectionString = Session["ConnString"].ToString();
            //CostCenterlookup.ConnectionString = Session["ConnString"].ToString();
            //PropertyNumberLookup.ConnectionString = Session["ConnString"].ToString();
            //AssetAcquisitionLookup.ConnectionString = Session["ConnString"].ToString();
            //VatCodeLookup.ConnectionString = Session["ConnString"].ToString();

            //JobOrderLookup.ConnectionString = Session["ConnString"].ToString();
            //CustomerLookup.ConnectionString = Session["ConnString"].ToString();

            ((SqlDataSource)sender).ConnectionString = Session["ConnString"].ToString();
        }

        protected void gvdetailgrid_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            var lookup = sender as ASPxGridView;
            if (e.Parameters.Contains("CreatePR"))
            {
                //"00001|JO-ITEM-1|RED|A|S|-190.00"
                string location = e.Parameters.Split('|')[0];//Set column name
                string supplier = e.Parameters.Split('|')[1];//Set column name
                string deliverydate = e.Parameters.Split('|')[2];//Set column name
                //deliverydate = Convert.ToDateTime(deliverydate).ToShortDateString();

                string itemcode = Session["SKU"].ToString().Split('|')[1];
                string colorcode = Session["SKU"].ToString().Split('|')[2];
                string classcode = Session["SKU"].ToString().Split('|')[3];
                string sizecode = Session["SKU"].ToString().Split('|')[4];
                string requestqty = Session["SKU"].ToString().Split('|')[5];


                string DocNumber = "";
                string Customer = "";
                string datenow = DateTime.Now.ToShortDateString();
                string datenow24 = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
                DataTable DocNumberSetter = Gears.RetriveData2("SELECT 'PR' + RIGHT('0000000' + CAST(CONVERT(int, SeriesNumber) + 1 AS VARCHAR(10)), 7) FROM it.DocNumberSettings where Module='PRCPRM' AND RecordId ='73'", Session["ConnString"].ToString());//ADD CONN
                foreach (DataRow dt in DocNumberSetter.Rows)
                {
                    DocNumber = dt[0].ToString();
                }
                DataTable CustomerSetter = Gears.RetriveData2("SELECT CustomerCode FROM Production.JobOrder WHERE DocNumber = '" + glJobOrder.Value + "'", Session["ConnString"].ToString());//ADD CONN
                foreach (DataRow dt in CustomerSetter.Rows)
                {
                    Customer = dt[0].ToString();
                }
                Gears.RetriveData2("UPDATE IT.DocNumberSettings SET SeriesNumber += 1 WHERE Module='PRCPRM' AND RecordId ='73'", Session["ConnString"].ToString());//ADD CONN

                Gears.RetriveData2("INSERT INTO Procurement.PurchaseRequest"
	                        + " (DocNumber,DocDate,TargetDate,CustomerCode,Remarks,IsWithDetail,AddedBy,AddedDate)"
                            + " VALUES ('" + DocNumber + "','" + datenow + "','" + deliverydate + "','" + Customer + "','" + supplier + "', 1, '" + Session["userid"].ToString() + "', '" + datenow24 + "')", Session["ConnString"].ToString());

                Gears.RetriveData2("INSERT INTO Procurement.PurchaseRequestDetail"
                            + " (LineNumber,DocNumber,ItemCode,ColorCode,ClassCode,SizeCode,RequestQty,IsAllowPartial)"
                            + " VALUES ('00001','" + DocNumber + "','" + itemcode + "','" + colorcode + "', '" + classcode + "', '" + sizecode + "','" + requestqty + "','1')", Session["ConnString"].ToString());

                
                lookup.JSProperties["cp_creation"] = "CreateSinglePR";
            }
            else if (e.Parameters.Contains("MultiplePRCreation"))
            {
                List<string> holder = new List<string>();
                string itemcode = "";
                string colorcode = "";
                string classcode = "";
                string sizecode = "";
                string requestqty = "";

                string DocNumber = "";
                string BaseUnit = "";
                string Customer = "";
                string datenow = DateTime.Now.ToShortDateString();
                string datenow24 = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
                DataTable DocNumberSetter = Gears.RetriveData2("SELECT 'PR' + RIGHT('0000000' + CAST(CONVERT(int, SeriesNumber) + 1 AS VARCHAR(10)), 7) FROM it.DocNumberSettings where Module='PRCPRM' AND RecordId ='73'", Session["ConnString"].ToString());//ADD CONN
                foreach (DataRow dt in DocNumberSetter.Rows)
                {
                    DocNumber = dt[0].ToString();
                }
                DataTable CustomerSetter = Gears.RetriveData2("SELECT CustomerCode FROM Production.JobOrder WHERE DocNumber = '" + glJobOrder.Value + "'", Session["ConnString"].ToString());//ADD CONN
                foreach (DataRow dt in CustomerSetter.Rows)
                {
                    Customer = dt[0].ToString();
                }
                Gears.RetriveData2("UPDATE IT.DocNumberSettings SET SeriesNumber += 1 WHERE Module='PRCPRM' AND RecordId ='73'", Session["ConnString"].ToString());//ADD CONN

                Gears.RetriveData2("INSERT INTO Procurement.PurchaseRequest"
                            + " (DocNumber,DocDate,TargetDate,CustomerCode,IsWithDetail,AddedBy,AddedDate)"
                            + " VALUES ('" + DocNumber + "','" + datenow + "','" + datenow24 + "','" + Customer + "', 1, '" + Session["userid"].ToString() + "', '" + datenow24 + "')", Session["ConnString"].ToString());

                for(int x = 0; x < e.Parameters.Split(',').Length; x++)
                {
                    holder.Add(e.Parameters.Split(',')[x]);
                }

                for (int x = 0; x < holder.Count; x++)
                {
                    if(holder[x].ToString().Split('|')[0] == "MultiplePRCreation")
                    {
                        itemcode = holder[x].ToString().Split('|')[2];
                        colorcode = holder[x].ToString().Split('|')[3];
                        classcode = holder[x].ToString().Split('|')[4];
                        sizecode = holder[x].ToString().Split('|')[5];
                        requestqty = holder[x].ToString().Split('|')[6];

                        int linenum = 0;
                        DataTable count = Gears.RetriveData2("select max(convert(int,LineNumber)) as LineNumber from Procurement.PurchaseRequestDetail where DocNumber = '" + DocNumber + "'", Session["ConnString"].ToString());

                        try
                        {
                            linenum = Convert.ToInt32(count.Rows[0][0].ToString()) + 1;
                        }
                        catch
                        {
                            linenum = 1;
                        }
                        string strLine = linenum.ToString().PadLeft(5, '0');

                        DataTable UnitSetter = Gears.RetriveData2("SELECT UnitBase FROM Masterfile.Item WHERE ItemCode= '" + itemcode + "'", Session["ConnString"].ToString());//ADD CONN
                        foreach (DataRow dt in UnitSetter.Rows)
                        {
                            BaseUnit = dt[0].ToString();
                        }


                        Gears.RetriveData2("INSERT INTO Procurement.PurchaseRequestDetail"
                                   + " (LineNumber,DocNumber,ItemCode,ColorCode,ClassCode,SizeCode,RequestQty,UnitBase,IsAllowPartial)"
                                   + " VALUES ('" + strLine + "','" + DocNumber + "','" + itemcode + "','" + colorcode + "', '" + classcode + "', '" + sizecode + "','" + requestqty + "','" + BaseUnit + "', '1')", Session["ConnString"].ToString());
                    }
                        
                    else
                    {
                        itemcode = holder[x].ToString().Split('|')[1];
                        colorcode = holder[x].ToString().Split('|')[2];
                        classcode = holder[x].ToString().Split('|')[3];
                        sizecode = holder[x].ToString().Split('|')[4];
                        requestqty = holder[x].ToString().Split('|')[5];

                        int linenum = 0;
                        DataTable count = Gears.RetriveData2("select max(convert(int,LineNumber)) as LineNumber from Procurement.PurchaseRequestDetail where DocNumber = '" + DocNumber + "'", Session["ConnString"].ToString());

                        try
                        {
                            linenum = Convert.ToInt32(count.Rows[0][0].ToString()) + 1;
                        }
                        catch
                        {
                            linenum = 1;
                        }
                        string strLine = linenum.ToString().PadLeft(5, '0');

                        DataTable UnitSetter = Gears.RetriveData2("SELECT UnitBase FROM Masterfile.Item WHERE ItemCode= '" + itemcode + "'", Session["ConnString"].ToString());//ADD CONN
                        foreach (DataRow dt in UnitSetter.Rows)
                        {
                            BaseUnit = dt[0].ToString();
                        }

                        Gears.RetriveData2("INSERT INTO Procurement.PurchaseRequestDetail"
                                   + " (LineNumber,DocNumber,ItemCode,ColorCode,ClassCode,SizeCode,RequestQty,UnitBase,IsAllowPartial)"
                                   + " VALUES ('" + strLine + "','" + DocNumber + "','" + itemcode + "','" + colorcode + "', '" + classcode + "', '" + sizecode + "','" + requestqty + "','" + BaseUnit + "', '1')", Session["ConnString"].ToString());
                    }
                }


                lookup.JSProperties["cp_creation"] = "CreateMultiplePR";
            }

            else
            {
                Session["SKU"] = null;
                Session["SKU"] = e.Parameters;
                string linenumber = e.Parameters.Split('|')[0];//Set column name
                string itemcode = e.Parameters.Split('|')[1];//Set column name
                string colorcode = e.Parameters.Split('|')[2];//Set column name
                string classcode = e.Parameters.Split('|')[3];//Set column name
                string sizecode = e.Parameters.Split('|')[4];//Set column name
                string neededqty = e.Parameters.Split('|')[5];//Set column name

                sdsDetail2.SelectCommand = "exec sp_Generate_MaterialPlanning_Sub_Detail '" + itemcode + "','" + colorcode + "','" + sizecode + "','" + classcode + "','" + neededqty + "'";

                gvdetailgrid.DataSource = sdsDetail2;
                gvdetailgrid.DataBind();
            }
        }

        protected void glCustomer_Init(object sender, EventArgs e)
        {
        }
    }
}