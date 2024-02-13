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
    public partial class frmFuncGroupClosing : System.Web.UI.Page
    {
        Boolean view = false;//Boolean for view state

        Entity.FunctionalGroupClosing _Func = new FunctionalGroupClosing();

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
                        txtFunctionalGroupID.ReadOnly = true;
                        glcheck.ClientVisible = true;
                        break;
                    case "V":
                        view = true;//sets view mode for entry
                        updateBtn.Text = "Close";
                        txtFunctionalGroupID.ReadOnly = true;
                        break;
                    case "D":
                        view = true;
                        updateBtn.Text = "Delete";
                        txtFunctionalGroupID.ReadOnly = true;
                        break;

                }

                if (Request.QueryString["entry"].ToString() == "N")
                {
                    
                }
                else
                {
                    txtFunctionalGroupID.Value = Request.QueryString["docnumber"].ToString();
                    _Func.getdata(txtFunctionalGroupID.Text, Session["ConnString"].ToString());
                    txtDescription.Value = _Func.Description.ToString();
                    txtAssignedHead.Value = _Func.AssignHead.ToString();
                    txtDateClosed.Value = String.IsNullOrEmpty(_Func.DateClosed) ? null : Convert.ToDateTime(_Func.DateClosed).ToShortDateString();
                    txtDays.Value = _Func.Days.ToString();
                    txtHField1.Value = _Func.Field1.ToString();
                    txtHField2.Value = _Func.Field2.ToString();
                    txtHField3.Value = _Func.Field3.ToString();
                    txtHField4.Value = _Func.Field4.ToString();
                    txtHField5.Value = _Func.Field5.ToString();
                    txtHField6.Value = _Func.Field6.ToString();
                    txtHField7.Value = _Func.Field7.ToString();
                    txtHField8.Value = _Func.Field8.ToString();
                    txtHField9.Value = _Func.Field9.ToString();
                }
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
            ASPxGridLookup lookup = sender as ASPxGridLookup;
            lookup.DropDownButton.Enabled = !view;
            lookup.ReadOnly = view;
        }
        #endregion

        #region Callback functions(Inserting/Updating/Deleting/Validating)
        protected void cp_Callback(object sender, CallbackEventArgsBase e)
        {
            _Func.FunctionalGroupID = txtFunctionalGroupID.Value.ToString();
            _Func.Description = txtDescription.Text;
            _Func.AssignHead = txtAssignedHead.Value.ToString();
            _Func.Days = Convert.ToInt32(txtDays.Value.ToString());
            _Func.LastEditedBy = Session["userid"].ToString();
            _Func.AddedBy = Session["userid"].ToString();
            _Func.Field1 = String.IsNullOrEmpty(txtHField1.Text) ? null : txtHField1.Text;
            _Func.Field2 = String.IsNullOrEmpty(txtHField2.Text) ? null : txtHField2.Text;
            _Func.Field3 = String.IsNullOrEmpty(txtHField3.Text) ? null : txtHField3.Text;
            _Func.Field4 = String.IsNullOrEmpty(txtHField4.Text) ? null : txtHField4.Text;
            _Func.Field5 = String.IsNullOrEmpty(txtHField5.Text) ? null : txtHField5.Text;
            _Func.Field6 = String.IsNullOrEmpty(txtHField6.Text) ? null : txtHField6.Text;
            _Func.Field7 = String.IsNullOrEmpty(txtHField7.Text) ? null : txtHField7.Text;
            _Func.Field8 = String.IsNullOrEmpty(txtHField8.Text) ? null : txtHField8.Text;
            _Func.Field9 = String.IsNullOrEmpty(txtHField9.Text) ? null : txtHField9.Text;
            _Func.Connection = Session["ConnString"].ToString();

            switch (e.Parameter)
            {
                case "Add":
                        _Func.InsertData(_Func);//Method of inserting for header
                        cp.JSProperties["cp_message"] = "Successfully Added!";//Message variable to client side
                        cp.JSProperties["cp_success"] = true;//Success bool variable  to client side
                        cp.JSProperties["cp_close"] = true;//Close window variable to client side
                    break;

                case "Update":

                        _Func.UpdateData(_Func);//Method of inserting for header
                        //Validate();
                        cp.JSProperties["cp_message"] = "Successfully Updated!";
                        cp.JSProperties["cp_success"] = true;
                        cp.JSProperties["cp_close"] = true;
                    break;

                case "Delete":
                    cp.JSProperties["cp_delete"] = true;
                    break;

                case "ConfDelete":
                    _Func.DeleteData(_Func);
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
            Masterfilebiz.ConnectionString = Session["ConnString"].ToString();
            MasterfileEmp.ConnectionString = Session["ConnString"].ToString();
        }


    }
}