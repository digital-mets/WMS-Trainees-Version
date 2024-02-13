using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GearsLibrary;
using System.Data;

namespace Entity
{
    public class WorkCenter
    {
        private static string Conn;
        public virtual string Connection { get; set; }
        public virtual int RecordID { get; set; }
        public virtual string WorkCenterCode { get; set; }
        public virtual string Description { get; set; }
        public virtual string Step { get; set; }
        public virtual int Days { get; set; }
        public virtual int Shift { get; set; }
        public virtual int OperatingShift { get; set; }
        public virtual int SAM { get; set; }
        public virtual decimal Utilization { get; set; }
        public virtual int MachineQty { get; set; }
        public virtual decimal OutputDays { get; set; }
        public virtual int OutputWeekly { get; set; }
        public virtual bool IsInActive { get; set; }
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
        public virtual string ActivatedBy { get; set; }
        public virtual string ActivatedDate { get; set; }
        public virtual string DeActivatedBy { get; set; }
        public virtual string DeActivatedDate { get; set; }

        public DataTable getdata(string RecordID, string Conn)
        {
            DataTable a = null;

            if (RecordID != null)
            {
                a = Gears.RetriveData2("select * from Masterfile.WorkCenter where RecordID = '" + RecordID + "'", Conn);
                foreach (DataRow dtRow in a.Rows)
                {
                    WorkCenterCode = dtRow["WorkCenter"].ToString();
                    Description = dtRow["Description"].ToString();
                    Step = dtRow["Step"].ToString();
                    Days = Convert.ToInt16(dtRow["Days"].ToString());
                    Shift = Convert.ToInt16(dtRow["Shift"].ToString());
                    OperatingShift = Convert.ToInt16(dtRow["OperatingShift"].ToString());
                    SAM = Convert.ToInt16(dtRow["SAM"].ToString());
                    Utilization = Convert.ToDecimal(dtRow["Utilization"].ToString());
                    MachineQty = Convert.ToInt16(dtRow["MachineQty"].ToString());
                    OutputDays = Convert.ToDecimal(dtRow["OutputDays"].ToString());
                    OutputWeekly = Convert.ToInt16(dtRow["OutputWeekly"].ToString());
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
                    ActivatedBy = dtRow["ActivatedBy"].ToString();
                    ActivatedDate = dtRow["ActivatedDate"].ToString();
                    DeActivatedBy = dtRow["DeActivatedBy"].ToString();
                    DeActivatedDate = dtRow["DeActivatedDate"].ToString();
                }
            }

            else
            {
                a = Gears.RetriveData2("select * from Masterfile.WorkCenter where WorkCenter is null", Conn);
            }
            return a;
        }
        public void InsertData(WorkCenter _ent)
        {
            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add("Masterfile.WorkCenter", "0", "WorkCenter", _ent.WorkCenterCode);
            DT1.Rows.Add("Masterfile.WorkCenter", "0", "Description", _ent.Description);
            DT1.Rows.Add("Masterfile.WorkCenter", "0", "Step", _ent.Step);
            DT1.Rows.Add("Masterfile.WorkCenter", "0", "Days", _ent.Days);
            DT1.Rows.Add("Masterfile.WorkCenter", "0", "Shift", _ent.Shift);
            DT1.Rows.Add("Masterfile.WorkCenter", "0", "OperatingShift", _ent.OperatingShift);
            DT1.Rows.Add("Masterfile.WorkCenter", "0", "SAM", _ent.SAM);
            DT1.Rows.Add("Masterfile.WorkCenter", "0", "Utilization", _ent.Utilization);
            DT1.Rows.Add("Masterfile.WorkCenter", "0", "MachineQty", _ent.MachineQty);
            DT1.Rows.Add("Masterfile.WorkCenter", "0", "OutputDays", _ent.OutputDays);
            DT1.Rows.Add("Masterfile.WorkCenter", "0", "OutputWeekly", _ent.OutputWeekly);
            DT1.Rows.Add("Masterfile.WorkCenter", "0", "Field1", _ent.Field1);
            DT1.Rows.Add("Masterfile.WorkCenter", "0", "Field2", _ent.Field2);
            DT1.Rows.Add("Masterfile.WorkCenter", "0", "Field3", _ent.Field3);
            DT1.Rows.Add("Masterfile.WorkCenter", "0", "Field4", _ent.Field4);
            DT1.Rows.Add("Masterfile.WorkCenter", "0", "Field5", _ent.Field5);
            DT1.Rows.Add("Masterfile.WorkCenter", "0", "Field6", _ent.Field6);
            DT1.Rows.Add("Masterfile.WorkCenter", "0", "Field7", _ent.Field7);
            DT1.Rows.Add("Masterfile.WorkCenter", "0", "Field8", _ent.Field8);
            DT1.Rows.Add("Masterfile.WorkCenter", "0", "Field9", _ent.Field9);
            Gears.CreateData(DT1, _ent.Connection);
            //Functions.AuditTrail("REFSIZE", _ent.SizeCode, _ent.AddedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "INSERT");
        }
        public void UpdateData(WorkCenter _ent)
        {

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add("Masterfile.WorkCenter", "cond", "RecordID", _ent.RecordID);
            DT1.Rows.Add("Masterfile.WorkCenter", "set", "WorkCenter", _ent.WorkCenterCode);
            DT1.Rows.Add("Masterfile.WorkCenter", "set", "Description", _ent.Description);
            DT1.Rows.Add("Masterfile.WorkCenter", "set", "Step", _ent.Step);
            DT1.Rows.Add("Masterfile.WorkCenter", "set", "Days", _ent.Days);
            DT1.Rows.Add("Masterfile.WorkCenter", "set", "Shift", _ent.Shift);
            DT1.Rows.Add("Masterfile.WorkCenter", "set", "OperatingShift", _ent.OperatingShift);
            DT1.Rows.Add("Masterfile.WorkCenter", "set", "SAM", _ent.SAM);
            DT1.Rows.Add("Masterfile.WorkCenter", "set", "Utilization", _ent.Utilization);
            DT1.Rows.Add("Masterfile.WorkCenter", "set", "MachineQty", _ent.MachineQty);
            DT1.Rows.Add("Masterfile.WorkCenter", "set", "OutputDays", _ent.OutputDays);
            DT1.Rows.Add("Masterfile.WorkCenter", "set", "OutputWeekly", _ent.OutputWeekly);
            DT1.Rows.Add("Masterfile.WorkCenter", "set", "Field1", _ent.Field1);
            DT1.Rows.Add("Masterfile.WorkCenter", "set", "Field2", _ent.Field2);
            DT1.Rows.Add("Masterfile.WorkCenter", "set", "Field3", _ent.Field3);
            DT1.Rows.Add("Masterfile.WorkCenter", "set", "Field4", _ent.Field4);
            DT1.Rows.Add("Masterfile.WorkCenter", "set", "Field5", _ent.Field5);
            DT1.Rows.Add("Masterfile.WorkCenter", "set", "Field6", _ent.Field6);
            DT1.Rows.Add("Masterfile.WorkCenter", "set", "Field7", _ent.Field7);
            DT1.Rows.Add("Masterfile.WorkCenter", "set", "Field8", _ent.Field8);
            DT1.Rows.Add("Masterfile.WorkCenter", "set", "Field9", _ent.Field9);
            DT1.Rows.Add("Masterfile.WorkCenter", "set", "LastEditedBy", _ent.LastEditedBy);
            DT1.Rows.Add("Masterfile.WorkCenter", "set", "LastEditedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            Gears.UpdateData(DT1, _ent.Connection);
            //Functions.AuditTrail("REFSIZE", SizeCode, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "UPDATE");
        }
        public void DeleteData(WorkCenter _ent)
        {
            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Masterfile.WorkCenter", "cond", "RecordID", _ent.RecordID);
            Gears.DeleteData(DT1, _ent.Connection);
            //Functions.AuditTrail("REFSIZE", SizeCode, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "DELETE");
        }
    }
}
