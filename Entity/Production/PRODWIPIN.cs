using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GearsLibrary;
using System.Data;

namespace Entity
{
    public class PRODWIPIN
    {
        private static string Docnum;

        private static string Conn;//ADD CONN
        public virtual string Connection { get; set; }//ADD CONN
        public virtual string DocNumber { get; set; }
        public virtual string DocDate { get; set; }
        public virtual string Type { get; set; }
     
        public virtual string JobOrder { get; set; }
        public virtual string Step { get; set; }
        public virtual string WorkCenter { get; set; }

        public virtual decimal TotalQuantity { get; set; }
        public virtual string DRDocNumber { get; set; }
        public virtual string Remarks { get; set; }

        public virtual string MemoRemarks { get; set; }

        public virtual decimal NumStrands { get; set; }
        public virtual decimal glStuffingMach { get; set; }
        public virtual string txtWeightSmokecart { get; set; }
        public virtual string txtWeightbefore { get; set; }
        public virtual bool cbQCStick { get; set; }
        public virtual bool cbQCHotdog { get; set; }
        public virtual bool cbQCfreefrom { get; set; }

        public virtual string Shift { get; set; }

        public virtual string TimeOn { get; set; }
        public virtual string TimeOff { get; set; }
        public virtual string txtBlastTemp { get; set; }
        public virtual string txtProductName { get; set; }
        public virtual string txtPD { get; set; }

        public virtual decimal NumPacks { get; set; }

        public virtual string txtLoadedBy { get; set; }
        public virtual string txtMonitoredBy { get; set; }
        public virtual string txtCheckedBy { get; set; }
        public virtual string txtMachSpeed { get; set; }
        
        public virtual decimal glSpiralMach { get; set; }
        public virtual decimal txtBlastMachine { get; set; }

        public virtual decimal QtyPacks { get; set; }
        public virtual decimal QtyLoosePacks { get; set; }

        public virtual string TimeStarted { get; set; }
        public virtual string TimeFinished { get; set; }

        public virtual decimal IntTempPL { get; set; }
        public virtual decimal StdRoomTemp { get; set; }

        public virtual string txtQAVSpiral { get; set; }
        public virtual string txtQAVPLoad { get; set; }
        public virtual string txtQAVValBy { get; set; }





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


        //2021-07-23    emc add field for Toll Project
        public virtual int DayNo { get; set; }
        public virtual int Year { get; set; }
        public virtual int WorkWeek { get; set; }
        public virtual string SKUcode { get; set; }
        public virtual string BatchNo { get; set; }
        public virtual string Machine { get; set; }
        public virtual decimal PlanQty { get; set; }
        public virtual decimal ActualQty { get; set; }
        public virtual string ProdSite { get; set; }
        public virtual string StepSeq { get; set; }


        public virtual IList<WIClassBreakDown> Detail { get; set; }


        public class WIClassBreakDown
        {

            public virtual PRODWIPIN Parent { get; set; }
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
                    a = Gears.RetriveData2("select * from Procurement.WIClassBreakdown where DocNumber='" + DocNumber + "' order by LineNumber", Conn);
                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }
            public void AddWIClassBreakDown(WIClassBreakDown WIClassBreakdown)
            {

                int linenum = 0;


                DataTable count = Gears.RetriveData2("select max(convert(int,LineNumber)) as LineNumber from Procurement.WIClassBreakdown where docnumber = '" + Docnum + "'", Conn);

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

                DT1.Rows.Add("Production.WIClassBreakdown", "0", "DocNumber", Docnum);
                DT1.Rows.Add("Procurement.WIClassBreakdown", "0", "LineNumber", strLine);
                DT1.Rows.Add("Procurement.WIClassBreakdown", "0", "ClassCode", WIClassBreakdown.ClassCode);
                DT1.Rows.Add("Procurement.WIClassBreakdown", "0", "Quantity", WIClassBreakdown.Quantity);


                DT1.Rows.Add("Procurement.WIClassBreakdown", "0", "Field1", WIClassBreakdown.Field1);
                DT1.Rows.Add("Procurement.WIClassBreakdown", "0", "Field2", WIClassBreakdown.Field2);
                DT1.Rows.Add("Procurement.WIClassBreakdown", "0", "Field3", WIClassBreakdown.Field3);
                DT1.Rows.Add("Procurement.WIClassBreakdown", "0", "Field4", WIClassBreakdown.Field4);
                DT1.Rows.Add("Procurement.WIClassBreakdown", "0", "Field5", WIClassBreakdown.Field5);
                DT1.Rows.Add("Procurement.WIClassBreakdown", "0", "Field6", WIClassBreakdown.Field6);
                DT1.Rows.Add("Procurement.WIClassBreakdown", "0", "Field7", WIClassBreakdown.Field7);
                DT1.Rows.Add("Procurement.WIClassBreakdown", "0", "Field8", WIClassBreakdown.Field8);
                DT1.Rows.Add("Procurement.WIClassBreakdown", "0", "Field9", WIClassBreakdown.Field9);



                //DT2.Rows.Add("Procurement.WipOUT", "cond", "DocNumber", Docnum);
                //DT2.Rows.Add("Procurement.WipOUT", "set", "IsWithDetail", "True");

                Gears.CreateData(DT1, Conn);
                //Gears.UpdateData(DT2);

            }
            public void UpdateWIClassBreakDown(WIClassBreakDown WIClassBreakdown)
            {
                int linenum = 0;

                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

                DT1.Rows.Add("Procurement.WIClassBreakdown", "cond", "DocNumber", Docnum);
                DT1.Rows.Add("Procurement.WIClassBreakdown", "cond", "LineNumber", WIClassBreakdown.LineNumber);
                DT1.Rows.Add("Procurement.WIClassBreakdown", "set", "ClassCode", WIClassBreakdown.ClassCode);
                DT1.Rows.Add("Procurement.WIClassBreakdown", "set", "Quantity", WIClassBreakdown.Quantity);



                DT1.Rows.Add("Procurement.WIClassBreakdown", "set", "Field1", WIClassBreakdown.Field1);
                DT1.Rows.Add("Procurement.WIClassBreakdown", "set", "Field2", WIClassBreakdown.Field2);
                DT1.Rows.Add("Procurement.WIClassBreakdown", "set", "Field3", WIClassBreakdown.Field3);
                DT1.Rows.Add("Procurement.WIClassBreakdown", "set", "Field4", WIClassBreakdown.Field4);
                DT1.Rows.Add("Procurement.WIClassBreakdown", "set", "Field5", WIClassBreakdown.Field5);
                DT1.Rows.Add("Procurement.WIClassBreakdown", "set", "Field6", WIClassBreakdown.Field6);
                DT1.Rows.Add("Procurement.WIClassBreakdown", "set", "Field7", WIClassBreakdown.Field7);
                DT1.Rows.Add("Procurement.WIClassBreakdown", "set", "Field8", WIClassBreakdown.Field8);
                DT1.Rows.Add("Procurement.WIClassBreakdown", "set", "Field9", WIClassBreakdown.Field9);

                Gears.UpdateData(DT1, Conn);




            }
            public void DeleteWIClassBreakDown(WIClassBreakDown WIClassBreakdown)
            {

                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT3 = new Gears.CRUDdatatable();
                DT1.Rows.Add("Procurement.WIClassBreakdown", "cond", "DocNumber", WIClassBreakdown.DocNumber);
                DT1.Rows.Add("Procurement.WIClassBreakdown", "cond", "LineNumber", WIClassBreakdown.LineNumber);


                Gears.DeleteData(DT1, Conn);


                //DataTable count = Gears.RetriveData2("select * from Procurement.WIClassBreakdown where docnumber = '" + Docnum + "'");

                //if (count.Rows.Count < 1)
                //{
                //    DT2.Rows.Add("Procurement.WipOUT", "cond", "DocNumber", Docnum);
                //    DT2.Rows.Add("Procurement.WipOUT", "set", "IsWithDetail", "False");
                //    Gears.UpdateData(DT2);
                //}


            }


        


        }
        public class RefTransaction
        {
            public virtual PRODWIPIN Parent { get; set; }
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
                    a = Gears.RetriveData2("select RTransType,REFDocNumber,RMenuID,RIGHT(B.CommandString, LEN(B.CommandString) - 1) as RCommandString,A.TransType,DocNumber,A.MenuID,RIGHT(C.CommandString, LEN(C.CommandString) - 1) as CommandString from  IT.ReferenceTrans  A "
                                            + " inner join IT.MainMenu B"
                                            + " on A.RMenuID =B.ModuleID "
                                            + " inner join IT.MainMenu C "
                                            + " on A.MenuID = C.ModuleID "
                                            + "  where (DocNumber='" + DocNumber + "' OR   REFDocNumber='" + DocNumber + "') and  (RTransType='PRDWIN' OR  A.TransType='PRDWIN') ", Conn);
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
            a = Gears.RetriveData2("select * from Production.WipIN where DocNumber = '" + DocNumber + "'", Conn);
            foreach (DataRow dtRow in a.Rows)
            {
                DocNumber = dtRow["DocNumber"].ToString();
                DocDate = dtRow["Docdate"].ToString();
                Type = dtRow["Type"].ToString();
                JobOrder = dtRow["JobOrder"].ToString();
                Step = dtRow["Step"].ToString();
                WorkCenter = dtRow["WorkCenter"].ToString();

                TotalQuantity = Convert.ToDecimal(Convert.IsDBNull(dtRow["TotalQuantity"]) ? 0 : dtRow["TotalQuantity"]);
                DRDocNumber = dtRow["DRDocNumber"].ToString();
                Remarks = dtRow["Remarks"].ToString();
                MemoRemarks = dtRow["MemoRemarks"].ToString();
                


                //2021-07-23    EMC
                DayNo = Convert.ToInt32(Convert.IsDBNull(dtRow["DayNo"]) ? 0 : dtRow["DayNo"]);
                Year = Convert.ToInt32(Convert.IsDBNull(dtRow["Year"]) ? 0 : dtRow["Year"]);
                WorkWeek = Convert.ToInt32(Convert.IsDBNull(dtRow["WorkWeek"]) ? 0 : dtRow["WorkWeek"]);
                SKUcode = dtRow["SKUcode"].ToString();
                BatchNo = dtRow["BatchNo"].ToString();
                //BatchNo = Convert.ToDecimal(Convert.IsDBNull(dtRow["BatchNo"]) ? 0 : dtRow["BatchNo"]);
                Machine = dtRow["Machine"].ToString();
                PlanQty = Convert.ToDecimal(Convert.IsDBNull(dtRow["PlanQty"]) ? 0 : dtRow["PlanQty"]);
                ActualQty = Convert.ToDecimal(Convert.IsDBNull(dtRow["ActualQty"]) ? 0 : dtRow["ActualQty"]);
                ProdSite = dtRow["ProdSite"].ToString();
                StepSeq = dtRow["StepSeq"].ToString();
                NumStrands = Convert.ToDecimal(Convert.IsDBNull(dtRow["NumStrands"]) ? 0 : dtRow["NumStrands"]);
                //glStuffingMach = dtRow["glStuffingMach"].ToString();
                glStuffingMach = Convert.ToDecimal(Convert.IsDBNull(dtRow["glStuffingMach"]) ? 0 : dtRow["glStuffingMach"]);
                txtWeightSmokecart = dtRow["txtWeightSmokecart"].ToString();
                txtWeightbefore = dtRow["txtWeightbefore"].ToString();
                cbQCStick = Convert.ToBoolean(dtRow["cbQCStick"]);
                cbQCHotdog = Convert.ToBoolean(dtRow["cbQCHotdog"]);
                cbQCfreefrom = Convert.ToBoolean(dtRow["cbQCfreefrom"]);
                Shift = dtRow["Shift"].ToString();

                TimeOn = dtRow["TimeOn"].ToString();
                TimeOff = dtRow["TimeOff"].ToString();
                txtBlastTemp = dtRow["txtBlastTemp"].ToString();
                txtProductName = dtRow["txtProductName"].ToString();
                txtPD = dtRow["txtPD"].ToString();
                NumPacks = Convert.ToDecimal(Convert.IsDBNull(dtRow["NumPacks"]) ? 0 : dtRow["NumPacks"]);
                txtLoadedBy = dtRow["txtLoadedBy"].ToString();
                txtMonitoredBy = dtRow["txtMonitoredBy"].ToString();
                txtCheckedBy = dtRow["txtCheckedBy"].ToString();
                txtMachSpeed = dtRow["txtMachSpeed"].ToString();
                
                //glSpiralMach = dtRow["glSpiralMach"].ToString();
                glSpiralMach = Convert.ToDecimal(Convert.IsDBNull(dtRow["glSpiralMach"]) ? 0 : dtRow["glSpiralMach"]);
                txtBlastMachine = Convert.ToDecimal(Convert.IsDBNull(dtRow["txtBlastMachine"]) ? 0 : dtRow["txtBlastMachine"]);
                QtyPacks = Convert.ToDecimal(Convert.IsDBNull(dtRow["QtyPacks"]) ? 0 : dtRow["QtyPacks"]);
                QtyLoosePacks = Convert.ToDecimal(Convert.IsDBNull(dtRow["QtyLoosePacks"]) ? 0 : dtRow["QtyLoosePacks"]);
                TimeStarted = dtRow["TimeStarted"].ToString();
                //TimeStarted = Convert.ToDateTime(Convert.IsDBNull(dtRow["TimeStarted"]) ? 0 : dtRow["TimeStarted"]);
                TimeFinished = dtRow["TimeFinished"].ToString();
                //TimeFinished = Convert.ToDateTime(Convert.IsDBNull(dtRow["TimeFinished"]) ? 0 : dtRow["TimeFinished"]);
                IntTempPL = Convert.ToDecimal(Convert.IsDBNull(dtRow["IntTempPL"]) ? 0 : dtRow["IntTempPL"]);
                StdRoomTemp = Convert.ToDecimal(Convert.IsDBNull(dtRow["StdRoomTemp"]) ? 0 : dtRow["StdRoomTemp"]);
                txtQAVSpiral = dtRow["txtQAVSpiral"].ToString();
                txtQAVPLoad = dtRow["txtQAVPLoad"].ToString();
                txtQAVValBy = dtRow["txtQAVValBy"].ToString();





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
        public void InsertData(PRODWIPIN _ent)
        {
            Docnum = _ent.DocNumber;
            Conn = _ent.Connection;     //ADD CONN

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();


            DT1.Rows.Add("Production.WipIN", "0", "DocNumber", _ent.DocNumber);
            DT1.Rows.Add("Production.WipIN", "0", "DocDate", _ent.DocDate);
            DT1.Rows.Add("Production.WipIN", "0", "Type  ", _ent.Type);
            
            DT1.Rows.Add("Production.WipIN", "0", "JobOrder ", _ent.JobOrder);

            DT1.Rows.Add("Production.WipIN", "0", "Step ", _ent.Step);
            DT1.Rows.Add("Production.WipIN", "0", "WorkCenter ", _ent.WorkCenter);
            DT1.Rows.Add("Production.WipIN", "0", "TotalQuantity ", _ent.TotalQuantity);
 
            DT1.Rows.Add("Production.WipIN", "0", "Remarks ", _ent.Remarks);
            DT1.Rows.Add("Production.WipIN", "0", "MemoRemarks ", _ent.MemoRemarks);


            //2021-07-23   emc ADD code
            DT1.Rows.Add("Production.WipIN", "0", "DayNo", _ent.DayNo);
            DT1.Rows.Add("Production.WipIN", "0", "Year", _ent.Year);
            DT1.Rows.Add("Production.WipIN", "0", "WorkWeek", _ent.WorkWeek);
            DT1.Rows.Add("Production.WipIN", "0", "SKUcode", _ent.SKUcode);
            DT1.Rows.Add("Production.WipIN", "0", "BatchNo", _ent.BatchNo);
            DT1.Rows.Add("Production.WipIN", "0", "Machine", _ent.Machine);
            DT1.Rows.Add("Production.WipIN", "0", "PlanQty", _ent.PlanQty);
            DT1.Rows.Add("Production.WipIN", "0", "ActualQty", _ent.ActualQty);
            DT1.Rows.Add("Production.WipIN", "0", "ProdSite", _ent.ProdSite);
            DT1.Rows.Add("Production.WipIN", "0", "StepSeq", _ent.StepSeq);
            DT1.Rows.Add("Production.WipIN", "0", "NumStrands", _ent.NumStrands);
            DT1.Rows.Add("Production.WipIN", "0", "glStuffingMach", _ent.glStuffingMach);
            DT1.Rows.Add("Production.WipIN", "0", "txtWeightSmokecart", _ent.txtWeightSmokecart);
            DT1.Rows.Add("Production.WipIN", "0", "txtWeightbefore", _ent.txtWeightbefore);
            DT1.Rows.Add("Production.WipIN", "0", "cbQCStick", _ent.cbQCStick);
            DT1.Rows.Add("Production.WipIN", "0", "cbQCHotdog", _ent.cbQCHotdog);
            DT1.Rows.Add("Production.WipIN", "0", "cbQCfreefrom", _ent.cbQCfreefrom);
            DT1.Rows.Add("Production.WipIN", "0", "Shift", _ent.Shift);
            DT1.Rows.Add("Production.WipIN", "0", "TimeOn", _ent.TimeOn);
            DT1.Rows.Add("Production.WipIN", "0", "TimeOff", _ent.TimeOff);
            DT1.Rows.Add("Production.WipIN", "0", "txtBlastTemp", _ent.txtBlastTemp);
            DT1.Rows.Add("Production.WipIN", "0", "txtProductName", _ent.txtProductName);
            DT1.Rows.Add("Production.WipIN", "0", "NumPacks", _ent.NumPacks);
            DT1.Rows.Add("Production.WipIN", "0", "txtLoadedBy", _ent.txtLoadedBy);
            DT1.Rows.Add("Production.WipIN", "0", "txtMonitoredBy", _ent.txtMonitoredBy);
            DT1.Rows.Add("Production.WipIN", "0", "txtCheckedBy", _ent.txtCheckedBy);
            DT1.Rows.Add("Production.WipIN", "0", "txtMachSpeed", _ent.txtMachSpeed);
            DT1.Rows.Add("Production.WipIN", "0", "glSpiralMach", _ent.glSpiralMach);
            DT1.Rows.Add("Production.WipIN", "0", "txtBlastMachine", _ent.txtBlastMachine);
            DT1.Rows.Add("Production.WipIN", "0", "QtyPacks", _ent.QtyPacks);
            DT1.Rows.Add("Production.WipIN", "0", "QtyLoosePacks", _ent.QtyLoosePacks);
            DT1.Rows.Add("Production.WipIN", "0", "TimeStarted", _ent.TimeStarted);
            DT1.Rows.Add("Production.WipIN", "0", "TimeFinished", _ent.TimeFinished);
            DT1.Rows.Add("Production.WipIN", "0", "IntTempPL", _ent.IntTempPL);
            DT1.Rows.Add("Production.WipIN", "0", "StdRoomTemp", _ent.StdRoomTemp);
            DT1.Rows.Add("Production.WipIN", "0", "txtQAVSpiral", _ent.txtQAVSpiral);
            DT1.Rows.Add("Production.WipIN", "0", "txtQAVPLoad", _ent.txtQAVPLoad);
            DT1.Rows.Add("Production.WipIN", "0", "txtQAVValBy", _ent.txtQAVValBy);
      


            DT1.Rows.Add("Production.WipIN", "0", "Field1", _ent.Field1);
            DT1.Rows.Add("Production.WipIN", "0", "Field2", _ent.Field2);
            DT1.Rows.Add("Production.WipIN", "0", "Field3", _ent.Field3);
            DT1.Rows.Add("Production.WipIN", "0", "Field4", _ent.Field4);
            DT1.Rows.Add("Production.WipIN", "0", "Field5", _ent.Field5);
            DT1.Rows.Add("Production.WipIN", "0", "Field6", _ent.Field6);
            DT1.Rows.Add("Production.WipIN", "0", "Field7", _ent.Field7);
            DT1.Rows.Add("Production.WipIN", "0", "Field8", _ent.Field8);
            DT1.Rows.Add("Production.WipIN", "0", "Field9", _ent.Field9);
            DT1.Rows.Add("Production.WipIN", "0", "SubmittedBy", "");



            Gears.CreateData(DT1, _ent.Connection);

        }

        public void UpdateData(PRODWIPIN _ent)
        {
            Docnum = _ent.DocNumber;
            Conn = _ent.Connection;     //ADD CONN

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Production.WipIN", "cond", "DocNumber", _ent.DocNumber);

            DT1.Rows.Add("Production.WipIN", "set", "DocDate", _ent.DocDate);
            DT1.Rows.Add("Production.WipIN", "set", "Type  ", _ent.Type);
            DT1.Rows.Add("Production.WipIN", "set", "JobOrder ", _ent.JobOrder);
            DT1.Rows.Add("Production.WipIN", "set", "Step ", _ent.Step);
            DT1.Rows.Add("Production.WipIN", "set", "WorkCenter ", _ent.WorkCenter);

            DT1.Rows.Add("Production.WipIN", "set", "TotalQuantity ", _ent.TotalQuantity);
            DT1.Rows.Add("Production.WipIN", "set", "DRDocNumber ", _ent.DRDocNumber);
            DT1.Rows.Add("Production.WipIN", "set", "Remarks ", _ent.Remarks);
            DT1.Rows.Add("Production.WipIN", "set", "MemoRemarks ", _ent.MemoRemarks);

            //2021-07-23   EMC ADD code
            DT1.Rows.Add("Production.WipIN", "set", "DayNo", _ent.DayNo);
            DT1.Rows.Add("Production.WipIN", "set", "Year", _ent.Year);
            DT1.Rows.Add("Production.WipIN", "set", "WorkWeek", _ent.WorkWeek);
            DT1.Rows.Add("Production.WipIN", "set", "SKUcode", _ent.SKUcode);
            DT1.Rows.Add("Production.WipIN", "set", "BatchNo", _ent.BatchNo);
            DT1.Rows.Add("Production.WipIN", "set", "Machine", _ent.Machine);
            DT1.Rows.Add("Production.WipIN", "set", "PlanQty", _ent.PlanQty);
            DT1.Rows.Add("Production.WipIN", "set", "ActualQty", _ent.ActualQty);
            DT1.Rows.Add("Production.WipIN", "set", "ProdSite", _ent.ProdSite);
            DT1.Rows.Add("Production.WipIN", "set", "StepSeq", _ent.StepSeq);
            DT1.Rows.Add("Production.WipIN", "set", "NumStrands", _ent.NumStrands);
            DT1.Rows.Add("Production.WipIN", "set", "glStuffingMach", _ent.glStuffingMach);
            DT1.Rows.Add("Production.WipIN", "set", "txtWeightSmokecart", _ent.txtWeightSmokecart);
            DT1.Rows.Add("Production.WipIN", "set", "txtWeightbefore", _ent.txtWeightbefore);
            DT1.Rows.Add("Production.WipIN", "set", "cbQCStick", _ent.cbQCStick);
            DT1.Rows.Add("Production.WipIN", "set", "cbQCHotdog", _ent.cbQCHotdog);
            DT1.Rows.Add("Production.WipIN", "set", "cbQCfreefrom", _ent.cbQCfreefrom);
            DT1.Rows.Add("Production.WipIN", "set", "Shift", _ent.Shift);
            DT1.Rows.Add("Production.WipIN", "set", "TimeOn", _ent.TimeOn);
            DT1.Rows.Add("Production.WipIN", "set", "TimeOff", _ent.TimeOff);
            DT1.Rows.Add("Production.WipIN", "set", "txtBlastTemp", _ent.txtBlastTemp);
            DT1.Rows.Add("Production.WipIN", "set", "txtProductName", _ent.txtProductName);
            DT1.Rows.Add("Production.WipIN", "set", "txtPD", _ent.txtPD);
            DT1.Rows.Add("Production.WipIN", "set", "NumPacks", _ent.NumPacks);
            DT1.Rows.Add("Production.WipIN", "set", "txtLoadedBy", _ent.txtLoadedBy);
            DT1.Rows.Add("Production.WipIN", "set", "txtMonitoredBy", _ent.txtMonitoredBy);
            DT1.Rows.Add("Production.WipIN", "set", "txtCheckedBy", _ent.txtCheckedBy);
            DT1.Rows.Add("Production.WipIN", "set", "txtMachSpeed", _ent.txtMachSpeed);
            DT1.Rows.Add("Production.WipIN", "set", "glSpiralMach", _ent.glSpiralMach);
            DT1.Rows.Add("Production.WipIN", "set", "txtBlastMachine", _ent.txtBlastMachine);
            DT1.Rows.Add("Production.WipIN", "set", "QtyPacks", _ent.QtyPacks);
            DT1.Rows.Add("Production.WipIN", "set", "QtyLoosePacks", _ent.QtyLoosePacks);
            DT1.Rows.Add("Production.WipIN", "set", "TimeStarted", _ent.TimeStarted);
            DT1.Rows.Add("Production.WipIN", "set", "TimeFinished", _ent.TimeFinished);
            DT1.Rows.Add("Production.WipIN", "set", "IntTempPL", _ent.IntTempPL);
            DT1.Rows.Add("Production.WipIN", "set", "StdRoomTemp", _ent.StdRoomTemp);
            DT1.Rows.Add("Production.WipIN", "set", "txtQAVSpiral", _ent.txtQAVSpiral);
            DT1.Rows.Add("Production.WipIN", "set", "txtQAVPLoad", _ent.txtQAVPLoad);
            DT1.Rows.Add("Production.WipIN", "set", "txtQAVValBy", _ent.txtQAVValBy);


            DT1.Rows.Add("Production.WipIN", "set", "Field1", _ent.Field1);
            DT1.Rows.Add("Production.WipIN", "set", "Field2", _ent.Field2);
            DT1.Rows.Add("Production.WipIN", "set", "Field3", _ent.Field3);
            DT1.Rows.Add("Production.WipIN", "set", "Field4", _ent.Field4);
            DT1.Rows.Add("Production.WipIN", "set", "Field5", _ent.Field5);
            DT1.Rows.Add("Production.WipIN", "set", "Field6", _ent.Field6);
            DT1.Rows.Add("Production.WipIN", "set", "Field7", _ent.Field7);
            DT1.Rows.Add("Production.WipIN", "set", "Field8", _ent.NumStrands);
            DT1.Rows.Add("Production.WipIN", "set", "Field9", _ent.Field9);
            DT1.Rows.Add("Production.WipIN", "set", "SubmittedBy", "");
            DT1.Rows.Add("Production.WipIN", "set", "LastEditedBy", _ent.LastEditedBy);
            DT1.Rows.Add("Production.WipIN", "set", "LastEditedDate", _ent.LastEditedDate);
            Gears.UpdateData(DT1, _ent.Connection);

            Functions.AuditTrail("PRDWIN", Docnum, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "UPDATE", _ent.Connection);
        }

        public void DeleteData(PRODWIPIN _ent)
        {
            Docnum = _ent.DocNumber;
            Conn = _ent.Connection;     //ADD CONN

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Production.WipIN", "cond", "DocNumber", _ent.DocNumber);
            Gears.DeleteData(DT1, _ent.Connection);
            Functions.AuditTrail("PRDWIN", Docnum, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "DELETE", _ent.Connection);
        }
        public class JournalEntry
        {
            public virtual PRODWIPIN Parent { get; set; }
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
                    + " ProfitCenterCode AS ProfitCenter, CostCenterCode AS CostCenter, Convert(varchar,Convert(money,DebitAmount),1) AS Debit, Convert(varchar,Convert(money,CreditAmount),1) AS Credit,A.BizPartnerCode  FROM Accounting.GeneralLedger A "
                    + " INNER JOIN Accounting.ChartOfAccount B ON A.AccountCode = B.AccountCode "
                    + " INNER JOIN Accounting.GLSubsiCode C ON A.SubsiCode = C.SubsiCode "
                    + " AND A.AccountCode = C.AccountCode WHERE A.DocNumber = '" + DocNumber + "' AND (TransType ='PRDWIN') ", Conn);

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
