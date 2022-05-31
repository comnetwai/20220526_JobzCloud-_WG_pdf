using Common;
using jobzcolud.WebFront;
using MySql.Data.MySqlClient;
using Service;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace JobzCloud.WebFront
{
    public partial class JC19TokuisakiSyousai : System.Web.UI.Page
    {
        MySqlConnection cn = null;
        MySqlCommand cmd = null;
        string Database = null;
        DataTable dt = new DataTable();
        DataTable dt1 = new DataTable();
        string stantou, sbumon, tel, yakusho, keisho = "";
        string tantouCarry;//20220225 MyatNoe Added
        Int64 njun;

        bool f_isomoji_msg = true;

        #region Page_Load()
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
                            cn = jc.GetConnection();
                            DataTable dt_loginuser = jc.GetLoginUserCodeFromClientDB();
                            JC99NavBar navbar_Master = (JC99NavBar)this.Master;
                            navbar_Master.lnkBtnSetting.Style.Add(" background-color", "rgba(46,117,182)");
                            navbar_Master.navbar2.Visible = false;
                            lblLoginUserCode.Text = dt_loginuser.Rows[0]["code"].ToString();
                            //lblLoginUserName.Text = dt_loginuser.Rows[0]["name"].ToString();

                            //lnkbtnSetting.Style.Add(" background-color", "rgba(46,117,182)");
                            div3.Visible = true;
                            divPopupHeader.Visible = false;
                            btCancel.Visible = false;
                            DivLine.Visible = false;
                            if (Session["fTokuisakiSyosai"] != null)
                            {
                                if (Session["fTokuisakiSyosai"].ToString() == "Popup")
                                {
                                    if (SessionUtility.GetSession("HOME") != null)
                                    {
                                        hdnHome.Value = SessionUtility.GetSession("HOME").ToString();
                                        SessionUtility.SetSession("HOME", null);
                                    }
                                    HF_flag.Value = Session["fTokuisakiSyosai"].ToString();
                                    navbar_Master.div_Nav.Visible = false;
                                    navbar_Master.div2.Visible = false;
                                    BD_Tokuisaki.Style.Add("background-color", "transparent");
                                    navbar_Master.divContent.Style.Add("background-color", "transparent");
                                    navbar_Master.form1.Style.Add("background-color", "transparent");
                                    navbar_Master.BD_Master.Style.Add("background-color", "transparent");
                                    Div_Body.Style.Add("background-color", "transparent");
                                    btnshinki.Visible = false;
                                    div3.Visible = false;
                                    divPopupHeader.Visible = true;
                                    btCancel.Visible = true;
                                    DivLine.Visible = true;

                                }

                            }
                            if (Session["cTokuisakiBukken"] != null)
                            {
                                String ctokuisaki = Session["cTokuisakiBukken"].ToString();
                                lblcode.Text = ctokuisaki;
                                SetTokuisakiData();
                                BindGridview();
                                GridView1.Rows[0].Visible = false; //20220222 Added MyatNoe
                                GridView1.FooterRow.Visible = false;

                            }
                            else
                            {
                                lblsShiiresaki.Text = "";
                                lblcShiiresaki.Text = "";
                                createnewrow();
                                HF_isChange.Value = "1";
                                updHeader.Update();

                                //dt1.Columns.Add("NJUNBAN", typeof(string));
                                //dt1.Columns.Add("STANTOU", typeof(string));
                                //dt1.Columns.Add("SBUMON", typeof(string));
                                //dt1.Columns.Add("SYAKUSHOKU", typeof(string));
                                //dt1.Columns.Add("STEL", typeof(string));
                                //dt1.Columns.Add("SKEISHOU", typeof(string));
                                //ViewState["Row"] = dt1;
                                //GridView1.DataSource = ViewState["Row"];
                                //GridView1.DataBind();
                                //GridView1.FooterRow.Visible = false;
                            }

                            if (divLabelSave.Style["display"] != "none")
                            {
                                divLabelSave.Style["display"] = "none";
                                updHeader.Update();
                            }
                        }
                        catch
                        {
                            if (HF_flag.Value == "Popup")
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "parentButtonClick('btnToLogin','" + hdnHome.Value + "');", true);
                            }
                            else
                            {
                                Response.Redirect("JC01Login.aspx");
                            }
                        }
                    }
                    else
                    {
                        lblPhoneError.Text = "";
                        if (divLabelSave.Style["display"] != "none")
                        {
                            divLabelSave.Style["display"] = "none";
                            updHeader.Update();
                        }
                    }
                    Session.Timeout = 480;
                }
                else
                {
                    if (HF_flag.Value == "Popup")
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "parentButtonClick('btnToLogin','" + hdnHome.Value + "');", true);
                    }
                    else
                    {
                        Response.Redirect("JC01Login.aspx");
                    }
                }
            }
            catch
            {
                if (HF_flag.Value == "Popup")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "parentButtonClick('btnToLogin','" + hdnHome.Value + "');", true);
                }
                else
                {
                    Response.Redirect("JC01Login.aspx");
                }
            }
        }
        #endregion

        #region SetTokuisakiData()
        protected void SetTokuisakiData()
        {
            String id = Request.QueryString["id"];
            DataTable dt_db = JC01Login_Class.Get_DB(id);
            String db = dt_db.Rows[0]["db"].ToString();
            String mail = dt_db.Rows[0]["mail"].ToString();
            ConstantVal.DB_NAME = db;

            JC_ClientConnecction_Class jc = new JC_ClientConnecction_Class();
            jc.loginId = mail;
            cn = jc.GetConnection();
            cn.Open();
            string sql = "SELECT cTOKUISAKI as ctokuisaki," +
                " sTOKUISAKI1 as stokuisaki," +
                " cYUUBIN as cYUUBIN," +
                " sJUUSHO1 as sJUUSHO1, " +
                " sJUUSHO2 as sJUUSHO2," +
                " sTEL as sTEL," +
                " sFAX as sFAX," +
                " fHAIJYO as fDEL," +
                " cSEIKYUSAKI as cSEIKYUSAKI," +
                " sSEIKYUSAKI as sSEIKYUSAKI," +
                " dTORIHIKIDATE as dTORIHIKIDATE," +
                " mjt.cTANTOUSHA as ceigyotantou," +
                " mjt.sTANTOUSHA as seigyotantou," +
                " sTOKKIJIKOU as sTOKKIJIKOU," +
                " cSHIHARAI as cSHIHARAI," +
                " sSHIHARAI as sSHIHARAI " +
                " FROM m_tokuisaki as mt" +
                " Left join m_j_tantousha as mjt ON mt.cEIGYOU_C = mjt.cTANTOUSHA" +
                " where cTOKUISAKI = '" + lblcode.Text + "'; ";
            cmd = new MySqlCommand(sql, cn);
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                txtsTouisaki.Text = dt.Rows[0]["stokuisaki"].ToString();
                txtcYUUBIN.Text = dt.Rows[0]["cYUUBIN"].ToString();
                txtsJUUSHO1.Text = dt.Rows[0]["sJUUSHO1"].ToString();
                txtsJUUSHO2.Text = dt.Rows[0]["sJUUSHO2"].ToString();
                txtPhone.Text = dt.Rows[0]["sTEL"].ToString();
                txtsFAX.Text = dt.Rows[0]["sFAX"].ToString();
                if (dt.Rows[0]["fDEL"].ToString().Equals("1"))
                {
                    chkdelete.Checked = true;
                }
                else
                {
                    chkdelete.Checked = false;
                }
                if (!String.IsNullOrEmpty(dt.Rows[0]["cSEIKYUSAKI"].ToString()))
                {
                    lblcShiiresaki.Text = dt.Rows[0]["cSEIKYUSAKI"].ToString();
                    lblsShiiresaki.Text = dt.Rows[0]["sSEIKYUSAKI"].ToString();
                    lblsShiiresaki.Attributes.Add("onClick", "BtnClick('MainContent_btn_tokuisaki')");
                    divTokuisakiBtn.Style["display"] = "none";
                    divTokuisakiLabel.Style["display"] = "block";
                    upd_TOKUISAKI.Update();
                }

                if (!String.IsNullOrEmpty(dt.Rows[0]["dTORIHIKIDATE"].ToString()))
                {
                    DateTime date = Convert.ToDateTime(dt.Rows[0]["dTORIHIKIDATE"].ToString());
                    lbldTokuisaki.Text = date.ToString("yyyy/MM/dd");
                    lbldTokuisaki.Attributes.Add("onClick", "BtnClick('MainContent_btnTokuisakiDate')");
                    btnTokuisakiDate.Style["display"] = "none";
                    divTokuisakiDate.Style["display"] = "block";
                    updTokuisakiDate.Update();
                }

                if (!String.IsNullOrEmpty(dt.Rows[0]["ceigyotantou"].ToString()))
                {
                    lblcJISHATANTOUSHA.Text = dt.Rows[0]["ceigyotantou"].ToString();
                    lblsJISHATANTOUSHA.Text = dt.Rows[0]["seigyotantou"].ToString().Replace("<", "&lt").Replace(">", "&gt");
                    lblsJISHATANTOUSHA.Attributes.Add("onClick", "BtnClick('MainContent_BT_JisyaTantousya_Add')");
                    divTantousyaBtn.Style["display"] = "none";
                    divTantousyaLabel.Style["display"] = "block";
                    upd_JISHATANTOUSHA.Update();
                }

                txtsTOKKIJIKOU.Text = dt.Rows[0]["sTOKKIJIKOU"].ToString();

                if (!String.IsNullOrEmpty(dt.Rows[0]["cSHIHARAI"].ToString()))
                {
                    lblcShiharai.Text = dt.Rows[0]["cSHIHARAI"].ToString();
                    lblsShihariai.Text = dt.Rows[0]["sSHIHARAI"].ToString().Replace("<", "&lt").Replace(">", "&gt");
                    lblsShihariai.Attributes.Add("onClick", "BtnClick('MainContent_btnshiharai')");
                    divShiharaibtn.Style["display"] = "none";
                    divShiharaiLabel.Style["display"] = "block";
                    updShiharaiHouhou.Update();
                }
            }
            cn.Close();
        }
        #endregion

        #region bt_tokuisaki_Click()
        protected void btn_tokuisaki_Click(object sender, EventArgs e)
        {
            SessionUtility.SetSession("HOME", "Master");
            //ifSentakuPopup.Style["width"] = "1160px";
            //ifSentakuPopup.Style["height"] = "650px";
            //ifSentakuPopup.Src = "JC18TokuisakiKensaku.aspx";
            //mpeSentakuPopup.Show();
            Session["ftokuisakisyosai"] = "1";
            String id = Request.QueryString["id"];
            ifShinkiPopup.Src = "JC18TokuisakiKensaku.aspx?id=" + id;
            mpeShinkiPopup.Show();
            lblsShiiresaki.Attributes.Add("onClick", "BtnClick('MainContent_btn_tokuisaki')");
            //updSentakuPopup.Update();
            updShinkiPopup.Update();
        }
        #endregion

        #region getcTokuisaki
        protected DataTable getcTokuisaki()
        {
            string sql = "select cTOKUISAKI as cTOKUISAKI from M_TOKUISAKI order by cTOKUISAKI";
            cmd = new MySqlCommand(sql, cn);
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
        #endregion

        #region txtPhone_TextChanged
        protected void txtPhone_TextChanged(object sender, EventArgs e)
        {
            if (TextUtility.IsIncludeZenkaku(txtPhone.Text))
            {
                lblPhoneError.Text = "半角英数を入力してください。";
                txtPhone.BorderColor = System.Drawing.Color.Red;
                txtPhone.BorderStyle = BorderStyle.Double;
                txtPhone.BorderWidth = 2;
                txtPhone.Text = "";
                txtPhone.Focus();
            }
            #region deleted by MyatNoe
            //else if (txtPhone.Text != "")
            //{
            //    if (!TextUtility.IsPhoneNumber(txtPhone.Text))
            //    {
            //        lblPhoneError.Text = "正しくお電話を入力してください";
            //        txtPhone.BorderColor = System.Drawing.Color.Red;
            //        txtPhone.BorderStyle = BorderStyle.Double;
            //        txtPhone.BorderWidth = 2;
            //        txtPhone.Text = "";
            //        txtPhone.Focus();
            //    }
            //    else
            //    {
            //        lblPhoneError.Text = "";
            //        txtPhone.BorderColor = System.Drawing.Color.LightGray;
            //        txtPhone.BorderStyle = BorderStyle.Solid;
            //        txtPhone.BorderWidth = 1;
            //    }
            //}
            #endregion
            else
            {
                lblPhoneError.Text = "";
                //txtPhone.BorderColor = System.Drawing.Color.LightGray;
                txtPhone.Style["border-color"] = "none";
                txtPhone.BorderStyle = BorderStyle.Solid;
                txtPhone.BorderWidth = 1;
            }
            HF_isChange.Value = "1";
            updHeader.Update();
        }
        #endregion

        #region btnhozone_Click()
        protected void btnhozon_Click(object sender, EventArgs e)
        {
            //getcTokuisaki1();
            Boolean saveSuccess = false;
            if (lblcode.Text == "")  //新規
            {
                String id = Request.QueryString["id"];
                DataTable dt_db = JC01Login_Class.Get_DB(id);
                String db = dt_db.Rows[0]["db"].ToString();
                String mail = dt_db.Rows[0]["mail"].ToString();
                ConstantVal.DB_NAME = db;

                JC_ClientConnecction_Class jc = new JC_ClientConnecction_Class();
                jc.loginId = mail;
                cn = jc.GetConnection();
                DataTable dt_cTokuisaki = getcTokuisaki();
                Int64 ctokuisaki = jc.GetMissingCode(dt_cTokuisaki, 9999999);
                String code = ctokuisaki.ToString().PadLeft(7, '0');
                String stokuisaki = txtsTouisaki.Text.Trim().Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
                String cYuubin = txtcYUUBIN.Text.Trim().Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
                String sJUUSHO1 = txtsJUUSHO1.Text.Trim().Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
                String sJUUSHO2 = txtsJUUSHO2.Text.Trim().Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
                String ceigyotantou = "0000";
                if (!String.IsNullOrEmpty(lblcJISHATANTOUSHA.Text))
                {
                    ceigyotantou = lblcJISHATANTOUSHA.Text.Trim().Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
                }
                String sTel = txtPhone.Text.Trim().Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
                String sFax = txtsFAX.Text.Trim().Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
                String sJIkou = txtsTOKKIJIKOU.Text.Trim().Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
                String dTorihikidate = DateTime.Now.ToString();

                if (lbldTokuisaki.Text != "")
                {
                    dTorihikidate = lbldTokuisaki.Text;
                }
                String cshiharai = lblcShiharai.Text;
                String sshiharai = lblsShihariai.Text.Replace("&lt", "<").Replace("&gt", ">");
                String cshiiresaki = lblcShiiresaki.Text;
                String sshiiresaki = lblsShiiresaki.Text;
                String fHaijyou = "0";

                if (txtPhone.Text != "")
                {
                    txtPhone_TextChanged(sender, e);
                }
                if (chkdelete.Checked)
                {
                    fHaijyou = "1";
                }

                if (stokuisaki != "")
                {
                    if (lblPhoneError.Text == "")
                    {
                        //txtsTouisaki.BorderColor = System.Drawing.Color.LightGray;
                        txtsTouisaki.Style["border-color"] = "none";
                        txtsTouisaki.BorderStyle = BorderStyle.Solid;
                        txtsTouisaki.BorderWidth = 1;

                        //txtPhone.BorderColor = System.Drawing.Color.LightGray;
                        txtPhone.Style["border-color"] = "none";
                        txtPhone.BorderStyle = BorderStyle.Solid;
                        txtPhone.BorderWidth = 1;

                        if (TextUtility.isomojiCharacter(stokuisaki))
                        {
                            f_isomoji_msg = false;
                        }
                        stokuisaki = stokuisaki.Replace("\\", "\\\\").Replace("'", "\\'");  //得意先名 

                        if (TextUtility.isomojiCharacter(sJUUSHO1))
                        {
                            f_isomoji_msg = false;
                        }
                        sJUUSHO1 = sJUUSHO1.Replace("\\", "\\\\").Replace("'", "\\'");  //住所1

                        if (TextUtility.isomojiCharacter(sJUUSHO2))
                        {
                            f_isomoji_msg = false;
                        }
                        sJUUSHO2 = sJUUSHO2.Replace("\\", "\\\\").Replace("'", "\\'");  //住所2

                        if (TextUtility.isomojiCharacter(sJIkou))
                        {
                            f_isomoji_msg = false;
                        }
                        sJIkou = sJIkou.Replace("\\", "\\\\").Replace("'", "\\'");  //特記事項

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

                            String sql = "INSERT INTO m_tokuisaki (cTOKUISAKI, cYUUBIN, sJUUSHO1, sJUUSHO2, sTOKUISAKI1, sTOKUISAKI_R, skTOKUISAKI,"
                        + " sTANTOUSHA, cEIGYOU_C, sTEL, sFAX, sTOKKIJIKOU, dTORIHIKIDATE, fHAIJYO, nNEBIKIRITSU, cRANK_SINYOU, dHENKOU, cHENKOUSYA,"
                        + " cSYUBETU, cGYOUSYU, sSETUMEI, cBUMON, cSAIJI, sSAIJI_CHOUKI, sSHIHARAIGETU, cAYAMATTE, cKATEGORI, cSHIHARAI, sSHIHARAI,"
                        + " fSAMA, fseikyuukubun, seikyuulayout, skihonharai1, nkihonharai_hiritsu1, skihonharai2, nkihonharai_hiritsu2, skakuchouharai1,"
                        + " nkakuchouharai_hiritsu1, skakuchouharai2, nkakuchouharai_hiritsu2, fkakuchouharai, ntekiyoujouken_kingaku, stekiyoujouken,"
                        + " cTOKUISAKI_G1, cTOKUISAKI_G2, cFURIKOMISAKI, fSEIKYUUBIKOUSHIYOU, cSEIKYUSAKI, sSEIKYUSAKI)"
                        + " VALUES ('" + code + "', '" + cYuubin + "', '" + sJUUSHO1 + "', '" + sJUUSHO2 + "', '" + stokuisaki + "', '', '',"
                        + " '', '" + ceigyotantou + "', '" + sTel + "', '" + sFax + "', '" + sJIkou + "', '" + dTorihikidate + "', '" + fHaijyou + "', '100', '00', Now(), '" + lblLoginUserCode.Text + "',"
                        + " '00', '00', '', '00', '', '', '0', '', '', '" + cshiharai + "', '" + sshiharai + "',"
                        + " '0', '0', '0', '', '100', '', '0', '',"
                        + " '100', '', '0', '0', '0.00', '', '', '', '00', '0', '" + cshiiresaki + "', '" + sshiiresaki + "');";

                            String sql_tantouinsert = "";
                            for (int i = 1; i < GridView1.Rows.Count; i++)
                            {
                                String sbumon = (GridView1.Rows[i].FindControl("SBUMON") as Label).Text.Trim().Replace("\r\n", "").Replace("\r", "").Replace("\n", "").Replace("&lt", "<").Replace("&gt", ">");
                                String stantou = (GridView1.Rows[i].FindControl("STANTOU") as Label).Text.Trim().Replace("\r\n", "").Replace("\r", "").Replace("\n", "").Replace("&lt", "<").Replace("&gt", ">");
                                tantouCarry = (GridView1.Rows[i].FindControl("STANTOU") as Label).Text.Trim().Replace("\r\n", "").Replace("\r", "").Replace("\n", "").Replace("&lt", "<").Replace("&gt", ">");//20220225 MyatNoe Added
                                String syakusho = (GridView1.Rows[i].FindControl("SYAKUSHOKU") as Label).Text.Trim().Replace("\r\n", "").Replace("\r", "").Replace("\n", "").Replace("&lt", "<").Replace("&gt", ">");
                                String stel = (GridView1.Rows[i].FindControl("STEL") as Label).Text.Trim().Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
                                String skeisho = (GridView1.Rows[i].FindControl("SKEISHOU") as Label).Text.Trim().Replace("\r\n", "").Replace("\r", "").Replace("\n", "").Replace("&lt", "<").Replace("&gt", ">");
                                sql_tantouinsert += "INSERT INTO tokuisaki_tantousha (CTOKUISAKI, NJUNBAN, SBUMON, STANTOU, SYAKUSHOKU, STEL, SFAX, SMAIL, FMAIL, SBIKOU, SKEISHOU)" +
                                " VALUES ('" + code + "', '" + i + "', '" + sbumon + "', '" + stantou + "', '" + syakusho + "', '" + syakusho + "', '', '', '', '', '" + skeisho + "');";
                            }
                            #region 20220225 MyatNoe Added
                            if (GridView1.Rows.Count == 2)
                            {
                                Session["STANTOUCarry"] = tantouCarry;
                            }
                            else
                            {
                                Session["STANTOUCarry"] = null;
                            }
                            #endregion
                            try
                            {
                                cn.Open();
                                MySqlCommand cmdInsert = new MySqlCommand(sql + sql_tantouinsert, cn);
                                cmdInsert.CommandTimeout = 0;
                                cmdInsert.ExecuteNonQuery();
                                cn.Close();
                                lblcode.Text = code;
                                saveSuccess = true;

                                divLabelSave.Style["display"] = "flex";//「保存しました。」メッセージを表示                                                                                                                                      
                                updLabelSave.Update();
                                HF_isChange.Value = "0";
                                HF_Save.Value = "1";
                                updHeader.Update();

                            }
                            catch (Exception ex)
                            {

                            }
                        }
                    }
                }
                else
                {
                    if (stokuisaki == "")
                    {
                        txtsTouisaki.BorderColor = System.Drawing.Color.Red;
                        txtsTouisaki.BorderStyle = BorderStyle.Double;
                        txtsTouisaki.BorderWidth = 2;
                    }
                }
            }
            else   //更新
            {
                String id = Request.QueryString["id"];
                DataTable dt_db = JC01Login_Class.Get_DB(id);
                String db = dt_db.Rows[0]["db"].ToString();
                String mail = dt_db.Rows[0]["mail"].ToString();
                ConstantVal.DB_NAME = db;

                JC_ClientConnecction_Class jc = new JC_ClientConnecction_Class();
                jc.loginId = mail;
                cn = jc.GetConnection();
                String code = lblcode.Text;
                String stokuisaki = txtsTouisaki.Text.Trim().Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
                String cYuubin = txtcYUUBIN.Text.Trim().Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
                String sJUUSHO1 = txtsJUUSHO1.Text.Trim().Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
                String sJUUSHO2 = txtsJUUSHO2.Text.Trim().Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
                String ceigyotantou = "0000";
                if (!String.IsNullOrEmpty(lblcJISHATANTOUSHA.Text))
                {
                    ceigyotantou = lblcJISHATANTOUSHA.Text.Trim().Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
                }
                String sTel = txtPhone.Text.Trim().Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
                String sFax = txtsFAX.Text.Trim().Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
                String sJIkou = txtsTOKKIJIKOU.Text.Trim().Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
                String dTorihikidate = DateTime.Now.ToString();
                if (lbldTokuisaki.Text != "")
                {
                    dTorihikidate = lbldTokuisaki.Text;
                }
                String cshiharai = lblcShiharai.Text;
                String sshiharai = lblsShihariai.Text.Replace("&lt", "<").Replace("&gt", ">");
                String cshiiresaki = lblcShiiresaki.Text;
                String sshiiresaki = lblsShiiresaki.Text;
                String fHaijyou = "0";
                if (txtPhone.Text != "")
                {
                    txtPhone_TextChanged(sender, e);
                }
                if (chkdelete.Checked)
                {
                    fHaijyou = "1";
                }

                if (stokuisaki != "")
                {
                    if (lblPhoneError.Text == "")
                    {
                        //txtsTouisaki.BorderColor = System.Drawing.Color.LightGray;
                        txtsTouisaki.Style["border-color"] = "none";
                        txtsTouisaki.BorderStyle = BorderStyle.Solid;
                        txtsTouisaki.BorderWidth = 1;

                        //txtPhone.BorderColor = System.Drawing.Color.LightGray;
                        txtPhone.Style["border-color"] = "none";
                        txtPhone.BorderStyle = BorderStyle.Solid;
                        txtPhone.BorderWidth = 1;

                        if (TextUtility.isomojiCharacter(stokuisaki))
                        {
                            f_isomoji_msg = false;
                        }
                        stokuisaki = stokuisaki.Replace("\\", "\\\\").Replace("'", "\\'");  //得意先名 

                        if (TextUtility.isomojiCharacter(sJUUSHO1))
                        {
                            f_isomoji_msg = false;
                        }
                        sJUUSHO1 = sJUUSHO1.Replace("\\", "\\\\").Replace("'", "\\'");  //住所1

                        if (TextUtility.isomojiCharacter(sJUUSHO2))
                        {
                            f_isomoji_msg = false;
                        }
                        sJUUSHO2 = sJUUSHO2.Replace("\\", "\\\\").Replace("'", "\\'");  //住所2

                        if (TextUtility.isomojiCharacter(sJIkou))
                        {
                            f_isomoji_msg = false;
                        }
                        sJIkou = sJIkou.Replace("\\", "\\\\").Replace("'", "\\'");  //特記事項

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

                            String sql = "UPDATE m_tokuisaki SET cYUUBIN='" + cYuubin + "', " +
                            "sJUUSHO1='" + sJUUSHO1 + "', sJUUSHO2='" + sJUUSHO2 + "', sTOKUISAKI1='" + stokuisaki + "'," +
                            " cEIGYOU_C='" + ceigyotantou + "', sTEL='" + sTel + "', sFAX='" + sFax + "'," +
                            " sTOKKIJIKOU='" + sJIkou + "', dTORIHIKIDATE='" + dTorihikidate + "'," +
                            " fHAIJYO='" + fHaijyou + "', dHENKOU=Now(), cHENKOUSYA='" + lblLoginUserCode.Text + "'," +
                            " cSHIHARAI='" + cshiharai + "', sSHIHARAI='" + sshiharai + "', cSEIKYUSAKI='" + cshiiresaki + "', " +
                            "sSEIKYUSAKI='" + sshiiresaki + "' WHERE cTOKUISAKI='" + code + "';";

                            String sql_tantouinsert = "DELETE FROM tokuisaki_tantousha WHERE CTOKUISAKI='" + code + "';";
                            for (int i = 1; i < GridView1.Rows.Count; i++)
                            {
                                String sbumon = (GridView1.Rows[i].FindControl("SBUMON") as Label).Text.Trim().Replace("\r\n", "").Replace("\r", "").Replace("\n", "").Replace("&lt", "<").Replace("&gt", ">");
                                String stantou = (GridView1.Rows[i].FindControl("STANTOU") as Label).Text.Trim().Replace("\r\n", "").Replace("\r", "").Replace("\n", "").Replace("&lt", "<").Replace("&gt", ">");
                                tantouCarry = (GridView1.Rows[i].FindControl("STANTOU") as Label).Text.Trim().Replace("\r\n", "").Replace("\r", "").Replace("\n", "").Replace("&lt", "<").Replace("&gt", ">");//20220225 MyatNoe Added
                                String syakusho = (GridView1.Rows[i].FindControl("SYAKUSHOKU") as Label).Text.Trim().Replace("\r\n", "").Replace("\r", "").Replace("\n", "").Replace("&lt", "<").Replace("&gt", ">");
                                String stel = (GridView1.Rows[i].FindControl("STEL") as Label).Text.Trim().Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
                                String skeisho = (GridView1.Rows[i].FindControl("SKEISHOU") as Label).Text.Trim().Replace("\r\n", "").Replace("\r", "").Replace("\n", "").Replace("&lt", "<").Replace("&gt", ">");
                                sql_tantouinsert += "INSERT INTO tokuisaki_tantousha (CTOKUISAKI, NJUNBAN, SBUMON, STANTOU, SYAKUSHOKU, STEL, SFAX, SMAIL, FMAIL, SBIKOU, SKEISHOU)" +
                                " VALUES ('" + code + "', '" + i + "', '" + sbumon + "', '" + stantou + "', '" + syakusho + "', '" + syakusho + "', '', '', '', '', '" + skeisho + "');";
                            }

                            if (GridView1.Rows.Count == 2)
                            {
                                Session["STANTOUCarry"] = tantouCarry;
                            }
                            else
                            {
                                Session["STANTOUCarry"] = null;
                            }

                            try
                            {
                                cn.Open();
                                MySqlCommand cmdInsert = new MySqlCommand(sql + sql_tantouinsert, cn);
                                cmdInsert.CommandTimeout = 0;
                                cmdInsert.ExecuteNonQuery();
                                cn.Close();
                                lblcode.Text = code;
                                saveSuccess = true;

                                divLabelSave.Style["display"] = "flex";//「保存しました。」メッセージを表示                                                                                                                                      
                                updLabelSave.Update();
                                HF_isChange.Value = "0";
                                HF_Save.Value = "1";
                                updHeader.Update();
                            }
                            catch (Exception ex)
                            {

                            }
                        }
                    }
                }
                else
                {
                    if (stokuisaki == "")
                    {
                        txtsTouisaki.BorderColor = System.Drawing.Color.Red;
                        txtsTouisaki.BorderStyle = BorderStyle.Double;
                        txtsTouisaki.BorderWidth = 2;

                    }
                }
            }
            if (saveSuccess)
            {
                if (HF_flag.Value == "Popup")
                {
                    Session["fTokuisakiSyosai"] = null;
                    Session["cTOUKUISAKI"] = lblcode.Text;
                    Session["sTOUKUISAKI"] = txtsTouisaki.Text.Replace("<", "&lt").Replace(">", "&gt");
                    if(Session["STANTOUCarry"] != null) //20220225 MyatNoe Added
                    {
                        Session["STANTOUCarry"] = tantouCarry.Replace("<", "&lt").Replace(">", "&gt");
                    }
                    
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "parentButtonClick('btn_Close','" + hdnHome.Value + "');", true);
                }
            }
        }
        #endregion

        #region BindGridView()
        protected void BindGridview()
        {
            String id = Request.QueryString["id"];
            DataTable dt_db = JC01Login_Class.Get_DB(id);
            String db = dt_db.Rows[0]["db"].ToString();
            String mail = dt_db.Rows[0]["mail"].ToString();
            ConstantVal.DB_NAME = db;

            JC_ClientConnecction_Class jc = new JC_ClientConnecction_Class();
            jc.loginId = mail;
            cn = jc.GetConnection();
            cn.Open();
            cmd = new MySqlCommand("select NJUNBAN,STANTOU,SBUMON,STEL,SYAKUSHOKU,SKEISHOU from tokuisaki_tantousha where CTOKUISAKI='" + lblcode.Text + "' order by NJUNBAN asc", cn);
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            dt = new DataTable();
            dt.Columns.Add("NJUNBAN", typeof(int));
            dt.Columns.Add("STANTOU", typeof(string));
            dt.Columns.Add("SBUMON", typeof(string));
            dt.Columns.Add("SYAKUSHOKU", typeof(string));
            dt.Columns.Add("STEL", typeof(string));
            dt.Columns.Add("SKEISHOU", typeof(string));
            DataRow dr1 = dt.NewRow();
            dr1["NJUNBAN"] = 0;
            dr1["STANTOU"] = "";
            dr1["SBUMON"] = "";
            dr1["SYAKUSHOKU"] = "";
            dr1["STEL"] = "";
            dr1["SKEISHOU"] = "";
            dt.Rows.Add(dr1);
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                GridView1.DataSource = dt;
                ViewState["sortDirection"] = SortDirection.Ascending;
                ViewState["z_sortexpresion"] = "NJUNBAN";
                GridView1.DataBind();
                GridView1.Rows[0].Visible = true;
                GridView1.FooterRow.Visible = false;
                Label njun = (GridView1.Rows[0].FindControl("lblnjun") as Label);
                //if (njun.Text == "0" || String.IsNullOrEmpty(njun.Text))
                //{
                //    Panel dropdown_Panel = (GridView1.Rows[0].FindControl("DropdownPanel") as Panel);
                //    dropdown_Panel.Visible = false;
                //}
            }
            else
            {
                GridView1.DataSource = dt;
                GridView1.DataBind();
                GridView1.Rows[0].Visible = true;
                GridView1.FooterRow.Visible = false;
                Label njun = (GridView1.Rows[0].FindControl("lblnjun") as Label);
                //if (njun.Text == "0" || String.IsNullOrEmpty(njun.Text))
                //{
                //    Panel dropdown_Panel = (GridView1.Rows[0].FindControl("DropdownPanel") as Panel);
                //    dropdown_Panel.Visible = false;
                //}
            }
            cn.Close();

            ViewState["Row"] = dt;
        }
        #endregion

        #region btnshiharai_Click()
        protected void btnshiharai_Click(object sender, EventArgs e)
        {
            SessionUtility.SetSession("HOME", "Master");
            ifSentakuPopup.Style["width"] = "470px";
            ifSentakuPopup.Style["height"] = "620px";
            String id = Request.QueryString["id"];
            ifSentakuPopup.Src = "JC25_Shiharaihouhou.aspx?id=" + id;
            mpeSentakuPopup.Show();
            lblsShihariai.Attributes.Add("onClick", "BtnClick('MainContent_btnshiharai')");
            updSentakuPopup.Update();
        }
        #endregion

        #region btnShihariCross_Click
        protected void btnShihariCross_Click(object sender, EventArgs e)
        {
            lblsShihariai.Text = "";
            lblcShiharai.Text = "";
            divShiharaibtn.Style["display"] = "block";
            divShiharaiLabel.Style["display"] = "none";
            updShiharaiHouhou.Update();
            HF_isChange.Value = "1";
            updHeader.Update();
        }
        #endregion

        #region btnshiarailistSelect_Click()
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
                updShiharaiHouhou.Update();
                HF_isChange.Value = "1";
                updHeader.Update();
            }

            ifSentakuPopup.Src = "";
            mpeSentakuPopup.Hide();
            updSentakuPopup.Update();
        }
        #endregion

        #region createnewrow()
        protected void createnewrow()
        {
            if (ViewState["Row"] != null)
            {

                dt1 = (DataTable)ViewState["Row"];
                if (dt1.Rows.Count > 0)
                {
                    dt1.DefaultView.Sort = "NJUNBAN ASC";
                    dt1.AcceptChanges();
                    if (dt1.Rows[0][1].ToString() != "")
                    {
                        DataRow dr1 = dt1.NewRow();
                        dr1["NJUNBAN"] = 0;
                        dr1["STANTOU"] = "";
                        dr1["SBUMON"] = "";
                        dr1["SYAKUSHOKU"] = "";
                        dr1["STEL"] = "";
                        dr1["SKEISHOU"] = "";
                        dt1.Rows.Add(dr1);
                    }
                    if (njun != 0)
                    {
                        dt1.Rows.Add(njun, stantou, sbumon, yakusho, tel, keisho);
                    }

                    dt1.DefaultView.Sort = "NJUNBAN ASC";
                    dt1.AcceptChanges();
                    ViewState["Row"] = dt1;
                    GridView1.DataSource = dt1;
                    GridView1.DataBind();
                    GridView1.Rows[0].Visible = false;//20220222 Added MyatNoe
                    Label njunLabel = (GridView1.Rows[0].FindControl("lblnjun") as Label);
                    //if (njunLabel.Text == "0" || String.IsNullOrEmpty(njunLabel.Text))
                    //{
                    //    Panel dropdown_Panel = (GridView1.Rows[0].FindControl("DropdownPanel") as Panel);
                    //    dropdown_Panel.Visible = false;
                    //}
                }
            }
            else
            {
                dt1.Columns.Add("NJUNBAN", typeof(int));
                dt1.Columns.Add("STANTOU", typeof(string));
                dt1.Columns.Add("SBUMON", typeof(string));
                dt1.Columns.Add("SYAKUSHOKU", typeof(string));
                dt1.Columns.Add("STEL", typeof(string));
                dt1.Columns.Add("SKEISHOU", typeof(string));

                DataRow dr1 = dt1.NewRow();
                dr1["NJUNBAN"] = njun;
                dr1["STANTOU"] = stantou;
                dr1["SBUMON"] = sbumon;
                dr1["SYAKUSHOKU"] = yakusho;
                dr1["STEL"] = tel;
                dr1["SKEISHOU"] = keisho;
                dt1.Rows.Add(dr1);
                dt1.DefaultView.Sort = "NJUNBAN ASC";
                dt1.AcceptChanges();
                ViewState["Row"] = dt1;
                ViewState["sortDirection"] = SortDirection.Ascending;
                ViewState["z_sortexpresion"] = "NJUNBAN";
                GridView1.DataSource = dt1;


                GridView1.DataBind();
                GridView1.Rows[0].Visible = false;//20220222 Added MyatNoe
                Label njunLabel = (GridView1.Rows[0].FindControl("lblnjun") as Label);
                //if (njunLabel.Text == "0" || String.IsNullOrEmpty(njunLabel.Text))
                //{
                //    Panel dropdown_Panel = (GridView1.Rows[0].FindControl("DropdownPanel") as Panel);
                //    dropdown_Panel.Visible = false;
                //}
            }
        }
        #endregion

        #region btnaddcust_Click1()
        protected void btnaddcust_Click1(object sender, EventArgs e)
        {
            GridView1.EditIndex = -1;
            if ((DataTable)ViewState["Row"] == null || ((DataTable)ViewState["Row"]).Rows.Count == 0 || ((DataTable)ViewState["Row"]).Rows.Count == 1)
            {
                //ViewState["Row"] = null;

                createnewrow();

                //GridView1.Rows[0].Visible = true; 20220223 MyatNoe Delete
                GridView1.FooterRow.Visible = false;

                //ViewState["Row"] = null;
                //(GridView1.FooterRow.FindControl("txtnjunbanFooter") as TextBox).Text = 1.ToString();
                (GridView1.FooterRow.FindControl("lblnjunbanFooter") as Label).Text = 1.ToString(); ////20220225 Updated エインドリ－
            }
            else
            {
                int count = 1;
                int result = 0;
                bool skip = true;
                DataTable dt1 = (DataTable)ViewState["Row"];
                dt = (DataTable)ViewState["Row"];
                dt.DefaultView.Sort = "NJUNBAN ASC";
                dt = dt.DefaultView.ToTable();
                
                if (dt.Rows[0][2].ToString() != "")
                {
                    skip = false;
                }
              
                foreach (DataRow row in dt.Rows)
                {
                    
                    if (!skip)
                    {
                        string value = row[0].ToString();
                        result = Convert.ToInt32(value);
                        if (result != count)
                        {
                            result = count;
                            break;
                        }
                        else
                        {
                            result = result + 1;
                        }
                        count++;
                    }
                    skip = false;
                    //int max = (int)dt.Compute("MAX([NJUNBAN])", "");
                }
                GridView1.DataSource = null;
                GridView1.DataBind();
                GridView1.DataSource = dt1;
                GridView1.DataBind();
                GridView1.Rows[0].Visible = false;

                //(GridView1.FooterRow.FindControl("txtnjunbanFooter") as TextBox).Text = result.ToString();
                (GridView1.FooterRow.FindControl("lblnjunbanFooter") as Label).Text = result.ToString(); ////20220225 Updated エインドリ－
            }
            GridView1.FooterRow.Visible = true;
        }
        #endregion

        #region btnTokuisakiSelect_Click()
        protected void btnTokuisakiSelect_Click(object sender, EventArgs e)
        {
            if (Session["cTOUKUISAKI"] != null)
            {
                string ctokuisaki = (string)Session["cTOUKUISAKI"];
                string stokuisaki = (string)Session["sTOUKUISAKI"];
                lblcShiiresaki.Text = ctokuisaki;
                lblsShiiresaki.Text = stokuisaki;
                divTokuisakiBtn.Style["display"] = "none";
                divTokuisakiLabel.Style["display"] = "block";
                upd_TOKUISAKI.Update();
                HF_isChange.Value = "1";
                updHeader.Update();

            }

            //ifSentakuPopup.Src = "";
            //mpeSentakuPopup.Hide();
            //updSentakuPopup.Update();
            ifShinkiPopup.Src = "";
            mpeShinkiPopup.Hide();
            updShinkiPopup.Update();
        }
        #endregion

        #region btnPopupClose()
        protected void btnClose_Click(object sender, EventArgs e)
        {
            ifSentakuPopup.Src = "";
            mpeSentakuPopup.Hide();
            updSentakuPopup.Update();
        }
        #endregion

        #region lblsTOKUISAKI_Click()
        protected void lblsTOKUI_Click(object sender, EventArgs e)
        {
            btn_tokuisaki_Click(sender, e);
        }
        #endregion

        #region BT_JisyaTantousya_Add_Click()
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

        #region BT_sJISHATANTOUSHA_Cross_Click()
        protected void BT_sJISHATANTOUSHA_Cross_Click(object sender, EventArgs e)
        {
            lblcJISHATANTOUSHA.Text = "";
            lblsJISHATANTOUSHA.Text = "";
            divTantousyaBtn.Style["display"] = "block";
            divTantousyaLabel.Style["display"] = "none";
            HF_isChange.Value = "1";
            updHeader.Update();
        }
        #endregion

        #region btnJishaTantouSelect_Click()
        protected void btnJishaTantouSelect_Click(object sender, EventArgs e)
        {

            if (Session["JISHAcTANTOUSHA"] != null)
            {
                string ctantou = (string)Session["JISHAcTANTOUSHA"];
                string stantou = (string)Session["JISHAsTANTOUSHA"];
                //20220225 Updated エインドリ－　Start
                if (!String.IsNullOrEmpty(ctantou))
                {
                    lblcJISHATANTOUSHA.Text = ctantou;
                    lblsJISHATANTOUSHA.Text = stantou.Replace("<", "&lt").Replace(">", "&gt");
                    divTantousyaBtn.Style["display"] = "none";
                    divTantousyaLabel.Style["display"] = "block";
                    upd_JISHATANTOUSHA.Update();
                    //HF_isChange.Value = "1";
                    //updHeader.Update();
                }
                else
                {
                    BT_sJISHATANTOUSHA_Cross_Click(sender, e);
                }
                HF_isChange.Value = "1";
                updHeader.Update();
            }
            //20220225 Updated エインドリ－　End
            ifSentakuPopup.Src = "";
            mpeSentakuPopup.Hide();
            updSentakuPopup.Update();
        }
        #endregion

        #region GridView1_RowCommand()
        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName.Equals("Update"))
                {
                    GridViewRow row = (GridViewRow)(((Control)e.CommandSource).NamingContainer);
                    int index = row.RowIndex;
                    try
                    {
                        //njun = Convert.ToInt64((GridView1.Rows[index].FindControl("txtnjunban") as TextBox).Text.Trim());
                        njun = Convert.ToInt64((GridView1.Rows[index].FindControl("lblnjunban") as Label).Text.Trim()); //20220228 Updated エインドリ－
                    }
                    catch
                    {
                        njun = 1;
                    }
                    stantou = (GridView1.Rows[index].FindControl("txtsTantousha") as TextBox).Text.Trim();
                    sbumon = (GridView1.Rows[index].FindControl("txtsBumon") as TextBox).Text.Trim();
                    tel = (GridView1.Rows[index].FindControl("txtsTel") as TextBox).Text.Trim();
                    yakusho = (GridView1.Rows[index].FindControl("txtsYakushoku") as TextBox).Text.Trim();
                    keisho = (GridView1.Rows[index].FindControl("txtsKeishou") as TextBox).Text.Trim();
                    if (njun.ToString() != "" && stantou != "")
                    {

                        GridView1.EditIndex = -1;
                        dt1 = new DataTable();
                        dt1 = (DataTable)ViewState["Row"];
                        dt1.Rows[index][0] = njun;
                        dt1.Rows[index][1] = stantou;
                        dt1.Rows[index][2] = sbumon;
                        dt1.Rows[index][3] = yakusho;
                        dt1.Rows[index][4] = tel;
                        dt1.Rows[index][5] = keisho;

                        ViewState["Row"] = dt1;
                        GridView1.DataSource = null;
                        GridView1.DataBind();
                        GridView1.DataSource = dt1;
                        GridView1.DataBind();
                        GridView1.Rows[0].Visible = false;//20220222 Added MyatNoe
                        GridView1.FooterRow.Visible = false;
                        Label njunLabel = (GridView1.Rows[0].FindControl("lblnjun") as Label);
                        //if (njunLabel.Text == "0" || String.IsNullOrEmpty(njunLabel.Text))
                        //{
                        //    Panel dropdown_Panel = (GridView1.Rows[0].FindControl("DropdownPanel") as Panel);
                        //    dropdown_Panel.Visible = false;
                        //}
                        HF_isChange.Value = "1";
                        updHeader.Update();
                    }
                }
                else if (e.CommandName.Equals("Save"))
                {
                    try
                    {
                        //njun = Convert.ToInt64((GridView1.FooterRow.FindControl("txtnjunbanFooter") as TextBox).Text.Trim());
                        njun = Convert.ToInt64((GridView1.FooterRow.FindControl("lblnjunbanFooter") as Label).Text.Trim());
                    }
                    catch
                    {
                        njun = 1;
                    }
                    stantou = (GridView1.FooterRow.FindControl("txtsTantoushaFooter") as TextBox).Text.Trim();
                    sbumon = (GridView1.FooterRow.FindControl("txtsBumonFooter") as TextBox).Text.Trim();
                    tel = (GridView1.FooterRow.FindControl("txtsTelFooter") as TextBox).Text.Trim();
                    yakusho = (GridView1.FooterRow.FindControl("txtsYakushokuFooter") as TextBox).Text.Trim();
                    keisho = (GridView1.FooterRow.FindControl("txtsKeishouFooter") as TextBox).Text.Trim();
                    if (njun.ToString() != "" && stantou != "")
                    {
                        //dt1.Rows.Add(dt1.NewRow());
                        createnewrow();
                    }
                    HF_isChange.Value = "1";
                    updHeader.Update();
                }
            }
            catch (Exception ex)
            {
                string script = ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);

            }
        }
        #endregion

        #region btnTokuisakiDate()
        protected void btnTokuisakiDate_Click(object sender, EventArgs e)
        {
            SessionUtility.SetSession("HOME", "Master");
            ifDatePopup.Style["width"] = "300px";
            ifDatePopup.Style["height"] = "370px";
            String id = Request.QueryString["id"];
            ifDatePopup.Src = "JCHidukeSelect.aspx?id=" + id;
            mpeDatePopup.Show();

            ViewState["DATETIME"] = btnTokuisakiDate.ID;

            if (!String.IsNullOrEmpty(lbldTokuisaki.Text))
            {
                DateTime dt = DateTime.Parse(lbldTokuisaki.Text);
                Session["CALENDARDATE"] = dt.ToString("yyyy/MM/dd");
                Session["YEAR"] = dt.Year;
            }

            // 設定したラベルをクリック時、時間設定ポップアップ画面を表示する
            lbldTokuisaki.Attributes.Add("onClick", "BtnClick('MainContent_btnTokuisakiDate')");
            updDatePopup.Update();
        }
        #endregion

        #region btndTokuisakiCross()
        protected void btndTokuisakiCross_Click(object sender, EventArgs e)
        {
            lbldTokuisaki.Text = "";
            btnTokuisakiDate.Style["display"] = "block";
            divTokuisakiDate.Style["display"] = "none";
            updTokuisakiDate.Update();
            HF_isChange.Value = "1";
            updHeader.Update();
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
            ifDatePopup.Src = "";
            mpeDatePopup.Hide();
            updDatePopup.Update();
        }
        #endregion

        #region "日付サブ画面を閉じる時のフォーカス処理"
        protected void CalendarFoucs()
        {
            string strBtnID = (string)ViewState["DATETIME"];

            if (strBtnID == btnTokuisakiDate.ID)
            {
                if (btnTokuisakiDate.Style["display"] != "none")
                {
                    btnTokuisakiDate.Focus();
                }
                else
                {
                    lbldTokuisaki.Focus();
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
                if (strBtnID == btnTokuisakiDate.ID)
                {
                    TokuisakiDateDataBind(strCalendarDateTime, strBtnID);
                }

                //lblHdnAnkenTextChange.Text = "true";
            }
            CalendarFoucs();
            HF_isChange.Value = "1";
            updHeader.Update();
        }
        #endregion

        #region "見積日データバインディング処理"
        protected void TokuisakiDateDataBind(string strCalendarDateTime, string strImgbtnID)
        {
            DateTime dtDeliveryDate = DateTime.Parse(strCalendarDateTime);
            lbldTokuisaki.Text = dtDeliveryDate.ToString("yyyy/MM/dd");
            btnTokuisakiDate.Style["display"] = "none";
            divTokuisakiDate.Style["display"] = "block";
            updTokuisakiDate.Update();
        }
        #endregion

        #region BT_sTOKUISAKI_Cross()
        protected void BT_sTOKUISAKI_Cross_Click(object sender, EventArgs e)
        {
            lblcShiiresaki.Text = "";
            lblsShiiresaki.Text = "";
            divTokuisakiBtn.Style["display"] = "block";
            divTokuisakiLabel.Style["display"] = "none";
            HF_isChange.Value = "1";
            updHeader.Update();
        }
        #endregion

        #region btnshinki_Click
        protected void btnshinki_Click(object sender, EventArgs e)
        {
            if (HF_isChange.Value == "1")
            {
                updHeader.Update();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowAnkenChangeMessage",
                "ShowKoumokuChangesConfirmMessage('項目が変更されています。保存しますか？','" + btnYes.ClientID + "','" + btnNo.ClientID + "','" + btnCancel.ClientID + "');", true);
                //}
            }
            else
            {
                Session["cTokuisakiBukken"] = null;
                if (!String.IsNullOrEmpty(Request.QueryString["id"]))
                {
                    String id = Request.QueryString["id"];
                    Response.Redirect("JC19TokuisakiSyousai.aspx?id=" + id);
                }
                else
                {
                    Response.Redirect("JC01Login.aspx");
                }
            }
            //Session["cTokuisakiBukken"] = null;
            //Response.Redirect("JC19TokuisakiSyousai.aspx");
        }
        #endregion

        #region GridView1_RowEditing()
        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            dt1 = new DataTable();
            dt1 = (DataTable)ViewState["Row"];
            if (dt1.Rows[0][1].ToString() != "")
            {
                DataRow dr1 = dt1.NewRow();
                dr1["NJUNBAN"] = 0;
                dr1["STANTOU"] = "";
                dr1["SBUMON"] = "";
                dr1["SYAKUSHOKU"] = "";
                dr1["STEL"] = "";
                dr1["SKEISHOU"] = "";
                dt1.Rows.Add(dr1);
            }
            ViewState["Row"] = dt1;
            GridView1.DataSource = dt1;
            GridView1.DataBind();
            GridView1.Rows[0].Visible = false;//20220223 Added MyatNoe
            GridView1.FooterRow.Visible = false;
            Label njun = (GridView1.Rows[0].FindControl("lblnjun") as Label);
            //if (njun.Text == "0" || String.IsNullOrEmpty(njun.Text))
            //{
            //    Panel dropdown_Panel = (GridView1.Rows[0].FindControl("DropdownPanel") as Panel);
            //    dropdown_Panel.Visible = false;
            //}
        }

        protected void GridView1_RowUpdated(object sender, GridViewUpdatedEventArgs e)
        {

        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridView1.EditIndex = -1;
            dt1 = new DataTable();
            if ((DataTable)ViewState["Row"] == null)
            {
                dt1.Columns.Add("NJUNBAN", typeof(int));
                dt1.Columns.Add("STANTOU", typeof(string));
                dt1.Columns.Add("SBUMON", typeof(string));
                dt1.Columns.Add("SYAKUSHOKU", typeof(string));
                dt1.Columns.Add("STEL", typeof(string));
                dt1.Columns.Add("SKEISHOU", typeof(string));
            }
            else
            {
                dt1 = (DataTable)ViewState["Row"];
            }
            ViewState["Row"] = dt1;
            GridView1.DataSource = dt1;
            GridView1.DataBind();
            GridView1.Rows[0].Visible = false;//20220223 Added MyatNoe
            GridView1.FooterRow.Visible = false;
            Label njun = (GridView1.Rows[0].FindControl("lblnjun") as Label);
            //if (njun.Text == "0" || String.IsNullOrEmpty(njun.Text))
            //{
            //    Panel dropdown_Panel = (GridView1.Rows[0].FindControl("DropdownPanel") as Panel);
            //    dropdown_Panel.Visible = false;
            //}
        }
        #endregion

        #region GridView1_RowCancelingEdit()
        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            dt1 = new DataTable();
            if ((DataTable)ViewState["Row"] == null)
            {
                dt1.Columns.Add("NJUNBAN", typeof(int));
                dt1.Columns.Add("STANTOU", typeof(string));
                dt1.Columns.Add("SBUMON", typeof(string));
                dt1.Columns.Add("SYAKUSHOKU", typeof(string));
                dt1.Columns.Add("STEL", typeof(string));
                dt1.Columns.Add("SKEISHOU", typeof(string));
            }
            else
            {
                dt1 = (DataTable)ViewState["Row"];
            }
            ViewState["Row"] = dt1;
            GridView1.DataSource = dt1;
            GridView1.DataBind();
            GridView1.Rows[0].Visible = false;//20220222 Added MyatNoe
            GridView1.FooterRow.Visible = false;
            Label njun = (GridView1.Rows[0].FindControl("lblnjun") as Label);
            //if (njun.Text == "0" || String.IsNullOrEmpty(njun.Text))
            //{
            //    Panel dropdown_Panel = (GridView1.Rows[0].FindControl("DropdownPanel") as Panel);
            //    dropdown_Panel.Visible = false;
            //}

        }
        #endregion

        #region GridView1_RowDeleting()
        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            DataTable dt2 = (DataTable)ViewState["Row"];
            dt2.DefaultView.Sort = "NJUNBAN ASC";
            dt2 = dt2.DefaultView.ToTable();
            if (dt2.Rows.Count > 0)
            {
                //    dt2.Rows[e.RowIndex].Delete();
                //    ViewState["Row"] = dt2;
                //    GridView1.DataSource = dt2;
                //    GridView1.DataBind();

                int index = Convert.ToInt32(e.RowIndex);
                dt2.Rows[index].Delete();
                dt2.AcceptChanges();
                ViewState["Row"] = dt2;
                GridView1.DataSource = dt2;
                GridView1.DataBind();
                GridView1.Rows[0].Visible = false;//20220223 Added MyatNoe
                Label njun = (GridView1.Rows[0].FindControl("lblnjun") as Label);
                //if (njun.Text == "0" || String.IsNullOrEmpty(njun.Text))
                //{
                //    Panel dropdown_Panel = (GridView1.Rows[0].FindControl("DropdownPanel") as Panel);
                //    dropdown_Panel.Visible = false;
                //}
            }
            HF_isChange.Value = "1";
            updHeader.Update();
        }
        #endregion

        #region lnkBukkenToroku_Click
        protected void lnkBukkenToroku_Click(object sender, EventArgs e)
        {
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
        #endregion

        #region lnkMitumoriToroku_Click
        protected void lnkMitumoriToroku_Click(object sender, EventArgs e)
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
        #endregion

        #region txtsTouisaki_TextChanged
        protected void txtsTouisaki_TextChanged(object sender, EventArgs e)
        {
            //txtsTouisaki.BorderColor = System.Drawing.Color.LightGray;
            txtsTouisaki.Style["border-color"] = "none";
            txtsTouisaki.BorderStyle = BorderStyle.Solid;
            txtsTouisaki.BorderWidth = 1;
            HF_isChange.Value = "1";
            updHeader.Update();

        }
        #endregion

        #region lnkbtnSetting_Click
        protected void lnkbtnSetting_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(Request.QueryString["id"]))
            {
                String id = Request.QueryString["id"];
                Response.Redirect("JC26Setting.aspx?id=" + id);
            }
            else
            {
                Response.Redirect("JC01Login.aspx");
            }
        }
        #endregion

        #region GridView1_Sorting()
        protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
        {
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
            GridView1.Rows[0].Visible = false;//20220222 Added MyatNoe
        }

        private void SortGridView(string sortExpression, string direction)
        {
            dt.Clear();

            dt = (DataTable)ViewState["Row"];
            dt.DefaultView.Sort = "NJUNBAN ASC";
            dt.AcceptChanges();
            dt1 = dt.Copy();
            if (dt.Rows[0][1].ToString() == "")
            {
                dt.Rows.Remove(dt.Rows[0]);
            }
            DataView dv = new DataView(dt);
            dv.Sort = sortExpression + " " + direction;
            dt1.Rows.Clear();
            DataRow dr1 = dt1.NewRow();
            dr1["NJUNBAN"] = 0;
            dr1["STANTOU"] = "";
            dr1["SBUMON"] = "";
            dr1["SYAKUSHOKU"] = "";
            dr1["STEL"] = "";
            dr1["SKEISHOU"] = "";
            dt1.Rows.Add(dr1);
            foreach (DataRow dr in dv.ToTable().Rows)
            {
                dt1.Rows.Add(dr.ItemArray);
            }
            ViewState["Row"] = dt1;
            this.GridView1.DataSource = dt1;
            GridView1.DataBind();
            Label njun = (GridView1.Rows[0].FindControl("lblnjun") as Label);
            //if (njun.Text == "0" || String.IsNullOrEmpty(njun.Text))
            //{
            //    Panel dropdown_Panel = (GridView1.Rows[0].FindControl("DropdownPanel") as Panel);
            //    dropdown_Panel.Visible = false;
            //}
        }

        public string SortExpression
        {
            get
            {
                if (ViewState["z_sortexpresion"] == null)
                    ViewState["z_sortexpresion"] = this.GridView1.DataKeyNames[0].ToString();
                return ViewState["z_sortexpresion"].ToString();
            }
            set
            {
                ViewState["z_sortexpresion"] = value;
            }
        }

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

        #region lnkMitumoriDirectCreate_Click
        protected void lnkMitumoriDirectCreate_Click(object sender, EventArgs e)
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

        protected void btCancel_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "parentButtonClick('btn_CloseShinkiTokui','" + hdnHome.Value + "');", true);
        }

        #endregion

        #region lnkbtnKojinJouhou_Click
        protected void lnkbtnKojinJouhou_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(Request.QueryString["id"]))
            {
                String id = Request.QueryString["id"];
                Response.Redirect("JC16KoujinJougouSetting.aspx?id=" + id);
            }
            else
            {
                Response.Redirect("JC01Login.aspx");
            }
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

        #region lnkMitumoriTaCopyCreate_Click
        protected void lnkMitumoriTaCopyCreate_Click(object sender, EventArgs e)
        {
            SessionUtility.SetSession("HOME", "Master");

            String id = Request.QueryString["id"];
            ifShinkiPopup.Src = "JC12MitsumoriKensaku.aspx?id=" + id;
            mpeShinkiPopup.Show();
            updShinkiPopup.Update();
        }
        #endregion

        #region lnkbtnHome_Click
        protected void lnkbtnHome_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(Request.QueryString["id"]))
            {
                String id = Request.QueryString["id"];
                Response.Redirect("JC07Home.aspx?id=" + id);
            }
            else
            {
                Response.Redirect("JC01Login.aspx");
            }
        }
        #endregion

        #region lnkbtnLogOut_Click
        protected void lnkbtnLogOut_Click(object sender, EventArgs e)
        {
            Response.Redirect("JC01Login.aspx");
        }
        #endregion

        #region GridView1_RowDataBound1
        protected void GridView1_RowDataBound1(object sender, GridViewRowEventArgs e)
        {
            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //Label njun = (e.Row.FindControl("lblnjun") as Label);
            //if (njun.Text == "0" || String.IsNullOrEmpty(njun.Text))
            //{
            //    Panel dropdown_Panel = (e.Row.FindControl("DropdownPanel") as Panel);
            //    dropdown_Panel.Visible = false;
            //}
            //}

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                try
                {

                    Label lbl_sTantou = (e.Row.FindControl("STANTOU") as Label);
                    String sTantou = lbl_sTantou.Text;
                    sTantou = sTantou.Replace("<", "&lt").Replace(">", "&gt");
                    lbl_sTantou.Text = sTantou;

                    Label lbl_sBumon = (e.Row.FindControl("SBUMON") as Label);
                    String sBumon = lbl_sBumon.Text;
                    sBumon = sBumon.Replace("<", "&lt").Replace(">", "&gt");
                    lbl_sBumon.Text = sBumon;

                    Label lbl_sYakushoku = (e.Row.FindControl("SYAKUSHOKU") as Label);
                    String sYakushoku = lbl_sYakushoku.Text;
                    sYakushoku = sYakushoku.Replace("<", "&lt").Replace(">", "&gt");
                    lbl_sYakushoku.Text = sYakushoku;

                    Label lbl_sKeishou = (e.Row.FindControl("SKEISHOU") as Label);
                    String sKeishou = lbl_sKeishou.Text;
                    sKeishou = sKeishou.Replace("<", "&lt").Replace(">", "&gt");
                    lbl_sKeishou.Text = sKeishou;
                }
                catch { }
            }
        }
        #endregion

        #region txtcYUUBIN_TextChanged
        protected void txtcYUUBIN_TextChanged(object sender, EventArgs e)
        {
            HF_isChange.Value = "1";
            updHeader.Update();
        }
        #endregion

        #region txtsJUUSHO1_TextChanged
        protected void txtsJUUSHO1_TextChanged(object sender, EventArgs e)
        {
            HF_isChange.Value = "1";
            updHeader.Update();
        }
        #endregion

        #region txtsJUUSHO2_TextChanged
        protected void txtsJUUSHO2_TextChanged(object sender, EventArgs e)
        {
            HF_isChange.Value = "1";
            updHeader.Update();
        }
        #endregion

        #region txtsFAX_TextChanged
        protected void txtsFAX_TextChanged(object sender, EventArgs e)
        {
            HF_isChange.Value = "1";
            updHeader.Update();
        }
        #endregion

        #region txtsTOKKIJIKOU_TextChanged
        protected void txtsTOKKIJIKOU_TextChanged(object sender, EventArgs e)
        {
            HF_isChange.Value = "1";
            updHeader.Update();
        }
        #endregion

        #region chkdelete_Clicked
        protected void chkdelete_Clicked(object sender, EventArgs e)
        {
            HF_isChange.Value = "1";
            updHeader.Update();
        }
        #endregion

        #region BT_LBSaveCross_Click
        protected void BT_LBSaveCross_Click(object sender, EventArgs e)
        {
            divLabelSave.Style["display"] = "none";
        }
        #endregion

        #region 確認メッセージの「はい」ボタン
        protected void btnYes_Click(object sender, EventArgs e)
        {
            btnhozon_Click(sender, e);
            if (HF_Save.Value == "1")
            {
                Session["cTokuisakiBukken"] = null;
                if (!String.IsNullOrEmpty(Request.QueryString["id"]))
                {
                    String id = Request.QueryString["id"];
                    Response.Redirect("JC19TokuisakiSyousai.aspx?id=" + id);
                }
                else
                {
                    Response.Redirect("JC01Login.aspx");
                }
            }
        }
        #endregion

        #region 確認メッセージの「いいえ」ボタン
        protected void btnNo_Click(object sender, EventArgs e)
        {
            Session["cTokuisakiBukken"] = null;
            if (!String.IsNullOrEmpty(Request.QueryString["id"]))
            {
                String id = Request.QueryString["id"];
                Response.Redirect("JC19TokuisakiSyousai.aspx?id=" + id);
            }
            else
            {
                Response.Redirect("JC01Login.aspx");
            }
        }
        #endregion

        #region grid_RowCreated //sorting arrow 20220223 MyatNoe added 
        protected void grid_RowCreated(object sender, GridViewRowEventArgs e)
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
                                img.ID = sortLinkButton.CommandArgument;
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
        
        #region BT_ColumnWidth_Click
        protected void BT_ColumnWidth_Click(object sender, EventArgs e)
        {
            Response.Cookies["colWidthbTokuisaki"].Value = HF_GridSize.Value;
            Response.Cookies["colWidthbTokuisaki"].Expires = DateTime.Now.AddYears(1);
            Response.Cookies["colWidthbTokuisakiGrid"].Value = HF_Grid.Value;
            Response.Cookies["colWidthbTokuisakiGrid"].Expires = DateTime.Now.AddYears(1);
        }
        #endregion

        #region btnToLogin_Click
        protected void btnToLogin_Click(object sender, EventArgs e)
        {
            Response.Redirect("JC01Login.aspx");
        }
        #endregion

    }
}