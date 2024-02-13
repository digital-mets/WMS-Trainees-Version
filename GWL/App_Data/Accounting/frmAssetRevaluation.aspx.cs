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
    public partial class frmAssetRevaluation : System.Web.UI.Page
    {
        Boolean error = false;//Boolean for grid validation
        Boolean view = false;//Boolean for view state
        Boolean check = false;//Boolean for grid validation
        Boolean edit = false; //Bolean for edit state

        private static string strError;

        Entity.AssetRevaluation _Entity = new AssetRevaluation();//Calls entity ICN
       
        #region page load/entry
        protected void Page_Load(object sender, EventArgs e)
        {
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

           // gv1.KeyFieldName = "DocNumber;LineNumber";

            //Details checking. changes datasourceid of gridview if there's no details for a certain docnumber; to enable adding of details.
            if (Request.QueryString["entry"].ToString() == "V")
            {
                view = true;//sets view mode for entry
            }

            switch (Request.QueryString["entry"].ToString())
            {
                case "N":
                    break;
                case "E":
                    updateBtn.Text = "Update";
                    edit = true;
                    break;
                case "V":
                    view = true;//sets view mode for entry
                    //txtDocnumber.Enabled = true;
                    //txtDocnumber.ReadOnly = false;
                    updateBtn.Text = "Close";
                    glcheck.ClientVisible = false;
                    break;
                case "D":
                    view = true;
                    updateBtn.Text = "Delete";
                    break;
            }


            if (!IsPostBack)
            {


                //txtDocnumber.ReadOnly = true;
                txtDocNumber.Value = Request.QueryString["docnumber"].ToString();
               
              
                
                if (Request.QueryString["entry"].ToString() == "N")
                {

                    //gv1.DataSourceID = "sdsDetail";
                    popup.ShowOnPageLoad = false;
                    //dtDocDate.Text = DateTime.Now.ToShortDateString();
                    //deExpDelDate.Text = DateTime.Now.ToShortDateString();
                    
                    spinNewBookValue.Text = "0.00";
                    spinNewRemainingLifeAsset.Text = "0.00";
                    
                }

                _Entity.getdata(txtDocNumber.Text, Session["ConnString"].ToString());//ADD CONN

                dtDocDate.Value = String.IsNullOrEmpty(_Entity.DocDate) ? DateTime.Now : Convert.ToDateTime(_Entity.DocDate);

                glPropertyNumber.Value = _Entity.PropertyNumber;
                txtRemarks.Value = _Entity.Remarks;

                if (String.IsNullOrEmpty(_Entity.LastEditedBy))
                {
                    updateBtn.Text = "Add";
                    frmlayout1.FindItemOrGroupByName("ReferenceTransaction").ClientVisible = false;
                }
                else
                {
                    updateBtn.Text = "Update";
                }


                spinQty.Value = _Entity.Qty;
                spinUnitCost.Value = _Entity.UnitCost;
                spinSalvageValue.Value = _Entity.SalvageValue;
                spinOtherCost.Value = _Entity.OtherCost;
                spinAccumulateDepreciation.Value = _Entity.AccumulatedDepreciation;
                spinTotalAcquisitionCost.Value = _Entity.TotalAcquisitionCost;


                spinNewBookValue.Value = _Entity.NewBookValue;
                spinNetBookValue.Value = _Entity.NetBookValue;
                cbNewDepreciationMethod.Value = _Entity.NewDepreciationMethod;
                spinNewMonthlyDepreciationAmount.Value = _Entity.NewMonthlyDepreciationAmount;
                spinNewRemainingLifeAsset.Value = _Entity.NewRemainingLifeAsset;

        
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
                txtPostedBy.Text = _Entity.PostedBy;
                txtPostedDate.Text = _Entity.PostedDate;


                SetEnables();
                gvJournal.DataSourceID = "odsJournalEntry";


                gvRef.DataSourceID = "odsReference";
                this.gvRef.Columns["CommandString"].Width = 0;
                this.gvRef.Columns["RCommandString"].Width = 0;
            }
        }


        private void SetEnables()
        {
            if(String.IsNullOrEmpty(glPropertyNumber.Text))
            {
                //cp.JSProperties["cp_selected"] = "0";
                cbNewDepreciationMethod.ClientEnabled = false;
                spinNewBookValue.ClientEnabled = false;
                spinNewRemainingLifeAsset.ClientEnabled = false;
                spinNewBookValue.ClientEnabled = false;
                cbNewDepreciationMethod.Text = "";
                spinNewBookValue.Text = "0.00";
                spinNewRemainingLifeAsset.Text = "0";
            }

            else
            {
                cbNewDepreciationMethod.ClientEnabled = true;
                spinNewBookValue.ClientEnabled = true;
                spinNewRemainingLifeAsset.ClientEnabled = true;
                spinNewBookValue.ClientEnabled = true;
                //cp.JSProperties["cp_selected"] = "1";
                if (String.IsNullOrEmpty(cbNewDepreciationMethod.Text))
                    cbNewDepreciationMethod.SelectedIndex = 1;
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
            gparam._TransType = "ACTRVL";
            gparam._Table = "Accounting.AssetRevaluation";
            gparam._Factor = -1;
            string strresult = GearsAccounting.GAccounting.AssetRevaluation_Post(gparam);
            if (strresult != " ")
            {
                cp.JSProperties["cp_valmsg"] = strresult;//Message variable to client side
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
            gparam._TransType = "ACTRVL";

            string strresult = GearsAccounting.GAccounting.AssetRevaluation_Validate(gparam);

            cp.JSProperties["cp_valmsg"] = strresult;//Message variable to client side
            
        }
        #endregion

        #region Set controls' state/behavior/etc...
        protected void TextboxLoad(object sender, EventArgs e)//Control for all textbox
        {
            ASPxTextBox text = sender as ASPxTextBox;
            text.ReadOnly = view;
        }


        protected void dtpDocDate_Init(object sender, EventArgs e)
        {
            if (Request.QueryString["entry"] == "N")
            {
                dtDocDate.Date = DateTime.Now;
            }
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

        protected void ComboBox_Load(object sender, EventArgs e)
        {
            ASPxComboBox combobox = sender as ASPxComboBox;
            combobox.DropDownButton.Enabled = !view;
            combobox.Enabled = !view;
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

            if (!IsPostBack)
            {
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
                if (Request.QueryString["entry"].ToString() == "V")
                {
                    if (e.ButtonType == ColumnCommandButtonType.Edit || e.ButtonType == ColumnCommandButtonType.Delete ||
                        e.ButtonType == ColumnCommandButtonType.New || e.ButtonType == ColumnCommandButtonType.Cancel ||
                        e.ButtonType == ColumnCommandButtonType.Update)
                        e.Visible = false;
                }
                if (e.ButtonType == ColumnCommandButtonType.Update)
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

            _Entity.DocNumber     = txtDocNumber.Text;
            _Entity.Connection = Session["ConnString"].ToString(); //ADD CONN
            _Entity.DocDate       = dtDocDate.Text;
            _Entity.PropertyNumber = glPropertyNumber.Text;
            _Entity.Remarks = txtRemarks.Text;


            _Entity.Qty = Convert.ToDecimal(spinQty.Value);
            _Entity.UnitCost = Convert.ToDecimal(spinUnitCost.Value);
            _Entity.SalvageValue = Convert.ToDecimal(spinSalvageValue.Value);
            _Entity.OtherCost = Convert.ToDecimal(spinOtherCost.Value);
            _Entity.AccumulatedDepreciation = Convert.ToDecimal(spinAccumulateDepreciation.Value);
            _Entity.TotalAcquisitionCost = Convert.ToDecimal(spinTotalAcquisitionCost.Value);


            _Entity.NewBookValue = Convert.ToDecimal(spinNewBookValue.Value);
            _Entity.NetBookValue = Convert.ToDecimal(spinNetBookValue.Value);
            _Entity.NewDepreciationMethod = cbNewDepreciationMethod.Text;
            _Entity.NewMonthlyDepreciationAmount = Convert.ToDecimal(spinNewMonthlyDepreciationAmount.Value);
            _Entity.NewRemainingLifeAsset = Convert.ToDecimal(spinNewRemainingLifeAsset.Value);




            
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
                    strError = Functions.Submitted(_Entity.DocNumber,"Accounting.AssetRevaluation",1,Session["ConnString"].ToString());//NEWADD factor 1 if submit, 2 if approve
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


                    ////    gv1.DataSourceID = "odsDetail";
                    //    odsDetail.SelectParameters["DocNumber"].DefaultValue = txtDocnumber.Text;
                      //  gv1.UpdateEdit();//2nd initiation to insert grid
                        Post();
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
                    strError = Functions.Submitted(_Entity.DocNumber,"Accounting.AssetRevaluation",1,Session["ConnString"].ToString());//NEWADD factor 1 if submit, 2 if approve
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
                        Gears.RetriveData2("Update Accounting.AssetRevaluation set UpdatedLife = (select COUNT(PostedDate) + '" + spinNewRemainingLifeAsset.Text + "' from Accounting.AssetInvDetail where ISNULL(PostedDate,0001-01-01)!=0001-01-01 and PropertyNumber ='" + glPropertyNumber.Text + "') where DocNumber = '" + txtDocNumber.Text + "'", Session["ConnString"].ToString());//ADD CONN

                        Post();
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
                    Session["Refresh"] = "1";
                    break;

                case "PropertyNumberCase":
                    setText();
                    SetEnables();
                    break;

                case "GetUsedLife":
                    GetUsedLife();
                    SetEnables();
                    break;

            }
        }

        
        protected void setText()
        {
            DataTable propertydata = Gears.RetriveData2("SELECT ISNULL(Qty,0) AS Qty,ISNULL(UnitCost,0) AS UnitCost,ISNULL(SalvageValue, 0) AS SalvageValue,ISNULL(OtherCost,0) AS OtherCost,ISNULL(AccumulatedDepreciation,0) AS AccumulatedDepreciation,ISNULL(AcquisitionCost,0) AS AcquisitionCost,(AcquisitionCost - AccumulatedDepreciation) AS NetBookValue,BookValue,Life,MonthlyDepreciation,PropertyNumber FROM Accounting.AssetInv where PropertyNumber = '" + glPropertyNumber.Text + "'", Session["ConnString"].ToString());//ADD CONN
            foreach (DataRow dt in propertydata.Rows)
            {
                spinQty.Value = dt[0].ToString();
                spinUnitCost.Value = dt[1].ToString();
                spinSalvageValue.Value = dt[2].ToString();
                spinOtherCost.Value = dt[3].ToString();
                spinAccumulateDepreciation.Value = dt[4].ToString();
                spinTotalAcquisitionCost.Value = dt[5].ToString();
                spinNetBookValue.Value = dt[6].ToString();
                spinNewBookValue.Value = dt[7].ToString();
                spinNewRemainingLifeAsset.Value = dt[8].ToString();
                spinNewMonthlyDepreciationAmount.Value = dt[9].ToString();
            }
            cbNewDepreciationMethod.Enabled = true;
        }

        protected void GetUsedLife()
        {
            DataTable getusedlife = Gears.RetriveData2("select COUNT(PostedDate) AS NumberofPostedDate from Accounting.AssetInvDetail where ISNULL(PostedDate,0001-01-01)!=0001-01-01 and PropertyNumber='" + glPropertyNumber.Text + "'", Session["ConnString"].ToString());//ADD CONN

            if (getusedlife.Rows.Count > 0)
            {
                cp.JSProperties["cp_usedlife"] = getusedlife.Rows[0]["NumberofPostedDate"].ToString();
            }

            else
            {
                cp.JSProperties["cp_usedlife"] = "0";
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

        //public void setOldDueDate()
        //{
        //    SqlDataSource ds = SODocNumberlookup;
        //    ds.SelectCommand = string.Format("SELECT DocNumber from Sales.SalesOrder where DocNumber = '" + glSODocNumber.Text + "'");
        //    DataView sodocnum = (DataView)ds.Select(DataSourceSelectArguments.Empty);
        //    if (sodocnum.Count > 0)
        //    {
        //        glSODocNumber.Text = sodocnum[0][0].ToString();
        //    }

        //    ds.SelectCommand = string.Format("SELECT CONVERT(DATE,TargetDeliveryDate,101) AS TargetDeliveryDate,DocNumber FROM Sales.SalesOrder where DocNumber = '" + glSODocNumber.Text + "'");
        //    DataView duedate = (DataView)ds.Select(DataSourceSelectArguments.Empty);
        //    if (duedate.Count > 0)
        //    {
        //       // txtOldDueDate.Text = duedate[0][0].ToString();
        //        txtOldDueDate.Text = String.IsNullOrEmpty(duedate[0][0].ToString()) ? null : Convert.ToDateTime(duedate[0][0].ToString()).ToShortDateString();
        //    }


        //}
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

            PropertyNumberLookup.ConnectionString = Session["ConnString"].ToString();              
        }

    }
}