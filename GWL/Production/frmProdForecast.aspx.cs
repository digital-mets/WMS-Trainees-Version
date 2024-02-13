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

namespace GWL.Production
{
    public partial class frmProdForecast : System.Web.UI.Page
    {
        //protected override void OnLoad(EventArgs e)
        //{
        //    base.OnLoad(e);  
        //}
        private static string Connection;
        protected void Page_Load(object sender, EventArgs e)
        {
                Connection = Session["ConnString"].ToString();
                string Type = RSF2.Checked == true ? "Sales" : "Production";
                versionsql.SelectCommand = "select distinct version,itemcategorycode,year,type from Production.Forecast_Customer where year = '" + year.Text + "' and itemcategorycode = '" + txtitemcat.Text + "' and type = '" + Type+"' order by version";
                txtver.DataBind();

                sql.SelectParameters["year"].DefaultValue = year.Text;
                sql.SelectParameters["itemcategory"].DefaultValue = txtitemcat.Value == null ? "" : txtitemcat.Text;

                stepsql.SelectParameters["year"].DefaultValue = year.Text;
                stepsql.SelectParameters["itemcategory"].DefaultValue = txtitemcat.Value == null ? "" : txtitemcat.Text;

                sql2.SelectParameters["version"].DefaultValue = txtver.Value == null ? "" : (txtver.Text == "" ? txtver.Value.ToString() : txtver.Text);
                //sql2.SelectParameters["version"].DefaultValue = txtver.Text;
                sql2.SelectParameters["agent"].DefaultValue = txtagent.Text == "" ? "none" : txtagent.Text;
                sql2.SelectParameters["year"].DefaultValue = year.Text;
                sql2.SelectParameters["itemcategory"].DefaultValue = txtitemcat.Text;
                sql2.SelectParameters["startmonth"].DefaultValue = startm.Text;
                sql2.SelectParameters["groupby"].DefaultValue = cbgroup.Checked.ToString();

                ENT_FWorksheet.SelectParameters[0].DefaultValue = year.Text;
                ENT_FWorksheet.SelectParameters[1].DefaultValue = txtitemcat.Text;

                forecastsumm.SelectParameters[0].DefaultValue = year.Text;
                forecastsumm.SelectParameters[1].DefaultValue = txtitemcat.Text;
                forecastsumm.SelectParameters[2].DefaultValue = txtver.Text;
                forecastsumm2.SelectParameters[0].DefaultValue = year.Text;
                
            
            if (!IsPostBack)
            {
                ASPxPivotGrid1.CollapseAll();
                ASPxPivotGrid2.CollapseAll();
                Session["sql"] = null;
                Session["sql2"] = null;
                Session["forecast"] = null;
            }
            //ASPxPivotGrid1.CellTemplate = new CellTemplate();
                //ASPxPivotGrid1.Fields.Clear();
            if (Session["sql"] != null)
            {
                ASPxPivotGrid1.DataSourceID = Session["sql"].ToString();
                updatefield();
            }

            if (Session["sql2"] != null)
            {
                ASPxPivotGrid2.DataSourceID = Session["sql2"].ToString();
                updatefield2();
            }
                
                
                //ASPxPivotGrid1.DataBind();
        }
        protected void pivotGrid_CustomCallback(object sender, DevExpress.Web.ASPxPivotGrid.PivotGridCustomCallbackEventArgs e)
        {
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
            ASPxPivotGrid1.DataSourceID = Session["sql"].ToString();
            pivot.JSProperties["cp_callback"] = true;
        }

        protected void pivot2_CustomCallback(object sender, PivotGridCustomCallbackEventArgs e)
        {
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
            ASPxPivotGrid2.DataSourceID = Session["sql"].ToString();
            pivot.JSProperties["cp_callback"] = true;
        }

        private void ChangeCellValue(PivotDrillDownDataSource source, decimal oldValue, decimal newValue)
        {
            decimal diff = newValue - oldValue;
            decimal fact = diff == newValue ? diff / source.RowCount : diff / oldValue;
            decimal statnewVal = newValue;
            for (int i = 0; i < source.RowCount; i++)
            {
                sql.UpdateParameters.Clear();
                decimal value = (decimal)source.GetValue(i, "Forecast");
                newValue = (value == 0 ? 1 : value) * (1m + fact);
                if (value == 0)
                {
                    newValue = statnewVal;
                }
                
                sql.UpdateParameters.Add("Forecast", DbType.Decimal,
                    newValue.ToString());
                sql.UpdateParameters.Add("Year", DbType.Int32,
                    source.GetValue(i, "Year").ToString());
                sql.UpdateParameters.Add("Month", DbType.Int32,
                    source.GetValue(i, "Month").ToString());
                sql.UpdateParameters.Add("Customer", DbType.String,
                    source.GetValue(i, "Customer").ToString());
                sql.UpdateParameters.Add("Col1", DbType.String,
                    source.GetValue(i, "Col1").ToString()+".");
                sql.UpdateParameters.Add("Version", DbType.Int16,
                    source.GetValue(i, "Version").ToString());
                sql.UpdateParameters.Add("Session", DbType.String,
                    Session["userid"].ToString());
                //if (Session["sql"].ToString() == "sql2")
                //{
                //    sql.UpdateCommand = "UPDATE sales.Forecast_Temp_Save SET forecast = @Forecast WHERE Year = @Year and Month = @Month and Customer = @Customer and Col1 = @Col1 and Version = @Version";
                //}
                //else
                //{
                sql.UpdateCommand = "UPDATE production.Forecast_Temp_Save_Customer SET forecast = @Forecast WHERE Year = @Year and Month = @Month and Customer = @Customer and Col1 = @Col1 and Version = @Version and Session = @Session";
                //}
                sql.Update();
                source.SetValue(
                        i, "Forecast",
                        newValue
                    );
            }
        }

        private void ChangeCellValue2(PivotDrillDownDataSource source, decimal oldValue, decimal newValue)
        {
            decimal diff = newValue - oldValue;
            decimal fact = diff == newValue ? diff / source.RowCount : diff / oldValue;
            decimal statnewVal = newValue;
            for (int i = 0; i < source.RowCount; i++)
            {
                sql.UpdateParameters.Clear();
                decimal value = (decimal)source.GetValue(i, "Forecast");
                newValue = (value == 0 ? 1 : value) * (1m + fact);
                if (value == 0)
                {
                    newValue = statnewVal;
                }

                sql.UpdateParameters.Add("Forecast", DbType.Decimal,
                    newValue.ToString());
                sql.UpdateParameters.Add("Year", DbType.Int32,
                    source.GetValue(i, "Year").ToString());
                sql.UpdateParameters.Add("Month", DbType.Int32,
                    source.GetValue(i, "Month").ToString());
                sql.UpdateParameters.Add("StepCode", DbType.String,
                    source.GetValue(i, "StepCode").ToString());
                sql.UpdateParameters.Add("Description", DbType.String,
                    source.GetValue(i, "Description").ToString() + ".");
                sql.UpdateParameters.Add("Version", DbType.Int16,
                    source.GetValue(i, "Version").ToString());
                sql.UpdateParameters.Add("Session", DbType.String,
                    Session["userid"].ToString());
                sql.UpdateCommand = "UPDATE production.Forecast_Temp_Save_Customer SET forecast = @Forecast WHERE Year = @Year and Month = @Month and Customer = @Customer and Col1 = @Col1 and Version = @Version and Session = @Session";
                //}
                sql.Update();
                source.SetValue(
                        i, "Forecast",
                        newValue
                    );
            }
        }

        protected void cp_Callback(object sender, CallbackEventArgsBase e)
        {
            string Type = RSF2.Checked == true ? "Sales" : "Production";
            string Type2 = RSF.Checked == true ? "Sales" : "Production";
            switch (e.Parameter)
            {    
                case "generate":
                    var num = Convert.ToInt16(string.IsNullOrEmpty(percentage.Text) ? "0" : percentage.Text) / 100m;
                    //Gears.RetriveData2("Delete from production.Forecast_Temp_Save_Customer where Year = '" + year.Text + "' and itemcategorycode = '" + txtitemcat.Text + "' and "
                    //    +"Session = '"+Session["userid"].ToString()+"'", Connection);

                    int leadint = Convert.ToInt16(string.IsNullOrEmpty(leadtime.Text) ? "0" : leadtime.Text);

                    if (RSF.Checked)
                    {
                        Gears.RetriveData2("exec sp_Prod_Forecast_Generate_Year_Sales '" + year.Text + "','" + startm.Text + "','" + txtitemcat.Text + "','" +
                        txtagent.Text + "','" + yearfrom.Text + "','" + yearto.Text + "','" + num + "','forecast','0'," + leadint + ",'" + Session["userid"].ToString() + "'", Connection);
                    }
                    if (RPQ.Checked)
                    {
                        Gears.RetriveData2("exec sp_Prod_Forecast_Generate_Year_Production '" + year.Text + "','" + startm.Text + "','" + txtitemcat.Text + "','" +
                        txtagent.Text + "','" + yearfrom.Text + "','" + yearto.Text + "','" + num + "','forecast','0'," + leadint + ",'" + Session["userid"].ToString() + "'", Connection);
                    }

                    Gears.RetriveData2("exec sp_Prod_Forecast_Generate_Year_Production_Step '" + year.Text + "','" + startm.Text + "','" + txtitemcat.Text + "','" +
                       txtagent.Text + "','" + yearfrom.Text + "','" + yearto.Text + "','" + num + "','forecast','0'," + leadint + ",'" + Session["userid"].ToString() + "'", Connection);
                    
                    Session["sql"] = sql.ID;
                    ASPxPivotGrid1.Fields.Clear();
                    updatefield();
                    ASPxPivotGrid1.DataBind();
                    ASPxPivotGrid1.CollapseAll();
                    //-- Step pivot
                    Session["sql2"] = stepsql.ID;
                    ASPxPivotGrid2.Fields.Clear();
                    updatefield2();
                    ASPxPivotGrid2.DataBind();
                    ASPxPivotGrid2.CollapseAll();
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
                        Gears.RetriveData2("Delete from production.Forecast_Temp_Save_Customer where Year = '" + year.Text + "' and itemcategorycode = '" + txtitemcat.Text + "' and "
                        + "Session = '" + Session["userid"].ToString() + "'", Connection);
                        Gears.RetriveData2("Delete from production.Forecast_Temp_Save_Step where Year = '" + year.Text + "' and itemcategorycode = '" + txtitemcat.Text + "' and "
                        + "Session = '" + Session["userid"].ToString() + "'", Connection);
                        Gears.RetriveData2("insert into production.Forecast_Temp_Save_Customer select [Month],[Year],[Col1],[Forecast],[Customer],[Version],[ItemCategoryCode],'" + Session["userid"].ToString() + "' from Production.Forecast_Customer where Year = '" + year.Text + "' and itemcategorycode = '" + txtitemcat.Text + "' and version = '" + txtver.Text + "' and Type = '" + Type + "' and "+
                                           "month between "+startm.Text+" and 12", Connection);
                        Gears.RetriveData2("insert into production.Forecast_Temp_Save_Step([Month],[Year],[WorkCenter],[Forecast],[StepCode],[Version],[ItemCategoryCode],Session) select [Month],[Year],[WorkCenter],[Forecast],[StepCode],[Version],[ItemCategoryCode],'" + Session["userid"].ToString() + "' from Production.Forecast_Step where Year = '" + year.Text + "' and itemcategorycode = '" + txtitemcat.Text + "' and version = '" + txtver.Text + "' and Type = '" + Type + "' and " +
                                           "month between " + startm.Text + " and 12", Connection);

                        if (RSF2.Checked)
                        {
                            Gears.RetriveData2("exec sp_Prod_Forecast_Generate_Year_Sales '" + year.Text + "','" + startm.Text + "','" + txtitemcat.Text + "','" +
                            txtagent.Text + "','" + yearfrom.Text + "','" + yearto.Text + "','0','current','" + txtver.Text + "','0','" + Session["userid"].ToString() + "'", Connection);
                        }
                        if (RPQ2.Checked)
                        {
                            Gears.RetriveData2("exec sp_Prod_Forecast_Generate_Year_Production '" + year.Text + "','" + startm.Text + "','" + txtitemcat.Text + "','" +
                            txtagent.Text + "','" + yearfrom.Text + "','" + yearto.Text + "','0','current','" + txtver.Text + "','0','" + Session["userid"].ToString() + "'", Connection);
                        }
                        DataTable getapp = Gears.RetriveData2("select top 1 ApprovedBy from Production.Forecast_Customer where Year = '" + year.Text + "' and itemcategorycode = '" + txtitemcat.Text + "' and version = '" + txtver.Text + "' " +
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

                        Session["sql2"] = stepsql.ID;
                        ASPxPivotGrid2.Fields.Clear();
                        updatefield2();
                        ASPxPivotGrid2.DataBind();
                        ASPxPivotGrid2.CollapseAll();
                        gv1.DataBind();

                        gv2.DataSourceID = forecastsumm.ID;
                        gv2.DataBind();
                        gv3.DataSourceID = forecastsumm2.ID;
                        gv3.DataBind();
                        Session["forecast"] = "current";
                    }
                    break;

                case "refresh":
                    if (string.IsNullOrEmpty(txtver.Text))
                    {
                        cp.JSProperties["cp_message"] = "No forecast for this year and selected Item Category";
                        Offbtn();
                    }
                    else
                    {
                        Onbtn();
                        Gears.RetriveData2("Delete from production.Forecast_Temp_Save_Customer where Year = '" + year.Text + "' and itemcategorycode = '" + txtitemcat.Text + "' and "
                        + "Session = '" + Session["userid"].ToString() + "'", Connection);
                        Gears.RetriveData2("insert into production.Forecast_Temp_Save_Customer select [Month],[Year],[Col1],[Forecast],[Customer],[Version],[ItemCategoryCode],'" + Session["userid"].ToString() + "' from Production.Forecast_Customer where Year = '" + year.Text + "' and itemcategorycode = '" + txtitemcat.Text + "' and version = '" + txtver.Text + "' and Type = '"+Type+"'", Connection);
                        //Gears.RetriveData2("exec sp_Prod_Forecast_Generate_Year_Sales '" + year.Text + "','" + startm.Text + "','" + txtitemcat.Text + "','" +
                        //txtagent.Text + "','" + yearfrom.Text + "','" + yearto.Text + "','0','current','" + txtver.Text + "'", Connection);

                        if (RSF2.Checked)
                        {
                            Gears.RetriveData2("exec sp_Prod_Forecast_Generate_Year_Sales '" + year.Text + "','" + startm.Text + "','" + txtitemcat.Text + "','" +
                            txtagent.Text + "','" + yearfrom.Text + "','" + yearto.Text + "','0','current','" + txtver.Text + "','0','" + Session["userid"].ToString() + "'", Connection);
                        }
                        if (RPQ2.Checked)
                        {
                            Gears.RetriveData2("exec sp_Prod_Forecast_Generate_Year_Production '" + year.Text + "','" + startm.Text + "','" + txtitemcat.Text + "','" +
                            txtagent.Text + "','" + yearfrom.Text + "','" + yearto.Text + "','0','current','" + txtver.Text + "','0','" + Session["userid"].ToString() + "'", Connection);
                        }

                        DataTable getapp = Gears.RetriveData2("select top 1 ApprovedBy from Production.Forecast_Customer where Year = '" + year.Text + "' and itemcategorycode = '" + txtitemcat.Text + "' and version = '" + txtver.Text + "' " +
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
                    }
                    break;

                case "save":
                    if (Session["forecast"] == "forecast")
                    {
                        Gears.RetriveData2("exec sp_Save_Forecast_prod '" + year.Text + "','" + yearfrom.Text + "','" + startm.Text + "','" + txtitemcat.Text + "','forecast','0','" + Session["userid"].ToString() + "','" + Type2 + "'", Connection);
                        Gears.RetriveData2("Delete from Production.Forecast_Temp_Save_Customer where Year = '" + year.Text + "' and itemcategorycode = '" + txtitemcat.Text + "' and Session = '"+ Session["userid"].ToString() + "'", Connection);
                        Gears.RetriveData2("Delete from Production.Forecast_Temp_Save_Step where Year = '" + year.Text + "' and itemcategorycode = '" + txtitemcat.Text + "' and Session = '" + Session["userid"].ToString() + "'", Connection);
                        cp.JSProperties["cp_reload"] = true;
                       
                    }
                    
                    else if (Session["forecast"] == "current")
                    {
                        Gears.RetriveData2("exec sp_Save_Forecast_prod '" + year.Text + "','" + yearfrom.Text + "','" + startm.Text + "','" + txtitemcat.Text + "','current','" + txtver.Value.ToString() + "','" + Session["userid"].ToString() + "','" + Type + "'", Connection);
                        Gears.RetriveData2("Delete from Production.Forecast_Temp_Save_Customer where Year = '" + year.Text + "' and itemcategorycode = '" + txtitemcat.Text + "' and version = '" + txtver.Value.ToString() + "' and Session = '" + Session["userid"].ToString() + "'", Connection);
                        //Gears.RetriveData2("insert into Production.Forecast_Temp_Save_Customer select [Month],[Year],[Col1],[Forecast],[Customer],[Version],[ItemCategoryCode],'" + Session["userid"].ToString() + "'  from Production.Forecast_Customer where Year = '" + year.Text + "' and itemcategorycode = '" + txtitemcat.Text + "' and version = '" + txtver.Text + "'", Connection);

                        if (RSF2.Checked)
                        {
                            Gears.RetriveData2("exec sp_Prod_Forecast_Generate_Year_Sales '" + year.Text + "','" + startm.Text + "','" + txtitemcat.Text + "','" +
                            txtagent.Text + "','" + yearfrom.Text + "','" + yearto.Text + "','0','current','" + txtver.Text + "','0','" + Session["userid"].ToString() + "'", Connection);
                        }
                        if (RPQ2.Checked)
                        {
                            Gears.RetriveData2("exec sp_Prod_Forecast_Generate_Year_Production '" + year.Text + "','" + startm.Text + "','" + txtitemcat.Text + "','" +
                            txtagent.Text + "','" + yearfrom.Text + "','" + yearto.Text + "','0','current','" + txtver.Text + "','0','" + Session["userid"].ToString() + "'", Connection);
                        }
                        //Gears.RetriveData2("exec sp_Prod_Forecast_Generate_Year_Sales '" + year.Text + "','" + startm.Text + "','" + txtitemcat.Text + "','" +
                        //txtagent.Text + "','" + yearfrom.Text + "','" + yearto.Text + "','0','current','" + txtver.Text + "'", Connection);

                        Session["sql"] = sql2.ID;
                        //ASPxPivotGrid1.DataSourceID = Session["sql"].ToString();
                        ASPxPivotGrid1.Fields.Clear();
                        updatefield();
                        ASPxPivotGrid1.DataBind();
                        cp.JSProperties["cp_reload"] = true;
                    }
                    Onbtn();
                    break;

                case "savever":
                    if (Session["Forecast"] == "forecast")
                    {
                        Gears.RetriveData2("exec sp_Save_Forecast_prod '" + year.Text + "','" + yearfrom.Text + "','" + startm.Text + "','" + txtitemcat.Text + "','forecast','" + txtver.Value.ToString() + "','" + Session["userid"].ToString() + "','" + Type2 + "'", Connection);
                        Gears.RetriveData2("Delete from Production.Forecast_Temp_Save_Customer where Year = '" + year.Text + "' and itemcategorycode = '" + txtitemcat.Text + "' and Session = '" + Session["userid"].ToString() + "'", Connection);
                        Gears.RetriveData2("Delete from Production.Forecast_Temp_Save_Step where Year = '" + year.Text + "' and itemcategorycode = '" + txtitemcat.Text + "' and Session = '" + Session["userid"].ToString() + "'", Connection);
                        cp.JSProperties["cp_reload"] = true;
                        
                    }
                    else if (Session["forecast"] == "current")
                    {
                        Gears.RetriveData2("exec sp_Save_Forecast_prod '" + year.Text + "','" + yearfrom.Text + "','" + startm.Text + "','" + txtitemcat.Text + "','forecast','" + txtver.Value.ToString() + "','" + Session["userid"].ToString() + "','" + Type2 + "'", Connection);
                    }
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
                    CriteriaOperator sel3 = new InOperator("type", new string[] { Type });
                    versionsql.FilterExpression = (GroupOperator.Combine(GroupOperatorType.And, sel1,sel2,sel3)).ToString();
                    txtver.DataSourceID = "versionsql";
                    txtver.DataBind();
                    break;

                case "Update":
                    gv1.UpdateEdit();
                    break;

                case "refreshgrid":
                    Onbtn();
                    break;
            }
        }

        protected void updatefield()
        {
            if (ASPxPivotGrid1.Fields.Count != 0)
            {
                return;
            }
            if (Session["sql"] != null)
            {
                ASPxPivotGrid1.DataSourceID = Session["sql"].ToString();
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

                DataTable GetCol = Gears.RetriveData2("select REPLACE(Field,'Masterfile.','') as Col from sales.ForecastLevel where year = '"+year.Text+"' order by ForecastLevel asc",Connection);

                //for (int i = 1; i < count + 1; i++)
                //{
                int i = 1;
                    foreach (DataRow dtrow in GetCol.Rows)
                    {   
                        ASPxPivotGrid1.Fields["M" + i.ToString()].Area = PivotArea.RowArea;
                        ASPxPivotGrid1.Fields["M" + i.ToString()].Caption = dtrow[0].ToString();
                        i++;
                    }
                //}
                cp.JSProperties["cp_field"] = "fieldM" + count.ToString();

                ASPxPivotGrid1.Fields["Forecast"].Area = PivotArea.DataArea;
                ASPxPivotGrid1.Fields["Forecast"].AreaIndex = 0;
                ASPxPivotGrid1.Fields["Forecast"].CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                ASPxPivotGrid1.Fields["Forecast"].CellFormat.FormatString = "{0}";
                if (Session["sql"].ToString() == "sql2")
                {
                    ASPxPivotGrid1.Fields["Actual"].Area = PivotArea.DataArea;
                    ASPxPivotGrid1.Fields["Actual"].AreaIndex = 1;
                    ASPxPivotGrid1.Fields["Actual"].CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    ASPxPivotGrid1.Fields["Actual"].CellFormat.FormatString = "{0}";
                }

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
        protected void updatefield2()
        {
            if (ASPxPivotGrid2.Fields.Count != 0)
            {
                return;
            }
            if (Session["sql2"] != null)
            {
                ASPxPivotGrid2.DataSourceID = Session["sql2"].ToString();
                ASPxPivotGrid2.RetrieveFields();

                DataTable dt = Gears.RetriveData2("select forecastlevel from sales.forecastlevel" +
                                 " where year='" + year.Text + "'", Connection);
                int count = dt.Rows.Count;

                ASPxPivotGrid2.Fields["Month"].Visible = false;
                ASPxPivotGrid2.Fields["Year"].Visible = false;
                ASPxPivotGrid2.Fields["Version"].Visible = false;
                ASPxPivotGrid2.Fields["ItemCategoryCode"].Visible = false;
                ASPxPivotGrid2.Fields["Date"].Area = PivotArea.ColumnArea;
                ASPxPivotGrid2.Fields["Date"].AreaIndex = 1;
                ASPxPivotGrid2.Fields["Date"].GroupInterval = PivotGroupInterval.DateMonth;
                ASPxPivotGrid2.Fields["StepCode"].Area = PivotArea.RowArea;
                ASPxPivotGrid2.Fields["StepCode"].AreaIndex = 0;
                ASPxPivotGrid2.Fields["WorkCenter"].Area = PivotArea.RowArea;

                ASPxPivotGrid2.Fields["Forecast"].Area = PivotArea.DataArea;
                ASPxPivotGrid2.Fields["Forecast"].AreaIndex = 0;
                ASPxPivotGrid2.Fields["Forecast"].CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                ASPxPivotGrid2.Fields["Forecast"].CellFormat.FormatString = "{0}";
                //if (Session["sql"].ToString() == "sql2")
                //{
                //    ASPxPivotGrid1.Fields["Actual"].Area = PivotArea.DataArea;
                //    ASPxPivotGrid1.Fields["Actual"].AreaIndex = 1;
                //    ASPxPivotGrid1.Fields["Actual"].CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                //    ASPxPivotGrid1.Fields["Actual"].CellFormat.FormatString = "{0}";
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

                    options = new XlsxExportOptionsEx() { ExportType = ExportType.WYSIWYG };
                    ASPxPivotGridExporter1.ExportXlsxToResponse(fileName, options, saveAs);
        }

        protected void buttonSaveAs_Click(object sender, EventArgs e)
        {
            Export(true);
        }

        protected void Connection_Init(object sender, EventArgs e)
        {
            ((SqlDataSource)sender).ConnectionString = Session["ConnString"].ToString();
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