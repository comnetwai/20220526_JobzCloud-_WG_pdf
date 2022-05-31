using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Service;
using MySql.Data.MySqlClient;
using System.Data;
using GrapeCity.ActiveReports.SectionReportModel;
using System.IO;

namespace jobzcolud.pdf
{
    /// <summary>
    /// uriage の概要の説明です。
    /// </summary>
    public partial class uriage : GrapeCity.ActiveReports.SectionReport
    {
        MySqlConnection con = null;
        private string j_info = "1";
        private string genka = "0";
        public int fINS = 0;
        public string loginId="";
        public string cURIAGE = "";
        public string logintantou = "";
        public string ckyoten = "";
        public string frogoimage = "";//1,2,3,4,5     //ロゴ combobox
        public string seikyubikou = ""; // 拠点 combobox
        //public string hidzukeari = "";
        //public string kingaku = "";
        public string bikou = "";
        public string sTOKUISAKI_YAKUSYOKU = "";
        public string sTOKUISAKI_KEISYO = "";
        
        public decimal nsyoujizei = 0;
        public decimal nuriage_kingaku = 0;

        int counter = 0;
        int row_count = 0;
        int p = 0;
        
        private int nPAGECOUNT = 1;
        public decimal ngoukeikingaku = 0;
        public bool f_kingakuari = true;
        public bool fhiduke = true;

        public string sinvoice="";
        public string sseikyusho = "";
        public string curiage = "";
        public string duriage = "";


        int mr = 0;
        DataTable dt_rum_info = new DataTable();
        DataTable dbl_result = new DataTable();
        DataTable dt_ru_info = new DataTable();
        DataTable dt_mj_info = new DataTable();
        DataTable dt_mj_info1 = new DataTable();

        public uriage()
        {
            //
            // デザイナー サポートに必要なメソッドです。
            //
            InitializeComponent();
        }

        #region uriage_ReportStart
        private void uriage_ReportStart(object sender, EventArgs e)
        {
            //this.Document.Printer.PrinterName = "";
            //this.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.A4;

            this.PageSettings.Margins.Left = 0.1f;
            this.PageSettings.Margins.Right = 0.1f;
            this.PageSettings.Margins.Top = 0.4f;
            //this.PageSettings.Margins.Bottom = 0.5f;
            
            string strSql = "";
            strSql = "select ";
            strSql += "ifnull(mji.sCO,'') AS sCO";
            strSql += ",ifnull(mji.cYUUBIN,'') AS cYUUBIN";
            strSql += ",ifnull(mji.sJUUSHO1,'') AS sJUUSHO1";
            strSql += ",ifnull(mji.sJUUSHO2,'') AS sJUUSHO2";
            strSql += ",ifnull(mji.sTEL,'') AS sTEL";
            strSql += ",ifnull(mji.sFAX,'') AS sFAX";
            //strSql += " ,ifnull(fIMAGESize1,'0') as fIMAGESize";
            //strSql += " from m_j_info mji ";
            //strSql += "WHERE mji.cCO='" + ckyoten + "'";
            //strSql += ",ifnull(mji.fkeisantani,'1') AS fkeisantani";
            //strSql += ",ifnull(mji.sINVOICE,'') AS sINVOICE";  //add by yamin 20180611         

            if (frogoimage == "1")
            {
                strSql += " ,sIMAGE1 as sIMAGE";
                strSql += " ,ifnull(fIMAGESize1,'0') as fIMAGESize";
            }
            else if (frogoimage == "2")
            {
                strSql += " ,sIMAGE2 as sIMAGE";
                strSql += " ,ifnull(fIMAGESize2,'0')as fIMAGESize";
            }
            else if (frogoimage == "3")
            {
                strSql += " ,sIMAGE3 as sIMAGE";
                strSql += " ,ifnull(fIMAGESize3,'0') as fIMAGESize";
            }
            else if (frogoimage == "4")
            {
                strSql += " ,sIMAGE4 as sIMAGE";
                strSql += " ,ifnull(fIMAGESize4,'0') as fIMAGESize";
            }
            else if (frogoimage == "5")
            {
                strSql += " ,sIMAGE5 as sIMAGE";
                strSql += " ,ifnull(fIMAGESize5,'0') as fIMAGESize";
            }
            else
            {
                strSql += " ,'' as sIMAGE";
                strSql += " ,'0' as fIMAGESize";
            }

            strSql += " from m_j_info mji ";
            strSql += "WHERE mji.cCO='" + ckyoten + "'";

            string strSql1 = "";
            strSql1 += "SELECT ";
            strSql1 += "ifnull(mji.fkeisantani,'1') AS fkeisantani";
            //strSql1 += ",ifnull(mji.fgentankatanka,'0') AS fgentankatanka";
            strSql1 += ",ifnull(mji.sINVOICE,'') AS sINVOICE";
            strSql1 += ",ifnull(mji.sSEIKYUUSHO,'') AS sSEIKYUUSHO";
            strSql1 += " FROM m_j_info mji";
            strSql1 += " WHERE 1=1 ";
            strSql1 += "AND mji.cCO='01'";

            string sql_m = "";

            sql_m += "select ";
            sql_m += " ifnull(date_format(ru.dURIAGE,'%Y/%m/%d'),'') as dURIAGE";

            //sql_m += ",IF(ru.cTOKUISAKI=rm.cTOKUISAKI, ifnull(rm.sTOKUISAKIYUBIN,''), ifnull(mt.cYUUBIN,'')) AS cYUUBIN";
            //sql_m += ",IF(ru.cTOKUISAKI=rm.cTOKUISAKI, ifnull(rm.sTOKUISAKIJUSYO,''), CONCAT(IF(mt.sJUUSHO1 IS NULL or mt.sJUUSHO1 = '', '', CONCAT(mt.sJUUSHO1,'\r\n')),mt.sJUUSHO2)) AS sJUUSHO1";
            //sql_m += ",IF(ru.cTOKUISAKI=rm.cTOKUISAKI, ifnull(rm.sTOKUISAKITEL,''), ifnull(mt.sTEL,'')) AS sTEL";
            sql_m += ",ifnull(mt.cYUUBIN,'') AS cYUUBIN";
            sql_m += ",CONCAT(IF(mt.sJUUSHO1 IS NULL or mt.sJUUSHO1 = '', '', CONCAT(mt.sJUUSHO1,'\r\n')),mt.sJUUSHO2) AS sJUUSHO1";
            sql_m += ",ifnull(mt.sTEL,'') AS sTEL";
            sql_m += ",IF(ru.cTOKUISAKI=rm.cTOKUISAKI, ifnull(rm.sTOKUISAKI,''), ifnull(ru.sTOKUISAKI,'')) AS sTOKUISAKI";

            sql_m += ",ifnull(mt.cTOKUISAKI,'') as cTOKUISAKI";
            sql_m += ",ifnull(ru.snouhin,'') as snouhin";
            sql_m += ",ifnull(ru.sTOKUISAKI_TAN,'') as sTOKUISAKI_TAN";
            sql_m += ",ifnull(ru.sTOKUISAKI_TANBUMON,'') as sTOKUISAKI_TANBUMON";
            sql_m += ",ifnull(ru.sTOKUISAKI_YAKUSYOKU ,'') as sTOKUISAKI_YAKUSYOKU ";
            sql_m += ",IFNULL(ru.sTOKUISAKI_KEISYO,'') as sTOKUISAKI_KEISYO ";
            sql_m += ",ifnull(ru.f_kingaku_p,'0') as f_kingaku_p";
            sql_m += ",ru.cSEIKYUSAKI as cSEIKYUSAKI";
            sql_m += ",ru.sSEIKYUSAKI as sSEIKYUSAKI";
            sql_m += ",ru.sSEIKYU_TAN as sSEIKYU_TAN";
            sql_m += ",ru.sSEIKYU_TANBUMON as sSEIKYU_TANBUMON";
            sql_m += ",ifnull(ru.nKINGAKU,0) as nKINGAKU";
            sql_m += ",ifnull(ru.nsyohizei,0) as nsyohizei";
            
            sql_m += ",ifnull(ru.sbikou,'') as sbikou";
            sql_m += ",ifnull(ru.nsyohizei,'0') as nsyohizei";
            sql_m += ",ifnull(ru.nKINGAKU,'0') as nKINGAKU";
            
            sql_m += ", ifnull(ru.nsyoukei_kingkau,'0') as nuriage_kingaku";
            sql_m += ",ifnull(ru.cEIGYOTANTOSYA,'') as cEIGYOTANTOSYA";
            sql_m += ",ifnull(mjt.sTANTOUSHA,'') as sTANTOUSHA";
            sql_m += " from r_uriage as ru";
            sql_m += " inner join m_tokuisaki as mt on ru.cTOKUISAKI=mt.cTOKUISAKI";
            sql_m += " left join m_j_tantousha as mjt on ru.cEIGYOTANTOSYA=mjt.cTANTOUSHA";
            sql_m += " LEFT JOIN r_uri_mitsu AS rum ON rum.cURIAGE=ru.cURIAGE";
            sql_m += " LEFT JOIN R_MITUMORI AS rm ON rum.cMITUMORI=rm.cMITUMORI ";

            sql_m += " LEFT JOIN R_BU_MITSU AS rbm ON rm.cMITUMORI=rbm.cMITUMORI";
            sql_m += " LEFT JOIN r_bukken AS rb ON rbm.cBUKKEN=rb.cBUKKEN";
            sql_m += " where ru.cURIAGE='" + cURIAGE + "'";
           
            string select_query = "select rum.cSYOUHIN as cSYOUHIN";
            select_query += ",rum.sSYOUHIN_R as sSYOUHIN_R";
            select_query += ",rum.nSURYO as nSURYO";
            select_query += ",rum.sTANI as sTANI";
            select_query += ",rum.nSIKIRITANKA as nSIKIRITANKA";
            select_query += ",rum.nSIKIRIKINGAKU as nSIKIRIKINGAKU";
            select_query += ",rum.skubun as skubun";
            select_query += ",rum.sbikou as sbikou";
            select_query += " from r_uriage_m as rum";
            select_query += " where rum.cURIAGE='" + cURIAGE + "'";

            JC_ClientConnecction_Class jc = new JC_ClientConnecction_Class();
            jc.loginId = loginId;
            con = jc.GetConnection();
            con.Open();

            MySqlCommand cmd_mj_info = new MySqlCommand(strSql, con);
            cmd_mj_info.CommandTimeout = 0;
            dt_mj_info = new DataTable();
            MySqlDataAdapter da_mj_info = new MySqlDataAdapter(cmd_mj_info);
            da_mj_info.Fill(dt_mj_info);

            MySqlCommand cmd_mj_info1 = new MySqlCommand(strSql1, con);
            cmd_mj_info1.CommandTimeout = 0;
            dt_mj_info1 = new DataTable();
            MySqlDataAdapter da_mj_info1 = new MySqlDataAdapter(cmd_mj_info1);
            da_mj_info1.Fill(dt_mj_info1);

            MySqlCommand cmd_ru_info = new MySqlCommand(sql_m, con);
            cmd_ru_info.CommandTimeout = 0;
            dt_ru_info = new DataTable();
            MySqlDataAdapter da_ru_info = new MySqlDataAdapter(cmd_ru_info);
            da_ru_info.Fill(dt_ru_info);

            MySqlCommand cmd_s_info = new MySqlCommand(select_query, con);
            cmd_s_info.CommandTimeout = 0;
            dt_rum_info = new DataTable();
            MySqlDataAdapter da_s_info = new MySqlDataAdapter(cmd_s_info);
            da_s_info.Fill(dt_rum_info);

            con.Close();
            dt_mj_info.Dispose();
            dt_mj_info1.Dispose();
            da_ru_info.Dispose();
            da_s_info.Dispose();
            dt_rum_info.Dispose();

            JC27UriageTouroku_Class settei = new JC27UriageTouroku_Class();
            settei.cTANTOUSHA = logintantou;
            DataTable dt_gettantou = settei.GetTantou();
            if (dt_gettantou.Rows.Count > 0)
            {
                if (dt_gettantou.Rows[0]["kingaku"].ToString() == "1")//金額あり・無
                {
                    f_kingakuari = true;
                }
                else
                {
                    f_kingakuari = false;
                }

                if (dt_gettantou.Rows[0]["hiduke"].ToString().TrimEnd() == "1") //日付あり・無
                {
                    fhiduke = true;
                }
                else
                {
                    fhiduke = false;
                }
            }

            if (dt_ru_info.Rows.Count > 0)
            {
                data_store();
                #region todelete

                //if (dt_ru_info.Rows[0]["kingaku"].ToString() == "1")//金額あり・無
                //{
                //    f_kingakuari = true;
                //}
                //else
                //{
                //    f_kingakuari = false;
                //}

                //if (dt_ru_info.Rows[0]["hiduke"].ToString() == "1")//日付あり・無
                //{
                //    fhiduke = true;
                //}
                //else
                //{
                //    fhiduke = false;
                //}
               #endregion

                Fields["cTOKUISAKI"].Value = dt_ru_info.Rows[0]["cTOKUISAKI"].ToString();
                TokuisakiName(dt_ru_info.Rows[0]["cTOKUISAKI"].ToString());
                if (dt_ru_info.Rows[0]["sTOKUISAKI_TAN"].ToString() != "")
                {
                    LB_ONCHU1.Visible = false;
                    Fields["sTOKUISAKI"].Value = dt_ru_info.Rows[0]["sTOKUISAKI"].ToString();
                    if (dt_ru_info.Rows[0]["sTOKUISAKI_YAKUSYOKU"].ToString() != "")
                    {
                        sTOKUISAKI_YAKUSYOKU = dt_ru_info.Rows[0]["sTOKUISAKI_YAKUSYOKU"].ToString() + " ";
                    }
                    if (dt_ru_info.Rows[0]["sTOKUISAKI_KEISYO"].ToString().Trim() == "")
                    {
                        sTOKUISAKI_KEISYO = " 様";
                    }
                    else
                    {
                        sTOKUISAKI_KEISYO = " " + dt_ru_info.Rows[0]["sTOKUISAKI_KEISYO"].ToString();
                    }
                    Fields["sTOKUISAKI_TANBUMON"].Value = dt_ru_info.Rows[0]["sTOKUISAKI_TANBUMON"].ToString();
                    Fields["sTOKUISAKI_TAN"].Value = sTOKUISAKI_YAKUSYOKU + dt_ru_info.Rows[0]["sTOKUISAKI_TAN"].ToString() + sTOKUISAKI_KEISYO; //modify by yamin 20210407
                }
                else
                {
                   // LB_sama.Visible = false;
                    Fields["sTOKUISAKI"].Value = dt_ru_info.Rows[0]["sTOKUISAKI"].ToString();
                }
                string yu_string = dt_ru_info.Rows[0]["cYUUBIN"].ToString();
                if (yu_string != "")
                {
                    string yu_string1 = "";
                    string yu_string2 = "";
                    int length = yu_string.Length;
                    if (length > 3)
                    {
                        yu_string1 = yu_string.Substring(0, 3);
                        yu_string2 = yu_string.Substring(3);
                        Fields["cYUUBIN"].Value = "〒" + yu_string1 + "-" + yu_string2;
                    }
                    else
                    {
                        Fields["cYUUBIN"].Value = "〒" + yu_string;
                    }
                }
                else
                {
                    Fields["cYUUBIN"].Value = ""; 
                }
                Fields["sJUUSHO1"].Value = dt_ru_info.Rows[0]["sJUUSHO1"].ToString();                
               // Fields["sTEL"].Value = "TEL:" + dt_ru_info.Rows[0]["sTEL"].ToString();
                
                Fields["cURIAGE"].Value = cURIAGE;
                if (fhiduke == true)
                {
                    Fields["dURIAGE"].Value = dt_ru_info.Rows[0]["dURIAGE"].ToString();
                }
                else
                {
                    Fields["dURIAGE"].Value = "";
                }
                LB_sNOUHN.Text = dt_ru_info.Rows[0]["snouhin"].ToString();
                nsyoujizei = Decimal.Parse(dt_ru_info.Rows[0]["nsyohizei"].ToString());
                ngoukeikingaku = Decimal.Parse(dt_ru_info.Rows[0]["nKINGAKU"].ToString());
                nuriage_kingaku = Decimal.Parse(dt_ru_info.Rows[0]["nuriage_kingaku"].ToString());
            }

            if (dt_mj_info1.Rows.Count > 0)
            {
                if (dt_mj_info.Rows.Count > 0)
                {
                    j_info = dt_mj_info1.Rows[0]["fkeisantani"].ToString();
                    //genka = dt_mj_info1.Rows[0]["fgentankatanka"].ToString();
                    LB_Report_Header.Text = dt_mj_info1.Rows[0]["sINVOICE"].ToString();
                    Fields["sCO"].Value = dt_mj_info.Rows[0]["sCO"].ToString();
                    string yu_string = dt_mj_info.Rows[0]["cYUUBIN"].ToString();
                    if (yu_string != "")
                    {
                        string yu_string1 = "";
                        string yu_string2 = "";
                        int length = yu_string.Length;
                        if (length > 3)
                        {
                            yu_string1 = yu_string.Substring(0, 3);
                            yu_string2 = yu_string.Substring(3);
                            Fields["yuu"].Value = "〒" + yu_string1 + "-" + yu_string2;
                        }
                        else
                        {
                            Fields["yuu"].Value = "〒" + yu_string;
                        }
                    }
                    else
                    {
                        Fields["yuu"].Value = "";
                    }

                    Fields["juu1"].Value = dt_mj_info.Rows[0]["sJUUSHO1"].ToString();
                    Fields["juu2"].Value = dt_mj_info.Rows[0]["sJUUSHO2"].ToString();
                    
                    if (dt_mj_info.Rows[0]["sJUUSHO2"].ToString().Trim() == "")
                    {
                        Fields["tel"].Value = "TEL:" + dt_mj_info.Rows[0]["sTEL"].ToString();
                        Fields["fax"].Value = "FAX:" + dt_mj_info.Rows[0]["sFAX"].ToString();
                        if (fINS == 0)
                        {
                            Fields["seigyou"].Value = "担当 : " + dt_ru_info.Rows[0]["sTANTOUSHA"].ToString();
                        }
                        else
                        {
                           // Fields["seigyou"].Value = "担当 ： " + seigyou;
                        }
                    }
                    else
                    {
                        Fields["tel2"].Value = "TEL:" + dt_mj_info.Rows[0]["sTEL"].ToString();
                        Fields["fax2"].Value = "FAX:" + dt_mj_info.Rows[0]["sFAX"].ToString();
                        if (fINS == 0)
                        {
                            Fields["seigyou2"].Value = "担当 : " + dt_ru_info.Rows[0]["sTANTOUSHA"].ToString();
                        }
                        else
                        {
                           // Fields["seigyou2"].Value = "担当 ： " + seigyou;
                        }
                    }
                    
                    if (dt_mj_info.Rows[0]["sIMAGE"].ToString() != "")
                    {
                        if (dt_mj_info.Rows[0]["fIMAGESize"].ToString() != "0")
                        {
                            byte[] bytes = (byte[])dt_mj_info.Rows[0]["sIMAGE"];
                            MemoryStream stream = new MemoryStream(bytes);
                            P_sIMAGE2.Image = System.Drawing.Image.FromStream(stream);
                            P_sIMAGE2.PictureAlignment = PictureAlignment.Center;
                            //P_sIMAGE2.Image = Image.FromStream(func.toImage((byte[])dt_mj_info.Rows[0]["sIMAGE"]));
                            //P_sIMAGE2.PictureAlignment = PictureAlignment.TopLeft;
                            //label10.Visible = false;
                            //label24.Visible = false;
                            //label23.Visible = false;
                            //label6.Visible = false;
                            //label9.Visible = false;
                            //label22.Visible = false;
                            //label14.Visible = false;
                            //label25.Visible = false;
                            //label26.Visible = false;
                            //label27.Visible = false;

                            if (fINS == 0)
                            {
                                Fields["seigyou2"].Value = "担当 : " + dt_ru_info.Rows[0]["sTANTOUSHA"].ToString();
                            }
                            else
                            {
                               // Fields["seigyou2"].Value = "担当 ： " + seigyou;
                            }
                        }
                        else
                        {
                            byte[] bytes = (byte[])dt_mj_info.Rows[0]["sIMAGE"];
                            MemoryStream stream = new MemoryStream(bytes);
                            P_sIMAGE.Image = System.Drawing.Image.FromStream(stream);
                            P_sIMAGE.PictureAlignment = PictureAlignment.TopLeft;
                        }
                    }
                    else
                    {
                        P_sIMAGE.Image = null;
                        P_sIMAGE2.Image = null;
                    }
                }
            }
            

            if (dbl_result.Rows.Count > 0)
            {
                if (row_count > 0)
                {
                    if (f_kingakuari == true)
                    {
                        if (j_info == "1")
                    {
                        dbl_result.Rows.Add();
                        dbl_result.Rows[dbl_result.Rows.Count - 1]["sSYOUHIN_R"] = "消費税";
                        dbl_result.Rows[dbl_result.Rows.Count - 1]["nSIKIRIKINGAKU"] = nsyoujizei;
                    }
                    }
                }
                int minrow = dbl_result.Rows.Count;
                int page = minrow % 24;
                if (page > 0)
                {
                    for (mr = page; mr < 24; mr++)
                    {
                        dbl_result.Rows.Add();
                    }
                }
            }
            else if (dbl_result.Rows.Count == 0)
            {
                for (mr = 0; mr < 23; mr++)
                {
                    dbl_result.Rows.Add();
                }
            }
            pagecount();
        }
        #endregion

        #region pagecount()
        private void pagecount()
        {
            if (dbl_result.Rows.Count > 0)
            {
                if (dbl_result.Rows.Count > 24)
                {
                    int maxrow = dbl_result.Rows.Count;
                    int page = maxrow / 24;
                    int plus_page = maxrow % 24;
                    if (plus_page > 0 && plus_page < 24)
                    {
                        nPAGECOUNT = page + 1;
                    }
                    else
                    {
                        nPAGECOUNT = page;
                    }
                }
            }
        }
        #endregion

        #region TokuisakiName
        private void TokuisakiName(string cTOKUISAKI)
        {
            string sql_new = "";
            sql_new = "";
            sql_new += "SELECT";
            sql_new += " CASE WHEN fSAMA='1' THEN '様' ELSE '御中' END as fSAMA";
            sql_new += " FROM m_tokuisaki";
            sql_new += " WHERE cTOKUISAKI='" + cTOKUISAKI + "'";

            MySqlCommand cmd = new MySqlCommand(sql_new, con);
            cmd.CommandTimeout = 0;
            DataTable db3 = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            da.Fill(db3);

            if (db3.Rows.Count > 0)
            {
                Fields["fSAMA"].Value = db3.Rows[0]["fSAMA"].ToString();  //得意先フラグ
            }
            else
            {
                Fields["fSAMA"].Value = "御中";  //得意先フラグ
            }

        }
        #endregion

        #region uriage_DataInitialize
        private void uriage_DataInitialize(object sender, EventArgs e)
        {
            Fields.Add("sCO"); 
            Fields.Add("yuu");           
            Fields.Add("tel");
            Fields.Add("tel2");
            Fields.Add("juu1");
            Fields.Add("fax"); 
            Fields.Add("fax2");
            Fields.Add("juu2");
            Fields.Add("seigyou");
            Fields.Add("seigyou2");
            Fields.Add("sTOKUISAKI");
            Fields.Add("cYUUBIN");
            Fields.Add("sJUUSHO1");
            Fields.Add("sJUUSHO2");
            Fields.Add("sTEL");
            Fields.Add("cTOKUISAKI");
            Fields.Add("cSYOUHIN");
            Fields.Add("sSYOUHIN_R");
            Fields.Add("skubun");
            Fields.Add("nSURYO");
            Fields.Add("nSURYO2");
            Fields.Add("sTANI");
            Fields.Add("nSIKIRITANKA");
            Fields.Add("nSIKIRIKINGAKU");
            Fields.Add("sbikou");
            Fields.Add("cURIAGE");
            Fields.Add("dURIAGE");
            Fields.Add("sTOKUISAKI_TAN");
            Fields.Add("sTOKUISAKI_TANBUMON");
            Fields.Add("nKINGAKU");
            Fields.Add("fSAMA");
        }
        #endregion

        #region detail_Format
        private void detail_Format(object sender, EventArgs e)
        {
            //if (dbl_result.Rows.Count > 24)
            //{
            //    if (counter >= (FIRSTMAXROWS + (PageNumber - 1) * MAXROWS) && counter != dbl_result.Rows.Count)
            //    {
            //        // Make new page and update record count.
            //        detail.NewPage = NewPage.After;
            //    }
            //    else
            //    {
            //        // No new page
            //        detail.NewPage = NewPage.None;
            //    }
            //}
            //else
            //{
            //    if (counter >= (24 + (PageNumber - 1) * 24) && counter != dbl_result.Rows.Count)
            //    {
            //        // Make new page and update record count.
            //        detail.NewPage = NewPage.After;
            //    }
            //    else
            //    {
            //        // No new page
            //        detail.NewPage = NewPage.None;

            //    }
            //}
        }
        #endregion

        #region columnCreate()
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
        }
        #endregion

        #region data_store()
        private void data_store()
        {
            columnCreate();
            if (dt_rum_info.Rows.Count > 0)
            {
                row_count = dt_rum_info.Rows.Count;
                for (int i = 0; i < dt_rum_info.Rows.Count; i++)
                {
                    if (dt_rum_info.Rows[i]["skubun"].ToString() == "売上" && string.IsNullOrEmpty(dt_rum_info.Rows[i]["sSYOUHIN_R"].ToString())
                            && string.IsNullOrEmpty(dt_rum_info.Rows[i]["cSYOUHIN"].ToString()) && (string.IsNullOrEmpty(dt_rum_info.Rows[i]["nSURYO"].ToString())
                            || dt_rum_info.Rows[i]["nSURYO"].ToString() == "0.00" || dt_rum_info.Rows[i]["nSURYO"].ToString() == "0"))
                    {
                        continue;
                    }
                    else
                    {
                        //if (string.IsNullOrEmpty(dbl_r_u_m.Tables[0].Rows[i]["nSURYO"].ToString()) || dbl_r_u_m.Tables[0].Rows[i]["nSURYO"].ToString() == "0.00"
                        //        || dbl_r_u_m.Tables[0].Rows[i]["nSURYO"].ToString() == "0")
                        //{
                        //    continue;
                        //}
                        //else
                        //{
                        dbl_result.Rows.Add();
                        int r = dbl_result.Rows.Count - 1;
                        dbl_result.Rows[r]["cSYOUHIN"] = dt_rum_info.Rows[i]["cSYOUHIN"].ToString();
                        dbl_result.Rows[r]["sSYOUHIN_R"] = dt_rum_info.Rows[i]["sSYOUHIN_R"].ToString();
                        if (string.IsNullOrEmpty(dt_rum_info.Rows[i]["nSURYO"].ToString()) || dt_rum_info.Rows[i]["nSURYO"].ToString() == "0.00"
                             || dt_rum_info.Rows[i]["nSURYO"].ToString() == "0")
                        {
                            dbl_result.Rows[r]["nSURYO"] = "";
                            dbl_result.Rows[r]["sTANI"] = "";
                            dbl_result.Rows[r]["nSIKIRIKINGAKU"] = "";
                            dbl_result.Rows[r]["nSIKIRIKINGAKU"] = "";
                            dbl_result.Rows[r]["skubun"] = "";
                        }
                        else
                        {
                            dbl_result.Rows[r]["nSURYO"] = dt_rum_info.Rows[i]["nSURYO"].ToString();
                            dbl_result.Rows[r]["sTANI"] = dt_rum_info.Rows[i]["sTANI"].ToString();

                            dbl_result.Rows[r]["skubun"] = dt_rum_info.Rows[i]["skubun"].ToString();

                            if (string.IsNullOrEmpty(dt_rum_info.Rows[i]["nSIKIRITANKA"].ToString()) || dt_rum_info.Rows[i]["nSIKIRITANKA"].ToString() == "0.00"
                                || dt_rum_info.Rows[i]["nSIKIRITANKA"].ToString() == "0")
                            {
                                dbl_result.Rows[r]["nSIKIRITANKA"] = "";
                                dbl_result.Rows[r]["nSIKIRIKINGAKU"] = 0;

                            }
                            else
                            {
                                if (dbl_result.Rows[r]["skubun"].ToString() == "返品" || dbl_result.Rows[r]["skubun"].ToString() == "値引")
                                {
                                    decimal nsikirikingaku = 0;
                                    if (dt_rum_info.Rows[i]["nSIKIRIKINGAKU"].ToString() != "")
                                    {
                                        nsikirikingaku = decimal.Parse(dt_rum_info.Rows[i]["nSIKIRIKINGAKU"].ToString());
                                        nsikirikingaku = (-nsikirikingaku);
                                    }
                                    decimal nSIKIRITANKA = 0;
                                    if (dt_rum_info.Rows[i]["nSIKIRITANKA"].ToString() != "")
                                    {
                                        nSIKIRITANKA = decimal.Parse(dt_rum_info.Rows[i]["nSIKIRITANKA"].ToString());
                                        nSIKIRITANKA = (-nSIKIRITANKA);
                                    }
                                    dbl_result.Rows[r]["nSIKIRIKINGAKU"] = nsikirikingaku;
                                    dbl_result.Rows[r]["nSIKIRITANKA"] = nSIKIRITANKA;
                                }
                                else
                                {
                                    dbl_result.Rows[r]["nSIKIRITANKA"] = Convert.ToDecimal(dt_rum_info.Rows[i]["nSIKIRITANKA"].ToString());
                                    dbl_result.Rows[r]["nSIKIRIKINGAKU"] = Convert.ToDecimal(dt_rum_info.Rows[i]["nSIKIRIKINGAKU"].ToString());
                                }
                            }
                        }
                        dbl_result.Rows[r]["sbikou"] = dt_rum_info.Rows[i]["sbikou"].ToString();
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

        }
        #endregion

        #region uriage_FetchData
        private void uriage_FetchData(object sender, FetchEventArgs eArgs)
        {
            if (counter < dbl_result.Rows.Count)
            {
                this.Fields["skubun"].Value = dbl_result.Rows[counter]["skubun"].ToString();
                this.Fields["cSYOUHIN"].Value = dbl_result.Rows[counter]["cSYOUHIN"].ToString();
                this.Fields["sSYOUHIN_R"].Value = dbl_result.Rows[counter]["sSYOUHIN_R"].ToString();
              
                if (dbl_result.Rows[counter]["nSURYO"].ToString() != "")
                {
                    string str = Double.Parse(dbl_result.Rows[counter]["nSURYO"].ToString()).ToString("0.##");

                    Fields["nSURYO"].Value = str;
                    //Fields["nSURYO2"].Value = str;

                    if (Fields["nSURYO"].Value.ToString() == "-0")
                    {
                        LB_Suryou.OutputFormat = null;
                    }
                    else
                    {
                        int length = getDecPoint(decimal.Parse(dbl_result.Rows[counter]["nSURYO"].ToString()));

                        if (length == 0)
                        {
                            LB_Suryou.OutputFormat = "#,##0";
                        }
                        else if (length == 1)
                        {
                            LB_Suryou.OutputFormat = "#,##0.0";
                        }
                        else if (length == 2)
                        {
                            LB_Suryou.OutputFormat = "#,##0.00";
                        }
                    }
                }
                else
                {
                    Fields["nSURYO"].Value = dbl_result.Rows[counter]["nSURYO"].ToString();
                }
                
                this.Fields["sTANI"].Value = dbl_result.Rows[counter]["sTANI"].ToString();
                if (f_kingakuari == true)
                {
                    this.Fields["nSIKIRITANKA"].Value = dbl_result.Rows[counter]["nSIKIRITANKA"].ToString();
                    this.Fields["nSIKIRIKINGAKU"].Value = dbl_result.Rows[counter]["nSIKIRIKINGAKU"].ToString();
                if (this.Fields["skubun"].Value.ToString() == "")
                {
                    this.Fields["sbikou"].Value = "";
                }
                else
                {
                    this.Fields["sbikou"].Value = dbl_result.Rows[counter]["sbikou"].ToString();
                }

                }
                else
                {
                    this.Fields["nSIKIRITANKA"].Value = "";
                    this.Fields["nSIKIRIKINGAKU"].Value = "";
                    if (this.Fields["skubun"].Value.ToString() == "")
                    {
                        this.Fields["sbikou"].Value = "";
                    }
                    else
                    {
                        this.Fields["sbikou"].Value = dbl_result.Rows[counter]["sbikou"].ToString();
                    }
                }
                int check_r = counter + 1;
                if (check_r % 24 == 3)
                {
                    TB_Drop1.Visible = true;
                    TB_Drop2.Visible = true;
                }
                else
                {
                    TB_Drop1.Visible = false;
                    TB_Drop2.Visible = false;
                }
                counter++;
                eArgs.EOF = false;
            }
           
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

        private void reportHeader1_Format(object sender, EventArgs e)
        {

        }

        #region pageHeader_BeforePrint
        private void pageHeader_BeforePrint(object sender, EventArgs e)
        {
            this.LB_PAGE.Text = "(" + (p + 1) + " / " + nPAGECOUNT.ToString() + ")";
            p++;
            if (p == 1)
            {
                if (f_kingakuari == true)
                {
                    if (j_info == "1")
                {
                    TB_goukei.Text = ngoukeikingaku.ToString("¥#,##0"); 
                    TB_shouhizei.Text = nuriage_kingaku.ToString("¥#,##0") + "\n" + nsyoujizei.ToString("¥#,##0");
                    TB_goukei_2.Visible = false;
                    LB_message.Visible = false;
                }
                else
                {
                    TB_goukei_2.Text = nuriage_kingaku.ToString("¥#,##0"); 
                    TB_goukei.Visible = false;
                    TB_shouhizei.Visible = false;
                    LB_shouhizei.Visible = false;
                    LB_message.Text = "◎消費税は「請求書」で一括請求させていただきます。";
                }
                }
                else
                {
                    TB_goukei.Visible = false;
                    TB_goukei_2.Visible = false;
                    TB_shouhizei.Visible = false;
                    LB_shouhizei.Visible = false;
                    LB_goukei.Visible = false;
                    LB_message.Visible = false;
                    line_goukei.Visible = false;
                }

                TB_henkan1.Visible = true;
                TB_henkan2.Visible = true;
                TB_henkan3.Visible = true;
            }
            else
            {
                TB_goukei.Visible = false;
                TB_shouhizei.Visible = false;
                LB_shouhizei.Visible = false;
                TB_henkan1.Visible = false;
                TB_henkan2.Visible = false;
                TB_henkan3.Visible = false;
                LB_goukei.Visible = false;
                LB_message.Visible = false;
                line_goukei.Visible = false;
            }

        }
        #endregion
        
        #region pageFooter_BeforePrint
        private void pageFooter_BeforePrint_1(object sender, EventArgs e)
        {
            if (fINS == 0)
            {
                if (p != nPAGECOUNT)
                {
                    TB_biko.Text = "（次頁に続きます）";
                }
                else
                {
                    TB_biko.Text = "\n" + dt_ru_info.Rows[0]["sbikou"].ToString();

                    LB_Total.Text = "合　計";
                    if (f_kingakuari == true)
                    {
                        if (j_info == "1")
                        {
                            TB_goukeikingaku.Text = ngoukeikingaku.ToString("#,###");
                        }
                        else
                        {
                            TB_goukeikingaku.Text = nuriage_kingaku.ToString("#,###");
                        }
                    }
                }
            }
            else
            {
                if (p != nPAGECOUNT)
                {
                    TB_biko.Text = "（次頁に続きます）";
                }
                else
                {
                    TB_biko.Text = "\n" + bikou;

                    LB_Total.Text = "合　計";
                    if (f_kingakuari == true)
                    {
                        //TB_goukeikingaku.Text = ngoukeikingaku.ToString("#,###"); //delete by yamin 20200630
                        if (j_info == "1")  //modify by yamin 20200630
                        {
                            TB_goukeikingaku.Text = ngoukeikingaku.ToString("#,###");
                        }
                        else
                        {
                            TB_goukeikingaku.Text = nuriage_kingaku.ToString("#,###");
                        }
                    }
                }
            }
        }
        #endregion
    }
}
