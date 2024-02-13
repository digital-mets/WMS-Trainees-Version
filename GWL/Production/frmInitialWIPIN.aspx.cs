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
using GearsWarehouseManagement;
using GearsProcurement;

namespace GWL
{
    public partial class frmInitialWIPIN : System.Web.UI.Page
    {
        Boolean error = false;//Boolean for grid validation
        Boolean view = false;//Boolean for view state

        private static string strError;

        Boolean check = false;//Boolean for grid validation
        string a = ""; //Renats
        string b = ""; //Renats
        Entity.InitialWIPIN _Entity = new InitialWIPIN();//Calls entity odsHeader
        Entity.InitialWIPIN.WISizeBreakDown _EntityDetail = new InitialWIPIN.WISizeBreakDown();//Call entity sdsDetail

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
                sdsProductCode.SelectCommand = "select DISTINCT ItemCode as ProductCode,FullDesc,A.DocNumber from Production.JOProductOrder A inner join Production.JobOrder B on A.DocNumber = B.DocNumber where A.Docnumber = '" + glJobOrder.Text + "'";
                sdsProductColor.SelectCommand = "select  DISTINCT ColorCode as ProductColor,ItemCode as ProductCode,A.DocNumber from Production.JOProductOrder A inner join Production.JobOrder B on A.DocNumber = B.DocNumber where A.Docnumber = '" + glJobOrder.Text + "' and ItemCode  = '" + glProductCode.Text + "'";

                Session["ProductCode"] = null;
                Session["ProductColor"] = null;
                Session["customoutbound"] = null;

                //V=View, E=Edit, N=New
                switch (Request.QueryString["entry"].ToString())
                {
                    case "N":
                       
                        updateBtn.Text = "Add";
                        glJobOrder.Visible = true;
                        break;
                    case "E":

                        updateBtn.Text = "Update";
                        txtDocNumber.ReadOnly = true;
                        break;
                    case "V":
                        sdsJobOrder.SelectCommand = "SELECT DISTINCT A.DocNumber,B.StepCode,WorkCenter FROM Production.JobOrder A inner join Production.JOStepPlanning B on A.DocNumber = B.DocNumber inner join Production.JOProductOrder C on A.DocNumber = C.DocNumber";
                        sdsJobOrder.DataBind();
                        view = true;//sets view mode for entry
                        updateBtn.Text = "Close";
                        glcheck.ClientVisible = false;

                        break;
                    case "D":
                        sdsJobOrder.SelectCommand = "SELECT DISTINCT A.DocNumber,B.StepCode,WorkCenter FROM Production.JobOrder A inner join Production.JOStepPlanning B on A.DocNumber = B.DocNumber inner join Production.JOProductOrder C on A.DocNumber = C.DocNumber";
                        sdsJobOrder.DataBind();
                        view = true;
                        updateBtn.Text = "Delete";
                        break;
                }




                txtDocNumber.Value = Request.QueryString["docnumber"].ToString(); //sets docnumber from session
                gv1.DataSourceID = "odsDetail";
                _Entity.getdata(txtDocNumber.Text, Session["ConnString"].ToString()); //Method for retrieving data from entity
                dtpDocDate.Text = Convert.ToDateTime(_Entity.DocDate.ToString()).ToShortDateString();
                txtType.Text = _Entity.Type.ToString();
                txtStep.Text = _Entity.Step;
                txtDrNumber.Text = _Entity.DRDocNumber.ToString();
                glJobOrder.Text = _Entity.JobOrder.ToString();
                txtWorkCenter.Text = _Entity.WorkCenter.ToString();

                speTotalQuantity.Value = _Entity.TotalQuantity.ToString();
                txtDrNumber.Text = _Entity.DRDocNumber;
                txtRemarks.Text = _Entity.Remarks;
                sdsProductCode.SelectCommand = "select DISTINCT ItemCode as ProductCode,FullDesc,A.DocNumber from Production.JOProductOrder A inner join Production.JobOrder B on A.DocNumber = B.DocNumber where A.Docnumber = '" + glJobOrder.Text + "'";
                sdsProductCode.DataBind();
                glProductCode.Value = _Entity.ProductCode;
                sdsProductColor.SelectCommand = "select  DISTINCT ColorCode as ProductColor,ItemCode as ProductCode,A.DocNumber from Production.JOProductOrder A inner join Production.JobOrder B on A.DocNumber = B.DocNumber where A.Docnumber = '" + glJobOrder.Text + "' and ItemCode  = '" + glProductCode.Text + "'";
                sdsProductColor.DataBind();
                glProductColor.Value = _Entity.ProductColor;
                txtHField1.Value = _Entity.Field1.ToString();
                txtHField2.Value = _Entity.Field2.ToString();
                txtHField3.Value = _Entity.Field3.ToString();
                txtHField4.Value = _Entity.Field4.ToString();
                txtHField5.Value = _Entity.Field5.ToString();
                txtHField6.Value = _Entity.Field6.ToString();
                txtHField7.Value = _Entity.Field7.ToString();
                txtHField8.Value = _Entity.Field8.ToString();
                txtHField9.Value = _Entity.Field9.ToString();

                txtHAddedBy.Text = _Entity.AddedBy;
                txtHAddedDate.Text = _Entity.AddedDate;
                txtHLastEditedBy.Text = _Entity.LastEditedBy;
                txtHLastEditedDate.Text = _Entity.LastEditedDate;
                txtHSubmittedBy.Text = _Entity.SubmittedBy;
                txtHSubmittedDate.Text = _Entity.SubmittedDate;


                txtType.Text = "Initial WIP IN";

                if (Request.QueryString["entry"].ToString() == "N")
                {
                    gv1.DataSourceID = null;
                    popup.ShowOnPageLoad = false;
                    glJobOrder.ClientEnabled = true;
                    frmlayout1.FindItemOrGroupByName("ReferenceTransaction").ClientVisible = false;

             
                   
                    //gv1.SettingsPager.Mode = GridViewPagerMode.ShowAllRecords;
                    //gv1.Settings.VerticalScrollableHeight = 200;
                
                }
                else
                {


                    gvRef.DataSourceID = "odsReference";
                    odsReference.SelectParameters["DocNumber"].DefaultValue = txtDocNumber.Text;
                    this.gvRef.Columns["CommandString"].Width = 0;

                    this.gvRef.Columns["RCommandString"].Width = 0;
                }
  
                
                //Details checking. changes datasourceid of gridview if there's no details for a certain docnumber; to enable adding of details.
                if (Request.QueryString["IsWithDetail"].ToString() == "False" && Request.QueryString["entry"].ToString() != "V")
                {
                    gv1.DataSourceID = null;
                }

                DataTable dtrr1 = Gears.RetriveData2("Select DocNumber from Procurement.WIPIN where docnumber = '" + txtDocNumber.Text + "' and ISNULL(ServiceOrder,'')!='' ", Session["ConnString"].ToString());
                if (dtrr1.Rows.Count > 0 && Request.QueryString["entry"].ToString() == "N")
                {
                    updateBtn.Text = "Update";
                }


                DataTable dtdetail = _EntityDetail.getdetail(txtDocNumber.Text, Session["ConnString"].ToString());
                if (dtdetail.Rows.Count > 0)
                {
                    odsDetail.SelectParameters["DocNumber"].DefaultValue = txtDocNumber.Text;
                    gv1.DataSourceID = "odsDetail";
                    
                }
                else
                {
                    gv1.DataSource = sdsDetail;
                    if (gv1.DataSourceID != "")
                    {
                        gv1.DataSourceID = null;
                    }
                    gv1.DataBind();
                }
                gv1.KeyFieldName = "LineNumber";
            }



        }
        #endregion

        #region Validation
        private void Validate()
        {
            GearsLibrary.Gears.GearsParameter gparam = new Gears.GearsParameter();
            gparam._DocNo = _Entity.DocNumber;
            gparam._TransType = "PRDIWN";
            gparam._Connection = Session["ConnString"].ToString();
            string strresult = GearsProduction.GProduction.InitialWIPIn_Validate (gparam);
    
            if (strresult != " " && strresult != "" && !String.IsNullOrEmpty(strresult))
            {
                cp.JSProperties["cp_valmsg"] = strresult + "\r\n" ;//Message variable to client side
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
            look.DropDownButton.Enabled = !view;
            look.ReadOnly = view;

            if (Request.QueryString["entry"].ToString() == "E" || Request.QueryString["editcosting"] != null)
            {

                //if (!String.IsNullOrEmpty(glSupplierCode.Text))
                //{
                //    glSupplierCode.DropDownButton.Enabled = false;
                //    glSupplierCode.ReadOnly = true;
                  
                //}
                //if (Request.QueryString["IsWithDetail"].ToString() == "true")
                //{
                //Generatebtn.ClientEnabled = false;
                //glJobOrder.ClientEnabled = false;
                //glJobOrder.DropDownButton.Enabled = false;
                ////Details checking. changes datasourceid of gridview if there's no details for a certain docnumber; to enable adding of details.
            
                //}
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
            date.DropDownButton.Enabled = !view;
            date.ReadOnly = view;
        }
        protected void SpinEdit_Load(object sender, EventArgs e)//Control for all numeric entries in header
        {
            ASPxSpinEdit spin = sender as ASPxSpinEdit;
            spin.HorizontalAlign = HorizontalAlign.Right;
            spin.ReadOnly = view;
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
            if (view == true)
            {
                if (e.ButtonType == ColumnCommandButtonType.Edit || e.ButtonType == ColumnCommandButtonType.Delete ||
                    e.ButtonType == ColumnCommandButtonType.Cancel)
                    e.Visible = false;
            }
            if (e.ButtonType == ColumnCommandButtonType.Update || e.ButtonType == ColumnCommandButtonType.New)
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
        protected void lookup_Init(object sender, EventArgs e)
        {
            //ASPxGridLookup gridLookup = sender as ASPxGridLookup;
            //gridLookup.GridView.CustomCallback += new ASPxGridViewCustomCallbackEventHandler(gridView_CustomCallback);
            //if (Session["FilterExpression"] != null)
            //{
            //    gridLookup.GridView.DataSourceID = "sdsItemDetail";
            //    sdsItemDetail.FilterExpression = Session["FilterExpression"].ToString();
            //    //Session["FilterExpression"] = null;
            //}
        }
        public void gridView_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            //string column = e.Parameters.Split('|')[0];//Set column name
            //if (column.Contains("GLP_AIC") || column.Contains("GLP_AC") || column.Contains("GLP_F")) return;//Traps the callback
            //string itemcode = e.Parameters.Split('|')[1];//Set Item Code
            //string val = e.Parameters.Split('|')[2];//Set column value
            //if (val.Contains("GLP_AIC") || val.Contains("GLP_AC") || val.Contains("GLP_F")) return;//Traps the callback

            //ASPxGridView grid = sender as ASPxGridView;
            //ASPxGridLookup lookup = (ASPxGridLookup)gv1.FindEditRowCellTemplateControl((GridViewDataColumn)gv1.Columns[2], "glItemCode");
            //var selectedValues = itemcode;
            //CriteriaOperator selectionCriteria = new InOperator(lookup.KeyFieldName, new string[] { itemcode });
            //sdsItemDetail.FilterExpression = (GroupOperator.Combine(GroupOperatorType.And, selectionCriteria)).ToString();
            //Session["FilterExpression"] = sdsItemDetail.FilterExpression;
            //grid.DataSourceID = "sdsItemDetail";
            //grid.DataBind();

            //for (int i = 0; i < grid.VisibleRowCount; i++)
            //{
            //    if (grid.GetRowValues(i, column) != null)
            //        if (grid.GetRowValues(i, column).ToString() == val)
            //        {
            //            grid.Selection.SelectRow(i);
            //            string key = grid.GetRowValues(i, column).ToString();
            //            grid.MakeRowVisible(key);
            //            break;
            //        }
            //}
        }

        #endregion

        #region Callback functions(Inserting/Updating/Deleting/Validating)
        protected void cp_Callback(object sender, CallbackEventArgsBase e)
        {
            _Entity.Connection = Session["ConnString"].ToString(); 
                _Entity.DocNumber = txtDocNumber.Value.ToString();
                _Entity.DocDate = dtpDocDate.Text;
                _Entity.Type = txtType.Text;
                _Entity.Step = txtStep.Text;
                _Entity.JobOrder = glJobOrder.Text;
                _Entity.WorkCenter = txtWorkCenter.Text;
                _Entity.TotalQuantity = Convert.ToDecimal(Convert.IsDBNull(speTotalQuantity.Value) ? "0" : speTotalQuantity.Value);
                _Entity.DRDocNumber = txtDrNumber.Text;
                _Entity.Remarks = txtRemarks.Text;
                _Entity.ProductCode = glProductCode.Text;
                _Entity.ProductColor = glProductColor.Text;
     
                _Entity.Field1 = String.IsNullOrEmpty(txtHField1.Text) ? null : txtHField1.Text;
                _Entity.Field2 = String.IsNullOrEmpty(txtHField2.Text) ? null : txtHField2.Text;
                _Entity.Field3 = String.IsNullOrEmpty(txtHField3.Text) ? null : txtHField3.Text;
                _Entity.Field4 = String.IsNullOrEmpty(txtHField4.Text) ? null : txtHField4.Text;
                _Entity.Field5 = String.IsNullOrEmpty(txtHField5.Text) ? null : txtHField5.Text;
                _Entity.Field6 = String.IsNullOrEmpty(txtHField6.Text) ? null : txtHField6.Text;
                _Entity.Field7 = String.IsNullOrEmpty(txtHField7.Text) ? null : txtHField7.Text;
                _Entity.Field8 = String.IsNullOrEmpty(txtHField8.Text) ? null : txtHField8.Text;
                _Entity.Field9 = String.IsNullOrEmpty(txtHField9.Text) ? null : txtHField9.Text;
     

              //  _Entity.Transtype = Request.QueryString["transtype"].ToString();

                string param = e.Parameter.Split('|')[0]; //Renats
                switch (param) //Renats
                {
                    case "Add":
                        gv1.UpdateEdit();

                        if (error == false)
                        {
                            check = true;
                            _Entity.UpdateData(_Entity);//Method of inserting for header
                            if (Session["Datatable"] == "1")
                            {
                                gv1.DataSourceID = sdsJobOrderDetail.ID;
                                gv1.UpdateEdit();
                            }
                            else
                            {
                                gv1.DataSourceID = "odsDetail";//Renew datasourceID to entity
                                odsDetail.SelectParameters["DocNumber"].DefaultValue = txtDocNumber.Text;//Set select parameter to prevent error
                                gv1.UpdateEdit();//2nd initiation to insert grid
                            }
                            // _Entity.SubsiEntry(txtDocNumber.Text);
                            Validate();
                            cp.JSProperties["cp_message"] = "Successfully Added!";
                            cp.JSProperties["cp_success"] = true;
                            cp.JSProperties["cp_close"] = true;
                            Session["Datatable"] = null;
                            gv1.DataSource = null;
                        }
                        else
                        {
                            cp.JSProperties["cp_message"] = "Please check all the fields!";
                            cp.JSProperties["cp_success"] = true;
                        }
                        break;

                case "Update":
            _Entity.LastEditedBy = Session["userid"].ToString();
           
            _Entity.LastEditedDate = DateTime.Now.ToString();
                    gv1.UpdateEdit();

                    if (error == false)
                    {
                        check = true;
                        strError = Functions.Submitted(_Entity.DocNumber, "Production.InitialWIPIN", 1, Session["ConnString"].ToString());//NEWADD factor 1 if submit, 2 if approve
                        if (!string.IsNullOrEmpty(strError))
                        {
                            gv1.JSProperties["cp_message"] = strError;
                            gv1.JSProperties["cp_success"] = true;
                            gv1.JSProperties["cp_forceclose"] = true;
                            return;
                        }


                        _Entity.UpdateData(_Entity);//Method of inserting for header
                        if (Session["Datatable"] == "1")
                        {
                            gv1.DataSourceID = sdsJobOrderDetail.ID;
                            gv1.UpdateEdit();
                        }
                        else
                        {
                            gv1.DataSourceID = "odsDetail";//Renew datasourceID to entity
                            odsDetail.SelectParameters["DocNumber"].DefaultValue = txtDocNumber.Text;//Set select parameter to prevent error
                            gv1.UpdateEdit();//2nd initiation to insert grid
                        }
                       // _Entity.SubsiEntry(txtDocNumber.Text);
                      Validate();
                        cp.JSProperties["cp_message"] = "Successfully Updated!";
                      cp.JSProperties["cp_success"] = true;
                        cp.JSProperties["cp_close"] = true;
                        Session["Datatable"] = null;
                        gv1.DataSource = null;
                    }
                    else
                    {
                        cp.JSProperties["cp_message"] = "Please check all the fields!";
                        cp.JSProperties["cp_success"] = true;
                    }
                    break;


                case "Delete":
                    //GetSelectedVal(); 
                    cp.JSProperties["cp_delete"] = true;
                    break;

                case "ConfDelete":
                    _Entity.DeleteData(_Entity);
                    cp.JSProperties["cp_close"] = true;
                    cp.JSProperties["cp_message"] = "Successfully deleted";
                    break;

                case "Close":
                        cp.JSProperties["cp_close"] = true;
                        gv1.DataSource = null;
                    break;

                case "JO":
                    a = e.Parameter.Split('|')[1]; //Renats
                    b = e.Parameter.Split('|')[2]; //Renats

                    SqlDataSource ds = sdsJobOrder;

     

                          ds.SelectCommand = string.Format("SELECT DISTINCT A.DocNumber,B.StepCode,WorkCenter,RTRIM(LTRIM(Itemcode)) AS ProductCode, Colorcode as ProductColor, DocDate, DueDate, LeadTime, CustomerCode, TotalJOQty "
                           + " , TotalINQty, TotalFinalQty, Remarks FROM Production.JobOrder A  "
                           + " inner join Production.JOStepPlanning B "
                           + " on A.DocNumber = B.DocNumber "
                           + " inner join Production.JOProductOrder C "
                           + " on A.DocNumber = C.DocNumber "
                           + "  WHERE Status IN ('N','W') "
                           + " and ISNULL(PreProd,0)='0' and Sequence=1  and B.StepCode!='CUTTING'  and A.DocNumber = '" + a + "' AND StepCode = '" + b + "'");
                         
                    DataView inb = (DataView)ds.Select(DataSourceSelectArguments.Empty);
                    if (inb.Count > 0)
                    {
                        txtStep.Text = inb[0][1].ToString();
                        txtWorkCenter.Text = inb[0][2].ToString();



                        DataTable dtproductcode = Gears.RetriveData2("SELECT DISTINCT Itemcode,Count(Itemcode) "
                               + " FROM Production.JobOrder A  "
                               + " inner join Production.JOStepPlanning B "
                               + " on A.DocNumber = B.DocNumber "
                               + " inner join Production.JOProductOrder C "
                               + " on A.DocNumber = C.DocNumber "
                               + "  WHERE Status IN ('N','W') "
                               + " and ISNULL(PreProd,0)='0'  and Sequence=1  and B.StepCode!='CUTTING' and A.DocNumber = '" + a + "'  AND B.StepCode = '" + b + "' GROUP BY Itemcode --HAVING COUNT(Itemcode)>1", Session["ConnString"].ToString());


                        if (dtproductcode.Rows.Count == 1)
                        {
                            glProductCode.Text = inb[0]["ProductCode"].ToString();
                        }

                        DataTable dtproductcolor = Gears.RetriveData2("SELECT DISTINCT Colorcode,Count(Colorcode) "
                               + "  FROM Production.JobOrder A  "
                               + " inner join Production.JOStepPlanning B "
                               + " on A.DocNumber = B.DocNumber "
                               + " inner join Production.JOProductOrder C "
                               + " on A.DocNumber = C.DocNumber "
                               + "  WHERE Status IN ('N','W') "
                               + " and ISNULL(PreProd,0)='0' and Sequence=1  and B.StepCode!='CUTTING'  and A.DocNumber = '" + a + "'  AND B.StepCode = '" + b + "' GROUP BY Colorcode --HAVING COUNT(Colorcode)>1", Session["ConnString"].ToString());

                        if (dtproductcolor.Rows.Count == 1)
                        {
                            glProductColor.Text = inb[0]["ProductColor"].ToString();
                        }
                    }
                    GetSelectedVal();
                    break;

               
                case "Generate":
                    GetSelectedVal();
                   // GetVat();
                    OtherDetail();
                    cp.JSProperties["cp_generated"] = true;
                   
                    //glSupplierCode.Enabled = false;
                    //Generatebtn.Enabled = false;
                    break;

                case "customercode":
                //    sdsPicklist.SelectParameters["customercode"].DefaultValue = aglCustomer.Text;
                    break;

            }
        }
        //dictionary method to hold error 

        protected void gv1_BatchUpdate(object sender, ASPxDataBatchUpdateEventArgs e)
        {
            if ((error == true || error == false) && check == false)//Prevents updating of grid to enable validation
            {
                e.Handled = true;
                e.UpdateValues.Clear();
            }

            if (Session["Datatable"] == "1" && check == true)
             {
                e.Handled = true;
                DataTable source = GetSelectedVal();

                // Removing all deleted rows from the data source(Excel file)
                foreach (ASPxDataDeleteValues values in e.DeleteValues)
                {
                    try
                    {
                        object[] keys = { values.Keys[0], values.Keys[1]};
                        source.Rows.Remove(source.Rows.Find(keys));
                    }
                    catch (Exception)
                    {
                        continue;
                    }
                }

                foreach (ASPxDataInsertValues values in e.InsertValues)//for insert chu chu
                {
                    var SizeCode = values.NewValues["SizeCode"];
                    var Qty = values.NewValues["Qty"];
                    var JOBreakdown = values.NewValues["JOBreakdown"];
               

                    
                    var Field1 = values.NewValues["Field1"];
                    var Field2 = values.NewValues["Field2"];
                    var Field3 = values.NewValues["Field3"];
                    var Field4 = values.NewValues["Field4"];
                    var Field5 = values.NewValues["Field5"];
                    var Field6 = values.NewValues["Field6"];
                    var Field7 = values.NewValues["Field7"];
                    var Field8 = values.NewValues["Field8"];
                    var Field9 = values.NewValues["Field9"];
                    source.Rows.Add(SizeCode, Qty, JOBreakdown, Field1, Field2, Field3, Field4, Field5, Field6, Field7, Field8, Field9);
                }

                 //Updating required rows
                foreach (ASPxDataUpdateValues values in e.UpdateValues)
                {
                        object[] keys = {values.NewValues["LineNumber"]};
                        DataRow row = source.Rows.Find(keys);

                        row["SizeCode"] = values.NewValues["SizeCode"];
                        row["Qty"] = values.NewValues["Qty"];
                        row["JOBreakdown"] = values.NewValues["JOBreakdown"];
                    
                        row["Field1"] = values.NewValues["Field1"];
                        row["Field2"] = values.NewValues["Field2"];
                        row["Field3"] = values.NewValues["Field3"];
                        row["Field4"] = values.NewValues["Field4"];
                        row["Field5"] = values.NewValues["Field5"];
                        row["Field6"] = values.NewValues["Field6"];
                        row["Field7"] = values.NewValues["Field7"];
                        row["Field8"] = values.NewValues["Field8"];
                        row["Field9"] = values.NewValues["Field9"];
                    
                }
      
                Gears.RetriveData2("DELETE FROM Production.WISizeBreakDown where Docnumber='" + txtDocNumber.Text + "'", Session["ConnString"].ToString());
                foreach (DataRow dtRow in source.Rows)//This is where the data will be inserted into db
                {
                    _EntityDetail.SizeCode = dtRow["SizeCode"].ToString();
     
                    _EntityDetail.Qty = Convert.ToDecimal(Convert.IsDBNull(dtRow["Qty"]) ? 0 : dtRow["Qty"]);
                    _EntityDetail.JOBreakdown = Convert.ToDecimal(Convert.IsDBNull(dtRow["JOBreakdown"]) ? 0 : dtRow["JOBreakdown"]);
                
                    _EntityDetail.Field1 = dtRow["Field1"].ToString();
                    _EntityDetail.Field2 = dtRow["Field2"].ToString();
                    _EntityDetail.Field3 = dtRow["Field3"].ToString();
                    _EntityDetail.Field4 = dtRow["Field4"].ToString();
                    _EntityDetail.Field5 = dtRow["Field5"].ToString();
                    _EntityDetail.Field6 = dtRow["Field6"].ToString();
                    _EntityDetail.Field7 = dtRow["Field7"].ToString();
                    _EntityDetail.Field8 = dtRow["Field8"].ToString();
                    _EntityDetail.Field9 = dtRow["Field9"].ToString();
                    _EntityDetail.AddWISizeBreakDown(_EntityDetail);
                }
            }
        }
        #endregion

     



      
        protected void gv1_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["picklistdetail"] = null;
            }

            if (Session["picklistdetail"] != null)
            {
                gv1.DataSourceID = sdsJobOrderDetail.ID;
                sdsJobOrderDetail.FilterExpression = Session["picklistdetail"].ToString();
                //gridview.DataSourceID = null;
            }
        }

        private DataTable GetSelectedVal()
        {

            gv1.DataSourceID = null;
            gv1.DataBind();
            DataTable dt = new DataTable();
            string[] selectedValues = glJobOrder.Text.Split(';');
            CriteriaOperator selectionCriteria = new InOperator("DocNumber", selectedValues);
            sdsJobOrderDetail.FilterExpression = (GroupOperator.Combine(GroupOperatorType.And, selectionCriteria)).ToString();
            Session["picklistdetail"] = sdsJobOrderDetail.FilterExpression;
            gv1.DataSourceID = sdsJobOrderDetail.ID;
            gv1.DataBind();

            Session["Datatable"] = "1";
            foreach (GridViewColumn col in gv1.VisibleColumns)
            {
                GridViewDataColumn dataColumn = col as GridViewDataColumn;
                if (dataColumn == null) continue;
                dt.Columns.Add(dataColumn.FieldName);
            }
            for (int i = 0; i < gv1.VisibleRowCount; i++)
            {
                DataRow row = dt.Rows.Add();
                foreach (DataColumn col in dt.Columns)
                    row[col.ColumnName] = gv1.GetRowValues(i, col.ColumnName);
            }

            dt.PrimaryKey = new DataColumn[] { //Sets datatable's primary key for batchupdate reference during adding.
            dt.Columns["LineNumber"]};

            //glJobOrder.ClientEnabled = false;

            return dt;
        }

        private void OtherDetail()
        {
            string pick = glJobOrder.Text;


            DataTable ret = Gears.RetriveData2("SELECT  WorkCenter FROM Procurement.SOWorkOrder WHERE DocNumber = '" + pick + "'", Session["ConnString"].ToString());

            DataRow _ret = ret.Rows[0];

            if (String.IsNullOrEmpty(txtWorkCenter.Text))
            {
                _Entity.WorkCenter = _ret["WorkCenter"].ToString();
                txtWorkCenter.Text = _ret["WorkCenter"].ToString();
            }
      

         
        }
   



        protected void dtpDocDate_Init(object sender, EventArgs e)
        {
            if (Request.QueryString["entry"] == "N")
            {
                dtpDocDate.Date = DateTime.Now;
            }
        }


        protected void Connection_Init(object sender, EventArgs e)
        {
           
               
            sdsDetail.ConnectionString = Session["ConnString"].ToString();
            sdsJobOrder.ConnectionString = Session["ConnString"].ToString();
            sdsJobOrderDetail.ConnectionString = Session["ConnString"].ToString();
            sdsProductCode.ConnectionString = Session["ConnString"].ToString();
            sdsProductColor.ConnectionString = Session["ConnString"].ToString();
    

   

        }

        protected void glProductCode_Init(object sender, EventArgs e)
        {
            ASPxGridLookup gridLookup = sender as ASPxGridLookup;
            gridLookup.GridView.CustomCallback += new ASPxGridViewCustomCallbackEventHandler(glProductCode_CustomCallback);
            if (Session["ProductCode"] != null)
            {
                gridLookup.GridView.DataSourceID = sdsProductCode.ID;
                sdsProductCode.FilterExpression = Session["ProductCode"].ToString();
            }
        }





        public void glProductCode_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            //string whcode = e.Parameters;//Set column name
            //if (whcode.Contains("GLP_AIC") || whcode.Contains("GLP_AC") || whcode.Contains("GLP_F")) return;//Traps the callback

            
            ASPxGridView grid = sender as ASPxGridView;
            grid.DataSourceID = null;
            CriteriaOperator selectionCriteria = new InOperator("DocNumber", new string[] { glJobOrder.Text });
            sdsProductCode.FilterExpression = (GroupOperator.Combine(GroupOperatorType.And, selectionCriteria)).ToString();
            Session["ProductCode"] = sdsProductCode.FilterExpression;
            grid.DataSourceID = sdsProductCode.ID;
            grid.DataBind();


        }


        protected void glProductColor_Init(object sender, EventArgs e)
        {
            ASPxGridLookup gridLookup = sender as ASPxGridLookup;
            gridLookup.GridView.CustomCallback += new ASPxGridViewCustomCallbackEventHandler(glProductColor_CustomCallback);
            if (Session["ProductColor"] != null)
            {
                gridLookup.GridView.DataSourceID = sdsProductColor.ID;
                sdsProductColor.FilterExpression = Session["ProductColor"].ToString();
            }
        }

        public void glProductColor_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            //string whcode = e.Parameters;//Set column name
            //if (whcode.Contains("GLP_AIC") || whcode.Contains("GLP_AC") || whcode.Contains("GLP_F")) return;//Traps the callback
            
      
            ASPxGridView grid = sender as ASPxGridView;
            grid.DataSourceID = null;
            CriteriaOperator selectionCriteria = new InOperator("DocNumber", new string[] { glJobOrder.Text });
            CriteriaOperator selectionCriteria1 = new InOperator("ProductCode", new string[] { glProductCode.Text });
            sdsProductColor.FilterExpression = (GroupOperator.Combine(GroupOperatorType.And, selectionCriteria, selectionCriteria1)).ToString();
            Session["ProductColor"] = sdsProductColor.FilterExpression;
            grid.DataSourceID = sdsProductColor.ID;
            grid.DataBind();


        }
        
        
    }
}