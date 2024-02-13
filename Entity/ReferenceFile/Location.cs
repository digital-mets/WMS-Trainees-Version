using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GearsLibrary;
using System.Data;

namespace Entity
{
    public class Location
    {
        //03-10-2016 KMM    add connection

        private static string RefDoc;
        private static string WareHCode;
        private static string LocCode;
        private static string Conn { get; set; }
        public virtual string Connection { get; set; }
        public virtual string LocationCode { get; set; }
        public virtual string LocationDescription { get; set; }
        public virtual string WarehouseCode { get; set; }
        public virtual string PlantCode { get; set; }
        public virtual string RoomCode { get; set; }
        public virtual string LocationType { get; set; }
        public virtual string StorageType { get; set; }
        public virtual decimal OnHandBulkQty { get; set; }
        public virtual decimal OnHandBaseQty { get; set; }
        public virtual string OnHandBaseUnit { get; set; }
        public virtual string CurrentPalletCount { get; set; }
        public virtual decimal MaxBulkQty { get; set; }
        public virtual decimal MaxBaseQty { get; set; }
        public virtual decimal MinWeight { get; set; }
        public virtual string MaxPalletCount { get; set; }
        public virtual string CustomerCode { get; set; }
        public virtual string ItemCode { get; set; }
        public virtual int Priority { get; set; }
        public virtual bool IsInactive { get; set; }
        public virtual bool Replenish { get; set; }
        public virtual string ABC { get; set; }
    
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
        public virtual string DeActivatedBy { get; set; }
        public virtual string DeActivatedDate { get; set; }

        public virtual IList<Location> Detail { get; set; }
        public class LocationDetail
        {
            public virtual Location Parent { get; set; }
            public virtual string DocNumber { get; set; }
            public virtual string LineNumber { get; set; }
            public virtual string LocationCode { get; set; }
            public virtual string WarehouseCode { get; set; }
            public virtual string CustomerCode { get; set; }

            public DataTable getdetail(string DocNumber, string Conn)//ADD CONN
            {
                // the pallet here is the declared variable from the top and updated the value below

                DataTable a;
                try
                {
                    a = Gears.RetriveData2("Select * from Masterfile.[LocationDetail] where DocNumber ='" + RefDoc + "'", Conn);
                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }
            public void AddLocationDetail(LocationDetail LocationDetail)
            {
                int linenum = 0;
              

                DataTable count = Gears.RetriveData2("select max(convert(int,LineNumber)) as LineNumber from Masterfile.[LocationDetail] where DocNumber = '" + RefDoc + "'"
                    , Conn);

                try
                {
                    linenum = Convert.ToInt32(count.Rows[0][0].ToString()) + 1;
                }
                catch
                {
                    linenum = 1;
                }
                string strLine = linenum.ToString().PadLeft(5, '0');

                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
                DT1.Rows.Add("Masterfile.LocationDetail", "0", "DocNumber", RefDoc);
                DT1.Rows.Add("Masterfile.LocationDetail", "0", "LineNumber", strLine);
                DT1.Rows.Add("Masterfile.LocationDetail", "0", "PutAwayLineNumber", strLine);
                DT1.Rows.Add("Masterfile.LocationDetail", "0", "WareHouseCode", WareHCode);
                DT1.Rows.Add("Masterfile.LocationDetail", "0", "LocationCode", LocCode);
                DT1.Rows.Add("Masterfile.LocationDetail", "0", "CustomerCode", LocationDetail.CustomerCode);

                //DT2.Rows.Add("Masterfile.Location", "cond", "ReferenceRecordID", RefDoc);
                //DT2.Rows.Add("Masterfile.Location", "set", "IsWithDetail", "True");
                //Gears.UpdateData(DT2, Conn);
                Gears.CreateData(DT1, Conn);
            

            }
            public void UpdateLocationDetail(LocationDetail LocationDetail)
            {

                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
                DT1.Rows.Add("Masterfile.LocationDetail", "cond", "DocNumber", LocationDetail.DocNumber);
                DT1.Rows.Add("Masterfile.LocationDetail", "cond", "LineNumber", LocationDetail.LineNumber);
                DT1.Rows.Add("Masterfile.LocationDetail", "set", "WareHouseCode", LocationDetail.WarehouseCode);
                DT1.Rows.Add("Masterfile.LocationDetail", "set", "LocationCode", LocationDetail.LocationCode);
                DT1.Rows.Add("Masterfile.LocationDetail", "set", "CustomerCode", LocationDetail.CustomerCode);
                Gears.UpdateData(DT1, Conn);

      
            }
            public void DeleteLocationDetail(LocationDetail LocationDetail)
            {

                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                DT1.Rows.Add("Masterfile.LocationDetail", "cond", "DocNumber", LocationDetail.DocNumber);
                DT1.Rows.Add("Masterfile.LocationDetail", "cond", "LineNumber", LocationDetail.LineNumber);
                Gears.DeleteData(DT1, Conn);

            }
        }

        public DataTable getdata(string Loc, string Conn)//KMM add Conn
        {
            DataTable a;

            if (Loc != null)
            {   //2020-06-21 RA START Handling of Multiple Warehouse,Plant and Room in one Location
                a = Gears.RetriveData2("select * from Masterfile.Location where ReferenceRecordID = '" + Loc + "'", Conn);//KMM add Conn
                foreach (DataRow dtRow in a.Rows)
                {
                    RefDoc = Loc;
                    LocCode = dtRow["LocationCode"].ToString();
                    WareHCode = dtRow["WarehouseCode"].ToString();
                    LocationCode = dtRow["LocationCode"].ToString();
                    LocationDescription = dtRow["LocationDescription"].ToString();
                    WarehouseCode = dtRow["WarehouseCode"].ToString();
                    PlantCode = dtRow["PlantCode"].ToString();
                    RoomCode = dtRow["RoomCode"].ToString();
                    LocationType = dtRow["LocationType"].ToString();
                    OnHandBulkQty = Convert.ToDecimal(string.IsNullOrEmpty(dtRow["OnHandBulkQty"].ToString()) ? "0" : dtRow["OnHandBulkQty"].ToString());
                    OnHandBaseQty = Convert.ToDecimal(string.IsNullOrEmpty(dtRow["OnHandBaseQty"].ToString()) ? "0" : dtRow["OnHandBaseQty"].ToString());
                    MinWeight = Convert.ToDecimal(string.IsNullOrEmpty(dtRow["minWeight"].ToString()) ? "0" : dtRow["minWeight"].ToString());
                    OnHandBaseUnit = dtRow["OnHandBaseUnit"].ToString();
                    CurrentPalletCount = dtRow["CurrentPalletCount"].ToString();
                    MaxBulkQty = Convert.ToDecimal(string.IsNullOrEmpty(dtRow["MaxBulkQty"].ToString()) ? "0" : dtRow["MaxBulkQty"].ToString());
                    MaxBaseQty = Convert.ToDecimal(string.IsNullOrEmpty(dtRow["MaxBaseQty"].ToString()) ? "0" : dtRow["MaxBaseQty"].ToString());
                    MaxPalletCount = dtRow["MaxPalletCount"].ToString();
                    CustomerCode = dtRow["CustomerCode"].ToString();
                    StorageType = dtRow["StorageType"].ToString();
                    ItemCode = dtRow["ItemCode"].ToString();
                    IsInactive = Convert.ToBoolean(Convert.IsDBNull(dtRow["IsInactive"]) ? false : dtRow["IsInactive"]);
                    Replenish = Convert.ToBoolean(Convert.IsDBNull(dtRow["Replenish"]) ? false : dtRow["Replenish"]);
                    ABC = dtRow["ABC"].ToString();
                    Priority = Convert.ToInt16(string.IsNullOrEmpty(dtRow["Priority"].ToString()) ? "0" : dtRow["Priority"].ToString());
                   

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
                }

            }

            else
            {
                a = Gears.RetriveData2("select '' as FunctionalGroupID,'' as Description,'' as AssignHead,'' as DateClosed,'' as Days", Conn);//KMM add Conn
            }
            return a;
        }
        public void InsertData(Location _ent)
        {
            Conn = _ent.Connection;//ADD CONN
            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();


            DT1.Rows.Add("Masterfile.Location", "0", "LocationCode", _ent.LocationCode);
            DT1.Rows.Add("Masterfile.Location", "0", "LocationDescription", _ent.LocationDescription);
            DT1.Rows.Add("Masterfile.Location", "0", "WarehouseCode", _ent.WarehouseCode);
            DT1.Rows.Add("Masterfile.Location", "0", "PlantCode", _ent.PlantCode);
            DT1.Rows.Add("Masterfile.Location", "0", "RoomCode", _ent.RoomCode);
            DT1.Rows.Add("Masterfile.Location", "0", "LocationType", _ent.LocationType);
            DT1.Rows.Add("Masterfile.Location", "0", "OnHandBulkQty", _ent.OnHandBulkQty);
            DT1.Rows.Add("Masterfile.Location", "0", "OnHandBaseQty", _ent.OnHandBaseQty);
            DT1.Rows.Add("Masterfile.Location", "0", "OnHandBaseUnit", _ent.OnHandBaseUnit);
            DT1.Rows.Add("Masterfile.Location", "0", "CurrentPalletCount", _ent.CurrentPalletCount);
            DT1.Rows.Add("Masterfile.Location", "0", "MaxBulkQty", _ent.MaxBulkQty);
            DT1.Rows.Add("Masterfile.Location", "0", "minWeight", _ent.MinWeight);
            DT1.Rows.Add("Masterfile.Location", "0", "MaxBaseQty", _ent.MaxBaseQty);
            DT1.Rows.Add("Masterfile.Location", "0", "MaxPalletCount", _ent.MaxPalletCount);
            DT1.Rows.Add("Masterfile.Location", "0", "CustomerCode", _ent.CustomerCode);
            DT1.Rows.Add("Masterfile.Location", "0", "ItemCode", _ent.ItemCode);
            DT1.Rows.Add("Masterfile.Location", "0", "Replenish", _ent.Replenish);
            DT1.Rows.Add("Masterfile.Location", "0", "IsInactive", _ent.IsInactive);
            DT1.Rows.Add("Masterfile.Location", "0", "ABC", _ent.ABC);
            DT1.Rows.Add("Masterfile.Location", "0", "Priority", _ent.Priority);
            DT1.Rows.Add("Masterfile.Location", "0", "StorageType", _ent.StorageType);
            DT1.Rows.Add("Masterfile.Location", "0", "AddedBy", _ent.AddedBy);
            DT1.Rows.Add("Masterfile.Location", "0", "AddedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        

            DT1.Rows.Add("Masterfile.Location", "0", "Field1", _ent.Field1);
            DT1.Rows.Add("Masterfile.Location", "0", "Field2", _ent.Field2);
            DT1.Rows.Add("Masterfile.Location", "0", "Field3", _ent.Field3);
            DT1.Rows.Add("Masterfile.Location", "0", "Field4", _ent.Field4);
            DT1.Rows.Add("Masterfile.Location", "0", "Field5", _ent.Field5);
            DT1.Rows.Add("Masterfile.Location", "0", "Field6", _ent.Field6);
            DT1.Rows.Add("Masterfile.Location", "0", "Field7", _ent.Field7);
            DT1.Rows.Add("Masterfile.Location", "0", "Field8", _ent.Field8);
            DT1.Rows.Add("Masterfile.Location", "0", "Field9", _ent.Field9);



            Gears.CreateData(DT1, _ent.Connection);//KMM add Conn

            Functions.AuditTrail("REFLOC", _ent.LocationCode, _ent.AddedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "INSERT", _ent.Connection);//KMM add Conn
        }

        public void UpdateData(Location _ent)
        {
            Conn = _ent.Connection;//ADD CONN
            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();


            DT1.Rows.Add("Masterfile.Location", "cond", "LocationCode", _ent.LocationCode);
            DT1.Rows.Add("Masterfile.Location", "set", "LocationDescription", _ent.LocationDescription);
            DT1.Rows.Add("Masterfile.Location", "cond", "WarehouseCode", _ent.WarehouseCode);
            DT1.Rows.Add("Masterfile.Location", "cond", "PlantCode", _ent.PlantCode);
            DT1.Rows.Add("Masterfile.Location", "cond", "RoomCode", _ent.RoomCode);
            DT1.Rows.Add("Masterfile.Location", "set", "LocationType", _ent.LocationType);
            DT1.Rows.Add("Masterfile.Location", "set", "OnHandBulkQty", _ent.OnHandBulkQty);
            DT1.Rows.Add("Masterfile.Location", "set", "minWeight", _ent.MinWeight);
            DT1.Rows.Add("Masterfile.Location", "set", "OnHandBaseQty", _ent.OnHandBaseQty);
            DT1.Rows.Add("Masterfile.Location", "set", "OnHandBaseUnit", _ent.OnHandBaseUnit);
            DT1.Rows.Add("Masterfile.Location", "set", "CurrentPalletCount", _ent.CurrentPalletCount);
            DT1.Rows.Add("Masterfile.Location", "set", "MaxBulkQty", _ent.MaxBulkQty);
            DT1.Rows.Add("Masterfile.Location", "set", "MaxBaseQty", _ent.MaxBaseQty);
            DT1.Rows.Add("Masterfile.Location", "set", "MaxPalletCount", _ent.MaxPalletCount);
            DT1.Rows.Add("Masterfile.Location", "set", "CustomerCode", _ent.CustomerCode);
            DT1.Rows.Add("Masterfile.Location", "set", "ItemCode", _ent.ItemCode);
            DT1.Rows.Add("Masterfile.Location", "set", "Replenish", _ent.Replenish);
            DT1.Rows.Add("Masterfile.Location", "set", "IsInactive", _ent.IsInactive);
            DT1.Rows.Add("Masterfile.Location", "set", "ABC", _ent.ABC);
            DT1.Rows.Add("Masterfile.Location", "set", "Priority", _ent.Priority);
            DT1.Rows.Add("Masterfile.Location", "set", "StorageType", _ent.StorageType);
            DT1.Rows.Add("Masterfile.Location", "set", "Field1", _ent.Field1);
            DT1.Rows.Add("Masterfile.Location", "set", "Field2", _ent.Field2);
            DT1.Rows.Add("Masterfile.Location", "set", "Field3", _ent.Field3);
            DT1.Rows.Add("Masterfile.Location", "set", "Field4", _ent.Field4);
            DT1.Rows.Add("Masterfile.Location", "set", "Field5", _ent.Field5);
            DT1.Rows.Add("Masterfile.Location", "set", "Field6", _ent.Field6);
            DT1.Rows.Add("Masterfile.Location", "set", "Field7", _ent.Field7);
            DT1.Rows.Add("Masterfile.Location", "set", "Field8", _ent.Field8);
            DT1.Rows.Add("Masterfile.Location", "set", "Field9", _ent.Field9);
            DT1.Rows.Add("Masterfile.Location", "set", "LastEditedBy", _ent.LastEditedBy);
            DT1.Rows.Add("Masterfile.Location", "set", "LastEditedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
     

            string strErr = Gears.UpdateData(DT1, _ent.Connection);//KMM add Conn
            Functions.AuditTrail("REFLOC", LocationCode, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "UPDATE", _ent.Connection);//KMM add Conn
        }
        
        public void DeleteData(Location _ent)
        {
            Conn = _ent.Connection;//ADD CONN
            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Masterfile.Location", "cond", "LocationCode", _ent.LocationCode);
            Gears.DeleteData(DT1, _ent.Connection);//KMM add Conn
            Functions.AuditTrail("REFLOC", LocationCode, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "DELETE", _ent.Connection);//KMM add Conn



        }
    }
}
