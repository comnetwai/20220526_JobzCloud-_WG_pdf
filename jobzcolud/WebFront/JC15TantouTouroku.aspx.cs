using Common;
using jobzcolud;
using MySql.Data.MySqlClient;
using Service;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

namespace JobzCloud.WebFront
{
    public partial class JC15TantouTouroku : System.Web.UI.Page
    {
        MySqlConnection con = null;
        MySql.Data.MySqlClient.MySqlCommand cmd;
        public string tantouCode;
        DataTable dt = new DataTable();
        public static string to, tomail; //20211209
        public static string b_mail;
        public bool clicked_edit = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(Request.QueryString["id"]))
                {
                    if (!this.IsPostBack)
                    {
                        try
                        {
                            if (SessionUtility.GetSession("HOME") != null)  //20211014 MiMi Added
                            {
                                hdnHome.Value = SessionUtility.GetSession("HOME").ToString();
                                SessionUtility.SetSession("HOME", null);
                            }

                            String id = Request.QueryString["id"];
                            DataTable dt_db = JC01Login_Class.Get_DB(id);
                            String db = dt_db.Rows[0]["db"].ToString();
                            String mail = dt_db.Rows[0]["mail"].ToString();
                            ConstantVal.DB_NAME = db;

                            JC_ClientConnecction_Class jc = new JC_ClientConnecction_Class();
                            jc.loginId = mail;
                            con = jc.GetConnection();
                            //btnEnroll.BorderWidth = 3;
                            btnEnroll.BackColor = Color.LightGray;
                            //btnLeave.BorderWidth = 2;
                            btnLeave.BackColor = Color.White;
                            DLeave.Visible = false;
                            updTantoudate.Visible = false;
                            updTokuisakiDate.Update();

                            if (Session["cTantousha"] != null)  //更新
                            {
                                lbl_title.Text = "ユーザーを編集";
                                tantouCode = Session["cTantousha"].ToString();
                                hiddenFieldId.Value = tantouCode;
                                SetTantouData(tantouCode);
                            }
                        }
                        catch (Exception ex)
                        {
                            //Response.Redirect("JC01Login.aspx");
                            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "parentButtonClick('btnToLogin','" + hdnHome.Value + "');", true);
                        }
                    }
                }
                else
                {

                    //Response.Redirect("JC01Login.aspx");
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "parentButtonClick('btnToLogin','" + hdnHome.Value + "');", true);
                }
                Session.Timeout = 480;

            }
            catch
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "parentButtonClick('btnToLogin','" + hdnHome.Value + "');", true);
            }
        }

        #region SetTantouData
        public void SetTantouData(String ctantou)
        {
            string sql = "SELECT mjt.sMAIL as sMAIL," +
                        " sTANTOUSHA as sTANTOUSHA," +
                        " sPWD as sPWD," +
                        " mjt.cKYOTEN as cKYOTEN," +
                        " mji.sKYOTEN as sKYOTEN," +
                        " mjt.cBUMON as cBUMON," +
                        " mb.sBUMON as sBUMON," +
                        " mkg.ckengenn as ckengenn," +
                        " mkg.sKENGENN as sKENGENN," +
                        " mjt.fTAISYA as fTAISYA," +
                        " date_format(mjt.dTAISHA, '%Y/%m/%d') as dTAISHA" +
                        " FROM m_j_tantousha as mjt" +
                        " LEFT JOIN m_j_info mji ON mjt.cKYOTEN = mji.cCo" +
                        " LEFT JOIN m_bumon mb ON mjt.cBUMON = mb.cBUMON" +
                        " LEFT JOIN m_kengenn mkg ON mjt.ckengenn = mkg.cKENGENN WHERE cTANTOUSHA = '"+ctantou+"'; ";

            cmd = new MySqlCommand(sql, con);
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                tb_login.Text = dt.Rows[0]["sMAIL"].ToString();
                b_mail = dt.Rows[0]["sMAIL"].ToString();
                tb_tantoumei.Text = dt.Rows[0]["sTANTOUSHA"].ToString();
                String pwd= dt.Rows[0]["sPWD"].ToString();
                String decrypt_pwd = TextUtility.DecryptData_Henkou(pwd);
                tb_pwd.Text = decrypt_pwd;
                tb_pwdConfirm.Text = decrypt_pwd;

                if (!String.IsNullOrEmpty(dt.Rows[0]["cKYOTEN"].ToString()))
                {
                    string cKyoten = dt.Rows[0]["cKYOTEN"].ToString();
                    string sKyoten = dt.Rows[0]["sKYOTEN"].ToString();
                    lblcKYOTEN.Text = cKyoten;
                    lblsKYOTEN.Text = sKyoten.Replace("<", "&lt").Replace(">", "&gt");
                    divKyotenbtn.Style["display"] = "none";
                    divKyotenLabel.Style["display"] = "block";
                    lblsKYOTEN.Attributes.Add("onClick", "BtnClick('btnKyoten')");
                    upd_KYOTENLIST.Update();
                }

                if (!String.IsNullOrEmpty(dt.Rows[0]["cBUMON"].ToString()))
                {
                    string cBumon = dt.Rows[0]["cBUMON"].ToString();
                    string sBumon = dt.Rows[0]["sBUMON"].ToString();
                    lblcBumon.Text = cBumon;
                    lblsBumon.Text = sBumon.Replace("<", "&lt").Replace(">", "&gt");
                    divBumonbtn.Style["display"] = "none";
                    divBumonLabel.Style["display"] = "block";
                    lblsBumon.Attributes.Add("onClick", "BtnClick('btnBumon')");
                    upd_Bumon.Update();
                }

                if (!String.IsNullOrEmpty(dt.Rows[0]["ckengenn"].ToString()) && dt.Rows[0]["ckengenn"].ToString()!="00")
                {
                    string cKengen = dt.Rows[0]["ckengenn"].ToString();
                    string sKengen = dt.Rows[0]["sKENGENN"].ToString();
                    lblcKengen.Text = cKengen;
                    lblsKengen.Text = sKengen.Replace("<", "&lt").Replace(">", "&gt");
                    divKengenBtn.Style["display"] = "none";
                    divKengenLabel.Style["display"] = "block";
                    lblsKengen.Attributes.Add("onClick", "BtnClick('btnKengen')");
                    upd_Kengen.Update();
                }

                //if (dt.Rows[0]["cBUMON"].ToString() == "1")
                if (dt.Rows[0]["fTAISYA"].ToString().Equals("1")) //20220207 Updated by エインドリ－・プ－プゥ
                {
                    btnLeave.BorderWidth = 3;
                    btnLeave.BackColor = Color.LightGray;
                    btnEnroll.BackColor = Color.White;
                    btnEnroll.BorderWidth = 2;
                    DLeave.Visible = true;
                    updTantoudate.Visible = true;

                    if (!String.IsNullOrEmpty(dt.Rows[0]["dTAISHA"].ToString()))
                    {
                        DateTime dtDeliveryDate = DateTime.Parse(dt.Rows[0]["dTAISHA"].ToString());
                        lbldTantou.Text = dtDeliveryDate.ToString("yyyy/MM/dd");
                        btnTantouDate.Style["display"] = "none";
                        divTantouDate.Style["display"] = "block";
                        lbldTantou.Attributes.Add("onClick", "BtnClick('btnTantouDate')");
                        updTokuisakiDate.Update();
                        updTantoudate.Update();
                    }
                }
                clicked_edit = true;//20211208 Added By エインドリ－・プ－プゥ
            }
        }
        #endregion

        #region save_btn_Click
        protected void save_btn_Click(object sender, EventArgs e)
        {
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowErrorMessage",
            //            "ShowErrorMessage('まだ、本登録できておりません。<br/> 入力されたメールアドレスに認証リンクを送付しました。<br/>メールアプリにてご確認の上、リンクをクリックして認証を行ってください。');", true);

            //20211209 Added By エインドリ－・プ－プゥ (Start)
            LB_login_Error.Text = "";                    //20211222 Added By エインドリ－・プ－プゥ 
            LB_tantoumei_Error.Text = "";                //20211222 Added By エインドリ－・プ－プゥ 
            LB_pwd_Error.Text = "";                      //20211222 Added By エインドリ－・プ－プゥ 
            LB_pwdConfirm_Error.Text = "";               //20211222 Added By エインドリ－・プ－プゥ 
            tb_pwd.BorderColor = Color.Gray;             //20211222 Added By エインドリ－・プ－プゥ 
            tb_pwdConfirm.BorderColor = Color.Gray;      //20211222 Added By エインドリ－・プ－プゥ 
            if (tb_login.Text == "")
            {
                LB_login_Error.Text = "ユーザーIDが正しくありません。";      //20211222 Added By エインドリ－・プ－プゥ 
                tb_login.Focus();
                updTokuisakiDate.Update();                                  //20211222 Added By エインドリ－・プ－プゥ 
            }
            else if (tb_tantoumei.Text == "")
            {
                LB_tantoumei_Error.Text = "ユーザー名が存在しません。";      //20211222 Added By エインドリ－・プ－プゥ
                tb_tantoumei.Focus();
                updTokuisakiDate.Update();                                   //20211222 Added By エインドリ－・プ－プゥ 
            }
            else if (tb_pwd.Text == "" || tb_pwdConfirm.Text=="")
            {
                tb_pwd.Text = "";
                tb_pwdConfirm.Text = "";
                tb_pwd.BorderColor = Color.Red;                  //20211222 Added By エインドリ－・プ－プゥ 
                tb_pwdConfirm.BorderColor = Color.Red;           //20211222 Added By エインドリ－・プ－プゥ 
                tb_pwd.Focus();
                updTokuisakiDate.Update();                       //20211222 Added By エインドリ－・プ－プゥ 
            }
            else if (tb_pwd.Text.Length < 8)　//20220119 Added エインドリ－・プ－プゥ
            {
                tb_pwdConfirm.Text = "";
                tb_pwdConfirm.Text = "";
                LB_pwd_Error.Text = "パスワードは8文字以上で登録してください。";
                tb_pwd.Focus();
                updTokuisakiDate.Update();
            }
            else
            {
                if (hiddenFieldId.Value != "") //20211214 Added エインドリ－・プ－プゥ
                {
                    #region Update_Tantou
                    /* 1. Check Mail has changed or Not*/
                    if (b_mail.Equals(tb_login.Text))
                    {
                        /* Update Without sending the mail*/

                        String id = Request.QueryString["id"];
                        DataTable dt_db = JC01Login_Class.Get_DB(id);
                        String db = dt_db.Rows[0]["db"].ToString();
                        String mail = dt_db.Rows[0]["mail"].ToString();
                        ConstantVal.DB_NAME = db;

                        JC15TantouTouroku_Class c = new JC15TantouTouroku_Class();
                        c.cTANTOUSHA = hiddenFieldId.Value;
                        c.sTANTOUSHA = tb_tantoumei.Text;
                        c.cBUMON= lblcBumon.Text;
                        c.SMAIL= tb_login.Text;
                        c.sPWD= TextUtility.EncryptData_Henkou(tb_pwd.Text);
                        c.fKANRISHA = lblcKengen.Text == "01" ? "1" : "0";
                        if (btnEnroll.BackColor == Color.LightGray)
                        {
                           c.fTAISYA= "0";
                        }
                        else
                        {
                            c.fTAISYA = "1";
                            if (lbldTantou.Text != "")
                            {
                                c.dTAISYA = lbldTantou.Text.ToString();
                            }
                            else
                            {
                                c.dTAISYA = "";
                            }
                        }

                        ConstantVal.DB_NAME = db;
                        c.cHENKOUSYA= JC99NavBar_Class.Login_Tan_Code;
                       c.cKYOTEN = lblcKYOTEN.Text;
                       c.ckengenn= lblcKengen.Text;
                        c.old_mail = b_mail;
                        if (tb_pwd.Text == tb_pwdConfirm.Text) //20211222 Added エインドリ－・プ－プゥ 
                        {
                            if (c.Tantou_Update()) //20211215 Added エインドリ－・プ－プゥ
                            {
                                //ScriptManager.RegisterStartupScript(this, this.GetType(), "alerts", "javascript:alert('更新しました。')", true);
                                //20211222 Added エインドリ－・プ－プゥ

                                //ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowErrorMessage",
                                //"ShowErrorMessage('更新しました。');", true);

                                //20220119 Added エインドリ－・プ－プゥ
                                JC14TantouKensaku.is_logout = mail == tb_login.Text ? true : false;
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowSuccessMessage",
                                           "ShowSuccessMessage('更新しました。','" + btn_cancel.ClientID + "');", true);

                            }
                            else
                            {
                                //ScriptManager.RegisterStartupScript(this, this.GetType(), "alerts", "javascript:alert('何かがうまくいかなかった。')", true);
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowErrorMessage",
                                "ShowErrorMessage('何かがうまくいかなかった。');", true); //20211222 Added エインドリ－・プ－プゥ
                            }
                        }
                        else
                        {
                            //ScriptManager.RegisterStartupScript(this, this.GetType(), "alerts", "javascript:alert('パスワードが一致していません。')", true);
                            //ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowErrorMessage",
                            //"ShowErrorMessage('パスワードが一致していません。');", true);
                            //20211222 Added By エインドリ－・プ－プゥ 
                            tb_pwdConfirm.Text = "";
                            tb_pwdConfirm.Text = "";
                            LB_pwd_Error.Text = "パスワードが一致していません。";            
                            tb_pwd.Focus();
                            updTokuisakiDate.Update();
                        }
                        
                    }
                    else
                    {
                        /* Update by sending the mail*/
                        /*TODO:: Send Mail and Show the message
                         1. Check Valid Mail or Not
                         2. Mail is already exist or not
                         3. If Not -> Send Mail
                                   4. Show alert message
                        */
                        if (!TextUtility.IsValidEmailAddress(tb_login.Text)) //1. Check Valid Mail or Not
                        {
                            LB_login_Error.Text = "ユーザーIDが正しくありません。";      //20211222 Added By エインドリ－・プ－プゥ 
                            tb_login.Focus();
                            updTokuisakiDate.Update();                                  //20211222 Added By エインドリ－・プ－プゥ 
                        }
                        else
                        {
                            String id = Request.QueryString["id"];
                            DataTable dt_db = JC01Login_Class.Get_DB(id);
                            String db = dt_db.Rows[0]["db"].ToString();
                            String mail = dt_db.Rows[0]["mail"].ToString();
                            ConstantVal.DB_NAME = db;
                            //2. Mail is already exist or not                     //20211227 Added By エインドリ－・プ－プゥ
                            if (JC02Touroku_Class.MailAcc_Check(tb_login.Text) || JC15TantouTouroku_Class.MailAcc_Check(tb_login.Text))
                            {
                                LB_login_Error.Text = "このメールアドレスはすでに会員登録で使用されているメールアドレスです。"; //20211222 Added By エインドリ－・プ－プゥ 
                                tb_login.Text = string.Empty;
                                tb_login.Focus();
                                updTokuisakiDate.Update();                                  //20211222 Added By エインドリ－・プ－プゥ 
                            }
                            else //3.Send Mail
                            {
                                if (tb_pwd.Text == tb_pwdConfirm.Text) //20211213 Updated エインドリ－・プ－プゥ
                                {
                                    //20220322 Update JOBZグラウド -> 見えるJOBZ in mail エインドリ－
                                    #region　メール送信
                                    String url = HttpContext.Current.Request.Url.AbsoluteUri;
                                    url = url.Substring(0, url.Length - 17);
                                    //url = url + "JC04Mailkakunin.aspx?id=" + strGuid;
                                    url = url + "JC01Login.aspx?id=true";
                                    string from, pass, messageBody = "";
                                    MailMessage message = new MailMessage();
                                    to = tb_login.Text;
                                    from = ConfigurationManager.AppSettings["SMTP_Sender"];
                                    pass = ConfigurationManager.AppSettings["SMTP_Password"];
                                    messageBody = "この度は見えるJOBZにご登録頂きまして誠にありがとうございます。" + "\n" + "２４時間以内に下記のリンクをクリックして認証を行ってください。" + "\n" + url;
                                    message.To.Add(to);
                                    //message.From = new MailAddress(from, "JOBZグラウド 運営チーム");
                                    message.From = new MailAddress(from, ConfigurationManager.AppSettings["SMTP_SenderName"]); //20211203 Updated エインドリ－・プ－プゥ
                                    message.Body = messageBody;
                                    message.Subject = "見えるJOBZのメールアドレスの認証";
                                    //SmtpClient smtp = new SmtpClient("smtp.gmail.com");
                                    SmtpClient smtp = new SmtpClient();
                                    smtp.Host = ConfigurationManager.AppSettings["SMTP_Host"];
                                    //smtp.Timeout = 300000;
                                    smtp.Timeout = int.Parse(ConfigurationManager.AppSettings["SMTP_Timeout"]);
                                    smtp.EnableSsl = true;
                                    //smtp.Port = 587;
                                    smtp.Port = int.Parse(ConfigurationManager.AppSettings["SMTP_Port"]);
                                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                                    smtp.UseDefaultCredentials = false;
                                    ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                                    smtp.Credentials = new NetworkCredential(from, pass);
                                    smtp.EnableSsl = true;
                                    //20211203 Updated エインドリ－・プ－プゥ (End)
                                    try
                                    {
                                        tomail = tb_login.Text;
                                        smtp.Send(message);
                                        JC01Login.is_tantou = true;
                                        JC01Login.is_edit = true;
                                        JC01Login.ID = mail;
                                        JC01Login.ctantou= hiddenFieldId.Value; //20211215 Added By エインドリ－・プ－プゥ
                                        JC01Login.stantou = tb_tantoumei.Text;
                                        JC01Login.cbumon = lblcBumon.Text;
                                        JC01Login.mail = tb_login.Text;
                                        JC01Login.old_email = b_mail;
                                        JC01Login.pwd = TextUtility.EncryptData_Henkou(tb_pwd.Text);
                                        JC01Login.chenkousha = JC99NavBar_Class.Login_Tan_Code;
                                        JC01Login.ckyoten = lblcKYOTEN.Text;
                                        JC01Login.ckengenn = lblcKengen.Text;
                                        if (lblcKengen.Text == "01")
                                        {
                                            JC01Login.fkanrisha = "1";
                                        }
                                        else
                                        {
                                            JC01Login.fkanrisha = "0";
                                        }
                                        if (btnEnroll.BackColor == Color.LightGray)
                                        {
                                            JC01Login.ftaisya = "0";
                                        }
                                        else
                                        {
                                            JC01Login.ftaisya = "1";
                                            if (lbldTantou.Text != "")
                                            {
                                                JC01Login.dtaisya = lbldTantou.Text.ToString();
                                            }
                                            else
                                            {
                                                JC01Login.dtaisya = "";
                                            }
                                        }

                                        ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowMailKakuninMessage",
                                        "MailKakuninMsgBox('まだ、本登録できておりません。<br/> 入力されたメールアドレスに認証リンクを送付しました。<br/>メールアプリにてご確認の上、リンクをクリックして認証を行ってください。','" + btn_cancel.ClientID + "');", true);
                                    }
                                    catch (Exception ex)
                                    {
                                        Response.Write("<script>alert('" + ex.Message + "');</script>");
                                    }
                                    #endregion
                                }
                                else
                                {
                                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "alerts", "javascript:alert('パスワードが一致していません。')", true);
                                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowErrorMessage",
                                    //"ShowErrorMessage('パスワードが一致していません。');", true);
                                    //20211222 Added By エインドリ－・プ－プゥ 
                                    tb_pwdConfirm.Text = "";
                                    tb_pwdConfirm.Text = "";
                                    LB_pwd_Error.Text = "パスワードが一致していません。";            
                                    tb_pwd.Focus();
                                    updTokuisakiDate.Update();
                                }
                            }
                        }
                    }
                    #endregion
                }
                else
                {
                    //20211214 Set Region 
                    #region Insert_Tantou
                    /*TODO:: Send Mail and Show the message
                     1. Check Valid Mail or Not
                     2. Mail is already exist or not
                     3. If Not -> Send Mail
                               4. Show alert message
                    */
                    if (!TextUtility.IsValidEmailAddress(tb_login.Text)) //1. Check Valid Mail or Not
                    {
                        LB_login_Error.Text = "ユーザーIDが正しくありません。";      //20211222 Added By エインドリ－・プ－プゥ 
                        tb_login.Focus();
                        updTokuisakiDate.Update();                                  //20211222 Added By エインドリ－・プ－プゥ 
                    }
                    else
                    {
                        String id = Request.QueryString["id"];
                        DataTable dt_db = JC01Login_Class.Get_DB(id);
                        String db = dt_db.Rows[0]["db"].ToString();
                        String mail = dt_db.Rows[0]["mail"].ToString();
                        ConstantVal.DB_NAME = db;

                        //2. Mail is already exist or not                        //20211227 Added エインドリ－・プ－プゥ
                        if (JC02Touroku_Class.MailAcc_Check(tb_login.Text) || JC15TantouTouroku_Class.MailAcc_Check(tb_login.Text))
                        {
                            LB_login_Error.Text = "このメールアドレスはすでに会員登録で使用されているメールアドレスです。"; //20211222 Added By エインドリ－・プ－プゥ 
                            tb_login.Text = string.Empty;
                            tb_login.Focus();
                            updTokuisakiDate.Update();                                  //20211222 Added By エインドリ－・プ－プゥ 
                        }
                        else //3.Send Mail
                        {
                            if (tb_pwd.Text == tb_pwdConfirm.Text) //20211213 Updated エインドリ－・プ－プゥ
                            {
                                //20220322 Update JOBZグラウド -> 見えるJOBZ in mail エインドリ－
                                #region　メール送信
                                String url = HttpContext.Current.Request.Url.AbsoluteUri;
                                url = url.Substring(0, url.Length - 17);
                                //url = url + "JC04Mailkakunin.aspx?id=" + strGuid;
                                url = url + "JC01Login.aspx?id=true";
                                string from, pass, messageBody = "";
                                MailMessage message = new MailMessage();
                                to = tb_login.Text;
                                from = ConfigurationManager.AppSettings["SMTP_Sender"];
                                pass = ConfigurationManager.AppSettings["SMTP_Password"];
                                messageBody = "この度は見えるJOBZにご登録頂きまして誠にありがとうございます。" + "\n" + "２４時間以内に下記のリンクをクリックして認証を行ってください。" + "\n" + url;
                                message.To.Add(to);
                                //message.From = new MailAddress(from, "JOBZグラウド 運営チーム");
                                message.From = new MailAddress(from, ConfigurationManager.AppSettings["SMTP_SenderName"]); //20211203 Updated エインドリ－・プ－プゥ
                                message.Body = messageBody;
                                message.Subject = "見えるJOBZのメールアドレスの認証";
                                //SmtpClient smtp = new SmtpClient("smtp.gmail.com");
                                SmtpClient smtp = new SmtpClient();
                                smtp.Host = ConfigurationManager.AppSettings["SMTP_Host"];
                                //smtp.Timeout = 300000;
                                smtp.Timeout = int.Parse(ConfigurationManager.AppSettings["SMTP_Timeout"]);
                                smtp.EnableSsl = true;
                                //smtp.Port = 587;
                                smtp.Port = int.Parse(ConfigurationManager.AppSettings["SMTP_Port"]);
                                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                                smtp.UseDefaultCredentials = false;
                                ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                                smtp.Credentials = new NetworkCredential(from, pass);
                                smtp.EnableSsl = true;
                                //20211203 Updated エインドリ－・プ－プゥ (End)
                                try
                                {
                                    tomail = tb_login.Text;
                                    smtp.Send(message);
                                    JC01Login.is_tantou = true;
                                    JC01Login.ID = mail;
                                    JC01Login.stantou = tb_tantoumei.Text;
                                    JC01Login.cbumon = lblcBumon.Text;
                                    JC01Login.mail = tb_login.Text;
                                    JC01Login.pwd = TextUtility.EncryptData_Henkou(tb_pwd.Text);
                                    JC01Login.chenkousha = JC99NavBar_Class.Login_Tan_Code;
                                    JC01Login.ckyoten = lblcKYOTEN.Text;
                                    JC01Login.ckengenn = lblcKengen.Text;
                                    if (lblcKengen.Text == "01")
                                    {
                                        JC01Login.fkanrisha = "1";
                                    }
                                    else
                                    {
                                        JC01Login.fkanrisha = "0";
                                    }
                                    if (btnEnroll.BackColor == Color.LightGray)
                                    {
                                        JC01Login.ftaisya = "0";
                                    }
                                    else
                                    {
                                        JC01Login.ftaisya = "1";
                                        if (lbldTantou.Text != "")
                                        {
                                            JC01Login.dtaisya = lbldTantou.Text.ToString();
                                        }
                                        else
                                        {
                                            JC01Login.dtaisya = "";
                                        }
                                    }
                                    //4.Show Alert Message
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowMailKakuninMessage",
                                    "MailKakuninMsgBox('まだ、本登録できておりません。<br/> 入力されたメールアドレスに認証リンクを送付しました。<br/>メールアプリにてご確認の上、リンクをクリックして認証を行ってください。','"+btn_cancel.ClientID+"');", true);
                                   
                                }
                                catch (Exception ex)
                                {
                                    Response.Write("<script>alert('" + ex.Message + "');</script>");
                                }
                                #endregion
                            }
                            else
                            {
                                //ScriptManager.RegisterStartupScript(this, this.GetType(), "alerts", "javascript:alert('パスワードが一致していません。')", true);
                                //ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowErrorMessage",
                                //"ShowErrorMessage('パスワードが一致していません。');", true);
                                //20211222 Added By エインドリ－・プ－プゥ 
                                tb_pwdConfirm.Text = "";
                                tb_pwdConfirm.Text = "";
                                LB_pwd_Error.Text = "パスワードが一致していません。";            
                                tb_pwd.Focus();
                                updTokuisakiDate.Update();
                            }
                        }
                    }
                    #endregion
                }


            }
            //20211209 Added By エインドリ－・プ－プゥ (End)

            //ViewState["DATETIME"] = btnTantouDate.ID;
            //if (tb_pwd.Text == "")
            //{
            //    tb_pwd.BackColor = Color.FromArgb(255, 255, 128);
            //    tb_pwd.Focus();
            //}
            //else
            //{
            //    tb_pwd.BackColor = Color.White;
            //}

            //JC_ClientConnecction_Class jc = new JC_ClientConnecction_Class();
            //jc.loginId = Session["LoginId"].ToString();
            //con = jc.GetConnection();
            //con.Open();
            //string insert = "INSERT INTO m_j_tantousha (cTANTOUSHA,SMAIL,sTANTOUSHA,sPWD,cBUMON,cHENKOUSYA,dHENKOU) " +
            //           "VALUES (@cTANTOUSHA,@SMAIL,@sTANTOUSHA,@sPWD,@cBUMON,@cHENKOUSYA,@dHENKOU)";
            //cmd = new MySqlCommand(insert, con);
            ////cmd.Parameters.AddWithValue("@cTANTOUSHA", lbl_ctantou.Text);
            //cmd.Parameters.AddWithValue("@SMAIL", tb_login.Text);
            //cmd.Parameters.AddWithValue("@sTANTOUSHA", tb_tantoumei.Text);
            //cmd.Parameters.AddWithValue("@sPWD", Encrypt(tb_pwd.Text));
            ////cmd.Parameters.AddWithValue("@cBUMON", TextBox1.Text);
            ////cmd.Parameters.AddWithValue("@cHENKOUSYA", TextBox2.Text);
            //cmd.ExecuteNonQuery();

            //Response.Write("<script type=\"text/javascript\">alert('登録しました。')</script>");
            //con.Close();

            //20211209 Close Comment By エインドリ－・プ－プゥ

            //if (DataSave())
            //{
            //    Response.Write("<script>alert('登録出来ます。')</script>");
            //}
            //else
            //{
            //    Response.Write("<script>alert('登録出来ません。')</script>");
            //}
        }
        #endregion
        public void logout()
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "parentButtonClick('btnClose1','" + hdnHome.Value + "');", true);
        }

        #region DataSave
        public bool DataSave()
        {
            String id = Request.QueryString["id"];
            DataTable dt_db = JC01Login_Class.Get_DB(id);
            String db = dt_db.Rows[0]["db"].ToString();
            String mail = dt_db.Rows[0]["mail"].ToString();
            ConstantVal.DB_NAME = db;

            JC15TantouTouroku_Class JC15_Class = new JC15TantouTouroku_Class();
            JC15_Class.loginID = mail;
            JC15_Class.sTANTOUSHA = tb_tantoumei.Text;
            String cBumon = "null";
            if (!String.IsNullOrEmpty(lblcBumon.Text))
            {
                cBumon = "'" + lblcBumon.Text + "'";
            }
            JC15_Class.cBUMON = cBumon;
            JC15_Class.SMAIL = tb_login.Text;
            JC15_Class.sPWD = TextUtility.EncryptData_Henkou(tb_pwd.Text);
            JC15_Class.cHENKOUSYA = JC99NavBar_Class.Login_Tan_Code;
            JC15_Class.is_edited = clicked_edit == true ? true : false;//20211208 Added エインドリ－・プ－プゥ
            //JC15_Class.fKANRISHA = "0";
            //JC15_Class.fTAISYA = "0";
            String cKyoten = "null";
            if (!String.IsNullOrEmpty(lblcKYOTEN.Text))
            {
                cKyoten = "'" + lblcKYOTEN.Text + "'";
            }
            JC15_Class.cKYOTEN = cKyoten;
            String cKengen = "null";
            if (!String.IsNullOrEmpty(lblcKengen.Text))
            {
                cKengen = "'" + lblcKengen.Text + "'";
            }
            JC15_Class.ckengenn = cKengen;
            if (lblcKengen.Text == "01")
            {
                JC15_Class.fKANRISHA = "1";
            }
            else
            {
                JC15_Class.fKANRISHA = "0";
            }
            if (btnEnroll.BackColor == Color.LightGray)
            {
                JC15_Class.fTAISYA = "0";
            }
            else
            {
                JC15_Class.fTAISYA = "1";
                if (lbldTantou.Text != "")
                {
                    JC15_Class.dTAISYA = lbldTantou.Text.ToString();
                }
                else
                {
                    JC15_Class.dTAISYA = "";
                }
            }
            //20211208 Added エインドリ－・プ－プゥ
            if (clicked_edit == false)
            {
                if (!JC15_Class.Tantou_Create())
                { return false; }
                return true;
            }
            else
            {
                if (!JC15_Class.Tantou_Update())
                { return false; }
                return true;
            }
        }
        #endregion

        #region show_message()
        public void show_message(string msg)
        {
            Response.Write("<script>alert('" + msg + "')</script>");
        }
        #endregion

        #region Encrypt
        public string Encrypt(string plainText)
        {
            string sKey;
            byte[] bKey;    //3DESの暗号キー
            byte[] bVector;//3DESのVector
            byte[] bEnc_Bfor;//暗号文 暗号化前
            byte[] bEnc;//暗号文

            TripleDES tdes = TripleDES.Create();
            // 暗号キーの取得
            sKey = GenerateKeyFromPassword("demo20", tdes.KeySize, tdes.BlockSize);
            if (sKey == null) return null;//エラー処理
                                          //string→bytes
            bEnc_Bfor = Encoding.UTF8.GetBytes(plainText);
            bKey = Convert.FromBase64String(sKey);
            bVector = Encoding.Default.GetBytes("00000000");

            //パラメータ設定
            tdes.IV = bVector;
            tdes.Key = bKey;
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;

            // encryption
            ICryptoTransform ict = tdes.CreateEncryptor();
            bEnc = ict.TransformFinalBlock(bEnc_Bfor, 0, bEnc_Bfor.Length);

            return Convert.ToBase64String(bEnc, 0, bEnc.Length);
        }
        #endregion

        #region Decrypt
        public string Decrypt(string encryptpwd)
        {
            string sKey;
            byte[] bKey;//3DESの暗号キー
            byte[] bVector;//3DESのVector
            byte[] bEnc;//復号文
            byte[] bDnc;//暗号文 

            TripleDES tdes = TripleDES.Create();
            // 暗号キーの取得

            sKey = GenerateKeyFromPassword("demo20", tdes.KeySize, tdes.BlockSize);
            if (sKey == null) return null;//エラー処理
                                          //string→bytes
            bDnc = Convert.FromBase64String(encryptpwd);
            bKey = Convert.FromBase64String(sKey);
            bVector = Encoding.Default.GetBytes("00000000");

            //パラメータ設定
            tdes.IV = bVector;
            tdes.Key = bKey;
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;

            // encryption
            ICryptoTransform ict = tdes.CreateEncryptor();
            ict = tdes.CreateDecryptor();
            bEnc = ict.TransformFinalBlock(bDnc, 0, bDnc.Length);
            //
            return Encoding.UTF8.GetString(bEnc);
        }
        #endregion

        #region GenerateKeyFromPassword
        private static string GenerateKeyFromPassword(string password, int keySize, int blockSize)
        {
            try
            {
                //パスワードから共有キーと初期化ベクタを作成する
                //saltを決める
                byte[] salt = System.Text.Encoding.UTF8.GetBytes("saltは必ず8バイト以上");
                //Rfc2898Derivebytesオブジェクトを作成する
                System.Security.Cryptography.Rfc2898DeriveBytes derivebytes =
                    new System.Security.Cryptography.Rfc2898DeriveBytes(password, salt);
                //.NET Framework 1.1以下の時は、PasswordDerivebytesを使用する
                //System.Security.Cryptography.PasswordDerivebytes derivebytes =
                //    new System.Security.Cryptography.PasswordDerivebytes(password, salt);
                //反復処理回数を指定する デフォルトで1000回
                derivebytes.IterationCount = 1000;

                //共有キーと初期化ベクタを生成する
                byte[] key;
                string sKey;
                key = derivebytes.GetBytes(keySize / 8);
                sKey = Convert.ToBase64String(key);
                return sKey;
                //iv = derivebytes.GetBytes(blockSize / 8);
                //iv = Encoding.Default.GetBytes("00000000");
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region date
        protected void btnTantouDate_Click(object sender, EventArgs e)
        {
            SessionUtility.SetSession("HOME", "Popup");
            ifDatePopup.Style["max-width"] = "260px";
            ifDatePopup.Style["height"] = "365px";

            String id = Request.QueryString["id"];
            ifDatePopup.Src = "JCHidukeSelect.aspx?id=" + id;
            mpeDatePopup.Show();
            
            ViewState["DATETIME"] = btnTantouDate.ID;

            if (!String.IsNullOrEmpty(lbldTantou.Text))
            {
                DateTime dt = DateTime.Parse(lbldTantou.Text);
                Session["CALENDARDATE"] = dt.ToString("yyyy/MM/dd");
                Session["YEAR"] = dt.Year;
            }

            // 設定したラベルをクリック時、時間設定ポップアップ画面を表示する
            lbldTantou.Attributes.Add("onClick", "BtnClick('btnTantouDate')");
            updDatePopup.Update();
        }
        #endregion

        #region btndTantouCross_Click
        protected void btndTantouCross_Click(object sender, EventArgs e)
        {
            lbldTantou.Text = "";
            btnTantouDate.Style["display"] = "block";
            divTantouDate.Style["display"] = "none";
            updTokuisakiDate.Update();
            updTantoudate.Update();
        }
        #endregion

        #region btnRightArrowdTantou_Click
        protected void btnRightArrowdTantou_Click(object sender, EventArgs e)
        {
            DateTime sdate = Convert.ToDateTime(lbldTantou.Text);
            sdate = sdate.AddDays(1);
            lbldTantou.Text = sdate.ToString("yyyy/MM/dd");
        }
        #endregion

        #region btnLeftArrowdTantou_Click
        protected void btnLeftArrowdTantou_Click(object sender, EventArgs e)
        {
            DateTime sdate = Convert.ToDateTime(lbldTantou.Text);
            sdate = sdate.AddDays(-1);
            lbldTantou.Text = sdate.ToString("yyyy/MM/dd");
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

            if (strBtnID == btnTantouDate.ID)
            {
                if (btnTantouDate.Style["display"] != "none")
                {
                    btnTantouDate.Focus();
                }
                else
                {
                    btnRightArrowdTantou.Focus();
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
                if (strBtnID == btnTantouDate.ID)
                {
                    TokuisakiDateDataBind(strCalendarDateTime, strBtnID);
                }

                //lblHdnAnkenTextChange.Text = "true";
            }
            CalendarFoucs();
        }

        #endregion

        #region "見積日データバインディング処理"
        protected void TokuisakiDateDataBind(string strCalendarDateTime, string strImgbtnID)
        {
            DateTime dtDeliveryDate = DateTime.Parse(strCalendarDateTime);
            lbldTantou.Text = dtDeliveryDate.ToString("yyyy/MM/dd");
            btnTantouDate.Style["display"] = "none";
            divTantouDate.Style["display"] = "block";
            updTokuisakiDate.Update();
            updTantoudate.Update();
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
       
        #region btnKyotenSelect_Click()
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
                upd_KYOTENLIST.Update();

                //BT_Save.Enabled = true;
                HF_isChange.Value = "1";
            }

            ifSentakuPopup.Src = "";
            mpeSentakuPopup.Hide();
            updSentakuPopup.Update();
        }
        #endregion

        #region btnOn_Click
        protected void btnOn_Click(object sender, EventArgs e)
        {
            //btnEnroll.BorderWidth = 3;
            btnEnroll.BackColor = Color.LightGray;
            //btnLeave.BorderWidth = 2;
            btnLeave.BackColor = Color.White;
            DLeave.Visible = false;
            updTantoudate.Visible = false;
            updTokuisakiDate.Update();
        }
        #endregion

        #region btnOff_Click
        protected void btnOff_Click(object sender, EventArgs e)
        {
            //btnLeave.BorderWidth = 3;
            btnLeave.BackColor = Color.LightGray;
            btnEnroll.BackColor = Color.White;
            //btnEnroll.BorderWidth = 2;
            DLeave.Visible = true;
            updTantoudate.Visible = true;
            updTokuisakiDate.Update();
        }
        #endregion

        #region btn_cross_Click
        protected void btn_cross_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "parentButtonClick('btnClose1','"+hdnHome.Value+"');", true);
        }
        #endregion

        #region btn_cancel_Click
        protected void btn_cancel_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "parentButtonClick('btnClose1','"+hdnHome.Value+"');", true);
        }
        #endregion

        #region BT_Kyoten_Add_Click()
        protected void BT_Kyoten_Add_Click(object sender, EventArgs e)
        {
            SessionUtility.SetSession("HOME", "Popup");
            //ifSentakuPopup.Style["width"] = "470px";
            //ifSentakuPopup.Style["height"] = "620px";
            ifSentakuPopup.Style["width"] = "100vw";
            ifSentakuPopup.Style["height"] = "100vh";

            String id = Request.QueryString["id"];
            ifSentakuPopup.Src = "JC25KyotenList.aspx?id=" + id;
            mpeSentakuPopup.Show();

            lblsKYOTEN.Attributes.Add("onClick", "BtnClick('btnKyoten')");
            updSentakuPopup.Update();

        }
        #endregion

        #region BT_sKYOTENLIST_Cross_Click
        protected void BT_sKYOTENLIST_Cross_Click(object sender, EventArgs e)
        {
            lblsKYOTEN.Text = "";
            lblcKYOTEN.Text = "";
            divKyotenbtn.Style["display"] = "block";
            divKyotenLabel.Style["display"] = "none";
            // BT_Save.Enabled = true;
            HF_isChange.Value = "1";
            upd_KYOTENLIST.Update();
        }
        #endregion

        #region btnBumon_Click
        protected void btnBumon_Click(object sender, EventArgs e)
        {
            SessionUtility.SetSession("HOME", "Popup");
            ifSentakuPopup.Style["width"] = "470px";
            ifSentakuPopup.Style["height"] = "620px";
            String id = Request.QueryString["id"];
            ifSentakuPopup.Src = "JC25BumonList.aspx?id=" + id;
            mpeSentakuPopup.Show();

            lblsBumon.Attributes.Add("onClick", "BtnClick('btnBumon')");
            updSentakuPopup.Update();
        }
        #endregion

        #region btnBumonSelect_Click()
        protected void btnBumonSelect_Click(object sender, EventArgs e)
        {
            if (Session["cBumon"] != null)
            {
                string cBumon = (string)Session["cBumon"];
                string sBumon = (string)Session["sBumon"];
                lblcBumon.Text = cBumon;
                lblsBumon.Text = sBumon.Replace("<", "&lt").Replace(">", "&gt");
                divBumonbtn.Style["display"] = "none";
                divBumonLabel.Style["display"] = "block";
                upd_Bumon.Update();

                //BT_Save.Enabled = true;
                HF_isChange.Value = "1";
            }

            ifSentakuPopup.Src = "";
            mpeSentakuPopup.Hide();
            updSentakuPopup.Update();
        }
        #endregion

        #region BT_sBumon_Cross_Click
        protected void BT_sBumon_Cross_Click(object sender, EventArgs e)
        {
            lblcBumon.Text = "";
            lblsBumon.Text = "";
            divBumonbtn.Style["display"] = "block";
            divBumonLabel.Style["display"] = "none";
            // BT_Save.Enabled = true;
            HF_isChange.Value = "1";
            upd_Bumon.Update();
        }
        #endregion

        #region btnKengen_Click
        protected void btnKengen_Click(object sender, EventArgs e)
        {
            SessionUtility.SetSession("HOME", "Popup");
            ifSentakuPopup.Style["width"] = "440px";
            ifSentakuPopup.Style["height"] = "450px";
            String id = Request.QueryString["id"];
            ifSentakuPopup.Src = "JC25Kengenn.aspx?id=" + id;
            mpeSentakuPopup.Show();

            lblsKengen.Attributes.Add("onClick", "BtnClick('btnKengen')");
            updSentakuPopup.Update();
        }
        #endregion

        #region btnKengennSelect_Click()
        protected void btnKengennSelect_Click(object sender, EventArgs e)
        {
            if (Session["cKengenn"] != null)
            {
                string cKengenn = (string)Session["cKengenn"];
                string sKengenn = (string)Session["sKengenn"];
                lblcKengen.Text = cKengenn;
                lblsKengen.Text = sKengenn.Replace("<", "&lt").Replace(">", "&gt");
                divKengenBtn.Style["display"] = "none";
                divKengenLabel.Style["display"] = "block";
                upd_Kengen.Update();

                //BT_Save.Enabled = true;
                HF_isChange.Value = "1";
            }

            ifSentakuPopup.Src = "";
            mpeSentakuPopup.Hide();
            updSentakuPopup.Update();
        }
        #endregion

        #region BT_sKengen_Cross_Click
        protected void BT_sKengen_Cross_Click(object sender, EventArgs e)
        {
            lblsKengen.Text = "";
            lblcKengen.Text = "";
            divKengenBtn.Style["display"] = "block";
            divKengenLabel.Style["display"] = "none";
            // BT_Save.Enabled = true;
            HF_isChange.Value = "1";
            upd_Kengen.Update();
        }
        #endregion
    }
}