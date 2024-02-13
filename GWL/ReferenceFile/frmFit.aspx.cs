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
using System.Drawing;

namespace GWL
{
    public partial class frmFit : System.Web.UI.Page
    {
        Boolean error = false;//Boolean for grid validation
        Boolean view = false;//Boolean for view state
        Boolean check = false;//Boolean for grid validation
        string Prodcat = "";
        Entity.Fit _Entity = new Fit();//Calls entity Fit
        Entity.Fit.FitSizeDetail _EntityDetail  = new Fit.FitSizeDetail();//Call entity POdetails
        Entity.Fit.MeasurementChartTemplate _EntityDetail1 = new Fit.MeasurementChartTemplate();

        #region page load/entry
        protected void Page_Load(object sender, EventArgs e)
        {
            
            //SetLookupFilter(); -- for datasource filter
            Gears.UseConnectionString(Session["ConnString"].ToString());

            if (Session["AddColumns"] == "1")
                AddColumns();
            if (Request.QueryString["entry"].ToString() != "N" & Session["AddColumns"] == null)
                AddColumns();

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

            gvSizeDetail2.KeyFieldName = "FitCode;SizeCode";

            //Details checking. changes datasourceid of gridview if there's no details for a certain docnumber; to enable adding of details.
            if (Request.QueryString["entry"].ToString() == "V")
            {
                view = true;//sets view mode for entry
            }

            if (!IsPostBack)
            {

                Session["AddColumns"] = null;
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
                        view = true;//sets view mode for entry
                        updateBtn.Text = "Delete";
                        glcheck.ClientVisible = false;
                        break;
                }


                //txtDocnumber.Value = Session["DocNumber"].ToString(); //sets docnumber from session


                //if (Request.QueryString["entry"].ToString() == "N")
                //{


                //}
                //else
                //{
                    //gvBiz.ReadOnly = true;
                    txtFitCode.Value = Request.QueryString["docnumber"].ToString();

                    
                    //_Entity.getdata(gvBiz.Text);
                    //kate 04-27

                    if (Request.QueryString["parameters"].ToString().Split('|')[0] == "CopyTrans")
                    {
                        
                        _Entity.getdata(Request.QueryString["parameters"].ToString().Split('|')[1], Session["ConnString"].ToString());//ADD CONN
                        txtFitCode.Text = "";
        
                    }
                    else
                    {
                        _Entity.getdata(txtFitCode.Text, Session["ConnString"].ToString());//ADD CONN
                        txtFitCode.Text = _Entity.FitCode;
                    }

                    Disable();
                    txtName.Text = _Entity.FitName;
                    gvBrand.Value = _Entity.Brand;
                    gvGender.Value = _Entity.GenderCode;
                    gvProdCat.Value = _Entity.ProductCategory;
                    txtRemarks.Text = _Entity.Remarks;
                    gvWaist.Value = _Entity.Waist;
                    gvFitType.Value = _Entity.FitType;
                   gvSilhouette.Value = _Entity.Silhouette;
                    txtPattern.Value = _Entity.MasterPattern;
                    chkIsInactive.Value = _Entity.IsInactive;
                    gvSizeTemp.Value = _Entity.SizeTemplate;
                    txtSizeCard.Text = _Entity.SizeCard;
                    txtBaseSize.Value = _Entity.StandardSize;
                    //gvItemCat.Value = _Entity.ItemCategoryCode;
                    //txtARTerm.Text = _Entity.ARTerm;
                    //gvProdCat.Value = _Entity.ProductCategory;
                    //// ** Filter ProdCat Code Look ** //
                    //sdsProdCat.SelectCommand = "select ProductCategoryCode,Description,HaveSubCategory,Mnemonics,IsForForecast from Masterfile.ProductCategory where ISNULL(IsInactive,0) = 0 and ProductCategoryCode = '" + gvProdCat.Text + "' ";
                    //// ** ** ** ... //
                    //gvWaist.Value = _Entity.Waist;
                    //// ** Filter Color Look ** //
                    //sdsWaist.SelectCommand = "select WaistCode,Description from Masterfile.Waist where ISNULL(IsInactive,0) = 0 AND WaistCode = '" + gvWaist.Text + "'";
                    //// ** ** ** ... //    
                    ////gvSilhouette.Value = _Entity.Silhouette;
                    //// --- --- //
                    ////sdsSilhouette.SelectCommand = "select SilhouetteCode,Description from Masterfile.Silhouette where ISNULL(IsInactive,0) = 0 AND Silhouette = '" + gvSilhouette.Text + "'";

                    txtActivatedDate.Text = _Entity.ActivatedDate;
                    txtAddedDate.Text = _Entity.AddedDate;
                    txtLastEditedDate.Text = _Entity.LastEditedDate;
                    txtDeActivatedDate.Text = _Entity.DeActivatedDate;
                    txtDeActivatedBy.Text = _Entity.DeActivatedBy;
                    txtActivatedBy.Text = _Entity.ActivatedBy;
                    txtAddedBy.Text = _Entity.AddedBy;
                    txtLastEditedBy.Text = _Entity.LastEditedBy;
                    txtHField1.Text = _Entity.Field1;
                    txtHField2.Text = _Entity.Field2;
                    txtHField3.Text = _Entity.Field3;
                    txtHField4.Text = _Entity.Field4;
                    txtHField5.Text = _Entity.Field5;
                    txtHField6.Text = _Entity.Field6;
                    txtHField7.Text = _Entity.Field7;
                    txtHField8.Text = _Entity.Field8;
                    txtHField9.Text = _Entity.Field9;

                    //SetSizeDetail2();
                    // --- SizeDetail DATA --- //
                    
                    AddColumns();
                    if (!String.IsNullOrEmpty(txtBaseSize.Text))
                    {
                        SetColor();
                    }
                    DataTable checkCount = Gears.RetriveData2("Select FitCode from Masterfile.MeasurementChartTemplate  where fitcode = '" + txtFitCode.Text + "'", Session["ConnString"].ToString());
                    if (checkCount.Rows.Count > 0)
                    {
                        //odsDetail.SelectParameters["docnumber"].DefaultValue = txtFitCode.Text;
                        //gvSizeDetail1.DataSourceID = "odsDetail2";
                        sdsDetail2.SelectCommand = "DECLARE @Sizes VARCHAR(MAX) "
                    + " DECLARE @query VARCHAR(MAX) "

                    + " SELECT @sizes = LEFT(Col1,DATALENGTH(Col1)-1)  FROM ( "
                    + " SELECT TOP 1 STUFF((SELECT DISTINCT '[' + CAST(CONVERT(varchar(10),SizeCode) + '],'  AS VARCHAR(MAX)) "
                    + " FROM Masterfile.MeasurementChartTemplate WHERE FitCode ='" + txtFitCode.Text + "'"
                    + " FOR XML PATH(''), TYPE) .value('.','VARCHAR(MAX)'),1,0,'') AS Col1 "
                    + " FROM Masterfile.MeasurementChartTemplate  WHERE FitCode ='" + txtFitCode.Text + "' ) AS Col "

                    + " SELECT @query = 'SELECT * FROM ( SELECT FitCode, A.POMCode AS [Code],B.Description AS PointofMeasurement, Tolerance,Bracket,A.IsMajor, Grade,Sorting AS [Order],Value, SizeCode  "
                    + " FROM Masterfile.MeasurementChartTemplate A LEFT JOIN Masterfile.POM B ON A.POMCode = B.POMCode WHERE FitCode =''" + txtFitCode.Text + "''"
                    + " ) src  pivot( MAX(Value) for SizeCode in ('+@sizes+ ')) piv;' EXEC (@query)";
                       
                        gvSizeDetail1.DataSourceID = "sdsDetail2";
                        gvSizeDetail1.DataBind();

                    }
                    else
                    {

                        gvSizeDetail1.DataSourceID = "sdsDetail2";


                    }
                    DataTable checkCount1 = Gears.RetriveData2("Select FitCode from Masterfile.FitSizeDetail  where fitcode = '" + txtFitCode.Text + "'", Session["ConnString"].ToString());
                    if (checkCount1.Rows.Count > 0)
                    {
                        gvSizeDetail2.KeyFieldName = "FitCode;SizeCode";
                        odsDetail.SelectParameters["docnumber"].DefaultValue = txtFitCode.Text;
                        gvSizeDetail2.DataSourceID = "odsDetail";
                    }
                    else
                    {
                        gvSizeDetail2.KeyFieldName = "FitCode;SizeCode";
                        gvSizeDetail2.DataSourceID = "sdsDetail";


                    }
                    //DataTable checkSizeDetail1 = Gears.RetriveData2("Select FitCode FROM Masterfile.PISStyleChart WHERE MeasurementChartTemplate = '" + txtFitCode.Text + "'", Session["ConnString"].ToString());
                    //if (checkSizeDetail1.Rows.Count > 0)
                    //{
                    //    sdsDetail2.SelectCommand = "DECLARE @Sizes VARCHAR(MAX) "
                    //+ " DECLARE @query VARCHAR(MAX) "

                    //+ " SELECT @sizes = LEFT(Col1,DATALENGTH(Col1)-1)  FROM ( "
                    //+ " SELECT TOP 1 STUFF((SELECT '[' + CAST(CONVERT(varchar(10),SizeCode) + '],'  AS VARCHAR(MAX)) "
                    //+ " FROM Masterfile.MeasurementChartTemplate WHERE FitCode ='" + txtFitCode.Text + "'"
                    //+ " ORDER BY SizeCode FOR XML PATH(''), TYPE) .value('.','VARCHAR(MAX)'),1,0,'') AS Col1 "
                    //+ " FROM Masterfile.MeasurementChartTemplate  WHERE FitCode ='" + txtFitCode.Text + "' ) AS Col "

                    //+ " SELECT @query = 'SELECT * FROM ( SELECT FitCode, A.POMCode AS [Code],B.Description AS PointofMeasurement, Tolerance,Bracket,A.IsMajor, Grade,Sorting AS [Order],Value, SizeCode  "
                    //+ " FROM Masterfile.MeasurementChartTemplate A LEFT JOIN Masterfile.POM B ON A.POMCode = B.POMCode WHERE FitCode =''" + txtFitCode.Text + "''"
                    //+ " ) src  pivot( MAX(Value) for SizeCode in ('+@sizes+ ')) piv;' EXEC (@query)";
                    //    gvSizeDetail1.DataSource = sdsDetail2;
                    //    gvSizeDetail1.DataBind();
                    //}
                    //else
                    //    gvSizeDetail1.DataSourceID = "sdsDetail2";
                  
                    
                }

            }
        #endregion

        #region Validation
        //private void Validate()
        //{
        //    GearsLibrary.Gears.GearsParameter gparam = new Gears.GearsParameter();
        //    gparam._DocNo = _Entity.BizPartnerCode;
        //    gparam._UserId = Session["Userid"].ToString();
        //    gparam._TransType = Request.QueryString["transtype"].ToString();
        //    //gparam._Factor = 1;
        //    // gparam._Action = "Validate";
        //    //here
        //    string strresult = GWarehouseManagement.OCN_Validate(gparam);
        //    cp.JSProperties["cp_valmsg"] = strresult;//Message variable to client side

        //}
        #endregion

        #region Set controls' state/behavior/etc...
        protected void TextboxLoad(object sender, EventArgs e)//Control for all textbox
        {
            ASPxTextBox text = sender as ASPxTextBox;
            text.ReadOnly = view;
        }
        protected void Comboboxload(object sender, EventArgs e)
        {
            ASPxComboBox combobox = sender as ASPxComboBox;
            combobox.ReadOnly = view;
        }
        protected void LookupLoad(object sender, EventArgs e)//Control for all lookup in header
        {
            var look = sender as ASPxGridLookup;
            if (look != null)
            {
                look.ReadOnly = view;
            }
        }
        protected void CheckBoxLoad(object sender, EventArgs e)
        {
            var check = sender as ASPxCheckBox;
            check.ReadOnly = view;
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
        protected void gv_CommandButtonInitialize1(object sender, ASPxGridViewCommandButtonEventArgs e)
        {   //Sets icon for grid/Control for Enabling/Disabling of buttons on grid.
            var a = sender as ASPxGridView;

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
            if (Request.QueryString["entry"].ToString() == "N" || Request.QueryString["entry"].ToString() == "E")
            {
                if (a.ID == "gvSizeDetail1")
                {
                    if (string.IsNullOrEmpty(txtBaseSize.Text))
                    {
                        if (e.ButtonType == ColumnCommandButtonType.New)
                            e.Visible = false;
                    }
                    else
                    {
                        if (e.ButtonType == ColumnCommandButtonType.New)
                            e.Visible = true;
                    }
                }
                else
                {
                    if (e.ButtonType == ColumnCommandButtonType.New)
                        e.Visible = true;
                }

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
            if (e.ButtonType == ColumnCommandButtonType.Cancel)
                e.Visible = false;
        }

        protected void gv1_CustomButtonInitialize1(object sender, ASPxGridViewCustomButtonEventArgs e)
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
        protected void gv_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
        {   //Sets icon for grid/Control for Enabling/Disabling of buttons on grid.
            var a = sender as ASPxGridView;

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
            if (Request.QueryString["entry"].ToString() == "N" || Request.QueryString["entry"].ToString() == "E")
            {
                if (a.ID == "gvSizeDetail1")
                {
                    if (string.IsNullOrEmpty(txtBaseSize.Text))
                    {
                        if (e.ButtonType == ColumnCommandButtonType.New)
                            e.Visible = false;
                    }
                    else
                    {
                        if (e.ButtonType == ColumnCommandButtonType.New)
                            e.Visible = true;
                    }
                }
                else
                {
                    if (e.ButtonType == ColumnCommandButtonType.New)
                        e.Visible = true;
                }

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
            if (e.ButtonType == ColumnCommandButtonType.Cancel)
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

        #region Lookup Settings

        public void gridView_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            string column = e.Parameters.Split('|')[0];//Set column name
            if (column.Contains("GLP_AIC") || column.Contains("GLP_AC") || column.Contains("GLP_F")) return;//Traps the callback
            string propertynumber = e.Parameters.Split('|')[1];//Set Item Code
            string val = e.Parameters.Split('|')[2];//Set column value
            if (val.Contains("GLP_AIC") || val.Contains("GLP_AC") || val.Contains("GLP_F")) return;//Traps the callback
            var propertynumberlookup = sender as ASPxGridView;
            string codes = "";
            if (e.Parameters.Contains("POMCode"))
            {
                DataTable getDetail = Gears.RetriveData2("SELECT Description FROM Masterfile.POM WHERE POMCode = '" + propertynumber + "'", Session["ConnString"].ToString());//ADD CONN

                foreach (DataRow dt in getDetail.Rows)
                {
                    codes = dt["Description"].ToString() + ";";

                }

                propertynumberlookup.JSProperties["cp_identifier"] = "POMCode";
                propertynumberlookup.JSProperties["cp_codes"] = codes;
            }
        }
        #endregion

        #region Callback functions(Inserting/Updating/Deleting/Validating)
        protected void cp_Callback(object sender, CallbackEventArgsBase e)
        {
            _Entity.Connection = Session["ConnString"].ToString(); //ADD CONN
            _Entity.FitCode = txtFitCode.Text;
            _Entity.FitName = txtName.Text;
            _Entity.Brand = String.IsNullOrEmpty(gvBrand.Text) ? null : gvBrand.Value.ToString();
            _Entity.GenderCode = String.IsNullOrEmpty(gvGender.Text) ? null : gvGender.Value.ToString();
            _Entity.ProductCategory = String.IsNullOrEmpty(gvProdCat.Text) ? null : gvProdCat.Value.ToString();
            _Entity.Remarks = txtRemarks.Text;
            _Entity.Waist = String.IsNullOrEmpty(gvWaist.Text) ? null : gvWaist.Value.ToString();
            _Entity.FitType = String.IsNullOrEmpty(gvFitType.Text) ? null : gvFitType.Value.ToString();
            _Entity.Silhouette = String.IsNullOrEmpty(gvSilhouette.Text) ? null : gvSilhouette.Value.ToString();
            _Entity.MasterPattern = txtPattern.Text;
            _Entity.SizeTemplate = String.IsNullOrEmpty(gvSizeTemp.Text) ? null : gvSizeTemp.Value.ToString();
            _Entity.SizeCard = txtSizeCard.Text;
            _Entity.StandardSize = String.IsNullOrEmpty(txtBaseSize.Text) ? null : txtBaseSize.Value.ToString();

            _Entity.IsInactive = Convert.ToBoolean(chkIsInactive.Checked);
            //_Entity.AddedBy = txtAddedBy.Text;
            //_Entity.AddedDate = txtAddedDate.Text;
            //_Entity.ActivatedBy = txtActivatedBy.Text;
            //_Entity.ActivatedDate = txtActivatedDate.Text;
            //_Entity.DeActivatedBy = txtDeActivatedBy.Text;
            //_Entity.DeActivatedDate = txtDeActivatedDate.Text;
            _Entity.LastEditedBy = Session["userid"].ToString();
            _Entity.LastEditedDate = DateTime.Now.ToString();
           
            _Entity.Field1 = txtHField1.Text;
            _Entity.Field2 = txtHField2.Text;
            _Entity.Field3 = txtHField3.Text;
            _Entity.Field4 = txtHField4.Text;
            _Entity.Field5 = txtHField5.Text;
            _Entity.Field6 = txtHField6.Text;
            _Entity.Field7 = txtHField7.Text;
            _Entity.Field8 = txtHField8.Text;
            _Entity.Field9 = txtHField9.Text;

            Disable();
      
            switch (e.Parameter)
            {
                case "Add":
                    if (error == false)
                    {
                        check = true;
                        //     _Entity.InsertData(_Entity);//Method of inserting for header
                        _Entity.AddedBy = Session["userid"].ToString();

                        _Entity.InsertData(_Entity);

                        //odsDetail2.SelectParameters["docnumber"].DefaultValue = txtFitCode.Text;
                        //gvSizeDetail1.DataSourceID = odsDetail2.ID;
                        gvSizeDetail1.UpdateEdit();

                       
                        gvSizeDetail2.DataSourceID = SizeDetail2DataSource.ID;
                        gvSizeDetail2.UpdateEdit();
                        //gvSizeDetail2.DataSourceID = "odsDetail";
                        //odsDetail.SelectParameters["docnumber"].DefaultValue = txtFitCode.Text;
                        //gvSizeDetail2.UpdateEdit();
                        //Validate();
                        cp.JSProperties["cp_message"] = "Successfully Added!";//Message variable to client side
                        cp.JSProperties["cp_success"] = true;//Success bool variable  to client side
                        cp.JSProperties["cp_close"] = true;//Close window variable to client side
                        Session["Refresh"] = "1";
                        Session["AddColumns"] = null;
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
                        //     _Entity.InsertData(_Entity);//Method of inserting for header
                        _Entity.LastEditedBy = Session["userid"].ToString();

                        _Entity.UpdateData(_Entity);
                        //gvSizeDetail1.DataSourceID = "odsDetail2"; 
                        gvSizeDetail1.UpdateEdit();
                        gvSizeDetail2.DataSourceID = SizeDetail2DataSource.ID;
                        gvSizeDetail2.UpdateEdit();
                        //gvSizeDetail2.DataSourceID = "odsDetail";
                        //odsDetail.SelectParameters["docnumber"].DefaultValue = txtFitCode.Text;
                        //gvSizeDetail2.UpdateEdit();
                        //Validate();
                        cp.JSProperties["cp_message"] = "Successfully Updated!";//Message variable to client side
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
                //case "SizeTempDetail":
                //    GetSelectedVal();
                //    cp.JSProperties["cp_generated"] = true;
                //    break;
                case "fitcodecase":
                    SqlDataSource ds = sdsSizeTemp;
                    ds.SelectCommand = string.Format("select SizeTemplateCode,Description,SizeType from Masterfile.SizeTemplate where ISNULL(IsInactive,0) = 0 and sizetemplatecode = '" + gvSizeTemp.Text + "'");
                    DataView tran = (DataView)ds.Select(DataSourceSelectArguments.Empty);
                    if (tran.Count > 0)
                    {
                        txtSizeCard.Text = tran[0][2].ToString();
                    }
                    //SetFitDetail();
                    SetSizeDetail2();

                    AddColumns();
                    Session["AddColumns"] = "1";

                    Disable();
                    break;
                case "setcase":
                    if (!String.IsNullOrEmpty(txtBaseSize.Text))
                    {
                        SetColor();
                    }
                    Disable();
                    break;
                
                case "ProdCat":
                    Prodcat = gvProdCat.Value.ToString(); //Prodcat

                     gvWaist.Text = "";
                       gvSilhouette.Text = "";
                    
                        gvWaist.ClientEnabled = true;
                        gvSilhouette.ClientEnabled = true;
                    if (Prodcat == "T")
                    {
                        //SqlDataSource Rv = sdsNeckline;
                        gvWaist.KeyFieldName = "Code";
                        //  Rv.SelectCommand = string.Format("select ProductCategoryCode,Description,HaveSubCategory,Mnemonics,IsForForecast from Masterfile.ProductCategory where ISNULL(IsInactive,0) = 0 and ProductCategoryCode != '" + gvProdCat.Text + "'");
                        gvWaist.DataSourceID = "sdsNeckline";
                        gvWaist.DataBind();

                        //SqlDataSource  = sdsNeckline;
                        gvSilhouette.KeyFieldName = "Code";
                        //  Rv.SelectCommand = string.Format("select ProductCategoryCode,Description,HaveSubCategory,Mnemonics,IsForForecast from Masterfile.ProductCategory where ISNULL(IsInactive,0) = 0 and ProductCategoryCode != '" + gvProdCat.Text + "'");
                        gvSilhouette.DataSourceID = "sdsSleeve";
                        gvSilhouette.DataBind();
                        txtFitCode.MaxLength = 4;
                       
                    
                    }
                    else if (Prodcat == "B")
                    {
                       // SqlDataSource bv = sdsWaist;
                        //   Rv.SelectCommand = string.Format(("select ProductCategoryCode,Description,HaveSubCategory,Mnemonics,IsForForecast from Masterfile.ProductCategory where ISNULL(IsInactive,0) = 0 and ProductCategoryCode != '" + gvProdCat.Text + "'");
                        gvWaist.KeyFieldName = "Code";
                        gvWaist.DataSourceID = "sdsWaist";
                        gvWaist.DataBind();

                       // SqlDataSource gv = sdsSilhouette;
                        //   Rv.SelectCommand = string.Format(("select ProductCategoryCode,Description,HaveSubCategory,Mnemonics,IsForForecast from Masterfile.ProductCategory where ISNULL(IsInactive,0) = 0 and ProductCategoryCode != '" + gvProdCat.Text + "'");
                        gvSilhouette.KeyFieldName = "Code";
                        gvSilhouette.DataSourceID = "sdsSilhouette";
                        gvSilhouette.DataBind();
                        txtFitCode.MaxLength = 3;
                    }
                    else
                    {

                        gvWaist.ClientEnabled = false;
                        gvSilhouette.ClientEnabled = false;
                    }
                    

                    break;
          }
        }
        protected void SetLookupFilter()
        {
            sdsProdCat.SelectCommand = "select ProductCategoryCode,Description,HaveSubCategory,Mnemonics,IsForForecast from Masterfile.ProductCategory where ISNULL(IsInactive,0) = 0 and ProductCategoryCode = '" + gvProdCat.Text + "'";
            //sdsWaist.SelectCommand = "SELECT FabricCode, FabricDescription FROM Masterfile.Fabric WHERE FabricGroup = '" + glProductGroup.Text + "' AND FabricSupplier = '" + glFabricSupplier.Text + "' ";
            //sdsSilhouette.SelectCommand = "SELECT FitCode, FitName FROM Masterfile.Fit WHERE ISNULL(IsInactive,0)=0 AND GenderCode = '" + glGender.Text + "' AND ProductCategory = '" + glProductCategory.Text + "' ";
        }


               

         
            void AddError(Dictionary<GridViewColumn, string> errors, GridViewColumn column, string errorText)
        {
            if (errors.ContainsKey(column)) return;
            errors[column] = errorText;
        }
        //protected void gv1_BatchUpdate(object sender, ASPxDataBatchUpdateEventArgs e)
        //{
        //    if ((error == true || error == false) && check == false)//Prevents updating of grid to enable validation
        //    {
        //        e.Handled = true;
        //        //e.DeleteValues.Clear();
        //        e.InsertValues.Clear();
        //        e.UpdateValues.Clear();
        //    }
        //}
        #endregion
                protected void grid_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
        { //Validation for gridview, set all validate conditions here.. (sample here is checking of empty cells)
           

            if (e.Errors.Count > 0)
            {
                error = true; //bool to cancel adding/updating if true
            }
        }
        //dictionary method to hold error 

        //protected void gv1_BatchUpdate(object sender, ASPxDataBatchUpdateEventArgs e)
        //{
        //    if ((error == true || error == false) && check == false)//Prevents updating of grid to enable validation
        //    {
        //        e.Handled = true;
        //        //e.DeleteValues.Clear();
        //        e.InsertValues.Clear();
        //        e.UpdateValues.Clear();
        //    }
        //    if (Session["Datatable"] == "1" && check == true)
        //    {
        //        e.Handled = true;
        //        DataTable source = GetSelectedVal();

        //        // Removing all deleted rows from the data source(Excel file)
        //        foreach (ASPxDataDeleteValues values in e.DeleteValues)
        //        {
        //            try
        //            {
        //                object[] keys = { values.Keys[0], values.Keys[1] };
        //                source.Rows.Remove(source.Rows.Find(keys));
        //            }
        //            catch (Exception)
        //            {
        //                continue;
        //            }
        //        }

        //        // Updating required rows
        //        foreach (ASPxDataUpdateValues values in e.UpdateValues)
        //        {
        //            object[] keys = { values.NewValues["FitCode"], values.NewValues["SizeCode"] };
        //            DataRow row = source.Rows.Find(keys);
                   
        //            row["Field1"] = values.NewValues["Field1"];
        //            row["Field2"] = values.NewValues["Field2"];
        //            row["Field3"] = values.NewValues["Field3"];
        //            row["Field4"] = values.NewValues["Field4"];
        //            row["Field5"] = values.NewValues["Field5"];
        //            row["Field6"] = values.NewValues["Field6"];
        //            row["Field7"] = values.NewValues["Field7"];
        //            row["Field8"] = values.NewValues["Field8"];
        //            row["Field9"] = values.NewValues["Field9"];
        //        }

        //        Gears.RetriveData2("Delete from masterfile.fitsizeDetail where fitcode = '" + txtFitCode.Text + "'", Session["ConnString"].ToString());

        //        foreach (DataRow dtRow in source.Rows)//This is where the data will be inserted into db
        //        {
        //            _EntityDetail.SizeCode = dtRow["SizeCode"].ToString();
        //            _EntityDetail.SizeName = dtRow["SizeName"].ToString();
        //            _EntityDetail.Length = dtRow["Length"].ToString();
        //            _EntityDetail.SortNumber = Convert.ToInt16(string.IsNullOrEmpty(dtRow["SortNumber"].ToString()) ? "" : dtRow["SortNumber"].ToString());

        //            _EntityDetail.Field1 = dtRow["Field1"].ToString();
        //            _EntityDetail.Field2 = dtRow["Field2"].ToString();
        //            _EntityDetail.Field3 = dtRow["Field3"].ToString();
        //            _EntityDetail.Field4 = dtRow["Field4"].ToString();
        //            _EntityDetail.Field5 = dtRow["Field5"].ToString();
        //            _EntityDetail.Field6 = dtRow["Field6"].ToString();
        //            _EntityDetail.Field7 = dtRow["Field7"].ToString();
        //            _EntityDetail.Field8 = dtRow["Field8"].ToString();
        //            _EntityDetail.Field9 = dtRow["Field9"].ToString();

        //            _EntityDetail.AddFitSizeDetail(_EntityDetail);

        //            //_EntityDetail.DeleteOutboundDetail(_EntityDetail);
        //        }
        //    }
        //}


        //private DataTable GetSelectedVal()
        //{
        //    DataTable dt = new DataTable();
        //    string[] selectedValues = gvSizeTemp.Text.Split(';');
        //    CriteriaOperator selectionCriteria = new InOperator(gvSizeTemp.KeyFieldName, selectedValues);
        //    sdsSizeTempDetail.FilterExpression = (GroupOperator.Combine(GroupOperatorType.And, selectionCriteria)).ToString();
        //    Session["RRses"] = sdsSizeTempDetail.FilterExpression;
        //    gv1.DataSourceID = sdsSizeTempDetail.ID;
        //    gv1.DataBind();
        //    Session["Datatable"] = "1";

        //    foreach (GridViewColumn col in gv1.VisibleColumns)
        //    {
        //        GridViewDataColumn dataColumn = col as GridViewDataColumn;
        //        if (dataColumn == null) continue;
        //        dt.Columns.Add(dataColumn.FieldName);
        //    }
        //    for (int i = 0; i < gv1.VisibleRowCount; i++)
        //    {
        //        DataRow row = dt.Rows.Add();
        //        foreach (DataColumn col in dt.Columns)
        //            row[col.ColumnName] = gv1.GetRowValues(i, col.ColumnName);
        //    }

        //    dt.PrimaryKey = new DataColumn[] { //Sets datatable's primary key for batchupdate reference during adding.
        //    dt.Columns["DocNumber"],dt.Columns["LineNumber"]};


        //    return dt;
        //}

        //protected void gv1_Init(object sender, EventArgs e)
        //{

        //    if (!IsPostBack)
        //    {
        //        Session["RRses"] = null;
        //    }

        //    if (Session["RRses"] != null)
        //    {
        //        gv1.DataSource = sdsSizeTempDetail;
        //        sdsSizeTempDetail.FilterExpression = Session["RRses"].ToString();
        //        //gridview.DataSourceID = null;
        //    }
        //}

                    protected void Disable()
                    {
                     if (gvProdCat.Text.ToString() == "T" || gvProdCat.Text.ToString() == "B")
                    {
                        gvWaist.Value = null;
                        gvSilhouette.Value = null;
                        gvWaist.ClientEnabled = true;
                        gvSilhouette.ClientEnabled = true;
                        //glRef.SetValue(null);

                    }
                    else
                    {
                        gvWaist.ClientEnabled = false;
                        gvSilhouette.ClientEnabled = false;
                        gvWaist.Value = null;
                        gvSilhouette.Value = null;
                    }
                 }
                protected void SetColor()
                {
                    try
                    {
                        gvSizeDetail1.Columns[txtBaseSize.Text.TrimEnd()].HeaderStyle.BackColor = ColorTranslator.FromHtml("#66ff33");
                        gvSizeDetail1.Columns[txtBaseSize.Text.TrimEnd()].CellStyle.BackColor = ColorTranslator.FromHtml("#66ff33");
                    }
                    catch (Exception e)
                    {

                    }


                }
                protected void glPOMCode_Init(object sender, EventArgs e)
                {
                    ASPxGridLookup gridLookup = sender as ASPxGridLookup;
                    gridLookup.GridView.CustomCallback += new ASPxGridViewCustomCallbackEventHandler(gridView_CustomCallback);
                    if (Session["FilterExpression"] != null)
                    {
                        gridLookup.GridView.DataSourceID = "sdsPOM";
                        //AssetAcquisitionLookup.FilterExpression = Session["FilterExpression"].ToString();
                        //Session["FilterExpression"] = null;
                    }
                    else
                    {
                        gridLookup.GridView.DataSourceID = "sdsPOM";
                    }
                }

          private DataTable SetSizeDetail2()
          {

              gvSizeDetail2.DataSourceID = null;
              DataTable dt = new DataTable();
              SizeDetail2DataSource.SelectCommand = "SELECT RIGHT('00000'+ CAST(ROW_NUMBER() OVER (ORDER BY SizeCode) AS VARCHAR(5)),5) AS LineNumber "
                                                              + ",'' as FitCode,SizeCode, SizeName, Length AS Length, SortNumber FROM Masterfile.SizeTemplateDetail"
                                                              + " WHERE SizeTemplateCode = '" + gvSizeTemp.Text + "' ";
              gvSizeDetail2.DataSource = SizeDetail2DataSource;
              gvSizeDetail2.DataBind();


              Session["Datatable"] = "1";
              foreach (GridViewColumn col in gvSizeDetail2.VisibleColumns)
              {
                  GridViewDataColumn dataColumn = col as GridViewDataColumn;
                  if (dataColumn == null) continue;
                  dt.Columns.Add(dataColumn.FieldName);
              }
              for (int i = 0; i < gvSizeDetail2.VisibleRowCount; i++)
              {
                  DataRow row = dt.Rows.Add();
                  foreach (DataColumn col in dt.Columns)
                      row[col.ColumnName] = gvSizeDetail2.GetRowValues(i, col.ColumnName);
              }

              dt.PrimaryKey = new DataColumn[] { //Sets datatable's primary key for batchupdate reference during adding.
               dt.Columns["SizeCode"]};

              //glJobOrder.ClientEnabled = false;

              return dt;
          }





        protected void AddColumns()
        {
            gvSizeDetail1.AutoGenerateColumns = false; 

            DataTable getSizeCode = Gears.RetriveData2("SELECT SizeCode from masterfile.SizeTemplateDetail WHERE SizeTemplateCode = '" + gvSizeTemp.Text.TrimEnd() + "'", Session["ConnString"].ToString());//ADD CONN

            int x = 9;
            foreach (DataRow dt in getSizeCode.Rows)
            {
                
                GridViewDataSpinEditColumn spin = new GridViewDataSpinEditColumn();
                spin.FieldName = dt[0].ToString().TrimEnd();
                spin.VisibleIndex = x;
                spin.Width = 50;
                spin.ReadOnly = false;
                spin.PropertiesSpinEdit.SpinButtons.ShowIncrementButtons = false;
                spin.UnboundType = DevExpress.Data.UnboundColumnType.Integer;
                spin.PropertiesSpinEdit.ClientSideEvents.ValueChanged = "computesizesvalue";
                gvSizeDetail1.Columns.Add(spin);
                x++;

                Session["RowHeader"] = dt[0].ToString() + ";";
            }
          
            gvSizeDetail1.DataBind();

            

        }

        protected void glWarehouseCOde_TextChanged(object sender, EventArgs e)
        {

        }

        protected void txtDocnumber_TextChanged(object sender, EventArgs e)
        {

        }

        protected void glBizPartnerCode_TextChanged(object sender, EventArgs e)
        {

        }

        protected void txtpickType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void txtPlant_TextChanged(object sender, EventArgs e)
        {

        }
        protected void gvSizeDetail1_BatchUpdate(object sender, ASPxDataBatchUpdateEventArgs e)
        {
            if ((error == true || error == false) && check == false)//Prevents updating of grid to enable validation
            {
                e.Handled = true;
                e.UpdateValues.Clear();
                e.InsertValues.Clear();

            }

            if (check == true)
            {
                e.Handled = true;
                DataTable source = new DataTable();
                source.Columns.Add("LineNumber", typeof(string));
                source.Columns.Add("FitCode", typeof(string));
                source.Columns.Add("Sorting", typeof(int));
                source.Columns.Add("IsMajor", typeof(bool));
                source.Columns.Add("POMCode", typeof(string));
                source.Columns.Add("SizeCode", typeof(string));
                source.Columns.Add("Value", typeof(string));
                source.Columns.Add("Tolerance", typeof(string));
                source.Columns.Add("Bracket", typeof(int));
                source.Columns.Add("Grade", typeof(string));
                //source.Columns.Add("Column2", typeof(string));
                DataTable getSizeCode = Gears.RetriveData2("SELECT SizeCode FROM masterfile.SizeTemplateDetail WHERE SizeTemplateCode = '" + gvSizeTemp.Text + "' ORDER BY SizeCode", Session["ConnString"].ToString());//ADD CONN

                int i = 0;
                foreach (ASPxDataInsertValues values in e.InsertValues)
                {
                    foreach (DataRow dt in getSizeCode.Rows)
                    {
                        var LineNumber = i;
                        var FitCode = "Temp";
                        var Sorting = values.NewValues["Order"];
                        var IsMajor = values.NewValues["IsMajor"];
                        var POMCode = values.NewValues["Code"];
                        var SizeCode = dt[0].ToString();
                        var Value = values.NewValues[dt[0].ToString()];
                        var Tolerance = values.NewValues["Tolerance"];
                        var Bracket = values.NewValues["Bracket"];
                        var Grade = values.NewValues["Grade"];

                        source.Rows.Add(LineNumber, FitCode, Sorting,IsMajor, POMCode, SizeCode, Value, Tolerance, Bracket, Grade);

                        i++;
                    }
                }

                //foreach (ASPxDataUpdateValues values in e.UpdateValues)
                //{
                //    object[] keys = { values.NewValues["FitCode"], values.NewValues["LineNumber"] };
                //    DataRow row = source.Rows.Find(keys);

                //    row["FitCode"] = values.NewValues["FitCode"];
                //    row["IsMajor"] = values.NewValues["IsMajor"];
                //    row["POMCode"] = values.NewValues["POMCode"];
                //    row["SizeCode"] = values.NewValues["SizeCode"];
                //    row["Value"] = values.NewValues["Value"];
                //    row["Tolerance"] = values.NewValues["Tolerance"];
                //    row["Bracket"] = values.NewValues["Bracket"];
                //    row["Grade"] = values.NewValues["Grade"];
                //    row["Sorting"] = values.NewValues["Sorting"];


                //}
                //Gears.RetriveData2("DELETE FROM Masterfile.MeasurementChartTemplate where FitCode='" + txtFitCode.Text + "'", Session["ConnString"].ToString());

                foreach (DataRow dtRow in source.Rows)//This is where the data will be inserted into db
                {
                    //_EntityDetail.PRNumber = dtRow["PRNumber"].ToString();
                    _EntityDetail1.LineNumber = dtRow["LineNumber"].ToString();
                    _EntityDetail1.FitCode = dtRow["FitCode"].ToString();
                    _EntityDetail1.IsMajor = Convert.ToBoolean(dtRow["IsMajor"].ToString());
                    _EntityDetail1.POMCode = dtRow["POMCode"].ToString();
                    _EntityDetail1.SizeCode = dtRow["SizeCode"].ToString();
                    _EntityDetail1.Value = dtRow["Value"].ToString();
                    _EntityDetail1.Tolerance = dtRow["Tolerance"].ToString();
                    _EntityDetail1.Bracket = dtRow["Bracket"].ToString();
                    _EntityDetail1.Grade = dtRow["Grade"].ToString();
                    _EntityDetail1.Sorting = dtRow["Sorting"].ToString();
                    //_EntityDetail.UnitCost = Convert.ToDecimal(Convert.IsDBNull(dtRow["UnitCost"]) ? 0 : dtRow["UnitCost"]);
                    //_EntityDetail.ReceivedQty = Convert.ToDecimal(Convert.IsDBNull(dtRow["QtyCount"]) ? 0 : dtRow["QtyCount"]);

                    _EntityDetail1.AddMeasurementChartTemplate(_EntityDetail1);
                }



       
            }
        }
        protected void Connection_Init(object sender, EventArgs e)
        {

            sdsWaist.ConnectionString = Session["ConnString"].ToString();
            sdsBrand.ConnectionString = Session["ConnString"].ToString();
            sdsGender.ConnectionString = Session["ConnString"].ToString();
            sdsFitType.ConnectionString = Session["ConnString"].ToString();
            sdsProdCat.ConnectionString = Session["ConnString"].ToString();
            sdsSilhouette.ConnectionString = Session["ConnString"].ToString();
            sdsSizeTemp.ConnectionString = Session["ConnString"].ToString();
            sdsPOM.ConnectionString = Session["ConnString"].ToString();
            sdsSizeCode.ConnectionString = Session["ConnString"].ToString();
            sdsSizeTempDetail.ConnectionString = Session["ConnString"].ToString();
            sdsSleeve.ConnectionString = Session["ConnString"].ToString();
            sdsNeckline.ConnectionString = Session["ConnString"].ToString();
            SizeDetail2DataSource.ConnectionString = Session["ConnString"].ToString();

        }

        protected void gv1_BatchUpdate(object sender, ASPxDataBatchUpdateEventArgs e)
        {
            if ((error == true || error == false) && check == false)//Prevents updating of grid to enable validation
            {
                e.Handled = true;
                e.UpdateValues.Clear();
            }

            if (Session["Datatable"] == "1" && check == true)
            {
                e.Handled = true;
                DataTable source = SetSizeDetail2();

                // Removing all deleted rows from the data source(Excel file)
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

                foreach (ASPxDataInsertValues values in e.InsertValues)//for insert chu chu
                {
                    var SizeCode = values.NewValues["SizeCode"];
                    var SizeName = values.NewValues["SizeName"];
                    var InseamLength = values.NewValues["InseamLength"];
                    var SortNumber = values.NewValues["SortNumber"];

                    source.Rows.Add(SizeCode, SizeName, InseamLength, SortNumber);
                }

                //Updating required rows
                foreach (ASPxDataUpdateValues values in e.UpdateValues)
                {
                    object[] keys = { values.NewValues["SizeCode"] };
                    DataRow row = source.Rows.Find(keys);

                    //row["FitCode"] = values.NewValues["FitCode"];
                    row["SizeCode"] = values.NewValues["SizeCode"];
                    row["SizeName"] = values.NewValues["SizeName"];
                    row["InseamLength"] = values.NewValues["InseamLength"];

                    row["SortNumber"] = values.NewValues["SortNumber"];
                    //row["SizeTemplateCode"] = values.NewValues["SizeTemplateCode"];

                }
                Gears.RetriveData2("DELETE FROM Masterfile.FitSizeDetail where FitCode='" + txtFitCode.Text + "'", Session["ConnString"].ToString());
       
                   foreach (DataRow dtRow in source.Rows)//This is where the data will be inserted into db
                {
                    //_EntityDetail.SizeName = dtRow["FitCode"].ToString();
                    _EntityDetail.SizeCode = dtRow["SizeCode"].ToString();
                    _EntityDetail.Length = dtRow["Length"].ToString();

                    _EntityDetail.SortNumber = Convert.ToInt32(Convert.IsDBNull(dtRow["SortNumber"]) ? 0 : dtRow["SortNumber"]);
                     _EntityDetail.SizeName = dtRow["SizeName"].ToString();
                   
       
                    _EntityDetail.AddFitSizeDetail(_EntityDetail);
                }
            }
        }

   
    }
}