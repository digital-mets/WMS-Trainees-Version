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
using System.IO;
using System.Web.Services;


namespace GWL
{
    public partial class frmMaterialOrder : System.Web.UI.Page
    {
        Boolean error = false;//Boolean for grid validation
        Boolean view = false;//Boolean for view state

        private static string strError;

        Entity.MaterialOrder _Entity = new MaterialOrder();//Calls entity KPIRef
        Entity.MaterialOrder.MaterialOrderDetail _EntityDetail = new MaterialOrder.MaterialOrderDetail();//Call entity sdsDetail

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
                Session["RevRemarks"] = null;
                switch (Request.QueryString["entry"].ToString())
                {
                    case "N":
                        updateBtn.Text = "Add";
                        break;
                    case "E":
                        updateBtn.Text = "Update";
                        bool a= frmlayout1.Visible;
                       
                        break;
                    case "V":
                        view = true;//sets view mode for entry
                        updateBtn.Text = "Close";
                        glcheck.ClientVisible = false;
                        RRemarks.ReadOnly = true;
                        Picture1.Visible = false;
                        gv1.SettingsEditing.BatchEditSettings.ShowConfirmOnLosingChanges = false;
                        break;
                    case "D":
                        updateBtn.Text = "Delete";
                        break;
                }
              
               
                

                txtDocnumber.Value = Request.QueryString["docnumber"].ToString(); //sets docnumber from session



                _Entity.getdata(txtDocnumber.Text, Session["ConnString"].ToString()); //Method for retrieving data from entity



                dtpdocdate.Text = Convert.ToDateTime(_Entity.DocDate.ToString()).ToShortDateString();
                
                
                txtRemarks.Text = _Entity.Remarks.ToString();
                ProductionSite.Value = _Entity.ProductionSite.ToString();
                txtCustomerCode.Value = _Entity.CustomerCode.ToString();
                txtWorkWeek.Text = _Entity.WorkWeek.ToString();
                WWeeks.Text = _Entity.WorkWeek.ToString();



                string entry = Request.QueryString["entry"].ToString();
                string parameters = Request.QueryString["parameters"].ToString().Split('|')[0];
                if (parameters == "ReviseTrans")
                {
                    dtpOrderDate.Text = Convert.ToDateTime(_Entity.OrderDate.ToString()).ToShortDateString();
                    glYear.Value = _Entity.Year.ToString();
                }
                
                else if (entry == "N")
                {
                    glYear.Value = DateTime.Now.Year.ToString();
                    string orderD = _Entity.OrderDate.ToString();
                    //dtpOrderDate.Text = _Entity.OrderDate.ToString();
                    if (orderD == "" || orderD == null)
                    {
                        dtpOrderDate.Text = _Entity.OrderDate.ToString();

                    }
                    else
                    {
                        dtpOrderDate.Text = Convert.ToDateTime(_Entity.OrderDate.ToString()).ToShortDateString();
                    }
                }
                
                else
                {
                    glYear.Value = _Entity.Year.ToString();
                    string orderD = _Entity.OrderDate.ToString();
                    if (orderD == "" || orderD == null)
                    {
                        dtpOrderDate.Text = _Entity.OrderDate.ToString();

                    }
                    else
                    {
                        dtpOrderDate.Text = Convert.ToDateTime(_Entity.OrderDate.ToString()).ToShortDateString();
                    }
                }
               
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
                txtHSubmittedBy.Text = _Entity.SubmittedBy;
                txtHSubmittedDate.Text = _Entity.SubmittedDate;
                txtHCancelledBy.Text = _Entity.CancelledBy;
                txtHCancelledDate.Text = _Entity.CancelledDate;
                txtHRevisedBy.Text = _Entity.RevisedBy;
                txtHRevisedDate.Text = _Entity.RevisedDate;
                FileName.Text = _Entity.Picture1;
                Uploadedpath.Text = _Entity.Picture1;
                RRemarks.Text = _Entity.RRemarks;





                string FName = FileName.Text;
                if (!string.IsNullOrEmpty(FName))
                {
                    picture1Img.ImageUrl = FName;
                }
                else
                {
                    picture1Img.ImageUrl = "../Assets/img/AdminImages/default.png";
                }
             

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
                DataTable checkCount =_EntityDetail.getdetail(txtDocnumber.Text, Session["ConnString"].ToString());
                if (checkCount.Rows.Count > 0)
                {
                    gv1.KeyFieldName = "DocNumber;LineNumber";
                    gv1.DataSourceID = "odsDetail";
                    gv2.KeyFieldName = "DocNumber;LineNumber";
                    gv2.DataSourceID = "odsDetail";
                }
                else
                {
                    gv1.KeyFieldName = "DocNumber;LineNumber";
                    gv1.DataSourceID = "sdsDetail";
                    gv2.KeyFieldName = "DocNumber;LineNumber";
                    gv2.DataSourceID = "sdsDetail";
                }
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
        protected void grid_CustomUnboundColumnData(object sender, ASPxGridViewColumnDataEventArgs e)
        {
            if (e.Column.FieldName == "TotalKG")
            {
                decimal Day1 = (decimal)e.GetListSourceFieldValue("Day1");
                decimal Day2 = (decimal)e.GetListSourceFieldValue("Day2");
                decimal Day3 = (decimal)e.GetListSourceFieldValue("Day3");
                decimal Day4 = (decimal)e.GetListSourceFieldValue("Day4");
                decimal Day5 = (decimal)e.GetListSourceFieldValue("Day5");
                decimal Day6 = (decimal)e.GetListSourceFieldValue("Day6");
                decimal Day7 = (decimal)e.GetListSourceFieldValue("Day7");
                e.Value = Day1 + Day2 + Day3 + Day4 + Day5 + Day6 + Day7;

            }
            if (e.Column.FieldName == "TotalBatch")
            {
                decimal Batch = (decimal)e.GetListSourceFieldValue("BatchWeight");
                decimal Day1 = (decimal)e.GetListSourceFieldValue("Day1");
                decimal Day2 = (decimal)e.GetListSourceFieldValue("Day2");
                decimal Day3 = (decimal)e.GetListSourceFieldValue("Day3");
                decimal Day4 = (decimal)e.GetListSourceFieldValue("Day4");
                decimal Day5 = (decimal)e.GetListSourceFieldValue("Day5");
                decimal Day6 = (decimal)e.GetListSourceFieldValue("Day6");
                decimal Day7 = (decimal)e.GetListSourceFieldValue("Day7");
                e.Value = (Day1 + Day2 + Day3 + Day4 + Day5 + Day6 + Day7) * Batch;

            }
           
        }
        protected void gv1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string Batch = e.Row.Cells[8].Text;

                if (e.Row.Cells[8].Text == "1")
                {
                    e.Row.BackColor = System.Drawing.Color.Black;
                    //e.Row.Cells[.ForeColor = Color.Red;
                }
                else
                {
                    e.Row.BackColor = System.Drawing.Color.White;
                }
            }
        }
//WorkWeek
        //protected Int32 GetWeekNumber(DateTime Input)
        //{


        //    int var = Convert.ToInt32(txtWorkWeek.Text);
        //    int week = var;
        //    DateTime YearStartDate = new DateTime(Input.Year, 1, 1);

        //    if (YearStartDate.Date.DayOfWeek != DayOfWeek.Thursday)
        //    {

        //        week = 1;

        //    }



        //    while (YearStartDate.Date.DayOfWeek != DayOfWeek.Monday)
        //        YearStartDate = YearStartDate.Date.AddDays(1);



        //    int Days = Input.Subtract(YearStartDate).Days; while (Days < 0)
        //    {

        //        DateTime PreviousYearStartDate = new DateTime(Input.Year - 1, 1, 1);


        //        while (PreviousYearStartDate.Date.DayOfWeek != DayOfWeek.Monday)
        //            PreviousYearStartDate = PreviousYearStartDate.Date.AddDays(1);

        //        Days = Input.Subtract(PreviousYearStartDate).Days;

        //    }

        //    week = week + Convert.ToInt32(Days / 7); if (week >= 52)
        //    {

        //        week = 0;
                
        //    }



        //    return (week + 1);
            
        //}

        protected void UploadControlLoad(object sender, EventArgs e)
        {
            ASPxUploadControl uploadcontrol = sender as ASPxUploadControl;
            uploadcontrol.Enabled = !view;
        }
        protected void ImageLoad(object sender, EventArgs e)
        {
            ASPxImage image = sender as ASPxImage;
            image.ReadOnly = view;
        }

        string fileholder = "";
        protected void UploadFile(object sender, EventArgs e)
        {
            string flPicture1 = "";
            string extensionPicture1 = "";
            string fnPicture1 = "";
            string remarks = "";

            flPicture1 = Path.GetFileNameWithoutExtension(Picture1.FileName);
            extensionPicture1 = Path.GetExtension(Picture1.FileName);            
            flPicture1 = flPicture1 + extensionPicture1;
            remarks = RRemarks.Text;
            
            bool b2 = string.IsNullOrEmpty(flPicture1);
            DataTable a1 = Gears.RetriveData2("select Value from it.SystemSettings where Code = 'PROOFPATH'", HttpContext.Current.Session["ConnString"].ToString());
            string Picture1path = a1.Rows[0][0].ToString();
            FileName.Text = Picture1path + flPicture1;
            RRemarksText.Text = remarks;
            picture1Img.ImageUrl = Picture1path + flPicture1;
            if (b2 != true)
            {

                fnPicture1 = Path.Combine(Server.MapPath(Picture1path), flPicture1);
                
                Picture1.SaveAs(fnPicture1);

            }
            else
            {

            }
        
        
        
        }
        [WebMethod]
        public static string RemarksRev(MaterialOrder name)
        {
            string a = "";


            try
            {
                
                HttpContext.Current.Session["RevRemarks"] = name.RRemarks;
            }


            catch (Exception ex)
            {
                a = ex.Message;
            }

            return a;

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
        private void Validate()
        {
            GearsLibrary.Gears.GearsParameter gparam = new Gears.GearsParameter();
            gparam._DocNo = _Entity.DocNumber;
            gparam._TransType = "PRDMTO";
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
            string param = e.Parameter.Split('|')[0];
            string revise = Request.QueryString["parameters"].ToString().Split('|')[0];
            string docno = Request.QueryString["docnumber"].ToString();
            _Entity.Connection = Session["ConnString"].ToString(); 
            _Entity.DocNumber = txtDocnumber.Text;
            _Entity.DocDate = dtpdocdate.Text;
            _Entity.OrderDate = dtpOrderDate.Text;
            _Entity.Remarks = txtRemarks.Text;
            _Entity.ProductionSite = ProductionSite.Text;
            _Entity.CustomerCode = txtCustomerCode.Text;
            _Entity.WorkWeek = txtWorkWeek.Text;
            _Entity.Year = glYear.Text;        
            _Entity.Year = glYear.Text;
            _Entity.AddedBy = Session["userid"].ToString();
            _Entity.RevisedBy = Session["userid"].ToString();
            _Entity.Picture1 = FileName.Text;

            if (string.IsNullOrEmpty(Session["RevRemarks"] as string))
            {
                _Entity.RRemarks = RRemarksText.Text;
            }
            else
            {
                _Entity.RRemarks = Session["RevRemarks"].ToString();
               
            }

            
            _Entity.Parameters = revise;
            _Entity.Docno = docno;
            
            switch (param) 
            {
                    
                case "Add":
                    if (error == false)
                    {

                        if (revise == "ReviseTrans")
                        {
                            _Entity.InsertData(_Entity);
                        }
                        else {
                            _Entity.UpdateData(_Entity);
                        }
                          gv1.DataSourceID = "odsDetail";
                          gv2.DataSourceID = "odsDetail";
                        odsDetail.SelectParameters["DocNumber"].DefaultValue = txtDocnumber.Text;
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
                    if (error == false)
                    {
                        _Entity.LastEditedBy = Session["userid"].ToString();

                        _Entity.LastEditedDate = DateTime.Now.ToString();
                        strError = Functions.Submitted(_Entity.DocNumber, "Production.MaterialOrder", 1, Session["ConnString"].ToString());//NEWADD factor 1 if submit, 2 if approve
                        if (!string.IsNullOrEmpty(strError))
                        {
                            gv1.JSProperties["cp_message"] = strError;
                            gv1.JSProperties["cp_success"] = true;
                            gv1.JSProperties["cp_forceclose"] = true;
                            return;
                        }

                        _Entity.UpdateData(_Entity);
                      
                        gv1.DataSourceID = "odsDetail";
                        odsDetail.SelectParameters["DocNumber"].DefaultValue = txtDocnumber.Text;
                      
                        gv1.UpdateEdit();
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
                    break;


                case "Step":
                    Gears.RetriveData2("DELETE FROM Production.MaterialOrderDetail where Docnumber='" + txtDocnumber.Text + "'", Session["ConnString"].ToString());
        
                    gv1.DataSourceID = "sdsDetail";
                    gv1.DataBind();
                    break;

               
            }
        }



        protected void Connection_Init(object sender, EventArgs e)
        {
            sdsJobOrder.ConnectionString = Session["ConnString"].ToString();
            sdsStep.ConnectionString = Session["ConnString"].ToString();
            sdsDetail.ConnectionString = Session["ConnString"].ToString();
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
        //    ASPxGridView grid = sender as ASPxGridView;
        //    CriteriaOperator selectionCriteria = new InOperator("StepCode", new string[] { glStep.Text });
        //    sdsJobOrder.FilterExpression = (GroupOperator.Combine(GroupOperatorType.And, selectionCriteria)).ToString();
        //    Session["JobOrder"] = sdsJobOrder.FilterExpression;
        //    grid.DataSourceID = "sdsJobOrder";
        //    grid.DataBind();

        //    for (int i = 0; i < grid.VisibleRowCount; i++)
        //    {
        //        if (grid.GetRowValues(i, "StepCode") != null)
        //            if (grid.GetRowValues(i, "StepCode").ToString() == e.Parameters)
        //            {
        //                grid.Selection.SelectRow(i);
        //                string key = grid.GetRowValues(i, "StepCode").ToString();
        //                grid.MakeRowVisible(key);
        //                break;
        //            }
        //    }
        }

        //#region Images
        ////protected void btnFrontUpload_FileUploadComplete(object sender, FileUploadCompleteEventArgs e)
        ////{
        ////    txt2DFrontImage64string.Text = Convert.ToBase64String((byte[])e.UploadedFile.FileBytes);

        ////    e.CallbackData = "data:image/jpg;base64," + Convert.ToBase64String((byte[])e.UploadedFile.FileBytes);
        ////}

        ////protected void btnBackUpload_FileUploadComplete(object sender, FileUploadCompleteEventArgs e)
        ////{
        ////    txt2DBackImage64string.Text = Convert.ToBase64String((byte[])e.UploadedFile.FileBytes);

        ////    e.CallbackData = "data:image/jpg;base64," + Convert.ToBase64String((byte[])e.UploadedFile.FileBytes);
        ////}

        //protected void gvuploadimageembroider_FileUploadComplete(object sender, FileUploadCompleteEventArgs e)
        //{
        //    e.CallbackData = "data:image/jpg;base64," + Convert.ToBase64String((byte[])e.UploadedFile.FileBytes);
        //}
        //protected void gvuploadimageprint_FileUploadComplete(object sender, FileUploadCompleteEventArgs e)
        //{
        //    e.CallbackData = "data:image/jpg;base64," + Convert.ToBase64String((byte[])e.UploadedFile.FileBytes);
        //}
        //protected void gvgvuploadimageotherimage_FileUploadComplete(object sender, FileUploadCompleteEventArgs e)
        //{
        //    e.CallbackData = "data:image/jpg;base64," + Convert.ToBase64String((byte[])e.UploadedFile.FileBytes);
        //}
        //#endregion

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
            string itemcode1 = e.Parameters.Split('|')[2];
            string val = e.Parameters.Split('|')[2];
            if (val.Contains("GLP_AIC") || val.Contains("GLP_AC") || val.Contains("GLP_F")) return;

            var itemlookup = sender as ASPxGridView;
            string codes = "";
            if (e.Parameters.Contains("Step"))
            {

                itemlookup.JSProperties["cp_codes"] = itemcode1;
            }
        }

        #endregion

        protected void dtpDocDate_Init(object sender, EventArgs e)
        {
            if (Request.QueryString["entry"] == "N")
            {
                dtpdocdate.Date = DateTime.Now;
            }
        }

        protected void dtpOrderDate_Init(object sender, EventArgs e)
        {
            if (Request.QueryString["entry"] == "N")
            {
                dtpOrderDate.Date = DateTime.Now;
            }
        }
    }
}