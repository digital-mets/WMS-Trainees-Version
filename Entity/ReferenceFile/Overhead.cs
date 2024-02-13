using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GearsLibrary;
using System.Data;

namespace Entity
{
    public class Overhead
    {
        private static string Code;

        private static string Conn;//ADD CONN
        public virtual string Connection { get; set; }//ADD CONN

        public virtual string OHCode { get; set; }
        public virtual string Description { get; set; }
        public virtual decimal OverheadCost { get; set; }
	public virtual string OverheadType { get; set; }
	public virtual bool IsInActive { get; set; }
        public virtual bool IsWithDetail { get; set; }
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


        public virtual IList<OverheadDetail> Detail { get; set; }
        public class OverheadDetail
        {
            public virtual Overhead Parent { get; set; } 
            public virtual string LineNumber { get; set; }
            public virtual string OHCode { get; set; }
            public virtual string RateCode { get; set; }
            public virtual string Description { get; set; }
            public virtual decimal OHRate { get; set; }
            

            public virtual string Field1 { get; set; }
            public virtual string Field2 { get; set; }
            public virtual string Field3 { get; set; }
            public virtual string Field4 { get; set; }
            public virtual string Field5 { get; set; }
            public virtual string Field6 { get; set; }
            public virtual string Field7 { get; set; }
            public virtual string Field8 { get; set; }
            public virtual string Field9 { get; set; }
            public DataTable getdetail(string Trans, string Conn)
            {

                DataTable a;
                try
                {
                    a = Gears.RetriveData2("select * from MasterFile.OverheadDetail where OHCode='" + Trans + "'", Conn);
                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }

            public void AddOverheadDetail(OverheadDetail OverheadDetail)
            {
                int linenum = 0;

                DataTable count = Gears.RetriveData2("select max(convert(int,LineNumber)) as LineNumber from Masterfile.OverheadDetail where OHCode= '" + Code+ "'", Conn);

                try
                {
                    linenum = Convert.ToInt32(count.Rows[0][0].ToString()) + 1;
                }
                catch
                {
                    linenum = 1;
                }
                string strLine = linenum.ToString().PadLeft(5, '0');

                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
                DT1.Rows.Add("MasterFile.OverheadDetail", "0", "OHCode", Code);
		        DT1.Rows.Add("MasterFile.OverheadDetail", "0", "LineNumber", strLine);
                DT1.Rows.Add("MasterFile.OverheadDetail", "0", "RateCode", OverheadDetail.RateCode);
                DT1.Rows.Add("MasterFile.OverheadDetail", "0", "Description", OverheadDetail.Description);
                DT1.Rows.Add("MasterFile.OverheadDetail", "0", "OHRate", OverheadDetail.OHRate);
                DT1.Rows.Add("MasterFile.OverheadDetail", "0", "Field1", OverheadDetail.Field1);
                DT1.Rows.Add("MasterFile.OverheadDetail", "0", "Field2", OverheadDetail.Field2);
                DT1.Rows.Add("MasterFile.OverheadDetail", "0", "Field3", OverheadDetail.Field3);
                DT1.Rows.Add("MasterFile.OverheadDetail", "0", "Field4", OverheadDetail.Field4);
                DT1.Rows.Add("MasterFile.OverheadDetail", "0", "Field5", OverheadDetail.Field5);
                DT1.Rows.Add("MasterFile.OverheadDetail", "0", "Field6", OverheadDetail.Field6);
                DT1.Rows.Add("MasterFile.OverheadDetail", "0", "Field7", OverheadDetail.Field7);
                DT1.Rows.Add("MasterFile.OverheadDetail", "0", "Field8", OverheadDetail.Field8);
                DT1.Rows.Add("MasterFile.OverheadDetail", "0", "Field9", OverheadDetail.Field9);


                DT2.Rows.Add("MasterFile.Overhead", "cond", "OHCode", Code);
                DT2.Rows.Add("MasterFile.Overhead", "set", "IsWithDetail", "True");

                Gears.CreateData(DT1, Conn);
                Gears.UpdateData(DT2, Conn);

            }
            public void UpdateOverheadDetail(OverheadDetail OverheadDetail)
            {

                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                DT1.Rows.Add("MasterFile.OverheadDetail", "cond", "OHCode", Code);
		DT1.Rows.Add("MasterFile.OverheadDetail", "cond", "LineNumber", OverheadDetail.LineNumber);
                DT1.Rows.Add("MasterFile.OverheadDetail", "set", "RateCode", OverheadDetail.RateCode);
                DT1.Rows.Add("MasterFile.OverheadDetail", "set", "Description", OverheadDetail.Description);
		DT1.Rows.Add("MasterFile.OverheadDetail", "set", "OHRate", OverheadDetail.OHRate);
                DT1.Rows.Add("MasterFile.OverheadDetail", "set", "Field1", OverheadDetail.Field1);
                DT1.Rows.Add("MasterFile.OverheadDetail", "set", "Field2", OverheadDetail.Field2);
                DT1.Rows.Add("MasterFile.OverheadDetail", "set", "Field3", OverheadDetail.Field3);
                DT1.Rows.Add("MasterFile.OverheadDetail", "set", "Field4", OverheadDetail.Field4);
                DT1.Rows.Add("MasterFile.OverheadDetail", "set", "Field5", OverheadDetail.Field5);
                DT1.Rows.Add("MasterFile.OverheadDetail", "set", "Field6", OverheadDetail.Field6);
                DT1.Rows.Add("MasterFile.OverheadDetail", "set", "Field7", OverheadDetail.Field7);
                DT1.Rows.Add("MasterFile.OverheadDetail", "set", "Field8", OverheadDetail.Field8);
                DT1.Rows.Add("MasterFile.OverheadDetail", "set", "Field9", OverheadDetail.Field9);

                Gears.UpdateData(DT1, Conn);                               
            }
            public void DeleteOverheadDetail(OverheadDetail OverheadDetail)
            {


                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
                DT1.Rows.Add("MasterFile.OverheadDetail", "cond", "OHCode", Code);
                DT1.Rows.Add("MasterFile.OverheadDetail", "cond", "LineNumber", OverheadDetail.LineNumber);
                Gears.DeleteData(DT1, Conn);

                DataTable count = Gears.RetriveData2("select * from MasterFile.OverheadDetail where OHCode = '" + Code + "'", Conn);

                if (count.Rows.Count < 1)
                {
                    DT2.Rows.Add("MasterFile.Overhead", "cond", "OHCode", OverheadDetail.OHCode);
                    DT2.Rows.Add("MasterFile.Overhead", "set", "IsWithDetail", "False");
                    Gears.UpdateData(DT2, Conn);
                }

            }
        }


        public DataTable getdata(string Trans, string Conn)
        {
            DataTable a;

            a = Gears.RetriveData2("select * from MasterFile.Overhead where OHCode = '" + Trans + "'", Conn);
            foreach (DataRow dtRow in a.Rows)
            {
                OHCode = dtRow["OHCode"].ToString();
                Description = dtRow["Description"].ToString();
		OverheadType = dtRow["OverheadType"].ToString();
                OverheadCost = Convert.ToDecimal(Convert.IsDBNull(dtRow["OverheadCost"]) ? 0 : dtRow["OverheadCost"]);
                IsInActive = Convert.ToBoolean(Convert.IsDBNull(dtRow["IsInactive"]) ? false : dtRow["IsInactive"]);
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
                IsWithDetail = Convert.ToBoolean(Convert.IsDBNull(dtRow["IsWithDetail"]) ? false : dtRow["IsWithDetail"]);

            }

            return a;
        }
        public void InsertData(Overhead _ent)
        {
            Conn = _ent.Connection;  //Ter
            Code = _ent.OHCode;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("MasterFile.Overhead", "0", "OHCode", _ent.OHCode);
            DT1.Rows.Add("MasterFile.Overhead", "0", "Description", _ent.Description);
            DT1.Rows.Add("MasterFile.Overhead", "0", "OverheadCost", _ent.OverheadCost);
            DT1.Rows.Add("MasterFile.Overhead", "0", "OverheadType", _ent.OverheadType);
            DT1.Rows.Add("MasterFile.Overhead", "0", "IsInactive", _ent.IsInActive);
	    DT1.Rows.Add("MasterFile.Overhead", "0", "IsWithDetail", _ent.IsWithDetail);
            DT1.Rows.Add("MasterFile.Overhead", "0", "AddedBy", _ent.AddedBy);
            DT1.Rows.Add("MasterFile.Overhead", "0", "AddedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            DT1.Rows.Add("MasterFile.Overhead", "0", "Field1", _ent.Field1);
            DT1.Rows.Add("MasterFile.Overhead", "0", "Field2", _ent.Field2);
            DT1.Rows.Add("MasterFile.Overhead", "0", "Field3", _ent.Field3);
            DT1.Rows.Add("MasterFile.Overhead", "0", "Field4", _ent.Field4);
            DT1.Rows.Add("MasterFile.Overhead", "0", "Field5", _ent.Field5);
            DT1.Rows.Add("MasterFile.Overhead", "0", "Field6", _ent.Field6);
            DT1.Rows.Add("MasterFile.Overhead", "0", "Field7", _ent.Field7);
            DT1.Rows.Add("MasterFile.Overhead", "0", "Field8", _ent.Field8);
            DT1.Rows.Add("MasterFile.Overhead", "0", "Field9", _ent.Field9);

            Gears.CreateData(DT1, _ent.Connection); // TER



            Gears.CreateData(DT1, _ent.Connection);
        }

        public void UpdateData(Overhead _ent)
        {
                       Conn = _ent.Connection; //Ter
                       Code = _ent.OHCode;
            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("MasterFile.Overhead", "cond", "OHCode", _ent.OHCode);
            DT1.Rows.Add("MasterFile.Overhead", "set", "Description", _ent.Description);
            DT1.Rows.Add("MasterFile.Overhead", "set", "OverheadCost", _ent.OverheadCost);
            DT1.Rows.Add("MasterFile.Overhead", "set", "OverheadType", _ent.OverheadType);
            DT1.Rows.Add("MasterFile.Overhead", "set", "IsWithDetail", _ent.IsWithDetail);
            DT1.Rows.Add("MasterFile.Overhead", "set", "IsInactive", _ent.IsInActive);
            DT1.Rows.Add("MasterFile.Overhead", "set", "LastEditedBy", _ent.AddedBy);
            DT1.Rows.Add("MasterFile.Overhead", "set", "LastEditedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            DT1.Rows.Add("MasterFile.Overhead", "set", "Field1", _ent.Field1);
            DT1.Rows.Add("MasterFile.Overhead", "set", "Field2", _ent.Field2);
            DT1.Rows.Add("MasterFile.Overhead", "set", "Field3", _ent.Field3);
            DT1.Rows.Add("MasterFile.Overhead", "set", "Field4", _ent.Field4);
            DT1.Rows.Add("MasterFile.Overhead", "set", "Field5", _ent.Field5);
            DT1.Rows.Add("MasterFile.Overhead", "set", "Field6", _ent.Field6);
            DT1.Rows.Add("MasterFile.Overhead", "set", "Field7", _ent.Field7);
            DT1.Rows.Add("MasterFile.Overhead", "set", "Field8", _ent.Field8);
            DT1.Rows.Add("MasterFile.Overhead", "set", "Field9", _ent.Field9);

            string strErr = Gears.UpdateData(DT1, _ent.Connection); // Ter
            Functions.AuditTrail("REFOVRH", _ent.OHCode, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "UPDATE", _ent.Connection); // Ter

            strErr = Gears.UpdateData(DT1, _ent.Connection); // Ter
        }

        public void DeleteData(Overhead _ent)
        {
            Conn = _ent.Connection; //Ter
            Code = _ent.OHCode;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("MasterFile.Overhead", "cond", "OHCode", _ent.OHCode);
            Gears.DeleteData(DT1, _ent.Connection);

            Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
            DT2.Rows.Add("MasterFile.OverheadDetail", "cond", "OHCode",  _ent.OHCode);
            Gears.DeleteData(DT2, _ent.Connection);

            Functions.AuditTrail("REFOVRH", _ent.OHCode, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "DELETE", _ent.Connection);
        }
    }
}
