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
using DevExpress.Data.Filtering;
using System.Data.SqlClient;

namespace GWL
{
    public partial class frmTaxRecording : System.Web.UI.Page
    {
        Boolean view = false;       //Boolean for view state

        APMemo _Entity = new APMemo();
        private static string Connection;

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

            if (referer == null)
            {
                Response.Redirect("~/error.aspx");
            }

            if (!IsPostBack)
            {
                Connection = Session["ConnString"].ToString();
                Session["Datatable"] = null; 
                String strEntry = Request.QueryString["entry"].ToString(); 
                 
                SqlDataSource ds = ForInit; 
                ds.SelectCommand = string.Format(" DECLARE @DocDate DATETIME = (SELECT Value FROM IT.SystemSettings WHERE Code = 'BOOKDATE') " + 
                    "SELECT @DocDate AS DateFrom, DATEADD(s,-1,DATEADD(mm, DATEDIFF(m,0,@DocDate)+1,0)) AS DateTo");
                DataView inb = (DataView)ds.Select(DataSourceSelectArguments.Empty);
                if (inb.Count > 0)
                {
                    dtpDateFrom.Value = Convert.ToDateTime(inb[0][0]);
                    dtpDateTo.Value = Convert.ToDateTime(inb[0][1]);
                } 

                updateBtn.Text = "Close"; 
            }
        }
        #endregion

        #region Set controls' state/behavior/etc...
        protected void Textbox_Load(object sender, EventArgs e)//Control for all textbox
        {
            ASPxTextBox text = sender as ASPxTextBox;
            text.ReadOnly = view;
        }
        protected void Lookup_Load(object sender, EventArgs e)//Control for all lookup in header
        {
            ASPxGridLookup lookup = sender as ASPxGridLookup;
            lookup.ReadOnly = view;
        }
        protected void Date_Load(object sender, EventArgs e)//Control for all date editor
        {
            ASPxDateEdit date = sender as ASPxDateEdit;
            date.ReadOnly = view;
            if (view) { date.ClearButton.Visibility = AutoBoolean.False; }
        }
        protected void SpinEdit_Load(object sender, EventArgs e)//Control for all numeric entries in header
        {
            ASPxSpinEdit spinedit = sender as ASPxSpinEdit;
            spinedit.ReadOnly = view;
        }
        #endregion

        protected void Checkbox_Load(object sender, EventArgs e)
        {
            ASPxCheckBox checkbox = sender as ASPxCheckBox;
            checkbox.ReadOnly = view;
        }

        protected void Combobox_Load(object sender, EventArgs e)
        {
            ASPxComboBox combobox = sender as ASPxComboBox;
            combobox.ReadOnly = view;
            if (view) { combobox.ClearButton.Visibility = AutoBoolean.False; }
        }

        protected void Memo_Load(object sender, EventArgs e)//Control for all memo
        {
            ASPxMemo memo = sender as ASPxMemo;
            memo.ReadOnly = view;
        }

        #region Lookup Settings

        #endregion

        #region Callback functions(Inserting/Updating/Deleting/Validating)
        protected void cp_Callback(object sender, CallbackEventArgsBase e)
        {
            if (e.Parameter != "Close" && glSupplierCode.Value == null) {
                cp.JSProperties["cp_message"] = "Please check all fields";
            }
            else if (e.Parameter == "Close")
            {
                cp.JSProperties["cp_close"] = true;
            }
            else if (e.Parameter == "GenerateAP")
            {  
                sdsCheckDetail.SelectParameters["SupplierCode"].DefaultValue = glSupplierCode.Value.ToString();
                sdsCheckDetail.SelectParameters["DateFrom"].DefaultValue = Convert.ToDateTime(dtpDateFrom.Value).ToShortDateString();
                sdsCheckDetail.SelectParameters["DateTo"].DefaultValue = Convert.ToDateTime(dtpDateTo.Value).ToShortDateString();
                sdsCheckDetail.DataBind();
                gv1.DataSource = sdsCheckDetail;
                gv1.DataBind(); 
            }
            else if (e.Parameter == "CreateAP")
            {
                gv1.KeyFieldName = "APNo";
                gv1.UpdateEdit();
                cp.JSProperties["cp_success"] = true;
                cp.JSProperties["cp_message"] = "Successfully Created!";
            }
        }
        protected void grid_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e) { }
        protected void CreateAPMemo_BatchUpdate(object sender, ASPxDataBatchUpdateEventArgs e)
        {
            
            ASPxGridView Grid = sender as ASPxGridView; 
            //e.Handled = true; 
            if (Grid.ID == "gv1")
            { 
                e.Handled = true;  
                DataTable source = Gears.RetriveData2("DECLARE @ATCCode VARCHAR(20), @ATCRate DECIMAL(15,2) "  +
                                                      "SELECT @ATCCode = A.ATCCode, @ATCRate = ISNULL(B.Rate,0) " +
                                                      "FROM Masterfile.BPSupplierInfo A " +
                                                      "INNER JOIN Masterfile.ATC B " +
                                                      "ON A.ATCCode = b.ATCCode " +
                                                      "WHERE SupplierCode = '" + glSupplierCode.Value.ToString() + "'" +
                                                       
                                                      "SELECT CAST(0 AS BIT) AS SelectData, " +
                                                               "A.DocNumber AS APNo, " +
                                                               "DocDate, " +
                                                               "SUBSTRING(ReferenceNumber, 0, LEN(ReferenceNumber) - LEN(LEFT(ReferenceNumber, CHARINDEX ('/', ReferenceNumber))) - LEN(RIGHT(ReferenceNumber, LEN(ReferenceNumber) - CHARINDEX (':', ReferenceNumber)))) AS RefNo, " +
                                                               "TotalAPAmount AS TotalAmount, " +
                                                               "CAST('' AS VARCHAR(20))  AS InvoiceNo, " +
                                                               "CAST(ISNULL(TotalAPAmount,0.00) AS DECIMAL(15,2)) AS WTaxBase, " +
                                                               "CAST(0.00 AS DECIMAL(15,2)) AS WTaxAmount, " +
                                                               "@ATCCode AS ATC, " +
                                                               "ISNULL(@ATCRate,0) * 100 AS ATCRate " +
                                                       "INTO #TER FROM Accounting.APVoucher A " +
                                                       "INNER JOIN Accounting.APVoucherDetail B " +
                                                       "ON A.DocNumber = B.DocNumber WHERE DocDate BETWEEN '" +
                                                        Convert.ToDateTime(dtpDateFrom.Value).ToShortDateString() + "' AND '" +
                                                        Convert.ToDateTime(dtpDateTo.Value).ToShortDateString() + "' AND SupplierCode = '" + 
                                                        glSupplierCode.Value.ToString() + "' ORDER BY DocDate DESC ,A.DocNumber DESC " +
 
                                                       "SELECT A.*, B.DocNumber " +
                                                       "INTO #LEO FROM #TER A " +
                                                       "LEFT JOIN Accounting.APMemo B " +
                                                       "ON A.APNo = B.ReferenceDocnumber " +
                        
                                                       "SELECT SelectData, APNo, DocDate, RefNo, " +
						                               "	   TotalAmount, InvoiceNo, WTaxBase, " +
                                                       "	   WTaxAmount, ATC, ATCRate " +
						                               "FROM #LEO WHERE DocNumber IS NULL " +
                        
                                                       "DROP TABLE #LEO, #TER"
                                                    , Session["ConnString"].ToString());

                source.PrimaryKey = new DataColumn[] { source.Columns["APNo"] };
                foreach (System.Data.DataColumn col in source.Columns) col.ReadOnly = false; 

                foreach (ASPxDataUpdateValues values in e.UpdateValues)
                {
                    object[] keys = {values.NewValues["APNo"]};
                    DataRow row = source.Rows.Find(keys);
                    row["SelectData"] = values.NewValues["SelectData"]; 
                    row["InvoiceNo"] = values.NewValues["InvoiceNo"];
                    row["WTaxBase"] = values.NewValues["WTaxBase"];
                    row["WTaxAmount"] = values.NewValues["WTaxAmount"];
                    row["ATC"] = values.NewValues["ATC"];
                    row["ATCRate"] = values.NewValues["ATCRate"]; 
                }

                SqlDataSource getbookdate = ForInit;
                DateTime bookdate = new DateTime();
                getbookdate.SelectCommand = string.Format("SELECT Value FROM IT.SystemSettings WHERE Code = 'BOOKDATE'");
                DataView bd = (DataView)getbookdate.Select(DataSourceSelectArguments.Empty);
                if (bd.Count > 0)
                {
                    bookdate = Convert.ToDateTime(bd[0][0]); 
                }
                foreach (DataRow dtRow in source.Rows) 
                {
                    if (Convert.ToBoolean(dtRow["SelectData"]) == true)
                    { 
                        String NewDocNum = ""; 
                        SqlDataSource ds = ForInit;
                        ds.SelectCommand = string.Format("DECLARE @Prefix VARCHAR(20), @SeriesWidth INT, @SeriesNumber INT " +
                                                         "SELECT @Prefix = Prefix , @SeriesWidth = SeriesWidth, @SeriesNumber = SeriesNumber " +
                                                         "FROM IT.DocNumberSettings WHERE Module = 'ACTAPM' AND ISNULL(IsDefault,0) = 1 " +
                                                         "UPDATE IT.DocNumberSettings SET SeriesNumber = SeriesNumber + 1 WHERE Module = 'ACTAPM' AND ISNULL(IsDefault,0) = 1 " +
                                                         "SELECT @Prefix+RIGHT(REPLICATE('0',@SeriesWidth)+CONVERT(varchar,@SeriesNumber+1),@SeriesWidth) AS DocNumber ");
                        DataView inb = (DataView)ds.Select(DataSourceSelectArguments.Empty);
                        if (inb.Count > 0)
                        {
                            NewDocNum = inb[0][0].ToString();
                        }

                        _Entity.Connection = Connection;
                        _Entity.DocNumber = NewDocNum;
                        _Entity.DocDate = (Convert.ToDateTime(dtRow["DocDate"]) > Convert.ToDateTime(bookdate)) ? Convert.ToDateTime(dtRow["DocDate"]).ToShortDateString() : bookdate.ToShortDateString();
                        _Entity.SupplierCode = glSupplierCode.Text;
                        _Entity.SupplierName = txtSupplierDesc.Text;
                        _Entity.Type = "TA";
                        _Entity.ReferenceDocnumber = dtRow["APNo"].ToString();
                        _Entity.ReferenceDate = Convert.ToDateTime(dtRow["DocDate"]).ToShortDateString();
                        _Entity.TotalAmount = Convert.ToDecimal(string.IsNullOrEmpty(dtRow["WTaxAmount"].ToString()) ? "0" : dtRow["WTaxAmount"].ToString()) * -1;
                        _Entity.TotalGrossAmount = Convert.ToDecimal(string.IsNullOrEmpty(dtRow["WTaxAmount"].ToString()) ? "0" : dtRow["WTaxAmount"].ToString()) * -1;
                        _Entity.TotalVatAmount = 0; //Convert.ToDecimal(string.IsNullOrEmpty(dtRow["WTaxAmount"].ToString()) ? "0" : dtRow["WTaxAmount"].ToString());
                        _Entity.Remarks = "Withholding tax adjustment for document: " + dtRow["InvoiceNo"].ToString() + "/" +
                                          NewDocNum + "/" + dtRow["TotalAmount"].ToString() + "/" +
                                          dtRow["ATCRate"].ToString() + "/" + dtRow["ATC"].ToString() + "/" +
                                          dtRow["WTaxAmount"].ToString() + " for the period of " +
                                          Convert.ToDateTime(dtpDateFrom.Value).ToShortDateString() + " AND " +
                                          Convert.ToDateTime(dtpDateTo.Value).ToShortDateString() + ".";
                        _Entity.AddedBy = Session["userid"].ToString();
                        _Entity.AddedDate = DateTime.Now.ToString(); 
                        _Entity.InsertDataTAX(_Entity);

                        Gears.RetriveData2("exec sp_GLEntryAPMemo '" + NewDocNum + "','" + Session["userid"].ToString() + "','Accounting.APMemo','ACTAPM',-1", Session["ConnString"].ToString());
                    }
                }
                
                sdsCheckDetail.SelectParameters["SupplierCode"].DefaultValue = glSupplierCode.Value.ToString();
                sdsCheckDetail.SelectParameters["DateFrom"].DefaultValue = Convert.ToDateTime(dtpDateFrom.Value).ToShortDateString();
                sdsCheckDetail.SelectParameters["DateTo"].DefaultValue = Convert.ToDateTime(dtpDateTo.Value).ToShortDateString();
                sdsCheckDetail.DataBind();
                gv1.DataSource = sdsCheckDetail; 
            } 
        }

        protected void Connection_Init(object sender, EventArgs e)
        {
            ((SqlDataSource)sender).ConnectionString = Session["ConnString"].ToString();
        }
        #endregion
    }
}