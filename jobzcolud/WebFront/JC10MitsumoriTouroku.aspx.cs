//using Common.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Service;
using Common;
using MySql.Data.MySqlClient;
using System.Reflection;
using System.Web.Services;
using Ghostscript.NET.Rasterizer;
using jobzcolud.pdf;
using System.Web.UI.HtmlControls;
using System.Text;

namespace jobzcolud.WebFront
{
    public partial class JC10MitsumoriTouroku : System.Web.UI.Page
    {
        MySqlConnection con = null;
        string sTOKUISAKIYUBIN = "";
        string sTOKUISAKIJUSYO = "";
        string sTOKUISAKIJUSYO1 = "";
        string sTOKUISAKIJUSYO2 = "";
        string sTOKUISAKITEL = "";
        string sTOKUISAKIFAX = "";
        string nTOKUISAKIKAKERITU = "";

        string sSEIKYU_YUBIN = "";
        string sSEIKYU_JUSYO1 = "";
        string sSEIKYU_JUSYO2 = "";
        string sSEIKYU_TEL = "";
        string sSEIKYU_FAX = "";
        string sSEIKYU_TKC = "";

        String cMitumori_Ko = "";

        string sTOKUISAKI_cSHIHARAI = "";
        string sTOKUISAKI_sSHIHARAI = "";

        string sSEIKYU_cSHIHARAI = "";
        string sSEIKYU_sSHIHARAIGetsu = "";

        string sTOKUISAKI_sBUMON = "";
        string sTOKUISAKI_SYAKUSHOKU = "";
        string sTOKUISAKI_SKEISHOU = "";

        string sSEIKYU_sBUMON = "";
        string sSEIKYU_SYAKUSHOKU = "";
        string sSEIKYU_SKEISHOU = "";
        string sSEIKYU_SHIMEBI = "";
        string sSEIKYU_SHIHARAIBI = "";
        string sSEIKYU_BIKO = "";

        String fImageUpload = "";
        DataTable Syousai_All;
        DataTable DBL_JISHABANGOU;
        private bool fAdd = false;

        private decimal nriritsu = 100;
        private int nkisuu = 0;
        private int nshisyagonyuu = 0;
        private int nGYOUZENTAI = 0;
        private bool fuwagakis = true;
        private bool fuwagakim = true;

        private double syouhizei = 0.05;  //消費税

        bool saveSuccess = false;

        String sql_Delete = "";

        DataTable dt_UriageKomoku;
        DataTable dt_SyohinKomoku;
        String linkcUriucol = "";
        String linkcMitucol = "";

        //int midashiTextboxWidth = 0;
        //int BeforeSyoekiTextboxWidth = 0;
        //int AfterSyoekiTextboxWidth = 0;
        String GokeiColumn_Position = "No";
        int GokeiColumn = 0;
        Boolean beforegokeiColumn = true;
        Boolean flagJoutai = false;

        public static int rmt_pcount1 = 0;//Page Numberの為に
        public static int pcount = 0;
        bool fMEISAI = false; //明細チェック
        bool fSYOUSAI = false; //詳細チェック
        bool fMIDASHI = false; //見出しチェック
        bool fHYOUSHI = false; //表紙チェック

        bool f_isomoji_msg = true;
        short tabIndex = 1;

        int GokeiCol = 0; // 20220519 maythu add
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(Request.QueryString["id"]))
                {
                    if (!IsPostBack)
                    {
                        try
                        {
                            String id = Request.QueryString["id"];
                            DataTable dt_db = JC01Login_Class.Get_DB(id);
                            String db = dt_db.Rows[0]["db"].ToString();
                            String mail = dt_db.Rows[0]["mail"].ToString();
                            ConstantVal.DB_NAME = db;

                            JC_ClientConnecction_Class jc = new JC_ClientConnecction_Class();
                            jc.loginId = mail;
                            MySqlConnection cn = jc.GetConnection();
                            DataTable dt_loginuser = jc.GetLoginUserCodeFromClientDB();
                            lblLoginUserCode.Text = dt_loginuser.Rows[0]["code"].ToString();

                            DataTable dt_kakudo = jc.GetKakudo();
                            DDL_Kakudo.DataSource = dt_kakudo;
                            DDL_Kakudo.DataTextField = "sKAKUDO";
                            DDL_Kakudo.DataValueField = "cKAKUDO";
                            DDL_Kakudo.DataBind();

                            JC99NavBar navbar_Master = (JC99NavBar)this.Master;
                            navbar_Master.LK_navbardrop1.Style.Add(" background-color", "rgba(46,117,182)");
                            navbar_Master.navbar2.Visible = false;
                            JC99NavBar_Class Jc99 = new JC99NavBar_Class();
                            Jc99.loginId = mail;
                            Jc99.FindLoginName();
                            lblLoginUserName.Text = Jc99.LoginName;

                            if (Request.Cookies["colWidthmUraige"] != null)
                            {
                                HF_GridSizeUriage.Value = Request.Cookies["colWidthmUraige"].Value;
                            }
                            else
                            {
                                HF_GridSizeUriage.Value = "LB_drop,30:cUriage,95:cMitumori,95:sSeikyusaki,160:sTokuisaki,160:sUriage,160:sTantou,160:dUriage,95:nUriage,110:UriageJoutai,70:sMemo,165:";
                            }
                            if (Request.Cookies["colWidthmUraigeGrid"] != null)
                            {
                                HF_GridUriage.Value = Request.Cookies["colWidthmUraigeGrid"].Value;
                            }
                            else
                            {
                                HF_GridUriage.Value = "1356";
                            }
                            if (Request.Cookies["colWidthmSyouhin"] != null)
                            {
                                HF_GridSizeSyouhin.Value = Request.Cookies["colWidthmSyouhin"].Value;
                            }
                            else
                            {
                                HF_GridSizeSyouhin.Value = "checkbox,20:AddSyouhin,30:CopySyouhin,30:SyouhinSyosai,30:Kubun,30:cSyouhin,95:Syouhin,30:" +
                                    "sSyouhin,300:Syouryou,70:tani,58:Hyoujuntanka,115:Tanka,115:kingaku,115:gentanka,115:ritsu,55:genkagokei,115:" +
                                    "arari,115:araritsu,115:drag,30:dropdown,25:";
                            }
                            if (Request.Cookies["colWidthmSyouhinGrid"] != null)
                            {
                                HF_GridSyouhin.Value = Request.Cookies["colWidthmSyouhinGrid"].Value;
                            }
                            else
                            {
                                HF_GridSyouhin.Value = "1585";
                            }

                            #region 拠点

                            ConstantVal.DB_NAME = db;
                            DataTable dtKyoten = jc.GetKyoten(lblLoginUserCode.Text);
                            lblcKYOTEN.Text = dtKyoten.Rows[0]["cKYOTEN"].ToString();
                            if (!String.IsNullOrEmpty(lblcKYOTEN.Text))
                            {
                                lblsKYOTEN.Text = dtKyoten.Rows[0]["sKYOTEN"].ToString().Replace("<", "&lt").Replace(">", "&gt");
                                lblsKYOTEN.Attributes.Add("onClick", "BtnClick('MainContent_btnKyotenAdd')");
                                divKyotenbtn.Style["display"] = "none";
                                divKyotenLabel.Style["display"] = "block";
                                updKyoten.Update();
                                updSentakuPopup.Update();
                            }
                            #endregion


                            lkMitumori.Style.Add(" background-color", "rgba(191,191,191)");
                            lkPrint.Style.Add(" background-color", "rgba(242,242,242)");
                            lkUriage.Style.Add(" background-color", "rgba(242,242,242)");
                            divMitumoriTorokuP.Attributes.Add("class", "JC10MitumoriTourokuDiv");
                            divMitumoriInsatsu.Attributes.Add("class", "DisplayNone");
                            divMitumoriUriage.Attributes.Add("class", "DisplayNone");
                            //updHeader.Update();

                            if (Session["cMitumori"] == null)  //新規
                            {
                                lblcJISHATANTOUSHA.Text = lblLoginUserCode.Text;
                                lblsJISHATANTOUSHA.Text = lblLoginUserName.Text.Replace("<", "&lt").Replace(">", "&gt");
                                divTantousyaBtn.Style["display"] = "none";
                                divTantousyaLabel.Style["display"] = "block";
                                lblsJISHATANTOUSHA.Attributes.Add("onClick", "BtnClick('MainContent_BT_JisyaTantousya_Add')");
                                upd_JISHATANTOUSHA.Update();

                                lblcMitumori.Text = "";
                                lblcMitumori_Ko.Text = "01";
                                if (Session["cBukken"] != null)
                                {
                                    lnkcBukken.Text = Session["cBukken"].ToString();

                                    if (Session["sBukken"] != null)
                                    {
                                        txtsMitumori.Text = Session["sBukken"].ToString();
                                    }
                                    else
                                    {
                                        txtsMitumori.Text = "";
                                    }
                                    getSeikyuTantouByBukken();
                                }
                                else
                                {
                                    lnkcBukken.Text = "";
                                }
                                btnBetsuMitumoriSave.Visible = false;
                                btnMitumoriDelete.Visible = false;
                                btnMitumorishoPDF.Enabled = false;
                                btnMitumorishoPDF.CssClass = "JC10SaveBtnDisable";
                                btnNewMitumori.Text = "見積を連続で作成";


                                DataTable dt = CreateSyouhinTableColomn();
                                for (int i = 1; i <= 15; i++)
                                {
                                    DataRow dr = dt.NewRow();
                                    dr[0] = "0";
                                    dr[1] = "";
                                    dr[2] = "";
                                    dr[3] = "";
                                    dr[4] = "";
                                    dr[5] = "";
                                    dr[6] = "";
                                    dr[7] = "";
                                    dr[8] = "";
                                    dr[9] = "";
                                    dr[10] = "";
                                    dr[11] = "1";
                                    dr[12] = "0";
                                    dr[13] = "100%";
                                    dr[14] = "";
                                    dr[15] = "";
                                    dr[16] = "0";
                                    dr[17] = "0";
                                    dt.Rows.Add(dr);
                                }

                                var max = dt.AsEnumerable().Max(x => int.Parse(x.Field<string>("rowNo")));
                                HF_maxRowNo.Value = max.ToString();
                                GV_MitumoriSyohin_Original.DataSource = dt;
                                GV_MitumoriSyohin_Original.DataBind();
                                updMitsumoriSyohinGrid.Update();

                                ViewState["SyouhinTable"] = dt;
                                SyohinKoumokuSort();

                                HasCheckRow();
                                ConstantVal.DB_NAME = db;
                                DateTime datenow = jc.GetCurrentDate();

                                lbldMitumori.Text = datenow.ToString("yyyy/MM/dd");
                                btnMitumoriDate.Style["display"] = "none";
                                divMitumoriDate.Style["display"] = "block";
                                updMitumoriDate.Update();
                                lbldMitumori.Attributes.Add("onClick", "BtnClick('MainContent_btnMitumoriDate')");
                                upddatePopup.Update();

                                lblcJoutai.Text = "04";
                                lblsJoutai.Text = "見積作成中";
                                lblsJoutai.Attributes.Add("onClick", "BtnClick('MainContent_btnJoutai')");
                                divJoutaibtn.Style["display"] = "none";
                                divJoutaiLabel.Style["display"] = "block";
                                updJoutai.Update();

                                HF_isChange.Value = "1";
                            }
                            else //更新
                            {
                                lblcMitumori.Text = Session["cMitumori"].ToString();
                                getMitumoriData();
                                getSyouhinData();
                                getSyosaiSyouhinData();
                                LoadImage();
                                HasCheckRow();
                                if (Session["btko"] != null)
                                {
                                    if (Session["cBukken"] != null)
                                    {
                                        lnkcBukken.Text = Session["cBukken"].ToString();
                                        getSeikyuTantouByBukken();
                                    }
                                    else
                                    {
                                        lnkcBukken.Text = "";
                                    }

                                    lblcMitumori.Text = "";
                                    btnBetsuMitumoriSave.Visible = false;
                                    btnMitumoriDelete.Visible = false;
                                    btnMitumorishoPDF.Enabled = false;
                                    btnMitumorishoPDF.CssClass = "JC10SaveBtnDisable";
                                    btnCreateUriage.Enabled = false;
                                    btnCreateUriage.CssClass = "JC10SaveBtnDisable";
                                    btnUriage.Enabled = false;
                                    btnUriage.CssClass = "JC10SaveBtnDisable";
                                    Session["btko"] = null;
                                    lblSakusekibi.Text = "";
                                    lblSakuseisya.Text = "";
                                    lblcSakuseisya.Text = "";
                                    lblHenkoubi.Text = "";
                                    lblHenkousya.Text = "";
                                    lblcHenkousya.Text = "";
                                    lblcMitumori_Ko.Text = "01";
                                }
                                HF_isChange.Value = "0";
                            }
                            SetSyosai();
                            if (String.IsNullOrEmpty(lbldJuuchu.Text))
                            {
                                btnCreateUriage.Enabled = false;
                                btnCreateUriage.CssClass = "JC10SaveBtnDisable";
                                btnUriage.Enabled = false;
                                btnUriage.CssClass = "JC10SaveBtnDisable";
                            }
                            else
                            {
                                btnCreateUriage.Enabled = true;
                                btnCreateUriage.CssClass = "BlueBackgroundButton JC10SaveBtn";
                                btnUriage.Enabled = true;
                                btnUriage.CssClass = "BlueBackgroundButton JC10SaveBtn";
                            }
                            updHeader.Update();
                        }
                        catch(Exception ex)
                        {
                            Response.Redirect("JC01Login.aspx");
                        }
                    }
                    else
                    {
                        //if (fileUpload1.HasFile)
                        //{
                        //    BindImage();
                        //}
                        if (divLabelSave.Style["display"] != "none")
                        {
                            divLabelSave.Style["display"] = "none";
                            updHeader.Update();
                        }

                        if (divMitumoriUriage.Attributes["class"] == "JC10MitumoriTourokuDiv")
                        {
                            UriageKoumokuSort();
                        }
                        else if (divMitumoriTorokuP.Attributes["class"] == "JC10MitumoriTourokuDiv")
                        {
                            SyohinKoumokuSort();
                        }

                    }
                    Session.Timeout = 480;

                }
                else
                {
                    Response.Redirect("JC01Login.aspx");
                }
            }
            catch
            {
                Response.Redirect("JC01Login.aspx");
            }
        }

        #region getSeikyuTantouByBukken
        private void getSeikyuTantouByBukken()
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
            string sql = "select" +
                " ifnull(rb.sBUKKEN, '') as sBUKKEN," +
                " ifnull(rb.cTOKUISAKI, '') as cTOKUISAKI," +
                " ifnull(rb.sTOKUISAKI, '') as sTOKUISAKI," +
                " ifnull(rb.sTOKUISAKI_TAN, '') as sTOKUISAKI_TAN," +
                " ifnull(rb.sTOKUISAKI_TAN_Jun, '') as sTOKUISAKI_TAN_Jun," +
                " ifnull(mk.cSHIHARAI, '') as cSHIHARAI," +                    
                " ifnull(mk.sSHIHARAI, '') as sSHIHARAI," +
                " ifnull(mk.cSEIKYUSAKI, '') as cSEIKYUSAKI," + //20220428 MyatNoe
                " ifnull(mk.sSEIKYUSAKI, '') as sSEIKYUSAKI," +  //20220428 MyatNoe
                " ifnull(rb.cPJ, '') as cPJ"+
                " from R_BUKKEN rb" +
                " left join m_tokuisaki mk on mk.cTOKUISAKI=rb.cTOKUISAKI " + 
                " where rb.cBUKKEN like '%" + lnkcBukken.Text + "%'";
            MySqlCommand cmd = new MySqlCommand(sql, cn);
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable dtable = new DataTable();
            da.Fill(dtable);
            cn.Close();
            da.Dispose();
            if (dtable.Rows.Count > 0)
            {
                if (!String.IsNullOrEmpty(dtable.Rows[0]["cTOKUISAKI"].ToString()))
                {
                    lblcTOKUISAKI.Text = dtable.Rows[0]["cTOKUISAKI"].ToString();
                    lblsTOKUISAKI.Text = dtable.Rows[0]["sTOKUISAKI"].ToString().Replace("<", "&lt").Replace(">", "&gt");
                    divTokuisakiBtn.Style["display"] = "none";
                    divTokuisakiLabel.Style["display"] = "block";
                    divTokuisakiSyosai.Style["display"] = "block";
                    btnTokuisaki.BorderStyle = BorderStyle.None;
                    lblsTOKUISAKI.Attributes.Add("onClick", "BtnClick('MainContent_btnTokuisaki')");
                    updTokuisaki.Update();

                    lblcSEIKYUSAKI.Text = dtable.Rows[0]["cTOKUISAKI"].ToString();
                    lblsSEIKYUSAKI.Text = dtable.Rows[0]["sTOKUISAKI"].ToString().Replace("<", "&lt").Replace(">", "&gt");
                    divSEIKYUSAKIBtn.Style["display"] = "none";
                    divsSEIKYUSAKILabel.Style["display"] = "block";
                    divSEIKYUSAKISyosai.Style["display"] = "block";
                    btnSeikyusaki.BorderStyle = BorderStyle.None;
                    lblsSEIKYUSAKI.Attributes.Add("onClick", "BtnClick('MainContent_btnSeikyusaki')");
                    updSEIKYUSAKI.Update();
                }
                if (!String.IsNullOrEmpty(dtable.Rows[0]["sTOKUISAKI_TAN"].ToString()))
                {
                    lblsTOKUISAKI_TAN.Text = dtable.Rows[0]["sTOKUISAKI_TAN"].ToString().Replace("<", "&lt").Replace(">", "&gt");
                    lblsTOKUISAKI_TAN_JUN.Text = dtable.Rows[0]["sTOKUISAKI_TAN_Jun"].ToString();
                    divTokuisakiTanBtn.Style["display"] = "none";
                    divTokuisakiTanLabel.Style["display"] = "block";
                    divTokuisakiTanSyosai.Style["display"] = "block";
                    lblsTOKUISAKI_TAN.Attributes.Add("onClick", "BtnClick('MainContent_btnTokuisakiTantou')");
                    updTokuisakiTantou.Update();

                    lblsSEIKYUSAKI_TAN.Text = dtable.Rows[0]["sTOKUISAKI_TAN"].ToString().Replace("<", "&lt").Replace(">", "&gt");
                    lblsSEIKYUSAKI_TAN_JUN.Text = dtable.Rows[0]["sTOKUISAKI_TAN_Jun"].ToString();
                    divSeikyusakiTanBtn.Style["display"] = "none";
                    divSeikyusakiTanLabel.Style["display"] = "block";
                    divSeikyusakiTanSyosai.Style["display"] = "block";
                    lblsSEIKYUSAKI_TAN.Attributes.Add("onClick", "BtnClick('MainContent_btnSeikyusakiTantou')");
                    updSeikyusakiTantou.Update();
                }

                #region 20220421 MyatNoe
                if (!String.IsNullOrEmpty(dtable.Rows[0]["cSHIHARAI"].ToString()))
                {
                    lblcShiharai.Text = dtable.Rows[0]["cSHIHARAI"].ToString();
                    lblsShihariai.Text = dtable.Rows[0]["sSHIHARAI"].ToString().Replace("<", "&lt").Replace(">", "&gt");
                    divShiharaibtn.Style["display"] = "none";
                    divShiharaiLabel.Style["display"] = "block";
                    lblsShihariai.Attributes.Add("onClick", "BtnClick('MainContent_btnShiharai')");
                    updShiharai.Update();
                }
                #endregion

                #region 20220428 MyatNoe
                if (!String.IsNullOrEmpty(dtable.Rows[0]["cSEIKYUSAKI"].ToString()))
                {
                    lblcSEIKYUSAKI.Text = dtable.Rows[0]["cSEIKYUSAKI"].ToString();
                    lblsSEIKYUSAKI.Text = dtable.Rows[0]["sSEIKYUSAKI"].ToString().Replace("<", "&lt").Replace(">", "&gt");
                    divSEIKYUSAKIBtn.Style["display"] = "none";
                    divsSEIKYUSAKILabel.Style["display"] = "block";
                    divSEIKYUSAKISyosai.Style["display"] = "block";
                    btnSeikyusaki.BorderStyle = BorderStyle.None;
                    lblsSEIKYUSAKI.Attributes.Add("onClick", "BtnClick('MainContent_btnSeikyusaki')");
                    updSEIKYUSAKI.Update();
                }
                else
                {
                    lblcSEIKYUSAKI.Text = dtable.Rows[0]["cTOKUISAKI"].ToString();
                    lblsSEIKYUSAKI.Text = dtable.Rows[0]["sTOKUISAKI"].ToString().Replace("<", "&lt").Replace(">", "&gt");
                    divSEIKYUSAKIBtn.Style["display"] = "none";
                    divsSEIKYUSAKILabel.Style["display"] = "block";
                    divSEIKYUSAKISyosai.Style["display"] = "block";
                    btnSeikyusaki.BorderStyle = BorderStyle.None;
                    lblsSEIKYUSAKI.Attributes.Add("onClick", "BtnClick('MainContent_btnSeikyusaki')");
                    updSEIKYUSAKI.Update();
                }
                #endregion

                txtJisyaBango.Text = dtable.Rows[0]["cPJ"].ToString();
                if (Session["fBukkenName"] != null)
                {
                    if (Session["fBukkenName"].ToString() == "true")
                    {
                        txtsMitumori.Text = dtable.Rows[0]["sBUKKEN"].ToString();
                    }
                }
            }
        }
        #endregion

        #region "グリッドに商品行追加"
        protected void btnSyouhinAdd_Click(object sender, EventArgs e)
        {
            Button btn_addSyohin = (Button)sender;
            GridViewRow gvRow = (GridViewRow)btn_addSyohin.NamingContainer;
            int rowID = gvRow.RowIndex + 1;

            DataTable dt = GetGridViewData();

            if (ViewState["SyouhinTable"] != null)
            {
                //if (dt.Rows.Count > 1)
                //{
                DataRow dr = dt.NewRow();
                dr[0] = "0";
                dr[1] = "";
                dr[2] = "";
                dr[3] = "";
                dr[4] = "";
                dr[5] = "";
                dr[6] = "";
                dr[7] = "";
                dr[8] = "";
                dr[9] = "";
                dr[10] = "";
                dr[11] = "1";
                dr[12] = "0";
                dr[13] = "100%";
                dr[14] = "";
                dr[15] = "";
                dr[16] = "0";
                dr[17] = "0";
                dt.Rows.InsertAt(dr, rowID);
                //}

                var max = dt.AsEnumerable().Max(x => int.Parse(x.Field<string>("rowNo")));
                HF_maxRowNo.Value = max.ToString();

                dt = SetMidashiSyokei(dt);
                ViewState["SyouhinTable"] = dt;
                GV_MitumoriSyohin_Original.DataSource = dt;
                GV_MitumoriSyohin_Original.DataBind();

                DataTable dt_Syosai = GetSyosaiGridViewData();

                setSyosaiCount(dt, dt_Syosai);
                updMitsumoriSyohinGrid.Update();
            }

            if (GV_MitumoriSyohin_Original.Rows.Count == 0)
            {
                BT_SyohinEmptyAdd.CssClass = "JC10GrayButton";
            }
            else
            {
                BT_SyohinEmptyAdd.CssClass = "DisplayNone";
            }
            //DataTable dt_SyohinOriginal = GetGridViewData();
            DataTable dt_SyohinOriginal = new DataTable();
            dt_SyohinOriginal = dt;
            GV_MitumoriSyohin.DataSource = dt_SyohinOriginal;
            GV_MitumoriSyohin.DataBind();
            HasCheckRow();
            //SetSyosai();
            updMitsumoriSyohinGrid.Update();
            HF_isChange.Value = "1";
            updHeader.Update();
        }

        protected void BT_SyohinEmptyAdd_Click(object sender, EventArgs e)
        {
            DataTable dt = GetGridViewData();

            if (ViewState["SyouhinTable"] != null)
            {
                DataRow dr = dt.NewRow();
                dr[0] = "0";
                dr[1] = "";
                dr[2] = "";
                dr[3] = "";
                dr[4] = "";
                dr[5] = "";
                dr[6] = "";
                dr[7] = "";
                dr[8] = "";
                dr[9] = "";
                dr[10] = "";
                dr[11] = "1";
                dr[12] = "0";
                dr[13] = "100%";
                dr[14] = "";
                dr[15] = "";
                dr[16] = "0";
                dr[17] = "0";
                dt.Rows.Add(dr);

                var max = dt.AsEnumerable().Max(x => int.Parse(x.Field<string>("rowNo")));
                HF_maxRowNo.Value = max.ToString();

                dt = SetMidashiSyokei(dt);
                ViewState["SyouhinTable"] = dt;
                GV_MitumoriSyohin_Original.DataSource = dt;
                GV_MitumoriSyohin_Original.DataBind();

                DataTable dt_Syosai = GetSyosaiGridViewData();

                setSyosaiCount(dt, dt_Syosai);
                updMitsumoriSyohinGrid.Update();
            }

            if (GV_MitumoriSyohin_Original.Rows.Count == 0)
            {
                BT_SyohinEmptyAdd.CssClass = "JC10GrayButton";
            }
            else
            {
                BT_SyohinEmptyAdd.CssClass = "DisplayNone";
            }
            //DataTable dt_SyohinOriginal = GetGridViewData();
            DataTable dt_SyohinOriginal = new DataTable();
            dt_SyohinOriginal = dt;
            ViewState["SyouhinTable"] = dt_SyohinOriginal;
            GV_MitumoriSyohin.DataSource = dt_SyohinOriginal;
            GV_MitumoriSyohin.DataBind();
            HasCheckRow();
            //SetSyosai();
            updMitsumoriSyohinGrid.Update();
            HF_isChange.Value = "1";
            updHeader.Update();
        }
        #endregion

        #region CreateSyouhinTableColomn
        private DataTable CreateSyouhinTableColomn()
        {
            DataTable dt_Syohin = new DataTable();
            dt_Syohin.Columns.Add("status");
            dt_Syohin.Columns.Add("cSYOHIN");
            dt_Syohin.Columns.Add("sSYOHIN");
            dt_Syohin.Columns.Add("nSURYO");
            dt_Syohin.Columns.Add("cTANI");
            dt_Syohin.Columns.Add("nTANKA");
            dt_Syohin.Columns.Add("nTANKAGOUKEI");
            dt_Syohin.Columns.Add("nGENKATANKA");
            dt_Syohin.Columns.Add("nGENKAGOUKEI");
            dt_Syohin.Columns.Add("nARARI");
            dt_Syohin.Columns.Add("nARARISu");
            dt_Syohin.Columns.Add("fgentankatanka");
            dt_Syohin.Columns.Add("rowNo");
            dt_Syohin.Columns.Add("nRITU");
            dt_Syohin.Columns.Add("sKUBUN");
            dt_Syohin.Columns.Add("nSIKIRITANKA");
            dt_Syohin.Columns.Add("fJITAIS");
            dt_Syohin.Columns.Add("fJITAIQ");
            return dt_Syohin;
        }
        #endregion

        #region GetGridViewData
        private DataTable GetGridViewData()
        {
            DataTable dt = new DataTable();
            if (ViewState["SyouhinTable"] == null)
            {
                dt = CreateSyouhinTableColomn();
                foreach (GridViewRow row in GV_MitumoriSyohin_Original.Rows)
                {
                    Label lbl_status = (row.FindControl("lblhdnStatus") as Label);
                    Label lbl_fgenkataka = (row.FindControl("lblfgenkatanka") as Label);
                    Label lbl_rowNo = (row.FindControl("lblRowNo") as Label);
                    TextBox txt_cSyohin = (row.FindControl("txtcSYOHIN") as TextBox);
                    TextBox txt_sSyohin = (row.FindControl("txtsSYOHIN") as TextBox);
                    TextBox txt_nSyoryo = (row.FindControl("txtnSURYO") as TextBox);
                    //DropDownList ddl_cTani = (row.FindControl("DDL_cTANI") as DropDownList);
                    TextBox txt_cTani = (row.FindControl("txtTani") as TextBox);
                    TextBox txt_nTanka = (row.FindControl("txtnTANKA") as TextBox);
                    Label lbl_TankaGokei = (row.FindControl("lblTankaGokei") as Label);
                    TextBox txt_nGenkaTanka = (row.FindControl("txtnGENKATANKA") as TextBox);
                    Label lbl_GenkaGokei = (row.FindControl("lblGenkaGokei") as Label);
                    Label lbl_Arari = (row.FindControl("lblnARARI") as Label);
                    Label lbl_ArariSu = (row.FindControl("lblnARARISu") as Label);
                    TextBox txt_nRITU = (row.FindControl("txtnRITU") as TextBox);
                    Label lbl_kubun = (row.FindControl("lblKubun") as Label);
                    Label lbl_nSIKIRITANKA = (row.FindControl("lblTanka") as Label);
                    Label lbl_fJITAIS = (row.FindControl("lblfjitais") as Label);
                    Label lbl_fJITAIQ = (row.FindControl("lblfjitaiq") as Label);

                    DataRow dr = dt.NewRow();
                    dr[0] = lbl_status.Text;
                    dr[1] = txt_cSyohin.Text;
                    dr[2] = txt_sSyohin.Text;
                    dr[3] = txt_nSyoryo.Text;
                    dr[4] = txt_cTani.Text;
                    dr[5] = txt_nTanka.Text;
                    dr[6] = lbl_TankaGokei.Text;
                    dr[7] = txt_nGenkaTanka.Text;
                    dr[8] = lbl_GenkaGokei.Text;
                    dr[9] = lbl_Arari.Text;
                    dr[10] = lbl_ArariSu.Text;
                    dr[11] = lbl_fgenkataka.Text;
                    dr[12] = lbl_rowNo.Text;
                    dr[13] = txt_nRITU.Text;
                    dr[14] = lbl_kubun.Text;
                    dr[15] = lbl_nSIKIRITANKA.Text;
                    dr[16] = lbl_fJITAIS.Text;
                    dr[17] = lbl_fJITAIQ.Text;
                    dt.Rows.Add(dr);
                }

                ViewState["SyouhinTable"] = dt;
            }
            else
            {
                dt = ViewState["SyouhinTable"] as DataTable;
            }
            return dt;
        }
        #endregion

        #region GetSyosaiGridViewData
        private DataTable GetSyosaiGridViewData()
        {
            DataTable dt = CreateSyosaiTableColomn();
            foreach (GridViewRow row in GV_Syosai.Rows)
            {
                Label lbl_status = (row.FindControl("lblhdnStatus") as Label);
                Label lbl_rowno = (row.FindControl("lblRowNo") as Label);
                TextBox txt_cSyohin = (row.FindControl("txtcSYOHIN") as TextBox);
                TextBox txt_sSyohin = (row.FindControl("txtsSYOHIN") as TextBox);
                TextBox txt_nSyoryo = (row.FindControl("txtnSURYO") as TextBox);
                Label lbl_cTani = (row.FindControl("lblcTANI") as Label);
                TextBox txt_nTanka = (row.FindControl("txtnTANKA") as TextBox);
                Label lbl_TankaGokei = (row.FindControl("lblTankaGokei") as Label);
                TextBox txt_nGenkaTanka = (row.FindControl("txtnGENKATANKA") as TextBox);
                Label lbl_GenkaGokei = (row.FindControl("lblGenkaGokei") as Label);
                Label lbl_Arari = (row.FindControl("lblnARARI") as Label);
                Label lbl_ArariSu = (row.FindControl("lblnARARISu") as Label);
                TextBox txt_nRitu = (row.FindControl("txtnRITU") as TextBox);
                Label lbl_nSikiritanka = (row.FindControl("lblTanka") as Label);
                Label lbl_fJITAIS = (row.FindControl("lblfjitais") as Label);

                DataRow dr = dt.NewRow();
                dr[0] = lbl_status.Text;
                dr[1] = txt_cSyohin.Text;
                dr[2] = txt_sSyohin.Text;
                dr[3] = txt_nSyoryo.Text;
                dr[4] = lbl_cTani.Text;
                dr[5] = txt_nTanka.Text;
                dr[6] = lbl_TankaGokei.Text;
                dr[7] = txt_nGenkaTanka.Text;
                dr[8] = lbl_GenkaGokei.Text;
                dr[9] = lbl_Arari.Text;
                dr[10] = lbl_ArariSu.Text;
                dr[11] = lbl_rowno.Text;
                dr[12] = txt_nRitu.Text;
                dr[13] = lbl_nSikiritanka.Text;
                dr[14] = lbl_fJITAIS.Text;
                dt.Rows.Add(dr);
            }
            return dt;
        }
        #endregion

        #region グリッドに商品行複写（該当行の下にコピー）
        protected void btnSyohinCopy_Click(object sender, EventArgs e)
        {
            Button btnSyohinCopy = (Button)sender;
            GridViewRow gvRow = (GridViewRow)btnSyohinCopy.NamingContainer;
            int rowID = gvRow.RowIndex + 1;
            String Syosai = (gvRow.FindControl("btnSyohinShosai") as Button).Text;
            String RowNo = (gvRow.FindControl("lblRowNo") as Label).Text;

            DataTable dt = GetGridViewData();
            if (ViewState["SyouhinTable"] != null)
            {
                var dr_copy = dt.NewRow();
                DataRow dr_exist = dt.Rows[gvRow.RowIndex];
                dr_copy.ItemArray = dr_exist.ItemArray.Clone() as object[]; ;
                dt.Rows.InsertAt(dr_copy, rowID);
                //var rowNo_col = dt.Ta.Select("rowNo =MAX(rowNo)");
                var max = dt.AsEnumerable().Max(x => int.Parse(x.Field<string>("rowNo")));
                dt.Rows[rowID]["rowNo"] = (Convert.ToInt32(max) + 1).ToString();
                HF_maxRowNo.Value = (Convert.ToInt32(max) + 1).ToString();
                dt = SetMidashiSyokei(dt);
                ViewState["SyouhinTable"] = dt;
                GV_MitumoriSyohin_Original.DataSource = dt;
                GV_MitumoriSyohin_Original.DataBind();
                updMitsumoriSyohinGrid.Update();


                DataTable dt_Syosai = GetSyosaiGridViewData();
                if (Syosai != "詳")
                {
                    DataRow[] rows = dt_Syosai.Select("rowNo = '" + RowNo + "'");
                    if (rows.Length > 0)
                    {
                        foreach (var drow in rows)
                        {
                            DataRow dr = dt_Syosai.NewRow();
                            dr[0] = drow[0];
                            dr[1] = drow[1];
                            dr[2] = drow[2];
                            dr[3] = drow[3];
                            dr[4] = drow[4];
                            dr[5] = drow[5];
                            dr[6] = drow[6];
                            dr[7] = drow[7];
                            dr[8] = drow[8];
                            dr[9] = drow[9];
                            dr[10] = drow[10];
                            dr[11] = (Convert.ToInt32(max) + 1).ToString();
                            dr[12] = drow[12];
                            dr[13] = drow[13];
                            dr[14] = drow[14];
                            dt_Syosai.Rows.Add(dr);
                        }

                        dt_Syosai.DefaultView.Sort = "rowNo asc";
                        dt_Syosai.AcceptChanges();

                        GV_Syosai.DataSource = dt_Syosai;
                        GV_Syosai.DataBind();
                    }
                }

                setSyosaiCount(dt, dt_Syosai);

            }
            //DataTable dt_SyohinOriginal = GetGridViewData();
            DataTable dt_SyohinOriginal = new DataTable();
            dt_SyohinOriginal = dt;
            ViewState["SyouhinTable"] = dt_SyohinOriginal;
            GV_MitumoriSyohin.DataSource = dt_SyohinOriginal;
            GV_MitumoriSyohin.DataBind();
            HasCheckRow();
            //SetSyosai();
            updMitsumoriSyohinGrid.Update();
            HF_isChange.Value = "1";
            GetTotalKingaku();
            updHeader.Update();
        }
        #endregion

        #region btnFukusuCopy_Click  商品複数行複写
        protected void btnFukusuCopy_Click(object sender, EventArgs e)
        {
            DataTable dt = CreateSyouhinTableColomn();
            DataTable dt_Copy = CreateSyouhinTableColomn();
            DataTable dt_Syosai = GetSyosaiGridViewData();
            int maxRowNo = Convert.ToInt32(HF_maxRowNo.Value);
            int Original_RowCount = GV_MitumoriSyohin_Original.Rows.Count;
            foreach (GridViewRow row in GV_MitumoriSyohin_Original.Rows)
            {
                CheckBox chk = (GV_MitumoriSyohin_Original.Rows[row.RowIndex].FindControl("chkSelectSyouhin") as CheckBox);
                Label lbl_status = (row.FindControl("lblhdnStatus") as Label);
                Label lbl_fgenkataka = (row.FindControl("lblfgenkatanka") as Label);
                Button btn_Syosai = (row.FindControl("btnSyohinShosai") as Button);
                Label lbl_rowNo = (row.FindControl("lblRowNo") as Label);
                TextBox txt_cSyohin = (row.FindControl("txtcSYOHIN") as TextBox);
                TextBox txt_sSyohin = (row.FindControl("txtsSYOHIN") as TextBox);
                TextBox txt_nSyoryo = (row.FindControl("txtnSURYO") as TextBox);
                //DropDownList ddl_cTani = (row.FindControl("DDL_cTANI") as DropDownList);
                TextBox txt_cTani = (row.FindControl("txtTani") as TextBox);
                TextBox txt_nTanka = (row.FindControl("txtnTANKA") as TextBox);
                Label lbl_TankaGokei = (row.FindControl("lblTankaGokei") as Label);
                TextBox txt_nGenkaTanka = (row.FindControl("txtnGENKATANKA") as TextBox);
                Label lbl_GenkaGokei = (row.FindControl("lblGenkaGokei") as Label);
                Label lbl_Arari = (row.FindControl("lblnARARI") as Label);
                Label lbl_ArariSu = (row.FindControl("lblnARARISu") as Label);
                TextBox txt_nRITU = (row.FindControl("txtnRITU") as TextBox);
                Label lbl_kubun = (row.FindControl("lblKubun") as Label);
                Label lbl_nSIKIRITANKA = (row.FindControl("lblTanka") as Label);
                Label lbl_fJITAIS = (row.FindControl("lblfjitais") as Label);
                Label lbl_fJITAIQ = (row.FindControl("lblfjitaiq") as Label);

                if (!String.IsNullOrEmpty(txt_cSyohin.Text) || !String.IsNullOrEmpty(txt_sSyohin.Text) ||
                           !String.IsNullOrEmpty(txt_nSyoryo.Text) || !String.IsNullOrEmpty(txt_cTani.Text) ||
                           !String.IsNullOrEmpty(txt_nTanka.Text) || !String.IsNullOrEmpty(lbl_TankaGokei.Text) ||
                           !String.IsNullOrEmpty(txt_nGenkaTanka.Text) || !String.IsNullOrEmpty(lbl_GenkaGokei.Text) || txt_nRITU.Text != "100%" || lbl_kubun.Text== "計" ||lbl_kubun.Text== "見")
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = "0";
                    dr[1] = txt_cSyohin.Text;
                    dr[2] = txt_sSyohin.Text;
                    dr[3] = txt_nSyoryo.Text;
                    dr[4] = txt_cTani.Text;
                    dr[5] = txt_nTanka.Text;
                    dr[6] = lbl_TankaGokei.Text;
                    dr[7] = txt_nGenkaTanka.Text;
                    dr[8] = lbl_GenkaGokei.Text;
                    dr[9] = lbl_Arari.Text;
                    dr[10] = lbl_ArariSu.Text;
                    dr[11] = lbl_fgenkataka.Text;
                    dr[12] = lbl_rowNo.Text;
                    dr[13] = txt_nRITU.Text;
                    dr[14] = lbl_kubun.Text;
                    dr[15] = lbl_nSIKIRITANKA.Text;
                    dr[16] = lbl_fJITAIS.Text;
                    dr[17] = lbl_fJITAIQ.Text;
                    dt.Rows.Add(dr);

                    if (lbl_status.Text == "1")
                    {
                        maxRowNo += 1;
                        DataRow dr_Copy = dt_Copy.NewRow();
                        dr_Copy[0] = "0";
                        dr_Copy[1] = txt_cSyohin.Text;
                        dr_Copy[2] = txt_sSyohin.Text;
                        dr_Copy[3] = txt_nSyoryo.Text;
                        dr_Copy[4] = txt_cTani.Text;
                        dr_Copy[5] = txt_nTanka.Text;
                        dr_Copy[6] = lbl_TankaGokei.Text;
                        dr_Copy[7] = txt_nGenkaTanka.Text;
                        dr_Copy[8] = lbl_GenkaGokei.Text;
                        dr_Copy[9] = lbl_Arari.Text;
                        dr_Copy[10] = lbl_ArariSu.Text;
                        dr_Copy[11] = lbl_fgenkataka.Text;
                        dr_Copy[12] = maxRowNo.ToString();
                        dr_Copy[13] = txt_nRITU.Text;
                        dr_Copy[14] = lbl_kubun.Text;
                        dr_Copy[15] = lbl_nSIKIRITANKA.Text;
                        dr_Copy[16] = lbl_fJITAIS.Text;
                        dr_Copy[17] = lbl_fJITAIQ.Text;
                        dt_Copy.Rows.Add(dr_Copy);

                        if (btn_Syosai.Text != "詳")
                        {
                            DataRow[] rows = dt_Syosai.Select("rowNo = '" + lbl_rowNo.Text + "'");
                            if (rows.Length > 0)
                            {
                                foreach (var drow in rows)
                                {
                                    DataRow dr_Syosai = dt_Syosai.NewRow();
                                    dr_Syosai[0] = drow[0];
                                    dr_Syosai[1] = drow[1];
                                    dr_Syosai[2] = drow[2];
                                    dr_Syosai[3] = drow[3];
                                    dr_Syosai[4] = drow[4];
                                    dr_Syosai[5] = drow[5];
                                    dr_Syosai[6] = drow[6];
                                    dr_Syosai[7] = drow[7];
                                    dr_Syosai[8] = drow[8];
                                    dr_Syosai[9] = drow[9];
                                    dr_Syosai[10] = drow[10];
                                    dr_Syosai[11] = maxRowNo.ToString();
                                    dr_Syosai[12] = drow[12];
                                    dr_Syosai[13] = drow[13];
                                    dr_Syosai[14] = drow[14];
                                    dt_Syosai.Rows.Add(dr_Syosai);
                                }

                                dt_Syosai.DefaultView.Sort = "rowNo asc";
                                dt_Syosai.AcceptChanges();
                            }
                        }
                    }
                }
            }

            if (dt_Copy.Rows.Count > 0)
            {
                dt.Merge(dt_Copy);
                dt.AcceptChanges();
                while (dt.Rows.Count < Original_RowCount)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = "0";
                    dr[1] = "";
                    dr[2] = "";
                    dr[3] = "";
                    dr[4] = "";
                    dr[5] = "";
                    dr[6] = "";
                    dr[7] = "";
                    dr[8] = "";
                    dr[9] = "";
                    dr[10] = "";
                    dr[11] = "1";
                    dr[12] = "0";
                    dr[13] = "100%";
                    dr[14] = "";
                    dr[15] = "";
                    dr[16] = "0";
                    dr[17] = "0";
                    dt.Rows.Add(dr);
                }

                HF_maxRowNo.Value = maxRowNo.ToString();
                dt = SetMidashiSyokei(dt);
                ViewState["SyouhinTable"] = dt;
                GV_MitumoriSyohin_Original.DataSource = dt;
                GV_MitumoriSyohin_Original.DataBind();
                GV_Syosai.DataSource = dt_Syosai;
                GV_Syosai.DataBind();
                updMitsumoriSyohinGrid.Update();
                setSyosaiCount(dt, dt_Syosai);
                GetTotalKingaku();
                GV_MitumoriSyohin.DataSource = dt;
                GV_MitumoriSyohin.DataBind();
                //SetSyosai();
                HasCheckRow();
                updMitsumoriSyohinGrid.Update();
                HF_isChange.Value = "1";
                updHeader.Update();
            }

        }
        #endregion

        #region btn_delete_Click
        protected void btn_delete_Click(object sender, EventArgs e)
        {
            DataTable dt = GetGridViewData();
            List<string> list_syousai = new List<string>();
            List<string> list_row = new List<string>();
            if (ViewState["SyouhinTable"] != null)
            {
                for (int i = dt.Rows.Count - 1; i >= 0; i--)
                {
                    DataRow dr = dt.Rows[i];
                    if (dr[0].ToString() == "1")
                    {
                        list_syousai.Add((GV_MitumoriSyohin_Original.Rows[i].FindControl("btnSyohinShosai") as Button).Text);
                        list_row.Add((GV_MitumoriSyohin_Original.Rows[i].FindControl("lblRowNo") as Label).Text);
                        dr.Delete();
                    }
                }
                dt.AcceptChanges();
                try
                {
                    var max = dt.AsEnumerable().Max(x => int.Parse(x.Field<string>("rowNo")));
                    HF_maxRowNo.Value = max.ToString();
                }
                catch { }
                dt = SetMidashiSyokei(dt);
                ViewState["SyouhinTable"] = dt;
                GV_MitumoriSyohin_Original.DataSource = dt;
                GV_MitumoriSyohin_Original.DataBind();
                updMitsumoriSyohinGrid.Update();

                DataTable dt_Syosai = GetSyosaiGridViewData();
                for (int j = 0; j < list_syousai.Count; j++)
                {
                    if (list_syousai[j] != "詳")
                    {
                        DataRow[] rows = dt_Syosai.Select("rowNo = '" + list_row[j] + "'");
                        HF_fTankaChange.Value = HF_fTankaChange.Value.Replace(list_row[j], "");
                        HF_fGenkaChange.Value = HF_fGenkaChange.Value.Replace(list_row[j], "");
                        if (rows.Length > 0)
                        {
                            foreach (var drow in rows)
                            {
                                drow.Delete();
                            }
                            dt_Syosai.DefaultView.Sort = "rowNo asc";
                            dt_Syosai.AcceptChanges();

                            GV_Syosai.DataSource = dt_Syosai;
                            GV_Syosai.DataBind();
                        }
                    }
                    setSyosaiCount(dt, dt_Syosai);
                }
            }
            if (GV_MitumoriSyohin_Original.Rows.Count == 0)
            {
                btnFukusuCopy.CssClass = "JC10DisableButton";
                btnFukusuCopy.Enabled = false;
                btnMidashitsuika.CssClass = "JC10DisableButton";
                btnMidashitsuika.Enabled = false;
                btnShokeitsuika.CssClass = "JC10DisableButton";
                btnShokeitsuika.Enabled = false;
                btn_delete.CssClass = "JC10DisableButton";
                btn_delete.Enabled = false;
                BT_SyohinEmptyAdd.CssClass = "JC10GrayButton";
            }
            else
            {
                BT_SyohinEmptyAdd.CssClass = "DisplayNone";
            }
            DataTable dt_SyohinOriginal = new DataTable();
            dt_SyohinOriginal = dt;
            ViewState["SyouhinTable"] = dt_SyohinOriginal;
            GV_MitumoriSyohin.DataSource = dt_SyohinOriginal;
            GV_MitumoriSyohin.DataBind();
            HasCheckRow();
            updMitsumoriSyohinGrid.Update();

            HF_isChange.Value = "1";
            GetTotalKingaku();
            updHeader.Update();
        }
        #endregion

        #region グリッドに商品行削除
        protected void btnSyohinDelete_Click(object sender, EventArgs e)
        {
            LinkButton btnSyohinDelete = (LinkButton)sender;
            GridViewRow gvRow = (GridViewRow)btnSyohinDelete.NamingContainer;
            int rowID = gvRow.RowIndex;
            String Syosai = (gvRow.FindControl("btnSyohinShosai") as Button).Text;
            String RowNo = (gvRow.FindControl("lblRowNo") as Label).Text;

            DataTable dt = GetGridViewData();
            if (ViewState["SyouhinTable"] != null)
            {
                dt.Rows.RemoveAt(rowID);
                try
                {
                    var max = dt.AsEnumerable().Max(x => int.Parse(x.Field<string>("rowNo")));
                    HF_maxRowNo.Value = max.ToString();
                }
                catch { }
                dt = SetMidashiSyokei(dt);
                ViewState["SyouhinTable"] = dt;
                GV_MitumoriSyohin_Original.DataSource = dt;
                GV_MitumoriSyohin_Original.DataBind();
                updMitsumoriSyohinGrid.Update();

                DataTable dt_Syosai = GetSyosaiGridViewData();
                if (Syosai != "詳")
                {
                    DataRow[] rows = dt_Syosai.Select("rowNo = '" + RowNo + "'");
                    if (rows.Length > 0)
                    {
                        foreach (var drow in rows)
                        {
                            drow.Delete();
                        }

                        dt_Syosai.DefaultView.Sort = "rowNo asc";
                        dt_Syosai.AcceptChanges();

                        GV_Syosai.DataSource = dt_Syosai;
                        GV_Syosai.DataBind();
                    }
                }

                setSyosaiCount(dt, dt_Syosai);
            }
            if (GV_MitumoriSyohin_Original.Rows.Count == 0)
            {
                BT_SyohinEmptyAdd.CssClass = "JC10GrayButton";
            }
            else
            {
                BT_SyohinEmptyAdd.CssClass = "DisplayNone";
            }
            //DataTable dt_SyohinOriginal = GetGridViewData();
            DataTable dt_SyohinOriginal = new DataTable();
            dt_SyohinOriginal = dt;
            ViewState["SyouhinTable"] = dt_SyohinOriginal;
            GV_MitumoriSyohin.DataSource = dt_SyohinOriginal;
            GV_MitumoriSyohin.DataBind();
            HasCheckRow();
            //SetSyosai();
            updMitsumoriSyohinGrid.Update();
            HF_isChange.Value = "1";
            GetTotalKingaku();

            HF_fTankaChange.Value = HF_fTankaChange.Value.Replace(RowNo,"");
            HF_fGenkaChange.Value = HF_fGenkaChange.Value.Replace(RowNo, "");

            updHeader.Update();
        }
        #endregion

        #region 日付を1日増す、日付を1日減らす、日付を削除「見積日、受注日、売上予定日、見積確定日、受注予定日、完了予定日」

        #region btndMitumoriCross_Click
        protected void btndMitumoriCross_Click(object sender, EventArgs e)
        {
            lbldMitumori.Text = "";
            btnMitumoriDate.Style["display"] = "block";
            divMitumoriDate.Style["display"] = "none";
            updMitumoriDate.Update();
        }
        #endregion

        #region btndJuuchuCross_Click
        protected void btndJuuchuCross_Click(object sender, EventArgs e)
        {
            if (flagJoutai)
            {
                lbldJuuchu.Text = "";
                btnJuuchuDate.Style["display"] = "block";
                divJuuchuuDate.Style["display"] = "none";
                updJuuchuuDate.Update();
            }
            else
            {
                String current_Joutai = lblcJoutai.Text;
                if (current_Joutai == "02")  //受注
                {
                    String id = Request.QueryString["id"];
                    DataTable dt_db = JC01Login_Class.Get_DB(id);
                    String db = dt_db.Rows[0]["db"].ToString();
                    String mail = dt_db.Rows[0]["mail"].ToString();
                    ConstantVal.DB_NAME = db;

                    JC_ClientConnecction_Class jc = new JC_ClientConnecction_Class();
                    jc.loginId = mail;
                    Boolean isShijishoExist = jc.IsExistShijishou(lblcMitumori.Text);
                    Boolean isUriageExist = jc.IsExistUriage(lblcMitumori.Text);
                    if (isShijishoExist || isUriageExist)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowErrorMessage",
                                "ShowErrorMessage('受注日を削除できません。');", true);
                    }
                    else
                    {
                        if (!String.IsNullOrEmpty(lbldMitumoriKakutei.Text))
                        {
                            lblcJoutai.Text = "01";
                            lblsJoutai.Text = "見積提出済";
                            lblsJoutai.Attributes.Add("onClick", "BtnClick('MainContent_btnJoutai')");
                            divJoutaibtn.Style["display"] = "none";
                            divJoutaiLabel.Style["display"] = "block";
                            updJoutai.Update();
                        }
                        else
                        {
                            lblcJoutai.Text = "04";
                            lblsJoutai.Text = "見積作成中";
                            lblsJoutai.Attributes.Add("onClick", "BtnClick('MainContent_btnJoutai')");
                            divJoutaibtn.Style["display"] = "none";
                            divJoutaiLabel.Style["display"] = "block";
                            updJoutai.Update();
                        }
                        lbldJuuchu.Text = "";
                        btnJuuchuDate.Style["display"] = "block";
                        divJuuchuuDate.Style["display"] = "none";
                        updJuuchuuDate.Update();
                    }
                }
                else if (current_Joutai == "06" || current_Joutai == "00")
                {
                    String id = Request.QueryString["id"];
                    DataTable dt_db = JC01Login_Class.Get_DB(id);
                    String db = dt_db.Rows[0]["db"].ToString();
                    String mail = dt_db.Rows[0]["mail"].ToString();
                    ConstantVal.DB_NAME = db;

                    JC_ClientConnecction_Class jc = new JC_ClientConnecction_Class();
                    jc.loginId = mail;
                    Boolean isShijishoExist = jc.IsExistShijishou(lblcMitumori.Text);
                    Boolean isUriageExist = jc.IsExistUriage(lblcMitumori.Text);
                    if (isShijishoExist || isUriageExist)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowErrorMessage",
                                "ShowErrorMessage('受注日を削除できません。');", true);
                    }
                    else
                    {
                        lbldJuuchu.Text = "";
                        btnJuuchuDate.Style["display"] = "block";
                        divJuuchuuDate.Style["display"] = "none";
                        updJuuchuuDate.Update();
                    }
                }
            }

            if (String.IsNullOrEmpty(lbldJuuchu.Text))
            {
                btnCreateUriage.Enabled = false;
                btnCreateUriage.CssClass = "JC10SaveBtnDisable";
                btnUriage.Enabled = false;
                btnUriage.CssClass = "JC10SaveBtnDisable";
            }
            else
            {
                btnCreateUriage.Enabled = true;
                btnCreateUriage.CssClass = "BlueBackgroundButton JC10SaveBtn";
                btnUriage.Enabled = true;
                btnUriage.CssClass = "BlueBackgroundButton JC10SaveBtn";
            }
            updHeader.Update();
        }
        #endregion

        #region btndUriageYoteiCross_Click
        protected void btndUriageYoteiCross_Click(object sender, EventArgs e)
        {
            lbldUriageYotei.Text = "";
            btnUriageYoteiDate.Style["display"] = "block";
            divUriageYoteiDate.Style["display"] = "none";
            updUriageYoteiDate.Update();
            GetTotalKingaku();
            updHeader.Update();
        }
        #endregion

        #region btndJuuchuuYoteiCross_Click
        protected void btndJuuchuuYoteiCross_Click(object sender, EventArgs e)
        {
            lbldJuuchuuYotei.Text = "";
            btnJuuchuuYoteibi.Style["display"] = "block";
            divJuuchuuYoteiDate.Style["display"] = "none";
            updJuuchuuYoteibi.Update();
        }
        #endregion

        #region btndshuryoYoteiCross_Click
        protected void btndshuryoYoteiCross_Click(object sender, EventArgs e)
        {
            lbldshuryoYotei.Text = "";
            btnshuryoYoteibi.Style["display"] = "block";
            divshuryoYoteiDate.Style["display"] = "none";
            updshuryoYoteibi.Update();
        }
        #endregion

        #region btndMitumoriKakuteiCross_Click
        protected void btndMitumoriKakuteiCross_Click(object sender, EventArgs e)
        {
            lbldMitumoriKakutei.Text = "";
            btnMitumoriKakuteibi.Style["display"] = "block";
            divMitumoriKakuteiiDate.Style["display"] = "none";
            if (!flagJoutai)
            {
                String current_Joutai = lblcJoutai.Text;
                if (current_Joutai == "01")  //見積提出済
                {
                    lblcJoutai.Text = "04";
                    lblsJoutai.Text = "見積作成中";
                    lblsJoutai.Attributes.Add("onClick", "BtnClick('MainContent_btnJoutai')");
                    divJoutaibtn.Style["display"] = "none";
                    divJoutaiLabel.Style["display"] = "block";
                    updJoutai.Update();
                }
            }
            updMitumoriKakuteibi.Update();
        }
        #endregion

        #region btnLeftArrowdMitumori_Click
        protected void btnLeftArrowdMitumori_Click(object sender, EventArgs e)
        {
            DateTime sdate = Convert.ToDateTime(lbldMitumori.Text);
            sdate = sdate.AddDays(-1);
            lbldMitumori.Text = sdate.ToString("yyyy/MM/dd");
            txtsMitumori.Text = "text";
        }
        #endregion

        #region btnRightArrowdMitumori_Click
        protected void btnRightArrowdMitumori_Click(object sender, EventArgs e)
        {
            DateTime sdate = Convert.ToDateTime(lbldMitumori.Text);
            sdate = sdate.AddDays(1);
            lbldMitumori.Text = sdate.ToString("yyyy/MM/dd");
        }
        #endregion

        #region btnLeftArrowdJuuchu_Click
        protected void btnLeftArrowdJuuchu_Click(object sender, EventArgs e)
        {
            DateTime sdate = Convert.ToDateTime(lbldJuuchu.Text);
            sdate = sdate.AddDays(-1);
            lbldJuuchu.Text = sdate.ToString("yyyy/MM/dd");
        }
        #endregion

        #region btnRightArrowdJuuchu_Click
        protected void btnRightArrowdJuuchu_Click(object sender, EventArgs e)
        {
            DateTime sdate = Convert.ToDateTime(lbldJuuchu.Text);
            sdate = sdate.AddDays(1);
            lbldJuuchu.Text = sdate.ToString("yyyy/MM/dd");
        }
        #endregion

        #region btnLeftArrowdUriageYotei_Click
        protected void btnLeftArrowdUriageYotei_Click(object sender, EventArgs e)
        {
            DateTime sdate = Convert.ToDateTime(lbldUriageYotei.Text);
            sdate = sdate.AddDays(-1);
            lbldUriageYotei.Text = sdate.ToString("yyyy/MM/dd");
        }
        #endregion

        #region btnRightArrowdUriageYotei_Click
        protected void btnRightArrowdUriageYotei_Click(object sender, EventArgs e)
        {
            DateTime sdate = Convert.ToDateTime(lbldUriageYotei.Text);
            sdate = sdate.AddDays(1);
            lbldUriageYotei.Text = sdate.ToString("yyyy/MM/dd");
        }
        #endregion

        #endregion

        #region BindImage()
        public void BindImage()
        {
            for (int i = 0; i < Request.Files.Count; i++)
            {
                if (i.ToString() == fImageUpload)
                {
                    Boolean issave = true;
                    HttpPostedFile file = Request.Files[i];
                    string filename = Path.GetFileName(file.FileName);
                    string ext = Path.GetExtension(file.FileName);
                    if (ext.ToLower().Contains("gif") || ext.ToLower().Contains("jpg") || ext.ToLower().Contains("jpeg") || ext.ToLower().Contains("png") || ext.ToLower().Contains("jfif"))
                    {
                        if (filename.Length <= 100)
                        {
                            System.IO.Stream fs = file.InputStream;
                            System.Drawing.Image originalImage = System.Drawing.Image.FromStream(fs);
                            int introtate = 0;
                            if (originalImage.PropertyIdList.Contains(0x0112))
                            {
                                introtate = originalImage.GetPropertyItem(0x0112).Value[0];
                                switch (introtate)
                                {
                                    case 1: // landscape, do nothing
                                        break;

                                    case 8: // rotated 90 right
                                            // de-rotate:
                                        originalImage.RotateFlip(rotateFlipType: RotateFlipType.Rotate270FlipNone);
                                        break;

                                    case 3: // bottoms up
                                        originalImage.RotateFlip(rotateFlipType: RotateFlipType.Rotate180FlipNone);
                                        break;

                                    case 6: // rotated 90 left
                                        originalImage.RotateFlip(rotateFlipType: RotateFlipType.Rotate90FlipNone);
                                        break;
                                }
                            }
                            try
                            {
                                var i2 = new Bitmap(originalImage);
                                EncoderParameters myEncoderParameters = new EncoderParameters(1);
                                EncoderParameter myEncoderParameter = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 75L);
                                myEncoderParameters.Param[0] = myEncoderParameter;
                                //i2.Save(folderPath + filename, GetEncoderInfo("image/jpeg"), myEncoderParameters);
                                //var i2 = new Bitmap(originalImage);
                                //i2.Save(folderPath + filename);
                                //originalImage.Save(folderPath + filename);
                            }
                            catch { }
                            using (var stream = new MemoryStream())
                            {
                                originalImage.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                                byte[] imageByte = stream.ToArray();
                                if (file.ContentLength > 23552)//about 23KB
                                {
                                    imageByte = ResizeImageFile(imageByte, 760);
                                }
                                string base64String = Convert.ToBase64String(imageByte);
                                string imgurl = "data:image/png;base64," + base64String;
                                if (fImageUpload == "3")
                                {
                                    imgDaihyou.Src = imgurl;
                                    txtFilePath.Text = Server.MapPath(fileUpload1.FileName).ToString();
                                }
                                else if (fImageUpload == "0")
                                {
                                    HyoshiuploadedImage.Src = imgurl;
                                    HyoshiuploadedImage.Attributes.Add("class", "JC10DaiHyouImage");
                                    BT_HyoshiImgaeDelete.CssClass = "JC10ImageDelete";
                                    HyoshidragZone.Attributes.Add("class", "DisplayNone");
                                }
                                else if (fImageUpload == "1")
                                {
                                    Mitumorisho1uploadedImage.Src = imgurl;
                                    Mitumorisho1uploadedImage.Attributes.Add("class", "JC10DaiHyouImage");
                                    BT_Mitumorisho1ImgaeDelete.CssClass = "JC10ImageDelete";
                                    Mitumorisho1dragZone.Attributes.Add("class", "DisplayNone");
                                }
                                else if (fImageUpload == "2")
                                {
                                    Mitumorisho2uploadedImage.Src = imgurl;
                                    Mitumorisho2uploadedImage.Attributes.Add("class", "JC10DaiHyouImage");
                                    BT_Mitumorisho2ImgaeDelete.CssClass = "JC10ImageDelete";
                                    Mitumorisho2dragZone.Attributes.Add("class", "DisplayNone");
                                }
                            }
                        }
                        else
                        {
                            //Response.Write("<script language='javascript'>window.alert('ファイルの名が正しくありません。確認してください。');</script>");
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowErrorMessage",
                    "ShowErrorMessage('ファイルの名が正しくありません。<br/>確認してください。');", true);
                        }
                    }
                    else if (ext.ToLower().Contains("pdf") || ext.ToLower().Contains("ai"))
                    {
                        if (filename.Length <= 100)
                        {
                            System.IO.Stream fs = file.InputStream;
                            GhostscriptRasterizer rasterizer = null;

                            using (rasterizer = new GhostscriptRasterizer())
                            {
                                rasterizer.Open(fs);

                                using (MemoryStream ms = new MemoryStream())
                                {
                                    int pagecount = rasterizer.PageCount;

                                    if (pagecount == 1)
                                    {
                                        System.Drawing.Image img = rasterizer.GetPage(200, 1);
                                        img.Save(ms, ImageFormat.Png);

                                        var SigBase64 = Convert.ToBase64String(ms.GetBuffer()); //Get Base64
                                        String imgurl = "data:image/png;base64," + SigBase64;
                                        if (fImageUpload == "3")
                                        {
                                            imgDaihyou.Src = imgurl;
                                            txtFilePath.Text = Server.MapPath(fileUpload1.FileName).ToString();
                                        }
                                        else if (fImageUpload == "0")
                                        {
                                            HyoshiuploadedImage.Src = imgurl;
                                            HyoshiuploadedImage.Attributes.Add("class", "JC10DaiHyouImage");
                                            BT_HyoshiImgaeDelete.CssClass = "JC10ImageDelete";
                                            HyoshidragZone.Attributes.Add("class", "DisplayNone");
                                        }
                                        else if (fImageUpload == "1")
                                        {
                                            Mitumorisho1uploadedImage.Src = imgurl;
                                            Mitumorisho1uploadedImage.Attributes.Add("class", "JC10DaiHyouImage");
                                            BT_Mitumorisho1ImgaeDelete.CssClass = "JC10ImageDelete";
                                            Mitumorisho1dragZone.Attributes.Add("class", "DisplayNone");
                                        }
                                        else if (fImageUpload == "2")
                                        {
                                            Mitumorisho2uploadedImage.Src = imgurl;
                                            Mitumorisho2uploadedImage.Attributes.Add("class", "JC10DaiHyouImage");
                                            BT_Mitumorisho2ImgaeDelete.CssClass = "JC10ImageDelete";
                                            Mitumorisho2dragZone.Attributes.Add("class", "DisplayNone");
                                        }
                                    }
                                    else
                                    {
                                        if (fImageUpload == "0")
                                        {
                                            byte[] fileBytes = fileUpload2.FileBytes;
                                            HF_ImgBase64.Value = Convert.ToBase64String(fileBytes);
                                        }
                                        else if (fImageUpload == "1")
                                        {
                                            byte[] fileBytes = fileUpload3.FileBytes;
                                            HF_ImgBase64.Value = Convert.ToBase64String(fileBytes);
                                        }
                                        else if (fImageUpload == "2")
                                        {
                                            byte[] fileBytes = fileUpload4.FileBytes;
                                            HF_ImgBase64.Value = Convert.ToBase64String(fileBytes);
                                        }
                                        updHeader.Update();
                                        SessionUtility.SetSession("HOME", "Master");
                                        HF_fImageUpload.Value = fImageUpload;
                                        Session["pageNumber"] = pagecount;
                                        //ifSentakuPopup.Style["width"] = "350px";
                                        //ifSentakuPopup.Style["height"] = "100px";
                                        ifSentakuPopup.Style["width"] = "100vw";
                                        ifSentakuPopup.Style["height"] = "100vh";

                                        String id = Request.QueryString["id"];
                                        ifSentakuPopup.Src = "JC35PdfPageChoice.aspx?id=" + id;
                                        mpeSentakuPopup.Show();
                                        updSentakuPopup.Update();
                                    }
                                }
                                rasterizer.Close();
                            }
                        }
                        else
                        {
                            //Response.Write("<script language='javascript'>window.alert('ファイルの名が正しくありません。確認してください。');</script>");
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowErrorMessage",
                    "ShowErrorMessage('ファイルの名が正しくありません。<br/>確認してください。');", true);
                        }
                    }
                    else
                    {
                        //Response.Write("<script language='javascript'>window.alert('ファイルの種類が正しくありません。確認してください。');</script>");
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowErrorMessage",
                    "ShowErrorMessage('ファイルの種類が正しくありません。<br/>確認してください。');", true);
                    }
                }
            }
            updHeader.Update();
        }

        #endregion

        #region ResizeImageFile()
        public static byte[] ResizeImageFile(byte[] imageFile, int targetSize) // Set targetSize to 760
        {
            using (System.Drawing.Image oldImage = System.Drawing.Image.FromStream(new MemoryStream(imageFile)))
            {
                Size newSize = CalculateDimensions(oldImage.Size, targetSize);
                using (Bitmap newImage = new Bitmap(newSize.Width, newSize.Height, PixelFormat.Format24bppRgb))
                {
                    using (Graphics canvas = Graphics.FromImage(newImage))
                    {
                        canvas.SmoothingMode = SmoothingMode.AntiAlias;
                        canvas.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        canvas.PixelOffsetMode = PixelOffsetMode.HighQuality;
                        canvas.DrawImage(oldImage, new Rectangle(new Point(0, 0), newSize));
                        MemoryStream m = new MemoryStream();
                        newImage.Save(m, ImageFormat.Jpeg);
                        return m.GetBuffer();
                    }
                }
            }
        }
        #endregion

        #region CalculateDimensions()
        public static Size CalculateDimensions(Size oldSize, int targetSize)
        {
            Size newSize = new Size();
            if (oldSize.Height > oldSize.Width)
            {
                newSize.Width = (int)(oldSize.Width * ((float)targetSize / (float)oldSize.Height));
                newSize.Height = targetSize;
            }
            else
            {
                newSize.Width = targetSize;
                newSize.Height = (int)(oldSize.Height * ((float)targetSize / (float)oldSize.Width));
            }
            return newSize;
        }
        #endregion

        #region 代表画像をアップロード
        protected void lkImageUpload_Click(object sender, EventArgs e)
        {
            if (txtFilePath.Text != "")
            {
                imgDaihyou.Attributes.Add("class", "JC10DaiHyouImage");
                lkImageUpload.CssClass = "DisplayNone";
            }
        }
        #endregion

        #region 受注日を選択
        protected void btnJuuchuDate_Click(object sender, EventArgs e)
        {
            SessionUtility.SetSession("HOME", "Master");
            String id = Request.QueryString["id"];
            ifdatePopup.Src = "JCHidukeSelect.aspx?id=" + id;
            mpedatePopup.Show();

            ViewState["DATETIME"] = btnJuuchuDate.ID;

            if (!String.IsNullOrEmpty(lbldJuuchu.Text))
            {
                DateTime dt = DateTime.Parse(lbldJuuchu.Text);
                Session["CALENDARDATE"] = dt.ToString("yyyy/MM/dd");
                Session["YEAR"] = dt.Year;
            }

            // 設定したラベルをクリック時、時間設定ポップアップ画面を表示する
            lbldJuuchu.Attributes.Add("onClick", "BtnClick('MainContent_btnJuuchuDate')");
            upddatePopup.Update();
        }
        #endregion

        #region 受注予定日を選択
        protected void btnJuuchuuYoteibi_Click(object sender, EventArgs e)
        {
            SessionUtility.SetSession("HOME", "Master");
            String id = Request.QueryString["id"];
            ifdatePopup.Src = "JCHidukeSelect.aspx?id=" + id;
            mpedatePopup.Show();

            ViewState["DATETIME"] = btnJuuchuuYoteibi.ID;

            if (!String.IsNullOrEmpty(lbldJuuchuuYotei.Text))
            {
                DateTime dt = DateTime.Parse(lbldJuuchuuYotei.Text);
                Session["CALENDARDATE"] = dt.ToString("yyyy/MM/dd");
                Session["YEAR"] = dt.Year;
            }

            // 設定したラベルをクリック時、時間設定ポップアップ画面を表示する
            lbldJuuchuuYotei.Attributes.Add("onClick", "BtnClick('MainContent_btnJuuchuuYoteibi')");
            upddatePopup.Update();
        }
        #endregion

        #region 見積日を選択
        protected void btnMitumoriDate_Click(object sender, EventArgs e)
        {
            SessionUtility.SetSession("HOME", "Master");

            String id = Request.QueryString["id"];
            ifdatePopup.Src = "JCHidukeSelect.aspx?id=" + id;
            mpedatePopup.Show();

            ViewState["DATETIME"] = btnMitumoriDate.ID;

            if (!String.IsNullOrEmpty(lbldMitumori.Text))
            {
                DateTime dt = DateTime.Parse(lbldMitumori.Text);
                Session["CALENDARDATE"] = dt.ToString("yyyy/MM/dd");
                Session["YEAR"] = dt.Year;
            }

            // 設定したラベルをクリック時、時間設定ポップアップ画面を表示する
            lbldMitumori.Attributes.Add("onClick", "BtnClick('MainContent_btnMitumoriDate')");
            upddatePopup.Update();
        }
        #endregion

        #region 売上予定日を選択
        protected void btnUriageYoteiDate_Click(object sender, EventArgs e)
        {
            SessionUtility.SetSession("HOME", "Master");
            String id = Request.QueryString["id"];
            ifdatePopup.Src = "JCHidukeSelect.aspx?id=" + id;
            mpedatePopup.Show();

            ViewState["DATETIME"] = btnUriageYoteiDate.ID;

            if (!String.IsNullOrEmpty(lbldUriageYotei.Text))
            {
                DateTime dt = DateTime.Parse(lbldUriageYotei.Text);
                Session["CALENDARDATE"] = dt.ToString("yyyy/MM/dd");
                Session["YEAR"] = dt.Year;
            }

            // 設定したラベルをクリック時、時間設定ポップアップ画面を表示する
            lbldUriageYotei.Attributes.Add("onClick", "BtnClick('MainContent_btnUriageYoteiDate')");
            upddatePopup.Update();
        }
        #endregion

        #region 完了予定日を選択
        protected void btnshuryoYoteibi_Click(object sender, EventArgs e)
        {
            SessionUtility.SetSession("HOME", "Master");

            String id = Request.QueryString["id"];
            ifdatePopup.Src = "JCHidukeSelect.aspx?id=" + id;
            mpedatePopup.Show();

            ViewState["DATETIME"] = btnshuryoYoteibi.ID;

            if (!String.IsNullOrEmpty(lbldshuryoYotei.Text))
            {
                DateTime dt = DateTime.Parse(lbldshuryoYotei.Text);
                Session["CALENDARDATE"] = dt.ToString("yyyy/MM/dd");
                Session["YEAR"] = dt.Year;
            }

            // 設定したラベルをクリック時、時間設定ポップアップ画面を表示する
            lbldshuryoYotei.Attributes.Add("onClick", "BtnClick('MainContent_btnshuryoYoteibi')");
            upddatePopup.Update();
        }
        #endregion

        #region 見積確定を選択
        protected void btnMitumoriKakuteibi_Click(object sender, EventArgs e)
        {
            SessionUtility.SetSession("HOME", "Master");
            String id = Request.QueryString["id"];
            ifdatePopup.Src = "JCHidukeSelect.aspx?id=" + id;
            mpedatePopup.Show();

            ViewState["DATETIME"] = btnMitumoriKakuteibi.ID;

            if (!String.IsNullOrEmpty(lbldMitumoriKakutei.Text))
            {
                DateTime dt = DateTime.Parse(lbldMitumoriKakutei.Text);
                Session["CALENDARDATE"] = dt.ToString("yyyy/MM/dd");
                Session["YEAR"] = dt.Year;
            }

            // 設定したラベルをクリック時、時間設定ポップアップ画面を表示する
            lbldMitumoriKakutei.Attributes.Add("onClick", "BtnClick('MainContent_btnMitumoriKakuteibi')");
            upddatePopup.Update();
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
                if (strBtnID == btnJuuchuDate.ID)
                {
                    JuuchuuDateDataBind(strCalendarDateTime, strBtnID);
                    String currentJoutai = lblcJoutai.Text;
                    if (currentJoutai == "04" || String.IsNullOrEmpty(currentJoutai))
                    {
                        lblcJoutai.Text = "02";
                        lblsJoutai.Text = "受注";
                        lblsJoutai.Attributes.Add("onClick", "BtnClick('MainContent_btnJoutai')");
                        divJoutaibtn.Style["display"] = "none";
                        divJoutaiLabel.Style["display"] = "block";
                        updJoutai.Update();

                        if (String.IsNullOrEmpty(lbldMitumoriKakutei.Text))
                        {
                            MitumoriKakuteiDateDataBind(strCalendarDateTime, btnMitumoriKakuteibi.ID);
                        }
                    }
                    else if (currentJoutai == "01")
                    {
                        lblcJoutai.Text = "02";
                        lblsJoutai.Text = "受注";
                        lblsJoutai.Attributes.Add("onClick", "BtnClick('MainContent_btnJoutai')");
                        divJoutaibtn.Style["display"] = "none";
                        divJoutaiLabel.Style["display"] = "block";
                        updJoutai.Update();
                    }
                }
                else if (strBtnID == btnMitumoriDate.ID)
                {
                    MitumoriDateDataBind(strCalendarDateTime, strBtnID);
                }
                else if (strBtnID == btnUriageYoteiDate.ID)
                {
                    UriageYoteiDateDataBind(strCalendarDateTime, strBtnID);
                }
                else if (strBtnID == btnJuuchuuYoteibi.ID)
                {
                    JuuchuuYoteiDateDataBind(strCalendarDateTime, strBtnID);
                }
                else if (strBtnID == btnshuryoYoteibi.ID)
                {
                    ShuryoYoteiDateDataBind(strCalendarDateTime, strBtnID);
                }
                else if (strBtnID == btnMitumoriKakuteibi.ID)
                {
                    MitumoriKakuteiDateDataBind(strCalendarDateTime, strBtnID);
                    String currentJoutai = lblcJoutai.Text;
                    if (currentJoutai == "04" || String.IsNullOrEmpty(currentJoutai))
                    {
                        lblcJoutai.Text = "01";
                        lblsJoutai.Text = "見積提出済";
                        lblsJoutai.Attributes.Add("onClick", "BtnClick('MainContent_btnJoutai')");
                        divJoutaibtn.Style["display"] = "none";
                        divJoutaiLabel.Style["display"] = "block";
                        updJoutai.Update();
                    }
                }
                //lblHdnAnkenTextChange.Text = "true";
            }
            HF_isChange.Value = "1";
            CalendarFoucs();
            updHeader.Update();
        }

        #endregion

        #region "受注日データバインディング処理"
        protected void JuuchuuDateDataBind(string strCalendarDateTime, string strImgbtnID)
        {
            DateTime dtDeliveryDate = DateTime.Parse(strCalendarDateTime);
            lbldJuuchu.Text = dtDeliveryDate.ToString("yyyy/MM/dd");
            btnJuuchuDate.Style["display"] = "none";
            divJuuchuuDate.Style["display"] = "block";
            updJuuchuuDate.Update();
            if (String.IsNullOrEmpty(lbldJuuchu.Text))
            {
                btnCreateUriage.Enabled = false;
                btnCreateUriage.CssClass = "JC10SaveBtnDisable";
                btnUriage.Enabled = false;
                btnUriage.CssClass = "JC10SaveBtnDisable";
            }
            else
            {
                btnCreateUriage.Enabled = true;
                btnCreateUriage.CssClass = "BlueBackgroundButton JC10SaveBtn";
                btnUriage.Enabled = true;
                btnUriage.CssClass = "BlueBackgroundButton JC10SaveBtn";
            }
            updHeader.Update();
        }
        #endregion

        #region "受注予定日データバインディング処理"
        protected void JuuchuuYoteiDateDataBind(string strCalendarDateTime, string strImgbtnID)
        {
            DateTime dtDeliveryDate = DateTime.Parse(strCalendarDateTime);
            lbldJuuchuuYotei.Text = dtDeliveryDate.ToString("yyyy/MM/dd");
            btnJuuchuuYoteibi.Style["display"] = "none";
            divJuuchuuYoteiDate.Style["display"] = "block";
            updJuuchuuYoteibi.Update();
        }
        #endregion

        #region "見積日データバインディング処理"
        protected void MitumoriDateDataBind(string strCalendarDateTime, string strImgbtnID)
        {
            DateTime dtDeliveryDate = DateTime.Parse(strCalendarDateTime);
            lbldMitumori.Text = dtDeliveryDate.ToString("yyyy/MM/dd");
            btnMitumoriDate.Style["display"] = "none";
            divMitumoriDate.Style["display"] = "block";
            updMitumoriDate.Update();
            GetTotalKingaku();
            updHeader.Update();
        }
        #endregion

        #region "売上予定日データバインディング処理"
        protected void UriageYoteiDateDataBind(string strCalendarDateTime, string strImgbtnID)
        {
            DateTime dtDeliveryDate = DateTime.Parse(strCalendarDateTime);
            lbldUriageYotei.Text = dtDeliveryDate.ToString("yyyy/MM/dd");
            btnUriageYoteiDate.Style["display"] = "none";
            divUriageYoteiDate.Style["display"] = "block";
            updUriageYoteiDate.Update();
            GetTotalKingaku();
            updHeader.Update();
        }
        #endregion

        #region "完了予定日データバインディング処理"
        protected void ShuryoYoteiDateDataBind(string strCalendarDateTime, string strImgbtnID)
        {
            DateTime dtDeliveryDate = DateTime.Parse(strCalendarDateTime);
            lbldshuryoYotei.Text = dtDeliveryDate.ToString("yyyy/MM/dd");
            btnshuryoYoteibi.Style["display"] = "none";
            divshuryoYoteiDate.Style["display"] = "block";
            updshuryoYoteibi.Update();
        }
        #endregion

        #region "見積確定日データバインディング処理"
        protected void MitumoriKakuteiDateDataBind(string strCalendarDateTime, string strImgbtnID)
        {
            DateTime dtDeliveryDate = DateTime.Parse(strCalendarDateTime);
            lbldMitumoriKakutei.Text = dtDeliveryDate.ToString("yyyy/MM/dd");
            btnMitumoriKakuteibi.Style["display"] = "none";
            divMitumoriKakuteiiDate.Style["display"] = "block";
            updMitumoriKakuteibi.Update();
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
            if (strBtnID == btnJuuchuDate.ID)
            {
                if (btnJuuchuDate.Style["display"] != "none")
                {
                    btnJuuchuDate.Focus();
                }
                else
                {
                    btnRightArrowdJuuchu.Focus();
                }
            }
            else if (strBtnID == btnMitumoriDate.ID)
            {
                if (btnMitumoriDate.Style["display"] != "none")
                {
                    btnMitumoriDate.Focus();
                }
                else
                {
                    btnRightArrowdMitumori.Focus();
                }
            }
            else if (strBtnID == btnUriageYoteiDate.ID)
            {
                if (btnUriageYoteiDate.Style["display"] != "none")
                {
                    btnUriageYoteiDate.Focus();
                }
                else
                {
                    btnRightArrowdUriageYotei.Focus();
                }
            }
        }
        #endregion

        #region lkMitumori_Click
        protected void lkMitumori_Click(object sender, EventArgs e)
        {
            lkMitumori.Style.Add(" background-color", "rgba(191,191,191)");
            lkPrint.Style.Add(" background-color", "rgba(242,242,242)");
            lkUriage.Style.Add(" background-color", "rgba(242,242,242)");
            divMitumoriTorokuP.Attributes.Add("class", "JC10MitumoriTourokuDiv");
            divMitumoriInsatsu.Attributes.Add("class", "DisplayNone");
            divMitumoriUriage.Attributes.Add("class", "DisplayNone");
            SyohinKoumokuSort();
            updHeader.Update();
        }
        #endregion

        #region lkPrint_Click
        protected void lkPrint_Click(object sender, EventArgs e)
        {
            try
            {
                lkPrint.Style.Add(" background-color", "rgba(191,191,191)");
                lkMitumori.Style.Add(" background-color", "rgba(242,242,242)");
                lkUriage.Style.Add(" background-color", "rgba(242,242,242)");
                divMitumoriInsatsu.Attributes.Add("class", "JC10MitumoriTourokuDiv");
                divMitumoriTorokuP.Attributes.Add("class", "DisplayNone");
                divMitumoriUriage.Attributes.Add("class", "DisplayNone");
                setrogo();
                SetGokeiKingakuHyoji();

                String id = Request.QueryString["id"];
                DataTable dt_db = JC01Login_Class.Get_DB(id);
                String db = dt_db.Rows[0]["db"].ToString();
                String mail = dt_db.Rows[0]["mail"].ToString();
                ConstantVal.DB_NAME = db;

                JC_ClientConnecction_Class jc = new JC_ClientConnecction_Class();
                jc.loginId = mail;
                DataTable dt_InsatsuSetting = jc.ExecuteInsatsuSetting(lblLoginUserCode.Text);
                if (dt_InsatsuSetting.Rows.Count > 0)
                {
                    try
                    {
                        String logo = dt_InsatsuSetting.Rows[0]["fLogo"].ToString();
                        DDL_Logo.SelectedIndex = Convert.ToInt32(logo);
                    }
                    catch { }

                    String fZeikomi = dt_InsatsuSetting.Rows[0]["fZei"].ToString();
                    if (fZeikomi == "0")
                    {
                        btnZeikomi_Click(sender, e);
                    }
                    else if (fZeikomi == "1")
                    {
                        btnZeinuKingaku1_Click(sender, e);
                    }
                    else if (fZeikomi == "2")
                    {
                        btnZeinuKingaku2_Click(sender, e);
                    }

                    String fMidashi= dt_InsatsuSetting.Rows[0]["fMidashi"].ToString();
                    if (fMidashi == "1")
                    {
                        CHK_Midashi.Checked = true;
                    }
                    else
                    {
                        CHK_Midashi.Checked = false;
                    }

                    String fMeisai = dt_InsatsuSetting.Rows[0]["fMeisai"].ToString();
                    if (fMeisai == "1")
                    {
                        CHK_Meisai.Checked = true;
                    }
                    else
                    {
                        CHK_Meisai.Checked = false;
                    }

                    String fSyosai= dt_InsatsuSetting.Rows[0]["fSyosai"].ToString();
                    if (fSyosai == "1")
                    {
                        CHK_Shosai.Checked = true;
                    }
                    else
                    {
                        CHK_Shosai.Checked = false;
                    }
                }
                updHeader.Update();
            }
            catch(Exception ex)
            {
                Response.Redirect("JC01Login.aspx");
            }
        }
        #endregion

        #region lkUriage_Click
        protected void lkUriage_Click(object sender, EventArgs e)
        {
            lkPrint.Style.Add(" background-color", "rgba(242,242,242)");
            lkMitumori.Style.Add(" background-color", "rgba(242,242,242)");
            lkUriage.Style.Add(" background-color", "rgba(191,191,191)");
            divMitumoriInsatsu.Attributes.Add("class", "DisplayNone");
            divMitumoriTorokuP.Attributes.Add("class", "DisplayNone");
            divMitumoriUriage.Attributes.Add("class", "JC10MitumoriTourokuDiv");
            DataTable dt1 = CreateUriageTableColomn();
            get_uriage_data();
            //for (int i = 1; i <= 10; i++)
            //{
            //    DataRow dr = dt1.NewRow();
            //    dr[0] = "";
            //    dr[1] = "0000010";
            //    dr[2] = "0000010";
            //    dr[3] = "aaa";
            //    dr[4] = "bbb";
            //    dr[5] = "ccc";
            //    dr[6] = "ddd";
            //    dr[7] = "2021/10/26";
            //    dr[8] = "";
            //    dr[9] = "";
            //    dr[10] = "";
            //    dt1.Rows.Add(dr);
            //}
            //GV_Uriage.DataSource = dt1;
            //GV_Uriage.DataBind();

            updHeader.Update();
        }
        #endregion

        #region CreateUriageTableColomn
        private DataTable CreateUriageTableColomn()
        {
            DataTable dt_Uriage = new DataTable();
            dt_Uriage.Columns.Add("status");
            dt_Uriage.Columns.Add("cURIAGE");
            dt_Uriage.Columns.Add("cMITUMORI");
            dt_Uriage.Columns.Add("sSEIKYUSAKI");
            dt_Uriage.Columns.Add("sTOKUISAKI");
            dt_Uriage.Columns.Add("snouhin");
            dt_Uriage.Columns.Add("sTANTOUSHA");
            dt_Uriage.Columns.Add("売上日");
            dt_Uriage.Columns.Add("売上会額");
            dt_Uriage.Columns.Add("売上状態");
            dt_Uriage.Columns.Add("sMemo");
            return dt_Uriage;
        }
        #endregion

        #region get_uriage_data
        public void get_uriage_data()
        {
            String id = Request.QueryString["id"];
            DataTable dt_db = JC01Login_Class.Get_DB(id);
            String db = dt_db.Rows[0]["db"].ToString();
            String mail = dt_db.Rows[0]["mail"].ToString();
            ConstantVal.DB_NAME = db;

            JC_ClientConnecction_Class jc = new JC_ClientConnecction_Class();
            jc.loginId = mail;
            con = jc.GetConnection();
            con.Open();
            string select_uriage = "SELECT ru.cURIAGE ,rum.cMITUMORI," +
                                    "ru.sSEIKYUSAKI,ru.sTOKUISAKI," +
                                    "ru.snouhin,mj.sTANTOUSHA," +
                                    "DATE_FORMAT(ru.dURIAGE,'%y/%m/%d') as 売上日,FORMAT(ru.nuriage_kingaku,'###,###,###') as 売上会額," +
                                    "CASE IFNULL(ru.cJYOTAI_Uriage,'') " +
                                    "when '00' then '作成中' when '01' then '作成済' " +
                                    "when '02' then '請求締処理' when '03' then '入金' " +
                                    "else '' end as  売上状態,ru.sMemo " +
                                    "FROM r_uriage ru " +
                                    "Inner JOIN r_uri_mitsu rum on rum.cURIAGE=ru.cURIAGE " +
                                    "LEFT JOIN (SELECT * " +
                                                "FROM (SELECT cMITUMORI,sMITUMORI,cEIGYOTANTOSYA " +
                                                "FROM r_mitumori " +
                                                "order by cMITUMORI_KO desc) as t group by cMITUMORI) rm " +
                                    "on rum.cMITUMORI=rm.cMITUMORI " +
                                    "LEFT JOIN m_j_tantousha mj on ru.cEIGYOTANTOSYA=mj.cTANTOUSHA " +
                                    "WHERE rum.cMITUMORI='" + lblcMitumori.Text + "';";
            MySqlCommand cmd = new MySqlCommand(select_uriage, con);
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable dtable = new DataTable();
            da.Fill(dtable);
            con.Close();
            da.Dispose();
            ViewState["dt_Uriage"] = dtable;
            GV_Uriage.DataSource = dtable;
            GV_Uriage.DataBind();

            UriageKoumokuSort();
        }
        #endregion

        #region getMitumoriData()
        protected void getMitumoriData()
        {
            String id = Request.QueryString["id"];
            DataTable dt_db = JC01Login_Class.Get_DB(id);
            String db = dt_db.Rows[0]["db"].ToString();
            String mail = dt_db.Rows[0]["mail"].ToString();
            ConstantVal.DB_NAME = db;

            JC_ClientConnecction_Class jc = new JC_ClientConnecction_Class();
            jc.loginId = mail;
            con = jc.GetConnection();
            con.Open();
            #region Selectquery
            string sql = "Select distinct" +
                " IfNull(rm.cMITUMORI, '') As cMITUMORI," +
                " IfNull(rm.cMITUMORI_KO, '00') As cMITUMORI_KO," +
                " IfNull(rm.nHENKOU, '0') As nHENKOU," +
                " IfNull(rm.sMITUMORI, '') As sMITUMORI," +
                " IfNull(R_BU_MITSU.cBUKKEN, '') As cBUKKEN, " +
                " IfNull(rm.cTOKUISAKI, '') As cTOKUISAKI," +
                " IfNull(rm.sTOKUISAKI, '') As sTOKUISAKI," +
                " IfNull(rm.cSEIKYUSAKI, '') As cSEIKYUSAKI," +
                " IfNull(rm.sSEIKYUSAKI, '') As sSEIKYUSAKI," +
                " IfNull(rm.sTOKUISAKI_TAN, '') As sTOKUISAKI_TAN," +
                " IfNull(rm.sTOKUISAKI_TAN_Jun, '') As sTOKUISAKI_TAN_Jun," +
                " IfNull(rm.sSEIKYU_TAN, '') As sSEIKYU_TAN," +
                " IfNull(rm.sSEIKYU_TAN_Jun, '') As sSEIKYU_TAN_Jun," +
                " date_format(rm.dMITUMORISAKUSEI, '%Y/%m/%d') As dMITUMORISAKUSEI," +
                " IfNull(rm.sMITUMORINOKI, '') As sMITUMORINOKI, " +
                " IfNull(if(rm.sMITUMORIYUKOKIGEN='選択してください','',rm.sMITUMORIYUKOKIGEN), '') As sMITUMORIYUKOKIGEN, " +
                " IfNull(if(rm.cSHIHARAI='00','',rm.cSHIHARAI), '') As cSHIHARAI, " +
                " IfNull(mshr.sSHIHARAI, '') As sSHIHARAI, " +
                " IfNull(rm.cKYOTEN, '') As cKYOTEN, " +
                " IfNull(rm.cJYOTAI_MITUMORI, '') As cJYOTAI, " +
                " case IfNull(rm.cJYOTAI_MITUMORI, '')" +
                " when '00' then '失注'" +
                " when '01' then '見積提出済'" +
                " when '02' then '受注'" +
                " when '03' then '完了'" +
                " when '04' then '見積作成中'" +
                " when '05' then 'キャンセル'" +
                " when '06' then '売上済み'" +
                " else ''" +
                " END As sJYOTAI," +
                " date_format(rm.dURIAGEYOTEI, '%Y/%m/%d') As dURIAGEYOTEI," +
                " date_format(rm.dYOTEINOUKI, '%Y/%m/%d') As Juuchubi," +
                " date_format(rm.YU_dURIYOTEI, '%Y/%m/%d') As YU_dURIYOTEI," +
                " date_format(rm.dKANRYOUYOTEI, '%Y/%m/%d') As dKANRYOUYOTEI," +
                " date_format(rm.dKETSUTEI, '%Y/%m/%d') As dKETSUTEI," +
                " format( IfNull(rm.nURIAGEKINGAKU, 0),0) As Juuchukingaku," +
                " IfNull(rm.sBIKOU, '') As sBIKOU," +
                " IfNull(rm.sMEMO, '') As sMEMO," +
                " IfNull(if(rm.sUKEWATASIBASYO='選択してください','',rm.sUKEWATASIBASYO), '') As sUKEWATASIBASYO," +
                " IfNull(M_J_TANTOUSHA.sTANTOUSHA, '') As sTANTOUSHA, " +
                " IfNull(M_J_TANTOUSHA.cTANTOUSHA, '') As cTANTOUSHA, " +
                " format( IfNull(rm.nKINGAKU_G, 0),0)  As nKINGAKU_G, " +
                " format( IfNull(rm.nKINGAKU, 0),0) As nKINGAKU," +
                " format( IfNull(rm.nMITUMORISYOHIZE, 0),0) As nMITUMORISYOHIZE," +
                " format( IfNull(rm.nMITUMORINEBIKI, 0),0) As nMITUMORINEBIKI," +
                " format( IfNull(rm.nTANKA_G, 0),0) As nTANKA_G," +
                " format( IfNull(rm.nKINGAKU - rm.nSIIRE_G  , 0),0) As ARARI," +
                " CONCAT(FORMAT(IfNull((rm.nKINGAKU - rm.nSIIRE_G) / rm.nKINGAKU, 0) * 100, 1), '%') As ARARIRITSU , " +
                " IfNull(rm.cPJ, '') As cPJ," +
                " IfNull(rm.cKAKUDO, '') As cKAKUDO," +
                " date_format(rm.dHENKOU, '%Y/%m/%d') as dHENKOU," +
                " IfNull(mjt1.sTANTOUSHA, '') As sSAKUSEISYA," +
                " IfNull(mjt2.sTANTOUSHA, '') As sHENKOUSYA," +
                " IfNull(mjt1.cTANTOUSHA, '') As cSAKUSEISYA," +
                " IfNull(mjt2.cTANTOUSHA, '') As cHENKOUSYA" +
                " From R_MITUMORI as rm" +
                " RIGHT join" +
                " (SELECT DISTINCT r_mitumori.cMITUMORI AS M_C, MAX(r_mitumori.cMITUMORI_KO) AS M_KO" +
                " FROM r_mitumori GROUP BY r_mitumori.cMITUMORI) AS M ON M.M_C = rm.cMITUMORI" +
                " AND M.M_KO = rm.cMITUMORI_KO" +
                " left join R_BU_MITSU ON R_BU_MITSU.cMITUMORI = rm.cMITUMORI " +
                " left join" +
                " M_J_TANTOUSHA ON M_J_TANTOUSHA.cTANTOUSHA = rm.cEIGYOTANTOSYA " +
                " left join M_J_TANTOUSHA mjt1 ON rm.cSAKUSEISYA = mjt1.cTANTOUSHA " +
                " left join M_J_TANTOUSHA mjt2 ON rm.cHENKOUSYA = mjt2.cTANTOUSHA " +
                " left join m_shiharai mshr ON rm.cSHIHARAI = mshr.cSHIHARAI " +
                " Where '1' = '1' and rm.cMITUMORI like '%" + lblcMitumori.Text + "%'" +
                " group by rm.cMITUMORI " +
                " order by rm.cMITUMORI desc";
            #endregion
            MySqlCommand cmd = new MySqlCommand(sql, con);
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable dtable = new DataTable();
            da.Fill(dtable);
            con.Close();
            da.Dispose();
            if (dtable.Rows.Count > 0)
            {
                lnkcBukken.Text = dtable.Rows[0]["cBUKKEN"].ToString();
                lblcMitumori_Ko.Text = (Convert.ToDouble(dtable.Rows[0]["cMITUMORI_KO"].ToString()) + 1).ToString().PadLeft(2, '0');
                lblnHenkou.Text = dtable.Rows[0]["nHENKOU"].ToString();
                txtsMitumori.Text = dtable.Rows[0]["sMITUMORI"].ToString();　//見積名
                txtJisyaBango.Text = dtable.Rows[0]["cPJ"].ToString();  //自社番号

                #region 自社担当者
                lblcJISHATANTOUSHA.Text = dtable.Rows[0]["cTANTOUSHA"].ToString();
                lblsJISHATANTOUSHA.Text = dtable.Rows[0]["sTANTOUSHA"].ToString().Replace("<", "&lt").Replace(">", "&gt");
                if (!String.IsNullOrEmpty(lblcJISHATANTOUSHA.Text))
                {
                    divTantousyaBtn.Style["display"] = "none";
                    divTantousyaLabel.Style["display"] = "block";
                    lblsJISHATANTOUSHA.Attributes.Add("onClick", "BtnClick('MainContent_BT_JisyaTantousya_Add')");
                    upd_JISHATANTOUSHA.Update();
                }
                #endregion

                #region 見積日
                lbldMitumori.Text = dtable.Rows[0]["dMITUMORISAKUSEI"].ToString();
                if (!String.IsNullOrEmpty(lbldMitumori.Text))
                {
                    btnMitumoriDate.Style["display"] = "none";
                    divMitumoriDate.Style["display"] = "block";
                    updMitumoriDate.Update();
                    lbldMitumori.Attributes.Add("onClick", "BtnClick('MainContent_btnMitumoriDate')");
                    upddatePopup.Update();
                }
                #endregion

                txtUkewatashibasho.Text = dtable.Rows[0]["sUKEWATASIBASYO"].ToString(); //受渡場所
                txtBikou.Text = dtable.Rows[0]["sBIKOU"].ToString();　　//見積書備考
                txtShanaiMemo.Text = dtable.Rows[0]["sMEMO"].ToString();　　//社内メモ

                #region 受注日
                lbldJuuchu.Text = dtable.Rows[0]["Juuchubi"].ToString();
                if (!String.IsNullOrEmpty(lbldJuuchu.Text))
                {
                    btnJuuchuDate.Style["display"] = "none";
                    divJuuchuuDate.Style["display"] = "block";
                    updJuuchuuDate.Update();
                    lbldJuuchu.Attributes.Add("onClick", "BtnClick('MainContent_btnJuuchuDate')");
                    upddatePopup.Update();
                }
                #endregion

                txtJuuchuKingaku.Text = dtable.Rows[0]["Juuchukingaku"].ToString();  //受注金額

                #region 売上予定日
                lbldUriageYotei.Text = dtable.Rows[0]["dURIAGEYOTEI"].ToString();
                if (!String.IsNullOrEmpty(lbldUriageYotei.Text))
                {
                    btnUriageYoteiDate.Style["display"] = "none";
                    divUriageYoteiDate.Style["display"] = "block";
                    updUriageYoteiDate.Update();
                    lbldUriageYotei.Attributes.Add("onClick", "BtnClick('MainContent_btnUriageYoteiDate')");
                    upddatePopup.Update();
                }
                #endregion

                #region 受注予定日
                lbldJuuchuuYotei.Text = dtable.Rows[0]["YU_dURIYOTEI"].ToString();
                if (!String.IsNullOrEmpty(lbldJuuchuuYotei.Text))
                {
                    btnJuuchuuYoteibi.Style["display"] = "none";
                    divJuuchuuYoteiDate.Style["display"] = "block";
                    updJuuchuuYoteibi.Update();
                    lbldJuuchuuYotei.Attributes.Add("onClick", "BtnClick('MainContent_btnJuuchuuYoteibi')");
                    upddatePopup.Update();
                }
                #endregion

                #region 完了予定日
                lbldshuryoYotei.Text = dtable.Rows[0]["dKANRYOUYOTEI"].ToString();
                if (!String.IsNullOrEmpty(lbldshuryoYotei.Text))
                {
                    btnshuryoYoteibi.Style["display"] = "none";
                    divshuryoYoteiDate.Style["display"] = "block";
                    updshuryoYoteibi.Update();
                    lbldshuryoYotei.Attributes.Add("onClick", "BtnClick('MainContent_btnshuryoYoteibi')");
                    upddatePopup.Update();
                }
                #endregion

                #region 見積確定日
                lbldMitumoriKakutei.Text = dtable.Rows[0]["dKETSUTEI"].ToString();
                if (!String.IsNullOrEmpty(lbldMitumoriKakutei.Text))
                {
                    btnMitumoriKakuteibi.Style["display"] = "none";
                    divMitumoriKakuteiiDate.Style["display"] = "block";
                    updMitumoriKakuteibi.Update();
                    lbldMitumoriKakutei.Attributes.Add("onClick", "BtnClick('MainContent_btnMitumoriKakuteibi')");
                    upddatePopup.Update();
                }
                #endregion

                txtNouki.Text = dtable.Rows[0]["sMITUMORINOKI"].ToString();  //納期

                #region 拠点
                if (!String.IsNullOrEmpty(dtable.Rows[0]["cKYOTEN"].ToString()))
                {
                    lblcKYOTEN.Text = dtable.Rows[0]["cKYOTEN"].ToString();
                    divKyotenbtn.Style["display"] = "none";
                    divKyotenLabel.Style["display"] = "block";
                    updKyoten.Update();
                    MySqlCommand cmdKyoten = new MySqlCommand(" SELECT cCO,sKYOTEN FROM m_j_info WHERE cCO='" + lblcKYOTEN.Text + "' ", con);
                    MySqlDataAdapter daKyoten = new MySqlDataAdapter(cmdKyoten);
                    DataTable dtKyoten = new DataTable();
                    daKyoten.Fill(dtKyoten);
                    daKyoten.Dispose();
                    if (dtKyoten.Rows.Count > 0)
                    {
                        lblsKYOTEN.Text = dtKyoten.Rows[0]["sKYOTEN"].ToString().Replace("<", "&lt").Replace(">", "&gt");
                    }
                    lblsKYOTEN.Attributes.Add("onClick", "BtnClick('MainContent_btnKyotenAdd')");
                    updSentakuPopup.Update();
                }
                #endregion

                #region 有効
                lblsYuukou.Text = dtable.Rows[0]["sMITUMORIYUKOKIGEN"].ToString().Replace("<", "&lt").Replace(">", "&gt");
                if (!String.IsNullOrEmpty(lblsYuukou.Text))
                {
                    divYuuKoubtn.Style["display"] = "none";
                    divYuukouLabel.Style["display"] = "block";
                    updYuukou.Update();
                    lblsYuukou.Attributes.Add("onClick", "BtnClick('MainContent_btnYuukou')");
                    updSentakuPopup.Update();
                }
                #endregion

                #region 支払方法
                lblcShiharai.Text = dtable.Rows[0]["cSHIHARAI"].ToString();
                lblsShihariai.Text = dtable.Rows[0]["sSHIHARAI"].ToString().Replace("<", "&lt").Replace(">", "&gt");
                if (!String.IsNullOrEmpty(lblcShiharai.Text))
                {
                    divShiharaibtn.Style["display"] = "none";
                    divShiharaiLabel.Style["display"] = "block";
                    updShiharai.Update();
                    lblsShihariai.Attributes.Add("onClick", "BtnClick('MainContent_btnShiharai')");
                    updSentakuPopup.Update();
                }
                #endregion

                #region 見積状態
                lblcJoutai.Text = dtable.Rows[0]["cJYOTAI"].ToString();
                lblsJoutai.Text = dtable.Rows[0]["sJYOTAI"].ToString();
                if (!String.IsNullOrEmpty(lblcJoutai.Text))
                {
                    divJoutaibtn.Style["display"] = "none";
                    divJoutaiLabel.Style["display"] = "block";
                    updJoutai.Update();

                    lblsJoutai.Attributes.Add("onClick", "BtnClick('MainContent_btnJoutai')");
                    updSentakuPopup.Update();
                }
                #endregion

                #region 得意先
                lblsTOKUISAKI.Text = dtable.Rows[0]["sTOKUISAKI"].ToString().Replace("<","&lt").Replace(">","&gt");
                lblcTOKUISAKI.Text = dtable.Rows[0]["cTOKUISAKI"].ToString();
                if (!String.IsNullOrEmpty(lblcTOKUISAKI.Text))
                {
                    divTokuisakiBtn.Style["display"] = "none";
                    divTokuisakiLabel.Style["display"] = "block";
                    divTokuisakiSyosai.Style["display"] = "block";
                    btnTokuisaki.BorderStyle = BorderStyle.None;
                    updTokuisaki.Update();
                    lblsTOKUISAKI.Attributes.Add("onClick", "BtnClick('MainContent_btnTokuisaki')");
                    updSentakuPopup.Update();
                }
                #endregion

                #region 得意先担当者
                lblsTOKUISAKI_TAN.Text = dtable.Rows[0]["sTOKUISAKI_TAN"].ToString().Replace("<", "&lt").Replace(">", "&gt");
                lblsTOKUISAKI_TAN_JUN.Text = dtable.Rows[0]["sTOKUISAKI_TAN_Jun"].ToString();
                if (!String.IsNullOrEmpty(lblsTOKUISAKI_TAN.Text))
                {
                    divTokuisakiTanBtn.Style["display"] = "none";
                    divTokuisakiTanLabel.Style["display"] = "block";
                    divTokuisakiTanSyosai.Style["display"] = "block";
                    updTokuisakiTantou.Update();
                    //updHeader.Update();
                    lblsTOKUISAKI_TAN.Attributes.Add("onClick", "BtnClick('MainContent_btnTokuisakiTantou')");
                    updSentakuPopup.Update();
                }
                #endregion

                #region 請求先
                lblsSEIKYUSAKI.Text = dtable.Rows[0]["sSEIKYUSAKI"].ToString().Replace("<", "&lt").Replace(">", "&gt");
                lblcSEIKYUSAKI.Text = dtable.Rows[0]["cSEIKYUSAKI"].ToString();
                if (!String.IsNullOrEmpty(lblcSEIKYUSAKI.Text))
                {
                    divSEIKYUSAKIBtn.Style["display"] = "none";
                    divsSEIKYUSAKILabel.Style["display"] = "block";
                    divSEIKYUSAKISyosai.Style["display"] = "block";
                    btnSeikyusaki.BorderStyle = BorderStyle.None;
                    updSEIKYUSAKI.Update();
                    lblsSEIKYUSAKI.Attributes.Add("onClick", "BtnClick('MainContent_btnSeikyusaki')");
                    updSentakuPopup.Update();
                }
                #endregion

                #region 請求先担当者
                lblsSEIKYUSAKI_TAN.Text = dtable.Rows[0]["sSEIKYU_TAN"].ToString().Replace("<", "&lt").Replace(">", "&gt");
                lblsSEIKYUSAKI_TAN_JUN.Text = dtable.Rows[0]["sSEIKYU_TAN_Jun"].ToString();
                if (!String.IsNullOrEmpty(lblsSEIKYUSAKI_TAN.Text))
                {
                    divSeikyusakiTanBtn.Style["display"] = "none";
                    divSeikyusakiTanLabel.Style["display"] = "block";
                    divSeikyusakiTanSyosai.Style["display"] = "block";
                    updSeikyusakiTantou.Update();
                    //updHeader.Update();
                    lblsSEIKYUSAKI_TAN.Attributes.Add("onClick", "BtnClick('MainContent_btnSeikyusakiTantou')");
                    updSentakuPopup.Update();
                }
                #endregion

                #region 確度
                if (!String.IsNullOrEmpty(dtable.Rows[0]["cKAKUDO"].ToString()))
                {
                    try
                    {
                        DDL_Kakudo.SelectedValue = dtable.Rows[0]["cKAKUDO"].ToString();
                    }
                    catch { }
                }
                #endregion

                lblGokeiKingaku.Text = dtable.Rows[0]["nKINGAKU_G"].ToString();
                lblKingaku.Text = dtable.Rows[0]["nKINGAKU"].ToString();
                lblShohizei.Text = dtable.Rows[0]["nMITUMORISYOHIZE"].ToString();
                txtShusseiNebiki.Text = dtable.Rows[0]["nMITUMORINEBIKI"].ToString();
                lblTeikaGokei.Text = dtable.Rows[0]["nTANKA_G"].ToString();
                lblArari.Text = dtable.Rows[0]["ARARI"].ToString();
                lblArariRitsu.Text = dtable.Rows[0]["ARARIRITSU"].ToString();

                MySqlCommand cmdSakuseki = new MySqlCommand("SELECT date_format(hm.dHENKOU, '%Y/%m/%d') as dSakuseki,hm.cHENKOUSYA as cSakuseki,mjt.sTANTOUSHA as sSakuseki FROM h_mitumori as hm LEFT join m_j_tantousha as mjt ON hm.cHENKOUSYA=mjt.cTANTOUSHA Where cMITUMORI='" + lblcMitumori.Text + "' AND cMITUMORI_KO='01' AND nJUNBAN='1';", con);
                MySqlDataAdapter daSakuseki = new MySqlDataAdapter(cmdSakuseki);
                DataTable dtSakusei = new DataTable();
                daSakuseki.Fill(dtSakusei);
                daSakuseki.Dispose();

                lblSakusekibi.Text = dtSakusei.Rows[0]["dSakuseki"].ToString();
                lblSakuseisya.Text = dtSakusei.Rows[0]["sSakuseki"].ToString();
                lblcSakuseisya.Text = dtSakusei.Rows[0]["cSakuseki"].ToString();
                lblHenkoubi.Text = dtable.Rows[0]["dHENKOU"].ToString();
                lblHenkousya.Text = dtable.Rows[0]["sHENKOUSYA"].ToString();
                lblcHenkousya.Text = dtable.Rows[0]["cHENKOUSYA"].ToString();
            }

            #region added by YG
            Session["cTokuisaki"] = lblcTOKUISAKI.Text;
            Session["sTokuisaki"] = lblsTOKUISAKI.Text;
            Session["TOUKUISAKITANTOU"] = lblsTOKUISAKI_TAN.Text;
            Session["TokuisakiTanJun"] = lblsTOKUISAKI_TAN_JUN.Text;
            Session["cSeikyusaki"] = lblcSEIKYUSAKI.Text;
            Session["sSeikyusaki"] = lblsSEIKYUSAKI.Text;
            Session["sSEIKYUSAKI_TAN"] = lblsSEIKYUSAKI_TAN.Text;
            Session["sSEIKYUSAKI_TAN_JUN"] = lblsSEIKYUSAKI_TAN_JUN.Text;
            Session["sMITUMORI"] = txtsMitumori.Text;
            Session["sKYOTEN1"] = lblsKYOTEN.Text;
            Session["cMitumoriJoutai1"] = lblcJoutai.Text;  //20220404 Added by MiMi
            Session["sMitumoriJoutai1"] = lblsJoutai.Text; //20220404 Added by MiMi
            #endregion
        }
        #endregion

        #region getSyouhinData()
        protected void getSyouhinData()
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
            string sql = "Select " +
                " IfNull(r_mitumori_m.cMITUMORI, '') As cMITUMORI, " +
                " case r_mitumori_m.nINSATSU_GYO " +
                " when 0 then '' " +
                " else cast(r_mitumori_m.nINSATSU_GYO as char) " +
                " end As nINSATSU_GYO," +
                " IfNull(r_mitumori_m.cMITUMORI_KO, '') As cMITUMORI_KO," +
                " IfNull(r_mitumori_m.nGYOUNO, 0) As nGYOUNO," +
                " IfNull(r_mitumori_m.cSYOUHIN, '') As cSYOUHIN," +
                " IfNull(r_mitumori_m.sSYOUHIN_R, '') As sSYOUHIN_R," +
                " format( IfNull(r_mitumori_m.nTANKA, 0),0)  As nTANKA," +
                " format(IfNull(r_mitumori_m.nSURYO, 0),2) As nSURYO," +
                " IfNull(r_mitumori_m.cSHIIRESAKI, '') As cSHIIRESAKI," +
                " IfNull(r_mitumori_m.sSHIIRESAKI, '') As sSHIIRESAKI," +
                " format( IfNull(r_mitumori_m.nSIIRETANKA, 0),0) As nSIIRETANKA," +
                " format( IfNull(r_mitumori_m.nSIIREKINGAKU, 0),0) As nSIIREKINGAKU," +
                " format( IfNull(r_mitumori_m.nSIKIRITANKA, 0),0) As nSIKIRITANKA," +
                " format( IfNull(r_mitumori_m.nSIKIRIKINGAKU, 0),0) As nSIKIRIKINGAKU," +
                " IfNull(r_mitumori_m.sTANI, '') As sTANI," +
                " IfNull(r_mitumori_m.nKINGAKU, 0) As nKINGAKU," +
                " IfNull(r_mitumori_m.nRITU, 100) As nRITU," +
                " IfNull(r_mitumori_m.cSYOUSAI, '') As cSYOUSAI," +
                " IfNull(r_mitumori_m.sSETSUMUI, '') As sSETSUMUI," +
                " IfNull(r_mitumori_m.fJITAIS, 0) As fJITAIS," +
                " IfNull(r_mitumori_m.fJITAIQ, 0) As fJITAIQ," +
                " ifnull(ms.fKazei, 0) AS fkazei," +
                " format( IfNull(r_mitumori_m.nSIKIRIKINGAKU - r_mitumori_m.nSIIREKINGAKU,0),0)  As nARARI," +
                " CONCAT(FORMAT(IfNull((r_mitumori_m.nSIKIRIKINGAKU - r_mitumori_m.nSIIREKINGAKU) / r_mitumori_m.nSIKIRIKINGAKU,0) * 100,1),'%') As nARARISu," +
                " r_mitumori_m.rowNO AS rowNO," +
                " ifnull(r_mitumori_m.sMEMO, '') as sMEMO," +
                " ifnull(r_mitumori_m.fCHECK, '') as fCHECK," +
                " IfNull(r_mitumori_m.fgentankatanka, '0') As fgentankatanka,'0' As jissekigenka," +
                " IfNull(r_mitumori_m.sKUBUN, '') As sKUBUN " +
                " From r_mitumori_m" +
                " left join  m_syouhin ms ON r_mitumori_m.cSYOUHIN = ms.cSYOUHIN " +
                " Where '1' = '1' and r_mitumori_m.cMITUMORI like '%" + lblcMitumori.Text + "%' order by r_mitumori_m.nGYOUNO; ";
            MySqlCommand cmd = new MySqlCommand(sql, cn);
            cmd.CommandTimeout = 0;
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable dtable = new DataTable();
            da.Fill(dtable);
            cn.Close();
            da.Dispose();
            if (dtable.Rows.Count > 0)
            {
                DataTable dt = CreateSyouhinTableColomn();
                int rowcount = 15;
                if (dtable.Rows.Count > 15)
                {
                    rowcount = dtable.Rows.Count;
                }
                for (int i = 1; i <= rowcount; i++)
                {
                    DataRow dr = dt.NewRow();
                    if (dtable.Rows.Count >= i)
                    {
                        dr[0] = "0";
                        dr[1] = dtable.Rows[i - 1]["cSYOUHIN"].ToString();
                        dr[2] = dtable.Rows[i - 1]["sSYOUHIN_R"].ToString();
                        Double nsuryo = Convert.ToDouble(dtable.Rows[i - 1]["nSURYO"].ToString());
                        dr[3] = nsuryo.ToString("#,##0.##");
                        dr[4] = dtable.Rows[i - 1]["sTANI"].ToString();
                        dr[5] = dtable.Rows[i - 1]["nTANKA"].ToString();
                        dr[6] = dtable.Rows[i - 1]["nSIKIRIKINGAKU"].ToString();
                        dr[7] = dtable.Rows[i - 1]["nSIIRETANKA"].ToString();
                        dr[8] = dtable.Rows[i - 1]["nSIIREKINGAKU"].ToString();
                        dr[9] = dtable.Rows[i - 1]["nARARI"].ToString();
                        dr[10] = dtable.Rows[i - 1]["nARARISu"].ToString();
                        dr[11] = dtable.Rows[i - 1]["fgentankatanka"].ToString();
                        dr[12] = dtable.Rows[i - 1]["rowNO"].ToString();
                        dr[13] = dtable.Rows[i - 1]["nRITU"].ToString() + "%";
                        dr[14] = dtable.Rows[i - 1]["sKUBUN"].ToString();
                        dr[15]= dtable.Rows[i - 1]["nSIKIRITANKA"].ToString();
                        dr[16] = dtable.Rows[i - 1]["fJITAIS"].ToString();
                        dr[17] = dtable.Rows[i - 1]["fJITAIQ"].ToString();
                        dt.Rows.Add(dr);
                    }
                    else
                    {
                        dr[0] = "0";
                        dr[1] = "";
                        dr[2] = "";
                        dr[3] = "";
                        dr[4] = "";
                        dr[5] = "";
                        dr[6] = "";
                        dr[7] = "";
                        dr[8] = "";
                        dr[9] = "";
                        dr[10] = "";
                        dr[11] = "1";
                        dr[12] = "0";
                        dr[13] = "100%";
                        dr[14] = "";
                        dr[15] = "";
                        dr[16] = "0";
                        dr[17] = "0";
                        dt.Rows.Add(dr);
                    }
                }

                var max = dt.AsEnumerable().Max(x => int.Parse(x.Field<string>("rowNo")));
                HF_maxRowNo.Value = max.ToString();

                GV_MitumoriSyohin_Original.DataSource = dt;
                GV_MitumoriSyohin_Original.DataBind();
                updMitsumoriSyohinGrid.Update();

                ViewState["SyouhinTable"] = dt;
            }
            else
            {
                DataTable dt = CreateSyouhinTableColomn();
                for (int i = 1; i <= 15; i++)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = "0";
                    dr[1] = "";
                    dr[2] = "";
                    dr[3] = "";
                    dr[4] = "";
                    dr[5] = "";
                    dr[6] = "";
                    dr[7] = "";
                    dr[8] = "";
                    dr[9] = "";
                    dr[10] = "";
                    dr[11] = "1";
                    dr[12] = "0";
                    dr[13] = "100%";
                    dr[14] = "";
                    dr[15] = "";
                    dr[16] = "0";
                    dr[17] = "0";
                    dt.Rows.Add(dr);
                }
                var max = dt.AsEnumerable().Max(x => int.Parse(x.Field<string>("rowNo")));
                HF_maxRowNo.Value = max.ToString();

                GV_MitumoriSyohin_Original.DataSource = dt;
                GV_MitumoriSyohin_Original.DataBind();
                updMitsumoriSyohinGrid.Update();

                ViewState["SyouhinTable"] = dt;
            }

            SyohinKoumokuSort();
            HasCheckRow();
        }
        #endregion

        #region LoadImage()
        protected void LoadImage()
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
            string sql = "SELECT mmf.cFILE as cFILE,mmf.nJUNBAN as nJUNBAN,mf.sPATH_SERVER_SOURCE as sPATH_SERVER_SOURCE FROM m_mitsu_file as mmf LEFT JOIN m_file as mf ON mmf.cFILE=mf.cFILE Where mmf.fVISABLE='1' AND mmf.cMITUMORI='" + lblcMitumori.Text + "';";
            MySqlCommand cmd = new MySqlCommand(sql, cn);
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable dtable = new DataTable();
            da.Fill(dtable);
            cn.Close();
            da.Dispose();
            foreach (DataRow dr in dtable.Rows)
            {
                string spath = dr["sPATH_SERVER_SOURCE"].ToString();
                string ext = Path.GetExtension(spath);
                if (ext.ToLower().Contains("gif") || ext.ToLower().Contains("jpg") || ext.ToLower().Contains("jpeg") || ext.ToLower().Contains("png") || ext.ToLower().Contains("jfif"))
                {
                    if (File.Exists(spath))
                    {
                        System.Drawing.Image originalImage;
                        FileInfo fi = new FileInfo(spath);
                        using (FileStream fileStream = new FileStream(spath, FileMode.Open, FileAccess.Read))
                        {
                            originalImage = new Bitmap(fileStream);
                            byte[] imageBytes = System.IO.File.ReadAllBytes(spath);
                            if (fi.Length > 23552)//about 23KB
                            {
                                imageBytes = ResizeImageFile(imageBytes, 760);
                            }
                            //originalImage = System.Drawing.Image.FromStream(fileStream);
                            EncoderParameters myEncoderParameters = new EncoderParameters(1);
                            EncoderParameter myEncoderParameter = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 75L);
                            myEncoderParameters.Param[0] = myEncoderParameter;
                            //originalImage.Save(folderPath + Path.GetFileName(spath), GetEncoderInfo("image/jpeg"), myEncoderParameters);
                            string base64String = Convert.ToBase64String(imageBytes);
                            string imgurl = "data:image/png;base64," + base64String;

                            if (dr["nJUNBAN"].ToString() == "5")
                            {
                                HyoshiuploadedImage.Src = imgurl;
                                HyoshiuploadedImage.Attributes.Add("class", "JC10DaiHyouImage");
                                BT_HyoshiImgaeDelete.CssClass = "JC10ImageDelete";
                                HyoshidragZone.Attributes.Add("class", "DisplayNone");
                                HF_HyoshiFileName.Value = fi.FullName;
                            }
                            else if (dr["nJUNBAN"].ToString() == "6")
                            {
                                Mitumorisho1uploadedImage.Src = imgurl;
                                Mitumorisho1uploadedImage.Attributes.Add("class", "JC10DaiHyouImage");
                                BT_Mitumorisho1ImgaeDelete.CssClass = "JC10ImageDelete";
                                Mitumorisho1dragZone.Attributes.Add("class", "DisplayNone");
                                HF_Gazo1FileName.Value = fi.FullName;
                            }
                            else if (dr["nJUNBAN"].ToString() == "7")
                            {
                                Mitumorisho2uploadedImage.Src = imgurl;
                                Mitumorisho2uploadedImage.Attributes.Add("class", "JC10DaiHyouImage");
                                BT_Mitumorisho2ImgaeDelete.CssClass = "JC10ImageDelete";
                                Mitumorisho2dragZone.Attributes.Add("class", "DisplayNone");
                                HF_Gazo2FileName.Value = fi.FullName;
                            }
                            updHeader.Update();
                        }
                    }
                }
                else
                {
                    //fe.Add(ext.Replace(".", "").ToUpper());
                    //Session["fe"] = fe;
                    //ScriptManager.RegisterStartupScript(this, GetType(), "displayalertmessage", "confirm2()", true);
                }
            }
        }
        #endregion

        #region CreateSyosaiTableColomn
        private DataTable CreateSyosaiTableColomn()
        {
            DataTable dt_Syohin = new DataTable();
            dt_Syohin.Columns.Add("status");
            dt_Syohin.Columns.Add("cSYOHIN");
            dt_Syohin.Columns.Add("sSYOHIN");
            dt_Syohin.Columns.Add("nSURYO");
            dt_Syohin.Columns.Add("cTANI");
            dt_Syohin.Columns.Add("nTANKA");
            dt_Syohin.Columns.Add("nTANKAGOUKEI");
            dt_Syohin.Columns.Add("nGENKATANKA");
            dt_Syohin.Columns.Add("nGENKAGOUKEI");
            dt_Syohin.Columns.Add("nARARI");
            dt_Syohin.Columns.Add("nARARISu");
            dt_Syohin.Columns.Add("rowNo");
            dt_Syohin.Columns.Add("nRITU");
            dt_Syohin.Columns.Add("nSIKIRITANKA");
            dt_Syohin.Columns.Add("fJITAIS");
            return dt_Syohin;
        }
        #endregion

        #region getSyosaiSyouhinData()
        protected void getSyosaiSyouhinData()
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
            string sql = "SELECT "
                + " IfNull(rmm2.cMITUMORI, '') As cMITUMORI,"
                + " case rmm2.nINSATSU_GYO"
                + " when 0 then ''"
                + " else cast(rmm2.nINSATSU_GYO as char)"
                + " end As nINSATSU_GYO,"
                + " IfNull(rmm2.cMITUMORI_KO, '') As cMITUMORI_KO,"
                + " IfNull(rmm2.nGYOUNO, 0) As nGYOUNO,"
                + " IfNull(rmm2.cSYOUHIN, '') As cSYOUHIN,"
                + " IfNull(rmm2.sSYOUHIN_R, '') As sSYOUHIN_R,"
                + " format( IfNull(rmm2.nTANKA, 0), 0)  As nTANKA,"
                + " format(IfNull(rmm2.nSURYO, 0), 2) As nSURYO,"
                + " IfNull(rmm2.cSHIIRESAKI, '') As cSHIIRESAKI,"
                + " IfNull(rmm2.sSHIIRESAKI, '') As sSHIIRESAKI,"
                + " format( IfNull(rmm2.nSIIRETANKA, 0), 0) As nSIIRETANKA,"
                + " format( IfNull(rmm2.nSIIREKINGAKU, 0), 0) As nSIIREKINGAKU,"
                + " IfNull(rmm2.nSIKIRITANKA, 0) As nSIKIRITANKA,"
                + " format( IfNull(rmm2.nSIKIRIKINGAKU, 0), 0) As nSIKIRIKINGAKU,"
                + " IfNull(rmm2.sTANI, '') As sTANI,"
                + " IfNull(rmm2.nKINGAKU, 0) As nKINGAKU,"
                + " IfNull(rmm2.nRITU, 100) As nRITU,"
                + " IfNull(rmm2.cSYOUSAI, '') As cSYOUSAI,"
                + " IfNull(rmm2.sSETSUMUI, '') As sSETSUMUI,"
                + " IfNull(rmm2.fJITAIS, 0) As fJITAIS,"
                + " IfNull(rmm2.fJITAIQ, 0) As fJITAIQ,"
                + " rmm2.rowNO AS rowNO,"
                + " rmm2.rowNO2 AS rowNO2,"
                + " ifnull(rmm2.sMEMO, '') as sMEMO,"
                + " ifnull(rmm2.fCHECK, '') as fCHECK,"
                + " rmm.nGYOUNO as nGYOUNO1"
                + " FROM r_mitumori_m2 rmm2"
                + " LEFT JOIN r_mitumori_m rmm ON rmm2.cMITUMORI=rmm.cMITUMORI AND rmm2.rowNO=rmm.rowNO"
                + " Where rmm2.cMitumori = '" + lblcMitumori.Text + "' order by rmm2.rowNO asc, rmm2.nGYOUNO asc; ";
            MySqlCommand cmd = new MySqlCommand(sql, cn);
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable dtable = new DataTable();
            da.Fill(dtable);
            cn.Close();
            da.Dispose();

            int prerowNo = -1;
            DataTable dt = CreateSyosaiTableColomn();
            if (dtable.Rows.Count > 0)
            {
                DataTable dt_meisai = new DataTable();
                dt_meisai = ViewState["SyouhinTable"] as DataTable;
                for (int i = 1; i <= dtable.Rows.Count; i++)
                {
                    int rowNo = Convert.ToInt32(dtable.Rows[i - 1]["rowNO"].ToString());

                    if (rowNo != prerowNo)
                    {
                        DataRow[] rows = dtable.Select("rowNo = '" + rowNo.ToString() + "'");
                        if (rows.Length > 0)
                        {
                            double genka = 0;
                            double tanka = 0;
                            for (int r = 0; r <= rows.Count(); r++)
                            {
                                if (!String.IsNullOrEmpty(dtable.Rows[i - 1]["nSIIREKINGAKU"].ToString()))
                                {
                                    tanka+= Convert.ToDouble(dtable.Rows[i - 1]["nSIKIRIKINGAKU"].ToString());
                                    genka += Convert.ToDouble(dtable.Rows[i - 1]["nSIIREKINGAKU"].ToString());
                                }
                            }

                            DataRow[] rows_Meisai = dt_meisai.Select("rowNo = '" + rowNo.ToString() + "'");
                            if (rows_Meisai.Length > 0)
                            {
                                int gyoNO = dt_meisai.Rows.IndexOf(rows_Meisai[0]);
                                HF_fGenkaChange.Value = HF_fGenkaChange.Value.Replace(rowNo.ToString(), "");
                                HF_fTankaChange.Value = HF_fTankaChange.Value.Replace(rowNo.ToString(), "");

                                if (tanka != 0)
                                {
                                    HF_fTankaChange.Value += rowNo + ",";
                                    (GV_MitumoriSyohin_Original.Rows[gyoNO].FindControl("txtnTANKA") as TextBox).Enabled = false;
                                    (GV_MitumoriSyohin.Rows[gyoNO].FindControl("txtnTANKA") as TextBox).Enabled = false;
                                }
                                else
                                {
                                    (GV_MitumoriSyohin_Original.Rows[gyoNO].FindControl("txtnTANKA") as TextBox).Enabled = true;
                                    (GV_MitumoriSyohin.Rows[gyoNO].FindControl("txtnTANKA") as TextBox).Enabled = true;
                                }

                                if (genka != 0)
                                {
                                    HF_fGenkaChange.Value += rowNo + ",";
                                    (GV_MitumoriSyohin_Original.Rows[gyoNO].FindControl("txtnGENKATANKA") as TextBox).Enabled = false;
                                    (GV_MitumoriSyohin.Rows[gyoNO].FindControl("txtnGENKATANKA") as TextBox).Enabled = false;
                                }
                                else
                                {
                                    (GV_MitumoriSyohin_Original.Rows[gyoNO].FindControl("txtnGENKATANKA") as TextBox).Enabled = true;
                                    (GV_MitumoriSyohin.Rows[gyoNO].FindControl("txtnGENKATANKA") as TextBox).Enabled = true;
                                }

                                (GV_MitumoriSyohin_Original.Rows[gyoNO].FindControl("btnSyohinShosai") as Button).Text = rows.Length.ToString();
                                updMitsumoriSyohinGrid.Update();
                                updHeader.Update();
                            }
                        }
                        prerowNo = rowNo;
                    }

                    DataRow dr = dt.NewRow();
                    dr[0] = "0";
                    dr[1] = dtable.Rows[i - 1]["cSYOUHIN"].ToString();
                    dr[2] = dtable.Rows[i - 1]["sSYOUHIN_R"].ToString();
                    Double nsuryo = Convert.ToDouble(dtable.Rows[i - 1]["nSURYO"].ToString());
                    dr[3] = nsuryo.ToString("#,##0.##");
                    dr[4] = dtable.Rows[i - 1]["sTANI"].ToString();
                    dr[5] = dtable.Rows[i - 1]["nTANKA"].ToString();
                    dr[6] = dtable.Rows[i - 1]["nSIKIRIKINGAKU"].ToString();
                    dr[7] = dtable.Rows[i - 1]["nSIIRETANKA"].ToString();
                    dr[8] = dtable.Rows[i - 1]["nSIIREKINGAKU"].ToString();
                    Double tankaGokei = Convert.ToDouble(dtable.Rows[i - 1]["nSIKIRIKINGAKU"].ToString());
                    Double genkaGokei = Convert.ToDouble(dtable.Rows[i - 1]["nSIIREKINGAKU"].ToString());
                    Double arari = tankaGokei - genkaGokei;
                    dr[9] = arari;
                    double nArariSu = (arari / tankaGokei) * 100;
                    if (tankaGokei == 0)
                    {
                        nArariSu = 0;
                    }
                    dr[10] = nArariSu.ToString("###0.0") + "%";
                    dr[11] = dtable.Rows[i - 1]["rowNo"].ToString();
                    dr[12] = dtable.Rows[i - 1]["nRITU"].ToString() + "%";
                    dr[13] = dtable.Rows[i - 1]["nSIKIRITANKA"].ToString();
                    dr[14] = dtable.Rows[i - 1]["fJITAIS"].ToString();
                    dt.Rows.Add(dr);
                }
            }
            GV_Syosai.DataSource = dt;
            GV_Syosai.DataBind();
            updMitsumoriSyohinGrid.Update();
            updHeader.Update();
        }
        #endregion

        #region GV_MitumoriSyohin_Original_RowDataBound
        protected void GV_MitumoriSyohin_Original_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int rowindex = e.Row.RowIndex;
                #region getsTANI 
                DataTable dtable = new DataTable();
                if (ViewState["dtTani"] == null)
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
                    string getsTANI = " select distinct cTANI, sTANI from M_TANI order by cTANI ";
                    MySqlCommand cmd = new MySqlCommand(getsTANI, cn);
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    da.Fill(dtable);
                    cn.Close();
                    da.Dispose();
                    ViewState["dtTani"] = dtable;
                }
                else
                {
                    dtable = ViewState["dtTani"] as DataTable;
                }

                DropDownList DropDownList1 = (e.Row.FindControl("DDL_cTANI") as DropDownList);
                DropDownList1.DataSource = dtable;
                DropDownList1.DataTextField = "sTANI";
                DropDownList1.DataValueField = "sTANI";
                DropDownList1.DataBind();

                TextBox txtBox = (e.Row.FindControl("txtcSYOHIN") as TextBox);
                string lblcTANI = (e.Row.FindControl("lblcTANI") as Label).Text;
                //if (txtBox.Text != "")
                //{
                try
                {
                    DropDownList1.Items.FindByText(lblcTANI).Selected = true;
                    //cb_cTANI.Items.FindByText(lblcTANI).Selected = true;
                }
                catch
                { }
                //}
                DropDownList1.Items.Insert(0, new ListItem(" ", "00"));
                //cb_cTANI.Items.Insert(0, new ListItem(" ", "00"));

                #endregion

                Label status = (e.Row.FindControl("lblhdnStatus") as Label);
                CheckBox chk = (e.Row.FindControl("chkSelectSyouhin") as CheckBox);
                if (status.Text == "1")
                {
                    chk.Checked = true;
                }
                else
                {
                    chk.Checked = false;
                }
            }
        }

        #endregion

        #region GV_MitumoriSyohin_RowDataBound
        protected void GV_MitumoriSyohin_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(GV_MitumoriSyohin, "Select$" + e.Row.RowIndex);
                int rowindex = e.Row.RowIndex;
                #region getsTANI 
                DataTable dtable = new DataTable();
                if (ViewState["dtTani"] == null)
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
                    string getsTANI = " select distinct cTANI, sTANI from M_TANI order by cTANI ";
                    MySqlCommand cmd = new MySqlCommand(getsTANI, cn);
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    da.Fill(dtable);
                    cn.Close();
                    da.Dispose();
                    ViewState["dtTani"] = dtable;
                }
                else
                {
                    dtable = ViewState["dtTani"] as DataTable;
                }

                DropDownList DropDownList1 = (e.Row.FindControl("DDL_cTANI") as DropDownList);
                DropDownList1.DataSource = dtable;
                DropDownList1.DataTextField = "sTANI";
                DropDownList1.DataValueField = "sTANI";
                DropDownList1.DataBind();

                TextBox txtBox = (e.Row.FindControl("txtcSYOHIN") as TextBox);
                string lblcTANI = (e.Row.FindControl("lblcTANI") as Label).Text;
                //if (txtBox.Text != "")
                //{
                try
                {
                    DropDownList1.Items.FindByText(lblcTANI).Selected = true;
                    //cb_cTANI.Items.FindByText(lblcTANI).Selected = true;
                }
                catch
                { }
                //}
                DropDownList1.Items.Insert(0, new ListItem(" ", "00"));
                //cb_cTANI.Items.Insert(0, new ListItem(" ", "00"));

                #endregion

                Label status = (e.Row.FindControl("lblhdnStatus") as Label);
                CheckBox chk = (e.Row.FindControl("chkSelectSyouhin") as CheckBox);
                if (status.Text == "1")
                {
                    chk.Checked = true;
                }
                else
                {
                    chk.Checked = false;
                }

                #region 小 、強力
                Label lbl_fJITAIS = (e.Row.FindControl("lblfjitais") as Label);
                Label lbl_fJITAIQ = (e.Row.FindControl("lblfjitaiq") as Label);
                LinkButton LK_syou = (e.Row.FindControl("LK_Syou") as LinkButton);
                LinkButton LK_kyou = (e.Row.FindControl("LK_Kyo") as LinkButton);
                TextBox txt_sSyouhin = (e.Row.FindControl("txtsSYOHIN") as TextBox);
                if (lbl_fJITAIS.Text == "0")
                {
                    LK_syou.BackColor = System.Drawing.Color.White;
                    //txt_sSyouhin.Style.Add("font-size", "13px !important");
                    txt_sSyouhin.MaxLength = 50;
                }
                else
                {
                    LK_syou.BackColor = ColorTranslator.FromHtml("#f2f2f2");
                    //txt_sSyouhin.Style.Add("font-size", "11px !important");
                    txt_sSyouhin.MaxLength = 60;
                }
                if (lbl_fJITAIQ.Text == "0")
                {
                    LK_kyou.BackColor = System.Drawing.Color.White;
                    //txt_sSyouhin.Font.Bold = false;
                }
                else
                {
                    LK_kyou.BackColor = ColorTranslator.FromHtml("#f2f2f2");
                    //txt_sSyouhin.Font.Bold = true;
                }
                #endregion

                #region 詳細
                try
                {
                    Button btn = (GV_MitumoriSyohin_Original.Rows[e.Row.RowIndex].FindControl("btnSyohinShosai") as Button);
                    (e.Row.FindControl("btnSyohinShosai") as Button).Text = btn.Text;
                }
                catch { }
                #endregion

                #region 区分
                Label lblKubun = (e.Row.FindControl("lblKubun") as Label);
                Label lblKubun1 = (e.Row.FindControl("lblKubun1") as Label);
                if (lblKubun.Text == "間")
                {
                    lblKubun1.Text = "";
                }
                else
                {
                    if (lblKubun.Text == "見" || lblKubun.Text == "計")
                    {
                        //e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffffe1");
                        try
                        {
                            String gokei = (GV_MitumoriSyohin_Original.Rows[e.Row.RowIndex].FindControl("lblTankaGokei") as Label).Text;

                            #region toDelete
                            //(e.Row.FindControl("btnSyohinShosai") as Button).Enabled = false;
                            //(e.Row.FindControl("btnSyohinShosai") as Button).CssClass = "JC10DisableButton";
                            //try  //for handle 非表示
                            //{
                            //    (e.Row.FindControl("txtnSURYO") as TextBox).Text = "";
                            //    (e.Row.FindControl("txtnSURYO") as TextBox).Enabled = false;
                            //    (e.Row.FindControl("txtnSURYO") as TextBox).CssClass = "form-control TextboxStyle JC10GridTextBox JC10TextboxDisable";
                            //}
                            //catch { }
                            //try //for handle 非表示
                            //{
                            //    (e.Row.FindControl("txtnTANKA") as TextBox).Text = "";
                            //    (e.Row.FindControl("txtnTANKA") as TextBox).Enabled = false;
                            //    (e.Row.FindControl("txtnTANKA") as TextBox).CssClass = "form-control TextboxStyle JC10GridTextBox JC10TextboxDisable";
                            //}
                            //catch { }
                            //if (lblKubun.Text == "見")
                            //{
                            //    try //for handle 非表示
                            //    {
                            //        (e.Row.FindControl("lblTankaGokei") as Label).Text = "";
                            //    }
                            //    catch { }
                            //}
                            //else if (lblKubun.Text == "計")
                            //{
                            //    (e.Row.FindControl("btnSyohinSelect") as Button).Enabled = false;
                            //    (e.Row.FindControl("btnSyohinSelect") as Button).CssClass = "JC10DisableButton";
                            //    try //for handle 非表示
                            //    {
                            //        (e.Row.FindControl("txtcSYOHIN") as TextBox).Enabled = false;
                            //        (e.Row.FindControl("txtcSYOHIN") as TextBox).CssClass = "form-control TextboxStyle JC10GridTextBox JC10TextboxDisable";
                            //    }
                            //    catch { }
                            //    try //for handle 非表示
                            //    {
                            //        (e.Row.FindControl("txtsSYOHIN") as TextBox).Enabled = false;
                            //        (e.Row.FindControl("txtsSYOHIN") as TextBox).CssClass = "form-control TextboxStyle JC10GridTextBox JC10TextboxDisable";
                            //    }
                            //    catch { }
                            //}
                            //try //for handle 非表示
                            //{
                            //    (e.Row.FindControl("txtnGENKATANKA") as TextBox).Text = "";
                            //    (e.Row.FindControl("txtnGENKATANKA") as TextBox).Enabled = false;
                            //    (e.Row.FindControl("txtnGENKATANKA") as TextBox).CssClass = "form-control TextboxStyle JC10GridTextBox JC10TextboxDisable";
                            //}
                            //catch { }
                            //try //for handle 非表示
                            //{
                            //    (e.Row.FindControl("lblGenkaGokei") as Label).Text = "";
                            //}
                            //catch { }
                            //try //for handle 非表示
                            //{
                            //    (e.Row.FindControl("lblnARARI") as Label).Text = "";
                            //}
                            //catch { }
                            //try //for handle 非表示
                            //{
                            //    (e.Row.FindControl("lblnARARISu") as Label).Text = "";
                            //}
                            //catch { }
                            //try //for handle 非表示
                            //{
                            //    (e.Row.FindControl("txtnRITU") as TextBox).Enabled = false;
                            //    (e.Row.FindControl("txtnRITU") as TextBox).CssClass = "form-control TextboxStyle JC10GridTextBox JC10TextboxDisable";
                            //}
                            //catch { }
                            //try //for handle 非表示
                            //{
                            //    (e.Row.FindControl("DDL_cTANI") as DropDownList).Enabled = false;
                            //    (e.Row.FindControl("DDL_cTANI") as DropDownList).CssClass = "form-control JC10GridTextBox JC10TextboxDisable";
                            //}
                            //catch { }
                            #endregion

                            if (lblKubun.Text == "見")
                            {
                                int column_count = e.Row.Cells.Count;
                                TextBox txtMidashi = (e.Row.FindControl("txtMidashi") as TextBox);
                                Button btnSyosai = (e.Row.FindControl("btnSyohinShosai") as Button);
                                e.Row.Cells[3].ColumnSpan = column_count - 6;
                                txtMidashi.Visible = true;
                                btnSyosai.Visible = false;
                                txtMidashi.Text = (GV_MitumoriSyohin_Original.Rows[e.Row.RowIndex].FindControl("txtsSYOHIN") as TextBox).Text;
                                //txtMidashi.Style.Add("min-width", midashiTextboxWidth + "px !important");
                                for (int c = 4; c < column_count - 2; c++)
                                {
                                    e.Row.Cells[c].Visible = false;
                                }
                            }
                            if (lblKubun.Text == "計")
                            {
                                int column_count = e.Row.Cells.Count;
                                TextBox txtSyokei = (e.Row.FindControl("txtSyokei") as TextBox);
                                Button btnSyosai = (e.Row.FindControl("btnSyohinShosai") as Button);
                                txtSyokei.Visible = true;
                                btnSyosai.Visible = false;

                                if (GokeiColumn_Position != "No")
                                {
                                    e.Row.Cells[3].ColumnSpan = GokeiColumn + 2;
                                    //txtSyokei.Style.Add("min-width", BeforeSyoekiTextboxWidth + "px !important");
                                    for (int c = 4; c < GokeiColumn + 6; c++)
                                    {
                                        e.Row.Cells[c].Visible = false;
                                    }

                                    //20220519 maythu delete start
                                    //int colspan = column_count - 8 - GokeiColumn;
                                    //e.Row.Cells[GokeiColumn + 6].ColumnSpan = colspan;
                                    //for (int c = GokeiColumn + 7; c < column_count - 2; c++) 
                                    //{
                                    //    e.Row.Cells[c].Visible = false;
                                    //}
                                    //20220519 maythu delete end

                                    #region 20220519 maythu add start
                                    int colspan = column_count - 8 - GokeiColumn;
                                    int startcolspan = 0;
                                    int colDisplay = 0;
                                    if (GokeiColumn == 5)　//　商品コード 非表示
                                    {
                                        startcolspan = GokeiColumn + GokeiCol + 2;
                                        colDisplay = GokeiCol + 7;
                                    }
                                    else if (GokeiColumn == 6)　//　商品コード 表示
                                    {
                                        startcolspan = GokeiColumn + GokeiCol + 1;
                                        colDisplay = GokeiCol + 7;
                                    }
                                    e.Row.Cells[startcolspan].ColumnSpan = colspan;
                                    e.Row.Cells[startcolspan].Style.Add("text-align", "right");
                                    for (int c = GokeiColumn + 6; c < column_count - 2; c++)
                                    {
                                        if (c != colDisplay)
                                        {
                                            e.Row.Cells[c].Visible = false;
                                        }
                                    }
                                    #endregion 20220519 maythu add end
                                }
                                else if (GokeiColumn_Position == "No")
                                {
                                    e.Row.Cells[3].ColumnSpan = column_count - 6;
                                    txtSyokei.Visible = true;
                                    btnSyosai.Visible = false;
                                    //txtSyokei.Style.Add("min-width", BeforeSyoekiTextboxWidth + "px !important");
                                    for (int c = 4; c < column_count - 2; c++)
                                    {
                                        e.Row.Cells[c].Visible = false;
                                    }
                                }

                            }
                        }
                        catch { }
                    }
                    else
                    {
                        e.Row.BackColor = System.Drawing.Color.White;
                    }
                    lblKubun1.Text = lblKubun.Text;
                }
                #endregion

                try
                {
                    TextBox txt_gentanka = (GV_MitumoriSyohin_Original.Rows[e.Row.RowIndex].FindControl("txtnGENKATANKA") as TextBox);
                    (e.Row.FindControl("txtnGENKATANKA") as TextBox).Enabled = txt_gentanka.Enabled;
                }
                catch { }

                try
                {
                    TextBox txt_tanka = (GV_MitumoriSyohin_Original.Rows[e.Row.RowIndex].FindControl("txtnTANKA") as TextBox);
                    (e.Row.FindControl("txtnTANKA") as TextBox).Enabled = txt_tanka.Enabled;
                }
                catch { }

                DataTable dtGridTextbox= ViewState["dtGridTextbox"] as DataTable;
                foreach (DataRow dr in dtGridTextbox.Rows)
                {
                    (e.Row.FindControl(dr[0].ToString()) as TextBox).TabIndex = tabIndex;
                    tabIndex++;
                }

            }
        }

        #endregion

        #region 得意先詳細画面へ
        protected void BT_sTOKUISAKI_Syousai_Click(object sender, EventArgs e)
        {
            Session["fTokuisakiSyosai"] = "null";
            Session["cTokuisakiBukken"] = lblcTOKUISAKI.Text;
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

        #region BT_sTOKUISAKI_TAN_Cross_Click1

        protected void BT_sTOKUISAKI_TAN_Cross_Click1(object sender, EventArgs e)
        {
            lblsTOKUISAKI_TAN.Text = "";
            lblsTOKUISAKI_TAN_JUN.Text = "";
            divTokuisakiTanBtn.Style["display"] = "block";
            divTokuisakiTanLabel.Style["display"] = "none";
            divTokuisakiTanSyosai.Style["display"] = "none";
            updTokuisakiTantou.Update();

            updHeader.Update();
        }
        #endregion

        #region BT_sSEIKYUSAKI_TAN_Cross_Click

        protected void BT_sSEIKYUSAKI_TAN_Cross_Click(object sender, EventArgs e)
        {
            lblsSEIKYUSAKI_TAN.Text = "";
            lblsSEIKYUSAKI_TAN_JUN.Text = "";
            divSeikyusakiTanBtn.Style["display"] = "block";
            divSeikyusakiTanLabel.Style["display"] = "none";
            divSeikyusakiTanSyosai.Style["display"] = "none";
            updSeikyusakiTantou.Update();

            updHeader.Update();
        }
        #endregion

        #region 拠点を選択
        protected void btnKyotenAdd_Click(object sender, EventArgs e)
        {
            SessionUtility.SetSession("HOME", "Master");
            //ifSentakuPopup.Style["width"] = "470px";
            //ifSentakuPopup.Style["height"] = "620px";
            ifSentakuPopup.Style["width"] = "100vw";
            ifSentakuPopup.Style["height"] = "100vh";
            String id = Request.QueryString["id"];
            ifSentakuPopup.Src = "JC25KyotenList.aspx?id=" + id;
            mpeSentakuPopup.Show();

            lblsKYOTEN.Attributes.Add("onClick", "BtnClick('MainContent_btnKyotenAdd')");
            updSentakuPopup.Update();
        }
        #endregion

        #region 拠点を削除
        protected void BT_sKYOTENLIST_Cross_Click(object sender, EventArgs e)
        {
            lblsKYOTEN.Text = "";
            lblcKYOTEN.Text = "";
            divKyotenbtn.Style["display"] = "block";
            divKyotenLabel.Style["display"] = "none";
            // BT_Save.Enabled = true;
            updKyoten.Update();
            updHeader.Update();
        }
        #endregion

        #region btnPopupClose()  選択サブ画面を閉じる処理
        protected void btnClose_Click(object sender, EventArgs e)
        {
            ifSentakuPopup.Src = "";
            mpeSentakuPopup.Hide();
            updSentakuPopup.Update();
        }
        #endregion

        #region btnKyotenSelect_Click()  拠点サブ画面を閉じる時のフォーカス処理
        protected void btnKyotenSelect_Click(object sender, EventArgs e)
        {
            if (Session["cKyoten"] != null)
            {
                string cKyoten = (string)Session["cKyoten"];
                string sKyoten = (string)Session["sKyoten"];
                lblcKYOTEN.Text = cKyoten;
                lblsKYOTEN.Text = sKyoten.Replace("<", "&lt").Replace(">", "&gt");
                divKyotenbtn.Style["display"] = "none";
                divKyotenLabel.Style["display"] = "block";
                updKyoten.Update();

            }
            HF_isChange.Value = "1";
            ifSentakuPopup.Src = "";
            mpeSentakuPopup.Hide();
            updSentakuPopup.Update();
            updHeader.Update();
        }
        #endregion

        #region 支払方法を選択
        protected void btnShiharai_Click(object sender, EventArgs e)
        {
            SessionUtility.SetSession("HOME", "Master");
            ifSentakuPopup.Style["width"] = "470px";
            ifSentakuPopup.Style["height"] = "620px";

            String id = Request.QueryString["id"];
            ifSentakuPopup.Src = "JC25_Shiharaihouhou.aspx?id=" + id;
            mpeSentakuPopup.Show();
            lblsShihariai.Attributes.Add("onClick", "BtnClick('MainContent_btnShiharai')");
            updSentakuPopup.Update();
        }
        #endregion

        #region 支払方法を削除
        protected void btnShihariCross_Click(object sender, EventArgs e)
        {
            lblsShihariai.Text = "";
            lblcShiharai.Text = "";
            divShiharaibtn.Style["display"] = "block";
            divShiharaiLabel.Style["display"] = "none";
            updShiharai.Update();
            updHeader.Update();
        }
        #endregion

        #region btnshiarailistSelect_Click()  支払方法サブ画面を閉じる時のフォーカス処理
        protected void btnshiarailistSelect_Click(object sender, EventArgs e)
        {
            if (Session["cSHIHARAI"] != null)
            {
                string cShiharai = (string)Session["cSHIHARAI"];
                string sShiharai = (string)Session["sSHIHARAI"];
                lblcShiharai.Text = cShiharai;
                lblsShihariai.Text = sShiharai.Replace("<", "&lt").Replace(">", "&gt");
                divShiharaibtn.Style["display"] = "none";
                divShiharaiLabel.Style["display"] = "block";
                updShiharai.Update();
            }

            HF_isChange.Value = "1";
            ifSentakuPopup.Src = "";
            mpeSentakuPopup.Hide();
            updSentakuPopup.Update();
            updHeader.Update();
        }
        #endregion

        #region 有効期限を選択
        protected void btnYuukou_Click(object sender, EventArgs e)
        {
            SessionUtility.SetSession("HOME", "Master");
            ifSentakuPopup.Style["width"] = "470px";
            ifSentakuPopup.Style["height"] = "620px";

            String id = Request.QueryString["id"];
            ifSentakuPopup.Src = "JC25YukoKigenList.aspx?id=" + id;
            mpeSentakuPopup.Show();

            lblsYuukou.Attributes.Add("onClick", "BtnClick('MainContent_btnYuukou')");
            updSentakuPopup.Update();
        }
        #endregion

        #region 有効期限を削除
        protected void btnYuukouCross_Click(object sender, EventArgs e)
        {
            lblsYuukou.Text = "";
            lblcYuukou.Text = "";
            divYuuKoubtn.Style["display"] = "block";
            divYuukouLabel.Style["display"] = "none";
            // BT_Save.Enabled = true;
            updYuukou.Update();
            updHeader.Update();
        }
        #endregion

        #region btnYukoKigenListSelect_Click()  有効期限サブ画面を閉じる時のフォーカス処理
        protected void btnYukoKigenListSelect_Click(object sender, EventArgs e)
        {
            if (Session["cYuko"] != null)
            {
                string cYukou = (string)Session["cYuko"];
                string sYukou = (string)Session["sYuko"];
                lblcYuukou.Text = cYukou;
                lblsYuukou.Text = sYukou.Replace("<", "&lt").Replace(">", "&gt");
                divYuuKoubtn.Style["display"] = "none";
                divYuukouLabel.Style["display"] = "block";
                updYuukou.Update();
            }

            HF_isChange.Value = "1";
            ifSentakuPopup.Src = "";
            mpeSentakuPopup.Hide();
            updSentakuPopup.Update();
            updHeader.Update();
        }
        #endregion

        #region 得意先を選択
        protected void btnTokuisaki_Click(object sender, EventArgs e)
        {
            SessionUtility.SetSession("HOME", "Master");
            /*Session["flag"] = "mitsumori";*/ //added by YG

            //ifSentakuPopup.Style["width"] = "1160px";
            //ifSentakuPopup.Style["height"] = "650px";
            //ifSentakuPopup.Src = "JC18TokuisakiKensaku.aspx";
            //mpeSentakuPopup.Show();
            Session["ftokuisakisyosai"] = "0";

            String id = Request.QueryString["id"];
            ifShinkiPopup.Src = "JC18TokuisakiKensaku.aspx?id=" + id;
            mpeShinkiPopup.Show();

            lblsTOKUISAKI.Attributes.Add("onClick", "BtnClick('MainContent_btnTokuisaki')");
            lblfTokuiSaki.Text = "0";
            updShinkiPopup.Update();
            //updSentakuPopup.Update();
        }
        #endregion

        #region btnTokuisakiSelect_Click()  得意先サブ画面を閉じる時のフォーカス処理
        protected void btnTokuisakiSelect_Click(object sender, EventArgs e)
        {
            if (Session["cTOUKUISAKI"] != null)
            {
                if (lblfTokuiSaki.Text == "0")
                {
                    string ctokuisaki = (string)Session["cTOUKUISAKI"];
                    string stokuisaki = (string)Session["sTOUKUISAKI"];
                    lblcTOKUISAKI.Text = ctokuisaki;
                    lblsTOKUISAKI.Text = stokuisaki.Replace("<", "&lt").Replace(">", "&gt");

                    string tantouCarry = (string)Session["STANTOUCarry"];
                    if (tantouCarry != null)
                    {
                        lblsTOKUISAKI_TAN.Text = tantouCarry.Replace("<", "&lt").Replace(">", "&gt");
                        lblsTOKUISAKI_TAN.Attributes.Add("onClick", "BtnClick('MainContent_btnTokuisakiTantou')");
                        lblsTOKUISAKI_TAN_JUN.Text = "1";
                        divTokuisakiTanBtn.Style["display"] = "none";
                        divTokuisakiTanLabel.Style["display"] = "block";
                        divTokuisakiTanSyosai.Style["display"] = "block";
                        updTokuisakiTantou.Update();

                        ifSentakuPopup.Src = "";
                        mpeSentakuPopup.Hide();
                        updSentakuPopup.Update();
                        Session["STANTOUCarry"] = null;
                    }

                    String id = Request.QueryString["id"];
                    DataTable dt_db = JC01Login_Class.Get_DB(id);
                    String db = dt_db.Rows[0]["db"].ToString();
                    String mail = dt_db.Rows[0]["mail"].ToString();
                    ConstantVal.DB_NAME = db;

                    //20220429 MyatNoe
                    JC_ClientConnecction_Class jc = new JC_ClientConnecction_Class();
                    MySqlConnection cn = jc.GetConnection();
                    cn.Open();
                    string sql = "select" +
                        " ifnull(cSHIHARAI, '') as cSHIHARAI," +
                        " ifnull(sSHIHARAI, '') as sSHIHARAI," +
                        " ifnull(cSEIKYUSAKI, '') as cSEIKYUSAKI," +
                        " ifnull(sSEIKYUSAKI, '') as sSEIKYUSAKI" +
                        " from m_tokuisaki" +
                        " where cTOKUISAKI like '%" + ctokuisaki + "%'";
                    MySqlCommand cmd = new MySqlCommand(sql, cn);
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    DataTable dtable = new DataTable();
                    da.Fill(dtable);
                    cn.Close();
                    da.Dispose();
                    if (dtable.Rows.Count > 0)
                    {
                        if (!String.IsNullOrEmpty(dtable.Rows[0]["cSHIHARAI"].ToString()))
                        {
                            lblcShiharai.Text = dtable.Rows[0]["cSHIHARAI"].ToString();
                            lblsShihariai.Text = dtable.Rows[0]["sSHIHARAI"].ToString().Replace("<", "&lt").Replace(">", "&gt");
                            divShiharaibtn.Style["display"] = "none";
                            divShiharaiLabel.Style["display"] = "block";
                            updShiharai.Update();
                            lblsShihariai.Attributes.Add("onClick", "BtnClick('MainContent_btnShiharai')");
                            updSentakuPopup.Update();
                        }

                        if (!String.IsNullOrEmpty(dtable.Rows[0]["cSEIKYUSAKI"].ToString()))
                        {
                            lblcSEIKYUSAKI.Text = dtable.Rows[0]["cSEIKYUSAKI"].ToString();
                            lblsSEIKYUSAKI.Text = dtable.Rows[0]["sSEIKYUSAKI"].ToString().Replace("<", "&lt").Replace(">", "&gt");
                            divSEIKYUSAKIBtn.Style["display"] = "none";
                            divsSEIKYUSAKILabel.Style["display"] = "block";
                            divSEIKYUSAKISyosai.Style["display"] = "block";
                            btnSeikyusaki.BorderStyle = BorderStyle.None;
                            updSEIKYUSAKI.Update();
                            lblsSEIKYUSAKI.Attributes.Add("onClick", "BtnClick('MainContent_btnSeikyusaki')");
                            updSentakuPopup.Update();
                        }
                    }
                    //20220429 MyatNoe

                    divTokuisakiBtn.Style["display"] = "none";
                    divTokuisakiLabel.Style["display"] = "block";
                    divTokuisakiSyosai.Style["display"] = "block";
                    btnTokuisaki.BorderStyle = BorderStyle.None;
                    updTokuisaki.Update();

                    if (tantouCarry == null)
                    {
                        BT_sTOKUISAKI_TAN_Cross_Click1(sender, e);
                    }
                }
                else
                {
                    string ctokuisaki = (string)Session["cTOUKUISAKI"];
                    string stokuisaki = (string)Session["sTOUKUISAKI"];
                    lblcSEIKYUSAKI.Text = ctokuisaki;
                    lblsSEIKYUSAKI.Text = stokuisaki.Replace("<", "&lt").Replace(">", "&gt");

                    string tantouCarry = (string)Session["STANTOUCarry"];
                    if (tantouCarry != null)
                    {
                        lblsSEIKYUSAKI_TAN.Text = tantouCarry.Replace("<", "&lt").Replace(">", "&gt");
                        lblsSEIKYUSAKI_TAN.Attributes.Add("onClick", "BtnClick('MainContent_btnSeikyusakiTantou')");
                        lblsSEIKYUSAKI_TAN_JUN.Text = "1";
                        divSeikyusakiTanBtn.Style["display"] = "none";
                        divSeikyusakiTanLabel.Style["display"] = "block";
                        divSeikyusakiTanSyosai.Style["display"] = "block";
                        updSeikyusakiTantou.Update();

                        ifSentakuPopup.Src = "";
                        mpeSentakuPopup.Hide();
                        updSentakuPopup.Update();
                        Session["STANTOUCarry"] = null;
                    }

                    if (tantouCarry == null)
                    {
                        BT_sSEIKYUSAKI_TAN_Cross_Click(sender, e);
                    }

                    divSEIKYUSAKIBtn.Style["display"] = "none";
                    divsSEIKYUSAKILabel.Style["display"] = "block";
                    divSEIKYUSAKISyosai.Style["display"] = "block";
                    btnSeikyusaki.BorderStyle = BorderStyle.None;
                    updSEIKYUSAKI.Update();
                }
            }
            HF_isChange.Value = "1";
            //ifSentakuPopup.Src = "";
            //mpeSentakuPopup.Hide();
            //updSentakuPopup.Update();

            ifShinkiPopup.Src = "";
            mpeShinkiPopup.Hide();
            updShinkiPopup.Update();
            updHeader.Update();
        }
        #endregion

        #region 得意先担当者を選択
        protected void btnTokuisakiTantou_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(lblcTOKUISAKI.Text))
            {
                SessionUtility.SetSession("HOME", "Master");
                Session["cTokuisakiBukken"] = lblcTOKUISAKI.Text;
                /*Session["flag"] = "mitsumori";*/ //added by YG
                                                   //ifSentakuPopup.Style["width"] = "1100px";
                                                   //ifSentakuPopup.Style["height"] = "650px";
                                                   //ifSentakuPopup.Src = "JC20TokuisakiTantouKensaku.aspx";
                                                   //mpeSentakuPopup.Show();

                String id = Request.QueryString["id"];
                ifShinkiPopup.Src = "JC20TokuisakiTantouKensaku.aspx?id=" + id;
                mpeShinkiPopup.Show();

                lblsTOKUISAKI_TAN.Attributes.Add("onClick", "BtnClick('MainContent_btnTokuisakiTantou')");
                lblfTokuiSaki.Text = "0";
                //updSentakuPopup.Update();
                updShinkiPopup.Update();
            }
        }
        #endregion

        #region 得意先担当者詳細
        protected void BT_sTOKUISAKI_TAN_Syousai_Click(object sender, EventArgs e)
        {
            Session["fTokuisakiSyosai"] = "null";
            Session["cTokuisakiBukken"] = lblcTOKUISAKI.Text;
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

        #region 請求先担当者詳細
        protected void BT_sSEIKYUSAKI_TAN_Syousai_Click(object sender, EventArgs e)
        {
            Session["fTokuisakiSyosai"] = "null";
            Session["cTokuisakiBukken"] = lblcSEIKYUSAKI.Text;
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

        #region btnTokuisakiTantouSelect_Click()  得意先担当者サブ画面を閉じる時のフォーカス処理
        protected void btnTokuisakiTantouSelect_Click(object sender, EventArgs e)
        {
            if (Session["TOUKUISAKITANTOU"] != null)
            {
                if (lblfTokuiSaki.Text == "0")
                {
                    string tokuisakitantou = (string)Session["TOUKUISAKITANTOU"];
                    string njun = (string)Session["TokuisakiTanJun"];
                    lblsTOKUISAKI_TAN.Text = tokuisakitantou.Replace("<", "&lt").Replace(">", "&gt");
                    lblsTOKUISAKI_TAN_JUN.Text = njun;
                    divTokuisakiTanBtn.Style["display"] = "none";
                    divTokuisakiTanLabel.Style["display"] = "block";
                    divTokuisakiTanSyosai.Style["display"] = "block";
                    updTokuisakiTantou.Update();
                }
                else
                {
                    string tokuisakitantou = (string)Session["TOUKUISAKITANTOU"];
                    string njun = (string)Session["TokuisakiTanJun"];
                    lblsSEIKYUSAKI_TAN.Text = tokuisakitantou.Replace("<", "&lt").Replace(">", "&gt");
                    lblsSEIKYUSAKI_TAN_JUN.Text = njun;
                    divSeikyusakiTanBtn.Style["display"] = "none";
                    divSeikyusakiTanLabel.Style["display"] = "block";
                    divSeikyusakiTanSyosai.Style["display"] = "block";
                    updSeikyusakiTantou.Update();
                }
                //BT_Save.Enabled = true;
                //HF_isChange.Value = "1";
                //updHeader.Update();
            }
            HF_isChange.Value = "1";
            //ifSentakuPopup.Src = "";
            //mpeSentakuPopup.Hide();
            //updSentakuPopup.Update();

            ifShinkiPopup.Src = "";
            mpeShinkiPopup.Hide();
            updShinkiPopup.Update();
            updHeader.Update();
        }
        #endregion

        #region 請求先を選択
        protected void btnSeikyusaki_Click(object sender, EventArgs e)
        {
            SessionUtility.SetSession("HOME", "Master");
            /* Session["flag"] = "mitsumori";*/ //added by YG
            //ifSentakuPopup.Style["width"] = "1160px";
            //ifSentakuPopup.Style["height"] = "650px";
            //ifSentakuPopup.Src = "JC18TokuisakiKensaku.aspx";
            //mpeSentakuPopup.Show();
            Session["ftokuisakisyosai"] = "0";

            String id = Request.QueryString["id"];
            ifShinkiPopup.Src = "JC18TokuisakiKensaku.aspx?id=" + id;
            mpeShinkiPopup.Show();

            lblsSEIKYUSAKI.Attributes.Add("onClick", "BtnClick('MainContent_btnSeikyusaki')");
            lblfTokuiSaki.Text = "1";
            //updSentakuPopup.Update();
            updShinkiPopup.Update();
        }
        #endregion

        #region 請求先詳細
        protected void BT_sSEIKYUSAKI_Syousai_Click(object sender, EventArgs e)
        {
            Session["fTokuisakiSyosai"] = "null";
            Session["cTokuisakiBukken"] = lblcSEIKYUSAKI.Text;
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

        #region 請求先担当者を選択
        protected void btnSeikyusakiTantou_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(lblcSEIKYUSAKI.Text))
            {
                SessionUtility.SetSession("HOME", "Master");
                Session["cTokuisakiBukken"] = lblcSEIKYUSAKI.Text;
                /*Session["flag"] = "mitsumori";*/ //added by YG
                                                   //ifSentakuPopup.Style["width"] = "1100px";
                                                   //ifSentakuPopup.Style["height"] = "650px";
                                                   //ifSentakuPopup.Src = "JC20TokuisakiTantouKensaku.aspx";
                                                   //mpeSentakuPopup.Show();
                String id = Request.QueryString["id"];
                ifShinkiPopup.Src = "JC20TokuisakiTantouKensaku.aspx?id=" + id;
                mpeShinkiPopup.Show();

                lblsSEIKYUSAKI_TAN.Attributes.Add("onClick", "BtnClick('MainContent_btnSeikyusakiTantou')");
                lblfTokuiSaki.Text = "1";
                //updSentakuPopup.Update();
                updShinkiPopup.Update();
            }
        }
        #endregion

        #region txtcSYOHIN_TextChanged  
        protected void txtcSYOHIN_TextChanged(object sender, EventArgs e)
        {
            var txt_cSYOUHIN = sender as TextBox;
            bool isNumber = Double.TryParse(txt_cSYOUHIN.Text, out Double numericValue);
            if (!isNumber)
            {
                txt_cSYOUHIN.Text = "";
            }

            GridViewRow gvr = (GridViewRow)txt_cSYOUHIN.NamingContainer;
            int rowindex = gvr.RowIndex;
            (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("txtcSYOHIN") as TextBox).Text = txt_cSYOUHIN.Text;
            string cSYOHIN = (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("txtcSYOHIN") as TextBox).Text;

            int rowNo = Convert.ToInt32((gvr.FindControl("lblRowNo") as Label).Text);
            DataTable dt_SyohinOriginal = GetGridViewData();
            if (rowNo == 0)
            {
                //DataTable dt_Meisai = GetGridViewData();
                var max = dt_SyohinOriginal.AsEnumerable().Max(x => int.Parse(x.Field<string>("rowNo")));
                max += 1;
                HF_maxRowNo.Value = max.ToString();
                (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblRowNo") as Label).Text = max.ToString();
                updMitsumoriSyohinGrid.Update();
            }


            DataTable dt_Syosai = GetSyosaiGridViewData();

            Boolean hasSyousai = false;
            Boolean IstextboxEnable = true;
            if (cSYOHIN != "")
            {
                cSYOHIN = cSYOHIN.ToString().PadLeft(10, '0');
                (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("txtcSYOHIN") as TextBox).Text = cSYOHIN;
                (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("txtcSYOHIN") as TextBox).ToolTip = cSYOHIN;

                String id = Request.QueryString["id"];
                DataTable dt_db = JC01Login_Class.Get_DB(id);
                String db = dt_db.Rows[0]["db"].ToString();
                String mail = dt_db.Rows[0]["mail"].ToString();
                ConstantVal.DB_NAME = db;

                JC_ClientConnecction_Class jc = new JC_ClientConnecction_Class();
                jc.loginId = mail;
                MySqlConnection cn = jc.GetConnection();
                cn.Open();
                string sql_syouhin = " ";
                sql_syouhin = " Select " +
                              " ms.cSYOUHIN as cSYOUHIN," +
                              " IfNull(ms.sSYOUHIN, '') as sSYOUHIN," +
                              " IfNull(ms.sSHIYOU, '') as sSHIYOU, " +
                              " IfNull(ms.sTANI,'') as sTANI, " +
                              " format(ifnull(ms.nHANNBAIKAKAKU, 0),2) as nHANNBAIKAKAKU," +
                              " Format(Round((ifnull(ms.nHANNBAIKAKAKU, 0))*(ifnull(ms.nSYOUKISU, 1)),0),0) as TankaGokei," +
                              " Format(ifnull(ms.nSHIIREKAKAKU, 0),2) as nSHIIREKAKAKU," +
                              " Format(Round((ifnull(ms.nSHIIREKAKAKU, 0))*(ifnull(ms.nSYOUKISU, 1)),0),0) as GenkaGokei," +
                              " format(Round((ifnull(ms.nHANNBAIKAKAKU, 0))*(ifnull(ms.nSYOUKISU, 1)),0)-Round((ifnull(ms.nSHIIREKAKAKU, 0))*(ifnull(ms.nSYOUKISU, 1)),0),0) as nARARI," +
                              " CONCAT(FORMAT(IfNull((Round((ifnull(ms.nHANNBAIKAKAKU, 0))*(ifnull(ms.nSYOUKISU, 1)),0)-Round((ifnull(ms.nSHIIREKAKAKU, 0))*(ifnull(ms.nSYOUKISU, 1)),0))/(Round((ifnull(ms.nHANNBAIKAKAKU, 0))*(ifnull(ms.nSYOUKISU, 1)),0)),0)*100, 1),'%') As nARARISu," +
                              " ifnull(ms.nSYOUKISU, 1) as nSYOUKISU," +
                              " MSD.sSYOUHIN_DAIGRP as sSYOUHIN_DAIGRP," +
                              " MST.sSYOUHIN_TYUUGRP as sSYOUHIN_TYUUGRP," +
                              " ifnull(ms.sBIKOU, '') as sBIKOU," +
                              " ifnull(ms.fJITAIS, '0') as fJITAIS" +
                              " From m_syouhin ms " +
                              " left join" +
                              " M_SYOUHIN_DAIGRP MSD ON MSD.cSYOUHIN_DAIGRP = ms.cSYOUHIN_DAIGRP" +
                              " left join " +
                              " M_SYOUHIN_TYUUGRP MST ON ms.cSYOUHIN_TYUUGRP = MST.cSYOUHIN_TYUUGRP" +
                              " Where (ms.fHAIBAN <> '1' or ms.fHAIBAN is null) and '1' = '1' and ms.cSYOUHIN like '%" + cSYOHIN + "%'; ";


                MySqlCommand cmd = new MySqlCommand(sql_syouhin, cn);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dtable = new DataTable();
                da.Fill(dtable);
                cn.Close();
                da.Dispose();

                if (dtable.Rows.Count > 0)
                {
                    (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("txtsSYOHIN") as TextBox).Text = dtable.Rows[0]["sSYOUHIN"].ToString() + "　" + dtable.Rows[0]["sSHIYOU"].ToString();
                    (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("txtsSYOHIN") as TextBox).ToolTip = dtable.Rows[0]["sSYOUHIN"].ToString() + "　" + dtable.Rows[0]["sSHIYOU"].ToString();
                    (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblfjitais") as Label).Text = dtable.Rows[0]["fJITAIS"].ToString();

                    double nSuryo = 0;
                    if (!String.IsNullOrEmpty(dtable.Rows[0]["nSYOUKISU"].ToString()))
                    {
                        nSuryo = Convert.ToDouble(dtable.Rows[0]["nSYOUKISU"].ToString());
                    }

                    (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("txtnSURYO") as TextBox).Text = nSuryo.ToString("#,##0.##");
                    (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("txtnSURYO") as TextBox).ToolTip = nSuryo.ToString("#,##0.##");

                    (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblcTANI") as Label).Text = dtable.Rows[0]["sTANI"].ToString();
                    (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblcTANI") as Label).ToolTip = dtable.Rows[0]["sTANI"].ToString();
                    (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("txtTani") as TextBox).Text = dtable.Rows[0]["sTANI"].ToString();
                    string getsTANI = " select distinct cTANI, sTANI from M_TANI order by cTANI ";
                    MySqlCommand cmd1 = new MySqlCommand(getsTANI, cn);
                    MySqlDataAdapter da1 = new MySqlDataAdapter(cmd1);
                    DataTable dt = new DataTable();
                    da1.Fill(dt);
                    cn.Close();
                    da1.Dispose();
                    DropDownList DropDownList1 = (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("DDL_cTANI") as DropDownList);
                    DropDownList1.DataSource = dt;
                    DropDownList1.DataTextField = "sTANI";
                    DropDownList1.DataValueField = "sTANI";
                    DropDownList1.DataBind();
                    string lblcTANI = (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblcTANI") as Label).Text;
                    DropDownList1.Items.Insert(0, new ListItem(" ", "00"));
                    try
                    {
                        DropDownList1.Items.FindByText(lblcTANI).Selected = true;
                    }
                    catch { }

                    Double nkakeritsu = Convert.ToDouble((GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("txtnRITU") as TextBox).Text.Replace("%", ""));
                    double nHanbaikakaku = 0;
                    Double nTankaGokei = 0;
                    Double nSikiriTanka = 0;
                    if (String.IsNullOrEmpty((GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("txtnTANKA") as TextBox).Text) || (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("txtnTANKA") as TextBox).Text == "0")
                    {
                        if (!String.IsNullOrEmpty(dtable.Rows[0]["nHANNBAIKAKAKU"].ToString()))
                        {
                            nHanbaikakaku = Convert.ToDouble(dtable.Rows[0]["nHANNBAIKAKAKU"].ToString());
                            nTankaGokei = ((nHanbaikakaku / 100) * nkakeritsu) * nSuryo;
                            nSikiriTanka = (nHanbaikakaku / 100) * nkakeritsu;
                        }
                    }
                    else
                    {
                        nHanbaikakaku = Convert.ToDouble((GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("txtnTANKA") as TextBox).Text);
                        nTankaGokei = ((nHanbaikakaku / 100) * nkakeritsu) * nSuryo;
                        nSikiriTanka = (nHanbaikakaku / 100) * nkakeritsu;
                    }

                    string sql_syouhin_m = " ";
                    sql_syouhin_m = " Select " +
                        " msm.cSYOUHIN_m as cSYOUHIN," +
                        " IfNull(ms.sSYOUHIN, '') as sSYOUHIN," +
                        " IfNull(ms.sSHIYOU, '') as sSHIYOU," +
                        " IfNull(ms.sTANI, '') as sTANI," +
                        " format(ifnull(ms.nHANNBAIKAKAKU, 0), 2) as nHANNBAIKAKAKU," +
                        " Format(Round((ifnull(ms.nHANNBAIKAKAKU, 0)) * (ifnull(ms.nSYOUKISU, 1)), 0), 0) as TankaGokei," +
                        " Format(ifnull(ms.nSHIIREKAKAKU, 0), 2) as nSHIIREKAKAKU," +
                        " Format(Round((ifnull(ms.nSHIIREKAKAKU, 0)) * (ifnull(ms.nSYOUKISU, 1)), 0), 0) as GenkaGokei," +
                        " format(Round((ifnull(ms.nHANNBAIKAKAKU, 0)) * (ifnull(ms.nSYOUKISU, 1)), 0) " +
                        "- Round((ifnull(ms.nSHIIREKAKAKU, 0)) * (ifnull(ms.nSYOUKISU, 1)), 0), 0) as nARARI," +
                        " CONCAT(FORMAT(IfNull((Round((ifnull(ms.nHANNBAIKAKAKU, 0)) * (ifnull(ms.nSYOUKISU, 1)), 0)" +
                        " - Round((ifnull(ms.nSHIIREKAKAKU, 0)) * (ifnull(ms.nSYOUKISU, 1)), 0)) " +
                        "/ (Round((ifnull(ms.nHANNBAIKAKAKU, 0)) * (ifnull(ms.nSYOUKISU, 1)), 0)), 0) * 100, 1),'%') As nARARISu," +
                        " ifnull(ms.nSYOUKISU, 1) as nSYOUKISU," +
                        " ifnull(ms.sBIKOU, '') as sBIKOU," +
                        " ifnull(ms.fJITAIS, '0') as fJITAIS" +
                        " From m_syouhin ms" +
                        " right join m_syouhin_m msm ON ms.cSYOUHIN = msm.cSYOUHIN_m" +
                        " Where msm.cSYOUHIN like '%" + cSYOHIN + "%' order by msm.nJUNBAN; ";
                    cn.Open();
                    cmd = new MySqlCommand(sql_syouhin_m, cn);
                    da = new MySqlDataAdapter(cmd);
                    DataTable dtable_syohin_m = new DataTable();
                    da.Fill(dtable_syohin_m);
                    cn.Close();
                    da.Dispose();

                    double nSHIIREKAKAKU = 0;
                    double nGenkaGokei = 0;
                   
                    if (dtable_syohin_m.Rows.Count == 0)
                    {
                        if ((GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("btnSyohinShosai") as Button).Text != "詳")
                        {
                            DataRow[] rows = dt_Syosai.Select("rowNo = '" + (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblRowNo") as Label).Text + "'");
                            if (rows.Length > 0)
                            {
                                foreach (var drow in rows)
                                {
                                    Double genka = 0;
                                    if (!String.IsNullOrEmpty(drow[8].ToString()))
                                    {
                                        genka = Convert.ToDouble(drow[8].ToString());
                                    }
                                    nGenkaGokei += genka;
                                }
                                hasSyousai = true;
                            }

                            if (nGenkaGokei != 0)
                            {
                                IstextboxEnable = false;
                                if ((GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblfgenkatanka") as Label).Text == "0")
                                {
                                    //nSHIIREKAKAKU = Convert.ToDouble(dtable.Rows[0]["nSHIIREKAKAKU"].ToString());
                                    nSHIIREKAKAKU = nGenkaGokei;
                                    nGenkaGokei = nSHIIREKAKAKU * nSuryo;
                                }
                                else
                                {
                                    if (nSuryo == 0)
                                    {
                                        nSHIIREKAKAKU = 0;
                                    }
                                    else
                                    {
                                        nSHIIREKAKAKU = nGenkaGokei / nSuryo;
                                    }
                                }
                            }
                            else
                            {
                                IstextboxEnable = true;
                                nSHIIREKAKAKU = 0;
                                nGenkaGokei = 0;
                            }

                        }
                        else
                        {
                            if (String.IsNullOrEmpty((GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("txtnGENKATANKA") as TextBox).Text) || (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("txtnGENKATANKA") as TextBox).Text == "0")
                            {
                                if (!String.IsNullOrEmpty(dtable.Rows[0]["nSHIIREKAKAKU"].ToString()))
                                {
                                    nSHIIREKAKAKU = Convert.ToDouble(dtable.Rows[0]["nSHIIREKAKAKU"].ToString());
                                    nGenkaGokei = nSHIIREKAKAKU * nSuryo;
                                }
                            }
                            else
                            {
                                nSHIIREKAKAKU = Convert.ToDouble((GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("txtnGENKATANKA") as TextBox).Text);
                                nGenkaGokei = ((nHanbaikakaku / 100) * nkakeritsu) * nSuryo;
                            }
                        }
                    }
                    else
                    {
                        hasSyousai = true;
                        DataRow[] rows = dt_Syosai.Select("rowNo = '" + (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblRowNo") as Label).Text + "'");
                        if (rows.Length > 0)
                        {
                            foreach (var drow in rows)
                            {
                                drow.Delete();
                            }

                            dt_Syosai.DefaultView.Sort = "rowNo asc";
                            dt_Syosai.AcceptChanges();
                        }

                        (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("btnSyohinShosai") as Button).Text = dtable_syohin_m.Rows.Count.ToString();

                        for (int i = 0; i < dtable_syohin_m.Rows.Count; i++)
                        {
                            DataRow dr = dt_Syosai.NewRow();
                            dr[0] = "0";
                            dr[1] = dtable_syohin_m.Rows[i]["cSYOUHIN"].ToString();
                            dr[2] = dtable_syohin_m.Rows[i]["sSYOUHIN"].ToString();
                            Double nsuryo = Convert.ToDouble(dtable_syohin_m.Rows[i]["nSYOUKISU"].ToString());
                            dr[3] = nsuryo.ToString("#,##0.##");
                            dr[4] = dtable_syohin_m.Rows[i]["sTANI"].ToString();
                            dr[5] = "0";
                            dr[6] = "0";
                            dr[7] = dtable_syohin_m.Rows[i]["nSHIIREKAKAKU"].ToString();
                            dr[8] = dtable_syohin_m.Rows[i]["GenkaGokei"].ToString();
                            Double tankaGokei = 0;
                            Double genkaGokei = Convert.ToDouble(dtable_syohin_m.Rows[i]["genkaGokei"].ToString());
                            Double arari = tankaGokei - genkaGokei;
                            dr[9] = arari;
                            double nArariSu_m = (arari / tankaGokei) * 100;
                            if (tankaGokei == 0)
                            {
                                nArariSu_m = 0;
                            }
                            dr[10] = nArariSu_m.ToString("###0.0") + "%";
                            dr[11] = (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblRowNo") as Label).Text;
                            dr[12] = nkakeritsu + "%";
                            dr[13] = "0";
                            dr[14] = dtable_syohin_m.Rows[i]["fJITAIS"].ToString();
                            dt_Syosai.Rows.Add(dr);

                            nGenkaGokei += genkaGokei;
                        }

                        dt_Syosai.DefaultView.Sort = "rowNo asc";
                        dt_Syosai.AcceptChanges();

                        GV_Syosai.DataSource = dt_Syosai;
                        GV_Syosai.DataBind();

                        if (nGenkaGokei != 0)
                        {
                            IstextboxEnable = false;
                            if ((GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblfgenkatanka") as Label).Text == "0")
                            {
                                nSHIIREKAKAKU = Convert.ToDouble(dtable.Rows[0]["nSHIIREKAKAKU"].ToString());
                                nGenkaGokei = nSHIIREKAKAKU * nSuryo;
                            }
                            else
                            {
                                if (nSuryo == 0)
                                {
                                    nSHIIREKAKAKU = 0;
                                }
                                else
                                {
                                    nSHIIREKAKAKU = nGenkaGokei / nSuryo;
                                }
                            }
                        }
                        else
                        {
                            IstextboxEnable = true;
                            nSHIIREKAKAKU = 0;
                            nGenkaGokei = 0;
                        }
                    }

                    Double nArari = nTankaGokei - nGenkaGokei;

                    double nArariSu = (nArari / nTankaGokei) * 100;
                    if (nTankaGokei == 0)
                    {
                        nArariSu = 0;
                    }



                    (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("txtnTANKA") as TextBox).Text = nHanbaikakaku.ToString("#,##0.##");
                    (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblTankaGokei") as Label).Text = nTankaGokei.ToString("#,##0");
                    (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblTanka") as Label).Text = nSikiriTanka.ToString("#,##0");
                    (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("txtnGENKATANKA") as TextBox).Text = nSHIIREKAKAKU.ToString("#,##0.##");
                    (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblGenkaGokei") as Label).Text = nGenkaGokei.ToString("#,##0");
                    (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblnARARI") as Label).Text = nArari.ToString("#,##0");
                    (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblnARARISu") as Label).Text = nArariSu.ToString("###0.0") + "%";

                    (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("txtnTANKA") as TextBox).ToolTip = nHanbaikakaku.ToString("#,##0.##");
                    (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblTankaGokei") as Label).ToolTip = nTankaGokei.ToString("#,##0");
                    (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblTanka") as Label).ToolTip = nSikiriTanka.ToString("#,##0");
                    (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("txtnGENKATANKA") as TextBox).ToolTip = nSHIIREKAKAKU.ToString("#,##0.##");
                    (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblGenkaGokei") as Label).ToolTip = nGenkaGokei.ToString("#,##0");
                    (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblnARARI") as Label).ToolTip = nArari.ToString("#,##0");
                    (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblnARARISu") as Label).ToolTip = nArariSu.ToString("###0.0") + "%";

                    if (String.IsNullOrEmpty((GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblfgenkatanka") as Label).Text))
                    {
                        (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblfgenkatanka") as Label).Text = "1";
                    }

                }
                else
                {
                    (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("txtcSYOHIN") as TextBox).Text = "";
                    (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("txtcSYOHIN") as TextBox).ToolTip = "";

                }
            }

            #region updateDatatable
            Label lbl_status = (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblhdnStatus") as Label);
            Label lbl_fgenkataka = (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblfgenkatanka") as Label);
            Label lbl_rowNo = (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblRowNo") as Label);
            TextBox txt_cSyohin = (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("txtcSYOHIN") as TextBox);
            TextBox txt_sSyohin = (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("txtsSYOHIN") as TextBox);
            TextBox txt_nSyoryo = (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("txtnSURYO") as TextBox);
            //DropDownList ddl_cTani = (row.FindControl("DDL_cTANI") as DropDownList);
            TextBox txt_cTani = (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("txtTani") as TextBox);
            TextBox txt_nTanka = (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("txtnTANKA") as TextBox);
            Label lbl_TankaGokei = (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblTankaGokei") as Label);
            TextBox txt_nGenkaTanka = (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("txtnGENKATANKA") as TextBox);
            Label lbl_GenkaGokei = (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblGenkaGokei") as Label);
            Label lbl_Arari = (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblnARARI") as Label);
            Label lbl_ArariSu = (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblnARARISu") as Label);
            TextBox txt_nRITU = (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("txtnRITU") as TextBox);
            Label lbl_kubun = (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblKubun") as Label);
            Label lbl_nSIKIRITANKA = (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblTanka") as Label);
            Label lbl_fJITAIS = (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblfjitais") as Label);
            Label lbl_fJITAIQ = (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblfjitaiq") as Label);

            dt_SyohinOriginal.Rows[rowindex][0] = lbl_status.Text;
            dt_SyohinOriginal.Rows[rowindex][1] = txt_cSyohin.Text;
            dt_SyohinOriginal.Rows[rowindex][2] = txt_sSyohin.Text;
            dt_SyohinOriginal.Rows[rowindex][3] = txt_nSyoryo.Text;
            dt_SyohinOriginal.Rows[rowindex][4] = txt_cTani.Text;
            dt_SyohinOriginal.Rows[rowindex][5] = txt_nTanka.Text;
            dt_SyohinOriginal.Rows[rowindex][6] = lbl_TankaGokei.Text;
            dt_SyohinOriginal.Rows[rowindex][7] = txt_nGenkaTanka.Text;
            dt_SyohinOriginal.Rows[rowindex][8] = lbl_GenkaGokei.Text;
            dt_SyohinOriginal.Rows[rowindex][9] = lbl_Arari.Text;
            dt_SyohinOriginal.Rows[rowindex][10] = lbl_ArariSu.Text;
            dt_SyohinOriginal.Rows[rowindex][11] = lbl_fgenkataka.Text;
            dt_SyohinOriginal.Rows[rowindex][12] = lbl_rowNo.Text;
            dt_SyohinOriginal.Rows[rowindex][13] = txt_nRITU.Text;
            dt_SyohinOriginal.Rows[rowindex][14] = lbl_kubun.Text;
            dt_SyohinOriginal.Rows[rowindex][15] = lbl_nSIKIRITANKA.Text;
            dt_SyohinOriginal.Rows[rowindex][16] = lbl_fJITAIS.Text;
            dt_SyohinOriginal.Rows[rowindex][17] = lbl_fJITAIQ.Text;

            dt_SyohinOriginal.AcceptChanges();
            #endregion

            dt_SyohinOriginal = SetMidashiSyokei(dt_SyohinOriginal);
            ViewState["SyouhinTable"] = dt_SyohinOriginal;
            GV_MitumoriSyohin_Original.DataSource = dt_SyohinOriginal;
            GV_MitumoriSyohin_Original.DataBind();
            HasCheckRow();
            setSyosaiCount(dt_SyohinOriginal, dt_Syosai);
            (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("txtnGENKATANKA") as TextBox).Enabled = IstextboxEnable;
            updMitsumoriSyohinGrid.Update();

            GV_MitumoriSyohin.DataSource = dt_SyohinOriginal;
            GV_MitumoriSyohin.DataBind();
            //SetSyosai();
            HF_isChange.Value = "1";
            GetTotalKingaku();

            //if (hasSyousai)
            //{
            //    HF_fTankaChange.Value = HF_fTankaChange.Value.Replace(rowNo.ToString(), "");
            //    HF_fGenkaChange.Value = HF_fGenkaChange.Value.Replace(rowNo.ToString(), "");
            //}
            updMitsumoriSyohinGrid.Update();
            updHeader.Update();

        }
        #endregion

        #region txtnSURYO_TextChanged  
        protected void txtnSURYO_TextChanged(object sender, EventArgs e)
        {
            var txt_nSURYO = sender as TextBox;
            bool isNumber = Double.TryParse(txt_nSURYO.Text, out Double numericValue);
            if (!isNumber)
            {
                txt_nSURYO.Text = "";
            }

            if (txt_nSURYO.Text == "")
            {
                txt_nSURYO.Text = "0";
            }

            DataTable dt_Syosai = GetSyosaiGridViewData();

            GridViewRow gvr = (GridViewRow)txt_nSURYO.NamingContainer;
            int rowindex = gvr.RowIndex;
            (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("txtnSURYO") as TextBox).Text = txt_nSURYO.Text;
            double nSURYO = Convert.ToDouble(txt_nSURYO.Text.Replace(",", ""));
            double TankaGokei = 0;
            double nSikiriTanka = 0;
            Double nkakeritsu = Convert.ToDouble((GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("txtnRITU") as TextBox).Text.Replace("%", ""));
            if (!String.IsNullOrEmpty((GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("txtnTANKA") as TextBox).Text))
            {
                double nTANKA = Double.Parse((GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("txtnTANKA") as TextBox).Text.Replace(",", ""));
                TankaGokei = Math.Round(nSURYO * ((nTANKA / 100) * nkakeritsu), 0);
                nSikiriTanka= Math.Round((nTANKA / 100) * nkakeritsu, 0);
                (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblTankaGokei") as Label).Text = TankaGokei.ToString("#,##0");
                (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblTankaGokei") as Label).ToolTip = TankaGokei.ToString("#,##0");
                (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblTanka") as Label).Text = nSikiriTanka.ToString("#,##0");
                (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblTanka") as Label).ToolTip = nSikiriTanka.ToString("#,##0");
            }
            double GenkaGokei = 0;
            if (!String.IsNullOrEmpty((GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("txtnGENKATANKA") as TextBox).Text))
            {
                double nGenka = Double.Parse((GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("txtnGENKATANKA") as TextBox).Text.Replace(",", ""));
                if ((GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblfgenkatanka") as Label).Text == "0")
                {
                    GenkaGokei = Math.Round(nSURYO * nGenka, 0);
                }
                else
                {
                    DataRow[] rows = dt_Syosai.Select("rowNo = '" + (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblRowNo") as Label).Text + "'");
                    if (rows.Length > 0)
                    {
                        foreach (var drow in rows)
                        {
                            Double genka = 0;
                            if (!String.IsNullOrEmpty(drow[8].ToString()))
                            {
                                genka = Convert.ToDouble(drow[8].ToString());
                            }
                            GenkaGokei += genka;
                        }

                        if (nSURYO == 0)
                        {
                            nGenka = 0;
                            GenkaGokei = 0;
                        }
                        else
                        {
                            nGenka = GenkaGokei / nSURYO;
                        }
                    }
                    else
                    {
                        GenkaGokei = Math.Round(nSURYO * nGenka, 0);
                    }
                }

                //GenkaGokei = Math.Round(nSURYO * nGenka, 0);
                 (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("txtnGENKATANKA") as TextBox).Text = nGenka.ToString("#,##0.##");
                (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("txtnGENKATANKA") as TextBox).ToolTip = nGenka.ToString("#,##0.##");
                (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblGenkaGokei") as Label).Text = GenkaGokei.ToString("#,##0");
                (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblGenkaGokei") as Label).ToolTip = GenkaGokei.ToString("#,##0");
            }

            if (!String.IsNullOrEmpty((GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("txtnTANKA") as TextBox).Text) && !String.IsNullOrEmpty((GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("txtnGENKATANKA") as TextBox).Text))
            {
                double nArari = TankaGokei - GenkaGokei;
                double nArariSu = (nArari / TankaGokei) * 100;
                if (TankaGokei == 0)
                {
                    nArariSu = 0;
                }
                (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblnARARI") as Label).Text = nArari.ToString("#,##0");
                (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblnARARI") as Label).ToolTip = nArari.ToString("#,##0");
                (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblnARARISu") as Label).Text = nArariSu.ToString("###0.0") + "%";
                (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblnARARISu") as Label).ToolTip = nArariSu.ToString("###0.0") + "%";
            }
            (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("txtnSURYO") as TextBox).Text = nSURYO.ToString("#,##0.##");
            (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("txtnSURYO") as TextBox).ToolTip = nSURYO.ToString("#,##0.##");
            //updMitsumoriSyohinGrid.Update();

            int rowNo = Convert.ToInt32((gvr.FindControl("lblRowNo") as Label).Text);

            DataTable dt_SyohinOriginal = GetGridViewData();
            if (rowNo == 0)
            {
                var max = dt_SyohinOriginal.AsEnumerable().Max(x => int.Parse(x.Field<string>("rowNo")));
                max += 1;
                HF_maxRowNo.Value = max.ToString();
                (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblRowNo") as Label).Text = max.ToString();
                updMitsumoriSyohinGrid.Update();
            }


            #region updateDatatable
            Label lbl_status = (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblhdnStatus") as Label);
            Label lbl_fgenkataka = (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblfgenkatanka") as Label);
            Label lbl_rowNo = (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblRowNo") as Label);
            TextBox txt_cSyohin = (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("txtcSYOHIN") as TextBox);
            TextBox txt_sSyohin = (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("txtsSYOHIN") as TextBox);
            TextBox txt_nSyoryo = (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("txtnSURYO") as TextBox);
            //DropDownList ddl_cTani = (row.FindControl("DDL_cTANI") as DropDownList);
            TextBox txt_cTani = (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("txtTani") as TextBox);
            TextBox txt_nTanka = (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("txtnTANKA") as TextBox);
            Label lbl_TankaGokei = (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblTankaGokei") as Label);
            TextBox txt_nGenkaTanka = (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("txtnGENKATANKA") as TextBox);
            Label lbl_GenkaGokei = (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblGenkaGokei") as Label);
            Label lbl_Arari = (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblnARARI") as Label);
            Label lbl_ArariSu = (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblnARARISu") as Label);
            TextBox txt_nRITU = (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("txtnRITU") as TextBox);
            Label lbl_kubun = (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblKubun") as Label);
            Label lbl_nSIKIRITANKA = (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblTanka") as Label);
            Label lbl_fJITAIS = (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblfjitais") as Label);
            Label lbl_fJITAIQ = (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblfjitaiq") as Label);

            dt_SyohinOriginal.Rows[rowindex][0] = lbl_status.Text;
            dt_SyohinOriginal.Rows[rowindex][1] = txt_cSyohin.Text;
            dt_SyohinOriginal.Rows[rowindex][2] = txt_sSyohin.Text;
            dt_SyohinOriginal.Rows[rowindex][3] = txt_nSyoryo.Text;
            dt_SyohinOriginal.Rows[rowindex][4] = txt_cTani.Text;
            dt_SyohinOriginal.Rows[rowindex][5] = txt_nTanka.Text;
            dt_SyohinOriginal.Rows[rowindex][6] = lbl_TankaGokei.Text;
            dt_SyohinOriginal.Rows[rowindex][7] = txt_nGenkaTanka.Text;
            dt_SyohinOriginal.Rows[rowindex][8] = lbl_GenkaGokei.Text;
            dt_SyohinOriginal.Rows[rowindex][9] = lbl_Arari.Text;
            dt_SyohinOriginal.Rows[rowindex][10] = lbl_ArariSu.Text;
            dt_SyohinOriginal.Rows[rowindex][11] = lbl_fgenkataka.Text;
            dt_SyohinOriginal.Rows[rowindex][12] = lbl_rowNo.Text;
            dt_SyohinOriginal.Rows[rowindex][13] = txt_nRITU.Text;
            dt_SyohinOriginal.Rows[rowindex][14] = lbl_kubun.Text;
            dt_SyohinOriginal.Rows[rowindex][15] = lbl_nSIKIRITANKA.Text;
            dt_SyohinOriginal.Rows[rowindex][16] = lbl_fJITAIS.Text;
            dt_SyohinOriginal.Rows[rowindex][17] = lbl_fJITAIQ.Text;

            dt_SyohinOriginal.AcceptChanges();
            #endregion

            dt_SyohinOriginal = SetMidashiSyokei(dt_SyohinOriginal);
            ViewState["SyouhinTable"] = dt_SyohinOriginal;
            GV_MitumoriSyohin_Original.DataSource = dt_SyohinOriginal;
            GV_MitumoriSyohin_Original.DataBind();
            HasCheckRow();
            setSyosaiCount(dt_SyohinOriginal, dt_Syosai);
            updMitsumoriSyohinGrid.Update();

            GV_MitumoriSyohin.DataSource = dt_SyohinOriginal;
            GV_MitumoriSyohin.DataBind();
            //SetSyosai();
            HF_isChange.Value = "1";
            GetTotalKingaku();
            updMitsumoriSyohinGrid.Update();
            updHeader.Update();
        }
        #endregion

        #region txtnTANKA_TextChanged  
        protected void txtnTANKA_TextChanged(object sender, EventArgs e)
        {
            var txt_nTanka = sender as TextBox;
            bool isNumber = Double.TryParse(txt_nTanka.Text, out Double numericValue);
            if (!isNumber)
            {
                txt_nTanka.Text = "";
            }

            if (txt_nTanka.Text == "")
            {
                txt_nTanka.Text = "0";
            }
            GridViewRow gvr = (GridViewRow)txt_nTanka.NamingContainer;
            int rowindex = gvr.RowIndex;
            (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("txtnTANKA") as TextBox).Text = txt_nTanka.Text;
            double nTANKA = Convert.ToDouble(txt_nTanka.Text.Replace(",", ""));
            Double nkakeritsu = Convert.ToDouble((GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("txtnRITU") as TextBox).Text.Replace("%", ""));
            if (!String.IsNullOrEmpty((GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("txtnSURYO") as TextBox).Text))
            {
                double nSURYO = Double.Parse((GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("txtnSURYO") as TextBox).Text.Replace(",", ""));
                double TankaGokei = Math.Round(nSURYO * ((nTANKA / 100) * nkakeritsu), 0);
                double nSikiriTanka= Math.Round((nTANKA / 100) * nkakeritsu, 0);
                (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblTankaGokei") as Label).Text = TankaGokei.ToString("#,##0");
                (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblTankaGokei") as Label).ToolTip = TankaGokei.ToString("#,##0");
                (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblTanka") as Label).Text = nSikiriTanka.ToString("#,##0");
                (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblTanka") as Label).ToolTip = nSikiriTanka.ToString("#,##0");
                if (!String.IsNullOrEmpty((GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("txtnGENKATANKA") as TextBox).Text))
                {
                    double nGenka = Double.Parse((GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("txtnGENKATANKA") as TextBox).Text.Replace(",", ""));
                    double GenkaGokei = Math.Round(nSURYO * nGenka, 0);
                    (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblGenkaGokei") as Label).Text = GenkaGokei.ToString("#,##0");
                    (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblGenkaGokei") as Label).ToolTip = GenkaGokei.ToString("#,##0");
                    double nArari = TankaGokei - GenkaGokei;
                    double nArariSu = (nArari / TankaGokei) * 100;
                    if (TankaGokei == 0)
                    {
                        nArariSu = 0;
                    }
                    (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblnARARI") as Label).Text = nArari.ToString("#,##0");
                    (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblnARARI") as Label).ToolTip = nArari.ToString("#,##0");
                    (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblnARARISu") as Label).Text = nArariSu.ToString("###0.0") + "%";
                    (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblnARARISu") as Label).ToolTip = nArariSu.ToString("###0.0") + "%";

                }

            }
            (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("txtnTANKA") as TextBox).Text = nTANKA.ToString("#,##0.##");
            (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("txtnTANKA") as TextBox).ToolTip = nTANKA.ToString("#,##0.##");
            //updMitsumoriSyohinGrid.Update();

            int rowNo = Convert.ToInt32((gvr.FindControl("lblRowNo") as Label).Text);
            DataTable dt_SyohinOriginal = GetGridViewData();

            if (rowNo == 0)
            {
                var max = dt_SyohinOriginal.AsEnumerable().Max(x => int.Parse(x.Field<string>("rowNo")));
                max += 1;
                HF_maxRowNo.Value = max.ToString();
                (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblRowNo") as Label).Text = max.ToString();
                updMitsumoriSyohinGrid.Update();
            }

            HF_fTankaChange.Value = HF_fTankaChange.Value.Replace(rowNo.ToString(), "");

            #region updateDatatable
            Label lbl_status = (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblhdnStatus") as Label);
            Label lbl_fgenkataka = (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblfgenkatanka") as Label);
            Label lbl_rowNo = (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblRowNo") as Label);
            TextBox txt_cSyohin = (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("txtcSYOHIN") as TextBox);
            TextBox txt_sSyohin = (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("txtsSYOHIN") as TextBox);
            TextBox txt_nSyoryo = (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("txtnSURYO") as TextBox);
            //DropDownList ddl_cTani = (row.FindControl("DDL_cTANI") as DropDownList);
            TextBox txt_cTani = (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("txtTani") as TextBox);
            TextBox txt_nTanka1 = (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("txtnTANKA") as TextBox);
            Label lbl_TankaGokei = (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblTankaGokei") as Label);
            TextBox txt_nGenkaTanka = (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("txtnGENKATANKA") as TextBox);
            Label lbl_GenkaGokei = (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblGenkaGokei") as Label);
            Label lbl_Arari = (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblnARARI") as Label);
            Label lbl_ArariSu = (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblnARARISu") as Label);
            TextBox txt_nRITU = (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("txtnRITU") as TextBox);
            Label lbl_kubun = (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblKubun") as Label);
            Label lbl_nSIKIRITANKA = (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblTanka") as Label);
            Label lbl_fJITAIS = (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblfjitais") as Label);
            Label lbl_fJITAIQ = (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblfjitaiq") as Label);

            dt_SyohinOriginal.Rows[rowindex][0] = lbl_status.Text;
            dt_SyohinOriginal.Rows[rowindex][1] = txt_cSyohin.Text;
            dt_SyohinOriginal.Rows[rowindex][2] = txt_sSyohin.Text;
            dt_SyohinOriginal.Rows[rowindex][3] = txt_nSyoryo.Text;
            dt_SyohinOriginal.Rows[rowindex][4] = txt_cTani.Text;
            dt_SyohinOriginal.Rows[rowindex][5] = txt_nTanka1.Text;
            dt_SyohinOriginal.Rows[rowindex][6] = lbl_TankaGokei.Text;
            dt_SyohinOriginal.Rows[rowindex][7] = txt_nGenkaTanka.Text;
            dt_SyohinOriginal.Rows[rowindex][8] = lbl_GenkaGokei.Text;
            dt_SyohinOriginal.Rows[rowindex][9] = lbl_Arari.Text;
            dt_SyohinOriginal.Rows[rowindex][10] = lbl_ArariSu.Text;
            dt_SyohinOriginal.Rows[rowindex][11] = lbl_fgenkataka.Text;
            dt_SyohinOriginal.Rows[rowindex][12] = lbl_rowNo.Text;
            dt_SyohinOriginal.Rows[rowindex][13] = txt_nRITU.Text;
            dt_SyohinOriginal.Rows[rowindex][14] = lbl_kubun.Text;
            dt_SyohinOriginal.Rows[rowindex][15] = lbl_nSIKIRITANKA.Text;
            dt_SyohinOriginal.Rows[rowindex][16] = lbl_fJITAIS.Text;
            dt_SyohinOriginal.Rows[rowindex][17] = lbl_fJITAIQ.Text;
            dt_SyohinOriginal.AcceptChanges();
            #endregion
            dt_SyohinOriginal = SetMidashiSyokei(dt_SyohinOriginal);
            ViewState["SyouhinTable"] = dt_SyohinOriginal;
            GV_MitumoriSyohin_Original.DataSource = dt_SyohinOriginal;
            GV_MitumoriSyohin_Original.DataBind();
            HasCheckRow();
            DataTable dt_Syosai = GetSyosaiGridViewData();
            setSyosaiCount(dt_SyohinOriginal, dt_Syosai);
            updMitsumoriSyohinGrid.Update();

            GV_MitumoriSyohin.DataSource = dt_SyohinOriginal;
            GV_MitumoriSyohin.DataBind();
            //SetSyosai();
            HF_isChange.Value = "1";
            GetTotalKingaku();
            #region toDelete
            //String rowNo1 = (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblRowNo") as Label).Text;
            //String tankachangeIndex = HF_fTankaChange.Value;
            //string[] textchangeIndex = HF_fTankaChange.Value.Split(',');
            //textchangeIndex = textchangeIndex.Where(x => !string.IsNullOrEmpty(x)).ToArray();
            //var results = Array.FindAll(textchangeIndex, s => s.Equals(rowNo1));
            //if (results.Length <= 0)
            //{
            //    HF_fTankaChange.Value = tankachangeIndex + rowNo1 + ",";
            //}
            #endregion


            updMitsumoriSyohinGrid.Update();
            updHeader.Update();
        }
        #endregion

        #region txtnTANKA_Changed  
        public void txtnTANKA_Changed(GridViewRow row)
        {
            var txt_nTanka = row.FindControl("txtnTANKA") as TextBox;
            bool isNumber = Double.TryParse(txt_nTanka.Text, out Double numericValue);
            if (!isNumber)
            {
                txt_nTanka.Text = "";
            }

            if (txt_nTanka.Text == "")
            {
                txt_nTanka.Text = "0";
            }
            GridViewRow gvr = (GridViewRow)txt_nTanka.NamingContainer;
            int rowindex = gvr.RowIndex;
            String rowNo = (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblRowNo") as Label).Text;
            HF_fTankaChange.Value = HF_fTankaChange.Value.Replace(rowNo.ToString(), "");
            (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("txtnTANKA") as TextBox).Text = txt_nTanka.Text;
            double nTANKA = Convert.ToDouble(txt_nTanka.Text.Replace(",", ""));
            Double nkakeritsu = Convert.ToDouble((row.FindControl("txtnRITU") as TextBox).Text.Replace("%", ""));
            if (!String.IsNullOrEmpty((row.FindControl("txtnSURYO") as TextBox).Text))
            {
                double nSURYO = Double.Parse((row.FindControl("txtnSURYO") as TextBox).Text.Replace(",", ""));
                double TankaGokei = Math.Round(nSURYO * ((nTANKA / 100) * nkakeritsu), 0);
                double nSikiriTanka = Math.Round((nTANKA / 100) * nkakeritsu, 0);
                (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblTankaGokei") as Label).Text = TankaGokei.ToString("#,##0");
                (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblTankaGokei") as Label).ToolTip = TankaGokei.ToString("#,##0");
                (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblTanka") as Label).Text = nSikiriTanka.ToString("#,##0");
                (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblTanka") as Label).ToolTip = nSikiriTanka.ToString("#,##0");
                if (!String.IsNullOrEmpty((row.FindControl("txtnGENKATANKA") as TextBox).Text))
                {
                    double nGenka = Double.Parse((row.FindControl("txtnGENKATANKA") as TextBox).Text.Replace(",", ""));
                    double GenkaGokei = Math.Round(nSURYO * nGenka, 0);
                    (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblGenkaGokei") as Label).Text = GenkaGokei.ToString("#,##0");
                    (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblGenkaGokei") as Label).ToolTip = GenkaGokei.ToString("#,##0");
                    double nArari = TankaGokei - GenkaGokei;
                    double nArariSu = (nArari / TankaGokei) * 100;
                    if (TankaGokei == 0)
                    {
                        nArariSu = 0;
                    }
                    nriritsu = Convert.ToDecimal(nArariSu);
                    (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblnARARI") as Label).Text = nArari.ToString("#,##0");
                    (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblnARARI") as Label).ToolTip = nArari.ToString("#,##0");
                    (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblnARARISu") as Label).Text = nArariSu.ToString("###0.0") + "%";
                    (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblnARARISu") as Label).ToolTip = nArariSu.ToString("###0.0") + "%";
                    updMitsumoriSyohinGrid.Update();

                }

            }
            (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("txtnTANKA") as TextBox).Text = nTANKA.ToString("#,##0.##");
            (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("txtnTANKA") as TextBox).ToolTip = nTANKA.ToString("#,##0.##");
            updMitsumoriSyohinGrid.Update();

            DataTable dt_SyohinOriginal = GetGridViewData();
            #region updateDatatable
            Label lbl_status = (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblhdnStatus") as Label);
            Label lbl_fgenkataka = (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblfgenkatanka") as Label);
            Label lbl_rowNo = (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblRowNo") as Label);
            TextBox txt_cSyohin = (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("txtcSYOHIN") as TextBox);
            TextBox txt_sSyohin = (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("txtsSYOHIN") as TextBox);
            TextBox txt_nSyoryo = (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("txtnSURYO") as TextBox);
            //DropDownList ddl_cTani = (row.FindControl("DDL_cTANI") as DropDownList);
            TextBox txt_cTani = (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("txtTani") as TextBox);
            TextBox txt_nTanka1 = (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("txtnTANKA") as TextBox);
            Label lbl_TankaGokei = (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblTankaGokei") as Label);
            TextBox txt_nGenkaTanka = (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("txtnGENKATANKA") as TextBox);
            Label lbl_GenkaGokei = (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblGenkaGokei") as Label);
            Label lbl_Arari = (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblnARARI") as Label);
            Label lbl_ArariSu = (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblnARARISu") as Label);
            TextBox txt_nRITU = (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("txtnRITU") as TextBox);
            Label lbl_kubun = (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblKubun") as Label);
            Label lbl_nSIKIRITANKA = (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblTanka") as Label);
            Label lbl_fJITAIS = (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblfjitais") as Label);
            Label lbl_fJITAIQ = (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblfjitaiq") as Label);

            dt_SyohinOriginal.Rows[rowindex][0] = lbl_status.Text;
            dt_SyohinOriginal.Rows[rowindex][1] = txt_cSyohin.Text;
            dt_SyohinOriginal.Rows[rowindex][2] = txt_sSyohin.Text;
            dt_SyohinOriginal.Rows[rowindex][3] = txt_nSyoryo.Text;
            dt_SyohinOriginal.Rows[rowindex][4] = txt_cTani.Text;
            dt_SyohinOriginal.Rows[rowindex][5] = txt_nTanka1.Text;
            dt_SyohinOriginal.Rows[rowindex][6] = lbl_TankaGokei.Text;
            dt_SyohinOriginal.Rows[rowindex][7] = txt_nGenkaTanka.Text;
            dt_SyohinOriginal.Rows[rowindex][8] = lbl_GenkaGokei.Text;
            dt_SyohinOriginal.Rows[rowindex][9] = lbl_Arari.Text;
            dt_SyohinOriginal.Rows[rowindex][10] = lbl_ArariSu.Text;
            dt_SyohinOriginal.Rows[rowindex][11] = lbl_fgenkataka.Text;
            dt_SyohinOriginal.Rows[rowindex][12] = lbl_rowNo.Text;
            dt_SyohinOriginal.Rows[rowindex][13] = txt_nRITU.Text;
            dt_SyohinOriginal.Rows[rowindex][14] = lbl_kubun.Text;
            dt_SyohinOriginal.Rows[rowindex][15] = lbl_nSIKIRITANKA.Text;
            dt_SyohinOriginal.Rows[rowindex][16] = lbl_fJITAIS.Text;
            dt_SyohinOriginal.Rows[rowindex][17] = lbl_fJITAIQ.Text;
            dt_SyohinOriginal.AcceptChanges();
            #endregion
            dt_SyohinOriginal = SetMidashiSyokei(dt_SyohinOriginal);
            ViewState["SyouhinTable"] = dt_SyohinOriginal;
            GV_MitumoriSyohin_Original.DataSource = dt_SyohinOriginal;
            GV_MitumoriSyohin_Original.DataBind();
            HasCheckRow();
            DataTable dt_Syosai = GetSyosaiGridViewData();
            setSyosaiCount(dt_SyohinOriginal, dt_Syosai);
            updMitsumoriSyohinGrid.Update();

            GV_MitumoriSyohin.DataSource = dt_SyohinOriginal;
            GV_MitumoriSyohin.DataBind();
            //SetSyosai();
            #region toDelete
            //String rowNo=(GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblRowNo") as Label).Text;
            //String tankachangeIndex = HF_fTankaChange.Value;
            //string[] textchangeIndex = HF_fTankaChange.Value.Split(',');
            //textchangeIndex = textchangeIndex.Where(x => !string.IsNullOrEmpty(x)).ToArray();
            //var results = Array.FindAll(textchangeIndex, s => s.Equals(rowNo));
            //if (results.Length <= 0)
            //{
            //    HF_fTankaChange.Value = tankachangeIndex + rowNo + ",";
            //}
            #endregion
            updMitsumoriSyohinGrid.Update();
            updHeader.Update();

            //int rowNo = Convert.ToInt32((gvr.FindControl("lblRowNo") as Label).Text);
            //if (rowNo == 0)
            //{
            //    DataTable dt_Meisai = GetGridViewData();
            //    var max = dt_Meisai.AsEnumerable().Max(x => int.Parse(x.Field<string>("rowNo")));
            //    max += 1;
            //    HF_maxRowNo.Value = max.ToString();
            //    (gvr.FindControl("lblRowNo") as Label).Text = max.ToString();
            //    updMitsumoriSyohinGrid.Update();
            //}
        }
        #endregion

        #region txtnRITU_TextChanged  
        protected void txtnRITU_TextChanged(object sender, EventArgs e)
        {
            var txt_nRitu = sender as TextBox;
            GridViewRow gvr = (GridViewRow)txt_nRitu.NamingContainer;
            int rowindex = gvr.RowIndex;
            Double nkakeritsu = 0;
            try
            {
                nkakeritsu = Convert.ToDouble(txt_nRitu.Text.Replace("%", ""));
            }
            catch
            {
                nkakeritsu = 0;
                txt_nRitu.Text = "0%";
            }
            if (txt_nRitu.Text == "")
            {
                txt_nRitu.Text = "0%";
            }
            (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("txtnRITU") as TextBox).Text = txt_nRitu.Text;
            if (!String.IsNullOrEmpty((GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("txtnSURYO") as TextBox).Text))
            {
                double nSURYO = Double.Parse((GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("txtnSURYO") as TextBox).Text.Replace(",", ""));
                double TankaGokei = 0;
                double nSikiriTanka = 0;
                if (!String.IsNullOrEmpty((GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("txtnTANKA") as TextBox).Text))
                {
                    Double nTANKA = Convert.ToDouble((GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("txtnTANKA") as TextBox).Text.Replace(",", ""));
                    TankaGokei = Math.Round(nSURYO * ((nTANKA / 100) * nkakeritsu), 0);
                    nSikiriTanka = Math.Round((nTANKA / 100) * nkakeritsu, 0);
                    (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblTankaGokei") as Label).Text = TankaGokei.ToString("#,##0");
                    (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblTankaGokei") as Label).ToolTip = TankaGokei.ToString("#,##0");
                    (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblTanka") as Label).Text = nSikiriTanka.ToString("#,##0");
                    (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblTanka") as Label).ToolTip = nSikiriTanka.ToString("#,##0");
                }

                if (!String.IsNullOrEmpty((GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblTankaGokei") as Label).Text) && !String.IsNullOrEmpty((GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblGenkaGokei") as Label).Text))
                {
                    double GenkaGokei = Convert.ToDouble((GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblGenkaGokei") as Label).Text.Replace(",", ""));
                    double nArari = TankaGokei - GenkaGokei;
                    double nArariSu = (nArari / TankaGokei) * 100;
                    if (TankaGokei == 0)
                    {
                        nArariSu = 0;
                    }
                    (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblnARARI") as Label).Text = nArari.ToString("#,##0");
                    (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblnARARI") as Label).ToolTip = nArari.ToString("#,##0");
                    (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblnARARISu") as Label).Text = nArariSu.ToString("###0.0") + "%";
                    (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblnARARISu") as Label).ToolTip = nArariSu.ToString("###0.0") + "%";

                }

            }
            (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("txtnRITU") as TextBox).Text = nkakeritsu.ToString("#,##0") + "%";
            (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("txtnRITU") as TextBox).ToolTip = nkakeritsu.ToString("#,##0") + "%";
            //updMitsumoriSyohinGrid.Update();

            int rowNo = Convert.ToInt32((gvr.FindControl("lblRowNo") as Label).Text);

            DataTable dt_SyohinOriginal = GetGridViewData();
            if (rowNo == 0)
            {
                var max = dt_SyohinOriginal.AsEnumerable().Max(x => int.Parse(x.Field<string>("rowNo")));
                max += 1;
                HF_maxRowNo.Value = max.ToString();
                (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblRowNo") as Label).Text = max.ToString();
                updMitsumoriSyohinGrid.Update();
            }
            #region updateDatatable
            Label lbl_status = (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblhdnStatus") as Label);
            Label lbl_fgenkataka = (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblfgenkatanka") as Label);
            Label lbl_rowNo = (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblRowNo") as Label);
            TextBox txt_cSyohin = (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("txtcSYOHIN") as TextBox);
            TextBox txt_sSyohin = (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("txtsSYOHIN") as TextBox);
            TextBox txt_nSyoryo = (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("txtnSURYO") as TextBox);
            //DropDownList ddl_cTani = (row.FindControl("DDL_cTANI") as DropDownList);
            TextBox txt_cTani = (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("txtTani") as TextBox);
            TextBox txt_nTanka = (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("txtnTANKA") as TextBox);
            Label lbl_TankaGokei = (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblTankaGokei") as Label);
            TextBox txt_nGenkaTanka = (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("txtnGENKATANKA") as TextBox);
            Label lbl_GenkaGokei = (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblGenkaGokei") as Label);
            Label lbl_Arari = (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblnARARI") as Label);
            Label lbl_ArariSu = (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblnARARISu") as Label);
            TextBox txt_nRITU = (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("txtnRITU") as TextBox);
            Label lbl_kubun = (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblKubun") as Label);
            Label lbl_nSIKIRITANKA = (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblTanka") as Label);
            Label lbl_fJITAIS = (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblfjitais") as Label);
            Label lbl_fJITAIQ = (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblfjitaiq") as Label);

            dt_SyohinOriginal.Rows[rowindex][0] = lbl_status.Text;
            dt_SyohinOriginal.Rows[rowindex][1] = txt_cSyohin.Text;
            dt_SyohinOriginal.Rows[rowindex][2] = txt_sSyohin.Text;
            dt_SyohinOriginal.Rows[rowindex][3] = txt_nSyoryo.Text;
            dt_SyohinOriginal.Rows[rowindex][4] = txt_cTani.Text;
            dt_SyohinOriginal.Rows[rowindex][5] = txt_nTanka.Text;
            dt_SyohinOriginal.Rows[rowindex][6] = lbl_TankaGokei.Text;
            dt_SyohinOriginal.Rows[rowindex][7] = txt_nGenkaTanka.Text;
            dt_SyohinOriginal.Rows[rowindex][8] = lbl_GenkaGokei.Text;
            dt_SyohinOriginal.Rows[rowindex][9] = lbl_Arari.Text;
            dt_SyohinOriginal.Rows[rowindex][10] = lbl_ArariSu.Text;
            dt_SyohinOriginal.Rows[rowindex][11] = lbl_fgenkataka.Text;
            dt_SyohinOriginal.Rows[rowindex][12] = lbl_rowNo.Text;
            dt_SyohinOriginal.Rows[rowindex][13] = txt_nRITU.Text;
            dt_SyohinOriginal.Rows[rowindex][14] = lbl_kubun.Text;
            dt_SyohinOriginal.Rows[rowindex][15] = lbl_nSIKIRITANKA.Text;
            dt_SyohinOriginal.Rows[rowindex][16] = lbl_fJITAIS.Text;
            dt_SyohinOriginal.Rows[rowindex][17] = lbl_fJITAIQ.Text;
            dt_SyohinOriginal.AcceptChanges();
            #endregion
            dt_SyohinOriginal = SetMidashiSyokei(dt_SyohinOriginal);
            ViewState["SyouhinTable"] = dt_SyohinOriginal;
            GV_MitumoriSyohin_Original.DataSource = dt_SyohinOriginal;
            GV_MitumoriSyohin_Original.DataBind();
            HasCheckRow();
            DataTable dt_Syosai = GetSyosaiGridViewData();
            setSyosaiCount(dt_SyohinOriginal, dt_Syosai);
            updMitsumoriSyohinGrid.Update();

            GV_MitumoriSyohin.DataSource = dt_SyohinOriginal;
            GV_MitumoriSyohin.DataBind();
            //SetSyosai();

            HF_isChange.Value = "1";
            GetTotalKingaku();
            updMitsumoriSyohinGrid.Update();
            updHeader.Update();
        }
        #endregion

        #region txtnGENKATANKA_TextChanged  
        protected void txtnGENKATANKA_TextChanged(object sender, EventArgs e)
        {
            var txt_nGenka = sender as TextBox;
            bool isNumber = Double.TryParse(txt_nGenka.Text, out Double numericValue);
            if (!isNumber)
            {
                txt_nGenka.Text = "";
            }

            if (txt_nGenka.Text == "")
            {
                txt_nGenka.Text = "0";
            }
            GridViewRow gvr = (GridViewRow)txt_nGenka.NamingContainer;
            int rowindex = gvr.RowIndex;
            (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("txtnGENKATANKA") as TextBox).Text = txt_nGenka.Text;
            double nGenka = Double.Parse(txt_nGenka.Text.Replace(",", ""));
            if (!String.IsNullOrEmpty((GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("txtnSURYO") as TextBox).Text))
            {
                double nSURYO = Double.Parse((GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("txtnSURYO") as TextBox).Text.Replace(",", ""));
                double GenkaGokei = Math.Round(nSURYO * nGenka, 0);
                (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblGenkaGokei") as Label).Text = GenkaGokei.ToString("#,##0");
                (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblGenkaGokei") as Label).ToolTip = GenkaGokei.ToString("#,##0");
                if (!String.IsNullOrEmpty((GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("txtnTANKA") as TextBox).Text))
                {
                    double TankaGokei = Convert.ToDouble((GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblTankaGokei") as Label).Text.Replace(",", ""));
                    double nArari = TankaGokei - GenkaGokei;
                    double nArariSu = (nArari / TankaGokei) * 100;
                    if (TankaGokei == 0)
                    {
                        nArariSu = 0;
                    }

                    (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblnARARI") as Label).Text = nArari.ToString("#,##0");
                    (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblnARARI") as Label).ToolTip = nArari.ToString("#,##0");
                    (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblnARARISu") as Label).Text = nArariSu.ToString("###0.0") + "%";
                    (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblnARARISu") as Label).ToolTip = nArariSu.ToString("###0.0") + "%";
                }
            }
            (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("txtnGENKATANKA") as TextBox).Text = nGenka.ToString("#,##0.##");
            (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("txtnGENKATANKA") as TextBox).ToolTip = nGenka.ToString("#,##0.##");
            //updMitsumoriSyohinGrid.Update();

            int rowNo = Convert.ToInt32((gvr.FindControl("lblRowNo") as Label).Text);
            DataTable dt_SyohinOriginal = GetGridViewData();
            if (rowNo == 0)
            {
                var max = dt_SyohinOriginal.AsEnumerable().Max(x => int.Parse(x.Field<string>("rowNo")));
                max += 1;
                HF_maxRowNo.Value = max.ToString();
                (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblRowNo") as Label).Text = max.ToString();
                updMitsumoriSyohinGrid.Update();
            }

            HF_fGenkaChange.Value = HF_fGenkaChange.Value.Replace(rowNo.ToString(), "");

            #region updateDatatable
            Label lbl_status = (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblhdnStatus") as Label);
            Label lbl_fgenkataka = (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblfgenkatanka") as Label);
            Label lbl_rowNo = (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblRowNo") as Label);
            TextBox txt_cSyohin = (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("txtcSYOHIN") as TextBox);
            TextBox txt_sSyohin = (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("txtsSYOHIN") as TextBox);
            TextBox txt_nSyoryo = (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("txtnSURYO") as TextBox);
            //DropDownList ddl_cTani = (row.FindControl("DDL_cTANI") as DropDownList);
            TextBox txt_cTani = (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("txtTani") as TextBox);
            TextBox txt_nTanka = (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("txtnTANKA") as TextBox);
            Label lbl_TankaGokei = (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblTankaGokei") as Label);
            TextBox txt_nGenkaTanka = (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("txtnGENKATANKA") as TextBox);
            Label lbl_GenkaGokei = (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblGenkaGokei") as Label);
            Label lbl_Arari = (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblnARARI") as Label);
            Label lbl_ArariSu = (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblnARARISu") as Label);
            TextBox txt_nRITU = (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("txtnRITU") as TextBox);
            Label lbl_kubun = (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblKubun") as Label);
            Label lbl_nSIKIRITANKA = (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblTanka") as Label);
            Label lbl_fJITAIS = (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblfjitais") as Label);
            Label lbl_fJITAIQ = (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblfjitaiq") as Label);

            dt_SyohinOriginal.Rows[rowindex][0] = lbl_status.Text;
            dt_SyohinOriginal.Rows[rowindex][1] = txt_cSyohin.Text;
            dt_SyohinOriginal.Rows[rowindex][2] = txt_sSyohin.Text;
            dt_SyohinOriginal.Rows[rowindex][3] = txt_nSyoryo.Text;
            dt_SyohinOriginal.Rows[rowindex][4] = txt_cTani.Text;
            dt_SyohinOriginal.Rows[rowindex][5] = txt_nTanka.Text;
            dt_SyohinOriginal.Rows[rowindex][6] = lbl_TankaGokei.Text;
            dt_SyohinOriginal.Rows[rowindex][7] = txt_nGenkaTanka.Text;
            dt_SyohinOriginal.Rows[rowindex][8] = lbl_GenkaGokei.Text;
            dt_SyohinOriginal.Rows[rowindex][9] = lbl_Arari.Text;
            dt_SyohinOriginal.Rows[rowindex][10] = lbl_ArariSu.Text;
            dt_SyohinOriginal.Rows[rowindex][11] = lbl_fgenkataka.Text;
            dt_SyohinOriginal.Rows[rowindex][12] = lbl_rowNo.Text;
            dt_SyohinOriginal.Rows[rowindex][13] = txt_nRITU.Text;
            dt_SyohinOriginal.Rows[rowindex][14] = lbl_kubun.Text;
            dt_SyohinOriginal.Rows[rowindex][15] = lbl_nSIKIRITANKA.Text;
            dt_SyohinOriginal.Rows[rowindex][16] = lbl_fJITAIS.Text;
            dt_SyohinOriginal.Rows[rowindex][17] = lbl_fJITAIQ.Text;
            dt_SyohinOriginal.AcceptChanges();
            #endregion

            ViewState["SyouhinTable"] = dt_SyohinOriginal;
            GV_MitumoriSyohin.DataSource = dt_SyohinOriginal;
            GV_MitumoriSyohin.DataBind();
            //SetSyosai();

            HF_isChange.Value = "1";
            GetTotalKingaku();
            updMitsumoriSyohinGrid.Update();
            updHeader.Update();
        }
        #endregion

        #region txtnGENKATANKA_Changed  
        public void txtnGENKATANKA_Changed(GridViewRow row)
        {
            var txt_nGenka = row.FindControl("txtnGENKATANKA") as TextBox;
            bool isNumber = Double.TryParse(txt_nGenka.Text, out Double numericValue);
            if (!isNumber)
            {
                txt_nGenka.Text = "";
            }

            if (txt_nGenka.Text == "")
            {
                txt_nGenka.Text = "0";
            }
            GridViewRow gvr = (GridViewRow)txt_nGenka.NamingContainer;
            int rowindex = gvr.RowIndex;
            String rowNo = (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblRowNo") as Label).Text;
            HF_fGenkaChange.Value = HF_fGenkaChange.Value.Replace(rowNo.ToString(), "");
            (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("txtnGENKATANKA") as TextBox).Text = txt_nGenka.Text;
            double nGenka = Double.Parse(txt_nGenka.Text.Replace(",", ""));
            if (!String.IsNullOrEmpty((row.FindControl("txtnSURYO") as TextBox).Text))
            {
                double nSURYO = Double.Parse((row.FindControl("txtnSURYO") as TextBox).Text.Replace(",", ""));
                double GenkaGokei = Math.Round(nSURYO * nGenka, 0);
                (row.FindControl("lblGenkaGokei") as Label).Text = GenkaGokei.ToString("#,##0");
                (row.FindControl("lblGenkaGokei") as Label).ToolTip = GenkaGokei.ToString("#,##0");
                if (!String.IsNullOrEmpty((row.FindControl("txtnTANKA") as TextBox).Text))
                {
                    double TankaGokei = Convert.ToDouble((row.FindControl("lblTankaGokei") as Label).Text.Replace(",", ""));
                    double nArari = TankaGokei - GenkaGokei;
                    double nArariSu = (nArari / TankaGokei) * 100;
                    if (TankaGokei == 0)
                    {
                        nArariSu = 0;
                    }
                    nriritsu = Convert.ToDecimal(nArariSu);
                    (row.FindControl("lblnARARI") as Label).Text = nArari.ToString("#,##0");
                    (row.FindControl("lblnARARI") as Label).ToolTip = nArari.ToString("#,##0");
                    (row.FindControl("lblnARARISu") as Label).Text = nArariSu.ToString("###0.0") + "%";
                    (row.FindControl("lblnARARISu") as Label).ToolTip = nArariSu.ToString("###0.0") + "%";
                }
            }
            (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("txtnGENKATANKA") as TextBox).Text = nGenka.ToString("#,##0.##");
            (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("txtnGENKATANKA") as TextBox).ToolTip = nGenka.ToString("#,##0.##");

            DataTable dt_SyohinOriginal = GetGridViewData();
            #region updateDatatable
            Label lbl_status = (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblhdnStatus") as Label);
            Label lbl_fgenkataka = (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblfgenkatanka") as Label);
            Label lbl_rowNo = (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblRowNo") as Label);
            TextBox txt_cSyohin = (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("txtcSYOHIN") as TextBox);
            TextBox txt_sSyohin = (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("txtsSYOHIN") as TextBox);
            TextBox txt_nSyoryo = (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("txtnSURYO") as TextBox);
            //DropDownList ddl_cTani = (row.FindControl("DDL_cTANI") as DropDownList);
            TextBox txt_cTani = (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("txtTani") as TextBox);
            TextBox txt_nTanka = (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("txtnTANKA") as TextBox);
            Label lbl_TankaGokei = (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblTankaGokei") as Label);
            TextBox txt_nGenkaTanka = (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("txtnGENKATANKA") as TextBox);
            Label lbl_GenkaGokei = (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblGenkaGokei") as Label);
            Label lbl_Arari = (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblnARARI") as Label);
            Label lbl_ArariSu = (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblnARARISu") as Label);
            TextBox txt_nRITU = (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("txtnRITU") as TextBox);
            Label lbl_kubun = (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblKubun") as Label);
            Label lbl_nSIKIRITANKA = (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblTanka") as Label);
            Label lbl_fJITAIS = (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblfjitais") as Label);
            Label lbl_fJITAIQ = (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblfjitaiq") as Label);

            dt_SyohinOriginal.Rows[rowindex][0] = lbl_status.Text;
            dt_SyohinOriginal.Rows[rowindex][1] = txt_cSyohin.Text;
            dt_SyohinOriginal.Rows[rowindex][2] = txt_sSyohin.Text;
            dt_SyohinOriginal.Rows[rowindex][3] = txt_nSyoryo.Text;
            dt_SyohinOriginal.Rows[rowindex][4] = txt_cTani.Text;
            dt_SyohinOriginal.Rows[rowindex][5] = txt_nTanka.Text;
            dt_SyohinOriginal.Rows[rowindex][6] = lbl_TankaGokei.Text;
            dt_SyohinOriginal.Rows[rowindex][7] = txt_nGenkaTanka.Text;
            dt_SyohinOriginal.Rows[rowindex][8] = lbl_GenkaGokei.Text;
            dt_SyohinOriginal.Rows[rowindex][9] = lbl_Arari.Text;
            dt_SyohinOriginal.Rows[rowindex][10] = lbl_ArariSu.Text;
            dt_SyohinOriginal.Rows[rowindex][11] = lbl_fgenkataka.Text;
            dt_SyohinOriginal.Rows[rowindex][12] = lbl_rowNo.Text;
            dt_SyohinOriginal.Rows[rowindex][13] = txt_nRITU.Text;
            dt_SyohinOriginal.Rows[rowindex][14] = lbl_kubun.Text;
            dt_SyohinOriginal.Rows[rowindex][15] = lbl_nSIKIRITANKA.Text;
            dt_SyohinOriginal.Rows[rowindex][16] = lbl_fJITAIS.Text;
            dt_SyohinOriginal.Rows[rowindex][17] = lbl_fJITAIQ.Text;
            dt_SyohinOriginal.AcceptChanges();
            #endregion
            ViewState["SyouhinTable"] = dt_SyohinOriginal;
            GV_MitumoriSyohin.DataSource = dt_SyohinOriginal;
            GV_MitumoriSyohin.DataBind();
            //SetSyosai();
            updMitsumoriSyohinGrid.Update();
            updHeader.Update();
            //int rowNo = Convert.ToInt32((gvr.FindControl("lblRowNo") as Label).Text);
            //if (rowNo == 0)
            //{
            //    DataTable dt_Meisai = GetGridViewData();
            //    var max = dt_Meisai.AsEnumerable().Max(x => int.Parse(x.Field<string>("rowNo")));
            //    max += 1;
            //    HF_maxRowNo.Value = max.ToString();
            //    (gvr.FindControl("lblRowNo") as Label).Text = max.ToString();
            //    updMitsumoriSyohinGrid.Update();
            //}
        }
        #endregion

        #region txtsSYOHIN_TextChanged  
        protected void txtsSYOHIN_TextChanged(object sender, EventArgs e)
        {
            var txt_sSyohin = sender as TextBox;
            GridViewRow gvr = (GridViewRow)txt_sSyohin.NamingContainer;
            int rowindex = gvr.RowIndex;
            (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("txtsSYOHIN") as TextBox).Text = txt_sSyohin.Text;

            int rowNo = Convert.ToInt32((gvr.FindControl("lblRowNo") as Label).Text);
            DataTable dt_SyohinOriginal = GetGridViewData();
            if (rowNo == 0)
            {
                var max = dt_SyohinOriginal.AsEnumerable().Max(x => int.Parse(x.Field<string>("rowNo")));
                max += 1;
                HF_maxRowNo.Value = max.ToString();
                (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblRowNo") as Label).Text = max.ToString();
                updMitsumoriSyohinGrid.Update();
            }

            //GetTotalKingaku();

            #region updateDatatable
            Label lbl_status = (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblhdnStatus") as Label);
            Label lbl_fgenkataka = (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblfgenkatanka") as Label);
            Label lbl_rowNo = (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblRowNo") as Label);
            TextBox txt_cSyohin = (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("txtcSYOHIN") as TextBox);
            TextBox txt_sSyohin1 = (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("txtsSYOHIN") as TextBox);
            TextBox txt_nSyoryo = (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("txtnSURYO") as TextBox);
            //DropDownList ddl_cTani = (row.FindControl("DDL_cTANI") as DropDownList);
            TextBox txt_cTani = (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("txtTani") as TextBox);
            TextBox txt_nTanka = (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("txtnTANKA") as TextBox);
            Label lbl_TankaGokei = (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblTankaGokei") as Label);
            TextBox txt_nGenkaTanka = (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("txtnGENKATANKA") as TextBox);
            Label lbl_GenkaGokei = (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblGenkaGokei") as Label);
            Label lbl_Arari = (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblnARARI") as Label);
            Label lbl_ArariSu = (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblnARARISu") as Label);
            TextBox txt_nRITU = (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("txtnRITU") as TextBox);
            Label lbl_kubun = (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblKubun") as Label);
            Label lbl_nSIKIRITANKA = (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblTanka") as Label);
            Label lbl_fJITAIS = (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblfjitais") as Label);
            Label lbl_fJITAIQ = (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblfjitaiq") as Label);

            dt_SyohinOriginal.Rows[rowindex][0] = lbl_status.Text;
            dt_SyohinOriginal.Rows[rowindex][1] = txt_cSyohin.Text;
            dt_SyohinOriginal.Rows[rowindex][2] = txt_sSyohin1.Text;
            dt_SyohinOriginal.Rows[rowindex][3] = txt_nSyoryo.Text;
            dt_SyohinOriginal.Rows[rowindex][4] = txt_cTani.Text;
            dt_SyohinOriginal.Rows[rowindex][5] = txt_nTanka.Text;
            dt_SyohinOriginal.Rows[rowindex][6] = lbl_TankaGokei.Text;
            dt_SyohinOriginal.Rows[rowindex][7] = txt_nGenkaTanka.Text;
            dt_SyohinOriginal.Rows[rowindex][8] = lbl_GenkaGokei.Text;
            dt_SyohinOriginal.Rows[rowindex][9] = lbl_Arari.Text;
            dt_SyohinOriginal.Rows[rowindex][10] = lbl_ArariSu.Text;
            dt_SyohinOriginal.Rows[rowindex][11] = lbl_fgenkataka.Text;
            dt_SyohinOriginal.Rows[rowindex][12] = lbl_rowNo.Text;
            dt_SyohinOriginal.Rows[rowindex][13] = txt_nRITU.Text;
            dt_SyohinOriginal.Rows[rowindex][14] = lbl_kubun.Text;
            dt_SyohinOriginal.Rows[rowindex][15] = lbl_nSIKIRITANKA.Text;
            dt_SyohinOriginal.Rows[rowindex][16] = lbl_fJITAIS.Text;
            dt_SyohinOriginal.Rows[rowindex][17] = lbl_fJITAIQ.Text;
            dt_SyohinOriginal.AcceptChanges();
            #endregion
            ViewState["SyouhinTable"] = dt_SyohinOriginal;
            GV_MitumoriSyohin.DataSource = dt_SyohinOriginal;
            GV_MitumoriSyohin.DataBind();
            //SetSyosai();

            HF_isChange.Value = "1";
            updMitsumoriSyohinGrid.Update();
            updHeader.Update();
        }
        #endregion

        #region DDL_cTANI_SelectedIndexChanged
        protected void DDL_cTANI_SelectedIndexChanged(object sender, EventArgs e)
        {
            var ddl_tani = sender as DropDownList;
            GridViewRow gvr = (GridViewRow)ddl_tani.NamingContainer;
            int rowindex = gvr.RowIndex;
            (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("DDL_cTANI") as DropDownList).SelectedIndex = ddl_tani.SelectedIndex;
            (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblcTANI") as Label).Text = ddl_tani.SelectedItem.ToString();
            (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("txtTani") as TextBox).Text = ddl_tani.SelectedItem.ToString();

            (GV_MitumoriSyohin.Rows[rowindex].FindControl("DDL_cTANI") as DropDownList).SelectedIndex = ddl_tani.SelectedIndex;
            (GV_MitumoriSyohin.Rows[rowindex].FindControl("lblcTANI") as Label).Text = ddl_tani.SelectedItem.ToString();
            (GV_MitumoriSyohin.Rows[rowindex].FindControl("txtTani") as TextBox).Text = ddl_tani.SelectedItem.ToString();
            updMitsumoriSyohinGrid.Update();

            int rowNo = Convert.ToInt32((gvr.FindControl("lblRowNo") as Label).Text);
            DataTable dt_Meisai = GetGridViewData();
            if (rowNo == 0)
            {
                var max = dt_Meisai.AsEnumerable().Max(x => int.Parse(x.Field<string>("rowNo")));
                max += 1;
                HF_maxRowNo.Value = max.ToString();
                (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblRowNo") as Label).Text = max.ToString();
                (GV_MitumoriSyohin.Rows[rowindex].FindControl("lblRowNo") as Label).Text = max.ToString();
                dt_Meisai.Rows[rowindex]["rowNo"] = max.ToString();
                updMitsumoriSyohinGrid.Update();
            }

            dt_Meisai.Rows[rowindex]["cTANI"] = ddl_tani.SelectedItem.ToString();
            dt_Meisai.AcceptChanges();

            ViewState["SyouhinTable"] = dt_Meisai;
            //DataTable dt_SyohinOriginal = GetGridViewData();
            //GV_MitumoriSyohin.DataSource = dt_Meisai;
            //GV_MitumoriSyohin.DataBind();
            //SetSyosai();

            HF_isChange.Value = "1";
           updMitsumoriSyohinGrid.Update();
           updHeader.Update();
        }
        #endregion

        #region txtTani_TextChanged
        protected void txtTani_TextChanged(object sender, EventArgs e)
        {
            var txt_tani = sender as TextBox;
            GridViewRow gvr = (GridViewRow)txt_tani.NamingContainer;
            int rowindex = gvr.RowIndex;
            String tani = HF_TxtTani.Value;
            (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblcTANI") as Label).Text = tani;
            (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("txtTani") as TextBox).Text = tani;

            (GV_MitumoriSyohin.Rows[rowindex].FindControl("lblcTANI") as Label).Text = tani;
            (GV_MitumoriSyohin.Rows[rowindex].FindControl("txtTani") as TextBox).Text = tani;
            updMitsumoriSyohinGrid.Update();

            int rowNo = Convert.ToInt32((gvr.FindControl("lblRowNo") as Label).Text);
            DataTable dt_Meisai = GetGridViewData();
            if (rowNo == 0)
            {
                var max = dt_Meisai.AsEnumerable().Max(x => int.Parse(x.Field<string>("rowNo")));
                max += 1;
                HF_maxRowNo.Value = max.ToString();
                (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblRowNo") as Label).Text = max.ToString();
                (GV_MitumoriSyohin.Rows[rowindex].FindControl("lblRowNo") as Label).Text = max.ToString();
                dt_Meisai.Rows[rowindex]["rowNo"] = max.ToString();
                updMitsumoriSyohinGrid.Update();
            }

            dt_Meisai.Rows[rowindex]["cTANI"] = tani;
            dt_Meisai.AcceptChanges();

            ViewState["SyouhinTable"] = dt_Meisai;
            //DataTable dt_SyohinOriginal = GetGridViewData();
            //GV_MitumoriSyohin.DataSource = dt_Meisai;
            //GV_MitumoriSyohin.DataBind();
            //SetSyosai();

            HF_isChange.Value = "1";
            updMitsumoriSyohinGrid.Update();
            updHeader.Update();
        }
        #endregion

        #region LK_Kyo_Click
        protected void LK_Kyo_Click(object sender, EventArgs e)
        {
            var LK_Kyo = sender as LinkButton;
            GridViewRow gvr = (GridViewRow)LK_Kyo.NamingContainer;
            int rowindex = gvr.RowIndex;
            String fJitaiq = "0";

            if (LK_Kyo.BackColor == Color.White)
            {
                (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblfjitaiq") as Label).Text = "1";
                (GV_MitumoriSyohin.Rows[rowindex].FindControl("lblfjitaiq") as Label).Text = "1";
                //(GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("txtsSYOHIN") as TextBox).Font.Bold = true;
                //(GV_MitumoriSyohin.Rows[rowindex].FindControl("txtsSYOHIN") as TextBox).Font.Bold = true;
                LK_Kyo.BackColor = ColorTranslator.FromHtml("#f2f2f2");
                fJitaiq = "1";
            }
            else
            {
                (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblfjitaiq") as Label).Text = "0";
                (GV_MitumoriSyohin.Rows[rowindex].FindControl("lblfjitaiq") as Label).Text = "0";
                //(GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("txtsSYOHIN") as TextBox).Font.Bold = false;
                //(GV_MitumoriSyohin.Rows[rowindex].FindControl("txtsSYOHIN") as TextBox).Font.Bold = false;
                LK_Kyo.BackColor = Color.White;
                fJitaiq = "0";
            }

            updMitsumoriSyohinGrid.Update();

            int rowNo = Convert.ToInt32((gvr.FindControl("lblRowNo") as Label).Text);
            DataTable dt_Meisai = GetGridViewData();
            if (rowNo == 0)
            {
                var max = dt_Meisai.AsEnumerable().Max(x => int.Parse(x.Field<string>("rowNo")));
                max += 1;
                HF_maxRowNo.Value = max.ToString();
                (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblRowNo") as Label).Text = max.ToString();
                (GV_MitumoriSyohin.Rows[rowindex].FindControl("lblRowNo") as Label).Text = max.ToString();
                dt_Meisai.Rows[rowindex]["rowNo"] = max.ToString();
                updMitsumoriSyohinGrid.Update();
            }

            dt_Meisai.Rows[rowindex]["fJITAIQ"] = fJitaiq;
            dt_Meisai.AcceptChanges();

            ViewState["SyouhinTable"] = dt_Meisai;
            //DataTable dt_SyohinOriginal = GetGridViewData();
            //GV_MitumoriSyohin.DataSource = dt_Meisai;
            //GV_MitumoriSyohin.DataBind();
            //SetSyosai();

            HF_isChange.Value = "1";
            updMitsumoriSyohinGrid.Update();
            updHeader.Update();
        }
        #endregion

        #region LK_Syou_Click
        protected void LK_Syou_Click(object sender, EventArgs e)
        {
            var LK_Syou = sender as LinkButton;
            GridViewRow gvr = (GridViewRow)LK_Syou.NamingContainer;
            int rowindex = gvr.RowIndex;
            String fJitais = "0";
            if (LK_Syou.BackColor == Color.White)
            {
                (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblfjitais") as Label).Text = "1";
                (GV_MitumoriSyohin.Rows[rowindex].FindControl("lblfjitais") as Label).Text = "1";
                //(GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("txtsSYOHIN") as TextBox).Style.Add("font-size", "11px !important");
                //(GV_MitumoriSyohin.Rows[rowindex].FindControl("txtsSYOHIN") as TextBox).Style.Add("font-size", "11px !important");
                (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("txtsSYOHIN") as TextBox).MaxLength = 60;
                (GV_MitumoriSyohin.Rows[rowindex].FindControl("txtsSYOHIN") as TextBox).MaxLength = 60;
                LK_Syou.BackColor = ColorTranslator.FromHtml("#f2f2f2");
                fJitais = "1";
            }
            else
            {
                (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblfjitais") as Label).Text = "0";
                (GV_MitumoriSyohin.Rows[rowindex].FindControl("lblfjitais") as Label).Text = "0";
                (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("txtsSYOHIN") as TextBox).MaxLength = 50;
                (GV_MitumoriSyohin.Rows[rowindex].FindControl("txtsSYOHIN") as TextBox).MaxLength = 50;

                var textByteCount = Encoding.Default.GetByteCount((GV_MitumoriSyohin.Rows[rowindex].FindControl("txtsSYOHIN") as TextBox).Text);
                if (textByteCount > 50)
                {
                    string val = (GV_MitumoriSyohin.Rows[rowindex].FindControl("txtsSYOHIN") as TextBox).Text;
                    while (Encoding.Default.GetByteCount(val) > 50)
                    {
                        val = val.Substring(0, val.Length - 1);
                    }
                    (GV_MitumoriSyohin.Rows[rowindex].FindControl("txtsSYOHIN") as TextBox).Text = val;
                    (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("txtsSYOHIN") as TextBox).Text = val;
                }

                //(GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("txtsSYOHIN") as TextBox).Style.Add("font-size", "13px !important");
                //(GV_MitumoriSyohin.Rows[rowindex].FindControl("txtsSYOHIN") as TextBox).Style.Add("font-size", "13px !important");
                LK_Syou.BackColor = Color.White;
                fJitais = "0";
            }
            updMitsumoriSyohinGrid.Update();

            int rowNo = Convert.ToInt32((gvr.FindControl("lblRowNo") as Label).Text);
            DataTable dt_Meisai = GetGridViewData();
            if (rowNo == 0)
            {
                var max = dt_Meisai.AsEnumerable().Max(x => int.Parse(x.Field<string>("rowNo")));
                max += 1;
                HF_maxRowNo.Value = max.ToString();
                (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblRowNo") as Label).Text = max.ToString();
                (GV_MitumoriSyohin.Rows[rowindex].FindControl("lblRowNo") as Label).Text = max.ToString();
                dt_Meisai.Rows[rowindex]["rowNo"] = max.ToString();
                updMitsumoriSyohinGrid.Update();
            }

            dt_Meisai.Rows[rowindex]["fJITAIS"] = fJitais;
            dt_Meisai.AcceptChanges();

            ViewState["SyouhinTable"] = dt_Meisai;
            //DataTable dt_SyohinOriginal = GetGridViewData();
            //GV_MitumoriSyohin.DataSource = dt_Meisai;
            //GV_MitumoriSyohin.DataBind();
            //SetSyosai();

            HF_isChange.Value = "1";
            updMitsumoriSyohinGrid.Update();
            updHeader.Update();
        }
        #endregion

        #region chkSelectSyouhin_CheckedChanged
        protected void chkSelectSyouhin_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chk = sender as CheckBox;
            GridViewRow gvr = (GridViewRow)chk.NamingContainer;
            int rowindex = gvr.RowIndex;
            String CheckValue = "";
            if (chk.Checked)
            {
                (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblhdnStatus") as Label).Text = "1";
                (GV_MitumoriSyohin.Rows[rowindex].FindControl("lblhdnStatus") as Label).Text = "1";
                CheckValue = "1";
            }
            else
            {
                (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblhdnStatus") as Label).Text = "0";
                (GV_MitumoriSyohin.Rows[rowindex].FindControl("lblhdnStatus") as Label).Text = "0";
                CheckValue = "0";
            }
            DataTable dt_SyohinOriginal = GetGridViewData();
            dt_SyohinOriginal.Rows[rowindex][0] = CheckValue;
            ViewState["SyouhinTable"] = dt_SyohinOriginal;

            //GV_MitumoriSyohin.DataSource = dt_SyohinOriginal;
            //GV_MitumoriSyohin.DataBind();
            //SetSyosai();
            updMitsumoriSyohinGrid.Update();
            updHeader.Update();
            HasCheckRow();
        }
        #endregion

        #region HasCheckRow()
        private void HasCheckRow()
        {
            foreach (GridViewRow row in GV_MitumoriSyohin.Rows)
            {
                CheckBox chk_status = (row.FindControl("chkSelectSyouhin") as CheckBox);
                if (chk_status.Checked)
                {
                    btnFukusuCopy.CssClass = "JC10GrayButton";
                    btnFukusuCopy.Enabled = true;
                    btnMidashitsuika.CssClass = "JC10GrayButton";
                    btnMidashitsuika.Enabled = true;
                    btnShokeitsuika.CssClass = "JC10GrayButton";
                    btnShokeitsuika.Enabled = true;
                    btn_delete.CssClass = "JC10GrayButton";
                    btn_delete.Enabled = true;
                    break;
                }
                else
                {
                    btnFukusuCopy.CssClass = "JC10DisableButton";
                    btnFukusuCopy.Enabled = false;
                    btnMidashitsuika.CssClass = "JC10DisableButton";
                    btnMidashitsuika.Enabled = false;
                    btnShokeitsuika.CssClass = "JC10DisableButton";
                    btnShokeitsuika.Enabled = false;
                    btn_delete.CssClass = "JC10DisableButton";
                    btn_delete.Enabled = false;
                }
            }
            updHeader.Update();
        }
        #endregion

        #region 見積状態を選択
        protected void btnJoutai_Click(object sender, EventArgs e)
        {
            HF_cJoutai.Value = lblcJoutai.Text;
            updHeader.Update();
            SessionUtility.SetSession("HOME", "Master");
            ifSentakuPopup.Style["width"] = "440px";
            ifSentakuPopup.Style["height"] = "450px";
            String id = Request.QueryString["id"];
            ifSentakuPopup.Src = "JC25_MitsuJyoutai.aspx?id=" + id;
            mpeSentakuPopup.Show();

            lblsJoutai.Attributes.Add("onClick", "BtnClick('MainContent_btnJoutai')");
            updSentakuPopup.Update();
        }
        #endregion

        #region btnJoutaiSelect_Click()  見積状態サブ画面を閉じる時のフォーカス処理
        protected void btnJoutaiSelect_Click(object sender, EventArgs e)
        {

            String cJoutai = "";
            if (Session["cJyotai"] != null)
            {
                cJoutai = (string)Session["cJyotai"];
                string sJoutai = (string)Session["sJyotai"];
                lblcJoutai.Text = cJoutai;
                lblsJoutai.Text = sJoutai;
                divJoutaibtn.Style["display"] = "none";
                divJoutaiLabel.Style["display"] = "block";
                updJoutai.Update();
            }

            HF_isChange.Value = "1";
            ifSentakuPopup.Src = "";
            mpeSentakuPopup.Hide();
            updSentakuPopup.Update();

            #region 見積状態
            String pre_cJoutai = HF_cJoutai.Value;
            flagJoutai = true;
            cJoutai = lblcJoutai.Text;
            if (pre_cJoutai == "04")   //見積作成中
            {
                if (cJoutai == "01")  //見積提出済
                {
                    if (String.IsNullOrEmpty(lbldMitumoriKakutei.Text))
                    {
                        String strDate = System.DateTime.Now.ToString("yyyy/MM/dd");
                        MitumoriKakuteiDateDataBind(strDate, btnMitumoriKakuteibi.ID);
                    }
                }
                else if (cJoutai == "02" || cJoutai == "06")  // 受注 or 売上済み
                {
                    if (String.IsNullOrEmpty(lbldJuuchu.Text))
                    {
                        String strDate = System.DateTime.Now.ToString("yyyy/MM/dd");
                        JuuchuuDateDataBind(strDate, btnJuuchuDate.ID);
                        if (String.IsNullOrEmpty(lbldMitumoriKakutei.Text))
                        {
                            MitumoriKakuteiDateDataBind(strDate, btnMitumoriKakuteibi.ID);
                        }
                    }
                }
            }
            else if (pre_cJoutai == "01") //見積提出済
            {
                if (cJoutai == "04")  //見積作成中
                {
                    flagJoutai = true;
                    btndMitumoriKakuteiCross_Click(sender, e);
                }
                else if (cJoutai == "02" || cJoutai == "06")  // 受注 or 売上済み
                {
                    if (String.IsNullOrEmpty(lbldJuuchu.Text))
                    {
                        String strDate = System.DateTime.Now.ToString("yyyy/MM/dd");
                        JuuchuuDateDataBind(strDate, btnJuuchuDate.ID);
                        if (String.IsNullOrEmpty(lbldMitumoriKakutei.Text))
                        {
                            MitumoriKakuteiDateDataBind(strDate, btnMitumoriKakuteibi.ID);
                        }
                    }
                }
            }
            else if (pre_cJoutai == "02") //受注
            {
                if (cJoutai == "04")  //見積作成中
                {
                    String id = Request.QueryString["id"];
                    DataTable dt_db = JC01Login_Class.Get_DB(id);
                    String db = dt_db.Rows[0]["db"].ToString();
                    String mail = dt_db.Rows[0]["mail"].ToString();
                    ConstantVal.DB_NAME = db;

                    JC_ClientConnecction_Class jc = new JC_ClientConnecction_Class();
                    jc.loginId = mail;
                    Boolean isShijishoExist = jc.IsExistShijishou(lblcMitumori.Text);
                    Boolean isUriageExist = jc.IsExistUriage(lblcMitumori.Text);
                    if (isShijishoExist || isUriageExist)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowJoutaiErrorMessage",
                                "ShowJoutaiErrorMessage('受注から見積作成中に変更できません。');", true);
                        lblcJoutai.Text = pre_cJoutai;
                        lblsJoutai.Text = "受注";
                        updJoutai.Update();
                    }
                    else
                    {
                        flagJoutai = true;
                        btndJuuchuCross_Click(sender, e);
                        btndMitumoriKakuteiCross_Click(sender, e);
                    }
                }
                else if (cJoutai == "01") //見積提出済
                {
                    String id = Request.QueryString["id"];
                    DataTable dt_db = JC01Login_Class.Get_DB(id);
                    String db = dt_db.Rows[0]["db"].ToString();
                    String mail = dt_db.Rows[0]["mail"].ToString();
                    ConstantVal.DB_NAME = db;

                    JC_ClientConnecction_Class jc = new JC_ClientConnecction_Class();
                    jc.loginId = mail;
                    Boolean isShijishoExist = jc.IsExistShijishou(lblcMitumori.Text);
                    Boolean isUriageExist = jc.IsExistUriage(lblcMitumori.Text);
                    if (isShijishoExist || isUriageExist)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowJoutaiErrorMessage",
                                "ShowJoutaiErrorMessage('受注から見積提出済に変更できません。');", true);
                        lblcJoutai.Text = pre_cJoutai;
                        lblsJoutai.Text = "受注";
                        updJoutai.Update();
                    }
                    else
                    {
                        btndJuuchuCross_Click(sender, e);
                        if (String.IsNullOrEmpty(lbldMitumoriKakutei.Text))
                        {
                            String strDate = System.DateTime.Now.ToString("yyyy/MM/dd");
                            MitumoriKakuteiDateDataBind(strDate, btnMitumoriKakuteibi.ID);
                        }
                    }
                }
            }
            else if (pre_cJoutai == "06") //売上済み
            {
                if (cJoutai == "04")  //見積作成中
                {
                    String id = Request.QueryString["id"];
                    DataTable dt_db = JC01Login_Class.Get_DB(id);
                    String db = dt_db.Rows[0]["db"].ToString();
                    String mail = dt_db.Rows[0]["mail"].ToString();
                    ConstantVal.DB_NAME = db;

                    JC_ClientConnecction_Class jc = new JC_ClientConnecction_Class();
                    jc.loginId = mail;
                    Boolean isShijishoExist = jc.IsExistShijishou(lblcMitumori.Text);
                    Boolean isUriageExist = jc.IsExistUriage(lblcMitumori.Text);
                    if (isShijishoExist || isUriageExist)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowJoutaiErrorMessage",
                                "ShowJoutaiErrorMessage('売上済みから見積作成中に変更できません。');", true);
                        lblcJoutai.Text = pre_cJoutai;
                        lblsJoutai.Text = "売上済み";
                        updJoutai.Update();
                    }
                    else
                    {
                        flagJoutai = true;
                        btndJuuchuCross_Click(sender, e);
                        btndMitumoriKakuteiCross_Click(sender, e);
                    }
                }
                else if (cJoutai == "01") //見積提出済
                {
                    String id = Request.QueryString["id"];
                    DataTable dt_db = JC01Login_Class.Get_DB(id);
                    String db = dt_db.Rows[0]["db"].ToString();
                    String mail = dt_db.Rows[0]["mail"].ToString();
                    ConstantVal.DB_NAME = db;

                    JC_ClientConnecction_Class jc = new JC_ClientConnecction_Class();
                    jc.loginId = mail;
                    Boolean isShijishoExist = jc.IsExistShijishou(lblcMitumori.Text);
                    Boolean isUriageExist = jc.IsExistUriage(lblcMitumori.Text);
                    if (isShijishoExist || isUriageExist)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowJoutaiErrorMessage",
                                "ShowJoutaiErrorMessage('売上済みから見積提出済に変更できません。');", true);
                        lblcJoutai.Text = pre_cJoutai;
                        lblsJoutai.Text = "売上済み";
                        updJoutai.Update();
                    }
                    else
                    {
                        btndJuuchuCross_Click(sender, e);
                        if (String.IsNullOrEmpty(lbldMitumoriKakutei.Text))
                        {
                            String strDate = System.DateTime.Now.ToString("yyyy/MM/dd");
                            MitumoriKakuteiDateDataBind(strDate, btnMitumoriKakuteibi.ID);
                        }
                    }
                }
                else if (cJoutai == "02") //受注
                {
                    String id = Request.QueryString["id"];
                    DataTable dt_db = JC01Login_Class.Get_DB(id);
                    String db = dt_db.Rows[0]["db"].ToString();
                    String mail = dt_db.Rows[0]["mail"].ToString();
                    ConstantVal.DB_NAME = db;

                    JC_ClientConnecction_Class jc = new JC_ClientConnecction_Class();
                    jc.loginId = mail;
                    Boolean isUriageExist = jc.IsExistUriage(lblcMitumori.Text);
                    if (isUriageExist)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowJoutaiErrorMessage",
                                "ShowJoutaiErrorMessage('売上済みから受注に変更できません。');", true);
                        lblcJoutai.Text = pre_cJoutai;
                        lblsJoutai.Text = "売上済み";
                        updJoutai.Update();
                    }
                    else
                    {
                        if (String.IsNullOrEmpty(lbldJuuchu.Text))
                        {
                            String strDate = System.DateTime.Now.ToString("yyyy/MM/dd");
                            JuuchuuDateDataBind(strDate, btnJuuchuDate.ID);
                            if (String.IsNullOrEmpty(lbldMitumoriKakutei.Text))
                            {
                                MitumoriKakuteiDateDataBind(strDate, btnMitumoriKakuteibi.ID);
                            }
                        }
                    }
                }
                else if (cJoutai == "00") //失注
                {
                    String id = Request.QueryString["id"];
                    DataTable dt_db = JC01Login_Class.Get_DB(id);
                    String db = dt_db.Rows[0]["db"].ToString();
                    String mail = dt_db.Rows[0]["mail"].ToString();
                    ConstantVal.DB_NAME = db;

                    JC_ClientConnecction_Class jc = new JC_ClientConnecction_Class();
                    jc.loginId = mail;
                    Boolean isUriageExist = jc.IsExistUriage(lblcMitumori.Text);
                    if (isUriageExist)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowJoutaiErrorMessage",
                                "ShowJoutaiErrorMessage('売上済みから失注に変更できません。');", true);
                        lblcJoutai.Text = pre_cJoutai;
                        lblsJoutai.Text = "売上済み";
                        updJoutai.Update();
                    }
                }
            }
            else if (pre_cJoutai == "00" || String.IsNullOrEmpty(pre_cJoutai)) //失注 or Empty
            {
                if (cJoutai == "01")  //見積提出済
                {
                    if (String.IsNullOrEmpty(lbldMitumoriKakutei.Text))
                    {
                        String strDate = System.DateTime.Now.ToString("yyyy/MM/dd");
                        MitumoriKakuteiDateDataBind(strDate, btnMitumoriKakuteibi.ID);
                    }
                }
                else if (cJoutai == "02") //受注
                {
                    if (String.IsNullOrEmpty(lbldJuuchu.Text))
                    {
                        String strDate = System.DateTime.Now.ToString("yyyy/MM/dd");
                        JuuchuuDateDataBind(strDate, btnJuuchuDate.ID);
                        if (String.IsNullOrEmpty(lbldMitumoriKakutei.Text))
                        {
                            MitumoriKakuteiDateDataBind(strDate, btnMitumoriKakuteibi.ID);
                        }
                    }
                }
                else if (cJoutai == "06")  //売上済み
                {
                    if (String.IsNullOrEmpty(lbldJuuchu.Text))
                    {
                        String strDate = System.DateTime.Now.ToString("yyyy/MM/dd");
                        JuuchuuDateDataBind(strDate, btnJuuchuDate.ID);
                        if (String.IsNullOrEmpty(lbldMitumoriKakutei.Text))
                        {
                            MitumoriKakuteiDateDataBind(strDate, btnMitumoriKakuteibi.ID);
                        }
                    }
                }
            }
            #endregion
            updHeader.Update();
        }
        #endregion

        #region 自社担当者を選択
        protected void BT_JisyaTantousya_Add_Click(object sender, EventArgs e)
        {
            SessionUtility.SetSession("HOME", "Master");
            Session["isKensaku"] = "false"; //担当者
            //ifSentakuPopup.Style["width"] = "715px";
            ifSentakuPopup.Style["width"] = "100vw";
            ifSentakuPopup.Style["height"] = "100vh";

            String id = Request.QueryString["id"];
            ifSentakuPopup.Src = "JC14TantouKensaku.aspx?id=" + id;
            mpeSentakuPopup.Show();

            lblsJISHATANTOUSHA.Attributes.Add("onClick", "BtnClick('MainContent_BT_JisyaTantousya_Add')");
            updSentakuPopup.Update();

        }
        #endregion

        #region 自社担当者を削除
        protected void BT_sJISHATANTOUSHA_Cross_Click(object sender, EventArgs e)
        {
            lblcJISHATANTOUSHA.Text = "";
            lblsJISHATANTOUSHA.Text = "";
            divTantousyaBtn.Style["display"] = "block";
            divTantousyaLabel.Style["display"] = "none";
            upd_JISHATANTOUSHA.Update();
            updHeader.Update();
        }
        #endregion

        #region btnJishaTantouSelect_Click　自社担当者サブ画面を閉じる時のフォーカス処理
        protected void btnJishaTantouSelect_Click(object sender, EventArgs e)
        {
            if (Session["JISHAcTANTOUSHA"] != null)
            {
                string ctantou = (string)Session["JISHAcTANTOUSHA"];
                string stantou = (string)Session["JISHAsTANTOUSHA"];
                if (!String.IsNullOrEmpty(ctantou))
                {
                    lblcJISHATANTOUSHA.Text = ctantou;
                    lblsJISHATANTOUSHA.Text = stantou.Replace("<", "&lt").Replace(">", "&gt");
                    divTantousyaBtn.Style["display"] = "none";
                    divTantousyaLabel.Style["display"] = "block";
                    upd_JISHATANTOUSHA.Update();
                }
                else
                {
                    BT_sJISHATANTOUSHA_Cross_Click(sender, e);
                }
            }
            HF_isChange.Value = "1";
            ifSentakuPopup.Src = "";
            mpeSentakuPopup.Hide();
            updSentakuPopup.Update();
            updHeader.Update();
        }
        #endregion

        #region グリッド行の並び替え
        protected void BT_Sort_Click(object sender, EventArgs e)
        {
            DataTable dt = GetGridViewData();
            if (ViewState["SyouhinTable"] != null)
            {
                var dr_copy = dt.NewRow();
                int before_index = Convert.ToInt32(HF_beforeSortIndex.Value) - 1;
                int after_index = Convert.ToInt32(HF_afterSortIndex.Value) - 1;
                DataRow dr = dt.Rows[before_index];
                dr_copy.ItemArray = dr.ItemArray.Clone() as object[];
                dt.Rows.RemoveAt(before_index);
                dt.Rows.InsertAt(dr_copy, after_index);

                var max = dt.AsEnumerable().Max(x => int.Parse(x.Field<string>("rowNo")));
                HF_maxRowNo.Value = max.ToString();

                dt = SetMidashiSyokei(dt);
                ViewState["SyouhinTable"] = dt;
                GV_MitumoriSyohin_Original.DataSource = dt;
                GV_MitumoriSyohin_Original.DataBind();

                DataTable dt_Syosai = GetSyosaiGridViewData();

                setSyosaiCount(dt, dt_Syosai);

                updMitsumoriSyohinGrid.Update();
                HF_isChange.Value = "1";
            }
            //DataTable dt_SyohinOriginal = GetGridViewData();
            DataTable dt_SyohinOriginal = new DataTable();
            dt_SyohinOriginal = dt;
            GV_MitumoriSyohin.DataSource = dt_SyohinOriginal;
            GV_MitumoriSyohin.DataBind();
            ViewState["SyouhinTable"] = dt;
            HasCheckRow();
            //SetSyosai();
            updMitsumoriSyohinGrid.Update();
            updHeader.Update();
        }
        #endregion

        #region btnCreateUriage_Click
        protected void btnCreateUriage_Click(object sender, EventArgs e)
        {
            if (lblcMitumori.Text != "")
            {
                HF_fBtn.Value = "btnCreateUriage";
                JC99NavBar.insatsusettei = false; // added by YG
                Session["fcopy"] = "false";// added by YG
                Session["uriageCode"] = "false"; //20220208 added by YG
                if (HF_isChange.Value == "1")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowAnkenChangeMessage",
                        "ShowKoumokuChangesConfirmMessage('項目が変更されています。保存しますか？','" + btnYes.ClientID + "','" + btnNo.ClientID + "','" + btnCancel.ClientID + "');", true);
                }
                else
                {
                    btnNo_Click(sender, e);
                }
            }
        }

        #endregion

        #region btnMitumoriSave_Click
        protected void btnMitumoriSave_Click(object sender, EventArgs e)
        {
            saveSuccess = false;
            checkData();
            if (HF_checkData.Value == "1")
            {
                if (TextUtility.isomojiCharacter(txtsMitumori.Text))
                {
                    f_isomoji_msg = false;
                }
                String sMitumori = txtsMitumori.Text.Replace("\\", "\\\\").Replace("'", "\\'");  //見積名

                if (TextUtility.isomojiCharacter(txtNouki.Text))
                {
                    f_isomoji_msg = false;
                }
                String sNouki = "null";  //納期
                if (!String.IsNullOrEmpty(txtNouki.Text))
                {
                    sNouki = "'" + txtNouki.Text.Replace("\\", "\\\\").Replace("'", "\\'") + "'";
                }
                String sYuukou = "null";
                if (!String.IsNullOrEmpty(lblsYuukou.Text))
                {
                    sYuukou = "'" + lblsYuukou.Text.Replace("&lt", "<").Replace("&gt", ">") + "'";
                }
                String cShiharai = "'00'";
                if (!String.IsNullOrEmpty(lblcShiharai.Text))
                {
                    cShiharai = "'" + lblcShiharai.Text + "'";
                }

                if (TextUtility.isomojiCharacter(txtUkewatashibasho.Text))
                {
                    f_isomoji_msg = false;
                }
                String sUkewatashi = "null";  //受渡場所
                if (!String.IsNullOrEmpty(txtUkewatashibasho.Text))
                {
                    sUkewatashi = "'"+txtUkewatashibasho.Text.Replace("\\", "\\\\").Replace("'", "\\'")+"'";
                }

                if (TextUtility.isomojiCharacter(txtShanaiMemo.Text))
                {
                    f_isomoji_msg = false;
                }
                String sMemo = "null";
                if (!String.IsNullOrEmpty(txtShanaiMemo.Text))
                {
                    sMemo = "'" + txtShanaiMemo.Text.Replace("\\", "\\\\").Replace("'", "\\'") + "'";  //社内メモ
                }

                if (TextUtility.isomojiCharacter(txtBikou.Text))
                {
                    f_isomoji_msg = false;
                }
                String sBikou = "null";
                if (!String.IsNullOrEmpty(txtBikou.Text))
                {
                    sBikou = "'"+txtBikou.Text.Replace("\\", "\\\\").Replace("'", "\\'")+"'";  //見積書備考
                }
                String cKakudo = "null";
                if (!String.IsNullOrEmpty(DDL_Kakudo.SelectedValue))
                {
                    cKakudo = "'" + DDL_Kakudo.SelectedValue + "'";
                }
                String Jisyabango = "null";
                if (!String.IsNullOrEmpty(txtJisyaBango.Text))
                {
                    Jisyabango = "'" + txtJisyaBango.Text + "'";
                }
                String jishaTantousya = "null";
                if (!String.IsNullOrEmpty(lblcJISHATANTOUSHA.Text))
                {
                    jishaTantousya = "'" + lblcJISHATANTOUSHA.Text + "'";
                }

                if (f_isomoji_msg == false)
                {
                    string msg = "使用不可能なテキスト（環境依存文字）が入力され保存できません。</br>文字化けの原因となるため、下記の文字を修正してください。</br>" + " 対象文字：「" + TextUtility.invalidtext_all + "」";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowAnkenChangeMessage",
                                "ShowMojiMessage('" + msg + "','" + btnmojiOK.ClientID + "');", true);
                    f_isomoji_msg = true;
                    Session["moji"] = "true";
                }
                else
                {
                    String id = Request.QueryString["id"];
                    DataTable dt_db = JC01Login_Class.Get_DB(id);
                    String db = dt_db.Rows[0]["db"].ToString();
                    String mail = dt_db.Rows[0]["mail"].ToString();
                    ConstantVal.DB_NAME = db;

                    JC_ClientConnecction_Class jc = new JC_ClientConnecction_Class();
                    jc.loginId = mail;
                    con = jc.GetConnection();

                    #region getnJUNBAN
                    string junban_sql = "select max(nJUNBAN) + 1 as nJUNBAN from h_mitumori where cMITUMORI = '" + lblcMitumori.Text + "'";
                    MySqlCommand cm = new MySqlCommand(junban_sql, con);
                    cm.CommandTimeout = 0;
                    con.Close();
                    con.Open();
                    MySqlDataReader mdr = cm.ExecuteReader();
                    string junban = "";
                    if (mdr.HasRows)
                    {
                        while (mdr.Read())
                        {
                            junban = mdr[0].ToString();
                        }
                        mdr.Close();
                    }
                    con.Close();
                    #endregion

                    getTOKUISAKI_INFO();
                    getTOKUISAKI_Tantou(lblcTOKUISAKI.Text);
                    getSEIKYU_Tantou(lblcSEIKYUSAKI.Text);
                    String dUriageYotei = "null";
                    if (!String.IsNullOrEmpty(lbldUriageYotei.Text))
                    {
                        dUriageYotei = "'" + lbldUriageYotei.Text + "'";
                    }
                    String dJuuchuu = "null";
                    if (!String.IsNullOrEmpty(lbldJuuchu.Text))
                    {
                        dJuuchuu = "'" + lbldJuuchu.Text + "'";
                    }

                    String dJuuchuuYotei = "null";
                    if (!String.IsNullOrEmpty(lbldJuuchuuYotei.Text))
                    {
                        dJuuchuuYotei = "'" + lbldJuuchuuYotei.Text + "'";
                    }

                    String dKanryoYotei = "null";
                    if (!String.IsNullOrEmpty(lbldshuryoYotei.Text))
                    {
                        dKanryoYotei = "'" + lbldshuryoYotei.Text + "'";
                    }

                    String dKakutei = "null";
                    if (!String.IsNullOrEmpty(lbldMitumoriKakutei.Text))
                    {
                        dKakutei = "'" + lbldMitumoriKakutei.Text + "'";
                    }

                    String nSiire_G = "0";
                    if (!String.IsNullOrEmpty(lblnSIIRE_G.Text))
                    {
                        nSiire_G = lblnSIIRE_G.Text;
                    }

                    String nNebiki = "0";
                    SetGokeiKingakuHyoji();
                    if (!CHK_KingakuNotDisplay.Checked)
                    {
                        if (!String.IsNullOrEmpty(txtShusseiNebiki.Text))
                        {
                            nNebiki = txtShusseiNebiki.Text.Replace(",", "");
                        }
                    }

                    String nJuuchuKingaku = "0";
                    if (!String.IsNullOrEmpty(txtJuuchuKingaku.Text))
                    {
                        nJuuchuKingaku = txtJuuchuKingaku.Text.Replace(",", "");
                    }


                    String sTokuisaki_Tan = "null";
                    String sTokuisaki_tan_Jun = "0";
                    if (!String.IsNullOrEmpty(lblsTOKUISAKI_TAN.Text))
                    {
                        sTokuisaki_Tan = lblsTOKUISAKI_TAN.Text;
                        sTokuisaki_Tan = "'" + sTokuisaki_Tan.Replace("&lt", "<").Replace("&gt", ">") + "'";

                        if (!String.IsNullOrEmpty(lblsTOKUISAKI_TAN_JUN.Text))
                        {
                            sTokuisaki_tan_Jun = lblsTOKUISAKI_TAN_JUN.Text;
                        }

                    }

                    String sSeikyusaki_Tan = "null";
                    String sSeikyu_tan_Jun = "0";
                    if (!String.IsNullOrEmpty(lblsSEIKYUSAKI_TAN.Text))
                    {
                        sSeikyusaki_Tan = lblsSEIKYUSAKI_TAN.Text;
                        sSeikyusaki_Tan = "'" + sSeikyusaki_Tan.Replace("&lt", "<").Replace("&gt", ">") + "'";

                        if (!String.IsNullOrEmpty(lblsSEIKYUSAKI_TAN_JUN.Text))
                        {
                            sSeikyu_tan_Jun = lblsSEIKYUSAKI_TAN_JUN.Text;
                        }
                    }
                    String sTokuisaki = lblsTOKUISAKI.Text;
                    sTokuisaki = sTokuisaki.Replace("&lt", "<").Replace("&gt", ">");


                    String sSeikyusaki = lblsSEIKYUSAKI.Text;
                    sSeikyusaki = sSeikyusaki.Replace("&lt", "<").Replace("&gt", ">");



                    if (lblcMitumori.Text == "")  //新規登録
                    {
                        #region getcMITUMORI
                        string cMITUMORI = "";
                        string mitumoriCode = " select ifnull(MAX(cMITUMORI),0)+1 as cMITUMORI  from r_mitumori; ";
                        MySqlCommand cmd = new MySqlCommand(mitumoriCode, con);
                        cmd.CommandTimeout = 0;
                        con.Close();
                        con.Open();
                        MySqlDataReader dr = cmd.ExecuteReader();

                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                cMITUMORI = dr[0].ToString();
                            }
                            dr.Close();
                        }

                        cMITUMORI = cMITUMORI.ToString().PadLeft(10, '0');
                        lblcMitumori.Text = cMITUMORI;
                        con.Close();
                        #endregion

                        string insertquery = "";
                        if (String.IsNullOrEmpty(lnkcBukken.Text))
                        {
                            #region getcBUKKEN
                            string cBUKKEN = "";
                            string bukkenCode = " select ifnull(MAX(cBUKKEN),0)+1 as cBUKKEN  from R_BUKKEN; ";
                            MySqlCommand cmd1 = new MySqlCommand(bukkenCode, con);
                            cmd1.CommandTimeout = 0;
                            con.Close();
                            con.Open();
                            MySqlDataReader dr1 = cmd1.ExecuteReader();

                            if (dr1.HasRows)
                            {
                                while (dr1.Read())
                                {
                                    cBUKKEN = dr1[0].ToString();
                                }
                                dr1.Close();
                            }

                            cBUKKEN = cBUKKEN.ToString().PadLeft(10, '0');
                            lnkcBukken.Text = cBUKKEN;
                            con.Close();
                            #endregion

                            #region Insert r_bukken,h_bukken
                            insertquery += "Insert Into r_bukken(cBUKKEN, sBUKKEN, sBIKOU, cTANTOUSHA, cTOKUISAKI," +
                                    " sTOKUISAKI, sTOKUISAKIJUSYO1, sTOKUISAKIJUSYO2," +
                                    "  sTOKUISAKITEL, sTOKUISAKIFAX,sTOKUISAKIYUBIN, sTOKUISAKI_TAN, sTOKUISAKI_TAN_Jun, fSAMA, sYUUKOUKI,dBUKKEN, " +
                                    "   cCREATE_TAN, dCREATE, dHENKOU, cHENKOUSYA,cJYOUTAI, cBukkenJYOUTAI, cRANK, fHYOUJI, nMORIARARI,nURIAGE," +
                                    "   cFILE, cPJ, sTOKUISAKI_TANBUMON, sTOKUISAKI_YAKUSHOKU,fJikaitenken, fJikaiokugai, sTOKUISAKI_KEISYO," +
                                    "   fHAITA, cSHIYOUSYA,dNOUKI, dJikaitenken, dJikaiokugai) " +
                                    " Values('" + cBUKKEN + "', '" + sMitumori + "', '', " + jishaTantousya + ", '" + lblcTOKUISAKI.Text + "', " +
                                    "'" + sTokuisaki + "', " + sTOKUISAKIJUSYO1 + ", " + sTOKUISAKIJUSYO2 + ", " + sTOKUISAKITEL + ", " + sTOKUISAKIFAX + ", " +
                                    "" + sTOKUISAKIYUBIN + ", " + sTokuisaki_Tan + ", '" + sTokuisaki_tan_Jun + "', '0', '', " +
                                    "Now() , '" + lblLoginUserCode.Text + "', Now() , Now() , '" + lblLoginUserCode.Text + "'," +
                                    " '01', '00', '00', '0', 0," +
                                    " 0, '', '', " + sTOKUISAKI_sBUMON + ", " + sTOKUISAKI_SYAKUSHOKU + "," +
                                    " '0', '0', " + sTOKUISAKI_SKEISHOU + ",'',''," +
                                    "Now(),Now(),Now());";

                            insertquery += " Insert Into h_bukken( cBUKKEN,nJUNBAN,sNAIYOU,dHENKOU,cHENKOUSYA ) Values('" + cBUKKEN + "','1','', Now() ,'" + lblLoginUserCode.Text + "'); ";
                            #endregion
                        }

                        #region Insert r_mitumori,r_bu_mitsu,h_mitumori
                        insertquery += "INSERT INTO r_mitumori (cMITUMORI, cMITUMORI_KO, cTOKUISAKI, sTOKUISAKI, sTOKUISAKITEL, sTOKUISAKIFAX," +
                            " sTOKUISAKIYUBIN, sTOKUISAKI_TAN, sTOKUISAKI_TAN_Jun, cEIGYOTANTOSYA, sMITUMORI, sBIKOU," +
                            " sMITUMORINOKI, sMITUMORIYUKOKIGEN, cSHIHARAI, nKINGAKU, nMITUMORIARARI, nMITUMORINEBIKI," +
                            " nMITUMORISYOHIZE, nHENKOU, dHENKOU, cHENKOUSYA, nKINGAKU_G, nMITUMORINEBIKI_G," +
                            " nTANKA_G, nKIRI_G, nSIIRE_G, cJYOTAI_MITUMORI, fHYOUJI, fYOUKOU," +
                            " cSHIHARAI_STOKUI, sSHIHARAI_STOKUI, sTOKUISAKIJUSYO, dURIAGEYOTEI, dYOTEINOUKI, sMEMO," +
                            " sTOKUISAKI_KEISYO, sTOKUISAKI_YAKUSHOKU, cSEIKYUSAKI, sSEIKYUSAKI, cSEIKYUSAKI_TKC," +
                            " cSEIKYU_YUUBIN, sSEIKYU_JUUSHO1, sSEIKYU_JUUSHO2, sSEIKYU_TEL, sSEIKYU_FAX," +
                            " sSEIKYU_TAN, sSEIKYU_TAN_Jun, sSEIKYU_TANBUMON, sSEIKYU_KEISYO, sSEIKYU_YAKUSYOKU, " +
                            " cSHIHARAI_SEIKYU, sSHIMEBI, sSEIKYU_BIKOU, sSEIKYU_SHIHARAIBI, sSEIKYU_SHIHARAIGETU," +
                            " sUKEWATASIBASYO, cPJ, cKAKUDO, YU_dURIYOTEI, dKANRYOUYOTEI,dKETSUTEI, nURIAGEKINGAKU," +
                            " cKYOTEN, sTOKUISAKI_TANBUMON, nKAZEIKINGAKU, nRIIRITSU, dMITUMORISAKUSEI, cSAKUSEISYA," +
                            " sKOUMOKU1, sKOUMOKU2, sKOUMOKU3, sKOUMOKU4, fSAMA, nTOKUISAKIKAKERITU)" +
                            " VALUES('" + lblcMitumori.Text + "', '"+lblcMitumori_Ko.Text+"', '" + lblcTOKUISAKI.Text + "', '" + sTokuisaki + "', " + sTOKUISAKITEL + ", " + sTOKUISAKIFAX + "," +
                            " " + sTOKUISAKIYUBIN + ", " + sTokuisaki_Tan + ", '" + sTokuisaki_tan_Jun + "', " + jishaTantousya + ", '" + sMitumori + "', " + sBikou + "," +
                            " " + sNouki + ", " + sYuukou + ", " + cShiharai + ", '" + lblKingaku.Text.Replace(",", "") + "', '" + lblArari.Text.Replace(",", "") + "', '" + nNebiki + "'," +
                            " '" + lblShohizei.Text.Replace(",", "") + "', '0', Now(), '" + lblLoginUserCode.Text + "', '" + lblGokeiKingaku.Text.Replace(",", "") + "', '" + nNebiki + "'," +
                            " '" + lblTeikaGokei.Text.Replace(",", "") + "', '" + lblTeikaGokei.Text.Replace(",", "") + "', '" + nSiire_G + "', '" + lblcJoutai.Text + "', '0', '1'," +
                            " " + sTOKUISAKI_cSHIHARAI + ", " + sTOKUISAKI_sSHIHARAI + ", " + sTOKUISAKIJUSYO + ", " + dUriageYotei + ", " + dJuuchuu + ", " + sMemo + "," +
                            " " + sTOKUISAKI_SKEISHOU + ", " + sTOKUISAKI_SYAKUSHOKU + ", '" + lblcSEIKYUSAKI.Text + "', '" + sSeikyusaki + "', " + sSEIKYU_TKC + "," +
                            " " + sSEIKYU_YUBIN + ", " + sSEIKYU_JUSYO1 + ", " + sSEIKYU_JUSYO2 + ", " + sSEIKYU_TEL + ", " + sSEIKYU_FAX + "," +
                            " " + sSeikyusaki_Tan + ", '" + sSeikyu_tan_Jun + "', " + sSEIKYU_sBUMON + ", " + sSEIKYU_SKEISHOU + ", " + sSEIKYU_SYAKUSHOKU + "," +
                            " " + sSEIKYU_cSHIHARAI + ", " + sSEIKYU_SHIMEBI + ", " + sSEIKYU_BIKO + ", " + sSEIKYU_SHIHARAIBI + ", " + sSEIKYU_sSHIHARAIGetsu + "," +
                            " " + sUkewatashi + ", " + Jisyabango + ", " + cKakudo + ", " + dJuuchuuYotei + ", " + dKanryoYotei + ", " + dKakutei + ", '" + nJuuchuKingaku + "', " +
                            " '" + lblcKYOTEN.Text + "', " + sTOKUISAKI_sBUMON + ", '" + lblKingaku.Text.Replace(",", "") + "', '" + lblArariRitsu.Text.Replace(",", "").Replace("%", "") + "', '" + lbldMitumori.Text + "', '" + lblLoginUserCode.Text + "', " +
                            " null, null, null, null,'0', "+ nTOKUISAKIKAKERITU + ");";

                        insertquery += "Insert Into r_bu_mitsu(cBUKKEN,cMITUMORI,dHENKOU,cHENKOUSYA)Values('" + lnkcBukken.Text + "','" + lblcMitumori.Text + "',Now(),'" + lblLoginUserCode.Text + "');";
                        insertquery += "INSERT INTO h_mitumori (cMITUMORI, cMITUMORI_KO, nJUNBAN, sNAIYOU, dHENKOU, cHENKOUSYA) VALUES ('" + lblcMitumori.Text + "', '"+lblcMitumori_Ko.Text+"', '1', '新規入力', Now(), '" + lblLoginUserCode.Text + "'); ";
                        insertquery += "Delete From r_mitumori_m where cMITUMORI='" + lblcMitumori.Text + "';";
                        #endregion
                        String insert_sql = "";
                        int rowNo = 1;
                        String insert_sql1 = "";

                        int maxRowNo = Convert.ToInt32(HF_maxRowNo.Value);
                        maxRowNo += 1;

                        #region　r_mitumori_m
                        foreach (GridViewRow row in GV_MitumoriSyohin_Original.Rows)
                        {
                            Label lbl_status = (row.FindControl("lblhdnStatus") as Label);
                            TextBox txt_cSyohin = (row.FindControl("txtcSYOHIN") as TextBox);
                            TextBox txt_sSyohin = (row.FindControl("txtsSYOHIN") as TextBox);
                            TextBox txt_nSyoryo = (row.FindControl("txtnSURYO") as TextBox);
                            //DropDownList ddl_cTani = (row.FindControl("DDL_cTANI") as DropDownList);
                            TextBox txt_cTani = (row.FindControl("txtTani") as TextBox);
                            TextBox txt_nTanka = (row.FindControl("txtnTANKA") as TextBox);
                            Label lbl_TankaGokei = (row.FindControl("lblTankaGokei") as Label);
                            TextBox txt_nGenkaTanka = (row.FindControl("txtnGENKATANKA") as TextBox);
                            Label lbl_GenkaGokei = (row.FindControl("lblGenkaGokei") as Label);
                            Label lbl_Arari = (row.FindControl("lblnARARI") as Label);
                            Label lbl_ArariSu = (row.FindControl("lblnARARISu") as Label);
                            Label lbl_RowNo = (row.FindControl("lblRowNo") as Label);
                            Label lbl_fgentankatanka = (row.FindControl("lblfgenkatanka") as Label);
                            Button btn_Syosai = (row.FindControl("btnSyohinShosai") as Button);
                            Label lbl_sKubun = (row.FindControl("lblKubun") as Label);
                            Label lbl_nSIKIRITANKA = (row.FindControl("lblTanka") as Label);
                            TextBox txt_nRITU = (row.FindControl("txtnRITU") as TextBox);
                            Label lbl_fJITAIS = (row.FindControl("lblfjitais") as Label);
                            Label lbl_fJITAIQ = (row.FindControl("lblfjitaiq") as Label);

                            if (TextUtility.isomojiCharacter(txt_sSyohin.Text))
                            {
                                f_isomoji_msg = false;
                                break;
                            }
                            String sSyouhin = txt_sSyohin.Text.Replace("\\", "\\\\").Replace("'", "\\'");  //明細商品名

                            if (TextUtility.isomojiCharacter(txt_cTani.Text))
                            {
                                f_isomoji_msg = false;
                                break;
                            }
                            String sTani = txt_cTani.Text.Replace("\\", "\\\\").Replace("'", "\\'");  //明細単位

                            String ntanka = "0";
                            if (!String.IsNullOrEmpty(txt_nTanka.Text))
                            {
                                ntanka = txt_nTanka.Text.Replace(",", "");
                            }

                            String nsikiritanka = "0";
                            if (!String.IsNullOrEmpty(lbl_nSIKIRITANKA.Text))
                            {
                                nsikiritanka = lbl_nSIKIRITANKA.Text.Replace(",", "");
                            }

                            String ntankagokei = "0";
                            if (!String.IsNullOrEmpty(lbl_TankaGokei.Text))
                            {
                                ntankagokei = lbl_TankaGokei.Text.Replace(",", "");
                            }

                            String ngenka = "0";
                            if (!String.IsNullOrEmpty(txt_nGenkaTanka.Text))
                            {
                                ngenka = txt_nGenkaTanka.Text.Replace(",", "");
                            }

                            String ngenkagokei = "0";
                            if (!String.IsNullOrEmpty(lbl_GenkaGokei.Text))
                            {
                                ngenkagokei = lbl_GenkaGokei.Text.Replace(",", "");
                            }

                            String nsuryo = "0";
                            if (!String.IsNullOrEmpty(txt_nSyoryo.Text))
                            {
                                nsuryo = txt_nSyoryo.Text.Replace(",", "");
                            }

                            String fCheck = "";
                            if (btn_Syosai.Text != "詳")
                            {
                                fCheck = "2";
                            }

                            if (!String.IsNullOrEmpty(txt_cSyohin.Text) || !String.IsNullOrEmpty(txt_sSyohin.Text) ||
                                !String.IsNullOrEmpty(txt_nSyoryo.Text) || !String.IsNullOrEmpty(txt_cTani.Text) ||
                                !String.IsNullOrEmpty(txt_nTanka.Text) || !String.IsNullOrEmpty(lbl_TankaGokei.Text) ||
                                !String.IsNullOrEmpty(txt_nGenkaTanka.Text) || !String.IsNullOrEmpty(lbl_GenkaGokei.Text))
                            {
                                insert_sql += insert_sql1 + "Insert Into"
                                    + " r_mitumori_m(cMITUMORI, cMITUMORI_KO, nGYOUNO, cSYOUHIN, sSYOUHIN_R,"
                                    + " nTANKA, nSURYO, sTANI, nSIIRETANKA, nKINGAKU,"
                                    + " nRITU, dHENKOU, cHENKOUSYA, cSHIIRESAKI, sSHIIRESAKI,"
                                    + " nSIIREKINGAKU, nSIKIRITANKA, nSIKIRIKINGAKU, nINSATSU_GYO, cSYOUSAI,"
                                    + " rowNO, sSETSUMUI, fJITAIS, fJITAIQ, sMEMO,"
                                    + " fCHECK, fgentankatanka, sKUBUN)"
                                    + " Values('" + lblcMitumori.Text + "', '" + lblcMitumori_Ko.Text + "', " + rowNo.ToString() + ", '" + txt_cSyohin.Text + "', '" + sSyouhin + "',"
                                    + " " + ntanka + ", " + nsuryo + ", '" + sTani + "', " + ngenka + ", " + ntankagokei + ","
                                    + " '" + txt_nRITU.Text.Replace("%", "") + "', Now(), '" + lblLoginUserCode.Text + "', '', '',"
                                    + " " + ngenkagokei + ", " + nsikiritanka + ", " + ntankagokei + ", 0, '',"
                                    + " " + lbl_RowNo.Text + ", '', '"+lbl_fJITAIS.Text+"', '"+lbl_fJITAIQ.Text+"', '', '" + fCheck + "', '" + lbl_fgentankatanka.Text + "', '" + lbl_sKubun.Text + "');";
                                insert_sql1 = "";
                            }
                            else
                            {
                                String rNo = lbl_RowNo.Text;
                                if (rNo == "0" || String.IsNullOrEmpty(rNo))
                                {
                                    rNo = maxRowNo.ToString();
                                    maxRowNo += 1;
                                }
                                insert_sql1 += "Insert Into"
                                    + " r_mitumori_m(cMITUMORI, cMITUMORI_KO, nGYOUNO, cSYOUHIN, sSYOUHIN_R,"
                                    + " nTANKA, nSURYO, sTANI, nSIIRETANKA, nKINGAKU,"
                                    + " nRITU, dHENKOU, cHENKOUSYA, cSHIIRESAKI, sSHIIRESAKI,"
                                    + " nSIIREKINGAKU, nSIKIRITANKA, nSIKIRIKINGAKU, nINSATSU_GYO, cSYOUSAI,"
                                    + " rowNO, sSETSUMUI, fJITAIS, fJITAIQ, sMEMO,"
                                    + " fCHECK, fgentankatanka, sKUBUN)"
                                    + " Values('" + lblcMitumori.Text + "', '" + lblcMitumori_Ko.Text + "', " + rowNo.ToString() + ", '" + txt_cSyohin.Text + "', '" + sSyouhin + "',"
                                    + " " + ntanka + ", " + nsuryo + ", '" + sTani + "', " + ngenka + ", " + ntankagokei + ","
                                    + " 100, Now(), '" + lblLoginUserCode.Text + "', '', '',"
                                    + " " + ngenkagokei + ", " + nsikiritanka + ", " + ntankagokei + ", 0, '',"
                                    + " " + rNo + ", '', '0', '0', '', '" + fCheck + "', '" + lbl_fgentankatanka.Text + "', '" + lbl_sKubun.Text + "');";
                            }
                            rowNo++;
                        }
                        #endregion

                        if (f_isomoji_msg == false)
                        {
                            string msg = "使用不可能なテキスト（環境依存文字）が入力され保存できません。</br>文字化けの原因となるため、下記の文字を修正してください。</br>" + " 対象文字：「" + TextUtility.invalidtext_all + "」";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowAnkenChangeMessage",
                                        "ShowMojiMessage('" + msg + "','" + btnmojiOK.ClientID + "');", true);
                            f_isomoji_msg = true;
                            Session["moji"] = "true";
                        }
                        else
                        {

                            insertquery += insert_sql;

                            insertquery += "Delete From r_mitumori_m2 where cMITUMORI='" + lblcMitumori.Text + "';";

                            String insert_syosai_sql = "";
                            int gyoNo = 1;

                            #region r_mitumori_m2
                            foreach (GridViewRow row in GV_Syosai.Rows)
                            {
                                Label lbl_status = (row.FindControl("lblhdnStatus") as Label);
                                Label lbl_rowno = (row.FindControl("lblRowNo") as Label);
                                TextBox txt_cSyohin = (row.FindControl("txtcSYOHIN") as TextBox);
                                TextBox txt_sSyohin = (row.FindControl("txtsSYOHIN") as TextBox);
                                TextBox txt_nSyoryo = (row.FindControl("txtnSURYO") as TextBox);
                                Label lbl_cTani = (row.FindControl("lblcTANI") as Label);
                                TextBox txt_nTanka = (row.FindControl("txtnTANKA") as TextBox);
                                Label lbl_TankaGokei = (row.FindControl("lblTankaGokei") as Label);
                                TextBox txt_nGenkaTanka = (row.FindControl("txtnGENKATANKA") as TextBox);
                                Label lbl_GenkaGokei = (row.FindControl("lblGenkaGokei") as Label);
                                Label lbl_Arari = (row.FindControl("lblnARARI") as Label);
                                Label lbl_ArariSu = (row.FindControl("lblnARARISu") as Label);
                                TextBox txt_nRITU = (row.FindControl("txtnRITU") as TextBox); //20220517 MyatNoe
                                Label lbl_nSIKIRITANKA = (row.FindControl("lblTanka") as Label);
                                Label lbl_fJITAIS = (row.FindControl("lblfjitais") as Label);

                                if (TextUtility.isomojiCharacter(txt_sSyohin.Text))
                                {
                                    f_isomoji_msg = false;
                                    break;
                                }
                                String sSyouhin = txt_sSyohin.Text.Replace("\\", "\\\\").Replace("'", "\\'");  //詳細商品名

                                if (TextUtility.isomojiCharacter(lbl_cTani.Text))
                                {
                                    f_isomoji_msg = false;
                                    break;
                                }
                                String sTani = lbl_cTani.Text.Replace("\\", "\\\\").Replace("'", "\\'");  //詳細単位

                                String ntanka = "0";
                                if (!String.IsNullOrEmpty(txt_nTanka.Text))
                                {
                                    ntanka = txt_nTanka.Text.Replace(",", "");
                                }

                                String nsuryo = "0";
                                if (!String.IsNullOrEmpty(txt_nSyoryo.Text))
                                {
                                    nsuryo = txt_nSyoryo.Text.Replace(",", "");
                                }

                                String ngenka = "0";
                                if (!String.IsNullOrEmpty(txt_nGenkaTanka.Text))
                                {
                                    ngenka = txt_nGenkaTanka.Text.Replace(",", "");
                                }

                                String ntankagokei = "0";
                                if (!String.IsNullOrEmpty(lbl_TankaGokei.Text))
                                {
                                    ntankagokei = lbl_TankaGokei.Text.Replace(",", "");
                                }

                                String nSikiriTanka = "0";
                                if (!String.IsNullOrEmpty(lbl_nSIKIRITANKA.Text))
                                {
                                    nSikiriTanka = lbl_nSIKIRITANKA.Text.Replace(",", "");
                                }

                                String ngenkagokei = "0";
                                if (!String.IsNullOrEmpty(lbl_GenkaGokei.Text))
                                {
                                    ngenkagokei = lbl_GenkaGokei.Text.Replace(",", "");
                                }
                                //String fJitais = "0";
                                //if (String.IsNullOrEmpty(txt_cSyohin.Text))
                                //{
                                //    fJitais = "1";
                                //}

                                insert_syosai_sql += "INSERT INTO r_mitumori_m2 (cMITUMORI, cMITUMORI_KO, nGYOUNO, cSYOUHIN, sSYOUHIN_R," +
                                    " nTANKA, nSURYO, sTANI, nSIIRETANKA, nKINGAKU," +
                                    " nRITU, dHENKOU, cHENKOUSYA, nSIIREKINGAKU, nSIKIRITANKA," +
                                    " nSIKIRIKINGAKU, nINSATSU_GYO, sSETSUMUI, fJITAIS, fJITAIQ," +
                                    " rowNO, rowNO2, sMEMO, fCHECK)" +
                                    " VALUES('" + lblcMitumori.Text + "', '" + lblcMitumori_Ko.Text + "', '" + gyoNo + "', '" + txt_cSyohin.Text + "', '" + sSyouhin + "'," +
                                    " '" + ntanka + "', '" + nsuryo + "', '" + sTani + "', '" + ngenka + "', '" + ntankagokei + "'," +
                                    " '"+ txt_nRITU.Text.Replace("%", "") + "', Now(), '" + lblLoginUserCode.Text + "', '" + ngenkagokei + "', '" + nSikiriTanka + "'," +
                                    " '" + ntankagokei + "', '0', '" + gyoNo + "', '" + lbl_fJITAIS.Text + "', '0'," +
                                    " '" + lbl_rowno.Text + "', '" + gyoNo + "', '', '');";

                                gyoNo += 1;
                            }
                            #endregion

                            if (f_isomoji_msg == false)
                            {
                                string msg = "使用不可能なテキスト（環境依存文字）が入力され保存できません。</br>文字化けの原因となるため、下記の文字を修正してください。</br>" + " 対象文字：「" + TextUtility.invalidtext_all + "」";
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowAnkenChangeMessage",
                                            "ShowMojiMessage('" + msg + "','" + btnmojiOK.ClientID + "');", true);
                                f_isomoji_msg = true;
                                Session["moji"] = "true";
                            }
                            else
                            {

                                insertquery += insert_syosai_sql;

                                MySqlTransaction tr = null;
                                MySqlCommand cmdUpdate = new MySqlCommand();
                                con.Open();
                                try
                                {
                                    tr = con.BeginTransaction();
                                    cmdUpdate.Transaction = tr;
                                    cmdUpdate.CommandTimeout = 0;
                                    cmdUpdate = new MySqlCommand(insertquery, con);
                                    cmdUpdate.ExecuteNonQuery();
                                    tr.Commit();

                                    divLabelSave.Style["display"] = "flex";//「保存しました。」メッセージを表示                                                                                                                                      
                                    updLabelSave.Update();

                                    btnBetsuMitumoriSave.Visible = true;
                                    btnMitumoriDelete.Visible = true;
                                    btnMitumorishoPDF.Enabled = true;
                                    btnMitumorishoPDF.CssClass = "BlueBackgroundButton JC10SaveBtn";
                                    //btnCreateUriage.Enabled = true;
                                    //btnCreateUriage.CssClass = "BlueBackgroundButton JC10SaveBtn";
                                    //btnUriage.Enabled = true;
                                    //btnUriage.CssClass = "BlueBackgroundButton JC10SaveBtn";
                                    lblSakuseisya.Text = lblLoginUserName.Text;
                                    lblHenkousya.Text = lblLoginUserName.Text;
                                    lblcSakuseisya.Text = lblLoginUserCode.Text;
                                    lblcHenkousya.Text = lblLoginUserCode.Text;
                                    lblSakusekibi.Text = DateTime.Now.ToString("yyyy/MM/dd");
                                    lblHenkoubi.Text = DateTime.Now.ToString("yyyy/MM/dd");
                                    lblnHenkou.Text = "0";
                                    lblcMitumori_Ko.Text = (Convert.ToDouble(lblcMitumori_Ko.Text) + 1).ToString().PadLeft(2, '0');
                                    saveSuccess = true;
                                    HF_isChange.Value = "0";
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
                            }
                        }
                    }
                    else//更新
                    {
                        int nHenkou = Convert.ToInt32(lblnHenkou.Text);
                        #region update r_mitumori
                        string update_sql = " Update r_mitumori"
                            + " Set cMITUMORI = '" + lblcMitumori.Text + "',"
                            + " cMITUMORI_KO = '" + lblcMitumori_Ko.Text + "',"
                            + " cTOKUISAKI = '" + lblcTOKUISAKI.Text + "',"
                            + " sTOKUISAKI = '" + sTokuisaki + "',"
                            + " sTOKUISAKITEL = " + sTOKUISAKITEL + ","
                            + " sTOKUISAKIFAX = " + sTOKUISAKIFAX + ","
                            + " sTOKUISAKIYUBIN = " + sTOKUISAKIYUBIN + ","
                            + " sTOKUISAKI_TAN = " + sTokuisaki_Tan + ","
                            + " sTOKUISAKI_TAN_Jun = '" + sTokuisaki_tan_Jun + "',"
                            + " cEIGYOTANTOSYA = " + jishaTantousya + ","
                            + " sMITUMORI = '" + sMitumori + "',"
                            + " sBIKOU = " + sBikou + ","
                            + " sMITUMORINOKI = " + sNouki + ","
                            + " sMITUMORIYUKOKIGEN = " + sYuukou + ","
                            + " cSHIHARAI = " + cShiharai + ","
                            + " nKINGAKU = " + lblKingaku.Text.Replace(",", "") + ","
                            + " nMITUMORIARARI = " + lblArari.Text.Replace(",", "") + ","
                            + " nMITUMORINEBIKI = " + nNebiki + ","
                            + " nMITUMORISYOHIZE = " + lblShohizei.Text.Replace(",", "") + ","
                            + " nHENKOU = " + (nHenkou+1).ToString() + ","
                            + " dHENKOU = Now(),"
                            + " cHENKOUSYA = '" + lblLoginUserCode.Text + "',"
                            + " nKINGAKU_G = " + lblGokeiKingaku.Text.Replace(",", "") + ","
                            + " nMITUMORINEBIKI_G = " + nNebiki + ","
                            + " nTANKA_G = " + lblTeikaGokei.Text.Replace(",", "") + ","
                            + " nKIRI_G = " + lblTeikaGokei.Text.Replace(",", "") + ","
                            + " nSIIRE_G = " + nSiire_G + ","
                            + " cJYOTAI_MITUMORI = '" + lblcJoutai.Text + "',"
                            + " fHYOUJI = '0',"
                            + " fYOUKOU = '1',"
                            + " cSHIHARAI_STOKUI = " + sTOKUISAKI_cSHIHARAI + ","
                            + " sSHIHARAI_STOKUI = " + sTOKUISAKI_sSHIHARAI + ","
                            + " sTOKUISAKIJUSYO = " + sTOKUISAKIJUSYO + ","
                            + " dURIAGEYOTEI = " + dUriageYotei + ","
                            + " dYOTEINOUKI = " + dJuuchuu + ","
                            + " YU_dURIYOTEI = " + dJuuchuuYotei + ","
                            + " dKANRYOUYOTEI = " + dKanryoYotei + ","
                            + " dKETSUTEI = " + dKakutei + ","
                            + " sMEMO = " + sMemo + ","
                            + " cSEIKYUSAKI = '" + lblcSEIKYUSAKI.Text + "',"
                            + " sSEIKYUSAKI = '" + sSeikyusaki + "',"
                            + " cSEIKYUSAKI_TKC = " + sSEIKYU_TKC + ","
                            + " cSEIKYU_YUUBIN = " + sSEIKYU_YUBIN + ","
                            + " sSEIKYU_JUUSHO1 = " + sSEIKYU_JUSYO1 + ","
                            + " sSEIKYU_JUUSHO2 = " + sSEIKYU_JUSYO2 + ","
                            + " sSEIKYU_TEL = " + sSEIKYU_TEL + ","
                            + " sSEIKYU_FAX = " + sSEIKYU_FAX + ","
                            + " sSEIKYU_TAN = " + sSeikyusaki_Tan + ","
                            + " sSEIKYU_TAN_Jun = '" + sSeikyu_tan_Jun + "',"
                            + " sSEIKYU_TANBUMON = " + sSEIKYU_sBUMON + ","
                            + " sSEIKYU_KEISYO = " + sSEIKYU_SKEISHOU + ","
                            + " sSEIKYU_YAKUSYOKU = " + sSEIKYU_SYAKUSHOKU + ","
                            + " cSHIHARAI_SEIKYU = " + sSEIKYU_cSHIHARAI + ","
                            + " sSHIMEBI = " + sSEIKYU_SHIMEBI + ","
                            + " sSEIKYU_BIKOU = " + sSEIKYU_BIKO + ","
                            + " sSEIKYU_SHIHARAIBI = " + sSEIKYU_SHIHARAIBI + ","
                            + " sSEIKYU_SHIHARAIGETU = " + sSEIKYU_sSHIHARAIGetsu + ","
                            + " sUKEWATASIBASYO = " + sUkewatashi + ","
                            + " cPJ = " + Jisyabango + ","
                            + " cKAKUDO = " + cKakudo + ","
                            + " nURIAGEKINGAKU = '" + nJuuchuKingaku + "',"
                            + " cKYOTEN = '" + lblcKYOTEN.Text + "',"
                            + " sTOKUISAKI_TANBUMON = " + sTOKUISAKI_sBUMON + ","
                            + " sTOKUISAKI_KEISYO = " + sTOKUISAKI_SKEISHOU + ","
                            + " sTOKUISAKI_YAKUSHOKU = " + sTOKUISAKI_SYAKUSHOKU + ","
                            + " nKAZEIKINGAKU = " + lblKingaku.Text.Replace(",", "") + ","
                            + " dMITUMORISAKUSEI = '" + lbldMitumori.Text + "',"
                            + " nTOKUISAKIKAKERITU = " + nTOKUISAKIKAKERITU + ","
                            + " nRIIRITSU = " + lblArariRitsu.Text.Replace(",", "").Replace("%", "")
                            + " Where '1' = '1' And cMITUMORI = '" + lblcMitumori.Text + "';";

                        update_sql += "INSERT INTO h_mitumori (cMITUMORI, cMITUMORI_KO, nJUNBAN, sNAIYOU, dHENKOU, cHENKOUSYA) VALUES ('" + lblcMitumori.Text + "', '" + lblcMitumori_Ko.Text + "', '" + junban + "', '見積登録/修正', Now(), '" + lblLoginUserCode.Text + "'); ";
                        update_sql += "Delete From r_mitumori_m where cMITUMORI='" + lblcMitumori.Text + "';";
                        #endregion

                        String insert_sql = "";
                        int rowNo = 1;
                        String insert_sql1 = "";

                        int maxRowNo = Convert.ToInt32(HF_maxRowNo.Value);
                        maxRowNo += 1;

                        #region r_mitumori_m
                        foreach (GridViewRow row in GV_MitumoriSyohin_Original.Rows)
                        {
                            Label lbl_status = (row.FindControl("lblhdnStatus") as Label);
                            TextBox txt_cSyohin = (row.FindControl("txtcSYOHIN") as TextBox);
                            TextBox txt_sSyohin = (row.FindControl("txtsSYOHIN") as TextBox);
                            TextBox txt_nSyoryo = (row.FindControl("txtnSURYO") as TextBox);
                            //DropDownList ddl_cTani = (row.FindControl("DDL_cTANI") as DropDownList);
                            TextBox txt_cTani = (row.FindControl("txtTani") as TextBox);
                            TextBox txt_nTanka = (row.FindControl("txtnTANKA") as TextBox);
                            Label lbl_TankaGokei = (row.FindControl("lblTankaGokei") as Label);
                            TextBox txt_nGenkaTanka = (row.FindControl("txtnGENKATANKA") as TextBox);
                            Label lbl_GenkaGokei = (row.FindControl("lblGenkaGokei") as Label);
                            Label lbl_Arari = (row.FindControl("lblnARARI") as Label);
                            Label lbl_ArariSu = (row.FindControl("lblnARARISu") as Label);
                            Label lbl_RowNo = (row.FindControl("lblRowNo") as Label);
                            Label lbl_fgentankatanka = (row.FindControl("lblfgenkatanka") as Label);
                            Button btn_Syosai = (row.FindControl("btnSyohinShosai") as Button);
                            Label lbl_sKubun = (row.FindControl("lblKubun") as Label);
                            Label lbl_nSIKIRITANKA = (row.FindControl("lblTanka") as Label);
                            TextBox txt_nRITU = (row.FindControl("txtnRITU") as TextBox);
                            Label lbl_fJITAIS = (row.FindControl("lblfjitais") as Label);
                            Label lbl_fJITAIQ = (row.FindControl("lblfjitaiq") as Label);

                            if (TextUtility.isomojiCharacter(txt_sSyohin.Text))
                            {
                                f_isomoji_msg = false;
                                break;
                            }
                            String sSyouhin = txt_sSyohin.Text.Replace("\\", "\\\\").Replace("'", "\\'");  //明細商品名

                            if (TextUtility.isomojiCharacter(txt_cTani.Text))
                            {
                                f_isomoji_msg = false;
                                break;
                            }
                            String sTani = txt_cTani.Text.Replace("\\", "\\\\").Replace("'", "\\'");  //明細単位

                            String ntanka = "0";
                            if (!String.IsNullOrEmpty(txt_nTanka.Text))
                            {
                                ntanka = txt_nTanka.Text.Replace(",", "");
                            }

                            String nsikiritanka = "0";
                            if (!String.IsNullOrEmpty(lbl_nSIKIRITANKA.Text))
                            {
                                nsikiritanka = lbl_nSIKIRITANKA.Text.Replace(",", "");
                            }

                            String ntankagokei = "0";
                            if (!String.IsNullOrEmpty(lbl_TankaGokei.Text))
                            {
                                ntankagokei = lbl_TankaGokei.Text.Replace(",", "");
                            }

                            String ngenka = "0";
                            if (!String.IsNullOrEmpty(txt_nGenkaTanka.Text))
                            {
                                ngenka = txt_nGenkaTanka.Text.Replace(",", "");
                            }

                            String ngenkagokei = "0";
                            if (!String.IsNullOrEmpty(lbl_GenkaGokei.Text))
                            {
                                ngenkagokei = lbl_GenkaGokei.Text.Replace(",", "");
                            }

                            String nsuryo = "0";
                            if (!String.IsNullOrEmpty(txt_nSyoryo.Text))
                            {
                                nsuryo = txt_nSyoryo.Text.Replace(",", "");
                            }

                            String fCheck = "";
                            if (btn_Syosai.Text != "詳")
                            {
                                fCheck = "2";
                            }

                            if (!String.IsNullOrEmpty(txt_cSyohin.Text) || !String.IsNullOrEmpty(txt_sSyohin.Text) ||
                                !String.IsNullOrEmpty(txt_nSyoryo.Text) || !String.IsNullOrEmpty(txt_cTani.Text) ||
                                !String.IsNullOrEmpty(txt_nTanka.Text) || !String.IsNullOrEmpty(lbl_TankaGokei.Text) ||
                                !String.IsNullOrEmpty(txt_nGenkaTanka.Text) || !String.IsNullOrEmpty(lbl_GenkaGokei.Text))
                            {
                                insert_sql += insert_sql1 + "Insert Into"
                                    + " r_mitumori_m(cMITUMORI, cMITUMORI_KO, nGYOUNO, cSYOUHIN, sSYOUHIN_R,"
                                    + " nTANKA, nSURYO, sTANI, nSIIRETANKA, nKINGAKU,"
                                    + " nRITU, dHENKOU, cHENKOUSYA, cSHIIRESAKI, sSHIIRESAKI,"
                                    + " nSIIREKINGAKU, nSIKIRITANKA, nSIKIRIKINGAKU, nINSATSU_GYO, cSYOUSAI,"
                                    + " rowNO, sSETSUMUI, fJITAIS, fJITAIQ, sMEMO,"
                                    + " fCHECK, fgentankatanka, sKUBUN)"
                                    + " Values('" + lblcMitumori.Text + "', '" + lblcMitumori_Ko.Text + "', " + rowNo.ToString() + ", '" + txt_cSyohin.Text + "', '" + sSyouhin + "',"
                                    + " " + ntanka + ", " + nsuryo + ", '" + sTani + "', " + ngenka + ", " + ntankagokei + ","
                                    + " '" + txt_nRITU.Text.Replace("%", "") + "', Now(), '" + lblLoginUserCode.Text + "', '', '',"
                                    + " " + ngenkagokei + ", " + nsikiritanka + ", " + ntankagokei + ", 0, '',"
                                    + " " + lbl_RowNo.Text + ", '', '"+lbl_fJITAIS.Text+"', '"+lbl_fJITAIQ.Text+"', '', '" + fCheck + "', '" + lbl_fgentankatanka.Text + "', '" + lbl_sKubun.Text + "');";
                                insert_sql1 = "";
                            }
                            else
                            {
                                String rNo = lbl_RowNo.Text;
                                if (rNo == "0" || String.IsNullOrEmpty(rNo))
                                {
                                    rNo = maxRowNo.ToString();
                                    maxRowNo += 1;
                                }
                                insert_sql1 += "Insert Into"
                                    + " r_mitumori_m(cMITUMORI, cMITUMORI_KO, nGYOUNO, cSYOUHIN, sSYOUHIN_R,"
                                    + " nTANKA, nSURYO, sTANI, nSIIRETANKA, nKINGAKU,"
                                    + " nRITU, dHENKOU, cHENKOUSYA, cSHIIRESAKI, sSHIIRESAKI,"
                                    + " nSIIREKINGAKU, nSIKIRITANKA, nSIKIRIKINGAKU, nINSATSU_GYO, cSYOUSAI,"
                                    + " rowNO, sSETSUMUI, fJITAIS, fJITAIQ, sMEMO,"
                                    + " fCHECK, fgentankatanka, sKUBUN)"
                                    + " Values('" + lblcMitumori.Text + "', '" + lblcMitumori_Ko.Text + "', " + rowNo.ToString() + ", '" + txt_cSyohin.Text + "', '" + sSyouhin + "',"
                                    + " " + ntanka + ", " + nsuryo + ", '" + sTani + "', " + ngenka + ", " + ntankagokei + ","
                                    + " 100, Now(), '" + lblLoginUserCode.Text + "', '', '',"
                                    + " " + ngenkagokei + ", " + nsikiritanka + ", " + ntankagokei + ", 0, '',"
                                    + " " + rNo + ", '', '0', '0', '', '" + fCheck + "', '" + lbl_fgentankatanka.Text + "', '" + lbl_sKubun.Text + "');";
                            }
                            rowNo++;
                        }
                        #endregion

                        if (f_isomoji_msg == false)
                        {
                            string msg = "使用不可能なテキスト（環境依存文字）が入力され保存できません。</br>文字化けの原因となるため、下記の文字を修正してください。</br>" + " 対象文字：「" + TextUtility.invalidtext_all + "」";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowAnkenChangeMessage",
                                        "ShowMojiMessage('" + msg + "','" + btnmojiOK.ClientID + "');", true);
                            f_isomoji_msg = true;
                            Session["moji"] = "true";
                        }
                        else
                        {

                            update_sql += insert_sql;

                            update_sql += "Delete From r_mitumori_m2 where cMITUMORI='" + lblcMitumori.Text + "';";

                            String insert_syosai_sql = "";
                            int gyoNo = 1;

                            #region r_mitumori_m2
                            foreach (GridViewRow row in GV_Syosai.Rows)
                            {
                                Label lbl_status = (row.FindControl("lblhdnStatus") as Label);
                                Label lbl_rowno = (row.FindControl("lblRowNo") as Label);
                                TextBox txt_cSyohin = (row.FindControl("txtcSYOHIN") as TextBox);
                                TextBox txt_sSyohin = (row.FindControl("txtsSYOHIN") as TextBox);
                                TextBox txt_nSyoryo = (row.FindControl("txtnSURYO") as TextBox);
                                Label lbl_cTani = (row.FindControl("lblcTANI") as Label);
                                TextBox txt_nTanka = (row.FindControl("txtnTANKA") as TextBox);
                                Label lbl_TankaGokei = (row.FindControl("lblTankaGokei") as Label);
                                TextBox txt_nGenkaTanka = (row.FindControl("txtnGENKATANKA") as TextBox);
                                Label lbl_GenkaGokei = (row.FindControl("lblGenkaGokei") as Label);
                                Label lbl_Arari = (row.FindControl("lblnARARI") as Label);
                                Label lbl_ArariSu = (row.FindControl("lblnARARISu") as Label);
                                TextBox txt_nRITU = (row.FindControl("txtnRITU") as TextBox); //20220517 MyatNoe
                                Label lbl_nSIKIRITANKA = (row.FindControl("lblTanka") as Label);
                                Label lbl_fJITAIS = (row.FindControl("lblfjitais") as Label);

                                if (TextUtility.isomojiCharacter(txt_sSyohin.Text))
                                {
                                    f_isomoji_msg = false;
                                    break;
                                }
                                String sSyouhin = txt_sSyohin.Text.Replace("\\", "\\\\").Replace("'", "\\'");  //詳細商品名

                                if (TextUtility.isomojiCharacter(lbl_cTani.Text))
                                {
                                    f_isomoji_msg = false;
                                    break;
                                }
                                String sTani = lbl_cTani.Text.Replace("\\", "\\\\").Replace("'", "\\'");  //詳細単位

                                String ntanka = "0";
                                if (!String.IsNullOrEmpty(txt_nTanka.Text))
                                {
                                    ntanka = txt_nTanka.Text.Replace(",", "");
                                }

                                String nsuryo = "0";
                                if (!String.IsNullOrEmpty(txt_nSyoryo.Text))
                                {
                                    nsuryo = txt_nSyoryo.Text.Replace(",", "");
                                }

                                String ngenka = "0";
                                if (!String.IsNullOrEmpty(txt_nGenkaTanka.Text))
                                {
                                    ngenka = txt_nGenkaTanka.Text.Replace(",", "");
                                }

                                String nSikiriTanka = "0";
                                if (!String.IsNullOrEmpty(lbl_nSIKIRITANKA.Text))
                                {
                                    nSikiriTanka = lbl_nSIKIRITANKA.Text.Replace(",", "");
                                }

                                String ntankagokei = "0";
                                if (!String.IsNullOrEmpty(lbl_TankaGokei.Text))
                                {
                                    ntankagokei = lbl_TankaGokei.Text.Replace(",", "");
                                }

                                String ngenkagokei = "0";
                                if (!String.IsNullOrEmpty(lbl_GenkaGokei.Text))
                                {
                                    ngenkagokei = lbl_GenkaGokei.Text.Replace(",", "");
                                }
                                //String fJitais = "0";
                                //if (String.IsNullOrEmpty(txt_cSyohin.Text))
                                //{
                                //    fJitais = "1";
                                //}

                                insert_syosai_sql += "INSERT INTO r_mitumori_m2 (cMITUMORI, cMITUMORI_KO, nGYOUNO, cSYOUHIN, sSYOUHIN_R," +
                                    " nTANKA, nSURYO, sTANI, nSIIRETANKA, nKINGAKU," +
                                    " nRITU, dHENKOU, cHENKOUSYA, nSIIREKINGAKU, nSIKIRITANKA," +
                                    " nSIKIRIKINGAKU, nINSATSU_GYO, sSETSUMUI, fJITAIS, fJITAIQ," +
                                    " rowNO, rowNO2, sMEMO, fCHECK)" +
                                    " VALUES('" + lblcMitumori.Text + "', '" + lblcMitumori_Ko.Text + "', '" + gyoNo + "', '" + txt_cSyohin.Text + "', '" + sSyouhin + "'," +
                                    " '" + ntanka + "', '" + nsuryo + "', '" + sTani + "', '" + ngenka + "', '" + ntankagokei + "'," +
                                    " '"+ txt_nRITU.Text.Replace("%", "") + "', Now(), '" + lblLoginUserCode.Text + "', '" + ngenkagokei + "', '" + nSikiriTanka + "'," +
                                    " '" + ntankagokei + "', '0', '" + gyoNo + "', '" + lbl_fJITAIS.Text + "', '0'," +
                                    " '" + lbl_rowno.Text + "', '" + gyoNo + "', '', '');";

                                gyoNo += 1;
                            }
                            #endregion

                            if (f_isomoji_msg == false)
                            {
                                string msg = "使用不可能なテキスト（環境依存文字）が入力され保存できません。</br>文字化けの原因となるため、下記の文字を修正してください。</br>" + " 対象文字：「" + TextUtility.invalidtext_all + "」";
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowAnkenChangeMessage",
                                            "ShowMojiMessage('" + msg + "','" + btnmojiOK.ClientID + "');", true);
                                f_isomoji_msg = true;
                                Session["moji"] = "true";
                            }
                            else
                            {

                                update_sql += insert_syosai_sql;

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

                                    divLabelSave.Style["display"] = "flex";//「保存しました。」メッセージを表示                                                                                                                                      
                                    updLabelSave.Update();

                                    btnBetsuMitumoriSave.Visible = true;
                                    btnMitumoriDelete.Visible = true;
                                    btnMitumorishoPDF.Enabled = true;
                                    btnMitumorishoPDF.CssClass = "BlueBackgroundButton JC10SaveBtn";
                                    //btnCreateUriage.Enabled = true;
                                    //btnCreateUriage.CssClass = "BlueBackgroundButton JC10SaveBtn";
                                    //btnUriage.Enabled = true;
                                    //btnUriage.CssClass = "BlueBackgroundButton JC10SaveBtn";
                                    lblHenkousya.Text = lblLoginUserName.Text;
                                    lblcHenkousya.Text = lblLoginUserCode.Text;
                                    lblHenkoubi.Text = DateTime.Now.ToString("yyyy/MM/dd");
                                    lblnHenkou.Text = nHenkou.ToString();
                                    saveSuccess = true;
                                    HF_isChange.Value = "0";
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
                            }
                        }
                    }





                    #region added by YG
                    Session["cTokuisaki"] = lblcTOKUISAKI.Text;
                    Session["sTokuisaki"] = lblsTOKUISAKI.Text;
                    Session["TOUKUISAKITANTOU"] = lblsTOKUISAKI_TAN.Text;
                    Session["TokuisakiTanJun"] = lblsTOKUISAKI_TAN_JUN.Text;
                    Session["cSeikyusaki"] = lblcSEIKYUSAKI.Text;
                    Session["sSeikyusaki"] = lblsSEIKYUSAKI.Text;
                    Session["sSEIKYUSAKI_TAN"] = lblsSEIKYUSAKI_TAN.Text;
                    Session["sSEIKYUSAKI_TAN_JUN"] = lblsSEIKYUSAKI_TAN_JUN.Text;
                    Session["sMITUMORI"] = txtsMitumori.Text;
                    Session["sKYOTEN1"] = lblsKYOTEN.Text;
                    Session["cMitumoriJoutai1"] = lblcJoutai.Text;  //20220404 Added by MiMi
                    Session["sMitumoriJoutai1"] = lblsJoutai.Text; //20220404 Added by MiMi
                    #endregion
                }
            }
            updHeader.Update();
        }
        #endregion

        #region checkData
        private void checkData()
        {
            if (!String.IsNullOrEmpty(txtsMitumori.Text) && lblcTOKUISAKI.Text != "" && lblcSEIKYUSAKI.Text != "")
            {
                HF_checkData.Value = "1";
            }
            else
            {
                HF_checkData.Value = "0";
            }

            if (String.IsNullOrEmpty(txtsMitumori.Text))
            {
                txtsMitumori.BorderColor = System.Drawing.Color.Red;
                txtsMitumori.BorderStyle = BorderStyle.Double;
                txtsMitumori.BorderWidth = 2;
            }
            else
            {
                txtsMitumori.BorderColor = System.Drawing.Color.Empty;
                txtsMitumori.BorderStyle = BorderStyle.NotSet;
                txtsMitumori.BorderWidth = 1;
            }

            if (lblcTOKUISAKI.Text != "")
            {
                btnTokuisaki.BorderStyle = BorderStyle.None;
            }
            else
            {
                btnTokuisaki.BorderColor = System.Drawing.Color.Red;
                btnTokuisaki.BorderStyle = BorderStyle.Double;
                btnTokuisaki.BorderWidth = 2;
            }

            if (lblcSEIKYUSAKI.Text != "")
            {
                btnSeikyusaki.BorderStyle = BorderStyle.None;
            }
            else
            {
                btnSeikyusaki.BorderColor = System.Drawing.Color.Red;
                btnSeikyusaki.BorderStyle = BorderStyle.Double;
                btnSeikyusaki.BorderWidth = 2;
            }
        }
        #endregion

        #region 売上予定日もしくは見積日によって諸費税パーセント変更する

        private double get_nSYOUHIZEI()
        {
            //売上予定日＝notNULLのとき…売上予定日
            //売上予定日＝NULLのとき…見積日

            DateTime Tax_Date = new DateTime();

            if (lbldUriageYotei.Text != null && !lbldUriageYotei.Text.Equals(""))
            {
                Tax_Date = Convert.ToDateTime(lbldUriageYotei.Text);
            }
            else if (lbldMitumori.Text != null && !lbldMitumori.Text.Equals(""))
            {
                Tax_Date = Convert.ToDateTime(lbldMitumori.Text);
            }

            DataTable nsyouhizei_table = new DataTable();
            string sql = "";
            sql = "select ifnull(nSYOUHIZEI,0) as nSYOUHIZEI";
            sql += ",ifnull(date_format(dDATE,'%Y/%m/%d'),'') as dDATE";
            sql += " from m_syouhizei";
            sql += " where dDATE <='" + Tax_Date.ToShortDateString() + "' order by dDATE DESC";
            String id = Request.QueryString["id"];
            DataTable dt_db = JC01Login_Class.Get_DB(id);
            String db = dt_db.Rows[0]["db"].ToString();
            String mail = dt_db.Rows[0]["mail"].ToString();
            ConstantVal.DB_NAME = db;

            JC_ClientConnecction_Class jc = new JC_ClientConnecction_Class();
            jc.loginId = mail;
            con = jc.GetConnection();
            con.Open();
            MySqlCommand cmd = new MySqlCommand(sql, con);
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            da.Fill(nsyouhizei_table);
            con.Close();
            da.Dispose();
            string per = "";
            if (nsyouhizei_table.Rows.Count > 0)
            {
                per = nsyouhizei_table.Rows[0]["nSYOUHIZEI"].ToString();
                if (per != "")
                {
                    return Convert.ToDouble(per);
                }
                else
                {
                    return 5;
                }
            }
            else
            {
                return 5;
            }
        }

        #endregion

        #region GetTotalKingaku
        private void GetTotalKingaku()
        {
            SetGokeiKingakuHyoji();
            if (!CHK_KingakuNotDisplay.Checked)
            {
                Double nTeikaGokei = 0;
                Double nArariGokei = 0;
                Double nGenkaGokei = 0;
                Double nkingaku = 0;
                foreach (GridViewRow row in GV_MitumoriSyohin_Original.Rows)
                {
                    Label lbl_Kubun = (row.FindControl("lblKubun") as Label);
                    if (lbl_Kubun.Text != "見" && lbl_Kubun.Text != "計")
                    {
                        Label lbl_TankaGokei = (row.FindControl("lblTankaGokei") as Label);
                        Label lbl_GenkaGokei = (row.FindControl("lblGenkaGokei") as Label);
                        Label lbl_Arari = (row.FindControl("lblnARARI") as Label);
                        Label lbl_ArariSu = (row.FindControl("lblnARARISu") as Label);
                        TextBox txtnTanka = (row.FindControl("txtnTANKA") as TextBox);
                        double nTanka = 0;
                        if (!String.IsNullOrEmpty(txtnTanka.Text))
                        {
                            nTanka = Convert.ToDouble(txtnTanka.Text);
                        }

                        TextBox txtSuryo = (row.FindControl("txtnSURYO") as TextBox);
                        double nSuryo = 0;
                        if (!String.IsNullOrEmpty(txtSuryo.Text))
                        {
                            nSuryo = Convert.ToDouble(txtSuryo.Text);
                        }

                        nTeikaGokei += (nTanka * nSuryo);

                        Double nTankaGokei = 0;
                        if (!String.IsNullOrEmpty(lbl_TankaGokei.Text))
                        {
                            nTankaGokei = Convert.ToDouble(lbl_TankaGokei.Text.Replace(",", ""));
                        }
                        nkingaku += nTankaGokei;

                        Double nGenka = 0;
                        if (!String.IsNullOrEmpty(lbl_GenkaGokei.Text))
                        {
                            nGenka = Convert.ToDouble(lbl_GenkaGokei.Text.Replace(",", ""));
                        }
                        nGenkaGokei += nGenka;

                        Double nArari = 0;
                        nArari = nTankaGokei - nGenka;
                        nArariGokei += nArari;
                    }
                }

                Double nNebiki = 0;
                if (!String.IsNullOrEmpty(txtShusseiNebiki.Text))
                {
                    nNebiki = Convert.ToDouble(txtShusseiNebiki.Text.Replace(",", ""));
                }
                Double nKingaku = nkingaku - nNebiki;
                getSYOUHIZEI();
                Double D_nMITUMORISYOHIZE = Convert.ToDouble(nKingaku) * Convert.ToDouble(syouhizei);

                Double nGokei = nKingaku + D_nMITUMORISYOHIZE;
                lblGokeiKingaku.Text = nGokei.ToString();
                nArariGokei -= nNebiki;
                Double nArarisu = (nArariGokei / nKingaku) * 100;
                if (nKingaku == 0)
                {
                    nArarisu = 0;
                }
                lblGokeiKingaku.Text = nGokei.ToString("#,##0");
                lblKingaku.Text = nKingaku.ToString("#,##0");
                lblShohizei.Text = D_nMITUMORISYOHIZE.ToString("#,##0");
                txtShusseiNebiki.Text = nNebiki.ToString("#,##0");
                lblTeikaGokei.Text = nTeikaGokei.ToString("#,##0");
                lblArari.Text = nArariGokei.ToString("#,##0");
                lblArariRitsu.Text = nArarisu.ToString("###0.0") + "%";
                lblnSIIRE_G.Text = nGenkaGokei.ToString();
                updKingaku.Update();
            }
            else
            {
                lblGokeiKingaku.Text = "0";
                lblKingaku.Text = "0";
                lblShohizei.Text = "0";
                //txtShusseiNebiki.Text = "0";
                lblTeikaGokei.Text = "0";
                lblArari.Text = "0";
                lblArariRitsu.Text = "0";
                lblnSIIRE_G.Text = "0";
                updKingaku.Update();
            }
        }
        #endregion

        #region getSYOUHIZEI         //消費税の取る

        private void getSYOUHIZEI()
        {
            try
            {
                syouhizei = Convert.ToDouble(get_nSYOUHIZEI()) / 100;
            }
            catch (Exception ex)
            {
            }
        }
        #endregion

        #region txtShusseiNebiki_TextChanged
        protected void txtShusseiNebiki_TextChanged(object sender, EventArgs e)
        {
            bool isNumber = Double.TryParse(txtShusseiNebiki.Text, out Double numericValue);
            if (!isNumber)
            {
                txtShusseiNebiki.Text = "";
            }

            GetTotalKingaku();
            if (txtShusseiNebiki.Text.StartsWith("-"))
            {
                txtShusseiNebiki.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                txtShusseiNebiki.ForeColor = System.Drawing.Color.Black;
            }
            HF_isChange.Value = "1";
            updHeader.Update();
        }
        #endregion

        #region getTOKUISAKI_INFO
        //得意先情報を取る
        private void getTOKUISAKI_INFO()
        {
            try
            {
                sTOKUISAKIYUBIN = "null";
                sTOKUISAKIJUSYO = "null";
                sTOKUISAKIJUSYO1 = "null";
                sTOKUISAKIJUSYO2 = "null";
                sTOKUISAKITEL = "null";
                sTOKUISAKIFAX = "null";
                nTOKUISAKIKAKERITU = "null";

                sTOKUISAKI_cSHIHARAI = "null";
                sTOKUISAKI_sSHIHARAI = "null";
                if (lblcTOKUISAKI.Text != "")
                {
                    //JC_ClientConnecction_Class jc = new JC_ClientConnecction_Class();
                    //jc.loginId = Session["LoginId"].ToString();
                    //con = jc.GetConnection();
                    string sqlTokuisaki = " select  sTOKUISAKI1, cYUUBIN, sJUUSHO1, sJUUSHO2, sTEL, sFAX,cSHIHARAI,sSHIHARAI,nNEBIKIRITSU from m_tokuisaki where cTOKUISAKI ='" + lblcTOKUISAKI.Text + "'";
                    MySqlCommand cmd = new MySqlCommand(sqlTokuisaki, con);
                    cmd.CommandTimeout = 0;
                    con.Close();
                    con.Open();
                    MySqlDataReader dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            lblsTOKUISAKI.Text = dr["sTOKUISAKI1"].ToString().Replace("<", "&lt").Replace(">", "&gt");
                            if (!String.IsNullOrEmpty(dr["cYUUBIN"].ToString()))
                            {
                                sTOKUISAKIYUBIN = "'" + dr["cYUUBIN"].ToString() + "'";
                            }
                            if (!String.IsNullOrEmpty(dr["sJUUSHO1"].ToString()))
                            {
                                sTOKUISAKIJUSYO1 = "'" + dr["sJUUSHO1"].ToString() + "'";
                            }
                            if (!String.IsNullOrEmpty(dr["sJUUSHO2"].ToString()))
                            {
                                sTOKUISAKIJUSYO2 = "'" + dr["sJUUSHO2"].ToString() + "'";
                            }
                            if (!(String.IsNullOrEmpty(dr["sJUUSHO1"].ToString()) && String.IsNullOrEmpty(dr["sJUUSHO2"].ToString())))
                            {
                                sTOKUISAKIJUSYO = "'" + dr["sJUUSHO1"].ToString()+" "+ dr["sJUUSHO2"].ToString()+ "'";
                            }
                            if (!String.IsNullOrEmpty(dr["sTEL"].ToString()))
                            {
                                sTOKUISAKITEL = "'" + dr["sTEL"].ToString() + "'";
                            }
                            if (!String.IsNullOrEmpty(dr["sFAX"].ToString()))
                            {
                                sTOKUISAKIFAX = "'" + dr["sFAX"].ToString() + "'";
                            }
                            if (!String.IsNullOrEmpty(dr["nNEBIKIRITSU"].ToString()))
                            {
                                nTOKUISAKIKAKERITU = "'" + dr["nNEBIKIRITSU"].ToString() + "'";
                            }
                            if (!String.IsNullOrEmpty(dr["cSHIHARAI"].ToString()))
                            {
                                sTOKUISAKI_cSHIHARAI = "'" + dr["cSHIHARAI"].ToString() + "'";
                            }
                            if (!String.IsNullOrEmpty(dr["sSHIHARAI"].ToString()))
                            {
                                sTOKUISAKI_sSHIHARAI = "'" + dr["sSHIHARAI"].ToString() + "'";
                            }
                        }
                        dr.Close();
                    }
                    con.Close();

                }
                else
                {
                }

                sSEIKYU_YUBIN = "null";
                sSEIKYU_JUSYO1 = "null";
                sSEIKYU_JUSYO2 = "null";
                sSEIKYU_TEL = "null";
                sSEIKYU_FAX = "null";
                sSEIKYU_TKC = "null";
                sSEIKYU_SHIMEBI = "null";
                sSEIKYU_SHIHARAIBI = "null";
                sSEIKYU_BIKO = "null";
                sSEIKYU_sSHIHARAIGetsu = "null";

                sSEIKYU_cSHIHARAI = "'00'";
                if (lblcSEIKYUSAKI.Text != "")
                {
                    //JC_ClientConnecction_Class jc = new JC_ClientConnecction_Class();
                    //jc.loginId = Session["LoginId"].ToString();
                    //con = jc.GetConnection();
                    string sqlTokuisaki = " select  sTOKUISAKI1, cYUUBIN, sJUUSHO1, sJUUSHO2, sTEL, sFAX,cSHIHARAI,sSHIHARAI,cTOKUISAKI_G1,sSHIHARAIBI,sTOKKIJIKOU,sSHIHARAIGETU,sSHIMEBI from m_tokuisaki where cTOKUISAKI ='" + lblcSEIKYUSAKI.Text + "'";
                    MySqlCommand cmd = new MySqlCommand(sqlTokuisaki, con);
                    cmd.CommandTimeout = 0;
                    con.Close();
                    con.Open();
                    MySqlDataReader dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            lblsSEIKYUSAKI.Text = dr["sTOKUISAKI1"].ToString().Replace("<", "&lt").Replace(">", "&gt");
                            if (!String.IsNullOrEmpty(dr["cYUUBIN"].ToString()))
                            {
                                sSEIKYU_YUBIN = "'" + dr["cYUUBIN"].ToString() + "'";
                            }
                            if (!String.IsNullOrEmpty(dr["sJUUSHO1"].ToString()))
                            {
                                sSEIKYU_JUSYO1 = "'" + dr["sJUUSHO1"].ToString() + "'";
                            }
                            if (!String.IsNullOrEmpty(dr["sJUUSHO2"].ToString()))
                            {
                                sSEIKYU_JUSYO2 = "'" + dr["sJUUSHO2"].ToString() + "'";
                            }
                            if (!String.IsNullOrEmpty(dr["sTEL"].ToString()))
                            {
                                sSEIKYU_TEL = "'" + dr["sTEL"].ToString() + "'";
                            }
                            if (!String.IsNullOrEmpty(dr["sFAX"].ToString()))
                            {
                                sSEIKYU_FAX = "'" + dr["sFAX"].ToString() + "'";
                            }
                            if (!String.IsNullOrEmpty(dr["cTOKUISAKI_G1"].ToString()))
                            {
                                sSEIKYU_TKC = "'" + dr["cTOKUISAKI_G1"].ToString() + "'";
                            }
                            if (!String.IsNullOrEmpty(dr["sSHIMEBI"].ToString()))
                            {
                                sSEIKYU_SHIMEBI = "'" + dr["sSHIMEBI"].ToString() + "'";
                            }
                            if (!String.IsNullOrEmpty(dr["sSHIHARAIBI"].ToString()))
                            {
                                sSEIKYU_SHIHARAIBI = "'" + dr["sSHIHARAIBI"].ToString() + "'";
                            }
                            if (!String.IsNullOrEmpty(dr["sTOKKIJIKOU"].ToString()))
                            {
                                sSEIKYU_BIKO = "'" + dr["sTOKKIJIKOU"].ToString() + "'";
                            }
                            if (!String.IsNullOrEmpty(dr["sSHIHARAIGETU"].ToString()))
                            {
                                sSEIKYU_sSHIHARAIGetsu = "'" + dr["sSHIHARAIGETU"].ToString() + "'";
                            }

                            if (!String.IsNullOrEmpty(dr["cSHIHARAI"].ToString()))
                            {
                                sSEIKYU_cSHIHARAI = "'" + dr["cSHIHARAI"].ToString() + "'";
                            }
                        }
                        dr.Close();
                    }
                    con.Close();

                }
                else
                {
                }
            }
            catch (Exception ex)
            {

            }
        }
        #endregion

        #region getTOKUISAKI_Tantou
        private void getTOKUISAKI_Tantou(string code)
        {
            sTOKUISAKI_sBUMON = "null";
            sTOKUISAKI_SKEISHOU = "null";
            sTOKUISAKI_SYAKUSHOKU = "null";
            if (code != "")
            {
                string tokuisaki_tantou = " select sBUMON as sBUMON ,SYAKUSHOKU as SYAKUSHOKU,SKEISHOU as SKEISHOU from tokuisaki_tantousha where cTOKUISAKI='" + code + "' and NJUNBAN='" + lblsTOKUISAKI_TAN_JUN.Text + "';  ";
                MySqlCommand cmd = new MySqlCommand(tokuisaki_tantou, con);
                cmd.CommandTimeout = 0;
                con.Close();
                con.Open();
                MySqlDataReader dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        if (!String.IsNullOrEmpty(dr["sBUMON"].ToString()))
                        {
                            sTOKUISAKI_sBUMON = "'" + dr["sBUMON"].ToString() + "'";
                        }
                        if (!String.IsNullOrEmpty(dr["SKEISHOU"].ToString()))
                        {
                            sTOKUISAKI_SKEISHOU = "'" + dr["SKEISHOU"].ToString() + "'";
                        }
                        if (!String.IsNullOrEmpty(dr["SYAKUSHOKU"].ToString()))
                        {
                            sTOKUISAKI_SYAKUSHOKU = "'" + dr["SYAKUSHOKU"].ToString() + "'";
                        }
                    }
                    dr.Close();
                }
                con.Close();
            }
        }
        #endregion

        #region getSEIKYU_Tantou
        private void getSEIKYU_Tantou(string code)
        {
            sSEIKYU_sBUMON = "null";
            sSEIKYU_SKEISHOU = "null";
            sSEIKYU_SYAKUSHOKU = "null";
            if (code != "")
            {
                string tokuisaki_tantou = " select sBUMON as sBUMON ,SYAKUSHOKU as SYAKUSHOKU,SKEISHOU as SKEISHOU from tokuisaki_tantousha where cTOKUISAKI='" + code + "' and NJUNBAN='" + lblsSEIKYUSAKI_TAN_JUN.Text + "';  ";
                MySqlCommand cmd = new MySqlCommand(tokuisaki_tantou, con);
                cmd.CommandTimeout = 0;
                con.Close();
                con.Open();
                MySqlDataReader dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        if (!String.IsNullOrEmpty(dr["sBUMON"].ToString()))
                        {
                            sSEIKYU_sBUMON = "'" + dr["sBUMON"].ToString() + "'";
                        }
                        if (!String.IsNullOrEmpty(dr["SKEISHOU"].ToString()))
                        {
                            sSEIKYU_SKEISHOU = "'" + dr["SKEISHOU"].ToString() + "'";
                        }
                        if (!String.IsNullOrEmpty(dr["SYAKUSHOKU"].ToString()))
                        { 
                            sSEIKYU_SYAKUSHOKU = "'" + dr["SYAKUSHOKU"].ToString() + "'";
                        }
                    }
                    dr.Close();
                }
                con.Close();
            }
        }
        #endregion

        #region 商品を選択
        protected void btnSyohinSelect_Click(object sender, EventArgs e)
        {
            var btn_syohin = sender as Button;
            GridViewRow gvr = (GridViewRow)btn_syohin.NamingContainer;
            int rowindex = gvr.RowIndex;
            Session["syohinSelectRowIndex"] = rowindex.ToString();

            SessionUtility.SetSession("HOME", "Master");
            //ifSentakuPopup.Style["width"] = "1270px";
            //ifSentakuPopup.Style["height"] = "645px";
            //ifSentakuPopup.Src = "JC21SyouhinKensaku.aspx";
            //mpeSentakuPopup.Show();

            //updSentakuPopup.Update();

            String id = Request.QueryString["id"];
            ifShinkiPopup.Src = "JC21SyouhinKensaku.aspx?id=" + id;
            mpeShinkiPopup.Show();
            updShinkiPopup.Update();

        }
        #endregion

        #region btnSyohinGridSelect_Click  商品サブ画面を閉じる時のフォーカス処理
        protected void btnSyohinGridSelect_Click(object sender, EventArgs e)
        {
            DataTable dt_Syosai = GetSyosaiGridViewData();
            DataTable dt_SyohinOriginal = GetGridViewData();
            int rowindex = 0;

            Boolean IstextboxEnable = true;
            if (Session["cSyohin"] != null)
            {
                string cSYOHIN = (string)Session["cSyohin"];
                rowindex = Convert.ToInt32((string)Session["syohinSelectRowIndex"]);
                Session["syohinSelectRowIndex"] = null;
                (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("txtcSYOHIN") as TextBox).Text = cSYOHIN;

                int rowNo = Convert.ToInt32((GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblRowNo") as Label).Text);
                if (rowNo == 0)
                {
                    var max = dt_SyohinOriginal.AsEnumerable().Max(x => int.Parse(x.Field<string>("rowNo")));
                    max += 1;
                    HF_maxRowNo.Value = max.ToString();
                    (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblRowNo") as Label).Text = max.ToString();
                    updMitsumoriSyohinGrid.Update();
                }

                if (cSYOHIN != "")
                {
                    cSYOHIN = cSYOHIN.ToString().PadLeft(10, '0');
                    (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("txtcSYOHIN") as TextBox).Text = cSYOHIN;
                    (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("txtcSYOHIN") as TextBox).ToolTip = cSYOHIN;

                    String id = Request.QueryString["id"];
                    DataTable dt_db = JC01Login_Class.Get_DB(id);
                    String db = dt_db.Rows[0]["db"].ToString();
                    String mail = dt_db.Rows[0]["mail"].ToString();
                    ConstantVal.DB_NAME = db;

                    JC_ClientConnecction_Class jc = new JC_ClientConnecction_Class();
                    jc.loginId = mail;
                    MySqlConnection cn = jc.GetConnection();
                    cn.Open();
                    string sql_syouhin = " ";
                    sql_syouhin = " Select " +
                                  " ms.cSYOUHIN as cSYOUHIN," +
                                  " IfNull(ms.sSYOUHIN, '') as sSYOUHIN," +
                                  " IfNull(ms.sSHIYOU, '') as sSHIYOU, " +
                                  " IfNull(ms.sTANI,'') as sTANI, " +
                                  " format(ifnull(ms.nHANNBAIKAKAKU, 0),2) as nHANNBAIKAKAKU," +
                                  " Format(Round((ifnull(ms.nHANNBAIKAKAKU, 0))*(ifnull(ms.nSYOUKISU, 1)),0),0) as TankaGokei," +
                                  " Format(ifnull(ms.nSHIIREKAKAKU, 0),2) as nSHIIREKAKAKU," +
                                  " Format(Round((ifnull(ms.nSHIIREKAKAKU, 0))*(ifnull(ms.nSYOUKISU, 1)),0),0) as GenkaGokei," +
                                  " format(Round((ifnull(ms.nHANNBAIKAKAKU, 0))*(ifnull(ms.nSYOUKISU, 1)),0)-Round((ifnull(ms.nSHIIREKAKAKU, 0))*(ifnull(ms.nSYOUKISU, 1)),0),0) as nARARI," +
                                  " CONCAT(FORMAT(IfNull((Round((ifnull(ms.nHANNBAIKAKAKU, 0))*(ifnull(ms.nSYOUKISU, 1)),0)-Round((ifnull(ms.nSHIIREKAKAKU, 0))*(ifnull(ms.nSYOUKISU, 1)),0))/(Round((ifnull(ms.nHANNBAIKAKAKU, 0))*(ifnull(ms.nSYOUKISU, 1)),0)),0)*100, 1),'%') As nARARISu," +
                                  " ifnull(ms.nSYOUKISU, 1) as nSYOUKISU," +
                                  " MSD.sSYOUHIN_DAIGRP as sSYOUHIN_DAIGRP," +
                                  " MST.sSYOUHIN_TYUUGRP as sSYOUHIN_TYUUGRP," +
                                  " ifnull(ms.sBIKOU, '') as sBIKOU," +
                                  " ifnull(ms.fJITAIS, '0') as fJITAIS" +
                                  " From m_syouhin ms " +
                                  " left join" +
                                  " M_SYOUHIN_DAIGRP MSD ON MSD.cSYOUHIN_DAIGRP = ms.cSYOUHIN_DAIGRP" +
                                  " left join " +
                                  " M_SYOUHIN_TYUUGRP MST ON ms.cSYOUHIN_TYUUGRP = MST.cSYOUHIN_TYUUGRP" +
                                  " Where (ms.fHAIBAN <> '1' or ms.fHAIBAN is null) and '1' = '1' and ms.cSYOUHIN like '%" + cSYOHIN + "%'; ";


                    MySqlCommand cmd = new MySqlCommand(sql_syouhin, cn);
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    DataTable dtable = new DataTable();
                    da.Fill(dtable);
                    cn.Close();
                    da.Dispose();
                    if (dtable.Rows.Count > 0)
                    {
                        (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("txtsSYOHIN") as TextBox).Text = dtable.Rows[0]["sSYOUHIN"].ToString() + "　" + dtable.Rows[0]["sSHIYOU"].ToString();
                        (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("txtsSYOHIN") as TextBox).ToolTip = dtable.Rows[0]["sSYOUHIN"].ToString() + "　" + dtable.Rows[0]["sSHIYOU"].ToString();
                        (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblfjitais") as Label).Text = dtable.Rows[0]["fJITAIS"].ToString();

                        double nSuryo = 0;
                        if (!String.IsNullOrEmpty(dtable.Rows[0]["nSYOUKISU"].ToString()))
                        {
                            nSuryo = Convert.ToDouble(dtable.Rows[0]["nSYOUKISU"].ToString());
                        }

                        (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("txtnSURYO") as TextBox).Text = nSuryo.ToString("#,##0.##");
                        (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("txtnSURYO") as TextBox).ToolTip = nSuryo.ToString("#,##0.##");

                        (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblcTANI") as Label).Text = dtable.Rows[0]["sTANI"].ToString();
                        (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblcTANI") as Label).ToolTip = dtable.Rows[0]["sTANI"].ToString();
                        (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("txtTani") as TextBox).Text = dtable.Rows[0]["sTANI"].ToString();
                        string getsTANI = " select distinct cTANI, sTANI from M_TANI order by cTANI ";
                        MySqlCommand cmd1 = new MySqlCommand(getsTANI, cn);
                        MySqlDataAdapter da1 = new MySqlDataAdapter(cmd1);
                        DataTable dt = new DataTable();
                        da1.Fill(dt);
                        cn.Close();
                        da1.Dispose();
                        DropDownList DropDownList1 = (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("DDL_cTANI") as DropDownList);
                        DropDownList1.DataSource = dt;
                        DropDownList1.DataTextField = "sTANI";
                        DropDownList1.DataValueField = "sTANI";
                        DropDownList1.DataBind();
                        string lblcTANI = (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblcTANI") as Label).Text;
                        DropDownList1.Items.Insert(0, new ListItem(" ", "00"));
                        try
                        {
                            DropDownList1.Items.FindByText(lblcTANI).Selected = true;
                        }
                        catch { }

                        Double nkakeritsu = Convert.ToDouble((GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("txtnRITU") as TextBox).Text.Replace("%", ""));
                        double nHanbaikakaku = 0;
                        Double nTankaGokei = 0;
                        Double nSikiriTanka = 0;
                        if (String.IsNullOrEmpty((GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("txtnTANKA") as TextBox).Text) || (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("txtnTANKA") as TextBox).Text == "0")
                        {
                            if (!String.IsNullOrEmpty(dtable.Rows[0]["nHANNBAIKAKAKU"].ToString()))
                            {
                                nHanbaikakaku = Convert.ToDouble(dtable.Rows[0]["nHANNBAIKAKAKU"].ToString());
                                nTankaGokei = ((nHanbaikakaku / 100) * nkakeritsu) * nSuryo;
                                nSikiriTanka = (nHanbaikakaku / 100) * nkakeritsu;
                            }
                        }
                        else
                        {
                            nHanbaikakaku = Convert.ToDouble((GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("txtnTANKA") as TextBox).Text);
                            nTankaGokei = ((nHanbaikakaku / 100) * nkakeritsu) * nSuryo;
                            nSikiriTanka = (nHanbaikakaku / 100) * nkakeritsu;
                        }

                        string sql_syouhin_m = " ";
                        sql_syouhin_m = " Select " +
                            " msm.cSYOUHIN_m as cSYOUHIN," +
                            " IfNull(ms.sSYOUHIN, '') as sSYOUHIN," +
                            " IfNull(ms.sSHIYOU, '') as sSHIYOU," +
                            " IfNull(ms.sTANI, '') as sTANI," +
                            " format(ifnull(ms.nHANNBAIKAKAKU, 0), 2) as nHANNBAIKAKAKU," +
                            " Format(Round((ifnull(ms.nHANNBAIKAKAKU, 0)) * (ifnull(ms.nSYOUKISU, 1)), 0), 0) as TankaGokei," +
                            " Format(ifnull(ms.nSHIIREKAKAKU, 0), 2) as nSHIIREKAKAKU," +
                            " Format(Round((ifnull(ms.nSHIIREKAKAKU, 0)) * (ifnull(ms.nSYOUKISU, 1)), 0), 0) as GenkaGokei," +
                            " format(Round((ifnull(ms.nHANNBAIKAKAKU, 0)) * (ifnull(ms.nSYOUKISU, 1)), 0) " +
                            "- Round((ifnull(ms.nSHIIREKAKAKU, 0)) * (ifnull(ms.nSYOUKISU, 1)), 0), 0) as nARARI," +
                            " CONCAT(FORMAT(IfNull((Round((ifnull(ms.nHANNBAIKAKAKU, 0)) * (ifnull(ms.nSYOUKISU, 1)), 0)" +
                            " - Round((ifnull(ms.nSHIIREKAKAKU, 0)) * (ifnull(ms.nSYOUKISU, 1)), 0)) " +
                            "/ (Round((ifnull(ms.nHANNBAIKAKAKU, 0)) * (ifnull(ms.nSYOUKISU, 1)), 0)), 0) * 100, 1),'%') As nARARISu," +
                            " ifnull(ms.nSYOUKISU, 1) as nSYOUKISU," +
                            " ifnull(ms.sBIKOU, '') as sBIKOU," +
                            " ifnull(ms.fJITAIS, '0') as fJITAIS" +
                            " From m_syouhin ms" +
                            " right join m_syouhin_m msm ON ms.cSYOUHIN = msm.cSYOUHIN_m" +
                            " Where msm.cSYOUHIN like '%" + cSYOHIN + "%' order by msm.nJUNBAN; ";
                        cn.Open();
                        cmd = new MySqlCommand(sql_syouhin_m, cn);
                        da = new MySqlDataAdapter(cmd);
                        DataTable dtable_syohin_m = new DataTable();
                        da.Fill(dtable_syohin_m);
                        cn.Close();
                        da.Dispose();

                        double nSHIIREKAKAKU = 0;
                        double nGenkaGokei = 0;
                        if (dtable_syohin_m.Rows.Count == 0)
                        {
                            if ((GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("btnSyohinShosai") as Button).Text != "詳")
                            {
                                DataRow[] rows = dt_Syosai.Select("rowNo = '" + (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblRowNo") as Label).Text + "'");
                                if (rows.Length > 0)
                                {
                                    foreach (var drow in rows)
                                    {
                                        Double genka = 0;
                                        if (!String.IsNullOrEmpty(drow[8].ToString()))
                                        {
                                            genka = Convert.ToDouble(drow[8].ToString());
                                        }
                                        nGenkaGokei += genka;
                                    }
                                }

                                if (nGenkaGokei != 0)
                                {
                                    IstextboxEnable = false;
                                    if ((GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblfgenkatanka") as Label).Text == "0")
                                    {
                                        //nSHIIREKAKAKU = Convert.ToDouble(dtable.Rows[0]["nSHIIREKAKAKU"].ToString());
                                        nSHIIREKAKAKU = nGenkaGokei;
                                        nGenkaGokei = nSHIIREKAKAKU * nSuryo;
                                    }
                                    else
                                    {
                                        if (nSuryo == 0)
                                        {
                                            nSHIIREKAKAKU = 0;
                                        }
                                        else
                                        {
                                            nSHIIREKAKAKU = nGenkaGokei / nSuryo;
                                        }
                                    }
                                }
                                else
                                {
                                    IstextboxEnable = true;
                                    nSHIIREKAKAKU = 0;
                                    nGenkaGokei = 0;
                                }

                            }
                            else
                            {
                                if (String.IsNullOrEmpty((GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("txtnGENKATANKA") as TextBox).Text) || (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("txtnGENKATANKA") as TextBox).Text == "0")
                                {
                                    if (!String.IsNullOrEmpty(dtable.Rows[0]["nSHIIREKAKAKU"].ToString()))
                                    {
                                        nSHIIREKAKAKU = Convert.ToDouble(dtable.Rows[0]["nSHIIREKAKAKU"].ToString());
                                        nGenkaGokei = nSHIIREKAKAKU * nSuryo;
                                    }
                                }
                                else
                                {
                                    nSHIIREKAKAKU = Convert.ToDouble((GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("txtnGENKATANKA") as TextBox).Text);
                                    nGenkaGokei = ((nHanbaikakaku / 100) * nkakeritsu) * nSuryo;
                                }
                            }
                        }
                        else
                        {
                            DataRow[] rows = dt_Syosai.Select("rowNo = '" + (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblRowNo") as Label).Text + "'");
                            if (rows.Length > 0)
                            {
                                foreach (var drow in rows)
                                {
                                    drow.Delete();
                                }

                                dt_Syosai.DefaultView.Sort = "rowNo asc";
                                dt_Syosai.AcceptChanges();
                            }

                            (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("btnSyohinShosai") as Button).Text = dtable_syohin_m.Rows.Count.ToString();

                            for (int i = 0; i < dtable_syohin_m.Rows.Count; i++)
                            {
                                DataRow dr = dt_Syosai.NewRow();
                                dr[0] = "0";
                                dr[1] = dtable_syohin_m.Rows[i]["cSYOUHIN"].ToString();
                                dr[2] = dtable_syohin_m.Rows[i]["sSYOUHIN"].ToString();
                                Double nsuryo = Convert.ToDouble(dtable_syohin_m.Rows[i]["nSYOUKISU"].ToString());
                                dr[3] = nsuryo.ToString("#,##0.##");
                                dr[4] = dtable_syohin_m.Rows[i]["sTANI"].ToString();
                                dr[5] = "0";
                                dr[6] = "0";
                                dr[7] = dtable_syohin_m.Rows[i]["nSHIIREKAKAKU"].ToString();
                                dr[8] = dtable_syohin_m.Rows[i]["GenkaGokei"].ToString();
                                Double tankaGokei = 0;
                                Double genkaGokei = Convert.ToDouble(dtable_syohin_m.Rows[i]["genkaGokei"].ToString());
                                Double arari = tankaGokei - genkaGokei;
                                dr[9] = arari;
                                double nArariSu_m = (arari / tankaGokei) * 100;
                                if (tankaGokei == 0)
                                {
                                    nArariSu_m = 0;
                                }
                                dr[10] = nArariSu_m.ToString("###0.0") + "%";
                                dr[11] = (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblRowNo") as Label).Text;
                                dr[12] = nkakeritsu + "%";
                                dr[13] = "0";
                                dr[14] = dtable_syohin_m.Rows[i]["fJITAIS"].ToString();
                                dt_Syosai.Rows.Add(dr);

                                nGenkaGokei += genkaGokei;
                            }

                            dt_Syosai.DefaultView.Sort = "rowNo asc";
                            dt_Syosai.AcceptChanges();

                            GV_Syosai.DataSource = dt_Syosai;
                            GV_Syosai.DataBind();

                            if (nGenkaGokei != 0)
                            {
                                IstextboxEnable = false;
                                if ((GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblfgenkatanka") as Label).Text == "0")
                                {
                                    nSHIIREKAKAKU = Convert.ToDouble(dtable.Rows[0]["nSHIIREKAKAKU"].ToString());
                                    nGenkaGokei = nSHIIREKAKAKU * nSuryo;
                                }
                                else
                                {
                                    if (nSuryo == 0)
                                    {
                                        nSHIIREKAKAKU = 0;
                                    }
                                    else
                                    {
                                        nSHIIREKAKAKU = nGenkaGokei / nSuryo;
                                    }
                                }
                            }
                            else
                            {
                               IstextboxEnable = true;
                                nSHIIREKAKAKU = 0;
                                nGenkaGokei = 0;
                            }
                        }

                        Double nArari = nTankaGokei - nGenkaGokei;

                        double nArariSu = (nArari / nTankaGokei) * 100;
                        if (nTankaGokei == 0)
                        {
                            nArariSu = 0;
                        }



                        (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("txtnTANKA") as TextBox).Text = nHanbaikakaku.ToString("#,##0.##");
                        (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblTankaGokei") as Label).Text = nTankaGokei.ToString("#,##0");
                        (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblTanka") as Label).Text = nSikiriTanka.ToString("#,##0");
                        (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("txtnGENKATANKA") as TextBox).Text = nSHIIREKAKAKU.ToString("#,##0.##");
                        (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblGenkaGokei") as Label).Text = nGenkaGokei.ToString("#,##0");
                        (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblnARARI") as Label).Text = nArari.ToString("#,##0");
                        (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblnARARISu") as Label).Text = nArariSu.ToString("###0.0") + "%";

                        (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("txtnTANKA") as TextBox).ToolTip = nHanbaikakaku.ToString("#,##0.##");
                        (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblTankaGokei") as Label).ToolTip = nTankaGokei.ToString("#,##0");
                        (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblTanka") as Label).ToolTip = nSikiriTanka.ToString("#,##0");
                        (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("txtnGENKATANKA") as TextBox).ToolTip = nSHIIREKAKAKU.ToString("#,##0.##");
                        (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblGenkaGokei") as Label).ToolTip = nGenkaGokei.ToString("#,##0");
                        (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblnARARI") as Label).ToolTip = nArari.ToString("#,##0");
                        (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblnARARISu") as Label).ToolTip = nArariSu.ToString("###0.0") + "%";

                        if (String.IsNullOrEmpty((GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblfgenkatanka") as Label).Text))
                        {
                            (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblfgenkatanka") as Label).Text = "1";
                        }

                    }
                    else
                    {
                        (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("txtcSYOHIN") as TextBox).Text = cSYOHIN;
                        (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("txtcSYOHIN") as TextBox).ToolTip = cSYOHIN;
                    }
                }

                GetTotalKingaku();

                #region updateDatatable
                Label lbl_status = (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblhdnStatus") as Label);
                Label lbl_fgenkataka = (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblfgenkatanka") as Label);
                Label lbl_rowNo = (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblRowNo") as Label);
                TextBox txt_cSyohin = (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("txtcSYOHIN") as TextBox);
                TextBox txt_sSyohin = (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("txtsSYOHIN") as TextBox);
                TextBox txt_nSyoryo = (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("txtnSURYO") as TextBox);
                //DropDownList ddl_cTani = (row.FindControl("DDL_cTANI") as DropDownList);
                TextBox txt_cTani = (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("txtTani") as TextBox);
                TextBox txt_nTanka = (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("txtnTANKA") as TextBox);
                Label lbl_TankaGokei = (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblTankaGokei") as Label);
                TextBox txt_nGenkaTanka = (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("txtnGENKATANKA") as TextBox);
                Label lbl_GenkaGokei = (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblGenkaGokei") as Label);
                Label lbl_Arari = (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblnARARI") as Label);
                Label lbl_ArariSu = (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblnARARISu") as Label);
                TextBox txt_nRITU = (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("txtnRITU") as TextBox);
                Label lbl_kubun = (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblKubun") as Label);
                Label lbl_nSIKIRITANKA = (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblTanka") as Label);
                Label lbl_fJITAIS = (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblfjitais") as Label);
                Label lbl_fJITAIQ = (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblfjitaiq") as Label);

                dt_SyohinOriginal.Rows[rowindex][0] = lbl_status.Text;
                dt_SyohinOriginal.Rows[rowindex][1] = txt_cSyohin.Text;
                dt_SyohinOriginal.Rows[rowindex][2] = txt_sSyohin.Text;
                dt_SyohinOriginal.Rows[rowindex][3] = txt_nSyoryo.Text;
                dt_SyohinOriginal.Rows[rowindex][4] = txt_cTani.Text;
                dt_SyohinOriginal.Rows[rowindex][5] = txt_nTanka.Text;
                dt_SyohinOriginal.Rows[rowindex][6] = lbl_TankaGokei.Text;
                dt_SyohinOriginal.Rows[rowindex][7] = txt_nGenkaTanka.Text;
                dt_SyohinOriginal.Rows[rowindex][8] = lbl_GenkaGokei.Text;
                dt_SyohinOriginal.Rows[rowindex][9] = lbl_Arari.Text;
                dt_SyohinOriginal.Rows[rowindex][10] = lbl_ArariSu.Text;
                dt_SyohinOriginal.Rows[rowindex][11] = lbl_fgenkataka.Text;
                dt_SyohinOriginal.Rows[rowindex][12] = lbl_rowNo.Text;
                dt_SyohinOriginal.Rows[rowindex][13] = txt_nRITU.Text;
                dt_SyohinOriginal.Rows[rowindex][14] = lbl_kubun.Text;
                dt_SyohinOriginal.Rows[rowindex][15] = lbl_nSIKIRITANKA.Text;
                dt_SyohinOriginal.Rows[rowindex][16] = lbl_fJITAIS.Text;
                dt_SyohinOriginal.Rows[rowindex][17] = lbl_fJITAIQ.Text;
                dt_SyohinOriginal.AcceptChanges();
                #endregion

                HF_fTankaChange.Value = HF_fTankaChange.Value.Replace(rowNo.ToString(), "");
                HF_fGenkaChange.Value = HF_fGenkaChange.Value.Replace(rowNo.ToString(), "");
                updMitsumoriSyohinGrid.Update();
            }
            
            dt_SyohinOriginal = SetMidashiSyokei(dt_SyohinOriginal);
            ViewState["SyouhinTable"] = dt_SyohinOriginal;
            GV_MitumoriSyohin_Original.DataSource = dt_SyohinOriginal;
            GV_MitumoriSyohin_Original.DataBind();
            setSyosaiCount(dt_SyohinOriginal, dt_Syosai);
            (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("txtnGENKATANKA") as TextBox).Enabled = IstextboxEnable;
            updMitsumoriSyohinGrid.Update();

            GV_MitumoriSyohin.DataSource = dt_SyohinOriginal;
            GV_MitumoriSyohin.DataBind();

            HasCheckRow();
            //SetSyosai();

            HF_isChange.Value = "1";

            ifShinkiPopup.Src = "";
            mpeShinkiPopup.Hide();
            updShinkiPopup.Update();
            updHeader.Update();
        }
        #endregion

        #region BT_ImageUpload_Click
        protected void BT_ImageUpload_Click(object sender, EventArgs e)
        {
            if (fileUpload1.HasFile)
            {
                fImageUpload = "3"; //ファイルパス
                BindImage();
            }
        }
        #endregion

        #region BT_HyoshiImageUpload_Click  //表紙画像アップロード
        protected void BT_HyoshiImageUpload_Click(object sender, EventArgs e)
        {
            if (fileUpload2.HasFile)
            {
                fImageUpload = "0"; //表紙
                BindImage();
            }
        }
        #endregion

        #region BT_HyoshiImgaeDrop_Click  //表紙画像Drag&Drop
        protected void BT_HyoshiImgaeDrop_Click(object sender, EventArgs e)
        {
            String fileName = HF_HyoshiFileName.Value;
            string extension = Path.GetExtension(fileName);
            if (extension.ToLower() == ".pdf" || extension.ToLower() == ".ai")
            {
                string base64String = HF_ImgBase64.Value;
                base64String = base64String.Replace("data:application/pdf;base64,", String.Empty);
                base64String = base64String.Replace("data:application/postscript;base64,", string.Empty);
                try
                {
                    byte[] pdfBytes = Convert.FromBase64String(base64String);

                    using (var inputMS = new MemoryStream(pdfBytes))
                    {
                        GhostscriptRasterizer rasterizer = null;

                        using (rasterizer = new GhostscriptRasterizer())
                        {
                            rasterizer.Open(inputMS);

                            using (MemoryStream ms = new MemoryStream())
                            {
                                int pagecount = rasterizer.PageCount;

                                if (pagecount == 1)
                                {
                                    System.Drawing.Image img = rasterizer.GetPage(200, 1);
                                    img.Save(ms, ImageFormat.Png);

                                    var SigBase64 = Convert.ToBase64String(ms.GetBuffer()); //Get Base64
                                    String imgurl = "data:image/png;base64," + SigBase64;
                                    HyoshiuploadedImage.Src = imgurl;
                                    HyoshiuploadedImage.Attributes.Add("class", "JC10DaiHyouImage");
                                    BT_HyoshiImgaeDelete.CssClass = "JC10ImageDelete";
                                    HyoshidragZone.Attributes.Add("class", "DisplayNone");
                                    updHeader.Update();
                                }
                                else
                                {
                                    SessionUtility.SetSession("HOME", "Master");
                                    HF_fImageUpload.Value = "0";
                                    Session["pageNumber"] = pagecount;
                                    //ifSentakuPopup.Style["width"] = "350px";
                                    //ifSentakuPopup.Style["height"] = "100px";
                                    ifSentakuPopup.Style["width"] = "100vw";
                                    ifSentakuPopup.Style["height"] = "100vh";

                                    String id = Request.QueryString["id"];
                                    ifSentakuPopup.Src = "JC35PdfPageChoice.aspx?id=" + id;
                                    mpeSentakuPopup.Show();
                                    updSentakuPopup.Update();
                                }
                            }


                            rasterizer.Close();
                        }
                    }

                    ////System.Drawing.Image image;
                    //using (MemoryStream ms = new MemoryStream(pdfBytes))
                    //{
                    //    PdfImage pdfImg = PdfImage.FromStream(ms);
                    //}
                }
                catch (Exception ex)
                {

                }

            }
            else
            {
                string base64String = HF_ImgBase64.Value;
                byte[] bytes = Convert.FromBase64String(base64String);

                System.Drawing.Image originalImage;
                using (MemoryStream ms = new MemoryStream(bytes))
                {
                    originalImage = System.Drawing.Image.FromStream(ms);
                }
                int introtate = 0;
                if (originalImage.PropertyIdList.Contains(0x0112))
                {
                    introtate = originalImage.GetPropertyItem(0x0112).Value[0];
                    switch (introtate)
                    {
                        case 1: // landscape, do nothing
                            break;

                        case 8: // rotated 90 right
                                // de-rotate:
                            originalImage.RotateFlip(rotateFlipType: RotateFlipType.Rotate270FlipNone);
                            break;

                        case 3: // bottoms up
                            originalImage.RotateFlip(rotateFlipType: RotateFlipType.Rotate180FlipNone);
                            break;

                        case 6: // rotated 90 left
                            originalImage.RotateFlip(rotateFlipType: RotateFlipType.Rotate90FlipNone);
                            break;
                    }
                }
                try
                {
                    var i2 = new Bitmap(originalImage);
                    EncoderParameters myEncoderParameters = new EncoderParameters(1);
                    EncoderParameter myEncoderParameter = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 75L);
                    myEncoderParameters.Param[0] = myEncoderParameter;
                    //i2.Save(folderPath + filename, GetEncoderInfo("image/jpeg"), myEncoderParameters);
                    //var i2 = new Bitmap(originalImage);
                    //i2.Save(folderPath + filename);
                    //originalImage.Save(folderPath + filename);
                }
                catch { }

                string imgurl = "";

                try
                {
                    using (var stream = new MemoryStream())
                    {
                        originalImage.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                        byte[] imageByte = stream.ToArray();
                        if (Convert.ToInt64(HF_ImgSize.Value) > 23552)//about 23KB
                        {
                            imageByte = ResizeImageFile(imageByte, 760);
                        }
                        base64String = Convert.ToBase64String(imageByte);
                        imgurl = "data:image/png;base64," + base64String;
                    }
                }
                catch
                {
                    base64String = HF_ImgBase64.Value;
                    imgurl = "data:image/png;base64," + base64String;
                }

                HyoshiuploadedImage.Src = imgurl;
                HyoshiuploadedImage.Attributes.Add("class", "JC10DaiHyouImage");
                BT_HyoshiImgaeDelete.CssClass = "JC10ImageDelete";
                HyoshidragZone.Attributes.Add("class", "DisplayNone");
                updHeader.Update();
            }
        }
        #endregion

        #region BT_Mitumorisho1ImgaeDrop_Click  //見積書1枚目画像Drag&Drop
        protected void BT_Mitumorisho1ImgaeDrop_Click(object sender, EventArgs e)
        {
            String fileName = HF_Gazo1FileName.Value;
            string extension = Path.GetExtension(fileName);
            if (extension.ToLower() == ".pdf" || extension.ToLower() == ".ai")
            {
                string base64String = HF_ImgBase64.Value;
                base64String = base64String.Replace("data:application/pdf;base64,", String.Empty);
                base64String = base64String.Replace("data:application/postscript;base64,", string.Empty);
                try
                {
                    byte[] pdfBytes = Convert.FromBase64String(base64String);

                    using (var inputMS = new MemoryStream(pdfBytes))
                    {
                        GhostscriptRasterizer rasterizer = null;

                        using (rasterizer = new GhostscriptRasterizer())
                        {
                            rasterizer.Open(inputMS);

                            using (MemoryStream ms = new MemoryStream())
                            {
                                int pagecount = rasterizer.PageCount;
                                if (pagecount == 1)
                                {
                                    System.Drawing.Image img = rasterizer.GetPage(200, 1);
                                    img.Save(ms, ImageFormat.Png);

                                    var SigBase64 = Convert.ToBase64String(ms.GetBuffer()); //Get Base64
                                    String imgurl = "data:image/png;base64," + SigBase64;
                                    Mitumorisho1uploadedImage.Src = imgurl;
                                    Mitumorisho1uploadedImage.Attributes.Add("class", "JC10DaiHyouImage");
                                    BT_Mitumorisho1ImgaeDelete.CssClass = "JC10ImageDelete";
                                    Mitumorisho1dragZone.Attributes.Add("class", "DisplayNone");
                                    updHeader.Update();
                                }
                                else
                                {
                                    SessionUtility.SetSession("HOME", "Master");
                                    HF_fImageUpload.Value = "1";
                                    Session["pageNumber"] = pagecount;
                                    //ifSentakuPopup.Style["width"] = "350px";
                                    //ifSentakuPopup.Style["height"] = "100px";
                                    ifSentakuPopup.Style["width"] = "100vw";
                                    ifSentakuPopup.Style["height"] = "100vh";
                                    String id = Request.QueryString["id"];
                                    ifSentakuPopup.Src = "JC35PdfPageChoice.aspx?id=" + id;
                                    mpeSentakuPopup.Show();
                                    updSentakuPopup.Update();
                                }
                            }


                            rasterizer.Close();
                        }
                    }

                    ////System.Drawing.Image image;
                    //using (MemoryStream ms = new MemoryStream(pdfBytes))
                    //{
                    //    PdfImage pdfImg = PdfImage.FromStream(ms);
                    //}
                }
                catch (Exception ex)
                {

                }

            }
            else
            {
                string base64String = HF_ImgBase64.Value;
                byte[] bytes = Convert.FromBase64String(base64String);

                System.Drawing.Image originalImage;
                using (MemoryStream ms = new MemoryStream(bytes))
                {
                    originalImage = System.Drawing.Image.FromStream(ms);
                }
                int introtate = 0;
                if (originalImage.PropertyIdList.Contains(0x0112))
                {
                    introtate = originalImage.GetPropertyItem(0x0112).Value[0];
                    switch (introtate)
                    {
                        case 1: // landscape, do nothing
                            break;

                        case 8: // rotated 90 right
                                // de-rotate:
                            originalImage.RotateFlip(rotateFlipType: RotateFlipType.Rotate270FlipNone);
                            break;

                        case 3: // bottoms up
                            originalImage.RotateFlip(rotateFlipType: RotateFlipType.Rotate180FlipNone);
                            break;

                        case 6: // rotated 90 left
                            originalImage.RotateFlip(rotateFlipType: RotateFlipType.Rotate90FlipNone);
                            break;
                    }
                }
                try
                {
                    var i2 = new Bitmap(originalImage);
                    EncoderParameters myEncoderParameters = new EncoderParameters(1);
                    EncoderParameter myEncoderParameter = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 75L);
                    myEncoderParameters.Param[0] = myEncoderParameter;
                    //i2.Save(folderPath + filename, GetEncoderInfo("image/jpeg"), myEncoderParameters);
                    //var i2 = new Bitmap(originalImage);
                    //i2.Save(folderPath + filename);
                    //originalImage.Save(folderPath + filename);
                }
                catch { }

                string imgurl = "";

                try
                {
                    using (var stream = new MemoryStream())
                    {
                        originalImage.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                        byte[] imageByte = stream.ToArray();
                        if (Convert.ToInt64(HF_ImgSize.Value) > 23552)//about 23KB
                        {
                            imageByte = ResizeImageFile(imageByte, 760);
                        }
                        base64String = Convert.ToBase64String(imageByte);
                        imgurl = "data:image/png;base64," + base64String;
                    }
                }
                catch
                {
                    base64String = HF_ImgBase64.Value;
                    imgurl = "data:image/png;base64," + base64String;
                }

                Mitumorisho1uploadedImage.Src = imgurl;
                Mitumorisho1uploadedImage.Attributes.Add("class", "JC10DaiHyouImage");
                BT_Mitumorisho1ImgaeDelete.CssClass = "JC10ImageDelete";
                Mitumorisho1dragZone.Attributes.Add("class", "DisplayNone");
                updHeader.Update();
            }
        }
        #endregion

        #region BT_Mitumorisho2ImgaeDrop_Click  //見積書1枚目画像Drag&Drop
        protected void BT_Mitumorisho2ImgaeDrop_Click(object sender, EventArgs e)
        {
            String fileName = HF_Gazo2FileName.Value;
            string extension = Path.GetExtension(fileName);
            if (extension.ToLower() == ".pdf" || extension.ToLower() == ".ai")
            {
                string base64String = HF_ImgBase64.Value;
                base64String = base64String.Replace("data:application/pdf;base64,", String.Empty);
                base64String = base64String.Replace("data:application/postscript;base64,", string.Empty);
                try
                {
                    byte[] pdfBytes = Convert.FromBase64String(base64String);

                    using (var inputMS = new MemoryStream(pdfBytes))
                    {
                        GhostscriptRasterizer rasterizer = null;

                        using (rasterizer = new GhostscriptRasterizer())
                        {
                            rasterizer.Open(inputMS);

                            using (MemoryStream ms = new MemoryStream())
                            {
                                int pagecount = rasterizer.PageCount;
                                if (pagecount == 1)
                                {
                                    System.Drawing.Image img = rasterizer.GetPage(200, 1);
                                    img.Save(ms, ImageFormat.Png);

                                    var SigBase64 = Convert.ToBase64String(ms.GetBuffer()); //Get Base64
                                    String imgurl = "data:image/png;base64," + SigBase64;
                                    Mitumorisho2uploadedImage.Src = imgurl;
                                    Mitumorisho2uploadedImage.Attributes.Add("class", "JC10DaiHyouImage");
                                    BT_Mitumorisho2ImgaeDelete.CssClass = "JC10ImageDelete";
                                    Mitumorisho2dragZone.Attributes.Add("class", "DisplayNone");
                                    updHeader.Update();
                                }
                                else
                                {
                                    SessionUtility.SetSession("HOME", "Master");
                                    HF_fImageUpload.Value = "2";
                                    Session["pageNumber"] = pagecount;
                                    //ifSentakuPopup.Style["width"] = "350px";
                                    //ifSentakuPopup.Style["height"] = "100px";
                                    ifSentakuPopup.Style["width"] = "100vw";
                                    ifSentakuPopup.Style["height"] = "100vh";
                                    String id = Request.QueryString["id"];
                                    ifSentakuPopup.Src = "JC35PdfPageChoice.aspx?id=" + id;
                                    mpeSentakuPopup.Show();
                                    updSentakuPopup.Update();
                                }
                            }


                            rasterizer.Close();
                        }
                    }

                    ////System.Drawing.Image image;
                    //using (MemoryStream ms = new MemoryStream(pdfBytes))
                    //{
                    //    PdfImage pdfImg = PdfImage.FromStream(ms);
                    //}
                }
                catch (Exception ex)
                {

                }

            }
            else
            {
                string base64String = HF_ImgBase64.Value;
                byte[] bytes = Convert.FromBase64String(base64String);

                System.Drawing.Image originalImage;
                using (MemoryStream ms = new MemoryStream(bytes))
                {
                    originalImage = System.Drawing.Image.FromStream(ms);
                }
                int introtate = 0;
                if (originalImage.PropertyIdList.Contains(0x0112))
                {
                    introtate = originalImage.GetPropertyItem(0x0112).Value[0];
                    switch (introtate)
                    {
                        case 1: // landscape, do nothing
                            break;

                        case 8: // rotated 90 right
                                // de-rotate:
                            originalImage.RotateFlip(rotateFlipType: RotateFlipType.Rotate270FlipNone);
                            break;

                        case 3: // bottoms up
                            originalImage.RotateFlip(rotateFlipType: RotateFlipType.Rotate180FlipNone);
                            break;

                        case 6: // rotated 90 left
                            originalImage.RotateFlip(rotateFlipType: RotateFlipType.Rotate90FlipNone);
                            break;
                    }
                }
                try
                {
                    var i2 = new Bitmap(originalImage);
                    EncoderParameters myEncoderParameters = new EncoderParameters(1);
                    EncoderParameter myEncoderParameter = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 75L);
                    myEncoderParameters.Param[0] = myEncoderParameter;
                    //i2.Save(folderPath + filename, GetEncoderInfo("image/jpeg"), myEncoderParameters);
                    //var i2 = new Bitmap(originalImage);
                    //i2.Save(folderPath + filename);
                    //originalImage.Save(folderPath + filename);
                }
                catch { }

                string imgurl = "";

                try
                {
                    using (var stream = new MemoryStream())
                    {
                        originalImage.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                        byte[] imageByte = stream.ToArray();
                        if (Convert.ToInt64(HF_ImgSize.Value) > 23552)//about 23KB
                        {
                            imageByte = ResizeImageFile(imageByte, 760);
                        }
                        base64String = Convert.ToBase64String(imageByte);
                        imgurl = "data:image/png;base64," + base64String;
                    }
                }
                catch
                {
                    base64String = HF_ImgBase64.Value;
                    imgurl = "data:image/png;base64," + base64String;
                }

                Mitumorisho2uploadedImage.Src = imgurl;
                Mitumorisho2uploadedImage.Attributes.Add("class", "JC10DaiHyouImage");
                BT_Mitumorisho2ImgaeDelete.CssClass = "JC10ImageDelete";
                Mitumorisho2dragZone.Attributes.Add("class", "DisplayNone");
                updHeader.Update();
            }

        }
        #endregion

        #region BT_Mitumorisyo1ImageUpload_Click　　//見積書1枚目画像アップロード
        protected void BT_Mitumorisyo1ImageUpload_Click(object sender, EventArgs e)
        {
            if (fileUpload3.HasFile)
            {
                fImageUpload = "1"; //見積書1枚目
                BindImage();
            }
        }
        #endregion

        #region BT_Mitumorisyo2Upload_Click　　//見積書2枚目画像アップロード 
        protected void BT_Mitumorisyo2Upload_Click(object sender, EventArgs e)
        {
            if (fileUpload4.HasFile)
            {
                fImageUpload = "2"; ////見積書2枚目
                BindImage();
            }
        }
        #endregion

        #region 見積登録＆明細
        protected void btnSyohinShosai_Click(object sender, EventArgs e)
        {
            var btn_syohin = sender as Button;
            GridViewRow gvr = (GridViewRow)btn_syohin.NamingContainer;
            int rowindex = gvr.RowIndex;
            Session["syohinSelectRowIndex"] = rowindex.ToString();
            DataTable dt_Meisai = GetGridViewData();
            Session["SyohinMeisaidt"] = dt_Meisai;

            DataTable dt_Syosai = GetSyosaiGridViewData();
            Session["SyohinSyosaidt"] = dt_Syosai;

            SessionUtility.SetSession("HOME", "Master");
            String id = Request.QueryString["id"];
            ifShinkiPopup.Src = "JC11MitsumoriSyousai.aspx?id=" + id;
            mpeShinkiPopup.Show();

            updShinkiPopup.Update();

        }
        #endregion

        #region btn_CloseSearch_Click
        protected void btn_CloseSearch_Click(object sender, EventArgs e)
        {
            ifShinkiPopup.Src = "";
            mpeShinkiPopup.Hide();
            updShinkiPopup.Update();
        }
        #endregion

        #region btn_getSyosai_Click  見積登録＆明細サブ画面を閉じる時のフォーカス処理
        protected void btn_getSyosai_Click(object sender, EventArgs e)
        {
            if (Session["SyohinMeisaidt"] != null)
            {
                DataTable dt_Meisai = Session["SyohinMeisaidt"] as DataTable;
                DataTable dt_Syosai = Session["SyohinSyosaidt"] as DataTable;

                var max = dt_Meisai.AsEnumerable().Max(x => int.Parse(x.Field<string>("rowNo")));
                HF_maxRowNo.Value = max.ToString();

                GV_MitumoriSyohin_Original.DataSource = dt_Meisai;
                GV_MitumoriSyohin_Original.DataBind();

                ViewState["SyouhinTable"] = dt_Meisai;

                setSyosaiCount(dt_Meisai, dt_Syosai);
                updMitsumoriSyohinGrid.Update();

                GV_Syosai.DataSource = dt_Syosai;
                GV_Syosai.DataBind();

                HasCheckRow();
                HF_isChange.Value = "1";
                updMitsumoriSyohinGrid.Update();
            }
            ViewState["SyouhinTable"] = null;
            DataTable dt_SyohinOriginal = GetGridViewData();
            GV_MitumoriSyohin.DataSource = dt_SyohinOriginal;
            GV_MitumoriSyohin.DataBind();
            GetTotalKingaku();
            //SetSyosai();
            updMitsumoriSyohinGrid.Update();

            ifShinkiPopup.Src = "";
            mpeShinkiPopup.Hide();
            updShinkiPopup.Update();
            updHeader.Update();
        }
        #endregion

        #region setSyosaiCount
        private void setSyosaiCount(DataTable dt_Meisai, DataTable dt_Syosai)
        {
            for (int i = 1; i <= dt_Syosai.Rows.Count; i++)
            {
                int rowNo = Convert.ToInt32(dt_Syosai.Rows[i - 1]["rowNO"].ToString());

                DataRow[] rows = dt_Syosai.Select("rowNo = '" + rowNo.ToString() + "'");
                if (rows.Length > 0)
                {
                    DataRow[] rows_Meisai = dt_Meisai.Select("rowNo = '" + rowNo.ToString() + "'");
                    if (rows_Meisai.Length > 0)
                    {
                        int gyoNO = dt_Meisai.Rows.IndexOf(rows_Meisai[0]);
                        //String SyousaiCount = (GV_MitumoriSyohin.Rows[gyoNO].FindControl("btnSyohinShosai") as Button).Text;
                        (GV_MitumoriSyohin_Original.Rows[gyoNO].FindControl("btnSyohinShosai") as Button).Text = rows.Length.ToString();
                        Double nGenkaGokei = 0;
                        Double nTankaGokei = 0;
                        Double nSikiriTanka = 0;

                        Double nSHIIREKAKAKU = 0;
                        Double nSHIKIRIKAKAKU = 0;
                        double nSuryo = 0;
                        if (!String.IsNullOrEmpty((GV_MitumoriSyohin_Original.Rows[gyoNO].FindControl("txtnSURYO") as TextBox).Text))
                        {
                            nSuryo = Convert.ToDouble((GV_MitumoriSyohin_Original.Rows[gyoNO].FindControl("txtnSURYO") as TextBox).Text);
                        }
                        foreach (var drow in rows)
                        {
                            Double genka = 0;
                            Double tanka = 0;
                            if (!String.IsNullOrEmpty(drow[8].ToString()))
                            {
                                genka = Convert.ToDouble(drow[8].ToString());
                            }
                            if (!String.IsNullOrEmpty(drow[6].ToString()))
                            {
                                tanka = Convert.ToDouble(drow[6].ToString());
                            }
                            nGenkaGokei += genka;
                            nTankaGokei += tanka;
                        }

                        if (/*nTankaGokei != 0 && */(GV_MitumoriSyohin_Original.Rows[gyoNO].FindControl("lblfgenkatanka") as Label).Text == "0")
                        {
                            if (nTankaGokei > 0)
                            {
                                (GV_MitumoriSyohin_Original.Rows[gyoNO].FindControl("txtnTANKA") as TextBox).Enabled = false;
                                nSHIKIRIKAKAKU = nTankaGokei;
                                nTankaGokei = nSHIKIRIKAKAKU * nSuryo;
                                nSikiriTanka = nSHIKIRIKAKAKU;

                                (GV_MitumoriSyohin_Original.Rows[gyoNO].FindControl("txtnTANKA") as TextBox).Text = nSHIKIRIKAKAKU.ToString("#,##0.##");
                                (GV_MitumoriSyohin_Original.Rows[gyoNO].FindControl("lblTankaGokei") as Label).Text = nTankaGokei.ToString("#,##0");
                                (GV_MitumoriSyohin_Original.Rows[gyoNO].FindControl("lblTanka") as Label).Text = nSikiriTanka.ToString("#,##0");

                                (GV_MitumoriSyohin_Original.Rows[gyoNO].FindControl("txtnTANKA") as TextBox).ToolTip = nSHIKIRIKAKAKU.ToString("#,##0.##");
                                (GV_MitumoriSyohin_Original.Rows[gyoNO].FindControl("lblTankaGokei") as Label).ToolTip = nTankaGokei.ToString("#,##0");
                                (GV_MitumoriSyohin_Original.Rows[gyoNO].FindControl("lblTanka") as Label).ToolTip = nSikiriTanka.ToString("#,##0");

                                String rowNo1 = (GV_MitumoriSyohin_Original.Rows[gyoNO].FindControl("lblRowNo") as Label).Text;
                                String tankachangeIndex = HF_fTankaChange.Value;
                                string[] textchangeIndex = HF_fTankaChange.Value.Split(',');
                                textchangeIndex = textchangeIndex.Where(x => !string.IsNullOrEmpty(x)).ToArray();
                                var results = Array.FindAll(textchangeIndex, s => s.Equals(rowNo1));
                                if (results.Length <= 0)
                                {
                                    HF_fTankaChange.Value = tankachangeIndex + rowNo1 + ",";
                                }
                            }
                            else
                            {
                                (GV_MitumoriSyohin_Original.Rows[gyoNO].FindControl("txtnTANKA") as TextBox).Enabled = true;
                                String rowNo1 = (GV_MitumoriSyohin_Original.Rows[gyoNO].FindControl("lblRowNo") as Label).Text;
                                string[] textchangeIndex = HF_fTankaChange.Value.Split(',');
                                textchangeIndex = textchangeIndex.Where(x => !string.IsNullOrEmpty(x)).ToArray();
                                var results = Array.FindAll(textchangeIndex, s => s.Equals(rowNo1));
                                if (results.Length > 0)
                                {
                                    nSHIKIRIKAKAKU = nTankaGokei;
                                    nTankaGokei = nSHIKIRIKAKAKU * nSuryo;
                                    nSikiriTanka = nSHIKIRIKAKAKU;

                                    (GV_MitumoriSyohin_Original.Rows[gyoNO].FindControl("txtnTANKA") as TextBox).Text = nSHIKIRIKAKAKU.ToString("#,##0.##");
                                    (GV_MitumoriSyohin_Original.Rows[gyoNO].FindControl("lblTankaGokei") as Label).Text = nTankaGokei.ToString("#,##0");
                                    (GV_MitumoriSyohin_Original.Rows[gyoNO].FindControl("lblTanka") as Label).Text = nSikiriTanka.ToString("#,##0");

                                    (GV_MitumoriSyohin_Original.Rows[gyoNO].FindControl("txtnTANKA") as TextBox).ToolTip = nSHIKIRIKAKAKU.ToString("#,##0.##");
                                    (GV_MitumoriSyohin_Original.Rows[gyoNO].FindControl("lblTankaGokei") as Label).ToolTip = nTankaGokei.ToString("#,##0");
                                    (GV_MitumoriSyohin_Original.Rows[gyoNO].FindControl("lblTanka") as Label).ToolTip = nSikiriTanka.ToString("#,##0");
                                }
                            }

                        }
                        else
                        {
                            String rowNo1 = (GV_MitumoriSyohin_Original.Rows[gyoNO].FindControl("lblRowNo") as Label).Text;
                            string[] textchangeIndex = HF_fTankaChange.Value.Split(',');
                            textchangeIndex = textchangeIndex.Where(x => !string.IsNullOrEmpty(x)).ToArray();
                            var results = Array.FindAll(textchangeIndex, s => s.Equals(rowNo1));
                            if (results.Length > 0)
                            {
                                if (nTankaGokei > 0)
                                {
                                    (GV_MitumoriSyohin_Original.Rows[gyoNO].FindControl("txtnTANKA") as TextBox).Enabled = false;
                                }
                                else
                                {
                                    (GV_MitumoriSyohin_Original.Rows[gyoNO].FindControl("txtnTANKA") as TextBox).Enabled = true;
                                }
                                nSHIKIRIKAKAKU = nTankaGokei;
                                nTankaGokei = nSHIKIRIKAKAKU * nSuryo;
                                nSikiriTanka = nSHIKIRIKAKAKU;

                                (GV_MitumoriSyohin_Original.Rows[gyoNO].FindControl("txtnTANKA") as TextBox).Text = nSHIKIRIKAKAKU.ToString("#,##0.##");
                                (GV_MitumoriSyohin_Original.Rows[gyoNO].FindControl("lblTankaGokei") as Label).Text = nTankaGokei.ToString("#,##0");
                                (GV_MitumoriSyohin_Original.Rows[gyoNO].FindControl("lblTanka") as Label).Text = nSikiriTanka.ToString("#,##0");

                                (GV_MitumoriSyohin_Original.Rows[gyoNO].FindControl("txtnTANKA") as TextBox).ToolTip = nSHIKIRIKAKAKU.ToString("#,##0.##");
                                (GV_MitumoriSyohin_Original.Rows[gyoNO].FindControl("lblTankaGokei") as Label).ToolTip = nTankaGokei.ToString("#,##0");
                                (GV_MitumoriSyohin_Original.Rows[gyoNO].FindControl("lblTanka") as Label).ToolTip = nSikiriTanka.ToString("#,##0");
                            }
                        }

                        if ((GV_MitumoriSyohin_Original.Rows[gyoNO].FindControl("lblfgenkatanka") as Label).Text == "0")
                        {
                            nSHIIREKAKAKU = nGenkaGokei;
                            nGenkaGokei = nSHIIREKAKAKU * nSuryo;
                        }
                        else
                        {
                            if (nSuryo == 0)
                            {
                                nSHIIREKAKAKU = 0;
                                //nGenkaGokei = 0;
                            }
                            else
                            {
                                nSHIIREKAKAKU = nGenkaGokei / nSuryo;
                            }
                        }

                        nTankaGokei = 0;
                        if (!String.IsNullOrEmpty((GV_MitumoriSyohin_Original.Rows[gyoNO].FindControl("lblTankaGokei") as Label).Text))
                        {
                            nTankaGokei = Convert.ToDouble((GV_MitumoriSyohin_Original.Rows[gyoNO].FindControl("lblTankaGokei") as Label).Text);
                        }

                        Double nArari = nTankaGokei - nGenkaGokei;

                        double nArariSu = (nArari / nTankaGokei) * 100;
                        if (nTankaGokei == 0)
                        {
                            nArariSu = 0;
                        }

                        //if (SyousaiCount!="詳")
                        //{
                        String rowNo2 = (GV_MitumoriSyohin_Original.Rows[gyoNO].FindControl("lblRowNo") as Label).Text;
                        string[] textchangeIndex1 = HF_fGenkaChange.Value.Split(',');
                        textchangeIndex1 = textchangeIndex1.Where(x => !string.IsNullOrEmpty(x)).ToArray();
                        var results1 = Array.FindAll(textchangeIndex1, s => s.Equals(rowNo2));

                        if (results1.Length > 0)
                        {
                            if (nGenkaGokei != 0)
                            {
                                (GV_MitumoriSyohin_Original.Rows[gyoNO].FindControl("txtnGENKATANKA") as TextBox).Enabled = false;
                            }
                            else
                            {
                                (GV_MitumoriSyohin_Original.Rows[gyoNO].FindControl("txtnGENKATANKA") as TextBox).Enabled = true;
                            }
                            (GV_MitumoriSyohin_Original.Rows[gyoNO].FindControl("txtnGENKATANKA") as TextBox).Text = nSHIIREKAKAKU.ToString("#,##0.##");
                            (GV_MitumoriSyohin_Original.Rows[gyoNO].FindControl("lblGenkaGokei") as Label).Text = nGenkaGokei.ToString("#,##0");
                            (GV_MitumoriSyohin_Original.Rows[gyoNO].FindControl("lblnARARI") as Label).Text = nArari.ToString("#,##0");
                            (GV_MitumoriSyohin_Original.Rows[gyoNO].FindControl("lblnARARISu") as Label).Text = nArariSu.ToString("###0.0") + "%";

                            (GV_MitumoriSyohin_Original.Rows[gyoNO].FindControl("txtnGENKATANKA") as TextBox).ToolTip = nSHIIREKAKAKU.ToString("#,##0.##");
                            (GV_MitumoriSyohin_Original.Rows[gyoNO].FindControl("lblGenkaGokei") as Label).ToolTip = nGenkaGokei.ToString("#,##0");
                            (GV_MitumoriSyohin_Original.Rows[gyoNO].FindControl("lblnARARI") as Label).ToolTip = nArari.ToString("#,##0");
                            (GV_MitumoriSyohin_Original.Rows[gyoNO].FindControl("lblnARARISu") as Label).ToolTip = nArariSu.ToString("###0.0") + "%";
                        }
                        else
                        {
                            if (nGenkaGokei > 0)
                            {
                                (GV_MitumoriSyohin_Original.Rows[gyoNO].FindControl("txtnGENKATANKA") as TextBox).Enabled = false;
                                (GV_MitumoriSyohin_Original.Rows[gyoNO].FindControl("txtnGENKATANKA") as TextBox).Text = nSHIIREKAKAKU.ToString("#,##0.##");
                                (GV_MitumoriSyohin_Original.Rows[gyoNO].FindControl("lblGenkaGokei") as Label).Text = nGenkaGokei.ToString("#,##0");
                                (GV_MitumoriSyohin_Original.Rows[gyoNO].FindControl("lblnARARI") as Label).Text = nArari.ToString("#,##0");
                                (GV_MitumoriSyohin_Original.Rows[gyoNO].FindControl("lblnARARISu") as Label).Text = nArariSu.ToString("###0.0") + "%";

                                (GV_MitumoriSyohin_Original.Rows[gyoNO].FindControl("txtnGENKATANKA") as TextBox).ToolTip = nSHIIREKAKAKU.ToString("#,##0.##");
                                (GV_MitumoriSyohin_Original.Rows[gyoNO].FindControl("lblGenkaGokei") as Label).ToolTip = nGenkaGokei.ToString("#,##0");
                                (GV_MitumoriSyohin_Original.Rows[gyoNO].FindControl("lblnARARI") as Label).ToolTip = nArari.ToString("#,##0");
                                (GV_MitumoriSyohin_Original.Rows[gyoNO].FindControl("lblnARARISu") as Label).ToolTip = nArariSu.ToString("###0.0") + "%";

                                HF_fGenkaChange.Value = HF_fGenkaChange.Value + rowNo2 + ",";
                            }
                            else
                            {
                                (GV_MitumoriSyohin_Original.Rows[gyoNO].FindControl("txtnGENKATANKA") as TextBox).Enabled = true;
                            }

                        }
                        //}
                    }
                    updMitsumoriSyohinGrid.Update();

                }
            }
        }
        #endregion

        #region 粗利率一括設定
        protected void btnAraritsuIkkatsu_Click(object sender, EventArgs e)
        {
            SessionUtility.SetSession("HOME", "Master");
            ifSentakuPopup.Style["width"] = "440px";
            ifSentakuPopup.Style["height"] = "450px";
            String id = Request.QueryString["id"];
            ifSentakuPopup.Src = "JC32ArarisuIkkatsuSetting.aspx?id=" + id;
            mpeSentakuPopup.Show();
            //lblsShihariai.Attributes.Add("onClick", "BtnClick('MainContent_btnShiharai')");
            updSentakuPopup.Update();
        }
        #endregion

        #region 見積詳細を表示
        protected void BT_MitsumoriSyosai_Click(object sender, EventArgs e)
        {
            if (DIV_MitsumoriSyosai.Visible == true)
            {
                BT_MitsumoriSyosai.Style.Add("background-image", "url('../Img/expand-more-1782315-1514165.png')");
                DIV_MitsumoriSyosai.Visible = false;
            }
            else
            {
                BT_MitsumoriSyosai.Style.Add("background-image", "url('../Img/expand-less-1781206-1518580.png')");
                DIV_MitsumoriSyosai.Visible = true;
            }
        }
        #endregion

        #region btnDisplayItemSetting_Click  表示項目を設定

        protected void btnDisplayItemSetting_Click(object sender, EventArgs e)
        {
            SessionUtility.SetSession("HOME", "Master");
            Session["HyoujiID"] = "mitsumorisyosai";
            Session["HyoujiSettingID"] = "mitsumorisyosai";
            ifpnlHyoujiSetPopUp.Attributes.Add("class", "HyoujiSettingIframe shouhiniframeStyle");
            String id = Request.QueryString["id"];
            ifpnlHyoujiSetPopUp.Src = "JC08HyoujiSetting.aspx?id=" + id;
            mpeHyoujiSetPopUp.Show();
            updHyoujiSet.Update();
        }
        #endregion

        #region btnArarisuIkkatsu_Click 粗利率一括設定サブ画面を閉じる時のフォーカス処理
        protected void btnArarisuIkkatsu_Click(object sender, EventArgs e)
        {
            if (Session["riritsu"] != null)
            {
                nriritsu = Convert.ToDecimal(Session["riritsu"].ToString());
                fuwagakim = Convert.ToBoolean(Session["fuwagakimeisai"].ToString());
                nkisuu = Convert.ToInt32(Session["kisuu"].ToString());
                nshisyagonyuu = Convert.ToInt32(Session["shisyagonyuu"].ToString());
                fuwagakis = Convert.ToBoolean(Session["fuwagakisyousai"].ToString());
                nGYOUZENTAI = Convert.ToInt32(Session["GYOUZENTAI"].ToString());
                Syousai_All = new DataTable();
                Syousai_All = GetSyosaiGridViewData();

                ELT_KeiSan_ARARI_KINGAKU(nriritsu, fuwagakim, nkisuu.ToString(), nshisyagonyuu.ToString(), fuwagakis, nGYOUZENTAI);

                GetTotalKingaku();
                HF_isChange.Value = "1";
                GV_Syosai.DataSource = Syousai_All;
                GV_Syosai.DataBind();

                DataTable dt_SyohinOriginal = GetGridViewData();
                GV_MitumoriSyohin.DataSource = dt_SyohinOriginal;
                GV_MitumoriSyohin.DataBind();
                //SetSyosai();

                updMitsumoriSyohinGrid.Update();
                updHeader.Update();
            }

            ifShinkiPopup.Src = "";
            mpeShinkiPopup.Hide();
            updShinkiPopup.Update();
            updHeader.Update();
        }
        #endregion

        #region　粗利率と原単価から単価【標準単価】計算
        private void ELT_KeiSan_ARARI_KINGAKU(decimal riritsu, bool uwagakim, string kisuu, string shisyagonyuu, bool uwagakis, int ngyouzentai)
        {

            if (ngyouzentai == 0)
            {
                if (GV_MitumoriSyohin_Original.Rows.Count > 0)
                {
                    Boolean check = false;
                    foreach (GridViewRow row in GV_MitumoriSyohin_Original.Rows)
                    {
                        //CheckBox chk = (row.FindControl("chkSelectSyouhin") as CheckBox);
                        Label lbl_status = (row.FindControl("lblhdnStatus") as Label);
                        Label lbl_kubun = (row.FindControl("lblKubun") as Label);
                        if (lbl_status.Text == "1")
                        {
                            if (!lbl_kubun.Text.ToString().Equals("見") && !lbl_kubun.Text.ToString().Equals("計"))
                            {
                                check = true;
                                Label lbl_fgenkataka = (row.FindControl("lblfgenkatanka") as Label);
                                Label lbl_RowNo = (row.FindControl("lblRowNo") as Label);

                                if (!String.IsNullOrEmpty(lbl_fgenkataka.Text))
                                {
                                    if (lbl_fgenkataka.Text != "1")
                                    {
                                        DataTable dbl_syousaibu = new DataTable();
                                        dbl_syousaibu = Syousai_All.Select("rowNo = '" + lbl_RowNo.Text + "'").CopyToDataTable();
                                        if (dbl_syousaibu.Rows.Count > 0)
                                        {
                                            DataTable dbl_syousaibu1 = new DataTable();
                                            try
                                            {
                                                dbl_syousaibu1 = dbl_syousaibu.Select("nTANKA<>'0.00' and nTANKA<>'0'").CopyToDataTable();
                                            }
                                            catch { }
                                            if (dbl_syousaibu1.Rows.Count > 0)
                                            {
                                                //2
                                                // MessageBox.Show("単価原価方式（詳細行の標準単価に0円でない金額あり）");
                                                keisan_syousai_data(riritsu, uwagakis, kisuu, shisyagonyuu, row);
                                            }
                                            else
                                            {
                                                //1
                                                //MessageBox.Show("総原価方式と単価原価方式（詳細行の標準単価が0円もしくは、詳細データがない）の場合");
                                                keisan_meisai_data(riritsu, uwagakim, kisuu, shisyagonyuu, row);
                                            }
                                        }
                                        else
                                        {
                                            //1
                                            //MessageBox.Show("総原価方式と単価原価方式（詳細行の標準単価が0円もしくは、詳細データがない）の場合");
                                            keisan_meisai_data(riritsu, uwagakim, kisuu, shisyagonyuu, row);
                                        }
                                    }
                                    else
                                    {
                                        //1
                                        //MessageBox.Show("総原価方式と単価原価方式（詳細行の標準単価が0円もしくは、詳細データがない）の場合");
                                        keisan_meisai_data(riritsu, uwagakim, kisuu, shisyagonyuu, row);
                                    }
                                }
                                else
                                {
                                    //1
                                    //MessageBox.Show("総原価方式と単価原価方式（詳細行の標準単価が0円もしくは、詳細データがない）の場合");
                                    keisan_meisai_data(riritsu, uwagakim, kisuu, shisyagonyuu, row);
                                }
                            }
                        }
                    }
                    if (check == false)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowErrorMessage",
                    "ShowErrorMessage('行を選択してください。');", true);
                    }
                }
            }
            else
            {
                foreach (GridViewRow row in GV_MitumoriSyohin_Original.Rows)
                {
                    Label lbl_fgenkataka = (row.FindControl("lblfgenkatanka") as Label);
                    Label lbl_RowNo = (row.FindControl("lblRowNo") as Label);
                    Label lbl_kubun = (row.FindControl("lblKubun") as Label);
                    if (!lbl_kubun.Text.ToString().Equals("見") && !lbl_kubun.Text.ToString().Equals("計"))
                    {
                        if (!String.IsNullOrEmpty(lbl_fgenkataka.Text))
                        {
                            if (lbl_fgenkataka.Text != "1")
                            {
                                DataTable dbl_syousaibu = new DataTable();
                                
                                dbl_syousaibu = Syousai_All.Select("rowNo = '" + lbl_RowNo.Text + "'").CopyToDataTable();
                               
                                if (dbl_syousaibu.Rows.Count > 0)
                                {
                                    DataTable dbl_syousaibu1 = new DataTable();
                                    try
                                    {
                                        dbl_syousaibu1 = dbl_syousaibu.Select("nTANKA<>'0.00' and nTANKA<>'0'").CopyToDataTable();
                                    }
                                    catch { }
                                    if (dbl_syousaibu1.Rows.Count > 0)
                                    {
                                        //2
                                        // MessageBox.Show("単価原価方式（詳細行の標準単価に0円でない金額あり）");
                                        keisan_syousai_data(riritsu, uwagakis, kisuu, shisyagonyuu, row);
                                    }
                                    else
                                    {
                                        //1
                                        //MessageBox.Show("総原価方式と単価原価方式（詳細行の標準単価が0円もしくは、詳細データがない）の場合");
                                        keisan_meisai_data(riritsu, uwagakim, kisuu, shisyagonyuu, row);
                                    }
                                }
                                else
                                {
                                    //1
                                    //MessageBox.Show("総原価方式と単価原価方式（詳細行の標準単価が0円もしくは、詳細データがない）の場合");
                                    keisan_meisai_data(riritsu, uwagakim, kisuu, shisyagonyuu, row);
                                }
                            }
                            else
                            {
                                //1
                                //MessageBox.Show("総原価方式と単価原価方式（詳細行の標準単価が0円もしくは、詳細データがない）の場合");
                                keisan_meisai_data(riritsu, uwagakim, kisuu, shisyagonyuu, row);
                            }
                        }
                        else
                        {
                            //1
                            //MessageBox.Show("総原価方式と単価原価方式（詳細行の標準単価が0円もしくは、詳細データがない）の場合");
                            keisan_meisai_data(riritsu, uwagakim, kisuu, shisyagonyuu, row);
                        }
                    }
                }
            }
        }
        #endregion

        #region getJishabangou
        //得意先情報を取る
        private DataTable getJishabangou()
        {
            DataTable dtable = new DataTable();
            try
            {
                String id = Request.QueryString["id"];
                DataTable dt_db = JC01Login_Class.Get_DB(id);
                String db = dt_db.Rows[0]["db"].ToString();
                String mail = dt_db.Rows[0]["mail"].ToString();
                ConstantVal.DB_NAME = db;

                JC_ClientConnecction_Class jc = new JC_ClientConnecction_Class();
                jc.loginId = mail;
                con = jc.GetConnection();
                string strSql = "";
                strSql = "select ";
                strSql += "ifnull(mji.sjishabangou,'自社番号') AS sjishabangou";
                strSql += ",ifnull(mji.fkeisantani,'1') AS fkeisantani";
                strSql += ",ifnull(mji.ftansuushori,'1') AS ftansuushori";
                strSql += ",ifnull(mji.fgentankatanka,'1') AS fgentankatanka";
                strSql += ",ifnull(mji.fKAKAKUHYOU,'0') AS fKAKAKUHYOU";
                strSql += " from m_j_info mji ";
                strSql += "WHERE mji.cCO='01'";
                con.Open();
                MySqlCommand cmd = new MySqlCommand(strSql, con);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                dtable = new DataTable();
                da.Fill(dtable);
                con.Close();
                da.Dispose();
                return dtable;
            }
            catch
            {
            }
            return dtable;

        }
        #endregion

        #region keisan_syousai_data
        private void keisan_syousai_data(decimal riritsus, bool uwagakis, string kisuus, string shisyagonyuus, GridViewRow row)
        {
            try
            {
                #region syousai
                bool rowaru = false;
                Label lbl_RowNo = (row.FindControl("lblRowNo") as Label);
                Label lbl_fgenkataka = (row.FindControl("lblfgenkatanka") as Label);
                DataTable dbl_syousaibu = new DataTable();
                dbl_syousaibu = Syousai_All.Select("rowNo = '" + lbl_RowNo.Text + "'").CopyToDataTable();
                string rowno = lbl_RowNo.Text;
                if (dbl_syousaibu.Rows.Count > 0)
                {
                    dbl_syousaibu = dbl_syousaibu.Select("nTANKA<>'0.00' and nTANKA<>'0'").CopyToDataTable();
                    if (dbl_syousaibu.Rows.Count > 0)
                    {
                        decimal kiritankatotal = 0;
                        for (int r = 0; r < Syousai_All.Rows.Count; r++)
                        {
                            if (Syousai_All.Rows[r]["rowNo"].ToString() == rowno)
                            {
                                rowaru = true;
                                #region keisan syousai
                                decimal kirikingaku = 0;
                                decimal siirekingaku = 0;
                                decimal nsuuryou = 0;
                                decimal kiritanka = 0;
                                decimal siiretanka = 0;
                                bool update = false;
                                try
                                {
                                    if (Syousai_All.Rows[r]["nSURYO"] != null)
                                    {
                                        if (!Syousai_All.Rows[r]["nSURYO"].ToString().Equals(""))
                                        {
                                            if (Syousai_All.Rows[r]["nGENKATANKA"] != null)
                                            {
                                                if (!Syousai_All.Rows[r]["nGENKATANKA"].ToString().Equals(""))
                                                {
                                                    if (lbl_fgenkataka.Text != "1")
                                                    {
                                                        siiretanka = Convert.ToDecimal(Syousai_All.Rows[r]["nGENKATANKA"].ToString());
                                                    }
                                                    else
                                                    {
                                                        siiretanka = Convert.ToDecimal(Syousai_All.Rows[r]["nTANKAGOUKEI"].ToString());
                                                    }
                                                    if (!siiretanka.ToString().Equals("0") && !siiretanka.ToString().Equals("0.00"))
                                                    {
                                                        if (uwagakis == false)
                                                        {
                                                            if (Syousai_All.Rows[r]["nTANKA"] == null)
                                                            {
                                                                update = true;
                                                            }
                                                            else if (Syousai_All.Rows[r]["nTANKA"].ToString() == "")
                                                            {
                                                                update = true;
                                                            }
                                                            else
                                                            {
                                                                decimal hyoutanka = 0;
                                                                hyoutanka = Convert.ToDecimal(Syousai_All.Rows[r]["nTANKA"].ToString());

                                                                if (hyoutanka.ToString().Equals("0") || hyoutanka.ToString().Equals("0.00"))
                                                                {
                                                                    update = true;
                                                                }
                                                                else
                                                                {
                                                                    update = false;
                                                                }
                                                            }
                                                        }
                                                        else
                                                        {
                                                            update = true;
                                                        }
                                                        if (update == true)
                                                        {

                                                            //Syousai_All.Rows[r]["arariritsu"]= riritsu.ToString("0.0") + "%";　　　//粗利率
                                                            kiritanka = (siiretanka / (100 - riritsus)) * 100;　　　//標準単価・単価
                                                            bool fkisuukeisan = true;
                                                            #region 単数処理
                                                            if (kisuus == "0")
                                                            {
                                                                if (kiritanka >= 10)
                                                                {
                                                                    kiritanka = kiritanka / 10;
                                                                    if (shisyagonyuus == "0")
                                                                    {
                                                                        kiritanka = Math.Round(kiritanka, 0, MidpointRounding.AwayFromZero) * 10;
                                                                    }
                                                                    else if (shisyagonyuus == "1")
                                                                    {
                                                                        kiritanka = Math.Ceiling(kiritanka) * 10;
                                                                    }
                                                                    else if (shisyagonyuus == "2")
                                                                    {
                                                                        kiritanka = Math.Floor(kiritanka) * 10;
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    fkisuukeisan = false;
                                                                }
                                                            }
                                                            else if (kisuus == "1")
                                                            {
                                                                if (kiritanka >= 100)
                                                                {
                                                                    kiritanka = kiritanka / 100;
                                                                    if (shisyagonyuus == "0")
                                                                    {
                                                                        kiritanka = Math.Round(kiritanka, 0, MidpointRounding.AwayFromZero) * 100;
                                                                    }
                                                                    else if (shisyagonyuus == "1")
                                                                    {
                                                                        kiritanka = Math.Ceiling(kiritanka) * 100;
                                                                    }
                                                                    else if (shisyagonyuus == "2")
                                                                    {
                                                                        kiritanka = Math.Floor(kiritanka) * 100;
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    fkisuukeisan = false;
                                                                }
                                                            }
                                                            else if (kisuus == "2")
                                                            {
                                                                if (kiritanka >= 1000)
                                                                {
                                                                    kiritanka = kiritanka / 1000;

                                                                    if (shisyagonyuus == "0")
                                                                    {
                                                                        kiritanka = Math.Round(kiritanka, 0, MidpointRounding.AwayFromZero) * 1000;
                                                                    }
                                                                    else if (shisyagonyuus == "1")
                                                                    {
                                                                        kiritanka = Math.Ceiling(kiritanka) * 1000;
                                                                    }
                                                                    else if (shisyagonyuus == "2")
                                                                    {
                                                                        kiritanka = Math.Floor(kiritanka) * 1000;
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    fkisuukeisan = false;
                                                                }
                                                            }
                                                            else if (kisuus == "3")
                                                            {
                                                                if (kiritanka >= 10000)
                                                                {
                                                                    kiritanka = kiritanka / 10000;

                                                                    if (shisyagonyuus == "0")
                                                                    {
                                                                        kiritanka = Math.Round(kiritanka, 0, MidpointRounding.AwayFromZero) * 10000;
                                                                    }
                                                                    else if (shisyagonyuus == "1")
                                                                    {
                                                                        kiritanka = Math.Ceiling(kiritanka) * 10000;
                                                                    }
                                                                    else if (shisyagonyuus == "2")
                                                                    {
                                                                        kiritanka = Math.Floor(kiritanka) * 10000;
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    fkisuukeisan = false;
                                                                }
                                                            }
                                                            if (fkisuukeisan == false)
                                                            {
                                                                if (shisyagonyuus == "0")
                                                                {
                                                                    kiritanka = Math.Round(kiritanka, 0, MidpointRounding.AwayFromZero);
                                                                }
                                                                else if (shisyagonyuus == "1")
                                                                {
                                                                    kiritanka = Math.Ceiling(kiritanka);
                                                                }
                                                                else if (shisyagonyuus == "2")
                                                                {
                                                                    kiritanka = Math.Floor(kiritanka);
                                                                }
                                                            }

                                                            kiritanka = Convert.ToDecimal(kiritanka.ToString("n0"));

                                                            #endregion

                                                            #region data set
                                                            if (lbl_fgenkataka.Text != "1")
                                                            {
                                                                decimal nkakeritu = 100;
                                                                Syousai_All.Rows[r]["nTANKA"] = kiritanka;　　　//標準単価
                                                                if (Syousai_All.Rows[r]["nRITU"] != null)
                                                                {
                                                                    nkakeritu = Convert.ToDecimal(Syousai_All.Rows[r]["nRITU"].ToString()) / 100;
                                                                }
                                                                else
                                                                {
                                                                    nkakeritu = 1;
                                                                }
                                                                kiritanka = kiritanka * nkakeritu;
                                                                //Syousai_All.Rows[r]["nSIKIRITANKA"] = kiritanka;   //単価
                                                                nsuuryou = Convert.ToDecimal(Syousai_All.Rows[r]["nSURYO"].ToString());
                                                                kirikingaku = nsuuryou * kiritanka;
                                                                siirekingaku = nsuuryou * siiretanka;
                                                                DBL_JISHABANGOU = new DataTable();
                                                                DBL_JISHABANGOU = getJishabangou();
                                                                if (DBL_JISHABANGOU.Rows.Count > 0)
                                                                {
                                                                    #region 少数点

                                                                    if (DBL_JISHABANGOU.Rows[0]["ftansuushori"].ToString() == "1")
                                                                    {
                                                                        kirikingaku = Math.Floor(kirikingaku);
                                                                    }
                                                                    else if (DBL_JISHABANGOU.Rows[0]["ftansuushori"].ToString() == "2")
                                                                    {
                                                                        kirikingaku = Math.Round(kirikingaku, 0, MidpointRounding.AwayFromZero);
                                                                        //nsikirikingaku = Math.Round(nsikirikingaku);
                                                                    }
                                                                    else
                                                                    {
                                                                        kirikingaku = Math.Ceiling(kirikingaku);
                                                                    }
                                                                    #endregion


                                                                }
                                                                Syousai_All.Rows[r]["nTANKAGOUKEI"] = kirikingaku;　//合計金額
                                                                //Syousai_All.Rows[r]["nKINGAKU"] = kirikingaku;
                                                                kiritankatotal = kirikingaku + kiritankatotal;
                                                                //EL_MITUMORI[(int)ELTStruts.sikiritanka, row].Value = kiritankatotal;   //単価
                                                                //EL_MITUMORI[(int)ELTStruts.tanka, row].Value = kiritankatotal;　　　//標準単価
                                                                //ELT_KeiSan_ARARI(row);
                                                            }
                                                            else
                                                            {
                                                                decimal nkakeritu = 100;
                                                                Syousai_All.Rows[r]["nTANKAGOUKEI"] = kiritanka;　//合計金額
                                                                //Syousai_All.Rows[r]["nKINGAKU"] = kiritanka;

                                                                kiritankatotal = kiritanka + kiritankatotal;

                                                                nsuuryou = Convert.ToDecimal(Syousai_All.Rows[r]["nSURYO"].ToString());
                                                                kirikingaku = kiritanka / nsuuryou;
                                                                siirekingaku = siiretanka / nsuuryou;
                                                                if (Syousai_All.Rows[r]["nRITU"] != null)
                                                                {
                                                                    nkakeritu = Convert.ToDecimal(Syousai_All.Rows[r]["nRITU"].ToString()) / 100;
                                                                }
                                                                else
                                                                {
                                                                    nkakeritu = 1;
                                                                }

                                                                //Syousai_All.Rows[r]["nSIKIRITANKA"] = kirikingaku;   //単価
                                                                kirikingaku = kirikingaku / nkakeritu;
                                                                Syousai_All.Rows[r]["nTANKA"] = kirikingaku;　　　//標準単価 
                                                            }
                                                            #endregion
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (!Syousai_All.Rows[r]["nTANKAGOUKEI"].ToString().Equals(""))
                                                        {
                                                            kirikingaku = Convert.ToDecimal(Syousai_All.Rows[r]["nTANKAGOUKEI"].ToString());
                                                            kiritankatotal = kirikingaku + kiritankatotal;

                                                        }
                                                    }
                                                }

                                            }

                                        }

                                    }

                                }
                                catch (Exception ex)
                                {

                                }
                                #endregion
                            }
                            else
                            {
                                if (rowaru == true)
                                {
                                    break;
                                }
                            }
                        }
                        if (uwagakis == true)
                        {
                            if (lbl_fgenkataka.Text != "1")
                            {
                                //EL_MITUMORI[(int)ELTStruts.sikiritanka, row].Value = kiritankatotal;   //単価
                                (row.FindControl("txtnTANKA") as TextBox).Text = kiritankatotal.ToString();　　　//標準単価 
                                txtnTANKA_Changed(row);
                            }
                            else
                            {
                                if (!String.IsNullOrEmpty((row.FindControl("txtnSURYO") as TextBox).Text))
                                {
                                    decimal nsuuryou = 0;
                                    nsuuryou = Convert.ToDecimal((row.FindControl("txtnSURYO") as TextBox).Text);
                                    (row.FindControl("lblTankaGokei") as Label).Text = kiritankatotal.ToString();   //単価
                                    //EL_MITUMORI[(int)ELTStruts.sikiritanka, row].Value = kiritankatotal / nsuuryou; 
                                    (row.FindControl("txtnTANKA") as TextBox).Text = (kiritankatotal / nsuuryou).ToString();　　　//標準単価 
                                    txtnTANKA_Changed(row);
                                }
                            }
                            ELT_KeiSan_ARARI(row);
                        }
                    }
                    else
                    {
                        keisanmeisaidata(riritsus, uwagakis, kisuus, shisyagonyuus, row);
                    }
                }
                #endregion
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region keisanmeisaidata
        private void keisanmeisaidata(decimal riritsum, bool uwagakim, string kisuum, string shisyagonyuum, GridViewRow row)
        {
            #region keisan meisai
            //decimal kirikingaku = 0;
            //decimal siirekingaku = 0;
            //decimal nsuuryou = 0;
            decimal kiritanka = 0;
            decimal siiretanka = 0;
            bool update = true;
            try
            {
                if (!String.IsNullOrEmpty((row.FindControl("txtnSURYO") as TextBox).Text))
                {
                    if (!String.IsNullOrEmpty((row.FindControl("txtnGENKATANKA") as TextBox).Text))
                    {
                        siiretanka = Convert.ToDecimal((row.FindControl("txtnGENKATANKA") as TextBox).Text);

                        if (!siiretanka.ToString().Equals("0") && !siiretanka.ToString().Equals("0.00"))
                        {
                            #region uwagaki flag
                            if (uwagakim == false)
                            {
                                if (!String.IsNullOrEmpty((row.FindControl("txtnTANKA") as TextBox).Text))
                                {
                                    if (!(row.FindControl("txtnTANKA") as TextBox).Text.ToString().Equals("0") && !(row.FindControl("txtnTANKA") as TextBox).Text.ToString().Equals("0.00"))
                                    {
                                        update = false;
                                    }
                                    else
                                    {
                                        update = true;
                                    }
                                }
                                else
                                {
                                    update = true;
                                }
                            }
                            else
                            {
                                update = true;
                            }
                            #endregion

                            if (update == true)
                            {

                                (row.FindControl("lblnARARISu") as Label).Text = riritsum.ToString("0.0") + "%";   //粗利率
                                kiritanka = (siiretanka / (100 - riritsum)) * 100;   //標準単価・単価
                                bool fkisuukeisan = true;
                                #region 単数処理
                                if (kisuum == "0")
                                {
                                    if (kiritanka >= 10)
                                    {
                                        kiritanka = kiritanka / 10;
                                        if (shisyagonyuum == "0")
                                        {
                                            kiritanka = Math.Round(kiritanka, 0, MidpointRounding.AwayFromZero) * 10;
                                        }
                                        else if (shisyagonyuum == "1")
                                        {
                                            kiritanka = Math.Ceiling(kiritanka) * 10;
                                        }
                                        else if (shisyagonyuum == "2")
                                        {
                                            kiritanka = Math.Floor(kiritanka) * 10;
                                        }
                                    }
                                    else
                                    {
                                        fkisuukeisan = false;
                                    }
                                }
                                else if (kisuum == "1")
                                {
                                    if (kiritanka >= 100)
                                    {
                                        kiritanka = kiritanka / 100;
                                        if (shisyagonyuum == "0")
                                        {
                                            kiritanka = Math.Round(kiritanka, 0, MidpointRounding.AwayFromZero) * 100;
                                        }
                                        else if (shisyagonyuum == "1")
                                        {
                                            kiritanka = Math.Ceiling(kiritanka) * 100;
                                        }
                                        else if (shisyagonyuum == "2")
                                        {
                                            kiritanka = Math.Floor(kiritanka) * 100;
                                        }
                                    }
                                    else
                                    {
                                        fkisuukeisan = false;
                                    }
                                }
                                else if (kisuum == "2")
                                {
                                    if (kiritanka >= 1000)
                                    {
                                        kiritanka = kiritanka / 1000;

                                        if (shisyagonyuum == "0")
                                        {
                                            kiritanka = Math.Round(kiritanka, 0, MidpointRounding.AwayFromZero) * 1000;
                                        }
                                        else if (shisyagonyuum == "1")
                                        {
                                            kiritanka = Math.Ceiling(kiritanka) * 1000;
                                        }
                                        else if (shisyagonyuum == "2")
                                        {
                                            kiritanka = Math.Floor(kiritanka) * 1000;
                                        }
                                    }
                                    else
                                    {
                                        fkisuukeisan = false;
                                    }
                                }
                                else if (kisuum == "3")
                                {
                                    if (kiritanka >= 10000)
                                    {
                                        kiritanka = kiritanka / 10000;

                                        if (shisyagonyuum == "0")
                                        {
                                            kiritanka = Math.Round(kiritanka, 0, MidpointRounding.AwayFromZero) * 10000;
                                        }
                                        else if (shisyagonyuum == "1")
                                        {
                                            kiritanka = Math.Ceiling(kiritanka) * 10000;
                                        }
                                        else if (shisyagonyuum == "2")
                                        {
                                            kiritanka = Math.Floor(kiritanka) * 10000;
                                        }
                                    }
                                    else
                                    {
                                        fkisuukeisan = false;
                                    }
                                }
                                if (fkisuukeisan == false)
                                {
                                    if (shisyagonyuum == "0")
                                    {
                                        kiritanka = Math.Round(kiritanka, 0, MidpointRounding.AwayFromZero);
                                    }
                                    else if (shisyagonyuum == "1")
                                    {
                                        kiritanka = Math.Ceiling(kiritanka);
                                    }
                                    else if (shisyagonyuum == "2")
                                    {
                                        kiritanka = Math.Floor(kiritanka);
                                    }
                                }
                                kiritanka = Convert.ToDecimal(kiritanka.ToString("n0"));

                                #endregion

                                #region data set
                                //EL_MITUMORI[(int)ELTStruts.sikiritanka, row].Value = 0;
                                //EL_MITUMORI[(int)ELTStruts.sikiritanka, row].Value = kiritanka;   //単価
                                (row.FindControl("txtnTANKA") as TextBox).Text = kiritanka.ToString();   //標準単価
                                txtnTANKA_Changed(row);
                                //nsuuryou = Convert.ToDecimal(EL_MITUMORI[(int)ELTStruts.suuryou, row].Value);
                                //kirikingaku = nsuuryou * kiritanka;
                                //siirekingaku = nsuuryou * siiretanka;
                                //EL_MITUMORI[(int)ELTStruts.sikirikingaku, row].Value = kirikingaku;　//合計金額
                                //EL_MITUMORI[(int)ELTStruts.kingaku, row].Value = kirikingaku;
                                ELT_KeiSan_ARARI(row);
                                //if (EL_MITUMORI[(int)ELTStruts.siirekingaku, row].Value != null && EL_MITUMORI[(int)ELTStruts.sikirikingaku, row].Value != null)
                                //{
                                //    if (!EL_MITUMORI[(int)ELTStruts.siirekingaku, row].Value.ToString().Equals("") && !EL_MITUMORI[(int)ELTStruts.sikirikingaku, row].Value.ToString().Equals(""))
                                //    {
                                //        siirekingaku = Convert.ToDecimal(EL_MITUMORI[(int)ELTStruts.siirekingaku, row].Value);
                                //        kirikingaku = Convert.ToDecimal(EL_MITUMORI[(int)ELTStruts.sikirikingaku, row].Value);
                                //        EL_MITUMORI[(int)ELTStruts.arari, row].Value = kirikingaku - siirekingaku;　　//粗利
                                //    }
                                //}
                                #endregion
                            }
                        }
                        else
                        {
                            (row.FindControl("lblnARARISu") as Label).Text = "0.0%";
                        }

                    }
                    else
                    {
                        (row.FindControl("lblnARARI") as Label).Text = "";
                        (row.FindControl("lblnARARISu") as Label).Text = "";
                    }

                }
                else
                {
                    (row.FindControl("lblnARARI") as Label).Text = "";
                    (row.FindControl("lblnARARISu") as Label).Text = "";
                }
            }
            catch (Exception ex)
            {
                //log.Warn("FRM_MITUMORI---" + ex.Message);
            }
            #endregion
        }
        #endregion

        #region keisan_meisai_data
        private void keisan_meisai_data(decimal riritsum, bool uwagakim, string kisuum, string shisyagonyuum, GridViewRow row)
        {
            #region keisan meisai
            decimal kiritanka = 0;
            decimal siiretanka = 0;
            bool update = false;
            try
            {
                if (!String.IsNullOrEmpty((row.FindControl("txtnSURYO") as TextBox).Text))
                {
                    if (!String.IsNullOrEmpty((row.FindControl("txtnGENKATANKA") as TextBox).Text))
                    {
                        siiretanka = Convert.ToDecimal((row.FindControl("txtnGENKATANKA") as TextBox).Text);

                        if (!siiretanka.ToString().Equals("0") && !siiretanka.ToString().Equals("0.00"))
                        {
                            if (uwagakim == false)
                            {
                                if (String.IsNullOrEmpty((row.FindControl("txtnTANKA") as TextBox).Text))
                                {
                                    update = true;
                                }
                                else
                                {
                                    decimal hyoutanka = 0;
                                    hyoutanka = Convert.ToDecimal((row.FindControl("txtnTANKA") as TextBox).Text);

                                    if (hyoutanka.ToString().Equals("0") || hyoutanka.ToString().Equals("0.00"))
                                    {
                                        update = true;
                                    }
                                    else
                                    {
                                        update = false;
                                    }
                                }
                            }
                            else
                            {
                                update = true;
                            }

                            if (update == true)
                            {
                                (row.FindControl("lblnARARISu") as Label).Text = riritsum.ToString("0.0") + "%";   //粗利率
                                kiritanka = (siiretanka / (100 - riritsum)) * 100;   //標準単価・単価
                                bool fkisuukeisan = true;
                                #region 単数処理
                                if (kisuum == "0")
                                {
                                    if (kiritanka >= 10)
                                    {
                                        kiritanka = kiritanka / 10;
                                        if (shisyagonyuum == "0")
                                        {
                                            kiritanka = Math.Round(kiritanka, 0, MidpointRounding.AwayFromZero) * 10;
                                        }
                                        else if (shisyagonyuum == "1")
                                        {
                                            kiritanka = Math.Ceiling(kiritanka) * 10;
                                        }
                                        else if (shisyagonyuum == "2")
                                        {
                                            kiritanka = Math.Floor(kiritanka) * 10;
                                        }
                                    }
                                    else
                                    {
                                        fkisuukeisan = false;
                                    }
                                }
                                else if (kisuum == "1")
                                {
                                    if (kiritanka >= 100)
                                    {
                                        kiritanka = kiritanka / 100;
                                        if (shisyagonyuum == "0")
                                        {
                                            kiritanka = Math.Round(kiritanka, 0, MidpointRounding.AwayFromZero) * 100;
                                        }
                                        else if (shisyagonyuum == "1")
                                        {
                                            kiritanka = Math.Ceiling(kiritanka) * 100;
                                        }
                                        else if (shisyagonyuum == "2")
                                        {
                                            kiritanka = Math.Floor(kiritanka) * 100;
                                        }
                                    }
                                    else
                                    {
                                        fkisuukeisan = false;
                                    }
                                }
                                else if (kisuum == "2")
                                {
                                    if (kiritanka >= 1000)
                                    {
                                        kiritanka = kiritanka / 1000;

                                        if (shisyagonyuum == "0")
                                        {
                                            kiritanka = Math.Round(kiritanka, 0, MidpointRounding.AwayFromZero) * 1000;
                                        }
                                        else if (shisyagonyuum == "1")
                                        {
                                            kiritanka = Math.Ceiling(kiritanka) * 1000;
                                        }
                                        else if (shisyagonyuum == "2")
                                        {
                                            kiritanka = Math.Floor(kiritanka) * 1000;
                                        }
                                    }
                                    else
                                    {
                                        fkisuukeisan = false;
                                    }
                                }
                                else if (kisuum == "3")
                                {
                                    if (kiritanka >= 10000)
                                    {
                                        kiritanka = kiritanka / 10000;

                                        if (shisyagonyuum == "0")
                                        {
                                            kiritanka = Math.Round(kiritanka, 0, MidpointRounding.AwayFromZero) * 10000;
                                        }
                                        else if (shisyagonyuum == "1")
                                        {
                                            kiritanka = Math.Ceiling(kiritanka) * 10000;
                                        }
                                        else if (shisyagonyuum == "2")
                                        {
                                            kiritanka = Math.Floor(kiritanka) * 10000;
                                        }
                                    }
                                    else
                                    {
                                        fkisuukeisan = false;
                                    }
                                }
                                if (fkisuukeisan == false)
                                {
                                    if (shisyagonyuum == "0")
                                    {
                                        kiritanka = Math.Round(kiritanka, 0, MidpointRounding.AwayFromZero);
                                    }
                                    else if (shisyagonyuum == "1")
                                    {
                                        kiritanka = Math.Ceiling(kiritanka);
                                    }
                                    else if (shisyagonyuum == "2")
                                    {
                                        kiritanka = Math.Floor(kiritanka);
                                    }
                                }

                                kiritanka = Convert.ToDecimal(kiritanka.ToString("n0"));

                                #endregion

                                #region data set
                                //EL_MITUMORI[(int)ELTStruts.sikiritanka, row].Value = 0;
                                //EL_MITUMORI[(int)ELTStruts.sikiritanka, row].Value = kiritanka;   //単価
                                (row.FindControl("txtnTANKA") as TextBox).Text = kiritanka.ToString();   //標準単価          
                                txtnTANKA_Changed(row);
                                ELT_KeiSan_ARARI(row);
                                #endregion
                            }
                        }
                        else
                        {
                            //EL_MITUMORI[(int)ELTStruts.arariritsu, row].Value = "0.0%";
                        }
                    }

                    else
                    {
                        (row.FindControl("lblnARARI") as Label).Text = "";
                        (row.FindControl("lblnARARISu") as Label).Text = "";
                    }
                }
                else
                {
                    (row.FindControl("lblnARARI") as Label).Text = "";
                    (row.FindControl("lblnARARISu") as Label).Text = "";
                }
            }
            catch (Exception ex)
            {
                //log.Warn("FRM_MITUMORI---" + ex.Message);
            }
            #endregion
        }
        #endregion

        #region ELT_KeiSan_ARARI
        private void ELT_KeiSan_ARARI(GridViewRow row)
        {
            ELT_KeiSan_KIRIKINGAKU(row);
            ELT_KeiSan_SIIREKINGAKU(row);
            decimal kirikingaku = 0;           //仕切金額
            decimal siirekingaku = 0;             //仕入金額
            Label lbl_TankaGokei = (row.FindControl("lblTankaGokei") as Label);
            Label lbl_GenkaGokei = (row.FindControl("lblGenkaGokei") as Label);
            try
            {
                if (!String.IsNullOrEmpty(lbl_TankaGokei.Text) && !String.IsNullOrEmpty(lbl_GenkaGokei.Text))
                {
                    siirekingaku = Convert.ToDecimal(lbl_GenkaGokei.Text);
                    kirikingaku = Convert.ToDecimal(lbl_TankaGokei.Text);
                    (row.FindControl("lblnARARI") as Label).Text = (kirikingaku - siirekingaku).ToString();
                    if ((!(row.FindControl("lblnARARI") as Label).Text.ToString().Equals("0") && !(row.FindControl("lblnARARI") as Label).Text.ToString().Equals("0.00"))
                        && (!kirikingaku.ToString().Equals("0") && !kirikingaku.ToString().Equals("0.00")))
                    {
                        decimal aa = ((kirikingaku - siirekingaku) / kirikingaku) * 100;
                        (row.FindControl("lblnARARISu") as Label).Text = aa.ToString("0.0") + "%";
                    }
                    else
                    {
                        (row.FindControl("lblnARARISu") as Label).Text = "0.0%";
                    }
                }
                else
                {
                    (row.FindControl("lblnARARI") as Label).Text = "";
                    (row.FindControl("lblnARARISu") as Label).Text = "";
                }
            }
            catch (Exception ex)
            {
                //log.Warn("FRM_MITUMORI---" + ex.Message);
            }
        }
        #endregion

        #region SyousaiCheck()

        private int SyousaiCheck(string rownumber)
        {
            int zero = 0;

            if (Syousai_All.Rows.Count > 0)
            {
                //得意先掛率の関係 start
                DataTable Syousai_Goukei = Syousai_All.Select("rowNo = '" + rownumber + "'").CopyToDataTable();

                if (Syousai_Goukei.Rows.Count > 0)  //詳細の値は０の場合、単価とかを変更しない 
                {
                    Syousai_Goukei = Syousai_Goukei.Select("nTANKAGOUKEI<>'0' AND nTANKAGOUKEI<>'0.00'").CopyToDataTable();
                    zero = Syousai_Goukei.Rows.Count;
                }
            }
            return zero;
        }

        #endregion

        #region ELT_KeiSan_KIRIKINGAKU
        private void ELT_KeiSan_KIRIKINGAKU(GridViewRow row)
        {
            ELT_KeiSan_KIRITANKA(row);
            decimal nsuuryou = 0;           //数量
            decimal ntanka = 0;             //単価
            decimal nsikirikingaku = 0;
            string calhouhou = "1";
            try
            {
                if (SyousaiCheck((row.FindControl("lblRowNo") as Label).Text) == 0)
                {
                    calhouhou = "1";
                }
                else if (!String.IsNullOrEmpty((row.FindControl("lblfgenkatanka") as Label).Text))
                {
                    if ((row.FindControl("lblfgenkatanka") as Label).Text != "1")
                    {
                        calhouhou = "1";
                    }
                    else
                    {
                        calhouhou = "2";
                    }
                }
                else
                {
                    calhouhou = "1";
                }
                if (calhouhou == "1")
                {
                    if (!String.IsNullOrEmpty((row.FindControl("txtnTANKA") as TextBox).Text) && !String.IsNullOrEmpty((row.FindControl("txtnSURYO") as TextBox).Text))
                    {
                        ntanka = (Convert.ToDecimal((row.FindControl("txtnTANKA") as TextBox).Text) / 100) * Convert.ToDecimal((row.FindControl("txtnRITU") as TextBox).Text);
                        nsuuryou = Convert.ToDecimal((row.FindControl("txtnSURYO") as TextBox).Text);
                        nsikirikingaku = ntanka * nsuuryou;
                        if (DBL_JISHABANGOU.Rows.Count > 0)
                        {
                            #region 少数点

                            if (DBL_JISHABANGOU.Rows[0]["ftansuushori"].ToString() == "1")
                            {
                                nsikirikingaku = Math.Floor(nsikirikingaku);
                            }
                            else if (DBL_JISHABANGOU.Rows[0]["ftansuushori"].ToString() == "2")
                            {
                                nsikirikingaku = Math.Round(nsikirikingaku, 0, MidpointRounding.AwayFromZero);
                                //nsikirikingaku = Math.Round(nsikirikingaku);
                            }
                            else
                            {
                                nsikirikingaku = Math.Ceiling(nsikirikingaku);
                            }
                            #endregion
                        }
                        (row.FindControl("lblTankaGokei") as Label).Text = nsikirikingaku.ToString();
                    }
                }
                else
                {
                    if (String.IsNullOrEmpty((row.FindControl("lblTankaGokei") as Label).Text) && !String.IsNullOrEmpty((row.FindControl("txtnSURYO") as TextBox).Text)
                        && !String.IsNullOrEmpty((row.FindControl("lblGenkaGokei") as Label).Text))
                    {
                        (row.FindControl("lblTankaGokei") as Label).Text = "0";
                        //EL_MITUMORI[(int)ELTStruts.sikiritanka, row].Value = 0;
                        (row.FindControl("txtnTANKA") as TextBox).Text = "0";
                        txtnTANKA_Changed(row);
                    }
                    else if (!String.IsNullOrEmpty((row.FindControl("lblTankaGokei") as Label).Text) && !String.IsNullOrEmpty((row.FindControl("txtnSURYO") as TextBox).Text))
                    {
                        if ((row.FindControl("txtnSURYO") as TextBox).Text != "0" && (row.FindControl("txtnSURYO") as TextBox).Text != "0.0"
                            && (row.FindControl("txtnSURYO") as TextBox).Text != "0.00")
                        {
                            ntanka = Convert.ToDecimal((row.FindControl("lblTankaGokei") as Label).Text);
                            nsuuryou = Convert.ToDecimal((row.FindControl("txtnSURYO") as TextBox).Text);
                            try
                            {
                                nsikirikingaku = ntanka / nsuuryou;
                            }
                            catch
                            {
                                nsikirikingaku = 0;
                            }
                            if (DBL_JISHABANGOU.Rows.Count > 0)
                            {
                                #region 少数点

                                if (DBL_JISHABANGOU.Rows[0]["ftansuushori"].ToString() == "1")
                                {
                                    nsikirikingaku = Math.Floor(nsikirikingaku);
                                }
                                else if (DBL_JISHABANGOU.Rows[0]["ftansuushori"].ToString() == "2")
                                {
                                    nsikirikingaku = Math.Round(nsikirikingaku, 0, MidpointRounding.AwayFromZero);
                                    //nsikirikingaku = Math.Round(nsikirikingaku);
                                }
                                else
                                {
                                    nsikirikingaku = Math.Ceiling(nsikirikingaku);
                                }
                                #endregion
                            }

                        }
                        fAdd = true;
                        //EL_MITUMORI[(int)ELTStruts.sikiritanka, row].Value = nsikirikingaku;
                        fAdd = false;
                    }

                }
            }
            catch (Exception ex)
            {
                //log.Warn("FRM_MITUMORI---" + ex.Message);
            }
        }
        #endregion

        #region ELT_KeiSan_SIIREKINGAKU
        private void ELT_KeiSan_SIIREKINGAKU(GridViewRow row)
        {
            decimal nsuuryou = 0;           //数量
            decimal ntanka = 0;             //単価
            decimal nsiirekingaku = 0;
            string calhouhou = "1";
            try
            {
                if ((row.FindControl("btnSyohinShosai") as Button).Text != "詳")
                {
                    if (!String.IsNullOrEmpty((row.FindControl("lblfgenkatanka") as Label).Text))
                    {
                        if ((row.FindControl("lblfgenkatanka") as Label).Text != "1")
                        {
                            calhouhou = "1";
                        }
                        else
                        {
                            calhouhou = "2";
                        }
                    }
                    else
                    {
                        calhouhou = "1";
                    }
                }
                else
                {
                    calhouhou = "1";
                }

                if (calhouhou == "1")
                {
                    if (!String.IsNullOrEmpty((row.FindControl("txtnGENKATANKA") as TextBox).Text) && !String.IsNullOrEmpty((row.FindControl("txtnSURYO") as TextBox).Text))
                    {
                        ntanka = Convert.ToDecimal((row.FindControl("txtnGENKATANKA") as TextBox).Text);
                        nsuuryou = Convert.ToDecimal((row.FindControl("txtnSURYO") as TextBox).Text);
                        nsiirekingaku = ntanka * nsuuryou;
                        if (DBL_JISHABANGOU.Rows.Count > 0)
                        {
                            #region 少数点

                            if (DBL_JISHABANGOU.Rows[0]["ftansuushori"].ToString() == "1")
                            {
                                nsiirekingaku = Math.Floor(nsiirekingaku);
                            }
                            else if (DBL_JISHABANGOU.Rows[0]["ftansuushori"].ToString() == "2")
                            {
                                nsiirekingaku = Math.Round(nsiirekingaku, 0, MidpointRounding.AwayFromZero);
                                //nsiirekingaku = Math.Round(nsiirekingaku);
                            }
                            else
                            {
                                nsiirekingaku = Math.Ceiling(nsiirekingaku);
                            }
                            #endregion
                        }
                        (row.FindControl("lblGenkaGokei") as Label).Text = nsiirekingaku.ToString();
                    }
                }
                else
                {
                    if (!String.IsNullOrEmpty((row.FindControl("lblGenkaGokei") as Label).Text) && !String.IsNullOrEmpty((row.FindControl("txtnSURYO") as TextBox).Text))
                    {
                        if ((row.FindControl("txtnSURYO") as TextBox).Text != "0" &&
                            (row.FindControl("txtnSURYO") as TextBox).Text != "0.0" &&
                            (row.FindControl("txtnSURYO") as TextBox).Text != "0.00")
                        {
                            ntanka = Convert.ToDecimal((row.FindControl("lblGenkaGokei") as Label).Text);
                            nsuuryou = Convert.ToDecimal((row.FindControl("txtnSURYO") as TextBox).Text);
                            try
                            {
                                nsiirekingaku = ntanka / nsuuryou;
                            }
                            catch
                            {
                                nsiirekingaku = 0;
                            }
                            if (DBL_JISHABANGOU.Rows.Count > 0)
                            {
                                #region 少数点

                                if (DBL_JISHABANGOU.Rows[0]["ftansuushori"].ToString() == "1")
                                {
                                    //nsiirekingaku = Math.Floor(nsiirekingaku);
                                    double dnsiirekingaku = Floor(double.Parse(nsiirekingaku.ToString()), 2);
                                    nsiirekingaku = decimal.Parse(dnsiirekingaku.ToString());
                                }
                                else if (DBL_JISHABANGOU.Rows[0]["ftansuushori"].ToString() == "2")
                                {
                                    nsiirekingaku = Math.Round(nsiirekingaku, 2, MidpointRounding.AwayFromZero);
                                    //nsiirekingaku = Math.Round(nsiirekingaku);
                                }
                                else
                                {
                                    double dnsiirekingaku = Ceil(double.Parse(nsiirekingaku.ToString()), 2);
                                    nsiirekingaku = decimal.Parse(dnsiirekingaku.ToString());
                                }
                                #endregion

                            }
                        }
                        (row.FindControl("txtnGENKATANKA") as TextBox).Text = nsiirekingaku.ToString();
                        txtnGENKATANKA_Changed(row);
                    }
                    else
                    {
                        if (String.IsNullOrEmpty((row.FindControl("lblGenkaGokei") as Label).Text))
                        {
                            (row.FindControl("lblGenkaGokei") as Label).Text = "0";
                        }
                        (row.FindControl("txtnGENKATANKA") as TextBox).Text = "0";
                        txtnGENKATANKA_Changed(row);
                    }
                }
            }
            catch (Exception ex)
            {
                // log.Warn("FRM_MITUMORI---" + ex.Message);
            }
        }
        #endregion

        #region ELT_KeiSan_KIRITANKA
        private void ELT_KeiSan_KIRITANKA(GridViewRow row)
        {
            decimal ntanka = 0;             //単価
            decimal nkakeritu = 100;        //掛け率
            try
            {
                if (!String.IsNullOrEmpty((row.FindControl("txtnTANKA") as TextBox).Text))
                {
                    ntanka = Convert.ToDecimal((row.FindControl("txtnTANKA") as TextBox).Text);
                    //掛け率の計算
                    if (!String.IsNullOrEmpty((row.FindControl("txtnRITU") as TextBox).Text))
                    {
                        nkakeritu = Convert.ToDecimal((row.FindControl("txtnRITU") as TextBox).Text) / 100;
                    }
                    else
                    {
                        nkakeritu = 1;
                    }
                    //EL_MITUMORI[(int)ELTStruts.sikiritanka, row].Value = ntanka * nkakeritu;

                }
            }
            catch (Exception ex)
            {
                //log.Warn("FRM_MITUMORI---" + ex.Message);
            }
        }
        #endregion

        #region Floor
        public double Floor(double aValue, int aDigits)
        {
            double m = Math.Pow(10, aDigits);
            aValue *= m;
            aValue = Math.Floor(aValue);
            return aValue / m;
        }
        #endregion

        #region Ceil
        public double Ceil(double aValue, int aDigits)
        {
            double m = Math.Pow(10, aDigits);
            aValue *= m;
            aValue = Math.Ceiling(aValue);
            return aValue / m;
        }
        #endregion

        #region 表示項目設定「保存ボタン」
        protected void btnHyoujiSettingSave_Click(object sender, EventArgs e)
        {
            if (HF_HyojiBtn.Value == "uriage")
            {
                get_uriage_data();
            }
            ifpnlHyoujiSetPopUp.Src = "";
            mpeHyoujiSetPopUp.Hide();
            updHyoujiSet.Update();
            updHeader.Update();
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

        #region BT_LBSaveCross_Click
        protected void BT_LBSaveCross_Click(object sender, EventArgs e)
        {
            divLabelSave.Style["display"] = "none";
        }
        #endregion

        #region btnNewMitumori_Click
        protected void btnNewMitumori_Click(object sender, EventArgs e)
        {
            HF_fBtn.Value = "btnNewMitumori";
            if (HF_isChange.Value == "1")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowAnkenChangeMessage",
                    "ShowKoumokuChangesConfirmMessage('項目が変更されています。保存しますか？','" + btnYes.ClientID + "','" + btnNo.ClientID + "','" + btnCancel.ClientID + "');", true);
            }
            else
            {
                btnNo_Click(sender, e);
            }
        }
        #endregion

        #region btnTaMitumoriCopy_Click
        protected void btnTaMitumoriCopy_Click(object sender, EventArgs e)
        {
            HF_fBtn.Value = "btnTaMitumoriCopy";
            if (HF_isChange.Value == "1")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowAnkenChangeMessage",
                    "ShowKoumokuChangesConfirmMessage('項目が変更されています。保存しますか？','" + btnYes.ClientID + "','" + btnNo.ClientID + "','" + btnCancel.ClientID + "');", true);
            }
            else
            {
                btnNo_Click(sender, e);
            }
        }
        #endregion

        #region BT_HyoshiImgaeDelete_Click  //表紙画像削除
        protected void BT_HyoshiImgaeDelete_Click(object sender, EventArgs e)
        {
            HyoshiuploadedImage.Src = "";
            HyoshiuploadedImage.Attributes.Add("class", "DisplayNone");
            BT_HyoshiImgaeDelete.CssClass = "DisplayNone";
            HyoshidragZone.Attributes.Remove("class");
            HF_HyoshiFileName.Value = "";
            updHeader.Update();
        }
        #endregion

        #region BT_Mitumorisho1ImgaeDelete_Click　//見積書1枚目画像削除
        protected void BT_Mitumorisho1ImgaeDelete_Click(object sender, EventArgs e)
        {
            Mitumorisho1uploadedImage.Src = "";
            Mitumorisho1uploadedImage.Attributes.Add("class", "DisplayNone");
            BT_Mitumorisho1ImgaeDelete.CssClass = "DisplayNone";
            Mitumorisho1dragZone.Attributes.Remove("class");
            HF_Gazo1FileName.Value = "";
            updHeader.Update();
        }
        #endregion

        #region BT_Mitumorisho2ImgaeDelete_Click　//見積書2枚目画像削除
        protected void BT_Mitumorisho2ImgaeDelete_Click(object sender, EventArgs e)
        {
            Mitumorisho2uploadedImage.Src = "";
            Mitumorisho2uploadedImage.Attributes.Add("class", "DisplayNone");
            BT_Mitumorisho2ImgaeDelete.CssClass = "DisplayNone";
            Mitumorisho2dragZone.Attributes.Remove("class");
            HF_Gazo2FileName.Value = "";
            updHeader.Update();
        }
        #endregion

        #region 見積削除
        #region btnMitumoriDelete_Click
        protected void btnMitumoriDelete_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "DeleteConfirmMessage",
                    "DeleteConfirmBox('見積に紐付けている売上を削除しても<br/>よろしいでしょうか？','" + btnDeleteMitumori.ClientID + "');", true);
        }
        #endregion

        protected void btnDeleteMitumori_Click(object sender, EventArgs e)
        {
            String id = Request.QueryString["id"];
            DataTable dt_db = JC01Login_Class.Get_DB(id);
            String db = dt_db.Rows[0]["db"].ToString();
            String mail = dt_db.Rows[0]["mail"].ToString();
            ConstantVal.DB_NAME = db;

            JC_ClientConnecction_Class jc = new JC_ClientConnecction_Class();
            jc.loginId = mail;
            con = jc.GetConnection();
            String cMitumori = lblcMitumori.Text;
            sql_Delete += "DELETE FROM r_bu_mitsu WHERE cMITUMORI='" + cMitumori + "';";
            sql_Delete += "Delete From r_mitumori where  cMITUMORI='" + cMitumori + "';";
            sql_Delete += "Delete From r_mitumori_m where  cMITUMORI='" + cMitumori + "';";
            sql_Delete += "Delete From r_mitumori_m2 where  cMITUMORI='" + cMitumori + "';";
            sql_Delete += "delete from h_mitumori where 1=1 and cMITUMORI='" + cMitumori + "';";
            sql_Delete += "Delete From m_mitsu_file where  cMITUMORI='" + cMitumori + "';";
            DeleteFileByMitumori(cMitumori);
            DeleteSekou(cMitumori, 2);//見積
            sql_Delete += "delete from r_shijisyo_mitsu where 1=1 and cMITUMORI='" + cMitumori + "';";
            DeleteShijishoByMitumori(cMitumori);
            sql_Delete += "delete from sekisankoumoku_mitsu where cMITUMORI='" + cMitumori + "' ;";


            sql_Delete += "delete from M_SEKISANKOUMOKU_M where cMITUMORI='" + cMitumori + "';";
            sql_Delete += "delete from M_CYOUKOKUSEKISAN where cMITUMORI='" + cMitumori + "';";
            sql_Delete += "delete from M_INKUJIXEXTUTO where cMITUMORI='" + cMitumori + "';";
            sql_Delete += "delete from m_kyoutsuu where cMITUMORI='" + cMitumori + "';";
            sql_Delete += "delete from r_router where cMITUMORI='" + cMitumori + "';";
            sql_Delete += "delete from r_pattern where cMITUMORI='" + cMitumori + "';";
            sql_Delete += "Delete From r_uri_mitsu where cMITUMORI='" + cMitumori + "';";
            DeleteUriageByMitumori(cMitumori);
            MySqlCommand cmdDelete = new MySqlCommand();
            MySqlTransaction tr = null;
            con.Open();
            try
            {
                tr = con.BeginTransaction();
                cmdDelete.Transaction = tr;
                cmdDelete.CommandTimeout = 0;
                cmdDelete = new MySqlCommand(sql_Delete, con);
                cmdDelete.ExecuteNonQuery();
                tr.Commit();
                if (!String.IsNullOrEmpty(Request.QueryString["id"]))
                {
                    id = Request.QueryString["id"];
                    Response.Redirect("JC31MitsumoriList.aspx?id=" + id, false);
                }
                else
                {
                    Response.Redirect("JC01Login.aspx");
                }
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
        }

        #region DeleteFileByMitumori
        public void DeleteFileByMitumori(String cMitumori)
        {
            String sql = "SELECT cFILE FROM m_mitsu_file Where cMITUMORI='" + cMitumori + "';";
            con.Open();
            MySqlCommand cmd = new MySqlCommand(sql, con);
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();
            da.Dispose();
            foreach (DataRow dr in dt.Rows)
            {
                sql_Delete += "DELETE FROM m_file WHERE cFILE='" + dr[0].ToString() + "';";
            }
        }
        #endregion

        #region DeleteFileByShijisyo
        public void DeleteFileByShijisyo(String cShijisyo)
        {
            String sql = "SELECT cFILE FROM m_shijisyo_file Where cSHIJISYO='" + cShijisyo + "';";
            con.Open();
            MySqlCommand cmd = new MySqlCommand(sql, con);
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();
            da.Dispose();
            foreach (DataRow dr in dt.Rows)
            {
                sql_Delete += "DELETE FROM m_file WHERE cFILE='" + dr[0].ToString() + "';";
            }
        }
        #endregion

        #region DeleteShijishoByMitumori
        public void DeleteShijishoByMitumori(String cMitumori)
        {
            String sql = "SELECT cSHIJISYO FROM r_shijisyo_mitsu Where cMitumori='" + cMitumori + "';";
            con.Open();
            MySqlCommand cmd = new MySqlCommand(sql, con);
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();
            da.Dispose();
            foreach (DataRow dr in dt.Rows)
            {
                String cShijisyo = dr[0].ToString();
                sql_Delete += "Delete From r_bu_shijisyo Where '1'='1'  And cBUKKEN='" + lnkcBukken.Text + "' And cSHIJISYO='" + cShijisyo + "';";
                sql_Delete += "Delete From m_shijisyo_file where cSHIJISYO='" + cShijisyo + "';";
                DeleteFileByShijisyo(cShijisyo);
                DeleteSekou(cShijisyo, 3);//指示書
                sql_Delete += "Delete From r_shijisyo where cSHIJISYO='" + cShijisyo + "';";
                sql_Delete += "Delete From h_shijisyo where cSHIJISYO='" + cShijisyo + "';";
                sql_Delete += "Delete From r_shiji_sagyo where cSHIJISYO='" + cShijisyo + "';";
                sql_Delete += "Delete From r_shiji_sagyonaiyo where cSHIJISYO='" + cShijisyo + "';";
                sql_Delete += "Delete From r_shiji_sagyotitle where cSHIJISYO='" + cShijisyo + "';";
                sql_Delete += "Delete From r_sjs_bunrui where cSHIJISYO = '" + cShijisyo + "';";
                sql_Delete += "Delete From r_shijisyo_bunrui where cSHIJISYO='" + cShijisyo + "';";
            }
        }
        #endregion

        #region DeleteUriageByMitumori
        public void DeleteUriageByMitumori(String cMitumori)
        {
            String sql = "SELECT cURIAGE FROM r_uri_mitsu Where cMITUMORI='" + cMitumori + "';";
            con.Open();
            MySqlCommand cmd = new MySqlCommand(sql, con);
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();
            da.Dispose();
            foreach (DataRow dr in dt.Rows)
            {
                String cUriage = dr[0].ToString();
                sql_Delete += "Delete From r_uriage where cURIAGE='" + cUriage + "';";
                sql_Delete += "Delete From r_uriage_m where cURIAGE='" + cUriage + "'";
            }
        }
        #endregion

        #region DeleteSekou
        public void DeleteSekou(String code, int flag)
        {
            String sql = "";
            if (flag == 1)  //物件
            {
                sql = "SELECT rs.cSEKOU as cSEKOU FROM r_sekouyotei rs where rs.cBUKKEN='" + code + "';";
            }
            else if (flag == 2)  //見積
            {
                sql = "SELECT rs.cSEKOU as cSEKOU FROM r_sekouyotei rs where rs.cMITUMORI='" + code + "';";
            }
            else if (flag == 3)  //指示書
            {
                sql = "SELECT rs.cSEKOU as cSEKOU FROM r_sekouyotei rs where rs.cSHIJISYO='" + code + "';";
            }
            con.Open();
            MySqlCommand cmd = new MySqlCommand(sql, con);
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();
            da.Dispose();
            foreach (DataRow dr in dt.Rows)
            {
                String csekou = dr[0].ToString();
                sql_Delete += "DELETE FROM r_sekouyotei WHERE cSEKOU='" + csekou + "';";
                sql_Delete += "DELETE FROM r_sankasya WHERE cSEKOU='" + csekou + "';";
                sql_Delete += "DELETE FROM r_syaryo WHERE cSEKOU='" + csekou + "';";
            }
        }
        #endregion

        #endregion

        #region lnkCodeUriage_Click
        protected void lnkCodeUriage_Click(object sender, EventArgs e)
        {
            LinkButton LkcUriage = sender as LinkButton;
            Session["cUriage"] = LkcUriage.Text;
            JC99NavBar.insatsusettei = false;
            if (!String.IsNullOrEmpty(Request.QueryString["id"]))
            {
                String id = Request.QueryString["id"];
                Response.Redirect("JC27UriageTouroku.aspx?id=" + id);
            }
            else
            {
                Response.Redirect("JC01Login.aspx");
            }
        }
        #endregion

        #region lnkcMitumori_Click
        protected void lnkcMitumori_Click(object sender, EventArgs e)
        {
            LinkButton lk_Mitumori = sender as LinkButton;
            Session["cMitumori"] = lk_Mitumori.Text;
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

        #region btnDisplayUriageItemSetting_Click
        protected void btnDisplayUriageItemSetting_Click(object sender, EventArgs e)
        {
            SessionUtility.SetSession("HOME", "Master");
            Session["HyoujiID"] = "uriage";
            ifpnlHyoujiSetPopUp.Attributes.Add("class", "HyoujiSettingIframe uriageiframeStyle");
            ifpnlHyoujiSetPopUp.Style["width"] = "950px";
            ifpnlHyoujiSetPopUp.Style["height"] = "650px";
            String id = Request.QueryString["id"];
            ifpnlHyoujiSetPopUp.Src = "JC08HyoujiSetting.aspx?id=" + id;
            mpeHyoujiSetPopUp.Show();
            updHyoujiSet.Update();
            updHeader.Update();
            ScriptManager.RegisterStartupScript(this, GetType(), "CloseModal", "closeLoadingModal();", true);
        }
        #endregion

        #region lnkbtnUriageEdit_Click
        protected void lnkbtnUriageEdit_Click(object sender, EventArgs e)
        {
            GridViewRow gvRow = (sender as LinkButton).NamingContainer as GridViewRow;
            string uriageCode = (gvRow.FindControl("lnkCodeUriage") as LinkButton).Text;
            Session["cUriage"] = uriageCode;
            JC99NavBar.insatsusettei = false;
            if (!String.IsNullOrEmpty(Request.QueryString["id"]))
            {
                String id = Request.QueryString["id"];
                Response.Redirect("JC27UriageTouroku.aspx?id=" + id);
            }
            else
            {
                Response.Redirect("JC01Login.aspx");
            }
        }
        #endregion

        #region lnkbtnUriageDelete_Click
        protected void lnkbtnUriageDelete_Click(object sender, EventArgs e)
        {
            GridViewRow gvRow = (sender as LinkButton).NamingContainer as GridViewRow;
            string uriageCode = (gvRow.FindControl("lnkCodeUriage") as LinkButton).Text;
            HF_cUriage.Value = uriageCode;
            updHeader.Update();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "DeleteConfirmMessage",
                    "DeleteConfirmBox('削除してもよろしいでしょうか？','" + btnDeleteUriage.ClientID + "');", true);
        }
        #endregion

        #region lnkcBukken_Click
        protected void lnkcBukken_Click(object sender, EventArgs e)
        {
            HF_fBtn.Value = "lnkcBukken";
            if (HF_isChange.Value == "1")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowAnkenChangeMessage",
                        "ShowKoumokuChangesConfirmMessage('項目が変更されています。保存しますか？','" + btnYes.ClientID + "','" + btnNo.ClientID + "','" + btnCancel.ClientID + "');", true);
            }
            else
            {
                btnNo_Click(sender, e);
            }

        }
        #endregion

        #region txtsMitumori_TextChanged
        protected void txtsMitumori_TextChanged(object sender, EventArgs e)
        {
            HF_isChange.Value = "1";
            txtsMitumori.BorderColor = System.Drawing.Color.Empty;
            txtsMitumori.BorderStyle = BorderStyle.NotSet;
            txtsMitumori.BorderWidth = 1;
            updHeader.Update();
        }
        #endregion

        #region btnYes_Click
        protected void btnYes_Click(object sender, EventArgs e)
        {
            btnMitumoriSave_Click(sender, e);
            if (saveSuccess == true)
            {
                if (HF_fBtn.Value == "lnkcBukken")
                {
                    Session["cBukken"] = lnkcBukken.Text;
                    if (!String.IsNullOrEmpty(Request.QueryString["id"]))
                    {
                        String id = Request.QueryString["id"];
                        Response.Redirect("JC09BukkenSyousai.aspx?id=" + id);
                    }
                    else
                    {
                        Response.Redirect("JC01Login.aspx");
                    }
                }
                else if (HF_fBtn.Value == "btnNewMitumori")
                {
                    Session["cBukken"] = lnkcBukken.Text;
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
                else if (HF_fBtn.Value == "btnTaMitumoriCopy")
                {
                    SessionUtility.SetSession("HOME", "Master");
                    String id = Request.QueryString["id"];
                    ifShinkiPopup.Src = "JC12MitsumoriKensaku.aspx?id=" + id;
                    mpeShinkiPopup.Show();
                    updShinkiPopup.Update();
                }
                else if (HF_fBtn.Value == "btnCreateUriage")
                {
                    Session["cUriage"] = null;


                    #region deleted by YG
                    //Session["cTokuisaki"] = lblcTOKUISAKI.Text;
                                                              //Session["sTokuisaki"] = lblsTOKUISAKI.Text;
                                                              //Session["TOUKUISAKITANTOU"] = lblsTOKUISAKI_TAN.Text;
                                                              //Session["TokuisakiTanJun"] = lblsTOKUISAKI_TAN_JUN.Text;
                                                              //Session["cSeikyusaki"] = lblcSEIKYUSAKI.Text;
                                                              //Session["sSeikyusaki"] = lblsSEIKYUSAKI.Text;
                                                              //Session["sSEIKYUSAKI_TAN"] = lblsSEIKYUSAKI_TAN.Text;
                                                              //Session["sSEIKYUSAKI_TAN_JUN"] = lblsSEIKYUSAKI_TAN_JUN.Text;
                                                              //Session["sMITUMORI"] = txtsMitumori.Text;
                    #endregion

                    Session["fcopy"] = "false";// added by YG
                    Session["cMITUMORI"] = lblcMitumori.Text;
                    Session["Jyuchuukingaku"] = txtJuuchuKingaku.Text;
                    Session["cPJ"] = txtJisyaBango.Text;
                    if (!String.IsNullOrEmpty(Request.QueryString["id"]))
                    {
                        String id = Request.QueryString["id"];
                        Response.Redirect("JC27UriageTouroku.aspx?id=" + id);
                    }
                    else
                    {
                        Response.Redirect("JC01Login.aspx");
                    }
                }
            }
        }

        protected void btnYes1_Click(object sender, EventArgs e)
        {
            btnMitumoriSave_Click(sender, e);
            if (saveSuccess == true)
            {
                if (HF_fBtn.Value == "btnMitumorishoPDF")
                {
                    btnInsatsuSave_Click(sender, e);
                    if (saveSuccess == true)
                    {
                        PrintPDF(sender, e);
                    }
                }
            }
        }
        #endregion

        #region btnNo_Click
        protected void btnNo_Click(object sender, EventArgs e)
        {
            if (HF_fBtn.Value == "lnkcBukken")
            {
                Session["cBukken"] = lnkcBukken.Text;
                if (!String.IsNullOrEmpty(Request.QueryString["id"]))
                {
                    String id = Request.QueryString["id"];
                    Response.Redirect("JC09BukkenSyousai.aspx?id=" + id);
                }
                else
                {
                    Response.Redirect("JC01Login.aspx");
                }
            }
            else if (HF_fBtn.Value == "btnNewMitumori")
            {
                Session["cBukken"] = lnkcBukken.Text;
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
            else if (HF_fBtn.Value == "btnTaMitumoriCopy")
            {
                SessionUtility.SetSession("HOME", "Master");
                String id = Request.QueryString["id"];
                ifShinkiPopup.Src = "JC12MitsumoriKensaku.aspx?id=" + id;
                mpeShinkiPopup.Show();
                updShinkiPopup.Update();
            }
            else if (HF_fBtn.Value == "btnCreateUriage")
            {
                Session["cUriage"] = null;

                #region deleted by YG
                //Session["cTokuisaki"] = lblcTOKUISAKI.Text;
                                                      //Session["sTokuisaki"] = lblsTOKUISAKI.Text;
                                                      //Session["TOUKUISAKITANTOU"] = lblsTOKUISAKI_TAN.Text;
                                                      //Session["TokuisakiTanJun"] = lblsTOKUISAKI_TAN_JUN.Text;
                                                      //Session["cSeikyusaki"] = lblcSEIKYUSAKI.Text;
                                                      //Session["sSeikyusaki"] = lblsSEIKYUSAKI.Text;
                                                      //Session["sSEIKYUSAKI_TAN"] = lblsSEIKYUSAKI_TAN.Text;
                                                      //Session["sSEIKYUSAKI_TAN_JUN"] = lblsSEIKYUSAKI_TAN_JUN.Text;
                                                      //Session["sMITUMORI"] = txtsMitumori.Text;
                #endregion

                Session["cMITUMORI"] = lblcMitumori.Text;
                Session["Jyuchuukingaku"] = txtJuuchuKingaku.Text;
                Session["cPJ"] = txtJisyaBango.Text;
                if (!String.IsNullOrEmpty(Request.QueryString["id"]))
                {
                    String id = Request.QueryString["id"];
                    Response.Redirect("JC27UriageTouroku.aspx?id=" + id);
                }
                else
                {
                    Response.Redirect("JC01Login.aspx");
                }
            }
        }

        protected void btnNo1_Click(object sender, EventArgs e)
        {
            if (HF_fBtn.Value == "btnMitumorishoPDF")
            {
                PrintPDF(sender, e);
            }
        }
        #endregion

        #region txtUkewatashibasho_TextChanged
        protected void txtUkewatashibasho_TextChanged(object sender, EventArgs e)
        {
            HF_isChange.Value = "1";
            updHeader.Update();
        }
        #endregion

        #region txtJuuchuKingaku_TextChanged
        protected void txtJuuchuKingaku_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Decimal number = Convert.ToDecimal(txtJuuchuKingaku.Text.Replace(",", ""));
                txtJuuchuKingaku.Text = number.ToString("#,##0");
                if (txtJuuchuKingaku.Text.StartsWith("-"))
                {
                    txtJuuchuKingaku.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    txtJuuchuKingaku.ForeColor = System.Drawing.Color.Black;
                }
            }
            catch
            {
                txtJuuchuKingaku.Text = "";
            }
            HF_isChange.Value = "1";
            updHeader.Update();
        }
        #endregion

        #region txtShanaiMemo_TextChanged
        protected void txtShanaiMemo_TextChanged(object sender, EventArgs e)
        {
            HF_isChange.Value = "1";
            updHeader.Update();
        }
        #endregion

        #region txtBikou_TextChanged
        protected void txtBikou_TextChanged(object sender, EventArgs e)
        {
            HF_isChange.Value = "1";
            updHeader.Update();
        }
        #endregion

        #region txtJisyaBango_TextChanged
        protected void txtJisyaBango_TextChanged(object sender, EventArgs e)
        {
            HF_isChange.Value = "1";
            updHeader.Update();
        }
        #endregion

        #region DDL_Kakudo_SelectedIndexChanged
        protected void DDL_Kakudo_SelectedIndexChanged(object sender, EventArgs e)
        {
            HF_isChange.Value = "1";
            updHeader.Update();
        }
        #endregion

        #region GetUriageColumn()
        private void GetUriageColumn()
        {
            String id = Request.QueryString["id"];
            DataTable dt_db = JC01Login_Class.Get_DB(id);
            String db = dt_db.Rows[0]["db"].ToString();
            String mail = dt_db.Rows[0]["mail"].ToString();
            ConstantVal.DB_NAME = db;

            JC07Home_Class JC07Home = new JC07Home_Class();
            JC07Home.loginId = mail;
            JC07Home.cListId = "3";
            dt_UriageKomoku = new DataTable();
            dt_UriageKomoku = JC07Home.KomokuSetting();
        }
        #endregion

        #region GetSyohinColumn()
        private void GetSyohinColumn()
        {
            String id = Request.QueryString["id"];
            DataTable dt_db = JC01Login_Class.Get_DB(id);
            String db = dt_db.Rows[0]["db"].ToString();
            String mail = dt_db.Rows[0]["mail"].ToString();
            ConstantVal.DB_NAME = db;

            JC07Home_Class JC07Home = new JC07Home_Class();
            JC07Home.loginId = mail;
            JC07Home.cListId = "4";
            dt_SyohinKomoku = new DataTable();
            dt_SyohinKomoku = JC07Home.KomokuSetting();
        }
        #endregion

        #region UriageKoumokuSort
        public void UriageKoumokuSort()
        {
            if (ViewState["dt_Uriage"] != null)
            {
                var columns = GV_Uriage.Columns.CloneFields();
                string[][] columnsGrid = HF_GridSizeUriage.Value.Split(':').Select(x=>x.Split(',')).ToArray();
                int drop_column = 30;
                int cUriage_column = 95;
                int cMitumori_column = 95;
                int sSeikyu_column = 160;
                int sTokui_column = 160;
                int sUriage_column = 160;
                int sTantou_column = 160;
                int dUriage_column = 95;
                int nUriage_column = 110;
                int UriageJoutai_column = 70;
                int sMemo_column = 165;

                #region getColumnWidth
                for (int i = 0; i < columnsGrid.Length; i++)
                {
                    if (columnsGrid[i][0] == "LB_drop")
                    {
                        drop_column = (int)Math.Round(Convert.ToDecimal(columnsGrid[i][1]));
                    }
                    else if (columnsGrid[i][0] == "cUriage")
                    {
                        cUriage_column = (int)Math.Round(Convert.ToDecimal(columnsGrid[i][1]));
                    }
                    else if(columnsGrid[i][0] == "cMitumori")
                    {
                        cMitumori_column = (int)Math.Round(Convert.ToDecimal(columnsGrid[i][1]));
                    }
                    else if (columnsGrid[i][0] == "sSeikyusaki")
                    {
                        sSeikyu_column = (int)Math.Round(Convert.ToDecimal(columnsGrid[i][1]));
                    }
                    else if (columnsGrid[i][0] == "sTokuisaki")
                    {
                        sTokui_column = (int)Math.Round(Convert.ToDecimal(columnsGrid[i][1]));
                    }
                    else if (columnsGrid[i][0] == "sUriage")
                    {
                        sUriage_column = (int)Math.Round(Convert.ToDecimal(columnsGrid[i][1]));
                    }
                    else if (columnsGrid[i][0] == "sTantou")
                    {
                        sTantou_column = (int)Math.Round(Convert.ToDecimal(columnsGrid[i][1]));
                    }
                    else if (columnsGrid[i][0] == "dUriage")
                    {
                        dUriage_column = (int)Math.Round(Convert.ToDecimal(columnsGrid[i][1]));
                    }
                    else if (columnsGrid[i][0] == "nUriage")
                    {
                        nUriage_column = (int)Math.Round(Convert.ToDecimal(columnsGrid[i][1]));
                    }
                    else if (columnsGrid[i][0] == "UriageJoutai")
                    {
                        UriageJoutai_column = (int)Math.Round(Convert.ToDecimal(columnsGrid[i][1]));
                    }
                    else if (columnsGrid[i][0] == "sMemo")
                    {
                        sMemo_column = (int)Math.Round(Convert.ToDecimal(columnsGrid[i][1]));
                    }
                }
                #endregion


                if (GV_Uriage_Original.Columns.Count > 0)
                {
                    GV_Uriage_Original.Columns.Clear();
                }
                DataTable dt = ViewState["dt_Uriage"] as DataTable;

                GV_Uriage.DataSource = dt;
                GV_Uriage.DataBind();

                GetUriageColumn();

                GV_Uriage_Original.Columns.Add(columns[0]);
                GV_Uriage_Original.Columns[0].HeaderStyle.Width = Unit.Pixel(drop_column);


                for (int i = 0; i < dt_UriageKomoku.Rows.Count; i++)
                {
                    if (dt_UriageKomoku.Rows[i]["sHYOUJI"].ToString() == "売上コード")
                    {
                        GV_Uriage_Original.Columns.Add(columns[1]);
                        GV_Uriage_Original.Columns[i+1].HeaderStyle.Width = Unit.Pixel(cUriage_column);
                    }
                    else if (dt_UriageKomoku.Rows[i]["sHYOUJI"].ToString() == "見積コード")
                    {
                        GV_Uriage_Original.Columns.Add(columns[2]);
                        GV_Uriage_Original.Columns[i + 1].HeaderStyle.Width = Unit.Pixel(cMitumori_column);
                    }
                    else if (dt_UriageKomoku.Rows[i]["sHYOUJI"].ToString() == "請求先")
                    {
                        GV_Uriage_Original.Columns.Add(columns[3]);
                        GV_Uriage_Original.Columns[i + 1].HeaderStyle.Width = Unit.Pixel(sSeikyu_column);
                    }
                    else if (dt_UriageKomoku.Rows[i]["sHYOUJI"].ToString() == "得意先")
                    {
                        GV_Uriage_Original.Columns.Add(columns[4]);
                        GV_Uriage_Original.Columns[i + 1].HeaderStyle.Width = Unit.Pixel(sTokui_column);
                    }
                    else if (dt_UriageKomoku.Rows[i]["sHYOUJI"].ToString() == "売上件名")
                    {
                        GV_Uriage_Original.Columns.Add(columns[5]);
                        GV_Uriage_Original.Columns[i + 1].HeaderStyle.Width = Unit.Pixel(sUriage_column);
                    }
                    else if (dt_UriageKomoku.Rows[i]["sHYOUJI"].ToString() == "自社担当者")
                    {
                        GV_Uriage_Original.Columns.Add(columns[6]);
                        GV_Uriage_Original.Columns[i + 1].HeaderStyle.Width = Unit.Pixel(sTantou_column);
                    }
                    else if (dt_UriageKomoku.Rows[i]["sHYOUJI"].ToString() == "売上日")
                    {
                        GV_Uriage_Original.Columns.Add(columns[7]);
                        GV_Uriage_Original.Columns[i + 1].HeaderStyle.Width = Unit.Pixel(dUriage_column);
                    }
                    else if (dt_UriageKomoku.Rows[i]["sHYOUJI"].ToString() == "売上金額")
                    {
                        GV_Uriage_Original.Columns.Add(columns[8]);
                        GV_Uriage_Original.Columns[i + 1].HeaderStyle.Width = Unit.Pixel(nUriage_column);
                    }
                    else if (dt_UriageKomoku.Rows[i]["sHYOUJI"].ToString() == "売上状態")
                    {
                        GV_Uriage_Original.Columns.Add(columns[9]);
                        GV_Uriage_Original.Columns[i + 1].HeaderStyle.Width = Unit.Pixel(UriageJoutai_column);
                    }
                    else if (dt_UriageKomoku.Rows[i]["sHYOUJI"].ToString() == "メモ")
                    {
                        GV_Uriage_Original.Columns.Add(columns[10]);
                        GV_Uriage_Original.Columns[i + 1].HeaderStyle.Width = Unit.Pixel(sMemo_column);
                    }
                }
                GV_Uriage_Original.DataSource = dt;
                GV_Uriage_Original.DataBind();

                GV_Uriage_Original.Width = Unit.Pixel(((int)Math.Round(Convert.ToDecimal(HF_GridUriage.Value))));

                Response.Cookies["colWidthmUraige"].Value = HF_GridSizeUriage.Value;
                Response.Cookies["colWidthmUraige"].Expires = DateTime.Now.AddYears(1);
                Response.Cookies["colWidthmUraigeGrid"].Value = HF_GridUriage.Value;
                Response.Cookies["colWidthmUraigeGrid"].Expires = DateTime.Now.AddYears(1);

                updUriageGrid.Update();
                updHeader.Update();

            }
        }
        #endregion

        #region SyohinKoumokuSort
        public void SyohinKoumokuSort()
        {
            if (ViewState["SyouhinTable"] != null)
            {
                //midashiTextboxWidth = 0;
                //BeforeSyoekiTextboxWidth = 0;
                //AfterSyoekiTextboxWidth = 0;
                beforegokeiColumn = true;
                var columns = GV_MitumoriSyohin_Original.Columns.CloneFields();
                string[][] columnsGrid = HF_GridSizeSyouhin.Value.Split(':').Select(x => x.Split(',')).ToArray();
                int chk_column = 30;
                int AddSyouhin_column = 30;
                int CopySyouhin_column = 30;
                int SyouhinSyosai_column = 30;
                int Kubun_column = 30;
                int cSyouhin_column = 95;
                int Syouhin_column = 30;
                int sSyouhin_column = 300;
                int Suryo_column = 70;
                int Tani_column = 58;
                int HyoujunTanka_column = 115;
                int Tanka_column = 115;
                int Kingaku_column = 115;
                int Gentaka_column = 115;
                int ritsu_column = 55;
                int Genkagokei_column = 115;
                int arari_column = 115;
                int araritsu_column = 115;
                int drag_column = 30;
                int dropdown_column = 30;

                #region getColumnWidth
                for (int i = 0; i < columnsGrid.Length; i++)
                {
                    if (columnsGrid[i][0] == "cSyouhin")
                    {
                        cSyouhin_column = (int)Math.Round(Convert.ToDecimal(columnsGrid[i][1]))-6;
                    }
                    else if (columnsGrid[i][0] == "sSyouhin")
                    {
                        sSyouhin_column = (int)Math.Round(Convert.ToDecimal(columnsGrid[i][1]))-6;
                    }
                    else if (columnsGrid[i][0] == "Syouryou")
                    {
                        Suryo_column = (int)Math.Round(Convert.ToDecimal(columnsGrid[i][1]))-6;
                    }
                    else if (columnsGrid[i][0] == "tani")
                    {
                        Tani_column = (int)Math.Round(Convert.ToDecimal(columnsGrid[i][1]))-6;
                    }
                    else if (columnsGrid[i][0] == "Hyoujuntanka")
                    {
                        HyoujunTanka_column = (int)Math.Round(Convert.ToDecimal(columnsGrid[i][1]))-6;
                    }
                    else if (columnsGrid[i][0] == "Tanka")
                    {
                        Tanka_column = (int)Math.Round(Convert.ToDecimal(columnsGrid[i][1]))-6;
                    }
                    else if (columnsGrid[i][0] == "kingaku")
                    {
                        Kingaku_column = (int)Math.Round(Convert.ToDecimal(columnsGrid[i][1]))-6;
                    }
                    else if (columnsGrid[i][0] == "gentanka")
                    {
                        Gentaka_column = (int)Math.Round(Convert.ToDecimal(columnsGrid[i][1]))-6;
                    }
                    else if (columnsGrid[i][0] == "ritsu")
                    {
                        ritsu_column = (int)Math.Round(Convert.ToDecimal(columnsGrid[i][1]))-6;
                    }
                    else if (columnsGrid[i][0] == "genkagokei")
                    {
                        Genkagokei_column = (int)Math.Round(Convert.ToDecimal(columnsGrid[i][1]))-6;
                    }
                    else if (columnsGrid[i][0] == "arari")
                    {
                        arari_column = (int)Math.Round(Convert.ToDecimal(columnsGrid[i][1]))-6;
                    }
                    else if (columnsGrid[i][0] == "araritsu")
                    {
                        araritsu_column = (int)Math.Round(Convert.ToDecimal(columnsGrid[i][1]))-6;
                    }
                }
                #endregion

                if (GV_MitumoriSyohin.Columns.Count > 0)
                {
                    GV_MitumoriSyohin.Columns.Clear();
                }
                DataTable dt = ViewState["SyouhinTable"] as DataTable;

                //GV_MitumoriSyohin_Original.DataSource = dt;
                //GV_MitumoriSyohin_Original.DataBind();
                //setSyosaiCount();

                GetSyohinColumn();

                GV_MitumoriSyohin.Columns.Add(columns[0]);
                GV_MitumoriSyohin.Columns.Add(columns[1]);
                GV_MitumoriSyohin.Columns.Add(columns[2]);
                GV_MitumoriSyohin.Columns.Add(columns[3]);
                GV_MitumoriSyohin.Columns.Add(columns[4]);
                GV_MitumoriSyohin.Columns.Add(columns[5]);

                GV_MitumoriSyohin.Columns[0].HeaderStyle.Width = Unit.Pixel(chk_column);
                GV_MitumoriSyohin.Columns[1].HeaderStyle.Width = Unit.Pixel(AddSyouhin_column);
                GV_MitumoriSyohin.Columns[2].HeaderStyle.Width = Unit.Pixel(CopySyouhin_column);
                GV_MitumoriSyohin.Columns[3].HeaderStyle.Width = Unit.Pixel(SyouhinSyosai_column);
                GV_MitumoriSyohin.Columns[4].HeaderStyle.Width = Unit.Pixel(dropdown_column);
                //GV_MitumoriSyohin.Columns[3].HeaderStyle.Width = Unit.Pixel(SyouhinSyosai_column);

                DataTable dtTextbox = new DataTable();
                dtTextbox.Columns.Add("ColumnID");
                for (int i = 0; i < dt_SyohinKomoku.Rows.Count; i++)
                {
                    if (i == 0)
                    {
                        if (dt_SyohinKomoku.Rows[i]["sHYOUJI"].ToString() == "商品コード")
                        {
                            GV_MitumoriSyohin.Columns.Add(columns[6]);
                            GV_MitumoriSyohin.Columns.Add(columns[7]);

                            GV_MitumoriSyohin.Columns[i + 6].HeaderStyle.Width = Unit.Pixel(cSyouhin_column);
                            GV_MitumoriSyohin.Columns[i + 7].HeaderStyle.Width = Unit.Pixel(Syouhin_column);

                            dtTextbox.Rows.Add("txtcSYOHIN");
                            dtTextbox.Rows.Add("txtsSYOHIN");
                            dtTextbox.Rows.Add("txtnSURYO");
                            dtTextbox.Rows.Add("txtTani");
                            dtTextbox.Rows.Add("txtnTANKA");

                            GV_MitumoriSyohin.Columns.Add(columns[8]); //商品名


                            GV_MitumoriSyohin.Columns.Add(columns[9]); //数量


                            GV_MitumoriSyohin.Columns.Add(columns[10]); //単位


                            GV_MitumoriSyohin.Columns.Add(columns[11]); //標準単価


                            //GV_MitumoriSyohin.Columns.Add(columns[12]); //合計金額

                            GV_MitumoriSyohin.Columns[i + 8].HeaderStyle.Width = Unit.Pixel(sSyouhin_column);
                            GV_MitumoriSyohin.Columns[i + 9].HeaderStyle.Width = Unit.Pixel(Suryo_column);
                            GV_MitumoriSyohin.Columns[i + 10].HeaderStyle.Width = Unit.Pixel(Tani_column);
                            GV_MitumoriSyohin.Columns[i + 11].HeaderStyle.Width = Unit.Pixel(HyoujunTanka_column);
                            //GV_MitumoriSyohin.Columns[i + 11].HeaderStyle.Width = Unit.Pixel(Kingaku_column);

                            //GokeiColumn_Position = "YES";
                            GokeiColumn = 6;
                            beforegokeiColumn = false;
                            i += 4;
                            HF_syohinIndex.Value = "6";
                        }
                        else
                        {
                            GV_MitumoriSyohin.Columns.Add(columns[7]);

                            GV_MitumoriSyohin.Columns.Add(columns[8]); //商品名

                            GV_MitumoriSyohin.Columns.Add(columns[9]); //数量

                            GV_MitumoriSyohin.Columns.Add(columns[10]); //単位

                            GV_MitumoriSyohin.Columns.Add(columns[11]); //標準単価

                            //GV_MitumoriSyohin.Columns.Add(columns[12]); //合計金額

                            GV_MitumoriSyohin.Columns[i + 6].HeaderStyle.Width = Syouhin_column;
                            GV_MitumoriSyohin.Columns[i + 7].HeaderStyle.Width = sSyouhin_column;
                            GV_MitumoriSyohin.Columns[i + 8].HeaderStyle.Width = Unit.Pixel(Suryo_column);
                            GV_MitumoriSyohin.Columns[i + 9].HeaderStyle.Width = Unit.Pixel(Tani_column);
                            GV_MitumoriSyohin.Columns[i + 10].HeaderStyle.Width = Unit.Pixel(HyoujunTanka_column);
                            //GV_MitumoriSyohin.Columns[i + 10].HeaderStyle.Width = Unit.Pixel(Kingaku_column);

                            dtTextbox.Rows.Add("txtsSYOHIN");
                            dtTextbox.Rows.Add("txtnSURYO");
                            dtTextbox.Rows.Add("txtTani");
                            dtTextbox.Rows.Add("txtnTANKA");

                            //GokeiColumn_Position = "YES";
                            GokeiColumn = 5;
                            beforegokeiColumn = false;
                            i += 3;
                            HF_syohinIndex.Value = "5";
                        }
                    }
                    else
                    {
                        if (dt_SyohinKomoku.Rows[i]["sHYOUJI"].ToString() == "商品コード" && i != 0)
                        {
                            dtTextbox.Rows.Add("txtcSYOHIN");
                            GV_MitumoriSyohin.Columns.Add(columns[6]);
                            GV_MitumoriSyohin.Columns[i + 7].HeaderStyle.Width = Unit.Pixel(cSyouhin_column);
                        }
                        else if (dt_SyohinKomoku.Rows[i]["sHYOUJI"].ToString() == "合計金額")
                        {
                            GV_MitumoriSyohin.Columns.Add(columns[13]); //合計金額
                            GV_MitumoriSyohin.Columns[i + 7].HeaderStyle.Width = Unit.Pixel(Kingaku_column);
                            GokeiColumn_Position = "YES";
                            //GokeiColumn = i;
                            GokeiCol = i;
                        }
                        else if (dt_SyohinKomoku.Rows[i]["sHYOUJI"].ToString() == "単価")
                        {
                            GV_MitumoriSyohin.Columns.Add(columns[12]);
                            GV_MitumoriSyohin.Columns[i + 7].HeaderStyle.Width = Unit.Pixel(Tanka_column);

                        }
                        else if (dt_SyohinKomoku.Rows[i]["sHYOUJI"].ToString() == "原価単価")
                        {
                            dtTextbox.Rows.Add("txtnGENKATANKA");
                            GV_MitumoriSyohin.Columns.Add(columns[14]);
                            GV_MitumoriSyohin.Columns[i + 7].HeaderStyle.Width = Unit.Pixel(Gentaka_column);

                        }
                        else if (dt_SyohinKomoku.Rows[i]["sHYOUJI"].ToString() == "掛率")
                        {
                            dtTextbox.Rows.Add("txtnRITU");
                            GV_MitumoriSyohin.Columns.Add(columns[15]);
                            GV_MitumoriSyohin.Columns[i + 7].HeaderStyle.Width = Unit.Pixel(ritsu_column);

                        }
                        else if (dt_SyohinKomoku.Rows[i]["sHYOUJI"].ToString() == "原価合計")
                        {
                            GV_MitumoriSyohin.Columns.Add(columns[16]);
                            GV_MitumoriSyohin.Columns[i + 7].HeaderStyle.Width = Unit.Pixel(Genkagokei_column);

                        }
                        else if (dt_SyohinKomoku.Rows[i]["sHYOUJI"].ToString() == "粗利")
                        {
                            GV_MitumoriSyohin.Columns.Add(columns[17]);
                            GV_MitumoriSyohin.Columns[i + 7].HeaderStyle.Width = Unit.Pixel(arari_column);

                        }
                        else if (dt_SyohinKomoku.Rows[i]["sHYOUJI"].ToString() == "粗利率")
                        {
                            GV_MitumoriSyohin.Columns.Add(columns[18]);
                            GV_MitumoriSyohin.Columns[i + 7].HeaderStyle.Width = Unit.Pixel(araritsu_column);

                        }
                    }
                }
                
                GV_MitumoriSyohin.Columns.Add(columns[19]);
                GV_MitumoriSyohin.Columns.Add(columns[20]);

                GV_MitumoriSyohin.Columns[dt_SyohinKomoku.Rows.Count+7].HeaderStyle.Width = Unit.Pixel(drag_column);
                GV_MitumoriSyohin.Columns[dt_SyohinKomoku.Rows.Count + 8].HeaderStyle.Width = Unit.Pixel(dropdown_column);

                HF_dragIndex.Value = (dt_SyohinKomoku.Rows.Count + 6).ToString();
                HF_dropIndex.Value = (dt_SyohinKomoku.Rows.Count + 7).ToString();

                ViewState["dtGridTextbox"] = dtTextbox;

                GV_MitumoriSyohin.DataSource = dt;
                GV_MitumoriSyohin.DataBind();

                GV_MitumoriSyohin.Width = Unit.Pixel(((int)Math.Round(Convert.ToDecimal(HF_GridSyouhin.Value))));

                Response.Cookies["colWidthmSyouhin"].Value = HF_GridSizeSyouhin.Value;
                Response.Cookies["colWidthmSyouhin"].Expires = DateTime.Now.AddYears(1);
                Response.Cookies["colWidthmSyouhinGrid"].Value = HF_GridSyouhin.Value;
                Response.Cookies["colWidthmSyouhinGrid"].Expires = DateTime.Now.AddYears(1);

                //SetSyosai();
                updMitsumoriSyohinGrid.Update();
                updHeader.Update();

            }
        }
        #endregion

        #region SetSyosai
        public void SetSyosai()
        {
            try
            {
                foreach (GridViewRow row in GV_MitumoriSyohin_Original.Rows)
                {
                    Button btn = (row.FindControl("btnSyohinShosai") as Button);
                    (GV_MitumoriSyohin.Rows[row.RowIndex].FindControl("btnSyohinShosai") as Button).Text = btn.Text;
                }
                updMitsumoriSyohinGrid.Update();
                updHeader.Update();
            }
            catch { }
        }
        #endregion

        #region btnMidashitsuika_Click
        protected void btnMidashitsuika_Click(object sender, EventArgs e)
        {
            List<int> rowindex = new List<int>();
            DataTable dt = CreateSyouhinTableColomn();
            foreach (GridViewRow row in GV_MitumoriSyohin_Original.Rows)
            {
                Label lbl_status = (row.FindControl("lblhdnStatus") as Label);
                Label lbl_fgenkataka = (row.FindControl("lblfgenkatanka") as Label);
                Label lbl_rowNo = (row.FindControl("lblRowNo") as Label);
                TextBox txt_cSyohin = (row.FindControl("txtcSYOHIN") as TextBox);
                TextBox txt_sSyohin = (row.FindControl("txtsSYOHIN") as TextBox);
                TextBox txt_nSyoryo = (row.FindControl("txtnSURYO") as TextBox);
                //DropDownList ddl_cTani = (row.FindControl("DDL_cTANI") as DropDownList);
                TextBox txt_cTani = (row.FindControl("txtTani") as TextBox);
                TextBox txt_nTanka = (row.FindControl("txtnTANKA") as TextBox);
                Label lbl_TankaGokei = (row.FindControl("lblTankaGokei") as Label);
                TextBox txt_nGenkaTanka = (row.FindControl("txtnGENKATANKA") as TextBox);
                Label lbl_GenkaGokei = (row.FindControl("lblGenkaGokei") as Label);
                Label lbl_Arari = (row.FindControl("lblnARARI") as Label);
                Label lbl_ArariSu = (row.FindControl("lblnARARISu") as Label);
                TextBox txt_nRITU = (row.FindControl("txtnRITU") as TextBox);
                Label lbl_kubun = (row.FindControl("lblKubun") as Label);
                Label lbl_nSIKIRITANKA = (row.FindControl("lblTanka") as Label);
                Label lbl_fJITAIS = (row.FindControl("lblfjitais") as Label);
                Label lbl_fJITAIQ = (row.FindControl("lblfjitaiq") as Label);
                if (lbl_status.Text == "1")
                {
                    rowindex.Add(row.RowIndex);
                }
                DataRow dr = dt.NewRow();
                dr[0] = "0";
                dr[1] = txt_cSyohin.Text;
                dr[2] = txt_sSyohin.Text;
                dr[3] = txt_nSyoryo.Text;
                dr[4] = txt_cTani.Text;
                dr[5] = txt_nTanka.Text;
                dr[6] = lbl_TankaGokei.Text;
                dr[7] = txt_nGenkaTanka.Text;
                dr[8] = lbl_GenkaGokei.Text;
                dr[9] = lbl_Arari.Text;
                dr[10] = lbl_ArariSu.Text;
                dr[11] = lbl_fgenkataka.Text;
                dr[12] = lbl_rowNo.Text;
                dr[13] = txt_nRITU.Text;
                dr[14] = lbl_kubun.Text;
                dr[15] = lbl_nSIKIRITANKA.Text;
                dr[16] = lbl_fJITAIS.Text;
                dr[17] = lbl_fJITAIQ.Text;
                dt.Rows.Add(dr);
            }
            var max = dt.AsEnumerable().Max(x => int.Parse(x.Field<string>("rowNo")));
            if (rowindex.Count == 1)
            {
                DataRow dr = dt.NewRow();
                dr[0] = "0";
                dr[1] = "";
                dr[3] = "";
                dr[4] = "";
                dr[5] = "";
                dr[6] = "";
                dr[7] = "";
                dr[8] = "";
                dr[9] = "";
                dr[10] = "";
                dr[11] = "0";
                dr[12] = (Convert.ToInt32(max) + 1).ToString();
                dr[13] = "100%";
                dr[14] = "見";
                dr[15] = "";
                dr[16] = "0";
                dr[17] = "0";
                dt.Rows.InsertAt(dr, rowindex[0]);
                HF_maxRowNo.Value = (Convert.ToInt32(max) + 1).ToString();
            }
            else
            {
                int first_range = rowindex[0];
                int last_range = rowindex[rowindex.Count - 1];
                var result = Enumerable.Range(first_range, last_range).Except(rowindex);
                int insert_row = 0;
                if (result.Count() > 0)
                {
                    int prerowIndex = 0;
                    for (int i = 0; i < rowindex.Count; i++)
                    {
                        if (i == 0)
                        {
                            max = max + 1;
                            DataRow dr = dt.NewRow();
                            dr[0] = "0";
                            dr[1] = "";
                            dr[3] = "";
                            dr[4] = "";
                            dr[5] = "";
                            dr[6] = "";
                            dr[7] = "";
                            dr[8] = "";
                            dr[9] = "";
                            dr[10] = "";
                            dr[11] = "0";
                            dr[12] = max.ToString();
                            dr[13] = "100%";
                            dr[14] = "見";
                            dr[15] = "";
                            dr[16] = "0";
                            dr[17] = "0";
                            dt.Rows.InsertAt(dr, rowindex[i] + insert_row);
                            insert_row += 1;
                        }
                        else
                        {
                            if (rowindex[i] - prerowIndex != 1)
                            {
                                max = max + 1;
                                DataRow dr = dt.NewRow();
                                dr[0] = "0";
                                dr[1] = "";
                                dr[3] = "";
                                dr[4] = "";
                                dr[5] = "";
                                dr[6] = "";
                                dr[7] = "";
                                dr[8] = "";
                                dr[9] = "";
                                dr[10] = "";
                                dr[11] = "0";
                                dr[12] = max.ToString();
                                dr[13] = "100%";
                                dr[14] = "計";
                                dr[15] = "";
                                dr[16] = "0";
                                dr[17] = "0";
                                dt.Rows.InsertAt(dr, prerowIndex + insert_row + 1);
                                insert_row += 1;

                                dr = dt.NewRow();
                                dr[0] = "0";
                                dr[1] = "";
                                dr[3] = "";
                                dr[4] = "";
                                dr[5] = "";
                                dr[6] = "";
                                dr[7] = "";
                                dr[8] = "";
                                dr[9] = "";
                                dr[10] = "";
                                dr[11] = "0";
                                dr[12] = max.ToString();
                                dr[13] = "100%";
                                dr[14] = "見";
                                dr[15] = "";
                                dr[16] = "0";
                                dr[17] = "0";
                                dt.Rows.InsertAt(dr, rowindex[i] + insert_row);
                                insert_row += 1;

                            }
                        }
                        if (i == rowindex.Count - 1)
                        {
                            max = max + 1;
                            DataRow dr = dt.NewRow();
                            dr[0] = "0";
                            dr[1] = "";
                            dr[3] = "";
                            dr[4] = "";
                            dr[5] = "";
                            dr[6] = "";
                            dr[7] = "";
                            dr[8] = "";
                            dr[9] = "";
                            dr[10] = "";
                            dr[11] = "0";
                            dr[12] = max.ToString();
                            dr[13] = "100%";
                            dr[14] = "計";
                            dr[15] = "";
                            dr[16] = "0";
                            dr[17] = "0";
                            dt.Rows.InsertAt(dr, rowindex[i] + insert_row + 1);
                        }
                        prerowIndex = rowindex[i];
                    }
                    HF_maxRowNo.Value = max.ToString();
                }
                else
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = "0";
                    dr[1] = "";
                    dr[3] = "";
                    dr[4] = "";
                    dr[5] = "";
                    dr[6] = "";
                    dr[7] = "";
                    dr[8] = "";
                    dr[9] = "";
                    dr[10] = "";
                    dr[11] = "0";
                    dr[12] = (Convert.ToInt32(max) + 1).ToString();
                    dr[13] = "100%";
                    dr[14] = "見";
                    dr[15] = "";
                    dr[16] = "0";
                    dr[17] = "0";
                    dt.Rows.InsertAt(dr, rowindex[0]);
                    HF_maxRowNo.Value = (Convert.ToInt32(max) + 1).ToString();

                    dr = dt.NewRow();
                    dr[0] = "0";
                    dr[1] = "";
                    dr[3] = "";
                    dr[4] = "";
                    dr[5] = "";
                    dr[6] = "";
                    dr[7] = "";
                    dr[8] = "";
                    dr[9] = "";
                    dr[10] = "";
                    dr[11] = "0";
                    dr[12] = (Convert.ToInt32(max) + 2).ToString();
                    dr[13] = "100%";
                    dr[14] = "計";
                    dr[15] = "";
                    dr[16] = "0";
                    dr[17] = "0";
                    dt.Rows.InsertAt(dr, rowindex[rowindex.Count - 1] + 2);
                    HF_maxRowNo.Value = (Convert.ToInt32(max) + 2).ToString();
                }
            }
            DataTable dt_MidashiSyokei = new DataTable();
            dt_MidashiSyokei = SetMidashiSyokei(dt);
            GV_MitumoriSyohin_Original.DataSource = dt_MidashiSyokei;
            GV_MitumoriSyohin_Original.DataBind();
            ViewState["SyouhinTable"] = dt_MidashiSyokei;
            DataTable dt_Syosai = GetSyosaiGridViewData();
            setSyosaiCount(dt_MidashiSyokei, dt_Syosai);
            GV_MitumoriSyohin.DataSource = dt_MidashiSyokei;
            GV_MitumoriSyohin.DataBind();
            HF_isChange.Value = "1";
            HasCheckRow();
            //SetSyosai();
            updMitsumoriSyohinGrid.Update();
            updHeader.Update();
        }
        #endregion

        #region SetMidashiSyokei
        private DataTable SetMidashiSyokei(DataTable dt)
        {
            double syokei_kingaku = 0;
            int midashi_row = -1;
            int r = 0;
            List<int> rowIndex = new List<int>();

            foreach (DataRow dr in dt.Rows)
            {
                if (dr[14].ToString().Equals("間"))
                {
                    dr[14] = "";
                    dt.AcceptChanges();
                }
            }

            foreach (DataRow dr in dt.Rows)
            {
                if (dr[14].ToString().Equals("見"))
                {
                    syokei_kingaku = 0;
                    midashi_row = r;
                }
                else if (!dr[14].ToString().Equals("見") && !dr[14].ToString().Equals("計"))
                {
                    double gokei = 0;
                    try
                    {
                        gokei = Convert.ToDouble(dr[6].ToString().Replace(",", ""));
                    }
                    catch { }
                    syokei_kingaku += gokei;
                    rowIndex.Add(r);
                }
                else if (dr[14].ToString().Equals("計"))
                {
                    dr[6] = syokei_kingaku.ToString("#,##0.##");
                    dr[3] = "0";
                    dr[5] = "0";
                    dr[7] = "0";
                    dr[15] = "0";
                    dr[13] = "100%";
                    dr[10] = "100%";
                    dr[9] = syokei_kingaku.ToString("#,##0.##");
                    if (midashi_row != -1)
                    {
                        dt.Rows[midashi_row][6] = syokei_kingaku.ToString("#,##0.##");
                        dt.Rows[midashi_row][3] = "0";
                        dt.Rows[midashi_row][5] = "0";
                        dt.Rows[midashi_row][7] = "0";
                        dt.Rows[midashi_row][15] = "0";
                        dt.Rows[midashi_row][13] = "100%";
                        dt.Rows[midashi_row][10] = "100%";
                        dt.Rows[midashi_row][9] = syokei_kingaku.ToString("#,##0.##");
                        midashi_row = -1;
                    }
                    syokei_kingaku = 0;

                    foreach (int item in rowIndex)
                    {
                        dt.Rows[item][14] = "間";
                    }
                    rowIndex.Clear();
                }
                r++;
            }
            return dt;
        }
        #endregion

        #region btnShokeitsuika_Click
        protected void btnShokeitsuika_Click(object sender, EventArgs e)
        {
            List<int> rowindex = new List<int>();
            DataTable dt = CreateSyouhinTableColomn();
            foreach (GridViewRow row in GV_MitumoriSyohin_Original.Rows)
            {
                Label lbl_status = (row.FindControl("lblhdnStatus") as Label);
                Label lbl_fgenkataka = (row.FindControl("lblfgenkatanka") as Label);
                Label lbl_rowNo = (row.FindControl("lblRowNo") as Label);
                TextBox txt_cSyohin = (row.FindControl("txtcSYOHIN") as TextBox);
                TextBox txt_sSyohin = (row.FindControl("txtsSYOHIN") as TextBox);
                TextBox txt_nSyoryo = (row.FindControl("txtnSURYO") as TextBox);
                //DropDownList ddl_cTani = (row.FindControl("DDL_cTANI") as DropDownList);
                TextBox txt_cTani = (row.FindControl("txtTani") as TextBox);
                TextBox txt_nTanka = (row.FindControl("txtnTANKA") as TextBox);
                Label lbl_TankaGokei = (row.FindControl("lblTankaGokei") as Label);
                TextBox txt_nGenkaTanka = (row.FindControl("txtnGENKATANKA") as TextBox);
                Label lbl_GenkaGokei = (row.FindControl("lblGenkaGokei") as Label);
                Label lbl_Arari = (row.FindControl("lblnARARI") as Label);
                Label lbl_ArariSu = (row.FindControl("lblnARARISu") as Label);
                TextBox txt_nRITU = (row.FindControl("txtnRITU") as TextBox);
                Label lbl_kubun = (row.FindControl("lblKubun") as Label);
                Label lbl_nSIKIRIKINGAKU = (row.FindControl("lblTanka") as Label);
                Label lbl_fJITAIS = (row.FindControl("lblfjitais") as Label);
                Label lbl_fJITAIQ = (row.FindControl("lblfjitaiq") as Label);
                if (lbl_status.Text == "1")
                {
                    rowindex.Add(row.RowIndex);
                }
                DataRow dr = dt.NewRow();
                dr[0] = "0";
                dr[1] = txt_cSyohin.Text;
                dr[2] = txt_sSyohin.Text;
                dr[3] = txt_nSyoryo.Text;
                dr[4] = txt_cTani.Text;
                dr[5] = txt_nTanka.Text;
                dr[6] = lbl_TankaGokei.Text;
                dr[7] = txt_nGenkaTanka.Text;
                dr[8] = lbl_GenkaGokei.Text;
                dr[9] = lbl_Arari.Text;
                dr[10] = lbl_ArariSu.Text;
                dr[11] = lbl_fgenkataka.Text;
                dr[12] = lbl_rowNo.Text;
                dr[13] = txt_nRITU.Text;
                dr[14] = lbl_kubun.Text;
                dr[15] = lbl_nSIKIRIKINGAKU.Text;
                dr[16] = lbl_fJITAIS.Text;
                dr[17] = lbl_fJITAIQ.Text;
                dt.Rows.Add(dr);
            }
            var max = dt.AsEnumerable().Max(x => int.Parse(x.Field<string>("rowNo")));
            if (rowindex.Count == 1)
            {
                DataRow dr = dt.NewRow();
                dr[0] = "0";
                dr[1] = "";
                dr[3] = "";
                dr[4] = "";
                dr[5] = "";
                dr[6] = "";
                dr[7] = "";
                dr[8] = "";
                dr[9] = "";
                dr[10] = "";
                dr[11] = "0";
                dr[12] = (Convert.ToInt32(max) + 1).ToString();
                dr[13] = "100%";
                dr[14] = "計";
                dr[15] = "";
                dr[16] = "0";
                dr[17] = "0";
                dt.Rows.InsertAt(dr, rowindex[0] + 1);
                HF_maxRowNo.Value = (Convert.ToInt32(max) + 1).ToString();
            }
            else
            {
                int first_range = rowindex[0];
                int last_range = rowindex[rowindex.Count - 1];
                var result = Enumerable.Range(first_range, last_range).Except(rowindex);
                int insert_row = 0;
                if (result.Count() > 0)
                {
                    int prerowIndex = 0;
                    for (int i = 0; i < rowindex.Count; i++)
                    {
                        if (i == 0)
                        {
                            max = max + 1;
                            DataRow dr = dt.NewRow();
                            dr[0] = "0";
                            dr[1] = "";
                            dr[3] = "";
                            dr[4] = "";
                            dr[5] = "";
                            dr[6] = "";
                            dr[7] = "";
                            dr[8] = "";
                            dr[9] = "";
                            dr[10] = "";
                            dr[11] = "0";
                            dr[12] = max.ToString();
                            dr[13] = "100%";
                            dr[14] = "見";
                            dr[15] = "";
                            dr[16] = "0";
                            dr[17] = "0";
                            dt.Rows.InsertAt(dr, rowindex[i] + insert_row);
                            insert_row += 1;
                        }
                        else
                        {
                            if (rowindex[i] - prerowIndex != 1)
                            {
                                max = max + 1;
                                DataRow dr = dt.NewRow();
                                dr[0] = "0";
                                dr[1] = "";
                                dr[3] = "";
                                dr[4] = "";
                                dr[5] = "";
                                dr[6] = "";
                                dr[7] = "";
                                dr[8] = "";
                                dr[9] = "";
                                dr[10] = "";
                                dr[11] = "0";
                                dr[12] = max.ToString();
                                dr[13] = "100%";
                                dr[14] = "計";
                                dr[15] = "";
                                dr[16] = "0";
                                dr[17] = "0";
                                dt.Rows.InsertAt(dr, prerowIndex + insert_row + 1);
                                insert_row += 1;

                                dr = dt.NewRow();
                                dr[0] = "0";
                                dr[1] = "";
                                dr[3] = "";
                                dr[4] = "";
                                dr[5] = "";
                                dr[6] = "";
                                dr[7] = "";
                                dr[8] = "";
                                dr[9] = "";
                                dr[10] = "";
                                dr[11] = "0";
                                dr[12] = max.ToString();
                                dr[13] = "100%";
                                dr[14] = "見";
                                dr[15] = "";
                                dr[16] = "0";
                                dr[17] = "0";
                                dt.Rows.InsertAt(dr, rowindex[i] + insert_row);
                                insert_row += 1;

                            }
                        }
                        if (i == rowindex.Count - 1)
                        {
                            max = max + 1;
                            DataRow dr = dt.NewRow();
                            dr[0] = "0";
                            dr[1] = "";
                            dr[3] = "";
                            dr[4] = "";
                            dr[5] = "";
                            dr[6] = "";
                            dr[7] = "";
                            dr[8] = "";
                            dr[9] = "";
                            dr[10] = "";
                            dr[11] = "0";
                            dr[12] = max.ToString();
                            dr[13] = "100%";
                            dr[14] = "計";
                            dr[15] = "";
                            dr[16] = "0";
                            dr[17] = "0";
                            dt.Rows.InsertAt(dr, rowindex[i] + insert_row + 1);
                        }
                        prerowIndex = rowindex[i];
                    }
                    HF_maxRowNo.Value = max.ToString();
                }
                else
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = "0";
                    dr[1] = "";
                    dr[3] = "";
                    dr[4] = "";
                    dr[5] = "";
                    dr[6] = "";
                    dr[7] = "";
                    dr[8] = "";
                    dr[9] = "";
                    dr[10] = "";
                    dr[11] = "0";
                    dr[12] = (Convert.ToInt32(max) + 1).ToString();
                    dr[13] = "100%";
                    dr[14] = "見";
                    dr[15] = "";
                    dr[16] = "0";
                    dr[17] = "0";
                    dt.Rows.InsertAt(dr, rowindex[0]);
                    HF_maxRowNo.Value = (Convert.ToInt32(max) + 1).ToString();

                    dr = dt.NewRow();
                    dr[0] = "0";
                    dr[1] = "";
                    dr[3] = "";
                    dr[4] = "";
                    dr[5] = "";
                    dr[6] = "";
                    dr[7] = "";
                    dr[8] = "";
                    dr[9] = "";
                    dr[10] = "";
                    dr[11] = "0";
                    dr[12] = (Convert.ToInt32(max) + 2).ToString();
                    dr[13] = "100%";
                    dr[14] = "計";
                    dr[15] = "";
                    dr[16] = "0";
                    dr[17] = "0";
                    dt.Rows.InsertAt(dr, rowindex[rowindex.Count - 1] + 2);
                    HF_maxRowNo.Value = (Convert.ToInt32(max) + 2).ToString();
                }
            }
            DataTable dt_MidashiSyokei = new DataTable();
            dt_MidashiSyokei = SetMidashiSyokei(dt);
            GV_MitumoriSyohin_Original.DataSource = dt_MidashiSyokei;
            GV_MitumoriSyohin_Original.DataBind();
            ViewState["SyouhinTable"] = dt_MidashiSyokei;
            DataTable dt_Syosai = GetSyosaiGridViewData();
            setSyosaiCount(dt_MidashiSyokei, dt_Syosai);
            GV_MitumoriSyohin.DataSource = dt_MidashiSyokei;
            GV_MitumoriSyohin.DataBind();
            HF_isChange.Value = "1";
            HasCheckRow();
            //SetSyosai();
            updMitsumoriSyohinGrid.Update();
            updHeader.Update();
        }
        #endregion

        #region btnTamitsumoriSyohinCopy_Click
        protected void btnTamitsumoriSyohinCopy_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(HF_syouhinRowIndex.Value))
            {
                HF_syouhinRowIndex.Value = "0";
            }
            Session["ftaMituSyohin"] = "1";
            HF_fBtn.Value = "btnTaMitumoriSyohinCopy";
            SessionUtility.SetSession("HOME", "Master");
            String id = Request.QueryString["id"];
            ifShinkiPopup.Src = "JC12MitsumoriKensaku.aspx?id=" + id;
            mpeShinkiPopup.Show();
            updShinkiPopup.Update();
        }
        #endregion

        #region btn_CloseMitumoriSearch_Click
        protected void btn_CloseMitumoriSearch_Click(object sender, EventArgs e)
        {
            ifShinkiPopup.Src = "";
            mpeShinkiPopup.Hide();
            updShinkiPopup.Update();
            if (Session["cMitumori"] != null)
            {
                String cMitumori = Session["cMitumori"].ToString();
                if (HF_fBtn.Value == "btnTaMitumoriSyohinCopy")
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

                    DataTable dt_Syohin = new DataTable();
                    dt_Syohin = Session["TaMituSyouhinTable"] as DataTable;

                    DataTable dt_Syosai = new DataTable();
                    dt_Syosai = Session["TaMituSyousaiTable"] as DataTable;

                    #region ToDelete
                    //#region 明細商品のSQL
                    //string sql = "Select " +
                    //    " IfNull(r_mitumori_m.cMITUMORI, '') As cMITUMORI, " +
                    //    " case r_mitumori_m.nINSATSU_GYO " +
                    //    " when 0 then '' " +
                    //    " else cast(r_mitumori_m.nINSATSU_GYO as char) " +
                    //    " end As nINSATSU_GYO," +
                    //    " IfNull(r_mitumori_m.cMITUMORI_KO, '') As cMITUMORI_KO," +
                    //    " IfNull(r_mitumori_m.nGYOUNO, 0) As nGYOUNO," +
                    //    " IfNull(r_mitumori_m.cSYOUHIN, '') As cSYOUHIN," +
                    //    " IfNull(r_mitumori_m.sSYOUHIN_R, '') As sSYOUHIN_R," +
                    //    " format( IfNull(r_mitumori_m.nTANKA, 0),0)  As nTANKA," +
                    //    " format(IfNull(r_mitumori_m.nSURYO, 0),2) As nSURYO," +
                    //    " IfNull(r_mitumori_m.cSHIIRESAKI, '') As cSHIIRESAKI," +
                    //    " IfNull(r_mitumori_m.sSHIIRESAKI, '') As sSHIIRESAKI," +
                    //    " format( IfNull(r_mitumori_m.nSIIRETANKA, 0),0) As nSIIRETANKA," +
                    //    " format( IfNull(r_mitumori_m.nSIIREKINGAKU, 0),0) As nSIIREKINGAKU," +
                    //    " IfNull(r_mitumori_m.nSIKIRITANKA, 0) As nSIKIRITANKA," +
                    //    " format( IfNull(r_mitumori_m.nSIKIRIKINGAKU, 0),0) As nSIKIRIKINGAKU," +
                    //    " IfNull(r_mitumori_m.sTANI, '') As sTANI," +
                    //    " IfNull(r_mitumori_m.nKINGAKU, 0) As nKINGAKU," +
                    //    " IfNull(r_mitumori_m.nRITU, 100) As nRITU," +
                    //    " IfNull(r_mitumori_m.cSYOUSAI, '') As cSYOUSAI," +
                    //    " IfNull(r_mitumori_m.sSETSUMUI, '') As sSETSUMUI," +
                    //    " IfNull(r_mitumori_m.fJITAIS, 0) As fJITAIS," +
                    //    " IfNull(r_mitumori_m.fJITAIQ, 0) As fJITAIQ," +
                    //    " ifnull(ms.fKazei, 0) AS fkazei," +
                    //    " format( IfNull(r_mitumori_m.nSIKIRIKINGAKU - r_mitumori_m.nSIIREKINGAKU,0),0)  As nARARI," +
                    //    " CONCAT(FORMAT(IfNull((r_mitumori_m.nSIKIRIKINGAKU - r_mitumori_m.nSIIREKINGAKU) / r_mitumori_m.nSIKIRIKINGAKU,0) * 100,1),'%') As nARARISu," +
                    //    " r_mitumori_m.rowNO AS rowNO," +
                    //    " ifnull(r_mitumori_m.sMEMO, '') as sMEMO," +
                    //    " ifnull(r_mitumori_m.fCHECK, '') as fCHECK," +
                    //    " IfNull(r_mitumori_m.fgentankatanka, '0') As fgentankatanka,'0' As jissekigenka," +
                    //    " IfNull(r_mitumori_m.sKUBUN, '') As sKUBUN " +
                    //    " From r_mitumori_m" +
                    //    " left join  m_syouhin ms ON r_mitumori_m.cSYOUHIN = ms.cSYOUHIN " +
                    //    " Where '1' = '1' and r_mitumori_m.cMITUMORI like '%" + cMitumori + "%' order by r_mitumori_m.nGYOUNO; ";
                    //#endregion
                    //MySqlCommand cmd = new MySqlCommand(sql, cn);
                    //MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    //DataTable dt_Syohin = new DataTable();
                    //da.Fill(dt_Syohin);
                    //da.Dispose();

                    //#region 詳細商品のSQL
                    //sql = "SELECT "
                    //+ " IfNull(rmm2.cMITUMORI, '') As cMITUMORI,"
                    //+ " case rmm2.nINSATSU_GYO"
                    //+ " when 0 then ''"
                    //+ " else cast(rmm2.nINSATSU_GYO as char)"
                    //+ " end As nINSATSU_GYO,"
                    //+ " IfNull(rmm2.cMITUMORI_KO, '') As cMITUMORI_KO,"
                    //+ " IfNull(rmm2.nGYOUNO, 0) As nGYOUNO,"
                    //+ " IfNull(rmm2.cSYOUHIN, '') As cSYOUHIN,"
                    //+ " IfNull(rmm2.sSYOUHIN_R, '') As sSYOUHIN_R,"
                    //+ " format( IfNull(rmm2.nTANKA, 0), 0)  As nTANKA,"
                    //+ " format(IfNull(rmm2.nSURYO, 0), 2) As nSURYO,"
                    //+ " IfNull(rmm2.cSHIIRESAKI, '') As cSHIIRESAKI,"
                    //+ " IfNull(rmm2.sSHIIRESAKI, '') As sSHIIRESAKI,"
                    //+ " format( IfNull(rmm2.nSIIRETANKA, 0), 0) As nSIIRETANKA,"
                    //+ " format( IfNull(rmm2.nSIIREKINGAKU, 0), 0) As nSIIREKINGAKU,"
                    //+ " IfNull(rmm2.nSIKIRITANKA, 0) As nSIKIRITANKA,"
                    //+ " format( IfNull(rmm2.nSIKIRIKINGAKU, 0), 0) As nSIKIRIKINGAKU,"
                    //+ " IfNull(rmm2.sTANI, '') As sTANI,"
                    //+ " IfNull(rmm2.nKINGAKU, 0) As nKINGAKU,"
                    //+ " IfNull(rmm2.nRITU, 100) As nRITU,"
                    //+ " IfNull(rmm2.cSYOUSAI, '') As cSYOUSAI,"
                    //+ " IfNull(rmm2.sSETSUMUI, '') As sSETSUMUI,"
                    //+ " IfNull(rmm2.fJITAIS, 0) As fJITAIS,"
                    //+ " IfNull(rmm2.fJITAIQ, 0) As fJITAIQ,"
                    //+ " rmm2.rowNO AS rowNO,"
                    //+ " rmm2.rowNO2 AS rowNO2,"
                    //+ " ifnull(rmm2.sMEMO, '') as sMEMO,"
                    //+ " ifnull(rmm2.fCHECK, '') as fCHECK,"
                    //+ " rmm.nGYOUNO as nGYOUNO1"
                    //+ " FROM r_mitumori_m2 rmm2"
                    //+ " LEFT JOIN r_mitumori_m rmm ON rmm2.cMITUMORI=rmm.cMITUMORI AND rmm2.rowNO=rmm.rowNO"
                    //+ " Where rmm2.cMitumori = '" + cMitumori + "' order by rmm2.rowNO asc, rmm2.nGYOUNO asc; ";
                    //#endregion
                    //cmd = new MySqlCommand(sql, cn);
                    //da = new MySqlDataAdapter(cmd);
                    //DataTable dt_Syosai = new DataTable();
                    //da.Fill(dt_Syosai);
                    //cn.Close();
                    //da.Dispose();
                    #endregion

                    DataTable dt_SyosaiSyohin = GetSyosaiGridViewData();
                    DataTable dt_MeisaiSyohin = CreateSyouhinTableColomn();

                    int maxRowNo = Convert.ToInt32(HF_maxRowNo.Value);//20220525 Maythu,MyatNoe add

                    #region グリードから取得
                    foreach (GridViewRow row in GV_MitumoriSyohin_Original.Rows)
                    {
                        //20220525 Maythu,MyatNoe add start
                        if (row.RowIndex == Convert.ToInt32(HF_syouhinRowIndex.Value))
                        {
                            #region 商品コピー
                            foreach (DataRow dr1 in dt_Syohin.Rows)
                            {
                                maxRowNo += 1;
                                DataRow dr = dt_MeisaiSyohin.NewRow();
                                dr[0] = "0";
                                dr[1] = dr1[1].ToString();
                                dr[2] = dr1[2].ToString();
                                Double nsuryo = Convert.ToDouble(dr1[3].ToString());
                                dr[3] = nsuryo.ToString("#,##0.##");
                                dr[4] = dr1[4].ToString();
                                dr[5] = dr1[5].ToString();
                                dr[6] = dr1[6].ToString();
                                dr[7] = dr1[7].ToString();
                                dr[8] = dr1[8].ToString();
                                dr[9] = dr1[9].ToString();
                                dr[10] = dr1[10].ToString();
                                dr[11] = dr1[11].ToString();
                                dr[12] = maxRowNo;
                                dr[13] = dr1[13].ToString();
                                dr[14] = dr1[14].ToString();
                                dr[15] = dr1[15].ToString();
                                dr[16] = dr1[16].ToString();
                                dr[17] = dr1[17].ToString();
                                dt_MeisaiSyohin.Rows.Add(dr);

                                if (dt_Syosai.Rows.Count > 0)
                                {
                                    DataRow[] rows = dt_Syosai.Select("rowNo = '" + dr1[12].ToString() + "'");
                                    if (rows.Length > 0)
                                    {
                                        foreach (var drow in rows)
                                        {
                                            DataRow dr_Syosai = dt_SyosaiSyohin.NewRow();
                                            dr_Syosai[0] = "0";
                                            dr_Syosai[1] = drow[1].ToString();
                                            dr_Syosai[2] = drow[2].ToString();
                                            Double nsuryo1 = Convert.ToDouble(drow[3].ToString());
                                            dr_Syosai[3] = nsuryo1.ToString("#,##0.##");
                                            dr_Syosai[4] = drow[4].ToString();
                                            dr_Syosai[5] = drow[5].ToString();
                                            dr_Syosai[6] = drow[6].ToString();
                                            dr_Syosai[7] = drow[7].ToString();
                                            dr_Syosai[8] = drow[8].ToString();
                                            Double tankaGokei = Convert.ToDouble(drow[6].ToString());
                                            Double genkaGokei = Convert.ToDouble(drow[8].ToString());
                                            Double arari = tankaGokei - genkaGokei;
                                            dr_Syosai[9] = arari;
                                            double nArariSu = (arari / tankaGokei) * 100;
                                            if (tankaGokei == 0)
                                            {
                                                nArariSu = 0;
                                            }
                                            dr_Syosai[10] = nArariSu.ToString("###0.0") + "%";
                                            dr_Syosai[11] = maxRowNo;
                                            dr_Syosai[12] = drow[12].ToString();
                                            dr_Syosai[13] = drow[13].ToString();
                                            dr_Syosai[14] = drow[14].ToString();
                                            dt_SyosaiSyohin.Rows.Add(dr_Syosai);
                                        }

                                        dt_SyosaiSyohin.DefaultView.Sort = "rowNo asc";
                                        dt_SyosaiSyohin.AcceptChanges();
                                    }
                                }
                            }
                            #endregion
                        }
                        //20220525 Maythu,MyatNoe add end

                        CheckBox chk = (GV_MitumoriSyohin_Original.Rows[row.RowIndex].FindControl("chkSelectSyouhin") as CheckBox);
                        Label lbl_status = (row.FindControl("lblhdnStatus") as Label);
                        Label lbl_fgenkataka = (row.FindControl("lblfgenkatanka") as Label);
                        Button btn_Syosai = (row.FindControl("btnSyohinShosai") as Button);
                        Label lbl_rowNo = (row.FindControl("lblRowNo") as Label);
                        TextBox txt_cSyohin = (row.FindControl("txtcSYOHIN") as TextBox);
                        TextBox txt_sSyohin = (row.FindControl("txtsSYOHIN") as TextBox);
                        TextBox txt_nSyoryo = (row.FindControl("txtnSURYO") as TextBox);
                        //DropDownList ddl_cTani = (row.FindControl("DDL_cTANI") as DropDownList);
                        TextBox txt_cTani = (row.FindControl("txtTani") as TextBox);
                        TextBox txt_nTanka = (row.FindControl("txtnTANKA") as TextBox);
                        Label lbl_TankaGokei = (row.FindControl("lblTankaGokei") as Label);
                        TextBox txt_nGenkaTanka = (row.FindControl("txtnGENKATANKA") as TextBox);
                        Label lbl_GenkaGokei = (row.FindControl("lblGenkaGokei") as Label);
                        Label lbl_Arari = (row.FindControl("lblnARARI") as Label);
                        Label lbl_ArariSu = (row.FindControl("lblnARARISu") as Label);
                        TextBox txt_nRITU = (row.FindControl("txtnRITU") as TextBox);
                        Label lbl_kubun = (row.FindControl("lblKubun") as Label);
                        Label lbl_nShikiriTanka = (row.FindControl("lblTanka") as Label);
                        Label lbl_fJITAIS = (row.FindControl("lblfjitais") as Label);
                        Label lbl_fJITAIQ = (row.FindControl("lblfjitaiq") as Label);

                        if (!String.IsNullOrEmpty(txt_cSyohin.Text) || !String.IsNullOrEmpty(txt_sSyohin.Text) ||
                                   !String.IsNullOrEmpty(txt_nSyoryo.Text) || !String.IsNullOrEmpty(txt_cTani.Text) ||
                                   !String.IsNullOrEmpty(txt_nTanka.Text) || !String.IsNullOrEmpty(lbl_TankaGokei.Text) ||
                                   !String.IsNullOrEmpty(txt_nGenkaTanka.Text) || !String.IsNullOrEmpty(lbl_GenkaGokei.Text) || txt_nRITU.Text != "100%")
                        {
                            DataRow dr = dt_MeisaiSyohin.NewRow();
                            dr[0] = "0";
                            dr[1] = txt_cSyohin.Text;
                            dr[2] = txt_sSyohin.Text;
                            dr[3] = txt_nSyoryo.Text;
                            dr[4] = txt_cTani.Text;
                            dr[5] = txt_nTanka.Text;
                            dr[6] = lbl_TankaGokei.Text;
                            dr[7] = txt_nGenkaTanka.Text;
                            dr[8] = lbl_GenkaGokei.Text;
                            dr[9] = lbl_Arari.Text;
                            dr[10] = lbl_ArariSu.Text;
                            dr[11] = lbl_fgenkataka.Text;
                            dr[12] = lbl_rowNo.Text;
                            dr[13] = txt_nRITU.Text;
                            dr[14] = lbl_kubun.Text;
                            dr[15] = lbl_nShikiriTanka.Text;
                            dr[16] = lbl_fJITAIS.Text;
                            dr[17] = lbl_fJITAIQ.Text;
                            dt_MeisaiSyohin.Rows.Add(dr);
                        }
                        else //20220525 Maythu,MyatNoe add for empty rows
                        {
                            DataRow dr = dt_MeisaiSyohin.NewRow();
                            dr[0] = "0";
                            dr[1] = "";
                            dr[2] = "";
                            dr[3] = "";
                            dr[4] = "";
                            dr[5] = "";
                            dr[6] = "";
                            dr[7] = "";
                            dr[8] = "";
                            dr[9] = "";
                            dr[10] = "";
                            dr[11] = "1";
                            dr[12] = "0";
                            dr[13] = "100%";
                            dr[14] = "";
                            dr[15] = "";
                            dr[16] = "0";
                            dr[17] = "0";
                            dt_MeisaiSyohin.Rows.Add(dr);
                        }
                    }
                    #endregion

                    //int maxRowNo = Convert.ToInt32(HF_maxRowNo.Value);//20220525 Maythu,MyatNoe delete

                    #region 商品コピー //20220525 Maythu,MyatNoe delete
                    //foreach (DataRow dr1 in dt_Syohin.Rows) 
                    //{
                    //    maxRowNo += 1;
                    //    DataRow dr = dt_MeisaiSyohin.NewRow();
                    //    dr[0] = "0";
                    //    dr[1] = dr1[1].ToString();
                    //    dr[2] = dr1[2].ToString();
                    //    Double nsuryo = Convert.ToDouble(dr1[3].ToString());
                    //    dr[3] = nsuryo.ToString("#,##0.##");
                    //    dr[4] = dr1[4].ToString();
                    //    dr[5] = dr1[5].ToString();
                    //    dr[6] = dr1[6].ToString();
                    //    dr[7] = dr1[7].ToString();
                    //    dr[8] = dr1[8].ToString();
                    //    dr[9] = dr1[9].ToString();
                    //    dr[10] = dr1[10].ToString();
                    //    dr[11] = dr1[11].ToString();
                    //    dr[12] = maxRowNo;
                    //    dr[13] = dr1[13].ToString();
                    //    dr[14] = dr1[14].ToString();
                    //    dr[15] = dr1[15].ToString();
                    //    dr[16] = dr1[16].ToString();
                    //    dr[17] = dr1[17].ToString();
                    //    dt_MeisaiSyohin.Rows.Add(dr);

                    //    if (dt_Syosai.Rows.Count > 0)
                    //    {
                    //        DataRow[] rows = dt_Syosai.Select("rowNo = '" + dr1[12].ToString() + "'");
                    //        if (rows.Length > 0)
                    //        {
                    //            foreach (var drow in rows)
                    //            {
                    //                DataRow dr_Syosai = dt_SyosaiSyohin.NewRow();
                    //                dr_Syosai[0] = "0";
                    //                dr_Syosai[1] = drow[1].ToString();
                    //                dr_Syosai[2] = drow[2].ToString();
                    //                Double nsuryo1 = Convert.ToDouble(drow[3].ToString());
                    //                dr_Syosai[3] = nsuryo1.ToString("#,##0.##");
                    //                dr_Syosai[4] = drow[4].ToString();
                    //                dr_Syosai[5] = drow[5].ToString();
                    //                dr_Syosai[6] = drow[6].ToString();
                    //                dr_Syosai[7] = drow[7].ToString();
                    //                dr_Syosai[8] = drow[8].ToString();
                    //                Double tankaGokei = Convert.ToDouble(drow[6].ToString());
                    //                Double genkaGokei = Convert.ToDouble(drow[8].ToString());
                    //                Double arari = tankaGokei - genkaGokei;
                    //                dr_Syosai[9] = arari;
                    //                double nArariSu = (arari / tankaGokei) * 100;
                    //                if (tankaGokei == 0)
                    //                {
                    //                    nArariSu = 0;
                    //                }
                    //                dr_Syosai[10] = nArariSu.ToString("###0.0") + "%";
                    //                dr_Syosai[11] = maxRowNo;
                    //                dr_Syosai[12] = drow[12].ToString();
                    //                dr_Syosai[13] = drow[13].ToString();
                    //                dr_Syosai[14] = drow[14].ToString();
                    //                dt_SyosaiSyohin.Rows.Add(dr_Syosai);
                    //            }

                    //            dt_SyosaiSyohin.DefaultView.Sort = "rowNo asc";
                    //            dt_SyosaiSyohin.AcceptChanges();
                    //        }
                    //    }
                    //}
                    #endregion

                    while (dt_MeisaiSyohin.Rows.Count < GV_MitumoriSyohin_Original.Rows.Count)
                    {
                        DataRow dr = dt_MeisaiSyohin.NewRow();
                        dr[0] = "0";
                        dr[1] = "";
                        dr[2] = "";
                        dr[3] = "";
                        dr[4] = "";
                        dr[5] = "";
                        dr[6] = "";
                        dr[7] = "";
                        dr[8] = "";
                        dr[9] = "";
                        dr[10] = "";
                        dr[11] = "1";
                        dr[12] = "0";
                        dr[13] = "100%";
                        dr[14] = "";
                        dr[15] = "";
                        dr[16] = "0";
                        dr[17] = "0";
                        dt_MeisaiSyohin.Rows.Add(dr);
                    }

                    HF_maxRowNo.Value = maxRowNo.ToString();
                    //HF_maxRowNo.Value = (maxRowNo + Convert.ToInt32(HF_maxRowNo.Value)).ToString();//20220525 Maythu,MyatNoe add
                    HF_syouhinRowIndex.Value = "0"; //20220525 Maythu,MyatNoe add
                    dt_MeisaiSyohin = SetMidashiSyokei(dt_MeisaiSyohin);
                    ViewState["SyouhinTable"] = dt_MeisaiSyohin;
                    GV_MitumoriSyohin_Original.DataSource = dt_MeisaiSyohin;
                    GV_MitumoriSyohin_Original.DataBind();
                    GV_MitumoriSyohin.DataSource = dt_MeisaiSyohin;
                    GV_MitumoriSyohin.DataBind();
                    GV_Syosai.DataSource = dt_SyosaiSyohin;
                    GV_Syosai.DataBind();
                    updMitsumoriSyohinGrid.Update();
                    setSyosaiCount(dt_MeisaiSyohin, dt_SyosaiSyohin);
                    GetTotalKingaku();
                    GV_MitumoriSyohin.DataSource = dt_MeisaiSyohin;
                    GV_MitumoriSyohin.DataBind();
                    //SetSyosai();
                    HasCheckRow();
                    updMitsumoriSyohinGrid.Update();
                    HF_isChange.Value = "1";
                    Session["cMitumori"] = lblcMitumori.Text;
                    Session["btko"] = null;
                    updHeader.Update();
                }
                else if (HF_fBtn.Value == "btnTaMitumoriCopy")
                {
                    String cbukken = lnkcBukken.Text;
                    lblcMitumori.Text = cMitumori;
                    getMitumoriData();
                    getSyouhinData();
                    getSyosaiSyouhinData();
                    LoadImage();
                    HasCheckRow();

                    if (Session["btko"] != null)
                    {
                        lnkcBukken.Text = cbukken;
                        getSeikyuTantouByBukken();

                        lblcMitumori.Text = "";
                        btnBetsuMitumoriSave.Visible = false;
                        btnMitumoriDelete.Visible = false;
                        btnMitumorishoPDF.Enabled = false;
                        btnMitumorishoPDF.CssClass = "JC10SaveBtnDisable";
                        btnCreateUriage.Enabled = false;
                        btnCreateUriage.CssClass = "JC10SaveBtnDisable";
                        btnUriage.Enabled = false;
                        btnUriage.CssClass = "JC10SaveBtnDisable";
                        Session["btko"] = null;
                        lblSakusekibi.Text = "";
                        lblSakuseisya.Text = "";
                        lblcSakuseisya.Text = "";
                        lblHenkoubi.Text = "";
                        lblHenkousya.Text = "";
                        lblcHenkousya.Text = "";
                        lblcMitumori_Ko.Text = "01";
                    }

                    HF_isChange.Value = "1";
                    SetSyosai();
                    updHeader.Update();
                }
            }
        }
        #endregion

        #region btn_CloseTokuisakiSentaku_Click
        protected void btn_CloseTokuisakiSentaku_Click(object sender, EventArgs e)
        {
            ifShinkiPopup.Src = "";
            mpeShinkiPopup.Hide();
            updShinkiPopup.Update();
        }
        #endregion

        #region btnPDFPageChoiceClose_Click
        protected void btnPDFPageChoiceClose_Click(object sender, EventArgs e)
        {
            if (Session["pageNumber"] != null)
            {
                int pageNumber = Convert.ToInt16(Session["pageNumber"].ToString());
                string base64String = HF_ImgBase64.Value;
                base64String = base64String.Replace("data:application/pdf;base64,", String.Empty);
                base64String = base64String.Replace("data:application/postscript;base64,", string.Empty);
                try
                {
                    byte[] pdfBytes = Convert.FromBase64String(base64String);

                    using (var inputMS = new MemoryStream(pdfBytes))
                    {
                        GhostscriptRasterizer rasterizer = null;

                        using (rasterizer = new GhostscriptRasterizer())
                        {
                            rasterizer.Open(inputMS);

                            using (MemoryStream ms = new MemoryStream())
                            {
                                int pagecount = rasterizer.PageCount;

                                System.Drawing.Image img = rasterizer.GetPage(200, pageNumber);
                                img.Save(ms, ImageFormat.Png);

                                var SigBase64 = Convert.ToBase64String(ms.GetBuffer()); //Get Base64
                                String imgurl = "data:image/png;base64," + SigBase64;
                                fImageUpload = HF_fImageUpload.Value;
                                if (fImageUpload == "0")
                                {
                                    HyoshiuploadedImage.Src = imgurl;
                                    HyoshiuploadedImage.Attributes.Add("class", "JC10DaiHyouImage");
                                    BT_HyoshiImgaeDelete.CssClass = "JC10ImageDelete";
                                    HyoshidragZone.Attributes.Add("class", "DisplayNone");
                                }
                                else if (fImageUpload == "1")
                                {
                                    Mitumorisho1uploadedImage.Src = imgurl;
                                    Mitumorisho1uploadedImage.Attributes.Add("class", "JC10DaiHyouImage");
                                    BT_Mitumorisho1ImgaeDelete.CssClass = "JC10ImageDelete";
                                    Mitumorisho1dragZone.Attributes.Add("class", "DisplayNone");
                                }
                                else if (fImageUpload == "2")
                                {
                                    Mitumorisho2uploadedImage.Src = imgurl;
                                    Mitumorisho2uploadedImage.Attributes.Add("class", "JC10DaiHyouImage");
                                    BT_Mitumorisho2ImgaeDelete.CssClass = "JC10ImageDelete";
                                    Mitumorisho2dragZone.Attributes.Add("class", "DisplayNone");
                                }

                            }


                            rasterizer.Close();
                        }
                    }
                }
                catch (Exception ex) { }
            }
            HF_isChange.Value = "1";
            ifSentakuPopup.Src = "";
            mpeSentakuPopup.Hide();
            updSentakuPopup.Update();
            updHeader.Update();
        }
        #endregion

        #region btnMitumorishoPDF_Click
        protected void btnMitumorishoPDF_Click(object sender, EventArgs e)
        {
            HF_fBtn.Value = "btnMitumorishoPDF";
            if (HF_isChange.Value == "1")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowAnkenChangeMessage",
                    "ShowKoumokuChangesConfirmMessage('項目が変更されています。保存しますか？','" + btnYes1.ClientID + "','" + btnNo1.ClientID + "','" + btnCancel.ClientID + "');", true);
            }
            else
            {
                btnNo1_Click(sender, e);
            }

        }
        #endregion

        #region btnZeikomi_Click
        protected void btnZeikomi_Click(object sender, EventArgs e)
        {
            btnZeikomi.CssClass = "JC10ZeikomiBtnActive";
            btnZeinuKingaku1.CssClass = "JC10ZeikomiBtn";
            btnZeinuKingaku2.CssClass = "JC10ZeikomiBtn";
            updHeader.Update();
        }
        #endregion

        #region btnZeinuKingaku1_Click
        protected void btnZeinuKingaku1_Click(object sender, EventArgs e)
        {
            btnZeikomi.CssClass = "JC10ZeikomiBtn";
            btnZeinuKingaku1.CssClass = "JC10ZeikomiBtnActive";
            btnZeinuKingaku2.CssClass = "JC10ZeikomiBtn";
            updHeader.Update();
        }
        #endregion

        #region btnZeinuKingaku2_Click
        protected void btnZeinuKingaku2_Click(object sender, EventArgs e)
        {
            btnZeikomi.CssClass = "JC10ZeikomiBtn";
            btnZeinuKingaku1.CssClass = "JC10ZeikomiBtn";
            btnZeinuKingaku2.CssClass = "JC10ZeikomiBtnActive";
            updHeader.Update();
        }
        #endregion

        #region setrogo
        private void setrogo()　　//ロゴ名
        {
            String id = Request.QueryString["id"];
            DataTable dt_db = JC01Login_Class.Get_DB(id);
            String db = dt_db.Rows[0]["db"].ToString();
            String mail = dt_db.Rows[0]["mail"].ToString();
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

            String cKyoten = "01";
            if(!String.IsNullOrEmpty(lblcKYOTEN.Text) && !String.IsNullOrEmpty(lblsKYOTEN.Text))
            {
                cKyoten = lblcKYOTEN.Text;
            }
            string sql = "";
            sql = "select ifnull(sIMAGETitle1,'') sIMAGETitle1,ifnull(sIMAGETitle2,'') sIMAGETitle2,ifnull(sIMAGETitle3,'') sIMAGETitle3,ifnull(sIMAGETitle4,'') sIMAGETitle4,ifnull(sIMAGETitle5,'') sIMAGETitle5";
            sql += " from m_j_info where cCO ='"+cKyoten+"'";
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
        
        #region SetGokeiKingakuHyoji
        private void SetGokeiKingakuHyoji()　　//合計金額非表示
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
            string sql = "";
            sql = "SELECT fSYUUKEI FROM r_mitumori WHERE cMITUMORI='"+lblcMitumori.Text+"';";
            MySqlCommand cmd = new MySqlCommand(sql, cn);
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            cn.Close();
            da.Dispose();
            CHK_KingakuNotDisplay.Checked = false;
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["fSYUUKEI"].ToString() == "1")
                {
                    CHK_KingakuNotDisplay.Checked = true;
                }
            }
        }
        #endregion

        #region btnInsatsuSave_Click
        protected void btnInsatsuSave_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(lblcMitumori.Text))
            {
                String id = Request.QueryString["id"];
                DataTable dt_db = JC01Login_Class.Get_DB(id);
                String db = dt_db.Rows[0]["db"].ToString();
                String mail = dt_db.Rows[0]["mail"].ToString();
                ConstantVal.DB_NAME = db;

                JC_ClientConnecction_Class jc = new JC_ClientConnecction_Class();
                jc.loginId = mail;
                con = jc.GetConnection();
                String fSyuukei = "0";
                if (CHK_KingakuNotDisplay.Checked)
                {
                    fSyuukei = "1";
                }
                String qr_SyuukeiUpdate = "UPDATE r_mitumori SET fSYUUKEI='"+fSyuukei+"' WHERE cMITUMORI='"+lblcMitumori.Text+"';";

                String logoindex = DDL_Logo.SelectedIndex.ToString();
                String fZeikomi = "0";
                if (btnZeikomi.CssClass == "JC10ZeikomiBtnActive")
                {
                    fZeikomi = "0";
                }
                else if (btnZeinuKingaku1.CssClass == "JC10ZeikomiBtnActive")
                {
                    fZeikomi = "1";
                }
                else if (btnZeinuKingaku2.CssClass == "JC10ZeikomiBtnActive")
                {
                    fZeikomi = "2";
                }

                String fMidashi = "0";
                String fMeisai = "0";
                String fSyosai = "0";
                if (CHK_Midashi.Checked)
                {
                    fMidashi = "1";
                }

                if (CHK_Meisai.Checked)
                {
                    fMeisai = "1";
                }

                if (CHK_Shosai.Checked)
                {
                    fSyosai = "1";
                }

                String qr_jisyaInfoUpdate = "UPDATE m_j_tantousha SET fZei='"+fZeikomi+"'," +
                    " fLogo='"+logoindex+"', fMidashi='"+fMidashi+"', fMeisai='"+fMeisai+"', fSyosai='"+fSyosai+"'" +
                    " WHERE cTANTOUSHA='"+lblLoginUserCode.Text+"';";

                MySqlTransaction tr = null;
                MySqlCommand cmdUpdate = new MySqlCommand();
                con.Open();
                try
                {
                    tr = con.BeginTransaction();
                    cmdUpdate.Transaction = tr;
                    cmdUpdate.CommandTimeout = 0;
                    cmdUpdate = new MySqlCommand(qr_SyuukeiUpdate+qr_jisyaInfoUpdate, con);
                    cmdUpdate.ExecuteNonQuery();
                    tr.Commit();

                    divLabelSave.Style["display"] = "flex";//「保存しました。」メッセージを表示                                                                                                                                      
                    updLabelSave.Update();

                    btnBetsuMitumoriSave.Visible = true;
                    btnMitumoriDelete.Visible = true;
                    btnMitumorishoPDF.Enabled = true;
                    btnMitumorishoPDF.CssClass = "BlueBackgroundButton JC10SaveBtn";
                    btnCreateUriage.Enabled = true;
                    btnCreateUriage.CssClass = "BlueBackgroundButton JC10SaveBtn";
                    btnUriage.Enabled = true;
                    btnUriage.CssClass = "BlueBackgroundButton JC10SaveBtn";

                    lblSakuseisya.Text = lblLoginUserName.Text;
                    lblHenkousya.Text = lblLoginUserName.Text;
                    lblcSakuseisya.Text = lblLoginUserCode.Text;
                    lblcHenkousya.Text = lblLoginUserCode.Text;
                    lblSakusekibi.Text = DateTime.Now.ToString("yyyy/MM/dd");
                    lblHenkoubi.Text = DateTime.Now.ToString("yyyy/MM/dd");
                    saveSuccess = true;
                    HF_isChange.Value = "0";
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
            }

            GetTotalKingaku();
            if (CHK_KingakuNotDisplay.Checked)
            {
                txtShusseiNebiki.Text = "0";
            }
            updHeader.Update();
        }
        #endregion

        #region PrintPDF
        private void PrintPDF(object sender,EventArgs e)
        {
            #region 見積書印刷設定
            setrogo();//ロゴ名
            SetGokeiKingakuHyoji();

            String id = Request.QueryString["id"];
            DataTable dt_db = JC01Login_Class.Get_DB(id);
            String db = dt_db.Rows[0]["db"].ToString();
            String mail = dt_db.Rows[0]["mail"].ToString();
            ConstantVal.DB_NAME = db;

            JC_ClientConnecction_Class jc = new JC_ClientConnecction_Class();
            jc.loginId = mail;
            DataTable dt_InsatsuSetting = jc.ExecuteInsatsuSetting(lblLoginUserCode.Text);
            if (dt_InsatsuSetting.Rows.Count > 0)
            {
                try
                {
                    String logo = dt_InsatsuSetting.Rows[0]["fLogo"].ToString();
                    DDL_Logo.SelectedIndex = Convert.ToInt32(logo);
                }
                catch { }

                try
                {
                    String fZeikomi = dt_InsatsuSetting.Rows[0]["fZei"].ToString();
                    if (fZeikomi == "0")
                    {
                        btnZeikomi_Click(sender, e);
                    }
                    else if (fZeikomi == "1")
                    {
                        btnZeinuKingaku1_Click(sender, e);
                    }
                    else if (fZeikomi == "2")
                    {
                        btnZeinuKingaku2_Click(sender, e);
                    }
                }
                catch { }

                String fMidashi = dt_InsatsuSetting.Rows[0]["fMidashi"].ToString();
                if (fMidashi == "1")
                {
                    CHK_Midashi.Checked = true;
                }
                else
                {
                    CHK_Midashi.Checked = false;
                }

                String fMeisai = dt_InsatsuSetting.Rows[0]["fMeisai"].ToString();
                if (fMeisai == "1")
                {
                    CHK_Meisai.Checked = true;
                }
                else
                {
                    CHK_Meisai.Checked = false;
                }

                String fSyosai = dt_InsatsuSetting.Rows[0]["fSyosai"].ToString();
                if (fSyosai == "1")
                {
                    CHK_Shosai.Checked = true;
                }
                else
                {
                    CHK_Shosai.Checked = false;
                }
            }
            updHeader.Update();
            #endregion

            #region ロゴタイトル
            string flagrogo = "";
            string sqlkyoten = "";
            String cKyoten = "01";
            if (!String.IsNullOrEmpty(lblcKYOTEN.Text) && !String.IsNullOrEmpty(lblsKYOTEN.Text))
            {
                cKyoten = lblcKYOTEN.Text;
            }
            sqlkyoten = "select ifnull(sIMAGETitle1,'') sIMAGETitle1,ifnull(sIMAGETitle2,'') sIMAGETitle2,ifnull(sIMAGETitle3,'') sIMAGETitle3,ifnull(sIMAGETitle4,'') sIMAGETitle4,ifnull(sIMAGETitle5,'') sIMAGETitle5";
            sqlkyoten += ",ifnull(sBIKOUTitle1,'') sBIKOUTitle1,ifnull(sBIKOUTitle2,'') sBIKOUTitle2,ifnull(sBIKOUTitle3,'') sBIKOUTitle3,ifnull(sBIKOUTitle4,'') sBIKOUTitle4,ifnull(sBIKOUTitle5,'') sBIKOUTitle5";
            sqlkyoten += " from m_j_info where cCO ='"+cKyoten+"'";
            //JC_ClientConnecction_Class jc = new JC_ClientConnecction_Class();
            ConstantVal.DB_NAME = db;
            jc.loginId = mail;
            con = jc.GetConnection();
            con.Open();

            MySqlCommand cmd = new MySqlCommand(sqlkyoten, con);
            cmd.CommandTimeout = 0;
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            da.Fill(dt);
            da.Dispose();
            con.Close();
            if (dt.Rows.Count > 0)
            {
                if (DDL_Logo.SelectedIndex > -1)
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
                else
                {
                    flagrogo = "0";
                }
            }
            #endregion

            ConstantVal.DB_NAME = db;
            DateTime datenow = jc.GetCurrentDate();
            String fileName = txtsMitumori.Text+"_"+datenow.ToString("yyyyMMdd");

            if (CHK_Shosai.Checked == true)
            {
                fSYOUSAI = true;
            }
            else
            {
                fSYOUSAI = false;
            }
            if (CHK_Meisai.Checked == true)
            {
                fMEISAI = true;
            }
            else
            {
                fMEISAI = false;
            }
            if (CHK_Midashi.Checked == true)
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
                rpt.cMITUMORI = lblcMitumori.Text;
                rpt.cBUKKEN = lnkcBukken.Text;
                rpt.cTANTOUSHA = lblcJISHATANTOUSHA.Text;
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
                rpt.cKyoten = lblcKYOTEN.Text;
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
                if (btnZeinuKingaku1.CssClass == "JC10ZeikomiBtnActive")
                {
                    rpt.fZEINUKIKINNGAKU = true;
                }
                else if (btnZeinuKingaku2.CssClass == "JC10ZeikomiBtnActive")
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
                Session["UriagePDF"] = "false"; //20220208 added by YG
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
                rpt.cMITUMORI = lblcMitumori.Text;
                rpt.cBUKKEN = lnkcBukken.Text;
                rpt.cTANTOUSHA = lblcJISHATANTOUSHA.Text;
                rpt.loginId = mail;
                rpt.fSYOUSAI = false;
                rpt.fICHIRAN = true;
                rpt.fRYOUHOU = fSYOUSAI;
                rpt.flag_page1 = true;
                rpt.frogoimage = flagrogo;//ロゴ combobox
                                          //选择打印封页：1，不打印封页：0，由前一个页面传值过来
                rpt.cKyoten = lblcKYOTEN.Text;
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
                if (btnZeinuKingaku1.CssClass == "JC10ZeikomiBtnActive")
                {
                    rpt.fZEINUKIKINNGAKU = true;
                }
                else if (btnZeinuKingaku2.CssClass == "JC10ZeikomiBtnActive")
                {
                    rpt.fZEINUKIKINNGAKU = false;
                }
                else
                {
                    rpt.fZEIFUKUMUKIKINNGAKU = true;
                }
                rpt.Run();

                mitsumori rpt_1 = new mitsumori();
                rpt_1.cMITUMORI = lblcMitumori.Text;
                rpt_1.cBUKKEN = lnkcBukken.Text;
                rpt_1.cTANTOUSHA = lblcJISHATANTOUSHA.Text;
                rpt_1.loginId = mail;
                rpt_1.fSYOUSAI = fSYOUSAI;
                rpt_1.fICHIRAN = false;
                rpt_1.fRYOUHOU = fSYOUSAI;
                pcount = rpt.pcount;
                rpt_1.pcount = pcount;
                rpt_1.flag_page1 = true;
                rpt_1.frogoimage = flagrogo;//ロゴ combobox
                                            //选择打印封页：1，不打印封页：0，由前一个页面传值过来
                rpt_1.cKyoten = lblcKYOTEN.Text;
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
                prt4.cMITUMORI = lblcMitumori.Text;
                prt4.cBUKKEN = lnkcBukken.Text;
                prt4.cTANTOUSHA = lblcJISHATANTOUSHA.Text;
                prt4.loginId = mail;
                prt4.fSYOUSAI = false;//詳細チェック
                prt4.fICHIRAN = true;//一覧アンチェック
                prt4.fRYOUHOU = fSYOUSAI;
                prt4.flag_page1 = false;
                prt4.frogoimage = flagrogo;
                prt4.cKyoten = lblcKYOTEN.Text;
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
                prt5.cMITUMORI = lblcMitumori.Text;
                prt5.cBUKKEN = lnkcBukken.Text;
                prt5.cTANTOUSHA = lblcJISHATANTOUSHA.Text;
                prt5.loginId = mail;
                prt5.fSYOUSAI = fSYOUSAI;//詳細チェック出来ると印刷できる
                prt5.fICHIRAN = false;//一覧アンチェック
                prt5.fRYOUHOU = fSYOUSAI;
                prt5.frogoimage = flagrogo;
                prt5.cKyoten = lblcKYOTEN.Text;
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
                Session["UriagePDF"] = "false"; //20220208 added by YG
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
                    rpt.cMITUMORI = lblcMitumori.Text;
                    rpt.cBUKKEN = lnkcBukken.Text;
                    rpt.cTANTOUSHA = lblcJISHATANTOUSHA.Text;
                    rpt.loginId = mail;
                    rpt.fSYOUSAI = false;//詳細チェック
                    rpt.fICHIRAN = true;//一覧チェック
                    rpt.fRYOUHOU = false;
                    rpt.flag_page1 = true;
                    rpt.frogoimage = flagrogo;//ロゴ combobox
                    rpt.cKyoten = lblcKYOTEN.Text;
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
                    if (btnZeinuKingaku1.CssClass == "JC10ZeikomiBtnActive")
                    {
                        rpt.fZEINUKIKINNGAKU = true;
                    }
                    else if (btnZeinuKingaku2.CssClass == "JC10ZeikomiBtnActive")
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
                    Session["UriagePDF"] = "false"; //20220208 added by YG
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
                        rpt_midashi.cMITUMORI = lblcMitumori.Text;
                        rpt_midashi.cBUKKEN = lnkcBukken.Text;
                        rpt_midashi.cTANTOUSHA = lblcJISHATANTOUSHA.Text;
                        rpt_midashi.loginId = mail;
                        rpt_midashi.fSYOUSAI = false;//詳細チェック
                        rpt_midashi.fICHIRAN = false;//一覧アンチェック
                        rpt_midashi.fRYOUHOU = true;
                        rpt_midashi.fMIDASHI = fMIDASHI;//見出しチェック
                        rpt_midashi.flag_page1 = true;
                        rpt_midashi.frogoimage = flagrogo;//ロゴ combobox
                        rpt_midashi.cKyoten = lblcKYOTEN.Text;
                        if (fMIDASHI == true)
                        {
                            rpt_midashi.Run();
                        }
                        #endregion

                        #region 明細
                        // rmt_pcount1 = 0;
                        mitsumori rpt_mesai = new mitsumori();
                        rpt_mesai.cMITUMORI = lblcMitumori.Text;
                        rpt_mesai.cBUKKEN = lnkcBukken.Text;
                        rpt_mesai.cTANTOUSHA = lblcJISHATANTOUSHA.Text;
                        rpt_mesai.loginId = mail;
                        rpt_mesai.fSYOUSAI = false;//詳細チェック
                        rpt_mesai.fICHIRAN = true;//一覧アンチェック
                        rpt_mesai.fRYOUHOU = true;
                        rpt_mesai.frogoimage = flagrogo;//ロゴ combobox
                        rpt_mesai.cKyoten = lblcKYOTEN.Text;
                        rpt_mesai.flag_page1 = true;
                        // rpt_mesai.rmt_pcount1 = rpt_midashi.nPAGECOUNT;
                        pcount = rpt_midashi.pcount;
                        rpt_mesai.pcount = pcount;
                        rpt_mesai.Run();

                        #endregion

                        #region 詳細
                        mitsumori rpt_syousai = new mitsumori();
                        rpt_syousai.cMITUMORI = lblcMitumori.Text;
                        rpt_syousai.cBUKKEN = lnkcBukken.Text;
                        rpt_syousai.cTANTOUSHA = lblcJISHATANTOUSHA.Text;
                        rpt_syousai.loginId = mail;
                        rpt_syousai.fSYOUSAI = fSYOUSAI;//詳細チェック出来ると印刷できる
                        rpt_syousai.fICHIRAN = false;//一覧アンチェック
                        rpt_syousai.fRYOUHOU = fSYOUSAI;
                        rpt_syousai.flag_page1 = true;
                        rpt_syousai.frogoimage = flagrogo;//ロゴ combobox
                                                          //rpt_syousai.rmt_pcount1 = rpt_mesai.nPAGECOUNT;
                        rpt_syousai.cKyoten = lblcKYOTEN.Text;
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
                        rpt_midashi1.cMITUMORI = lblcMitumori.Text;
                        rpt_midashi1.cBUKKEN = lnkcBukken.Text;
                        rpt_midashi1.cTANTOUSHA = lblcJISHATANTOUSHA.Text;
                        rpt_midashi1.loginId = mail;
                        rpt_midashi1.fSYOUSAI = false;//詳細チェック
                        rpt_midashi1.fICHIRAN = false;//一覧アンチェック
                        rpt_midashi1.fRYOUHOU = fSYOUSAI;
                        rpt_midashi1.fMIDASHI = fMIDASHI;//見出しチェック
                        rpt_midashi1.flag_page1 = false;
                        rpt_midashi1.frogoimage = flagrogo;//ロゴ combobox
                        rpt_midashi1.cKyoten = lblcKYOTEN.Text;
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
                        rpt4.cMITUMORI = lblcMitumori.Text;
                        rpt4.cBUKKEN = lnkcBukken.Text;
                        rpt4.cTANTOUSHA = lblcJISHATANTOUSHA.Text;
                        rpt4.loginId = mail;
                        rpt4.fSYOUSAI = false;//詳細チェック
                        rpt4.fICHIRAN = true;//一覧アンチェック
                        rpt4.fRYOUHOU = fSYOUSAI;
                        rpt4.frogoimage = flagrogo;//ロゴ combobox
                                                   //  rpt4.rmt_pcount1 = rmt_pcount1;
                        rpt4.cKyoten = lblcKYOTEN.Text;
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
                        rpt5.cMITUMORI = lblcMitumori.Text;
                        rpt5.cBUKKEN = lnkcBukken.Text;
                        rpt5.cTANTOUSHA = lblcJISHATANTOUSHA.Text;
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
                        rpt5.cKyoten = lblcKYOTEN.Text;
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
                            Session["UriagePDF"] = "false"; //20220208 added by YG
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
                            Session["UriagePDF"] = "false"; //20220208 added by YG
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

                        #endregion

                        #endregion
                    }
                    else if (fMIDASHI == true && (fSYOUSAI == true && fMEISAI == false)) //見出し、明細かつ詳細
                    {
                        #region pagenumber
                        rmt_pcount1 = 0;
                        #region 見出し
                        mitsumori prt4_midashi = new mitsumori(); // 纵向数据表 type1
                        prt4_midashi.cMITUMORI = lblcMitumori.Text;
                        prt4_midashi.cBUKKEN = lnkcBukken.Text;
                        prt4_midashi.cTANTOUSHA = lblcJISHATANTOUSHA.Text;
                        prt4_midashi.loginId = mail;
                        prt4_midashi.fSYOUSAI = false;//詳細チェック
                        prt4_midashi.fICHIRAN = false;//一覧アンチェック
                        prt4_midashi.fRYOUHOU = false;
                        prt4_midashi.fMIDASHI = fMIDASHI;//見出しチェック
                        prt4_midashi.flag_page1 = true;
                        prt4_midashi.frogoimage = flagrogo;//ロゴ combobox
                                                           //选择打印封页：1，不打印封页：0，由前一个页面传值过来
                        prt4_midashi.cKyoten = lblcKYOTEN.Text;
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
                        prt5_p1.cMITUMORI = lblcMitumori.Text;
                        prt5_p1.cBUKKEN = lnkcBukken.Text;
                        prt5_p1.cTANTOUSHA = lblcJISHATANTOUSHA.Text;
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
                        prt5_p1.cKyoten = lblcKYOTEN.Text;
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
                        Session["UriagePDF"] = "false"; //20220208 added by YG
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

                        #endregion

                        #region 表示

                        #region 見出し
                        mitsumori prt4_midashi1 = new mitsumori(); // 纵向数据表 type1
                        prt4_midashi1.cMITUMORI = lblcMitumori.Text;
                        prt4_midashi1.cBUKKEN = lnkcBukken.Text;
                        prt4_midashi1.cTANTOUSHA = lblcJISHATANTOUSHA.Text;
                        prt4_midashi1.loginId = mail;
                        prt4_midashi1.fSYOUSAI = false;//詳細チェック
                        prt4_midashi1.fICHIRAN = false;//一覧アンチェック
                        prt4_midashi1.fRYOUHOU = false;
                        prt4_midashi1.fMIDASHI = fMIDASHI;//見出しチェック
                        prt4_midashi1.flag_page1 = false;
                        prt4_midashi1.frogoimage = flagrogo;//ロゴ combobox
                                                            //选择打印封页：1，不打印封页：0，由前一个页面传值过来
                        prt4_midashi1.cKyoten = lblcKYOTEN.Text;
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
                        prt5.cMITUMORI = lblcMitumori.Text;
                        prt5.cBUKKEN = lnkcBukken.Text;
                        prt5.cTANTOUSHA = lblcJISHATANTOUSHA.Text;
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
                        prt5.cKyoten = lblcKYOTEN.Text;
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

                        #endregion
                    }
                    else if (fMIDASHI == true && (fSYOUSAI == false && fMEISAI == true)) //見出し、明細かつ詳細
                    {
                        rmt_pcount1 = 0;
                        #region pagenumber
                        #region 見出し
                        mitsumori prt4_midashi = new mitsumori(); // 纵向数据表 type1
                        prt4_midashi.cMITUMORI = lblcMitumori.Text;
                        prt4_midashi.cBUKKEN = lnkcBukken.Text;
                        prt4_midashi.cTANTOUSHA = lblcJISHATANTOUSHA.Text;
                        prt4_midashi.loginId = mail;
                        prt4_midashi.fSYOUSAI = false;//詳細チェック
                        prt4_midashi.fICHIRAN = false;//一覧アンチェック
                        prt4_midashi.fRYOUHOU = false;
                        prt4_midashi.fMIDASHI = fMIDASHI;//見出しチェック
                        prt4_midashi.flag_page1 = true;
                        prt4_midashi.frogoimage = flagrogo;//ロゴ combobox
                                                           //选择打印封页：1，不打印封页：0，由前一个页面传值过来
                        prt4_midashi.cKyoten = lblcKYOTEN.Text;
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
                        prt4_p1.cMITUMORI = lblcMitumori.Text;
                        prt4_p1.cBUKKEN = lnkcBukken.Text;
                        prt4_p1.cTANTOUSHA = lblcJISHATANTOUSHA.Text;
                        prt4_p1.loginId = mail;
                        prt4_p1.fSYOUSAI = false;//詳細チェック
                        prt4_p1.fICHIRAN = true;//一覧アンチェック
                        prt4_p1.fRYOUHOU = false;
                        prt4_p1.frogoimage = flagrogo;//ロゴ combobox
                                                      //选择打印封页：1，不打印封页：0，由前一个页面传值过来
                        prt4_p1.cKyoten = lblcKYOTEN.Text;
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
                        Session["UriagePDF"] = "false"; //20220208 added by YG
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

                        #endregion

                        #region 表示

                        #region 見出し
                        mitsumori prt4_midashi1 = new mitsumori(); // 纵向数据表 type1
                        prt4_midashi1.cMITUMORI = lblcMitumori.Text;
                        prt4_midashi1.cBUKKEN = lnkcBukken.Text;
                        prt4_midashi1.cTANTOUSHA = lblcJISHATANTOUSHA.Text;
                        prt4_midashi1.loginId = mail;
                        prt4_midashi1.fSYOUSAI = false;//詳細チェック
                        prt4_midashi1.fICHIRAN = false;//一覧アンチェック
                        prt4_midashi1.fRYOUHOU = false;
                        prt4_midashi1.fMIDASHI = fMIDASHI;//見出しチェック
                        prt4_midashi1.flag_page1 = false;
                        prt4_midashi1.frogoimage = flagrogo;//ロゴ combobox
                                                            //选择打印封页：1，不打印封页：0，由前一个页面传值过来
                        prt4_midashi1.cKyoten = lblcKYOTEN.Text;
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
                        prt4.cMITUMORI = lblcMitumori.Text;
                        prt4.cBUKKEN = lnkcBukken.Text;
                        prt4.cTANTOUSHA = lblcJISHATANTOUSHA.Text;
                        prt4.loginId = mail;
                        prt4.fSYOUSAI = false;//詳細チェック
                        prt4.fICHIRAN = true;//一覧アンチェック
                        prt4.fRYOUHOU = false;
                        prt4.frogoimage = flagrogo;//ロゴ combobox
                                                   //选择打印封页：1，不打印封页：0，由前一个页面传值过来
                        prt4.cKyoten = lblcKYOTEN.Text;
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
                    else if (fMIDASHI == true && fSYOUSAI == false && fMEISAI == false) //見出し
                    {
                        rmt_pcount1 = 0;
                        mitsumori rpt = new mitsumori();
                        rpt.cMITUMORI = lblcMitumori.Text;
                        rpt.cBUKKEN = lnkcBukken.Text;
                        rpt.cTANTOUSHA = lblcJISHATANTOUSHA.Text;
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
                        rpt.cKyoten = lblcKYOTEN.Text;
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
                        if (btnZeinuKingaku1.CssClass == "JC10ZeikomiBtnActive")
                        {
                            rpt.fZEINUKIKINNGAKU = true;
                        }
                        else if (btnZeinuKingaku2.CssClass == "JC10ZeikomiBtnActive")
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
                        Session["UriagePDF"] = "false"; //20220208 added by YG
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
                }
            }
        }
        #endregion

        #region CHK_KingakuNotDisplay_CheckedChanged
        protected void CHK_KingakuNotDisplay_CheckedChanged(object sender, EventArgs e)
        {
            HF_isChange.Value = "1";
        }
        #endregion

        #region btnDeleteUriage_Click
        protected void btnDeleteUriage_Click(object sender, EventArgs e)
        {
            String uriageCode = HF_cUriage.Value;

            String id = Request.QueryString["id"];
            DataTable dt_db = JC01Login_Class.Get_DB(id);
            String db = dt_db.Rows[0]["db"].ToString();
            String mail = dt_db.Rows[0]["mail"].ToString();
            ConstantVal.DB_NAME = db;

            JC27UriageTouroku_Class delete_class = new JC27UriageTouroku_Class();
            if (delete_class.Delete_Uriage(uriageCode))
            {
                lkUriage_Click(sender, e);
            }
        }
        #endregion

        #region btnToLogin_Click
        protected void btnToLogin_Click(object sender, EventArgs e)
        {
            Response.Redirect("JC01Login.aspx");
        }
        #endregion

        #region 別見積で保存
        protected void btnBetsuMitumoriSave_Click(object sender, EventArgs e)
        {
            lblcMitumori.Text = "";

            String id = Request.QueryString["id"];
            DataTable dt_db = JC01Login_Class.Get_DB(id);
            String db = dt_db.Rows[0]["db"].ToString();
            String mail = dt_db.Rows[0]["mail"].ToString();
            ConstantVal.DB_NAME = db;

            JC_ClientConnecction_Class jc = new JC_ClientConnecction_Class();
            jc.loginId = mail;
            DateTime datenow = jc.GetCurrentDate();
            lbldMitumori.Text = datenow.ToString("yyyy/MM/dd");　　//見積日に今日を設定
            lblcMitumori_Ko.Text = "01";

            btndJuuchuCross_Click(sender, e);  //受注日をEmptyにする
            btndUriageYoteiCross_Click(sender, e);　　//売上予定日をEmptyにする
            btndshuryoYoteiCross_Click(sender, e);　//完了予定日をEmptyにする
            btndJuuchuuYoteiCross_Click(sender, e); //受注予定日をEmptyにする
            btndMitumoriKakuteiCross_Click(sender, e);　//見積確定日をEmptyにする

            lblcJoutai.Text = "04";
            lblsJoutai.Text = "見積作成中";
            lblsJoutai.Attributes.Add("onClick", "BtnClick('MainContent_btnJoutai')");
            divJoutaibtn.Style["display"] = "none";
            divJoutaiLabel.Style["display"] = "block";
            updJoutai.Update();


            updHeader.Update();
            btnMitumoriSave_Click(sender, e);
        }
        #endregion

        #region GV_Uriage_RowDataBound
        protected void GV_Uriage_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton lbk_pdf = (e.Row.FindControl("lnkbtnuriageOutput") as LinkButton);

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
                    if (dt_gettantou.Rows[0]["Shoshiki"].ToString() == "1")
                    {
                        lbk_pdf.Text = "兼請求書PDF出力";
                    }
                    else
                    {
                        lbk_pdf.Text = "納品書と請求書PDF出力";
                    }
                }
            }
        }
        #endregion

        #region Login_Tantou
        private void Login_Tantou()
        {
            lblLoginUserName.Text = JC99NavBar_Class.Login_Tan_Name;
            lblLoginUserCode.Text = JC99NavBar_Class.Login_Tan_Code;
        }
        #endregion

        #region GV_Uriage_Original_RowCommand
        protected void GV_Uriage_Original_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "PDF")
            {
                GridViewRow row = (GridViewRow)(((Control)e.CommandSource).NamingContainer);
                int index = row.RowIndex;
                HF_cUriage.Value = GV_Uriage_Original.DataKeys[index].Value.ToString();
                Session["uriageCode"] = "true";
                upd_Hidden.Update();
                updHeader.Update();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ClickPDFButton",
                   "ClickPDFButton();", true);
            }
        }
        #endregion

        #region BT_UriagePDF_Click
        protected void BT_UriagePDF_Click(object sender, EventArgs e)
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
                    string flagrogo = "";
                    String flagbikou = "";  //20220131 MiMi Added
                    String cKyoten = "";
                    String sBikou = ""; //20220131 MiMi Added
                    String dUriage = ""; //20220131 MiMi Added
                    DataTable dt_r_uriage = new DataTable();
                    ConstantVal.DB_NAME = db;
                    JC27UriageTouroku_Class r_uriage_data = new JC27UriageTouroku_Class();
                    r_uriage_data.cURIAGE = HF_cUriage.Value;
                    dt_r_uriage = r_uriage_data.UriageData();
                    cKyoten = dt_r_uriage.Rows[0]["cCO"].ToString();
                    sBikou = dt_r_uriage.Rows[0]["sbikou"].ToString();  //20220131 MiMi Added
                    dUriage = dt_r_uriage.Rows[0]["dURIAGE"].ToString();  //20220131 MiMi Added

                    if (cKyoten != "")
                    {
                        ConstantVal.DB_NAME = db;
                        JC_ClientConnecction_Class jc = new JC_ClientConnecction_Class();
                        jc.loginId = mail;
                        con = jc.GetConnection();
                        con.Open();
                        if (dt_gettantou.Rows.Count > 0)
                        {
                            flagrogo = dt_gettantou.Rows[0]["rogo"].ToString().TrimEnd();
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

                        uriage rpt = new uriage();
                        rpt.cURIAGE = HF_cUriage.Value;
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
                        rpt_seikyusho.cURIAGE = HF_cUriage.Value;
                        rpt_seikyusho.dINVOICE = dUriage;
                        rpt_seikyusho.dMITUMORISAKUSEI = dUriage;
                        if (dt_gettantou.Rows[0]["kingaku"].ToString().TrimEnd() == "1")
                        {
                            rpt_seikyusho.fhiduke = true;
                        }
                        else
                        {
                            rpt_seikyusho.fhiduke = false;
                        }
                        rpt_seikyusho.fbikou = flagbikou;
                        rpt_seikyusho.loginId = mail;
                        rpt_seikyusho.ckyoten = cKyoten;
                        rpt_seikyusho.frogoimage = flagrogo;
                        rpt_seikyusho.bikou = sBikou;
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

        #region PrintSeikyuShoPDF
        private void PrintSeikyuShoPDF(object sender, EventArgs e)
        {
            try
            {
                string flagrogo = "";
                String flagbikou = "";
                String cKyoten = "";
                String sBikou = "";
                String dUriage = "";

                String id = Request.QueryString["id"];
                DataTable dt_db = JC01Login_Class.Get_DB(id);
                String db = dt_db.Rows[0]["db"].ToString();
                String mail = dt_db.Rows[0]["mail"].ToString();
                ConstantVal.DB_NAME = db;

                DataTable dt_r_uriage = new DataTable();
                JC27UriageTouroku_Class r_uriage_data = new JC27UriageTouroku_Class();
                r_uriage_data.cURIAGE = HF_cUriage.Value;
                dt_r_uriage = r_uriage_data.UriageData();
                cKyoten = dt_r_uriage.Rows[0]["cCO"].ToString();
                sBikou = dt_r_uriage.Rows[0]["sbikou"].ToString();
                dUriage = dt_r_uriage.Rows[0]["dURIAGE"].ToString();

                JC_ClientConnecction_Class jc = new JC_ClientConnecction_Class();
                ConstantVal.DB_NAME = db;
                jc.loginId = mail;

                DateTime datenow = jc.GetCurrentDate();
                String fileName = "～兼請求書～" + HF_cUriage.Value + "_" + datenow.ToString("yyyyMMdd");

                con = jc.GetConnection();
                con.Open();

                JC27UriageTouroku_Class settei = new JC27UriageTouroku_Class();
                Login_Tantou();
                settei.cTANTOUSHA = lblLoginUserCode.Text;
                DataTable dt_gettantou = settei.GetTantou();

                if (dt_gettantou.Rows.Count > 0)
                {
                    flagrogo = dt_gettantou.Rows[0]["rogo"].ToString().TrimEnd();
                    flagbikou = dt_gettantou.Rows[0]["seikyubikou"].ToString().TrimEnd();
                }

                seikyuusho rpt = new seikyuusho();
                rpt.stitle = "兼請求書";
                rpt.fSEIKYUU = true;
                rpt.cURIAGE = HF_cUriage.Value;
                rpt.dINVOICE = dUriage;
                rpt.dMITUMORISAKUSEI = dUriage;
                if (dt_gettantou.Rows[0]["kingaku"].ToString().TrimEnd() == "1")
                {
                    rpt.fhiduke = true;
                }
                else
                {
                    rpt.fhiduke = false;
                }
                rpt.fbikou = flagbikou;
                rpt.loginId = mail;
                rpt.ckyoten = cKyoten;
                rpt.frogoimage = flagrogo;
                rpt.bikou = sBikou;
                Login_Tantou();
                rpt.Run();
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                GrapeCity.ActiveReports.Export.Pdf.Section.PdfExport pdfExport1 = new GrapeCity.ActiveReports.Export.Pdf.Section.PdfExport();
                pdfExport1.Export(rpt.Document, ms);
                ms.Position = 0;
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

                con.Close();
            }
            catch
            {
                Response.Redirect("JC01Login.aspx");
            }
        }
        #endregion

        #region BT_ColumnWidthUriage_Click
        protected void BT_ColumnWidthUriage_Click(object sender, EventArgs e)
        {
            Response.Cookies["colWidthmUraige"].Value = HF_GridSizeUriage.Value;
            Response.Cookies["colWidthmUraige"].Expires = DateTime.Now.AddYears(1);
            Response.Cookies["colWidthmUraigeGrid"].Value = HF_GridUriage.Value;
            Response.Cookies["colWidthmUraigeGrid"].Expires = DateTime.Now.AddYears(1);
        }
        #endregion

        #region BT_ColumnWidthSyouhin_Click
        protected void BT_ColumnWidthSyouhin_Click(object sender, EventArgs e)
        {
            Response.Cookies["colWidthmSyouhin"].Value = HF_GridSizeSyouhin.Value;
            Response.Cookies["colWidthmSyouhin"].Expires = DateTime.Now.AddYears(1);
            Response.Cookies["colWidthmSyouhinGrid"].Value = HF_GridSyouhin.Value;
            Response.Cookies["colWidthmSyouhinGrid"].Expires = DateTime.Now.AddYears(1);
        }
        #endregion

        #region  GV_MitumoriSyohin_SelectedIndexChanged 20220525 Maythu,MyatNoe add
        protected void GV_MitumoriSyohin_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (GridViewRow row in GV_MitumoriSyohin.Rows)
            {
                if (row.RowIndex == GV_MitumoriSyohin.SelectedIndex)
                {
                    HF_syouhinRowIndex.Value = row.RowIndex.ToString();
                }
            }
        }
        #endregion
    }
}