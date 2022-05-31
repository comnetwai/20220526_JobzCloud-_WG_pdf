using Common;
using jobzcolud.pdf;
using Microsoft.VisualBasic;
using MySql.Data.MySqlClient;
using Service;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace jobzcolud.WebFront
{
    public partial class JC31MitsumoriList : System.Web.UI.Page
    {
        DataTable dt_MitsumoriKomoku = null;
        string[] strExpand = new string[10];
        string motsumoriSetting = "";
        string fcol = "";
        string linkMutsucol = "";
        string imgcol = "";
        public Double totalDataCount = 0;
        MySqlConnection cn = null;
        public int rowindex, startRowIndex, endRowIndex;
        public int pageInd = 1;
        int total = 0;
        string sort_expression = "";
        string sort_direction = "";
        DataTable dt_Mitsuimg = new DataTable();
        DataTable dt_mitsumori = new DataTable();

        //20220118 MiMi Added start
        String cKyoten = "";
        public static int rmt_pcount1 = 0;//Page Numberの為に
        public static int pcount = 0;
        bool fMEISAI = false; //明細チェック
        bool fSYOUSAI = false; //詳細チェック
        bool fMIDASHI = false; //見出しチェック
        bool fHYOUSHI = false; //表紙チェック
        //20220118 MiMi Added End

        #region PageLoad()
        protected void Page_Load(object sender, EventArgs e)
        {
            JC99NavBar navbar_Master = (JC99NavBar)this.Master;
            navbar_Master.LK_navbardrop1.Style.Add(" background-color", "rgba(46,117,182)");
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
                    GridView1.PageIndex = 0;
                    rowindex = 0;
                    GV_MiRowindex.Text = rowindex.ToString();
                    GridView1.PageSize = 20;
                    ViewState["sortDirection"] = SortDirection.Descending;
                    ViewState["z_sortexpresion"] = "見積コード";
                    sort_expression = "見積コード";
                    sort_direction = "DESC";
                    this.BindGrid();

                    int endRowIndex = GV_Mitumori.Rows.Count;
                    lblHyojikensuu.Text = "1-" + endRowIndex + "/" ;
                    LB_Total.Text = totalDataCount.ToString();
                }
                else
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
                Session.Timeout = 480;
                updHyojikensuu1.Update();
                updMitsumoriGrid.Update();
            }
            else
            {
                Response.Redirect("JC01Login.aspx");

            }
        }
        #endregion

        #region CreateMitumoriTableColomn
        private DataTable CreateMitumoriTableColomn()
        {
            DataTable dt_Mitumori = new DataTable();
            dt_Mitumori.Columns.Add("見積コード");
            dt_Mitumori.Columns.Add("見積名");
            dt_Mitumori.Columns.Add("自社担当者");  //20220502 Added by phoo
            dt_Mitumori.Columns.Add("作成者");
            dt_Mitumori.Columns.Add("見積日");
            dt_Mitumori.Columns.Add("合計金額");
            dt_Mitumori.Columns.Add("見積状態");
            dt_Mitumori.Columns.Add("金額粗利");
            dt_Mitumori.Columns.Add("メモ"); //20220502 Added by phoo
            dt_Mitumori.Columns.Add("備考");//20220502 Added by phoo
            dt_Mitumori.Columns.Add("得意先");  //20220502 Added by phoo
            dt_Mitumori.Columns.Add("得意先担当者"); //20220502 Added by phoo
            dt_Mitumori.Columns.Add("画像");
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
        protected void BindGrid()
        {
            DDL_Jyouken.Items.Clear();

            String qrpg = "select ";
                   qrpg += " ifnull(m.cMITUMORI, '') as 見積コード ";
            String qr = "select "
                     + " ifnull(m.cMITUMORI, '') as 見積コード,"
                     + " ifnull(m.sMITUMORI, '') as 見積名,"
                     + " ifnull(mjt.sTANTOUSHA, '') as 営業担当,"
                     + " ifnull(m.cEIGYOTANTOSYA, '') as cEIGYOTANTOSYA,"  //20220107 MiMi Added
                     + "	ifnull(mt.sTANTOUSHA, '') as 作成者,"
                     + " date_format(m.dMITUMORISAKUSEI, '%y/%m/%d') as 見積日,"
                     + " ifnull(m.nKINGAKU, 0) as 合計金額,"
                     + " case IFNULL(m.cJYOTAI_MITUMORI, '')"
                     + " when '00' then '失注' when '01' then '見積提出済' when '02' then '受注' when '03' then '完了'  when '04' then '見積作成中' when '05' then 'キャンセル' when '06' then '売上済み' else '' end as 見積状態,"
                     + " ifnull(CC.nMITUMORIARARI, 0) 金額粗利,"
                     + " ifnull(m.sMEMO, '') as 社内メモ,"
                     + " ifnull(m.sBIKOU, '') as 見積書備考,"
                     + " ifnull(m.sTOKUISAKI, '') as 得意先名,"
                     + " ifnull(m.sTOKUISAKI_TAN, '') as 得意先担当,"
                     + "'' as 画像,"
                     + " '' as file64string ,mf.cFILE, mf.sFILE, mf.sPATH_SERVER_SOURCE "
                     + " from r_mitumori m"
                     + " LEFT JOIN (SELECT b.cMITUMORI as cmitu, b.cBUKKEN as cBUKKEN, sum(c.nMITUMORIARARI) nMITUMORIARARI from R_BUKKEN as r_bukken"
                     + " LEFT JOIN r_bu_mitsu b ON b.cBUKKEN = r_bukken.cBUKKEN LEFT JOIN m_file mfb ON mfb.cFILE = r_bukken.cFILE"
                     + " LEFT JOIN R_MITUMORI c ON b.cMITUMORI = c.cMITUMORI GROUP BY c.cMITUMORI) CC ON m.cMITUMORI = CC.cMITU"
                     + " LEFT JOIN m_mitsu_file mmf on mmf.cMITUMORI = m.cMITUMORI  and mmf.fVISABLE = 1 "
                     + " LEFT JOIN m_file mf on mf.cFILE = mmf.cFILE "

                     + " left join m_j_tantousha mjt ON m.cEIGYOTANTOSYA = mjt.cTANTOUSHA  ";

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
            qrpg += " group by m.cMITUMORI order by m.cMITUMORI DESC ;";
            qr += " order by "+sort_expression+" "+sort_direction+" LIMIT "+ Convert.ToInt16(DDL_Hyojikensuu.SelectedValue) + " OFFSET "+rowindex+";";

            String id = Request.QueryString["id"];
            DataTable dt1 = JC01Login_Class.Get_DB(id);
            String db = dt1.Rows[0]["db"].ToString();
            String mail = dt1.Rows[0]["mail"].ToString();
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
            ViewState["dt_Mitumori"] = dt;
            ViewState["dtpg_Mitumori"] = dtpg;
            MitumoriKoumokuSort();
            //updMitsumoriGrid.Update();
        }
       
        #endregion

        #region GetMitumoriColumn()
        private void GetMitumoriColumn()
        {
            String id = Request.QueryString["id"];
            DataTable dt = JC01Login_Class.Get_DB(id);
            String db = dt.Rows[0]["db"].ToString();
            String mail = dt.Rows[0]["mail"].ToString();
            ConstantVal.DB_NAME = db;

            JC07Home_Class JC07Home = new JC07Home_Class();
            JC07Home.loginId = mail;
            JC07Home.cListId = "2";
            dt_MitsumoriKomoku = new DataTable();
            dt_MitsumoriKomoku = JC07Home.KomokuSetting();
        }
        #endregion

        #region MitumoriKoumokuSort()
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

                //GridView1.PageSize = GV_Mitumori.PageSize;
                //GridView1.PageIndex = GV_Mitumori.PageIndex;

                GetMitumoriColumn();
                int col = 1;
                for (int i = 0; i < dt_MitsumoriKomoku.Rows.Count; i++)
                {
                    if (col > 0)
                    {
                        if (dt_MitsumoriKomoku.Rows[i]["sHYOUJI"].ToString() == "見積コード")
                        {
                            var cMitumori_column = GV_Mitumori.Columns[1];
                            GV_Mitumori_Original.Columns.Insert(col, cMitumori_column);
                            linkMutsucol = i.ToString();
                        }
                        else if (dt_MitsumoriKomoku.Rows[i]["sHYOUJI"].ToString() == "見積名")
                        {
                            var sMitumori_column = GV_Mitumori.Columns[2];
                            GV_Mitumori_Original.Columns.Insert(col, sMitumori_column);
                        }
                        else if (dt_MitsumoriKomoku.Rows[i]["sHYOUJI"].ToString() == "メモ")//20220502 Added by phoo
                        {
                            var shanai_column = GV_Mitumori.Columns[9];
                            GV_Mitumori_Original.Columns.Insert(col, shanai_column);
                        }
                        else if (dt_MitsumoriKomoku.Rows[i]["sHYOUJI"].ToString() == "備考") //20220502 Added by phoo
                        {
                            var biko_column = GV_Mitumori.Columns[10];
                            GV_Mitumori_Original.Columns.Insert(col, biko_column);
                        }
                        else if (dt_MitsumoriKomoku.Rows[i]["sHYOUJI"].ToString() == "得意先") //20220502 Added by phoo
                        {
                            var tokuisaki_column = GV_Mitumori.Columns[11];
                            GV_Mitumori_Original.Columns.Insert(col, tokuisaki_column);
                        }
                        else if (dt_MitsumoriKomoku.Rows[i]["sHYOUJI"].ToString() == "得意先担当者") //20220502 Added by phoo
                        {
                            var tokuisakiTantou_column = GV_Mitumori.Columns[12];
                            GV_Mitumori_Original.Columns.Insert(col, tokuisakiTantou_column);
                        }
                        else if (dt_MitsumoriKomoku.Rows[i]["sHYOUJI"].ToString() == "見積日")
                        {
                            var mitumoribi_column = GV_Mitumori.Columns[5];
                            GV_Mitumori_Original.Columns.Insert(col, mitumoribi_column);
                        }
                        else if (dt_MitsumoriKomoku.Rows[i]["sHYOUJI"].ToString() == "自社担当者") //20220502 Added by phoo
                        {
                            var eigyoTantou_column = GV_Mitumori.Columns[3];
                            GV_Mitumori_Original.Columns.Insert(col, eigyoTantou_column);
                        }
                        else if (dt_MitsumoriKomoku.Rows[i]["sHYOUJI"].ToString() == "合計金額")
                        {
                            var gokeiKingaku_column = GV_Mitumori.Columns[6];
                            GV_Mitumori_Original.Columns.Insert(col, gokeiKingaku_column);
                        }
                        else if (dt_MitsumoriKomoku.Rows[i]["sHYOUJI"].ToString() == "金額粗利")//20220502 Added by phoo
                        {
                            var Arai_column = GV_Mitumori.Columns[8];
                            GV_Mitumori_Original.Columns.Insert(col, Arai_column);
                        }
                        else if (dt_MitsumoriKomoku.Rows[i]["sHYOUJI"].ToString() == "作成者")
                        {
                            var sakuseisya_column = GV_Mitumori.Columns[4];
                            GV_Mitumori_Original.Columns.Insert(col, sakuseisya_column);
                        }
                        else if (dt_MitsumoriKomoku.Rows[i]["sHYOUJI"].ToString() == "見積状態")
                        {
                            var joutai_column = GV_Mitumori.Columns[7];
                            GV_Mitumori_Original.Columns.Insert(col, joutai_column);
                        }
                        else if (dt_MitsumoriKomoku.Rows[i]["sHYOUJI"].ToString() == "画像")
                        {
                            var sFile_column = GV_Mitumori.Columns[13];
                            GV_Mitumori_Original.Columns.Insert(col, sFile_column);
                            imgcol = i.ToString();
                        }
                    }
                    col++;
                }

                int kensuu = Convert.ToInt32(DDL_Hyojikensuu.SelectedValue);
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["sFile"].ToString() != "")
                    {
                        string file = dr["sFile"].ToString();
                        string path = dr["sPATH_SERVER_SOURCE"].ToString();
                        //string urlstring = SetPhotoRoot(path, file);
                        if (file != "")
                        {

                            dr["file64string"] = "../Img/gazou.png";// ;urlstring

                        }
                        else
                        {
                            dr["file64string"] = "../Img/imgerr.png";
                        }

                    }
                    else
                    {
                        dr["file64string"] = "../Img/imgerr.png";
                    }
                }
                //for (int i = rowindex; i < dt.Rows.Count; i++)
                //{
                //    if (i < rowindex + kensuu)
                //    {
                //        if (dt.Rows[i]["sFile"].ToString() != "")
                //        {
                //            string file = dt.Rows[i]["sFile"].ToString();
                //            string path = dt.Rows[i]["sPATH_SERVER_SOURCE"].ToString();
                //            //string urlstring = SetPhotoRoot(path, file);
                //            //if (urlstring != "")
                //            //{
                //            //    dt.Rows[i]["file64string"] = urlstring;
                //            //}
                //            if (file != "")
                //            {
                //                dt.Rows[i]["file64string"] = "../Img/gazou.png";
                //            }
                //            else
                //            {
                //                dt.Rows[i]["file64string"] = "../Img/imgerr.png";
                //            }

                //        }
                //        else
                //        {
                //            dt.Rows[i]["file64string"] = "../Img/imgerr.png";
                //        }
                //    }
                //    else
                //    {
                //        break;
                //    }
                //}
                GV_Mitumori_Original.DataSource = dt;
                GV_Mitumori_Original.DataBind();

               // updMitsumoriGrid.Update();
                //JoukenReload();
                //updMitsumoriGrid.Update();
            }
        }
        #endregion

        #region DDL_Hyojikensuu_SelectedIndexChanged()
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
            lblHyojikensuu.Text = "1-" + endRowIndex + "/" ;
            LB_Total.Text = totalDataCount.ToString();

            updHyojikensuu1.Update();
            updMitsumoriGrid.Update();
        }
        #endregion

        #region btnHyoujiSetting
        protected void btnHyoujiSetting_Click(object sender, EventArgs e)
        {
            SessionUtility.SetSession("HOME", "Master");
            Session["HyoujiID"] = "mitsumori";
            Session["HyoujiSettingID"] = "mitsumori";
            ifpnlHyoujiSetPopUp.Attributes.Add("class", "HyoujiSettingIframe mitsumoriiframeStyle");


            String id = Request.QueryString["id"];
            ifpnlHyoujiSetPopUp.Src = "JC08HyoujiSetting.aspx?id=" + id;
            mpeHyoujiSetPopUp.Show();
            updHyoujiSet.Update();
           // updMitsumoriGrid.Update();
        }

        protected void btnHyoujiSettingClose_Click(object sender, EventArgs e)
        {
            ifpnlHyoujiSetPopUp.Src = "";
            mpeHyoujiSetPopUp.Hide();
            updHyoujiSet.Update();
            Session["HyoujiSettingID"] = null;
            updhyojipopup.Update();
            //updMitsumoriGrid.Update();
        }

        protected void btnSaveHyoujiClose_Click(object sender, EventArgs e)
        {
            ifpnlHyoujiSetPopUp.Src = "";
            mpeHyoujiSetPopUp.Hide();
            updHyoujiSet.Update();
            if (Session["HyoujiSettingID"] != null)
            {
                if (Session["HyoujiSettingID"].ToString() == "mitsumori")
                {
                    GV_Mitumori_Original.DataSource = null;
                    GV_Mitumori_Original.DataBind();
                    MitumoriKoumokuSort();
                    updMitsumoriGrid.Update();
                }
            }
            Session["HyoujiSettingID"] = null;
        }
        #endregion

        #region btnSearch 見積データ検索
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
            if (lbldMitumoriS.Text != "")
            {
                if (Session["M_startdate"] == null)
                {
                    Session["M_startdate"] = lbldMitumoriS.Text;
                }
            }
            if (lbldMitumoriE.Text != "")
            {
                if (Session["M_enddate"] == null)
                {
                    Session["M_enddate"] = lbldMitumoriE.Text;
                }
            }
            if (lblJuchuuStart.Text != "")
            {
                if (Session["J_startdate"] == null)
                {
                    Session["J_startdate"] = lblJuchuuStart.Text;
                }
            }
            if (lblJuchuuEnd.Text != "")
            {
                if (Session["J_enddate"] == null)
                {
                    Session["J_enddate"] = lblJuchuuEnd.Text;
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
            lblHyojikensuu.Text = "1-" + endRowIndex + "/" ;
            LB_Total.Text = totalDataCount.ToString();
            updMitsumoriGrid.Update();
        }
        #endregion

        #region クリアボタン
        protected void btnClear_Click(object sender, EventArgs e)
        {
            TB_MitsuListCode.Text = "";
            txtsMitumori.Text = "";
            txtTokuisaki.Text = "";
            txtTokuisakiTantou.Text = "";
            txtTantousha.Text = "";
            MitumoriStartCross();
            MitumoriEndCross();
            JuchuuStartCross();
            JuchuuEndCross();
            UriageYoteiSCross();
            UriageYoteiECross();
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
            //DDL_Hyojikensuu.SelectedIndex = 0;
            //GV_Mitumori.PageIndex = 0;
            //GV_Mitumori.PageSize = kensuu;
            //this.BindGrid();
            //int endRowIndex = GV_Mitumori.Rows.Count;
            //lblHyojikensuu.Text = "1-" + endRowIndex + "/" + totalDataCount;

        }
        #endregion

        #region tantousha
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
            SessionUtility.SetSession("HOME", "Master");
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

        #region popupclose
        protected void btnClose_Click(object sender, EventArgs e)
        {
            ifSentakuPopup.Src = "";
            mpeSentakuPopup.Hide();
            updSentakuPopup.Update();
        }
        #endregion

        #region 見積検索ポップアップを閉じる
        protected void btnHeaderCross_Click(object sender, EventArgs e)
        {
            Session["cMitumori"] = null;
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "parentButtonClick('btn_CloseMitumoriSearch','" + hdnHome.Value + "');", true);
        }
        #endregion

        #region グリッドの見積コードはをクリックする「見積登録＆明細画面へ」
        protected void LK_cMitumori_Click(object sender, EventArgs e)
        {
            LinkButton lk = sender as LinkButton;
            Session["cMitumori"] = lk.Text;
            if (Session["cMitumori"] != null)
            {
                if (!String.IsNullOrEmpty(Request.QueryString["id"]))
                {
                    String id = Request.QueryString["id"];
                    Response.Redirect("JC10MitsumoriTouroku.aspx?id=" + id);
                }
                else
                {
                    Response.Redirect("JC01Login.aspx");
                }
            }
            else
            {
                Response.Redirect("JC01Login.aspx");
            }
            //ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "parentButtonClick('btn_CloseMitumoriSearch','" + hdnHome.Value + "');", true);

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
                    pn.CssClass = "JC31JokenDiv";
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
                    jisyaLabel.CssClass = "JC30JokenLabel";

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
                    btn_Cross.CssClass = "JC30JokenCross";
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
                    MitumoriStartCross();
                    Session["M_startdate"] = null;
                }
                else if (removeItem.Value.Equals("見積日End") && removeItem.Text.Equals(Session["M_enddate"].ToString()))
                {
                    MitumoriEndCross();
                    Session["M_enddate"] = null;
                }
            }
            else if (label.Equals("受注日"))
            {
                if (removeItem.Value.Equals("受注日Start") && removeItem.Text.Equals(Session["J_startdate"].ToString()))
                {
                    JuchuuStartCross();
                    Session["J_startdate"] = null;
                }
                else if (removeItem.Value.Equals("受注日End") && removeItem.Text.Equals(Session["J_enddate"].ToString()))
                {
                    JuchuuEndCross();
                    Session["J_enddate"] = null;
                }
            }
            else if (label.Equals("売上予定日"))
            {
                if (removeItem.Value.Equals("売上予定日Start") && removeItem.Text.Equals(Session["U_startdate"].ToString()))
                {
                    UriageYoteiSCross();
                    Session["U_startdate"] = null;
                }
                else if (removeItem.Value.Equals("売上予定日End") && removeItem.Text.Equals(Session["U_enddate"].ToString()))
                {
                    UriageYoteiECross();
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
            int kensuu = Convert.ToInt16(DDL_Hyojikensuu.SelectedValue);
            GV_Mitumori.PageIndex = 0;
            GV_Mitumori.PageSize = kensuu;
            GV_Mitumori_Original.PageIndex = 0;
            GV_Mitumori_Original.PageSize = kensuu;
            this.BindGrid();
            JoukenReload();
            int endRowIndex = GV_Mitumori.Rows.Count;
            lblHyojikensuu.Text = "1-" + endRowIndex + "/" ;
            LB_Total.Text = totalDataCount.ToString();
            updHyojikensuu.Update();
            updMitsumoriGrid.Update();
        }
        #endregion

        #region btnjishatantou
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

        #region btndatechange
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

        #region GV_Mitumori_PageIndexChanging
        protected void GV_Mitumori_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                GridView gv = sender as GridView;
                gv.PageIndex = e.NewPageIndex;
               // GV_Mitumori.PageIndex = e.NewPageIndex;
                int kensuu = Convert.ToInt16(DDL_Hyojikensuu.SelectedValue);
                GridView1.PageSize = kensuu;
                int startRowIndex = (kensuu * e.NewPageIndex) + 1;
                rowindex = (kensuu * e.NewPageIndex);
                GV_MiRowindex.Text = rowindex.ToString();
               // GV_Mitumori.PageIndex = e.NewPageIndex;
                GridView1.PageIndex = e.NewPageIndex;
                BindGrid();
                int endRowIndex = (kensuu * e.NewPageIndex) + GV_Mitumori.Rows.Count;
                lblHyojikensuu.Text = startRowIndex + "-" + endRowIndex + "/" ;
                LB_Total.Text = totalDataCount.ToString();


                //GridViewRowCount.Text = endRowIndex.ToString();

                updHyojikensuu1.Update();
               // updMitsumoriGrid.Update();
            }
            catch (Exception ex)
            {

            }
        }
        #endregion

        #region GV_Mitumori_Original_RowDataBound
        protected void GV_Mitumori_Original_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (ViewState["dt_Mitumori"] != null)
                {
                    e.Row.Attributes.Add("onmouseover", "this.style.cursor='hand';this.style.backgroundColor='rgb(235, 235, 245)'");
                    e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='white'");
                    DataTable dt_mitsumori = ViewState["dt_Mitumori"] as DataTable;
                    //string mitsumoriId = (e.Row.DataItem as DataRowView).Row["見積コード"].ToString();
                    DataColumnCollection columns = dt_mitsumori.Columns;
                    if (dt_mitsumori.Columns.Count > 0)
                    {

                        //リンク項目
                        if (linkMutsucol != "")
                        {
                            int linkMitsucolIdx = int.Parse(linkMutsucol);

                            LinkButton lnkbtn = new LinkButton();
                            lnkbtn.ID = "lnkView";
                            //lnkbtn.Text = mitsumoriId;
                            lnkbtn.CommandName = "Select";
                            e.Row.Cells[linkMitsucolIdx].Controls.Add(lnkbtn);
                        }
                        //foreach (DataColumn dr in dt_mitsumori.Columns)
                        //{
                        //string HeaderName = dr.ColumnName.ToString();
                        //if (imgcol != "")
                        //{
                        //    if (HeaderName == "画像")
                        //    {
                        //        int imgcolIdx = int.Parse(imgcol)+1;
                        //        DataRow[] rowResult = dt_Mitsuimg.Select("cMITUMORI='" + mitsumoriId + "'");
                        //        foreach (DataRow row in rowResult)
                        //        {
                        //            filepath = row["sPATH_SERVER_SOURCE"].ToString();
                        //        }
                        //        if (rowResult.Length > 0)
                        //        {
                        //            Image img = (Image)e.Row.FindControl("Image1"); 
                        //            img.ImageUrl = "../Img/gazou.png";
                        //            img.Width = 24;
                        //            img.Height = 24;
                        //            e.Row.Cells[imgcolIdx].Controls.Add(img);

                        //            Image popupimg = (Image)e.Row.FindControl("popupimg");
                        //            popupimg.Width = 100;
                        //            popupimg.Height = 100;
                        //            string fileName = Path.GetFileName(filepath);
                        //            SetPhotoRoot(filepath, fileName, popupimg);
                        //        }

                        //    }
                        //}
                        //} 
                    }
                }
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

        #region SetPhotoRoot
        private string SetPhotoRoot(string root, string nam)
        {
            string imgurl = "";
            string filePath = root;
            //string filePath_f = "";
            //string name = nam;
            //// name = System.IO.Path.GetFileNameWithoutExtension(name);
            //string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            //filePath = folderPath + "\\DEMO20" + "\\ICONTEMP\\" + "MitumoriPrint";
            //int i = root.IndexOf("\\営業用");
            //if (i == -1)
            //{
            //    i = root.IndexOf("\\指示書");
            //    filePath_f = root.Substring(0, i) + "MitumoriPrint";
            //}
            //else
            //{
            //    int j = root.IndexOf("\\指示書");
            //    if (j != -1 && j < i)
            //    {
            //        filePath_f = root.Substring(0, j) + "MitumoriPrint";
            //    }
            //    else
            //    {
            //        filePath_f = root.Substring(0, i) + "MitumoriPrint";
            //    }
            //}

            //string filePath_temp = filePath_f + "\\Temp.jpg";
            //filePath_f += "\\" + name;

            //if (Directory.Exists(filePath) == false)
            //{
            //    Directory.CreateDirectory(filePath);
            //}
            ////if (img.ImageUrl != "")
            ////{
            ////    img.ImageUrl = "";
            ////}

            //filePath += "\\" + name;
            //if (File.Exists(filePath) == false)
            //{
            //    try
            //    {
            //        File.Copy(filePath_f, filePath);
            //    }
            //    catch { }
            //}
            //else
            //{
            //    try
            //    {
            //        File.Delete(filePath);
            //        File.Copy(filePath_f, filePath);
            //    }
            //    catch { }
            //}
            //if (filePath == null)
            //{
            //    //if (img.ImageUrl != null) img.ImageUrl = "";
            //    //img.ImageUrl = System.Environment.CurrentDirectory + @"\res\Error.jpg";
            //    imgurl = "";
            //}
            //else
            //{

            try
            {
                byte[] byteData = System.IO.File.ReadAllBytes(filePath);

                //now convert byte[] to Base64
                string base64String = Convert.ToBase64String(byteData);
                imgurl = string.Format("data:image/png;base64,{0}", base64String);

            }
            catch { }
            //}

            return imgurl;
        }

        #endregion

        #region mitsumorisakusei
        protected void BT_mitsumorisakusei_Click(object sender, EventArgs e)
        {
            Session["cBukken"] = null;  //20220209 MiMi Added
            Session["cMitumori"] = null;
            if (!String.IsNullOrEmpty(Request.QueryString["id"]))
            {
                String id = Request.QueryString["id"];
                Response.Redirect("JC10MitsumoriTouroku.aspx?id=" + id);
            }
            else
            {
                Response.Redirect("JC01Login.aspx");
            }
        }
        #endregion

        #region gvrowcommand
        protected void GV_Mitsumori_Original_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Session["UriagePDF"] = "false"; // 20220208 added by YG
            Session["uriageCode"] = "false"; // 20220208 added by YG

            if (e.CommandName == "Update")
            {
                if (!String.IsNullOrEmpty(Request.QueryString["id"]))
                {
                    String id = Request.QueryString["id"];
                    DataTable dt = JC01Login_Class.Get_DB(id);
                    String db = dt.Rows[0]["db"].ToString();
                    String mail = dt.Rows[0]["mail"].ToString();

                    GridViewRow row = (GridViewRow)(((Control)e.CommandSource).NamingContainer);
                    int index = row.RowIndex;
                    string mitsumoriId = GV_Mitumori_Original.DataKeys[index].Value.ToString();
                    JC07Home_Class JC07Home = new JC07Home_Class();
                    JC07Home.loginId = mail;
                    JC07Home.mitsumoriId = mitsumoriId;
                    ConstantVal.DB_NAME = db;
                    string BukkenId = JC07Home.FindBukkenID();
                    ConstantVal.DB_NAME = db;
                    string cmitu_code = JC07Home.cMitumori();
                    this.BindGrid();
                    Session["cMitumori"] = mitsumoriId;

                    Response.Redirect("JC10MitsumoriTouroku.aspx?id=" + id);
                }
                else
                {
                    Response.Redirect("JC01Login.aspx");
                }
            }
            if (e.CommandName == "Copy")
            {
                GridViewRow row = (GridViewRow)(((Control)e.CommandSource).NamingContainer);
                int index = row.RowIndex;
                string mitsumoriId = GV_Mitumori_Original.DataKeys[index].Value.ToString();

                //物件グリッドデータ
                JC07Home_Class JC07Home = new JC07Home_Class();
                String id = Request.QueryString["id"];
                DataTable dt = JC01Login_Class.Get_DB(id);
                String db = dt.Rows[0]["db"].ToString();
                String mail = dt.Rows[0]["mail"].ToString();
                JC07Home.loginId = mail;
                JC07Home.mitsumoriId = mitsumoriId;
                ConstantVal.DB_NAME = db; // 20220323 nan 
                string BukkenId = JC07Home.FindBukkenID();
                MitsumoriCopy(BukkenId, mitsumoriId);
                ConstantVal.DB_NAME = db; // 20220323 nan 
                string cmitu_code = JC07Home.cMitumori();
                fcol = "Copy";
                this.BindGrid();
                Session["cMitumori"] = cmitu_code;
                if (!String.IsNullOrEmpty(Request.QueryString["id"]))
                {
                    //String id = Request.QueryString["id"];
                    Response.Redirect("JC10MitsumoriTouroku.aspx?id=" + id);
                }
                else
                {
                    Response.Redirect("JC01Login.aspx");
                }
            }
            else if (e.CommandName == "PDF")  //20220107 MiMi Added
            {
                GridViewRow row = (GridViewRow)(((Control)e.CommandSource).NamingContainer);
                int index = row.RowIndex;
                string mitsumoriId = GV_Mitumori_Original.DataKeys[index].Value.ToString();
                string ctantousya = (row.FindControl("lbl_cEigyoTantou") as Label).Text;
                string sMitumori= (row.FindControl("lblsMitumori_Grid") as Label).Text;

                //物件グリッドデータ
                JC07Home_Class JC07Home = new JC07Home_Class();

                String id = Request.QueryString["id"];
                DataTable dt = JC01Login_Class.Get_DB(id);
                String db = dt.Rows[0]["db"].ToString();
                String mail = dt.Rows[0]["mail"].ToString();

                JC07Home.loginId = mail;
                JC07Home.mitsumoriId = mitsumoriId;
                ConstantVal.DB_NAME = db; // 20220323 nan 
                string BukkenId = JC07Home.FindBukkenID();
                HF_cMitumori.Value = mitsumoriId;
                HF_cBukken.Value = BukkenId;
                HF_ctantousya.Value = ctantousya;
                HF_sMitumori.Value = sMitumori;
                upd_Hidden.Update();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ClickPDFButton",
                    "ClickPDFButton();", true);
            }
        }
        #endregion

        #region mitsumoricopy()
        protected void MitsumoriCopy(string bukkenId, string mitsumoriId)
        {
            if (!String.IsNullOrEmpty(Request.QueryString["id"]))
            {
                JC07Home_Class JC07Home = new JC07Home_Class();

                String id = Request.QueryString["id"];
                DataTable dt = JC01Login_Class.Get_DB(id);
                String db = dt.Rows[0]["db"].ToString();
                String mail = dt.Rows[0]["mail"].ToString();

                JC_ClientConnecction_Class jc = new JC_ClientConnecction_Class();
                jc.loginId = mail;
                jc.GetConnection();
                DataTable dt_loginuser = jc.GetLoginUserCodeFromClientDB();
                String loginId = dt_loginuser.Rows[0]["code"].ToString();

                JC07Home.loginId = mail;
                JC07Home.bukkenId = bukkenId;
                JC07Home.mitsumoriId = mitsumoriId;
                ConstantVal.DB_NAME = db; // 20220323 nan 
                JC07Home.MitsumoriCopy(loginId);
            }
            else
            {
                Response.Redirect("JC01Login.aspx");
            }

        }
        #endregion

        #region rowdeleting
        protected void GV_Mitsumori_Original_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string mitsumoriId = GV_Mitumori_Original.DataKeys[e.RowIndex].Value.ToString();
            JC07Home_Class JC07Home = new JC07Home_Class();

            String id = Request.QueryString["id"];
            DataTable dt = JC01Login_Class.Get_DB(id);
            String db = dt.Rows[0]["db"].ToString();
            String mail = dt.Rows[0]["mail"].ToString();

            JC07Home.loginId = mail;
            JC07Home.mitsumoriId = mitsumoriId;
            ConstantVal.DB_NAME = db; // 20220323 nan 
            string BukkenId = JC07Home.FindBukkenID();
            MitsumoriDelete(BukkenId, mitsumoriId);
            BindGrid();
            LB_Total.Text = totalDataCount.ToString();
            //int kensuu = Convert.ToInt16(DDL_Hyojikensuu.SelectedValue);
            ////GV_Mitumori.PageIndex = 0;
            ////GV_Mitumori.PageSize = kensuu;
            //GV_Mitumori_Original.PageIndex = 0;
            //GV_Mitumori_Original.PageSize = kensuu;
            //GridView1.PageIndex = 0;
            //GridView1.PageSize = kensuu;
            //this.BindGrid();
            //int endRowIndex = GridView1.Rows.Count;
            //lblHyojikensuu.Text = "1-" + endRowIndex + "/" + totalDataCount;
            updHyojikensuu1.Update();
        }
        #endregion

        #region mitsumoridelete
        protected void MitsumoriDelete(string BukkenId, string mitsumoriId)
        {
            if (!String.IsNullOrEmpty(Request.QueryString["id"]))
            {
                JC07Home_Class JC07Home = new JC07Home_Class();

                String id = Request.QueryString["id"];
                DataTable dt = JC01Login_Class.Get_DB(id);
                String db = dt.Rows[0]["db"].ToString();
                String mail = dt.Rows[0]["mail"].ToString();

                JC07Home.loginId = mail;
                JC07Home.bukkenId = BukkenId;
                JC07Home.mitsumoriId = mitsumoriId;
                ConstantVal.DB_NAME = db; // 20220323 nan 
                JC07Home.MitsumoriDelete();
            }
            else
            {
                Response.Redirect("JC01Login.aspx");
            }

        }

        //20220321 Add Start
        protected void btnDeleteMitu_Click(object sender, EventArgs e)
        {
            string mituvaluehdn = hdnmituID.Value;
            //string mitsumoriId = GV_Mitumori_Original.DataKeys[index].Value.ToString();

            String id = Request.QueryString["id"];
            DataTable dt = JC01Login_Class.Get_DB(id);
            String db = dt.Rows[0]["db"].ToString();
            String mail = dt.Rows[0]["mail"].ToString();
            ConstantVal.DB_NAME = db;

            JC07Home_Class JC07Home = new JC07Home_Class();
            JC07Home.loginId = mail;
            JC07Home.mitsumoriId = mituvaluehdn;
            string BukkenId = JC07Home.FindBukkenID();

            MitsumoriDelete(BukkenId, mituvaluehdn);
            BindGrid();
            LB_Total.Text = totalDataCount.ToString();
            updHyojikensuu1.Update();
        }

        protected void lnkbtnMitsuDelete_Click(object sender, EventArgs e)
        {
            GridViewRow gvrow = (sender as LinkButton).NamingContainer as GridViewRow;
            string mitsumoriId = (gvrow.FindControl("LK_cMitumori") as LinkButton).Text;
            hdnmituID.Value = mitsumoriId;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "DeleteConfirmMessage",
                    "DeleteConfirmBox('削除してもよろしいでしょうか','" + btnDeleteMitu.ClientID + "');", true);
            updbody.Update();
        }
        // 20220321 Add End
        #endregion

        protected void txtMitumoriStartDate_TextChanged(object sender, EventArgs e)
        {
            if (TextUtility.IsIncludeZenkaku(lbldMitumoriS.Text))
            {
                lbldMitumoriS.Text = "";
                updMitumoriStartDate.Update();
            }
        }
        
        #region BT_MitsumoriSyosai_Click()
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

        #region "日付カレンダーポップアップの【X】ボタンクリック処理"
        protected void btnCalendarClose_Click(object sender, EventArgs e)
        {
            // 【日付サブ画面】を閉じる
            CloseSentakuSub();
            // フォーカスする
            CalendarFoucs();
        }
        #endregion

        #region "日付サブ画面を閉じる時のフォーカス処理"
        protected void CalendarFoucs()
        {
            string strBtnID = (string)ViewState["DATETIME"];
            if (strBtnID == btnMitumoriStart.ID)
            {
                if (btnMitumoriStart.Style["display"] != "none")
                {
                    btnMitumoriStart.Focus();
                }

            }
            else if (strBtnID == btnMitumoriEnd.ID)
            {
                if (btnMitumoriEnd.Style["display"] != "none")
                {
                    btnMitumoriEnd.Focus();
                }

            }
            else if (strBtnID == btnJuchuuStart.ID)
            {
                if (btnJuchuuStart.Style["display"] != "none")
                {
                    btnJuchuuStart.Focus();
                }

            }
            else if (strBtnID == btnJuchuuEnd.ID)
            {
                if (btnJuchuuEnd.Style["display"] != "none")
                {
                    btnJuchuuEnd.Focus();
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

        #region "選択日付サブ画面を閉じる処理"
        protected void CloseSentakuSub()
        {
            ifdatePopup.Src = "";
            mpedatePopup.Hide();
            upddatePopup.Update();
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

                if (strBtnID == btnMitumoriStart.ID)
                {
                    MitumoriSDateDataBind(strCalendarDateTime, strBtnID);
                }
                if (strBtnID == btnMitumoriEnd.ID)
                {
                    MitumoriEDateDataBind(strCalendarDateTime, strBtnID);
                }
                else if (strBtnID == btnJuchuuStart.ID)
                {
                    JuuchuustartDateDataBind(strCalendarDateTime, strBtnID);
                }
                else if (strBtnID == btnJuchuuEnd.ID)
                {
                    JuuchuuendDateDataBind(strCalendarDateTime, strBtnID);
                }
                else if (strBtnID == btnUriageYoteiStartDate.ID)
                {
                    UriageYoteistartDateDataBind(strCalendarDateTime, strBtnID);
                }
                else if (strBtnID == btnUriageYoteiEndDate.ID)
                {
                    UriageYoteiendDateDataBind(strCalendarDateTime, strBtnID);
                }
            }

            CalendarFoucs();
        }
        #endregion

        #region "見積日データバインディング処理"
        protected void MitumoriSDateDataBind(string strCalendarDateTime, string strImgbtnID)
        {
            DateTime dtDeliveryDate = DateTime.Parse(strCalendarDateTime);
            lbldMitumoriS.Text = dtDeliveryDate.ToString("yyyy/MM/dd");
            btnMitumoriStart.Style["display"] = "none";
            divMitumoriSDate.Style["display"] = "block";
            updMitumoriStartDate.Update();

        }

        protected void MitumoriEDateDataBind(string strCalendarDateTime, string strImgbtnID)
        {
            DateTime dtDeliveryDate = DateTime.Parse(strCalendarDateTime);
            lbldMitumoriE.Text = dtDeliveryDate.ToString("yyyy/MM/dd");
            btnMitumoriEnd.Style["display"] = "none";
            divMitumoriEDate.Style["display"] = "block";
            UpdMitumoriEndDate.Update();
        }

        protected void JuuchuustartDateDataBind(string strCalendarDateTime, string strImgbtnID)
        {
            DateTime dtDeliveryDate = DateTime.Parse(strCalendarDateTime);
            lblJuchuuStart.Text = dtDeliveryDate.ToString("yyyy/MM/dd");
            btnJuchuuStart.Style["display"] = "none";
            divJuchuuStart.Style["display"] = "block";
            updJuchuuStartDate.Update();
        }

        protected void JuuchuuendDateDataBind(string strCalendarDateTime, string strImgbtnID)
        {
            DateTime dtDeliveryDate = DateTime.Parse(strCalendarDateTime);
            lblJuchuuEnd.Text = dtDeliveryDate.ToString("yyyy/MM/dd");
            btnJuchuuEnd.Style["display"] = "none";
            divJuchuuEnd.Style["display"] = "block";
            updJuchuuEndDate.Update();
        }

        protected void UriageYoteistartDateDataBind(string strCalendarDateTime, string strImgbtnID)
        {
            DateTime dtDeliveryDate = DateTime.Parse(strCalendarDateTime);
            lblUriageYoteiStartDate.Text = dtDeliveryDate.ToString("yyyy/MM/dd");
            btnUriageYoteiStartDate.Style["display"] = "none";
            divUriageYoteiStartDate.Style["display"] = "block";
            updUriageYoteiStartDate.Update();
        }

        protected void UriageYoteiendDateDataBind(string strCalendarDateTime, string strImgbtnID)
        {
            DateTime dtDeliveryDate = DateTime.Parse(strCalendarDateTime);
            lblUriageYoteiEndDate.Text = dtDeliveryDate.ToString("yyyy/MM/dd");
            btnUriageYoteiEndDate.Style["display"] = "none";
            divUriageYoteiEndDate.Style["display"] = "block";
            updUriageYoteiEndDate.Update();
        }
        #endregion

        #region 見積日
        protected void btnMitumoriStart_Click(object sender, EventArgs e)
        {
            SessionUtility.SetSession("HOME", "Master");
            String id = Request.QueryString["id"];
            ifdatePopup.Src = "JCHidukeSelect.aspx?id=" + id;
            mpedatePopup.Show();

            ViewState["DATETIME"] = btnMitumoriStart.ID;

            if (!String.IsNullOrEmpty(lbldMitumoriS.Text))
            {
                DateTime dt = DateTime.Parse(lbldMitumoriS.Text);
                Session["CALENDARDATE"] = dt.ToString("yyyy/MM/dd");
                Session["YEAR"] = dt.Year;
            }

            // 設定したラベルをクリック時、時間設定ポップアップ画面を表示する
            lbldMitumoriS.Attributes.Add("onClick", "BtnClick('MainContent_btnMitumoriStart')");
            upddatePopup.Update();
        }

        protected void btndMitumoriSCross_Click(object sender, EventArgs e)
        {
            MitumoriStartCross();
        }

        protected void btnMitumoriEnd_Click(object sender, EventArgs e)
        {
            SessionUtility.SetSession("HOME", "Master");
            String id = Request.QueryString["id"];
            ifdatePopup.Src = "JCHidukeSelect.aspx?id=" + id;
            mpedatePopup.Show();

            ViewState["DATETIME"] = btnMitumoriEnd.ID;

            if (!String.IsNullOrEmpty(lbldMitumoriE.Text))
            {
                DateTime dt = DateTime.Parse(lbldMitumoriE.Text);
                Session["CALENDARDATE"] = dt.ToString("yyyy/MM/dd");
                Session["YEAR"] = dt.Year;
            }

            // 設定したラベルをクリック時、時間設定ポップアップ画面を表示する
            lbldMitumoriE.Attributes.Add("onClick", "BtnClick('MainContent_btnMitumoriEnd')");
            upddatePopup.Update();
        }

        protected void btndMitumoriECross_Click(object sender, EventArgs e)
        {
            MitumoriEndCross();
        }

        public void MitumoriStartCross()
        {
            lbldMitumoriS.Text = "";
            btnMitumoriStart.Style["display"] = "block";
            divMitumoriSDate.Style["display"] = "none";
            updMitumoriStartDate.Update();
        }
        public void MitumoriEndCross()
        {
            lbldMitumoriE.Text = "";
            btnMitumoriEnd.Style["display"] = "block";
            divMitumoriEDate.Style["display"] = "none";
            UpdMitumoriEndDate.Update();
        }
        #endregion

        #region 受注日
        protected void btnJuchuuStart_Click(object sender, EventArgs e)
        {
            SessionUtility.SetSession("HOME", "Master");
            String id = Request.QueryString["id"];
            ifdatePopup.Src = "JCHidukeSelect.aspx?id=" + id;
            mpedatePopup.Show();

            ViewState["DATETIME"] = btnJuchuuStart.ID;

            if (!String.IsNullOrEmpty(lblJuchuuStart.Text))
            {
                DateTime dt = DateTime.Parse(lblJuchuuStart.Text);
                Session["CALENDARDATE"] = dt.ToString("yyyy/MM/dd");
                Session["YEAR"] = dt.Year;
            }

            // 設定したラベルをクリック時、時間設定ポップアップ画面を表示する
            lblJuchuuStart.Attributes.Add("onClick", "BtnClick('MainContent_btnJuchuuStart')");
            upddatePopup.Update();
        }

        protected void btnJuchuuSCross_Click(object sender, EventArgs e)
        {
            JuchuuStartCross();
        }

        protected void btnJuchuuEnd_Click(object sender, EventArgs e)
        {
            SessionUtility.SetSession("HOME", "Master");
            String id = Request.QueryString["id"];
            ifdatePopup.Src = "JCHidukeSelect.aspx?id=" + id;
            mpedatePopup.Show();

            ViewState["DATETIME"] = btnJuchuuEnd.ID;

            if (!String.IsNullOrEmpty(lblJuchuuEnd.Text))
            {
                DateTime dt = DateTime.Parse(lblJuchuuEnd.Text);
                Session["CALENDARDATE"] = dt.ToString("yyyy/MM/dd");
                Session["YEAR"] = dt.Year;
            }

            // 設定したラベルをクリック時、時間設定ポップアップ画面を表示する
            lblJuchuuEnd.Attributes.Add("onClick", "BtnClick('MainContent_btnJuchuuEnd')");
            upddatePopup.Update();
        }

        protected void btnJuchuuECross_Click(object sender, EventArgs e)
        {
            JuchuuEndCross();
        }

        public void JuchuuEndCross()
        {
            lblJuchuuEnd.Text = "";
            btnJuchuuEnd.Style["display"] = "block";
            divJuchuuEnd.Style["display"] = "none";
            updJuchuuEndDate.Update();
        }

        public void JuchuuStartCross()
        {
            lblJuchuuStart.Text = "";
            btnJuchuuStart.Style["display"] = "block";
            divJuchuuStart.Style["display"] = "none";
            updJuchuuStartDate.Update();
        }
        #endregion

        #region 売上予定日
        protected void btnUriageYoteiStartDate_Click(object sender, EventArgs e)
        {
            SessionUtility.SetSession("HOME", "Master");
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
            lblUriageYoteiStartDate.Attributes.Add("onClick", "BtnClick('MainContent_btnUriageYoteiStartDate')");
            upddatePopup.Update();
        }

        protected void btnUriageYoteiSCross_Click(object sender, EventArgs e)
        {
            UriageYoteiSCross();
        }

        protected void btnUriageYoteiEndDate_Click(object sender, EventArgs e)
        {
            SessionUtility.SetSession("HOME", "Master");
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
            lblUriageYoteiEndDate.Attributes.Add("onClick", "BtnClick('MainContent_btnUriageYoteiEndDate')");
            upddatePopup.Update();
        }

        protected void btnUriageYoteiECross_Click(object sender, EventArgs e)
        {
            UriageYoteiECross();
        }

        public void UriageYoteiSCross()
        {
            lblUriageYoteiStartDate.Text = "";
            btnUriageYoteiStartDate.Style["display"] = "block";
            divUriageYoteiStartDate.Style["display"] = "none";
            updUriageYoteiStartDate.Update();
        }

        public void UriageYoteiECross()
        {
            lblUriageYoteiEndDate.Text = "";
            btnUriageYoteiEndDate.Style["display"] = "block";
            divUriageYoteiEndDate.Style["display"] = "none";
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
            SessionUtility.SetSession("HOME", "Master");
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

        #region BT_MitumoriPDF_Click //20220107 MiMi Added
        protected void BT_MitumoriPDF_Click(object sender, EventArgs e)
        {
            PrintPDF();
        }
        #endregion

        #region setrogo  //20220107 MiMi Added
        private void setrogo()　　//ロゴ名
        {
            String id = Request.QueryString["id"];
            DataTable dt1 = JC01Login_Class.Get_DB(id);
            String db = dt1.Rows[0]["db"].ToString();
            String mail = dt1.Rows[0]["mail"].ToString();
            ConstantVal.DB_NAME = db;

            JC_ClientConnecction_Class jc = new JC_ClientConnecction_Class();
            jc.loginId = mail;
            MySqlConnection cn = jc.GetConnection();

            try
            {
                cn.Open();
            }
            catch
            {
                Response.Redirect("JC01Login.aspx");
            }
            ConstantVal.DB_NAME = db;
            JC_Mitumori_Class jm = new JC_Mitumori_Class();
            cKyoten = jm.GetKyoten(HF_cMitumori.Value);

            string sql = "";
            sql = "select ifnull(sIMAGETitle1,'') sIMAGETitle1,ifnull(sIMAGETitle2,'') sIMAGETitle2,ifnull(sIMAGETitle3,'') sIMAGETitle3,ifnull(sIMAGETitle4,'') sIMAGETitle4,ifnull(sIMAGETitle5,'') sIMAGETitle5";
            sql += " from m_j_info where cCO ='" + cKyoten + "'";
            MySqlCommand cmd = new MySqlCommand(sql, cn);
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            cn.Close();
            da.Dispose();
            if (dt.Rows.Count > 0)
            {
                DDL_Logo.Items.Clear();
                if (dt.Rows[0]["sIMAGETitle1"].ToString().TrimEnd() != "")
                {
                    DDL_Logo.Items.Add(dt.Rows[0]["sIMAGETitle1"].ToString());
                }
                if (dt.Rows[0]["sIMAGETitle2"].ToString().TrimEnd() != "")
                {
                    DDL_Logo.Items.Add(dt.Rows[0]["sIMAGETitle2"].ToString());
                }
                if (dt.Rows[0]["sIMAGETitle3"].ToString().TrimEnd() != "")
                {
                    DDL_Logo.Items.Add(dt.Rows[0]["sIMAGETitle3"].ToString());
                }
                if (dt.Rows[0]["sIMAGETitle4"].ToString().TrimEnd() != "")
                {
                    DDL_Logo.Items.Add(dt.Rows[0]["sIMAGETitle4"].ToString());
                }
                if (dt.Rows[0]["sIMAGETitle5"].ToString().TrimEnd() != "")
                {
                    DDL_Logo.Items.Add(dt.Rows[0]["sIMAGETitle5"].ToString());
                }
            }
        }
        #endregion

        #region PrintPDF //20220107 MiMi Added
        private void PrintPDF()
        {
            String cBukken = HF_cBukken.Value;
            String cMitumori = HF_cMitumori.Value;
            String cTantousya = HF_ctantousya.Value;
            if (!String.IsNullOrEmpty(Request.QueryString["id"]))
            {
                String id = Request.QueryString["id"];
                DataTable dt1 = JC01Login_Class.Get_DB(id);
                String db = dt1.Rows[0]["db"].ToString();
                String mail = dt1.Rows[0]["mail"].ToString();
                ConstantVal.DB_NAME = db;

                JC_ClientConnecction_Class jc = new JC_ClientConnecction_Class();
                jc.loginId = mail;
                DataTable dt_loginuser = jc.GetLoginUserCodeFromClientDB();
                String cLoginUser = dt_loginuser.Rows[0]["code"].ToString();

                #region 見積書印刷設定
                setrogo();//ロゴ名
                
                DataTable dt_InsatsuSetting = jc.ExecuteInsatsuSetting(cLoginUser);
                Boolean chk_midashi = false;
                Boolean chk_meisai = false;
                Boolean chk_syosai = false;
                String fZeikomi = "0";
                if (dt_InsatsuSetting.Rows.Count > 0)
                {
                    try
                    {
                        String logo = dt_InsatsuSetting.Rows[0]["fLogo"].ToString();
                        DDL_Logo.SelectedIndex = Convert.ToInt32(logo);
                    }
                    catch { }

                    fZeikomi = dt_InsatsuSetting.Rows[0]["fZei"].ToString();
                    if (dt_InsatsuSetting.Rows[0]["fMidashi"].ToString() == "1")
                    {
                        chk_midashi = true;
                    }

                    if (dt_InsatsuSetting.Rows[0]["fMeisai"].ToString() == "1")
                    {
                        chk_meisai = true;
                    }

                    if (dt_InsatsuSetting.Rows[0]["fSyosai"].ToString() == "1")
                    {
                        chk_syosai = true;
                    }
                }
                #endregion

                #region ロゴタイトル
                string flagrogo = "";
                string sqlkyoten = "";
                sqlkyoten = "select ifnull(sIMAGETitle1,'') sIMAGETitle1,ifnull(sIMAGETitle2,'') sIMAGETitle2,ifnull(sIMAGETitle3,'') sIMAGETitle3,ifnull(sIMAGETitle4,'') sIMAGETitle4,ifnull(sIMAGETitle5,'') sIMAGETitle5";
                sqlkyoten += ",ifnull(sBIKOUTitle1,'') sBIKOUTitle1,ifnull(sBIKOUTitle2,'') sBIKOUTitle2,ifnull(sBIKOUTitle3,'') sBIKOUTitle3,ifnull(sBIKOUTitle4,'') sBIKOUTitle4,ifnull(sBIKOUTitle5,'') sBIKOUTitle5";
                sqlkyoten += " from m_j_info where cCO ='" + cKyoten + "'";
                //JC_ClientConnecction_Class jc = new JC_ClientConnecction_Class();


                jc.loginId = mail;
                cn = jc.GetConnection();
                cn.Open();

                MySqlCommand cmd = new MySqlCommand(sqlkyoten, cn);
                cmd.CommandTimeout = 0;
                DataTable dt = new DataTable();
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["sIMAGETitle1"].ToString().TrimEnd() == DDL_Logo.SelectedItem.ToString().TrimEnd())
                    {
                        flagrogo = "1";
                    }
                    else if (dt.Rows[0]["sIMAGETitle2"].ToString().TrimEnd() == DDL_Logo.SelectedItem.ToString().TrimEnd())
                    {
                        flagrogo = "2";
                    }
                    else if (dt.Rows[0]["sIMAGETitle3"].ToString().TrimEnd() == DDL_Logo.SelectedItem.ToString().TrimEnd())
                    {
                        flagrogo = "3";
                    }
                    else if (dt.Rows[0]["sIMAGETitle4"].ToString().TrimEnd() == DDL_Logo.SelectedItem.ToString().TrimEnd())
                    {
                        flagrogo = "4";
                    }
                    else if (dt.Rows[0]["sIMAGETitle5"].ToString().TrimEnd() == DDL_Logo.SelectedItem.ToString().TrimEnd())
                    {
                        flagrogo = "5";
                    }
                }
                #endregion

                DateTime datenow = jc.GetCurrentDate();
                String fileName = HF_sMitumori.Value + "_" + datenow.ToString("yyyyMMdd");

                if (chk_syosai == true)
                {
                    fSYOUSAI = true;
                }
                else
                {
                    fSYOUSAI = false;
                }
                if (chk_meisai == true)
                {
                    fMEISAI = true;
                }
                else
                {
                    fMEISAI = false;
                }
                if (chk_midashi == true)
                {
                    fMIDASHI = true;
                }
                else
                {
                    fMIDASHI = false;
                }

                //1.見出し、明細、詳細
                //2.見出し、明細
                //3.見出し、詳細
                //4.明細、詳細
                //5.明細
                //6.詳細

                if (fMIDASHI == false && fSYOUSAI == true && fMEISAI == false)//詳細だけ 
                {
                    pcount = 0;
                    rmt_pcount1 = 0;
                    mitsumori rpt = new mitsumori();
                    rpt.cMITUMORI = HF_cMitumori.Value;
                    rpt.cBUKKEN = cBukken;
                    rpt.cTANTOUSHA = cTantousya;
                    rpt.loginId = mail;
                    if (fSYOUSAI == true)
                    {
                        rpt.fSYOUSAI = fSYOUSAI;//詳細チェック出来ると印刷できる
                    }
                    else
                    {
                        rpt.fICHIRAN = true;//詳細のみを印刷できなっかた場合、デフォルトで明細をチェック
                    }
                    rpt.fSYOUSAI = true;//詳細チェック
                    rpt.fICHIRAN = false;//一覧アンチェック
                    rpt.fRYOUHOU = false;
                    rpt.flag_page1 = true;
                    rpt.frogoimage = flagrogo;//ロゴ combobox
                    rpt.cKyoten = cKyoten;
                    if (fHYOUSHI == true)//CB_HYOUSHI.Checked
                    {
                        // frm.hyoushi = 1;
                        rpt.fHYOUSI = true;
                    }
                    else
                    {
                        // frm.hyoushi = 0;
                        rpt.fHYOUSI = false;
                    }
                    if (fZeikomi == "1")
                    {
                        rpt.fZEINUKIKINNGAKU = true;
                    }
                    else if (fZeikomi == "2")
                    {
                        rpt.fZEINUKIKINNGAKU = false;
                    }
                    else
                    {
                        rpt.fZEIFUKUMUKIKINNGAKU = true;
                    }
                    rpt.Run();
                    //String filename = "mitumorisho.pdf";
                    System.IO.MemoryStream ms = new System.IO.MemoryStream();
                    GrapeCity.ActiveReports.Export.Pdf.Section.PdfExport pdfExport1 = new GrapeCity.ActiveReports.Export.Pdf.Section.PdfExport();
                    pdfExport1.Export(rpt.Document, ms);
                    ms.Position = 0;//position stream to 0
                                    //Response.AddHeader("Content-Disposition", "inline;filename=" + filename);
                                    //Response.AddHeader("Content-length", ms.Length.ToString());
                                    //Response.ContentType = "application/pdf";
                                    //Response.BinaryWrite(ms.ToArray());
                                    //Response.End();

                    Session["PDFMemoryStream"] = ms;
                    Session["PDFFileName"] = fileName;
                    Session["UriagePDF"] = "false";
                    //Response.Write("<script language='javascript'>window.open('JCPDFViewer.aspx', '_blank');</script>");
                    if (!String.IsNullOrEmpty(Request.QueryString["id"]))
                    {
                        id = Request.QueryString["id"];
                        Response.Write("<script language='javascript'>window.open('JCPDFViewer.aspx?id=" + id + "', '_blank');</script>");
                    }
                    else
                    {
                        Response.Redirect("JC01Login.aspx");
                    }
                }
                else if (fMIDASHI == false && fSYOUSAI == true && fMEISAI == true)//詳細、明細
                {
                    pcount = 0;
                    rmt_pcount1 = 0;
                    #region pagenumber
                    mitsumori rpt = new mitsumori();
                    rpt.cMITUMORI = HF_cMitumori.Value;
                    rpt.cBUKKEN = cBukken;
                    rpt.cTANTOUSHA = cTantousya;
                    rpt.loginId = mail;
                    rpt.fSYOUSAI = false;
                    rpt.fICHIRAN = true;
                    rpt.fRYOUHOU = fSYOUSAI;
                    rpt.flag_page1 = true;
                    rpt.frogoimage = flagrogo;//ロゴ combobox
                                              //选择打印封页：1，不打印封页：0，由前一个页面传值过来
                    rpt.cKyoten = cKyoten;
                    if (fHYOUSHI == true)//CB_HYOUSHI.Checked
                    {
                        // frm.hyoushi = 1;
                        rpt.fHYOUSI = true;
                    }
                    else
                    {
                        // frm.hyoushi = 0;
                        rpt.fHYOUSI = false;
                    }
                    if (fZeikomi == "1")
                    {
                        rpt.fZEINUKIKINNGAKU = true;
                    }
                    else if (fZeikomi == "2")
                    {
                        rpt.fZEINUKIKINNGAKU = false;
                    }
                    else
                    {
                        rpt.fZEIFUKUMUKIKINNGAKU = true;
                    }
                    rpt.Run();

                    mitsumori rpt_1 = new mitsumori();
                    rpt_1.cMITUMORI = HF_cMitumori.Value;
                    rpt_1.cBUKKEN = cBukken;
                    rpt_1.cTANTOUSHA = cTantousya;
                    rpt_1.loginId =mail;
                    rpt_1.fSYOUSAI = fSYOUSAI;
                    rpt_1.fICHIRAN = false;
                    rpt_1.fRYOUHOU = fSYOUSAI;
                    pcount = rpt.pcount;
                    rpt_1.pcount = pcount;
                    rpt_1.flag_page1 = true;
                    rpt_1.frogoimage = flagrogo;//ロゴ combobox
                                                //选择打印封页：1，不打印封页：0，由前一个页面传值过来
                    rpt_1.cKyoten = cKyoten;
                    if (fHYOUSHI == true)//CB_HYOUSHI.Checked
                    {
                        //frm.hyoushi = 1;
                        rpt_1.fHYOUSI = true;
                    }
                    else
                    {
                        // frm.hyoushi = 0;
                        rpt_1.fHYOUSI = false;
                    }
                    rpt_1.Run();

                    if (fSYOUSAI == true)
                    {
                        for (int i = 0; i < rpt_1.Document.Pages.Count; i++)//一覧と詳細両方表示
                        {
                            rpt.Document.Pages.Add(rpt_1.Document.Pages[i]);
                        }
                    }

                    //System.IO.MemoryStream ms = new System.IO.MemoryStream();
                    //GrapeCity.ActiveReports.Export.Pdf.Section.PdfExport pdfExport1 = new GrapeCity.ActiveReports.Export.Pdf.Section.PdfExport();
                    //pdfExport1.Export(rpt.Document, ms);
                    //ms.Position = 0;//position stream to 0
                    //Response.ContentType = "application/pdf";
                    //Response.BinaryWrite(ms.ToArray());
                    //Response.End();

                    #endregion

                    #region 一覧
                    mitsumori prt4 = new mitsumori(); // 纵向数据表 type1
                    prt4.cMITUMORI = HF_cMitumori.Value;
                    prt4.cBUKKEN = cBukken;
                    prt4.cTANTOUSHA = cTantousya;
                    prt4.loginId = mail;
                    prt4.fSYOUSAI = false;//詳細チェック
                    prt4.fICHIRAN = true;//一覧アンチェック
                    prt4.fRYOUHOU = fSYOUSAI;
                    prt4.flag_page1 = false;
                    prt4.frogoimage = flagrogo;
                    prt4.cKyoten = cKyoten;
                    pcount = rpt.Document.Pages.Count;
                    prt4.pcount = pcount;
                    //选择打印封页：1，不打印封页：0，由前一个页面传值过来
                    if (fHYOUSHI == true)
                    {
                        //frm.hyoushi = 1;
                        prt4.fHYOUSI = true;
                    }
                    else
                    {
                        // frm.hyoushi = 0;
                        prt4.fHYOUSI = false;
                    }
                    prt4.Run();
                    #endregion

                    #region 詳細
                    rmt_pcount1 = rpt.nPAGECOUNT;
                    pcount = prt4.pcount;

                    mitsumori prt5 = new mitsumori(); // 纵向数据表 type1
                    prt5.cMITUMORI = HF_cMitumori.Value;
                    prt5.cBUKKEN = cBukken;
                    prt5.cTANTOUSHA = cTantousya;
                    prt5.loginId = mail;
                    prt5.fSYOUSAI = fSYOUSAI;//詳細チェック出来ると印刷できる
                    prt5.fICHIRAN = false;//一覧アンチェック
                    prt5.fRYOUHOU = fSYOUSAI;
                    prt5.frogoimage = flagrogo;
                    prt5.cKyoten = cKyoten;
                    prt5.rmt_pcount1 = rmt_pcount1;
                    prt5.pcount = pcount;
                    //选择打印封页：1，不打印封页：0，由前一个页面传值过来
                    if (fHYOUSHI == true)//CB_HYOUSHI.Checked
                    {
                        //  frm.hyoushi = 1;
                        prt5.fHYOUSI = true;
                    }
                    else
                    {
                        //  frm.hyoushi = 0;
                        prt5.fHYOUSI = false;
                    }
                    prt5.Run();

                    if (fSYOUSAI == true)
                    {
                        for (int i = 0; i < prt5.Document.Pages.Count; i++)//一覧と詳細両方表示
                        {
                            prt4.Document.Pages.Add(prt5.Document.Pages[i]);
                        }
                    }

                    //String filename = "mitumorisho.pdf";
                    System.IO.MemoryStream ms = new System.IO.MemoryStream();
                    GrapeCity.ActiveReports.Export.Pdf.Section.PdfExport pdfExport1 = new GrapeCity.ActiveReports.Export.Pdf.Section.PdfExport();
                    pdfExport1.Export(rpt.Document, ms);
                    ms.Position = 0;//position stream to 0
                                    //Response.AddHeader("Content-Disposition", "inline;filename=" + filename);
                                    //Response.AddHeader("Content-length", ms.Length.ToString());
                                    //Response.ContentType = "application/pdf";
                                    //Response.BinaryWrite(ms.ToArray());
                                    //Response.End();

                    Session["PDFMemoryStream"] = ms;
                    Session["PDFFileName"] = fileName;
                    Session["UriagePDF"] = "false";
                    //Response.Write("<script language='javascript'>window.open('JCPDFViewer.aspx', '_blank');</script>");
                    if (!String.IsNullOrEmpty(Request.QueryString["id"]))
                    {
                        id = Request.QueryString["id"];
                        Response.Write("<script language='javascript'>window.open('JCPDFViewer.aspx?id=" + id + "', '_blank');</script>");
                    }
                    else
                    {
                        Response.Redirect("JC01Login.aspx");
                    }

                    #endregion
                }
                else
                {
                    if (fMIDASHI == false)
                    {
                        #region 見出し
                        pcount = 0;
                        rmt_pcount1 = 0;
                        mitsumori rpt = new mitsumori();
                        rpt.cMITUMORI = HF_cMitumori.Value;
                        rpt.cBUKKEN = cBukken;
                        rpt.cTANTOUSHA = cTantousya;
                        rpt.loginId = mail;
                        rpt.fSYOUSAI = false;//詳細チェック
                        rpt.fICHIRAN = true;//一覧チェック
                        rpt.fRYOUHOU = false;
                        rpt.flag_page1 = true;
                        rpt.frogoimage = flagrogo;//ロゴ combobox
                        rpt.cKyoten = cKyoten;
                        if (fHYOUSHI == true)//CB_HYOUSHI.Checked
                        {
                            // frm.hyoushi = 1;
                            rpt.fHYOUSI = true;
                        }
                        else
                        {
                            // frm.hyoushi = 0;
                            rpt.fHYOUSI = false;
                        }
                        if (fZeikomi == "1")
                        {
                            rpt.fZEINUKIKINNGAKU = true;
                        }
                        else if (fZeikomi == "2")
                        {
                            rpt.fZEINUKIKINNGAKU = false;
                        }
                        else
                        {
                            rpt.fZEIFUKUMUKIKINNGAKU = true;
                        }
                        rpt.Run();

                        //String filename = "mitumorisho.pdf";
                        System.IO.MemoryStream ms = new System.IO.MemoryStream();
                        GrapeCity.ActiveReports.Export.Pdf.Section.PdfExport pdfExport1 = new GrapeCity.ActiveReports.Export.Pdf.Section.PdfExport();
                        pdfExport1.Export(rpt.Document, ms);
                        ms.Position = 0;//position stream to 0
                                        //Response.AddHeader("Content-Disposition", "inline;filename=" + filename);
                                        //Response.AddHeader("Content-length", ms.Length.ToString());
                                        //Response.ContentType = "application/pdf";
                                        //Response.BinaryWrite(ms.ToArray());
                                        //Response.End();
                        Session["PDFMemoryStream"] = ms;
                        Session["PDFFileName"] = fileName;
                        Session["UriagePDF"] = "false";
                        //Response.Write("<script language='javascript'>window.open('JCPDFViewer.aspx', '_blank');</script>");
                        if (!String.IsNullOrEmpty(Request.QueryString["id"]))
                        {
                            id = Request.QueryString["id"];
                            Response.Write("<script language='javascript'>window.open('JCPDFViewer.aspx?id=" + id + "', '_blank');</script>");
                        }
                        else
                        {
                            Response.Redirect("JC01Login.aspx");
                        }
                        #endregion
                    }
                    else
                    {
                        if (fMIDASHI == true && fSYOUSAI == true && fMEISAI == true) //見出し、明細、詳細
                        {
                            #region pagenumber
                            #region 見出し
                            pcount = 0;
                            rmt_pcount1 = 0;
                            mitsumori rpt_midashi = new mitsumori();
                            rpt_midashi.cMITUMORI = HF_cMitumori.Value;
                            rpt_midashi.cBUKKEN = cBukken;
                            rpt_midashi.cTANTOUSHA = cTantousya;
                            rpt_midashi.loginId = mail;
                            rpt_midashi.fSYOUSAI = false;//詳細チェック
                            rpt_midashi.fICHIRAN = false;//一覧アンチェック
                            rpt_midashi.fRYOUHOU = true;
                            rpt_midashi.fMIDASHI = fMIDASHI;//見出しチェック
                            rpt_midashi.flag_page1 = true;
                            rpt_midashi.frogoimage = flagrogo;//ロゴ combobox
                            rpt_midashi.cKyoten = cKyoten;
                            if (fMIDASHI == true)
                            {
                                rpt_midashi.Run();
                            }
                            #endregion

                            #region 明細
                            // rmt_pcount1 = 0;
                            mitsumori rpt_mesai = new mitsumori();
                            rpt_mesai.cMITUMORI = HF_cMitumori.Value;
                            rpt_mesai.cBUKKEN = cBukken;
                            rpt_mesai.cTANTOUSHA = cTantousya;
                            rpt_mesai.loginId = mail;
                            rpt_mesai.fSYOUSAI = false;//詳細チェック
                            rpt_mesai.fICHIRAN = true;//一覧アンチェック
                            rpt_mesai.fRYOUHOU = true;
                            rpt_mesai.frogoimage = flagrogo;//ロゴ combobox
                            rpt_mesai.flag_page1 = true;
                            rpt_mesai.cKyoten = cKyoten;
                            // rpt_mesai.rmt_pcount1 = rpt_midashi.nPAGECOUNT;
                            pcount = rpt_midashi.pcount;
                            rpt_mesai.pcount = pcount;
                            rpt_mesai.Run();

                            #endregion

                            #region 詳細
                            mitsumori rpt_syousai = new mitsumori();
                            rpt_syousai.cMITUMORI = HF_cMitumori.Value;
                            rpt_syousai.cBUKKEN = cBukken;
                            rpt_syousai.cTANTOUSHA = cTantousya;
                            rpt_syousai.loginId = mail;
                            rpt_syousai.fSYOUSAI = fSYOUSAI;//詳細チェック出来ると印刷できる
                            rpt_syousai.fICHIRAN = false;//一覧アンチェック
                            rpt_syousai.fRYOUHOU = fSYOUSAI;
                            rpt_syousai.flag_page1 = true;
                            rpt_syousai.frogoimage = flagrogo;//ロゴ combobox
                                                              //rpt_syousai.rmt_pcount1 = rpt_mesai.nPAGECOUNT;
                            rpt_syousai.cKyoten = cKyoten;
                            pcount = rpt_mesai.pcount;
                            rpt_syousai.pcount = pcount;
                            if (fSYOUSAI == true)
                            {
                                rpt_syousai.Run();
                            }

                            if (fMIDASHI == true)
                            {
                                for (int i = 0; i < rpt_mesai.Document.Pages.Count; i++)
                                {
                                    rpt_midashi.Document.Pages.Add(rpt_mesai.Document.Pages[i]);
                                }

                                if (fSYOUSAI == true)
                                {
                                    for (int i = 0; i < rpt_syousai.Document.Pages.Count; i++)
                                    {
                                        rpt_midashi.Document.Pages.Add(rpt_syousai.Document.Pages[i]);
                                    }
                                }
                            }
                            else
                            {
                                if (fSYOUSAI == true)
                                {
                                    for (int i = 0; i < rpt_syousai.Document.Pages.Count; i++)//一覧と詳細両方表示
                                    {
                                        rpt_midashi.Document.Pages.Add(rpt_syousai.Document.Pages[i]);
                                    }
                                }
                            }

                            //System.IO.MemoryStream ms = new System.IO.MemoryStream();
                            //GrapeCity.ActiveReports.Export.Pdf.Section.PdfExport pdfExport1 = new GrapeCity.ActiveReports.Export.Pdf.Section.PdfExport();
                            //pdfExport1.Export(rpt_midashi.Document, ms);
                            //ms.Position = 0;//position stream to 0
                            //Response.ContentType = "application/pdf";
                            //Response.BinaryWrite(ms.ToArray());
                            //Response.End();

                            #endregion

                            #endregion

                            #region 表示

                            #region 見出し

                            mitsumori rpt_midashi1 = new mitsumori();
                            rpt_midashi1.cMITUMORI = HF_cMitumori.Value;
                            rpt_midashi1.cBUKKEN = cBukken;
                            rpt_midashi1.cTANTOUSHA = cTantousya;
                            rpt_midashi1.loginId = mail;
                            rpt_midashi1.fSYOUSAI = false;//詳細チェック
                            rpt_midashi1.fICHIRAN = false;//一覧アンチェック
                            rpt_midashi1.fRYOUHOU = fSYOUSAI;
                            rpt_midashi1.fMIDASHI = fMIDASHI;//見出しチェック
                            rpt_midashi1.flag_page1 = false;
                            rpt_midashi1.frogoimage = flagrogo;//ロゴ combobox
                            rpt_midashi1.cKyoten = cKyoten;
                            rmt_pcount1 = rpt_midashi.nPAGECOUNT;
                            // rpt_midashi1.rmt_pcount1 = rmt_pcount1;
                            pcount = rpt_midashi.Document.Pages.Count;
                            rpt_midashi1.pcount = pcount;
                            if (fMIDASHI == true)
                            {
                                rpt_midashi1.Run();
                            }
                            #endregion

                            #region 一覧
                            // rmt_pcount1 = rpt_midashi1.nPAGECOUNT;
                            pcount = rpt_midashi1.pcount;
                            mitsumori rpt4 = new mitsumori();
                            rpt4.cMITUMORI = HF_cMitumori.Value;
                            rpt4.cBUKKEN = cBukken;
                            rpt4.cTANTOUSHA = cTantousya;
                            rpt4.loginId = mail;
                            rpt4.fSYOUSAI = false;//詳細チェック
                            rpt4.fICHIRAN = true;//一覧アンチェック
                            rpt4.fRYOUHOU = fSYOUSAI;
                            rpt4.frogoimage = flagrogo;//ロゴ combobox
                                                       //  rpt4.rmt_pcount1 = rmt_pcount1;
                            rpt4.cKyoten = cKyoten;
                            rpt4.pcount = pcount;
                            rpt4.Run();
                            #endregion

                            #region 詳細
                            if (fHYOUSHI == true)//CB_HYOUSHI.Checked 
                            {
                                rmt_pcount1 = (rpt_midashi.nPAGECOUNT + rpt_mesai.nPAGECOUNT) - 1;//合計書数-表紙数 //prt4_p1
                            }
                            else
                            {
                                rmt_pcount1 = rpt_midashi.nPAGECOUNT + rpt_mesai.nPAGECOUNT;//合計書数-表紙数
                            }

                            mitsumori rpt5 = new mitsumori();
                            rpt5.cMITUMORI = HF_cMitumori.Value;
                            rpt5.cBUKKEN = cBukken;
                            rpt5.cTANTOUSHA = cTantousya;
                            rpt5.loginId = mail;
                            rpt5.fSYOUSAI = fSYOUSAI;//詳細チェック出来ると印刷できる
                            rpt5.fICHIRAN = false;//一覧アンチェック
                            rpt5.fRYOUHOU = fSYOUSAI;
                            if (fHYOUSHI == true)//CB_HYOUSHI.Checked
                            {
                                //frm.hyoushi = 1;
                                rpt5.fHYOUSI = true;
                            }
                            else
                            {
                                // frm.hyoushi = 0;
                                rpt5.fHYOUSI = false;
                            }
                            rpt5.frogoimage = flagrogo;//ロゴ combobox
                                                       //rpt5.rmt_pcount1 = rmt_pcount1;
                            rpt5.cKyoten = cKyoten;
                            pcount = rpt_midashi1.pcount;
                            rpt5.pcount = pcount;
                            if (fSYOUSAI == true)
                            {
                                rpt5.Run();
                            }

                            if (fMIDASHI == true)
                            {
                                for (int i = 0; i < rpt4.Document.Pages.Count; i++)
                                {
                                    rpt_midashi1.Document.Pages.Add(rpt4.Document.Pages[i]);
                                }

                                if (fSYOUSAI == true)
                                {
                                    for (int i = 0; i < rpt5.Document.Pages.Count; i++)
                                    {
                                        rpt_midashi1.Document.Pages.Add(rpt5.Document.Pages[i]);
                                    }
                                }

                                // frm.prt = prt4_midashi1;

                                //String filename = "mitumorisho.pdf";
                                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                                GrapeCity.ActiveReports.Export.Pdf.Section.PdfExport pdfExport1 = new GrapeCity.ActiveReports.Export.Pdf.Section.PdfExport();
                                pdfExport1.Export(rpt_midashi1.Document, ms);
                                ms.Position = 0;//position stream to 0
                                                //Response.AddHeader("Content-Disposition", "inline;filename=" + filename);
                                                //Response.AddHeader("Content-length", ms.Length.ToString());
                                                //Response.ContentType = "application/pdf";
                                                //Response.BinaryWrite(ms.ToArray());
                                                //Response.End();
                                Session["PDFMemoryStream"] = ms;
                                Session["PDFFileName"] = fileName;
                                Session["UriagePDF"] = "false";
                                //Response.Write("<script language='javascript'>window.open('JCPDFViewer.aspx', '_blank');</script>");
                                if (!String.IsNullOrEmpty(Request.QueryString["id"]))
                                {
                                    id = Request.QueryString["id"];
                                    Response.Write("<script language='javascript'>window.open('JCPDFViewer.aspx?id=" + id + "', '_blank');</script>");
                                }
                                else
                                {
                                    Response.Redirect("JC01Login.aspx");
                                }
                            }
                            else
                            {
                                if (fSYOUSAI == true)
                                {
                                    for (int i = 0; i < rpt5.Document.Pages.Count; i++)//一覧と詳細両方表示
                                    {
                                        rpt4.Document.Pages.Add(rpt5.Document.Pages[i]);
                                    }
                                }

                                // frm.prt = prt4;
                                //String filename = "mitumorisho.pdf";
                                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                                GrapeCity.ActiveReports.Export.Pdf.Section.PdfExport pdfExport1 = new GrapeCity.ActiveReports.Export.Pdf.Section.PdfExport();
                                pdfExport1.Export(rpt4.Document, ms);
                                ms.Position = 0;//position stream to 0
                                                //Response.AddHeader("Content-Disposition", "inline;filename=" + filename);
                                                //Response.AddHeader("Content-length", ms.Length.ToString());
                                                //Response.ContentType = "application/pdf";
                                                //Response.BinaryWrite(ms.ToArray());
                                                //Response.End();
                                Session["PDFMemoryStream"] = ms;
                                Session["PDFFileName"] = fileName;
                                Session["UriagePDF"] = "false";
                                if (!String.IsNullOrEmpty(Request.QueryString["id"]))
                                {
                                    id = Request.QueryString["id"];
                                    Response.Write("<script language='javascript'>window.open('JCPDFViewer.aspx?id=" + id + "', '_blank');</script>");
                                }
                                else
                                {
                                    Response.Redirect("JC01Login.aspx");
                                }
                            }

                            #endregion

                            #endregion
                        }
                        else if (fMIDASHI == true && (fSYOUSAI == true && fMEISAI == false)) //見出し、明細かつ詳細
                        {
                            #region pagenumber
                            rmt_pcount1 = 0;
                            #region 見出し
                            mitsumori prt4_midashi = new mitsumori(); // 纵向数据表 type1
                            prt4_midashi.cMITUMORI = HF_cMitumori.Value;
                            prt4_midashi.cBUKKEN = cBukken;
                            prt4_midashi.cTANTOUSHA = cTantousya;
                            prt4_midashi.loginId = mail;
                            prt4_midashi.fSYOUSAI = false;//詳細チェック
                            prt4_midashi.fICHIRAN = false;//一覧アンチェック
                            prt4_midashi.fRYOUHOU = false;
                            prt4_midashi.fMIDASHI = fMIDASHI;//見出しチェック
                            prt4_midashi.flag_page1 = true;
                            prt4_midashi.frogoimage = flagrogo;//ロゴ combobox
                                                               //选择打印封页：1，不打印封页：0，由前一个页面传值过来
                            prt4_midashi.cKyoten = cKyoten;
                            if (fHYOUSHI == true)//CB_HYOUSHI.Checked
                            {
                                // frm.hyoushi = 1;
                                prt4_midashi.fHYOUSI = true;
                            }
                            else
                            {
                                // frm.hyoushi = 0;
                                prt4_midashi.fHYOUSI = false;
                            }
                            if (fMIDASHI == true)
                            {
                                prt4_midashi.Run();
                            }
                            #endregion

                            #region 詳細
                            mitsumori prt5_p1 = new mitsumori(); // 纵向数据表 type1
                            prt5_p1.cMITUMORI = HF_cMitumori.Value;
                            prt5_p1.cBUKKEN = cBukken;
                            prt5_p1.cTANTOUSHA = cTantousya;
                            prt5_p1.loginId = mail;
                            prt5_p1.fSYOUSAI = fSYOUSAI;//詳細チェック出来ると印刷できる
                            if (fSYOUSAI == false && fMIDASHI == false)
                            {
                                prt5_p1.fICHIRAN = true;//見出しと詳細両方ともない場合、明細をデフォルトで表示する
                            }
                            else
                            {
                                prt5_p1.fICHIRAN = false;//一覧アンチェック
                            }
                            prt5_p1.fRYOUHOU = false;
                            prt5_p1.frogoimage = flagrogo;//ロゴ combobox
                                                          //选择打印封页：1，不打印封页：0，由前一个页面传值过来
                            prt5_p1.cKyoten = cKyoten;
                            if (fHYOUSHI == true)//CB_HYOUSHI.Checked
                            {
                                // frm.hyoushi = 1;
                                prt5_p1.fHYOUSI = true;
                            }
                            else
                            {
                                // frm.hyoushi = 0;
                                prt5_p1.fHYOUSI = false;
                            }
                            prt5_p1.Run();

                            if (fMIDASHI == true)
                            {
                                for (int i = 0; i < prt5_p1.Document.Pages.Count; i++)//一覧と詳細両方表示
                                {
                                    prt4_midashi.Document.Pages.Add(prt5_p1.Document.Pages[i]);
                                }
                            }
                            //String filename = "mitumorisho.pdf";
                            System.IO.MemoryStream ms = new System.IO.MemoryStream();
                            GrapeCity.ActiveReports.Export.Pdf.Section.PdfExport pdfExport1 = new GrapeCity.ActiveReports.Export.Pdf.Section.PdfExport();
                            pdfExport1.Export(prt4_midashi.Document, ms);
                            ms.Position = 0;//position stream to 0
                                            //Response.AddHeader("Content-Disposition", "inline;filename=" + filename);
                                            //Response.AddHeader("Content-length", ms.Length.ToString());
                                            //Response.ContentType = "application/pdf";
                                            //Response.BinaryWrite(ms.ToArray());
                                            //Response.End();
                            Session["PDFMemoryStream"] = ms;
                            Session["PDFFileName"] = fileName;
                            Session["UriagePDF"] = "false";
                            if (!String.IsNullOrEmpty(Request.QueryString["id"]))
                            {
                                id = Request.QueryString["id"];
                                Response.Write("<script language='javascript'>window.open('JCPDFViewer.aspx?id=" + id + "', '_blank');</script>");
                            }
                            else
                            {
                                Response.Redirect("JC01Login.aspx");
                            }
                            #endregion

                            #endregion

                            #region 表示

                            #region 見出し
                            mitsumori prt4_midashi1 = new mitsumori(); // 纵向数据表 type1
                            prt4_midashi1.cMITUMORI = HF_cMitumori.Value;
                            prt4_midashi1.cBUKKEN = cBukken;
                            prt4_midashi1.cTANTOUSHA = cTantousya;
                            prt4_midashi1.loginId = mail;
                            prt4_midashi1.fSYOUSAI = false;//詳細チェック
                            prt4_midashi1.fICHIRAN = false;//一覧アンチェック
                            prt4_midashi1.fRYOUHOU = false;
                            prt4_midashi1.fMIDASHI = fMIDASHI;//見出しチェック
                            prt4_midashi1.flag_page1 = false;
                            prt4_midashi1.frogoimage = flagrogo;//ロゴ combobox
                                                                //选择打印封页：1，不打印封页：0，由前一个页面传值过来
                            prt4_midashi1.cKyoten = cKyoten;
                            if (fHYOUSHI == true)//CB_HYOUSHI.Checked
                            {
                                // frm.hyoushi = 1;
                                prt4_midashi1.fHYOUSI = true;
                            }
                            else
                            {
                                // frm.hyoushi = 0;
                                prt4_midashi1.fHYOUSI = false;
                            }
                            if (fMIDASHI == true)
                            {
                                prt4_midashi1.Run();
                            }
                            #endregion

                            #region 詳細

                            rmt_pcount1 = prt4_midashi.nPAGECOUNT;

                            mitsumori prt5 = new mitsumori(); // 纵向数据表 type1
                            prt5.cMITUMORI = HF_cMitumori.Value;
                            prt5.cBUKKEN = cBukken;
                            prt5.cTANTOUSHA = cTantousya;
                            prt5.loginId = mail;
                            prt5.fSYOUSAI = fSYOUSAI;//詳細チェック出来ると印刷できる
                            if (fSYOUSAI == false && fMIDASHI == false)
                            {
                                prt5.fICHIRAN = true;
                            }
                            else
                            {
                                prt5.fICHIRAN = false;//一覧アンチェック
                            }
                            prt5.fRYOUHOU = false;
                            prt5.frogoimage = flagrogo;//ロゴ combobox
                                                       //选择打印封页：1，不打印封页：0，由前一个页面传值过来
                            prt5.cKyoten = cKyoten;
                            if (fHYOUSHI == true)//CB_HYOUSHI.Checked
                            {
                                //frm.hyoushi = 1;
                                prt5.fHYOUSI = true;
                            }
                            else
                            {
                                //frm.hyoushi = 0;
                                prt5.fHYOUSI = false;
                            }
                            prt5.Run();

                            if (fMIDASHI == true)
                            {
                                for (int i = 0; i < prt5.Document.Pages.Count; i++)
                                {
                                    prt4_midashi1.Document.Pages.Add(prt5.Document.Pages[i]);
                                }

                                //frm.prt = prt4_midashi1;
                            }
                            else
                            {
                                // frm.prt = prt5;
                            }
                            // System.IO.MemoryStream ms = new System.IO.MemoryStream();
                            // GrapeCity.ActiveReports.Export.Pdf.Section.PdfExport pdfExport1 = new GrapeCity.ActiveReports.Export.Pdf.Section.PdfExport();
                            pdfExport1.Export(prt4_midashi1.Document, ms);
                            ms.Position = 0;//position stream to 0
                            //Response.AddHeader("Content-Disposition", "inline;filename=" + filename);
                            //Response.AddHeader("Content-length", ms.Length.ToString());
                            //Response.ContentType = "application/pdf";
                            //Response.BinaryWrite(ms.ToArray());
                            //Response.End();
                            Session["PDFMemoryStream"] = ms;
                            Session["PDFFileName"] = fileName;
                            Session["UriagePDF"] = "false";
                            if (!String.IsNullOrEmpty(Request.QueryString["id"]))
                            {
                                id = Request.QueryString["id"];
                                Response.Write("<script language='javascript'>window.open('JCPDFViewer.aspx?id=" + id + "', '_blank');</script>");
                            }
                            else
                            {
                                Response.Redirect("JC01Login.aspx");
                            }

                            #endregion

                            #endregion
                        }
                        else if (fMIDASHI == true && (fSYOUSAI == false && fMEISAI == true)) //見出し、明細かつ詳細
                        {
                            rmt_pcount1 = 0;
                            #region pagenumber
                            #region 見出し
                            mitsumori prt4_midashi = new mitsumori(); // 纵向数据表 type1
                            prt4_midashi.cMITUMORI = HF_cMitumori.Value;
                            prt4_midashi.cBUKKEN = cBukken;
                            prt4_midashi.cTANTOUSHA = cTantousya;
                            prt4_midashi.loginId = mail;
                            prt4_midashi.fSYOUSAI = false;//詳細チェック
                            prt4_midashi.fICHIRAN = false;//一覧アンチェック
                            prt4_midashi.fRYOUHOU = false;
                            prt4_midashi.fMIDASHI = fMIDASHI;//見出しチェック
                            prt4_midashi.flag_page1 = true;
                            prt4_midashi.frogoimage = flagrogo;//ロゴ combobox
                                                               //选择打印封页：1，不打印封页：0，由前一个页面传值过来
                            prt4_midashi.cKyoten = cKyoten;
                            if (fHYOUSHI == true)//
                            {
                                // frm.hyoushi = 1;
                                prt4_midashi.fHYOUSI = true;
                            }
                            else
                            {
                                // frm.hyoushi = 0;
                                prt4_midashi.fHYOUSI = false;
                            }
                            if (fMIDASHI == true)
                            {
                                prt4_midashi.Run();
                            }
                            #endregion

                            #region 明細
                            mitsumori prt4_p1 = new mitsumori(); // 纵向数据表 type1
                            prt4_p1.cMITUMORI = HF_cMitumori.Value;
                            prt4_p1.cBUKKEN = cBukken;
                            prt4_p1.cTANTOUSHA = cTantousya;
                            prt4_p1.loginId = mail;
                            prt4_p1.fSYOUSAI = false;//詳細チェック
                            prt4_p1.fICHIRAN = true;//一覧アンチェック
                            prt4_p1.fRYOUHOU = false;
                            prt4_p1.frogoimage = flagrogo;//ロゴ combobox
                                                          //选择打印封页：1，不打印封页：0，由前一个页面传值过来
                            prt4_p1.cKyoten = cKyoten;
                            if (fHYOUSHI == true)//CB_HYOUSHI.Checked
                            {
                                //frm.hyoushi = 1;
                                prt4_p1.fHYOUSI = true;
                            }
                            else
                            {
                                // frm.hyoushi = 0;
                                prt4_p1.fHYOUSI = false;
                            }
                            prt4_p1.Run();

                            if (fMIDASHI == true)
                            {
                                for (int i = 0; i < prt4_p1.Document.Pages.Count; i++)
                                {
                                    prt4_midashi.Document.Pages.Add(prt4_p1.Document.Pages[i]);
                                }
                            }
                            //String filename = "mitumorisho.pdf";
                            System.IO.MemoryStream ms = new System.IO.MemoryStream();
                            GrapeCity.ActiveReports.Export.Pdf.Section.PdfExport pdfExport1 = new GrapeCity.ActiveReports.Export.Pdf.Section.PdfExport();
                            pdfExport1.Export(prt4_midashi.Document, ms);
                            ms.Position = 0;//position stream to 0
                                            //Response.AddHeader("Content-Disposition", "inline;filename=" + filename);
                                            //Response.AddHeader("Content-length", ms.Length.ToString());
                                            //Response.ContentType = "application/pdf";
                                            //Response.BinaryWrite(ms.ToArray());
                                            //Response.End();
                            Session["PDFMemoryStream"] = ms;
                            Session["PDFFileName"] = fileName;
                            Session["UriagePDF"] = "false";
                            if (!String.IsNullOrEmpty(Request.QueryString["id"]))
                            {
                                id = Request.QueryString["id"];
                                Response.Write("<script language='javascript'>window.open('JCPDFViewer.aspx?id=" + id + "', '_blank');</script>");
                            }
                            else
                            {
                                Response.Redirect("JC01Login.aspx");
                            }
                            #endregion

                            #endregion

                            #region 表示

                            #region 見出し
                            mitsumori prt4_midashi1 = new mitsumori(); // 纵向数据表 type1
                            prt4_midashi1.cMITUMORI = HF_cMitumori.Value;
                            prt4_midashi1.cBUKKEN = cBukken;
                            prt4_midashi1.cTANTOUSHA = cTantousya;
                            prt4_midashi1.loginId = mail;
                            prt4_midashi1.fSYOUSAI = false;//詳細チェック
                            prt4_midashi1.fICHIRAN = false;//一覧アンチェック
                            prt4_midashi1.fRYOUHOU = false;
                            prt4_midashi1.fMIDASHI = fMIDASHI;//見出しチェック
                            prt4_midashi1.flag_page1 = false;
                            prt4_midashi1.frogoimage = flagrogo;//ロゴ combobox
                                                                //选择打印封页：1，不打印封页：0，由前一个页面传值过来
                            prt4_midashi1.cKyoten = cKyoten;
                            if (fHYOUSHI == true)//CB_HYOUSHI.Checked
                            {
                                // frm.hyoushi = 1;
                                prt4_midashi1.fHYOUSI = true;
                            }
                            else
                            {
                                // frm.hyoushi = 0;
                                prt4_midashi1.fHYOUSI = false;
                            }
                            if (fMIDASHI == true)
                            {
                                prt4_midashi1.Run();
                                // frm.nMAXPAGE = prt4_midashi1.nPAGECOUNT.ToString();
                            }

                            #endregion

                            #region 一覧

                            rmt_pcount1 = prt4_midashi.nPAGECOUNT;

                            mitsumori prt4 = new mitsumori(); // 纵向数据表 type1
                            prt4.cMITUMORI = HF_cMitumori.Value;
                            prt4.cBUKKEN = cBukken;
                            prt4.cTANTOUSHA = cTantousya;
                            prt4.loginId = mail;
                            prt4.fSYOUSAI = false;//詳細チェック
                            prt4.fICHIRAN = true;//一覧アンチェック
                            prt4.fRYOUHOU = false;
                            prt4.frogoimage = flagrogo;//ロゴ combobox
                                                       //选择打印封页：1，不打印封页：0，由前一个页面传值过来
                            prt4.cKyoten = cKyoten;
                            if (fHYOUSHI == true)//CB_HYOUSHI.Checked
                            {
                                // frm.hyoushi = 1;
                                prt4.fHYOUSI = true;
                            }
                            else
                            {
                                // frm.hyoushi = 0;
                                prt4.fHYOUSI = false;
                            }
                            prt4.Run();
                            #endregion

                            if (fMIDASHI == true)
                            {
                                for (int i = 0; i < prt4.Document.Pages.Count; i++)
                                {
                                    prt4_midashi1.Document.Pages.Add(prt4.Document.Pages[i]);
                                }

                                // frm.prt = prt4_midashi1;
                            }
                            else
                            {
                                // frm.prt = prt4;

                            }
                            // System.IO.MemoryStream ms = new System.IO.MemoryStream();
                            // GrapeCity.ActiveReports.Export.Pdf.Section.PdfExport pdfExport1 = new GrapeCity.ActiveReports.Export.Pdf.Section.PdfExport();
                            pdfExport1.Export(prt4_midashi1.Document, ms);
                            ms.Position = 0;//position stream to 0
                            //Response.AddHeader("Content-Disposition", "inline;filename=" + filename);
                            //Response.AddHeader("Content-length", ms.Length.ToString());
                            //Response.ContentType = "application/pdf";
                            //Response.BinaryWrite(ms.ToArray());
                            //Response.End();
                            Session["PDFMemoryStream"] = ms;
                            Session["PDFFileName"] = fileName;
                            Session["UriagePDF"] = "false";
                            if (!String.IsNullOrEmpty(Request.QueryString["id"]))
                            {
                                id = Request.QueryString["id"];
                                Response.Write("<script language='javascript'>window.open('JCPDFViewer.aspx?id=" + id + "', '_blank');</script>");
                            }
                            else
                            {
                                Response.Redirect("JC01Login.aspx");
                            }
                            #endregion
                        }
                        else if (fMIDASHI == true && fSYOUSAI == false && fMEISAI == false) //見出し
                        {
                            rmt_pcount1 = 0;
                            mitsumori rpt = new mitsumori();
                            rpt.cMITUMORI = HF_cMitumori.Value;
                            rpt.cBUKKEN = cBukken;
                            rpt.cTANTOUSHA = cTantousya;
                            rpt.loginId = mail;
                            rpt.fSYOUSAI = false;//詳細チェック
                            if (fMIDASHI == false)
                            {
                                rpt.fICHIRAN = true;//一覧チェック
                            }
                            rpt.fRYOUHOU = false;
                            rpt.fMIDASHI = fMIDASHI;//見出しチェック
                            rpt.flag_page1 = true;
                            rpt.frogoimage = flagrogo;//ロゴ combobox
                            rpt.cKyoten = cKyoten;
                            if (fHYOUSHI == true)//CB_HYOUSHI.Checked
                            {
                                // frm.hyoushi = 1;
                                rpt.fHYOUSI = true;
                            }
                            else
                            {
                                // frm.hyoushi = 0;
                                rpt.fHYOUSI = false;
                            }
                            if (fZeikomi == "1")
                            {
                                rpt.fZEINUKIKINNGAKU = true;
                            }
                            else if (fZeikomi == "2")
                            {
                                rpt.fZEINUKIKINNGAKU = false;
                            }
                            else
                            {
                                rpt.fZEIFUKUMUKIKINNGAKU = true;
                            }
                            rpt.Run();
                            //String filename = "mitumorisho.pdf";
                            System.IO.MemoryStream ms = new System.IO.MemoryStream();
                            GrapeCity.ActiveReports.Export.Pdf.Section.PdfExport pdfExport1 = new GrapeCity.ActiveReports.Export.Pdf.Section.PdfExport();
                            pdfExport1.Export(rpt.Document, ms);
                            ms.Position = 0;//position stream to 0
                                            //Response.AddHeader("Content-Disposition", "inline;filename=" + filename);
                                            //Response.AddHeader("Content-length", ms.Length.ToString());
                                            //Response.ContentType = "application/pdf";
                                            //Response.BinaryWrite(ms.ToArray());
                                            //Response.End();
                            Session["PDFMemoryStream"] = ms;
                            Session["PDFFileName"] = fileName;
                            Session["UriagePDF"] = "false";
                            if (!String.IsNullOrEmpty(Request.QueryString["id"]))
                            {
                                id = Request.QueryString["id"];
                                Response.Write("<script language='javascript'>window.open('JCPDFViewer.aspx?id=" + id + "', '_blank');</script>");
                            }
                            else
                            {
                                Response.Redirect("JC01Login.aspx");
                            }
                        }
                    }
                }
            }
            else
            {
                Response.Redirect("JC01Login.aspx");
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
    }
}