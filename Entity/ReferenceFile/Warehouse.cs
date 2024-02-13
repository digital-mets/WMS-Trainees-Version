using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GearsLibrary;
using System.Data;
namespace Entity
{
    public class Warehouse
    {
        private static string Conn;
        public virtual string Connection { get; set; }
        public virtual string WarehouseCode { get; set; }
        public virtual string Mnemonics { get; set; }
        public virtual string Description { get; set; }
        public virtual string Supervisor { get; set; }
        public virtual string Address { get; set; }
        public virtual string ContactNumber { get; set; }
        public virtual string CustomerNumber { get; set; }
        public virtual string LastCountDate { get; set; }
        public virtual bool IsBizPartner{ get; set; }
        public virtual bool IsInactive { get; set; }
        public virtual string AddedBy { get; set; }
        public virtual string Latitude { get; set; }
        public virtual string Longitude { get; set; }
        public virtual string AddedDate { get; set; }
        public virtual string LastEditedBy { get; set; }
        public virtual string LastEditedDate { get; set; }

        public virtual string Field1 { get; set; }
        public virtual string Field2 { get; set; }
        public virtual string Field3 { get; set; }
        public virtual string Field4 { get; set; }
        public virtual string Field5 { get; set; }
        public virtual string Field6 { get; set; }
        public virtual string Field7 { get; set; }
        public virtual string Field8 { get; set; }
        public virtual string Field9 { get; set; }
        public virtual IList<WarehouseDetail> Detail { get; set; }
        public class WarehouseDetail
        {
            public virtual Warehouse Parent { get; set; }
            public virtual string WarehouseCode { get; set; }
            public virtual string ItemCode { get; set; }
            public virtual string ColorCode { get; set; }
            public virtual string SizeCode { get; set; }
            public virtual string ClassCode { get; set; }
       
            public virtual decimal Qty { get; set; }
            public virtual decimal BulkQty { get; set; }
            public virtual string BaseUnit { get; set; }
            public virtual string StatusCode { get; set; }
            public virtual DateTime LastMovementDate { get; set; }

            public virtual DateTime FirstIn { get; set; }
            public virtual DateTime LastIn { get; set; }
            public virtual DateTime FirstOut { get; set; }
            public virtual DateTime LastOut { get; set; }
            public DataTable getdetail(string WarehouseCode, string Conn)
            {

                DataTable a;
                try
                {
                    a = Gears.RetriveData2("select * from Masterfile.ItemWHDetail where WarehouseCode='" + WarehouseCode + "'", Conn);
                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }

         
        }
        public DataTable getdata(string WHcode, string Conn)
        {
            DataTable a;

            if (WHcode != null)
            {
                a = Gears.RetriveData2("select * from Masterfile.Warehouse where WarehouseCode = '" + WHcode + "'", Conn);
                foreach (DataRow dtRow in a.Rows)
                {
                    WarehouseCode = dtRow["WarehouseCode"].ToString();
           
                    Mnemonics = dtRow["Mnemonics"].ToString();
                    Description = dtRow["Description"].ToString();
                    Supervisor = dtRow["Supervisor"].ToString();
                    Address = dtRow["Address"].ToString();
                    ContactNumber = dtRow["ContactNumber"].ToString();
                    CustomerNumber = dtRow["CustomerNumber"].ToString();
                    LastCountDate = dtRow["LastCountDate"].ToString();
                    IsBizPartner = Convert.ToBoolean(Convert.IsDBNull(dtRow["IsBizPartner"]) ? false : dtRow["IsBizPartner"]);
                    IsInactive = Convert.ToBoolean(Convert.IsDBNull(dtRow["IsInactive"]) ? false : dtRow["IsInactive"]);
                    Latitude = dtRow["Latitude"].ToString();
                    Longitude = dtRow["Longitude"].ToString();
                }
            }
            else
            {
                a = Gears.RetriveData2("select '' as FunctionalGroupID,'' as Description,'' as AssignHead,'' as DateClosed,'' as Days", Conn);
            }
            return a;
        }
        public void InsertData(Warehouse _ent)
        {
            Conn = _ent.Connection;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add("Masterfile.Warehouse", "0", "WarehouseCode", _ent.WarehouseCode);
            DT1.Rows.Add("Masterfile.Warehouse", "0", "Mnemonics", _ent.Mnemonics);
            DT1.Rows.Add("Masterfile.Warehouse", "0", "Description", _ent.Description);
            DT1.Rows.Add("Masterfile.Warehouse", "0", "Supervisor", _ent.Supervisor);
            DT1.Rows.Add("Masterfile.Warehouse", "0", "Address", _ent.Address);
            DT1.Rows.Add("Masterfile.Warehouse", "0", "ContactNumber", _ent.ContactNumber);
            DT1.Rows.Add("Masterfile.Warehouse", "0", "CustomerNumber", _ent.CustomerNumber);
            DT1.Rows.Add("Masterfile.Warehouse", "0", "LastCountDate", _ent.LastCountDate);
            DT1.Rows.Add("Masterfile.Warehouse", "0", "IsBizPartner", _ent.IsBizPartner);
            DT1.Rows.Add("Masterfile.Warehouse", "0", "IsInactive", _ent.IsInactive);
            DT1.Rows.Add("Masterfile.Warehouse", "0", "AddedBy", _ent.AddedBy);
            DT1.Rows.Add("Masterfile.Warehouse", "0", "AddedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            DT1.Rows.Add("Masterfile.Warehouse", "0", "Latitude", _ent.Latitude);
            DT1.Rows.Add("Masterfile.Warehouse", "0", "Longitude", _ent.Longitude);


            DT1.Rows.Add("Masterfile.Warehouse", "0", "Field1", _ent.Field1);
            DT1.Rows.Add("Masterfile.Warehouse", "0", "Field2", _ent.Field2);
            DT1.Rows.Add("Masterfile.Warehouse", "0", "Field3", _ent.Field3);
            DT1.Rows.Add("Masterfile.Warehouse", "0", "Field4", _ent.Field4);
            DT1.Rows.Add("Masterfile.Warehouse", "0", "Field5", _ent.Field5);
            DT1.Rows.Add("Masterfile.Warehouse", "0", "Field6", _ent.Field6);
            DT1.Rows.Add("Masterfile.Warehouse", "0", "Field7", _ent.Field7);
            DT1.Rows.Add("Masterfile.Warehouse", "0", "Field8", _ent.Field8);
            DT1.Rows.Add("Masterfile.Warehouse", "0", "Field9", _ent.Field9);


            Gears.CreateData(DT1, _ent.Connection);

            Functions.AuditTrail("REFWAREHOUSE", _ent.WarehouseCode, _ent.AddedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "INSERT", _ent.Connection);
        }
        public void UpdateData(Warehouse _ent)
        {
            Conn = _ent.Connection;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Masterfile.Warehouse", "cond", "WarehouseCode", _ent.WarehouseCode);
            DT1.Rows.Add("Masterfile.Warehouse", "set", "Description", _ent.Description);
            DT1.Rows.Add("Masterfile.Warehouse", "set", "Supervisor", _ent.Supervisor);
            DT1.Rows.Add("Masterfile.Warehouse", "set", "Address", _ent.Address);
            DT1.Rows.Add("Masterfile.Warehouse", "set", "ContactNumber", _ent.ContactNumber);
            DT1.Rows.Add("Masterfile.Warehouse", "set", "CustomerNumber", _ent.CustomerNumber);
            DT1.Rows.Add("Masterfile.Warehouse", "set", "LastCountDate", _ent.LastCountDate);
            DT1.Rows.Add("Masterfile.Warehouse", "set", "LastEditedBy", _ent.LastEditedBy);
            DT1.Rows.Add("Masterfile.Warehouse", "set", "LastEditedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            DT1.Rows.Add("Masterfile.Warehouse", "set", "Latitude", _ent.Latitude);
            DT1.Rows.Add("Masterfile.Warehouse", "set", "Longitude", _ent.Longitude);
            DT1.Rows.Add("Masterfile.Warehouse", "set", "Field1", _ent.Field1);
            DT1.Rows.Add("Masterfile.Warehouse", "set", "Field2", _ent.Field2);
            DT1.Rows.Add("Masterfile.Warehouse", "set", "Field3", _ent.Field3);
            DT1.Rows.Add("Masterfile.Warehouse", "set", "Field4", _ent.Field4);
            DT1.Rows.Add("Masterfile.Warehouse", "set", "Field5", _ent.Field5);
            DT1.Rows.Add("Masterfile.Warehouse", "set", "Field6", _ent.Field6);
            DT1.Rows.Add("Masterfile.Warehouse", "set", "Field7", _ent.Field7);
            DT1.Rows.Add("Masterfile.Warehouse", "set", "Field8", _ent.Field8);
            DT1.Rows.Add("Masterfile.Warehouse", "set", "Field9", _ent.Field9);

            string strErr = Gears.UpdateData(DT1, _ent.Connection);

            Functions.AuditTrail("WH", _ent.WarehouseCode, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "UPDATE", _ent.Connection);
        }
        public void DeleteData(Warehouse _ent)
        {
            WarehouseCode = _ent.WarehouseCode;
            Conn = _ent.Connection;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Masterfile.Warehouse", "cond", "WarehouseCode", _ent.WarehouseCode);
            Gears.DeleteData(DT1, _ent.Connection);
            Functions.AuditTrail("WH", _ent.WarehouseCode, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "DELETE", _ent.Connection);
        }
    }
}
