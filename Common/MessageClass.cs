using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using GearsLibrary;

namespace Common
{
    public class MessageClass
    {
        private static string Conns;
        public virtual string Connection { get; set; }
        public virtual int RecordID { get; set; }
        public virtual string Date { get; set; }
        public virtual string Header { get; set; }
        public virtual string HeaderFont { get; set; }
        public virtual string Message { get; set; }
        public virtual string Status { get; set; }

        public DataTable getdetail(string ItemCode, string Conn)
        {
            Conns = Conn;

            DataTable a;
            try
            {

                a = Gears.RetriveData2("select RecordID,CONVERT(varchar(50),Date) Date,Header,HeaderFont,Message,Status from IT.Message", Conn);
                return a;
            }
            catch (Exception e)
            {
                a = null;
                return a;
            }
        }
        public void InsertData(MessageClass _ent)
        {   
            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add("IT.Message", "0", "Date", _ent.Date);
            DT1.Rows.Add("IT.Message", "0", "Header", _ent.Header);
            DT1.Rows.Add("IT.Message", "0", "HeaderFont", _ent.HeaderFont);
            DT1.Rows.Add("IT.Message", "0", "Message", _ent.Message);
            DT1.Rows.Add("IT.Message", "0", "Status", _ent.Status);

            Gears.CreateData(DT1, Conns);
        }

        public void UpdateData(MessageClass _ent)
        {
            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();


            DT1.Rows.Add("IT.Message", "cond", "RecordID", _ent.RecordID); 
            DT1.Rows.Add("IT.Message", "set", "Date", _ent.Date);
            DT1.Rows.Add("IT.Message", "set", "Header", _ent.Header);
            DT1.Rows.Add("IT.Message", "set", "HeaderFont", _ent.HeaderFont);
            DT1.Rows.Add("IT.Message", "set", "Message", _ent.Message);
            DT1.Rows.Add("IT.Message", "set", "Status", _ent.Status);

            Gears.UpdateData(DT1, Conns);
        }
    }
}
