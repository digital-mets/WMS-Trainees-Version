using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GearsLibrary;
using System.Data;

namespace Entity
{
    public class PettyCashFundSetUp
    {
        public virtual string FundCode { get; set; }
        public virtual string FundDescription { get; set; }
        public virtual string Custodian { get; set; }
        public virtual decimal CashFundAmount { get; set; }
        public virtual string GLAccountCode { get; set; }
        public virtual string GLSubsiCode { get; set; }
        public virtual string RecordingMethod { get; set; }
        public virtual Boolean IsInactive { get; set; }
        public virtual string AddedBy { get; set; }
        public virtual string AddedDate { get; set; }
        public virtual string LastEditedBy { get; set; }
        public virtual string LastEditedDate { get; set; }
        public virtual string ActivatedBy { get; set; }
        public virtual string ActivatedDate { get; set; }
        public virtual string DeActivatedBy { get; set; }
        public virtual string DeActivatedDate { get; set; }

        public virtual string Field1 { get; set; }
        public virtual string Field2 { get; set; }
        public virtual string Field3 { get; set; }
        public virtual string Field4 { get; set; }
        public virtual string Field5 { get; set; }
        public virtual string Field6 { get; set; }
        public virtual string Field7 { get; set; }
        public virtual string Field8 { get; set; }
        public virtual string Field9 { get; set; }
        public DataTable getdata(string DocNumber)
        {
            DataTable a;

            if (DocNumber != null)
            {
                a = Gears.RetriveData2("select * from Masterfile.PettyCashFundSetUp where FundCode = '" + DocNumber + "'");
                foreach (DataRow dtRow in a.Rows)
                {
                    FundCode = dtRow["FundCode"].ToString();
                    FundDescription = dtRow["FundDescription"].ToString();
                    Custodian = dtRow["Custodian"].ToString();
                    CashFundAmount = Convert.ToDecimal(Convert.IsDBNull(dtRow["CashFundAmount"]) ? 0 : dtRow["CashFundAmount"]);
                    GLAccountCode = dtRow["GLAccountCode"].ToString();
                    GLSubsiCode = dtRow["GLSubsiCode"].ToString();
                    RecordingMethod = dtRow["RecordingMethod"].ToString();
                    AddedBy= dtRow["AddedBy"].ToString();
                    AddedDate= dtRow["AddedDate"].ToString();
                    LastEditedBy= dtRow["LastEditedBy"].ToString();
                    LastEditedDate = dtRow["LastEditedDate"].ToString();
                    ActivatedBy = dtRow["ActivatedBy"].ToString();
                    ActivatedDate = dtRow["ActivatedDate"].ToString();
                    DeActivatedBy = dtRow["DeActivatedBy"].ToString();
                    DeActivatedDate = dtRow["DeActivatedDate"].ToString();
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
                }
            }
            else
            {
                a = Gears.RetriveData2("select '' as FunctionalGroupID,'' as Description,'' as AssignHead,'' as DateClosed,'' as Days");
            }
            return a;
        }
        public void InsertData(PettyCashFundSetUp T)
        {
            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add("Masterfile.PettyCashFundSetUp", "0", "FundCode", T.FundCode);
            DT1.Rows.Add("Masterfile.PettyCashFundSetUp", "0", "FundDescription", T.FundDescription);
            DT1.Rows.Add("Masterfile.PettyCashFundSetUp", "0", "Custodian", T.Custodian);
            DT1.Rows.Add("Masterfile.PettyCashFundSetUp", "0", "CashFundAmount", T.CashFundAmount);
            DT1.Rows.Add("Masterfile.PettyCashFundSetUp", "0", "GLAccountCode", T.GLAccountCode);
            DT1.Rows.Add("Masterfile.PettyCashFundSetUp", "0", "GLSubsiCode", T.GLSubsiCode);
            DT1.Rows.Add("Masterfile.PettyCashFundSetUp", "0", "RecordingMethod", T.RecordingMethod);
            DT1.Rows.Add("Masterfile.PettyCashFundSetUp", "0", "IsInactive", T.IsInactive);
            DT1.Rows.Add("Masterfile.PettyCashFundSetUp", "0", "AddedBy", T.AddedBy);
            DT1.Rows.Add("Masterfile.PettyCashFundSetUp", "0", "AddedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            DT1.Rows.Add("Masterfile.PettyCashFundSetUp", "0", "Field1", T.Field1);
            DT1.Rows.Add("Masterfile.PettyCashFundSetUp", "0", "Field2", T.Field2);
            DT1.Rows.Add("Masterfile.PettyCashFundSetUp", "0", "Field3", T.Field3);
            DT1.Rows.Add("Masterfile.PettyCashFundSetUp", "0", "Field4", T.Field4);
            DT1.Rows.Add("Masterfile.PettyCashFundSetUp", "0", "Field5", T.Field5);
            DT1.Rows.Add("Masterfile.PettyCashFundSetUp", "0", "Field6", T.Field6);
            DT1.Rows.Add("Masterfile.PettyCashFundSetUp", "0", "Field7", T.Field7);
            DT1.Rows.Add("Masterfile.PettyCashFundSetUp", "0", "Field8", T.Field8);
            DT1.Rows.Add("Masterfile.PettyCashFundSetUp", "0", "Field9", T.Field9);
                        
            Gears.CreateData(DT1);

            Functions.AuditTrail("PETTYCSHFND", T.FundCode, T.AddedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "INSERT");
        }

        public void UpdateData(PettyCashFundSetUp L)
        {
            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Masterfile.PettyCashFundSetUp", "cond", "FundCode", L.FundCode);
            DT1.Rows.Add("Masterfile.PettyCashFundSetUp", "set", "FundDescription", L.FundDescription);
            DT1.Rows.Add("Masterfile.PettyCashFundSetUp", "set", "Custodian", L.Custodian);
            DT1.Rows.Add("Masterfile.PettyCashFundSetUp", "set", "CashFundAmount", L.CashFundAmount);
            DT1.Rows.Add("Masterfile.PettyCashFundSetUp", "set", "GLAccountCode", L.GLAccountCode);
            DT1.Rows.Add("Masterfile.PettyCashFundSetUp", "set", "GLSubsiCode", L.GLSubsiCode);
            DT1.Rows.Add("Masterfile.PettyCashFundSetUp", "set", "RecordingMethod", L.RecordingMethod);
            DT1.Rows.Add("Masterfile.PettyCashFundSetUp", "set", "IsInactive", L.IsInactive);
            DT1.Rows.Add("Masterfile.PettyCashFundSetUp", "set", "LastEditedBy", L.LastEditedBy);
            DT1.Rows.Add("Masterfile.PettyCashFundSetUp", "set", "LastEditedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            DT1.Rows.Add("Masterfile.PettyCashFundSetUp", "set", "Field1", L.Field1);
            DT1.Rows.Add("Masterfile.PettyCashFundSetUp", "set", "Field2", L.Field2);
            DT1.Rows.Add("Masterfile.PettyCashFundSetUp", "set", "Field3", L.Field3);
            DT1.Rows.Add("Masterfile.PettyCashFundSetUp", "set", "Field4", L.Field4);
            DT1.Rows.Add("Masterfile.PettyCashFundSetUp", "set", "Field5", L.Field5);
            DT1.Rows.Add("Masterfile.PettyCashFundSetUp", "set", "Field6", L.Field6);
            DT1.Rows.Add("Masterfile.PettyCashFundSetUp", "set", "Field7", L.Field7);
            DT1.Rows.Add("Masterfile.PettyCashFundSetUp", "set", "Field8", L.Field8);
            DT1.Rows.Add("Masterfile.PettyCashFundSetUp", "set", "Field9", L.Field9);

            string strErr = Gears.UpdateData(DT1);

            Functions.AuditTrail("PETTYCSHFND", L.FundCode, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "UPDATE");
        }
        public void DeleteData(PettyCashFundSetUp V)
        {
            FundCode = V.FundCode;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Masterfile.PettyCashFundSetUp", "cond", "FundCode", V.FundCode);
            Gears.DeleteData(DT1);
            Functions.AuditTrail("PETTYCSHFND", V.FundCode, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "DELETE");
        }
    }
}
