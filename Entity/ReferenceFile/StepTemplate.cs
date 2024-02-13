using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GearsLibrary;
using System.Data;

namespace Entity
{
    public class StepTemplate
    {
        private static string Code;

        private static string Conn;//ADD CONN
        public virtual string Connection { get; set; }//ADD CONN

        public virtual string StepTemplateCode { get; set; }
	    public virtual string ParentStepCode { get; set; }
        public virtual string Description { get; set; }
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


        public virtual IList<StepTemplateDetail> Detail { get; set; }
        public class StepTemplateDetail
        {
            public virtual StepTemplate Parent { get; set; } 
            public virtual string LineNumber { get; set; }
            public virtual string StepTemplateCode { get; set; }
            public virtual decimal Sequence { get; set; }
            public virtual string StepCode { get; set; }
            public virtual string Description { get; set; }
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
                    a = Gears.RetriveData2("select * from MasterFile.StepTemplateDetail where StepTemplateCode='" + Trans + "'", Conn);
                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }

            public void AddStepTemplateDetail(StepTemplateDetail StepTemplateDetail)
            {
                int linenum = 0;

                DataTable count = Gears.RetriveData2("select max(convert(int,LineNumber)) as LineNumber from MasterFile.StepTemplateDetail where StepTemplateCode= '" + Code+ "'", Conn);

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
                DT1.Rows.Add("MasterFile.StepTemplateDetail", "0", "StepTemplateCode", Code);
		        DT1.Rows.Add("MasterFile.StepTemplateDetail", "0", "LineNumber", strLine);
                DT1.Rows.Add("MasterFile.StepTemplateDetail", "0", "Sequence", StepTemplateDetail.Sequence);
                DT1.Rows.Add("MasterFile.StepTemplateDetail", "0", "StepCode", StepTemplateDetail.StepCode);
                DT1.Rows.Add("MasterFile.StepTemplateDetail", "0", "Description", StepTemplateDetail.Description);
                DT1.Rows.Add("MasterFile.StepTemplateDetail", "0", "Field1", StepTemplateDetail.Field1);
                DT1.Rows.Add("MasterFile.StepTemplateDetail", "0", "Field2", StepTemplateDetail.Field2);
                DT1.Rows.Add("MasterFile.StepTemplateDetail", "0", "Field3", StepTemplateDetail.Field3);
                DT1.Rows.Add("MasterFile.StepTemplateDetail", "0", "Field4", StepTemplateDetail.Field4);
                DT1.Rows.Add("MasterFile.StepTemplateDetail", "0", "Field5", StepTemplateDetail.Field5);
                DT1.Rows.Add("MasterFile.StepTemplateDetail", "0", "Field6", StepTemplateDetail.Field6);
                DT1.Rows.Add("MasterFile.StepTemplateDetail", "0", "Field7", StepTemplateDetail.Field7);
                DT1.Rows.Add("MasterFile.StepTemplateDetail", "0", "Field8", StepTemplateDetail.Field8);
                DT1.Rows.Add("MasterFile.StepTemplateDetail", "0", "Field9", StepTemplateDetail.Field9);


                DT2.Rows.Add("MasterFile.StepTemplate", "cond", "StepTemplateCode", Code);
                DT2.Rows.Add("MasterFile.StepTemplate", "set", "IsWithDetail", "True");

                Gears.CreateData(DT1, Conn);
                Gears.UpdateData(DT2, Conn);

            }
            public void UpdateStepTemplateDetail(StepTemplateDetail StepTemplateDetail)
            {

                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                DT1.Rows.Add("MasterFile.StepTemplateDetail", "cond", "StepTemplateCode", StepTemplateDetail.StepTemplateCode);
		        DT1.Rows.Add("MasterFile.StepTemplateDetail", "cond", "LineNumber", StepTemplateDetail.LineNumber);
                DT1.Rows.Add("MasterFile.StepTemplateDetail", "set", "Sequence", StepTemplateDetail.Sequence);
                DT1.Rows.Add("MasterFile.StepTemplateDetail", "set", "StepCode", StepTemplateDetail.StepCode);
                DT1.Rows.Add("MasterFile.StepTemplateDetail", "set", "Description", StepTemplateDetail.Description);
                DT1.Rows.Add("MasterFile.StepTemplateDetail", "set", "Field1", StepTemplateDetail.Field1);
                DT1.Rows.Add("MasterFile.StepTemplateDetail", "set", "Field2", StepTemplateDetail.Field2);
                DT1.Rows.Add("MasterFile.StepTemplateDetail", "set", "Field3", StepTemplateDetail.Field3);
                DT1.Rows.Add("MasterFile.StepTemplateDetail", "set", "Field4", StepTemplateDetail.Field4);
                DT1.Rows.Add("MasterFile.StepTemplateDetail", "set", "Field5", StepTemplateDetail.Field5);
                DT1.Rows.Add("MasterFile.StepTemplateDetail", "set", "Field6", StepTemplateDetail.Field6);
                DT1.Rows.Add("MasterFile.StepTemplateDetail", "set", "Field7", StepTemplateDetail.Field7);
                DT1.Rows.Add("MasterFile.StepTemplateDetail", "set", "Field8", StepTemplateDetail.Field8);
                DT1.Rows.Add("MasterFile.StepTemplateDetail", "set", "Field9", StepTemplateDetail.Field9);

                Gears.UpdateData(DT1, Conn);                               
            }
            public void DeleteStepTemplateDetail(StepTemplateDetail StepTemplateDetail)
            {


                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
                DT1.Rows.Add("MasterFile.StepTemplateDetail", "cond", "StepTemplateCode", StepTemplateDetail.StepTemplateCode);
                DT1.Rows.Add("MasterFile.StepTemplateDetail", "cond", "LineNumber", StepTemplateDetail.LineNumber);
                Gears.DeleteData(DT1, Conn);

                DataTable count = Gears.RetriveData2("select * from MasterFile.StepTemplateDetail where StepTemplateCode = '" + Code + "'", Conn);

                if (count.Rows.Count < 1)
                {
                    DT2.Rows.Add("MasterFile.StepTemplate", "cond", "StepTemplateCode", StepTemplateDetail.StepTemplateCode);
                    DT2.Rows.Add("MasterFile.StepTemplate", "set", "IsWithDetail", "False");
                    Gears.UpdateData(DT2, Conn);
                }

            }
        }


        public DataTable getdata(string Trans, string Conn)
        {
            DataTable a;

            a = Gears.RetriveData2("select * from MasterFile.StepTemplate where StepTemplateCode = '" + Trans + "'", Conn);
            foreach (DataRow dtRow in a.Rows)
            {
                StepTemplateCode = dtRow["StepTemplateCode"].ToString();
                Description = dtRow["Description"].ToString();
		        ParentStepCode = dtRow["ParentStepCode"].ToString();
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
        public void InsertData(StepTemplate _ent)
        {
            Conn = _ent.Connection; //Ter
            Code = _ent.StepTemplateCode;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("MasterFile.StepTemplate", "0", "StepTemplateCode", _ent.StepTemplateCode);
	    DT1.Rows.Add("MasterFile.StepTemplate", "0", "ParentStepCode", _ent.ParentStepCode);
            DT1.Rows.Add("MasterFile.StepTemplate", "0", "Description", _ent.Description);
            DT1.Rows.Add("MasterFile.StepTemplate", "0", "IsInactive", _ent.IsInActive);
	    DT1.Rows.Add("MasterFile.StepTemplate", "0", "IsWithDetail", _ent.IsWithDetail);
            DT1.Rows.Add("MasterFile.StepTemplate", "0", "AddedBy", _ent.AddedBy);
            DT1.Rows.Add("MasterFile.StepTemplate", "0", "AddedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            DT1.Rows.Add("MasterFile.StepTemplate", "0", "Field1", _ent.Field1);
            DT1.Rows.Add("MasterFile.StepTemplate", "0", "Field2", _ent.Field2);
            DT1.Rows.Add("MasterFile.StepTemplate", "0", "Field3", _ent.Field3);
            DT1.Rows.Add("MasterFile.StepTemplate", "0", "Field4", _ent.Field4);
            DT1.Rows.Add("MasterFile.StepTemplate", "0", "Field5", _ent.Field5);
            DT1.Rows.Add("MasterFile.StepTemplate", "0", "Field6", _ent.Field6);
            DT1.Rows.Add("MasterFile.StepTemplate", "0", "Field7", _ent.Field7);
            DT1.Rows.Add("MasterFile.StepTemplate", "0", "Field8", _ent.Field8);
            DT1.Rows.Add("MasterFile.StepTemplate", "0", "Field9", _ent.Field9);

            Gears.CreateData(DT1, _ent.Connection); // TER



            Gears.CreateData(DT1, _ent.Connection);
        }

        public void UpdateData(StepTemplate _ent)
        {
            Conn = _ent.Connection; //Ter
            Code = _ent.StepTemplateCode;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("MasterFile.StepTemplate", "cond", "StepTemplateCode", _ent.StepTemplateCode);
            DT1.Rows.Add("MasterFile.StepTemplate", "set", "Description", _ent.Description);
	    DT1.Rows.Add("MasterFile.StepTemplate", "set", "ParentStepCode", _ent.ParentStepCode);
            DT1.Rows.Add("MasterFile.StepTemplate", "set", "IsWithDetail", _ent.IsWithDetail);
            DT1.Rows.Add("MasterFile.StepTemplate", "set", "IsInactive", _ent.IsInActive);
            DT1.Rows.Add("MasterFile.StepTemplate", "set", "LastEditedBy", _ent.AddedBy);
            DT1.Rows.Add("MasterFile.StepTemplate", "set", "LastEditedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            DT1.Rows.Add("MasterFile.StepTemplate", "set", "Field1", _ent.Field1);
            DT1.Rows.Add("MasterFile.StepTemplate", "set", "Field2", _ent.Field2);
            DT1.Rows.Add("MasterFile.StepTemplate", "set", "Field3", _ent.Field3);
            DT1.Rows.Add("MasterFile.StepTemplate", "set", "Field4", _ent.Field4);
            DT1.Rows.Add("MasterFile.StepTemplate", "set", "Field5", _ent.Field5);
            DT1.Rows.Add("MasterFile.StepTemplate", "set", "Field6", _ent.Field6);
            DT1.Rows.Add("MasterFile.StepTemplate", "set", "Field7", _ent.Field7);
            DT1.Rows.Add("MasterFile.StepTemplate", "set", "Field8", _ent.Field8);
            DT1.Rows.Add("MasterFile.StepTemplate", "set", "Field9", _ent.Field9);

            string strErr = Gears.UpdateData(DT1, _ent.Connection); // Ter
            Functions.AuditTrail("REFSTPT", _ent.StepTemplateCode, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "UPDATE", _ent.Connection); // Ter

            strErr = Gears.UpdateData(DT1, _ent.Connection); // Ter
        }

        public void DeleteData(StepTemplate _ent)
        {
            Conn = _ent.Connection;
            Code = _ent.StepTemplateCode;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("MasterFile.StepTemplate", "cond", "StepTemplateCode", _ent.StepTemplateCode);
            Gears.DeleteData(DT1, _ent.Connection);

            Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
            DT2.Rows.Add("MasterFile.StepTemplateDetail", "cond", "StepTemplateCode",  _ent.StepTemplateCode);
            Gears.DeleteData(DT2, _ent.Connection);

            Functions.AuditTrail("REFSTPT", _ent.StepTemplateCode, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "DELETE", _ent.Connection);
        }
    }
}