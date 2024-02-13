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
using GearsWarehouseManagement;
using System.Data.SqlClient;

namespace GWL.Production
{
    public partial class frmProdScheduling : System.Web.UI.Page
    {
        Boolean error = false;//Boolean for grid validation
        Boolean view = false;//Boolean for view state
        Boolean check = false;//Boolean for grid validation
        private static string Connection;

        Entity.WorkCenter _Entity = new WorkCenter();//Calls entity ICN

        List<object> selectedValues;
        

        #region page load/entry
        protected void Page_Load(object sender, EventArgs e)
        {
            
            Gears.UseConnectionString(Connection);
            //string referer;
            //try //Validation to restrict user to browse/type directly to browser's address
            //{
            //    referer = Request.ServerVariables["http_referer"];
            //}
            //catch
            //{
            //    referer = "";
            //}

            //if (referer == null)
            //{
            //    Response.Redirect("~/error.aspx");
            //}
         
            //gv1.KeyFieldName = "DocNumber;LineNumber";

            ////Details checking. changes datasourceid of gridview if there's no details for a certain docnumber; to enable adding of details.
            if (Request.QueryString["entry"].ToString() == "V" || Request.QueryString["entry"].ToString() == "D")
            {
                view = true;//sets view mode for entry
            }

            if (!IsPostBack)
            {
                

               //sets docnumber from session
                if (Request.QueryString["entry"].ToString() == "N")
                {
                   // gv1.DataSourceID = "sdsDetail";
                  
                    //sdsDetail.SelectCommand = "select * from WMS.PICKLIST where docnumber is null";
                    //sdsDetail.SelectParameters.Remove(sdsDetail.SelectParameters["DocNumber"]);
                }
                else
                {
                    _Entity.getdata(Request.QueryString["docnumber"].ToString(), Session["ConnString"].ToString());

                    txtWorkCenter.Value = _Entity.WorkCenterCode;
                    //txtDescription.Text = _Entity.Description;
                    //txtStep.Text = _Entity.Step;
                    //txtDays.Text = _Entity.Days.ToString();
                    //txtShift.Text = _Entity.Shift.ToString();
                    //txtOPHours.Text = _Entity.OperatingShift.ToString();
                    //txtSAM.Text = _Entity.SAM.ToString();
                    //txtUtil.Text = _Entity.Utilization.ToString();
                    //txtMachine.Text = _Entity.MachineQty.ToString();
                    //txtDaily.Text = _Entity.OutputDays.ToString();
                    //txtWeekly.Text = _Entity.OutputWeekly.ToString();
                }

                switch (Request.QueryString["entry"].ToString())
                {
                    case "E":
                        txtWorkCenter.ReadOnly = true;
                        break;
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

        protected void CheckBoxLoad(object sender, EventArgs e)//Control for all textbox
        {
            ASPxCheckBox check = sender as ASPxCheckBox;
            check.ReadOnly = view;
        }
        protected void LookupLoad(object sender, EventArgs e)//Control for all lookup in header
        {
            ASPxGridLookup look = sender as ASPxGridLookup;
            look.DropDownButton.Enabled = !view;
            look.ReadOnly = view;
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
            if (Request.QueryString["entry"].ToString() == "V")
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
            _Entity.LastEditedBy = Session["userid"].ToString();
            _Entity.AddedBy = Session["userid"].ToString();
            //List<object> fieldValues = gvUnloaded.GetSelectedFieldValues(recordID);
            //List<object> fieldStep = gvUnloaded.GetSelectedFieldValues(step);
            string strError = "";
            string param = e.Parameter.Split('|')[0]; //Renats
            switch (param)
            {
                case "Generate":
                    Requery();
                    break;

            case "Load":
                    GetSelectedValues();
                    foreach (object[] item in selectedValues)
                    {
                            if (txtStep.Text.TrimEnd().TrimStart().ToUpper() != item[6].ToString().TrimEnd().TrimStart().ToUpper())
                            {
                                cp.JSProperties["cp_message"] = "Can't Load to Subcon Because it has different Step";
                                cp.JSProperties["cp_success"] = true;
                                return;
                            }

                            try
                            {
                                Gears.RetriveData2(string.Format("exec sp_LoadSubcon '{0}',{1}", txtWorkCenter.Text, item[11]), Connection);
                            }
                            catch (Exception ex)
                            {
                                strError += "\n" + ex.Message;
                            }
                    }
                    if (!String.IsNullOrEmpty(strError))
                    {
                        cp.JSProperties["cp_message"]=strError;
                    }
                    else
                    {
                        cp.JSProperties["cp_message"]="Loaded Successfully!";
                    }
                    cp.JSProperties["cp_success"] = true;
                    Requery();
                    break;

            case "Unload":
                    GetSelectedValues2();
                    foreach (object[] item in selectedValues)
                    {
                        if (txtStep.Text.TrimEnd().TrimStart() != item[6].ToString().TrimEnd().TrimStart().ToUpper())
                        {
                            cp.JSProperties["cp_message"] = "Can't Load to Subcon Because it has different Step";
                            cp.JSProperties["cp_success"] = true;
                            return;
                        }

                        try
                        {
                            Gears.RetriveData2("exec sp_UnloadSubcon " + item[11].ToString() + "", Connection);
                        }
                        catch (Exception ex)
                        {
                            strError += "\n" + ex.Message;
                        }
                    }
                    if (!String.IsNullOrEmpty(strError))
                    {
                        cp.JSProperties["cp_message"] = strError;
                    }
                    else
                    {
                        cp.JSProperties["cp_message"] = "Unloaded Successfully!";
                    }
                    cp.JSProperties["cp_success"] = true;
                    Requery();
                    break;

                case "Date":
                    GetSelectedValues2();

                    if (selectedValues.Count > 1)
                    {
                        cp.JSProperties["cp_message"] = "cannot select multiple JO!";
                        cp.JSProperties["cp_success"] = true;
                        cp.JSProperties["cp_popup"] = false;
                        Requery();
                        return;
                    }
                    else if (selectedValues.Count == 0)
                    {
                        cp.JSProperties["cp_message"] = "No selected JO!";
                        cp.JSProperties["cp_success"] = true;
                        cp.JSProperties["cp_popup"] = false;
                        Requery();
                        return;
                    }

                    foreach (object[] item in selectedValues)
                    {
                        string strSQL = " Select Sequence,Stepcode,TargetDateIn,TargetDateOut "
                            + ",DateCommitted, WorkCenter, DocNumber, RecordID from Production.JOStepPlanning "
                            + "where DocNumber = '" + item[1].ToString() + "' and isnull(PreProd,0)=0 ORDER BY Sequence ";
                        DataTable JOList = Gears.RetriveData2(strSQL, Connection);
                        gvSteps.DataSource = JOList;
                        gvSteps.DataBind();
                        Requery();
                        DatePop.HeaderText = "JO Step for JO " + item[1].ToString();
                        cp.JSProperties["cp_popup"] = true;
                        break;
                    }
                    break;

                case "Update":
                    gvSteps.UpdateEdit();
                    foreach (object[] item in selectedValues)
                    {
                        string strSQL = " Select Sequence,Stepcode,TargetDateIn,TargetDateOut "
                            + ",DateCommitted, WorkCenter, DocNumber, RecordID from Production.JOStepPlanning "
                            + "where DocNumber = '" + item[1].ToString() + "' and isnull(PreProd,0)=0 ORDER BY Sequence ";
                        DataTable JOList = Gears.RetriveData2(strSQL, Connection);
                        gvSteps.DataSource = JOList;
                        gvSteps.DataBind();
                        Requery();
                        DatePop.HeaderText = "JO Step for JO " + item[1].ToString();
                        break;
                    }
                    break;

                case "Close":
                    cp.JSProperties["cp_close"] = true;
                    Session["Refresh"] = "1";

                    break;

                case "Filter":
                    Requery();
                    break;

                case "JO":
                    txtStep.Value = e.Parameter.Split('|')[2];
                    txtWorkCenter.Value = e.Parameter.Split('|')[3];
                    break;
            }
        }

        private void Requery()
        {
            gvUnloaded.DataSource = null;
            gvUnloaded.DataBind();
            gvLoaded.DataSource = null;
            gvLoaded.DataBind();
            if (!String.IsNullOrEmpty(txtStep.Text) & !String.IsNullOrEmpty(txtWorkCenter.Text))
            {
                gvLoaded.Caption = "List of JO loaded to " + txtWorkCenter.Text;
                string strSQL = "";
                int intUnpost = 0;
                if (this.cbInclude.CheckState == CheckState.Checked) intUnpost = 1;

                strSQL = "exec sp_ProdPlan2 @StepCodeUsed = '" + txtStep.Text + "', @BizPartner = '" + txtWorkCenter.Text + "', @Unposted = " + intUnpost.ToString();


                DataTable JOList = Gears.RetriveData2(strSQL, Connection);
                gvLoaded.DataSource = JOList;

                //strSQL = "Select ROW_NUMBER() Over (order by a.DocNumber,Status,Stockcode, JODueDate,StepCode,TotalJOQty,TargetDateIn, TargetDateOut, " +
                //            "Subcon,StepCode,Sequence,RecordID) as No,a.DocNumber as JONumber,Status,Stockcode, JODueDate,Stepcode,TotalJOQty as JOQty, " +
                //            "TargetDateIn, TargetDateOut, " +
                //            "Subcon,Sequence,RecordID  " +
                //            "From Production.JobOrder A, Production.JOStep B  " +
                //            "where A.DocNumber = b.DocNumber   " +
                //            "AND A.status IN ('N','W')  AND Rtrim(ISNULL(Subcon,''))='' AND StepCode = '" + txtStep.Text.TrimEnd() + "' ";

                string submitted = "and isnull(AllocSubmittedBy,'')!=''";
                if (this.cbInclude.CheckState == CheckState.Checked) submitted = "";
                strSQL = "select ROW_NUMBER() Over (order by JONumber,RecordID,Status,ItemCode, DueDate,StepCode, " +
                            "JOQty,TargetDateIn, WorkCenter,StepCode,Sequence) as No,* from( "+
                            "Select distinct a.DocNumber as JONumber,b.RecordID,Status,ItemCode, "+
                            "DueDate,Stepcode, CustomerCode, ItemCode as Stockcode ,TotalJOQty as JOQty, TargetDateIn, TargetDateOut, WorkCenter,Sequence  " +
                            "From Production.JobOrder A, Production.JOStepPlanning B, Production.JOProductOrder C "+
                            "where A.DocNumber = b.DocNumber AND A.DocNumber = C.DocNumber AND B.DocNumber = C.DocNumber AND " +
                            "A.status IN ('N','W')  AND Rtrim(ISNULL(WorkCenter,''))='' AND StepCode = '" + txtStep.Text.TrimEnd() + 
                             "' " + submitted +") t";
                //((Subcon != '" + txtSubcon.Text + "') OR

                DataTable JONOList = Gears.RetriveData2(strSQL, Connection);
                gvUnloaded.DataSource = JONOList;
                
                DataView dv = new DataView(JOList);
                DataView dv2 = new DataView(JONOList);

                if(JOList.Rows.Count > 0){
                    dv.RowFilter = DoFilter(true);
                }

                if (JOList.Rows.Count > 0){
                        dv2.RowFilter = DoFilter2(true);
                }

                gvLoaded.DataSource = dv;
                gvUnloaded.DataSource = dv2;

                gvUnloaded.DataBind();
                gvLoaded.DataBind();
                //if (dgvJOList.ColumnCount > 1)
                //{
                //    dgvJOList.Columns[1].Frozen = true;

                //}

                //if (dgvJOList.ColumnCount > 1)
                //{
                //    dgvNoJoList.Columns[1].Frozen = true;
                //}
                gvUnloaded.Selection.UnselectAll();
            }
        }

        private string DoFilter(bool isYourJo)
        {
            string Col = "", Val = "";

            if (isYourJo)
            {
                if (cbFilter.Text.ToString().Trim() == "DocNumber")
                {
                    Col = "JONumber";
                }
                else
                {
                    Col = cbFilter.Text.ToString().Trim();
                }
                Val = txtFilter.Text.ToString().Trim();
            }
            //else
            //{
            //    Col = cbFilter2.Text.ToString().Trim();
            //    Val = txtFilter2.Text.ToString().Trim();
            //}

            if (string.IsNullOrEmpty(Col) || string.IsNullOrEmpty(Val))
            {
                return "";
            }
            string StrRet = string.Format("{0} LIKE '*{1}*' ", Col, Val);
            return StrRet;
        }

        private string DoFilter2(bool isYourJo)
        {
            string Col = "", Val = "";

            if (isYourJo)
            {
                if (cbFilter2.Text.ToString().Trim() == "DocNumber")
                {
                    Col = "JONumber";
                }
                else
                {
                    Col = cbFilter2.Text.ToString().Trim();
                }
                Val = txtFilter2.Text.ToString().Trim();
            }
            //else
            //{
            //    Col = cbFilter2.Text.ToString().Trim();
            //    Val = txtFilter2.Text.ToString().Trim();
            //}

            if (string.IsNullOrEmpty(Col) || string.IsNullOrEmpty(Val))
            {
                return "";
            }
            string StrRet = string.Format("{0} LIKE '*{1}*' ", Col, Val);
            return StrRet;
        }

        private void GetSelectedValues()//Getting selected values in grid function
        {
            List<string> fieldNames = new List<string>();
            foreach (GridViewColumn column in gvUnloaded.Columns)
                if (column is GridViewDataColumn)
                    fieldNames.Add(((GridViewDataColumn)column).FieldName);
            selectedValues = gvUnloaded.GetSelectedFieldValues(fieldNames.ToArray());
        }

        private void GetSelectedValues2()//Getting selected values in grid function
        {
            List<string> fieldNames = new List<string>();
            foreach (GridViewColumn column in gvLoaded.Columns)
                if (column is GridViewDataColumn)
                    fieldNames.Add(((GridViewDataColumn)column).FieldName);
            selectedValues = gvLoaded.GetSelectedFieldValues(fieldNames.ToArray());
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

        protected void Connection_Init(object sender, EventArgs e)
        {
            Connection = Session["ConnString"].ToString();
            Masterfilebiz.ConnectionString = Connection;
            MasterfileStep.ConnectionString = Connection;
            Masterfileitem.ConnectionString = Connection;
            JobOrder.ConnectionString = Connection;
        }

        protected void gvUnloaded_DataBound(object sender, EventArgs e)
        {
            ASPxGridView grid = sender as ASPxGridView;
            if (grid.Columns.IndexOf(grid.Columns["CommandColumn"]) != -1)
                return;
            GridViewCommandColumn col = new GridViewCommandColumn();
            col.Name = "CommandColumn";
            
            col.ShowSelectCheckbox = true;
            col.SelectAllCheckboxMode = GridViewSelectAllCheckBoxMode.Page;
            col.VisibleIndex = 0;
            col.Width = 30;
            col.FixedStyle = GridViewColumnFixedStyle.Left;
            grid.Columns.Add(col);
        }

        protected void gvSteps_BatchUpdate(object sender, ASPxDataBatchUpdateEventArgs e)
        {
            e.Handled = true;
            DataTable JOList = new DataTable();
            GetSelectedValues2();
            foreach (object[] item in selectedValues)
                    {
                        string strSQL = " Select Sequence,Stepcode,TargetDateIn,TargetDateOut "
                            + ",DateCommitted, WorkCenter, DocNumber, RecordID from Production.JOStepPlanning "
                            + "where DocNumber = '" + item[1].ToString() + "' and isnull(PreProd,0)=0 ORDER BY Sequence ";
                        JOList = Gears.RetriveData2(strSQL, Connection);
                        JOList.PrimaryKey = new DataColumn[] { JOList.Columns["RecordID"] };
                        break;
                    }

            foreach (ASPxDataUpdateValues values in e.UpdateValues)
            {
                object[] keys = { values.Keys[0] };
                DataRow row = JOList.Rows.Find(keys);
                row["TargetDateIn"] = values.NewValues["TargetDateIn"] != null ? (object)DateTime.Parse(values.NewValues["TargetDateIn"].ToString()).ToShortDateString() : DBNull.Value;
                row["TargetDateOut"] = values.NewValues["TargetDateOut"] != null ? (object)DateTime.Parse(values.NewValues["TargetDateOut"].ToString()).ToShortDateString() : DBNull.Value;

            }

            foreach (DataRow dtRow in JOList.Rows)//This is where the data will be inserted into db
            {
                string strStoredproc = "sp_UpdateTargetDate";
                
                using (SqlConnection connection = new SqlConnection(Connection))
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand(strStoredproc, connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@DocNumber", dtRow["DocNumber"]));
                    cmd.Parameters.Add(new SqlParameter("@Sequence", Convert.ToDecimal(dtRow["Sequence"])));
                    cmd.Parameters.Add(new SqlParameter("@RecordID", Convert.ToInt32(dtRow["RecordID"])));
                    if (dtRow["TargetDateIn"] != DBNull.Value) { cmd.Parameters.Add(new SqlParameter("@TargetDateIn", Convert.ToDateTime(dtRow["TargetDateIn"]).ToShortDateString())); }
                    else { cmd.Parameters.Add(new SqlParameter("@TargetDateIn", DBNull.Value)); }
                    if (dtRow["TargetDateOut"] != DBNull.Value) { cmd.Parameters.Add(new SqlParameter("@TargetDateOut", Convert.ToDateTime(dtRow["TargetDateOut"]).ToShortDateString())); }
                    else { cmd.Parameters.Add(new SqlParameter("@TargetDateOut", DBNull.Value)); }
                    cmd.Parameters.Add(new SqlParameter("@WorkCenter", txtWorkCenter.Text));
                    String strMessage = (String)cmd.ExecuteScalar();
                    connection.Close();
                }
            }
        }
    }
}