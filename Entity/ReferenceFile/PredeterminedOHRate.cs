using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GearsLibrary;
using System.Data;

namespace Entity
{
    public class PredeterminedOHRate
    {
        private static string Conn; //Ter
        public virtual string Connection { get; set; } //ter

        public virtual string RateCode { get; set; }
        public virtual string Description { get; set; }
        public virtual decimal OHRate { get; set; }
        public virtual string Type { get; set; }
        public virtual decimal BudgetOverhead { get; set; }
        public virtual string BudgetOHDesc { get; set; }
        public virtual decimal BudgetQtyAlloc { get; set; }
        public virtual string BudgetQtyAllocDesc { get; set; }
        public virtual string EffectivityDate { get; set; }
        public virtual string OHAppliedGLCode { get; set; }
        public virtual string OHAppliedSubsiCode { get; set; }
        public virtual string OHAppliedCostCenter { get; set; }
        public virtual string OHActualGLCode { get; set; }
        public virtual string OHActualSubsiCode { get; set; }
        public virtual string OHActualCostCenter { get; set; }
        public virtual bool IsInactive { get; set; }
        public virtual string AddedBy { get; set; }
        public virtual string AddedDate { get; set; }
        public virtual string LastEditedBy { get; set; }
        public virtual string LastEditedDate { get; set; }
        public virtual string ActivatedBy { get; set; }
        public virtual string ActivatedDate { get; set; }
        public virtual string DeActivatedBy { get; set; }
        public virtual string DeActivatedDate { get; set; }
        public virtual string ApprovedBy { get; set; }
        public virtual string ApprovedDate { get; set; }
        public virtual string Field1 { get; set; }
        public virtual string Field2 { get; set; }
        public virtual string Field3 { get; set; }
        public virtual string Field4 { get; set; }
        public virtual string Field5 { get; set; }
        public virtual string Field6 { get; set; }
        public virtual string Field7 { get; set; }
        public virtual string Field8 { get; set; }
        public virtual string Field9 { get; set; }
        public class RateHistory
        {
            public virtual PredeterminedOHRate Parent { get; set; }
            public virtual string RateCode { get; set; }
            public virtual decimal Rate { get; set; }
            public virtual decimal BudgetOverhead { get; set; }
            public virtual decimal BudgetQtyAlloc { get; set; }

            public DataTable getreftransaction(string RateCode, string Conn)
            {

                DataTable a;
                try
                {
                    a = Gears.RetriveData2("select RecordID,RateCode,Rate,BudgetOverhead,BudgetQtyAlloc,convert(varchar(15),EffectivityDate) as EffectivityDate  from Masterfile.RateHistory where RateCode ='" + RateCode + "' order by RecordId asc", Conn);//KMM add Conn
                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }
        }
        public DataTable getdata(string PredeterminedOHRate, string Conn) //Ter
        {
            DataTable a;

            if (PredeterminedOHRate != null)
            {
                a = Gears.RetriveData2("select * from Masterfile.PredeterminedOHRate where RateCode = '" + PredeterminedOHRate + "'", Conn); //Ter
                foreach (DataRow dtRow in a.Rows)
                {
                    RateCode = dtRow["RateCode"].ToString();
                    Description = dtRow["Description"].ToString();
                    Type = dtRow["Type"].ToString();
                    OHRate = Convert.ToDecimal(Convert.IsDBNull(dtRow["OHRate"]) ? false : dtRow["OHRate"]);
                    BudgetOverhead = Convert.ToDecimal(Convert.IsDBNull(dtRow["BudgetOverhead"]) ? false : dtRow["BudgetOverhead"]);
                    BudgetOHDesc = dtRow["BudgetOHDesc"].ToString();
                    BudgetQtyAlloc = Convert.ToDecimal(Convert.IsDBNull(dtRow["BudgetQtyAlloc"]) ? false : dtRow["BudgetQtyAlloc"]);
                    BudgetQtyAllocDesc = dtRow["BudgetQtyAllocDesc"].ToString();
                    EffectivityDate = dtRow["EffectivityDate"].ToString();
                    OHAppliedGLCode = dtRow["OHAppliedGLCode"].ToString();
                    OHAppliedSubsiCode = dtRow["OHAppliedSubsiCode"].ToString();
                    OHAppliedCostCenter = dtRow["OHAppliedCostCenter"].ToString();
                    OHActualGLCode = dtRow["OHActualGLCode"].ToString();
                    OHActualSubsiCode = dtRow["OHActualSubsiCode"].ToString();
                    OHActualCostCenter = dtRow["OHActualCostCenter"].ToString();
                    IsInactive = Convert.ToBoolean(Convert.IsDBNull(dtRow["IsInactive"]) ? false : dtRow["IsInactive"]);
                    AddedBy = dtRow["AddedBy"].ToString();
                    AddedDate = dtRow["AddedDate"].ToString();
                    LastEditedBy = dtRow["LastEditedBy"].ToString();
                    LastEditedDate = dtRow["LastEditedDate"].ToString();
                    ActivatedBy = dtRow["ActivatedBy"].ToString();
                    ActivatedDate = dtRow["ActivatedDate"].ToString();
                    DeActivatedBy = dtRow["DeActivatedBy"].ToString();
                    DeActivatedDate = dtRow["DeActivatedDate"].ToString();
                    ApprovedBy = dtRow["ApprovedBy"].ToString();
                    ApprovedDate = dtRow["ApprovedDate"].ToString();
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
            }
            else
            {
                a = Gears.RetriveData2("select '' as FunctionalGroupID,'' as Description,'' as AssignHead,'' as DateClosed,'' as Days", Conn); //Ter
            }
            return a;
        }
        public void InsertData(PredeterminedOHRate _ent)
        {
            Conn = _ent.Connection; //Ter

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Masterfile.PredeterminedOHRate", "0", "RateCode", _ent.RateCode);
            DT1.Rows.Add("Masterfile.PredeterminedOHRate", "0", "Description", _ent.Description);
            DT1.Rows.Add("Masterfile.PredeterminedOHRate", "0", "Type", _ent.Type);
            DT1.Rows.Add("Masterfile.PredeterminedOHRate", "0", "OHRate", _ent.OHRate);
            DT1.Rows.Add("Masterfile.PredeterminedOHRate", "0", "BudgetOverhead", _ent.BudgetOverhead);
            DT1.Rows.Add("Masterfile.PredeterminedOHRate", "0", "BudgetOHDesc", _ent.BudgetOHDesc);
            DT1.Rows.Add("Masterfile.PredeterminedOHRate", "0", "BudgetQtyAlloc", _ent.BudgetQtyAlloc);
            DT1.Rows.Add("Masterfile.PredeterminedOHRate", "0", "BudgetQtyAllocDesc", _ent.BudgetQtyAllocDesc);
            DT1.Rows.Add("Masterfile.PredeterminedOHRate", "0", "EffectivityDate", _ent.EffectivityDate);
            DT1.Rows.Add("Masterfile.PredeterminedOHRate", "0", "OHAppliedGLCode", _ent.OHAppliedGLCode);
            DT1.Rows.Add("Masterfile.PredeterminedOHRate", "0", "OHAppliedSubsiCode", _ent.OHAppliedSubsiCode);
            DT1.Rows.Add("Masterfile.PredeterminedOHRate", "0", "OHAppliedCostCenter", _ent.OHAppliedCostCenter);
            DT1.Rows.Add("Masterfile.PredeterminedOHRate", "0", "OHActualGLCode", _ent.OHActualGLCode);
            DT1.Rows.Add("Masterfile.PredeterminedOHRate", "0", "OHActualSubsiCode", _ent.OHActualSubsiCode);
            DT1.Rows.Add("Masterfile.PredeterminedOHRate", "0", "OHActualCostCenter", _ent.OHActualCostCenter);
            DT1.Rows.Add("Masterfile.PredeterminedOHRate", "0", "IsInactive", _ent.IsInactive);
            DT1.Rows.Add("Masterfile.PredeterminedOHRate", "0", "AddedBy", _ent.AddedBy);
            DT1.Rows.Add("Masterfile.PredeterminedOHRate", "0", "AddedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            DT1.Rows.Add("Masterfile.PredeterminedOHRate", "0", "Field1", _ent.Field1);
            DT1.Rows.Add("Masterfile.PredeterminedOHRate", "0", "Field2", _ent.Field2);
            DT1.Rows.Add("Masterfile.PredeterminedOHRate", "0", "Field3", _ent.Field3);
            DT1.Rows.Add("Masterfile.PredeterminedOHRate", "0", "Field4", _ent.Field4);
            DT1.Rows.Add("Masterfile.PredeterminedOHRate", "0", "Field5", _ent.Field5);
            DT1.Rows.Add("Masterfile.PredeterminedOHRate", "0", "Field6", _ent.Field6);
            DT1.Rows.Add("Masterfile.PredeterminedOHRate", "0", "Field7", _ent.Field7);
            DT1.Rows.Add("Masterfile.PredeterminedOHRate", "0", "Field8", _ent.Field8);
            DT1.Rows.Add("Masterfile.PredeterminedOHRate", "0", "Field9", _ent.Field9);

            Gears.CreateData(DT1, _ent.Connection); // TER
        }

        public void UpdateData(PredeterminedOHRate _ent)
        {
            Conn = _ent.Connection; //Ter
            if (ApprovedBy != null)
            {
                DataTable dtbl = Gears.RetriveData2(
                    " Insert into Masterfile.RateHistory (RateCode,EffectivityDate, Rate, BudgetOverhead, BudgetQtyAlloc) " +
                    " select RateCode,EffectivityDate, OHRate, BudgetOverhead, BudgetQtyAlloc " +
                    " from Masterfile.PredeterminedOHRate where RateCode = '" + _ent.RateCode + "'", Conn); //Ter

            }
            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Masterfile.PredeterminedOHRate", "cond", "RateCode", _ent.RateCode);
            DT1.Rows.Add("Masterfile.PredeterminedOHRate", "set", "Description", _ent.Description);
            DT1.Rows.Add("Masterfile.PredeterminedOHRate", "set", "Type", _ent.Type);
            DT1.Rows.Add("Masterfile.PredeterminedOHRate", "set", "OHRate", _ent.OHRate);
            DT1.Rows.Add("Masterfile.PredeterminedOHRate", "set", "BudgetOverhead", _ent.BudgetOverhead);
            DT1.Rows.Add("Masterfile.PredeterminedOHRate", "set", "BudgetOHDesc", _ent.BudgetOHDesc);
            DT1.Rows.Add("Masterfile.PredeterminedOHRate", "set", "BudgetQtyAlloc", _ent.BudgetQtyAlloc);
            DT1.Rows.Add("Masterfile.PredeterminedOHRate", "set", "BudgetQtyAllocDesc", _ent.BudgetQtyAllocDesc);
            DT1.Rows.Add("Masterfile.PredeterminedOHRate", "set", "EffectivityDate", _ent.EffectivityDate);
            DT1.Rows.Add("Masterfile.PredeterminedOHRate", "set", "OHAppliedGLCode", _ent.OHAppliedGLCode);
            DT1.Rows.Add("Masterfile.PredeterminedOHRate", "set", "OHAppliedSubsiCode", _ent.OHAppliedSubsiCode);
            DT1.Rows.Add("Masterfile.PredeterminedOHRate", "set", "OHAppliedCostCenter", _ent.OHAppliedCostCenter);
            DT1.Rows.Add("Masterfile.PredeterminedOHRate", "set", "OHActualGLCode", _ent.OHActualGLCode);
            DT1.Rows.Add("Masterfile.PredeterminedOHRate", "set", "OHActualSubsiCode", _ent.OHActualSubsiCode);
            DT1.Rows.Add("Masterfile.PredeterminedOHRate", "set", "OHActualCostCenter", _ent.OHActualCostCenter);
            DT1.Rows.Add("Masterfile.PredeterminedOHRate", "set", "IsInactive", _ent.IsInactive);
            DT1.Rows.Add("Masterfile.PredeterminedOHRate", "set", "LastEditedBy", _ent.AddedBy);
            DT1.Rows.Add("Masterfile.PredeterminedOHRate", "set", "LastEditedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            DT1.Rows.Add("Masterfile.PredeterminedOHRate", "set", "Field1", _ent.Field1);
            DT1.Rows.Add("Masterfile.PredeterminedOHRate", "set", "Field2", _ent.Field2);
            DT1.Rows.Add("Masterfile.PredeterminedOHRate", "set", "Field3", _ent.Field3);
            DT1.Rows.Add("Masterfile.PredeterminedOHRate", "set", "Field4", _ent.Field4);
            DT1.Rows.Add("Masterfile.PredeterminedOHRate", "set", "Field5", _ent.Field5);
            DT1.Rows.Add("Masterfile.PredeterminedOHRate", "set", "Field6", _ent.Field6);
            DT1.Rows.Add("Masterfile.PredeterminedOHRate", "set", "Field7", _ent.Field7);
            DT1.Rows.Add("Masterfile.PredeterminedOHRate", "set", "Field8", _ent.Field8);
            DT1.Rows.Add("Masterfile.PredeterminedOHRate", "set", "Field9", _ent.Field9);

            string strErr = Gears.UpdateData(DT1, _ent.Connection); // Ter
            Functions.AuditTrail("REFPDOH", _ent.RateCode, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "UPDATE", _ent.Connection); // Ter

            strErr = Gears.UpdateData(DT1, _ent.Connection); // Ter
        }

        public void DeleteData(PredeterminedOHRate _ent)
        {
            Conn = _ent.Connection; //Ter
            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Masterfile.PredeterminedOHRate", "cond", "RateCode", _ent.RateCode);
            Gears.DeleteData(DT1, _ent.Connection); // Ter
            Functions.AuditTrail("REFPDOH", _ent.RateCode, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "DELETE", _ent.Connection); // Ter
        }
    }
}
