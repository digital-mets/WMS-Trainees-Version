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
    public partial class frmWIPreprocess : System.Web.UI.Page
    {
        Boolean error = false;//Boolean for grid validation
        Boolean view = false;//Boolean for view state

        private static string strError;

        Entity.PreProductionOut _Entity = new PreProductionOut();//Calls entity KPIRef
        Entity.PreProductionOut.PreProductionOutDetail _EntityDetail = new PreProductionOut.PreProductionOutDetail();//Call entity sdsDetail

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
                        view = true;//sets view mode for entry
                        updateBtn.Text = "Close";
                        glcheck.ClientVisible = false;
                        break;
                    case "D":
                        updateBtn.Text = "Delete";
                        break;
                }


                txtDocnumber.Value = Request.QueryString["docnumber"].ToString(); //sets docnumber from session



                _Entity.getdata(txtDocnumber.Text, Session["ConnString"].ToString()); //Method for retrieving data from entity



                dtpdocdate.Text = Convert.ToDateTime(_Entity.DocDate.ToString()).ToShortDateString();
                //txtType.Text = _Entity.Type.ToString();
                //glStep.Value = _Entity.Step.ToString();
                txtRemarks.Text = _Entity.Remarks.ToString();
                //txtHField1.Text = _Entity.Field1;
                //txtHField2.Text = _Entity.Field2;
                //txtHField3.Text = _Entity.Field3;
                //txtHField4.Text = _Entity.Field4;
                //txtHField5.Text = _Entity.Field5;
                //txtHField6.Text = _Entity.Field6;
                //txtHField7.Text = _Entity.Field7;
                //txtHField8.Text = _Entity.Field8;
                //txtHField9.Text = _Entity.Field9;
                txtHAddedBy.Text = _Entity.AddedBy;
                txtHAddedDate.Text = _Entity.AddedDate;
                txtHLastEditedBy.Text = _Entity.LastEditedBy;
                txtHLastEditedDate.Text = _Entity.LastEditedDate;

                //if (Request.QueryString["entry"].ToString() == "N")
                //{
                //    frmlayout1.FindItemOrGroupByName("ReferenceTransaction").ClientVisible = false;
                //}
                //else
                //{

                
              


                //    gvRef.DataSourceID = "odsReference";
                //    odsReference.SelectParameters["DocNumber"].DefaultValue = txtDocnumber.Text;
                //    this.gvRef.Columns["CommandString"].Width = 0;

                //    this.gvRef.Columns["RCommandString"].Width = 0;

                //}
                DataTable checkCount =_EntityDetail.getdetail(txtDocnumber.Text, Session["ConnString"].ToString());
                if (checkCount.Rows.Count > 0)
                {
                    gv1.KeyFieldName = "DocNumber;LineNumber";
                    gv1.DataSourceID = "odsDetail";
                }
                else
                {
                    gv1.KeyFieldName = "DocNumber;LineNumber";
                    gv1.DataSourceID = "sdsDetail";
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
        protected void CheckBoxLoad(object sender, EventArgs e)
        {
            var check = sender as ASPxCheckBox;
            check.ReadOnly = view;
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

        #region Validation
        private void Validate()
        {
            GearsLibrary.Gears.GearsParameter gparam = new Gears.GearsParameter();
            gparam._DocNo = _Entity.DocNumber;
            gparam._TransType = "PRDPPO";
            gparam._Connection = Session["ConnString"].ToString();
            string strresult = GearsProduction.GProduction.PreProductionOut_Validate(gparam);
  
            if (strresult != " " && strresult != "" && !String.IsNullOrEmpty(strresult))
            {
                cp.JSProperties["cp_valmsg"] = strresult + "\r\n" ;//Message variable to client side
            }
        }
        #endregion

        #region Callback functions(Inserting/Updating/Deleting/Validating)
        protected void cp_Callback(object sender, CallbackEventArgsBase e)
        {
            _Entity.Connection = Session["ConnString"].ToString(); 
            _Entity.DocNumber = txtDocnumber.Text;
            _Entity.DocDate = dtpdocdate.Text;
            //_Entity.Type = txtType.Text;
            //_Entity.Step = glStep.Text;
            _Entity.Remarks = txtRemarks.Text;
            //_Entity.Field1 = txtHField1.Text;
            //_Entity.Field2 = txtHField2.Text;
            //_Entity.Field3 = txtHField3.Text;
            //_Entity.Field4 = txtHField4.Text;
            //_Entity.Field5 = txtHField5.Text;
            //_Entity.Field6 = txtHField6.Text;
            //_Entity.Field7 = txtHField7.Text;
            //_Entity.Field8 = txtHField8.Text;
            //_Entity.Field9 = txtHField9.Text;
  
            _Entity.AddedBy = Session["userid"].ToString();
        
            string param = e.Parameter.Split('|')[0]; //Renats
            switch (param) //Renats
            {
                case "Add":
                    if (error == false)
                    {
                       
                       
                        _Entity.UpdateData(_Entity);
                          gv1.DataSourceID = "odsDetail";
                        
                        odsDetail.SelectParameters["DocNumber"].DefaultValue = txtDocnumber.Text;
                        gv1.UpdateEdit();
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
                    }
                    break;
                case "Update":
                    if (error == false)
                    {
                        _Entity.LastEditedBy = Session["userid"].ToString();

                        _Entity.LastEditedDate = DateTime.Now.ToString();
                        strError = Functions.Submitted(_Entity.DocNumber, "Production.PreProductionOut", 1, Session["ConnString"].ToString());//NEWADD factor 1 if submit, 2 if approve
                        if (!string.IsNullOrEmpty(strError))
                        {
                            gv1.JSProperties["cp_message"] = strError;
                            gv1.JSProperties["cp_success"] = true;
                            gv1.JSProperties["cp_forceclose"] = true;
                            return;
                        }

                        _Entity.UpdateData(_Entity);
                      
                        gv1.DataSourceID = "odsDetail";
                        odsDetail.SelectParameters["DocNumber"].DefaultValue = txtDocnumber.Text;
                      
                        gv1.UpdateEdit();
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
                    break;


                case "Step":
                    Gears.RetriveData2("DELETE FROM Production.PreProductionOutDetail where Docnumber='" + txtDocnumber.Text + "'", Session["ConnString"].ToString());
        
                    gv1.DataSourceID = "sdsDetail";
                    gv1.DataBind();
                    break;

               
            }
        }



        protected void Connection_Init(object sender, EventArgs e)
        {
            sdsJobOrder.ConnectionString = Session["ConnString"].ToString();
            sdsStep.ConnectionString = Session["ConnString"].ToString();
            sdsDetail.ConnectionString = Session["ConnString"].ToString();
        }
        protected void glSizeCode_Init(object sender, EventArgs e)
        {
            ASPxGridLookup gridLookup = sender as ASPxGridLookup;
            gridLookup.GridView.CustomCallback += new ASPxGridViewCustomCallbackEventHandler(rate_CustomCallback);
            if (Session["JobOrder"] != null)
            {
                gridLookup.GridView.DataSourceID = "sdsJobOrder";
                sdsJobOrder.FilterExpression = Session["JobOrder"].ToString();
                gridLookup.DataBind();
            }
        }

        public void rate_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
        //    ASPxGridView grid = sender as ASPxGridView;
        //    CriteriaOperator selectionCriteria = new InOperator("StepCode", new string[] { glStep.Text });
        //    sdsJobOrder.FilterExpression = (GroupOperator.Combine(GroupOperatorType.And, selectionCriteria)).ToString();
        //    Session["JobOrder"] = sdsJobOrder.FilterExpression;
        //    grid.DataSourceID = "sdsJobOrder";
        //    grid.DataBind();

        //    for (int i = 0; i < grid.VisibleRowCount; i++)
        //    {
        //        if (grid.GetRowValues(i, "StepCode") != null)
        //            if (grid.GetRowValues(i, "StepCode").ToString() == e.Parameters)
        //            {
        //                grid.Selection.SelectRow(i);
        //                string key = grid.GetRowValues(i, "StepCode").ToString();
        //                grid.MakeRowVisible(key);
        //                break;
        //            }
        //    }
        }

        protected void glItemCode_Init(object sender, EventArgs e)
        {
            ASPxGridLookup gridLookup = sender as ASPxGridLookup;
            gridLookup.GridView.CustomCallback += new ASPxGridViewCustomCallbackEventHandler(gridView_CustomCallback);

        }

        public void gridView_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            string column = e.Parameters.Split('|')[0];
            if (column.Contains("GLP_AIC") || column.Contains("GLP_AC") || column.Contains("GLP_F")) return;
            string itemcode = e.Parameters.Split('|')[1];
            string itemcode1 = e.Parameters.Split('|')[2];
            string val = e.Parameters.Split('|')[2];
            if (val.Contains("GLP_AIC") || val.Contains("GLP_AC") || val.Contains("GLP_F")) return;

            var itemlookup = sender as ASPxGridView;
            string codes = "";
            if (e.Parameters.Contains("Step"))
            {

                itemlookup.JSProperties["cp_codes"] = itemcode1;
            }
        }

     
   
        #endregion

        protected void dtpDocDate_Init(object sender, EventArgs e)
        {
            if (Request.QueryString["entry"] == "N")
            {
                dtpdocdate.Date = DateTime.Now;
            }
        }
    }
}