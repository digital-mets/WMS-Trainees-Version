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
using GearsCommon;
namespace GWL
{
    public partial class frmRawMaterialsRequest : System.Web.UI.Page
    {
        Boolean error = false;
        Boolean view = false;
        Boolean check = false;
        private static string strError;

        Entity.RawMaterialsRequest _Entity = new RawMaterialsRequest();
        Entity.RawMaterialsRequest.RMRDetail _EntityDetail = new RawMaterialsRequest.RMRDetail();
        string access1 = "";
        string strresult = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            //Session["ConnString"] = "Data Source=192.168.180.21;Initial Catalog=GEARS-CMMS;Persist Security Info=True;User ID=sa;Password=mets123*;";
            Gears.UseConnectionString(Session["ConnString"].ToString());
            /*
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
                Session["BizPartnerCode"] = null;

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
            }

            txtHRMRNumber.Value = Request.QueryString["docnumber"].ToString(); //sets docnumber from session
            _Entity.getdata(txtHRMRNumber.Text, Session["ConnString"].ToString());

            dtpDocDate.Text = Convert.ToDateTime(_Entity.DocDate.ToString()).ToShortDateString();

            txtHRMRNumber.Text = _Entity.RMRNumber.ToString();
            txtHRMRRDetails.Text = _Entity.RMRRDetails.ToString();
            txtHRequestingDeptCompany.Text = _Entity.RequestingDeptCompany.ToString();
            glHWarehouseCode.Text = _Entity.WarehouseCode.ToString();
            glHCustomerCode.Text = _Entity.CustomerCode.ToString();
            txtHConsignee.Text = _Entity.Consignee.ToString();
            txtHConsigneeAddress.Text = _Entity.ConsigneeAddress.ToString();
            txtHRequiredLoadingTime.Text = _Entity.RequiredLoadingTime.ToString();
            txtHShipmentType.Text = _Entity.ShipmentType.ToString();

            txtHAddedBy.Text = _Entity.AddedBy.ToString();
            txtHAddedDate.Text = Convert.ToDateTime(_Entity.AddedDate.ToString()).ToShortDateString();
            txtHLastEditedBy.Text = _Entity.LastEditedBy.ToString();
            txtHLastEditedDate.Text = Convert.ToDateTime(_Entity.LastEditedDate.ToString()).ToShortDateString();

            DataTable checkCount = _EntityDetail.getdetail(txtHRMRNumber.Text, Session["ConnString"].ToString());
            if (checkCount.Rows.Count > 0)
            {
                gridRMRDetail.KeyFieldName = "RMRNumber;BatchNumber";
                gridRMRDetail.DataSourceID = "odsDetail";
            }
            else
            {
                gridRMRDetail.KeyFieldName = "RMRNumber;BatchNumber";
                gridRMRDetail.DataSourceID = "sdsDetail";
            }*/
        }

        protected void Connection_Init(object sender, EventArgs e)
        {
            //Session["ConnString"] = "Data Source=192.168.180.21;Initial Catalog=GEARS-CMMS;Persist Security Info=True;User ID=sa;Password=mets123*;";
            ((SqlDataSource)sender).ConnectionString = Session["ConnString"].ToString();

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

        protected void LookupLoad(object sender, EventArgs e)//Control for all lookup in header
        {
            ASPxGridLookup look = sender as ASPxGridLookup;
            look.ReadOnly = view;
        }

        protected void dtpDocDate_Init(object sender, EventArgs e)
        {
            if (Request.QueryString["entry"] == "N")
            {
                dtpDocDate.Date = DateTime.Now;
              
            }
        }

        protected void Date_Load(object sender, EventArgs e)//Control for all date editor
        {
            ASPxDateEdit date = sender as ASPxDateEdit;
            date.ReadOnly = view;
        }

        protected void TextboxLoad(object sender, EventArgs e)//Control for all textbox
        {
            ASPxTextBox text = sender as ASPxTextBox;
            text.ReadOnly = view;
        }

        protected void cp_Callback(object sender, CallbackEventArgsBase e)
        {
            /*
            _Entity.Connection = Session["ConnString"].ToString();
            _Entity.DocDate = dtpDocDate.Text;
            _Entity.RMRNumber = txtHRMRNumber.Text;
            _Entity.RequestingDeptCompany = txtHRequestingDeptCompany.Text;
            _Entity.WarehouseCode = glHWarehouseCode.Text;
            _Entity.CustomerCode = glHCustomerCode.Text;
            _Entity.Consignee = txtHConsignee.Text;
            _Entity.ConsigneeAddress = txtHConsigneeAddress.Text;
            _Entity.RequiredLoadingTime = txtHRequiredLoadingTime.Text;
            _Entity.ShipmentType = txtHShipmentType.Text;
            _Entity.AddedBy = Session["userid"].ToString();

            
            string param = e.Parameter.Split('|')[0]; //Renats
            switch (param) //Renats
            {

                case "Add":
      
                    if (error == false)
                    {
                        check = true;

                        _Entity.UpdateData(_Entity);
                        gridRMRDetail.DataSourceID = "odsDetail";

                        odsDetail.SelectParameters["RMRNumber"].DefaultValue = txtHRMRNumber.Text;
                        gridRMRDetail.UpdateEdit();
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
                        strError = Functions.Submitted(_Entity.RMRNumber, "Production.RawMaterialsRequest", 1, Session["ConnString"].ToString());//NEWADD factor 1 if submit, 2 if approve
                        if (!string.IsNullOrEmpty(strError))
                        {
                            cp.JSProperties["cp_message"] = strError;
                            cp.JSProperties["cp_success"] = true;
                            cp.JSProperties["cp_forceclose"] = true;
                            return;
                        }
                        _Entity.UpdateData(_Entity);
                        gridRMRDetail.DataSourceID = "odsDetail";

                        odsDetail.SelectParameters["DocNumber"].DefaultValue = txtHRMRNumber.Text;
                        gridRMRDetail.UpdateEdit();
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


            } */
            }

        private void Validate()
        {
            GearsLibrary.Gears.GearsParameter gparam = new Gears.GearsParameter();
            gparam._DocNo = _Entity.RMRNumber;
            gparam._TransType = "PRDRMR";
            gparam._Connection = Session["ConnString"].ToString();
            strresult += GearsProduction.GProduction.ChargeReceipt_Validate(gparam);

            if (strresult != " ")
            {
                cp.JSProperties["cp_valmsg"] = strresult + "\r\n";//Message variable to client side
            }
        }
        
        private void Post()
        {
            GearsLibrary.Gears.GearsParameter gparam = new Gears.GearsParameter();
            gparam._DocNo = _Entity.RMRNumber;
            gparam._UserId = Session["Userid"].ToString();
            gparam._TransType = "PRDRMR";
            gparam._Table = "Production.RawMaterialsRequest";
            gparam._Factor = -1;
            gparam._Connection = Session["ConnString"].ToString();
            strresult += GearsProduction.GProduction.ProdChargeReceipt_Post(gparam);
            if (strresult != " ")
            {
                cp.JSProperties["cp_valmsg"] = strresult;//Message variable to client side
            }
        }



    }
}