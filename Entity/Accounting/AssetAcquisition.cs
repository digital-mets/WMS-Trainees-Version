using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GearsLibrary;
using System.Data;

namespace Entity
{
  public  class AssetAcquisition
    {

        private static string Docnum;

        private static string PropertyNum;

        private static string DateAcquired;

        private static string addedby;

        private static string whcode;

        private static string TotalQuantity;

        private static string Conn;//ADD CONN
        public virtual string Connection { get; set; }//ADD CONN
        public virtual string DocNumber { get; set; }
        public virtual string DocDate { get; set; }
        public virtual string WarehouseCode { get; set; }
        public virtual decimal TotalQty { get; set; }


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
        public virtual string CancelledBy { get; set; }
        public virtual string CancelledDate { get; set; }
        public virtual IList<AssetAcquisitionDetail> Detail { get; set; }


        public class AssetAcquisitionDetail
        {
            public virtual AssetAcquisition Parent { get; set; }
            public virtual string DocNumber { get; set; }

            public virtual string LineNumber { get; set; }

            public virtual string PropertyNumber { get; set; }
            public virtual string WarehouseCode { get; set; }
            public virtual string Unit { get; set; }
            public virtual string ItemCode { get; set; }
            public virtual string FullDesc { get; set; }
            public virtual string ColorCode { get; set; }
            public virtual string ClassCode { get; set; }
            public virtual string SizeCode { get; set; }
            public virtual decimal ReceivedQty { get; set; }
            public virtual decimal UnitCost { get; set; }

            //public virtual string Field1 { get; set; }
            //public virtual string Field2 { get; set; }
            //public virtual string Field3 { get; set; }
            //public virtual string Field4 { get; set; }
            //public virtual string Field5 { get; set; }
            //public virtual string Field6 { get; set; }
            //public virtual string Field7 { get; set; }
            //public virtual string Field8 { get; set; }
            //public virtual string Field9 { get; set; }


            public DataTable getdetail(string DocNumber, string Conn)
            {

                DataTable a;
                try
                {
                    a = Gears.RetriveData2("select *,FullDesc from Procurement.ReceivingReportDetailPO RRDPO LEFT JOIN Masterfile.Item I ON I.ItemCode = RRDPO.ItemCode where DocNumber='" + DocNumber + "' order by LineNumber", Conn);
                   
                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }
            public void AddAssetAcquisitionDetail(AssetAcquisitionDetail AssetAcquisitionDetail)
            {
                string NewProperty = "";
                //int MonthsToStartDep = 0;

                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                //DataTable setProperty = Gears.RetriveData2("exec [sp_Generate_PropertyNumber] '" + AssetAcquisitionDetail.ItemCode + "' ");
                DataTable setProperty = Gears.RetriveData2("exec sp_Generate_PropertyNumber '" + AssetAcquisitionDetail.ItemCode + "'", Conn);
                //   + " declare @newLine varchar(7) "
                //   + "	SELECT TOP 1 @Line = CONVERT(int,SUBSTRING(PropertyNumber,LEN(PropertyNumber)-6,7)) FROM Accounting.AssetInv "
                //   + "	WHERE ItemCode = '" + AssetAcquisitionDetail.ItemCode + "' "
                //   + "	GROUP BY PropertyNumber ORDER BY PropertyNumber DESC "
                //   + "	SELECT @newLine = RIGHT('0000000'+CAST((@Line + 1) AS VARCHAR(7)),7) "	
                //   + "	SELECT '" + AssetAcquisitionDetail.ItemCode + "' +'-'+ @newLine AS NewProperty", Conn);

                
                NewProperty = Convert.IsDBNull(setProperty.Rows[0]["NewProperty"]) ? AssetAcquisitionDetail.ItemCode + "-0000000001" : setProperty.Rows[0]["NewProperty"].ToString();


                //DataTable getMonth = Gears.RetriveData2("SELECT  FROM Masterfile.ItemCategory WHERE ItemCategoryCode IN (SELECT DISTINCT ItemCategoryCode FROM Masterfile.Item WHERE ItemCode =  '" + AssetAcquisitionDetail.ItemCode + "')", Conn);

                DataTable getData = Gears.RetriveData2("select ISNULL(AssetLife,0) AS AssetLife, DepreciationGLCode, GLSubsiCode, AccumulatedGLCode, GainLossAccount, DepreciationMethod, ISNULL(IC.MonthsToStartDep,0) AS MonthsToStartDep from Masterfile.Item I Left Join Masterfile.ItemCategory IC ON I.ItemCategoryCode = IC.ItemCategoryCode WHERE ItemCode = '" + AssetAcquisitionDetail.ItemCode + "'", Conn);
                    //MonthsToStartDep = Convert.ToInt16(getData.Rows[0]["MonthsToStartDep"].ToString());
                    DateTime docdate = Convert.ToDateTime(DateAcquired);
                    DateTime defaulttarget = docdate.AddMonths(Convert.ToInt16(getData.Rows[0]["MonthsToStartDep"].ToString()));

                
                DT1.Rows.Add("Accounting.AssetInv", "0", "ItemCode", AssetAcquisitionDetail.ItemCode);
                DT1.Rows.Add("Accounting.AssetInv", "0", "Description", AssetAcquisitionDetail.FullDesc);
                DT1.Rows.Add("Accounting.AssetInv", "0", "ColorCode", AssetAcquisitionDetail.ColorCode);
                DT1.Rows.Add("Accounting.AssetInv", "0", "ClassCode", AssetAcquisitionDetail.ClassCode);
                DT1.Rows.Add("Accounting.AssetInv", "0", "SizeCode", AssetAcquisitionDetail.SizeCode);
                DT1.Rows.Add("Accounting.AssetInv", "0", "Qty ", AssetAcquisitionDetail.ReceivedQty);
                DT1.Rows.Add("Accounting.AssetInv", "0", "UnitCost", AssetAcquisitionDetail.UnitCost);
                DT1.Rows.Add("Accounting.AssetInv", "0", "TransType", "ACTAQT");

                DT1.Rows.Add("Accounting.AssetInv", "0", "WarehouseCode", whcode);
                DT1.Rows.Add("Accounting.AssetInv", "0", "Unit", AssetAcquisitionDetail.Unit);

                ////DT1.Rows.Add("Accounting.AssetInv", "0", "Field1", AssetAcquisitionDetail.Field1);
                ////DT1.Rows.Add("Accounting.AssetInv", "0", "Field2", AssetAcquisitionDetail.Field2);
                ////DT1.Rows.Add("Accounting.AssetInv", "0", "Field3", AssetAcquisitionDetail.Field3);
                ////DT1.Rows.Add("Accounting.AssetInv", "0", "Field4", AssetAcquisitionDetail.Field4);
                ////DT1.Rows.Add("Accounting.AssetInv", "0", "Field5", AssetAcquisitionDetail.Field5);
                ////DT1.Rows.Add("Accounting.AssetInv", "0", "Field6", AssetAcquisitionDetail.Field6);
                ////DT1.Rows.Add("Accounting.AssetInv", "0", "Field7", AssetAcquisitionDetail.Field7);
                ////DT1.Rows.Add("Accounting.AssetInv", "0", "Field8", AssetAcquisitionDetail.Field8);
                ////DT1.Rows.Add("Accounting.AssetInv", "0", "Field9", AssetAcquisitionDetail.Field9);

                DT1.Rows.Add("Accounting.AssetInv", "0", "DocNumber", Docnum);
                DT1.Rows.Add("Accounting.AssetInv", "0", "DateAcquired", DateAcquired);
                DT1.Rows.Add("Accounting.AssetInv", "0", "StartOfDepreciation", defaulttarget.ToShortDateString());
                DT1.Rows.Add("Accounting.AssetInv", "0", "PropertyNumber", NewProperty);

                //Additional Default Informations
                DT1.Rows.Add("Accounting.AssetInv", "0", "Life", getData.Rows[0]["AssetLife"].ToString());
                DT1.Rows.Add("Accounting.AssetInv", "0", "DepreciationAccountCode", getData.Rows[0]["DepreciationGLCode"].ToString());
                DT1.Rows.Add("Accounting.AssetInv", "0", "DepreciationSubsiCode", getData.Rows[0]["GLSubsiCode"].ToString());


                DT1.Rows.Add("Accounting.AssetInv", "0", "AccumulatedAccountCode", getData.Rows[0]["AccumulatedGLCode"].ToString());
                DT1.Rows.Add("Accounting.AssetInv", "0", "AccumulatedSubsiCode", getData.Rows[0]["GLSubsiCode"].ToString());

                DT1.Rows.Add("Accounting.AssetInv", "0", "GainLossAccount", getData.Rows[0]["GainLossAccount"].ToString());
                DT1.Rows.Add("Accounting.AssetInv", "0", "DepreciationMethod", getData.Rows[0]["DepreciationMethod"].ToString());

                //KMM
                DT1.Rows.Add("Accounting.AssetInv", "0", "AcquisitionCost", AssetAcquisitionDetail.UnitCost * AssetAcquisitionDetail.ReceivedQty);
                DT1.Rows.Add("Accounting.AssetInv", "0", "BookValue", AssetAcquisitionDetail.UnitCost * AssetAcquisitionDetail.ReceivedQty);
                DT1.Rows.Add("Accounting.AssetInv", "0", "MonthlyDepreciation", AssetAcquisitionDetail.UnitCost * AssetAcquisitionDetail.ReceivedQty / Convert.ToDecimal(getData.Rows[0]["AssetLife"].ToString()));
                //

                DT1.Rows.Add("Accounting.AssetInv", "0", "AddedBy", addedby);
                DT1.Rows.Add("Accounting.AssetInv", "0", "AddedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));


                
           
                DT1.Rows.Add("Accounting.AssetInv", "0", "Status", "F");
                DT1.Rows.Add("Accounting.AssetInv", "0", "IsWithDetail", "False");

                Gears.CreateData(DT1, Conn);
            }
            public void UpdateAssetAcquisitionDetail(AssetAcquisitionDetail AssetAcquisitionDetail)
            {
                int linenum = 0;

                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

                DT1.Rows.Add("Accounting.AssetInv", "cond", "DocNumber", Docnum);
                DT1.Rows.Add("Accounting.AssetInv", "cond", "LineNumber", AssetAcquisitionDetail.LineNumber);
                DT1.Rows.Add("Accounting.AssetInv", "cond", "PropertyNumber", PropertyNumber);
                DT1.Rows.Add("Accounting.AssetInv", "set", "ItemCode", AssetAcquisitionDetail.ItemCode);
                DT1.Rows.Add("Accounting.AssetInv", "set", "ColorCode", AssetAcquisitionDetail.ColorCode);
                DT1.Rows.Add("Accounting.AssetInv", "set", "ClassCode", AssetAcquisitionDetail.ClassCode);
                DT1.Rows.Add("Accounting.AssetInv", "set", "SizeCode", AssetAcquisitionDetail.SizeCode);
                DT1.Rows.Add("Accounting.AssetInv", "set", "ReceivedQty ", AssetAcquisitionDetail.ReceivedQty);
                DT1.Rows.Add("Accounting.AssetInv", "set", "UnitCost", AssetAcquisitionDetail.UnitCost);
                Gears.UpdateData(DT1, Conn);
            }
            public void DeleteAssetAcquisitionDetail(AssetAcquisitionDetail AssetAcquisitionDetail)
            {

                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();


                string NewProperty = "";

                DataTable setProperty = Gears.RetriveData2("declare @Line INT "
               + " declare @newLine varchar(5) "
               + "	SELECT TOP 1 @Line = CONVERT(int,SUBSTRING(PropertyNumber,LEN(PropertyNumber)-4,6)) FROM Accounting.AssetInv "
               + "	WHERE ItemCode = '" + AssetAcquisitionDetail.ItemCode + "' "
               + "	GROUP BY PropertyNumber ORDER BY PropertyNumber DESC "
               + "	SELECT @newLine = RIGHT('00000'+CAST((@Line + 1) AS VARCHAR(5)),5) "
               + "	SELECT '" + AssetAcquisitionDetail.ItemCode + "' +'-'+ @newLine AS NewProperty", Conn);


                NewProperty = Convert.IsDBNull(setProperty.Rows[0]["NewProperty"]) ? AssetAcquisitionDetail.ItemCode + "-00001" : setProperty.Rows[0]["NewProperty"].ToString();


                DT1.Rows.Add("Accounting.AssetInv", "cond", "LineNumber", PropertyNum);
                DT1.Rows.Add("Accounting.AssetInv", "cond", "DocNumber", Docnum);


                Gears.DeleteData(DT1, Conn);
                //DT3.Rows.Add("WMS.CountSheetSubsi", "cond", "DocNumber", PICKLISTDetail.DocNumber);
                //DT3.Rows.Add("WMS.CountSheetSubsi", "cond", "TransLine", PICKLISTDetail.LineNumber);
                //Gears.DeleteData(DT3);

                DataTable count = Gears.RetriveData2("select * from Procurement.ReceivingReportDetailPO where docnumber = '" + Docnum + "'", Conn);

                if (count.Rows.Count < 1)
                {
                    DT2.Rows.Add("Procurement.ReceivingReport", "cond", "DocNumber", Docnum);
                    DT2.Rows.Add("Procurement.ReceivingReport", "set", "IsWithDetail", "False");
                    Gears.UpdateData(DT2, Conn);
                }
            }


        }
        public DataTable getdata(string DocNumber, string Conn)
        {
            DataTable a;

            //if (DocNumber != null)
            //{
            a = Gears.RetriveData2("select * from Procurement.ReceivingReport where DocNumber = '" + DocNumber + "'", Conn);
            foreach (DataRow dtRow in a.Rows)
            {
                DocNumber = dtRow["DocNumber"].ToString();
                DocDate = dtRow["DocDate"].ToString();
                WarehouseCode = dtRow["WarehouseCode"].ToString();

                TotalQty = Convert.ToDecimal(Convert.IsDBNull(dtRow["TotalQuantity"]) ? 0 : dtRow["TotalQuantity"]);
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
                CancelledBy = dtRow["CancelledBy"].ToString();
                CancelledDate = dtRow["CancelledDate"].ToString();
                
                   

            }


            return a;
        }
        public void InsertData(AssetAcquisition _ent)
        {
            Docnum = _ent.DocNumber;
            Conn = _ent.Connection;
            DateAcquired = _ent.DocDate;
            addedby = _ent.AddedBy;
            whcode = _ent.WarehouseCode;
        }

        public void UpdateData(AssetAcquisition _ent)
        {
            Docnum = _ent.DocNumber;
            Conn = _ent.Connection;
            DateAcquired = _ent.DocNumber;
            addedby = _ent.AddedBy;
            whcode = _ent.WarehouseCode;
        }

        public void DeleteData(AssetAcquisition _ent)
        {
            Docnum = _ent.DocNumber;
            Conn = _ent.Connection;
            DateAcquired = _ent.DocNumber;
            addedby = _ent.AddedBy;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Accounting.AssetInv", "cond", "DocNumber", _ent.DocNumber);
            Gears.DeleteData(DT1, _ent.Connection);
            Functions.AuditTrail("ACTAQT", Docnum, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "DELETE", _ent.Connection);
        }
    }
}
