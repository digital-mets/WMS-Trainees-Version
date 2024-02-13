using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GearsLibrary;
using System.Data;

namespace Entity
{
    public class InventoryForecastLevel
    {
        public virtual string RecordID { get; set; }
        public virtual int Year { get; set; }
        public virtual string InventoryLevel { get; set; }
        public virtual decimal InventoryLevelVal { get; set; }
        public virtual int ForecastLevel { get; set; }
        public virtual string Field { get; set; }
        public virtual string ForecastFigure { get; set; }
        public virtual string ColumnName { get; set; }
        public virtual string Connector { get; set; }
        public virtual string Field1 { get; set; }
        public virtual string Field2 { get; set; }
        public virtual string Field3 { get; set; }
        public virtual string Field4 { get; set; }
        public virtual string Field5 { get; set; }
        public virtual string Field6 { get; set; }
        public virtual string Field7 { get; set; }
        public virtual string Field8 { get; set; }
        public virtual string Field9 { get; set; }
        public virtual string AddedBy { get; set; }
        public virtual string AddedDate { get; set; }
        public virtual string LastEditedBy { get; set; }
        public virtual string LastEditedDate { get; set; }
        public virtual string ActivatedBy { get; set; }
        public virtual string ActivatedDate { get; set; }
        public virtual string DeactivatedBy { get; set; }
        public virtual string DeactivatedDate { get; set; }

        public DataTable getdata(string recordid)
        {
            DataTable a = null;

            if (Year != null)
            {
                a = Gears.RetriveData2("SELECT * FROM Inventory.ForecastLevel WHERE RecordID = '" + recordid + "'");
                foreach (DataRow dtRow in a.Rows)
                {
                    Year = Convert.ToInt16(dtRow["Year"].ToString());
                    InventoryLevel = dtRow["InventoryLevel"].ToString();
                    InventoryLevelVal = Convert.ToDecimal(dtRow["InventoryLevelVal"].ToString());
                    ForecastLevel = Convert.ToInt16(dtRow["ForecastLevel"].ToString());
                    Field = dtRow["Field"].ToString();
                    ForecastFigure = dtRow["ForecastFigure"].ToString();

                    Field1 = dtRow["Field1"].ToString();
                    Field2 = dtRow["Field2"].ToString();
                    Field3 = dtRow["Field3"].ToString();
                    Field4 = dtRow["Field4"].ToString();
                    Field5 = dtRow["Field5"].ToString();
                    Field6 = dtRow["Field6"].ToString();
                    Field7 = dtRow["Field7"].ToString();
                    Field8 = dtRow["Field8"].ToString();
                    Field9 = dtRow["Field9"].ToString();

                    AddedBy = dtRow["AddedBy"].ToString();
                    AddedDate = dtRow["AddedDate"].ToString();
                    LastEditedBy = dtRow["LastEditedBy"].ToString();
                    LastEditedDate = dtRow["LastEditedDate"].ToString();
                    ActivatedBy = dtRow["ActivatedBy"].ToString();
                    ActivatedDate = dtRow["ActivatedDate"].ToString();
                    DeactivatedBy = dtRow["DeactivatedBy"].ToString();
                    DeactivatedDate = dtRow["DeactivatedDate"].ToString();
                }

            }
            else
            {
                a = Gears.RetriveData2("SELECT * FROM Inventory.ForecastLevel WHERE Year IS NULL");
            }
            return a;
        }
        public void InsertData(InventoryForecastLevel _ent)
        {
            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add("Inventory.ForecastLevel", "0", "Year", _ent.Year);
            DT1.Rows.Add("Inventory.ForecastLevel", "0", "InventoryLevel", _ent.InventoryLevel);
            DT1.Rows.Add("Inventory.ForecastLevel", "0", "InventoryLevelVal", _ent.InventoryLevelVal);
            DT1.Rows.Add("Inventory.ForecastLevel", "0", "ForecastLevel", _ent.ForecastLevel);
            DT1.Rows.Add("Inventory.ForecastLevel", "0", "Field", _ent.Field);
            DT1.Rows.Add("Inventory.ForecastLevel", "0", "ForecastFigure", _ent.ForecastFigure);
            DT1.Rows.Add("Inventory.ForecastLevel", "0", "ColumnName", _ent.ColumnName);
            DT1.Rows.Add("Inventory.ForecastLevel", "0", "Connector", _ent.Connector);

            DT1.Rows.Add("Inventory.ForecastLevel", "0", "Field1", _ent.Field1);
            DT1.Rows.Add("Inventory.ForecastLevel", "0", "Field2", _ent.Field2);
            DT1.Rows.Add("Inventory.ForecastLevel", "0", "Field3", _ent.Field3);
            DT1.Rows.Add("Inventory.ForecastLevel", "0", "Field4", _ent.Field4);
            DT1.Rows.Add("Inventory.ForecastLevel", "0", "Field5", _ent.Field5);
            DT1.Rows.Add("Inventory.ForecastLevel", "0", "Field6", _ent.Field6);
            DT1.Rows.Add("Inventory.ForecastLevel", "0", "Field7", _ent.Field7);
            DT1.Rows.Add("Inventory.ForecastLevel", "0", "Field8", _ent.Field8);
            DT1.Rows.Add("Inventory.ForecastLevel", "0", "Field9", _ent.Field9);

            DT1.Rows.Add("Inventory.ForecastLevel", "0", "AddedBy", _ent.AddedBy);
            DT1.Rows.Add("Inventory.ForecastLevel", "0", "AddedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            Gears.CreateData(DT1);

            Functions.AuditTrail("INVFOR", "RecordID " + _ent.RecordID, _ent.AddedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "INSERT");
        }
        public void UpdateData(InventoryForecastLevel _ent)
        {

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add("Inventory.ForecastLevel", "cond", "RecordID", _ent.RecordID);
            DT1.Rows.Add("Inventory.ForecastLevel", "cond", "Year", _ent.Year);
            DT1.Rows.Add("Inventory.ForecastLevel", "set", "InventoryLevel", _ent.InventoryLevel);
            DT1.Rows.Add("Inventory.ForecastLevel", "set", "InventoryLevelVal", _ent.InventoryLevelVal);
            DT1.Rows.Add("Inventory.ForecastLevel", "set", "ForecastLevel", _ent.ForecastLevel);
            DT1.Rows.Add("Inventory.ForecastLevel", "set", "Field", _ent.Field);
            DT1.Rows.Add("Inventory.ForecastLevel", "set", "ForecastFigure", _ent.ForecastFigure);
            DT1.Rows.Add("Inventory.ForecastLevel", "set", "ColumnName", _ent.ColumnName);
            DT1.Rows.Add("Inventory.ForecastLevel", "set", "Connector", _ent.Connector);

            DT1.Rows.Add("Inventory.ForecastLevel", "set", "Field1", _ent.Field1);
            DT1.Rows.Add("Inventory.ForecastLevel", "set", "Field2", _ent.Field2);
            DT1.Rows.Add("Inventory.ForecastLevel", "set", "Field3", _ent.Field3);
            DT1.Rows.Add("Inventory.ForecastLevel", "set", "Field4", _ent.Field4);
            DT1.Rows.Add("Inventory.ForecastLevel", "set", "Field5", _ent.Field5);
            DT1.Rows.Add("Inventory.ForecastLevel", "set", "Field6", _ent.Field6);
            DT1.Rows.Add("Inventory.ForecastLevel", "set", "Field7", _ent.Field7);
            DT1.Rows.Add("Inventory.ForecastLevel", "set", "Field8", _ent.Field8);
            DT1.Rows.Add("Inventory.ForecastLevel", "set", "Field9", _ent.Field9);

            DT1.Rows.Add("Inventory.ForecastLevel", "set", "LastEditedBy", _ent.LastEditedBy);
            DT1.Rows.Add("Inventory.ForecastLevel", "set", "LastEditedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            Gears.UpdateData(DT1);

            Functions.AuditTrail("INVFOR", "RecordID " + _ent.RecordID, _ent.LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "UPDATE");
        }
        public void DeleteData(InventoryForecastLevel _ent)
        {
            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable(); 
            DT1.Rows.Add("Inventory.ForecastLevel", "cond", "RecordID", _ent.RecordID);
            DT1.Rows.Add("Inventory.ForecastLevel", "cond", "Year", _ent.Year);
            Gears.DeleteData(DT1);
            Functions.AuditTrail("INVFOR", "RecordID " + _ent.RecordID, _ent.LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "DELETE");
        }
    }
}
