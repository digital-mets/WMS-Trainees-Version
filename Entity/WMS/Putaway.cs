using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GearsLibrary;
using System.Data;

namespace Entity
{
    public class Putaway
    {
        public static string Docnum;

        private static string Conn;//ADD CONN
        public virtual string Connection { get; set; }//ADD CONN

        private static string trans;

        private static string strat;

        private static string status;
        public virtual string DocNumber { get; set; }
        public virtual string PutAwayStrategy { get; set; }
        public virtual string Room { get; set; }
        public virtual string LastEditedBy { get; set; }
        public virtual IList<PutawayDetail> Detail { get; set; }

        public class PutawayDetail
        {
            public virtual Putaway Parent { get; set; }
            public virtual string DocNumber { get; set; }
            public virtual string LineNumber { get; set; }
            public virtual string ItemCode { get; set; }
            public virtual string FullDesc { get; set; }
            public virtual string ColorCode { get; set; }
            public virtual string ClassCode { get; set; }
            public virtual string SizeCode { get; set; }
            public virtual decimal BulkQty { get; set; }
            public virtual string BulkUnit { get; set; }
            public virtual decimal ReceivedQty { get; set; }
            public virtual string Unit { get; set; }
            public virtual DateTime ExpiryDate { get; set; }
            public virtual string BatchNumber { get; set; }
            public virtual DateTime ManufacturingDate { get; set; }
            public virtual string ToLocation { get; set; }
            public virtual string PalletID { get; set; }
            public virtual string LotID { get; set; }
            public virtual DateTime RRDocDate { get; set; }
            public virtual decimal PickedQty { get; set; }
            public virtual string Remarks { get; set; }
            public virtual decimal BaseQty { get; set; }
            public virtual string StatusCode { get; set; }
            public virtual string Status { get; set; }
            public virtual string Strategy { get; set; }
            public virtual string BarcodeNo { get; set; }

            public virtual string Field1 { get; set; }
            public virtual string Field2 { get; set; }
            public virtual string Field3 { get; set; }
            public virtual string Field4 { get; set; }
            public virtual string Field5 { get; set; }
            public virtual string Field6 { get; set; }
            public virtual string Field7 { get; set; }
            public virtual string Field8 { get; set; }
            public virtual string Field9 { get; set; }
            public DataTable getdetail(string DocNumber, string Conn)
            {

                DataTable a;
                try
                {
                    a = Gears.RetriveData2("select a.*,b.FullDesc from WMS.InboundDetail a left join masterfile.item b on a.ItemCode = b.ItemCode where DocNumber='" + DocNumber + "' order by ItemCode,LineNumber",Conn);
                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }

            public void UpdatePutawayDetail(PutawayDetail PutawayDetail)
            {
                if (strat == "M" || PutawayDetail.Strategy == "M")
                {
                    Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                    DT1.Rows.Add("WMS.InboundDetail", "cond", "DocNumber", PutawayDetail.DocNumber);
                    DT1.Rows.Add("WMS.InboundDetail", "cond", "LineNumber", PutawayDetail.LineNumber);
                    DT1.Rows.Add("WMS.InboundDetail", "set", "ToLocation", PutawayDetail.ToLocation);
                    DT1.Rows.Add("WMS.InboundDetail", "set", "Strategy", PutawayDetail.Strategy);
                    Gears.UpdateData(DT1,Conn);

                    Gears.CRUDdatatable DT5 = new Gears.CRUDdatatable();
                    DT5.Rows.Add("WMS.CountSheetSetUp", "cond", "TransType", trans);
                    DT5.Rows.Add("WMS.CountSheetSetUp", "cond", "TransDoc", Docnum);
                    DT5.Rows.Add("WMS.CountSheetSetUp", "cond", "TransLine", PutawayDetail.LineNumber);
                    DT5.Rows.Add("WMS.CountSheetSetUp", "set", "Location", PutawayDetail.ToLocation);
                    Gears.UpdateData(DT5,Conn);

                    Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
                    DT2.Rows.Add("WMS.Inbound", "cond", "DocNumber", PutawayDetail.DocNumber);
                    DT2.Rows.Add("WMS.Inbound", "set", "ApprovedBy", null);
                    DT2.Rows.Add("WMS.Inbound", "set", "ApprovedDate", null);
                    Gears.UpdateData(DT2,Conn);
                }
                else
                {
                    Gears.CRUDdatatable DT3 = new Gears.CRUDdatatable();
                    DT3.Rows.Add("WMS.InboundDetail", "cond", "DocNumber", PutawayDetail.DocNumber);
                    DT3.Rows.Add("WMS.InboundDetail", "cond", "LineNumber", PutawayDetail.LineNumber);
                    DT3.Rows.Add("WMS.InboundDetail", "set", "Strategy", PutawayDetail.Strategy);
                    Gears.UpdateData(DT3,Conn);
                }
            }
        }

        public DataTable getdata(string DocNumber, string Conn)
        {
            DataTable a;

            //if (DocNumber != null)
            //{
            a = Gears.RetriveData2("select * from WMS.Inbound where DocNumber = '" + DocNumber + "'",Conn);
            foreach (DataRow dtRow in a.Rows)
            {
                PutAwayStrategy = dtRow["PutAwayStrategy"].ToString();
                Room = dtRow["RoomCode"].ToString();
            }

            return a;
        }

        public void UpdateData(Putaway _ent)
        {
            Docnum = _ent.DocNumber;
            Conn = _ent.Connection;
            trans = "WMSINB";
            strat = _ent.PutAwayStrategy;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add("WMS.Inbound", "cond", "DocNumber", _ent.DocNumber);
            DT1.Rows.Add("WMS.Inbound", "set", "PutAwayStrategy", _ent.PutAwayStrategy);
            DT1.Rows.Add("WMS.Inbound", "set", "RoomCode", _ent.Room);
            DT1.Rows.Add("WMS.Inbound", "set", "LastEditedBy", _ent.LastEditedBy);
            DT1.Rows.Add("WMS.Inbound", "set", "LastEditedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            string strErr = Gears.UpdateData(DT1,_ent.Connection);
            Functions.AuditTrail("WMSPUT", Docnum, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "UPDATE",_ent.Connection);

            //if (_ent.PutAwayStrategy == "M")
            //{
            //    Gears.RetriveData2 = "update"
            //}
        }
        public void DeleteData(Putaway _ent)
        {
            Docnum = _ent.DocNumber;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("WMS.Inbound", "cond", "DocNumber", _ent.DocNumber);
            Gears.DeleteData(DT1,_ent.Connection);
            Functions.AuditTrail("WMSPUT", Docnum, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "DELETE",_ent.Connection);
        }
    }
}
