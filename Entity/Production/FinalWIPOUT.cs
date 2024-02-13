using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GearsLibrary;
using System.Data;

namespace Entity
{
    public class FinalWIPOUT
    {
        private static string Docnum; 
        private static string ProdCode;
        private static string ProdColor;
        private static string Warehousee;
        private static string Docdatee;
        private static string By; 
        private static string Conn;//ADD CONN
        public virtual string Connection { get; set; }//ADD CONN
        public virtual string DocNumber { get; set; }
        public virtual string DocDate { get; set; }
     
        public virtual string Type { get; set; }
        public virtual string AdjustmentClass { get; set; }
        public virtual string JobOrder { get; set; }
        public virtual string Step { get; set; }
   
        public virtual string WorkCenter { get; set; }


        public virtual string ProductCode { get; set; }

        public virtual string ProductColor { get; set; }

        public virtual string DRDocnumber { get; set; }
        public virtual string RRDocnumber { get; set; }
        public virtual string Warehouse { get; set; }
        public virtual string Remarks { get; set; }
        public virtual string OverheadCode { get; set; }
        public virtual decimal TotalQuantity { get; set; }
        public virtual decimal WorkOrderPrice { get; set; }
        public virtual decimal OriginalWorkOrderPrice { get; set; }
        public virtual string VatCode { get; set; }
        public virtual string Currency { get; set; }
        public virtual decimal ExchangeRate { get; set; }
        public virtual decimal PesoAmount { get; set; }
        public virtual decimal ForeignAmount { get; set; }
        public virtual decimal GrossVatableAmount { get; set; }
        public virtual decimal NonVatableAmount { get; set; }
        public virtual decimal VatAmount { get; set; }
        public virtual decimal WTaxAmount { get; set; }
        public virtual decimal VatRate { get; set; }
        public virtual decimal AtcRate { get; set; }
        public virtual bool ClassA { get; set; }
        public virtual bool AutoCharge { get; set; }
        public virtual string Status { get; set; }
        public virtual string Disposition { get; set; }
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


        public virtual IList<WOClassBreakDown> Detail { get; set; }


        public class WOClassBreakDown
        {

            public virtual FinalWIPOUT Parent { get; set; }
            public virtual string DocNumber { get; set; }
            public virtual string LineNumber { get; set; }

            public virtual string JOClass { get; set; }
            public virtual string ClassCode { get; set; }
            public virtual string SizeCodes { get; set; }
            public virtual decimal Quantity { get; set; }
            public virtual decimal BulkQuantity { get; set; }


            public virtual string Field1 { get; set; }
            public virtual string Field2 { get; set; }
            public virtual string Field3 { get; set; }
            public virtual string Field4 { get; set; }
            public virtual string Field5 { get; set; }
            public virtual string Field6 { get; set; }
            public virtual string Field7 { get; set; }
            public virtual string Field8 { get; set; }
            public virtual string Field9 { get; set; }

            public virtual DateTime ExpDate { get; set; }
            public virtual DateTime MfgDate { get; set; }
            public virtual string BatchNo { get; set; }
            public virtual string LotNo { get; set; }
            public DataTable getdetail(string DocNumber, string Conn)
            {

                DataTable a;
                try
                {
                    a = Gears.RetriveData2("select * from Production.FinalWOClassBreakdown where DocNumber='" + DocNumber + "' order by LineNumber", Conn);
                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }
            public void AddWOClassBreakDown(WOClassBreakDown WOClassBreakDown)
            {

                int linenum = 0;


                DataTable count = Gears.RetriveData2("select max(convert(int,LineNumber)) as LineNumber from Production.FinalWOClassBreakdown where docnumber = '" + Docnum + "'", Conn);

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

                DT1.Rows.Add("Production.FinalWOClassBreakdown", "0", "DocNumber", Docnum);
                DT1.Rows.Add("Production.FinalWOClassBreakdown", "0", "LineNumber", strLine);
                DT1.Rows.Add("Production.FinalWOClassBreakdown", "0", "JOClass", WOClassBreakDown.JOClass);
                DT1.Rows.Add("Production.FinalWOClassBreakdown", "0", "ClassCode", WOClassBreakDown.ClassCode);
                DT1.Rows.Add("Production.FinalWOClassBreakdown", "0", "Quantity", WOClassBreakDown.Quantity);
                DT1.Rows.Add("Production.FinalWOClassBreakdown", "0", "BulkQuantity", WOClassBreakDown.BulkQuantity);
                DT1.Rows.Add("Production.FinalWOClassBreakdown", "0", "SizeCodes", WOClassBreakDown.SizeCodes);

                DT1.Rows.Add("Production.FinalWOClassBreakdown", "0", "Field1", WOClassBreakDown.Field1);
                DT1.Rows.Add("Production.FinalWOClassBreakdown", "0", "Field2", WOClassBreakDown.Field2);
                DT1.Rows.Add("Production.FinalWOClassBreakdown", "0", "Field3", WOClassBreakDown.Field3);
                DT1.Rows.Add("Production.FinalWOClassBreakdown", "0", "Field4", WOClassBreakDown.Field4);
                DT1.Rows.Add("Production.FinalWOClassBreakdown", "0", "Field5", WOClassBreakDown.Field5);
                DT1.Rows.Add("Production.FinalWOClassBreakdown", "0", "Field6", WOClassBreakDown.Field6);
                DT1.Rows.Add("Production.FinalWOClassBreakdown", "0", "Field7", WOClassBreakDown.Field7);
                DT1.Rows.Add("Production.FinalWOClassBreakdown", "0", "Field8", WOClassBreakDown.Field8);
                DT1.Rows.Add("Production.FinalWOClassBreakdown", "0", "Field9", WOClassBreakDown.Field9);



                //DT2.Rows.Add("Procurement.FinalWIPOUT", "cond", "DocNumber", Docnum);
                //DT2.Rows.Add("Procurement.FinalWIPOUT", "set", "IsWithDetail", "True");


                if (WOClassBreakDown.ExpDate.ToString() != "1/1/0001 12:00:00 AM")
                {
                    DT1.Rows.Add("Production.FinalWOClassBreakdown", "0", "ExpDate", WOClassBreakDown.ExpDate);
                }
                else
                {
                    DT1.Rows.Add("Production.FinalWOClassBreakdown", "0", "ExpDate", null);
                }
                if (WOClassBreakDown.MfgDate.ToString() != "1/1/0001 12:00:00 AM")
                {
                    DT1.Rows.Add("Production.FinalWOClassBreakdown", "0", "MfgDate", WOClassBreakDown.MfgDate);
                }
                else
                {
                    DT1.Rows.Add("Production.FinalWOClassBreakdown", "0", "MfgDate", null);
                }
                DT1.Rows.Add("Production.FinalWOClassBreakdown", "0", "BatchNo", WOClassBreakDown.BatchNo);
                DT1.Rows.Add("Production.FinalWOClassBreakdown", "0", "LotNo", WOClassBreakDown.LotNo);



                Gears.CreateData(DT1, Conn);

                Gears.RetriveData2("UPDATE Production.FinalWIPOut set TotalQuantity = (SELECT SUM(Qty) FROM Production.FinalWOClassBreakdown  where DocNumber='" + Docnum + "') where DocNumber ='" + Docnum + "'", Conn);
                Gears.RetriveData2("DELETE FROM Production.FinalWOSizeBreakdown where Docnumber='" + Docnum + "'", Conn);
                //Gears.UpdateData(DT2);


                #region Countsheet TLAV

                bool isbybulk = false;
                Gears.CRUDdatatable DT3 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT4 = new Gears.CRUDdatatable();

                DataTable CS = Gears.RetriveData2("SELECT ISNULL(IsByBulk,0) AS IsByBulk, ISNULL(StandardQty,0) AS StandardQty FROM Masterfile.Item WHERE ItemCode = '" + ProdCode + "'", Conn);
                foreach (DataRow dt in CS.Rows)
                {
                    isbybulk = Convert.ToBoolean(dt["IsByBulk"]);
                }
                if (isbybulk == true)
                {
                    for (int i = 1; i <= WOClassBreakDown.BulkQuantity; i++)
                    {
                        string strLine2 = i.ToString().PadLeft(5, '0');
                        DT4.Rows.Add("WMS.CountSheetSetup", "0", "TransType", "PRDFWT");
                        DT4.Rows.Add("WMS.CountSheetSetup", "0", "TransDoc", Docnum);
                        DT4.Rows.Add("WMS.CountSheetSetup", "0", "TransLine", strLine);
                        DT4.Rows.Add("WMS.CountSheetSetup", "0", "LineNumber", strLine2); // RollId
                        DT4.Rows.Add("WMS.CountSheetSetup", "0", "ItemCode", ProdCode);
                        DT4.Rows.Add("WMS.CountSheetSetup", "0", "ColorCode", ProdColor);
                        DT4.Rows.Add("WMS.CountSheetSetup", "0", "ClassCode", WOClassBreakDown.ClassCode);
                        DT4.Rows.Add("WMS.CountSheetSetup", "0", "SizeCode", WOClassBreakDown.SizeCodes);
                        DT4.Rows.Add("WMS.CountSheetSetup", "0", "AddedBy", By);
                        DT4.Rows.Add("WMS.CountSheetSetup", "0", "AddedDate", DateTime.Now);
                        DT4.Rows.Add("WMS.CountSheetSetup", "0", "RRDate", Docdatee);
                        DT4.Rows.Add("WMS.CountSheetSetup", "0", "WarehouseCode", Warehousee);
                        DT4.Rows.Add("WMS.CountSheetSetup", "0", "OriginalBulkQty", 1);
						DT4.Rows.Add("WMS.CountSheetSetup", "0", "OriginalBaseQty", WOClassBreakDown.Quantity);
                        DT4.Rows.Add("WMS.CountSheetSetup", "0", "ExpirationDate", WOClassBreakDown.ExpDate);
                        DT4.Rows.Add("WMS.CountSheetSetup", "0", "MfgDate", WOClassBreakDown.MfgDate);
                        DT4.Rows.Add("WMS.CountSheetSetup", "0", "BatchNumber", WOClassBreakDown.BatchNo);
                        DT4.Rows.Add("WMS.CountSheetSetup", "0", "LotNo", WOClassBreakDown.LotNo);
                        Gears.CreateData(DT4, Conn);
                        DT4.Rows.Clear();
                    }
                }
                #endregion
            }
            public void UpdateWOClassBreakDown(WOClassBreakDown WOClassBreakDown)
            {
                #region Countsheet
                bool isbybulk = false;

                DataTable dtable = Gears.RetriveData2("SELECT BulkQuantity FROM Production.FinalWOClassBreakdown WHERE DocNumber = '" + Docnum + "' AND LineNumber = '" + WOClassBreakDown.LineNumber + "'", Conn);
                foreach (DataRow dtrow in dtable.Rows)
                {
                    if (Convert.ToDecimal(dtrow["BulkQuantity"].ToString()) != WOClassBreakDown.BulkQuantity)
                    {
                        Gears.CRUDdatatable DT3 = new Gears.CRUDdatatable();
                        DT3.Rows.Add("WMS.CountSheetSetUp", "cond", "TransDoc", Docnum);
                        DT3.Rows.Add("WMS.CountSheetSetUp", "cond", "TransLine", WOClassBreakDown.LineNumber);
                        Gears.DeleteData(DT3, Conn);

                        Gears.CRUDdatatable DT4 = new Gears.CRUDdatatable();
                        DataTable CS = Gears.RetriveData2("SELECT ISNULL(IsByBulk,0) AS IsByBulk, ISNULL(StandardQty,0) AS StandardQty FROM Masterfile.Item WHERE ItemCode = '" + ProdCode + "'", Conn);
                        foreach (DataRow dt in CS.Rows)
                        {
                            isbybulk = Convert.ToBoolean(dt["IsByBulk"]);
                        }
                        if (isbybulk == true)
                        {
                            for (int i = 1; i <= WOClassBreakDown.BulkQuantity; i++)
                            {
                                string strLine2 = i.ToString().PadLeft(5, '0');

                                DT4.Rows.Add("WMS.CountSheetSetUp", "0", "TransType", "PRDFWT");
                                DT4.Rows.Add("WMS.CountSheetSetUp", "0", "TransDoc", Docnum);
                                DT4.Rows.Add("WMS.CountSheetSetUp", "0", "TransLine", WOClassBreakDown.LineNumber);
                                DT4.Rows.Add("WMS.CountSheetSetUp", "0", "LineNumber", strLine2);
                                DT4.Rows.Add("WMS.CountSheetSetUp", "0", "ItemCode", ProdCode);
                                DT4.Rows.Add("WMS.CountSheetSetUp", "0", "ColorCode", ProdColor);
                                DT4.Rows.Add("WMS.CountSheetSetUp", "0", "ClassCode", WOClassBreakDown.ClassCode);
                                DT4.Rows.Add("WMS.CountSheetSetUp", "0", "SizeCode", WOClassBreakDown.SizeCodes);
                                DT4.Rows.Add("WMS.CountSheetSetup", "0", "AddedBy", By);
                                DT4.Rows.Add("WMS.CountSheetSetup", "0", "AddedDate", DateTime.Now);
                                DT4.Rows.Add("WMS.CountSheetSetup", "0", "RRDate", Docdatee);
                                DT4.Rows.Add("WMS.CountSheetSetup", "0", "WarehouseCode", Warehousee);
                                DT4.Rows.Add("WMS.CountSheetSetup", "0", "OriginalBulkQty", 1);
								DT4.Rows.Add("WMS.CountSheetSetup", "0", "OriginalBaseQty", WOClassBreakDown.Quantity);
                                DT4.Rows.Add("WMS.CountSheetSetup", "0", "ExpirationDate", WOClassBreakDown.ExpDate);
                                DT4.Rows.Add("WMS.CountSheetSetup", "0", "MfgDate", WOClassBreakDown.MfgDate);
                                DT4.Rows.Add("WMS.CountSheetSetup", "0", "BatchNumber", WOClassBreakDown.BatchNo);
                                DT4.Rows.Add("WMS.CountSheetSetup", "0", "LotNo", WOClassBreakDown.LotNo);
                                Gears.CreateData(DT4, Conn);
                                DT4.Rows.Clear();
                            }
                        }
                    }
                }
                #endregion

                int linenum = 0;

                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

                DT1.Rows.Add("Production.FinalWOClassBreakdown", "cond", "DocNumber", Docnum);
                DT1.Rows.Add("Production.FinalWOClassBreakdown", "cond", "LineNumber", WOClassBreakDown.LineNumber);
                DT1.Rows.Add("Production.FinalWOClassBreakdown", "set", "JOClass", WOClassBreakDown.JOClass);
                DT1.Rows.Add("Production.FinalWOClassBreakdown", "set", "ClassCode", WOClassBreakDown.ClassCode);
                DT1.Rows.Add("Production.FinalWOClassBreakdown", "set", "SizeCodes", WOClassBreakDown.SizeCodes);
                DT1.Rows.Add("Production.FinalWOClassBreakdown", "set", "Quantity", WOClassBreakDown.Quantity);
                DT1.Rows.Add("Production.FinalWOClassBreakdown", "set", "BulkQuantity", WOClassBreakDown.BulkQuantity);



                DT1.Rows.Add("Production.FinalWOClassBreakdown", "set", "Field1", WOClassBreakDown.Field1);
                DT1.Rows.Add("Production.FinalWOClassBreakdown", "set", "Field2", WOClassBreakDown.Field2);
                DT1.Rows.Add("Production.FinalWOClassBreakdown", "set", "Field3", WOClassBreakDown.Field3);
                DT1.Rows.Add("Production.FinalWOClassBreakdown", "set", "Field4", WOClassBreakDown.Field4);
                DT1.Rows.Add("Production.FinalWOClassBreakdown", "set", "Field5", WOClassBreakDown.Field5);
                DT1.Rows.Add("Production.FinalWOClassBreakdown", "set", "Field6", WOClassBreakDown.Field6);
                DT1.Rows.Add("Production.FinalWOClassBreakdown", "set", "Field7", WOClassBreakDown.Field7);
                DT1.Rows.Add("Production.FinalWOClassBreakdown", "set", "Field8", WOClassBreakDown.Field8);
                DT1.Rows.Add("Production.FinalWOClassBreakdown", "set", "Field9", WOClassBreakDown.Field9);

                if (WOClassBreakDown.ExpDate.ToString() != "1/1/0001 12:00:00 AM")
                {
                    DT1.Rows.Add("Production.FinalWOClassBreakdown", "set", "ExpDate", WOClassBreakDown.ExpDate);
                }
                else
                {
                    DT1.Rows.Add("Production.FinalWOClassBreakdown", "set", "ExpDate", null);
                }
                if (WOClassBreakDown.MfgDate.ToString() != "1/1/0001 12:00:00 AM")
                {
                    DT1.Rows.Add("Production.FinalWOClassBreakdown", "set", "MfgDate", WOClassBreakDown.MfgDate);
                }
                else
                {
                    DT1.Rows.Add("Production.FinalWOClassBreakdown", "set", "MfgDate", null);
                }
                DT1.Rows.Add("Production.FinalWOClassBreakdown", "set", "BatchNo", WOClassBreakDown.BatchNo);
                DT1.Rows.Add("Production.FinalWOClassBreakdown", "set", "LotNo", WOClassBreakDown.LotNo);

                Gears.UpdateData(DT1, Conn);


                Gears.RetriveData2("UPDATE Production.FinalWIPOut set TotalQuantity = (SELECT SUM(Qty) FROM Production.FinalWOClassBreakdown  where DocNumber='" + Docnum + "') where DocNumber ='" + Docnum + "'", Conn);
                Gears.RetriveData2("DELETE FROM Production.FinalWOSizeBreakdown where Docnumber='" + Docnum + "'", Conn);
          


            }
            public void DeleteWOClassBreakDown(WOClassBreakDown WOClassBreakDown)
            {

                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT3 = new Gears.CRUDdatatable();
                DT1.Rows.Add("Production.FinalWOClassBreakdown", "cond", "DocNumber", Docnum);
                DT1.Rows.Add("Production.FinalWOClassBreakdown", "cond", "LineNumber", WOClassBreakDown.LineNumber);


                Gears.DeleteData(DT1, Conn);


                Gears.RetriveData2("UPDATE Production.FinalWIPOut set TotalQuantity = (SELECT SUM(Qty) FROM Production.FinalWOClassBreakdown  where DocNumber='" + Docnum + "') where DocNumber ='" + Docnum + "'", Conn);
                Gears.RetriveData2("DELETE FROM Production.FinalWOSizeBreakdown where Docnumber='" + Docnum + "'", Conn);
                 
                DT3.Rows.Add("WMS.CountSheetSetUp", "cond", "TransDoc", Docnum);
                DT3.Rows.Add("WMS.CountSheetSetUp", "cond", "TransLine", WOClassBreakDown.LineNumber);
                DT3.Rows.Add("WMS.CountSheetSubsi", "cond", "TransType", "PRDFWT");
                Gears.DeleteData(DT3, Conn);

                //DataTable count = Gears.RetriveData2("select * from Production.FinalWOClassBreakdown where docnumber = '" + Docnum + "'");

                //if (count.Rows.Count < 1)
                //{
                //    DT2.Rows.Add("Procurement.FinalWIPOUT", "cond", "DocNumber", Docnum);
                //    DT2.Rows.Add("Procurement.FinalWIPOUT", "set", "IsWithDetail", "False");
                //    Gears.UpdateData(DT2);
                //}


            }


          


        }

        public class WOSizeBreakDown
        {

            public virtual FinalWIPOUT Parent { get; set; }
            public virtual string DocNumber { get; set; }
            public virtual string LineNumber { get; set; }

            public virtual string SizeCode { get; set; }
            public virtual string ClassCodes { get; set; }

            public virtual decimal Qty { get; set; }
            public virtual decimal BulkQty { get; set; }

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
            public virtual DateTime ExpDate { get; set; }
            public virtual DateTime MfgDate { get; set; }
            public virtual string BatchNo { get; set; }
            public virtual string LotNo { get; set; }
            public DataTable getdetail(string DocNumber, string Conn)
            {

                DataTable a;
                try
                {
                    a = Gears.RetriveData2("select * from Production.FinalWOSizeBreakdown  where DocNumber='" + DocNumber + "' order by LineNumber", Conn);
                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }
            public void AddWOSizeBreakDown(WOSizeBreakDown WOSizeBreakDown)
            {

                int linenum = 0;


                DataTable count = Gears.RetriveData2("select max(convert(int,LineNumber)) as LineNumber from Production.FinalWOSizeBreakdown  where docnumber = '" + Docnum + "'", Conn);

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

                DT1.Rows.Add("Production.FinalWOSizeBreakdown ", "0", "DocNumber", Docnum);
                DT1.Rows.Add("Production.FinalWOSizeBreakdown ", "0", "LineNumber", strLine);
                DT1.Rows.Add("Production.FinalWOSizeBreakdown ", "0", "SizeCode", WOSizeBreakDown.SizeCode);
                DT1.Rows.Add("Production.FinalWOSizeBreakdown ", "0", "ClassCodes", WOSizeBreakDown.ClassCodes);

                DT1.Rows.Add("Production.FinalWOSizeBreakdown ", "0", "Qty", WOSizeBreakDown.Qty);
                DT1.Rows.Add("Production.FinalWOSizeBreakdown ", "0", "BulkQty", WOSizeBreakDown.BulkQty);
                DT1.Rows.Add("Production.FinalWOSizeBreakdown ", "0", "JOBreakdown", WOSizeBreakDown.JOBreakdown);

                DT1.Rows.Add("Production.FinalWOSizeBreakdown ", "0", "Field1", WOSizeBreakDown.Field1);
                DT1.Rows.Add("Production.FinalWOSizeBreakdown ", "0", "Field2", WOSizeBreakDown.Field2);
                DT1.Rows.Add("Production.FinalWOSizeBreakdown ", "0", "Field3", WOSizeBreakDown.Field3);
                DT1.Rows.Add("Production.FinalWOSizeBreakdown ", "0", "Field4", WOSizeBreakDown.Field4);
                DT1.Rows.Add("Production.FinalWOSizeBreakdown ", "0", "Field5", WOSizeBreakDown.Field5);
                DT1.Rows.Add("Production.FinalWOSizeBreakdown ", "0", "Field6", WOSizeBreakDown.Field6);
                DT1.Rows.Add("Production.FinalWOSizeBreakdown ", "0", "Field7", WOSizeBreakDown.Field7);
                DT1.Rows.Add("Production.FinalWOSizeBreakdown ", "0", "Field8", WOSizeBreakDown.Field8);
                DT1.Rows.Add("Production.FinalWOSizeBreakdown ", "0", "Field9", WOSizeBreakDown.Field9);

                if (WOSizeBreakDown.ExpDate.ToString() != "1/1/0001 12:00:00 AM")
                {
                    DT1.Rows.Add("Production.FinalWOSizeBreakdown", "0", "ExpDate", WOSizeBreakDown.ExpDate);
                }
                else
                {
                    DT1.Rows.Add("Production.FinalWOSizeBreakdown", "0", "ExpDate", null);
                }
                if (WOSizeBreakDown.MfgDate.ToString() != "1/1/0001 12:00:00 AM")
                {
                    DT1.Rows.Add("Production.FinalWOSizeBreakdown", "0", "MfgDate", WOSizeBreakDown.MfgDate);
                }
                else
                {
                    DT1.Rows.Add("Production.FinalWOSizeBreakdown", "0", "MfgDate", null);
                }
                DT1.Rows.Add("Production.FinalWOSizeBreakdown", "0", "BatchNo", WOSizeBreakDown.BatchNo);
                DT1.Rows.Add("Production.FinalWOSizeBreakdown", "0", "LotNo", WOSizeBreakDown.LotNo);




                //DT2.Rows.Add("Production.InitialFinalWIPOUT", "cond", "DocNumber", Docnum);
                //DT2.Rows.Add("Production.InitialFinalWIPOUT", "set", "IsWithDetail", "True");

                Gears.CreateData(DT1, Conn);

                Gears.RetriveData2("UPDATE Production.FinalWIPOut set TotalQuantity = (SELECT SUM(Qty) FROM Production.FinalWOSizeBreakdown  where DocNumber='" + Docnum + "') where DocNumber ='" + Docnum + "'", Conn);
                Gears.RetriveData2("DELETE FROM Production.FinalWOClassBreakdown where Docnumber='" + Docnum + "'", Conn);


                //Gears.UpdateData(DT2);


                #region Countsheet TLAV

                bool isbybulk = false;
                Gears.CRUDdatatable DT3 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT4 = new Gears.CRUDdatatable();

                DataTable CS = Gears.RetriveData2("SELECT ISNULL(IsByBulk,0) AS IsByBulk, ISNULL(StandardQty,0) AS StandardQty FROM Masterfile.Item WHERE ItemCode = '" + ProdCode + "'", Conn);
                foreach (DataRow dt in CS.Rows)
                {
                    isbybulk = Convert.ToBoolean(dt["IsByBulk"]);
                }
                if (isbybulk == true)
                {
                    for (int i = 1; i <= WOSizeBreakDown.BulkQty; i++)
                    {
                        string strLine2 = i.ToString().PadLeft(5, '0');
                        DT4.Rows.Add("WMS.CountSheetSetup", "0", "TransType", "PRDFWT");
                        DT4.Rows.Add("WMS.CountSheetSetup", "0", "TransDoc", Docnum);
                        DT4.Rows.Add("WMS.CountSheetSetup", "0", "TransLine", strLine);
                        DT4.Rows.Add("WMS.CountSheetSetup", "0", "LineNumber", strLine2); // RollId
                        DT4.Rows.Add("WMS.CountSheetSetup", "0", "ItemCode", ProdCode);
                        DT4.Rows.Add("WMS.CountSheetSetup", "0", "ColorCode", ProdColor);
                        DT4.Rows.Add("WMS.CountSheetSetup", "0", "ClassCode", WOSizeBreakDown.ClassCodes);
                        DT4.Rows.Add("WMS.CountSheetSetup", "0", "SizeCode", WOSizeBreakDown.SizeCode);
                        DT4.Rows.Add("WMS.CountSheetSetup", "0", "AddedBy", By);
                        DT4.Rows.Add("WMS.CountSheetSetup", "0", "AddedDate", DateTime.Now);
                        DT4.Rows.Add("WMS.CountSheetSetup", "0", "RRDate", Docdatee);
                        DT4.Rows.Add("WMS.CountSheetSetup", "0", "WarehouseCode", Warehousee);
                        DT4.Rows.Add("WMS.CountSheetSetup", "0", "OriginalBulkQty", 1);
						DT4.Rows.Add("WMS.CountSheetSetup", "0", "OriginalBaseQty", WOSizeBreakDown.Qty);
                        DT4.Rows.Add("WMS.CountSheetSetup", "0", "ExpirationDate", WOSizeBreakDown.ExpDate);
                        DT4.Rows.Add("WMS.CountSheetSetup", "0", "MfgDate", WOSizeBreakDown.MfgDate);
                        DT4.Rows.Add("WMS.CountSheetSetup", "0", "BatchNumber", WOSizeBreakDown.BatchNo);
                        DT4.Rows.Add("WMS.CountSheetSetup", "0", "LotNo", WOSizeBreakDown.LotNo);
                        Gears.CreateData(DT4, Conn);
                        DT4.Rows.Clear();
                    }
                }
                #endregion
            }
            public void UpdateWOSizeBreakDown(WOSizeBreakDown WOSizeBreakDown)
            {
                #region Countsheet
                bool isbybulk = false;

                DataTable dtable = Gears.RetriveData2("SELECT ISNULL(BulkQty,0) AS BulkQty FROM Production.FinalWOSizeBreakdown WHERE DocNumber = '" + Docnum + "' AND LineNumber = '" + WOSizeBreakDown.LineNumber + "'", Conn);
                foreach (DataRow dtrow in dtable.Rows)
                {
                    if (Convert.ToDecimal(dtrow["BulkQty"].ToString()) != WOSizeBreakDown.BulkQty)
                    {
                        Gears.CRUDdatatable DT3 = new Gears.CRUDdatatable();
                        DT3.Rows.Add("WMS.CountSheetSetUp", "cond", "TransDoc", Docnum);
                        DT3.Rows.Add("WMS.CountSheetSetUp", "cond", "TransLine", WOSizeBreakDown.LineNumber);
                        Gears.DeleteData(DT3, Conn);

                        Gears.CRUDdatatable DT4 = new Gears.CRUDdatatable();
                        DataTable CS = Gears.RetriveData2("SELECT ISNULL(IsByBulk,0) AS IsByBulk, ISNULL(StandardQty,0) AS StandardQty FROM Masterfile.Item WHERE ItemCode = '" + ProdCode + "'", Conn);
                        foreach (DataRow dt in CS.Rows)
                        {
                            isbybulk = Convert.ToBoolean(dt["IsByBulk"]);
                        }
                        if (isbybulk == true)
                        {
                            for (int i = 1; i <= WOSizeBreakDown.BulkQty; i++)
                            {
                                string strLine2 = i.ToString().PadLeft(5, '0');

                                DT4.Rows.Add("WMS.CountSheetSetUp", "0", "TransType", "PRDFWT");
                                DT4.Rows.Add("WMS.CountSheetSetUp", "0", "TransDoc", Docnum);
                                DT4.Rows.Add("WMS.CountSheetSetUp", "0", "TransLine", WOSizeBreakDown.LineNumber);
                                DT4.Rows.Add("WMS.CountSheetSetUp", "0", "LineNumber", strLine2);
                                DT4.Rows.Add("WMS.CountSheetSetUp", "0", "ItemCode", ProdCode);
                                DT4.Rows.Add("WMS.CountSheetSetUp", "0", "ColorCode", ProdColor);
                                DT4.Rows.Add("WMS.CountSheetSetUp", "0", "ClassCode", WOSizeBreakDown.ClassCodes);
                                DT4.Rows.Add("WMS.CountSheetSetUp", "0", "SizeCode", WOSizeBreakDown.SizeCode);
                                DT4.Rows.Add("WMS.CountSheetSetup", "0", "AddedBy", By);
                                DT4.Rows.Add("WMS.CountSheetSetup", "0", "AddedDate", DateTime.Now);
                                DT4.Rows.Add("WMS.CountSheetSetup", "0", "RRDate", Docdatee);
                                DT4.Rows.Add("WMS.CountSheetSetup", "0", "WarehouseCode", Warehousee);
                                DT4.Rows.Add("WMS.CountSheetSetup", "0", "OriginalBulkQty", 1);
                                DT4.Rows.Add("WMS.CountSheetSetup", "0", "OriginalBaseQty", WOSizeBreakDown.Qty);
                                DT4.Rows.Add("WMS.CountSheetSetup", "0", "ExpirationDate", WOSizeBreakDown.ExpDate);
                                DT4.Rows.Add("WMS.CountSheetSetup", "0", "MfgDate", WOSizeBreakDown.MfgDate);
                                DT4.Rows.Add("WMS.CountSheetSetup", "0", "BatchNumber", WOSizeBreakDown.BatchNo);
                                DT4.Rows.Add("WMS.CountSheetSetup", "0", "LotNo", WOSizeBreakDown.LotNo);
                                Gears.CreateData(DT4, Conn);
                                DT4.Rows.Clear();
                            }
                        }
                    }
                }
                #endregion

                int linenum = 0;

                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

                DT1.Rows.Add("Production.FinalWOSizeBreakdown ", "cond", "DocNumber", Docnum);
                DT1.Rows.Add("Production.FinalWOSizeBreakdown ", "cond", "LineNumber", WOSizeBreakDown.LineNumber);
                DT1.Rows.Add("Production.FinalWOSizeBreakdown ", "set", "SizeCode", WOSizeBreakDown.SizeCode);
                DT1.Rows.Add("Production.FinalWOSizeBreakdown ", "set", "ClassCodes", WOSizeBreakDown.ClassCodes);

                DT1.Rows.Add("Production.FinalWOSizeBreakdown ", "set", "Qty", WOSizeBreakDown.Qty);
                DT1.Rows.Add("Production.FinalWOSizeBreakdown ", "set", "BulkQty", WOSizeBreakDown.BulkQty);
                DT1.Rows.Add("Production.FinalWOSizeBreakdown ", "set", "JOBreakdown", WOSizeBreakDown.JOBreakdown);
                 
                DT1.Rows.Add("Production.FinalWOSizeBreakdown ", "set", "Field1", WOSizeBreakDown.Field1);
                DT1.Rows.Add("Production.FinalWOSizeBreakdown ", "set", "Field2", WOSizeBreakDown.Field2);
                DT1.Rows.Add("Production.FinalWOSizeBreakdown ", "set", "Field3", WOSizeBreakDown.Field3);
                DT1.Rows.Add("Production.FinalWOSizeBreakdown ", "set", "Field4", WOSizeBreakDown.Field4);
                DT1.Rows.Add("Production.FinalWOSizeBreakdown ", "set", "Field5", WOSizeBreakDown.Field5);
                DT1.Rows.Add("Production.FinalWOSizeBreakdown ", "set", "Field6", WOSizeBreakDown.Field6);
                DT1.Rows.Add("Production.FinalWOSizeBreakdown ", "set", "Field7", WOSizeBreakDown.Field7);
                DT1.Rows.Add("Production.FinalWOSizeBreakdown ", "set", "Field8", WOSizeBreakDown.Field8);
                DT1.Rows.Add("Production.FinalWOSizeBreakdown ", "set", "Field9", WOSizeBreakDown.Field9);

                if (WOSizeBreakDown.ExpDate.ToString() != "1/1/0001 12:00:00 AM")
                {
                    DT1.Rows.Add("Production.FinalWOSizeBreakdown", "set", "ExpDate", WOSizeBreakDown.ExpDate);
                }
                else
                {
                    DT1.Rows.Add("Production.FinalWOSizeBreakdown", "set", "ExpDate", null);
                }
                if (WOSizeBreakDown.MfgDate.ToString() != "1/1/0001 12:00:00 AM")
                {
                    DT1.Rows.Add("Production.FinalWOSizeBreakdown", "set", "MfgDate", WOSizeBreakDown.MfgDate);
                }
                else
                {
                    DT1.Rows.Add("Production.FinalWOSizeBreakdown", "set", "MfgDate", null);
                }
                DT1.Rows.Add("Production.FinalWOSizeBreakdown", "set", "BatchNo", WOSizeBreakDown.BatchNo);
                DT1.Rows.Add("Production.FinalWOSizeBreakdown", "set", "LotNo", WOSizeBreakDown.LotNo);

                Gears.UpdateData(DT1, Conn);

                Gears.RetriveData2("UPDATE Production.FinalWIPOut set TotalQuantity = (SELECT SUM(Qty) FROM Production.FinalWOSizeBreakdown  where DocNumber='" + Docnum + "') where DocNumber ='" + Docnum + "'", Conn);
                Gears.RetriveData2("DELETE FROM Production.FinalWOClassBreakdown where Docnumber='" + Docnum + "'", Conn); 
            }
            public void DeleteWOSizeBreakDown(WOSizeBreakDown WOSizeBreakDown)
            {

                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT3 = new Gears.CRUDdatatable();
                DT1.Rows.Add("Production.FinalWOSizeBreakdown ", "cond", "DocNumber", Docnum);
                DT1.Rows.Add("Production.FinalWOSizeBreakdown ", "cond", "LineNumber", WOSizeBreakDown.LineNumber);
                 
                DT3.Rows.Add("WMS.CountSheetSetUp", "cond", "TransDoc", Docnum);
                DT3.Rows.Add("WMS.CountSheetSetUp", "cond", "TransLine", WOSizeBreakDown.LineNumber);
                DT3.Rows.Add("WMS.CountSheetSubsi", "cond", "TransType", "PRDFWT");
                Gears.DeleteData(DT3, Conn);

                Gears.DeleteData(DT1, Conn);

                Gears.RetriveData2("UPDATE Production.FinalWIPOut set TotalQuantity = (SELECT SUM(Qty) FROM Production.FinalWOSizeBreakdown  where DocNumber='" + Docnum + "') where DocNumber ='" + Docnum + "'", Conn);
                Gears.RetriveData2("DELETE FROM Production.FinalWOClassBreakdown where Docnumber='" + Docnum + "'", Conn);
                //DataTable count = Gears.RetriveData2("select * from Production.FinalWOSizeBreakdown  where docnumber = '" + Docnum + "'");

                //if (count.Rows.Count < 1)
                //{
                //    DT2.Rows.Add("Production.InitialFinalWIPOUT", "cond", "DocNumber", Docnum);
                //    DT2.Rows.Add("Production.InitialFinalWIPOUT", "set", "IsWithDetail", "False");
                //    Gears.UpdateData(DT2);
                //}


            }




        }


        public void OverrideData(FinalWIPOUT _ent)
        {
            Docnum = _ent.DocNumber;
            Conn = _ent.Connection;     //ADD CONN
            ProdCode = _ent.ProductCode;
            ProdColor = _ent.ProductColor;
            Warehousee = _ent.Warehouse;
            By = _ent.LastEditedBy;
            Docdatee = _ent.DocDate;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Production.FinalWIPOUT", "cond", "DocNumber", _ent.DocNumber);
            DT1.Rows.Add("Production.FinalWIPOUT", "set", "DRDocnumber", _ent.DRDocnumber);
            DT1.Rows.Add("Production.FinalWIPOUT", "set", "LastEditedBy", _ent.LastEditedBy);
            DT1.Rows.Add("Production.FinalWIPOUT", "set", "LastEditedDate", _ent.LastEditedDate);
            Gears.UpdateData(DT1, _ent.Connection);


            Functions.AuditTrail("PRDFWT", Docnum, _ent.LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "UPDATE", _ent.Connection);
        }

        public DataTable getdata(string DocNumber, string Conn)
        {
            DataTable a;

            //if (DocNumber != null)
            //{
            a = Gears.RetriveData2("select * from Production.FinalWIPOUT where DocNumber = '" + DocNumber + "'", Conn);
            foreach (DataRow dtRow in a.Rows)
            {
                DocNumber = dtRow["DocNumber"].ToString();
                DocDate = dtRow["Docdate"].ToString();
                Type = dtRow["Type"].ToString();
                AdjustmentClass = dtRow["AdjustmentClass"].ToString();
                JobOrder = dtRow["JobOrder"].ToString();
                Step = dtRow["Step"].ToString();
                WorkCenter = dtRow["WorkCenter"].ToString();
                ProductCode = dtRow["ProductCode"].ToString();
                ProductColor = dtRow["ProductColor"].ToString();
                DRDocnumber = dtRow["DRDocnumber"].ToString();
                RRDocnumber = dtRow["RRDocnumber"].ToString();
                Warehouse = dtRow["Warehouse"].ToString();
                Remarks = dtRow["Remarks"].ToString();
                OverheadCode = dtRow["OverheadCode"].ToString();
                Status = dtRow["Status"].ToString();
                Disposition = dtRow["Disposition"].ToString();
                TotalQuantity = Convert.ToDecimal(Convert.IsDBNull(dtRow["TotalQuantity"]) ? 0 : dtRow["TotalQuantity"]);

            
                WorkOrderPrice = Convert.ToDecimal(Convert.IsDBNull(dtRow["WorkOrderPrice"]) ? 0 : dtRow["WorkOrderPrice"]);
                OriginalWorkOrderPrice = Convert.ToDecimal(Convert.IsDBNull(dtRow["OriginalWorkOrderPrice"]) ? 0 : dtRow["OriginalWorkOrderPrice"]);
                VatCode = dtRow["VatCode"].ToString();


                ExchangeRate = Convert.ToDecimal(Convert.IsDBNull(dtRow["ExchangeRate"]) ? 0 : dtRow["ExchangeRate"]);
                PesoAmount = Convert.ToDecimal(Convert.IsDBNull(dtRow["PesoAmount"]) ? 0 : dtRow["PesoAmount"]);
                ForeignAmount = Convert.ToDecimal(Convert.IsDBNull(dtRow["ForeignAmount"]) ? 0 : dtRow["ForeignAmount"]);
                GrossVatableAmount = Convert.ToDecimal(Convert.IsDBNull(dtRow["GrossVatableAmount"]) ? 0 : dtRow["GrossVatableAmount"]);
                NonVatableAmount = Convert.ToDecimal(Convert.IsDBNull(dtRow["NonVatableAmount"]) ? 0 : dtRow["NonVatableAmount"]);
                VatAmount = Convert.ToDecimal(Convert.IsDBNull(dtRow["VatAmount"]) ? 0 : dtRow["VatAmount"]);
                WTaxAmount = Convert.ToDecimal(Convert.IsDBNull(dtRow["WTaxAmount"]) ? 0 : dtRow["WTaxAmount"]);
                ClassA = Convert.ToBoolean(Convert.IsDBNull(dtRow["ClassA"]) ? false : dtRow["ClassA"]);
                AutoCharge = Convert.ToBoolean(Convert.IsDBNull(dtRow["AutoCharge"]) ? false : dtRow["AutoCharge"]);
                VatRate = Convert.ToDecimal(Convert.IsDBNull(dtRow["VatRate"]) ? 0 : dtRow["VatRate"]);
                AtcRate = Convert.ToDecimal(Convert.IsDBNull(dtRow["AtcRate"]) ? 0 : dtRow["AtcRate"]);
             
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
        public void InsertData(FinalWIPOUT _ent)
        {
            Docnum = _ent.DocNumber;
            Conn = _ent.Connection;     //ADD CONN
            ProdCode = _ent.ProductCode;
            ProdColor = _ent.ProductColor;
            Warehousee = _ent.Warehouse;
            By = _ent.LastEditedBy;
            Docdatee = _ent.DocDate;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

          
       
            DT1.Rows.Add("Production.FinalWIPOUT", "0", "DocNumber", _ent.DocNumber);
            DT1.Rows.Add("Production.FinalWIPOUT", "0", "DocDate", _ent.DocDate);
            DT1.Rows.Add("Production.FinalWIPOUT", "0", "Type", _ent.Type);
            DT1.Rows.Add("Production.FinalWIPOUT", "0", "AdjustmentClass", _ent.AdjustmentClass);
         
            DT1.Rows.Add("Production.FinalWIPOUT", "0", "JobOrder", _ent.JobOrder);
            DT1.Rows.Add("Production.FinalWIPOUT", "0", "Step", _ent.Step);
            DT1.Rows.Add("Production.FinalWIPOUT", "0", "WorkCenter", _ent.WorkCenter);

            DT1.Rows.Add("Production.FinalWIPOUT", "0", "ProductCode", _ent.ProductCode);
            DT1.Rows.Add("Production.FinalWIPOUT", "0", "ProductColor", _ent.ProductColor);
            DT1.Rows.Add("Production.FinalWIPOUT", "0", "DRDocnumber", _ent.DRDocnumber);
            DT1.Rows.Add("Production.FinalWIPOUT", "0", "RRDocnumber  ", _ent.RRDocnumber);
            DT1.Rows.Add("Production.FinalWIPOUT", "0", "Warehouse  ", _ent.Warehouse);
            DT1.Rows.Add("Production.FinalWIPOUT", "0", "TotalQuantity  ", _ent.TotalQuantity);

            DT1.Rows.Add("Production.FinalWIPOUT", "0", "Remarks", _ent.Remarks);
            DT1.Rows.Add("Production.FinalWIPOUT", "0", "WorkOrderPrice ", _ent.WorkOrderPrice);
            DT1.Rows.Add("Production.FinalWIPOUT", "0", "OriginalWorkOrderPrice ", _ent.OriginalWorkOrderPrice);
            DT1.Rows.Add("Production.FinalWIPOUT", "0", "VatCode ", _ent.VatCode);
            DT1.Rows.Add("Production.FinalWIPOUT", "0", "Currency ", _ent.Currency);


            DT1.Rows.Add("Production.FinalWIPOUT", "0", "ExchangeRate", _ent.ExchangeRate);
            DT1.Rows.Add("Production.FinalWIPOUT", "0", "PesoAmount", _ent.PesoAmount);
            DT1.Rows.Add("Production.FinalWIPOUT", "0", "ForeignAmount", _ent.ForeignAmount);
            DT1.Rows.Add("Production.FinalWIPOUT", "0", "GrossVatableAmount", _ent.GrossVatableAmount);
            DT1.Rows.Add("Production.FinalWIPOUT", "0", "NonVatableAmount", _ent.NonVatableAmount);
            DT1.Rows.Add("Production.FinalWIPOUT", "0", "VatAmount", _ent.VatAmount);
            DT1.Rows.Add("Production.FinalWIPOUT", "0", "WTaxAmount", _ent.WTaxAmount);
            DT1.Rows.Add("Production.FinalWIPOUT", "0", "ClassA", _ent.ClassA);
            DT1.Rows.Add("Production.FinalWIPOUT", "0", "AutoCharge", _ent.AutoCharge);
            DT1.Rows.Add("Production.FinalWIPOUT", "0", "VatRate", _ent.VatRate);
            DT1.Rows.Add("Production.FinalWIPOUT", "0", "AtcRate", _ent.AtcRate);
            DT1.Rows.Add("Production.FinalWIPOUT", "0", "Status", _ent.Status);
            DT1.Rows.Add("Production.FinalWIPOUT", "0", "Disposition", _ent.Disposition);
            DT1.Rows.Add("Production.FinalWIPOUT", "0", "Field1", _ent.Field1);
            DT1.Rows.Add("Production.FinalWIPOUT", "0", "Field2", _ent.Field2);
            DT1.Rows.Add("Production.FinalWIPOUT", "0", "Field3", _ent.Field3);
            DT1.Rows.Add("Production.FinalWIPOUT", "0", "Field4", _ent.Field4);
            DT1.Rows.Add("Production.FinalWIPOUT", "0", "Field5", _ent.Field5);
            DT1.Rows.Add("Production.FinalWIPOUT", "0", "Field6", _ent.Field6);
            DT1.Rows.Add("Production.FinalWIPOUT", "0", "Field7", _ent.Field7);
            DT1.Rows.Add("Production.FinalWIPOUT", "0", "Field8", _ent.Field8);
            DT1.Rows.Add("Production.FinalWIPOUT", "0", "Field9", _ent.Field9);
            DT1.Rows.Add("Production.FinalWIPOUT", "0", "SubmittedBy", "");
            DT1.Rows.Add("Production.FinalWIPOUT", "0", "IsWithDetail", "False");


            Gears.CreateData(DT1, _ent.Connection);

        }

        public void UpdateData(FinalWIPOUT _ent)
        {
            Docnum = _ent.DocNumber;
            Conn = _ent.Connection;     //ADD CONN
            ProdCode = _ent.ProductCode;
            ProdColor = _ent.ProductColor;
            Warehousee = _ent.Warehouse;
            By = _ent.LastEditedBy;
            Docdatee = _ent.DocDate;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Production.FinalWIPOUT", "cond", "DocNumber", _ent.DocNumber);

            DT1.Rows.Add("Production.FinalWIPOUT", "set", "DocDate", _ent.DocDate);
            DT1.Rows.Add("Production.FinalWIPOUT", "set", "Type", _ent.Type);
            DT1.Rows.Add("Production.FinalWIPOUT", "set", "AdjustmentClass", _ent.AdjustmentClass);

            DT1.Rows.Add("Production.FinalWIPOUT", "set", "JobOrder", _ent.JobOrder);
            DT1.Rows.Add("Production.FinalWIPOUT", "set", "Step", _ent.Step);
            DT1.Rows.Add("Production.FinalWIPOUT", "set", "WorkCenter", _ent.WorkCenter);

            DT1.Rows.Add("Production.FinalWIPOUT", "set", "ProductCode", _ent.ProductCode);
            DT1.Rows.Add("Production.FinalWIPOUT", "set", "ProductColor", _ent.ProductColor);
            DT1.Rows.Add("Production.FinalWIPOUT", "set", "DRDocnumber", _ent.DRDocnumber);
            DT1.Rows.Add("Production.FinalWIPOUT", "set", "RRDocnumber  ", _ent.RRDocnumber);
            DT1.Rows.Add("Production.FinalWIPOUT", "set", "Warehouse  ", _ent.Warehouse);
            DT1.Rows.Add("Production.FinalWIPOUT", "set", "TotalQuantity  ", _ent.TotalQuantity);
            DT1.Rows.Add("Production.FinalWIPOUT", "set", "Status", _ent.Status);
            DT1.Rows.Add("Production.FinalWIPOUT", "set", "Disposition", _ent.Disposition);

            
            DT1.Rows.Add("Production.FinalWIPOUT", "set", "Remarks", _ent.Remarks);
            DT1.Rows.Add("Production.FinalWIPOUT", "set", "OverheadCode", _ent.OverheadCode);
        
            DT1.Rows.Add("Production.FinalWIPOUT", "set", "WorkOrderPrice ", _ent.WorkOrderPrice);
            DT1.Rows.Add("Production.FinalWIPOUT", "set", "OriginalWorkOrderPrice ", _ent.OriginalWorkOrderPrice);
            DT1.Rows.Add("Production.FinalWIPOUT", "set", "VatCode ", _ent.VatCode);
            DT1.Rows.Add("Production.FinalWIPOUT", "set", "Currency ", _ent.Currency);
            DT1.Rows.Add("Production.FinalWIPOUT", "set", "VatRate", _ent.VatRate);
            DT1.Rows.Add("Production.FinalWIPOUT", "set", "AtcRate", _ent.AtcRate);

            DT1.Rows.Add("Production.FinalWIPOUT", "set", "ExchangeRate", _ent.ExchangeRate);
            DT1.Rows.Add("Production.FinalWIPOUT", "set", "PesoAmount", _ent.PesoAmount);
            DT1.Rows.Add("Production.FinalWIPOUT", "set", "ForeignAmount", _ent.ForeignAmount);
            DT1.Rows.Add("Production.FinalWIPOUT", "set", "GrossVatableAmount", _ent.GrossVatableAmount);
            DT1.Rows.Add("Production.FinalWIPOUT", "set", "NonVatableAmount", _ent.NonVatableAmount);
            DT1.Rows.Add("Production.FinalWIPOUT", "set", "VatAmount", _ent.VatAmount);
            DT1.Rows.Add("Production.FinalWIPOUT", "set", "WTaxAmount", _ent.WTaxAmount);
            DT1.Rows.Add("Production.FinalWIPOUT", "set", "ClassA", _ent.ClassA);
            DT1.Rows.Add("Production.FinalWIPOUT", "set", "AutoCharge", _ent.AutoCharge);


            DT1.Rows.Add("Production.FinalWIPOUT", "set", "Field1", _ent.Field1);
            DT1.Rows.Add("Production.FinalWIPOUT", "set", "Field2", _ent.Field2);
            DT1.Rows.Add("Production.FinalWIPOUT", "set", "Field3", _ent.Field3);
            DT1.Rows.Add("Production.FinalWIPOUT", "set", "Field4", _ent.Field4);
            DT1.Rows.Add("Production.FinalWIPOUT", "set", "Field5", _ent.Field5);
            DT1.Rows.Add("Production.FinalWIPOUT", "set", "Field6", _ent.Field6);
            DT1.Rows.Add("Production.FinalWIPOUT", "set", "Field7", _ent.Field7);
            DT1.Rows.Add("Production.FinalWIPOUT", "set", "Field8", _ent.Field8);
            DT1.Rows.Add("Production.FinalWIPOUT", "set", "Field9", _ent.Field9);
            DT1.Rows.Add("Production.FinalWIPOUT", "set", "SubmittedBy", "");
            DT1.Rows.Add("Production.FinalWIPOUT", "set", "LastEditedBy", _ent.LastEditedBy);
            DT1.Rows.Add("Production.FinalWIPOUT", "set", "LastEditedDate", _ent.LastEditedDate);
            Gears.UpdateData(DT1, _ent.Connection);


            Functions.AuditTrail("PRDFWT", Docnum, _ent.LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "UPDATE", _ent.Connection);
        }

        public void DeleteData(FinalWIPOUT _ent)
        {
            Docnum = _ent.DocNumber;
            Conn = _ent.Connection;     //ADD CONN
            ProdCode = _ent.ProductCode;
            ProdColor = _ent.ProductColor;
            Warehousee = _ent.Warehouse;
            By = _ent.LastEditedBy;
            Docdatee = _ent.DocDate;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Production.FinalWIPOUT", "cond", "DocNumber", _ent.DocNumber);
            Gears.DeleteData(DT1, _ent.Connection);
            Functions.AuditTrail("PRDFWT", Docnum, _ent.LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "DELETE", _ent.Connection);
        }
        public class JournalEntry
        {
            public virtual FinalWIPOUT Parent { get; set; }
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
                    + " ProfitCenterCode AS ProfitCenter, CostCenterCode AS CostCenter, Convert(varchar,Convert(money,DebitAmount),1) AS Debit, Convert(varchar,Convert(money,CreditAmount),1) AS Credit, A.BizPartnerCode  FROM Accounting.GeneralLedger A "
                    + " INNER JOIN Accounting.ChartOfAccount B ON A.AccountCode = B.AccountCode "
                    + " INNER JOIN Accounting.GLSubsiCode C ON A.SubsiCode = C.SubsiCode "
                    + " AND A.AccountCode = C.AccountCode WHERE A.DocNumber = '" + DocNumber + "' AND (TransType ='PRDFWT') ", Conn);

                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }
        }

        public class RefTransaction
        {
            public virtual FinalWIPOUT Parent { get; set; }
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
                                            + " on A.RMenuID = B.ModuleID "
                                            + " inner join IT.MainMenu C "
                                            + " on A.MenuID = C.ModuleID "
                                            + "  where (DocNumber='" + DocNumber + "' OR   REFDocNumber='" + DocNumber + "') and  (RTransType='PRDFWT' OR  A.TransType='PRDFWT') ", Conn);
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
