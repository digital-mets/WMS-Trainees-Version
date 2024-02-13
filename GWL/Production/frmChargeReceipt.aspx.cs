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
    public partial class frmChargeReceipt : System.Web.UI.Page
    {
        Boolean error = false;//Boolean for grid validation
        Boolean view = false;//Boolean for view state
        Boolean check = false;//Boolean for grid validation
        string a = ""; //Renats
        string b = ""; //Renats
        string c = ""; //Renats
        private static string strError;

        Entity.ChargeReceipt _Entity = new ChargeReceipt();//Calls entity KPIRef
        Entity.ChargeReceipt.CRDetail _EntityDetail = new ChargeReceipt.CRDetail();
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
                Session["WorkCenter"] = null;

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
                //gljoborder.Text = _Entity.JobOrder.ToString();

                
                glChargeTo.Value = _Entity.WorkCenter.ToString();
                glPayTo.Value = _Entity.PayTo.ToString();
                //txtStep.Text = _Entity.Step.ToString();
                //txtDescCharge.Text = _Entity.DescCharge.ToString();
                speQty.Text= _Entity.Qty.ToString();
                //spePrice.Value= _Entity.Price.ToString();
                //speAmount.Value = _Entity.Amount.ToString();
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

   
                DataTable checkCount = _EntityDetail.getdetail(txtDocnumber.Text, Session["ConnString"].ToString());
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
                //gvJournal.DataSourceID = "odsJournalEntry";
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
        string strresult = "";
        private void Validate()
        {
            GearsLibrary.Gears.GearsParameter gparam = new Gears.GearsParameter();
            gparam._DocNo = _Entity.DocNumber;
            gparam._TransType = "PRDCRT";
            gparam._Connection = Session["ConnString"].ToString();
            strresult += GearsProduction.GProduction.ChargeReceipt_Validate(gparam);
    
            if (strresult != " ")
            {
                cp.JSProperties["cp_valmsg"] = strresult + "\r\n";//Message variable to client side
            }
        }
        #endregion
        #region Post
        private void Post()
        {
            GearsLibrary.Gears.GearsParameter gparam = new Gears.GearsParameter();
            gparam._DocNo = _Entity.DocNumber;
            gparam._UserId = Session["Userid"].ToString();
            gparam._TransType = "PRDCRT";
            gparam._Table = "Production.ChargeReceipt";
            gparam._Factor = -1;
            gparam._Connection = Session["ConnString"].ToString();
            strresult += GearsProduction.GProduction.ProdChargeReceipt_Post(gparam);
            if (strresult != " ")
            {
                cp.JSProperties["cp_valmsg"] = strresult;//Message variable to client side
            }
        }
        #endregion

        #region Callback functions(Inserting/Updating/Deleting/Validating)
        protected void cp_Callback(object sender, CallbackEventArgsBase e)
        {

  

           
            _Entity.Connection = Session["ConnString"].ToString();
            _Entity.DocNumber = txtDocnumber.Text;
            _Entity.DocDate = dtpdocdate.Text;
          
            _Entity.WorkCenter = glChargeTo.Text;
            _Entity.PayTo = glPayTo.Text;
            _Entity.Qty = Convert.ToDecimal(Convert.IsDBNull(speQty.Value) ? "0" : speQty.Value);
         
            //_Entity.Amount = Convert.ToDecimal(Convert.IsDBNull(speAmount.Value) ? "0" : speAmount.Value);

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
                        check = true;

                        _Entity.UpdateData(_Entity);
                        gv1.DataSourceID = "odsDetail";

                        odsDetail.SelectParameters["DocNumber"].DefaultValue = txtDocnumber.Text;
                        gv1.UpdateEdit();
                        Validate(); Post();

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
                        check = true;
                        _Entity.LastEditedBy = Session["userid"].ToString();

                        _Entity.LastEditedDate = DateTime.Now.ToString();
                        strError = Functions.Submitted(_Entity.DocNumber, "Production.ChargeReceipt", 1, Session["ConnString"].ToString());//NEWADD factor 1 if submit, 2 if approve
                        if (!string.IsNullOrEmpty(strError))
                        {
                            cp.JSProperties["cp_message"] = strError;
                            cp.JSProperties["cp_success"] = true;
                            cp.JSProperties["cp_forceclose"] = true;
                            return;
                        }
                        _Entity.UpdateData(_Entity);
                        gv1.DataSourceID = "odsDetail";

                        odsDetail.SelectParameters["DocNumber"].DefaultValue = txtDocnumber.Text;
                        gv1.UpdateEdit();
                        Validate(); Post();

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



            }
        }



        protected void Connection_Init(object sender, EventArgs e)
        {
            ((SqlDataSource)sender).ConnectionString = Session["ConnString"].ToString();

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
            string column = e.Parameters.Split('|')[0];
            if (column.Contains("GLP_AIC") || column.Contains("GLP_AC") || column.Contains("GLP_F")) return;
          
            string val = e.Parameters.Split('|')[1];//Set column value
            if (val.Contains("GLP_AIC") || val.Contains("GLP_AC") || val.Contains("GLP_F")) return;//Traps the callback


            ASPxGridLookup lookup = (ASPxGridLookup)gv1.FindEditRowCellTemplateControl((GridViewDataTextColumn)gv1.Columns[3], "glSizeCode");


            ASPxGridView grid = sender as ASPxGridView;
            CriteriaOperator selectionCriteria = new InOperator("WorkCenter", new string[] { glChargeTo.Text });
            sdsJobOrder.FilterExpression = (GroupOperator.Combine(GroupOperatorType.And, selectionCriteria)).ToString();
            Session["JobOrder"] = sdsJobOrder.FilterExpression;
            grid.DataSourceID = "sdsJobOrder";
            grid.DataBind();
            if (val != "")
            {
                for (int i = 0; i < grid.VisibleRowCount; i++)
                {
                    if (grid.GetRowValues(i, "StepCode") != null)
                        if (grid.GetRowValues(i, "StepCode").ToString() == e.Parameters)
                        {
                            grid.Selection.SelectRow(i);
                            string key = grid.GetRowValues(i, "StepCode").ToString();
                            grid.MakeRowVisible(key);
                            break;
                        }
                }
            }
            if (val == "")
            {
                lookup.Value = null;
            }
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
            string val = e.Parameters.Split('|')[2];//Set column value
            if (val.Contains("GLP_AIC") || val.Contains("GLP_AC") || val.Contains("GLP_F")) return;//Traps the callback

            string itemcode1 = e.Parameters.Split('|')[3];
            string itemc = e.Parameters.Split('|')[4];
            string colorcode = e.Parameters.Split('|')[5];//Set column value
            //if (col.Contains("GLP_AIC") || col.Contains("GLP_AC") || col.Contains("GLP_F")) return;//Traps the callback
            //end

            //for class
            string classcode = e.Parameters.Split('|')[6];//Set column value
            //if (cls.Contains("GLP_AIC") || cls.Contains("GLP_AC") || cls.Contains("GLP_F")) return;//Traps the callback
            //end

            //for size
            string sizecode = e.Parameters.Split('|')[7];//Set column value
            //if (cls.Contains("GLP_AIC") || cls.Contains("GLP_AC") || cls.Contains("GLP_F")) return;//Traps the callback
            //end

 

            var itemlookup = sender as ASPxGridView;
            string codes = "";
            if (e.Parameters.Contains("Step"))
            {

                itemlookup.JSProperties["cp_codes"] = val;
                //sdsItemDetail.SelectCommand = "SELECT DISTINCT ItemCode,StepCode,DocNumber FROM Production.JOMaterialMovement  WHERE  Docnumber  = '" + itemcode + "' AND StepCode = '" + val + "' and ISNULL(IsChargedAlready,0)=0";
                //ASPxGridView grid = sender as ASPxGridView;
                //var selectedValues = itemcode;
                //grid.DataSourceID = "sdsItemDetail";
                //grid.DataBind();
            }
            else
            {
                if (e.Parameters.Contains("ItemCode"))
                {




                    //for (int i = 0; i < grid.VisibleRowCount; i++)
                    //{
                    //    if (grid.GetRowValues(i, column) != null)
                    //        if (grid.GetRowValues(i, column).ToString() == column)
                    //        {
                    //            grid.Selection.SelectRow(i);
                    //            string key = grid.GetRowValues(i, column).ToString();
                    //            grid.MakeRowVisible(key);
                    //            break;
                    //        }
                    //}


                    DataTable countcolor = Gears.RetriveData2("SELECT DISTINCT A.ItemCode, ColorCode,ClassCode,SizeCode FROM "
                        + " Production.JOMaterialMovement  A  WHERE A.ItemCode = '" + val + "' AND Docnumber  = '" + itemcode + "' AND StepCode = '" + itemcode1 + "'", Session["ConnString"].ToString());

                    DataTable dtcolor = Gears.RetriveData2("SELECT ColorCode, COUNT(ColorCode) as ColorCode FROM Production.JOMaterialMovement  WHERE ItemCode  = '" + val + "'  AND Docnumber  = '" + itemcode + "' AND StepCode = '" + itemcode1 + "' GROUP BY ColorCode HAVING COUNT(ColorCode)>1", Session["ConnString"].ToString());

                    DataTable dtclass = Gears.RetriveData2("SELECT ClassCode, COUNT(ClassCode) FROM Production.JOMaterialMovement  WHERE ItemCode  = '" + val + "'  AND Docnumber  = '" + itemcode + "' AND StepCode = '" + itemcode1 + "'  GROUP BY ClassCode HAVING COUNT(ClassCode)>1", Session["ConnString"].ToString());
                    DataTable dtsize = Gears.RetriveData2("SELECT SizeCode, COUNT(SizeCode) FROM Production.JOMaterialMovement  WHERE ItemCode  = '" + val + "'  AND Docnumber  = '" + itemcode + "' AND StepCode = '" + itemcode1 + "'   GROUP BY SizeCode HAVING COUNT(SizeCode)>1", Session["ConnString"].ToString());

                    if (countcolor.Rows.Count == 1)
                    {
                        foreach (DataRow dt in countcolor.Rows)
                        {
                            codes = dt["ColorCode"].ToString() + ";";
                            codes += dt["ClassCode"].ToString() + ";";
                            codes += dt["SizeCode"].ToString() + ";";

                        }
                    }
                    else
                    {
                        if (dtcolor.Rows.Count == 1)
                        {

                            codes = dtcolor.Rows[0][0].ToString() + ";";
                        }
                        else
                        {
                            codes = "" + ";";
                        }
                        if (dtclass.Rows.Count == 1)
                        {

                            codes += dtclass.Rows[0][0].ToString() + ";";
                        }
                        else
                        {
                            codes += "" + ";";
                        }

                        if (dtsize.Rows.Count == 1)
                        {

                            codes += dtsize.Rows[0][0].ToString() + ";";
                        }
                        else
                        {
                            codes += "" + ";";
                        }


                    }



                    itemlookup.JSProperties["cp_identifier"] = "item";
                    itemlookup.JSProperties["cp_codes"] = codes;
                }

                else
                {
                    ASPxGridView grid = sender as ASPxGridView;

                    if (e.Parameters.Contains("ColorCode"))
                    {
                        sdsItemDetail.SelectCommand = "SELECT DISTINCT ItemCode, [ColorCode], '' AS [ClassCode], '' AS [SizeCode] FROM Production.JOMaterialMovement  WHERE ItemCode  = '" + itemc + "'  AND Docnumber  = '" + itemcode + "' AND StepCode = '" + itemcode1 + "'";
                        var selectedValues = itemcode;

                        grid.DataSourceID = "sdsItemDetail";
                        grid.DataBind();
                    }
                    else if (e.Parameters.Contains("ClassCode"))
                    {
                        sdsItemDetail.SelectCommand = "SELECT DISTINCT ItemCode, '' AS [ColorCode], [ClassCode], '' AS [SizeCode] FROM Production.JOMaterialMovement  WHERE ItemCode  = '" + itemc + "'  AND Docnumber  = '" + itemcode + "' AND StepCode = '" + itemcode1 + "'";
                        var selectedValues = itemcode;

                        grid.DataSourceID = "sdsItemDetail";
                        grid.DataBind();
                    }
                    else if (e.Parameters.Contains("SizeCode"))
                    {
                        sdsItemDetail.SelectCommand = "SELECT DISTINCT ItemCode, '' AS [ColorCode], '' AS [ClassCode], [SizeCode] FROM Production.JOMaterialMovement  WHERE ItemCode  = '" + itemc + "'  AND Docnumber  = '" + itemcode + "' AND StepCode = '" + itemcode1 + "'";
                        var selectedValues = itemcode;

                        grid.DataSourceID = "sdsItemDetail";
                        grid.DataBind();
                    }


                    for (int i = 0; i < grid.VisibleRowCount; i++)
                    {
                        if (grid.GetRowValues(i, column) != null)
                            if (grid.GetRowValues(i, column).ToString() == val)
                            {
                                grid.Selection.SelectRow(i);
                                string key = grid.GetRowValues(i, column).ToString();
                                grid.MakeRowVisible(key);
                                break;
                            }
                    }
                    itemlookup.JSProperties["cp_identifier"] = "sku";
                }
            }
        }

        protected void itemcode_Init(object sender, EventArgs e)
        {
            ASPxGridLookup gridLookup = sender as ASPxGridLookup;
            gridLookup.GridView.CustomCallback += new ASPxGridViewCustomCallbackEventHandler(item_CustomCallback);
            if (Session["JobOrder"] != null)
            {
                gridLookup.GridView.DataSourceID = "sdsItemDetail";
                sdsJobOrder.FilterExpression = Session["JobOrder"].ToString();
                gridLookup.DataBind();
            }
        }

        public void item_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
            CriteriaOperator selectionCriteria = new InOperator("Docnumber", new string[] { glChargeTo.Text });
            sdsJobOrder.FilterExpression = (GroupOperator.Combine(GroupOperatorType.And, selectionCriteria)).ToString();
            Session["JobOrder"] = sdsJobOrder.FilterExpression;
            grid.DataSourceID = "sdsItemDetail";
            grid.DataBind();

            for (int i = 0; i < grid.VisibleRowCount; i++)
            {
                if (grid.GetRowValues(i, "ItemCode") != null)
                    if (grid.GetRowValues(i, "ItemCode").ToString() == e.Parameters)
                    {
                        grid.Selection.SelectRow(i);
                        string key = grid.GetRowValues(i, "ItemCode").ToString();
                        grid.MakeRowVisible(key);
                        break;
                    }
            }
        }

        protected void glItemCode1_Init(object sender, EventArgs e)
        {
            ASPxGridLookup gridLookup = sender as ASPxGridLookup;
            gridLookup.GridView.CustomCallback += new ASPxGridViewCustomCallbackEventHandler(item1_CustomCallback);
            //if (Session["FilterItemCode"] != null)
            //{
            //    gridLookup.GridView.DataSourceID = Session["FilterItemCode"];
            //    //ItemCodeLookup.FilterExpression = Session["FilterItemCodeStyle"].ToString();
            //    //Session["FilterExpression"] = null;
            //}
           
        }

        public void item1_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            //if (e.Parameters.Contains("GLP_AIC") || e.Parameters.Contains("GLP_AC") || e.Parameters.Contains("GLP_F")) return;
            //ASPxGridView grid = sender as ASPxGridView;
            //grid.DataSourceID = "ItemCodeLookup";
            //grid.DataBind();


            string column = e.Parameters.Split('|')[0];
            if (column.Contains("GLP_AIC") || column.Contains("GLP_AC") || column.Contains("GLP_F")) return;
            string itemcode = e.Parameters.Split('|')[1];
            string val = e.Parameters.Split('|')[2];//Set column value
            if (val.Contains("GLP_AIC") || val.Contains("GLP_AC") || val.Contains("GLP_F")) return;//Traps the callback

            string itemcode1 = e.Parameters.Split('|')[3];
            string itemc = e.Parameters.Split('|')[4];
            string colorcode = e.Parameters.Split('|')[5];//Set column value
            //if (col.Contains("GLP_AIC") || col.Contains("GLP_AC") || col.Contains("GLP_F")) return;//Traps the callback
            //end

            //for class
            string classcode = e.Parameters.Split('|')[6];//Set column value
            //if (cls.Contains("GLP_AIC") || cls.Contains("GLP_AC") || cls.Contains("GLP_F")) return;//Traps the callback
            //end

            //for size
            string sizecode = e.Parameters.Split('|')[7];//Set column value
            //if (cls.Contains("GLP_AIC") || cls.Contains("GLP_AC") || cls.Contains("GLP_F")) return;//Traps the callback
            //end

 
            ASPxGridView grid = sender as ASPxGridView;

            if (e.Parameters.Contains("ItemCode"))
            {

                sdsItemDetail.SelectCommand = "SELECT DISTINCT ItemCode,StepCode,DocNumber FROM Production.JOMaterialMovement  WHERE  Docnumber  = '" + itemcode + "' AND StepCode = '" + itemcode1 + "' and ISNULL(IsChargedAlready,0)=0";
            }
           
            Session["FilterItemCode"] = sdsItemDetail;
            grid.DataSourceID = "sdsItemDetail";
            grid.DataBind();
        }
        protected void dtpDocDate_Init(object sender, EventArgs e)
        {
            if (Request.QueryString["entry"] == "N")
            {
                dtpdocdate.Date = DateTime.Now;
            }
        }

      

        #endregion

   


    }
}