using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using GearsLibrary;

namespace Entity
{
    public class SystemSettings
    {
        public virtual string Code { get; set; }
        public virtual decimal SequenceNumber { get; set; }
        public virtual string Description { get; set; }
        public virtual string Value { get; set; }

        public static DataTable getdata()
        {

            DataTable a = Gears.RetriveData("select Code,Description,Value,SequenceNumber from IT.SystemSettings"); 
            return a; 
        }

        public void InsertData(string Code, string Description, string Value, Int32 Sequence)
        {
            
            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add("it.systemsettings", "0", "value", Value);
            DT1.Rows.Add("it.systemsettings", "0", "Description", Description);
            DT1.Rows.Add("it.systemsettings", "0", "code", Code);
            DT1.Rows.Add("it.systemsettings", "0", "SequenceNumber", Sequence);

            Gears.CreateData(DT1);

        }


        public void InsertData2(SystemSettings _ent)
        {

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add("it.systemsettings", "0", "value", _ent.Value);
            DT1.Rows.Add("it.systemsettings", "0", "Description", _ent.Description);
            DT1.Rows.Add("it.systemsettings", "0", "code", _ent.Code);
            DT1.Rows.Add("it.systemsettings", "0", "SequenceNumber", _ent.SequenceNumber);

            Gears.CreateData(DT1);

        }

        public void UpdateData(SystemSettings _ent)
        {

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("it.systemsettings", "set", "value", _ent.Value);
            DT1.Rows.Add("it.systemsettings", "set", "SequenceNumber", _ent.SequenceNumber);
            DT1.Rows.Add("it.systemsettings", "set", "Description",_ent.Description);
            DT1.Rows.Add("it.systemsettings", "cond", "code", _ent.Code);


            string strErr = Gears.UpdateData(DT1);


        }

        
    }


}
