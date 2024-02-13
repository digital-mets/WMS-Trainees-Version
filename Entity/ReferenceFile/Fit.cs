using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GearsLibrary;
using System.Data;

namespace Entity
{
    public class Fit
    {
        private static string Conn; //Ter
        public virtual string Connection { get; set; } //ter

        private static string fit;
        public virtual string FitCode { get; set; }
        public virtual string FitName { get; set; }
        public virtual string Brand { get; set; }
        public virtual string GenderCode { get; set; }
        public virtual string ProductCategory { get; set; }
        public virtual string Remarks { get; set; }
        public virtual string Waist { get; set; }
        public virtual string FitType { get; set; }
        public virtual string Silhouette { get; set; }
        public virtual string MasterPattern { get; set; }
        public virtual string SizeTemplate { get; set; }
        public virtual string SizeCard { get; set; }
        public virtual string StandardSize { get; set; }
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

         public virtual IList<FitSizeDetail> Detail { get; set; }
         public virtual IList<MeasurementChartTemplate> Details { get; set; }


        public class FitSizeDetail
        {
            public virtual Fit Parent { get; set; }
            public virtual string FitCode { get; set; }
            
            public virtual string SizeCode { get; set; }
            public virtual string SizeName { get; set; }
            public virtual string Length { get; set; }
            public virtual int SortNumber { get; set; }

            //public virtual string Field1 { get; set; }
            //public virtual string Field2 { get; set; }
            //public virtual string Field3 { get; set; }
            //public virtual string Field4 { get; set; }
            //public virtual string Field5 { get; set; }
            //public virtual string Field6 { get; set; }
            //public virtual string Field7 { get; set; }
            //public virtual string Field8 { get; set; }
            //public virtual string Field9 { get; set; }

            public virtual string SizeTemplateCode { get; set; }

            public DataTable getdetail(string docnumber, string Conn)
            {

                DataTable a;
                try
                {
                    a = Gears.RetriveData2("select * from Masterfile.FitSizeDetail where FitCode ='" + docnumber + "' order by RecordId", Conn);
                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }

            public void AddFitSizeDetail(FitSizeDetail FitSizeDetail)
            {


                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();

                DT1.Rows.Add("Masterfile.FitSizeDetail", "0", "FitCode", fit);
                DT1.Rows.Add("Masterfile.FitSizeDetail", "0", "SizeCode", FitSizeDetail.SizeCode);
                
                DT1.Rows.Add("Masterfile.FitSizeDetail", "0", "SizeName", FitSizeDetail.SizeName);
                DT1.Rows.Add("Masterfile.FitSizeDetail", "0", "Length", FitSizeDetail.Length);
                DT1.Rows.Add("Masterfile.FitSizeDetail", "0", "SortNumber", FitSizeDetail.SortNumber);



                //DT1.Rows.Add("Masterfile.FitSizeDetail", "0", "Field1", FitSizeDetail.Field1);
                //DT1.Rows.Add("Masterfile.FitSizeDetail", "0", "Field2", FitSizeDetail.Field2);
                //DT1.Rows.Add("Masterfile.FitSizeDetail", "0", "Field3", FitSizeDetail.Field3);
                //DT1.Rows.Add("Masterfile.FitSizeDetail", "0", "Field4", FitSizeDetail.Field4);
                //DT1.Rows.Add("Masterfile.FitSizeDetail", "0", "Field5", FitSizeDetail.Field5);
                //DT1.Rows.Add("Masterfile.FitSizeDetail", "0", "Field6", FitSizeDetail.Field6);
                //DT1.Rows.Add("Masterfile.FitSizeDetail", "0", "Field7", FitSizeDetail.Field7);
                //DT1.Rows.Add("Masterfile.FitSizeDetail", "0", "Field8", FitSizeDetail.Field8);
                //DT1.Rows.Add("Masterfile.FitSizeDetail", "0", "Field9", FitSizeDetail.Field9);

                DT1.Rows.Add("Masterfile.FitSizeDetail", "0", "SizeTemplateCode", FitSizeDetail.SizeTemplateCode);

                DT2.Rows.Add("Masterfile.Fit", "cond", "FitCode", FitCode);
                DT2.Rows.Add("Masterfile.Fit", "set", "IsWithDetail", "True");

                Gears.CreateData(DT1, Conn);
                Gears.UpdateData(DT2, Conn);



            }
            public void UpdateFitSizeDetail(FitSizeDetail FitSizeDetail)
            {



                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                DT1.Rows.Add("Masterfile.FitSizeDetail", "cond", "FitCode", FitSizeDetail.FitCode);
                DT1.Rows.Add("Masterfile.FitSizeDetail", "cond", "SizeCode", FitSizeDetail.SizeCode);
                      
                DT1.Rows.Add("Masterfile.FitSizeDetail", "set", "SizeName", FitSizeDetail.SizeName);
                DT1.Rows.Add("Masterfile.FitSizeDetail", "set", "Length", FitSizeDetail.Length);
                DT1.Rows.Add("Masterfile.FitSizeDetail", "set", "SortNumber", FitSizeDetail.SortNumber);



                //DT1.Rows.Add("Masterfile.FitSizeDetail", "set", "Field1", FitSizeDetail.Field1);
                //DT1.Rows.Add("Masterfile.FitSizeDetail", "set", "Field2", FitSizeDetail.Field2);
                //DT1.Rows.Add("Masterfile.FitSizeDetail", "set", "Field3", FitSizeDetail.Field3);
                //DT1.Rows.Add("Masterfile.FitSizeDetail", "set", "Field4", FitSizeDetail.Field4);
                //DT1.Rows.Add("Masterfile.FitSizeDetail", "set", "Field5", FitSizeDetail.Field5);
                //DT1.Rows.Add("Masterfile.FitSizeDetail", "set", "Field6", FitSizeDetail.Field6);
                //DT1.Rows.Add("Masterfile.FitSizeDetail", "set", "Field7", FitSizeDetail.Field7);
                //DT1.Rows.Add("Masterfile.FitSizeDetail", "set", "Field8", FitSizeDetail.Field8);
                //DT1.Rows.Add("Masterfile.FitSizeDetail", "set", "Field9", FitSizeDetail.Field9);

                //DT1.Rows.Add("Masterfile.FitSizeDetail", "set", "SizeTemplateCode", FitSizeDetail.SizeTemplateCode); 

                Gears.UpdateData(DT1, Conn);
            }
            public void DeleteFitSizeDetail(FitSizeDetail FitSizeDetail)
            {


                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
                
                //DT1.Rows.Add("Masterfile.FitSizeDetail", "cond", "FitCode", FitSizeDetail.FitCode);
                DT1.Rows.Add("Masterfile.FitSizeDetail", "cond", "FitCode", FitSizeDetail.FitCode);
                DT1.Rows.Add("Masterfile.FitSizeDetail", "cond", "SizeCode", FitSizeDetail.SizeCode);


                Gears.DeleteData(DT1, Conn);


                DataTable count = Gears.RetriveData2("select * from Masterfile.FitSizeDetail where FitCode = '" + fit + "'", Conn);

                if (count.Rows.Count < 1)
                {
                    DT2.Rows.Add("Masterfile.Fit", "cond", "FitCode", fit);
                    DT2.Rows.Add("Masterfile.Fit", "set", "IsWithDetail", "False");
                    Gears.UpdateData(DT2, Conn);
                }

            }
        }


        public class MeasurementChartTemplate
        {
            public virtual Fit Parent { get; set; }
            public virtual string FitCode { get; set; }
            public virtual string LineNumber { get; set; }
            public virtual string SizeCode { get; set; }
            public virtual string POMCode { get; set; }
            public virtual string Tolerance { get; set; }
            public virtual string Grade { get; set; }
            public virtual string Sorting { get; set; }
            public virtual string Value { get; set; }
            public virtual string Bracket { get; set; }
            public virtual bool IsMajor { get; set; }


            public DataTable getdetail(string docnumber, string Conn)
            {

                DataTable a;
                try
                {
                    a = Gears.RetriveData2("DECLARE @Sizes VARCHAR(MAX) "
                    + " DECLARE @query VARCHAR(MAX) "

                    + " SELECT @sizes = LEFT(Col1,DATALENGTH(Col1)-1)  FROM ( "
                    + " SELECT TOP 1 STUFF((SELECT DISTINCT '[' + CAST(CONVERT(varchar(10),SizeCode) + '],'  AS VARCHAR(MAX)) "
                    + " FROM Masterfile.MeasurementChartTemplate WHERE FitCode ='" + docnumber + "'"
                    + " FOR XML PATH(''), TYPE) .value('.','VARCHAR(MAX)'),1,0,'') AS Col1 "
                    + " FROM Masterfile.MeasurementChartTemplate  WHERE FitCode ='" + docnumber + "' ) AS Col "

                    + " SELECT @query = 'SELECT * FROM ( SELECT FitCode, A.POMCode AS [Code],B.Description AS PointofMeasurement, Tolerance,Bracket,A.IsMajor, Grade,Sorting AS [Order],Value, SizeCode  "
                    + " FROM Masterfile.MeasurementChartTemplate A LEFT JOIN Masterfile.POM B ON A.POMCode = B.POMCode WHERE FitCode =''" + docnumber + "''"
                    + " ) src  pivot( MAX(Value) for SizeCode in ('+@sizes+ ')) piv;' EXEC (@query)", Conn);
                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }

            public void AddMeasurementChartTemplate(MeasurementChartTemplate MeasurementChartTemplate)
            {
                int linenum = 0;

                DataTable count = Gears.RetriveData2("SELECT MAX(convert(int,LineNumber)) AS LineNumber from Masterfile.MeasurementChartTemplate where FitCode = '" + fit + "'", Conn);

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
                //Gears.CRUDdatatable DT3 = new Gears.CRUDdatatable();

                DT1.Rows.Add("Masterfile.MeasurementChartTemplate", "0", "FitCode", fit);
                DT1.Rows.Add("Masterfile.MeasurementChartTemplate", "0", "LineNumber", strLine);

                DT1.Rows.Add("Masterfile.MeasurementChartTemplate", "0", "SizeCode", MeasurementChartTemplate.SizeCode);
                DT1.Rows.Add("Masterfile.MeasurementChartTemplate", "0", "POMCode", MeasurementChartTemplate.POMCode);
                DT1.Rows.Add("Masterfile.MeasurementChartTemplate", "0", "Tolerance", MeasurementChartTemplate.Tolerance);
                DT1.Rows.Add("Masterfile.MeasurementChartTemplate", "0", "Grade", MeasurementChartTemplate.Grade);
                DT1.Rows.Add("Masterfile.MeasurementChartTemplate", "0", "Sorting", MeasurementChartTemplate.Sorting);
                DT1.Rows.Add("Masterfile.MeasurementChartTemplate", "0", "Value", MeasurementChartTemplate.Value);
                DT1.Rows.Add("Masterfile.MeasurementChartTemplate", "0", "Bracket", MeasurementChartTemplate.Bracket);
                DT1.Rows.Add("Masterfile.MeasurementChartTemplate", "0", "IsMajor", MeasurementChartTemplate.IsMajor);




                DT2.Rows.Add("Masterfile.Fit", "cond", "FitCode", FitCode);
                DT2.Rows.Add("Masterfile.Fit", "set", "IsWithDetail", "True");

                Gears.CreateData(DT1, Conn);
                Gears.UpdateData(DT2, Conn);



            }
            public void UpdateMeasurementChartTemplate(MeasurementChartTemplate MeasurementChartTemplate)
            {



                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                DT1.Rows.Add("Masterfile.MeasurementChartTemplate", "cond", "FitCode", MeasurementChartTemplate.FitCode);
                DT1.Rows.Add("Masterfile.MeasurementChartTemplate", "cond", "LineNumber", MeasurementChartTemplate.LineNumber);

                DT1.Rows.Add("Masterfile.MeasurementChartTemplate", "set", "SizeCode", MeasurementChartTemplate.SizeCode);
                DT1.Rows.Add("Masterfile.MeasurementChartTemplate", "set", "POMCode", MeasurementChartTemplate.POMCode);
                DT1.Rows.Add("Masterfile.MeasurementChartTemplate", "set", "Tolerance", MeasurementChartTemplate.Tolerance);
                DT1.Rows.Add("Masterfile.MeasurementChartTemplate", "set", "Grade", MeasurementChartTemplate.Grade);
                DT1.Rows.Add("Masterfile.MeasurementChartTemplate", "set", "Sorting", MeasurementChartTemplate.Sorting);
                DT1.Rows.Add("Masterfile.MeasurementChartTemplate", "set", "Value", MeasurementChartTemplate.Value);
                DT1.Rows.Add("Masterfile.MeasurementChartTemplate", "set", "Bracket", MeasurementChartTemplate.Bracket);
                DT1.Rows.Add("Masterfile.MeasurementChartTemplate", "set", "IsMajor", MeasurementChartTemplate.IsMajor);




                Gears.UpdateData(DT1, Conn);
            }
            public void DeleteMeasurementChartTemplate(MeasurementChartTemplate MeasurementChartTemplate)
            {


                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();

                DT1.Rows.Add("Masterfile.MeasurementChartTemplate", "cond", "FitCode", MeasurementChartTemplate.FitCode);
                DT1.Rows.Add("Masterfile.MeasurementChartTemplate", "cond", "LineNumber", MeasurementChartTemplate.LineNumber);


                Gears.DeleteData(DT1, Conn);


                DataTable count = Gears.RetriveData2("select * from Masterfile.MeasurementChartTemplate where FitCode = '" + fit + "'", Conn);

                if (count.Rows.Count < 1)
                {
                    DT2.Rows.Add("Masterfile.Fit", "cond", "FitCode", fit);
                    DT2.Rows.Add("Masterfile.Fit", "set", "IsWithDetail", "False");
                    Gears.UpdateData(DT2, Conn);
                }

            }
        }
        public DataTable getdata(string Fitcode, string Conn) //Ter
        {
            DataTable a;

            //if (Fitcode != null)
            //{
                a = Gears.RetriveData2("select * from Masterfile.Fit where FitCode = '" + Fitcode + "'", Conn); //Ter
                foreach (DataRow dtRow in a.Rows)
                {
                    FitCode = dtRow["FitCode"].ToString();
                    FitName = dtRow["FitName"].ToString();
                    Brand = dtRow["Brand"].ToString();
                    GenderCode = dtRow["GenderCode"].ToString();
                    ProductCategory = dtRow["ProductCategory"].ToString();
                    Remarks = dtRow["Remarks"].ToString();
                    Waist = dtRow["Waist"].ToString();
                    FitType = dtRow["FitType"].ToString();
                    Silhouette = dtRow["Silhouette"].ToString();
                    MasterPattern = dtRow["MasterPattern"].ToString();
                    SizeTemplate = dtRow["SizeTemplate"].ToString();
                    SizeCard = dtRow["SizeCard"].ToString();
                    StandardSize = dtRow["StandardSize"].ToString();
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
         //   }
           // else
          //  {
           //     a = Gears.RetriveData2("select '' as FunctionalGroupID,'' as Description,'' as AssignHead,'' as DateClosed,'' as Days", Conn); //Ter
          //  }
            return a;
        }
        public void InsertData(Fit _ent)
        {
            fit = _ent.FitCode;
            
            Conn = _ent.Connection; //Ter

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Masterfile.Fit", "0", "FitCode", _ent.FitCode);
            DT1.Rows.Add("Masterfile.Fit", "0", "FitName", _ent.FitName);
            DT1.Rows.Add("Masterfile.Fit", "0", "Brand", _ent.Brand);
            DT1.Rows.Add("Masterfile.Fit", "0", "GenderCode", _ent.GenderCode);
            DT1.Rows.Add("Masterfile.Fit", "0", "ProductCategory", _ent.ProductCategory);
            DT1.Rows.Add("Masterfile.Fit", "0", "Remarks", _ent.Remarks);
            DT1.Rows.Add("Masterfile.Fit", "0", "Waist", _ent.Waist);
            DT1.Rows.Add("Masterfile.Fit", "0", "FitType", _ent.FitType);
            DT1.Rows.Add("Masterfile.Fit", "0", "Silhouette", _ent.Silhouette);
            DT1.Rows.Add("Masterfile.Fit", "0", "MasterPattern", _ent.MasterPattern);
            DT1.Rows.Add("Masterfile.Fit", "0", "SizeTemplate", _ent.SizeTemplate);
            DT1.Rows.Add("Masterfile.Fit", "0", "SizeCard", _ent.SizeCard);
            DT1.Rows.Add("Masterfile.Fit", "0", "StandardSize", _ent.StandardSize);
            DT1.Rows.Add("Masterfile.Fit", "0", "IsInactive", _ent.IsInactive);
            DT1.Rows.Add("Masterfile.Fit", "0", "AddedBy", _ent.AddedBy);
            DT1.Rows.Add("Masterfile.Fit", "0", "AddedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            DT1.Rows.Add("Masterfile.Fit", "0", "Field1", _ent.Field1);
            DT1.Rows.Add("Masterfile.Fit", "0", "Field2", _ent.Field2);
            DT1.Rows.Add("Masterfile.Fit", "0", "Field3", _ent.Field3);
            DT1.Rows.Add("Masterfile.Fit", "0", "Field4", _ent.Field4);
            DT1.Rows.Add("Masterfile.Fit", "0", "Field5", _ent.Field5);
            DT1.Rows.Add("Masterfile.Fit", "0", "Field6", _ent.Field6);
            DT1.Rows.Add("Masterfile.Fit", "0", "Field7", _ent.Field7);
            DT1.Rows.Add("Masterfile.Fit", "0", "Field8", _ent.Field8);
            DT1.Rows.Add("Masterfile.Fit", "0", "Field9", _ent.Field9);

            Gears.CreateData(DT1, _ent.Connection); // TER
        }

        public void UpdateData(Fit _ent)
        {
            fit = _ent.FitCode;
            Conn = _ent.Connection; //Ter

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Masterfile.Fit", "cond", "FitCode", _ent.FitCode);
            DT1.Rows.Add("Masterfile.Fit", "set", "FitName", _ent.FitName);
            DT1.Rows.Add("Masterfile.Fit", "set", "Brand", _ent.Brand);
            DT1.Rows.Add("Masterfile.Fit", "set", "GenderCode", _ent.GenderCode);
            DT1.Rows.Add("Masterfile.Fit", "set", "ProductCategory", _ent.ProductCategory);
            DT1.Rows.Add("Masterfile.Fit", "set", "Remarks", _ent.Remarks);
            DT1.Rows.Add("Masterfile.Fit", "set", "Waist", _ent.Waist);
            DT1.Rows.Add("Masterfile.Fit", "set", "FitType", _ent.FitType);
            DT1.Rows.Add("Masterfile.Fit", "set", "Silhouette", _ent.Silhouette);
            DT1.Rows.Add("Masterfile.Fit", "set", "MasterPattern", _ent.MasterPattern);
            DT1.Rows.Add("Masterfile.Fit", "set", "SizeTemplate", _ent.SizeTemplate);
            DT1.Rows.Add("Masterfile.Fit", "set", "SizeCard", _ent.SizeCard);
            DT1.Rows.Add("Masterfile.Fit", "set", "StandardSize", _ent.StandardSize);
            DT1.Rows.Add("Masterfile.Fit", "set", "IsInactive", _ent.IsInactive);
            DT1.Rows.Add("Masterfile.Fit", "set", "LastEditedBy", _ent.AddedBy);
            DT1.Rows.Add("Masterfile.Fit", "set", "LastEditedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            DT1.Rows.Add("Masterfile.Fit", "set", "Field1", _ent.Field1);
            DT1.Rows.Add("Masterfile.Fit", "set", "Field2", _ent.Field2);
            DT1.Rows.Add("Masterfile.Fit", "set", "Field3", _ent.Field3);
            DT1.Rows.Add("Masterfile.Fit", "set", "Field4", _ent.Field4);
            DT1.Rows.Add("Masterfile.Fit", "set", "Field5", _ent.Field5);
            DT1.Rows.Add("Masterfile.Fit", "set", "Field6", _ent.Field6);
            DT1.Rows.Add("Masterfile.Fit", "set", "Field7", _ent.Field7);
            DT1.Rows.Add("Masterfile.Fit", "set", "Field8", _ent.Field8);
            DT1.Rows.Add("Masterfile.Fit", "set", "Field9", _ent.Field9);

            string strErr = Gears.UpdateData(DT1, _ent.Connection); // Ter
            Functions.AuditTrail("REFFIT", _ent.FitCode, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "UPDATE", _ent.Connection); // Ter

            strErr = Gears.UpdateData(DT1, _ent.Connection); // Ter
        }

        public void DeleteData(Fit _ent)
        {
            fit = _ent.FitCode;
            Conn = _ent.Connection; //Ter
            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Masterfile.Fit", "cond", "FitCode", _ent.FitCode);
            Gears.DeleteData(DT1, _ent.Connection); // Ter
            Functions.AuditTrail("REFFIT", fit, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "DELETE", _ent.Connection); // Ter
        }
    }
}
