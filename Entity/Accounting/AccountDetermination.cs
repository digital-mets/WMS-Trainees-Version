using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GearsLibrary;
using System.Data;

namespace Entity
{
    public class AccountDetermination
    {
        private static string TransactionType;

        private static string Conn;//ADD CONN
        public virtual string Connection { get; set; }//ADD CONN
        public virtual string TransType { get; set; }
        public virtual string Description { get; set; }
        public virtual string Module { get; set; }


        public virtual bool IsWithDetail { get; set; }
        public virtual bool IsValidated { get; set; }

        public virtual string AddedBy { get; set; }
        public virtual string AddedDate { get; set; }
        public virtual string LastEditedBy { get; set; }
        public virtual string LastEditedDate { get; set; }
        //public virtual string SubmittedBy { get; set; }
        //public virtual string SubmittedDate { get; set; }
        //public virtual string CancelledBy { get; set; }
        //public virtual string CancelledDate { get; set; }
        //public virtual string PostedBy { get; set; }
        //public virtual string PostedDate { get; set; }
        
        
        public virtual string Field1 { get; set; }
        public virtual string Field2 { get; set; }
        public virtual string Field3 { get; set; }
        public virtual string Field4 { get; set; }
        public virtual string Field5 { get; set; }
        public virtual string Field6 { get; set; }
        public virtual string Field7 { get; set; }
        public virtual string Field8 { get; set; }
        public virtual string Field9 { get; set; }





        public virtual IList<AccountDeterminationDetail> Detail { get; set; }
        public class AccountDeterminationDetail
        {
            public virtual AccountDetermination Parent { get; set; } 
            public virtual string LineNumber { get; set; }
            public virtual string Transtype { get; set; }
            public virtual string TOACode { get; set; }
            public virtual string TypeofAccount { get; set; }
            public virtual string AccountCode { get; set; }
            public virtual string SubsiCode { get; set; }
            

            public virtual string Field1 { get; set; }
            public virtual string Field2 { get; set; }
            public virtual string Field3 { get; set; }
            public virtual string Field4 { get; set; }
            public virtual string Field5 { get; set; }
            public virtual string Field6 { get; set; }
            public virtual string Field7 { get; set; }
            public virtual string Field8 { get; set; }
            public virtual string Field9 { get; set; }
            public DataTable getdetail(string Trans, string Conn)
            {
                //Trans = string.IsNullOrEmpty(TransactionType) ? Trans : TransactionType;
                
                DataTable a;
                try
                {
                    a = Gears.RetriveData2("select * from Accounting.AccountDeterminationDetail where Transtype='" + Trans +"'", Conn);
                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }

            public void AddAccountDeterminationDetail(AccountDeterminationDetail AccountDeterminationDetail)
            {
                //int linenum = 0;
                ////bool isbybulk = false;

                //DataTable count = Gears.RetriveData2("select max(convert(int,LineNumber)) as LineNumber from Accounting.AssetDisposalDetail where DocNumber = '" + Docnum + "'", Conn);

                //try
                //{
                //    linenum = Convert.ToInt32(count.Rows[0][0].ToString()) + 1;
                //}
                //catch
                //{
                //    linenum = 1;
                //}
                //string strLine = linenum.ToString().PadLeft(5, '0');

                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
                DT1.Rows.Add("Accounting.AccountDeterminationDetail", "0", "TransType", TransactionType);
                DT1.Rows.Add("Accounting.AccountDeterminationDetail", "0", "TOACode", AccountDeterminationDetail.TOACode);


                DT1.Rows.Add("Accounting.AccountDeterminationDetail", "0", "TypeofAccount", AccountDeterminationDetail.TypeofAccount);
                DT1.Rows.Add("Accounting.AccountDeterminationDetail", "0", "AccountCode", AccountDeterminationDetail.AccountCode);
                DT1.Rows.Add("Accounting.AccountDeterminationDetail", "0", "SubsiCode", AccountDeterminationDetail.SubsiCode);
                //DT1.Rows.Add("Accounting.AccountDeterminationDetail", "0", "FullDesc", AccountDeterminationDetail.FullDesc);
                //DT1.Rows.Add("Accounting.AccountDeterminationDetail", "0", "ColorCode", AccountDeterminationDetail.ColorCode);
                //DT1.Rows.Add("Accounting.AccountDeterminationDetail", "0", "Qty", AccountDeterminationDetail.Qty);
                //DT1.Rows.Add("Accounting.AccountDeterminationDetail", "0", "OrigQty", AssetDisposalDetail.OrigQty);
                //DT1.Rows.Add("Accounting.AssetDisposalDetail", "0", "UnitPrice", AssetDisposalDetail.UnitPrice);
                //DT1.Rows.Add("Accounting.AssetDisposalDetail", "0", "UnitCost", AssetDisposalDetail.UnitCost);
                //DT1.Rows.Add("Accounting.AssetDisposalDetail", "0", "AccumulatedDepreciation", AssetDisposalDetail.AccumulatedDepreciation);
                //DT1.Rows.Add("Accounting.AssetDisposalDetail", "0", "SoldAmount", AssetDisposalDetail.SoldAmount);
                //DT1.Rows.Add("Accounting.AssetDisposalDetail", "0", "IsVat", AssetDisposalDetail.IsVat);
                //DT1.Rows.Add("Accounting.AssetDisposalDetail", "0", "Rate", AssetDisposalDetail.Rate);
                //DT1.Rows.Add("Accounting.AssetDisposalDetail", "0", "VATCode", AssetDisposalDetail.VATCode);
                //DT1.Rows.Add("Accounting.AssetDisposalDetail", "0", "PropertyStatus", AssetDisposalDetail.PropertyStatus);


                DT1.Rows.Add("Accounting.AccountDeterminationDetail", "0", "Field1", AccountDeterminationDetail.Field1);
                DT1.Rows.Add("Accounting.AccountDeterminationDetail", "0", "Field2", AccountDeterminationDetail.Field2);
                DT1.Rows.Add("Accounting.AccountDeterminationDetail", "0", "Field3", AccountDeterminationDetail.Field3);
                DT1.Rows.Add("Accounting.AccountDeterminationDetail", "0", "Field4", AccountDeterminationDetail.Field4);
                DT1.Rows.Add("Accounting.AccountDeterminationDetail", "0", "Field5", AccountDeterminationDetail.Field5);
                DT1.Rows.Add("Accounting.AccountDeterminationDetail", "0", "Field6", AccountDeterminationDetail.Field6);
                DT1.Rows.Add("Accounting.AccountDeterminationDetail", "0", "Field7", AccountDeterminationDetail.Field7);
                DT1.Rows.Add("Accounting.AccountDeterminationDetail", "0", "Field8", AccountDeterminationDetail.Field8);
                DT1.Rows.Add("Accounting.AccountDeterminationDetail", "0", "Field9", AccountDeterminationDetail.Field9);


                DT2.Rows.Add("Accounting.AccountDetermination", "cond", "TransType", TransactionType);
                DT2.Rows.Add("Accounting.AccountDetermination", "set", "IsWithDetail", "True");

                Gears.CreateData(DT1, Conn);
                Gears.UpdateData(DT2, Conn);

            }
            public void UpdateAccountDeterminationDetail(AccountDeterminationDetail AccountDeterminationDetail)
            {

                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                DT1.Rows.Add("Accounting.AccountDeterminationDetail", "cond", "TransType", TransactionType);
                DT1.Rows.Add("Accounting.AccountDeterminationDetail", "cond", "TOACode", AccountDeterminationDetail.TOACode);


                DT1.Rows.Add("Accounting.AccountDeterminationDetail", "set", "TypeofAccount", AccountDeterminationDetail.TypeofAccount);
                DT1.Rows.Add("Accounting.AccountDeterminationDetail", "set", "AccountCode", AccountDeterminationDetail.AccountCode);
                DT1.Rows.Add("Accounting.AccountDeterminationDetail", "set", "SubsiCode", AccountDeterminationDetail.SubsiCode);


                DT1.Rows.Add("Accounting.AccountDeterminationDetail", "set", "Field1", AccountDeterminationDetail.Field1);
                DT1.Rows.Add("Accounting.AccountDeterminationDetail", "set", "Field2", AccountDeterminationDetail.Field2);
                DT1.Rows.Add("Accounting.AccountDeterminationDetail", "set", "Field3", AccountDeterminationDetail.Field3);
                DT1.Rows.Add("Accounting.AccountDeterminationDetail", "set", "Field4", AccountDeterminationDetail.Field4);
                DT1.Rows.Add("Accounting.AccountDeterminationDetail", "set", "Field5", AccountDeterminationDetail.Field5);
                DT1.Rows.Add("Accounting.AccountDeterminationDetail", "set", "Field6", AccountDeterminationDetail.Field6);
                DT1.Rows.Add("Accounting.AccountDeterminationDetail", "set", "Field7", AccountDeterminationDetail.Field7);
                DT1.Rows.Add("Accounting.AccountDeterminationDetail", "set", "Field8", AccountDeterminationDetail.Field8);
                DT1.Rows.Add("Accounting.AccountDeterminationDetail", "set", "Field9", AccountDeterminationDetail.Field9);

                Gears.UpdateData(DT1, Conn);                               
            }
            public void DeleteAccountDeterminationDetail(AccountDeterminationDetail AccountDeterminationDetail)
            {


                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
                DT1.Rows.Add("Accounting.AccountDeterminationDetail", "cond", "TransType", TransactionType);
                DT1.Rows.Add("Accounting.AccountDeterminationDetail", "cond", "TOACode", AccountDeterminationDetail.TOACode);
                Gears.DeleteData(DT1, Conn);

                DataTable count = Gears.RetriveData2("select * from Accounting.AccountDeterminationDetail where TransType = '" + TransactionType + "'", Conn);

                if (count.Rows.Count < 1)
                {
                    DT2.Rows.Add("Accounting.AccountDeterminationDetail", "cond", "TransType", TransactionType);
                    DT2.Rows.Add("Accounting.AccountDeterminationDetail", "set", "IsWithDetail", "False");
                    Gears.UpdateData(DT2, Conn);
                }

            }
        }


        public DataTable getdata(string Trans, string Conn)
        {
            DataTable a;

            //if (DocNumber != null)
            //{
            a = Gears.RetriveData2("select * from Accounting.AccountDetermination where TransType = '" + Trans + "'", Conn);
            foreach (DataRow dtRow in a.Rows)
            {
                TransType = dtRow["TransType"].ToString();
                Description = dtRow["Description"].ToString();
                Module = dtRow["Module"].ToString();
                //DisposalType = dtRow["DisposalType"].ToString();
                //SoldTo = dtRow["SoldTo"].ToString();
                //TotalAmountSold = Convert.ToDecimal(Convert.IsDBNull(dtRow["TotalAmountSold"]) ? 0 : dtRow["TotalAmountSold"]);
                //GrossVATAmount = Convert.ToDecimal(Convert.IsDBNull(dtRow["GrossVATAmount"]) ? 0 : dtRow["GrossVATAmount"]);
                //GrossNonVATAmount = Convert.ToDecimal(Convert.IsDBNull(dtRow["GrossNonVATAmount"]) ? 0 : dtRow["GrossNonVATAmount"]);
                //Remarks = dtRow["Remarks"].ToString();
                //TotalAssetCost = Convert.ToDecimal(Convert.IsDBNull(dtRow["TotalAssetCost"]) ? 0 : dtRow["TotalAssetCost"]);
                //TotalAccumulatedDepreciation = Convert.ToDecimal(Convert.IsDBNull(dtRow["TotalAccumulatedDepreciation"]) ? 0 : dtRow["TotalAccumulatedDepreciation"]);
                //NetBookValue = Convert.ToDecimal(Convert.IsDBNull(dtRow["NetBookValue"]) ? 0 : dtRow["NetBookValue"]);
                //TotalGainLoss = Convert.ToDecimal(Convert.IsDBNull(dtRow["TotalGainLoss"]) ? 0 : dtRow["TotalGainLoss"]);
                
                AddedBy = dtRow["AddedBy"].ToString();
                AddedDate = dtRow["AddedDate"].ToString();
                LastEditedBy = dtRow["LastEditedBy"].ToString();
                LastEditedDate = dtRow["LastEditedDate"].ToString();
                //SubmittedBy = dtRow["SubmittedBy"].ToString();
                //SubmittedDate = dtRow["SubmittedDate"].ToString();
                //CancelledBy = dtRow["CancelledBy"].ToString();
                //CancelledDate = dtRow["CancelledDate"].ToString();
                //PostedBy = dtRow["PostedBy"].ToString();
                //PostedDate = dtRow["PostedDate"].ToString();


                IsValidated = Convert.ToBoolean(Convert.IsDBNull(dtRow["IsValidated"]) ? false : dtRow["IsValidated"]);
                IsWithDetail = Convert.ToBoolean(Convert.IsDBNull(dtRow["IsWithDetail"]) ? false : dtRow["IsWithDetail"]);


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
        public void InsertData(AccountDetermination _ent)
        {
            TransactionType = _ent.TransType;
            Conn = _ent.Connection;
            //trans = _ent.TransType;
            //ddate = _ent.DocDate;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add("Accounting.AccountDetermination", "0", "TransType", _ent.TransType);
            //DT1.Rows.Add("Accounting.AssetDisposal", "0", "InvoiceDocNum", _ent.InvoiceDocNumber);
            DT1.Rows.Add("Accounting.AccountDetermination", "0", "Description", _ent.Description);


            DT1.Rows.Add("Accounting.AccountDetermination", "0", "Module", _ent.Module);
            //DT1.Rows.Add("Accounting.AssetDisposal", "0", "SoldTo", _ent.SoldTo);
            //DT1.Rows.Add("Accounting.AssetDisposal", "0", "TotalAmountSold", _ent.TotalAmountSold);
            //DT1.Rows.Add("Accounting.AssetDisposal", "0", "GrossVATAmount", _ent.GrossVATAmount);
            //DT1.Rows.Add("Accounting.AssetDisposal", "0", "GrossNonVATAmount", _ent.GrossNonVATAmount);
            //DT1.Rows.Add("Accounting.AssetDisposal", "0", "Remarks", _ent.Remarks);
            //DT1.Rows.Add("Accounting.AssetDisposal", "0", "TotalAssetCost", _ent.TotalAssetCost);
            //DT1.Rows.Add("Accounting.AssetDisposal", "0", "TotalAccumulatedDepreciation", _ent.TotalAccumulatedDepreciation);
            //DT1.Rows.Add("Accounting.AssetDisposal", "0", "NetBookValue", _ent.NetBookValue);
            //DT1.Rows.Add("Accounting.AssetDisposal", "0", "TotalGainLoss", _ent.TotalGainLoss);


            DT1.Rows.Add("Accounting.AccountDetermination", "0", "AddedBy", _ent.AddedBy);
            DT1.Rows.Add("Accounting.AccountDetermination", "0", "AddedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            //DT1.Rows.Add("Accounting.AssetDisposal", "0", "IsValidated", _ent.IsValidated);
            //DT1.Rows.Add("Accounting.AssetDisposal", "0", "IsWithDetail", "False");

            DT1.Rows.Add("Accounting.AccountDetermination", "0", "Field1", _ent.Field1);
            DT1.Rows.Add("Accounting.AccountDetermination", "0", "Field2", _ent.Field2);
            DT1.Rows.Add("Accounting.AccountDetermination", "0", "Field3", _ent.Field3);
            DT1.Rows.Add("Accounting.AccountDetermination", "0", "Field4", _ent.Field4);
            DT1.Rows.Add("Accounting.AccountDetermination", "0", "Field5", _ent.Field5);
            DT1.Rows.Add("Accounting.AccountDetermination", "0", "Field6", _ent.Field6);
            DT1.Rows.Add("Accounting.AccountDetermination", "0", "Field7", _ent.Field7);
            DT1.Rows.Add("Accounting.AccountDetermination", "0", "Field8", _ent.Field8);
            DT1.Rows.Add("Accounting.AccountDetermination", "0", "Field9", _ent.Field9);



            Gears.CreateData(DT1, _ent.Connection);
        }

        public void UpdateData(AccountDetermination _ent)
        {
            TransactionType = _ent.TransType;
            Conn = _ent.Connection;
            //trans = _ent.TransType;
            //ddate = _ent.DocDate;


            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add("Accounting.AccountDetermination", "cond", "TransType", _ent.TransType);
            //DT1.Rows.Add("Accounting.AssetDisposal", "set", "InvoiceDocNum", _ent.InvoiceDocNumber);
            DT1.Rows.Add("Accounting.AccountDetermination", "set", "Description", _ent.Description);
            DT1.Rows.Add("Accounting.AccountDetermination", "set", "Module", _ent.Module);
            //DT1.Rows.Add("Accounting.AssetDisposal", "set", "SoldTo", _ent.SoldTo);
            //DT1.Rows.Add("Accounting.AssetDisposal", "set", "TotalAmountSold", _ent.TotalAmountSold);
            //DT1.Rows.Add("Accounting.AssetDisposal", "set", "GrossVATAmount", _ent.GrossVATAmount);
            //DT1.Rows.Add("Accounting.AssetDisposal", "set", "GrossNonVATAmount", _ent.GrossNonVATAmount);
            //DT1.Rows.Add("Accounting.AssetDisposal", "set", "Remarks", _ent.Remarks);
            //DT1.Rows.Add("Accounting.AssetDisposal", "set", "TotalAssetCost", _ent.TotalAssetCost);
            //DT1.Rows.Add("Accounting.AssetDisposal", "set", "TotalAccumulatedDepreciation", _ent.TotalAccumulatedDepreciation);
            //DT1.Rows.Add("Accounting.AssetDisposal", "set", "NetBookValue", _ent.NetBookValue);
            //DT1.Rows.Add("Accounting.AssetDisposal", "set", "TotalGainLoss", _ent.TotalGainLoss);


            DT1.Rows.Add("Accounting.AssetDisposal", "set", "LastEditedBy", _ent.LastEditedBy);
            DT1.Rows.Add("Accounting.AssetDisposal", "set", "LastEditedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));


            //DT1.Rows.Add("Accounting.AssetDisposal", "set", "IsValidated", _ent.IsValidated);
            //DT1.Rows.Add("Accounting.AssetDisposal", "set", "IsWithDetail", _ent.IsWithDetail);


            DT1.Rows.Add("Accounting.AccountDetermination", "set", "Field1", _ent.Field1);
            DT1.Rows.Add("Accounting.AccountDetermination", "set", "Field2", _ent.Field2);
            DT1.Rows.Add("Accounting.AccountDetermination", "set", "Field3", _ent.Field3);
            DT1.Rows.Add("Accounting.AccountDetermination", "set", "Field4", _ent.Field4);
            DT1.Rows.Add("Accounting.AccountDetermination", "set", "Field5", _ent.Field5);
            DT1.Rows.Add("Accounting.AccountDetermination", "set", "Field6", _ent.Field6);
            DT1.Rows.Add("Accounting.AccountDetermination", "set", "Field7", _ent.Field7);
            DT1.Rows.Add("Accounting.AccountDetermination", "set", "Field8", _ent.Field8);
            DT1.Rows.Add("Accounting.AccountDetermination", "set", "Field9", _ent.Field9);

         Gears.UpdateData(DT1, _ent.Connection);
         Functions.AuditTrail("ACTADT", TransactionType, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "UPDATE", _ent.Connection);
        }
        public void DeleteData(AccountDetermination _ent)
        {
            TransType = _ent.TransType;
            Conn = _ent.Connection;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Accounting.AccountDetermination", "cond", "TransType", _ent.TransType);
            Gears.DeleteData(DT1, _ent.Connection);

            Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
            DT2.Rows.Add("Accounting.AccountDetermination", "cond", "TransType", _ent.TransType);
            Gears.DeleteData(DT2, _ent.Connection);

            Functions.AuditTrail("ACTADT", TransactionType, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "DELETE", _ent.Connection);
        }
    }
}
