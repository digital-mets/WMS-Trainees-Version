using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GearsLibrary;
using System.Data;

namespace Entity
{
    public class CuttingWorksheet
    {
        private static string Docnum;

        private static string Conn;//ADD CONN
        public virtual string Connection { get; set; }//ADD CONN
        public virtual string DocNumber { get; set; }
        public virtual string DocDate { get; set; }
        public virtual string JobOrder { get; set; }
        public virtual string Type { get; set; }
        public virtual string Step { get; set; }
   
        public virtual string WorkCenter { get; set; }

        public virtual string ReceivedWorkCenter { get; set; }

        public virtual string ProductCode { get; set; }

        public virtual string ProductColor { get; set; }
        public virtual decimal TotalQuantity { get; set; }
        public virtual string DRDocnumber { get; set; }
        public virtual string Remarks { get; set; }
        public virtual string OverheadCode { get; set; }
        public virtual decimal WorkOrderPrice { get; set; }
        public virtual decimal OriginalWorkOrderPrice { get; set; }
        public virtual string VatCode { get; set; }
        public virtual string ATCCode { get; set; }
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

        public virtual IList<WCSizeBreakDown> Detail { get; set; }


        public class WCSizeBreakDown
        {

            public virtual CuttingWorksheet Parent { get; set; }
            public virtual string DocNumber { get; set; }
            public virtual string LineNumber { get; set; }

            public virtual string SizeCode { get; set; }
            public virtual decimal Qty { get; set; }

            public virtual decimal JOBreakdown { get; set; }
            public virtual decimal QtyDifference { get; set; }

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
                    a = Gears.RetriveData2("select * from Production.WCSizeBreakDown where DocNumber='" + DocNumber + "' order by LineNumber", Conn);
                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }
            public void AddWCSizeBreakDown(WCSizeBreakDown WCSizeBreakDown)
            {

                int linenum = 0;


                DataTable count = Gears.RetriveData2("select max(convert(int,LineNumber)) as LineNumber from Production.WCSizeBreakDown where docnumber = '" + Docnum + "'", Conn);

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

                DT1.Rows.Add("Production.WCSizeBreakDown", "0", "DocNumber", Docnum);
                DT1.Rows.Add("Production.WCSizeBreakDown", "0", "LineNumber", strLine);
                DT1.Rows.Add("Production.WCSizeBreakDown", "0", "SizeCode", WCSizeBreakDown.SizeCode);
                DT1.Rows.Add("Production.WCSizeBreakDown", "0", "Qty", WCSizeBreakDown.Qty);
                DT1.Rows.Add("Production.WCSizeBreakDown", "0", "JOBreakdown", WCSizeBreakDown.JOBreakdown);
                DT1.Rows.Add("Production.WCSizeBreakDown", "0", "QtyDifference", WCSizeBreakDown.QtyDifference);

                DT1.Rows.Add("Production.WCSizeBreakDown", "0", "Field1", WCSizeBreakDown.Field1);
                DT1.Rows.Add("Production.WCSizeBreakDown", "0", "Field2", WCSizeBreakDown.Field2);
                DT1.Rows.Add("Production.WCSizeBreakDown", "0", "Field3", WCSizeBreakDown.Field3);
                DT1.Rows.Add("Production.WCSizeBreakDown", "0", "Field4", WCSizeBreakDown.Field4);
                DT1.Rows.Add("Production.WCSizeBreakDown", "0", "Field5", WCSizeBreakDown.Field5);
                DT1.Rows.Add("Production.WCSizeBreakDown", "0", "Field6", WCSizeBreakDown.Field6);
                DT1.Rows.Add("Production.WCSizeBreakDown", "0", "Field7", WCSizeBreakDown.Field7);
                DT1.Rows.Add("Production.WCSizeBreakDown", "0", "Field8", WCSizeBreakDown.Field8);
                DT1.Rows.Add("Production.WCSizeBreakDown", "0", "Field9", WCSizeBreakDown.Field9);



                //DT2.Rows.Add("Production.CuttingWorksheet", "cond", "DocNumber", Docnum);
                //DT2.Rows.Add("Production.CuttingWorksheet", "set", "IsWithDetail", "True");

                Gears.CreateData(DT1, Conn);
                //Gears.UpdateData(DT2);

            }
            public void UpdateWCSizeBreakDown(WCSizeBreakDown WCSizeBreakDown)
            {
                int linenum = 0;

                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

                DT1.Rows.Add("Production.WCSizeBreakDown", "cond", "DocNumber", Docnum);
                DT1.Rows.Add("Production.WCSizeBreakDown", "cond", "LineNumber", WCSizeBreakDown.LineNumber);
                DT1.Rows.Add("Production.WCSizeBreakDown", "set", "SizeCode", WCSizeBreakDown.SizeCode);
                DT1.Rows.Add("Production.WCSizeBreakDown", "set", "Qty", WCSizeBreakDown.Qty);
                DT1.Rows.Add("Production.WCSizeBreakDown", "set", "JOBreakdown", WCSizeBreakDown.JOBreakdown);
                DT1.Rows.Add("Production.WCSizeBreakDown", "set", "QtyDifference", WCSizeBreakDown.QtyDifference);
                

                DT1.Rows.Add("Production.WCSizeBreakDown", "set", "Field1", WCSizeBreakDown.Field1);
                DT1.Rows.Add("Production.WCSizeBreakDown", "set", "Field2", WCSizeBreakDown.Field2);
                DT1.Rows.Add("Production.WCSizeBreakDown", "set", "Field3", WCSizeBreakDown.Field3);
                DT1.Rows.Add("Production.WCSizeBreakDown", "set", "Field4", WCSizeBreakDown.Field4);
                DT1.Rows.Add("Production.WCSizeBreakDown", "set", "Field5", WCSizeBreakDown.Field5);
                DT1.Rows.Add("Production.WCSizeBreakDown", "set", "Field6", WCSizeBreakDown.Field6);
                DT1.Rows.Add("Production.WCSizeBreakDown", "set", "Field7", WCSizeBreakDown.Field7);
                DT1.Rows.Add("Production.WCSizeBreakDown", "set", "Field8", WCSizeBreakDown.Field8);
                DT1.Rows.Add("Production.WCSizeBreakDown", "set", "Field9", WCSizeBreakDown.Field9);

                Gears.UpdateData(DT1, Conn);




            }
            public void DeleteWCSizeBreakDown(WCSizeBreakDown WCSizeBreakDown)
            {

                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT3 = new Gears.CRUDdatatable();
                DT1.Rows.Add("Production.WCSizeBreakDown", "cond", "DocNumber", WCSizeBreakDown.DocNumber);
                DT1.Rows.Add("Production.WCSizeBreakDown", "cond", "LineNumber", WCSizeBreakDown.LineNumber);


                Gears.DeleteData(DT1, Conn);


                //DataTable count = Gears.RetriveData2("select * from Production.WCSizeBreakDown where docnumber = '" + Docnum + "'");

                //if (count.Rows.Count < 1)
                //{
                //    DT2.Rows.Add("Production.CuttingWorksheet", "cond", "DocNumber", Docnum);
                //    DT2.Rows.Add("Production.CuttingWorksheet", "set", "IsWithDetail", "False");
                //    Gears.UpdateData(DT2);
                //}


            }




        }
        public class CWUsedFabric
        {

            public virtual CuttingWorksheet Parent { get; set; }
            public virtual string DocNumber { get; set; }
            public virtual string LineNumber { get; set; }
            public virtual string ItemCode { get; set; }
            public virtual string ColorCode { get; set; }
            public virtual string ClassCode { get; set; }
            public virtual string SizeCode { get; set; }
            public virtual string Unit { get; set; }
            public virtual decimal IssuedQty { get; set; }
            public virtual decimal ReturnQty { get; set; }
            public virtual decimal UsedQty { get; set; }
            public virtual decimal EndCuts { get; set; }
            public virtual decimal OverageShortage { get; set; }
            public virtual decimal ForReturn { get; set; }

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
                    a = Gears.RetriveData2("select * from Production.CWUsedFabric where DocNumber='" + DocNumber + "' order by LineNumber", Conn);
                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }
            public void AddCWUsedFabric(CWUsedFabric CWUsedFabric)
            {

                int linenum = 0;


                DataTable count = Gears.RetriveData2("select max(convert(int,LineNumber)) as LineNumber from Production.CWUsedFabric where docnumber = '" + Docnum + "'", Conn);

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

                DT1.Rows.Add("Production.CWUsedFabric", "0", "DocNumber", Docnum);
                DT1.Rows.Add("Production.CWUsedFabric", "0", "LineNumber", strLine);
                DT1.Rows.Add("Production.CWUsedFabric", "0", "ItemCode", CWUsedFabric.ItemCode);
                DT1.Rows.Add("Production.CWUsedFabric", "0", "ColorCode", CWUsedFabric.ColorCode);
                DT1.Rows.Add("Production.CWUsedFabric", "0", "ClassCode", CWUsedFabric.ClassCode);
                DT1.Rows.Add("Production.CWUsedFabric", "0", "SizeCode", CWUsedFabric.SizeCode);
                DT1.Rows.Add("Production.CWUsedFabric", "0", "Unit", CWUsedFabric.Unit);
                DT1.Rows.Add("Production.CWUsedFabric", "0", "IssuedQty", CWUsedFabric.IssuedQty);
                DT1.Rows.Add("Production.CWUsedFabric", "0", "ReturnQty", CWUsedFabric.ReturnQty);
                DT1.Rows.Add("Production.CWUsedFabric", "0", "UsedQty", CWUsedFabric.UsedQty);
                DT1.Rows.Add("Production.CWUsedFabric", "0", "EndCuts", CWUsedFabric.EndCuts);
                DT1.Rows.Add("Production.CWUsedFabric", "0", "OverageShortage", CWUsedFabric.OverageShortage);
                DT1.Rows.Add("Production.CWUsedFabric", "0", "ForReturn", CWUsedFabric.ForReturn);


                DT1.Rows.Add("Production.CWUsedFabric", "0", "Field1", CWUsedFabric.Field1);
                DT1.Rows.Add("Production.CWUsedFabric", "0", "Field2", CWUsedFabric.Field2);
                DT1.Rows.Add("Production.CWUsedFabric", "0", "Field3", CWUsedFabric.Field3);
                DT1.Rows.Add("Production.CWUsedFabric", "0", "Field4", CWUsedFabric.Field4);
                DT1.Rows.Add("Production.CWUsedFabric", "0", "Field5", CWUsedFabric.Field5);
                DT1.Rows.Add("Production.CWUsedFabric", "0", "Field6", CWUsedFabric.Field6);
                DT1.Rows.Add("Production.CWUsedFabric", "0", "Field7", CWUsedFabric.Field7);
                DT1.Rows.Add("Production.CWUsedFabric", "0", "Field8", CWUsedFabric.Field8);
                DT1.Rows.Add("Production.CWUsedFabric", "0", "Field9", CWUsedFabric.Field9);



                //DT2.Rows.Add("Production.CuttingWorksheet", "cond", "DocNumber", Docnum);
                //DT2.Rows.Add("Production.CuttingWorksheet", "set", "IsWithDetail", "True");

                Gears.CreateData(DT1, Conn);
                //Gears.UpdateData(DT2);

            }
            public void UpdateCWUsedFabric(CWUsedFabric CWUsedFabric)
            {
                int linenum = 0;

                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

                DT1.Rows.Add("Production.CWUsedFabric", "cond", "DocNumber", Docnum);
                DT1.Rows.Add("Production.CWUsedFabric", "cond", "LineNumber", CWUsedFabric.LineNumber);
                DT1.Rows.Add("Production.CWUsedFabric", "set", "ItemCode", CWUsedFabric.ItemCode);
                DT1.Rows.Add("Production.CWUsedFabric", "set", "ColorCode", CWUsedFabric.ColorCode);
                DT1.Rows.Add("Production.CWUsedFabric", "set", "ClassCode", CWUsedFabric.ClassCode);
                DT1.Rows.Add("Production.CWUsedFabric", "set", "SizeCode", CWUsedFabric.SizeCode);
                DT1.Rows.Add("Production.CWUsedFabric", "set", "Unit", CWUsedFabric.Unit);
                DT1.Rows.Add("Production.CWUsedFabric", "set", "IssuedQty", CWUsedFabric.IssuedQty);
                DT1.Rows.Add("Production.CWUsedFabric", "set", "ReturnQty", CWUsedFabric.ReturnQty);
                DT1.Rows.Add("Production.CWUsedFabric", "set", "UsedQty", CWUsedFabric.UsedQty);
                DT1.Rows.Add("Production.CWUsedFabric", "set", "EndCuts", CWUsedFabric.EndCuts);
                DT1.Rows.Add("Production.CWUsedFabric", "set", "OverageShortage", CWUsedFabric.OverageShortage);
                DT1.Rows.Add("Production.CWUsedFabric", "set", "ForReturn", CWUsedFabric.ForReturn);

                DT1.Rows.Add("Production.CWUsedFabric", "set", "Field1", CWUsedFabric.Field1);
                DT1.Rows.Add("Production.CWUsedFabric", "set", "Field2", CWUsedFabric.Field2);
                DT1.Rows.Add("Production.CWUsedFabric", "set", "Field3", CWUsedFabric.Field3);
                DT1.Rows.Add("Production.CWUsedFabric", "set", "Field4", CWUsedFabric.Field4);
                DT1.Rows.Add("Production.CWUsedFabric", "set", "Field5", CWUsedFabric.Field5);
                DT1.Rows.Add("Production.CWUsedFabric", "set", "Field6", CWUsedFabric.Field6);
                DT1.Rows.Add("Production.CWUsedFabric", "set", "Field7", CWUsedFabric.Field7);
                DT1.Rows.Add("Production.CWUsedFabric", "set", "Field8", CWUsedFabric.Field8);
                DT1.Rows.Add("Production.CWUsedFabric", "set", "Field9", CWUsedFabric.Field9);

                Gears.UpdateData(DT1, Conn);




            }
            public void DeleteCWUsedFabric(CWUsedFabric CWUsedFabric)
            {

                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT3 = new Gears.CRUDdatatable();
                DT1.Rows.Add("Production.CWUsedFabric", "cond", "DocNumber", CWUsedFabric.DocNumber);
                DT1.Rows.Add("Production.CWUsedFabric", "cond", "LineNumber", CWUsedFabric.LineNumber);


                Gears.DeleteData(DT1, Conn);


                //DataTable count = Gears.RetriveData2("select * from Production.CWUsedFabric where docnumber = '" + Docnum + "'");

                //if (count.Rows.Count < 1)
                //{
                //    DT2.Rows.Add("Production.CuttingWorksheet", "cond", "DocNumber", Docnum);
                //    DT2.Rows.Add("Production.CuttingWorksheet", "set", "IsWithDetail", "False");
                //    Gears.UpdateData(DT2);
                //}


            }


         


        }
        public class RefTransaction
        {
            public virtual CuttingWorksheet Parent { get; set; }
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
                                            + " on A.RMenuID =B.ModuleID "
                                            + " inner join IT.MainMenu C "
                                            + " on A.MenuID = C.ModuleID "
                                            + "  where (DocNumber='" + DocNumber + "' OR   REFDocNumber='" + DocNumber + "') and  (RTransType='PRDCWT' OR  A.TransType='PRDCWT') ", Conn);
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
            a = Gears.RetriveData2("select * from Production.CuttingWorksheet where DocNumber = '" + DocNumber + "'", Conn);
            foreach (DataRow dtRow in a.Rows)
            {
                DocNumber = dtRow["DocNumber"].ToString();
                DocDate = dtRow["Docdate"].ToString();
                Type = dtRow["Type"].ToString();
                JobOrder = dtRow["JobOrder"].ToString();
                Step = dtRow["Step"].ToString();
                WorkCenter = dtRow["WorkCenter"].ToString();
                ReceivedWorkCenter = dtRow["ReceivedWorkCenter"].ToString();
                ProductCode = dtRow["ProductCode"].ToString();
                ProductColor = dtRow["ProductColor"].ToString();
                TotalQuantity = Convert.ToDecimal(Convert.IsDBNull(dtRow["TotalQuantity"]) ? 0 : dtRow["TotalQuantity"]);

                DRDocnumber = dtRow["DRDocnumber"].ToString();
                Remarks = dtRow["Remarks"].ToString();
                OverheadCode = dtRow["OverheadCode"].ToString();  
                WorkOrderPrice = Convert.ToDecimal(Convert.IsDBNull(dtRow["WorkOrderPrice"]) ? 0 : dtRow["WorkOrderPrice"]);
                OriginalWorkOrderPrice = Convert.ToDecimal(Convert.IsDBNull(dtRow["OriginalWorkOrderPrice"]) ? 0 : dtRow["OriginalWorkOrderPrice"]);
                VatCode = dtRow["VatCode"].ToString();
                ATCCode = dtRow["ATCCode"].ToString();
                Currency = dtRow["Currency"].ToString();

                ExchangeRate = Convert.ToDecimal(Convert.IsDBNull(dtRow["ExchangeRate"]) ? 0 : dtRow["ExchangeRate"]);
                PesoAmount = Convert.ToDecimal(Convert.IsDBNull(dtRow["PesoAmount"]) ? 0 : dtRow["PesoAmount"]);
                ForeignAmount = Convert.ToDecimal(Convert.IsDBNull(dtRow["ForeignAmount"]) ? 0 : dtRow["ForeignAmount"]);
                GrossVatableAmount = Convert.ToDecimal(Convert.IsDBNull(dtRow["GrossVatableAmount"]) ? 0 : dtRow["GrossVatableAmount"]);
                NonVatableAmount = Convert.ToDecimal(Convert.IsDBNull(dtRow["NonVatableAmount"]) ? 0 : dtRow["NonVatableAmount"]);
                VatAmount = Convert.ToDecimal(Convert.IsDBNull(dtRow["VatAmount"]) ? 0 : dtRow["VatAmount"]);
                WTaxAmount = Convert.ToDecimal(Convert.IsDBNull(dtRow["WTaxAmount"]) ? 0 : dtRow["WTaxAmount"]);
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
        public void InsertData(CuttingWorksheet _ent)
        {
            Docnum = _ent.DocNumber;
            Conn = _ent.Connection;     //ADD CONN

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

    
            DT1.Rows.Add("Production.CuttingWorksheet", "0", "DocNumber", _ent.DocNumber);
            DT1.Rows.Add("Production.CuttingWorksheet", "0", "DocDate", _ent.DocDate);
            DT1.Rows.Add("Production.CuttingWorksheet", "0", "Type  ", _ent.Type);
         
            DT1.Rows.Add("Production.CuttingWorksheet", "0", "JobOrder  ", _ent.JobOrder);
            DT1.Rows.Add("Production.CuttingWorksheet", "0", "Step  ", _ent.Step);
            DT1.Rows.Add("Production.CuttingWorksheet", "0", "WorkCenter  ", _ent.WorkCenter);
            DT1.Rows.Add("Production.CuttingWorksheet", "0", "ReceivedWorkCenter  ", _ent.ReceivedWorkCenter);
            DT1.Rows.Add("Production.CuttingWorksheet", "0", "ProductCode  ", _ent.ProductCode);
            DT1.Rows.Add("Production.CuttingWorksheet", "0", "ProductColor  ", _ent.ProductColor);
            DT1.Rows.Add("Production.CuttingWorksheet", "0", "TotalQuantity  ", _ent.TotalQuantity);
            DT1.Rows.Add("Production.CuttingWorksheet", "0", "DRDocnumber ", _ent.DRDocnumber);
            DT1.Rows.Add("Production.CuttingWorksheet", "0", "Remarks", _ent.Remarks);
            DT1.Rows.Add("Production.CuttingWorksheet", "0", "WorkOrderPrice ", _ent.WorkOrderPrice);
            DT1.Rows.Add("Production.CuttingWorksheet", "0", "OriginalWorkOrderPrice ", _ent.OriginalWorkOrderPrice);
            DT1.Rows.Add("Production.CuttingWorksheet", "0", "VatCode ", _ent.VatCode);
            DT1.Rows.Add("Production.CuttingWorksheet", "0", "ATCCode ", _ent.ATCCode);
            DT1.Rows.Add("Production.CuttingWorksheet", "0", "Currency ", _ent.Currency);
            DT1.Rows.Add("Production.CuttingWorksheet", "0", "VatRate ", _ent.VatRate);
            DT1.Rows.Add("Production.CuttingWorksheet", "0", "AtcRate ", _ent.AtcRate);

            DT1.Rows.Add("Production.CuttingWorksheet", "0", "ExchangeRate", _ent.ExchangeRate);
            DT1.Rows.Add("Production.CuttingWorksheet", "0", "PesoAmount", _ent.PesoAmount);
            DT1.Rows.Add("Production.CuttingWorksheet", "0", "ForeignAmount", _ent.ForeignAmount);
            DT1.Rows.Add("Production.CuttingWorksheet", "0", "GrossVatableAmount", _ent.GrossVatableAmount);
            DT1.Rows.Add("Production.CuttingWorksheet", "0", "NonVatableAmount", _ent.NonVatableAmount);
            DT1.Rows.Add("Production.CuttingWorksheet", "0", "VatAmount", _ent.VatAmount);
            DT1.Rows.Add("Production.CuttingWorksheet", "0", "WTaxAmount", _ent.WTaxAmount);



            DT1.Rows.Add("Production.CuttingWorksheet", "0", "Field1", _ent.Field1);
            DT1.Rows.Add("Production.CuttingWorksheet", "0", "Field2", _ent.Field2);
            DT1.Rows.Add("Production.CuttingWorksheet", "0", "Field3", _ent.Field3);
            DT1.Rows.Add("Production.CuttingWorksheet", "0", "Field4", _ent.Field4);
            DT1.Rows.Add("Production.CuttingWorksheet", "0", "Field5", _ent.Field5);
            DT1.Rows.Add("Production.CuttingWorksheet", "0", "Field6", _ent.Field6);
            DT1.Rows.Add("Production.CuttingWorksheet", "0", "Field7", _ent.Field7);
            DT1.Rows.Add("Production.CuttingWorksheet", "0", "Field8", _ent.Field8);
            DT1.Rows.Add("Production.CuttingWorksheet", "0", "Field9", _ent.Field9);
            DT1.Rows.Add("Production.CuttingWorksheet", "0", "SubmittedBy", "");
            DT1.Rows.Add("Production.CuttingWorksheet", "0", "IsWithDetail", "False");


            Gears.CreateData(DT1, _ent.Connection);

        }

        public void UpdateData(CuttingWorksheet _ent)
        {
            Docnum = _ent.DocNumber;
            Conn = _ent.Connection;     //ADD CONN

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Production.CuttingWorksheet", "cond", "DocNumber", _ent.DocNumber);

            DT1.Rows.Add("Production.CuttingWorksheet", "set", "DocDate", _ent.DocDate);
            DT1.Rows.Add("Production.CuttingWorksheet", "set", "Type  ", _ent.Type);

            DT1.Rows.Add("Production.CuttingWorksheet", "set", "JobOrder  ", _ent.JobOrder);
            DT1.Rows.Add("Production.CuttingWorksheet", "set", "Step  ", _ent.Step);
            DT1.Rows.Add("Production.CuttingWorksheet", "set", "WorkCenter  ", _ent.WorkCenter);
            DT1.Rows.Add("Production.CuttingWorksheet", "set", "ReceivedWorkCenter  ", _ent.ReceivedWorkCenter);
            DT1.Rows.Add("Production.CuttingWorksheet", "set", "ProductCode  ", _ent.ProductCode);
            DT1.Rows.Add("Production.CuttingWorksheet", "set", "ProductColor  ", _ent.ProductColor);
            DT1.Rows.Add("Production.CuttingWorksheet", "set", "TotalQuantity  ", _ent.TotalQuantity);
            DT1.Rows.Add("Production.CuttingWorksheet", "set", "DRDocnumber ", _ent.DRDocnumber);
            DT1.Rows.Add("Production.CuttingWorksheet", "set", "Remarks", _ent.Remarks); 
            DT1.Rows.Add("Production.CuttingWorksheet", "set", "OverheadCode", _ent.OverheadCode);
           
            DT1.Rows.Add("Production.CuttingWorksheet", "set", "WorkOrderPrice ", _ent.WorkOrderPrice);
            DT1.Rows.Add("Production.CuttingWorksheet", "set", "OriginalWorkOrderPrice ", _ent.OriginalWorkOrderPrice);
            DT1.Rows.Add("Production.CuttingWorksheet", "set", "VatCode ", _ent.VatCode);
            DT1.Rows.Add("Production.CuttingWorksheet", "set", "ATCCode ", _ent.ATCCode);
            DT1.Rows.Add("Production.CuttingWorksheet", "set", "Currency ", _ent.Currency);
            DT1.Rows.Add("Production.CuttingWorksheet", "set", "ExchangeRate", _ent.ExchangeRate);
            DT1.Rows.Add("Production.CuttingWorksheet", "set", "PesoAmount", _ent.PesoAmount);
            DT1.Rows.Add("Production.CuttingWorksheet", "set", "ForeignAmount", _ent.ForeignAmount);
            DT1.Rows.Add("Production.CuttingWorksheet", "set", "GrossVatableAmount", _ent.GrossVatableAmount);
            DT1.Rows.Add("Production.CuttingWorksheet", "set", "NonVatableAmount", _ent.NonVatableAmount);
            DT1.Rows.Add("Production.CuttingWorksheet", "set", "VatAmount", _ent.VatAmount);
            DT1.Rows.Add("Production.CuttingWorksheet", "set", "WTaxAmount", _ent.WTaxAmount);
            DT1.Rows.Add("Production.CuttingWorksheet", "set", "VatRate ", _ent.VatRate);
            DT1.Rows.Add("Production.CuttingWorksheet", "set", "AtcRate ", _ent.AtcRate);
            DT1.Rows.Add("Production.CuttingWorksheet", "set", "Field1", _ent.Field1);
            DT1.Rows.Add("Production.CuttingWorksheet", "set", "Field2", _ent.Field2);
            DT1.Rows.Add("Production.CuttingWorksheet", "set", "Field3", _ent.Field3);
            DT1.Rows.Add("Production.CuttingWorksheet", "set", "Field4", _ent.Field4);
            DT1.Rows.Add("Production.CuttingWorksheet", "set", "Field5", _ent.Field5);
            DT1.Rows.Add("Production.CuttingWorksheet", "set", "Field6", _ent.Field6);
            DT1.Rows.Add("Production.CuttingWorksheet", "set", "Field7", _ent.Field7);
            DT1.Rows.Add("Production.CuttingWorksheet", "set", "Field8", _ent.Field8);
            DT1.Rows.Add("Production.CuttingWorksheet", "set", "Field9", _ent.Field9);
            DT1.Rows.Add("Production.CuttingWorksheet", "set", "SubmittedBy", "");
            DT1.Rows.Add("Production.CuttingWorksheet", "set", "LastEditedBy", _ent.LastEditedBy);
            DT1.Rows.Add("Production.CuttingWorksheet", "set", "LastEditedDate", _ent.LastEditedDate);
            Gears.UpdateData(DT1, _ent.Connection);


            Functions.AuditTrail("PRDCWT", Docnum, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "UPDATE", _ent.Connection);
        }

        public void DeleteData(CuttingWorksheet _ent)
        {
            Docnum = _ent.DocNumber;
            Conn = _ent.Connection;     //ADD CONN

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Production.CuttingWorksheet", "cond", "DocNumber", _ent.DocNumber);
            Gears.DeleteData(DT1, _ent.Connection);
            Functions.AuditTrail("PRDCWT", Docnum, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "DELETE", _ent.Connection);
        }
        public class JournalEntry
        {
            public virtual CuttingWorksheet Parent { get; set; }
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
                    + " AND A.AccountCode = C.AccountCode WHERE A.DocNumber = '" + DocNumber + "' AND (TransType ='PRDCWT') ", Conn);

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
