using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GearsLibrary;
using System.Data;

namespace Entity
{
    public class Machine
    {
        private static string Conn; 
        public virtual string Connection { get; set; }

        public virtual string MachineCode { get; set; }
        public virtual string MachineName { get; set; }
        public virtual string Specifications { get; set; }
        public virtual string Brand { get; set; }
        public virtual string Model { get; set; }
        public virtual string SerialNo { get; set; }
        public virtual string AssetTag { get; set; }
        public virtual string CapacityKW { get; set; }
        public virtual string SupplyVoltage { get; set; }
        public virtual string Location { get; set; }
        public virtual string ProcessStep { get; set; }
        public virtual string SchedPM { get; set; }
        public virtual string Status { get; set; }
        public virtual string MachineCapacityPerUnit { get; set; }
        public virtual string MachineCapacityPerBatch { get; set; }
        public virtual string IsInactive { get; set; }
        public virtual string AddedBy { get; set; }
        public virtual string AddedDate { get; set; }
        public virtual string LastEditedBy { get; set; }
        public virtual string LastEditedDate { get; set; }
        public virtual string ActivatedBy { get; set; }
        public virtual string ActivatedDate { get; set; }
        public virtual string DeActivatedBy { get; set; }
        public virtual string DeActivatedDate { get; set; }
        public virtual string Field1 { get; set; }
        public virtual string Field2 { get; set; }
        public virtual string Field3 { get; set; }
        public virtual string Field4 { get; set; }
        public virtual string Field5 { get; set; }
        public virtual string Field6 { get; set; }
        public virtual string Field7 { get; set; }
        public virtual string Field8 { get; set; }
        public virtual string Field9 { get; set; }
        public virtual bool IsDaily { get; set; }
        public virtual string Daily { get; set; }
        public virtual bool IsWeekly { get; set; }
        public virtual string Weekly { get; set; }
        public virtual bool IsBiMonthly { get; set; }
        public virtual string BiMonthly { get; set; }
        public virtual bool IsMonthly { get; set; }
        public virtual string Monthly { get; set; }
        public virtual bool IsSemiAnnual { get; set; }
        public virtual string SemiAnnual { get; set; }
        public virtual bool IsAnnually { get; set; }
        public virtual string Annually { get; set; }

        public DataTable getdata(string Code, string Conn) //Ter
        {
            DataTable a;

            a = Gears.RetriveData2("SELECT * FROM Masterfile.Machine WHERE MachineCode= '" + Code + "'", Conn); //Ter
            foreach (DataRow dtRow in a.Rows)
            {
                MachineCode = dtRow["MachineCode"].ToString();
                MachineName = dtRow["Description"].ToString();
                Specifications = dtRow["Specifications"].ToString();
                Brand = dtRow["Brand"].ToString();
                Model = dtRow["Model"].ToString();
                SerialNo = dtRow["SerialNo"].ToString();
                AssetTag = dtRow["AssetTag"].ToString();
                CapacityKW = dtRow["CapacityKW"].ToString();
                SupplyVoltage = dtRow["SupplyVoltage"].ToString();
                Location = dtRow["Location"].ToString();
                ProcessStep = dtRow["ProcessStep"].ToString();
                Status = dtRow["Status"].ToString();
                MachineCapacityPerBatch = dtRow["MachineCapacityPerBatch"].ToString();
                MachineCapacityPerUnit = dtRow["MachineCapacityPerUnit"].ToString();
                IsDaily = Convert.ToBoolean(Convert.IsDBNull(dtRow["IsDaily"]) ? false : dtRow["IsDaily"]);
                Daily = dtRow["Daily"].ToString();
                IsWeekly = Convert.ToBoolean(Convert.IsDBNull(dtRow["IsWeekly"]) ? false : dtRow["IsWeekly"]);
                Weekly = dtRow["Weekly"].ToString();
                IsBiMonthly = Convert.ToBoolean(Convert.IsDBNull(dtRow["IsBiMonthly"]) ? false : dtRow["IsBiMonthly"]);
                BiMonthly = dtRow["BiMonthly"].ToString();
                IsMonthly = Convert.ToBoolean(Convert.IsDBNull(dtRow["IsMonthly"]) ? false : dtRow["IsMonthly"]);
                Monthly = dtRow["Monthly"].ToString();
                IsSemiAnnual = Convert.ToBoolean(Convert.IsDBNull(dtRow["IsSemiAnnual"]) ? false : dtRow["IsSemiAnnual"]);
                SemiAnnual = dtRow["SemiAnnual"].ToString();
                IsAnnually = Convert.ToBoolean(Convert.IsDBNull(dtRow["IsAnnually"]) ? false : dtRow["IsAnnually"]);
                Annually = dtRow["Annually"].ToString();
                AddedBy = dtRow["AddedBy"].ToString();
                AddedDate = dtRow["AddedDate"].ToString();
                LastEditedBy = dtRow["LastEditedBy"].ToString();
                LastEditedDate = dtRow["LastEditedDate"].ToString();
                ActivatedBy = dtRow["ActivatedBy"].ToString();
                ActivatedDate = dtRow["ActivatedDate"].ToString();
                DeActivatedBy = dtRow["DeActivatedBy"].ToString();
                DeActivatedDate = dtRow["DeActivatedDate"].ToString();
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

        public string InsertData(Machine _ent)
        {
            Conn = _ent.Connection; //Ter

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Masterfile.Machine", "0", "MachineCode", _ent.MachineCode);
            DT1.Rows.Add("Masterfile.Machine", "0", "MachineName", _ent.MachineName);
            DT1.Rows.Add("Masterfile.Machine", "0", "Specifications", _ent.Specifications);
            DT1.Rows.Add("Masterfile.Machine", "0", "Brand", _ent.Brand);
            DT1.Rows.Add("Masterfile.Machine", "0", "Model", _ent.Model);
            DT1.Rows.Add("Masterfile.Machine", "0", "SerialNo", _ent.SerialNo);
            DT1.Rows.Add("Masterfile.Machine", "0", "AssetTag", _ent.AssetTag);
            DT1.Rows.Add("Masterfile.Machine", "0", "CapacityKW", _ent.CapacityKW);
            DT1.Rows.Add("Masterfile.Machine", "0", "SupplyVoltage", _ent.SupplyVoltage);
            DT1.Rows.Add("Masterfile.Machine", "0", "Location", _ent.Location);
            DT1.Rows.Add("Masterfile.Machine", "0", "ProcessStep", _ent.ProcessStep);
            DT1.Rows.Add("Masterfile.Machine", "0", "Status", _ent.Status);
            DT1.Rows.Add("Masterfile.Machine", "0", "MachineCapacityPerBatch", _ent.MachineCapacityPerBatch);
            DT1.Rows.Add("Masterfile.Machine", "0", "MachineCapacityPerUnit", _ent.MachineCapacityPerUnit);
            DT1.Rows.Add("Masterfile.Machine", "0", "IsDaily", _ent.IsDaily);
            DT1.Rows.Add("Masterfile.Machine", "0", "Daily", _ent.Daily);
            DT1.Rows.Add("Masterfile.Machine", "0", "IsWeekly", _ent.IsWeekly);
            DT1.Rows.Add("Masterfile.Machine", "0", "Weekly", _ent.Weekly);
            DT1.Rows.Add("Masterfile.Machine", "0", "IsBiMonthly", _ent.IsBiMonthly);
            DT1.Rows.Add("Masterfile.Machine", "0", "BiMonthly", _ent.BiMonthly);
            DT1.Rows.Add("Masterfile.Machine", "0", "IsMonthly", _ent.IsMonthly);
            DT1.Rows.Add("Masterfile.Machine", "0", "Monthly", _ent.Monthly);
            DT1.Rows.Add("Masterfile.Machine", "0", "IsSemiAnnual", _ent.IsSemiAnnual);
            DT1.Rows.Add("Masterfile.Machine", "0", "SemiAnnual", _ent.SemiAnnual);
            DT1.Rows.Add("Masterfile.Machine", "0", "IsAnnually", _ent.IsAnnually);
            DT1.Rows.Add("Masterfile.Machine", "0", "Annually", _ent.Annually);

            DT1.Rows.Add("Masterfile.Machine", "0", "AddedBy", _ent.AddedBy);
            DT1.Rows.Add("Masterfile.Machine", "0", "AddedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            DT1.Rows.Add("Masterfile.Machine", "0", "Field1", _ent.Field1);
            DT1.Rows.Add("Masterfile.Machine", "0", "Field2", _ent.Field2);
            DT1.Rows.Add("Masterfile.Machine", "0", "Field3", _ent.Field3);
            DT1.Rows.Add("Masterfile.Machine", "0", "Field4", _ent.Field4);
            DT1.Rows.Add("Masterfile.Machine", "0", "Field5", _ent.Field5);
            DT1.Rows.Add("Masterfile.Machine", "0", "Field6", _ent.Field6);
            DT1.Rows.Add("Masterfile.Machine", "0", "Field7", _ent.Field7);
            DT1.Rows.Add("Masterfile.Machine", "0", "Field8", _ent.Field8);
            DT1.Rows.Add("Masterfile.Machine", "0", "Field9", _ent.Field9);

            return Gears.CreateData(DT1, _ent.Connection); // TER
        }

        public string UpdateData(Machine _ent)
        {
            Conn = _ent.Connection; //Ter

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Masterfile.Machine", "cond", "MachineCode", _ent.MachineCode);
            DT1.Rows.Add("Masterfile.Machine", "set", "MachineName", _ent.MachineName);
            DT1.Rows.Add("Masterfile.Machine", "set", "Specifications", _ent.Specifications);
            DT1.Rows.Add("Masterfile.Machine", "set", "Brand", _ent.Brand);
            DT1.Rows.Add("Masterfile.Machine", "set", "Model", _ent.Model);
            DT1.Rows.Add("Masterfile.Machine", "set", "SerialNo", _ent.SerialNo);
            DT1.Rows.Add("Masterfile.Machine", "set", "AssetTag", _ent.AssetTag);
            DT1.Rows.Add("Masterfile.Machine", "set", "CapacityKW", _ent.CapacityKW);
            DT1.Rows.Add("Masterfile.Machine", "set", "SupplyVoltage", _ent.SupplyVoltage);
            DT1.Rows.Add("Masterfile.Machine", "set", "Location", _ent.Location);
            DT1.Rows.Add("Masterfile.Machine", "set", "ProcessStep", _ent.ProcessStep);
            DT1.Rows.Add("Masterfile.Machine", "set", "Status", _ent.Status);
            DT1.Rows.Add("Masterfile.Machine", "set", "MachineCapacityPerBatch", _ent.MachineCapacityPerBatch);
            DT1.Rows.Add("Masterfile.Machine", "set", "MachineCapacityPerUnit", _ent.MachineCapacityPerUnit);
            DT1.Rows.Add("Masterfile.Machine", "set", "IsDaily", _ent.IsDaily);
            DT1.Rows.Add("Masterfile.Machine", "set", "Daily", _ent.Daily);
            DT1.Rows.Add("Masterfile.Machine", "set", "IsWeekly", _ent.IsWeekly);
            DT1.Rows.Add("Masterfile.Machine", "set", "Weekly", _ent.Weekly);
            DT1.Rows.Add("Masterfile.Machine", "set", "IsBiMonthly", _ent.IsBiMonthly);
            DT1.Rows.Add("Masterfile.Machine", "set", "BiMonthly", _ent.BiMonthly);
            DT1.Rows.Add("Masterfile.Machine", "set", "IsMonthly", _ent.IsMonthly);
            DT1.Rows.Add("Masterfile.Machine", "set", "Monthly", _ent.Monthly);
            DT1.Rows.Add("Masterfile.Machine", "set", "IsSemiAnnual", _ent.IsSemiAnnual);
            DT1.Rows.Add("Masterfile.Machine", "set", "SemiAnnual", _ent.SemiAnnual);
            DT1.Rows.Add("Masterfile.Machine", "set", "IsAnnually", _ent.IsAnnually);
            DT1.Rows.Add("Masterfile.Machine", "set", "Annually", _ent.Annually);

            DT1.Rows.Add("Masterfile.Machine", "set", "LastEditedBy", _ent.AddedBy);
            DT1.Rows.Add("Masterfile.Machine", "set", "LastEditedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            DT1.Rows.Add("Masterfile.Machine", "set", "Field1", _ent.Field1);
            DT1.Rows.Add("Masterfile.Machine", "set", "Field2", _ent.Field2);
            DT1.Rows.Add("Masterfile.Machine", "set", "Field3", _ent.Field3);
            DT1.Rows.Add("Masterfile.Machine", "set", "Field4", _ent.Field4);
            DT1.Rows.Add("Masterfile.Machine", "set", "Field5", _ent.Field5);
            DT1.Rows.Add("Masterfile.Machine", "set", "Field6", _ent.Field6);
            DT1.Rows.Add("Masterfile.Machine", "set", "Field7", _ent.Field7);
            DT1.Rows.Add("Masterfile.Machine", "set", "Field8", _ent.Field8);
            DT1.Rows.Add("Masterfile.Machine", "set", "Field9", _ent.Field9);

            string strErr = Gears.UpdateData(DT1, _ent.Connection); // Ter
            Functions.AuditTrail("REFMACH", _ent.MachineCode, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "UPDATE", _ent.Connection); // Ter

            return Gears.UpdateData(DT1, _ent.Connection); // Ter
        }

        public void DeleteData(Machine _ent)
        {
            Conn = _ent.Connection; //Ter
            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Masterfile.Machine", "cond", "MachineCode", _ent.MachineCode);
            Gears.DeleteData(DT1, _ent.Connection); // Ter
            Functions.AuditTrail("REFSTEP", _ent.MachineCode, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "DELETE", _ent.Connection); // Ter
        }
    }
}
