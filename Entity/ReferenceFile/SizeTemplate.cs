using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GearsLibrary;
using System.Data;

namespace Entity
{
    public class SizeTemplate
    {
        private static string Conn; //Ter
        public virtual string Connection { get; set; } //ter
        
        private static string SizeTempCode;
        public virtual string SizeTemplateCode { get; set; }
        public virtual string Description { get; set; }
        public virtual string SizeType { get; set; }
        public virtual bool IsInactive { get; set; }
        //public virtual bool IsWithDetail { get; set; }


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

        public virtual IList<SizeTemplateDetail> Detail { get; set; }


        public class SizeTemplateDetail
        {
            public virtual SizeTemplate Parent { get; set; }
            public virtual string SizeTemplateCode { get; set; }
            
            public virtual string SizeCode { get; set; }
            public virtual string SizeName { get; set; }
            public virtual string Length { get; set; }
            public virtual int SortNumber { get; set; }

            public virtual string Field1 { get; set; }
            public virtual string Field2 { get; set; }
            public virtual string Field3 { get; set; }
            public virtual string Field4 { get; set; }
            public virtual string Field5 { get; set; }
            public virtual string Field6 { get; set; }
            public virtual string Field7 { get; set; }
            public virtual string Field8 { get; set; }
            public virtual string Field9 { get; set; }

            public DataTable getdetail(string docnumber, string Conn)
            {

                DataTable a;
                try
                {
                    a = Gears.RetriveData2("select * from Masterfile.SizeTemplateDetail where SizeTemplateCode ='" + docnumber + "' order by RecordId", Conn);
                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }

            public void AddSizeTemplateDetail(SizeTemplateDetail SizeTemplateDetail)
            {


                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();

                DT1.Rows.Add("Masterfile.SizeTemplateDetail", "0", "SizeTemplateCode",SizeTempCode);
                DT1.Rows.Add("Masterfile.SizeTemplateDetail", "0", "SizeCode", SizeTemplateDetail.SizeCode);
                DT1.Rows.Add("Masterfile.SizeTemplateDetail", "0", "SizeName", SizeTemplateDetail.SizeName);
                DT1.Rows.Add("Masterfile.SizeTemplateDetail", "0", "Length", SizeTemplateDetail.Length);
                DT1.Rows.Add("Masterfile.SizeTemplateDetail", "0", "SortNumber", SizeTemplateDetail.SortNumber);
               


                DT1.Rows.Add("Masterfile.SizeTemplateDetail", "0", "Field1", SizeTemplateDetail.Field1);
                DT1.Rows.Add("Masterfile.SizeTemplateDetail", "0", "Field2", SizeTemplateDetail.Field2);
                DT1.Rows.Add("Masterfile.SizeTemplateDetail", "0", "Field3", SizeTemplateDetail.Field3);
                DT1.Rows.Add("Masterfile.SizeTemplateDetail", "0", "Field4", SizeTemplateDetail.Field4);
                DT1.Rows.Add("Masterfile.SizeTemplateDetail", "0", "Field5", SizeTemplateDetail.Field5);
                DT1.Rows.Add("Masterfile.SizeTemplateDetail", "0", "Field6", SizeTemplateDetail.Field6);
                DT1.Rows.Add("Masterfile.SizeTemplateDetail", "0", "Field7", SizeTemplateDetail.Field7);
                DT1.Rows.Add("Masterfile.SizeTemplateDetail", "0", "Field8", SizeTemplateDetail.Field8);
                DT1.Rows.Add("Masterfile.SizeTemplateDetail", "0", "Field9", SizeTemplateDetail.Field9);

                DT2.Rows.Add("Masterfile.SizeTemplate", "cond", "SizeTemplateCode", SizeTempCode);
                DT2.Rows.Add("Masterfile.SizeTemplate", "set", "IsWithDetail", "True");

                Gears.CreateData(DT1, Conn);
                Gears.UpdateData(DT2, Conn);



            }
            public void UpdateSizeTemplateDetail(SizeTemplateDetail SizeTemplateDetail)
            {



                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                DT1.Rows.Add("Masterfile.SizeTemplateDetail", "cond", "SizeTemplateCode", SizeTemplateDetail.SizeTemplateCode);
                DT1.Rows.Add("Masterfile.SizeTemplateDetail", "set", "SizeCode", SizeTemplateDetail.SizeCode);
                DT1.Rows.Add("Masterfile.SizeTemplateDetail", "set", "SizeName", SizeTemplateDetail.SizeName);
                DT1.Rows.Add("Masterfile.SizeTemplateDetail", "set", "Length", SizeTemplateDetail.Length);
                DT1.Rows.Add("Masterfile.SizeTemplateDetail", "set", "SortNumber", SizeTemplateDetail.SortNumber);
                


                DT1.Rows.Add("Masterfile.SizeTemplateDetail", "set", "Field1", SizeTemplateDetail.Field1);
                DT1.Rows.Add("Masterfile.SizeTemplateDetail", "set", "Field2", SizeTemplateDetail.Field2);
                DT1.Rows.Add("Masterfile.SizeTemplateDetail", "set", "Field3", SizeTemplateDetail.Field3);
                DT1.Rows.Add("Masterfile.SizeTemplateDetail", "set", "Field4", SizeTemplateDetail.Field4);
                DT1.Rows.Add("Masterfile.SizeTemplateDetail", "set", "Field5", SizeTemplateDetail.Field5);
                DT1.Rows.Add("Masterfile.SizeTemplateDetail", "set", "Field6", SizeTemplateDetail.Field6);
                DT1.Rows.Add("Masterfile.SizeTemplateDetail", "set", "Field7", SizeTemplateDetail.Field7);
                DT1.Rows.Add("Masterfile.SizeTemplateDetail", "set", "Field8", SizeTemplateDetail.Field8);
                DT1.Rows.Add("Masterfile.SizeTemplateDetail", "set", "Field9", SizeTemplateDetail.Field9);

                Gears.UpdateData(DT1, Conn);
            }
            public void DeleteSizeTemplateDetail(SizeTemplateDetail SizeTemplateDetail)
            {


                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();

                DT1.Rows.Add("Masterfile.SizeTemplateDetail", "cond", "SizeTemplateCode", SizeTemplateDetail.SizeTemplateCode);
                DT1.Rows.Add("Masterfile.SizeTemplateDetail", "cond", "SizeCode", SizeTemplateDetail.SizeCode);


                Gears.DeleteData(DT1, Conn);


                DataTable count = Gears.RetriveData2("select * from Masterfile.SizeTemplateDetail where SizeTemplateCode = '" + SizeTempCode + "'", Conn);

                if (count.Rows.Count < 1)
                {
                    DT2.Rows.Add("Masterfile.SizeTemplate", "cond", "SizeTemplateCode", SizeTempCode);
                    DT2.Rows.Add("Masterfile.SizeTemplate", "set", "IsWithDetail", "False");
                    Gears.UpdateData(DT2, Conn);
                }

            }
        }
        public DataTable getdata(string SizeTempCode, string Conn) //Ter
        {
            DataTable a;

            if (SizeTempCode != null)
            {
                a = Gears.RetriveData2("select * from Masterfile.SizeTemplate where SizeTemplateCode = '" + SizeTempCode + "'", Conn); //Ter
                foreach (DataRow dtRow in a.Rows)
                {
                    SizeTemplateCode = dtRow["SizeTemplateCode"].ToString();
                    Description = dtRow["Description"].ToString();
                    SizeType = dtRow["SizeType"].ToString();
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
            }
            else
            {
                a = Gears.RetriveData2("select '' as FunctionalGroupID,'' as Description,'' as AssignHead,'' as DateClosed,'' as Days", Conn); //Ter
            }
            return a;
        }
        public void InsertData(SizeTemplate _ent)
        {
            Conn = _ent.Connection; //Ter
            SizeTempCode = _ent.SizeTemplateCode;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Masterfile.SizeTemplate", "0", "SizeTemplateCode", _ent.SizeTemplateCode);
            DT1.Rows.Add("Masterfile.SizeTemplate", "0", "Description", _ent.Description);
            DT1.Rows.Add("Masterfile.SizeTemplate", "0", "SizeType", _ent.SizeType);
            DT1.Rows.Add("Masterfile.SizeTemplate", "0", "IsInactive", _ent.IsInactive);
            DT1.Rows.Add("Masterfile.SizeTemplate", "0", "AddedBy", _ent.AddedBy);
            DT1.Rows.Add("Masterfile.SizeTemplate", "0", "AddedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            DT1.Rows.Add("Masterfile.SizeTemplate", "0", "Field1", _ent.Field1);
            DT1.Rows.Add("Masterfile.SizeTemplate", "0", "Field2", _ent.Field2);
            DT1.Rows.Add("Masterfile.SizeTemplate", "0", "Field3", _ent.Field3);
            DT1.Rows.Add("Masterfile.SizeTemplate", "0", "Field4", _ent.Field4);
            DT1.Rows.Add("Masterfile.SizeTemplate", "0", "Field5", _ent.Field5);
            DT1.Rows.Add("Masterfile.SizeTemplate", "0", "Field6", _ent.Field6);
            DT1.Rows.Add("Masterfile.SizeTemplate", "0", "Field7", _ent.Field7);
            DT1.Rows.Add("Masterfile.SizeTemplate", "0", "Field8", _ent.Field8);
            DT1.Rows.Add("Masterfile.SizeTemplate", "0", "Field9", _ent.Field9);

            Gears.CreateData(DT1, _ent.Connection); // TER
        }

        public void UpdateData(SizeTemplate _ent)
        {
            Conn = _ent.Connection; //Ter
            SizeTempCode = _ent.SizeTemplateCode;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Masterfile.SizeTemplate", "cond", "SizeTemplateCode", _ent.SizeTemplateCode);
            DT1.Rows.Add("Masterfile.SizeTemplate", "set", "Description", _ent.Description);
            DT1.Rows.Add("Masterfile.SizeTemplate", "set", "SizeType", _ent.SizeType);
            DT1.Rows.Add("Masterfile.SizeTemplate", "set", "IsInactive", _ent.IsInactive);
            DT1.Rows.Add("Masterfile.SizeTemplate", "set", "LastEditedBy", _ent.AddedBy);
            DT1.Rows.Add("Masterfile.SizeTemplate", "set", "LastEditedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            DT1.Rows.Add("Masterfile.SizeTemplate", "set", "Field1", _ent.Field1);
            DT1.Rows.Add("Masterfile.SizeTemplate", "set", "Field2", _ent.Field2);
            DT1.Rows.Add("Masterfile.SizeTemplate", "set", "Field3", _ent.Field3);
            DT1.Rows.Add("Masterfile.SizeTemplate", "set", "Field4", _ent.Field4);
            DT1.Rows.Add("Masterfile.SizeTemplate", "set", "Field5", _ent.Field5);
            DT1.Rows.Add("Masterfile.SizeTemplate", "set", "Field6", _ent.Field6);
            DT1.Rows.Add("Masterfile.SizeTemplate", "set", "Field7", _ent.Field7);
            DT1.Rows.Add("Masterfile.SizeTemplate", "set", "Field8", _ent.Field8);
            DT1.Rows.Add("Masterfile.SizeTemplate", "set", "Field9", _ent.Field9);

            string strErr = Gears.UpdateData(DT1, _ent.Connection); // Ter
            Functions.AuditTrail("REFSCT", _ent.SizeTemplateCode, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "UPDATE", _ent.Connection); // Ter

            strErr = Gears.UpdateData(DT1, _ent.Connection); // Ter
        }

        public void DeleteData(SizeTemplate _ent)
        {
            Conn = _ent.Connection; //Ter
            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Masterfile.SizeTemplate", "cond", "SizeTemplateCode", _ent.SizeTemplateCode);
            Gears.DeleteData(DT1, _ent.Connection); // Ter
            Functions.AuditTrail("REFSCT", _ent.SizeTemplateCode, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "DELETE", _ent.Connection); // Ter
        }
    }
}
