using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GearsLibrary;
using System.Data;

namespace Entity
{
    public class ChartOfAccounts
    {

        private static string Conn;//ADD CONN
        public virtual string Connection { get; set; }//ADD CONN
        private static string AccCode;
        public virtual string AccountCode { get; set; }
        public virtual string Description { get; set; }
        public virtual string GroupCode { get; set; }
        public virtual string ExternalCode { get; set; }
        public virtual bool Confidential { get; set; }
        public virtual decimal GLBalanceAmount { get; set; }
        public virtual bool ControlAccount { get; set; }
        public virtual bool IsBudget { get; set; }
        public virtual string BudgetApproach { get; set; }
        public virtual string BudgetCoverage { get; set; }
        public virtual string BudgetLevel { get; set; }
        public virtual bool AmortizationAccount { get; set; }
        public virtual bool CashAccount { get; set; }
        public virtual bool IsDebit { get; set; }
        public virtual bool AllowJV { get; set; }
        public virtual bool IsInactive { get; set; }
        public virtual string TypeOpex { get; set; }
        public virtual string FixedCostCenter { get; set; }
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
        public virtual string DeactivatedBy { get; set; }
        public virtual string DeactivatedDate { get; set; }

        public virtual IList<GLSubsiCode> Detail { get; set; }


        public class GLSubsiCode
        {
            public virtual GLSubsiCode Parent { get; set; }
            public virtual string AccountCode { get; set; }
            public virtual string SubsiCode { get; set; }
            public virtual string Description { get; set; }
            public virtual bool IsInactive { get; set; }
            public virtual bool IsVariable { get; set; }

             public virtual string Field1 { get; set; }
            public virtual string Field2 { get; set; }
            public virtual string Field3 { get; set; }
            public virtual string Field4 { get; set; }
            public virtual string Field5 { get; set; }
            public virtual string Field6 { get; set; }
            public virtual string Field7 { get; set; }
            public virtual string Field8 { get; set; }
            public virtual string Field9 { get; set; }

            public DataTable getdetail(string AccountCode, string Conn)
            {

                DataTable a;
                try
                {
                    a = Gears.RetriveData2("select * from Accounting.GLSubsiCode where AccountCode='" + AccountCode + "' order by RecordId", Conn);
                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }

            public void AddGLSubsiCode(GLSubsiCode GLSubsiCode)
            {
                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();

                DT1.Rows.Add("Accounting.GLSubsiCode", "0", "AccountCode", AccCode);
                DT1.Rows.Add("Accounting.GLSubsiCode", "0", "SubsiCode", GLSubsiCode.SubsiCode);
                DT1.Rows.Add("Accounting.GLSubsiCode", "0", "Description", GLSubsiCode.Description);
                DT1.Rows.Add("Accounting.GLSubsiCode", "0", "IsInactive", "0");
                DT1.Rows.Add("Accounting.GLSubsiCode", "0", "IsVariable", GLSubsiCode.IsVariable);
                
                DT1.Rows.Add("Accounting.GLSubsiCode", "0", "Field1", GLSubsiCode.Field1);
                DT1.Rows.Add("Accounting.GLSubsiCode", "0", "Field2", GLSubsiCode.Field2);
                DT1.Rows.Add("Accounting.GLSubsiCode", "0", "Field3", GLSubsiCode.Field3);
                DT1.Rows.Add("Accounting.GLSubsiCode", "0", "Field4", GLSubsiCode.Field4);
                DT1.Rows.Add("Accounting.GLSubsiCode", "0", "Field5", GLSubsiCode.Field5);
                DT1.Rows.Add("Accounting.GLSubsiCode", "0", "Field6", GLSubsiCode.Field6);
                DT1.Rows.Add("Accounting.GLSubsiCode", "0", "Field7", GLSubsiCode.Field7);
                DT1.Rows.Add("Accounting.GLSubsiCode", "0", "Field8", GLSubsiCode.Field8);
                DT1.Rows.Add("Accounting.GLSubsiCode", "0", "Field9", GLSubsiCode.Field9);

                DT2.Rows.Add("Accounting.ChartOfAccount", "cond", "AccountCode", AccCode);
                DT2.Rows.Add("Accounting.ChartOfAccount", "set", "IsWithDetail", "True");

                Gears.CreateData(DT1, Conn);
                Gears.UpdateData(DT2, Conn);



            }
            public void UpdateGLSubsiCode(GLSubsiCode GLSubsiCode)
            {
                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                DT1.Rows.Add("Accounting.GLSubsiCode", "cond", "AccountCode", AccCode);
                DT1.Rows.Add("Accounting.GLSubsiCode", "cond", "SubsiCode", GLSubsiCode.SubsiCode);
                DT1.Rows.Add("Accounting.GLSubsiCode", "set", "Description", GLSubsiCode.Description);
                DT1.Rows.Add("Accounting.GLSubsiCode", "set", "IsInactive", GLSubsiCode.IsInactive);
                DT1.Rows.Add("Accounting.GLSubsiCode", "set", "IsVariable", GLSubsiCode.IsVariable);
                DT1.Rows.Add("Accounting.GLSubsiCode", "set", "Field1", GLSubsiCode.Field1);
                DT1.Rows.Add("Accounting.GLSubsiCode", "set", "Field2", GLSubsiCode.Field2);
                DT1.Rows.Add("Accounting.GLSubsiCode", "set", "Field3", GLSubsiCode.Field3);
                DT1.Rows.Add("Accounting.GLSubsiCode", "set", "Field4", GLSubsiCode.Field4);
                DT1.Rows.Add("Accounting.GLSubsiCode", "set", "Field5", GLSubsiCode.Field5);
                DT1.Rows.Add("Accounting.GLSubsiCode", "set", "Field6", GLSubsiCode.Field6);
                DT1.Rows.Add("Accounting.GLSubsiCode", "set", "Field7", GLSubsiCode.Field7);
                DT1.Rows.Add("Accounting.GLSubsiCode", "set", "Field8", GLSubsiCode.Field8);
                DT1.Rows.Add("Accounting.GLSubsiCode", "set", "Field9", GLSubsiCode.Field9);

                Gears.UpdateData(DT1, Conn);
            }
            public void DeleteGLSubsiCode(GLSubsiCode GLSubsiCode)
            {
                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();

                DT1.Rows.Add("Accounting.GLSubsiCode", "cond", "AccountCode", AccCode);
                DT1.Rows.Add("Accounting.GLSubsiCode", "cond", "SubsiCode", GLSubsiCode.SubsiCode);

                Gears.DeleteData(DT1, Conn);

                DataTable count = Gears.RetriveData2("SELECT * FROM Accounting.GLSubsiCode WHERE AccountCode = '" + AccCode + "'", Conn);

                if (count.Rows.Count < 1)
                {
                    DT2.Rows.Add("Accounting.ChartOfAccount", "cond", "AccountCode", AccCode);
                    DT2.Rows.Add("Accounting.ChartOfAccount", "set", "IsWithDetail", "False");
                    Gears.UpdateData(DT2, Conn);
                }

            }
        }

        public DataTable getdata(string Accountcode, string Conn)
        {
            DataTable a;

            if (Accountcode != null)
            {
                a = Gears.RetriveData2("select * from Accounting.ChartOfAccount where AccountCode = '" + Accountcode + "'", Conn);
                foreach (DataRow dtRow in a.Rows)
                {
                    AccountCode = dtRow["AccountCode"].ToString();
                    Description = dtRow["Description"].ToString();
                    GroupCode = dtRow["GroupCode"].ToString();
                    ExternalCode = dtRow["ExternalCode"].ToString();
                    Confidential = Convert.ToBoolean(Convert.IsDBNull(dtRow["Confidential"]) ? false : dtRow["Confidential"]);
                    GLBalanceAmount = Convert.ToDecimal(Convert.IsDBNull(dtRow["GLBalanceAmount"]) ? 0 : dtRow["GLBalanceAmount"]);
                    ControlAccount = Convert.ToBoolean(Convert.IsDBNull(dtRow["ControlAccount"]) ? false : dtRow["ControlAccount"]);
                    IsBudget = Convert.ToBoolean(Convert.IsDBNull(dtRow["IsBudget"]) ? false : dtRow["IsBudget"]);
                    BudgetApproach = dtRow["BudgetApproach"].ToString();
                    BudgetCoverage = dtRow["BudgetCoverage"].ToString();
                    BudgetLevel = dtRow["BudgetLevel"].ToString();
                    AmortizationAccount = Convert.ToBoolean(Convert.IsDBNull(dtRow["AmortizationAccount"]) ? false : dtRow["AmortizationAccount"]);
                    CashAccount = Convert.ToBoolean(Convert.IsDBNull(dtRow["CashAccount"]) ? false : dtRow["CashAccount"]);
                    IsDebit = Convert.ToBoolean(Convert.IsDBNull(dtRow["IsDebit"]) ? false : dtRow["IsDebit"]);
                    AllowJV = Convert.ToBoolean(Convert.IsDBNull(dtRow["AllowJV"]) ? false : dtRow["AllowJV"]);
                    IsInactive = Convert.ToBoolean(Convert.IsDBNull(dtRow["IsInactive"]) ? false : dtRow["IsInactive"]);
                    TypeOpex = dtRow["TypeOpex"].ToString();
                    FixedCostCenter = dtRow["FixedCostCenter"].ToString();

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
                    DeactivatedBy = dtRow["DeactivatedBy"].ToString();
                    DeactivatedDate = dtRow["DeactivatedDate"].ToString();
                }
            }
            else
            {
                a = Gears.RetriveData2("select '' as FunctionalGroupID,'' as Description,'' as AssignHead,'' as DateClosed,'' as Days", Conn);
            }
            return a;
        }
        public void InsertData(ChartOfAccounts _ent)
        {
            Conn = _ent.Connection;     //ADD CONN
            AccCode = _ent.AccountCode;
            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add("Accounting.ChartOfAccount", "0", "AccountCode", _ent.AccountCode);
            DT1.Rows.Add("Accounting.ChartOfAccount", "0", "Description", _ent.Description);
            DT1.Rows.Add("Accounting.ChartOfAccount", "0", "GroupCode", _ent.GroupCode);
            DT1.Rows.Add("Accounting.ChartOfAccount", "0", "ExternalCode", _ent.ExternalCode);
            DT1.Rows.Add("Accounting.ChartOfAccount", "0", "Confidential", _ent.Confidential);
            DT1.Rows.Add("Accounting.ChartOfAccount", "0", "GLBalanceAmount", _ent.GLBalanceAmount);
            DT1.Rows.Add("Accounting.ChartOfAccount", "0", "ControlAccount", _ent.ControlAccount);
            DT1.Rows.Add("Accounting.ChartOfAccount", "0", "IsBudget", _ent.IsBudget);
            DT1.Rows.Add("Accounting.ChartOfAccount", "0", "BudgetApproach", _ent.BudgetApproach);
            DT1.Rows.Add("Accounting.ChartOfAccount", "0", "BudgetCoverage", _ent.BudgetCoverage);
            DT1.Rows.Add("Accounting.ChartOfAccount", "0", "BudgetLevel", _ent.BudgetLevel);
            DT1.Rows.Add("Accounting.ChartOfAccount", "0", "AmortizationAccount", _ent.AmortizationAccount);
            DT1.Rows.Add("Accounting.ChartOfAccount", "0", "CashAccount", _ent.CashAccount);
            DT1.Rows.Add("Accounting.ChartOfAccount", "0", "IsDebit", _ent.IsDebit);
            DT1.Rows.Add("Accounting.ChartOfAccount", "0", "AllowJV", _ent.AllowJV);
            DT1.Rows.Add("Accounting.ChartOfAccount", "0", "IsInactive", _ent.IsInactive);
            DT1.Rows.Add("Accounting.ChartOfAccount", "0", "TypeOpex", _ent.TypeOpex);
            DT1.Rows.Add("Accounting.ChartOfAccount", "0", "FixedCostCenter", _ent.FixedCostCenter);
           
            DT1.Rows.Add("Accounting.ChartOfAccount", "0", "AddedBy", _ent.AddedBy);
            DT1.Rows.Add("Accounting.ChartOfAccount", "0", "AddedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            DT1.Rows.Add("Accounting.ChartOfAccount", "0", "Field1", _ent.Field1);
            DT1.Rows.Add("Accounting.ChartOfAccount", "0", "Field2", _ent.Field2);
            DT1.Rows.Add("Accounting.ChartOfAccount", "0", "Field3", _ent.Field3);
            DT1.Rows.Add("Accounting.ChartOfAccount", "0", "Field4", _ent.Field4);
            DT1.Rows.Add("Accounting.ChartOfAccount", "0", "Field5", _ent.Field5);
            DT1.Rows.Add("Accounting.ChartOfAccount", "0", "Field6", _ent.Field6);
            DT1.Rows.Add("Accounting.ChartOfAccount", "0", "Field7", _ent.Field7);
            DT1.Rows.Add("Accounting.ChartOfAccount", "0", "Field8", _ent.Field8);
            DT1.Rows.Add("Accounting.ChartOfAccount", "0", "Field9", _ent.Field9);


            Gears.CreateData(DT1, _ent.Connection);
            Functions.AuditTrail("ACTCOA", AccountCode, _ent.AddedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "INSERT", _ent.Connection);
        }

        public void UpdateData(ChartOfAccounts _ent)
        {
            Conn = _ent.Connection;     //ADD CONN
            AccCode = _ent.AccountCode;
            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add("Accounting.ChartOfAccount", "cond", "AccountCode", _ent.AccountCode);
            DT1.Rows.Add("Accounting.ChartOfAccount", "set", "Description", _ent.Description);
            DT1.Rows.Add("Accounting.ChartOfAccount", "set", "GroupCode", _ent.GroupCode);
            DT1.Rows.Add("Accounting.ChartOfAccount", "set", "ExternalCode", _ent.ExternalCode);
            DT1.Rows.Add("Accounting.ChartOfAccount", "set", "Confidential", _ent.Confidential);
            DT1.Rows.Add("Accounting.ChartOfAccount", "set", "GLBalanceAmount", _ent.GLBalanceAmount);
            DT1.Rows.Add("Accounting.ChartOfAccount", "set", "ControlAccount", _ent.ControlAccount);
            DT1.Rows.Add("Accounting.ChartOfAccount", "set", "IsBudget", _ent.IsBudget);
            DT1.Rows.Add("Accounting.ChartOfAccount", "set", "BudgetApproach", _ent.BudgetApproach);
            DT1.Rows.Add("Accounting.ChartOfAccount", "set", "BudgetCoverage", _ent.BudgetCoverage);
            DT1.Rows.Add("Accounting.ChartOfAccount", "set", "BudgetLevel", _ent.BudgetLevel);
            DT1.Rows.Add("Accounting.ChartOfAccount", "set", "AmortizationAccount", _ent.AmortizationAccount);
            DT1.Rows.Add("Accounting.ChartOfAccount", "set", "CashAccount", _ent.CashAccount);
            DT1.Rows.Add("Accounting.ChartOfAccount", "set", "IsDebit", _ent.IsDebit);
            DT1.Rows.Add("Accounting.ChartOfAccount", "set", "AllowJV", _ent.AllowJV);
            DT1.Rows.Add("Accounting.ChartOfAccount", "set", "IsInactive", _ent.IsInactive);
            DT1.Rows.Add("Accounting.ChartOfAccount", "set", "TypeOpex", _ent.TypeOpex);
            DT1.Rows.Add("Accounting.ChartOfAccount", "set", "FixedCostCenter", _ent.FixedCostCenter);

            DT1.Rows.Add("Accounting.ChartOfAccount", "set", "Field1", _ent.Field1);
            DT1.Rows.Add("Accounting.ChartOfAccount", "set", "Field2", _ent.Field2);
            DT1.Rows.Add("Accounting.ChartOfAccount", "set", "Field3", _ent.Field3);
            DT1.Rows.Add("Accounting.ChartOfAccount", "set", "Field4", _ent.Field4);
            DT1.Rows.Add("Accounting.ChartOfAccount", "set", "Field5", _ent.Field5);
            DT1.Rows.Add("Accounting.ChartOfAccount", "set", "Field6", _ent.Field6);
            DT1.Rows.Add("Accounting.ChartOfAccount", "set", "Field7", _ent.Field7);
            DT1.Rows.Add("Accounting.ChartOfAccount", "set", "Field8", _ent.Field8);
            DT1.Rows.Add("Accounting.ChartOfAccount", "set", "Field9", _ent.Field9);

            DT1.Rows.Add("Accounting.ChartOfAccount", "set", "LastEditedBy", _ent.LastEditedBy);
            DT1.Rows.Add("Accounting.ChartOfAccount", "set", "LastEditedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            string strErr = Gears.UpdateData(DT1, _ent.Connection);
            Functions.AuditTrail("ACTCOA", AccountCode, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "UPDATE", _ent.Connection);
        }
        public void DeleteData(ChartOfAccounts _ent)
        {
            AccountCode = _ent.AccountCode;
            Conn = _ent.Connection;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Accounting.ChartOfAccount", "cond", "AccountCode", AccountCode);
            Gears.DeleteData(DT1, _ent.Connection);
            Functions.AuditTrail("ACTCOA", AccountCode, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "DELETE", _ent.Connection);
        }
    }
}
