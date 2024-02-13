using DevExpress.Spreadsheet;
using DevExpress.Spreadsheet.Export;
using DevExpress.Web;
using DevExpress.Web.Data;
using DevExpress.XtraGrid;
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
using GearsProduction;
using System.Data.OleDb;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Web.Services;


namespace GWL
{
    public partial class frmMachineMaster : System.Web.UI.Page
    {
        Boolean error = false;//Boolean for grid validation
        Boolean view = false;//Boolean for view state
        Boolean check = false;//Boolean for grid validation
        Boolean edit = false;   //Boolean for Edit Mode
        Boolean postback = false;
        Boolean isDuplicate = false;
        
        Entity.MachineMaster _Entity = new Entity.MachineMaster();//Calls entity odsHeader
        Entity.MachineMaster.PMSchedule _PMSchedule = new Entity.MachineMaster.PMSchedule();
        Entity.MachineMaster.PMChecklist _PMChecklist = new Entity.MachineMaster.PMChecklist();
        Entity.MachineMaster.ProdRoutingStepMachine _ProdRoutingStepMachine = new Entity.MachineMaster.ProdRoutingStepMachine();
        Entity.MachineMaster.ProdRoutingStepManpower _ProdRoutingStepManpower = new Entity.MachineMaster.ProdRoutingStepManpower();
     

        #region page load/entry
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (Request.QueryString["entry"] == "N"); // Set status Value if entry is N

            //view = (Request.QueryString["entry"].ToString() == "V"); // If entry equals to V, then readonly
            if (Request.QueryString["entry"].ToString() == "V")
            {
                view = true;//sets view mode for entry
                MachineID.ReadOnly = true;
                Upload.Visible = false;

            }
            if (!IsPostBack)
            {
                Session["FilePath"] = null;
                //datasources(); // Load all datasources selectcommand
                //sets docnumber from session
                if (Request.QueryString["entry"].ToString() == "N")
                {
                    // gv1.DataSourceID = "sdsDetail";
                    
                    //sdsDetail.SelectCommand = "select * from WMS.PICKLIST where docnumber is null";
                    //sdsDetail.SelectParameters.Remove(sdsDetail.SelectParameters["DocNumber"]);
                    DEL.Visible = false;
                    FileList.Visible = false;
                }
                else
                {
                    MachineID.ReadOnly = true;
                    MachineID.Value = Request.QueryString["docnumber"].ToString();


                    _Entity.getdata(MachineID.Text, Session["ConnString"].ToString());

                    //MachineID.Text = _Entity.MachineID;
                    RecordID.Text = _Entity.RecordID;
                    txtAssetCode.Text = _Entity.AssetCode;
                    AssetTag.Text = _Entity.AssetTag;
                    Description.Text = _Entity.Description;
                    MachineCategory.Text = _Entity.MachineCategory;
                    Cat.Text = _Entity.MachineCategory;
                    Brand.Text = _Entity.Brand;
                    txtModel.Text = _Entity.Model;
                    SerialNo.Text = _Entity.SerialNo;
                    SupplyVoltage.Text = _Entity.SupplyVoltage;
                    glLocation.Text = _Entity.Location;
                    glAssignedPersonnel.Text = _Entity.AssignedPersonnel;
                    Status.Text = _Entity.Status;
                    Section.Text = _Entity.Section; 
                    FileName.Text = _Entity.Manual;
                    Session["FilePath"] = _Entity.Manual;

                    dtpDocDate.Text = String.IsNullOrEmpty(_Entity.DateAcquired) ? null : Convert.ToDateTime(_Entity.DateAcquired.ToString()).ToShortDateString();




                    //chkisinactive.Value = Convert.ToBoolean(_Entity.ColorIsInactive);




                    txtActivatedBy.Text = _Entity.ActivatedBy;
                    txtActivatedDate.Text = _Entity.ActivatedDate;
                    txtDeactivatedBy.Text = _Entity.DeactivatedBy;
                    txtDeactivatedDate.Text = _Entity.DeactivatedDate;
                    txtAddedBy.Text = _Entity.AddedBy;
                    txtAddedDate.Text = _Entity.AddedDate;
                    txtLastEditedBy.Text = _Entity.LastEditedBy;
                    txtLastEditedDate.Text = _Entity.LastEditedDate;

                }
                // Destroy Sessions
                //Session["SKUCode"] = null;
                //Session["StepSequence"] = null;
                //postback = true;


                InitControls();
                switch (Request.QueryString["entry"].ToString())
                {




                    case "N":
                        updateBtn.Text = "Add";
                        break;
                    case "E":
                        updateBtn.Text = "Update";
                        break;

                    case "V":
                        view = true;
                        updateBtn.Text = "Close";
                        ViewOnly();
                        break;

                    case "D":
                        view = true;
                        updateBtn.Text = "Delete";
                        ViewOnly();
                        break;
                }

                switch (Request.QueryString["entry"].ToString())
                {
                    case "N":
                    case "E":
                    case "V":
                        view = true;
                        //DataTable dt = Gears.RetriveData2("SELECT SKUCode FROM Production.ProdRouting WHERE RecordID='" + Request.QueryString["docnumber"].ToString() + "'", Session["ConnString"].ToString());

                        //SKUCode.Value = dt.Rows[0][0].ToString();

                        //_Entity.getdata(SKUCode.Text, Session["ConnString"].ToString());//ADD CONN

                        //setValues();
                        break;
                }
            }
        }
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

        protected void DeleteManual(object sender, EventArgs e)
        {
            Session["FilePath"] = "";
            FileName.Text = null;
        }
        [WebMethod]
        public static string UpdateSession(MachineMaster name)
        {
            string a = "";
          

            try{
                HttpContext.Current.Session["FilePath"] = name.Manual;
            }
            

         catch (Exception ex)
            {
                a = ex.Message;
            }

            return a;
        
        }
        protected void Upload_FileUploadComplete(object sender, FileUploadCompleteEventArgs e)
        {
            string PathSave = "../MachineManual/";
            string fn = "";
            string Ex = "";
            string FilenameExt = "";
            string Exten = "";
            string FilePathmulti = "";
            string Name = "";

            string FP = FileName.Text;

            Exten = Path.GetExtension(e.UploadedFile.FileName);
            Name = Path.GetFileNameWithoutExtension(e.UploadedFile.FileName);

            switch (Exten)
            {
                case ".pdf":
                    e.CallbackData = String.Format("\\" + Name + "{0}.pdf", DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss"));
                    break;
                case ".PDF":
                    e.CallbackData = String.Format("\\" + Name + "{0}.pdf", DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss"));
                    break;
                case ".docx":
                    e.CallbackData = String.Format("\\" + Name + "{0}.docx", DateTime.Now.ToString("yyyy-MM-dd hh-mm-ss"));
                    break;
                case ".xlsx":
                    e.CallbackData = String.Format("\\" + Name + "{0}.xlsx", DateTime.Now.ToString("yyyy-MM-dd hh-mm-ss"));
                    break;

            }

            
            string path = Page.MapPath("../MachineManual/") + e.CallbackData;

            fn = Path.GetFileNameWithoutExtension(e.CallbackData);
            Ex = Path.GetExtension(e.CallbackData);
            FilenameExt = PathSave + fn + Ex;

            //string sessionpath = Session["FilePath"].ToString();

            if (string.IsNullOrEmpty(Session["FilePath"] as string))
            {
                FilePathmulti = FilenameExt;
            }
            else
            {
                FilePathmulti = FilenameExt + "," + Session["FilePath"].ToString();
            }

           

            Session["FilePath"] = FilePathmulti;

            FileName.Value = FilePathmulti;
            FileName.Text = FilePathmulti;

            e.UploadedFile.SaveAs(path);
            e.ErrorText = FilePathmulti;
            //Update_FileName(sender, e);
        }
        protected void ShowPopup(object sender, EventArgs e)
        {
            string title = "Greetings";
            string body = "Welcome to ASPSnippets.com";
            ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + title + "', '" + body + "');", true);
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            connect();
        }
        //protected void Update_FileName(object sender, EventArgs e)
        //{
        //    string F1 = Session["FilePath"].ToString();
        //    string FP = FileName.Text;
        //    FileName.Value = Session["FilePath"].ToString();
        //    FileName.Text = Session["FilePath"].ToString();
        //}
        private void connect()
        {
            foreach (Control c in form2.Controls)
            {
                if (c is SqlDataSource)
                {
                    ((SqlDataSource)c).ConnectionString = Session["ConnString"].ToString();
                }
            }
        }
        #endregion

        protected void Connection_Init(object sender, EventArgs e)
        {
            ((SqlDataSource)sender).ConnectionString = Session["ConnString"].ToString();
        }

        protected void BatchUpdate(object sender, ASPxDataBatchUpdateEventArgs e)
        {
            if ((error == true || error == false) && check == false)//Prevents updating of grid to enable validation
            {
                e.Handled = true;
                e.InsertValues.Clear();
                e.UpdateValues.Clear();
            }
        }



        protected void ButtonLoad(object sender, EventArgs e)//Control for all textbox
        {
            ASPxButton btn = sender as ASPxButton;
            btn.ClientVisible = !view;
        }
        protected void TextboxLoad(ASPxEdit sender)
        {
            ASPxTextBox text = sender as ASPxTextBox;
            if (!text.ReadOnly)
                text.ReadOnly = view;
        }
        protected void LookupLoad(ASPxEdit sender)//Control for all lookup in header
        {
            ASPxGridLookup look = sender as ASPxGridLookup;
            look.DropDownButton.Enabled = !view;
            look.ReadOnly = view;
        }
        protected void CheckBoxLoad(ASPxEdit sender)
        {
            var check = sender as ASPxCheckBox;
            check.ReadOnly = view;
        }
        protected void ComboBoxLoad(ASPxEdit sender)
        {
            var combo = sender as ASPxComboBox;
            combo.ReadOnly = view;
        }
        protected void MemoLoad(ASPxEdit sender)
        {
            var memo = sender as ASPxMemo;
            memo.ReadOnly = view;
        }
        protected void Date_Load(ASPxEdit sender)//Control for all date editor
        {
            ASPxDateEdit date = sender as ASPxDateEdit;
            date.DropDownButton.Enabled = !view;
            date.ReadOnly = view;
        }
        protected void SpinEdit_Load(ASPxEdit sender)//Control for all numeric entries in header
        {
            ASPxSpinEdit spinedit = sender as ASPxSpinEdit;
            spinedit.ReadOnly = view;
        }

        protected void gv_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e) // Gridview CommandButton
        {   //Sets icon for grid/Control for Enabling/Disabling of buttons on grid.
            ASPxGridView grid = sender as ASPxGridView;
            if (Request.Params["__CALLBACKID"] == null && IsPostBack) return;
            if (!IsPostBack || Request.Params["__CALLBACKID"].Contains(grid.ID))
            {
                if (Request.QueryString["entry"] == "V" || Request.QueryString["entry"] == "D")
                {
                    if (e.ButtonType == ColumnCommandButtonType.New || e.ButtonType == ColumnCommandButtonType.Delete ||
                        e.ButtonType == ColumnCommandButtonType.Cancel || e.ButtonType == ColumnCommandButtonType.Edit)
                    {
                        e.Visible = false;
                    }
                }
            }
        }

        protected void gv_CustomButtonInitialize(object sender, ASPxGridViewCustomButtonEventArgs e) // Gridview CustomButton
        {
            ASPxGridView grid = sender as ASPxGridView;
           
            if (Request.Params["__CALLBACKID"] == null && IsPostBack) return;
            if (!IsPostBack || Request.Params["__CALLBACKID"].Contains(grid.ID))
            {
                if (Request.QueryString["entry"] == "V" || Request.QueryString["entry"] == "D")
                {
                    if (e.ButtonID == "Delete" || e.ButtonID == "PMDelete" || e.ButtonID == "BOMDelete" || e.ButtonID == "MachineDelete"
                        || e.ButtonID == "ManpowerDelete" || e.ButtonID == "MaterialDelete")
                    {
                        e.Visible = DevExpress.Utils.DefaultBoolean.False;
                    }
                }
            }
        }

        protected void grid_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e) // Validation for gridview, set all validate conditions here.. (sample here is checking of empty cells)
        {
            if (error == false && check == false)
            {
                if (e.Errors.Count > 0)
                {
                    error = true; //bool to cancel adding/updating if true
                }
            }
        }

        #region Functions
        private void datasources()
        {
            // Gridview
            sdsStepProcess.SelectCommand = "select LineNumber,MachineID,MachineCode,Description,Brand,Model,SerialNo,SupplyVoltage from Masterfile.MachineDetail WHERE MachineCode IS NULL";
            sdsPMSched.SelectCommand = "select LineNumber,MachineID,MachineCode,DLineNumber,PMType,DateTime,Time,Priority,ReportTitle,FormNo,Version,ChecklistForm from Masterfile.MachinePMSched";
            sdsBOM.SelectCommand = "SELECT RecordID,PMNumber,PMSNumber,LineNumber,Code,ActivityTask,Remarks,Conformance,NonConformance FROM Masterfile.MachinePMChecklist WHERE MachineCode IS NULL";
            sdsMaterial.SelectCommand = "SELECT RecordID,PMNumber,PMCNumber,PMSNumber,LineNumber,MachineCode,MaterialType,Qty,Remarks FROM Masterfile.MachineMaterialReq";
            sdsTool.SelectCommand = "SELECT RecordID,PMNumber,PMCNumber,PMSNumber,LineNumber,MachineCode,ToolType,Qty,Remarks FROM Masterfile.MachineToolReq";
            sdsDowntime.SelectCommand = "SELECT A.DocNumber,'Corrective' AS Type,RefWONumber AS WorkOrderNo,Concern AS ProblemIssue,B.Code,B.ServicedBy,Convert(varchar, B.TimeReported) AS EncounteredDateandTime,Convert(varchar, B.DateCompleted) AS CompletionDateandTime,	CAST(DATEDIFF(SECOND,B.TimeReported, ISNULL(B.DateCompleted, GETDATE())) / 60.0 / 60.0 AS decimal(15, 2)) AS DownTimeInHour,B.Description AS Remarks,B.LineNumber FROM PM.CorrectiveReport A LEFT JOIN PM.CorrectiveReportDetails B ON A.DocNumber = B.DocNumber";
            sdsPMGen.SelectCommand = "Select DocNumber,A.PMType,YEAR(StartDateTime) AS Year,DATEPART(week, StartDateTime)-1 AS WeekNo,DATENAME(dayofyear , StartDateTime) AS Day,Code,Status,WorkOrderControlNo,Remarks,B.Priority,B.RecordID,B.MachineCode,A.LineNumber,B.DLineNumber,B.MachineID from PM.WorkOrderDetailPM A left join Masterfile.MachinePMSched B on A.Code = B.MachineID";

        //    // Lookup
        //    sdsSKUCode.SelectCommand = "SELECT A.ItemCode, A.FullDesc FROM Masterfile.Item A LEFT JOIN Production.ProdRouting B ON A.ItemCode = B.SKUCode WHERE B.SKUCode IS NULL";
        //    sdsCustomerCode.SelectCommand = "SELECT BizPartnerCode, Name FROM Masterfile.BizPartner";
        //    sdsStepCode.SelectCommand = "SELECT StepCode, Description FROM Masterfile.Step";
        }
        protected void InitControls()
        {
            foreach (var item in frmLayoutRouting.Items)
            {
                if (item is LayoutGroupBase)
                    (item as LayoutGroupBase).ForEach(GetNestedControls);
            }
        }
        protected void GetNestedControls(LayoutItemBase item)
        {
            if (item is LayoutItem)
                SetViewState(item as LayoutItem);
        }
        protected void SetViewState(LayoutItem c)
        {
            if (c.ParentGroup.Caption != "Audit Trail")
                foreach (Control control in c.Controls)
                {
                    ASPxEdit editor = control as ASPxEdit;
                    if (editor != null)
                    {
                        if (editor.GetType().ToString() == "DevExpress.Web.ASPxTextBox")
                        {
                            TextboxLoad(editor);
                        }
                        if (editor.GetType().ToString() == "DevExpress.Web.ASPxGridLookup")
                        {
                            LookupLoad(editor);
                        }
                        if (editor.GetType().ToString() == "DevExpress.Web.ASPxDateEdit")
                        {
                            Date_Load(editor);
                        }
                        if (editor.GetType().ToString() == "DevExpress.Web.ASPxSpinEdit")
                        {
                            SpinEdit_Load(editor);
                        }
       
                        if (editor.GetType().ToString() == "DevExpress.Web.ASPxMemo")
                        {
                            MemoLoad(editor);
                        }
                    }
                }
        }

        protected void setValues()
        {
            //RecordID.Value = _Entity.RecordID.ToString();
            
            //CustomerCode.Value = _Entity.CustomerCode.ToString();
            //CustomerName.Text = _Entity.CustomerName.ToString();
            //Remarks.Text = _Entity.Remarks.ToString();
           
            //EffectivityDate.Value = String.IsNullOrEmpty(_Entity.EffectivityDate.ToString()) ? null : Convert.ToDateTime(_Entity.EffectivityDate.ToString()).ToShortDateString();
        }

        protected void ViewOnly()
        {
            //CustomerCode.Enabled = false;
            //Remarks.Enabled = false;
          
        }
        #endregion

        #region Callback functions(Inserting/Updating/Deleting/Validating)
        protected void cp_Callback(object sender, CallbackEventArgsBase e)
        {

            DataTable Code = Gears.RetriveData2("select * from Masterfile.Machine  WHERE MachineID = '" + MachineID.Text + "'", Session["ConnString"].ToString());
            if (Code.Rows.Count > 0 && updateBtn.Text == "Add")
            {
                cp.JSProperties["cp_message"] = "Machine Code: '" + MachineID.Text + "' already Exist!";
                cp.JSProperties["cp_success"] = true;
                return;
            }

            _Entity.Connection = Session["ConnString"].ToString();
            _Entity.RecordID = RecordID.Text;
            _Entity.MachineID = MachineID.Text;
     

            _Entity.AssetCode = txtAssetCode.Text;
            _Entity.AssetTag = AssetTag.Text;
            _Entity.Description = Description.Text;
            _Entity.MachineCategory = MachineCategory.Text;
            _Entity.Brand = Brand.Text;
            _Entity.Model = txtModel.Text;
            _Entity.SerialNo = SerialNo.Text;
            _Entity.SupplyVoltage = SupplyVoltage.Text;
            _Entity.Location = glLocation.Text;
            _Entity.AssignedPersonnel = glAssignedPersonnel.Text;
            _Entity.Status = Status.Text;
            _Entity.Section = Section.Text;
            
            _Entity.DateAcquired = dtpDocDate.Text;

            if (string.IsNullOrEmpty(Session["FilePath"] as string))
            {
                
            }
            else
            {
                _Entity.Manual = Session["FilePath"].ToString();
            }




            _Entity.AddedBy = Session["userid"].ToString();
            _Entity.LastEditedBy = Session["userid"].ToString();

            string conn = Session["ConnString"].ToString();
            string param = e.Parameter.Split('|')[0];
            switch (param)
            {
                case "Add":
                    
                        check = true;
                        _Entity.InsertData(_Entity);//Method of inserting for header
                        
                   
                        Validate();
                        cp.JSProperties["cp_message"] = "Successfully Added!";//Message variable to client side
                     
                        cp.JSProperties["cp_close"] = true;//Close window variable to client side
                        Session["Refresh"] = "1";
                   

                    break;
                case "Update":

                    if (error == false)
                    {
                        check = true;

                        _Entity.UpdateData(_Entity);

                        cp.JSProperties["cp_message"] = "Updated Successfully!";

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

                case "Close":

                    cp.JSProperties["cp_close"] = true;

                    break;

                // SKUCode Lookup Event, Update SKU Description
                //case "SKUCode":

                //    string PSKUCode = e.Parameter.Split('|')[1];

                //    DataTable SKUCodedata = Gears.RetriveData2("SELECT FullDesc FROM Masterfile.Item WHERE ItemCode='" + PSKUCode + "'", conn);

                //    SKUDescription.Text = SKUCodedata.Rows[0]["FullDesc"].ToString();

                //break;

                //// CustomerCode Lookup Event, Update Customer Name
                //case "CustomerCode":

                //    string PCustomerCode = e.Parameter.Split('|')[1];

                //    DataTable CustomerCodedata = Gears.RetriveData2("SELECT Name FROM Masterfile.BizPartner WHERE BizPartnerCode='" + PCustomerCode + "'", conn);

                //    CustomerName.Text = CustomerCodedata.Rows[0]["Name"].ToString();

                //break;

                //// Step Process Grid, StepCode Lookup Event, Update Step Description
                //case "StepCode":
                //    string PStepCode = e.Parameter.Split('|')[1];

                //    DataTable StepCodedata = Gears.RetriveData2("SELECT Description FROM Masterfile.Item WHERE ItemCode='" + PStepCode + "'", conn);

                //break;
            }
        }
        #endregion
        protected void masterGrid_BeforePerformDataSelect(object sender, EventArgs e) //
        {
            //sdsStepProcess.SelectCommand = "SELECT A.RecordID, HeaderID, SKUCode, StepSequence, A.StepCode, B.Description AS StepDescription, " +
            //                                "SequenceDay FROM Production.ProdRoutingStep A LEFT JOIN Masterfile.Step B ON A.StepCode = B.StepCode " +
            //                                "WHERE SKUCode='" + SKUCode.Text + "' AND HeaderID='" + RecordID.Value + "'";

            sdsStepProcess.SelectCommand = "select LineNumber,MachineID,MachineCode,Description,Brand,Model,SerialNo,SupplyVoltage,RecordID from Masterfile.MachineDetail WHERE MachineID != '' and MachineID='" + MachineID.Text + "'";
            sdsStepProcess.DataBind();
        }

        protected void detailPMSched_BeforePerformDataSelect(object sender, EventArgs e) //a
        {

            sdsPMSched.SelectCommand = "select LineNumber,MachineID,MachineCode,DLineNumber,PMType,DateTime,Time,Priority,ReportTitle,FormNo,Version,ChecklistForm,RecordID from Masterfile.MachinePMSched WHERE MachineID != '' and MachineID='" + MachineID.Text + "'";
            sdsPMSched.DataBind();
        }


        protected void detailBOM_BeforePerformDataSelect(object sender, EventArgs e) //
        {

            sdsBOM.SelectCommand = "SELECT RecordID,PMNumber,LineNumber,PMSNumber,Code,ActivityTask,Remarks,Conformance,NonConformance,MaterialRequirement,Tools FROM Masterfile.MachinePMChecklist WHERE Code='" + (sender as ASPxGridView).GetMasterRowFieldValues("MachineID").ToString() + "' and PMNumber='" + (sender as ASPxGridView).GetMasterRowFieldValues("LineNumber").ToString() + "'";
            sdsBOM.DataBind();
        }

        protected void detailMaterial_BeforePerformDataSelect(object sender, EventArgs e) //
        {

            sdsMaterial.SelectCommand = "SELECT RecordID,PMNumber,PMCNumber,PMSNumber,LineNumber,MachineCode,MaterialType,Qty,Remarks FROM Masterfile.MachineMaterialReq WHERE MachineCode='" + (sender as ASPxGridView).GetMasterRowFieldValues("MachineID").ToString() + "' and PMNumber='" + (sender as ASPxGridView).GetMasterRowFieldValues("LineNumber").ToString() + "'";
            sdsMaterial.DataBind();
        }
        protected void detailTool_BeforePerformDataSelect(object sender, EventArgs e) //
        {

            sdsTool.SelectCommand = "SELECT RecordID,PMNumber,PMCNumber,PMSNumber,LineNumber,MachineCode,ToolType,Qty,Remarks FROM Masterfile.MachineToolReq WHERE MachineCode='" + (sender as ASPxGridView).GetMasterRowFieldValues("MachineID").ToString() + "' and PMNumber='" + (sender as ASPxGridView).GetMasterRowFieldValues("LineNumber").ToString() + "'";
            sdsTool.DataBind();
        }

        protected void detailDownTime_BeforePerformDataSelect(object sender, EventArgs e) //
        {

            sdsDowntime.SelectCommand = "SELECT A.DocNumber,'Corrective' AS Type,RefWONumber AS WorkOrderNo,Concern AS ProblemIssue,B.Code,B.ServicedBy,Convert(varchar, B.TimeReported) AS EncounteredDateandTime,Convert(varchar, B.DateCompleted) AS CompletionDateandTime,	CAST(DATEDIFF(SECOND,B.TimeReported, ISNULL(B.DateCompleted, GETDATE())) / 60.0 / 60.0 AS decimal(15, 2)) AS DownTimeInHour,B.Description AS Remarks,B.LineNumber FROM PM.CorrectiveReport A LEFT JOIN PM.CorrectiveReportDetails B ON A.DocNumber = B.DocNumber  WHERE B.Code='" + MachineID.Text + "'";
            sdsDowntime.DataBind();
        }



        protected void PMGen_BeforePerformDataSelect(object sender, EventArgs e) //
        {
            
            sdsPMGen.SelectCommand = "Select DocNumber,A.PMType,YEAR(StartDateTime) AS Year,DATEPART(week, StartDateTime)-1 AS WeekNo,DATENAME(dayofyear , StartDateTime) AS Day,Code,Status,WorkOrderControlNo,Remarks,B.Priority,B.RecordID,B.MachineCode,A.LineNumber,B.DLineNumber,B.MachineID from PM.WorkOrderDetailPM A left join Masterfile.MachinePMSched B on A.Code = B.MachineID where A.Code = '" + MachineID.Text + "'";
            sdsPMGen.DataBind();
        }
        //protected void detailMachine_BeforePerformDataSelect(object sender, EventArgs e) //
        //{
        //    //sdsMachine.SelectCommand = "SELECT * FROM Production.ProdRoutingStepMachine WHERE SKUCode='" + (sender as ASPxGridView).GetMasterRowFieldValues("SKUCode").ToString() + "'" +
        //    //                            "AND StepSequence='" + (sender as ASPxGridView).GetMasterRowFieldValues("StepSequence").ToString() + "' AND HeaderID='" + (sender as ASPxGridView).GetMasterRowFieldValues("HeaderID").ToString() + "'";
        //    //sdsMachine.DataBind();
        //}
        //protected void detailManpower_BeforePerformDataSelect(object sender, EventArgs e) //
        //{
        //    //sdsManpower.SelectCommand = "SELECT A.*, B.FullDesc AS SKUDescription FROM Production.ProdRoutingStepManpower A LEFT JOIN Masterfile.Item B " +
        //    //                            "ON A.SKUCode = B.ItemCode WHERE SKUCode='" + (sender as ASPxGridView).GetMasterRowFieldValues("SKUCode").ToString() + "'" +
        //    //                            "AND StepSequence='" + (sender as ASPxGridView).GetMasterRowFieldValues("StepSequence").ToString() + "' AND HeaderID='" + (sender as ASPxGridView).GetMasterRowFieldValues("HeaderID").ToString() + "'";
        //    //sdsManpower.DataBind();
        //}
        //protected void otherGrid_BeforePerformDataSelect(object sender, EventArgs e) //
        //{
        //    //sdsOtherMaterials.SelectCommand = "SELECT A.*, B.FullDesc AS Description FROM Production.ProdRoutingOtherMaterials A LEFT JOIN Masterfile.Item B " +
        //    //                                    "ON A.ItemCode = B.ItemCode WHERE SKUCode='" + SKUCode.Text + "' AND HeaderID='" + RecordID.Value + "'";
        //    //sdsOtherMaterials.DataBind();
        //}

        protected void gvStepProcess_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e) //
        {
            if (!IsPostBack) return;
            DataTable dtbl = Gears.RetriveData2("Select Count(*) Number FROM MasterFile.MachineDetail WHERE MachineID ='" + MachineID.Text + "'", Session["ConnString"].ToString());
            string LineNumber = "00001";
            if (dtbl.Rows.Count > 0)
            {
                LineNumber = "0000" + Convert.ToString(Convert.ToInt64(dtbl.Rows[0][0].ToString()) + 1);
            }
            string TableName = "MasterFile.MachineDetail";
            string SQLCommand = "";
            SQLCommand += "INSERT INTO " + TableName;
            SQLCommand += " ( [LineNumber],[MachineID],[MachineCode],[Description],[Brand],[Model],[SerialNo],[SupplyVoltage])";
            SQLCommand += " VALUES (";
            SQLCommand += "'" + LineNumber + "'," + DataValue(MachineID.Text) + "," + DataValue(e.NewValues["MachineCode"]) + "";
            SQLCommand += " ," + DataValue(e.NewValues["Description"]) + "," + DataValue(e.NewValues["Brand"]) + "," + DataValue(e.NewValues["Model"]) + "," + DataValue(e.NewValues["SerialNo"]) + "," + DataValue(e.NewValues["SupplyVoltage"]) + "";
            SQLCommand += " )";

            sdsStepProcess.InsertCommand = SQLCommand;
            //sdsStepProcess.InsertParameters["ICode"].DefaultValue = Code.Text;
            //sdsStepProcess.InsertParameters["IPMType"].DefaultValue = e.NewValues["PMType"].ToString();
            //sdsStepProcess.InsertParameters["IDatetime"].DefaultValue = e.NewValues["DateTime"].ToString();
            //sdsStepProcess.InsertParameters["IPriority"].DefaultValue = e.NewValues["Priority"].ToString();
        }



        public string DataValue(Object Data)
        {
            string output = "";
            if (Data == null)
            {
                output = "NULL";
            }
            else
            {
                output = "'" + Data.ToString() + "'";
            }

            return output;
        }
        protected void gvStepProcess_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e) //
        {
            if (!IsPostBack) return;

            string TableName = "MasterFile.MachineDetail";
            string SQLCommand = "";


            SQLCommand += " UPDATE " + TableName;
            SQLCommand += " SET ";

            SQLCommand += "[Description]=" + DataValue(e.NewValues["Description"]) + ",";
            SQLCommand += "[Brand]=" + DataValue(e.NewValues["Brand"]) + ",";
            SQLCommand += "[Model]=" + DataValue(e.NewValues["Model"]) + ",";
            SQLCommand += "[SerialNo]=" + DataValue(e.NewValues["SerialNo"]) + ",";
            SQLCommand += "[SupplyVoltage]=" + DataValue(e.NewValues["SupplyVoltage"]) + "";
            SQLCommand += " WHERE MachineID= " + DataValue(MachineID.Text) + " AND MachineCode=" + DataValue(e.OldValues["MachineCode"]) + "";

            sdsStepProcess.UpdateCommand = SQLCommand;
        }
        protected void gvStepProcess_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e) //
        {
            sdsStepProcess.DeleteParameters["DRecordID"].DefaultValue = e.Values["RecordID"].ToString();
        }



        protected void gvPMSched_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e) //
        {
            
            var PMT = DataValue(e.NewValues["PMType"]);
            DataTable Pmty = Gears.RetriveData2("select PMType from MasterFile.MachinePMSched WHERE PMType = " + PMT + " and MachineID ='" + MachineID.Text + "'", Session["ConnString"].ToString());
            if (Pmty.Rows.Count > 0)
            {
                cp.JSProperties["cp_message"] = "PMType:'" + PMT + "' already Exist!";
                cp.JSProperties["cp_success"] = true;
                isDuplicate = true;
               
            }
            if (isDuplicate == false) { 
                        if (!IsPostBack) return;
                        var DetailMachineCode = MachineID.Text;
                        //var DetailMachineID = (sender as ASPxGridView).GetMasterRowFieldValues("MachineID").ToString();
                        //var DetailLineNumber = (sender as ASPxGridView).GetMasterRowFieldValues("LineNumber").ToString();

                        DataTable dtbl = Gears.RetriveData2("Select Count(*) Number FROM MasterFile.MachinePMSched WHERE MachineID ='" + MachineID.Text + "'", Session["ConnString"].ToString());
                        string LineNumber = "00001";
                        if (dtbl.Rows.Count > 0)
                        {
                            LineNumber = "0000" + Convert.ToString(Convert.ToInt64(dtbl.Rows[0][0].ToString()) + 1);
                        }
                        string TableName = "MasterFile.MachinePMSched";
                        string SQLCommand = "";
                        SQLCommand += "INSERT INTO " + TableName;
                        SQLCommand += " ( [MachineID],[LineNumber],[PMType],[DateTime],[Time],[Priority],[ReportTitle],[FormNo],[Version],[ChecklistForm])";
                        SQLCommand += " VALUES (";
                        SQLCommand += "'" + DetailMachineCode + "','" + LineNumber + "'," + DataValue(e.NewValues["PMType"]) + "," + DataValue(e.NewValues["DateTime"]) + "," + DataValue(e.NewValues["Time"]) + "";
                        SQLCommand += " ," + DataValue(e.NewValues["Priority"]) + "," + DataValue(e.NewValues["ReportTitle"]) + "," + DataValue(e.NewValues["FormNo"]) + "," + DataValue(e.NewValues["Version"]) + "," + DataValue(e.NewValues["ChecklistForm"]) + "";
                        SQLCommand += " )";

                        sdsPMSched.InsertCommand = SQLCommand;
            }
            else
            {
                string title = "Greetings";
                string body = "Welcome to ASPSnippets.com";
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + title + "', '" + body + "');", true);

           
            }
      
            

        }
        protected void gvPMSched_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e) //
        {
            var PMT = DataValue(e.NewValues["PMType"]);
            var OPMT = DataValue(e.OldValues["PMType"]);
            DataTable Pmty = Gears.RetriveData2("select PMType from MasterFile.MachinePMSched WHERE PMType = " + PMT + " and MachineID ='" + MachineID.Text + "'", Session["ConnString"].ToString());
            if (Pmty.Rows.Count > 0 && PMT != OPMT)
            {
                cp.JSProperties["cp_message"] = "PMType:'" + PMT + "' already Exist!";
                cp.JSProperties["cp_success"] = true;
                return;
            }
            if (!IsPostBack) return;

            string TableName = "MasterFile.MachinePMSched";
            string SQLCommand = "";


            SQLCommand += " UPDATE " + TableName;
            SQLCommand += " SET ";

            SQLCommand += "[PMType]=" + DataValue(e.NewValues["PMType"]) + ",";
            SQLCommand += "[DateTime]=" + DataValue(e.NewValues["DateTime"]) + ",";
            SQLCommand += "[Time]=" + DataValue(e.NewValues["Time"]) + ",";
            SQLCommand += "[Priority]=" + DataValue(e.NewValues["Priority"]) + ",";
            SQLCommand += "[ReportTitle]=" + DataValue(e.NewValues["ReportTitle"]) + ",";
            SQLCommand += "[FormNo]=" + DataValue(e.NewValues["FormNo"]) + ",";
            SQLCommand += "[Version]=" + DataValue(e.NewValues["Version"]) + ",";
            SQLCommand += "[ChecklistForm]=" + DataValue(e.NewValues["ChecklistForm"]) + "";

            SQLCommand += " WHERE MachineID= " + DataValue(MachineID.Text) + " AND RecordID=" + DataValue(e.OldValues["RecordID"]) + "";

            sdsPMSched.UpdateCommand = SQLCommand;
        }
        protected void gvPMSched_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e) //
        {
            sdsPMSched.DeleteParameters["DRecordID"].DefaultValue = e.Values["RecordID"].ToString();
        }







        protected void gvStepBOM_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e) //
        {
            if (Request.QueryString["entry"].ToString() != "V")
            {
                if (!IsPostBack) return;
                //var PMMachineCode = (sender as ASPxGridView).GetMasterRowFieldValues("MachineCode").ToString();
                var PMMachineID = (sender as ASPxGridView).GetMasterRowFieldValues("MachineID").ToString();
                var PMLineNumber = (sender as ASPxGridView).GetMasterRowFieldValues("LineNumber").ToString();

                DataTable dtbl = Gears.RetriveData2("Select Count(*) Number FROM MasterFile.MachinePMChecklist WHERE Code ='" + MachineID.Text + "' and PMNumber ='" + PMLineNumber + "' ", Session["ConnString"].ToString());
                string LineNumber = "00001";
                if (dtbl.Rows.Count > 0)
                {
                    LineNumber = "0000" + Convert.ToString(Convert.ToInt64(dtbl.Rows[0][0].ToString()) + 1);
                }
                string TableName = "MasterFile.MachinePMChecklist";
                string SQLCommand = "";
                SQLCommand += "INSERT INTO " + TableName;
                SQLCommand += " ([PMNumber],[LineNumber],[Code],[ActivityTask],[Remarks],[Conformance],[NonConformance])";
                SQLCommand += " VALUES (";
                SQLCommand += "'" + PMLineNumber + "','" + LineNumber + "','" + PMMachineID + "'," + DataValue(e.NewValues["ActivityTask"]) + "," + DataValue(e.NewValues["Remarks"]) + "";
                SQLCommand += " ," + DataValue(e.NewValues["Conformance"]) + "," + DataValue(e.NewValues["Conformance"]) + "";
                SQLCommand += " )";

                sdsBOM.InsertCommand = SQLCommand;
            }
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Inserted Successfully')", true);
        }
        protected void gvStepBOM_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e) //
        {
            if (Request.QueryString["entry"].ToString() != "V")
            {
            if (!IsPostBack) return;

            string TableName = "MasterFile.MachinePMChecklist";
            string SQLCommand = "";


            SQLCommand += " UPDATE " + TableName;
            SQLCommand += " SET ";

            SQLCommand += "[ActivityTask]=" + DataValue(e.NewValues["ActivityTask"]) + ",";
            SQLCommand += "[Conformance]=" + DataValue(e.NewValues["Conformance"]) + ",";
            SQLCommand += "[NonConformance]=" + DataValue(e.NewValues["NonConformance"]) + ",";
            SQLCommand += "[Remarks]=" + DataValue(e.NewValues["Remarks"]) + "";
           
            SQLCommand += " WHERE Code= " + DataValue(MachineID.Text) + " AND RecordID=" + DataValue(e.OldValues["RecordID"]) + "";

            sdsBOM.UpdateCommand = SQLCommand;
                }
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Inserted Successfully')", true);
        }
        protected void gvStepBOM_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e) //
        {
            sdsBOM.DeleteParameters["DRecordID"].DefaultValue = e.Values["RecordID"].ToString();
        }

        //Material
        protected void gvMaterial_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e) //
        {
            if (Request.QueryString["entry"].ToString() != "V")
            {
            if (!IsPostBack) return;
            //var PMCNumber = (sender as ASPxGridView).GetMasterRowFieldValues("LineNumber").ToString();
            //var PMNumber = (sender as ASPxGridView).GetMasterRowFieldValues("PMNumber").ToString();
            //var PMSNumber = (sender as ASPxGridView).GetMasterRowFieldValues("PMSNumber").ToString();
            //var PMCode = (sender as ASPxGridView).GetMasterRowFieldValues("Code").ToString();
            var PMMachineID = (sender as ASPxGridView).GetMasterRowFieldValues("MachineID").ToString();
            var PMLineNumber = (sender as ASPxGridView).GetMasterRowFieldValues("LineNumber").ToString();

            DataTable dtbl = Gears.RetriveData2("Select Count(*) Number FROM MasterFile.MachineMaterialReq WHERE MachineCode ='" + MachineID.Text + "' and PMNumber ='" + PMLineNumber + "' ", Session["ConnString"].ToString());
            string LineNumber = "00001";
            if (dtbl.Rows.Count > 0)
            {
                LineNumber = "0000" + Convert.ToString(Convert.ToInt64(dtbl.Rows[0][0].ToString()) + 1);
            }
            string TableName = "MasterFile.MachineMaterialReq";
            string SQLCommand = "";
            SQLCommand += "INSERT INTO " + TableName;
            SQLCommand += " ( [PMNumber],[LineNumber],[MachineCode],[MaterialType],[Qty],[Remarks])";
            SQLCommand += " VALUES (";
            SQLCommand += "'" + PMLineNumber + "','" + LineNumber + "','" + MachineID.Text + "'," + DataValue(e.NewValues["MaterialType"]) + "," + DataValue(e.NewValues["Qty"]) + "," + DataValue(e.NewValues["Remarks"]) + "";
            SQLCommand += " )";

            sdsMaterial.InsertCommand = SQLCommand;
                }
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Inserted Successfully')", true);

        }
        protected void gvMaterial_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e) //
        {
            if (Request.QueryString["entry"].ToString() != "V")
            {
            if (!IsPostBack) return;

            string TableName = "MasterFile.MachineMaterialReq";
            string SQLCommand = "";


            SQLCommand += " UPDATE " + TableName;
            SQLCommand += " SET ";

            SQLCommand += "[MaterialType]=" + DataValue(e.NewValues["MaterialType"]) + ",";
            SQLCommand += "[Qty]=" + DataValue(e.NewValues["Qty"]) + ",";
            SQLCommand += "[Remarks]=" + DataValue(e.NewValues["Remarks"]) + "";
         

            SQLCommand += " WHERE MachineCode= " + DataValue(MachineID.Text) + " AND RecordID=" + DataValue(e.OldValues["RecordID"]) + "";

            sdsMaterial.UpdateCommand = SQLCommand;
                }
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Inserted Successfully')", true);
        }
        protected void gvMaterial_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e) //
        {
            sdsMaterial.DeleteParameters["DRecordID"].DefaultValue = e.Values["RecordID"].ToString();
        }

        //Tool

        protected void gvTool_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e) //
        {
            if (Request.QueryString["entry"].ToString() != "V")
            {
            if (!IsPostBack) return;
            //var PMCNumber = (sender as ASPxGridView).GetMasterRowFieldValues("LineNumber").ToString();
            //var PMNumber = (sender as ASPxGridView).GetMasterRowFieldValues("PMNumber").ToString();
            //var PMSNumber = (sender as ASPxGridView).GetMasterRowFieldValues("PMSNumber").ToString();
            //var PMCode = (sender as ASPxGridView).GetMasterRowFieldValues("Code").ToString();
            var PMMachineID = (sender as ASPxGridView).GetMasterRowFieldValues("MachineID").ToString();
            var PMLineNumber = (sender as ASPxGridView).GetMasterRowFieldValues("LineNumber").ToString();

            DataTable dtbl = Gears.RetriveData2("Select Count(*) Number FROM MasterFile.MachineToolreq WHERE MachineCode ='" + MachineID.Text + "' and PMNumber ='" + PMLineNumber + "' ", Session["ConnString"].ToString());
            string LineNumber = "00001";
            if (dtbl.Rows.Count > 0)
            {
                LineNumber = "0000" + Convert.ToString(Convert.ToInt64(dtbl.Rows[0][0].ToString()) + 1);
            }
            string TableName = "MasterFile.MachineToolReq";
            string SQLCommand = "";
            SQLCommand += "INSERT INTO " + TableName;
            SQLCommand += " ([PMNumber],[LineNumber],[MachineCode],[ToolType],[Qty],[Remarks])";
            SQLCommand += " VALUES (";
            SQLCommand += "'" + PMLineNumber + "','" + LineNumber + "','" + MachineID.Text + "'," + DataValue(e.NewValues["ToolType"]) + "," + DataValue(e.NewValues["Qty"]) + "," + DataValue(e.NewValues["Remarks"]) + "";
            SQLCommand += " )";

            sdsTool.InsertCommand = SQLCommand;
                }
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Inserted Successfully')", true);


        }
        protected void gvTool_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e) //
        {
            if (Request.QueryString["entry"].ToString() != "V")
            {
            if (!IsPostBack) return;

            string TableName = "MasterFile.MachineToolReq";
            string SQLCommand = "";


            SQLCommand += " UPDATE " + TableName;
            SQLCommand += " SET ";

            SQLCommand += "[ToolType]=" + DataValue(e.NewValues["ToolType"]) + ",";
            SQLCommand += "[Qty]=" + DataValue(e.NewValues["Qty"]) + ",";
            SQLCommand += "[Remarks]=" + DataValue(e.NewValues["Remarks"]) + "";


            SQLCommand += " WHERE MachineCode= " + DataValue(MachineID.Text) + " AND RecordID=" + DataValue(e.OldValues["RecordID"]) + "";

            sdsTool.UpdateCommand = SQLCommand;
            }
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Inserted Successfully')", true);
        }
        protected void gvTool_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e) //
        {
            sdsTool.DeleteParameters["DRecordID"].DefaultValue = e.Values["RecordID"].ToString();
        }











        protected void gvStepMachine_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e) //
        {
            if (!IsPostBack) return;

            var StepSequence = (sender as ASPxGridView).GetMasterRowFieldValues("StepSequence").ToString();
            var StepCode = (sender as ASPxGridView).GetMasterRowFieldValues("StepCode").ToString();

            //sdsMachine.InsertParameters["IHeaderID"].DefaultValue = RecordID.Value;
            sdsMachine.InsertParameters["ISKUCode"].DefaultValue = MachineID.Text;
            sdsMachine.InsertParameters["IStepSequence"].DefaultValue = StepSequence;
            sdsMachine.InsertParameters["IStepCode"].DefaultValue = StepCode;
            sdsMachine.InsertParameters["IMachineType"].DefaultValue = e.NewValues["MachineType"].ToString();
            sdsMachine.InsertParameters["ILocation"].DefaultValue = e.NewValues["Location"].ToString();
            sdsMachine.InsertParameters["IMachineRun"].DefaultValue = e.NewValues["MachineRun"].ToString();
            sdsMachine.InsertParameters["IUnit"].DefaultValue = e.NewValues["Unit"].ToString();
            sdsMachine.InsertParameters["IMachineCapacityQty"].DefaultValue = e.NewValues["MachineCapacityQty"].ToString();
            sdsMachine.InsertParameters["IMachineCapacityUnit"].DefaultValue = e.NewValues["MachineCapacityUnit"].ToString();
            sdsMachine.InsertParameters["ICostPerUnit"].DefaultValue = e.NewValues["CostPerUnit"].ToString();
        }

        protected void gvStepMachine_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e) //
        {
            if (!IsPostBack) return;

            var StepSequence = (sender as ASPxGridView).GetMasterRowFieldValues("StepSequence").ToString();
            var StepCode = (sender as ASPxGridView).GetMasterRowFieldValues("StepCode").ToString();

            sdsMachine.UpdateParameters["ParamURecordID"].DefaultValue = e.OldValues["RecordID"].ToString();

            sdsMachine.UpdateParameters["UStepSequence"].DefaultValue = StepSequence;
            sdsMachine.UpdateParameters["UStepCode"].DefaultValue = StepCode;
            sdsMachine.UpdateParameters["UMachineType"].DefaultValue = e.NewValues["MachineType"].ToString();
            sdsMachine.UpdateParameters["ULocation"].DefaultValue = e.NewValues["Location"].ToString();
            sdsMachine.UpdateParameters["UMachineRun"].DefaultValue = e.NewValues["MachineRun"].ToString();
            sdsMachine.UpdateParameters["UUnit"].DefaultValue = e.NewValues["Unit"].ToString();
            sdsMachine.UpdateParameters["UMachineCapacityQty"].DefaultValue = e.NewValues["MachineCapacityQty"].ToString();
            sdsMachine.UpdateParameters["UMachineCapacityUnit"].DefaultValue = e.NewValues["MachineCapacityUnit"].ToString();
            sdsMachine.UpdateParameters["UCostPerUnit"].DefaultValue = e.NewValues["CostPerUnit"].ToString();
        }
        protected void gvStepMachine_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e) //
        {
            sdsBOM.DeleteParameters["DRecordID"].DefaultValue = e.Values["RecordID"].ToString();
        }

        protected void gvStepManpower_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e) //
        {
            if (!IsPostBack) return;

            var StepSequence = (sender as ASPxGridView).GetMasterRowFieldValues("StepSequence").ToString();
            var StepCode = (sender as ASPxGridView).GetMasterRowFieldValues("StepCode").ToString();

            DataTable PKey = Gears.RetriveData2("SELECT SeriesNumber FROM IT.DocNumberSettings WHERE Module='PRDRTG'", HttpContext.Current.Session["ConnString"].ToString());

            //sdsManpower.InsertParameters["IHeaderID"].DefaultValue = RecordID.Value;
            sdsManpower.InsertParameters["ISKUCode"].DefaultValue = MachineID.Text;
            sdsManpower.InsertParameters["IStepSequence"].DefaultValue = StepSequence;
            sdsManpower.InsertParameters["IStepCode"].DefaultValue = StepCode;
            sdsManpower.InsertParameters["IDesignation"].DefaultValue = e.NewValues["Designation"].ToString();
            sdsManpower.InsertParameters["INoManpower"].DefaultValue = e.NewValues["NoManpower"].ToString();
            sdsManpower.InsertParameters["INoHour"].DefaultValue = e.NewValues["NoHour"].ToString();
            sdsManpower.InsertParameters["IStandardRate"].DefaultValue = e.NewValues["StandardRate"].ToString();
            sdsManpower.InsertParameters["IStandardRateUnit"].DefaultValue = e.NewValues["StandardRateUnit"].ToString();
            sdsManpower.InsertParameters["ICostPerUnit"].DefaultValue = e.NewValues["CostPerUnit"].ToString();
        }
        protected void gvStepManpower_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e) //
        {
            if (!IsPostBack) return;

            var StepSequence = (sender as ASPxGridView).GetMasterRowFieldValues("StepSequence").ToString();
            var StepCode = (sender as ASPxGridView).GetMasterRowFieldValues("StepCode").ToString();

            sdsManpower.UpdateParameters["ParamURecordID"].DefaultValue = e.OldValues["RecordID"].ToString();

            sdsManpower.UpdateParameters["UStepSequence"].DefaultValue = StepSequence;
            sdsManpower.UpdateParameters["UStepCode"].DefaultValue = StepCode;
            sdsManpower.UpdateParameters["UDesignation"].DefaultValue = e.NewValues["Designation"].ToString();
            sdsManpower.UpdateParameters["UNoManpower"].DefaultValue = e.NewValues["NoManpower"].ToString();
            sdsManpower.UpdateParameters["UNoHour"].DefaultValue = e.NewValues["NoHour"].ToString();
            sdsManpower.UpdateParameters["UStandardRate"].DefaultValue = e.NewValues["StandardRate"].ToString();
            sdsManpower.UpdateParameters["UStandardRateUnit"].DefaultValue = e.NewValues["StandardRateUnit"].ToString();
            sdsManpower.UpdateParameters["UCostPerUnit"].DefaultValue = e.NewValues["CostPerUnit"].ToString();
        }
        protected void gvStepManpower_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e) //
        {
            sdsManpower.DeleteParameters["DRecordID"].DefaultValue = e.Values["RecordID"].ToString();
        }

        protected void gvOtherMaterial_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e) //
        {
            if (!IsPostBack) return;

            //var StepSequence = (sender as ASPxGridView).GetMasterRowFieldValues("StepSequence").ToString();
            //var StepCode = (sender as ASPxGridView).GetMasterRowFieldValues("StepCode").ToString();

            DataTable PKey = Gears.RetriveData2("SELECT SeriesNumber FROM IT.DocNumberSettings WHERE Module='PRDRTG'", HttpContext.Current.Session["ConnString"].ToString());

            //sdsOtherMaterials.InsertParameters["IHeaderID"].DefaultValue = RecordID.Value;
            sdsOtherMaterials.InsertParameters["ISKUCode"].DefaultValue = MachineID.Text;
            sdsOtherMaterials.InsertParameters["IItemCode"].DefaultValue = e.NewValues["ItemCode"].ToString();
            //sdsOtherMaterials.InsertParameters["IStepSequence"].DefaultValue = e.NewValues["StepSequence"].ToString();
            sdsOtherMaterials.InsertParameters["IStepCode"].DefaultValue = e.NewValues["StepCode"].ToString();
            sdsOtherMaterials.InsertParameters["IUnit"].DefaultValue = e.NewValues["Unit"].ToString();
            sdsOtherMaterials.InsertParameters["IConsumptionPerProduct"].DefaultValue = e.NewValues["ConsumptionPerProduct"].ToString();
            sdsOtherMaterials.InsertParameters["ITotalConsumption"].DefaultValue = e.NewValues["TotalConsumption"].ToString();
            sdsOtherMaterials.InsertParameters["IPercentageAllowance"].DefaultValue = e.NewValues["PercentageAllowance"].ToString();
            sdsOtherMaterials.InsertParameters["IQtyAllowance"].DefaultValue = e.NewValues["QtyAllowance"].ToString();
            sdsOtherMaterials.InsertParameters["IClientSuppliedMaterial"].DefaultValue = Convert.ToBoolean(e.NewValues["ClientSuppliedMaterial"]).ToString();
            sdsOtherMaterials.InsertParameters["IEstimatedUnitCost"].DefaultValue = e.NewValues["EstimatedUnitCost"].ToString();
            sdsOtherMaterials.InsertParameters["IStandardUsage"].DefaultValue = e.NewValues["StandardUsage"].ToString();
            sdsOtherMaterials.InsertParameters["IStandardUsageUnit"].DefaultValue = e.NewValues["StandardUsageUnit"].ToString();
            sdsOtherMaterials.InsertParameters["IRemarks"].DefaultValue = e.NewValues["Remarks"].ToString() != "" ? e.NewValues["Remarks"].ToString() : "";
        }
        protected void gvOtherMaterial_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e) //
        {
            if (!IsPostBack) return;

            sdsOtherMaterials.UpdateParameters["ParamURecordID"].DefaultValue = e.OldValues["RecordID"].ToString();

            sdsOtherMaterials.UpdateParameters["UStepSequence"].DefaultValue = e.NewValues["StepSequence"].ToString();
            sdsOtherMaterials.UpdateParameters["UStepCode"].DefaultValue = e.NewValues["StepCode"].ToString();
            sdsOtherMaterials.UpdateParameters["UUnit"].DefaultValue = e.NewValues["Unit"].ToString();
            sdsOtherMaterials.UpdateParameters["UConsumptionPerProduct"].DefaultValue = e.NewValues["ConsumptionPerProduct"].ToString();
            sdsOtherMaterials.UpdateParameters["UTotalConsumption"].DefaultValue = e.NewValues["TotalConsumption"].ToString();
            sdsOtherMaterials.UpdateParameters["UPercentageAllowance"].DefaultValue = e.NewValues["PercentageAllowance"].ToString();
            sdsOtherMaterials.UpdateParameters["UQtyAllowance"].DefaultValue = e.NewValues["QtyAllowance"].ToString();
            sdsOtherMaterials.UpdateParameters["UClientSuppliedMaterial"].DefaultValue = Convert.ToBoolean(e.NewValues["ClientSuppliedMaterial"]).ToString();
            sdsOtherMaterials.UpdateParameters["UEstimatedUnitCost"].DefaultValue = e.NewValues["EstimatedUnitCost"].ToString();
            sdsOtherMaterials.UpdateParameters["UStandardUsage"].DefaultValue = e.NewValues["StandardUsage"].ToString();
            sdsOtherMaterials.UpdateParameters["UStandardUsageUnit"].DefaultValue = e.NewValues["StandardUsageUnit"].ToString();
            sdsOtherMaterials.UpdateParameters["URemarks"].DefaultValue = e.NewValues["Remarks"].ToString();
        }
        protected void gvOtherMaterial_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e) //
        {
            sdsOtherMaterials.DeleteParameters["DRecordID"].DefaultValue = e.Values["RecordID"].ToString();
        }

        private string getPKey(string TableName, string PKey, string Keyid) // 04-21-2021 TA Added getUserID
        {
            string Conn = Session["Connstring"].ToString();
            string getSeriesNumber = "SELECT SeriesNumber FROM IT.DocNumberSettings WHERE Module='PRDRTG'";
            string checkIfAvailable = "SELECT " + Keyid + " FROM Production." + TableName + " WHERE " + PKey + "=";
            string Key = "";

            DataTable systemID = Gears.RetriveData2(getSeriesNumber, Conn); // Get DocNumberSettings SeriesNumber Value
            Key = systemID.Rows[0][0].ToString();

            DataTable checkID = Gears.RetriveData2(checkIfAvailable + Key, Conn); // Check if available

            // Loop until available key produce
            while (checkID.Rows.Count > 0)
            {
                Gears.RetriveData2("UPDATE IT.DocNumberSettings SET SeriesNumber+=1 WHERE Module='PRDRTG'", Conn);

                systemID = Gears.RetriveData2(getSeriesNumber, Conn); // Get DocNumberSettings SeriesNumber Value
                Key = systemID.Rows[0][0].ToString();

                checkID = Gears.RetriveData2(checkIfAvailable + Key, Conn); // Check if UserId is available
            }

            return Key;

        }        
    
    }

}