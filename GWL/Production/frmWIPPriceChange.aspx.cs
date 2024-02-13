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
    public partial class frmWIPPriceChange : System.Web.UI.Page
    {
        Boolean error = false;//Boolean for grid validation
        Boolean view = false;//Boolean for view state
        Boolean check = false;//Boolean for grid validation
        string a = ""; //Renats
        string b = ""; //Renats
        string c = ""; //Renats
        string d = ""; //Renats
        string f = ""; //Renats
        private static string strError;
        Entity.WIPPriceChange _Entity = new WIPPriceChange();//Calls entity KPIRef

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

                glWip.Text = _Entity.WIPNo.ToString();

                txtTransType.Text = _Entity.TransType.ToString();
                txtWorkCenter.Text = _Entity.WorkCenter.ToString();
                txtStep.Text = _Entity.Step.ToString();
                txtJONumber.Text = _Entity.JONumber.ToString();
                speOldWOP.Text = _Entity.OldWorkOrderPrice.ToString();
                speNWOP.Text = _Entity.NewWorkOrderPrice.ToString();
                speQty.Text = _Entity.Qty.ToString();
                txtRemarks.Text = _Entity.Remarks.ToString();
                txtHField1.Text = _Entity.Field1;
                txtHField2.Text = _Entity.Field2;
                txtHField3.Text = _Entity.Field3;
                txtHField4.Text = _Entity.Field4;
                txtHField5.Text = _Entity.Field5;
                txtHField6.Text = _Entity.Field6;
                txtHField7.Text = _Entity.Field7;
                txtHField8.Text = _Entity.Field8;
                txtHField9.Text = _Entity.Field9;
                txtHAddedBy.Text = _Entity.AddedBy;
                txtHAddedDate.Text = _Entity.AddedDate;
                txtHLastEditedBy.Text = _Entity.LastEditedBy;
                txtHLastEditedDate.Text = _Entity.LastEditedDate;

                if (Request.QueryString["entry"].ToString() == "N")
                {
                    frmlayout1.FindItemOrGroupByName("ReferenceTransaction").ClientVisible = false;
                }
                else
                {





                    gvRef.DataSourceID = "odsReference";
                    odsReference.SelectParameters["DocNumber"].DefaultValue = txtDocnumber.Text;
                    this.gvRef.Columns["CommandString"].Width = 0;

                    this.gvRef.Columns["RCommandString"].Width = 0;

                }

                DataTable dtrr1 = Gears.RetriveData2("Select DocNumber from Production.WIPPriceChange where docnumber = '" + txtDocnumber.Text + "' and ISNULL(WIPNo,'')!='' ", Session["ConnString"].ToString());
                if (dtrr1.Rows.Count > 0 && Request.QueryString["entry"].ToString() == "N")
                {
                    updateBtn.Text = "Update";
                }

                //Details checking. changes datasourceid of gridview if there's no details for a certain docnumber; to enable adding of details.
                gvJournal.DataSourceID = "odsJournalEntry";


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
            if (Request.QueryString["entry"].ToString() == "V")
            {
                ASPxGridLookup look = sender as ASPxGridLookup;
                look.Enabled = false;
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
                e.Image.IconID = "tasks_newtask_16x16";
            }
            //if (e.ButtonType == ColumnCommandButtonType.New)
            //{
            //    e.Image.IconID = "tasks_newtask_16x16";
            //}
            if (view == true)
            {
                if (e.ButtonType == ColumnCommandButtonType.Edit || e.ButtonType == ColumnCommandButtonType.Delete ||
                    e.ButtonType == ColumnCommandButtonType.New || e.ButtonType == ColumnCommandButtonType.Cancel)
                    e.Visible = false;
            }
            if (e.ButtonType == ColumnCommandButtonType.Update && e.ButtonType == ColumnCommandButtonType.New)
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

        #region Validation
        string strresult = "";
        private void Validate()
        {
            GearsLibrary.Gears.GearsParameter gparam = new Gears.GearsParameter();
            gparam._DocNo = _Entity.DocNumber;
            gparam._TransType = "PRDWPC";
            gparam._Connection = Session["ConnString"].ToString();
            strresult += GearsProduction.GProduction.WIPChange_Validate(gparam);
          
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
            gparam._TransType = "PRDWPC";
            gparam._Table = "Production.WIPPriceChange";
            gparam._Factor = -1;
            gparam._Connection = Session["ConnString"].ToString();
            strresult += GearsProduction.GProduction.ProdPriceChange_Post(gparam);
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
            _Entity.WIPNo = glWip.Text;
            _Entity.Step = txtStep.Text;
            _Entity.WorkCenter = txtWorkCenter.Text;
            _Entity.JONumber = txtJONumber.Text;
            _Entity.Qty = Convert.ToDecimal(Convert.IsDBNull(speQty.Value) ? "0" : speQty.Value);
            _Entity.OldWorkOrderPrice = Convert.ToDecimal(Convert.IsDBNull(speOldWOP.Value) ? "0" : speOldWOP.Value);
            _Entity.NewWorkOrderPrice = Convert.ToDecimal(Convert.IsDBNull(speNWOP.Value) ? "0" : speNWOP.Value);
            _Entity.TransType = txtTransType.Text;
            _Entity.Remarks = txtRemarks.Text;
            _Entity.Field1 = txtHField1.Text;
            _Entity.Field2 = txtHField2.Text;
            _Entity.Field3 = txtHField3.Text;
            _Entity.Field4 = txtHField4.Text;
            _Entity.Field5 = txtHField5.Text;
            _Entity.Field6 = txtHField6.Text;
            _Entity.Field7 = txtHField7.Text;
            _Entity.Field8 = txtHField8.Text;
            _Entity.Field9 = txtHField9.Text;
   
            _Entity.AddedBy = Session["userid"].ToString();
      


            string param = e.Parameter.Split('|')[0]; //Renats
            switch (param) //Renats
            {


                case "Add":
             
                    if (error == false)
                    {
                        check = true;
                        strError = Functions.Submitted(_Entity.DocNumber, "Production.WIPPriceChange", 1, Session["ConnString"].ToString());//NEWADD factor 1 if submit, 2 if approve
                        if (!string.IsNullOrEmpty(strError))
                        {
                            cp.JSProperties["cp_message"] = strError;
                            cp.JSProperties["cp_success"] = true;
                            cp.JSProperties["cp_forceclose"] = true;
                            return;
                        }
                        _Entity.UpdateData(_Entity);
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
                         _Entity.LastEditedBy = Session["userid"].ToString();
           
            _Entity.LastEditedDate = DateTime.Now.ToString();
                    if (error == false)
                    {
                        check = true;
                        strError = Functions.Submitted(_Entity.DocNumber, "Production.WIPIN", 1, Session["ConnString"].ToString());//NEWADD factor 1 if submit, 2 if approve
                        if (!string.IsNullOrEmpty(strError))
                        {
                            cp.JSProperties["cp_message"] = strError;
                            cp.JSProperties["cp_success"] = true;
                            cp.JSProperties["cp_forceclose"] = true;
                            return;
                        }
                        _Entity.UpdateData(_Entity);
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



                case "WIP":
                    a = e.Parameter.Split('|')[1]; //Renats
                    b = e.Parameter.Split('|')[2]; //Renats
                    c = e.Parameter.Split('|')[3]; //Renats
                    d = e.Parameter.Split('|')[4]; //Renats
                    f = e.Parameter.Split('|')[5]; //Renats
                    SqlDataSource ds = sdsWIP;



                    ds.SelectCommand = string.Format(" SELECT * FROM (SELECT A.DocNumber,B.DocNumber as ReferenceJO,Step,WorkCenter,WorkOrderPrice,TotalQuantity,'PRDFWT' as TransType  from Production.FinalWIPOut A "
                                                      +"  inner join Production.JobOrder B "
                                                      +"  on a.JobOrder = B.DocNumber "
                                                      + "  where B.Status IN ('W','C')  and (CASE WHEN B.Status='C' THEN DateCompleted ELSE  (SELECT Value from IT.SystemSettings where Code='BOOKDATE') END)>= (SELECT Value from IT.SystemSettings where Code='BOOKDATE') "
                                                      +"  and ISNULL(SubmittedBy,'')!='' "
                                                      +"  UNION ALL "
                                                      + "  SELECT A.DocNumber,B.DocNumber as ReferenceJO,Step,WorkCenter,WorkOrderPrice,TotalQuantity,'PRDWOT' as TransType from Production.WIPOut A "
                                                      +"  inner join Production.JobOrder B "
                                                      +"  on a.JobOrder = B.DocNumber "
                                                      + "  where B.Status IN ('W','C')  and (CASE WHEN B.Status='C' THEN DateCompleted ELSE  (SELECT Value from IT.SystemSettings where Code='BOOKDATE') END)>= (SELECT Value from IT.SystemSettings where Code='BOOKDATE') "
                                                      +"  and ISNULL(SubmittedBy,'')!='' "
                                                      +"  UNION ALL "
                                                      + "  SELECT A.DocNumber,B.DocNumber as ReferenceJO,Step,WorkCenter,WorkOrderPrice,TotalQuantity,'PRDCWT' as TransType  from Production.CuttingWorksheet A "
                                                      +"  inner join Production.JobOrder B "
                                                      +" on a.JobOrder = B.DocNumber "
                                                      + "  where B.Status IN ('W','C')  and (CASE WHEN B.Status='C' THEN DateCompleted ELSE  (SELECT Value from IT.SystemSettings where Code='BOOKDATE') END)>= (SELECT Value from IT.SystemSettings where Code='BOOKDATE') "
                                                       + " and ISNULL(SubmittedBy,'')!='') AS WIP  where DocNumber = '" + a + "' AND ReferenceJO = '" +b + "' AND Step = '" + c + "' and WorkCenter= '" + d + "'");


                    DataView inb = (DataView)ds.Select(DataSourceSelectArguments.Empty);
                    if (inb.Count > 0)
                    {
                        txtStep.Text = inb[0]["Step"].ToString();
                        txtWorkCenter.Text = inb[0]["WorkCenter"].ToString();
                        speOldWOP.Text = inb[0]["WorkOrderPrice"].ToString();
                        txtJONumber.Text = inb[0]["ReferenceJO"].ToString();
                        speQty.Text = inb[0]["TotalQuantity"].ToString();
                        txtTransType.Text = f;
                    }

                    break;


                case "Generate":

                    // GetVat();
                    OtherDetail();
                    cp.JSProperties["cp_generated"] = true;

                    //glSupplierCode.Enabled = false;
                    //Generatebtn.Enabled = false;
                    break;

                case "Close":
                    cp.JSProperties["cp_close"] = true;
                    break;



            }
        }



        protected void Connection_Init(object sender, EventArgs e)
        {
            sdsWIP.ConnectionString = Session["ConnString"].ToString();

        }
        protected void dtpDocDate_Init(object sender, EventArgs e)
        {
            if (Request.QueryString["entry"] == "N")
            {
                dtpdocdate.Date = DateTime.Now;
            }
        }
        private void OtherDetail()
        {
            //string pick = ;


            //DataTable ret = Gears.RetriveData2("select B.TargetDeliveryDate,Labor from Procurement.SOWorkOrder A  inner join Procurement.ServiceOrder B on A.DocNumber = B.DocNumber WHERE A.DocNumber = '" + pick + "'", Session["ConnString"].ToString());

            //if (ret.Rows.Count > 0)
            //{
            //    speOldWorkOrder.Text = ret.Rows[0][1].ToString();
            //    dtoldduedate.Text = Convert.ToDateTime(ret.Rows[0][0].ToString()).ToShortDateString();
            //    speNewWorkOrder.Text = ret.Rows[0][1].ToString();
            //    dtnewduedate.Text = Convert.ToDateTime(ret.Rows[0][0].ToString()).ToShortDateString();
            //}
            //else
            //{
            //    speOldWorkOrder.Text = "0.00";
            //    dtoldduedate.Text = null;
            //}


        }



        #endregion

    }
}