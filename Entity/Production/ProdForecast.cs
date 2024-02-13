using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GearsLibrary;
using System.Data;

namespace Entity
{
    public class ProdForecast
    {
        private static string Connection;
        public virtual string CustomerCode { get; set; }
        public virtual Int16 Year { get; set; }
        public virtual Int16 Month { get; set; }
        public virtual string ItemCategoryCode { get; set; }
        public virtual string ProductCategoryCode { get; set; }
        public virtual string ProductSubCategoryCode { get; set; }
        public virtual decimal ForecastQty { get; set; }
        public virtual decimal ForecastAmt { get; set; }
        public virtual int RecordID { get; set; }
        public virtual IList<ForecastWorksheet> Detail { get; set; }

        public class ForecastWorksheet
        {
            public virtual SalesForecast Parent { get; set; }
            public virtual string Year { get; set; }
            public virtual string Customer { get; set; }
            public virtual string CustomerName { get; set; }
            public virtual string ItemCategoryCode { get; set; }
            public virtual decimal AverageSOQty { get; set; }
            public virtual decimal ForecastQty { get; set; }
            public virtual decimal ForecastAmount { get; set; }
            public virtual decimal AverageSRP { get; set; }
            public virtual decimal ProjectedSRP { get; set; }
            public virtual decimal TargetMarkup { get; set; }
            public virtual decimal ActualMarkup { get; set; }
            public virtual decimal NewMarkup { get; set; }
            public virtual decimal TargetTerm { get; set; }
            public virtual decimal ActualTerm { get; set; }
            public virtual decimal NewTerm { get; set; }
            public virtual decimal CurrentCreditLimit { get; set; }
            public virtual decimal CurrentAR { get; set; }
            public virtual decimal NewCreditLimit { get; set; }
            public virtual string Note { get; set; }
            public virtual decimal PDC { get; set; }
            public virtual decimal TotalAR_PDC { get; set; }

            public DataTable getdetail(string yr,string itemcat,string Conn)
            {
                Connection = Conn;

                DataTable a;
                try
                {
                    a = Gears.RetriveData2("select * from sales.ForecastWorksheet where year ='" + yr + "' and ItemCategoryCode = '"+itemcat+"' order by Customer",Conn);
                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }

            public void UpdateForecastWorksheet(ForecastWorksheet ForecastWorksheet)
            {

                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                DT1.Rows.Add("Sales.ForecastWorksheet", "cond", "Year", ForecastWorksheet.Year);
                DT1.Rows.Add("Sales.ForecastWorksheet", "cond", "Customer", ForecastWorksheet.Customer);
                DT1.Rows.Add("Sales.ForecastWorksheet", "cond", "ItemCategoryCode", ForecastWorksheet.ItemCategoryCode);
                DT1.Rows.Add("Sales.ForecastWorksheet", "set", "ProjectedSRP", ForecastWorksheet.ProjectedSRP);
                DT1.Rows.Add("Sales.ForecastWorksheet", "set", "NewMarkup", ForecastWorksheet.NewMarkup);
                DT1.Rows.Add("Sales.ForecastWorksheet", "set", "NewTerm", ForecastWorksheet.NewTerm);
                DT1.Rows.Add("Sales.ForecastWorksheet", "set", "Note", ForecastWorksheet.Note);
                DT1.Rows.Add("Sales.ForecastWorksheet", "set", "PDC", ForecastWorksheet.PDC);
                DT1.Rows.Add("Sales.ForecastWorksheet", "set", "TotalAR_PDC", ForecastWorksheet.TotalAR_PDC);

                Gears.UpdateData(DT1, Connection);
            }

        }

        public DataTable getdata(string Size)
        {
            DataTable a = null;

            //if (Size != null)
            //{
            //    a = Gears.RetriveData2("select * from Masterfile.Size where SizeCode = '" + Size + "'");
            //    foreach (DataRow dtRow in a.Rows)
            //    {

            //        SizeCode = dtRow["SizeCode"].ToString();
            //        Description = dtRow["Description"].ToString();
            //        SizeType = dtRow["SizeType"].ToString();
            //        SortOrder = dtRow["SortOrder"].ToString();
            //        IsInactive = Convert.ToBoolean(dtRow["IsInactive"].ToString());

            //        Field1 = dtRow["Field1"].ToString();
            //        Field2 = dtRow["Field2"].ToString();
            //        Field3 = dtRow["Field3"].ToString();
            //        Field4 = dtRow["Field4"].ToString();
            //        Field5 = dtRow["Field5"].ToString();
            //        Field6 = dtRow["Field6"].ToString();
            //        Field7 = dtRow["Field7"].ToString();
            //        Field8 = dtRow["Field8"].ToString();
            //        Field9 = dtRow["Field9"].ToString();
            //    }

            //}

            //else
            //{
            //    a = Gears.RetriveData2("select '' as FunctionalGroupID,'' as Description,'' as AssignHead,'' as DateClosed,'' as Days");
            //}
            return a;
        }
        public void InsertData(SalesForecast _ent)
        {
            //Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();


            //DT1.Rows.Add("Masterfile.Size", "0", "SizeCode", _ent.SizeCode);
            //DT1.Rows.Add("Masterfile.Size", "0", "Description", _ent.Description);
            //DT1.Rows.Add("Masterfile.Size", "0", "SizeType", _ent.SizeType);
            //DT1.Rows.Add("Masterfile.Size", "0", "SortOrder", _ent.SortOrder);
            //DT1.Rows.Add("Masterfile.Size", "0", "IsInactive", _ent.IsInactive);
            //DT1.Rows.Add("Masterfile.Size", "0", "AddedBy", _ent.AddedBy);
            //DT1.Rows.Add("Masterfile.Size", "0", "AddedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            //DT1.Rows.Add("Masterfile.Size", "0", "Field1", _ent.Field1);
            //DT1.Rows.Add("Masterfile.Size", "0", "Field2", _ent.Field2);
            //DT1.Rows.Add("Masterfile.Size", "0", "Field3", _ent.Field3);
            //DT1.Rows.Add("Masterfile.Size", "0", "Field4", _ent.Field4);
            //DT1.Rows.Add("Masterfile.Size", "0", "Field5", _ent.Field5);
            //DT1.Rows.Add("Masterfile.Size", "0", "Field6", _ent.Field6);
            //DT1.Rows.Add("Masterfile.Size", "0", "Field7", _ent.Field7);
            //DT1.Rows.Add("Masterfile.Size", "0", "Field8", _ent.Field8);
            //DT1.Rows.Add("Masterfile.Size", "0", "Field9", _ent.Field9);


            //Gears.CreateData(DT1);
            //Functions.AuditTrail("REFSIZE", _ent.SizeCode, _ent.AddedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "INSERT");
        }
        public void UpdateData(SalesForecast _ent)
        {

            //Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();


            //DT1.Rows.Add("Masterfile.Size", "cond", "SizeCode", _ent.SizeCode);
            //DT1.Rows.Add("Masterfile.Size", "set", "Description", _ent.Description);
            //DT1.Rows.Add("Masterfile.Size", "set", "SizeType", _ent.SizeType);
            //DT1.Rows.Add("Masterfile.Size", "set", "SortOrder", _ent.SortOrder);
            //DT1.Rows.Add("Masterfile.Size", "set", "IsInactive", _ent.IsInactive);

            //DT1.Rows.Add("Masterfile.Size", "set", "Field1", _ent.Field1);
            //DT1.Rows.Add("Masterfile.Size", "set", "Field2", _ent.Field2);
            //DT1.Rows.Add("Masterfile.Size", "set", "Field3", _ent.Field3);
            //DT1.Rows.Add("Masterfile.Size", "set", "Field4", _ent.Field4);
            //DT1.Rows.Add("Masterfile.Size", "set", "Field5", _ent.Field5);
            //DT1.Rows.Add("Masterfile.Size", "set", "Field6", _ent.Field6);
            //DT1.Rows.Add("Masterfile.Size", "set", "Field7", _ent.Field7);
            //DT1.Rows.Add("Masterfile.Size", "set", "Field8", _ent.Field8);
            //DT1.Rows.Add("Masterfile.Size", "set", "Field9", _ent.Field9);

            //DT1.Rows.Add("Masterfile.Size", "set", "LastEditedBy", _ent.LastEditedBy);
            //DT1.Rows.Add("Masterfile.Size", "set", "LastEditedDate", _ent.LastEditedDate);

            //string strErr = Gears.UpdateData(DT1);
            //Functions.AuditTrail("REFSIZE", SizeCode, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "UPDATE");
        }
        public void DeleteData(SalesForecast _ent)
        {
            //SizeCode = _ent.SizeCode;
            //string strTableName = "Masterfile.Size";

            //Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            //DT1.Rows.Add("Masterfile.Size", "cond", "SizeCode", _ent.SizeCode);
            //Gears.DeleteData(DT1);
            //Functions.AuditTrail("REFSIZE", SizeCode, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "DELETE");
        }
    }
}
