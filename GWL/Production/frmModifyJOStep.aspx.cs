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
using GearsProduction;

namespace GWL
{
    public partial class frmModifyJOStep : System.Web.UI.Page
    {
        Boolean error = false;//Boolean for grid validation
        Boolean view = false;//Boolean for view state
        Boolean check = false;//Boolean for grid validation
        Boolean edit = false;   //Boolean for Edit Mode
        string filter = "";

        Entity.ModifyJOStep _Entity = new ModifyJOStep();

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

            if (referer == null && Common.Common.SystemSetting("URLCHECK", Session["ConnString"].ToString()) != "NO")
            {
                Response.Redirect("~/error.aspx");
            }

            txtDocNumber.Value = Request.QueryString["docnumber"].ToString();

            sdsSteps.SelectCommand =  "SELECT DISTINCT StepCode, Description FROM Masterfile.Step WHERE ISNULL(IsInactive,0) = 0 " +
                (aglType.Text == "ADD STEP" ? "" : " AND StepCode IN (SELECT DISTINCT StepCode FROM Production.JOStepPlanning WHERE DocNumber = '" + aglRefJO.Text + "')");
            sdsSteps.DataBind();


            if (!IsPostBack)
            {
                Session["FilterExpression"] = null;
                Session["JODocNumber"] = null;
                Session["JORecordID"] = null;
                Session["StepQuery"] = null;
                popup.ShowOnPageLoad = false;

                _Entity.getdata(txtDocNumber.Text, Session["ConnString"].ToString());
                dtpDocDate.Text = Convert.ToDateTime(_Entity.DocDate.ToString()).ToShortDateString();
                aglType.Value = _Entity.Type.ToString();
                aglRefJO.Text = _Entity.ReferenceJO.ToString();
                //sdsSteps.SelectCommand = "SELECT DISTINCT StepCode, Description FROM Masterfile.Step WHERE ISNULL(IsInactive,0) = 0 "
                //       + " AND StepCode IN (SELECT DISTINCT StepCode FROM Production.JOStepPlanning WHERE DocNumber = '" + aglRefJO.Text + "')";
                sdsSteps.SelectCommand = "SELECT DISTINCT StepCode, Description FROM Masterfile.Step WHERE ISNULL(IsInactive,0) = 0 " +
                    (aglType.Text == "ADD STEP" ? "" : " AND StepCode IN (SELECT DISTINCT StepCode FROM Production.JOStepPlanning WHERE DocNumber = '" + aglRefJO.Text + "')");
                sdsSteps.DataBind(); 
                aglStep.Text = _Entity.StepCode.ToString();
                txtSequence.Value = _Entity.SequenceNo.ToString();
                speWorkOrderPrice.Value = _Entity.WorkOrderPrice.ToString();
                speWorkOrderQty.Value = _Entity.WorkOrderQty.ToString();
                speEstWOPrice.Value = _Entity.EstWOPrice.ToString();
                dtpWODate.Text = String.IsNullOrEmpty(_Entity.WorkOrderDate.ToString()) ? null : Convert.ToDateTime(_Entity.WorkOrderDate.ToString()).ToShortDateString();
                dtpDateCommitted.Text = String.IsNullOrEmpty(_Entity.DateCommitted.ToString()) ? null : Convert.ToDateTime(_Entity.DateCommitted.ToString()).ToShortDateString();
                aglWorkCenter.Value = _Entity.WorkCenter.ToString();
                txtOverhead.Value = _Entity.Overhead.ToString();
                chkIsInhouse.Value = _Entity.IsInHouse;
                chkPreProduction.Value = _Entity.PreProd;
                aglReasons.Value = _Entity.Reason.ToString();
                txtCustomer.Value = _Entity.Customer.ToString();
                memRemarks.Text = _Entity.Remarks.ToString();
                dtpSODueDate.Text = String.IsNullOrEmpty(_Entity.SODueDate.ToString()) ? null : Convert.ToDateTime(_Entity.SODueDate.ToString()).ToShortDateString();

                txtHField1.Value = _Entity.Field1.ToString();
                txtHField2.Value = _Entity.Field2.ToString();
                txtHField3.Value = _Entity.Field3.ToString();
                txtHField4.Value = _Entity.Field4.ToString();
                txtHField5.Value = _Entity.Field5.ToString();
                txtHField6.Value = _Entity.Field6.ToString();
                txtHField7.Value = _Entity.Field7.ToString();
                txtHField8.Value = _Entity.Field8.ToString();
                txtHField9.Value = _Entity.Field9.ToString();
                txtRecordID.Value = _Entity.RefRecordID.ToString();

                txtHAddedBy.Text = _Entity.AddedBy;
                txtHAddedDate.Text = _Entity.AddedDate;
                txtHLastEditedBy.Text = _Entity.LastEditedBy;
                txtHLastEditedDate.Text = _Entity.LastEditedDate;
                txtHApprovedBy.Text = _Entity.ApprovedBy;
                txtHApprovedDate.Text = _Entity.ApprovedDate;
                txtHCancelledBy.Text = _Entity.CancelledBy;
                txtHCancelledDate.Text = _Entity.CancelledDate;

                //V=View, E=Edit, N=New
                updateBtn.Text = "Add";
                frmlayout1.FindItemOrGroupByName("ReferenceTransaction").ClientVisible = true;
                switch (Request.QueryString["entry"].ToString())
                {
                    case "N":
                        if (!String.IsNullOrEmpty(_Entity.LastEditedBy))
                        {
                            updateBtn.Text = "Update";
                        }
                        else
                        {
                            frmlayout1.FindItemOrGroupByName("ReferenceTransaction").ClientVisible = false;
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

                gvRef.DataSourceID = "odsReference";
                this.gvRef.Columns["CommandString"].Width = 0;
                this.gvRef.Columns["RCommandString"].Width = 0;

                CheckType();
            }
        }
        #endregion

        #region Validation
        private void Validate()
        {
            GearsLibrary.Gears.GearsParameter gparam = new Gears.GearsParameter();
            gparam._DocNo = _Entity.DocNumber;
            gparam._UserId = Session["Userid"].ToString();
            gparam._TransType = "PRDMOD";
            gparam._Connection = Session["ConnString"].ToString();
            string strresult = GearsProduction.GProduction.ModifyJOStep_Validate(gparam);
            if (strresult != " ")
            {
                cp.JSProperties["cp_valmsg"] = strresult;
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
        protected void CheckboxLoad(object sender, EventArgs e)//Control for all lookup in header
        {
            ASPxCheckBox check = sender as ASPxCheckBox;
            check.Enabled = !view;
            check.ReadOnly = view;
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

            if (Request.QueryString["entry"] == "N" || Request.QueryString["entry"] == "E")
            {
                if (e.ButtonType == ColumnCommandButtonType.New || e.ButtonType == ColumnCommandButtonType.Delete)
                {
                    e.Visible = true;
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
       
        #region Callback functions(Inserting/Updating/Deleting/Validating)
        protected void cp_Callback(object sender, CallbackEventArgsBase e)
        {
            _Entity.Connection = Session["ConnString"].ToString();
            _Entity.DocNumber = txtDocNumber.Value.ToString();
            _Entity.DocDate = dtpDocDate.Text;
            _Entity.Type = String.IsNullOrEmpty(aglType.Text) ? null : aglType.Value.ToString();
            _Entity.ReferenceJO = String.IsNullOrEmpty(aglRefJO.Text) ? null : aglRefJO.Text;
            _Entity.StepCode = String.IsNullOrEmpty(aglStep.Text) ? null : aglStep.Text;
            _Entity.SequenceNo = txtSequence.Text;
            _Entity.WorkOrderPrice = String.IsNullOrEmpty(speWorkOrderPrice.Text) ? 0 : Convert.ToDecimal(speWorkOrderPrice.Value.ToString());
            _Entity.WorkOrderQty = String.IsNullOrEmpty(speWorkOrderQty.Text) ? 0 : Convert.ToDecimal(speWorkOrderQty.Value.ToString());
            _Entity.EstWOPrice = String.IsNullOrEmpty(speEstWOPrice.Text) ? 0 : Convert.ToDecimal(speEstWOPrice.Value.ToString());
            _Entity.WorkOrderDate = dtpWODate.Text;
            _Entity.DateCommitted = dtpDateCommitted.Text;
            _Entity.WorkCenter = String.IsNullOrEmpty(aglWorkCenter.Text) ? null : aglWorkCenter.Value.ToString();
            _Entity.Overhead = txtOverhead.Text;
            _Entity.IsInHouse = Convert.ToBoolean(chkIsInhouse.Value.ToString());
            _Entity.PreProd = Convert.ToBoolean(chkPreProduction.Value.ToString());
            _Entity.Customer = txtCustomer.Text;
            _Entity.Reason = String.IsNullOrEmpty(aglReasons.Text) ? null : aglReasons.Value.ToString();
            _Entity.SODueDate = dtpSODueDate.Text;
            _Entity.Remarks = memRemarks.Text;
            _Entity.RefRecordID = txtRecordID.Text.Trim();
            
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

            string param = e.Parameter.Split('|')[0];

            switch (param)
            {
                case "Add":

                    string strError = Functions.Submitted(_Entity.DocNumber, "Production.ModifyJOStep", 2, Session["ConnString"].ToString());
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
                        _Entity.UpdateData(_Entity);

                        Validate();

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
                    
                    string strError1 = Functions.Submitted(_Entity.DocNumber, "Production.ModifyJOStep", 2, Session["ConnString"].ToString());
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
                        _Entity.LastEditedBy = Session["userid"].ToString();
                        _Entity.UpdateData(_Entity);

                        Validate();

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
                    break;

                case "CallbackType":
                    CheckType();
                    ResetFields();
                    break;

                case "CallbackReferenceJO":
                                        
                    string docnum = e.Parameter.Split('|')[1];
                    if (!String.IsNullOrEmpty(docnum))
                    {
                        string recid = "";
                        string docnumber = "";

                        docnumber = docnum;
                        recid = e.Parameter.Split('|')[2];
                        Session["JODocNumber"] = docnumber;
                        Session["JORecordID"] = recid;
                        txtRecordID.Text = recid.Trim();
                    }

                    ReferenceJOEffects();
                    CheckType();

                    DataTable data = Gears.RetriveData2("SELECT '('+ RTRIM(LTRIM(CustomerCode)) +') ' + Name AS Customer, ISNULL(DueDate,'') AS DueDate, ISNULL(SODueDate,'') AS SODueDate  FROM Production.JobOrder A INNER JOIN "
                                   + " Masterfile.BPCustomerInfo B ON A.CustomerCode = B.BizPartnerCode WHERE A.DocNumber = '" + Session["JODocNumber"].ToString() + "'", Session["ConnString"].ToString());

                    if (data.Rows.Count != 0)
                    {
                        txtCustomer.Text = data.Rows[0]["Customer"].ToString();
                        dtpSODueDate.Text = String.IsNullOrEmpty(data.Rows[0]["SODueDate"].ToString()) ? "" : Convert.ToDateTime(data.Rows[0]["SODueDate"].ToString()).ToShortDateString();
                    }
                    sdsSteps.SelectCommand =  "SELECT DISTINCT StepCode, Description FROM Masterfile.Step WHERE ISNULL(IsInactive,0) = 0 " +
                        (aglType.Text == "ADD STEP" ? "" : " AND StepCode IN (SELECT DISTINCT StepCode FROM Production.JOStepPlanning WHERE DocNumber = '" + aglRefJO.Text + "')");
                    sdsSteps.DataBind();
                    aglStep.DataBind();
                    break;

                case "CallbackStep":

                    string step = e.Parameter.Split('|')[1];

                    DataTable stepdata = Gears.RetriveData2("SELECT DISTINCT StepCode, Description, ISNULL(OverheadCode,'') AS Overhead, "
                                    + " CASE WHEN ISNULL(IsInhouse,0) = 1 THEN 'TRUE' ELSE 'FALSE' END AS IsInhouse, "
                                    + " CASE WHEN ISNULL(IsPreProductionStep,0) = 1 THEN 'TRUE' ELSE 'FALSE' END AS PreProd  "
                                    + " FROM Masterfile.Step WHERE StepCode = '" + step + "'", Session["ConnString"].ToString());

                    if (stepdata.Rows.Count != 0)
                    {
                        if (stepdata.Rows[0]["PreProd"].ToString() == "TRUE")
                        {
                            chkPreProduction.Checked = true;
                        }
                        else
                        {
                            chkPreProduction.Checked = false;
                        }

                        if (stepdata.Rows[0]["IsInhouse"].ToString() == "TRUE")
                        {
                            chkIsInhouse.Checked = true;
                        }
                        else
                        {
                            chkIsInhouse.Checked = false;
                        }

                        txtOverhead.Text = stepdata.Rows[0]["Overhead"].ToString();
                    }

                    CheckType();

                    break;
            }
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
        }
        #endregion
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
            //sdsType.ConnectionString = Session["ConnString"].ToString();
            //sdsReferenceJO.ConnectionString = Session["ConnString"].ToString();
            //sdsSteps.ConnectionString = Session["ConnString"].ToString();
            //sdsWorkCenter.ConnectionString = Session["ConnString"].ToString();
        }

        protected void dtpDateCommitted_Init(object sender, EventArgs e)
        {
            if (Request.QueryString["entry"] == "N")
            {
                dtpDateCommitted.Date = DateTime.Now;
            }
        }
        protected void aglRefJO_Init(object sender, EventArgs e)
        {
            if (Request.QueryString["entry"].ToString() == "V" || Request.QueryString["entry"].ToString() == "D")
            {
                sdsReferenceJO.SelectCommand = "SELECT  A.RecordID, ISNULL(A.DocNumber,'') AS DocNumber, ISNULL(Sequence,0) AS Sequence, ISNULL(StepCode,'') AS StepCode, ISNULL(WorkCenter,'') AS WorkCenter, ISNULL(Overhead,'') AS Overhead, ISNULL(InQty,0) AS InQty, ISNULL(OutQty,0) AS OutQty, ISNULL(AdjQty,0) AS AdjQty, ISNULL(WorkOrderQty,0) AS WorkOrderQty, "
            + " ISNULL(WorkOrderPrice,0) AS WorkOrderPrice, ISNULL(WorkOrderDate,'') AS WorkOrderDate, ISNULL(DateCommitted,NULL) AS DateCommitted, ISNULL(Remarks,'') AS Remarks, ISNULL(Name + ' (' + CustomerCode + ')','') AS CustomerCode, ISNULL(SODueDate,NULL) AS SODueDate, ISNULL(IsInhouse,0) AS IsInhouse, ISNULL(PreProd,0) AS PreProd  "
            + " FROM Production.JOStepPlanning A INNER JOIN Production.JobOrder B ON A.DocNumber = B.DocNumber  LEFT JOIN Masterfile.BPCustomerInfo C ON B.CustomerCode = C.BizPartnerCode ";
                aglRefJO.DataSource = "sdsReferenceJO";
                if (aglRefJO.DataSourceID != null)
                {
                    aglRefJO.DataSourceID = null;
                }
                aglRefJO.DataBind();
            }
            else
            {
                aglRefJO.DataSourceID = sdsReferenceJO.ID;
                aglRefJO.DataBind();
            }
        }

        //protected void aglStep_Init(object sender, EventArgs e)
        //{
        //    if (Session["JODocNumber"] != null)
        //    {
        //        Session["StepQuery"] = null;
        //        Session["StepQuery"] = "SELECT DISTINCT StepCode, Description FROM Masterfile.Step WHERE ISNULL(IsInactive,0) = 0 "
        //                    + " AND StepCode NOT IN (SELECT DISTINCT StepCode FROM Production.JOStepPlanning WHERE DocNumber = '" + Session["JODocNumber"].ToString() + "')";
        //        sdsSteps.SelectCommand = Session["StepQuery"].ToString();
        //        aglStep.DataBind();
        //    }
        //}

        protected void CheckType()
        {
            //if (String.IsNullOrEmpty(aglType.Text))
            //{
            //    aglRefJO.Enabled = false;
            //    aglRefJO.DropDownButton.Enabled = false;
            //}
            //else
            //{
            //    aglRefJO.Enabled = true;
            //    aglRefJO.DropDownButton.Enabled = true;
            //}

            if (!String.IsNullOrEmpty(aglType.Text))
            {
                switch (aglType.Value.ToString())
                {
                    case "A":
                        aglStep.ClientEnabled = true;
                        txtSequence.ClientEnabled = true;
                        speWorkOrderPrice.ClientEnabled = true;
                        speWorkOrderQty.ClientEnabled = true;
                        speEstWOPrice.ClientEnabled = true;
                        aglWorkCenter.ClientEnabled = true;
                        dtpWODate.ClientEnabled = true;
                        txtOverhead.ClientEnabled = false;
                        dtpDateCommitted.ClientEnabled = true;
                        dtpSODueDate.ClientEnabled = false;
                        break;


                    case "S":
                        aglStep.ClientEnabled = false;
                        txtSequence.ClientEnabled = false;
                        speWorkOrderPrice.ClientEnabled = true;
                        speWorkOrderQty.ClientEnabled = true;
                        speEstWOPrice.ClientEnabled = true;
                        aglWorkCenter.ClientEnabled = true;
                        dtpWODate.ClientEnabled = true;
                        txtOverhead.ClientEnabled = false;
                        dtpDateCommitted.ClientEnabled = true;
                        dtpSODueDate.ClientEnabled = false;
                        break;

                    case "D":
                        aglStep.ClientEnabled = false;
                        txtSequence.ClientEnabled = false;
                        speWorkOrderPrice.ClientEnabled = false;
                        speWorkOrderQty.ClientEnabled = true;
                        speEstWOPrice.ClientEnabled = false;
                        aglWorkCenter.ClientEnabled = false;
                        txtOverhead.ClientEnabled = false;
                        dtpDateCommitted.ClientEnabled = false;
                        dtpWODate.ClientEnabled = false;
                        dtpSODueDate.ClientEnabled = false;
                        break;
                }
            }
        }

        protected void ReferenceJOEffects()
        {
            DataTable getdata = Gears.RetriveData2("SELECT A.DocNumber, A.Sequence, C.StepCode, UPPER(RTRIM(LTRIM(A.WorkCenter))) AS WorkCenter, A.Overhead, A.InQty, A.OutQty, A.AdjQty, A.WorkOrderQty, "
                            + " A.WorkOrderPrice, A.DateCommitted, Remarks, ISNULL(A.EstWorkOrderPrice,0) AS EstWOPrice, A.WorkOrderDate, "
                            + " CASE WHEN ISNULL(A.IsInhouse,0) = 1 THEN 'TRUE' ELSE 'FALSE' END AS IsInhouse, "
                            + " CASE WHEN ISNULL(A.PreProd,0) = 1 THEN 'TRUE' ELSE 'FALSE' END AS PreProd, B.TotalJOQty AS WorkOrderQty  "
                            + " FROM Production.JOStepPlanning A INNER JOIN Production.JobOrder B ON "
                            + " A.DocNumber = B.DocNumber LEFT JOIN Masterfile.Step C ON A.StepCode = C.StepCode"
                            + " WHERE A.DocNumber = '" + Session["JODocNumber"].ToString() + "' "
                            + " AND A.RecordID = '" + Session["JORecordID"].ToString() + "'", Session["ConnString"].ToString());

            if (!String.IsNullOrEmpty(aglType.Text))
            {
                switch (aglType.Value.ToString())
                {
                    case "A":
                        Session["StepQuery"] = null;
                        Session["StepQuery"] = "SELECT DISTINCT StepCode, Description FROM Masterfile.Step WHERE ISNULL(IsInactive,0) = 0 ";
                        sdsSteps.SelectCommand = Session["StepQuery"].ToString();
                        sdsSteps.DataBind();

                        aglStep.Text = "";
                        txtSequence.Text = "";
                        speWorkOrderPrice.Text = "0.00";
                        speWorkOrderQty.Text = getdata.Rows[0]["WorkOrderQty"].ToString();
                        speEstWOPrice.Text = "0.00";
                        aglWorkCenter.Text = "";
                        txtOverhead.Text = "";
                        dtpWODate.Text = DateTime.Now.ToShortDateString();
                        dtpDateCommitted.Text = DateTime.Now.ToShortDateString();
                        chkPreProduction.Checked = false;
                        chkIsInhouse.Checked = false;

                        break;
                    case "S":
                        Session["StepQuery"] = null;
                        Session["StepQuery"] = "SELECT DISTINCT StepCode, Description FROM Masterfile.Step WHERE ISNULL(IsInactive,0) = 0 "
                                    + " AND StepCode IN (SELECT DISTINCT StepCode FROM Production.JOStepPlanning WHERE DocNumber = '" + Session["JODocNumber"].ToString() + "')";
                        sdsSteps.SelectCommand = Session["StepQuery"].ToString();
                        sdsSteps.DataBind();

                        aglStep.Text = getdata.Rows[0]["StepCode"].ToString();
                        txtSequence.Text = getdata.Rows[0]["Sequence"].ToString();
                        speWorkOrderPrice.Text = "0.00";
                        speWorkOrderQty.Text = getdata.Rows[0]["WorkOrderQty"].ToString();
                        speEstWOPrice.Text = getdata.Rows[0]["EstWOPrice"].ToString();
                        aglWorkCenter.Text = getdata.Rows[0]["WorkCenter"].ToString();
                        txtOverhead.Text = getdata.Rows[0]["Overhead"].ToString();
                        dtpDateCommitted.Text = String.IsNullOrEmpty(getdata.Rows[0]["DateCommitted"].ToString()) ? "" : Convert.ToDateTime(getdata.Rows[0]["DateCommitted"].ToString()).ToShortDateString();
                        dtpWODate.Text = String.IsNullOrEmpty(getdata.Rows[0]["WorkOrderDate"].ToString()) ? "" : Convert.ToDateTime(getdata.Rows[0]["WorkOrderDate"].ToString()).ToShortDateString();
                        
                        if (getdata.Rows[0]["PreProd"].ToString() == "TRUE")
                        {
                            chkPreProduction.Checked = true;
                        }
                        else
                        {
                            chkPreProduction.Checked = false;
                        }

                        if (getdata.Rows[0]["IsInhouse"].ToString() == "TRUE")
                        {
                            chkIsInhouse.Checked = true;
                        }
                        else
                        {
                            chkIsInhouse.Checked = false;
                        }

                        break;
                    case "D":
                        Session["StepQuery"] = null;
                        Session["StepQuery"] = "SELECT DISTINCT StepCode, Description FROM Masterfile.Step WHERE ISNULL(IsInactive,0) = 0 "
                                    + " AND StepCode IN (SELECT DISTINCT StepCode FROM Production.JOStepPlanning WHERE DocNumber = '" + Session["JODocNumber"].ToString() + "')";
                        sdsSteps.SelectCommand = Session["StepQuery"].ToString();
                        sdsSteps.DataBind();

                        aglStep.Text = getdata.Rows[0]["StepCode"].ToString();
                        txtSequence.Text = getdata.Rows[0]["Sequence"].ToString();
                        speWorkOrderPrice.Text = getdata.Rows[0]["WorkOrderPrice"].ToString();
                        speWorkOrderQty.Text = getdata.Rows[0]["WorkOrderQty"].ToString();
                        speEstWOPrice.Text = getdata.Rows[0]["EstWOPrice"].ToString();
                        aglWorkCenter.Text = getdata.Rows[0]["WorkCenter"].ToString();
                        txtOverhead.Text = getdata.Rows[0]["Overhead"].ToString();
                        dtpDateCommitted.Text = String.IsNullOrEmpty(getdata.Rows[0]["DateCommitted"].ToString()) ? "" : Convert.ToDateTime(getdata.Rows[0]["DateCommitted"].ToString()).ToShortDateString();
                        dtpWODate.Text = String.IsNullOrEmpty(getdata.Rows[0]["WorkOrderDate"].ToString()) ? "" : Convert.ToDateTime(getdata.Rows[0]["WorkOrderDate"].ToString()).ToShortDateString();
                        
                        if (getdata.Rows[0]["PreProd"].ToString() == "TRUE")
                        {
                            chkPreProduction.Checked = true;
                        }
                        else
                        {
                            chkPreProduction.Checked = false;
                        }

                        if (getdata.Rows[0]["IsInhouse"].ToString() == "TRUE")
                        {
                            chkIsInhouse.Checked = true;
                        }
                        else
                        {
                            chkIsInhouse.Checked = false;
                        }

                        break;
                }
            }
        }

        protected void ResetFields()
        {
            aglRefJO.Text = "";
            aglStep.Text = "";
            txtSequence.Text = "";
            dtpSODueDate.Text = "";
            txtCustomer.Text = "";
            speWorkOrderPrice.Text = "0.00";
            speWorkOrderQty.Text = "0.00";
            speEstWOPrice.Text = "0.00";
            aglWorkCenter.Text = "";
            txtOverhead.Text = "";
            dtpDateCommitted.Text = "";
            dtpWODate.Text = "";
            chkIsInhouse.Checked = false;
            chkPreProduction.Checked = false;
        }
    }
}