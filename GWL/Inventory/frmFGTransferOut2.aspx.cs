using DevExpress.Web;
using DevExpress.Web.Data;
using System;
using System.Web.UI.WebControls;
using GearsLibrary;
using Entity;
using System.Data;

namespace GWL.Inventory
{
    public partial class frmFGTransferOut2 : System.Web.UI.Page
    {
        Boolean error = false;//Boolean for grid validation
        Boolean view = false;//Boolean for view state
        Boolean check = false;//Boolean for grid validation
        string a = ""; //Renats
        string b = ""; //Renats
        string c = ""; //Renats
        private static string strError;

        Entity.FGTransferOut _Entity = new FGTransferOut();//Calls entity KPIRef
        Entity.FGTransferOut.FGTransferOutDetail _EntityDetail = new FGTransferOut.FGTransferOutDetail();

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

                txtDocNumber.Value = Request.QueryString["docnumber"].ToString(); //sets docnumber from session

                _Entity.getdata(txtDocNumber.Text, Session["ConnString"].ToString()); //Method for retrieving data from entity

                dtpDocDate.Text = Convert.ToDateTime(_Entity.DocDate.ToString()).ToShortDateString();
                glRefMONum.Value = _Entity.ReferenceMONumber.ToString();
                txtPicklistNumber.Text = _Entity.PicklistNumber.ToString();
                glCustomer.Value = _Entity.CustomerCode.ToString();
                glWarehouse.Value = _Entity.WarehouseCode.ToString();
                memRemarks.Text = _Entity.Remarks.ToString();
                txtHAddedBy.Text = _Entity.AddedBy;
                txtHAddedDate.Text = _Entity.AddedDate;
                txtHLastEditedBy.Text = _Entity.LastEditedBy;
                txtHLastEditedDate.Text = _Entity.LastEditedDate;
                DataTable checkCount = _EntityDetail.getdetail(txtDocNumber.Text, Session["ConnString"].ToString());
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
            }
        }
        #endregion

        #region Set controls' state/behavior/etc...
        protected void Generate_Btn(object sender, EventArgs e)//Control for all textbox
        {
            ASPxButton btn = sender as ASPxButton;
            Generatebtn.ClientVisible = !view;
        }

        private DataTable GetSelectedVal()
        {
            //2020-10-30 Convert approach to odsdetail and sdsdetail
            gv2.DataSourceID = null;

            DataTable dt = new DataTable();
            DataTable getDetail = new DataTable();

            string selectedValues = glRefMONum.Text.Replace(';', ',');

            getDetail = Gears.RetriveData2("SELECT DocNumber,SKUCode,PackagingType, Orders,LineNumber,FullDesc as ItemDescription FROM    " +
                                    " (select A.DocNumber,A.SKUCode,A.PackagingType,C.ProductName AS FullDesc,LineNumber,Day1,Day2,Day3,Day4,Day5,Day6 " +
                                    " ,Day7, B.Workweek,B.Year, B.SubmittedBy " +
                                    " from Production.MaterialOrderDetail A  " +
                                    " left join Production.MaterialOrder B " +
                                    " on A.DocNumber = B.DocNumber " +
                                    " LEFT JOIN Masterfile.FGSKU C " +
                                    " ON A.SKUCode = C.SKUCode " +
                                    " where B.SubmittedBy is not null " +
                                    " AND CHARINDEX(A.DocNumber, '" + selectedValues + "') > 0  ) p  " +
                                    " UNPIVOT  " +
                                    " (Orders FOR DayOrder IN   " +
                                    " (Day1,Day2,Day3,Day4,Day5,Day6 " +
                                    " ,Day7)   " +
                                    " )AS unpvt " +
                                    " WHERE dateadd (week, Workweek, dateadd (year, Year-1900, 0)) - 4 -datepart(dw, dateadd (week, Workweek, dateadd (year, Year-1900, 0)) - 4)+ Convert(int,REPLACE(DayOrder,'Day','') +1) ='" + dtpDocDate.Text + "'  ", Session["ConnString"].ToString());

            gv2.DataSource = getDetail;
            gv2.DataBind();

            gv2.KeyFieldName = "DocNumber;LineNumber";
            //END 2020-10-30 Convert approach to odsdetail and sdsdetail

            return dt;
        }

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

        protected void MemoLoad(object sender, EventArgs e)
        {
            var memo = sender as ASPxMemo;
            memo.ReadOnly = view;
        }

        protected void CheckBoxLoad(object sender, EventArgs e)
        {
            var check = sender as ASPxCheckBox;
            check.ReadOnly = view;
        }

        protected void gv1_BatchUpdate(object sender, ASPxDataBatchUpdateEventArgs e)
        {

        }

        protected void gv1_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)//Control for grid
        {
            if (Request.QueryString["entry"].ToString() == "V" || Request.QueryString["entry"].ToString() == "D")
            {
                ASPxGridView grid = sender as ASPxGridView;
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
        #endregion

        #region Post
        #endregion

        #region Callback functions(Inserting/Updating/Deleting/Validating)
        protected void cp_Callback(object sender, CallbackEventArgsBase e)
        {
            _Entity.Connection = Session["ConnString"].ToString();
            _Entity.DocNumber = txtDocNumber.Text;
            _Entity.DocDate = dtpDocDate.Text;
            _Entity.ReferenceMONumber = glRefMONum.Text;
            _Entity.PicklistNumber = txtPicklistNumber.Text;
            _Entity.CustomerCode = glCustomer.Text;
            _Entity.WarehouseCode = glWarehouse.Text;
            _Entity.Remarks = memRemarks.Text;
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

                        odsDetail.SelectParameters["DocNumber"].DefaultValue = txtDocNumber.Text;
                        gv1.UpdateEdit();
                        //Validate(); Post();

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
                        strError = Functions.Submitted(_Entity.DocNumber, "Production.FGTransferOut", 1, Session["ConnString"].ToString());//NEWADD factor 1 if submit, 2 if approve
                        if (!string.IsNullOrEmpty(strError))
                        {
                            cp.JSProperties["cp_message"] = strError;
                            cp.JSProperties["cp_success"] = true;
                            cp.JSProperties["cp_forceclose"] = true;
                            return;
                        }
                        _Entity.UpdateData(_Entity);
                        gv1.DataSourceID = "odsDetail";

                        odsDetail.SelectParameters["DocNumber"].DefaultValue = txtDocNumber.Text;
                        gv1.UpdateEdit();

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
                case "Generate":
                    GetSelectedVal();
                    cp.JSProperties["cp_generated"] = true;
                    break;
            }
        }

        protected void Connection_Init(object sender, EventArgs e)
        {
            ((SqlDataSource)sender).ConnectionString = Session["ConnString"].ToString();
        }

        protected void glItemCode_Init(object sender, EventArgs e)
        {

        }

        protected void dtpDocDate_Init(object sender, EventArgs e)
        {
            if (Request.QueryString["entry"] == "N")
            {
                dtpDocDate.Date = DateTime.Now;
            }
        }


        #endregion
    }
}