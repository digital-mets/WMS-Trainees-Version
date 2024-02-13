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
    public partial class frmTransactionNonStorage : System.Web.UI.Page
    {
        Boolean error = false;//Boolean for grid validation
        Boolean view = false;//Boolean for view state
        Boolean check = false;//Boolean for grid validation

        private static string Connection;

        Entity.TransactionNonStorage _Entity = new TransactionNonStorage();//Calls entity 

        #region page load/entry


        private void ChangeFieldName()
        {
            Gears.UseConnectionString(Session["ConnString"].ToString());
            string sql = "Select Field1,Field2,Field3,Field4,Field5,Field6,Field7,Field8,Field9,VariableRate,ServiceRate from Masterfile.WMSServiceType where ServiceType = '" + glServiceType.Text + "' ";
            DataTable dtWMSServiceType = Gears.RetriveData2(sql, Session["ConnString"].ToString());

            if (dtWMSServiceType.Rows.Count > 0)
            {
                for (int i = 1; i <= 9; i++)
                {
                    var formLayout = frmlayout1 as ASPxFormLayout;
                    var layoutItem = formLayout.FindItemOrGroupByName("Field" + i.ToString());
                    string r1 = dtWMSServiceType.Rows[0][i - 1].ToString();

                    layoutItem.Caption = r1;

                    layoutItem.Visible = String.IsNullOrEmpty(r1) ? false : true;
                }

                if (Convert.ToBoolean(dtWMSServiceType.Rows[0]["VariableRate"]) == false)
                {
                    txtHField5.ReadOnly = true;

                    // 2/2/2024 JCB Updated Code to Fetch Rate from SP

                    string ServRate = "";

                    DataTable dtRate = Gears.RetriveData2("EXEC [sp_generate_NonStorageRate] '" + glBizpartnerCode.Text + "','" + glServiceType.Text + "','" + deDocDate.Text + "' ,'" + gvWarehouse.Text + "'", Session["ConnString"].ToString());

                    foreach (DataRow dtrow in dtRate.Rows)
                    {
                        ServRate = dtrow["ServiceRate"].ToString();
                    }

                    txtHField5.Value = ServRate;
                    //txtHField5.Value = dtWMSServiceType.Rows[0]["ServiceRate"].ToString();
                }
                else
                {
                    txtHField5.Value = 0;
                    txtHField5.ReadOnly = false;
                }

            }

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Gears.UseConnectionString(Session["ConnString"].ToString());
            //Random rand = new Random();
            //GearsLibrary.Gears.GearsParameter gp = new Gears.GearsParameter();
            //string Meth = "GearsWarehouseManagement.GWarehouseManagement.WMSTransactionNonStorage._Submit(gp)"
            //System.Reflection.MethodInfo info = rand.GetType().GetMethod(Meth);
            //string sstrmessage = (string)info.Invoke(rand,null);

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


            deDocDate.MinDate = Convert.ToDateTime(GearsCommon.GCommon.SystemSetting("BookDate", Session["ConnString"].ToString()).ToString());



            //Details checking. changes datasourceid of gridview if there's no details for a certain docnumber; to enable adding of details.
            if (Request.QueryString["entry"].ToString() == "V")
            {
                view = true;//sets view mode for entry
            }
            Session["ChangeServiceType"] = "";
            if (!IsPostBack)
            {
                Connection = (Session["ConnString"].ToString());
                switch (Request.QueryString["entry"].ToString())
                {
                    case "N":
                        updateBtn.Text = "Add";
                        glcheck.ClientVisible = false;
                        break;
                    case "E":
                        updateBtn.Text = "Update";
                        glAddNew.ClientVisible = false;
                        break;
                    case "V":
                        view = true;//sets view mode for entry
                        updateBtn.Text = "Close";
                        glcheck.ClientVisible = false;
                        glAddNew.ClientVisible = false;
                        break;
                    case "D":
                        view = true;//sets view mode for entry
                        updateBtn.Text = "Delete";
                        glcheck.ClientVisible = false;
                        glAddNew.ClientVisible = false;
                        break;
                    case "AN":
                        deDocDate.Value = DateTime.Now;
                        updateBtn.Text = "Add";
                        glAddNew.ClientVisible = true;
                        glcheck.ClientVisible = false;
                        break;
                }

                txtDocnumber.ReadOnly = true;
                txtDocnumber.Value = Request.QueryString["docnumber"].ToString();

                _Entity.getdata(txtDocnumber.Text, Session["ConnString"].ToString());//ADD CONN

                deDocDate.Text = String.IsNullOrEmpty(_Entity.DocDate) ? "" : Convert.ToDateTime(_Entity.DocDate).ToShortDateString();
                glServiceType.Value = _Entity.ServiceType;
                txtBillNumber.Text = _Entity.BillNumber;
                glBizpartnerCode.Value = _Entity.BizPartnerCode;
                gvWarehouse.Value = _Entity.WarehouseCode;
                txtHField1.Text = _Entity.Field1;
                txtHField2.Text = _Entity.Field2;
                txtHField3.Text = _Entity.Field3;
                txtHField4.Value = _Entity.Field4;
                txtHField5.Value = _Entity.Field5;
                txtHField6.Value = _Entity.Field6;
                txtHField7.Value = _Entity.Field7;
                txtHField8.Text = _Entity.Field8;
                txtHField9.Text = _Entity.Field9;

                txtAddedBy.Text = _Entity.AddedBy;
                txtAddedDate.Text = _Entity.AddedDate;
                txtLastEditedBy.Text = _Entity.LastEditedBy;
                txtLastEditedDate.Text = _Entity.LastEditedDate;
                txtHSubmittedBy.Text = _Entity.SubmittedBy;
                txtHSubmittedDate.Text = _Entity.SubmittedDate;

                //ServiceType.SelectCommand = "select Distinct ServiceType, Description from  WMS.Contract a inner join WMS.ContractDetail b on a.DocNumber = b.DocNumber where Status != 'Closed' and Type = 'NONSTORAGE' and BizPartnerCode = '" + glBizpartnerCode.Text + "'";
                //glServiceType.Value = _Entity.ServiceType;


            }
            if (!string.IsNullOrEmpty(glServiceType.Text))
            {
                ChangeFieldName();
            }
        }
        #endregion

        #region Validation
        private void Validate()
        {
            GearsLibrary.Gears.GearsParameter gparam = new Gears.GearsParameter();
            gparam._DocNo = _Entity.DocNumber;
            gparam._UserId = Session["Userid"].ToString();
            gparam._TransType = Request.QueryString["transtype"].ToString();
            gparam._Connection = Session["ConnString"].ToString();
            string strresult = GWarehouseManagement.WMSTransactionNonStorage_Validate(gparam);

            cp.JSProperties["cp_valmsg"] = strresult;//Message variable to client side

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

        #region Lookup Settings
        protected void lookup_Init(object sender, EventArgs e)
        {

        }
        public void gridView_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {

        }

        #endregion

        #region Callback functions(Inserting/Updating/Deleting/Validating)
        protected void cp_Callback(object sender, CallbackEventArgsBase e)
        {
            _Entity.Connection = Session["ConnString"].ToString(); //ADD CONN
            _Entity.DocNumber = txtDocnumber.Text;
            _Entity.DocDate = deDocDate.Text;
            _Entity.ServiceType = String.IsNullOrEmpty(glServiceType.Text) ? null : glServiceType.Value.ToString();
            _Entity.BillNumber = txtBillNumber.Text;
            _Entity.BizPartnerCode = String.IsNullOrEmpty(glBizpartnerCode.Text) ? null : glBizpartnerCode.Value.ToString();
            _Entity.WarehouseCode = String.IsNullOrEmpty(gvWarehouse.Text) ? null : gvWarehouse.Value.ToString();
            _Entity.Field1 = txtHField1.Text;
            _Entity.Field2 = txtHField2.Text;
            _Entity.Field3 = txtHField3.Text;
            _Entity.Field4 = String.IsNullOrEmpty(txtHField4.Text) ? 0 : Convert.ToDecimal(txtHField4.Text);
            _Entity.Field5 = String.IsNullOrEmpty(txtHField5.Text) ? 0 : Convert.ToDecimal(txtHField5.Text);
            _Entity.Field6 = String.IsNullOrEmpty(txtHField6.Text) ? 0 : Convert.ToDecimal(txtHField6.Text);
            _Entity.Field7 = String.IsNullOrEmpty(txtHField7.Text) ? 0 : Convert.ToDecimal(txtHField7.Text);
            _Entity.Field8 = txtHField8.Text;
            _Entity.Field9 = txtHField9.Text;

            _Entity.LastEditedBy = Session["userid"].ToString();
            _Entity.LastEditedDate = DateTime.Now.ToString();
            _Entity.AddedBy = Session["userid"].ToString();
            _Entity.AddedDate = DateTime.Now.ToString();


            switch (e.Parameter)
            {
                case "Add":
                    string strError = Functions.Submitted(_Entity.DocNumber, "WMS.TransactionNonStorage", 1, Connection);//NEWADD factor 1 if submit, 2 if approve

                    if (error == false)
                    {
                        check = true;
                        _Entity.UpdateData(_Entity);
                        Validate();
                        cp.JSProperties["cp_message"] = "Successfully Added!";//Message variable to client side
                        cp.JSProperties["cp_addnew"] = false;
                        cp.JSProperties["cp_success"] = true;//Success bool variable  to client side
                        cp.JSProperties["cp_close"] = true;//Close window variable to client side
                    }
                    else
                    {
                        cp.JSProperties["cp_message"] = "Please check all the fields!";
                        cp.JSProperties["cp_success"] = true;
                    }

                    if (glAddNew.Checked)
                    {
                        DataTable a = Gears.RetriveData2("SELECT Prefix, REPLICATE('0', Serieswidth - len(Seriesnumber+1)) " +
                                    " + cast(Seriesnumber+1 as varchar) AS NewDocNumber from it.docnumbersettings where " +
                                    " Module = 'WMSNON'", Session["ConnString"].ToString());
                        foreach (DataRow dtRow in a.Rows)
                        {
                            txtDocnumber.Text = dtRow["Prefix"].ToString() + dtRow["NewDocNumber"].ToString();
                        }
                        DataTable docexist = Gears.RetriveData2("Select DocNumber from WMS.TransactionNonStprage where docnumber = '"
                                           + txtDocnumber.Text + "'", Session["ConnString"].ToString());
                        if (docexist.Rows.Count > 0)
                        {
                            cp.JSProperties["cp_exist"] = true;
                            Gears.RetriveData2("exec sp_AddDocNumber 'WMSNON','TRB','" + txtDocnumber.Text + "'", Session["ConnString"].ToString());
                        }
                        else
                        {
                            cp.JSProperties["cp_exist"] = false;
                            cp.JSProperties["cp_docnumber"] = txtDocnumber.Text;
                            cp.JSProperties["cp_transtype"] = "WMSNON";
                            cp.JSProperties["cp_parameters"] = "";
                            Gears.RetriveData2("exec sp_AddNewTransaction '" + txtDocnumber.Text + "','" + Session["userid"] + "','WMS.TransactionNonStorage','WMSNON'", Session["ConnString"].ToString());
                            Gears.RetriveData2("exec sp_UpdateWorkDate '" + txtDocnumber.Text + "','" + Session["userid"] + "','WMS.TransactionNonStorage','WMSNON','" + Session["WorkDate"].ToString() + "' ", Session["ConnString"].ToString());
                            Gears.RetriveData2("exec sp_UpdateWarehouse '" + txtDocnumber.Text + "','" + Session["userid"] + "','WMS.TransactionNonStorage','WMSNON','" + Session["Warehouse"].ToString() + "' ", Session["ConnString"].ToString());
                            Gears.RetriveData2("exec sp_AddDocNumber 'WMSNON','TRB','" + txtDocnumber.Text + "'", Session["ConnString"].ToString());

                            DataTable b = Gears.RetriveData2("SELECT Prefix,REPLICATE('0', Serieswidth - len(Seriesnumber+1)) " +
                                                    " + cast(Seriesnumber+1 as varchar) AS 'docnum' from it.docnumbersettings where " +
                                                    " module = 'WWMSNON' and IsDefault = 1", Session["ConnString"].ToString());
                            foreach (DataRow dtRow in b.Rows)
                            {
                                txtDocnumber.Value = dtRow["Prefix"].ToString();
                            }
                        }
                    }
                    break;

                case "Update":
                    string strError1 = Functions.Submitted(_Entity.DocNumber, "WMS.TransactionNonStorage", 1, Connection);//NEWADD factor 1 if submit, 2 if approve

                    if (error == false)
                    {
                        check = true;

                        _Entity.UpdateData(_Entity);//Method of Updating header
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
                case "servicetype":
                    Session["ChangeServiceType"] = "1";
                    ChangeFieldName();
                    SqlDataSource ds = Masterfileitem;
                    // 05/08/2017   GC  Changed code
                    //ds.SelectCommand = string.Format("Select top 1 a.ServiceRate from WMS.ContractDetail a inner join WMS.Contract b on a.DocNumber = b.DocNumber where B.BizPartnerCode = '"+glBizpartnerCode.Text+"' AND A.ServiceType = '"+glServiceType.Text+"' and ISNULL(SubmittedBy,'') != '' order by b.docdate desc");
                    //ds.SelectCommand = string.Format("SELECT TOP 1 ISNULL(A.ServiceRate,0) AS ServiceRate FROM WMS.ContractDetail A INNER JOIN WMS.Contract B ON A.DocNumber = B.DocNumber WHERE B.BizPartnerCode = '" + glBizpartnerCode.Text + "' AND A.ServiceType = '" + glServiceType.Text + "' AND B.Status = 'ACTIVE' " +
                    //    " AND (B.EffectivityDate <= '" + deDocDate.Text + "') AND ('" + deDocDate.Text + "' BETWEEN B.DateFrom AND B.DateTo) ORDER BY B.EffectivityDate DESC");
                    ds.SelectCommand = string.Format(" EXEC [sp_generate_NonStorageRate] '" + glBizpartnerCode.Text + "','" + glServiceType.Text + "','" + deDocDate.Text + "','" + gvWarehouse.Text + "'");
                    //END
                    DataView tran = (DataView)ds.Select(DataSourceSelectArguments.Empty);
                    if (tran.Count > 0)
                    {
                        txtHField5.Value = tran[0][4].ToString();
                    }
                    cp.JSProperties["cp_servicetype"] = true;
                    break;
                case "vat":
                    //DataTable getvat = Gears.RetriveData2("select Vatable from wms.Contract A inner join WMS.ContractDetail B "+
                    //                                    "on A.DocNumber = B.DocNumber "+
                    //                                    "where Isnull(SubmittedBy,'')!='' and Status != 'Closed' and BizPartnerCode = '" + glBizpartnerCode.Text + "' and ServiceType='" + glServiceType.Text + "' order by DocDate desc", Session["ConnString"].ToString());
                    DataTable getvat = Gears.RetriveData2(" EXEC [sp_generate_NonStorageRate] '" + glBizpartnerCode.Text + "','" + glServiceType.Text + "','" + deDocDate.Text + "' ,'" + gvWarehouse.Text + "'", Session["ConnString"].ToString());
                    string vatable = "";
                    foreach (DataRow dtrow in getvat.Rows)
                    {
                        //vatable = dtrow[0].ToString();
                        vatable = dtrow[5].ToString();
                    }
                    if (vatable == "True")
                    {
                        DataTable getvatrate = Gears.RetriveData2("select Value from it.SystemSettings where Code = 'vat'", Session["ConnString"].ToString());
                        foreach (DataRow dtrow in getvatrate.Rows)
                        {
                            cp.JSProperties["cp_vatrate"] = dtrow[0].ToString();
                        }
                    }
                    else
                    {
                        cp.JSProperties["cp_vatrate"] = "0";
                    }
                    break;
                case "ST":
                    SqlDataSource pl = ServiceType;
                    pl.SelectCommand = string.Format("select Distinct ServiceType, Description, BizPartnerCode from  WMS.Contract a inner join WMS.ContractDetail b on a.DocNumber = b.DocNumber where Status != 'Closed' and Type = 'NONSTORAGE' and ISNULL(SubmittedBy,'') != '' and BizPartnerCode = '" + glBizpartnerCode.Text + "' AND a.WarehouseCode ='" + gvWarehouse.Text + "'");
                    glServiceType.DataSourceID = "ServiceType";
                    glServiceType.DataBind();
                    //DataView tranpl = (DataView)pl.Select(DataSourceSelectArguments.Empty);
                    //if (tranpl.Count > 0)
                    //{
                    //    glRevSubsi.Text = tranpl[0][1].ToString();
                    //}
                    break;
            }
        }


        protected void grid_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        {

        }
        //dictionary method to hold error 
        void AddError(Dictionary<GridViewColumn, string> errors, GridViewColumn column, string errorText)
        {

        }
        protected void gv1_BatchUpdate(object sender, ASPxDataBatchUpdateEventArgs e)
        {

        }
        #endregion

        protected void glWarehouseCOde_TextChanged(object sender, EventArgs e)
        {

        }

        protected void glOCN_TextChanged(object sender, EventArgs e)
        {

        }

        protected void sup_cp_Callback(object sender, CallbackEventArgsBase e)
        {

        }

        protected void deDocDate_Init(object sender, EventArgs e)
        {
            if (Request.QueryString["entry"] == "N")
            {
                deDocDate.Date = DateTime.Now;
            }
        }
        protected void glServiceType_Init(object sender, EventArgs e)
        {
            ASPxGridLookup gridLookup = sender as ASPxGridLookup;
            gridLookup.GridView.CustomCallback += new ASPxGridViewCustomCallbackEventHandler(glPickList_CustomCallback);
            if (Session["transnon"] != null)
            {
                gridLookup.GridView.DataSourceID = "ServiceType";
                ServiceType.FilterExpression = Session["transnon"].ToString();
            }
        }

        public void glPickList_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {



            string whcode = e.Parameters;//Set column name
            if (whcode.Contains("GLP_AIC") || whcode.Contains("GLP_AC") || whcode.Contains("GLP_F")) return;//Traps the callback

            ASPxGridView grid = sender as ASPxGridView;
            grid.DataSourceID = null;
            CriteriaOperator selectionCriteria = new InOperator("BizPartnerCode", new string[] { whcode });
            ServiceType.FilterExpression = (GroupOperator.Combine(GroupOperatorType.And, selectionCriteria)).ToString();
            Session["transnon"] = ServiceType.FilterExpression;
            grid.DataSourceID = "ServiceType";
            grid.DataBind();
        }
        protected void Connection_Init(object sender, EventArgs e)
        {

            //sdsDetail.ConnectionString = Session["ConnString"].ToString();
            //Masterfileitem.ConnectionString = Session["ConnString"].ToString();
            //Masterfileitemdetail.ConnectionString = Session["ConnString"].ToString();
            //ServiceType.ConnectionString = Session["ConnString"].ToString();
            //Masterfilebiz.ConnectionString = Session["ConnString"].ToString();
            //Masterfilebizcustomer.ConnectionString = Session["ConnString"].ToString();
            //OCN.ConnectionString = Session["ConnString"].ToString();
            //sdsWarehouse.ConnectionString = Session["ConnString"].ToString();
            ((SqlDataSource)sender).ConnectionString = Session["ConnString"].ToString();



        }
    }
}