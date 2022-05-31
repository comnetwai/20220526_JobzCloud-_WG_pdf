using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using GrapeCity.ActiveReports.Drawing;
using GrapeCity.ActiveReports.SectionReportModel;
using System.Data;
using Service;
using MySql.Data.MySqlClient;
using System.IO;
using GrapeCity.ActiveReports;

namespace jobzcolud.pdf
{
    /// <summary>
    /// seikyuusho の概要の説明です。
    /// </summary>
    public partial class seikyuusho : GrapeCity.ActiveReports.SectionReport
    {
        MySqlConnection con = null;
        MySqlCommand cmd = null;
        MySqlDataAdapter da = null;

        private string nouhinlabel = "納品日";
        private bool fnouhinbi = false;
        public string sNOUHN { private get; set; }
        private int mr = 0;
        public string stitle { private get; set; }
        public string cTOKUISAKI { private get; set; }
        public string sTOKUISAKI { private get; set; }
        public string sSEIKYUSAKI { private get; set; }
        public string cSEIKYU_YUUBIN { private get; set; }
        public string sSEIKYU_JUUSHO1 { private get; set; }
        public string sSEIKYU_JUUSHO2 { private get; set; }
        public string sSEIKYU_TEL { private get; set; }
        public string sTOKUISAKI_TAN { private get; set; }
        public string sTOKUISAKI_TAN_jun = "0";
        public string sTOKUISAKI_TANBUMON { private get; set; }
        public string sSEIKYU_YAKUSYOKU { private get; set; }
        public DataTable dbl_sekyu = new DataTable();
        public bool f_kingakuari = true;
        public decimal nuriagekingaku { private get; set; }
        public decimal nnebiki { private get; set; }
        public decimal nshyohizei { private get; set; }
        public decimal nshoukei { private get; set; }
        public decimal kingaku { private get; set; }
        public decimal kazeikingaku { private get; set; }
        public bool fhikazei = false;
        public string cEIGYOTANTOSYA { private get; set; }
        public int row_count { private get; set; }
        public string bikou { private get; set; }
        private const int FIRSTMAXROWS = 28;//見積商品明細数据第一页显示的最大行数[25]
        private const int MAXROWS = 39;     //見積商品明細数据其它页显示的最大行数[36]
        private int row = 0;
        public int fINS = 0;
        DataTable dbl_result = new DataTable();
        DataTable dbl_r_u_m = new DataTable(); //見積商品明細 
        public DataTable dbl_r_u_m_table = new DataTable();

        public string cMITUMORI { private get; set; }                                //見積コード
        public string cMITUMORI_KO { private get; set; }                             //見積子番号
        public string imagecheck { private get; set; }
        DataTable dbl_m = new DataTable();                           //見積 表数据集
        DataTable dbl_m_s_info = new DataTable();                    //書類基本情報 表数据集
        public bool fKAKE = true;                                                    //掛け率印刷フラグ
        public int nPAGECOUNT = 0;                                                   //页码
        //---------------------------------------------------------------------------------
        double nMITUMORISYOHIZE = 0;                                                 //消费税
        DataTable dbl_m_j_info = new DataTable();                    //自社情報マスター表

        private System.Drawing.Font f1 = new System.Drawing.Font("ＭＳ 明朝", float.Parse("10"));
        private System.Drawing.Font f2 = new System.Drawing.Font("HG明朝B", float.Parse("12"));  
        private System.Drawing.Font f3 = new System.Drawing.Font("HG明朝B", float.Parse("10"));  
        private System.Drawing.Font f4 = new System.Drawing.Font("ＭＳ 明朝", float.Parse("12"));
        private System.Drawing.Font f8 = new System.Drawing.Font("ＭＳ 明朝", float.Parse("10.5"));

       // public DEMO20DataClass.r_mitumori_Class rm = new DEMO20DataClass.r_mitumori_Class();
        public int NEWIMAGE = 0;
        private int RowsCount = 0;
        public String dMITUMORISAKUSEI = ""; //将印刷页面的发行日期传递过来　発行日
        public String dINVOICE = "";// 納品日
        //public DEMO20BasicClass.TokuisakiInfoBean tib = new DEMO20BasicClass.TokuisakiInfoBean();

        public bool fhiduke { private get; set; }

        public Boolean fSEIKYUU = false;//請求先登録チェック

        public string cURIAGE = "";
        public string frogoimage { private get; set; }
        public string fbikou { private get; set; }
        public string ckyoten { private get; set; }
        public string sSEIKYU_KEISYO { private get; set; }

        public string loginId = "";

        public seikyuusho()
        {
            //
            // デザイナー サポートに必要なメソッドです。
            //
            InitializeComponent();
            if (f3.Name != "HG明朝B")
            {
                f3 = new System.Drawing.Font("ＭＳ 明朝", float.Parse("10"));
            }
            if (f2.Name != "HG明朝B")
            {
                f2 = new System.Drawing.Font("ＭＳ 明朝", float.Parse("12"));
            }
        }

        #region seikyuusho_ReportStart
        private void seikyuusho_ReportStart(object sender, EventArgs e)
        {
            if (stitle == "納品書と請求書")
            {
                LB_S_sMITUMORI.Text = "請  求  書";
            }
            else
            {
                LB_S_sMITUMORI.Text = "納品書兼請求書";
            }

            this.PageSettings.Margins.Top = 0.25F;

            #region ＭＳ 明朝 

            LB_NO.Font = f8;
            LB_S_sNAIYOU.Font = f4;
            LB_nSURYO.Font = f8;
            LB_sTANI.Font = f8;
            LB_nSIKIRITANKA.Font = f8;
            LB_S_nKINGAKU1.Font = f8;

            #endregion

            Printing();

            int maxrow = dbl_result.Rows.Count;

            int newmaxrow = FIRSTMAXROWS;

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
            for (int rowind = maxrow; rowind < newmaxrow; rowind++)
            {
                dbl_result.Rows.Add();
            }
            //getTokuisaki();20170705 WaiWai delete
            pagecount();
        }
        #endregion

        #region 行数」
        private int LineCount(string str)
        {
            //StringSplitOptions.None including empty row
            //StringSplitOptions.RemoveEmptyEntries not including empty row
            return str.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries).Length;
        }
        #endregion

        #region 印刷赋值
        private void Printing()
        {
            string sql_m = ""; //用于查询“見積”表的sql文。
            string select_query = ""; //用于查询“見積商品明細”表和“单位”表的sql文。

            string sql_m_s_info = "";//用于查询“書類基本情報”表的sql文。
            string sql_m_j_info = "";//自社情報マスター表
            DataTable db1 = new DataTable();
            DataTable db2 = new DataTable();

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

            if (frogoimage == "1")
            {
                sql_m_j_info += ",sIMAGE1 as sIMAGE";
                sql_m_j_info += " ,ifnull(fIMAGESize1,'0') as fIMAGESize";
            }
            else if (frogoimage == "2")
            {
                sql_m_j_info += " ,sIMAGE2 as sIMAGE";
                sql_m_j_info += " ,ifnull(fIMAGESize2,'0') as fIMAGESize";
            }
            else if (frogoimage == "3")
            {
                sql_m_j_info += " ,sIMAGE3 as sIMAGE";
                sql_m_j_info += " ,ifnull(fIMAGESize3,'0') as fIMAGESize";
            }
            else if (frogoimage == "4")
            {
                sql_m_j_info += " ,sIMAGE4 as sIMAGE";
                sql_m_j_info += " ,ifnull(fIMAGESize4,'0') as fIMAGESize";
            }
            else if (frogoimage == "5")
            {
                sql_m_j_info += " ,sIMAGE5 as sIMAGE";
                sql_m_j_info += " ,ifnull(fIMAGESize5,'0') as fIMAGESize";
            }
            else
            {
                sql_m_j_info += " ,'' as sIMAGE";
                sql_m_j_info += " ,'0' as fIMAGESize";
            }
            sql_m_j_info += " FROM M_J_INFO";
            sql_m_j_info += " WHERE cCO='" + ckyoten + "'";

            JC_ClientConnecction_Class jc = new JC_ClientConnecction_Class();
            jc.loginId = loginId;//Session["LoginId"].ToString();
            con = jc.GetConnection();
            con.Open();

            cmd = new MySqlCommand(sql_m_s_info, con);
            cmd.CommandTimeout = 0;
            dbl_m_s_info = new DataTable();
            da = new MySqlDataAdapter(cmd);
            da.Fill(dbl_m_s_info);
            da.Dispose();

            cmd = new MySqlCommand(sql_m_j_info, con);
            cmd.CommandTimeout = 0;
            dbl_m_j_info = new DataTable();
            da = new MySqlDataAdapter(cmd);
            da.Fill(dbl_m_j_info);
            da.Dispose();

            con.Close();

            try
            {
                LB_cMITUMORI.Text = cURIAGE;  //报表 右上角：見積No.赋值
                #region print
                sql_m = "select ";
                sql_m += " ifnull(date_format(ru.dURIAGE,'%Y/%m/%d'),'') as dURIAGE";
                sql_m += ",ifnull(ru.snouhin,'') as snouhin";
                sql_m += ",ru.cSEIKYUSAKI as cTOKUISAKI";
                sql_m += ",ru.sSEIKYUSAKI as sTOKUISAKI";
                sql_m += ",ifnull(ru.sSEIKYU_TAN,'') as sTOKUISAKI_TAN";
                sql_m += ",ifnull(ru.sSEIKYU_YAKUSHOKU,'') as sSEIKYU_YAKUSHOKU";  
                sql_m += ",ifnull(ru.sSEIKYU_TANBUMON,'') as sTOKUISAKI_TANBUMON";
                sql_m += ",ifnull(ru.f_kingaku_p,'0') as f_kingaku_p";
                sql_m += ",ru.cSEIKYUSAKI as cSEIKYUSAKI";
                sql_m += ",ru.sSEIKYUSAKI as sSEIKYUSAKI";
                sql_m += ",ru.sSEIKYU_TAN as sSEIKYU_TAN";
                sql_m += ",ru.sSEIKYU_TANBUMON as sSEIKYU_TANBUMON";
                sql_m += ",ifnull(ru.nuriage_kingaku,0) as nuriage_kingaku";
                sql_m += ",ifnull(ru.nnebiki_kingaku,0) as nnebiki_kingaku";
                sql_m += ",ifnull(ru.nsyoukei_kingkau,0) as nsyoukei_kingkau";
                sql_m += ",ifnull(ru.cSEIKYU_YUUBIN,'') as cYUUBIN";
                sql_m += ",ifnull(ru.sSEIKYU_JUUSHO1,'') as sJUUSHO1";
                sql_m += ",ifnull(ru.sSEIKYU_JUUSHO2,'') as sJUUSHO2";
                sql_m += ",ifnull(ru.sSEIKYU_TEL,'') as sTEL";
                sql_m += ",ifnull(ru.sbikou,'') as sbikou";
                sql_m += ",ifnull(ru.nsyohizei,'0') as nsyohizei";
                sql_m += ",ifnull(ru.nKINGAKU,'0') as nKINGAKU";
                sql_m += ",ifnull(ru.cEIGYOTANTOSYA,'') as cEIGYOTANTOSYA";
                sql_m += ",ifnull(mjt.sTANTOUSHA,'') as sTANTOUSHA";
                sql_m += ", mjt.sMAIL AS sMAIL ";
                sql_m += ",ifnull(mt.fSEIKYUUBIKOUSHIYOU,'0') as fSEIKYUUBIKOUSHIYOU";
                sql_m += ",ifnull(mt.cFURIKOMISAKI,'00') as cFURIKOMISAKI"; 
                sql_m += ",ifnull(ru.nKAZEIKINGAKU,'0') as nKAZEIKINGAKU";
                
                sql_m += ",IFNULL(ru.sSEIKYU_KEISYO,'') as sSEIKYU_KEISYO ";

                sql_m += " from r_uriage as ru";
                sql_m += " inner join m_tokuisaki as mt on ru.cSEIKYUSAKI=mt.cTOKUISAKI";
                sql_m += " left join m_j_tantousha as mjt on ru.cEIGYOTANTOSYA=mjt.cTANTOUSHA";
                sql_m += " where ru.cURIAGE='" + cURIAGE + "'";


                #region set data r_uriage_m
                select_query = "select rum.cSYOUHIN as cSYOUHIN";
                select_query += ",rum.sSYOUHIN_R as sSYOUHIN_R";
                select_query += ",rum.nSURYO as nSURYO";
                select_query += ",rum.sTANI as sTANI";
                select_query += ",rum.nSIKIRITANKA as nSIKIRITANKA";
                select_query += ",rum.nSIKIRIKINGAKU as nSIKIRIKINGAKU";
                select_query += ",rum.skubun as skubun";
                select_query += ",rum.sbikou as sbikou";
                select_query += ",ifnull(rum.fKazei,'0') as fKazei";
                select_query += " from r_uriage_m as rum";
                select_query += " where rum.cURIAGE='" + cURIAGE + "'";
                #endregion
                
                if (con != null)
                {
                    con.Open();

                    cmd = new MySqlCommand(sql_m, con);
                    cmd.CommandTimeout = 0;
                    dbl_m = new DataTable();
                    da = new MySqlDataAdapter(cmd);
                    da.Fill(dbl_m);
                    da.Dispose();

                    cmd = new MySqlCommand(select_query, con);
                    cmd.CommandTimeout = 0;
                    dbl_r_u_m = new DataTable();
                    da = new MySqlDataAdapter(cmd);
                    da.Fill(dbl_r_u_m);
                    da.Dispose();
                    con.Close();

                    data_store();
                    if (dbl_m.Rows.Count > 0)
                    {
                        LB_sNOUHN.Text = dbl_m.Rows[0]["snouhin"].ToString();

                        if (dbl_m.Rows[0]["sTANTOUSHA"].ToString() != "")
                        {
                            Fields["sTANTOUSHA"].Value = "担当:" + dbl_m.Rows[0]["sTANTOUSHA"].ToString(); //担当者名
                        }
                        else
                        {
                            Fields["sTANTOUSHA"].Value = "担当:" + ""; //担当者名
                        }

                        #region 「請求先を登録する場合は請求先を表示、請求先がnullの場合は得意先を表示する」


                        if (dbl_m.Rows[0]["sSEIKYUSAKI"].ToString() != "")
                        {
                            Fields["sTOKUISAKI"].Value = dbl_m.Rows[0]["sTOKUISAKI"].ToString();         //請求先
                            if (dbl_m.Rows[0]["sSEIKYU_KEISYO"].ToString() != "")
                            {
                                if (dbl_m.Rows[0]["sSEIKYU_KEISYO"].ToString() == "")
                                {
                                    dbl_m.Rows[0]["sSEIKYU_KEISYO"] = " 様";
                                }

                                string sSEIKYU_YAKUSHOKU = ""; 
                                if (dbl_m.Rows[0]["sSEIKYU_YAKUSHOKU"].ToString() != "") 
                                {
                                    sSEIKYU_YAKUSHOKU = dbl_m.Rows[0]["sSEIKYU_YAKUSHOKU"].ToString() + " ";
                                }

                                //Fields["sTOKUISAKI_TAN"].Value = dbl_m.Tables[0].Rows[0]["sTOKUISAKI_TAN"].ToString() + " " + dbl_m.Tables[0].Rows[0]["SKEISHOU"].ToString();    //請求先担当者
                                Fields["sTOKUISAKI_TAN"].Value = sSEIKYU_YAKUSHOKU + dbl_m.Rows[0]["sTOKUISAKI_TAN"].ToString() + " " + dbl_m.Rows[0]["sSEIKYU_KEISYO"].ToString();    //請求先担当者
                            }
                            Fields["sSEIKYU_TANBUMON"].Value = dbl_m.Rows[0]["sTOKUISAKI_TANBUMON"].ToString();//請求先担当者部門

                            TokuisakiName(dbl_m.Rows[0]["cTOKUISAKI"].ToString());

                            if (dbl_m.Rows[0]["sTOKUISAKI_TAN"].ToString() == "")
                            {
                                label13.Text = "";
                            }
                            else
                            {
                                label1.Text = "";
                                Fields["fSAMA"].Value = "";          //得意先フラグ
                            }

                            if (!string.IsNullOrEmpty(dbl_m.Rows[0]["cYUUBIN"].ToString()))
                            {
                                if (dbl_m.Rows[0]["cYUUBIN"].ToString().Length > 3)
                                {
                                    LB_sTOKUISAKIYUBIN.Value = "〒" + dbl_m.Rows[0]["cYUUBIN"].ToString().Substring(0, 3) + "-" + dbl_m.Rows[0]["cYUUBIN"].ToString().Substring(3);     //郵便番号
                                }
                                else
                                {
                                    LB_sTOKUISAKIYUBIN.Value = "〒" + dbl_m.Rows[0]["cYUUBIN"].ToString();     //郵便番号
                                }
                            }
                            else
                            {
                                LB_sTOKUISAKIYUBIN.Value = "";     //郵便番号
                            }
                            LB_sTOKUISAKIJUSYO1.Value = dbl_m.Rows[0]["sJUUSHO1"].ToString().Trim().Replace("\r\n", "").Replace("\r", "").Replace("\n", "") + "\r\n" + dbl_m.Rows[0]["sJUUSHO2"].ToString().Trim().Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
                            Fields["sTELH"].Value = "TEL:" + dbl_m.Rows[0]["sTEL"].ToString();

                            Fields["cTOKUISAKI"].Value = dbl_m.Rows[0]["cTOKUISAKI"].ToString();
                        }
                        else
                        {
                            //getTokuisaki();
                        }

                        #endregion
                        

                        #region
                        if (dbl_m.Rows[0]["cFURIKOMISAKI"].ToString() == "00")
                        {
                            string sbikoudisplay = get_BIKOU("1");
                            Fields["sBIKOU"].Value = sbikoudisplay;
                        }
                        else
                        {
                            if (dbl_m.Rows[0]["fSEIKYUUBIKOUSHIYOU"].ToString() == "1")
                            {
                                string sbikoudisplay = get_BIKOU("1");
                                Fields["sBIKOU"].Value = sbikoudisplay;
                            }
                            else
                            {

                                string sbikoudisplay = get_BIKOU("2");

                                DataTable dbl_m_furikomisaki = new DataTable();
                                string furikomisaki = "";
                                furikomisaki = "Select";
                                furikomisaki += " sKINYUUKIKAN as sKINYUUKIKAN";
                                furikomisaki += ",sSHITEN as sSHITEN";
                                furikomisaki += ",ifnull(fYOKINKAMOKU,1) as fYOKINKAMOKU";
                                furikomisaki += ",sKOUZABANGOU as sKOUZABANGOU";
                                furikomisaki += ",sKOUZAMEIGI as sKOUZAMEIGI";
                                furikomisaki += " from m_furikomisaki ";
                                furikomisaki += " where cFURIKOMISAKI='" + dbl_m.Rows[0]["cFURIKOMISAKI"].ToString() + "'";

                                con.Open();
                                cmd = new MySqlCommand(furikomisaki, con);
                                cmd.CommandTimeout = 0;
                                da = new MySqlDataAdapter(cmd);
                                da.Fill(dbl_m_furikomisaki);
                                da.Dispose();
                                con.Close();

                                string fYOKINKAMOKU = "普通";
                                if (dbl_m_furikomisaki.Rows.Count > 0)
                                {
                                    if (dbl_m_furikomisaki.Rows[0]["fYOKINKAMOKU"].ToString() == "1")
                                    {
                                        fYOKINKAMOKU = "普通";
                                    }
                                    else
                                    {
                                        fYOKINKAMOKU = "当座";
                                    }
                                    Fields["sBIKOU"].Value = "振込先　" + dbl_m_furikomisaki.Rows[0]["sKINYUUKIKAN"].ToString() + "　" + dbl_m_furikomisaki.Rows[0]["sSHITEN"].ToString() + "\r\n" +
                                                                "　　　　" + fYOKINKAMOKU + "　口座番号 " + dbl_m_furikomisaki.Rows[0]["sKOUZABANGOU"].ToString() + "\r\n" +
                                                                "　　　　" + "口座名義 " + dbl_m_furikomisaki.Rows[0]["sKOUZAMEIGI"].ToString() + "\r\n" +
                                                                 sbikoudisplay;
                                }
                                else
                                {
                                    Fields["sBIKOU"].Value = "振込先　" + "　" + "\r\n" +
                                                                "　　　　" + fYOKINKAMOKU + "　口座番号 " + "\r\n" +
                                                                "　　　　" + "口座名義 " + "\r\n" +
                                                                 sbikoudisplay;
                                }
                            }
                        }
                        #endregion

                        if (fhiduke == true)
                        {
                            if (fnouhinbi == true)
                            {
                                LB_dMITUMORISAKUSEI.Text = dMITUMORISAKUSEI;
                                label15.Text = nouhinlabel + "：";
                                LB_date.Text = dINVOICE;
                                if (dINVOICE == "")
                                {
                                    LB_DateLabel.Visible = false;
                                }
                            }
                            else
                            {
                                LB_dMITUMORISAKUSEI.Text = "";
                                label15.Text = "";
                                LB_date.Text = dMITUMORISAKUSEI;
                            }
                        }
                        else
                        {
                            LB_dMITUMORISAKUSEI.Text = "";
                            label15.Text = "";
                            LB_date.Text = "";
                            LB_DateLabel.Visible = false;
                        }

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

                        Fields["nuriage_kingaku"].Value = dbl_m.Rows[0]["nuriage_kingaku"].ToString();          //仕切合計
                        //Fields["nnebiki_kingaku"].Value = dbl_m.Tables[0].Rows[0]["nnebiki_kingaku"].ToString();          //nebiki
                        Fields["nsyoukei_kingkau"].Value = dbl_m.Rows[0]["nsyoukei_kingkau"].ToString();          //小計                        
                        Fields["nsyohizei"].Value = dbl_m.Rows[0]["nsyohizei"].ToString();      //消費税
                        Fields["nKINGAKU2"].Value = dbl_m.Rows[0]["nKINGAKU"].ToString(); ////お見積合計額＝小計＋消費税
                        if (fhikazei == true)
                        {
                            label14.Visible = true;
                            textBox4.Visible = true;
                            Fields["kazeikingaku"].Value = dbl_m.Rows[0]["nKAZEIKINGAKU"].ToString();
                        }
                        else
                        {
                            label14.Text = "";
                        }
                        if (dbl_m.Rows[0]["nnebiki_kingaku"].ToString() == "0" || dbl_m.Rows[0]["nnebiki_kingaku"].ToString() == "0.00")
                        {
                            LB_nebiki.Text = "";
                            Fields["nnebiki_kingaku"].Value = "";
                        }
                        else
                        {
                            LB_nebiki.Text = "出精値引";
                            Fields["nnebiki_kingaku"].Value = dbl_m.Rows[0]["nnebiki_kingaku"].ToString();          //nebiki
                        }
                    }

                }
                #endregion
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }

            if (dbl_m_s_info.Rows.Count > 0)
            {
                Fields["sNAIYOU"].Value = dbl_m_s_info.Rows[0][1].ToString();    //内容欄
                Fields["sKEN"].Value = dbl_m_s_info.Rows[0][2].ToString();       //件名欄
                Fields["sNOUKI"].Value = dbl_m_s_info.Rows[0][3].ToString();     //納期欄
                Fields["sYUUKOU"].Value = dbl_m_s_info.Rows[0][4].ToString();    //有効期限欄 
                Fields["sSHIHARAI"].Value = dbl_m_s_info.Rows[0][5].ToString();  //支払条件欄
                Fields["sUKEBASYOU"].Value = dbl_m_s_info.Rows[0][6].ToString(); //受渡し場所欄 
            }

            if (dbl_m_j_info.Rows.Count > 0)
            {
                if (!String.IsNullOrEmpty(dbl_m_j_info.Rows[0]["cYUUBIN"].ToString()))
                {
                    if (dbl_m_j_info.Rows[0]["cYUUBIN"].ToString().Length > 3)
                    {
                        Fields["cYUUBIN"].Value = "〒" + dbl_m_j_info.Rows[0]["cYUUBIN"].ToString().Substring(0, 3) + "-" + dbl_m_j_info.Rows[0]["cYUUBIN"].ToString().Substring(3);     //郵便番号
                    }
                    else
                    {
                        Fields["cYUUBIN"].Value = "〒" + dbl_m_j_info.Rows[0]["cYUUBIN"].ToString();     //郵便番号
                    }
                }
                else
                {
                    Fields["cYUUBIN"].Value = "";
                }
                Fields["sCO"].Value = dbl_m_j_info.Rows[0]["sCO"].ToString();
                Fields["sJUUSHO1"].Value = dbl_m_j_info.Rows[0]["sJUUSHO1"].ToString();              //住所１
                Fields["sJUUSHO2"].Value = dbl_m_j_info.Rows[0]["sJUUSHO2"].ToString();              //住所2
                if (dbl_m_j_info.Rows[0]["sJUUSHO2"].ToString().Trim() == "")
                {
                    if (dbl_m_j_info.Rows[0]["sTEL"].ToString() != "")
                    {
                        LB_TEL2.Value = "TEL:" + dbl_m_j_info.Rows[0]["sTEL"].ToString();             //電話番号
                    }
                    if (dbl_m_j_info.Rows[0]["sFAX"].ToString() != "")
                    {
                        LB_FAX2.Value = "FAX:" + dbl_m_j_info.Rows[0]["sFAX"].ToString();             //ファックス番号
                    }
                    if (fINS == 0)
                    {
                        if (dbl_m.Rows[0]["sMAIL"].ToString() != "")
                        {
                            LB_MAI2.Value = "MAIL:" + dbl_m.Rows[0]["sMAIL"].ToString();//メールアドレス
                        }
                        else
                        {
                            if (dbl_m_j_info.Rows.Count > 0)
                            {
                                //担当者のメールアドレスがnullの場合は代表メールアドレスを表示
                                if (dbl_m_j_info.Rows[0]["sMAIL"].ToString() != "")
                                {
                                    LB_MAI2.Value = "MAIL:" + dbl_m_j_info.Rows[0]["sMAIL"].ToString();
                                }
                            }
                        }
                    }
                    else if (fINS == 1)
                    {
                        if (db2.Rows.Count > 0)
                        {
                            if (db2.Rows[0]["sMAIL"].ToString() != "")
                            {
                                LB_MAI2.Value = "MAIL:" + db2.Rows[0]["sMAIL"].ToString();//メールアドレス
                            }
                            Fields["sMAIL"].Value = "";
                        }
                        else
                        {
                            if (dbl_m_j_info.Rows.Count > 0)
                            {
                                //担当者のメールアドレスがnullの場合は代表メールアドレスを表示
                                if (dbl_m_j_info.Rows[0]["sMAIL"].ToString() != "")
                                {
                                    LB_MAI2.Value = "MAIL:" + dbl_m_j_info.Rows[0]["sMAIL"].ToString();
                                }
                            }

                        }

                    }
                    Fields["sMAIL"].Value = "";
                    //Fields["sTANTOUSHA"].Value = "";
                    Fields["sURL"].Value = "";
                }
                else
                {
                    if (dbl_m_j_info.Rows[0]["sTEL"].ToString() != "")
                    {
                        Fields["sTEL"].Value = "TEL:" + dbl_m_j_info.Rows[0]["sTEL"].ToString();             //電話番号
                    }
                    if (dbl_m_j_info.Rows[0]["sFAX"].ToString() != "")
                    {
                        Fields["sFAX"].Value = "FAX:" + dbl_m_j_info.Rows[0]["sFAX"].ToString();             //ファックス番号
                    }
                    if (dbl_m.Rows[0]["sTANTOUSHA"].ToString() != "")//担当者がnullの場合は表示しない
                    {
                        LB_DAN.Value = "担当:" + dbl_m.Rows[0]["sTANTOUSHA"].ToString(); //担当者名
                    }

                    Fields["sTANTOUSHA"].Value = "";
                    Fields["sURL"].Value = "";
                }
               

                if (dbl_m_j_info.Rows[0]["sIMAGE"].ToString() != "")
                {

                    //P_sIMAGE.Image = Image.FromStream(func.toImage((byte[])dbl_m_j_info.Tables[0].Rows[0]["sIMAGE"]));
                    if (dbl_m_j_info.Rows[0]["fIMAGESize"].ToString() != "0")
                    {
                        byte[] bytes = (byte[])dbl_m_j_info.Rows[0]["sIMAGE"];
                        MemoryStream stream = new MemoryStream(bytes);

                        P_sIMAGE2.Image = System.Drawing.Image.FromStream(stream);
                        P_sIMAGE2.PictureAlignment = PictureAlignment.TopLeft;
                        label16.Visible = false;
                        LB_cYUUBIN.Visible = false;
                        LB_sJUUSHO1.Visible = false;
                        label3.Visible = false;
                        LB_FAX2.Visible = false;
                        LB_TEL2.Visible = false;
                        LB_sTEL.Visible = false;
                        LB_sFAX.Visible = false;
                        LB_MAI2.Visible = false;
                        LB_DAN.Visible = false;
                        LB_DAN.Visible = false;
                        LB_sURL.Visible = false;
                        LB_sTANTOUSHA.Visible = false;
                        LB_SMAIL.Visible = false;
                    }
                    else
                    {
                        byte[] bytes = (byte[])dbl_m_j_info.Rows[0]["sIMAGE"];
                        MemoryStream stream = new MemoryStream(bytes);

                        P_sIMAGE.Image = System.Drawing.Image.FromStream(stream);
                        P_sIMAGE.PictureAlignment = PictureAlignment.TopLeft;
                    }
                    P_sIMAGE.PictureAlignment = PictureAlignment.TopLeft;
                }
                else
                {
                    P_sIMAGE.Image = null;
                }
            }

            kaigyoucode();
        }
        #endregion

        #region seikyuusho_DataInitialize
        /// <summary>
        /// 使用DataInitialize事件添加fields。该事件在ReportStart 事件之后触发。使用它向报表的字段集合添加自定义字段。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void seikyuusho_DataInitialize(object sender, EventArgs e)
        {
            Fields.Add("sNAIYOU");            //内容欄
            Fields.Add("sBIKOU");             //明細備考欄
            Fields.Add("sKEN");               //件名欄
            Fields.Add("sNOUKI");             //納期欄
            Fields.Add("sYUUKOU");            //有効期限欄
            Fields.Add("sSHIHARAI");          //支払条件欄
            Fields.Add("sUKEBASYOU");         //受渡し場所欄
            Fields.Add("sTOKUISAKI");         //得意先名
            Fields.Add("sTOKUISAKI_TAN");     //得意先担当者
            Fields.Add("dMITUMORINOKI");      //納期
            Fields.Add("sMITUMORIYUKOKIGEN"); //有効期限
            Fields.Add("cSHIHARAI");          //支払条件
            Fields.Add("sUKEWATASIBASYO");    //受渡し場所
            Fields.Add("nRITU");             //掛け率 
            Fields.Add("nsyohizei");   //消費税
            Fields.Add("sTANTOUSHA");         //担当者
            Fields.Add("skubun");  
            Fields.Add("sSYOUHIN_R");         //内容.仕様----商品名 
            Fields.Add("nSURYO");             //数量 整数部分
            Fields.Add("nSURYO2");            //数量 小数部分
            Fields.Add("sTANI");              //単位 
            Fields.Add("nTANKA");             //単価
            Fields.Add("nSIKIRITANKA");       //仕切単価
            Fields.Add("nSIKIRIKINGAKU");           //金額           
            Fields.Add("nsyoukei_kingkau");          //小計
            Fields.Add("nKINGAKU2");          //お見積合計額
            Fields.Add("nSOUGOUKEI");         //税込金額
            Fields.Add("nnebiki_kingaku");         //税込金額
            Fields.Add("nuriage_kingaku");             //明細合計
            Fields.Add("nTANKA_G");
            Fields.Add("cYUUBIN");            //郵便番号
            Fields.Add("sJUUSHO1");           //住所１
            Fields.Add("sJUUSHO2");           //住所2
            Fields.Add("sTEL");               //電話番号
            Fields.Add("sFAX");               //ファックス番号
            Fields.Add("sURL");               //ホームページURL
            Fields.Add("sMAIL");              //メールアドレス            
            Fields.Add("sSEIKYU_TANBUMON");          //請求先担当者部門
            Fields.Add("fSAMA");//得意先様、御中フラグ
            Fields.Add("sCO");        
            Fields.Add("sTELH");               //電話番号
            Fields.Add("cTOKUISAKI");
            Fields.Add("kazeikingaku");
        }
        #endregion

        #region pageHeader_BeforePrint
        int k = 1;
        private void pageHeader_BeforePrint(object sender, EventArgs e)
        {
            if (p > 1)
            {
                this.LB_PAGE2.Text = "(" + (k + 1) + " / " + nPAGECOUNT.ToString() + ")";
                k++;
            }
            else
            {
                p++;
            }
            if (PageNumber < 2)
            {
                label3.Visible = false;
            }
            else
            {
                label3.Visible = true;
            }
        }
        #endregion

        #region detail_Format
        private void detail_Format(object sender, EventArgs e)
        {
            #region
            if (fINS == 0)
            {
                if (row >= (FIRSTMAXROWS + (PageNumber - 1) * MAXROWS) && row != dbl_result.Rows.Count)
                {
                    detail.NewPage = NewPage.After;
                }
                else
                {
                    detail.NewPage = NewPage.None;
                }
            }
            else
            {
                if (dbl_result.Rows.Count > 28)
                {
                    if (row >= (FIRSTMAXROWS + (PageNumber - 1) * MAXROWS) && row != dbl_result.Rows.Count)
                    {
                        detail.NewPage = NewPage.After;
                    }
                    else
                    {
                        detail.NewPage = NewPage.None;
                    }
                }
                else
                {

                    if (row >= (25 + (PageNumber - 1) * 36) && row != dbl_result.Rows.Count)
                    {
                        detail.NewPage = NewPage.After;
                    }
                    else
                    {
                        detail.NewPage = NewPage.None;
                    }
                }
            }
            #endregion
        }
        #endregion

        #region TokuisakiName
        private void TokuisakiName(string cTOKUISAKI)
        {
            DataTable db3 = new DataTable();//得意先フラグのため
            string sql_new = "";
            sql_new = "";
            sql_new += "SELECT";
            sql_new += " CASE WHEN fSAMA='1' THEN '様' ELSE '御中' END as fSAMA";
            sql_new += " FROM m_tokuisaki";
            sql_new += " WHERE cTOKUISAKI='" + cTOKUISAKI + "'";

            con.Open();
            cmd = new MySqlCommand(sql_new, con);
            cmd.CommandTimeout = 0;
            da = new MySqlDataAdapter(cmd);
            da.Fill(db3);
            da.Dispose();
            con.Close();

            if (db3.Rows.Count > 0)
            {
                Fields["fSAMA"].Value = db3.Rows[0]["fSAMA"].ToString();          //得意先フラグ
            }
            else
            {
                Fields["fSAMA"].Value = "御中";          //得意先フラグ
            }

        }
        #endregion
                
        #region pageFooter_BeforePrint
        private void pageFooter_BeforePrint(object sender, EventArgs e)
        {
            //if (fINS == 0)//印刷
            //{
            //    if (rmt_pcount1 > 0)
            //    {
            //        this.LB_PAGE.Text = (rmt_pcount1 + k) + " / " + nPAGECOUNT.ToString();
            //    }
            //    else
            //    {

            //        this.LB_PAGE.Text = (rmt_pcount1 + k) + " / " + nPAGECOUNT.ToString();

            //    }
            //    k++;
            //}
        }
        #endregion
        
        #region seikyuusho_FetchData

        int j = 1;
        private void seikyuusho_FetchData(object sender, FetchEventArgs eArgs)
        {
            try
            {
                if (row < dbl_result.Rows.Count)
                {

                    this.Fields["skubun"].Value = dbl_result.Rows[row]["skubun"].ToString();
                    this.Fields["sSYOUHIN_R"].Value = dbl_result.Rows[row]["sSYOUHIN_R"].ToString();
                    if (dbl_result.Rows[row]["nSURYO"].ToString() != "")
                    {
                        string str = Double.Parse(dbl_result.Rows[row]["nSURYO"].ToString()).ToString("0.##");

                        Fields["nSURYO"].Value = str;
                        Fields["nSURYO2"].Value = str;

                        if (Fields["nSURYO"].Value.ToString() == "-0")
                        {
                            LB_nSURYO.OutputFormat = null;
                        }
                        else
                        {
                            int length = getDecPoint(decimal.Parse(dbl_result.Rows[row]["nSURYO"].ToString()));

                            if (length == 0)
                            {
                                LB_nSURYO.OutputFormat = "#,##0";
                            }
                            else if (length == 1)
                            {
                                LB_nSURYO.OutputFormat = "#,##0.0";
                            }
                            else if (length == 2)
                            {
                                LB_nSURYO.OutputFormat = "#,##0.00";
                            }
                        }
                    }
                    else
                    {
                        this.Fields["nSURYO"].Value = dbl_result.Rows[row]["nSURYO"].ToString();
                    }
                    this.Fields["sTANI"].Value = dbl_result.Rows[row]["sTANI"].ToString();
                    if (f_kingakuari == true)
                    {       
                        if (dbl_result.Rows[row]["skubun"].ToString() == "返品" || dbl_result.Rows[row]["skubun"].ToString() == "値引")
                        {
                            decimal nsikirikingaku = 0;
                            if (dbl_result.Rows[row]["nSIKIRIKINGAKU"].ToString() != "")
                            {
                                nsikirikingaku = decimal.Parse(dbl_result.Rows[row]["nSIKIRIKINGAKU"].ToString());
                                nsikirikingaku = (-nsikirikingaku);
                            }
                            decimal nSIKIRITANKA = 0;
                            if (dbl_result.Rows[row]["nSIKIRITANKA"].ToString() != "")
                            {
                                nSIKIRITANKA = decimal.Parse(dbl_result.Rows[row]["nSIKIRITANKA"].ToString());
                                nSIKIRITANKA = (-nSIKIRITANKA);
                            }
                            this.Fields["nSIKIRIKINGAKU"].Value = nsikirikingaku;
                            this.Fields["nSIKIRITANKA"].Value = nSIKIRITANKA;
                        }
                        else
                        {
                            this.Fields["nSIKIRIKINGAKU"].Value = dbl_result.Rows[row]["nSIKIRIKINGAKU"].ToString();
                            this.Fields["nSIKIRITANKA"].Value = dbl_result.Rows[row]["nSIKIRITANKA"].ToString();
                        }
                    }
                    else
                    {
                        this.Fields["nSIKIRITANKA"].Value = "";
                        this.Fields["nSIKIRIKINGAKU"].Value = "";

                    }

                    int check_r = row + 1;
                    if (row == 1)
                    {
                        lbl_point.Visible = true;
                    }
                    else
                    {
                        lbl_point.Visible = false;
                    }
                    row++;
                    eArgs.EOF = false;
                }

            }
            catch
            {
                eArgs.EOF = true;
            }
        }
        #endregion

        #region ChangeFont
        private void ChangeFont(System.Drawing.Font fond)
        {
            LB_S_sNAIYOU.Font = fond;
        }
        #endregion
                
        #region pagecount
        private void pagecount()
        {
            if (dbl_result.Rows.Count <= 25)
            {
                nPAGECOUNT = 1;
            }
            else
            {
                int n = (dbl_result.Rows.Count - 28) / 39;
                for (int i = 0; i < n + 1; i++)
                {
                    if ((dbl_result.Rows.Count - 28) - i * 39 <= 39)
                    {
                        nPAGECOUNT = i + 1;
                    }
                    else
                    {
                        nPAGECOUNT = i + 1;
                    }
                }
            }
        }

        #endregion

        #region reportHeader1_BeforePrint
        int p = 0;
        private void reportHeader1_BeforePrint(object sender, EventArgs e)
        {
            this.LB_PAGE.Text = "(" + (p + 1) + " / " + nPAGECOUNT.ToString() + ")";
            p++;
        }
        #endregion

        #region columnCreate
        private void columnCreate()
        {
            dbl_result.Clear();
            dbl_result = new DataTable();
            dbl_result.Columns.Add("cSYOUHIN");
            dbl_result.Columns.Add("sSYOUHIN_R");
            dbl_result.Columns.Add("skubun");
            dbl_result.Columns.Add("nSURYO");
            dbl_result.Columns.Add("sTANI");
            dbl_result.Columns.Add("nSIKIRITANKA");
            dbl_result.Columns.Add("nSIKIRIKINGAKU");
            dbl_result.Columns.Add("sbikou");
            dbl_result.Columns.Add("fkazei");
        }
        #endregion

        #region data_store
        private void data_store()
        {
            columnCreate();
            if (fINS == 0)
            {
                #region print
                if (dbl_r_u_m.Rows.Count > 0)
                {
                    row_count = dbl_r_u_m.Rows.Count;
                    for (int i = 0; i < dbl_r_u_m.Rows.Count; i++)
                    {
                        if (dbl_r_u_m.Rows[i]["skubun"].ToString() == "売上" && string.IsNullOrEmpty(dbl_r_u_m.Rows[i]["sSYOUHIN_R"].ToString())
                                && string.IsNullOrEmpty(dbl_r_u_m.Rows[i]["cSYOUHIN"].ToString()) && (string.IsNullOrEmpty(dbl_r_u_m.Rows[i]["nSURYO"].ToString())
                                || dbl_r_u_m.Rows[i]["nSURYO"].ToString() == "0.00" || dbl_r_u_m.Rows[i]["nSURYO"].ToString() == "0"))
                        {
                            continue;
                        }
                        else
                        {
                            dbl_result.Rows.Add();
                            int r = dbl_result.Rows.Count - 1;
                            dbl_result.Rows[r]["cSYOUHIN"] = dbl_r_u_m.Rows[i]["cSYOUHIN"].ToString();
                            dbl_result.Rows[r]["sSYOUHIN_R"] = dbl_r_u_m.Rows[i]["sSYOUHIN_R"].ToString();
                            if (string.IsNullOrEmpty(dbl_r_u_m.Rows[i]["nSURYO"].ToString()) || dbl_r_u_m.Rows[i]["nSURYO"].ToString() == "0.00"
                                 || dbl_r_u_m.Rows[i]["nSURYO"].ToString() == "0")
                            {
                                dbl_result.Rows[r]["nSURYO"] = "";
                                dbl_result.Rows[r]["sTANI"] = "";
                                dbl_result.Rows[r]["nSIKIRIKINGAKU"] = "";
                                dbl_result.Rows[r]["nSIKIRIKINGAKU"] = "";
                                dbl_result.Rows[r]["skubun"] = "";
                            }
                            else
                            {
                                dbl_result.Rows[r]["nSURYO"] = dbl_r_u_m.Rows[i]["nSURYO"].ToString();
                                dbl_result.Rows[r]["sTANI"] = dbl_r_u_m.Rows[i]["sTANI"].ToString();

                                if (dbl_r_u_m.Rows[i]["skubun"].ToString() != "売上")
                                {
                                    dbl_result.Rows[r]["skubun"] = dbl_r_u_m.Rows[i]["skubun"].ToString();
                                }
                                else
                                {
                                    dbl_result.Rows[r]["skubun"] = "";
                                }

                                if (string.IsNullOrEmpty(dbl_r_u_m.Rows[i]["nSIKIRITANKA"].ToString()) || dbl_r_u_m.Rows[i]["nSIKIRITANKA"].ToString() == "0.00"
                                    || dbl_r_u_m.Rows[i]["nSIKIRITANKA"].ToString() == "0")
                                {
                                    dbl_result.Rows[r]["nSIKIRITANKA"] = "";
                                    dbl_result.Rows[r]["nSIKIRIKINGAKU"] = 0;

                                }
                                else
                                {
                                    dbl_result.Rows[r]["nSIKIRITANKA"] = Convert.ToDecimal(dbl_r_u_m.Rows[i]["nSIKIRITANKA"].ToString());
                                    dbl_result.Rows[r]["nSIKIRIKINGAKU"] = Convert.ToDecimal(dbl_r_u_m.Rows[i]["nSIKIRIKINGAKU"].ToString());
                                }
                            }
                            //dbl_result.Rows[r]["skubun"] = dbl_r_u_m.Tables[0].Rows[i]["skubun"].ToString(); //delete by yamin 20181004
                            dbl_result.Rows[r]["sbikou"] = dbl_r_u_m.Rows[i]["sbikou"].ToString();
                            if (dbl_r_u_m.Rows[i]["fKazei"].ToString() == "1")
                            {
                                fhikazei = true;
                            }
                            //}
                        }
                    }
                }
                else
                {
                    row_count = 0;
                    for (int i = 0; i < 24; i++)
                    {
                        dbl_result.Rows.Add();
                    }
                }
                #endregion
            }
        }
        #endregion

        #region kaigyoucode
        private void kaigyoucode()
        {
            try
            {

                //件名
                LB_sNOUHN.Text = LB_sNOUHN.Text.Trim().Replace("\r\n", "CRLFu000Du000A").Replace("\r", "").Replace("\n", "").Replace("CRLFu000Du000A", "\r\n");

                //得意先
                Fields["sTOKUISAKI"].Value = Fields["sTOKUISAKI"].Value.ToString().Trim().Replace("\r\n", "CRLFu000Du000A").Replace("\r", "").Replace("\n", "").Replace("CRLFu000Du000A", "\r\n");
                //得意先担当者
                Fields["sTOKUISAKI_TAN"].Value = Fields["sTOKUISAKI_TAN"].Value.ToString().Trim().Replace("\r\n", "").Replace("\r", "").Replace("\n", "");

                //得意先担当者部門
                Fields["sSEIKYU_TANBUMON"].Value = Fields["sSEIKYU_TANBUMON"].Value.ToString().Trim().Replace("\r\n", "").Replace("\r", "").Replace("\n", "");

            }
            catch { }

        }

        #endregion

        #region getDecPoint
        private int getDecPoint(decimal val)
        {
            int i = 0;
            while (Math.Round(val, i) != val)
                i++;
            return i;
        }
        #endregion

        #region get_BIKOU
        //获取自社基本情报的备考
        private string get_BIKOU(string conreturn)
        {
            string sBIKOU = "";
            DataTable dbl = new DataTable();
            String sql = "";
            sql += "SELECT";
            if (fbikou == "1")
            {
                sql += " ifnull(sBIKOU,'') AS sBIKOU";
                sql += ",ifnull(sfurikomisakibikou,'') AS sfurikomisakibikou";
            }
            else if (fbikou == "2")
            {
                sql += " ifnull(sBIKOU2,'') AS sBIKOU";
                sql += " ,ifnull(sfurikomisakibikou2,'') AS sfurikomisakibikou";
            }
            else if (fbikou == "3")
            {
                sql += " ifnull(sBIKOU3,'') AS sBIKOU";
                sql += ",ifnull(sfurikomisakibikou3,'') AS sfurikomisakibikou"; 
            }
            else if (fbikou == "4")
            {
                sql += " ifnull(sBIKOU4,'') AS sBIKOU";
                sql += ",ifnull(sfurikomisakibikou4,'') AS sfurikomisakibikou";
            }
            else if (fbikou == "5")
            {
                sql += " ifnull(sBIKOU5,'') AS sBIKOU";
                sql += ",ifnull(sfurikomisakibikou5,'') AS sfurikomisakibikou";
            }
            else
            {
                sql += " '' AS sBIKOU";
                sql += ",'' AS sfurikomisakibikou"; 
            }
            sql += ", IFNULL(sSEIKYUUSHO,'') AS sSEIKYUUSHO";
            if (stitle == "納品書と請求書")
            {
                sql += ",ifnull(sSEIKYUUSHO,'') AS sKENSEIKYUUSYO";
            }
            else
            {
                sql += ",ifnull(sKENSEIKYUUSYO,'') AS sKENSEIKYUUSYO";
            }
            sql += ",ifnull(fseikyudatehyouji,'0') AS fseikyudatehyouji"; 
            sql += ",ifnull(snouhinbilabel,'') AS snouhinbilabel"; 

            sql += " FROM m_j_info ";
            sql += "WHERE cCO='01'";

            con.Open();
            cmd = new MySqlCommand(sql, con);
            cmd.CommandTimeout = 0;
            da = new MySqlDataAdapter(cmd);
            da.Fill(dbl);
            da.Dispose();
            con.Close();

            if (dbl.Rows.Count > 0)
            {
                if (conreturn == "1")
                {
                    sBIKOU = dbl.Rows[0]["sBIKOU"].ToString();
                }
                else
                {
                    sBIKOU = dbl.Rows[0]["sfurikomisakibikou"].ToString();
                }

                LB_S_sMITUMORI.Text = dbl.Rows[0]["sKENSEIKYUUSYO"].ToString();

                nouhinlabel = dbl.Rows[0]["snouhinbilabel"].ToString(); 
                if (dbl.Rows[0]["fseikyudatehyouji"].ToString() == "0") 
                {
                    fnouhinbi = true;
                }
                else
                {
                    fnouhinbi = false;
                }
            }

            sBIKOU = sBIKOU + "\r\n" + bikou;
            return sBIKOU;
        }
        #endregion
    }
}
