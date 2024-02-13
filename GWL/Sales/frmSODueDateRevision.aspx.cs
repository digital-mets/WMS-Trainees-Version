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
using GearsProcurement;
using GearsSales;

namespace GWL
{
    public partial class frmSODueDateRevision : System.Web.UI.Page
    {
        Boolean error = false;//Boolean for grid validation
        Boolean view = false;//Boolean for view state
        Boolean check = false;//Boolean for grid validation
        Boolean edit = false; //Bolean for edit state
        Boolean isnotvalid = false;

        private static string strError;

        Entity.SODueDateRevision _Entity = new SODueDateRevision();//Calls entity ICN
       
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

            dtDocDate.MinDate = Convert.ToDateTime(GearsCommon.GCommon.SystemSetting("BookDate", Session["ConnString"].ToString()).ToString());

            if (referer == null)
            {
                Response.Redirect("~/error.aspx");
            }

           // gv1.KeyFieldName = "DocNumber;LineNumber";

            //Details checking. changes datasourceid of gridview if there's no details for a certain docnumber; to enable adding of details.
            if (Request.QueryString["entry"].ToString() == "V")
            {
                view = true;//sets view mode for entry
            }



            if (!IsPostBack)
            {
                Session["Status"] = null;

                txtDocnumber.ReadOnly = true;
                txtDocnumber.Value = Request.QueryString["docnumber"].ToString();

                _Entity.getdata(txtDocnumber.Text, Session["ConnString"].ToString());//ADD CONN

                    // Moved inside !IsPostBack   LGE 03 - 02 - 2016
                    //V=View, E=Edit, N=New
                    switch (Request.QueryString["entry"].ToString())
                    {
                        case "N":
                            if (string.IsNullOrEmpty(_Entity.LastEditedBy))
                            {
                                updateBtn.Text = "Add";
                                frmlayout1.FindItemOrGroupByName("ReferenceTransaction").ClientVisible = false;
                            }
                            else
                            {
                                updateBtn.Text = "Update";
                            }
                            break;
                        case "E":
                            updateBtn.Text = "Update";
                            edit = true;
                            break;
                        case "V":
                            view = true;//sets view mode for entry
                            txtDocnumber.Enabled = true;
                            txtDocnumber.ReadOnly = false;
                            updateBtn.Text = "Close";
                            glcheck.ClientVisible = false;
                            break;
                        case "D":
                            view = true;
                            glcheck.ClientVisible = false;
                            updateBtn.Text = "Delete";
                            break;
                    }

                    dtDocDate.Value = String.IsNullOrEmpty(_Entity.DocDate) ? DateTime.Now : Convert.ToDateTime(_Entity.DocDate);
                    glSODocNumber.Value = _Entity.SONumber;
                    txtRemarks.Value = _Entity.Remarks;

                    dtOldDueDate.Text = String.IsNullOrEmpty(_Entity.OldDueDate.ToString()) ? null : Convert.ToDateTime(_Entity.OldDueDate.ToString()).ToShortDateString();
                    dtNewDueDate.Text = String.IsNullOrEmpty(_Entity.NewDueDate.ToString()) ? null : Convert.ToDateTime(_Entity.NewDueDate.ToString()).ToShortDateString();
                
                    txtHField1.Value = _Entity.Field1;
                    txtHField2.Value = _Entity.Field2;
                    txtHField3.Value = _Entity.Field3;
                    txtHField4.Value = _Entity.Field4;
                    txtHField5.Value = _Entity.Field5;
                    txtHField6.Value = _Entity.Field6;
                    txtHField7.Value = _Entity.Field7;
                    txtHField8.Value = _Entity.Field8;
                    txtHField9.Value = _Entity.Field9;

                    txtHAddedBy.Text = _Entity.AddedBy;
                    txtHAddedDate.Text = _Entity.AddedDate;
                    txtHLastEditedBy.Text = _Entity.LastEditedBy;
                    txtHLastEditedDate.Text = _Entity.LastEditedDate;
                    txtHSubmittedBy.Text = _Entity.SubmittedBy;
                    txtHSubmittedDate.Text = _Entity.SubmittedDate;
                    txtHCancelledBy.Text = _Entity.CancelledBy;
                    txtHCancelledDate.Text = _Entity.CancelledDate;

                    if (Request.QueryString["entry"].ToString() == "N")
                    {

                        //gv1.DataSourceID = "sdsDetail";
                        
                        dtDocDate.Text = DateTime.Now.ToShortDateString();
                        //deExpDelDate.Text = DateTime.Now.ToShortDateString();
                       // frmlayout1.FindItemOrGroupByName("ReferenceTransaction").ClientVisible = false;
                    }

                    gvRef.DataSourceID = "odsReference";
                    this.gvRef.Columns["CommandString"].Width = 0;
                    this.gvRef.Columns["RCommandString"].Width = 0;


                //}
                //if (Request.QueryString["IsWithDetail"].ToString() == "False" && Request.QueryString["entry"].ToString() != "V")
                //{
                //    gv1.DataSourceID = "sdsDetail";

                //    sdsDetail.SelectCommand = "SELECT * FROM Procurement.PurchaseRequestDetail WHERE DocNumber is null";
                //    sdsDetail.SelectParameters.Remove(sdsDetail.SelectParameters["DocNumber"]);
                //}
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
            gparam._TransType = "SLSSOD";

            string strresult = GearsSales.GSales.ChangeSODueDate_Validate(gparam);

            cp.JSProperties["cp_valmsg"] = strresult;//Message variable to client side
            
        }
        #endregion

        #region Set controls' state/behavior/etc...
        protected void TextboxLoad(object sender, EventArgs e)//Control for all textbox
        {
            ASPxTextBox text = sender as ASPxTextBox;
            text.ReadOnly = view;
        }

        protected void MemoLoad(object sender, EventArgs e)//Control for all textbox
        {
            ASPxMemo text = sender as ASPxMemo;
            text.ReadOnly = view;
        }
        protected void LookupLoad(object sender, EventArgs e)//Control for all lookup in header
        {

            ASPxGridLookup look = sender as ASPxGridLookup;
            look.DropDownButton.Enabled = !view;
            look.ReadOnly = view;
            //var look = sender as ASPxGridLookup;
            //if (look != null)
            //{
            //    look.ReadOnly = view;
            //}
        }

        protected void CostCenterLookupLoad(object sender, EventArgs e)
        {
            if(edit != false)
            {
                ASPxGridLookup look = sender as ASPxGridLookup;
                look.DropDownButton.Enabled = !edit;
                look.ReadOnly = edit;
            }
            else
            {
                ASPxGridLookup look = sender as ASPxGridLookup;
                look.DropDownButton.Enabled = !view;
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

        protected void CheckBoxLoad(object sender, EventArgs e)
        {
            var check = sender as ASPxCheckBox;
            check.ReadOnly = view;
        }

        protected void gv1_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)//Control for grid
        {
            //if (Request.QueryString["entry"].ToString() == "V" || Request.QueryString["entry"].ToString() == "D")
            //{
            //    ASPxGridView grid = sender as ASPxGridView;
            //    grid.SettingsBehavior.AllowGroup = false;
            //    grid.SettingsBehavior.AllowSort = false;
            //    e.Editor.ReadOnly = view;
            //}
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

            //if (!IsPostBack)
            //{
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

                if (Request.QueryString["entry"].ToString() == "V")
                {
                    if ( e.ButtonType == ColumnCommandButtonType.Delete ||
                        e.ButtonType == ColumnCommandButtonType.New || 
                        e.ButtonType == ColumnCommandButtonType.Update)
                        e.Visible = false;
                }
            //}

                if (e.ButtonType == ColumnCommandButtonType.Cancel || e.ButtonType == ColumnCommandButtonType.Edit)
                {
                    e.Visible = false;
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
        }
        #endregion

 
        
        #region Callback functions(Inserting/Updating/Deleting/Validating)
        protected void cp_Callback(object sender, CallbackEventArgsBase e)
        {

            _Entity.DocNumber     = txtDocnumber.Text;
            _Entity.Connection = Session["ConnString"].ToString(); //ADD CONN
            _Entity.DocDate       = dtDocDate.Text;
            _Entity.SONumber   = glSODocNumber.Text;
            _Entity.Remarks       = txtRemarks.Text;
            _Entity.OldDueDate = dtOldDueDate.Text;
            _Entity.NewDueDate = dtNewDueDate.Text;

            
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
            _Entity.LastEditedDate = DateTime.Now.ToString();


            switch (e.Parameter)
            {
                case "Add":

                  //  gv1.UpdateEdit();//Initiate to call gridvalidation/batchupdate method
                   strError = Functions.Submitted(_Entity.DocNumber,"Sales.SODueDateRevision",1,Session["ConnString"].ToString());//NEWADD factor 1 if submit, 2 if approve
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
                        _Entity.AddedBy = Session["userid"].ToString();
                        _Entity.InsertData(_Entity);//Method of inserting for header
                        _Entity.UpdateData(_Entity);


                    //    gv1.DataSourceID = "odsDetail";
                        odsDetail.SelectParameters["DocNumber"].DefaultValue = txtDocnumber.Text;
                      //  gv1.UpdateEdit();//2nd initiation to insert grid
                        Validate();
                        cp.JSProperties["cp_message"] = "Successfully Added!";//Message variable to client side
                        cp.JSProperties["cp_success"] = true;//Success bool variable  to client side
                        cp.JSProperties["cp_close"] = true;//Close window variable to client side
                        Session["Refresh"] = "1";
                    }
                    else
                    {
                        cp.JSProperties["cp_message"] = "Please check all the fields!";
                        cp.JSProperties["cp_success"] = true;
                    }

                    break;

                case "Update":

                    strError = Functions.Submitted(_Entity.DocNumber,"Sales.SODueDateRevision",1,Session["ConnString"].ToString());//NEWADD factor 1 if submit, 2 if approve
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
                        _Entity.UpdateData(_Entity);//Method of Updating header


  //                      gv1.DataSourceID = "odsDetail";
                        odsDetail.SelectParameters["DocNumber"].DefaultValue = txtDocnumber.Text; 
    //                    gv1.UpdateEdit();//2nd Initiation to update grid
                        Validate();
                        cp.JSProperties["cp_message"] = "Successfully Updated!";
                        cp.JSProperties["cp_success"] = true;
                        cp.JSProperties["cp_close"] = true;
                        Session["Refresh"] = "1";
                    }
                    else
                    {
                        if(isnotvalid == true)
                        {
                            cp.JSProperties["cp_message"] = Session["CheckStatus"];
                            cp.JSProperties["cp_isnotvalid"] = true;
                        }
                        else
                        { 
                            cp.JSProperties["cp_message"] = "Please check all the fields!";
                            cp.JSProperties["cp_success"] = true;
                        }
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

                case "SoDueDate":
                    try
                    {
                        if (!String.IsNullOrEmpty(glSODocNumber.Value.ToString()))
                        {
                            setOldDueDate();
                        }
                    }
                    catch(Exception er)
                    {
                        dtOldDueDate.Text = "";
                    }

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
            if ((error == true || error == false) && check == false)//Prevents updating of grid to enable validation
            {
                e.Handled = true;
                //e.DeleteValues.Clear();
                e.InsertValues.Clear();
                e.UpdateValues.Clear();
            }
        }
        #endregion

        public void setOldDueDate()
        {
            DataTable getDueDate = Gears.RetriveData2("SELECT CONVERT(DATE,TargetDeliveryDate,101) AS TargetDeliveryDate FROM Sales.SalesOrder where DocNumber = '" + glSODocNumber.Text + "'", Session["ConnString"].ToString());//ADD CONN
            if (getDueDate.Rows.Count > 0)
            {
                //txtOldDueDate.Text = getDueDate.Rows[0]["TargetDeliveryDate"].ToString();
                dtOldDueDate.Text = String.IsNullOrEmpty(getDueDate.Rows[0]["TargetDeliveryDate"].ToString()) ? null : Convert.ToDateTime(getDueDate.Rows[0]["TargetDeliveryDate"].ToString()).ToShortDateString();
                //dtNewDueDate.Text = String.IsNullOrEmpty(getDueDate.Rows[0]["TargetDeliveryDate"].ToString()) ? null : Convert.ToDateTime(getDueDate.Rows[0]["TargetDeliveryDate"].ToString()).ToShortDateString();
            }

            //else
            //{
            //    vatcode = "";
            //}

            //ds.SelectCommand = string.Format("SELECT CONVERT(DATE,TargetDeliveryDate,101) AS TargetDeliveryDate,DocNumber FROM Sales.SalesOrder where DocNumber = '" + glSODocNumber.Text + "'");
            //DataView duedate = (DataView)ds.Select(DataSourceSelectArguments.Empty);
            //if (duedate.Count > 0)
            //{
            //   // txtOldDueDate.Text = duedate[0][0].ToString();
            //    txtOldDueDate.Text = String.IsNullOrEmpty(duedate[0][0].ToString()) ? null : Convert.ToDateTime(duedate[0][0].ToString()).ToShortDateString();
            //}


        }
        protected void glWarehouseCOde_TextChanged(object sender, EventArgs e)
        {

        }

        protected void glOCN_TextChanged(object sender, EventArgs e)
        {

        }

        protected void sup_cp_Callback(object sender, CallbackEventArgsBase e)
        {
       //       //This is the datasource where we will get the connection string
       //     SqlDataSource ds = OCN;
       //     //This is the sql command that we will initiate to find the data that we want to set to the textbox.
       //// (the e.Parameter is the callback item that we sent to the server)
       //     ds.SelectCommand = string.Format("SELECT OutgoingDocType FROM WMS.[ICN] WHERE DocNumber = '" + e.Parameter + "'");
       // //This is where we now initiate the command and get the data from it using dataview	
       //     DataView biz = (DataView)ds.Select(DataSourceSelectArguments.Empty);
       //     if (biz.Count > 0)
       //     {
       // //Now, this is the part where we assign the following data to the textbox
       //         //txttype.Text = biz[0][0].ToString();
       //     }
        }

        protected void Connection_Init(object sender, EventArgs e)
        {
            sdsDetail.ConnectionString = Session["ConnString"].ToString();
            Masterfileitem.ConnectionString = Session["ConnString"].ToString();
            Masterfileitemdetail.ConnectionString = Session["ConnString"].ToString();

            OldDueDatelookup.ConnectionString = Session["ConnString"].ToString();
            SODocNumberlookup.ConnectionString = Session["ConnString"].ToString();              
        }

    }
}