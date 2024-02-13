using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GearsLibrary;
using System.Data;

namespace Entity
{
    public class RemovePartsToProperty
    {
        private static string property;

        private static string Conn;//ADD CONN
        public virtual string Connection { get; set; }//ADD CONN
        public virtual string PropertyNumber { get; set; }
        public virtual string ItemCode { get; set; }
        public virtual string FullDesc { get; set; }
        public virtual string ColorCode { get; set; }
        public virtual string ClassCode { get; set; }
        public virtual string SizeCode { get; set; }

        public virtual bool IsWithDetail { get; set; }
        public virtual bool IsValidated { get; set; }



        public virtual IList<RemovePartsToPropertyDetail> Detail { get; set; }
        public class RemovePartsToPropertyDetail
        {
            public virtual RemovePartsToProperty Parent { get; set; }
            public virtual string LineNumber { get; set; }
            public virtual string PropertyNumber { get; set; }
            public virtual string ItemCode { get; set; }
            public virtual string Description { get; set; }
            public virtual string ColorCode { get; set; }
            public virtual string ClassCode { get; set; }
            public virtual string SizeCode { get; set; }

            public DataTable getdetail(string PropertyNumber, string Conn)
            {

                DataTable a;
                try
                {
                    a = Gears.RetriveData2("select * from Accounting.AssetInv where ParentProperty='" + PropertyNumber + "'", Conn);
                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }

            public void AddRemovePartsToPropertyDetail(RemovePartsToPropertyDetail RemovePartsToPropertyDetail)
            {
                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

                DT1.Rows.Add("Accounting.AssetInv", "cond", "PropertyNumber", RemovePartsToPropertyDetail.PropertyNumber);

                DT1.Rows.Add("Accounting.AssetInv", "set", "ParentProperty", property);

                Gears.UpdateData(DT1, Conn);

            }
            public void UpdateRemovePartsToPropertyDetail(RemovePartsToPropertyDetail RemovePartsToPropertyDetail)
            {

                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

                DT1.Rows.Add("Accounting.AssetInv", "cond", "PropertyNumber", RemovePartsToPropertyDetail.PropertyNumber);

                DT1.Rows.Add("Accounting.AssetInv", "set", "ParentProperty", property);
                
                Gears.UpdateData(DT1, Conn);                               
            }
            public void DeleteRemovePartsToPropertyDetail(RemovePartsToPropertyDetail RemovePartsToPropertyDetail)
            {
                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                //DT1.Rows.Add("Accounting.AddPartsToPropertyDetail", "cond", "DocNumber", AddPartsToPropertyDetail.DocNumber);
                DT1.Rows.Add("Accounting.AssetInv", "cond", "PropertyNumber", RemovePartsToPropertyDetail.PropertyNumber);


                DT1.Rows.Add("Accounting.AssetInv", "set", "ParentProperty","");
                Gears.UpdateData(DT1, Conn);   

            }
        }

        public DataTable getdata(string PropertyNumber, string Conn)
        {
            DataTable a;

            //if (DocNumber != null)
            //{
            a = Gears.RetriveData2("select * from Accounting.AssetInv where PropertyNumber = '" + PropertyNumber + "'", Conn);
            foreach (DataRow dtRow in a.Rows)
            {  
                PropertyNumber = dtRow["PropertyNumber"].ToString();
                ItemCode = dtRow["ItemCode"].ToString();
                FullDesc = dtRow["Description"].ToString();
                ColorCode = dtRow["ColorCOde"].ToString();
                ClassCode = dtRow["ClassCode"].ToString();
                SizeCode = dtRow["SizeCode"].ToString();
                IsValidated = Convert.ToBoolean(Convert.IsDBNull(dtRow["IsValidated"]) ? false : dtRow["IsValidated"]);
                IsWithDetail = Convert.ToBoolean(Convert.IsDBNull(dtRow["IsWithDetail"]) ? false : dtRow["IsWithDetail"]);



            }

            return a;
        }
        public void InsertData(RemovePartsToProperty _ent)
        {
            property = _ent.PropertyNumber;
            Conn = _ent.Connection;
        }

        public void UpdateData(RemovePartsToProperty _ent)
        {
            property = _ent.PropertyNumber;
            Conn = _ent.Connection;
        }
        public void DeleteData(RemovePartsToProperty _ent)
        {
            property = _ent.PropertyNumber;
            Conn = _ent.Connection;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Accounting.AssetInv", "cond", "PropertyNumber", _ent.PropertyNumber);
            Gears.DeleteData(DT1, _ent.Connection);
           //Functions.AuditTrail("ACTAPD", Docnum, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "DELETE");
        }
    }
}
