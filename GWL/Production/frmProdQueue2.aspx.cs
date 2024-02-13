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
    public partial class frmProdQueue2 : System.Web.UI.Page
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


                //Session["userid"].ToString();
                Session["Prod_Init"] = "";
                wipuserid.Text = Session["userid"].ToString();

                switch (Request.QueryString["entry"].ToString())
                {
                    case "N":
                        //updateBtn.Text = "Add";
                        view = true;//sets view mode for entry
                        updateBtn.Text = "Close";
                        glcheck.ClientVisible = false;
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
                DataTable dtHeader = Gears.RetriveData2("SELECT * FROM Production.BatchQueue WHERE RecordID = " + txtDocnumber.Value + " ", Session["ConnString"].ToString());


                if (dtHeader.Rows.Count > 0)
                {
                    txtyear.Value = dtHeader.Rows[0]["Year"].ToString();
                    txtweek.Value = dtHeader.Rows[0]["WorkWeek"].ToString();
                    hdayno.Text = dtHeader.Rows[0]["DayNo"].ToString();
                    //txtDocDate.Text = dtHeader.Rows[0]["DocDate"].ToString();
                    txtDocDate.Text = dtHeader.Rows[0]["Field9"].ToString();

                }

                string strSQL2 = " EXEC dbo.sp_ProdBatchQueue " + txtDocnumber.Value + ",0,'SKU-VIEW',0,'USERID','VIEW','0' ";

                sdsProdQueue.SelectCommand = strSQL2;


                SetSKU();

                //Details checking. changes datasourceid of gridview if there's no details for a certain docnumber; to enable adding of details.

                //2021-05-21    EMC   
                gv1.KeyFieldName = "BatchNo";
                gv1.DataSourceID = "sdsProdQueue";

                string strSQL3 = "";

                strSQL3 = "SELECT SKUcode ";
                strSQL3 = strSQL3 + " FROM Production.BatchQueue  WHERE YEAR = '" + txtyear.Value + "' AND WorkWeek = '" + txtweek.Value + "' AND DayNo = '" + hdayno.Text + "' ";
                strSQL3 = strSQL3 + " GROUP BY SKUcode ";

                sdsSKU.SelectCommand = strSQL3;


         

                


            }

            //2021-07-22    emc for List of Step --emc999
      

        
            

        }
        #endregion


        protected void Initialize()
        {
            if (Request.QueryString["entry"].ToString() == "V" || Request.QueryString["entry"].ToString() == "E")
            {
                string StepTypei = "";
                if (Request.QueryString["parameters"].ToString() == "NRTE")
                {
                    StepTypei = "1";
                    htitle.Text = "(Production)-" + htitle.Text;
                }
                else
                {
                    StepTypei = "2";
                    htitle.Text = "(Packaging)-" + htitle.Text;
                }

                // 2021-11-05   TL  Revise DataSource
                //Session["Prod_Init"] = "EXEC sp_ProdBatchQueue_Data '" + StepTypei + "','" + txtweek.Text + "','" + wipSKU.Text + "','GETSTEP' ";
                Session["Prod_Init"] =
                    "SELECT DISTINCT StepCode FROM Production." + (StepTypei == "1" ? "ProdRoutingStep" : "ProdRoutingStepPack") +
                    " WHERE SKUCode = '" + wipSKU.Text + "'";
                // 2021-11-05   TL  (End)
                // 2021-11-05   TL  Comment out
                //sdsStep.SelectCommand = Session["Prod_Init"].ToString();
                //sdsStep.DataBind();
                //wipprocess.DataBind();
                // 2021-11-05   TL  (End)
            }
        }


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

        private void SetSKU()
        {
            //2021-09-02

            string strSQL2 = " EXEC dbo.sp_ProdBatchQueue " + txtDocnumber.Value + ",0,'SKU',0,'USERID','VIEW','0'  ";

            DataTable dtSKU = Gears.RetriveData2(strSQL2, Session["ConnString"].ToString());


            for(int i=1; i <= 10;i++)
            {
                string strHeader = "S" + i.ToString();
                string strHeaderID = "ID" + i.ToString();
                
                gv1.Columns[strHeaderID].Width = 0;

                if(i <= dtSKU.Rows.Count)
                {
                    string strSKUcode = dtSKU.Rows[i-1][0].ToString();
                    gv1.Columns[strHeader].Caption = strSKUcode;
                    gv1.Columns[strHeader].Width = 100;

                }
                else
                {
                    gv1.Columns[strHeader].Width = 0;

                }

            }

        }
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
            //_Entity.DocDate = dtpdocdate.Text;
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
                case "skucodeview":

                    //gv1.DataSourceID = "";

                     string strSQL2 = "";

                     strSQL2 = "SELECT ISNULL(MIN(RecordID),0) AS RecID,MAX(ISNULL(Field9,'')) AS Field9 ";
                     strSQL2 = strSQL2 + " FROM Production.BatchQueue  WHERE YEAR = '" + txtyear.Value + "' AND WorkWeek = '" + txtweek.Value + "' ";
                     strSQL2 = strSQL2 + " AND DayNo = '"+hdayno.Text+"'  ";

                     DataTable dtHeader5 = Gears.RetriveData2(strSQL2, Session["ConnString"].ToString());

                     if (dtHeader5.Rows.Count > 0)
                     {
                         txtDocnumber.Text = dtHeader5.Rows[0][0].ToString();

                         txtDocDate.Text = dtHeader5.Rows[0]["Field9"].ToString();

                         
                         if (txtDocnumber.Text != "0")
                         {
                             //gv1.DataSourceID = "sdsProdQueue";

                             strSQL2 = " EXEC dbo.sp_ProdBatchQueue " + txtDocnumber.Text + ",0,'SKU-VIEW',0,'USERID','VIEW'  ";
                             sdsProdQueue.SelectCommand = strSQL2;
                             
                             gv1.DataBind();

                             SetSKU();
                         }
                         else
                         {
                             sdsProdQueue.SelectCommand = "";   
                         }
                     }


                    break;
                case "viewsku":
                    string param1a = e.Parameter.Split('|')[1];
                    //DataTable dtsku5 = Gears.RetriveData2("SELECT * FROM Production.BatchQueue WHERE RecordID = " + param1a + " ", Session["ConnString"].ToString());
                    string strSKUSQL = " EXEC dbo.sp_ProdBatchQueue " + param1a + "," + "0" + ",'" + wipprocess.Text + "'," + "0" + ",'" + wipuserid.Text + "','" + "SKU" + "'," + "0" + "  ";
                    DataTable dtsku5 = Gears.RetriveData2(strSKUSQL, Session["ConnString"].ToString());


                    if (dtsku5.Rows.Count > 0)
                    {
                        cp.JSProperties["cp_viewsku"] = true;
                        //cp.JSProperties["cp_wipprocess"] = wipprocess.Text; //"2START" + batchid.Text;                    
                        //cp.JSProperties["cp_batchstatus"] = dtsku5.Rows[0]["Cstatus"].ToString(); // "Started"; //"2START" + batchid.Text;
                        cp.JSProperties["cp_progress"] = dtsku5.Rows[0]["CProgress"].ToString();
                        cp.JSProperties["cp_sku"] = dtsku5.Rows[0]["SKUcode"].ToString();
                          wipSKU.Text = dtsku5.Rows[0]["SKUcode"].ToString();
                        cp.JSProperties["cp_skuname"] = dtsku5.Rows[0]["ProductName"].ToString();
                        cp.JSProperties["cp_batchno"] = dtsku5.Rows[0]["BatchNo"].ToString();
                        cp.JSProperties["cp_recid"] = param1a;
                        cp.JSProperties["cp_curstep"] = dtsku5.Rows[0]["CurStep"].ToString();
                        cp.JSProperties["cp_ExpectedOutputQty"] = dtsku5.Rows[0]["ExpectedOutputQty"].ToString();
                        //wipcurrstep.Text = dtsku5.Rows[0]["CurStep"].ToString();
                        //skudesc.Text = dtsku5.Rows[0]["ProductName"].ToString();
                        //wipprogress.Text = dtsku5.Rows[0]["CProgress"].ToString();
                      
                    }

                    Initialize();

                    break;
                case "batch":
                    string param1 = e.Parameter.Split('|')[1];
                    //2021-05-26    EMC update database ProdQueue
                    
                    //if(param1 == "0")
                    //{
                    //    param1 = batchid.Text;
                    //}

                    string param2 = "0";

                    if(chkbackflash.Checked)
                    {
                        param2 = "1";
                    }

                    wiptype.Text = param1;

                    //param1 = batchid.Text;
                    
                    //EXEC [dbo].[sp_ProdBatchQueue] 1,0,'Curing','UPDATE'
                    //EMC999
                    string strBatchSQL = " EXEC dbo.sp_ProdBatchQueue " + batchid.Text + "," + param2 + ",'" + wipprocess.Text + "'," + Qtywip.Text + ",'" + wipuserid.Text + "','" + wiptype.Text + "','" + NumStrands.Value + '|' + glStuffingMach.Value + '|' + txtWeightSmokecart.Text + '|' + txtWeightbefore.Text + '|' + cbQCStick.Value + '|' + cbQCHotdog.Value + '|' + cbQCfreefrom.Value + '|' + Shift.Value + '|' + TimeOn.Text + '|' + TimeOff.Text + '|' + txtBlastTemp.Text + '|' + txtProductName.Text + '|' + txtPD.Text + '|' + Convert.ToDecimal(Convert.IsDBNull(NumPacks.Value) ? 0 : Convert.ToDecimal(NumPacks.Value)) + '|' + txtLoadedBy.Text + '|' + txtMonitoredBy.Text + '|' + txtCheckedBy.Text + '|' + glSpiralMach.Value + '|' + Convert.ToDecimal(Convert.IsDBNull(QtyPacks.Value) ? 0 : Convert.ToDecimal(QtyPacks.Value)) + '|' + Convert.ToDecimal(Convert.IsDBNull(QtyLoosePacks.Value) ? 0 : Convert.ToDecimal(QtyLoosePacks.Value)) + '|' + TimeStarted.Text + '|' + TimeFinished.Text + '|' + Convert.ToDecimal(Convert.IsDBNull(IntTempPL.Value) ? 0 : Convert.ToDecimal(IntTempPL.Value)) + '|' + Convert.ToDecimal(Convert.IsDBNull(StdRoomTemp.Value) ? 0 : Convert.ToDecimal(StdRoomTemp.Value)) + '|' + txtQAVSpiral.Text + '|' + txtQAVPLoad.Text + '|' + txtQAVValBy.Text + '|' + glSmokehouse.Value + '|' + TimeStandard.Text + '|' + TimeStart.Text + '|' + TimeEnd.Text + '|' + txtStoveTemp.Text + '|' + txtStoveTempAct.Text + '|' + txtHumiditySTD.Text + '|' + txtHumidityAct.Text + '|' + txtSteam.Text + '|' + txtInternal.Text + '|' + TxtWeighingAC.Text + '|' + txtValidated.Text + '|' + chkbackflash.Value + "'  ";

                    DataTable dtBatch1 = Gears.RetriveData2(strBatchSQL, Session["ConnString"].ToString());

                    if (dtBatch1.Rows.Count > 0)
                    {
                        cp.JSProperties["cp_batch"] = true;
                        //cp.JSProperties["cp_wipprocess"] = wipprocess.Text; //"2START" + batchid.Text;                    
                        //cp.JSProperties["cp_batchstatus"] = dtBatch1.Rows[0]["Cstatus"].ToString(); // "Started"; //"2START" + batchid.Text;
                        cp.JSProperties["cp_progress"] = dtBatch1.Rows[0]["CProgress"].ToString();
                    }

                    

                    //gv1.row

                    break;
                case "Add":
                    if (error == false)
                    {
                       
                        _Entity.UpdateData(_Entity);
                          gv1.DataSourceID = "odsDetail";
                        
                        //odsDetail.SelectParameters["DocNumber"].DefaultValue = txtDocnumber.Text;
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

                        cp.JSProperties["cp_message"] = "Successfully Updated! 2 ";
                        cp.JSProperties["cp_success"] = true;
                        cp.JSProperties["cp_close"] = true;
                        Session["Refresh"] = "1";

                    //if (error == false)
                    //{
                    //    _Entity.LastEditedBy = Session["userid"].ToString();

                    //    _Entity.LastEditedDate = DateTime.Now.ToString();
                    //    strError = Functions.Submitted(_Entity.DocNumber, "Production.PreProductionOut", 1, Session["ConnString"].ToString());//NEWADD factor 1 if submit, 2 if approve
                    //    if (!string.IsNullOrEmpty(strError))
                    //    {
                    //        gv1.JSProperties["cp_message"] = strError;
                    //        gv1.JSProperties["cp_success"] = true;
                    //        gv1.JSProperties["cp_forceclose"] = true;
                    //        return;
                    //    }

                    //    _Entity.UpdateData(_Entity);
                      
                    //    gv1.DataSourceID = "odsDetail";
                      
                    //    gv1.UpdateEdit();
                    //    Validate();

                    //    cp.JSProperties["cp_message"] = "Successfully Updated!";

                    //    cp.JSProperties["cp_success"] = true;
                    //    cp.JSProperties["cp_close"] = true;
                    //    Session["Refresh"] = "1";
                    //}
                    //else
                    //{
                    //    cp.JSProperties["cp_message"] = "Please check all the fields!";
                    //    cp.JSProperties["cp_success"] = true;
                    //}

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


                case "FilterStep":

                  

                 


                    break;

                case "ChangeStep":
               
                    ASPxFormLayout1.FindItemOrGroupByName("Process").ClientVisible = false;
                    
                    break;



               
            }
        }

        protected void cp2_Callback(object sender, CallbackEventArgsBase e)
        {
            wipprocess.DataBind();
        }

        protected void Connection_Init(object sender, EventArgs e)
        {
            //sdsJobOrder.ConnectionString = Session["ConnString"].ToString();
            //sdsStep.ConnectionString = Session["ConnString"].ToString();
            //sdsDetail.ConnectionString = Session["ConnString"].ToString();
            ((SqlDataSource)sender).ConnectionString = Session["ConnString"].ToString();
        }
        protected void glSizeCode_Init(object sender, EventArgs e)
        {
            //ASPxGridLookup gridLookup = sender as ASPxGridLookup;
            //gridLookup.GridView.CustomCallback += new ASPxGridViewCustomCallbackEventHandler(rate_CustomCallback);
            //if (Session["JobOrder"] != null)
            //{
            //    gridLookup.GridView.DataSourceID = "sdsJobOrder";
            //    //sdsJobOrder.FilterExpression = Session["JobOrder"].ToString();
            //    gridLookup.DataBind();
            //}
        }
        protected void wipprocess_Init(object sender, EventArgs e)
        {
            if (Session["Prod_Init"] != null)
            {
                // 2021-11-05   TL  Connectionstring set by Connection_Init
                //sdsStep.ConnectionString = Session["ConnString"].ToString();
                // 2021-11-05   TL  (End)
                sdsStep.SelectCommand = Session["Prod_Init"].ToString();
                // 2021-11-05   TL  May not be needed
                //sdsStep.DataBind();
                //wipprocess.DataBind();
                // 2021-11-05   (End)
            }
        }
    


        public void rate_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            //ASPxGridView grid = sender as ASPxGridView;
            //CriteriaOperator selectionCriteria = new InOperator("StepCode", new string[] { glStep.Text });
            //sdsJobOrder.FilterExpression = (GroupOperator.Combine(GroupOperatorType.And, selectionCriteria)).ToString();
            //Session["JobOrder"] = sdsJobOrder.FilterExpression;
            //grid.DataSourceID = "sdsJobOrder";
            //grid.DataBind();

            //for (int i = 0; i < grid.VisibleRowCount; i++)
            //{
            //    if (grid.GetRowValues(i, "StepCode") != null)
            //        if (grid.GetRowValues(i, "StepCode").ToString() == e.Parameters)
            //        {
            //            grid.Selection.SelectRow(i);
            //            string key = grid.GetRowValues(i, "StepCode").ToString();
            //            grid.MakeRowVisible(key);
            //            break;
            //        }
            //}

        }

        protected void glItemCode_Init(object sender, EventArgs e)
        {
            ASPxGridLookup gridLookup = sender as ASPxGridLookup;
            gridLookup.GridView.CustomCallback += new ASPxGridViewCustomCallbackEventHandler(gridView_CustomCallback);

        }

        public void gridView_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            //string column = e.Parameters.Split('|')[0];
            //if (column.Contains("GLP_AIC") || column.Contains("GLP_AC") || column.Contains("GLP_F")) return;
            //string itemcode = e.Parameters.Split('|')[1];
            //string itemcode1 = e.Parameters.Split('|')[2];
            //string val = e.Parameters.Split('|')[2];
            //if (val.Contains("GLP_AIC") || val.Contains("GLP_AC") || val.Contains("GLP_F")) return;

            //var itemlookup = sender as ASPxGridView;
            //string codes = "";
            //if (e.Parameters.Contains("Step"))
            //{

            //    itemlookup.JSProperties["cp_codes"] = itemcode1;
            //}

        }

     
   
        #endregion

        protected void dtpDocDate_Init(object sender, EventArgs e)
        {
            if (Request.QueryString["entry"] == "N")
            {
                //dtpdocdate.Date = DateTime.Now;
            }
        }
    }
}