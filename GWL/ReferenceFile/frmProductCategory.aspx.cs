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
    public partial class frmProductCategory : System.Web.UI.Page
    {
        Boolean view = false;//Boolean for view state

        Entity.ProductCategory _Entity = new ProductCategory();

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
                        txtProductCategoryCode.ReadOnly = true;
                        glcheck.ClientVisible = true;
                        break;
                    case "V":
                        view = true;//sets view mode for entry
                        updateBtn.Text = "Close";
                        txtProductCategoryCode.ReadOnly = true;
                        cbHaveSubCat.ReadOnly = true;
                        cbForecast.ReadOnly = true;
                        break;
                    case "D":
                        view = true;
                        updateBtn.Text = "Delete";
                        txtProductCategoryCode.ReadOnly = true;
                        cbHaveSubCat.ReadOnly = true;
                        cbForecast.ReadOnly = true;
                        break;

                }

                if (Request.QueryString["entry"].ToString() == "N")
                {
                    
                }
                else
                {
                    txtProductCategoryCode.Value = Request.QueryString["docnumber"].ToString();
                    _Entity.getdata(txtProductCategoryCode.Text, Session["ConnString"].ToString());//ADD CONN
                    txtDescription.Value = _Entity.Description.ToString();
                    txtItemCategoryCode.Value = _Entity.ItemCategoryCode.ToString();
                    txtMnemonics.Value = _Entity.Mnemonics.ToString();
                    cbForecast.Value = _Entity.ForForecast;
                    cbHaveSubCat.Value = _Entity.HaveSubCategory;
                    cbIsInactive.Value = _Entity.IsInactive;
                    txtActivatedDate.Text = String.IsNullOrEmpty(_Entity.ActivatedDate) ? "" : Convert.ToDateTime(_Entity.ActivatedDate).ToShortDateString();
                    txtAddedDate.Text = String.IsNullOrEmpty(_Entity.AddedDate) ? "" : Convert.ToDateTime(_Entity.AddedDate).ToShortDateString();
                    txtLastEditedDate.Text = String.IsNullOrEmpty(_Entity.LastEditedDate) ? "" : Convert.ToDateTime(_Entity.LastEditedDate).ToShortDateString();
                    txtDeActivatedDate.Text = String.IsNullOrEmpty(_Entity.DeActivatedDate) ? "" : Convert.ToDateTime(_Entity.DeActivatedDate).ToShortDateString();
                    txtAddedBy.Text = _Entity.AddedBy;
                    txtLastEditedBy.Text = _Entity.LastEditedBy;
                    txtHField1.Value = _Entity.Field1.ToString();
                    txtHField2.Value = _Entity.Field2.ToString();
                    txtHField3.Value = _Entity.Field3.ToString();
                    txtHField4.Value = _Entity.Field4.ToString();
                    txtHField5.Value = _Entity.Field5.ToString();
                    txtHField6.Value = _Entity.Field6.ToString();
                    txtHField7.Value = _Entity.Field7.ToString();
                    txtHField8.Value = _Entity.Field8.ToString();
                    txtHField9.Value = _Entity.Field9.ToString();
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
        #endregion

        #region Callback functions(Inserting/Updating/Deleting/Validating)
        protected void cp_Callback(object sender, CallbackEventArgsBase e)
        {
            _Entity.Connection = Session["ConnString"].ToString(); //ADD CONN
            _Entity.ProductCategoryCode = txtProductCategoryCode.Value.ToString();
            _Entity.Description = txtDescription.Text;
            _Entity.ItemCategoryCode = txtItemCategoryCode.Value.ToString();
            _Entity.Mnemonics = txtMnemonics.Text;
            _Entity.ForForecast = Convert.ToBoolean(cbForecast.Value.ToString());
            _Entity.HaveSubCategory = Convert.ToBoolean(cbHaveSubCat.Value.ToString());
            _Entity.IsInactive = Convert.ToBoolean(cbIsInactive.Value.ToString());
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
            itemcat.ConnectionString = Session["ConnString"].ToString();

        }


    }
}