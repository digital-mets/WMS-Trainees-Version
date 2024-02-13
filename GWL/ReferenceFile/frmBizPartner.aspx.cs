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
    public partial class frmBizPartner : System.Web.UI.Page
    {
        Boolean view = false;//Boolean for view state

        Entity.BizPartnerEnt _Biz = new BizPartnerEnt();

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
                        txtBizPartnerCode.ReadOnly = true;
                        glcheck.ClientVisible = true;
                        break;
                    case "V":
                        view = true;//sets view mode for entry
                        updateBtn.Text = "Close";
                        txtBizPartnerCode.ReadOnly = true;
                        cbIsInactive.ReadOnly = true;
                        break;
                    case "D":
                        view = true;
                        updateBtn.Text = "Delete";
                        txtBizPartnerCode.ReadOnly = true;
                        cbIsInactive.ReadOnly = true;
                        break;

                }

                if (Request.QueryString["entry"].ToString() == "N")
                {
                    
                }
                else
                {
                    txtBizPartnerCode.Value = Request.QueryString["docnumber"].ToString();
                    _Biz.getdata(txtBizPartnerCode.Text, Session["ConnString"].ToString());
                    txtName.Value = _Biz.Name.ToString();
                    txtAddress.Value = _Biz.Address.ToString();
                    txtContactPerson.Value = _Biz.ContactPerson.ToString();
                    txtTIN.Value = _Biz.TIN.ToString();
                    txtContactNumber.Value = _Biz.ContactNumber.ToString();
                    txtEmailAddress.Value = _Biz.EmailAddress.ToString();
                    txtBizAccountCode.Value = _Biz.BusinessAccountCode.ToString();
                    cbIsInactive.Value = _Biz.IsInactive;
                    cbIsSupplier.Value = _Biz.IsSupplier;
                    cbIsCustomer.Value = _Biz.IsCustomer;
                    cbIsEmployee.Value = _Biz.IsEmployee;
                    txtActivatedDate.Text = String.IsNullOrEmpty(_Biz.ActivatedDate) ? "" : Convert.ToDateTime(_Biz.ActivatedDate).ToShortDateString();
                    txtAddedDate.Text = String.IsNullOrEmpty(_Biz.AddedDate) ? "" : Convert.ToDateTime(_Biz.AddedDate).ToShortDateString();
                    txtLastEditedDate.Text = String.IsNullOrEmpty(_Biz.LastEditedDate) ? "" : Convert.ToDateTime(_Biz.LastEditedDate).ToShortDateString();
                    txtDeActivatedDate.Text = String.IsNullOrEmpty(_Biz.DeActivatedDate) ? "" : Convert.ToDateTime(_Biz.DeActivatedDate).ToShortDateString();
                    txtAddedBy.Text = _Biz.AddedBy;
                    txtLastEditedBy.Text = _Biz.LastEditedBy;
                
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
            if (look != null)
            {
                look.ReadOnly = view;
            }
        }

        protected void SpinEdit_Load(object sender, EventArgs e)//Control for all numeric entries in header
        {
            ASPxSpinEdit spinedit = sender as ASPxSpinEdit;
            spinedit.ReadOnly = view;
        }

        protected void CheckBox_Load(object sender, EventArgs e)//Control for all numeric entries in header
        {
            ASPxCheckBox check = sender as ASPxCheckBox;
            check.ReadOnly = view;
        }
        #endregion

        #region Callback functions(Inserting/Updating/Deleting/Validating)
        protected void cp_Callback(object sender, CallbackEventArgsBase e)
        {
            _Biz.BusinessPartnerCode = txtBizPartnerCode.Text;
            _Biz.Name = txtName.Text;
            _Biz.Address = txtAddress.Text;
            _Biz.ContactPerson = txtContactPerson.Text;
            _Biz.TIN = txtTIN.Text;
            _Biz.ContactNumber = String.IsNullOrEmpty(txtContactNumber.Text) ? null : txtContactNumber.Text;
            _Biz.IsInactive = Convert.ToBoolean(cbIsInactive.Value.ToString());
            _Biz.IsSupplier = Convert.ToBoolean(cbIsSupplier.Value.ToString());
            _Biz.IsCustomer = Convert.ToBoolean(cbIsCustomer.Value.ToString());
            _Biz.IsEmployee = Convert.ToBoolean(cbIsEmployee.Value.ToString());
            _Biz.EmailAddress = String.IsNullOrEmpty(txtEmailAddress.Text) ? null : txtEmailAddress.Text;
            _Biz.BusinessAccountCode = String.IsNullOrEmpty(txtBizAccountCode.Text) ? null : txtBizAccountCode.Text;
            _Biz.LastEditedBy = Session["userid"].ToString();
            _Biz.AddedBy = Session["userid"].ToString();
         
            _Biz.Connection = Session["ConnString"].ToString();

            switch (e.Parameter)
            {
                case "Add":

                    DataTable dtbldetail = Gears.RetriveData2("select top 1 * from wms.Contract A INNER JOIN WMS.ContractDetail B ON A.DocNumber = B.DocNumber  where CHARINDEX(DiffCustomerCode,'" + txtBizPartnerCode.Text + "') >0    and Status IN ('ACTIVE','REVISED','RENEWED') AND EffectivityDate <= GETDATE() AND GETDATE()  BETWEEN DateFrom  AND DateTo AND ISNULL(SubmittedBy,'')!=''", Session["ConnString"].ToString());

                    if (dtbldetail.Rows.Count == 0)
                    {
                     
                            cp.JSProperties["cp_message"] = "No Available Contract for this Customer: "+txtBizAccountCode.Text+"";
                            cp.JSProperties["cp_success"] = true;
                            return;
                        
                    }
                        _Biz.InsertData(_Biz);//Method of inserting for header
                        cp.JSProperties["cp_message"] = "Successfully Added!";//Message variable to client side
                        cp.JSProperties["cp_success"] = true;//Success bool variable  to client side
                        cp.JSProperties["cp_close"] = true;//Close window variable to client side
                    break;

                case "Update":

                        _Biz.UpdateData(_Biz);//Method of inserting for header

                        //Validate();
                        cp.JSProperties["cp_message"] = "Successfully Updated!";
                        cp.JSProperties["cp_success"] = true;
                        cp.JSProperties["cp_close"] = true;
                    break;

                case "Delete":
                    cp.JSProperties["cp_delete"] = true;
                    break;

                case "ConfDelete":
                    _Biz.DeleteData(_Biz);
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