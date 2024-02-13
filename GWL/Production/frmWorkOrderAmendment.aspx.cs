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
    public partial class frmWorkOrderAmendment : System.Web.UI.Page
    {
        Boolean error = false;//Boolean for grid validation
        Boolean view = false;//Boolean for view state
        Boolean check = false;//Boolean for grid validation
        Boolean edit = false;   //Boolean for Edit Mode
        string filter = "";

        Entity.WorkOrderAmendment _Entity = new WorkOrderAmendment();

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

            if (!IsPostBack)
            {
                Session["FilterExpression"] = null;
                Session["JODocNumber"] = null;
                Session["JOSequence"] = null;
                Session["JOStep"] = null;
                popup.ShowOnPageLoad = false;


                _Entity.getdata(txtDocNumber.Text, Session["ConnString"].ToString());
                dtpDocDate.Text = Convert.ToDateTime(_Entity.DocDate.ToString()).ToShortDateString();
                aglReferenceJO.Text = _Entity.ReferenceJO.ToString();
                txtStep.Value = _Entity.StepCode.ToString();
                txtWorkCenter.Value = _Entity.WorkCenter.ToString();
                aglReason.Value = _Entity.Reason.ToString();
                memRemarks.Value = _Entity.Remarks.ToString();
                
                speOldWorkOrderQty.Value = _Entity.OldWOQty.ToString();
                speOldWorkOrderPrice.Value = _Entity.OldWOPrice.ToString();
                dtpOldCommitment.Text = String.IsNullOrEmpty(_Entity.OldCommitmentDate.ToString()) ? null : Convert.ToDateTime(_Entity.OldCommitmentDate.ToString()).ToShortDateString();
                dtpOldWODate.Text = String.IsNullOrEmpty(_Entity.OldWODate.ToString()) ? null : Convert.ToDateTime(_Entity.OldWODate.ToString()).ToShortDateString();
                
                speNewWorkOrderQty.Value = _Entity.NewWOQty.ToString();
                speNewWorkOrderPrice.Value = _Entity.NewWOPrice.ToString();
                dtpNewCommitment.Text = String.IsNullOrEmpty(_Entity.NewCommitmentDate.ToString()) ? null : Convert.ToDateTime(_Entity.NewCommitmentDate.ToString()).ToShortDateString();
                dtpNewWODate.Text = String.IsNullOrEmpty(_Entity.NewWODate.ToString()) ? null : Convert.ToDateTime(_Entity.NewWODate.ToString()).ToShortDateString();
                
                txtHField1.Value = _Entity.Field1.ToString();
                txtHField2.Value = _Entity.Field2.ToString();
                txtHField3.Value = _Entity.Field3.ToString();
                txtHField4.Value = _Entity.Field4.ToString();
                txtHField5.Value = _Entity.Field5.ToString();
                txtHField6.Value = _Entity.Field6.ToString();
                txtHField7.Value = _Entity.Field7.ToString();
                txtHField8.Value = _Entity.Field8.ToString();
                txtHField9.Value = _Entity.Field9.ToString();
                txtRecordID.Text = _Entity.RefRecordID.ToString();

                txtHAddedBy.Text = _Entity.AddedBy;
                txtHAddedDate.Text = _Entity.AddedDate;
                txtHLastEditedBy.Text = _Entity.LastEditedBy;
                txtHLastEditedDate.Text = _Entity.LastEditedDate;
                txtHApprovedBy.Text = _Entity.ApprovedBy;
                txtHApprovedDate.Text = _Entity.ApprovedDate;
                txtHCancelledBy.Text = _Entity.CancelledBy;
                txtHCancelledDate.Text = _Entity.CancelledDate;

                //V=View, E=Edit, N=New
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
                            updateBtn.Text = "Add";
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
            }
        }
        #endregion

        #region Validation
        private void Validate()
        {
            GearsLibrary.Gears.GearsParameter gparam = new Gears.GearsParameter();
            gparam._DocNo = _Entity.DocNumber;
            gparam._UserId = Session["Userid"].ToString();
            gparam._TransType = "PRDWOA";
            gparam._Connection = Session["ConnString"].ToString();
            string strresult = GearsProduction.GProduction.WorkOrderAmendment_Validate(gparam);
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
            _Entity.ReferenceJO = String.IsNullOrEmpty(aglReferenceJO.Text) ? null : aglReferenceJO.Text;
            _Entity.StepCode = txtStep.Text;
            _Entity.WorkCenter = txtWorkCenter.Text; 
            _Entity.Reason = String.IsNullOrEmpty(aglReason.Text) ? null : aglReason.Value.ToString();
            _Entity.Remarks = memRemarks.Text;

            _Entity.OldWOQty = String.IsNullOrEmpty(speOldWorkOrderQty.Text) ? 0 : Convert.ToDecimal(speOldWorkOrderQty.Value.ToString());
            _Entity.OldWOPrice = String.IsNullOrEmpty(speOldWorkOrderPrice.Text) ? 0 : Convert.ToDecimal(speOldWorkOrderPrice.Value.ToString());
            _Entity.OldCommitmentDate = dtpOldCommitment.Text;
            _Entity.OldWODate = dtpOldWODate.Text;
            _Entity.NewWOQty = String.IsNullOrEmpty(speNewWorkOrderQty.Text) ? 0 : Convert.ToDecimal(speNewWorkOrderQty.Value.ToString());
            _Entity.NewWOPrice = String.IsNullOrEmpty(speNewWorkOrderPrice.Text) ? 0 : Convert.ToDecimal(speNewWorkOrderPrice.Value.ToString());
            _Entity.NewCommitmentDate = dtpNewCommitment.Text;
            _Entity.NewWODate = dtpNewWODate.Text;
            
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

            _Entity.RefRecordID = txtRecordID.Text.Trim();

            string param = e.Parameter.Split('|')[0];

            switch (param)
            {
                case "Add":

                    string strError = Functions.Submitted(_Entity.DocNumber, "Production.WorkOrderAmendment", 2, Session["ConnString"].ToString());
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

                    string strError1 = Functions.Submitted(_Entity.DocNumber, "Production.WorkOrderAmendment", 2, Session["ConnString"].ToString());
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

                case "CallbackReferenceJO":

                    if (!String.IsNullOrEmpty(aglReferenceJO.Text))
                    {
                        string recid = "";
                        string docnumber = "";

                        docnumber = e.Parameter.Split('|')[1];
                        recid = e.Parameter.Split('|')[2];
                        txtRecordID.Text = recid.Trim();

                        DataTable getdata = Gears.RetriveData2("SELECT A.StepCode, A.Sequence, A.WorkCenter, A.WorkOrderPrice, A.WorkOrderQty, A.DateCommitted, A.WorkOrderDate "
                                    + " FROM Production.JOStepPlanning A INNER JOIN Production.JobOrder B ON A.DocNumber = B.DocNumber "
                                    + " WHERE A.DocNumber = '" + docnumber.Trim() + "' AND A.RecordID = '" + recid.Trim() + "'", Session["ConnString"].ToString());

                        txtStep.Text = getdata.Rows[0]["StepCode"].ToString();
                        speOldWorkOrderPrice.Text = getdata.Rows[0]["WorkOrderPrice"].ToString();
                        speOldWorkOrderQty.Text = getdata.Rows[0]["WorkOrderQty"].ToString();
                        txtWorkCenter.Text = getdata.Rows[0]["WorkCenter"].ToString();
                        dtpOldCommitment.Text = String.IsNullOrEmpty(getdata.Rows[0]["DateCommitted"].ToString()) ? null : Convert.ToDateTime(getdata.Rows[0]["DateCommitted"].ToString()).ToShortDateString();
                        dtpOldWODate.Text = String.IsNullOrEmpty(getdata.Rows[0]["WorkOrderDate"].ToString()) ? null : Convert.ToDateTime(getdata.Rows[0]["WorkOrderDate"].ToString()).ToShortDateString();
                    }
                    break;

                case "CallbackWOP":
                    DataTable ask = new DataTable();
                    ask = Gears.RetriveData2("SELECT DISTINCT 'TRUE' AS Checking FROM Masterfile.Step WHERE StepCode ='"+ txtStep.Text.Trim() +"' AND '"+ speNewWorkOrderPrice.Text.Trim() +"' "
                        + " NOT BETWEEN MinimumWOPrice AND MaximumWOPrice", Session["ConnString"].ToString());

                    if (ask.Rows.Count != 0)
                    {
                        cp.JSProperties["cp_WOP"] = true;
                    }
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
        protected void dtpOldCommitment_Init(object sender, EventArgs e)
        {
            if (Request.QueryString["entry"] == "N")
            {
                dtpOldCommitment.Date = DateTime.Now;
            }
        }
        protected void dtpNewCommitment_Init(object sender, EventArgs e)
        {
            if (Request.QueryString["entry"] == "N")
            {
                dtpNewCommitment.Date = DateTime.Now;
            }
        }
        
        protected void Connection_Init(object sender, EventArgs e)
        {
            ((SqlDataSource)sender).ConnectionString = Session["ConnString"].ToString();
        }
        protected void aglReferenceJO_Init(object sender, EventArgs e)
        {
            aglReferenceJO.DataSourceID = "sdsReferenceJO";
            if (aglReferenceJO.DataSource != null)
            {
                aglReferenceJO.DataSource = null;
            }
            aglReferenceJO.DataBind();
        }

        protected void aglStep_Init(object sender, EventArgs e)
        {

        }

        protected void dtpNewWODate_Init(object sender, EventArgs e)
        {
            if (Request.QueryString["entry"] == "N")
            {
                dtpNewWODate.Date = DateTime.Now;
            }
        }
    }
}