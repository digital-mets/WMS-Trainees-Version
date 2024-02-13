using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data;
using GearsLibrary;
using DevExpress.DataAccess.Sql;
using System.Web;

namespace GWL.WebReports.GEARS_Reports
{
    /// <summary>
    /// Summary description for R_AnnualBalanceSheet
    /// </summary>
    public partial class R_ContributionMarginISDetail : DevExpress.XtraReports.UI.XtraReport
    {
        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private DevExpress.XtraReports.Parameters.Parameter Year;
        private DevExpress.DataAccess.Sql.SqlDataSource sqlDataSource1;
        private ReportHeaderBand ReportHeader;
        private XRTable xrTable2;
        private XRTableRow xrTableRow2;
        private XRTableCell xrTableCell13;
        private XRTableCell xrTableCell14;
        private XRTableCell xrTableCell15;
        private XRTableCell xrTableCell16;
        private XRTableCell xrTableCell17;
        private XRTableCell xrTableCell18;
        private XRTableCell xrTableCell19;
        private XRTableCell xrTableCell20;
        private XRTableCell xrTableCell21;
        private XRTableCell xrTableCell22;
        private XRTableCell xrTableCell23;
        private XRTableCell xrTableCell24;
        private DetailReportBand DetailReport;
        private DetailBand Detail1;
        private GroupHeaderBand GroupHeader1;
        private XRTableCell xrTableCell25;
        private XRTable xrTable3;
        private XRTableRow xrTableRow3;
        private XRTableCell xrTableCell29;
        private XRTableCell xrTableCell30;
        private XRTableCell xrTableCell31;
        private XRTableCell xrTableCell32;
        private XRTableCell xrTableCell33;
        private XRTableCell xrTableCell34;
        private XRTableCell xrTableCell35;
        private XRTableCell xrTableCell36;
        private XRTableCell xrTableCell37;
        private XRTableCell xrTableCell38;
        private XRTableCell xrTableCell39;
        private XRTableCell xrTableCell40;
        private XRTableCell xrTableCell41;
        private DetailReportBand DetailReport1;
        private DetailBand Detail2;
        private GroupFooterBand GroupFooter3;
        private CalculatedField calculatedField1;
        private CalculatedField calculatedField2;
        private CalculatedField calculatedField3;
        private CalculatedField calculatedField4;
        private PageHeaderBand PageHeader;
        private XRLabel xrLabel15;
        private XRLabel xrLabel18;
        private XRLabel xrLabel4;
        private XRTable xrTable11;
        private XRTableRow xrTableRow22;
        private XRTableCell xrTableCell81;
        private XRTableCell xrTableCell289;
        private XRTableCell xrTableCell290;
        private XRTableCell xrTableCell291;
        private XRTableCell xrTableCell292;
        private XRTableCell xrTableCell293;
        private XRTableCell xrTableCell294;
        private XRTableCell xrTableCell295;
        private XRTableCell xrTableCell296;
        private XRTableCell xrTableCell297;
        private XRTableCell xrTableCell298;
        private XRTableCell xrTableCell299;
        private XRTableCell xrTableCell300;
        private XRLabel xrLabel6;
        private XRTable xrTable9;
        private XRTableRow xrTableRow20;
        private XRTableCell xrTableCell251;
        private XRTableCell xrTableCell264;
        private XRTableCell xrTableCell265;
        private XRTableCell xrTableCell266;
        private XRTableCell xrTableCell267;
        private XRTableCell xrTableCell268;
        private XRTableCell xrTableCell269;
        private XRTableCell xrTableCell270;
        private XRTableCell xrTableCell271;
        private XRTableCell xrTableCell272;
        private XRTableCell xrTableCell273;
        private XRTableCell xrTableCell274;
        private XRTableCell xrTableCell275;
        private XRLabel xrLabel5;
        private DetailReportBand DetailReport2;
        private DetailBand Detail3;
        private GroupFooterBand GroupFooter2;
        private XRLabel xrLabel1;
        private FormattingRule formattingRule1;
        private CalculatedField calculatedField5;
        private XRTable xrTable16;
        private XRTableRow xrTableRow26;
        private XRTableCell xrTableCell342;
        private XRTableCell xrTableCell343;
        private XRTableCell xrTableCell344;
        private XRTableCell xrTableCell345;
        private XRTableCell xrTableCell346;
        private XRTableCell xrTableCell347;
        private XRTableCell xrTableCell348;
        private XRTableCell xrTableCell349;
        private XRTableCell xrTableCell350;
        private XRTableCell xrTableCell351;
        private XRTableCell xrTableCell352;
        private XRTableCell xrTableCell353;
        private XRTableCell xrTableCell354;
        private XRTable xrTable15;
        private XRTableRow xrTableRow25;
        private XRTableCell xrTableCell327;
        private XRTableCell xrTableCell328;
        private XRTableCell xrTableCell329;
        private XRTableCell xrTableCell330;
        private XRTableCell xrTableCell331;
        private XRTableCell xrTableCell332;
        private XRTableCell xrTableCell333;
        private XRTableCell xrTableCell334;
        private XRTableCell xrTableCell335;
        private XRTableCell xrTableCell336;
        private XRTableCell xrTableCell337;
        private XRTableCell xrTableCell338;
        private XRTableCell xrTableCell339;
        private XRLabel xrLabel9;
        private XRTable xrTable5;
        private XRTableRow xrTableRow5;
        private XRTableCell xrTableCell55;
        private XRTableCell xrTableCell56;
        private XRTableCell xrTableCell57;
        private XRTableCell xrTableCell58;
        private XRTableCell xrTableCell59;
        private XRTableCell xrTableCell60;
        private XRTableCell xrTableCell61;
        private XRTableCell xrTableCell62;
        private XRTableCell xrTableCell63;
        private XRTableCell xrTableCell64;
        private XRTableCell xrTableCell65;
        private XRTableCell xrTableCell66;
        private XRTableCell xrTableCell67;
        private XRTable xrTable6;
        private XRTableRow xrTableRow6;
        private XRTableCell xrTableCell68;
        private XRTableCell xrTableCell69;
        private XRTableCell xrTableCell70;
        private XRTableCell xrTableCell71;
        private XRTableCell xrTableCell72;
        private XRTableCell xrTableCell73;
        private XRTableCell xrTableCell74;
        private XRTableCell xrTableCell75;
        private XRTableCell xrTableCell76;
        private XRTableCell xrTableCell77;
        private XRTableCell xrTableCell78;
        private XRTableCell xrTableCell79;
        private XRTableCell xrTableCell80;
        private XRTable xrTable10;
        private XRTableRow xrTableRow21;
        private XRTableCell xrTableCell276;
        private XRTableCell xrTableCell277;
        private XRTableCell xrTableCell278;
        private XRTableCell xrTableCell279;
        private XRTableCell xrTableCell280;
        private XRTableCell xrTableCell281;
        private XRTableCell xrTableCell282;
        private XRTableCell xrTableCell283;
        private XRTableCell xrTableCell284;
        private XRTableCell xrTableCell285;
        private XRTableCell xrTableCell286;
        private XRTableCell xrTableCell287;
        private XRTableCell xrTableCell288;
        private XRTable xrTable12;
        private XRTableRow xrTableRow23;
        private XRTableCell xrTableCell301;
        private XRTableCell xrTableCell302;
        private XRTableCell xrTableCell303;
        private XRTableCell xrTableCell304;
        private XRTableCell xrTableCell305;
        private XRTableCell xrTableCell306;
        private XRTableCell xrTableCell307;
        private XRTableCell xrTableCell308;
        private XRTableCell xrTableCell309;
        private XRTableCell xrTableCell310;
        private XRTableCell xrTableCell311;
        private XRTableCell xrTableCell312;
        private XRTableCell xrTableCell313;
        private XRLabel xrLabel2;
        private XRLabel xrLabel7;
        private ReportFooterBand ReportFooter1;
        private XRTable xrTable14;
        private XRTableRow xrTableRow27;
        private XRTableCell xrTableCell355;
        private XRTableCell xrTableCell356;
        private XRTableCell xrTableCell357;
        private XRTableCell xrTableCell358;
        private XRTableCell xrTableCell359;
        private XRTableCell xrTableCell360;
        private XRTableCell xrTableCell361;
        private XRTableCell xrTableCell362;
        private XRTableCell xrTableCell363;
        private XRTableCell xrTableCell364;
        private XRTableCell xrTableCell365;
        private XRTableCell xrTableCell366;
        private XRTableCell xrTableCell367;
        private XRTableCell xrTableCell368;
        private XRTableRow xrTableRow28;
        private XRTableCell xrTableCell369;
        private XRTableCell xrTableCell370;
        private XRTableCell xrTableCell371;
        private XRTableCell xrTableCell372;
        private XRTableCell xrTableCell373;
        private XRTableCell xrTableCell374;
        private XRTableCell xrTableCell375;
        private XRTableCell xrTableCell376;
        private XRTableCell xrTableCell377;
        private XRTableCell xrTableCell378;
        private XRTableCell xrTableCell379;
        private XRTableCell xrTableCell380;
        private XRTableCell xrTableCell381;
        private XRTableCell xrTableCell382;
        private XRTableRow xrTableRow29;
        private XRTableCell xrTableCell383;
        private XRTableCell xrTableCell384;
        private XRTableCell xrTableCell385;
        private XRTableCell xrTableCell386;
        private XRTableCell xrTableCell387;
        private XRTableCell xrTableCell388;
        private XRTableCell xrTableCell389;
        private XRTableCell xrTableCell390;
        private XRTableCell xrTableCell391;
        private XRTableCell xrTableCell392;
        private XRTableCell xrTableCell393;
        private XRTableCell xrTableCell394;
        private XRTableCell xrTableCell395;
        private XRTableCell xrTableCell396;
        private GroupFooterBand GroupFooter1;
        private XRTableRow xrTableRow7;
        private XRTableCell xrTableCell82;
        private XRTableCell xrTableCell83;
        private XRTableCell xrTableCell84;
        private XRTableCell xrTableCell85;
        private XRTableCell xrTableCell86;
        private XRTableCell xrTableCell87;
        private XRTableCell xrTableCell88;
        private XRTableCell xrTableCell89;
        private XRTableCell xrTableCell90;
        private XRTableCell xrTableCell91;
        private XRTableCell xrTableCell92;
        private XRTableCell xrTableCell93;
        private XRTableCell xrTableCell94;
        private XRTableCell xrTableCell314;
        private XRTableRow xrTableRow24;
        private XRTableCell xrTableCell315;
        private XRTableCell xrTableCell316;
        private XRTableCell xrTableCell317;
        private XRTableCell xrTableCell318;
        private XRTableCell xrTableCell319;
        private XRTableCell xrTableCell320;
        private XRTableCell xrTableCell321;
        private XRTableCell xrTableCell322;
        private XRTableCell xrTableCell323;
        private XRTableCell xrTableCell324;
        private XRTableCell xrTableCell325;
        private XRTableCell xrTableCell326;
        private XRTableCell xrTableCell340;
        private XRTableCell xrTableCell341;
        private XRTableRow xrTableRow30;
        private XRTableCell xrTableCell397;
        private XRTableCell xrTableCell398;
        private XRTableCell xrTableCell399;
        private XRTableCell xrTableCell400;
        private XRTableCell xrTableCell401;
        private XRTableCell xrTableCell402;
        private XRTableCell xrTableCell403;
        private XRTableCell xrTableCell404;
        private XRTableCell xrTableCell405;
        private XRTableCell xrTableCell406;
        private XRTableCell xrTableCell407;
        private XRTableCell xrTableCell408;
        private XRTableCell xrTableCell409;
        private XRTableCell xrTableCell410;
        private XRTableRow xrTableRow31;
        private XRTableCell xrTableCell411;
        private XRTableCell xrTableCell412;
        private XRTableCell xrTableCell413;
        private XRTableCell xrTableCell414;
        private XRTableCell xrTableCell415;
        private XRTableCell xrTableCell416;
        private XRTableCell xrTableCell417;
        private XRTableCell xrTableCell418;
        private XRTableCell xrTableCell419;
        private XRTableCell xrTableCell420;
        private XRTableCell xrTableCell421;
        private XRTableCell xrTableCell422;
        private XRTableCell xrTableCell423;
        private XRTableCell xrTableCell424;
        private XRTableRow xrTableRow32;
        private XRTableCell xrTableCell425;
        private XRTableCell xrTableCell426;
        private XRTableCell xrTableCell427;
        private XRTableCell xrTableCell428;
        private XRTableCell xrTableCell429;
        private XRTableCell xrTableCell430;
        private XRTableCell xrTableCell431;
        private XRTableCell xrTableCell432;
        private XRTableCell xrTableCell433;
        private XRTableCell xrTableCell434;
        private XRTableCell xrTableCell435;
        private XRTableCell xrTableCell436;
        private XRTableCell xrTableCell437;
        private XRTableCell xrTableCell438;
        private XRTableRow xrTableRow33;
        private XRTableCell xrTableCell439;
        private XRTableCell xrTableCell440;
        private XRTableCell xrTableCell441;
        private XRTableCell xrTableCell442;
        private XRTableCell xrTableCell443;
        private XRTableCell xrTableCell444;
        private XRTableCell xrTableCell445;
        private XRTableCell xrTableCell446;
        private XRTableCell xrTableCell447;
        private XRTableCell xrTableCell448;
        private XRTableCell xrTableCell449;
        private XRTableCell xrTableCell450;
        private XRTableCell xrTableCell451;
        private XRTableCell xrTableCell452;
        private XRTableRow xrTableRow34;
        private XRTableCell xrTableCell453;
        private XRTableCell xrTableCell454;
        private XRTableCell xrTableCell455;
        private XRTableCell xrTableCell456;
        private XRTableCell xrTableCell457;
        private XRTableCell xrTableCell458;
        private XRTableCell xrTableCell459;
        private XRTableCell xrTableCell460;
        private XRTableCell xrTableCell461;
        private XRTableCell xrTableCell462;
        private XRTableCell xrTableCell463;
        private XRTableCell xrTableCell464;
        private XRTableCell xrTableCell465;
        private XRTableCell xrTableCell466;
        private XRTableRow xrTableRow35;
        private XRTableCell xrTableCell467;
        private XRTableCell xrTableCell468;
        private XRTableCell xrTableCell469;
        private XRTableCell xrTableCell470;
        private XRTableCell xrTableCell471;
        private XRTableCell xrTableCell472;
        private XRTableCell xrTableCell473;
        private XRTableCell xrTableCell474;
        private XRTableCell xrTableCell475;
        private XRTableCell xrTableCell476;
        private XRTableCell xrTableCell477;
        private XRTableCell xrTableCell478;
        private XRTableCell xrTableCell479;
        private XRTableCell xrTableCell480;
        private XRTableRow xrTableRow36;
        private XRTableCell xrTableCell481;
        private XRTableCell xrTableCell482;
        private XRTableCell xrTableCell483;
        private XRTableCell xrTableCell484;
        private XRTableCell xrTableCell485;
        private XRTableCell xrTableCell486;
        private XRTableCell xrTableCell487;
        private XRTableCell xrTableCell488;
        private XRTableCell xrTableCell489;
        private XRTableCell xrTableCell490;
        private XRTableCell xrTableCell491;
        private XRTableCell xrTableCell492;
        private XRTableCell xrTableCell493;
        private XRTableCell xrTableCell494;
        private XRTableRow xrTableRow37;
        private XRTableCell xrTableCell495;
        private XRTableCell xrTableCell496;
        private XRTableCell xrTableCell497;
        private XRTableCell xrTableCell498;
        private XRTableCell xrTableCell499;
        private XRTableCell xrTableCell500;
        private XRTableCell xrTableCell501;
        private XRTableCell xrTableCell502;
        private XRTableCell xrTableCell503;
        private XRTableCell xrTableCell504;
        private XRTableCell xrTableCell505;
        private XRTableCell xrTableCell506;
        private XRTableCell xrTableCell507;
        private XRTableCell xrTableCell508;
        private XRTableRow xrTableRow38;
        private XRTableCell xrTableCell509;
        private XRTableCell xrTableCell510;
        private XRTableCell xrTableCell511;
        private XRTableCell xrTableCell512;
        private XRTableCell xrTableCell513;
        private XRTableCell xrTableCell514;
        private XRTableCell xrTableCell515;
        private XRTableCell xrTableCell516;
        private XRTableCell xrTableCell517;
        private XRTableCell xrTableCell518;
        private XRTableCell xrTableCell519;
        private XRTableCell xrTableCell520;
        private XRTableCell xrTableCell521;
        private XRTableCell xrTableCell522;
        private XRTableRow xrTableRow39;
        private XRTableCell xrTableCell523;
        private XRTableCell xrTableCell524;
        private XRTableCell xrTableCell525;
        private XRTableCell xrTableCell526;
        private XRTableCell xrTableCell527;
        private XRTableCell xrTableCell528;
        private XRTableCell xrTableCell529;
        private XRTableCell xrTableCell530;
        private XRTableCell xrTableCell531;
        private XRTableCell xrTableCell532;
        private XRTableCell xrTableCell533;
        private XRTableCell xrTableCell534;
        private XRTableCell xrTableCell535;
        private XRTableCell xrTableCell536;
        private XRLabel xrLabel10;
        private XRLabel xrLabel3;
        private DetailReportBand DetailReport3;
        private DetailBand Detail4;
        private GroupFooterBand GroupFooter4;
        private XRTable xrTable7;
        private XRTableRow xrTableRow8;
        private XRTableCell xrTableCell95;
        private XRTableCell xrTableCell96;
        private XRTableCell xrTableCell97;
        private XRTableCell xrTableCell98;
        private XRTableCell xrTableCell99;
        private XRTableCell xrTableCell100;
        private XRTableCell xrTableCell101;
        private XRTableCell xrTableCell102;
        private XRTableCell xrTableCell103;
        private XRTableCell xrTableCell104;
        private XRTableCell xrTableCell105;
        private XRTableCell xrTableCell106;
        private XRTableCell xrTableCell107;
        private XRTableCell xrTableCell108;
        private XRTable xrTable8;
        private XRTableRow xrTableRow9;
        private XRTableCell xrTableCell109;
        private XRTableCell xrTableCell110;
        private XRTableCell xrTableCell111;
        private XRTableCell xrTableCell112;
        private XRTableCell xrTableCell113;
        private XRTableCell xrTableCell114;
        private XRTableCell xrTableCell115;
        private XRTableCell xrTableCell116;
        private XRTableCell xrTableCell117;
        private XRTableCell xrTableCell118;
        private XRTableCell xrTableCell119;
        private XRTableCell xrTableCell120;
        private XRTableCell xrTableCell121;
        private XRTableCell xrTableCell122;
        private DetailReportBand DetailReport4;
        private DetailBand Detail5;
        private XRTable xrTable13;
        private XRTableRow xrTableRow10;
        private XRTableCell xrTableCell123;
        private XRTableCell xrTableCell124;
        private XRTableCell xrTableCell125;
        private XRTableCell xrTableCell126;
        private XRTableCell xrTableCell127;
        private XRTableCell xrTableCell128;
        private XRTableCell xrTableCell129;
        private XRTableCell xrTableCell130;
        private XRTableCell xrTableCell131;
        private XRTableCell xrTableCell132;
        private XRTableCell xrTableCell133;
        private XRTableCell xrTableCell134;
        private XRTableCell xrTableCell135;
        private XRTableCell xrTableCell136;
        private GroupFooterBand GroupFooter5;
        private XRTable xrTable17;
        private XRTableRow xrTableRow11;
        private XRTableCell xrTableCell137;
        private XRTableCell xrTableCell138;
        private XRTableCell xrTableCell139;
        private XRTableCell xrTableCell140;
        private XRTableCell xrTableCell141;
        private XRTableCell xrTableCell142;
        private XRTableCell xrTableCell143;
        private XRTableCell xrTableCell144;
        private XRTableCell xrTableCell145;
        private XRTableCell xrTableCell146;
        private XRTableCell xrTableCell147;
        private XRTableCell xrTableCell148;
        private XRTableCell xrTableCell149;
        private XRTableCell xrTableCell150;
        private DetailReportBand DetailReport5;
        private DetailBand Detail6;
        private XRTable xrTable1;
        private XRTableRow xrTableRow1;
        private XRTableCell xrTableCell1;
        private XRTableCell xrTableCell2;
        private XRTableCell xrTableCell3;
        private XRTableCell xrTableCell4;
        private XRTableCell xrTableCell5;
        private XRTableCell xrTableCell6;
        private XRTableCell xrTableCell7;
        private XRTableCell xrTableCell8;
        private XRTableCell xrTableCell9;
        private XRTableCell xrTableCell10;
        private XRTableCell xrTableCell11;
        private XRTableCell xrTableCell12;
        private XRTableCell xrTableCell26;
        private XRTableCell xrTableCell27;
        private GroupHeaderBand GroupHeader3;
        private XRLabel xrLabel8;
        private GroupFooterBand GroupFooter6;
        private XRTable xrTable18;
        private XRTableRow xrTableRow12;
        private XRTableCell xrTableCell151;
        private XRTableCell xrTableCell152;
        private XRTableCell xrTableCell153;
        private XRTableCell xrTableCell154;
        private XRTableCell xrTableCell155;
        private XRTableCell xrTableCell156;
        private XRTableCell xrTableCell157;
        private XRTableCell xrTableCell158;
        private XRTableCell xrTableCell159;
        private XRTableCell xrTableCell160;
        private XRTableCell xrTableCell161;
        private XRTableCell xrTableCell162;
        private XRTableCell xrTableCell163;
        private XRTableCell xrTableCell164;
        private DetailReportBand DetailReport6;
        private DetailBand Detail7;
        private XRTable xrTable19;
        private XRTableRow xrTableRow13;
        private XRTableCell xrTableCell165;
        private XRTableCell xrTableCell166;
        private XRTableCell xrTableCell167;
        private XRTableCell xrTableCell168;
        private XRTableCell xrTableCell169;
        private XRTableCell xrTableCell170;
        private XRTableCell xrTableCell171;
        private XRTableCell xrTableCell172;
        private XRTableCell xrTableCell173;
        private XRTableCell xrTableCell174;
        private XRTableCell xrTableCell175;
        private XRTableCell xrTableCell176;
        private XRTableCell xrTableCell177;
        private XRTableCell xrTableCell178;
        private GroupFooterBand GroupFooter7;
        private XRTable xrTable4;
        private XRTableRow xrTableRow4;
        private XRTableCell xrTableCell28;
        private XRTableCell xrTableCell42;
        private XRTableCell xrTableCell43;
        private XRTableCell xrTableCell44;
        private XRTableCell xrTableCell45;
        private XRTableCell xrTableCell46;
        private XRTableCell xrTableCell47;
        private XRTableCell xrTableCell48;
        private XRTableCell xrTableCell49;
        private XRTableCell xrTableCell50;
        private XRTableCell xrTableCell51;
        private XRTableCell xrTableCell52;
        private XRTableCell xrTableCell53;
        private XRTableCell xrTableCell54;
        private XRTable xrTable20;
        private XRTableRow xrTableRow14;
        private XRTableCell xrTableCell181;
        private XRTable xrTable21;
        private XRTableRow xrTableRow15;
        private XRTableCell xrTableCell179;
        private XRTable xrTable22;
        private XRTableRow xrTableRow16;
        private XRTableCell xrTableCell180;
        private XRTable xrTable23;
        private XRTableRow xrTableRow17;
        private XRTableCell xrTableCell182;
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public R_ContributionMarginISDetail()
        {
            InitializeComponent();
            //
            // TODO: Add constructor logic here
            //
            if (HttpContext.Current.Session["ConnString"] != null)
                sqlDataSource1.Connection.ConnectionString = HttpContext.Current.Session["ConnString"].ToString();

            CustomSqlQuery query = sqlDataSource1.Queries[1] as CustomSqlQuery;
            DataTable dtyear = Gears.RetriveData2(query.Sql, HttpContext.Current.Session["ConnString"].ToString());
            DataRow _ret = dtyear.Rows[0];
            Year.Value = Convert.ToInt16(_ret[0].ToString());
        }

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            DevExpress.DataAccess.Sql.CustomSqlQuery customSqlQuery1 = new DevExpress.DataAccess.Sql.CustomSqlQuery();
            DevExpress.DataAccess.Sql.CustomSqlQuery customSqlQuery2 = new DevExpress.DataAccess.Sql.CustomSqlQuery();
            DevExpress.DataAccess.Sql.CustomSqlQuery customSqlQuery3 = new DevExpress.DataAccess.Sql.CustomSqlQuery();
            DevExpress.DataAccess.Sql.StoredProcQuery storedProcQuery1 = new DevExpress.DataAccess.Sql.StoredProcQuery();
            DevExpress.DataAccess.Sql.QueryParameter queryParameter1 = new DevExpress.DataAccess.Sql.QueryParameter();
            DevExpress.DataAccess.Sql.StoredProcQuery storedProcQuery2 = new DevExpress.DataAccess.Sql.StoredProcQuery();
            DevExpress.DataAccess.Sql.QueryParameter queryParameter2 = new DevExpress.DataAccess.Sql.QueryParameter();
            DevExpress.DataAccess.Sql.QueryParameter queryParameter3 = new DevExpress.DataAccess.Sql.QueryParameter();
            DevExpress.DataAccess.Sql.StoredProcQuery storedProcQuery3 = new DevExpress.DataAccess.Sql.StoredProcQuery();
            DevExpress.DataAccess.Sql.QueryParameter queryParameter4 = new DevExpress.DataAccess.Sql.QueryParameter();
            DevExpress.DataAccess.Sql.QueryParameter queryParameter5 = new DevExpress.DataAccess.Sql.QueryParameter();
            DevExpress.DataAccess.Sql.StoredProcQuery storedProcQuery4 = new DevExpress.DataAccess.Sql.StoredProcQuery();
            DevExpress.DataAccess.Sql.QueryParameter queryParameter6 = new DevExpress.DataAccess.Sql.QueryParameter();
            DevExpress.DataAccess.Sql.QueryParameter queryParameter7 = new DevExpress.DataAccess.Sql.QueryParameter();
            DevExpress.DataAccess.Sql.StoredProcQuery storedProcQuery5 = new DevExpress.DataAccess.Sql.StoredProcQuery();
            DevExpress.DataAccess.Sql.QueryParameter queryParameter8 = new DevExpress.DataAccess.Sql.QueryParameter();
            DevExpress.DataAccess.Sql.QueryParameter queryParameter9 = new DevExpress.DataAccess.Sql.QueryParameter();
            DevExpress.DataAccess.Sql.StoredProcQuery storedProcQuery6 = new DevExpress.DataAccess.Sql.StoredProcQuery();
            DevExpress.DataAccess.Sql.QueryParameter queryParameter10 = new DevExpress.DataAccess.Sql.QueryParameter();
            DevExpress.DataAccess.Sql.QueryParameter queryParameter11 = new DevExpress.DataAccess.Sql.QueryParameter();
            DevExpress.DataAccess.Sql.StoredProcQuery storedProcQuery7 = new DevExpress.DataAccess.Sql.StoredProcQuery();
            DevExpress.DataAccess.Sql.QueryParameter queryParameter12 = new DevExpress.DataAccess.Sql.QueryParameter();
            DevExpress.DataAccess.Sql.QueryParameter queryParameter13 = new DevExpress.DataAccess.Sql.QueryParameter();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(R_ContributionMarginISDetail));
            DevExpress.XtraReports.Parameters.DynamicListLookUpSettings dynamicListLookUpSettings1 = new DevExpress.XtraReports.Parameters.DynamicListLookUpSettings();
            DevExpress.XtraReports.UI.XRSummary xrSummary1 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary2 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary3 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary4 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary5 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary6 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary7 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary8 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary9 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary10 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary11 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary12 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary13 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary14 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary15 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary16 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary17 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary18 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary19 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary20 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary21 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary22 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary23 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary24 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary25 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary26 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary27 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary28 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary29 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary30 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary31 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary32 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary33 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary34 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary35 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary36 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary37 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary38 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary39 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary40 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary41 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary42 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary43 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary44 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary45 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary46 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary47 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary48 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary49 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary50 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary51 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary52 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary53 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary54 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary55 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary56 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary57 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary58 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary59 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary60 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary61 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary62 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary63 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary64 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary65 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary66 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary67 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary68 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary69 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary70 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary71 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary72 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary73 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary74 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary75 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary76 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary77 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary78 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary79 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary80 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary81 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary82 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary83 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary84 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary85 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary86 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary87 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary88 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary89 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary90 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary91 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary92 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary93 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary94 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary95 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary96 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary97 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary98 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary99 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary100 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary101 = new DevExpress.XtraReports.UI.XRSummary();
            DevExpress.XtraReports.UI.XRSummary xrSummary102 = new DevExpress.XtraReports.UI.XRSummary();
            this.sqlDataSource1 = new DevExpress.DataAccess.Sql.SqlDataSource(this.components);
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.Year = new DevExpress.XtraReports.Parameters.Parameter();
            this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
            this.xrLabel15 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel18 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel4 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrTable2 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell13 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell14 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell15 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell16 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell17 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell18 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell19 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell20 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell21 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell22 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell23 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell24 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell25 = new DevExpress.XtraReports.UI.XRTableCell();
            this.DetailReport = new DevExpress.XtraReports.UI.DetailReportBand();
            this.Detail1 = new DevExpress.XtraReports.UI.DetailBand();
            this.GroupHeader1 = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.xrTable20 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow14 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell181 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrLabel6 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrTable9 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow20 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell251 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell264 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell265 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell266 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell267 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell268 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell269 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell270 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell271 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell272 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell273 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell274 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell275 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrLabel5 = new DevExpress.XtraReports.UI.XRLabel();
            this.DetailReport2 = new DevExpress.XtraReports.UI.DetailReportBand();
            this.Detail3 = new DevExpress.XtraReports.UI.DetailBand();
            this.GroupFooter2 = new DevExpress.XtraReports.UI.GroupFooterBand();
            this.xrTable16 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow26 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell342 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell343 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell344 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell345 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell346 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell347 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell348 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell349 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell350 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell351 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell352 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell353 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell354 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTable15 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow25 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell327 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell328 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell329 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell330 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell331 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell332 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell333 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell334 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell335 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell336 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell337 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell338 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell339 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrLabel9 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrTable11 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow22 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell81 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell289 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell290 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell291 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell292 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell293 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell294 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell295 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell296 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell297 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell298 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell299 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell300 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTable3 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow3 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell29 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell30 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell31 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell32 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell33 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell34 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell35 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell36 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell37 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell38 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell39 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell40 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell41 = new DevExpress.XtraReports.UI.XRTableCell();
            this.DetailReport3 = new DevExpress.XtraReports.UI.DetailReportBand();
            this.Detail4 = new DevExpress.XtraReports.UI.DetailBand();
            this.xrTable7 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow8 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell95 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell96 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell97 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell98 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell99 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell100 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell101 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell102 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell103 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell104 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell105 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell106 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell107 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell108 = new DevExpress.XtraReports.UI.XRTableCell();
            this.GroupFooter4 = new DevExpress.XtraReports.UI.GroupFooterBand();
            this.xrTable21 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow15 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell179 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTable8 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow9 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell109 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell110 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell111 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell112 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell113 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell114 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell115 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell116 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell117 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell118 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell119 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell120 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell121 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell122 = new DevExpress.XtraReports.UI.XRTableCell();
            this.DetailReport4 = new DevExpress.XtraReports.UI.DetailReportBand();
            this.Detail5 = new DevExpress.XtraReports.UI.DetailBand();
            this.xrTable13 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow10 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell123 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell124 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell125 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell126 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell127 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell128 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell129 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell130 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell131 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell132 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell133 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell134 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell135 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell136 = new DevExpress.XtraReports.UI.XRTableCell();
            this.GroupFooter5 = new DevExpress.XtraReports.UI.GroupFooterBand();
            this.xrTable17 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow11 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell137 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell138 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell139 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell140 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell141 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell142 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell143 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell144 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell145 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell146 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell147 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell148 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell149 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell150 = new DevExpress.XtraReports.UI.XRTableCell();
            this.DetailReport5 = new DevExpress.XtraReports.UI.DetailReportBand();
            this.Detail6 = new DevExpress.XtraReports.UI.DetailBand();
            this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell4 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell5 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell6 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell7 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell8 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell9 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell10 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell11 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell12 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell26 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell27 = new DevExpress.XtraReports.UI.XRTableCell();
            this.GroupHeader3 = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.xrTable22 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow16 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell180 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrLabel8 = new DevExpress.XtraReports.UI.XRLabel();
            this.GroupFooter6 = new DevExpress.XtraReports.UI.GroupFooterBand();
            this.xrTable23 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow17 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell182 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTable18 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow12 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell151 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell152 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell153 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell154 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell155 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell156 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell157 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell158 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell159 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell160 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell161 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell162 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell163 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell164 = new DevExpress.XtraReports.UI.XRTableCell();
            this.DetailReport6 = new DevExpress.XtraReports.UI.DetailReportBand();
            this.Detail7 = new DevExpress.XtraReports.UI.DetailBand();
            this.xrTable19 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow13 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell165 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell166 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell167 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell168 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell169 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell170 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell171 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell172 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell173 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell174 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell175 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell176 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell177 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell178 = new DevExpress.XtraReports.UI.XRTableCell();
            this.GroupFooter7 = new DevExpress.XtraReports.UI.GroupFooterBand();
            this.xrTable4 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow4 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell28 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell42 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell43 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell44 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell45 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell46 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell47 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell48 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell49 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell50 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell51 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell52 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell53 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell54 = new DevExpress.XtraReports.UI.XRTableCell();
            this.DetailReport1 = new DevExpress.XtraReports.UI.DetailReportBand();
            this.Detail2 = new DevExpress.XtraReports.UI.DetailBand();
            this.GroupFooter3 = new DevExpress.XtraReports.UI.GroupFooterBand();
            this.xrTable5 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow5 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell55 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell56 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell57 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell58 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell59 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell60 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell61 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell62 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell63 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell64 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell65 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell66 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell67 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTable6 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow6 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell68 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell69 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell70 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell71 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell72 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell73 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell74 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell75 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell76 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell77 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell78 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell79 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell80 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTable10 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow21 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell276 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell277 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell278 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell279 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell280 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell281 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell282 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell283 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell284 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell285 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell286 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell287 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell288 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTable12 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow23 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell301 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell302 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell303 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell304 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell305 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell306 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell307 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell308 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell309 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell310 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell311 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell312 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell313 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel7 = new DevExpress.XtraReports.UI.XRLabel();
            this.calculatedField1 = new DevExpress.XtraReports.UI.CalculatedField();
            this.calculatedField2 = new DevExpress.XtraReports.UI.CalculatedField();
            this.calculatedField3 = new DevExpress.XtraReports.UI.CalculatedField();
            this.calculatedField4 = new DevExpress.XtraReports.UI.CalculatedField();
            this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
            this.formattingRule1 = new DevExpress.XtraReports.UI.FormattingRule();
            this.calculatedField5 = new DevExpress.XtraReports.UI.CalculatedField();
            this.ReportFooter1 = new DevExpress.XtraReports.UI.ReportFooterBand();
            this.xrTable14 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow27 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell355 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell356 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell357 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell358 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell359 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell360 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell361 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell362 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell363 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell364 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell365 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell366 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell367 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell368 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow28 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell369 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell370 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell371 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell372 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell373 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell374 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell375 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell376 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell377 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell378 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell379 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell380 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell381 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell382 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow29 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell383 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell384 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell385 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell386 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell387 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell388 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell389 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell390 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell391 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell392 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell393 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell394 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell395 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell396 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow7 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell82 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell83 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell84 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell85 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell86 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell87 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell88 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell89 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell90 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell91 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell92 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell93 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell94 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell314 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow24 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell315 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell316 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell317 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell318 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell319 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell320 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell321 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell322 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell323 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell324 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell325 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell326 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell340 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell341 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow30 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell397 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell398 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell399 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell400 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell401 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell402 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell403 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell404 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell405 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell406 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell407 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell408 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell409 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell410 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow31 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell411 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell412 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell413 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell414 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell415 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell416 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell417 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell418 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell419 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell420 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell421 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell422 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell423 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell424 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow32 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell425 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell426 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell427 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell428 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell429 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell430 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell431 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell432 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell433 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell434 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell435 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell436 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell437 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell438 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow33 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell439 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell440 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell441 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell442 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell443 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell444 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell445 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell446 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell447 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell448 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell449 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell450 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell451 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell452 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow34 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell453 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell454 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell455 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell456 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell457 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell458 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell459 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell460 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell461 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell462 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell463 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell464 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell465 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell466 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow35 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell467 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell468 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell469 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell470 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell471 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell472 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell473 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell474 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell475 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell476 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell477 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell478 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell479 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell480 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow36 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell481 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell482 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell483 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell484 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell485 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell486 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell487 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell488 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell489 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell490 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell491 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell492 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell493 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell494 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow37 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell495 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell496 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell497 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell498 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell499 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell500 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell501 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell502 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell503 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell504 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell505 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell506 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell507 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell508 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow38 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell509 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell510 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell511 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell512 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell513 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell514 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell515 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell516 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell517 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell518 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell519 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell520 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell521 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell522 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableRow39 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell523 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell524 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell525 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell526 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell527 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell528 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell529 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell530 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell531 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell532 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell533 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell534 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell535 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell536 = new DevExpress.XtraReports.UI.XRTableCell();
            this.GroupFooter1 = new DevExpress.XtraReports.UI.GroupFooterBand();
            this.xrLabel10 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel3 = new DevExpress.XtraReports.UI.XRLabel();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable20)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable16)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable15)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable21)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable13)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable17)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable22)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable23)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable18)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable19)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable12)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable14)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // sqlDataSource1
            // 
            this.sqlDataSource1.ConnectionName = "GEARS-METSITConnectionString";
            this.sqlDataSource1.Name = "sqlDataSource1";
            customSqlQuery1.Name = "YearValue";
            customSqlQuery1.Sql = "SELECT Year\r\n  from (SELECT Year FROM Accounting.GLTable GROUP BY Year UNION ALL\r" +
    "\n       SELECT Value FROM IT.SystemSettings WHERE (Code = \'CYEAR\')) t\r\n       GR" +
    "OUP BY Year ORDER BY Year DESC\r\n";
            customSqlQuery2.Name = "DefaultYear";
            customSqlQuery2.Sql = "SELECT CONVERT (int, Value) as Year FROM IT.SystemSettings WHERE\r\n       Code = \'" +
    "CYEAR\'\r\n";
            customSqlQuery3.Name = "CompName";
            customSqlQuery3.Sql = "SELECT Value FROM IT.SYSTEMSETTINGS\r\nWHERE CODE=\'COMPNAME\'\r\n";
            storedProcQuery1.Name = "sp_report_ContributionMargin";
            queryParameter1.Name = "@YearDate";
            queryParameter1.Type = typeof(DevExpress.DataAccess.Expression);
            queryParameter1.Value = new DevExpress.DataAccess.Expression("[Parameters.Year]", typeof(string));
            storedProcQuery1.Parameters.Add(queryParameter1);
            storedProcQuery1.StoredProcName = "sp_report_ContributionMarginIS";
            storedProcQuery2.Name = "sp_report_ContributionMarginDetail1";
            queryParameter2.Name = "@YearDate";
            queryParameter2.Type = typeof(DevExpress.DataAccess.Expression);
            queryParameter2.Value = new DevExpress.DataAccess.Expression("[Parameters.Year]", typeof(string));
            queryParameter3.Name = "@ReportType";
            queryParameter3.Type = typeof(DevExpress.DataAccess.Expression);
            queryParameter3.Value = new DevExpress.DataAccess.Expression("\'HEADER\'", typeof(string));
            storedProcQuery2.Parameters.Add(queryParameter2);
            storedProcQuery2.Parameters.Add(queryParameter3);
            storedProcQuery2.StoredProcName = "sp_report_ContributionMarginDetail1";
            storedProcQuery3.Name = "sp_report_ContributionMarginDetail2";
            queryParameter4.Name = "@YearDate";
            queryParameter4.Type = typeof(DevExpress.DataAccess.Expression);
            queryParameter4.Value = new DevExpress.DataAccess.Expression("[Parameters.Year]", typeof(string));
            queryParameter5.Name = "@ReportType";
            queryParameter5.Type = typeof(DevExpress.DataAccess.Expression);
            queryParameter5.Value = new DevExpress.DataAccess.Expression("\'HEADER\'", typeof(string));
            storedProcQuery3.Parameters.Add(queryParameter4);
            storedProcQuery3.Parameters.Add(queryParameter5);
            storedProcQuery3.StoredProcName = "sp_report_ContributionMarginDetail2";
            storedProcQuery4.Name = "sp_report_ContributionMarginDetailAcnt";
            queryParameter6.Name = "@YearDate";
            queryParameter6.Type = typeof(DevExpress.DataAccess.Expression);
            queryParameter6.Value = new DevExpress.DataAccess.Expression("[Parameters.Year]", typeof(string));
            queryParameter7.Name = "@ReportType";
            queryParameter7.Type = typeof(string);
            queryParameter7.ValueInfo = "DETAIL";
            storedProcQuery4.Parameters.Add(queryParameter6);
            storedProcQuery4.Parameters.Add(queryParameter7);
            storedProcQuery4.StoredProcName = "sp_report_ContributionMarginDetailAcnt";
            storedProcQuery5.Name = "sp_report_ContributionMarginDetailAcnt1";
            queryParameter8.Name = "@YearDate";
            queryParameter8.Type = typeof(DevExpress.DataAccess.Expression);
            queryParameter8.Value = new DevExpress.DataAccess.Expression("[Parameters.Year]", typeof(string));
            queryParameter9.Name = "@ReportType";
            queryParameter9.Type = typeof(string);
            queryParameter9.ValueInfo = "DETAIL";
            storedProcQuery5.Parameters.Add(queryParameter8);
            storedProcQuery5.Parameters.Add(queryParameter9);
            storedProcQuery5.StoredProcName = "sp_report_ContributionMarginDetailAcnt1";
            storedProcQuery6.Name = "sp_report_ContributionMarginDetailAcnt2";
            queryParameter10.Name = "@YearDate";
            queryParameter10.Type = typeof(DevExpress.DataAccess.Expression);
            queryParameter10.Value = new DevExpress.DataAccess.Expression("[Parameters.Year]", typeof(string));
            queryParameter11.Name = "@ReportType";
            queryParameter11.Type = typeof(string);
            queryParameter11.ValueInfo = "DETAIL";
            storedProcQuery6.Parameters.Add(queryParameter10);
            storedProcQuery6.Parameters.Add(queryParameter11);
            storedProcQuery6.StoredProcName = "sp_report_ContributionMarginDetailAcnt2";
            storedProcQuery7.Name = "sp_report_ContributionMarginDetailAcnt3";
            queryParameter12.Name = "@YearDate";
            queryParameter12.Type = typeof(DevExpress.DataAccess.Expression);
            queryParameter12.Value = new DevExpress.DataAccess.Expression("[Parameters.Year]", typeof(string));
            queryParameter13.Name = "@ReportType";
            queryParameter13.Type = typeof(string);
            queryParameter13.ValueInfo = "DETAIL";
            storedProcQuery7.Parameters.Add(queryParameter12);
            storedProcQuery7.Parameters.Add(queryParameter13);
            storedProcQuery7.StoredProcName = "sp_report_ContributionMarginDetailAcnt";
            this.sqlDataSource1.Queries.AddRange(new DevExpress.DataAccess.Sql.SqlQuery[] {
            customSqlQuery1,
            customSqlQuery2,
            customSqlQuery3,
            storedProcQuery1,
            storedProcQuery2,
            storedProcQuery3,
            storedProcQuery4,
            storedProcQuery5,
            storedProcQuery6,
            storedProcQuery7});
            this.sqlDataSource1.ResultSchemaSerializable = resources.GetString("sqlDataSource1.ResultSchemaSerializable");
            // 
            // Detail
            // 
            this.Detail.Dpi = 100F;
            this.Detail.Expanded = false;
            this.Detail.HeightF = 0F;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // TopMargin
            // 
            this.TopMargin.Dpi = 100F;
            this.TopMargin.HeightF = 0F;
            this.TopMargin.Name = "TopMargin";
            this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // BottomMargin
            // 
            this.BottomMargin.Dpi = 100F;
            this.BottomMargin.HeightF = 33F;
            this.BottomMargin.Name = "BottomMargin";
            this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // Year
            // 
            this.Year.Description = "Year";
            dynamicListLookUpSettings1.DataAdapter = null;
            dynamicListLookUpSettings1.DataMember = "YearValue";
            dynamicListLookUpSettings1.DataSource = this.sqlDataSource1;
            dynamicListLookUpSettings1.DisplayMember = "Year";
            dynamicListLookUpSettings1.ValueMember = "Year";
            this.Year.LookUpSettings = dynamicListLookUpSettings1;
            this.Year.Name = "Year";
            // 
            // ReportHeader
            // 
            this.ReportHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel15,
            this.xrLabel18,
            this.xrLabel4});
            this.ReportHeader.Dpi = 100F;
            this.ReportHeader.HeightF = 83.82848F;
            this.ReportHeader.Name = "ReportHeader";
            // 
            // xrLabel15
            // 
            this.xrLabel15.Dpi = 100F;
            this.xrLabel15.Font = new System.Drawing.Font("Arial Narrow", 13F, System.Drawing.FontStyle.Bold);
            this.xrLabel15.LocationFloat = new DevExpress.Utils.PointFloat(171.5001F, 22.99999F);
            this.xrLabel15.Name = "xrLabel15";
            this.xrLabel15.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel15.SizeF = new System.Drawing.SizeF(1060F, 23.00002F);
            this.xrLabel15.StylePriority.UseFont = false;
            this.xrLabel15.StylePriority.UseTextAlignment = false;
            this.xrLabel15.Text = "Contribution Margin Income Statement";
            this.xrLabel15.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel18
            // 
            this.xrLabel18.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "CompName.Value")});
            this.xrLabel18.Dpi = 100F;
            this.xrLabel18.Font = new System.Drawing.Font("Arial Narrow", 9.75F);
            this.xrLabel18.LocationFloat = new DevExpress.Utils.PointFloat(171.4999F, 0F);
            this.xrLabel18.Name = "xrLabel18";
            this.xrLabel18.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel18.SizeF = new System.Drawing.SizeF(1060F, 23.00002F);
            this.xrLabel18.StylePriority.UseFont = false;
            this.xrLabel18.StylePriority.UseTextAlignment = false;
            this.xrLabel18.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel4
            // 
            this.xrLabel4.Dpi = 100F;
            this.xrLabel4.Font = new System.Drawing.Font("Arial Narrow", 8F, System.Drawing.FontStyle.Bold);
            this.xrLabel4.LocationFloat = new DevExpress.Utils.PointFloat(171.5001F, 46.00007F);
            this.xrLabel4.Name = "xrLabel4";
            this.xrLabel4.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel4.Scripts.OnBeforePrint = "xrLabel1_BeforePrint";
            this.xrLabel4.SizeF = new System.Drawing.SizeF(1060F, 21.16175F);
            this.xrLabel4.StylePriority.UseFont = false;
            this.xrLabel4.StylePriority.UsePadding = false;
            this.xrLabel4.StylePriority.UseTextAlignment = false;
            this.xrLabel4.Text = "For The Year [Parameters.Year]";
            this.xrLabel4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrTable2
            // 
            this.xrTable2.Dpi = 100F;
            this.xrTable2.LocationFloat = new DevExpress.Utils.PointFloat(331.8087F, 0F);
            this.xrTable2.Name = "xrTable2";
            this.xrTable2.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow2});
            this.xrTable2.SizeF = new System.Drawing.SizeF(1071.191F, 25F);
            // 
            // xrTableRow2
            // 
            this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell13,
            this.xrTableCell14,
            this.xrTableCell15,
            this.xrTableCell16,
            this.xrTableCell17,
            this.xrTableCell18,
            this.xrTableCell19,
            this.xrTableCell20,
            this.xrTableCell21,
            this.xrTableCell22,
            this.xrTableCell23,
            this.xrTableCell24,
            this.xrTableCell25});
            this.xrTableRow2.Dpi = 100F;
            this.xrTableRow2.Name = "xrTableRow2";
            this.xrTableRow2.Weight = 1D;
            // 
            // xrTableCell13
            // 
            this.xrTableCell13.Dpi = 100F;
            this.xrTableCell13.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell13.Name = "xrTableCell13";
            this.xrTableCell13.StylePriority.UseFont = false;
            this.xrTableCell13.StylePriority.UseTextAlignment = false;
            this.xrTableCell13.Text = "JAN";
            this.xrTableCell13.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell13.Weight = 0.68180451750533344D;
            // 
            // xrTableCell14
            // 
            this.xrTableCell14.Dpi = 100F;
            this.xrTableCell14.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell14.Name = "xrTableCell14";
            this.xrTableCell14.StylePriority.UseFont = false;
            this.xrTableCell14.StylePriority.UseTextAlignment = false;
            this.xrTableCell14.Text = "FEB";
            this.xrTableCell14.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell14.Weight = 0.681797781908337D;
            // 
            // xrTableCell15
            // 
            this.xrTableCell15.Dpi = 100F;
            this.xrTableCell15.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell15.Name = "xrTableCell15";
            this.xrTableCell15.StylePriority.UseFont = false;
            this.xrTableCell15.StylePriority.UseTextAlignment = false;
            this.xrTableCell15.Text = "MAR";
            this.xrTableCell15.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell15.Weight = 0.6817969594139961D;
            // 
            // xrTableCell16
            // 
            this.xrTableCell16.Dpi = 100F;
            this.xrTableCell16.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell16.Name = "xrTableCell16";
            this.xrTableCell16.StylePriority.UseFont = false;
            this.xrTableCell16.StylePriority.UseTextAlignment = false;
            this.xrTableCell16.Text = "APR";
            this.xrTableCell16.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell16.Weight = 0.68179778647176648D;
            // 
            // xrTableCell17
            // 
            this.xrTableCell17.Dpi = 100F;
            this.xrTableCell17.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell17.Name = "xrTableCell17";
            this.xrTableCell17.StylePriority.UseFont = false;
            this.xrTableCell17.StylePriority.UseTextAlignment = false;
            this.xrTableCell17.Text = "MAY";
            this.xrTableCell17.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell17.Weight = 0.68178907790117593D;
            // 
            // xrTableCell18
            // 
            this.xrTableCell18.Dpi = 100F;
            this.xrTableCell18.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell18.Name = "xrTableCell18";
            this.xrTableCell18.StylePriority.UseFont = false;
            this.xrTableCell18.StylePriority.UseTextAlignment = false;
            this.xrTableCell18.Text = "JUN";
            this.xrTableCell18.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell18.Weight = 0.68179777456421919D;
            // 
            // xrTableCell19
            // 
            this.xrTableCell19.Dpi = 100F;
            this.xrTableCell19.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell19.Name = "xrTableCell19";
            this.xrTableCell19.StylePriority.UseFont = false;
            this.xrTableCell19.StylePriority.UseTextAlignment = false;
            this.xrTableCell19.Text = "JUL";
            this.xrTableCell19.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell19.Weight = 0.68179614681772349D;
            // 
            // xrTableCell20
            // 
            this.xrTableCell20.Dpi = 100F;
            this.xrTableCell20.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell20.Name = "xrTableCell20";
            this.xrTableCell20.StylePriority.UseFont = false;
            this.xrTableCell20.StylePriority.UseTextAlignment = false;
            this.xrTableCell20.Text = "AUG";
            this.xrTableCell20.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell20.Weight = 0.681799406155517D;
            // 
            // xrTableCell21
            // 
            this.xrTableCell21.Dpi = 100F;
            this.xrTableCell21.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell21.Name = "xrTableCell21";
            this.xrTableCell21.StylePriority.UseFont = false;
            this.xrTableCell21.StylePriority.UseTextAlignment = false;
            this.xrTableCell21.Text = "SEP";
            this.xrTableCell21.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell21.Weight = 0.68215592814643888D;
            // 
            // xrTableCell22
            // 
            this.xrTableCell22.Dpi = 100F;
            this.xrTableCell22.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell22.Name = "xrTableCell22";
            this.xrTableCell22.StylePriority.UseFont = false;
            this.xrTableCell22.StylePriority.UseTextAlignment = false;
            this.xrTableCell22.Text = "OCT";
            this.xrTableCell22.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell22.Weight = 0.68144134210106366D;
            // 
            // xrTableCell23
            // 
            this.xrTableCell23.Dpi = 100F;
            this.xrTableCell23.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell23.Name = "xrTableCell23";
            this.xrTableCell23.StylePriority.UseFont = false;
            this.xrTableCell23.StylePriority.UseTextAlignment = false;
            this.xrTableCell23.Text = "NOV";
            this.xrTableCell23.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell23.Weight = 0.68170473019852751D;
            // 
            // xrTableCell24
            // 
            this.xrTableCell24.Dpi = 100F;
            this.xrTableCell24.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell24.Name = "xrTableCell24";
            this.xrTableCell24.StylePriority.UseFont = false;
            this.xrTableCell24.StylePriority.UseTextAlignment = false;
            this.xrTableCell24.Text = "DEC";
            this.xrTableCell24.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell24.Weight = 0.681837839286845D;
            // 
            // xrTableCell25
            // 
            this.xrTableCell25.Dpi = 100F;
            this.xrTableCell25.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell25.Name = "xrTableCell25";
            this.xrTableCell25.StylePriority.UseFont = false;
            this.xrTableCell25.StylePriority.UseTextAlignment = false;
            this.xrTableCell25.Text = "Total";
            this.xrTableCell25.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTableCell25.Weight = 0.6818379779022129D;
            // 
            // DetailReport
            // 
            this.DetailReport.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail1,
            this.GroupHeader1,
            this.DetailReport2,
            this.DetailReport3,
            this.DetailReport4,
            this.DetailReport5,
            this.DetailReport6});
            this.DetailReport.DataMember = "sp_report_ContributionMargin";
            this.DetailReport.DataSource = this.sqlDataSource1;
            this.DetailReport.Dpi = 100F;
            this.DetailReport.Level = 0;
            this.DetailReport.Name = "DetailReport";
            // 
            // Detail1
            // 
            this.Detail1.Dpi = 100F;
            this.Detail1.Expanded = false;
            this.Detail1.HeightF = 15F;
            this.Detail1.Name = "Detail1";
            // 
            // GroupHeader1
            // 
            this.GroupHeader1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable20,
            this.xrLabel6,
            this.xrTable9,
            this.xrLabel5});
            this.GroupHeader1.Dpi = 100F;
            this.GroupHeader1.GroupFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("Row", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});
            this.GroupHeader1.HeightF = 45.25004F;
            this.GroupHeader1.Name = "GroupHeader1";
            // 
            // xrTable20
            // 
            this.xrTable20.Dpi = 100F;
            this.xrTable20.LocationFloat = new DevExpress.Utils.PointFloat(10.00404F, 30.87502F);
            this.xrTable20.Name = "xrTable20";
            this.xrTable20.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow14});
            this.xrTable20.SizeF = new System.Drawing.SizeF(100F, 14.37502F);
            // 
            // xrTableRow14
            // 
            this.xrTableRow14.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell181});
            this.xrTableRow14.Dpi = 100F;
            this.xrTableRow14.Name = "xrTableRow14";
            this.xrTableRow14.Weight = 1D;
            // 
            // xrTableCell181
            // 
            this.xrTableCell181.Dpi = 100F;
            this.xrTableCell181.Font = new System.Drawing.Font("Arial", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell181.Name = "xrTableCell181";
            this.xrTableCell181.StylePriority.UseFont = false;
            this.xrTableCell181.Text = "Cost of Good Sold";
            this.xrTableCell181.Weight = 1D;
            // 
            // xrLabel6
            // 
            this.xrLabel6.Dpi = 100F;
            this.xrLabel6.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrLabel6.LocationFloat = new DevExpress.Utils.PointFloat(0F, 15.87501F);
            this.xrLabel6.Name = "xrLabel6";
            this.xrLabel6.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel6.SizeF = new System.Drawing.SizeF(331.8086F, 15F);
            this.xrLabel6.StylePriority.UseFont = false;
            this.xrLabel6.StylePriority.UseTextAlignment = false;
            this.xrLabel6.Text = "Less: Variable Cost";
            this.xrLabel6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrTable9
            // 
            this.xrTable9.Dpi = 100F;
            this.xrTable9.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTable9.LocationFloat = new DevExpress.Utils.PointFloat(331.8085F, 0.875028F);
            this.xrTable9.Name = "xrTable9";
            this.xrTable9.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow20});
            this.xrTable9.SizeF = new System.Drawing.SizeF(1071.192F, 15F);
            this.xrTable9.StylePriority.UseFont = false;
            // 
            // xrTableRow20
            // 
            this.xrTableRow20.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell251,
            this.xrTableCell264,
            this.xrTableCell265,
            this.xrTableCell266,
            this.xrTableCell267,
            this.xrTableCell268,
            this.xrTableCell269,
            this.xrTableCell270,
            this.xrTableCell271,
            this.xrTableCell272,
            this.xrTableCell273,
            this.xrTableCell274,
            this.xrTableCell275});
            this.xrTableRow20.Dpi = 100F;
            this.xrTableRow20.Name = "xrTableRow20";
            this.xrTableRow20.Weight = 11.5D;
            // 
            // xrTableCell251
            // 
            this.xrTableCell251.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMargin.Amount_01")});
            this.xrTableCell251.Dpi = 100F;
            this.xrTableCell251.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell251.Name = "xrTableCell251";
            this.xrTableCell251.StylePriority.UseFont = false;
            this.xrTableCell251.StylePriority.UseTextAlignment = false;
            xrSummary1.FormatString = "{0:n}";
            xrSummary1.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            this.xrTableCell251.Summary = xrSummary1;
            this.xrTableCell251.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell251.Weight = 0.014973887748189205D;
            // 
            // xrTableCell264
            // 
            this.xrTableCell264.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMargin.Amount_02")});
            this.xrTableCell264.Dpi = 100F;
            this.xrTableCell264.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell264.Name = "xrTableCell264";
            this.xrTableCell264.StylePriority.UseFont = false;
            this.xrTableCell264.StylePriority.UseTextAlignment = false;
            xrSummary2.FormatString = "{0:n}";
            xrSummary2.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            this.xrTableCell264.Summary = xrSummary2;
            this.xrTableCell264.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell264.Weight = 0.014973689631148673D;
            // 
            // xrTableCell265
            // 
            this.xrTableCell265.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMargin.Amount_03")});
            this.xrTableCell265.Dpi = 100F;
            this.xrTableCell265.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell265.Name = "xrTableCell265";
            this.xrTableCell265.StylePriority.UseFont = false;
            this.xrTableCell265.StylePriority.UseTextAlignment = false;
            xrSummary3.FormatString = "{0:n}";
            xrSummary3.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            this.xrTableCell265.Summary = xrSummary3;
            this.xrTableCell265.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell265.Weight = 0.014973689631148673D;
            // 
            // xrTableCell266
            // 
            this.xrTableCell266.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMargin.Amount_04")});
            this.xrTableCell266.Dpi = 100F;
            this.xrTableCell266.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell266.Name = "xrTableCell266";
            this.xrTableCell266.StylePriority.UseFont = false;
            this.xrTableCell266.StylePriority.UseTextAlignment = false;
            xrSummary4.FormatString = "{0:n}";
            xrSummary4.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            this.xrTableCell266.Summary = xrSummary4;
            this.xrTableCell266.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell266.Weight = 0.014973689631148673D;
            // 
            // xrTableCell267
            // 
            this.xrTableCell267.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMargin.Amount_05")});
            this.xrTableCell267.Dpi = 100F;
            this.xrTableCell267.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell267.Name = "xrTableCell267";
            this.xrTableCell267.StylePriority.UseFont = false;
            this.xrTableCell267.StylePriority.UseTextAlignment = false;
            xrSummary5.FormatString = "{0:n}";
            xrSummary5.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            this.xrTableCell267.Summary = xrSummary5;
            this.xrTableCell267.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell267.Weight = 0.014973689631148673D;
            // 
            // xrTableCell268
            // 
            this.xrTableCell268.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMargin.Amount_06")});
            this.xrTableCell268.Dpi = 100F;
            this.xrTableCell268.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell268.Name = "xrTableCell268";
            this.xrTableCell268.StylePriority.UseFont = false;
            this.xrTableCell268.StylePriority.UseTextAlignment = false;
            xrSummary6.FormatString = "{0:n}";
            xrSummary6.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            this.xrTableCell268.Summary = xrSummary6;
            this.xrTableCell268.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell268.Weight = 0.014973689631148673D;
            // 
            // xrTableCell269
            // 
            this.xrTableCell269.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMargin.Amount_07")});
            this.xrTableCell269.Dpi = 100F;
            this.xrTableCell269.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell269.Name = "xrTableCell269";
            this.xrTableCell269.StylePriority.UseFont = false;
            this.xrTableCell269.StylePriority.UseTextAlignment = false;
            xrSummary7.FormatString = "{0:n}";
            xrSummary7.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            this.xrTableCell269.Summary = xrSummary7;
            this.xrTableCell269.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell269.Weight = 0.014973689631148673D;
            // 
            // xrTableCell270
            // 
            this.xrTableCell270.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMargin.Amount_08")});
            this.xrTableCell270.Dpi = 100F;
            this.xrTableCell270.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell270.Name = "xrTableCell270";
            this.xrTableCell270.StylePriority.UseFont = false;
            this.xrTableCell270.StylePriority.UseTextAlignment = false;
            xrSummary8.FormatString = "{0:n}";
            xrSummary8.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            this.xrTableCell270.Summary = xrSummary8;
            this.xrTableCell270.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell270.Weight = 0.014973689631148673D;
            // 
            // xrTableCell271
            // 
            this.xrTableCell271.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMargin.Amount_09")});
            this.xrTableCell271.Dpi = 100F;
            this.xrTableCell271.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell271.Name = "xrTableCell271";
            this.xrTableCell271.StylePriority.UseFont = false;
            this.xrTableCell271.StylePriority.UseTextAlignment = false;
            xrSummary9.FormatString = "{0:n}";
            xrSummary9.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            this.xrTableCell271.Summary = xrSummary9;
            this.xrTableCell271.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell271.Weight = 0.014981511803462308D;
            // 
            // xrTableCell272
            // 
            this.xrTableCell272.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMargin.Amount_10")});
            this.xrTableCell272.Dpi = 100F;
            this.xrTableCell272.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell272.Name = "xrTableCell272";
            this.xrTableCell272.StylePriority.UseFont = false;
            this.xrTableCell272.StylePriority.UseTextAlignment = false;
            xrSummary10.FormatString = "{0:n}";
            xrSummary10.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            this.xrTableCell272.Summary = xrSummary10;
            this.xrTableCell272.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell272.Weight = 0.014965867458835038D;
            // 
            // xrTableCell273
            // 
            this.xrTableCell273.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMargin.Amount_11")});
            this.xrTableCell273.Dpi = 100F;
            this.xrTableCell273.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell273.Name = "xrTableCell273";
            this.xrTableCell273.StylePriority.UseFont = false;
            this.xrTableCell273.StylePriority.UseTextAlignment = false;
            xrSummary11.FormatString = "{0:n}";
            xrSummary11.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            this.xrTableCell273.Summary = xrSummary11;
            this.xrTableCell273.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell273.Weight = 0.014971685899902717D;
            // 
            // xrTableCell274
            // 
            this.xrTableCell274.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMargin.Amount_12")});
            this.xrTableCell274.Dpi = 100F;
            this.xrTableCell274.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell274.Name = "xrTableCell274";
            this.xrTableCell274.StylePriority.UseFont = false;
            this.xrTableCell274.StylePriority.UseTextAlignment = false;
            xrSummary12.FormatString = "{0:n}";
            xrSummary12.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            this.xrTableCell274.Summary = xrSummary12;
            this.xrTableCell274.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell274.Weight = 0.014974533857959983D;
            // 
            // xrTableCell275
            // 
            this.xrTableCell275.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMargin.Amount")});
            this.xrTableCell275.Dpi = 100F;
            this.xrTableCell275.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell275.Name = "xrTableCell275";
            this.xrTableCell275.StylePriority.UseFont = false;
            this.xrTableCell275.StylePriority.UseTextAlignment = false;
            xrSummary13.FormatString = "{0:n}";
            xrSummary13.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            this.xrTableCell275.Summary = xrSummary13;
            this.xrTableCell275.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell275.Weight = 0.014974849135583321D;
            // 
            // xrLabel5
            // 
            this.xrLabel5.Dpi = 100F;
            this.xrLabel5.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrLabel5.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0.8750095F);
            this.xrLabel5.Name = "xrLabel5";
            this.xrLabel5.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel5.SizeF = new System.Drawing.SizeF(331.8086F, 15F);
            this.xrLabel5.StylePriority.UseFont = false;
            this.xrLabel5.StylePriority.UseTextAlignment = false;
            this.xrLabel5.Text = "Sales";
            this.xrLabel5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // DetailReport2
            // 
            this.DetailReport2.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail3,
            this.GroupFooter2});
            this.DetailReport2.DataMember = "sp_report_ContributionMarginDetail1";
            this.DetailReport2.DataSource = this.sqlDataSource1;
            this.DetailReport2.Dpi = 100F;
            this.DetailReport2.Level = 2;
            this.DetailReport2.Name = "DetailReport2";
            // 
            // Detail3
            // 
            this.Detail3.Dpi = 100F;
            this.Detail3.Expanded = false;
            this.Detail3.HeightF = 15F;
            this.Detail3.Name = "Detail3";
            // 
            // GroupFooter2
            // 
            this.GroupFooter2.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable16,
            this.xrTable15,
            this.xrLabel9,
            this.xrLabel1,
            this.xrTable11,
            this.xrTable3});
            this.GroupFooter2.Dpi = 100F;
            this.GroupFooter2.HeightF = 90.41666F;
            this.GroupFooter2.Name = "GroupFooter2";
            // 
            // xrTable16
            // 
            this.xrTable16.Dpi = 100F;
            this.xrTable16.LocationFloat = new DevExpress.Utils.PointFloat(331.8079F, 44.99998F);
            this.xrTable16.Name = "xrTable16";
            this.xrTable16.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow26});
            this.xrTable16.SizeF = new System.Drawing.SizeF(1071.193F, 15F);
            // 
            // xrTableRow26
            // 
            this.xrTableRow26.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell342,
            this.xrTableCell343,
            this.xrTableCell344,
            this.xrTableCell345,
            this.xrTableCell346,
            this.xrTableCell347,
            this.xrTableCell348,
            this.xrTableCell349,
            this.xrTableCell350,
            this.xrTableCell351,
            this.xrTableCell352,
            this.xrTableCell353,
            this.xrTableCell354});
            this.xrTableRow26.Dpi = 100F;
            this.xrTableRow26.Name = "xrTableRow26";
            this.xrTableRow26.Weight = 11.5D;
            // 
            // xrTableCell342
            // 
            this.xrTableCell342.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail1.TotalMargin1", "{0:n}")});
            this.xrTableCell342.Dpi = 100F;
            this.xrTableCell342.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell342.Name = "xrTableCell342";
            this.xrTableCell342.StylePriority.UseFont = false;
            this.xrTableCell342.StylePriority.UseTextAlignment = false;
            this.xrTableCell342.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell342.Weight = 0.011457159151225459D;
            // 
            // xrTableCell343
            // 
            this.xrTableCell343.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail1.TotalMargin2", "{0:n}")});
            this.xrTableCell343.Dpi = 100F;
            this.xrTableCell343.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell343.Name = "xrTableCell343";
            this.xrTableCell343.StylePriority.UseFont = false;
            this.xrTableCell343.StylePriority.UseTextAlignment = false;
            this.xrTableCell343.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell343.Weight = 0.011456439447518315D;
            // 
            // xrTableCell344
            // 
            this.xrTableCell344.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail1.TotalMargin3", "{0:n}")});
            this.xrTableCell344.Dpi = 100F;
            this.xrTableCell344.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell344.Name = "xrTableCell344";
            this.xrTableCell344.StylePriority.UseFont = false;
            this.xrTableCell344.StylePriority.UseTextAlignment = false;
            this.xrTableCell344.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell344.Weight = 0.01145644711665144D;
            // 
            // xrTableCell345
            // 
            this.xrTableCell345.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail1.TotalMargin4", "{0:n}")});
            this.xrTableCell345.Dpi = 100F;
            this.xrTableCell345.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell345.Name = "xrTableCell345";
            this.xrTableCell345.StylePriority.UseFont = false;
            this.xrTableCell345.StylePriority.UseTextAlignment = false;
            this.xrTableCell345.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell345.Weight = 0.011456464340136261D;
            // 
            // xrTableCell346
            // 
            this.xrTableCell346.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail1.TotalMargin5", "{0:n}")});
            this.xrTableCell346.Dpi = 100F;
            this.xrTableCell346.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell346.Name = "xrTableCell346";
            this.xrTableCell346.StylePriority.UseFont = false;
            this.xrTableCell346.StylePriority.UseTextAlignment = false;
            this.xrTableCell346.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell346.Weight = 0.011456379748427982D;
            // 
            // xrTableCell347
            // 
            this.xrTableCell347.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail1.TotalMargin6", "{0:n}")});
            this.xrTableCell347.Dpi = 100F;
            this.xrTableCell347.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell347.Name = "xrTableCell347";
            this.xrTableCell347.StylePriority.UseFont = false;
            this.xrTableCell347.StylePriority.UseTextAlignment = false;
            this.xrTableCell347.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell347.Weight = 0.011456481623145959D;
            // 
            // xrTableCell348
            // 
            this.xrTableCell348.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail1.TotalMargin7", "{0:n}")});
            this.xrTableCell348.Dpi = 100F;
            this.xrTableCell348.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell348.Name = "xrTableCell348";
            this.xrTableCell348.StylePriority.UseFont = false;
            this.xrTableCell348.StylePriority.UseTextAlignment = false;
            this.xrTableCell348.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell348.Weight = 0.011456448104521738D;
            // 
            // xrTableCell349
            // 
            this.xrTableCell349.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail1.TotalMargin8", "{0:n}")});
            this.xrTableCell349.Dpi = 100F;
            this.xrTableCell349.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell349.Name = "xrTableCell349";
            this.xrTableCell349.StylePriority.UseFont = false;
            this.xrTableCell349.StylePriority.UseTextAlignment = false;
            this.xrTableCell349.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell349.Weight = 0.011456203908747933D;
            // 
            // xrTableCell350
            // 
            this.xrTableCell350.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail1.TotalMargin9", "{0:n}")});
            this.xrTableCell350.Dpi = 100F;
            this.xrTableCell350.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell350.Name = "xrTableCell350";
            this.xrTableCell350.StylePriority.UseFont = false;
            this.xrTableCell350.StylePriority.UseTextAlignment = false;
            this.xrTableCell350.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell350.Weight = 0.011462725416847258D;
            // 
            // xrTableCell351
            // 
            this.xrTableCell351.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail1.TotalMargin10", "{0:n}")});
            this.xrTableCell351.Dpi = 100F;
            this.xrTableCell351.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell351.Name = "xrTableCell351";
            this.xrTableCell351.StylePriority.UseFont = false;
            this.xrTableCell351.StylePriority.UseTextAlignment = false;
            this.xrTableCell351.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell351.Weight = 0.011450439867777159D;
            // 
            // xrTableCell352
            // 
            this.xrTableCell352.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail1.TotalMargin11", "{0:n}")});
            this.xrTableCell352.Dpi = 100F;
            this.xrTableCell352.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell352.Name = "xrTableCell352";
            this.xrTableCell352.StylePriority.UseFont = false;
            this.xrTableCell352.StylePriority.UseTextAlignment = false;
            this.xrTableCell352.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell352.Weight = 0.011455021519186374D;
            // 
            // xrTableCell353
            // 
            this.xrTableCell353.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail1.TotalMargin12", "{0:n}")});
            this.xrTableCell353.Dpi = 100F;
            this.xrTableCell353.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell353.Name = "xrTableCell353";
            this.xrTableCell353.StylePriority.UseFont = false;
            this.xrTableCell353.StylePriority.UseTextAlignment = false;
            xrSummary14.FormatString = "{0:#,0}";
            this.xrTableCell353.Summary = xrSummary14;
            this.xrTableCell353.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell353.Weight = 0.01145697336366436D;
            // 
            // xrTableCell354
            // 
            this.xrTableCell354.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail1.TotalMargin", "{0:n}")});
            this.xrTableCell354.Dpi = 100F;
            this.xrTableCell354.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell354.Name = "xrTableCell354";
            this.xrTableCell354.StylePriority.UseFont = false;
            this.xrTableCell354.StylePriority.UseTextAlignment = false;
            this.xrTableCell354.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell354.Weight = 0.011456859862550751D;
            // 
            // xrTable15
            // 
            this.xrTable15.Dpi = 100F;
            this.xrTable15.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTable15.LocationFloat = new DevExpress.Utils.PointFloat(331.8079F, 59.99998F);
            this.xrTable15.Name = "xrTable15";
            this.xrTable15.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow25});
            this.xrTable15.SizeF = new System.Drawing.SizeF(1071.19F, 15F);
            this.xrTable15.StylePriority.UseFont = false;
            // 
            // xrTableRow25
            // 
            this.xrTableRow25.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell327,
            this.xrTableCell328,
            this.xrTableCell329,
            this.xrTableCell330,
            this.xrTableCell331,
            this.xrTableCell332,
            this.xrTableCell333,
            this.xrTableCell334,
            this.xrTableCell335,
            this.xrTableCell336,
            this.xrTableCell337,
            this.xrTableCell338,
            this.xrTableCell339});
            this.xrTableRow25.Dpi = 100F;
            this.xrTableRow25.Name = "xrTableRow25";
            this.xrTableRow25.Weight = 11.5D;
            // 
            // xrTableCell327
            // 
            this.xrTableCell327.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail1.MarginPer1", "{0:0.00%}")});
            this.xrTableCell327.Dpi = 100F;
            this.xrTableCell327.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell327.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.xrTableCell327.Name = "xrTableCell327";
            this.xrTableCell327.StylePriority.UseFont = false;
            this.xrTableCell327.StylePriority.UseForeColor = false;
            this.xrTableCell327.StylePriority.UseTextAlignment = false;
            xrSummary15.FormatString = "{0:0.00%}";
            this.xrTableCell327.Summary = xrSummary15;
            this.xrTableCell327.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell327.Weight = 0.014973887748189205D;
            // 
            // xrTableCell328
            // 
            this.xrTableCell328.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail1.MarginPer2", "{0:0.00%}")});
            this.xrTableCell328.Dpi = 100F;
            this.xrTableCell328.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell328.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.xrTableCell328.Name = "xrTableCell328";
            this.xrTableCell328.StylePriority.UseFont = false;
            this.xrTableCell328.StylePriority.UseForeColor = false;
            this.xrTableCell328.StylePriority.UseTextAlignment = false;
            xrSummary16.FormatString = "{0:n}";
            this.xrTableCell328.Summary = xrSummary16;
            this.xrTableCell328.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell328.Weight = 0.014973689631148673D;
            // 
            // xrTableCell329
            // 
            this.xrTableCell329.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail1.MarginPer3", "{0:0.00%}")});
            this.xrTableCell329.Dpi = 100F;
            this.xrTableCell329.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell329.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.xrTableCell329.Name = "xrTableCell329";
            this.xrTableCell329.StylePriority.UseFont = false;
            this.xrTableCell329.StylePriority.UseForeColor = false;
            this.xrTableCell329.StylePriority.UseTextAlignment = false;
            xrSummary17.FormatString = "{0:n}";
            this.xrTableCell329.Summary = xrSummary17;
            this.xrTableCell329.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell329.Weight = 0.014973689631148673D;
            // 
            // xrTableCell330
            // 
            this.xrTableCell330.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail1.MarginPer4", "{0:0.00%}")});
            this.xrTableCell330.Dpi = 100F;
            this.xrTableCell330.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell330.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.xrTableCell330.Name = "xrTableCell330";
            this.xrTableCell330.StylePriority.UseFont = false;
            this.xrTableCell330.StylePriority.UseForeColor = false;
            this.xrTableCell330.StylePriority.UseTextAlignment = false;
            xrSummary18.FormatString = "{0:n}";
            this.xrTableCell330.Summary = xrSummary18;
            this.xrTableCell330.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell330.Weight = 0.014973689631148673D;
            // 
            // xrTableCell331
            // 
            this.xrTableCell331.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail1.MarginPer5", "{0:0.00%}")});
            this.xrTableCell331.Dpi = 100F;
            this.xrTableCell331.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell331.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.xrTableCell331.Name = "xrTableCell331";
            this.xrTableCell331.StylePriority.UseFont = false;
            this.xrTableCell331.StylePriority.UseForeColor = false;
            this.xrTableCell331.StylePriority.UseTextAlignment = false;
            xrSummary19.FormatString = "{0:n}";
            this.xrTableCell331.Summary = xrSummary19;
            this.xrTableCell331.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell331.Weight = 0.014973689631148673D;
            // 
            // xrTableCell332
            // 
            this.xrTableCell332.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail1.MarginPer6", "{0:0.00%}")});
            this.xrTableCell332.Dpi = 100F;
            this.xrTableCell332.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell332.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.xrTableCell332.Name = "xrTableCell332";
            this.xrTableCell332.StylePriority.UseFont = false;
            this.xrTableCell332.StylePriority.UseForeColor = false;
            this.xrTableCell332.StylePriority.UseTextAlignment = false;
            xrSummary20.FormatString = "{0:n}";
            this.xrTableCell332.Summary = xrSummary20;
            this.xrTableCell332.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell332.Weight = 0.014973689631148673D;
            // 
            // xrTableCell333
            // 
            this.xrTableCell333.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail1.MarginPer7", "{0:0.00%}")});
            this.xrTableCell333.Dpi = 100F;
            this.xrTableCell333.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell333.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.xrTableCell333.Name = "xrTableCell333";
            this.xrTableCell333.StylePriority.UseFont = false;
            this.xrTableCell333.StylePriority.UseForeColor = false;
            this.xrTableCell333.StylePriority.UseTextAlignment = false;
            xrSummary21.FormatString = "{0:0.00%}";
            this.xrTableCell333.Summary = xrSummary21;
            this.xrTableCell333.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell333.Weight = 0.014973689631148673D;
            // 
            // xrTableCell334
            // 
            this.xrTableCell334.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail1.MarginPer8", "{0:0.00%}")});
            this.xrTableCell334.Dpi = 100F;
            this.xrTableCell334.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell334.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.xrTableCell334.Name = "xrTableCell334";
            this.xrTableCell334.StylePriority.UseFont = false;
            this.xrTableCell334.StylePriority.UseForeColor = false;
            this.xrTableCell334.StylePriority.UseTextAlignment = false;
            xrSummary22.FormatString = "{0:n}";
            this.xrTableCell334.Summary = xrSummary22;
            this.xrTableCell334.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell334.Weight = 0.014973689631148673D;
            // 
            // xrTableCell335
            // 
            this.xrTableCell335.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail1.MarginPer9", "{0:0.00%}")});
            this.xrTableCell335.Dpi = 100F;
            this.xrTableCell335.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell335.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.xrTableCell335.Name = "xrTableCell335";
            this.xrTableCell335.StylePriority.UseFont = false;
            this.xrTableCell335.StylePriority.UseForeColor = false;
            this.xrTableCell335.StylePriority.UseTextAlignment = false;
            xrSummary23.FormatString = "{0:n}";
            this.xrTableCell335.Summary = xrSummary23;
            this.xrTableCell335.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell335.Weight = 0.014981511803462308D;
            // 
            // xrTableCell336
            // 
            this.xrTableCell336.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail1.MarginPer10", "{0:0.00%}")});
            this.xrTableCell336.Dpi = 100F;
            this.xrTableCell336.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell336.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.xrTableCell336.Name = "xrTableCell336";
            this.xrTableCell336.StylePriority.UseFont = false;
            this.xrTableCell336.StylePriority.UseForeColor = false;
            this.xrTableCell336.StylePriority.UseTextAlignment = false;
            xrSummary24.FormatString = "{0:n}";
            this.xrTableCell336.Summary = xrSummary24;
            this.xrTableCell336.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell336.Weight = 0.014965867458835038D;
            // 
            // xrTableCell337
            // 
            this.xrTableCell337.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail1.MarginPer11", "{0:0.00%}")});
            this.xrTableCell337.Dpi = 100F;
            this.xrTableCell337.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell337.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.xrTableCell337.Name = "xrTableCell337";
            this.xrTableCell337.StylePriority.UseFont = false;
            this.xrTableCell337.StylePriority.UseForeColor = false;
            this.xrTableCell337.StylePriority.UseTextAlignment = false;
            xrSummary25.FormatString = "{0:n}";
            this.xrTableCell337.Summary = xrSummary25;
            this.xrTableCell337.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell337.Weight = 0.014971685899902717D;
            // 
            // xrTableCell338
            // 
            this.xrTableCell338.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail1.MarginPer12", "{0:0.00%}")});
            this.xrTableCell338.Dpi = 100F;
            this.xrTableCell338.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell338.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.xrTableCell338.Name = "xrTableCell338";
            this.xrTableCell338.StylePriority.UseFont = false;
            this.xrTableCell338.StylePriority.UseForeColor = false;
            this.xrTableCell338.StylePriority.UseTextAlignment = false;
            xrSummary26.FormatString = "{0:n}";
            this.xrTableCell338.Summary = xrSummary26;
            this.xrTableCell338.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell338.Weight = 0.014974533857959983D;
            // 
            // xrTableCell339
            // 
            this.xrTableCell339.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail1.MarginPer", "{0:0.00%}")});
            this.xrTableCell339.Dpi = 100F;
            this.xrTableCell339.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell339.ForeColor = System.Drawing.Color.Blue;
            this.xrTableCell339.Name = "xrTableCell339";
            this.xrTableCell339.StylePriority.UseBackColor = false;
            this.xrTableCell339.StylePriority.UseFont = false;
            this.xrTableCell339.StylePriority.UseForeColor = false;
            this.xrTableCell339.StylePriority.UseTextAlignment = false;
            xrSummary27.FormatString = "{0:n}";
            this.xrTableCell339.Summary = xrSummary27;
            this.xrTableCell339.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell339.Weight = 0.014974849135583321D;
            // 
            // xrLabel9
            // 
            this.xrLabel9.Dpi = 100F;
            this.xrLabel9.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrLabel9.LocationFloat = new DevExpress.Utils.PointFloat(0F, 44.99998F);
            this.xrLabel9.Name = "xrLabel9";
            this.xrLabel9.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel9.SizeF = new System.Drawing.SizeF(331.808F, 15F);
            this.xrLabel9.StylePriority.UseFont = false;
            this.xrLabel9.StylePriority.UseTextAlignment = false;
            this.xrLabel9.Text = "Contribution Margin";
            this.xrLabel9.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel1
            // 
            this.xrLabel1.Dpi = 100F;
            this.xrLabel1.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(0.002829234F, 0F);
            this.xrLabel1.Name = "xrLabel1";
            this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel1.SizeF = new System.Drawing.SizeF(331.8086F, 15F);
            this.xrLabel1.StylePriority.UseFont = false;
            this.xrLabel1.StylePriority.UseTextAlignment = false;
            this.xrLabel1.Text = "Total Variable Cost";
            this.xrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrTable11
            // 
            this.xrTable11.Dpi = 100F;
            this.xrTable11.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTable11.LocationFloat = new DevExpress.Utils.PointFloat(331.81F, 14.99999F);
            this.xrTable11.Name = "xrTable11";
            this.xrTable11.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow22});
            this.xrTable11.SizeF = new System.Drawing.SizeF(1071.19F, 15F);
            this.xrTable11.StylePriority.UseFont = false;
            // 
            // xrTableRow22
            // 
            this.xrTableRow22.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell81,
            this.xrTableCell289,
            this.xrTableCell290,
            this.xrTableCell291,
            this.xrTableCell292,
            this.xrTableCell293,
            this.xrTableCell294,
            this.xrTableCell295,
            this.xrTableCell296,
            this.xrTableCell297,
            this.xrTableCell298,
            this.xrTableCell299,
            this.xrTableCell300});
            this.xrTableRow22.Dpi = 100F;
            this.xrTableRow22.Name = "xrTableRow22";
            this.xrTableRow22.Weight = 11.5D;
            // 
            // xrTableCell81
            // 
            this.xrTableCell81.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail1.TotalVarPer1", "{0:0.00%}")});
            this.xrTableCell81.Dpi = 100F;
            this.xrTableCell81.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell81.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.xrTableCell81.Name = "xrTableCell81";
            this.xrTableCell81.StylePriority.UseFont = false;
            this.xrTableCell81.StylePriority.UseForeColor = false;
            this.xrTableCell81.StylePriority.UseTextAlignment = false;
            xrSummary28.FormatString = "{0:0.00%}";
            this.xrTableCell81.Summary = xrSummary28;
            this.xrTableCell81.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell81.Weight = 0.014973887748189205D;
            // 
            // xrTableCell289
            // 
            this.xrTableCell289.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail1.TotalVarPer2", "{0:0.00%}")});
            this.xrTableCell289.Dpi = 100F;
            this.xrTableCell289.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell289.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.xrTableCell289.Name = "xrTableCell289";
            this.xrTableCell289.StylePriority.UseFont = false;
            this.xrTableCell289.StylePriority.UseForeColor = false;
            this.xrTableCell289.StylePriority.UseTextAlignment = false;
            xrSummary29.FormatString = "{0:n}";
            this.xrTableCell289.Summary = xrSummary29;
            this.xrTableCell289.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell289.Weight = 0.014973689631148673D;
            // 
            // xrTableCell290
            // 
            this.xrTableCell290.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail1.TotalVarPer3", "{0:0.00%}")});
            this.xrTableCell290.Dpi = 100F;
            this.xrTableCell290.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell290.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.xrTableCell290.Name = "xrTableCell290";
            this.xrTableCell290.StylePriority.UseFont = false;
            this.xrTableCell290.StylePriority.UseForeColor = false;
            this.xrTableCell290.StylePriority.UseTextAlignment = false;
            xrSummary30.FormatString = "{0:n}";
            this.xrTableCell290.Summary = xrSummary30;
            this.xrTableCell290.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell290.Weight = 0.014973689631148673D;
            // 
            // xrTableCell291
            // 
            this.xrTableCell291.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail1.TotalVarPer4", "{0:0.00%}")});
            this.xrTableCell291.Dpi = 100F;
            this.xrTableCell291.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell291.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.xrTableCell291.Name = "xrTableCell291";
            this.xrTableCell291.StylePriority.UseFont = false;
            this.xrTableCell291.StylePriority.UseForeColor = false;
            this.xrTableCell291.StylePriority.UseTextAlignment = false;
            xrSummary31.FormatString = "{0:n}";
            this.xrTableCell291.Summary = xrSummary31;
            this.xrTableCell291.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell291.Weight = 0.014973689631148673D;
            // 
            // xrTableCell292
            // 
            this.xrTableCell292.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail1.TotalVarPer5", "{0:0.00%}")});
            this.xrTableCell292.Dpi = 100F;
            this.xrTableCell292.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell292.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.xrTableCell292.Name = "xrTableCell292";
            this.xrTableCell292.StylePriority.UseFont = false;
            this.xrTableCell292.StylePriority.UseForeColor = false;
            this.xrTableCell292.StylePriority.UseTextAlignment = false;
            xrSummary32.FormatString = "{0:n}";
            this.xrTableCell292.Summary = xrSummary32;
            this.xrTableCell292.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell292.Weight = 0.014973689631148673D;
            // 
            // xrTableCell293
            // 
            this.xrTableCell293.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail1.TotalVarPer6", "{0:0.00%}")});
            this.xrTableCell293.Dpi = 100F;
            this.xrTableCell293.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell293.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.xrTableCell293.Name = "xrTableCell293";
            this.xrTableCell293.StylePriority.UseFont = false;
            this.xrTableCell293.StylePriority.UseForeColor = false;
            this.xrTableCell293.StylePriority.UseTextAlignment = false;
            xrSummary33.FormatString = "{0:n}";
            this.xrTableCell293.Summary = xrSummary33;
            this.xrTableCell293.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell293.Weight = 0.014973689631148673D;
            // 
            // xrTableCell294
            // 
            this.xrTableCell294.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail1.TotalVarPer7", "{0:0.00%}")});
            this.xrTableCell294.Dpi = 100F;
            this.xrTableCell294.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell294.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.xrTableCell294.Name = "xrTableCell294";
            this.xrTableCell294.StylePriority.UseFont = false;
            this.xrTableCell294.StylePriority.UseForeColor = false;
            this.xrTableCell294.StylePriority.UseTextAlignment = false;
            xrSummary34.FormatString = "{0:0.00%}";
            this.xrTableCell294.Summary = xrSummary34;
            this.xrTableCell294.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell294.Weight = 0.014973689631148673D;
            // 
            // xrTableCell295
            // 
            this.xrTableCell295.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail1.TotalVarPer8", "{0:0.00%}")});
            this.xrTableCell295.Dpi = 100F;
            this.xrTableCell295.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell295.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.xrTableCell295.Name = "xrTableCell295";
            this.xrTableCell295.StylePriority.UseFont = false;
            this.xrTableCell295.StylePriority.UseForeColor = false;
            this.xrTableCell295.StylePriority.UseTextAlignment = false;
            xrSummary35.FormatString = "{0:n}";
            this.xrTableCell295.Summary = xrSummary35;
            this.xrTableCell295.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell295.Weight = 0.014973689631148673D;
            // 
            // xrTableCell296
            // 
            this.xrTableCell296.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail1.TotalVarPer9", "{0:0.00%}")});
            this.xrTableCell296.Dpi = 100F;
            this.xrTableCell296.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell296.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.xrTableCell296.Name = "xrTableCell296";
            this.xrTableCell296.StylePriority.UseFont = false;
            this.xrTableCell296.StylePriority.UseForeColor = false;
            this.xrTableCell296.StylePriority.UseTextAlignment = false;
            xrSummary36.FormatString = "{0:n}";
            this.xrTableCell296.Summary = xrSummary36;
            this.xrTableCell296.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell296.Weight = 0.014981511803462308D;
            // 
            // xrTableCell297
            // 
            this.xrTableCell297.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail1.TotalVarPer10", "{0:0.00%}")});
            this.xrTableCell297.Dpi = 100F;
            this.xrTableCell297.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell297.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.xrTableCell297.Name = "xrTableCell297";
            this.xrTableCell297.StylePriority.UseFont = false;
            this.xrTableCell297.StylePriority.UseForeColor = false;
            this.xrTableCell297.StylePriority.UseTextAlignment = false;
            xrSummary37.FormatString = "{0:n}";
            this.xrTableCell297.Summary = xrSummary37;
            this.xrTableCell297.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell297.Weight = 0.014965867458835038D;
            // 
            // xrTableCell298
            // 
            this.xrTableCell298.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail1.TotalVarPer11", "{0:0.00%}")});
            this.xrTableCell298.Dpi = 100F;
            this.xrTableCell298.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell298.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.xrTableCell298.Name = "xrTableCell298";
            this.xrTableCell298.StylePriority.UseFont = false;
            this.xrTableCell298.StylePriority.UseForeColor = false;
            this.xrTableCell298.StylePriority.UseTextAlignment = false;
            xrSummary38.FormatString = "{0:n}";
            this.xrTableCell298.Summary = xrSummary38;
            this.xrTableCell298.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell298.Weight = 0.014971685899902717D;
            // 
            // xrTableCell299
            // 
            this.xrTableCell299.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail1.TotalVarPer12", "{0:0.00%}")});
            this.xrTableCell299.Dpi = 100F;
            this.xrTableCell299.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell299.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.xrTableCell299.Name = "xrTableCell299";
            this.xrTableCell299.StylePriority.UseFont = false;
            this.xrTableCell299.StylePriority.UseForeColor = false;
            this.xrTableCell299.StylePriority.UseTextAlignment = false;
            xrSummary39.FormatString = "{0:n}";
            this.xrTableCell299.Summary = xrSummary39;
            this.xrTableCell299.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell299.Weight = 0.014974533857959983D;
            // 
            // xrTableCell300
            // 
            this.xrTableCell300.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail1.TotalVarPer", "{0:0.00%}")});
            this.xrTableCell300.Dpi = 100F;
            this.xrTableCell300.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell300.ForeColor = System.Drawing.Color.Blue;
            this.xrTableCell300.Name = "xrTableCell300";
            this.xrTableCell300.StylePriority.UseBackColor = false;
            this.xrTableCell300.StylePriority.UseFont = false;
            this.xrTableCell300.StylePriority.UseForeColor = false;
            this.xrTableCell300.StylePriority.UseTextAlignment = false;
            xrSummary40.FormatString = "{0:n}";
            this.xrTableCell300.Summary = xrSummary40;
            this.xrTableCell300.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell300.Weight = 0.014974849135583321D;
            // 
            // xrTable3
            // 
            this.xrTable3.Borders = DevExpress.XtraPrinting.BorderSide.Top;
            this.xrTable3.Dpi = 100F;
            this.xrTable3.LocationFloat = new DevExpress.Utils.PointFloat(331.8114F, 0F);
            this.xrTable3.Name = "xrTable3";
            this.xrTable3.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow3});
            this.xrTable3.SizeF = new System.Drawing.SizeF(1071.189F, 15F);
            this.xrTable3.StylePriority.UseBorders = false;
            // 
            // xrTableRow3
            // 
            this.xrTableRow3.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell29,
            this.xrTableCell30,
            this.xrTableCell31,
            this.xrTableCell32,
            this.xrTableCell33,
            this.xrTableCell34,
            this.xrTableCell35,
            this.xrTableCell36,
            this.xrTableCell37,
            this.xrTableCell38,
            this.xrTableCell39,
            this.xrTableCell40,
            this.xrTableCell41});
            this.xrTableRow3.Dpi = 100F;
            this.xrTableRow3.Name = "xrTableRow3";
            this.xrTableRow3.Weight = 11.5D;
            // 
            // xrTableCell29
            // 
            this.xrTableCell29.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail1.Amount_01")});
            this.xrTableCell29.Dpi = 100F;
            this.xrTableCell29.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell29.Name = "xrTableCell29";
            this.xrTableCell29.StylePriority.UseFont = false;
            this.xrTableCell29.StylePriority.UseTextAlignment = false;
            xrSummary41.FormatString = "{0:n}";
            xrSummary41.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            this.xrTableCell29.Summary = xrSummary41;
            this.xrTableCell29.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell29.Weight = 0.010833457060557624D;
            // 
            // xrTableCell30
            // 
            this.xrTableCell30.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail1.Amount_02")});
            this.xrTableCell30.Dpi = 100F;
            this.xrTableCell30.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell30.Name = "xrTableCell30";
            this.xrTableCell30.StylePriority.UseFont = false;
            this.xrTableCell30.StylePriority.UseTextAlignment = false;
            xrSummary42.FormatString = "{0:n}";
            xrSummary42.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            this.xrTableCell30.Summary = xrSummary42;
            this.xrTableCell30.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell30.Weight = 0.010833353068105147D;
            // 
            // xrTableCell31
            // 
            this.xrTableCell31.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail1.Amount_03")});
            this.xrTableCell31.Dpi = 100F;
            this.xrTableCell31.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell31.Name = "xrTableCell31";
            this.xrTableCell31.StylePriority.UseFont = false;
            this.xrTableCell31.StylePriority.UseTextAlignment = false;
            xrSummary43.FormatString = "{0:n}";
            xrSummary43.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            this.xrTableCell31.Summary = xrSummary43;
            this.xrTableCell31.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell31.Weight = 0.010833340095552212D;
            // 
            // xrTableCell32
            // 
            this.xrTableCell32.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail1.Amount_04")});
            this.xrTableCell32.Dpi = 100F;
            this.xrTableCell32.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell32.Name = "xrTableCell32";
            this.xrTableCell32.StylePriority.UseFont = false;
            this.xrTableCell32.StylePriority.UseTextAlignment = false;
            xrSummary44.FormatString = "{0:n}";
            xrSummary44.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            this.xrTableCell32.Summary = xrSummary44;
            this.xrTableCell32.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell32.Weight = 0.010833344100769705D;
            // 
            // xrTableCell33
            // 
            this.xrTableCell33.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail1.Amount_05")});
            this.xrTableCell33.Dpi = 100F;
            this.xrTableCell33.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell33.Name = "xrTableCell33";
            this.xrTableCell33.StylePriority.UseFont = false;
            this.xrTableCell33.StylePriority.UseTextAlignment = false;
            xrSummary45.FormatString = "{0:n}";
            xrSummary45.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            this.xrTableCell33.Summary = xrSummary45;
            this.xrTableCell33.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell33.Weight = 0.010833228290575174D;
            // 
            // xrTableCell34
            // 
            this.xrTableCell34.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail1.Amount_06")});
            this.xrTableCell34.Dpi = 100F;
            this.xrTableCell34.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell34.Name = "xrTableCell34";
            this.xrTableCell34.StylePriority.UseFont = false;
            this.xrTableCell34.StylePriority.UseTextAlignment = false;
            xrSummary46.FormatString = "{0:n}";
            xrSummary46.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            this.xrTableCell34.Summary = xrSummary46;
            this.xrTableCell34.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell34.Weight = 0.010833348592122385D;
            // 
            // xrTableCell35
            // 
            this.xrTableCell35.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail1.Amount_07")});
            this.xrTableCell35.Dpi = 100F;
            this.xrTableCell35.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell35.Name = "xrTableCell35";
            this.xrTableCell35.StylePriority.UseFont = false;
            this.xrTableCell35.StylePriority.UseTextAlignment = false;
            xrSummary47.FormatString = "{0:n}";
            xrSummary47.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            this.xrTableCell35.Summary = xrSummary47;
            this.xrTableCell35.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell35.Weight = 0.010833348592122385D;
            // 
            // xrTableCell36
            // 
            this.xrTableCell36.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail1.Amount_08")});
            this.xrTableCell36.Dpi = 100F;
            this.xrTableCell36.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell36.Name = "xrTableCell36";
            this.xrTableCell36.StylePriority.UseFont = false;
            this.xrTableCell36.StylePriority.UseTextAlignment = false;
            xrSummary48.FormatString = "{0:n}";
            xrSummary48.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            this.xrTableCell36.Summary = xrSummary48;
            this.xrTableCell36.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell36.Weight = 0.010833348592122385D;
            // 
            // xrTableCell37
            // 
            this.xrTableCell37.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail1.Amount_09")});
            this.xrTableCell37.Dpi = 100F;
            this.xrTableCell37.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell37.Name = "xrTableCell37";
            this.xrTableCell37.StylePriority.UseFont = false;
            this.xrTableCell37.StylePriority.UseTextAlignment = false;
            xrSummary49.FormatString = "{0:n}";
            xrSummary49.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            this.xrTableCell37.Summary = xrSummary49;
            this.xrTableCell37.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell37.Weight = 0.010839060839599962D;
            // 
            // xrTableCell38
            // 
            this.xrTableCell38.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail1.Amount_10")});
            this.xrTableCell38.Dpi = 100F;
            this.xrTableCell38.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell38.Name = "xrTableCell38";
            this.xrTableCell38.StylePriority.UseFont = false;
            this.xrTableCell38.StylePriority.UseTextAlignment = false;
            xrSummary50.FormatString = "{0:n}";
            xrSummary50.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            this.xrTableCell38.Summary = xrSummary50;
            this.xrTableCell38.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell38.Weight = 0.01082767958648794D;
            // 
            // xrTableCell39
            // 
            this.xrTableCell39.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail1.Amount_11")});
            this.xrTableCell39.Dpi = 100F;
            this.xrTableCell39.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell39.Name = "xrTableCell39";
            this.xrTableCell39.StylePriority.UseFont = false;
            this.xrTableCell39.StylePriority.UseForeColor = false;
            this.xrTableCell39.StylePriority.UseTextAlignment = false;
            xrSummary51.FormatString = "{0:n}";
            xrSummary51.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            this.xrTableCell39.Summary = xrSummary51;
            this.xrTableCell39.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell39.Weight = 0.010831891342008878D;
            // 
            // xrTableCell40
            // 
            this.xrTableCell40.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail1.Amount_12")});
            this.xrTableCell40.Dpi = 100F;
            this.xrTableCell40.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell40.Name = "xrTableCell40";
            this.xrTableCell40.StylePriority.UseFont = false;
            this.xrTableCell40.StylePriority.UseTextAlignment = false;
            xrSummary52.FormatString = "{0:n}";
            xrSummary52.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            this.xrTableCell40.Summary = xrSummary52;
            this.xrTableCell40.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell40.Weight = 0.010833984247216408D;
            // 
            // xrTableCell41
            // 
            this.xrTableCell41.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail1.Amount")});
            this.xrTableCell41.Dpi = 100F;
            this.xrTableCell41.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell41.ForeColor = System.Drawing.Color.Black;
            this.xrTableCell41.Name = "xrTableCell41";
            this.xrTableCell41.StylePriority.UseFont = false;
            this.xrTableCell41.StylePriority.UseForeColor = false;
            this.xrTableCell41.StylePriority.UseTextAlignment = false;
            xrSummary53.FormatString = "{0:n}";
            xrSummary53.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            this.xrTableCell41.Summary = xrSummary53;
            this.xrTableCell41.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell41.Weight = 0.01083412694529874D;
            // 
            // DetailReport3
            // 
            this.DetailReport3.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail4,
            this.GroupFooter4});
            this.DetailReport3.DataMember = "sp_report_ContributionMarginDetailAcnt";
            this.DetailReport3.DataSource = this.sqlDataSource1;
            this.DetailReport3.Dpi = 100F;
            this.DetailReport3.Level = 0;
            this.DetailReport3.Name = "DetailReport3";
            // 
            // Detail4
            // 
            this.Detail4.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable7});
            this.Detail4.Dpi = 100F;
            this.Detail4.HeightF = 15F;
            this.Detail4.Name = "Detail4";
            // 
            // xrTable7
            // 
            this.xrTable7.Dpi = 100F;
            this.xrTable7.LocationFloat = new DevExpress.Utils.PointFloat(10F, 0F);
            this.xrTable7.Name = "xrTable7";
            this.xrTable7.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow8});
            this.xrTable7.SizeF = new System.Drawing.SizeF(1392.997F, 15F);
            // 
            // xrTableRow8
            // 
            this.xrTableRow8.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell95,
            this.xrTableCell96,
            this.xrTableCell97,
            this.xrTableCell98,
            this.xrTableCell99,
            this.xrTableCell100,
            this.xrTableCell101,
            this.xrTableCell102,
            this.xrTableCell103,
            this.xrTableCell104,
            this.xrTableCell105,
            this.xrTableCell106,
            this.xrTableCell107,
            this.xrTableCell108});
            this.xrTableRow8.Dpi = 100F;
            this.xrTableRow8.Name = "xrTableRow8";
            this.xrTableRow8.Weight = 11.5D;
            // 
            // xrTableCell95
            // 
            this.xrTableCell95.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetailAcnt.DescriptionDetail")});
            this.xrTableCell95.Dpi = 100F;
            this.xrTableCell95.Font = new System.Drawing.Font("Arial", 7.5F);
            this.xrTableCell95.Name = "xrTableCell95";
            this.xrTableCell95.StylePriority.UseFont = false;
            this.xrTableCell95.StylePriority.UseTextAlignment = false;
            this.xrTableCell95.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            this.xrTableCell95.Weight = 0.044742444917237875D;
            // 
            // xrTableCell96
            // 
            this.xrTableCell96.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetailAcnt.Amount_01", "{0:n}")});
            this.xrTableCell96.Dpi = 100F;
            this.xrTableCell96.Font = new System.Drawing.Font("Arial", 7.5F);
            this.xrTableCell96.Name = "xrTableCell96";
            this.xrTableCell96.StylePriority.UseFont = false;
            this.xrTableCell96.StylePriority.UseTextAlignment = false;
            this.xrTableCell96.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell96.Weight = 0.011456676506122643D;
            // 
            // xrTableCell97
            // 
            this.xrTableCell97.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetailAcnt.Amount_02", "{0:n}")});
            this.xrTableCell97.Dpi = 100F;
            this.xrTableCell97.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell97.Name = "xrTableCell97";
            this.xrTableCell97.StylePriority.UseFont = false;
            this.xrTableCell97.StylePriority.UseTextAlignment = false;
            this.xrTableCell97.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell97.Weight = 0.011456439447518315D;
            // 
            // xrTableCell98
            // 
            this.xrTableCell98.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetailAcnt.Amount_03", "{0:n}")});
            this.xrTableCell98.Dpi = 100F;
            this.xrTableCell98.Font = new System.Drawing.Font("Arial", 7.5F);
            this.xrTableCell98.Name = "xrTableCell98";
            this.xrTableCell98.StylePriority.UseFont = false;
            this.xrTableCell98.StylePriority.UseTextAlignment = false;
            this.xrTableCell98.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell98.Weight = 0.01145644711665144D;
            // 
            // xrTableCell99
            // 
            this.xrTableCell99.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetailAcnt.Amount_04", "{0:n}")});
            this.xrTableCell99.Dpi = 100F;
            this.xrTableCell99.Font = new System.Drawing.Font("Arial", 7.5F);
            this.xrTableCell99.Name = "xrTableCell99";
            this.xrTableCell99.StylePriority.UseFont = false;
            this.xrTableCell99.StylePriority.UseTextAlignment = false;
            this.xrTableCell99.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell99.Weight = 0.011456464340136261D;
            // 
            // xrTableCell100
            // 
            this.xrTableCell100.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetailAcnt.Amount_05", "{0:n}")});
            this.xrTableCell100.Dpi = 100F;
            this.xrTableCell100.Font = new System.Drawing.Font("Arial", 7.5F);
            this.xrTableCell100.Name = "xrTableCell100";
            this.xrTableCell100.StylePriority.UseFont = false;
            this.xrTableCell100.StylePriority.UseTextAlignment = false;
            this.xrTableCell100.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell100.Weight = 0.011456379748427982D;
            // 
            // xrTableCell101
            // 
            this.xrTableCell101.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetailAcnt.Amount_06", "{0:n}")});
            this.xrTableCell101.Dpi = 100F;
            this.xrTableCell101.Font = new System.Drawing.Font("Arial", 7.5F);
            this.xrTableCell101.Name = "xrTableCell101";
            this.xrTableCell101.StylePriority.UseFont = false;
            this.xrTableCell101.StylePriority.UseTextAlignment = false;
            this.xrTableCell101.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell101.Weight = 0.011456481623145959D;
            // 
            // xrTableCell102
            // 
            this.xrTableCell102.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetailAcnt.Amount_07", "{0:n}")});
            this.xrTableCell102.Dpi = 100F;
            this.xrTableCell102.Font = new System.Drawing.Font("Arial", 7.5F);
            this.xrTableCell102.Name = "xrTableCell102";
            this.xrTableCell102.StylePriority.UseFont = false;
            this.xrTableCell102.StylePriority.UseTextAlignment = false;
            this.xrTableCell102.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell102.Weight = 0.011456448104521738D;
            // 
            // xrTableCell103
            // 
            this.xrTableCell103.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetailAcnt.Amount_08", "{0:n}")});
            this.xrTableCell103.Dpi = 100F;
            this.xrTableCell103.Font = new System.Drawing.Font("Arial", 7.5F);
            this.xrTableCell103.Name = "xrTableCell103";
            this.xrTableCell103.StylePriority.UseFont = false;
            this.xrTableCell103.StylePriority.UseTextAlignment = false;
            this.xrTableCell103.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell103.Weight = 0.01145656843549997D;
            // 
            // xrTableCell104
            // 
            this.xrTableCell104.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetailAcnt.Amount_09", "{0:n}")});
            this.xrTableCell104.Dpi = 100F;
            this.xrTableCell104.Font = new System.Drawing.Font("Arial", 7.5F);
            this.xrTableCell104.Name = "xrTableCell104";
            this.xrTableCell104.StylePriority.UseFont = false;
            this.xrTableCell104.StylePriority.UseTextAlignment = false;
            this.xrTableCell104.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell104.Weight = 0.011462360890095221D;
            // 
            // xrTableCell105
            // 
            this.xrTableCell105.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetailAcnt.Amount_10", "{0:n}")});
            this.xrTableCell105.Dpi = 100F;
            this.xrTableCell105.Font = new System.Drawing.Font("Arial", 7.5F);
            this.xrTableCell105.Name = "xrTableCell105";
            this.xrTableCell105.StylePriority.UseFont = false;
            this.xrTableCell105.StylePriority.UseTextAlignment = false;
            this.xrTableCell105.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell105.Weight = 0.011450439867777159D;
            // 
            // xrTableCell106
            // 
            this.xrTableCell106.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetailAcnt.Amount_11", "{0:n}")});
            this.xrTableCell106.Dpi = 100F;
            this.xrTableCell106.Font = new System.Drawing.Font("Arial", 7.5F);
            this.xrTableCell106.Name = "xrTableCell106";
            this.xrTableCell106.StylePriority.UseFont = false;
            this.xrTableCell106.StylePriority.UseTextAlignment = false;
            this.xrTableCell106.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell106.Weight = 0.011455021519186374D;
            // 
            // xrTableCell107
            // 
            this.xrTableCell107.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetailAcnt.Amount_12", "{0:n}")});
            this.xrTableCell107.Dpi = 100F;
            this.xrTableCell107.Font = new System.Drawing.Font("Arial", 7.5F);
            this.xrTableCell107.Name = "xrTableCell107";
            this.xrTableCell107.StylePriority.UseFont = false;
            this.xrTableCell107.StylePriority.UseTextAlignment = false;
            xrSummary54.FormatString = "{0:#,0}";
            this.xrTableCell107.Summary = xrSummary54;
            this.xrTableCell107.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell107.Weight = 0.01145697336366436D;
            // 
            // xrTableCell108
            // 
            this.xrTableCell108.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetailAcnt.Amount", "{0:n}")});
            this.xrTableCell108.Dpi = 100F;
            this.xrTableCell108.Font = new System.Drawing.Font("Arial", 7.5F);
            this.xrTableCell108.Name = "xrTableCell108";
            this.xrTableCell108.StylePriority.UseFont = false;
            this.xrTableCell108.StylePriority.UseTextAlignment = false;
            this.xrTableCell108.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell108.Weight = 0.011457284165937842D;
            // 
            // GroupFooter4
            // 
            this.GroupFooter4.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable21,
            this.xrTable8});
            this.GroupFooter4.Dpi = 100F;
            this.GroupFooter4.HeightF = 46.04165F;
            this.GroupFooter4.Name = "GroupFooter4";
            // 
            // xrTable21
            // 
            this.xrTable21.Dpi = 100F;
            this.xrTable21.LocationFloat = new DevExpress.Utils.PointFloat(10.00404F, 31.66663F);
            this.xrTable21.Name = "xrTable21";
            this.xrTable21.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow15});
            this.xrTable21.SizeF = new System.Drawing.SizeF(332.2917F, 14.37502F);
            // 
            // xrTableRow15
            // 
            this.xrTableRow15.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell179});
            this.xrTableRow15.Dpi = 100F;
            this.xrTableRow15.Name = "xrTableRow15";
            this.xrTableRow15.Weight = 1D;
            // 
            // xrTableCell179
            // 
            this.xrTableCell179.Dpi = 100F;
            this.xrTableCell179.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell179.Name = "xrTableCell179";
            this.xrTableCell179.StylePriority.UseFont = false;
            this.xrTableCell179.Text = "Selling, Administrative and General";
            this.xrTableCell179.Weight = 1D;
            // 
            // xrTable8
            // 
            this.xrTable8.Dpi = 100F;
            this.xrTable8.LocationFloat = new DevExpress.Utils.PointFloat(10F, 0F);
            this.xrTable8.Name = "xrTable8";
            this.xrTable8.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow9});
            this.xrTable8.SizeF = new System.Drawing.SizeF(1392.997F, 15F);
            // 
            // xrTableRow9
            // 
            this.xrTableRow9.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell109,
            this.xrTableCell110,
            this.xrTableCell111,
            this.xrTableCell112,
            this.xrTableCell113,
            this.xrTableCell114,
            this.xrTableCell115,
            this.xrTableCell116,
            this.xrTableCell117,
            this.xrTableCell118,
            this.xrTableCell119,
            this.xrTableCell120,
            this.xrTableCell121,
            this.xrTableCell122});
            this.xrTableRow9.Dpi = 100F;
            this.xrTableRow9.Name = "xrTableRow9";
            this.xrTableRow9.Weight = 11.5D;
            // 
            // xrTableCell109
            // 
            this.xrTableCell109.Dpi = 100F;
            this.xrTableCell109.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell109.Name = "xrTableCell109";
            this.xrTableCell109.StylePriority.UseFont = false;
            this.xrTableCell109.Text = "Total Cost of Goods Sold";
            this.xrTableCell109.Weight = 0.044742444870609452D;
            // 
            // xrTableCell110
            // 
            this.xrTableCell110.Borders = DevExpress.XtraPrinting.BorderSide.Top;
            this.xrTableCell110.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetailAcnt.Amount_01", "{0:n}")});
            this.xrTableCell110.Dpi = 100F;
            this.xrTableCell110.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell110.Name = "xrTableCell110";
            this.xrTableCell110.StylePriority.UseBorders = false;
            this.xrTableCell110.StylePriority.UseFont = false;
            this.xrTableCell110.StylePriority.UseTextAlignment = false;
            this.xrTableCell110.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell110.Weight = 0.011456676506122643D;
            // 
            // xrTableCell111
            // 
            this.xrTableCell111.Borders = DevExpress.XtraPrinting.BorderSide.Top;
            this.xrTableCell111.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetailAcnt.Amount_02", "{0:n}")});
            this.xrTableCell111.Dpi = 100F;
            this.xrTableCell111.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell111.Name = "xrTableCell111";
            this.xrTableCell111.StylePriority.UseBorders = false;
            this.xrTableCell111.StylePriority.UseFont = false;
            this.xrTableCell111.StylePriority.UseTextAlignment = false;
            this.xrTableCell111.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell111.Weight = 0.011456439447518315D;
            // 
            // xrTableCell112
            // 
            this.xrTableCell112.Borders = DevExpress.XtraPrinting.BorderSide.Top;
            this.xrTableCell112.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetailAcnt.Amount_03", "{0:n}")});
            this.xrTableCell112.Dpi = 100F;
            this.xrTableCell112.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell112.Name = "xrTableCell112";
            this.xrTableCell112.StylePriority.UseBorders = false;
            this.xrTableCell112.StylePriority.UseFont = false;
            this.xrTableCell112.StylePriority.UseTextAlignment = false;
            this.xrTableCell112.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell112.Weight = 0.01145644711665144D;
            // 
            // xrTableCell113
            // 
            this.xrTableCell113.Borders = DevExpress.XtraPrinting.BorderSide.Top;
            this.xrTableCell113.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetailAcnt.Amount_04", "{0:n}")});
            this.xrTableCell113.Dpi = 100F;
            this.xrTableCell113.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell113.Name = "xrTableCell113";
            this.xrTableCell113.StylePriority.UseBorders = false;
            this.xrTableCell113.StylePriority.UseFont = false;
            this.xrTableCell113.StylePriority.UseTextAlignment = false;
            this.xrTableCell113.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell113.Weight = 0.011456464340136261D;
            // 
            // xrTableCell114
            // 
            this.xrTableCell114.Borders = DevExpress.XtraPrinting.BorderSide.Top;
            this.xrTableCell114.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetailAcnt.Amount_05", "{0:n}")});
            this.xrTableCell114.Dpi = 100F;
            this.xrTableCell114.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell114.Name = "xrTableCell114";
            this.xrTableCell114.StylePriority.UseBorders = false;
            this.xrTableCell114.StylePriority.UseFont = false;
            this.xrTableCell114.StylePriority.UseTextAlignment = false;
            this.xrTableCell114.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell114.Weight = 0.011456379748427982D;
            // 
            // xrTableCell115
            // 
            this.xrTableCell115.Borders = DevExpress.XtraPrinting.BorderSide.Top;
            this.xrTableCell115.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetailAcnt.Amount_06", "{0:n}")});
            this.xrTableCell115.Dpi = 100F;
            this.xrTableCell115.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell115.Name = "xrTableCell115";
            this.xrTableCell115.StylePriority.UseBorders = false;
            this.xrTableCell115.StylePriority.UseFont = false;
            this.xrTableCell115.StylePriority.UseTextAlignment = false;
            this.xrTableCell115.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell115.Weight = 0.011456481623145959D;
            // 
            // xrTableCell116
            // 
            this.xrTableCell116.Borders = DevExpress.XtraPrinting.BorderSide.Top;
            this.xrTableCell116.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetailAcnt.Amount_07", "{0:n}")});
            this.xrTableCell116.Dpi = 100F;
            this.xrTableCell116.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell116.Name = "xrTableCell116";
            this.xrTableCell116.StylePriority.UseBorders = false;
            this.xrTableCell116.StylePriority.UseFont = false;
            this.xrTableCell116.StylePriority.UseTextAlignment = false;
            this.xrTableCell116.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell116.Weight = 0.011456448104521738D;
            // 
            // xrTableCell117
            // 
            this.xrTableCell117.Borders = DevExpress.XtraPrinting.BorderSide.Top;
            this.xrTableCell117.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetailAcnt.Amount_08", "{0:n}")});
            this.xrTableCell117.Dpi = 100F;
            this.xrTableCell117.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell117.Name = "xrTableCell117";
            this.xrTableCell117.StylePriority.UseBorders = false;
            this.xrTableCell117.StylePriority.UseFont = false;
            this.xrTableCell117.StylePriority.UseTextAlignment = false;
            this.xrTableCell117.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell117.Weight = 0.01145656843549997D;
            // 
            // xrTableCell118
            // 
            this.xrTableCell118.Borders = DevExpress.XtraPrinting.BorderSide.Top;
            this.xrTableCell118.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetailAcnt.Amount_09", "{0:n}")});
            this.xrTableCell118.Dpi = 100F;
            this.xrTableCell118.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell118.Name = "xrTableCell118";
            this.xrTableCell118.StylePriority.UseBorders = false;
            this.xrTableCell118.StylePriority.UseFont = false;
            this.xrTableCell118.StylePriority.UseTextAlignment = false;
            this.xrTableCell118.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell118.Weight = 0.011462360890095221D;
            // 
            // xrTableCell119
            // 
            this.xrTableCell119.Borders = DevExpress.XtraPrinting.BorderSide.Top;
            this.xrTableCell119.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetailAcnt.Amount_10", "{0:n}")});
            this.xrTableCell119.Dpi = 100F;
            this.xrTableCell119.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell119.Name = "xrTableCell119";
            this.xrTableCell119.StylePriority.UseBorders = false;
            this.xrTableCell119.StylePriority.UseFont = false;
            this.xrTableCell119.StylePriority.UseTextAlignment = false;
            this.xrTableCell119.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell119.Weight = 0.011450439867777159D;
            // 
            // xrTableCell120
            // 
            this.xrTableCell120.Borders = DevExpress.XtraPrinting.BorderSide.Top;
            this.xrTableCell120.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetailAcnt.Amount_11", "{0:n}")});
            this.xrTableCell120.Dpi = 100F;
            this.xrTableCell120.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell120.Name = "xrTableCell120";
            this.xrTableCell120.StylePriority.UseBorders = false;
            this.xrTableCell120.StylePriority.UseFont = false;
            this.xrTableCell120.StylePriority.UseTextAlignment = false;
            this.xrTableCell120.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell120.Weight = 0.011455021519186374D;
            // 
            // xrTableCell121
            // 
            this.xrTableCell121.Borders = DevExpress.XtraPrinting.BorderSide.Top;
            this.xrTableCell121.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetailAcnt.Amount_12", "{0:n}")});
            this.xrTableCell121.Dpi = 100F;
            this.xrTableCell121.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell121.Name = "xrTableCell121";
            this.xrTableCell121.StylePriority.UseBorders = false;
            this.xrTableCell121.StylePriority.UseFont = false;
            this.xrTableCell121.StylePriority.UseTextAlignment = false;
            xrSummary55.FormatString = "{0:#,0}";
            this.xrTableCell121.Summary = xrSummary55;
            this.xrTableCell121.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell121.Weight = 0.01145697336366436D;
            // 
            // xrTableCell122
            // 
            this.xrTableCell122.Borders = DevExpress.XtraPrinting.BorderSide.Top;
            this.xrTableCell122.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetailAcnt.Amount", "{0:n}")});
            this.xrTableCell122.Dpi = 100F;
            this.xrTableCell122.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell122.Name = "xrTableCell122";
            this.xrTableCell122.StylePriority.UseBorders = false;
            this.xrTableCell122.StylePriority.UseFont = false;
            this.xrTableCell122.StylePriority.UseTextAlignment = false;
            this.xrTableCell122.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell122.Weight = 0.011457284165937842D;
            // 
            // DetailReport4
            // 
            this.DetailReport4.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail5,
            this.GroupFooter5});
            this.DetailReport4.Dpi = 100F;
            this.DetailReport4.Level = 1;
            this.DetailReport4.Name = "DetailReport4";
            // 
            // Detail5
            // 
            this.Detail5.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable13});
            this.Detail5.Dpi = 100F;
            this.Detail5.HeightF = 15F;
            this.Detail5.Name = "Detail5";
            this.Detail5.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.Detail5_BeforePrint);
            // 
            // xrTable13
            // 
            this.xrTable13.Dpi = 100F;
            this.xrTable13.LocationFloat = new DevExpress.Utils.PointFloat(10F, 0F);
            this.xrTable13.Name = "xrTable13";
            this.xrTable13.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow10});
            this.xrTable13.SizeF = new System.Drawing.SizeF(1392.997F, 15F);
            // 
            // xrTableRow10
            // 
            this.xrTableRow10.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell123,
            this.xrTableCell124,
            this.xrTableCell125,
            this.xrTableCell126,
            this.xrTableCell127,
            this.xrTableCell128,
            this.xrTableCell129,
            this.xrTableCell130,
            this.xrTableCell131,
            this.xrTableCell132,
            this.xrTableCell133,
            this.xrTableCell134,
            this.xrTableCell135,
            this.xrTableCell136});
            this.xrTableRow10.Dpi = 100F;
            this.xrTableRow10.Name = "xrTableRow10";
            this.xrTableRow10.Weight = 11.5D;
            // 
            // xrTableCell123
            // 
            this.xrTableCell123.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetailAcnt.DescriptionDetail")});
            this.xrTableCell123.Dpi = 100F;
            this.xrTableCell123.Font = new System.Drawing.Font("Arial", 7.5F);
            this.xrTableCell123.Name = "xrTableCell123";
            this.xrTableCell123.StylePriority.UseFont = false;
            this.xrTableCell123.Weight = 0.044742444917237875D;
            // 
            // xrTableCell124
            // 
            this.xrTableCell124.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetailAcnt.Amount_01", "{0:n}")});
            this.xrTableCell124.Dpi = 100F;
            this.xrTableCell124.Font = new System.Drawing.Font("Arial", 7.5F);
            this.xrTableCell124.Name = "xrTableCell124";
            this.xrTableCell124.StylePriority.UseFont = false;
            this.xrTableCell124.StylePriority.UseTextAlignment = false;
            this.xrTableCell124.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell124.Weight = 0.011456676506122643D;
            // 
            // xrTableCell125
            // 
            this.xrTableCell125.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetailAcnt.Amount_02", "{0:n}")});
            this.xrTableCell125.Dpi = 100F;
            this.xrTableCell125.Font = new System.Drawing.Font("Arial", 7.5F);
            this.xrTableCell125.Name = "xrTableCell125";
            this.xrTableCell125.StylePriority.UseFont = false;
            this.xrTableCell125.StylePriority.UseTextAlignment = false;
            this.xrTableCell125.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell125.Weight = 0.011456439447518315D;
            // 
            // xrTableCell126
            // 
            this.xrTableCell126.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetailAcnt.Amount_03", "{0:n}")});
            this.xrTableCell126.Dpi = 100F;
            this.xrTableCell126.Font = new System.Drawing.Font("Arial", 7.5F);
            this.xrTableCell126.Name = "xrTableCell126";
            this.xrTableCell126.StylePriority.UseFont = false;
            this.xrTableCell126.StylePriority.UseTextAlignment = false;
            this.xrTableCell126.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell126.Weight = 0.01145644711665144D;
            // 
            // xrTableCell127
            // 
            this.xrTableCell127.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetailAcnt.Amount_04", "{0:n}")});
            this.xrTableCell127.Dpi = 100F;
            this.xrTableCell127.Font = new System.Drawing.Font("Arial", 7.5F);
            this.xrTableCell127.Name = "xrTableCell127";
            this.xrTableCell127.StylePriority.UseFont = false;
            this.xrTableCell127.StylePriority.UseTextAlignment = false;
            this.xrTableCell127.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell127.Weight = 0.011456464340136261D;
            // 
            // xrTableCell128
            // 
            this.xrTableCell128.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetailAcnt.Amount_05", "{0:n}")});
            this.xrTableCell128.Dpi = 100F;
            this.xrTableCell128.Font = new System.Drawing.Font("Arial", 7.5F);
            this.xrTableCell128.Name = "xrTableCell128";
            this.xrTableCell128.StylePriority.UseFont = false;
            this.xrTableCell128.StylePriority.UseTextAlignment = false;
            this.xrTableCell128.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell128.Weight = 0.011456379748427982D;
            // 
            // xrTableCell129
            // 
            this.xrTableCell129.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetailAcnt.Amount_06", "{0:n}")});
            this.xrTableCell129.Dpi = 100F;
            this.xrTableCell129.Font = new System.Drawing.Font("Arial", 7.5F);
            this.xrTableCell129.Name = "xrTableCell129";
            this.xrTableCell129.StylePriority.UseFont = false;
            this.xrTableCell129.StylePriority.UseTextAlignment = false;
            this.xrTableCell129.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell129.Weight = 0.011456481623145959D;
            // 
            // xrTableCell130
            // 
            this.xrTableCell130.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetailAcnt.Amount_07", "{0:n}")});
            this.xrTableCell130.Dpi = 100F;
            this.xrTableCell130.Font = new System.Drawing.Font("Arial", 7.5F);
            this.xrTableCell130.Name = "xrTableCell130";
            this.xrTableCell130.StylePriority.UseFont = false;
            this.xrTableCell130.StylePriority.UseTextAlignment = false;
            this.xrTableCell130.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell130.Weight = 0.011456448104521738D;
            // 
            // xrTableCell131
            // 
            this.xrTableCell131.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetailAcnt.Amount_08", "{0:n}")});
            this.xrTableCell131.Dpi = 100F;
            this.xrTableCell131.Font = new System.Drawing.Font("Arial", 7.5F);
            this.xrTableCell131.Name = "xrTableCell131";
            this.xrTableCell131.StylePriority.UseFont = false;
            this.xrTableCell131.StylePriority.UseTextAlignment = false;
            this.xrTableCell131.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell131.Weight = 0.01145656843549997D;
            // 
            // xrTableCell132
            // 
            this.xrTableCell132.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetailAcnt.Amount_09", "{0:n}")});
            this.xrTableCell132.Dpi = 100F;
            this.xrTableCell132.Font = new System.Drawing.Font("Arial", 7.5F);
            this.xrTableCell132.Name = "xrTableCell132";
            this.xrTableCell132.StylePriority.UseFont = false;
            this.xrTableCell132.StylePriority.UseTextAlignment = false;
            this.xrTableCell132.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell132.Weight = 0.011462360890095221D;
            // 
            // xrTableCell133
            // 
            this.xrTableCell133.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetailAcnt.Amount_10", "{0:n}")});
            this.xrTableCell133.Dpi = 100F;
            this.xrTableCell133.Font = new System.Drawing.Font("Arial", 7.5F);
            this.xrTableCell133.Name = "xrTableCell133";
            this.xrTableCell133.StylePriority.UseFont = false;
            this.xrTableCell133.StylePriority.UseTextAlignment = false;
            this.xrTableCell133.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell133.Weight = 0.011450439867777159D;
            // 
            // xrTableCell134
            // 
            this.xrTableCell134.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetailAcnt.Amount_11", "{0:n}")});
            this.xrTableCell134.Dpi = 100F;
            this.xrTableCell134.Font = new System.Drawing.Font("Arial", 7.5F);
            this.xrTableCell134.Name = "xrTableCell134";
            this.xrTableCell134.StylePriority.UseFont = false;
            this.xrTableCell134.StylePriority.UseTextAlignment = false;
            this.xrTableCell134.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell134.Weight = 0.011455021519186374D;
            // 
            // xrTableCell135
            // 
            this.xrTableCell135.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetailAcnt.Amount_12", "{0:n}")});
            this.xrTableCell135.Dpi = 100F;
            this.xrTableCell135.Font = new System.Drawing.Font("Arial", 7.5F);
            this.xrTableCell135.Name = "xrTableCell135";
            this.xrTableCell135.StylePriority.UseFont = false;
            this.xrTableCell135.StylePriority.UseTextAlignment = false;
            xrSummary56.FormatString = "{0:#,0}";
            this.xrTableCell135.Summary = xrSummary56;
            this.xrTableCell135.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell135.Weight = 0.01145697336366436D;
            // 
            // xrTableCell136
            // 
            this.xrTableCell136.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetailAcnt.Amount", "{0:n}")});
            this.xrTableCell136.Dpi = 100F;
            this.xrTableCell136.Font = new System.Drawing.Font("Arial", 7.5F);
            this.xrTableCell136.Name = "xrTableCell136";
            this.xrTableCell136.StylePriority.UseFont = false;
            this.xrTableCell136.StylePriority.UseTextAlignment = false;
            this.xrTableCell136.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell136.Weight = 0.011457284165937842D;
            // 
            // GroupFooter5
            // 
            this.GroupFooter5.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable17});
            this.GroupFooter5.Dpi = 100F;
            this.GroupFooter5.HeightF = 29.16667F;
            this.GroupFooter5.Name = "GroupFooter5";
            // 
            // xrTable17
            // 
            this.xrTable17.Dpi = 100F;
            this.xrTable17.LocationFloat = new DevExpress.Utils.PointFloat(10F, 0F);
            this.xrTable17.Name = "xrTable17";
            this.xrTable17.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow11});
            this.xrTable17.SizeF = new System.Drawing.SizeF(1392.997F, 15F);
            // 
            // xrTableRow11
            // 
            this.xrTableRow11.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell137,
            this.xrTableCell138,
            this.xrTableCell139,
            this.xrTableCell140,
            this.xrTableCell141,
            this.xrTableCell142,
            this.xrTableCell143,
            this.xrTableCell144,
            this.xrTableCell145,
            this.xrTableCell146,
            this.xrTableCell147,
            this.xrTableCell148,
            this.xrTableCell149,
            this.xrTableCell150});
            this.xrTableRow11.Dpi = 100F;
            this.xrTableRow11.Name = "xrTableRow11";
            this.xrTableRow11.Weight = 11.5D;
            // 
            // xrTableCell137
            // 
            this.xrTableCell137.Dpi = 100F;
            this.xrTableCell137.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell137.Name = "xrTableCell137";
            this.xrTableCell137.StylePriority.UseFont = false;
            this.xrTableCell137.Text = "Total Selling Administrative and General";
            this.xrTableCell137.Weight = 0.044742444870609452D;
            // 
            // xrTableCell138
            // 
            this.xrTableCell138.Borders = DevExpress.XtraPrinting.BorderSide.Top;
            this.xrTableCell138.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetailAcnt.Amount_01", "{0:n}")});
            this.xrTableCell138.Dpi = 100F;
            this.xrTableCell138.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell138.Name = "xrTableCell138";
            this.xrTableCell138.StylePriority.UseBorders = false;
            this.xrTableCell138.StylePriority.UseFont = false;
            this.xrTableCell138.StylePriority.UseTextAlignment = false;
            this.xrTableCell138.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell138.Weight = 0.011456676506122643D;
            // 
            // xrTableCell139
            // 
            this.xrTableCell139.Borders = DevExpress.XtraPrinting.BorderSide.Top;
            this.xrTableCell139.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetailAcnt.Amount_02", "{0:n}")});
            this.xrTableCell139.Dpi = 100F;
            this.xrTableCell139.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell139.Name = "xrTableCell139";
            this.xrTableCell139.StylePriority.UseBorders = false;
            this.xrTableCell139.StylePriority.UseFont = false;
            this.xrTableCell139.StylePriority.UseTextAlignment = false;
            this.xrTableCell139.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell139.Weight = 0.011456439447518315D;
            // 
            // xrTableCell140
            // 
            this.xrTableCell140.Borders = DevExpress.XtraPrinting.BorderSide.Top;
            this.xrTableCell140.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetailAcnt.Amount_03", "{0:n}")});
            this.xrTableCell140.Dpi = 100F;
            this.xrTableCell140.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell140.Name = "xrTableCell140";
            this.xrTableCell140.StylePriority.UseBorders = false;
            this.xrTableCell140.StylePriority.UseFont = false;
            this.xrTableCell140.StylePriority.UseTextAlignment = false;
            this.xrTableCell140.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell140.Weight = 0.01145644711665144D;
            // 
            // xrTableCell141
            // 
            this.xrTableCell141.Borders = DevExpress.XtraPrinting.BorderSide.Top;
            this.xrTableCell141.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetailAcnt.Amount_04", "{0:n}")});
            this.xrTableCell141.Dpi = 100F;
            this.xrTableCell141.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell141.Name = "xrTableCell141";
            this.xrTableCell141.StylePriority.UseBorders = false;
            this.xrTableCell141.StylePriority.UseFont = false;
            this.xrTableCell141.StylePriority.UseTextAlignment = false;
            this.xrTableCell141.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell141.Weight = 0.011456464340136261D;
            // 
            // xrTableCell142
            // 
            this.xrTableCell142.Borders = DevExpress.XtraPrinting.BorderSide.Top;
            this.xrTableCell142.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetailAcnt.Amount_05", "{0:n}")});
            this.xrTableCell142.Dpi = 100F;
            this.xrTableCell142.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell142.Name = "xrTableCell142";
            this.xrTableCell142.StylePriority.UseBorders = false;
            this.xrTableCell142.StylePriority.UseFont = false;
            this.xrTableCell142.StylePriority.UseTextAlignment = false;
            this.xrTableCell142.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell142.Weight = 0.011456379748427982D;
            // 
            // xrTableCell143
            // 
            this.xrTableCell143.Borders = DevExpress.XtraPrinting.BorderSide.Top;
            this.xrTableCell143.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetailAcnt.Amount_06", "{0:n}")});
            this.xrTableCell143.Dpi = 100F;
            this.xrTableCell143.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell143.Name = "xrTableCell143";
            this.xrTableCell143.StylePriority.UseBorders = false;
            this.xrTableCell143.StylePriority.UseFont = false;
            this.xrTableCell143.StylePriority.UseTextAlignment = false;
            this.xrTableCell143.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell143.Weight = 0.011456481623145959D;
            // 
            // xrTableCell144
            // 
            this.xrTableCell144.Borders = DevExpress.XtraPrinting.BorderSide.Top;
            this.xrTableCell144.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetailAcnt.Amount_07", "{0:n}")});
            this.xrTableCell144.Dpi = 100F;
            this.xrTableCell144.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell144.Name = "xrTableCell144";
            this.xrTableCell144.StylePriority.UseBorders = false;
            this.xrTableCell144.StylePriority.UseFont = false;
            this.xrTableCell144.StylePriority.UseTextAlignment = false;
            this.xrTableCell144.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell144.Weight = 0.011456448104521738D;
            // 
            // xrTableCell145
            // 
            this.xrTableCell145.Borders = DevExpress.XtraPrinting.BorderSide.Top;
            this.xrTableCell145.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetailAcnt.Amount_08", "{0:n}")});
            this.xrTableCell145.Dpi = 100F;
            this.xrTableCell145.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell145.Name = "xrTableCell145";
            this.xrTableCell145.StylePriority.UseBorders = false;
            this.xrTableCell145.StylePriority.UseFont = false;
            this.xrTableCell145.StylePriority.UseTextAlignment = false;
            this.xrTableCell145.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell145.Weight = 0.01145656843549997D;
            // 
            // xrTableCell146
            // 
            this.xrTableCell146.Borders = DevExpress.XtraPrinting.BorderSide.Top;
            this.xrTableCell146.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetailAcnt.Amount_09", "{0:n}")});
            this.xrTableCell146.Dpi = 100F;
            this.xrTableCell146.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell146.Name = "xrTableCell146";
            this.xrTableCell146.StylePriority.UseBorders = false;
            this.xrTableCell146.StylePriority.UseFont = false;
            this.xrTableCell146.StylePriority.UseTextAlignment = false;
            this.xrTableCell146.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell146.Weight = 0.011462360890095221D;
            // 
            // xrTableCell147
            // 
            this.xrTableCell147.Borders = DevExpress.XtraPrinting.BorderSide.Top;
            this.xrTableCell147.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetailAcnt.Amount_10", "{0:n}")});
            this.xrTableCell147.Dpi = 100F;
            this.xrTableCell147.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell147.Name = "xrTableCell147";
            this.xrTableCell147.StylePriority.UseBorders = false;
            this.xrTableCell147.StylePriority.UseFont = false;
            this.xrTableCell147.StylePriority.UseTextAlignment = false;
            this.xrTableCell147.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell147.Weight = 0.011450439867777159D;
            // 
            // xrTableCell148
            // 
            this.xrTableCell148.Borders = DevExpress.XtraPrinting.BorderSide.Top;
            this.xrTableCell148.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetailAcnt.Amount_11", "{0:n}")});
            this.xrTableCell148.Dpi = 100F;
            this.xrTableCell148.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell148.Name = "xrTableCell148";
            this.xrTableCell148.StylePriority.UseBorders = false;
            this.xrTableCell148.StylePriority.UseFont = false;
            this.xrTableCell148.StylePriority.UseTextAlignment = false;
            this.xrTableCell148.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell148.Weight = 0.011455021519186374D;
            // 
            // xrTableCell149
            // 
            this.xrTableCell149.Borders = DevExpress.XtraPrinting.BorderSide.Top;
            this.xrTableCell149.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetailAcnt.Amount_12", "{0:n}")});
            this.xrTableCell149.Dpi = 100F;
            this.xrTableCell149.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell149.Name = "xrTableCell149";
            this.xrTableCell149.StylePriority.UseBorders = false;
            this.xrTableCell149.StylePriority.UseFont = false;
            this.xrTableCell149.StylePriority.UseTextAlignment = false;
            xrSummary57.FormatString = "{0:#,0}";
            this.xrTableCell149.Summary = xrSummary57;
            this.xrTableCell149.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell149.Weight = 0.01145697336366436D;
            // 
            // xrTableCell150
            // 
            this.xrTableCell150.Borders = DevExpress.XtraPrinting.BorderSide.Top;
            this.xrTableCell150.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetailAcnt.Amount", "{0:n}")});
            this.xrTableCell150.Dpi = 100F;
            this.xrTableCell150.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell150.Name = "xrTableCell150";
            this.xrTableCell150.StylePriority.UseBorders = false;
            this.xrTableCell150.StylePriority.UseFont = false;
            this.xrTableCell150.StylePriority.UseTextAlignment = false;
            this.xrTableCell150.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell150.Weight = 0.011457284165937842D;
            // 
            // DetailReport5
            // 
            this.DetailReport5.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail6,
            this.GroupHeader3,
            this.GroupFooter6});
            this.DetailReport5.DataMember = "sp_report_ContributionMarginDetailAcnt2";
            this.DetailReport5.DataSource = this.sqlDataSource1;
            this.DetailReport5.Dpi = 100F;
            this.DetailReport5.Level = 3;
            this.DetailReport5.Name = "DetailReport5";
            // 
            // Detail6
            // 
            this.Detail6.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable1});
            this.Detail6.Dpi = 100F;
            this.Detail6.HeightF = 15F;
            this.Detail6.Name = "Detail6";
            // 
            // xrTable1
            // 
            this.xrTable1.Dpi = 100F;
            this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(10F, 0F);
            this.xrTable1.Name = "xrTable1";
            this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1});
            this.xrTable1.SizeF = new System.Drawing.SizeF(1392.997F, 15F);
            // 
            // xrTableRow1
            // 
            this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell1,
            this.xrTableCell2,
            this.xrTableCell3,
            this.xrTableCell4,
            this.xrTableCell5,
            this.xrTableCell6,
            this.xrTableCell7,
            this.xrTableCell8,
            this.xrTableCell9,
            this.xrTableCell10,
            this.xrTableCell11,
            this.xrTableCell12,
            this.xrTableCell26,
            this.xrTableCell27});
            this.xrTableRow1.Dpi = 100F;
            this.xrTableRow1.Name = "xrTableRow1";
            this.xrTableRow1.Weight = 11.5D;
            // 
            // xrTableCell1
            // 
            this.xrTableCell1.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetailAcnt2.DescriptionDetail")});
            this.xrTableCell1.Dpi = 100F;
            this.xrTableCell1.Font = new System.Drawing.Font("Arial", 7.5F);
            this.xrTableCell1.Name = "xrTableCell1";
            this.xrTableCell1.StylePriority.UseFont = false;
            this.xrTableCell1.Weight = 0.044742444917237875D;
            // 
            // xrTableCell2
            // 
            this.xrTableCell2.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetailAcnt2.Amount_01", "{0:n}")});
            this.xrTableCell2.Dpi = 100F;
            this.xrTableCell2.Font = new System.Drawing.Font("Arial", 7.5F);
            this.xrTableCell2.Name = "xrTableCell2";
            this.xrTableCell2.StylePriority.UseFont = false;
            this.xrTableCell2.StylePriority.UseTextAlignment = false;
            this.xrTableCell2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell2.Weight = 0.011456676506122643D;
            // 
            // xrTableCell3
            // 
            this.xrTableCell3.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetailAcnt2.Amount_02", "{0:n}")});
            this.xrTableCell3.Dpi = 100F;
            this.xrTableCell3.Font = new System.Drawing.Font("Arial", 7.5F);
            this.xrTableCell3.Name = "xrTableCell3";
            this.xrTableCell3.StylePriority.UseFont = false;
            this.xrTableCell3.StylePriority.UseTextAlignment = false;
            this.xrTableCell3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell3.Weight = 0.011456439447518315D;
            // 
            // xrTableCell4
            // 
            this.xrTableCell4.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetailAcnt2.Amount_03", "{0:n}")});
            this.xrTableCell4.Dpi = 100F;
            this.xrTableCell4.Font = new System.Drawing.Font("Arial", 7.5F);
            this.xrTableCell4.Name = "xrTableCell4";
            this.xrTableCell4.StylePriority.UseFont = false;
            this.xrTableCell4.StylePriority.UseTextAlignment = false;
            this.xrTableCell4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell4.Weight = 0.01145644711665144D;
            // 
            // xrTableCell5
            // 
            this.xrTableCell5.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetailAcnt2.Amount_04", "{0:n}")});
            this.xrTableCell5.Dpi = 100F;
            this.xrTableCell5.Font = new System.Drawing.Font("Arial", 7.5F);
            this.xrTableCell5.Name = "xrTableCell5";
            this.xrTableCell5.StylePriority.UseFont = false;
            this.xrTableCell5.StylePriority.UseTextAlignment = false;
            this.xrTableCell5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell5.Weight = 0.011456464340136261D;
            // 
            // xrTableCell6
            // 
            this.xrTableCell6.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetailAcnt2.Amount_05", "{0:n}")});
            this.xrTableCell6.Dpi = 100F;
            this.xrTableCell6.Font = new System.Drawing.Font("Arial", 7.5F);
            this.xrTableCell6.Name = "xrTableCell6";
            this.xrTableCell6.StylePriority.UseFont = false;
            this.xrTableCell6.StylePriority.UseTextAlignment = false;
            this.xrTableCell6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell6.Weight = 0.011456379748427982D;
            // 
            // xrTableCell7
            // 
            this.xrTableCell7.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetailAcnt2.Amount_06", "{0:n}")});
            this.xrTableCell7.Dpi = 100F;
            this.xrTableCell7.Font = new System.Drawing.Font("Arial", 7.5F);
            this.xrTableCell7.Name = "xrTableCell7";
            this.xrTableCell7.StylePriority.UseFont = false;
            this.xrTableCell7.StylePriority.UseTextAlignment = false;
            this.xrTableCell7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell7.Weight = 0.011456481623145959D;
            // 
            // xrTableCell8
            // 
            this.xrTableCell8.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetailAcnt2.Amount_07", "{0:n}")});
            this.xrTableCell8.Dpi = 100F;
            this.xrTableCell8.Font = new System.Drawing.Font("Arial", 7.5F);
            this.xrTableCell8.Name = "xrTableCell8";
            this.xrTableCell8.StylePriority.UseFont = false;
            this.xrTableCell8.StylePriority.UseTextAlignment = false;
            this.xrTableCell8.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell8.Weight = 0.011456448104521738D;
            // 
            // xrTableCell9
            // 
            this.xrTableCell9.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetailAcnt2.Amount_08", "{0:n}")});
            this.xrTableCell9.Dpi = 100F;
            this.xrTableCell9.Font = new System.Drawing.Font("Arial", 7.5F);
            this.xrTableCell9.Name = "xrTableCell9";
            this.xrTableCell9.StylePriority.UseFont = false;
            this.xrTableCell9.StylePriority.UseTextAlignment = false;
            this.xrTableCell9.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell9.Weight = 0.01145656843549997D;
            // 
            // xrTableCell10
            // 
            this.xrTableCell10.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetailAcnt2.Amount_09", "{0:n}")});
            this.xrTableCell10.Dpi = 100F;
            this.xrTableCell10.Font = new System.Drawing.Font("Arial", 7.5F);
            this.xrTableCell10.Name = "xrTableCell10";
            this.xrTableCell10.StylePriority.UseFont = false;
            this.xrTableCell10.StylePriority.UseTextAlignment = false;
            this.xrTableCell10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell10.Weight = 0.011462360890095221D;
            // 
            // xrTableCell11
            // 
            this.xrTableCell11.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetailAcnt2.Amount_10", "{0:n}")});
            this.xrTableCell11.Dpi = 100F;
            this.xrTableCell11.Font = new System.Drawing.Font("Arial", 7.5F);
            this.xrTableCell11.Name = "xrTableCell11";
            this.xrTableCell11.StylePriority.UseFont = false;
            this.xrTableCell11.StylePriority.UseTextAlignment = false;
            this.xrTableCell11.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell11.Weight = 0.011450439867777159D;
            // 
            // xrTableCell12
            // 
            this.xrTableCell12.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetailAcnt2.Amount_11", "{0:n}")});
            this.xrTableCell12.Dpi = 100F;
            this.xrTableCell12.Font = new System.Drawing.Font("Arial", 7.5F);
            this.xrTableCell12.Name = "xrTableCell12";
            this.xrTableCell12.StylePriority.UseFont = false;
            this.xrTableCell12.StylePriority.UseTextAlignment = false;
            this.xrTableCell12.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell12.Weight = 0.011455021519186374D;
            // 
            // xrTableCell26
            // 
            this.xrTableCell26.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetailAcnt2.Amount_12", "{0:n}")});
            this.xrTableCell26.Dpi = 100F;
            this.xrTableCell26.Font = new System.Drawing.Font("Arial", 7.5F);
            this.xrTableCell26.Name = "xrTableCell26";
            this.xrTableCell26.StylePriority.UseFont = false;
            this.xrTableCell26.StylePriority.UseTextAlignment = false;
            xrSummary58.FormatString = "{0:#,0}";
            this.xrTableCell26.Summary = xrSummary58;
            this.xrTableCell26.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell26.Weight = 0.01145697336366436D;
            // 
            // xrTableCell27
            // 
            this.xrTableCell27.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetailAcnt2.Amount", "{0:n}")});
            this.xrTableCell27.Dpi = 100F;
            this.xrTableCell27.Font = new System.Drawing.Font("Arial", 7.5F);
            this.xrTableCell27.Name = "xrTableCell27";
            this.xrTableCell27.StylePriority.UseFont = false;
            this.xrTableCell27.StylePriority.UseTextAlignment = false;
            this.xrTableCell27.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell27.Weight = 0.011457284165937842D;
            // 
            // GroupHeader3
            // 
            this.GroupHeader3.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable22,
            this.xrLabel8});
            this.GroupHeader3.Dpi = 100F;
            this.GroupHeader3.HeightF = 29.99996F;
            this.GroupHeader3.Name = "GroupHeader3";
            // 
            // xrTable22
            // 
            this.xrTable22.Dpi = 100F;
            this.xrTable22.LocationFloat = new DevExpress.Utils.PointFloat(10.00404F, 15.62494F);
            this.xrTable22.Name = "xrTable22";
            this.xrTable22.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow16});
            this.xrTable22.SizeF = new System.Drawing.SizeF(332.2917F, 14.37502F);
            // 
            // xrTableRow16
            // 
            this.xrTableRow16.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell180});
            this.xrTableRow16.Dpi = 100F;
            this.xrTableRow16.Name = "xrTableRow16";
            this.xrTableRow16.Weight = 1D;
            // 
            // xrTableCell180
            // 
            this.xrTableCell180.Dpi = 100F;
            this.xrTableCell180.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell180.Name = "xrTableCell180";
            this.xrTableCell180.StylePriority.UseFont = false;
            this.xrTableCell180.Text = "Overhead";
            this.xrTableCell180.Weight = 1D;
            // 
            // xrLabel8
            // 
            this.xrLabel8.Dpi = 100F;
            this.xrLabel8.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrLabel8.LocationFloat = new DevExpress.Utils.PointFloat(0.002861023F, 0F);
            this.xrLabel8.Name = "xrLabel8";
            this.xrLabel8.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel8.SizeF = new System.Drawing.SizeF(331.8086F, 15F);
            this.xrLabel8.StylePriority.UseFont = false;
            this.xrLabel8.StylePriority.UseTextAlignment = false;
            this.xrLabel8.Text = "Less: Fixed Cost";
            this.xrLabel8.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // GroupFooter6
            // 
            this.GroupFooter6.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable23,
            this.xrTable18});
            this.GroupFooter6.Dpi = 100F;
            this.GroupFooter6.HeightF = 46.04165F;
            this.GroupFooter6.Name = "GroupFooter6";
            // 
            // xrTable23
            // 
            this.xrTable23.Dpi = 100F;
            this.xrTable23.LocationFloat = new DevExpress.Utils.PointFloat(10.00404F, 31.66663F);
            this.xrTable23.Name = "xrTable23";
            this.xrTable23.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow17});
            this.xrTable23.SizeF = new System.Drawing.SizeF(332.2917F, 14.37502F);
            // 
            // xrTableRow17
            // 
            this.xrTableRow17.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell182});
            this.xrTableRow17.Dpi = 100F;
            this.xrTableRow17.Name = "xrTableRow17";
            this.xrTableRow17.Weight = 1D;
            // 
            // xrTableCell182
            // 
            this.xrTableCell182.Dpi = 100F;
            this.xrTableCell182.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTableCell182.Name = "xrTableCell182";
            this.xrTableCell182.StylePriority.UseFont = false;
            this.xrTableCell182.Text = "Selling, Administrative and General";
            this.xrTableCell182.Weight = 1D;
            // 
            // xrTable18
            // 
            this.xrTable18.Dpi = 100F;
            this.xrTable18.LocationFloat = new DevExpress.Utils.PointFloat(10.00403F, 0F);
            this.xrTable18.Name = "xrTable18";
            this.xrTable18.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow12});
            this.xrTable18.SizeF = new System.Drawing.SizeF(1392.997F, 15F);
            // 
            // xrTableRow12
            // 
            this.xrTableRow12.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell151,
            this.xrTableCell152,
            this.xrTableCell153,
            this.xrTableCell154,
            this.xrTableCell155,
            this.xrTableCell156,
            this.xrTableCell157,
            this.xrTableCell158,
            this.xrTableCell159,
            this.xrTableCell160,
            this.xrTableCell161,
            this.xrTableCell162,
            this.xrTableCell163,
            this.xrTableCell164});
            this.xrTableRow12.Dpi = 100F;
            this.xrTableRow12.Name = "xrTableRow12";
            this.xrTableRow12.Weight = 11.5D;
            // 
            // xrTableCell151
            // 
            this.xrTableCell151.Dpi = 100F;
            this.xrTableCell151.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell151.Name = "xrTableCell151";
            this.xrTableCell151.StylePriority.UseFont = false;
            this.xrTableCell151.Text = "Total Overhead";
            this.xrTableCell151.Weight = 0.044742444870609452D;
            // 
            // xrTableCell152
            // 
            this.xrTableCell152.Borders = DevExpress.XtraPrinting.BorderSide.Top;
            this.xrTableCell152.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetailAcnt2.Amount_01", "{0:n}")});
            this.xrTableCell152.Dpi = 100F;
            this.xrTableCell152.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell152.Name = "xrTableCell152";
            this.xrTableCell152.StylePriority.UseBorders = false;
            this.xrTableCell152.StylePriority.UseFont = false;
            this.xrTableCell152.StylePriority.UseTextAlignment = false;
            this.xrTableCell152.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell152.Weight = 0.011456676506122643D;
            // 
            // xrTableCell153
            // 
            this.xrTableCell153.Borders = DevExpress.XtraPrinting.BorderSide.Top;
            this.xrTableCell153.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetailAcnt2.Amount_02", "{0:n}")});
            this.xrTableCell153.Dpi = 100F;
            this.xrTableCell153.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell153.Name = "xrTableCell153";
            this.xrTableCell153.StylePriority.UseBorders = false;
            this.xrTableCell153.StylePriority.UseFont = false;
            this.xrTableCell153.StylePriority.UseTextAlignment = false;
            this.xrTableCell153.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell153.Weight = 0.011456439447518315D;
            // 
            // xrTableCell154
            // 
            this.xrTableCell154.Borders = DevExpress.XtraPrinting.BorderSide.Top;
            this.xrTableCell154.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetailAcnt2.Amount_03", "{0:n}")});
            this.xrTableCell154.Dpi = 100F;
            this.xrTableCell154.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell154.Name = "xrTableCell154";
            this.xrTableCell154.StylePriority.UseBorders = false;
            this.xrTableCell154.StylePriority.UseFont = false;
            this.xrTableCell154.StylePriority.UseTextAlignment = false;
            this.xrTableCell154.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell154.Weight = 0.01145644711665144D;
            // 
            // xrTableCell155
            // 
            this.xrTableCell155.Borders = DevExpress.XtraPrinting.BorderSide.Top;
            this.xrTableCell155.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetailAcnt2.Amount_04", "{0:n}")});
            this.xrTableCell155.Dpi = 100F;
            this.xrTableCell155.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell155.Name = "xrTableCell155";
            this.xrTableCell155.StylePriority.UseBorders = false;
            this.xrTableCell155.StylePriority.UseFont = false;
            this.xrTableCell155.StylePriority.UseTextAlignment = false;
            this.xrTableCell155.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell155.Weight = 0.011456464340136261D;
            // 
            // xrTableCell156
            // 
            this.xrTableCell156.Borders = DevExpress.XtraPrinting.BorderSide.Top;
            this.xrTableCell156.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetailAcnt2.Amount_05", "{0:n}")});
            this.xrTableCell156.Dpi = 100F;
            this.xrTableCell156.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell156.Name = "xrTableCell156";
            this.xrTableCell156.StylePriority.UseBorders = false;
            this.xrTableCell156.StylePriority.UseFont = false;
            this.xrTableCell156.StylePriority.UseTextAlignment = false;
            this.xrTableCell156.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell156.Weight = 0.011456379748427982D;
            // 
            // xrTableCell157
            // 
            this.xrTableCell157.Borders = DevExpress.XtraPrinting.BorderSide.Top;
            this.xrTableCell157.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetailAcnt2.Amount_06", "{0:n}")});
            this.xrTableCell157.Dpi = 100F;
            this.xrTableCell157.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell157.Name = "xrTableCell157";
            this.xrTableCell157.StylePriority.UseBorders = false;
            this.xrTableCell157.StylePriority.UseFont = false;
            this.xrTableCell157.StylePriority.UseTextAlignment = false;
            this.xrTableCell157.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell157.Weight = 0.011456481623145959D;
            // 
            // xrTableCell158
            // 
            this.xrTableCell158.Borders = DevExpress.XtraPrinting.BorderSide.Top;
            this.xrTableCell158.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetailAcnt2.Amount_07", "{0:n}")});
            this.xrTableCell158.Dpi = 100F;
            this.xrTableCell158.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell158.Name = "xrTableCell158";
            this.xrTableCell158.StylePriority.UseBorders = false;
            this.xrTableCell158.StylePriority.UseFont = false;
            this.xrTableCell158.StylePriority.UseTextAlignment = false;
            this.xrTableCell158.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell158.Weight = 0.011456448104521738D;
            // 
            // xrTableCell159
            // 
            this.xrTableCell159.Borders = DevExpress.XtraPrinting.BorderSide.Top;
            this.xrTableCell159.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetailAcnt2.Amount_08", "{0:n}")});
            this.xrTableCell159.Dpi = 100F;
            this.xrTableCell159.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell159.Name = "xrTableCell159";
            this.xrTableCell159.StylePriority.UseBorders = false;
            this.xrTableCell159.StylePriority.UseFont = false;
            this.xrTableCell159.StylePriority.UseTextAlignment = false;
            this.xrTableCell159.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell159.Weight = 0.01145656843549997D;
            // 
            // xrTableCell160
            // 
            this.xrTableCell160.Borders = DevExpress.XtraPrinting.BorderSide.Top;
            this.xrTableCell160.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetailAcnt2.Amount_09", "{0:n}")});
            this.xrTableCell160.Dpi = 100F;
            this.xrTableCell160.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell160.Name = "xrTableCell160";
            this.xrTableCell160.StylePriority.UseBorders = false;
            this.xrTableCell160.StylePriority.UseFont = false;
            this.xrTableCell160.StylePriority.UseTextAlignment = false;
            this.xrTableCell160.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell160.Weight = 0.011462360890095221D;
            // 
            // xrTableCell161
            // 
            this.xrTableCell161.Borders = DevExpress.XtraPrinting.BorderSide.Top;
            this.xrTableCell161.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetailAcnt2.Amount_10", "{0:n}")});
            this.xrTableCell161.Dpi = 100F;
            this.xrTableCell161.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell161.Name = "xrTableCell161";
            this.xrTableCell161.StylePriority.UseBorders = false;
            this.xrTableCell161.StylePriority.UseFont = false;
            this.xrTableCell161.StylePriority.UseTextAlignment = false;
            this.xrTableCell161.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell161.Weight = 0.011450439867777159D;
            // 
            // xrTableCell162
            // 
            this.xrTableCell162.Borders = DevExpress.XtraPrinting.BorderSide.Top;
            this.xrTableCell162.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetailAcnt2.Amount_11", "{0:n}")});
            this.xrTableCell162.Dpi = 100F;
            this.xrTableCell162.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell162.Name = "xrTableCell162";
            this.xrTableCell162.StylePriority.UseBorders = false;
            this.xrTableCell162.StylePriority.UseFont = false;
            this.xrTableCell162.StylePriority.UseTextAlignment = false;
            this.xrTableCell162.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell162.Weight = 0.011455021519186374D;
            // 
            // xrTableCell163
            // 
            this.xrTableCell163.Borders = DevExpress.XtraPrinting.BorderSide.Top;
            this.xrTableCell163.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetailAcnt2.Amount_12", "{0:n}")});
            this.xrTableCell163.Dpi = 100F;
            this.xrTableCell163.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell163.Name = "xrTableCell163";
            this.xrTableCell163.StylePriority.UseBorders = false;
            this.xrTableCell163.StylePriority.UseFont = false;
            this.xrTableCell163.StylePriority.UseTextAlignment = false;
            xrSummary59.FormatString = "{0:#,0}";
            this.xrTableCell163.Summary = xrSummary59;
            this.xrTableCell163.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell163.Weight = 0.01145697336366436D;
            // 
            // xrTableCell164
            // 
            this.xrTableCell164.Borders = DevExpress.XtraPrinting.BorderSide.Top;
            this.xrTableCell164.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetailAcnt2.Amount", "{0:n}")});
            this.xrTableCell164.Dpi = 100F;
            this.xrTableCell164.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell164.Name = "xrTableCell164";
            this.xrTableCell164.StylePriority.UseBorders = false;
            this.xrTableCell164.StylePriority.UseFont = false;
            this.xrTableCell164.StylePriority.UseTextAlignment = false;
            this.xrTableCell164.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell164.Weight = 0.011457284165937842D;
            // 
            // DetailReport6
            // 
            this.DetailReport6.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail7,
            this.GroupFooter7});
            this.DetailReport6.DataMember = "sp_report_ContributionMarginDetailAcnt3";
            this.DetailReport6.DataSource = this.sqlDataSource1;
            this.DetailReport6.Dpi = 100F;
            this.DetailReport6.Level = 4;
            this.DetailReport6.Name = "DetailReport6";
            // 
            // Detail7
            // 
            this.Detail7.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable19});
            this.Detail7.Dpi = 100F;
            this.Detail7.HeightF = 15F;
            this.Detail7.Name = "Detail7";
            // 
            // xrTable19
            // 
            this.xrTable19.Dpi = 100F;
            this.xrTable19.LocationFloat = new DevExpress.Utils.PointFloat(10F, 0F);
            this.xrTable19.Name = "xrTable19";
            this.xrTable19.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow13});
            this.xrTable19.SizeF = new System.Drawing.SizeF(1392.997F, 15F);
            // 
            // xrTableRow13
            // 
            this.xrTableRow13.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell165,
            this.xrTableCell166,
            this.xrTableCell167,
            this.xrTableCell168,
            this.xrTableCell169,
            this.xrTableCell170,
            this.xrTableCell171,
            this.xrTableCell172,
            this.xrTableCell173,
            this.xrTableCell174,
            this.xrTableCell175,
            this.xrTableCell176,
            this.xrTableCell177,
            this.xrTableCell178});
            this.xrTableRow13.Dpi = 100F;
            this.xrTableRow13.Name = "xrTableRow13";
            this.xrTableRow13.Weight = 11.5D;
            // 
            // xrTableCell165
            // 
            this.xrTableCell165.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetailAcnt3.DescriptionDetail")});
            this.xrTableCell165.Dpi = 100F;
            this.xrTableCell165.Font = new System.Drawing.Font("Arial", 7.5F);
            this.xrTableCell165.Name = "xrTableCell165";
            this.xrTableCell165.StylePriority.UseFont = false;
            this.xrTableCell165.Weight = 0.044742444917237875D;
            // 
            // xrTableCell166
            // 
            this.xrTableCell166.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetailAcnt3.Amount_01", "{0:n}")});
            this.xrTableCell166.Dpi = 100F;
            this.xrTableCell166.Font = new System.Drawing.Font("Arial", 7.5F);
            this.xrTableCell166.Name = "xrTableCell166";
            this.xrTableCell166.StylePriority.UseFont = false;
            this.xrTableCell166.StylePriority.UseTextAlignment = false;
            this.xrTableCell166.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell166.Weight = 0.011456676506122643D;
            // 
            // xrTableCell167
            // 
            this.xrTableCell167.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetailAcnt3.Amount_02", "{0:n}")});
            this.xrTableCell167.Dpi = 100F;
            this.xrTableCell167.Font = new System.Drawing.Font("Arial", 7.5F);
            this.xrTableCell167.Name = "xrTableCell167";
            this.xrTableCell167.StylePriority.UseFont = false;
            this.xrTableCell167.StylePriority.UseTextAlignment = false;
            this.xrTableCell167.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell167.Weight = 0.011456439447518315D;
            // 
            // xrTableCell168
            // 
            this.xrTableCell168.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetailAcnt3.Amount_03", "{0:n}")});
            this.xrTableCell168.Dpi = 100F;
            this.xrTableCell168.Font = new System.Drawing.Font("Arial", 7.5F);
            this.xrTableCell168.Name = "xrTableCell168";
            this.xrTableCell168.StylePriority.UseFont = false;
            this.xrTableCell168.StylePriority.UseTextAlignment = false;
            this.xrTableCell168.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell168.Weight = 0.01145644711665144D;
            // 
            // xrTableCell169
            // 
            this.xrTableCell169.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetailAcnt3.Amount_04", "{0:n}")});
            this.xrTableCell169.Dpi = 100F;
            this.xrTableCell169.Font = new System.Drawing.Font("Arial", 7.5F);
            this.xrTableCell169.Name = "xrTableCell169";
            this.xrTableCell169.StylePriority.UseFont = false;
            this.xrTableCell169.StylePriority.UseTextAlignment = false;
            this.xrTableCell169.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell169.Weight = 0.011456464340136261D;
            // 
            // xrTableCell170
            // 
            this.xrTableCell170.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetailAcnt3.Amount_05", "{0:n}")});
            this.xrTableCell170.Dpi = 100F;
            this.xrTableCell170.Font = new System.Drawing.Font("Arial", 7.5F);
            this.xrTableCell170.Name = "xrTableCell170";
            this.xrTableCell170.StylePriority.UseFont = false;
            this.xrTableCell170.StylePriority.UseTextAlignment = false;
            this.xrTableCell170.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell170.Weight = 0.011456379748427982D;
            // 
            // xrTableCell171
            // 
            this.xrTableCell171.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetailAcnt3.Amount_06", "{0:n}")});
            this.xrTableCell171.Dpi = 100F;
            this.xrTableCell171.Font = new System.Drawing.Font("Arial", 7.5F);
            this.xrTableCell171.Name = "xrTableCell171";
            this.xrTableCell171.StylePriority.UseFont = false;
            this.xrTableCell171.StylePriority.UseTextAlignment = false;
            this.xrTableCell171.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell171.Weight = 0.011456481623145959D;
            // 
            // xrTableCell172
            // 
            this.xrTableCell172.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetailAcnt3.Amount_07", "{0:n}")});
            this.xrTableCell172.Dpi = 100F;
            this.xrTableCell172.Font = new System.Drawing.Font("Arial", 7.5F);
            this.xrTableCell172.Name = "xrTableCell172";
            this.xrTableCell172.StylePriority.UseFont = false;
            this.xrTableCell172.StylePriority.UseTextAlignment = false;
            this.xrTableCell172.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell172.Weight = 0.011456448104521738D;
            // 
            // xrTableCell173
            // 
            this.xrTableCell173.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetailAcnt3.Amount_08", "{0:n}")});
            this.xrTableCell173.Dpi = 100F;
            this.xrTableCell173.Font = new System.Drawing.Font("Arial", 7.5F);
            this.xrTableCell173.Name = "xrTableCell173";
            this.xrTableCell173.StylePriority.UseFont = false;
            this.xrTableCell173.StylePriority.UseTextAlignment = false;
            this.xrTableCell173.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell173.Weight = 0.01145656843549997D;
            // 
            // xrTableCell174
            // 
            this.xrTableCell174.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetailAcnt3.Amount_09", "{0:n}")});
            this.xrTableCell174.Dpi = 100F;
            this.xrTableCell174.Font = new System.Drawing.Font("Arial", 7.5F);
            this.xrTableCell174.Name = "xrTableCell174";
            this.xrTableCell174.StylePriority.UseFont = false;
            this.xrTableCell174.StylePriority.UseTextAlignment = false;
            this.xrTableCell174.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell174.Weight = 0.011462360890095221D;
            // 
            // xrTableCell175
            // 
            this.xrTableCell175.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetailAcnt3.Amount_10", "{0:n}")});
            this.xrTableCell175.Dpi = 100F;
            this.xrTableCell175.Font = new System.Drawing.Font("Arial", 7.5F);
            this.xrTableCell175.Name = "xrTableCell175";
            this.xrTableCell175.StylePriority.UseFont = false;
            this.xrTableCell175.StylePriority.UseTextAlignment = false;
            this.xrTableCell175.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell175.Weight = 0.011450439867777159D;
            // 
            // xrTableCell176
            // 
            this.xrTableCell176.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetailAcnt3.Amount_11", "{0:n}")});
            this.xrTableCell176.Dpi = 100F;
            this.xrTableCell176.Font = new System.Drawing.Font("Arial", 7.5F);
            this.xrTableCell176.Name = "xrTableCell176";
            this.xrTableCell176.StylePriority.UseFont = false;
            this.xrTableCell176.StylePriority.UseTextAlignment = false;
            this.xrTableCell176.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell176.Weight = 0.011455021519186374D;
            // 
            // xrTableCell177
            // 
            this.xrTableCell177.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetailAcnt3.Amount_12", "{0:n}")});
            this.xrTableCell177.Dpi = 100F;
            this.xrTableCell177.Font = new System.Drawing.Font("Arial", 7.5F);
            this.xrTableCell177.Name = "xrTableCell177";
            this.xrTableCell177.StylePriority.UseFont = false;
            this.xrTableCell177.StylePriority.UseTextAlignment = false;
            xrSummary60.FormatString = "{0:#,0}";
            this.xrTableCell177.Summary = xrSummary60;
            this.xrTableCell177.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell177.Weight = 0.01145697336366436D;
            // 
            // xrTableCell178
            // 
            this.xrTableCell178.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetailAcnt3.Amount", "{0:n}")});
            this.xrTableCell178.Dpi = 100F;
            this.xrTableCell178.Font = new System.Drawing.Font("Arial", 7.5F);
            this.xrTableCell178.Name = "xrTableCell178";
            this.xrTableCell178.StylePriority.UseFont = false;
            this.xrTableCell178.StylePriority.UseTextAlignment = false;
            this.xrTableCell178.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell178.Weight = 0.011457284165937842D;
            // 
            // GroupFooter7
            // 
            this.GroupFooter7.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable4});
            this.GroupFooter7.Dpi = 100F;
            this.GroupFooter7.HeightF = 15F;
            this.GroupFooter7.Name = "GroupFooter7";
            // 
            // xrTable4
            // 
            this.xrTable4.Dpi = 100F;
            this.xrTable4.LocationFloat = new DevExpress.Utils.PointFloat(10F, 0F);
            this.xrTable4.Name = "xrTable4";
            this.xrTable4.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow4});
            this.xrTable4.SizeF = new System.Drawing.SizeF(1392.997F, 15F);
            // 
            // xrTableRow4
            // 
            this.xrTableRow4.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell28,
            this.xrTableCell42,
            this.xrTableCell43,
            this.xrTableCell44,
            this.xrTableCell45,
            this.xrTableCell46,
            this.xrTableCell47,
            this.xrTableCell48,
            this.xrTableCell49,
            this.xrTableCell50,
            this.xrTableCell51,
            this.xrTableCell52,
            this.xrTableCell53,
            this.xrTableCell54});
            this.xrTableRow4.Dpi = 100F;
            this.xrTableRow4.Name = "xrTableRow4";
            this.xrTableRow4.Weight = 11.5D;
            // 
            // xrTableCell28
            // 
            this.xrTableCell28.Dpi = 100F;
            this.xrTableCell28.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell28.Name = "xrTableCell28";
            this.xrTableCell28.StylePriority.UseFont = false;
            this.xrTableCell28.Text = "Total Selling Administrative and General";
            this.xrTableCell28.Weight = 0.044742444870609452D;
            // 
            // xrTableCell42
            // 
            this.xrTableCell42.Borders = DevExpress.XtraPrinting.BorderSide.Top;
            this.xrTableCell42.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetailAcnt3.Amount_01", "{0:n}")});
            this.xrTableCell42.Dpi = 100F;
            this.xrTableCell42.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell42.Name = "xrTableCell42";
            this.xrTableCell42.StylePriority.UseBorders = false;
            this.xrTableCell42.StylePriority.UseFont = false;
            this.xrTableCell42.StylePriority.UseTextAlignment = false;
            this.xrTableCell42.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell42.Weight = 0.011456676506122643D;
            // 
            // xrTableCell43
            // 
            this.xrTableCell43.Borders = DevExpress.XtraPrinting.BorderSide.Top;
            this.xrTableCell43.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetailAcnt3.Amount_02", "{0:n}")});
            this.xrTableCell43.Dpi = 100F;
            this.xrTableCell43.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell43.Name = "xrTableCell43";
            this.xrTableCell43.StylePriority.UseBorders = false;
            this.xrTableCell43.StylePriority.UseFont = false;
            this.xrTableCell43.StylePriority.UseTextAlignment = false;
            this.xrTableCell43.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell43.Weight = 0.011456439447518315D;
            // 
            // xrTableCell44
            // 
            this.xrTableCell44.Borders = DevExpress.XtraPrinting.BorderSide.Top;
            this.xrTableCell44.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetailAcnt3.Amount_03", "{0:n}")});
            this.xrTableCell44.Dpi = 100F;
            this.xrTableCell44.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell44.Name = "xrTableCell44";
            this.xrTableCell44.StylePriority.UseBorders = false;
            this.xrTableCell44.StylePriority.UseFont = false;
            this.xrTableCell44.StylePriority.UseTextAlignment = false;
            this.xrTableCell44.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell44.Weight = 0.01145644711665144D;
            // 
            // xrTableCell45
            // 
            this.xrTableCell45.Borders = DevExpress.XtraPrinting.BorderSide.Top;
            this.xrTableCell45.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetailAcnt3.Amount_04", "{0:n}")});
            this.xrTableCell45.Dpi = 100F;
            this.xrTableCell45.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell45.Name = "xrTableCell45";
            this.xrTableCell45.StylePriority.UseBorders = false;
            this.xrTableCell45.StylePriority.UseFont = false;
            this.xrTableCell45.StylePriority.UseTextAlignment = false;
            this.xrTableCell45.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell45.Weight = 0.011456464340136261D;
            // 
            // xrTableCell46
            // 
            this.xrTableCell46.Borders = DevExpress.XtraPrinting.BorderSide.Top;
            this.xrTableCell46.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetailAcnt3.Amount_05", "{0:n}")});
            this.xrTableCell46.Dpi = 100F;
            this.xrTableCell46.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell46.Name = "xrTableCell46";
            this.xrTableCell46.StylePriority.UseBorders = false;
            this.xrTableCell46.StylePriority.UseFont = false;
            this.xrTableCell46.StylePriority.UseTextAlignment = false;
            this.xrTableCell46.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell46.Weight = 0.011456379748427982D;
            // 
            // xrTableCell47
            // 
            this.xrTableCell47.Borders = DevExpress.XtraPrinting.BorderSide.Top;
            this.xrTableCell47.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetailAcnt3.Amount_06", "{0:n}")});
            this.xrTableCell47.Dpi = 100F;
            this.xrTableCell47.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell47.Name = "xrTableCell47";
            this.xrTableCell47.StylePriority.UseBorders = false;
            this.xrTableCell47.StylePriority.UseFont = false;
            this.xrTableCell47.StylePriority.UseTextAlignment = false;
            this.xrTableCell47.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell47.Weight = 0.011456481623145959D;
            // 
            // xrTableCell48
            // 
            this.xrTableCell48.Borders = DevExpress.XtraPrinting.BorderSide.Top;
            this.xrTableCell48.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetailAcnt3.Amount_07", "{0:n}")});
            this.xrTableCell48.Dpi = 100F;
            this.xrTableCell48.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell48.Name = "xrTableCell48";
            this.xrTableCell48.StylePriority.UseBorders = false;
            this.xrTableCell48.StylePriority.UseFont = false;
            this.xrTableCell48.StylePriority.UseTextAlignment = false;
            this.xrTableCell48.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell48.Weight = 0.011456448104521738D;
            // 
            // xrTableCell49
            // 
            this.xrTableCell49.Borders = DevExpress.XtraPrinting.BorderSide.Top;
            this.xrTableCell49.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetailAcnt3.Amount_08", "{0:n}")});
            this.xrTableCell49.Dpi = 100F;
            this.xrTableCell49.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell49.Name = "xrTableCell49";
            this.xrTableCell49.StylePriority.UseBorders = false;
            this.xrTableCell49.StylePriority.UseFont = false;
            this.xrTableCell49.StylePriority.UseTextAlignment = false;
            this.xrTableCell49.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell49.Weight = 0.01145656843549997D;
            // 
            // xrTableCell50
            // 
            this.xrTableCell50.Borders = DevExpress.XtraPrinting.BorderSide.Top;
            this.xrTableCell50.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetailAcnt3.Amount_09", "{0:n}")});
            this.xrTableCell50.Dpi = 100F;
            this.xrTableCell50.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell50.Name = "xrTableCell50";
            this.xrTableCell50.StylePriority.UseBorders = false;
            this.xrTableCell50.StylePriority.UseFont = false;
            this.xrTableCell50.StylePriority.UseTextAlignment = false;
            this.xrTableCell50.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell50.Weight = 0.011462360890095221D;
            // 
            // xrTableCell51
            // 
            this.xrTableCell51.Borders = DevExpress.XtraPrinting.BorderSide.Top;
            this.xrTableCell51.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetailAcnt3.Amount_10", "{0:n}")});
            this.xrTableCell51.Dpi = 100F;
            this.xrTableCell51.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell51.Name = "xrTableCell51";
            this.xrTableCell51.StylePriority.UseBorders = false;
            this.xrTableCell51.StylePriority.UseFont = false;
            this.xrTableCell51.StylePriority.UseTextAlignment = false;
            this.xrTableCell51.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell51.Weight = 0.011450439867777159D;
            // 
            // xrTableCell52
            // 
            this.xrTableCell52.Borders = DevExpress.XtraPrinting.BorderSide.Top;
            this.xrTableCell52.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetailAcnt3.Amount_11", "{0:n}")});
            this.xrTableCell52.Dpi = 100F;
            this.xrTableCell52.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell52.Name = "xrTableCell52";
            this.xrTableCell52.StylePriority.UseBorders = false;
            this.xrTableCell52.StylePriority.UseFont = false;
            this.xrTableCell52.StylePriority.UseTextAlignment = false;
            this.xrTableCell52.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell52.Weight = 0.011455021519186374D;
            // 
            // xrTableCell53
            // 
            this.xrTableCell53.Borders = DevExpress.XtraPrinting.BorderSide.Top;
            this.xrTableCell53.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetailAcnt3.Amount_12", "{0:n}")});
            this.xrTableCell53.Dpi = 100F;
            this.xrTableCell53.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell53.Name = "xrTableCell53";
            this.xrTableCell53.StylePriority.UseBorders = false;
            this.xrTableCell53.StylePriority.UseFont = false;
            this.xrTableCell53.StylePriority.UseTextAlignment = false;
            xrSummary61.FormatString = "{0:#,0}";
            this.xrTableCell53.Summary = xrSummary61;
            this.xrTableCell53.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell53.Weight = 0.01145697336366436D;
            // 
            // xrTableCell54
            // 
            this.xrTableCell54.Borders = DevExpress.XtraPrinting.BorderSide.Top;
            this.xrTableCell54.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetailAcnt3.Amount", "{0:n}")});
            this.xrTableCell54.Dpi = 100F;
            this.xrTableCell54.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell54.Name = "xrTableCell54";
            this.xrTableCell54.StylePriority.UseBorders = false;
            this.xrTableCell54.StylePriority.UseFont = false;
            this.xrTableCell54.StylePriority.UseTextAlignment = false;
            this.xrTableCell54.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell54.Weight = 0.011457284165937842D;
            // 
            // DetailReport1
            // 
            this.DetailReport1.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail2,
            this.GroupFooter3});
            this.DetailReport1.DataMember = "sp_report_ContributionMarginDetail2";
            this.DetailReport1.DataSource = this.sqlDataSource1;
            this.DetailReport1.Dpi = 100F;
            this.DetailReport1.Level = 1;
            this.DetailReport1.Name = "DetailReport1";
            // 
            // Detail2
            // 
            this.Detail2.Dpi = 100F;
            this.Detail2.Expanded = false;
            this.Detail2.HeightF = 15F;
            this.Detail2.Name = "Detail2";
            // 
            // GroupFooter3
            // 
            this.GroupFooter3.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable5,
            this.xrTable6,
            this.xrTable10,
            this.xrTable12,
            this.xrLabel2,
            this.xrLabel7});
            this.GroupFooter3.Dpi = 100F;
            this.GroupFooter3.HeightF = 88.54166F;
            this.GroupFooter3.Name = "GroupFooter3";
            // 
            // xrTable5
            // 
            this.xrTable5.Borders = DevExpress.XtraPrinting.BorderSide.Top;
            this.xrTable5.Dpi = 100F;
            this.xrTable5.LocationFloat = new DevExpress.Utils.PointFloat(331.811F, 0F);
            this.xrTable5.Name = "xrTable5";
            this.xrTable5.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow5});
            this.xrTable5.SizeF = new System.Drawing.SizeF(1071.189F, 15F);
            this.xrTable5.StylePriority.UseBorders = false;
            // 
            // xrTableRow5
            // 
            this.xrTableRow5.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell55,
            this.xrTableCell56,
            this.xrTableCell57,
            this.xrTableCell58,
            this.xrTableCell59,
            this.xrTableCell60,
            this.xrTableCell61,
            this.xrTableCell62,
            this.xrTableCell63,
            this.xrTableCell64,
            this.xrTableCell65,
            this.xrTableCell66,
            this.xrTableCell67});
            this.xrTableRow5.Dpi = 100F;
            this.xrTableRow5.Name = "xrTableRow5";
            this.xrTableRow5.Weight = 11.5D;
            // 
            // xrTableCell55
            // 
            this.xrTableCell55.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail2.Amount_01")});
            this.xrTableCell55.Dpi = 100F;
            this.xrTableCell55.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell55.Name = "xrTableCell55";
            this.xrTableCell55.StylePriority.UseFont = false;
            this.xrTableCell55.StylePriority.UseTextAlignment = false;
            xrSummary62.FormatString = "{0:n}";
            xrSummary62.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            this.xrTableCell55.Summary = xrSummary62;
            this.xrTableCell55.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell55.Weight = 0.010833457060557624D;
            // 
            // xrTableCell56
            // 
            this.xrTableCell56.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail2.Amount_02")});
            this.xrTableCell56.Dpi = 100F;
            this.xrTableCell56.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell56.Name = "xrTableCell56";
            this.xrTableCell56.StylePriority.UseFont = false;
            this.xrTableCell56.StylePriority.UseTextAlignment = false;
            xrSummary63.FormatString = "{0:n}";
            xrSummary63.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            this.xrTableCell56.Summary = xrSummary63;
            this.xrTableCell56.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell56.Weight = 0.010833353068105147D;
            // 
            // xrTableCell57
            // 
            this.xrTableCell57.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail2.Amount_03")});
            this.xrTableCell57.Dpi = 100F;
            this.xrTableCell57.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell57.Name = "xrTableCell57";
            this.xrTableCell57.StylePriority.UseFont = false;
            this.xrTableCell57.StylePriority.UseTextAlignment = false;
            xrSummary64.FormatString = "{0:n}";
            xrSummary64.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            this.xrTableCell57.Summary = xrSummary64;
            this.xrTableCell57.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell57.Weight = 0.010833340095552212D;
            // 
            // xrTableCell58
            // 
            this.xrTableCell58.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail2.Amount_04")});
            this.xrTableCell58.Dpi = 100F;
            this.xrTableCell58.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell58.Name = "xrTableCell58";
            this.xrTableCell58.StylePriority.UseFont = false;
            this.xrTableCell58.StylePriority.UseTextAlignment = false;
            xrSummary65.FormatString = "{0:n}";
            xrSummary65.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            this.xrTableCell58.Summary = xrSummary65;
            this.xrTableCell58.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell58.Weight = 0.010833344100769705D;
            // 
            // xrTableCell59
            // 
            this.xrTableCell59.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail2.Amount_05")});
            this.xrTableCell59.Dpi = 100F;
            this.xrTableCell59.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell59.Name = "xrTableCell59";
            this.xrTableCell59.StylePriority.UseFont = false;
            this.xrTableCell59.StylePriority.UseTextAlignment = false;
            xrSummary66.FormatString = "{0:n}";
            xrSummary66.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            this.xrTableCell59.Summary = xrSummary66;
            this.xrTableCell59.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell59.Weight = 0.010833228290575174D;
            // 
            // xrTableCell60
            // 
            this.xrTableCell60.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail2.Amount_06")});
            this.xrTableCell60.Dpi = 100F;
            this.xrTableCell60.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell60.Name = "xrTableCell60";
            this.xrTableCell60.StylePriority.UseFont = false;
            this.xrTableCell60.StylePriority.UseTextAlignment = false;
            xrSummary67.FormatString = "{0:n}";
            xrSummary67.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            this.xrTableCell60.Summary = xrSummary67;
            this.xrTableCell60.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell60.Weight = 0.010833348592122385D;
            // 
            // xrTableCell61
            // 
            this.xrTableCell61.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail2.Amount_07")});
            this.xrTableCell61.Dpi = 100F;
            this.xrTableCell61.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell61.Name = "xrTableCell61";
            this.xrTableCell61.StylePriority.UseFont = false;
            this.xrTableCell61.StylePriority.UseTextAlignment = false;
            xrSummary68.FormatString = "{0:n}";
            xrSummary68.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            this.xrTableCell61.Summary = xrSummary68;
            this.xrTableCell61.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell61.Weight = 0.010833348592122385D;
            // 
            // xrTableCell62
            // 
            this.xrTableCell62.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail2.Amount_08")});
            this.xrTableCell62.Dpi = 100F;
            this.xrTableCell62.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell62.Name = "xrTableCell62";
            this.xrTableCell62.StylePriority.UseFont = false;
            this.xrTableCell62.StylePriority.UseTextAlignment = false;
            xrSummary69.FormatString = "{0:n}";
            xrSummary69.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            this.xrTableCell62.Summary = xrSummary69;
            this.xrTableCell62.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell62.Weight = 0.010833348592122385D;
            // 
            // xrTableCell63
            // 
            this.xrTableCell63.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail2.Amount_09")});
            this.xrTableCell63.Dpi = 100F;
            this.xrTableCell63.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell63.Name = "xrTableCell63";
            this.xrTableCell63.StylePriority.UseFont = false;
            this.xrTableCell63.StylePriority.UseTextAlignment = false;
            xrSummary70.FormatString = "{0:n}";
            xrSummary70.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            this.xrTableCell63.Summary = xrSummary70;
            this.xrTableCell63.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell63.Weight = 0.010839060839599962D;
            // 
            // xrTableCell64
            // 
            this.xrTableCell64.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail2.Amount_10")});
            this.xrTableCell64.Dpi = 100F;
            this.xrTableCell64.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell64.Name = "xrTableCell64";
            this.xrTableCell64.StylePriority.UseFont = false;
            this.xrTableCell64.StylePriority.UseTextAlignment = false;
            xrSummary71.FormatString = "{0:n}";
            xrSummary71.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            this.xrTableCell64.Summary = xrSummary71;
            this.xrTableCell64.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell64.Weight = 0.01082767958648794D;
            // 
            // xrTableCell65
            // 
            this.xrTableCell65.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail2.Amount_11")});
            this.xrTableCell65.Dpi = 100F;
            this.xrTableCell65.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell65.Name = "xrTableCell65";
            this.xrTableCell65.StylePriority.UseFont = false;
            this.xrTableCell65.StylePriority.UseForeColor = false;
            this.xrTableCell65.StylePriority.UseTextAlignment = false;
            xrSummary72.FormatString = "{0:n}";
            xrSummary72.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            this.xrTableCell65.Summary = xrSummary72;
            this.xrTableCell65.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell65.Weight = 0.010831891342008878D;
            // 
            // xrTableCell66
            // 
            this.xrTableCell66.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail2.Amount_12")});
            this.xrTableCell66.Dpi = 100F;
            this.xrTableCell66.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell66.Name = "xrTableCell66";
            this.xrTableCell66.StylePriority.UseFont = false;
            this.xrTableCell66.StylePriority.UseTextAlignment = false;
            xrSummary73.FormatString = "{0:n}";
            xrSummary73.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            this.xrTableCell66.Summary = xrSummary73;
            this.xrTableCell66.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell66.Weight = 0.010833984247216408D;
            // 
            // xrTableCell67
            // 
            this.xrTableCell67.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail2.Amount")});
            this.xrTableCell67.Dpi = 100F;
            this.xrTableCell67.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell67.ForeColor = System.Drawing.Color.Black;
            this.xrTableCell67.Name = "xrTableCell67";
            this.xrTableCell67.StylePriority.UseFont = false;
            this.xrTableCell67.StylePriority.UseForeColor = false;
            this.xrTableCell67.StylePriority.UseTextAlignment = false;
            xrSummary74.FormatString = "{0:n}";
            xrSummary74.Running = DevExpress.XtraReports.UI.SummaryRunning.Group;
            this.xrTableCell67.Summary = xrSummary74;
            this.xrTableCell67.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell67.Weight = 0.01083412694529874D;
            // 
            // xrTable6
            // 
            this.xrTable6.Dpi = 100F;
            this.xrTable6.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTable6.LocationFloat = new DevExpress.Utils.PointFloat(331.8096F, 14.99999F);
            this.xrTable6.Name = "xrTable6";
            this.xrTable6.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow6});
            this.xrTable6.SizeF = new System.Drawing.SizeF(1071.19F, 15F);
            this.xrTable6.StylePriority.UseFont = false;
            // 
            // xrTableRow6
            // 
            this.xrTableRow6.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell68,
            this.xrTableCell69,
            this.xrTableCell70,
            this.xrTableCell71,
            this.xrTableCell72,
            this.xrTableCell73,
            this.xrTableCell74,
            this.xrTableCell75,
            this.xrTableCell76,
            this.xrTableCell77,
            this.xrTableCell78,
            this.xrTableCell79,
            this.xrTableCell80});
            this.xrTableRow6.Dpi = 100F;
            this.xrTableRow6.Name = "xrTableRow6";
            this.xrTableRow6.Weight = 11.5D;
            // 
            // xrTableCell68
            // 
            this.xrTableCell68.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail2.TotalFixedPer1", "{0:0.00%}")});
            this.xrTableCell68.Dpi = 100F;
            this.xrTableCell68.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell68.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.xrTableCell68.Name = "xrTableCell68";
            this.xrTableCell68.StylePriority.UseFont = false;
            this.xrTableCell68.StylePriority.UseForeColor = false;
            this.xrTableCell68.StylePriority.UseTextAlignment = false;
            xrSummary75.FormatString = "{0:0.00%}";
            this.xrTableCell68.Summary = xrSummary75;
            this.xrTableCell68.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell68.Weight = 0.014973887748189205D;
            // 
            // xrTableCell69
            // 
            this.xrTableCell69.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail2.TotalFixedPer2", "{0:0.00%}")});
            this.xrTableCell69.Dpi = 100F;
            this.xrTableCell69.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell69.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.xrTableCell69.Name = "xrTableCell69";
            this.xrTableCell69.StylePriority.UseFont = false;
            this.xrTableCell69.StylePriority.UseForeColor = false;
            this.xrTableCell69.StylePriority.UseTextAlignment = false;
            xrSummary76.FormatString = "{0:n}";
            this.xrTableCell69.Summary = xrSummary76;
            this.xrTableCell69.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell69.Weight = 0.014973689631148673D;
            // 
            // xrTableCell70
            // 
            this.xrTableCell70.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail2.TotalFixedPer3", "{0:0.00%}")});
            this.xrTableCell70.Dpi = 100F;
            this.xrTableCell70.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell70.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.xrTableCell70.Name = "xrTableCell70";
            this.xrTableCell70.StylePriority.UseFont = false;
            this.xrTableCell70.StylePriority.UseForeColor = false;
            this.xrTableCell70.StylePriority.UseTextAlignment = false;
            xrSummary77.FormatString = "{0:n}";
            this.xrTableCell70.Summary = xrSummary77;
            this.xrTableCell70.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell70.Weight = 0.014973689631148673D;
            // 
            // xrTableCell71
            // 
            this.xrTableCell71.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail2.TotalFixedPer4", "{0:0.00%}")});
            this.xrTableCell71.Dpi = 100F;
            this.xrTableCell71.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell71.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.xrTableCell71.Name = "xrTableCell71";
            this.xrTableCell71.StylePriority.UseFont = false;
            this.xrTableCell71.StylePriority.UseForeColor = false;
            this.xrTableCell71.StylePriority.UseTextAlignment = false;
            xrSummary78.FormatString = "{0:n}";
            this.xrTableCell71.Summary = xrSummary78;
            this.xrTableCell71.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell71.Weight = 0.014973689631148673D;
            // 
            // xrTableCell72
            // 
            this.xrTableCell72.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail2.TotalFixedPer5", "{0:0.00%}")});
            this.xrTableCell72.Dpi = 100F;
            this.xrTableCell72.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell72.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.xrTableCell72.Name = "xrTableCell72";
            this.xrTableCell72.StylePriority.UseFont = false;
            this.xrTableCell72.StylePriority.UseForeColor = false;
            this.xrTableCell72.StylePriority.UseTextAlignment = false;
            xrSummary79.FormatString = "{0:n}";
            this.xrTableCell72.Summary = xrSummary79;
            this.xrTableCell72.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell72.Weight = 0.014973689631148673D;
            // 
            // xrTableCell73
            // 
            this.xrTableCell73.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail2.TotalFixedPer6", "{0:0.00%}")});
            this.xrTableCell73.Dpi = 100F;
            this.xrTableCell73.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell73.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.xrTableCell73.Name = "xrTableCell73";
            this.xrTableCell73.StylePriority.UseFont = false;
            this.xrTableCell73.StylePriority.UseForeColor = false;
            this.xrTableCell73.StylePriority.UseTextAlignment = false;
            xrSummary80.FormatString = "{0:n}";
            this.xrTableCell73.Summary = xrSummary80;
            this.xrTableCell73.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell73.Weight = 0.014973689631148673D;
            // 
            // xrTableCell74
            // 
            this.xrTableCell74.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail2.TotalFixedPer7", "{0:0.00%}")});
            this.xrTableCell74.Dpi = 100F;
            this.xrTableCell74.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell74.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.xrTableCell74.Name = "xrTableCell74";
            this.xrTableCell74.StylePriority.UseFont = false;
            this.xrTableCell74.StylePriority.UseForeColor = false;
            this.xrTableCell74.StylePriority.UseTextAlignment = false;
            xrSummary81.FormatString = "{0:0.00%}";
            this.xrTableCell74.Summary = xrSummary81;
            this.xrTableCell74.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell74.Weight = 0.014973689631148673D;
            // 
            // xrTableCell75
            // 
            this.xrTableCell75.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail2.TotalFixedPer8", "{0:0.00%}")});
            this.xrTableCell75.Dpi = 100F;
            this.xrTableCell75.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell75.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.xrTableCell75.Name = "xrTableCell75";
            this.xrTableCell75.StylePriority.UseFont = false;
            this.xrTableCell75.StylePriority.UseForeColor = false;
            this.xrTableCell75.StylePriority.UseTextAlignment = false;
            xrSummary82.FormatString = "{0:n}";
            this.xrTableCell75.Summary = xrSummary82;
            this.xrTableCell75.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell75.Weight = 0.014973689631148673D;
            // 
            // xrTableCell76
            // 
            this.xrTableCell76.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail2.TotalFixedPer9", "{0:0.00%}")});
            this.xrTableCell76.Dpi = 100F;
            this.xrTableCell76.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell76.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.xrTableCell76.Name = "xrTableCell76";
            this.xrTableCell76.StylePriority.UseFont = false;
            this.xrTableCell76.StylePriority.UseForeColor = false;
            this.xrTableCell76.StylePriority.UseTextAlignment = false;
            xrSummary83.FormatString = "{0:n}";
            this.xrTableCell76.Summary = xrSummary83;
            this.xrTableCell76.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell76.Weight = 0.014981511803462308D;
            // 
            // xrTableCell77
            // 
            this.xrTableCell77.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail2.TotalFixedPer10", "{0:0.00%}")});
            this.xrTableCell77.Dpi = 100F;
            this.xrTableCell77.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell77.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.xrTableCell77.Name = "xrTableCell77";
            this.xrTableCell77.StylePriority.UseFont = false;
            this.xrTableCell77.StylePriority.UseForeColor = false;
            this.xrTableCell77.StylePriority.UseTextAlignment = false;
            xrSummary84.FormatString = "{0:n}";
            this.xrTableCell77.Summary = xrSummary84;
            this.xrTableCell77.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell77.Weight = 0.014965867458835038D;
            // 
            // xrTableCell78
            // 
            this.xrTableCell78.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail2.TotalFixedPer11", "{0:0.00%}")});
            this.xrTableCell78.Dpi = 100F;
            this.xrTableCell78.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell78.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.xrTableCell78.Name = "xrTableCell78";
            this.xrTableCell78.StylePriority.UseFont = false;
            this.xrTableCell78.StylePriority.UseForeColor = false;
            this.xrTableCell78.StylePriority.UseTextAlignment = false;
            xrSummary85.FormatString = "{0:n}";
            this.xrTableCell78.Summary = xrSummary85;
            this.xrTableCell78.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell78.Weight = 0.014971685899902717D;
            // 
            // xrTableCell79
            // 
            this.xrTableCell79.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail2.TotalFixedPer12", "{0:0.00%}")});
            this.xrTableCell79.Dpi = 100F;
            this.xrTableCell79.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell79.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.xrTableCell79.Name = "xrTableCell79";
            this.xrTableCell79.StylePriority.UseFont = false;
            this.xrTableCell79.StylePriority.UseForeColor = false;
            this.xrTableCell79.StylePriority.UseTextAlignment = false;
            xrSummary86.FormatString = "{0:n}";
            this.xrTableCell79.Summary = xrSummary86;
            this.xrTableCell79.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell79.Weight = 0.014974533857959983D;
            // 
            // xrTableCell80
            // 
            this.xrTableCell80.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail2.TotalFixedPer", "{0:0.00%}")});
            this.xrTableCell80.Dpi = 100F;
            this.xrTableCell80.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell80.ForeColor = System.Drawing.Color.Blue;
            this.xrTableCell80.Name = "xrTableCell80";
            this.xrTableCell80.StylePriority.UseBackColor = false;
            this.xrTableCell80.StylePriority.UseFont = false;
            this.xrTableCell80.StylePriority.UseForeColor = false;
            this.xrTableCell80.StylePriority.UseTextAlignment = false;
            xrSummary87.FormatString = "{0:n}";
            this.xrTableCell80.Summary = xrSummary87;
            this.xrTableCell80.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell80.Weight = 0.014974849135583321D;
            // 
            // xrTable10
            // 
            this.xrTable10.Dpi = 100F;
            this.xrTable10.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrTable10.LocationFloat = new DevExpress.Utils.PointFloat(331.8075F, 59.99998F);
            this.xrTable10.Name = "xrTable10";
            this.xrTable10.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow21});
            this.xrTable10.SizeF = new System.Drawing.SizeF(1071.19F, 15F);
            this.xrTable10.StylePriority.UseFont = false;
            // 
            // xrTableRow21
            // 
            this.xrTableRow21.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell276,
            this.xrTableCell277,
            this.xrTableCell278,
            this.xrTableCell279,
            this.xrTableCell280,
            this.xrTableCell281,
            this.xrTableCell282,
            this.xrTableCell283,
            this.xrTableCell284,
            this.xrTableCell285,
            this.xrTableCell286,
            this.xrTableCell287,
            this.xrTableCell288});
            this.xrTableRow21.Dpi = 100F;
            this.xrTableRow21.Name = "xrTableRow21";
            this.xrTableRow21.Weight = 11.5D;
            // 
            // xrTableCell276
            // 
            this.xrTableCell276.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMArginDetail2.OpsIncomePer1", "{0:0.00%}")});
            this.xrTableCell276.Dpi = 100F;
            this.xrTableCell276.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell276.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.xrTableCell276.Name = "xrTableCell276";
            this.xrTableCell276.StylePriority.UseFont = false;
            this.xrTableCell276.StylePriority.UseForeColor = false;
            this.xrTableCell276.StylePriority.UseTextAlignment = false;
            xrSummary88.FormatString = "{0:0.00%}";
            this.xrTableCell276.Summary = xrSummary88;
            this.xrTableCell276.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell276.Weight = 0.014973887748189205D;
            // 
            // xrTableCell277
            // 
            this.xrTableCell277.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMArginDetail2.OpsIncomePer2", "{0:0.00%}")});
            this.xrTableCell277.Dpi = 100F;
            this.xrTableCell277.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell277.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.xrTableCell277.Name = "xrTableCell277";
            this.xrTableCell277.StylePriority.UseFont = false;
            this.xrTableCell277.StylePriority.UseForeColor = false;
            this.xrTableCell277.StylePriority.UseTextAlignment = false;
            xrSummary89.FormatString = "{0:n}";
            this.xrTableCell277.Summary = xrSummary89;
            this.xrTableCell277.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell277.Weight = 0.014973689631148673D;
            // 
            // xrTableCell278
            // 
            this.xrTableCell278.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMArginDetail2.OpsIncomePer3", "{0:0.00%}")});
            this.xrTableCell278.Dpi = 100F;
            this.xrTableCell278.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell278.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.xrTableCell278.Name = "xrTableCell278";
            this.xrTableCell278.StylePriority.UseFont = false;
            this.xrTableCell278.StylePriority.UseForeColor = false;
            this.xrTableCell278.StylePriority.UseTextAlignment = false;
            xrSummary90.FormatString = "{0:n}";
            this.xrTableCell278.Summary = xrSummary90;
            this.xrTableCell278.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell278.Weight = 0.014973689631148673D;
            // 
            // xrTableCell279
            // 
            this.xrTableCell279.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMArginDetail2.OpsIncomePer4", "{0:0.00%}")});
            this.xrTableCell279.Dpi = 100F;
            this.xrTableCell279.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell279.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.xrTableCell279.Name = "xrTableCell279";
            this.xrTableCell279.StylePriority.UseFont = false;
            this.xrTableCell279.StylePriority.UseForeColor = false;
            this.xrTableCell279.StylePriority.UseTextAlignment = false;
            xrSummary91.FormatString = "{0:n}";
            this.xrTableCell279.Summary = xrSummary91;
            this.xrTableCell279.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell279.Weight = 0.014973689631148673D;
            // 
            // xrTableCell280
            // 
            this.xrTableCell280.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMArginDetail2.OpsIncomePer5", "{0:0.00%}")});
            this.xrTableCell280.Dpi = 100F;
            this.xrTableCell280.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell280.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.xrTableCell280.Name = "xrTableCell280";
            this.xrTableCell280.StylePriority.UseFont = false;
            this.xrTableCell280.StylePriority.UseForeColor = false;
            this.xrTableCell280.StylePriority.UseTextAlignment = false;
            xrSummary92.FormatString = "{0:n}";
            this.xrTableCell280.Summary = xrSummary92;
            this.xrTableCell280.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell280.Weight = 0.014973689631148673D;
            // 
            // xrTableCell281
            // 
            this.xrTableCell281.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMArginDetail2.OpsIncomePer6", "{0:0.00%}")});
            this.xrTableCell281.Dpi = 100F;
            this.xrTableCell281.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell281.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.xrTableCell281.Name = "xrTableCell281";
            this.xrTableCell281.StylePriority.UseFont = false;
            this.xrTableCell281.StylePriority.UseForeColor = false;
            this.xrTableCell281.StylePriority.UseTextAlignment = false;
            xrSummary93.FormatString = "{0:n}";
            this.xrTableCell281.Summary = xrSummary93;
            this.xrTableCell281.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell281.Weight = 0.014973689631148673D;
            // 
            // xrTableCell282
            // 
            this.xrTableCell282.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMArginDetail2.OpsIncomePer7", "{0:0.00%}")});
            this.xrTableCell282.Dpi = 100F;
            this.xrTableCell282.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell282.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.xrTableCell282.Name = "xrTableCell282";
            this.xrTableCell282.StylePriority.UseFont = false;
            this.xrTableCell282.StylePriority.UseForeColor = false;
            this.xrTableCell282.StylePriority.UseTextAlignment = false;
            xrSummary94.FormatString = "{0:0.00%}";
            this.xrTableCell282.Summary = xrSummary94;
            this.xrTableCell282.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell282.Weight = 0.014973689631148673D;
            // 
            // xrTableCell283
            // 
            this.xrTableCell283.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMArginDetail2.OpsIncomePer8", "{0:0.00%}")});
            this.xrTableCell283.Dpi = 100F;
            this.xrTableCell283.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell283.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.xrTableCell283.Name = "xrTableCell283";
            this.xrTableCell283.StylePriority.UseFont = false;
            this.xrTableCell283.StylePriority.UseForeColor = false;
            this.xrTableCell283.StylePriority.UseTextAlignment = false;
            xrSummary95.FormatString = "{0:n}";
            this.xrTableCell283.Summary = xrSummary95;
            this.xrTableCell283.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell283.Weight = 0.014973689631148673D;
            // 
            // xrTableCell284
            // 
            this.xrTableCell284.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMArginDetail2.OpsIncomePer9", "{0:0.00%}")});
            this.xrTableCell284.Dpi = 100F;
            this.xrTableCell284.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell284.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.xrTableCell284.Name = "xrTableCell284";
            this.xrTableCell284.StylePriority.UseFont = false;
            this.xrTableCell284.StylePriority.UseForeColor = false;
            this.xrTableCell284.StylePriority.UseTextAlignment = false;
            xrSummary96.FormatString = "{0:n}";
            this.xrTableCell284.Summary = xrSummary96;
            this.xrTableCell284.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell284.Weight = 0.014981511803462308D;
            // 
            // xrTableCell285
            // 
            this.xrTableCell285.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMArginDetail2.OpsIncomePer10", "{0:0.00%}")});
            this.xrTableCell285.Dpi = 100F;
            this.xrTableCell285.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell285.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.xrTableCell285.Name = "xrTableCell285";
            this.xrTableCell285.StylePriority.UseFont = false;
            this.xrTableCell285.StylePriority.UseForeColor = false;
            this.xrTableCell285.StylePriority.UseTextAlignment = false;
            xrSummary97.FormatString = "{0:n}";
            this.xrTableCell285.Summary = xrSummary97;
            this.xrTableCell285.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell285.Weight = 0.014965867458835038D;
            // 
            // xrTableCell286
            // 
            this.xrTableCell286.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMArginDetail2.OpsIncomePer11", "{0:0.00%}")});
            this.xrTableCell286.Dpi = 100F;
            this.xrTableCell286.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell286.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.xrTableCell286.Name = "xrTableCell286";
            this.xrTableCell286.StylePriority.UseFont = false;
            this.xrTableCell286.StylePriority.UseForeColor = false;
            this.xrTableCell286.StylePriority.UseTextAlignment = false;
            xrSummary98.FormatString = "{0:n}";
            this.xrTableCell286.Summary = xrSummary98;
            this.xrTableCell286.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell286.Weight = 0.014971685899902717D;
            // 
            // xrTableCell287
            // 
            this.xrTableCell287.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMArginDetail2.OpsIncomePer12", "{0:0.00%}")});
            this.xrTableCell287.Dpi = 100F;
            this.xrTableCell287.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell287.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.xrTableCell287.Name = "xrTableCell287";
            this.xrTableCell287.StylePriority.UseFont = false;
            this.xrTableCell287.StylePriority.UseForeColor = false;
            this.xrTableCell287.StylePriority.UseTextAlignment = false;
            xrSummary99.FormatString = "{0:n}";
            this.xrTableCell287.Summary = xrSummary99;
            this.xrTableCell287.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell287.Weight = 0.014974533857959983D;
            // 
            // xrTableCell288
            // 
            this.xrTableCell288.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMArginDetail2.OpsIncomePer", "{0:0.00%}")});
            this.xrTableCell288.Dpi = 100F;
            this.xrTableCell288.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell288.ForeColor = System.Drawing.Color.Blue;
            this.xrTableCell288.Name = "xrTableCell288";
            this.xrTableCell288.StylePriority.UseBackColor = false;
            this.xrTableCell288.StylePriority.UseFont = false;
            this.xrTableCell288.StylePriority.UseForeColor = false;
            this.xrTableCell288.StylePriority.UseTextAlignment = false;
            xrSummary100.FormatString = "{0:n}";
            this.xrTableCell288.Summary = xrSummary100;
            this.xrTableCell288.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell288.Weight = 0.014974849135583321D;
            // 
            // xrTable12
            // 
            this.xrTable12.Dpi = 100F;
            this.xrTable12.LocationFloat = new DevExpress.Utils.PointFloat(331.8075F, 44.99998F);
            this.xrTable12.Name = "xrTable12";
            this.xrTable12.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow23});
            this.xrTable12.SizeF = new System.Drawing.SizeF(1071.193F, 15F);
            // 
            // xrTableRow23
            // 
            this.xrTableRow23.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell301,
            this.xrTableCell302,
            this.xrTableCell303,
            this.xrTableCell304,
            this.xrTableCell305,
            this.xrTableCell306,
            this.xrTableCell307,
            this.xrTableCell308,
            this.xrTableCell309,
            this.xrTableCell310,
            this.xrTableCell311,
            this.xrTableCell312,
            this.xrTableCell313});
            this.xrTableRow23.Dpi = 100F;
            this.xrTableRow23.Name = "xrTableRow23";
            this.xrTableRow23.Weight = 11.5D;
            // 
            // xrTableCell301
            // 
            this.xrTableCell301.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail2.TotalOpsIncome1", "{0:n}")});
            this.xrTableCell301.Dpi = 100F;
            this.xrTableCell301.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell301.Name = "xrTableCell301";
            this.xrTableCell301.StylePriority.UseFont = false;
            this.xrTableCell301.StylePriority.UseTextAlignment = false;
            this.xrTableCell301.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell301.Weight = 0.011457159151225459D;
            // 
            // xrTableCell302
            // 
            this.xrTableCell302.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail2.TotalOpsIncome2", "{0:n}")});
            this.xrTableCell302.Dpi = 100F;
            this.xrTableCell302.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell302.Name = "xrTableCell302";
            this.xrTableCell302.StylePriority.UseFont = false;
            this.xrTableCell302.StylePriority.UseTextAlignment = false;
            this.xrTableCell302.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell302.Weight = 0.011456439447518315D;
            // 
            // xrTableCell303
            // 
            this.xrTableCell303.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail2.TotalOpsIncome3", "{0:n}")});
            this.xrTableCell303.Dpi = 100F;
            this.xrTableCell303.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell303.Name = "xrTableCell303";
            this.xrTableCell303.StylePriority.UseFont = false;
            this.xrTableCell303.StylePriority.UseTextAlignment = false;
            this.xrTableCell303.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell303.Weight = 0.01145644711665144D;
            // 
            // xrTableCell304
            // 
            this.xrTableCell304.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail2.TotalOpsIncome4", "{0:n}")});
            this.xrTableCell304.Dpi = 100F;
            this.xrTableCell304.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell304.Name = "xrTableCell304";
            this.xrTableCell304.StylePriority.UseFont = false;
            this.xrTableCell304.StylePriority.UseTextAlignment = false;
            this.xrTableCell304.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell304.Weight = 0.011456464340136261D;
            // 
            // xrTableCell305
            // 
            this.xrTableCell305.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail2.TotalOpsIncome5", "{0:n}")});
            this.xrTableCell305.Dpi = 100F;
            this.xrTableCell305.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell305.Name = "xrTableCell305";
            this.xrTableCell305.StylePriority.UseFont = false;
            this.xrTableCell305.StylePriority.UseTextAlignment = false;
            this.xrTableCell305.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell305.Weight = 0.011456379748427982D;
            // 
            // xrTableCell306
            // 
            this.xrTableCell306.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail2.TotalOpsIncome6", "{0:n}")});
            this.xrTableCell306.Dpi = 100F;
            this.xrTableCell306.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell306.Name = "xrTableCell306";
            this.xrTableCell306.StylePriority.UseFont = false;
            this.xrTableCell306.StylePriority.UseTextAlignment = false;
            this.xrTableCell306.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell306.Weight = 0.011456481623145959D;
            // 
            // xrTableCell307
            // 
            this.xrTableCell307.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail2.TotalOpsIncome7", "{0:n}")});
            this.xrTableCell307.Dpi = 100F;
            this.xrTableCell307.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell307.Name = "xrTableCell307";
            this.xrTableCell307.StylePriority.UseFont = false;
            this.xrTableCell307.StylePriority.UseTextAlignment = false;
            this.xrTableCell307.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell307.Weight = 0.011456448104521738D;
            // 
            // xrTableCell308
            // 
            this.xrTableCell308.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail2.TotalOpsIncome8", "{0:n}")});
            this.xrTableCell308.Dpi = 100F;
            this.xrTableCell308.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell308.Name = "xrTableCell308";
            this.xrTableCell308.StylePriority.UseFont = false;
            this.xrTableCell308.StylePriority.UseTextAlignment = false;
            this.xrTableCell308.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell308.Weight = 0.011456102075976372D;
            // 
            // xrTableCell309
            // 
            this.xrTableCell309.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail2.TotalOpsIncome9", "{0:n}")});
            this.xrTableCell309.Dpi = 100F;
            this.xrTableCell309.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell309.Name = "xrTableCell309";
            this.xrTableCell309.StylePriority.UseFont = false;
            this.xrTableCell309.StylePriority.UseTextAlignment = false;
            this.xrTableCell309.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell309.Weight = 0.011462827249618819D;
            // 
            // xrTableCell310
            // 
            this.xrTableCell310.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail2.TotalOpsIncome10", "{0:n}")});
            this.xrTableCell310.Dpi = 100F;
            this.xrTableCell310.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell310.Name = "xrTableCell310";
            this.xrTableCell310.StylePriority.UseFont = false;
            this.xrTableCell310.StylePriority.UseTextAlignment = false;
            this.xrTableCell310.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell310.Weight = 0.011450439867777159D;
            // 
            // xrTableCell311
            // 
            this.xrTableCell311.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail2.TotalOpsIncome11", "{0:n}")});
            this.xrTableCell311.Dpi = 100F;
            this.xrTableCell311.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell311.Name = "xrTableCell311";
            this.xrTableCell311.StylePriority.UseFont = false;
            this.xrTableCell311.StylePriority.UseTextAlignment = false;
            this.xrTableCell311.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell311.Weight = 0.011455021519186374D;
            // 
            // xrTableCell312
            // 
            this.xrTableCell312.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail2.TotalOpsIncome12", "{0:n}")});
            this.xrTableCell312.Dpi = 100F;
            this.xrTableCell312.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell312.Name = "xrTableCell312";
            this.xrTableCell312.StylePriority.UseFont = false;
            this.xrTableCell312.StylePriority.UseTextAlignment = false;
            xrSummary101.FormatString = "{0:#,0}";
            this.xrTableCell312.Summary = xrSummary101;
            this.xrTableCell312.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell312.Weight = 0.01145697336366436D;
            // 
            // xrTableCell313
            // 
            this.xrTableCell313.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail2.TotalOpsIncome", "{0:n}")});
            this.xrTableCell313.Dpi = 100F;
            this.xrTableCell313.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell313.Name = "xrTableCell313";
            this.xrTableCell313.StylePriority.UseFont = false;
            this.xrTableCell313.StylePriority.UseTextAlignment = false;
            this.xrTableCell313.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell313.Weight = 0.011456859862550751D;
            // 
            // xrLabel2
            // 
            this.xrLabel2.Dpi = 100F;
            this.xrLabel2.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 44.99995F);
            this.xrLabel2.Name = "xrLabel2";
            this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel2.SizeF = new System.Drawing.SizeF(331.8057F, 15F);
            this.xrLabel2.StylePriority.UseFont = false;
            this.xrLabel2.StylePriority.UseTextAlignment = false;
            this.xrLabel2.Text = "Operating Income/Loss";
            this.xrLabel2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel7
            // 
            this.xrLabel7.Dpi = 100F;
            this.xrLabel7.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrLabel7.LocationFloat = new DevExpress.Utils.PointFloat(0.002401988F, 0F);
            this.xrLabel7.Name = "xrLabel7";
            this.xrLabel7.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel7.SizeF = new System.Drawing.SizeF(331.8086F, 15F);
            this.xrLabel7.StylePriority.UseFont = false;
            this.xrLabel7.StylePriority.UseTextAlignment = false;
            this.xrLabel7.Text = "Total Fixed Cost";
            this.xrLabel7.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // calculatedField1
            // 
            this.calculatedField1.DisplayName = "pctg1";
            this.calculatedField1.Expression = "[usp_ConsolidatedIncomeStatement_test.Amount]/[usp_ConsolidatedIncomeStatement_to" +
    "talRevenue.Total Revenue]";
            this.calculatedField1.Name = "calculatedField1";
            // 
            // calculatedField2
            // 
            this.calculatedField2.Expression = "([usp_ConsolidatedIncomeStatement3.a14]+[usp_ConsolidatedIncomeStatement3.a15])/\r" +
    "\n[usp_ConsolidatedIncomeStatement_totalRevenue.Total Revenue]";
            this.calculatedField2.Name = "calculatedField2";
            // 
            // calculatedField3
            // 
            this.calculatedField3.DisplayName = "pctg2";
            this.calculatedField3.Expression = "[usp_ConsolidatedIncomeStatement_test.Amount2]/[usp_ConsolidatedIncomeStatement_t" +
    "otalRevenue.Total Revenue]";
            this.calculatedField3.Name = "calculatedField3";
            // 
            // calculatedField4
            // 
            this.calculatedField4.DisplayName = "pctg3";
            this.calculatedField4.Expression = "[usp_ConsolidatedIncomeStatement2_test.Amount]/[usp_ConsolidatedIncomeStatement_t" +
    "otalRevenue.Total Revenue]";
            this.calculatedField4.Name = "calculatedField4";
            // 
            // PageHeader
            // 
            this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable2});
            this.PageHeader.Dpi = 100F;
            this.PageHeader.HeightF = 30F;
            this.PageHeader.Name = "PageHeader";
            // 
            // formattingRule1
            // 
            this.formattingRule1.Name = "formattingRule1";
            // 
            // calculatedField5
            // 
            this.calculatedField5.Expression = "[sp_report_ContributionMarginDetail1.Amount_01] /[sp_report_ContributionMarginDet" +
    "ail1.Amount_02]";
            this.calculatedField5.Name = "calculatedField5";
            // 
            // ReportFooter1
            // 
            this.ReportFooter1.Dpi = 100F;
            this.ReportFooter1.Expanded = false;
            this.ReportFooter1.HeightF = 658.0595F;
            this.ReportFooter1.Name = "ReportFooter1";
            // 
            // xrTable14
            // 
            this.xrTable14.Dpi = 100F;
            this.xrTable14.LocationFloat = new DevExpress.Utils.PointFloat(0.002861023F, 10.00001F);
            this.xrTable14.Name = "xrTable14";
            this.xrTable14.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow27,
            this.xrTableRow28,
            this.xrTableRow29,
            this.xrTableRow7,
            this.xrTableRow24,
            this.xrTableRow30,
            this.xrTableRow31,
            this.xrTableRow32,
            this.xrTableRow33,
            this.xrTableRow34,
            this.xrTableRow35,
            this.xrTableRow36,
            this.xrTableRow37,
            this.xrTableRow38,
            this.xrTableRow39});
            this.xrTable14.SizeF = new System.Drawing.SizeF(1402.995F, 225F);
            // 
            // xrTableRow27
            // 
            this.xrTableRow27.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell355,
            this.xrTableCell356,
            this.xrTableCell357,
            this.xrTableCell358,
            this.xrTableCell359,
            this.xrTableCell360,
            this.xrTableCell361,
            this.xrTableCell362,
            this.xrTableCell363,
            this.xrTableCell364,
            this.xrTableCell365,
            this.xrTableCell366,
            this.xrTableCell367,
            this.xrTableCell368});
            this.xrTableRow27.Dpi = 100F;
            this.xrTableRow27.Name = "xrTableRow27";
            this.xrTableRow27.Weight = 11.5D;
            // 
            // xrTableCell355
            // 
            this.xrTableCell355.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)));
            this.xrTableCell355.Dpi = 100F;
            this.xrTableCell355.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell355.Name = "xrTableCell355";
            this.xrTableCell355.StylePriority.UseBorders = false;
            this.xrTableCell355.StylePriority.UseFont = false;
            this.xrTableCell355.Text = " Sales per Unit";
            this.xrTableCell355.Weight = 0.046132403429672113D;
            // 
            // xrTableCell356
            // 
            this.xrTableCell356.Borders = DevExpress.XtraPrinting.BorderSide.Top;
            this.xrTableCell356.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail1.TotalSalesUnit1", "{0:n}")});
            this.xrTableCell356.Dpi = 100F;
            this.xrTableCell356.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell356.Name = "xrTableCell356";
            this.xrTableCell356.StylePriority.UseBorders = false;
            this.xrTableCell356.StylePriority.UseFont = false;
            this.xrTableCell356.StylePriority.UseTextAlignment = false;
            this.xrTableCell356.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell356.Weight = 0.011456676506122643D;
            // 
            // xrTableCell357
            // 
            this.xrTableCell357.Borders = DevExpress.XtraPrinting.BorderSide.Top;
            this.xrTableCell357.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail1.TotalSalesUnit2", "{0:n}")});
            this.xrTableCell357.Dpi = 100F;
            this.xrTableCell357.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell357.Name = "xrTableCell357";
            this.xrTableCell357.StylePriority.UseBorders = false;
            this.xrTableCell357.StylePriority.UseFont = false;
            this.xrTableCell357.StylePriority.UseTextAlignment = false;
            this.xrTableCell357.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell357.Weight = 0.011456439447518315D;
            // 
            // xrTableCell358
            // 
            this.xrTableCell358.Borders = DevExpress.XtraPrinting.BorderSide.Top;
            this.xrTableCell358.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail1.TotalSalesUnit3", "{0:n}")});
            this.xrTableCell358.Dpi = 100F;
            this.xrTableCell358.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell358.Name = "xrTableCell358";
            this.xrTableCell358.StylePriority.UseBorders = false;
            this.xrTableCell358.StylePriority.UseFont = false;
            this.xrTableCell358.StylePriority.UseTextAlignment = false;
            this.xrTableCell358.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell358.Weight = 0.01145644711665144D;
            // 
            // xrTableCell359
            // 
            this.xrTableCell359.Borders = DevExpress.XtraPrinting.BorderSide.Top;
            this.xrTableCell359.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail1.TotalSalesUnit4", "{0:n}")});
            this.xrTableCell359.Dpi = 100F;
            this.xrTableCell359.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell359.Name = "xrTableCell359";
            this.xrTableCell359.StylePriority.UseBorders = false;
            this.xrTableCell359.StylePriority.UseFont = false;
            this.xrTableCell359.StylePriority.UseTextAlignment = false;
            this.xrTableCell359.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell359.Weight = 0.011456464340136261D;
            // 
            // xrTableCell360
            // 
            this.xrTableCell360.Borders = DevExpress.XtraPrinting.BorderSide.Top;
            this.xrTableCell360.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail1.TotalSalesUnit5", "{0:n}")});
            this.xrTableCell360.Dpi = 100F;
            this.xrTableCell360.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell360.Name = "xrTableCell360";
            this.xrTableCell360.StylePriority.UseBorders = false;
            this.xrTableCell360.StylePriority.UseFont = false;
            this.xrTableCell360.StylePriority.UseTextAlignment = false;
            this.xrTableCell360.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell360.Weight = 0.011456379748427982D;
            // 
            // xrTableCell361
            // 
            this.xrTableCell361.Borders = DevExpress.XtraPrinting.BorderSide.Top;
            this.xrTableCell361.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail1.TotalSalesUnit6", "{0:n}")});
            this.xrTableCell361.Dpi = 100F;
            this.xrTableCell361.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell361.Name = "xrTableCell361";
            this.xrTableCell361.StylePriority.UseBorders = false;
            this.xrTableCell361.StylePriority.UseFont = false;
            this.xrTableCell361.StylePriority.UseTextAlignment = false;
            this.xrTableCell361.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell361.Weight = 0.011456481623145959D;
            // 
            // xrTableCell362
            // 
            this.xrTableCell362.Borders = DevExpress.XtraPrinting.BorderSide.Top;
            this.xrTableCell362.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail1.TotalSalesUnit7", "{0:n}")});
            this.xrTableCell362.Dpi = 100F;
            this.xrTableCell362.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell362.Name = "xrTableCell362";
            this.xrTableCell362.StylePriority.UseBorders = false;
            this.xrTableCell362.StylePriority.UseFont = false;
            this.xrTableCell362.StylePriority.UseTextAlignment = false;
            this.xrTableCell362.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell362.Weight = 0.011456448104521738D;
            // 
            // xrTableCell363
            // 
            this.xrTableCell363.Borders = DevExpress.XtraPrinting.BorderSide.Top;
            this.xrTableCell363.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail1.TotalSalesUnit8", "{0:n}")});
            this.xrTableCell363.Dpi = 100F;
            this.xrTableCell363.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell363.Name = "xrTableCell363";
            this.xrTableCell363.StylePriority.UseBorders = false;
            this.xrTableCell363.StylePriority.UseFont = false;
            this.xrTableCell363.StylePriority.UseTextAlignment = false;
            this.xrTableCell363.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell363.Weight = 0.009940725420112706D;
            // 
            // xrTableCell364
            // 
            this.xrTableCell364.Borders = DevExpress.XtraPrinting.BorderSide.Top;
            this.xrTableCell364.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail1.TotalSalesUnit9", "{0:n}")});
            this.xrTableCell364.Dpi = 100F;
            this.xrTableCell364.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell364.Name = "xrTableCell364";
            this.xrTableCell364.StylePriority.UseBorders = false;
            this.xrTableCell364.StylePriority.UseFont = false;
            this.xrTableCell364.StylePriority.UseTextAlignment = false;
            this.xrTableCell364.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell364.Weight = 0.012978203905482485D;
            // 
            // xrTableCell365
            // 
            this.xrTableCell365.Borders = DevExpress.XtraPrinting.BorderSide.Top;
            this.xrTableCell365.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail1.TotalSalesUnit10", "{0:n}")});
            this.xrTableCell365.Dpi = 100F;
            this.xrTableCell365.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell365.Name = "xrTableCell365";
            this.xrTableCell365.StylePriority.UseBorders = false;
            this.xrTableCell365.StylePriority.UseFont = false;
            this.xrTableCell365.StylePriority.UseTextAlignment = false;
            this.xrTableCell365.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell365.Weight = 0.011450439867777159D;
            // 
            // xrTableCell366
            // 
            this.xrTableCell366.Borders = DevExpress.XtraPrinting.BorderSide.Top;
            this.xrTableCell366.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail1.TotalSalesUnit11", "{0:n}")});
            this.xrTableCell366.Dpi = 100F;
            this.xrTableCell366.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell366.Name = "xrTableCell366";
            this.xrTableCell366.StylePriority.UseBorders = false;
            this.xrTableCell366.StylePriority.UseFont = false;
            this.xrTableCell366.StylePriority.UseTextAlignment = false;
            this.xrTableCell366.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell366.Weight = 0.011455021519186374D;
            // 
            // xrTableCell367
            // 
            this.xrTableCell367.Borders = DevExpress.XtraPrinting.BorderSide.Top;
            this.xrTableCell367.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail1.TotalSalesUnit12", "{0:n}")});
            this.xrTableCell367.Dpi = 100F;
            this.xrTableCell367.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell367.Name = "xrTableCell367";
            this.xrTableCell367.StylePriority.UseBorders = false;
            this.xrTableCell367.StylePriority.UseFont = false;
            this.xrTableCell367.StylePriority.UseTextAlignment = false;
            xrSummary102.FormatString = "{0:#,0}";
            this.xrTableCell367.Summary = xrSummary102;
            this.xrTableCell367.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell367.Weight = 0.01145697336366436D;
            // 
            // xrTableCell368
            // 
            this.xrTableCell368.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)));
            this.xrTableCell368.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail1.TotalSalesUnit", "{0:n}")});
            this.xrTableCell368.Dpi = 100F;
            this.xrTableCell368.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell368.Name = "xrTableCell368";
            this.xrTableCell368.StylePriority.UseBorders = false;
            this.xrTableCell368.StylePriority.UseFont = false;
            this.xrTableCell368.StylePriority.UseTextAlignment = false;
            this.xrTableCell368.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell368.Weight = 0.011457284165937842D;
            // 
            // xrTableRow28
            // 
            this.xrTableRow28.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell369,
            this.xrTableCell370,
            this.xrTableCell371,
            this.xrTableCell372,
            this.xrTableCell373,
            this.xrTableCell374,
            this.xrTableCell375,
            this.xrTableCell376,
            this.xrTableCell377,
            this.xrTableCell378,
            this.xrTableCell379,
            this.xrTableCell380,
            this.xrTableCell381,
            this.xrTableCell382});
            this.xrTableRow28.Dpi = 100F;
            this.xrTableRow28.Name = "xrTableRow28";
            this.xrTableRow28.Weight = 11.5D;
            // 
            // xrTableCell369
            // 
            this.xrTableCell369.Borders = DevExpress.XtraPrinting.BorderSide.Left;
            this.xrTableCell369.Dpi = 100F;
            this.xrTableCell369.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell369.Name = "xrTableCell369";
            this.xrTableCell369.StylePriority.UseBorders = false;
            this.xrTableCell369.StylePriority.UseFont = false;
            this.xrTableCell369.Text = " Variable Cost per Unit";
            this.xrTableCell369.Weight = 0.046132403429672113D;
            // 
            // xrTableCell370
            // 
            this.xrTableCell370.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrTableCell370.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail1.VariableCostUnit1", "{0:n}")});
            this.xrTableCell370.Dpi = 100F;
            this.xrTableCell370.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell370.Name = "xrTableCell370";
            this.xrTableCell370.StylePriority.UseBorders = false;
            this.xrTableCell370.StylePriority.UseFont = false;
            this.xrTableCell370.StylePriority.UseTextAlignment = false;
            this.xrTableCell370.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell370.Weight = 0.011456676506122643D;
            // 
            // xrTableCell371
            // 
            this.xrTableCell371.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrTableCell371.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail1.VariableCostUnit2", "{0:n}")});
            this.xrTableCell371.Dpi = 100F;
            this.xrTableCell371.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell371.Name = "xrTableCell371";
            this.xrTableCell371.StylePriority.UseBorders = false;
            this.xrTableCell371.StylePriority.UseFont = false;
            this.xrTableCell371.StylePriority.UseTextAlignment = false;
            this.xrTableCell371.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell371.Weight = 0.011456676506122643D;
            // 
            // xrTableCell372
            // 
            this.xrTableCell372.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrTableCell372.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail1.VariableCostUnit3", "{0:n}")});
            this.xrTableCell372.Dpi = 100F;
            this.xrTableCell372.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell372.Name = "xrTableCell372";
            this.xrTableCell372.StylePriority.UseBorders = false;
            this.xrTableCell372.StylePriority.UseFont = false;
            this.xrTableCell372.StylePriority.UseTextAlignment = false;
            this.xrTableCell372.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell372.Weight = 0.011456676506122643D;
            // 
            // xrTableCell373
            // 
            this.xrTableCell373.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrTableCell373.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail1.VariableCostUnit4", "{0:n}")});
            this.xrTableCell373.Dpi = 100F;
            this.xrTableCell373.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell373.Name = "xrTableCell373";
            this.xrTableCell373.StylePriority.UseBorders = false;
            this.xrTableCell373.StylePriority.UseFont = false;
            this.xrTableCell373.StylePriority.UseTextAlignment = false;
            this.xrTableCell373.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell373.Weight = 0.011456676506122643D;
            // 
            // xrTableCell374
            // 
            this.xrTableCell374.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrTableCell374.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail1.VariableCostUnit5", "{0:n}")});
            this.xrTableCell374.Dpi = 100F;
            this.xrTableCell374.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell374.Name = "xrTableCell374";
            this.xrTableCell374.StylePriority.UseBorders = false;
            this.xrTableCell374.StylePriority.UseFont = false;
            this.xrTableCell374.StylePriority.UseTextAlignment = false;
            this.xrTableCell374.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell374.Weight = 0.011456676506122643D;
            // 
            // xrTableCell375
            // 
            this.xrTableCell375.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrTableCell375.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail1.VariableCostUnit6", "{0:n}")});
            this.xrTableCell375.Dpi = 100F;
            this.xrTableCell375.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell375.Name = "xrTableCell375";
            this.xrTableCell375.StylePriority.UseBorders = false;
            this.xrTableCell375.StylePriority.UseFont = false;
            this.xrTableCell375.StylePriority.UseTextAlignment = false;
            this.xrTableCell375.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell375.Weight = 0.011456676506122643D;
            // 
            // xrTableCell376
            // 
            this.xrTableCell376.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrTableCell376.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail1.VariableCostUnit7", "{0:n}")});
            this.xrTableCell376.Dpi = 100F;
            this.xrTableCell376.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell376.Name = "xrTableCell376";
            this.xrTableCell376.StylePriority.UseBorders = false;
            this.xrTableCell376.StylePriority.UseFont = false;
            this.xrTableCell376.StylePriority.UseTextAlignment = false;
            this.xrTableCell376.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell376.Weight = 0.011456676506122643D;
            // 
            // xrTableCell377
            // 
            this.xrTableCell377.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrTableCell377.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail1.VariableCostUnit8", "{0:n}")});
            this.xrTableCell377.Dpi = 100F;
            this.xrTableCell377.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell377.Name = "xrTableCell377";
            this.xrTableCell377.StylePriority.UseBorders = false;
            this.xrTableCell377.StylePriority.UseFont = false;
            this.xrTableCell377.StylePriority.UseTextAlignment = false;
            this.xrTableCell377.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell377.Weight = 0.011456676506122643D;
            // 
            // xrTableCell378
            // 
            this.xrTableCell378.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrTableCell378.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail1.VariableCostUnit9", "{0:n}")});
            this.xrTableCell378.Dpi = 100F;
            this.xrTableCell378.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell378.Name = "xrTableCell378";
            this.xrTableCell378.StylePriority.UseBorders = false;
            this.xrTableCell378.StylePriority.UseFont = false;
            this.xrTableCell378.StylePriority.UseTextAlignment = false;
            this.xrTableCell378.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell378.Weight = 0.011456676506122643D;
            // 
            // xrTableCell379
            // 
            this.xrTableCell379.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrTableCell379.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail1.VariableCostUnit10", "{0:n}")});
            this.xrTableCell379.Dpi = 100F;
            this.xrTableCell379.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell379.Name = "xrTableCell379";
            this.xrTableCell379.StylePriority.UseBorders = false;
            this.xrTableCell379.StylePriority.UseFont = false;
            this.xrTableCell379.StylePriority.UseTextAlignment = false;
            this.xrTableCell379.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell379.Weight = 0.011456676506122643D;
            // 
            // xrTableCell380
            // 
            this.xrTableCell380.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrTableCell380.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail1.VariableCostUnit11", "{0:n}")});
            this.xrTableCell380.Dpi = 100F;
            this.xrTableCell380.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell380.Name = "xrTableCell380";
            this.xrTableCell380.StylePriority.UseBorders = false;
            this.xrTableCell380.StylePriority.UseFont = false;
            this.xrTableCell380.StylePriority.UseTextAlignment = false;
            this.xrTableCell380.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell380.Weight = 0.011456676506122643D;
            // 
            // xrTableCell381
            // 
            this.xrTableCell381.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrTableCell381.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail1.VariableCostUnit12", "{0:n}")});
            this.xrTableCell381.Dpi = 100F;
            this.xrTableCell381.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell381.Name = "xrTableCell381";
            this.xrTableCell381.StylePriority.UseBorders = false;
            this.xrTableCell381.StylePriority.UseFont = false;
            this.xrTableCell381.StylePriority.UseTextAlignment = false;
            this.xrTableCell381.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell381.Weight = 0.011456676506122643D;
            // 
            // xrTableCell382
            // 
            this.xrTableCell382.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell382.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail1.VariableCostUnit", "{0:n}")});
            this.xrTableCell382.Dpi = 100F;
            this.xrTableCell382.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell382.Name = "xrTableCell382";
            this.xrTableCell382.StylePriority.UseBorders = false;
            this.xrTableCell382.StylePriority.UseFont = false;
            this.xrTableCell382.StylePriority.UseTextAlignment = false;
            this.xrTableCell382.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell382.Weight = 0.011456676506122643D;
            // 
            // xrTableRow29
            // 
            this.xrTableRow29.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell383,
            this.xrTableCell384,
            this.xrTableCell385,
            this.xrTableCell386,
            this.xrTableCell387,
            this.xrTableCell388,
            this.xrTableCell389,
            this.xrTableCell390,
            this.xrTableCell391,
            this.xrTableCell392,
            this.xrTableCell393,
            this.xrTableCell394,
            this.xrTableCell395,
            this.xrTableCell396});
            this.xrTableRow29.Dpi = 100F;
            this.xrTableRow29.Name = "xrTableRow29";
            this.xrTableRow29.Weight = 11.5D;
            // 
            // xrTableCell383
            // 
            this.xrTableCell383.Borders = DevExpress.XtraPrinting.BorderSide.Left;
            this.xrTableCell383.Dpi = 100F;
            this.xrTableCell383.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell383.Name = "xrTableCell383";
            this.xrTableCell383.StylePriority.UseBorders = false;
            this.xrTableCell383.StylePriority.UseFont = false;
            this.xrTableCell383.Text = " Gross Margin per Unit";
            this.xrTableCell383.Weight = 0.046132403429672113D;
            // 
            // xrTableCell384
            // 
            this.xrTableCell384.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail1.ContributionUnit1", "{0:n}")});
            this.xrTableCell384.Dpi = 100F;
            this.xrTableCell384.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell384.Name = "xrTableCell384";
            this.xrTableCell384.StylePriority.UseFont = false;
            this.xrTableCell384.StylePriority.UseTextAlignment = false;
            this.xrTableCell384.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell384.Weight = 0.011456676506122643D;
            // 
            // xrTableCell385
            // 
            this.xrTableCell385.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail1.ContributionUnit2", "{0:n}")});
            this.xrTableCell385.Dpi = 100F;
            this.xrTableCell385.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell385.Name = "xrTableCell385";
            this.xrTableCell385.StylePriority.UseFont = false;
            this.xrTableCell385.StylePriority.UseTextAlignment = false;
            this.xrTableCell385.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell385.Weight = 0.011456676506122643D;
            // 
            // xrTableCell386
            // 
            this.xrTableCell386.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail1.ContributionUnit3", "{0:n}")});
            this.xrTableCell386.Dpi = 100F;
            this.xrTableCell386.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell386.Name = "xrTableCell386";
            this.xrTableCell386.StylePriority.UseFont = false;
            this.xrTableCell386.StylePriority.UseTextAlignment = false;
            this.xrTableCell386.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell386.Weight = 0.011456676506122643D;
            // 
            // xrTableCell387
            // 
            this.xrTableCell387.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail1.ContributionUnit4", "{0:n}")});
            this.xrTableCell387.Dpi = 100F;
            this.xrTableCell387.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell387.Name = "xrTableCell387";
            this.xrTableCell387.StylePriority.UseFont = false;
            this.xrTableCell387.StylePriority.UseTextAlignment = false;
            this.xrTableCell387.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell387.Weight = 0.011456676506122643D;
            // 
            // xrTableCell388
            // 
            this.xrTableCell388.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail1.ContributionUnit5", "{0:n}")});
            this.xrTableCell388.Dpi = 100F;
            this.xrTableCell388.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell388.Name = "xrTableCell388";
            this.xrTableCell388.StylePriority.UseFont = false;
            this.xrTableCell388.StylePriority.UseTextAlignment = false;
            this.xrTableCell388.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell388.Weight = 0.011456676506122643D;
            // 
            // xrTableCell389
            // 
            this.xrTableCell389.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail1.ContributionUnit7", "{0:n}")});
            this.xrTableCell389.Dpi = 100F;
            this.xrTableCell389.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell389.Name = "xrTableCell389";
            this.xrTableCell389.StylePriority.UseFont = false;
            this.xrTableCell389.StylePriority.UseTextAlignment = false;
            this.xrTableCell389.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell389.Weight = 0.011456676506122643D;
            // 
            // xrTableCell390
            // 
            this.xrTableCell390.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail1.ContributionUnit7", "{0:n}")});
            this.xrTableCell390.Dpi = 100F;
            this.xrTableCell390.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell390.Name = "xrTableCell390";
            this.xrTableCell390.StylePriority.UseFont = false;
            this.xrTableCell390.StylePriority.UseTextAlignment = false;
            this.xrTableCell390.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell390.Weight = 0.011456676506122643D;
            // 
            // xrTableCell391
            // 
            this.xrTableCell391.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail1.ContributionUnit8", "{0:n}")});
            this.xrTableCell391.Dpi = 100F;
            this.xrTableCell391.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell391.Name = "xrTableCell391";
            this.xrTableCell391.StylePriority.UseFont = false;
            this.xrTableCell391.StylePriority.UseTextAlignment = false;
            this.xrTableCell391.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell391.Weight = 0.011456676506122643D;
            // 
            // xrTableCell392
            // 
            this.xrTableCell392.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail1.ContributionUnit9", "{0:n}")});
            this.xrTableCell392.Dpi = 100F;
            this.xrTableCell392.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell392.Name = "xrTableCell392";
            this.xrTableCell392.StylePriority.UseFont = false;
            this.xrTableCell392.StylePriority.UseTextAlignment = false;
            this.xrTableCell392.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell392.Weight = 0.011456676506122643D;
            // 
            // xrTableCell393
            // 
            this.xrTableCell393.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail1.ContributionUnit10", "{0:n}")});
            this.xrTableCell393.Dpi = 100F;
            this.xrTableCell393.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell393.Name = "xrTableCell393";
            this.xrTableCell393.StylePriority.UseFont = false;
            this.xrTableCell393.StylePriority.UseTextAlignment = false;
            this.xrTableCell393.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell393.Weight = 0.011456676506122643D;
            // 
            // xrTableCell394
            // 
            this.xrTableCell394.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail1.ContributionUnit11", "{0:n}")});
            this.xrTableCell394.Dpi = 100F;
            this.xrTableCell394.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell394.Name = "xrTableCell394";
            this.xrTableCell394.StylePriority.UseFont = false;
            this.xrTableCell394.StylePriority.UseTextAlignment = false;
            this.xrTableCell394.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell394.Weight = 0.011456676506122643D;
            // 
            // xrTableCell395
            // 
            this.xrTableCell395.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail1.ContributionUnit12", "{0:n}")});
            this.xrTableCell395.Dpi = 100F;
            this.xrTableCell395.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell395.Name = "xrTableCell395";
            this.xrTableCell395.StylePriority.UseFont = false;
            this.xrTableCell395.StylePriority.UseTextAlignment = false;
            this.xrTableCell395.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell395.Weight = 0.011456676506122643D;
            // 
            // xrTableCell396
            // 
            this.xrTableCell396.Borders = DevExpress.XtraPrinting.BorderSide.Right;
            this.xrTableCell396.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail1.ContributionUnit", "{0:n}")});
            this.xrTableCell396.Dpi = 100F;
            this.xrTableCell396.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell396.Name = "xrTableCell396";
            this.xrTableCell396.StylePriority.UseBorders = false;
            this.xrTableCell396.StylePriority.UseFont = false;
            this.xrTableCell396.StylePriority.UseTextAlignment = false;
            this.xrTableCell396.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell396.Weight = 0.011456676506122643D;
            // 
            // xrTableRow7
            // 
            this.xrTableRow7.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell82,
            this.xrTableCell83,
            this.xrTableCell84,
            this.xrTableCell85,
            this.xrTableCell86,
            this.xrTableCell87,
            this.xrTableCell88,
            this.xrTableCell89,
            this.xrTableCell90,
            this.xrTableCell91,
            this.xrTableCell92,
            this.xrTableCell93,
            this.xrTableCell94,
            this.xrTableCell314});
            this.xrTableRow7.Dpi = 100F;
            this.xrTableRow7.Name = "xrTableRow7";
            this.xrTableRow7.Weight = 11.5D;
            // 
            // xrTableCell82
            // 
            this.xrTableCell82.Borders = DevExpress.XtraPrinting.BorderSide.Left;
            this.xrTableCell82.Dpi = 100F;
            this.xrTableCell82.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell82.Name = "xrTableCell82";
            this.xrTableCell82.StylePriority.UseBorders = false;
            this.xrTableCell82.StylePriority.UseFont = false;
            this.xrTableCell82.Text = " Fixed Cost per Unit";
            this.xrTableCell82.Weight = 0.046132403429672113D;
            // 
            // xrTableCell83
            // 
            this.xrTableCell83.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrTableCell83.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail2.FixedCostUnit1", "{0:n}")});
            this.xrTableCell83.Dpi = 100F;
            this.xrTableCell83.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell83.Name = "xrTableCell83";
            this.xrTableCell83.StylePriority.UseBorders = false;
            this.xrTableCell83.StylePriority.UseFont = false;
            this.xrTableCell83.StylePriority.UseTextAlignment = false;
            this.xrTableCell83.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell83.Weight = 0.011456676506122643D;
            // 
            // xrTableCell84
            // 
            this.xrTableCell84.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrTableCell84.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail2.FixedCostUnit2", "{0:n}")});
            this.xrTableCell84.Dpi = 100F;
            this.xrTableCell84.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell84.Name = "xrTableCell84";
            this.xrTableCell84.StylePriority.UseBorders = false;
            this.xrTableCell84.StylePriority.UseFont = false;
            this.xrTableCell84.StylePriority.UseTextAlignment = false;
            this.xrTableCell84.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell84.Weight = 0.011456676506122643D;
            // 
            // xrTableCell85
            // 
            this.xrTableCell85.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrTableCell85.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail2.FixedCostUnit3", "{0:n}")});
            this.xrTableCell85.Dpi = 100F;
            this.xrTableCell85.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell85.Name = "xrTableCell85";
            this.xrTableCell85.StylePriority.UseBorders = false;
            this.xrTableCell85.StylePriority.UseFont = false;
            this.xrTableCell85.StylePriority.UseTextAlignment = false;
            this.xrTableCell85.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell85.Weight = 0.011456676506122643D;
            // 
            // xrTableCell86
            // 
            this.xrTableCell86.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrTableCell86.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail2.FixedCostUnit4", "{0:n}")});
            this.xrTableCell86.Dpi = 100F;
            this.xrTableCell86.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell86.Name = "xrTableCell86";
            this.xrTableCell86.StylePriority.UseBorders = false;
            this.xrTableCell86.StylePriority.UseFont = false;
            this.xrTableCell86.StylePriority.UseTextAlignment = false;
            this.xrTableCell86.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell86.Weight = 0.011456676506122643D;
            // 
            // xrTableCell87
            // 
            this.xrTableCell87.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrTableCell87.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail2.FixedCostUnit5", "{0:n}")});
            this.xrTableCell87.Dpi = 100F;
            this.xrTableCell87.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell87.Name = "xrTableCell87";
            this.xrTableCell87.StylePriority.UseBorders = false;
            this.xrTableCell87.StylePriority.UseFont = false;
            this.xrTableCell87.StylePriority.UseTextAlignment = false;
            this.xrTableCell87.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell87.Weight = 0.011456676506122643D;
            // 
            // xrTableCell88
            // 
            this.xrTableCell88.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrTableCell88.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail2.FixedCostUnit6", "{0:n}")});
            this.xrTableCell88.Dpi = 100F;
            this.xrTableCell88.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell88.Name = "xrTableCell88";
            this.xrTableCell88.StylePriority.UseBorders = false;
            this.xrTableCell88.StylePriority.UseFont = false;
            this.xrTableCell88.StylePriority.UseTextAlignment = false;
            this.xrTableCell88.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell88.Weight = 0.011456676506122643D;
            // 
            // xrTableCell89
            // 
            this.xrTableCell89.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrTableCell89.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail2.TotalFixedPer7", "{0:n}")});
            this.xrTableCell89.Dpi = 100F;
            this.xrTableCell89.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell89.Name = "xrTableCell89";
            this.xrTableCell89.StylePriority.UseBorders = false;
            this.xrTableCell89.StylePriority.UseFont = false;
            this.xrTableCell89.StylePriority.UseTextAlignment = false;
            this.xrTableCell89.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell89.Weight = 0.011456676506122643D;
            // 
            // xrTableCell90
            // 
            this.xrTableCell90.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrTableCell90.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail2.TotalFixedPer8", "{0:n}")});
            this.xrTableCell90.Dpi = 100F;
            this.xrTableCell90.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell90.Name = "xrTableCell90";
            this.xrTableCell90.StylePriority.UseBorders = false;
            this.xrTableCell90.StylePriority.UseFont = false;
            this.xrTableCell90.StylePriority.UseTextAlignment = false;
            this.xrTableCell90.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell90.Weight = 0.011456676506122643D;
            // 
            // xrTableCell91
            // 
            this.xrTableCell91.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrTableCell91.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail2.TotalFixedPer9", "{0:n}")});
            this.xrTableCell91.Dpi = 100F;
            this.xrTableCell91.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell91.Name = "xrTableCell91";
            this.xrTableCell91.StylePriority.UseBorders = false;
            this.xrTableCell91.StylePriority.UseFont = false;
            this.xrTableCell91.StylePriority.UseTextAlignment = false;
            this.xrTableCell91.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell91.Weight = 0.011456676506122643D;
            // 
            // xrTableCell92
            // 
            this.xrTableCell92.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrTableCell92.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail2.FixedCostUnit10", "{0:n}")});
            this.xrTableCell92.Dpi = 100F;
            this.xrTableCell92.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell92.Name = "xrTableCell92";
            this.xrTableCell92.StylePriority.UseBorders = false;
            this.xrTableCell92.StylePriority.UseFont = false;
            this.xrTableCell92.StylePriority.UseTextAlignment = false;
            this.xrTableCell92.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell92.Weight = 0.011456676506122643D;
            // 
            // xrTableCell93
            // 
            this.xrTableCell93.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrTableCell93.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail2.FixedCostUnit11", "{0:n}")});
            this.xrTableCell93.Dpi = 100F;
            this.xrTableCell93.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell93.Name = "xrTableCell93";
            this.xrTableCell93.StylePriority.UseBorders = false;
            this.xrTableCell93.StylePriority.UseFont = false;
            this.xrTableCell93.StylePriority.UseTextAlignment = false;
            this.xrTableCell93.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell93.Weight = 0.011456676506122643D;
            // 
            // xrTableCell94
            // 
            this.xrTableCell94.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrTableCell94.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail2.FixedCostUnit12", "{0:n}")});
            this.xrTableCell94.Dpi = 100F;
            this.xrTableCell94.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell94.Name = "xrTableCell94";
            this.xrTableCell94.StylePriority.UseBorders = false;
            this.xrTableCell94.StylePriority.UseFont = false;
            this.xrTableCell94.StylePriority.UseTextAlignment = false;
            this.xrTableCell94.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell94.Weight = 0.011456676506122643D;
            // 
            // xrTableCell314
            // 
            this.xrTableCell314.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell314.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail2.FixedCostUnit", "{0:n}")});
            this.xrTableCell314.Dpi = 100F;
            this.xrTableCell314.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell314.Name = "xrTableCell314";
            this.xrTableCell314.StylePriority.UseBorders = false;
            this.xrTableCell314.StylePriority.UseFont = false;
            this.xrTableCell314.StylePriority.UseTextAlignment = false;
            this.xrTableCell314.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell314.Weight = 0.011456676506122643D;
            // 
            // xrTableRow24
            // 
            this.xrTableRow24.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell315,
            this.xrTableCell316,
            this.xrTableCell317,
            this.xrTableCell318,
            this.xrTableCell319,
            this.xrTableCell320,
            this.xrTableCell321,
            this.xrTableCell322,
            this.xrTableCell323,
            this.xrTableCell324,
            this.xrTableCell325,
            this.xrTableCell326,
            this.xrTableCell340,
            this.xrTableCell341});
            this.xrTableRow24.Dpi = 100F;
            this.xrTableRow24.Name = "xrTableRow24";
            this.xrTableRow24.Weight = 11.5D;
            // 
            // xrTableCell315
            // 
            this.xrTableCell315.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell315.Dpi = 100F;
            this.xrTableCell315.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell315.Name = "xrTableCell315";
            this.xrTableCell315.StylePriority.UseBorders = false;
            this.xrTableCell315.StylePriority.UseFont = false;
            this.xrTableCell315.Text = " Net Profit/Loss per Unit";
            this.xrTableCell315.Weight = 0.046132403429672113D;
            // 
            // xrTableCell316
            // 
            this.xrTableCell316.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrTableCell316.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail2.OperatingIncomeUnit1", "{0:n}")});
            this.xrTableCell316.Dpi = 100F;
            this.xrTableCell316.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell316.Name = "xrTableCell316";
            this.xrTableCell316.StylePriority.UseBorders = false;
            this.xrTableCell316.StylePriority.UseFont = false;
            this.xrTableCell316.StylePriority.UseTextAlignment = false;
            this.xrTableCell316.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell316.Weight = 0.011456676506122643D;
            // 
            // xrTableCell317
            // 
            this.xrTableCell317.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrTableCell317.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail2.OperatingIncomeUnit2", "{0:n}")});
            this.xrTableCell317.Dpi = 100F;
            this.xrTableCell317.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell317.Name = "xrTableCell317";
            this.xrTableCell317.StylePriority.UseBorders = false;
            this.xrTableCell317.StylePriority.UseFont = false;
            this.xrTableCell317.StylePriority.UseTextAlignment = false;
            this.xrTableCell317.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell317.Weight = 0.011456676506122643D;
            // 
            // xrTableCell318
            // 
            this.xrTableCell318.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrTableCell318.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail2.OperatingIncomeUnit3", "{0:n}")});
            this.xrTableCell318.Dpi = 100F;
            this.xrTableCell318.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell318.Name = "xrTableCell318";
            this.xrTableCell318.StylePriority.UseBorders = false;
            this.xrTableCell318.StylePriority.UseFont = false;
            this.xrTableCell318.StylePriority.UseTextAlignment = false;
            this.xrTableCell318.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell318.Weight = 0.011456676506122643D;
            // 
            // xrTableCell319
            // 
            this.xrTableCell319.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrTableCell319.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail2.OperatingIncomeUnit4", "{0:n}")});
            this.xrTableCell319.Dpi = 100F;
            this.xrTableCell319.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell319.Name = "xrTableCell319";
            this.xrTableCell319.StylePriority.UseBorders = false;
            this.xrTableCell319.StylePriority.UseFont = false;
            this.xrTableCell319.StylePriority.UseTextAlignment = false;
            this.xrTableCell319.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell319.Weight = 0.011456676506122643D;
            // 
            // xrTableCell320
            // 
            this.xrTableCell320.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrTableCell320.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail2.OperatingIncomeUnit5", "{0:n}")});
            this.xrTableCell320.Dpi = 100F;
            this.xrTableCell320.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell320.Name = "xrTableCell320";
            this.xrTableCell320.StylePriority.UseBorders = false;
            this.xrTableCell320.StylePriority.UseFont = false;
            this.xrTableCell320.StylePriority.UseTextAlignment = false;
            this.xrTableCell320.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell320.Weight = 0.011456676506122643D;
            // 
            // xrTableCell321
            // 
            this.xrTableCell321.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrTableCell321.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail2.OperatingIncomeUnit6", "{0:n}")});
            this.xrTableCell321.Dpi = 100F;
            this.xrTableCell321.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell321.Name = "xrTableCell321";
            this.xrTableCell321.StylePriority.UseBorders = false;
            this.xrTableCell321.StylePriority.UseFont = false;
            this.xrTableCell321.StylePriority.UseTextAlignment = false;
            this.xrTableCell321.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell321.Weight = 0.011456676506122643D;
            // 
            // xrTableCell322
            // 
            this.xrTableCell322.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrTableCell322.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail2.OperatingIncomeUnit7", "{0:n}")});
            this.xrTableCell322.Dpi = 100F;
            this.xrTableCell322.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell322.Name = "xrTableCell322";
            this.xrTableCell322.StylePriority.UseBorders = false;
            this.xrTableCell322.StylePriority.UseFont = false;
            this.xrTableCell322.StylePriority.UseTextAlignment = false;
            this.xrTableCell322.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell322.Weight = 0.011456676506122643D;
            // 
            // xrTableCell323
            // 
            this.xrTableCell323.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrTableCell323.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail2.OperatingIncomeUnit8", "{0:n}")});
            this.xrTableCell323.Dpi = 100F;
            this.xrTableCell323.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell323.Name = "xrTableCell323";
            this.xrTableCell323.StylePriority.UseBorders = false;
            this.xrTableCell323.StylePriority.UseFont = false;
            this.xrTableCell323.StylePriority.UseTextAlignment = false;
            this.xrTableCell323.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell323.Weight = 0.011456676506122643D;
            // 
            // xrTableCell324
            // 
            this.xrTableCell324.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrTableCell324.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail2.OperatingIncomeUnit9", "{0:n}")});
            this.xrTableCell324.Dpi = 100F;
            this.xrTableCell324.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell324.Name = "xrTableCell324";
            this.xrTableCell324.StylePriority.UseBorders = false;
            this.xrTableCell324.StylePriority.UseFont = false;
            this.xrTableCell324.StylePriority.UseTextAlignment = false;
            this.xrTableCell324.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell324.Weight = 0.011456676506122643D;
            // 
            // xrTableCell325
            // 
            this.xrTableCell325.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrTableCell325.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail2.OperatingIncomeUnit10", "{0:n}")});
            this.xrTableCell325.Dpi = 100F;
            this.xrTableCell325.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell325.Name = "xrTableCell325";
            this.xrTableCell325.StylePriority.UseBorders = false;
            this.xrTableCell325.StylePriority.UseFont = false;
            this.xrTableCell325.StylePriority.UseTextAlignment = false;
            this.xrTableCell325.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell325.Weight = 0.011456676506122643D;
            // 
            // xrTableCell326
            // 
            this.xrTableCell326.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrTableCell326.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail2.OperatingIncomeUnit11", "{0:n}")});
            this.xrTableCell326.Dpi = 100F;
            this.xrTableCell326.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell326.Name = "xrTableCell326";
            this.xrTableCell326.StylePriority.UseBorders = false;
            this.xrTableCell326.StylePriority.UseFont = false;
            this.xrTableCell326.StylePriority.UseTextAlignment = false;
            this.xrTableCell326.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell326.Weight = 0.011456676506122643D;
            // 
            // xrTableCell340
            // 
            this.xrTableCell340.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrTableCell340.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail2.OperatingIncomeUnit12", "{0:n}")});
            this.xrTableCell340.Dpi = 100F;
            this.xrTableCell340.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell340.Name = "xrTableCell340";
            this.xrTableCell340.StylePriority.UseBorders = false;
            this.xrTableCell340.StylePriority.UseFont = false;
            this.xrTableCell340.StylePriority.UseTextAlignment = false;
            this.xrTableCell340.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell340.Weight = 0.011456676506122643D;
            // 
            // xrTableCell341
            // 
            this.xrTableCell341.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell341.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail2.OperatingIncomeUnit", "{0:n}")});
            this.xrTableCell341.Dpi = 100F;
            this.xrTableCell341.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell341.Name = "xrTableCell341";
            this.xrTableCell341.StylePriority.UseBorders = false;
            this.xrTableCell341.StylePriority.UseFont = false;
            this.xrTableCell341.StylePriority.UseTextAlignment = false;
            this.xrTableCell341.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell341.Weight = 0.011456676506122643D;
            // 
            // xrTableRow30
            // 
            this.xrTableRow30.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell397,
            this.xrTableCell398,
            this.xrTableCell399,
            this.xrTableCell400,
            this.xrTableCell401,
            this.xrTableCell402,
            this.xrTableCell403,
            this.xrTableCell404,
            this.xrTableCell405,
            this.xrTableCell406,
            this.xrTableCell407,
            this.xrTableCell408,
            this.xrTableCell409,
            this.xrTableCell410});
            this.xrTableRow30.Dpi = 100F;
            this.xrTableRow30.Name = "xrTableRow30";
            this.xrTableRow30.Weight = 11.5D;
            // 
            // xrTableCell397
            // 
            this.xrTableCell397.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTableCell397.Dpi = 100F;
            this.xrTableCell397.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell397.Name = "xrTableCell397";
            this.xrTableCell397.StylePriority.UseBorders = false;
            this.xrTableCell397.StylePriority.UseFont = false;
            this.xrTableCell397.Weight = 0.046132403429672113D;
            // 
            // xrTableCell398
            // 
            this.xrTableCell398.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTableCell398.Dpi = 100F;
            this.xrTableCell398.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell398.Name = "xrTableCell398";
            this.xrTableCell398.StylePriority.UseBorders = false;
            this.xrTableCell398.StylePriority.UseFont = false;
            this.xrTableCell398.StylePriority.UseTextAlignment = false;
            this.xrTableCell398.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell398.Weight = 0.011456676506122643D;
            // 
            // xrTableCell399
            // 
            this.xrTableCell399.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTableCell399.Dpi = 100F;
            this.xrTableCell399.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell399.Name = "xrTableCell399";
            this.xrTableCell399.StylePriority.UseBorders = false;
            this.xrTableCell399.StylePriority.UseFont = false;
            this.xrTableCell399.StylePriority.UseTextAlignment = false;
            this.xrTableCell399.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell399.Weight = 0.011456676506122643D;
            // 
            // xrTableCell400
            // 
            this.xrTableCell400.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTableCell400.Dpi = 100F;
            this.xrTableCell400.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell400.Name = "xrTableCell400";
            this.xrTableCell400.StylePriority.UseBorders = false;
            this.xrTableCell400.StylePriority.UseFont = false;
            this.xrTableCell400.StylePriority.UseTextAlignment = false;
            this.xrTableCell400.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell400.Weight = 0.011456676506122643D;
            // 
            // xrTableCell401
            // 
            this.xrTableCell401.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTableCell401.Dpi = 100F;
            this.xrTableCell401.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell401.Name = "xrTableCell401";
            this.xrTableCell401.StylePriority.UseBorders = false;
            this.xrTableCell401.StylePriority.UseFont = false;
            this.xrTableCell401.StylePriority.UseTextAlignment = false;
            this.xrTableCell401.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell401.Weight = 0.011456676506122643D;
            // 
            // xrTableCell402
            // 
            this.xrTableCell402.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTableCell402.Dpi = 100F;
            this.xrTableCell402.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell402.Name = "xrTableCell402";
            this.xrTableCell402.StylePriority.UseBorders = false;
            this.xrTableCell402.StylePriority.UseFont = false;
            this.xrTableCell402.StylePriority.UseTextAlignment = false;
            this.xrTableCell402.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell402.Weight = 0.011456676506122643D;
            // 
            // xrTableCell403
            // 
            this.xrTableCell403.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTableCell403.Dpi = 100F;
            this.xrTableCell403.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell403.Name = "xrTableCell403";
            this.xrTableCell403.StylePriority.UseBorders = false;
            this.xrTableCell403.StylePriority.UseFont = false;
            this.xrTableCell403.StylePriority.UseTextAlignment = false;
            this.xrTableCell403.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell403.Weight = 0.011456676506122643D;
            // 
            // xrTableCell404
            // 
            this.xrTableCell404.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTableCell404.Dpi = 100F;
            this.xrTableCell404.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell404.Name = "xrTableCell404";
            this.xrTableCell404.StylePriority.UseBorders = false;
            this.xrTableCell404.StylePriority.UseFont = false;
            this.xrTableCell404.StylePriority.UseTextAlignment = false;
            this.xrTableCell404.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell404.Weight = 0.011456676506122643D;
            // 
            // xrTableCell405
            // 
            this.xrTableCell405.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTableCell405.Dpi = 100F;
            this.xrTableCell405.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell405.Name = "xrTableCell405";
            this.xrTableCell405.StylePriority.UseBorders = false;
            this.xrTableCell405.StylePriority.UseFont = false;
            this.xrTableCell405.StylePriority.UseTextAlignment = false;
            this.xrTableCell405.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell405.Weight = 0.011456676506122643D;
            // 
            // xrTableCell406
            // 
            this.xrTableCell406.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTableCell406.Dpi = 100F;
            this.xrTableCell406.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell406.Name = "xrTableCell406";
            this.xrTableCell406.StylePriority.UseBorders = false;
            this.xrTableCell406.StylePriority.UseFont = false;
            this.xrTableCell406.StylePriority.UseTextAlignment = false;
            this.xrTableCell406.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell406.Weight = 0.011456676506122643D;
            // 
            // xrTableCell407
            // 
            this.xrTableCell407.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTableCell407.Dpi = 100F;
            this.xrTableCell407.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell407.Name = "xrTableCell407";
            this.xrTableCell407.StylePriority.UseBorders = false;
            this.xrTableCell407.StylePriority.UseFont = false;
            this.xrTableCell407.StylePriority.UseTextAlignment = false;
            this.xrTableCell407.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell407.Weight = 0.011456676506122643D;
            // 
            // xrTableCell408
            // 
            this.xrTableCell408.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTableCell408.Dpi = 100F;
            this.xrTableCell408.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell408.Name = "xrTableCell408";
            this.xrTableCell408.StylePriority.UseBorders = false;
            this.xrTableCell408.StylePriority.UseFont = false;
            this.xrTableCell408.StylePriority.UseTextAlignment = false;
            this.xrTableCell408.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell408.Weight = 0.011456676506122643D;
            // 
            // xrTableCell409
            // 
            this.xrTableCell409.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTableCell409.Dpi = 100F;
            this.xrTableCell409.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell409.Name = "xrTableCell409";
            this.xrTableCell409.StylePriority.UseBorders = false;
            this.xrTableCell409.StylePriority.UseFont = false;
            this.xrTableCell409.StylePriority.UseTextAlignment = false;
            this.xrTableCell409.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell409.Weight = 0.011456676506122643D;
            // 
            // xrTableCell410
            // 
            this.xrTableCell410.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTableCell410.Dpi = 100F;
            this.xrTableCell410.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell410.Name = "xrTableCell410";
            this.xrTableCell410.StylePriority.UseBorders = false;
            this.xrTableCell410.StylePriority.UseFont = false;
            this.xrTableCell410.StylePriority.UseTextAlignment = false;
            this.xrTableCell410.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell410.Weight = 0.011456676506122643D;
            // 
            // xrTableRow31
            // 
            this.xrTableRow31.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell411,
            this.xrTableCell412,
            this.xrTableCell413,
            this.xrTableCell414,
            this.xrTableCell415,
            this.xrTableCell416,
            this.xrTableCell417,
            this.xrTableCell418,
            this.xrTableCell419,
            this.xrTableCell420,
            this.xrTableCell421,
            this.xrTableCell422,
            this.xrTableCell423,
            this.xrTableCell424});
            this.xrTableRow31.Dpi = 100F;
            this.xrTableRow31.Name = "xrTableRow31";
            this.xrTableRow31.Weight = 11.5D;
            // 
            // xrTableCell411
            // 
            this.xrTableCell411.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)));
            this.xrTableCell411.Dpi = 100F;
            this.xrTableCell411.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell411.Name = "xrTableCell411";
            this.xrTableCell411.StylePriority.UseBorders = false;
            this.xrTableCell411.StylePriority.UseFont = false;
            this.xrTableCell411.Text = " Qty Produced/Delivered";
            this.xrTableCell411.Weight = 0.046132403429672113D;
            // 
            // xrTableCell412
            // 
            this.xrTableCell412.Borders = DevExpress.XtraPrinting.BorderSide.Top;
            this.xrTableCell412.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail2.TotalQuantity1", "{0:n}")});
            this.xrTableCell412.Dpi = 100F;
            this.xrTableCell412.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell412.Name = "xrTableCell412";
            this.xrTableCell412.StylePriority.UseBorders = false;
            this.xrTableCell412.StylePriority.UseFont = false;
            this.xrTableCell412.StylePriority.UseTextAlignment = false;
            this.xrTableCell412.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell412.Weight = 0.011456676506122643D;
            // 
            // xrTableCell413
            // 
            this.xrTableCell413.Borders = DevExpress.XtraPrinting.BorderSide.Top;
            this.xrTableCell413.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail2.TotalQuantity2", "{0:n}")});
            this.xrTableCell413.Dpi = 100F;
            this.xrTableCell413.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell413.Name = "xrTableCell413";
            this.xrTableCell413.StylePriority.UseBorders = false;
            this.xrTableCell413.StylePriority.UseFont = false;
            this.xrTableCell413.StylePriority.UseTextAlignment = false;
            this.xrTableCell413.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell413.Weight = 0.011456676506122643D;
            // 
            // xrTableCell414
            // 
            this.xrTableCell414.Borders = DevExpress.XtraPrinting.BorderSide.Top;
            this.xrTableCell414.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail2.TotalQuantity3", "{0:n}")});
            this.xrTableCell414.Dpi = 100F;
            this.xrTableCell414.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell414.Name = "xrTableCell414";
            this.xrTableCell414.StylePriority.UseBorders = false;
            this.xrTableCell414.StylePriority.UseFont = false;
            this.xrTableCell414.StylePriority.UseTextAlignment = false;
            this.xrTableCell414.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell414.Weight = 0.011456676506122643D;
            // 
            // xrTableCell415
            // 
            this.xrTableCell415.Borders = DevExpress.XtraPrinting.BorderSide.Top;
            this.xrTableCell415.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail2.TotalQuantity4", "{0:n}")});
            this.xrTableCell415.Dpi = 100F;
            this.xrTableCell415.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell415.Name = "xrTableCell415";
            this.xrTableCell415.StylePriority.UseBorders = false;
            this.xrTableCell415.StylePriority.UseFont = false;
            this.xrTableCell415.StylePriority.UseTextAlignment = false;
            this.xrTableCell415.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell415.Weight = 0.011456676506122643D;
            // 
            // xrTableCell416
            // 
            this.xrTableCell416.Borders = DevExpress.XtraPrinting.BorderSide.Top;
            this.xrTableCell416.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail2.TotalQuantity5", "{0:n}")});
            this.xrTableCell416.Dpi = 100F;
            this.xrTableCell416.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell416.Name = "xrTableCell416";
            this.xrTableCell416.StylePriority.UseBorders = false;
            this.xrTableCell416.StylePriority.UseFont = false;
            this.xrTableCell416.StylePriority.UseTextAlignment = false;
            this.xrTableCell416.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell416.Weight = 0.011456676506122643D;
            // 
            // xrTableCell417
            // 
            this.xrTableCell417.Borders = DevExpress.XtraPrinting.BorderSide.Top;
            this.xrTableCell417.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail2.TotalQuantity6", "{0:n}")});
            this.xrTableCell417.Dpi = 100F;
            this.xrTableCell417.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell417.Name = "xrTableCell417";
            this.xrTableCell417.StylePriority.UseBorders = false;
            this.xrTableCell417.StylePriority.UseFont = false;
            this.xrTableCell417.StylePriority.UseTextAlignment = false;
            this.xrTableCell417.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell417.Weight = 0.011456676506122643D;
            // 
            // xrTableCell418
            // 
            this.xrTableCell418.Borders = DevExpress.XtraPrinting.BorderSide.Top;
            this.xrTableCell418.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail2.TotalQuantity7", "{0:n}")});
            this.xrTableCell418.Dpi = 100F;
            this.xrTableCell418.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell418.Name = "xrTableCell418";
            this.xrTableCell418.StylePriority.UseBorders = false;
            this.xrTableCell418.StylePriority.UseFont = false;
            this.xrTableCell418.StylePriority.UseTextAlignment = false;
            this.xrTableCell418.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell418.Weight = 0.011456676506122643D;
            // 
            // xrTableCell419
            // 
            this.xrTableCell419.Borders = DevExpress.XtraPrinting.BorderSide.Top;
            this.xrTableCell419.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail2.TotalQuantity8", "{0:n}")});
            this.xrTableCell419.Dpi = 100F;
            this.xrTableCell419.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell419.Name = "xrTableCell419";
            this.xrTableCell419.StylePriority.UseBorders = false;
            this.xrTableCell419.StylePriority.UseFont = false;
            this.xrTableCell419.StylePriority.UseTextAlignment = false;
            this.xrTableCell419.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell419.Weight = 0.011456676506122643D;
            // 
            // xrTableCell420
            // 
            this.xrTableCell420.Borders = DevExpress.XtraPrinting.BorderSide.Top;
            this.xrTableCell420.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail2.TotalQuantity9", "{0:n}")});
            this.xrTableCell420.Dpi = 100F;
            this.xrTableCell420.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell420.Name = "xrTableCell420";
            this.xrTableCell420.StylePriority.UseBorders = false;
            this.xrTableCell420.StylePriority.UseFont = false;
            this.xrTableCell420.StylePriority.UseTextAlignment = false;
            this.xrTableCell420.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell420.Weight = 0.011456676506122643D;
            // 
            // xrTableCell421
            // 
            this.xrTableCell421.Borders = DevExpress.XtraPrinting.BorderSide.Top;
            this.xrTableCell421.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail2.TotalQuantity10", "{0:n}")});
            this.xrTableCell421.Dpi = 100F;
            this.xrTableCell421.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell421.Name = "xrTableCell421";
            this.xrTableCell421.StylePriority.UseBorders = false;
            this.xrTableCell421.StylePriority.UseFont = false;
            this.xrTableCell421.StylePriority.UseTextAlignment = false;
            this.xrTableCell421.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell421.Weight = 0.011456676506122643D;
            // 
            // xrTableCell422
            // 
            this.xrTableCell422.Borders = DevExpress.XtraPrinting.BorderSide.Top;
            this.xrTableCell422.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail2.TotalQuantity11", "{0:n}")});
            this.xrTableCell422.Dpi = 100F;
            this.xrTableCell422.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell422.Name = "xrTableCell422";
            this.xrTableCell422.StylePriority.UseBorders = false;
            this.xrTableCell422.StylePriority.UseFont = false;
            this.xrTableCell422.StylePriority.UseTextAlignment = false;
            this.xrTableCell422.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell422.Weight = 0.011456676506122643D;
            // 
            // xrTableCell423
            // 
            this.xrTableCell423.Borders = DevExpress.XtraPrinting.BorderSide.Top;
            this.xrTableCell423.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail2.TotalQuantity12", "{0:n}")});
            this.xrTableCell423.Dpi = 100F;
            this.xrTableCell423.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell423.Name = "xrTableCell423";
            this.xrTableCell423.StylePriority.UseBorders = false;
            this.xrTableCell423.StylePriority.UseFont = false;
            this.xrTableCell423.StylePriority.UseTextAlignment = false;
            this.xrTableCell423.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell423.Weight = 0.011456676506122643D;
            // 
            // xrTableCell424
            // 
            this.xrTableCell424.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)));
            this.xrTableCell424.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail2.TotalQuantity", "{0:n}")});
            this.xrTableCell424.Dpi = 100F;
            this.xrTableCell424.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell424.Name = "xrTableCell424";
            this.xrTableCell424.StylePriority.UseBorders = false;
            this.xrTableCell424.StylePriority.UseFont = false;
            this.xrTableCell424.StylePriority.UseTextAlignment = false;
            this.xrTableCell424.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell424.Weight = 0.011456676506122643D;
            // 
            // xrTableRow32
            // 
            this.xrTableRow32.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell425,
            this.xrTableCell426,
            this.xrTableCell427,
            this.xrTableCell428,
            this.xrTableCell429,
            this.xrTableCell430,
            this.xrTableCell431,
            this.xrTableCell432,
            this.xrTableCell433,
            this.xrTableCell434,
            this.xrTableCell435,
            this.xrTableCell436,
            this.xrTableCell437,
            this.xrTableCell438});
            this.xrTableRow32.Dpi = 100F;
            this.xrTableRow32.Name = "xrTableRow32";
            this.xrTableRow32.Weight = 11.5D;
            // 
            // xrTableCell425
            // 
            this.xrTableCell425.Borders = DevExpress.XtraPrinting.BorderSide.Left;
            this.xrTableCell425.Dpi = 100F;
            this.xrTableCell425.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell425.Name = "xrTableCell425";
            this.xrTableCell425.StylePriority.UseBorders = false;
            this.xrTableCell425.StylePriority.UseFont = false;
            this.xrTableCell425.Text = " Sales In Units";
            this.xrTableCell425.Weight = 0.046132403429672113D;
            // 
            // xrTableCell426
            // 
            this.xrTableCell426.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTableCell426.Dpi = 100F;
            this.xrTableCell426.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell426.Name = "xrTableCell426";
            this.xrTableCell426.StylePriority.UseBorders = false;
            this.xrTableCell426.StylePriority.UseFont = false;
            this.xrTableCell426.StylePriority.UseTextAlignment = false;
            this.xrTableCell426.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell426.Weight = 0.011456676506122643D;
            // 
            // xrTableCell427
            // 
            this.xrTableCell427.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTableCell427.Dpi = 100F;
            this.xrTableCell427.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell427.Name = "xrTableCell427";
            this.xrTableCell427.StylePriority.UseBorders = false;
            this.xrTableCell427.StylePriority.UseFont = false;
            this.xrTableCell427.StylePriority.UseTextAlignment = false;
            this.xrTableCell427.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell427.Weight = 0.011456676506122643D;
            // 
            // xrTableCell428
            // 
            this.xrTableCell428.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTableCell428.Dpi = 100F;
            this.xrTableCell428.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell428.Name = "xrTableCell428";
            this.xrTableCell428.StylePriority.UseBorders = false;
            this.xrTableCell428.StylePriority.UseFont = false;
            this.xrTableCell428.StylePriority.UseTextAlignment = false;
            this.xrTableCell428.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell428.Weight = 0.011456676506122643D;
            // 
            // xrTableCell429
            // 
            this.xrTableCell429.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTableCell429.Dpi = 100F;
            this.xrTableCell429.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell429.Name = "xrTableCell429";
            this.xrTableCell429.StylePriority.UseBorders = false;
            this.xrTableCell429.StylePriority.UseFont = false;
            this.xrTableCell429.StylePriority.UseTextAlignment = false;
            this.xrTableCell429.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell429.Weight = 0.011456676506122643D;
            // 
            // xrTableCell430
            // 
            this.xrTableCell430.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTableCell430.Dpi = 100F;
            this.xrTableCell430.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell430.Name = "xrTableCell430";
            this.xrTableCell430.StylePriority.UseBorders = false;
            this.xrTableCell430.StylePriority.UseFont = false;
            this.xrTableCell430.StylePriority.UseTextAlignment = false;
            this.xrTableCell430.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell430.Weight = 0.011456676506122643D;
            // 
            // xrTableCell431
            // 
            this.xrTableCell431.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTableCell431.Dpi = 100F;
            this.xrTableCell431.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell431.Name = "xrTableCell431";
            this.xrTableCell431.StylePriority.UseBorders = false;
            this.xrTableCell431.StylePriority.UseFont = false;
            this.xrTableCell431.StylePriority.UseTextAlignment = false;
            this.xrTableCell431.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell431.Weight = 0.011456676506122643D;
            // 
            // xrTableCell432
            // 
            this.xrTableCell432.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTableCell432.Dpi = 100F;
            this.xrTableCell432.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell432.Name = "xrTableCell432";
            this.xrTableCell432.StylePriority.UseBorders = false;
            this.xrTableCell432.StylePriority.UseFont = false;
            this.xrTableCell432.StylePriority.UseTextAlignment = false;
            this.xrTableCell432.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell432.Weight = 0.011456676506122643D;
            // 
            // xrTableCell433
            // 
            this.xrTableCell433.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTableCell433.Dpi = 100F;
            this.xrTableCell433.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell433.Name = "xrTableCell433";
            this.xrTableCell433.StylePriority.UseBorders = false;
            this.xrTableCell433.StylePriority.UseFont = false;
            this.xrTableCell433.StylePriority.UseTextAlignment = false;
            this.xrTableCell433.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell433.Weight = 0.011456676506122643D;
            // 
            // xrTableCell434
            // 
            this.xrTableCell434.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTableCell434.Dpi = 100F;
            this.xrTableCell434.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell434.Name = "xrTableCell434";
            this.xrTableCell434.StylePriority.UseBorders = false;
            this.xrTableCell434.StylePriority.UseFont = false;
            this.xrTableCell434.StylePriority.UseTextAlignment = false;
            this.xrTableCell434.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell434.Weight = 0.011456676506122643D;
            // 
            // xrTableCell435
            // 
            this.xrTableCell435.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTableCell435.Dpi = 100F;
            this.xrTableCell435.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell435.Name = "xrTableCell435";
            this.xrTableCell435.StylePriority.UseBorders = false;
            this.xrTableCell435.StylePriority.UseFont = false;
            this.xrTableCell435.StylePriority.UseTextAlignment = false;
            this.xrTableCell435.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell435.Weight = 0.011456676506122643D;
            // 
            // xrTableCell436
            // 
            this.xrTableCell436.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTableCell436.Dpi = 100F;
            this.xrTableCell436.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell436.Name = "xrTableCell436";
            this.xrTableCell436.StylePriority.UseBorders = false;
            this.xrTableCell436.StylePriority.UseFont = false;
            this.xrTableCell436.StylePriority.UseTextAlignment = false;
            this.xrTableCell436.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell436.Weight = 0.011456676506122643D;
            // 
            // xrTableCell437
            // 
            this.xrTableCell437.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTableCell437.Dpi = 100F;
            this.xrTableCell437.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell437.Name = "xrTableCell437";
            this.xrTableCell437.StylePriority.UseBorders = false;
            this.xrTableCell437.StylePriority.UseFont = false;
            this.xrTableCell437.StylePriority.UseTextAlignment = false;
            this.xrTableCell437.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell437.Weight = 0.011456676506122643D;
            // 
            // xrTableCell438
            // 
            this.xrTableCell438.Borders = DevExpress.XtraPrinting.BorderSide.Right;
            this.xrTableCell438.Dpi = 100F;
            this.xrTableCell438.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell438.Name = "xrTableCell438";
            this.xrTableCell438.StylePriority.UseBorders = false;
            this.xrTableCell438.StylePriority.UseFont = false;
            this.xrTableCell438.StylePriority.UseTextAlignment = false;
            this.xrTableCell438.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell438.Weight = 0.011456676506122643D;
            // 
            // xrTableRow33
            // 
            this.xrTableRow33.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell439,
            this.xrTableCell440,
            this.xrTableCell441,
            this.xrTableCell442,
            this.xrTableCell443,
            this.xrTableCell444,
            this.xrTableCell445,
            this.xrTableCell446,
            this.xrTableCell447,
            this.xrTableCell448,
            this.xrTableCell449,
            this.xrTableCell450,
            this.xrTableCell451,
            this.xrTableCell452});
            this.xrTableRow33.Dpi = 100F;
            this.xrTableRow33.Name = "xrTableRow33";
            this.xrTableRow33.Weight = 11.5D;
            // 
            // xrTableCell439
            // 
            this.xrTableCell439.Borders = DevExpress.XtraPrinting.BorderSide.Left;
            this.xrTableCell439.Dpi = 100F;
            this.xrTableCell439.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell439.Name = "xrTableCell439";
            this.xrTableCell439.StylePriority.UseBorders = false;
            this.xrTableCell439.StylePriority.UseFont = false;
            this.xrTableCell439.Text = "    Breakeven Point";
            this.xrTableCell439.Weight = 0.046132403429672113D;
            // 
            // xrTableCell440
            // 
            this.xrTableCell440.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTableCell440.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail2.BreakevenTotal1", "{0:n}")});
            this.xrTableCell440.Dpi = 100F;
            this.xrTableCell440.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell440.Name = "xrTableCell440";
            this.xrTableCell440.StylePriority.UseBorders = false;
            this.xrTableCell440.StylePriority.UseFont = false;
            this.xrTableCell440.StylePriority.UseTextAlignment = false;
            this.xrTableCell440.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell440.Weight = 0.011456676506122643D;
            // 
            // xrTableCell441
            // 
            this.xrTableCell441.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTableCell441.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail2.BreakevenTotal2", "{0:n}")});
            this.xrTableCell441.Dpi = 100F;
            this.xrTableCell441.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell441.Name = "xrTableCell441";
            this.xrTableCell441.StylePriority.UseBorders = false;
            this.xrTableCell441.StylePriority.UseFont = false;
            this.xrTableCell441.StylePriority.UseTextAlignment = false;
            this.xrTableCell441.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell441.Weight = 0.011456676506122643D;
            // 
            // xrTableCell442
            // 
            this.xrTableCell442.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTableCell442.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail2.BreakevenTotal3", "{0:n}")});
            this.xrTableCell442.Dpi = 100F;
            this.xrTableCell442.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell442.Name = "xrTableCell442";
            this.xrTableCell442.StylePriority.UseBorders = false;
            this.xrTableCell442.StylePriority.UseFont = false;
            this.xrTableCell442.StylePriority.UseTextAlignment = false;
            this.xrTableCell442.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell442.Weight = 0.011456676506122643D;
            // 
            // xrTableCell443
            // 
            this.xrTableCell443.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTableCell443.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail2.BreakevenTotal4", "{0:n}")});
            this.xrTableCell443.Dpi = 100F;
            this.xrTableCell443.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell443.Name = "xrTableCell443";
            this.xrTableCell443.StylePriority.UseBorders = false;
            this.xrTableCell443.StylePriority.UseFont = false;
            this.xrTableCell443.StylePriority.UseTextAlignment = false;
            this.xrTableCell443.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell443.Weight = 0.011456676506122643D;
            // 
            // xrTableCell444
            // 
            this.xrTableCell444.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTableCell444.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail2.BreakevenTotal5", "{0:n}")});
            this.xrTableCell444.Dpi = 100F;
            this.xrTableCell444.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell444.Name = "xrTableCell444";
            this.xrTableCell444.StylePriority.UseBorders = false;
            this.xrTableCell444.StylePriority.UseFont = false;
            this.xrTableCell444.StylePriority.UseTextAlignment = false;
            this.xrTableCell444.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell444.Weight = 0.011456676506122643D;
            // 
            // xrTableCell445
            // 
            this.xrTableCell445.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTableCell445.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail2.BreakevenTotal6", "{0:n}")});
            this.xrTableCell445.Dpi = 100F;
            this.xrTableCell445.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell445.Name = "xrTableCell445";
            this.xrTableCell445.StylePriority.UseBorders = false;
            this.xrTableCell445.StylePriority.UseFont = false;
            this.xrTableCell445.StylePriority.UseTextAlignment = false;
            this.xrTableCell445.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell445.Weight = 0.011456676506122643D;
            // 
            // xrTableCell446
            // 
            this.xrTableCell446.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTableCell446.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail2.BreakevenTotal7", "{0:n}")});
            this.xrTableCell446.Dpi = 100F;
            this.xrTableCell446.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell446.Name = "xrTableCell446";
            this.xrTableCell446.StylePriority.UseBorders = false;
            this.xrTableCell446.StylePriority.UseFont = false;
            this.xrTableCell446.StylePriority.UseTextAlignment = false;
            this.xrTableCell446.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell446.Weight = 0.011456676506122643D;
            // 
            // xrTableCell447
            // 
            this.xrTableCell447.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTableCell447.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail2.BreakevenTotal8", "{0:n}")});
            this.xrTableCell447.Dpi = 100F;
            this.xrTableCell447.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell447.Name = "xrTableCell447";
            this.xrTableCell447.StylePriority.UseBorders = false;
            this.xrTableCell447.StylePriority.UseFont = false;
            this.xrTableCell447.StylePriority.UseTextAlignment = false;
            this.xrTableCell447.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell447.Weight = 0.011456676506122643D;
            // 
            // xrTableCell448
            // 
            this.xrTableCell448.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTableCell448.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail2.BreakevenTotal9", "{0:n}")});
            this.xrTableCell448.Dpi = 100F;
            this.xrTableCell448.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell448.Name = "xrTableCell448";
            this.xrTableCell448.StylePriority.UseBorders = false;
            this.xrTableCell448.StylePriority.UseFont = false;
            this.xrTableCell448.StylePriority.UseTextAlignment = false;
            this.xrTableCell448.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell448.Weight = 0.011456676506122643D;
            // 
            // xrTableCell449
            // 
            this.xrTableCell449.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTableCell449.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail2.BreakevenTotal10", "{0:n}")});
            this.xrTableCell449.Dpi = 100F;
            this.xrTableCell449.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell449.Name = "xrTableCell449";
            this.xrTableCell449.StylePriority.UseBorders = false;
            this.xrTableCell449.StylePriority.UseFont = false;
            this.xrTableCell449.StylePriority.UseTextAlignment = false;
            this.xrTableCell449.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell449.Weight = 0.011456676506122643D;
            // 
            // xrTableCell450
            // 
            this.xrTableCell450.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTableCell450.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail2.BreakevenTotal11", "{0:n}")});
            this.xrTableCell450.Dpi = 100F;
            this.xrTableCell450.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell450.Name = "xrTableCell450";
            this.xrTableCell450.StylePriority.UseBorders = false;
            this.xrTableCell450.StylePriority.UseFont = false;
            this.xrTableCell450.StylePriority.UseTextAlignment = false;
            this.xrTableCell450.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell450.Weight = 0.011456676506122643D;
            // 
            // xrTableCell451
            // 
            this.xrTableCell451.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTableCell451.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail2.BreakevenTotal12", "{0:n}")});
            this.xrTableCell451.Dpi = 100F;
            this.xrTableCell451.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell451.Name = "xrTableCell451";
            this.xrTableCell451.StylePriority.UseBorders = false;
            this.xrTableCell451.StylePriority.UseFont = false;
            this.xrTableCell451.StylePriority.UseTextAlignment = false;
            this.xrTableCell451.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell451.Weight = 0.011456676506122643D;
            // 
            // xrTableCell452
            // 
            this.xrTableCell452.Borders = DevExpress.XtraPrinting.BorderSide.Right;
            this.xrTableCell452.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail2.BreakevenTotal", "{0:n}")});
            this.xrTableCell452.Dpi = 100F;
            this.xrTableCell452.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell452.Name = "xrTableCell452";
            this.xrTableCell452.StylePriority.UseBorders = false;
            this.xrTableCell452.StylePriority.UseFont = false;
            this.xrTableCell452.StylePriority.UseTextAlignment = false;
            this.xrTableCell452.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell452.Weight = 0.011456676506122643D;
            // 
            // xrTableRow34
            // 
            this.xrTableRow34.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell453,
            this.xrTableCell454,
            this.xrTableCell455,
            this.xrTableCell456,
            this.xrTableCell457,
            this.xrTableCell458,
            this.xrTableCell459,
            this.xrTableCell460,
            this.xrTableCell461,
            this.xrTableCell462,
            this.xrTableCell463,
            this.xrTableCell464,
            this.xrTableCell465,
            this.xrTableCell466});
            this.xrTableRow34.Dpi = 100F;
            this.xrTableRow34.Name = "xrTableRow34";
            this.xrTableRow34.Weight = 11.5D;
            // 
            // xrTableCell453
            // 
            this.xrTableCell453.Borders = DevExpress.XtraPrinting.BorderSide.Left;
            this.xrTableCell453.Dpi = 100F;
            this.xrTableCell453.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell453.Name = "xrTableCell453";
            this.xrTableCell453.StylePriority.UseBorders = false;
            this.xrTableCell453.StylePriority.UseFont = false;
            this.xrTableCell453.Text = "   Target Proft @               8%";
            this.xrTableCell453.Weight = 0.046132403429672113D;
            // 
            // xrTableCell454
            // 
            this.xrTableCell454.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTableCell454.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail2.TotalProfitPer1", "{0:n}")});
            this.xrTableCell454.Dpi = 100F;
            this.xrTableCell454.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell454.Name = "xrTableCell454";
            this.xrTableCell454.StylePriority.UseBorders = false;
            this.xrTableCell454.StylePriority.UseFont = false;
            this.xrTableCell454.StylePriority.UseTextAlignment = false;
            this.xrTableCell454.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell454.Weight = 0.011456676506122643D;
            // 
            // xrTableCell455
            // 
            this.xrTableCell455.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTableCell455.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail2.TotalProfitPer2", "{0:n}")});
            this.xrTableCell455.Dpi = 100F;
            this.xrTableCell455.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell455.Name = "xrTableCell455";
            this.xrTableCell455.StylePriority.UseBorders = false;
            this.xrTableCell455.StylePriority.UseFont = false;
            this.xrTableCell455.StylePriority.UseTextAlignment = false;
            this.xrTableCell455.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell455.Weight = 0.011456676506122643D;
            // 
            // xrTableCell456
            // 
            this.xrTableCell456.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTableCell456.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail2.TotalProfitPer3", "{0:n}")});
            this.xrTableCell456.Dpi = 100F;
            this.xrTableCell456.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell456.Name = "xrTableCell456";
            this.xrTableCell456.StylePriority.UseBorders = false;
            this.xrTableCell456.StylePriority.UseFont = false;
            this.xrTableCell456.StylePriority.UseTextAlignment = false;
            this.xrTableCell456.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell456.Weight = 0.011456676506122643D;
            // 
            // xrTableCell457
            // 
            this.xrTableCell457.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTableCell457.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail2.TotalProfitPer4", "{0:n}")});
            this.xrTableCell457.Dpi = 100F;
            this.xrTableCell457.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell457.Name = "xrTableCell457";
            this.xrTableCell457.StylePriority.UseBorders = false;
            this.xrTableCell457.StylePriority.UseFont = false;
            this.xrTableCell457.StylePriority.UseTextAlignment = false;
            this.xrTableCell457.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell457.Weight = 0.011456676506122643D;
            // 
            // xrTableCell458
            // 
            this.xrTableCell458.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTableCell458.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail2.TotalProfitPer5", "{0:n}")});
            this.xrTableCell458.Dpi = 100F;
            this.xrTableCell458.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell458.Name = "xrTableCell458";
            this.xrTableCell458.StylePriority.UseBorders = false;
            this.xrTableCell458.StylePriority.UseFont = false;
            this.xrTableCell458.StylePriority.UseTextAlignment = false;
            this.xrTableCell458.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell458.Weight = 0.011456676506122643D;
            // 
            // xrTableCell459
            // 
            this.xrTableCell459.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTableCell459.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail2.TotalProfitPer6", "{0:n}")});
            this.xrTableCell459.Dpi = 100F;
            this.xrTableCell459.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell459.Name = "xrTableCell459";
            this.xrTableCell459.StylePriority.UseBorders = false;
            this.xrTableCell459.StylePriority.UseFont = false;
            this.xrTableCell459.StylePriority.UseTextAlignment = false;
            this.xrTableCell459.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell459.Weight = 0.011456676506122643D;
            // 
            // xrTableCell460
            // 
            this.xrTableCell460.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTableCell460.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail2.TotalProfitPer7", "{0:n}")});
            this.xrTableCell460.Dpi = 100F;
            this.xrTableCell460.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell460.Name = "xrTableCell460";
            this.xrTableCell460.StylePriority.UseBorders = false;
            this.xrTableCell460.StylePriority.UseFont = false;
            this.xrTableCell460.StylePriority.UseTextAlignment = false;
            this.xrTableCell460.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell460.Weight = 0.011456676506122643D;
            // 
            // xrTableCell461
            // 
            this.xrTableCell461.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTableCell461.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail2.TotalProfitPer8", "{0:n}")});
            this.xrTableCell461.Dpi = 100F;
            this.xrTableCell461.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell461.Name = "xrTableCell461";
            this.xrTableCell461.StylePriority.UseBorders = false;
            this.xrTableCell461.StylePriority.UseFont = false;
            this.xrTableCell461.StylePriority.UseTextAlignment = false;
            this.xrTableCell461.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell461.Weight = 0.011456676506122643D;
            // 
            // xrTableCell462
            // 
            this.xrTableCell462.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTableCell462.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail2.TotalProfitPer9", "{0:n}")});
            this.xrTableCell462.Dpi = 100F;
            this.xrTableCell462.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell462.Name = "xrTableCell462";
            this.xrTableCell462.StylePriority.UseBorders = false;
            this.xrTableCell462.StylePriority.UseFont = false;
            this.xrTableCell462.StylePriority.UseTextAlignment = false;
            this.xrTableCell462.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell462.Weight = 0.011456676506122643D;
            // 
            // xrTableCell463
            // 
            this.xrTableCell463.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTableCell463.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail2.TotalProfitPer10", "{0:n}")});
            this.xrTableCell463.Dpi = 100F;
            this.xrTableCell463.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell463.Name = "xrTableCell463";
            this.xrTableCell463.StylePriority.UseBorders = false;
            this.xrTableCell463.StylePriority.UseFont = false;
            this.xrTableCell463.StylePriority.UseTextAlignment = false;
            this.xrTableCell463.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell463.Weight = 0.011456676506122643D;
            // 
            // xrTableCell464
            // 
            this.xrTableCell464.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTableCell464.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail2.TotalProfitPer11", "{0:n}")});
            this.xrTableCell464.Dpi = 100F;
            this.xrTableCell464.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell464.Name = "xrTableCell464";
            this.xrTableCell464.StylePriority.UseBorders = false;
            this.xrTableCell464.StylePriority.UseFont = false;
            this.xrTableCell464.StylePriority.UseTextAlignment = false;
            this.xrTableCell464.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell464.Weight = 0.011456676506122643D;
            // 
            // xrTableCell465
            // 
            this.xrTableCell465.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTableCell465.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail2.TotalProfitPer12", "{0:n}")});
            this.xrTableCell465.Dpi = 100F;
            this.xrTableCell465.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell465.Name = "xrTableCell465";
            this.xrTableCell465.StylePriority.UseBorders = false;
            this.xrTableCell465.StylePriority.UseFont = false;
            this.xrTableCell465.StylePriority.UseTextAlignment = false;
            this.xrTableCell465.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell465.Weight = 0.011456676506122643D;
            // 
            // xrTableCell466
            // 
            this.xrTableCell466.Borders = DevExpress.XtraPrinting.BorderSide.Right;
            this.xrTableCell466.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail2.TotalProfitPer", "{0:n}")});
            this.xrTableCell466.Dpi = 100F;
            this.xrTableCell466.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell466.Name = "xrTableCell466";
            this.xrTableCell466.StylePriority.UseBorders = false;
            this.xrTableCell466.StylePriority.UseFont = false;
            this.xrTableCell466.StylePriority.UseTextAlignment = false;
            this.xrTableCell466.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell466.Weight = 0.011456676506122643D;
            // 
            // xrTableRow35
            // 
            this.xrTableRow35.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell467,
            this.xrTableCell468,
            this.xrTableCell469,
            this.xrTableCell470,
            this.xrTableCell471,
            this.xrTableCell472,
            this.xrTableCell473,
            this.xrTableCell474,
            this.xrTableCell475,
            this.xrTableCell476,
            this.xrTableCell477,
            this.xrTableCell478,
            this.xrTableCell479,
            this.xrTableCell480});
            this.xrTableRow35.Dpi = 100F;
            this.xrTableRow35.Name = "xrTableRow35";
            this.xrTableRow35.Weight = 11.5D;
            // 
            // xrTableCell467
            // 
            this.xrTableCell467.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell467.Dpi = 100F;
            this.xrTableCell467.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell467.Name = "xrTableCell467";
            this.xrTableCell467.StylePriority.UseBorders = false;
            this.xrTableCell467.StylePriority.UseFont = false;
            this.xrTableCell467.Text = "   Target Profit @             15%";
            this.xrTableCell467.Weight = 0.046132403429672113D;
            // 
            // xrTableCell468
            // 
            this.xrTableCell468.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrTableCell468.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail2.TotalProfitPerH1", "{0:n}")});
            this.xrTableCell468.Dpi = 100F;
            this.xrTableCell468.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell468.Name = "xrTableCell468";
            this.xrTableCell468.StylePriority.UseBorders = false;
            this.xrTableCell468.StylePriority.UseFont = false;
            this.xrTableCell468.StylePriority.UseTextAlignment = false;
            this.xrTableCell468.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell468.Weight = 0.011456676506122643D;
            // 
            // xrTableCell469
            // 
            this.xrTableCell469.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrTableCell469.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail2.TotalProfitPerH2", "{0:n}")});
            this.xrTableCell469.Dpi = 100F;
            this.xrTableCell469.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell469.Name = "xrTableCell469";
            this.xrTableCell469.StylePriority.UseBorders = false;
            this.xrTableCell469.StylePriority.UseFont = false;
            this.xrTableCell469.StylePriority.UseTextAlignment = false;
            this.xrTableCell469.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell469.Weight = 0.011456676506122643D;
            // 
            // xrTableCell470
            // 
            this.xrTableCell470.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrTableCell470.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail2.TotalProfitPerH3", "{0:n}")});
            this.xrTableCell470.Dpi = 100F;
            this.xrTableCell470.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell470.Name = "xrTableCell470";
            this.xrTableCell470.StylePriority.UseBorders = false;
            this.xrTableCell470.StylePriority.UseFont = false;
            this.xrTableCell470.StylePriority.UseTextAlignment = false;
            this.xrTableCell470.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell470.Weight = 0.011456676506122643D;
            // 
            // xrTableCell471
            // 
            this.xrTableCell471.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrTableCell471.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail2.TotalProfitPerH4", "{0:n}")});
            this.xrTableCell471.Dpi = 100F;
            this.xrTableCell471.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell471.Name = "xrTableCell471";
            this.xrTableCell471.StylePriority.UseBorders = false;
            this.xrTableCell471.StylePriority.UseFont = false;
            this.xrTableCell471.StylePriority.UseTextAlignment = false;
            this.xrTableCell471.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell471.Weight = 0.011456676506122643D;
            // 
            // xrTableCell472
            // 
            this.xrTableCell472.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrTableCell472.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail2.TotalProfitPerH5", "{0:n}")});
            this.xrTableCell472.Dpi = 100F;
            this.xrTableCell472.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell472.Name = "xrTableCell472";
            this.xrTableCell472.StylePriority.UseBorders = false;
            this.xrTableCell472.StylePriority.UseFont = false;
            this.xrTableCell472.StylePriority.UseTextAlignment = false;
            this.xrTableCell472.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell472.Weight = 0.011456676506122643D;
            // 
            // xrTableCell473
            // 
            this.xrTableCell473.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrTableCell473.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail2.TotalProfitPerH6", "{0:n}")});
            this.xrTableCell473.Dpi = 100F;
            this.xrTableCell473.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell473.Name = "xrTableCell473";
            this.xrTableCell473.StylePriority.UseBorders = false;
            this.xrTableCell473.StylePriority.UseFont = false;
            this.xrTableCell473.StylePriority.UseTextAlignment = false;
            this.xrTableCell473.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell473.Weight = 0.011456676506122643D;
            // 
            // xrTableCell474
            // 
            this.xrTableCell474.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrTableCell474.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail2.TotalProfitPerH7", "{0:n}")});
            this.xrTableCell474.Dpi = 100F;
            this.xrTableCell474.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell474.Name = "xrTableCell474";
            this.xrTableCell474.StylePriority.UseBorders = false;
            this.xrTableCell474.StylePriority.UseFont = false;
            this.xrTableCell474.StylePriority.UseTextAlignment = false;
            this.xrTableCell474.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell474.Weight = 0.011456676506122643D;
            // 
            // xrTableCell475
            // 
            this.xrTableCell475.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrTableCell475.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail2.TotalProfitPerH8", "{0:n}")});
            this.xrTableCell475.Dpi = 100F;
            this.xrTableCell475.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell475.Name = "xrTableCell475";
            this.xrTableCell475.StylePriority.UseBorders = false;
            this.xrTableCell475.StylePriority.UseFont = false;
            this.xrTableCell475.StylePriority.UseTextAlignment = false;
            this.xrTableCell475.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell475.Weight = 0.011456676506122643D;
            // 
            // xrTableCell476
            // 
            this.xrTableCell476.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrTableCell476.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail2.TotalProfitPerH9", "{0:n}")});
            this.xrTableCell476.Dpi = 100F;
            this.xrTableCell476.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell476.Name = "xrTableCell476";
            this.xrTableCell476.StylePriority.UseBorders = false;
            this.xrTableCell476.StylePriority.UseFont = false;
            this.xrTableCell476.StylePriority.UseTextAlignment = false;
            this.xrTableCell476.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell476.Weight = 0.011456676506122643D;
            // 
            // xrTableCell477
            // 
            this.xrTableCell477.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrTableCell477.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail2.TotalProfitPerH10", "{0:n}")});
            this.xrTableCell477.Dpi = 100F;
            this.xrTableCell477.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell477.Name = "xrTableCell477";
            this.xrTableCell477.StylePriority.UseBorders = false;
            this.xrTableCell477.StylePriority.UseFont = false;
            this.xrTableCell477.StylePriority.UseTextAlignment = false;
            this.xrTableCell477.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell477.Weight = 0.011456676506122643D;
            // 
            // xrTableCell478
            // 
            this.xrTableCell478.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrTableCell478.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail2.TotalProfitPerH11", "{0:n}")});
            this.xrTableCell478.Dpi = 100F;
            this.xrTableCell478.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell478.Name = "xrTableCell478";
            this.xrTableCell478.StylePriority.UseBorders = false;
            this.xrTableCell478.StylePriority.UseFont = false;
            this.xrTableCell478.StylePriority.UseTextAlignment = false;
            this.xrTableCell478.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell478.Weight = 0.011456676506122643D;
            // 
            // xrTableCell479
            // 
            this.xrTableCell479.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
            this.xrTableCell479.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail2.TotalProfitPerH12", "{0:n}")});
            this.xrTableCell479.Dpi = 100F;
            this.xrTableCell479.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell479.Name = "xrTableCell479";
            this.xrTableCell479.StylePriority.UseBorders = false;
            this.xrTableCell479.StylePriority.UseFont = false;
            this.xrTableCell479.StylePriority.UseTextAlignment = false;
            this.xrTableCell479.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell479.Weight = 0.011456676506122643D;
            // 
            // xrTableCell480
            // 
            this.xrTableCell480.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell480.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail2.TotalProfitPerH", "{0:n}")});
            this.xrTableCell480.Dpi = 100F;
            this.xrTableCell480.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell480.Name = "xrTableCell480";
            this.xrTableCell480.StylePriority.UseBorders = false;
            this.xrTableCell480.StylePriority.UseFont = false;
            this.xrTableCell480.StylePriority.UseTextAlignment = false;
            this.xrTableCell480.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell480.Weight = 0.011456676506122643D;
            // 
            // xrTableRow36
            // 
            this.xrTableRow36.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell481,
            this.xrTableCell482,
            this.xrTableCell483,
            this.xrTableCell484,
            this.xrTableCell485,
            this.xrTableCell486,
            this.xrTableCell487,
            this.xrTableCell488,
            this.xrTableCell489,
            this.xrTableCell490,
            this.xrTableCell491,
            this.xrTableCell492,
            this.xrTableCell493,
            this.xrTableCell494});
            this.xrTableRow36.Dpi = 100F;
            this.xrTableRow36.Name = "xrTableRow36";
            this.xrTableRow36.Weight = 11.5D;
            // 
            // xrTableCell481
            // 
            this.xrTableCell481.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTableCell481.Dpi = 100F;
            this.xrTableCell481.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell481.Name = "xrTableCell481";
            this.xrTableCell481.StylePriority.UseBorders = false;
            this.xrTableCell481.StylePriority.UseFont = false;
            this.xrTableCell481.Weight = 0.046132403429672113D;
            // 
            // xrTableCell482
            // 
            this.xrTableCell482.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTableCell482.Dpi = 100F;
            this.xrTableCell482.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell482.Name = "xrTableCell482";
            this.xrTableCell482.StylePriority.UseBorders = false;
            this.xrTableCell482.StylePriority.UseFont = false;
            this.xrTableCell482.StylePriority.UseTextAlignment = false;
            this.xrTableCell482.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell482.Weight = 0.011456676506122643D;
            // 
            // xrTableCell483
            // 
            this.xrTableCell483.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTableCell483.Dpi = 100F;
            this.xrTableCell483.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell483.Name = "xrTableCell483";
            this.xrTableCell483.StylePriority.UseBorders = false;
            this.xrTableCell483.StylePriority.UseFont = false;
            this.xrTableCell483.StylePriority.UseTextAlignment = false;
            this.xrTableCell483.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell483.Weight = 0.011456676506122643D;
            // 
            // xrTableCell484
            // 
            this.xrTableCell484.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTableCell484.Dpi = 100F;
            this.xrTableCell484.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell484.Name = "xrTableCell484";
            this.xrTableCell484.StylePriority.UseBorders = false;
            this.xrTableCell484.StylePriority.UseFont = false;
            this.xrTableCell484.StylePriority.UseTextAlignment = false;
            this.xrTableCell484.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell484.Weight = 0.011456676506122643D;
            // 
            // xrTableCell485
            // 
            this.xrTableCell485.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTableCell485.Dpi = 100F;
            this.xrTableCell485.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell485.Name = "xrTableCell485";
            this.xrTableCell485.StylePriority.UseBorders = false;
            this.xrTableCell485.StylePriority.UseFont = false;
            this.xrTableCell485.StylePriority.UseTextAlignment = false;
            this.xrTableCell485.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell485.Weight = 0.011456676506122643D;
            // 
            // xrTableCell486
            // 
            this.xrTableCell486.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTableCell486.Dpi = 100F;
            this.xrTableCell486.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell486.Name = "xrTableCell486";
            this.xrTableCell486.StylePriority.UseBorders = false;
            this.xrTableCell486.StylePriority.UseFont = false;
            this.xrTableCell486.StylePriority.UseTextAlignment = false;
            this.xrTableCell486.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell486.Weight = 0.011456676506122643D;
            // 
            // xrTableCell487
            // 
            this.xrTableCell487.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTableCell487.Dpi = 100F;
            this.xrTableCell487.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell487.Name = "xrTableCell487";
            this.xrTableCell487.StylePriority.UseBorders = false;
            this.xrTableCell487.StylePriority.UseFont = false;
            this.xrTableCell487.StylePriority.UseTextAlignment = false;
            this.xrTableCell487.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell487.Weight = 0.011456676506122643D;
            // 
            // xrTableCell488
            // 
            this.xrTableCell488.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTableCell488.Dpi = 100F;
            this.xrTableCell488.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell488.Name = "xrTableCell488";
            this.xrTableCell488.StylePriority.UseBorders = false;
            this.xrTableCell488.StylePriority.UseFont = false;
            this.xrTableCell488.StylePriority.UseTextAlignment = false;
            this.xrTableCell488.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell488.Weight = 0.011456676506122643D;
            // 
            // xrTableCell489
            // 
            this.xrTableCell489.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTableCell489.Dpi = 100F;
            this.xrTableCell489.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell489.Name = "xrTableCell489";
            this.xrTableCell489.StylePriority.UseBorders = false;
            this.xrTableCell489.StylePriority.UseFont = false;
            this.xrTableCell489.StylePriority.UseTextAlignment = false;
            this.xrTableCell489.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell489.Weight = 0.011456676506122643D;
            // 
            // xrTableCell490
            // 
            this.xrTableCell490.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTableCell490.Dpi = 100F;
            this.xrTableCell490.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell490.Name = "xrTableCell490";
            this.xrTableCell490.StylePriority.UseBorders = false;
            this.xrTableCell490.StylePriority.UseFont = false;
            this.xrTableCell490.StylePriority.UseTextAlignment = false;
            this.xrTableCell490.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell490.Weight = 0.011456676506122643D;
            // 
            // xrTableCell491
            // 
            this.xrTableCell491.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTableCell491.Dpi = 100F;
            this.xrTableCell491.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell491.Name = "xrTableCell491";
            this.xrTableCell491.StylePriority.UseBorders = false;
            this.xrTableCell491.StylePriority.UseFont = false;
            this.xrTableCell491.StylePriority.UseTextAlignment = false;
            this.xrTableCell491.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell491.Weight = 0.011456676506122643D;
            // 
            // xrTableCell492
            // 
            this.xrTableCell492.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTableCell492.Dpi = 100F;
            this.xrTableCell492.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell492.Name = "xrTableCell492";
            this.xrTableCell492.StylePriority.UseBorders = false;
            this.xrTableCell492.StylePriority.UseFont = false;
            this.xrTableCell492.StylePriority.UseTextAlignment = false;
            this.xrTableCell492.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell492.Weight = 0.011456676506122643D;
            // 
            // xrTableCell493
            // 
            this.xrTableCell493.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTableCell493.Dpi = 100F;
            this.xrTableCell493.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell493.Name = "xrTableCell493";
            this.xrTableCell493.StylePriority.UseBorders = false;
            this.xrTableCell493.StylePriority.UseFont = false;
            this.xrTableCell493.StylePriority.UseTextAlignment = false;
            this.xrTableCell493.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell493.Weight = 0.011456676506122643D;
            // 
            // xrTableCell494
            // 
            this.xrTableCell494.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTableCell494.Dpi = 100F;
            this.xrTableCell494.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell494.Name = "xrTableCell494";
            this.xrTableCell494.StylePriority.UseBorders = false;
            this.xrTableCell494.StylePriority.UseFont = false;
            this.xrTableCell494.StylePriority.UseTextAlignment = false;
            this.xrTableCell494.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell494.Weight = 0.011456676506122643D;
            // 
            // xrTableRow37
            // 
            this.xrTableRow37.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell495,
            this.xrTableCell496,
            this.xrTableCell497,
            this.xrTableCell498,
            this.xrTableCell499,
            this.xrTableCell500,
            this.xrTableCell501,
            this.xrTableCell502,
            this.xrTableCell503,
            this.xrTableCell504,
            this.xrTableCell505,
            this.xrTableCell506,
            this.xrTableCell507,
            this.xrTableCell508});
            this.xrTableRow37.Dpi = 100F;
            this.xrTableRow37.Name = "xrTableRow37";
            this.xrTableRow37.Weight = 11.5D;
            // 
            // xrTableCell495
            // 
            this.xrTableCell495.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell495.Dpi = 100F;
            this.xrTableCell495.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell495.Name = "xrTableCell495";
            this.xrTableCell495.StylePriority.UseBorders = false;
            this.xrTableCell495.StylePriority.UseFont = false;
            this.xrTableCell495.Text = " Factor *";
            this.xrTableCell495.Weight = 0.046132403429672113D;
            // 
            // xrTableCell496
            // 
            this.xrTableCell496.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell496.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail1.Factor1", "{0:n}")});
            this.xrTableCell496.Dpi = 100F;
            this.xrTableCell496.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell496.Name = "xrTableCell496";
            this.xrTableCell496.StylePriority.UseBorders = false;
            this.xrTableCell496.StylePriority.UseFont = false;
            this.xrTableCell496.StylePriority.UseTextAlignment = false;
            this.xrTableCell496.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell496.Weight = 0.011456676506122643D;
            // 
            // xrTableCell497
            // 
            this.xrTableCell497.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell497.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail1.Factor2", "{0:n}")});
            this.xrTableCell497.Dpi = 100F;
            this.xrTableCell497.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell497.Name = "xrTableCell497";
            this.xrTableCell497.StylePriority.UseBorders = false;
            this.xrTableCell497.StylePriority.UseFont = false;
            this.xrTableCell497.StylePriority.UseTextAlignment = false;
            this.xrTableCell497.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell497.Weight = 0.011456676506122643D;
            // 
            // xrTableCell498
            // 
            this.xrTableCell498.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell498.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail1.Factor3", "{0:n}")});
            this.xrTableCell498.Dpi = 100F;
            this.xrTableCell498.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell498.Name = "xrTableCell498";
            this.xrTableCell498.StylePriority.UseBorders = false;
            this.xrTableCell498.StylePriority.UseFont = false;
            this.xrTableCell498.StylePriority.UseTextAlignment = false;
            this.xrTableCell498.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell498.Weight = 0.011456676506122643D;
            // 
            // xrTableCell499
            // 
            this.xrTableCell499.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell499.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail1.Factor4", "{0:n}")});
            this.xrTableCell499.Dpi = 100F;
            this.xrTableCell499.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell499.Name = "xrTableCell499";
            this.xrTableCell499.StylePriority.UseBorders = false;
            this.xrTableCell499.StylePriority.UseFont = false;
            this.xrTableCell499.StylePriority.UseTextAlignment = false;
            this.xrTableCell499.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell499.Weight = 0.011456676506122643D;
            // 
            // xrTableCell500
            // 
            this.xrTableCell500.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell500.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail1.Factor5", "{0:n}")});
            this.xrTableCell500.Dpi = 100F;
            this.xrTableCell500.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell500.Name = "xrTableCell500";
            this.xrTableCell500.StylePriority.UseBorders = false;
            this.xrTableCell500.StylePriority.UseFont = false;
            this.xrTableCell500.StylePriority.UseTextAlignment = false;
            this.xrTableCell500.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell500.Weight = 0.011456676506122643D;
            // 
            // xrTableCell501
            // 
            this.xrTableCell501.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell501.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail1.Factor6", "{0:n}")});
            this.xrTableCell501.Dpi = 100F;
            this.xrTableCell501.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell501.Name = "xrTableCell501";
            this.xrTableCell501.StylePriority.UseBorders = false;
            this.xrTableCell501.StylePriority.UseFont = false;
            this.xrTableCell501.StylePriority.UseTextAlignment = false;
            this.xrTableCell501.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell501.Weight = 0.011456676506122643D;
            // 
            // xrTableCell502
            // 
            this.xrTableCell502.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell502.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail1.Factor7", "{0:n}")});
            this.xrTableCell502.Dpi = 100F;
            this.xrTableCell502.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell502.Name = "xrTableCell502";
            this.xrTableCell502.StylePriority.UseBorders = false;
            this.xrTableCell502.StylePriority.UseFont = false;
            this.xrTableCell502.StylePriority.UseTextAlignment = false;
            this.xrTableCell502.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell502.Weight = 0.011456676506122643D;
            // 
            // xrTableCell503
            // 
            this.xrTableCell503.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell503.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail1.Factor8", "{0:n}")});
            this.xrTableCell503.Dpi = 100F;
            this.xrTableCell503.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell503.Name = "xrTableCell503";
            this.xrTableCell503.StylePriority.UseBorders = false;
            this.xrTableCell503.StylePriority.UseFont = false;
            this.xrTableCell503.StylePriority.UseTextAlignment = false;
            this.xrTableCell503.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell503.Weight = 0.011456676506122643D;
            // 
            // xrTableCell504
            // 
            this.xrTableCell504.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell504.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail1.Factor9", "{0:n}")});
            this.xrTableCell504.Dpi = 100F;
            this.xrTableCell504.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell504.Name = "xrTableCell504";
            this.xrTableCell504.StylePriority.UseBorders = false;
            this.xrTableCell504.StylePriority.UseFont = false;
            this.xrTableCell504.StylePriority.UseTextAlignment = false;
            this.xrTableCell504.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell504.Weight = 0.011456676506122643D;
            // 
            // xrTableCell505
            // 
            this.xrTableCell505.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell505.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail1.Factor10", "{0:n}")});
            this.xrTableCell505.Dpi = 100F;
            this.xrTableCell505.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell505.Name = "xrTableCell505";
            this.xrTableCell505.StylePriority.UseBorders = false;
            this.xrTableCell505.StylePriority.UseFont = false;
            this.xrTableCell505.StylePriority.UseTextAlignment = false;
            this.xrTableCell505.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell505.Weight = 0.011456676506122643D;
            // 
            // xrTableCell506
            // 
            this.xrTableCell506.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell506.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail1.Factor11", "{0:n}")});
            this.xrTableCell506.Dpi = 100F;
            this.xrTableCell506.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell506.Name = "xrTableCell506";
            this.xrTableCell506.StylePriority.UseBorders = false;
            this.xrTableCell506.StylePriority.UseFont = false;
            this.xrTableCell506.StylePriority.UseTextAlignment = false;
            this.xrTableCell506.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell506.Weight = 0.011456676506122643D;
            // 
            // xrTableCell507
            // 
            this.xrTableCell507.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell507.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail1.Factor12", "{0:n}")});
            this.xrTableCell507.Dpi = 100F;
            this.xrTableCell507.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell507.Name = "xrTableCell507";
            this.xrTableCell507.StylePriority.UseBorders = false;
            this.xrTableCell507.StylePriority.UseFont = false;
            this.xrTableCell507.StylePriority.UseTextAlignment = false;
            this.xrTableCell507.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell507.Weight = 0.011456676506122643D;
            // 
            // xrTableCell508
            // 
            this.xrTableCell508.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell508.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail1.Factor", "{0:n}")});
            this.xrTableCell508.Dpi = 100F;
            this.xrTableCell508.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell508.Name = "xrTableCell508";
            this.xrTableCell508.StylePriority.UseBorders = false;
            this.xrTableCell508.StylePriority.UseFont = false;
            this.xrTableCell508.StylePriority.UseTextAlignment = false;
            this.xrTableCell508.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell508.Weight = 0.011456676506122643D;
            // 
            // xrTableRow38
            // 
            this.xrTableRow38.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell509,
            this.xrTableCell510,
            this.xrTableCell511,
            this.xrTableCell512,
            this.xrTableCell513,
            this.xrTableCell514,
            this.xrTableCell515,
            this.xrTableCell516,
            this.xrTableCell517,
            this.xrTableCell518,
            this.xrTableCell519,
            this.xrTableCell520,
            this.xrTableCell521,
            this.xrTableCell522});
            this.xrTableRow38.Dpi = 100F;
            this.xrTableRow38.Name = "xrTableRow38";
            this.xrTableRow38.Weight = 11.5D;
            // 
            // xrTableCell509
            // 
            this.xrTableCell509.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTableCell509.Dpi = 100F;
            this.xrTableCell509.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell509.Name = "xrTableCell509";
            this.xrTableCell509.StylePriority.UseBorders = false;
            this.xrTableCell509.StylePriority.UseFont = false;
            this.xrTableCell509.Weight = 0.046132403429672113D;
            // 
            // xrTableCell510
            // 
            this.xrTableCell510.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTableCell510.Dpi = 100F;
            this.xrTableCell510.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell510.Name = "xrTableCell510";
            this.xrTableCell510.StylePriority.UseBorders = false;
            this.xrTableCell510.StylePriority.UseFont = false;
            this.xrTableCell510.StylePriority.UseTextAlignment = false;
            this.xrTableCell510.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell510.Weight = 0.011456676506122643D;
            // 
            // xrTableCell511
            // 
            this.xrTableCell511.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTableCell511.Dpi = 100F;
            this.xrTableCell511.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell511.Name = "xrTableCell511";
            this.xrTableCell511.StylePriority.UseBorders = false;
            this.xrTableCell511.StylePriority.UseFont = false;
            this.xrTableCell511.StylePriority.UseTextAlignment = false;
            this.xrTableCell511.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell511.Weight = 0.011456676506122643D;
            // 
            // xrTableCell512
            // 
            this.xrTableCell512.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTableCell512.Dpi = 100F;
            this.xrTableCell512.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell512.Name = "xrTableCell512";
            this.xrTableCell512.StylePriority.UseBorders = false;
            this.xrTableCell512.StylePriority.UseFont = false;
            this.xrTableCell512.StylePriority.UseTextAlignment = false;
            this.xrTableCell512.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell512.Weight = 0.011456676506122643D;
            // 
            // xrTableCell513
            // 
            this.xrTableCell513.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTableCell513.Dpi = 100F;
            this.xrTableCell513.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell513.Name = "xrTableCell513";
            this.xrTableCell513.StylePriority.UseBorders = false;
            this.xrTableCell513.StylePriority.UseFont = false;
            this.xrTableCell513.StylePriority.UseTextAlignment = false;
            this.xrTableCell513.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell513.Weight = 0.011456676506122643D;
            // 
            // xrTableCell514
            // 
            this.xrTableCell514.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTableCell514.Dpi = 100F;
            this.xrTableCell514.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell514.Name = "xrTableCell514";
            this.xrTableCell514.StylePriority.UseBorders = false;
            this.xrTableCell514.StylePriority.UseFont = false;
            this.xrTableCell514.StylePriority.UseTextAlignment = false;
            this.xrTableCell514.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell514.Weight = 0.011456676506122643D;
            // 
            // xrTableCell515
            // 
            this.xrTableCell515.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTableCell515.Dpi = 100F;
            this.xrTableCell515.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell515.Name = "xrTableCell515";
            this.xrTableCell515.StylePriority.UseBorders = false;
            this.xrTableCell515.StylePriority.UseFont = false;
            this.xrTableCell515.StylePriority.UseTextAlignment = false;
            this.xrTableCell515.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell515.Weight = 0.011456676506122643D;
            // 
            // xrTableCell516
            // 
            this.xrTableCell516.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTableCell516.Dpi = 100F;
            this.xrTableCell516.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell516.Name = "xrTableCell516";
            this.xrTableCell516.StylePriority.UseBorders = false;
            this.xrTableCell516.StylePriority.UseFont = false;
            this.xrTableCell516.StylePriority.UseTextAlignment = false;
            this.xrTableCell516.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell516.Weight = 0.011456676506122643D;
            // 
            // xrTableCell517
            // 
            this.xrTableCell517.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTableCell517.Dpi = 100F;
            this.xrTableCell517.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell517.Name = "xrTableCell517";
            this.xrTableCell517.StylePriority.UseBorders = false;
            this.xrTableCell517.StylePriority.UseFont = false;
            this.xrTableCell517.StylePriority.UseTextAlignment = false;
            this.xrTableCell517.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell517.Weight = 0.011456676506122643D;
            // 
            // xrTableCell518
            // 
            this.xrTableCell518.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTableCell518.Dpi = 100F;
            this.xrTableCell518.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell518.Name = "xrTableCell518";
            this.xrTableCell518.StylePriority.UseBorders = false;
            this.xrTableCell518.StylePriority.UseFont = false;
            this.xrTableCell518.StylePriority.UseTextAlignment = false;
            this.xrTableCell518.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell518.Weight = 0.011456676506122643D;
            // 
            // xrTableCell519
            // 
            this.xrTableCell519.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTableCell519.Dpi = 100F;
            this.xrTableCell519.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell519.Name = "xrTableCell519";
            this.xrTableCell519.StylePriority.UseBorders = false;
            this.xrTableCell519.StylePriority.UseFont = false;
            this.xrTableCell519.StylePriority.UseTextAlignment = false;
            this.xrTableCell519.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell519.Weight = 0.011456676506122643D;
            // 
            // xrTableCell520
            // 
            this.xrTableCell520.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTableCell520.Dpi = 100F;
            this.xrTableCell520.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell520.Name = "xrTableCell520";
            this.xrTableCell520.StylePriority.UseBorders = false;
            this.xrTableCell520.StylePriority.UseFont = false;
            this.xrTableCell520.StylePriority.UseTextAlignment = false;
            this.xrTableCell520.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell520.Weight = 0.011456676506122643D;
            // 
            // xrTableCell521
            // 
            this.xrTableCell521.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTableCell521.Dpi = 100F;
            this.xrTableCell521.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell521.Name = "xrTableCell521";
            this.xrTableCell521.StylePriority.UseBorders = false;
            this.xrTableCell521.StylePriority.UseFont = false;
            this.xrTableCell521.StylePriority.UseTextAlignment = false;
            this.xrTableCell521.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell521.Weight = 0.011456676506122643D;
            // 
            // xrTableCell522
            // 
            this.xrTableCell522.Borders = DevExpress.XtraPrinting.BorderSide.None;
            this.xrTableCell522.Dpi = 100F;
            this.xrTableCell522.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell522.Name = "xrTableCell522";
            this.xrTableCell522.StylePriority.UseBorders = false;
            this.xrTableCell522.StylePriority.UseFont = false;
            this.xrTableCell522.StylePriority.UseTextAlignment = false;
            this.xrTableCell522.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell522.Weight = 0.011456676506122643D;
            // 
            // xrTableRow39
            // 
            this.xrTableRow39.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell523,
            this.xrTableCell524,
            this.xrTableCell525,
            this.xrTableCell526,
            this.xrTableCell527,
            this.xrTableCell528,
            this.xrTableCell529,
            this.xrTableCell530,
            this.xrTableCell531,
            this.xrTableCell532,
            this.xrTableCell533,
            this.xrTableCell534,
            this.xrTableCell535,
            this.xrTableCell536});
            this.xrTableRow39.Dpi = 100F;
            this.xrTableRow39.Name = "xrTableRow39";
            this.xrTableRow39.Weight = 11.5D;
            // 
            // xrTableCell523
            // 
            this.xrTableCell523.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell523.Dpi = 100F;
            this.xrTableCell523.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell523.Name = "xrTableCell523";
            this.xrTableCell523.StylePriority.UseBorders = false;
            this.xrTableCell523.StylePriority.UseFont = false;
            this.xrTableCell523.Text = " Margin of Safety";
            this.xrTableCell523.Weight = 0.046132403429672113D;
            // 
            // xrTableCell524
            // 
            this.xrTableCell524.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell524.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail2.MarginSafetyTota1", "{0:0.00%}")});
            this.xrTableCell524.Dpi = 100F;
            this.xrTableCell524.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell524.Name = "xrTableCell524";
            this.xrTableCell524.StylePriority.UseBorders = false;
            this.xrTableCell524.StylePriority.UseFont = false;
            this.xrTableCell524.StylePriority.UseTextAlignment = false;
            this.xrTableCell524.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell524.Weight = 0.011456676506122643D;
            // 
            // xrTableCell525
            // 
            this.xrTableCell525.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell525.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail2.MarginSafetyTota2", "{0:0.00%}")});
            this.xrTableCell525.Dpi = 100F;
            this.xrTableCell525.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell525.Name = "xrTableCell525";
            this.xrTableCell525.StylePriority.UseBorders = false;
            this.xrTableCell525.StylePriority.UseFont = false;
            this.xrTableCell525.StylePriority.UseTextAlignment = false;
            this.xrTableCell525.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell525.Weight = 0.011456676506122643D;
            // 
            // xrTableCell526
            // 
            this.xrTableCell526.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell526.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail2.MarginSafetyTotal3", "{0:0.00%}")});
            this.xrTableCell526.Dpi = 100F;
            this.xrTableCell526.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell526.Name = "xrTableCell526";
            this.xrTableCell526.StylePriority.UseBorders = false;
            this.xrTableCell526.StylePriority.UseFont = false;
            this.xrTableCell526.StylePriority.UseTextAlignment = false;
            this.xrTableCell526.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell526.Weight = 0.011456676506122643D;
            // 
            // xrTableCell527
            // 
            this.xrTableCell527.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell527.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail2.MarginSafetyTotal4", "{0:0.00%}")});
            this.xrTableCell527.Dpi = 100F;
            this.xrTableCell527.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell527.Name = "xrTableCell527";
            this.xrTableCell527.StylePriority.UseBorders = false;
            this.xrTableCell527.StylePriority.UseFont = false;
            this.xrTableCell527.StylePriority.UseTextAlignment = false;
            this.xrTableCell527.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell527.Weight = 0.011456676506122643D;
            // 
            // xrTableCell528
            // 
            this.xrTableCell528.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell528.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail2.MarginSafetyTotal5", "{0:0.00%}")});
            this.xrTableCell528.Dpi = 100F;
            this.xrTableCell528.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell528.Name = "xrTableCell528";
            this.xrTableCell528.StylePriority.UseBorders = false;
            this.xrTableCell528.StylePriority.UseFont = false;
            this.xrTableCell528.StylePriority.UseTextAlignment = false;
            this.xrTableCell528.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell528.Weight = 0.011456676506122643D;
            // 
            // xrTableCell529
            // 
            this.xrTableCell529.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell529.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail2.MarginSafetyTotal6", "{0:0.00%}")});
            this.xrTableCell529.Dpi = 100F;
            this.xrTableCell529.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell529.Name = "xrTableCell529";
            this.xrTableCell529.StylePriority.UseBorders = false;
            this.xrTableCell529.StylePriority.UseFont = false;
            this.xrTableCell529.StylePriority.UseTextAlignment = false;
            this.xrTableCell529.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell529.Weight = 0.011456676506122643D;
            // 
            // xrTableCell530
            // 
            this.xrTableCell530.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell530.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail2.MarginSafetyTotal7", "{0:0.00%}")});
            this.xrTableCell530.Dpi = 100F;
            this.xrTableCell530.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell530.Name = "xrTableCell530";
            this.xrTableCell530.StylePriority.UseBorders = false;
            this.xrTableCell530.StylePriority.UseFont = false;
            this.xrTableCell530.StylePriority.UseTextAlignment = false;
            this.xrTableCell530.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell530.Weight = 0.011456676506122643D;
            // 
            // xrTableCell531
            // 
            this.xrTableCell531.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell531.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail2.MarginSafetyTotal8", "{0:0.00%}")});
            this.xrTableCell531.Dpi = 100F;
            this.xrTableCell531.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell531.Name = "xrTableCell531";
            this.xrTableCell531.StylePriority.UseBorders = false;
            this.xrTableCell531.StylePriority.UseFont = false;
            this.xrTableCell531.StylePriority.UseTextAlignment = false;
            this.xrTableCell531.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell531.Weight = 0.011456676506122643D;
            // 
            // xrTableCell532
            // 
            this.xrTableCell532.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell532.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail2.MarginSafetyTotal9", "{0:0.00%}")});
            this.xrTableCell532.Dpi = 100F;
            this.xrTableCell532.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell532.Name = "xrTableCell532";
            this.xrTableCell532.StylePriority.UseBorders = false;
            this.xrTableCell532.StylePriority.UseFont = false;
            this.xrTableCell532.StylePriority.UseTextAlignment = false;
            this.xrTableCell532.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell532.Weight = 0.011456676506122643D;
            // 
            // xrTableCell533
            // 
            this.xrTableCell533.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell533.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail2.MarginSafetyTotal10", "{0:0.00%}")});
            this.xrTableCell533.Dpi = 100F;
            this.xrTableCell533.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell533.Name = "xrTableCell533";
            this.xrTableCell533.StylePriority.UseBorders = false;
            this.xrTableCell533.StylePriority.UseFont = false;
            this.xrTableCell533.StylePriority.UseTextAlignment = false;
            this.xrTableCell533.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell533.Weight = 0.011456676506122643D;
            // 
            // xrTableCell534
            // 
            this.xrTableCell534.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell534.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail2.MarginSafetyTotal11", "{0:0.00%}")});
            this.xrTableCell534.Dpi = 100F;
            this.xrTableCell534.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell534.Name = "xrTableCell534";
            this.xrTableCell534.StylePriority.UseBorders = false;
            this.xrTableCell534.StylePriority.UseFont = false;
            this.xrTableCell534.StylePriority.UseTextAlignment = false;
            this.xrTableCell534.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell534.Weight = 0.011456676506122643D;
            // 
            // xrTableCell535
            // 
            this.xrTableCell535.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell535.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail2.MarginSafetyTotal12", "{0:0.00%}")});
            this.xrTableCell535.Dpi = 100F;
            this.xrTableCell535.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell535.Name = "xrTableCell535";
            this.xrTableCell535.StylePriority.UseBorders = false;
            this.xrTableCell535.StylePriority.UseFont = false;
            this.xrTableCell535.StylePriority.UseTextAlignment = false;
            this.xrTableCell535.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell535.Weight = 0.011456676506122643D;
            // 
            // xrTableCell536
            // 
            this.xrTableCell536.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCell536.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "sp_report_ContributionMarginDetail2.MarginSafetyTotal", "{0:0.00%}")});
            this.xrTableCell536.Dpi = 100F;
            this.xrTableCell536.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrTableCell536.Name = "xrTableCell536";
            this.xrTableCell536.StylePriority.UseBorders = false;
            this.xrTableCell536.StylePriority.UseFont = false;
            this.xrTableCell536.StylePriority.UseTextAlignment = false;
            this.xrTableCell536.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrTableCell536.Weight = 0.011456676506122643D;
            // 
            // GroupFooter1
            // 
            this.GroupFooter1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel10,
            this.xrLabel3,
            this.xrTable14});
            this.GroupFooter1.Dpi = 100F;
            this.GroupFooter1.HeightF = 298.5417F;
            this.GroupFooter1.Name = "GroupFooter1";
            // 
            // xrLabel10
            // 
            this.xrLabel10.Dpi = 100F;
            this.xrLabel10.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrLabel10.LocationFloat = new DevExpress.Utils.PointFloat(0.002861023F, 283.5417F);
            this.xrLabel10.Name = "xrLabel10";
            this.xrLabel10.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel10.SizeF = new System.Drawing.SizeF(331.8086F, 15F);
            this.xrLabel10.StylePriority.UseFont = false;
            this.xrLabel10.StylePriority.UseTextAlignment = false;
            this.xrLabel10.Text = "** Margin of Safety = (Sales-Breakevenpoint) / Sales";
            this.xrLabel10.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLabel3
            // 
            this.xrLabel3.Dpi = 100F;
            this.xrLabel3.Font = new System.Drawing.Font("Arial", 7.5F, System.Drawing.FontStyle.Bold);
            this.xrLabel3.LocationFloat = new DevExpress.Utils.PointFloat(0.002861023F, 264.7917F);
            this.xrLabel3.Name = "xrLabel3";
            this.xrLabel3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel3.SizeF = new System.Drawing.SizeF(331.8086F, 15F);
            this.xrLabel3.StylePriority.UseFont = false;
            this.xrLabel3.StylePriority.UseTextAlignment = false;
            this.xrLabel3.Text = "* Factor = ((Sales per Unit / Variable Cost per Unit)*100)+100";
            this.xrLabel3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // R_ContributionMarginISDetail
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.ReportHeader,
            this.DetailReport,
            this.DetailReport1,
            this.PageHeader,
            this.ReportFooter1,
            this.GroupFooter1});
            this.CalculatedFields.AddRange(new DevExpress.XtraReports.UI.CalculatedField[] {
            this.calculatedField1,
            this.calculatedField2,
            this.calculatedField3,
            this.calculatedField4,
            this.calculatedField5});
            this.ComponentStorage.AddRange(new System.ComponentModel.IComponent[] {
            this.sqlDataSource1});
            this.DataSource = this.sqlDataSource1;
            this.FormattingRuleSheet.AddRange(new DevExpress.XtraReports.UI.FormattingRule[] {
            this.formattingRule1});
            this.Landscape = true;
            this.Margins = new System.Drawing.Printing.Margins(100, 100, 0, 33);
            this.PageHeight = 850;
            this.PageWidth = 1603;
            this.PaperKind = System.Drawing.Printing.PaperKind.Custom;
            this.Parameters.AddRange(new DevExpress.XtraReports.Parameters.Parameter[] {
            this.Year});
            this.Scripts.OnBeforePrint = "R_AnnualIncomeStatement_BeforePrint";
            this.ScriptsSource = "\r\nprivate void R_AnnualIncomeStatement_BeforePrint(object sender, System.Drawing." +
    "Printing.PrintEventArgs e) {\r\n\r\n}\r\n";
            this.Version = "15.2";
            this.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.R_AnnualIncomeStatement_BeforePrint);
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable20)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable16)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable15)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable21)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable13)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable17)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable22)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable23)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable18)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable19)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable12)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable14)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private void R_AnnualIncomeStatement_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

            xrTable14.KeepTogether = true;
           
        }

        private void Detail5_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
        
        }

    


   
     


    }

}