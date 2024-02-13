using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using GearsLibrary;

namespace Entity
{
    public class SAM
    {
        private static string Conn;
        private static string Docnum;
        public virtual string Connection { get; set; }
        public virtual string DocNumber { get; set; }
        public virtual string DocDate { get; set; }
        public virtual string SupplierCode { get; set; }
        public virtual string Name { get; set; }
        public virtual string DISNumber { get; set; }
        public virtual string Brand { get; set; }
        public virtual string Gender { get; set; }
        public virtual string ProductCategory { get; set; }
        public virtual string ProductSubCategory { get; set; }
        public virtual string ProductGroup { get; set; }
        public virtual string FitCode { get; set; }
        public virtual string ProductClass { get; set; }
        public virtual string DesignDesc { get; set; }
        public virtual string Remarks { get; set; }
        public virtual string FrontImage { get; set; }
        public virtual string BackImage { get; set; }
        public virtual decimal CMPCost { get; set; }
        public virtual decimal CuttingCost { get; set; }
        public virtual decimal SewingCost { get; set; }
        public virtual decimal FinishingCost { get; set; }
        public virtual decimal WashingCost { get; set; }
        public virtual decimal EmbroideryCost { get; set; }
        public virtual decimal PrintingCost { get; set; }
        public virtual decimal TotalCost { get; set; }
        public virtual decimal CEfficiency { get; set; }
        public virtual decimal CAllowance { get; set; }
        public virtual decimal CTotalObserved { get; set; }
        public virtual decimal CBasicMinutes { get; set; }
        public virtual decimal CSAM { get; set; }
        public virtual decimal CMinimumWage { get; set; }
        public virtual string CRegion { get; set; }
        public virtual decimal CLaborCost { get; set; }
        public virtual decimal CMarkup { get; set; }
        public virtual decimal CCost { get; set; }
        public virtual decimal SEfficiency { get; set; }
        public virtual decimal SAllowance { get; set; }
        public virtual decimal STotalObserved { get; set; }
        public virtual decimal SBasicMinutes { get; set; }
        public virtual decimal SSAM { get; set; }
        public virtual decimal SMinimumWage { get; set; }
        public virtual string SRegion { get; set; }
        public virtual decimal SLaborCost { get; set; }
        public virtual decimal SMarkup { get; set; }
        public virtual decimal SCost { get; set; }


        public virtual decimal FEfficiency { get; set; }
        public virtual decimal FAllowance { get; set; }
        public virtual decimal FTotalObserved { get; set; }
        public virtual decimal FBasicMinutes { get; set; }
        public virtual decimal FSAM { get; set; }
        public virtual decimal FMinimumWage { get; set; }
        public virtual string FRegion { get; set; }
        public virtual decimal FLaborCost { get; set; }
        public virtual decimal FMarkup { get; set; }
        public virtual decimal FCost { get; set; }
        public virtual decimal WEfficiency { get; set; }
        public virtual decimal WAllowance { get; set; }
        public virtual decimal WTotalObserved { get; set; }
        public virtual decimal WBasicMinutes { get; set; }
        public virtual decimal WSAM { get; set; }
        public virtual decimal WMinimumWage { get; set; }
        public virtual string WRegion { get; set; }
        public virtual decimal WLaborCost { get; set; }
        public virtual decimal WMarkup { get; set; }
        public virtual decimal WCost { get; set; }
        public virtual decimal EEfficiency { get; set; }
        public virtual decimal EAllowance { get; set; }
        public virtual decimal ETotalObserved { get; set; }
        public virtual decimal EBasicMinutes { get; set; }
        public virtual decimal ESAM { get; set; }
        public virtual decimal EMinimumWage { get; set; }
        public virtual string ERegion { get; set; }
        public virtual decimal ELaborCost { get; set; }
        public virtual decimal EMarkup { get; set; }
        public virtual decimal ECost { get; set; }
        public virtual decimal PEfficiency { get; set; }
        public virtual decimal PAllowance { get; set; }
        public virtual decimal PTotalObserved { get; set; }
        public virtual decimal PBasicMinutes { get; set; }
        public virtual decimal PSAM { get; set; }
        public virtual decimal PMinimumWage { get; set; }
        public virtual string PRegion { get; set; }
        public virtual decimal PLaborCost { get; set; }
        public virtual decimal PMarkup { get; set; }
        public virtual decimal PCost { get; set; }
        public virtual bool IsWithDetail { get; set; }
        public virtual bool IsValidated { get; set; }
        public virtual bool IsPrinted { get; set; }
        public virtual string CancelledBy { get; set; }
        public virtual string CancelledDate { get; set; }
        public virtual string AddedBy { get; set; }
        public virtual string AddedDate { get; set; }
        public virtual string LastEditedBy { get; set; }
        public virtual string LastEditedDate { get; set; }
        public virtual string SubmittedBy { get; set; }
        public virtual string SubmittedDate { get; set; }
        public virtual string ForceClosedBy { get; set; }
        public virtual string ForceClosedDate { get; set; }



        string strTranstype = "PRODSAM";

        public class SAMCutting
        {

            public virtual SAM Parent { get; set; }
            public virtual string DocNumber { get; set; }
            public virtual string LineNumber { get; set; }
            public virtual string Type { get; set; }
            public virtual string OperationCode { get; set; }
            public virtual string Steps { get; set; }
            public virtual string Parts { get; set; }
            public virtual string OpsBreakdown { get; set; }
            public virtual string MachineType { get; set; }
            public virtual decimal ObservedTime { get; set; }
            public virtual decimal SAM { get; set; }
            public virtual string Picture { get; set; }
            public virtual string Video { get; set; }
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
                    a = Gears.RetriveData2("select * from Production.SAMDetail where DocNumber='" + DocNumber + "' AND Type='C' order by OperationCode asc", Conn);
                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }
            public void AddSAMCutting(SAMCutting SAMCutting)
            {

                int linenum = 0;


                DataTable count = Gears.RetriveData2("select max(convert(int,LineNumber)) as LineNumber from Production.SAMDetail where docnumber = '" + Docnum + "' AND Type='C'", Conn);

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

                DT1.Rows.Add("Production.SAMDetail", "0", "DocNumber", Docnum);
                DT1.Rows.Add("Production.SAMDetail", "0", "LineNumber", strLine);
                DT1.Rows.Add("Production.SAMDetail", "0", "Type", "C");
                DT1.Rows.Add("Production.SAMDetail", "0", "OperationCode", SAMCutting.OperationCode);
                DT1.Rows.Add("Production.SAMDetail", "0", "Steps", SAMCutting.Steps);
                DT1.Rows.Add("Production.SAMDetail", "0", "Parts", SAMCutting.Parts);
                DT1.Rows.Add("Production.SAMDetail", "0", "OpsBreakdown", SAMCutting.OpsBreakdown);
                DT1.Rows.Add("Production.SAMDetail", "0", "MachineType", SAMCutting.MachineType);
                DT1.Rows.Add("Production.SAMDetail", "0", "ObservedTime", SAMCutting.ObservedTime);
                DT1.Rows.Add("Production.SAMDetail", "0", "SAM", SAMCutting.SAM);
                DT1.Rows.Add("Production.SAMDetail", "0", "Picture", SAMCutting.Picture);
                DT1.Rows.Add("Production.SAMDetail", "0", "Video", SAMCutting.Video);
                DT1.Rows.Add("Production.SAMDetail", "0", "Field1", SAMCutting.Field1);
                DT1.Rows.Add("Production.SAMDetail", "0", "Field2", SAMCutting.Field2);
                DT1.Rows.Add("Production.SAMDetail", "0", "Field3", SAMCutting.Field3);
                DT1.Rows.Add("Production.SAMDetail", "0", "Field4", SAMCutting.Field4);
                DT1.Rows.Add("Production.SAMDetail", "0", "Field5", SAMCutting.Field5);
                DT1.Rows.Add("Production.SAMDetail", "0", "Field6", SAMCutting.Field6);
                DT1.Rows.Add("Production.SAMDetail", "0", "Field7", SAMCutting.Field7);
                DT1.Rows.Add("Production.SAMDetail", "0", "Field8", SAMCutting.Field8);
                DT1.Rows.Add("Production.SAMDetail", "0", "Field9", SAMCutting.Field9); 

                Gears.CreateData(DT1, Conn);

            }
            public void UpdateSAMCutting(SAMCutting SAMCutting)
            {
                int linenum = 0;

                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();


                DT1.Rows.Add("Production.SAMDetail", "cond", "DocNumber", SAMCutting.DocNumber);
                DT1.Rows.Add("Production.SAMDetail", "cond", "LineNumber", SAMCutting.LineNumber);
                DT1.Rows.Add("Production.SAMDetail", "cond", "Type", "C");
                DT1.Rows.Add("Production.SAMDetail", "set", "OperationCode", SAMCutting.OperationCode);
                DT1.Rows.Add("Production.SAMDetail", "set", "Steps", SAMCutting.Steps);
                DT1.Rows.Add("Production.SAMDetail", "set", "Parts", SAMCutting.Parts);
                DT1.Rows.Add("Production.SAMDetail", "set", "OpsBreakdown", SAMCutting.OpsBreakdown);
                DT1.Rows.Add("Production.SAMDetail", "set", "MachineType", SAMCutting.MachineType);
                DT1.Rows.Add("Production.SAMDetail", "set", "ObservedTime", SAMCutting.ObservedTime);
                DT1.Rows.Add("Production.SAMDetail", "set", "SAM", SAMCutting.SAM);
                DT1.Rows.Add("Production.SAMDetail", "set", "Picture", SAMCutting.Picture);
                DT1.Rows.Add("Production.SAMDetail", "set", "Video", SAMCutting.Video);
                DT1.Rows.Add("Production.SAMDetail", "set", "Field1", SAMCutting.Field1);
                DT1.Rows.Add("Production.SAMDetail", "set", "Field2", SAMCutting.Field2);
                DT1.Rows.Add("Production.SAMDetail", "set", "Field3", SAMCutting.Field3);
                DT1.Rows.Add("Production.SAMDetail", "set", "Field4", SAMCutting.Field4);
                DT1.Rows.Add("Production.SAMDetail", "set", "Field5", SAMCutting.Field5);
                DT1.Rows.Add("Production.SAMDetail", "set", "Field6", SAMCutting.Field6);
                DT1.Rows.Add("Production.SAMDetail", "set", "Field7", SAMCutting.Field7);
                DT1.Rows.Add("Production.SAMDetail", "set", "Field8", SAMCutting.Field8);
                DT1.Rows.Add("Production.SAMDetail", "set", "Field9", SAMCutting.Field9); 

                Gears.UpdateData(DT1, Conn);

            }
            public void DeleteSAMCutting(SAMCutting SAMCutting)
            {

                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
       
                DT1.Rows.Add("Production.SAMDetail", "cond", "DocNumber", Docnum);
                DT1.Rows.Add("Production.SAMDetail", "cond", "LineNumber", SAMCutting.LineNumber);
                DT1.Rows.Add("Production.SAMDetail", "cond", "Type", "C");
                Gears.DeleteData(DT1, Conn);

            }


        }
        public class SAMSewing
        {

            public virtual SAM Parent { get; set; }
            public virtual string DocNumber { get; set; }
            public virtual string LineNumber { get; set; }
            public virtual string Type { get; set; }
            public virtual string OperationCode { get; set; }
            public virtual string Steps { get; set; }
            public virtual string Parts { get; set; }
            public virtual string OpsBreakdown { get; set; }
            public virtual string MachineType { get; set; }
            public virtual decimal ObservedTime { get; set; }
            public virtual decimal SAM { get; set; }
            public virtual string Picture { get; set; }
            public virtual string Video { get; set; }
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
                    a = Gears.RetriveData2("select * from Production.SAMDetail where DocNumber='" + DocNumber + "' AND Type='S' order by OperationCode asc", Conn);
                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }
            public void AddSAMSewing(SAMSewing SAMSewing)
            {

                int linenum = 0;


                DataTable count = Gears.RetriveData2("select max(convert(int,LineNumber)) as LineNumber from Production.SAMDetail where docnumber = '" + Docnum + "' AND Type='S'", Conn);

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

                DT1.Rows.Add("Production.SAMDetail", "0", "DocNumber", Docnum);
                DT1.Rows.Add("Production.SAMDetail", "0", "LineNumber", strLine);
                DT1.Rows.Add("Production.SAMDetail", "0", "Type", "S");
                DT1.Rows.Add("Production.SAMDetail", "0", "OperationCode", SAMSewing.OperationCode);
                DT1.Rows.Add("Production.SAMDetail", "0", "Steps", SAMSewing.Steps);
                DT1.Rows.Add("Production.SAMDetail", "0", "Parts", SAMSewing.Parts);
                DT1.Rows.Add("Production.SAMDetail", "0", "OpsBreakdown", SAMSewing.OpsBreakdown);
                DT1.Rows.Add("Production.SAMDetail", "0", "MachineType", SAMSewing.MachineType);
                DT1.Rows.Add("Production.SAMDetail", "0", "ObservedTime", SAMSewing.ObservedTime);
                DT1.Rows.Add("Production.SAMDetail", "0", "SAM", SAMSewing.SAM);
                DT1.Rows.Add("Production.SAMDetail", "0", "Picture", SAMSewing.Picture);
                DT1.Rows.Add("Production.SAMDetail", "0", "Video", SAMSewing.Video);
                DT1.Rows.Add("Production.SAMDetail", "0", "Field1", SAMSewing.Field1);
                DT1.Rows.Add("Production.SAMDetail", "0", "Field2", SAMSewing.Field2);
                DT1.Rows.Add("Production.SAMDetail", "0", "Field3", SAMSewing.Field3);
                DT1.Rows.Add("Production.SAMDetail", "0", "Field4", SAMSewing.Field4);
                DT1.Rows.Add("Production.SAMDetail", "0", "Field5", SAMSewing.Field5);
                DT1.Rows.Add("Production.SAMDetail", "0", "Field6", SAMSewing.Field6);
                DT1.Rows.Add("Production.SAMDetail", "0", "Field7", SAMSewing.Field7);
                DT1.Rows.Add("Production.SAMDetail", "0", "Field8", SAMSewing.Field8);
                DT1.Rows.Add("Production.SAMDetail", "0", "Field9", SAMSewing.Field9);

                Gears.CreateData(DT1, Conn);

            }
            public void UpdateSAMSewing(SAMSewing SAMSewing)
            {
                int linenum = 0;

                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();


                DT1.Rows.Add("Production.SAMDetail", "cond", "DocNumber", SAMSewing.DocNumber);
                DT1.Rows.Add("Production.SAMDetail", "cond", "LineNumber", SAMSewing.LineNumber);
                DT1.Rows.Add("Production.SAMDetail", "cond", "Type", "S");
                DT1.Rows.Add("Production.SAMDetail", "set", "OperationCode", SAMSewing.OperationCode);
                DT1.Rows.Add("Production.SAMDetail", "set", "Steps", SAMSewing.Steps);
                DT1.Rows.Add("Production.SAMDetail", "set", "Parts", SAMSewing.Parts);
                DT1.Rows.Add("Production.SAMDetail", "set", "OpsBreakdown", SAMSewing.OpsBreakdown);
                DT1.Rows.Add("Production.SAMDetail", "set", "MachineType", SAMSewing.MachineType);
                DT1.Rows.Add("Production.SAMDetail", "set", "ObservedTime", SAMSewing.ObservedTime);
                DT1.Rows.Add("Production.SAMDetail", "set", "SAM", SAMSewing.SAM);
                DT1.Rows.Add("Production.SAMDetail", "set", "Picture", SAMSewing.Picture);
                DT1.Rows.Add("Production.SAMDetail", "set", "Video", SAMSewing.Video);
                DT1.Rows.Add("Production.SAMDetail", "set", "Field1", SAMSewing.Field1);
                DT1.Rows.Add("Production.SAMDetail", "set", "Field2", SAMSewing.Field2);
                DT1.Rows.Add("Production.SAMDetail", "set", "Field3", SAMSewing.Field3);
                DT1.Rows.Add("Production.SAMDetail", "set", "Field4", SAMSewing.Field4);
                DT1.Rows.Add("Production.SAMDetail", "set", "Field5", SAMSewing.Field5);
                DT1.Rows.Add("Production.SAMDetail", "set", "Field6", SAMSewing.Field6);
                DT1.Rows.Add("Production.SAMDetail", "set", "Field7", SAMSewing.Field7);
                DT1.Rows.Add("Production.SAMDetail", "set", "Field8", SAMSewing.Field8);
                DT1.Rows.Add("Production.SAMDetail", "set", "Field9", SAMSewing.Field9);

                Gears.UpdateData(DT1, Conn);

            }
            public void DeleteSAMSewing(SAMSewing SAMSewing)
            {

                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

                DT1.Rows.Add("Production.SAMDetail", "cond", "DocNumber", SAMSewing.DocNumber);
                DT1.Rows.Add("Production.SAMDetail", "cond", "LineNumber", SAMSewing.LineNumber);
                DT1.Rows.Add("Production.SAMDetail", "cond", "Type", "S");
                Gears.DeleteData(DT1, Conn);

            }


        }
        public class SAMFinishing
        {

            public virtual SAM Parent { get; set; }
            public virtual string DocNumber { get; set; }
            public virtual string LineNumber { get; set; }
            public virtual string Type { get; set; }
            public virtual string OperationCode { get; set; }
            public virtual string Steps { get; set; }
            public virtual string Parts { get; set; }
            public virtual string OpsBreakdown { get; set; }
            public virtual string MachineType { get; set; }
            public virtual decimal ObservedTime { get; set; }
            public virtual decimal SAM { get; set; }
            public virtual string Picture { get; set; }
            public virtual string Video { get; set; }
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
                    a = Gears.RetriveData2("select * from Production.SAMDetail where DocNumber='" + DocNumber + "' AND Type='F' order by OperationCode asc", Conn);
                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }
            public void AddSAMFinishing(SAMFinishing SAMFinishing)
            {

                int linenum = 0;


                DataTable count = Gears.RetriveData2("select max(convert(int,LineNumber)) as LineNumber from Production.SAMDetail where docnumber = '" + Docnum + "' AND Type='F'", Conn);

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
                DT1.Rows.Add("Production.SAMDetail", "0", "DocNumber", Docnum);
                DT1.Rows.Add("Production.SAMDetail", "0", "LineNumber", strLine);
                DT1.Rows.Add("Production.SAMDetail", "0", "Type", "F");
                DT1.Rows.Add("Production.SAMDetail", "0", "OperationCode", SAMFinishing.OperationCode);
                DT1.Rows.Add("Production.SAMDetail", "0", "Steps", SAMFinishing.Steps);
                DT1.Rows.Add("Production.SAMDetail", "0", "Parts", SAMFinishing.Parts);
                DT1.Rows.Add("Production.SAMDetail", "0", "OpsBreakdown", SAMFinishing.OpsBreakdown);
                DT1.Rows.Add("Production.SAMDetail", "0", "MachineType", SAMFinishing.MachineType);
                DT1.Rows.Add("Production.SAMDetail", "0", "ObservedTime", SAMFinishing.ObservedTime);
                DT1.Rows.Add("Production.SAMDetail", "0", "SAM", SAMFinishing.SAM);
                DT1.Rows.Add("Production.SAMDetail", "0", "Picture", SAMFinishing.Picture);
                DT1.Rows.Add("Production.SAMDetail", "0", "Video", SAMFinishing.Video);
                DT1.Rows.Add("Production.SAMDetail", "0", "Field1", SAMFinishing.Field1);
                DT1.Rows.Add("Production.SAMDetail", "0", "Field2", SAMFinishing.Field2);
                DT1.Rows.Add("Production.SAMDetail", "0", "Field3", SAMFinishing.Field3);
                DT1.Rows.Add("Production.SAMDetail", "0", "Field4", SAMFinishing.Field4);
                DT1.Rows.Add("Production.SAMDetail", "0", "Field5", SAMFinishing.Field5);
                DT1.Rows.Add("Production.SAMDetail", "0", "Field6", SAMFinishing.Field6);
                DT1.Rows.Add("Production.SAMDetail", "0", "Field7", SAMFinishing.Field7);
                DT1.Rows.Add("Production.SAMDetail", "0", "Field8", SAMFinishing.Field8);
                DT1.Rows.Add("Production.SAMDetail", "0", "Field9", SAMFinishing.Field9);

                Gears.CreateData(DT1, Conn);

            }
            public void UpdateSAMFinishing(SAMFinishing SAMFinishing)
            {
                int linenum = 0;

                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();


                DT1.Rows.Add("Production.SAMDetail", "cond", "DocNumber", SAMFinishing.DocNumber);
                DT1.Rows.Add("Production.SAMDetail", "cond", "LineNumber", SAMFinishing.LineNumber);
                DT1.Rows.Add("Production.SAMDetail", "cond", "Type", "F");
                DT1.Rows.Add("Production.SAMDetail", "set", "OperationCode", SAMFinishing.OperationCode);
                DT1.Rows.Add("Production.SAMDetail", "set", "Steps", SAMFinishing.Steps);
                DT1.Rows.Add("Production.SAMDetail", "set", "Parts", SAMFinishing.Parts);
                DT1.Rows.Add("Production.SAMDetail", "set", "OpsBreakdown", SAMFinishing.OpsBreakdown);
                DT1.Rows.Add("Production.SAMDetail", "set", "MachineType", SAMFinishing.MachineType);
                DT1.Rows.Add("Production.SAMDetail", "set", "ObservedTime", SAMFinishing.ObservedTime);
                DT1.Rows.Add("Production.SAMDetail", "set", "SAM", SAMFinishing.SAM);
                DT1.Rows.Add("Production.SAMDetail", "set", "Picture", SAMFinishing.Picture);
                DT1.Rows.Add("Production.SAMDetail", "set", "Video", SAMFinishing.Video);
                DT1.Rows.Add("Production.SAMDetail", "set", "Field1", SAMFinishing.Field1);
                DT1.Rows.Add("Production.SAMDetail", "set", "Field2", SAMFinishing.Field2);
                DT1.Rows.Add("Production.SAMDetail", "set", "Field3", SAMFinishing.Field3);
                DT1.Rows.Add("Production.SAMDetail", "set", "Field4", SAMFinishing.Field4);
                DT1.Rows.Add("Production.SAMDetail", "set", "Field5", SAMFinishing.Field5);
                DT1.Rows.Add("Production.SAMDetail", "set", "Field6", SAMFinishing.Field6);
                DT1.Rows.Add("Production.SAMDetail", "set", "Field7", SAMFinishing.Field7);
                DT1.Rows.Add("Production.SAMDetail", "set", "Field8", SAMFinishing.Field8);
                DT1.Rows.Add("Production.SAMDetail", "set", "Field9", SAMFinishing.Field9);

                Gears.UpdateData(DT1, Conn);

            }
            public void DeleteSAMFinishing(SAMFinishing SAMFinishing)
            {

                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

                DT1.Rows.Add("Production.SAMDetail", "cond", "DocNumber", SAMFinishing.DocNumber);
                DT1.Rows.Add("Production.SAMDetail", "cond", "LineNumber", SAMFinishing.LineNumber);
                DT1.Rows.Add("Production.SAMDetail", "cond", "Type", "F");
                Gears.DeleteData(DT1, Conn);

            }


        }
        public class SAMWashing
        {

            public virtual SAM Parent { get; set; }
            public virtual string DocNumber { get; set; }
            public virtual string LineNumber { get; set; }
            public virtual string Type { get; set; }
            public virtual string OperationCode { get; set; }
            public virtual string Steps { get; set; }
            public virtual string Parts { get; set; }
            public virtual string OpsBreakdown { get; set; }
            public virtual string MachineType { get; set; }
            public virtual decimal ObservedTime { get; set; }
            public virtual decimal SAM { get; set; }
            public virtual string Picture { get; set; }
            public virtual string Video { get; set; }
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
                    a = Gears.RetriveData2("select * from Production.SAMDetail where DocNumber='" + DocNumber + "' AND Type='W' order by OperationCode asc", Conn);
                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }
            public void AddSAMWashing(SAMWashing SAMWashing)
            {

                int linenum = 0;


                DataTable count = Gears.RetriveData2("select max(convert(int,LineNumber)) as LineNumber from Production.SAMDetail where docnumber = '" + Docnum + "' AND Type='W'", Conn);

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
                DT1.Rows.Add("Production.SAMDetail", "0", "DocNumber", Docnum);
                DT1.Rows.Add("Production.SAMDetail", "0", "LineNumber", strLine);
                DT1.Rows.Add("Production.SAMDetail", "0", "Type", "W");
                DT1.Rows.Add("Production.SAMDetail", "0", "OperationCode", SAMWashing.OperationCode);
                DT1.Rows.Add("Production.SAMDetail", "0", "Steps", SAMWashing.Steps);
                DT1.Rows.Add("Production.SAMDetail", "0", "Parts", SAMWashing.Parts);
                DT1.Rows.Add("Production.SAMDetail", "0", "OpsBreakdown", SAMWashing.OpsBreakdown);
                DT1.Rows.Add("Production.SAMDetail", "0", "MachineType", SAMWashing.MachineType);
                DT1.Rows.Add("Production.SAMDetail", "0", "ObservedTime", SAMWashing.ObservedTime);
                DT1.Rows.Add("Production.SAMDetail", "0", "SAM", SAMWashing.SAM);
                DT1.Rows.Add("Production.SAMDetail", "0", "Picture", SAMWashing.Picture);
                DT1.Rows.Add("Production.SAMDetail", "0", "Video", SAMWashing.Video);
                DT1.Rows.Add("Production.SAMDetail", "0", "Field1", SAMWashing.Field1);
                DT1.Rows.Add("Production.SAMDetail", "0", "Field2", SAMWashing.Field2);
                DT1.Rows.Add("Production.SAMDetail", "0", "Field3", SAMWashing.Field3);
                DT1.Rows.Add("Production.SAMDetail", "0", "Field4", SAMWashing.Field4);
                DT1.Rows.Add("Production.SAMDetail", "0", "Field5", SAMWashing.Field5);
                DT1.Rows.Add("Production.SAMDetail", "0", "Field6", SAMWashing.Field6);
                DT1.Rows.Add("Production.SAMDetail", "0", "Field7", SAMWashing.Field7);
                DT1.Rows.Add("Production.SAMDetail", "0", "Field8", SAMWashing.Field8);
                DT1.Rows.Add("Production.SAMDetail", "0", "Field9", SAMWashing.Field9);

                Gears.CreateData(DT1, Conn);

            }
            public void UpdateSAMWashing(SAMWashing SAMWashing)
            {
                int linenum = 0;

                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();


                DT1.Rows.Add("Production.SAMDetail", "cond", "DocNumber", SAMWashing.DocNumber);
                DT1.Rows.Add("Production.SAMDetail", "cond", "LineNumber", SAMWashing.LineNumber);
                DT1.Rows.Add("Production.SAMDetail", "cond", "Type", "W");
                DT1.Rows.Add("Production.SAMDetail", "set", "OperationCode", SAMWashing.OperationCode);
                DT1.Rows.Add("Production.SAMDetail", "set", "Steps", SAMWashing.Steps);
                DT1.Rows.Add("Production.SAMDetail", "set", "Parts", SAMWashing.Parts);
                DT1.Rows.Add("Production.SAMDetail", "set", "OpsBreakdown", SAMWashing.OpsBreakdown);
                DT1.Rows.Add("Production.SAMDetail", "set", "MachineType", SAMWashing.MachineType);
                DT1.Rows.Add("Production.SAMDetail", "set", "ObservedTime", SAMWashing.ObservedTime);
                DT1.Rows.Add("Production.SAMDetail", "set", "SAM", SAMWashing.SAM);
                DT1.Rows.Add("Production.SAMDetail", "set", "Picture", SAMWashing.Picture);
                DT1.Rows.Add("Production.SAMDetail", "set", "Video", SAMWashing.Video);
                DT1.Rows.Add("Production.SAMDetail", "set", "Field1", SAMWashing.Field1);
                DT1.Rows.Add("Production.SAMDetail", "set", "Field2", SAMWashing.Field2);
                DT1.Rows.Add("Production.SAMDetail", "set", "Field3", SAMWashing.Field3);
                DT1.Rows.Add("Production.SAMDetail", "set", "Field4", SAMWashing.Field4);
                DT1.Rows.Add("Production.SAMDetail", "set", "Field5", SAMWashing.Field5);
                DT1.Rows.Add("Production.SAMDetail", "set", "Field6", SAMWashing.Field6);
                DT1.Rows.Add("Production.SAMDetail", "set", "Field7", SAMWashing.Field7);
                DT1.Rows.Add("Production.SAMDetail", "set", "Field8", SAMWashing.Field8);
                DT1.Rows.Add("Production.SAMDetail", "set", "Field9", SAMWashing.Field9);

                Gears.UpdateData(DT1, Conn);

            }
            public void DeleteSAMWashing(SAMWashing SAMWashing)
            {

                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

                DT1.Rows.Add("Production.SAMDetail", "cond", "DocNumber", SAMWashing.DocNumber);
                DT1.Rows.Add("Production.SAMDetail", "cond", "LineNumber", SAMWashing.LineNumber);
                DT1.Rows.Add("Production.SAMDetail", "cond", "Type", "W");
                Gears.DeleteData(DT1, Conn);

            }


        }
        public class SAMEmbroidery
        {

            public virtual SAM Parent { get; set; }
            public virtual string DocNumber { get; set; }
            public virtual string LineNumber { get; set; }
            public virtual string Type { get; set; }
            public virtual string OperationCode { get; set; }
            public virtual string Steps { get; set; }
            public virtual string Parts { get; set; }
            public virtual string OpsBreakdown { get; set; }
            public virtual string MachineType { get; set; }
            public virtual decimal ObservedTime { get; set; }
            public virtual decimal SAM { get; set; }
            public virtual string Picture { get; set; }
            public virtual string Video { get; set; }
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
                    a = Gears.RetriveData2("select * from Production.SAMDetail where DocNumber='" + DocNumber + "' AND Type='E' order by OperationCode asc", Conn);
                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }
            public void AddSAMEmbroidery(SAMEmbroidery SAMEmbroidery)
            {

                int linenum = 0;


                DataTable count = Gears.RetriveData2("select max(convert(int,LineNumber)) as LineNumber from Production.SAMDetail where docnumber = '" + Docnum + "' AND Type='E'", Conn);

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
                DT1.Rows.Add("Production.SAMDetail", "0", "DocNumber", Docnum);
                DT1.Rows.Add("Production.SAMDetail", "0", "LineNumber", strLine);
                DT1.Rows.Add("Production.SAMDetail", "0", "Type", "E");
                DT1.Rows.Add("Production.SAMDetail", "0", "OperationCode", SAMEmbroidery.OperationCode);
                DT1.Rows.Add("Production.SAMDetail", "0", "Steps", SAMEmbroidery.Steps);
                DT1.Rows.Add("Production.SAMDetail", "0", "Parts", SAMEmbroidery.Parts);
                DT1.Rows.Add("Production.SAMDetail", "0", "OpsBreakdown", SAMEmbroidery.OpsBreakdown);
                DT1.Rows.Add("Production.SAMDetail", "0", "MachineType", SAMEmbroidery.MachineType);
                DT1.Rows.Add("Production.SAMDetail", "0", "ObservedTime", SAMEmbroidery.ObservedTime);
                DT1.Rows.Add("Production.SAMDetail", "0", "SAM", SAMEmbroidery.SAM);
                DT1.Rows.Add("Production.SAMDetail", "0", "Picture", SAMEmbroidery.Picture);
                DT1.Rows.Add("Production.SAMDetail", "0", "Video", SAMEmbroidery.Video);
                DT1.Rows.Add("Production.SAMDetail", "0", "Field1", SAMEmbroidery.Field1);
                DT1.Rows.Add("Production.SAMDetail", "0", "Field2", SAMEmbroidery.Field2);
                DT1.Rows.Add("Production.SAMDetail", "0", "Field3", SAMEmbroidery.Field3);
                DT1.Rows.Add("Production.SAMDetail", "0", "Field4", SAMEmbroidery.Field4);
                DT1.Rows.Add("Production.SAMDetail", "0", "Field5", SAMEmbroidery.Field5);
                DT1.Rows.Add("Production.SAMDetail", "0", "Field6", SAMEmbroidery.Field6);
                DT1.Rows.Add("Production.SAMDetail", "0", "Field7", SAMEmbroidery.Field7);
                DT1.Rows.Add("Production.SAMDetail", "0", "Field8", SAMEmbroidery.Field8);
                DT1.Rows.Add("Production.SAMDetail", "0", "Field9", SAMEmbroidery.Field9);

                Gears.CreateData(DT1, Conn);

            }
            public void UpdateSAMEmbroidery(SAMEmbroidery SAMEmbroidery)
            {
                int linenum = 0;

                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();


                DT1.Rows.Add("Production.SAMDetail", "cond", "DocNumber", SAMEmbroidery.DocNumber);
                DT1.Rows.Add("Production.SAMDetail", "cond", "LineNumber", SAMEmbroidery.LineNumber);
                DT1.Rows.Add("Production.SAMDetail", "cond", "Type", "E");
                DT1.Rows.Add("Production.SAMDetail", "set", "OperationCode", SAMEmbroidery.OperationCode);
                DT1.Rows.Add("Production.SAMDetail", "set", "Steps", SAMEmbroidery.Steps);
                DT1.Rows.Add("Production.SAMDetail", "set", "Parts", SAMEmbroidery.Parts);
                DT1.Rows.Add("Production.SAMDetail", "set", "OpsBreakdown", SAMEmbroidery.OpsBreakdown);
                DT1.Rows.Add("Production.SAMDetail", "set", "MachineType", SAMEmbroidery.MachineType);
                DT1.Rows.Add("Production.SAMDetail", "set", "ObservedTime", SAMEmbroidery.ObservedTime);
                DT1.Rows.Add("Production.SAMDetail", "set", "SAM", SAMEmbroidery.SAM);
                DT1.Rows.Add("Production.SAMDetail", "set", "Picture", SAMEmbroidery.Picture);
                DT1.Rows.Add("Production.SAMDetail", "set", "Video", SAMEmbroidery.Video);
                DT1.Rows.Add("Production.SAMDetail", "set", "Field1", SAMEmbroidery.Field1);
                DT1.Rows.Add("Production.SAMDetail", "set", "Field2", SAMEmbroidery.Field2);
                DT1.Rows.Add("Production.SAMDetail", "set", "Field3", SAMEmbroidery.Field3);
                DT1.Rows.Add("Production.SAMDetail", "set", "Field4", SAMEmbroidery.Field4);
                DT1.Rows.Add("Production.SAMDetail", "set", "Field5", SAMEmbroidery.Field5);
                DT1.Rows.Add("Production.SAMDetail", "set", "Field6", SAMEmbroidery.Field6);
                DT1.Rows.Add("Production.SAMDetail", "set", "Field7", SAMEmbroidery.Field7);
                DT1.Rows.Add("Production.SAMDetail", "set", "Field8", SAMEmbroidery.Field8);
                DT1.Rows.Add("Production.SAMDetail", "set", "Field9", SAMEmbroidery.Field9);

                Gears.UpdateData(DT1, Conn);

            }
            public void DeleteSAMEmbroidery(SAMEmbroidery SAMEmbroidery)
            {

                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

                DT1.Rows.Add("Production.SAMDetail", "cond", "DocNumber", SAMEmbroidery.DocNumber);
                DT1.Rows.Add("Production.SAMDetail", "cond", "LineNumber", SAMEmbroidery.LineNumber);
                DT1.Rows.Add("Production.SAMDetail", "cond", "Type", "E");
                Gears.DeleteData(DT1, Conn);

            }


        }
        public class SAMPrinting
        {

            public virtual SAM Parent { get; set; }
            public virtual string DocNumber { get; set; }
            public virtual string LineNumber { get; set; }
            public virtual string Type { get; set; }
            public virtual string OperationCode { get; set; }
            public virtual string Steps { get; set; }
            public virtual string Parts { get; set; }
            public virtual string OpsBreakdown { get; set; }
            public virtual string MachineType { get; set; }
            public virtual decimal ObservedTime { get; set; }
            public virtual decimal SAM { get; set; }
            public virtual string Picture { get; set; }
            public virtual string Video { get; set; }
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
                    a = Gears.RetriveData2("select * from Production.SAMDetail where DocNumber='" + DocNumber + "' AND Type='P' order by OperationCode asc", Conn);
                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }
            public void AddSAMPrinting(SAMPrinting SAMPrinting)
            {

                int linenum = 0;


                DataTable count = Gears.RetriveData2("select max(convert(int,LineNumber)) as LineNumber from Production.SAMDetail where docnumber = '" + Docnum + "' AND Type='P'", Conn);

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
                DT1.Rows.Add("Production.SAMDetail", "0", "DocNumber", Docnum);
                DT1.Rows.Add("Production.SAMDetail", "0", "LineNumber", strLine);
                DT1.Rows.Add("Production.SAMDetail", "0", "Type", "P");
                DT1.Rows.Add("Production.SAMDetail", "0", "OperationCode", SAMPrinting.OperationCode);
                DT1.Rows.Add("Production.SAMDetail", "0", "Steps", SAMPrinting.Steps);
                DT1.Rows.Add("Production.SAMDetail", "0", "Parts", SAMPrinting.Parts);
                DT1.Rows.Add("Production.SAMDetail", "0", "OpsBreakdown", SAMPrinting.OpsBreakdown);
                DT1.Rows.Add("Production.SAMDetail", "0", "MachineType", SAMPrinting.MachineType);
                DT1.Rows.Add("Production.SAMDetail", "0", "ObservedTime", SAMPrinting.ObservedTime);
                DT1.Rows.Add("Production.SAMDetail", "0", "SAM", SAMPrinting.SAM);
                DT1.Rows.Add("Production.SAMDetail", "0", "Picture", SAMPrinting.Picture);
                DT1.Rows.Add("Production.SAMDetail", "0", "Video", SAMPrinting.Video);
                DT1.Rows.Add("Production.SAMDetail", "0", "Field1", SAMPrinting.Field1);
                DT1.Rows.Add("Production.SAMDetail", "0", "Field2", SAMPrinting.Field2);
                DT1.Rows.Add("Production.SAMDetail", "0", "Field3", SAMPrinting.Field3);
                DT1.Rows.Add("Production.SAMDetail", "0", "Field4", SAMPrinting.Field4);
                DT1.Rows.Add("Production.SAMDetail", "0", "Field5", SAMPrinting.Field5);
                DT1.Rows.Add("Production.SAMDetail", "0", "Field6", SAMPrinting.Field6);
                DT1.Rows.Add("Production.SAMDetail", "0", "Field7", SAMPrinting.Field7);
                DT1.Rows.Add("Production.SAMDetail", "0", "Field8", SAMPrinting.Field8);
                DT1.Rows.Add("Production.SAMDetail", "0", "Field9", SAMPrinting.Field9);

                Gears.CreateData(DT1, Conn);

            }
            public void UpdateSAMPrinting(SAMPrinting SAMPrinting)
            {
                int linenum = 0;

                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();


                DT1.Rows.Add("Production.SAMDetail", "cond", "DocNumber", SAMPrinting.DocNumber);
                DT1.Rows.Add("Production.SAMDetail", "cond", "LineNumber", SAMPrinting.LineNumber);
                DT1.Rows.Add("Production.SAMDetail", "cond", "Type", "P");
                DT1.Rows.Add("Production.SAMDetail", "set", "OperationCode", SAMPrinting.OperationCode);
                DT1.Rows.Add("Production.SAMDetail", "set", "Steps", SAMPrinting.Steps);
                DT1.Rows.Add("Production.SAMDetail", "set", "Parts", SAMPrinting.Parts);
                DT1.Rows.Add("Production.SAMDetail", "set", "OpsBreakdown", SAMPrinting.OpsBreakdown);
                DT1.Rows.Add("Production.SAMDetail", "set", "MachineType", SAMPrinting.MachineType);
                DT1.Rows.Add("Production.SAMDetail", "set", "ObservedTime", SAMPrinting.ObservedTime);
                DT1.Rows.Add("Production.SAMDetail", "set", "SAM", SAMPrinting.SAM);
                DT1.Rows.Add("Production.SAMDetail", "set", "Picture", SAMPrinting.Picture);
                DT1.Rows.Add("Production.SAMDetail", "set", "Video", SAMPrinting.Video);
                DT1.Rows.Add("Production.SAMDetail", "set", "Field1", SAMPrinting.Field1);
                DT1.Rows.Add("Production.SAMDetail", "set", "Field2", SAMPrinting.Field2);
                DT1.Rows.Add("Production.SAMDetail", "set", "Field3", SAMPrinting.Field3);
                DT1.Rows.Add("Production.SAMDetail", "set", "Field4", SAMPrinting.Field4);
                DT1.Rows.Add("Production.SAMDetail", "set", "Field5", SAMPrinting.Field5);
                DT1.Rows.Add("Production.SAMDetail", "set", "Field6", SAMPrinting.Field6);
                DT1.Rows.Add("Production.SAMDetail", "set", "Field7", SAMPrinting.Field7);
                DT1.Rows.Add("Production.SAMDetail", "set", "Field8", SAMPrinting.Field8);
                DT1.Rows.Add("Production.SAMDetail", "set", "Field9", SAMPrinting.Field9);

                Gears.UpdateData(DT1, Conn);

            }
            public void DeleteSAMPrinting(SAMPrinting SAMPrinting)
            {

                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

                DT1.Rows.Add("Production.SAMDetail", "cond", "DocNumber", SAMPrinting.DocNumber);
                DT1.Rows.Add("Production.SAMDetail", "cond", "LineNumber", SAMPrinting.LineNumber);
                DT1.Rows.Add("Production.SAMDetail", "cond", "Type", "P");
                Gears.DeleteData(DT1, Conn);

            }


        }


        public DataTable getdata(string Code, string Conn)
        {
            DataTable a;

            a = Gears.RetriveData2("select A.*,B.Name from Production.SAM A INNER JOIN Masterfile.BPSupplierInfo B ON A.SupplierCode = B.SupplierCode  where DocNumber = '" + Code + "'", Conn);

            foreach (DataRow dtRow in a.Rows)
            {
                DocNumber = dtRow["DocNumber"].ToString();
                DocDate = dtRow["DocDate"].ToString();
                SupplierCode = dtRow["SupplierCode"].ToString();
                Name = dtRow["Name"].ToString();
                DISNumber = dtRow["DISNumber"].ToString();
                Brand = dtRow["Brand"].ToString();
                Gender = dtRow["Gender"].ToString();
                ProductCategory = dtRow["ProductCategory"].ToString();
                ProductSubCategory = dtRow["ProductSubCategory"].ToString();
                ProductGroup = dtRow["ProductGroup"].ToString();
                FitCode = dtRow["FitCode"].ToString();
                ProductClass = dtRow["ProductClass"].ToString();
                DesignDesc = dtRow["DesignDesc"].ToString();
                Remarks = dtRow["Remarks"].ToString();
                FrontImage = dtRow["FrontImage"].ToString();
                BackImage = dtRow["BackImage"].ToString();
                CMPCost = Convert.ToDecimal(Convert.IsDBNull(dtRow["CMPCost"]) ? 0 : dtRow["CMPCost"]);
                CuttingCost = Convert.ToDecimal(Convert.IsDBNull(dtRow["CuttingCost"]) ? 0 : dtRow["CuttingCost"]);
                SewingCost = Convert.ToDecimal(Convert.IsDBNull(dtRow["SewingCost"]) ? 0 : dtRow["SewingCost"]);
                FinishingCost = Convert.ToDecimal(Convert.IsDBNull(dtRow["FinishingCost"]) ? 0 : dtRow["FinishingCost"]);
                WashingCost = Convert.ToDecimal(Convert.IsDBNull(dtRow["WashingCost"]) ? 0 : dtRow["WashingCost"]);
                EmbroideryCost = Convert.ToDecimal(Convert.IsDBNull(dtRow["EmbroideryCost"]) ? 0 : dtRow["EmbroideryCost"]);
                PrintingCost = Convert.ToDecimal(Convert.IsDBNull(dtRow["PrintingCost"]) ? 0 : dtRow["PrintingCost"]);
                TotalCost = Convert.ToDecimal(Convert.IsDBNull(dtRow["TotalCost"]) ? 0 : dtRow["TotalCost"]);
                CEfficiency = Convert.ToDecimal(Convert.IsDBNull(dtRow["CEfficiency"]) ? 0 : dtRow["CEfficiency"]);
                CAllowance = Convert.ToDecimal(Convert.IsDBNull(dtRow["CAllowance"]) ? 0 : dtRow["CAllowance"]);
                CTotalObserved = Convert.ToDecimal(Convert.IsDBNull(dtRow["CTotalObserved"]) ? 0 : dtRow["CTotalObserved"]);
                CBasicMinutes = Convert.ToDecimal(Convert.IsDBNull(dtRow["CBasicMinutes"]) ? 0 : dtRow["CBasicMinutes"]);
                CSAM = Convert.ToDecimal(Convert.IsDBNull(dtRow["CSAM"]) ? 0 : dtRow["CSAM"]);
                CMinimumWage = Convert.ToDecimal(Convert.IsDBNull(dtRow["CMinimumWage"]) ? 0 : dtRow["CMinimumWage"]);
                CLaborCost = Convert.ToDecimal(Convert.IsDBNull(dtRow["CLaborCost"]) ? 0 : dtRow["CLaborCost"]);
                CMarkup = Convert.ToDecimal(Convert.IsDBNull(dtRow["CMarkup"]) ? 0 : dtRow["CMarkup"]);
                CCost = Convert.ToDecimal(Convert.IsDBNull(dtRow["CCost"]) ? 0 : dtRow["CCost"]);
                SEfficiency = Convert.ToDecimal(Convert.IsDBNull(dtRow["SEfficiency"]) ? 0 : dtRow["SEfficiency"]);
                SAllowance = Convert.ToDecimal(Convert.IsDBNull(dtRow["SAllowance"]) ? 0 : dtRow["SAllowance"]);
                STotalObserved = Convert.ToDecimal(Convert.IsDBNull(dtRow["STotalObserved"]) ? 0 : dtRow["STotalObserved"]);
                SBasicMinutes = Convert.ToDecimal(Convert.IsDBNull(dtRow["SBasicMinutes"]) ? 0 : dtRow["SBasicMinutes"]);
                SSAM = Convert.ToDecimal(Convert.IsDBNull(dtRow["SSAM"]) ? 0 : dtRow["SSAM"]);
                SMinimumWage = Convert.ToDecimal(Convert.IsDBNull(dtRow["SMinimumWage"]) ? 0 : dtRow["SMinimumWage"]);
                SLaborCost = Convert.ToDecimal(Convert.IsDBNull(dtRow["SLaborCost"]) ? 0 : dtRow["SLaborCost"]);
                SMarkup = Convert.ToDecimal(Convert.IsDBNull(dtRow["SMarkup"]) ? 0 : dtRow["SMarkup"]);
                SCost = Convert.ToDecimal(Convert.IsDBNull(dtRow["SCost"]) ? 0 : dtRow["SCost"]);

                FEfficiency = Convert.ToDecimal(Convert.IsDBNull(dtRow["FEfficiency"]) ? 0 : dtRow["FEfficiency"]);
                FAllowance = Convert.ToDecimal(Convert.IsDBNull(dtRow["FAllowance"]) ? 0 : dtRow["FAllowance"]);
                FTotalObserved = Convert.ToDecimal(Convert.IsDBNull(dtRow["FTotalObserved"]) ? 0 : dtRow["FTotalObserved"]);
                FBasicMinutes = Convert.ToDecimal(Convert.IsDBNull(dtRow["FBasicMinutes"]) ? 0 : dtRow["FBasicMinutes"]);
                FSAM = Convert.ToDecimal(Convert.IsDBNull(dtRow["FSAM"]) ? 0 : dtRow["FSAM"]);
                FMinimumWage = Convert.ToDecimal(Convert.IsDBNull(dtRow["FMinimumWage"]) ? 0 : dtRow["FMinimumWage"]);
                FLaborCost = Convert.ToDecimal(Convert.IsDBNull(dtRow["FLaborCost"]) ? 0 : dtRow["FLaborCost"]);
                FMarkup = Convert.ToDecimal(Convert.IsDBNull(dtRow["FMarkup"]) ? 0 : dtRow["FMarkup"]);
                FCost = Convert.ToDecimal(Convert.IsDBNull(dtRow["FCost"]) ? 0 : dtRow["FCost"]);

                WEfficiency = Convert.ToDecimal(Convert.IsDBNull(dtRow["WEfficiency"]) ? 0 : dtRow["WEfficiency"]);
                WAllowance = Convert.ToDecimal(Convert.IsDBNull(dtRow["WAllowance"]) ? 0 : dtRow["WAllowance"]);
                WTotalObserved = Convert.ToDecimal(Convert.IsDBNull(dtRow["WTotalObserved"]) ? 0 : dtRow["WTotalObserved"]);
                WBasicMinutes = Convert.ToDecimal(Convert.IsDBNull(dtRow["WBasicMinutes"]) ? 0 : dtRow["WBasicMinutes"]);
                WSAM = Convert.ToDecimal(Convert.IsDBNull(dtRow["WSAM"]) ? 0 : dtRow["WSAM"]);
                WMinimumWage = Convert.ToDecimal(Convert.IsDBNull(dtRow["WMinimumWage"]) ? 0 : dtRow["WMinimumWage"]);
                WLaborCost = Convert.ToDecimal(Convert.IsDBNull(dtRow["WLaborCost"]) ? 0 : dtRow["WLaborCost"]);
                WMarkup = Convert.ToDecimal(Convert.IsDBNull(dtRow["WMarkup"]) ? 0 : dtRow["WMarkup"]);
                WCost = Convert.ToDecimal(Convert.IsDBNull(dtRow["WCost"]) ? 0 : dtRow["WCost"]);
                EEfficiency = Convert.ToDecimal(Convert.IsDBNull(dtRow["EEfficiency"]) ? 0 : dtRow["EEfficiency"]);
                EAllowance = Convert.ToDecimal(Convert.IsDBNull(dtRow["EAllowance"]) ? 0 : dtRow["EAllowance"]);
                ETotalObserved = Convert.ToDecimal(Convert.IsDBNull(dtRow["ETotalObserved"]) ? 0 : dtRow["ETotalObserved"]);
                EBasicMinutes = Convert.ToDecimal(Convert.IsDBNull(dtRow["EBasicMinutes"]) ? 0 : dtRow["EBasicMinutes"]);
                ESAM = Convert.ToDecimal(Convert.IsDBNull(dtRow["ESAM"]) ? 0 : dtRow["ESAM"]);
                EMinimumWage = Convert.ToDecimal(Convert.IsDBNull(dtRow["EMinimumWage"]) ? 0 : dtRow["EMinimumWage"]);
                ELaborCost = Convert.ToDecimal(Convert.IsDBNull(dtRow["ELaborCost"]) ? 0 : dtRow["ELaborCost"]);
                EMarkup = Convert.ToDecimal(Convert.IsDBNull(dtRow["EMarkup"]) ? 0 : dtRow["EMarkup"]);
                ECost = Convert.ToDecimal(Convert.IsDBNull(dtRow["ECost"]) ? 0 : dtRow["ECost"]);
                PEfficiency = Convert.ToDecimal(Convert.IsDBNull(dtRow["PEfficiency"]) ? 0 : dtRow["PEfficiency"]);
                PAllowance = Convert.ToDecimal(Convert.IsDBNull(dtRow["PAllowance"]) ? 0 : dtRow["PAllowance"]);
                PTotalObserved = Convert.ToDecimal(Convert.IsDBNull(dtRow["PTotalObserved"]) ? 0 : dtRow["PTotalObserved"]);
                PBasicMinutes = Convert.ToDecimal(Convert.IsDBNull(dtRow["PBasicMinutes"]) ? 0 : dtRow["PBasicMinutes"]);
                PSAM = Convert.ToDecimal(Convert.IsDBNull(dtRow["PSAM"]) ? 0 : dtRow["PSAM"]);
                PMinimumWage = Convert.ToDecimal(Convert.IsDBNull(dtRow["PMinimumWage"]) ? 0 : dtRow["PMinimumWage"]);
                PLaborCost = Convert.ToDecimal(Convert.IsDBNull(dtRow["PLaborCost"]) ? 0 : dtRow["PLaborCost"]);
                PMarkup = Convert.ToDecimal(Convert.IsDBNull(dtRow["PMarkup"]) ? 0 : dtRow["PMarkup"]);
                PCost = Convert.ToDecimal(Convert.IsDBNull(dtRow["PCost"]) ? 0 : dtRow["PCost"]);
                IsWithDetail = Convert.ToBoolean(Convert.IsDBNull(dtRow["IsWithDetail"]) ? 0 : dtRow["IsWithDetail"]);
                IsValidated = Convert.ToBoolean(Convert.IsDBNull(dtRow["IsValidated"]) ? 0 : dtRow["IsValidated"]);
                IsPrinted = Convert.ToBoolean(Convert.IsDBNull(dtRow["IsPrinted"]) ? 0 : dtRow["IsPrinted"]);
                CancelledBy = dtRow["CancelledBy"].ToString();
                CancelledDate = dtRow["CancelledDate"].ToString();
                AddedBy = dtRow["AddedBy"].ToString();
                AddedDate = dtRow["AddedDate"].ToString();
                LastEditedBy = dtRow["LastEditedBy"].ToString();
                LastEditedDate = dtRow["LastEditedDate"].ToString();
                SubmittedBy = dtRow["SubmittedBy"].ToString();
                SubmittedDate = dtRow["SubmittedDate"].ToString();
                ForceClosedBy = dtRow["ForceClosedBy"].ToString();
                ForceClosedDate = dtRow["ForceClosedDate"].ToString();
                CRegion = dtRow["CRegion"].ToString();
                SRegion = dtRow["SRegion"].ToString();
                FRegion = dtRow["FRegion"].ToString();
                WRegion = dtRow["WRegion"].ToString();
                ERegion = dtRow["ERegion"].ToString();
                PRegion = dtRow["PRegion"].ToString();
            }

            return a;
        }
         public string InsertData (SAM _ent)
        {

            Docnum = _ent.DocNumber;//<--- You can change this part
            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add("Production.SAM", "0", "DocNumber", _ent.DocNumber);
            DT1.Rows.Add("Production.SAM", "0", "DocDate", _ent.DocDate);
            DT1.Rows.Add("Production.SAM", "0", "SupplierCode", _ent.SupplierCode);
            DT1.Rows.Add("Production.SAM", "0", "DISNumber", _ent.DISNumber);
            DT1.Rows.Add("Production.SAM", "0", "Brand", _ent.Brand);
            DT1.Rows.Add("Production.SAM", "0", "Gender", _ent.Gender);
            DT1.Rows.Add("Production.SAM", "0", "ProductCategory", _ent.ProductCategory);
            DT1.Rows.Add("Production.SAM", "0", "ProductSubCategory", _ent.ProductSubCategory);
            DT1.Rows.Add("Production.SAM", "0", "ProductGroup", _ent.ProductGroup);
            DT1.Rows.Add("Production.SAM", "0", "FitCode", _ent.FitCode);
            DT1.Rows.Add("Production.SAM", "0", "ProductClass", _ent.ProductClass);
            DT1.Rows.Add("Production.SAM", "0", "DesignDesc", _ent.DesignDesc);
            DT1.Rows.Add("Production.SAM", "0", "Remarks", _ent.Remarks);
            DT1.Rows.Add("Production.SAM", "0", "FrontImage", _ent.FrontImage);
            DT1.Rows.Add("Production.SAM", "0", "CMPCost", _ent.CMPCost);
            DT1.Rows.Add("Production.SAM", "0", "CuttingCost", _ent.CuttingCost);
            DT1.Rows.Add("Production.SAM", "0", "SewingCost", _ent.SewingCost);
            DT1.Rows.Add("Production.SAM", "0", "FinishingCost", _ent.FinishingCost);
            DT1.Rows.Add("Production.SAM", "0", "WashingCost", _ent.WashingCost);
            DT1.Rows.Add("Production.SAM", "0", "EmbroideryCost", _ent.EmbroideryCost);
            DT1.Rows.Add("Production.SAM", "0", "PrintingCost", _ent.PrintingCost);
            DT1.Rows.Add("Production.SAM", "0", "TotalCost", _ent.TotalCost);
            DT1.Rows.Add("Production.SAM", "0", "CEfficiency", _ent.CEfficiency);
            DT1.Rows.Add("Production.SAM", "0", "CAllowance", _ent.CAllowance);
            DT1.Rows.Add("Production.SAM", "0", "CTotalObserved", _ent.CTotalObserved);
            DT1.Rows.Add("Production.SAM", "0", "CBasicMinutes", _ent.CBasicMinutes);
            DT1.Rows.Add("Production.SAM", "0", "CSAM", _ent.CSAM);
            DT1.Rows.Add("Production.SAM", "0", "CMinimumWage", _ent.CMinimumWage);
            DT1.Rows.Add("Production.SAM", "0", "CLaborCost", _ent.CLaborCost);
            DT1.Rows.Add("Production.SAM", "0", "CMarkup", _ent.CMarkup);
            DT1.Rows.Add("Production.SAM", "0", "CCost", _ent.CCost);
            DT1.Rows.Add("Production.SAM", "0", "SEfficiency", _ent.SEfficiency);
            DT1.Rows.Add("Production.SAM", "0", "SAllowance", _ent.SAllowance);
            DT1.Rows.Add("Production.SAM", "0", "STotalObserved", _ent.STotalObserved);
            DT1.Rows.Add("Production.SAM", "0", "SBasicMinutes", _ent.SBasicMinutes);
            DT1.Rows.Add("Production.SAM", "0", "SSAM", _ent.SSAM);
            DT1.Rows.Add("Production.SAM", "0", "SMinimumWage", _ent.SMinimumWage);
            DT1.Rows.Add("Production.SAM", "0", "SLaborCost", _ent.SLaborCost);
            DT1.Rows.Add("Production.SAM", "0", "SMarkup", _ent.SMarkup);
            DT1.Rows.Add("Production.SAM", "0", "SCost", _ent.SCost);
            DT1.Rows.Add("Production.SAM", "0", "FEfficiency", _ent.SEfficiency);
            DT1.Rows.Add("Production.SAM", "0", "FAllowance", _ent.SAllowance);
            DT1.Rows.Add("Production.SAM", "0", "FTotalObserved", _ent.STotalObserved);
            DT1.Rows.Add("Production.SAM", "0", "FBasicMinutes", _ent.SBasicMinutes);
            DT1.Rows.Add("Production.SAM", "0", "FSAM", _ent.SSAM);
            DT1.Rows.Add("Production.SAM", "0", "FMinimumWage", _ent.SMinimumWage);
            DT1.Rows.Add("Production.SAM", "0", "FLaborCost", _ent.SLaborCost);
            DT1.Rows.Add("Production.SAM", "0", "FMarkup", _ent.SMarkup);
            DT1.Rows.Add("Production.SAM", "0", "FCost", _ent.SCost);
            DT1.Rows.Add("Production.SAM", "0", "WEfficiency", _ent.WEfficiency);
            DT1.Rows.Add("Production.SAM", "0", "WAllowance", _ent.WAllowance);
            DT1.Rows.Add("Production.SAM", "0", "WTotalObserved", _ent.WTotalObserved);
            DT1.Rows.Add("Production.SAM", "0", "WBasicMinutes", _ent.WBasicMinutes);
            DT1.Rows.Add("Production.SAM", "0", "WSAM", _ent.WSAM);
            DT1.Rows.Add("Production.SAM", "0", "WMinimumWage", _ent.WMinimumWage);
            DT1.Rows.Add("Production.SAM", "0", "WLaborCost", _ent.WLaborCost);
            DT1.Rows.Add("Production.SAM", "0", "WMarkup", _ent.WMarkup);
            DT1.Rows.Add("Production.SAM", "0", "WCost", _ent.WCost);
            DT1.Rows.Add("Production.SAM", "0", "EEfficiency", _ent.EEfficiency);
            DT1.Rows.Add("Production.SAM", "0", "EAllowance", _ent.EAllowance);
            DT1.Rows.Add("Production.SAM", "0", "ETotalObserved", _ent.ETotalObserved);
            DT1.Rows.Add("Production.SAM", "0", "EBasicMinutes", _ent.EBasicMinutes);
            DT1.Rows.Add("Production.SAM", "0", "ESAM", _ent.ESAM);
            DT1.Rows.Add("Production.SAM", "0", "EMinimumWage", _ent.EMinimumWage);
            DT1.Rows.Add("Production.SAM", "0", "ELaborCost", _ent.ELaborCost);
            DT1.Rows.Add("Production.SAM", "0", "EMarkup", _ent.EMarkup);
            DT1.Rows.Add("Production.SAM", "0", "ECost", _ent.ECost);
            DT1.Rows.Add("Production.SAM", "0", "PEfficiency", _ent.PEfficiency);
            DT1.Rows.Add("Production.SAM", "0", "PAllowance", _ent.PAllowance);
            DT1.Rows.Add("Production.SAM", "0", "PTotalObserved", _ent.PTotalObserved);
            DT1.Rows.Add("Production.SAM", "0", "PBasicMinutes", _ent.PBasicMinutes);
            DT1.Rows.Add("Production.SAM", "0", "PSAM", _ent.PSAM);
            DT1.Rows.Add("Production.SAM", "0", "PMinimumWage", _ent.PMinimumWage);
            DT1.Rows.Add("Production.SAM", "0", "PLaborCost", _ent.PLaborCost);
            DT1.Rows.Add("Production.SAM", "0", "PMarkup", _ent.PMarkup);
            DT1.Rows.Add("Production.SAM", "0", "PCost", _ent.PCost);
            DT1.Rows.Add("Production.SAM", "0", "IsWithDetail", _ent.IsWithDetail);
            DT1.Rows.Add("Production.SAM", "0", "IsValidated", _ent.IsValidated);
            DT1.Rows.Add("Production.SAM", "0", "IsPrinted", _ent.IsPrinted);
            DT1.Rows.Add("Production.SAM", "0", "CancelledBy", _ent.CancelledBy);
            DT1.Rows.Add("Production.SAM", "0", "CancelledDate", _ent.CancelledDate);
            DT1.Rows.Add("Production.SAM", "0", "AddedBy", _ent.AddedBy);
            DT1.Rows.Add("Production.SAM", "0", "AddedDate", _ent.AddedDate);
            DT1.Rows.Add("Production.SAM", "0", "LastEditedBy", _ent.LastEditedBy);
            DT1.Rows.Add("Production.SAM", "0", "LastEditedDate", _ent.LastEditedDate);
            DT1.Rows.Add("Production.SAM", "0", "SubmittedBy", _ent.SubmittedBy);
            DT1.Rows.Add("Production.SAM", "0", "SubmittedDate", _ent.SubmittedDate);
            DT1.Rows.Add("Production.SAM", "0", "ForceClosedBy", _ent.ForceClosedBy);
            DT1.Rows.Add("Production.SAM", "0", "ForceClosedDate", _ent.ForceClosedDate);
            DT1.Rows.Add("Production.SAM", "0", "CRegion", _ent.CRegion);
            DT1.Rows.Add("Production.SAM", "0", "SRegion", _ent.SRegion);
            DT1.Rows.Add("Production.SAM", "0", "FRegion", _ent.FRegion);
            DT1.Rows.Add("Production.SAM", "0", "WRegion", _ent.WRegion);
            DT1.Rows.Add("Production.SAM", "0", "ERegion", _ent.ERegion);
            DT1.Rows.Add("Production.SAM", "0", "PRegion", _ent.PRegion);

            Functions.AuditTrail(strTranstype, _ent.DocNumber, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "INSERT", _ent.Connection);
    
           return  Gears.CreateData(DT1, _ent.Connection);
    }


         public string UpdateData(SAM _ent)
        {

            Docnum = _ent.DocNumber;//<--- You can change this part
            Conn = _ent.Connection;
            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add("Production.SAM", "cond", "DocNumber", _ent.DocNumber);
            DT1.Rows.Add("Production.SAM", "set", "DocDate", _ent.DocDate);
            DT1.Rows.Add("Production.SAM", "set", "SupplierCode", _ent.SupplierCode);
            DT1.Rows.Add("Production.SAM", "set", "DISNumber", _ent.DISNumber);
            DT1.Rows.Add("Production.SAM", "set", "Brand", _ent.Brand);
            DT1.Rows.Add("Production.SAM", "set", "Gender", _ent.Gender);
            DT1.Rows.Add("Production.SAM", "set", "ProductCategory", _ent.ProductCategory);
            DT1.Rows.Add("Production.SAM", "set", "ProductSubCategory", _ent.ProductSubCategory);
            DT1.Rows.Add("Production.SAM", "set", "ProductGroup", _ent.ProductGroup);
            DT1.Rows.Add("Production.SAM", "set", "FitCode", _ent.FitCode);
            DT1.Rows.Add("Production.SAM", "set", "ProductClass", _ent.ProductClass);
            DT1.Rows.Add("Production.SAM", "set", "DesignDesc", _ent.DesignDesc);
            DT1.Rows.Add("Production.SAM", "set", "Remarks", _ent.Remarks);
            DT1.Rows.Add("Production.SAM", "set", "FrontImage", _ent.FrontImage);
            DT1.Rows.Add("Production.SAM", "set", "BackImage", _ent.BackImage);
            DT1.Rows.Add("Production.SAM", "set", "CMPCost", _ent.CMPCost);
            DT1.Rows.Add("Production.SAM", "set", "CuttingCost", _ent.CuttingCost);
            DT1.Rows.Add("Production.SAM", "set", "SewingCost", _ent.SewingCost);
            DT1.Rows.Add("Production.SAM", "set", "FinishingCost", _ent.FinishingCost);
            DT1.Rows.Add("Production.SAM", "set", "WashingCost", _ent.WashingCost);
            DT1.Rows.Add("Production.SAM", "set", "EmbroideryCost", _ent.EmbroideryCost);
            DT1.Rows.Add("Production.SAM", "set", "PrintingCost", _ent.PrintingCost);
            DT1.Rows.Add("Production.SAM", "set", "TotalCost", _ent.TotalCost);
            DT1.Rows.Add("Production.SAM", "set", "CEfficiency", _ent.CEfficiency);
            DT1.Rows.Add("Production.SAM", "set", "CAllowance", _ent.CAllowance);
            DT1.Rows.Add("Production.SAM", "set", "CTotalObserved", _ent.CTotalObserved);
            DT1.Rows.Add("Production.SAM", "set", "CBasicMinutes", _ent.CBasicMinutes);
            DT1.Rows.Add("Production.SAM", "set", "CSAM", _ent.CSAM);
            DT1.Rows.Add("Production.SAM", "set", "CMinimumWage", _ent.CMinimumWage);
            DT1.Rows.Add("Production.SAM", "set", "CLaborCost", _ent.CLaborCost);
            DT1.Rows.Add("Production.SAM", "set", "CMarkup", _ent.CMarkup);
            DT1.Rows.Add("Production.SAM", "set", "CCost", _ent.CCost);
            DT1.Rows.Add("Production.SAM", "set", "SEfficiency", _ent.SEfficiency);
            DT1.Rows.Add("Production.SAM", "set", "SAllowance", _ent.SAllowance);
            DT1.Rows.Add("Production.SAM", "set", "STotalObserved", _ent.STotalObserved);
            DT1.Rows.Add("Production.SAM", "set", "SBasicMinutes", _ent.SBasicMinutes);
            DT1.Rows.Add("Production.SAM", "set", "SSAM", _ent.SSAM);
            DT1.Rows.Add("Production.SAM", "set", "SMinimumWage", _ent.SMinimumWage);
            DT1.Rows.Add("Production.SAM", "set", "SLaborCost", _ent.SLaborCost);
            DT1.Rows.Add("Production.SAM", "set", "SMarkup", _ent.SMarkup);
            DT1.Rows.Add("Production.SAM", "set", "SCost", _ent.SCost);
            DT1.Rows.Add("Production.SAM", "set", "FEfficiency", _ent.SEfficiency);
            DT1.Rows.Add("Production.SAM", "set", "FAllowance", _ent.SAllowance);
            DT1.Rows.Add("Production.SAM", "set", "FTotalObserved", _ent.STotalObserved);
            DT1.Rows.Add("Production.SAM", "set", "FBasicMinutes", _ent.SBasicMinutes);
            DT1.Rows.Add("Production.SAM", "set", "FSAM", _ent.SSAM);
            DT1.Rows.Add("Production.SAM", "set", "FMinimumWage", _ent.SMinimumWage);
            DT1.Rows.Add("Production.SAM", "set", "FLaborCost", _ent.SLaborCost);
            DT1.Rows.Add("Production.SAM", "set", "FMarkup", _ent.SMarkup);
            DT1.Rows.Add("Production.SAM", "set", "FCost", _ent.SCost);
            DT1.Rows.Add("Production.SAM", "set", "WEfficiency", _ent.WEfficiency);
            DT1.Rows.Add("Production.SAM", "set", "WAllowance", _ent.WAllowance);
            DT1.Rows.Add("Production.SAM", "set", "WTotalObserved", _ent.WTotalObserved);
            DT1.Rows.Add("Production.SAM", "set", "WBasicMinutes", _ent.WBasicMinutes);
            DT1.Rows.Add("Production.SAM", "set", "WSAM", _ent.WSAM);
            DT1.Rows.Add("Production.SAM", "set", "WMinimumWage", _ent.WMinimumWage);
            DT1.Rows.Add("Production.SAM", "set", "WLaborCost", _ent.WLaborCost);
            DT1.Rows.Add("Production.SAM", "set", "WMarkup", _ent.WMarkup);
            DT1.Rows.Add("Production.SAM", "set", "WCost", _ent.WCost);
            DT1.Rows.Add("Production.SAM", "set", "EEfficiency", _ent.EEfficiency);
            DT1.Rows.Add("Production.SAM", "set", "EAllowance", _ent.EAllowance);
            DT1.Rows.Add("Production.SAM", "set", "ETotalObserved", _ent.ETotalObserved);
            DT1.Rows.Add("Production.SAM", "set", "EBasicMinutes", _ent.EBasicMinutes);
            DT1.Rows.Add("Production.SAM", "set", "ESAM", _ent.ESAM);
            DT1.Rows.Add("Production.SAM", "set", "EMinimumWage", _ent.EMinimumWage);
            DT1.Rows.Add("Production.SAM", "set", "ELaborCost", _ent.ELaborCost);
            DT1.Rows.Add("Production.SAM", "set", "EMarkup", _ent.EMarkup);
            DT1.Rows.Add("Production.SAM", "set", "ECost", _ent.ECost);
            DT1.Rows.Add("Production.SAM", "set", "PEfficiency", _ent.PEfficiency);
            DT1.Rows.Add("Production.SAM", "set", "PAllowance", _ent.PAllowance);
            DT1.Rows.Add("Production.SAM", "set", "PTotalObserved", _ent.PTotalObserved);
            DT1.Rows.Add("Production.SAM", "set", "PBasicMinutes", _ent.PBasicMinutes);
            DT1.Rows.Add("Production.SAM", "set", "PSAM", _ent.PSAM);
            DT1.Rows.Add("Production.SAM", "set", "PMinimumWage", _ent.PMinimumWage);
            DT1.Rows.Add("Production.SAM", "set", "PLaborCost", _ent.PLaborCost);
            DT1.Rows.Add("Production.SAM", "set", "PMarkup", _ent.PMarkup);
            DT1.Rows.Add("Production.SAM", "set", "PCost", _ent.PCost);
            DT1.Rows.Add("Production.SAM", "set", "IsWithDetail", _ent.IsWithDetail);
            DT1.Rows.Add("Production.SAM", "set", "IsValidated", _ent.IsValidated);
            DT1.Rows.Add("Production.SAM", "set", "IsPrinted", _ent.IsPrinted);
            DT1.Rows.Add("Production.SAM", "set", "CRegion", _ent.CRegion);
            DT1.Rows.Add("Production.SAM", "set", "SRegion", _ent.SRegion);
            DT1.Rows.Add("Production.SAM", "set", "FRegion", _ent.FRegion);
            DT1.Rows.Add("Production.SAM", "set", "WRegion", _ent.WRegion);
            DT1.Rows.Add("Production.SAM", "set", "ERegion", _ent.ERegion);
            DT1.Rows.Add("Production.SAM", "set", "PRegion", _ent.PRegion);
            DT1.Rows.Add("Production.SAM", "set", "LastEditedBy", _ent.LastEditedBy);
            DT1.Rows.Add("Production.SAM", "set", "LastEditedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        


            Functions.AuditTrail(strTranstype, _ent.DocNumber, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "UPDATE", _ent.Connection);

             return Gears.UpdateData(DT1, _ent.Connection);
                    }
         public string DeleteData(SAM _ent)
         {
             Docnum = _ent.DocNumber;//<--- You can change this part
             Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

             DT1.Rows.Add("Production.SAM", "cond", "DocNumber", _ent.DocNumber);

             Functions.AuditTrail(strTranstype, _ent.DocNumber, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "DELETE", _ent.Connection);

             return Gears.DeleteData(DT1, _ent.Connection);
         }

    }
}
