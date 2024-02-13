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
    public partial class frmServiceOrderRevision : System.Web.UI.Page
    {
        Boolean error = false;
        Boolean view = false;
        Boolean check = false;

        Entity.ServiceOrderRevision _Entity = new ServiceOrderRevision();

        #region page load/entry
        protected void Page_Load(object sender, EventArgs e)
        {
            Gears.UseConnectionString(Session["ConnString"].ToString());

            string referer;
            try { referer = Request.ServerVariables["http_referer"]; }
            catch { referer = ""; }

            if (referer == null && Common.Common.SystemSetting("URLCHECK", Session["ConnString"].ToString()) != "NO") {
                Response.Redirect("~/error.aspx");
            }

            if (!IsPostBack)
            {
                switch (Request.QueryString["entry"].ToString())
                {
                    case "N":
                        updateBtn.Text = "Add";
                        break;
                    case "E":
                        updateBtn.Text = "Update"; 
                        break;
                    case "V":
                        view = true; 
                        updateBtn.Text = "Close";
                        glcheck.ClientVisible = false;
                        break;
                    case "D":
                        updateBtn.Text = "Delete";
                        glcheck.ClientVisible = false;
                        break;
                }
                txtDocnumber.Value = Request.QueryString["docnumber"].ToString();
                _Entity.getdata(txtDocnumber.Text, Session["ConnString"].ToString());
                glserviceorder.Value = _Entity.ServiceOrder.ToString();
                dtpdocdate.Text = Convert.ToDateTime(_Entity.DocDate.ToString()).ToShortDateString();
                txtoldduedate.Text = String.IsNullOrEmpty(_Entity.OldDueDate.ToString()) ? null : Convert.ToDateTime(_Entity.OldDueDate.ToString()).ToShortDateString();
                dtnewduedate.Text = String.IsNullOrEmpty(_Entity.NewDueDate.ToString()) ? null : Convert.ToDateTime(_Entity.NewDueDate.ToString()).ToShortDateString();
                speOldWorkOrder.Text = _Entity.OldWOPrice.ToString();
                speNewWorkOrder.Text = _Entity.NewWOPrice.ToString();
                memReason.Text = _Entity.Reason;
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

                if (Request.QueryString["entry"].ToString() == "N") {
                    frmlayout1.FindItemOrGroupByName("ReferenceTransaction").ClientVisible = false;
                }
                else
                {
                    gvRef.DataSourceID = "odsReference";
                    odsReference.SelectParameters["DocNumber"].DefaultValue = txtDocnumber.Text;
                    this.gvRef.Columns["CommandString"].Width = 0;
                    this.gvRef.Columns["RCommandString"].Width = 0;
                }

                DataTable dtrr1 = Gears.RetriveData2("SELECT DocNumber FROM Procurement.ServiceOrderRevision WHERE DocNumber = '" + txtDocnumber.Text + "' AND ISNULL(ServiceOrder,'') != '' ", Session["ConnString"].ToString());
                if (dtrr1.Rows.Count > 0 && Request.QueryString["entry"].ToString() == "N"){
                    updateBtn.Text = "Update";
                }
                //dtnewduedate.MinDate = DateTime.Now;
            }
        }
        #endregion

        #region Set controls' state/behavior/etc...
        protected void TextboxLoad(object sender, EventArgs e)
        {
            if (Request.QueryString["entry"].ToString() == "V" || Request.QueryString["entry"].ToString() == "D")
            {
                ASPxTextBox text = sender as ASPxTextBox;
                text.ReadOnly = true;
            }
        }
        protected void LookupLoad(object sender, EventArgs e)
        {
            if (Request.QueryString["entry"].ToString() == "V" || Request.QueryString["entry"].ToString() == "D")
            {
                ASPxGridLookup look = sender as ASPxGridLookup;
                look.DropDownButton.Enabled = false;
                look.ReadOnly = true;
            }
        }
        protected void Date_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["entry"].ToString() == "V" || Request.QueryString["entry"].ToString() == "D")
            {
                ASPxDateEdit date = sender as ASPxDateEdit;
                date.DropDownButton.Enabled = false;
                date.ReadOnly = true;
            }
        }
        protected void SpinEdit_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["entry"].ToString() == "V" || Request.QueryString["entry"].ToString() == "D")
            {
                ASPxSpinEdit spinedit = sender as ASPxSpinEdit;
                spinedit.ReadOnly = true;
            }
        }
        protected void Memo_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["entry"].ToString() == "V" || Request.QueryString["entry"].ToString() == "D")
            {
                ASPxMemo memo = sender as ASPxMemo;
                memo.ReadOnly = true;
            }
        }
        protected void gv_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
        {
            e.Visible = false;
        }
        #endregion

        #region Validation
        private void Validate()
        {
            GearsLibrary.Gears.GearsParameter gparam = new Gears.GearsParameter();
            gparam._DocNo = _Entity.DocNumber;
            gparam._TransType = "PRCSOR";
            gparam._Connection = Session["ConnString"].ToString();
            string strresult = GearsProcurement.GProcurement.ServiceOrderRevision_Validate(gparam); 
            if (strresult != " ") {
                cp.JSProperties["cp_valmsg"] = strresult + "\r\n";
            }
        }
        #endregion

        #region Callback functions(Inserting/Updating/Deleting/Validating)
        protected void cp_Callback(object sender, CallbackEventArgsBase e)
        {
            _Entity.Connection = Session["ConnString"].ToString(); 
            _Entity.DocNumber = txtDocnumber.Text;
            _Entity.DocDate = dtpdocdate.Text;
             _Entity.OldDueDate = txtoldduedate.Text; 
            _Entity.NewDueDate = dtnewduedate.Text;
            _Entity.ServiceOrder = glserviceorder.Text; 
            _Entity.OldWOPrice = String.IsNullOrEmpty(speOldWorkOrder.Text) ? 0 : Convert.ToDecimal(speOldWorkOrder.Value.ToString());
            _Entity.NewWOPrice = String.IsNullOrEmpty(speNewWorkOrder.Text) ? 0 : Convert.ToDecimal(speNewWorkOrder.Value.ToString());
            _Entity.Reason = memReason.Text;
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
            _Entity.AddedBy = Session["userid"].ToString();
            _Entity.LastEditedDate = DateTime.Now.ToString();

            switch (e.Parameter)
            {
                case "Add":
                case "Update":
                    if (error == false)
                    {
                        check = true; 
                        _Entity.UpdateData(_Entity);
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
                case "Delete":
                    cp.JSProperties["cp_delete"] = true;
                    break;
                case "ConfDelete":
                    _Entity.DeleteData(_Entity);
                    cp.JSProperties["cp_close"] = true;
                    cp.JSProperties["cp_message"] = "Successfully deleted";
                    Session["Refresh"] = "1";
                    break;
                case "Generate":
                    OtherDetail();
                    cp.JSProperties["cp_generated"] = true;
                    break;
                case "Close":
                    cp.JSProperties["cp_close"] = true;
                    break;
            }
        }

        protected void Connection_Init(object sender, EventArgs e)
        {
            sdsServiceOrder.ConnectionString = Session["ConnString"].ToString();
        }
        private void OtherDetail()
        {
            string pick = glserviceorder.Text;
            DataTable ret = Gears.RetriveData2("select B.TargetDeliveryDate,Labor from Procurement.SOWorkOrder A  inner join Procurement.ServiceOrder B on A.DocNumber = B.DocNumber WHERE A.DocNumber = '" + pick + "'", Session["ConnString"].ToString());

            if (ret.Rows.Count > 0)
            {
                speOldWorkOrder.Text = ret.Rows[0][1].ToString();
                txtoldduedate.Text = Convert.ToDateTime(ret.Rows[0][0].ToString()).ToShortDateString();
                speNewWorkOrder.Text = ret.Rows[0][1].ToString();
                dtnewduedate.Text = Convert.ToDateTime(ret.Rows[0][0].ToString()).ToShortDateString();
            }
            else
            {
                speOldWorkOrder.Text = "0.00";
                txtoldduedate.Text = null;
            }  
        }
        #endregion

    }
}