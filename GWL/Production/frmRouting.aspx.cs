using DevExpress.Spreadsheet;
using DevExpress.Spreadsheet.Export;
using DevExpress.Web;
using DevExpress.Web.Data;
using DevExpress.XtraGrid;
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
using GearsProduction;
using System.Data.OleDb;

namespace GWL
{
    public partial class frmRouting : System.Web.UI.Page
    {
        Boolean error = false;//Boolean for grid validation
        Boolean view = false;//Boolean for view state
        Boolean check = false;//Boolean for grid validation
        Boolean edit = false;   //Boolean for Edit Mode
        Boolean postback = false;

        Entity.ProductionRouting _Entity = new Entity.ProductionRouting();//Calls entity odsHeader
        Entity.ProductionRouting.ProdRoutingStep _ProdRoutingStep = new Entity.ProductionRouting.ProdRoutingStep();
        Entity.ProductionRouting.ProdRoutingStepBOM _ProdRoutingStepBOM = new Entity.ProductionRouting.ProdRoutingStepBOM();
        Entity.ProductionRouting.ProdRoutingStepMachine _ProdRoutingStepMachine = new Entity.ProductionRouting.ProdRoutingStepMachine();
        Entity.ProductionRouting.ProdRoutingStepManpower _ProdRoutingStepManpower = new Entity.ProductionRouting.ProdRoutingStepManpower();
        Entity.ProductionRouting.ProdRoutingOtherMaterial _ProdRoutingOtherMaterial = new Entity.ProductionRouting.ProdRoutingOtherMaterial();

        #region page load/entry
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["entry"] == "N") Status.Text = "New"; // Set status Value if entry is N

            view = (Request.QueryString["entry"].ToString() == "V"); // If entry equals to V, then readonly

            if (!IsPostBack)
            {
                //datasources(); // Load all datasources selectcommand

                // Destroy Sessions
                Session["SKUCode"] = null;
                Session["StepSequence"] = null;
                //postback = true;

                switch (Request.QueryString["entry"].ToString())
                {
                    case "N":
                    case "E":
                        updateBtn.Text = "Update";
                        break;

                    case "V":
                        view = true;
                        updateBtn.Text = "Close";
                        ViewOnly();
                        break;

                    case "D":
                        view = true;
                        updateBtn.Text = "Delete";
                        ViewOnly();
                        break;
                }

                switch (Request.QueryString["entry"].ToString())
                {
                    case "N":
                    case "E":
                    case "V":
                        DataTable dt = Gears.RetriveData2("SELECT SKUCode FROM Production.ProdRouting WHERE RecordID='" + Request.QueryString["docnumber"].ToString()+"'", Session["ConnString"].ToString());

                        SKUCode.Value = dt.Rows[0][0].ToString();

                        _Entity.getdata(SKUCode.Text, Session["ConnString"].ToString());//ADD CONN

                        setValues();
                        break;
                }
            }
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            connect();
        }

        private void connect()
        {
            foreach (Control c in form2.Controls)
            {
                if (c is SqlDataSource)
                {
                    ((SqlDataSource)c).ConnectionString = Session["ConnString"].ToString();
                }
            }
        }
        #endregion

        protected void Connection_Init(object sender, EventArgs e)
        {
            ((SqlDataSource)sender).ConnectionString = Session["ConnString"].ToString();
        }

        protected void BatchUpdate(object sender, ASPxDataBatchUpdateEventArgs e)
        {
            if ((error == true || error == false) && check == false)//Prevents updating of grid to enable validation
            {
                e.Handled = true;
                e.InsertValues.Clear();
                e.UpdateValues.Clear();
            }
        }

        protected void DefaultValues(object sender, ASPxDataInitNewRowEventArgs e) // Gridview Default Values
        {
            e.NewValues["PercentageAllowance"] = 0;
            //e.NewValues["EstimatedUnitCost"] = 0;
        }

        protected void gv_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e) // Gridview CommandButton
        {   //Sets icon for grid/Control for Enabling/Disabling of buttons on grid.
            ASPxGridView grid = sender as ASPxGridView;
            //if (Request.Params["__CALLBACKID"] == null && IsPostBack) return;
            //if (!IsPostBack || Request.Params["__CALLBACKID"].Contains(grid.ID))
            //{
            //    if (Request.QueryString["entry"] == "V" || Request.QueryString["entry"] == "D")
            //    {
            //        e.Visible = false;
            //    }
            //}
            if (Request.QueryString["entry"] == "V" || Request.QueryString["entry"] == "D")
            {
                e.Visible = false;
            }
        }

        protected void gv_CustomButtonInitialize(object sender, ASPxGridViewCustomButtonEventArgs e) // Gridview CustomButton
        {
            ASPxGridView grid = sender as ASPxGridView;
            //if (Request.Params["__CALLBACKID"] == null && IsPostBack) return;
            //if (!IsPostBack || Request.Params["__CALLBACKID"].Contains(grid.ID))
            //{
            //    if (Request.QueryString["entry"] == "V" || Request.QueryString["entry"] == "D")
            //    {
            //        e.Visible = DevExpress.Utils.DefaultBoolean.False;
            //    }
            //}
            if (Request.QueryString["entry"] == "V" || Request.QueryString["entry"] == "D")
            {
                e.Visible = DevExpress.Utils.DefaultBoolean.False;
            }
        }

        protected void grid_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e) // Validation for gridview, set all validate conditions here.. (sample here is checking of empty cells)
        { 
            if (error == false && check == false)
            {
                if (e.Errors.Count > 0)
                {
                    error = true; //bool to cancel adding/updating if true
                }
            }
        }

        #region Functions
        //private void datasources()
        //{
        //    // Gridview
        //    sdsStepProcess.SelectCommand = "SELECT SKUCode, StepSequence, A.StepCode, B.Description AS StepDescription, SequenceDay FROM Production.ProdRoutingStep A LEFT JOIN Masterfile.Step B ON A.StepCode = B.StepCode WHERE SKUCode IS NULL";
        //    sdsBOM.SelectCommand = "SELECT * FROM Production.ProdRoutingStepBOM WHERE SKUCode IS NULL";
        //    sdsMachine.SelectCommand = "SELECT * FROM Production.ProdRoutingStepMachine WHERE SKUCode IS NULL";
        //    sdsManpower.SelectCommand = "SELECT * FROM Production.ProdRoutingStepManpower WHERE SKUCode IS NULL";
        //    sdsOtherMaterials.SelectCommand = "SELECT * FROM Production.ProdRoutingOtherMaterials WHERE SKUCode IS NULL";

        //    // Lookup
        //    sdsSKUCode.SelectCommand = "SELECT A.ItemCode, A.FullDesc FROM Masterfile.Item A LEFT JOIN Production.ProdRouting B ON A.ItemCode = B.SKUCode WHERE B.SKUCode IS NULL";
        //    sdsCustomerCode.SelectCommand = "SELECT BizPartnerCode, Name FROM Masterfile.BizPartner";
        //    sdsStepCode.SelectCommand = "SELECT StepCode, Description FROM Masterfile.Step";
        //}

        protected void setValues()
        {
            RecordID.Value = _Entity.RecordID.ToString();
            SKUDescription.Text = _Entity.SKUDescription.ToString();
            CustomerCode.Value = _Entity.CustomerCode.ToString();
            CustomerName.Text = _Entity.CustomerName.ToString();
            Remarks.Text = _Entity.Remarks.ToString();
            UnitMeasure.Text = _Entity.UnitMeasure.ToString();
            ExpectedOutputQty.Text = _Entity.ExpectedOutputQty.ToString();
            Status.Text = _Entity.Status.ToString();
            EffectivityDate.Value = String.IsNullOrEmpty(_Entity.EffectivityDate.ToString()) ? null : Convert.ToDateTime(_Entity.EffectivityDate.ToString()).ToShortDateString();
        }

        protected void ViewOnly() {
            CustomerCode.Enabled = false;
            Remarks.Enabled = false;
            UnitMeasure.Enabled = false;
            ExpectedOutputQty.Enabled = false;
        }

        protected void lookup_Init(object sender, EventArgs e)
        {
            ASPxGridLookup gridLookup = sender as ASPxGridLookup;
            if (IsCallback && Request.Params["__CALLBACKID"].Contains(gridLookup.ID))
            {
                gridLookup.GridView.CustomCallback += new ASPxGridViewCustomCallbackEventHandler(gridView_CustomCallback);
                //if (Session["PRitemID"] != null)
                //{
                //    gridLookup.GridView.DataSourceID = "sdsUnit";
                //    sdsUnit.SelectCommand = Session["PRsql"].ToString();
                //    gridLookup.DataBind();
                //}

                //gridLookup.GridView.CustomCallback += new ASPxGridViewCustomCallbackEventHandler(gridView_CustomCallback);
                //if (Session["PRitemID"] != null)
                //{
                //    if (Session["PRitemID"].ToString() == glIDFinder(gridLookup.ID))
                //    {
                //        gridLookup.GridView.DataSourceID = "Masterfileitemdetail";
                //        Masterfileitemdetail.SelectCommand = Session["PRsql"].ToString();
                //        //Masterfileitemdetail.FilterExpression = Session["FilterExpression"].ToString();
                //        //Session["FilterExpression"] = null;
                //        gridLookup.DataBind();
                //    }
                //}
            }
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
        #endregion



        #region Callback functions(Inserting/Updating/Deleting/Validating)
        protected void cp_Callback(object sender, CallbackEventArgsBase e)
        {
            _Entity.Connection = Session["ConnString"].ToString();
            _Entity.RecordID = RecordID.Value;
            _Entity.SKUCode = SKUCode.Text;
            _Entity.CustomerCode = CustomerCode.Text;
            _Entity.UnitMeasure = UnitMeasure.Text;
            _Entity.ExpectedOutputQty = (ExpectedOutputQty.Text == "") ? Convert.ToDecimal(0.00) : Convert.ToDecimal(ExpectedOutputQty.Text);
            _Entity.Status = Status.Text;
            _Entity.Remarks = Remarks.Text;
            _Entity.AddedBy = Session["userid"].ToString();
            _Entity.LastEditedBy = Session["userid"].ToString();

            string conn = Session["ConnString"].ToString();
            string param = e.Parameter.Split('|')[0];
            switch (param)
            {
                case "Add":
                case "Update":

                if (error == false)
                {
                    check = true;

                    _Entity.UpdateData(_Entity);

                    cp.JSProperties["cp_message"] = "Updated Successfully!";

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

                case "Close":

                    cp.JSProperties["cp_close"] = true;

                break;
            }
        }

        public void gridView_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            string column = e.Parameters.Split('|')[0];//Set column name
            if (column.Contains("GLP_AIC") || column.Contains("GLP_AC") || column.Contains("GLP_F")) return;//Traps the callback
            string itemcode = e.Parameters.Split('|')[1];//Set Item Code
            string val = e.Parameters.Split('|')[2];//Set column value

            if (itemcode != "" || itemcode != null)
            {
                if (e.Parameters.Contains("Unit"))
                {
                    sdsUnit.SelectCommand = "SELECT DISTINCT * FROM( " +
                                                    "SELECT DISTINCT UnitBase AS Unit, B.Description, C.ConversionFactor FROM Masterfile.Item A  " +
                                                    "LEFT JOIN Masterfile.Unit B ON A.ItemCode = B.UnitCode  " +
                                                    "LEFT JOIN Masterfile.UnitConversion C ON B.UnitCode = C.UnitCodeFrom WHERE A.ItemCode = '" + itemcode + "' " +
                                                    "UNION ALL " +
                                                    "SELECT DISTINCT BaseUnit AS Unit, B.Description, C.ConversionFactor FROM Masterfile.ItemDetail A  " +
                                                    "LEFT JOIN Masterfile.Unit B ON A.ItemCode = B.UnitCode  " +
                                                    "LEFT JOIN Masterfile.UnitConversion C ON B.UnitCode = C.UnitCodeFrom WHERE A.ItemCode = '" + itemcode + "' " +
                                                ") AS TMP";
                    Session["PRitemID"] = "Unit";
                }
            }
            else
            {
                sdsUnit.SelectCommand = "SELECT A.UnitCode AS Unit, A.Description, B.ConversionFactor FROM Masterfile.Unit A LEFT JOIN Masterfile.UnitConversion B ON A.UnitCode = B.UnitCodeFrom WHERE ISNULL(A.IsInactive,0)=0";
                Session["PRitemID"] = "Unit";
            }

            ASPxGridView grid = sender as ASPxGridView;

            grid.DataSourceID = "sdsUnit";
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
            protected void masterGrid_BeforePerformDataSelect(object sender, EventArgs e) //
        {
                sdsStepProcess.SelectCommand = "SELECT A.RecordID, HeaderID, SKUCode, StepSequence, A.StepCode, B.Description AS StepDescription, " +
                                                "SequenceDay, A.IsBackFlush FROM Production.ProdRoutingStep A LEFT JOIN Masterfile.Step B ON A.StepCode = B.StepCode " +
                                                "WHERE SKUCode='" + SKUCode.Text + "' AND HeaderID='" + RecordID.Value + "' ORDER BY StepSequence ASC";
                sdsStepProcess.DataBind();
        }
        protected void detailBOM_BeforePerformDataSelect(object sender, EventArgs e) //
        {
            sdsBOM.SelectCommand = "SELECT A.*, B.FullDesc AS Description FROM Production.ProdRoutingStepBOM A LEFT JOIN Masterfile.Item B " +
                                    "ON A.ItemCode = B.ItemCode WHERE SKUCode='" + (sender as ASPxGridView).GetMasterRowFieldValues("SKUCode").ToString() + "'" +
                                    "AND StepSequence='" + (sender as ASPxGridView).GetMasterRowFieldValues("StepSequence").ToString()  +
                                    "' AND HeaderID='" + (sender as ASPxGridView).GetMasterRowFieldValues("HeaderID").ToString() + "' AND StepCode='" + (sender as ASPxGridView).GetMasterRowFieldValues("StepCode").ToString() + "'";
            sdsBOM.DataBind();
        }
        protected void detailMachine_BeforePerformDataSelect(object sender, EventArgs e) //
        {
            sdsMachine.SelectCommand = "SELECT * FROM Production.ProdRoutingStepMachine WHERE SKUCode='" + (sender as ASPxGridView).GetMasterRowFieldValues("SKUCode").ToString() + "'" +
                                        "AND StepSequence='" + (sender as ASPxGridView).GetMasterRowFieldValues("StepSequence").ToString() + 
                                        "' AND HeaderID='" + (sender as ASPxGridView).GetMasterRowFieldValues("HeaderID").ToString() + "' AND StepCode='" + (sender as ASPxGridView).GetMasterRowFieldValues("StepCode").ToString() + "'";
            sdsMachine.DataBind();
        }
        protected void detailManpower_BeforePerformDataSelect(object sender, EventArgs e) //
        {
            sdsManpower.SelectCommand = "SELECT A.*, B.FullDesc AS SKUDescription FROM Production.ProdRoutingStepManpower A LEFT JOIN Masterfile.Item B " +
                                        "ON A.SKUCode = B.ItemCode WHERE SKUCode='" + (sender as ASPxGridView).GetMasterRowFieldValues("SKUCode").ToString() + "'" +
                                        "AND StepSequence='" + (sender as ASPxGridView).GetMasterRowFieldValues("StepSequence").ToString() + 
                                        "' AND HeaderID='" + (sender as ASPxGridView).GetMasterRowFieldValues("HeaderID").ToString() + "' AND StepCode='" + (sender as ASPxGridView).GetMasterRowFieldValues("StepCode").ToString() + "'";
            sdsManpower.DataBind();
        }
        protected void otherGrid_BeforePerformDataSelect(object sender, EventArgs e) //
        {
            sdsOtherMaterials.SelectCommand = "SELECT A.*, B.FullDesc AS Description FROM Production.ProdRoutingOtherMaterials A LEFT JOIN Masterfile.Item B " +
                                                "ON A.ItemCode = B.ItemCode WHERE SKUCode='" + SKUCode.Text + "' AND HeaderID='" + RecordID.Value + "'";
            sdsOtherMaterials.DataBind();
        }

        protected void gvStepProcess_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e) //
        {
            if (!IsPostBack) return;

            sdsStepProcess.InsertParameters["IHeaderID"].DefaultValue = RecordID.Value;
            sdsStepProcess.InsertParameters["ISKUCode"].DefaultValue = SKUCode.Text;
            sdsStepProcess.InsertParameters["IStepSequence"].DefaultValue = e.NewValues["StepSequence"].ToString();
            sdsStepProcess.InsertParameters["IStepCode"].DefaultValue = e.NewValues["StepCode"].ToString();
            sdsStepProcess.InsertParameters["IsBackFlush"].DefaultValue = Convert.ToBoolean(e.NewValues["IsBackFlush"]).ToString();

            (sender as ASPxGridView).JSProperties["cp_redirect"] = "success";
            (sender as ASPxGridView).JSProperties["cp_successmes"] = "Updated Successfully.";
        }
        protected void gvStepProcess_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e) //
        {
            if (!IsPostBack) return;

            sdsStepProcess.UpdateParameters["ParamURecordID"].DefaultValue = e.OldValues["RecordID"].ToString();

            sdsStepProcess.UpdateParameters["UStepSequence"].DefaultValue = e.NewValues["StepSequence"].ToString();
            sdsStepProcess.UpdateParameters["UStepCode"].DefaultValue = e.NewValues["StepCode"].ToString();
            sdsStepProcess.UpdateParameters["UIsBackFlush"].DefaultValue = Convert.ToBoolean(e.NewValues["IsBackFlush"]).ToString();

            gvStepProcess.JSProperties["cp_redirect"] = "success";
            gvStepProcess.JSProperties["cp_successmes"] = "Updated Successfully.";
        }
        protected void gvStepProcess_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e) //
        {
            sdsStepProcess.DeleteParameters["DRecordID"].DefaultValue = e.Values["RecordID"].ToString();

            (sender as ASPxGridView).JSProperties["cp_redirect"] = "success";
            (sender as ASPxGridView).JSProperties["cp_successmes"] = "Deleted Successfully.";
        }

        protected void gvStepBOM_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e) //
        {
            if (!IsPostBack) return;

            var StepSequence = (sender as ASPxGridView).GetMasterRowFieldValues("StepSequence").ToString();
            var StepCode = (sender as ASPxGridView).GetMasterRowFieldValues("StepCode").ToString();

            sdsBOM.InsertParameters["IHeaderID"].DefaultValue = RecordID.Value;
            sdsBOM.InsertParameters["ISKUCode"].DefaultValue = SKUCode.Text;
            sdsBOM.InsertParameters["IStepSequence"].DefaultValue = StepSequence;
            sdsBOM.InsertParameters["IStepCode"].DefaultValue = StepCode;
            sdsBOM.InsertParameters["IItemCode"].DefaultValue = e.NewValues["ItemCode"].ToString();
            sdsBOM.InsertParameters["IUnit"].DefaultValue = e.NewValues["Unit"].ToString();
            sdsBOM.InsertParameters["IConsumptionPerProduct"].DefaultValue = e.NewValues["ConsumptionPerProduct"].ToString();
            sdsBOM.InsertParameters["ITotalConsumption"].DefaultValue = e.NewValues["TotalConsumption"].ToString();
            sdsBOM.InsertParameters["IPercentageAllowance"].DefaultValue = e.NewValues["PercentageAllowance"].ToString();
            sdsBOM.InsertParameters["IQtyAllowance"].DefaultValue = e.NewValues["QtyAllowance"].ToString();
            sdsBOM.InsertParameters["IClientSuppliedMaterial"].DefaultValue = Convert.ToBoolean(e.NewValues["ClientSuppliedMaterial"]).ToString();
            sdsBOM.InsertParameters["IEstimatedUnitCost"].DefaultValue = e.NewValues["EstimatedUnitCost"].ToString();
            sdsBOM.InsertParameters["IStandardUsage"].DefaultValue = e.NewValues["StandardUsage"].ToString();
            sdsBOM.InsertParameters["IStandardUsageUnit"].DefaultValue = e.NewValues["StandardUsageUnit"].ToString();
            sdsBOM.InsertParameters["IRemarks"].DefaultValue = e.NewValues["Remarks"] == null ? "" : e.NewValues["Remarks"].ToString();

            (sender as ASPxGridView).JSProperties["cp_redirect"] = "success";
            (sender as ASPxGridView).JSProperties["cp_successmes"] = "Updated Successfully.";
        }
        protected void gvStepBOM_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e) //
        {
            if (!IsPostBack) return;

            var StepSequence = (sender as ASPxGridView).GetMasterRowFieldValues("StepSequence").ToString();
            var StepCode = (sender as ASPxGridView).GetMasterRowFieldValues("StepCode").ToString();

            sdsBOM.UpdateParameters["ParamURecordID"].DefaultValue = e.OldValues["RecordID"].ToString();

            sdsBOM.UpdateParameters["UStepSequence"].DefaultValue = StepSequence;
            sdsBOM.UpdateParameters["UStepCode"].DefaultValue = StepCode;
            sdsBOM.UpdateParameters["UItemCode"].DefaultValue = e.NewValues["ItemCode"].ToString();
            sdsBOM.UpdateParameters["UUnit"].DefaultValue = e.NewValues["Unit"].ToString();
            sdsBOM.UpdateParameters["UConsumptionPerProduct"].DefaultValue = e.NewValues["ConsumptionPerProduct"].ToString();
            sdsBOM.UpdateParameters["UTotalConsumption"].DefaultValue = e.NewValues["TotalConsumption"].ToString();
            sdsBOM.UpdateParameters["UPercentageAllowance"].DefaultValue = e.NewValues["PercentageAllowance"].ToString();
            sdsBOM.UpdateParameters["UQtyAllowance"].DefaultValue = e.NewValues["QtyAllowance"].ToString();
            sdsBOM.UpdateParameters["UClientSuppliedMaterial"].DefaultValue = Convert.ToBoolean(e.NewValues["ClientSuppliedMaterial"]).ToString();
            sdsBOM.UpdateParameters["UEstimatedUnitCost"].DefaultValue = e.NewValues["EstimatedUnitCost"].ToString();
            sdsBOM.UpdateParameters["UStandardUsage"].DefaultValue = e.NewValues["StandardUsage"].ToString();
            sdsBOM.UpdateParameters["UStandardUsageUnit"].DefaultValue = e.NewValues["StandardUsageUnit"].ToString();
            sdsBOM.UpdateParameters["URemarks"].DefaultValue = e.NewValues["Remarks"] == null ? "" : e.NewValues["Remarks"].ToString();

            (sender as ASPxGridView).JSProperties["cp_redirect"] = "success";
            (sender as ASPxGridView).JSProperties["cp_successmes"] = "Updated Successfully.";
        }

        protected void gvStepBOM_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e) //
        {
            sdsBOM.DeleteParameters["DRecordID"].DefaultValue = e.Values["RecordID"].ToString();

            (sender as ASPxGridView).JSProperties["cp_redirect"] = "success";
            (sender as ASPxGridView).JSProperties["cp_successmes"] = "Updated Successfully.";
        }

        protected void gvStepMachine_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e) //
        {
            if (!IsPostBack) return;

            var StepSequence = (sender as ASPxGridView).GetMasterRowFieldValues("StepSequence").ToString();
            var StepCode = (sender as ASPxGridView).GetMasterRowFieldValues("StepCode").ToString();

            sdsMachine.InsertParameters["IHeaderID"].DefaultValue = RecordID.Value;
            sdsMachine.InsertParameters["ISKUCode"].DefaultValue = SKUCode.Text;
            sdsMachine.InsertParameters["IStepSequence"].DefaultValue = StepSequence;
            sdsMachine.InsertParameters["IStepCode"].DefaultValue = StepCode;
            sdsMachine.InsertParameters["IMachineType"].DefaultValue = e.NewValues["MachineType"].ToString();
            sdsMachine.InsertParameters["ILocation"].DefaultValue = e.NewValues["Location"] == null ? "" : e.NewValues["Location"].ToString();
            sdsMachine.InsertParameters["IMachineRun"].DefaultValue = e.NewValues["MachineRun"].ToString();
            sdsMachine.InsertParameters["IUnit"].DefaultValue = e.NewValues["Unit"].ToString();
            sdsMachine.InsertParameters["IMachineCapacityQty"].DefaultValue = e.NewValues["MachineCapacityQty"] == null ? "" : e.NewValues["MachineCapacityQty"].ToString();
            sdsMachine.InsertParameters["IMachineCapacityUnit"].DefaultValue = e.NewValues["MachineCapacityUnit"] == null ? "" : e.NewValues["MachineCapacityUnit"].ToString();
            sdsMachine.InsertParameters["ICostPerUnit"].DefaultValue = e.NewValues["CostPerUnit"].ToString();

            (sender as ASPxGridView).JSProperties["cp_redirect"] = "success";
            (sender as ASPxGridView).JSProperties["cp_successmes"] = "Updated Successfully.";
        }
        protected void gvStepMachine_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e) //
        {
            if (!IsPostBack) return;

            var StepSequence = (sender as ASPxGridView).GetMasterRowFieldValues("StepSequence").ToString();
            var StepCode = (sender as ASPxGridView).GetMasterRowFieldValues("StepCode").ToString();

            sdsMachine.UpdateParameters["ParamURecordID"].DefaultValue = e.OldValues["RecordID"].ToString();

            sdsMachine.UpdateParameters["UStepSequence"].DefaultValue = StepSequence;
            sdsMachine.UpdateParameters["UStepCode"].DefaultValue = StepCode;
            sdsMachine.UpdateParameters["UMachineType"].DefaultValue = e.NewValues["MachineType"].ToString();
            sdsMachine.UpdateParameters["ULocation"].DefaultValue = e.NewValues["Location"] == null ? "" : e.NewValues["Location"].ToString();
            sdsMachine.UpdateParameters["UMachineRun"].DefaultValue = e.NewValues["MachineRun"].ToString();
            sdsMachine.UpdateParameters["UUnit"].DefaultValue = e.NewValues["Unit"].ToString();
            sdsMachine.UpdateParameters["UMachineCapacityQty"].DefaultValue = e.NewValues["MachineCapacityQty"] == null ? "" : e.NewValues["MachineCapacityQty"].ToString();
            sdsMachine.UpdateParameters["UMachineCapacityUnit"].DefaultValue = e.NewValues["MachineCapacityUnit"] == null ? "" : e.NewValues["MachineCapacityUnit"].ToString();
            sdsMachine.UpdateParameters["UCostPerUnit"].DefaultValue = e.NewValues["CostPerUnit"].ToString();

            (sender as ASPxGridView).JSProperties["cp_redirect"] = "success";
            (sender as ASPxGridView).JSProperties["cp_successmes"] = "Updated Successfully.";
        }
        protected void gvStepMachine_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e) //
        {
            sdsMachine.DeleteParameters["DRecordID"].DefaultValue = e.Values["RecordID"].ToString();

            (sender as ASPxGridView).JSProperties["cp_redirect"] = "success";
            (sender as ASPxGridView).JSProperties["cp_successmes"] = "Updated Successfully.";
        }

        protected void gvStepManpower_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e) //
        {
            if (!IsPostBack) return;

            var StepSequence = (sender as ASPxGridView).GetMasterRowFieldValues("StepSequence").ToString();
            var StepCode = (sender as ASPxGridView).GetMasterRowFieldValues("StepCode").ToString();

            DataTable PKey = Gears.RetriveData2("SELECT SeriesNumber FROM IT.DocNumberSettings WHERE Module='PRDRTG'", HttpContext.Current.Session["ConnString"].ToString());

            sdsManpower.InsertParameters["IHeaderID"].DefaultValue = RecordID.Value;
            sdsManpower.InsertParameters["ISKUCode"].DefaultValue = SKUCode.Text;
            sdsManpower.InsertParameters["IStepSequence"].DefaultValue = StepSequence;
            sdsManpower.InsertParameters["IStepCode"].DefaultValue = StepCode;
            sdsManpower.InsertParameters["IDesignation"].DefaultValue = e.NewValues["Designation"].ToString();
            sdsManpower.InsertParameters["INoManpower"].DefaultValue = e.NewValues["NoManpower"].ToString();
            sdsManpower.InsertParameters["INoHour"].DefaultValue = e.NewValues["NoHour"].ToString();
            sdsManpower.InsertParameters["IStandardRate"].DefaultValue = e.NewValues["StandardRate"].ToString();
            sdsManpower.InsertParameters["IStandardRateUnit"].DefaultValue = e.NewValues["StandardRateUnit"].ToString();
            sdsManpower.InsertParameters["ICostPerUnit"].DefaultValue = e.NewValues["CostPerUnit"].ToString();

            (sender as ASPxGridView).JSProperties["cp_redirect"] = "success";
            (sender as ASPxGridView).JSProperties["cp_successmes"] = "Updated Successfully.";
        }
        protected void gvStepManpower_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e) //
        {
            if (!IsPostBack) return;

            var StepSequence = (sender as ASPxGridView).GetMasterRowFieldValues("StepSequence").ToString();
            var StepCode = (sender as ASPxGridView).GetMasterRowFieldValues("StepCode").ToString();

            sdsManpower.UpdateParameters["ParamURecordID"].DefaultValue = e.OldValues["RecordID"].ToString();

            sdsManpower.UpdateParameters["UStepSequence"].DefaultValue = StepSequence;
            sdsManpower.UpdateParameters["UStepCode"].DefaultValue = StepCode;
            sdsManpower.UpdateParameters["UDesignation"].DefaultValue = e.NewValues["Designation"].ToString();
            sdsManpower.UpdateParameters["UNoManpower"].DefaultValue = e.NewValues["NoManpower"].ToString();
            sdsManpower.UpdateParameters["UNoHour"].DefaultValue = e.NewValues["NoHour"].ToString();
            sdsManpower.UpdateParameters["UStandardRate"].DefaultValue = e.NewValues["StandardRate"].ToString();
            sdsManpower.UpdateParameters["UStandardRateUnit"].DefaultValue = e.NewValues["StandardRateUnit"].ToString();
            sdsManpower.UpdateParameters["UCostPerUnit"].DefaultValue = e.NewValues["CostPerUnit"].ToString();

            (sender as ASPxGridView).JSProperties["cp_redirect"] = "success";
            (sender as ASPxGridView).JSProperties["cp_successmes"] = "Updated Successfully.";
        }
        protected void gvStepManpower_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e) //
        {
            sdsManpower.DeleteParameters["DRecordID"].DefaultValue = e.Values["RecordID"].ToString();

            (sender as ASPxGridView).JSProperties["cp_redirect"] = "success";
            (sender as ASPxGridView).JSProperties["cp_successmes"] = "Updated Successfully.";
        }

        protected void masterGrid_BeforePerformDataSelect1(object sender, EventArgs e) //
        {
            sdsStepProcess1.SelectCommand = "SELECT A.RecordID, HeaderID, SKUCode, StepSequence, A.StepCode, B.Description AS StepDescription, " +
                                            "SequenceDay, A.IsBackFlush FROM Production.ProdRoutingStepPack A LEFT JOIN Masterfile.Step B ON A.StepCode = B.StepCode " +
                                            "WHERE SKUCode='" + SKUCode.Text + "' AND HeaderID='" + RecordID.Value + "' ORDER BY StepSequence ASC";
            sdsStepProcess1.DataBind();
        }
        protected void detailBOM_BeforePerformDataSelect1(object sender, EventArgs e) //
        {
            sdsBOM1.SelectCommand = "SELECT A.*, B.FullDesc AS Description FROM Production.ProdRoutingStepPackBOM A LEFT JOIN Masterfile.Item B " +
                                    "ON A.ItemCode = B.ItemCode WHERE SKUCode='" + (sender as ASPxGridView).GetMasterRowFieldValues("SKUCode").ToString() + "'" +
                                    "AND StepSequence='" + (sender as ASPxGridView).GetMasterRowFieldValues("StepSequence").ToString() + 
                                    "' AND HeaderID='" + (sender as ASPxGridView).GetMasterRowFieldValues("HeaderID").ToString() + "' AND StepCode='" + (sender as ASPxGridView).GetMasterRowFieldValues("StepCode").ToString() + "'";
            sdsBOM1.DataBind();
        }
        protected void detailMachine_BeforePerformDataSelect1(object sender, EventArgs e) //
        {
            sdsMachine1.SelectCommand = "SELECT * FROM Production.ProdRoutingStepPackMachine WHERE SKUCode='" + (sender as ASPxGridView).GetMasterRowFieldValues("SKUCode").ToString() + "'" +
                                        "AND StepSequence='" + (sender as ASPxGridView).GetMasterRowFieldValues("StepSequence").ToString() + 
                                        "' AND HeaderID='" + (sender as ASPxGridView).GetMasterRowFieldValues("HeaderID").ToString() + "' AND StepCode='" + (sender as ASPxGridView).GetMasterRowFieldValues("StepCode").ToString() + "'";
            sdsMachine1.DataBind();
        }
        protected void detailManpower_BeforePerformDataSelect1(object sender, EventArgs e) //
        {
            sdsManpower1.SelectCommand = "SELECT A.*, B.FullDesc AS SKUDescription FROM Production.ProdRoutingStepPackManpower A LEFT JOIN Masterfile.Item B " +
                                        "ON A.SKUCode = B.ItemCode WHERE SKUCode='" + (sender as ASPxGridView).GetMasterRowFieldValues("SKUCode").ToString() + "'" +
                                        "AND StepSequence='" + (sender as ASPxGridView).GetMasterRowFieldValues("StepSequence").ToString() + 
                                        "' AND HeaderID='" + (sender as ASPxGridView).GetMasterRowFieldValues("HeaderID").ToString() + "' AND StepCode='" + (sender as ASPxGridView).GetMasterRowFieldValues("StepCode").ToString() + "'";
            sdsManpower1.DataBind();
        }

        protected void gvStepProcess_RowInserting1(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e) //
        {
            if (!IsPostBack) return;

            sdsStepProcess1.InsertParameters["IHeaderID"].DefaultValue = RecordID.Value;
            sdsStepProcess1.InsertParameters["ISKUCode"].DefaultValue = SKUCode.Text;
            sdsStepProcess1.InsertParameters["IStepSequence"].DefaultValue = e.NewValues["StepSequence"].ToString();
            sdsStepProcess1.InsertParameters["IStepCode"].DefaultValue = e.NewValues["StepCode"].ToString();
            sdsStepProcess1.InsertParameters["IsBackFlush"].DefaultValue = Convert.ToBoolean(e.NewValues["IsBackFlush"]).ToString();

            (sender as ASPxGridView).JSProperties["cp_redirect"] = "success";
            (sender as ASPxGridView).JSProperties["cp_successmes"] = "Updated Successfully.";
        }
        protected void gvStepProcess_RowUpdating1(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e) //
        {
            if (!IsPostBack) return;

            sdsStepProcess1.UpdateParameters["ParamURecordID"].DefaultValue = e.OldValues["RecordID"].ToString();
            sdsStepProcess1.UpdateParameters["UStepSequence"].DefaultValue = e.NewValues["StepSequence"].ToString();
            sdsStepProcess1.UpdateParameters["UStepCode"].DefaultValue = e.NewValues["StepCode"].ToString();
            sdsStepProcess1.UpdateParameters["UIsBackFlush"].DefaultValue = Convert.ToBoolean(e.NewValues["IsBackFlush"]).ToString();

            (sender as ASPxGridView).JSProperties["cp_redirect"] = "success";
            (sender as ASPxGridView).JSProperties["cp_successmes"] = "Updated Successfully.";
        }
        protected void gvStepProcess_RowDeleting1(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e) //
        {
            sdsStepProcess1.DeleteParameters["DRecordID"].DefaultValue = e.Values["RecordID"].ToString();

            (sender as ASPxGridView).JSProperties["cp_redirect"] = "success";
            (sender as ASPxGridView).JSProperties["cp_successmes"] = "Updated Successfully.";
        }

        protected void gvStepBOM_RowInserting1(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e) //
        {
            if (!IsPostBack) return;

            var StepSequence = (sender as ASPxGridView).GetMasterRowFieldValues("StepSequence").ToString();
            var StepCode = (sender as ASPxGridView).GetMasterRowFieldValues("StepCode").ToString();

            sdsBOM1.InsertParameters["IHeaderID"].DefaultValue = RecordID.Value;
            sdsBOM1.InsertParameters["ISKUCode"].DefaultValue = SKUCode.Text;
            sdsBOM1.InsertParameters["IStepSequence"].DefaultValue = StepSequence;
            sdsBOM1.InsertParameters["IStepCode"].DefaultValue = StepCode;
            sdsBOM1.InsertParameters["IItemCode"].DefaultValue = e.NewValues["ItemCode"].ToString();
            sdsBOM1.InsertParameters["IUnit"].DefaultValue = e.NewValues["Unit"].ToString();
            sdsBOM1.InsertParameters["IConsumptionPerProduct"].DefaultValue = e.NewValues["ConsumptionPerProduct"].ToString();
            sdsBOM1.InsertParameters["ITotalConsumption"].DefaultValue = e.NewValues["TotalConsumption"].ToString();
            sdsBOM1.InsertParameters["IPercentageAllowance"].DefaultValue = e.NewValues["PercentageAllowance"].ToString();
            sdsBOM1.InsertParameters["IQtyAllowance"].DefaultValue = e.NewValues["QtyAllowance"].ToString();
            sdsBOM1.InsertParameters["IClientSuppliedMaterial"].DefaultValue = Convert.ToBoolean(e.NewValues["ClientSuppliedMaterial"]).ToString();
            sdsBOM1.InsertParameters["IEstimatedUnitCost"].DefaultValue = e.NewValues["EstimatedUnitCost"].ToString();
            sdsBOM1.InsertParameters["IStandardUsage"].DefaultValue = e.NewValues["StandardUsage"].ToString();
            sdsBOM1.InsertParameters["IStandardUsageUnit"].DefaultValue = e.NewValues["StandardUsageUnit"].ToString();
            sdsBOM1.InsertParameters["IRemarks"].DefaultValue = e.NewValues["Remarks"] == null ? "" : e.NewValues["Remarks"].ToString();

            (sender as ASPxGridView).JSProperties["cp_redirect"] = "success";
            (sender as ASPxGridView).JSProperties["cp_successmes"] = "Updated Successfully.";
        }
        protected void gvStepBOM_RowUpdating1(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e) //
        {
            if (!IsPostBack) return;

            var StepSequence = (sender as ASPxGridView).GetMasterRowFieldValues("StepSequence").ToString();
            var StepCode = (sender as ASPxGridView).GetMasterRowFieldValues("StepCode").ToString();

            sdsBOM1.UpdateParameters["ParamURecordID"].DefaultValue = e.OldValues["RecordID"].ToString();

            sdsBOM1.UpdateParameters["UStepSequence"].DefaultValue = StepSequence;
            sdsBOM1.UpdateParameters["UStepCode"].DefaultValue = StepCode;
            sdsBOM1.UpdateParameters["UItemCode"].DefaultValue = e.NewValues["ItemCode"].ToString();
            sdsBOM1.UpdateParameters["UUnit"].DefaultValue = e.NewValues["Unit"].ToString();
            sdsBOM1.UpdateParameters["UConsumptionPerProduct"].DefaultValue = e.NewValues["ConsumptionPerProduct"].ToString();
            sdsBOM1.UpdateParameters["UTotalConsumption"].DefaultValue = e.NewValues["TotalConsumption"].ToString();
            sdsBOM1.UpdateParameters["UPercentageAllowance"].DefaultValue = e.NewValues["PercentageAllowance"].ToString();
            sdsBOM1.UpdateParameters["UQtyAllowance"].DefaultValue = e.NewValues["QtyAllowance"].ToString();
            sdsBOM1.UpdateParameters["UClientSuppliedMaterial"].DefaultValue = Convert.ToBoolean(e.NewValues["ClientSuppliedMaterial"]).ToString();
            sdsBOM1.UpdateParameters["UEstimatedUnitCost"].DefaultValue = e.NewValues["EstimatedUnitCost"].ToString();
            sdsBOM1.UpdateParameters["UStandardUsage"].DefaultValue = e.NewValues["StandardUsage"].ToString();
            sdsBOM1.UpdateParameters["UStandardUsageUnit"].DefaultValue = e.NewValues["StandardUsageUnit"].ToString();
            sdsBOM1.UpdateParameters["URemarks"].DefaultValue = e.NewValues["Remarks"] == null ? "" : e.NewValues["Remarks"].ToString();

            (sender as ASPxGridView).JSProperties["cp_redirect"] = "success";
            (sender as ASPxGridView).JSProperties["cp_successmes"] = "Updated Successfully.";
        }

        protected void gvStepBOM_RowDeleting1(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e) //
        {
            sdsBOM1.DeleteParameters["DRecordID"].DefaultValue = e.Values["RecordID"].ToString();

            (sender as ASPxGridView).JSProperties["cp_redirect"] = "success";
            (sender as ASPxGridView).JSProperties["cp_successmes"] = "Updated Successfully.";
        }

        protected void gvStepMachine_RowInserting1(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e) //
        {
            if (!IsPostBack) return;

            var StepSequence = (sender as ASPxGridView).GetMasterRowFieldValues("StepSequence").ToString();
            var StepCode = (sender as ASPxGridView).GetMasterRowFieldValues("StepCode").ToString();

            sdsMachine1.InsertParameters["IHeaderID"].DefaultValue = RecordID.Value;
            sdsMachine1.InsertParameters["ISKUCode"].DefaultValue = SKUCode.Text;
            sdsMachine1.InsertParameters["IStepSequence"].DefaultValue = StepSequence;
            sdsMachine1.InsertParameters["IStepCode"].DefaultValue = StepCode;
            sdsMachine1.InsertParameters["IMachineType"].DefaultValue = e.NewValues["MachineType"].ToString();
            sdsMachine1.InsertParameters["ILocation"].DefaultValue = e.NewValues["Location"] == null ? "" : e.NewValues["Location"].ToString();
            sdsMachine1.InsertParameters["IMachineRun"].DefaultValue = e.NewValues["MachineRun"].ToString();
            sdsMachine1.InsertParameters["IUnit"].DefaultValue = e.NewValues["Unit"].ToString();
            sdsMachine1.InsertParameters["IMachineCapacityQty"].DefaultValue = e.NewValues["MachineCapacityQty"] == null ? "" : e.NewValues["MachineCapacityQty"].ToString();
            sdsMachine1.InsertParameters["IMachineCapacityUnit"].DefaultValue = e.NewValues["MachineCapacityUnit"] == null ? "" : e.NewValues["MachineCapacityUnit"].ToString();
            sdsMachine1.InsertParameters["ICostPerUnit"].DefaultValue = e.NewValues["CostPerUnit"].ToString();

            (sender as ASPxGridView).JSProperties["cp_redirect"] = "success";
            (sender as ASPxGridView).JSProperties["cp_successmes"] = "Updated Successfully.";
        }
        protected void gvStepMachine_RowUpdating1(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e) //
        {
            if (!IsPostBack) return;

            var StepSequence = (sender as ASPxGridView).GetMasterRowFieldValues("StepSequence").ToString();
            var StepCode = (sender as ASPxGridView).GetMasterRowFieldValues("StepCode").ToString();

            sdsMachine1.UpdateParameters["ParamURecordID"].DefaultValue = e.OldValues["RecordID"].ToString();

            sdsMachine1.UpdateParameters["UStepSequence"].DefaultValue = StepSequence;
            sdsMachine1.UpdateParameters["UStepCode"].DefaultValue = StepCode;
            sdsMachine1.UpdateParameters["UMachineType"].DefaultValue = e.NewValues["MachineType"].ToString();
            sdsMachine1.UpdateParameters["ULocation"].DefaultValue = e.NewValues["Location"] == null ? "" : e.NewValues["Location"].ToString();
            sdsMachine1.UpdateParameters["UMachineRun"].DefaultValue = e.NewValues["MachineRun"].ToString();
            sdsMachine1.UpdateParameters["UUnit"].DefaultValue = e.NewValues["Unit"].ToString();
            sdsMachine1.UpdateParameters["UMachineCapacityQty"].DefaultValue = e.NewValues["MachineCapacityQty"] == null ? "" : e.NewValues["MachineCapacityQty"].ToString();
            sdsMachine1.UpdateParameters["UMachineCapacityUnit"].DefaultValue = e.NewValues["MachineCapacityUnit"] == null ? "" : e.NewValues["MachineCapacityUnit"].ToString();
            sdsMachine1.UpdateParameters["UCostPerUnit"].DefaultValue = e.NewValues["CostPerUnit"].ToString();

            (sender as ASPxGridView).JSProperties["cp_redirect"] = "success";
            (sender as ASPxGridView).JSProperties["cp_successmes"] = "Updated Successfully.";
        }
        protected void gvStepMachine_RowDeleting1(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e) //
        {
            sdsMachine1.DeleteParameters["DRecordID"].DefaultValue = e.Values["RecordID"].ToString();

            (sender as ASPxGridView).JSProperties["cp_redirect"] = "success";
            (sender as ASPxGridView).JSProperties["cp_successmes"] = "Updated Successfully.";
        }

        protected void gvStepManpower_RowInserting1(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e) //
        {
            if (!IsPostBack) return;

            var StepSequence = (sender as ASPxGridView).GetMasterRowFieldValues("StepSequence").ToString();
            var StepCode = (sender as ASPxGridView).GetMasterRowFieldValues("StepCode").ToString();

            DataTable PKey = Gears.RetriveData2("SELECT SeriesNumber FROM IT.DocNumberSettings WHERE Module='PRDRTG'", HttpContext.Current.Session["ConnString"].ToString());

            sdsManpower1.InsertParameters["IHeaderID"].DefaultValue = RecordID.Value;
            sdsManpower1.InsertParameters["ISKUCode"].DefaultValue = SKUCode.Text;
            sdsManpower1.InsertParameters["IStepSequence"].DefaultValue = StepSequence;
            sdsManpower1.InsertParameters["IStepCode"].DefaultValue = StepCode;
            sdsManpower1.InsertParameters["IDesignation"].DefaultValue = e.NewValues["Designation"].ToString();
            sdsManpower1.InsertParameters["INoManpower"].DefaultValue = e.NewValues["NoManpower"].ToString();
            sdsManpower1.InsertParameters["INoHour"].DefaultValue = e.NewValues["NoHour"].ToString();
            sdsManpower1.InsertParameters["IStandardRate"].DefaultValue = e.NewValues["StandardRate"].ToString();
            sdsManpower1.InsertParameters["IStandardRateUnit"].DefaultValue = e.NewValues["StandardRateUnit"].ToString();
            sdsManpower1.InsertParameters["ICostPerUnit"].DefaultValue = e.NewValues["CostPerUnit"].ToString();

            (sender as ASPxGridView).JSProperties["cp_redirect"] = "success";
            (sender as ASPxGridView).JSProperties["cp_successmes"] = "Updated Successfully.";
        }
        protected void gvStepManpower_RowUpdating1(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e) //
        {
            if (!IsPostBack) return;

            var StepSequence = (sender as ASPxGridView).GetMasterRowFieldValues("StepSequence").ToString();
            var StepCode = (sender as ASPxGridView).GetMasterRowFieldValues("StepCode").ToString();

            sdsManpower1.UpdateParameters["ParamURecordID"].DefaultValue = e.OldValues["RecordID"].ToString();

            sdsManpower1.UpdateParameters["UStepSequence"].DefaultValue = StepSequence;
            sdsManpower1.UpdateParameters["UStepCode"].DefaultValue = StepCode;
            sdsManpower1.UpdateParameters["UDesignation"].DefaultValue = e.NewValues["Designation"].ToString();
            sdsManpower1.UpdateParameters["UNoManpower"].DefaultValue = e.NewValues["NoManpower"].ToString();
            sdsManpower1.UpdateParameters["UNoHour"].DefaultValue = e.NewValues["NoHour"].ToString();
            sdsManpower1.UpdateParameters["UStandardRate"].DefaultValue = e.NewValues["StandardRate"].ToString();
            sdsManpower1.UpdateParameters["UStandardRateUnit"].DefaultValue = e.NewValues["StandardRateUnit"].ToString();
            sdsManpower1.UpdateParameters["UCostPerUnit"].DefaultValue = e.NewValues["CostPerUnit"].ToString();

            (sender as ASPxGridView).JSProperties["cp_redirect"] = "success";
            (sender as ASPxGridView).JSProperties["cp_successmes"] = "Updated Successfully.";
        }
        protected void gvStepManpower_RowDeleting1(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e) //
        {
            sdsManpower1.DeleteParameters["DRecordID"].DefaultValue = e.Values["RecordID"].ToString();

            (sender as ASPxGridView).JSProperties["cp_redirect"] = "success";
            (sender as ASPxGridView).JSProperties["cp_successmes"] = "Updated Successfully.";
        }

        protected void gvOtherMaterial_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e) //
        {
            if (!IsPostBack) return;

            DataTable PKey = Gears.RetriveData2("SELECT SeriesNumber FROM IT.DocNumberSettings WHERE Module='PRDRTG'", HttpContext.Current.Session["ConnString"].ToString());

            sdsOtherMaterials.InsertParameters["IHeaderID"].DefaultValue = RecordID.Value;
            sdsOtherMaterials.InsertParameters["ISKUCode"].DefaultValue = SKUCode.Text;
            sdsOtherMaterials.InsertParameters["IItemCode"].DefaultValue = e.NewValues["ItemCode"].ToString();
            sdsOtherMaterials.InsertParameters["IStepCode"].DefaultValue = e.NewValues["StepCode"].ToString();
            sdsOtherMaterials.InsertParameters["IUnit"].DefaultValue = e.NewValues["Unit"].ToString();
            sdsOtherMaterials.InsertParameters["IConsumptionPerProduct"].DefaultValue = e.NewValues["ConsumptionPerProduct"].ToString();
            sdsOtherMaterials.InsertParameters["ITotalConsumption"].DefaultValue = e.NewValues["TotalConsumption"].ToString();
            sdsOtherMaterials.InsertParameters["IPercentageAllowance"].DefaultValue = e.NewValues["PercentageAllowance"].ToString();
            sdsOtherMaterials.InsertParameters["IQtyAllowance"].DefaultValue = e.NewValues["QtyAllowance"].ToString();
            sdsOtherMaterials.InsertParameters["IClientSuppliedMaterial"].DefaultValue = Convert.ToBoolean(e.NewValues["ClientSuppliedMaterial"]).ToString();
            sdsOtherMaterials.InsertParameters["IEstimatedUnitCost"].DefaultValue = e.NewValues["EstimatedUnitCost"].ToString();
            sdsOtherMaterials.InsertParameters["IStandardUsage"].DefaultValue = e.NewValues["StandardUsage"].ToString();
            sdsOtherMaterials.InsertParameters["IStandardUsageUnit"].DefaultValue = e.NewValues["StandardUsageUnit"].ToString();
            sdsOtherMaterials.InsertParameters["IRemarks"].DefaultValue = e.NewValues["Remarks"] == null ? "" : e.NewValues["Remarks"].ToString();

            (sender as ASPxGridView).JSProperties["cp_redirect"] = "success";
            (sender as ASPxGridView).JSProperties["cp_successmes"] = "Updated Successfully.";
        }
        protected void gvOtherMaterial_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e) //
        {
            if (!IsPostBack) return;

            sdsOtherMaterials.UpdateParameters["ParamURecordID"].DefaultValue = e.OldValues["RecordID"].ToString();

            sdsOtherMaterials.UpdateParameters["UStepSequence"].DefaultValue = e.NewValues["StepSequence"].ToString();
            sdsOtherMaterials.UpdateParameters["UStepCode"].DefaultValue = e.NewValues["StepCode"].ToString();
            sdsOtherMaterials.UpdateParameters["UUnit"].DefaultValue = e.NewValues["Unit"].ToString();
            sdsOtherMaterials.UpdateParameters["UConsumptionPerProduct"].DefaultValue = e.NewValues["ConsumptionPerProduct"].ToString();
            sdsOtherMaterials.UpdateParameters["UTotalConsumption"].DefaultValue = e.NewValues["TotalConsumption"].ToString();
            sdsOtherMaterials.UpdateParameters["UPercentageAllowance"].DefaultValue = e.NewValues["PercentageAllowance"].ToString();
            sdsOtherMaterials.UpdateParameters["UQtyAllowance"].DefaultValue = e.NewValues["QtyAllowance"].ToString();
            sdsOtherMaterials.UpdateParameters["UClientSuppliedMaterial"].DefaultValue = Convert.ToBoolean(e.NewValues["ClientSuppliedMaterial"]).ToString();
            sdsOtherMaterials.UpdateParameters["UEstimatedUnitCost"].DefaultValue = e.NewValues["EstimatedUnitCost"].ToString();
            sdsOtherMaterials.UpdateParameters["UStandardUsage"].DefaultValue = e.NewValues["StandardUsage"].ToString();
            sdsOtherMaterials.UpdateParameters["UStandardUsageUnit"].DefaultValue = e.NewValues["StandardUsageUnit"].ToString();
            sdsOtherMaterials.UpdateParameters["URemarks"].DefaultValue = e.NewValues["Remarks"] == null ? "" : e.NewValues["Remarks"].ToString();

            (sender as ASPxGridView).JSProperties["cp_redirect"] = "success";
            (sender as ASPxGridView).JSProperties["cp_successmes"] = "Updated Successfully.";
        }
        protected void gvOtherMaterial_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e) //
        {
            sdsOtherMaterials.DeleteParameters["DRecordID"].DefaultValue = e.Values["RecordID"].ToString();

            (sender as ASPxGridView).JSProperties["cp_redirect"] = "success";
            (sender as ASPxGridView).JSProperties["cp_successmes"] = "Updated Successfully.";
        }

        private string getPKey(string TableName, string PKey, string Keyid) // 04-21-2021 TA Added getUserID
        {
            string Conn = Session["Connstring"].ToString();
            string getSeriesNumber = "SELECT SeriesNumber FROM IT.DocNumberSettings WHERE Module='PRDRTG'";
            string checkIfAvailable = "SELECT " + Keyid + " FROM Production." + TableName + " WHERE " + PKey + "=";
            string Key = "";

            DataTable systemID = Gears.RetriveData2(getSeriesNumber, Conn); // Get DocNumberSettings SeriesNumber Value
            Key = systemID.Rows[0][0].ToString();

            DataTable checkID = Gears.RetriveData2(checkIfAvailable + Key, Conn); // Check if available

            // Loop until available key produce
            while (checkID.Rows.Count > 0)
            {
                Gears.RetriveData2("UPDATE IT.DocNumberSettings SET SeriesNumber+=1 WHERE Module='PRDRTG'", Conn);

                systemID = Gears.RetriveData2(getSeriesNumber, Conn); // Get DocNumberSettings SeriesNumber Value
                Key = systemID.Rows[0][0].ToString();

                checkID = Gears.RetriveData2(checkIfAvailable + Key, Conn); // Check if UserId is available
            }

            return Key;
        }

        protected void gvStepProcess_CustomErrorText(object sender, ASPxGridViewCustomErrorTextEventArgs e)
        {
            if (e.Exception is System.Data.SqlClient.SqlException)
            {
                if (e.ErrorText == "Text that should be replaced")
                {
                    e.ErrorText = "Text that should be shown";
                }
            }
        }
    }
}