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


namespace GWL
{
    public partial class frmMainMenu : System.Web.UI.Page
    {
        Boolean view = false;//Boolean for view state

        Entity.MainMenuM _Entity = new MainMenuM();

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

            if (!IsPostBack)
            {
                //V=View, E=Edit, N=New
                switch (Request.QueryString["entry"].ToString())
                {
                    case "N":
                        updateBtn.Text = "Add";
                        break;
                    case "E":
                        updateBtn.Text = "Update";
                        //txtBizPartnerCode.ReadOnly = true;
                        break;
                    case "V":
                        view = true;//sets view mode for entry
                        updateBtn.Text = "Close";
                        glcheck.ClientVisible = false;
                        //txtBizPartnerCode.ReadOnly = true;
                        //cbIsInactive.ReadOnly = true;
                        break;
                    case "D":
                        view = true;
                        updateBtn.Text = "Delete";
                        //txtBizPartnerCode.ReadOnly = true;
                        break;

                }

                if (Request.QueryString["entry"].ToString() == "N")
                {
                    
                }
                else
                {
                    _Entity.getdata(Request.QueryString["docnumber"].ToString(), Session["ConnString"].ToString());
                    txtMenuID.Value = _Entity.MenuID;
                    txtFactBox.Value = _Entity.FactBox;
                    txtMenuSeq.Value = _Entity.MenuSequence;
                    txtCancelProc.Value = _Entity.CancelStoredProcedure;
                    txtFuncGroup.Value = _Entity.FuncGroupID;
                    txtApproveProc.Value = _Entity.ApproveStoredProcedure;
                    txtModuleID.Value = _Entity.ModuleID;
                    txtRibbon.Value = _Entity.Ribbon;
                    txtModuleDesc.Value = _Entity.ModuleDescription;
                    txtGLPosting.Value = _Entity.GLPosting;
                    txtCommandString.Value = _Entity.CommandString;
                    txtColDate.Value = _Entity.ColDate;
                    txtIcon.Value = _Entity.IconFileName;
                    txtSQLCommand.Value = _Entity.SQLCommand;
                    txtHelpIndex.Value = _Entity.HelpIndex;
                    txtExtract.Value = _Entity.Extract;
                    txtPKey.Value = _Entity.PrimarykeyColumn;
                    txtTable.Value = _Entity.TableName;
                    txtStoredProc.Value = _Entity.StoredProcedure;
                    txtTransType.Value = _Entity.TransactionType;
                    txtParameters.Value = _Entity.Parameters;
                }

                //Details checking. changes datasourceid of gridview if there's no details for a certain docnumber; to enable adding of details.
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
            
        }

        protected void MemoLoad(object sender, EventArgs e)//Control for all lookup in header
        {
            ASPxMemo memo = sender as ASPxMemo;
            memo.ReadOnly = view;
            
        }
        #endregion

        #region Callback functions(Inserting/Updating/Deleting/Validating)
        protected void cp_Callback(object sender, CallbackEventArgsBase e)
        {
            //_Biz.BusinessPartnerCode = txtBizPartnerCode.Value.ToString();
            //_Biz.Name = txtName.Text;
            //_Biz.Address = txtAddress.Value.ToString();
            //_Biz.ContactPerson = txtContactPerson.Value.ToString();
            //_Biz.TIN = txtTIN.Value.ToString();
            //_Biz.ContactNumber = String.IsNullOrEmpty(txtContactNumber.Text) ? null : txtContactNumber.Text;
            //_Biz.IsInactive = Convert.ToBoolean(cbIsInactive.Value.ToString());
            //_Biz.EmailAddress = String.IsNullOrEmpty(txtEmailAddress.Text) ? null : txtEmailAddress.Text;
            //_Biz.BusinessAccountCode = String.IsNullOrEmpty(txtBizAccountCode.Text) ? null : txtBizAccountCode.Text;
            _Entity.RecordID = Request.QueryString["docnumber"].ToString();
            _Entity.MenuID = txtMenuID.Text;
            _Entity.FactBox = txtFactBox.Text;
            _Entity.MenuSequence = txtMenuSeq.Text;
            _Entity.CancelStoredProcedure = txtCancelProc.Text;
            _Entity.FuncGroupID = txtFuncGroup.Text;
            _Entity.ApproveStoredProcedure = txtApproveProc.Text;
            _Entity.ModuleID = txtModuleID.Text;
            _Entity.Ribbon = txtRibbon.Text;
            _Entity.ModuleDescription = txtModuleDesc.Text;
            _Entity.GLPosting = txtGLPosting.Text;
            _Entity.CommandString = txtCommandString.Text;
            _Entity.ColDate = txtColDate.Text;
            _Entity.IconFileName = txtIcon.Text;
            _Entity.SQLCommand = txtSQLCommand.Text;
            _Entity.HelpIndex = txtHelpIndex.Text;
            _Entity.Extract = txtExtract.Text;
            _Entity.PrimarykeyColumn = txtPKey.Text;
            _Entity.TableName = txtTable.Text;
            _Entity.StoredProcedure = txtStoredProc.Text;
            _Entity.TransactionType = txtTransType.Text;
            _Entity.Parameters = txtParameters.Text;
            _Entity.Connection = Session["ConnString"].ToString();

            switch (e.Parameter)
            {
                case "Add":
                        _Entity.InsertData(_Entity);//Method of inserting for header
                        cp.JSProperties["cp_message"] = "Successfully Added!";//Message variable to client side
                        cp.JSProperties["cp_success"] = true;//Success bool variable  to client side
                        cp.JSProperties["cp_close"] = true;//Close window variable to client side
                    break;

                case "Update":

                        _Entity.UpdateData(_Entity);//Method of inserting for header

                        //Validate();
                        cp.JSProperties["cp_message"] = "Successfully Updated!";
                        cp.JSProperties["cp_success"] = true;
                        cp.JSProperties["cp_close"] = true;
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

            }
        }
        //dictionary method to hold error

        #endregion


        protected void gv1_DataBound(object sender, EventArgs e)
        {
            //ASPxGridView grid = sender as ASPxGridView;
            //if (grid.Columns.IndexOf(grid.Columns["CommandColumn"]) != -1)
            //    return;
            //GridViewCommandColumn col = new GridViewCommandColumn();
            //col.Name = "CommandColumn";
            //col.ShowDeleteButton = true;
            ////col.ShowNewButtonInHeader = true;
            //col.VisibleIndex = 0;
            //grid.Columns.Add(col);

            //gv1.Columns["DocNumber"].Visible = false;
            //gv1.Columns["LineNumber"].Visible = false;
            //if (!String.IsNullOrEmpty(FilePath))//Bind the data source to the grid
            //{
            //    
            //}
            //else
            //{
            //    gv1.KeyFieldName = "DocNumber;LineNumber";
            //}
        }


        protected void Connection_Init(object sender, EventArgs e)
        {
            Masterfilebizaccount.ConnectionString = Session["ConnString"].ToString();
        }




    }
}