using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GearsLibrary;
using System.Data;

namespace Entity
{
    public class AuditTrail
    {
        public virtual string DocNumber { get; set; }
        public virtual string Column { get; set; }
        public virtual string OldValue { get; set; }
        public virtual string Name { get; set; }
        public virtual string EditedDate { get; set; }
        public DataTable getdata(string docnum)
        {
            DataTable a;

            if (docnum != null)
            {
                a = Gears.RetriveData2("select * from IT.AuditTrail where DocNumber = '" + docnum + "'");
                foreach (DataRow dtRow in a.Rows)
                {
                    DocNumber = dtRow["DocNumber"].ToString();
                    Column = dtRow["Column"].ToString();
                    OldValue = dtRow["OldValue"].ToString();
                    Name = dtRow["Name"].ToString();
                    EditedDate = dtRow["EditedDate"].ToString();
                }
            }
            else
            {
                a = Gears.RetriveData2("select '' as DocNumber,'' as Column,'' as OldValue,'' as Name,'' as EditedDate");
            }
            return a;
        }
        public void InsertData(AuditTrail _ent)
        {
            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add("IT.AuditTrail", "0", "DocNumber", _ent.DocNumber);
            DT1.Rows.Add("IT.AuditTrail", "0", "Column", _ent.Column);
            DT1.Rows.Add("IT.AuditTrail", "0", "OldValue", _ent.OldValue);
            DT1.Rows.Add("IT.AuditTrail", "0", "Name", _ent.Name);
            DT1.Rows.Add("IT.AuditTrail", "0", "EditedDate", _ent.EditedDate);
            Gears.CreateData(DT1);
        }
        public void UpdateData(AuditTrail _ent)
        {
            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("IT.AuditTrail", "cond", "DocNumber", _ent.DocNumber);
            DT1.Rows.Add("IT.AuditTrail", "set", "Column", _ent.Column);
            DT1.Rows.Add("IT.AuditTrail", "set", "OldValue", _ent.OldValue);
            DT1.Rows.Add("IT.AuditTrail", "set", "Name", _ent.Name);
            DT1.Rows.Add("IT.AuditTrail", "set", "EditedDate", _ent.EditedDate);
            string strErr = Gears.UpdateData(DT1);
        }
        public void DeleteData(AuditTrail _ent)
        {
            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("IT.AuditTrail", "cond", "DocNumber", _ent.DocNumber);
            Gears.DeleteData(DT1);
        }
    }
}
