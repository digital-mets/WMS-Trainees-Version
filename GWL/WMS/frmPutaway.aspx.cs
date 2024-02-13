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


namespace GWL
{
    public partial class frmPutaway : System.Web.UI.Page
    {
        Boolean error = false;//Boolean for grid validation
        Boolean view = false;//Boolean for view state
        Boolean check = false;//Boolean for grid validation

        Entity.Putaway _Entity = new Putaway();//Calls entity odsHeader

        #region page load/entry
        protected void Page_Load(object sender, EventArgs e)
        {
            Gears.UseConnectionString(Session["ConnString"].ToString());

            //locationsql.SelectCommand = "Select LocationCode,WarehouseCode,RoomCode from masterfile.location A INNER JOIN WMS.Inbound B ON A.WarehouseCode = B.WarehouseCode AND A.PlantCode = B.PlantCode AND A.RoomCode = '" + glRoom.Text + "'  AND B.Docnumber = '" + Request.QueryString["docnumber"].ToString() + "' " +
            //                            "  WHERE B.Docnumber = '" + Request.QueryString["docnumber"].ToString() + "' ";

            roomsql.SelectCommand = "select A.RoomCode,A.StorageType,PlantCode from masterfile.Room  A INNER JOIN WMS.Inbound B ON A.WarehouseCode = B.WarehouseCode AND A.PlantCode =B.Plant AND B.Docnumber = '" + Request.QueryString["docnumber"].ToString() + "' ";
            string referer;
            try
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

            gv1.KeyFieldName = "DocNumber;LineNumber";

            //roomsql.SelectCommand = "select distinct b.RoomCode, A.StorageType,b.PlantCode " +
            //                        "from masterfile.Item a inner join masterfile.Room b " +
            //                        "on a.StorageType = b.StorageType " +
            //                        "inner join (select PlantCode,DispatchLocationCode,b.CustomerCode " +
            //                        "from  Masterfile.Plant a " +
            //                        "inner join wms.Inbound  b " +
            //                        "on a.PlantCode = b.plant " +
            //                        "where B.DocNumber = '" + Request.QueryString["docnumber"].ToString() + "') c " +
            //                        "on b.PlantCode = c.PlantCode";
            glRoom.DataBind();

            //Details checking. changes datasourceid of gridview if there's no details for a certain docnumber; to enable adding of details.
            if (Request.QueryString["entry"].ToString() == "V")
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
                        txtDocNumber.ReadOnly = true;
                        break;
                    case "V":
                        view = true;//sets view mode for entry
                        Generatebtn.Visible = false;
                        updateBtn.Text = "Close";
                        glcheck.ClientVisible = false;
                        break;
                    case "D":
                        updateBtn.Text = "Delete";
                        break;
                }

                txtDocNumber.Value = Request.QueryString["docnumber"].ToString(); //sets docnumber from session
                _Entity.getdata(txtDocNumber.Text, Session["ConnString"].ToString());
                gvStrategy.Value = _Entity.PutAwayStrategy;
                glRoom.Text = _Entity.Room;

                //if (Session["strategy"] != null)
                //{
                //    if (_Entity.PutAwayStrategy.ToString()!="")
                //    {
                //        gvStrategy.Text = _Entity.PutAwayStrategy.ToString();
                //    }
                //    else
                //    {
                //        gvStrategy.Text = Session["strategy"].ToString();
                //    }
                //}

                    gv1.DataSourceID = "odsDetail";
                    _Entity.getdata(txtDocNumber.Text, Session["ConnString"].ToString()); //Method for retrieving data from entity    
                    popup.ShowOnPageLoad = true;

            }

            glcheck.Checked = true;
        }
        #endregion

        #region Validation
        private void Validate()
        {
            GearsLibrary.Gears.GearsParameter gparam = new Gears.GearsParameter();
            gparam._DocNo = _Entity.DocNumber;
            gparam._UserId = Session["Userid"].ToString();
            gparam._TransType = "WMSPUT";

            string strresult = GWarehouseManagement.PutAway_Validate(gparam);

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
            //Control look = (Control)sender;
            //((ASPxGridLookup)look).ReadOnly = view;
            var look = sender as ASPxGridLookup;
            if (look != null)
            {
                look.ReadOnly = view;
            }
        }
        protected void gvLookupLoad(object sender, EventArgs e)//Control for all lookup in details/grid
        {
                ASPxGridLookup look = sender as ASPxGridLookup;
                look.Enabled = false;
        }
        protected void gv1_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)//Control for grid
        {

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
            if (e.ButtonType == ColumnCommandButtonType.Edit || e.ButtonType == ColumnCommandButtonType.Delete ||
                    e.ButtonType == ColumnCommandButtonType.New || e.ButtonType == ColumnCommandButtonType.Cancel ||
                    e.ButtonType == ColumnCommandButtonType.Update)
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
                if (e.ButtonID == "CountSheet")
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
            Gears.UseConnectionString(Session["ConnString"].ToString());

            _Entity.DocNumber = txtDocNumber.Text;
            _Entity.Connection = Session["ConnString"].ToString();
            _Entity.LastEditedBy = Session["userid"].ToString();
            _Entity.PutAwayStrategy = gvStrategy.Text;
            _Entity.Room = glRoom.Text;
            //Session["strategy"] = gvStrategy.Text;
            string strError;
            switch (e.Parameter)
            {
                case "Add":

                    gv1.UpdateEdit();//Initiate to call gridvalidation/batchupdate method

                        strError = Functions.Submitted(_Entity.DocNumber,"WMS.Inbound",2,Session["ConnString"].ToString());//NEWADD factor 1 if submit, 2 if approve
                        
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
                        _Entity.UpdateData(_Entity);//Method of inserting for header
                        gv1.DataSourceID = "odsDetail";//Renew datasourceID to entity
                        odsDetail.SelectParameters["DocNumber"].DefaultValue = txtDocNumber.Text;//Set select parameter to prevent error
                        gv1.UpdateEdit();//2nd initiation to insert grid
                        //Validate();
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

                        strError = Functions.Submitted(_Entity.DocNumber,"WMS.Inbound",2,Session["ConnString"].ToString());//NEWADD factor 1 if submit, 2 if approve
                        
                        if (!string.IsNullOrEmpty(strError))
                        {
                            cp.JSProperties["cp_message"] = strError;
                            cp.JSProperties["cp_success"] = true;
                            cp.JSProperties["cp_forceclose"] = true;
                            return;
                        }

                    //gv1.UpdateEdit();

                    //if (error == false)
                    //{
                        check = true;
                        _Entity.UpdateData(_Entity);//Method of inserting for header
                        gv1.DataSourceID = "odsDetail";//Renew datasourceID to entity
                        odsDetail.SelectParameters["DocNumber"].DefaultValue = txtDocNumber.Text;//Set select parameter to prevent error
                        gv1.UpdateEdit();//2nd initiation to insert grid
                        Validate();
                        cp.JSProperties["cp_message"] = "Successfully Updated!";
                        cp.JSProperties["cp_success"] = true;
                        cp.JSProperties["cp_close"] = true;
                    //}
                    //else
                    //{
                    //    cp.JSProperties["cp_message"] = "Please check all the fields!";
                    //    cp.JSProperties["cp_success"] = true;
                    //}
                break;

                case "Delete":
                       strError = Functions.Submitted(_Entity.DocNumber,"WMS.Inbound",3,Session["ConnString"].ToString());//NEWADD factor 1 if submit, 2 if approve
                        
                        if (!string.IsNullOrEmpty(strError))
                        {
                            cp.JSProperties["cp_message"] = strError;
                            cp.JSProperties["cp_success"] = true;
                            cp.JSProperties["cp_forceclose"] = true;
                            return;
                        }

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

                case "generate":
                _Entity.UpdateData(_Entity);
                string result = "";
                DataTable generate = Gears.RetriveData2("exec [sp_GeneratePutaway] '"+ Request.QueryString["docnumber"].ToString()
                                     + "','" + gvStrategy.Text + "','" + glRoom.Text + "'", Session["ConnString"].ToString());
                foreach (DataRow dt in generate.Rows)
                {
                    result += dt[0].ToString()+"\r\n";
                }
                if (!string.IsNullOrEmpty(result))
                {
                    cp.JSProperties["cp_result"] = result;
                }
                else
                {
                    cp.JSProperties["cp_result"] = "Successfully Generated!";
                }
                gv1.DataBind();
                break;

                case "refgrid":
                gv1.DataBind();
                break;

                
                    
            }
        }
        private GridViewDataColumn GetColumnByFieldName(string fieldName)//Get all column name function
        {
            IEnumerable<GridViewDataColumn> dataColumns = gv1.Columns.OfType<GridViewDataColumn>().Where(c => c.FieldName == fieldName);
            if (dataColumns.Count() > 0)
                return dataColumns.First();
            return null;
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

        protected void gv1_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
        }

        protected void gv1_Init(object sender, EventArgs e)
        {
            string plant = "";
            DataTable checkplant = Gears.RetriveData2("select plant from wms.inbound where docnumber = '" + Request.QueryString["docnumber"].ToString() + "'", Session["ConnString"].ToString());
            foreach (DataRow dt in checkplant.Rows)
            {
                plant = dt["plant"].ToString();
            }
            //locationsql.SelectCommand = "Select LocationCode,WarehouseCode,RoomCode from masterfile.location where PlantCode = '" + plant + "'";
        }

        protected void Connection_Init(object sender, EventArgs e)
        {

            ((SqlDataSource)sender).ConnectionString = Session["ConnString"].ToString();
            //sdsDetail.ConnectionString = Session["ConnString"].ToString();
            //sdsItem.ConnectionString = Session["ConnString"].ToString();
            //sdsItemDetail.ConnectionString = Session["ConnString"].ToString();
            //sdsBizPartner.ConnectionString = Session["ConnString"].ToString();
            //sdsWarehouse.ConnectionString = Session["ConnString"].ToString();
            //Masterfileitem.ConnectionString = Session["ConnString"].ToString();
            //Masterfileitemdetail.ConnectionString = Session["ConnString"].ToString();
            //Warehouse.ConnectionString = Session["ConnString"].ToString();
            //ICNNumber.ConnectionString = Session["ConnString"].ToString();
            //TranType.ConnectionString = Session["ConnString"].ToString();
            //Masterfilebiz.ConnectionString = Session["ConnString"].ToString();
            //Masterfilebizcustomer.ConnectionString = Session["ConnString"].ToString();
            //PutAwayStrategy.ConnectionString = Session["ConnString"].ToString();
            //locationsql.ConnectionString = Session["ConnString"].ToString();
            //roomsql.ConnectionString = Session["ConnString"].ToString();
        }
    }
}