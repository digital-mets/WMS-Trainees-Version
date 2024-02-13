using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GearsLibrary;
using System.Data;

namespace Entity
{
    public class MachineMaster
    {
        private static string Conn;
        public virtual string Connection { get; set; }
        public virtual string RecordID { get; set; }
       public virtual string MachineID  { get; set; }
	   public virtual string MachineName  { get; set; }
	   public virtual string MachineType  { get; set; }
       public virtual string Description { get; set; }
	   public virtual string AssetCode  { get; set; }
	   public virtual string AssetTag  { get; set; }
	   public virtual string MachineCategory  { get; set; }
	   public virtual string Brand  { get; set; }
	   public virtual string Model  { get; set; }
	   public virtual string SerialNo  { get; set; }
	   public virtual string SupplyVoltage  { get; set; }
	   public virtual string Location  { get; set; }
	   public virtual string AssignedPersonnel  { get; set; }
	   public virtual string Status  { get; set; }
       public virtual string DateAcquired { get; set; }
       public virtual string Section { get; set; }
       public virtual string Manual { get; set; }

        public virtual string AddedBy { get; set; }
        public virtual string AddedDate { get; set; }
        public virtual string LastEditedBy { get; set; }
        public virtual string LastEditedDate { get; set; }
        public virtual string ActivatedBy { get; set; }
        public virtual string ActivatedDate { get; set; }
        public virtual string DeactivatedBy { get; set; }
        public virtual string DeactivatedDate { get; set; }

        public virtual IList<PMSchedule> StepProcessDetail { get; set; }
        public virtual IList<PMChecklist> StepProcessBOMDetail { get; set; }
        public virtual IList<MaterialReq> MaterialReqDetail { get; set; }
      
        
        public class PMSchedule
        {
            public virtual string MachineCode { get; set; }
            public virtual string LineNumber { get; set; }
            public virtual string MachineID { get; set; }
            public virtual string Description { get; set; }
            public virtual string Brand { get; set; }
            public virtual string Model { get; set; }
            public virtual string SerialNo { get; set; }
            public virtual string SupplyVoltage { get; set; }

            public DataTable getStepProcess(string SKUCode, string Conn)
            {
                DataTable a;
                try
                {
                    a = Gears.RetriveData2("select LineNumber,MachineID,MachineCode,Description,Brand,Model,SerialNo,SupplyVoltage from Masterfile.MachineDetail Code='" + MachineID + "' ORDER BY LineNumber ASC", Conn);

                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }
            public void AddStepProcess(PMSchedule _StepProcessDetails, string Conn)
            {
                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();

                DT1.Rows.Add("Masterfile.MachineDetail", "cond", "MachineID", MachineID);
                Gears.DeleteData(DT1, Conn);

                DT2.Rows.Add("Masterfile.MachineDetail", "0", "MachineCode", MachineCode);
                DT2.Rows.Add("Masterfile.MachineDetail", "0", "LineNumber", _StepProcessDetails.LineNumber);
                DT2.Rows.Add("Masterfile.MachineDetail", "0", "MachineID", MachineID);
                DT2.Rows.Add("Masterfile.MachineDetail", "0", "Description", _StepProcessDetails.Description);
                DT2.Rows.Add("Masterfile.MachineDetail", "0", "Brand", _StepProcessDetails.Brand);
                DT2.Rows.Add("Masterfile.MachineDetail", "0", "Model", _StepProcessDetails.Model);
                DT2.Rows.Add("Masterfile.MachineDetail", "0", "SerialNo", _StepProcessDetails.SerialNo);
                DT2.Rows.Add("Masterfile.MachineDetail", "0", "SupplyVoltage", _StepProcessDetails.SupplyVoltage);

                Gears.CreateData(DT2, Conn);

            }

            public void UpdateStepProcess(PMSchedule _StepProcessDetails)
            {
                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

                DT1.Rows.Add("Masterfile.MachineDetail", "cond", "MachineID", MachineID);
                DT1.Rows.Add("Masterfile.MachineDetail", "cond", "MachineCode", _StepProcessDetails.MachineCode);
                DT1.Rows.Add("Masterfile.MachineDetail", "cond", "LineNumber", _StepProcessDetails.LineNumber);
                DT1.Rows.Add("Masterfile.MachineDetail", "set", "Description", _StepProcessDetails.Description);
                DT1.Rows.Add("Masterfile.MachineDetail", "set", "Brand", _StepProcessDetails.Brand);
                DT1.Rows.Add("Masterfile.MachineDetail", "set", "Model", _StepProcessDetails.Model);
                DT1.Rows.Add("Masterfile.MachineDetail", "set", "SerialNo", _StepProcessDetails.SerialNo);
                DT1.Rows.Add("Masterfile.MachineDetail", "set", "SupplyVoltage", _StepProcessDetails.SupplyVoltage);
           
                Gears.UpdateData(DT1, Conn);

            }

            public void DeleteStepProcess(PMSchedule _StepProcessDetails)
            {
                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT3 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT4 = new Gears.CRUDdatatable();

                DT1.Rows.Add("Masterfile.MachineDetail", "cond", "MachineCode", MachineCode);
                DT1.Rows.Add("Masterfile.MachineDetail", "cond", "LineNumber", _StepProcessDetails.LineNumber);

                DT2.Rows.Add("Masterfile.MachineDetailBOM", "cond", "SKUCode", MachineCode);
                DT2.Rows.Add("Masterfile.MachineDetailBOM", "cond", "StepSequence", _StepProcessDetails.LineNumber);

                DT3.Rows.Add("Masterfile.MachineDetailMachine", "cond", "SKUCode", MachineCode);
                DT3.Rows.Add("Masterfile.MachineDetailMachine", "cond", "StepSequence", _StepProcessDetails.LineNumber);


                DT4.Rows.Add("Masterfile.MachineDetailManpower", "cond", "SKUCode", MachineCode);
                DT4.Rows.Add("Masterfile.MachineDetailManpower", "cond", "StepSequence", _StepProcessDetails.LineNumber);


                Gears.DeleteData(DT1, Conn);
                Gears.DeleteData(DT2, Conn);
                Gears.DeleteData(DT3, Conn);
                Gears.DeleteData(DT4, Conn);
            }
        }
        public class PMChecklist
        {
            public virtual string Code { get; set; }
            public virtual string PMNumber { get; set; }
            public virtual string LineNumber { get; set; }
            public virtual string ActivityTask { get; set; }
            public virtual string Remarks { get; set; }
            public virtual string Conformance { get; set; }
            public virtual string NonConformance { get; set; }


            public DataTable getStepProcessBOM(string PMNumber, int LineNumber, string Conn)
            {
                
                DataTable a;
                try
                {
                    string strStepProcessBOM = "SELECT RecordID,PMNumber,LineNumber,Code,ActivityTask,Remarks,Conformance,NonConformance FROM Masterfile.MachinePMChecklist WHERE PMNumber='" + PMNumber + "' AND LineNumber='" + LineNumber + "' ORDER BY LineNumber ASC";
                    a = Gears.RetriveData2(strStepProcessBOM, Conn);

                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }

            public void AddStepProcessBOM(PMChecklist _StepProcessBOMDetails)
            {
                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

                DT1.Rows.Add("Masterfile.MachinePMChecklist", "0", "Code", Code);
                DT1.Rows.Add("Masterfile.MachinePMChecklist", "0", "PMNumber", _StepProcessBOMDetails.PMNumber);
                DT1.Rows.Add("Masterfile.MachinePMChecklist", "0", "LineNumber", _StepProcessBOMDetails.LineNumber);
                DT1.Rows.Add("Masterfile.MachinePMChecklist", "0", "ActivityTask", _StepProcessBOMDetails.ActivityTask);
                DT1.Rows.Add("Masterfile.MachinePMChecklist", "0", "Remarks", _StepProcessBOMDetails.Remarks);
                DT1.Rows.Add("Masterfile.MachinePMChecklist", "0", "Conformance", _StepProcessBOMDetails.Conformance);
                DT1.Rows.Add("Masterfile.MachinePMChecklist", "0", "NonConformance", _StepProcessBOMDetails.NonConformance);
              

                Gears.CreateData(DT1, Conn);

            }

            public void UpdateStepProcessBOM(PMChecklist _StepProcessBOMDetails)
            {
                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

                DT1.Rows.Add("Masterfile.MachinePMChecklist", "cond", "Code", Code);
                DT1.Rows.Add("Masterfile.MachinePMChecklist", "cond", "PMNumber", _StepProcessBOMDetails.PMNumber);
                DT1.Rows.Add("Masterfile.MachinePMChecklist", "set", "LineNumber", _StepProcessBOMDetails.LineNumber);
                DT1.Rows.Add("Masterfile.MachinePMChecklist", "set", "ActivityTask", _StepProcessBOMDetails.ActivityTask);
                DT1.Rows.Add("Masterfile.MachinePMChecklist", "set", "Remarks", _StepProcessBOMDetails.Remarks);
                DT1.Rows.Add("Masterfile.MachinePMChecklist", "set", "Conformance", _StepProcessBOMDetails.Conformance);
                DT1.Rows.Add("Masterfile.MachinePMChecklist", "set", "NonConformance", _StepProcessBOMDetails.NonConformance);
               
                Gears.UpdateData(DT1, Conn);

            }

            public void DeleteStepProcessBOM(PMChecklist _StepProcessBOMDetails)
            {
                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

                DT1.Rows.Add("Masterfile.MachinePMChecklistBOM", "cond", "PMNumber", PMNumber);
                DT1.Rows.Add("Masterfile.MachinePMChecklistBOM", "cond", "LineNumber", _StepProcessBOMDetails.LineNumber);

                Gears.DeleteData(DT1, Conn);
            }
        }

        public class MaterialReq
        {
            public virtual string MachineCode { get; set; }
            public virtual string PMNumber { get; set; }
            public virtual string PMCNumber { get; set; }
            public virtual string LineNumber { get; set; }
            public virtual string MaterialType { get; set; }
            public virtual string Remarks { get; set; }
            public virtual string Quantity { get; set; }



            public DataTable getMaterialReq(string PMCNumber, string PMNumber, int LineNumber, string Conn)
            {

                DataTable a;
                try
                {
                    string strStepProcessBOM = "SELECT RecordID,PMNumber,PMCNumber,LineNumber,MachineCode,MaterialType,Qty,Remarks FROM Masterfile.MachineMaterialReq WHERE PMCNumber='" + PMCNumber + "' AND PMNumber='" + PMNumber + "' AND LineNumber='" + LineNumber + "' ORDER BY LineNumber ASC";
                    a = Gears.RetriveData2(strStepProcessBOM, Conn);

                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }

            public void AddStepProcessBOM(PMChecklist _StepProcessBOMDetails)
            {
                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

                DT1.Rows.Add("Masterfile.MachinePMChecklist", "0", "Code", MachineCode);
                DT1.Rows.Add("Masterfile.MachinePMChecklist", "0", "PMNumber", _StepProcessBOMDetails.PMNumber);
                DT1.Rows.Add("Masterfile.MachinePMChecklist", "0", "LineNumber", _StepProcessBOMDetails.LineNumber);
                DT1.Rows.Add("Masterfile.MachinePMChecklist", "0", "ActivityTask", _StepProcessBOMDetails.ActivityTask);
                DT1.Rows.Add("Masterfile.MachinePMChecklist", "0", "Remarks", _StepProcessBOMDetails.Remarks);
                DT1.Rows.Add("Masterfile.MachinePMChecklist", "0", "Conformance", _StepProcessBOMDetails.Conformance);
                DT1.Rows.Add("Masterfile.MachinePMChecklist", "0", "NonConformance", _StepProcessBOMDetails.NonConformance);


                Gears.CreateData(DT1, Conn);

            }

            public void UpdateStepProcessBOM(PMChecklist _StepProcessBOMDetails)
            {
                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

                DT1.Rows.Add("Masterfile.MachinePMChecklist", "cond", "Code", MachineCode);
                DT1.Rows.Add("Masterfile.MachinePMChecklist", "cond", "PMNumber", _StepProcessBOMDetails.PMNumber);
                DT1.Rows.Add("Masterfile.MachinePMChecklist", "set", "LineNumber", _StepProcessBOMDetails.LineNumber);
                DT1.Rows.Add("Masterfile.MachinePMChecklist", "set", "ActivityTask", _StepProcessBOMDetails.ActivityTask);
                DT1.Rows.Add("Masterfile.MachinePMChecklist", "set", "Remarks", _StepProcessBOMDetails.Remarks);
                DT1.Rows.Add("Masterfile.MachinePMChecklist", "set", "Conformance", _StepProcessBOMDetails.Conformance);
                DT1.Rows.Add("Masterfile.MachinePMChecklist", "set", "NonConformance", _StepProcessBOMDetails.NonConformance);

                Gears.UpdateData(DT1, Conn);

            }

            public void DeleteStepProcessBOM(PMChecklist _StepProcessBOMDetails)
            {
                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

                DT1.Rows.Add("Masterfile.MachinePMChecklistBOM", "cond", "PMNumber", PMNumber);
                DT1.Rows.Add("Masterfile.MachinePMChecklistBOM", "cond", "LineNumber", _StepProcessBOMDetails.LineNumber);

                Gears.DeleteData(DT1, Conn);
            }
        }
        public class ProdRoutingStepMachine
        {
            public virtual string SKUCode { get; set; }
            public virtual string StepSequence { get; set; }
            public virtual string StepCode { get; set; }
            public virtual string MachineType { get; set; }
            public virtual string Location { get; set; }
            public virtual string MachineRun { get; set; }
            public virtual string Unit { get; set; }
            public virtual string MachineCapacityQty { get; set; }
            public virtual string MachineCapacityUnit { get; set; }
            public virtual string CostPerUnit { get; set; }

            public DataTable getStepProcessMachine(string SKUCode, int StepSequence, string Conn)
            {

                DataTable a;
                try
                {
                    string strStepProcessMachine = "SELECT * FROM Masterfile.MachineStepMachine WHERE SKUCode='" + SKUCode + "' AND StepSequence='" + StepSequence + "' ORDER BY StepSequence ASC";
                    a = Gears.RetriveData2(strStepProcessMachine, Conn);

                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }

            public void AddStepProcessMachine(ProdRoutingStepMachine _StepProcessMachineDetails)
            {
                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

                DT1.Rows.Add("Masterfile.MachineStepMachine", "0", "SKUCode", SKUCode);
                DT1.Rows.Add("Masterfile.MachineStepMachine", "0", "StepSequence", _StepProcessMachineDetails.StepSequence);
                DT1.Rows.Add("Masterfile.MachineStepMachine", "0", "StepCode", _StepProcessMachineDetails.StepCode);
                DT1.Rows.Add("Masterfile.MachineStepMachine", "0", "MachineType", _StepProcessMachineDetails.MachineType);
                DT1.Rows.Add("Masterfile.MachineStepMachine", "0", "Location", _StepProcessMachineDetails.Location);
                DT1.Rows.Add("Masterfile.MachineStepMachine", "0", "MachineRun", _StepProcessMachineDetails.MachineRun);
                DT1.Rows.Add("Masterfile.MachineStepMachine", "0", "Unit", _StepProcessMachineDetails.Unit);
                DT1.Rows.Add("Masterfile.MachineStepMachine", "0", "MachineCapacityQty", _StepProcessMachineDetails.MachineCapacityQty);
                DT1.Rows.Add("Masterfile.MachineStepMachine", "0", "MachineCapacityUnit", _StepProcessMachineDetails.MachineCapacityUnit);
                DT1.Rows.Add("Masterfile.MachineStepMachine", "0", "CostPerUnit", _StepProcessMachineDetails.CostPerUnit);

                Gears.CreateData(DT1, Conn);

            }

            public void UpdateStepProcessMachine(ProdRoutingStepMachine _StepProcessMachineDetails)
            {
                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

                DT1.Rows.Add("Masterfile.MachineStepMachine", "cond", "SKUCode", SKUCode);
                DT1.Rows.Add("Masterfile.MachineStepMachine", "cond", "StepSequence", _StepProcessMachineDetails.StepSequence);
                DT1.Rows.Add("Masterfile.MachineStepMachine", "set", "StepCode", _StepProcessMachineDetails.StepCode);
                DT1.Rows.Add("Masterfile.MachineStepMachine", "set", "MachineType", _StepProcessMachineDetails.MachineType);
                DT1.Rows.Add("Masterfile.MachineStepMachine", "set", "Location", _StepProcessMachineDetails.Location);
                DT1.Rows.Add("Masterfile.MachineStepMachine", "set", "MachineRun", _StepProcessMachineDetails.MachineRun);
                DT1.Rows.Add("Masterfile.MachineStepMachine", "set", "Unit", _StepProcessMachineDetails.Unit);
                DT1.Rows.Add("Masterfile.MachineStepMachine", "set", "MachineCapacityQty", _StepProcessMachineDetails.MachineCapacityQty);
                DT1.Rows.Add("Masterfile.MachineStepMachine", "set", "MachineCapacityUnit", _StepProcessMachineDetails.MachineCapacityUnit);
                DT1.Rows.Add("Masterfile.MachineStepMachine", "set", "CostPerUnit", _StepProcessMachineDetails.CostPerUnit);

                Gears.UpdateData(DT1, Conn);

            }

            public void DeleteStepProcessMachine(ProdRoutingStepMachine _StepProcessMachineDetails)
            {
                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

                DT1.Rows.Add("Masterfile.MachineStepMachine", "cond", "SKUCode", SKUCode);
                DT1.Rows.Add("Masterfile.MachineStepMachine", "cond", "StepSequence", _StepProcessMachineDetails.StepSequence);

                Gears.DeleteData(DT1, Conn);
            }
        }
        public class ProdRoutingStepManpower
        {
            public virtual string SKUCode { get; set; }
            public virtual string StepSequence { get; set; }
            public virtual string StepCode { get; set; }
            public virtual string Designation { get; set; }
            public virtual string NoManpower { get; set; }
            public virtual string NoHour { get; set; }
            public virtual string StandardRate { get; set; }
            public virtual string StandardRateUnit { get; set; }
            public virtual string CostPerUnit { get; set; }

            public DataTable getStepProcessManpower(string SKUCode, int StepSequence, string Conn)
            {

                DataTable a;
                try
                {
                    string strStepProcessManpower = "SELECT * FROM Masterfile.MachineStepManpower WHERE SKUCode='" + SKUCode + "' AND StepSequence='" + StepSequence + "' ORDER BY StepSequence ASC";
                    a = Gears.RetriveData2(strStepProcessManpower, Conn);

                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }

            public void AddStepProcessManpower(ProdRoutingStepManpower _StepProcessManpowerDetails)
            {
                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

                DT1.Rows.Add("Masterfile.MachineStepManpower", "0", "SKUCode", SKUCode);
                DT1.Rows.Add("Masterfile.MachineStepManpower", "0", "StepSequence", _StepProcessManpowerDetails.StepSequence);
                DT1.Rows.Add("Masterfile.MachineStepManpower", "0", "StepCode", _StepProcessManpowerDetails.StepCode);
                DT1.Rows.Add("Masterfile.MachineStepManpower", "0", "Designation", _StepProcessManpowerDetails.Designation);
                DT1.Rows.Add("Masterfile.MachineStepManpower", "0", "NoManpower", _StepProcessManpowerDetails.NoManpower);
                DT1.Rows.Add("Masterfile.MachineStepManpower", "0", "NoHour", _StepProcessManpowerDetails.NoHour);
                DT1.Rows.Add("Masterfile.MachineStepManpower", "0", "StandardRate", _StepProcessManpowerDetails.StandardRate);
                DT1.Rows.Add("Masterfile.MachineStepManpower", "0", "StandardRateUnit", _StepProcessManpowerDetails.StandardRateUnit);
                DT1.Rows.Add("Masterfile.MachineStepManpower", "0", "CostPerUnit", _StepProcessManpowerDetails.CostPerUnit);

                Gears.CreateData(DT1, Conn);

            }

            public void UpdateStepProcessManpower(ProdRoutingStepManpower _StepProcessManpowerDetails)
            {
                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

                DT1.Rows.Add("Masterfile.MachineStepManpower", "cond", "SKUCode", SKUCode);
                DT1.Rows.Add("Masterfile.MachineStepManpower", "cond", "StepSequence", _StepProcessManpowerDetails.StepSequence);
                DT1.Rows.Add("Masterfile.MachineStepManpower", "set", "StepCode", _StepProcessManpowerDetails.StepCode);
                DT1.Rows.Add("Masterfile.MachineStepManpower", "set", "Designation", _StepProcessManpowerDetails.Designation);
                DT1.Rows.Add("Masterfile.MachineStepManpower", "set", "NoManpower", _StepProcessManpowerDetails.NoManpower);
                DT1.Rows.Add("Masterfile.MachineStepManpower", "set", "NoHour", _StepProcessManpowerDetails.NoHour);
                DT1.Rows.Add("Masterfile.MachineStepManpower", "set", "StandardRate", _StepProcessManpowerDetails.StandardRate);
                DT1.Rows.Add("Masterfile.MachineStepManpower", "set", "StandardRateUnit", _StepProcessManpowerDetails.StandardRateUnit);
                DT1.Rows.Add("Masterfile.MachineStepManpower", "set", "CostPerUnit", _StepProcessManpowerDetails.CostPerUnit);

                Gears.UpdateData(DT1, Conn);

            }

            public void DeleteStepProcessManpower(ProdRoutingStepManpower _StepProcessManpowerDetails)
            {
                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

                DT1.Rows.Add("Masterfile.MachineStepManpower", "cond", "SKUCode", SKUCode);
                DT1.Rows.Add("Masterfile.MachineStepManpower", "cond", "StepSequence", _StepProcessManpowerDetails.StepSequence);

                Gears.DeleteData(DT1, Conn);
            }
        }
        

        public DataTable getdata(string MachineID, string Conn)
        {
            DataTable a;

            a = Gears.RetriveData2("SELECT RecordID,MachineID,MachineName,MachineType,Description,AssetCode,AssetTag,MachineCategory,Brand,Model,SerialNo,SupplyVoltage,Location,AssignedPersonnel,Status,Section,Manual,DateAcquired,IsInactive,AddedBy,AddedDate,LastEditedBy,LastEditedDate,ActivatedBy,ActivatedDate,DeactivatedBy,DeactivatedDate FROM Masterfile.MachineMaster where MachineID = '" + MachineID + "'", Conn);

            foreach (DataRow dtRow in a.Rows)
            {
                RecordID = dtRow["RecordID"].ToString();
                MachineID = dtRow["MachineID"].ToString();
                MachineName = dtRow["MachineName"].ToString();
                MachineType = dtRow["MachineType"].ToString();
                Description = dtRow["Description"].ToString();
                AssetCode = dtRow["AssetCode"].ToString();
                AssetTag = dtRow["AssetTag"].ToString();
                MachineCategory = dtRow["MachineCategory"].ToString();
                Brand = dtRow["Brand"].ToString();
                Model = dtRow["Model"].ToString();
                SerialNo = dtRow["SerialNo"].ToString();
                SupplyVoltage = dtRow["SupplyVoltage"].ToString();
                Location = dtRow["Location"].ToString();
                AssignedPersonnel = dtRow["AssignedPersonnel"].ToString();
                Status = dtRow["Status"].ToString();
                Section = dtRow["Section"].ToString();
                Manual = dtRow["Manual"].ToString();
                DateAcquired = dtRow["DateAcquired"].ToString();
                AddedBy = dtRow["AddedBy"].ToString();
                AddedDate = dtRow["AddedDate"].ToString();
                LastEditedBy = dtRow["LastEditedBy"].ToString();
                LastEditedDate = dtRow["LastEditedDate"].ToString();
                ActivatedBy = dtRow["ActivatedBy"].ToString();
                ActivatedDate = dtRow["ActivatedDate"].ToString();
                DeactivatedBy = dtRow["DeactivatedBy"].ToString();
                DeactivatedDate = dtRow["DeactivatedDate"].ToString();
            }

            return a;
        }
        public void InsertData(MachineMaster _ent)
        {
            MachineID = _ent.MachineID;
            Conn = _ent.Connection;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add("Masterfile.MachineMaster", "0", "MachineID", _ent.MachineID);
            DT1.Rows.Add("Masterfile.MachineMaster", "0", "MachineName", _ent.MachineName);
            DT1.Rows.Add("Masterfile.MachineMaster", "0", "MachineType", _ent.MachineType);
            DT1.Rows.Add("Masterfile.MachineMaster", "0", "AssetCode", _ent.AssetCode);
            DT1.Rows.Add("Masterfile.MachineMaster", "0", "AssetTag", _ent.AssetTag);
            DT1.Rows.Add("Masterfile.MachineMaster", "0", "MachineCategory", _ent.MachineCategory);
            DT1.Rows.Add("Masterfile.MachineMaster", "0", "Description", _ent.Description);
            DT1.Rows.Add("Masterfile.MachineMaster", "0", "Brand", _ent.Brand);
            DT1.Rows.Add("Masterfile.MachineMaster", "0", "Model", _ent.Model);
            DT1.Rows.Add("Masterfile.MachineMaster", "0", "SerialNo", _ent.SerialNo);
            DT1.Rows.Add("Masterfile.MachineMaster", "0", "SupplyVoltage", _ent.SupplyVoltage);
            DT1.Rows.Add("Masterfile.MachineMaster", "0", "Location", _ent.Location);
            DT1.Rows.Add("Masterfile.MachineMaster", "0", "AssignedPersonnel", _ent.AssignedPersonnel);
            DT1.Rows.Add("Masterfile.MachineMaster", "0", "Status", "Operational");
            DT1.Rows.Add("Masterfile.MachineMaster", "0", "Section", _ent.Section);
            DT1.Rows.Add("Masterfile.MachineMaster", "0", "Manual", _ent.Manual);
            DT1.Rows.Add("Masterfile.MachineMaster", "0", "DateAcquired", _ent.DateAcquired);

            DT1.Rows.Add("Masterfile.MachineMaster", "0", "IsInactive", 0);
            DT1.Rows.Add("Masterfile.MachineMaster", "0", "AddedBy", _ent.AddedBy);
            DT1.Rows.Add("Masterfile.MachineMaster", "0", "AddedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        

            Gears.CreateData(DT1, _ent.Connection);
        }
        public void UpdateData(MachineMaster _ent)
        {
            MachineID = _ent.MachineID;
            Conn = _ent.Connection;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

           


            DT1.Rows.Add("Masterfile.MachineMaster", "cond", "MachineID", _ent.MachineID);
            DT1.Rows.Add("Masterfile.MachineMaster", "cond", "RecordID", _ent.RecordID);
            DT1.Rows.Add("Masterfile.MachineMaster", "set", "MachineName", _ent.MachineName);
            DT1.Rows.Add("Masterfile.MachineMaster", "set", "MachineType", _ent.MachineType);
            DT1.Rows.Add("Masterfile.MachineMaster", "set", "AssetCode", _ent.AssetCode);
            DT1.Rows.Add("Masterfile.MachineMaster", "set", "AssetTag", _ent.AssetTag);
            DT1.Rows.Add("Masterfile.MachineMaster", "set", "MachineCategory", _ent.MachineCategory);
            DT1.Rows.Add("Masterfile.MachineMaster", "set", "Description", _ent.Description);
            DT1.Rows.Add("Masterfile.MachineMaster", "set", "Brand", _ent.Brand);
            DT1.Rows.Add("Masterfile.MachineMaster", "set", "Model", _ent.Model);
            DT1.Rows.Add("Masterfile.MachineMaster", "set", "SerialNo", _ent.SerialNo);
            DT1.Rows.Add("Masterfile.MachineMaster", "set", "SupplyVoltage", _ent.SupplyVoltage);
            DT1.Rows.Add("Masterfile.MachineMaster", "set", "Location", _ent.Location);
            DT1.Rows.Add("Masterfile.MachineMaster", "set", "AssignedPersonnel", _ent.AssignedPersonnel);
            DT1.Rows.Add("Masterfile.MachineMaster", "set", "Status", _ent.Status);
            DT1.Rows.Add("Masterfile.MachineMaster", "set", "Section", _ent.Section);
            DT1.Rows.Add("Masterfile.MachineMaster", "set", "Manual", _ent.Manual);
            DT1.Rows.Add("Masterfile.MachineMaster", "set", "DateAcquired", _ent.DateAcquired);




            DT1.Rows.Add("Masterfile.MachineMaster", "set", "LastEditedBy", _ent.LastEditedBy);
            DT1.Rows.Add("Masterfile.MachineMaster", "set", "LastEditedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            Gears.UpdateData(DT1, _ent.Connection);
            Functions.AuditTrail("REFBLDG", MachineID, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "UPDATE", _ent.Connection);
        }
        public void DeleteData(MachineMaster _ent)
        {
            MachineID = _ent.MachineID;
            Conn = _ent.Connection;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add("Masterfile.MachineMaster", "cond", "SKUCode", _ent.MachineID);

            Gears.DeleteData(DT1, _ent.Connection);
            Functions.AuditTrail("REFBLDG", MachineID, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "DELETE", _ent.Connection);
        }
    }
}
