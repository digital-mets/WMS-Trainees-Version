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
using GearsProduction;

namespace GWL
{
    public partial class frmDISout : System.Web.UI.Page
    {
        Boolean error = false; //Boolean for grid validation
        Boolean view = false;  //Boolean for view state
        Boolean check = false; //Boolean for grid validation
        private static string Connection;

        string a = "";
        string b = "";

        Entity.DISout _Entity = new DISout();//Calls entity Room
        Entity.DISout.DISoutDetail _EntityDetail = new DISout.DISoutDetail();//Call entity sdsDetail

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

            if (Request.QueryString["entry"].ToString() == "V" || Request.QueryString["entry"].ToString() == "D")
            {
                view = true;
            }

            if (!IsPostBack)
            {
                Session["DISvalue"] = null;
                Session["Datatable"] = null;
                Session["a"] = "";
                Session["b"] = "";
                Connection = Session["ConnString"].ToString();
                txtDocNumber.Value = Request.QueryString["docnumber"].ToString();
                _Entity.getdata(txtDocNumber.Text, Session["ConnString"].ToString());//ADD CONN

                dtpDocDate.Value = String.IsNullOrEmpty(_Entity.DocDate) ? DateTime.Now : Convert.ToDateTime(_Entity.DocDate);
                txtStep.Text = _Entity.Step;
                glDIS.Text = _Entity.DIS;
                txtWorkCenter.Text = _Entity.WorkCenter;
                glReceivingStep.Text = _Entity.ReceivingStep;
                txtReceivingWorkCenter.Text = _Entity.ReceivingWorkCenter;
                txtDRDocNo.Text = _Entity.DRDocNo;
                txtTotalAmount.Text = _Entity.TotalAmount.ToString();
                txtLaborCost.Text = _Entity.LaborCost.ToString();
                //glSize.Value = _Entity.Size;
                txtQty.Text = _Entity.Qty.ToString();
                glWarehouse.Value = _Entity.Warehouse;
                cbxFinalDIS.Value = _Entity.FinalDIS;
                memRemarks.Text = _Entity.Remarks;

                Session["DISvalue"] = _Entity.DIS;

                txtAddedBy.Text = _Entity.AddedBy;
                txtAddedDate.Text = _Entity.AddedDate;
                txtLastEditedBy.Text = _Entity.LastEditedBy;
                txtLastEditedDate.Text = _Entity.LastEditedDate;
                txtSubmittedBy.Text = _Entity.SubmittedBy;
                txtSubmittedDate.Text = _Entity.SubmittedDate;
                //txtPostedBy.Text = _Entity.PostedBy;
                //txtPostedDate.Text = _Entity.PostedDate;

                txtHField1.Text = _Entity.Field1;
                txtHField2.Text = _Entity.Field2;
                txtHField3.Text = _Entity.Field3;
                txtHField4.Text = _Entity.Field4;
                txtHField5.Text = _Entity.Field5;
                txtHField6.Text = _Entity.Field6;
                txtHField7.Text = _Entity.Field7;
                txtHField8.Text = _Entity.Field8;
                txtHField9.Text = _Entity.Field9;

                updateBtn.Text = "Add";
                FormLayout.FindItemOrGroupByName("ReferenceTransaction").ClientVisible = false;
                if (!string.IsNullOrEmpty(_Entity.LastEditedBy))
                {
                    updateBtn.Text = "Update";
                    FormLayout.FindItemOrGroupByName("ReferenceTransaction").ClientVisible = true;
                }
                switch (Request.QueryString["entry"].ToString())
                {
                    case "N":
                        break;
                    case "E":
                        updateBtn.Text = "Update";
                        FormLayout.FindItemOrGroupByName("ReferenceTransaction").ClientVisible = true;
                        break;
                    case "V":
                        ForViewAndDeleteLookup();
                        glcheck.ClientVisible = false;
                        updateBtn.Text = "Close";
                        FormLayout.FindItemOrGroupByName("ReferenceTransaction").ClientVisible = true;
                        break;
                    case "D":
                        ForViewAndDeleteLookup();
                        glcheck.ClientVisible = false;
                        updateBtn.Text = "Delete";
                        FormLayout.FindItemOrGroupByName("ReferenceTransaction").ClientVisible = true;
                        break;
                }

                if (_Entity.DIS != null && (cbxFinalDIS.Checked == false || cbxFinalDIS.Checked == null))
                {
                    txtReceivingWorkCenter.ClientEnabled = true;
                    glReceivingStep.ClientEnabled = true;
                    Session["a"] = _Entity.DIS;
                    Session["b"] = _Entity.Step;
                    sdsReceiving.SelectCommand = "select B.DocNumber AS DISDocNumber, Step, WorkCenter FROM Production.DISStep A INNER JOIN Production.DIS B ON A.DocNumber = B.DocNumber WHERE ISNULL(A.DateOUT,'') = '' AND Status IN ('N','W') AND ISNULL(SubmittedBy,'') != '' and A.DocNumber = '" + _Entity.DIS + "' AND A.Step != '" + _Entity.Step + "'";
                    sdsReceiving.DataBind();
                }

                gvRef.DataSourceID = "odsReference";
                this.gvRef.Columns["CommandString"].Width = 0;
                this.gvRef.Columns["RCommandString"].Width = 0;

                DataTable dtbldetail = Gears.RetriveData2("SELECT DocNumber FROM Production.DISoutDetail Where DocNumber = '" + txtDocNumber.Text + "'", Session["ConnString"].ToString());//ADD CONN
                gv1.DataSourceID = (dtbldetail.Rows.Count > 0 ? "odsDetail" : "sdsDetail");
                //gvJournal.DataSourceID = "odsJournalEntry";
            }
        }
        #endregion

        #region Validation
        private void Validate()
        {
            GearsLibrary.Gears.GearsParameter gparam = new Gears.GearsParameter();
            gparam._Connection = Session["ConnString"].ToString();
            gparam._DocNo = _Entity.DocNumber;
            gparam._UserId = Session["userid"].ToString();
            gparam._TransType = "PRDOUT";
            string strresult = GearsProduction.GProduction.DISOut_Validate(gparam);
            cp.JSProperties["cp_valmsg"] = strresult;//Message variable to client side
        }
        #endregion
       
        //#region Post
        //private void Post()
        //{
        //    GearsLibrary.Gears.GearsParameter gparam = new Gears.GearsParameter();
        //    gparam._Connection = Session["ConnString"].ToString();
        //    gparam._DocNo = _Entity.DocNumber;
        //    gparam._UserId = Session["Userid"].ToString();
        //    gparam._TransType = "ACTCAD";
        //    gparam._Table = "Production.DISout";
        //    gparam._Factor = -1;
        //    string strresult = GearsAccounting.GAccounting.DISout_Post(gparam);
        //    if (strresult != " ")
        //    {
        //        cp.JSProperties["cp_valmsg"] = strresult;//Message variable to client side
        //    }
        //}
        //#endregion

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
            _Entity.DocNumber = txtDocNumber.Text;
            _Entity.Connection = Session["ConnString"].ToString();
            _Entity.DocDate = dtpDocDate.Text;
            _Entity.Step = txtStep.Text;
            _Entity.DIS = glDIS.Text;
            _Entity.WorkCenter = txtWorkCenter.Text;
            _Entity.ReceivingStep = glReceivingStep.Text;
            _Entity.ReceivingWorkCenter = txtReceivingWorkCenter.Text;
            _Entity.DRDocNo = txtDRDocNo.Text;
            _Entity.TotalAmount = Convert.ToDecimal(Convert.IsDBNull(txtTotalAmount.Value) ? 0 : Convert.ToDecimal(txtTotalAmount.Value));
            _Entity.LaborCost = Convert.ToDecimal(Convert.IsDBNull(txtLaborCost.Value) ? 0 : Convert.ToDecimal(txtLaborCost.Value));
            //_Entity.Size = glSize.Text;
            _Entity.Qty = Convert.ToDecimal(Convert.IsDBNull(txtQty.Value) ? 0 : Convert.ToDecimal(txtQty.Value));
            _Entity.Warehouse = glWarehouse.Text;
            _Entity.FinalDIS = Convert.ToBoolean(Convert.IsDBNull(cbxFinalDIS.Value.ToString()) ? false : Convert.ToBoolean(cbxFinalDIS.Value.ToString()));
            _Entity.Remarks = memRemarks.Text;
            
            _Entity.AddedBy = txtAddedBy.Text;
            _Entity.AddedDate = txtAddedDate.Text;
            _Entity.LastEditedBy = Session["userid"].ToString();

            _Entity.Field1 = txtHField1.Text;
            _Entity.Field2 = txtHField2.Text;
            _Entity.Field3 = txtHField3.Text;
            _Entity.Field4 = txtHField4.Text;
            _Entity.Field5 = txtHField5.Text;
            _Entity.Field6 = txtHField6.Text;
            _Entity.Field7 = txtHField7.Text;
            _Entity.Field8 = txtHField8.Text;
            _Entity.Field9 = txtHField9.Text;

            string param = e.Parameter.Split('|')[0];

            switch (param)
            {
                case "Add":
                    gv1.UpdateEdit();
                    string strError = Functions.Submitted(_Entity.DocNumber, "Production.DISout", 1, Connection);//NEWADD factor 1 if submit, 2 if approve
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
                        _Entity.UpdateData(_Entity);
                        if (Session["Datatable"] == "1")
                        {
                            Gears.RetriveData2("DELETE FROM Production.DISoutDetail WHERE DocNumber = '" + txtDocNumber.Text + "'", Session["ConnString"].ToString());//ADD CONN
                            gv1.KeyFieldName = "LineNumber";
                            gv1.UpdateEdit();
                        }
                        else
                        {
                            gv1.DataSourceID = "odsDetail";
                            odsDetail.SelectParameters["DocNumber"].DefaultValue = txtDocNumber.Text;
                            gv1.UpdateEdit();
                        }
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
                    gv1.UpdateEdit();
                    string strError1 = Functions.Submitted(_Entity.DocNumber, "Production.DISout", 1, Connection);//NEWADD factor 1 if submit, 2 if approve
                    if (!string.IsNullOrEmpty(strError1))
                    {
                        cp.JSProperties["cp_message"] = strError1;
                        cp.JSProperties["cp_success"] = true;
                        cp.JSProperties["cp_forceclose"] = true;
                        return;
                    }
                    if (error == false)
                    {
                        check = true;
                        _Entity.LastEditedBy = Session["userid"].ToString();
                        _Entity.UpdateData(_Entity);//Method of Updating header

                        if (Session["Datatable"] == "1")
                        {
                            Gears.RetriveData2("DELETE FROM Production.DISoutDetail WHERE DocNumber = '" + txtDocNumber.Text + "'", Session["ConnString"].ToString());//ADD CONN
                            gv1.KeyFieldName = "LineNumber";
                            gv1.UpdateEdit();
                        }
                        else
                        {
                            gv1.DataSourceID = "odsDetail";
                            odsDetail.SelectParameters["DocNumber"].DefaultValue = txtDocNumber.Text;
                            gv1.UpdateEdit();
                        }
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
                    check = true;
                    cp.JSProperties["cp_delete"] = true;
                    _Entity.DeleteData(_Entity);
                        cp.JSProperties["cp_message"] = "Successfully Deleted!";
                        cp.JSProperties["cp_success"] = true;
                        cp.JSProperties["cp_close"] = true;
                        Session["Refresh"] = "1";
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

                case "DIS":
                    GetSelectedVal();
                    a = e.Parameter.Split('|')[1];
                    b = e.Parameter.Split('|')[2];

                    SqlDataSource ds = sdsDIS;
                    ds.SelectCommand = string.Format("select A.DocNumber AS DISDocNumber, DISNumber, Step, WorkCenter, ISNULL(LaborCost,0) AS LaborCost FROM Production.DISStep A INNER JOIN Production.DIS B ON A.DocNumber = B.DocNumber WHERE B.DocNumber = '" + a + "' AND A.Step = '" + b + "'");
                    DataView inb = (DataView)ds.Select(DataSourceSelectArguments.Empty);
                    if (inb.Count > 0)
                    {
                        txtStep.Text = inb[0][2].ToString();
                        txtWorkCenter.Text = inb[0][3].ToString();
                        txtLaborCost.Text = inb[0][4].ToString();
                    }
                    
                    txtReceivingWorkCenter.Text = "";
                    glReceivingStep.Text = "";
                    
                    Session["a"] = a;
                    Session["b"] = b;
                    sdsReceiving.SelectCommand = "select B.DocNumber AS DISDocNumber, Step, WorkCenter FROM Production.DISStep A INNER JOIN Production.DIS B ON A.DocNumber = B.DocNumber WHERE ISNULL(A.DateOUT,'') = '' AND Status IN ('N','W') AND ISNULL(SubmittedBy,'') != '' and A.DocNumber = '" + a + "' AND A.Step != '" + b + "'";
                    sdsReceiving.DataBind();

                    txtReceivingWorkCenter.ClientEnabled = true;
                    glReceivingStep.ClientEnabled = true;
                    break;

                case "Final":
                    if (cbxFinalDIS.Checked == true )
                    {
                        glReceivingStep.ClientEnabled = false;
                        txtReceivingWorkCenter.ClientEnabled = false;
                        glReceivingStep.Text = "";
                        txtReceivingWorkCenter.Text = "";
                    }
                    else
                    {
                        if (glDIS.Text == "")
                        {
                            glReceivingStep.ClientEnabled = false;
                            txtReceivingWorkCenter.ClientEnabled = false;
                            glReceivingStep.Text = "";
                            txtReceivingWorkCenter.Text = "";
                        }
                        else
                        { 
                            glReceivingStep.ClientEnabled = true;
                            txtReceivingWorkCenter.ClientEnabled = true;
                            glReceivingStep.Text = "";
                            txtReceivingWorkCenter.Text = "";
                        }
                    }
                    break;
            }
        }

        private DataTable GetSelectedVal()
        {
            Session["Datatable"] = "0";
            gv1.DataSourceID = null;
            gv1.DataBind();

            DataTable dt = new DataTable();
            Session["referencedetail"] = sdsSizeBreakdown.FilterExpression;
            sdsSizeBreakdown.SelectCommand = "Select RIGHT('00000'+ CAST(ROW_NUMBER() OVER (ORDER BY DocNumber) AS VARCHAR(5)),5) AS LineNumber, Size AS SizeCode, ISNULL(Qty,0.00) AS Qty, Field1, Field2, Field3, Field4, Field5, Field6, Field7, Field8, Field9 from Production.DISSizeBreakdown "
                                           + "WHERE DocNumber = '" + glDIS.Text + "'";

            gv1.DataSourceID = "sdsSizeBreakdown";
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

            return dt;
        }

        protected void gv1_BatchUpdate(object sender, ASPxDataBatchUpdateEventArgs e)
        {
            if ((error == true || error == false) && check == false)//Prevents updating of grid to enable validation
            {
                e.Handled = true;
                //e.InsertValues.Clear();
                e.UpdateValues.Clear();
            }

            if (Session["Datatable"] == "1" && check == true)
            {
                e.Handled = true;
                DataTable source = GetSelectedVal();

                foreach (ASPxDataDeleteValues values in e.DeleteValues)
                {
                    try
                    {
                        object[] keys = { values.Keys[0] };
                        source.Rows.Remove(source.Rows.Find(keys));
                    }
                    catch (Exception)
                    {
                        continue;
                    }
                }

                foreach (ASPxDataUpdateValues values in e.UpdateValues)
                {
                    object[] keys = { values.NewValues["LineNumber"] };
                    DataRow row = source.Rows.Find(keys);
                    row["SizeCode"] = values.NewValues["SizeCode"];
                    row["Qty"] = values.NewValues["Qty"];
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

                foreach (DataRow dtRow in source.Rows)//This is where the data will be inserted into db
                {
                    _EntityDetail.SizeCode = dtRow["SizeCode"].ToString();
                    _EntityDetail.Qty = Convert.ToDecimal(dtRow["Qty"].ToString());
                    _EntityDetail.Field1 = dtRow["Field1"].ToString();
                    _EntityDetail.Field2 = dtRow["Field2"].ToString();
                    _EntityDetail.Field3 = dtRow["Field3"].ToString();
                    _EntityDetail.Field4 = dtRow["Field4"].ToString();
                    _EntityDetail.Field5 = dtRow["Field5"].ToString();
                    _EntityDetail.Field6 = dtRow["Field6"].ToString();
                    _EntityDetail.Field7 = dtRow["Field7"].ToString();
                    _EntityDetail.Field8 = dtRow["Field8"].ToString();
                    _EntityDetail.Field9 = dtRow["Field9"].ToString();
                    _EntityDetail.AddDISoutDetail(_EntityDetail);
                }
            }
        }

        public void ForViewAndDeleteLookup()
        {
            sdsDIS.SelectCommand = "select B.DocNumber AS DISDocNumber, DISNumber, Step, WorkCenter FROM Production.DISStep A INNER JOIN Production.DIS B ON A.DocNumber = B.DocNumber WHERE ISNULL(SubmittedBy,'') != '' AND B.DocNumber = '" + Session["DISvalue"].ToString() + "'";
            glDIS.DataSourceID = "sdsDIS";
            glDIS.DataBind();
            glDIS.Text = Session["DISvalue"].ToString();
        }

        protected void AddError(Dictionary<GridViewColumn, string> errors, GridViewColumn column, string errorText)
        {
            if (errors.ContainsKey(column)) return;
            errors[column] = errorText;
        }

        protected void Connection_Init(object sender, EventArgs e)
        {
            ((SqlDataSource)sender).ConnectionString = Session["ConnString"].ToString();
        }
        #endregion

        protected void glReceivingStep_Init(object sender, EventArgs e)
        {
            if (Session["a"] != null && Session["b"] != null)
            {
                sdsReceiving.SelectCommand = "select B.DocNumber AS DISDocNumber, Step, WorkCenter FROM Production.DISStep A INNER JOIN Production.DIS B ON A.DocNumber = B.DocNumber WHERE ISNULL(A.DateOUT,'') = '' AND Status IN ('N','W') AND ISNULL(SubmittedBy,'') != '' and A.DocNumber = '" + Session["a"].ToString() + "' AND A.Step != '" + Session["b"].ToString() + "'";
                sdsReceiving.DataBind();
            }
        }
    }
}