using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GearsLibrary;
using System.Data;

namespace Entity
{
    public class BudgetAllocation
    {
        private static string Conn;//ADD CONN
        public virtual string Connection { get; set; }//ADD CONN
        private static string Docnum;
        public virtual string DocNumber { get; set; }
        public virtual string Year { get; set; }
        public virtual string ReferenceBudget { get; set; }
        public virtual string BudgetStatus { get; set; }
        public virtual bool IsInactive { get; set; }

        

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
        public virtual string ApprovedBy { get; set; }
        public virtual string ApprovedDate { get; set; }
        public virtual bool IsWithDetail { get; set; }
        public virtual bool IsValidated { get; set; }

        public virtual IList<BudgetAllocationDetail> Detail { get; set; }
        public class BudgetAllocationDetail
        {
            public virtual BudgetAllocationDetail Parent { get; set; }
            public virtual string DocNumber { get; set; }
            public virtual string LineNumber { get; set; }
            public virtual string AccountCode { get; set; }
            public virtual string SubsiCode { get; set; }
            public virtual string CostCenterCode { get; set; }
            public virtual decimal Amount { get; set; }
            public virtual int Month { get; set; }


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
                    a = Gears.RetriveData2("select * from Accounting.BudgetAllocationDetail where DocNumber='" + DocNumber + "' order by LineNumber", Conn);
                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }
           public void AddBudgetAllocationDetail(BudgetAllocationDetail BudgetAllocationDetail)
           {
               int linenum = 0;


               DataTable count = Gears.RetriveData2("select max(convert(int,LineNumber)) as LineNumber from Accounting.BudgetAllocationDetail where docnumber = '" + Docnum + "'", Conn);

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
               DT1.Rows.Add("Accounting.BudgetAllocationDetail", "0", "DocNumber", Docnum);
               DT1.Rows.Add("Accounting.BudgetAllocationDetail", "0", "LineNumber", strLine);
               DT1.Rows.Add("Accounting.BudgetAllocationDetail", "0", "AccountCode", BudgetAllocationDetail.AccountCode);
               DT1.Rows.Add("Accounting.BudgetAllocationDetail", "0", "SubsiCode", BudgetAllocationDetail.SubsiCode);
               DT1.Rows.Add("Accounting.BudgetAllocationDetail", "0", "CostCenterCode", BudgetAllocationDetail.CostCenterCode);
               DT1.Rows.Add("Accounting.BudgetAllocationDetail", "0", "Amount", BudgetAllocationDetail.Amount);
               DT1.Rows.Add("Accounting.BudgetAllocationDetail", "0", "Month", BudgetAllocationDetail.Month);


               DT1.Rows.Add("Accounting.BudgetAllocationDetail", "0", "Field1", BudgetAllocationDetail.Field1);
               DT1.Rows.Add("Accounting.BudgetAllocationDetail", "0", "Field2", BudgetAllocationDetail.Field2);
               DT1.Rows.Add("Accounting.BudgetAllocationDetail", "0", "Field3", BudgetAllocationDetail.Field3);
               DT1.Rows.Add("Accounting.BudgetAllocationDetail", "0", "Field4", BudgetAllocationDetail.Field4);
               DT1.Rows.Add("Accounting.BudgetAllocationDetail", "0", "Field5", BudgetAllocationDetail.Field5);
               DT1.Rows.Add("Accounting.BudgetAllocationDetail", "0", "Field6", BudgetAllocationDetail.Field6);
               DT1.Rows.Add("Accounting.BudgetAllocationDetail", "0", "Field7", BudgetAllocationDetail.Field7);
               DT1.Rows.Add("Accounting.BudgetAllocationDetail", "0", "Field8", BudgetAllocationDetail.Field8);
               DT1.Rows.Add("Accounting.BudgetAllocationDetail", "0", "Field9", BudgetAllocationDetail.Field9);

               DT2.Rows.Add("Accounting.BudgetAllocation", "cond", "DocNumber", Docnum);
               DT2.Rows.Add("Accounting.BudgetAllocation", "set", "IsWithDetail", "True");

               Gears.CreateData(DT1, Conn);
               Gears.UpdateData(DT2, Conn);


           }
           public void UpdateBudgetAllocationDetail(BudgetAllocationDetail BudgetAllocationDetail)
           {



               Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
               DT1.Rows.Add("Accounting.BudgetAllocationDetail", "cond", "DocNumber", BudgetAllocationDetail.DocNumber);
               DT1.Rows.Add("Accounting.BudgetAllocationDetail", "cond", "LineNumber", BudgetAllocationDetail.LineNumber);
               DT1.Rows.Add("Accounting.BudgetAllocationDetail", "set", "AccountCode", BudgetAllocationDetail.AccountCode);
               DT1.Rows.Add("Accounting.BudgetAllocationDetail", "set", "SubsiCode", BudgetAllocationDetail.SubsiCode);
               DT1.Rows.Add("Accounting.BudgetAllocationDetail", "set", "CostCenterCode", BudgetAllocationDetail.CostCenterCode);
               DT1.Rows.Add("Accounting.BudgetAllocationDetail", "set", "Amount", BudgetAllocationDetail.Amount);
               DT1.Rows.Add("Accounting.BudgetAllocationDetail", "set", "Month", BudgetAllocationDetail.Month);


               DT1.Rows.Add("Accounting.BudgetAllocationDetail", "set", "Field1", BudgetAllocationDetail.Field1);
               DT1.Rows.Add("Accounting.BudgetAllocationDetail", "set", "Field2", BudgetAllocationDetail.Field2);
               DT1.Rows.Add("Accounting.BudgetAllocationDetail", "set", "Field3", BudgetAllocationDetail.Field3);
               DT1.Rows.Add("Accounting.BudgetAllocationDetail", "set", "Field4", BudgetAllocationDetail.Field4);
               DT1.Rows.Add("Accounting.BudgetAllocationDetail", "set", "Field5", BudgetAllocationDetail.Field5);
               DT1.Rows.Add("Accounting.BudgetAllocationDetail", "set", "Field6", BudgetAllocationDetail.Field6);
               DT1.Rows.Add("Accounting.BudgetAllocationDetail", "set", "Field7", BudgetAllocationDetail.Field7);
               DT1.Rows.Add("Accounting.BudgetAllocationDetail", "set", "Field8", BudgetAllocationDetail.Field8);
               DT1.Rows.Add("Accounting.BudgetAllocationDetail", "set", "Field9", BudgetAllocationDetail.Field9);

               Gears.UpdateData(DT1, Conn);
           }
           public void DeleteBudgetAllocationDetail(BudgetAllocationDetail BudgetAllocationDetail)
           {


               Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
               Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();

               DT1.Rows.Add("Accounting.BudgetAllocationDetail", "cond", "DocNumber", BudgetAllocationDetail.DocNumber);
               DT1.Rows.Add("Accounting.BudgetAllocationDetail", "cond", "LineNumber", BudgetAllocationDetail.LineNumber);


               Gears.DeleteData(DT1, Conn);


               DataTable count = Gears.RetriveData2("select * from Accounting.BudgetAllocationDetail where docnumber = '" + Docnum + "'", Conn);

               if (count.Rows.Count < 1)
               {
                   DT2.Rows.Add("Accounting.BudgetAllocationDetail", "cond", "DocNumber", Docnum);
                   DT2.Rows.Add("Accounting.BudgetAllocationDetail", "set", "IsWithDetail", "False");
                   Gears.UpdateData(DT2, Conn);
               }

           }
        }
        public DataTable getdata(string DocNumber, string Conn)
        {
            DataTable a;

            //if (DocNumber != null)
            //{
            a = Gears.RetriveData2("select * from Accounting.BudgetAllocation where DocNumber = '" + DocNumber + "'", Conn);
            foreach (DataRow dtRow in a.Rows)
            {
                DocNumber = dtRow["DocNumber"].ToString();
                Year = dtRow["Year"].ToString();
                ReferenceBudget = dtRow["ReferenceBudget"].ToString();
                BudgetStatus = dtRow["BudgetStatus"].ToString();
                IsInactive = Convert.ToBoolean(Convert.IsDBNull(dtRow["IsInactive"]) ? false : dtRow["IsInactive"]);
                

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
                ApprovedBy = dtRow["ApprovedBy"].ToString();
                ApprovedDate = dtRow["ApprovedDate"].ToString();

    
            }

            return a;
        }
        public void InsertData(BudgetAllocation _ent)
        {
            Conn = _ent.Connection;     //ADD CONN
            Docnum = _ent.DocNumber;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add("Accounting.BudgetAllocation", "0", "DocNumber", _ent.DocNumber);
            DT1.Rows.Add("Accounting.BudgetAllocation", "0", "Year", _ent.Year);
            DT1.Rows.Add("Accounting.BudgetAllocation", "0", "ReferenceBudget", _ent.ReferenceBudget);
            DT1.Rows.Add("Accounting.BudgetAllocation", "0", "BudgetStatus", _ent.BudgetStatus);
            DT1.Rows.Add("Accounting.BudgetAllocation", "0", "IsInactive", _ent.IsInactive);
          

            DT1.Rows.Add("Accounting.BudgetAllocation", "0", "Field1", _ent.Field1);
            DT1.Rows.Add("Accounting.BudgetAllocation", "0", "Field2", _ent.Field2);
            DT1.Rows.Add("Accounting.BudgetAllocation", "0", "Field3", _ent.Field3);
            DT1.Rows.Add("Accounting.BudgetAllocation", "0", "Field4", _ent.Field4);
            DT1.Rows.Add("Accounting.BudgetAllocation", "0", "Field5", _ent.Field5);
            DT1.Rows.Add("Accounting.BudgetAllocation", "0", "Field6", _ent.Field6);
            DT1.Rows.Add("Accounting.BudgetAllocation", "0", "Field7", _ent.Field7);
            DT1.Rows.Add("Accounting.BudgetAllocation", "0", "Field8", _ent.Field8);
            DT1.Rows.Add("Accounting.BudgetAllocation", "0", "Field9", _ent.Field9);
            DT1.Rows.Add("Accounting.BudgetAllocation", "0", "IsWithDetail", "False");
            DT1.Rows.Add("Accounting.BudgetAllocation", "0", "AddedBy", _ent.AddedBy);
            DT1.Rows.Add("Accounting.BudgetAllocation", "0", "AddedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            Gears.CreateData(DT1, _ent.Connection); //KMM add Conn
        }

        public void UpdateData(BudgetAllocation _ent)
        {
            Conn = _ent.Connection;     //ADD CONN
            Docnum = _ent.DocNumber;
            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add("Accounting.BudgetAllocation", "cond", "DocNumber", _ent.DocNumber);
            DT1.Rows.Add("Accounting.BudgetAllocation", "set", "Year", _ent.Year);
            DT1.Rows.Add("Accounting.BudgetAllocation", "set", "ReferenceBudget", _ent.ReferenceBudget);
            if (!String.IsNullOrEmpty(_ent.ReferenceBudget))
            {
                DT1.Rows.Add("Accounting.BudgetAllocation", "set", "BudgetStatus", "Revised");
            }
            else
            {
                DT1.Rows.Add("Accounting.BudgetAllocation", "set", "BudgetStatus", "Draft");
            }
            DT1.Rows.Add("Accounting.BudgetAllocation", "set", "IsInactive", _ent.IsInactive);

            DT1.Rows.Add("Accounting.BudgetAllocation", "set", "Field1", _ent.Field1);
            DT1.Rows.Add("Accounting.BudgetAllocation", "set", "Field2", _ent.Field2);
            DT1.Rows.Add("Accounting.BudgetAllocation", "set", "Field3", _ent.Field3);
            DT1.Rows.Add("Accounting.BudgetAllocation", "set", "Field4", _ent.Field4);
            DT1.Rows.Add("Accounting.BudgetAllocation", "set", "Field5", _ent.Field5);
            DT1.Rows.Add("Accounting.BudgetAllocation", "set", "Field6", _ent.Field6);
            DT1.Rows.Add("Accounting.BudgetAllocation", "set", "Field7", _ent.Field7);
            DT1.Rows.Add("Accounting.BudgetAllocation", "set", "Field8", _ent.Field8);
            DT1.Rows.Add("Accounting.BudgetAllocation", "set", "Field9", _ent.Field9);
            DT1.Rows.Add("Accounting.BudgetAllocation", "set", "LastEditedBy", _ent.LastEditedBy);
            DT1.Rows.Add("Accounting.BudgetAllocation", "set", "LastEditedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            string strErr = Gears.UpdateData(DT1, _ent.Connection);
            Functions.AuditTrail("ACTBAL", Docnum, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "UPDATE", _ent.Connection);
        }
        public void DeleteData(BudgetAllocation _ent)
        {
            Docnum = _ent.DocNumber;
            Conn = _ent.Connection;     //ADD CONN

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Accounting.BudgetAllocation", "cond", "DocNumber", _ent.DocNumber);
            Gears.DeleteData(DT1,  _ent.Connection); //KMM add Conn
            Functions.AuditTrail("ACTBAL", Docnum, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "DELETE", _ent.DocNumber);
        }
    }
}
