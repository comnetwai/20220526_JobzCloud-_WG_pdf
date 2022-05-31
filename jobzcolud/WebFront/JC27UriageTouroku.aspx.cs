using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Service;
using Common;
using System.Data;
using MySql.Data.MySqlClient;
using System.Text;
using jobzcolud.pdf;

namespace jobzcolud.WebFront
{
    public partial class JC27UriageTouroku : System.Web.UI.Page
    {
        bool fsavekakunin = true;
        DataTable dt_tokuisaki_tantou_jyouhou = new DataTable();
        DataTable dt_tani = new DataTable();
        DataTable dt_data = new DataTable();
        public static string precTOKUISAKI, precSEIKYUSAKI, valsTOKUISAKI_TAN, valsSEIKYUSAKI_TAN, tokuisakinjun, seikyusakinjun, preeigyou, hakkoudate, uriagedate, nyuukinyouteidate;
        //string eigyoutantousha,preeigyou;
        public static string cco = "";
        MySqlConnection con = null;
        bool f_isomoji_msg = true;
        public static string curiage = "";
        Boolean isUriagezumiMsg = false;

        #region Page_Load
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(Request.QueryString["id"]))
            {
                String id = Request.QueryString["id"];
                DataTable dt_db = JC01Login_Class.Get_DB(id);
                String db = dt_db.Rows[0]["db"].ToString();
                String mail = dt_db.Rows[0]["mail"].ToString();
                ConstantVal.DB_NAME = db;

                divLabelSave.Style["display"] = "none";
                ConstantVal.flag_Nav = true;
                // ConstantVal.check_ts = true;
                ConstantVal.gamenname = "uriage";
                if (!IsPostBack)
                {
                    JC27UriageTouroku_Class m_j_info = new JC27UriageTouroku_Class();
                    m_j_info.J_Info();
                    m_j_info.Get_Tani();

                    Login_Tantou();
                    m_j_info.cTANTOUSHA = lblLoginUserCode.Text;
                    DataTable dt_pdf = m_j_info.GetTantou();
                    if (dt_pdf.Rows.Count > 0)
                    {
                        if (dt_pdf.Rows[0]["Shoshiki"].ToString().TrimEnd() == "1")
                        {
                            BT_Pdf.Text = "兼請求書PDF出力";
                        }
                        else
                        {
                            BT_Pdf.Text = "納品書と請求書PDF出力";
                        }
                    }

                    DataTable dt_title = m_j_info.Get_Title();
                    if (dt_title.Rows.Count > 0)
                    {
                        if (dt_title.Rows[0]["sIMAGETitle1"].ToString().TrimEnd() != "")
                        {
                            DDL_Logo.Items.Add(dt_title.Rows[0]["sIMAGETitle1"].ToString());
                        }
                        if (dt_title.Rows[0]["sIMAGETitle2"].ToString().TrimEnd() != "")
                        {
                            DDL_Logo.Items.Add(dt_title.Rows[0]["sIMAGETitle2"].ToString());
                        }
                        if (dt_title.Rows[0]["sIMAGETitle3"].ToString().TrimEnd() != "")
                        {
                            DDL_Logo.Items.Add(dt_title.Rows[0]["sIMAGETitle3"].ToString());
                        }
                        if (dt_title.Rows[0]["sIMAGETitle4"].ToString().TrimEnd() != "")
                        {
                            DDL_Logo.Items.Add(dt_title.Rows[0]["sIMAGETitle4"].ToString());
                        }
                        if (dt_title.Rows[0]["sIMAGETitle5"].ToString().TrimEnd() != "")
                        {
                            DDL_Logo.Items.Add(dt_title.Rows[0]["sIMAGETitle5"].ToString());
                        }

                        if (dt_title.Rows[0]["sBIKOUTitle1"].ToString().TrimEnd() != "")
                        {
                            DDL_bikou.Items.Add(dt_title.Rows[0]["sBIKOUTitle1"].ToString());
                        }
                        if (dt_title.Rows[0]["sBIKOUTitle2"].ToString().TrimEnd() != "")
                        {
                            DDL_bikou.Items.Add(dt_title.Rows[0]["sBIKOUTitle2"].ToString());
                        }
                        if (dt_title.Rows[0]["sBIKOUTitle3"].ToString().TrimEnd() != "")
                        {
                            DDL_bikou.Items.Add(dt_title.Rows[0]["sBIKOUTitle3"].ToString());
                        }
                        if (dt_title.Rows[0]["sBIKOUTitle4"].ToString().TrimEnd() != "")
                        {
                            DDL_bikou.Items.Add(dt_title.Rows[0]["sBIKOUTitle4"].ToString());
                        }
                        if (dt_title.Rows[0]["sBIKOUTitle5"].ToString().TrimEnd() != "")
                        {
                            DDL_bikou.Items.Add(dt_title.Rows[0]["sBIKOUTitle5"].ToString());
                        }
                        if (dt_title.Rows[0]["sBIKOUTitle1"].ToString().TrimEnd() == "" && dt_title.Rows[0]["sBIKOUTitle2"].ToString().TrimEnd() == "" && dt_title.Rows[0]["sBIKOUTitle3"].ToString().TrimEnd() == "" && dt_title.Rows[0]["sBIKOUTitle4"].ToString().TrimEnd() == "" && dt_title.Rows[0]["sBIKOUTitle5"].ToString().TrimEnd() == "")
                        {
                            DDL_bikou.Items.Insert(0, new ListItem(""));
                        }
                        if (dt_title.Rows[0]["sIMAGETitle1"].ToString().TrimEnd() == "" && dt_title.Rows[0]["sIMAGETitle2"].ToString().TrimEnd() == "" && dt_title.Rows[0]["sIMAGETitle3"].ToString().TrimEnd() == "" && dt_title.Rows[0]["sIMAGETitle4"].ToString().TrimEnd() == "" && dt_title.Rows[0]["sIMAGETitle5"].ToString().TrimEnd() == "")
                        {
                            DDL_Logo.Items.Insert(0, new ListItem(""));
                        }
                    }

                    //DataTable dt_bikoutitle = m_j_info.Get_BikouTitle();

                    //DDL_bikou.DataTextField = "title";
                    //DDL_bikou.DataValueField = "title";
                    //DDL_bikou.DataSource = dt_bikoutitle;
                    //DDL_bikou.DataBind();

                    #region 端数処理
                    if (JC27UriageTouroku_Class.dt_J_Info.Rows.Count > 0)
                    {
                        if (JC27UriageTouroku_Class.dt_J_Info.Rows[0]["ftansuushori"].ToString() == "1")//切り捨て
                        {
                            DDL_ftansuushori.SelectedIndex = 0;
                        }
                        else if (JC27UriageTouroku_Class.dt_J_Info.Rows[0]["ftansuushori"].ToString() == "2")//四捨五入
                        {
                            DDL_ftansuushori.SelectedIndex = 1;
                        }
                        else   //切り上げ
                        {
                            DDL_ftansuushori.SelectedIndex = 2;
                        }
                        updftansuushori.Update();
                    }
                    #endregion

                    if (Session["cUriage"] == null)
                    {
                        #region 新規
                        if (JC99NavBar.insatsusettei == false)
                        {
                            BT_Pdf.Enabled = false;
                            BT_Pdf.CssClass = "JC10SaveBtnDisable";
                            BT_Sakujo.Visible = false;

                            CB_NouhinKingaku.Text = (m_j_info.Get_NouhinKingaku(Session["cMITUMORI"].ToString(), "0000000000")).ToString("#,##0");  //納品金額

                            Div_shousai.Attributes["style"] = "display:block";
                            Div_settei.Attributes["style"] = "display:none";

                            #region　受注金額
                            if (Session["Jyuchuukingaku"] != null)
                            {
                                if (Session["Jyuchuukingaku"].ToString() != "")
                                {
                                    CB_Jyuchuukingaku.Text = (decimal.Parse(Session["Jyuchuukingaku"].ToString())).ToString("#,##0");
                                }
                            }
                            #endregion

                            LB_Mitsumori_Code.Text = Session["cMITUMORI"].ToString();  //見積コード
                            TB_Uriagekenmei.Text = Session["sMITUMORI"].ToString();//見積件名
                            LB_skyoten.Text = Session["sKYOTEN1"].ToString(); //拠点

                            tb_jishabangou.Text= Session["cPJ"].ToString(); //自社番号

                            HF_cMituJoutai.Value = Session["cMitumoriJoutai1"].ToString(); //見積状態
                            HF_sMituJoutai.Value = Session["sMitumoriJoutai1"].ToString(); //見積状態
                            updHiddenField.Update();

                            ConstantVal.DB_NAME = db;

                            JC27UriageTouroku_Class meisai_data = new JC27UriageTouroku_Class();
                            dt_data = meisai_data.Mitsu_Meisai_Data(LB_Mitsumori_Code.Text);

                            for (int row = 0; row < dt_data.Rows.Count; row++)
                            {
                                if (dt_data.Rows[row]["cSYOUHIN"].ToString() != "")
                                {
                                    ConstantVal.DB_NAME = db;
                                    JC27UriageTouroku_Class syouhin_kazei = new JC27UriageTouroku_Class();
                                    DataTable dt_fkazei = syouhin_kazei.dt_syouhin_kazei(dt_data.Rows[row]["cSYOUHIN"].ToString());

                                    if (dt_fkazei.Rows.Count > 0)
                                    {
                                        if (dt_fkazei.Rows[0]["fKazei"].ToString() == "1")
                                        {
                                            dt_data.Rows[row]["fKazei"] = "非課税";
                                        }
                                        else
                                        {
                                            dt_data.Rows[row]["fKazei"] = "課税";
                                        }
                                    }
                                    else
                                    {
                                        dt_data.Rows[row]["fKazei"] = "課税";
                                    }
                                }
                                else
                                {
                                    dt_data.Rows[row]["fKazei"] = "課税";
                                }
                            }
                            GV_UriageSyohin.DataSource = dt_data;
                            GV_UriageSyohin.DataBind();

                            ViewState["SyouhinTable"] = dt_data;

                            DateTime dtDeliveryDate = DateTime.Now;
                            LB_Hakkou.Text = dtDeliveryDate.ToString("yyyy/MM/dd");
                            BT_HakkouDate.Style["display"] = "none";
                            divHakkouDate.Style["display"] = "block";
                            LB_Hakkou.Attributes.Add("onClick", "BtnClick('MainContent_BT_HakkouDate')");
                            updHakkouDate.Update();

                            LB_Uriage.Text = dtDeliveryDate.ToString("yyyy/MM/dd");
                            BT_UriageDate.Style["display"] = "none";
                            divUriageDate.Style["display"] = "block";
                            LB_Uriage.Attributes.Add("onClick", "BtnClick('MainContent_BT_UriageDate')");
                            updUriageDate.Update();

                            LB_NyuukinYoutei.Text = dtDeliveryDate.ToString("yyyy/MM/dd");
                            BT_NyuukinYouteiDate.Style["display"] = "none";
                            divNyuukinYouteiDate.Style["display"] = "block";
                            LB_NyuukinYoutei.Attributes.Add("onClick", "BtnClick('MainContent_BT_NyuukinYouteiDate')");
                            updNyuukinYouteiDate.Update();

                            //LB_SeikyusaiDate.Text = dtDeliveryDate.ToString("yyyy/MM/dd");
                            //LB_KoushinDate.Text = dtDeliveryDate.ToString("yyyy/MM/dd");                       

                            Login_Tantou();  //ログイン担当取って表示

                            lblcJISHATANTOUSHA.Text = lblLoginUserCode.Text;
                            lblsJISHATANTOUSHA.Text = lblLoginUserName.Text;
                            divTantousyaBtn.Style["display"] = "none";
                            divTantousyaLabel.Style["display"] = "block";
                            lblsJISHATANTOUSHA.Attributes.Add("onClick", "BtnClick('MainContent_BT_EigyouTantousya_Add')");
                            upd_EIGYOUTANTOUSHA.Update();

                            LB_Uriage_Jyoutai.Text = "作成済"; //売上状態

                            #region 得意先表示
                            LB_cTOKUISAKI.Text = Session["cTokuisaki"].ToString();
                            LB_sTOKUISAKI.Text = Session["sTokuisaki"].ToString();
                            divTokuisakiBtn.Style["display"] = "none";
                            divTokuisakiLabel.Style["display"] = "block";
                            divTokuisakiSyosai.Style["display"] = "block";
                            BT_Tokuisaki.BorderStyle = BorderStyle.None;
                            LB_sTOKUISAKI.Attributes.Add("onClick", "BtnClick('MainContent_BT_Tokuisaki')");
                            updTokuisaki.Update();

                            #region 得意先担当表示
                            if (Session["TOUKUISAKITANTOU"] != null)
                            {
                                if (Session["TOUKUISAKITANTOU"].ToString() != "")
                                {
                                    LB_sTOKUISAKI_TAN.Text = Session["TOUKUISAKITANTOU"].ToString();
                                    LB_sTOKUISAKI_TAN_JUN.Text = Session["TokuisakiTanJun"].ToString();
                                    //ConstantVal.tokuisakinjun = LB_sTOKUISAKI_TAN_JUN.Text;
                                    #region 得意先担当情報取る
                                    ConstantVal.DB_NAME = db;
                                    JC27UriageTouroku_Class UriageTouroku_Class = new JC27UriageTouroku_Class();
                                    UriageTouroku_Class.cTOKUISAKI = LB_cTOKUISAKI.Text;
                                    UriageTouroku_Class.NJUNBAN = LB_sTOKUISAKI_TAN_JUN.Text;
                                    dt_tokuisaki_tantou_jyouhou = new DataTable();
                                    dt_tokuisaki_tantou_jyouhou = UriageTouroku_Class.Tokuisaki_Tantou_Jyohou();
                                    #endregion

                                    #region 得意先担当情報表示
                                    if (dt_tokuisaki_tantou_jyouhou.Rows.Count > 0)
                                    {
                                        LB_Jun.Text = LB_sTOKUISAKI_TAN_JUN.Text;
                                        LB_Yakusyoku.Text = dt_tokuisaki_tantou_jyouhou.Rows[0]["SYAKUSHOKU"].ToString();
                                        LB_Keisyo.Text = dt_tokuisaki_tantou_jyouhou.Rows[0]["SKEISHOU"].ToString();
                                        TB_Tokuisakibumon.Text = dt_tokuisaki_tantou_jyouhou.Rows[0]["SBUMON"].ToString();
                                        updtokuisakibumon.Update();
                                    }
                                    #endregion

                                    divTokuisakiTanBtn.Style["display"] = "none";
                                    divTokuisakiTanLabel.Style["display"] = "block";
                                    divTokuisakiTanSyosai.Style["display"] = "block";
                                    LB_sTOKUISAKI_TAN.Attributes.Add("onClick", "BtnClick('MainContent_BT_TokuisakiTantou')");
                                    updTokuisakiTantou.Update();
                                }
                            }

                            #endregion
                            #endregion

                            #region 請求先表示
                            if (Session["cSeikyusaki"] != null)
                            {
                                if (Session["cSeikyusaki"].ToString() != "")
                                {
                                    LB_cSEIKYUSAKI.Text = Session["cSeikyusaki"].ToString();
                                    LB_sSEIKYUSAKI.Text = Session["sSeikyusaki"].ToString();
                                    divSeikyusakiBtn.Style["display"] = "none";
                                    divSeikyusakiLabel.Style["display"] = "block";
                                    divSeikyusakiSyosai.Style["display"] = "block";
                                    BT_Seikyusaki.BorderStyle = BorderStyle.None;
                                    LB_sSEIKYUSAKI.Attributes.Add("onClick", "BtnClick('MainContent_BT_Seikyusaki')");
                                    updSeikyusaki.Update();
                                }
                            }
                            #region 請求先担当表示
                            if (Session["sSEIKYUSAKI_TAN"] != null)
                            {
                                if (Session["sSEIKYUSAKI_TAN"].ToString() != "")
                                {
                                    LB_sSEIKYUSAKI_TAN.Text = Session["sSEIKYUSAKI_TAN"].ToString();
                                    LB_sSEIKYUSAKI_TAN_JUN.Text = Session["sSEIKYUSAKI_TAN_JUN"].ToString();
                                    //ConstantVal.seikyusakinjun = LB_sSEIKYUSAKI_TAN_JUN.Text;
                                    #region 請求先担当情報取る
                                    ConstantVal.DB_NAME = db;
                                    JC27UriageTouroku_Class UriageTouroku_Class = new JC27UriageTouroku_Class();
                                    UriageTouroku_Class.cTOKUISAKI = LB_cSEIKYUSAKI.Text;
                                    UriageTouroku_Class.NJUNBAN = LB_sSEIKYUSAKI_TAN_JUN.Text;
                                    dt_tokuisaki_tantou_jyouhou = new DataTable();
                                    dt_tokuisaki_tantou_jyouhou = UriageTouroku_Class.Tokuisaki_Tantou_Jyohou();
                                    #endregion

                                    #region 得意先担当情報表示                                
                                    if (dt_tokuisaki_tantou_jyouhou.Rows.Count > 0)
                                    {
                                        LB_Seikyu_JUN.Text = LB_sSEIKYUSAKI_TAN_JUN.Text;
                                        LB_Seikyu_YAKUSYOKU.Text = dt_tokuisaki_tantou_jyouhou.Rows[0]["SYAKUSHOKU"].ToString();
                                        LB_Seikyu_KEISYO.Text = dt_tokuisaki_tantou_jyouhou.Rows[0]["SKEISHOU"].ToString();
                                        TB_Seikyuusakibumon.Text = dt_tokuisaki_tantou_jyouhou.Rows[0]["SBUMON"].ToString();
                                        updtokuisakibumon.Update();
                                    }
                                    #endregion
                                    divSeikyusakiTanBtn.Style["display"] = "none";
                                    divSeikyusakiTanLabel.Style["display"] = "block";
                                    divSeikyusakiTanSyosai.Style["display"] = "block";
                                    LB_sSEIKYUSAKI_TAN.Attributes.Add("onClick", "BtnClick('MainContent_BT_SeikyuTantou')");
                                    updSeikyuTantou.Update();
                                }
                            }

                            ConstantVal.DB_NAME = db;
                            LB_fseikyuukubun.Text = JC27UriageTouroku_Class.Get_fseikyuukubun(LB_cSEIKYUSAKI.Text); //請求コードで請求区分を取る
                            updSeikyuTantou.Update();
                            #endregion
                            #endregion

                            KeiSan_CB_nKINGAKU();

                            Session["fedit"] = "true";
                            Session["gvedit"] = "true";
                            Session.Timeout = 480;
                        }
                        else
                        {
                            Div_shousai.Attributes["style"] = "display:none";
                            Div_settei.Attributes["style"] = "display:block";
                            InsatsuSettei(sender, e); // insatsusettei data
                        }
                        #endregion
                    }
                    else
                    {
                        if (JC99NavBar.insatsusettei == false)
                        {
                            LB_Uriage_Code.Text = Session["cUriage"].ToString();
                            Login_Tantou();  //ログイン担当取って表示
                            DataTable dt_r_uriage = new DataTable();
                            ConstantVal.DB_NAME = db;
                            JC27UriageTouroku_Class r_uriage_data = new JC27UriageTouroku_Class();
                            r_uriage_data.cURIAGE = LB_Uriage_Code.Text;

                            dt_r_uriage = r_uriage_data.UriageData();
                            if (dt_r_uriage.Rows.Count > 0)
                            {
                                LB_Mitsumori_Code.Text = dt_r_uriage.Rows[0]["cMITUMORI"].ToString();//見積コード
                                tb_jishabangou.Text = dt_r_uriage.Rows[0]["cPJ"].ToString();
                                HF_cMituJoutai.Value = dt_r_uriage.Rows[0]["cJYOTAI"].ToString(); //見積状態
                                HF_sMituJoutai.Value = dt_r_uriage.Rows[0]["sJYOTAI"].ToString(); //見積状態
                                updHiddenField.Update();

                                if (dt_r_uriage.Rows[0]["dNYUUKINYOTEI"].ToString() != "")
                                {
                                    LB_NyuukinYoutei.Text = dt_r_uriage.Rows[0]["dNYUUKINYOTEI"].ToString();//入金予定日
                                    BT_NyuukinYouteiDate.Style["display"] = "none";
                                    divNyuukinYouteiDate.Style["display"] = "block";
                                    LB_NyuukinYoutei.Attributes.Add("onClick", "BtnClick('MainContent_BT_NyuukinYouteiDate')");
                                    updNyuukinYouteiDate.Update();
                                }

                                if (dt_r_uriage.Rows[0]["dURIAGE"].ToString() != "")
                                {
                                    LB_Uriage.Text = dt_r_uriage.Rows[0]["dURIAGE"].ToString();  //売上日
                                    BT_UriageDate.Style["display"] = "none";
                                    divUriageDate.Style["display"] = "block";
                                    LB_Uriage.Attributes.Add("onClick", "BtnClick('MainContent_BT_UriageDate')");
                                    updUriageDate.Update();
                                }
                                if (dt_r_uriage.Rows[0]["dDate"].ToString() != "")
                                {
                                    LB_Hakkou.Text = dt_r_uriage.Rows[0]["dDate"].ToString();  //発行日
                                    BT_HakkouDate.Style["display"] = "none";
                                    divHakkouDate.Style["display"] = "block";
                                    LB_Hakkou.Attributes.Add("onClick", "BtnClick('MainContent_BT_HakkouDate')");
                                    updHakkouDate.Update();
                                }

                                TB_Uriagekenmei.Text = dt_r_uriage.Rows[0]["snouhin"].ToString();  //売上件名

                                LB_Uriage_Jyoutai.Text = dt_r_uriage.Rows[0]["cJYOTAI_Uriage"].ToString(); //売上状態

                                LB_skyoten.Text = dt_r_uriage.Rows[0]["sKYOTEN"].ToString(); //拠点

                                cco = dt_r_uriage.Rows[0]["cCO"].ToString(); // cCO

                                LB_cTOKUISAKI.Text = dt_r_uriage.Rows[0]["cTOKUISAKI"].ToString();  //得意先コード

                                LB_sTOKUISAKI.Text = dt_r_uriage.Rows[0]["sTOKUISAKI"].ToString();  //得意先名 
                                divTokuisakiBtn.Style["display"] = "none";
                                divTokuisakiLabel.Style["display"] = "block";
                                divTokuisakiSyosai.Style["display"] = "block";
                                BT_Tokuisaki.BorderStyle = BorderStyle.None;
                                LB_sTOKUISAKI.Attributes.Add("onClick", "BtnClick('MainContent_BT_Tokuisaki')");
                                updTokuisaki.Update();


                                if (dt_r_uriage.Rows[0]["sTOKUISAKI_TAN_JUN"].ToString() != "0")
                                {
                                    LB_sTOKUISAKI_TAN.Text = dt_r_uriage.Rows[0]["sTOKUISAKI_TAN"].ToString();//得意先担当

                                    LB_sTOKUISAKI_TAN_JUN.Text = dt_r_uriage.Rows[0]["sTOKUISAKI_TAN_JUN"].ToString();//得意先担当順番
                                    LB_Jun.Text = LB_sTOKUISAKI_TAN_JUN.Text;

                                    TB_Tokuisakibumon.Text = dt_r_uriage.Rows[0]["sTOKUISAKI_TANBUMON"].ToString();//得意先担当部門

                                    LB_Yakusyoku.Text = dt_r_uriage.Rows[0]["sTOKUISAKI_YAKUSYOKU"].ToString(); //得意先担当役職

                                    LB_Keisyo.Text = dt_r_uriage.Rows[0]["sTOKUISAKI_KEISYO"].ToString();  //得意先敬称

                                    divTokuisakiTanBtn.Style["display"] = "none";
                                    divTokuisakiTanLabel.Style["display"] = "block";
                                    divTokuisakiTanSyosai.Style["display"] = "block";
                                    LB_sTOKUISAKI_TAN.Attributes.Add("onClick", "BtnClick('MainContent_BT_TokuisakiTantou')");
                                    updtokuisakibumon.Update();
                                    updTokuisakiTantou.Update();
                                }

                                LB_cSEIKYUSAKI.Text = dt_r_uriage.Rows[0]["cSEIKYUSAKI"].ToString();  //請求先
                                LB_sSEIKYUSAKI.Text = dt_r_uriage.Rows[0]["sSEIKYUSAKI"].ToString(); //請求先名
                                divSeikyusakiBtn.Style["display"] = "none";
                                divSeikyusakiLabel.Style["display"] = "block";
                                divSeikyusakiSyosai.Style["display"] = "block";

                                BT_Seikyusaki.BorderStyle = BorderStyle.None;
                                LB_sSEIKYUSAKI.Attributes.Add("onClick", "BtnClick('MainContent_BT_Seikyusaki')");
                                updSeikyusaki.Update();


                                if (dt_r_uriage.Rows[0]["sSEIKYU_TAN_Jun"].ToString() != "0")
                                {
                                    LB_sSEIKYUSAKI_TAN.Text = dt_r_uriage.Rows[0]["sSEIKYU_TAN"].ToString(); //請求先担当

                                    LB_sSEIKYUSAKI_TAN_JUN.Text = dt_r_uriage.Rows[0]["sSEIKYU_TAN_Jun"].ToString(); //請求先担当順番
                                    LB_Seikyu_JUN.Text = LB_sSEIKYUSAKI_TAN_JUN.Text;

                                    TB_Seikyuusakibumon.Text = dt_r_uriage.Rows[0]["sSEIKYU_TANBUMON"].ToString(); //請求先担当部門

                                    LB_Seikyu_YAKUSYOKU.Text = dt_r_uriage.Rows[0]["sSEIKYU_YAKUSHOKU"].ToString(); //請求先役職

                                    LB_Seikyu_KEISYO.Text = dt_r_uriage.Rows[0]["sSEIKYU_KEISYO"].ToString(); //請求先敬称

                                    seikyusakinjun = LB_Seikyu_JUN.Text;
                                    divSeikyusakiTanBtn.Style["display"] = "none";
                                    divSeikyusakiTanLabel.Style["display"] = "block";
                                    divSeikyusakiTanSyosai.Style["display"] = "block";
                                    LB_sSEIKYUSAKI_TAN.Attributes.Add("onClick", "BtnClick('MainContent_BT_SeikyuTantou')");
                                    updtokuisakibumon.Update();
                                    updSeikyuTantou.Update();
                                }
                                ConstantVal.DB_NAME = db;
                                LB_fseikyuukubun.Text = JC27UriageTouroku_Class.Get_fseikyuukubun(LB_cSEIKYUSAKI.Text); //請求コードで請求区分を取る
                                updSeikyuTantou.Update();

                                CB_KINGAKU.Text = Decimal.Parse(dt_r_uriage.Rows[0]["nKINGAKU"].ToString()).ToString("#,##0");//合計金額

                                CB_Uriage.Text = Decimal.Parse(dt_r_uriage.Rows[0]["nuriage_kingaku"].ToString()).ToString("#,##0");//合計売上

                                CB_Nebiki.Text = Decimal.Parse(dt_r_uriage.Rows[0]["nnebiki_kingaku"].ToString()).ToString("#,##0");//合計値引

                                CB_Shoukei.Text = Decimal.Parse(dt_r_uriage.Rows[0]["nsyoukei_kingkau"].ToString()).ToString("#,##0");//合計小計

                                CB_Henpin.Text = Decimal.Parse(dt_r_uriage.Rows[0]["nhenpin_kingkau"].ToString()).ToString("#,##0");//合計返品

                                CB_Tatekae.Text = Decimal.Parse(dt_r_uriage.Rows[0]["ntatekae_kingaku"].ToString()).ToString("#,##0");//合計立替

                                CB_Kazei.Text = Decimal.Parse(dt_r_uriage.Rows[0]["nKAZEIKINGAKU"].ToString()).ToString("#,##0");//課税合計

                                CB_Hikazei.Text = Decimal.Parse(dt_r_uriage.Rows[0]["nutihikazei"].ToString()).ToString("#,##0");//非課税合計

                                CB_Syouhizei.Text = Decimal.Parse(dt_r_uriage.Rows[0]["nsyohizei"].ToString()).ToString("#,##0");//消費税

                                LB_Kazei.Text = "（" + Decimal.Parse(dt_r_uriage.Rows[0]["nKAZEIKINGAKU"].ToString()).ToString("#,###") + "）";

                                CB_Jyuchuukingaku.Text = Decimal.Parse(dt_r_uriage.Rows[0]["nURIAGEKINGAKU"].ToString()).ToString("#,##0");//受注金額

                                TB_Denpyoubikou.Text = dt_r_uriage.Rows[0]["sbikou"].ToString();  //伝票備考

                                TB_Shanaimemo.Text = dt_r_uriage.Rows[0]["sMemo"].ToString();  //社内メモ

                                if (!String.IsNullOrEmpty(dt_r_uriage.Rows[0]["cEIGYOTANTOSYA"].ToString()))
                                {
                                    lblcJISHATANTOUSHA.Text = dt_r_uriage.Rows[0]["cEIGYOTANTOSYA"].ToString();  //営業担当者
                                    lblsJISHATANTOUSHA.Text = dt_r_uriage.Rows[0]["sEIGYOTANTOSYA"].ToString();  //営業担当者
                                    preeigyou = lblcJISHATANTOUSHA.Text;

                                    divTantousyaBtn.Style["display"] = "none";
                                    divTantousyaLabel.Style["display"] = "block";
                                    lblsJISHATANTOUSHA.Attributes.Add("onClick", "BtnClick('MainContent_BT_EigyouTantousya_Add')");
                                    upd_EIGYOUTANTOUSHA.Update();
                                }

                                LB_cSakuseisha.Text = dt_r_uriage.Rows[0]["cSAKUSEISYA"].ToString(); //作成者
                                LB_sSakuseisha.Text = dt_r_uriage.Rows[0]["sSAKUSEISYA"].ToString(); //作成者

                                LB_cSaishukoushinsha.Text = dt_r_uriage.Rows[0]["cHENKOUSYA"].ToString();//最後更新者
                                LB_sSaishukoushinsha.Text = dt_r_uriage.Rows[0]["sHENKOUSYA"].ToString();//最後更新者

                                LB_SakuseiDate.Text = dt_r_uriage.Rows[0]["dURIAGESAKUSEI"].ToString(); //作成日

                                LB_KoushinDate.Text = dt_r_uriage.Rows[0]["dHENKOU"].ToString();  //最後更新日


                                CB_NouhinKingaku.Text = (m_j_info.Get_NouhinKingaku(LB_Mitsumori_Code.Text, LB_Uriage_Code.Text)).ToString("#,##0");  //納品金額


                                dt_data = new DataTable();
                                dt_data = r_uriage_data.Uriage_Meisai_Data();
                                //if (dt_data.Rows.Count > 0)
                                //{                               
                                GV_UriageSyohin.DataSource = dt_data;
                                GV_UriageSyohin.DataBind();
                                ViewState["SyouhinTable"] = dt_data;
                                ViewState["PreSyouhinTable"] = GetGridViewData(true);
                                //}
                                KeiSan_CB_nKINGAKU();

                            }
                            Div_shousai.Attributes["style"] = "display:block";
                            Div_settei.Attributes["style"] = "display:none";
                            //if (Session["fcopy"] != null)
                            //{
                            //    if (Session["fcopy"].ToString() == "true")
                            //    {
                            //        Login_Tantou();  //ログイン担当取って表示

                            //        lblcJISHATANTOUSHA.Text = lblLoginUserCode.Text;
                            //        lblsJISHATANTOUSHA.Text = lblLoginUserName.Text;
                            //        divTantousyaBtn.Style["display"] = "none";
                            //        divTantousyaLabel.Style["display"] = "block";
                            //        lblsJISHATANTOUSHA.Attributes.Add("onClick", "BtnClick('MainContent_BT_EigyouTantousya_Add')");
                            //        upd_EIGYOUTANTOUSHA.Update();

                            //        LB_Uriage_Jyoutai.Text = "作成済"; //売上状態
                            //        LB_Uriage_Code.Text = "";
                            //        Save();
                            //        Session["fcopy"] = "false";
                            //    }
                            //}
                            Session["fedit"] = "false";
                            Session["gvedit"] = "false";
                            Session.Timeout = 480;
                        }

                        else
                        {
                            Div_shousai.Attributes["style"] = "display:none";
                            Div_settei.Attributes["style"] = "display:block";
                            InsatsuSettei(sender, e); //insatsusettei data
                        }
                    }

                    if (GV_UriageSyohin.Rows.Count == 0)
                    {
                        BT_Add.Style["display"] = "block";
                    }
                    precTOKUISAKI = LB_cTOKUISAKI.Text;
                    precSEIKYUSAKI = LB_cSEIKYUSAKI.Text;
                    tokuisakinjun = LB_Jun.Text;
                    seikyusakinjun = LB_Seikyu_JUN.Text;
                    hakkoudate = LB_Hakkou.Text;
                    uriagedate = LB_Uriage.Text;
                    nyuukinyouteidate = LB_NyuukinYoutei.Text;
                    LB_Mitsumori_Code.Attributes.Add("onClick", "BtnClick('MainContent_BT_Mitsumori_Code')");
                    //Session["fedit"] = "false";

                }
                else
                {
                    //dt_data = (DataTable)ViewState["SyouhinTable"];
                    //GV_UriageSyohin.DataSource = dt_data;
                }
                valsTOKUISAKI_TAN = LB_sTOKUISAKI_TAN.Text;
                valsSEIKYUSAKI_TAN = LB_sSEIKYUSAKI_TAN.Text;
                curiage = LB_Uriage_Code.Text;
                Session.Timeout = 480;
            }
            else
            {
                Response.Redirect("JC01Login.aspx");
            }
        }
        #endregion

        #region ログイン担当取って表示
        private void Login_Tantou()
        {
            lblLoginUserName.Text = JC99NavBar_Class.Login_Tan_Name;
            lblLoginUserCode.Text = JC99NavBar_Class.Login_Tan_Code;
        }
        #endregion

        #region グリッドビューデータをタベールにセット()
        private DataTable GetGridViewData(bool fsave)
        {
            DataTable dt = CreateSyouhinTableColomn();
            int count = GV_UriageSyohin.Rows.Count;
            foreach (GridViewRow row in GV_UriageSyohin.Rows)
            {
                DropDownList ddl_kubun = (row.FindControl("DDL_Kubun") as DropDownList);
                DropDownList ddl_kazeikubun = (row.FindControl("DDL_KazeiKubun") as DropDownList);
                TextBox txt_cSyohin = (row.FindControl("TB_cSYOHIN") as TextBox);
                TextBox txt_sSyohin = (row.FindControl("TB_sSYOHIN") as TextBox);
                TextBox txt_nSuryo = (row.FindControl("TB_nSURYO") as TextBox);
                //DropDownList ddl_cTani = (row.FindControl("DDL_cTANI") as DropDownList);

                TextBox txt_sTani = (row.FindControl("TB_sTANI") as TextBox);
                TextBox txt_nsikiritanka = (row.FindControl("TB_nSIKIRITANKA") as TextBox);
                Label lbl_nsikirikingaku = (row.FindControl("LB_nSIKIRIKINGAKU") as Label);
                TextBox txt_txtsbikou = (row.FindControl("TB_sbikou") as TextBox);

                txt_sSyohin.BorderColor = System.Drawing.Color.Gray;
                txt_sTani.BorderColor = System.Drawing.Color.Gray;
                txt_txtsbikou.BorderColor = System.Drawing.Color.Gray;
                if (fsave == true)                {                    if (TextUtility.isomojiCharacter(txt_sSyohin.Text))                    {                        f_isomoji_msg = false;                        txt_sSyohin.BorderColor = System.Drawing.Color.Red;                    }                    if (TextUtility.isomojiCharacter(txt_sTani.Text))                    {                        f_isomoji_msg = false;                        txt_sTani.BorderColor = System.Drawing.Color.Red;                    }                    if (TextUtility.isomojiCharacter(txt_txtsbikou.Text))                    {                        f_isomoji_msg = false;                        txt_txtsbikou.BorderColor = System.Drawing.Color.Red;                    }                }

                DataRow dr = dt.NewRow();
                dr[0] = ddl_kubun.SelectedItem;
                dr[1] = ddl_kazeikubun.SelectedItem;
                dr[2] = txt_cSyohin.Text;
                dr[3] = txt_sSyohin.Text;
                dr[4] = txt_nSuryo.Text;
                //dr[5] = ddl_cTani.SelectedValue;
                //dr[5] = ddl_cTani.SelectedItem;
                dr[5] = txt_sTani.Text;
                dr[6] = txt_nsikiritanka.Text;
                dr[7] = lbl_nsikirikingaku.Text;
                dr[8] = txt_txtsbikou.Text;
                
                dt.Rows.Add(dr);
            }
            return dt;
        }
        #endregion

        #region 商品タベール例作成
        private DataTable CreateSyouhinTableColomn()
        {
            DataTable dt_Syohin = new DataTable();
            dt_Syohin.Columns.Add("skubun");
            dt_Syohin.Columns.Add("fKazei");
            dt_Syohin.Columns.Add("cSYOUHIN");
            dt_Syohin.Columns.Add("sSYOUHIN_R");
            dt_Syohin.Columns.Add("nSURYO");
            dt_Syohin.Columns.Add("sTANI");
            dt_Syohin.Columns.Add("nSIKIRITANKA");
            dt_Syohin.Columns.Add("nSIKIRIKINGAKU");
            dt_Syohin.Columns.Add("sbikou");
            return dt_Syohin;
        }
        #endregion


        #region グリッドに商品行削除
        protected void BT_SyohinDelete_Click(object sender, EventArgs e)
        {
            BT_Add.Style["display"] = "none";
            Button btnSyohinDelete = (Button)sender;
            GridViewRow gvRow = (GridViewRow)btnSyohinDelete.NamingContainer;
            int rowID = gvRow.RowIndex;
            if (ViewState["SyouhinTable"] != null)
            {
                dt_data = GetGridViewData(true);
                dt_data.Rows.RemoveAt(rowID);
                ViewState["SyouhinTable"] = dt_data;
                GV_UriageSyohin.DataSource = dt_data;
                GV_UriageSyohin.DataBind();
            }
            if(GV_UriageSyohin.Rows.Count==0)
            {
                BT_Add.Style["display"] = "block";
            }
            KeiSan_CB_nKINGAKU();
            gridviewcheck();
        }
        #endregion

        protected void btnClose_Click(object sender, EventArgs e)
        {
            //ifSentakuPopup.Src = "";
            //mpeSentakuPopup.Hide();
            //updSentakuPopup.Update();
        }

        #region 得意先ボタンクリック
        protected void BT_Tokuisaki_Click(object sender, EventArgs e)
        {
            SessionUtility.SetSession("HOME", "Master");
            //Session["flag"] = "uriage";
            
            #region 20220120 MiMi Deleted
            //ifSentakuPopup.Style["width"] = "1300px";  
            //ifSentakuPopup.Style["height"] = "675px";
            //ifSentakuPopup.Src = "JC18TokuisakiKensaku.aspx";
            //mpeSentakuPopup.Show();
            #endregion

            Session["ftokuisakisyosai"] = "0";  //20220120 MiMi Added
            Session.Timeout = 480;

            String id = Request.QueryString["id"];
            ifShinkiPopup.Src = "JC18TokuisakiKensaku.aspx?id=" + id;  //20220120 MiMi Added
            mpeShinkiPopup.Show(); //20220120 MiMi Added

            LB_sTOKUISAKI.Attributes.Add("onClick", "BtnClick('MainContent_BT_Tokuisaki')");
            //updSentakuPopup.Update();
            updShinkiPopup.Update();   //20220120 MiMi Added
            //ConstantVal.precTOKUISAKI = LB_cTOKUISAKI.Text;
        }
        #endregion

        #region 請求先ボタンクリック
        protected void BT_Seikyuusaki_Click(object sender, EventArgs e)
        {
            ConstantVal.check_ts = true;
            SessionUtility.SetSession("HOME", "Master");
            //Session["flag"] = "uriage";

            #region 20220120 MiMi Deleted
            //ifSentakuPopup.Style["width"] = "1300px";
            //ifSentakuPopup.Style["height"] = "675px";
            //ifSentakuPopup.Src = "JC18TokuisakiKensaku.aspx";
            //mpeSentakuPopup.Show();
            #endregion

            Session["ftokuisakisyosai"] = "0";  //20220120 MiMi Added
            Session.Timeout = 480;

            String id = Request.QueryString["id"];
            ifShinkiPopup.Src = "JC18TokuisakiKensaku.aspx?id=" + id;  //20220120 MiMi Added
            mpeShinkiPopup.Show(); //20220120 MiMi Added

            LB_sSEIKYUSAKI.Attributes.Add("onClick", "BtnClick('MainContent_BT_Seikyusaki')");
            //updSentakuPopup.Update();
            updShinkiPopup.Update();   //20220120 MiMi Added
            //ConstantVal.precSEIKYUSAKI = LB_cSEIKYUSAKI.Text;
        }
        #endregion
        protected void btnKyotenSelect_Click(object sender, EventArgs e)
        {

        }

        protected void btnshiarailistSelect_Click(object sender, EventArgs e)
        {
        }

        protected void btnYukoKigenListSelect_Click(object sender, EventArgs e)
        {

        }

        #region 得意先選択
        protected void btnTokuisakiSelect_Click(object sender, EventArgs e)
        {
            if (Session["cTOUKUISAKI"] != null)
            {
                string ctokuisaki = (string)Session["cTOUKUISAKI"];
                string stokuisaki = (string)Session["sTOUKUISAKI"];
                LB_cTOKUISAKI.Text = ctokuisaki;　　//得意先コード
                LB_sTOKUISAKI.Text = stokuisaki;　　//得意先名
                divTokuisakiBtn.Style["display"] = "none";
                divTokuisakiLabel.Style["display"] = "block";
                divTokuisakiSyosai.Style["display"] = "block";
                BT_Tokuisaki.BorderStyle = BorderStyle.None;
                updTokuisaki.Update();
                BT_sTOKUISAKI_TAN_Cross_Click1(sender, e);
                if (valsTOKUISAKI_TAN != "")
                {
                    Session["fedit"] = "true";
                }
                else
                {
                    if (precTOKUISAKI != ctokuisaki)
                    {
                        Session["fedit"] = "true";
                    }
                    else
                    {
                        Session["fedit"] = "false";
                    }
                }
            }
            Session.Timeout = 480;


            //ifSentakuPopup.Src = "";
            //mpeSentakuPopup.Hide();
            //updSentakuPopup.Update();

            //20220120 MiMi Added
            ifShinkiPopup.Src = "";
            mpeShinkiPopup.Hide();
            updShinkiPopup.Update();
        }
        #endregion

        #region 請求先選択
        protected void btnSeikyusakiSelect_Click(object sender, EventArgs e)
        {
            if (Session["cTOUKUISAKI"] != null)
            {
                string ctokuisaki = (string)Session["cTOUKUISAKI"];
                string stokuisaki = (string)Session["sTOUKUISAKI"];
                LB_cSEIKYUSAKI.Text = ctokuisaki;　　//請求先コード
                LB_sSEIKYUSAKI.Text = stokuisaki;   //請求先名
                divSeikyusakiBtn.Style["display"] = "none";
                divSeikyusakiLabel.Style["display"] = "block";
                divSeikyusakiSyosai.Style["display"] = "block";
                BT_Seikyusaki.BorderStyle = BorderStyle.None;
                updSeikyusaki.Update();
                BT_SEIKYUSAKI_TAN_Cross_Click1(sender, e);
                if (valsSEIKYUSAKI_TAN != "")
                {
                    Session["fedit"] = "true";
                }
                else
                {
                    if (precSEIKYUSAKI != ctokuisaki)
                    {
                        Session["fedit"] = "true";
                    }
                    else
                    {
                        Session["fedit"] = "false";
                    }
                }
                Session.Timeout = 480;
                String id = Request.QueryString["id"];
                DataTable dt_db = JC01Login_Class.Get_DB(id);
                String db = dt_db.Rows[0]["db"].ToString();
                String mail = dt_db.Rows[0]["mail"].ToString();
                ConstantVal.DB_NAME = db;
                LB_fseikyuukubun.Text = JC27UriageTouroku_Class.Get_fseikyuukubun(LB_cSEIKYUSAKI.Text); //請求先コードで請求区分を取る
                updSeikyuTantou.Update();
                Calculate_Zei();
                pudgoukei.Update();
                CB_KINGAKU.Text = (Convert.ToDecimal(CB_Shoukei.Text) + Convert.ToDecimal(CB_Syouhizei.Text)).ToString("#,##0");
                pudgoukei.Update();
            }

            //ifSentakuPopup.Src = "";
            //mpeSentakuPopup.Hide();
            //updSentakuPopup.Update();

            //20220120 MiMi Added
            ifShinkiPopup.Src = "";
            mpeShinkiPopup.Hide();
            updShinkiPopup.Update();
        }
        #endregion

        #region 得意先詳細ボタンクリック
        protected void BT_sTOKUISAKI_Syousai_Click(object sender, EventArgs e)
        {
            Session["cTokuisakiBukken"] = LB_cTOKUISAKI.Text;
            Session.Timeout = 480;
            //Response.Redirect("JC19TokuisakiSyousai.aspx");
            if (!String.IsNullOrEmpty(Request.QueryString["id"]))
            {
                String id = Request.QueryString["id"];
                Response.Write("<script language='javascript'>window.open('JC19TokuisakiSyousai.aspx?id=" + id + "', '_blank');</script>");
            }
            else
            {
                Response.Redirect("JC01Login.aspx");
            }
        }
        #endregion

        #region 請求先詳細ボタンクリック
        protected void BT_sSEIKYUSAKI_Syousai_Click(object sender, EventArgs e)
        {
            Session["cTokuisakiBukken"] = LB_cSEIKYUSAKI.Text;
            Session.Timeout = 480;
            //Response.Redirect("JC19TokuisakiSyousai.aspx");
            if (!String.IsNullOrEmpty(Request.QueryString["id"]))
            {
                String id = Request.QueryString["id"];
                Response.Write("<script language='javascript'>window.open('JC19TokuisakiSyousai.aspx?id=" + id + "', '_blank');</script>");
            }
            else
            {
                Response.Redirect("JC01Login.aspx");
            }
        }
         #endregion

        #region 得意先担当選択
        protected void btnTokuisakiTantouSelect_Click(object sender, EventArgs e)
        {
            if (Session["TOUKUISAKITANTOU"] != null)
            {
                string tokuisakitantou = (string)Session["TOUKUISAKITANTOU"];
                string njun = (string)Session["TokuisakiTanJun"];
                string yakushoku = (string)Session["SYAKUSHOKU"];
                string keishou = (string)Session["sKEISHOU"];
                LB_sTOKUISAKI_TAN.Text = tokuisakitantou;  //得意先担当
                LB_sTOKUISAKI_TAN_JUN.Text = njun;　　　　//得意先順番
                LB_Jun.Text = njun;　　　　//得意先順番
                LB_Yakusyoku.Text = yakushoku;　　//得意先役職
                LB_Keisyo.Text = keishou;　　//得意先敬称
                divTokuisakiTanBtn.Style["display"] = "none";
                divTokuisakiTanLabel.Style["display"] = "block";
                divTokuisakiTanSyosai.Style["display"] = "block";
                updTokuisakiTantou.Update();

                string bumon = (string)Session["SBUMON"];
                TB_Tokuisakibumon.Text = bumon;
                updtokuisakibumon.Update();
                //if (tokuisakinjun != njun)
                //{
                    Session["fedit"] = "true";
                Session.Timeout = 480;
                //}
                //else
                //{
                //    Session["fedit"] = "false";
                //}

                //BT_Save.Enabled = true;
                //HF_isChange.Value = "1";
                //updHeader.Update();
            }

            //ifSentakuPopup.Src = "";
            //mpeSentakuPopup.Hide();
            //updSentakuPopup.Update();

            //20220120 MiMi Added
            ifShinkiPopup.Src = "";
            mpeShinkiPopup.Hide();
            updShinkiPopup.Update();

        }
        #endregion

        #region 請求先担当選択
        protected void btnSeikyusakiTantouSelect_Click(object sender, EventArgs e)
        {
            if (Session["TOUKUISAKITANTOU"] != null)
            {
                string tokuisakitantou = (string)Session["TOUKUISAKITANTOU"];
                string njun = (string)Session["TokuisakiTanJun"];
                LB_sSEIKYUSAKI_TAN.Text = tokuisakitantou;　　//請求先担当
                LB_sSEIKYUSAKI_TAN_JUN.Text = njun;　　　//請求先順番
                LB_Seikyu_JUN.Text = njun;
                string yakushoku = (string)Session["SYAKUSHOKU"];　　//請求先役職
                string keishou = (string)Session["sKEISHOU"];　　//請求先敬称
                LB_Seikyu_YAKUSYOKU.Text = yakushoku;　//請求先役職
                LB_Seikyu_KEISYO.Text = keishou;　　　//請求先敬称
                divSeikyusakiTanBtn.Style["display"] = "none";
                divSeikyusakiTanLabel.Style["display"] = "block";
                divSeikyusakiTanSyosai.Style["display"] = "block";
                updSeikyuTantou.Update();

                string bumon = (string)Session["SBUMON"];
                TB_Seikyuusakibumon.Text = bumon;
                updseikyusakibumon.Update();
                //if (seikyusakinjun != njun)
                //{
                    Session["fedit"] = "true";
                Session.Timeout = 480;
                //}
                //else
                //{
                //    Session["fedit"] = "false";
                //}

                //BT_Save.Enabled = true;
                //HF_isChange.Value = "1";
                //updHeader.Update();
            }

            //ifSentakuPopup.Src = "";
            //mpeSentakuPopup.Hide();
            //updSentakuPopup.Update();

            //20220120 MiMi Added
            ifShinkiPopup.Src = "";
            mpeShinkiPopup.Hide();
            updShinkiPopup.Update();
        }
        #endregion

        #region 得意先担当ボタンクリック
        protected void BT_TokuisakiTantou_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(LB_cTOKUISAKI.Text))
            {
                SessionUtility.SetSession("HOME", "Master");
                //Session["flag"] = "uriage";
                Session["cTokuisakiBukken"] = LB_cTOKUISAKI.Text;
                Session.Timeout = 480;

                #region 20220120 MiMi Deleted
                //ifSentakuPopup.Style["width"] = "1100px";
                //ifSentakuPopup.Style["height"] = "650px";
                //ifSentakuPopup.Src = "JC20TokuisakiTantouKensaku.aspx";
                //mpeSentakuPopup.Show();
                #endregion

                String id = Request.QueryString["id"];
                ifShinkiPopup.Src = "JC20TokuisakiTantouKensaku.aspx?id=" + id; //20220120 MiMi Added
                mpeShinkiPopup.Show(); //20220120 MiMi Added

                LB_sTOKUISAKI_TAN.Attributes.Add("onClick", "BtnClick('MainContent_BT_TokuisakiTantou')");
                //updSentakuPopup.Update();
                updShinkiPopup.Update(); //20220120 MiMi Added
                //ConstantVal.tokuisakinjun = LB_Jun.Text;
            }
        }
        #endregion

        #region 請求先担当ボタンクリック
        protected void BT_SeikyuTantou_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(LB_cSEIKYUSAKI.Text))
            {
                ConstantVal.check_ts = true;
                SessionUtility.SetSession("HOME", "Master");
                //Session["flag"] = "uriage";
                Session["cTokuisakiBukken"] = LB_cSEIKYUSAKI.Text;
                Session.Timeout = 480;

                #region 20220120 MiMi Deleted
                //ifSentakuPopup.Style["width"] = "1100px";
                //ifSentakuPopup.Style["height"] = "650px";
                //ifSentakuPopup.Src = "JC20TokuisakiTantouKensaku.aspx";
                //mpeSentakuPopup.Show();
                #endregion

                String id = Request.QueryString["id"];
                ifShinkiPopup.Src = "JC20TokuisakiTantouKensaku.aspx?id=" + id; //20220120 MiMi Added
                mpeShinkiPopup.Show(); //20220120 MiMi Added

                LB_sSEIKYUSAKI_TAN.Attributes.Add("onClick", "BtnClick('MainContent_BT_SeikyuTantou')");
                //updSentakuPopup.Update();
                updShinkiPopup.Update(); //20220120 MiMi Added
                //ConstantVal.seikyusakinjun = LB_Seikyu_JUN.Text;
            }
        }
        #endregion

        #region 得意先担当詳細ボタンクリック
        protected void BT_sTOKUISAKI_TAN_Syousai_Click(object sender, EventArgs e)
        {
            Session["cTokuisakiBukken"] = LB_cTOKUISAKI.Text;
            Session.Timeout = 480;
            //Response.Redirect("JC19TokuisakiSyousai.aspx");
            if (!String.IsNullOrEmpty(Request.QueryString["id"]))
            {
                String id = Request.QueryString["id"];
                Response.Write("<script language='javascript'>window.open('JC19TokuisakiSyousai.aspx?id=" + id + "', '_blank');</script>");
            }
            else
            {
                Response.Redirect("JC01Login.aspx");
            }
        }
        #endregion

        #region 請求先担当詳細ボタンクリック
        protected void BT_sSEIKYUSAKI_TAN_Syousai_Click(object sender, EventArgs e)
        {
            Session["cTokuisakiBukken"] = LB_cSEIKYUSAKI.Text;
            Session.Timeout = 480;
            //Response.Redirect("JC19TokuisakiSyousai.aspx");
            if (!String.IsNullOrEmpty(Request.QueryString["id"]))
            {
                String id = Request.QueryString["id"];
                Response.Write("<script language='javascript'>window.open('JC19TokuisakiSyousai.aspx?id=" + id + "', '_blank');</script>");
            }
            else
            {
                Response.Redirect("JC01Login.aspx");
            }
        }
        #endregion

        #region BT_HakkouDate_Click
        protected void BT_HakkouDate_Click(object sender, EventArgs e)
        {
            SessionUtility.SetSession("HOME", "Master");
            //ifDatePopup.Style["width"] = "300px";
            //ifDatePopup.Style["height"] = "370px";
            //ifDatePopup.Src = "JCHidukeSelect.aspx";
            //mpeDatePopup.Show();
            String id = Request.QueryString["id"];
            ifdatePopup.Src = "JCHidukeSelect.aspx?id=" + id;
            mpedatePopup.Show();

            ViewState["DATETIME"] = BT_HakkouDate.ID;

            if (!String.IsNullOrEmpty(LB_Hakkou.Text))
            {
                DateTime dt = DateTime.Parse(LB_Hakkou.Text);
                Session["CALENDARDATE"] = dt.ToString("yyyy/MM/dd");
                Session["YEAR"] = dt.Year;
                Session.Timeout = 480;
            }

            // 設定したラベルをクリック時、時間設定ポップアップ画面を表示する
            LB_Hakkou.Attributes.Add("onClick", "BtnClick('MainContent_BT_HakkouDate')");
            upddatePopup.Update();
        }
        #endregion

        #region BT_dHakkouCross_Click
        protected void BT_dHakkouCross_Click(object sender, EventArgs e)
        {
            LB_Hakkou.Text = "";
            BT_HakkouDate.Style["display"] = "block";
            divHakkouDate.Style["display"] = "none";
            updHakkouDate.Update();
            
            if(hakkoudate != LB_Hakkou.Text)
            {
                Session["fedit"] = "true";
            }
            else
            {
                Session["fedit"] = "false";
            }
            Session.Timeout = 480;

        }
        #endregion
       
        #region "日付カレンダーポップアップの【X】ボタンクリック処理"
        protected void btnCalendarClose_Click(object sender, EventArgs e)
        {
            // 【日付サブ画面】を閉じる
            CloseDateSub();
            // フォーカスする
            CalendarFoucs();
        }
        #endregion

        #region "選択サブ画面を閉じる処理"
        protected void CloseDateSub()
        {
            ifdatePopup.Src = "";            mpedatePopup.Hide();            upddatePopup.Update();
        }
        #endregion

        #region "日付サブ画面を閉じる時のフォーカス処理"
        protected void CalendarFoucs()
        {
            string strBtnID = (string)ViewState["DATETIME"];

            if (strBtnID == BT_HakkouDate.ID)
            {
                if (BT_HakkouDate.Style["display"] != "none")
                {
                    BT_HakkouDate.Focus();
                }
            }
            else if (strBtnID == BT_UriageDate.ID)
            {
                if (BT_UriageDate.Style["display"] != "none")
                {
                    BT_UriageDate.Focus();
                }
                
            }
            else if (strBtnID == BT_NyuukinYouteiDate.ID)
            {
                if (BT_NyuukinYouteiDate.Style["display"] != "none")
                {
                    BT_NyuukinYouteiDate.Focus();
                }
            }

        }
        #endregion

        #region "日付カレンダーポップアップの【設定】ボタンを押す処理"
        protected void btnCalendarSettei_Click(object sender, EventArgs e)
        {
            DateTime dtSelectedDate;

            // 【日付サブ画面】を閉じる
            CloseDateSub();
            string strBtnID = (string)ViewState["DATETIME"];
            string strCalendarDateTime = (string)Session["CALENDARDATETIME"];
            if (Session["CALENDARDATETIME"] != null)
            {
                strCalendarDateTime = (string)Session["CALENDARDATETIME"];
                dtSelectedDate = DateTime.Parse(strCalendarDateTime);
                if (strBtnID == BT_HakkouDate.ID)
                {
                    HakkouDateDataBind(strCalendarDateTime, strBtnID);
                }
                else if (strBtnID == BT_UriageDate.ID)
                {
                    UriageDateDataBind(strCalendarDateTime, strBtnID);
                    Calculate_Zei();
                    pudgoukei.Update();
                    CB_KINGAKU.Text = (Convert.ToDecimal(CB_Shoukei.Text) + Convert.ToDecimal(CB_Syouhizei.Text)).ToString("#,##0");
                    pudgoukei.Update();
                }
                else if (strBtnID == BT_NyuukinYouteiDate.ID)
                {
                    NyuukinYouteiDateDataBind(strCalendarDateTime, strBtnID);
                }
                //lblHdnAnkenTextChange.Text = "true";
            }
            CalendarFoucs();

        }
        #endregion

        #region "発行日データバインディング処理"
        protected void HakkouDateDataBind(string strCalendarDateTime, string strImgbtnID)
        {
            DateTime dtDeliveryDate = DateTime.Parse(strCalendarDateTime);
            LB_Hakkou.Text = dtDeliveryDate.ToString("yyyy/MM/dd");
            BT_HakkouDate.Style["display"] = "none";
            divHakkouDate.Style["display"] = "block";
            updHakkouDate.Update();
            if(hakkoudate != strCalendarDateTime)
            {
                Session["fedit"] = "true";
            }
            else
            {
                Session["fedit"] = "false";
            }
            Session.Timeout = 480;
        }
        #endregion

        #region "売上日データバインディング処理"
        protected void UriageDateDataBind(string strCalendarDateTime, string strImgbtnID)
        {
            DateTime dtDeliveryDate = DateTime.Parse(strCalendarDateTime);
            LB_Uriage.Text = dtDeliveryDate.ToString("yyyy/MM/dd");
            BT_UriageDate.Style["display"] = "none";
            divUriageDate.Style["display"] = "block";
            updUriageDate.Update();
            if (uriagedate != strCalendarDateTime)
            {
                Session["fedit"] = "true";
            }
            else
            {
                Session["fedit"] = "false";
            }
            Session.Timeout = 480;
        }
        #endregion

        #region "納期データバインディング処理"
        protected void NyuukinYouteiDateDataBind(string strCalendarDateTime, string strImgbtnID)
        {
            DateTime dtDeliveryDate = DateTime.Parse(strCalendarDateTime);
            LB_NyuukinYoutei.Text = dtDeliveryDate.ToString("yyyy/MM/dd");
            BT_NyuukinYouteiDate.Style["display"] = "none";
            divNyuukinYouteiDate.Style["display"] = "block";
            updNyuukinYouteiDate.Update();
            if (nyuukinyouteidate != strCalendarDateTime)
            {
                Session["fedit"] = "true";
            }
            else
            {
                Session["fedit"] = "false";
            }
            Session.Timeout = 480;
        }
        #endregion

        #region BT_UriageDate_Click
        protected void BT_UriageDate_Click(object sender, EventArgs e)
        {
            SessionUtility.SetSession("HOME", "Master");
            //ifDatePopup.Style["width"] = "300px";
            //ifDatePopup.Style["height"] = "370px";
            //ifDatePopup.Src = "JCHidukeSelect.aspx";
            //mpeDatePopup.Show();
            String id = Request.QueryString["id"];
            ifdatePopup.Src = "JCHidukeSelect.aspx?id=" + id;
            mpedatePopup.Show();

            ViewState["DATETIME"] = BT_UriageDate.ID;

            if (!String.IsNullOrEmpty(LB_Uriage.Text))
            {
                DateTime dt = DateTime.Parse(LB_Uriage.Text);
                Session["CALENDARDATE"] = dt.ToString("yyyy/MM/dd");
                Session["YEAR"] = dt.Year;
                Session.Timeout = 480;
            }

            // 設定したラベルをクリック時、時間設定ポップアップ画面を表示する
            LB_Uriage.Attributes.Add("onClick", "BtnClick('MainContent_BT_UriageDate')");
            upddatePopup.Update();
        }
         #endregion
        
        #region BT_dUriageCross_Click
        protected void BT_dUriageCross_Click(object sender, EventArgs e)
        {
            LB_Uriage.Text = "";
            BT_UriageDate.Style["display"] = "block";
            divUriageDate.Style["display"] = "none";
            updUriageDate.Update();

            if(uriagedate != LB_Uriage.Text)
            {
                Session["fedit"] = "true";
            }
            else
            {
                Session["fedit"] = "false";
            }
            Session.Timeout = 480;
            Calculate_Zei();            pudgoukei.Update();            CB_KINGAKU.Text = (Convert.ToDecimal(CB_Shoukei.Text) + Convert.ToDecimal(CB_Syouhizei.Text)).ToString("#,##0");            pudgoukei.Update();

        }
        #endregion
        
        #region 納期を選択
        protected void BT_NyuukinYouteiDate_Click(object sender, EventArgs e)
        {
            SessionUtility.SetSession("HOME", "Master");
            //ifDatePopup.Style["width"] = "300px";
            //ifDatePopup.Style["height"] = "370px";
            //ifDatePopup.Src = "JCHidukeSelect.aspx";
            //mpeDatePopup.Show();
            String id = Request.QueryString["id"];
            ifdatePopup.Src = "JCHidukeSelect.aspx?id=" + id;
            mpedatePopup.Show();

            ViewState["DATETIME"] = BT_NyuukinYouteiDate.ID;

            if (!String.IsNullOrEmpty(LB_NyuukinYoutei.Text))
            {
                DateTime dt = DateTime.Parse(LB_NyuukinYoutei.Text);
                Session["CALENDARDATE"] = dt.ToString("yyyy/MM/dd");
                Session["YEAR"] = dt.Year;
                Session.Timeout = 480;
            }

            // 設定したラベルをクリック時、時間設定ポップアップ画面を表示する
            LB_NyuukinYoutei.Attributes.Add("onClick", "BtnClick('MainContent_BT_NyuukinYouteiDate')");
            upddatePopup.Update();
            //ConstantVal.nyuukinyouteidate = LB_NyuukinYoutei.Text;
        }
        #endregion
        
        #region BT_dNyuukinYouteiCross_Click
        protected void BT_dNyuukinYouteiCross_Click(object sender, EventArgs e)
        {
            LB_NyuukinYoutei.Text = "";
            BT_NyuukinYouteiDate.Style["display"] = "block";
            divNyuukinYouteiDate.Style["display"] = "none";
            updNyuukinYouteiDate.Update();

            if(nyuukinyouteidate != LB_NyuukinYoutei.Text)
            {
                Session["fedit"] = "true";
            }
            else
            {
                Session["fedit"] = "false";
            }
            
        }
        #endregion

        #region BT_sTOKUISAKI_TAN_Cross_Click1
        protected void BT_sTOKUISAKI_TAN_Cross_Click1(object sender, EventArgs e)
        {
            LB_sTOKUISAKI_TAN.Text = "";
            LB_sTOKUISAKI_TAN_JUN.Text = "";
            LB_Jun.Text = "";
            LB_Yakusyoku.Text = "";
            LB_Keisyo.Text = "";

            divTokuisakiTanBtn.Style["display"] = "block";
            divTokuisakiTanLabel.Style["display"] = "none";
            divTokuisakiTanSyosai.Style["display"] = "none";
            updTokuisakiTantou.Update();
            TB_Tokuisakibumon.Text = "";
            updtokuisakibumon.Update();

            if(tokuisakinjun != LB_Jun.Text)
            {
                Session["fedit"] = "true";
            }
            else
            {
                Session["fedit"] = "false";
            }
            
            //updHeader.Update();
        }
        #endregion

        #region BT_SEIKYUSAKI_TAN_Cross_Click1
        protected void BT_SEIKYUSAKI_TAN_Cross_Click1(object sender, EventArgs e)
        {
            LB_sSEIKYUSAKI_TAN.Text = "";
            LB_sSEIKYUSAKI_TAN_JUN.Text = "";
            LB_Seikyu_YAKUSYOKU.Text = "";
            LB_Seikyu_KEISYO.Text = "";
            LB_Seikyu_JUN.Text = "";

            divSeikyusakiTanBtn.Style["display"] = "block";
            divSeikyusakiTanLabel.Style["display"] = "none";
            divSeikyusakiTanSyosai.Style["display"] = "none";
            updSeikyuTantou.Update();
            TB_Seikyuusakibumon.Text = "";
            updseikyusakibumon.Update();

            if(seikyusakinjun != LB_Seikyu_JUN.Text)
            {
                Session["fedit"] = "true";
            }
            else
            {
                Session["fedit"] = "false";
            }
            
            //updHeader.Update();
        }
        #endregion

        #region BT_EigyouTantousya_Add_Click
        protected void BT_EigyouTantousya_Add_Click(object sender, EventArgs e)
        {
            SessionUtility.SetSession("HOME", "Master");
            Session["isKensaku"] = "false"; //担当者
            Session["isTantouOrUser"] = "0"; //担当者
            Session.Timeout = 480;
            ifSentakuPopup.Style["width"] = "700px";
            ifSentakuPopup.Style["height"] = "650px";

            String id = Request.QueryString["id"];
            ifSentakuPopup.Src = "JC14TantouKensaku.aspx?id=" + id;
            mpeSentakuPopup.Show();

            lblsJISHATANTOUSHA.Attributes.Add("onClick", "BtnClick('MainContent_BT_EigyouTantousya_Add')");
            updSentakuPopup.Update();

        }
        #endregion

        #region BT_sEIGYOUTANTOUSHA_Cross_Click
        protected void BT_sEIGYOUTANTOUSHA_Cross_Click(object sender, EventArgs e)
        {
            lblsJISHATANTOUSHA.Text = "";
            lblcJISHATANTOUSHA.Text = "";
            divTantousyaBtn.Style["display"] = "block";
            divTantousyaLabel.Style["display"] = "none";
            //BT_Save.Enabled = true;
            //HF_isChange.Value = "1";
            //updHeader.Update();
            Session["fedit"] = "true";
            Session.Timeout = 480;
        }
        #endregion

        #region btnJishaTantouSelect_Click
        protected void btnJishaTantouSelect_Click(object sender, EventArgs e)
        {
            if (Session["JISHAcTANTOUSHA"] != null)
            {
                string ctantou = (string)Session["JISHAcTANTOUSHA"];
                string stantou = (string)Session["JISHAsTANTOUSHA"];
                lblcJISHATANTOUSHA.Text = ctantou;
                lblsJISHATANTOUSHA.Text = stantou;
                if (ctantou == "")
                {
                    divTantousyaBtn.Style["display"] = "block";
                    divTantousyaLabel.Style["display"] = "none";
                    upd_EIGYOUTANTOUSHA.Update();
                }
                else
                {
                    divTantousyaBtn.Style["display"] = "none";
                    divTantousyaLabel.Style["display"] = "block";
                    upd_EIGYOUTANTOUSHA.Update();
                }
                
                if(preeigyou != lblcJISHATANTOUSHA.Text)
                {
                    Session["fedit"] = "true";
                }
                else
                {
                    Session["fedit"] = "false";
                }
                Session.Timeout = 480;

                //divTantousyaBtn.Style["display"] = "none";
                //divTantousyaLabel.Style["display"] = "block";
                //upd_EIGYOUTANTOUSHA.Update();

                //BT_Save.Enabled = true;
                //HF_isChange.Value = "1";
                //updHeader.Update();
            }

            ifSentakuPopup.Src = "";
            mpeSentakuPopup.Hide();
            updSentakuPopup.Update();
            //Session["fedit"] = "true";
        }


        #endregion

        #region BT_Hidzukeari_Click        protected void BT_Hidzukeari_Click(object sender, EventArgs e)        {            BT_Hidzukeari.CssClass = "JC10ZeikomiBtnActive";            BT_Hidzukearinashi.CssClass = "JC10ZeikomiBtn";

        }
        #endregion
        #region BT_Hidzukearinashi_Click        protected void BT_Hidzukearinashi_Click(object sender, EventArgs e)        {            BT_Hidzukearinashi.CssClass = "JC10ZeikomiBtnActive";            BT_Hidzukeari.CssClass = "JC10ZeikomiBtn";

        }
        #endregion
        #region BT_kenseikyuusho_Click        protected void BT_kenseikyuusho_Click(object sender, EventArgs e)        {            BT_kenseikyuusho.CssClass = "JC10ZeikomiBtnActive";            BT_Nouhinsho.CssClass = "JC10ZeikomiBtn";        }
        #endregion
        #region BT_Nouhinsho_Click        protected void BT_Nouhinsho_Click(object sender, EventArgs e)        {            BT_Nouhinsho.CssClass = "JC10ZeikomiBtnActive";            BT_kenseikyuusho.CssClass = "JC10ZeikomiBtn";        }
        #endregion
        #region BT_Kingakuari_Click        protected void BT_Kingakuari_Click(object sender, EventArgs e)        {            BT_Kingakuari.CssClass = "JC10ZeikomiBtnActive";            BT_Kingakunashi.CssClass = "JC10ZeikomiBtn";

        }
        #endregion
        #region BT_Kingakunashi_Click        protected void BT_Kingakunashi_Click(object sender, EventArgs e)        {            BT_Kingakunashi.CssClass = "JC10ZeikomiBtnActive";            BT_Kingakuari.CssClass = "JC10ZeikomiBtn";        }
        #endregion

        #region "グリッドに商品行追加"
        protected void BT_SyouhinAdd_Click(object sender, EventArgs e)
        {
            Button btn_addSyohin = (Button)sender;
            GridViewRow gvRow = (GridViewRow)btn_addSyohin.NamingContainer;
            int rowID = gvRow.RowIndex + 1;
            if (ViewState["SyouhinTable"] != null)
            {
                dt_data = GetGridViewData(true);
                if (dt_data.Rows.Count > 0)
                {
                    DataRow dr = dt_data.NewRow();
                    dr[0] = "";
                    dr[1] = "";
                    dr[2] = "";
                    dr[3] = "";
                    dr[4] = "";
                    dr[5] = "";
                    dr[6] = "";
                    dr[7] = "";
                    dr[8] = "";

                    dt_data.Rows.InsertAt(dr, rowID);
                }

                ViewState["SyouhinTable"] = dt_data;
                GV_UriageSyohin.DataSource = dt_data;
                GV_UriageSyohin.DataBind();
                updUriageSyohinGrid.Update();
            }
            KeiSan_CB_nKINGAKU();
            gridviewcheck();
            //Session["fedit"] = "true";
        }
        #endregion

        #region BT_SyohinCopy_Click
        protected void BT_SyohinCopy_Click(object sender, EventArgs e)
        {
            Button btnSyohinCopy = (Button)sender;
            GridViewRow gvRow = (GridViewRow)btnSyohinCopy.NamingContainer;
            int rowID = gvRow.RowIndex + 1;
            if (ViewState["SyouhinTable"] != null)
            {
                dt_data = GetGridViewData(true);
                var dr_copy = dt_data.NewRow();
                DataRow dr_exist = dt_data.Rows[gvRow.RowIndex];
                dr_copy.ItemArray = dr_exist.ItemArray.Clone() as object[];
                dt_data.Rows.InsertAt(dr_copy, rowID);
                
                ViewState["SyouhinTable"] = dt_data;
                GV_UriageSyohin.DataSource = dt_data;
                GV_UriageSyohin.DataBind();
                updUriageSyohinGrid.Update();

            }

            KeiSan_CB_nKINGAKU();
            gridviewcheck();

            // Session["fedit"] = "true";
        }
        #endregion

        #region GV_UriageSyohin_RowDataBound
        protected void GV_UriageSyohin_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowIndex >= 0)
            {
                #region ドロップダウンリストセット
                DropDownList kubundropdown = (e.Row.FindControl("DDL_Kubun") as DropDownList);　//区分
                kubundropdown.Items.Add("売上");
                kubundropdown.Items.Add("返品");
                kubundropdown.Items.Add("値引");
                kubundropdown.Items.Add("立替");
                

                DropDownList kazeidropdown = (e.Row.FindControl("DDL_Kazeikubun") as DropDownList);//課税区分
                kazeidropdown.Items.Add("課税");
                kazeidropdown.Items.Add("非課税");

                
                DropDownList tanidropdown = (e.Row.FindControl("DDL_cTANI") as DropDownList);//単位

                String id = Request.QueryString["id"];
                DataTable dt_db = JC01Login_Class.Get_DB(id);
                String db = dt_db.Rows[0]["db"].ToString();
                String mail = dt_db.Rows[0]["mail"].ToString();
                ConstantVal.DB_NAME = db;

                dt_tani = JC27UriageTouroku_Class.dt_tani;
                tanidropdown.DataSource = dt_tani;
                tanidropdown.DataTextField = "sTANI";
                tanidropdown.DataValueField = "sTANI";
                tanidropdown.DataBind();
               
                if (dt_data.Rows.Count > 0)
                {
                    tanidropdown.Text = dt_data.Rows[e.Row.RowIndex]["sTANI"].ToString();
                    kubundropdown.Text = dt_data.Rows[e.Row.RowIndex]["skubun"].ToString();
                    kazeidropdown.Text = dt_data.Rows[e.Row.RowIndex]["fKazei"].ToString();
                }
                else
                {
                    tanidropdown.Text = "00";
                }
                #endregion

                #region　金額フォーマット変更
                TextBox txt_nsikiritanka = (e.Row.FindControl("TB_nSIKIRITANKA") as TextBox);//単価
                if (txt_nsikiritanka.Text != "")　　
                {
                    decimal nsikiritanka = Convert.ToDecimal(txt_nsikiritanka.Text);
                    txt_nsikiritanka.Text = nsikiritanka.ToString("#,##0.##");
                }
                TextBox txt_nsuryo = (e.Row.FindControl("TB_nSURYO") as TextBox); //数量
                if (txt_nsuryo.Text != "")
                {
                    decimal nsuryo = Convert.ToDecimal(txt_nsuryo.Text);
                    txt_nsuryo.Text = nsuryo.ToString("#,##0.##");
                }
                Label lb_nsikirikingaku = (e.Row.FindControl("LB_nSIKIRIKINGAKU") as Label);//金額
                if (txt_nsuryo.Text != "")
                {
                    decimal nsikirikingaku = Convert.ToDecimal(lb_nsikirikingaku.Text);
                    lb_nsikirikingaku.Text = nsikirikingaku.ToString("#,##0");
                }
                #endregion
                
            }
            //e.Row.Attributes.Add("onclick", Page.ClientScript.GetPostBackClientHyperlink((GridView)sender, "Select$" + e.Row.RowIndex));
        }
        #endregion

       
        #region TB_cSYOHIN_TextChanged
        protected void TB_cSYOHIN_TextChanged(object sender,EventArgs e)
        {
            var txt_csyouhin = sender as TextBox;
            GridViewRow gvRow = (GridViewRow)txt_csyouhin.NamingContainer;

            if (txt_csyouhin.Text != "")
            {
                if (TextUtility.IsIncludeZenkaku(txt_csyouhin.Text))
                {
                    txt_csyouhin.Text = "";
                }
                else
                {
                    txt_csyouhin.Text = txt_csyouhin.Text.PadLeft(10, '0');
                }
            }

            #region 商品コードによって情報を取って表示「上書き」
            String id = Request.QueryString["id"];
            DataTable dt_db = JC01Login_Class.Get_DB(id);
            String db = dt_db.Rows[0]["db"].ToString();
            String mail = dt_db.Rows[0]["mail"].ToString();
            ConstantVal.DB_NAME = db;

            JC27UriageTouroku_Class syouhin_kazei = new JC27UriageTouroku_Class();
            DataTable dt_fkazei = syouhin_kazei.dt_syouhin_kazei(txt_csyouhin.Text);

            if (dt_fkazei.Rows.Count > 0)
            {
                if (dt_fkazei.Rows[0]["fKazei"].ToString() == "1")
                {
                    (GV_UriageSyohin.Rows[gvRow.RowIndex].FindControl("DDL_KazeiKubun") as DropDownList).Text = "非課税";
                }
                else
                {
                    (GV_UriageSyohin.Rows[gvRow.RowIndex].FindControl("DDL_KazeiKubun") as DropDownList).Text = "課税";
                }
                //(GV_UriageSyohin.Rows[gvRow.RowIndex].FindControl("TB_nSURYO") as TextBox).Text = decimal.Parse(dt_fkazei.Rows[0]["nSYOUKISU"].ToString()).ToString("#,##0");//数量
                //(GV_UriageSyohin.Rows[gvRow.RowIndex].FindControl("TB_nSIKIRITANKA") as TextBox).Text = decimal.Parse(dt_fkazei.Rows[0]["nHANNBAIKAKAKU"].ToString()).ToString("#,##0");//単価

                decimal nsuryo = 0;
                decimal nsikiritanka = 0;
                decimal nsikirikingaku = 0;
                nsuryo = Convert.ToDecimal(dt_fkazei.Rows[0]["nSYOUKISU"].ToString());
                nsikiritanka = Convert.ToDecimal(dt_fkazei.Rows[0]["nHANNBAIKAKAKU"].ToString());
                (GV_UriageSyohin.Rows[gvRow.RowIndex].FindControl("TB_nSURYO") as TextBox).Text = nsuryo.ToString("#,##0.##");//数量
                (GV_UriageSyohin.Rows[gvRow.RowIndex].FindControl("TB_nSIKIRITANKA") as TextBox).Text = nsikiritanka.ToString("#,##0.##"); ;//単価
                nsikirikingaku = nsuryo * nsikiritanka;
                (GV_UriageSyohin.Rows[gvRow.RowIndex].FindControl("LB_nSIKIRIKINGAKU") as Label).Text = nsikirikingaku.ToString("#,##0");

                //(GV_UriageSyohin.Rows[gvRow.RowIndex].FindControl("DDL_cTANI") as DropDownList).Text = dt_fkazei.Rows[0]["sTANI"].ToString();
                (GV_UriageSyohin.Rows[gvRow.RowIndex].FindControl("TB_sSYOHIN") as TextBox).Text = dt_fkazei.Rows[0]["sSYOUHIN"].ToString() + " " + dt_fkazei.Rows[0]["sSHIYOU"].ToString();  //商品名

                DropDownList tanidropdown = (gvRow.FindControl("DDL_cTANI") as DropDownList);//単位

                ConstantVal.DB_NAME = db;

                dt_tani = JC27UriageTouroku_Class.dt_tani;
                tanidropdown.DataSource = dt_tani;
                tanidropdown.DataTextField = "sTANI";
                tanidropdown.DataValueField = "sTANI";
                tanidropdown.DataBind();
                tanidropdown.Text = dt_fkazei.Rows[0]["sTANI"].ToString();
            }
            else
            {
                txt_csyouhin.Text = "";
            }
            #endregion
            updUriageSyohinGrid.Update();

            KeiSan_CB_nKINGAKU();
            gridviewcheck();
           // Session["fedit"] = "true";
        }
        #endregion

        #region TB_sSYOHIN_TextChanged
        protected void TB_sSYOHIN_TextChanged(object sender, EventArgs e)
        {

            TextBox txt_sSYOHIN = (TextBox)sender;            GridViewRow gvRow = (GridViewRow)txt_sSYOHIN.NamingContainer;            int rowindex = gvRow.RowIndex;            if (txt_sSYOHIN.Text != "")            {                string s = txt_sSYOHIN.Text;                while (getbyte(s) > 50)                    s = s.Substring(0, s.Length - 1);                txt_sSYOHIN.Text = s;                (GV_UriageSyohin.Rows[rowindex].FindControl("TB_sSYOHIN") as TextBox).Text = s;            }

            gridviewcheck();
            updUriageSyohinGrid.Update();
           // Session["fedit"] = "true";
        }
        #endregion

        #region TB_nSURYO_TextChanged

        protected void TB_nSURYO_TextChanged(object sender,EventArgs e)
        {
           
            TextBox txt_nsuryou = (TextBox)sender;
            GridViewRow gvRow = (GridViewRow)txt_nsuryou.NamingContainer;

            decimal nsuryo = 0;
            decimal nsikiritanka = 0;
            decimal nsikirikingaku = 0;

            if (txt_nsuryou.Text != "")
            {
                if (TextUtility.IsIncludeZenkaku(txt_nsuryou.Text))
                {
                    txt_nsuryou.Text = "";
                    (GV_UriageSyohin.Rows[gvRow.RowIndex].FindControl("LB_nSIKIRIKINGAKU") as Label).Text = nsikirikingaku.ToString("#,##0");
                    
                }
                else
                {
                    nsuryo = Convert.ToDecimal(txt_nsuryou.Text);

                    if ((GV_UriageSyohin.Rows[gvRow.RowIndex].FindControl("TB_nSIKIRITANKA") as TextBox).Text != "")
                    {
                        nsikiritanka = Convert.ToDecimal((GV_UriageSyohin.Rows[gvRow.RowIndex].FindControl("TB_nSIKIRITANKA") as TextBox).Text);
                    }
                    nsikirikingaku = nsuryo * nsikiritanka;
                    (GV_UriageSyohin.Rows[gvRow.RowIndex].FindControl("LB_nSIKIRIKINGAKU") as Label).Text = nsikirikingaku.ToString("#,##0");

                    txt_nsuryou.Text = nsuryo.ToString("#,##0.##");
                }
            }
            else
            {
                TextBox tb_csyouhin = GV_UriageSyohin.Rows[gvRow.RowIndex].FindControl("TB_cSYOHIN") as TextBox;                if (tb_csyouhin.Text != "")                {                    txt_nsuryou.Text = "0";                }
                (GV_UriageSyohin.Rows[gvRow.RowIndex].FindControl("LB_nSIKIRIKINGAKU") as Label).Text = nsikirikingaku.ToString("#,##0");
            }

            KeiSan_CB_nKINGAKU();
            gridviewcheck();
            updUriageSyohinGrid.Update();
            //Session["fedit"] = "true";
        }
        #endregion

        #region TB_nSIKIRITANKA_TextChanged
        protected void TB_nSIKIRITANKA_TextChanged(object sender,EventArgs e)
        {
            TextBox txt_nsikiritanka = (TextBox)sender;
            GridViewRow gvRow = (GridViewRow)txt_nsikiritanka.NamingContainer;

            decimal nsuryo = 0;
            decimal nsikiritanka = 0;
            decimal nsikirikingaku = 0;

            if (txt_nsikiritanka.Text != "")
            {
                if (TextUtility.IsIncludeZenkaku(txt_nsikiritanka.Text))
                {
                    txt_nsikiritanka.Text = "";
                    (GV_UriageSyohin.Rows[gvRow.RowIndex].FindControl("LB_nSIKIRIKINGAKU") as Label).Text = nsikirikingaku.ToString("#,##0");
                }
                else
                {
                    nsikiritanka = Convert.ToDecimal(txt_nsikiritanka.Text);

                    if ((GV_UriageSyohin.Rows[gvRow.RowIndex].FindControl("TB_nSURYO") as TextBox).Text != "")
                    {
                        nsuryo = Convert.ToDecimal((GV_UriageSyohin.Rows[gvRow.RowIndex].FindControl("TB_nSURYO") as TextBox).Text);
                    }
                    nsikirikingaku = nsuryo * nsikiritanka;
                    (GV_UriageSyohin.Rows[gvRow.RowIndex].FindControl("LB_nSIKIRIKINGAKU") as Label).Text = nsikirikingaku.ToString("#,##0");

                    txt_nsikiritanka.Text = nsikiritanka.ToString("#,##0.##");

                }
            }
            else
            {
                TextBox tb_csyouhin = GV_UriageSyohin.Rows[gvRow.RowIndex].FindControl("TB_cSYOHIN") as TextBox;
                if (tb_csyouhin.Text != "")
                {
                    txt_nsikiritanka.Text = "0";
                }
                (GV_UriageSyohin.Rows[gvRow.RowIndex].FindControl("LB_nSIKIRIKINGAKU") as Label).Text = nsikirikingaku.ToString("#,##0");
            }
            KeiSan_CB_nKINGAKU();
            gridviewcheck();
            updUriageSyohinGrid.Update();
            //Session["fedit"] = "true";
        }
        #endregion

        #region TB_sTANI_TextChanged
        protected void TB_sTANI_TextChanged(object sender,EventArgs e)
        {
            TextBox txt_sTANI = (TextBox)sender;            GridViewRow gvRow = (GridViewRow)txt_sTANI.NamingContainer;            int rowindex = gvRow.RowIndex;            if (txt_sTANI.Text != "")            {                string s = txt_sTANI.Text;                while (getbyte(s) > 4)                    s = s.Substring(0, s.Length - 1);                txt_sTANI.Text = s;                (GV_UriageSyohin.Rows[rowindex].FindControl("TB_sTANI") as TextBox).Text = s;            }
            gridviewcheck();
        }
        #endregion

        #region BT_Sort_Click
        protected void BT_Sort_Click(object sender, EventArgs e)
        {
            if (ViewState["SyouhinTable"] != null)
            {
                dt_data = GetGridViewData(true);
                var dr_copy = dt_data.NewRow();
                int before_index = Convert.ToInt32(HF_beforeSortIndex.Value) - 1;
                int after_index = Convert.ToInt32(HF_afterSortIndex.Value) - 1;
                DataRow dr = dt_data.Rows[before_index];
                dr_copy.ItemArray = dr.ItemArray.Clone() as object[];
                dt_data.Rows.RemoveAt(before_index);
                dt_data.Rows.InsertAt(dr_copy, after_index);
                ViewState["SyouhinTable"] = dt_data;
                GV_UriageSyohin.DataSource = dt_data;
                GV_UriageSyohin.DataBind();
                Session["fedit"] = "true";
                Session.Timeout = 480;
            }
        }
        #endregion

        #region 合計金額計算
        private void KeiSan_CB_nKINGAKU()
        {
            dt_data = GetGridViewData(true);
            decimal nuriage = 0;
            decimal nhenpin = 0;
            decimal nebiki = 0;
            decimal tatekae = 0;
            decimal nuriagekazei = 0;
            decimal nhenpinkazei = 0;
            decimal nebikikazei = 0;
            decimal kazeitotal = 0;
            decimal nuriagehikazei = 0;
            decimal nhenpinhikazei = 0;
            decimal nebikihikazei = 0;
            decimal hikazeitotal = 0;
            try
            {
                for (int row = 0; row < dt_data.Rows.Count; row++)
                {
                    if (dt_data.Rows[row]["skubun"] != null)
                    {
                        if (dt_data.Rows[row]["skubun"].ToString() == "売上")
                        {
                            if (dt_data.Rows[row]["nSIKIRIKINGAKU"].ToString() !="")
                            {
                                nuriage = nuriage + Convert.ToDecimal(dt_data.Rows[row]["nSIKIRIKINGAKU"].ToString());
                                if (dt_data.Rows[row]["fKazei"] != null)
                                {
                                    if (dt_data.Rows[row]["fKazei"].ToString() == "課税")
                                    {
                                        nuriagekazei = nuriagekazei + Convert.ToDecimal(dt_data.Rows[row]["nSIKIRIKINGAKU"].ToString());
                                    }
                                    else
                                    {
                                        nuriagehikazei = nuriagehikazei + Convert.ToDecimal(dt_data.Rows[row]["nSIKIRIKINGAKU"].ToString());
                                    }
                                }
                                else
                                {
                                    nuriagekazei = nuriagekazei + Convert.ToDecimal(dt_data.Rows[row]["nSIKIRIKINGAKU"].ToString());
                                }
                            }
                            else
                            {
                                nuriage += 0;
                                nuriagekazei += 0;
                            }
                        }
                        else if (dt_data.Rows[row]["skubun"].ToString() == "返品")
                        {
                            if (dt_data.Rows[row]["nSIKIRIKINGAKU"].ToString() != "")
                            {
                                nhenpin = nhenpin + Convert.ToDecimal(dt_data.Rows[row]["nSIKIRIKINGAKU"].ToString());
                                if (dt_data.Rows[row]["fKazei"] != null)
                                {
                                    if (dt_data.Rows[row]["fKazei"].ToString() == "課税")
                                    {
                                        nhenpinkazei = nhenpinkazei + Convert.ToDecimal(dt_data.Rows[row]["nSIKIRIKINGAKU"].ToString());
                                    }
                                    else
                                    {
                                        nhenpinhikazei = nhenpinhikazei + Convert.ToDecimal(dt_data.Rows[row]["nSIKIRIKINGAKU"].ToString());
                                    }
                                }
                                else
                                {
                                    nhenpinkazei = nhenpinkazei + Convert.ToDecimal(dt_data.Rows[row]["nSIKIRIKINGAKU"].ToString());
                                }
                            }
                            else
                            {
                                nhenpin += 0;
                                nhenpinkazei += 0;
                            }
                        }
                        else if (dt_data.Rows[row]["skubun"].ToString() == "値引")
                        {
                            if (dt_data.Rows[row]["nSIKIRIKINGAKU"].ToString() != "")
                            {
                                nebiki = nebiki + Convert.ToDecimal(dt_data.Rows[row]["nSIKIRIKINGAKU"].ToString());
                                if (dt_data.Rows[row]["fKazei"] != null)
                                {
                                    if (dt_data.Rows[row]["fKazei"].ToString() == "課税")
                                    {
                                        nebikikazei = nebikikazei + Convert.ToDecimal(dt_data.Rows[row]["nSIKIRIKINGAKU"].ToString());
                                    }
                                    else
                                    {
                                        //nebikikazei += 0;
                                        nebikihikazei = nebikihikazei + Convert.ToDecimal(dt_data.Rows[row]["nSIKIRIKINGAKU"].ToString());
                                    }
                                }
                                else
                                {
                                    nebikikazei = nebikikazei + Convert.ToDecimal(dt_data.Rows[row]["nSIKIRIKINGAKU"].ToString());
                                }
                            }
                            else
                            {
                                nebiki += 0;
                                nebikikazei += 0;
                            }
                        }
                        else if (dt_data.Rows[row]["skubun"].ToString() == "立替")
                        {
                            if (dt_data.Rows[row]["nSIKIRIKINGAKU"].ToString() != null)
                            {
                                tatekae = tatekae + Convert.ToDecimal(dt_data.Rows[row]["nSIKIRIKINGAKU"].ToString());
                            }
                            else
                            {
                                tatekae += 0;
                            }
                        }
                    }
                }

                CB_Uriage.Text = (nuriage - nhenpin + tatekae).ToString("#,##0");

                CB_Henpin.Text = nhenpin.ToString("#,##0");

                CB_Nebiki.Text = nebiki.ToString("#,##0");

                CB_Tatekae.Text = tatekae.ToString("#,##0");

                CB_Shoukei.Text = (Convert.ToDecimal(CB_Uriage.Text) - nebiki).ToString("#,##0");

                kazeitotal = (nuriagekazei - nhenpinkazei) - nebikikazei;

                CB_Kazei.Text = kazeitotal.ToString("#,##0");

                hikazeitotal = (nuriagehikazei - nhenpinhikazei) - nebikihikazei;
                CB_Hikazei.Text = hikazeitotal.ToString("#,##0");

                Calculate_Zei();   //消費税計算

                CB_KINGAKU.Text = (Convert.ToDecimal(CB_Shoukei.Text) + Convert.ToDecimal(CB_Syouhizei.Text)).ToString("#,##0");

                pudgoukei.Update();

                CB_Jyuchuuzan.Text =(Convert.ToDecimal(CB_Jyuchuukingaku.Text) - (Convert.ToDecimal(CB_Shoukei.Text) + Convert.ToDecimal(CB_NouhinKingaku.Text))).ToString("#,##0");
               
                updjyuchu.Update();

                LB_Kazei.Text = "（" + kazeitotal.ToString("#,###") + "）";
                updkazei.Update();
            }
            catch 
            {
                
            }
        }
        #endregion

        #region 消費税計算
        private void Calculate_Zei()
        {
            String id = Request.QueryString["id"];
            DataTable dt_db = JC01Login_Class.Get_DB(id);
            String db = dt_db.Rows[0]["db"].ToString();
            String mail = dt_db.Rows[0]["mail"].ToString();
            ConstantVal.DB_NAME = db;

            if (JC27UriageTouroku_Class.dt_J_Info.Rows.Count > 0)
            {
                if (JC27UriageTouroku_Class.dt_J_Info.Rows[0]["fkeisantani"].ToString() == "1" || LB_fseikyuukubun.Text == "1")
                {

                    if (LB_Uriage.Text != "")
                    {
                        decimal nsyouhizei = 0;
                        string per = "";
                        per = JC27UriageTouroku_Class.Get_per(LB_Uriage.Text);  //消費税パーセンテージ取る
                        if (per != "")
                        {  
                            nsyouhizei = (Convert.ToDecimal(CB_Kazei.Text)) * (Convert.ToDecimal(per) / 100);
                        }

                        #region 少数点 端数処理

                        if (DDL_ftansuushori.SelectedIndex == 0)//切り捨て
                        {
                            nsyouhizei = Math.Floor(nsyouhizei);
                        }
                        else if (DDL_ftansuushori.SelectedIndex == 1)//四捨五入
                        {
                            nsyouhizei = Math.Round(nsyouhizei, 0, MidpointRounding.AwayFromZero);
                            
                        }
                        else   //切り上げ
                        {
                            nsyouhizei = Math.Ceiling(nsyouhizei);
                        }
                        #endregion

                        CB_Syouhizei.Text = nsyouhizei.ToString("#,##0");
                    }
                    else
                    {
                        CB_Syouhizei.Text = "0";
                    }
                }
                else
                {
                    CB_Syouhizei.Text = "0";
                }
            }
        }

        #endregion

        #region BT_Uriagehozon_Click
        protected void BT_Uriagehozon_Click(object sender, EventArgs e)
        {
            Save();
        }
        #endregion

        private void sagoukeical()
        {
            String id = Request.QueryString["id"];
            DataTable dt_db = JC01Login_Class.Get_DB(id);
            String db = dt_db.Rows[0]["db"].ToString();
            String mail = dt_db.Rows[0]["mail"].ToString();
            ConstantVal.DB_NAME = db;

            JC_ClientConnecction_Class jc = new JC_ClientConnecction_Class();
            jc.loginId = mail;
            MySqlConnection cn = jc.GetConnection();
            cn.Open();

            Double uriagekingaku = 0;
            DataTable dt_goukei = new DataTable();
            string selectgoukei_query = "select ";
            //selectgoukei_query += " ifnull(sum(ru.nKINGAKU),0) as nKINGAKU";
            selectgoukei_query += " ifnull(sum(ru.nuriage_kingaku),0) as nKINGAKU";
            selectgoukei_query += " from r_uriage as ru";
            selectgoukei_query += " inner join r_uri_mitsu as rum";
            selectgoukei_query += " on ru.cURIAGE=rum.cURIAGE";
            selectgoukei_query += " and rum.cMITUMORI='" + LB_Mitsumori_Code.Text + "'";
            selectgoukei_query += " where ru.cURIAGE<>'" + LB_Uriage_Code.Text + "'";
            selectgoukei_query += " and (ru.dURIAGE is not null or ru.dURIAGE!='')";
            MySqlCommand cmd = new MySqlCommand(selectgoukei_query, cn);
            cmd.CommandTimeout = 0;
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            da.Fill(dt_goukei);
            cn.Close();
            da.Dispose();
            if (dt_goukei.Rows.Count > 0)
            {
                string kingaku_goukei = dt_goukei.Rows[0]["nKINGAKU"].ToString();
                if (kingaku_goukei != "")
                {
                    uriagekingaku = Double.Parse(kingaku_goukei);
                }
            }

            ConstantVal.DB_NAME = db;

            JC27UriageTouroku_Class JC27_Uriage = new JC27UriageTouroku_Class();
            Double mitumorikingaku = JC27_Uriage.GetMitumoriKingaku(LB_Mitsumori_Code.Text);

            TB_sa_goukei.Value = (mitumorikingaku - (uriagekingaku + Double.Parse(CB_Uriage.Text))).ToString();

            if (Convert.ToDouble(TB_sa_goukei.Value) <= 0)
            {
                //if (MessageBox.Show("売上済にしますか？", "確認", MessageBoxButtons.OKCancel) == DialogResult.OK)
                //{
                //    BT_mitsumorijyoutai.Text = "売上済解除";
                //    setcolormitsujyoutai();
                //}
                isUriagezumiMsg = true;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowAnkenChangeMessage",
                    "DeleteConfirmBox('売上済にしますか？','" + BT_UriagezumiOk.ClientID + "','" + BT_UriagezumiCancel.ClientID + "');", true);
            }
        }

        #region Save()
        private bool Save()        {
            #region             //if (LB_Uriage.Text != "" && LB_NyuukinYoutei.Text == "")  //delete by yamin 20180706
                                 //{
                                 //    int b = 0;
                                 //    if (JC27UriageTouroku_Class.dt_J_Info.Rows.Count > 0)
                                 //    {
                                 //        string strRestrict = JC27UriageTouroku_Class.dt_J_Info.Rows[0]["fKKUBUN"].ToString();
                                 //        if (strRestrict.Length >= 5)
                                 //        {
                                 //            b = Convert.ToInt16(strRestrict[4].ToString(), 16);
                                 //        }
                                 //    }
                                 //    if (b == 1)
                                 //    {
                                 //        ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowAnkenChangeMessage",
                                 //        "ShowKoumokuChangesConfirmMessage('入金日が未入力です。保存しますか？','" + BT_Ok.ClientID + "','" + BT_Cancel.ClientID + "');", true);

            //    }
            //}
            #endregion            TB_Uriagekenmei.BorderColor = System.Drawing.Color.Gray;
            TB_Denpyoubikou.BorderColor = System.Drawing.Color.Gray;
            TB_Tokuisakibumon.BorderColor = System.Drawing.Color.Gray;
            TB_Seikyuusakibumon.BorderColor = System.Drawing.Color.Gray;
            TB_Shanaimemo.BorderColor = System.Drawing.Color.Gray;
            if (GV_UriageSyohin.Rows.Count <= 0)            {
                string msg = "明細がありません。";                ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowAnkenChangeMessage",                            "ShowMojiMessage('" + msg + "','" + btnmojiOK.ClientID + "');", true);
                //MessageBox.Show("明細がありません。");
                return false;            }            else            {                //dt_data = GetGridViewData(true);                string uricode = LB_Uriage_Code.Text;                String id = Request.QueryString["id"];
                DataTable dt_db = JC01Login_Class.Get_DB(id);
                String db = dt_db.Rows[0]["db"].ToString();
                String mail = dt_db.Rows[0]["mail"].ToString();
                ConstantVal.DB_NAME = db;                JC27UriageTouroku_Class save_class = new JC27UriageTouroku_Class();                if (LB_Uriage_Code.Text == "")                {                    uricode = save_class.Uriage_code();                    save_class.f_new = true;                }                else                {                    save_class.f_new = false;                }                save_class.cURIAGE = uricode;       //売上コード
                save_class.dURIAGE = LB_Uriage.Text;  //売上日
                save_class.cMITUMORI = LB_Mitsumori_Code.Text;  //見積コード
                save_class.dNYUUKINYOTEI = LB_NyuukinYoutei.Text;  //入金予定日
                save_class.dDate = LB_Hakkou.Text;  //発行日

                save_class.cHENKOUSYA = lblLoginUserCode.Text; //ログイン担当
                save_class.cTOKUISAKI = LB_cTOKUISAKI.Text;  //得意先コード
                save_class.sTOKUISAKI = LB_sTOKUISAKI.Text;  //得意先名
                save_class.sTOKUISAKI_TAN = LB_sTOKUISAKI_TAN.Text;  //得意先担当
                                                                     //save_class.sTOKUISAKI_TAN_Jun = LB_sTOKUISAKI_TAN_JUN.Text; //得意先担当順番
                if (LB_sTOKUISAKI_TAN_JUN.Text == "")                {                    save_class.sTOKUISAKI_TAN_Jun = "0";  //請求先担当順番
                }                else                {                    save_class.sTOKUISAKI_TAN_Jun = LB_sTOKUISAKI_TAN_JUN.Text;  //請求先担当順番
                }

                save_class.sTOKUISAKI_YAKUSYOKU = LB_Yakusyoku.Text; //得意先担当役職
                save_class.sTOKUISAKI_KEISYO = LB_Keisyo.Text;  //得意先敬称
                save_class.cSEIKYUSAKI = LB_cSEIKYUSAKI.Text;  //請求先
                save_class.sSEIKYUSAKI = LB_sSEIKYUSAKI.Text;  //請求先名
                save_class.sSEIKYU_TAN = LB_sSEIKYUSAKI_TAN.Text; //請求先担当
                if (LB_sSEIKYUSAKI_TAN_JUN.Text == "")                {                    save_class.sSEIKYU_TAN_Jun = "0";  //請求先担当順番
                }                else                {                    save_class.sSEIKYU_TAN_Jun = LB_sSEIKYUSAKI_TAN_JUN.Text;  //請求先担当順番
                }

                save_class.sSEIKYU_YAKUSHOKU = LB_Seikyu_YAKUSYOKU.Text; //請求先役職
                save_class.sSEIKYU_KEISYO = LB_Seikyu_KEISYO.Text;  //請求先敬称
                save_class.nKINGAKU = Decimal.Parse(CB_KINGAKU.Text); //合計金額
                save_class.nuriage_kingaku = Decimal.Parse(CB_Uriage.Text);  //合計売上
                save_class.nnebiki_kingaku = Decimal.Parse(CB_Nebiki.Text); //合計値引
                save_class.nsyoukei_kingkau = Decimal.Parse(CB_Shoukei.Text); //小計
                save_class.nhenpin_kingkau = Decimal.Parse(CB_Henpin.Text);//返品
                save_class.ntatekae_kingaku = Decimal.Parse(CB_Tatekae.Text); //立替
                save_class.nKAZEIKINGAKU = Decimal.Parse(CB_Kazei.Text); //課税合計
                save_class.nutihikazei = Decimal.Parse(CB_Hikazei.Text); //非課税合計

                save_class.cEIGYOTANTOSYA = lblcJISHATANTOUSHA.Text; //営業担当者
                save_class.fprint = "0";

                save_class.nsyohizei = Decimal.Parse(CB_Syouhizei.Text);  //消費税

                if (TextUtility.isomojiCharacter(TB_Uriagekenmei.Text))                {                    f_isomoji_msg = false;                    TB_Uriagekenmei.BorderColor = System.Drawing.Color.Red;                }                save_class.snouhin = TB_Uriagekenmei.Text.Replace("\\", "\\\\").Replace("'", "\\'");  //売上件名

                if (TextUtility.isomojiCharacter(TB_Tokuisakibumon.Text))                {                    f_isomoji_msg = false;                    TB_Tokuisakibumon.BorderColor = System.Drawing.Color.Red;                }                save_class.sTOKUISAKI_TANBUMON = TB_Tokuisakibumon.Text.Replace("\\", "\\\\").Replace("'", "\\'"); //得意先担当部門

                if (TextUtility.isomojiCharacter(TB_Seikyuusakibumon.Text))                {                    f_isomoji_msg = false;                    TB_Seikyuusakibumon.BorderColor = System.Drawing.Color.Red;                }                save_class.sSEIKYU_TANBUMON = TB_Seikyuusakibumon.Text.Replace("\\", "\\\\").Replace("'", "\\'");   //請求先部門

                if (TextUtility.isomojiCharacter(TB_Denpyoubikou.Text))                {                    f_isomoji_msg = false;                    TB_Denpyoubikou.BorderColor = System.Drawing.Color.Red;                }                save_class.sbikou = TB_Denpyoubikou.Text.Replace("\\", "\\\\").Replace("'", "\\'"); //伝票備考

                if (TextUtility.isomojiCharacter(TB_Shanaimemo.Text))                {                    f_isomoji_msg = false;                    TB_Shanaimemo.BorderColor = System.Drawing.Color.Red;                }                save_class.sMemo = TB_Shanaimemo.Text.Replace("\\", "\\\\").Replace("'", "\\'"); //社内メモ

                save_class.cPJ = tb_jishabangou.Text.Replace("\\", "\\\\").Replace("'", "\\'"); //社内メモ

                dt_data = GetGridViewData(true);

                if (f_isomoji_msg == false)                {                    //TB_Uriagekenmei.BorderColor = System.Drawing.Color.Red;                    //TB_Denpyoubikou.BorderColor = System.Drawing.Color.Red;
                    //TB_Tokuisakibumon.BorderColor = System.Drawing.Color.Red;
                    //TB_Seikyuusakibumon.BorderColor = System.Drawing.Color.Red;
                    //TB_Shanaimemo.BorderColor = System.Drawing.Color.Red;                    string msg = "使用不可能なテキスト（環境依存文字）が入力され保存できません。</br>文字化けの原因となるため、下記の文字を修正してください。</br>" + " 対象文字：「" + TextUtility.invalidtext_all + "」";                    ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowAnkenChangeMessage",                                "ShowMojiMessage('" + msg + "','" + btnmojiOK.ClientID + "');", true);                    f_isomoji_msg = true;                    Session["moji"] = "true";                    return false;                }                ViewState["SyouhinTable"] = dt_data;                save_class.dt_data = dt_data;                if (HF_cMituJoutai.Value != "06")
                {
                    sagoukeical();
                }

                if (!save_class.DataSave())
                {
                    return false;
                }
                HF_curiage.Value = uricode;
                HF_dHenkou.Value= save_class.dHENKOU.ToString("yyyy/MM/dd");
                updHiddenField.Update();
                if (!isUriagezumiMsg)
                {
                    if (LB_Uriage_Code.Text == "")
                    {
                        LB_Uriage_Code.Text = uricode;
                        LB_cSakuseisha.Text = lblLoginUserCode.Text; //作成者
                        LB_sSakuseisha.Text = lblLoginUserName.Text; //作成者
                        LB_cSaishukoushinsha.Text = lblLoginUserCode.Text;//最後更新者
                        LB_sSaishukoushinsha.Text = lblLoginUserName.Text;//最後更新者
                        LB_SakuseiDate.Text = save_class.dHENKOU.ToString("yyyy/MM/dd"); //作成日
                        LB_KoushinDate.Text = save_class.dHENKOU.ToString("yyyy/MM/dd");  //最後更新日
                        updSakuseiDate.Update();
                        updSakuseisha.Update();
                        updKoushinDate.Update();
                        updKoushinsha.Update();
                        BT_Pdf.Enabled = true;
                        BT_Pdf.CssClass = "BlueBackgroundButton JC10SaveBtn";
                        BT_Sakujo.Visible = true;
                    }
                    else
                    {
                        LB_cSaishukoushinsha.Text = lblLoginUserCode.Text;//最後更新者
                        LB_sSaishukoushinsha.Text = lblLoginUserName.Text;//最後更新者
                        LB_KoushinDate.Text = save_class.dHENKOU.ToString("yyyy/MM/dd");//最後更新日
                        updKoushinDate.Update();
                        updKoushinsha.Update();
                    }
                }

            }            //if (LB_Uriage.Text == "")            //{            //    LB_Uriage_Jyoutai.Text = "作成中"; //売上状態
            //}            //else            //{            //    LB_Uriage_Jyoutai.Text = "作成済"; //売上状態
            //}            Session["cUriage"] = LB_Uriage_Code.Text;
            upd_uriagecode.Update();

            //if (Session["fcopy"] != null)
            //{
            //    if (Session["fcopy"].ToString() != "true")
            //    {
            //        divLabelSave.Style["display"] = "flex";
            //        updLabelSave.Update();
            //    }
            //}
            //else
            //{
            //    divLabelSave.Style["display"] = "flex";
            //    updLabelSave.Update();

            //}
            if (!isUriagezumiMsg)
            {
                divLabelSave.Style["display"] = "flex";
                updLabelSave.Update();
                Session["fedit"] = "false";
                Session["gvedit"] = "false";
                Session["moji"] = "false";
            }            return true;        }
        #endregion

        protected void BT_LBSaveCross_Click(object sender, EventArgs e)
        {
            divLabelSave.Style["display"] = "none";
        }

        protected void BT_Ok_Click(object sender, EventArgs e)
        {
            fsavekakunin = true;
        }

        protected void BT_No_Click(object sender, EventArgs e)
        {
            fsavekakunin = false;
        }

        protected void BT_Cancel_Click(object sender, EventArgs e)
        {
            fsavekakunin = false;
        }
        #region 区分選択
        protected void DDL_Kubun_SelectedIndexChanged(object sender, EventArgs e)
        { 
            DropDownList DDL_Kubun = (DropDownList)sender;
            GridViewRow gvRow = (GridViewRow)DDL_Kubun.NamingContainer;
            String skubun = DDL_Kubun.Text;
           
            if (skubun == "立替")
            {
                (GV_UriageSyohin.Rows[gvRow.RowIndex].FindControl("DDL_KazeiKubun") as DropDownList).Text = "非課税";
            }
            KeiSan_CB_nKINGAKU();
            gridviewcheck();
        }
        #endregion

        #region 課税区分選択
        protected void DDL_KazeiKubun_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList DDL_KazeiKubun = (DropDownList)sender;
            GridViewRow gvRow = (GridViewRow)DDL_KazeiKubun.NamingContainer;
            String skaze = DDL_KazeiKubun.Text;
            if (skaze == "課税"  && (GV_UriageSyohin.Rows[gvRow.RowIndex].FindControl("DDL_Kubun") as DropDownList).Text== "立替")　//立替場合非課税
            {
                (GV_UriageSyohin.Rows[gvRow.RowIndex].FindControl("DDL_KazeiKubun") as DropDownList).Text = "非課税";
            }
            else if(skaze == "課税" &&
                    (GV_UriageSyohin.Rows[gvRow.RowIndex].FindControl("TB_cSYOHIN") as TextBox).Text!="")　　//商品コードがあれば商品によって課税区分を表示
            {
                String id = Request.QueryString["id"];
                DataTable dt_db = JC01Login_Class.Get_DB(id);
                String db = dt_db.Rows[0]["db"].ToString();
                String mail = dt_db.Rows[0]["mail"].ToString();
                ConstantVal.DB_NAME = db;

                JC27UriageTouroku_Class syouhin_kazei = new JC27UriageTouroku_Class();
                DataTable dt_fkazei = syouhin_kazei.dt_syouhin_kazei((GV_UriageSyohin.Rows[gvRow.RowIndex].FindControl("TB_cSYOHIN") as TextBox).Text);

                if (dt_fkazei.Rows.Count > 0)
                {
                    if (dt_fkazei.Rows[0]["fKazei"].ToString() == "1")
                    {
                        (GV_UriageSyohin.Rows[gvRow.RowIndex].FindControl("DDL_KazeiKubun") as DropDownList).Text = "非課税";
                    }                    
                }
            }

            KeiSan_CB_nKINGAKU();
            gridviewcheck();
           // Session["fedit"] = "true";
        }
        #endregion

        #region BT_Sakujo_Click
        protected void BT_Sakujo_Click(object sender, EventArgs e)
        {
            divLabelSave.Style["display"] = "none";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowAnkenChangeMessage",
                    "DeleteConfirmBox('削除してもよろしいでしょうか？','" + BT_DeleteOk.ClientID + "','" +  BT_DeleteCancel.ClientID + "');", true);
        }
        #endregion

        #region TB_Uriagekenmei_TextChanged
        protected void TB_Uriagekenmei_TextChanged(object sender, EventArgs e)
        {
            var textByteCount = Encoding.Default.GetByteCount(TB_Uriagekenmei.Text);
            if(textByteCount>80)
            {
                string abb = TB_Uriagekenmei.Text;
                while(Encoding.Default.GetByteCount(abb)>80)
                {
                    abb = abb.Substring(0, abb.Length - 1);
                }
                TB_Uriagekenmei.Text = abb;
            }

            Session["fedit"] = "true";
        }
        #endregion

        #region TB_Denpyoubikou_TextChanged
        protected void TB_Denpyoubikou_TextChanged(object sender, EventArgs e)
        {
            var textByteCount = Encoding.Default.GetByteCount(TB_Denpyoubikou.Text);
            if (textByteCount > 120)
            {
                string abb = TB_Denpyoubikou.Text;
                while (Encoding.Default.GetByteCount(abb) > 120)
                {
                    abb = abb.Substring(0, abb.Length - 1);
                }
                TB_Denpyoubikou.Text = abb;
            }
            Session["fedit"] = "true";
        }
        #endregion

        #region TB_Tokuisakibumon_TextChanged
        protected void TB_Tokuisakibumon_TextChanged(object sender, EventArgs e)
        {
            var textByteCount = Encoding.Default.GetByteCount(TB_Tokuisakibumon.Text);
            if (textByteCount > 36)
            {
                string abb = TB_Tokuisakibumon.Text;
                while (Encoding.Default.GetByteCount(abb) > 36)
                {
                    abb = abb.Substring(0, abb.Length - 1);
                }
                TB_Tokuisakibumon.Text = abb;
            }
            Session["fedit"] = "true";
        }
        #endregion

        #region TB_Seikyuusakibumon_TextChanged
        protected void TB_Seikyuusakibumon_TextChanged(object sender, EventArgs e)
        {
            var textByteCount = Encoding.Default.GetByteCount(TB_Seikyuusakibumon.Text);
            if (textByteCount > 36)
            {
                string abb = TB_Seikyuusakibumon.Text;
                while (Encoding.Default.GetByteCount(abb) > 36)
                {
                    abb = abb.Substring(0, abb.Length - 1);
                }
                TB_Seikyuusakibumon.Text = abb;
            }
            Session["fedit"] = "true";
        }
        #endregion

        #region TB_Shanaimemo_TextChanged
        protected void TB_Shanaimemo_TextChanged(object sender, EventArgs e)
        {
            var textByteCount = Encoding.Default.GetByteCount(TB_Shanaimemo.Text);
            if (textByteCount > 250)
            {
                string abb = TB_Shanaimemo.Text;
                while (Encoding.Default.GetByteCount(abb) > 250)
                {
                    abb = abb.Substring(0, abb.Length - 1);
                }
                TB_Shanaimemo.Text = abb;
            }
            Session["fedit"] = "true";
        }
        #endregion

        #region BT_Add_Click
        protected void BT_Add_Click(object sender, EventArgs e)
        {
            BT_Add.Style["display"] = "none";
            Button btn_addSyohin = (Button)sender;
            int rowID = 0;
            dt_data = GetGridViewData(true);
            if (dt_data.Rows.Count == 0)
            {
                DataRow dr = dt_data.NewRow();
                dr[0] = "";
                dr[1] = "";
                dr[2] = "";
                dr[3] = "";
                dr[4] = "";
                dr[5] = "";
                dr[6] = "";
                dr[7] = "";
                dr[8] = "";

                dt_data.Rows.InsertAt(dr, rowID);
            }
            ViewState["SyouhinTable"] = dt_data;
            GV_UriageSyohin.DataSource = dt_data;
            GV_UriageSyohin.DataBind();
            KeiSan_CB_nKINGAKU();
            gridviewcheck();
        }
        #endregion

        #region BT_DeleteOk_Click
        protected void BT_DeleteOk_Click(object sender, EventArgs e)
        {
            String id = Request.QueryString["id"];
            DataTable dt_db = JC01Login_Class.Get_DB(id);
            String db = dt_db.Rows[0]["db"].ToString();
            String mail = dt_db.Rows[0]["mail"].ToString();
            ConstantVal.DB_NAME = db;

            JC27UriageTouroku_Class delete_class = new JC27UriageTouroku_Class();
            delete_class.Delete_Uriage(LB_Uriage_Code.Text);
            if (!String.IsNullOrEmpty(Request.QueryString["id"]))
            {
                id = Request.QueryString["id"];
                Response.Redirect("JC34UriageList.aspx?id=" + id);
            }
            else
            {
                Response.Redirect("JC01Login.aspx");
            }
        }
        #endregion

        #region BT_Mitsumori_Code_Click
        protected void BT_Mitsumori_Code_Click(object sender, EventArgs e)
        {
            if (Session["fedit"].ToString() == "true" || Session["gvedit"].ToString() == "true")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowAnkenChangeMessage",
                        "ShowKoumokuChangesConfirmMessage('保存しますか？','" + btnYes.ClientID + "','" + btnNo.ClientID + "','" + btnCancel.ClientID + "');", true);
            }
            else
            {
                //Display_Mitsumori();
                if (GV_UriageSyohin.Rows.Count <= 0)                {                    string msg = "明細がありません。";                    ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowAnkenChangeMessage",                                "ShowMojiMessage('" + msg + "','" + btnmojiOK.ClientID + "');", true);
                    //MessageBox.Show("明細がありません。");
                    //return false;
                }                else                {                    Display_Mitsumori();                }
            }
        }
        #endregion

        #region btnYes_Click
        protected void btnYes_Click(object sender, EventArgs e)
        {
            if(Save())
            {
                Display_Mitsumori();
            }
            
        }
        #endregion

        #region btnNo_Click
        protected void btnNo_Click(object sender, EventArgs e)
        {
            Display_Mitsumori();
        }
        #endregion

        #region Display_Mitsumori()
        private void Display_Mitsumori()
        {
            Session["cMitumori"] = LB_Mitsumori_Code.Text;
            if (Session["cMitumori"] != null)
            {
                JC99NavBar.insatsusettei = false;
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
        }
        #endregion

        #region BT_Pdf_Click
        protected void BT_Pdf_Click(object sender, EventArgs e)
        {
            divLabelSave.Style["display"] = "none";
            if (LB_Uriage_Code.Text != "")
            {
                if (Session["fedit"].ToString() == "true" || Session["gvedit"].ToString() == "true")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowAnkenChangeMessage",
                    "ShowMessage1('項目が変更されています。保存しますか？','" + btnYes1.ClientID + "','" + btnNo1.ClientID + "','" + btnCancel1.ClientID + "');", true);                    
                }
                else
                {
                    //PrintPDF(sender, e);
                    if (GV_UriageSyohin.Rows.Count <= 0)                    {                        string msg = "明細がありません。";                        ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowAnkenChangeMessage",                                    "ShowMojiMessage('" + msg + "','" + btnmojiOK.ClientID + "');", true);
                        //MessageBox.Show("明細がありません。");
                        //return false;
                    }                    else                    {                        PrintPDF(sender, e);                    }
                }
            }
           

        }
        #endregion

        #region PrintPDF
        private void PrintPDF(object sender, EventArgs e)
        {
            String id = Request.QueryString["id"];
            DataTable dt_db = JC01Login_Class.Get_DB(id);
            String db = dt_db.Rows[0]["db"].ToString();
            String mail = dt_db.Rows[0]["mail"].ToString();
            ConstantVal.DB_NAME = db;

            JC27UriageTouroku_Class settei = new JC27UriageTouroku_Class();
            Login_Tantou();
            settei.cTANTOUSHA = lblLoginUserCode.Text;
            DataTable dt_gettantou = settei.GetTantou();
            if (dt_gettantou.Rows.Count > 0)
            {
                if (dt_gettantou.Rows[0]["Shoshiki"].ToString() == "2")
                {
                    // setrogo();
                    string flagrogo = "";
                    String flagbikou = "";  //20220131 MiMi Added
                    String cKyoten = "";

                    if (!String.IsNullOrEmpty(LB_skyoten.Text))
                    {
                        DataTable dt_r_uriage = new DataTable();
                        ConstantVal.DB_NAME = db;
                        JC27UriageTouroku_Class r_uriage_data = new JC27UriageTouroku_Class();
                        r_uriage_data.cURIAGE = LB_Uriage_Code.Text;
                        dt_r_uriage = r_uriage_data.UriageData();
                        cKyoten = dt_r_uriage.Rows[0]["cCO"].ToString();
                    }

                    if (cKyoten != "")
                    {
                        ConstantVal.DB_NAME = db;
                        JC_ClientConnecction_Class jc = new JC_ClientConnecction_Class();
                        jc.loginId = mail;
                        con = jc.GetConnection();
                        con.Open();

                        #region ロゴタイトル
                        if (dt_gettantou.Rows.Count > 0)
                        {
                            flagrogo = dt_gettantou.Rows[0]["rogo"].ToString().TrimEnd();
                            #region todelete
                            //if (dt_gettantou.Rows[0]["rogo"].ToString().TrimEnd() == "1")
                            //{
                            //    flagrogo = "1";
                            //}
                            //else if (dt_gettantou.Rows[0]["rogo"].ToString().TrimEnd() == "2")
                            //{
                            //    flagrogo = "2";
                            //}
                            //else if (dt_gettantou.Rows[0]["rogo"].ToString().TrimEnd() == "3")
                            //{
                            //    flagrogo = "3";
                            //}
                            //else if (dt_gettantou.Rows[0]["rogo"].ToString().TrimEnd() == "4")
                            //{
                            //    flagrogo = "4";
                            //}
                            //else if (dt_gettantou.Rows[0]["rogo"].ToString().TrimEnd() == "5")
                            //{
                            //    flagrogo = "5";
                            //}
                            #endregion
                        }

                        string sqlkyoten = "";
                        sqlkyoten = "select";
                        if (flagrogo == "1")
                        {
                            sqlkyoten += " sIMAGE1 as sIMAGE";
                        }
                        else if (flagrogo == "2")
                        {
                            sqlkyoten += " sIMAGE2 as sIMAGE";
                        }
                        else if (flagrogo == "3")
                        {
                            sqlkyoten += " sIMAGE3 as sIMAGE";
                        }
                        else if (flagrogo == "4")
                        {
                            sqlkyoten += " sIMAGE4 as sIMAGE";
                        }
                        else if (flagrogo == "5")
                        {
                            sqlkyoten += " sIMAGE5 as sIMAGE";
                        }
                        else
                        {
                            sqlkyoten += " '' as sIMAGE";
                        }
                        sqlkyoten += " from m_j_info where cCO ='" + cKyoten + "'";

                        MySqlCommand cmd = new MySqlCommand(sqlkyoten, con);
                        DataTable dt_mj_info = new DataTable();
                        MySqlDataAdapter da_mj_info = new MySqlDataAdapter(cmd);
                        da_mj_info.Fill(dt_mj_info);
                        da_mj_info.Dispose();
                        if (dt_mj_info.Rows.Count > 0)
                        {
                            if (dt_mj_info.Rows[0]["sIMAGE"].ToString() == "")
                            {
                                //Response.Write("その拠点にはロゴが登録されていません。");
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowAnkenChangeMessage",
                                "ShowRogoMessage('その拠点にはロゴが登録されていません。','" + btnOK.ClientID + "');", true);
                                return;
                            }
                        }
                        #endregion
                        // divLabelSave.Style["display"] = "none";
                        uriage rpt = new uriage();
                        rpt.cURIAGE = LB_Uriage_Code.Text;
                        rpt.loginId = mail;
                        rpt.ckyoten = cKyoten;
                        rpt.frogoimage = flagrogo;
                        Login_Tantou();
                        rpt.logintantou = lblLoginUserCode.Text;
                        rpt.Run(false);

                        #region 20220131 MiMi Added Start
                        flagbikou = dt_gettantou.Rows[0]["seikyubikou"].ToString().TrimEnd();
                        seikyuusho rpt_seikyusho = new seikyuusho();
                        rpt_seikyusho.stitle = "御請求書";
                        rpt_seikyusho.fSEIKYUU = true;
                        rpt_seikyusho.cURIAGE = LB_Uriage_Code.Text;
                        rpt_seikyusho.dINVOICE = LB_Uriage.Text;
                        rpt_seikyusho.dMITUMORISAKUSEI = LB_Uriage.Text;
                        if (BT_Hidzukeari.CssClass == "JC10ZeikomiBtnActive")
                        {
                            rpt_seikyusho.fhiduke = true;
                        }
                        else if (BT_Hidzukearinashi.CssClass == "JC10ZeikomiBtnActive")
                        {
                            rpt_seikyusho.fhiduke = false;
                        }
                        rpt_seikyusho.fbikou = flagbikou;
                        rpt_seikyusho.loginId = mail;
                        rpt_seikyusho.ckyoten = cKyoten;
                        rpt_seikyusho.frogoimage = flagrogo;
                        rpt_seikyusho.bikou = TB_Denpyoubikou.Text;
                        rpt_seikyusho.Run(false);

                        for (int i = 0; i < rpt_seikyusho.Document.Pages.Count; i++)
                        {
                            rpt.Document.Pages.Add(rpt_seikyusho.Document.Pages[i].Clone());
                        }
                        #endregion

                        String filename = "uriage.pdf";
                        System.IO.MemoryStream ms = new System.IO.MemoryStream();
                        GrapeCity.ActiveReports.Export.Pdf.Section.PdfExport pdfExport1 = new GrapeCity.ActiveReports.Export.Pdf.Section.PdfExport();
                        pdfExport1.Export(rpt.Document, ms);
                        ms.Position = 0;
                        Session["PDFMemoryStream"] = ms;
                        Session["UriagePDF"] = "true";
                        if (!String.IsNullOrEmpty(Request.QueryString["id"]))
                        {
                            id = Request.QueryString["id"];
                            Response.Write("<script language='javascript'>window.open('JCPDFViewer.aspx?id=" + id + "', '_blank');</script>");
                        }
                        else
                        {
                            Response.Redirect("JC01Login.aspx");
                        }
                        //Response.Write("<script language='javascript'>window.open('JCPDFViewer.aspx', '_blank');</script>");
                        con.Close();
                    }
                }
                else if (dt_gettantou.Rows[0]["Shoshiki"].ToString() == "1")  //20220126 MiMi Added
                {
                    PrintSeikyuShoPDF(sender, e);
                }
            }
            else
            {
                //PrintPDF(sender, e);
            }

           
        }
        #endregion

        #region btnYes1_Click
        protected void btnYes1_Click(object sender, EventArgs e)        {
            //Save();
            //if(Session["moji"].ToString() != "true")
            //{
            //    PrintPDF(sender, e);
            //}
            if (Save())
            {
                if (Session["moji"].ToString() != "true")
                {
                    PrintPDF(sender, e);
                }
            }

        }
        #endregion

        #region btnNo1_Click
        protected void btnNo1_Click(object sender,EventArgs e)
        {
            PrintPDF(sender, e);
            //checkShoshiki(sender, e);
        }

        #endregion

        #region DDL_cTANI_SelectedIndexChanged
        protected void DDL_cTANI_SelectedIndexChanged(object sender, EventArgs e)
        {
            var ddl_tani = sender as DropDownList;            GridViewRow gvr = (GridViewRow)ddl_tani.NamingContainer;            int rowindex = gvr.RowIndex;            (GV_UriageSyohin.Rows[rowindex].FindControl("DDL_cTANI") as DropDownList).SelectedIndex = ddl_tani.SelectedIndex;            (GV_UriageSyohin.Rows[rowindex].FindControl("LB_cTANI") as Label).Text = ddl_tani.SelectedItem.ToString();            (GV_UriageSyohin.Rows[rowindex].FindControl("TB_sTANI") as TextBox).Text = ddl_tani.SelectedItem.ToString();            gridviewcheck();
           
            //Session["fedit"] = "true";
        }
        
        #endregion

        #region TB_sbikou_TextChanged
        protected void TB_sbikou_TextChanged(object sender, EventArgs e)
        {
            TextBox txt_sbikou = (TextBox)sender;            GridViewRow gvRow = (GridViewRow)txt_sbikou.NamingContainer;            int rowindex = gvRow.RowIndex;            if (txt_sbikou.Text != "")            {                string s = txt_sbikou.Text;                while (getbyte(s) > 40)                    s = s.Substring(0, s.Length - 1);                txt_sbikou.Text = s;                (GV_UriageSyohin.Rows[rowindex].FindControl("TB_sbikou") as TextBox).Text = s;            }
            gridviewcheck();
            //Session["fedit"] = "true";
        }

        protected void tb_jishabangou_TextChanged(object sender, EventArgs e)
        {
            Session["fedit"] = "true";
        }
        #endregion

        #region AreTablesEqual
        public static bool AreTablesEqual(DataTable t1, DataTable t2)        {            if (t1 == null && t2 == null) return true;            if (t1 == null || t2 == null) return false;            if (t1.Columns.Count != t2.Columns.Count || t1.Rows.Count != t2.Rows.Count) return false;            for (int i = 0; i < t1.Rows.Count; i++)            {                if (!DataRowComparer.Default.Equals(t1.Rows[i], t2.Rows[i])) return false;            }            return true;        }
        #endregion

        #region gridviewcheck()
        public void gridviewcheck()
        {
            DataTable dt_pre = new DataTable();
            dt_pre = (DataTable)ViewState["PreSyouhinTable"];
            dt_data = GetGridViewData(true);
            if (AreTablesEqual(dt_pre, dt_data))            {                Session["gvedit"] = "false";            }            else            {                Session["gvedit"] = "true";            }
            //if (AreTablesEqual(dt_pre, dt_data))
            //{
            //    Session["fedit"] = "false";
            //}
            //else
            //{
            //    Session["fedit"] = "true";
            //}
        }
        #endregion

        #region BT_Hozon_Click
        protected void BT_Hozon_Click(object sender, EventArgs e)        {            String id = Request.QueryString["id"];
            DataTable dt_db = JC01Login_Class.Get_DB(id);
            String db = dt_db.Rows[0]["db"].ToString();
            String mail = dt_db.Rows[0]["mail"].ToString();
            ConstantVal.DB_NAME = db;            JC27UriageTouroku_Class settei = new JC27UriageTouroku_Class();            Login_Tantou();            settei.cTANTOUSHA = lblLoginUserCode.Text;            DataTable dt_gettantou = settei.GetTantou();
            //bool exists = dt_gettantou.AsEnumerable().Where(c => c.Field<string>("cTANTOUSHA").Equals(lblLoginUserCode.Text)).Count() > 0;
            settei.cHENKOUSYA = lblLoginUserCode.Text;            settei.dHENKOU = DateTime.Now;            if (BT_Hidzukeari.CssClass == "JC10ZeikomiBtnActive")            {                settei.hiduke = "1";            }            else if (BT_Hidzukearinashi.CssClass == "JC10ZeikomiBtnActive")            {                settei.hiduke = "0";            }

            //Response.Cookies["Hidzuke"].Value = settei.hiduke;
            //Response.Cookies["Hidzuke"].Expires = DateTime.Now.AddYears(1);

            if (BT_kenseikyuusho.CssClass == "JC10ZeikomiBtnActive")            {                settei.Shoshiki = "1";            }            else if (BT_Nouhinsho.CssClass == "JC10ZeikomiBtnActive")            {                settei.Shoshiki = "2";            }
            //Response.Cookies["Shoshiki"].Value = settei.Shoshiki;
            //Response.Cookies["Shoshiki"].Expires = DateTime.Now.AddYears(1);

            if (BT_Kingakuari.CssClass == "JC10ZeikomiBtnActive")            {                settei.kingaku = "1";            }            else if (BT_Kingakunashi.CssClass == "JC10ZeikomiBtnActive")            {                settei.kingaku = "0";            }
            //Response.Cookies["Kingaku"].Value = settei.kingaku;
            //Response.Cookies["Kingaku"].Expires = DateTime.Now.AddYears(1);

            DataTable dt_title = settei.Get_Title();            if (!String.IsNullOrEmpty(dt_title.Rows[0]["sIMAGETitle1"].ToString()) || !String.IsNullOrEmpty(dt_title.Rows[0]["sIMAGETitle2"].ToString()) || !String.IsNullOrEmpty(dt_title.Rows[0]["sIMAGETitle3"].ToString()) || !String.IsNullOrEmpty(dt_title.Rows[0]["sIMAGETitle4"].ToString()) || !String.IsNullOrEmpty(dt_title.Rows[0]["sIMAGETitle5"].ToString()))            {                if (dt_title.Rows[0]["sIMAGETitle1"].ToString().TrimEnd() == DDL_Logo.SelectedItem.ToString().TrimEnd())                {                    settei.rogo = "1";                }                else if (dt_title.Rows[0]["sIMAGETitle2"].ToString().TrimEnd() == DDL_Logo.SelectedItem.ToString().TrimEnd())                {                    settei.rogo = "2";                }                else if (dt_title.Rows[0]["sIMAGETitle3"].ToString().TrimEnd() == DDL_Logo.SelectedItem.ToString().TrimEnd())                {                    settei.rogo = "3";                }                else if (dt_title.Rows[0]["sIMAGETitle4"].ToString().TrimEnd() == DDL_Logo.SelectedItem.ToString().TrimEnd())                {                    settei.rogo = "4";                }                else if (dt_title.Rows[0]["sIMAGETitle5"].ToString().TrimEnd() == DDL_Logo.SelectedItem.ToString().TrimEnd())                {                    settei.rogo = "5";                }            }            else            {                settei.rogo = "0";            }


            #region todelete            //if (dt_title.Rows[0]["sIMAGETitle1"].ToString().TrimEnd() == DDL_Logo.SelectedItem.ToString().TrimEnd())
                                         //{
                                         //    settei.rogo = "1";
                                         //}
                                         //else if (dt_title.Rows[0]["sIMAGETitle2"].ToString().TrimEnd() == DDL_Logo.SelectedItem.ToString().TrimEnd())
                                         //{
                                         //    settei.rogo = "2";
                                         //}
                                         //else if (dt_title.Rows[0]["sIMAGETitle3"].ToString().TrimEnd() == DDL_Logo.SelectedItem.ToString().TrimEnd())
                                         //{
                                         //    settei.rogo = "3";
                                         //}
                                         //else if (dt_title.Rows[0]["sIMAGETitle4"].ToString().TrimEnd() == DDL_Logo.SelectedItem.ToString().TrimEnd())
                                         //{
                                         //    settei.rogo = "4";
                                         //}
                                         //else if (dt_title.Rows[0]["sIMAGETitle5"].ToString().TrimEnd() == DDL_Logo.SelectedItem.ToString().TrimEnd())
                                         //{
                                         //    settei.rogo = "5";
                                         //}
                                         //else
                                         //{
                                         //    settei.rogo = "0";
                                         //}
                                         //Response.Cookies["Logo"].Value = DDL_Logo.SelectedIndex.ToString();
                                         //Response.Cookies["Logo"].Expires = DateTime.Now.AddYears(1);
#endregion
            if (DDL_bikou.SelectedIndex > -1)            {                if(!String.IsNullOrEmpty(dt_title.Rows[0]["sBIKOUTitle1"].ToString()) || !String.IsNullOrEmpty(dt_title.Rows[0]["sBIKOUTitle2"].ToString()) || !String.IsNullOrEmpty(dt_title.Rows[0]["sBIKOUTitle3"].ToString()) || !String.IsNullOrEmpty(dt_title.Rows[0]["sBIKOUTitle4"].ToString()) || !String.IsNullOrEmpty(dt_title.Rows[0]["sBIKOUTitle5"].ToString()))
                {
                    if (dt_title.Rows[0]["sBIKOUTitle1"].ToString().TrimEnd() == DDL_bikou.SelectedItem.ToString().TrimEnd())
                    {
                        settei.seikyubikou = "1";
                    }
                    else if (dt_title.Rows[0]["sBIKOUTitle2"].ToString().TrimEnd() == DDL_bikou.SelectedItem.ToString().TrimEnd())
                    {
                        settei.seikyubikou = "2";
                    }
                    else if (dt_title.Rows[0]["sBIKOUTitle3"].ToString().TrimEnd() == DDL_bikou.SelectedItem.ToString().TrimEnd())
                    {
                        settei.seikyubikou = "3";
                    }
                    else if (dt_title.Rows[0]["sBIKOUTitle4"].ToString().TrimEnd() == DDL_bikou.SelectedItem.ToString().TrimEnd())
                    {
                        settei.seikyubikou = "4";
                    }
                    else if (dt_title.Rows[0]["sBIKOUTitle5"].ToString().TrimEnd() == DDL_bikou.SelectedItem.ToString().TrimEnd())
                    {
                        settei.seikyubikou = "5";
                    }
                } 
                else                {                    settei.seikyubikou = "0";                }

                //Response.Cookies["Seikyubikou"].Value = DDL_bikou.SelectedIndex.ToString();
                //Response.Cookies["Seikyubikou"].Expires = DateTime.Now.AddYears(1);
            }            if (dt_gettantou.Rows.Count > 0)            {                settei.UpdatePrint();            }            else            {                settei.InsertPrint();            }            divLabelSave.Style["display"] = "none";            divLabelSave.Style["display"] = "flex";            updLabelSave.Update();
            //rogoindex1 = DDL_Logo.SelectedIndex;
        }

        #endregion

        #region InsatsuSettei
        public void InsatsuSettei(object sender, EventArgs e)        {

            #region delete            //if (Request.Cookies["Logo"] != null)
                                       //{
                                       //    try
                                       //    {
                                       //        String logo = Request.Cookies["Logo"].Value;
                                       //        DDL_Logo.SelectedIndex = Convert.ToInt32(logo);
                                       //    }
                                       //    catch { }
                                       //}
                                       //if (Request.Cookies["Hidzuke"] != null)
                                       //{
                                       //    try
                                       //    {
                                       //        string hidzuke = Request.Cookies["Hidzuke"].Value;
                                       //        if (hidzuke == "1")
                                       //        {
                                       //            BT_Hidzukeari_Click(sender, e);
                                       //        }
                                       //        else
                                       //        {
                                       //            BT_Hidzukearinashi_Click(sender, e);
                                       //        }
                                       //    }
                                       //    catch { }
                                       //}

            //if(Request.Cookies["Shoshiki"]!=null)
            //{
            //    try
            //    {
            //        string shoshiki = Request.Cookies["Shoshiki"].Value;
            //        if (shoshiki == "1")
            //        {
            //            BT_kenseikyuusho_Click(sender, e);
            //        }
            //        else
            //        {
            //            BT_Nouhinsho_Click(sender, e);
            //        }
            //    }
            //    catch { }
            //}

            //if(Request.Cookies["Kingaku"]!=null)
            //{
            //    try
            //    {
            //        string kingaku = Request.Cookies["Kingaku"].Value;
            //        if (kingaku == "1")
            //        {
            //            BT_Kingakuari_Click(sender, e);
            //        }
            //        else
            //        {
            //            BT_Kingakunashi_Click(sender, e);
            //        }
            //    }
            //    catch { }
            //}

            //if (Request.Cookies["Seikyubikou"] != null)
            //{
            //    try
            //    {
            //        String bikou = Request.Cookies["Seikyubikou"].Value;
            //        DDL_bikou.SelectedIndex = Convert.ToInt32(bikou);
            //    }
            //    catch { }
            //}
            #endregion

            String id = Request.QueryString["id"];
            DataTable dt_db = JC01Login_Class.Get_DB(id);
            String db = dt_db.Rows[0]["db"].ToString();
            String mail = dt_db.Rows[0]["mail"].ToString();
            ConstantVal.DB_NAME = db;

            JC27UriageTouroku_Class settei = new JC27UriageTouroku_Class();            Login_Tantou();            settei.cTANTOUSHA = lblLoginUserCode.Text;            DataTable dt_gettantou = settei.GetTantou();            if (dt_gettantou.Rows.Count > 0)            {                if (dt_gettantou.Rows[0]["hiduke"].ToString().TrimEnd() == "1")                {                    BT_Hidzukeari_Click(sender, e);                }                else                {                    BT_Hidzukearinashi_Click(sender, e);                }                if (dt_gettantou.Rows[0]["Shoshiki"].ToString().TrimEnd() == "1")                {                    BT_kenseikyuusho_Click(sender, e);                }                else                {                    BT_Nouhinsho_Click(sender, e);                }                if (dt_gettantou.Rows[0]["kingaku"].ToString().TrimEnd() == "1")                {                    BT_Kingakuari_Click(sender, e);                }                else                {                    BT_Kingakunashi_Click(sender, e);                }                DataTable dt_title = settei.Get_Title();                if (dt_gettantou.Rows[0]["rogo"].ToString().TrimEnd() == "1")                {                    DDL_Logo.Text = dt_title.Rows[0]["sIMAGETitle1"].ToString().TrimEnd();                }                else if (dt_gettantou.Rows[0]["rogo"].ToString().TrimEnd() == "2")                {                    DDL_Logo.Text = dt_title.Rows[0]["sIMAGETitle2"].ToString().TrimEnd();                }                else if (dt_gettantou.Rows[0]["rogo"].ToString().TrimEnd() == "3")                {                    DDL_Logo.Text = dt_title.Rows[0]["sIMAGETitle3"].ToString().TrimEnd();                }                else if (dt_gettantou.Rows[0]["rogo"].ToString().TrimEnd() == "4")                {                    DDL_Logo.Text = dt_title.Rows[0]["sIMAGETitle4"].ToString().TrimEnd();                }                else if (dt_gettantou.Rows[0]["rogo"].ToString().TrimEnd() == "5")                {                    DDL_Logo.Text = dt_title.Rows[0]["sIMAGETitle5"].ToString().TrimEnd();                }

                if (dt_gettantou.Rows[0]["seikyubikou"].ToString().TrimEnd() == "1")                {                    DDL_bikou.Text = dt_title.Rows[0]["sBIKOUTitle1"].ToString().TrimEnd();                }                else if (dt_gettantou.Rows[0]["seikyubikou"].ToString().TrimEnd() == "2")                {                    DDL_bikou.Text = dt_title.Rows[0]["sBIKOUTitle2"].ToString().TrimEnd();                }                else if (dt_gettantou.Rows[0]["seikyubikou"].ToString().TrimEnd() == "3")                {                    DDL_bikou.Text = dt_title.Rows[0]["sBIKOUTitle3"].ToString().TrimEnd();                }                else if (dt_gettantou.Rows[0]["seikyubikou"].ToString().TrimEnd() == "4")                {                    DDL_bikou.Text = dt_title.Rows[0]["sBIKOUTitle4"].ToString().TrimEnd();                }                else if (dt_gettantou.Rows[0]["seikyubikou"].ToString().TrimEnd() == "5")                {                    DDL_bikou.Text = dt_title.Rows[0]["sBIKOUTitle5"].ToString().TrimEnd();                }

            }

        }
        #endregion

        #region btn_CloseTokuisakiSentaku_Click  20220120 MiMi Added
        protected void btn_CloseTokuisakiSentaku_Click(object sender, EventArgs e)
        {
            ifShinkiPopup.Src = "";
            mpeShinkiPopup.Hide();
            updShinkiPopup.Update();
        }
        #endregion

        #region PrintSeikyuShoPDF
        private void PrintSeikyuShoPDF(object sender, EventArgs e)  //20220126 MiMi Added
        {
            string flagrogo = "";
            String flagbikou = "";
            String cKyoten = "";

            String id = Request.QueryString["id"];
            DataTable dt_db = JC01Login_Class.Get_DB(id);
            String db = dt_db.Rows[0]["db"].ToString();
            String mail = dt_db.Rows[0]["mail"].ToString();
            ConstantVal.DB_NAME = db;

            if (!String.IsNullOrEmpty(LB_skyoten.Text))
            {
                DataTable dt_r_uriage = new DataTable();

                JC27UriageTouroku_Class r_uriage_data = new JC27UriageTouroku_Class();
                r_uriage_data.cURIAGE = LB_Uriage_Code.Text;
                dt_r_uriage = r_uriage_data.UriageData();

                cKyoten = dt_r_uriage.Rows[0]["cCO"].ToString();
            }
            //sqlkyoten = "select ifnull(sIMAGETitle1,'') sIMAGETitle1,ifnull(sIMAGETitle2,'') sIMAGETitle2,ifnull(sIMAGETitle3,'') sIMAGETitle3,ifnull(sIMAGETitle4,'') sIMAGETitle4,ifnull(sIMAGETitle5,'') sIMAGETitle5";
            //sqlkyoten += ",ifnull(sBIKOUTitle1,'') sBIKOUTitle1,ifnull(sBIKOUTitle2,'') sBIKOUTitle2,ifnull(sBIKOUTitle3,'') sBIKOUTitle3,ifnull(sBIKOUTitle4,'') sBIKOUTitle4,ifnull(sBIKOUTitle5,'') sBIKOUTitle5";
            //sqlkyoten += " from m_j_info where cCO ='"+ cKyoten + "'";

            ConstantVal.DB_NAME = db;
            JC_ClientConnecction_Class jc = new JC_ClientConnecction_Class();
            jc.loginId = mail;

            DateTime datenow = jc.GetCurrentDate();
            String fileName = "～兼請求書～" + LB_Uriage_Code.Text + "_" + datenow.ToString("yyyyMMdd");

            con = jc.GetConnection();
            con.Open();

            #region ロゴタイトル
            ConstantVal.DB_NAME = db;
            JC27UriageTouroku_Class m_j_info = new JC27UriageTouroku_Class();
            DataTable dt = m_j_info.Get_Title();
            //DataTable dt = new DataTable();
            //MySqlCommand cmd = new MySqlCommand(sqlkyoten, con);
            //cmd.CommandTimeout = 0;
            //MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            //da.Fill(dt);
            InsatsuSettei(sender, e);
            //string a = DDL_Logo.SelectedItem.ToString();

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
                //}


                if (dt.Rows[0]["sBIKOUTitle1"].ToString().TrimEnd() == DDL_bikou.SelectedItem.ToString().TrimEnd())
                {
                    flagbikou = "1";
                }
                else if (dt.Rows[0]["sBIKOUTitle2"].ToString().TrimEnd() == DDL_bikou.SelectedItem.ToString().TrimEnd())
                {
                    flagbikou = "2";
                }
                else if (dt.Rows[0]["sBIKOUTitle3"].ToString().TrimEnd() == DDL_bikou.SelectedItem.ToString().TrimEnd())
                {
                    flagbikou = "3";
                }
                else if (dt.Rows[0]["sBIKOUTitle4"].ToString().TrimEnd() == DDL_bikou.SelectedItem.ToString().TrimEnd())
                {
                    flagbikou = "4";
                }
                else if (dt.Rows[0]["sBIKOUTitle5"].ToString().TrimEnd() == DDL_bikou.SelectedItem.ToString().TrimEnd())
                {
                    flagbikou = "5";
                }
            }
            #endregion

            seikyuusho rpt = new seikyuusho();
            rpt.stitle = "兼請求書";
            rpt.fSEIKYUU = true;
            rpt.cURIAGE = LB_Uriage_Code.Text;
            rpt.dINVOICE = LB_Uriage.Text;
            rpt.dMITUMORISAKUSEI = LB_Uriage.Text;
            if (BT_Hidzukeari.CssClass == "JC10ZeikomiBtnActive")
            {
                rpt.fhiduke = true;
            }
            else if (BT_Hidzukearinashi.CssClass == "JC10ZeikomiBtnActive")
            {
                rpt.fhiduke = false;
            }
            rpt.fbikou = flagbikou;
            rpt.loginId = mail;
            rpt.ckyoten = cKyoten;
            rpt.frogoimage = flagrogo;
            rpt.bikou = TB_Denpyoubikou.Text;
            Login_Tantou();
            rpt.Run();
            String filename = "seikyushou.pdf";
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            GrapeCity.ActiveReports.Export.Pdf.Section.PdfExport pdfExport1 = new GrapeCity.ActiveReports.Export.Pdf.Section.PdfExport();
            pdfExport1.Export(rpt.Document, ms);
            ms.Position = 0;
            Session["PDFMemoryStream"] = ms;
            Session["PDFFileName"] = fileName;
            Session["UriagePDF"] = "false";
            Session.Timeout = 480;
            if (!String.IsNullOrEmpty(Request.QueryString["id"]))
            {
                id = Request.QueryString["id"];
                Response.Write("<script language='javascript'>window.open('JCPDFViewer.aspx?id=" + id + "', '_blank');</script>");
            }
            else
            {
                Response.Redirect("JC01Login.aspx");
            }
            //Response.Write("<script language='javascript'>window.open('JCPDFViewer.aspx', '_blank');</script>");
            con.Close();
        }
        #endregion

        public static int getbyte(string txt)        {            Encoding sjisEnc = Encoding.GetEncoding("Shift_JIS");            int num = sjisEnc.GetByteCount(txt);            return num;        }


        protected void BT_UriagezumiOk_Click(object sender, EventArgs e)
        {
            String id = Request.QueryString["id"];
            DataTable dt_db = JC01Login_Class.Get_DB(id);
            String db = dt_db.Rows[0]["db"].ToString();
            String mail = dt_db.Rows[0]["mail"].ToString();
            ConstantVal.DB_NAME = db;

            JC_ClientConnecction_Class jc = new JC_ClientConnecction_Class();
            jc.loginId = mail;
            con = jc.GetConnection();

            HF_cMituJoutai.Value = "06";
            HF_sMituJoutai.Value = "売上済み";
            updHiddenField.Update();

            string update_sql = " Update r_mitumori"
                            + " Set "
                            + " dHENKOU = Now(),"
                            + " cHENKOUSYA = '" + lblLoginUserCode.Text + "',"
                            + " cJYOTAI_MITUMORI = '" + HF_cMituJoutai.Value + "'"
                            + " Where '1' = '1' And cMITUMORI = '" + LB_Mitsumori_Code.Text + "';";

            MySqlCommand cmdUpdate = new MySqlCommand();
            MySqlTransaction tr = null;
            con.Open();

            try
            {
                tr = con.BeginTransaction();
                cmdUpdate.Transaction = tr;
                cmdUpdate.CommandTimeout = 0;
                cmdUpdate = new MySqlCommand(update_sql, con);
                cmdUpdate.ExecuteNonQuery();
                tr.Commit();
            }
            catch (Exception ex)
            {
                try
                {
                    tr.Rollback();
                }
                catch
                {
                }
            }
            con.Close();

            if (LB_Uriage_Code.Text == "")
            {
                LB_Uriage_Code.Text = HF_curiage.Value;
                upd_uriagecode.Update();
                LB_cSakuseisha.Text = lblLoginUserCode.Text; //作成者
                LB_sSakuseisha.Text = lblLoginUserName.Text; //作成者
                LB_cSaishukoushinsha.Text = lblLoginUserCode.Text;//最後更新者
                LB_sSaishukoushinsha.Text = lblLoginUserName.Text;//最後更新者
                LB_SakuseiDate.Text = HF_dHenkou.Value; //作成日
                LB_KoushinDate.Text = HF_dHenkou.Value;  //最後更新日
                updSakuseiDate.Update();
                updSakuseisha.Update();
                updKoushinDate.Update();
                updKoushinsha.Update();
                BT_Pdf.Enabled = true;
                BT_Pdf.CssClass = "BlueBackgroundButton JC10SaveBtn";
                BT_Sakujo.Visible = true;
            }
            else
            {
                LB_cSaishukoushinsha.Text = lblLoginUserCode.Text;//最後更新者
                LB_sSaishukoushinsha.Text = lblLoginUserName.Text;//最後更新者
                LB_KoushinDate.Text = HF_dHenkou.Value;//最後更新日
                updKoushinDate.Update();
                updKoushinsha.Update();
            }

            divLabelSave.Style["display"] = "flex";
            updLabelSave.Update();
            Session["fedit"] = "false";
            Session["gvedit"] = "false";
            Session["moji"] = "false";
            Session.Timeout = 480;
        }

        protected void BT_UriagezumiCancel_Click(object sender, EventArgs e)
        {
            if (LB_Uriage_Code.Text == "")
            {
                LB_Uriage_Code.Text = HF_curiage.Value;
                upd_uriagecode.Update();
                LB_cSakuseisha.Text = lblLoginUserCode.Text; //作成者
                LB_sSakuseisha.Text = lblLoginUserName.Text; //作成者
                LB_cSaishukoushinsha.Text = lblLoginUserCode.Text;//最後更新者
                LB_sSaishukoushinsha.Text = lblLoginUserName.Text;//最後更新者
                LB_SakuseiDate.Text = HF_dHenkou.Value; //作成日
                LB_KoushinDate.Text = HF_dHenkou.Value;  //最後更新日
                updSakuseiDate.Update();
                updSakuseisha.Update();
                updKoushinDate.Update();
                updKoushinsha.Update();
                BT_Pdf.Enabled = true;
                BT_Pdf.CssClass = "BlueBackgroundButton JC10SaveBtn";
                BT_Sakujo.Visible = true;
            }
            else
            {
                LB_cSaishukoushinsha.Text = lblLoginUserCode.Text;//最後更新者
                LB_sSaishukoushinsha.Text = lblLoginUserName.Text;//最後更新者
                LB_KoushinDate.Text = HF_dHenkou.Value;//最後更新日
                updKoushinDate.Update();
                updKoushinsha.Update();
            }

            divLabelSave.Style["display"] = "flex";
            updLabelSave.Update();
            Session["fedit"] = "false";
            Session["gvedit"] = "false";
            Session["moji"] = "false";
            Session.Timeout = 480;
        }
    }
}