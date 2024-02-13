using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;
using DevExpress.Web.ASPxPivotGrid;
using DevExpress.XtraPivotGrid;
using System.Data;
using GearsLibrary;
using System.Globalization;
using DevExpress.Export;
using DevExpress.Utils;
using DevExpress.XtraPrinting;
using DevExpress.Data.Filtering;
using DevExpress.ChartRangeControlClient.Core;
using DevExpress.Spreadsheet;
using DevExpress.Spreadsheet.Export;
using Common;
using Newtonsoft.Json;

namespace GWL.Sales
{
    public partial class frmSalesForecast : System.Web.UI.Page
    {
        //protected override void OnLoad(EventArgs e)
        //{
        //    base.OnLoad(e);  
        //}
        private static string Connection;
        protected void Page_Load(object sender, EventArgs e)
        {
                Connection = Session["ConnString"].ToString();

                versionsql.SelectCommand = "select distinct version,itemcategorycode,year from sales.forecast where year = '" + year.Text + "' and itemcategorycode = '" + txtitemcat.Text + "'";
                txtver.DataBind();

                sql.SelectParameters["year"].DefaultValue = year.Text;
                sql.SelectParameters["itemcategory"].DefaultValue = txtitemcat.Value == null ? "" : txtitemcat.Text;

                sql2.SelectParameters["version"].DefaultValue = txtver.Value == null ? "" : (txtver.Text == "" ? txtver.Value.ToString() : txtver.Text);
                sql2.SelectParameters["version"].DefaultValue = txtver.Text;
                sql2.SelectParameters["agent"].DefaultValue = txtagent.Text == "" ? "none" : txtagent.Text;
                sql2.SelectParameters["year"].DefaultValue = year.Text;
                sql2.SelectParameters["itemcategory"].DefaultValue = txtitemcat.Text;
                sql2.SelectParameters["startmonth"].DefaultValue = startm.Text;
                sql2.SelectParameters["groupby"].DefaultValue = cbgroup.Checked.ToString();

                ENT_FWorksheet.SelectParameters[0].DefaultValue = year.Text;
                ENT_FWorksheet.SelectParameters[1].DefaultValue = txtitemcat.Text;
                ENT_FWorksheet.SelectParameters[2].DefaultValue = txtver.Text;

                forecastsumm.SelectParameters[0].DefaultValue = year.Text;
                forecastsumm.SelectParameters[1].DefaultValue = txtitemcat.Text;
                forecastsumm.SelectParameters[2].DefaultValue = txtver.Text;
                forecastsumm2.SelectParameters[0].DefaultValue = year.Text;
                
            
            if (!IsPostBack)
            {
                ASPxPivotGrid1.CollapseAll();
                Session["sql"] = null;
                Session["forecast"] = null;

            }
            //ASPxPivotGrid1.CellTemplate = new CellTemplate();
                //ASPxPivotGrid1.Fields.Clear();
            if (IsCallback)
            if (Session["sql"] != null && !Request.Params["__CALLBACKPARAM"].Contains("generate"))
            {
                //ASPxPivotGrid1.DataSourceID = Session["sql"].ToString();
                ASPxPivotGrid1.DataSource = Session["sql"];
                //ASPxPivotGrid1.DataBind();
                updatefield();
            }

            //if (Request.Params["__EVENTTARGET"] != null)
            //    if (Session["sql"] != null && Request.Params["__EVENTTARGET"].Contains("cp$frmlayout1$PC_4$btnexp"))
            //{
            //    ASPxPivotGrid1.DataSourceID = Session["sql"].ToString();
            //    updatefield();
            //    ASPxPivotGrid1.DataBind();
            //}
                
                //ASPxPivotGrid1.DataBind();
        }
        protected void pivotGrid_CustomCallback(object sender, DevExpress.Web.ASPxPivotGrid.PivotGridCustomCallbackEventArgs e)
        {
            //ASPxPivotGrid1.DataSourceID = Session["sql"].ToString();
            //ASPxPivotGrid1.DataBind();
            ASPxPivotGrid pivot = (ASPxPivotGrid)sender;
            string[] args = e.Parameters.Split('|');
            int colIndex = int.Parse(args[1]);
            int rowIndex = int.Parse(args[2]);
            ChangeCellValue(
                    pivot.CreateDrillDownDataSource(colIndex, rowIndex),
                    (decimal)pivot.GetCellValue(colIndex, rowIndex),
                    decimal.Parse(args[3], NumberStyles.Currency)
                );

            //ASPxPivotGrid1.DataSourceID = Session["sql"].ToString();
            ASPxPivotGrid1.ReloadData();
            pivot.JSProperties["cp_callback"] = true;
        }

        private void ChangeCellValue(PivotDrillDownDataSource source, decimal oldValue, decimal newValue)
        {

            //decimal diff = newValue - oldValue;
            //decimal fact = diff == newValue ? diff / source.RowCount : diff / oldValue;
            //decimal statnewVal = newValue;
            for (int i = 0; i < source.RowCount; i++)
            {
                //sql.UpdateParameters.Clear();
                decimal value = Convert.ToDecimal(source.GetValue(i, "Forecast"));
                //newValue = (value == 0 ? 1 : value) * (1m + fact);
                //if (value == 0)
                //{
                //    newValue = statnewVal;
                //}

                //DataTable fvck = Session["sql"] as DataTable;

                Decimal p1 = Convert.ToDecimal(newValue.ToString());
                int p2 = Convert.ToInt16(source.GetValue(i, "Year").ToString());
                int p3 = Convert.ToInt16(source.GetValue(i, "Month").ToString());
                string p4 = source.GetValue(i, "Customer").ToString();
                string p5 = string.IsNullOrEmpty(source.GetValue(i, "Col1").ToString()) ? "" : source.GetValue(i, "Col1").ToString() + ".";
                int p6 = Convert.ToInt16(source.GetValue(i, "Version").ToString());
                string p7 = Session["userid"].ToString();

                //int index = p4.IndexOf("(");
                //p4 = p4.Substring(0, index);
                //DataRow[] dr = fvck.Select(string.Format("Year = {0} and Month = {1} and Customer = '{2}' and Col1 = '{3}' and Version = {4}", p2, p3, p4, p5, p6));
                //dr[0]["forecast"] = p1;
                Gears.RetriveData2(string.Format("UPDATE sales.Forecast_Temp_Save SET forecast = {0} WHERE Year = {1} and Month = {2} and Customer = '{3}' and Col1 = '{4}' and Version = {5} and Session = '{6}'",p1,p2,p3,p4,p5,p6,p7), Session["ConnString"].ToString());
                //sql.UpdateParameters.Add("Forecast", DbType.Decimal,
                //    newValue.ToString());
                //sql.UpdateParameters.Add("Year", DbType.Int32,
                //    source.GetValue(i, "Year").ToString());
                //sql.UpdateParameters.Add("Month", DbType.Int32,
                //    source.GetValue(i, "Month").ToString());
                //sql.UpdateParameters.Add("Customer", DbType.String,
                //    source.GetValue(i, "Customer").ToString());
                //sql.UpdateParameters.Add("Col1", DbType.String,
                //    string.IsNullOrEmpty(source.GetValue(i, "Col1").ToString()) ? "" : source.GetValue(i, "Col1").ToString() + ".");
                //sql.UpdateParameters.Add("Version", DbType.Int16,
                //    source.GetValue(i, "Version").ToString());
                //if (Session["sql"].ToString() == "sql2")
                //{
                //    sql.UpdateCommand = "UPDATE sales.Forecast_Temp_Save SET forecast = @Forecast WHERE Year = @Year and Month = @Month and Customer = @Customer and Col1 = @Col1 and Version = @Version";
                //}
                //else
                //{
                    //sql.UpdateCommand = "UPDATE sales.Forecast_Temp_Save SET forecast = @Forecast WHERE Year = @Year and Month = @Month and Customer = @Customer and Col1 = @Col1 and Version = @Version";
                //}
                //sql.Update();
                source.SetValue(
                        i, "Forecast",
                        newValue
                    );
            }
        }

        protected void cp_Callback(object sender, CallbackEventArgsBase e)
        {
            switch (e.Parameter)
            {
                case "generate":
                    var num = Convert.ToInt16(string.IsNullOrEmpty(percentage.Text) ? "0" : percentage.Text) / 100m;
                    Gears.RetriveData2(string.Format("Delete from sales.Forecast_Temp_Save where Year = '{0}' and itemcategorycode = '{1}' and Session = '{2}'", year.Text, txtitemcat.Text, Session["userid"].ToString())
                        , Connection);
                    Gears.RetriveData2(string.Format("exec sp_Forecast_Generate_Year_Sales '{0}','{1}','{2}','{3}','{4}','{5}','{6}','forecast','0','{7}'",
                        year.Text, startm.Text, txtitemcat.Text, txtagent.Text, yearfrom.Text, yearto.Text, num, Session["userid"]), Connection);
                    //Session["sql"] = sql.ID;
                    SqlDataSource SPSQL = sql;
                    DataView dw1 = (DataView)SPSQL.Select(DataSourceSelectArguments.Empty);
                    DataTable transtable = dw1.ToTable();
                    ASPxPivotGrid1.Fields.Clear();
                    Session["sql"] = transtable;
                    updatefield();
                    //ASPxPivotGrid1.DataBind();
                    ASPxPivotGrid1.CollapseAll();
                    //ASPxPivotGrid1.ReloadData();
                    foreach (DataRow dtrow in transtable.Rows)
                    {
                        ASPxPivotGrid1.ExpandValue(false, new object[] { dtrow[4].ToString() });
                    }
                    Session["forecast"] = "forecast";
                    Onbtn();
                    break;

                case "retrieve":
                    //DataTable checktable = Gears.RetriveData2("select * from sales.Forecast where Year = '" + year.Text + "' and itemcategorycode = '" + txtitemcat.Text + "' and version = '" + txtver.Text + "'");
                    if (string.IsNullOrEmpty(txtver.Text))
                    {
                        cp.JSProperties["cp_message"] = "No forecast for this year and selected Item Category";
                        Offbtn();
                    }
                    else
                    {
                        Onbtn();
                        Gears.RetriveData2(string.Format("Delete from sales.Forecast_Temp_Save where Year = '{0}' and itemcategorycode = '{1}' and version = '{2}' and session = '{3}'", 
                            year.Text, txtitemcat.Text, txtver.Text, Session["userid"].ToString()), Connection);
                        Gears.RetriveData2("insert into sales.Forecast_Temp_save select [Month],[Year],[Col1],[Forecast],[Customer],[Version],[ItemCategoryCode],'" + Session["userid"].ToString() + "' from sales.Forecast where Year = '" + year.Text + "' and itemcategorycode = '" + txtitemcat.Text + "' and version = '" + txtver.Text + "'", Connection);
                        Gears.RetriveData2(string.Format("exec sp_Forecast_Generate_Year_Sales '{0}','{1}','{2}','{3}','{4}','{5}','0','current','{6}','{7}'",
                            year.Text, startm.Text, txtitemcat.Text, txtagent.Text, yearfrom.Text, yearto.Text, txtver.Text,Session["userid"]), Connection);
                        DataTable getapp = Gears.RetriveData2("select top 1 ApprovedBy from sales.forecast where Year = '" + year.Text + "' and itemcategorycode = '" + txtitemcat.Text + "' and version = '" + txtver.Text + "' "+
                                                                "and isnull(ApprovedBy,'')!='' ", Connection);
                         DataRow _ret = null;
                        if (getapp.Rows.Count > 0)
                        {
                            _ret = getapp.Rows[0];
                            applabel.Text = "Approved By: " + _ret[0].ToString();
                        }
                        else
                        {
                            applabel.Text = null;
                        }
                        
                        //Session["sql"] = sql2.ID;
                        SqlDataSource SPSQL2 = sql2;
                        DataView dw2 = (DataView)SPSQL2.Select(DataSourceSelectArguments.Empty);
                        DataTable transtable2 = dw2.ToTable();
                        ASPxPivotGrid1.Fields.Clear();
                        Session["sql"] = transtable2;
                        updatefield();
                        //ASPxPivotGrid1.DataBind();
                        ASPxPivotGrid1.CollapseAll();
                        //ASPxPivotGrid1.ReloadData();
                        foreach (DataRow dtrow in transtable2.Rows)
                        {
                            ASPxPivotGrid1.ExpandValue(false, new object[] { dtrow[5].ToString() });
                        }
                        //ASPxPivotGrid1.ReloadData();
                        //if (applabel.Text != null)
                        //{
                            gv1.DataBind();
                        //}
                        //else
                        //{
                        //    gv1.DataSourceID = null;
                        //}
                        gv2.DataSourceID = forecastsumm.ID;
                        gv2.DataBind();
                        gv3.DataSourceID = forecastsumm2.ID;
                        gv3.DataBind();
                        Session["forecast"] = "current";
                    }
                    break;

                case "refresh":
                    if (Session["forecast"].ToString() != "import")
                    {
                        if (string.IsNullOrEmpty(txtver.Text))
                        {
                            if (Session["forecast"].ToString() == "forecast")
                            {
                                var num2 = Convert.ToInt16(string.IsNullOrEmpty(percentage.Text) ? "0" : percentage.Text) / 100m;
                                Gears.RetriveData2(string.Format("Delete from sales.Forecast_Temp_Save where Year = '{0}' and itemcategorycode = '{1}' and Session = '{2}'", year.Text, txtitemcat.Text, Session["userid"].ToString())
                                    , Connection);
                                Gears.RetriveData2(string.Format("exec sp_Forecast_Generate_Year_Sales '{0}','{1}','{2}','{3}','{4}','{5}','{6}','forecast','0','{7}'",
                                    year.Text, startm.Text, txtitemcat.Text, txtagent.Text, yearfrom.Text, yearto.Text, num2, Session["userid"]), Connection);
                                //Session["sql"] = sql.ID;
                                SqlDataSource SPSQL4 = sql;
                                DataView dw4 = (DataView)SPSQL4.Select(DataSourceSelectArguments.Empty);
                                DataTable transtable4 = dw4.ToTable();
                                ASPxPivotGrid1.Fields.Clear();
                                Session["sql"] = transtable4;
                                updatefield();
                                //ASPxPivotGrid1.DataBind();
                                ASPxPivotGrid1.CollapseAll();
                                //ASPxPivotGrid1.ReloadData();
                                foreach (DataRow dtrow in transtable4.Rows)
                                {
                                    ASPxPivotGrid1.ExpandValue(false, new object[] { dtrow[4].ToString() });
                                }
                                Onbtn();
                            }
                            else
                            {
                                cp.JSProperties["cp_message"] = "No forecast for this year and selected Item Category";
                                Offbtn();
                            }
                            
                        }
                        else
                        {
                            Gears.RetriveData2(string.Format("Delete from sales.Forecast_Temp_Save where Year = '{0}' and itemcategorycode = '{1}' and version = '{2}' and session = '{3}'", year.Text, txtitemcat.Text, txtver.Text, Session["userid"]), Connection);
                            Gears.RetriveData2("insert into sales.Forecast_Temp_save select [Month],[Year],[Col1],[Forecast],[Customer],[Version],[ItemCategoryCode],'" + Session["userid"].ToString() + "' from sales.Forecast where Year = '" + year.Text + "' and itemcategorycode = '" + txtitemcat.Text + "' and version = '" + txtver.Text + "'", Connection);
                            Gears.RetriveData2(string.Format("exec sp_Forecast_Generate_Year_Sales '{0}','{1}','{2}','{3}','{4}','{5}','0','current','{6}','{7}'",
                                year.Text, startm.Text, txtitemcat.Text, txtagent.Text, yearfrom.Text, yearto.Text, txtver.Text, Session["userid"]), Connection);

                            DataTable getapp = Gears.RetriveData2("select top 1 ApprovedBy from sales.forecast where Year = '" + year.Text + "' and itemcategorycode = '" + txtitemcat.Text + "' and version = '" + txtver.Text + "' " +
                                                                    "and isnull(ApprovedBy,'')!='' ", Connection);
                            DataRow _ret = null;
                            if (getapp.Rows.Count > 0)
                            {
                                _ret = getapp.Rows[0];
                                applabel.Text = "Approved By: " + _ret[0].ToString();
                            }

                            Session["sql"] = sql2.ID;
                            ASPxPivotGrid1.Fields.Clear();
                            updatefield();
                            ASPxPivotGrid1.DataBind();
                            ASPxPivotGrid1.CollapseAll();
                            gv1.DataBind();
                            gv2.DataSourceID = forecastsumm.ID;
                            gv2.DataBind();
                            gv3.DataSourceID = forecastsumm2.ID;
                            gv3.DataBind();
                            Session["forecast"] = "current";
                            Onbtn();
                        }
                    }
                    else
                    {
                        ASPxPivotGrid1.Fields.Clear();
                        DataTable dtValue = (DataTable)JsonConvert.DeserializeObject(Cache["origtable"].ToString(), (typeof(DataTable)));
                        DataTable dtCloned = dtValue.Clone();
                        dtCloned.Columns[3].DataType = typeof(Decimal);
                        foreach (DataRow row in dtValue.Rows)
                        {
                            dtCloned.ImportRow(row);
                        }
                        Session["sql"] = dtCloned;
                        updatefield();
                        ASPxPivotGrid1.CollapseAll();
                        foreach (DataRow dtrow in dtCloned.Rows)
                        {
                            ASPxPivotGrid1.ExpandValue(false, new object[] { dtrow[4].ToString() });
                        }
                        //ASPxPivotGrid1.ReloadData();
                        Onbtn();
                    }
                    break;

                case "save":
                    if (Session["forecast"] == "forecast")
                    {
                        int ver = 0;
                        Gears.RetriveData2(string.Format("exec sp_Save_Forecast '{0}','{1}','{2}','forecast','0','{3}'", year.Text, yearfrom.Text, txtitemcat.Text, Session["userid"]), Connection);
                        Gears.RetriveData2("Delete from sales.Forecast_Temp_Save where Year = '" + year.Text + "' and itemcategorycode = '" + txtitemcat.Text + "' and Session = '" + Session["userid"].ToString() + "'", Connection);
                        cp.JSProperties["cp_reload"] = true;
                        //DataTable dtable = Gears.RetriveData2("select MAX(version) as version from Sales.Forecast where Year = '" + year.Text + "' and itemcategorycode = '" + txtitemcat.Text + "'");
                        //foreach (DataRow dtrow in dtable.Rows)
                        //{
                        //    ver = Convert.ToInt16(dtrow["version"].ToString());
                        //}
                        //Gears.RetriveData2("Delete from sales.Forecast_Temp_Save where Year = '" + year.Text + "' and itemcategorycode = '" + txtitemcat.Text + "'");
                        //Gears.RetriveData2("insert into sales.Forecast_Temp_save select * from sales.Forecast where Year = '" + year.Text + "' and itemcategorycode = '" + txtitemcat.Text + "' and version = '" + ver + "'");
                    }
                    
                    else if (Session["forecast"] == "current")
                    {
                        Gears.RetriveData2("exec sp_Save_Forecast '" + year.Text + "','" + yearfrom.Text + "','" + txtitemcat.Text + "','current','" + txtver.Value.ToString() + "','" + Session["userid"].ToString() + "'", Connection);
                        Gears.RetriveData2("Delete from sales.Forecast_Temp_Save where Year = '" + year.Text + "' and itemcategorycode = '" + txtitemcat.Text + "' and version = '" + txtver.Value.ToString() + "' and Session = '" + Session["userid"].ToString() + "'", Connection);
                        Gears.RetriveData2("insert into sales.Forecast_Temp_save select [Month],[Year],[Col1],[Forecast],[Customer],[Version],[ItemCategoryCode],'" + Session["userid"].ToString() + "' from sales.Forecast where Year = '" + year.Text + "' and itemcategorycode = '" + txtitemcat.Text + "' and version = '" + txtver.Value.ToString() + "'", Connection);
                        Gears.RetriveData2("exec sp_Forecast_Generate_Year_Sales '" + year.Text + "','" + startm.Text + "','" + txtitemcat.Text + "','" +
                        txtagent.Text + "','" + yearfrom.Text + "','" + yearto.Text + "','0','current','" + txtver.Text + "'", Connection);
                        Session["sql"] = sql2.ID;
                        //ASPxPivotGrid1.DataSourceID = Session["sql"].ToString();
                        //ASPxPivotGrid1.Fields.Clear();
                        //updatefield();
                        //ASPxPivotGrid1.DataBind();
                        cp.JSProperties["cp_reload"] = true;
                    }
                    else
                    {
                        Gears.RetriveData2("exec sp_Save_Forecast '" + year.Text + "','" + yearfrom.Text + "','" + txtitemcat.Text + "','forecast','0','" + Session["userid"].ToString() + "'", Connection);
                        Gears.RetriveData2("Delete from sales.Forecast_Temp_Save where Year = '" + year.Text + "' and itemcategorycode = '" + txtitemcat.Text + "' and version = '0' and Session = '" + Session["userid"].ToString() + "'", Connection);
                        cp.JSProperties["cp_reload"] = true;
                    }
                    Onbtn();
                    break;

                case "savever":
                    string vers = string.IsNullOrEmpty(txtver.Text) ? "0" : txtver.Text;
                    Gears.RetriveData2("exec sp_Save_Forecast '" + year.Text + "','" + yearfrom.Text + "','" + txtitemcat.Text + "','forecast','"+vers+"','" + Session["userid"].ToString() + "'", Connection);
                    cp.JSProperties["cp_reload"] = true;
                    Onbtn();
                    //Session["sql"] = sql2.ID;
                    //ASPxPivotGrid1.DataSourceID = Session["sql"].ToString();
                    //ASPxPivotGrid1.DataBind();
                    //cp.JSProperties["cp_close"] = true;
                    break;

                case "itemcat":
                    CriteriaOperator sel1 = new InOperator("itemcategorycode", new string[] { txtitemcat.Text });
                    CriteriaOperator sel2 = new InOperator("year", new string[] { year.Text });
                    versionsql.FilterExpression = (GroupOperator.Combine(GroupOperatorType.And, sel1,sel2)).ToString();
                    txtver.DataSourceID = "versionsql";
                    txtver.DataBind();
                    break;

                case "Update":
                    gv1.UpdateEdit();
                    break;

                case "refreshgrid":
                    Onbtn();
                    break;

                case "import":
                    SqlDataSource SPSQL3 = sql;
                    DataView dw3 = (DataView)SPSQL3.Select(DataSourceSelectArguments.Empty);
                    DataTable transtable3 = dw3.ToTable();
                    ASPxPivotGrid1.Fields.Clear();
                    Session["sql"] = transtable3;
                    Cache["origtable"] = DataTableToJSONWithJSONNet(transtable3);
                    updatefield();
                    ASPxPivotGrid1.CollapseAll();
                    foreach (DataRow dtrow in transtable3.Rows)
                    {
                        ASPxPivotGrid1.ExpandValue(false, new object[] { dtrow[4].ToString() });
                    }
                    //ASPxPivotGrid1.ReloadData();
                    Onbtn();
                    Session["forecast"] = "import";
                    cp.JSProperties["cp_successimp"] = true;
                    break;
            }
        }

        public string DataTableToJSONWithJSONNet(DataTable table)
        {
            string JSONString = string.Empty;
            JSONString = JsonConvert.SerializeObject(table);
            return JSONString;
        }  
        protected void updatefield()
        {
            if (ASPxPivotGrid1.Fields.Count != 0)
            {
                return;
            }
            if (Session["sql"] != null)
            {
                //ASPxPivotGrid1.DataSourceID = Session["sql"].ToString();
                ASPxPivotGrid1.DataSource = Session["sql"];
                ASPxPivotGrid1.DataBind();
                ASPxPivotGrid1.RetrieveFields();

                DataTable dt = Gears.RetriveData2("select forecastlevel from sales.forecastlevel"+
                                 " where year='" + year.Text + "'", Connection);
                int count = dt.Rows.Count;

                ASPxPivotGrid1.Fields["Month"].Visible = false;
                ASPxPivotGrid1.Fields["Year"].Visible = false;
                ASPxPivotGrid1.Fields["Col1"].Visible = false;
                ASPxPivotGrid1.Fields["Version"].Visible = false;
                ASPxPivotGrid1.Fields["ItemCategoryCode"].Visible = false;
                ASPxPivotGrid1.Fields["Date"].Area = PivotArea.ColumnArea;
                ASPxPivotGrid1.Fields["Date"].AreaIndex = 1;
                ASPxPivotGrid1.Fields["Date"].GroupInterval = PivotGroupInterval.DateMonth;
                ASPxPivotGrid1.Fields["Customer"].Area = PivotArea.RowArea;
                ASPxPivotGrid1.Fields["Customer"].AreaIndex = 0;
                ASPxPivotGrid1.Fields["Name"].Area = PivotArea.RowArea;
                ASPxPivotGrid1.Fields["Name"].Width = 400;
                ASPxPivotGrid1.Fields["Name"].AreaIndex = 1;
                
                //ASPxPivotGrid1.Fields["Customer"].SortBySummaryInfo.FieldName = "Forecast";
                for (int i = 1; i < count + 1; i++)
                {
                    ASPxPivotGrid1.Fields["M" + i.ToString()].Area = PivotArea.RowArea;
                }

                cp.JSProperties["cp_field"] = "fieldM" + count.ToString();
                if (count == 0)
                {
                    cp.JSProperties["cp_onlyone"] = true;
                }
                else
                {
                    cp.JSProperties["cp_onlyone"] = false;
                }

                ASPxPivotGrid1.Fields["Forecast"].Area = PivotArea.DataArea;
                ASPxPivotGrid1.Fields["Forecast"].AreaIndex = 0;
                ASPxPivotGrid1.Fields["Forecast"].CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                ASPxPivotGrid1.Fields["Forecast"].CellFormat.FormatString = "{0:#,0.0000;(#,0.0000);}";
                if (Session["sql"].ToString() == "sql2")
                {
                    ASPxPivotGrid1.Fields["Actual"].Area = PivotArea.DataArea;
                    ASPxPivotGrid1.Fields["Actual"].AreaIndex = 1;
                    ASPxPivotGrid1.Fields["Actual"].CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    ASPxPivotGrid1.Fields["Actual"].CellFormat.FormatString = "{0:#,0.0000;(#,0.0000);}";
                }

                //DataTable sestable = Session["sql"] as DataTable;
                //foreach (DataRow dtrow in sestable.Rows)
                //{
                //    ASPxPivotGrid1.ExpandValue(false, new object[] { dtrow[5].ToString() });
                //}
                //if (figure.Text == "Quantity")
                //{
                //    ASPxPivotGrid1.Fields["Forecast"].CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                //    ASPxPivotGrid1.Fields["Forecast"].CellFormat.FormatString = "{0}";
                //}
                //else
                //{
                //    ASPxPivotGrid1.Fields["Forecast"].CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                //    ASPxPivotGrid1.Fields["Forecast"].CellFormat.FormatString = "c2";
                //}
            }
        }

        void Export(bool saveAs)
        {
            const string fileName = "PivotGrid";
            XlsxExportOptionsEx options;

            if (Session["sql"] != null)
            {
                //ASPxPivotGrid1.DataSourceID = Session["sql"].ToString();
                ASPxPivotGrid1.DataSource = Session["sql"];
                ASPxPivotGrid1.DataBind();
                //options = new XlsxExportOptionsEx() { ExportType = ExportType.WYSIWYG };
                ASPxPivotGridExporter1.ExportXlsToResponse(fileName, new XlsExportOptionsEx() { ExportType = ExportType.WYSIWYG });
            }
        }

        protected void buttonSaveAs_Click(object sender, EventArgs e)
        {
            Export(true);
        }

        protected void Connection_Init(object sender, EventArgs e)
        {
            itemcat.ConnectionString = Session["ConnString"].ToString();
            Masterfilebiz.ConnectionString = Session["ConnString"].ToString();
            versionsql.ConnectionString = Session["ConnString"].ToString();
            employee.ConnectionString = Session["ConnString"].ToString();
            sql.ConnectionString = Session["ConnString"].ToString();
            sql2.ConnectionString = Session["ConnString"].ToString();
            forecastsumm.ConnectionString = Session["ConnString"].ToString();
            forecastsumm2.ConnectionString = Session["ConnString"].ToString();
        }

        protected void Offbtn()
        {
            btnrefresh.ClientEnabled = false;
            btnsave.ClientEnabled = false;
            btnsavever.ClientEnabled = false;
            btnexp.ClientEnabled = false;
        }

        protected void Onbtn()
        {
            btnrefresh.ClientEnabled = true;
            btnsave.ClientEnabled = true;
            btnsavever.ClientEnabled = true;
            btnexp.ClientEnabled = true;
        }

        #region importdata
        string FilePath
        {
            get { return Session["FilePath"] == null ? String.Empty : Session["FilePath"].ToString(); }
            set { Session["FilePath"] = value; }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (!IsPostBack)
                FilePath = String.Empty;
        }
        protected void Upload_FileUploadComplete(object sender, DevExpress.Web.FileUploadCompleteEventArgs e)
        {
            FilePath = Page.MapPath("~/App_Data/") + e.UploadedFile.FileName;
            e.UploadedFile.SaveAs(FilePath);
        }
        private DataTable GetTableFromExcel()//Method for reading the imported file
        {
            Workbook book = new Workbook();
            book.LoadDocument(FilePath);
            Worksheet sheet = book.Worksheets.ActiveWorksheet;
            Range range = sheet.GetUsedRange();
            DataTable table = sheet.CreateDataTable(range, true);
            for (int i = 2; i < table.Columns.Count; i++)
            {
                table.Columns[i].DataType = typeof(string);
            }
            DataTableExporter exporter = sheet.CreateDataTableExporter(range, table, true);
            exporter.Export();
            //List<string> arrayList = new List<string>();
            //foreach (DataRow dt in table.Rows)
            //{
            //    if (dt[0].ToString() != "Customer")
            //    {
            //        table.Rows[x].Delete();
            //        x++;
            //    }
            //    else
            //    {
            //        int q = 0;
            //        foreach (DataColumn c in dt.Table.Columns)
            //        {
            //            if (dt[q].ToString() == "Name" || dt[q].ToString() == "Grand Total" || dt[q].ToString() == "")
            //            {
            //                arrayList.Add(c.ToString());
            //            }
            //            q++;
            //        }
            //        break;
            //    }
            //}
            //string[] terms = arrayList.ToArray();
            //for (int a = 0; a < terms.Count(); a++)
            //{
            //    table.Columns.Remove(terms[a]);
            //}
            //table.AcceptChanges();
            return table;
        }

        void book_InvalidFormatException(object sender, SpreadsheetInvalidFormatExceptionEventArgs e)
        {
            Exception exception = new Exception();
            throw new Exception(e.Exception.Message, exception);
        }

        protected void import_cp_Callback(object sender, CallbackEventArgsBase e)
        {
                string execute = "";
                execute = Common.Common.Import_Forecast(GetTableFromExcel(), year.Text, txtitemcat.Text, Session["userid"].ToString(), Session["ConnString"].ToString());

                if (execute == "")
                {
                    import_cp.JSProperties["cp_execmes"] = "Successfully imported";
                }
                else
                {
                    import_cp.JSProperties["cp_execmes"] = execute;
                }
        }
        #endregion
    }


    

    public class CellTemplate : ITemplate
    {
        #region ITemplate Members

        public void InstantiateIn(Control container)
        {
            PivotGridCellTemplateContainer c = (PivotGridCellTemplateContainer)container;
            ASPxLabel l = new ASPxLabel();
            l.ClientSideEvents.Click = "function(s, e) { editor.SetText(s.GetText()); editPopup.ShowAtPos(e.htmlEvent.pageX, e.htmlEvent.pageY); }";
            l.Text = c.Text;
            c.Controls.Add(l);

        }

        #endregion
    }
}