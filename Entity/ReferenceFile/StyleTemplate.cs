using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GearsLibrary;
using System.Data;

namespace Entity
{
    public class StyleTemplate
    {
        private static string Code;

        private static string Conn;//ADD CONN
        public virtual string Connection { get; set; }//ADD CONN

        public virtual string StyleCode { get; set; }
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


        public virtual IList<StyleTemplateDetail> Detail { get; set; }
        public class StyleTemplateDetail
        {
            public virtual StyleTemplate Parent { get; set; } 
            public virtual string LineNumber { get; set; }
            public virtual string StyleCode { get; set; }
            public virtual string Component { get; set; }
            public virtual string Step { get; set; }
            public virtual string ItemCode { get; set; }
            public virtual string ColorCode { get; set; }
            public virtual string ClassCode { get; set; }
            public virtual string SizeCode { get; set; }
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
                    a = Gears.RetriveData2("select * from MasterFile.StyleTemplateDetail where StyleCode='" + Trans + "'", Conn);
                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }

            public void AddStyleTemplateDetail(StyleTemplateDetail StyleTemplateDetail)
            {
                int linenum = 0;

                DataTable count = Gears.RetriveData2("select max(convert(int,LineNumber)) as LineNumber from MasterFile.StyleTemplateDetail where StyleCode= '" + Code+ "'", Conn);

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
                DT1.Rows.Add("MasterFile.StyleTemplateDetail", "0", "StyleCode", Code);
		DT1.Rows.Add("MasterFile.StyleTemplateDetail", "0", "LineNumber", strLine);
        DT1.Rows.Add("MasterFile.StyleTemplateDetail", "0", "Component", StyleTemplateDetail.Component);
        DT1.Rows.Add("MasterFile.StyleTemplateDetail", "0", "Step", StyleTemplateDetail.Step);
                DT1.Rows.Add("MasterFile.StyleTemplateDetail", "0", "ItemCode", StyleTemplateDetail.ItemCode);
                DT1.Rows.Add("MasterFile.StyleTemplateDetail", "0", "ColorCode", StyleTemplateDetail.ColorCode);
                DT1.Rows.Add("MasterFile.StyleTemplateDetail", "0", "ClassCode", StyleTemplateDetail.ClassCode);
                DT1.Rows.Add("MasterFile.StyleTemplateDetail", "0", "SizeCode", StyleTemplateDetail.SizeCode);
                DT1.Rows.Add("MasterFile.StyleTemplateDetail", "0", "Field1", StyleTemplateDetail.Field1);
                DT1.Rows.Add("MasterFile.StyleTemplateDetail", "0", "Field2", StyleTemplateDetail.Field2);
                DT1.Rows.Add("MasterFile.StyleTemplateDetail", "0", "Field3", StyleTemplateDetail.Field3);
                DT1.Rows.Add("MasterFile.StyleTemplateDetail", "0", "Field4", StyleTemplateDetail.Field4);
                DT1.Rows.Add("MasterFile.StyleTemplateDetail", "0", "Field5", StyleTemplateDetail.Field5);
                DT1.Rows.Add("MasterFile.StyleTemplateDetail", "0", "Field6", StyleTemplateDetail.Field6);
                DT1.Rows.Add("MasterFile.StyleTemplateDetail", "0", "Field7", StyleTemplateDetail.Field7);
                DT1.Rows.Add("MasterFile.StyleTemplateDetail", "0", "Field8", StyleTemplateDetail.Field8);
                DT1.Rows.Add("MasterFile.StyleTemplateDetail", "0", "Field9", StyleTemplateDetail.Field9);


                DT2.Rows.Add("MasterFile.StyleTemplate", "cond", "StyleCode", Code);
                DT2.Rows.Add("MasterFile.StyleTemplate", "set", "IsWithDetail", "True");

                Gears.CreateData(DT1, Conn);
                Gears.UpdateData(DT2, Conn);

            }
            public void UpdateStyleTemplateDetail(StyleTemplateDetail StyleTemplateDetail)
            {

                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                DT1.Rows.Add("MasterFile.StyleTemplateDetail", "cond", "StyleCode", StyleTemplateDetail.StyleCode);
		DT1.Rows.Add("MasterFile.StyleTemplateDetail", "cond", "LineNumber", StyleTemplateDetail.LineNumber);
        DT1.Rows.Add("MasterFile.StyleTemplateDetail", "set", "Component", StyleTemplateDetail.Component);
        DT1.Rows.Add("MasterFile.StyleTemplateDetail", "set", "Step", StyleTemplateDetail.Step);
                DT1.Rows.Add("MasterFile.StyleTemplateDetail", "set", "ItemCode", StyleTemplateDetail.ItemCode);
                DT1.Rows.Add("MasterFile.StyleTemplateDetail", "set", "ColorCode", StyleTemplateDetail.ColorCode);
                DT1.Rows.Add("MasterFile.StyleTemplateDetail", "set", "ClassCode", StyleTemplateDetail.ClassCode);
                DT1.Rows.Add("MasterFile.StyleTemplateDetail", "set", "SizeCode", StyleTemplateDetail.SizeCode);
                DT1.Rows.Add("MasterFile.StyleTemplateDetail", "set", "Field1", StyleTemplateDetail.Field1);
                DT1.Rows.Add("MasterFile.StyleTemplateDetail", "set", "Field2", StyleTemplateDetail.Field2);
                DT1.Rows.Add("MasterFile.StyleTemplateDetail", "set", "Field3", StyleTemplateDetail.Field3);
                DT1.Rows.Add("MasterFile.StyleTemplateDetail", "set", "Field4", StyleTemplateDetail.Field4);
                DT1.Rows.Add("MasterFile.StyleTemplateDetail", "set", "Field5", StyleTemplateDetail.Field5);
                DT1.Rows.Add("MasterFile.StyleTemplateDetail", "set", "Field6", StyleTemplateDetail.Field6);
                DT1.Rows.Add("MasterFile.StyleTemplateDetail", "set", "Field7", StyleTemplateDetail.Field7);
                DT1.Rows.Add("MasterFile.StyleTemplateDetail", "set", "Field8", StyleTemplateDetail.Field8);
                DT1.Rows.Add("MasterFile.StyleTemplateDetail", "set", "Field9", StyleTemplateDetail.Field9);

                Gears.UpdateData(DT1, Conn);                               
            }
            public void DeleteStyleTemplateDetail(StyleTemplateDetail StyleTemplateDetail)
            {


                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
                DT1.Rows.Add("MasterFile.StyleTemplateDetail", "cond", "StyleCode", StyleTemplateDetail.StyleCode);
                DT1.Rows.Add("MasterFile.StyleTemplateDetail", "cond", "LineNumber", StyleTemplateDetail.LineNumber);
                Gears.DeleteData(DT1, Conn);

                DataTable count = Gears.RetriveData2("select * from MasterFile.StyleTemplateDetail where StyleCode = '" + Code + "'", Conn);

                if (count.Rows.Count < 1)
                {
                    DT2.Rows.Add("MasterFile.StyleTemplate", "cond", "StyleCode", StyleTemplateDetail.StyleCode);
                    DT2.Rows.Add("MasterFile.StyleTemplate", "set", "IsWithDetail", "False");
                    Gears.UpdateData(DT2, Conn);
                }

            }
        }


        public DataTable getdata(string Trans, string Conn)
        {
            DataTable a;

            a = Gears.RetriveData2("select * from MasterFile.StyleTemplate where StyleCode = '" + Trans + "'", Conn);
            foreach (DataRow dtRow in a.Rows)
            {
                StyleCode = dtRow["StyleCode"].ToString();
                Description = dtRow["Description"].ToString();
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
        public void InsertData(StyleTemplate _ent)
        {
            Conn = _ent.Connection; //Ter
            Code = _ent.StyleCode;
            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("MasterFile.StyleTemplate", "0", "StyleCode", _ent.StyleCode);
            DT1.Rows.Add("MasterFile.StyleTemplate", "0", "Description", _ent.Description);
            DT1.Rows.Add("MasterFile.StyleTemplate", "0", "IsInactive", _ent.IsInActive);
	    DT1.Rows.Add("MasterFile.StyleTemplate", "0", "IsWithDetail", _ent.IsWithDetail);
            DT1.Rows.Add("MasterFile.StyleTemplate", "0", "AddedBy", _ent.AddedBy);
            DT1.Rows.Add("MasterFile.StyleTemplate", "0", "AddedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            DT1.Rows.Add("MasterFile.StyleTemplate", "0", "Field1", _ent.Field1);
            DT1.Rows.Add("MasterFile.StyleTemplate", "0", "Field2", _ent.Field2);
            DT1.Rows.Add("MasterFile.StyleTemplate", "0", "Field3", _ent.Field3);
            DT1.Rows.Add("MasterFile.StyleTemplate", "0", "Field4", _ent.Field4);
            DT1.Rows.Add("MasterFile.StyleTemplate", "0", "Field5", _ent.Field5);
            DT1.Rows.Add("MasterFile.StyleTemplate", "0", "Field6", _ent.Field6);
            DT1.Rows.Add("MasterFile.StyleTemplate", "0", "Field7", _ent.Field7);
            DT1.Rows.Add("MasterFile.StyleTemplate", "0", "Field8", _ent.Field8);
            DT1.Rows.Add("MasterFile.StyleTemplate", "0", "Field9", _ent.Field9);

            Gears.CreateData(DT1, _ent.Connection); // TER



            Gears.CreateData(DT1, _ent.Connection);
        }

        public void UpdateData(StyleTemplate _ent)
        {
                       Conn = _ent.Connection; //Ter
                       Code = _ent.StyleCode;
            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("MasterFile.StyleTemplate", "cond", "StyleCode", _ent.StyleCode);
            DT1.Rows.Add("MasterFile.StyleTemplate", "set", "Description", _ent.Description);
            DT1.Rows.Add("MasterFile.StyleTemplate", "set", "IsWithDetail", _ent.IsWithDetail);
            DT1.Rows.Add("MasterFile.StyleTemplate", "set", "IsInactive", _ent.IsInActive);
            DT1.Rows.Add("MasterFile.StyleTemplate", "set", "LastEditedBy", _ent.AddedBy);
            DT1.Rows.Add("MasterFile.StyleTemplate", "set", "LastEditedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            DT1.Rows.Add("MasterFile.StyleTemplate", "set", "Field1", _ent.Field1);
            DT1.Rows.Add("MasterFile.StyleTemplate", "set", "Field2", _ent.Field2);
            DT1.Rows.Add("MasterFile.StyleTemplate", "set", "Field3", _ent.Field3);
            DT1.Rows.Add("MasterFile.StyleTemplate", "set", "Field4", _ent.Field4);
            DT1.Rows.Add("MasterFile.StyleTemplate", "set", "Field5", _ent.Field5);
            DT1.Rows.Add("MasterFile.StyleTemplate", "set", "Field6", _ent.Field6);
            DT1.Rows.Add("MasterFile.StyleTemplate", "set", "Field7", _ent.Field7);
            DT1.Rows.Add("MasterFile.StyleTemplate", "set", "Field8", _ent.Field8);
            DT1.Rows.Add("MasterFile.StyleTemplate", "set", "Field9", _ent.Field9);

            string strErr = Gears.UpdateData(DT1, _ent.Connection); // Ter
            Functions.AuditTrail("REFSTYT", _ent.StyleCode, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "UPDATE", _ent.Connection); // Ter

            strErr = Gears.UpdateData(DT1, _ent.Connection); // Ter
        }

        public void DeleteData(StyleTemplate _ent)
        {
            Conn = _ent.Connection;
            Code = _ent.StyleCode;
            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("MasterFile.StyleTemplate", "cond", "StyleCode", _ent.StyleCode);
            Gears.DeleteData(DT1, _ent.Connection);

            Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
            DT2.Rows.Add("MasterFile.StyleTemplateDetail", "cond", "StyleCode",  _ent.StyleCode);
            Gears.DeleteData(DT2, _ent.Connection);

            Functions.AuditTrail("REFSTYT", _ent.StyleCode, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "DELETE", _ent.Connection);
        }
    }
}
