using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GearsLibrary;
using System.Data;

namespace Entity
{
    public class Step
    {
        private static string Conn; 
        public virtual string Connection { get; set; } 

        public virtual string StepCode { get; set; }
        public virtual string Description { get; set; }
	public virtual string ParentStepCode { get; set; }
	public virtual string Mnemonics { get; set; }
	public virtual decimal EstimatedWOPrice { get; set; }
	public virtual decimal MinimumWOPrice { get; set; }
	public virtual decimal MaximumWOPrice { get; set; }
	public virtual string OverheadCode { get; set; }
	public virtual bool IsPreProductionStep { get; set; }
	public virtual bool IsInhouse { get; set; }
        public virtual bool IsInactive { get; set; }
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

        public DataTable getdata(string Code, string Conn) //Ter
        {
            DataTable a;

                a = Gears.RetriveData2("select * from Masterfile.Step where StepCode= '" + Code+ "'", Conn); //Ter
                foreach (DataRow dtRow in a.Rows)
                {
                    StepCode = dtRow["StepCode"].ToString();
                    Description = dtRow["Description"].ToString();
		    ParentStepCode = dtRow["ParentStepCode"].ToString();
		    Mnemonics = dtRow["Mnemonics"].ToString();
		    EstimatedWOPrice =  Convert.ToDecimal(Convert.IsDBNull(dtRow["EstimatedWOPrice"]) ? false : dtRow["EstimatedWOPrice"]);
		    MinimumWOPrice =  Convert.ToDecimal(Convert.IsDBNull(dtRow["MinimumWOPrice"]) ? false : dtRow["MinimumWOPrice"]);
		    MaximumWOPrice =  Convert.ToDecimal(Convert.IsDBNull(dtRow["MaximumWOPrice"]) ? false : dtRow["MaximumWOPrice"]);
            OverheadCode = dtRow["Overhead"].ToString();	
		    IsPreProductionStep= Convert.ToBoolean(Convert.IsDBNull(dtRow["IsPreProductionStep"]) ? false : dtRow["IsPreProductionStep"]);
		    IsInhouse= Convert.ToBoolean(Convert.IsDBNull(dtRow["IsInhouse"]) ? false : dtRow["IsInhouse"]);	    
		    IsInactive = Convert.ToBoolean(Convert.IsDBNull(dtRow["IsInactive"]) ? false : dtRow["IsInactive"]);
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

        public void InsertData(Step _ent)
        {
            Conn = _ent.Connection; //Ter

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Masterfile.Step", "0", "StepCode", _ent.StepCode);
            DT1.Rows.Add("Masterfile.Step", "0", "Description", _ent.Description);
	    DT1.Rows.Add("Masterfile.Step", "0", "ParentStepCode", _ent.ParentStepCode);
	    DT1.Rows.Add("Masterfile.Step", "0", "Mnemonics", _ent.Mnemonics);
	    DT1.Rows.Add("Masterfile.Step", "0", "EstimatedWOPrice", _ent.EstimatedWOPrice);
	    DT1.Rows.Add("Masterfile.Step", "0", "MinimumWOPrice", _ent.MinimumWOPrice);
	    DT1.Rows.Add("Masterfile.Step", "0", "MaximumWOPrice", _ent.MaximumWOPrice);
            DT1.Rows.Add("Masterfile.Step", "0", "Overhead", _ent.OverheadCode);
	    DT1.Rows.Add("Masterfile.Step", "0", "IsPreProductionStep", _ent.IsPreProductionStep);
	    DT1.Rows.Add("Masterfile.Step", "0", "IsInhouse", _ent.IsInhouse);
            DT1.Rows.Add("Masterfile.Step", "0", "IsInactive", _ent.IsInactive);
            DT1.Rows.Add("Masterfile.Step", "0", "AddedBy", _ent.AddedBy);
            DT1.Rows.Add("Masterfile.Step", "0", "AddedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            DT1.Rows.Add("Masterfile.Step", "0", "Field1", _ent.Field1);
            DT1.Rows.Add("Masterfile.Step", "0", "Field2", _ent.Field2);
            DT1.Rows.Add("Masterfile.Step", "0", "Field3", _ent.Field3);
            DT1.Rows.Add("Masterfile.Step", "0", "Field4", _ent.Field4);
            DT1.Rows.Add("Masterfile.Step", "0", "Field5", _ent.Field5);
            DT1.Rows.Add("Masterfile.Step", "0", "Field6", _ent.Field6);
            DT1.Rows.Add("Masterfile.Step", "0", "Field7", _ent.Field7);
            DT1.Rows.Add("Masterfile.Step", "0", "Field8", _ent.Field8);
            DT1.Rows.Add("Masterfile.Step", "0", "Field9", _ent.Field9);

            Gears.CreateData(DT1, _ent.Connection); // TER
        }

        public void UpdateData(Step _ent)
        {
            Conn = _ent.Connection; //Ter

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Masterfile.Step", "cond", "StepCode", _ent.StepCode);
            DT1.Rows.Add("Masterfile.Step", "set", "Description", _ent.Description);
	    DT1.Rows.Add("Masterfile.Step", "set", "ParentStepCode", _ent.ParentStepCode);
	    DT1.Rows.Add("Masterfile.Step", "set", "Mnemonics", _ent.Mnemonics);
	    DT1.Rows.Add("Masterfile.Step", "set", "EstimatedWOPrice", _ent.EstimatedWOPrice);
	    DT1.Rows.Add("Masterfile.Step", "set", "MinimumWOPrice", _ent.MinimumWOPrice);
	    DT1.Rows.Add("Masterfile.Step", "set", "MaximumWOPrice", _ent.MaximumWOPrice);
            DT1.Rows.Add("Masterfile.Step", "set", "Overhead", _ent.OverheadCode);
	    DT1.Rows.Add("Masterfile.Step", "set", "IsPreProductionStep", _ent.IsPreProductionStep);
	    DT1.Rows.Add("Masterfile.Step", "set", "IsInhouse", _ent.IsInhouse);
            DT1.Rows.Add("Masterfile.Step", "set", "IsInactive", _ent.IsInactive);
            DT1.Rows.Add("Masterfile.Step", "set", "LastEditedBy", _ent.AddedBy);
            DT1.Rows.Add("Masterfile.Step", "set", "LastEditedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            DT1.Rows.Add("Masterfile.Step", "set", "Field1", _ent.Field1);
            DT1.Rows.Add("Masterfile.Step", "set", "Field2", _ent.Field2);
            DT1.Rows.Add("Masterfile.Step", "set", "Field3", _ent.Field3);
            DT1.Rows.Add("Masterfile.Step", "set", "Field4", _ent.Field4);
            DT1.Rows.Add("Masterfile.Step", "set", "Field5", _ent.Field5);
            DT1.Rows.Add("Masterfile.Step", "set", "Field6", _ent.Field6);
            DT1.Rows.Add("Masterfile.Step", "set", "Field7", _ent.Field7);
            DT1.Rows.Add("Masterfile.Step", "set", "Field8", _ent.Field8);
            DT1.Rows.Add("Masterfile.Step", "set", "Field9", _ent.Field9);

            string strErr = Gears.UpdateData(DT1, _ent.Connection); // Ter
            Functions.AuditTrail("REFSTEP", _ent.StepCode, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "UPDATE", _ent.Connection); // Ter

            strErr = Gears.UpdateData(DT1, _ent.Connection); // Ter
        }

        public void DeleteData(Step _ent)
        {
            Conn = _ent.Connection; //Ter
            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Masterfile.Step", "cond", "StepCode", _ent.StepCode);
            Gears.DeleteData(DT1, _ent.Connection); // Ter
            Functions.AuditTrail("REFSTEP", _ent.StepCode, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "DELETE", _ent.Connection); // Ter
        }
    }
}
