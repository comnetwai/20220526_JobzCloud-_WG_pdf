using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using MySql.Data.MySqlClient;
using System.Data;
using GrapeCity.ActiveReports.SectionReportModel;
using System.IO;
using System.Linq;
using System.Xml;
using GrapeCity.ActiveReports;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Service;
using Ghostscript.NET.Rasterizer;
using System.Drawing.Imaging;
using GrapeCity.ActiveReports.Drawing;
using System.Drawing.Text;

namespace jobzcolud.pdf
{
    /// <summary>
    /// SectionReport2 の概要の説明です。
    /// </summary>
    public partial class mitsumori : GrapeCity.ActiveReports.SectionReport
    {
        public string clickedKyoten { get; set; }  //20220209 MyatNoe Added (kyoten preview)

        MySqlConnection con = null;
        string Database = String.Empty;

        double nMITUMORISYOHIZE = 0;    //消费税
        double nKINNGAKUKAZEI = 0;

        // DataTable dt_syousai = new DataTable();
        DataTable dt_m_m = new DataTable();
        DataTable dt_m = new DataTable();
        int counter = 0;
        int FIRSTMAXROWS = 28;
        int MAXROWS = 39;
        int IMAGEROWS = 9;

        public bool fHYOUSHI = false;
        public bool fMEISAI = false;
        public bool fSYOUSAI = false;//  詳細 checkbox
        public bool fMIDASHI = false; //  見出し checkbox
        public bool fHidzuke = false;

        public string HANKO_Check = "欄有り(担当印有り)"; //判子欄 combobox
        //public string HANKO_Check = "欄無し";
        public bool fZEINUKIKINNGAKU = false;
        public bool fZEIFUKUMUKIKINNGAKU = false;

        public string tokui_align = "中央";//左寄せ    //得意先 combobox
        public string busyo_align = "中央";//中央          //部署 combobox
        public string tantou_align = "";               //担当者 combobox
        public string frogoimage = "";//1,2,3,4,5     //ロゴ combobox
        public string cKyoten = "";

        public int rmt_pcount1 = 0;
        public int pcount = 0;
        public Boolean flag_page1 = false;
        public int nPAGECOUNT = 0;
        public int nPAGECOUNT_1 = 0;
        public int fINS = 0;
        public bool fNEWIMAGE = false;
        public Boolean fICHIRAN = false;
        public Boolean fHYOUSI = false;
        public Boolean fRYOUHOU = false;
        public Boolean header_flag = false;

        public string cMITUMORI = "";
        public string cBUKKEN = "";
        public string cTANTOUSHA = "";
        public string loginId = "";
        public string sTITLE = "";  //20220209 MyatNoe Added (kyoten preview)
        public string logourl = ""; //20220209 MyatNoe Added (kyoten preview)
        public byte[] logoByte; //20220209 MyatNoe Added (kyoten preview)

        DataTable Syousai_Temp1 = new DataTable();
        DataTable Syousai_Temp2 = new DataTable();
        DataTable Syousai_Temp3 = new DataTable();
        DataTable Syousai_All = new DataTable();

        public int Count_Midashi = 1;

        private System.Drawing.Font f1 = new System.Drawing.Font("ＭＳ 明朝", float.Parse("10"));
        private System.Drawing.Font f2 = new System.Drawing.Font("HG明朝B", float.Parse("12"));
        private System.Drawing.Font f3 = new System.Drawing.Font("HG明朝B", float.Parse("10"));
        private System.Drawing.Font f4 = new System.Drawing.Font("ＭＳ 明朝", float.Parse("12"));

        private System.Drawing.Font f6 = new System.Drawing.Font("ＭＳ 明朝", float.Parse("9"));
        private System.Drawing.Font f7 = new System.Drawing.Font("ＭＳ 明朝", float.Parse("9.75"));
        private System.Drawing.Font f8 = new System.Drawing.Font("ＭＳ 明朝", float.Parse("10.5"));

        PrivateFontCollection collection1 = new PrivateFontCollection();
        public mitsumori()
        {
            //
            // デザイナー サポートに必要なメソッドです。
            //
            InitializeComponent();
            string Font_folderPath = System.Web.Hosting.HostingEnvironment.MapPath("~/fonts/");
            collection1.AddFontFile(Font_folderPath+"HGRMB.TTC");
            Font f_3 = new Font(collection1.Families.Last(), float.Parse("10"));
            Font f_2 = new Font(collection1.Families.Last(), float.Parse("12"));
            f2 = f_2;
            f3 = f_3;

            if (f3.Name != "HG明朝B")
            {
                f3 = new System.Drawing.Font("ＭＳ 明朝", float.Parse("10"));
                //f3 = new System.Drawing.Font("游明朝 Demibold", float.Parse("10"));
            }
            if (f2.Name != "HG明朝B")
            {
                f2 = new System.Drawing.Font("ＭＳ 明朝", float.Parse("12"));
                //f2 = new System.Drawing.Font("游明朝 Demibold", float.Parse("12"));
            }

        }

        #region mitsumori_ReportStart
        private void mitsumori_ReportStart(object sender, EventArgs e)
        {
            string s = clickedKyoten; //20220125 MyatNoe Added(kyoten preview)
            if (s != "kyoten")
            {
                #region ＭＳ 明朝

                LB_NO.Font = f8;
            LB_S_sNAIYOU.Font = f4;
            LB_nSURYO.Font = f8;
            LB_sTANI.Font = f8;
            LB_nSIKIRITANKA.Font = f8;
            LB_S_nKINGAKU1.Font = f8;

                #endregion

                //this.Document.Printer.PrinterName = "";
                //this.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.A4;
                //this.PageSettings.Orientation = GrapeCity.ActiveReports.Document.PageOrientation.Portrait;

                //this.PageSettings.Margins.Top = 0.6F;
                //this.PageSettings.Margins.Bottom = 0.5F;
                //reportFooter1.Height = float.Parse("0");
                #region 
                //reportfooter picture start
                //picture1.Visible = false;
                //picture2.Visible = false;
                //reportfooter picture end

                //pageheader label start
                label28.Visible = false;//見積No.
            LB_PAGE1.Visible = false;
            LB_PAGE_cMITUMORI.Visible = false;
            LB_PAGE_dMITUMORISAKUSEI.Visible = false;
            //pageheader label end

           
            string sql_m = ""; //用于查询“見積”表的sql文。
            string sql_m_m = ""; //用于查询“見積商品明細”表和“单位”表的sql文。

            string sql_m_ms = ""; //詳細データ取得する為に使う　

            string sql_m_s_info = "";//用于查询“書類基本情報”表的sql文。
            string sql_m_j_info = "";//自社情報マスター表

            sql_m_s_info += "SELECT ";
            sql_m_s_info += "sMITUMORI as sMITUMORI, ";
            sql_m_s_info += "sNAIYOU as sNAIYOU, ";
            sql_m_s_info += "sKEN as sKEN, ";
            sql_m_s_info += "sNOUKI as sNOUKI, ";
            sql_m_s_info += "sYUUKOU as sYUUKOU, ";
            sql_m_s_info += "sSHIHARAI as sSHIHARAI, ";
            sql_m_s_info += "sUKEBASYOU as sUKEBASYOU, ";
            sql_m_s_info += "IFNULL(fIMAGE,'1') AS fIMAGE ";
            sql_m_s_info += ",sAisatsu as sAisatsu";
            sql_m_s_info += ",sZeinu as sZeinu ";
            sql_m_s_info += "FROM M_S_INFO ";
            sql_m_s_info += "WHERE 1=1 ";
            sql_m_s_info += "AND cSYOU = '01' ";

            sql_m_j_info += " SELECT";
            sql_m_j_info += " IFNULL(cYUUBIN,'') AS cYUUBIN";   //郵便番号
            sql_m_j_info += ",ifnull(sCO,'') AS sCO";
            sql_m_j_info += ", IFNULL(sJUUSHO1,'') AS sJUUSHO1"; //住所１
            sql_m_j_info += ", IFNULL(sJUUSHO2,'') AS sJUUSHO2"; //住所２
            sql_m_j_info += ", IFNULL(sTEL,'') AS sTEL";     //電話番号
            sql_m_j_info += ", IFNULL(sFAX,'') AS sFAX";     //ファックス番号
            sql_m_j_info += ", IFNULL(sURL,'') AS sURL";     //ホームページURL
            sql_m_j_info += ", IFNULL(sMAIL,'') AS sMAIL";     //代表メールアドレス
            sql_m_j_info += ", IFNULL(sNAIYOU,'') AS sNAIYOU";
            sql_m_j_info += ", IFNULL(sSPACE,0) AS sSPACE";

            if (frogoimage == "1")
            {
                sql_m_j_info += ",sIMAGE1 as sIMAGE";
                sql_m_j_info += " ,ifnull(fIMAGESize1,'0') as fIMAGESize";
            }
            else if (frogoimage == "2")
            {
                sql_m_j_info += ",sIMAGE2 as sIMAGE";
                sql_m_j_info += " ,ifnull(fIMAGESize2,'0') as fIMAGESize";
            }
            else if (frogoimage == "3")
            {
                sql_m_j_info += ",sIMAGE3 as sIMAGE";
                sql_m_j_info += " ,ifnull(fIMAGESize3,'0') as fIMAGESize";
            }
            else if (frogoimage == "4")
            {
                sql_m_j_info += ",sIMAGE4 as sIMAGE";
                sql_m_j_info += " ,ifnull(fIMAGESize4,'0') as fIMAGESize";
            }
            else if (frogoimage == "5")
            {
                sql_m_j_info += ",sIMAGE5 as sIMAGE";
                sql_m_j_info += " ,ifnull(fIMAGESize5,'0') as fIMAGESize";
            }
            else
            {
                sql_m_j_info += " ,'' as sIMAGE";
                sql_m_j_info += " ,'0' as fIMAGESize";
            }

            String cCO = "01";
            if (!String.IsNullOrEmpty(cKyoten))
            {
                cCO = cKyoten;
            }

            sql_m_j_info += " FROM M_J_INFO";
            sql_m_j_info += " WHERE cco ='"+cCO+"'";  // + ckyoten.ToString() +
            
            JC_ClientConnecction_Class jc = new JC_ClientConnecction_Class();
            jc.loginId = loginId;//Session["LoginId"].ToString();
            con = jc.GetConnection();
            con.Open();

            MySqlCommand cmd_s_info = new MySqlCommand(sql_m_s_info, con);
            cmd_s_info.CommandTimeout = 0;
            DataTable dt_s_info = new DataTable();
            MySqlDataAdapter da_s_info = new MySqlDataAdapter(cmd_s_info);
            da_s_info.Fill(dt_s_info);

            MySqlCommand cmd_j_info = new MySqlCommand(sql_m_j_info, con);
            cmd_j_info.CommandTimeout = 0;
            DataTable dt_j_info = new DataTable();
            MySqlDataAdapter da_j_info = new MySqlDataAdapter(cmd_j_info);
            da_j_info.Fill(dt_j_info);
            
            con.Close();
            da_s_info.Dispose();
            da_j_info.Dispose();

            sql_m += "select distinct ";
            sql_m += "T.sTOKUISAKI as sTOKUISAKI, ";                 //得意先名
            sql_m += "T.cMITUMORI as cMITUMORI, ";
            sql_m += "(CASE WHEN (T.sMITUMORIKENMEI is not null AND T.sMITUMORIKENMEI<>'') THEN T.sMITUMORIKENMEI ELSE T.sMITUMORI END) as sMITUMORI1, ";  //件名 
            sql_m += "T.sMITUMORINOKI as dMITUMORINOKI, ";           //納期              
            sql_m += "T.sMITUMORIYUKOKIGEN as sMITUMORIYUKOKIGEN, "; //有効期限
            sql_m += "K.sSHIHARAI as sSHIHARAI, ";                   //支払条件
            sql_m += "T.sUKEWATASIBASYO as sUKEWATASIBASYO, ";       //受渡し場所   
            sql_m += "T.sBIKOU as sBIKOU, ";                         //備考       
            sql_m += "T.nMITUMORISYOHIZE as nMITUMORISYOHIZE, ";     //消費税 
            sql_m += "T.nTOKUISAKIKAKERITU as nTOKUISAKIKAKERITU, "; //得意先の掛率 
            sql_m += "T.nMITUMORINEBIKI as nMITUMORINEBIKI, ";       //値引き
            sql_m += "CASE WHEN T.fSAKUSEISYA='1' THEN S2.sTANTOUSHA ELSE S.sTANTOUSHA END as sTANTOUSHA ,"; //担当者名 
            sql_m += "T.sTOKUISAKI_TAN as sTOKUISAKI_TAN, ";          //得意先担当者
            sql_m += "T.nKIRI_G as nKIRI_G, ";                           //仕切合計
            sql_m += "T.nTANKA_G as nTANKA_G, ";                         //定価合計
            sql_m += "T.nKINGAKU as nKINGAKU, ";                         //提供金額の合計＝小計
            sql_m += "T.dMITUMORISAKUSEI AS dMITUMORISAKUSEI ";       //作成日
            sql_m += ", CASE WHEN T.fSAKUSEISYA='1' THEN S2.sMAIL ELSE S.sMAIL END AS sMAIL ";//メールアドレス
            sql_m += ",T.fSYUUKEI AS fSYUUKEI ";                     //複数案見積 
            sql_m += ",IFNULL(T.sTOKUISAKI_TANBUMON,'') as sTOKUISAKI_TANBUMON ";//得意先担当者部門　
            sql_m += ",T.fNO as fNO ";//行番号手入力チェック　
            sql_m += ",CASE WHEN MT.fSAMA='1' THEN '様' ELSE '御中' END as fSAMA ";//得意先様、御中フラグ
            sql_m += ",T.nKAZEIKINGAKU as nKAZEIKINGAKU ";
            sql_m += ",IFNULL(T.sTOKUISAKI_YAKUSHOKU,'') as sTOKUISAKI_YAKUSHOKU ";//得意先担当者役職　
            sql_m += ",IFNULL(T.sTOKUISAKI_KEISYO,'') as SKEISHOU ";
            sql_m += ",CASE WHEN T.fSAKUSEISYA='1' THEN S2.sIMAGE1 ELSE S.sIMAGE1 END AS sIMAGE1 ";
            sql_m += "from r_mitumori T ";                           //从“見積 "表中查询
            sql_m += "left join m_shiharai K on T.cSHIHARAI = K.cSHIHARAI ";
            sql_m += "left join m_j_tantousha S on T.cEIGYOTANTOSYA = S.cTANTOUSHA ";  //営業担当者CODE 和 担当者コード 连接
            sql_m += "left join m_j_tantousha S2 on T.cSAKUSEISYA = S2.cTANTOUSHA ";
            sql_m += "left join m_tokuisaki MT on T.cTOKUISAKI=MT.cTOKUISAKI ";  //得意先マスターから得意先様、御中フラグを取るため
            sql_m += "where 1=1 ";
            sql_m += "and T.cMITUMORI='" + cMITUMORI + "' ";         //見積コード //" + cMITUMORI + "

            if (fMIDASHI == false)
            {
                //提供金額＝単価＊数量＊掛け率
                sql_m_m += "select distinct ";
                sql_m_m += "T.rowNO as rowNO, ";                    //Index行　
                sql_m_m += "T.nGYOUNO as nGYOUNO, ";                    //行NO
                sql_m_m += "T.nINSATSU_GYO as nINSATSU_GYO, ";          //印刷行NO
                sql_m_m += "IF(T.sSYOUHIN_R IS NULL OR T.sSYOUHIN_R='','追加行1',sSYOUHIN_R) as sSYOUHIN_R, ";//内容.仕様----商品名 20171011 WaiWai add
                sql_m_m += "T.nSURYO as nSURYO, ";                      //数量  
                sql_m_m += "T.sTANI as sTANI, ";                        //単位 
                sql_m_m += "T.nTANKA as nTANKA, ";                      //単価
                sql_m_m += "T.nKINGAKU as nKINGAKU, ";                  //提供金額
                sql_m_m += "FORMAT(T.nRITU,0) as nRITU, ";              //掛け率
                sql_m_m += "IF((T.sKUBUN='見' AND T.sKUBUN is not null),'',T.nSIKIRIKINGAKU) as nSIKIRIKINGAKU, ";      //仕切金額 20190424 WaiWai update
                sql_m_m += "T.nSIKIRITANKA as nSIKIRITANKA, ";           //仕切単価
                sql_m_m += "T.sSETSUMUI AS sSETSUMUI, ";
                sql_m_m += "IFNULL(T.fJITAIS,0) AS fJITAIS, ";          //XIAO 20140605 ZWW ADD
                sql_m_m += "IFNULL(T.fJITAIQ,0) AS fJITAIQ ";           //QIANG 20140605 ZWW ADD
                sql_m_m += ",IFNULL(T.sKUBUN,'') AS sKUBUN ";           //区分 20190404 WaiWai add
                sql_m_m += "from r_mitumori_m T ";                      //从“見積商品明細 "表中查询
                sql_m_m += "where 1=1 ";
                sql_m_m += "and T.cMITUMORI='" + cMITUMORI + "' ";      //見積コード
                sql_m_m += " order by T.nGYOUNO ";
            }
            else
            {
                //提供金額＝単価＊数量＊掛け率
                sql_m_m += "select distinct ";
                sql_m_m += "T.rowNO as rowNO, ";                    //Index行　20170407 WaiWai add
                sql_m_m += "T.nGYOUNO as nGYOUNO, ";                    //行NO
                sql_m_m += "T.nINSATSU_GYO as nINSATSU_GYO, ";          //印刷行NO 20120802 jiangxiaomeng 
                                                                        //sql_m_m += "T.sSYOUHIN_R as sSYOUHIN_R, ";              //内容.仕様----商品名 20171011 WaiWai deleted
                sql_m_m += "IF(T.sSYOUHIN_R IS NULL OR T.sSYOUHIN_R='','追加行1',sSYOUHIN_R) as sSYOUHIN_R, ";//内容.仕様----商品名 20171011 WaiWai add

                sql_m_m += "IF((T.sKUBUN='見' AND T.sKUBUN is not null),'1',T.nSURYO) as nSURYO, ";   //数量  
                sql_m_m += "IF((T.sKUBUN='見' AND T.sKUBUN is not null),'式',T.sTANI) as sTANI, ";     //単位 
                sql_m_m += "T.nTANKA as nTANKA, ";                      //単価
                sql_m_m += "T.nKINGAKU as nKINGAKU, ";                  //提供金額
                sql_m_m += "FORMAT(T.nRITU,0) as nRITU, ";              //掛け率
                sql_m_m += "T.nSIKIRIKINGAKU as nSIKIRIKINGAKU, ";      //仕切金額
                sql_m_m += "T.nSIKIRITANKA as nSIKIRITANKA, ";           //仕切単価
                sql_m_m += "T.sSETSUMUI AS sSETSUMUI, ";
                sql_m_m += "IFNULL(T.fJITAIS,0) AS fJITAIS, ";          //XIAO 20140605 ZWW ADD
                sql_m_m += "IFNULL(T.fJITAIQ,0) AS fJITAIQ ";           //QIANG 20140605 ZWW ADD
                sql_m_m += ",IFNULL(T.sKUBUN,'') AS sKUBUN ";           //区分 20190404 WaiWai add
                sql_m_m += "from r_mitumori_m T ";                      //从“見積商品明細 "表中查询
                sql_m_m += "where 1=1 ";
                sql_m_m += "and T.cMITUMORI='" + cMITUMORI + "' ";      //見積コード
                sql_m_m += " AND ((T.sKUBUN<>'計' AND (T.sKUBUN='見' OR T.sKUBUN<>'間') AND T.sKUBUN is not null) OR T.sKUBUN is null) order by T.nGYOUNO ";//見出し行のみ、見出しと小計に挟まれていない行を表示する
            }

            #region 見積詳細関係　

            sql_m_ms += "select distinct ";
            sql_m_ms += "T.rowNO as rowNO, ";                    //行
            sql_m_ms += "T.nGYOUNO as nGYOUNO, ";                    //行NO
            sql_m_ms += "T.nINSATSU_GYO as nINSATSU_GYO, ";          //印刷行NO 20120802 jiangxiaomeng 
            sql_m_ms += "T.sSYOUHIN_R as sSYOUHIN_R, ";              //内容.仕様----商品名 
            sql_m_ms += "T.nSURYO as nSURYO, ";                      //数量  
            sql_m_ms += "T.sTANI as sTANI, ";                        //単位
            sql_m_ms += "T.nTANKA as nTANKA, ";                      //単価
            sql_m_ms += "T.nKINGAKU as nKINGAKU, ";                  //提供金額
            sql_m_ms += "FORMAT(T.nRITU,0) as nRITU, ";              //掛け率
            sql_m_ms += "T.nSIKIRIKINGAKU as nSIKIRIKINGAKU, ";      //仕切金額
            sql_m_ms += "T.nSIKIRITANKA as nSIKIRITANKA, ";           //仕切単価
            sql_m_ms += "'' AS sSETSUMUI, ";
            sql_m_ms += "IFNULL(T.fJITAIS,0) AS fJITAIS, ";          //XIAO 20140605 ZWW ADD
            sql_m_ms += "IFNULL(T.fJITAIQ,0) AS fJITAIQ ";           //QIANG 20140605 ZWW ADD
            sql_m_ms += ",'' AS sKUBUN ";           //区分 20190404 WaiWai add
            sql_m_ms += "from r_mitumori_m2 T ";                      //从“見積商品明細 "表中查询
            sql_m_ms += "where 1=1 ";
            sql_m_ms += "and T.cMITUMORI='" + cMITUMORI + "' ";      //見積コード
                                                                     //sql_m_ms += "and T.cMITUMORI_KO='" + cMITUMORI_KO + "' ";//見積子番号 20200319 WaiWai delete
            sql_m_ms += "and T.sSYOUHIN_R<>'' order by T.rowNO,T.nGYOUNO ";
            
            con.Open();
            MySqlCommand cmd_m = new MySqlCommand(sql_m, con);
            cmd_m.CommandTimeout = 0;
            dt_m = new DataTable();
            MySqlDataAdapter da_m = new MySqlDataAdapter(cmd_m);
            da_m.Fill(dt_m);

            MySqlCommand cmd_m_m = new MySqlCommand(sql_m_m, con);
            cmd_m_m.CommandTimeout = 0;
            dt_m_m = new DataTable();
            MySqlDataAdapter da_m_m = new MySqlDataAdapter(cmd_m_m);
            da_m_m.Fill(dt_m_m);

            //if (dt_m_m.Rows.Count > 0)
            //{
            //    fSYOUSAI = true;
            //}
            //else
            //{
            //    fSYOUSAI = false;
            //   // fRYOUHOU = false;
            //}

            con.Close();
            da_m.Dispose();
            da_m_m.Dispose();

            if (fMIDASHI == true)
            {
                int rindex_meisai_start = 1;
                for (int rindex = 0; rindex < dt_m_m.Rows.Count; rindex++)
                {
                    if (dt_m_m.Rows[rindex]["sKUBUN"].ToString() != "見" && dt_m_m.Rows[rindex]["nSURYO"].ToString() != "0.00" && dt_m_m.Rows[rindex]["nSURYO"].ToString() != "")
                    {
                        dt_m_m.Rows[rindex]["nINSATSU_GYO"] = rindex_meisai_start;
                        rindex_meisai_start++;
                    }
                }
                string expression = "(sKUBUN<>'計' AND (sKUBUN='見' OR sKUBUN<>'間') AND sKUBUN is not null) OR sKUBUN is null";
                string sortOrder = "nGYOUNO asc";
                DataRow[] dr_m_m = dt_m_m.Select(expression,sortOrder);
                dt_m_m = dr_m_m.CopyToDataTable();
            }

            #region getSyousaiData
            if (fICHIRAN == false && fSYOUSAI == true)
            {
                Boolean Row_add = false;//行追加flag
                Syousai_Temp1 = new DataTable();
                Syousai_Temp2 = new DataTable();
                Syousai_Temp3 = new DataTable();
                Syousai_All = new DataTable();
                sql_m_m = string.Empty;
                //
                //提供金額＝単価＊数量＊掛け率
                sql_m_m += "select distinct ";
                sql_m_m += "T.rowNO as rowNO, ";                    //Index行　20170407 WaiWai add
                sql_m_m += "T.nGYOUNO as nGYOUNO, ";                    //行NO
                sql_m_m += "T.nINSATSU_GYO as nINSATSU_GYO, ";          //印刷行NO 20120802 jiangxiaomeng 
                sql_m_m += "T.sSYOUHIN_R as sSYOUHIN_R, ";              //内容.仕様----商品名 
                sql_m_m += "T.nSURYO as nSURYO, ";                      //数量  
                sql_m_m += "T.sTANI as sTANI, ";                        //単位
                sql_m_m += "T.nTANKA as nTANKA, ";                      //単価
                sql_m_m += "T.nKINGAKU as nKINGAKU, ";                  //提供金額
                sql_m_m += "FORMAT(T.nRITU,0) as nRITU, ";              //掛け率
                sql_m_m += "T.nSIKIRIKINGAKU as nSIKIRIKINGAKU, ";      //仕切金額
                sql_m_m += "T.nSIKIRITANKA as nSIKIRITANKA, ";           //仕切単価
                sql_m_m += "T.sSETSUMUI AS sSETSUMUI, ";
                sql_m_m += "IFNULL(T.fJITAIS,0) AS fJITAIS, ";          //XIAO 20140605 ZWW ADD
                sql_m_m += "IFNULL(T.fJITAIQ,0) AS fJITAIQ ";           //QIANG 20140605 ZWW ADD
                sql_m_m += ",'' AS sKUBUN ";           //区分 20190404 WaiWai add
                sql_m_m += "from r_mitumori_m T ";                      //从“見積商品明細 "表中查询
                sql_m_m += "where 1=1 ";
                sql_m_m += "and T.cMITUMORI='" + cMITUMORI + "' ";      //見積コード
                                                                        //sql_m_m += "and T.cMITUMORI_KO='" + cMITUMORI_KO + "' ";//見積子番号 20200319 WaiWai delete
                                                                        //sql_m_m += "and T.sSYOUHIN_R<>'' AND (T.sKUBUN<>'見' AND T.sKUBUN<>'計') order by T.nGYOUNO ";  //modify by yamin 20200318
                sql_m_m += "and T.sSYOUHIN_R<>''";
                sql_m_m += " AND ((T.sKUBUN<>'見' AND T.sKUBUN<>'計') || T.sKUBUN is null)";
                sql_m_m += " order by T.nGYOUNO ";
               
                con.Open();
                MySqlCommand cmd1 = new MySqlCommand(sql_m_m, con);
                cmd1.CommandTimeout = 0;
                MySqlDataAdapter da1 = new MySqlDataAdapter(cmd1);
                da1.Fill(Syousai_Temp1);

                MySqlCommand cmdAll = new MySqlCommand(sql_m_ms, con);
                cmdAll.CommandTimeout = 0;
                MySqlDataAdapter daAll = new MySqlDataAdapter(cmdAll);
                daAll.Fill(Syousai_All);
                daAll.Fill(Syousai_Temp2);
                Syousai_Temp2.Rows.Clear();
                Syousai_Temp2.AcceptChanges();

                con.Close();
                daAll.Dispose();
                da1.Dispose();

                int o = Syousai_Temp1.Rows.Count;//一覧のcount
                int j = 0, m = 0;
                int rowno = 1;

                for (int kk = 0; kk < o; kk++)
                {
                    if (m > 0)//上の空行
                    {

                        try
                        {
                            //Syousai_Temp3 = Syousai_All.Set_DataFind("rowNO='" + Syousai_Temp1.Rows[kk]["rowNO"].ToString() + "' and sSYOUHIN_R<>''", "nGYOUNO ASC");
                            Syousai_Temp3 = Syousai_All.Select("rowNO='" + Syousai_Temp1.Rows[kk]["rowNO"].ToString() + "' and sSYOUHIN_R<>''", "nGYOUNO ASC").CopyToDataTable();
                        }
                        catch
                        {
                            Syousai_Temp3.Rows.Clear();
                            Syousai_Temp3.AcceptChanges();
                        }

                        if (Syousai_Temp3.Rows.Count != 0)
                        {
                            Syousai_Temp2.Rows.Add(Syousai_Temp2.NewRow());
                            Syousai_Temp2.AcceptChanges();

                            Syousai_Temp2.Rows[j]["nGYOUNO"] = Syousai_Temp1.Rows[kk]["nGYOUNO"];
                            //Syousai_Temp2.Rows[j]["cSYOUHIN"] = Syousai_Temp1.Rows[kk]["cSYOUHIN"];
                            Syousai_Temp2.Rows[j]["sSYOUHIN_R"] = "追加行";
                            //Syousai_Temp2.Rows[j]["nTANKA"] = Syousai_Temp1.Rows[kk]["nTANKA"];
                            //Syousai_Temp2.Rows[j]["nSURYO"] = Syousai_Temp1.Rows[kk]["nSURYO"];
                            //Syousai_Temp2.Rows[j]["sTANI"] = Syousai_Temp1.Rows[kk]["sTANI"];
                            //Syousai_Temp2.Rows[j]["nSIIRETANKA"] = Syousai_Temp1.Rows[kk]["nSIIRETANKA"];
                            //Syousai_Temp2.Rows[j]["nKINGAKU"] = Syousai_Temp1.Rows[kk]["nKINGAKU"];
                            //Syousai_Temp2.Rows[j]["nRITU"] = Syousai_Temp1.Rows[kk]["nRITU"];

                            //Syousai_Temp2.Rows[j]["cSHIIRESAKI"] = Syousai_Temp1.Rows[kk]["cSHIIRESAKI"];
                            //Syousai_Temp2.Rows[j]["sSHIIRESAKI"] = Syousai_Temp1.Rows[kk]["sSHIIRESAKI"];
                            //Syousai_Temp2.Rows[j]["nSIIREKINGAKU"] = Syousai_Temp1.Rows[kk]["nSIIREKINGAKU"];
                            //Syousai_Temp2.Rows[j]["nSIKIRITANKA"] = Syousai_Temp1.Rows[kk]["nSIKIRITANKA"];
                            //Syousai_Temp2.Rows[j]["nSIKIRIKINGAKU"] = Syousai_Temp1.Rows[kk]["nSIKIRIKINGAKU"];
                            Syousai_Temp2.Rows[j]["nINSATSU_GYO"] = "0";
                            //Syousai_Temp2.Rows[j]["cSYOUSAI"] = Syousai_Temp1.Rows[kk]["cSYOUSAI"];
                            //Syousai_Temp2.Rows[j]["sSETSUMUI"] = Syousai_Temp1.Rows[kk]["sSETSUMUI"];
                            //Syousai_Temp2.Rows[j]["fJITAIS"] = Syousai_Temp1.Rows[kk]["fJITAIS"];
                            //Syousai_Temp2.Rows[j]["fJITAIQ"] = Syousai_Temp1.Rows[kk]["fJITAIQ"];
                            //Syousai_Temp2.Rows[j]["rowNO"] = Syousai_Temp1.Rows[kk]["rowNO"];
                            //Syousai_Temp2.Rows[j]["sMEMO"] = Syousai_Temp1.Rows[kk]["sMEMO"];

                            j++;
                        }
                        else
                        {
                            try
                            {
                                //Syousai_Temp3 = Syousai_All.Set_DataFind("rowNO='" + Syousai_Temp1.Rows[kk - 1]["rowNO"].ToString() + "' and sSYOUHIN_R<>''", "nGYOUNO ASC");
                                Syousai_Temp3 = Syousai_All.Select("rowNO='" + Syousai_Temp1.Rows[kk - 1]["rowNO"].ToString() + "' and sSYOUHIN_R<>''", "nGYOUNO ASC").CopyToDataTable();
                            }
                            catch
                            {
                                Syousai_Temp3.Rows.Clear();
                                Syousai_Temp3.AcceptChanges();
                            }

                            if (Row_add == false && Syousai_Temp3.Rows.Count != 0)
                            {
                                Syousai_Temp2.Rows.Add(Syousai_Temp2.NewRow());
                                Syousai_Temp2.AcceptChanges();

                                Syousai_Temp2.Rows[j]["nGYOUNO"] = Syousai_Temp1.Rows[kk]["nGYOUNO"];
                                //Syousai_Temp2.Rows[j]["cSYOUHIN"] = Syousai_Temp1.Rows[kk]["cSYOUHIN"];
                                Syousai_Temp2.Rows[j]["sSYOUHIN_R"] = "追加行";
                                //Syousai_Temp2.Rows[j]["nTANKA"] = Syousai_Temp1.Rows[kk]["nTANKA"];
                                //Syousai_Temp2.Rows[j]["nSURYO"] = Syousai_Temp1.Rows[kk]["nSURYO"];
                                //Syousai_Temp2.Rows[j]["sTANI"] = Syousai_Temp1.Rows[kk]["sTANI"];
                                //Syousai_Temp2.Rows[j]["nSIIRETANKA"] = Syousai_Temp1.Rows[kk]["nSIIRETANKA"];
                                //Syousai_Temp2.Rows[j]["nKINGAKU"] = Syousai_Temp1.Rows[kk]["nKINGAKU"];
                                //Syousai_Temp2.Rows[j]["nRITU"] = Syousai_Temp1.Rows[kk]["nRITU"];

                                //Syousai_Temp2.Rows[j]["cSHIIRESAKI"] = Syousai_Temp1.Rows[kk]["cSHIIRESAKI"];
                                //Syousai_Temp2.Rows[j]["sSHIIRESAKI"] = Syousai_Temp1.Rows[kk]["sSHIIRESAKI"];
                                //Syousai_Temp2.Rows[j]["nSIIREKINGAKU"] = Syousai_Temp1.Rows[kk]["nSIIREKINGAKU"];
                                //Syousai_Temp2.Rows[j]["nSIKIRITANKA"] = Syousai_Temp1.Rows[kk]["nSIKIRITANKA"];
                                //Syousai_Temp2.Rows[j]["nSIKIRIKINGAKU"] = Syousai_Temp1.Rows[kk]["nSIKIRIKINGAKU"];
                                Syousai_Temp2.Rows[j]["nINSATSU_GYO"] = "0";
                                //Syousai_Temp2.Rows[j]["cSYOUSAI"] = Syousai_Temp1.Rows[kk]["cSYOUSAI"];
                                //Syousai_Temp2.Rows[j]["sSETSUMUI"] = Syousai_Temp1.Rows[kk]["sSETSUMUI"];
                                //Syousai_Temp2.Rows[j]["fJITAIS"] = Syousai_Temp1.Rows[kk]["fJITAIS"];
                                //Syousai_Temp2.Rows[j]["fJITAIQ"] = Syousai_Temp1.Rows[kk]["fJITAIQ"];
                                //Syousai_Temp2.Rows[j]["rowNO"] = Syousai_Temp1.Rows[kk]["rowNO"];
                                //Syousai_Temp2.Rows[j]["sMEMO"] = Syousai_Temp1.Rows[kk]["sMEMO"];

                                j++;
                                Row_add = true;
                            }

                        }

                    }

                    //タイトルデータ登録(一覧行データ)
                    Syousai_Temp2.Rows.Add(Syousai_Temp2.NewRow());
                    Syousai_Temp2.AcceptChanges();

                    Syousai_Temp2.Rows[j]["nGYOUNO"] = Syousai_Temp1.Rows[kk]["nGYOUNO"];
                    Syousai_Temp2.Rows[j]["rowNO"] = Syousai_Temp1.Rows[kk]["rowNO"];
                    Syousai_Temp2.Rows[j]["sSYOUHIN_R"] = Syousai_Temp1.Rows[kk]["sSYOUHIN_R"];
                    //Syousai_Temp2.Rows[j]["nTANKA"] = Syousai_Temp1.Rows[kk]["nTANKA"];
                    //Syousai_Temp2.Rows[j]["nSURYO"] = Syousai_Temp1.Rows[kk]["nSURYO"];
                    //Syousai_Temp2.Rows[j]["sTANI"] = Syousai_Temp1.Rows[kk]["sTANI"];
                    //Syousai_Temp2.Rows[j]["nKINGAKU"] = Syousai_Temp1.Rows[kk]["nKINGAKU"];
                    //Syousai_Temp2.Rows[j]["nRITU"] = Syousai_Temp1.Rows[kk]["nRITU"];
                    //Syousai_Temp2.Rows[j]["nSIKIRITANKA"] = Syousai_Temp1.Rows[kk]["nSIKIRITANKA"];
                    //Syousai_Temp2.Rows[j]["nSIKIRIKINGAKU"] = Syousai_Temp1.Rows[kk]["nSIKIRIKINGAKU"];
                    if (Syousai_Temp1.Rows[kk]["nSURYO"].ToString() != "0.00" && Syousai_Temp1.Rows[kk]["nSURYO"].ToString() != "" && Syousai_Temp1.Rows[kk]["nSURYO"].ToString() != "0")//数量がある行だけ表示
                    {
                        Syousai_Temp2.Rows[j]["nINSATSU_GYO"] = rowno;
                        rowno++;
                    }
                    else
                    {
                        Syousai_Temp2.Rows[j]["nINSATSU_GYO"] = "0";
                    }
                    Syousai_Temp2.Rows[j]["sSETSUMUI"] = Syousai_Temp1.Rows[kk]["sSETSUMUI"];
                    Syousai_Temp2.Rows[j]["fJITAIS"] = Syousai_Temp1.Rows[kk]["fJITAIS"];
                    Syousai_Temp2.Rows[j]["fJITAIQ"] = Syousai_Temp1.Rows[kk]["fJITAIQ"];

                    try
                    {
                        //一覧行の値によって詳細データを取得して登録
                        //Syousai_Temp3 = Syousai_All.Set_DataFind("rowNO='" + Syousai_Temp1.Rows[kk ]["rowNO"].ToString() + "' and sSYOUHIN_R<>''", "nGYOUNO ASC");
                        Syousai_Temp3 = Syousai_All.Select("rowNO='" + Syousai_Temp1.Rows[kk]["rowNO"].ToString() + "' and sSYOUHIN_R<>''", "nGYOUNO ASC").CopyToDataTable();
                    }
                    catch
                    {
                        Syousai_Temp3.Rows.Clear();
                        Syousai_Temp3.AcceptChanges();
                    }

                    if (Syousai_Temp3.Rows.Count == 0)
                    {
                        Syousai_Temp2.Rows[j]["nGYOUNO"] = Syousai_Temp1.Rows[kk]["nGYOUNO"];
                        Syousai_Temp2.Rows[j]["rowNO"] = Syousai_Temp1.Rows[kk]["rowNO"];
                        Syousai_Temp2.Rows[j]["sSYOUHIN_R"] = Syousai_Temp1.Rows[kk]["sSYOUHIN_R"];
                        Syousai_Temp2.Rows[j]["nTANKA"] = Syousai_Temp1.Rows[kk]["nTANKA"];
                        Syousai_Temp2.Rows[j]["nSURYO"] = Syousai_Temp1.Rows[kk]["nSURYO"];
                        Syousai_Temp2.Rows[j]["sTANI"] = Syousai_Temp1.Rows[kk]["sTANI"];
                        Syousai_Temp2.Rows[j]["nKINGAKU"] = Syousai_Temp1.Rows[kk]["nKINGAKU"];
                        Syousai_Temp2.Rows[j]["nRITU"] = Syousai_Temp1.Rows[kk]["nRITU"];
                        Syousai_Temp2.Rows[j]["nSIKIRITANKA"] = Syousai_Temp1.Rows[kk]["nSIKIRITANKA"];
                        Syousai_Temp2.Rows[j]["nSIKIRIKINGAKU"] = Syousai_Temp1.Rows[kk]["nSIKIRIKINGAKU"];
                        //Syousai_Temp2.Rows[j]["nINSATSU_GYO"] = Syousai_Temp1.Rows[kk]["nINSATSU_GYO"];
                        Syousai_Temp2.Rows[j]["sSETSUMUI"] = Syousai_Temp1.Rows[kk]["sSETSUMUI"];
                        Syousai_Temp2.Rows[j]["fJITAIS"] = Syousai_Temp1.Rows[kk]["fJITAIS"];
                        Syousai_Temp2.Rows[j]["fJITAIQ"] = Syousai_Temp1.Rows[kk]["fJITAIQ"];

                        m++;
                        j++;

                    }
                    else
                    {
                        Row_add = false;

                        m++;
                        j++;

                        //Tempテブルに詳細データを登録
                        for (int kkk = 0; kkk < Syousai_Temp3.Rows.Count; kkk++)
                        {

                            Syousai_Temp2.Rows.Add(Syousai_Temp2.NewRow());
                            Syousai_Temp2.AcceptChanges();

                            Syousai_Temp2.Rows[j]["nGYOUNO"] = Syousai_Temp3.Rows[kkk]["nGYOUNO"];
                            Syousai_Temp2.Rows[j]["rowNO"] = Syousai_Temp3.Rows[kkk]["rowNO"];
                            Syousai_Temp2.Rows[j]["sSYOUHIN_R"] = Syousai_Temp3.Rows[kkk]["sSYOUHIN_R"];
                            Syousai_Temp2.Rows[j]["nTANKA"] = Syousai_Temp3.Rows[kkk]["nTANKA"];
                            Syousai_Temp2.Rows[j]["nSURYO"] = Syousai_Temp3.Rows[kkk]["nSURYO"];
                            Syousai_Temp2.Rows[j]["sTANI"] = Syousai_Temp3.Rows[kkk]["sTANI"];
                            Syousai_Temp2.Rows[j]["nKINGAKU"] = Syousai_Temp3.Rows[kkk]["nKINGAKU"];
                            Syousai_Temp2.Rows[j]["nRITU"] = Syousai_Temp3.Rows[kkk]["nRITU"];
                            Syousai_Temp2.Rows[j]["nSIKIRITANKA"] = Syousai_Temp3.Rows[kkk]["nSIKIRITANKA"];
                            Syousai_Temp2.Rows[j]["nSIKIRIKINGAKU"] = Syousai_Temp3.Rows[kkk]["nSIKIRIKINGAKU"];
                            Syousai_Temp2.Rows[j]["nINSATSU_GYO"] = "0";
                            Syousai_Temp2.Rows[j]["sSETSUMUI"] = Syousai_Temp3.Rows[kkk]["sSETSUMUI"];
                            Syousai_Temp2.Rows[j]["fJITAIS"] = Syousai_Temp3.Rows[kkk]["fJITAIS"];
                            Syousai_Temp2.Rows[j]["fJITAIQ"] = Syousai_Temp3.Rows[kkk]["fJITAIQ"];

                            j++;


                        }

                        //for (int kkk = 0; kkk < 3; kkk++)
                        for (int kkk = 0; kkk < 2; kkk++)
                        {

                            if (kkk == 0)//計
                            {
                                if (!string.IsNullOrEmpty(Syousai_Temp1.Rows[kk]["sSYOUHIN_R"].ToString()))
                                {
                                    Syousai_Temp2.Rows.Add(Syousai_Temp2.NewRow());
                                    Syousai_Temp2.AcceptChanges();

                                    Syousai_Temp2.Rows[j]["nGYOUNO"] = Syousai_Temp1.Rows[kk]["nGYOUNO"];
                                    // Syousai_Temp2.Rows[j]["cSYOUHIN"] = Syousai_Temp1.Rows[kk]["cSYOUHIN"];
                                    Syousai_Temp2.Rows[j]["sSYOUHIN_R"] = "計";
                                    //Syousai_Temp2.Rows[j]["nTANKA"] = Syousai_Temp1.Rows[kk]["nTANKA"];
                                    //Syousai_Temp2.Rows[j]["nSURYO"] = Syousai_Temp1.Rows[kk]["nSURYO"];
                                    //Syousai_Temp2.Rows[j]["sTANI"] = Syousai_Temp1.Rows[kk]["sTANI"];
                                    // Syousai_Temp2.Rows[j]["nSIIRETANKA"] = Syousai_Temp1.Rows[kk]["nSIIRETANKA"];
                                    if (Syousai_Temp1.Rows[kk]["nSURYO"].ToString() != "0.00" && Syousai_Temp1.Rows[kk]["nSURYO"].ToString() != "")//数量がある行だけ表示
                                    {
                                        Syousai_Temp2.Rows[j]["nKINGAKU"] = Syousai_Temp1.Rows[kk]["nSIKIRITANKA"];//
                                        Syousai_Temp2.Rows[j]["nSIKIRIKINGAKU"] = Syousai_Temp1.Rows[kk]["nSIKIRITANKA"];
                                    }
                                    else
                                    {
                                        Syousai_Temp2.Rows[j]["nKINGAKU"] = 0;
                                        Syousai_Temp2.Rows[j]["nSIKIRIKINGAKU"] = 0;
                                    }
                                    //Syousai_Temp2.Rows[j]["nRITU"] = Syousai_Temp1.Rows[kk]["nRITU"];

                                    //Syousai_Temp2.Rows[j]["cSHIIRESAKI"] = Syousai_Temp1.Rows[kk]["cSHIIRESAKI"];
                                    //Syousai_Temp2.Rows[j]["sSHIIRESAKI"] = Syousai_Temp1.Rows[kk]["sSHIIRESAKI"];
                                    //Syousai_Temp2.Rows[j]["nSIIREKINGAKU"] = Syousai_Temp1.Rows[kk]["nSIIREKINGAKU"];
                                    // Syousai_Temp2.Rows[j]["nSIKIRITANKA"] = Syousai_Temp1.Rows[kk]["nSIKIRITANKA"];

                                    Syousai_Temp2.Rows[j]["nINSATSU_GYO"] = "0";
                                    //Syousai_Temp2.Rows[j]["cSYOUSAI"] = Syousai_Temp1.Rows[kk]["cSYOUSAI"];
                                    //Syousai_Temp2.Rows[j]["sSETSUMUI"] = "0";
                                    //Syousai_Temp2.Rows[j]["fJITAIS"] = Syousai_Temp1.Rows[kk]["fJITAIS"];
                                    //Syousai_Temp2.Rows[j]["fJITAIQ"] = Syousai_Temp1.Rows[kk]["fJITAIQ"];
                                    //Syousai_Temp2.Rows[j]["rowNO"] = Syousai_Temp1.Rows[kk]["rowNO"];
                                    //Syousai_Temp2.Rows[j]["sMEMO"] = Syousai_Temp1.Rows[kk]["sMEMO"];
                                }
                            }
                            else if (kkk == 1) //小計
                            {
                                if (!string.IsNullOrEmpty(Syousai_Temp1.Rows[kk]["sSYOUHIN_R"].ToString()))
                                {
                                    Syousai_Temp2.Rows.Add(Syousai_Temp2.NewRow());
                                    Syousai_Temp2.AcceptChanges();

                                    Syousai_Temp2.Rows[j]["nGYOUNO"] = Syousai_Temp1.Rows[kk]["nGYOUNO"];
                                    //Syousai_Temp2.Rows[j]["cSYOUHIN"] = Syousai_Temp1.Rows[kk]["cSYOUHIN"];
                                    //Syousai_Temp2.Rows[j]["sSYOUHIN_R"] = "小計";
                                    Syousai_Temp2.Rows[j]["sSYOUHIN_R"] = "詳細計";
                                    Syousai_Temp2.Rows[j]["nTANKA"] = Syousai_Temp1.Rows[kk]["nTANKA"];
                                    Syousai_Temp2.Rows[j]["nSURYO"] = Syousai_Temp1.Rows[kk]["nSURYO"];
                                    Syousai_Temp2.Rows[j]["sTANI"] = Syousai_Temp1.Rows[kk]["sTANI"];
                                    // Syousai_Temp2.Rows[j]["nSIIRETANKA"] = Syousai_Temp1.Rows[kk]["nSIIRETANKA"];
                                    if (Syousai_Temp1.Rows[kk]["nSURYO"].ToString() != "0.00" && Syousai_Temp1.Rows[kk]["nSURYO"].ToString() != "")//数量がある行だけ表示
                                    {
                                        Syousai_Temp2.Rows[j]["nKINGAKU"] = Syousai_Temp1.Rows[kk]["nKINGAKU"];
                                        Syousai_Temp2.Rows[j]["nSIKIRITANKA"] = Syousai_Temp1.Rows[kk]["nSIKIRITANKA"];
                                        Syousai_Temp2.Rows[j]["nSIKIRIKINGAKU"] = Syousai_Temp1.Rows[kk]["nSIKIRIKINGAKU"];
                                    }
                                    else
                                    {
                                        Syousai_Temp2.Rows[j]["nKINGAKU"] = 0;
                                        Syousai_Temp2.Rows[j]["nSIKIRITANKA"] = 0;
                                        Syousai_Temp2.Rows[j]["nSIKIRIKINGAKU"] = 0;
                                    }
                                    Syousai_Temp2.Rows[j]["nRITU"] = Syousai_Temp1.Rows[kk]["nRITU"];

                                    //Syousai_Temp2.Rows[j]["cSHIIRESAKI"] = Syousai_Temp1.Rows[kk]["cSHIIRESAKI"];
                                    //Syousai_Temp2.Rows[j]["sSHIIRESAKI"] = Syousai_Temp1.Rows[kk]["sSHIIRESAKI"];
                                    //Syousai_Temp2.Rows[j]["nSIIREKINGAKU"] = Syousai_Temp1.Rows[kk]["nSIIREKINGAKU"];

                                    Syousai_Temp2.Rows[j]["nINSATSU_GYO"] = "0";
                                    //Syousai_Temp2.Rows[j]["cSYOUSAI"] = Syousai_Temp1.Rows[kk]["cSYOUSAI"];
                                    //Syousai_Temp2.Rows[j]["sSETSUMUI"] = "0";
                                    //Syousai_Temp2.Rows[j]["fJITAIS"] = Syousai_Temp1.Rows[kk]["fJITAIS"];
                                    //Syousai_Temp2.Rows[j]["fJITAIQ"] = Syousai_Temp1.Rows[kk]["fJITAIQ"];
                                    //Syousai_Temp2.Rows[j]["rowNO"] = Syousai_Temp1.Rows[kk]["rowNO"];
                                    //Syousai_Temp2.Rows[j]["sMEMO"] = Syousai_Temp1.Rows[kk]["sMEMO"];
                                }

                            }
                            else
                            {

                                //if (!string.IsNullOrEmpty(Syousai_Temp1.Rows[kk]["sSYOUHIN_R"].ToString()) && !string.IsNullOrEmpty(Syousai_Temp1.Rows[kk+1]["sSYOUHIN_R"].ToString()))
                                //    {
                                //if (!string.IsNullOrEmpty(Syousai_Temp1.Rows[kk]["sSYOUHIN_R"].ToString()))
                                //{
                                //    Syousai_Temp2.Rows.Add(Syousai_Temp2.NewRow());
                                //    Syousai_Temp2.AcceptChanges();

                                //    Syousai_Temp2.Rows[j]["nGYOUNO"] = Syousai_Temp1.Rows[kk]["nGYOUNO"];
                                //    //Syousai_Temp2.Rows[j]["cSYOUHIN"] = Syousai_Temp1.Rows[kk]["cSYOUHIN"];
                                //    Syousai_Temp2.Rows[j]["sSYOUHIN_R"] = "追加行";
                                //    //Syousai_Temp2.Rows[j]["nTANKA"] = Syousai_Temp1.Rows[kk]["nTANKA"];
                                //    //Syousai_Temp2.Rows[j]["nSURYO"] = Syousai_Temp1.Rows[kk]["nSURYO"];
                                //    //Syousai_Temp2.Rows[j]["sTANI"] = Syousai_Temp1.Rows[kk]["sTANI"];
                                //    //Syousai_Temp2.Rows[j]["nSIIRETANKA"] = Syousai_Temp1.Rows[kk]["nSIIRETANKA"];
                                //    //Syousai_Temp2.Rows[j]["nKINGAKU"] = Syousai_Temp1.Rows[kk]["nKINGAKU"];
                                //    //Syousai_Temp2.Rows[j]["nRITU"] = Syousai_Temp1.Rows[kk]["nRITU"];

                                //    //Syousai_Temp2.Rows[j]["cSHIIRESAKI"] = Syousai_Temp1.Rows[kk]["cSHIIRESAKI"];
                                //    //Syousai_Temp2.Rows[j]["sSHIIRESAKI"] = Syousai_Temp1.Rows[kk]["sSHIIRESAKI"];
                                //    //Syousai_Temp2.Rows[j]["nSIIREKINGAKU"] = Syousai_Temp1.Rows[kk]["nSIIREKINGAKU"];
                                //    //Syousai_Temp2.Rows[j]["nSIKIRITANKA"] = Syousai_Temp1.Rows[kk]["nSIKIRITANKA"];
                                //    //Syousai_Temp2.Rows[j]["nSIKIRIKINGAKU"] = Syousai_Temp1.Rows[kk]["nSIKIRIKINGAKU"];
                                //    Syousai_Temp2.Rows[j]["nINSATSU_GYO"] = "0";
                                //    //Syousai_Temp2.Rows[j]["cSYOUSAI"] = Syousai_Temp1.Rows[kk]["cSYOUSAI"];
                                //    //Syousai_Temp2.Rows[j]["sSETSUMUI"] = Syousai_Temp1.Rows[kk]["sSETSUMUI"];
                                //    //Syousai_Temp2.Rows[j]["fJITAIS"] = Syousai_Temp1.Rows[kk]["fJITAIS"];
                                //    //Syousai_Temp2.Rows[j]["fJITAIQ"] = Syousai_Temp1.Rows[kk]["fJITAIQ"];
                                //    //Syousai_Temp2.Rows[j]["rowNO"] = Syousai_Temp1.Rows[kk]["rowNO"];
                                //    //Syousai_Temp2.Rows[j]["sMEMO"] = Syousai_Temp1.Rows[kk]["sMEMO"];
                                //}

                            }

                            j++;
                        }
                    }
                    // }
                }

                if (Syousai_Temp2.Rows.Count > 0)//詳細がある時だけ入れる
                {
                    //一覧と詳細データ入れる
                    dt_m_m = Syousai_Temp2;
                }
            }

            if (fSYOUSAI == true || fRYOUHOU == true)
            {
                //if (dbl_m_m.Rows[dbl_m_m.Rows.Count - 1]["sSYOUHIN_R"].ToString() == "小計")
                if (dt_m_m.Rows[dt_m_m.Rows.Count - 1]["sSYOUHIN_R"].ToString() == "詳細計")
                {
                    dt_m_m.Rows.Add(dt_m_m.NewRow());
                    dt_m_m.AcceptChanges();

                    dt_m_m.Rows[dt_m_m.Rows.Count - 1]["sSYOUHIN_R"] = "追加行";
                    dt_m_m.Rows[dt_m_m.Rows.Count - 1]["nINSATSU_GYO"] = "0";
                }
            }
            #endregion

            #endregion
            
            #region nPAGECOUNT
            if (dt_m_m.Rows.Count <= 25)
            {
                if (dt_m_m.Rows.Count <= 16)
                {
                    nPAGECOUNT = 1;

                    nPAGECOUNT_1 = 1;
                }
                else
                {
                        //    if (picture2.Visible == false && picture2.Visible == false && fNEWIMAGE == false)//20220505 maythu delete
                        //{
                        nPAGECOUNT = 1;

                        nPAGECOUNT_1 = 1;
                        //}
                        //else //20220505 maythu delete
                        //{
                        //    nPAGECOUNT = 2;

                        //    nPAGECOUNT_1 = 2;
                        //    header_flag = true;

                        //}
                    }

                }
            else
            {
                int n = 0;
                if (dt_m_m.Rows.Count == 28)
                {
                    n = 0;
                }
                else
                {
                    if ((dt_m_m.Rows.Count - 28) % 39 != 0)
                    {
                        n = (dt_m_m.Rows.Count - 28) / 39;
                    }
                    else
                    {
                        n = (dt_m_m.Rows.Count - 28) / 39;
                        n = n - 1;
                    }
                }
                for (int i = 0; i < n + 1; i++)
                {
                    if ((dt_m_m.Rows.Count - 28) - i * 39 <= 39)
                    {
                        if ((dt_m_m.Rows.Count - 28) - i * 39 <= 27)
                        {
                                //if (picture2.Visible == false && picture2.Visible == false && fNEWIMAGE == false)//20220505 maythu delete
                                //{
                                nPAGECOUNT = i + 2;


                                nPAGECOUNT_1 = i + 2;

                                //}
                                //else  //20220505 maythu delete
                                //{
                                //    nPAGECOUNT = i + 2;


                                //    nPAGECOUNT_1 = i + 2;
                                //}
                            }
                            else
                        {
                                //if (picture2.Visible == false && picture2.Visible == false && fNEWIMAGE == false) //20220505 maythu delete
                                //{
                                nPAGECOUNT = i + 2;


                                nPAGECOUNT_1 = i + 2;


                                //}
                                //else  //20220505 maythu delete
                                //{
                                //    nPAGECOUNT = i + 3;


                                //    nPAGECOUNT_1 = i + 3;

                                //    header_flag = true;

                                //}
                            }
                        }
                }
            }

            if (flag_page1 == true)
            {
               pcount += nPAGECOUNT;
            }

            if (flag_page1 == false)
            {
                nPAGECOUNT = pcount;
            }

            #endregion

            if (fHYOUSI == true)
            {
                nPAGECOUNT += 1;
            }

            if (dt_m.Rows.Count > 0)
            {
                Fields["sTOKUISAKI"].Value = dt_m.Rows[0]["sTOKUISAKI"].ToString();         //得意先名
                Fields["sMITUMORI1"].Value = dt_m.Rows[0]["sMITUMORI1"].ToString();           //件名
                Fields["cMITUMORI"].Value = dt_m.Rows[0]["cMITUMORI"].ToString();           //CODE
                Fields["dMITUMORINOKI"].Value = dt_m.Rows[0]["dMITUMORINOKI"].ToString();

                if (dt_m.Rows[0]["sMITUMORIYUKOKIGEN"].ToString().Trim() != "選択してください")
                {
                    Fields["sMITUMORIYUKOKIGEN"].Value = dt_m.Rows[0]["sMITUMORIYUKOKIGEN"].ToString(); //有効期限
                }

                Fields["cSHIHARAI"].Value = dt_m.Rows[0]["sSHIHARAI"].ToString();          //支払条件

                if (dt_m.Rows[0]["sUKEWATASIBASYO"].ToString().Trim() != "選択してください")
                {
                    Fields["sUKEWATASIBASYO"].Value = dt_m.Rows[0]["sUKEWATASIBASYO"].ToString();    //受渡し場所
                }
                nMITUMORISYOHIZE = Convert.ToDouble(dt_m.Rows[0]["nMITUMORISYOHIZE"].ToString());   //消費税
                nKINNGAKUKAZEI = Convert.ToDouble(dt_m.Rows[0]["nKAZEIKINGAKU"].ToString());   //kazeikingaku
                if (dt_m.Rows[0]["sTANTOUSHA"].ToString() != "")
                {
                    string space_dai = string.Empty;
                    if (dt_j_info.Rows[0]["sIMAGE"].ToString() != "")
                    {
                        if (dt_j_info.Rows[0]["fIMAGESize"].ToString() != "0")
                        {
                            space_dai = "　　　　　　　　　　　　　";
                        }
                    }

                    Fields["sTANTOUSHA"].Value = space_dai + "担当:" + dt_m.Rows[0]["sTANTOUSHA"].ToString(); //担当者名
                }

                if (dt_m.Rows[0]["sTOKUISAKI_TAN"].ToString() != "")
                {
                    if (dt_m.Rows[0]["SKEISHOU"].ToString() == "")
                    {
                        dt_m.Rows[0]["SKEISHOU"] = " 様";
                    }
                    if (dt_m.Rows[0]["sTOKUISAKI_YAKUSHOKU"].ToString() == "")
                    {
                        Fields["sTOKUISAKI_TAN"].Value = dt_m.Rows[0]["sTOKUISAKI_TAN"].ToString() + " " + dt_m.Rows[0]["SKEISHOU"].ToString();      //得意先担当者
                    }
                    else
                    {
                        Fields["sTOKUISAKI_TAN"].Value = dt_m.Rows[0]["sTOKUISAKI_YAKUSHOKU"].ToString() + " " + dt_m.Rows[0]["sTOKUISAKI_TAN"].ToString() + " " + dt_m.Rows[0]["SKEISHOU"].ToString();      //得意先担当者
                    }
                    try
                    {
                        if (Fields["sTOKUISAKI_TAN"].Value.ToString().Trim().Contains("\r\n"))
                        {
                            Fields["sTOKUISAKI_TAN"].Value = Fields["sTOKUISAKI_TAN"].Value.ToString().Trim().Replace("\r\n", "");
                        }
                        else if (Fields["sTOKUISAKI_TAN"].Value.ToString().Trim().Contains("\r"))
                        {
                            Fields["sTOKUISAKI_TAN"].Value = Fields["sTOKUISAKI_TAN"].Value.ToString().Trim().Replace("\r", "");
                        }
                        else if (Fields["sTOKUISAKI_TAN"].Value.ToString().Trim().Contains("\n"))
                        {
                            Fields["sTOKUISAKI_TAN"].Value = Fields["sTOKUISAKI_TAN"].Value.ToString().Trim().Replace("\n", "");
                        }

                    }
                    catch
                    { }
                    LB_sTOKUISAKI_TANBUMON.Visible = true;
                    LB_sTOKUISAKI_TAN.Visible = true;
                    LB_sTOKUISAKI_TAN1.Visible = false;
                    line7.Visible = false;//御中 label under line
                    line30.Visible = false;
                    //LB_sTOKUISAKI_TAN.Border.BottomStyle = BorderLineStyle.Solid;
                    // line7.Location =System.Drawing.PointF(0,0,0,0);

                    LB_fSAMA.Border.BottomStyle = BorderLineStyle.None;
                    LB_sTOKUISAKI.Border.BottomStyle = BorderLineStyle.None;
                    LB_sTOKUISAKI_TANBUMON.Border.BottomStyle = BorderLineStyle.None;
                    LB_sTOKUISAKI_TAN1.Border.BottomStyle = BorderLineStyle.None;
                    label19.Border.BottomStyle = BorderLineStyle.None;
                    LB_sTOKUISAKI_TAN.Border.BottomStyle = BorderLineStyle.Solid;
                    label20.Border.BottomStyle = BorderLineStyle.Solid;

                    if (dt_m.Rows[0]["sTOKUISAKI_TANBUMON"].ToString() != "")
                    {
                        Fields["sTOKUISAKI_TANBUMON"].Value = dt_m.Rows[0]["sTOKUISAKI_TANBUMON"].ToString(); ;//得意先担当者部門

                        try
                        {
                            if (Fields["sTOKUISAKI_TANBUMON"].Value.ToString().Trim().Contains("\r\n"))
                            {
                                Fields["sTOKUISAKI_TANBUMON"].Value = Fields["sTOKUISAKI_TANBUMON"].Value.ToString().Trim().Replace("\r\n", "");
                            }
                            else if (Fields["sTOKUISAKI_TANBUMON"].Value.ToString().Trim().Contains("\r"))
                            {
                                Fields["sTOKUISAKI_TANBUMON"].Value = Fields["sTOKUISAKI_TANBUMON"].Value.ToString().Trim().Replace("\r", "");
                            }
                            else if (Fields["sTOKUISAKI_TANBUMON"].Value.ToString().Trim().Contains("\n"))
                            {
                                Fields["sTOKUISAKI_TANBUMON"].Value = Fields["sTOKUISAKI_TANBUMON"].Value.ToString().Trim().Replace("\n", "");
                            }

                        }
                        catch
                        { }
                    }
                    else
                    {
                        LB_sTOKUISAKI_TANBUMON.Visible = false;
                        LB_sTOKUISAKI_TAN.Visible = false;
                        line30.Visible = false;
                    }
                }
                else
                {
                    LB_sTOKUISAKI_TANBUMON.Visible = false;
                    LB_sTOKUISAKI_TAN.Visible = false;
                    line30.Visible = false;
                }
                if (dt_m.Rows[0]["sTOKUISAKI_TAN"].ToString() == "")
                {
                    label20.Text = "";//様 label
                    LB_fSAMA.Text = "";

                    Fields["fSAMA"].Value = dt_m.Rows[0]["fSAMA"].ToString();//得意先フラグ　

                    if (!string.IsNullOrEmpty(Fields["fSAMA"].Value.ToString()))
                    {
                        Fields["sTOKUISAKI"].Value = Fields["sTOKUISAKI"].Value + "　" + Fields["fSAMA"].Value;
                    }
                    Fields["fSAMA"].Value = "";

                }
                else
                {
                    LB_fSAMA.Text = "";
                    // Fields["fSAMA"].Value = dt_m.Rows[0]["fSAMA"].ToString();
                }

                Fields["sBIKOU"].Value = dt_m.Rows[0]["sBIKOU"].ToString();    //備考

                if (dt_m.Rows[0]["dMITUMORISAKUSEI"].ToString() != "")
                {
                    Fields["dMITUMORISAKUSEI"].Value = dt_m.Rows[0]["dMITUMORISAKUSEI"].ToString().Substring(0, 10);  //作成日
                }
                if (HANKO_Check == "欄無し")
                {
                    lb_hanko1.Visible = false;
                    lb_hanko2.Visible = false;
                    lb_hanko3.Visible = false;
                    if (dt_m.Rows[0]["sMAIL"].ToString() != "")
                    {
                        Fields["sMAIL"].Value = "MAIL:" + dt_m.Rows[0]["sMAIL"].ToString();//メールアドレス20140515 lsl add
                    }
                    else
                    {
                        #region 
                        if (dt_j_info.Rows.Count > 0)
                        {
                            //担当者のメールアドレスがnullの場合は代表メールアドレスを表示
                            if (dt_j_info.Rows[0]["sMAIL"].ToString() != "")
                            {
                                Fields["sMAIL"].Value = "MAIL:" + dt_j_info.Rows[0]["sMAIL"].ToString();
                            }
                        }
                        #endregion
                    }
                }
                else
                {
                    LB_sTANTOUSHA.Visible = false;
                    LB_DAN2.Visible = false;
                    LB_sURL.Visible = false;
                    LB_URL2.Visible = false;
                    LB_SMAIL.Visible = false;
                    LB_MAI2.Visible = false;


                    if (HANKO_Check == "欄有り(担当印有り)")
                    {
                        try
                        {
                            if (dt_m.Rows.Count > 0)
                            {
                                if (dt_m.Rows[0]["sIMAGE1"] != null)
                                {
                                    if (!string.IsNullOrEmpty(dt_m.Rows[0]["sIMAGE1"].ToString()))
                                    {
                                        // P_sIMAGEHankou.Image = Image.FromStream(func.toImage((byte[])dbl_m.Tables[0].Rows[0]["sIMAGE1"]));
                                        byte[] bytes = (byte[])dt_m.Rows[0]["sIMAGE1"];
                                        MemoryStream stream = new MemoryStream(bytes);
                                        P_sIMAGEHankou.Image = System.Drawing.Image.FromStream(stream);
                                        P_sIMAGEHankou.PictureAlignment = PictureAlignment.Center;
                                    }
                                }
                            }
                        }
                        catch { }
                    }

                }
                if (!dt_m.Rows[0]["nMITUMORINEBIKI"].ToString().Equals("0.00"))
                {
                    Fields["nMITUMORINEBIKI"].Value = dt_m.Rows[0]["nMITUMORINEBIKI"].ToString();    //値引き
                }
                else
                {
                    label27.Text = "";
                    Fields["nMITUMORINEBIKI"].Value = "";
                }
                Fields["nSHIKIRI"].Value = dt_m.Rows[0]["nKINGAKU"].ToString();          //仕切合計
                Fields["nTANKA_G"].Value = dt_m.Rows[0]["nTANKA_G"].ToString();          //定価合計
                Fields["nKINGAKU1"].Value = dt_m.Rows[0]["nKINGAKU"].ToString();          //小計

                if (fZEINUKIKINNGAKU == true)
                {
                    // fZEINUKI(false);
                    label17.Visible = false;// (内、　本体価格 
                    TB_nKINGAKU1.Visible = false;
                    label22.Visible = false;// ,消費税 
                    TB_nMITUMORISYOHIZE1.Visible = false;
                    label23.Visible = false;// ) 
                    TB_nKINGAKUKAZEI.Visible = false;
                    LB_nKINGAKUKAZEI.Visible = false;
                    Fields["nKINGAKU2"].Value = Convert.ToDouble(Fields["nKINGAKU1"].Value);//税抜金額
                    label4.Text = "";//消費税
                    label9.Visible = true;//上記金額に消費税は含まれておりません。
                }
                else
                {
                    // fZEINUKI(false);
                    label17.Visible = false;// (内、　本体価格 
                    TB_nKINGAKU1.Visible = false;
                    label22.Visible = false;// ,消費税 
                    TB_nMITUMORISYOHIZE1.Visible = false;
                    label23.Visible = false;// ) 
                    TB_nKINGAKUKAZEI.Visible = false;
                    LB_nKINGAKUKAZEI.Visible = false;

                    Fields["nMITUMORISYOHIZE"].Value = Convert.ToString(nMITUMORISYOHIZE);      //消費税
                    Fields["nKINGAKU2"].Value = Convert.ToDouble(Fields["nKINGAKU1"].Value) + nMITUMORISYOHIZE;    ////お見積合計額＝小計＋消費税 
                    if (nKINNGAKUKAZEI != 0 && nKINNGAKUKAZEI != 0.00)
                    {
                        Fields["nKAZEIKINGAKU"].Value = Convert.ToString(nKINNGAKUKAZEI);
                    }
                    else
                    {
                        LB_nKINGAKUKAZEI.Visible = false;
                    }
                    label9.Visible = true;//上記金額に消費税は含まれておりません。
                }

                if (fZEIFUKUMUKIKINNGAKU == true)
                {
                    Fields["nKINGAKU_Title_goukei"].Value = Convert.ToDouble(Fields["nKINGAKU1"].Value) + nMITUMORISYOHIZE;     //小計 
                                                                                                                                //fZEINUKI(true);

                    label17.Visible = true;// (内、　本体価格 
                    TB_nKINGAKU1.Visible = true;
                    label22.Visible = true;// ,消費税 
                    TB_nMITUMORISYOHIZE1.Visible = true;
                    label23.Visible = true;// ) 
                    TB_nKINGAKUKAZEI.Visible = true;
                    LB_nKINGAKUKAZEI.Visible = true;
                    label9.Visible = false;//上記金額に消費税は含まれておりません。
                }
                else
                {
                    Fields["nKINGAKU_Title_goukei"].Value = Convert.ToDouble(Fields["nKINGAKU1"].Value);
                }

                if (dt_m.Rows[0]["fSYUUKEI"].ToString() == "1")
                {
                    Fields["nKINGAKU_Title_goukei"].Value = "";
                    Fields["nKINGAKU2"].Value = "";
                    Fields["nKINGAKU1"].Value = "";
                    Fields["nMITUMORISYOHIZE"].Value = "";
                    Fields["nSHIKIRI"].Value = "";
                    Fields["nKINGAKU1"].Value = "";
                    Fields["nKAZEIKINGAKU"].Value = "";
                    LB_nKINGAKUKAZEI.Visible = false;
                }
                if (dt_m.Rows[0]["nMITUMORINEBIKI"].ToString().Equals("0.00"))
                {
                    label27.Visible = false;//出精値引
                    textBox10.Visible = false;
                    textBox11.Visible = false;
                    label3.Visible = false;//明細計
                    label4.Location = label27.Location;//消費税
                    textBox12.Location = textBox10.Location;
                    LB_total.Location = label3.Location;//合計
                    textBox13.Location = textBox11.Location;
                    line29.Visible = false;
                    //if (picture1.Visible == false && picture2.Visible == false)//20220505 maythu delete
                    //{
                        LB_total.Border.BottomStyle = BorderLineStyle.None;
                        textBox13.Border.BottomStyle = BorderLineStyle.None;
                    //}
                }

            }

            if (dt_s_info.Rows.Count > 0)
            {
                if (fRYOUHOU == true && fSYOUSAI == true)
                {
                    Fields["sMITUMORI"].Value = dt_s_info.Rows[0][0].ToString() + "(詳細)";   //見積欄
                    LB_S_sMITUMORI_1.Visible = false;//見積一覧
                    LB_S_sMITUMORI.Visible = true;//見積詳細
                    //picture1.Visible = false;//20220505 maythu delete
                    //picture2.Visible = false;//20220505 maythu delete
                    }
                    else
                {
                    Fields["sMITUMORI"].Value = dt_s_info.Rows[0][0].ToString();   //見積欄
                    LB_S_sMITUMORI_1.Visible = true;//見積一覧
                    LB_S_sMITUMORI.Visible = false;//見積詳細
                }

                label2.Text = dt_s_info.Rows[0]["sAisatsu"].ToString();//ありがとうございました。下記の通りお見積りを申し上げます。

                Fields["sNAIYOU"].Value = dt_s_info.Rows[0][1].ToString();    //内容欄
                Fields["sKEN"].Value = dt_s_info.Rows[0][2].ToString();       //件名欄
                Fields["sNOUKI"].Value = dt_s_info.Rows[0][3].ToString();     //納期欄
                Fields["sYUUKOU"].Value = dt_s_info.Rows[0][4].ToString();    //有効期限欄 
                Fields["sSHIHARAI"].Value = dt_s_info.Rows[0][5].ToString();  //支払条件欄
                Fields["sUKEBASYOU"].Value = dt_s_info.Rows[0][6].ToString(); //受渡し場所欄 

                if (dt_j_info.Rows[0]["sIMAGE"].ToString() != "")
                {
                    if (dt_j_info.Rows[0]["fIMAGESize"].ToString() != "0")
                    {
                        //  P_sIMAGE2.Image = Image.FromStream(func.toImage((byte[])dbl_m_j_info.Tables[0].Rows[0]["sIMAGE"]));
                        byte[] bytes = (byte[])dt_j_info.Rows[0]["sIMAGE"];
                        MemoryStream stream = new MemoryStream(bytes);
                        P_sIMAGE2.Image = System.Drawing.Image.FromStream(stream);
                        P_sIMAGE2.PictureAlignment = PictureAlignment.TopLeft;

                        label21.Visible = false;//sCO
                        LB_cYUUBIN.Visible = false;
                        LB_sJUUSHO1.Visible = false;
                        LB_sJUUSHO2.Visible = false;
                        LB_TEL2.Visible = false;
                        LB_FAX2.Visible = false;
                        LB_sFAX.Visible = false;
                        LB_sTEL.Visible = false;
                        LB_sURL.Visible = false;
                        LB_URL2.Visible = false;
                        LB_SMAIL.Visible = false;
                        LB_MAI2.Visible = false;
                    }
                    else
                    {
                        // P_sIMAGE.Image = Image.FromStream(func.toImage((byte[])dbl_m_j_info.Tables[0].Rows[0]["sIMAGE"]));
                        byte[] bytes = (byte[])dt_j_info.Rows[0]["sIMAGE"];
                        MemoryStream stream = new MemoryStream(bytes);
                        P_sIMAGE.Image = System.Drawing.Image.FromStream(stream);
                        P_sIMAGE.PictureAlignment = PictureAlignment.TopLeft;
                    }
                    //start draw image for picture1,picture2 
                    //LoadFile(); //20220505 maythu delete
                    //end
                }
                else
                {
                    P_sIMAGE.Image = null;
                    P_sIMAGE2.Image = null;
                }
            }

                #region 詳細の場合は画像を表示しない 

                if (fSYOUSAI == true)
                {
                    //picture1.Visible = false;//20220505 maythu delete
                    //picture2.Visible = false;//20220505 maythu delete
                    line29.Visible = false;
                    fNEWIMAGE = false;
                }

            #endregion

            if (dt_j_info.Rows.Count > 0)
            {
                if (dt_j_info.Rows[0]["fIMAGESize"].ToString() == "0")
                {
                    LB_sTITLE.Text = dt_j_info.Rows[0]["sNAIYOU"].ToString();

                    if (LB_sTITLE.Text != "")
                    {
                        int index = -1;
                        int count = 0;
                        while (-1 != (index = LB_sTITLE.Text.IndexOf(Environment.NewLine, index + 1)))
                            count++;

                        if (count == 3)
                        {
                            //LB_sTITLE.Top = 1.706299F;
                            //P_sIMAGE.Top = 0.7512599F;

                            LB_sTITLE.Top = 1.8F;
                            P_sIMAGE.Top = 0.7712599F;
                        }
                        else if (count == 4)
                        {
                            if (HANKO_Check != "欄無し")
                            {
                                LB_sTITLE.Top = 1.673937F;
                                P_sIMAGE.Top = 0.7112599F;
                            }
                            else
                            {
                                LB_sTITLE.Top = 2.203937F;
                                P_sIMAGE.Top = 1.3612599F;
                            }

                        }
                        else if (count == 5 || count == 6)
                        {
                            if (HANKO_Check != "欄無し")
                            {
                                LB_sTITLE.Top = 1.57F;
                                P_sIMAGE.Top = 0.61F;
                            }
                            else
                            {
                                LB_sTITLE.Top = 1.903937F;
                                P_sIMAGE.Top = 0.9112599F;
                            }
                        }
                    }
                }
                if (HANKO_Check == "欄無し")
                {
                    lb_hanko1.Visible = false;
                    lb_hanko2.Visible = false;
                    lb_hanko3.Visible = false;
                    if (fINS == 0)
                    {
                        if (dt_m.Rows[0]["sMAIL"].ToString() != "")
                        {
                            LB_SMAIL.Value = "MAIL:" + dt_m.Rows[0]["sMAIL"].ToString();//メールアドレス
                        }
                        else
                        {
                            if (dt_j_info.Rows.Count > 0)
                            {
                                //担当者のメールアドレスがnullの場合は代表メールアドレスを表示
                                if (dt_j_info.Rows[0]["sMAIL"].ToString() != "")
                                {
                                    LB_SMAIL.Value = "MAIL:" + dt_j_info.Rows[0]["sMAIL"].ToString();
                                }
                            }
                        }

                        if (dt_m.Rows[0]["sTANTOUSHA"].ToString() != "")
                        {

                            LB_sTANTOUSHA.Value = "担当:" + dt_m.Rows[0]["sTANTOUSHA"].ToString(); //担当者名                                

                        }
                    }
                    else
                    {
                        if (dt_m.Rows.Count > 0)
                        {
                            if (dt_m.Rows[0]["sMAIL"].ToString() != "")
                            {
                                LB_SMAIL.Value = "MAIL:" + dt_m.Rows[0]["sMAIL"].ToString();//メールアドレス
                            }
                            else
                            {
                                if (dt_j_info.Rows.Count > 0)
                                {
                                    //担当者のメールアドレスがnullの場合は代表メールアドレスを表示
                                    if (dt_j_info.Rows[0]["sMAIL"].ToString() != "")
                                    {
                                        LB_SMAIL.Value = "MAIL:" + dt_j_info.Rows[0]["sMAIL"].ToString();
                                    }
                                }
                            }

                            if (dt_m.Rows[0]["sTANTOUSHA"].ToString() != "")
                            {
                                LB_sTANTOUSHA.Value = "担当:" + dt_m.Rows[0]["sTANTOUSHA"].ToString(); //担当者名  
                            }
                        }
                        else
                        {
                            if (dt_j_info.Rows.Count > 0)
                            {
                                //担当者のメールアドレスがnullの場合は代表メールアドレスを表示
                                if (dt_j_info.Rows[0]["sMAIL"].ToString() != "")
                                {
                                    LB_SMAIL.Value = "MAIL:" + dt_j_info.Rows[0]["sMAIL"].ToString();
                                }
                            }
                        }
                    }

                }
                else
                {
                    LB_sTANTOUSHA.Visible = false;
                    LB_SMAIL.Visible = false;
                }
            }

            if (dt_j_info.Rows.Count > 0)
            {
                int space = int.Parse(dt_j_info.Rows[0]["sSPACE"].ToString());
                for (int i = 0; i < space; i++)
                {
                    Fields["SMAIL"].Value = " " + Fields["SMAIL"].Value;
                    //Fields["sTANTOUSHA"].Value = " " + Fields["sTANTOUSHA"].Value;   
                    if (dt_j_info.Rows[0]["sIMAGE"].ToString() != "")
                    {
                        if (dt_j_info.Rows[0]["fIMAGESize"].ToString() != "0")
                        {

                        }
                        else
                        {
                            Fields["sTANTOUSHA"].Value = " " + Fields["sTANTOUSHA"].Value;
                        }
                    }
                    else
                    {
                        Fields["sTANTOUSHA"].Value = " " + Fields["sTANTOUSHA"].Value;
                    }

                }
            }

            #region LB_sTITLE
            int index1 = -1;
            int count1 = 0;
            while (-1 != (index1 = LB_sTITLE.Text.IndexOf(Environment.NewLine, index1 + 1)))
                count1++;
            if (count1 < 7)
            {
                if (HANKO_Check == "欄無し")
                {
                    if (!string.IsNullOrEmpty(LB_sTITLE.Text))
                    {
                        if (LB_sTITLE.Text.EndsWith("\r\n") == false)
                        {
                            LB_sTITLE.Text = LB_sTITLE.Text + "\r\n" + Fields["sTANTOUSHA"].Value + "\r\n" + Fields["SMAIL"].Value;
                            LB_SMAIL.Visible = false;
                            LB_sTANTOUSHA.Visible = false;
                        }
                        else
                        {
                            LB_sTITLE.Text = LB_sTITLE.Text + Fields["sTANTOUSHA"].Value + "\r\n" + Fields["SMAIL"].Value;
                            LB_SMAIL.Visible = false;
                            LB_sTANTOUSHA.Visible = false;
                        }
                    }
                }
            }
            #endregion

            #region LB_sTOKUISAKI, LB_sTOKUISAKI_TANBUMON, LB_sTOKUISAKI_TAN Text Alignment
            if (tokui_align != null)
            {
                if (!string.IsNullOrEmpty(tokui_align.Trim()))
                {
                    if (tokui_align == "左寄せ")
                    {
                        LB_sTOKUISAKI.Alignment = GrapeCity.ActiveReports.Drawing.TextAlignment.Left;
                    }
                    else if (tokui_align == "中央")
                    {
                        LB_sTOKUISAKI.Alignment = GrapeCity.ActiveReports.Drawing.TextAlignment.Center;
                    }
                }
            }
            if (busyo_align != null)
            {
                if (!string.IsNullOrEmpty(busyo_align.Trim()))
                {
                    if (busyo_align == "左寄せ")
                    {
                        LB_sTOKUISAKI_TANBUMON.Alignment = GrapeCity.ActiveReports.Drawing.TextAlignment.Left;
                    }
                    else if (busyo_align == "中央")
                    {
                        LB_sTOKUISAKI_TANBUMON.Alignment = GrapeCity.ActiveReports.Drawing.TextAlignment.Center;
                    }
                }
            }
            if (tantou_align != null)
            {
                if (!string.IsNullOrEmpty(tantou_align.Trim()))
                {
                    if (tantou_align == "左寄せ")
                    {
                        if (LB_sTOKUISAKI_TAN.Visible == true)
                        {
                            LB_sTOKUISAKI_TAN.Alignment = GrapeCity.ActiveReports.Drawing.TextAlignment.Left;
                        }
                        else
                        {
                            LB_sTOKUISAKI_TAN1.Alignment = GrapeCity.ActiveReports.Drawing.TextAlignment.Left;
                        }
                    }
                    else if (tantou_align == "中央")
                    {
                        if (LB_sTOKUISAKI_TAN.Visible == true)
                        {
                            LB_sTOKUISAKI_TAN.Alignment = GrapeCity.ActiveReports.Drawing.TextAlignment.Center;
                        }
                        else
                        {
                            LB_sTOKUISAKI_TAN1.Alignment = GrapeCity.ActiveReports.Drawing.TextAlignment.Center;
                        }
                    }
                }
            }
                #endregion



                #endregion

            }
            else
            {
                //20220125 MyatNoe Added
                pageHeader.Visible = false;
                reportFooter1.Visible = false;
                detail.Visible = false;
                line7.Visible = false;
                line30.Visible = false;
                LB_sTITLE.Text = sTITLE;

                if (logourl != "")
                {
                    if (logoByte != null)
                    {
                        MemoryStream stream = new MemoryStream(logoByte);
                        P_sIMAGE.Image = System.Drawing.Image.FromStream(stream);
                        P_sIMAGE.PictureAlignment = PictureAlignment.Center;
                    }
                    else
                    {
                        byte[] bytes = Convert.FromBase64String(logourl);
                        using (MemoryStream ms = new MemoryStream(bytes))
                        {
                            //P_sIMAGE.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                            P_sIMAGE.Image = System.Drawing.Image.FromStream(ms);
                            P_sIMAGE.PictureAlignment = PictureAlignment.Center;
                        }
                    }
                }
            }

        }

        #endregion

        #region LoadFile
        private void LoadFile()
        {
            #region bukkenlist
            string sql = "";
            sql = "Select distinct ";
            sql += "M_BU_FILE.cBUKKEN as cBUKKEN,";
            sql += "IfNull(M_BU_FILE.nJUNBAN,1) As nJUNBAN,";
            sql += "IfNull(M_BU_FILE.cFILE,'') As cFILE,";
            sql += "IfNull(M_FILE.sPATH_SERVER_SOURCE,'') As sPATH_SERVER_SOURCE,";
            sql += "IfNull(M_FILE.sFILE,'') As sFILE,";
            sql += "IfNull(M_FILE.sPATH_SUB_DIR,'') As sPATH_SUB_DIR,";
            sql += "IfNull(M_FILE.cTYPE,'') As cTYPE ";
            sql += "From M_BU_FILE ";
            sql += "Left Join M_FILE on M_FILE.cFILE = M_BU_FILE.cFILE ";
            sql += "Where '1'='1' and M_BU_FILE.cBUKKEN='" + cBUKKEN + "' and M_BU_FILE.fVISABLE='1' ";
            sql += " and (M_FILE.sPATH_SERVER_SOURCE is not null and sPATH_SERVER_SOURCE<>'');";

            JC_ClientConnecction_Class jc = new JC_ClientConnecction_Class();
            jc.loginId = loginId;//Session["LoginId"].ToString();
            con = jc.GetConnection();
            con.Open();
            MySqlCommand cmd_bukken = new MySqlCommand(sql, con);
            cmd_bukken.CommandTimeout = 0;
            DataTable dt_bukkenlist = new DataTable();
            MySqlDataAdapter da_bukken = new MySqlDataAdapter(cmd_bukken);
            da_bukken.Fill(dt_bukkenlist);
            con.Close();
            da_bukken.Dispose();
            #endregion

            #region mitsumorilist
            sql = "";
            sql += "SELECT DISTINCT";
            sql += " cMITUMORI AS cMITUMORI";
            sql += ",cFILE AS cFILE";
            sql += ",fVISABLE AS fVISABLE";
            sql += ",nJUNBAN AS nJUNBAN";
            sql += " FROM M_MITSU_FILE";
            sql += " WHERE cMITUMORI='" + cMITUMORI + "'";
            con.Open();
            MySqlCommand cmd_mitsumori = new MySqlCommand(sql, con);
            cmd_mitsumori.CommandTimeout = 0;
            DataTable dt_mitsumorilist = new DataTable();
            MySqlDataAdapter da_mitsumori = new MySqlDataAdapter(cmd_mitsumori);
            da_mitsumori.Fill(dt_mitsumorilist);
            con.Close();
            da_mitsumori.Dispose();
            #endregion

            #region draw image for picture1 and picture2
            picture1.Visible = false;
            picture2.Visible = false;
            for (int i = 0; i < dt_mitsumorilist.Rows.Count; i++)
            {
                for (int j = 0; j < dt_bukkenlist.Rows.Count; j++)
                {
                    if (dt_mitsumorilist.Rows[i]["cFILE"].ToString() == dt_bukkenlist.Rows[j]["cFILE"].ToString())
                    {
                        if (dt_mitsumorilist.Rows[i]["nJUNBAN"].ToString() == "6")
                        {
                            try
                            {
                                // convert byte[] to Base64
                                string filepath = dt_bukkenlist.Rows[j]["sPATH_SERVER_SOURCE"].ToString();
                                string ext = Path.GetExtension(filepath);
                                if (ext.ToLower().Contains("gif") || ext.ToLower().Contains("jpg") || ext.ToLower().Contains("jpeg") || ext.ToLower().Contains("png") || ext.ToLower().Contains("jfif"))
                                {
                                    byte[] bytes = System.IO.File.ReadAllBytes(filepath);
                                    MemoryStream stream = new MemoryStream(bytes);
                                    picture1.Image = System.Drawing.Image.FromStream(stream);
                                    picture1.PictureAlignment = PictureAlignment.Center;
                                    picture1.Visible = true;
                                }
                                else if (ext.ToLower().Contains("pdf") || ext.ToLower().Contains("ai"))
                                {
                                    byte[] pdfBytes = System.IO.File.ReadAllBytes(filepath);
                                    using (var inputMS = new MemoryStream(pdfBytes))
                                    {
                                        GhostscriptRasterizer rasterizer = null;

                                        using (rasterizer = new GhostscriptRasterizer())
                                        {
                                            rasterizer.Open(inputMS);

                                            using (MemoryStream ms = new MemoryStream())
                                            {
                                                int pagecount = rasterizer.PageCount;

                                                System.Drawing.Image img = rasterizer.GetPage(200, 1);
                                                img.Save(ms, ImageFormat.Png);

                                                picture1.Image = img;
                                                picture1.PictureAlignment = PictureAlignment.Center;
                                                picture1.Visible = true;
                                            }


                                            rasterizer.Close();
                                        }
                                    }
                                }
                            }
                            catch { }
                        }
                        else if (dt_mitsumorilist.Rows[i]["nJUNBAN"].ToString() == "7")
                        {
                            try
                            {

                                string filepath = dt_bukkenlist.Rows[j]["sPATH_SERVER_SOURCE"].ToString();
                                string ext = Path.GetExtension(filepath);
                                if (ext.ToLower().Contains("gif") || ext.ToLower().Contains("jpg") || ext.ToLower().Contains("jpeg") || ext.ToLower().Contains("png") || ext.ToLower().Contains("jfif"))
                                {
                                    byte[] bytes = System.IO.File.ReadAllBytes(filepath);
                                    MemoryStream stream = new MemoryStream(bytes);
                                    picture2.Image = System.Drawing.Image.FromStream(stream);
                                    picture2.PictureAlignment = PictureAlignment.Center;
                                    picture2.Visible = true;
                                    line29.Visible = true;
                                }
                                else if (ext.ToLower().Contains("pdf") || ext.ToLower().Contains("ai"))
                                {
                                    byte[] pdfBytes = System.IO.File.ReadAllBytes(filepath);
                                    using (var inputMS = new MemoryStream(pdfBytes))
                                    {
                                        GhostscriptRasterizer rasterizer = null;

                                        using (rasterizer = new GhostscriptRasterizer())
                                        {
                                            rasterizer.Open(inputMS);

                                            using (MemoryStream ms = new MemoryStream())
                                            {
                                                int pagecount = rasterizer.PageCount;

                                                System.Drawing.Image img = rasterizer.GetPage(200, 1);
                                                img.Save(ms, ImageFormat.Png);

                                                picture2.Image = img;
                                                picture2.PictureAlignment = PictureAlignment.Center;
                                                picture2.Visible = true;
                                                line29.Visible = true;
                                            }

                                            rasterizer.Close();
                                        }
                                    }
                                }
                            }
                            catch { }
                        }
                    }
                }
            }
            #endregion
        }
        #endregion

        #region mitsumori_DataInitialize
        private void mitsumori_DataInitialize(object sender, EventArgs e)
        {
            Fields.Add("sCO");
            Fields.Add("cMITUMORI");          //見積CODE
            Fields.Add("sMITUMORI");          //見積欄
            Fields.Add("sNAIYOU");            //内容欄
            Fields.Add("sBIKOU");             //明細備考欄
            Fields.Add("sKEN");               //件名欄
            Fields.Add("sNOUKI");             //納期欄
            Fields.Add("sYUUKOU");            //有効期限欄
            Fields.Add("sSHIHARAI");          //支払条件欄
            Fields.Add("cSHIHARAI");          //支払条件欄
            Fields.Add("sUKEBASYOU");         //受渡し場所欄
            Fields.Add("sTOKUISAKI");         //得意先名
            Fields.Add("sTOKUISAKI_TAN");     //得意先担当者
            Fields.Add("sMITUMORI1");         //件名
            Fields.Add("dMITUMORINOKI");      //納期
            Fields.Add("dMITUMORISAKUSEI");   //作成日
            Fields.Add("sMITUMORIYUKOKIGEN"); //有効期限
            Fields.Add("cSHIHARAI");          //支払条件
            Fields.Add("sUKEWATASIBASYO");    //受渡し場所
            Fields.Add("nRITU");             //掛け率 
            Fields.Add("nKAZEIKINGAKU");
            Fields.Add("sTANTOUSHA");         //担当者
            Fields.Add("nTANKA");             //単価
            Fields.Add("nSIKIRITANKA");       //仕切単価  
            Fields.Add("nSIKIRITANKA2");       //仕切単価  
            Fields.Add("nKINGAKU");           //金額      
            Fields.Add("nMITUMORINEBIKI");         //税込金額
            Fields.Add("nSHIKIRI");             //明細合計
            Fields.Add("nTANKA_G");
            Fields.Add("cYUUBIN");            //郵便番号
            Fields.Add("sJUUSHO1");           //住所１
            Fields.Add("sJUUSHO2");           //住所2
            Fields.Add("sTEL");               //電話番号
            Fields.Add("sFAX");               //ファックス番号
            Fields.Add("sURL");               //ホームページURL
            Fields.Add("sMAIL");              //メールアドレス

            Fields.Add("sTOKUISAKI_TANBUMON");//得意先担当者部門
            Fields.Add("fSAMA");//得意先様、御中フラグ

            Fields.Add("nMITUMORISYOHIZE");//消費税
            Fields.Add("nKINGAKUKAZEI");
            Fields.Add("nKINGAKU1");
            Fields.Add("nKINGAKU_Title_goukei");

            Fields.Add("nINSATSU_GYO");
            Fields.Add("sSYOUHIN_R");
            Fields.Add("nSURYO");
            Fields.Add("nSURYO2");
            Fields.Add("sTANI");
            Fields.Add("nKINGAKU");
            Fields.Add("nKINGAKU2");
        }
        #endregion

        #region mitsumori_FetchData
        int j = 1;
        private void mitsumori_FetchData(object sender, FetchEventArgs eArgs)
        {
            try
            {
                #region total row counts
                int maxrow = dt_m_m.Rows.Count;

                int newmaxrow = FIRSTMAXROWS;
                //20220505 maythu add start
                if (maxrow <= 25)
                {
                    newmaxrow = 25;
                }
                else
                {
                    if (maxrow > 25 && maxrow <= 28)
                    {
                        newmaxrow += MAXROWS;
                    }
                    else
                    {
                        while (maxrow > newmaxrow)
                        {
                            newmaxrow += MAXROWS;
                        }

                    }
                }
                reportFooter1.Height = float.Parse("0");
                //20220505 maythu add end

                #region 20220505 maythu delete
                //if (picture1.Visible == false && picture2.Visible == false && fNEWIMAGE == false)//不显示画像
                //{
                //    if (maxrow <= 25)
                //    {
                //        newmaxrow = 25;
                //    }
                //    else
                //    {
                //        if (maxrow > 25 && maxrow <= 28)
                //        {
                //            newmaxrow += MAXROWS;
                //        }
                //        else
                //        {
                //            while (maxrow > newmaxrow)
                //            {
                //                newmaxrow += MAXROWS;
                //            }

                //        }
                //    }
                //    reportFooter1.Height = float.Parse("0");
                //}
                //else//显示画像
                //{
                //    if (maxrow <= FIRSTMAXROWS)//見積商品明細数据少于25行
                //    {
                //        if (maxrow <= 25)
                //        {
                //            if (maxrow <= (25 - IMAGEROWS))
                //            {
                //                newmaxrow = 25 - IMAGEROWS;
                //            }
                //            else
                //            {
                //                newmaxrow = FIRSTMAXROWS + MAXROWS - IMAGEROWS;
                //            }
                //        }
                //        else
                //        {
                //            if (maxrow <= (FIRSTMAXROWS - IMAGEROWS))
                //            {
                //                newmaxrow = FIRSTMAXROWS - IMAGEROWS;
                //            }
                //            else
                //            {
                //                newmaxrow = FIRSTMAXROWS + MAXROWS - IMAGEROWS;
                //            }
                //        }
                //    }
                //    else//見積商品明細数据大于25行
                //    {
                //        while (maxrow > newmaxrow)
                //        {
                //            if (maxrow <= (newmaxrow + MAXROWS))
                //            {
                //                if (maxrow <= (newmaxrow + MAXROWS - IMAGEROWS))
                //                {
                //                    newmaxrow += (MAXROWS - IMAGEROWS);
                //                }
                //                else
                //                {
                //                    newmaxrow += MAXROWS + MAXROWS - IMAGEROWS;
                //                }
                //            }
                //            else
                //            {
                //                newmaxrow += MAXROWS;
                //            }
                //        }
                //    }
                //}
                #endregion

                for (int rowind = maxrow; rowind < newmaxrow; rowind++)
                {
                    dt_m_m.Rows.Add(dt_m_m.NewRow());
                    dt_m_m.AcceptChanges();
                    dt_m_m.Rows[rowind][0] = rowind + 1;
                    dt_m_m.Rows[rowind]["nINSATSU_GYO"] = "99999";
                    dt_m_m.Rows[rowind]["sSYOUHIN_R"] = "追加行1";
                }

                #endregion

                if (counter < dt_m_m.Rows.Count)
                {
                    if (dt_m_m.Rows[counter]["fJITAIS"].ToString() == "1" && dt_m_m.Rows[counter]["fJITAIQ"].ToString() == "1")
                    {
                        ChangeFont(f3);
                    }
                    else if (dt_m_m.Rows[counter]["fJITAIS"].ToString() == "1" && (dt_m_m.Rows[counter]["fJITAIQ"].ToString() == "" || dt_m_m.Rows[counter]["fJITAIQ"].ToString() == "0"))
                    {
                        ChangeFont(f1);
                    }
                    else if ((dt_m_m.Rows[counter]["fJITAIS"].ToString() == "" || dt_m_m.Rows[counter]["fJITAIS"].ToString() == "0") && dt_m_m.Rows[counter]["fJITAIQ"].ToString() == "1")
                    {
                        ChangeFont(f2);
                    }
                    else
                    {
                        ChangeFont(f4);
                    }

                    if (fSYOUSAI == true)
                    {
                        if (!string.IsNullOrEmpty(dt_m_m.Rows[counter]["nINSATSU_GYO"].ToString()))
                        {
                            if (dt_m.Rows[0]["fNO"].ToString() == "1")//行番号手入力の場合
                            {
                                Fields["nINSATSU_GYO"].Value = dt_m_m.Rows[counter]["sSETSUMUI"].ToString();
                            }
                            else //行番号自動入力の場合
                            {
                                if (dt_m_m.Rows[counter]["nINSATSU_GYO"].ToString() == "0" || dt_m_m.Rows[counter]["nINSATSU_GYO"].ToString() == "99999" || dt_m_m.Rows[counter]["nINSATSU_GYO"].ToString() == "")
                                {
                                    Fields["nINSATSU_GYO"].Value = "";
                                }
                                else
                                {
                                    int print_no = int.Parse(dt_m_m.Rows[counter]["nINSATSU_GYO"].ToString());
                                    string print_format = string.Empty;
                                    if (print_no.ToString().Length <= 2)//99まで
                                    {
                                        print_format = "(" + print_no.ToString("00") + ")";
                                    }
                                    else //100の場合
                                    {
                                        print_format = "(" + print_no.ToString("000") + ")";
                                    }

                                    Fields["nINSATSU_GYO"].Value = print_format;
                                }
                            }
                        }
                    }
                    else //一覧の場合
                    {
                        if (dt_m_m.Rows[counter]["nINSATSU_GYO"].ToString() == "99999")
                        {
                            Fields["nINSATSU_GYO"].Value = "";
                        }
                        else
                        {

                            if (dt_m.Rows[0]["fNO"].ToString() == "1")//行番号手入力の場合
                            {
                                Fields["nINSATSU_GYO"].Value = dt_m_m.Rows[counter]["sSETSUMUI"].ToString();
                            }
                            else //行番号自動入力の場合
                            {
                                int print_no = j;
                                int print = Count_Midashi;
                                string print_format = string.Empty;
                                if (dt_m_m.Rows[counter]["nSURYO"].ToString() != "0.00" && dt_m_m.Rows[counter]["nSURYO"].ToString() != "" && dt_m_m.Rows[counter]["nSURYO"].ToString() != "0")
                                {
                                    if (fMIDASHI == false)
                                    {

                                        if (print_no.ToString().Length <= 2)//99まで
                                        {
                                            print_format = "(" + print_no.ToString("00") + ")";
                                        }
                                        else //100の場合
                                        {
                                            print_format = "(" + print_no.ToString("000") + ")";
                                        }
                                        j++;
                                    }
                                    else
                                    {
                                        if (fINS == 0)
                                        {
                                            if (dt_m_m.Rows[counter]["sKUBUN"].ToString() == "見")
                                            {
                                                print_format = print.ToString() + ".";
                                                Count_Midashi++;
                                            }
                                            else
                                            {
                                                print_no = int.Parse(dt_m_m.Rows[counter]["nINSATSU_GYO"].ToString());
                                                if (print_no.ToString().Length <= 2)//99まで
                                                {
                                                    print_format = "(" + print_no.ToString("00") + ")";
                                                }
                                                else //100の場合
                                                {
                                                    print_format = "(" + print_no.ToString("000") + ")";
                                                }
                                            }
                                        }
                                        else
                                        {
                                            print_format = print.ToString() + ".";
                                            Count_Midashi++;
                                        }
                                    }
                                    Fields["nINSATSU_GYO"].Value = print_format;
                                    j++;
                                }
                                else
                                {
                                    if (dt_m_m.Rows[counter]["sKUBUN"].ToString() == "見")//add by zinmar 20211011
                                    {
                                        print_format = print.ToString() + ".";
                                        Count_Midashi++;
                                        Fields["nINSATSU_GYO"].Value = print_format;
                                    }
                                    else
                                    {
                                        Fields["nINSATSU_GYO"].Value = "";
                                    }
                                }
                            }

                        }
                    }

                    #region 
                    if (dt_m_m.Rows[counter]["sSYOUHIN_R"].ToString() == "追加行" || dt_m_m.Rows[counter]["sSYOUHIN_R"].ToString() == "追加行1")
                    {
                        Fields["sSYOUHIN_R"].Value = "";
                    }
                    else
                    {
                        Fields["sSYOUHIN_R"].Value = dt_m_m.Rows[counter]["sSYOUHIN_R"].ToString();
                    }

                    if (dt_m_m.Rows[counter]["nSURYO"].ToString() == "0.00" || dt_m_m.Rows[counter]["nSURYO"].ToString() == "" || dt_m_m.Rows[counter]["nSURYO"].ToString() == "0")   //数量  为空时，数据库默认为：0.00
                    {
                        Fields["nSURYO"].Value = "";
                        Fields["nSURYO2"].Value = "";
                        Fields["sTANI"].Value = "";
                        Fields["nTANKA"].Value = "";
                        Fields["nSIKIRITANKA"].Value = "";
                        Fields["nKINGAKU"].Value = "";
                    }
                    else
                    {
                        string str = Double.Parse(dt_m_m.Rows[counter]["nSURYO"].ToString()).ToString("0.00");
                        if (str.EndsWith("00") == true)
                        {
                            Fields["nSURYO"].Value = Double.Parse(dt_m_m.Rows[counter]["nSURYO"].ToString()).ToString("0");
                            Fields["nSURYO2"].Value = "";
                            LB_nSURYO.OutputFormat = "#,##0";

                        }
                        else
                        {
                            Fields["nSURYO"].Value = str.Substring(0, str.Length - 2).ToString();
                            Fields["nSURYO2"].Value = str.Substring(str.Length - 2).ToString();

                            if (Fields["nSURYO"].Value.ToString() == "-0")
                            {
                                 LB_nSURYO.OutputFormat = null;

                            }
                            else if (Fields["nSURYO2"].Value.ToString() != ".00")
                            {
                                Fields["nSURYO"].Value = str;
                                if (Fields["nSURYO2"].Value.ToString().EndsWith("0") == false)
                                {
                                     LB_nSURYO.OutputFormat = "#,##0.00";
                                }
                                else
                                {
                                     LB_nSURYO.OutputFormat = "#,##0.0";
                                }
                            }
                            else
                            {
                                Fields["nSURYO"].Value = str.Substring(0, str.Length - 2).ToString();
                                LB_nSURYO.OutputFormat = "#,##0";
                            }
                        }
                    }

                    Fields["sTANI"].Value = dt_m_m.Rows[counter]["sTANI"].ToString();

                    if (dt_m_m.Rows[counter]["nSIKIRITANKA"].ToString() == "0.00" || dt_m_m.Rows[counter]["nSIKIRITANKA"].ToString() == "0.0" || dt_m_m.Rows[counter]["nSIKIRITANKA"].ToString() == "0")
                    {
                        Fields["nSIKIRITANKA"].Value = "";
                    }
                    else
                    {
                        Fields["nSIKIRITANKA"].Value = dt_m_m.Rows[counter]["nSIKIRITANKA"].ToString();
                    }

                    if (dt_m_m.Rows[counter]["nSIKIRIKINGAKU"].ToString() == "0.0000" || dt_m_m.Rows[counter]["nSIKIRIKINGAKU"].ToString() == "0.000" || dt_m_m.Rows[counter]["nSIKIRIKINGAKU"].ToString() == "0.00" || dt_m_m.Rows[counter]["nSIKIRIKINGAKU"].ToString() == "0" || dt_m_m.Rows[counter]["nSIKIRIKINGAKU"].ToString() == "")                //提供金額  
                    {
                        Fields["nKINGAKU"].Value = "";
                    }
                    else
                    {
                        Fields["nKINGAKU"].Value = dt_m_m.Rows[counter]["nSIKIRIKINGAKU"].ToString();
                    }
                    if (!string.IsNullOrEmpty(dt_m_m.Rows[counter]["sSYOUHIN_R"].ToString()) && dt_m_m.Rows[counter]["sSYOUHIN_R"].ToString() != "追加行"
                                 //&& ((dbl_m_m.Rows[i]["sSYOUHIN_R"].ToString() != "小計" && dbl_m_m.Rows[i]["sSYOUHIN_R"].ToString() != "計") || dbl_m_m.Rows[i]["sKUBUN"].ToString() == "計"))
                                 && (dt_m_m.Rows[counter]["sSYOUHIN_R"].ToString() != "詳細計" && dt_m_m.Rows[counter]["sSYOUHIN_R"].ToString() != "計"
                            && dt_m_m.Rows[counter]["sKUBUN"].ToString() != "計") && dt_m_m.Rows[counter]["sKUBUN"].ToString() != "見") //商品名　区分は小計、見出し以外
                    {
                      LB_S_sNAIYOU.Alignment = GrapeCity.ActiveReports.Drawing.TextAlignment.Left;
                        line15.Visible = true;
                        line16.Visible = true;
                        line17.Visible = true;
                        line18.Visible = true;
                        line19.Visible = true;
                    }
                    else if (dt_m_m.Rows[counter]["sSYOUHIN_R"].ToString() == "計" || dt_m_m.Rows[counter]["sSYOUHIN_R"].ToString() == "詳細計" || dt_m_m.Rows[counter]["sSYOUHIN_R"].ToString() == "追加行" || dt_m_m.Rows[counter]["sKUBUN"].ToString() == "計" || dt_m_m.Rows[counter]["sKUBUN"].ToString() == "見")//計もしくは小計もしくは追加行
                    {
                        LB_S_sNAIYOU.Alignment = GrapeCity.ActiveReports.Drawing.TextAlignment.Right;
                        if (dt_m_m.Rows[counter]["sKUBUN"].ToString() == "計" || dt_m_m.Rows[counter]["sKUBUN"].ToString() == "見")
                        {
                            LB_S_sNAIYOU.Alignment = TextAlignment.Left;
                        }
                        line15.Visible = false;
                        line16.Visible = false;
                        line17.Visible = false;
                        line18.Visible = false;
                        line19.Visible = false;

                    }

                    #endregion

                    counter++;
                    eArgs.EOF = false;
                }
            }
            catch { eArgs.EOF = true; }
        }
        #endregion

        #region detail_Format
        private void detail_Format(object sender, EventArgs e)
        {
            if (dt_m_m.Rows.Count > 28)
            {
                if (counter >= (FIRSTMAXROWS + (PageNumber - 1) * MAXROWS) && counter != dt_m_m.Rows.Count)
                {
                    // Make new page and update record count.
                    detail.NewPage = NewPage.After;
                }
                else
                {
                    // No new page
                    detail.NewPage = NewPage.None;
                }
            }
            else
            {
                if (counter >= (25 + (PageNumber - 1) * 36) && counter != dt_m_m.Rows.Count)
                {
                    // Make new page and update record count.
                    detail.NewPage = NewPage.After;
                }
                else
                {
                    // No new page
                    detail.NewPage = NewPage.None;
                }
            }
        }
        #endregion

        #region reportHeader1_BeforePrint
        private void reportHeader1_BeforePrint(object sender, EventArgs e)
        {
            if (fINS == 0)//印刷
            {
                if (rmt_pcount1 > 0)
                {
                    this.LB_PAGE.Text = "(" + (rmt_pcount1 + k) + " / " + nPAGECOUNT.ToString() + ")";
                }
                else
                {
                    if (fHYOUSI == true)
                    {
                        this.LB_PAGE.Text = "(" + (rmt_pcount1 + k + 1) + " / " + nPAGECOUNT.ToString() + ")";
                    }
                    else
                    {
                        this.LB_PAGE.Text = "(" + (rmt_pcount1 + k) + " / " + nPAGECOUNT.ToString() + ")";
                    }
                }
                k++;
            }
            string s = clickedKyoten; //20220127 MyatNoe Added(kyoten preview)
            if (s == "kyoten")
            {
                this.LB_PAGE.Text = "(1 / 1)";
            }

            //else //プレピュー
            //{
            //    if (fHYOUSI == true)
            //    {
            //        this.LB_PAGE.Text = "(" + (FRM_SHIJI_VIEW.rmt_pcount + k + 1) + " / " + nPAGECOUNT.ToString() + ")";
            //    }
            //    else
            //    {
            //        this.LB_PAGE.Text = "(" + (FRM_SHIJI_VIEW.rmt_pcount + k) + " / " + nPAGECOUNT.ToString() + ")";
            //    }
            //    k++;


            //    if (fkyoten == true)
            //    {
            //        this.LB_PAGE.Text = "(1 / 1)";
            //    }

            //}
        }
        #endregion

        #region pageHeader_BeforePrint
        int k1 = 1;
        private void pageHeader_BeforePrint(object sender, EventArgs e)
        {
            if (PageNumber < 2)
            {
                //pageheader label start
                label28.Visible = false;//見積No.
                LB_PAGE1.Visible = false;
                LB_PAGE_cMITUMORI.Visible = false;
                LB_PAGE_dMITUMORISAKUSEI.Visible = false;
                //pageheader label end
            }
            else
            {
                //pageheader label start
                label28.Visible = true;//見積No.
                LB_PAGE1.Visible = true;
                LB_PAGE_cMITUMORI.Visible = true;
                LB_PAGE_dMITUMORISAKUSEI.Visible = true;
                //pageheader label end
            }

            if (nPAGECOUNT > 1)
            {
                if (fINS == 0)//印刷
                {
                    if ( rmt_pcount1 > 0)
                    {
                        LB_PAGE1.Text = "(" + (rmt_pcount1 + k1) + " / " + nPAGECOUNT.ToString() + ")";
                    }
                    else
                    {
                        if (fHYOUSI == true)
                        {
                            LB_PAGE1.Text = "(" + (rmt_pcount1 + k1 + 1) + " / " + nPAGECOUNT.ToString() + ")";
                        }
                        else
                        {
                            LB_PAGE1.Text = "(" + (rmt_pcount1 + k1) + " / " + nPAGECOUNT.ToString() + ")";
                        }
                    }
                    k1++;
                }
                //else //プレピュー
                //{
                //    if (fHYOUSI == true)
                //    {
                //        LB_PAGE1.Text = "(" + (FRM_SHIJI_VIEW.rmt_pcount + k1 + 1) + " / " + nPAGECOUNT.ToString() + ")";
                //    }
                //    else
                //    {
                //        LB_PAGE1.Text = "(" + (FRM_SHIJI_VIEW.rmt_pcount + k1) + " / " + nPAGECOUNT.ToString() + ")";
                //    }
                //    k1++;
                //}
            }
        }
        #endregion

        #region reportFooter1_Format
        private void reportFooter1_Format(object sender, EventArgs e)
        {
            //this.reportFooter1.KeepTogether = true;
            // this.reportFooter1.NewPage = NewPage.none;
            //this.reportFooter1.PrintAtBottom = true;
            //if (picture1.Visible == false && picture2.Visible == false)//20220505 maythu delete
            //{
            reportFooter1.Height = float.Parse("0");
            //}
        }
        #endregion

        #region pageFooter_BeforePrint
        int k = 1;
        private void pageFooter_BeforePrint(object sender, EventArgs e)
        {
            if (fINS == 0)//印刷
            {
                if (rmt_pcount1 > 0)
                {
                    this.LB_PAGE.Text = "(" + (rmt_pcount1 + k) + " / " + nPAGECOUNT.ToString() + ")";
                }
                else
                {
                    if (fHYOUSI == true)
                    {
                        this.LB_PAGE.Text = "(" + (rmt_pcount1 + k + 1) + " / " + nPAGECOUNT.ToString() + ")";
                    }
                    else
                    {
                        this.LB_PAGE.Text = "(" + (rmt_pcount1 + k) + " / " + nPAGECOUNT.ToString() + ")";
                    }
                }
                k++;
            }
            //else //プレピュー
            //{
            //    if (fHYOUSI == true)
            //    {
            //        this.LB_PAGE.Text = "(" + (FRM_SHIJI_VIEW.rmt_pcount + k + 1) + " / " + nPAGECOUNT.ToString() + ")";
            //    }
            //    else
            //    {
            //        this.LB_PAGE.Text = "(" + (FRM_SHIJI_VIEW.rmt_pcount + k) + " / " + nPAGECOUNT.ToString() + ")";
            //    }
            //    k++;
            //}
        }
        #endregion

        #region ChangeFont
        private void ChangeFont(System.Drawing.Font fond)
        {
            LB_S_sNAIYOU.Font = fond;
        }
        #endregion
    }
}
