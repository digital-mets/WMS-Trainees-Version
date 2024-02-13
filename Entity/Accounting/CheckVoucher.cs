using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GearsLibrary;
using System.Data;

namespace Entity
{
    public class CheckVoucher
    {
        private static string Conn;
        public virtual string Connection { get; set; }

        private static string Docnum;
        public virtual string DocNumber { get; set; }
        public virtual string DocDate { get; set; }
        public virtual string TransType { get; set; }
        public virtual string SupplierCode { get; set; }
        public virtual string SupplierName { get; set; }
        public virtual decimal TotalCheckAmount { get; set; }
        public virtual decimal TotalAppliedAmount { get; set; }
        public virtual string Remarks { get; set; }
        public virtual string RefTrans { get; set; }
        public virtual string AddedBy { get; set; }
        public virtual string AddedDate { get; set; }
        public virtual string LastEditedBy { get; set; }
        public virtual string LastEditedDate { get; set; }
        public virtual string SubmittedBy { get; set; }
        public virtual string SubmittedDate { get; set; }
        public virtual string CancelledBy { get; set; }
        public virtual string CancelledDate { get; set; }
        public virtual string PostedBy { get; set; }
        public virtual string PostedDate { get; set; }
        public virtual string IsWithDetail { get; set; }
        public virtual string IsValidated { get; set; }
        public virtual string Field1 { get; set; }
        public virtual string Field2 { get; set; }
        public virtual string Field3 { get; set; }
        public virtual string Field4 { get; set; }
        public virtual string Field5 { get; set; }
        public virtual string Field6 { get; set; }
        public virtual string Field7 { get; set; }
        public virtual string Field8 { get; set; }
        public virtual string Field9 { get; set; }
        public virtual string TaggedTransaction { get; set; }
        public virtual IList<CheckVoucherDetail> Detail1 { get; set; }
        public virtual IList<CheckVoucherTagging> Detail2 { get; set; }
        public virtual IList<CheckVoucherAdjEntry> Detail3 { get; set; }

        public class CheckVoucherDetail
        {
            public virtual CheckVoucher Parent { get; set; }
            public virtual string DocNumber { get; set; }
            public virtual string LineNumber { get; set; }
            public virtual string BankAccount { get; set; }
            public virtual string Bank { get; set; }
            public virtual string Branch { get; set; }
            public virtual string PayeeName { get; set; }
            public virtual DateTime CheckDate { get; set; }
            public virtual string CheckNumber { get; set; }
            public virtual decimal CheckAmount { get; set; }
            public virtual bool IsCross { get; set; }
            public virtual string Field1 { get; set; }
            public virtual string Field2 { get; set; }
            public virtual string Field3 { get; set; }
            public virtual string Field4 { get; set; }
            public virtual string Field5 { get; set; }
            public virtual string Field6 { get; set; }
            public virtual string Field7 { get; set; }
            public virtual string Field8 { get; set; }
            public virtual string Field9 { get; set; }
            public virtual DateTime ReleasedDate { get; set; }
            public virtual DateTime ClearedDate { get; set; }

            public DataTable getdetail(string DocNumber,string Conn)
            {

                DataTable a;
                try
                {
                    a = Gears.RetriveData2("SELECT * FROM Accounting.CheckVoucherDetail WHERE DocNumber = '" + DocNumber + "' ORDER BY LineNumber",Conn);
                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }

            public void AddCheckVoucherDetail(CheckVoucherDetail CheckVoucherDetail)
            {
                int linenum = 0;

                DataTable count = Gears.RetriveData2("SELECT MAX(CONVERT(int,LineNumber)) AS LineNumber FROM Accounting.CheckVoucherDetail WHERE DocNumber = '" + Docnum + "'",Conn);

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
                DT1.Rows.Add("Accounting.CheckVoucherDetail", "0", "DocNumber", Docnum);
                DT1.Rows.Add("Accounting.CheckVoucherDetail", "0", "LineNumber", strLine);
                DT1.Rows.Add("Accounting.CheckVoucherDetail", "0", "BankAccount", CheckVoucherDetail.BankAccount);
                DT1.Rows.Add("Accounting.CheckVoucherDetail", "0", "Bank", CheckVoucherDetail.Bank);
                DT1.Rows.Add("Accounting.CheckVoucherDetail", "0", "Branch", CheckVoucherDetail.Branch);
                DT1.Rows.Add("Accounting.CheckVoucherDetail", "0", "PayeeName", CheckVoucherDetail.PayeeName); 
                if (CheckVoucherDetail.CheckDate.ToString() != "1/1/0001 12:00:00 AM")
                {
                    DT1.Rows.Add("Accounting.CheckVoucherDetail", "0", "CheckDate", CheckVoucherDetail.CheckDate);
                }
                DT1.Rows.Add("Accounting.CheckVoucherDetail", "0", "CheckNumber", CheckVoucherDetail.CheckNumber);
                DT1.Rows.Add("Accounting.CheckVoucherDetail", "0", "CheckAmount", CheckVoucherDetail.CheckAmount);
                //if (CheckVoucherDetail.ReleasedDate.ToString() != "1/1/0001 12:00:00 AM")
                //{
                //    DT1.Rows.Add("Accounting.CheckVoucherDetail", "0", "ReleasedDate", CheckVoucherDetail.ReleasedDate);
                //}
                //if (CheckVoucherDetail.ClearedDate.ToString() != "1/1/0001 12:00:00 AM")
                //{
                //    DT1.Rows.Add("Accounting.CheckVoucherDetail", "0", "ClearedDate", CheckVoucherDetail.ClearedDate);
                //}
                DT1.Rows.Add("Accounting.CheckVoucherDetail", "0", "IsCross", CheckVoucherDetail.IsCross ? "1" : "0");
                DT1.Rows.Add("Accounting.CheckVoucherDetail", "0", "Field1", CheckVoucherDetail.Field1);
                DT1.Rows.Add("Accounting.CheckVoucherDetail", "0", "Field2", CheckVoucherDetail.Field2);
                DT1.Rows.Add("Accounting.CheckVoucherDetail", "0", "Field3", CheckVoucherDetail.Field3);
                DT1.Rows.Add("Accounting.CheckVoucherDetail", "0", "Field4", CheckVoucherDetail.Field4);
                DT1.Rows.Add("Accounting.CheckVoucherDetail", "0", "Field5", CheckVoucherDetail.Field5);
                DT1.Rows.Add("Accounting.CheckVoucherDetail", "0", "Field6", CheckVoucherDetail.Field6);
                DT1.Rows.Add("Accounting.CheckVoucherDetail", "0", "Field7", CheckVoucherDetail.Field7);
                DT1.Rows.Add("Accounting.CheckVoucherDetail", "0", "Field8", CheckVoucherDetail.Field8);
                DT1.Rows.Add("Accounting.CheckVoucherDetail", "0", "Field9", CheckVoucherDetail.Field9);

                DT2.Rows.Add("Accounting.CheckVoucher", "cond", "DocNumber", Docnum);
                DT2.Rows.Add("Accounting.CheckVoucher", "set", "IsWithDetail", "True");

                Gears.CreateData(DT1,Conn);
                Gears.UpdateData(DT2,Conn);
            }
            public void UpdateCheckVoucherDetail(CheckVoucherDetail CheckVoucherDetail)
            {

                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                DT1.Rows.Add("Accounting.CheckVoucherDetail", "cond", "DocNumber", Docnum);
                DT1.Rows.Add("Accounting.CheckVoucherDetail", "cond", "LineNumber", CheckVoucherDetail.LineNumber);
                DT1.Rows.Add("Accounting.CheckVoucherDetail", "set", "BankAccount", CheckVoucherDetail.BankAccount);
                DT1.Rows.Add("Accounting.CheckVoucherDetail", "set", "Bank", CheckVoucherDetail.Bank);
                DT1.Rows.Add("Accounting.CheckVoucherDetail", "set", "Branch", CheckVoucherDetail.Branch);
                DT1.Rows.Add("Accounting.CheckVoucherDetail", "set", "PayeeName", CheckVoucherDetail.PayeeName);
                DT1.Rows.Add("Accounting.CheckVoucherDetail", "set", "CheckDate", CheckVoucherDetail.CheckDate);
                DT1.Rows.Add("Accounting.CheckVoucherDetail", "set", "CheckNumber", CheckVoucherDetail.CheckNumber);
                DT1.Rows.Add("Accounting.CheckVoucherDetail", "set", "CheckAmount", CheckVoucherDetail.CheckAmount);
                //DT1.Rows.Add("Accounting.CheckVoucherDetail", "set", "ReleasedDate", CheckVoucherDetail.ReleasedDate);
                //DT1.Rows.Add("Accounting.CheckVoucherDetail", "set", "ClearedDate", CheckVoucherDetail.ClearedDate);
                DT1.Rows.Add("Accounting.CheckVoucherDetail", "set", "IsCross", CheckVoucherDetail.IsCross);
                DT1.Rows.Add("Accounting.CheckVoucherDetail", "set", "Field1", CheckVoucherDetail.Field1);
                DT1.Rows.Add("Accounting.CheckVoucherDetail", "set", "Field2", CheckVoucherDetail.Field2);
                DT1.Rows.Add("Accounting.CheckVoucherDetail", "set", "Field3", CheckVoucherDetail.Field3);
                DT1.Rows.Add("Accounting.CheckVoucherDetail", "set", "Field4", CheckVoucherDetail.Field4);
                DT1.Rows.Add("Accounting.CheckVoucherDetail", "set", "Field5", CheckVoucherDetail.Field5);
                DT1.Rows.Add("Accounting.CheckVoucherDetail", "set", "Field6", CheckVoucherDetail.Field6);
                DT1.Rows.Add("Accounting.CheckVoucherDetail", "set", "Field7", CheckVoucherDetail.Field7);
                DT1.Rows.Add("Accounting.CheckVoucherDetail", "set", "Field8", CheckVoucherDetail.Field8);
                DT1.Rows.Add("Accounting.CheckVoucherDetail", "set", "Field9", CheckVoucherDetail.Field9);

                Gears.UpdateData(DT1,Conn);
            }
            public void DeleteCheckVoucherDetail(CheckVoucherDetail CheckVoucherDetail)
            {
                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
                DT1.Rows.Add("Accounting.CheckVoucherDetail", "cond", "DocNumber", Docnum);
                // 2017-03-01   TL  Bug Fix
                //DT1.Rows.Add("Accounting.CheckVoucherDetail", "cond", "RecordID", CheckVoucherDetail.LineNumber);
                DT1.Rows.Add("Accounting.CheckVoucherDetail", "cond", "LineNumber", CheckVoucherDetail.LineNumber);
                // 2017-03-01   TL  (End)


                Gears.DeleteData(DT1,Conn);

                DataTable count = Gears.RetriveData2("SELECT * FROM Accounting.CheckVoucherDetail WHERE DocNumber = '" + Docnum + "'",Conn);

                if (count.Rows.Count < 1)
                {
                    DT2.Rows.Add("Accounting.CheckVoucherDetail", "cond", "DocNumber", Docnum);
                    DT2.Rows.Add("Accounting.CheckVoucherDetail", "set", "IsWithDetail", "False");
                    Gears.UpdateData(DT2,Conn);
                }
            }
        }
        public class CheckVoucherTagging
        {
            public virtual CheckVoucher Parent { get; set; }
            public virtual string DocNumber { get; set; }
            public virtual string LineNumber { get; set; }
            public virtual string RecordId { get; set; }
            public virtual string TransDocNumber { get; set; }
            public virtual string TransType { get; set; }
            public virtual DateTime TransDate { get; set; }
            public virtual string TransAccountCode { get; set; }
            public virtual string TransSubsiCode { get; set; }
            public virtual string TransBizPartnerCode { get; set; }
            public virtual string TransProfitCenter { get; set; }
            public virtual string TransCostCenter { get; set; }
            public virtual DateTime TransDueDate { get; set; }
            public virtual decimal TransAmount { get; set; }
            public virtual decimal TransAppliedAmount { get; set; }
            public virtual string Field1 { get; set; }
            public virtual string Field2 { get; set; }
            public virtual string Field3 { get; set; }
            public virtual string Field4 { get; set; }
            public virtual string Field5 { get; set; }
            public virtual string Field6 { get; set; }
            public virtual string Field7 { get; set; }
            public virtual string Field8 { get; set; }
            public virtual string Field9 { get; set; }
            public virtual string Version { get; set; }
            public DataTable getdetail(string DocNumber,string Conn)
            {

                DataTable a;
                try
                {
                    a = Gears.RetriveData2("SELECT * FROM Accounting.CheckVoucherTagging WHERE DocNumber='" + DocNumber + "' ORDER BY LineNumber",Conn);
                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }

            public void AddCheckVoucherTagging(CheckVoucherTagging CheckVoucherTagging)
            {
                int linenum = 0;

                DataTable count = Gears.RetriveData2("SELECT MAX(CONVERT(int,LineNumber)) AS LineNumber FROM Accounting.CheckVoucherTagging WHERE DocNumber = '" + Docnum + "'",Conn);

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
                DT1.Rows.Add("Accounting.CheckVoucherTagging", "0", "DocNumber", Docnum);
                DT1.Rows.Add("Accounting.CheckVoucherTagging", "0", "LineNumber", strLine);
                DT1.Rows.Add("Accounting.CheckVoucherTagging", "0", "RecordId", CheckVoucherTagging.RecordId);
                DT1.Rows.Add("Accounting.CheckVoucherTagging", "0", "TransDocNumber", CheckVoucherTagging.TransDocNumber);
                DT1.Rows.Add("Accounting.CheckVoucherTagging", "0", "TransType", CheckVoucherTagging.TransType);
                DT1.Rows.Add("Accounting.CheckVoucherTagging", "0", "TransDate", CheckVoucherTagging.TransDate);
                DT1.Rows.Add("Accounting.CheckVoucherTagging", "0", "TransDueDate", CheckVoucherTagging.TransDueDate);
                DT1.Rows.Add("Accounting.CheckVoucherTagging", "0", "TransAccountCode", CheckVoucherTagging.TransAccountCode);
                DT1.Rows.Add("Accounting.CheckVoucherTagging", "0", "TransSubsiCode", CheckVoucherTagging.TransSubsiCode);
                DT1.Rows.Add("Accounting.CheckVoucherTagging", "0", "TransProfitCenter", CheckVoucherTagging.TransProfitCenter);
                DT1.Rows.Add("Accounting.CheckVoucherTagging", "0", "TransCostCenter", CheckVoucherTagging.TransCostCenter);
                DT1.Rows.Add("Accounting.CheckVoucherTagging", "0", "TransBizPartnerCode", CheckVoucherTagging.TransBizPartnerCode);
                DT1.Rows.Add("Accounting.CheckVoucherTagging", "0", "TransAmount", CheckVoucherTagging.TransAmount);
                DT1.Rows.Add("Accounting.CheckVoucherTagging", "0", "TransAppliedAmount", CheckVoucherTagging.TransAppliedAmount);
                DT1.Rows.Add("Accounting.CheckVoucherTagging", "0", "Field1", CheckVoucherTagging.Field1);
                DT1.Rows.Add("Accounting.CheckVoucherTagging", "0", "Field2", CheckVoucherTagging.Field2);
                DT1.Rows.Add("Accounting.CheckVoucherTagging", "0", "Field3", CheckVoucherTagging.Field3);
                DT1.Rows.Add("Accounting.CheckVoucherTagging", "0", "Field4", CheckVoucherTagging.Field4);
                DT1.Rows.Add("Accounting.CheckVoucherTagging", "0", "Field5", CheckVoucherTagging.Field5);
                DT1.Rows.Add("Accounting.CheckVoucherTagging", "0", "Field6", CheckVoucherTagging.Field6);
                DT1.Rows.Add("Accounting.CheckVoucherTagging", "0", "Field7", CheckVoucherTagging.Field7);
                DT1.Rows.Add("Accounting.CheckVoucherTagging", "0", "Field8", CheckVoucherTagging.Field8);
                DT1.Rows.Add("Accounting.CheckVoucherTagging", "0", "Field9", CheckVoucherTagging.Field9);
                DT1.Rows.Add("Accounting.CheckVoucherTagging", "0", "Version", "1");

                Gears.CreateData(DT1,Conn);
            }
            public void UpdateCheckVoucherTagging(CheckVoucherTagging CheckVoucherTagging)
            {

                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                DT1.Rows.Add("Accounting.CheckVoucherTagging", "cond", "DocNumber", Docnum);
                DT1.Rows.Add("Accounting.CheckVoucherTagging", "cond", "LineNumber", CheckVoucherTagging.LineNumber);
                DT1.Rows.Add("Accounting.CheckVoucherTagging", "cond", "RecordId", CheckVoucherTagging.RecordId);
                DT1.Rows.Add("Accounting.CheckVoucherTagging", "set", "TransDocNumber", CheckVoucherTagging.TransDocNumber);
                DT1.Rows.Add("Accounting.CheckVoucherTagging", "set", "TransType", CheckVoucherTagging.TransType);
                DT1.Rows.Add("Accounting.CheckVoucherTagging", "set", "TransDate", CheckVoucherTagging.TransDate);
                DT1.Rows.Add("Accounting.CheckVoucherTagging", "set", "TransDueDate", CheckVoucherTagging.TransDueDate);
                DT1.Rows.Add("Accounting.CheckVoucherTagging", "set", "TransAccountCode", CheckVoucherTagging.TransAccountCode);
                DT1.Rows.Add("Accounting.CheckVoucherTagging", "set", "TransSubsiCode", CheckVoucherTagging.TransSubsiCode);
                DT1.Rows.Add("Accounting.CheckVoucherTagging", "set", "TransProfitCenter", CheckVoucherTagging.TransProfitCenter);
                DT1.Rows.Add("Accounting.CheckVoucherTagging", "set", "TransCostCenter", CheckVoucherTagging.TransCostCenter);
                DT1.Rows.Add("Accounting.CheckVoucherTagging", "set", "TransBizPartnerCode", CheckVoucherTagging.TransBizPartnerCode);
                DT1.Rows.Add("Accounting.CheckVoucherTagging", "set", "TransAmount", CheckVoucherTagging.TransAmount);
                DT1.Rows.Add("Accounting.CheckVoucherTagging", "set", "TransAppliedAmount", CheckVoucherTagging.TransAppliedAmount);
                DT1.Rows.Add("Accounting.CheckVoucherTagging", "set", "Field1", CheckVoucherTagging.Field1);
                DT1.Rows.Add("Accounting.CheckVoucherTagging", "set", "Field2", CheckVoucherTagging.Field2);
                DT1.Rows.Add("Accounting.CheckVoucherTagging", "set", "Field3", CheckVoucherTagging.Field3);
                DT1.Rows.Add("Accounting.CheckVoucherTagging", "set", "Field4", CheckVoucherTagging.Field4);
                DT1.Rows.Add("Accounting.CheckVoucherTagging", "set", "Field5", CheckVoucherTagging.Field5);
                DT1.Rows.Add("Accounting.CheckVoucherTagging", "set", "Field6", CheckVoucherTagging.Field6);
                DT1.Rows.Add("Accounting.CheckVoucherTagging", "set", "Field7", CheckVoucherTagging.Field7);
                DT1.Rows.Add("Accounting.CheckVoucherTagging", "set", "Field8", CheckVoucherTagging.Field8);
                DT1.Rows.Add("Accounting.CheckVoucherTagging", "set", "Field9", CheckVoucherTagging.Field9);
                DT1.Rows.Add("Accounting.CheckVoucherTagging", "set", "Version", "1");

                Gears.UpdateData(DT1,Conn);
            }
            public void DeleteCheckVoucherTagging(CheckVoucherTagging CheckVoucherTagging)
            {
                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
                DT1.Rows.Add("Accounting.CheckVoucherTagging", "cond", "DocNumber", Docnum);
                DT1.Rows.Add("Accounting.CheckVoucherTagging", "cond", "RecordID", CheckVoucherTagging.RecordId);

                Gears.DeleteData(DT1,Conn);
            }
        }
        public class CheckVoucherAdjEntry
        {
            public virtual CheckVoucher Parent { get; set; }
            public virtual string DocNumber { get; set; }
            public virtual string LineNumber { get; set; }
            public virtual string AccountCode { get; set; }
            public virtual string AccountDescription { get; set; }
            public virtual string SubsidiaryCode { get; set; }
            public virtual string SubsidiaryDescription { get; set; }
            public virtual string ProfitCenterCode { get; set; }
            public virtual string CostCenterCode { get; set; }
            public virtual string Bizpartner { get; set; }
            public virtual decimal DebitAmount { get; set; }
            public virtual decimal CreditAmount { get; set; }
            public virtual decimal TotalAmount { get; set; }
            public virtual string Field1 { get; set; }
            public virtual string Field2 { get; set; }
            public virtual string Field3 { get; set; }
            public virtual string Field4 { get; set; }
            public virtual string Field5 { get; set; }
            public virtual string Field6 { get; set; }
            public virtual string Field7 { get; set; }
            public virtual string Field8 { get; set; }
            public virtual string Field9 { get; set; }

            public DataTable getdetail(string DocNumber,string Conn)
            {

                DataTable a;
                try
                {
                    a = Gears.RetriveData2("SELECT *,0 AS Total FROM  Accounting.CheckVoucherAdjEntry WHERE DocNumber='" + DocNumber + "' ORDER BY LineNumber",Conn);
                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }

            public void AddCheckVoucherAdjEntry(CheckVoucherAdjEntry CheckVoucherAdjEntry)
            {
                int linenum = 0;

                DataTable count = Gears.RetriveData2("SELECT MAX(CONVERT(int,LineNumber)) AS LineNumber FROM Accounting.CheckVoucherAdjEntry WHERE DocNumber = '" + Docnum + "'",Conn);

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
                DT1.Rows.Add("Accounting.CheckVoucherAdjEntry", "0", "DocNumber", Docnum);
                DT1.Rows.Add("Accounting.CheckVoucherAdjEntry", "0", "LineNumber", strLine);
                DT1.Rows.Add("Accounting.CheckVoucherAdjEntry", "0", "AccountCode", CheckVoucherAdjEntry.AccountCode);
                DT1.Rows.Add("Accounting.CheckVoucherAdjEntry", "0", "AccountDescription", CheckVoucherAdjEntry.AccountDescription);
                DT1.Rows.Add("Accounting.CheckVoucherAdjEntry", "0", "SubsidiaryCode", CheckVoucherAdjEntry.SubsidiaryCode);
                DT1.Rows.Add("Accounting.CheckVoucherAdjEntry", "0", "SubsidiaryDescription", CheckVoucherAdjEntry.SubsidiaryDescription);
                DT1.Rows.Add("Accounting.CheckVoucherAdjEntry", "0", "ProfitCenterCode", CheckVoucherAdjEntry.ProfitCenterCode);
                DT1.Rows.Add("Accounting.CheckVoucherAdjEntry", "0", "CostCenterCode", CheckVoucherAdjEntry.CostCenterCode);
                DT1.Rows.Add("Accounting.CheckVoucherAdjEntry", "0", "Bizpartner", CheckVoucherAdjEntry.Bizpartner);
                DT1.Rows.Add("Accounting.CheckVoucherAdjEntry", "0", "DebitAmount", CheckVoucherAdjEntry.DebitAmount);
                DT1.Rows.Add("Accounting.CheckVoucherAdjEntry", "0", "CreditAmount", CheckVoucherAdjEntry.CreditAmount);
                DT1.Rows.Add("Accounting.CheckVoucherAdjEntry", "0", "TotalAmount", CheckVoucherAdjEntry.TotalAmount);
                DT1.Rows.Add("Accounting.CheckVoucherAdjEntry", "0", "Field1", CheckVoucherAdjEntry.Field1);
                DT1.Rows.Add("Accounting.CheckVoucherAdjEntry", "0", "Field2", CheckVoucherAdjEntry.Field2);
                DT1.Rows.Add("Accounting.CheckVoucherAdjEntry", "0", "Field3", CheckVoucherAdjEntry.Field3);
                DT1.Rows.Add("Accounting.CheckVoucherAdjEntry", "0", "Field4", CheckVoucherAdjEntry.Field4);
                DT1.Rows.Add("Accounting.CheckVoucherAdjEntry", "0", "Field5", CheckVoucherAdjEntry.Field5);
                DT1.Rows.Add("Accounting.CheckVoucherAdjEntry", "0", "Field6", CheckVoucherAdjEntry.Field6);
                DT1.Rows.Add("Accounting.CheckVoucherAdjEntry", "0", "Field7", CheckVoucherAdjEntry.Field7);
                DT1.Rows.Add("Accounting.CheckVoucherAdjEntry", "0", "Field8", CheckVoucherAdjEntry.Field8);
                DT1.Rows.Add("Accounting.CheckVoucherAdjEntry", "0", "Field9", CheckVoucherAdjEntry.Field9);

                Gears.CreateData(DT1,Conn);
            }
            public void UpdateCheckVoucherAdjEntry(CheckVoucherAdjEntry CheckVoucherAdjEntry)
            {

                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                DT1.Rows.Add("Accounting.CheckVoucherAdjEntry", "cond", "DocNumber", Docnum);
                DT1.Rows.Add("Accounting.CheckVoucherAdjEntry", "cond", "LineNumber", CheckVoucherAdjEntry.LineNumber);
                DT1.Rows.Add("Accounting.CheckVoucherAdjEntry", "set", "AccountCode", CheckVoucherAdjEntry.AccountCode);
                DT1.Rows.Add("Accounting.CheckVoucherAdjEntry", "set", "AccountDescription", CheckVoucherAdjEntry.AccountDescription);
                DT1.Rows.Add("Accounting.CheckVoucherAdjEntry", "set", "SubsidiaryCode", CheckVoucherAdjEntry.SubsidiaryCode);
                DT1.Rows.Add("Accounting.CheckVoucherAdjEntry", "set", "SubsidiaryDescription", CheckVoucherAdjEntry.SubsidiaryDescription);
                DT1.Rows.Add("Accounting.CheckVoucherAdjEntry", "set", "ProfitCenterCode", CheckVoucherAdjEntry.ProfitCenterCode);
                DT1.Rows.Add("Accounting.CheckVoucherAdjEntry", "set", "CostCenterCode", CheckVoucherAdjEntry.CostCenterCode);
                DT1.Rows.Add("Accounting.CheckVoucherAdjEntry", "set", "Bizpartner", CheckVoucherAdjEntry.Bizpartner);
                DT1.Rows.Add("Accounting.CheckVoucherAdjEntry", "set", "DebitAmount", CheckVoucherAdjEntry.DebitAmount);
                DT1.Rows.Add("Accounting.CheckVoucherAdjEntry", "set", "CreditAmount", CheckVoucherAdjEntry.CreditAmount);
                DT1.Rows.Add("Accounting.CheckVoucherAdjEntry", "set", "TotalAmount", CheckVoucherAdjEntry.TotalAmount);
                DT1.Rows.Add("Accounting.CheckVoucherAdjEntry", "set", "Field1", CheckVoucherAdjEntry.Field1);
                DT1.Rows.Add("Accounting.CheckVoucherAdjEntry", "set", "Field2", CheckVoucherAdjEntry.Field2);
                DT1.Rows.Add("Accounting.CheckVoucherAdjEntry", "set", "Field3", CheckVoucherAdjEntry.Field3);
                DT1.Rows.Add("Accounting.CheckVoucherAdjEntry", "set", "Field4", CheckVoucherAdjEntry.Field4);
                DT1.Rows.Add("Accounting.CheckVoucherAdjEntry", "set", "Field5", CheckVoucherAdjEntry.Field5);
                DT1.Rows.Add("Accounting.CheckVoucherAdjEntry", "set", "Field6", CheckVoucherAdjEntry.Field6);
                DT1.Rows.Add("Accounting.CheckVoucherAdjEntry", "set", "Field7", CheckVoucherAdjEntry.Field7);
                DT1.Rows.Add("Accounting.CheckVoucherAdjEntry", "set", "Field8", CheckVoucherAdjEntry.Field8);
                DT1.Rows.Add("Accounting.CheckVoucherAdjEntry", "set", "Field9", CheckVoucherAdjEntry.Field9);

                Gears.UpdateData(DT1,Conn);
            }
            public void DeleteCheckVoucherAdjEntry(CheckVoucherAdjEntry CheckVoucherAdjEntry)
            {
                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
                DT1.Rows.Add("Accounting.CheckVoucherAdjEntry", "cond", "DocNumber", Docnum);
                DT1.Rows.Add("Accounting.CheckVoucherAdjEntry", "cond", "LineNumber", CheckVoucherAdjEntry.LineNumber);

                Gears.DeleteData(DT1,Conn);
            }
        }
        public class JournalEntry
        {
            public virtual CheckVoucher Parent { get; set; }
            public virtual string AccountCode { get; set; }
            public virtual string AccountDescription { get; set; }
            public virtual string SubsidiaryCode { get; set; }
            public virtual string SubsidiaryDescription { get; set; }
            public virtual string ProfitCenter { get; set; }
            public virtual string CostCenter { get; set; }
            public virtual string Debit { get; set; }
            public virtual string Credit { get; set; }
            public DataTable getJournalEntry(string DocNumber,string Conn)
            {

                DataTable a;
                try
                {
                    a = Gears.RetriveData2("SELECT A.AccountCode, B.Description AS AccountDescription, A.SubsiCode AS SubsidiaryCode, C.Description AS SubsidiaryDescription, "
                    + " ProfitCenterCode AS ProfitCenter, CostCenterCode AS CostCenter, CONVERT(varchar,CONVERT(money,DebitAmount),1) AS Debit, CONVERT(varchar,CONVERT(money,CreditAmount),1) AS Credit  FROM Accounting.GeneralLedger A "
                    + " INNER JOIN Accounting.ChartOfAccount B ON A.AccountCode = B.AccountCode "
                    + " INNER JOIN Accounting.GLSubsiCode C ON A.SubsiCode = C.SubsiCode "
                    + " AND A.AccountCode = C.AccountCode WHERE A.DocNumber = '" + DocNumber + "' AND TransType ='ACTCHV' ", Conn);

                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }
        }

        public DataTable getdata(string DocNumber,string Conn)
        {
            DataTable a;

            a = Gears.RetriveData2("SELECT * FROM Accounting.CheckVoucher WHERE DocNumber = '" + DocNumber + "'",Conn);
            foreach (DataRow dtRow in a.Rows)
            {
                DocNumber = dtRow["DocNumber"].ToString();
                DocDate = dtRow["Docdate"].ToString();
                TransType = dtRow["TransType"].ToString();
                SupplierCode = dtRow["SupplierCode"].ToString();
                SupplierName = dtRow["SupplierName"].ToString();
                TotalCheckAmount = Convert.ToDecimal(string.IsNullOrEmpty(dtRow["TotalCheckAmount"].ToString()) ? 0 : dtRow["TotalCheckAmount"]);
                TotalAppliedAmount = Convert.ToDecimal(string.IsNullOrEmpty(dtRow["TotalAppliedAmount"].ToString()) ? 0 : dtRow["TotalAppliedAmount"]);
                Field1 = dtRow["Field1"].ToString();
                Field2 = dtRow["Field2"].ToString();
                Field3 = dtRow["Field3"].ToString();
                Field4 = dtRow["Field4"].ToString();
                Field5 = dtRow["Field5"].ToString();
                Field6 = dtRow["Field6"].ToString();
                Field7 = dtRow["Field7"].ToString();
                Field8 = dtRow["Field8"].ToString();
                Field9 = dtRow["Field9"].ToString();
                Remarks = dtRow["Remarks"].ToString();
                RefTrans = dtRow["RefTrans"].ToString();
                AddedBy = dtRow["AddedBy"].ToString();
                AddedDate = dtRow["AddedDate"].ToString();
                LastEditedBy = dtRow["LastEditedBy"].ToString();
                LastEditedDate = dtRow["LastEditedDate"].ToString();
                SubmittedBy = dtRow["SubmittedBy"].ToString();
                SubmittedDate = dtRow["SubmittedDate"].ToString();
                TaggedTransaction = dtRow["TaggedTransaction"].ToString();
            }

            return a;
        }
        public void InsertData(CheckVoucher _ent)
        {
            Docnum = _ent.DocNumber;
            Conn = _ent.Connection;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add("Accounting.CheckVoucher", "0", "DocNumber", _ent.DocNumber);
            DT1.Rows.Add("Accounting.CheckVoucher", "0", "DocDate", _ent.DocDate);
            DT1.Rows.Add("Accounting.CheckVoucher", "0", "TransType", _ent.TransType);
            DT1.Rows.Add("Accounting.CheckVoucher", "0", "SupplierCode", _ent.SupplierCode);
            DT1.Rows.Add("Accounting.CheckVoucher", "0", "SupplierName", _ent.SupplierName);
            DT1.Rows.Add("Accounting.CheckVoucher", "0", "TotalCheckAmount", _ent.TotalCheckAmount);
            DT1.Rows.Add("Accounting.CheckVoucher", "0", "TotalAppliedAmount", _ent.TotalAppliedAmount);
            DT1.Rows.Add("Accounting.CheckVoucher", "0", "Field1", _ent.Field1);
            DT1.Rows.Add("Accounting.CheckVoucher", "0", "Field2", _ent.Field2);
            DT1.Rows.Add("Accounting.CheckVoucher", "0", "Field3", _ent.Field3);
            DT1.Rows.Add("Accounting.CheckVoucher", "0", "Field4", _ent.Field4);
            DT1.Rows.Add("Accounting.CheckVoucher", "0", "Field5", _ent.Field5);
            DT1.Rows.Add("Accounting.CheckVoucher", "0", "Field6", _ent.Field6);
            DT1.Rows.Add("Accounting.CheckVoucher", "0", "Field7", _ent.Field7);
            DT1.Rows.Add("Accounting.CheckVoucher", "0", "Field8", _ent.Field8);
            DT1.Rows.Add("Accounting.CheckVoucher", "0", "Field9", _ent.Field9);
            DT1.Rows.Add("Accounting.CheckVoucher", "0", "Remarks", _ent.Remarks);
            DT1.Rows.Add("Accounting.CheckVoucher", "0", "RefTrans", _ent.RefTrans);
            DT1.Rows.Add("Accounting.CheckVoucher", "0", "IsWithDetail", "False");
            DT1.Rows.Add("Accounting.CheckVoucher", "0", "AddedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            DT1.Rows.Add("Accounting.CheckVoucher", "0", "TaggedTransaction", _ent.TaggedTransaction);

            Gears.CreateData(DT1,_ent.Connection);
        }
        public void UpdateData(CheckVoucher _ent)
        {
            Docnum = _ent.DocNumber;
            Conn = _ent.Connection;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Accounting.CheckVoucher", "cond", "DocNumber", _ent.DocNumber);
            DT1.Rows.Add("Accounting.CheckVoucher", "set", "DocDate", _ent.DocDate);
            DT1.Rows.Add("Accounting.CheckVoucher", "set", "TransType", _ent.TransType);
            DT1.Rows.Add("Accounting.CheckVoucher", "set", "SupplierCode", _ent.SupplierCode);
            DT1.Rows.Add("Accounting.CheckVoucher", "set", "SupplierName", _ent.SupplierName);
            DT1.Rows.Add("Accounting.CheckVoucher", "set", "TotalCheckAmount", _ent.TotalCheckAmount);
            DT1.Rows.Add("Accounting.CheckVoucher", "set", "TotalAppliedAmount", _ent.TotalAppliedAmount);
            DT1.Rows.Add("Accounting.CheckVoucher", "set", "Field1", _ent.Field1);
            DT1.Rows.Add("Accounting.CheckVoucher", "set", "Field2", _ent.Field2);
            DT1.Rows.Add("Accounting.CheckVoucher", "set", "Field3", _ent.Field3);
            DT1.Rows.Add("Accounting.CheckVoucher", "set", "Field4", _ent.Field4);
            DT1.Rows.Add("Accounting.CheckVoucher", "set", "Field5", _ent.Field5);
            DT1.Rows.Add("Accounting.CheckVoucher", "set", "Field6", _ent.Field6);
            DT1.Rows.Add("Accounting.CheckVoucher", "set", "Field7", _ent.Field7);
            DT1.Rows.Add("Accounting.CheckVoucher", "set", "Field8", _ent.Field8);
            DT1.Rows.Add("Accounting.CheckVoucher", "set", "Field9", _ent.Field9);
            DT1.Rows.Add("Accounting.CheckVoucher", "set", "Remarks", _ent.Remarks);
            DT1.Rows.Add("Accounting.CheckVoucher", "set", "RefTrans", _ent.RefTrans);
            DT1.Rows.Add("Accounting.CheckVoucher", "set", "LastEditedBy", _ent.LastEditedBy);
            DT1.Rows.Add("Accounting.CheckVoucher", "set", "LastEditedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            DT1.Rows.Add("Accounting.CheckVoucher", "set", "TaggedTransaction", _ent.TaggedTransaction);

            string strErr = Gears.UpdateData(DT1,_ent.Connection);

            Functions.AuditTrail("ACTCHV", Docnum, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "UPDATE",_ent.Connection);
        }
        public void DeleteData(CheckVoucher _ent)
        {
            Docnum = _ent.DocNumber;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Accounting.CheckVoucher", "cond", "DocNumber", _ent.DocNumber);
            Gears.DeleteData(DT1,_ent.Connection);
            Functions.AuditTrail("ACTCHV", Docnum, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "DELETE",_ent.Connection);
        }
        public void DeleteFirstData(string DocNumber, string Conn)
        {
            DataTable a = new DataTable();
            a = Gears.RetriveData2("DELETE FROM Accounting.CheckVoucherTagging WHERE DocNumber = '" + DocNumber + "'", Conn);
        }

        public void DeleteTagsAndAdj(string DocNumber, string Conn)
        {
            DataTable a = new DataTable();
            a = Gears.RetriveData2("DELETE FROM Accounting.CheckVoucherTagging WHERE DocNumber = '" + DocNumber + "' DELETE FROM Accounting.CheckVoucherAdjEntry WHERE DocNumber = '" + DocNumber + "'", Conn);
        }
    }
}

