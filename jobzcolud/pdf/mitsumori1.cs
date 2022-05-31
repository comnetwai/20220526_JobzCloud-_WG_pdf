using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Text;
using GrapeCity.ActiveReports;
using GrapeCity.ActiveReports.Drawing;
using GrapeCity.ActiveReports.SectionReportModel;
using Service;
using MySql.Data.MySqlClient;
using System.IO;

namespace jobzcolud.pdf
{
    /// <summary>
    /// mitsumori1 の概要の説明です。
    /// </summary>
    public partial class mitsumori1 : GrapeCity.ActiveReports.SectionReport
    {
        private string cHENKOUSYA /*= DEMO20BasicClass.StaticValue.cHENKOUSYA*/;
        public string cMITUMORI { private get; set; }       //見積コード
        public string cMITUMORI_KO { private get; set; }    //見積子番号
        public string sTANTOUSHA { private get; set; }    //担当者名
        public string imagecheck { private get; set; } 
        DataTable dbl_m = new DataTable();   //見積 表数据集
        //MoonLight2.DbLOAD dbl_m_m = new MoonLight2.DbLOAD(); //見積商品明細 表数据集

        DataTable dbl_m_m = new DataTable(); //見積商品明細 
        DataTable dbl_m_ms = new DataTable(); //見積商品明細 詳細サブ

        DataTable dbl_m_s_info = new DataTable(); //書類基本情報 表数据集,
        //private DEMO20BasicClass.Function func = new DEMO20BasicClass.Function();
        private int i = 0;
        public bool fKAKE = true;           //掛け率印刷フラグ
        
        //private const int MAXROWS = 25;     //每页显示的最大行数 
        private const int IMAGEROWS = 9;//画像所占行数
        private const int FIRSTMAXROWS = 28;//見積商品明細数据第一页显示的最大行数[25]
        private const int MAXROWS = 39;     //見積商品明細数据其它页显示的最大行数[36]
        public bool fHYOUSI = false;       //false不打印表紙 True打印表紙
        public bool fNEWIMAGE = false;     //true 最后一页只有图片
        public bool fRYOUHOU = false;// 詳細と一覧両方表示：1，詳細もしくは一覧表示：0
        public int nPAGECOUNT = 0;         //页码
        public int nPAGECOUNT_1 = 0;


        //---------------------------------------------------------------------------------
        double nMITUMORISYOHIZE = 0;    //消费税
        double nKINNGAKUKAZEI = 0;
        public bool fZEINUKIKINNGAKU = false; //税抜金額 
        public bool fZEIFUKUMUKIKINNGAKU = false;
        DataTable dbl_m_j_info = new DataTable();//自社情報マスター表 
        
        private System.Drawing.Font f1 = new System.Drawing.Font("ＭＳ 明朝", float.Parse("10"));
        private System.Drawing.Font f2 = new System.Drawing.Font("HG明朝B", float.Parse("12"));
        private System.Drawing.Font f3 = new System.Drawing.Font("HG明朝B", float.Parse("10"));
        private System.Drawing.Font f4 = new System.Drawing.Font("ＭＳ 明朝", float.Parse("12"));

        private System.Drawing.Font f6 = new System.Drawing.Font("ＭＳ 明朝", float.Parse("9"));
        private System.Drawing.Font f7 = new System.Drawing.Font("ＭＳ 明朝", float.Parse("9.75"));
        private System.Drawing.Font f8 = new System.Drawing.Font("ＭＳ 明朝", float.Parse("10.5"));

        //.......end.........
        //public DEMO20DataClass.r_mitumori_Class rm = new DEMO20DataClass.r_mitumori_Class();
        DataTable dbl_Min = new DataTable();

        public DataTable Syousai_All = new DataTable();//詳細データリストア 
        private DataTable Syousai_Temp1 = new DataTable();//詳細データリストア 
        private DataTable Syousai_Temp2 = new DataTable();//詳細データリストア
        private DataTable Syousai_Temp3 = new DataTable();//詳細データリストア 

        public bool fICHIRAN = false;// 一覧表示：1 
        public bool fSYOUSAI = false;// 詳細表示：1

        public bool fMIDASHI = false;//見出し表示

        public int NEWIMAGE = 0;
        public int fINS = 0;
        private int RowsCount = 0;

        public string HANKO_Check { private get; set; }

        private Boolean header_flag = false;
        //private MoonLight2.DbLOAD dbl_Min = new MoonLight2.DbLOAD();
        //............end..........

        public string frogoimage { private get; set; }
        public string ckyoten { private get; set; }

        public Boolean fkyoten = false;// 拠点マスタで設定してレイアウトを確認するため
        public System.Drawing.Image img { private get; set; }
        public Boolean size_dai { private get; set; }
        public string sTITLE { private get; set; }

        public string tokui_align { private get; set; }
        public string busyo_align { private get; set; }
        public string tantou_align { private get; set; }

        public string sSEIKYU_KEISYO { private get; set; } 
        public string sSEIKYU_YAKUSYOKU { private get; set; }
        public string sTOKUISAKI_TAN { private get; set; }

        public DataTable dt_meisai = new DataTable();

        MySqlConnection con = null;
        public string loginId = "";

        public DataTable dt_rm = new DataTable();

        Boolean flag_page1;

        Boolean fHIDZUKE;

        int pcount;
        public mitsumori1()
        {
            //
            // デザイナー サポートに必要なメソッドです。
            //
            InitializeComponent();

            #region Font
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
            #endregion
        }

        private void mitsumori1_ReportStart(object sender, EventArgs e)
        {
            #region ＭＳ 明朝

            LB_NO.Font = f8;
            LB_S_sNAIYOU.Font = f4;
            LB_nSURYO.Font = f8;
            LB_sTANI.Font = f8;
            LB_nSIKIRITANKA.Font = f8;
            LB_S_nKINGAKU1.Font = f8;

            #endregion
        
            if (fkyoten == true)
            {
                this.pageHeader.Visible = false;
                this.pageFooter.Visible = false;
                this.reportFooter1.Visible = false;
                this.detail.Visible = false;

                if (size_dai == true)
                {
                    P_sIMAGE2.Image = img;
                    P_sIMAGE2.PictureAlignment = PictureAlignment.TopLeft;

                    label10.Visible = false;
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

                    P_sIMAGE.Image = null;
                    LB_sTITLE.Text = string.Empty;
                }
                else
                {
                    P_sIMAGE.Image = img;
                    P_sIMAGE.PictureAlignment = PictureAlignment.TopLeft;

                    LB_sTITLE.Text = sTITLE;
                    if (LB_sTITLE.Text != "")
                    {
                        int index = -1;
                        int count = 0;
                        while (-1 != (index = LB_sTITLE.Text.IndexOf(Environment.NewLine, index + 1)))
                            count++;

                        if (count == 3)
                        {
                            LB_sTITLE.Top = 1.703937F;
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
                        show_hankou();
                    }
                }
            }
            else
            {
                string sql_m = ""; //用于查询“見積”表的sql文。
                string sql_m_m = ""; //用于查询“見積商品明細”表和“单位”表的sql文。

                string sql_m_ms = ""; //詳細データ取得する為に使う

                string sql_m_s_info = "";//用于查询“書類基本情報”表的sql文。
                string sql_m_j_info = "";//自社情報マスター表
                DataTable db1 = new DataTable();
                DataTable db2 = new DataTable();
                DataTable db3 = new DataTable();//得意先フラグのため

                sql_m_s_info += "SELECT ";
                sql_m_s_info += "sMITUMORI as sMITUMORI, ";
                sql_m_s_info += "sNAIYOU as sNAIYOU, ";
                sql_m_s_info += "sKEN as sKEN, ";
                sql_m_s_info += "sNOUKI as sNOUKI, ";
                sql_m_s_info += "sYUUKOU as sYUUKOU, ";
                sql_m_s_info += "sSHIHARAI as sSHIHARAI, ";
                sql_m_s_info += "sUKEBASYOU as sUKEBASYOU, ";
                //画像印刷（当最后一页数据大于28(首页16）行时，加载下一页）: 1补全27行空白行画像在最低部显示；2画像另一页顶部显示不添加空白行
                sql_m_s_info += "IFNULL(fIMAGE,'1') AS fIMAGE ";

                sql_m_s_info += ",sAisatsu as sAisatsu";
                sql_m_s_info += ",sZeinu as sZeinu ";
                //--------------------------------------------------

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

                sql_m_j_info += " FROM M_J_INFO";

                JC_ClientConnecction_Class jc = new JC_ClientConnecction_Class();
                jc.loginId = loginId;
                con = jc.GetConnection();


                if (fINS == 0)
                {
                    getMitumoriData(this.cMITUMORI);
                }

                sql_m_j_info += " WHERE cco ='" + ckyoten.ToString() + "'";

                con.Open();
                MySqlCommand cmd_s_info = new MySqlCommand(sql_m_s_info, con);
                cmd_s_info.CommandTimeout = 0;
                MySqlDataAdapter da_s_info = new MySqlDataAdapter(cmd_s_info);
                da_s_info.Fill(dbl_m_s_info);
                da_s_info.Dispose();

                MySqlCommand cmd_j_info = new MySqlCommand(sql_m_j_info, con);
                cmd_j_info.CommandTimeout = 0;
                MySqlDataAdapter da_j_info = new MySqlDataAdapter(cmd_j_info);
                da_j_info.Fill(dbl_m_j_info);
                da_j_info.Dispose();

                con.Close();

                if (fINS == 0)
                {
                    try
                    {
                        #region 詳細の場合は画像を表示しない
                        
                        if (fSYOUSAI == true)
                        {
                            picture1.Visible = false;
                            picture2.Visible = false;
                            fNEWIMAGE = false;
                        }

                        #endregion

                        LB_cMITUMORI.Text = cMITUMORI;  //报表 右上角：見積No.赋值
                        LB_PAGE_cMITUMORI.Text = cMITUMORI;
                        sql_m += "select distinct ";
                        sql_m += "T.sTOKUISAKI as sTOKUISAKI, ";                 //得意先名
                        sql_m += "(CASE WHEN (T.sMITUMORIKENMEI is not null AND T.sMITUMORIKENMEI<>'') THEN T.sMITUMORIKENMEI ELSE T.sMITUMORI END) as sMITUMORI1, ";  //件名
                        sql_m += "T.sMITUMORINOKI as dMITUMORINOKI, ";           //納期              
                        sql_m += "T.sMITUMORIYUKOKIGEN as sMITUMORIYUKOKIGEN, "; //有効期限
                        sql_m += "K.sSHIHARAI as sSHIHARAI, ";                   //支払条件
                        sql_m += "T.sUKEWATASIBASYO as sUKEWATASIBASYO, ";       //受渡し場所   
                        sql_m += "T.sBIKOU as sBIKOU, ";                         //備考       
                        sql_m += "T.nMITUMORISYOHIZE as nMITUMORISYOHIZE, ";     //消費税 
                        sql_m += "T.nTOKUISAKIKAKERITU as nTOKUISAKIKAKERITU, "; //得意先の掛率 
                        sql_m += "T.nMITUMORINEBIKI as nMITUMORINEBIKI, ";       //値引き
                        sql_m += "CASE WHEN T.fSAKUSEISYA='1' THEN S2.sTANTOUSHA ELSE S.sTANTOUSHA END as sTANTOUSHA ,"; //担当者名  作成者か営業者をフラグによって表示
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
                        sql_m += ",T.nKAZEIKINGAKU as nKAZEIKINGAKU ";     //kazeikingaku
                        sql_m += ",IFNULL(T.sTOKUISAKI_YAKUSHOKU,'') as sTOKUISAKI_YAKUSHOKU ";//得意先担当者役職

                        sql_m += ",IFNULL(T.sTOKUISAKI_KEISYO,'') as SKEISHOU ";

                        sql_m += ",CASE WHEN T.fSAKUSEISYA='1' THEN S2.sIMAGE1 ELSE S.sIMAGE1 END AS sIMAGE1 ";
                        sql_m += "from r_mitumori T ";                           //从“見積 "表中查询
                        sql_m += "left join m_shiharai K on T.cSHIHARAI = K.cSHIHARAI ";
                        sql_m += "left join m_j_tantousha S on T.cEIGYOTANTOSYA = S.cTANTOUSHA ";  //営業担当者CODE 和 担当者コード 连接
                        sql_m += "left join m_j_tantousha S2 on T.cSAKUSEISYA = S2.cTANTOUSHA ";
                        sql_m += "left join m_tokuisaki MT on T.cTOKUISAKI=MT.cTOKUISAKI ";  //得意先マスターから得意先様、御中フラグを取るため

                        sql_m += "where 1=1 ";
                        sql_m += "and T.cMITUMORI='" + cMITUMORI + "' ";         //見積コード

                        if (fMIDASHI == false)
                        {
                            //提供金額＝単価＊数量＊掛け率
                            sql_m_m += "select distinct ";
                            sql_m_m += "T.rowNO as rowNO, ";                    //Index行
                            sql_m_m += "T.nGYOUNO as nGYOUNO, ";                    //行NO
                            sql_m_m += "T.nINSATSU_GYO as nINSATSU_GYO, ";          //印刷行NO
                            sql_m_m += "IF(T.sSYOUHIN_R IS NULL OR T.sSYOUHIN_R='','追加行1',sSYOUHIN_R) as sSYOUHIN_R, ";//内容.仕様----商品名
                            sql_m_m += "T.nSURYO as nSURYO, ";                      //数量  
                            sql_m_m += "T.sTANI as sTANI, ";                        //単位 
                            sql_m_m += "T.nTANKA as nTANKA, ";                      //単価
                            sql_m_m += "T.nKINGAKU as nKINGAKU, ";                  //提供金額
                            sql_m_m += "FORMAT(T.nRITU,0) as nRITU, ";              //掛け率
                            sql_m_m += "IF((T.sKUBUN='見' AND T.sKUBUN is not null),'',T.nSIKIRIKINGAKU) as nSIKIRIKINGAKU, ";      //仕切金額
                            sql_m_m += "T.nSIKIRITANKA as nSIKIRITANKA, ";           //仕切単価
                            sql_m_m += "T.sSETSUMUI AS sSETSUMUI, ";
                            sql_m_m += "IFNULL(T.fJITAIS,0) AS fJITAIS, "; 
                            sql_m_m += "IFNULL(T.fJITAIQ,0) AS fJITAIQ "; 
                            sql_m_m += ",IFNULL(T.sKUBUN,'') AS sKUBUN ";           //区分
                            sql_m_m += "from r_mitumori_m T ";                      //从“見積商品明細 "表中查询
                            sql_m_m += "where 1=1 ";
                            sql_m_m += "and T.cMITUMORI='" + cMITUMORI + "' ";      //見積コード
                            //sql_m_m += "and T.cMITUMORI_KO='" + cMITUMORI_KO + "' ";//見積子番号
                            sql_m_m += " order by T.nGYOUNO ";
                        }
                        else
                        {
                            //提供金額＝単価＊数量＊掛け率
                            sql_m_m += "select distinct ";
                            sql_m_m += "T.rowNO as rowNO, ";                    //Index行
                            sql_m_m += "T.nGYOUNO as nGYOUNO, ";                    //行NO
                            sql_m_m += "T.nINSATSU_GYO as nINSATSU_GYO, ";          //印刷行NO
                            sql_m_m += "IF(T.sSYOUHIN_R IS NULL OR T.sSYOUHIN_R='','追加行1',sSYOUHIN_R) as sSYOUHIN_R, ";//内容.仕様----商品名
                            
                            sql_m_m += "IF((T.sKUBUN='見' AND T.sKUBUN is not null),'1',T.nSURYO) as nSURYO, ";   //数量  
                            sql_m_m += "IF((T.sKUBUN='見' AND T.sKUBUN is not null),'式',T.sTANI) as sTANI, ";     //単位 

                            sql_m_m += "T.nTANKA as nTANKA, ";                      //単価
                            sql_m_m += "T.nKINGAKU as nKINGAKU, ";                  //提供金額
                            sql_m_m += "FORMAT(T.nRITU,0) as nRITU, ";              //掛け率
                            sql_m_m += "T.nSIKIRIKINGAKU as nSIKIRIKINGAKU, ";      //仕切金額
                            sql_m_m += "T.nSIKIRITANKA as nSIKIRITANKA, ";           //仕切単価
                            sql_m_m += "T.sSETSUMUI AS sSETSUMUI, ";
                            sql_m_m += "IFNULL(T.fJITAIS,0) AS fJITAIS, ";
                            sql_m_m += "IFNULL(T.fJITAIQ,0) AS fJITAIQ ";
                            sql_m_m += ",IFNULL(T.sKUBUN,'') AS sKUBUN ";           //区分
                            sql_m_m += "from r_mitumori_m T ";                      //从“見積商品明細 "表中查询
                            sql_m_m += "where 1=1 ";
                            sql_m_m += "and T.cMITUMORI='" + cMITUMORI + "' ";      //見積コード
                            //sql_m_m += "and T.cMITUMORI_KO='" + cMITUMORI_KO + "' ";//見積子番号
                            //sql_m_m += " AND (T.sKUBUN='見' OR T.sKUBUN='計') order by T.nGYOUNO ";//見出し行と小計行両方表示する
                            //sql_m_m += " AND ((T.sKUBUN<>'計' AND (T.sKUBUN='見' OR T.sKUBUN<>'間') AND T.sKUBUN is not null) OR T.sKUBUN is null) order by T.nGYOUNO ";//見出し行のみ、見出しと小計に挟まれていない行を表示する
                            sql_m_m += " AND (T.sKUBUN<>'計') order by T.nGYOUNO ";
                        }


                        #region 見積詳細関係

                        sql_m_ms += "select distinct ";
                        sql_m_ms += "T.rowNO as rowNO, ";                    //行
                        sql_m_ms += "T.nGYOUNO as nGYOUNO, ";                    //行NO
                        sql_m_ms += "T.nINSATSU_GYO as nINSATSU_GYO, ";          //印刷行NO
                        sql_m_ms += "T.sSYOUHIN_R as sSYOUHIN_R, ";              //内容.仕様----商品名 
                        sql_m_ms += "T.nSURYO as nSURYO, ";                      //数量  
                        sql_m_ms += "T.sTANI as sTANI, ";                        //単位
                        sql_m_ms += "T.nTANKA as nTANKA, ";                      //単価
                        sql_m_ms += "T.nKINGAKU as nKINGAKU, ";                  //提供金額
                        sql_m_ms += "FORMAT(T.nRITU,0) as nRITU, ";              //掛け率
                        sql_m_ms += "T.nSIKIRIKINGAKU as nSIKIRIKINGAKU, ";      //仕切金額
                        sql_m_ms += "T.nSIKIRITANKA as nSIKIRITANKA, ";           //仕切単価
                        sql_m_ms += "'' AS sSETSUMUI, ";
                        sql_m_ms += "IFNULL(T.fJITAIS,0) AS fJITAIS, ";
                        sql_m_ms += "IFNULL(T.fJITAIQ,0) AS fJITAIQ ";
                        sql_m_ms += ",'' AS sKUBUN ";           //区分
                        sql_m_ms += "from r_mitumori_m2 T ";                      //从“見積商品明細 "表中查询
                        sql_m_ms += "where 1=1 ";
                        sql_m_ms += "and T.cMITUMORI='" + cMITUMORI + "' ";      //見積コード
                        //sql_m_ms += "and T.cMITUMORI_KO='" + cMITUMORI_KO + "' ";//見積子番号
                        sql_m_ms += "and T.sSYOUHIN_R<>'' order by T.rowNO,T.nGYOUNO ";

                        #endregion
                        
                        if (con != null)
                        {
                            con.Open();
                            MySqlCommand cmd_m = new MySqlCommand(sql_m, con);
                            cmd_m.CommandTimeout = 0;
                            MySqlDataAdapter da_m = new MySqlDataAdapter(cmd_m);
                            da_m.Fill(dbl_m);
                            da_m.Dispose();

                            MySqlCommand cmd_m_m = new MySqlCommand(sql_m_m, con);
                            cmd_m_m.CommandTimeout = 0;
                            MySqlDataAdapter da_m_m = new MySqlDataAdapter(cmd_m_m);
                            da_m_m.Fill(dbl_m_m);
                            da_m_m.Dispose();
                            con.Close();

                            //dbl_m.Autoitem(sql_m, "r_mitumori", DEMO20BasicClass.DBConnector.conn);
                            //dbl_m_m.Autoitem(sql_m_m, "r_mitumori_m", DEMO20BasicClass.DBConnector.conn);

                            #region
                            if (fMIDASHI == true)
                            {
                                int rindex_meisai_start = 1;
                                for (int rindex = 0; rindex < dbl_m_m.Rows.Count; rindex++)
                                {
                                    if (dbl_m_m.Rows[rindex]["sKUBUN"].ToString() != "見" && dbl_m_m.Rows[rindex]["nSURYO"].ToString() != "0.00" && dbl_m_m.Rows[rindex]["nSURYO"].ToString() != "")
                                    {
                                        dbl_m_m.Rows[rindex]["nINSATSU_GYO"] = rindex_meisai_start;
                                        rindex_meisai_start++;
                                    }
                                }
                                dbl_m_m = dbl_m_m.Select("(sKUBUN<>'計' AND (sKUBUN='見' OR sKUBUN<>'間') AND sKUBUN is not null) OR sKUBUN is null ", "nGYOUNO asc").CopyToDataTable();
                            }
                            #endregion

                            #region 見積詳細関係

                            if (fICHIRAN == false && fSYOUSAI == true)
                            {
                                Boolean Row_add = false;//行追加flag

                                //Table initialize
                                Syousai_Temp1 = new DataTable();
                                Syousai_Temp2 = new DataTable();
                                Syousai_Temp3 = new DataTable();

                                sql_m_m = string.Empty;
                                //
                                //提供金額＝単価＊数量＊掛け率
                                sql_m_m += "select distinct ";
                                sql_m_m += "T.rowNO as rowNO, ";                    //Index行
                                sql_m_m += "T.nGYOUNO as nGYOUNO, ";                    //行NO
                                sql_m_m += "T.nINSATSU_GYO as nINSATSU_GYO, ";          //印刷行NO
                                sql_m_m += "T.sSYOUHIN_R as sSYOUHIN_R, ";              //内容.仕様----商品名 
                                sql_m_m += "T.nSURYO as nSURYO, ";                      //数量  
                                sql_m_m += "T.sTANI as sTANI, ";                        //単位
                                sql_m_m += "T.nTANKA as nTANKA, ";                      //単価
                                sql_m_m += "T.nKINGAKU as nKINGAKU, ";                  //提供金額
                                sql_m_m += "FORMAT(T.nRITU,0) as nRITU, ";              //掛け率
                                sql_m_m += "T.nSIKIRIKINGAKU as nSIKIRIKINGAKU, ";      //仕切金額
                                sql_m_m += "T.nSIKIRITANKA as nSIKIRITANKA, ";           //仕切単価
                                sql_m_m += "T.sSETSUMUI AS sSETSUMUI, ";
                                sql_m_m += "IFNULL(T.fJITAIS,0) AS fJITAIS, "; 
                                sql_m_m += "IFNULL(T.fJITAIQ,0) AS fJITAIQ ";  
                                sql_m_m += ",'' AS sKUBUN ";           //区分
                                sql_m_m += "from r_mitumori_m T ";                      //从“見積商品明細 "表中查询
                                sql_m_m += "where 1=1 ";
                                sql_m_m += "and T.cMITUMORI='" + cMITUMORI + "' ";      //見積コード
                                //sql_m_m += "and T.cMITUMORI_KO='" + cMITUMORI_KO + "' ";//見積子番号
                                //sql_m_m += "and T.sSYOUHIN_R<>'' AND (T.sKUBUN<>'見' AND T.sKUBUN<>'計') order by T.nGYOUNO ";
                                sql_m_m += "and T.sSYOUHIN_R<>''";
                                sql_m_m += " AND ((T.sKUBUN<>'見' AND T.sKUBUN<>'計') || T.sKUBUN is null)";
                                sql_m_m += " order by T.nGYOUNO ";

                                //

                                con.Open();

                                MySqlCommand cmd_Temp1 = new MySqlCommand(sql_m_m, con);
                                cmd_Temp1.CommandTimeout = 0;
                                MySqlDataAdapter da_Temp1 = new MySqlDataAdapter(cmd_Temp1);
                                da_Temp1.Fill(Syousai_Temp1);  //Temp テブルに見積画面の詳細データ登録
                                da_Temp1.Dispose();

                                MySqlCommand cmd_Syousai_All = new MySqlCommand(sql_m_ms, con);
                                cmd_Syousai_All.CommandTimeout = 0;
                                MySqlDataAdapter da_Syousai_All = new MySqlDataAdapter(cmd_Syousai_All);
                                da_Syousai_All.Fill(Syousai_All);//Temp テブルに見積詳細画面の詳細データ登録
                                da_Syousai_All.Dispose();

                                MySqlCommand cmd_Temp2 = new MySqlCommand(sql_m_ms, con);
                                cmd_Temp2.CommandTimeout = 0;
                                MySqlDataAdapter da_Temp2 = new MySqlDataAdapter(cmd_Temp2);
                                da_Temp2.Fill(Syousai_Temp2);//Temp テブルに項目設定
                                da_Temp2.Dispose();

                                con.Close();

                                //Syousai_Temp1.Autoitem(sql_m_m, "r_mitumori_m", DEMO20BasicClass.DBConnector.conn);
                                //Syousai_All.Autoitem(sql_m_ms, "r_mitumori_m2", DEMO20BasicClass.DBConnector.conn);

                                //Syousai_Temp2.Autoitem(sql_m_ms, "r_mitumori_m2", DEMO20BasicClass.DBConnector.conn);

                                Syousai_Temp2.Clear();

                                //Syousai_Temp1 = Syousai_Temp1.Set_DataFind("sSYOUHIN_R<>''");//見積詳細表から空行以外の行を

                                int o = Syousai_Temp1.Rows.Count;//一覧のcount
                                int j = 0, m = 0;
                                int rowno = 1;

                                for (int kk = 0; kk < o; kk++)
                                {
                                    //if (Syousai_Temp1.Rows[kk]["sSYOUHIN_R"].ToString() != "")
                                    //{

                                    if (m > 0)//上の空行
                                    {

                                        Syousai_Temp3 = Syousai_All.Select("rowNO='" + Syousai_Temp1.Rows[kk]["rowNO"].ToString() + "' and sSYOUHIN_R<>''", "nGYOUNO ASC").CopyToDataTable();

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
                                            Syousai_Temp3 = Syousai_All.Select("rowNO='" + Syousai_Temp1.Rows[kk - 1]["rowNO"].ToString() + "' and sSYOUHIN_R<>''", "nGYOUNO ASC").CopyToDataTable();

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


                                    //一覧行の値によって詳細データを取得して登録
                                    Syousai_Temp3 = Syousai_All.Select("rowNO='" + Syousai_Temp1.Rows[kk]["rowNO"].ToString() + "' and sSYOUHIN_R<>''", "nGYOUNO ASC").CopyToDataTable();

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
                                    dbl_m_m = Syousai_Temp2;
                                }

                            }
                            if (fSYOUSAI == true || fRYOUHOU == true)
                            {
                                //if (dbl_m_m.Rows[dbl_m_m.Rows.Count - 1]["sSYOUHIN_R"].ToString() == "小計")
                                if (dbl_m_m.Rows[dbl_m_m.Rows.Count - 1]["sSYOUHIN_R"].ToString() == "詳細計")
                                {
                                    dbl_m_m.Rows.Add(dbl_m_m.NewRow());
                                    dbl_m_m.AcceptChanges();

                                    dbl_m_m.Rows[dbl_m_m.Rows.Count - 1]["sSYOUHIN_R"] = "追加行";
                                    dbl_m_m.Rows[dbl_m_m.Rows.Count - 1]["nINSATSU_GYO"] = "0";
                                }
                            }

                            #endregion


                            #region
                            if (dbl_m_m.Rows.Count <= 25)
                            {
                                if (dbl_m_m.Rows.Count <= 16)
                                {
                                    nPAGECOUNT = 1;

                                    nPAGECOUNT_1 = 1;
                                }
                                else
                                {
                                    if (picture1.Visible == false && picture2.Visible == false && fNEWIMAGE == false)
                                    {
                                        nPAGECOUNT = 1;

                                        nPAGECOUNT_1 = 1;
                                    }
                                    else
                                    {
                                        nPAGECOUNT = 2;

                                        nPAGECOUNT_1 = 2;
                                        header_flag = true;

                                    }
                                }

                            }
                            else
                            {
                                int n = 0;
                                if (dbl_m_m.Rows.Count == 28)
                                {
                                    n = 0;
                                }
                                else
                                {
                                    if ((dbl_m_m.Rows.Count - 28) % 39 != 0)
                                    {
                                        n = (dbl_m_m.Rows.Count - 28) / 39;
                                    }
                                    else
                                    {
                                        n = (dbl_m_m.Rows.Count - 28) / 39;
                                        n = n - 1;
                                    }
                                }
                                for (int i = 0; i < n + 1; i++)
                                {
                                    if ((dbl_m_m.Rows.Count - 28) - i * 39 <= 39)
                                    {
                                        if ((dbl_m_m.Rows.Count - 28) - i * 39 <= 27)
                                        {
                                            if (picture1.Visible == false && picture2.Visible == false && fNEWIMAGE == false)
                                            {
                                                //if ((dbl_m_m.Rows.Count - 28) - i * 39 == 0)//表示できるページまでは
                                                //{
                                                //    nPAGECOUNT = i + 1;

                                                //    nPAGECOUNT_1 = i + 1;
                                                //}
                                                //else
                                                //{
                                                nPAGECOUNT = i + 2;

                                                //20170412 WaiWai add start
                                                nPAGECOUNT_1 = i + 2;
                                                //}

                                                //20170412 WaiWAi add end
                                            }
                                            else
                                            {
                                                nPAGECOUNT = i + 2;

                                                //20170412 WaiWai add start
                                                nPAGECOUNT_1 = i + 2;
                                            }
                                        }
                                        else
                                        {
                                            if (picture1.Visible == false && picture2.Visible == false && fNEWIMAGE == false)
                                            {
                                                nPAGECOUNT = i + 2;

                                                //20170412 WaiWai add start
                                                nPAGECOUNT_1 = i + 2;

                                                //20170412 WaiWai add end
                                            }
                                            else
                                            {
                                                nPAGECOUNT = i + 3;

                                                //20170412 WaiWai add start
                                                nPAGECOUNT_1 = i + 3;

                                                header_flag = true;

                                                //20170412 WaiWai add end
                                            }
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

                            if (dbl_m.Rows.Count > 0)
                            {
                                Fields["sTOKUISAKI"].Value = dbl_m.Rows[0][0].ToString();         //得意先名
                                Fields["sMITUMORI1"].Value = dbl_m.Rows[0][1].ToString();         //件名
                                Fields["dMITUMORINOKI"].Value = dbl_m.Rows[0][2].ToString();

                                if (dbl_m.Rows[0][3].ToString().Trim() != "選択してください")
                                {
                                    Fields["sMITUMORIYUKOKIGEN"].Value = dbl_m.Rows[0][3].ToString(); //有効期限
                                }

                                Fields["cSHIHARAI"].Value = dbl_m.Rows[0][4].ToString();          //支払条件

                                if (dbl_m.Rows[0][5].ToString().Trim() != "選択してください")
                                {
                                    Fields["sUKEWATASIBASYO"].Value = dbl_m.Rows[0][5].ToString();    //受渡し場所
                                }

                                nMITUMORISYOHIZE = Convert.ToDouble(dbl_m.Rows[0][7].ToString());   //消費税
                                nKINNGAKUKAZEI = Convert.ToDouble(dbl_m.Rows[0]["nKAZEIKINGAKU"].ToString());   //kazeikingaku
                                if (dbl_m.Rows[0][10].ToString() != "")// 担当者はnullの場合は表示しない
                                {
                                    string space_dai = string.Empty;
                                    if (dbl_m_j_info.Rows[0]["sIMAGE"].ToString() != "")
                                    {
                                        if (dbl_m_j_info.Rows[0]["fIMAGESize"].ToString() != "0")
                                        {
                                            space_dai = "　　　　　　　　　　　　　";
                                        }
                                    }

                                    Fields["sTANTOUSHA"].Value = space_dai + "担当:" + dbl_m.Rows[0][10].ToString(); //担当者名
                                }

                                //Fields["sTOKUISAKI_TAN"].Value = dbl_m.Tables[0].Rows[0][11].ToString();    //得意先担当者
                                //Fields["sTOKUISAKI_TANBUMON"].Value = dbl_m.Tables[0].Rows[0]["sTOKUISAKI_TANBUMON"].ToString();//得意先担当者部門
                                
                                if (dbl_m.Rows[0]["sTOKUISAKI_TAN"].ToString() != "")
                                {
                                    if (dbl_m.Rows[0]["SKEISHOU"].ToString() == "")
                                    {
                                        dbl_m.Rows[0]["SKEISHOU"] = " 様";
                                    }
                                    //Fields["sTOKUISAKI_TAN"].Value = dbl_m.Tables[0].Rows[0][11].ToString();      //得意先担当者
                                    
                                    if (dbl_m.Rows[0]["sTOKUISAKI_YAKUSHOKU"].ToString() == "")
                                    {
                                        //Fields["sTOKUISAKI_TAN"].Value = dbl_m.Tables[0].Rows[0][11].ToString() + " 様";      //得意先担当者
                                        Fields["sTOKUISAKI_TAN"].Value = dbl_m.Rows[0][11].ToString() + " " + dbl_m.Rows[0]["SKEISHOU"].ToString();      //得意先担当者
                                    }
                                    else
                                    {
                                        //Fields["sTOKUISAKI_TAN"].Value = dbl_m.Tables[0].Rows[0]["sTOKUISAKI_YAKUSHOKU"].ToString() + " " + dbl_m.Tables[0].Rows[0][11].ToString() + " 様";      //得意先担当者
                                        Fields["sTOKUISAKI_TAN"].Value = dbl_m.Rows[0]["sTOKUISAKI_YAKUSHOKU"].ToString() + " " + dbl_m.Rows[0][11].ToString() + " " + dbl_m.Rows[0]["SKEISHOU"].ToString();      //得意先担当者
                                    }


                                    //20190305 WaiWai add start
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
                                    //20190305 WaiWai add end

                                    LB_sTOKUISAKI_TANBUMON.Visible = true;
                                    LB_sTOKUISAKI_TAN1.Visible = true;
                                    //label131.Visible = true;20190524 WaiWai delete
                                    LB_sTOKUISAKI_TAN.Visible = true;
                                    //label13.Visible = true;20190524 WaiWai delete

                                    label1.Border.BottomStyle = BorderLineStyle.None;
                                    LB_sTOKUISAKI.Border.BottomStyle = BorderLineStyle.None;
                                    LB_sTOKUISAKI_TANBUMON.Border.BottomStyle = BorderLineStyle.None;
                                    LB_sTOKUISAKI_TAN1.Border.BottomStyle = BorderLineStyle.None;
                                    label131.Border.BottomStyle = BorderLineStyle.None;
                                    LB_sTOKUISAKI_TAN.Border.BottomStyle = BorderLineStyle.Solid;
                                    label13.Border.BottomStyle = BorderLineStyle.Solid;

                                    if (dbl_m.Rows[0]["sTOKUISAKI_TANBUMON"].ToString() != "")
                                    {
                                        Fields["sTOKUISAKI_TANBUMON"].Value = dbl_m.Rows[0]["sTOKUISAKI_TANBUMON"].ToString(); ;//得意先担当者部門
                                        
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

                                        LB_sTOKUISAKI_TANBUMON.Visible = true;
                                        LB_sTOKUISAKI_TAN1.Visible = false;
                                        LB_sTOKUISAKI_TAN.Visible = true;

                                        label1.Border.BottomStyle = BorderLineStyle.None;
                                        LB_sTOKUISAKI.Border.BottomStyle = BorderLineStyle.None;
                                        LB_sTOKUISAKI_TANBUMON.Border.BottomStyle = BorderLineStyle.None;
                                        LB_sTOKUISAKI_TAN1.Border.BottomStyle = BorderLineStyle.None;
                                        label131.Border.BottomStyle = BorderLineStyle.None;
                                        LB_sTOKUISAKI_TAN.Border.BottomStyle = BorderLineStyle.Solid;
                                        label13.Border.BottomStyle = BorderLineStyle.Solid;
                                    }
                                    else
                                    {
                                        LB_sTOKUISAKI_TANBUMON.Visible = false;
                                        LB_sTOKUISAKI_TAN1.Visible = true;
                                        LB_sTOKUISAKI_TAN.Visible = false;

                                        label1.Border.BottomStyle = BorderLineStyle.None;
                                        LB_sTOKUISAKI.Border.BottomStyle = BorderLineStyle.None;
                                        LB_sTOKUISAKI_TANBUMON.Border.BottomStyle = BorderLineStyle.None;
                                        LB_sTOKUISAKI_TAN1.Border.BottomStyle = BorderLineStyle.Solid;
                                        label131.Border.BottomStyle = BorderLineStyle.Solid;
                                        LB_sTOKUISAKI_TAN.Border.BottomStyle = BorderLineStyle.None;
                                        label13.Border.BottomStyle = BorderLineStyle.None;
                                    }
                                }
                                else
                                {
                                    LB_sTOKUISAKI_TANBUMON.Visible = false;
                                    LB_sTOKUISAKI_TAN1.Visible = false;
                                    LB_sTOKUISAKI_TAN.Visible = false;

                                    label1.Border.BottomStyle = BorderLineStyle.Solid;
                                    LB_sTOKUISAKI.Border.BottomStyle = BorderLineStyle.Solid;
                                    LB_sTOKUISAKI_TANBUMON.Border.BottomStyle = BorderLineStyle.None;
                                    LB_sTOKUISAKI_TAN1.Border.BottomStyle = BorderLineStyle.None;
                                    label131.Border.BottomStyle = BorderLineStyle.None;
                                    LB_sTOKUISAKI_TAN.Border.BottomStyle = BorderLineStyle.None;
                                    label13.Border.BottomStyle = BorderLineStyle.None;

                                }
                                
                                
                                if (dbl_m.Rows[0][11].ToString() == "")
                                {
                                    label13.Text = "";

                                    Fields["fSAMA"].Value = dbl_m.Rows[0]["fSAMA"].ToString();//得意先フラグ
                                    if (!string.IsNullOrEmpty(Fields["fSAMA"].Value.ToString()))
                                    {
                                        //Fields["sTOKUISAKI"].Value = Fields["sTOKUISAKI"].Value + "　" + Fields["fSAMA"].Value;

                                        if (!Fields["sTOKUISAKI"].Value.ToString().Contains("\r\n") && !Fields["sTOKUISAKI"].Value.ToString().Contains("\n"))
                                        {
                                            int cgetbyte = getbyte(Fields["sTOKUISAKI"].Value.ToString());
                                            if (cgetbyte == 32 || cgetbyte == 33)
                                            {
                                                Fields["sTOKUISAKI"].Value = Fields["sTOKUISAKI"].Value + "　　" + Fields["fSAMA"].Value;
                                            }
                                            else
                                            {
                                                Fields["sTOKUISAKI"].Value = Fields["sTOKUISAKI"].Value + "　" + Fields["fSAMA"].Value;
                                            }
                                        }
                                        else
                                        {
                                            Fields["sTOKUISAKI"].Value = Fields["sTOKUISAKI"].Value + "　" + Fields["fSAMA"].Value;
                                        }
                                    }
                                    Fields["fSAMA"].Value = "";
                                }
                                else
                                {
                                    label1.Text = "";
                                }

                                Fields["sBIKOU"].Value = dbl_m.Rows[0][6].ToString();    //備考
                                
                                if (dbl_m.Rows[0]["dMITUMORISAKUSEI"].ToString() != "")
                                {
                                    LB_dMITUMORISAKUSEI.Text = dbl_m.Rows[0]["dMITUMORISAKUSEI"].ToString().Substring(0, 10);  //作成日
                                }
                                if (dbl_m.Rows[0]["dMITUMORISAKUSEI"].ToString() != "")
                                {
                                    LB_PAGE_dMITUMORISAKUSEI.Text = dbl_m.Rows[0]["dMITUMORISAKUSEI"].ToString().Substring(0, 10);
                                }

                                if (HANKO_Check == "欄無し")
                                {
                                    lb_hanko1.Visible = false;
                                    lb_hanko2.Visible = false;
                                    lb_hanko3.Visible = false;

                                    if (dbl_m.Rows[0]["sMAIL"].ToString() != "")
                                    {
                                        Fields["sMAIL"].Value = "MAIL:" + dbl_m.Rows[0]["sMAIL"].ToString();//メールアドレス
                                    }
                                    else
                                    {
                                        if (dbl_m_j_info.Rows.Count > 0)
                                        {
                                            //担当者のメールアドレスがnullの場合は代表メールアドレスを表示
                                            if (dbl_m_j_info.Rows[0]["sMAIL"].ToString() != "")
                                            {
                                                Fields["sMAIL"].Value = "MAIL:" + dbl_m_j_info.Rows[0]["sMAIL"].ToString();
                                            }
                                        }
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
                                        if (dbl_m.Rows.Count > 0)
                                        {
                                            if (dbl_m.Rows[0]["sIMAGE1"] != null)
                                            {
                                                if (!string.IsNullOrEmpty(dbl_m.Rows[0]["sIMAGE1"].ToString()))
                                                {
                                                    byte[] bytes = (byte[])dbl_m.Rows[0]["sIMAGE1"];
                                                    MemoryStream stream = new MemoryStream(bytes);
                                                    P_sIMAGEHankou.Image = System.Drawing.Image.FromStream(stream);
                                                    P_sIMAGEHankou.PictureAlignment = PictureAlignment.Center;

                                                    //P_sIMAGEHankou.Image = Image.FromStream(func.toImage((byte[])dbl_m.Rows[0]["sIMAGE1"]));
                                                    //P_sIMAGEHankou.PictureAlignment = PictureAlignment.Center;
                                                }
                                            }
                                        }
                                    }
                                }

                                //==============20130723_yuanhaojun============================= 
                                //当 出精値引 的值为0时，就没有必要把 出精値引 显示出来了
                                if (!dbl_m.Rows[0][9].ToString().Equals("0.00"))
                                {
                                    Fields["nMITUMORINEBIKI"].Value = dbl_m.Rows[0][9].ToString();    //値引き
                                }
                                else
                                {
                                    label27.Text = "";
                                    Fields["nMITUMORINEBIKI"].Value = "";
                                }
                                //==============================================================

                                Fields["nSHIKIRI"].Value = dbl_m.Rows[0][12].ToString();          //仕切合計
                                Fields["nTANKA_G"].Value = dbl_m.Rows[0][13].ToString();          //定価合計
                                Fields["nKINGAKU1"].Value = dbl_m.Rows[0][14].ToString();          //小計
                                //20140515 lsl add
                                if (fZEINUKIKINNGAKU == true)
                                {
                                    fZEINUKI(false);
                                    Fields["nKINGAKU2"].Value = Convert.ToDouble(Fields["nKINGAKU1"].Value);//税抜金額
                                    label22.Text = "";
                                    label5.Visible = true;
                                }
                                else
                                {
                                    fZEINUKI(false);
                                    Fields["nMITUMORISYOHIZE"].Value = Convert.ToString(nMITUMORISYOHIZE);      //消費税
                                    Fields["nKINGAKU2"].Value = Convert.ToDouble(Fields["nKINGAKU1"].Value) + nMITUMORISYOHIZE;    ////お見積合計額＝小計＋消費税 
                                    if (nKINNGAKUKAZEI != 0 && nKINNGAKUKAZEI != 0.00)
                                    {
                                        Fields["nKAZEIKINGAKU"].Value = Convert.ToString(nKINNGAKUKAZEI);
                                        //LB_nKINGAKUKAZEI.Visible = true;
                                    }
                                    else
                                    {
                                        LB_nKINGAKUKAZEI.Visible = false;
                                    }
                                    label5.Visible = true;
                                }
                                if (fZEIFUKUMUKIKINNGAKU == true)
                                {
                                    Fields["nKINGAKU_Title_goukei"].Value = Convert.ToDouble(Fields["nKINGAKU1"].Value) + nMITUMORISYOHIZE;     //小計 
                                    fZEINUKI(true);
                                    label5.Visible = false;
                                }
                                else
                                {
                                    Fields["nKINGAKU_Title_goukei"].Value = Convert.ToDouble(Fields["nKINGAKU1"].Value);
                                }
                                if (dbl_m.Rows[0]["fSYUUKEI"].ToString() == "1")
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
                                
                                if (dbl_m.Rows[0][9].ToString().Equals("0.00"))
                                {
                                    label27.Visible = false;
                                    textBox4.Visible = false;
                                    textBox2.Visible = false;
                                    label23.Visible = false;
                                    label22.Location = label27.Location;
                                    textBox1.Location = textBox4.Location;
                                    label6.Location = label23.Location;
                                    textBox13.Location = textBox2.Location;

                                    if (picture1.Visible == false && picture2.Visible == false)
                                    {
                                        label6.Border.BottomStyle = BorderLineStyle.None;
                                        textBox13.Border.BottomStyle = BorderLineStyle.None;                         
                                    }

                                }
                                
                            }

                        }

                    }
                    catch (Exception ex)
                    {

                        System.Console.WriteLine(ex.Message);
                    }
                }
                else
                {
                    #region 詳細の場合は画像を表示しない
                    
                    if (fSYOUSAI == true)
                    {
                        picture1.Visible = false;
                        picture2.Visible = false;
                        fNEWIMAGE = false;
                    }

                    #endregion

                    for (int i = dbl_Min.Rows.Count - 1; i >= 0; i--)
                    {
                        if ((dbl_Min.Rows[i]["sSYOUHIN_R"].ToString() != "" && dbl_Min.Rows[i]["sSYOUHIN_R"].ToString() != "追加行1") || dbl_Min.Rows[i]["cSYOUHIN"].ToString() != "" || dbl_Min.Rows[i]["nTANKA"].ToString() != "" || dbl_Min.Rows[i]["nSIKIRIKINGAKU"].ToString() != "" || (dbl_Min.Rows[i]["nSURYO"].ToString() != "" && dbl_Min.Rows[i]["nSURYO"].ToString() != "0") || dbl_Min.Rows[i]["sTANI"].ToString() != "")
                        {
                            RowsCount = i + 1;
                            break;
                        }
                        else
                        {
                            dbl_Min.Rows.RemoveAt(i);
                        }
                    }
                    

                    #region
                    
                    if (RowsCount <= 25)
                    {
                        if (RowsCount <= 16)
                        {
                            nPAGECOUNT = 1;

                            nPAGECOUNT_1 = 1;
                        }
                        else
                        {
                            if (picture1.Visible == false && picture2.Visible == false && fNEWIMAGE == false)
                            {
                                nPAGECOUNT = 1;

                                nPAGECOUNT_1 = 1;
                            }
                            else
                            {
                                nPAGECOUNT = 2;
                                nPAGECOUNT_1 = 2;
                                header_flag = true;
                            }
                        }
                        
                    }
                    else
                    {
                        int n = 0;
                        
                        if (RowsCount == 28) 
                        {
                            n = 0;
                        }
                        else
                        {
                            if ((RowsCount - 28) % 39 != 0)
                            {
                                n = (RowsCount - 28) / 39;
                            }
                            else
                            {
                                n = (RowsCount - 28) / 39;
                                n = n - 1;
                            }
                        }
                        for (int i = 0; i < n + 1; i++)
                        {
                            if ((RowsCount - 28) - i * 39 <= 39)
                            {
                                if ((RowsCount - 28) - i * 28 <= 27)
                                {
                                    if (picture1.Visible == false && picture2.Visible == false && fNEWIMAGE == false)
                                    {
                                        //if ((RowsCount - 28) - i * 39 == 0)//表示できるページまでは
                                        //{
                                        //    nPAGECOUNT = i + 1;

                                        //    nPAGECOUNT_1 = i + 1;
                                        //}
                                        //else
                                        //{
                                        nPAGECOUNT = i + 2;
                                        
                                        nPAGECOUNT_1 = i + 2;
                                        //}
                                    }
                                    else
                                    {
                                        nPAGECOUNT = i + 2;
                                        
                                        nPAGECOUNT_1 = i + 2;
                                    }
                                }
                                else
                                {
                                    if (picture1.Visible == false && picture2.Visible == false && fNEWIMAGE == false)
                                    {
                                        nPAGECOUNT = i + 2;
                                        
                                        nPAGECOUNT_1 = i + 2;
                                        
                                    }
                                    else
                                    {
                                        nPAGECOUNT = i + 3;
                                        
                                        nPAGECOUNT_1 = i + 3;

                                        header_flag = true;
                                        
                                    }
                                }
                            }
                        }

                    }

                    //if (FRM_SHIJI_VIEW.flag_page == true)
                    //{
                    //    FRM_SHIJI_VIEW.pcount += nPAGECOUNT;
                    //}

                    //if (FRM_SHIJI_VIEW.flag_page == false)
                    //{
                    //    nPAGECOUNT = FRM_SHIJI_VIEW.pcount;
                    //}
                    #endregion

                    if (fHYOUSI == true)
                    {
                        nPAGECOUNT += 1;
                    }


                    string sql_new = "";
                    sql_new += "SELECT ";
                    sql_new += " sSHIHARAI as sSHIHARAI";
                    sql_new += " FROM m_shiharai";
                    sql_new += " WHERE cSHIHARAI='" + dt_rm.Rows[0]["cSHIHARAI"].ToString() + "' ";

                    con.Open();
                    MySqlCommand cmd_new = new MySqlCommand(sql_new, con);
                    cmd_new.CommandTimeout = 0;
                    MySqlDataAdapter da_new = new MySqlDataAdapter(cmd_new);
                    da_new.Fill(db1);
                    da_new.Dispose();
                    
                    //db1.Autoitem(sql_new, "m_shiharai", DEMO20BasicClass.DBConnector.conn);

                    sql_new = "";
                    sql_new += "SELECT";
                    sql_new += " sTANTOUSHA as sTANTOUSHA";
                    sql_new += ",sMAIL AS sMAIL";
                    sql_new += ",sIMAGE1 AS sIMAGE1";
                    sql_new += " FROM m_j_tantousha";
                    //作成者か営業者をフラグによって表示
                    if (dt_rm.Rows[0]["fSAKUSEISYA"].ToString() == "1")
                    {
                        sql_new += " WHERE cTANTOUSHA='" + dt_rm.Rows[0]["cSAKUSEISYA"].ToString() + "'";
                    }
                    else
                    {
                        sql_new += " WHERE cTANTOUSHA='" + dt_rm.Rows[0]["cEIGYOTANTOSYA"].ToString() + "'";
                    }

                    MySqlCommand cmd_new1 = new MySqlCommand(sql_new, con);
                    cmd_new1.CommandTimeout = 0;
                    MySqlDataAdapter da_new1 = new MySqlDataAdapter(cmd_new1);
                    da_new1.Fill(db2);
                    da_new1.Dispose();

                    //db2.Autoitem(sql_new, "m_j_tantousha", DEMO20BasicClass.DBConnector.conn);

                    sql_new = "";
                    sql_new += "SELECT";
                    sql_new += " CASE WHEN fSAMA='1' THEN '様' ELSE '御中' END as fSAMA";
                    sql_new += " FROM m_tokuisaki";
                    sql_new += " WHERE cTOKUISAKI='" + dt_rm.Rows[0]["cTOKUISAKI"].ToString() + "'";

                    MySqlCommand cmd_new2 = new MySqlCommand(sql_new, con);
                    cmd_new2.CommandTimeout = 0;
                    MySqlDataAdapter da_new2 = new MySqlDataAdapter(cmd_new2);
                    da_new2.Fill(db3);
                    da_new2.Dispose();
                    con.Close();

                    //db3.Autoitem(sql_new, "m_tokuisaki", DEMO20BasicClass.DBConnector.conn);

                    LB_cMITUMORI.Text = dt_rm.Rows[0]["cMITUMORI"].ToString();  //报表 右上角：見積No.赋值
                    LB_PAGE_cMITUMORI.Text = dt_rm.Rows[0]["cMITUMORI"].ToString(); 
                    Fields["sTOKUISAKI"].Value = dt_rm.Rows[0]["sTOKUISAKI"].ToString();         //得意先名
                    if (!string.IsNullOrEmpty(dt_rm.Rows[0]["sMITUMORIKENMEI"].ToString()))
                    {
                        Fields["sMITUMORI1"].Value = dt_rm.Rows[0]["sMITUMORIKENMEI"].ToString();  //件名
                    }
                    else
                    {
                        Fields["sMITUMORI1"].Value = dt_rm.Rows[0]["sMITUMORI"].ToString();  //件名
                    }

                    Fields["dMITUMORINOKI"].Value = dt_rm.Rows[0]["sMITUMORINOKI"].ToString();

                    if (dt_rm.Rows[0]["sMITUMORIYUKOKIGEN"].ToString().Trim() != "選択してください")
                    {
                        Fields["sMITUMORIYUKOKIGEN"].Value = dt_rm.Rows[0]["sMITUMORIYUKOKIGEN"].ToString(); //有効期限
                    }


                    if (db1.Rows.Count > 0)
                    {
                        Fields["cSHIHARAI"].Value = db1.Rows[0]["sSHIHARAI"].ToString();          //支払条件
                    }
                    else
                    {
                        Fields["cSHIHARAI"].Value = "";          //支払条件
                    }

                    if (dt_rm.Rows[0]["sUKEWATASIBASYO"].ToString().Trim() != "選択してください")
                    {
                        Fields["sUKEWATASIBASYO"].Value = dt_rm.Rows[0]["sUKEWATASIBASYO"].ToString();    //受渡し場所
                    }

                    nMITUMORISYOHIZE = Convert.ToDouble(dt_rm.Rows[0]["nMITUMORISYOHIZE"].ToString());   //消費税
                    nKINNGAKUKAZEI = Convert.ToDouble(dt_rm.Rows[0]["nKAZEIKINGAKU"].ToString());
                    if (db2.Rows.Count > 0)
                    {
                        string space_dai = string.Empty;
                        if (dbl_m_j_info.Rows[0]["sIMAGE"].ToString() != "")
                        {
                            if (dbl_m_j_info.Rows[0]["fIMAGESize"].ToString() != "0")
                            {
                                space_dai = "　　　　　　　　　　　　　";
                            }
                        }
                        
                        Fields["sTANTOUSHA"].Value = space_dai + "担当:" + db2.Rows[0]["sTANTOUSHA"].ToString(); //担当者名   
                    }
                    
                    if (dt_rm.Rows[0]["sTOKUISAKI_TAN"].ToString() != "")
                    {
                        if (dt_rm.Rows[0]["sTOKUISAKI_TAN"].ToString() != "")
                        {
                            #region
                            //if (rm.sTOKUISAKI_KEISYO.Trim() == "")  
                            //{
                            //    rm.sTOKUISAKI_KEISYO = " 様";
                            //}
                            //else
                            //{
                            //    rm.sTOKUISAKI_KEISYO = " " + rm.sTOKUISAKI_KEISYO;
                            //}
                            //if (rm.sTOKUISAKI_YAKUSHOKU != "")  
                            //{
                            //    rm.sTOKUISAKI_YAKUSHOKU = rm.sTOKUISAKI_YAKUSHOKU + " ";
                            //}

                            //Fields["sTOKUISAKI_TAN"].Value = rm.sTOKUISAKI_YAKUSHOKU + " " + rm.sTOKUISAKI_TAN + " " + rm.sTOKUISAKI_KEISYO;  //請求先担当者

                            string sTOKUISAKI_KEISYO = " 様";

                            if (dt_rm.Rows[0]["sTOKUISAKI_KEISYO"].ToString().Trim() != "")
                            {
                                sTOKUISAKI_KEISYO = " " + dt_rm.Rows[0]["sTOKUISAKI_KEISYO"].ToString();
                            }
                            if (dt_rm.Rows[0]["sTOKUISAKI_YAKUSHOKU"].ToString().Trim() != "")
                            {
                                Fields["sTOKUISAKI_TAN"].Value = dt_rm.Rows[0]["sTOKUISAKI_YAKUSHOKU"].ToString() + " " + dt_rm.Rows[0]["sTOKUISAKI_TAN"].ToString() + sTOKUISAKI_KEISYO;
                            }
                            else
                            {
                                Fields["sTOKUISAKI_TAN"].Value = dt_rm.Rows[0]["sTOKUISAKI_TAN"].ToString() + sTOKUISAKI_KEISYO;
                            }
                            #endregion
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
                        LB_sTOKUISAKI_TAN1.Visible = true;
                        LB_sTOKUISAKI_TAN.Visible = true;

                        label1.Border.BottomStyle = BorderLineStyle.None;
                        LB_sTOKUISAKI.Border.BottomStyle = BorderLineStyle.None;
                        LB_sTOKUISAKI_TANBUMON.Border.BottomStyle = BorderLineStyle.None;
                        LB_sTOKUISAKI_TAN1.Border.BottomStyle = BorderLineStyle.None;
                        label131.Border.BottomStyle = BorderLineStyle.None;
                        LB_sTOKUISAKI_TAN.Border.BottomStyle = BorderLineStyle.Solid;
                        label13.Border.BottomStyle = BorderLineStyle.Solid;

                        if (dt_rm.Rows[0]["sTOKUISAKI_TANBUMON"].ToString() != "")
                        {
                            Fields["sTOKUISAKI_TANBUMON"].Value = dt_rm.Rows[0]["sTOKUISAKI_TANBUMON"].ToString();//得意先担当者部門

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

                            LB_sTOKUISAKI_TANBUMON.Visible = true;
                            LB_sTOKUISAKI_TAN1.Visible = false;
                            LB_sTOKUISAKI_TAN.Visible = true;

                            label1.Border.BottomStyle = BorderLineStyle.None;
                            LB_sTOKUISAKI.Border.BottomStyle = BorderLineStyle.None;
                            LB_sTOKUISAKI_TANBUMON.Border.BottomStyle = BorderLineStyle.None;
                            LB_sTOKUISAKI_TAN1.Border.BottomStyle = BorderLineStyle.None;
                            label131.Border.BottomStyle = BorderLineStyle.None;
                            LB_sTOKUISAKI_TAN.Border.BottomStyle = BorderLineStyle.Solid;
                            label13.Border.BottomStyle = BorderLineStyle.Solid;
                        }
                        else
                        {
                            LB_sTOKUISAKI_TANBUMON.Visible = false;
                            LB_sTOKUISAKI_TAN1.Visible = true;
                            LB_sTOKUISAKI_TAN.Visible = false;

                            label1.Border.BottomStyle = BorderLineStyle.None;
                            LB_sTOKUISAKI.Border.BottomStyle = BorderLineStyle.None;
                            LB_sTOKUISAKI_TANBUMON.Border.BottomStyle = BorderLineStyle.None;
                            LB_sTOKUISAKI_TAN1.Border.BottomStyle = BorderLineStyle.Solid;
                            label131.Border.BottomStyle = BorderLineStyle.Solid;
                            LB_sTOKUISAKI_TAN.Border.BottomStyle = BorderLineStyle.None;
                            label13.Border.BottomStyle = BorderLineStyle.None;
                        }
                    }
                    else
                    {
                        LB_sTOKUISAKI_TANBUMON.Visible = false;
                        LB_sTOKUISAKI_TAN1.Visible = false;
                        LB_sTOKUISAKI_TAN.Visible = false;

                        label1.Border.BottomStyle = BorderLineStyle.Solid;
                        LB_sTOKUISAKI.Border.BottomStyle = BorderLineStyle.Solid;
                        LB_sTOKUISAKI_TANBUMON.Border.BottomStyle = BorderLineStyle.None;
                        LB_sTOKUISAKI_TAN1.Border.BottomStyle = BorderLineStyle.None;
                        label131.Border.BottomStyle = BorderLineStyle.None;
                        LB_sTOKUISAKI_TAN.Border.BottomStyle = BorderLineStyle.None;
                        label13.Border.BottomStyle = BorderLineStyle.None;

                    }

                    if (dt_rm.Rows[0]["sTOKUISAKI_TAN"].ToString() == "")
                    {
                        label13.Text = "";

                        if (db3.Rows.Count > 0)
                        {
                            Fields["fSAMA"].Value = db3.Rows[0]["fSAMA"].ToString();          //得意先フラグ
                        }
                        else
                        {
                            Fields["fSAMA"].Value = "御中";          //得意先フラグ
                        }

                        if (!string.IsNullOrEmpty(Fields["fSAMA"].Value.ToString()))
                        {
                            if (!Fields["sTOKUISAKI"].Value.ToString().Contains("\r\n") && !Fields["sTOKUISAKI"].Value.ToString().Contains("\n"))
                            {
                                int cgetbyte = getbyte(Fields["sTOKUISAKI"].Value.ToString());
                                if (cgetbyte == 32 || cgetbyte == 33)
                                {
                                    Fields["sTOKUISAKI"].Value = Fields["sTOKUISAKI"].Value + "　　" + Fields["fSAMA"].Value;
                                }
                                else
                                {
                                    Fields["sTOKUISAKI"].Value = Fields["sTOKUISAKI"].Value + "　" + Fields["fSAMA"].Value;
                                }
                            }
                            else
                            {
                                Fields["sTOKUISAKI"].Value = Fields["sTOKUISAKI"].Value + "　" + Fields["fSAMA"].Value;
                            }
                            //Fields["sTOKUISAKI"].Value = Fields["sTOKUISAKI"].Value + "　" + Fields["fSAMA"].Value;
                        }
                        Fields["fSAMA"].Value = "";
                    }
                    else
                    {
                        label1.Text = "";
                    }

                    Fields["sBIKOU"].Value = dt_rm.Rows[0]["sBIKOU"].ToString();    //備考
                    //LB_dMITUMORISAKUSEI.Text = DateTime.Now.ToShortDateString();  //作成日
                    if (dt_rm.Rows[0]["dMITUMORISAKUSEI"].ToString().Substring(0, 10) != "1900/01/01")
                    {
                        LB_dMITUMORISAKUSEI.Text = dt_rm.Rows[0]["dMITUMORISAKUSEI"].ToString().Substring(0, 10);
                        LB_PAGE_dMITUMORISAKUSEI.Text = dt_rm.Rows[0]["dMITUMORISAKUSEI"].ToString().Substring(0, 10);  //「一番目のページに見積作成日と二番目以降の見積作成日が違う問題」
                    }
                    //「一番目のページに見積作成日と二番目以降の見積作成日が違う問題」
                    // LB_PAGE_dMITUMORISAKUSEI.Text = DEMO20BasicClass.DBConnector.Fu_GetDateTime().ToShortDateString();// dbl_m.Tables[0].Rows[0]["dMITUMORISAKUSEI"].ToString().Substring(0, 10);

                    if (HANKO_Check == "欄無し")
                    {
                        lb_hanko1.Visible = false;
                        lb_hanko2.Visible = false;
                        lb_hanko3.Visible = false;
                        if (db2.Rows.Count > 0)
                        {
                            if (db2.Rows[0]["sMAIL"].ToString() != "")
                            {
                                Fields["sMAIL"].Value = "MAIL:" + db2.Rows[0]["sMAIL"].ToString();//メールアドレス
                            }
                            else
                            {
                                #region
                                if (dbl_m_j_info.Rows.Count > 0)
                                {
                                    //担当者のメールアドレスがnullの場合は代表メールアドレスを表示
                                    if (dbl_m_j_info.Rows[0]["sMAIL"].ToString() != "")
                                    {
                                        Fields["sMAIL"].Value = "MAIL:" + dbl_m_j_info.Rows[0]["sMAIL"].ToString();
                                    }
                                }
                                #endregion
                            }
                        }
                        else
                        {
                            #region
                            if (dbl_m_j_info.Rows.Count > 0)
                            {
                                //担当者のメールアドレスがnullの場合は代表メールアドレスを表示
                                if (dbl_m_j_info.Rows[0]["sMAIL"].ToString() != "")
                                {
                                    Fields["sMAIL"].Value = "MAIL:" + dbl_m_j_info.Rows[0]["sMAIL"].ToString();
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
                            if (db2.Rows.Count > 0)
                            {
                                if (db2.Rows[0]["sIMAGE1"] != null)
                                {
                                    if (!string.IsNullOrEmpty(db2.Rows[0]["sIMAGE1"].ToString()))
                                    {
                                        byte[] bytes = (byte[])db2.Rows[0]["sIMAGE1"];
                                        MemoryStream stream = new MemoryStream(bytes);
                                        P_sIMAGEHankou.Image = System.Drawing.Image.FromStream(stream);
                                        P_sIMAGEHankou.PictureAlignment = PictureAlignment.Center;

                                        //P_sIMAGEHankou.Image = Image.FromStream(func.toImage((byte[])db2.Rows[0]["sIMAGE1"]));
                                        //P_sIMAGEHankou.PictureAlignment = PictureAlignment.Center;
                                    }
                                }
                            }
                        }
                    }

                    //当 出精値引 的值为0时，就没有必要把 出精値引 显示出来了
                    if (!dt_rm.Rows[0]["nMITUMORINEBIKI"].ToString().Equals("0"))
                    {
                        Fields["nMITUMORINEBIKI"].Value = dt_rm.Rows[0]["nMITUMORINEBIKI"].ToString();    //値引き
                    }
                    else
                    {
                        label27.Text = "";
                        Fields["nMITUMORINEBIKI"].Value = "";
                    }
                    //==============================================================

                    Fields["nSHIKIRI"].Value = dt_rm.Rows[0]["nKIRI_G"].ToString();          //仕切合計
                    Fields["nTANKA_G"].Value = dt_rm.Rows[0]["nTANKA_G"].ToString();          //定価合計
                    Fields["nKINGAKU1"].Value = dt_rm.Rows[0]["nKINGAKU"].ToString();          //小計
                    if (fZEINUKIKINNGAKU == true)
                    {
                        fZEINUKI(false);
                        Fields["nKINGAKU2"].Value = Convert.ToDouble(Fields["nKINGAKU1"].Value);//税抜金額
                        label22.Text = "";
                        label5.Visible = true;
                    }
                    else
                    {
                        fZEINUKI(false);
                        Fields["nMITUMORISYOHIZE"].Value = Convert.ToString(nMITUMORISYOHIZE);      //消費税
                        Fields["nKINGAKU2"].Value = Convert.ToDouble(Fields["nKINGAKU1"].Value) + nMITUMORISYOHIZE;    ////お見積合計額＝小計＋消費税 
                        //Fields["nKAZEIKINGAKU"].Value = Convert.ToString(nKINNGAKUKAZEI);
                        if (nKINNGAKUKAZEI != 0 && nKINNGAKUKAZEI != 0.00)
                        {
                            Fields["nKAZEIKINGAKU"].Value = Convert.ToString(nKINNGAKUKAZEI); 
                            // LB_nKINGAKUKAZEI.Visible = true;
                        }
                        else
                        {
                            LB_nKINGAKUKAZEI.Visible = false;
                        }
                        label5.Visible = true;
                    }
                    if (fZEIFUKUMUKIKINNGAKU == true)
                    {
                        Fields["nKINGAKU_Title_goukei"].Value = Convert.ToDouble(Fields["nKINGAKU1"].Value) + nMITUMORISYOHIZE;     //小計 
                        fZEINUKI(true);
                        label5.Visible = false;
                    }
                    else
                    {
                        Fields["nKINGAKU_Title_goukei"].Value = Convert.ToDouble(Fields["nKINGAKU1"].Value);
                    }
                    if (dt_rm.Rows[0]["fSYUUKEI"].ToString() == "1")
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
                    
                    if (dt_rm.Rows[0]["nMITUMORINEBIKI"].ToString().Equals("0"))
                    {
                        label27.Visible = false;
                        textBox4.Visible = false;
                        textBox2.Visible = false;
                        label23.Visible = false;
                        label22.Location = label27.Location;
                        textBox1.Location = textBox4.Location;
                        label6.Location = label23.Location;
                        textBox13.Location = textBox2.Location;

                        if (picture1.Visible == false && picture2.Visible == false)
                        {
                            label6.Border.BottomStyle = BorderLineStyle.None;
                            textBox13.Border.BottomStyle = BorderLineStyle.None;                      
                        }

                    }
                }
                if (dbl_m_s_info.Rows.Count > 0)
                {
                    //if (fRYOUHOU == true || fSYOUSAI == true)
                    if (fRYOUHOU == true && fSYOUSAI == true)
                    {
                        Fields["sMITUMORI"].Value = dbl_m_s_info.Rows[0][0].ToString() + "(詳細)";   //見積欄
                        LB_S_sMITUMORI_1.Visible = false;//見積一覧
                        LB_S_sMITUMORI.Visible = true;//見積詳細
                        picture1.Visible = false;
                        picture2.Visible = false;
                    }
                    else
                    {
                        Fields["sMITUMORI"].Value = dbl_m_s_info.Rows[0][0].ToString();   //見積欄
                        LB_S_sMITUMORI_1.Visible = true;//見積一覧
                        LB_S_sMITUMORI.Visible = false;//見積詳細
                    }
                    label2.Text = dbl_m_s_info.Rows[0]["sAisatsu"].ToString();
                    label5.Text = dbl_m_s_info.Rows[0]["sZeinu"].ToString();
                    // Fields["sMITUMORI"].Value = dbl_m_s_info.Tables[0].Rows[0][0].ToString();   //見積欄
                    Fields["sNAIYOU"].Value = dbl_m_s_info.Rows[0][1].ToString();    //内容欄
                    Fields["sKEN"].Value = dbl_m_s_info.Rows[0][2].ToString();       //件名欄
                    Fields["sNOUKI"].Value = dbl_m_s_info.Rows[0][3].ToString();     //納期欄
                    Fields["sYUUKOU"].Value = dbl_m_s_info.Rows[0][4].ToString();    //有効期限欄 
                    Fields["sSHIHARAI"].Value = dbl_m_s_info.Rows[0][5].ToString();  //支払条件欄
                    Fields["sUKEBASYOU"].Value = dbl_m_s_info.Rows[0][6].ToString(); //受渡し場所欄 
                
                    if (dbl_m_j_info.Rows[0]["sIMAGE"].ToString() != "")
                    {
                        //P_sIMAGE.Image = Image.FromStream(func.toImage((byte[])dbl_m_j_info.Tables[0].Rows[0]["sIMAGE"]));
                        if (dbl_m_j_info.Rows[0]["fIMAGESize"].ToString() != "0")
                        {
                            byte[] bytes = (byte[])dbl_m_j_info.Rows[0]["sIMAGE"];
                            MemoryStream stream = new MemoryStream(bytes);
                            P_sIMAGE2.Image = System.Drawing.Image.FromStream(stream);
                            P_sIMAGE2.PictureAlignment = PictureAlignment.TopLeft;

                            //P_sIMAGE2.Image = Image.FromStream(func.toImage((byte[])dbl_m_j_info.Rows[0]["sIMAGE"]));
                            //P_sIMAGE2.PictureAlignment = PictureAlignment.TopLeft;

                            label10.Visible = false;
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
                            byte[] bytes = (byte[])dbl_m_j_info.Rows[0]["sIMAGE"];
                            MemoryStream stream = new MemoryStream(bytes);
                            P_sIMAGE.Image = System.Drawing.Image.FromStream(stream);
                            P_sIMAGE.PictureAlignment = PictureAlignment.TopLeft;

                            //P_sIMAGE.Image = Image.FromStream(func.toImage((byte[])dbl_m_j_info.Rows[0]["sIMAGE"]));
                            //P_sIMAGE.PictureAlignment = PictureAlignment.TopLeft;
                        }
                    }
                    else
                    {
                        P_sIMAGE.Image = null;
                        P_sIMAGE2.Image = null;
                    }
                }
                
                if (dbl_m_j_info.Rows.Count > 0)
                {
                    if (dbl_m_j_info.Rows[0]["fIMAGESize"].ToString() == "0")
                    {
                        LB_sTITLE.Text = dbl_m_j_info.Rows[0]["sNAIYOU"].ToString();

                        if (LB_sTITLE.Text != "")
                        {
                            int index = -1;
                            int count = 0;
                            while (-1 != (index = LB_sTITLE.Text.IndexOf(Environment.NewLine, index + 1)))
                                count++;

                            if (count == 3)
                            {
                                //LB_sTITLE.Height = 0.650786F;
                                LB_sTITLE.Top = 1.806299F;
                                P_sIMAGE.Top = 0.7712599F;
                            }
                            else if (count == 4)
                            {
                                if (HANKO_Check != "欄無し")
                                {
                                    //LB_sTITLE.Height = 0.750786F;
                                    LB_sTITLE.Top = 1.673937F;
                                    P_sIMAGE.Top = 0.7112599F;
                                }
                                else
                                {
                                    LB_sTITLE.Top = 2.003937F;
                                    P_sIMAGE.Top = 1.0612599F;
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
                            if (dbl_m.Rows[0]["sMAIL"].ToString() != "")
                            {
                                LB_SMAIL.Value = "MAIL:" + dbl_m.Rows[0]["sMAIL"].ToString();//メールアドレス
                            }
                            else
                            {
                                if (dbl_m_j_info.Rows.Count > 0)
                                {
                                    //担当者のメールアドレスがnullの場合は代表メールアドレスを表示
                                    if (dbl_m_j_info.Rows[0]["sMAIL"].ToString() != "")
                                    {
                                        LB_SMAIL.Value = "MAIL:" + dbl_m_j_info.Rows[0]["sMAIL"].ToString();
                                    }
                                }
                            }

                            if (dbl_m.Rows[0]["sTANTOUSHA"].ToString() != "")
                            {

                                LB_sTANTOUSHA.Value = "担当:" + dbl_m.Rows[0]["sTANTOUSHA"].ToString(); //担当者名                                

                            }
                        }
                        else
                        {
                            if (db2.Rows.Count > 0)
                            {
                                if (db2.Rows[0]["sMAIL"].ToString() != "")
                                {
                                    LB_SMAIL.Value = "MAIL:" + db2.Rows[0]["sMAIL"].ToString();//メールアドレス
                                }
                                else
                                {
                                    if (dbl_m_j_info.Rows.Count > 0)
                                    {
                                        //担当者のメールアドレスがnullの場合は代表メールアドレスを表示
                                        if (dbl_m_j_info.Rows[0]["sMAIL"].ToString() != "")
                                        {
                                            LB_SMAIL.Value = "MAIL:" + dbl_m_j_info.Rows[0]["sMAIL"].ToString();
                                        }
                                    }
                                }

                                if (db2.Rows[0]["sTANTOUSHA"].ToString() != "")
                                {
                                    LB_sTANTOUSHA.Value = "担当:" + db2.Rows[0]["sTANTOUSHA"].ToString(); //担当者名  
                                }
                            }
                            else
                            {
                                if (dbl_m_j_info.Rows.Count > 0)
                                {
                                    //担当者のメールアドレスがnullの場合は代表メールアドレスを表示
                                    if (dbl_m_j_info.Rows[0]["sMAIL"].ToString() != "")
                                    {
                                        LB_SMAIL.Value = "MAIL:" + dbl_m_j_info.Rows[0]["sMAIL"].ToString();
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

                kaigyoucode();

                #region「行数」
                if (!Fields["sMITUMORI1"].Value.ToString().Trim().Equals(""))
                {
                    if (LineCount(Fields["sMITUMORI1"].Value.ToString().Trim()) > 1)
                    {
                        LB_sMITUMORI.Alignment = TextAlignment.Left;
                        if (LineCount(Fields["sMITUMORI1"].Value.ToString().Trim()) > 2)
                        {
                            LB_S_sKEN.Location = new System.Drawing.PointF(0.599f, 1.907f); //1.807f
                            LB_sMITUMORI.Location = new System.Drawing.PointF(0.995f, 1.463f);
                            LB_sMITUMORI.Height = 0.7f;
                            label2.Location = new System.Drawing.PointF(0.599f, 2.204f);
                        }
                        else
                        {
                            if (getbyte(Fields["sMITUMORI1"].Value.ToString()) > 80)
                            {
                                LB_S_sKEN.Location = new System.Drawing.PointF(0.599f, 1.907f);
                                LB_sMITUMORI.Location = new System.Drawing.PointF(0.995f, 1.463f);
                                LB_sMITUMORI.Height = 0.7f;
                                label2.Location = new System.Drawing.PointF(0.599f, 2.204f);
                            }
                        }
                    }
                    else
                    {
                        LB_sMITUMORI.Alignment = TextAlignment.Center;

                        if (getbyte(Fields["sMITUMORI1"].Value.ToString()) > 80)
                        {
                            LB_S_sKEN.Location = new System.Drawing.PointF(0.599f, 1.907f);
                            LB_sMITUMORI.Location = new System.Drawing.PointF(0.995f, 1.463f);
                            LB_sMITUMORI.Height = 0.7f;
                            label2.Location = new System.Drawing.PointF(0.599f, 2.204f);

                            LB_sMITUMORI.Alignment = TextAlignment.Left;
                        }
                        else if (getbyte(Fields["sMITUMORI1"].Value.ToString()) > 40)
                        {
                            LB_sMITUMORI.Alignment = TextAlignment.Left;
                        }

                    }
                    string temp = Fields["sMITUMORI1"].Value.ToString();
                    Fields["sMITUMORI1"].Value = temp.Trim();
                }
                #endregion

                if (dbl_m_j_info.Rows.Count > 0)
                {
                    int space = int.Parse(dbl_m_j_info.Rows[0]["sSPACE"].ToString());
                    for (int i = 0; i < space; i++)
                    {
                        Fields["SMAIL"].Value = " " + Fields["SMAIL"].Value;
                        //Fields["sTANTOUSHA"].Value = " " + Fields["sTANTOUSHA"].Value;   
                        if (dbl_m_j_info.Rows[0]["sIMAGE"].ToString() != "")
                        {
                            if (dbl_m_j_info.Rows[0]["fIMAGESize"].ToString() != "0")
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
                if (tokui_align != null)
                {
                    if (!string.IsNullOrEmpty(tokui_align.Trim()))
                    {
                        if (tokui_align == "左寄せ")
                        {
                            LB_sTOKUISAKI.Alignment = TextAlignment.Left;
                        }
                        else if (tokui_align == "中央")
                        {
                            LB_sTOKUISAKI.Alignment = TextAlignment.Center;
                        }
                    }
                }
                if (busyo_align != null)
                {
                    if (!string.IsNullOrEmpty(busyo_align.Trim()))
                    {
                        if (busyo_align == "左寄せ")
                        {
                            LB_sTOKUISAKI_TANBUMON.Alignment = TextAlignment.Left;
                        }
                        else if (busyo_align == "中央")
                        {
                            LB_sTOKUISAKI_TANBUMON.Alignment = TextAlignment.Center;
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
                                LB_sTOKUISAKI_TAN.Alignment = TextAlignment.Left;
                            }
                            else
                            {
                                LB_sTOKUISAKI_TAN1.Alignment = TextAlignment.Left;
                            }
                        }
                        else if (tantou_align == "中央")
                        {
                            if (LB_sTOKUISAKI_TAN.Visible == true)
                            {
                                LB_sTOKUISAKI_TAN.Alignment = TextAlignment.Center;
                            }
                            else
                            {
                                LB_sTOKUISAKI_TAN1.Alignment = TextAlignment.Center;
                            }
                        }
                    }
                }
            }

            #region 20210615 maythu add
            if (fHIDZUKE == true)
            {
                LB_dMITUMORISAKUSEI.Visible = true;
            }
            else
            {
                LB_dMITUMORISAKUSEI.Visible = false;
            }
            #endregion
        }

        public static int getbyte(string ssyouhin)
        {
            Encoding sjisEnc = Encoding.GetEncoding("Shift_JIS");
            int num = sjisEnc.GetByteCount(ssyouhin);
            return num;
        }

        #region「行数」
        private int LineCount(string str)
        {
            //StringSplitOptions.None including empty row
            //StringSplitOptions.RemoveEmptyEntries not including empty row
            return str.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries).Length;
        }
        #endregion

        #region kaigyoucode

        private void kaigyoucode()
        {
            try
            {

                //件名
                Fields["sMITUMORI1"].Value = Fields["sMITUMORI1"].Value.ToString().Trim().Replace("\r\n", "CRLFu000Du000A").Replace("\r", "").Replace("\n", "").Replace("CRLFu000Du000A", "\r\n");

                //得意先
                Fields["sTOKUISAKI"].Value = Fields["sTOKUISAKI"].Value.ToString().Trim().Replace("\r\n", "CRLFu000Du000A").Replace("\r", "").Replace("\n", "").Replace("CRLFu000Du000A", "\r\n");
                //得意先担当者
                Fields["sTOKUISAKI_TAN"].Value = Fields["sTOKUISAKI_TAN"].Value.ToString().Trim().Replace("\r\n", "").Replace("\r", "").Replace("\n", "");

                //得意先担当者部門
                Fields["sTOKUISAKI_TANBUMON"].Value = Fields["sTOKUISAKI_TANBUMON"].Value.ToString().Trim().Replace("\r\n", "").Replace("\r", "").Replace("\n", "");

            }
            catch { }

        }

        #endregion

        #region  是否有消费税
        private void fZEINUKI(bool fZEIKINNGAKU)
        {
            label8.Visible = fZEIKINNGAKU;
            textBox6.Visible = fZEIKINNGAKU;
            label9.Visible = fZEIKINNGAKU;
            TB_nMITUMORISYOHIZE1.Visible = fZEIKINNGAKU;
            label12.Visible = fZEIKINNGAKU;
            TB_nKINGAKUKAZEI.Visible = fZEIKINNGAKU;
            LB_nKINGAKUKAZEI.Visible = fZEIKINNGAKU;
        }
        #endregion

        private void mitsumori1_ReportEnd(object sender, EventArgs e)
        {
            try
            {
                ;
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }
        }

        private void mitsumori1_DataInitialize(object sender, EventArgs e)
        {
            Fields.Add("sCO");
            Fields.Add("sMITUMORI");          //見積欄
            Fields.Add("sNAIYOU");            //内容欄
            Fields.Add("sBIKOU");             //明細備考欄
            Fields.Add("sKEN");               //件名欄
            Fields.Add("sNOUKI");             //納期欄
            Fields.Add("sYUUKOU");            //有効期限欄
            Fields.Add("sSHIHARAI");          //支払条件欄
            Fields.Add("sUKEBASYOU");         //受渡し場所欄
            Fields.Add("sTOKUISAKI");         //得意先名
            Fields.Add("sTOKUISAKI_TAN");     //得意先担当者
            Fields.Add("sMITUMORI1");         //件名
            Fields.Add("dMITUMORINOKI");      //納期
            Fields.Add("sMITUMORIYUKOKIGEN"); //有効期限
            Fields.Add("cSHIHARAI");          //支払条件
            Fields.Add("sUKEWATASIBASYO");    //受渡し場所
            Fields.Add("nRITU");             //掛け率 
            Fields.Add("nMITUMORISYOHIZE");   //消費税
            Fields.Add("nKAZEIKINGAKU"); 
            Fields.Add("sTANTOUSHA");         //担当者
            Fields.Add("nINSATSU_GYO");  
            Fields.Add("sSYOUHIN_R");         //内容.仕様----商品名 
            Fields.Add("nSURYO");             //数量 整数部分
            Fields.Add("nSURYO2");            //数量 小数部分
            Fields.Add("sTANI");              //単位 
            Fields.Add("nTANKA");             //単価
            Fields.Add("nSIKIRITANKA");       //仕切単価 
            Fields.Add("nSIKIRITANKA2");       //仕切単価  
            Fields.Add("nKINGAKU");           //金額           
            Fields.Add("nKINGAKU3");           //金額           
            Fields.Add("nKINGAKU1");          //小計
            Fields.Add("nKINGAKU2");          //お見積合計額
            Fields.Add("nSOUGOUKEI");         //税込金額
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
            Fields.Add("nKINGAKU_Title_goukei");           //金額  
        }

        #region
        private void Syouhinmei()
        {
            LB_S_sNAIYOU.Alignment = TextAlignment.Left;

            LB_NO.Border.TopStyle = BorderLineStyle.Solid; //行NO
            LB_NO.Border.BottomStyle = BorderLineStyle.Solid;//行NO
            LB_NO.Alignment = TextAlignment.Center;//行NO
            LB_S_sNAIYOU.Border.TopStyle = BorderLineStyle.Solid; //内容.仕様----商品名 
            LB_S_sNAIYOU.Border.BottomStyle = BorderLineStyle.Solid; //内容.仕様----商品名 
            LB_nSURYO.Border.TopStyle = BorderLineStyle.Solid; //数量(整数部分）
            LB_nSURYO.Border.BottomStyle = BorderLineStyle.Solid; //数量(整数部分）
            LB_sTANI.Border.TopStyle = BorderLineStyle.Solid; //単位
            LB_sTANI.Border.BottomStyle = BorderLineStyle.Solid; //単位
            LB_nSIKIRITANKA.Border.TopStyle = BorderLineStyle.Solid;  //単価
            LB_nSIKIRITANKA.Border.BottomStyle = BorderLineStyle.Solid;  //単価
            LB_S_nKINGAKU1.Border.TopStyle = BorderLineStyle.Solid; //金額
            LB_S_nKINGAKU1.Border.BottomStyle = BorderLineStyle.Solid; //金額
            LB_S_sNAIYOU.Border.LeftStyle = BorderLineStyle.Solid;//商品名
            LB_nSURYO.Border.LeftStyle = BorderLineStyle.Solid;
            LB_sTANI.Border.LeftStyle = BorderLineStyle.Solid;
            LB_sTANI.Border.RightStyle = BorderLineStyle.Solid;
            LB_S_nKINGAKU1.Border.LeftStyle = BorderLineStyle.Solid;
            
            if (fSYOUSAI == false && fMIDASHI == false)
            {
                if (fINS == 0)
                {
                    if (dbl_m_m.Rows[i]["sKUBUN"].ToString() == "見")
                    {
                        LB_S_sNAIYOU.Border.LeftStyle = BorderLineStyle.None;
                        LB_nSURYO.Border.LeftStyle = BorderLineStyle.None;
                        LB_sTANI.Border.LeftStyle = BorderLineStyle.None;
                        LB_sTANI.Border.RightStyle = BorderLineStyle.None;
                        LB_S_nKINGAKU1.Border.LeftStyle = BorderLineStyle.None;
                    }
                }
                else
                {
                    if (dbl_Min.Rows[i]["sKUBUN"].ToString() == "見")
                    {
                        LB_S_sNAIYOU.Border.LeftStyle = BorderLineStyle.None;
                        LB_nSURYO.Border.LeftStyle = BorderLineStyle.None;
                        LB_sTANI.Border.LeftStyle = BorderLineStyle.None;
                        LB_sTANI.Border.RightStyle = BorderLineStyle.None;
                        LB_S_nKINGAKU1.Border.LeftStyle = BorderLineStyle.None;
                    }
                }
            }
        }

        private void Syouhinmei_Tsuika(Boolean flag_print, int row)
        {
            ChangeFont(f4);

            //label23.Text = "明細計";
            LB_S_sNAIYOU.Alignment = TextAlignment.Right;
            LB_S_sNAIYOU.Border.LeftStyle = BorderLineStyle.None;
            LB_nSURYO.Border.LeftStyle = BorderLineStyle.None;
            LB_sTANI.Border.LeftStyle = BorderLineStyle.None;
            LB_sTANI.Border.RightStyle = BorderLineStyle.None;
            LB_S_nKINGAKU1.Border.LeftStyle = BorderLineStyle.None;

            if (flag_print == true)//印刷の場合
            {
                if (dbl_m_m.Rows[row]["sSYOUHIN_R"].ToString() == "計")
                {
                    Kei();
                }
                else
                {
                    if (dbl_m_m.Rows[row]["sKUBUN"].ToString() == "計")
                    {
                        SyouKei(false);
                    }
                    else
                    {
                        SyouKei(true);
                    }
                }
            }
            else //プレピューの場合
            {
                if (dbl_Min.Rows[row]["sSYOUHIN_R"].ToString() == "計")
                {
                    Kei();
                }
                else
                {
                    if (dbl_Min.Rows[row]["sKUBUN"].ToString() == "計")
                    {
                        SyouKei(false);
                    }
                    else
                    {
                        SyouKei(true);
                    }
                }

            }
        }
        private void Kei()//計の場合 
        {
            LB_NO.Border.TopStyle = BorderLineStyle.Solid; //行NO
            LB_NO.Alignment = TextAlignment.Center;//行NO
            LB_S_sNAIYOU.Border.LeftStyle = BorderLineStyle.None; //内容.仕様----商品名 
            LB_nSURYO.Border.TopStyle = BorderLineStyle.Solid; //数量(整数部分）
            LB_sTANI.Border.TopStyle = BorderLineStyle.Solid; //単位
            LB_nSIKIRITANKA.Border.TopStyle = BorderLineStyle.Solid;  //単価
            LB_S_nKINGAKU1.Border.TopStyle = BorderLineStyle.Solid; //金額
        }
        private void SyouKei(bool syousai_gyou)//小計の場合 
        {
            if (syousai_gyou == true)
            {
                LB_NO.Border.TopStyle = BorderLineStyle.ThickSolid; //行NO
                LB_S_sNAIYOU.Border.TopStyle = BorderLineStyle.ThickSolid; //内容.仕様----商品名 
                LB_nSURYO.Border.TopStyle = BorderLineStyle.ThickSolid; //数量(整数部分）
                LB_sTANI.Border.TopStyle = BorderLineStyle.ThickSolid; //単位
                LB_nSIKIRITANKA.Border.TopStyle = BorderLineStyle.ThickSolid;  //単価
                LB_S_nKINGAKU1.Border.TopStyle = BorderLineStyle.ThickSolid; //金額
            }
            LB_NO.Border.BottomStyle = BorderLineStyle.ThickSolid;
            LB_NO.Alignment = TextAlignment.Center;//行NO
            LB_S_sNAIYOU.Border.BottomStyle = BorderLineStyle.ThickSolid;
            LB_nSURYO.Border.BottomStyle = BorderLineStyle.ThickSolid;
            LB_sTANI.Border.BottomStyle = BorderLineStyle.ThickSolid;
            LB_nSIKIRITANKA.Border.BottomStyle = BorderLineStyle.ThickSolid;
            LB_S_nKINGAKU1.Border.BottomStyle = BorderLineStyle.ThickSolid;
        }

        private void LastRow()//最後行の設定
        {
            LB_NO.Border.BottomStyle = BorderLineStyle.ThickSolid;           //行NO
            LB_NO.Alignment = TextAlignment.Center;//行NO
            LB_S_sNAIYOU.Border.BottomStyle = BorderLineStyle.ThickSolid;    //内容.仕様----商品名 
            LB_nSURYO.Border.BottomStyle = BorderLineStyle.ThickSolid;       //数量(整数部分）
            LB_nSURYO2.Border.BottomStyle = BorderLineStyle.ThickSolid;      //数量(小数部分
            LB_sTANI.Border.BottomStyle = BorderLineStyle.ThickSolid;        //単位
            LB_nSIKIRITANKA.Border.BottomStyle = BorderLineStyle.ThickSolid;       //単価
            LB_S_nKINGAKU1.Border.BottomStyle = BorderLineStyle.ThickSolid;  //金額
        }
        #endregion

        #region

        int k1 = 1;
        private void pageHeader_BeforePrint(object sender, EventArgs e)
        {
            if (PageNumber < 2)
            {
                label3.Visible = false;
                LB_PAGE_cMITUMORI.Visible = false;
                LB_PAGE_dMITUMORISAKUSEI.Visible = false;

                LB_PAGE1.Visible = false;
            }
            else
            {
                label3.Visible = true;
                LB_PAGE_cMITUMORI.Visible = true;
                //LB_PAGE_dMITUMORISAKUSEI.Visible = true;
                #region
                //if (FRM_RPT_PRINT_CHOICE.fHIDZUKE == true)
                //{
                //    LB_PAGE_dMITUMORISAKUSEI.Visible = true;
                //}
                //else
                //{
                //    LB_PAGE_dMITUMORISAKUSEI.Visible = false;
                //}
                #endregion

                //LB_PAGE_cMITUMORI.Font = f6;
                //LB_PAGE_dMITUMORISAKUSEI.Font = f7;

                //if (header_flag == true && this.PageNumber == nPAGECOUNT_1)
                //{
                //    textBox8.Visible = false;
                //    textBox9.Visible = false;
                //    textBox10.Visible = false;
                //    textBox7.Visible = false;
                //    textBox3.Visible = false;
                //    textBox11.Visible = false;

                //    //this.pageHeader.Height = float.Parse("0");
                //    header_flag = false;

                //    label3.Visible = false;
                //    LB_PAGE_cMITUMORI.Visible = false;
                //    LB_PAGE_dMITUMORISAKUSEI.Visible = false;
                //}
            }

            #region「ページが一つだけの場合」

            if (nPAGECOUNT > 1)
            {
                if (fINS == 0)//印刷
                {
                    //if (FRM_RPT_PRINT_CHOICE.rmt_pcount1 > 0)
                    //{
                    //    LB_PAGE1.Text = "(" + (FRM_RPT_PRINT_CHOICE.rmt_pcount1 + k1) + " / " + nPAGECOUNT.ToString() + ")";
                    //}
                    //else
                    //{
                    //    if (fHYOUSI == true)
                    //    {
                    //        LB_PAGE1.Text = "(" + (FRM_RPT_PRINT_CHOICE.rmt_pcount1 + k1 + 1) + " / " + nPAGECOUNT.ToString() + ")";
                    //    }
                    //    else
                    //    {
                    //        LB_PAGE1.Text = "(" + (FRM_RPT_PRINT_CHOICE.rmt_pcount1 + k1) + " / " + nPAGECOUNT.ToString() + ")";
                    //    }
                    //}
                    k1++;
                }
                else //プレピュー
                {
                    //if (fHYOUSI == true)
                    //{
                    //    LB_PAGE1.Text = "(" + (FRM_SHIJI_VIEW.rmt_pcount + k1 + 1) + " / " + nPAGECOUNT.ToString() + ")";
                    //}
                    //else
                    //{
                    //    LB_PAGE1.Text = "(" + (FRM_SHIJI_VIEW.rmt_pcount + k1) + " / " + nPAGECOUNT.ToString() + ")";
                    //}
                    //k1++;
                }
            }


            #endregion
        }
        int k = 1;
        private void pageFooter_BeforePrint(object sender, EventArgs e)
        {

            if (fINS == 0)//印刷
            {
                //if (FRM_RPT_PRINT_CHOICE.rmt_pcount1 > 0)
                //{
                //    this.LB_PAGE.Text = "(" + (FRM_RPT_PRINT_CHOICE.rmt_pcount1 + k) + " / " + nPAGECOUNT.ToString() + ")";
                //}
                //else
                //{
                //    if (fHYOUSI == true)
                //    {
                //        this.LB_PAGE.Text = "(" + (FRM_RPT_PRINT_CHOICE.rmt_pcount1 + k + 1) + " / " + nPAGECOUNT.ToString() + ")";
                //    }
                //    else
                //    {
                //        this.LB_PAGE.Text = "(" + (FRM_RPT_PRINT_CHOICE.rmt_pcount1 + k) + " / " + nPAGECOUNT.ToString() + ")";
                //    }
                //}
                //k++;
            }
            else //プレピュー
            {
                //if (fHYOUSI == true)
                //{
                //    this.LB_PAGE.Text = "(" + (FRM_SHIJI_VIEW.rmt_pcount + k + 1) + " / " + nPAGECOUNT.ToString() + ")";
                //}
                //else
                //{
                //    this.LB_PAGE.Text = "(" + (FRM_SHIJI_VIEW.rmt_pcount + k) + " / " + nPAGECOUNT.ToString() + ")";
                //}
                //k++;
            }


        }
        #endregion

        private void ChangeFont(System.Drawing.Font fond)
        {
            LB_S_sNAIYOU.Font = fond;
        }

        #region

        private void reportHeader1_BeforePrint(object sender, EventArgs e)
        {
            if (fINS == 0)//印刷
            {
                //if (FRM_RPT_PRINT_CHOICE.rmt_pcount1 > 0)
                //{
                //    this.LB_PAGE.Text = "(" + (FRM_RPT_PRINT_CHOICE.rmt_pcount1 + k) + " / " + nPAGECOUNT.ToString() + ")";
                //}
                //else
                //{
                //    if (fHYOUSI == true)
                //    {
                //        this.LB_PAGE.Text = "(" + (FRM_RPT_PRINT_CHOICE.rmt_pcount1 + k + 1) + " / " + nPAGECOUNT.ToString() + ")";
                //    }
                //    else
                //    {
                //        this.LB_PAGE.Text = "(" + (FRM_RPT_PRINT_CHOICE.rmt_pcount1 + k) + " / " + nPAGECOUNT.ToString() + ")";
                //    }
                //}
                //k++;
            }
            else //プレピュー
            {
                //if (fHYOUSI == true)
                //{
                //    this.LB_PAGE.Text = "(" + (FRM_SHIJI_VIEW.rmt_pcount + k + 1) + " / " + nPAGECOUNT.ToString() + ")";
                //}
                //else
                //{
                //    this.LB_PAGE.Text = "(" + (FRM_SHIJI_VIEW.rmt_pcount + k) + " / " + nPAGECOUNT.ToString() + ")";
                //}
                //k++;

                //if (fkyoten == true)
                //{
                //    this.LB_PAGE.Text = "(1 / 1)";
                //}
            }

        }

        #endregion

        #region show_hankou()
        private void show_hankou()
        {
            DataTable db = new DataTable();
            string sql_new = string.Empty;
            sql_new = "";
            sql_new += "SELECT";
            sql_new += " sTANTOUSHA as sTANTOUSHA";
            sql_new += ",sMAIL AS sMAIL";
            sql_new += ",sIMAGE1 AS sIMAGE1";
            sql_new += " FROM m_j_tantousha";
            sql_new += " WHERE cTANTOUSHA='" + this.cHENKOUSYA + "'";

            //db.Autoitem(sql_new, "m_j_tantousha", DEMO20BasicClass.DBConnector.conn);

            if (HANKO_Check == "欄有り(担当印有り)")
            {
                if (db.Rows.Count > 0)
                {
                    if (db.Rows[0]["sIMAGE1"] != null)
                    {
                        if (!string.IsNullOrEmpty(db.Rows[0]["sIMAGE1"].ToString()))
                        {
                            //P_sIMAGEHankou.Image = Image.FromStream(func.toImage((byte[])db.Rows[0]["sIMAGE1"]));
                            P_sIMAGEHankou.PictureAlignment = PictureAlignment.Center;
                        }
                    }
                }
            }
        }
        #endregion

        public void getMitumoriData(String cMitumori)
        {
            String sql_rm = "SELECT cMITUMORI as cMITUMORI," +
                         " cSHIHARAI as cSHIHARAI, " +
                         " fSAKUSEISYA as fSAKUSEISYA," +
                         " cSAKUSEISYA as cSAKUSEISYA," +
                         " cEIGYOTANTOSYA as cEIGYOTANTOSYA," +
                         " cTOKUISAKI as cTOKUISAKI," +
                         " ifnull(sTOKUISAKI,'')  as sTOKUISAKI," +
                         " ifnull(sMITUMORIKENMEI,'') as sMITUMORIKENMEI," +
                         " ifnull(sMITUMORI,'') as sMITUMORI," +
                         " ifnull(sMITUMORINOKI,'') as sMITUMORINOKI," +
                         " ifnull(sMITUMORIYUKOKIGEN,'') as sMITUMORIYUKOKIGEN," +
                         " ifnull(sUKEWATASIBASYO,'') as sUKEWATASIBASYO," +
                         " nMITUMORISYOHIZE as nMITUMORISYOHIZE," +
                         " nKAZEIKINGAKU as nKAZEIKINGAKU," +
                         " ifnull(sTOKUISAKI_TAN,'') as sTOKUISAKI_TAN," +
                         " ifnull(sTOKUISAKI_KEISYO,'') as sTOKUISAKI_KEISYO," +
                         " ifnull(sTOKUISAKI_YAKUSHOKU,'') as sTOKUISAKI_YAKUSHOKU," +
                         " ifnull(sTOKUISAKI_TANBUMON,'') as sTOKUISAKI_TANBUMON," +
                         " ifnull(sBIKOU,'') as sBIKOU," +
                         " dMITUMORISAKUSEI as dMITUMORISAKUSEI," +
                         " nMITUMORINEBIKI as nMITUMORINEBIKI," +
                         " nKIRI_G as nKIRI_G," +
                         " nTANKA_G as nTANKA_G," +
                         " nKINGAKU as nKINGAKU," +
                         " fSYUUKEI as fSYUUKEI" +
                         " FROM r_mitumori" +
                         " Where cMITUMORI = '0000000001'; ";
            con.Open();
            MySqlCommand cmd_rm = new MySqlCommand(sql_rm, con);
            cmd_rm.CommandTimeout = 0;
            MySqlDataAdapter da_rm = new MySqlDataAdapter(cmd_rm);
            da_rm.Fill(dt_rm);
            da_rm.Dispose();
            con.Close();
        }
    }
}
