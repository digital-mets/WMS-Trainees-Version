using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using GearsLibrary;
using Entity;

namespace Entity
{
    public class Replenishment
    {
        private static string Conn;
        private static string Doc;
        private static string Date;
        public virtual string Connection { get; set; }
        public virtual string DocNumber { get; set; }
        public virtual string ReferenceRecordID { get; set; }
        public virtual string WarehouseCode { get; set; }
        public virtual string CustomerCode { get; set; }
        public virtual string DocDate { get; set; }
        public virtual string Location { get; set; }
        public virtual decimal MinWeight { get; set; }
        public virtual decimal CurrentWeight { get; set; }
        public virtual decimal RemainingWeight { get; set; }
        public virtual decimal MaxWeight { get; set; }
        public virtual string AddedBy { get; set; }
        public virtual string AddedDate { get; set; }
        public virtual string LastEditedBy { get; set; }
        public virtual string LastEditedDate { get; set; }
        public virtual string SubmittedBy { get; set; }
        public virtual string SubmittedDate { get; set; }
       public virtual IList<Replenishment> Detail { get; set; }
       public class ReplenishmentDetail {
           public virtual Location Parent { get; set; }
           public virtual string DocNumber { get; set; }
           public virtual string LineNumber { get; set; }
           public virtual string ItemCode { get; set; }
           public virtual string LocationCode { get; set; }
           public virtual decimal Qty { get; set; }
           public virtual decimal Kilo { get; set; }
          

           public DataTable getdetail(string DocNumber, string Conn)//ADD CONN
           {
               // the pallet here is the declared variable from the top and updated the value below

               DataTable a;
               try
               {
                   a = Gears.RetriveData2("Select * from wms.[ReplenishmentDetail] where DocNumber ='" + Doc + "'", Conn);
                   return a;
               }
               catch (Exception e)
               {
                   a = null;
                   return a;
               }
           }
           public void AddRepDetail(ReplenishmentDetail ReplenishmentDetail)
           {
               int linenum = 0;


               DataTable count = Gears.RetriveData2("select max(convert(int,LineNumber)) as LineNumber from wms.[ReplenishmentDetail] where DocNumber = '" + Doc + "'", Conn);

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
               DT1.Rows.Add("WMS.ReplenishmentDetail", "0", "DocNumber", Doc);
               DT1.Rows.Add("WMS.ReplenishmentDetail", "0", "LineNumber", strLine);
               DT1.Rows.Add("WMS.ReplenishmentDetail", "0", "ItemCode", ReplenishmentDetail.ItemCode);
               DT1.Rows.Add("WMS.ReplenishmentDetail", "0", "LocationCode", ReplenishmentDetail.LocationCode);
               DT1.Rows.Add("WMS.ReplenishmentDetail", "0", "Qty",Convert.ToDecimal(ReplenishmentDetail.Qty));
               DT1.Rows.Add("WMS.ReplenishmentDetail", "0", "Kilo",Convert.ToDecimal(ReplenishmentDetail.Kilo));

               DT2.Rows.Add("WMS.Replenishment", "cond", "DocNumber", Doc);
               DT2.Rows.Add("WMS.Replenishment", "set", "IsWithDetail", "True");
               
               //DT2.Rows.Add("Masterfile.Location", "cond", "ReferenceRecordID", RefDoc);
               //DT2.Rows.Add("Masterfile.Location", "set", "IsWithDetail", "True");
               //Gears.UpdateData(DT2, Conn);
               Gears.CreateData(DT1, Conn);
               Gears.UpdateData(DT2, Conn);


           }
           public void UpdateRepDetail(ReplenishmentDetail ReplenishmentDetail)
           {

               Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
               Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
               DT1.Rows.Add("WMS.ReplenishmentDetail", "cond", "DocNumber", ReplenishmentDetail.DocNumber);
               DT1.Rows.Add("WMS.ReplenishmentDetail", "cond", "LineNumber", ReplenishmentDetail.LineNumber);
               DT1.Rows.Add("WMS.ReplenishmentDetail", "set", "ItemCode", ReplenishmentDetail.ItemCode);
               DT1.Rows.Add("WMS.ReplenishmentDetail", "set", "LocationCode", ReplenishmentDetail.LocationCode);
               DT1.Rows.Add("WMS.ReplenishmentDetail", "set", "Qty", Convert.ToDecimal(ReplenishmentDetail.Qty));
               DT1.Rows.Add("WMS.ReplenishmentDetail", "set", "Kilo", Convert.ToDecimal(ReplenishmentDetail.Kilo));
               Gears.UpdateData(DT1, Conn);


           }
           public void DeleteRepDetail(ReplenishmentDetail ReplenishmentDetail)
           {

               Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
               Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
               DT1.Rows.Add("WMS.ReplenishmentDetail", "cond", "DocNumber", ReplenishmentDetail.DocNumber);
               DT1.Rows.Add("WMS.ReplenishmentDetail", "cond", "LineNumber", ReplenishmentDetail.LineNumber);
               Gears.DeleteData(DT1, Conn);

               DataTable count = Gears.RetriveData2("select * from WMS.ReplenishmentDetail where DocNumber = '" + Doc + "'", Conn);

               if (count.Rows.Count < 1)
               {
                   DT2.Rows.Add("WMS.Replenishment", "cond", "DocNumber", Doc);
                   DT2.Rows.Add("WMS.Replenishment", "set", "IsWithDetail", "False");
                   Gears.UpdateData(DT2, Conn);
               }
           }
       
       
       }
        public DataTable getdata(string DocNumber, string Conn)
        {
            DataTable a;

            a = Gears.RetriveData2("select a.*,ISNULL(b.MaxBaseQty,0) as MaxWeight from wms.Replenishment a left join Masterfile.Location b on a.ReferenceRecordID = b.ReferenceRecordID where DocNumber = '" + DocNumber + "'", Conn);
         
            foreach (DataRow dtRow in a.Rows)
            {
                Doc = DocNumber;
                DocNumber = dtRow["DocNumber"].ToString();
                DocDate = dtRow["DocDate"].ToString();
                ReferenceRecordID = dtRow["ReferenceRecordID"].ToString();
                WarehouseCode = dtRow["WarehouseCode"].ToString();
                CustomerCode = dtRow["CustomerCode"].ToString();
                Location = dtRow["Location"].ToString();
                CurrentWeight = Convert.ToDecimal(string.IsNullOrEmpty(dtRow["CurrentWeight"].ToString()) ? "0" : dtRow["CurrentWeight"].ToString());
                RemainingWeight = Convert.ToDecimal(string.IsNullOrEmpty(dtRow["RemainingWeight"].ToString()) ? "0" : dtRow["RemainingWeight"].ToString());
                MinWeight = Convert.ToDecimal(string.IsNullOrEmpty(dtRow["MinWeight"].ToString()) ? "0" : dtRow["MinWeight"].ToString());
                MaxWeight = Convert.ToDecimal(string.IsNullOrEmpty(dtRow["MaxWeight"].ToString()) ? "0" : dtRow["MaxWeight"].ToString());
                AddedBy = dtRow["AddedBy"].ToString();
                AddedDate = dtRow["AddedDate"].ToString();
                LastEditedBy = dtRow["LastEditedBy"].ToString();
                LastEditedDate = dtRow["LastEditedDate"].ToString();
                SubmittedBy = dtRow["SubmittedBy"].ToString();
                SubmittedDate = dtRow["SubmittedDate"].ToString();
            }

            return a;
        }
        public void InsertData(Replenishment _ent)
        {
            Doc= _ent.DocNumber;//<--- You can change this part
            Conn = _ent.Connection;
            Date = _ent.DocDate;
            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add("WMS.Replenishment", "0", "DocNumber", _ent.DocNumber);
            DT1.Rows.Add("WMS.Replenishment", "0", "DocDate", _ent.DocDate);
            DT1.Rows.Add("WMS.Replenishment", "0", "ReferenceRecordID", _ent.ReferenceRecordID);
            DT1.Rows.Add("WMS.Replenishment", "0", "WarehouseCode", _ent.WarehouseCode);
            DT1.Rows.Add("WMS.Replenishment", "0", "CustomerCode", _ent.CustomerCode);
            DT1.Rows.Add("WMS.Replenishment", "0", "CurrentWeight", _ent.CurrentWeight);
            DT1.Rows.Add("WMS.Replenishment", "0", "Location", _ent.Location);
            DT1.Rows.Add("WMS.Replenishment", "0", "RemainingWeight", _ent.RemainingWeight);
            DT1.Rows.Add("WMS.Replenishment", "0", "MinWeight", _ent.MinWeight);
            DT1.Rows.Add("WMS.Replenishment", "0", "AddedBy", _ent.AddedBy);
            DT1.Rows.Add("WMS.Replenishment", "0", "AddedDate", DateTime.Now.ToShortDateString());

            Gears.CreateData(DT1, _ent.Connection);
            Functions.AuditTrail("WMSREPL", _ent.DocNumber, AddedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "INSERT", _ent.Connection);
        }
        public void UpdateData(Replenishment _ent)
        {
            Doc = _ent.DocNumber;//<--- You can change this part
            Conn = _ent.Connection;
       
            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add("WMS.Replenishment", "cond", "DocNumber", _ent.DocNumber);
            DT1.Rows.Add("WMS.Replenishment", "set", "DocDate",_ent.DocDate);
            DT1.Rows.Add("WMS.Replenishment", "set", "ReferenceRecordID", _ent.ReferenceRecordID);
            DT1.Rows.Add("WMS.Replenishment", "set", "WarehouseCode", _ent.WarehouseCode);
            DT1.Rows.Add("WMS.Replenishment", "set", "MinWeight", _ent.MinWeight);
            DT1.Rows.Add("WMS.Replenishment", "set", "CustomerCode", _ent.CustomerCode);
            DT1.Rows.Add("WMS.Replenishment", "set", "Location", _ent.Location);
            DT1.Rows.Add("WMS.Replenishment", "set", "CurrentWeight", _ent.CurrentWeight);
            DT1.Rows.Add("WMS.Replenishment", "set", "RemainingWeight", _ent.RemainingWeight);
            DT1.Rows.Add("WMS.Replenishment", "set", "LastEditedBy", _ent.LastEditedBy);
            DT1.Rows.Add("WMS.Replenishment", "set", "LastEditedDate", DateTime.Now.ToShortDateString());

            Gears.UpdateData(DT1, _ent.Connection);
            Functions.AuditTrail("WMSREPL", _ent.DocNumber, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "UPDATE", _ent.Connection);
        }

        public void DeleteData(Replenishment _ent)
        {
            Doc = _ent.DocNumber;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
         

            DT1.Rows.Add("WMS.Replenishment", "cond", "DocNumber", _ent.DocNumber);

            Gears.DeleteData(DT1, _ent.Connection);

            Functions.AuditTrail("WMSREPL", Doc, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "DELETE", _ent.Connection);
        }
    }
}
