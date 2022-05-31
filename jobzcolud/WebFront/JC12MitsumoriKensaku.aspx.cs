using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.VisualBasic;
using Service;
using Common;

namespace JobzCloud.WebFront
{
    public partial class JC12MitsumoriKensaku : System.Web.UI.Page
    {
        public Double totalDataCount = 0;
        MySqlConnection cn = null;
        DataTable dt_MitsumoriKomoku = null;
        string linkMutsucol = "";
        public int rowindex;
        string sort_expression = "";
        string sort_direction = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(Request.QueryString["id"]))
                {
                    if (!IsPostBack)
                    {
                        if (SessionUtility.GetSession("HOME") != null)
                        {
                            hdnHome.Value = SessionUtility.GetSession("HOME").ToString();
                            SessionUtility.SetSession("HOME", null);
                        }
                        Session["M_code"] = null;
                        Session["M_name"] = null;
                        Session["HyoujiSettingID"] = null;
                        Session["M_touisaki"] = null;
                        Session["M_touisaki_tan"] = null;
                        Session["M_startdate"] = null;
                        Session["M_enddate"] = null;
                        Session["J_startdate"] = null;
                        Session["J_enddate"] = null;
                        Session["U_startdate"] = null;
                        Session["U_enddate"] = null;
                        Session["syohin1"] = null;
                        Session["syohin2"] = null;
                        Session["syohin3"] = null;
                        Session["memo"] = null;

                        rowindex = 0;
                        GV_MiRowindex.Text = rowindex.ToString();

                        this.BindGrid();
                        int kensuu = Convert.ToInt16(DDL_Hyojikensuu.SelectedValue);
                        int endRowIndex = GV_Mitumori.Rows.Count;
                        lblHyojikensuu.Text = "1-" + endRowIndex + "/";
                        LB_Total.Text = totalDataCount.ToString();
                        if (Session["ftaMituSyohin"] != null)
                        {
                            if (Session["ftaMituSyohin"].ToString() == "1")
                            {
                                HF_fSyouhin.Value = "1";
                                Session["ftaMituSyohin"] = null;
                            }
                        }
                    }
                    else
                    {
                        if (HF_fMeisaiOK.Value != "1") //20220520 MyatNoe Added
                        {
                            rowindex = Convert.ToInt32(GV_MiRowindex.Text);
                            if (ViewState["sortDirection"] != null && ViewState["z_sortexpresion"] != null)
                            {
                                if (ViewState["sortDirection"].ToString() == "Ascending")
                                {
                                    sort_direction = "ASC";
                                }
                                else
                                {
                                    sort_direction = "DESC";
                                }
                                sort_expression = ViewState["z_sortexpresion"].ToString();
                            }
                            else
                            {
                                sort_expression = "見積コード";
                                sort_direction = "DESC";
                            }

                            MitumoriKoumokuSort();
                            JoukenReload();
                            updbound.Update();
                        }
                    }
                    Session.Timeout = 480;
                    updHyojikensuu1.Update();
                    updMitsumoriGrid.Update();
                    updShinkiPopup.Update(); //20220519 MyatNoe Added
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "parentButtonClick('btnToLogin','" + hdnHome.Value + "');", true);

                }
            }
            catch
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "parentButtonClick('btnToLogin','" + hdnHome.Value + "');", true);
            }
        }

        #region CreateMitumoriTableColomn
        private DataTable CreateMitumoriTableColomn()
        {
            DataTable dt_Mitumori = new DataTable();
            dt_Mitumori.Columns.Add("cMitumori");
            dt_Mitumori.Columns.Add("sMitumori");
            dt_Mitumori.Columns.Add("sEigyouTantou");
            dt_Mitumori.Columns.Add("sSakuseisya");
            dt_Mitumori.Columns.Add("dMitumori");
            dt_Mitumori.Columns.Add("nGokeiKingaku");
            dt_Mitumori.Columns.Add("MitumoriJoutai");
            dt_Mitumori.Columns.Add("nArari");
            dt_Mitumori.Columns.Add("Memo");
            dt_Mitumori.Columns.Add("Bikou");
            dt_Mitumori.Columns.Add("sTokuisaki");
            dt_Mitumori.Columns.Add("sTokuisakiTantou");
            return dt_Mitumori;
        }
        #endregion

        #region SearchNameReplace
        public string SearchNameReplace(String str)
        {
            string s = str;
            if (str.Contains("\\"))
            {
                s = s.Replace("\\", "\\\\\\\\");
            }
            if (str.Contains("%"))
            {
                s = s.Replace("%", "\\%");
            }
            if (str.Contains("'"))
            {
                s = s.Replace("'", "\\'");
            }
            if (str.Contains("’"))
            {
                s = s.Replace("’", "\\’");
            }
            return s;
        }
        #endregion

        #region BindGrid()
        private void BindGrid()
        {
            try
            {
                DDL_Jyouken.Items.Clear();
                String qrpg = "select ";
                qrpg += " ifnull(m.cMITUMORI, '') as 見積コード ";
                String qr = "select "
                    + " Replace(Replace(ifnull(m.sMITUMORI, ''),'<','&lt'),'>','&gt') as sMitumori,"
                        + " ifnull(m.cMITUMORI, '') as cMitumori,"
                        + " ifnull(mjt.sTANTOUSHA, '') as sEigyouTantou,"
                        + "	ifnull(mt.sTANTOUSHA, '') as sSakuseisya,"
                        + " date_format(m.dMITUMORISAKUSEI, '%y/%m/%d') as dMITUMORISAKUSEI,"
                        + " ifnull(m.nKINGAKU, 0) as nGokeiKingaku,"
                        + " case IFNULL(m.cJYOTAI_MITUMORI, '')"
                        + " when '00' then '失注' when '01' then '見積提出済' when '02' then '受注' when '03' then '完了'  when '04' then '見積作成中' when '05' then 'キャンセル' when '06' then '売上済み' else '' end as MitumoriJoutai,"
                        + " ifnull(CC.nMITUMORIARARI, 0) nArari,"
                        + " ifnull(m.sMEMO, '') as Memo,"
                        + " ifnull(m.sBIKOU, '') as Bikou,"
                        + " ifnull(m.sTOKUISAKI, '') as sTokuisaki,"
                        + " ifnull(m.sTOKUISAKI_TAN, '') as sTokuisakiTantou"
                        + " from r_mitumori m"
                        + " LEFT JOIN (SELECT b.cMITUMORI as cmitu, b.cBUKKEN as cBUKKEN, sum(c.nMITUMORIARARI) nMITUMORIARARI from R_BUKKEN as r_bukken"
                        + " LEFT JOIN r_bu_mitsu b ON b.cBUKKEN = r_bukken.cBUKKEN LEFT JOIN m_file mfb ON mfb.cFILE = r_bukken.cFILE"
                         + " LEFT JOIN R_MITUMORI c ON b.cMITUMORI = c.cMITUMORI GROUP BY c.cMITUMORI) CC ON m.cMITUMORI = CC.cMITU"
                        + " left join m_j_tantousha mjt ON m.cEIGYOTANTOSYA = mjt.cTANTOUSHA";

                qrpg += " from r_mitumori m";
                qrpg += " LEFT JOIN (SELECT b.cMITUMORI as cmitu, b.cBUKKEN as cBUKKEN, sum(c.nMITUMORIARARI) nMITUMORIARARI from R_BUKKEN as r_bukken";
                qrpg += " LEFT JOIN r_bu_mitsu b ON b.cBUKKEN = r_bukken.cBUKKEN LEFT JOIN m_file mfb ON mfb.cFILE = r_bukken.cFILE";
                qrpg += " LEFT JOIN R_MITUMORI c ON b.cMITUMORI = c.cMITUMORI GROUP BY c.cMITUMORI) CC ON m.cMITUMORI = CC.cMITU";
                qrpg += " left join m_j_tantousha mjt ON m.cEIGYOTANTOSYA = mjt.cTANTOUSHA ";

                if (Session["syohin1"] != null || Session["syohin2"] != null || Session["syohin3"] != null || Session["M_name"] != null)
                {
                    qr += " left join r_mitumori_m rmm on m.cMITUMORI = rmm.cMITUMORI";   //見積表ー見積商品明細表
                    qrpg += " left join r_mitumori_m rmm on m.cMITUMORI = rmm.cMITUMORI";   //見積表ー見積商品明細表

                    qr += " left join m_syouhin syouhin on rmm.cSYOUHIN = syouhin.cSYOUHIN";   //見積商品明細表ー商品表
                    qrpg += " left join m_syouhin syouhin on rmm.cSYOUHIN = syouhin.cSYOUHIN";   //見積商品明細表ー商品表
                }

                qr += " left join m_j_tantousha as mt ON m.cSAKUSEISYA = mt.cTANTOUSHA"
                + " where 1 = 1 "
                + " AND ifnull(m.fHYOUJI, '0') = '0'";

                qrpg += " left join m_j_tantousha as mt ON m.cSAKUSEISYA = mt.cTANTOUSHA where 1 = 1 AND ifnull(m.fHYOUJI, '0') = '0' ";

                if (Session["syohin1"] != null)
                {
                    string str_mitsumori_syouhin1_name1 = Strings.StrConv(SearchNameReplace(Session["syohin1"].ToString()), VbStrConv.Narrow);
                    string str_mitsumori_syouhin1_name2 = Strings.StrConv(SearchNameReplace(Session["syohin1"].ToString()), VbStrConv.Wide);
                    String str_mitsumori_syouhin1_name3 = Strings.StrConv(SearchNameReplace(Session["syohin1"].ToString()), VbStrConv.Katakana);
                    str_mitsumori_syouhin1_name3 = Strings.StrConv(str_mitsumori_syouhin1_name3, VbStrConv.Narrow);
                    String str_mitsumori_syouhin1_name4 = Strings.StrConv(str_mitsumori_syouhin1_name2, VbStrConv.Hiragana);
                    str_mitsumori_syouhin1_name4 = Strings.StrConv(str_mitsumori_syouhin1_name4, VbStrConv.Narrow);
                    string strANDOR = " ";
                    qr += " AND (";
                    qr += strANDOR + " (rmm.sSYOUHIN_R like '%" + str_mitsumori_syouhin1_name1 + "%' or rmm.sSYOUHIN_R like '%" + str_mitsumori_syouhin1_name2 + "%' or rmm.sSYOUHIN_R like '%" + str_mitsumori_syouhin1_name3 + "%' or rmm.sSYOUHIN_R like '%" + str_mitsumori_syouhin1_name4 + "%' or rmm.sSYOUHIN_R collate utf8_unicode_ci like '%" + SearchNameReplace(Session["syohin1"].ToString()) + "%')";
                    //strANDOR = " OR";

                    qrpg += " AND (";
                    qrpg += strANDOR + " (rmm.sSYOUHIN_R like '%" + str_mitsumori_syouhin1_name1 + "%' or rmm.sSYOUHIN_R like '%" + str_mitsumori_syouhin1_name2 + "%' or rmm.sSYOUHIN_R like '%" + str_mitsumori_syouhin1_name3 + "%' or rmm.sSYOUHIN_R like '%" + str_mitsumori_syouhin1_name4 + "%' or rmm.sSYOUHIN_R collate utf8_unicode_ci like '%" + SearchNameReplace(Session["syohin1"].ToString()) + "%')";
                    strANDOR = " OR";

                    ListItem li = new ListItem();
                    li.Value = "商品名1";
                    li.Text = Session["syohin1"].ToString();
                    DDL_Jyouken.Items.Add(li);
                    qr += ")";
                    qrpg += ")";
                }

                if (Session["M_touisaki"] != null) //得意先
                {
                    String str_tokuisaki_name1 = Strings.StrConv(SearchNameReplace(Session["M_touisaki"].ToString()), VbStrConv.Wide);
                    String str_tokuisaki_name2 = Strings.StrConv(SearchNameReplace(Session["M_touisaki"].ToString()), VbStrConv.Narrow);


                    String str_tokuisaki_name3 = Strings.StrConv(SearchNameReplace(Session["M_touisaki"].ToString()), VbStrConv.Katakana);
                    str_tokuisaki_name3 = Strings.StrConv(str_tokuisaki_name3, VbStrConv.Narrow);
                    String str_tokuisaki_name4 = Strings.StrConv(str_tokuisaki_name1, VbStrConv.Hiragana);
                    str_tokuisaki_name4 = Strings.StrConv(str_tokuisaki_name4, VbStrConv.Narrow);

                    qr += " and ( ";
                    qr += " (m.sTOKUISAKI like '%" + str_tokuisaki_name1 + "%' OR m.sTOKUISAKI like '%" + str_tokuisaki_name2 + "%' OR m.sTOKUISAKI like '%" + str_tokuisaki_name3 + "%' OR m.sTOKUISAKI like '%" + str_tokuisaki_name4 + "%' OR m.sTOKUISAKI collate utf8_unicode_ci like '%" + SearchNameReplace(Session["M_touisaki"].ToString()) + "%')";
                    qr += ") ";

                    qrpg += " and ( ";
                    qrpg += " (m.sTOKUISAKI like '%" + str_tokuisaki_name1 + "%' OR m.sTOKUISAKI like '%" + str_tokuisaki_name2 + "%' OR m.sTOKUISAKI like '%" + str_tokuisaki_name3 + "%' OR m.sTOKUISAKI like '%" + str_tokuisaki_name4 + "%' OR m.sTOKUISAKI collate utf8_unicode_ci like '%" + SearchNameReplace(Session["M_touisaki"].ToString()) + "%')";
                    qrpg += ") ";
                    ListItem li = new ListItem();
                    li.Value = "得意先";
                    li.Text = Session["M_touisaki"].ToString();
                    DDL_Jyouken.Items.Add(li);
                }
                if (Session["M_name"] != null) //見積名
                {
                    String str_mitumori_name1 = Strings.StrConv(SearchNameReplace(Session["M_name"].ToString()), VbStrConv.Wide);
                    String str_mitumori_name2 = Strings.StrConv(SearchNameReplace(Session["M_name"].ToString()), VbStrConv.Narrow);


                    String str_mitumori_name3 = Strings.StrConv(SearchNameReplace(Session["M_name"].ToString()), VbStrConv.Katakana);
                    str_mitumori_name3 = Strings.StrConv(str_mitumori_name3, VbStrConv.Narrow);
                    String str_mitumori_name4 = Strings.StrConv(str_mitumori_name1, VbStrConv.Hiragana);
                    str_mitumori_name4 = Strings.StrConv(str_mitumori_name4, VbStrConv.Narrow);

                    qr += " and ( ";
                    qr += " (m.sMITUMORI like '%" + str_mitumori_name1 + "%' OR m.sMITUMORI like '%" + str_mitumori_name2 + "%' OR m.sMITUMORI like '%" + str_mitumori_name3 + "%' OR m.sMITUMORI like '%" + str_mitumori_name4 + "%' OR m.sMITUMORI collate utf8_unicode_ci like '%" + SearchNameReplace(Session["M_name"].ToString()) + "%')";
                    qr += ") ";

                    qrpg += " and ( ";
                    qrpg += " (m.sMITUMORI like '%" + str_mitumori_name1 + "%' OR m.sMITUMORI like '%" + str_mitumori_name2 + "%' OR m.sMITUMORI like '%" + str_mitumori_name3 + "%' OR m.sMITUMORI like '%" + str_mitumori_name4 + "%' OR m.sMITUMORI collate utf8_unicode_ci like '%" + SearchNameReplace(Session["M_name"].ToString()) + "%')";
                    qrpg += ") ";

                    ListItem li = new ListItem();
                    li.Value = "見積名";
                    li.Text = Session["M_name"].ToString();
                    DDL_Jyouken.Items.Add(li);
                }

                if (Session["M_code"] != null)//見積コード
                {
                    qr += " and m.cMITUMORI like '%" + Session["M_code"].ToString() + "%'";

                    qrpg += " and m.cMITUMORI like '%" + Session["M_code"].ToString() + "%'";
                    ListItem li = new ListItem();
                    li.Value = "見積コード";
                    li.Text = Session["M_code"].ToString();
                    DDL_Jyouken.Items.Add(li);
                }

                if (DDL_Jyotai.Items.Count > 0)  //見積状態
                {
                    qr += " and ( ";
                    qrpg += " and ( ";
                    for (int i = 0; i < DDL_Jyotai.Items.Count; i++)
                    {
                        String cJyotai = DDL_Jyotai.Items[i].Value;
                        if (i == 0)
                        {
                            qr += " (m.cJYOTAI_MITUMORI like '%" + cJyotai + "%'";
                            qrpg += " (m.cJYOTAI_MITUMORI like '%" + cJyotai + "%'";
                        }
                        else
                        {
                            qr += " OR m.cJYOTAI_MITUMORI like '%" + cJyotai + "%'";
                            qrpg += " OR m.cJYOTAI_MITUMORI like '%" + cJyotai + "%'";
                        }

                        ListItem li = new ListItem();
                        li.Value = "見積状態" + cJyotai;
                        li.Text = DDL_Jyotai.Items[i].Text;
                        DDL_Jyouken.Items.Add(li);
                    }
                    qr += ")) ";
                    qrpg += ")) ";
                }
                if (Session["M_touisaki_tan"] != null) //得意先担当者
                {
                    String str_tokuisakitantou_name1 = Strings.StrConv(SearchNameReplace(Session["M_touisaki_tan"].ToString()), VbStrConv.Wide);
                    String str_tokuisakitantou_name2 = Strings.StrConv(SearchNameReplace(Session["M_touisaki_tan"].ToString()), VbStrConv.Narrow);


                    String str_tokuisakitantou_name3 = Strings.StrConv(SearchNameReplace(Session["M_touisaki_tan"].ToString()), VbStrConv.Katakana);
                    str_tokuisakitantou_name3 = Strings.StrConv(str_tokuisakitantou_name3, VbStrConv.Narrow);
                    String str_tokuisakitantou_name4 = Strings.StrConv(str_tokuisakitantou_name1, VbStrConv.Hiragana);
                    str_tokuisakitantou_name4 = Strings.StrConv(str_tokuisakitantou_name4, VbStrConv.Narrow);

                    qr += " and ( ";
                    qr += " (m.sTOKUISAKI_TAN like '%" + str_tokuisakitantou_name1 + "%' OR m.sTOKUISAKI_TAN like '%" + str_tokuisakitantou_name2 + "%' OR m.sTOKUISAKI_TAN like '%" + str_tokuisakitantou_name3 + "%' OR m.sTOKUISAKI_TAN like '%" + str_tokuisakitantou_name4 + "%' OR m.sTOKUISAKI_TAN collate utf8_unicode_ci like '%" + SearchNameReplace(Session["M_touisaki_tan"].ToString()) + "%')";
                    qr += ") ";

                    qrpg += " and ( ";
                    qrpg += " (m.sTOKUISAKI_TAN like '%" + str_tokuisakitantou_name1 + "%' OR m.sTOKUISAKI_TAN like '%" + str_tokuisakitantou_name2 + "%' OR m.sTOKUISAKI_TAN like '%" + str_tokuisakitantou_name3 + "%' OR m.sTOKUISAKI_TAN like '%" + str_tokuisakitantou_name4 + "%' OR m.sTOKUISAKI_TAN collate utf8_unicode_ci like '%" + SearchNameReplace(Session["M_touisaki_tan"].ToString()) + "%')";
                    qrpg += ") ";
                    ListItem li = new ListItem();
                    li.Value = "得意先担当者";
                    li.Text = Session["M_touisaki_tan"].ToString();
                    DDL_Jyouken.Items.Add(li);
                }

                if (DDL_Tantousya.Items.Count > 0)  //自社担当者
                {
                    qr += " and ( ";
                    qrpg += " and ( ";
                    for (int i = 0; i < DDL_Tantousya.Items.Count; i++)
                    {
                        String cTantousha = DDL_Tantousya.Items[i].Value;
                        if (i == 0)
                        {
                            qr += " (mjt.cTANTOUSHA like '%" + cTantousha + "%'";
                            qrpg += " (mjt.cTANTOUSHA like '%" + cTantousha + "%'";
                        }
                        else
                        {
                            qr += " OR mjt.cTANTOUSHA like '%" + cTantousha + "%'";
                            qrpg += " OR mjt.cTANTOUSHA like '%" + cTantousha + "%'";
                        }

                        ListItem li = new ListItem();
                        li.Value = "自社担当者" + cTantousha;
                        li.Text = DDL_Tantousya.Items[i].Text;
                        DDL_Jyouken.Items.Add(li);
                    }
                    qr += ")) ";
                    qrpg += ")) ";
                }
                if (Session["memo"] != null)
                {
                    String str_memo_name1 = Strings.StrConv(SearchNameReplace(Session["memo"].ToString()), VbStrConv.Wide);
                    String str_memo_name2 = Strings.StrConv(SearchNameReplace(Session["memo"].ToString()), VbStrConv.Narrow);
                    String str_memo_name3 = Strings.StrConv(SearchNameReplace(Session["memo"].ToString()), VbStrConv.Katakana);
                    str_memo_name3 = Strings.StrConv(str_memo_name3, VbStrConv.Narrow);
                    String str_memo_name4 = Strings.StrConv(str_memo_name1, VbStrConv.Hiragana);
                    str_memo_name4 = Strings.StrConv(str_memo_name4, VbStrConv.Narrow);

                    qr += " and ( ";
                    qr += " (m.sMEMO like '%" + str_memo_name1 + "%' OR m.sMEMO like '%" + str_memo_name2 + "%' OR m.sMEMO like '%" + str_memo_name3 + "%' OR m.sMEMO like '%" + str_memo_name4 + "%' OR m.sMEMO collate utf8_unicode_ci like '%" + SearchNameReplace(Session["memo"].ToString()) + "%')";
                    qr += ") ";

                    qrpg += " and ( ";
                    qrpg += " (m.sMEMO like '%" + str_memo_name1 + "%' OR m.sMEMO like '%" + str_memo_name2 + "%' OR m.sMEMO like '%" + str_memo_name3 + "%' OR m.sMEMO like '%" + str_memo_name4 + "%' OR m.sMEMO collate utf8_unicode_ci like '%" + SearchNameReplace(Session["memo"].ToString()) + "%')";
                    qrpg += ") ";
                    ListItem li = new ListItem();
                    li.Value = "社内メモ";
                    li.Text = Session["memo"].ToString();
                    DDL_Jyouken.Items.Add(li);
                }
                if (Session["M_startdate"] != null && Session["M_enddate"] != null) //  見積日
                {
                    qr += " and date_format(m.dMITUMORISAKUSEI,'%Y/%m/%d') BETWEEN '" + Session["M_startdate"].ToString() + "' AND '" + Session["M_enddate"].ToString() + "'";
                    qrpg += " and date_format(m.dMITUMORISAKUSEI,'%Y/%m/%d') BETWEEN '" + Session["M_startdate"].ToString() + "' AND '" + Session["M_enddate"].ToString() + "'";
                    ListItem li = new ListItem();
                    li.Value = "見積日Start";
                    li.Text = Session["M_startdate"].ToString();
                    DDL_Jyouken.Items.Add(li);

                    li = new ListItem();
                    li.Value = "見積日End";
                    li.Text = Session["M_enddate"].ToString();
                    DDL_Jyouken.Items.Add(li);
                }
                else
                {
                    if (Session["M_startdate"] != null) //見積日 start
                    {
                        qr += " and date_format(m.dMITUMORISAKUSEI,'%Y/%m/%d')>='" + Session["M_startdate"].ToString() + "'";
                        qrpg += " and date_format(m.dMITUMORISAKUSEI,'%Y/%m/%d')>='" + Session["M_startdate"].ToString() + "'";
                        ListItem li = new ListItem();
                        li.Value = "見積日Start";
                        li.Text = Session["M_startdate"].ToString();
                        DDL_Jyouken.Items.Add(li);
                    }
                    else if (Session["M_enddate"] != null)//見積日 end
                    {
                        qr += " and date_format(m.dMITUMORISAKUSEI,'%Y/%m/%d')<='" + Session["M_enddate"].ToString() + "'";
                        qrpg += " and date_format(m.dMITUMORISAKUSEI,'%Y/%m/%d')<='" + Session["M_enddate"].ToString() + "'";
                        ListItem li = new ListItem();
                        li.Value = "見積日End";
                        li.Text = Session["M_enddate"].ToString();
                        DDL_Jyouken.Items.Add(li);
                    }
                }

                if (Session["J_startdate"] != null && Session["J_enddate"] != null)// 受注日
                {
                    qr += " and date_format(m.dYOTEINOUKI,'%Y/%m/%d') BETWEEN '" + Session["J_startdate"].ToString() + "' AND '" + Session["J_enddate"].ToString() + "'";
                    qrpg += " and date_format(m.dYOTEINOUKI,'%Y/%m/%d') BETWEEN '" + Session["J_startdate"].ToString() + "' AND '" + Session["J_enddate"].ToString() + "'";
                    ListItem li = new ListItem();
                    li.Value = "受注日Start";
                    li.Text = Session["J_startdate"].ToString();
                    DDL_Jyouken.Items.Add(li);

                    li = new ListItem();
                    li.Value = "受注日End";
                    li.Text = Session["J_enddate"].ToString();
                    DDL_Jyouken.Items.Add(li);
                }
                else
                {
                    if (Session["J_startdate"] != null)// 受注日 start
                    {
                        qr += " and date_format(m.dYOTEINOUKI,'%Y/%m/%d')>='" + Session["J_startdate"].ToString() + "'";
                        qrpg += " and date_format(m.dYOTEINOUKI,'%Y/%m/%d')>='" + Session["J_startdate"].ToString() + "'";
                        ListItem li = new ListItem();
                        li.Value = "受注日Start";
                        li.Text = Session["J_startdate"].ToString();
                        DDL_Jyouken.Items.Add(li);
                    }
                    else if (Session["J_enddate"] != null) //受注日 end
                    {
                        qr += " and date_format(m.dYOTEINOUKI,'%Y/%m/%d')<='" + Session["J_enddate"].ToString() + "'";
                        qrpg += " and date_format(m.dYOTEINOUKI,'%Y/%m/%d')<='" + Session["J_enddate"].ToString() + "'";
                        ListItem li = new ListItem();
                        li.Value = "受注日End";
                        li.Text = Session["J_enddate"].ToString();
                        DDL_Jyouken.Items.Add(li);
                    }
                }

                if (Session["U_startdate"] != null && Session["U_enddate"] != null)// 売上予定日
                {
                    qr += " and date_format(m.dURIAGEYOTEI,'%Y/%m/%d') BETWEEN '" + Session["U_startdate"].ToString() + "' AND '" + Session["U_enddate"].ToString() + "'";
                    qrpg += " and date_format(m.dURIAGEYOTEI,'%Y/%m/%d') BETWEEN '" + Session["U_startdate"].ToString() + "' AND '" + Session["U_enddate"].ToString() + "'";
                    ListItem li = new ListItem();
                    li.Value = "売上予定日Start";
                    li.Text = Session["U_startdate"].ToString();
                    DDL_Jyouken.Items.Add(li);

                    li = new ListItem();
                    li.Value = "売上予定日End";
                    li.Text = Session["U_enddate"].ToString();
                    DDL_Jyouken.Items.Add(li);
                }
                else
                {
                    if (Session["U_startdate"] != null)// 売上予定日 start
                    {
                        qr += " and date_format(m.dURIAGEYOTEI,'%Y/%m/%d')>='" + Session["U_startdate"].ToString() + "'";
                        qrpg += " and date_format(m.dURIAGEYOTEI,'%Y/%m/%d')>='" + Session["U_startdate"].ToString() + "'";
                        ListItem li = new ListItem();
                        li.Value = "売上予定日Start";
                        li.Text = Session["U_startdate"].ToString();
                        DDL_Jyouken.Items.Add(li);
                    }
                    else if (Session["U_enddate"] != null) //売上予定日 end
                    {
                        qr += " and date_format(m.dURIAGEYOTEI,'%Y/%m/%d')<='" + Session["U_enddate"].ToString() + "'";
                        qrpg += " and date_format(m.dURIAGEYOTEI,'%Y/%m/%d')<='" + Session["U_enddate"].ToString() + "'";
                        ListItem li = new ListItem();
                        li.Value = "売上予定日End";
                        li.Text = Session["U_enddate"].ToString();
                        DDL_Jyouken.Items.Add(li);
                    }
                }


                qr += " group by m.cMITUMORI";
                qrpg += " group by m.cMITUMORI order by CC.cBUKKEN DESC, m.cMITUMORI DESC ;";
                qr += " order by CC.cBUKKEN DESC, m.cMITUMORI DESC LIMIT " + Convert.ToInt16(DDL_Hyojikensuu.SelectedValue) + " OFFSET " + rowindex + ";";

                String id = Request.QueryString["id"];
                DataTable dt_db = JC01Login_Class.Get_DB(id);
                String db = dt_db.Rows[0]["db"].ToString();
                String mail = dt_db.Rows[0]["mail"].ToString();
                ConstantVal.DB_NAME = db;

                JC_ClientConnecction_Class jc = new JC_ClientConnecction_Class();
                jc.loginId = mail;
                cn = jc.GetConnection();
                MySqlCommand cmdpg = new MySqlCommand();
                cmdpg.CommandTimeout = 0;
                cmdpg = new MySqlCommand(qrpg, cn);
                cn.Open();
                MySqlDataAdapter dapg = new MySqlDataAdapter(cmdpg);
                DataTable dtpg = new DataTable();
                dapg.Fill(dtpg);
                cn.Close();
                dapg.Dispose();


                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandTimeout = 0;
                cmd = new MySqlCommand(qr, cn);
                cn.Open();
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                cn.Close();
                da.Dispose();
                //GV_Mitumori.DataSource = dt;
                //GV_Mitumori.DataBind();
                //totalDataCount = dt.Rows.Count;

                ViewState["dt_Mitumori"] = dt;
                ViewState["dtpg_Mitumori"] = dtpg;
                MitumoriKoumokuSort();
                updMitsumoriGrid.Update();
                updBody.Update();
            }
            catch(Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "parentButtonClick('btnToLogin','" + hdnHome.Value + "');", true);
            }
        }
        #endregion

        #region GV_Mitumori_PageIndexChanging
        protected void GV_Mitumori_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView gv = sender as GridView;
            gv.PageIndex = e.NewPageIndex;
            // GV_Mitumori.PageIndex = e.NewPageIndex;
            int kensuu = Convert.ToInt16(DDL_Hyojikensuu.SelectedValue);
            GV_Mitumori_Original.PageSize = kensuu;
            int startRowIndex = (kensuu * e.NewPageIndex) + 1;
            rowindex = (kensuu * e.NewPageIndex);
            GV_MiRowindex.Text = rowindex.ToString();
            // GV_Mitumori.PageIndex = e.NewPageIndex;
            GridView1.PageIndex = e.NewPageIndex;
            BindGrid();
            int endRowIndex = (kensuu * e.NewPageIndex) + GV_Mitumori.Rows.Count;
            lblHyojikensuu.Text = startRowIndex + "-" + endRowIndex + "/";
            LB_Total.Text = totalDataCount.ToString();


            //GridViewRowCount.Text = endRowIndex.ToString();

            updHyojikensuu1.Update();
            updMitsumoriGrid.Update();
        }
        #endregion

        #region DDL_Hyojikensuu_SelectedIndexChanged //表示件数
        protected void DDL_Hyojikensuu_SelectedIndexChanged(object sender, EventArgs e)
        {
            int kensuu = Convert.ToInt16(DDL_Hyojikensuu.SelectedValue);
            //GV_Mitumori.PageIndex = 0;
            //GV_Mitumori_Original.PageIndex = 0;
            //GV_Mitumori.PageSize = kensuu;
            //GV_Mitumori_Original.PageSize = kensuu;
            GridView1.PageIndex = 0;
            GridView1.PageSize = kensuu;
            //pageInd = 1;
            rowindex = 0;
            GV_MiRowindex.Text = rowindex.ToString();
            this.BindGrid();
            int endRowIndex = GV_Mitumori_Original.Rows.Count;
            lblHyojikensuu.Text = "1-" + endRowIndex + "/";
            LB_Total.Text = totalDataCount.ToString();

            updHyojikensuu1.Update();
            updMitsumoriGrid.Update();
            updBody.Update();
        }
        #endregion

        #region btnSearch_Click 見積データ検索
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Session["M_code"] = null;
            Session["M_name"] = null;
            Session["HyoujiSettingID"] = null;
            Session["M_touisaki"] = null;
            Session["M_touisaki_tan"] = null;
            Session["M_startdate"] = null;
            Session["M_enddate"] = null;
            Session["J_startdate"] = null;
            Session["J_enddate"] = null;
            Session["U_startdate"] = null;
            Session["U_enddate"] = null;
            Session["syohin1"] = null;
            Session["syohin2"] = null;
            Session["syohin3"] = null;
            Session["memo"] = null;
            if (TB_MitsuListCode.Text != "")
            {
                if (Session["M_code"] == null)
                {
                    Session["M_code"] = TB_MitsuListCode.Text;
                }
            }
            if (txtsMitumori.Text != "")
            {
                if (Session["M_name"] == null)
                {
                    Session["M_name"] = txtsMitumori.Text;
                }
            }
            if (txtMitumoriJoutai.Text != "")
            {
                if (Session["M_joutai"] == null)
                {
                    Session["M_joutai"] = txtMitumoriJoutai.Text;
                }
            }
            if (txtTokuisaki.Text != "")
            {
                if (Session["M_touisaki"] == null)
                {
                    Session["M_touisaki"] = txtTokuisaki.Text;
                }
            }
            if (txtTokuisakiTantou.Text != "")
            {
                if (Session["M_touisaki_tan"] == null)
                {
                    Session["M_touisaki_tan"] = txtTokuisakiTantou.Text;
                }
            }
            if (lblMitumoriStartDate.Text != "")
            {
                if (Session["M_startdate"] == null)
                {
                    Session["M_startdate"] = lblMitumoriStartDate.Text;
                }
            }
            if (lblMitumoriEndDate.Text != "")
            {
                if (Session["M_enddate"] == null)
                {
                    Session["M_enddate"] = lblMitumoriEndDate.Text;
                }
            }
            if (lblJuchuuStartDate.Text != "")
            {
                if (Session["J_startdate"] == null)
                {
                    Session["J_startdate"] = lblJuchuuStartDate.Text;
                }
            }
            if (lblJuchuuEndDate.Text != "")
            {
                if (Session["J_enddate"] == null)
                {
                    Session["J_enddate"] = lblJuchuuEndDate.Text;
                }
            }
            if (lblUriageYoteiStartDate.Text != "")
            {
                if (Session["U_startdate"] == null)
                {
                    Session["U_startdate"] = lblUriageYoteiStartDate.Text;
                }
            }
            if (lblUriageYoteiEndDate.Text != "")
            {
                if (Session["U_enddate"] == null)
                {
                    Session["U_enddate"] = lblUriageYoteiEndDate.Text;
                }
            }
            if (txtSyohin1.Text != "")
            {
                if (Session["syohin1"] == null)
                {
                    Session["syohin1"] = txtSyohin1.Text;
                }
            }
            if (txtSyohin2.Text != "")
            {
                if (Session["syohin2"] == null)
                {
                    Session["syohin2"] = txtSyohin2.Text;
                }
            }
            if (txtSyohin3.Text != "")
            {
                if (Session["syohin3"] == null)
                {
                    Session["syohin3"] = txtSyohin3.Text;
                }
            }
            if (txtmemo.Text != "")
            {
                if (Session["memo"] == null)
                {
                    Session["memo"] = txtmemo.Text;
                }
            }
            if (btn_Tantousya.Text == "選択なし")
            {
                DDL_Tantousya.Items.Clear();
            }
            int kensuu = Convert.ToInt16(DDL_Hyojikensuu.SelectedValue);
            rowindex = 0;
            GV_MiRowindex.Text = rowindex.ToString();
            GV_Mitumori.PageIndex = 0;
            GV_Mitumori.PageSize = kensuu;
            GV_Mitumori_Original.PageIndex = 0;
            GV_Mitumori_Original.PageSize = kensuu;
            GridView1.PageIndex = 0;
            GridView1.PageSize = kensuu;
            this.BindGrid();
            JoukenReload();
            int endRowIndex = GV_Mitumori_Original.Rows.Count;
            lblHyojikensuu.Text = "1-" + endRowIndex + "/";
            LB_Total.Text = totalDataCount.ToString();
            updMitsumoriGrid.Update();
        }
        #endregion

        #region クリアボタン
        protected void btnClear_Click(object sender, EventArgs e)
        {
            #region 20220324 MiMi Deleted
            //TB_MitsuListCode.Text = "";
            //txtsMitumori.Text = "";
            //txtMitumoriJoutai.Text = "";
            //txtTokuisaki.Text = "";
            //txtTokuisakiTantou.Text = "";
            //txtTantousha.Text = "";
            //btndMitumoriStartCross_Click(sender, e);
            //btndMitumoriEndCross_Click(sender, e);
            //btndJuchuuStartDateCross_Click(sender, e);
            //btndJuchuuEndDateCross_Click(sender, e);
            //btndUriageYoteiStartDateCross_Click(sender, e);
            //btndUriageYoteiEndDateCross_Click(sender, e);
            //txtSyohin1.Text = "";
            //txtSyohin2.Text = "";
            //txtSyohin3.Text = "";

            //DDL_Hyojikensuu.SelectedIndex = 0;
            //rowindex = 0;
            //GV_MiRowindex.Text = rowindex.ToString();
            //GV_Mitumori.PageIndex = 0;
            //GV_Mitumori.PageSize = 20;
            //GV_Mitumori_Original.PageIndex = 0;
            //GV_Mitumori_Original.PageSize = 20;
            //GridView1.PageIndex = 0;
            //GridView1.PageSize = 20;
            //DataTable dt = CreateMitumoriTableColomn();
            //GV_Mitumori.DataSource = dt;
            //GV_Mitumori.DataBind();
            //GV_Mitumori_Original.DataSource = dt;
            //GV_Mitumori_Original.DataBind();
            //int endRowIndex = GV_Mitumori.Rows.Count;
            //lblHyojikensuu.Text = "";

            //DDL_Tantousya.Items.Clear();
            //btn_Tantousya.Text = "選択なし";
            //btn_Tantousya.CssClass = "JC31TantouBtn";

            //DDL_Jyotai.Items.Clear();
            //btn_Jyotai.Text = "選択なし";
            //btn_Jyotai.CssClass = "JC31TantouBtn";
            //updTantousha.Update();

            //this.BindGrid();
            #endregion

            TB_MitsuListCode.Text = "";
            txtsMitumori.Text = "";
            txtTokuisaki.Text = "";
            txtTokuisakiTantou.Text = "";
            txtTantousha.Text = "";
            btndMitumoriStartCross_Click(sender, e);
            btndMitumoriEndCross_Click(sender, e);
            btndJuchuuStartDateCross_Click(sender, e);
            btndJuchuuEndDateCross_Click(sender, e);
            btndUriageYoteiStartDateCross_Click(sender, e);
            btndUriageYoteiEndDateCross_Click(sender, e);
            txtSyohin1.Text = "";
            txtSyohin2.Text = "";
            txtSyohin3.Text = "";
            txtmemo.Text = "";
            //DDL_Tantousya.Items.Clear();
            btn_Tantousya.Text = "選択なし";
            btn_Tantousya.CssClass = "JC31TantouBtn";
            btn_Jyotai.Text = "選択なし";
            btn_Jyotai.CssClass = "JC31TantouBtn";
            updSyohin1.Update();
            updTokuisaki.Update();
            updsMitummori.Update();
            updbound.Update();
        }
        #endregion

        #region 見積コードテキストの変更
        protected void txtcMitumori_TextChanged(object sender, EventArgs e)
        {
            //if (!txtcMitumori.Text.Equals(""))
            //{
            //    String cMitumori = txtcMitumori.Text;
            //    txtcMitumori.Text = cMitumori.PadLeft(10, '0');
            //}
            //MitumoriKoumokuSort();
            //btnSearch_Click(sender, e);
        }
        #endregion

        #region datepickerから選択した後、対応テキストボックスに日付を設定
        protected void btnDateChange_Click(object sender, EventArgs e)
        {
            //txtMitumoriStartDate.Text = Request.Form[txtMitumoriStartDate.UniqueID];
            //txtMitumoriEndDate.Text = Request.Form[txtMitumoriEndDate.UniqueID];
            //txtJuchuuStartDate.Text = Request.Form[txtJuchuuStartDate.UniqueID];
            //txtJuchuuEndDate.Text = Request.Form[txtJuchuuEndDate.UniqueID];
            //txtUriageYoteiStartDate.Text = Request.Form[txtUriageYoteiStartDate.UniqueID];
            //txtUriageYoteiEndDate.Text = Request.Form[txtUriageYoteiEndDate.UniqueID];
        }
        #endregion

        #region 見積検索ポップアップを閉じる
        protected void btnHeaderCross_Click(object sender, EventArgs e)
        {
            ViewState["dt_Mitumori"] = null;
            Session["cMitumori"] = null;
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "parentButtonClick('btn_CloseMitumoriSearch','"+hdnHome.Value+"');", true);
        }
        #endregion

        #region グリッドの見積コードはをクリックする「見積登録＆明細画面へ」
        protected void LK_cMitumori_Click(object sender, EventArgs e)
        {
            //LinkButton lk = sender as LinkButton;

            //Session["btko"] = "true";
            //Session["fBukkenName"] = "true";
            //Session["cMitumori"] = lk.Text;
            //ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "parentButtonClick('btn_CloseMitumoriSearch','" + hdnHome.Value + "');", true);
        }
        #endregion

        #region btnDisplayItemSetting_Click
        protected void btnDisplayItemSetting_Click(object sender, EventArgs e)
        {
            //SessionUtility.SetSession("HOME", "Popup");
            //Session["HyoujiID"] = "mitsumori";
            //Session["HyoujiSettingID"] = "mitsumori";
            //ifpnlHyoujiSetPopUp.Attributes.Add("class", "HyoujiSettingIframe JC12mitsumoriiframeStyle");
            //ifpnlHyoujiSetPopUp.Src = "JC08HyoujiSetting.aspx";
            //mpeHyoujiSetPopUp.Show();
            //updHyoujiSet.Update();

            SessionUtility.SetSession("HOME", "Popup");
            Session["HyoujiID"] = "mitsumori";
            Session["HyoujiSettingID"] = "mitsumori";
            ifpnlHyoujiSetPopUp.Attributes.Add("class", "HyoujiSettingIframe mitsumoriiframeStyle");

            String id = Request.QueryString["id"];
            ifpnlHyoujiSetPopUp.Src = "JC08HyoujiSetting.aspx?id=" + id;
            mpeHyoujiSetPopUp.Show();
            updHyoujiSet.Update();
        }
        #endregion

        #region 表示項目設定「保存ボタン」
        protected void btnHyoujiSettingSave_Click(object sender, EventArgs e)
        {
            ifpnlHyoujiSetPopUp.Src = "";
            mpeHyoujiSetPopUp.Hide();
            updHyoujiSet.Update();
            MitumoriKoumokuSort();
        }
        #endregion

        #region 表示項目設定「閉じる」
        protected void btnHyoujiSettingClose_Click(object sender, EventArgs e)
        {
            ifpnlHyoujiSetPopUp.Src = "";
            mpeHyoujiSetPopUp.Hide();
            updHyoujiSet.Update();
        }
        #endregion
        
        #region GetMitumoriColumn()
        private void GetMitumoriColumn()
        {
            String id = Request.QueryString["id"];
            DataTable dt_db = JC01Login_Class.Get_DB(id);
            String db = dt_db.Rows[0]["db"].ToString();
            String mail = dt_db.Rows[0]["mail"].ToString();
            ConstantVal.DB_NAME = db;

            JC07Home_Class JC07Home = new JC07Home_Class();
            JC07Home.loginId = mail;
            JC07Home.cListId = "2";
            dt_MitsumoriKomoku = new DataTable();
            dt_MitsumoriKomoku = JC07Home.KomokuSetting();
        }
        #endregion

        #region GV_Mitumori_Original_RowDataBound
        protected void GV_Mitumori_Original_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //if (ViewState["dt_Mitumori"] != null)
                //{
                //    DataTable dt_mitsumori = ViewState["dt_Mitumori"] as DataTable;
                //    if (dt_mitsumori.Columns.Count > 0)
                //    {
                //        //リンク項目
                //        if (linkMutsucol != "")
                //        {
                //            int linkMitsucolIdx = int.Parse(linkMutsucol);

                //            LinkButton lnkbtn = new LinkButton();
                //            lnkbtn.ID = "lnkView";
                //            //lnkbtn.Text = mitsumoriId;
                //            lnkbtn.CommandName = "Select";
                //            e.Row.Cells[linkMitsucolIdx].Controls.Add(lnkbtn);
                //        }
                //    }
                //}

                 e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.GV_Mitumori_Original, "Select$" + e.Row.RowIndex);
            }
        }

        protected void GV_MitumoriPg_Original_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Visible = false;
            }
        }
        #endregion

        #region MitumoriKoumokuSort
        public void MitumoriKoumokuSort()
        {
            if (ViewState["dt_Mitumori"] != null)
            {
                var columns = GV_Mitumori.Columns.CloneFields();
                if (GV_Mitumori_Original.Columns.Count > 1)
                {
                    GV_Mitumori_Original.Columns.Clear();
                    GV_Mitumori_Original.Columns.Add(columns[0]);
                }
                DataTable dt = ViewState["dt_Mitumori"] as DataTable;
                DataTable dtpg = ViewState["dtpg_Mitumori"] as DataTable;

                GV_Mitumori.DataSource = dt;
                GV_Mitumori.DataBind();
                GridView1.DataSource = dtpg;
                GridView1.DataBind();
                totalDataCount = dtpg.Rows.Count;
                GV_Mitumori_Original.PageSize = GV_Mitumori.PageSize;
                GV_Mitumori_Original.PageIndex = GV_Mitumori.PageIndex;

                GetMitumoriColumn();

                DataRow[] rows = dt_MitsumoriKomoku.Select("sHYOUJI = '画像'");
                foreach (DataRow row in rows)
                {
                    dt_MitsumoriKomoku.Rows.Remove(row);
                }

                for (int i = 0; i < dt_MitsumoriKomoku.Rows.Count; i++)
                {
                    if (dt_MitsumoriKomoku.Rows[i]["sHYOUJI"].ToString() == "見積コード")
                    {
                        var cMitumori_column = GV_Mitumori.Columns[0];
                        GV_Mitumori_Original.Columns.Insert(i, cMitumori_column);
                        linkMutsucol = i.ToString();
                    }
                    else if (dt_MitsumoriKomoku.Rows[i]["sHYOUJI"].ToString() == "見積名")
                    {
                        var sMitumori_column = GV_Mitumori.Columns[1];
                        GV_Mitumori_Original.Columns.Insert(i, sMitumori_column);
                    }
                    else if (dt_MitsumoriKomoku.Rows[i]["sHYOUJI"].ToString() == "メモ")
                    {
                        var shanai_column = GV_Mitumori.Columns[8];
                        GV_Mitumori_Original.Columns.Insert(i, shanai_column);
                    }
                    else if (dt_MitsumoriKomoku.Rows[i]["sHYOUJI"].ToString() == "備考")
                    {
                        var biko_column = GV_Mitumori.Columns[9];
                        GV_Mitumori_Original.Columns.Insert(i, biko_column);
                    }
                    else if (dt_MitsumoriKomoku.Rows[i]["sHYOUJI"].ToString() == "得意先")
                    {
                        var tokuisaki_column = GV_Mitumori.Columns[10];
                        GV_Mitumori_Original.Columns.Insert(i, tokuisaki_column);
                    }
                    else if (dt_MitsumoriKomoku.Rows[i]["sHYOUJI"].ToString() == "得意先担当者")
                    {
                        var tokuisakiTantou_column = GV_Mitumori.Columns[11];
                        GV_Mitumori_Original.Columns.Insert(i, tokuisakiTantou_column);
                    }
                    else if (dt_MitsumoriKomoku.Rows[i]["sHYOUJI"].ToString() == "見積日")
                    {
                        var mitumoribi_column = GV_Mitumori.Columns[4];
                        GV_Mitumori_Original.Columns.Insert(i, mitumoribi_column);
                    }
                    else if (dt_MitsumoriKomoku.Rows[i]["sHYOUJI"].ToString() == "自社担当者")
                    {
                        var eigyoTantou_column = GV_Mitumori.Columns[2];
                        GV_Mitumori_Original.Columns.Insert(i, eigyoTantou_column);
                    }
                    else if (dt_MitsumoriKomoku.Rows[i]["sHYOUJI"].ToString() == "合計金額")
                    {
                        var gokeiKingaku_column = GV_Mitumori.Columns[5];
                        GV_Mitumori_Original.Columns.Insert(i, gokeiKingaku_column);
                    }
                    else if (dt_MitsumoriKomoku.Rows[i]["sHYOUJI"].ToString() == "金額粗利")
                    {
                        var Arai_column = GV_Mitumori.Columns[7];
                        GV_Mitumori_Original.Columns.Insert(i, Arai_column);
                    }
                    else if (dt_MitsumoriKomoku.Rows[i]["sHYOUJI"].ToString() == "作成者")
                    {
                        var sakuseisya_column = GV_Mitumori.Columns[3];
                        GV_Mitumori_Original.Columns.Insert(i, sakuseisya_column);
                    }
                    else if (dt_MitsumoriKomoku.Rows[i]["sHYOUJI"].ToString() == "見積状態")
                    {
                        var joutai_column = GV_Mitumori.Columns[6];
                        GV_Mitumori_Original.Columns.Insert(i, joutai_column);
                    }
                }
                GV_Mitumori_Original.DataSource = dt;
                GV_Mitumori_Original.DataBind();
                if (dt.Rows.Count == 0)
                {
                    lblDataNai.Visible = true;
                }
                else
                {
                    lblDataNai.Visible = false;
                }
                updMitsumoriGrid.Update();
                //JoukenReload();
                //updMitsumoriGrid.Update();
                updBody.Update();
            }
        }
        #endregion

        #region btn_Tantousya_Click
        protected void btn_Tantousya_Click(object sender, EventArgs e)
        {
            DataTable dtSelect = new DataTable();
            dtSelect.Columns.Add("code");
            dtSelect.Columns.Add("name");
            if (DDL_Tantousya.Items.Count > 0)
            {
                for (int i = 0; i < DDL_Tantousya.Items.Count; i++)
                {
                    DataRow dr = dtSelect.NewRow();
                    dr[0] = DDL_Tantousya.Items[i].Value;
                    dr[1] = DDL_Tantousya.Items[i].Text;
                    dtSelect.Rows.Add(dr);
                }
            }
            else
            {
                DataRow dr = dtSelect.NewRow();
                dr[0] = "";
                dr[1] = "選択なし";
                dtSelect.Rows.Add(dr);
            }
            SessionUtility.SetSession("HOME", "Popup");
            Session["isKensaku"] = "true";
            Session["dtTantou_MultiSelect"] = dtSelect;
            ifSentakuPopup.Style["width"] = "100vw";
            ifSentakuPopup.Style["height"] = "100vh";

            String id = Request.QueryString["id"];
            ifSentakuPopup.Src = "JC14TantouKensaku.aspx?id=" + id;
            mpeSentakuPopup.Show();

            //lblsJISHATANTOUSHA.Attributes.Add("onClick", "BtnClick('MainContent_BT_JisyaTantousya_Add')");
            updSentakuPopup.Update();
        }
        #endregion 

        #region btnClose_Click 選択サブ画面を閉じる処理
        protected void btnClose_Click(object sender, EventArgs e)
        {
            ifSentakuPopup.Src = "";
            mpeSentakuPopup.Hide();
            updSentakuPopup.Update();
        }
        #endregion

        #region 担当者サブ画面を閉じる時のフォーカス処理
        protected void btnJishaTantouSelect_Click(object sender, EventArgs e)
        {
            if (Session["dtTantou_MultiSelect"] != null)
            {
                DataTable dt = Session["dtTantou_MultiSelect"] as DataTable;
                DDL_Tantousya.DataSource = dt;
                DDL_Tantousya.DataTextField = "name";
                DDL_Tantousya.DataValueField = "code";
                DDL_Tantousya.DataBind();
                if (dt.Rows.Count > 0)
                {
                    btn_Tantousya.Text = DDL_Tantousya.Items.Count + "人";
                    btn_Tantousya.CssClass = "JC31ClearBtn";
                }
                else
                {
                    btn_Tantousya.Text = "選択なし";
                    btn_Tantousya.CssClass = "JC31TantouBtn";
                }
                updTantousha.Update();
                //JoukenReload();
                //btnSearch_Click(sender, e);
            }

            ifSentakuPopup.Src = "";
            mpeSentakuPopup.Hide();
            updSentakuPopup.Update();
        }
        #endregion

        #region JoukenReload
        private void JoukenReload()
        {
            TC_Jouken.Controls.Clear();
            updJoken.Update();

            if (DDL_Jyouken.Items.Count > 0)
            {
                for (int i = 0; i < DDL_Jyouken.Items.Count; i++)
                {
                    Panel pn = new Panel();
                    pn.ID = DDL_Jyouken.Items[i].Value;
                    pn.CssClass = "JC12JokenDiv";
                    Label jisyaLabel = new Label();
                    jisyaLabel.Font.Size = 10;
                    jisyaLabel.ForeColor = System.Drawing.ColorTranslator.FromHtml("#495057");
                    jisyaLabel.ID = DDL_Jyouken.Items[i].Value + "lbl_JokenLabel";
                    if (DDL_Jyouken.Items[i].Value.Contains("自社担当者"))
                    {
                        jisyaLabel.Text = "自社担当者";
                    }
                    else if (DDL_Jyouken.Items[i].Value.Contains("見積状態"))
                    {
                        jisyaLabel.Text = "見積状態";
                    }
                    else if (DDL_Jyouken.Items[i].Value.Contains("Start"))
                    {
                        jisyaLabel.Text = DDL_Jyouken.Items[i].Value.Replace("Start", "");
                    }
                    else if (DDL_Jyouken.Items[i].Value.Contains("End"))
                    {
                        jisyaLabel.Text = DDL_Jyouken.Items[i].Value.Replace("End", "");
                    }
                    else if (DDL_Jyouken.Items[i].Value.Contains("商品名"))
                    {
                        jisyaLabel.Text = "商品名";
                    }
                    else
                    {
                        jisyaLabel.Text = DDL_Jyouken.Items[i].Value;
                    }
                    jisyaLabel.CssClass = "JC12JokenLabel";

                    Label cjisya = new Label();
                    cjisya.Font.Size = 10;
                    cjisya.ForeColor = System.Drawing.ColorTranslator.FromHtml("#495057");
                    cjisya.ID = DDL_Jyouken.Items[i].Value + "lblsKYOTEN";
                    if (DDL_Jyouken.Items[i].Value.Contains("Start"))
                    {
                        String end = "";
                        try
                        {
                            end = DDL_Jyouken.Items[i + 1].Value.Replace("End", "");
                        }
                        catch { }
                        if (!String.IsNullOrEmpty(end) && DDL_Jyouken.Items[i].Value.Replace("Start", "") == end)
                        {
                            cjisya.Text = DDL_Jyouken.Items[i].Text + "～" + DDL_Jyouken.Items[i + 1].Text;
                            i = i + 1;
                        }
                        else
                        {
                            cjisya.Text = DDL_Jyouken.Items[i].Text + "～";
                        }
                    }
                    else if (DDL_Jyouken.Items[i].Value.Contains("End"))
                    {
                        cjisya.Text = "～" + DDL_Jyouken.Items[i].Text;
                    }
                    else
                    {
                        cjisya.Text = DDL_Jyouken.Items[i].Text;
                    }
                    Button btn_Cross = new Button();
                    btn_Cross.Font.Size = 9;
                    btn_Cross.BackColor = System.Drawing.Color.White;
                    btn_Cross.Text = "✕";
                    btn_Cross.ID = "btn_Jisyacross_" + jisyaLabel.Text + "_" + DDL_Jyouken.Items[i].Value;
                    btn_Cross.CssClass = "JC12JokenCross";
                    btn_Cross.Click += new EventHandler(btnJokenCross_Click);

                    pn.Controls.Add(jisyaLabel);
                    pn.Controls.Add(cjisya);
                    pn.Controls.Add(btn_Cross);

                    TC_Jouken.Controls.Add(pn);
                    updJoken.ContentTemplateContainer.Controls.Add(TC_Jouken);
                    updJoken.Update();
                }
            }

            updJoken.Update();
            updTantousha.Update();
        }
        #endregion

        #region btnJokenCross_Click 条件削除
        protected void btnJokenCross_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            String id = btn.ID;
            id = id.Replace("btn_Jisyacross_", "");
            int index = id.IndexOf("_");
            String label = id.Substring(0, index);
            id = id.Substring(index + 1, (id.Length - label.Length) - 1);
            ListItem removeItem = DDL_Jyouken.Items.FindByValue(id);
            if (label.Equals("見積コード"))
            {
                if (Session["M_code"].ToString().Equals(removeItem.Text))
                {
                    TB_MitsuListCode.Text = "";
                    Session["M_code"] = null;

                }
            }
            else if (label.Equals("見積名"))
            {
                if (Session["M_name"].ToString().Equals(removeItem.Text))
                {
                    txtsMitumori.Text = "";
                    Session["M_name"] = null;
                    updsMitummori.Update();
                }
            }
            else if (label.Equals("見積状態"))
            {
                try
                {
                    ListItem jyotai = DDL_Jyotai.Items.FindByValue(removeItem.Value.Replace("見積状態", ""));
                    if (jyotai != null)
                    {
                        DDL_Jyotai.Items.Remove(jyotai);
                        if (DDL_Jyotai.Items.Count > 0)
                        {
                            btn_Jyotai.Text = DDL_Jyotai.Items.Count.ToString();
                            btn_Jyotai.CssClass = "JC31ClearBtn";
                        }
                        else
                        {
                            btn_Jyotai.Text = "選択なし";
                            btn_Jyotai.CssClass = "JC31TantouBtn";
                        }
                    }
                }
                catch { }
            }
            else if (label.Equals("得意先"))
            {
                if (Session["M_touisaki"].ToString().Equals(removeItem.Text))
                {
                    txtTokuisaki.Text = "";
                    Session["M_touisaki"] = null;
                    updTokuisaki.Update();
                }
            }
            else if (label.Equals("得意先担当者"))
            {
                if (Session["M_touisaki_tan"].ToString().Equals(removeItem.Text))
                {
                    txtTokuisakiTantou.Text = "";
                    Session["M_touisaki_tan"] = null;
                }
            }
            else if (label.Equals("自社担当者"))
            {
                try
                {
                    ListItem tantousya = DDL_Tantousya.Items.FindByValue(removeItem.Value.Replace("自社担当者", ""));
                    if (tantousya != null)
                    {
                        DDL_Tantousya.Items.Remove(tantousya);
                        if (DDL_Tantousya.Items.Count > 0)
                        {
                            btn_Tantousya.Text = DDL_Tantousya.Items.Count + "人";
                            btn_Tantousya.CssClass = "JC31ClearBtn";
                        }
                        else
                        {
                            btn_Tantousya.Text = "選択なし";
                            btn_Tantousya.CssClass = "JC31TantouBtn";
                        }
                    }
                }
                catch { }
            }
            else if (label.Equals("見積日"))
            {
                if (removeItem.Value.Equals("見積日Start") && removeItem.Text.Equals(Session["M_startdate"].ToString()))
                {
                    btndMitumoriStartCross_Click(sender, e);
                    Session["M_startdate"] = null;
                }
                else if (removeItem.Value.Equals("見積日End") && removeItem.Text.Equals(Session["M_enddate"].ToString()))
                {
                    btndMitumoriEndCross_Click(sender, e);
                    Session["M_enddate"] = null;
                }
            }
            else if (label.Equals("受注日"))
            {
                if (removeItem.Value.Equals("受注日Start") && removeItem.Text.Equals(Session["J_startdate"].ToString()))
                {
                    btndJuchuuStartDateCross_Click(sender, e);
                    Session["J_startdate"] = null;
                }
                else if (removeItem.Value.Equals("受注日End") && removeItem.Text.Equals(Session["J_enddate"].ToString()))
                {
                    btndJuchuuEndDateCross_Click(sender, e);
                    Session["J_enddate"] = null;
                }
            }
            else if (label.Equals("売上予定日"))
            {
                if (removeItem.Value.Equals("売上予定日Start") && removeItem.Text.Equals(Session["U_startdate"].ToString()))
                {
                    btndUriageYoteiStartDateCross_Click(sender, e);
                    Session["U_startdate"] = null;
                }
                else if (removeItem.Value.Equals("売上予定日End") && removeItem.Text.Equals(Session["U_enddate"].ToString()))
                {
                    btndUriageYoteiEndDateCross_Click(sender, e);
                    Session["U_enddate"] = null;
                }
            }
            else if (label.Equals("商品名"))
            {
                if (removeItem.Value.Equals("商品名1") && removeItem.Text.Equals(Session["syohin1"].ToString()))
                {
                    txtSyohin1.Text = "";
                    Session["syohin1"] = null;
                    updSyohin1.Update();
                }
                else if (removeItem.Value.Equals("商品名2") && removeItem.Text.Equals(Session["syohin2"].ToString()))
                {
                    txtSyohin2.Text = "";
                    Session["syohin2"] = null;
                }
                else if (removeItem.Value.Equals("商品名3") && removeItem.Text.Equals(Session["syohin3"].ToString()))
                {
                    txtSyohin3.Text = "";
                    Session["syohin3"] = null;
                }
            }
            else if (label.Equals("社内メモ"))
            {
                if (removeItem.Value.Equals("社内メモ") && removeItem.Text.Equals(Session["memo"].ToString()))
                {
                    txtmemo.Text = "";
                    Session["memo"] = null;
                }
            }
            updTantousha.Update();
            updBody.Update();
            //DDL_Jyouken.Items.Remove(removeItem);
            //updTantousha.Update();
            //JoukenReload();
            btnSearch_Click(sender, e);
        }
        #endregion

        #region BT_MitsumoriSyosai_Click
        protected void BT_MitsumoriSyosai_Click(object sender, EventArgs e)
        {
            if (Panel1.Visible == true)
            {
                BT_MitsumoriSyosai.Style.Add("background-image", "url('../Img/expand-more-1782315-1514165.png')");
                Panel1.Visible = false;
            }
            else
            {
                BT_MitsumoriSyosai.Style.Add("background-image", "url('../Img/expand-less-1781206-1518580.png')");
                Panel1.Visible = true;
            }
            updbound.Update();
        }
        #endregion

        #region btnToLogin_Click
        protected void btnToLogin_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "parentButtonClick('btnToLogin','" + hdnHome.Value + "');", true);
        }
        #endregion

        #region 見積開始日を選択
        protected void btnMitumoriStartDate_Click(object sender, EventArgs e)
        {
            SessionUtility.SetSession("HOME", "Popup");

            String id = Request.QueryString["id"];
            ifdatePopup.Src = "JCHidukeSelect.aspx?id=" + id;
            mpedatePopup.Show();

            ViewState["DATETIME"] = btnMitumoriStartDate.ID;

            if (!String.IsNullOrEmpty(lblMitumoriStartDate.Text))
            {
                DateTime dt = DateTime.Parse(lblMitumoriStartDate.Text);
                Session["CALENDARDATE"] = dt.ToString("yyyy/MM/dd");
                Session["YEAR"] = dt.Year;
            }

            // 設定したラベルをクリック時、時間設定ポップアップ画面を表示する
            lblMitumoriStartDate.Attributes.Add("onClick", "BtnClick('btnMitumoriStartDate')");
            upddatePopup.Update();
        }
        #endregion

        #region 見積終了日を選択
        protected void btnMitumoriEndDate_Click(object sender, EventArgs e)
        {
            SessionUtility.SetSession("HOME", "Popup");
            String id = Request.QueryString["id"];
            ifdatePopup.Src = "JCHidukeSelect.aspx?id=" + id;
            mpedatePopup.Show();

            ViewState["DATETIME"] = btnMitumoriEndDate.ID;

            if (!String.IsNullOrEmpty(lblMitumoriEndDate.Text))
            {
                DateTime dt = DateTime.Parse(lblMitumoriEndDate.Text);
                Session["CALENDARDATE"] = dt.ToString("yyyy/MM/dd");
                Session["YEAR"] = dt.Year;
            }

            // 設定したラベルをクリック時、時間設定ポップアップ画面を表示する
            lblMitumoriEndDate.Attributes.Add("onClick", "BtnClick('btnMitumoriEndDate')");
            upddatePopup.Update();
        }
        #endregion

        #region 受注開始日を選択
        protected void btnJuchuuStartDate_Click(object sender, EventArgs e)
        {
            SessionUtility.SetSession("HOME", "Popup");

            String id = Request.QueryString["id"];
            ifdatePopup.Src = "JCHidukeSelect.aspx?id=" + id;
            mpedatePopup.Show();

            ViewState["DATETIME"] = btnJuchuuStartDate.ID;

            if (!String.IsNullOrEmpty(lblJuchuuStartDate.Text))
            {
                DateTime dt = DateTime.Parse(lblJuchuuStartDate.Text);
                Session["CALENDARDATE"] = dt.ToString("yyyy/MM/dd");
                Session["YEAR"] = dt.Year;
            }

            // 設定したラベルをクリック時、時間設定ポップアップ画面を表示する
            lblJuchuuStartDate.Attributes.Add("onClick", "BtnClick('btnJuchuuStartDate')");
            upddatePopup.Update();
        }
        #endregion

        #region 受注終了日を選択
        protected void btnJuchuuEndDate_Click(object sender, EventArgs e)
        {
            SessionUtility.SetSession("HOME", "Popup");

            String id = Request.QueryString["id"];
            ifdatePopup.Src = "JCHidukeSelect.aspx?id=" + id;
            mpedatePopup.Show();

            ViewState["DATETIME"] = btnJuchuuEndDate.ID;

            if (!String.IsNullOrEmpty(lblJuchuuEndDate.Text))
            {
                DateTime dt = DateTime.Parse(lblJuchuuEndDate.Text);
                Session["CALENDARDATE"] = dt.ToString("yyyy/MM/dd");
                Session["YEAR"] = dt.Year;
            }

            // 設定したラベルをクリック時、時間設定ポップアップ画面を表示する
            lblJuchuuEndDate.Attributes.Add("onClick", "BtnClick('btnJuchuuEndDate')");
            upddatePopup.Update();
        }
        #endregion

        #region 売上予定開始日を選択
        protected void btnUriageYoteiStartDate_Click(object sender, EventArgs e)
        {
            SessionUtility.SetSession("HOME", "Popup");

            String id = Request.QueryString["id"];
            ifdatePopup.Src = "JCHidukeSelect.aspx?id=" + id;
            mpedatePopup.Show();

            ViewState["DATETIME"] = btnUriageYoteiStartDate.ID;

            if (!String.IsNullOrEmpty(lblUriageYoteiStartDate.Text))
            {
                DateTime dt = DateTime.Parse(lblUriageYoteiStartDate.Text);
                Session["CALENDARDATE"] = dt.ToString("yyyy/MM/dd");
                Session["YEAR"] = dt.Year;
            }

            // 設定したラベルをクリック時、時間設定ポップアップ画面を表示する
            lblUriageYoteiStartDate.Attributes.Add("onClick", "BtnClick('btnUriageYoteiStartDate')");
            upddatePopup.Update();
        }
        #endregion

        #region 売上予定終了日を選択
        protected void btnUriageYoteiEndDate_Click(object sender, EventArgs e)
        {
            SessionUtility.SetSession("HOME", "Popup");

            String id = Request.QueryString["id"];
            ifdatePopup.Src = "JCHidukeSelect.aspx?id=" + id;
            mpedatePopup.Show();

            ViewState["DATETIME"] = btnUriageYoteiEndDate.ID;

            if (!String.IsNullOrEmpty(lblUriageYoteiEndDate.Text))
            {
                DateTime dt = DateTime.Parse(lblUriageYoteiEndDate.Text);
                Session["CALENDARDATE"] = dt.ToString("yyyy/MM/dd");
                Session["YEAR"] = dt.Year;
            }

            // 設定したラベルをクリック時、時間設定ポップアップ画面を表示する
            lblUriageYoteiEndDate.Attributes.Add("onClick", "BtnClick('btnUriageYoteiEndDate')");
            upddatePopup.Update();
        }
        #endregion

        #region 日付を1日増す、日付を1日減らす、日付を削除「見積開始日、見積終了日、受注開始日、受注終了日、売上予定開始日、売上予定終了日」

        #region btndMitumoriStartCross_Click
        protected void btndMitumoriStartCross_Click(object sender, EventArgs e)
        {
            lblMitumoriStartDate.Text = "";
            btnMitumoriStartDate.Style["display"] = "block";
            divMitumoriStartDate.Style["display"] = "none";
            updMitumoriStartDate.Update();
        }
        #endregion

        #region btndMitumoriEndCross_Click
        protected void btndMitumoriEndCross_Click(object sender, EventArgs e)
        {
            lblMitumoriEndDate.Text = "";
            btnMitumoriEndDate.Style["display"] = "block";
            divMitumoriEndDate.Style["display"] = "none";
            UpdMitumoriEndDate.Update();
        }
        #endregion

        #region btndJuchuuStartDateCross_Click
        protected void btndJuchuuStartDateCross_Click(object sender, EventArgs e)
        {
            lblJuchuuStartDate.Text = "";
            btnJuchuuStartDate.Style["display"] = "block";
            divJuchuuStartDate.Style["display"] = "none";
            updJuchuuStartDate.Update();
        }
        #endregion

        #region btndJuchuuEndDateCross_Click
        protected void btndJuchuuEndDateCross_Click(object sender, EventArgs e)
        {
            lblJuchuuEndDate.Text = "";
            btnJuchuuEndDate.Style["display"] = "block";
            divJuchuuEndDate.Style["display"] = "none";
            updJuchuuEndDate.Update();
        }
        #endregion

        #region btndUriageYoteiStartDateCross_Click
        protected void btndUriageYoteiStartDateCross_Click(object sender, EventArgs e)
        {
            lblUriageYoteiStartDate.Text = "";
            btnUriageYoteiStartDate.Style["display"] = "block";
            divUriageYoteiStartDate.Style["display"] = "none";
            updUriageYoteiStartDate.Update();
        }
        #endregion

        #region btndUriageYoteiEndDateCross_Click
        protected void btndUriageYoteiEndDateCross_Click(object sender, EventArgs e)
        {
            lblUriageYoteiEndDate.Text = "";
            btnUriageYoteiEndDate.Style["display"] = "block";
            divUriageYoteiEndDate.Style["display"] = "none";
            updUriageYoteiEndDate.Update();
        }
        #endregion

        #endregion

        #region "日付カレンダーポップアップの【X】ボタンクリック処理"
        protected void btnCalendarClose_Click(object sender, EventArgs e)
        {
            // 【日付サブ画面】を閉じる
            CloseSentakuSub();
            // フォーカスする
            CalendarFoucs();
        }
        #endregion

        #region "日付カレンダーポップアップの【設定】ボタンを押す処理"
        protected void btnCalendarSettei_Click(object sender, EventArgs e)
        {
            DateTime dtSelectedDate;

            // 【日付サブ画面】を閉じる
            CloseSentakuSub();
            string strBtnID = (string)ViewState["DATETIME"];
            string strCalendarDateTime = (string)Session["CALENDARDATETIME"];
            if (Session["CALENDARDATETIME"] != null)
            {
                strCalendarDateTime = (string)Session["CALENDARDATETIME"];
                dtSelectedDate = DateTime.Parse(strCalendarDateTime);
                if (strBtnID == btnMitumoriStartDate.ID)
                {
                    MitumoriStartDateDataBind(strCalendarDateTime, strBtnID);
                }
                else if (strBtnID == btnMitumoriEndDate.ID)
                {
                    MitumoriEndDateDataBind(strCalendarDateTime, strBtnID);
                }
                else if (strBtnID == btnJuchuuStartDate.ID)
                {
                    JuuchuStartDateDataBind(strCalendarDateTime, strBtnID);
                }
                else if (strBtnID == btnJuchuuEndDate.ID)
                {
                    JuuchuEndDateDataBind(strCalendarDateTime, strBtnID);
                }
                else if (strBtnID == btnUriageYoteiStartDate.ID)
                {
                    UriageYoteiStartDateDataBind(strCalendarDateTime, strBtnID);
                }
                else if (strBtnID == btnUriageYoteiEndDate.ID)
                {
                    UriageYoteiEndDateDataBind(strCalendarDateTime, strBtnID);
                }

                //lblHdnAnkenTextChange.Text = "true";
            }
            CalendarFoucs();
            updBody.Update();
        }

        #endregion

        #region "選択日付サブ画面を閉じる処理"
        protected void CloseSentakuSub()
        {
            ifdatePopup.Src = "";
            mpedatePopup.Hide();
            upddatePopup.Update();
        }
        #endregion

        #region "日付サブ画面を閉じる時のフォーカス処理"
        protected void CalendarFoucs()
        {
            string strBtnID = (string)ViewState["DATETIME"];
            if (strBtnID == btnMitumoriStartDate.ID)
            {
                if (btnMitumoriStartDate.Style["display"] != "none")
                {
                    btnMitumoriStartDate.Focus();
                }
            }
            else if (strBtnID == btnMitumoriEndDate.ID)
            {
                if (btnMitumoriEndDate.Style["display"] != "none")
                {
                    btnMitumoriEndDate.Focus();
                }
            }

            else if (strBtnID == btnJuchuuStartDate.ID)
            {
                if (btnJuchuuStartDate.Style["display"] != "none")
                {
                    btnJuchuuStartDate.Focus();
                }
            }
            else if (strBtnID == btnJuchuuEndDate.ID)
            {
                if (btnJuchuuEndDate.Style["display"] != "none")
                {
                    btnJuchuuEndDate.Focus();
                }
            }
            else if (strBtnID == btnUriageYoteiStartDate.ID)
            {
                if (btnUriageYoteiStartDate.Style["display"] != "none")
                {
                    btnUriageYoteiStartDate.Focus();
                }
            }
            else if (strBtnID == btnUriageYoteiEndDate.ID)
            {
                if (btnUriageYoteiEndDate.Style["display"] != "none")
                {
                    btnUriageYoteiEndDate.Focus();
                }
            }
        }
        #endregion

        #region "見積開始日データバインディング処理"
        protected void MitumoriStartDateDataBind(string strCalendarDateTime, string strImgbtnID)
        {
            DateTime dtDeliveryDate = DateTime.Parse(strCalendarDateTime);
            lblMitumoriStartDate.Text = dtDeliveryDate.ToString("yyyy/MM/dd");
            btnMitumoriStartDate.Style["display"] = "none";
            divMitumoriStartDate.Style["display"] = "block";
            updMitumoriStartDate.Update();
        }
        #endregion

        #region "見積終了日データバインディング処理"
        protected void MitumoriEndDateDataBind(string strCalendarDateTime, string strImgbtnID)
        {
            DateTime dtDeliveryDate = DateTime.Parse(strCalendarDateTime);
            lblMitumoriEndDate.Text = dtDeliveryDate.ToString("yyyy/MM/dd");
            btnMitumoriEndDate.Style["display"] = "none";
            divMitumoriEndDate.Style["display"] = "block";
            UpdMitumoriEndDate.Update();
        }
        #endregion

        #region "受注開始日データバインディング処理"
        protected void JuuchuStartDateDataBind(string strCalendarDateTime, string strImgbtnID)
        {
            DateTime dtDeliveryDate = DateTime.Parse(strCalendarDateTime);
            lblJuchuuStartDate.Text = dtDeliveryDate.ToString("yyyy/MM/dd");
            btnJuchuuStartDate.Style["display"] = "none";
            divJuchuuStartDate.Style["display"] = "block";
            updJuchuuStartDate.Update();
        }
        #endregion

        #region "受注終了日データバインディング処理"
        protected void JuuchuEndDateDataBind(string strCalendarDateTime, string strImgbtnID)
        {
            DateTime dtDeliveryDate = DateTime.Parse(strCalendarDateTime);
            lblJuchuuEndDate.Text = dtDeliveryDate.ToString("yyyy/MM/dd");
            btnJuchuuEndDate.Style["display"] = "none";
            divJuchuuEndDate.Style["display"] = "block";
            updJuchuuEndDate.Update();
        }
        #endregion

        #region "売上予定開始日データバインディング処理"
        protected void UriageYoteiStartDateDataBind(string strCalendarDateTime, string strImgbtnID)
        {
            DateTime dtDeliveryDate = DateTime.Parse(strCalendarDateTime);
            lblUriageYoteiStartDate.Text = dtDeliveryDate.ToString("yyyy/MM/dd");
            btnUriageYoteiStartDate.Style["display"] = "none";
            divUriageYoteiStartDate.Style["display"] = "block";
            updUriageYoteiStartDate.Update();
        }
        #endregion

        #region "売上予定終了日データバインディング処理"
        protected void UriageYoteiEndDateDataBind(string strCalendarDateTime, string strImgbtnID)
        {
            DateTime dtDeliveryDate = DateTime.Parse(strCalendarDateTime);
            lblUriageYoteiEndDate.Text = dtDeliveryDate.ToString("yyyy/MM/dd");
            btnUriageYoteiEndDate.Style["display"] = "none";
            divUriageYoteiEndDate.Style["display"] = "block";
            updUriageYoteiEndDate.Update();
        }
        #endregion

        #region btn_Jyotai_Click()
        protected void btn_Jyotai_Click(object sender, EventArgs e)
        {
            DataTable dt_JSelect = new DataTable();
            dt_JSelect.Columns.Add("code");
            dt_JSelect.Columns.Add("name");
            if (DDL_Jyotai.Items.Count > 0)
            {
                for (int i = 0; i < DDL_Jyotai.Items.Count; i++)
                {
                    DataRow dr = dt_JSelect.NewRow();
                    dr[0] = DDL_Jyotai.Items[i].Value;
                    dr[1] = DDL_Jyotai.Items[i].Text;
                    dt_JSelect.Rows.Add(dr);
                }
            }
            else
            {
                DataRow dr = dt_JSelect.NewRow();
                dr[0] = "";
                dr[1] = "選択なし";
                dt_JSelect.Rows.Add(dr);
            }
            SessionUtility.SetSession("HOME", "Popup");
            Session["isKensaku"] = "true";
            Session["dtJyotai_MultiSelect"] = dt_JSelect;
            ifSentakuPopup.Style["width"] = "100vw";
            ifSentakuPopup.Style["height"] = "100vh";

            String id = Request.QueryString["id"];
            ifSentakuPopup.Src = "JC36_MitsuJyotaiKensaku.aspx?id=" + id;
            mpeSentakuPopup.Show();
            updSentakuPopup.Update();
        }
        #endregion

        #region Jyotai_Close()
        protected void btnClose1_Click(object sender, EventArgs e)
        {
            ifSentakuPopup.Src = "";
            mpeSentakuPopup.Hide();
            updSentakuPopup.Update();
        }
        #endregion

        #region btnJyoutaiSelect_Click()
        protected void btnJyoutaiSelect_Click(object sender, EventArgs e)
        {
            if (Session["dtJyotai_MultiSelect"] != null)
            {
                DataTable dt = Session["dtJyotai_MultiSelect"] as DataTable;
                DDL_Jyotai.DataSource = dt;
                DDL_Jyotai.DataTextField = "name";
                DDL_Jyotai.DataValueField = "code";
                DDL_Jyotai.DataBind();
                if (dt.Rows.Count > 0)
                {
                    btn_Jyotai.Text = DDL_Jyotai.Items.Count.ToString();
                    btn_Jyotai.CssClass = "JC31ClearBtn";
                }
                else
                {
                    btn_Jyotai.Text = "選択なし";
                    btn_Jyotai.CssClass = "JC31TantouBtn";
                }

                updMitumoriJoutai.Update();
            }

            ifSentakuPopup.Src = "";
            mpeSentakuPopup.Hide();
            updSentakuPopup.Update();
        }
        #endregion

        #region GV_Mitumori_Original_SelectedIndexChanged
        protected void GV_Mitumori_Original_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (HF_fSyouhin.Value == "1")
            {
                DIV_Body.Style.Add("display", "none");
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "PopupOpenButton();", true);
                SessionUtility.SetSession("HOME", "Popup");
                String id = Request.QueryString["id"];
                ifShinkiPopup.Src = "JC44MitumoriMeisaiSelect.aspx?id=" + id;
                int i = GV_Mitumori_Original.SelectedRow.RowIndex;
                Label lb_cMitu = (Label)GV_Mitumori_Original.Rows[i].FindControl("LB_cMitumori");
                Session["cMitumori"] = lb_cMitu.Text;
                mpeShinkiPopup.Show();
                updShinkiPopup.Update();
            }
            else
            {
                int i = GV_Mitumori_Original.SelectedRow.RowIndex;
                Label lb_cMitu = (Label)GV_Mitumori_Original.Rows[i].FindControl("LB_cMitumori");
                Session["btko"] = "true";
                Session["fBukkenName"] = "true";
                Session["cMitumori"] = lb_cMitu.Text;
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "parentButtonClick('btn_CloseMitumoriSearch','" + hdnHome.Value + "');", true);
            }
        }
        #endregion

        #region GridViewSortDirection
        public SortDirection GridViewSortDirection
        {
            get
            {
                if (ViewState["sortDirection"] == null)
                    ViewState["sortDirection"] = SortDirection.Ascending;
                return (SortDirection)ViewState["sortDirection"];
            }
            set
            {
                ViewState["sortDirection"] = value;
            }
        }
        #endregion

        #region SortGridView
        private void SortGridView(string sortExpression, string direction)
        {
            sort_expression = sortExpression;
            sort_direction = direction;
            BindGrid();
            updMitsumoriGrid.Update();
        }
        #endregion

        #region SortExpression
        public string SortExpression
        {
            get
            {
                if (ViewState["z_sortexpresion"] == null)
                    ViewState["z_sortexpresion"] = this.GV_Mitumori_Original.DataKeyNames[0].ToString();
                return ViewState["z_sortexpresion"].ToString();
            }
            set
            {
                ViewState["z_sortexpresion"] = value;
            }
        }
        #endregion

        #region GV_Mitumori_Sorting()
        protected void GV_Mitumori_Sorting(object sender, GridViewSortEventArgs e)
        {
            rowindex = Convert.ToInt32(GV_MiRowindex.Text);
            string sortExpression = e.SortExpression;
            ViewState["z_sortexpresion"] = e.SortExpression;
            if (GridViewSortDirection == SortDirection.Ascending)
            {
                GridViewSortDirection = SortDirection.Descending;
                SortGridView(sortExpression, "DESC");
            }
            else
            {
                GridViewSortDirection = SortDirection.Ascending;
                SortGridView(sortExpression, "ASC");
            }
        }
        #endregion

        #region GV_Mitumori_Original_RowCreated()
        protected void GV_Mitumori_Original_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (ViewState["sortDirection"] != null && ViewState["z_sortexpresion"] != null)

            {

                if (e.Row.RowType == DataControlRowType.Header)

                {

                    foreach (TableCell tablecell in e.Row.Cells)

                    {

                        if (tablecell.HasControls())

                        {

                            LinkButton sortLinkButton = null;

                            if (tablecell.Controls[0] is LinkButton)
                            {
                                sortLinkButton = (LinkButton)tablecell.Controls[0];

                            }

                            if (sortLinkButton != null && ViewState["z_sortexpresion"].ToString() == sortLinkButton.CommandArgument)
                            {
                                Image img = new Image();
                                img.Width = 20;

                                if (GridViewSortDirection == SortDirection.Ascending)
                                {

                                    img.ImageUrl = "../Img/expand-less-1781206-1518580.png";
                                }
                                else
                                {
                                    img.ImageUrl = "../Img/expand-more-1782315-1514165.png";
                                }

                                //tablecell.Controls.Add(new LiteralControl("&nbsp;"));

                                tablecell.Controls.Add(img);
                                break;
                            }
                        }

                    }

                }

            }
        }
        #endregion

        #region btn_SelectMeisaiCopy_Click //20220509 Added MayThu,MyatNoe
        protected void btn_SelectMeisaiCopy_Click(object sender, EventArgs e)
        {
            ifShinkiPopup.Src = "";  //20220519 MyatNoe Added
            mpeShinkiPopup.Hide();   //20220519 MyatNoe Added
            updShinkiPopup.Update(); //20220519 MyatNoe Added
            Session["btko"] = "true";
            Session["fBukkenName"] = "true";
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "parentButtonClick('btn_CloseMitumoriSearch','" + hdnHome.Value + "');", true);
        }
        #endregion

        #region btn_CloseMeisaiCopy_Click //20220509 Added MayThu,MyatNoe
        protected void btn_CloseMeisaiCopy_Click(object sender, EventArgs e)
        {
            DIV_Body.Style.Add("display", "block");
            ifShinkiPopup.Src = "";
            mpeShinkiPopup.Hide();
            updShinkiPopup.Update();
        }
        #endregion
    }
}