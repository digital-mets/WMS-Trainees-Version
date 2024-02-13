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

namespace GWL
{
    public partial class frmServiceType : System.Web.UI.Page
    {
        Boolean error = false;//Boolean for grid validation
        Boolean view = false;//Boolean for view state
        Boolean check = false;//Boolean for grid validation

        Entity.ServType _Entity = new ServType();//Calls entity ICN
  
        #region page load/entry
        protected void Page_Load(object sender, EventArgs e)
        {
            Gears.UseConnectionString(Session["ConnString"].ToString());
            sdsGLSubsi.SelectCommand = "SELECT  SubsiCode, Description, AccountCode FROM Accounting.[glsubsicode] WHERE ISNULL (IsInactive, '') =0 AND AccountCode = '" + glGL.Text + "'";
            sdsARGLSubsi.SelectCommand = "SELECT  SubsiCode, Description, AccountCode FROM Accounting.[glsubsicode] WHERE ISNULL (IsInactive, '') =0 AND AccountCode = '" + glARGL.Text + "'";
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
         
            //gv1.KeyFieldName = "DocNumber;LineNumber";

            ////Details checking. changes datasourceid of gridview if there's no details for a certain docnumber; to enable adding of details.
            if (Request.QueryString["entry"].ToString() == "V" || Request.QueryString["entry"].ToString() == "D")
            {
                view = true;//sets view mode for entry
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
                        txtServiceType.ReadOnly = true;
                        break;
                    case "V":
                        glcheck.ClientVisible = false;
                        updateBtn.Text = "Close";
                        txtServiceType.ReadOnly = true;
                        chckStandard.ReadOnly = true;
                        break;
                    case "D":
                        glcheck.ClientVisible = false;
                        updateBtn.Text = "Delete";
                        txtServiceType.ReadOnly = true;
                        chckStandard.ReadOnly = true;
                        break;
                }

                
               //sets docnumber from session
                if (Request.QueryString["entry"].ToString() == "N")
                {
                   // gv1.DataSourceID = "sdsDetail";
                  
                    //sdsDetail.SelectCommand = "select * from WMS.PICKLIST where docnumber is null";
                    //sdsDetail.SelectParameters.Remove(sdsDetail.SelectParameters["DocNumber"]);
                }
                else
                {
                    txtServiceType.Value = Request.QueryString["docnumber"].ToString();

                    _Entity.getdata(txtServiceType.Text, Session["ConnString"].ToString());//ADD CONN
                    txtSequenceNumber.Text = _Entity.SequenceNumber;
                    txtType.Text = _Entity.Type;
                    txtDescription.Text  =  _Entity.Description ;
                    seServiceRate.Text = _Entity.ServiceRate;
                    txtServiceTypeCategoryCode.Text = _Entity.ServiceTypeCategoryCode;
                    glGL.Value = _Entity.SalesGLCode;
                    sdsGLSubsi.SelectCommand = "SELECT  SubsiCode, Description, AccountCode FROM Accounting.[glsubsicode] WHERE ISNULL (IsInactive, '') =0 AND AccountCode = '" + glGL.Text + "'";
                    glSubsi.DataBind();
                    glSubsi.Value = _Entity.SalesGLSubsiCode;
                    glARGL.Value = _Entity.ARGLCode;
                    sdsARGLSubsi.SelectCommand = "SELECT  SubsiCode, Description, AccountCode FROM Accounting.[glsubsicode] WHERE ISNULL (IsInactive, '') =0 AND AccountCode = '" + glARGL.Text + "'";
                    glARGLSubsi.DataBind();
                    glARGLSubsi.Value = _Entity.ARGLSubsiCode;
                    chckStandard.Value =   _Entity.IsStandard;

                    txtAddedBy.Text = _Entity.AddedBy;
                    txtLastEditedBy.Text = _Entity.LastEditedBy;
                    txtActivatedDate.Text = _Entity.ActivatedDate;
                    txtAddedDate.Text = _Entity.AddedDate;
                    txtLastEditedDate.Text = _Entity.LastEditedDate;
                    txtDeActivatedDate.Text = _Entity.DeActivatedDate;
                    txtHField1.Text = _Entity.Field1;
                    txtHField2.Text = _Entity.Field2;
                    txtHField3.Text = _Entity.Field3;
                    txtHField4.Text = _Entity.Field4;
                    txtHField5.Text = _Entity.Field5;
                    txtHField6.Text = _Entity.Field6;
                    txtHField7.Text = _Entity.Field7;
                    txtHField8.Text = _Entity.Field8;
                    txtHField9.Text = _Entity.Field9;


                }
                //if (Session["IsWithDetail"].ToString() == "False" && Request.QueryString["entry"].ToString() != "V")
              //  {
              //      gv1.DataSourceID = "sdsDetail";

                    //sdsDetail.SelectCommand = "SELECT * FROM WMS.PICKLISTDETAIL WHERE DocNumber is null";
                    //sdsDetail.SelectParameters.Remove(sdsDetail.SelectParameters["DocNumber"]);
             //   }
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
                look.ReadOnly = view;
            }
        }
        protected void gvLookupLoad(object sender, EventArgs e)//Control for all lookup in details/grid
        {
            if (Request.QueryString["entry"].ToString() == "V" || Request.QueryString["entry"].ToString() == "D")
            {
                ASPxGridLookup look = sender as ASPxGridLookup;
                look.Enabled = false;
            }
        }
        protected void gv1_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)//Control for grid
        {
            if (Request.QueryString["entry"].ToString() == "V" || Request.QueryString["entry"].ToString() == "D")
            {
                ASPxGridView grid = sender as ASPxGridView;
                grid.SettingsBehavior.AllowGroup = false;
                grid.SettingsBehavior.AllowSort = false;
                e.Editor.ReadOnly = view;
            }
        }
        protected void Date_Load(object sender, EventArgs e)//Control for all date editor
        {
            ASPxDateEdit date = sender as ASPxDateEdit;
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
                if (e.ButtonID == "Details" || e.ButtonID == "CountSheet")
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
            _Entity.Connection = Session["ConnString"].ToString(); //ADD CONN
            _Entity.ServiceType = txtServiceType.Text;
            _Entity.SequenceNumber = txtSequenceNumber.Text;
            _Entity.Type = txtType.Text;
            _Entity.Description = txtDescription.Text;
            _Entity.ServiceRate = seServiceRate.Text;
            _Entity.ServiceTypeCategoryCode = txtServiceTypeCategoryCode.Text;
            _Entity.SalesGLCode = glGL.Text;
            _Entity.SalesGLSubsiCode = glSubsi.Text;
            _Entity.ARGLCode = glARGL.Text;
            _Entity.ARGLSubsiCode = glARGLSubsi.Text;
            _Entity.IsStandard = Convert.ToBoolean(chckStandard.Value);
            _Entity.Field1 = txtHField1.Text;
            _Entity.Field2 = txtHField2.Text;
            _Entity.Field3 = txtHField3.Text;
            _Entity.Field4 = txtHField4.Text;
            _Entity.Field5 = txtHField5.Text;
            _Entity.Field6 = txtHField6.Text;
            _Entity.Field7 = txtHField7.Text;
            _Entity.Field8 = txtHField8.Text;
            _Entity.Field9 = txtHField9.Text;

            _Entity.AddedDate = txtAddedDate.Text;
            _Entity.LastEditedDate = txtLastEditedDate.Text;
            _Entity.ActivatedBy = txtActivatedBy.Text;
            _Entity.ActivatedDate = txtActivatedDate.Text;
            _Entity.DeActivatedBy = txtDeActivatedBy.Text;
            _Entity.DeActivatedDate = txtDeActivatedDate.Text;

            _Entity.LastEditedBy = Session["userid"].ToString();
            _Entity.AddedBy = Session["userid"].ToString();
            switch (e.Parameter)
            {
                case "Add":
                        check = true;
                        _Entity.InsertData(_Entity);//Method of inserting for header
                        
                   
                        Validate();
                        cp.JSProperties["cp_message"] = "Successfully Added!";//Message variable to client side
                     
                        cp.JSProperties["cp_close"] = true;//Close window variable to client side
                        Session["Refresh"] = "1";
                   

                    break;

                case "Update":
                    if (error == false)
                    {
                        check = true;
                        _Entity.UpdateData(_Entity);//Method of inserting for header
                       // odsDetail.SelectParameters["DocNumber"].DefaultValue = txtDocnumber.Text;//Set select parameter to prevent error
                      
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

                case "Close":
                    cp.JSProperties["cp_close"] = true;
                    Session["Refresh"] = "1";

                    break;
                case "ARGL":
                    glARGLSubsi.DataBind();
                    break;
                case "GL":
                    glSubsi.DataBind();
                    break;
            }
        }

      
  
        //dictionary method to hold error 
        void AddError(Dictionary<GridViewColumn, string> errors, GridViewColumn column, string errorText)
        {
            if (errors.ContainsKey(column)) return;
            errors[column] = errorText;
        }
      #endregion

   #region Validation
   private void Validate()
   {
       //GearsLibrary.Gears.GearsParameter gparam = new Gears.GearsParameter();
       //gparam._DocNo = _Entity.;
       //gparam._UserId = Session["Userid"].ToString();
       //gparam._TransType = "WMSIA";

       //string strresult = GWarehouseManagement.PickList_Validate(gparam);

       //cp.JSProperties["cp_valmsg"] = strresult;//Message variable to client side

   }
   #endregion


        protected void lookup_Init(object sender, EventArgs e)
        {
            ASPxGridLookup gridLookup = sender as ASPxGridLookup;
            gridLookup.GridView.CustomCallback += new ASPxGridViewCustomCallbackEventHandler(gridView_CustomCallback);
            if (Session["LocationExp"] != null)
            {
                gridLookup.GridView.DataSource = sdsGLSubsi;
                sdsGLSubsi.FilterExpression = Session["LocationExp"].ToString();
            }
        }
        public void gridView_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            string AccCode = e.Parameters;//Set column name
            if (AccCode.Contains("GLP_AIC") || AccCode.Contains("GLP_AC") || AccCode.Contains("GLP_F")) return;//Traps the callback

            ASPxGridView grid = sender as ASPxGridView;
            //ASPxGridLookup lookup = (ASPxGridLookup)grid.FindEditRowCellTemplateControl((GridViewDataColumn)grid.Columns[0], "glSalesSubsiCode");
            CriteriaOperator selectionCriteria = new InOperator("AccountCode", new string[] { AccCode });
            sdsGLSubsi.FilterExpression = (GroupOperator.Combine(GroupOperatorType.And, selectionCriteria)).ToString();
            Session["LocationExp"] = sdsGLSubsi.FilterExpression;
            grid.DataSource = sdsGLSubsi;
            grid.DataBind();
        }


        //protected void lookup_Init1(object sender, EventArgs e)
        //{
        //    ASPxGridLookup gridLookup = sender as ASPxGridLookup;
        //    gridLookup.GridView.CustomCallback += new ASPxGridViewCustomCallbackEventHandler(gridView_CustomCallback1);
        //    if (Session["LocationExp1"] != null)
        //    {
        //        gridLookup.GridView.DataSource = sdsGLSubsi1;
        //        sdsGLSubsi1.FilterExpression = Session["LocationExp1"].ToString();
        //    }
        //}
        //public void gridView_CustomCallback1(object sender, ASPxGridViewCustomCallbackEventArgs e)
        //{
        //    string AC = e.Parameters;//Set column name
        //    if (AC.Contains("GLP_AIC") || AC.Contains("GLP_AC") || AC.Contains("GLP_F")) return;//Traps the callback

        //    ASPxGridView grid = sender as ASPxGridView;
        //    //ASPxGridLookup lookup = (ASPxGridLookup)grid.FindEditRowCellTemplateControl((GridViewDataColumn)grid.Columns[0], "glSalesSubsiCode");
        //    CriteriaOperator selectionCriteria = new InOperator("AccountCode", new string[] { AC });
        //    sdsGLSubsi1.FilterExpression = (GroupOperator.Combine(GroupOperatorType.And, selectionCriteria)).ToString();
        //    Session["LocationExp1"] = sdsGLSubsi1.FilterExpression;
        //    grid.DataSource = sdsGLSubsi1;
        //    grid.DataBind();
        //}
    }
}