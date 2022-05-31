using Common;
using MySql.Data.MySqlClient;
using Service;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace jobzcolud.WebFront
{
    public partial class ShiresakiMaster : System.Web.UI.Page
    {
        MySqlConnection cn = null;
        MySqlCommand cmd = null;
        DataTable dt = new DataTable();
        DataTable dt1 = new DataTable();
        Int64 njun;
        string stantou, sbumon, tel, yakusho = "";
        string tantouCarry;
        bool f_isomoji_msg = true;

        #region GV_ShiiresakiMaster_RowCancelingEdit()
        protected void GV_ShiiresakiMaster_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GV_ShiiresakiMaster.EditIndex = -1;
            dt1 = new DataTable();
            if ((DataTable)ViewState["Row"] == null)
            {
                dt1.Columns.Add("NJUNBAN", typeof(int));
                dt1.Columns.Add("SBUMON", typeof(string));
                dt1.Columns.Add("SYAKUSHOKU", typeof(string));
                dt1.Columns.Add("STEL", typeof(string));
                dt1.Columns.Add("STANTOU", typeof(string));
            }
            else
            {
                dt1 = (DataTable)ViewState["Row"];
            }
            ViewState["Row"] = dt1;
            GV_ShiiresakiMaster.DataSource = dt1;
            GV_ShiiresakiMaster.DataBind();
            GV_ShiiresakiMaster.Rows[0].Visible = false;
            GV_ShiiresakiMaster.FooterRow.Visible = false;
            Label njun = (GV_ShiiresakiMaster.Rows[0].FindControl("lblnjun") as Label);

        }
        #endregion

        #region GV_ShiiresakiMaster_RowEditing()
        protected void GV_ShiiresakiMaster_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GV_ShiiresakiMaster.EditIndex = e.NewEditIndex;
            dt1 = new DataTable();
            dt1 = (DataTable)ViewState["Row"];
            if (dt1.Rows[0][1].ToString() != "")
            {
                DataRow dr1 = dt1.NewRow();
                dr1["NJUNBAN"] = 0;
                dr1["SBUMON"] = "";
                dr1["SYAKUSHOKU"] = "";
                dr1["STEL"] = "";
                dr1["STANTOU"] = "";
                dt1.Rows.Add(dr1);
            }
            ViewState["Row"] = dt1;
            GV_ShiiresakiMaster.DataSource = dt1;
            GV_ShiiresakiMaster.DataBind();
            GV_ShiiresakiMaster.Rows[0].Visible = false;
            GV_ShiiresakiMaster.FooterRow.Visible = false;
            Label njun = (GV_ShiiresakiMaster.Rows[0].FindControl("lblnjun") as Label);
        }
        #endregion

        #region GV_ShiiresakiMaster_RowUpdating()
        protected void GV_ShiiresakiMaster_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GV_ShiiresakiMaster.EditIndex = -1;
            dt1 = new DataTable();
            if ((DataTable)ViewState["Row"] == null)
            {
                dt1.Columns.Add("NJUNBAN", typeof(int));
                dt1.Columns.Add("SBUMON", typeof(string));
                dt1.Columns.Add("SYAKUSHOKU", typeof(string));
                dt1.Columns.Add("STEL", typeof(string));
                dt1.Columns.Add("STANTOU", typeof(string));
            }
            else
            {
                dt1 = (DataTable)ViewState["Row"];
            }
            ViewState["Row"] = dt1;
            GV_ShiiresakiMaster.DataSource = dt1;
            GV_ShiiresakiMaster.DataBind();
            GV_ShiiresakiMaster.Rows[0].Visible = false;
            GV_ShiiresakiMaster.FooterRow.Visible = false;
            Label njun = (GV_ShiiresakiMaster.Rows[0].FindControl("lblnjun") as Label);
        }
        #endregion

        #region GV_ShiiresakiMaster_RowDeleting()
        protected void GV_ShiiresakiMaster_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            DataTable dt2 = (DataTable)ViewState["Row"];
            dt2.DefaultView.Sort = "NJUNBAN ASC";
            dt2 = dt2.DefaultView.ToTable();
            if (dt2.Rows.Count > 0)
            {
                int index = Convert.ToInt32(e.RowIndex);
                dt2.Rows[index].Delete();
                dt2.AcceptChanges();
                ViewState["Row"] = dt2;
                GV_ShiiresakiMaster.DataSource = dt2;
                GV_ShiiresakiMaster.DataBind();
                GV_ShiiresakiMaster.Rows[0].Visible = false;
                Label njun = (GV_ShiiresakiMaster.Rows[0].FindControl("lblnjun") as Label);
            }
            HF_isChange.Value = "1";
            updHeader.Update();
        }
        #endregion

        #region GV_ShiiresakiMaster_RowCommand()
        protected void GV_ShiiresakiMaster_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName.Equals("Update"))
                {
                    GridViewRow row = (GridViewRow)(((Control)e.CommandSource).NamingContainer);
                    int index = row.RowIndex;
                    try
                    {
                        njun = Convert.ToInt64((GV_ShiiresakiMaster.Rows[index].FindControl("lblnjunban") as Label).Text.Trim());
                    }
                    catch
                    {
                        njun = 1;
                    }
                    stantou = (GV_ShiiresakiMaster.Rows[index].FindControl("txtsTantousha") as TextBox).Text.Trim();
                    sbumon = (GV_ShiiresakiMaster.Rows[index].FindControl("txtsBumon") as TextBox).Text.Trim();
                    tel = (GV_ShiiresakiMaster.Rows[index].FindControl("txtsTel") as TextBox).Text.Trim();
                    yakusho = (GV_ShiiresakiMaster.Rows[index].FindControl("txtsYakushoku") as TextBox).Text.Trim();
                    if (njun.ToString() != "" && stantou != "")
                    {

                        GV_ShiiresakiMaster.EditIndex = -1;
                        dt1 = new DataTable();
                        dt1 = (DataTable)ViewState["Row"];
                        dt1.Rows[index][0] = njun;
                        dt1.Rows[index][1] = sbumon;
                        dt1.Rows[index][2] = yakusho;
                        dt1.Rows[index][3] = tel;
                        dt1.Rows[index][4] = stantou;

                        ViewState["Row"] = dt1;
                        GV_ShiiresakiMaster.DataSource = null;
                        GV_ShiiresakiMaster.DataBind();
                        GV_ShiiresakiMaster.DataSource = dt1;
                        GV_ShiiresakiMaster.DataBind();
                        GV_ShiiresakiMaster.Rows[0].Visible = false;
                        GV_ShiiresakiMaster.FooterRow.Visible = false;
                        Label njunLabel = (GV_ShiiresakiMaster.Rows[0].FindControl("lblnjun") as Label);
                        HF_isChange.Value = "1";
                        updHeader.Update();
                    }
                }
                else if (e.CommandName.Equals("Save"))
                {
                    try
                    {
                        njun = Convert.ToInt64((GV_ShiiresakiMaster.FooterRow.FindControl("lblnjunbanFooter") as Label).Text.Trim());
                    }
                    catch
                    {
                        njun = 1;
                    }
                    stantou = (GV_ShiiresakiMaster.FooterRow.FindControl("txtsTantoushaFooter") as TextBox).Text.Trim();
                    sbumon = (GV_ShiiresakiMaster.FooterRow.FindControl("txtsBumonFooter") as TextBox).Text.Trim();
                    tel = (GV_ShiiresakiMaster.FooterRow.FindControl("txtsTelFooter") as TextBox).Text.Trim();
                    yakusho = (GV_ShiiresakiMaster.FooterRow.FindControl("txtsYakushokuFooter") as TextBox).Text.Trim();
                    if (njun.ToString() != "" && stantou != "")
                    {
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

        #region btnaddNewrow_Click()
        protected void btnaddNewrow_Click(object sender, EventArgs e)
        {

            if ((DataTable)ViewState["Row"] == null || ((DataTable)ViewState["Row"]).Rows.Count == 0 || ((DataTable)ViewState["Row"]).Rows.Count == 1)
            {
                createnewrow();

                GV_ShiiresakiMaster.FooterRow.Visible = false;

                (GV_ShiiresakiMaster.FooterRow.FindControl("lblnjunbanFooter") as Label).Text = 1.ToString();
            }
            else
            {
                int count = 1;
                int result = 0;
                bool skip = true;
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
                }

                (GV_ShiiresakiMaster.FooterRow.FindControl("lblnjunbanFooter") as Label).Text = result.ToString();
            }
            GV_ShiiresakiMaster.FooterRow.Visible = true;
        }
        #endregion

        #region PageLoad()
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(Request.QueryString["id"]))
                {
                    if (!Page.IsPostBack)
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
                        //BT_Delete.Visible = false;
                        div3.Visible = true;
                        divPopupHeader.Visible = false;
                        btnCancel.Visible = false;
                        DivLine.Visible = false;
                        ddl_shimebi();
                        ddl_harai();
                        ddl_Yan();
                        if (Session["fSyouhinSyosai"] != null)
                        {
                            if (Session["fSyouhinSyosai"].ToString() == "Popup")
                            {
                                if (SessionUtility.GetSession("HOME") != null)
                                {
                                    hdnHome.Value = SessionUtility.GetSession("HOME").ToString();
                                    SessionUtility.SetSession("HOME", null);
                                }
                                HF_flag.Value = Session["fSyouhinSyosai"].ToString();
                                navbar_Master.div_Nav.Visible = false;
                                navbar_Master.div2.Visible = false;
                                BD_Shiiresaki.Style.Add("background-color", "transparent");
                                navbar_Master.divContent.Style.Add("background-color", "transparent");
                                navbar_Master.form1.Style.Add("background-color", "transparent");
                                navbar_Master.BD_Master.Style.Add("background-color", "transparent");
                                Div_Body.Style.Add("background-color", "transparent");
                                BT_Shinki.Visible = false;
                                div3.Visible = false;
                                divPopupHeader.Visible = true;
                                btCancel.Visible = true;
                                DivLine.Visible = true;

                            }
                        }
                        if (Session["cSHIIRESAKI"] != null)
                        {
                            if (Request.Cookies["colWidthbTokuisaki"] != null)
                            {
                                HF_GridSize.Value = Request.Cookies["colWidthbTokuisaki"].Value;
                            }
                            else
                            {
                                HF_GridSize.Value = "70, 335, 125, 125, 100, 100, 214";
                            }
                            if (Request.Cookies["colWidthbTokuisakiGrid"] != null)
                            {
                                HF_Grid.Value = Request.Cookies["colWidthbTokuisakiGrid"].Value;
                            }
                            else
                            {
                                HF_Grid.Value = "1093";
                            }
                            String cSHIIRESAK = Session["cSHIIRESAKI"].ToString();
                            LB_ShiresakiCode.Text = cSHIIRESAK;
                            SetShiiresakiData();
                            BindGridview();
                            GV_ShiiresakiMaster.Rows[0].Visible = false;
                            GV_ShiiresakiMaster.FooterRow.Visible = false;
                        }
                        else
                        {
                            createnewrow();
                            HF_isChange.Value = "1";
                            updHeader.Update();
                        }
                        if (divLabelSave.Style["display"] != "none")
                        {
                            divLabelSave.Style["display"] = "none";
                            updHeader.Update();
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

        #region DDL_Shimebi
        public void ddl_shimebi()
        {
            DataTable dtt = new DataTable();
            dtt.Columns.Add("cSHIMEBI", typeof(string));
            dtt.Columns.Add("sSHIMEBI", typeof(string));
            dtt.Rows.Add("0", "選択して");
            dtt.Rows.Add("1", "未");
            for (int i = 28; i >= 1; i--)
            {
                dtt.Rows.Add(i, i);
            }
            DDL_shimebi.DataSource = dtt;
            DDL_shimebi.DataTextField = "sSHIMEBI";
            DDL_shimebi.DataValueField = "sSHIMEBI";//updated 20220324
            DDL_shimebi.DataBind();
        }

        #endregion

        #region DDL_harai
        public void ddl_harai()
        {
            //Updated start 20220324 by テタ
            DataTable dtt = new DataTable();
            dtt.Columns.Add("charai", typeof(string));
            dtt.Columns.Add("sharai", typeof(string));
            dtt.Rows.Add("選択してください", "選択してください");
            dtt.Rows.Add("現金", "現金");
            dtt.Rows.Add("振込", "振込");
            dtt.Rows.Add("小切手", "小切手");
            dtt.Rows.Add("手形", "手形");
            dtt.Rows.Add("相殺", "相殺");
            dtt.Rows.Add("その他", "その他");
            //end 20220324
            DDL_kihonshiharai.DataSource = dtt;
            DDL_kihonshiharai.DataTextField = "sharai";
            DDL_kihonshiharai.DataValueField = "charai";
            DDL_kihonshiharai.DataBind();

            DDL_kihonshiharai1.DataSource = dtt;
            DDL_kihonshiharai1.DataTextField = "sharai";
            DDL_kihonshiharai1.DataValueField = "charai";
            DDL_kihonshiharai1.DataBind();

            DDL_kakuchoshiharai.DataSource = dtt;
            DDL_kakuchoshiharai.DataTextField = "sharai";
            DDL_kakuchoshiharai.DataValueField = "charai";
            DDL_kakuchoshiharai.DataBind();

            DDL_kakuchoshiharai1.DataSource = dtt;
            DDL_kakuchoshiharai1.DataTextField = "sharai";
            DDL_kakuchoshiharai1.DataValueField = "charai";
            DDL_kakuchoshiharai1.DataBind();

        }

        #endregion

        #region DDL_Yan
        public void ddl_Yan()
        {
            //Updated start 20220324 by テタ
            DataTable dtt = new DataTable();
            dtt.Columns.Add("cYan", typeof(string));
            dtt.Columns.Add("sYan", typeof(string));
            dtt.Rows.Add("選択してください", "選択してください");
            dtt.Rows.Add("以上", "以上");
            dtt.Rows.Add("以下", "以下");
            dtt.Rows.Add("未満", "未満");
            dtt.Rows.Add("を超える", "を超える");
            //end 20220324
            DDL_Yan.DataSource = dtt;
            DDL_Yan.DataTextField = "sYan";
            DDL_Yan.DataValueField = "cYan";
            DDL_Yan.DataBind();
        }
        #endregion

        #region TB_sShiresaki_TextChanged()
        protected void TB_sShiresaki_TextChanged(object sender, EventArgs e)
        {
            TB_sShiresaki.BorderColor = System.Drawing.Color.LightGray;
            TB_sShiresaki.BorderStyle = BorderStyle.Solid;
            TB_sShiresaki.BorderWidth = 1;
            HF_isChange.Value = "1";
            updHeader.Update();
        }
        #endregion

        #region BT_Save()
        protected void BT_Save_Click(object sender, EventArgs e)
        {
            Boolean saveSuccess = false;
            if (LB_ShiresakiCode.Text == "")
            {
                String id = Request.QueryString["id"];
                DataTable dt_db = JC01Login_Class.Get_DB(id);
                String db = dt_db.Rows[0]["db"].ToString();
                String mail = dt_db.Rows[0]["mail"].ToString();
                ConstantVal.DB_NAME = db;

                JC_ClientConnecction_Class jc = new JC_ClientConnecction_Class();
                jc.loginId = mail;
                cn = jc.GetConnection();
                DataTable dt_cSHIIRESAKI = getcSHIIRESAKI();
                Int64 cSHIIRESAKI = jc.GetMissingCode(dt_cSHIIRESAKI, 9999999);
                String code = cSHIIRESAKI.ToString().PadLeft(7, '0');
                String sSHIIRESAKI = TB_sShiresaki.Text.Trim().Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
                String skSHIIRESAKI = TB_skShiresaki.Text.Trim().Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
                String sSHIIRESAKI_R = TB_sShiresaki_R.Text.Trim().Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
                String cYuubin = TB_cYuubin.Text.Trim().Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
                String sJUUSHO1 = TB_Juusho1.Text.Trim().Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
                String sJUUSHO2 = TB_Juusho2.Text.Trim().Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
                String sTel = TB_sTel.Text.Trim().Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
                String sFax = TB_sFAX.Text.Trim().Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
                String dHenkou = DateTime.Now.ToString();
                //Updated start 20220324 by テタ
                String nki_hiritsu1 = "0";
                if (!String.IsNullOrEmpty(TB_kihonvalue.Text))
                {
                    nki_hiritsu1 = TB_kihonvalue.Text;
                }
                String nki_hiritsu2 = "0";
                if (!String.IsNullOrEmpty(TB_kihonvalue1.Text))
                {
                    nki_hiritsu2 = TB_kihonvalue1.Text;
                }
                String nka_hiritsu1 = "0";
                if (!String.IsNullOrEmpty(TB_kakuchovalue.Text))
                {
                    nka_hiritsu1 = TB_kakuchovalue.Text;
                }
                String nka_hiritsu2 = "0";
                if (!String.IsNullOrEmpty(TB_kakuchovalue1.Text))
                {
                    nka_hiritsu2 = TB_kakuchovalue1.Text;
                }

                String shimebi = null;
                if (DDL_shimebi.SelectedIndex != 0)
                {
                    shimebi = DDL_shimebi.SelectedItem.Text;
                }
                String skihonharai1 = null;
                if (DDL_kihonshiharai.SelectedIndex != 0)
                {
                    skihonharai1 = DDL_kihonshiharai.SelectedItem.Text;
                }
                String skihonharai2 = null;
                if (DDL_kihonshiharai1.SelectedIndex != 0)
                {
                    skihonharai2 = DDL_kihonshiharai1.SelectedItem.Text;
                }
                String skakuchoharai1 = null;
                if (DDL_kakuchoshiharai.SelectedIndex != 0)
                {
                    skakuchoharai1 = DDL_kakuchoshiharai.SelectedItem.Text;
                }

                String skakuchoharai2 = null;
                if (DDL_kakuchoshiharai1.SelectedIndex != 0)
                {
                    skakuchoharai2 = DDL_kakuchoshiharai1.SelectedItem.Text;
                }

                String fkakuchouharai = "0";
                if (chk_kakuchoshiharai.Checked)
                {
                    fkakuchouharai = "1";
                }
                String nte_kingaku = "0";
                if (!String.IsNullOrEmpty(TB_TekiyoJouken.Text))
                {
                    nte_kingaku = TB_TekiyoJouken.Text.Replace(",", "");
                }

                String stekiyoujouken = null;
                if (DDL_Yan.SelectedIndex != 0)
                {
                    stekiyoujouken = DDL_Yan.SelectedItem.Text;
                }
                //end 20220324 
                String sJIkou = TB_sTOKKIJIKOU.Text.Trim().Replace("\r\n", "").Replace("\r", "").Replace("\n", "");

                if (sSHIIRESAKI != "")
                {
                    if (lblPhoneError.Text == "")
                    {

                        TB_sShiresaki.BorderColor = System.Drawing.Color.LightGray;
                        TB_sShiresaki.BorderStyle = BorderStyle.Solid;
                        TB_sShiresaki.BorderWidth = 1;

                        TB_sTel.Style["border-color"] = "none";
                        TB_sTel.BorderStyle = BorderStyle.Solid;
                        TB_sTel.BorderWidth = 1;

                        if (TextUtility.isomojiCharacter(sSHIIRESAKI))
                        {
                            f_isomoji_msg = false;
                        }
                        sSHIIRESAKI = sSHIIRESAKI.Replace("\\", "\\\\").Replace("'", "\\'");  //仕入先名 

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
                            String sql = "INSERT INTO m_shiiresaki (cSHIIRESAKI, sSHIIRESAKI, skSHIIRESAKI, sSHIIRESAKI_R, skSHIIRESAKI_R, cYUUBIN, sJUUSHO1,"
                       + " sJUUSHO2, sTEL, sFAX, dHENKOU, cHENKOUSYA, sSHIMEBI, skihonharai1, nkihonharai_hiritsu1, skihonharai2, nkihonharai_hiritsu2, skakuchouharai1,"
                       + " nkakuchouharai_hiritsu1, skakuchouharai2, nkakuchouharai_hiritsu2, fkakuchouharai,ntekiyoujouken_kingaku, stekiyoujouken, sTOKKIJIKOU)"
                       + " VALUES ('" + code + "', '" + sSHIIRESAKI + "', '" + skSHIIRESAKI + "', '" + sSHIIRESAKI_R + "', '', '" + cYuubin + "', '" + sJUUSHO1 + "'"
                       + " , '" + sJUUSHO2 + "', '" + sTel + "', '" + sFax + "', '" + dHenkou + "', '" + lblLoginUserCode.Text + "', '" + shimebi + "', '" + skihonharai1 + "', '" + nki_hiritsu1 + "', '" + skihonharai2 + "', '" + nki_hiritsu2 + "', '" + skakuchoharai1 + "',"
                       + "  '" + nka_hiritsu1 + "', '" + skakuchoharai2 + "','" + nka_hiritsu2 + "','" + fkakuchouharai + "',"
                       + "  '" + nte_kingaku + "','" + stekiyoujouken + "', '" + sJIkou + "');";

                            String sql_tantouinsert = "";
                            for (int i = 1; i < GV_ShiiresakiMaster.Rows.Count; i++)
                            {
                                String sbumon = (GV_ShiiresakiMaster.Rows[i].FindControl("SBUMON") as Label).Text.Trim().Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
                                String stantou = (GV_ShiiresakiMaster.Rows[i].FindControl("STANTOU") as Label).Text.Trim().Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
                                tantouCarry = (GV_ShiiresakiMaster.Rows[i].FindControl("STANTOU") as Label).Text.Trim().Replace("\r\n", "").Replace("\r", "").Replace("\n", "").Replace("&lt", "<").Replace("&gt", ">");
                                String syakusho = (GV_ShiiresakiMaster.Rows[i].FindControl("SYAKUSHOKU") as Label).Text.Trim().Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
                                String stel = (GV_ShiiresakiMaster.Rows[i].FindControl("STEL") as Label).Text.Trim().Replace("\r\n", "").Replace("\r", "").Replace("\n", "");

                                sql_tantouinsert += "INSERT INTO m_shiiresaki_tantousha (cSHIIRESAKI, NJUNBAN, SBUMON, STANTOU, SYAKUSHOKU, STEL, SFAX, SMAIL, FMAIL, SBIKOU, SKEISHOU)" +
                                " VALUES ('" + code + "', '" + i + "', '" + sbumon + "', '" + stantou + "', '" + syakusho + "', '" + stel + "', '', '', '', '', '');";
                            }
                            #region TantouCarry
                            if (GV_ShiiresakiMaster.Rows.Count == 2)
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
                                divLabelSave.Style["display"] = "flex";//「保存しました。」メッセージを表示                                                                                                                                      
                                updLabelSave.Update();
                                LB_ShiresakiCode.Text = code;
                                saveSuccess = true;
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
                    if (sSHIIRESAKI == "")
                    {
                        TB_sShiresaki.BorderColor = System.Drawing.Color.Red;
                        TB_sShiresaki.BorderStyle = BorderStyle.Double;
                        TB_sShiresaki.BorderWidth = 2;
                    }
                }
            }
            else //更新
            {
                String id = Request.QueryString["id"];
                DataTable dt_db = JC01Login_Class.Get_DB(id);
                String db = dt_db.Rows[0]["db"].ToString();
                String mail = dt_db.Rows[0]["mail"].ToString();
                ConstantVal.DB_NAME = db;
                JC_ClientConnecction_Class jc = new JC_ClientConnecction_Class();
                jc.loginId = mail;
                cn = jc.GetConnection();
                String code = LB_ShiresakiCode.Text;
                String sSHIIRESAKI = TB_sShiresaki.Text.Trim().Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
                String skSHIIRESAKI = TB_skShiresaki.Text.Trim().Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
                String sSHIIRESAKI_R = TB_sShiresaki_R.Text.Trim().Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
                String cYuubin = TB_cYuubin.Text.Trim().Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
                String sJUUSHO1 = TB_Juusho1.Text.Trim().Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
                String sJUUSHO2 = TB_Juusho2.Text.Trim().Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
                String sTel = TB_sTel.Text.Trim().Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
                String sFax = TB_sFAX.Text.Trim().Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
                String nki_hiritsu1 = TB_kihonvalue.Text.Trim().Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
                String nki_hiritsu2 = TB_kihonvalue1.Text.Trim().Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
                String nka_hiritsu1 = TB_kakuchovalue.Text.Trim().Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
                String nka_hiritsu2 = TB_kakuchovalue1.Text.Trim().Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
                String dHenkou = DateTime.Now.ToString();
                //Updated start 20220324 by テタ
                String shimebi = null;
                if (DDL_shimebi.SelectedIndex != 0)
                {
                    shimebi = DDL_shimebi.SelectedItem.Text;
                }
                String skihonharai1 = null;
                if (DDL_kihonshiharai.SelectedIndex != 0)
                {
                    skihonharai1 = DDL_kihonshiharai.SelectedItem.Text;
                }
                String skihonharai2 = null;
                if (DDL_kihonshiharai1.SelectedIndex != 0)
                {
                    skihonharai2 = DDL_kihonshiharai1.SelectedItem.Text;
                }
                String skakuchoharai1 = null;
                if (DDL_kakuchoshiharai.SelectedIndex != 0)
                {
                    skakuchoharai1 = DDL_kakuchoshiharai.SelectedItem.Text;
                }
                String skakuchoharai2 = null;
                if (DDL_kakuchoshiharai1.SelectedIndex != 0)
                {
                    skakuchoharai2 = DDL_kakuchoshiharai1.SelectedItem.Text;
                }
                String fkakuchouharai = "0";
                if (chk_kakuchoshiharai.Checked)
                {
                    fkakuchouharai = "1";
                }
                String nte_kingaku = "0";
                if (!String.IsNullOrEmpty(TB_TekiyoJouken.Text))
                {
                    nte_kingaku = TB_TekiyoJouken.Text.Replace(",", "");
                }
                String stekiyoujouken = null;
                if (DDL_Yan.SelectedIndex != 0)
                {
                    stekiyoujouken = DDL_Yan.SelectedItem.Text;
                }
                //end 20220324
                String sJIkou = TB_sTOKKIJIKOU.Text.Trim().Replace("\r\n", "").Replace("\r", "").Replace("\n", "");

                if (sSHIIRESAKI != "")
                {
                    if (lblPhoneError.Text == "")
                    {

                        TB_sShiresaki.BorderColor = System.Drawing.Color.LightGray;
                        TB_sShiresaki.BorderStyle = BorderStyle.Solid;
                        TB_sShiresaki.BorderWidth = 1;

                        TB_sTel.Style["border-color"] = "none";
                        TB_sTel.BorderStyle = BorderStyle.Solid;
                        TB_sTel.BorderWidth = 1;

                        if (TextUtility.isomojiCharacter(sSHIIRESAKI))
                        {
                            f_isomoji_msg = false;
                        }
                        sSHIIRESAKI = sSHIIRESAKI.Replace("\\", "\\\\").Replace("'", "\\'");  //仕入先名 

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

                            String sql = "UPDATE m_shiiresaki SET sSHIIRESAKI='" + sSHIIRESAKI + "',skSHIIRESAKI='" + skSHIIRESAKI + "',sSHIIRESAKI_R='" + sSHIIRESAKI_R + "',cYUUBIN='" + cYuubin + "', " +
                                "sJUUSHO1='" + sJUUSHO1 + "', sJUUSHO2='" + sJUUSHO2 + "', sTEL='" + sTel + "', sFAX='" + sFax + "'," +
                                "  dHENKOU=Now(), cHENKOUSYA='" + lblLoginUserCode.Text + "',  sSHIMEBI='" + shimebi + "', skihonharai1='" + skihonharai1 + "', nkihonharai_hiritsu1='" + nki_hiritsu1 + "'," +
                                " skihonharai2='" + skihonharai2 + "', nkihonharai_hiritsu2='" + nki_hiritsu2 + "',skakuchouharai1='" + skakuchoharai1 + "',nkakuchouharai_hiritsu1='" + nka_hiritsu1 + "'," +
                                " skakuchouharai2 ='" + skakuchoharai2 + "',nkakuchouharai_hiritsu2 ='" + nka_hiritsu2 + "'," +
                                " fkakuchouharai='" + fkakuchouharai + "', ntekiyoujouken_kingaku='" + nte_kingaku + "', stekiyoujouken='" + stekiyoujouken + "', " +
                                "sTOKKIJIKOU='" + sJIkou + "' WHERE cSHIIRESAKI='" + code + "';";

                            String sql_tantouinsert = "DELETE FROM m_shiiresaki_tantousha WHERE cSHIIRESAKI='" + code + "';";
                            for (int i = 1; i < GV_ShiiresakiMaster.Rows.Count; i++)
                            {
                                String sbumon = (GV_ShiiresakiMaster.Rows[i].FindControl("SBUMON") as Label).Text.Trim().Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
                                String stantou = (GV_ShiiresakiMaster.Rows[i].FindControl("STANTOU") as Label).Text.Trim().Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
                                String syakusho = (GV_ShiiresakiMaster.Rows[i].FindControl("SYAKUSHOKU") as Label).Text.Trim().Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
                                String stel = (GV_ShiiresakiMaster.Rows[i].FindControl("STEL") as Label).Text.Trim().Replace("\r\n", "").Replace("\r", "").Replace("\n", "");

                                sql_tantouinsert += "INSERT INTO m_shiiresaki_tantousha (cSHIIRESAKI, NJUNBAN, SBUMON, STANTOU, SYAKUSHOKU, STEL, SFAX, SMAIL, FMAIL, SBIKOU, SKEISHOU)" +
                                " VALUES ('" + code + "', '" + i + "', '" + sbumon + "', '" + stantou + "', '" + syakusho + "', '" + stel + "', '', '', '', '', '');";
                            }

                            try
                            {
                                cn.Open();
                                MySqlCommand cmdInsert = new MySqlCommand(sql + sql_tantouinsert, cn);
                                cmdInsert.CommandTimeout = 0;
                                cmdInsert.ExecuteNonQuery();
                                cn.Close();
                                LB_ShiresakiCode.Text = code;
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
                    if (sSHIIRESAKI == "")
                    {
                        TB_sShiresaki.BorderColor = System.Drawing.Color.Red;
                        TB_sShiresaki.BorderStyle = BorderStyle.Double;
                        TB_sShiresaki.BorderWidth = 2;
                    }
                }
            }
            if (saveSuccess)
            {
                if (HF_flag.Value == "Popup")
                {
                    Session["fSyouhinSyosai"] = null;
                    Session["cSHIIRESAKI"] = LB_ShiresakiCode.Text;
                    Session["sSHIIRESAKI"] = TB_sShiresaki.Text;
                    if (Session["STANTOUCarry"] != null)
                    {
                        Session["STANTOUCarry"] = tantouCarry;
                    }
                    ifShinkiPopup.Src = "";
                    mpeShinkiPopup.Hide();
                    updShinkiPopup.Update();
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "parentButtonClick('btnShiresaki_Close','" + hdnHome.Value + "');", true);//20220323 addede by ルインマー
                                                                                                                                                                     // ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "parentButtonClick('btn_Close','" + hdnHome.Value + "');", true);
                }
            }

        }
        #endregion

        #region BT_LBSaveCross_Click()
        protected void BT_LBSaveCross_Click(object sender, EventArgs e)
        {
            divLabelSave.Style["display"] = "none";
        }
        #endregion

        #region BT_Shinki()
        protected void BT_Shinki_Click(object sender, EventArgs e)
        {
            if (HF_isChange.Value == "1")
            {
                updHeader.Update();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowAnkenChangeMessage",
                "ShowKoumokuChangesConfirmMessage('項目が変更されています。保存しますか？','" + btnYes.ClientID + "','" + btnNo.ClientID + "','" + btnCancel.ClientID + "');", true);
              
            }
            else
            {
                //20220328 テタ updated change session name
                Session["cSHIIRESAKI"] = null;
                //Response.Redirect("JC40Shiiresaki.aspx");
                if (!String.IsNullOrEmpty(Request.QueryString["id"]))
                {
                    String id = Request.QueryString["id"];
                    Response.Redirect("JC40Shiiresaki.aspx?id=" + id);
                }
                else
                {
                    Response.Redirect("JC01Login.aspx");
                }
            }
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
                        dr1["SBUMON"] = "";
                        dr1["SYAKUSHOKU"] = "";
                        dr1["STEL"] = "";
                        dr1["STANTOU"] = "";
                        dt1.Rows.Add(dr1);
                    }
                    if (njun != 0)
                    {
                        dt1.Rows.Add(njun, sbumon, yakusho, tel, stantou);
                    }

                    if (Request.Cookies["colWidthbTokuisaki"] != null)
                    {
                        HF_GridSize.Value = Request.Cookies["colWidthbTokuisaki"].Value;
                    }
                    else
                    {
                        HF_GridSize.Value = "30, 655, 255, 200, 180, 160";
                    }
                    if (Request.Cookies["colWidthbTokuisakiGrid"] != null)
                    {
                        HF_Grid.Value = Request.Cookies["colWidthbTokuisakiGrid"].Value;
                    }
                    else
                    {
                        HF_Grid.Value = "1093";
                    }

                    dt1.DefaultView.Sort = "NJUNBAN ASC";
                    dt1.AcceptChanges();
                    ViewState["Row"] = dt1;
                    GV_ShiiresakiMaster.DataSource = dt1;
                    GV_ShiiresakiMaster.DataBind();
                    GV_ShiiresakiMaster.Rows[0].Visible = false;
                    Label njunLabel = (GV_ShiiresakiMaster.Rows[0].FindControl("lblnjun") as Label);
                }
            }
            else
            {
                dt1.Columns.Add("NJUNBAN", typeof(int));
                dt1.Columns.Add("SBUMON", typeof(string));
                dt1.Columns.Add("SYAKUSHOKU", typeof(string));
                dt1.Columns.Add("STEL", typeof(string));
                dt1.Columns.Add("STANTOU", typeof(string));

                DataRow dr1 = dt1.NewRow();
                dr1["NJUNBAN"] = njun;
                dr1["SBUMON"] = sbumon;
                dr1["SYAKUSHOKU"] = yakusho;
                dr1["STEL"] = tel;
                dr1["STANTOU"] = stantou;
                dt1.Rows.Add(dr1);
                dt1.DefaultView.Sort = "NJUNBAN ASC";
                dt1.AcceptChanges();
                ViewState["Row"] = dt1;
                ViewState["sortDirection"] = SortDirection.Ascending;
                ViewState["z_sortexpresion"] = "NJUNBAN";
                GV_ShiiresakiMaster.DataSource = dt1;

                if (Request.Cookies["colWidthbTokuisaki"] != null)
                {
                    HF_GridSize.Value = Request.Cookies["colWidthbTokuisaki"].Value;
                }
                else
                {
                    HF_GridSize.Value = "70, 335, 125, 125, 100, 214";
                }
                if (Request.Cookies["colWidthbTokuisakiGrid"] != null)
                {
                    HF_Grid.Value = Request.Cookies["colWidthbTokuisakiGrid"].Value;
                }
                else
                {
                    HF_Grid.Value = "1093";
                }

                GV_ShiiresakiMaster.DataBind();
                GV_ShiiresakiMaster.Rows[0].Visible = false;
                Label njunLabel = (GV_ShiiresakiMaster.Rows[0].FindControl("lblnjun") as Label);
            }
        }
        #endregion

        #region BT_ColumnWidth_Click()
        protected void BT_ColumnWidth_Click(object sender, EventArgs e)
        {
            Response.Cookies["colWidthbTokuisaki"].Value = HF_GridSize.Value;
            Response.Cookies["colWidthbTokuisaki"].Expires = DateTime.Now.AddYears(1);
            Response.Cookies["colWidthbTokuisakiGrid"].Value = HF_Grid.Value;
            Response.Cookies["colWidthbTokuisakiGrid"].Expires = DateTime.Now.AddYears(1);
        }
        #endregion

        #region 確認メッセージの「はい」ボタン
        protected void btnYes_Click(object sender, EventArgs e)
        {
            BT_Save_Click(sender, e);
            if (HF_Save.Value == "1")
            {
                //20220328 テタ updated change session name
                Session["cSHIIRESAKI"] = null;
                //Response.Redirect("JC40Shiiresaki.aspx");
                if (!String.IsNullOrEmpty(Request.QueryString["id"]))
                {
                    String id = Request.QueryString["id"];
                    Response.Redirect("JC40Shiiresaki.aspx?id=" + id);
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
            //20220328 テタ updated change session name
            Session["cSHIIRESAKI"] = null;
            //Response.Redirect("JC40Shiiresaki.aspx");
            if (!String.IsNullOrEmpty(Request.QueryString["id"]))
            {
                String id = Request.QueryString["id"];
                Response.Redirect("JC40Shiiresaki.aspx?id=" + id);
            }
            else
            {
                Response.Redirect("JC01Login.aspx");
            }
        }
        #endregion

        #region GV_ShiiresakiMaster_Sorting()
        protected void GV_ShiiresakiMaster_Sorting(object sender, GridViewSortEventArgs e)
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
            GV_ShiiresakiMaster.Rows[0].Visible = false;
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
            dr1["SBUMON"] = "";
            dr1["SYAKUSHOKU"] = "";
            dr1["STEL"] = "";
            dr1["STANTOU"] = "";
            dt1.Rows.Add(dr1);
            foreach (DataRow dr in dv.ToTable().Rows)
            {
                dt1.Rows.Add(dr.ItemArray);
            }
            ViewState["Row"] = dt1;
            this.GV_ShiiresakiMaster.DataSource = dt1;
            GV_ShiiresakiMaster.DataBind();
            Label njun = (GV_ShiiresakiMaster.Rows[0].FindControl("lblnjun") as Label);

        }

        public string SortExpression
        {
            get
            {
                if (ViewState["z_sortexpresion"] == null)
                    ViewState["z_sortexpresion"] = this.GV_ShiiresakiMaster.DataKeyNames[0].ToString();
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

        #region GV_ShiiresakiMaster_PreRender()
        protected void GV_ShiiresakiMaster_PreRender(object sender, EventArgs e)
        {
            string[] columns = HF_GridSize.Value.Split(',');
            GV_ShiiresakiMaster.Columns[0].HeaderStyle.Width = Unit.Pixel((int)Math.Round(Convert.ToDecimal(columns[0])));
            GV_ShiiresakiMaster.Columns[1].HeaderStyle.Width = Unit.Pixel((int)Math.Round(Convert.ToDecimal(columns[1])));
            GV_ShiiresakiMaster.Columns[2].HeaderStyle.Width = Unit.Pixel((int)Math.Round(Convert.ToDecimal(columns[2])));
            GV_ShiiresakiMaster.Columns[3].HeaderStyle.Width = Unit.Pixel((int)Math.Round(Convert.ToDecimal(columns[3])));
            GV_ShiiresakiMaster.Columns[4].HeaderStyle.Width = Unit.Pixel((int)Math.Round(Convert.ToDecimal(columns[4])));
            GV_ShiiresakiMaster.Columns[5].HeaderStyle.Width = Unit.Pixel((int)Math.Round(Convert.ToDecimal(columns[5])));

            GV_ShiiresakiMaster.Width = Unit.Pixel((int)Math.Round(Convert.ToDecimal(HF_Grid.Value)));

            Response.Cookies["colWidthbTokuisaki"].Value = HF_GridSize.Value;
            Response.Cookies["colWidthbTokuisaki"].Expires = DateTime.Now.AddYears(1);
            Response.Cookies["colWidthbTokuisakiGrid"].Value = HF_Grid.Value;
            Response.Cookies["colWidthbTokuisakiGrid"].Expires = DateTime.Now.AddYears(1);
        }
        #endregion

        #region GV_ShiiresakiMaster_RowDataBound()
        protected void GV_ShiiresakiMaster_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                try
                {
                    Label lbl_sBumon = (e.Row.FindControl("SBUMON") as Label);
                    String sBumon = lbl_sBumon.Text;
                    sBumon = sBumon.Replace("<", "&lt").Replace(">", "&gt");
                    lbl_sBumon.Text = sBumon;

                    Label lbl_sYakushoku = (e.Row.FindControl("SYAKUSHOKU") as Label);
                    String sYakushoku = lbl_sYakushoku.Text;
                    sYakushoku = sYakushoku.Replace("<", "&lt").Replace(">", "&gt");
                    lbl_sYakushoku.Text = sYakushoku;

                    Label lbl_sTantou = (e.Row.FindControl("STANTOU") as Label);
                    String sTantou = lbl_sTantou.Text;
                    sTantou = sTantou.Replace("<", "&lt").Replace(">", "&gt");
                    lbl_sTantou.Text = sTantou;

                }
                catch { }
            }
        }
        #endregion

        #region GV_ShiiresakiMaster_RowCreated()
        protected void GV_ShiiresakiMaster_RowCreated(object sender, GridViewRowEventArgs e)
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
                                tablecell.Controls.Add(img);
                                break;
                            }
                        }

                    }

                }

            }
        }
        #endregion

        #region TB_skShiresaki_TextChanged()
        protected void TB_skShiresaki_TextChanged(object sender, EventArgs e)
        {
            HF_isChange.Value = "1";
            updHeader.Update();
        }
        #endregion

        #region TB_sShiresaki_R_TextChanged()
        protected void TB_sShiresaki_R_TextChanged(object sender, EventArgs e)
        {
            HF_isChange.Value = "1";
            updHeader.Update();
        }
        #endregion

        #region TB_sTel_TextChanged()
        protected void TB_sTel_TextChanged(object sender, EventArgs e)
        {
            if (TextUtility.IsIncludeZenkaku(TB_sTel.Text))
            {
                lblPhoneError.Text = "半角英数を入力してください。";
                TB_sTel.BorderColor = System.Drawing.Color.Red;
                TB_sTel.BorderStyle = BorderStyle.Double;
                TB_sTel.BorderWidth = 2;
                TB_sTel.Text = "";
                TB_sTel.Focus();
            }
            else
            {
                lblPhoneError.Text = "";
                TB_sTel.Style["border-color"] = "none";
                TB_sTel.BorderStyle = BorderStyle.Solid;
                TB_sTel.BorderWidth = 1;
            }
            HF_isChange.Value = "1";
            updHeader.Update();
        }
        #endregion

        #region TB_cYuubin_TextChanged()
        protected void TB_cYuubin_TextChanged(object sender, EventArgs e)
        {
            HF_isChange.Value = "1";
            updHeader.Update();
        }
        #endregion

        #region TB_Juusho1_TextChanged()
        protected void TB_Juusho1_TextChanged(object sender, EventArgs e)
        {
            HF_isChange.Value = "1";
            updHeader.Update();
        }
        #endregion

        #region TB_Juusho2_TextChanged()
        protected void TB_Juusho2_TextChanged(object sender, EventArgs e)
        {
            HF_isChange.Value = "1";
            updHeader.Update();
        }
        #endregion

        #region TB_sFAX_TextChanged()
        protected void TB_sFAX_TextChanged(object sender, EventArgs e)
        {
            HF_isChange.Value = "1";
            updHeader.Update();
        }
        #endregion

        #region TB_sTOKKIJIKOU_TextChanged()
        protected void TB_sTOKKIJIKOU_TextChanged(object sender, EventArgs e)
        {
            HF_isChange.Value = "1";
            updHeader.Update();
        }
        #endregion

        #region chk_kakuchoshiharai_Clicked
        protected void chk_kakuchoshiharai_Clicked(object sender, EventArgs e)
        {
            HF_isChange.Value = "1";
            updHeader.Update();
        }
        #endregion

        #region TB_kihonvalue_TextChanged
        protected void TB_kihonvalue_TextChanged(object sender, EventArgs e)
        {
            HF_isChange.Value = "1";
            updHeader.Update();
        }
        #endregion

        #region TB_kihonvalue1_TextChanged
        protected void TB_kihonvalue1_TextChanged(object sender, EventArgs e)
        {
            HF_isChange.Value = "1";
            updHeader.Update();
        }
        #endregion

        #region TB_TekiyoJouken_TextChanged
        protected void TB_TekiyoJouken_TextChanged(object sender, EventArgs e)
        {
            //Updated start 20220324 by テタ
            try
            {
                Decimal number = Convert.ToDecimal(TB_TekiyoJouken.Text.Replace(",", ""));
                TB_TekiyoJouken.Text = number.ToString("#,##0");
            }
            catch
            {
                TB_TekiyoJouken.Text = "";
            }
            //end 20220324
            HF_isChange.Value = "1";
            updHeader.Update();
        }
        #endregion

        #region TB_kakuchovalue_TextChanged
        protected void TB_kakuchovalue_TextChanged(object sender, EventArgs e)
        {
            HF_isChange.Value = "1";
            updHeader.Update();
        }
        #endregion

        #region TB_kakuchovalue1_TextChanged
        protected void TB_kakuchovalue1_TextChanged(object sender, EventArgs e)
        {
            HF_isChange.Value = "1";
            updHeader.Update();
        }
        #endregion

        #region btCancel_Click
        protected void btCancel_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "parentButtonClick('btnClose','" + hdnHome.Value + "');", true);
        }
        #endregion

        protected void btnClose_Click(object sender, EventArgs e)
        {
            ifShinkiPopup.Src = "";
            mpeShinkiPopup.Hide();
            updShinkiPopup.Update();

        }

        protected void btnToLogin_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "parentButtonClick('btnToLogin','" + hdnHome.Value + "');", true);
        }

        //start 20220328 テタ updated SelectedIndexChanged

        #region DDL_shimebi_SelectedIndexChanged
        protected void DDL_shimebi_SelectedIndexChanged(object sender, EventArgs e)
        {
            HF_isChange.Value = "1";
            updHeader.Update();
        }
        #endregion

        #region DDL_kihonshiharai_SelectedIndexChanged
        protected void DDL_kihonshiharai_SelectedIndexChanged(object sender, EventArgs e)
        {
            HF_isChange.Value = "1";
            updHeader.Update();
        }
        #endregion

        #region DDL_kihonshiharai1_SelectedIndexChanged
        protected void DDL_kihonshiharai1_SelectedIndexChanged(object sender, EventArgs e)
        {
            HF_isChange.Value = "1";
            updHeader.Update();
        }
        #endregion

        #region DDL_Yan_SelectedIndexChanged
        protected void DDL_Yan_SelectedIndexChanged(object sender, EventArgs e)
        {
            HF_isChange.Value = "1";
            updHeader.Update();
        }
        #endregion

        #region DDL_kakuchoshiharai_SelectedIndexChanged
        protected void DDL_kakuchoshiharai_SelectedIndexChanged(object sender, EventArgs e)
        {
            HF_isChange.Value = "1";
            updHeader.Update();
        }
        #endregion

        #region DDL_kakuchoshiharai1_SelectedIndexChanged
        protected void DDL_kakuchoshiharai1_SelectedIndexChanged(object sender, EventArgs e)
        {
            HF_isChange.Value = "1";
            updHeader.Update();
        }
        #endregion
        //end 20220328


        #region getcSHIIRESAKI
        protected DataTable getcSHIIRESAKI()
        {
            string sql = "select cSHIIRESAKI as cSHIIRESAKI from m_shiiresaki order by cSHIIRESAKI";
            cmd = new MySqlCommand(sql, cn);
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
        #endregion

        #region SetShiiresakiData()
        public void SetShiiresakiData()
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
            string sql = "SELECT cSHIIRESAKI as cshiiresaki," +
                " sSHIIRESAKI as sshiiresaki," +
                " skSHIIRESAKI as skSHIIRESAKI," +
                " sSHIIRESAKI_R as sSHIIRESAKI_R, " +
                " skSHIIRESAKI_R as skSHIIRESAKI_R," +
                " cYUUBIN as cYUUBIN," +
                " sJUUSHO1 as sJUUSHO1," +
                " sJUUSHO2 as sJUUSHO2," +
                " sTEL as sTEL," +
                " sFAX as sFAX," +
                " sSHIMEBI as SHIMEBI," +
                " skihonharai1 as skiharai1," +
                " nkihonharai_hiritsu1 as nki_hiritsu1," +
                " skihonharai2 as skiharai2," +
                " nkihonharai_hiritsu2 as nki_hiritsu2," +
                " skakuchouharai1 as skaharai1," +
                " nkakuchouharai_hiritsu1 as nka_hiritsu1," +
                " skakuchouharai2 as skaharai2," +
                " nkakuchouharai_hiritsu2 as nka_hiritsu2," +
                " fkakuchouharai as fkakuchouharai," +
                " ntekiyoujouken_kingaku as nteki_kingaku, " +
                " stekiyoujouken as stekiyoujouken, " +
                " sTOKKIJIKOU as sTOKKIJIKOU " +
                " FROM m_shiiresaki as ms" +
                " Left join m_j_tantousha as mjt ON ms.cHENKOUSYA = mjt.cTANTOUSHA" +
                " where cSHIIRESAKI = '" + LB_ShiresakiCode.Text + "'; ";
            cmd = new MySqlCommand(sql, cn);
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                LB_ShiresakiCode.Text = dt.Rows[0]["cshiiresaki"].ToString();
                TB_sShiresaki.Text = dt.Rows[0]["sshiiresaki"].ToString();
                TB_skShiresaki.Text = dt.Rows[0]["skSHIIRESAKI"].ToString();
                TB_sShiresaki_R.Text = dt.Rows[0]["sSHIIRESAKI_R"].ToString();
                TB_cYuubin.Text = dt.Rows[0]["cYUUBIN"].ToString();
                TB_Juusho1.Text = dt.Rows[0]["sJUUSHO1"].ToString();
                TB_Juusho2.Text = dt.Rows[0]["sJUUSHO2"].ToString();
                TB_sTel.Text = dt.Rows[0]["sTEL"].ToString();
                TB_sFAX.Text = dt.Rows[0]["sFAX"].ToString();
                //Updated start 20220324 by テタ
                if (String.IsNullOrEmpty(dt.Rows[0]["SHIMEBI"].ToString()))
                {
                    DDL_shimebi.SelectedIndex = 0;
                }
                else
                {
                    DDL_shimebi.SelectedValue = dt.Rows[0]["SHIMEBI"].ToString();
                }
                if (String.IsNullOrEmpty(dt.Rows[0]["skiharai1"].ToString()))
                {
                    DDL_kihonshiharai.SelectedIndex = 0;
                }
                else
                {
                    DDL_kihonshiharai.SelectedValue = dt.Rows[0]["skiharai1"].ToString();
                }
                TB_kihonvalue.Text = dt.Rows[0]["nki_hiritsu1"].ToString();
                if (String.IsNullOrEmpty(dt.Rows[0]["skiharai2"].ToString()))
                {
                    DDL_kihonshiharai1.SelectedIndex = 0;
                }
                else
                {
                    DDL_kihonshiharai1.SelectedValue = dt.Rows[0]["skiharai2"].ToString();
                }
                TB_kihonvalue1.Text = dt.Rows[0]["nki_hiritsu2"].ToString();
                if (String.IsNullOrEmpty(dt.Rows[0]["skaharai1"].ToString()))
                {
                    DDL_kakuchoshiharai.SelectedIndex = 0;
                }
                else
                {
                    DDL_kakuchoshiharai.SelectedValue = dt.Rows[0]["skaharai1"].ToString();
                }
                TB_kakuchovalue.Text = dt.Rows[0]["nka_hiritsu1"].ToString();
                if (String.IsNullOrEmpty(dt.Rows[0]["skaharai2"].ToString()))
                {
                    DDL_kakuchoshiharai1.SelectedIndex = 0;
                }
                else
                {
                    DDL_kakuchoshiharai1.SelectedValue = dt.Rows[0]["skaharai2"].ToString();
                }
                TB_kakuchovalue1.Text = dt.Rows[0]["nka_hiritsu2"].ToString();
                if (dt.Rows[0]["fkakuchouharai"].ToString().Equals("1"))
                {
                    chk_kakuchoshiharai.Checked = true;
                }
                else
                {
                    chk_kakuchoshiharai.Checked = false;
                }
                if (!String.IsNullOrEmpty(dt.Rows[0]["nteki_kingaku"].ToString()))
                {
                    string nteki_kingaku = Convert.ToInt32(dt.Rows[0]["nteki_kingaku"]).ToString();
                    TB_TekiyoJouken.Text = nteki_kingaku;
                    Decimal number = Convert.ToDecimal(TB_TekiyoJouken.Text.Replace(",", ""));
                    TB_TekiyoJouken.Text = number.ToString("#,##0");
                }
                if (String.IsNullOrEmpty(dt.Rows[0]["stekiyoujouken"].ToString()))
                {
                    DDL_Yan.SelectedIndex = 0;
                }
                else
                {
                    DDL_Yan.SelectedValue = dt.Rows[0]["stekiyoujouken"].ToString();
                }
                //end 20220324
                TB_sTOKKIJIKOU.Text = dt.Rows[0]["sTOKKIJIKOU"].ToString();
            }
            cn.Close();
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
            //20220328 テタ updated change column
            cmd = new MySqlCommand("select NJUNBAN,SBUMON,SYAKUSHOKU,STEL,STANTOU from m_shiiresaki_tantousha where cSHIIRESAKI='" + LB_ShiresakiCode.Text + "' order by NJUNBAN asc", cn);
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            dt = new DataTable();
            dt.Rows.Add(dt.NewRow());
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                GV_ShiiresakiMaster.DataSource = dt;
                GV_ShiiresakiMaster.DataBind();
                GV_ShiiresakiMaster.Rows[0].Visible = true;
                GV_ShiiresakiMaster.FooterRow.Visible = false;
            }
            else
            {
                GV_ShiiresakiMaster.DataSource = dt;
                GV_ShiiresakiMaster.DataBind();
                GV_ShiiresakiMaster.Rows[0].Visible = true;
                GV_ShiiresakiMaster.FooterRow.Visible = false;
            }
            cn.Close();

            ViewState["Row"] = dt;
        }
        #endregion

    }
}