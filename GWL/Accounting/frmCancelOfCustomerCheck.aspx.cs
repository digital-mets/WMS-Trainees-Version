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
using GearsAccounting;

namespace GWL
{
    public partial class frmCancelOfCustomerCheck : System.Web.UI.Page
    {
        Boolean error = false;//Boolean for grid validation
        Boolean view = false;//Boolean for view state
        Boolean check = false;//Boolean for grid validation

        private static string Connection;

        Entity.CancelCustomerCheck _Entity = new CancelCustomerCheck();//Calls entity Room

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
            if (Request.QueryString["entry"].ToString() == "V" || Request.QueryString["entry"].ToString() == "D")
            {
                view = true;//sets view mode for entry
            }

            dtpDocDate.MinDate = Convert.ToDateTime(GearsCommon.GCommon.SystemSetting("BookDate", Session["ConnString"].ToString()).ToString());

            if (!IsPostBack)
            {
                Session["CustChkQuery"] = null;

                Connection = Session["ConnString"].ToString();

                txtDocNumber.ReadOnly = true;
                txtDocNumber.Value = Request.QueryString["docnumber"].ToString();

                dtpDocDate.Text = DateTime.Now.ToShortDateString();
                _Entity.getdata(txtDocNumber.Text, Session["ConnString"].ToString());//ADD CONN
                dtpDocDate.Value = String.IsNullOrEmpty(_Entity.DocDate) ? DateTime.Now : Convert.ToDateTime(_Entity.DocDate);
                glCheckCancelType.Value = _Entity.CheckCancelType;
                typefilter();
                glCheckNumber.Value = _Entity.CheckNumber;
                dtpCheckDate.Text = String.IsNullOrEmpty(_Entity.CheckDate) ? null : Convert.ToDateTime(_Entity.CheckDate).ToShortDateString();
                txtCheckAmount.Value = Convert.ToDecimal(_Entity.CheckAmount).ToString();
                txtCheckRecID.Value = Convert.ToInt32(_Entity.CheckRecID).ToString();
                txtBouncedReason.Value = _Entity.BouncedReason;
                txtCheckRefNum.Value = _Entity.CheckRefNumber;

                txtAddedBy.Text = _Entity.AddedBy;
                txtAddedDate.Text = _Entity.AddedDate;
                txtLastEditedBy.Text = _Entity.LastEditedBy;
                txtLastEditedDate.Text = _Entity.LastEditedDate;
                txtSubmittedBy.Value = _Entity.SubmittedBy;
                txtSubmittedDate.Value = _Entity.SubmittedDate;
                txtPostedBy.Text = _Entity.PostedBy;
                txtPostedDate.Text = _Entity.PostedDate;
                txtHField1.Text = _Entity.Field1;
                txtHField2.Text = _Entity.Field2;
                txtHField3.Text = _Entity.Field3;
                txtHField4.Text = _Entity.Field4;
                txtHField5.Text = _Entity.Field5;
                txtHField6.Text = _Entity.Field6;
                txtHField7.Text = _Entity.Field7;
                txtHField8.Text = _Entity.Field8;
                txtHField9.Text = _Entity.Field9;

                FormLayout.FindItemOrGroupByName("ReferenceTransaction").ClientVisible = false;

                updateBtn.Text = "Add";

                if (!string.IsNullOrEmpty(_Entity.LastEditedBy))
                {
                    updateBtn.Text = "Update";
                    FormLayout.FindItemOrGroupByName("ReferenceTransaction").ClientVisible = true;
                }

                switch (Request.QueryString["entry"].ToString())
                {
                    case "N":
                        //updateBtn.Text = "Add";
                        break;
                    case "E":
                        updateBtn.Text = "Update";
                        FormLayout.FindItemOrGroupByName("ReferenceTransaction").ClientVisible = true;
                        break;
                    case "V":
                        glcheck.ClientVisible = false;
                        updateBtn.Text = "Close";
                        FormLayout.FindItemOrGroupByName("ReferenceTransaction").ClientVisible = true;
                        break;
                    case "D":
                        glcheck.ClientVisible = false;
                        updateBtn.Text = "Delete";
                        FormLayout.FindItemOrGroupByName("ReferenceTransaction").ClientVisible = true;
                        break;
                }

                gvRef.DataSourceID = "odsReference";
                this.gvRef.Columns["CommandString"].Width = 0;
                this.gvRef.Columns["RCommandString"].Width = 0;
                gvJournal.DataSourceID = "odsJournalEntry";
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
            gparam._TransType = "ACTCOC";
            string strresult = GearsAccounting.GAccounting.CancellationCustomerCheck_Validate(gparam);
            if (strresult != " ")
            {
                cp.JSProperties["cp_valmsg"] = strresult;
            }
        }
        #endregion

        #region Post
        private void Post()
        {
            GearsLibrary.Gears.GearsParameter gparam = new Gears.GearsParameter();
            gparam._Connection = Session["ConnString"].ToString();
            gparam._DocNo = _Entity.DocNumber;
            gparam._UserId = Session["Userid"].ToString();
            gparam._TransType = "ACTCOC";
            gparam._Table = "Accounting.CancellationOfCustomerCheck";
            gparam._Factor = -1;
            string strresult = GearsAccounting.GAccounting.CancellationCustomerCheck_Post(gparam);
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
            var look = sender as ASPxGridLookup;
            if (look != null)
            {
                look.DropDownButton.Enabled = !view;
                look.ReadOnly = view;
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
            date.DropDownButton.Enabled = !view;
            date.ReadOnly = view;
        }
        protected void SpinEdit_Load(object sender, EventArgs e)//Control for all numeric entries in header
        {
            ASPxSpinEdit spinedit = sender as ASPxSpinEdit;
            spinedit.ReadOnly = view;
        }
        protected void Memo_Load(object sender, EventArgs e)//Control for all memo
        {
            ASPxMemo memo = sender as ASPxMemo;
            memo.ReadOnly = view;
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


        #endregion

        #region Callback functions(Inserting/Updating/Deleting/Validating)
        protected void cp_Callback(object sender, CallbackEventArgsBase e)
        {
            string CheckNum = glCheckNumber.Text.Split('|')[0];
            _Entity.DocNumber = txtDocNumber.Text;
            _Entity.Connection = Session["ConnString"].ToString();
            _Entity.DocDate = dtpDocDate.Text;
            _Entity.CheckCancelType = glCheckCancelType.Text;
            _Entity.CheckNumber = CheckNum;
            _Entity.CheckDate = dtpCheckDate.Text;
            _Entity.CheckAmount = String.IsNullOrEmpty(txtCheckAmount.Text) ? 0 : Convert.ToDecimal(txtCheckAmount.Text);
            _Entity.CheckRecID = String.IsNullOrEmpty(txtCheckRecID.Text) ? 0 : Convert.ToInt32(txtCheckRecID.Text);
            _Entity.CheckRefNumber = txtCheckRefNum.Text;
            _Entity.BouncedReason = txtBouncedReason.Text;
            _Entity.LastEditedBy = Session["userid"].ToString();

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
                case "Update":

                    string strError = Functions.Submitted(_Entity.DocNumber, "Accounting.CancellationOfCustomerCheck", 1, Connection);//NEWADD factor 1 if submit, 2 if approve
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

                        Post();
                        Validate();

                        cp.JSProperties["cp_message"] = e.Parameter == "Add" ? "Successfully Add!" : "Successfully Updated!";
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
                    check = true;
                    cp.JSProperties["cp_delete"] = true;
                    break;

                case "ConfDelete":
                    _Entity.DeleteData(_Entity);
                    cp.JSProperties["cp_close"] = true;
                    cp.JSProperties["cp_message"] = "Successfully Deleted!";
                    Session["Refresh"] = "1";
                    break;

                case "Close":
                    cp.JSProperties["cp_close"] = true;
                    Session["Refresh"] = "1";
                    break;

                case "CheckNumber":
                    SqlDataSource ds = sdsCheckNumber;

                    if (String.IsNullOrEmpty(glCheckNumber.Text))
                    {
                        dtpCheckDate.Text = "";
                        txtCheckAmount.Text = "";
                        txtCheckRecID.Text = "";
                        txtCheckRefNum.Text = "";
                    }
                    else
                    {
                        string Check = glCheckNumber.Text.Split('|')[0];
                        string Num = glCheckNumber.Text.Split('|')[1];

                        DataTable getDetails = new DataTable();
                        getDetails = Gears.RetriveData2("SELECT DISTINCT DocNumber, CheckNumber, CheckDate, CheckAmount, RecID FROM Accounting.CollectionChecks WHERE CheckNumber = '" + Check + "' AND DocNumber = '" + Num + "'", Session["ConnString"].ToString());
                        if (getDetails.Rows.Count > 0)
                        {
                            dtpCheckDate.Text = Convert.ToDateTime(getDetails.Rows[0]["CheckDate"].ToString()).ToShortDateString();
                            txtCheckAmount.Text = getDetails.Rows[0]["CheckAmount"].ToString();
                            txtCheckRecID.Text = getDetails.Rows[0]["RecID"].ToString();
                            txtCheckRefNum.Text = getDetails.Rows[0]["DocNumber"].ToString();
                        }
                        else
                        {
                            dtpCheckDate.Text = "";
                            txtCheckAmount.Text = "";
                            txtCheckRecID.Text = "";
                            txtCheckRefNum.Text = "";
                        }

                        //ds.SelectCommand = string.Format("SELECT DISTINCT DocNumber, CheckNumber, CheckDate, CheckAmount, RecID FROM Accounting.CollectionChecks WHERE CheckNumber = '" + Check + "' AND DocNumber = '" + Num + "'");
                        //DataView inb = (DataView)ds.Select(DataSourceSelectArguments.Empty);
                        //if (inb.Count > 0)
                        //{
                        //    //glCheckNumber.Text = inb[0][1].ToString();
                        //    dtpCheckDate.Text = Convert.ToDateTime(inb[0][2]).ToShortDateString();
                        //    txtCheckAmount.Text = inb[0][3].ToString();
                        //    txtCheckRecID.Text = inb[0][4].ToString();
                        //}

                        //ds.SelectCommand = string.Format(((Session["CustChkQuery"] != null || Session["CustChkQuery"] != "") ? Session["CustChkQuery"].ToString() : "SELECT A.DocNumber, A.CheckNumber FROM Accounting.CollectionChecks A WHERE ISNULL(DateCleared,'') = '' AND ISNULL(CancelledCheckDocNum,'') = ''"));
                    }

                    break;

                case "CheckCancelType":
                    typefilter();
                    break;
            }
        }
        void AddError(Dictionary<GridViewColumn, string> errors, GridViewColumn column, string errorText)
        {
            if (errors.ContainsKey(column)) return;
            errors[column] = errorText;
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

        protected void Connection_Init(object sender, EventArgs e)
        {
            ((SqlDataSource)sender).ConnectionString = Session["ConnString"].ToString();
        }

        public void typefilter()
        {
            string type = glCheckCancelType.Text;
            if (type == "Pull-out")
            {
                sdsCheckNumber.SelectCommand = "SELECT A.DocNumber, A.CheckNumber FROM Accounting.CollectionChecks A " +
                                               " inner join  Accounting.Collection B on A.Docnumber =B.Docnumber " +
                                               " WHERE ISNULL(A.DepositedDate,'') = '' AND ISNULL(A.DateCleared,'') = '' AND ISNULL(A.CancelledCheckDocNum,'') = '' " +
                                               " and B.ReceiptType!='M' and ISNULL(B.CancelledBy,'')='' and ISNULL(B.CancelledDate,'')=''";
                Session["CustChkQuery"] = sdsCheckNumber.SelectCommand;
                glCheckNumber.Value = null;
                //dtpCheckDate.Value = DateTime.Now.ToShortDateString();
                txtCheckAmount.Value = "0.00";
                txtCheckRecID.Value = "";
                txtCheckRefNum.Text = "";
                glCheckNumber.DataSourceID = "sdsCheckNumber";
                glCheckNumber.DataBind();
            }
            else
            {
                sdsCheckNumber.SelectCommand = "SELECT A.DocNumber, A.CheckNumber FROM Accounting.CollectionChecks A " +
                                               " inner join  Accounting.Collection B on A.Docnumber =B.Docnumber WHERE ISNULL(A.DepositedDate,'') != '' AND ISNULL(A.DateCleared,'') = '' AND ISNULL(A.CancelledCheckDocNum,'') = '' " +
                                               " and B.ReceiptType!='M' and ISNULL(B.CancelledBy,'')='' and ISNULL(B.CancelledDate,'')=''";
                Session["CustChkQuery"] = sdsCheckNumber.SelectCommand;
                glCheckNumber.Value = null;
                //dtpCheckDate.Value = DateTime.Now.ToShortDateString();
                txtCheckAmount.Value = "0.00";
                txtCheckRecID.Value = "";
                txtCheckRefNum.Text = "";
                glCheckNumber.DataSourceID = "sdsCheckNumber";
                glCheckNumber.DataBind();
            }
        }
    }
}