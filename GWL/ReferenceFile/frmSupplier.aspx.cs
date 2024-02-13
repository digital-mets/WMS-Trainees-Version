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
    public partial class frmSupplier : System.Web.UI.Page
    {
        Boolean view = false;//Boolean for view state

        Entity.Supplier _Entity = new Supplier();

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
                DataTable getCurrency = new DataTable();
                getCurrency = Gears.RetriveData2("SELECT RTRIM(LTRIM(Value)) AS Value FROM IT.SystemSettings WHERE Code = 'CURRENCY'", Session["ConnString"].ToString());
               

                if (Request.QueryString["entry"].ToString() == "N")
                {
                    
                }
                else
                {
                    txtSupplierCode.Value = Request.QueryString["docnumber"].ToString();
                    //_Entity.getdata(txtSupplierCode.Text);
                    _Entity.getdata(txtSupplierCode.Text, Session["ConnString"].ToString());//ADD CONN

                    txtName.Value = _Entity.Name;
                    txtProfitCenterCode.Value = _Entity.ProfitCenterCode;
                    txtCostCenterCode.Value = _Entity.CostCenterCode;
                    txtAddress.Value = _Entity.Address;
                    txtContactPerson.Value = _Entity.ContactPerson;
                    txtAPAccount.Value = _Entity.APAccountCode;
                    txtMnemonics.Value = _Entity.Mnemonics;
                    cbShowForPayee.Value = _Entity.ShowForPayee;
                    txtPayeeName.Value = _Entity.PayeeName;
                    txtCurrency.Value = _Entity.Currency;
                    cbIsInactive.Value = _Entity.IsInactive;
                    cbIsWorkCenter.Value = _Entity.IsWorkCenter;
                    txtTaxCode.Value = _Entity.TaxCode;
                    //cbWithholdingTaxAgent.Value = _Entity.WithholdingTaxAgent;
                    txtATCCode.Value = _Entity.ATCCode;
                    txtWithReferenceNumber.Value = _Entity.WithReferenceNumber;
                    txtActivatedDate.Text = String.IsNullOrEmpty(_Entity.ActivatedDate) ? "" : Convert.ToDateTime(_Entity.ActivatedDate).ToShortDateString();
                    txtAddedDate.Text = String.IsNullOrEmpty(_Entity.AddedDate) ? "" : Convert.ToDateTime(_Entity.AddedDate).ToShortDateString();
                    txtLastEditedDate.Text = String.IsNullOrEmpty(_Entity.LastEditedDate) ? "" : Convert.ToDateTime(_Entity.LastEditedDate).ToShortDateString();
                    txtDeActivatedDate.Text = String.IsNullOrEmpty(_Entity.DeActivatedDate) ? "" : Convert.ToDateTime(_Entity.DeActivatedDate).ToShortDateString();
                    txtAddedBy.Text = _Entity.AddedBy;
                    txtLastEditedBy.Text = _Entity.LastEditedBy;
                    txtHField1.Value = _Entity.Field1;
                    txtHField2.Value = _Entity.Field2;
                    txtHField3.Value = _Entity.Field3;
                    txtHField4.Value = _Entity.Field4;
                    txtHField5.Value = _Entity.Field5;
                    txtHField6.Value = _Entity.Field6;
                    txtHField7.Value = _Entity.Field7;
                    txtHField8.Value = _Entity.Field8;
                    txtHField9.Value = _Entity.Field9;
                }

                //V=View, E=Edit, N=New
                switch (Request.QueryString["entry"].ToString())
                {
                    case "N":
                        updateBtn.Text = "Add";
                        if (String.IsNullOrEmpty(txtCurrency.Text))
                        {
                            txtCurrency.Value = getCurrency.Rows[0]["Value"].ToString();
                        }
                        break;
                    case "E":
                        updateBtn.Text = "Update";
                        txtSupplierCode.ReadOnly = true;
                        glcheck.ClientVisible = true;
                        if (String.IsNullOrEmpty(txtCurrency.Text))
                        {
                            txtCurrency.Value = getCurrency.Rows[0]["Value"].ToString();
                        }
                        break;
                    case "V":
                        view = true;//sets view mode for entry
                        updateBtn.Text = "Close";
                        txtSupplierCode.ReadOnly = true;
                        cbIsInactive.ReadOnly = true;
                        //cbForecast.ReadOnly = true;
                        glcheck.ClientVisible = false;
                        break;
                    case "D":
                        view = true;
                        updateBtn.Text = "Delete";
                        txtSupplierCode.ReadOnly = true;
                        cbIsInactive.ReadOnly = true;
                        //cbForecast.ReadOnly = true;
                        glcheck.ClientVisible = false;
                        break;

                }
                //Details checking. changes datasourceid of gridview if there's no details for a certain docnumber; to enable adding of details.
            }

            
        }
        #endregion

        #region Set controls' state/behavior/etc...

        protected void SupplierCodeLoad(object sender, EventArgs e)//Control for all textbox
        {
            ASPxGridLookup sup = sender as ASPxGridLookup;

            if (Request.QueryString["entry"].ToString() == "E")
            {
                if (!String.IsNullOrEmpty(txtSupplierCode.Text))
                {
                    sup.ReadOnly = true;
                    sup.DropDownButton.Enabled = false;
                }
                else
                {
                    sup.ReadOnly = false;
                    sup.DropDownButton.Enabled = true;
                }
            }
            else if (Request.QueryString["entry"].ToString() == "N")
            {
                sup.ReadOnly = false;
                sup.DropDownButton.Enabled = true;
            }
            else
            {
                sup.ReadOnly = true;
                sup.DropDownButton.Enabled = false;
            }
        }
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
                look.DropDownButton.Visible = !view;
            }
        }
        protected void Check_Load(object sender, EventArgs e)//Control for all textbox
        {
            ASPxCheckBox check = sender as ASPxCheckBox;
            check.ReadOnly = view;
        }
        #endregion

        #region Callback functions(Inserting/Updating/Deleting/Validating)
        protected void cp_Callback(object sender, CallbackEventArgsBase e)
        {
            _Entity.Connection = Session["ConnString"].ToString(); //ADD CONN
            _Entity.SupplierCode = txtSupplierCode.Text;
            _Entity.Name = txtName.Text;
            _Entity.ProfitCenterCode = txtProfitCenterCode.Text;
            _Entity.CostCenterCode = txtCostCenterCode.Text;
            _Entity.Address = txtAddress.Text;
            _Entity.ContactPerson = txtContactPerson.Text;
            _Entity.APAccountCode = txtAPAccount.Text;
            _Entity.Mnemonics = txtMnemonics.Text;
            _Entity.ShowForPayee = Convert.ToBoolean(cbShowForPayee.Value.ToString());
            _Entity.PayeeName = txtPayeeName.Text;
            _Entity.Currency = txtCurrency.Text;
            _Entity.IsInactive = Convert.ToBoolean(cbIsInactive.Value.ToString());
            _Entity.IsWorkCenter = Convert.ToBoolean(cbIsWorkCenter.Value.ToString());
            _Entity.TaxCode = txtTaxCode.Text;
            //_Entity.WithholdingTaxAgent = Convert.ToBoolean(cbWithholdingTaxAgent.Value.ToString());
            _Entity.ATCCode = txtATCCode.Text;
            _Entity.WithReferenceNumber = txtWithReferenceNumber.Text;
            _Entity.LastEditedBy = Session["userid"].ToString();
            _Entity.AddedBy = Session["userid"].ToString();
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
            _Entity.AddedBy = Session["userid"].ToString();
            _Entity.AddedDate = DateTime.Now.ToString();
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
                case "SUP":
                    SqlDataSource ds = BizAccount;
                    ds.SelectCommand = string.Format("select BizPartnerCode,Name,Address,ContactPerson from Masterfile.BizPartner where BizPartnerCode = '" + txtSupplierCode.Text + "'");
                    DataView tran = (DataView)ds.Select(DataSourceSelectArguments.Empty);
                    if (tran.Count > 0)
                    {
                        txtName.Value = tran[0][1].ToString();
                        txtAddress.Value = tran[0][2].ToString();
                        txtContactPerson.Value = tran[0][3].ToString();
                    }
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
            bizpartner.ConnectionString = Session["ConnString"].ToString();
            profitcenter.ConnectionString = Session["ConnString"].ToString();
            costcenter.ConnectionString = Session["ConnString"].ToString();
            atc.ConnectionString = Session["ConnString"].ToString();
            currency.ConnectionString = Session["ConnString"].ToString();
            tax.ConnectionString = Session["ConnString"].ToString();
            chartofaccount.ConnectionString = Session["ConnString"].ToString();
            BizAccount.ConnectionString = Session["ConnString"].ToString();
        }


    }
}