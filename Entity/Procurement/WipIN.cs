using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GearsLibrary;
using System.Data;

namespace Entity
{
    public class WipIN
    {
        private static string Docnum;

        private static string Conn;//ADD CONN
        public virtual string Connection { get; set; }//ADD CONN
        public virtual string DocNumber { get; set; }
        public virtual string DocDate { get; set; }
        public virtual string Type { get; set; }
        public virtual string WorkCenter { get; set; }
        public virtual string ServiceOrder { get; set; }
        public virtual decimal TotalQuantity { get; set; }
        public virtual string DRDocNumber { get; set; }
        public virtual string Remarks { get; set; }
      
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
        public virtual string SubmittedBy { get; set; }
        public virtual string SubmittedDate { get; set; }

        public virtual IList<WISizeBreakDown> Detail { get; set; }


        public class WISizeBreakDown
        {

            public virtual WipIN Parent { get; set; }
            public virtual string DocNumber { get; set; }
            public virtual string LineNumber { get; set; }

            public virtual string SizeCode { get; set; }
            public virtual decimal Qty { get; set; }

            public virtual decimal SVOBreakdown { get; set; }

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
                    a = Gears.RetriveData2("select * from Procurement.WISizeBreakDown where DocNumber='" + DocNumber + "' order by LineNumber", Conn);
                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }
            public void AddWISizeBreakDown(WISizeBreakDown WISizeBreakDown)
            {

                int linenum = 0;


                DataTable count = Gears.RetriveData2("select max(convert(int,LineNumber)) as LineNumber from Procurement.WISizeBreakDown where docnumber = '" + Docnum + "'", Conn);

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

                DT1.Rows.Add("Procurement.WISizeBreakDown", "0", "DocNumber", Docnum);
                DT1.Rows.Add("Procurement.WISizeBreakDown", "0", "LineNumber", strLine);
                DT1.Rows.Add("Procurement.WISizeBreakDown", "0", "SizeCode", WISizeBreakDown.SizeCode);
                DT1.Rows.Add("Procurement.WISizeBreakDown", "0", "Qty", WISizeBreakDown.Qty);
                DT1.Rows.Add("Procurement.WISizeBreakDown", "0", "SVOBreakdown", WISizeBreakDown.SVOBreakdown);

                DT1.Rows.Add("Procurement.WISizeBreakDown", "0", "Field1", WISizeBreakDown.Field1);
                DT1.Rows.Add("Procurement.WISizeBreakDown", "0", "Field2", WISizeBreakDown.Field2);
                DT1.Rows.Add("Procurement.WISizeBreakDown", "0", "Field3", WISizeBreakDown.Field3);
                DT1.Rows.Add("Procurement.WISizeBreakDown", "0", "Field4", WISizeBreakDown.Field4);
                DT1.Rows.Add("Procurement.WISizeBreakDown", "0", "Field5", WISizeBreakDown.Field5);
                DT1.Rows.Add("Procurement.WISizeBreakDown", "0", "Field6", WISizeBreakDown.Field6);
                DT1.Rows.Add("Procurement.WISizeBreakDown", "0", "Field7", WISizeBreakDown.Field7);
                DT1.Rows.Add("Procurement.WISizeBreakDown", "0", "Field8", WISizeBreakDown.Field8);
                DT1.Rows.Add("Procurement.WISizeBreakDown", "0", "Field9", WISizeBreakDown.Field9);



                //DT2.Rows.Add("Procurement.WipIN", "cond", "DocNumber", Docnum);
                //DT2.Rows.Add("Procurement.WipIN", "set", "IsWithDetail", "True");

                Gears.CreateData(DT1, Conn);
                //Gears.UpdateData(DT2);

            }
            public void UpdateWISizeBreakDown(WISizeBreakDown WISizeBreakDown)
            {
                int linenum = 0;

                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

                DT1.Rows.Add("Procurement.WISizeBreakDown", "cond", "DocNumber", Docnum);
                DT1.Rows.Add("Procurement.WISizeBreakDown", "cond", "LineNumber", WISizeBreakDown.LineNumber);
                DT1.Rows.Add("Procurement.WISizeBreakDown", "set", "SizeCode", WISizeBreakDown.SizeCode);
                DT1.Rows.Add("Procurement.WISizeBreakDown", "set", "Qty", WISizeBreakDown.Qty);
                DT1.Rows.Add("Procurement.WISizeBreakDown", "set", "SVOBreakdown", WISizeBreakDown.SVOBreakdown);


                DT1.Rows.Add("Procurement.WISizeBreakDown", "set", "Field1", WISizeBreakDown.Field1);
                DT1.Rows.Add("Procurement.WISizeBreakDown", "set", "Field2", WISizeBreakDown.Field2);
                DT1.Rows.Add("Procurement.WISizeBreakDown", "set", "Field3", WISizeBreakDown.Field3);
                DT1.Rows.Add("Procurement.WISizeBreakDown", "set", "Field4", WISizeBreakDown.Field4);
                DT1.Rows.Add("Procurement.WISizeBreakDown", "set", "Field5", WISizeBreakDown.Field5);
                DT1.Rows.Add("Procurement.WISizeBreakDown", "set", "Field6", WISizeBreakDown.Field6);
                DT1.Rows.Add("Procurement.WISizeBreakDown", "set", "Field7", WISizeBreakDown.Field7);
                DT1.Rows.Add("Procurement.WISizeBreakDown", "set", "Field8", WISizeBreakDown.Field8);
                DT1.Rows.Add("Procurement.WISizeBreakDown", "set", "Field9", WISizeBreakDown.Field9);

                Gears.UpdateData(DT1, Conn);




            }
            public void DeleteWISizeBreakDown(WISizeBreakDown WISizeBreakDown)
            {

                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT3 = new Gears.CRUDdatatable();
                DT1.Rows.Add("Procurement.WISizeBreakDown", "cond", "DocNumber", WISizeBreakDown.DocNumber);
                DT1.Rows.Add("Procurement.WISizeBreakDown", "cond", "LineNumber", WISizeBreakDown.LineNumber);


                Gears.DeleteData(DT1, Conn);


                //DataTable count = Gears.RetriveData2("select * from Procurement.WISizeBreakDown where docnumber = '" + Docnum + "'");

                //if (count.Rows.Count < 1)
                //{
                //    DT2.Rows.Add("Procurement.WipIN", "cond", "DocNumber", Docnum);
                //    DT2.Rows.Add("Procurement.WipIN", "set", "IsWithDetail", "False");
                //    Gears.UpdateData(DT2);
                //}


            }



            public class RefTransaction
            {
                public virtual WipIN Parent { get; set; }
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
                                                + " on A.RMenuID =B.TransType "
                                                + " inner join IT.MainMenu C "
                                                + " on A.MenuID = C.TransType "
                                                + "  where (DocNumber='" + DocNumber + "' OR   REFDocNumber='" + DocNumber + "') and  (RTransType='PRCWIN' OR  A.TransType='PRCWIN') ", Conn);
                        return a;
                    }
                    catch (Exception e)
                    {
                        a = null;
                        return a;
                    }
                }
            }

        }

        public DataTable getdata(string DocNumber, string Conn)
        {
            DataTable a;

            //if (DocNumber != null)
            //{
            a = Gears.RetriveData2("select * from Procurement.WipIN where DocNumber = '" + DocNumber + "'", Conn);
            foreach (DataRow dtRow in a.Rows)
            {
                DocNumber = dtRow["DocNumber"].ToString();
                DocDate = dtRow["Docdate"].ToString();
                Type = dtRow["Type"].ToString();
                ServiceOrder = dtRow["ServiceOrder"].ToString();
                WorkCenter = dtRow["WorkCenter"].ToString();
                DRDocNumber = dtRow["DRDocNumber"].ToString();
                TotalQuantity = Convert.ToDecimal(Convert.IsDBNull(dtRow["TotalQuantity"]) ? 0 : dtRow["TotalQuantity"]);
                Remarks = dtRow["Remarks"].ToString();


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
            //}
            //else
            //{
            //    a = Gears.RetriveData2("select '' as DocNumber,'' as Docdate,'' as CustomerCode,'' as DeliveryDate ,'' as PicklistType ,'' as Type " +
            //         ",'' as StorerKey ,'' as WarehouseCode ,'' as PlantCode ,'' as ReferenceNo ,'' as Remarks ,'' as OutboundNo  ,0 as IsAutoPick  ,'' as Field1" +
            //   ",'' as Field2,'' as Field3,'' as Field4,'' as Field5,'' as Field6,'' as Field7,'' as Field8,'' as Field9");
            //}

            return a;
        }
        public void InsertData(WipIN _ent)
        {
            Docnum = _ent.DocNumber;
            Conn = _ent.Connection;     //ADD CONN

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();


            DT1.Rows.Add("Procurement.WipIN", "0", "DocNumber", _ent.DocNumber);
            DT1.Rows.Add("Procurement.WipIN", "0", "DocDate", _ent.DocDate);
            DT1.Rows.Add("Procurement.WipIN", "0", "Type  ", _ent.Type);
            DT1.Rows.Add("Procurement.WipIN", "0", "ServiceOrder ", _ent.ServiceOrder);
            DT1.Rows.Add("Procurement.WipIN", "0", "WorkCenter ", _ent.WorkCenter);
            DT1.Rows.Add("Procurement.WipIN", "0", "TotalQuantity ", _ent.TotalQuantity);
 
            DT1.Rows.Add("Procurement.WipIN", "0", "Remarks ", _ent.Remarks);

         

            DT1.Rows.Add("Procurement.WipIN", "0", "Field1", _ent.Field1);
            DT1.Rows.Add("Procurement.WipIN", "0", "Field2", _ent.Field2);
            DT1.Rows.Add("Procurement.WipIN", "0", "Field3", _ent.Field3);
            DT1.Rows.Add("Procurement.WipIN", "0", "Field4", _ent.Field4);
            DT1.Rows.Add("Procurement.WipIN", "0", "Field5", _ent.Field5);
            DT1.Rows.Add("Procurement.WipIN", "0", "Field6", _ent.Field6);
            DT1.Rows.Add("Procurement.WipIN", "0", "Field7", _ent.Field7);
            DT1.Rows.Add("Procurement.WipIN", "0", "Field8", _ent.Field8);
            DT1.Rows.Add("Procurement.WipIN", "0", "Field9", _ent.Field9);
            DT1.Rows.Add("Procurement.WipIN", "0", "SubmittedBy", "");
            DT1.Rows.Add("Procurement.WipIN", "0", "IsWithDetail", "False");


            Gears.CreateData(DT1, _ent.Connection);

        }

        public void UpdateData(WipIN _ent)
        {
            Docnum = _ent.DocNumber;
            Conn = _ent.Connection;     //ADD CONN

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Procurement.WipIN", "cond", "DocNumber", _ent.DocNumber);

            DT1.Rows.Add("Procurement.WipIN", "set", "DocDate", _ent.DocDate);
            DT1.Rows.Add("Procurement.WipIN", "set", "Type  ", _ent.Type);
            DT1.Rows.Add("Procurement.WipIN", "set", "ServiceOrder ", _ent.ServiceOrder);
            DT1.Rows.Add("Procurement.WipIN", "set", "WorkCenter ", _ent.WorkCenter);
            DT1.Rows.Add("Procurement.WipIN", "set", "TotalQuantity ", _ent.TotalQuantity);
            DT1.Rows.Add("Procurement.WipIN", "set", "DRDocNumber ", _ent.DRDocNumber);
            DT1.Rows.Add("Procurement.WipIN", "set", "Remarks ", _ent.Remarks);

            DT1.Rows.Add("Procurement.WipIN", "set", "Field1", _ent.Field1);
            DT1.Rows.Add("Procurement.WipIN", "set", "Field2", _ent.Field2);
            DT1.Rows.Add("Procurement.WipIN", "set", "Field3", _ent.Field3);
            DT1.Rows.Add("Procurement.WipIN", "set", "Field4", _ent.Field4);
            DT1.Rows.Add("Procurement.WipIN", "set", "Field5", _ent.Field5);
            DT1.Rows.Add("Procurement.WipIN", "set", "Field6", _ent.Field6);
            DT1.Rows.Add("Procurement.WipIN", "set", "Field7", _ent.Field7);
            DT1.Rows.Add("Procurement.WipIN", "set", "Field8", _ent.Field8);
            DT1.Rows.Add("Procurement.WipIN", "set", "Field9", _ent.Field9);
            DT1.Rows.Add("Procurement.WipIN", "set", "SubmittedBy", "");
            DT1.Rows.Add("Procurement.WipIN", "set", "LastEditedBy", _ent.LastEditedBy);
            DT1.Rows.Add("Procurement.WipIN", "set", "LastEditedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            Gears.UpdateData(DT1, _ent.Connection);

            Functions.AuditTrail("PRCWIN", Docnum, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "UPDATE", _ent.Connection);
        }

        public void DeleteData(WipIN _ent)
        {
            Docnum = _ent.DocNumber;
            Conn = _ent.Connection;     //ADD CONN

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Procurement.WipIN", "cond", "DocNumber", _ent.DocNumber);
            Gears.DeleteData(DT1, _ent.Connection);
            Functions.AuditTrail("PRCWIN", Docnum, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "DELETE", _ent.Connection);
        }


    }
}
