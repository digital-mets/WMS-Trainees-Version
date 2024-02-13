using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GearsLibrary;
using System.Data;

namespace Entity
{
   public class ServiceOrderRevision
    {
        private static string Conn;//ADD CONN
        public virtual string Connection { get; set; }//ADD CONN
       public virtual string DocNumber { get; set; }
       public virtual string DocDate { get; set; }
        public virtual string ServiceOrder { get; set; }
        public virtual string OldDueDate { get; set; }
        public virtual string NewDueDate { get; set; }
        public virtual decimal OldWOPrice { get; set; }
        public virtual decimal NewWOPrice { get; set; }
        public virtual string Reason { get; set; }
        public virtual string AddedBy { get; set; }
        public virtual string AddedDate { get; set; }
        public virtual string LastEditedBy { get; set; }
        public virtual string LastEditedDate { get; set; }
        public virtual string SubmittedBy { get; set; }
        public virtual string SubmittedDate { get; set; }
        public virtual string Field1 { get; set; }
        public virtual string Field2 { get; set; }
        public virtual string Field3 { get; set; }
        public virtual string Field4 { get; set; }
        public virtual string Field5 { get; set; }
        public virtual string Field6 { get; set; }
        public virtual string Field7 { get; set; }
        public virtual string Field8 { get; set; }
        public virtual string Field9 { get; set; }
        public DataTable getdata(string DocNumber, string Conn)
        {
            DataTable a;


            a = Gears.RetriveData2("select * from Procurement.ServiceOrderRevision where DocNumber = '" + DocNumber + "'", Conn);
            foreach (DataRow dtRow in a.Rows)
            {
                DocNumber = dtRow["DocNumber"].ToString();
                ServiceOrder = dtRow["ServiceOrder"].ToString();
                DocDate = dtRow["DocDate"].ToString();
                OldDueDate = dtRow["OldDueDate"].ToString();
                NewDueDate = dtRow["NewDueDate"].ToString();
                OldWOPrice = Convert.ToDecimal(Convert.IsDBNull(dtRow["OldWOPrice"]) ? 0 : dtRow["OldWOPrice"]);
                NewWOPrice = Convert.ToDecimal(Convert.IsDBNull(dtRow["NewWOPrice"]) ? 0 : dtRow["NewWOPrice"]);
                Reason = dtRow["Reason"].ToString();
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
                SubmittedBy = dtRow["SubmittedBy"].ToString();
                SubmittedDate = dtRow["SubmittedDate"].ToString();
            }

            return a;
        }

        public class RefTransaction
        {
            public virtual ServiceOrderRevision Parent { get; set; }
            public virtual string RTransType { get; set; }
            public virtual string REFDocNumber { get; set; }
            public virtual string RMenuID { get; set; }
            public virtual string TransType { get; set; }
            public virtual string DocNumber { get; set; }
            public virtual string MenuID { get; set; }
            public virtual string CommandString { get; set; }
            public virtual string RCommandString { get; set; }
            public DataTable getreftransaction(string DocNumber, string Conn)
            {

                DataTable a;
                try
                {
                    a = Gears.RetriveData2("select DISTINCT RTransType,REFDocNumber,RMenuID,RIGHT(B.CommandString, LEN(B.CommandString) - 1) as RCommandString,A.TransType,DocNumber,A.MenuID,RIGHT(C.CommandString, LEN(C.CommandString) - 1) as CommandString from  IT.ReferenceTrans  A "
                                            + " inner join IT.MainMenu B"
                                            + " on A.RMenuID =B.ModuleID "
                                            + " inner join IT.MainMenu C "
                                            + " on A.MenuID = C.ModuleID "
                                            + "  where (DocNumber='" + DocNumber + "' OR   REFDocNumber='" + DocNumber + "') and  (RTransType='PRCSOR' OR  A.TransType='PRCSOR') ", Conn);
                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }
        }
        public void InsertData(ServiceOrderRevision _ent)
        {
            Conn = _ent.Connection;     //ADD CONN
            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add("Procurement.ServiceOrderRevision", "0", "DocNumber", _ent.DocNumber);
            DT1.Rows.Add("Procurement.ServiceOrderRevision", "0", "DocDate", _ent.DocDate);
            DT1.Rows.Add("Procurement.ServiceOrderRevision", "0", "OldDueDate", _ent.OldDueDate);
            DT1.Rows.Add("Procurement.ServiceOrderRevision", "0", "NewDueDate", _ent.NewDueDate);
            DT1.Rows.Add("Procurement.ServiceOrderRevision", "0", "OldWOPrice", _ent.OldWOPrice);
            DT1.Rows.Add("Procurement.ServiceOrderRevision", "0", "NewWOPrice", _ent.NewWOPrice);
            DT1.Rows.Add("Procurement.ServiceOrderRevision", "0", "Reason", _ent.Reason);
            DT1.Rows.Add("Procurement.ServiceOrderRevision", "0", "Field1", _ent.Field1);
            DT1.Rows.Add("Procurement.ServiceOrderRevision", "0", "Field2", _ent.Field2);
            DT1.Rows.Add("Procurement.ServiceOrderRevision", "0", "Field3", _ent.Field3);
            DT1.Rows.Add("Procurement.ServiceOrderRevision", "0", "Field4", _ent.Field4);
            DT1.Rows.Add("Procurement.ServiceOrderRevision", "0", "Field5", _ent.Field5);
            DT1.Rows.Add("Procurement.ServiceOrderRevision", "0", "Field6", _ent.Field6);
            DT1.Rows.Add("Procurement.ServiceOrderRevision", "0", "Field7", _ent.Field7);
            DT1.Rows.Add("Procurement.ServiceOrderRevision", "0", "Field8", _ent.Field8);
            DT1.Rows.Add("Procurement.ServiceOrderRevision", "0", "Field9", _ent.Field9);
            DT1.Rows.Add("Procurement.ServiceOrderRevision", "0", "AddedBy", _ent.AddedBy);
            DT1.Rows.Add("Procurement.ServiceOrderRevision", "0", "AddedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            Gears.CreateData(DT1, _ent.Connection);
        }
        public void UpdateData(ServiceOrderRevision _ent)
        {

            Conn = _ent.Connection;     //ADD CONN
            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Procurement.ServiceOrderRevision", "cond", "DocNumber", _ent.DocNumber);

            DT1.Rows.Add("Procurement.ServiceOrderRevision", "set", "ServiceOrder", _ent.ServiceOrder);
 
            DT1.Rows.Add("Procurement.ServiceOrderRevision", "set", "DocDate", _ent.DocDate);
            DT1.Rows.Add("Procurement.ServiceOrderRevision", "set", "OldDueDate", _ent.OldDueDate);
            DT1.Rows.Add("Procurement.ServiceOrderRevision", "set", "NewDueDate", _ent.NewDueDate);
            DT1.Rows.Add("Procurement.ServiceOrderRevision", "set", "OldWOPrice", _ent.OldWOPrice);
            DT1.Rows.Add("Procurement.ServiceOrderRevision", "set", "NewWOPrice", _ent.NewWOPrice);
            DT1.Rows.Add("Procurement.ServiceOrderRevision", "set", "Reason", _ent.Reason);
            DT1.Rows.Add("Procurement.ServiceOrderRevision", "set", "Field1", _ent.Field1);
            DT1.Rows.Add("Procurement.ServiceOrderRevision", "set", "Field2", _ent.Field2);
            DT1.Rows.Add("Procurement.ServiceOrderRevision", "set", "Field3", _ent.Field3);
            DT1.Rows.Add("Procurement.ServiceOrderRevision", "set", "Field4", _ent.Field4);
            DT1.Rows.Add("Procurement.ServiceOrderRevision", "set", "Field5", _ent.Field5);
            DT1.Rows.Add("Procurement.ServiceOrderRevision", "set", "Field6", _ent.Field6);
            DT1.Rows.Add("Procurement.ServiceOrderRevision", "set", "Field7", _ent.Field7);
            DT1.Rows.Add("Procurement.ServiceOrderRevision", "set", "Field8", _ent.Field8);
            DT1.Rows.Add("Procurement.ServiceOrderRevision", "set", "Field9", _ent.Field9);
            DT1.Rows.Add("Procurement.ServiceOrderRevision", "set", "LastEditedBy", _ent.LastEditedBy);
            DT1.Rows.Add("Procurement.ServiceOrderRevision", "set", "LastEditedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));


            string strErr = Gears.UpdateData(DT1, _ent.Connection);

            Functions.AuditTrail("PRCSOR", DocNumber, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "UPDATE", _ent.Connection);
        }
        public void DeleteData(ServiceOrderRevision _ent)
        {
            Conn = _ent.Connection;     //ADD CONN
            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Procurement.ServiceOrderRevision", "cond", "DocNumber", _ent.DocNumber);
            Gears.DeleteData(DT1, _ent.Connection);
            Functions.AuditTrail("PRCSOR", DocNumber, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "DELETE", _ent.Connection);
        }
    }
}
