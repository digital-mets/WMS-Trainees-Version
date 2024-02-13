using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GearsLibrary;
using System.Data;

namespace Entity
{
    public class Room
    {
        private static string Conn; //Ter
        public virtual string Connection { get; set; } //ter

        private static string strRoom;
        public virtual string RoomCode { get; set; }
        public virtual string WarehouseCode { get; set; }
        public virtual string PlantCode { get; set; }
        public virtual string RoomDescription { get; set; }
        public virtual string StorageType { get; set; }
        public virtual string SideCountperRoom { get; set; }
        public virtual string RowCountperRoom { get; set; }
        public virtual string LevelCountperRoom { get; set; }
        public virtual string TotalCountLocation { get; set; }
        public virtual string MinTemperature { get; set; }
        public virtual string MaxTemperature { get; set; }
        public virtual string MaxPalletCount { get; set; }
        public virtual string MaxBaseQty { get; set; }
        public virtual string MaxBulkQty { get; set; }
        public virtual string CustomerCode { get; set; }
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
        public virtual bool IsInactive { get; set; }
        public virtual string ActivatedBy  { get; set; }
        public virtual string ActivatedDate { get; set; }
        public virtual string DeActivatedBy	 { get; set; }
        public virtual string DeActivatedDate { get; set; }


        public DataTable getdata(string MFRoom, string Conn) //Ter
        {
            DataTable a;

            if (MFRoom != null)
            {
                a = Gears.RetriveData2("select * from Masterfile.Room where RoomCode = '" + MFRoom + "'", Conn); //Ter
                foreach (DataRow dtRow in a.Rows)
                {
                    RoomCode = dtRow["RoomCode"].ToString();
                    WarehouseCode = dtRow["WarehouseCode"].ToString();
                    PlantCode = dtRow["PlantCode"].ToString();
                    RoomDescription = dtRow["RoomDescription"].ToString();
                    StorageType = dtRow["StorageType"].ToString();
                    SideCountperRoom = dtRow["SideCountperRoom"].ToString();
                    RowCountperRoom = dtRow["RowCountperRoom"].ToString();
                    LevelCountperRoom = dtRow["LevelCountperRoom"].ToString();
                    TotalCountLocation = dtRow["TotalCountLocation"].ToString();
                    MinTemperature = dtRow["MinTemperature"].ToString();
                    MaxTemperature = dtRow["MaxTemperature"].ToString();
                    MaxPalletCount = dtRow["MaxPalletCount"].ToString();
                    MaxBaseQty = dtRow["MaxBaseQty"].ToString();
                    MaxBulkQty = dtRow["MaxBulkQty"].ToString();
                    CustomerCode = dtRow["CustomerCode"].ToString();
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
                    DeActivatedBy = dtRow["DeActivatedBy"].ToString();
                    DeActivatedDate = dtRow["DeActivatedDate"].ToString();
                    IsInactive = Convert.ToBoolean(Convert.IsDBNull(dtRow["IsInactive"]) ? false : dtRow["IsInactive"]);
                }
            }
            else
            {
                a = Gears.RetriveData2("select '' as RoomCode,'' as WarehouseCode,'' as PlantCode, "+
                " '' as RoomDescription,'' as StorageType, 0 as SideCountperRoom,0 as RowCountperRoom"+
                " 0 as LevelCountperRoom, 0 as TotalCountLocation, 0 as MinTemperature"+
                " 0 as MaxTemperature, 0 as MaxPalletCount, 0 as MaxBaseQty, 0 as MaxBulkQty " +
                " 0 as CustomerCode", Conn); //Ter
            }
            return a;
        }
        public void InsertData(Room _ent)
        {
            Conn = _ent.Connection; //Ter

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add("Masterfile.Room", "0", "RoomCode", _ent.RoomCode);
            DT1.Rows.Add("Masterfile.Room", "0", "WarehouseCode", _ent.WarehouseCode);
            DT1.Rows.Add("Masterfile.Room", "0", "PlantCode", _ent.PlantCode);
            DT1.Rows.Add("Masterfile.Room", "0", "RoomDescription", _ent.RoomDescription);
            DT1.Rows.Add("Masterfile.Room", "0", "StorageType", _ent.StorageType);
            DT1.Rows.Add("Masterfile.Room", "0", "SideCountperRoom", _ent.SideCountperRoom);
            DT1.Rows.Add("Masterfile.Room", "0", "RowCountperRoom", _ent.RowCountperRoom);
            DT1.Rows.Add("Masterfile.Room", "0", "LevelCountperRoom", _ent.LevelCountperRoom);
            DT1.Rows.Add("Masterfile.Room", "0", "TotalCountLocation", _ent.TotalCountLocation);
            DT1.Rows.Add("Masterfile.Room", "0", "MinTemperature", _ent.MinTemperature);
            DT1.Rows.Add("Masterfile.Room", "0", "MaxTemperature", _ent.MaxTemperature);
            DT1.Rows.Add("Masterfile.Room", "0", "MaxPalletCount", _ent.MaxPalletCount);
            DT1.Rows.Add("Masterfile.Room", "0", "MaxBaseQty", _ent.MaxBaseQty);
            DT1.Rows.Add("Masterfile.Room", "0", "MaxBulkQty", _ent.MaxBulkQty);
            DT1.Rows.Add("Masterfile.Room", "0", "CustomerCode", _ent.CustomerCode);
            DT1.Rows.Add("Masterfile.Room", "0", "Field1", _ent.Field1);
            DT1.Rows.Add("Masterfile.Room", "0", "Field2", _ent.Field2);
            DT1.Rows.Add("Masterfile.Room", "0", "Field3", _ent.Field3);
            DT1.Rows.Add("Masterfile.Room", "0", "Field4", _ent.Field4);
            DT1.Rows.Add("Masterfile.Room", "0", "Field5", _ent.Field5);
            DT1.Rows.Add("Masterfile.Room", "0", "Field6", _ent.Field6);
            DT1.Rows.Add("Masterfile.Room", "0", "Field7", _ent.Field7);
            DT1.Rows.Add("Masterfile.Room", "0", "Field8", _ent.Field8);
            DT1.Rows.Add("Masterfile.Room", "0", "Field9", _ent.Field9);
            DT1.Rows.Add("Masterfile.Room", "0", "AddedBy", _ent.AddedBy);
            DT1.Rows.Add("Masterfile.Room", "0", "AddedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            Gears.CreateData(DT1, _ent.Connection); // TER
        }

        public void UpdateData(Room _ent)
        {
            Conn = _ent.Connection; //Ter

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Masterfile.Room", "cond", "RoomCode", _ent.RoomCode);
            DT1.Rows.Add("Masterfile.Room", "cond", "WarehouseCode", _ent.WarehouseCode);
            DT1.Rows.Add("Masterfile.Room", "cond", "PlantCode", _ent.PlantCode);
            DT1.Rows.Add("Masterfile.Room", "set", "RoomDescription", _ent.RoomDescription);
            DT1.Rows.Add("Masterfile.Room", "set", "StorageType", _ent.StorageType);
            DT1.Rows.Add("Masterfile.Room", "set", "SideCountperRoom", _ent.SideCountperRoom);
            DT1.Rows.Add("Masterfile.Room", "set", "RowCountperRoom", _ent.RowCountperRoom);
            DT1.Rows.Add("Masterfile.Room", "set", "LevelCountperRoom", _ent.LevelCountperRoom);
            DT1.Rows.Add("Masterfile.Room", "set", "TotalCountLocation", _ent.TotalCountLocation);
            DT1.Rows.Add("Masterfile.Room", "set", "MinTemperature", _ent.MinTemperature);
            DT1.Rows.Add("Masterfile.Room", "set", "MaxTemperature", _ent.MaxTemperature);
            DT1.Rows.Add("Masterfile.Room", "set", "MaxPalletCount", _ent.MaxPalletCount);
            DT1.Rows.Add("Masterfile.Room", "set", "MaxBaseQty", _ent.MaxBaseQty);
            DT1.Rows.Add("Masterfile.Room", "set", "MaxBulkQty", _ent.MaxBulkQty);
            DT1.Rows.Add("Masterfile.Room", "set", "AddedBy", _ent.AddedBy);
            DT1.Rows.Add("Masterfile.Room", "set", "AddedDate", _ent.AddedDate);
            DT1.Rows.Add("Masterfile.Room", "set", "LastEditedBy", _ent.LastEditedBy);
            DT1.Rows.Add("Masterfile.Room", "set", "LastEditedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            DT1.Rows.Add("Masterfile.Room", "set", "Field1", _ent.Field1);
            DT1.Rows.Add("Masterfile.Room", "set", "Field2", _ent.Field2);
            DT1.Rows.Add("Masterfile.Room", "set", "Field3", _ent.Field3);
            DT1.Rows.Add("Masterfile.Room", "set", "Field4", _ent.Field4);
            DT1.Rows.Add("Masterfile.Room", "set", "Field5", _ent.Field5);
            DT1.Rows.Add("Masterfile.Room", "set", "Field6", _ent.Field6);
            DT1.Rows.Add("Masterfile.Room", "set", "Field7", _ent.Field7);
            DT1.Rows.Add("Masterfile.Room", "set", "Field8", _ent.Field8);
            DT1.Rows.Add("Masterfile.Room", "set", "Field9", _ent.Field9);

            string strErr = Gears.UpdateData(DT1, _ent.Connection); // Ter
        }

        public void DeleteData(Room _ent)
        {
            Conn = _ent.Connection; //Ter
            strRoom = _ent.RoomCode;
               
            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Masterfile.Room", "cond", "RoomCode", _ent.RoomCode);
            Gears.DeleteData(DT1, _ent.Connection); // Ter
            Functions.AuditTrail("REFROOM", strRoom, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "DELETE", _ent.Connection); // Ter
        }
    }
}
