using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GearsLibrary;
using System.Data;

namespace Entity
{
   public class ServiceOrder
    {
         private static string Docnum;

         private static string Conn;//ADD CONN
         public virtual string Connection { get; set; }//ADD CONN
        public virtual string DocNumber { get; set; }
        public virtual string DocDate { get; set; }
        public virtual string DueDate { get; set; }
        public virtual string CustomerCode { get; set; }
        public virtual string ReferenceNo { get; set; }
        public virtual string ItemCode { get; set; }
        public virtual string ColorCode { get; set; }
        public virtual string Status { get; set; }
        public virtual string DateCompleted { get; set; }
        public virtual string Remarks { get; set; }
        public virtual decimal TotalSVOQty { get; set; }
        public virtual decimal TotalINQty { get; set; }
        public virtual decimal TotalFinalQty { get; set; }
        public virtual decimal TotalLabor { get; set; }
        public virtual decimal TotalRawMaterials { get; set; }
        public virtual decimal UnitCost { get; set; }
        public virtual decimal EstAccCost { get; set; }
        public virtual decimal EstUnitCost { get; set; }

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
        public virtual string ManualClosedBy { get; set; }
        public virtual string ManualClosedDate { get; set; }

        public virtual IList<SOBillOfMaterial> Detail { get; set; }
        public virtual IList<SOWorkOrder> Detail1 { get; set; }


        public class SOBillOfMaterial
        {
            public virtual ServiceOrder Parent { get; set; }
            public virtual string DocNumber { get; set; }
            public virtual string LineNumber { get; set; }

       
            public virtual string StockSizes { get; set; }
            public virtual string FullDesc { get; set; }
            public virtual string ItemCode { get; set; }
            public virtual string ColorCode { get; set; }
            public virtual string ClassCode { get; set; }
            public virtual string SizeCode { get; set; }
             public virtual string Unit { get; set; }
            public virtual decimal PerPieceConsumption { get; set; }
            public virtual decimal Consumption { get; set; }
            public virtual decimal Cost { get; set; }
     
            public virtual bool MajorMaterial { get; set; }
            public virtual bool ByBulk { get; set; }

            public virtual string Field1 { get; set; }
            public virtual string Field2 { get; set; }
            public virtual string Field3 { get; set; }
            public virtual string Field4 { get; set; }
            public virtual string Field5 { get; set; }
            public virtual string Field6 { get; set; }
            public virtual string Field7 { get; set; }
            public virtual string Field8 { get; set; }
            public virtual string Field9 { get; set; }
            public virtual string Tagged { get; set; }

            
            public DataTable getdetail(string DocNumber, string Conn)
            {

                DataTable a;
                try
                {
                    a = Gears.RetriveData2("select A.*,B.FullDesc,'' as Tagged from Procurement.SOBillOfMaterial A inner join Masterfile.Item B on A.ItemCode = B.ItemCode where DocNumber='" + DocNumber + "' order by LineNumber", Conn);
                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }
            public void AddSOBillOfMaterial(SOBillOfMaterial SOBillOfMaterial)
            {

                int linenum = 0;


                DataTable count = Gears.RetriveData2("select max(convert(int,LineNumber)) as LineNumber from Procurement.SOBillOfMaterial where docnumber = '" + Docnum + "'", Conn);

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

                DT1.Rows.Add("Procurement.SOBillOfMaterial", "0", "DocNumber", Docnum);
                DT1.Rows.Add("Procurement.SOBillOfMaterial", "0", "LineNumber", strLine);
            
                DT1.Rows.Add("Procurement.SOBillOfMaterial", "0", "StockSizes", SOBillOfMaterial.StockSizes);
                DT1.Rows.Add("Procurement.SOBillOfMaterial", "0", "ItemCode", SOBillOfMaterial.ItemCode);
                DT1.Rows.Add("Procurement.SOBillOfMaterial", "0", "ColorCode", SOBillOfMaterial.ColorCode);
                DT1.Rows.Add("Procurement.SOBillOfMaterial", "0", "ClassCode", SOBillOfMaterial.ClassCode);
                DT1.Rows.Add("Procurement.SOBillOfMaterial", "0", "SizeCode", SOBillOfMaterial.SizeCode);
                DT1.Rows.Add("Procurement.SOBillOfMaterial", "0", "Unit ", SOBillOfMaterial.Unit);
                DT1.Rows.Add("Procurement.SOBillOfMaterial", "0", "Cost", SOBillOfMaterial.Cost);
                DT1.Rows.Add("Procurement.SOBillOfMaterial", "0", "PerPieceConsumption", SOBillOfMaterial.PerPieceConsumption);
                DT1.Rows.Add("Procurement.SOBillOfMaterial", "0", "Consumption", SOBillOfMaterial.Consumption);
                DT1.Rows.Add("Procurement.SOBillOfMaterial", "0", "MajorMaterial", SOBillOfMaterial.MajorMaterial);
                DT1.Rows.Add("Procurement.SOBillOfMaterial", "0", "ByBulk", SOBillOfMaterial.ByBulk);

                DT1.Rows.Add("Procurement.SOBillOfMaterial", "0", "Field1", SOBillOfMaterial.Field1);
                DT1.Rows.Add("Procurement.SOBillOfMaterial", "0", "Field2", SOBillOfMaterial.Field2);
                DT1.Rows.Add("Procurement.SOBillOfMaterial", "0", "Field3", SOBillOfMaterial.Field3);
                DT1.Rows.Add("Procurement.SOBillOfMaterial", "0", "Field4", SOBillOfMaterial.Field4);
                DT1.Rows.Add("Procurement.SOBillOfMaterial", "0", "Field5", SOBillOfMaterial.Field5);
                DT1.Rows.Add("Procurement.SOBillOfMaterial", "0", "Field6", SOBillOfMaterial.Field6);
                DT1.Rows.Add("Procurement.SOBillOfMaterial", "0", "Field7", SOBillOfMaterial.Field7);
                DT1.Rows.Add("Procurement.SOBillOfMaterial", "0", "Field8", SOBillOfMaterial.Field8);
                DT1.Rows.Add("Procurement.SOBillOfMaterial", "0", "Field9", SOBillOfMaterial.Field9);

                //DT2.Rows.Add("Procurement.ServiceOrder", "cond", "DocNumber", Docnum);
                //DT2.Rows.Add("Procurement.ServiceOrder", "set", "IsWithDetail", "True");

                Gears.CreateData(DT1, Conn);
                //Gears.UpdateData(DT2);

            }
            public void UpdateSOBillOfMaterial(SOBillOfMaterial SOBillOfMaterial)
            {
                int linenum = 0;

                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

                DT1.Rows.Add("Procurement.SOBillOfMaterial", "cond", "DocNumber", Docnum);
                DT1.Rows.Add("Procurement.SOBillOfMaterial", "cond", "LineNumber", SOBillOfMaterial.LineNumber);

                DT1.Rows.Add("Procurement.SOBillOfMaterial", "set", "StockSizes", SOBillOfMaterial.StockSizes);
                DT1.Rows.Add("Procurement.SOBillOfMaterial", "set", "ItemCode", SOBillOfMaterial.ItemCode);
                DT1.Rows.Add("Procurement.SOBillOfMaterial", "set", "ColorCode", SOBillOfMaterial.ColorCode);
                DT1.Rows.Add("Procurement.SOBillOfMaterial", "set", "ClassCode", SOBillOfMaterial.ClassCode);
                DT1.Rows.Add("Procurement.SOBillOfMaterial", "set", "SizeCode", SOBillOfMaterial.SizeCode);
                DT1.Rows.Add("Procurement.SOBillOfMaterial", "set", "Unit ", SOBillOfMaterial.Unit);
                DT1.Rows.Add("Procurement.SOBillOfMaterial", "set", "Cost", SOBillOfMaterial.Cost);
                DT1.Rows.Add("Procurement.SOBillOfMaterial", "set", "PerPieceConsumption  ", SOBillOfMaterial.PerPieceConsumption);
                DT1.Rows.Add("Procurement.SOBillOfMaterial", "set", "Consumption", SOBillOfMaterial.Consumption);
                DT1.Rows.Add("Procurement.SOBillOfMaterial", "set", "MajorMaterial", SOBillOfMaterial.MajorMaterial);
                DT1.Rows.Add("Procurement.SOBillOfMaterial", "set", "ByBulk", SOBillOfMaterial.ByBulk);

                DT1.Rows.Add("Procurement.SOBillOfMaterial", "set", "Field1", SOBillOfMaterial.Field1);
                DT1.Rows.Add("Procurement.SOBillOfMaterial", "set", "Field2", SOBillOfMaterial.Field2);
                DT1.Rows.Add("Procurement.SOBillOfMaterial", "set", "Field3", SOBillOfMaterial.Field3);
                DT1.Rows.Add("Procurement.SOBillOfMaterial", "set", "Field4", SOBillOfMaterial.Field4);
                DT1.Rows.Add("Procurement.SOBillOfMaterial", "set", "Field5", SOBillOfMaterial.Field5);
                DT1.Rows.Add("Procurement.SOBillOfMaterial", "set", "Field6", SOBillOfMaterial.Field6);
                DT1.Rows.Add("Procurement.SOBillOfMaterial", "set", "Field7", SOBillOfMaterial.Field7);
                DT1.Rows.Add("Procurement.SOBillOfMaterial", "set", "Field8", SOBillOfMaterial.Field8);
                DT1.Rows.Add("Procurement.SOBillOfMaterial", "set", "Field9", SOBillOfMaterial.Field9);

                Gears.UpdateData(DT1, Conn);




            }
            public void DeleteSOBillOfMateriall(SOBillOfMaterial SOBillOfMaterial)
            {

                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT3 = new Gears.CRUDdatatable();
                DT1.Rows.Add("Procurement.SOBillOfMaterial", "cond", "DocNumber", SOBillOfMaterial.DocNumber);
                DT1.Rows.Add("Procurement.SOBillOfMaterial", "cond", "LineNumber", SOBillOfMaterial.LineNumber);


                Gears.DeleteData(DT1, Conn);


                DataTable count = Gears.RetriveData2("select * from Procurement.SOBillOfMaterial where docnumber = '" + Docnum + "'", Conn);

                if (count.Rows.Count < 1)
                {
                    DT2.Rows.Add("Procurement.ServiceOrder", "cond", "DocNumber", Docnum);
                    DT2.Rows.Add("Procurement.ServiceOrder", "set", "IsWithDetail", "False");
                    Gears.UpdateData(DT2, Conn);
                }

             
            }




        }
        public class SOWorkOrder
        {
            public virtual ServiceOrder Parent { get; set; }
            public virtual string DocNumber { get; set; }
            public virtual string LineNumber { get; set; }

            public virtual string WorkCenter { get; set; }
            public virtual decimal INQty { get; set; }
            
            public virtual decimal OutQty { get; set; }
            public virtual decimal AdjQty { get; set; }
            public virtual decimal Labor { get; set; }
            public virtual string VatCode { get; set; }

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
                    a = Gears.RetriveData2("select * from Procurement.SOWorkOrder where DocNumber='" + DocNumber + "' order by LineNumber", Conn);
                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }
            public void AddSOWorkOrder(SOWorkOrder SOWorkOrder)
            {

                int linenum = 0;


                DataTable count = Gears.RetriveData2("select max(convert(int,LineNumber)) as LineNumber from Procurement.SOWorkOrder where docnumber = '" + Docnum + "'", Conn);

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

                DT1.Rows.Add("Procurement.SOWorkOrder", "0", "DocNumber", Docnum);
                DT1.Rows.Add("Procurement.SOWorkOrder", "0", "LineNumber", strLine);
                DT1.Rows.Add("Procurement.SOWorkOrder", "0", "WorkCenter", SOWorkOrder.WorkCenter);
                DT1.Rows.Add("Procurement.SOWorkOrder", "0", "INQty", SOWorkOrder.INQty);
                DT1.Rows.Add("Procurement.SOWorkOrder", "0", "OutQty", SOWorkOrder.OutQty);
                DT1.Rows.Add("Procurement.SOWorkOrder", "0", "AdjQty", SOWorkOrder.AdjQty);
                DT1.Rows.Add("Procurement.SOWorkOrder", "0", "Labor", SOWorkOrder.Labor);
                DT1.Rows.Add("Procurement.SOWorkOrder", "0", "VatCode", SOWorkOrder.VatCode);


                DT1.Rows.Add("Procurement.SOWorkOrder", "0", "Field1", SOWorkOrder.Field1);
                DT1.Rows.Add("Procurement.SOWorkOrder", "0", "Field2", SOWorkOrder.Field2);
                DT1.Rows.Add("Procurement.SOWorkOrder", "0", "Field3", SOWorkOrder.Field3);
                DT1.Rows.Add("Procurement.SOWorkOrder", "0", "Field4", SOWorkOrder.Field4);
                DT1.Rows.Add("Procurement.SOWorkOrder", "0", "Field5", SOWorkOrder.Field5);
                DT1.Rows.Add("Procurement.SOWorkOrder", "0", "Field6", SOWorkOrder.Field6);
                DT1.Rows.Add("Procurement.SOWorkOrder", "0", "Field7", SOWorkOrder.Field7);
                DT1.Rows.Add("Procurement.SOWorkOrder", "0", "Field8", SOWorkOrder.Field8);
                DT1.Rows.Add("Procurement.SOWorkOrder", "0", "Field9", SOWorkOrder.Field9);

                Gears.CreateData(DT1, Conn);
                //Gears.UpdateData(DT2);

            }
            public void UpdateSOWorkOrder(SOWorkOrder SOWorkOrder)
            {
                int linenum = 0;

                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

                DT1.Rows.Add("Procurement.SOWorkOrder", "cond", "DocNumber", Docnum);
                DT1.Rows.Add("Procurement.SOWorkOrder", "cond", "LineNumber", SOWorkOrder.LineNumber);
                DT1.Rows.Add("Procurement.SOWorkOrder", "set", "WorkCenter", SOWorkOrder.WorkCenter);
                DT1.Rows.Add("Procurement.SOWorkOrder", "set", "INQty", SOWorkOrder.INQty);
                DT1.Rows.Add("Procurement.SOWorkOrder", "set", "OutQty", SOWorkOrder.OutQty);
                DT1.Rows.Add("Procurement.SOWorkOrder", "set", "AdjQty", SOWorkOrder.AdjQty);
                DT1.Rows.Add("Procurement.SOWorkOrder", "set", "Labor", SOWorkOrder.Labor);
                DT1.Rows.Add("Procurement.SOWorkOrder", "set", "VatCode", SOWorkOrder.VatCode);


                DT1.Rows.Add("Procurement.SOWorkOrder", "set", "Field1", SOWorkOrder.Field1);
                DT1.Rows.Add("Procurement.SOWorkOrder", "set", "Field2", SOWorkOrder.Field2);
                DT1.Rows.Add("Procurement.SOWorkOrder", "set", "Field3", SOWorkOrder.Field3);
                DT1.Rows.Add("Procurement.SOWorkOrder", "set", "Field4", SOWorkOrder.Field4);
                DT1.Rows.Add("Procurement.SOWorkOrder", "set", "Field5", SOWorkOrder.Field5);
                DT1.Rows.Add("Procurement.SOWorkOrder", "set", "Field6", SOWorkOrder.Field6);
                DT1.Rows.Add("Procurement.SOWorkOrder", "set", "Field7", SOWorkOrder.Field7);
                DT1.Rows.Add("Procurement.SOWorkOrder", "set", "Field8", SOWorkOrder.Field8);
                DT1.Rows.Add("Procurement.SOWorkOrder", "set", "Field9", SOWorkOrder.Field9);

                Gears.UpdateData(DT1, Conn);




            }
            public void DeleteSOWorkOrder(SOWorkOrder SOWorkOrder)
            {

                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT3 = new Gears.CRUDdatatable();
                DT1.Rows.Add("Procurement.SOWorkOrder", "cond", "DocNumber", SOWorkOrder.DocNumber);
                DT1.Rows.Add("Procurement.SOWorkOrder", "cond", "LineNumber", SOWorkOrder.LineNumber);


                Gears.DeleteData(DT1, Conn);


                DataTable count = Gears.RetriveData2("select * from Procurement.SOWorkOrder where docnumber = '" + Docnum + "'", Conn);

                if (count.Rows.Count < 1)
                {
                    DT2.Rows.Add("Procurement.ServiceOrder", "cond", "DocNumber", Docnum);
                    DT2.Rows.Add("Procurement.ServiceOrder", "set", "IsWithDetail", "False");
                    Gears.UpdateData(DT2, Conn);
                }

             
            }

        }
        public class SoSizeBreakDown
        {
     
            public virtual ServiceOrder Parent { get; set; }
            public virtual string DocNumber { get; set; }
            public virtual string LineNumber { get; set; }

            public virtual string StockSize { get; set; }
            public virtual decimal SVOQty { get; set; }
            
            public virtual decimal INQty { get; set; }
            public virtual decimal FinalQty { get; set; }

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
                    a = Gears.RetriveData2("select * from Procurement.SoSizeBreakDown where DocNumber='" + DocNumber + "' order by LineNumber", Conn);
                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }
            public void AddSoSizeBreakDown(SoSizeBreakDown SoSizeBreakDown)
            {

                int linenum = 0;


                DataTable count = Gears.RetriveData2("select max(convert(int,LineNumber)) as LineNumber from Procurement.SoSizeBreakDown where docnumber = '" + Docnum + "'", Conn);

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

                DT1.Rows.Add("Procurement.SoSizeBreakDown", "0", "DocNumber", Docnum);
                DT1.Rows.Add("Procurement.SoSizeBreakDown", "0", "LineNumber", strLine);
                DT1.Rows.Add("Procurement.SoSizeBreakDown", "0", "StockSize", SoSizeBreakDown.StockSize);
                DT1.Rows.Add("Procurement.SoSizeBreakDown", "0", "SVOQty", SoSizeBreakDown.SVOQty);
                DT1.Rows.Add("Procurement.SoSizeBreakDown", "0", "INQty", SoSizeBreakDown.INQty);
                DT1.Rows.Add("Procurement.SoSizeBreakDown", "0", "FinalQty", SoSizeBreakDown.FinalQty);


                DT1.Rows.Add("Procurement.SoSizeBreakDown", "0", "Field1", SoSizeBreakDown.Field1);
                DT1.Rows.Add("Procurement.SoSizeBreakDown", "0", "Field2", SoSizeBreakDown.Field2);
                DT1.Rows.Add("Procurement.SoSizeBreakDown", "0", "Field3", SoSizeBreakDown.Field3);
                DT1.Rows.Add("Procurement.SoSizeBreakDown", "0", "Field4", SoSizeBreakDown.Field4);
                DT1.Rows.Add("Procurement.SoSizeBreakDown", "0", "Field5", SoSizeBreakDown.Field5);
                DT1.Rows.Add("Procurement.SoSizeBreakDown", "0", "Field6", SoSizeBreakDown.Field6);
                DT1.Rows.Add("Procurement.SoSizeBreakDown", "0", "Field7", SoSizeBreakDown.Field7);
                DT1.Rows.Add("Procurement.SoSizeBreakDown", "0", "Field8", SoSizeBreakDown.Field8);
                DT1.Rows.Add("Procurement.SoSizeBreakDown", "0", "Field9", SoSizeBreakDown.Field9);

                //DT2.Rows.Add("Procurement.ServiceOrder", "cond", "DocNumber", Docnum);
                //DT2.Rows.Add("Procurement.ServiceOrder", "set", "IsWithDetail", "True");

                Gears.CreateData(DT1, Conn);
                //Gears.UpdateData(DT2);

            }
            public void UpdateSoSizeBreakDown(SoSizeBreakDown SoSizeBreakDown)
            {
                int linenum = 0;

                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

                DT1.Rows.Add("Procurement.SoSizeBreakDown", "cond", "DocNumber", Docnum);
                DT1.Rows.Add("Procurement.SoSizeBreakDown", "cond", "LineNumber", SoSizeBreakDown.LineNumber);
                DT1.Rows.Add("Procurement.SoSizeBreakDown", "set", "StockSize", SoSizeBreakDown.StockSize);
                DT1.Rows.Add("Procurement.SoSizeBreakDown", "set", "SVOQty", SoSizeBreakDown.SVOQty);
                DT1.Rows.Add("Procurement.SoSizeBreakDown", "set", "INQty", SoSizeBreakDown.INQty);
                DT1.Rows.Add("Procurement.SoSizeBreakDown", "set", "FinalQty", SoSizeBreakDown.FinalQty);


                DT1.Rows.Add("Procurement.SoSizeBreakDown", "set", "Field1", SoSizeBreakDown.Field1);
                DT1.Rows.Add("Procurement.SoSizeBreakDown", "set", "Field2", SoSizeBreakDown.Field2);
                DT1.Rows.Add("Procurement.SoSizeBreakDown", "set", "Field3", SoSizeBreakDown.Field3);
                DT1.Rows.Add("Procurement.SoSizeBreakDown", "set", "Field4", SoSizeBreakDown.Field4);
                DT1.Rows.Add("Procurement.SoSizeBreakDown", "set", "Field5", SoSizeBreakDown.Field5);
                DT1.Rows.Add("Procurement.SoSizeBreakDown", "set", "Field6", SoSizeBreakDown.Field6);
                DT1.Rows.Add("Procurement.SoSizeBreakDown", "set", "Field7", SoSizeBreakDown.Field7);
                DT1.Rows.Add("Procurement.SoSizeBreakDown", "set", "Field8", SoSizeBreakDown.Field8);
                DT1.Rows.Add("Procurement.SoSizeBreakDown", "set", "Field9", SoSizeBreakDown.Field9);

                Gears.UpdateData(DT1, Conn);




            }
            public void DeleteSoSizeBreakDown(SoSizeBreakDown SoSizeBreakDown)
            {

                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT3 = new Gears.CRUDdatatable();
                DT1.Rows.Add("Procurement.SoSizeBreakDown", "cond", "DocNumber", SoSizeBreakDown.DocNumber);
                DT1.Rows.Add("Procurement.SoSizeBreakDown", "cond", "LineNumber", SoSizeBreakDown.LineNumber);


                Gears.DeleteData(DT1, Conn);
       

                //DataTable count = Gears.RetriveData2("select * from Procurement.SoSizeBreakDown where docnumber = '" + Docnum + "'");

                //if (count.Rows.Count < 1)
                //{
                //    DT2.Rows.Add("Procurement.ServiceOrder", "cond", "DocNumber", Docnum);
                //    DT2.Rows.Add("Procurement.ServiceOrder", "set", "IsWithDetail", "False");
                //    Gears.UpdateData(DT2);
                //}

             
            }







        }
        public class SoClassBreakDown
            {

                public virtual ServiceOrder Parent { get; set; }
                public virtual string DocNumber { get; set; }
                public virtual string LineNumber { get; set; }

                public virtual string ClassCode { get; set; }
                public virtual decimal Quantity { get; set; }


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
                        a = Gears.RetriveData2("select * from Procurement.SoClassBreakDown where DocNumber='" + DocNumber + "' order by LineNumber", Conn);
                        return a;
                    }
                    catch (Exception e)
                    {
                        a = null;
                        return a;
                    }
                }
                //public void AddSoClassBreakDown(SoClassBreakDown SoClassBreakDown)
                //{

                //    int linenum = 0;


                //    DataTable count = Gears.RetriveData2("select max(convert(int,LineNumber)) as LineNumber from Procurement.SoClassBreakDown where docnumber = '" + Docnum + "'");

                //    try
                //    {
                //        linenum = Convert.ToInt32(count.Rows[0][0].ToString()) + 1;
                //    }
                //    catch
                //    {
                //        linenum = 1;
                //    }
                //    string strLine = linenum.ToString().PadLeft(5, '0');

                //    Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                //    Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();

                //    DT1.Rows.Add("Procurement.SoClassBreakDown", "0", "DocNumber", Docnum);
                //    DT1.Rows.Add("Procurement.SoClassBreakDown", "0", "LineNumber", strLine);
                //    DT1.Rows.Add("Procurement.SoClassBreakDown", "0", "ClassCode", SoClassBreakDown.ClassCode);
                //    DT1.Rows.Add("Procurement.SoClassBreakDown", "0", "Quantity", SoClassBreakDown.Quantity);

                //    DT1.Rows.Add("Procurement.SoClassBreakDown", "0", "Field1", SoClassBreakDown.Field1);
                //    DT1.Rows.Add("Procurement.SoClassBreakDown", "0", "Field2", SoClassBreakDown.Field2);
                //    DT1.Rows.Add("Procurement.SoClassBreakDown", "0", "Field3", SoClassBreakDown.Field3);
                //    DT1.Rows.Add("Procurement.SoClassBreakDown", "0", "Field4", SoClassBreakDown.Field4);
                //    DT1.Rows.Add("Procurement.SoClassBreakDown", "0", "Field5", SoClassBreakDown.Field5);
                //    DT1.Rows.Add("Procurement.SoClassBreakDown", "0", "Field6", SoClassBreakDown.Field6);
                //    DT1.Rows.Add("Procurement.SoClassBreakDown", "0", "Field7", SoClassBreakDown.Field7);
                //    DT1.Rows.Add("Procurement.SoClassBreakDown", "0", "Field8", SoClassBreakDown.Field8);
                //    DT1.Rows.Add("Procurement.SoClassBreakDown", "0", "Field9", SoClassBreakDown.Field9);



                //    //DT2.Rows.Add("Procurement.ServiceOrder", "cond", "DocNumber", Docnum);
                //    //DT2.Rows.Add("Procurement.ServiceOrder", "set", "IsWithDetail", "True");

                //    Gears.CreateData(DT1);
                //    //Gears.UpdateData(DT2);

                //}
                //public void UpdateSoClassBreakDown(SoClassBreakDown SoClassBreakDown)
                //{
                //    int linenum = 0;

                //    Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

                //    DT1.Rows.Add("Procurement.SoClassBreakDown", "cond", "DocNumber", Docnum);
                //    DT1.Rows.Add("Procurement.SoClassBreakDown", "cond", "LineNumber", SoClassBreakDown.LineNumber);
                //    DT1.Rows.Add("Procurement.SoClassBreakDown", "set", "ClassCode", SoClassBreakDown.ClassCode);
                //    DT1.Rows.Add("Procurement.SoClassBreakDown", "set", "Quantity", SoClassBreakDown.Quantity);


                //    DT1.Rows.Add("Procurement.SoClassBreakDown", "set", "Field1", SoClassBreakDown.Field1);
                //    DT1.Rows.Add("Procurement.SoClassBreakDown", "set", "Field2", SoClassBreakDown.Field2);
                //    DT1.Rows.Add("Procurement.SoClassBreakDown", "set", "Field3", SoClassBreakDown.Field3);
                //    DT1.Rows.Add("Procurement.SoClassBreakDown", "set", "Field4", SoClassBreakDown.Field4);
                //    DT1.Rows.Add("Procurement.SoClassBreakDown", "set", "Field5", SoClassBreakDown.Field5);
                //    DT1.Rows.Add("Procurement.SoClassBreakDown", "set", "Field6", SoClassBreakDown.Field6);
                //    DT1.Rows.Add("Procurement.SoClassBreakDown", "set", "Field7", SoClassBreakDown.Field7);
                //    DT1.Rows.Add("Procurement.SoClassBreakDown", "set", "Field8", SoClassBreakDown.Field8);
                //    DT1.Rows.Add("Procurement.SoClassBreakDown", "set", "Field9", SoClassBreakDown.Field9);

                //    Gears.UpdateData(DT1);




                //}
                //public void DeleteSoClassBreakDown(SoClassBreakDown SoClassBreakDown)
                //{

                //    Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                //    Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
                //    Gears.CRUDdatatable DT3 = new Gears.CRUDdatatable();
                //    DT1.Rows.Add("Procurement.SoClassBreakDown", "cond", "DocNumber", SoClassBreakDown.DocNumber);
                //    DT1.Rows.Add("Procurement.SoClassBreakDown", "cond", "LineNumber", SoClassBreakDown.LineNumber);


                //    Gears.DeleteData(DT1);


                //    //DataTable count = Gears.RetriveData2("select * from Procurement.SoClassBreakDown where docnumber = '" + Docnum + "'");

                //    //if (count.Rows.Count < 1)
                //    //{
                //    //    DT2.Rows.Add("Procurement.ServiceOrder", "cond", "DocNumber", Docnum);
                //    //    DT2.Rows.Add("Procurement.ServiceOrder", "set", "IsWithDetail", "False");
                //    //    Gears.UpdateData(DT2);
                //    //}


                //}

            }
        public class SOMaterialMovement
        {
            public virtual ServiceOrder Parent { get; set; }
            public virtual string DocNumber { get; set; }
            public virtual string LineNumber { get; set; }

            public virtual string ItemCode { get; set; }
            public virtual string ColorCode { get; set; }
            public virtual string ClassCode { get; set; }
            public virtual string SizeCode { get; set; }
            public virtual string Unit { get; set; }
            public virtual decimal IssuedQty { get; set; }
            public virtual decimal ReturnQty { get; set; }
            public virtual decimal INQty { get; set; }
            public virtual decimal Allocation { get; set; }
            public virtual decimal ReplacementQty { get; set; }
            public virtual decimal Qty { get; set; }


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
                    a = Gears.RetriveData2("select * from  Procurement.SOMaterialMovement where DocNumber='" + DocNumber + "' order by LineNumber", Conn);
                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }
            //public void AddSOMaterialMovement(SOMaterialMovement SOMaterialMovement)
            //{

            //    int linenum = 0;


            //    DataTable count = Gears.RetriveData2("select max(convert(int,LineNumber)) as LineNumber from  Procurement.SOMaterialMovement where docnumber = '" + Docnum + "'");

            //    try
            //    {
            //        linenum = Convert.ToInt32(count.Rows[0][0].ToString()) + 1;
            //    }
            //    catch
            //    {
            //        linenum = 1;
            //    }
            //    string strLine = linenum.ToString().PadLeft(5, '0');

            //    Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            //    Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();

            //    DT1.Rows.Add("Procurement.SOMaterialMovement", "0", "DocNumber", Docnum);
            //    DT1.Rows.Add("Procurement.SOMaterialMovement", "0", "LineNumber", strLine);
            //    DT1.Rows.Add("Procurement.SOMaterialMovement", "0", "ItemCode", SOMaterialMovement.ItemCode);
            //    DT1.Rows.Add("Procurement.SOMaterialMovement", "0", "ColorCode", SOMaterialMovement.ColorCode);
            //    DT1.Rows.Add("Procurement.SOMaterialMovement", "0", "ClassCode", SOMaterialMovement.ClassCode);
            //    DT1.Rows.Add("Procurement.SOMaterialMovement", "0", "SizeCode", SOMaterialMovement.SizeCode);
            //    DT1.Rows.Add("Procurement.SOMaterialMovement", "0", "Unit", SOMaterialMovement.Unit);
            //    DT1.Rows.Add("Procurement.SOMaterialMovement", "0", "IssuedQty ", SOMaterialMovement.IssuedQty);
            //    DT1.Rows.Add("Procurement.SOMaterialMovement", "0", "ReturnQty", SOMaterialMovement.ReturnQty);
            //    DT1.Rows.Add("Procurement.SOMaterialMovement", "0", "INQty   ", SOMaterialMovement.INQty);

            //    DT1.Rows.Add("Procurement.SOMaterialMovement", "0", "Allocation ", SOMaterialMovement.Allocation);
            //    DT1.Rows.Add("Procurement.SOMaterialMovement", "0", "ReplacementQty ", SOMaterialMovement.ReplacementQty);
            //    DT1.Rows.Add("Procurement.SOMaterialMovement", "0", "Qty  ", SOMaterialMovement.Qty);

            //    //DT1.Rows.Add("Procurement.SOMaterialMovement", "0", "Field1", SOMaterialMovement.Field1);
            //    //DT1.Rows.Add("Procurement.SOMaterialMovement", "0", "Field2", SOMaterialMovement.Field2);
            //    //DT1.Rows.Add("Procurement.SOMaterialMovement", "0", "Field3", SOMaterialMovement.Field3);
            //    //DT1.Rows.Add("Procurement.SOMaterialMovement", "0", "Field4", SOMaterialMovement.Field4);
            //    //DT1.Rows.Add("Procurement.SOMaterialMovement", "0", "Field5", SOMaterialMovement.Field5);
            //    //DT1.Rows.Add("Procurement.SOMaterialMovement", "0", "Field6", SOMaterialMovement.Field6);
            //    //DT1.Rows.Add("Procurement.SOMaterialMovement", "0", "Field7", SOMaterialMovement.Field7);
            //    //DT1.Rows.Add("Procurement.SOMaterialMovement", "0", "Field8", SOMaterialMovement.Field8);
            //    //DT1.Rows.Add("Procurement.SOMaterialMovement", "0", "Field9", SOMaterialMovement.Field9);

            //    //DT2.Rows.Add("Procurement.ServiceOrder", "cond", "DocNumber", Docnum);
            //    //DT2.Rows.Add("Procurement.ServiceOrder", "set", "IsWithDetail", "True");

            //    Gears.CreateData(DT1);
            //    //Gears.UpdateData(DT2);

            //}
            //public void UpdateSOMaterialMovement(SOMaterialMovement SOMaterialMovement)
            //{
            //    int linenum = 0;

            //    Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            //    DT1.Rows.Add("Procurement.SOMaterialMovement", "cond", "DocNumber", Docnum);
            //    DT1.Rows.Add("Procurement.SOMaterialMovement", "cond", "LineNumber", SOMaterialMovement.LineNumber);
            //    DT1.Rows.Add("Procurement.SOMaterialMovement", "set", "ItemCode", SOMaterialMovement.ItemCode);
            //    DT1.Rows.Add("Procurement.SOMaterialMovement", "set", "ColorCode", SOMaterialMovement.ColorCode);
            //    DT1.Rows.Add("Procurement.SOMaterialMovement", "set", "ClassCode", SOMaterialMovement.ClassCode);
            //    DT1.Rows.Add("Procurement.SOMaterialMovement", "set", "SizeCode", SOMaterialMovement.SizeCode);
            //    DT1.Rows.Add("Procurement.SOMaterialMovement", "set", "Unit", SOMaterialMovement.Unit);
            //    DT1.Rows.Add("Procurement.SOMaterialMovement", "set", "IssuedQty ", SOMaterialMovement.IssuedQty);
            //    DT1.Rows.Add("Procurement.SOMaterialMovement", "set", "ReturnQty", SOMaterialMovement.ReturnQty);
            //    DT1.Rows.Add("Procurement.SOMaterialMovement", "set", "INQty   ", SOMaterialMovement.INQty);

            //    DT1.Rows.Add("Procurement.SOMaterialMovement", "set", "Allocation ", SOMaterialMovement.Allocation);
            //    DT1.Rows.Add("Procurement.SOMaterialMovement", "set", "ReplacementQty ", SOMaterialMovement.ReplacementQty);
            //    DT1.Rows.Add("Procurement.SOMaterialMovement", "set", "Qty  ", SOMaterialMovement.Qty);


            //    //DT1.Rows.Add("Procurement.SOMaterialMovement", "set", "Field1", SOMaterialMovement.Field1);
            //    //DT1.Rows.Add("Procurement.SOMaterialMovement", "set", "Field2", SOMaterialMovement.Field2);
            //    //DT1.Rows.Add("Procurement.SOMaterialMovement", "set", "Field3", SOMaterialMovement.Field3);
            //    //DT1.Rows.Add("Procurement.SOMaterialMovement", "set", "Field4", SOMaterialMovement.Field4);
            //    //DT1.Rows.Add("Procurement.SOMaterialMovement", "set", "Field5", SOMaterialMovement.Field5);
            //    //DT1.Rows.Add("Procurement.SOMaterialMovement", "set", "Field6", SOMaterialMovement.Field6);
            //    //DT1.Rows.Add("Procurement.SOMaterialMovement", "set", "Field7", SOMaterialMovement.Field7);
            //    //DT1.Rows.Add("Procurement.SOMaterialMovement", "set", "Field8", SOMaterialMovement.Field8);
            //    //DT1.Rows.Add("Procurement.SOMaterialMovement", "set", "Field9", SOMaterialMovement.Field9);

            //    Gears.UpdateData(DT1);




            //}
            //public void DeleteSOMaterialMovement(SOMaterialMovement SOMaterialMovement)
            //{

            //    Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            //    Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
            //    Gears.CRUDdatatable DT3 = new Gears.CRUDdatatable();
            //    DT1.Rows.Add("Procurement.SOMaterialMovement", "cond", "DocNumber", SOMaterialMovement.DocNumber);
            //    DT1.Rows.Add("Procurement.SOMaterialMovement", "cond", "LineNumber", SOMaterialMovement.LineNumber);


            //    Gears.DeleteData(DT1);


            //    //DataTable count = Gears.RetriveData2("select * from  SOMaterialMovement where docnumber = '" + Docnum + "'");

            //    //if (count.Rows.Count < 1)
            //    //{
            //    //    DT2.Rows.Add("Procurement.ServiceOrder", "cond", "DocNumber", Docnum);
            //    //    DT2.Rows.Add("Procurement.ServiceOrder", "set", "IsWithDetail", "False");
            //    //    Gears.UpdateData(DT2);
            //    //}


            //}




        }

        public class RefTransaction
        {
            public virtual ServiceOrder Parent { get; set; }
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
                                            + "  where (DocNumber='" + DocNumber + "' OR   REFDocNumber='" + DocNumber + "') and  (RTransType='SLSORD' OR  A.TransType='SLSORD') ", Conn);
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
            a = Gears.RetriveData2("select * from Procurement.ServiceOrder where DocNumber = '" + DocNumber + "'", Conn);
            foreach (DataRow dtRow in a.Rows)
            {
                DocNumber = dtRow["DocNumber"].ToString();
                DocDate = dtRow["Docdate"].ToString();
                DueDate = dtRow["TargetDeliveryDate"].ToString();
                CustomerCode = dtRow["CustomerCode"].ToString();
                ReferenceNo = dtRow["ReferenceNo"].ToString();
                ItemCode = dtRow["ItemCode"].ToString();
                ColorCode = dtRow["ColorCode"].ToString();
                Status = dtRow["Status"].ToString();
                DateCompleted = dtRow["DateCompleted"].ToString();
                Remarks = dtRow["Remarks"].ToString();


                TotalSVOQty = Convert.ToDecimal(Convert.IsDBNull(dtRow["TotalSVOQty"]) ? 0 : dtRow["TotalSVOQty"]);
                TotalINQty = Convert.ToDecimal(Convert.IsDBNull(dtRow["TotalINQty"]) ? 0 : dtRow["TotalINQty"]);
                TotalFinalQty = Convert.ToDecimal(Convert.IsDBNull(dtRow["TotalFinalQty"]) ? 0 : dtRow["TotalFinalQty"]);
                TotalLabor = Convert.ToDecimal(Convert.IsDBNull(dtRow["TotalLabor"]) ? 0 : dtRow["TotalLabor"]);
                TotalRawMaterials = Convert.ToDecimal(Convert.IsDBNull(dtRow["TotalRawMaterials"]) ? 0 : dtRow["TotalRawMaterials"]);
                UnitCost = Convert.ToDecimal(Convert.IsDBNull(dtRow["UnitCost"]) ? 0 : dtRow["UnitCost"]);
                EstAccCost = Convert.ToDecimal(Convert.IsDBNull(dtRow["EstAccCost"]) ? 0 : dtRow["EstAccCost"]);
                EstUnitCost = Convert.ToDecimal(Convert.IsDBNull(dtRow["EstUnitCost"]) ? 0 : dtRow["EstUnitCost"]);
    
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
                ManualClosedBy = dtRow["ManualClosedBy"].ToString();
                ManualClosedDate = dtRow["ManualClosedDate"].ToString();

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
        public void InsertData(ServiceOrder _ent)
        {
            Docnum = _ent.DocNumber;
            Conn = _ent.Connection;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add("Procurement.ServiceOrder", "0", "DocNumber", _ent.DocNumber);
            DT1.Rows.Add("Procurement.ServiceOrder", "0", "DocDate", _ent.DocDate);
            DT1.Rows.Add("Procurement.ServiceOrder", "0", "TargetDeliveryDate  ", _ent.DueDate );

            DT1.Rows.Add("Procurement.ServiceOrder", "0", "ReferenceNo  ", _ent.ReferenceNo);

            DT1.Rows.Add("Procurement.ServiceOrder", "0", "CustomerCode ", _ent.CustomerCode);
            DT1.Rows.Add("Procurement.ServiceOrder", "0", "ItemCode ", _ent.ItemCode );
            DT1.Rows.Add("Procurement.ServiceOrder", "0", "ColorCode ", _ent.ColorCode);
            DT1.Rows.Add("Procurement.ServiceOrder", "0", "Status ", _ent.Status);


            DT1.Rows.Add("Procurement.ServiceOrder", "0", "Remarks ", _ent.Remarks);

            DT1.Rows.Add("Procurement.ServiceOrder", "0", "TotalSVOQty ", _ent.TotalSVOQty);


            DT1.Rows.Add("Procurement.ServiceOrder", "0", "EstAccCost ", _ent.EstAccCost);
            DT1.Rows.Add("Procurement.ServiceOrder", "0", "EstUnitCost ", _ent.EstUnitCost);

            DT1.Rows.Add("Procurement.ServiceOrder", "0", "Field1", _ent.Field1);
            DT1.Rows.Add("Procurement.ServiceOrder", "0", "Field2", _ent.Field2);
            DT1.Rows.Add("Procurement.ServiceOrder", "0", "Field3", _ent.Field3);
            DT1.Rows.Add("Procurement.ServiceOrder", "0", "Field4", _ent.Field4);
            DT1.Rows.Add("Procurement.ServiceOrder", "0", "Field5", _ent.Field5);
            DT1.Rows.Add("Procurement.ServiceOrder", "0", "Field6", _ent.Field6);
            DT1.Rows.Add("Procurement.ServiceOrder", "0", "Field7", _ent.Field7);
            DT1.Rows.Add("Procurement.ServiceOrder", "0", "Field8", _ent.Field8);
            DT1.Rows.Add("Procurement.ServiceOrder", "0", "Field9", _ent.Field9);
            DT1.Rows.Add("Procurement.ServiceOrder", "0", "SubmittedBy","");
            DT1.Rows.Add("Procurement.ServiceOrder", "0", "IsWithDetail", "False");


            Gears.CreateData(DT1, _ent.Connection);

        }

        public void UpdateData(ServiceOrder _ent)
        {
            Docnum = _ent.DocNumber;
            Conn = _ent.Connection;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Procurement.ServiceOrder", "cond", "DocNumber", _ent.DocNumber);

            DT1.Rows.Add("Procurement.ServiceOrder", "set", "DocDate", _ent.DocDate);
            DT1.Rows.Add("Procurement.ServiceOrder", "set", "TargetDeliveryDate  ", _ent.DueDate );
            DT1.Rows.Add("Procurement.ServiceOrder", "set", "ReferenceNo  ", _ent.ReferenceNo);

            DT1.Rows.Add("Procurement.ServiceOrder", "set", "CustomerCode ", _ent.CustomerCode);
            DT1.Rows.Add("Procurement.ServiceOrder", "set", "ItemCode ", _ent.ItemCode );
            DT1.Rows.Add("Procurement.ServiceOrder", "set", "ColorCode ", _ent.ColorCode);
            DT1.Rows.Add("Procurement.ServiceOrder", "set", "Status ", _ent.Status);


            DT1.Rows.Add("Procurement.ServiceOrder", "set", "Remarks ", _ent.Remarks);

            DT1.Rows.Add("Procurement.ServiceOrder", "set", "TotalSVOQty ", _ent.TotalSVOQty);

   
            DT1.Rows.Add("Procurement.ServiceOrder", "set", "EstAccCost ", _ent.EstAccCost);
            DT1.Rows.Add("Procurement.ServiceOrder", "set", "EstUnitCost ", _ent.EstUnitCost);

            DT1.Rows.Add("Procurement.ServiceOrder", "set", "Field1", _ent.Field1);
            DT1.Rows.Add("Procurement.ServiceOrder", "set", "Field2", _ent.Field2);
            DT1.Rows.Add("Procurement.ServiceOrder", "set", "Field3", _ent.Field3);
            DT1.Rows.Add("Procurement.ServiceOrder", "set", "Field4", _ent.Field4);
            DT1.Rows.Add("Procurement.ServiceOrder", "set", "Field5", _ent.Field5);
            DT1.Rows.Add("Procurement.ServiceOrder", "set", "Field6", _ent.Field6);
            DT1.Rows.Add("Procurement.ServiceOrder", "set", "Field7", _ent.Field7);
            DT1.Rows.Add("Procurement.ServiceOrder", "set", "Field8", _ent.Field8);
            DT1.Rows.Add("Procurement.ServiceOrder", "set", "Field9", _ent.Field9);
            DT1.Rows.Add("Procurement.ServiceOrder", "set", "SubmittedBy", "");
            DT1.Rows.Add("Procurement.ServiceOrder", "set", "LastEditedBy", _ent.LastEditedBy);
            DT1.Rows.Add("Procurement.ServiceOrder", "set", "LastEditedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            Gears.UpdateData(DT1, _ent.Connection);

            Functions.AuditTrail("PRCSCO", Docnum, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "UPDATE", _ent.Connection);
        }

        public void DeleteData(ServiceOrder _ent)
        {
            Docnum = _ent.DocNumber;
            Conn = _ent.Connection;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Procurement.ServiceOrder", "cond", "DocNumber", _ent.DocNumber);
            Gears.DeleteData(DT1, _ent.Connection);
            Functions.AuditTrail("PRCSCO", Docnum, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "DELETE", _ent.Connection);
        }
    

        }
}
