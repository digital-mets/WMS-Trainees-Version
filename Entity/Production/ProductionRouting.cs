using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GearsLibrary;
using System.Data;

namespace Entity
{
    public class ProductionRouting
    {
        private static string Conn;
        public virtual string Connection { get; set; }
        public virtual string RecordID { get; set; }
        public virtual string SKUCode { get; set; }
        public virtual string SKUDescription { get; set; }
        public virtual string CustomerCode { get; set; }
        public virtual string CustomerName { get; set; }
        public virtual string UnitMeasure { get; set; }
        public virtual decimal ExpectedOutputQty { get; set; }
        public virtual string Status { get; set; }
        public virtual string EffectivityDate { get; set; }
        public virtual string Remarks { get; set; }
        public virtual string AddedBy { get; set; }
        public virtual string AddedDate { get; set; }
        public virtual string LastEditedBy { get; set; }
        public virtual string LastEditedDate { get; set; }
        public virtual string ActivatedBy { get; set; }
        public virtual string ActivatedDate { get; set; }
        public virtual string DeactivatedBy { get; set; }
        public virtual string DeactivatedDate { get; set; }

        public virtual IList<ProdRoutingStep> StepProcessDetail { get; set; }
        public virtual IList<ProdRoutingStepBOM> StepProcessBOMDetail { get; set; }
        public virtual IList<ProdRoutingStepMachine> StepProcessMachineDetail { get; set; }
        public virtual IList<ProdRoutingStepManpower> StepProcessManpowerDetail { get; set; }
        public virtual IList<ProdRoutingOtherMaterial> OtherMaterialDetail { get; set; }

        public class ProdRoutingStep
        {
            public virtual string SKUCode { get; set; }
            public virtual string StepSequence { get; set; }
            public virtual string StepCode { get; set; }
            public virtual string StepDescription { get; set; }
            public virtual string SequenceDay { get; set; }

            public DataTable getStepProcess(string SKUCode, string Conn)
            {
                DataTable a;
                try
                {
                    a = Gears.RetriveData2("SELECT SKUCode, StepSequence, A.StepCode, B.Description AS StepDescription, SequenceDay FROM Production.ProdRoutingStep A LEFT JOIN Masterfile.Step B ON A.StepCode = B.StepCode WHERE SKUCode='" + SKUCode + "' ORDER BY StepSequence ASC", Conn);
                    
                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }
            public void AddStepProcess(ProdRoutingStep _StepProcessDetails, string Conn)
            {
                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();

                DT1.Rows.Add("Production.ProdRoutingStep", "cond", "SKUCode", SKUCode);
                Gears.DeleteData(DT1, Conn);

                DT2.Rows.Add("Production.ProdRoutingStep", "0", "SKUCode", SKUCode);
                DT2.Rows.Add("Production.ProdRoutingStep", "0", "StepSequence", _StepProcessDetails.StepSequence);
                DT2.Rows.Add("Production.ProdRoutingStep", "0", "StepCode", _StepProcessDetails.StepCode);
                DT2.Rows.Add("Production.ProdRoutingStep", "0", "StepDescription", _StepProcessDetails.StepDescription);
                DT2.Rows.Add("Production.ProdRoutingStep", "0", "SequenceDay", _StepProcessDetails.SequenceDay);

                Gears.CreateData(DT2, Conn);

            }

            public void UpdateStepProcess(ProdRoutingStep _StepProcessDetails)
            {
                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

                DT1.Rows.Add("Production.ProdRoutingStep", "cond", "SKUCode", SKUCode);
                DT1.Rows.Add("Production.ProdRoutingStep", "cond", "StepSequence", _StepProcessDetails.StepSequence);
                DT1.Rows.Add("Production.ProdRoutingStep", "set", "StepCode", _StepProcessDetails.StepCode);
                DT1.Rows.Add("Production.ProdRoutingStep", "set", "StepDescription", _StepProcessDetails.StepDescription);
                DT1.Rows.Add("Production.ProdRoutingStep", "set", "SequenceDay", _StepProcessDetails.SequenceDay);

                Gears.UpdateData(DT1, Conn);

            }

            public void DeleteStepProcess(ProdRoutingStep _StepProcessDetails)
            {
                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT3 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT4 = new Gears.CRUDdatatable();

                DT1.Rows.Add("Production.ProdRoutingStep", "cond", "SKUCode", SKUCode);
                DT1.Rows.Add("Production.ProdRoutingStep", "cond", "StepSequence", _StepProcessDetails.StepSequence);

                DT2.Rows.Add("Production.ProdRoutingStepBOM", "cond", "SKUCode", SKUCode);
                DT2.Rows.Add("Production.ProdRoutingStepBOM", "cond", "StepSequence", _StepProcessDetails.StepSequence);

                DT3.Rows.Add("Production.ProdRoutingStepMachine", "cond", "SKUCode", SKUCode);
                DT3.Rows.Add("Production.ProdRoutingStepMachine", "cond", "StepSequence", _StepProcessDetails.StepSequence);

                DT4.Rows.Add("Production.ProdRoutingStepManpower", "cond", "SKUCode", SKUCode);
                DT4.Rows.Add("Production.ProdRoutingStepManpower", "cond", "StepSequence", _StepProcessDetails.StepSequence);

                Gears.DeleteData(DT1, Conn);
                Gears.DeleteData(DT2, Conn);
                Gears.DeleteData(DT3, Conn);
                Gears.DeleteData(DT4, Conn);
            }
        }
        public class ProdRoutingStepBOM
        {
            public virtual string SKUCode { get; set; }
            public virtual string StepSequence { get; set; }
            public virtual string StepCode { get; set; }
            public virtual string Unit { get; set; }
            public virtual string ConsumptionPerProduct { get; set; }
            public virtual string TotalConsumption { get; set; }
            public virtual string PercentageAllowance { get; set; }
            public virtual string QtyAllowance { get; set; }
            public virtual string ClientSuppliedMaterial { get; set; }
            public virtual string EstimatedUnitCost { get; set; }
            public virtual string StandardUsage { get; set; }
            public virtual string Remarks { get; set; }

            public DataTable getStepProcessBOM(string SKUCode, int StepSequence, string Conn)
            {

                DataTable a;
                try
                {
                    string strStepProcessBOM = "SELECT A.*, B.FullDesc AS SKUDescription FROM Production.ProdRoutingStepBOM A LEFT JOIN Masterfile.Item B ON A.SKUCode = B.ItemCode WHERE SKUCode='" + SKUCode + "' AND StepSequence='" + StepSequence + "' ORDER BY StepSequence ASC";
                    a = Gears.RetriveData2(strStepProcessBOM, Conn);

                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }

            public void AddStepProcessBOM(ProdRoutingStepBOM _StepProcessBOMDetails)
            {
                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

                DT1.Rows.Add("Production.ProdRoutingStep", "0", "SKUCode", SKUCode);
                DT1.Rows.Add("Production.ProdRoutingStep", "0", "StepSequence", _StepProcessBOMDetails.StepSequence);
                DT1.Rows.Add("Production.ProdRoutingStep", "0", "StepCode", _StepProcessBOMDetails.StepCode);
                DT1.Rows.Add("Production.ProdRoutingStep", "0", "Unit", _StepProcessBOMDetails.Unit);
                DT1.Rows.Add("Production.ProdRoutingStep", "0", "ConsumptionPerProduct", _StepProcessBOMDetails.ConsumptionPerProduct);
                DT1.Rows.Add("Production.ProdRoutingStep", "0", "TotalConsumption", _StepProcessBOMDetails.TotalConsumption);
                DT1.Rows.Add("Production.ProdRoutingStep", "0", "PercentageAllowance", _StepProcessBOMDetails.PercentageAllowance);
                DT1.Rows.Add("Production.ProdRoutingStep", "0", "QtyAllowance", _StepProcessBOMDetails.QtyAllowance);
                DT1.Rows.Add("Production.ProdRoutingStep", "0", "ClientSuppliedMaterial", _StepProcessBOMDetails.ClientSuppliedMaterial);
                DT1.Rows.Add("Production.ProdRoutingStep", "0", "EstimatedUnitCost", _StepProcessBOMDetails.EstimatedUnitCost);
                DT1.Rows.Add("Production.ProdRoutingStep", "0", "StandardUsage", _StepProcessBOMDetails.StandardUsage);
                DT1.Rows.Add("Production.ProdRoutingStep", "0", "Remarks", _StepProcessBOMDetails.Remarks);

                Gears.CreateData(DT1, Conn);

            }

            public void UpdateStepProcessBOM(ProdRoutingStepBOM _StepProcessBOMDetails)
            {
                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

                DT1.Rows.Add("Production.ProdRoutingStep", "cond", "SKUCode", SKUCode);
                DT1.Rows.Add("Production.ProdRoutingStep", "cond", "StepSequence", _StepProcessBOMDetails.StepSequence);
                DT1.Rows.Add("Production.ProdRoutingStep", "set", "StepCode", _StepProcessBOMDetails.StepCode);
                DT1.Rows.Add("Production.ProdRoutingStep", "set", "Unit", _StepProcessBOMDetails.Unit);
                DT1.Rows.Add("Production.ProdRoutingStep", "set", "ConsumptionPerProduct", _StepProcessBOMDetails.ConsumptionPerProduct);
                DT1.Rows.Add("Production.ProdRoutingStep", "set", "TotalConsumption", _StepProcessBOMDetails.TotalConsumption);
                DT1.Rows.Add("Production.ProdRoutingStep", "set", "PercentageAllowance", _StepProcessBOMDetails.PercentageAllowance);
                DT1.Rows.Add("Production.ProdRoutingStep", "set", "QtyAllowance", _StepProcessBOMDetails.QtyAllowance);
                DT1.Rows.Add("Production.ProdRoutingStep", "set", "ClientSuppliedMaterial", _StepProcessBOMDetails.ClientSuppliedMaterial);
                DT1.Rows.Add("Production.ProdRoutingStep", "set", "EstimatedUnitCost", _StepProcessBOMDetails.EstimatedUnitCost);
                DT1.Rows.Add("Production.ProdRoutingStep", "set", "StandardUsage", _StepProcessBOMDetails.StandardUsage);
                DT1.Rows.Add("Production.ProdRoutingStep", "set", "Remarks", _StepProcessBOMDetails.Remarks);

                Gears.UpdateData(DT1, Conn);

            }

            public void DeleteStepProcessBOM(ProdRoutingStepBOM _StepProcessBOMDetails)
            {
                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

                DT1.Rows.Add("Production.ProdRoutingStepBOM", "cond", "SKUCode", SKUCode);
                DT1.Rows.Add("Production.ProdRoutingStepBOM", "cond", "StepSequence", _StepProcessBOMDetails.StepSequence);

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
                    string strStepProcessMachine = "SELECT * FROM Production.ProdRoutingStepMachine WHERE SKUCode='" + SKUCode + "' AND StepSequence='" + StepSequence + "' ORDER BY StepSequence ASC";
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

                DT1.Rows.Add("Production.ProdRoutingStepMachine", "0", "SKUCode", SKUCode);
                DT1.Rows.Add("Production.ProdRoutingStepMachine", "0", "StepSequence", _StepProcessMachineDetails.StepSequence);
                DT1.Rows.Add("Production.ProdRoutingStepMachine", "0", "StepCode", _StepProcessMachineDetails.StepCode);
                DT1.Rows.Add("Production.ProdRoutingStepMachine", "0", "MachineType", _StepProcessMachineDetails.MachineType);
                DT1.Rows.Add("Production.ProdRoutingStepMachine", "0", "Location", _StepProcessMachineDetails.Location);
                DT1.Rows.Add("Production.ProdRoutingStepMachine", "0", "MachineRun", _StepProcessMachineDetails.MachineRun);
                DT1.Rows.Add("Production.ProdRoutingStepMachine", "0", "Unit", _StepProcessMachineDetails.Unit);
                DT1.Rows.Add("Production.ProdRoutingStepMachine", "0", "MachineCapacityQty", _StepProcessMachineDetails.MachineCapacityQty);
                DT1.Rows.Add("Production.ProdRoutingStepMachine", "0", "MachineCapacityUnit", _StepProcessMachineDetails.MachineCapacityUnit);
                DT1.Rows.Add("Production.ProdRoutingStepMachine", "0", "CostPerUnit", _StepProcessMachineDetails.CostPerUnit);

                Gears.CreateData(DT1, Conn);

            }

            public void UpdateStepProcessMachine(ProdRoutingStepMachine _StepProcessMachineDetails)
            {
                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

                DT1.Rows.Add("Production.ProdRoutingStepMachine", "cond", "SKUCode", SKUCode);
                DT1.Rows.Add("Production.ProdRoutingStepMachine", "cond", "StepSequence", _StepProcessMachineDetails.StepSequence);
                DT1.Rows.Add("Production.ProdRoutingStepMachine", "set", "StepCode", _StepProcessMachineDetails.StepCode);
                DT1.Rows.Add("Production.ProdRoutingStepMachine", "set", "MachineType", _StepProcessMachineDetails.MachineType);
                DT1.Rows.Add("Production.ProdRoutingStepMachine", "set", "Location", _StepProcessMachineDetails.Location);
                DT1.Rows.Add("Production.ProdRoutingStepMachine", "set", "MachineRun", _StepProcessMachineDetails.MachineRun);
                DT1.Rows.Add("Production.ProdRoutingStepMachine", "set", "Unit", _StepProcessMachineDetails.Unit);
                DT1.Rows.Add("Production.ProdRoutingStepMachine", "set", "MachineCapacityQty", _StepProcessMachineDetails.MachineCapacityQty);
                DT1.Rows.Add("Production.ProdRoutingStepMachine", "set", "MachineCapacityUnit", _StepProcessMachineDetails.MachineCapacityUnit);
                DT1.Rows.Add("Production.ProdRoutingStepMachine", "set", "CostPerUnit", _StepProcessMachineDetails.CostPerUnit);

                Gears.UpdateData(DT1, Conn);

            }

            public void DeleteStepProcessMachine(ProdRoutingStepMachine _StepProcessMachineDetails)
            {
                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

                DT1.Rows.Add("Production.ProdRoutingStepMachine", "cond", "SKUCode", SKUCode);
                DT1.Rows.Add("Production.ProdRoutingStepMachine", "cond", "StepSequence", _StepProcessMachineDetails.StepSequence);

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
                    string strStepProcessManpower = "SELECT * FROM Production.ProdRoutingStepManpower WHERE SKUCode='" + SKUCode + "' AND StepSequence='" + StepSequence + "' ORDER BY StepSequence ASC";
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

                DT1.Rows.Add("Production.ProdRoutingStepManpower", "0", "SKUCode", SKUCode);
                DT1.Rows.Add("Production.ProdRoutingStepManpower", "0", "StepSequence", _StepProcessManpowerDetails.StepSequence);
                DT1.Rows.Add("Production.ProdRoutingStepManpower", "0", "StepCode", _StepProcessManpowerDetails.StepCode);
                DT1.Rows.Add("Production.ProdRoutingStepManpower", "0", "Designation", _StepProcessManpowerDetails.Designation);
                DT1.Rows.Add("Production.ProdRoutingStepManpower", "0", "NoManpower", _StepProcessManpowerDetails.NoManpower);
                DT1.Rows.Add("Production.ProdRoutingStepManpower", "0", "NoHour", _StepProcessManpowerDetails.NoHour);
                DT1.Rows.Add("Production.ProdRoutingStepManpower", "0", "StandardRate", _StepProcessManpowerDetails.StandardRate);
                DT1.Rows.Add("Production.ProdRoutingStepManpower", "0", "StandardRateUnit", _StepProcessManpowerDetails.StandardRateUnit);
                DT1.Rows.Add("Production.ProdRoutingStepManpower", "0", "CostPerUnit", _StepProcessManpowerDetails.CostPerUnit);

                Gears.CreateData(DT1, Conn);

            }

            public void UpdateStepProcessManpower(ProdRoutingStepManpower _StepProcessManpowerDetails)
            {
                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

                DT1.Rows.Add("Production.ProdRoutingStepManpower", "cond", "SKUCode", SKUCode);
                DT1.Rows.Add("Production.ProdRoutingStepManpower", "cond", "StepSequence", _StepProcessManpowerDetails.StepSequence);
                DT1.Rows.Add("Production.ProdRoutingStepManpower", "set", "StepCode", _StepProcessManpowerDetails.StepCode);
                DT1.Rows.Add("Production.ProdRoutingStepManpower", "set", "Designation", _StepProcessManpowerDetails.Designation);
                DT1.Rows.Add("Production.ProdRoutingStepManpower", "set", "NoManpower", _StepProcessManpowerDetails.NoManpower);
                DT1.Rows.Add("Production.ProdRoutingStepManpower", "set", "NoHour", _StepProcessManpowerDetails.NoHour);
                DT1.Rows.Add("Production.ProdRoutingStepManpower", "set", "StandardRate", _StepProcessManpowerDetails.StandardRate);
                DT1.Rows.Add("Production.ProdRoutingStepManpower", "set", "StandardRateUnit", _StepProcessManpowerDetails.StandardRateUnit);
                DT1.Rows.Add("Production.ProdRoutingStepManpower", "set", "CostPerUnit", _StepProcessManpowerDetails.CostPerUnit);

                Gears.UpdateData(DT1, Conn);

            }

            public void DeleteStepProcessManpower(ProdRoutingStepManpower _StepProcessManpowerDetails)
            {
                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

                DT1.Rows.Add("Production.ProdRoutingStepManpower", "cond", "SKUCode", SKUCode);
                DT1.Rows.Add("Production.ProdRoutingStepManpower", "cond", "StepSequence", _StepProcessManpowerDetails.StepSequence);

                Gears.DeleteData(DT1, Conn);
            }
        }
        public class ProdRoutingOtherMaterial
        {
            public virtual string SKUCode { get; set; }
            public virtual string StepSequence { get; set; }
            public virtual string StepCode { get; set; }
            public virtual string Unit { get; set; }
            public virtual string ConsumptionPerProduct { get; set; }
            public virtual string TotalConsumption { get; set; }
            public virtual string PercentageAllowance { get; set; }
            public virtual string QtyAllowance { get; set; }
            public virtual string ClientSuppliedMaterial { get; set; }
            public virtual string EstimatedUnitCost { get; set; }
            public virtual string StandardUsage { get; set; }
            public virtual string Remarks { get; set; }

            public DataTable getOtherMaterial(string SKUCode, string Conn)
            {

                DataTable a;
                try
                {
                    string strStepProcessManpower = "SELECT * FROM Production.ProdRoutingOtherMaterials WHERE SKUCode='" + SKUCode + "' ORDER BY StepSequence ASC";
                    a = Gears.RetriveData2(strStepProcessManpower, Conn);

                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }

            public void AddOtherMaterial(ProdRoutingOtherMaterial _OtherMaterialDetails)
            {
                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

                DT1.Rows.Add("Production.ProdRoutingOtherMaterials", "0", "SKUCode", SKUCode);
                DT1.Rows.Add("Production.ProdRoutingOtherMaterials", "0", "StepSequence", _OtherMaterialDetails.StepSequence);
                DT1.Rows.Add("Production.ProdRoutingOtherMaterials", "0", "StepCode", _OtherMaterialDetails.StepCode);
                DT1.Rows.Add("Production.ProdRoutingOtherMaterials", "0", "Unit", _OtherMaterialDetails.Unit);
                DT1.Rows.Add("Production.ProdRoutingOtherMaterials", "0", "ConsumptionPerProduct", _OtherMaterialDetails.ConsumptionPerProduct);
                DT1.Rows.Add("Production.ProdRoutingOtherMaterials", "0", "TotalConsumption", _OtherMaterialDetails.TotalConsumption);
                DT1.Rows.Add("Production.ProdRoutingOtherMaterials", "0", "PercentageAllowance", _OtherMaterialDetails.PercentageAllowance);
                DT1.Rows.Add("Production.ProdRoutingOtherMaterials", "0", "QtyAllowance", _OtherMaterialDetails.QtyAllowance);
                DT1.Rows.Add("Production.ProdRoutingOtherMaterials", "0", "ClientSuppliedMaterial", _OtherMaterialDetails.ClientSuppliedMaterial);
                DT1.Rows.Add("Production.ProdRoutingOtherMaterials", "0", "EstimatedUnitCost", _OtherMaterialDetails.EstimatedUnitCost);
                DT1.Rows.Add("Production.ProdRoutingOtherMaterials", "0", "StandardUsage", _OtherMaterialDetails.StandardUsage);
                DT1.Rows.Add("Production.ProdRoutingOtherMaterials", "0", "Remarks", _OtherMaterialDetails.Remarks);

                Gears.CreateData(DT1, Conn);

            }

            public void UpdateOtherMaterial(ProdRoutingOtherMaterial _OtherMaterialDetails)
            {
                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

                DT1.Rows.Add("Production.ProdRoutingOtherMaterials", "cond", "SKUCode", SKUCode);
                DT1.Rows.Add("Production.ProdRoutingOtherMaterials", "cond", "StepSequence", _OtherMaterialDetails.StepSequence);
                DT1.Rows.Add("Production.ProdRoutingOtherMaterials", "set", "StepCode", _OtherMaterialDetails.StepCode);
                DT1.Rows.Add("Production.ProdRoutingOtherMaterials", "set", "Unit", _OtherMaterialDetails.Unit);
                DT1.Rows.Add("Production.ProdRoutingOtherMaterials", "set", "ConsumptionPerProduct", _OtherMaterialDetails.ConsumptionPerProduct);
                DT1.Rows.Add("Production.ProdRoutingOtherMaterials", "set", "TotalConsumption", _OtherMaterialDetails.TotalConsumption);
                DT1.Rows.Add("Production.ProdRoutingOtherMaterials", "set", "PercentageAllowance", _OtherMaterialDetails.PercentageAllowance);
                DT1.Rows.Add("Production.ProdRoutingOtherMaterials", "set", "QtyAllowance", _OtherMaterialDetails.QtyAllowance);
                DT1.Rows.Add("Production.ProdRoutingOtherMaterials", "set", "ClientSuppliedMaterial", _OtherMaterialDetails.ClientSuppliedMaterial);
                DT1.Rows.Add("Production.ProdRoutingOtherMaterials", "set", "EstimatedUnitCost", _OtherMaterialDetails.EstimatedUnitCost);
                DT1.Rows.Add("Production.ProdRoutingOtherMaterials", "set", "StandardUsage", _OtherMaterialDetails.StandardUsage);
                DT1.Rows.Add("Production.ProdRoutingOtherMaterials", "set", "Remarks", _OtherMaterialDetails.Remarks);

                Gears.UpdateData(DT1, Conn);

            }

            public void DeleteOtherMaterial(ProdRoutingOtherMaterial _OtherMaterialDetails)
            {
                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

                DT1.Rows.Add("Production.ProdRoutingOtherMaterials", "cond", "SKUCode", SKUCode);
                DT1.Rows.Add("Production.ProdRoutingOtherMaterials", "cond", "StepSequence", _OtherMaterialDetails.StepSequence);

                Gears.DeleteData(DT1, Conn);
            }
        }

        public DataTable getdata(string skucodep, string Conn)
        {
            DataTable a;

            a = Gears.RetriveData2("SELECT A.RecordID, A.SKUCode, B.ProductName AS SKUDescription, A.CustomerCode, C.Name AS CustomerName, ISNULL(A.UnitMeasure, B.Unit) AS UnitMeasure, ExpectedOutputQty, Status, A.Remarks, A.EffectivityDate, A.AddedBy, A.AddedDate, A.LastEditedBy, A.LastEditedDate, A.ActivatedBy, A.ActivatedDate, A.DeactivatedBy, A.DeactivatedDate FROM Production.ProdRouting A LEFT JOIN Masterfile.FGSKU B ON A.SKUCode = B.SKUCode LEFT JOIN Masterfile.BizPartner C ON A.CustomerCode = C.BizPartnerCode WHERE A.SKUCode='" + skucodep + "'", Conn);

            foreach (DataRow dtRow in a.Rows)
            {
                RecordID = dtRow["RecordID"].ToString();
                SKUCode = dtRow["SKUCode"].ToString();
                SKUDescription = dtRow["SKUDescription"].ToString();
                CustomerCode = dtRow["CustomerCode"].ToString();
                CustomerName = dtRow["CustomerName"].ToString();
                UnitMeasure = dtRow["UnitMeasure"].ToString();
                ExpectedOutputQty = Convert.ToDecimal(Convert.IsDBNull(dtRow["ExpectedOutputQty"]) ? 0 : dtRow["ExpectedOutputQty"]);
                Status = dtRow["Status"].ToString();
                EffectivityDate = dtRow["EffectivityDate"].ToString();
                Remarks = dtRow["Remarks"].ToString();
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
        public void InsertData(ProductionRouting _ent)
        {
            SKUCode = _ent.SKUCode;
            Conn = _ent.Connection; 

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add("Production.ProdRouting", "0", "SKUCode", _ent.SKUCode);
            DT1.Rows.Add("Production.ProdRouting", "0", "CustomerCode", _ent.CustomerCode);
            DT1.Rows.Add("Production.ProdRouting", "0", "UnitMeasure", _ent.UnitMeasure);
            DT1.Rows.Add("Production.ProdRouting", "0", "ExpectedOutputQty", _ent.ExpectedOutputQty);
            DT1.Rows.Add("Production.ProdRouting", "0", "Status", _ent.Status);
            DT1.Rows.Add("Production.ProdRouting", "0", "Remarks", _ent.Remarks);
            DT1.Rows.Add("Production.ProdRouting", "0", "IsInactive", 1);
            DT1.Rows.Add("Procurement.ProdRouting", "0", "AddedBy", _ent.AddedBy);
            DT1.Rows.Add("Procurement.ProdRouting", "0", "AddedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            DT1.Rows.Add("Procurement.ProdRouting", "0", "DocDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            Gears.CreateData(DT1, _ent.Connection);
        }
        public void UpdateData(ProductionRouting _ent)
        {
            SKUCode = _ent.SKUCode;
            Conn = _ent.Connection;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add("Production.ProdRouting", "cond", "SKUCode", _ent.SKUCode);
            DT1.Rows.Add("Production.ProdRouting", "cond", "RecordID", _ent.RecordID);
            DT1.Rows.Add("Production.ProdRouting", "set", "CustomerCode", _ent.CustomerCode);
            DT1.Rows.Add("Production.ProdRouting", "set", "UnitMeasure", _ent.UnitMeasure);
            DT1.Rows.Add("Production.ProdRouting", "set", "ExpectedOutputQty", _ent.ExpectedOutputQty);
            DT1.Rows.Add("Production.ProdRouting", "set", "Status", _ent.Status);
            DT1.Rows.Add("Production.ProdRouting", "set", "Remarks", _ent.Remarks);
            DT1.Rows.Add("Production.ProdRouting", "set", "LastEditedBy", _ent.LastEditedBy);
            DT1.Rows.Add("Production.ProdRouting", "set", "LastEditedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            Gears.UpdateData(DT1, _ent.Connection);
            Functions.AuditTrail("PRDRTG", SKUCode, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "UPDATE", _ent.Connection);
        }
        public void DeleteData(ProductionRouting _ent)
        {
            SKUCode = _ent.SKUCode;
            Conn = _ent.Connection;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add("Production.ProdRouting", "cond", "SKUCode", _ent.SKUCode);

            Gears.DeleteData(DT1, _ent.Connection);
            Functions.AuditTrail("PRDRTG", SKUCode, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "DELETE", _ent.Connection);
        }
    }
}
