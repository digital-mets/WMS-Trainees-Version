using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GearsLibrary;
using System.Data;

namespace Entity
{
    public class InitialWIPIN
    {
        private static string Docnum;

        private static string Conn;//ADD CONN
        public virtual string Connection { get; set; }//ADD CONN
        public virtual string DocNumber { get; set; }
        public virtual string DocDate { get; set; }
        public virtual string Type { get; set; }
     
        public virtual string JobOrder { get; set; }
        public virtual string Step { get; set; }
        public virtual string WorkCenter { get; set; }

        public virtual string ProductCode { get; set; }

        public virtual string ProductColor { get; set; }
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

            public virtual InitialWIPIN Parent { get; set; }
            public virtual string DocNumber { get; set; }
            public virtual string LineNumber { get; set; }

            public virtual string SizeCode { get; set; }
            public virtual decimal Qty { get; set; }

            public virtual decimal JOBreakdown { get; set; }

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
                    a = Gears.RetriveData2("select * from Production.WISizeBreakDown where DocNumber='" + DocNumber + "' order by LineNumber", Conn);
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


                DataTable count = Gears.RetriveData2("select max(convert(int,LineNumber)) as LineNumber from Production.WISizeBreakDown where docnumber = '" + Docnum + "'", Conn);

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

                DT1.Rows.Add("Production.WISizeBreakDown", "0", "DocNumber", Docnum);
                DT1.Rows.Add("Production.WISizeBreakDown", "0", "LineNumber", strLine);
                DT1.Rows.Add("Production.WISizeBreakDown", "0", "SizeCode", WISizeBreakDown.SizeCode);
                DT1.Rows.Add("Production.WISizeBreakDown", "0", "Qty", WISizeBreakDown.Qty);
                DT1.Rows.Add("Production.WISizeBreakDown", "0", "JOBreakdown", WISizeBreakDown.JOBreakdown);

                DT1.Rows.Add("Production.WISizeBreakDown", "0", "Field1", WISizeBreakDown.Field1);
                DT1.Rows.Add("Production.WISizeBreakDown", "0", "Field2", WISizeBreakDown.Field2);
                DT1.Rows.Add("Production.WISizeBreakDown", "0", "Field3", WISizeBreakDown.Field3);
                DT1.Rows.Add("Production.WISizeBreakDown", "0", "Field4", WISizeBreakDown.Field4);
                DT1.Rows.Add("Production.WISizeBreakDown", "0", "Field5", WISizeBreakDown.Field5);
                DT1.Rows.Add("Production.WISizeBreakDown", "0", "Field6", WISizeBreakDown.Field6);
                DT1.Rows.Add("Production.WISizeBreakDown", "0", "Field7", WISizeBreakDown.Field7);
                DT1.Rows.Add("Production.WISizeBreakDown", "0", "Field8", WISizeBreakDown.Field8);
                DT1.Rows.Add("Production.WISizeBreakDown", "0", "Field9", WISizeBreakDown.Field9);



                //DT2.Rows.Add("Production.InitialWipIN", "cond", "DocNumber", Docnum);
                //DT2.Rows.Add("Production.InitialWipIN", "set", "IsWithDetail", "True");

                Gears.CreateData(DT1, Conn);
                //Gears.UpdateData(DT2);

            }
            public void UpdateWISizeBreakDown(WISizeBreakDown WISizeBreakDown)
            {
                int linenum = 0;

                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

                DT1.Rows.Add("Production.WISizeBreakDown", "cond", "DocNumber", Docnum);
                DT1.Rows.Add("Production.WISizeBreakDown", "cond", "LineNumber", WISizeBreakDown.LineNumber);
                DT1.Rows.Add("Production.WISizeBreakDown", "set", "SizeCode", WISizeBreakDown.SizeCode);
                DT1.Rows.Add("Production.WISizeBreakDown", "set", "Qty", WISizeBreakDown.Qty);
                DT1.Rows.Add("Production.WISizeBreakDown", "set", "JOBreakdown", WISizeBreakDown.JOBreakdown);


                DT1.Rows.Add("Production.WISizeBreakDown", "set", "Field1", WISizeBreakDown.Field1);
                DT1.Rows.Add("Production.WISizeBreakDown", "set", "Field2", WISizeBreakDown.Field2);
                DT1.Rows.Add("Production.WISizeBreakDown", "set", "Field3", WISizeBreakDown.Field3);
                DT1.Rows.Add("Production.WISizeBreakDown", "set", "Field4", WISizeBreakDown.Field4);
                DT1.Rows.Add("Production.WISizeBreakDown", "set", "Field5", WISizeBreakDown.Field5);
                DT1.Rows.Add("Production.WISizeBreakDown", "set", "Field6", WISizeBreakDown.Field6);
                DT1.Rows.Add("Production.WISizeBreakDown", "set", "Field7", WISizeBreakDown.Field7);
                DT1.Rows.Add("Production.WISizeBreakDown", "set", "Field8", WISizeBreakDown.Field8);
                DT1.Rows.Add("Production.WISizeBreakDown", "set", "Field9", WISizeBreakDown.Field9);

                Gears.UpdateData(DT1, Conn);




            }
            public void DeleteWISizeBreakDown(WISizeBreakDown WISizeBreakDown)
            {

                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT3 = new Gears.CRUDdatatable();
                DT1.Rows.Add("Production.WISizeBreakDown", "cond", "DocNumber", Docnum);
                DT1.Rows.Add("Production.WISizeBreakDown", "cond", "LineNumber", WISizeBreakDown.LineNumber);


                Gears.DeleteData(DT1, Conn);


                //DataTable count = Gears.RetriveData2("select * from Production.WISizeBreakDown where docnumber = '" + Docnum + "'");

                //if (count.Rows.Count < 1)
                //{
                //    DT2.Rows.Add("Production.InitialWipIN", "cond", "DocNumber", Docnum);
                //    DT2.Rows.Add("Production.InitialWipIN", "set", "IsWithDetail", "False");
                //    Gears.UpdateData(DT2);
                //}


            }



          

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
                    a = Gears.RetriveData2("select RTransType,REFDocNumber,RMenuID,RIGHT(B.CommandString, LEN(B.CommandString) - 1) as RCommandString,A.TransType,DocNumber,A.MenuID,RIGHT(C.CommandString, LEN(C.CommandString) - 1) as CommandString from  IT.ReferenceTrans  A "
                                            + " inner join IT.MainMenu B"
                                            + " on A.RMenuID =B.ModuleID "
                                            + " inner join IT.MainMenu C "
                                            + " on A.MenuID = C.ModuleID "
                                            + "  where (DocNumber='" + DocNumber + "' OR   REFDocNumber='" + DocNumber + "') and  (RTransType='PRDIWN' OR  A.TransType='PRDIWN') ", Conn);
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
            a = Gears.RetriveData2("select * from Production.InitialWipIN where DocNumber = '" + DocNumber + "'", Conn);
            foreach (DataRow dtRow in a.Rows)
            {
                DocNumber = dtRow["DocNumber"].ToString();
                DocDate = dtRow["Docdate"].ToString();
                Type = dtRow["Type"].ToString();
                Step = dtRow["Step"].ToString();
                JobOrder = dtRow["JobOrder"].ToString();
                WorkCenter = dtRow["WorkCenter"].ToString();
                DRDocNumber = dtRow["DRDocNumber"].ToString();
                TotalQuantity = Convert.ToDecimal(Convert.IsDBNull(dtRow["TotalQuantity"]) ? 0 : dtRow["TotalQuantity"]);
                Remarks = dtRow["Remarks"].ToString();
                ProductCode = dtRow["ProductCode"].ToString();
                ProductColor = dtRow["ProductColor"].ToString();

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
        public void InsertData(InitialWIPIN _ent)
        {
            Docnum = _ent.DocNumber;
            Conn = _ent.Connection;     //ADD CONN

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();


            DT1.Rows.Add("Production.InitialWipIN", "0", "DocNumber", _ent.DocNumber);
            DT1.Rows.Add("Production.InitialWipIN", "0", "DocDate", _ent.DocDate);
            DT1.Rows.Add("Production.InitialWipIN", "0", "Type  ", _ent.Type);
            DT1.Rows.Add("Production.InitialWipIN", "0", "JobOrder ", _ent.JobOrder);
            DT1.Rows.Add("Production.InitialWipIN", "0", "Step ", _ent.Step);
            DT1.Rows.Add("Production.InitialWipIN", "0", "WorkCenter ", _ent.WorkCenter);
            DT1.Rows.Add("Production.InitialWipIN", "0", "ProductCode ", _ent.ProductCode);
            DT1.Rows.Add("Production.InitialWipIN", "0", "ProductColor ", _ent.ProductColor);
            DT1.Rows.Add("Production.InitialWipIN", "0", "TotalQuantity ", _ent.TotalQuantity);
 
            DT1.Rows.Add("Production.InitialWipIN", "0", "Remarks ", _ent.Remarks);

         

            DT1.Rows.Add("Production.InitialWipIN", "0", "Field1", _ent.Field1);
            DT1.Rows.Add("Production.InitialWipIN", "0", "Field2", _ent.Field2);
            DT1.Rows.Add("Production.InitialWipIN", "0", "Field3", _ent.Field3);
            DT1.Rows.Add("Production.InitialWipIN", "0", "Field4", _ent.Field4);
            DT1.Rows.Add("Production.InitialWipIN", "0", "Field5", _ent.Field5);
            DT1.Rows.Add("Production.InitialWipIN", "0", "Field6", _ent.Field6);
            DT1.Rows.Add("Production.InitialWipIN", "0", "Field7", _ent.Field7);
            DT1.Rows.Add("Production.InitialWipIN", "0", "Field8", _ent.Field8);
            DT1.Rows.Add("Production.InitialWipIN", "0", "Field9", _ent.Field9);
            DT1.Rows.Add("Production.InitialWipIN", "0", "SubmittedBy", "");
            DT1.Rows.Add("Production.InitialWipIN", "0", "IsWithDetail", "False");


            Gears.CreateData(DT1, _ent.Connection);

        }

        public void UpdateData(InitialWIPIN _ent)
        {
            Docnum = _ent.DocNumber;
            Conn = _ent.Connection;     //ADD CONN

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Production.InitialWipIN", "cond", "DocNumber", _ent.DocNumber);

            DT1.Rows.Add("Production.InitialWipIN", "set", "DocDate", _ent.DocDate);
            DT1.Rows.Add("Production.InitialWipIN", "set", "Type  ", _ent.Type);
            DT1.Rows.Add("Production.InitialWipIN", "set", "JobOrder ", _ent.JobOrder);
            DT1.Rows.Add("Production.InitialWipIN", "set", "Step ", _ent.Step);
            DT1.Rows.Add("Production.InitialWipIN", "set", "WorkCenter ", _ent.WorkCenter);
            DT1.Rows.Add("Production.InitialWipIN", "set", "ProductCode ", _ent.ProductCode);
            DT1.Rows.Add("Production.InitialWipIN", "set", "ProductColor ", _ent.ProductColor);
            DT1.Rows.Add("Production.InitialWipIN", "set", "TotalQuantity ", _ent.TotalQuantity);
            DT1.Rows.Add("Production.InitialWipIN", "set", "DRDocNumber ", _ent.DRDocNumber);
            DT1.Rows.Add("Production.InitialWipIN", "set", "Remarks ", _ent.Remarks);

            DT1.Rows.Add("Production.InitialWipIN", "set", "Field1", _ent.Field1);
            DT1.Rows.Add("Production.InitialWipIN", "set", "Field2", _ent.Field2);
            DT1.Rows.Add("Production.InitialWipIN", "set", "Field3", _ent.Field3);
            DT1.Rows.Add("Production.InitialWipIN", "set", "Field4", _ent.Field4);
            DT1.Rows.Add("Production.InitialWipIN", "set", "Field5", _ent.Field5);
            DT1.Rows.Add("Production.InitialWipIN", "set", "Field6", _ent.Field6);
            DT1.Rows.Add("Production.InitialWipIN", "set", "Field7", _ent.Field7);
            DT1.Rows.Add("Production.InitialWipIN", "set", "Field8", _ent.Field8);
            DT1.Rows.Add("Production.InitialWipIN", "set", "Field9", _ent.Field9);
            DT1.Rows.Add("Production.InitialWipIN", "set", "SubmittedBy", "");
            DT1.Rows.Add("Production.InitialWipIN", "set", "LastEditedBy", _ent.LastEditedBy);
            DT1.Rows.Add("Production.InitialWipIN", "set", "LastEditedDate",_ent.LastEditedDate);
            Gears.UpdateData(DT1, _ent.Connection);

            Functions.AuditTrail("PRDIWN", Docnum, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "UPDATE", _ent.Connection);
        }

        public void DeleteData(InitialWIPIN _ent)
        {
            Docnum = _ent.DocNumber;
            Conn = _ent.Connection;     //ADD CONN

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Production.InitialWipIN", "cond", "DocNumber", _ent.DocNumber);
            Gears.DeleteData(DT1, _ent.Connection);
            Functions.AuditTrail("PRDIWN", Docnum, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "DELETE", _ent.Connection);
        }
        public class JournalEntry
        {
            public virtual InitialWIPIN Parent { get; set; }
            public virtual string AccountCode { get; set; }
            public virtual string AccountDescription { get; set; }
            public virtual string SubsidiaryCode { get; set; }
            public virtual string SubsidiaryDescription { get; set; }
            public virtual string ProfitCenter { get; set; }
            public virtual string CostCenter { get; set; }
            public virtual string Debit { get; set; }
            public virtual string Credit { get; set; }

            public virtual string BizPartnerCode { get; set; } //joseph - 12-1-2017
            public DataTable getJournalEntry(string DocNumber, string Conn)
            {

                DataTable a;
                try
                {
                    a = Gears.RetriveData2("SELECT A.AccountCode, B.Description AS AccountDescription, A.SubsiCode AS SubsidiaryCode, C.Description AS SubsidiaryDescription, "
                    + " ProfitCenterCode AS ProfitCenter, CostCenterCode AS CostCenter, Convert(varchar,Convert(money,DebitAmount),1) AS Debit, Convert(varchar,Convert(money,CreditAmount),1) AS Credit,A.BizPartnerCode  FROM Accounting.GeneralLedger A "
                    + " INNER JOIN Accounting.ChartOfAccount B ON A.AccountCode = B.AccountCode "
                    + " INNER JOIN Accounting.GLSubsiCode C ON A.SubsiCode = C.SubsiCode "
                    + " AND A.AccountCode = C.AccountCode WHERE A.DocNumber = '" + DocNumber + "' AND (TransType ='PRDIWN') ", Conn);

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
}
