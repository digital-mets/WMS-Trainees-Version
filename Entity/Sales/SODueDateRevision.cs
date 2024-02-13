using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GearsLibrary;
using System.Data;

namespace Entity
{
    public class SODueDateRevision
    {
        private static string Docnum;

        private static string Conn;
        public virtual string Connection { get; set; }
        public virtual string DocNumber { get; set; }
        public virtual string DocDate { get; set; }
        public virtual string SONumber { get; set; }
        public virtual string Remarks { get; set; }
        public virtual string OldDueDate { get; set; }
        public virtual string NewDueDate { get; set; }        
        public virtual string SubmittedBy { get; set; }
        public virtual string SubmittedDate { get; set; }
        public virtual string AddedBy { get; set; }
        public virtual string AddedDate { get; set; }
        public virtual string LastEditedBy { get; set; }
        public virtual string LastEditedDate { get; set; }
        public virtual string CancelledBy { get; set; }
        public virtual string CancelledDate { get; set; }
        
        public virtual string Field1 { get; set; }
        public virtual string Field2 { get; set; }
        public virtual string Field3 { get; set; }
        public virtual string Field4 { get; set; }
        public virtual string Field5 { get; set; }
        public virtual string Field6 { get; set; }
        public virtual string Field7 { get; set; }
        public virtual string Field8 { get; set; }
        public virtual string Field9 { get; set; }

        public class RefTransaction
        {
            public virtual SODueDateRevision Parent { get; set; }
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
                    a = Gears.RetriveData2("SELECT DISTINCT RTransType,REFDocNumber,RMenuID,RIGHT(B.CommandString, LEN(B.CommandString) - 1) as RCommandString,A.TransType,DocNumber,A.MenuID,RIGHT(C.CommandString, LEN(C.CommandString) - 1) as CommandString from  IT.ReferenceTrans  A "
                                            + " INNER JOIN IT.MainMenu B"
                                            + " ON A.RMenuID =B.ModuleID "
                                            + " INNER JOIN IT.MainMenu C "
                                            + " ON A.MenuID = C.ModuleID "
                                            + "  where (DocNumber='" + DocNumber + "' OR REFDocNumber='" + DocNumber + "')  AND  (RTransType='SLSSOD' OR  A.TransType='SLSSOD') ", Conn);
                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }
        }

        public DataTable getdata(string DocNumber, string Conn)
        {
            DataTable a;
            //if (DocNumber != null)
            //{
            a = Gears.RetriveData2("select * from Sales.SODueDateRevision where DocNumber = '" + DocNumber + "'", Conn);
            foreach (DataRow dtRow in a.Rows)
            {
                DocNumber = dtRow["DocNumber"].ToString();
                DocDate = dtRow["DocDate"].ToString();
                SONumber = dtRow["SONumber"].ToString();
                Remarks = dtRow["Remarks"].ToString();
                OldDueDate = dtRow["OldDueDate"].ToString();
                NewDueDate = dtRow["NewDueDate"].ToString();

                SubmittedBy = dtRow["SubmittedBy"].ToString();
                SubmittedDate = dtRow["SubmittedDate"].ToString();
                AddedBy = dtRow["AddedBy"].ToString();
                AddedDate = dtRow["AddedDate"].ToString();
                LastEditedBy = dtRow["LastEditedBy"].ToString();
                LastEditedDate = dtRow["LastEditedDate"].ToString();
                CancelledBy = dtRow["CancelledBy"].ToString();
                CancelledDate = dtRow["CancelledDate"].ToString();
                //IsValidated = Convert.ToBoolean(Convert.IsDBNull(dtRow["IsValidated"]) ? false : dtRow["IsValited"]);
                //IsWithDetail =  Convert.ToBoolean(dtRow["IsWithDetail"].ToString());
                //IsSample = Convert.ToBoolean(Convert.IsDBNull(dtRow["IsSample"]) ? false : dtRow["IsSample"]);
                //IsPrinted = Convert.ToBoolean(Convert.IsDBNull(dtRow["IsPrinted"]) ? false : dtRow["IsPrinted"]);

                Field1 = dtRow["Field1"].ToString();
                Field2 = dtRow["Field2"].ToString();
                Field3 = dtRow["Field3"].ToString();
                Field4 = dtRow["Field4"].ToString();
                Field5 = dtRow["Field5"].ToString();
                Field6 = dtRow["Field6"].ToString();
                Field7 = dtRow["Field7"].ToString();
                Field8 = dtRow["Field8"].ToString();
                Field9 = dtRow["Field9"].ToString();
            }
            return a;
        }
        public void InsertData(SODueDateRevision _ent)
        {
            Docnum = _ent.DocNumber;
            Conn = _ent.Connection;//ADD CONN
            //trans = _ent.TransType;
            //ddate = _ent.DocDate;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add("Sales.SODueDateRevision", "0", "DocNumber", _ent.DocNumber);
            DT1.Rows.Add("Sales.SODueDateRevision", "0", "DocDate", _ent.DocDate);
            DT1.Rows.Add("Sales.SODueDateRevision", "0", "SONumber", _ent.SONumber);
            DT1.Rows.Add("Sales.SODueDateRevision", "0", "Remarks", _ent.Remarks);
            DT1.Rows.Add("Sales.SODueDateRevision", "0", "OldDueDate", _ent.OldDueDate);
            DT1.Rows.Add("Sales.SODueDateRevision", "0", "NewDueDate", _ent.NewDueDate);


            DT1.Rows.Add("Sales.SODueDateRevision", "0", "AddedBy", _ent.AddedBy);
            DT1.Rows.Add("Sales.SODueDateRevision", "0", "AddedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            //DT1.Rows.Add("Procurement.PurchaseRequest", "0", "LastEditedBy", _ent.LastEditedBy);
            //DT1.Rows.Add("Procurement.PurchaseRequest", "0", "LastEditedDate", _ent.LastEditedDate);

            DT1.Rows.Add("Sales.SODueDateRevision", "0", "Field1", _ent.Field1);
            DT1.Rows.Add("Sales.SODueDateRevision", "0", "Field2", _ent.Field2);
            DT1.Rows.Add("Sales.SODueDateRevision", "0", "Field3", _ent.Field3);
            DT1.Rows.Add("Sales.SODueDateRevision", "0", "Field4", _ent.Field4);
            DT1.Rows.Add("Sales.SODueDateRevision", "0", "Field5", _ent.Field5);
            DT1.Rows.Add("Sales.SODueDateRevision", "0", "Field6", _ent.Field6);
            DT1.Rows.Add("Sales.SODueDateRevision", "0", "Field7", _ent.Field7);
            DT1.Rows.Add("Sales.SODueDateRevision", "0", "Field8", _ent.Field8);
            DT1.Rows.Add("Sales.SODueDateRevision", "0", "Field9", _ent.Field9);

            Gears.CreateData(DT1, _ent.Connection);
        }
        public void UpdateData(SODueDateRevision _ent)
        {
            Docnum = _ent.DocNumber;
            Conn = _ent.Connection;//ADD CONN
            //trans = _ent.TransType;
            //ddate = _ent.DocDate;


            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add("Sales.SODueDateRevision", "cond", "DocNumber", _ent.DocNumber);
            DT1.Rows.Add("Sales.SODueDateRevision", "set", "DocDate", _ent.DocDate);
            DT1.Rows.Add("Sales.SODueDateRevision", "set", "SONumber", _ent.SONumber);
            DT1.Rows.Add("Sales.SODueDateRevision", "set", "Remarks", _ent.Remarks);
            DT1.Rows.Add("Sales.SODueDateRevision", "set", "OldDueDate", _ent.OldDueDate);
            DT1.Rows.Add("Sales.SODueDateRevision", "set", "NewDueDate", _ent.NewDueDate);


          
            DT1.Rows.Add("Sales.SODueDateRevision", "set", "LastEditedBy", _ent.LastEditedBy);
            DT1.Rows.Add("Sales.SODueDateRevision", "set", "LastEditedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            DT1.Rows.Add("Sales.SODueDateRevision", "set", "Field1", _ent.Field1);
            DT1.Rows.Add("Sales.SODueDateRevision", "set", "Field2", _ent.Field2);
            DT1.Rows.Add("Sales.SODueDateRevision", "set", "Field3", _ent.Field3);
            DT1.Rows.Add("Sales.SODueDateRevision", "set", "Field4", _ent.Field4);
            DT1.Rows.Add("Sales.SODueDateRevision", "set", "Field5", _ent.Field5);
            DT1.Rows.Add("Sales.SODueDateRevision", "set", "Field6", _ent.Field6);
            DT1.Rows.Add("Sales.SODueDateRevision", "set", "Field7", _ent.Field7);
            DT1.Rows.Add("Sales.SODueDateRevision", "set", "Field8", _ent.Field8);
            DT1.Rows.Add("Sales.SODueDateRevision", "set", "Field9", _ent.Field9);

            string strErr = Gears.UpdateData(DT1, _ent.Connection);
            Functions.AuditTrail("SLSORM", Docnum, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "UPDATE", _ent.Connection);
        }
        public void DeleteData(SODueDateRevision _ent)
        {
            Docnum = _ent.DocNumber;
            Conn = _ent.Connection;//ADD CONN

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Sales.SODueDateRevision", "cond", "DocNumber", _ent.DocNumber);
            Gears.DeleteData(DT1, _ent.Connection);
            Functions.AuditTrail("SLSORM", Docnum, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "DELETE", _ent.Connection);
        }
    }
}
