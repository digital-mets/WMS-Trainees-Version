using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GearsLibrary;
using System.Data;
using System.Data.SqlClient;

namespace Entity
{
    public class ProductInfoSheet
    {
        private static string Docnum;

        private static string SFitCode;

        private static string Conn;//ADD CONN
        public virtual string Connection { get; set; }//ADD CONN
        public virtual string PISNumber { get; set; }
        public virtual string PISDescription { get; set; }
        public virtual string DocDate { get; set; }
        public virtual string CustomerCode { get; set; }
        public virtual string Brand { get; set; }
        public virtual string Gender { get; set; }
        public virtual string ProductCategory { get; set; }
        public virtual string ProductGroup { get; set; }
        public virtual string FOBSupplier { get; set; }
        public virtual string ProductSubCategory { get; set; }
        public virtual string DesignCategory { get; set; }
        public virtual string DesignSubCategory { get; set; }
        public virtual string ProductClass { get; set; }
        public virtual string ProductSubClass { get; set; }
        public virtual string Inspiration { get; set; }
        public virtual string DeliveryYear { get; set; }
        public virtual string DeliveryMonth { get; set; }
        public virtual string Theme { get; set; } //Collection
        public virtual string FrontImageFileName { get; set; }
        public virtual string FrontImageDateUploaded { get; set; }
        public virtual string BackImageFileName { get; set; }
        public virtual string BackImageDateUploaded { get; set; }
        public virtual string FrontImage { get; set; }      // 06-23-16 LGE FROM byte[] to string
        public virtual string BackImage { get; set; }       // 06-23-16 LGE FROM byte[] to string
        public virtual string FrontImage2D { get; set; }    // 06-23-16 LGE FROM byte[] to string
        public virtual string BackImage2D { get; set; }     // 06-23-16 LGE FROM byte[] to string
        public virtual string Designer { get; set; }
        public virtual string DISNo { get; set; }
        //public virtual byte Front { get; set; }

        // --- FABRIC DATA --- //
        public virtual string FabricCode { get; set; }
        public virtual string FabricSupplier { get; set; }
        public virtual string FabricColor { get; set; }
            // --- --- //
                public virtual string FabricGroup { get; set; }
                public virtual string FabricDesignCategory { get; set; }
                public virtual string Dyeing { get; set; }
                public virtual string WeaveType { get; set; }
                public virtual string CuttableWidth { get; set; }
                public virtual string GrossWidth { get; set; }
                public virtual string ForKnitsOnly { get; set; }
                public virtual string CuttableWeightBW { get; set; }
                public virtual string GrossWeightBW { get; set; }
                public virtual string Yield { get; set; }
                public virtual string FabricStretch { get; set; }
                public virtual string WarpConstruction { get; set; }
                public virtual string WeftConstruction { get; set; }
                public virtual string WarpDensity { get; set; }
                public virtual string WeftDensity { get; set; }
                public virtual string WarpShrinkage { get; set; }
                public virtual string WeftShrinkage { get; set; }
            // --- --- //
        // --- END OF FABRIC DATA --- //


        // --- FIT DATA --- //
        public virtual string ReferenceBizPartner { get; set; }
        public virtual string FitCode { get; set; }
        public virtual string BasePattern { get; set; }
        public virtual decimal ActualWarpShrinkage { get; set; }
        public virtual decimal ActualWeftShrinkage { get; set; }
        public virtual decimal CombinedShrinkage { get; set; }
            // --- --- //
                public virtual string Waist { get; set; }
                public virtual string Fit { get; set; }
                public virtual string Silhouette { get; set; }
                public virtual string MasterPattern { get; set; }
            // --- --- //
        // --- END OF FIT DATA --- //

        // --- WASH DATA --- //
        public virtual string WashSupplier { get; set; }
        public virtual string WashCode { get; set; }
        public virtual string TintColor { get; set; }
        public virtual string WashDescription { get; set; }
        // --- END OF WASH DATA --- //

        // --- EMBROIDER DATA --- //
        public virtual string EmbroiderySupplier { get; set; }
        // --- END OF EMBROIDER DATA --- //


        // --- PRINT DATA --- //
        public virtual string PrintSupplier { get; set; }
        // --- END OF PRINT DATA --- //

        // --- MEASUREMENT CHART DATA --- //
        public virtual string BaseSize { get; set; }
        public virtual string Remarks { get; set; }
        // --- END OF MEASUREMENT CHART DATA --- //


        // --- BOMandCOSTING --- //
        public virtual string StyleTemplateCode { get; set; }
        public virtual string StepTemplateCode { get; set; }
        public virtual decimal TotalItemCost { get; set; }
        public virtual decimal Markup { get; set; }
        public virtual decimal SellingPrice { get; set; }
        public virtual decimal AdditionalOverhead { get; set; }
        public virtual decimal SRP { get; set; }
        public virtual decimal ProfitFactor { get; set; }
        // --- END OF BOMandCOSTING DATA --- //
        public virtual bool IsWithDetail { get; set; }
        public virtual bool IsValidated { get; set; }


        public virtual string AddedBy { get; set; }
        public virtual string AddedDate { get; set; }
        public virtual string LastEditedBy { get; set; }
        public virtual string LastEditedDate { get; set; }
        public virtual string ApprovedBy { get; set; }
        public virtual string ApprovedDate { get; set; }
        public virtual string UnapprovedBy { get; set; }
        public virtual string UnapprovedDate { get; set; }
        public virtual string CancelledBy { get; set; }
        public virtual string CancelledDate { get; set; }
        public virtual string SyncedBy { get; set; }
        public virtual string SyncedDate { get; set; }
        //public virtual string SubmittedBy { get; set; }
        //public virtual string SubmittedDate { get; set; }
        //public virtual string CancelledBy { get; set; }
        //public virtual string CancelledDate { get; set; }
        //public virtual string PostedBy { get; set; }
        //public virtual string PostedDate { get; set; }
        
        
        public virtual string Field1 { get; set; }
        public virtual string Field2 { get; set; }
        public virtual string Field3 { get; set; }
        public virtual string Field4 { get; set; }
        public virtual string Field5 { get; set; }
        public virtual string Field6 { get; set; }
        public virtual string Field7 { get; set; }
        public virtual string Field8 { get; set; }
        public virtual string Field9 { get; set; }





        public virtual IList<PISStyleChart> Detail { get; set; }
        public class PISStyleChart
        {
            public virtual ProductInfoSheet Parent { get; set; }
            public virtual string PISNumber { get; set; }
            public virtual string LineNumber { get; set; }
            public virtual string POMCode { get; set; }
            public virtual string SizeCode { get; set; }
            public virtual string Value { get; set; }
            public virtual string Tolerance { get; set; }
            public virtual string Bracket { get; set; }
            public virtual string Grade { get; set; }
            public virtual string Sorting { get; set; }
            public virtual Boolean IsMajor { get; set; }

            public DataTable getdetail(string DocNumber, string Conn)
            {

                DataTable a;
                try
                {                   
                    //a = Gears.RetriveData2("DECLARE @Sizes VARCHAR(MAX) "
                    //+ " DECLARE @query VARCHAR(MAX) "

                    //+ " SELECT @sizes = SUBSTRING(Col1,0,LEN(Col1)-5) FROM ( "
                    //+ " SELECT TOP 1 STUFF((SELECT '[' + CAST(CONVERT(varchar(10),SizeCode) + '],'  AS VARCHAR(MAX)) "
                    //+ " FROM Masterfile.PISStyleChart "
                    //+ " ORDER BY SizeCode FOR XML PATH(''), TYPE) .value('.','VARCHAR(MAX)'),1,0,'') AS Col1 "
                    //+ " FROM Masterfile.PISStyleChart ) AS Col "

                    //+ " SELECT @query = 'SELECT * FROM ( SELECT PISNumber, A.POMCode AS [Code],B.Description AS PointofMeasurement, Tolerance,Bracket, Grade, Sorting AS [Order],Value, SizeCode "
                    //+ " FROM Masterfile.PISStyleChart A LEFT JOIN Masterfile.POM B ON A.POMCode = B.POMCode WHERE PISNumber =''" + DocNumber + "''"
                    //+ " ) src  pivot( MAX(Value) for SizeCode in ('+@sizes+ ')) piv;' EXEC (@query)", Conn);
                    //return a;

                    a = Gears.RetriveData2("DECLARE @Sizes VARCHAR(MAX)"
                                    + " DECLARE @query VARCHAR(MAX)"
                                    + " SELECT @sizes = LEFT(Col1,DATALENGTH(Col1)-1) FROM ("
                                    + " SELECT TOP 1 STUFF((SELECT DISTINCT '[' + CAST(CONVERT(varchar(10),RTRIM(LTRIM(SizeCode))) + '],'  AS VARCHAR(MAX))"
                                    + " FROM Masterfile.PISStyleChart WHERE PISNumber ='" + Docnum + "'"
                                    + " FOR XML PATH(''), TYPE) .value('.','VARCHAR(MAX)'),1,0,'') AS Col1"
                                    + " FROM Masterfile.PISStyleChart WHERE PISNumber ='" + Docnum + "' ) AS Col"
                                    + " SELECT @query = 'SELECT * FROM ( SELECT PISNumber, ISNULL(A.IsMajor,0) AS IsMajor, A.POMCode AS [Code],B.Description AS PointofMeasurement, Tolerance,Bracket, Grade, Sorting AS [Order],Value, RTRIM(LTRIM(SizeCode)) AS SizeCode"
                                    + " FROM Masterfile.PISStyleChart A LEFT JOIN Masterfile.POM B ON A.POMCode = B.POMCode WHERE PISNumber =''" + Docnum + "''"
                                    + " ) src  pivot( MAX(Value) for SizeCode in ('+@sizes+ ')) piv;' EXEC (@query) ", Conn);
                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }

            public void AddPISStyleChart(PISStyleChart PISStyleChart)
            {

                int linenum = 0;

                DataTable count = Gears.RetriveData2("SELECT MAX(convert(int,LineNumber)) AS LineNumber from Masterfile.PISStyleChart where PISNumber = '" + Docnum + "'", Conn);

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

                DT1.Rows.Add("Masterfile.PISStyleChart", "0", "PISNumber", Docnum);
                DT1.Rows.Add("Masterfile.PISStyleChart", "0", "LineNumber", strLine);


                    DT1.Rows.Add("Masterfile.PISStyleChart", "0", "IsMajor", PISStyleChart.IsMajor);
                    DT1.Rows.Add("Masterfile.PISStyleChart", "0", "POMCode", PISStyleChart.POMCode);
                    DT1.Rows.Add("Masterfile.PISStyleChart", "0", "SizeCode", PISStyleChart.SizeCode);
                    DT1.Rows.Add("Masterfile.PISStyleChart", "0", "Value", PISStyleChart.Value);
                    DT1.Rows.Add("Masterfile.PISStyleChart", "0", "Tolerance", PISStyleChart.Tolerance);
                    DT1.Rows.Add("Masterfile.PISStyleChart", "0", "Bracket", PISStyleChart.Bracket);
                    DT1.Rows.Add("Masterfile.PISStyleChart", "0", "Grade", PISStyleChart.Grade);
                    DT1.Rows.Add("Masterfile.PISStyleChart", "0", "Sorting",PISStyleChart.Sorting);
                //}

                DT2.Rows.Add("Production.ProductInfoSheet", "cond", "PISNumber", Docnum);
                DT2.Rows.Add("Production.ProductInfoSheet", "set", "IsWithDetail", "True");

                Gears.CreateData(DT1, Conn);
                Gears.UpdateData(DT2, Conn);

            }
            public void UpdatePISStyleChart(PISStyleChart PISStyleChart)
            {

                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                DT1.Rows.Add("Masterfile.PISStyleChart", "cond", "PISNumber", PISStyleChart.PISNumber);
                DT1.Rows.Add("Masterfile.PISStyleChart", "cond", "LineNumber", PISStyleChart.LineNumber);


                DT1.Rows.Add("Masterfile.PISStyleChart", "set", "POMCode", PISStyleChart.POMCode);
                DT1.Rows.Add("Masterfile.PISStyleChart", "set", "SizeCode", PISStyleChart.SizeCode);
                DT1.Rows.Add("Masterfile.PISStyleChart", "set", "Value", PISStyleChart.Value);
                DT1.Rows.Add("Masterfile.PISStyleChart", "set", "Tolerance", PISStyleChart.Tolerance);
                DT1.Rows.Add("Masterfile.PISStyleChart", "set", "Bracket", PISStyleChart.Bracket);
                DT1.Rows.Add("Masterfile.PISStyleChart", "set", "Grade", PISStyleChart.Grade);
                DT1.Rows.Add("Masterfile.PISStyleChart", "set", "Sorting", PISStyleChart.Sorting);


                Gears.UpdateData(DT1, Conn);
            }
            public void DeletePISStyleChart(PISStyleChart PISStyleChart)
            {


                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
                DT1.Rows.Add("Masterfile.PISStyleChart", "cond", "PISNumber", PISStyleChart.PISNumber);
                DT1.Rows.Add("Masterfile.PISStyleChart", "cond", "LineNumber", PISStyleChart.LineNumber);
                Gears.DeleteData(DT1, Conn);

                DataTable count = Gears.RetriveData2("select * from Masterfile.PISStyleChart where DocNumber = '" + Docnum + "'", Conn);

                if (count.Rows.Count < 1)
                {
                    DT2.Rows.Add("Masterfile.PISStyleChart", "cond", "PISNumber", Docnum);
                    DT2.Rows.Add("Masterfile.PISStyleChart", "set", "IsWithDetail", "False");
                    Gears.UpdateData(DT2, Conn);
                }

            }
        }


        public class PISThreadDetail
        {
            public virtual ProductInfoSheet Parent { get; set; }
            public virtual string PISNumber { get; set; }
            public virtual string LineNumber { get; set; }


            public virtual string Stitch { get; set; }
            public virtual string ThreadColor { get; set; }
            public virtual string Ticket { get; set; }
            public virtual string R { get; set; }
            public virtual string G { get; set; }
            public virtual string B { get; set; }
            public virtual string Location { get; set; }

            public DataTable getdetail(string DocNumber, string Conn)
            {
                DocNumber = string.IsNullOrEmpty(Docnum) ? DocNumber : Docnum;
                DataTable a;
                try
                {
                    a = Gears.RetriveData2("SELECT PISTD.*, ISNULL(C.R,'') AS R,ISNULL(C.G,'') AS G,ISNULL(C.B,'') AS B FROM MasterFile.PISThreadDetail PISTD LEFT JOIN Masterfile.Color C ON PISTD.ThreadColor = C.ColorCode WHERE PISNumber='" + DocNumber + "' order by LineNumber", Conn);
                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }
            public void AddPISThreadDetail(PISThreadDetail PISThreadDetail)
            {

                int linenum = 0;

                DataTable count = Gears.RetriveData2("select max(convert(int,LineNumber)) as LineNumber from Masterfile.PISThreadDetail where PISNumber = '" + Docnum + "'", Conn);

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

                DT1.Rows.Add("Masterfile.PISThreadDetail", "0", "PISNumber", Docnum);
                DT1.Rows.Add("Masterfile.PISThreadDetail", "0", "LineNumber", strLine);

                DT1.Rows.Add("Masterfile.PISThreadDetail", "0", "Stitch", PISThreadDetail.Stitch);
                DT1.Rows.Add("Masterfile.PISThreadDetail", "0", "ThreadColor", PISThreadDetail.ThreadColor);
                DT1.Rows.Add("Masterfile.PISThreadDetail", "0", "Ticket", PISThreadDetail.Ticket);
                DT1.Rows.Add("Masterfile.PISThreadDetail", "0", "Location", PISThreadDetail.Location);


                //DT1.Rows.Add("Masterfile.PISEmbroideryDetail", "0", "Field1", SOBillOfMaterial.Field1);
                //DT1.Rows.Add("Masterfile.PISEmbroideryDetail", "0", "Field2", SOBillOfMaterial.Field2);
                //DT1.Rows.Add("Masterfile.PISEmbroideryDetail", "0", "Field3", SOBillOfMaterial.Field3);
                //DT1.Rows.Add("Masterfile.PISEmbroideryDetail", "0", "Field4", SOBillOfMaterial.Field4);
                //DT1.Rows.Add("Masterfile.PISEmbroideryDetail", "0", "Field5", SOBillOfMaterial.Field5);
                //DT1.Rows.Add("Masterfile.PISEmbroideryDetail", "0", "Field6", SOBillOfMaterial.Field6);
                //DT1.Rows.Add("Masterfile.PISEmbroideryDetail", "0", "Field7", SOBillOfMaterial.Field7);
                //DT1.Rows.Add("Masterfile.PISEmbroideryDetail", "0", "Field8", SOBillOfMaterial.Field8);
                //DT1.Rows.Add("Masterfile.PISEmbroideryDetail", "0", "Field9", SOBillOfMaterial.Field9);

                DT2.Rows.Add("Production.ProductInfoSheet", "cond", "PISNumber", Docnum);
                DT2.Rows.Add("Production.ProductInfoSheet", "set", "IsWithDetail", "True");

                Gears.CreateData(DT1, Conn);
                Gears.UpdateData(DT2, Conn);

            }
            public void UpdatePISThreadDetail(PISThreadDetail PISThreadDetail)
            {
                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();


                DT1.Rows.Add("Masterfile.PISThreadDetail", "cond", "PISNumber", Docnum);
                DT1.Rows.Add("Masterfile.PISThreadDetail", "cond", "LineNumber", PISThreadDetail.LineNumber);

                DT1.Rows.Add("Masterfile.PISThreadDetail", "set", "Stitch", PISThreadDetail.Stitch);
                DT1.Rows.Add("Masterfile.PISThreadDetail", "set", "ThreadColor", PISThreadDetail.ThreadColor);
                DT1.Rows.Add("Masterfile.PISThreadDetail", "set", "Ticket", PISThreadDetail.Ticket);
                DT1.Rows.Add("Masterfile.PISThreadDetail", "set", "Location", PISThreadDetail.Location);

                //DT1.Rows.Add("Masterfile.PISEmbroideryDetail", "set", "Field1", PISEmbroideryDetail.Field1);
                //DT1.Rows.Add("Masterfile.PISEmbroideryDetail", "set", "Field2", PISEmbroideryDetail.Field2);
                //DT1.Rows.Add("Masterfile.PISEmbroideryDetail", "set", "Field3", PISEmbroideryDetail.Field3);
                //DT1.Rows.Add("Masterfile.PISEmbroideryDetail", "set", "Field4", PISEmbroideryDetail.Field4);
                //DT1.Rows.Add("Masterfile.PISEmbroideryDetail", "set", "Field5", PISEmbroideryDetail.Field5);
                //DT1.Rows.Add("Masterfile.PISEmbroideryDetail", "set", "Field6", PISEmbroideryDetail.Field6);
                //DT1.Rows.Add("Masterfile.PISEmbroideryDetail", "set", "Field7", PISEmbroideryDetail.Field7);
                //DT1.Rows.Add("Masterfile.PISEmbroideryDetail", "set", "Field8", PISEmbroideryDetail.Field8);
                //DT1.Rows.Add("Masterfile.PISEmbroideryDetail", "set", "Field9", PISEmbroideryDetail.Field9);

                Gears.UpdateData(DT1, Conn);
            }
            public void DeletePISThreadDetail(PISThreadDetail PISThreadDetail)
            {

                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
                DT1.Rows.Add("Masterfile.PISThreadDetail", "cond", "DocNumber", PISThreadDetail.PISNumber);
                DT1.Rows.Add("Masterfile.PISThreadDetail", "cond", "LineNumber", PISThreadDetail.LineNumber);


                Gears.DeleteData(DT1, Conn);


                DataTable count = Gears.RetriveData2("select * from Masterfile.PISThreadDetail where PISNumber = '" + Docnum + "'", Conn);

                if (count.Rows.Count < 1)
                {
                    DT2.Rows.Add("Production.ProductInfoSheet", "cond", "PISNumber", Docnum);
                    DT2.Rows.Add("Production.ProductInfoSheet", "set", "IsWithDetail", "False");
                    Gears.UpdateData(DT2, Conn);
                }
            }
        }

        public class PISEmbroideryDetail
        {
            public virtual ProductInfoSheet Parent { get; set; }
            public virtual string PISNumber { get; set; }
            public virtual string LineNumber { get; set; }


            public virtual string EmbroPart { get; set; }
            public virtual string EmbroDescription { get; set; }
            public virtual string EmbroCode { get; set; }
            public virtual decimal Height { get; set; }
            public virtual decimal Width { get; set; }
            public virtual string PictureEmbroider { get; set; }
            public virtual string UploadEmbroider { get; set; }
            //public virtual byte[] PictureEmbroiderByte { get; set; }
            //public virtual byte[] PictureEmbroiderByte { get; set; }


            //,B.Description AS EmbroDescription
            public DataTable getdetail(string DocNumber, string Conn)
            
            {
                DocNumber = string.IsNullOrEmpty(Docnum) ? DocNumber : Docnum;
                DataTable a;
                try
                {
                    a = Gears.RetriveData2("SELECT A.*,'' AS UploadEmbroider, B.Description AS EmbroDescription FROM Masterfile.PISEmbroideryDetail A LEFT JOIN Masterfile.Embroidery B ON A.EmbroCode = B.EmbroideryCode WHERE PISNumber='" + DocNumber + "' order by LineNumber", Conn);
                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }
            public void AddPISEmbroideryDetail(PISEmbroideryDetail PISEmbroideryDetail)
            {

                int linenum = 0;

                DataTable count = Gears.RetriveData2("select max(convert(int,LineNumber)) as LineNumber from Masterfile.PISEmbroideryDetail where PISNumber = '" + Docnum + "'", Conn);

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

                DT1.Rows.Add("Masterfile.PISEmbroideryDetail", "0", "PISNumber", Docnum);
                DT1.Rows.Add("Masterfile.PISEmbroideryDetail", "0", "LineNumber", strLine);

                DT1.Rows.Add("Masterfile.PISEmbroideryDetail", "0", "EmbroPart", PISEmbroideryDetail.EmbroPart);
                DT1.Rows.Add("Masterfile.PISEmbroideryDetail", "0", "EmbroCode", PISEmbroideryDetail.EmbroCode);
                DT1.Rows.Add("Masterfile.PISEmbroideryDetail", "0", "Height", PISEmbroideryDetail.Height);
                DT1.Rows.Add("Masterfile.PISEmbroideryDetail", "0", "Width", PISEmbroideryDetail.Width);
                DT1.Rows.Add("Masterfile.PISEmbroideryDetail", "0", "PictureEmbroider", PISEmbroideryDetail.PictureEmbroider);


                //DT1.Rows.Add("Masterfile.PISEmbroideryDetail", "0", "Field1", SOBillOfMaterial.Field1);
                //DT1.Rows.Add("Masterfile.PISEmbroideryDetail", "0", "Field2", SOBillOfMaterial.Field2);
                //DT1.Rows.Add("Masterfile.PISEmbroideryDetail", "0", "Field3", SOBillOfMaterial.Field3);
                //DT1.Rows.Add("Masterfile.PISEmbroideryDetail", "0", "Field4", SOBillOfMaterial.Field4);
                //DT1.Rows.Add("Masterfile.PISEmbroideryDetail", "0", "Field5", SOBillOfMaterial.Field5);
                //DT1.Rows.Add("Masterfile.PISEmbroideryDetail", "0", "Field6", SOBillOfMaterial.Field6);
                //DT1.Rows.Add("Masterfile.PISEmbroideryDetail", "0", "Field7", SOBillOfMaterial.Field7);
                //DT1.Rows.Add("Masterfile.PISEmbroideryDetail", "0", "Field8", SOBillOfMaterial.Field8);
                //DT1.Rows.Add("Masterfile.PISEmbroideryDetail", "0", "Field9", SOBillOfMaterial.Field9);

                DT2.Rows.Add("Production.ProductInfoSheet", "cond", "PISNumber", Docnum);
                DT2.Rows.Add("Production.ProductInfoSheet", "set", "IsWithDetail", "True");

                Gears.CreateData(DT1, Conn);
                Gears.UpdateData(DT2, Conn);


                //SqlConnection sqlConnection1 = new SqlConnection(Conn);
                //if (PISEmbroideryDetail.PictureEmbroider != null)
                //{
                //    sqlConnection1.Open();
                //    using (SqlCommand cmd = new SqlCommand("UPDATE Masterfile.PISEmbroideryDetail SET Picture=@Picture WHERE PISNumber = '" + Docnum + "' ", sqlConnection1))
                //    {
                //        cmd.Parameters.Add("@Picture", SqlDbType.VarBinary, -1).Value = PISEmbroideryDetail.PictureEmbroider;
                //        cmd.ExecuteNonQuery();
                //    }
                //    sqlConnection1.Close();
                //}

            }
            public void UpdatePISEmbroideryDetail(PISEmbroideryDetail PISEmbroideryDetail)
            {
                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();


                DT1.Rows.Add("Masterfile.PISEmbroideryDetail", "cond", "PISNumber", Docnum);
                DT1.Rows.Add("Masterfile.PISEmbroideryDetail", "cond", "LineNumber", PISEmbroideryDetail.LineNumber);

                DT1.Rows.Add("Masterfile.PISEmbroideryDetail", "set", "EmbroPart", PISEmbroideryDetail.EmbroPart);
                DT1.Rows.Add("Masterfile.PISEmbroideryDetail", "set", "EmbroCode", PISEmbroideryDetail.EmbroCode);
                DT1.Rows.Add("Masterfile.PISEmbroideryDetail", "set", "Height", PISEmbroideryDetail.Height);
                DT1.Rows.Add("Masterfile.PISEmbroideryDetail", "set", "Width", PISEmbroideryDetail.Width);
                DT1.Rows.Add("Masterfile.PISEmbroideryDetail", "set", "PictureEmbroider", PISEmbroideryDetail.PictureEmbroider);
                //DT1.Rows.Add("Masterfile.PISEmbroideryDetail", "set", "Picture", PISEmbroideryDetail.Picture);

                //DT1.Rows.Add("Masterfile.PISEmbroideryDetail", "set", "Field1", PISEmbroideryDetail.Field1);
                //DT1.Rows.Add("Masterfile.PISEmbroideryDetail", "set", "Field2", PISEmbroideryDetail.Field2);
                //DT1.Rows.Add("Masterfile.PISEmbroideryDetail", "set", "Field3", PISEmbroideryDetail.Field3);
                //DT1.Rows.Add("Masterfile.PISEmbroideryDetail", "set", "Field4", PISEmbroideryDetail.Field4);
                //DT1.Rows.Add("Masterfile.PISEmbroideryDetail", "set", "Field5", PISEmbroideryDetail.Field5);
                //DT1.Rows.Add("Masterfile.PISEmbroideryDetail", "set", "Field6", PISEmbroideryDetail.Field6);
                //DT1.Rows.Add("Masterfile.PISEmbroideryDetail", "set", "Field7", PISEmbroideryDetail.Field7);
                //DT1.Rows.Add("Masterfile.PISEmbroideryDetail", "set", "Field8", PISEmbroideryDetail.Field8);
                //DT1.Rows.Add("Masterfile.PISEmbroideryDetail", "set", "Field9", PISEmbroideryDetail.Field9);

                Gears.UpdateData(DT1, Conn);
            }
            public void DeletePISEmbroideryDetail(PISEmbroideryDetail PISEmbroideryDetail)
            {

                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT3 = new Gears.CRUDdatatable();
                DT1.Rows.Add("Masterfile.PISEmbroideryDetail", "cond", "DocNumber", PISEmbroideryDetail.PISNumber);
                DT1.Rows.Add("Masterfile.PISEmbroideryDetail", "cond", "LineNumber", PISEmbroideryDetail.LineNumber);


                Gears.DeleteData(DT1, Conn);


                DataTable count = Gears.RetriveData2("select * from Masterfile.PISEmbroideryDetail where PISNumber = '" + Docnum + "'", Conn);

                if (count.Rows.Count < 1)
                {
                    DT2.Rows.Add("Production.ProductInfoSheet", "cond", "PISNumber", Docnum);
                    DT2.Rows.Add("Production.ProductInfoSheet", "set", "IsWithDetail", "False");
                    Gears.UpdateData(DT2, Conn);
                }
            }
        }


        public class PISGradeBracket
        {
            public virtual ProductInfoSheet Parent { get; set; }
            public virtual string LineNumber { get; set; }
            public virtual string FitCode { get; set; }
            public virtual string POMCode { get; set; }
            public virtual string Size { get; set; }
            public virtual string Grade { get; set; }
            public virtual int Bracket { get; set; }
            public virtual string Tolerance { get; set; }

            public DataTable getdetailFitGradeBracket(string docnumber, string Conn)
            {

                DataTable a;
                try
                {
                    a = Gears.RetriveData2("select * from Production.PISGradeBracket where DocNumber ='" + docnumber + "'", Conn);
                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }

            public void AddFitGradeBracket(PISGradeBracket PISGradeBracket)
            {
                int linenum = 0;
                DataTable count = Gears.RetriveData2("SELECT MAX(CONVERT(int,LineNumber)) AS LineNumber FROM Production.PISGradeBracket WHERE DocNumber = '" + Docnum + "'", Conn);
                try { linenum = Convert.ToInt32(count.Rows[0][0].ToString()) + 1; }
                catch { linenum = 1; }

                string strLine = linenum.ToString().PadLeft(5, '0');
                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();

                DT1.Rows.Add("Production.PISGradeBracket", "0", "DocNumber", Docnum);
                DT1.Rows.Add("Production.PISGradeBracket", "0", "LineNumber", strLine);
                DT1.Rows.Add("Production.PISGradeBracket", "0", "FitCode", PISGradeBracket.FitCode);
                DT1.Rows.Add("Production.PISGradeBracket", "0", "POMCode", PISGradeBracket.POMCode);
                DT1.Rows.Add("Production.PISGradeBracket", "0", "Size", PISGradeBracket.Size);
                DT1.Rows.Add("Production.PISGradeBracket", "0", "Grade", PISGradeBracket.Grade);
                DT1.Rows.Add("Production.PISGradeBracket", "0", "Bracket", PISGradeBracket.Bracket);
                DT1.Rows.Add("Production.PISGradeBracket", "0", "Tolerance", PISGradeBracket.Tolerance);
                 
                DT2.Rows.Add("Production.ProductInfoSheet", "cond", "PISNumber", Docnum);
                DT2.Rows.Add("Production.ProductInfoSheet", "set", "IsWithDetail", "True");

                Gears.CreateData(DT1, Conn);
                Gears.UpdateData(DT2, Conn);
            }
            public void UpdateFitGradeBracket(PISGradeBracket PISGradeBracket)
            {
                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                DT1.Rows.Add("Production.PISGradeBracket", "cond", "DocNumber", Docnum);
                DT1.Rows.Add("Production.PISGradeBracket", "cond", "FitCode", PISGradeBracket.FitCode);
                DT1.Rows.Add("Production.PISGradeBracket", "cond", "LineNumber", PISGradeBracket.LineNumber);
                DT1.Rows.Add("Production.PISGradeBracket", "cond", "POMCode", PISGradeBracket.POMCode);
                DT1.Rows.Add("Production.PISGradeBracket", "set", "Size", PISGradeBracket.Size);
                DT1.Rows.Add("Production.PISGradeBracket", "set", "Grade", PISGradeBracket.Grade);
                DT1.Rows.Add("Production.PISGradeBracket", "set", "Bracket", PISGradeBracket.Bracket);
                DT1.Rows.Add("Production.PISGradeBracket", "set", "Tolerance", PISGradeBracket.Tolerance);

                Gears.UpdateData(DT1, Conn);
            }
            public void DeleteFitGradeBracket(PISGradeBracket PISGradeBracket)
            {
                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();

                DT1.Rows.Add("Production.PISGradeBracket", "cond", "DocNumber", Docnum);
                DT1.Rows.Add("Production.PISGradeBracket", "cond", "FitCode", PISGradeBracket.FitCode);
                DT1.Rows.Add("Production.PISGradeBracket", "cond", "POMCode", PISGradeBracket.POMCode);
                DT1.Rows.Add("Production.PISGradeBracket", "cond", "LineNumber", PISGradeBracket.LineNumber);

                Gears.DeleteData(DT1, Conn);

                DataTable count = Gears.RetriveData2("select * from Production.PISGradeBracket where DocNumber = '" + Docnum + "' and LineNumber = '" + PISGradeBracket.LineNumber + "'", Conn);

                if (count.Rows.Count < 1)
                {
                    DT2.Rows.Add("Production.ProductInfoSheet", "cond", "PISNumber", Docnum);
                    DT2.Rows.Add("Production.ProductInfoSheet", "set", "IsWithDetail", "False");
                    Gears.UpdateData(DT2, Conn);
                }
            }
        }


        public class PISPrintDetail
        {
            public virtual ProductInfoSheet Parent { get; set; }
            public virtual string PISNumber { get; set; }
            public virtual string LineNumber { get; set; }


            public virtual string PrintPart { get; set; }
            public virtual string PrintCode { get; set; }
            public virtual string PrintDescription { get; set; }
            public virtual string PrintInk { get; set; }
            public virtual string InkDescription { get; set; }
            public virtual string PicturePrint { get; set; }
            public virtual string UploadPrint { get; set; }


            //,B.Description AS EmbroDescription
            public DataTable getdetail(string DocNumber, string Conn)
            {
                DocNumber = string.IsNullOrEmpty(Docnum) ? DocNumber : Docnum;
                DataTable a;
                try
                {
                    a = Gears.RetriveData2("SELECT A.*,'' AS UploadPrint, B.Description AS PrintDescription,C.Description AS InkDescription FROM Masterfile.PISPrintDetail A LEFT JOIN Masterfile.PrintProcess B ON A.PrintCode = B.ProcessCode LEFT JOIN Masterfile.PrintInk C ON A.PrintInk = C.InkCode WHERE PISNumber='" + DocNumber + "' order by LineNumber", Conn);
                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }
            public void AddPISPrintDetail(PISPrintDetail PISPrintDetail)
            {

                int linenum = 0;

                DataTable count = Gears.RetriveData2("select max(convert(int,LineNumber)) as LineNumber from Masterfile.PISPrintDetail where PISNumber = '" + Docnum + "'", Conn);

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

                DT1.Rows.Add("Masterfile.PISPrintDetail", "0", "PISNumber", Docnum);
                DT1.Rows.Add("Masterfile.PISPrintDetail", "0", "LineNumber", strLine);

                DT1.Rows.Add("Masterfile.PISPrintDetail", "0", "PrintPart", PISPrintDetail.PrintPart);
                DT1.Rows.Add("Masterfile.PISPrintDetail", "0", "PrintCode", PISPrintDetail.PrintCode);
                DT1.Rows.Add("Masterfile.PISPrintDetail", "0", "PrintInk", PISPrintDetail.PrintInk);
                DT1.Rows.Add("Masterfile.PISPrintDetail", "0", "PicturePrint", PISPrintDetail.PicturePrint);
                //DT1.Rows.Add("Masterfile.PISEmbroideryDetail", "0", "Picture", PISEmbroideryDetail.Picture);


                //DT1.Rows.Add("Masterfile.PISEmbroideryDetail", "0", "Field1", SOBillOfMaterial.Field1);
                //DT1.Rows.Add("Masterfile.PISEmbroideryDetail", "0", "Field2", SOBillOfMaterial.Field2);
                //DT1.Rows.Add("Masterfile.PISEmbroideryDetail", "0", "Field3", SOBillOfMaterial.Field3);
                //DT1.Rows.Add("Masterfile.PISEmbroideryDetail", "0", "Field4", SOBillOfMaterial.Field4);
                //DT1.Rows.Add("Masterfile.PISEmbroideryDetail", "0", "Field5", SOBillOfMaterial.Field5);
                //DT1.Rows.Add("Masterfile.PISEmbroideryDetail", "0", "Field6", SOBillOfMaterial.Field6);
                //DT1.Rows.Add("Masterfile.PISEmbroideryDetail", "0", "Field7", SOBillOfMaterial.Field7);
                //DT1.Rows.Add("Masterfile.PISEmbroideryDetail", "0", "Field8", SOBillOfMaterial.Field8);
                //DT1.Rows.Add("Masterfile.PISEmbroideryDetail", "0", "Field9", SOBillOfMaterial.Field9);

                //DT2.Rows.Add("Production.ProductInfoSheet", "cond", "PISNumber", Docnum);
                //DT2.Rows.Add("Production.ProductInfoSheet", "set", "IsWithDetail", "True");

                Gears.CreateData(DT1, Conn);
                //Gears.UpdateData(DT2, Conn);

            }
            public void UpdatePISPrintDetail(PISPrintDetail PISPrintDetail)
            {
                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();


                DT1.Rows.Add("Masterfile.PISEmbroideryDetail", "cond", "PISNumber", Docnum);
                DT1.Rows.Add("Masterfile.PISEmbroideryDetail", "cond", "LineNumber", PISPrintDetail.LineNumber);

                DT1.Rows.Add("Masterfile.PISPrintDetail", "set", "PrintPart", PISPrintDetail.PrintPart);
                DT1.Rows.Add("Masterfile.PISPrintDetail", "set", "PrintCode", PISPrintDetail.PrintCode);
                DT1.Rows.Add("Masterfile.PISPrintDetail", "set", "PrintInk", PISPrintDetail.PrintInk);
                DT1.Rows.Add("Masterfile.PISPrintDetail", "set", "PicturePrint", PISPrintDetail.PicturePrint);
                //DT1.Rows.Add("Masterfile.PISEmbroideryDetail", "set", "Picture", PISEmbroideryDetail.Picture);

                //DT1.Rows.Add("Masterfile.PISEmbroideryDetail", "set", "Field1", PISEmbroideryDetail.Field1);
                //DT1.Rows.Add("Masterfile.PISEmbroideryDetail", "set", "Field2", PISEmbroideryDetail.Field2);
                //DT1.Rows.Add("Masterfile.PISEmbroideryDetail", "set", "Field3", PISEmbroideryDetail.Field3);
                //DT1.Rows.Add("Masterfile.PISEmbroideryDetail", "set", "Field4", PISEmbroideryDetail.Field4);
                //DT1.Rows.Add("Masterfile.PISEmbroideryDetail", "set", "Field5", PISEmbroideryDetail.Field5);
                //DT1.Rows.Add("Masterfile.PISEmbroideryDetail", "set", "Field6", PISEmbroideryDetail.Field6);
                //DT1.Rows.Add("Masterfile.PISEmbroideryDetail", "set", "Field7", PISEmbroideryDetail.Field7);
                //DT1.Rows.Add("Masterfile.PISEmbroideryDetail", "set", "Field8", PISEmbroideryDetail.Field8);
                //DT1.Rows.Add("Masterfile.PISEmbroideryDetail", "set", "Field9", PISEmbroideryDetail.Field9);

                Gears.UpdateData(DT1, Conn);
            }
            public void DeletePISPrintDetail(PISPrintDetail PISPrintDetail)
            {

                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT3 = new Gears.CRUDdatatable();
                DT1.Rows.Add("Masterfile.PISPrintDetail", "cond", "DocNumber", PISPrintDetail.PISNumber);
                DT1.Rows.Add("Masterfile.PISPrintDetail", "cond", "LineNumber", PISPrintDetail.LineNumber);


                Gears.DeleteData(DT1, Conn);


                DataTable count = Gears.RetriveData2("select * from Masterfile.PISPrintDetail where PISNumber = '" + Docnum + "'", Conn);

                if (count.Rows.Count < 1)
                {
                    DT2.Rows.Add("Production.PISPrintDetail", "cond", "PISNumber", Docnum);
                    DT2.Rows.Add("Production.PISPrintDetail", "set", "IsWithDetail", "False");
                    Gears.UpdateData(DT2, Conn);
                }
            }
        }



        public class PISStepTemplate
        {
            public virtual ProductInfoSheet Parent { get; set; }
            public virtual string PISNumber { get; set; }
            public virtual string LineNumber { get; set; }
            public virtual string Sequence { get; set; }
            public virtual string StepCodeSteps { get; set; }
            public virtual string SupplierSteps { get; set; }
            public virtual string WorkCenterName { get; set; }
            public virtual decimal EstimatedPrice { get; set; }

            public DataTable getdetail(string DocNumber, string Conn)
            {
                DocNumber = string.IsNullOrEmpty(Docnum) ? DocNumber : Docnum;
                DataTable a;
                try
                {
                    a = Gears.RetriveData2("SELECT PISST.LineNumber"
                                           + " , PISST.Sequence, PISST.StepCode AS StepCodeSteps, "
                                           + " PISST.Supplier AS SupplierSteps, BPSI.Name AS WorkCenterName, PISST.EstimatedPrice"
                                           + " from Masterfile.PISStepTemplate  PISST"
                                           + " LEFT JOIN Masterfile.BPSupplierInfo BPSI"
                                           + " ON PISST.Supplier = BPSI.SupplierCode"
                                           + " where PISNumber ='" + DocNumber + "' order by LineNumber", Conn);
                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }

            public void AddPISStepTemplate(PISStepTemplate PISStepTemplate)
            {

                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                //Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();

                DT1.Rows.Add("Masterfile.PISStepTemplate", "0", "PISNumber", Docnum);
                DT1.Rows.Add("Masterfile.PISStepTemplate", "0", "LineNumber", PISStepTemplate.LineNumber);


                DT1.Rows.Add("Masterfile.PISStepTemplate", "0", "Sequence", PISStepTemplate.Sequence);
                DT1.Rows.Add("Masterfile.PISStepTemplate", "0", "StepCode", PISStepTemplate.StepCodeSteps);
                DT1.Rows.Add("Masterfile.PISStepTemplate", "0", "Supplier", PISStepTemplate.SupplierSteps);
                DT1.Rows.Add("Masterfile.PISStepTemplate", "0", "EstimatedPrice", PISStepTemplate.EstimatedPrice);

                //DT2.Rows.Add("Production.ProductInfoSheet", "cond", "PISNumber", Docnum);
                //DT2.Rows.Add("Production.ProductInfoSheet", "set", "IsWithDetail", "True");

                Gears.CreateData(DT1, Conn);
                //Gears.UpdateData(DT2, Conn);

            }
            public void UpdatePISStepTemplate(PISStepTemplate PISStepTemplate)
            {

                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                DT1.Rows.Add("Masterfile.PISStepTemplate", "cond", "PISNumber", PISStepTemplate.PISNumber);
                DT1.Rows.Add("Masterfile.PISStepTemplate", "cond", "LineNumber", PISStepTemplate.LineNumber);


                DT1.Rows.Add("Masterfile.PISStepTemplate", "set", "Sequence", PISStepTemplate.Sequence);
                DT1.Rows.Add("Masterfile.PISStepTemplate", "set", "StepCode", PISStepTemplate.StepCodeSteps);
                DT1.Rows.Add("Masterfile.PISStepTemplate", "set", "Supplier", PISStepTemplate.SupplierSteps);
                DT1.Rows.Add("Masterfile.PISStepTemplate", "set", "EstimatedPrice", PISStepTemplate.EstimatedPrice);


                Gears.UpdateData(DT1, Conn);
            }
            public void DeletePISStepTemplate(PISStepTemplate PISStepTemplate)
            {

                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                //Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
                DT1.Rows.Add("Masterfile.PISStepTemplate", "cond", "PISNumber", Docnum);
                DT1.Rows.Add("Masterfile.PISStepTemplate", "cond", "LineNumber", PISStepTemplate.LineNumber);
                Gears.DeleteData(DT1, Conn);

                //Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                //Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
                //DT1.Rows.Add("Masterfile.PISStyleChart", "cond", "PISNumber", PISStyleChart.PISNumber);
                //DT1.Rows.Add("Masterfile.PISStyleChart", "cond", "LineNumber", PISStyleChart.LineNumber);
                //Gears.DeleteData(DT1, Conn);

                //DataTable count = Gears.RetriveData2("select * from Masterfile.PISStyleChart where DocNumber = '" + Docnum + "'", Conn);

                //if (count.Rows.Count < 1)
                //{
                //    DT2.Rows.Add("Masterfile.PISStyleChart", "cond", "PISNumber", Docnum);
                //    DT2.Rows.Add("Masterfile.PISStyleChart", "set", "IsWithDetail", "False");
                //    Gears.UpdateData(DT2, Conn);
                //}

            }
        }


        public class PISStyleTemplate
        {
            public virtual ProductInfoSheet Parent { get; set; }
            public virtual string PISNumber { get; set; }
            public virtual string LineNumber { get; set; }
            public virtual string StepCodeStyle { get; set; }
            public virtual string ItemCategoryCodeStyle { get; set; }
            public virtual string ItemCategoryDescription { get; set; }
            public virtual string ProductCategoryCodeStyle { get; set; }
            public virtual string ProductCategoryDescription { get; set; }
            public virtual string SupplierCodeStyle { get; set; }
            public virtual string ComponentStyle { get; set; }
            public virtual string ItemCodeStyle { get; set; }
            public virtual string ItemDescription { get; set; }
            public virtual string StockSize { get; set; }
            public virtual Boolean BySize { get; set; }
            public virtual string ColorCodeStyle { get; set; }
            public virtual string ClassCodeStyle { get; set; }
            public virtual string SizeCodeStyle { get; set; }
            public virtual decimal PerPieceConsumption { get; set; }
            public virtual string UnitStyle { get; set; }
            public virtual decimal EstimatedUnitCost { get; set; }
            public virtual decimal EstimatedCost { get; set; }
            public virtual string PictureBOM { get; set; }

            public DataTable getdetail(string DocNumber, string Conn)
            {
                DocNumber = string.IsNullOrEmpty(Docnum) ? DocNumber : Docnum;
                DataTable a;
                try
                {
                    a = Gears.RetriveData2("SELECT PISST.LineNumber"
                                                    +" , StepCode AS StepCodeStyle"
                                                    +" , PISST.ItemCategoryCode AS ItemCategoryCodeStyle"
	                                                +" , ISNULL(IC.Description,'') AS ItemCategoryDescription"
                                                    +" , PISST.ProductCategoryCode AS ProductCategoryCodeStyle"
	                                                +" , ISNULL(PC.Description,'') AS ProductCategoryDescription"
                                                    + " , PISST.SupplierCode AS SupplierCodeStyle"
                                                    +" , PISST.Components AS ComponentStyle"
                                                    +" , PISST.ItemCode AS ItemCodeStyle"
	                                                +" , ISNULL(I.FullDesc,'') AS ItemDescription"
	                                                +" , PISST.StockSize"
	                                                +" , PISST.BySize"
                                                    +" , PISST.ColorCode AS ColorCodeStyle"
                                                    +" , PISST.ClassCode AS ClassCodeStyle"
                                                    +" , PISST.SizeCode AS SizeCodeStyle"
	                                                +" , PISST.PerPieceConsumption"
                                                    +" , PISST.Unit AS UnitStyle"
	                                                +" , PISST.EstimatedUnitCost"
	                                                +" , PISST.EstimatedCost"
	                                                +" , '' AS PictureBOM"
	                                                +"  from Masterfile.PISStyleTemplate PISST"
	                                                +"  LEFT JOIN Masterfile.ItemCategory IC"
	                                                +"  ON PISST.ItemCategoryCode = IC.ItemCategoryCode"
	                                                +"  LEFT JOIN Masterfile.ProductCategory PC"
	                                                +"  ON PISST.ProductCategoryCode = PC.ProductCategoryCode"
	                                                +"  LEFT JOIN Masterfile.Item I"
                                                    + "  ON PISST.ItemCode = I.ItemCode where PISST.PISNumber ='" + DocNumber + "' order by PISST.LineNumber", Conn);
                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }

            public void AddPISStyleTemplate(PISStyleTemplate PISStyleTemplate)
            {
                int linenum = 0;
                //bool isbybulk = false;

                DataTable count = Gears.RetriveData2("select max(convert(int,LineNumber)) as LineNumber from Masterfile.PISStyleTemplate where PISNumber = '" + Docnum + "'", Conn);

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
                //Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();

                DT1.Rows.Add("Masterfile.PISStyleTemplate", "0", "PISNumber", Docnum);
                DT1.Rows.Add("Masterfile.PISStyleTemplate", "0", "LineNumber", strLine);

                
                DT1.Rows.Add("Masterfile.PISStyleTemplate", "0", "StepCode", PISStyleTemplate.StepCodeStyle);
                DT1.Rows.Add("Masterfile.PISStyleTemplate", "0", "ItemCategoryCode", PISStyleTemplate.ItemCategoryCodeStyle);
                DT1.Rows.Add("Masterfile.PISStyleTemplate", "0", "ProductCategoryCode", PISStyleTemplate.ProductCategoryCodeStyle);
                DT1.Rows.Add("Masterfile.PISStyleTemplate", "0", "SupplierCode", PISStyleTemplate.SupplierCodeStyle);
                DT1.Rows.Add("Masterfile.PISStyleTemplate", "0", "Components", PISStyleTemplate.ComponentStyle);
                DT1.Rows.Add("Masterfile.PISStyleTemplate", "0", "ItemCode", PISStyleTemplate.ItemCodeStyle);
                DT1.Rows.Add("Masterfile.PISStyleTemplate", "0", "ColorCode", PISStyleTemplate.ColorCodeStyle);
                DT1.Rows.Add("Masterfile.PISStyleTemplate", "0", "ClassCode", PISStyleTemplate.ClassCodeStyle);
                DT1.Rows.Add("Masterfile.PISStyleTemplate", "0", "SizeCode", PISStyleTemplate.SizeCodeStyle);
                DT1.Rows.Add("Masterfile.PISStyleTemplate", "0", "StockSize", PISStyleTemplate.StockSize);
                DT1.Rows.Add("Masterfile.PISStyleTemplate", "0", "BySize", PISStyleTemplate.BySize);
                DT1.Rows.Add("Masterfile.PISStyleTemplate", "0", "PerPieceConsumption", PISStyleTemplate.PerPieceConsumption);
                DT1.Rows.Add("Masterfile.PISStyleTemplate", "0", "Unit", PISStyleTemplate.UnitStyle);
                DT1.Rows.Add("Masterfile.PISStyleTemplate", "0", "EstimatedUnitCost", PISStyleTemplate.EstimatedUnitCost);
                DT1.Rows.Add("Masterfile.PISStyleTemplate", "0", "EstimatedCost", PISStyleTemplate.EstimatedCost);

                //DT2.Rows.Add("Production.ProductInfoSheet", "cond", "PISNumber", Docnum);
                //DT2.Rows.Add("Production.ProductInfoSheet", "set", "IsWithDetail", "True");

                Gears.CreateData(DT1, Conn);
                //Gears.UpdateData(DT2, Conn);

            }
            public void UpdatePISStyleTemplate(PISStyleTemplate PISStyleTemplate)
            {

                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                DT1.Rows.Add("Masterfile.PISStyleTemplate", "cond", "PISNumber", Docnum);
                DT1.Rows.Add("Masterfile.PISStyleTemplate", "cond", "LineNumber", PISStyleTemplate.LineNumber);


                DT1.Rows.Add("Masterfile.PISStyleTemplate", "set", "StepCode", PISStyleTemplate.StepCodeStyle);
                DT1.Rows.Add("Masterfile.PISStyleTemplate", "set", "ItemCategoryCode", PISStyleTemplate.ItemCategoryCodeStyle);
                DT1.Rows.Add("Masterfile.PISStyleTemplate", "set", "ProductCategoryCode", PISStyleTemplate.ProductCategoryCodeStyle);
                DT1.Rows.Add("Masterfile.PISStyleTemplate", "set", "SupplierCode", PISStyleTemplate.SupplierCodeStyle);
                DT1.Rows.Add("Masterfile.PISStyleTemplate", "set", "Components", PISStyleTemplate.ComponentStyle);
                DT1.Rows.Add("Masterfile.PISStyleTemplate", "set", "ItemCode", PISStyleTemplate.ItemCodeStyle);
                DT1.Rows.Add("Masterfile.PISStyleTemplate", "set", "ColorCode", PISStyleTemplate.ColorCodeStyle);
                DT1.Rows.Add("Masterfile.PISStyleTemplate", "set", "ClassCode", PISStyleTemplate.ClassCodeStyle);
                DT1.Rows.Add("Masterfile.PISStyleTemplate", "set", "SizeCode", PISStyleTemplate.SizeCodeStyle);
                DT1.Rows.Add("Masterfile.PISStyleTemplate", "set", "StockSize", PISStyleTemplate.StockSize);
                DT1.Rows.Add("Masterfile.PISStyleTemplate", "set", "BySize", PISStyleTemplate.BySize);
                DT1.Rows.Add("Masterfile.PISStyleTemplate", "set", "PerPieceConsumption", PISStyleTemplate.PerPieceConsumption);
                DT1.Rows.Add("Masterfile.PISStyleTemplate", "set", "Unit", PISStyleTemplate.UnitStyle);
                DT1.Rows.Add("Masterfile.PISStyleTemplate", "set", "EstimatedUnitCost", PISStyleTemplate.EstimatedUnitCost);
                DT1.Rows.Add("Masterfile.PISStyleTemplate", "set", "EstimatedCost", PISStyleTemplate.EstimatedCost);


                Gears.UpdateData(DT1, Conn);
            }
            public void DeletePISStyleTemplate(PISStyleTemplate PISStyleTemplate)
            {
                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                //Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
                DT1.Rows.Add("Masterfile.PISStyleTemplate", "cond", "PISNumber", Docnum);
                DT1.Rows.Add("Masterfile.PISStyleTemplate", "cond", "LineNumber", PISStyleTemplate.LineNumber);
                Gears.DeleteData(DT1, Conn);

                //DataTable count = Gears.RetriveData2("select * from Masterfile.PISStyleChart where DocNumber = '" + Docnum + "'", Conn);

                //if (count.Rows.Count < 1)
                //{
                //    DT2.Rows.Add("Masterfile.PISStyleChart", "cond", "PISNumber", Docnum);
                //    DT2.Rows.Add("Masterfile.PISStyleChart", "set", "IsWithDetail", "False");
                //    Gears.UpdateData(DT2, Conn);
                //}

            }
        }



        public class PISOtherPictureDetail
        {
            public virtual ProductInfoSheet Parent { get; set; }
            public virtual string PISNumber { get; set; }
            public virtual string LineNumber { get; set; }


            public virtual string ImageFileName { get; set; }
            public virtual string OtherPicture { get; set; }
            public virtual string OtherPictureUpload { get; set; }
            public virtual Boolean IsBlowUp { get; set; }


            //,B.Description AS EmbroDescription
            public DataTable getdetail(string DocNumber, string Conn)
            {
                DocNumber = string.IsNullOrEmpty(Docnum) ? DocNumber : Docnum;
                DataTable a;
                try
                {
                    a = Gears.RetriveData2("SELECT *,'' AS OtherPictureUpload FROM Masterfile.PISImage WHERE PISNumber='" + DocNumber + "' order by LineNumber", Conn);
                    return a;
                }
                catch (Exception e)
                {
                    a = null;
                    return a;
                }
            }
            public void AddPISOtherPictureDetail(PISOtherPictureDetail PISOtherPictureDetail)
            {

                int linenum = 0;

                DataTable count = Gears.RetriveData2("select max(convert(int,LineNumber)) as LineNumber from Masterfile.PISImage where PISNumber = '" + Docnum + "'", Conn);

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

                DT1.Rows.Add("Masterfile.PISImage", "0", "PISNumber", Docnum);
                DT1.Rows.Add("Masterfile.PISImage", "0", "LineNumber", strLine);

                DT1.Rows.Add("Masterfile.PISImage", "0", "ImageFileName", PISOtherPictureDetail.ImageFileName);
                DT1.Rows.Add("Masterfile.PISImage", "0", "OtherPicture", PISOtherPictureDetail.OtherPicture);
                DT1.Rows.Add("Masterfile.PISImage", "0", "IsBlowUp", PISOtherPictureDetail.IsBlowUp);


                //DT1.Rows.Add("Masterfile.PISEmbroideryDetail", "0", "Field1", SOBillOfMaterial.Field1);
                //DT1.Rows.Add("Masterfile.PISEmbroideryDetail", "0", "Field2", SOBillOfMaterial.Field2);
                //DT1.Rows.Add("Masterfile.PISEmbroideryDetail", "0", "Field3", SOBillOfMaterial.Field3);
                //DT1.Rows.Add("Masterfile.PISEmbroideryDetail", "0", "Field4", SOBillOfMaterial.Field4);
                //DT1.Rows.Add("Masterfile.PISEmbroideryDetail", "0", "Field5", SOBillOfMaterial.Field5);
                //DT1.Rows.Add("Masterfile.PISEmbroideryDetail", "0", "Field6", SOBillOfMaterial.Field6);
                //DT1.Rows.Add("Masterfile.PISEmbroideryDetail", "0", "Field7", SOBillOfMaterial.Field7);
                //DT1.Rows.Add("Masterfile.PISEmbroideryDetail", "0", "Field8", SOBillOfMaterial.Field8);
                //DT1.Rows.Add("Masterfile.PISEmbroideryDetail", "0", "Field9", SOBillOfMaterial.Field9);

                DT2.Rows.Add("Production.ProductInfoSheet", "cond", "PISNumber", Docnum);
                DT2.Rows.Add("Production.ProductInfoSheet", "set", "IsWithDetail", "True");

                Gears.CreateData(DT1, Conn);
                Gears.UpdateData(DT2, Conn);


                //SqlConnection sqlConnection1 = new SqlConnection(Conn);
                //if (PISEmbroideryDetail.PictureEmbroider != null)
                //{
                //    sqlConnection1.Open();
                //    using (SqlCommand cmd = new SqlCommand("UPDATE Masterfile.PISEmbroideryDetail SET Picture=@Picture WHERE PISNumber = '" + Docnum + "' ", sqlConnection1))
                //    {
                //        cmd.Parameters.Add("@Picture", SqlDbType.VarBinary, -1).Value = PISEmbroideryDetail.PictureEmbroider;
                //        cmd.ExecuteNonQuery();
                //    }
                //    sqlConnection1.Close();
                //}

            }
            public void UpdatePISOtherPictureDetail(PISOtherPictureDetail PISOtherPictureDetail)
            {
                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();


                DT1.Rows.Add("Masterfile.PISImage", "cond", "PISNumber", Docnum);
                DT1.Rows.Add("Masterfile.PISImage", "cond", "LineNumber", PISOtherPictureDetail.LineNumber);


                DT1.Rows.Add("Masterfile.PISImage", "set", "ImageFileName", PISOtherPictureDetail.ImageFileName);
                DT1.Rows.Add("Masterfile.PISImage", "set", "OtherPicture", PISOtherPictureDetail.OtherPicture);
                DT1.Rows.Add("Masterfile.PISImage", "set", "IsBlowUp", PISOtherPictureDetail.IsBlowUp);
                //DT1.Rows.Add("Masterfile.PISEmbroideryDetail", "set", "Picture", PISEmbroideryDetail.Picture);

                //DT1.Rows.Add("Masterfile.PISEmbroideryDetail", "set", "Field1", PISEmbroideryDetail.Field1);
                //DT1.Rows.Add("Masterfile.PISEmbroideryDetail", "set", "Field2", PISEmbroideryDetail.Field2);
                //DT1.Rows.Add("Masterfile.PISEmbroideryDetail", "set", "Field3", PISEmbroideryDetail.Field3);
                //DT1.Rows.Add("Masterfile.PISEmbroideryDetail", "set", "Field4", PISEmbroideryDetail.Field4);
                //DT1.Rows.Add("Masterfile.PISEmbroideryDetail", "set", "Field5", PISEmbroideryDetail.Field5);
                //DT1.Rows.Add("Masterfile.PISEmbroideryDetail", "set", "Field6", PISEmbroideryDetail.Field6);
                //DT1.Rows.Add("Masterfile.PISEmbroideryDetail", "set", "Field7", PISEmbroideryDetail.Field7);
                //DT1.Rows.Add("Masterfile.PISEmbroideryDetail", "set", "Field8", PISEmbroideryDetail.Field8);
                //DT1.Rows.Add("Masterfile.PISEmbroideryDetail", "set", "Field9", PISEmbroideryDetail.Field9);

                Gears.UpdateData(DT1, Conn);
            }
            public void DeletePISOtherPictureDetail(PISOtherPictureDetail PISOtherPictureDetail)
            {

                Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
                Gears.CRUDdatatable DT3 = new Gears.CRUDdatatable();
                DT1.Rows.Add("Masterfile.PISImage", "cond", "DocNumber", PISOtherPictureDetail.PISNumber);
                DT1.Rows.Add("Masterfile.PISImage", "cond", "LineNumber", PISOtherPictureDetail.LineNumber);


                Gears.DeleteData(DT1, Conn);


                DataTable count = Gears.RetriveData2("select * from Masterfile.PISImage where PISNumber = '" + Docnum + "'", Conn);

                if (count.Rows.Count < 1)
                {
                    DT2.Rows.Add("Production.ProductInfoSheet", "cond", "PISNumber", Docnum);
                    DT2.Rows.Add("Production.ProductInfoSheet", "set", "IsWithDetail", "False");
                    Gears.UpdateData(DT2, Conn);
                }
            }
        }
        //public class JournalEntry
        //{
        //    public virtual MaterialPlanning Parent { get; set; }
        //    public virtual string AccountCode { get; set; }
        //    public virtual string AccountDescription { get; set; }
        //    public virtual string SubsidiaryCode { get; set; }
        //    public virtual string SubsidiaryDescription { get; set; }
        //    public virtual string ProfitCenter { get; set; }
        //    public virtual string CostCenter { get; set; }
        //    public virtual string Debit { get; set; }
        //    public virtual string Credit { get; set; }
        //    public DataTable getJournalEntry(string DocNumber, string Conn)
        //    {

        //        DataTable a;
        //        try
        //        {
        //            a = Gears.RetriveData2("SELECT A.AccountCode, B.Description AS AccountDescription, A.SubsiCode AS SubsidiaryCode, C.Description AS SubsidiaryDescription, "
        //            + " ProfitCenterCode AS ProfitCenter, CostCenterCode AS CostCenter, DebitAmount AS Debit, CreditAmount AS Credit  FROM Accounting.GeneralLedger A "
        //            + " INNER JOIN Accounting.ChartOfAccount B ON A.AccountCode = B.AccountCode "
        //            + " INNER JOIN Accounting.GLSubsiCode C ON A.SubsiCode = C.SubsiCode "
        //            + " AND A.AccountCode = C.AccountCode WHERE A.DocNumber = '" + DocNumber + "' AND TransType ='ACTADI' ", Conn);

        //            return a;
        //        }
        //        catch (Exception e)
        //        {
        //            a = null;
        //            return a;
        //        }
        //    }
        //}


        //public class RefTransaction
        //{
        //    public virtual AssetDisposal Parent { get; set; }
        //    public virtual string RTransType { get; set; }
        //    public virtual string REFDocNumber { get; set; }
        //    public virtual string RMenuID { get; set; }
        //    public virtual string TransType { get; set; }
        //    public virtual string DocNumber { get; set; }
        //    public virtual string MenuID { get; set; }
        //    public virtual string CommandString { get; set; }
        //    public virtual string RCommandString { get; set; }
        //    public DataTable getreftransaction(string DocNumber, string Conn)
        //    {

        //        DataTable a;
        //        try
        //        {
        //            a = Gears.RetriveData2("SELECT RTransType,REFDocNumber,RMenuID,RIGHT(B.CommandString, LEN(B.CommandString) - 1) as RCommandString,A.TransType,DocNumber,A.MenuID,RIGHT(C.CommandString, LEN(C.CommandString) - 1) as CommandString from  IT.ReferenceTrans  A "
        //                                    + " INNER JOIN IT.MainMenu B"
        //                                    + " ON A.RMenuID =B.ModuleID "
        //                                    + " INNER JOIN IT.MainMenu C "
        //                                    + " ON A.MenuID = C.ModuleID "
        //                                    + "  where (DocNumber='" + DocNumber + "' OR REFDocNumber='" + DocNumber + "')  AND  (RTransType='ACTADI' OR  A.TransType='ACTADI') ", Conn);
        //            return a;
        //        }
        //        catch (Exception e)
        //        {
        //            a = null;
        //            return a;
        //        }
        //    }
        //}

        public DataTable getdata(string DocNumber, string Conn)
        {
            DataTable a;

            //if (DocNumber != null)
            //{
            a = Gears.RetriveData2("SELECT PIS.PISNumber, PIS.PISDescription, PIS.DocDate, PIS.CustomerCode, PIS.Brand, PIS.Gender"
	                                + " ,PIS.ProductCategory, PIS.ProductGroup, PIS.FOBSupplier, PIS.ProductSubCategory, PIS.DesignCategory"
	                                + " ,PIS.DesignSubCategory, PIS.ProductClass, PIS.ProductSubClass, PIS.Inspiration"
                                    + " ,PIS.FabricCode, PIS.FabricSupplier, PIS.FabricColor"
	                                + " ,PIS.AddedBy, PIS.AddedDate, PIS.LastEditedBy, PIS.LastEditedDate"
                                    + " ,PIS.ApprovedBy, PIS.ApprovedDate, PIS.UnapprovedBy, PIS.UnapprovedDate"
                                    + " ,PIS.CancelledBy, PIS.CancelledDate, PIS.SyncedBy, PIS.SyncedDate"
                                    + " ,PIS.Field1, PIS.Field2, PIS.Field3, PIS.Field4, PIS.Field5, PIS.Field6, PIS.Field7, PIS.Field8, PIS.Field9"
                                    + " ,PIS.IsWithDetail, PIS.IsValidated"
                                    + " ,PIS.FrontImage, PIS.BackImage, PIS.FrontImage2D, PIS.BackImage2D"
                                    + " ,FAB.FabricGroup, FAB.FabricDesignCategory, FAB.Dyeing, FAB.WeaveType"
	                                + " ,FAB.CuttableWidth, FAB.GrossWidth, FAB.ForKnitsOnly"
                                    + " ,FAB.Weight, FAB.WeightUnit, FAB.Yield"
	                                + " ,FAB.FabricStretch"
	                                + " ,FAB.WarpConstruction, FAB.WeftConstruction"
	                                + " ,FAB.WarpDensity, FAB.WeftDensity"
	                                + " ,FAB.WarpShrinkage, FAB.WeftShrinkage"

                                    + " ,PIS.ReferenceBizPartner, PIS.FitCode, PIS.BasePattern"
                                    + " ,PIS.WarpShrinkage AS ActualWarpShrinkage, PIS.WeftShrinkage AS ActualWeftShrinkage, PIS.CombinedShrinkage"
                                    + " ,FIT.Waist, FIT.FitType, FIT.Silhouette, FIT.MasterPattern"
                                    + " ,PIS.WashSupplier, PIS.WashCode, PIS.TintColor, PIS.WashDescription"
                                    + " ,PIS.EmbroiderySupplier"
                                    + " ,PIS.PrintSupplier"
                                    + " ,PIS.BaseSize, PIS.Remarks"
                                    + " ,PIS.StyleTemplateCode, PIS.StepTemplateCode"
                                    + " ,PIS.TotalItemCost, PIS.Markup, PIS.SellingPrice"
                                    + " ,PIS.AdditionalOverhead, PIS.SRP, PIS.ProfitFactor"
                                    + " ,PIS.DeliveryYear, PIS.DeliveryMonth, PIS.Theme, PIS.Designer, PIS.DISNo"
	                                + " FROM Production.ProductInfoSheet PIS LEFT JOIN"
                                    + " Masterfile.Fabric FAB"
                                    + " ON PIS.FabricCode = FAB.FabricCode"
                                    + " LEFT JOIN Masterfile.Fit FIT"
                                    + " ON PIS.FitCode = FIT.FitCode WHERE PIS.PISNumber = '" + DocNumber + "'", Conn);
            foreach (DataRow dtRow in a.Rows)
            {
                Docnum = dtRow["PISNumber"].ToString();
                PISNumber = dtRow["PISNumber"].ToString();
                PISDescription = dtRow["PISDescription"].ToString();
                DocDate = dtRow["DocDate"].ToString();
                CustomerCode = dtRow["CustomerCode"].ToString();
                Brand = dtRow["Brand"].ToString();
                Gender = dtRow["Gender"].ToString();
                ProductCategory = dtRow["ProductCategory"].ToString();
                ProductGroup = dtRow["ProductGroup"].ToString();
                FOBSupplier = dtRow["FOBSupplier"].ToString();
                ProductSubCategory = dtRow["ProductSubCategory"].ToString();
                DesignCategory = dtRow["DesignCategory"].ToString();
                DesignSubCategory = dtRow["DesignSubCategory"].ToString();
                ProductClass = dtRow["ProductClass"].ToString();
                ProductSubClass = dtRow["ProductSubClass"].ToString();
                Inspiration = dtRow["Inspiration"].ToString();
                DeliveryYear = dtRow["DeliveryYear"].ToString();
                DeliveryMonth = dtRow["DeliveryMonth"].ToString();
                Theme = dtRow["Theme"].ToString();
                Designer = dtRow["Designer"].ToString();
                DISNo = dtRow["DISNo"].ToString();

                // -- IMAGES -- //
                FrontImage = dtRow["FrontImage"].ToString();
                BackImage = dtRow["BackImage"].ToString();
                FrontImage2D = dtRow["FrontImage2D"].ToString();
                BackImage2D = dtRow["BackImage2D"].ToString();
                // -- END IMAGES -- //



                // --- FABRIC DATA --- //
                FabricCode = dtRow["FabricCode"].ToString();
                FabricSupplier = dtRow["FabricSupplier"].ToString();
                FabricColor = dtRow["FabricColor"].ToString();
                    // --- --- //
                        FabricGroup = dtRow["FabricGroup"].ToString();
                        FabricDesignCategory = dtRow["FabricDesignCategory"].ToString();
                        Dyeing = dtRow["Dyeing"].ToString();
                        WeaveType = dtRow["WeaveType"].ToString();
                        CuttableWidth = dtRow["CuttableWidth"].ToString();
                        GrossWidth = dtRow["GrossWidth"].ToString();
                        ForKnitsOnly = dtRow["ForKnitsOnly"].ToString();
                        CuttableWeightBW = dtRow["Weight"].ToString();
                        GrossWeightBW = dtRow["WeightUnit"].ToString();
                        Yield = dtRow["Yield"].ToString();
                        FabricStretch = dtRow["FabricStretch"].ToString();
                        WarpConstruction = dtRow["WarpConstruction"].ToString();
                        WeftConstruction = dtRow["WeftConstruction"].ToString();
                        WarpDensity = dtRow["WarpDensity"].ToString();
                        WeftDensity = dtRow["WeftDensity"].ToString();
                        WarpShrinkage = dtRow["WarpShrinkage"].ToString();
                        WeftShrinkage = dtRow["WeftShrinkage"].ToString();
                    // --- --- //
                // --- END OF FABRIC DATA --- //
                
                // --- FIT DATA --- //
                ReferenceBizPartner = dtRow["ReferenceBizPartner"].ToString();
                FitCode = dtRow["FitCode"].ToString();
                BasePattern = dtRow["BasePattern"].ToString();
                ActualWarpShrinkage = Convert.ToDecimal(Convert.IsDBNull(dtRow["ActualWarpShrinkage"]) ? 0 : dtRow["ActualWarpShrinkage"]);
                ActualWeftShrinkage = Convert.ToDecimal(Convert.IsDBNull(dtRow["ActualWeftShrinkage"]) ? 0 : dtRow["ActualWeftShrinkage"]);
                CombinedShrinkage = Convert.ToDecimal(Convert.IsDBNull(dtRow["CombinedShrinkage"]) ? 0 : dtRow["CombinedShrinkage"]);
                    // --- --- //
                        Waist = dtRow["Waist"].ToString();
                        Fit = dtRow["FitType"].ToString();
                        Silhouette = dtRow["Silhouette"].ToString();
                        MasterPattern = dtRow["MasterPattern"].ToString();
                    // --- --- //
                // --- END OF FIT DATA --- //

                // --- WASH DATA --- //
                    WashSupplier = dtRow["WashSupplier"].ToString();
                    WashCode = dtRow["WashCode"].ToString();
                    TintColor = dtRow["TintColor"].ToString();
                    WashDescription = dtRow["WashDescription"].ToString();
                // --- END OF WASH DATA --- //

                // --- EMBROIDER DATA --- //
                    EmbroiderySupplier = dtRow["EmbroiderySupplier"].ToString();
                // --- END OF EMBROIDER DATA --- //

                // --- PRINT DATA --- //
                    PrintSupplier = dtRow["PrintSupplier"].ToString();
                // --- END OF PRINT DATA --- //

                // --- MEASUREMENT CHART DATA --- //
                    BaseSize = dtRow["BaseSize"].ToString();
                    Remarks = dtRow["Remarks"].ToString();
                // --- END OF MEASUREMENT CHART DATA --- //

                // --- BOM&COSTING DATA --- //
                StyleTemplateCode = dtRow["StyleTemplateCode"].ToString();
                StepTemplateCode = dtRow["StepTemplateCode"].ToString();
                TotalItemCost = Convert.ToDecimal(Convert.IsDBNull(dtRow["TotalItemCost"]) ? 0 : dtRow["TotalItemCost"]);
                Markup = Convert.ToDecimal(Convert.IsDBNull(dtRow["Markup"]) ? 0 : dtRow["Markup"]);
                SellingPrice = Convert.ToDecimal(Convert.IsDBNull(dtRow["SellingPrice"]) ? 0 : dtRow["SellingPrice"]);
                AdditionalOverhead = Convert.ToDecimal(Convert.IsDBNull(dtRow["AdditionalOverhead"]) ? 0 : dtRow["AdditionalOverhead"]);
                SRP = Convert.ToDecimal(Convert.IsDBNull(dtRow["SRP"]) ? 0 : dtRow["SRP"]);
                ProfitFactor = Convert.ToDecimal(Convert.IsDBNull(dtRow["ProfitFactor"]) ? 0 : dtRow["ProfitFactor"]);
                // --- END OF BOM DATA --- //



                //DueDateTo = Convert.ToDecimal(Convert.IsDBNull(dtRow["TotalAmountSold"]) ? 0 : dtRow["TotalAmountSold"]);
                //GrossVATAmount = Convert.ToDecimal(Convert.IsDBNull(dtRow["GrossVATAmount"]) ? 0 : dtRow["GrossVATAmount"]);
                //GrossNonVATAmount = Convert.ToDecimal(Convert.IsDBNull(dtRow["GrossNonVATAmount"]) ? 0 : dtRow["GrossNonVATAmount"]);
                //Remarks = dtRow["Remarks"].ToString();
                //TotalAssetCost = Convert.ToDecimal(Convert.IsDBNull(dtRow["TotalAssetCost"]) ? 0 : dtRow["TotalAssetCost"]);
                //TotalAccumulatedDepreciation = Convert.ToDecimal(Convert.IsDBNull(dtRow["TotalAccumulatedDepreciation"]) ? 0 : dtRow["TotalAccumulatedDepreciation"]);
                //NetBookValue = Convert.ToDecimal(Convert.IsDBNull(dtRow["NetBookValue"]) ? 0 : dtRow["NetBookValue"]);
                //TotalGainLoss = Convert.ToDecimal(Convert.IsDBNull(dtRow["TotalGainLoss"]) ? 0 : dtRow["TotalGainLoss"]);
                
                AddedBy = dtRow["AddedBy"].ToString();
                AddedDate = dtRow["AddedDate"].ToString();
                LastEditedBy = dtRow["LastEditedBy"].ToString();
                LastEditedDate = dtRow["LastEditedDate"].ToString();
                ApprovedBy = dtRow["ApprovedBy"].ToString();
                ApprovedDate = dtRow["ApprovedDate"].ToString();
                UnapprovedBy = dtRow["UnapprovedBy"].ToString();
                UnapprovedDate = dtRow["UnapprovedDate"].ToString();
                CancelledBy = dtRow["CancelledBy"].ToString();
                CancelledDate = dtRow["CancelledDate"].ToString();
                SyncedBy = dtRow["SyncedBy"].ToString();
                SyncedDate = dtRow["SyncedDate"].ToString();
                //SubmittedBy = dtRow["SubmittedBy"].ToString();
                //SubmittedDate = dtRow["SubmittedDate"].ToString();
                //CancelledBy = dtRow["CancelledBy"].ToString();
                //CancelledDate = dtRow["CancelledDate"].ToString();
                //PostedBy = dtRow["PostedBy"].ToString();
                //PostedDate = dtRow["PostedDate"].ToString();


                IsValidated = Convert.ToBoolean(Convert.IsDBNull(dtRow["IsValidated"]) ? false : dtRow["IsValidated"]);
                IsWithDetail = Convert.ToBoolean(Convert.IsDBNull(dtRow["IsWithDetail"]) ? false : dtRow["IsWithDetail"]);


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

            return a;
        }
        public void InsertData(ProductInfoSheet _ent)
        {

            Conn = _ent.Connection;
            
            //trans = _ent.TransType;
            //ddate = _ent.DocDate;
            string PISNumber = "";
            string SeriesNumber = "";
            byte[] frontimage;
            byte[] backimage;
            byte[] frontimage2d;
            byte[] backimage2d;
            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();


            //DataTable createSeriesNumber = Gears.RetriveData2("SELECT RIGHT('00000' + CAST((CONVERT(int,MAX(SUBSTRING(RIGHT(PISNumber,7),1,5))) + 1) AS VARCHAR(5)),5) AS SeriesNumber FROM Production.ProductInfoSheet", Conn);
            DataTable createSeriesNumber = Gears.RetriveData2("exec sp_Generate_PISNumber", Conn);
            SeriesNumber = Convert.IsDBNull(createSeriesNumber.Rows[0]["SeriesNumber"]) ? "00001" : createSeriesNumber.Rows[0]["SeriesNumber"].ToString();

            PISNumber = _ent.PISNumber.Substring(0,2) + "" + _ent.Gender + "" + _ent.ProductCategory + "" + _ent.ProductGroup + "" + _ent.ProductSubCategory + "" + SeriesNumber + "" + _ent.FOBSupplier;

            Docnum = PISNumber;
            SFitCode = _ent.FitCode;

            DT1.Rows.Add("Production.ProductInfoSheet", "0", "PISNumber", PISNumber);
            DT1.Rows.Add("Production.ProductInfoSheet", "0", "PISDescription", _ent.PISDescription);


            DT1.Rows.Add("Production.ProductInfoSheet", "0", "CustomerCode", _ent.CustomerCode);
            DT1.Rows.Add("Production.ProductInfoSheet", "0", "Brand", _ent.Brand);
            DT1.Rows.Add("Production.ProductInfoSheet", "0", "Gender", _ent.Gender);
            DT1.Rows.Add("Production.ProductInfoSheet", "0", "ProductCategory", _ent.ProductCategory);
            DT1.Rows.Add("Production.ProductInfoSheet", "0", "ProductGroup", _ent.ProductGroup);
            DT1.Rows.Add("Production.ProductInfoSheet", "0", "FOBSupplier", _ent.FOBSupplier);
            DT1.Rows.Add("Production.ProductInfoSheet", "0", "ProductSubCategory", _ent.ProductSubCategory);
            DT1.Rows.Add("Production.ProductInfoSheet", "0", "DesignCategory", _ent.DesignCategory);
            DT1.Rows.Add("Production.ProductInfoSheet", "0", "DesignSubCategory", _ent.DesignSubCategory);
            DT1.Rows.Add("Production.ProductInfoSheet", "0", "ProductClass", _ent.ProductClass);
            DT1.Rows.Add("Production.ProductInfoSheet", "0", "ProductSubClass", _ent.ProductSubClass);
            DT1.Rows.Add("Production.ProductInfoSheet", "0", "Inspiration", _ent.Inspiration);
            DT1.Rows.Add("Production.ProductInfoSheet", "0", "DeliveryYear", _ent.DeliveryYear);
            DT1.Rows.Add("Production.ProductInfoSheet", "0", "DeliveryMonth", _ent.DeliveryMonth);
            DT1.Rows.Add("Production.ProductInfoSheet", "0", "Theme", _ent.Theme);
            DT1.Rows.Add("Production.ProductInfoSheet", "0", "Designer", _ent.Designer);
            DT1.Rows.Add("Production.ProductInfoSheet", "0", "DISNo", _ent.DISNo);
           
            // --- FABRIC DATA --- //
            DT1.Rows.Add("Production.ProductInfoSheet", "0", "FabricCode", _ent.FabricCode);
            DT1.Rows.Add("Production.ProductInfoSheet", "0", "FabricSupplier", _ent.FabricSupplier);
            DT1.Rows.Add("Production.ProductInfoSheet", "0", "FabricColor", _ent.FabricColor);
            // --- END OF FABRIC DATA --- //


            // --- FIT DATA --- //
            DT1.Rows.Add("Production.ProductInfoSheet", "0", "ReferenceBizPartner", _ent.ReferenceBizPartner);
            DT1.Rows.Add("Production.ProductInfoSheet", "0", "FitCode", _ent.FitCode);
            DT1.Rows.Add("Production.ProductInfoSheet", "0", "BasePattern", _ent.BasePattern);
            DT1.Rows.Add("Production.ProductInfoSheet", "0", "WarpShrinkage", _ent.ActualWarpShrinkage);
            DT1.Rows.Add("Production.ProductInfoSheet", "0", "WeftShrinkage", _ent.ActualWeftShrinkage);
            DT1.Rows.Add("Production.ProductInfoSheet", "0", "CombinedShrinkage", _ent.CombinedShrinkage);
            // --- END OF FIT DATA --- //


            // --- WASH DATA --- //
            DT1.Rows.Add("Production.ProductInfoSheet", "0", "WashSupplier", _ent.WashSupplier);
            DT1.Rows.Add("Production.ProductInfoSheet", "0", "WashCode", _ent.WashCode);
            DT1.Rows.Add("Production.ProductInfoSheet", "0", "TintColor", _ent.TintColor);
            DT1.Rows.Add("Production.ProductInfoSheet", "0", "WashDescription", _ent.WashDescription);
            // --- END OF WASH DATA --- //

            // --- EMBROIDER DATA --- //
            DT1.Rows.Add("Production.ProductInfoSheet", "0", "EmbroiderySupplier", _ent.EmbroiderySupplier);
            // --- END OF EMBROIDER DATA --- //

            // --- PRINT DATA --- //
            DT1.Rows.Add("Production.ProductInfoSheet", "0", "PrintSupplier", _ent.PrintSupplier);
            // --- END OF PRINT DATA --- //

            // --- MEASUREMENT CHART DATA --- //
            DT1.Rows.Add("Production.ProductInfoSheet", "0", "BaseSize", _ent.BaseSize);
            DT1.Rows.Add("Production.ProductInfoSheet", "0", "Remarks", _ent.Remarks);
            // --- END OF MEASUREMENT CHART DATA --- //

            // --- BOM&COSTING DATA --- //
            DT1.Rows.Add("Production.ProductInfoSheet", "0", "StyleTemplateCode", _ent.StyleTemplateCode);
            DT1.Rows.Add("Production.ProductInfoSheet", "0", "StepTemplateCode", _ent.StepTemplateCode);
            DT1.Rows.Add("Production.ProductInfoSheet", "0", "TotalItemCost", _ent.TotalItemCost);
            DT1.Rows.Add("Production.ProductInfoSheet", "0", "Markup", _ent.Markup);
            DT1.Rows.Add("Production.ProductInfoSheet", "0", "SellingPrice", _ent.SellingPrice);
            DT1.Rows.Add("Production.ProductInfoSheet", "0", "AdditionalOverhead", _ent.AdditionalOverhead);
            DT1.Rows.Add("Production.ProductInfoSheet", "0", "SRP", _ent.SRP);
            DT1.Rows.Add("Production.ProductInfoSheet", "0", "ProfitFactor", _ent.ProfitFactor);
            // --- END OF BOM&COSTING DATA --- //


            DT1.Rows.Add("Production.ProductInfoSheet", "0", "FrontImage", _ent.FrontImage);
            DT1.Rows.Add("Production.ProductInfoSheet", "0", "BackImage", _ent.BackImage);
            DT1.Rows.Add("Production.ProductInfoSheet", "0", "FrontImage2D", _ent.FrontImage2D);
            DT1.Rows.Add("Production.ProductInfoSheet", "0", "BackImage2D", _ent.BackImage2D);

            DT1.Rows.Add("Production.ProductInfoSheet", "0", "AddedBy", _ent.AddedBy);
            DT1.Rows.Add("Production.ProductInfoSheet", "0", "AddedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            DT1.Rows.Add("Production.ProductInfoSheet", "0", "Field1", _ent.Field1);
            DT1.Rows.Add("Production.ProductInfoSheet", "0", "Field2", _ent.Field2);
            DT1.Rows.Add("Production.ProductInfoSheet", "0", "Field3", _ent.Field3);
            DT1.Rows.Add("Production.ProductInfoSheet", "0", "Field4", _ent.Field4);
            DT1.Rows.Add("Production.ProductInfoSheet", "0", "Field5", _ent.Field5);
            DT1.Rows.Add("Production.ProductInfoSheet", "0", "Field6", _ent.Field6);
            DT1.Rows.Add("Production.ProductInfoSheet", "0", "Field7", _ent.Field7);
            DT1.Rows.Add("Production.ProductInfoSheet", "0", "Field8", _ent.Field8);
            DT1.Rows.Add("Production.ProductInfoSheet", "0", "Field9", _ent.Field9);

            Gears.CreateData(DT1, _ent.Connection);

            //SqlConnection sqlConnection1 = new SqlConnection(Conn);
            
            //sqlConnection1.Open();
            //using (SqlCommand cmd = new SqlCommand("UPDATE Production.ProductInfoSheet SET FrontImage=@FrontImage,BackImage=@BackImage WHERE PISNumber = '" + PISNumber + "' ", sqlConnection1))
            //{
            //    cmd.Parameters.Add("@FrontImage", SqlDbType.VarBinary, -1).Value = _ent.FrontImage;
            //    cmd.Parameters.Add("@BackImage", SqlDbType.VarBinary, -1).Value = _ent.BackImage;
            //    cmd.ExecuteNonQuery();
            //}
            //sqlConnection1.Close();

            //if(_ent.FrontImage != null)
            //{
            //    sqlConnection1.Open();
            //    using (SqlCommand cmd = new SqlCommand("UPDATE Production.ProductInfoSheet SET FrontImage=@FrontImage WHERE PISNumber = '" + PISNumber + "' ", sqlConnection1))
            //    {
            //        cmd.Parameters.Add("@FrontImage", SqlDbType.VarBinary, -1).Value = _ent.FrontImage;
            //        cmd.ExecuteNonQuery();
            //    }
            //    sqlConnection1.Close();
            //}

            //if (_ent.BackImage != null)
            //{
            //    sqlConnection1.Open();
            //    using (SqlCommand cmd = new SqlCommand("UPDATE Production.ProductInfoSheet SET BackImage=@BackImage WHERE PISNumber = '" + PISNumber + "' ", sqlConnection1))
            //    {
            //        cmd.Parameters.Add("@BackImage", SqlDbType.VarBinary, -1).Value = _ent.BackImage;
            //        cmd.ExecuteNonQuery();
            //    }
            //    sqlConnection1.Close();
            //}

            //if (_ent.FrontImage2D != null)
            //{
            //    sqlConnection1.Open();
            //    using (SqlCommand cmd = new SqlCommand("UPDATE Production.ProductInfoSheet SET FrontImage2D=@FrontImage2D WHERE PISNumber = '" + PISNumber + "' ", sqlConnection1))
            //    {
            //        cmd.Parameters.Add("@FrontImage2D", SqlDbType.VarBinary, -1).Value = _ent.FrontImage2D;
            //        cmd.ExecuteNonQuery();
            //    }
            //    sqlConnection1.Close();
            //}

            //if (_ent.BackImage2D != null)
            //{
            //    sqlConnection1.Open();
            //    using (SqlCommand cmd = new SqlCommand("UPDATE Production.ProductInfoSheet SET BackImage2D=@BackImage2D WHERE PISNumber = '" + PISNumber + "' ", sqlConnection1))
            //    {
            //        cmd.Parameters.Add("@BackImage2D", SqlDbType.VarBinary, -1).Value = _ent.BackImage2D;
            //        cmd.ExecuteNonQuery();
            //    }
            //    sqlConnection1.Close();
            //}
            
        }

        public void UpdateData(ProductInfoSheet _ent)
        {
            Docnum = _ent.PISNumber;
            Conn = _ent.Connection;
            SFitCode = _ent.FitCode;
            //trans = _ent.TransType;
            //ddate = _ent.DocDate;


            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();

            DT1.Rows.Add("Production.ProductInfoSheet", "cond", "PISNumber", _ent.PISNumber);
            DT1.Rows.Add("Production.ProductInfoSheet", "set", "PISDescription", _ent.PISDescription);


            DT1.Rows.Add("Production.ProductInfoSheet", "set", "CustomerCode", _ent.CustomerCode);
            DT1.Rows.Add("Production.ProductInfoSheet", "set", "Brand", _ent.Brand);
            DT1.Rows.Add("Production.ProductInfoSheet", "set", "Gender", _ent.Gender);
            DT1.Rows.Add("Production.ProductInfoSheet", "set", "ProductCategory", _ent.ProductCategory);
            DT1.Rows.Add("Production.ProductInfoSheet", "set", "ProductGroup", _ent.ProductGroup);
            DT1.Rows.Add("Production.ProductInfoSheet", "set", "FOBSupplier", _ent.FOBSupplier);
            DT1.Rows.Add("Production.ProductInfoSheet", "set", "ProductSubCategory", _ent.ProductSubCategory);
            DT1.Rows.Add("Production.ProductInfoSheet", "set", "DesignCategory", _ent.DesignCategory);
            DT1.Rows.Add("Production.ProductInfoSheet", "set", "DesignSubCategory", _ent.DesignSubCategory);
            DT1.Rows.Add("Production.ProductInfoSheet", "set", "ProductClass", _ent.ProductClass);
            DT1.Rows.Add("Production.ProductInfoSheet", "set", "ProductSubClass", _ent.ProductSubClass);
            DT1.Rows.Add("Production.ProductInfoSheet", "set", "Inspiration", _ent.Inspiration);
            DT1.Rows.Add("Production.ProductInfoSheet", "set", "DeliveryYear", _ent.DeliveryYear);
            DT1.Rows.Add("Production.ProductInfoSheet", "set", "DeliveryMonth", _ent.DeliveryMonth);
            DT1.Rows.Add("Production.ProductInfoSheet", "set", "Theme", _ent.Theme);
            DT1.Rows.Add("Production.ProductInfoSheet", "set", "Designer", _ent.Designer);
            DT1.Rows.Add("Production.ProductInfoSheet", "set", "DISNo", _ent.DISNo);

            // --- FABRIC DATA --- //
            DT1.Rows.Add("Production.ProductInfoSheet", "set", "FabricCode", _ent.FabricCode);
            DT1.Rows.Add("Production.ProductInfoSheet", "set", "FabricSupplier", _ent.FabricSupplier);
            DT1.Rows.Add("Production.ProductInfoSheet", "set", "FabricColor", _ent.FabricColor);
            // --- END OF FABRIC DATA --- //

            // --- FIT DATA --- //
            DT1.Rows.Add("Production.ProductInfoSheet", "set", "ReferenceBizPartner", _ent.ReferenceBizPartner);
            DT1.Rows.Add("Production.ProductInfoSheet", "set", "FitCode", _ent.FitCode);
            DT1.Rows.Add("Production.ProductInfoSheet", "set", "BasePattern", _ent.BasePattern);
            DT1.Rows.Add("Production.ProductInfoSheet", "set", "WarpShrinkage", _ent.ActualWarpShrinkage);
            DT1.Rows.Add("Production.ProductInfoSheet", "set", "WeftShrinkage", _ent.ActualWeftShrinkage);
            DT1.Rows.Add("Production.ProductInfoSheet", "set", "CombinedShrinkage", _ent.CombinedShrinkage);
            // --- END OF FIT DATA --- //

            // --- WASH DATA --- //
            DT1.Rows.Add("Production.ProductInfoSheet", "set", "WashSupplier", _ent.WashSupplier);
            DT1.Rows.Add("Production.ProductInfoSheet", "set", "WashCode", _ent.WashCode);
            DT1.Rows.Add("Production.ProductInfoSheet", "set", "TintColor", _ent.TintColor);
            DT1.Rows.Add("Production.ProductInfoSheet", "set", "WashDescription", _ent.WashDescription);
            // --- END OF WASH DATA --- //

            // --- EMBROIDER DATA --- //
            DT1.Rows.Add("Production.ProductInfoSheet", "set", "EmbroiderySupplier", _ent.EmbroiderySupplier);
            // --- END OF EMBROIDER DATA --- //

            // --- PRINT DATA --- //
            DT1.Rows.Add("Production.ProductInfoSheet", "set", "PrintSupplier", _ent.PrintSupplier);
            // --- END OF PRINT DATA --- //

            // --- MEASUREMENT CHART DATA --- //
            DT1.Rows.Add("Production.ProductInfoSheet", "set", "BaseSize", _ent.BaseSize);
            DT1.Rows.Add("Production.ProductInfoSheet", "set", "Remarks", _ent.Remarks);
            // --- END OF MEASUREMENT CHART DATA --- //

            // --- BOM&COSTING DATA --- //
            DT1.Rows.Add("Production.ProductInfoSheet", "set", "StyleTemplateCode", _ent.StyleTemplateCode);
            DT1.Rows.Add("Production.ProductInfoSheet", "set", "StepTemplateCode", _ent.StepTemplateCode);
            DT1.Rows.Add("Production.ProductInfoSheet", "set", "TotalItemCost", _ent.TotalItemCost);
            DT1.Rows.Add("Production.ProductInfoSheet", "set", "Markup", _ent.Markup);
            DT1.Rows.Add("Production.ProductInfoSheet", "set", "SellingPrice", _ent.SellingPrice);
            DT1.Rows.Add("Production.ProductInfoSheet", "set", "AdditionalOverhead", _ent.AdditionalOverhead);
            DT1.Rows.Add("Production.ProductInfoSheet", "set", "SRP", _ent.SRP);
            DT1.Rows.Add("Production.ProductInfoSheet", "set", "ProfitFactor", _ent.ProfitFactor);
            // --- END OF BOM&COSTING DATA --- //


            DT1.Rows.Add("Production.ProductInfoSheet", "set", "FrontImage", _ent.FrontImage);
            DT1.Rows.Add("Production.ProductInfoSheet", "set", "BackImage", _ent.BackImage);
            DT1.Rows.Add("Production.ProductInfoSheet", "set", "FrontImage2D", _ent.FrontImage2D);
            DT1.Rows.Add("Production.ProductInfoSheet", "set", "BackImage2D", _ent.BackImage2D);


            DT1.Rows.Add("Production.ProductInfoSheet", "set", "LastEditedBy", _ent.LastEditedBy);
            DT1.Rows.Add("Production.ProductInfoSheet", "set", "LastEditedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));


            //DT1.Rows.Add("Accounting.AssetDisposal", "set", "IsValidated", _ent.IsValidated);
            //DT1.Rows.Add("Accounting.AssetDisposal", "set", "IsWithDetail", _ent.IsWithDetail);


            DT1.Rows.Add("Production.ProductInfoSheet", "set", "Field1", _ent.Field1);
            DT1.Rows.Add("Production.ProductInfoSheet", "set", "Field2", _ent.Field2);
            DT1.Rows.Add("Production.ProductInfoSheet", "set", "Field3", _ent.Field3);
            DT1.Rows.Add("Production.ProductInfoSheet", "set", "Field4", _ent.Field4);
            DT1.Rows.Add("Production.ProductInfoSheet", "set", "Field5", _ent.Field5);
            DT1.Rows.Add("Production.ProductInfoSheet", "set", "Field6", _ent.Field6);
            DT1.Rows.Add("Production.ProductInfoSheet", "set", "Field7", _ent.Field7);
            DT1.Rows.Add("Production.ProductInfoSheet", "set", "Field8", _ent.Field8);
            DT1.Rows.Add("Production.ProductInfoSheet", "set", "Field9", _ent.Field9);

         Gears.UpdateData(DT1, _ent.Connection);
            Functions.AuditTrail("PROPIS", Docnum, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "UPDATE", _ent.Connection);
        }
        public void DeleteData(ProductInfoSheet _ent)
        {
            Docnum = _ent.PISNumber;
            Conn = _ent.Connection;

            Gears.CRUDdatatable DT1 = new Gears.CRUDdatatable();
            DT1.Rows.Add("Production.ProductInfoSheet", "cond", "PISNumber", _ent.PISNumber);
            Gears.DeleteData(DT1, _ent.Connection);

            Gears.CRUDdatatable DT2 = new Gears.CRUDdatatable();
            DT2.Rows.Add("Production.MaterialPlanningDetail", "cond", "PISNumber", _ent.PISNumber);
            Gears.DeleteData(DT2, _ent.Connection);

            Functions.AuditTrail("PROPIS", Docnum, LastEditedBy, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "DELETE", _ent.Connection);
        }
    }
}
