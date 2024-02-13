using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using GearsLibrary;

namespace Entity
{
    public class TRRSetup
    {
        private static string Connection;
        public static string TableName { get; set; }
        public static string ColumnName { get; set; }
        public virtual string TransType { get; set; }
        public virtual string TransDoc { get; set; }
        public virtual string TransLine { get; set; }

        public virtual string LineNumber { get; set; }
        public virtual decimal OriginalBaseQty { get; set; }
        public virtual string Width { get; set; }
        public virtual string Shade { get; set; }
        public virtual string Shrinkage { get; set; }
        public virtual string AddedBy { get; set; }
        public virtual DateTime AddedDate { get; set; }
        public virtual string LastEditedBy { get; set; }
        public virtual DateTime LastEditedDate { get; set; }

        public virtual string Item { get; set; }
        public virtual string Color { get; set; }
        public virtual string Class { get; set; }
        public virtual string Size { get; set; }
        public virtual string Unit { get; set; }
        public virtual decimal Qty { get; set; }
        public virtual decimal BulkQty { get; set; }
        
        public DataTable getdetail(string DocNumber, string LineNumber, string TransType,string Conn)
        {
            Connection = Conn;
            DataTable a;
            try
            {
                a = Gears.RetriveData2("SELECT DISTINCT * FROM WMS.CountSheetSetup WHERE TransDoc='" + DocNumber + "' AND TransLine = '" + LineNumber + "' AND TransType = 'PRCRCR' ORDER BY LineNumber ASC",Conn);
                return a;
            }
            catch (Exception e)
            {
                a = null;
                return a;
            }
        }

        public void GetTableName(string tablename, string columnname,string Conn)
        {
            TableName = tablename;
            ColumnName = columnname;
            Connection = Conn;
        }

        public void UpdateTRRSetup(TRRSetup A)
        {
            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("WMS.CountSheetSetup", "cond", "TransDoc", A.TransDoc);
            DT1.Rows.Add("WMS.CountSheetSetup", "cond", "TransLine", A.TransLine);
            DT1.Rows.Add("WMS.CountSheetSetup", "cond", "TransType", A.TransType);
            DT1.Rows.Add("WMS.CountSheetSetup", "cond", "LineNumber", A.LineNumber); //RollID
            DT1.Rows.Add("WMS.CountSheetSetup", "set", "OriginalBaseQty", A.OriginalBaseQty); //RollQty
            DT1.Rows.Add("WMS.CountSheetSetup", "set", "Width", A.Width);
            //DT1.Rows.Add("WMS.CountSheetSetup", "set", "Shade", A.Shade);
            //DT1.Rows.Add("WMS.CountSheetSetup", "set", "Shrinkage", A.Shrinkage);
            //DT1.Rows.Add("WMS.CountSheetSetup", "set", "LastEditedBy", DateTime.Now);

            Gears.UpdateData(DT1,Connection);
        }
    }
}
