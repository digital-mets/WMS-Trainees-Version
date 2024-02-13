using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GearsLibrary;
using System.Data;

namespace Entity
{
    public class ProdForecastLevel
    {
        public virtual string Connection { get; set; }//ADD CONN
        public virtual int Year { get; set; }
        public virtual int ForecastLevel { get; set; }
        public virtual string ForecastFigure { get; set; }
        public virtual string Field { get; set; }

        public DataTable getdata(string recordid,string Conn)
        {
            DataTable a = null;

            if (Year != null)
            {
                a = Gears.RetriveData2("select * from Sales.ForecastLevel where RecordID = '" + recordid + "'",Conn);
                foreach (DataRow dtRow in a.Rows)
                {

                    Year = Convert.ToInt16(dtRow["Year"].ToString());
                    ForecastLevel = Convert.ToInt16(dtRow["ForecastLevel"].ToString());
                    ForecastFigure = dtRow["ForecastFigure"].ToString();
                    Field = dtRow["Field"].ToString();
                }

            }

            else
            {
                a = Gears.RetriveData2("select * from Sales.ForecastLevel where Year is null",Conn);
            }
            return a;
        }
        public void InsertData(SalesForecastLevel _ent)
        {
            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add("Sales.ForecastLevel", "0", "Year", _ent.Year);
            DT1.Rows.Add("Sales.ForecastLevel", "0", "ForecastLevel", _ent.ForecastLevel);
            DT1.Rows.Add("Sales.ForecastLevel", "0", "ForecastFigure", _ent.ForecastFigure);
            DT1.Rows.Add("Sales.ForecastLevel", "0", "Field", _ent.Field);

            if (_ent.Field == "masterfile.ProductCategory")
            {
                DT1.Rows.Add("Sales.ForecastLevel", "0", "ColumnName", "ProductCategoryCode");
                DT1.Rows.Add("Sales.ForecastLevel", "0", "Connector", "ProductCategoryCode");
            }
            else if (_ent.Field == "masterfile.ProductCategorySub")
            {
                DT1.Rows.Add("Sales.ForecastLevel", "0", "ColumnName", "ProductSubCatCode");
                DT1.Rows.Add("Sales.ForecastLevel", "0", "Connector", "ProductSubCatCode");
            }

            Gears.CreateData(DT1,_ent.Connection);
            //Functions.AuditTrail("REFSIZE", _ent.SizeCode, _ent.AddedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "INSERT");
        }
        public void UpdateData(SalesForecastLevel _ent)
        {

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add("Sales.ForecastLevel", "cond", "Year", _ent.Year);
            DT1.Rows.Add("Sales.ForecastLevel", "cond", "ForecastLevel", _ent.ForecastLevel);
            DT1.Rows.Add("Sales.ForecastLevel", "cond", "ForecastFigure", _ent.ForecastFigure);
            DT1.Rows.Add("Sales.ForecastLevel", "set", "Field", _ent.Field);
            DT1.Rows.Add("Sales.ForecastLevel", "set", "ForecastLevel", _ent.ForecastLevel);
            DT1.Rows.Add("Sales.ForecastLevel", "set", "ForecastFigure", _ent.ForecastFigure);

            if (_ent.Field == "masterfile.ProductCategory")
            {
                DT1.Rows.Add("Sales.ForecastLevel", "set", "ColumnName", "ProductCategoryCode");
                DT1.Rows.Add("Sales.ForecastLevel", "set", "Connector", "ProductCategoryCode");
            }
            else if (_ent.Field == "masterfile.ProductCategorySub")
            {
                DT1.Rows.Add("Sales.ForecastLevel", "set", "ColumnName", "ProductSubCatCode");
                DT1.Rows.Add("Sales.ForecastLevel", "set", "Connector", "ProductSubCatCode");
            }

            Gears.UpdateData(DT1,_ent.Connection);
            //Functions.AuditTrail("REFSIZE", SizeCode, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "UPDATE");
        }
        public void DeleteData(SalesForecastLevel _ent)
        {
            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Sales.ForecastLevel", "cond", "Year", _ent.Year);
            DT1.Rows.Add("Sales.ForecastLevel", "cond", "ForecastLevel", _ent.ForecastLevel);
            DT1.Rows.Add("Sales.ForecastLevel", "cond", "ForecastFigure", _ent.ForecastFigure);
            DT1.Rows.Add("Sales.ForecastLevel", "cond", "Field", _ent.Field);
            Gears.DeleteData(DT1,_ent.Connection);
            //Functions.AuditTrail("REFSIZE", SizeCode, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "DELETE");
        }
    }
}
