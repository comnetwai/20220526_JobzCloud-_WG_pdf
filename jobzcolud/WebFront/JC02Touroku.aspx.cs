using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Service;
using MySql.Data.MySqlClient;
using Common;
using System.Configuration;

namespace jobzcolud
{
    public partial class JC02Touroku : System.Web.UI.Page
    {
        #region "変数"

        /// <summary>
        /// 登録画面の変数
        /// </summary>
        private static Random random = new Random();

        #endregion

        public static string loginid, tantoumei, psw, repsw, gaishamei, yuubenbangou, juusho1, juusho2, denwa;
        public static string to, tomail;
        String strGuid = Guid.NewGuid().ToString();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (DBUtilitycs.Server == "")
                {
                    get_connect_data();
                }
                Div_tourokukakunin.Attributes["style"] = "display:none";
                TB_Login_Id.Focus();
            }
            else
            {
                LB_mailerror.Text = "";
                LB_Yuubenbangouerror.Text = "";
                LB_DenwaError.Text = "";
            }
        }
        private void get_connect_data()
        {
            DBUtilitycs.get_connetion_ifo();
        }

        protected void BT_kakunin_Click(object sender, EventArgs e)
        {
            TB_Login_Id.Style["border-color"] = "none";
            TB_Usermei.Style["border-color"] = "none";
            TB_password.Style["border-color"] = "none";
            TB_repassword.Style["border-color"] = "none";
            TB_Kaishamei.Style["border-color"] = "none";
            TB_Yuubenbangou.Style["border-color"] = "none";
            TB_Juusho1.Style["border-color"] = "none";
            TB_Denwa.Style["border-color"] = "none";
            LB_pwderror.Text = string.Empty;

            if (LB_mailerror.Text == "" && TB_Yuubenbangou.Text != "")
            {
                TB_Yuubenbangou_TextChanged(sender, e);

            }
            if (LB_mailerror.Text == "" && LB_Yuubenbangouerror.Text == "" && TB_Denwa.Text != "")
            {
                TB_Denwa_TextChanged(sender, e);
            }

            if (TB_Login_Id.Text == "" || TB_Usermei.Text == "" || TB_password.Text == "" || TB_repassword.Text == "" || TB_Kaishamei.Text == "" || TB_Yuubenbangou.Text == "" || TB_Juusho1.Text == "" || TB_Denwa.Text == "")
            {
                if (TB_Denwa.Text == "")
                {
                    TB_Denwa.Style.Add("border-color", "red");
                    TB_Denwa.Focus();
                }
                if (TB_Juusho1.Text == "")
                {
                    TB_Juusho1.Style.Add("border-color", "red");
                    TB_Juusho1.Focus();
                }
                if (TB_Yuubenbangou.Text == "")
                {
                    TB_Yuubenbangou.Style.Add("border-color", "red");
                    TB_Yuubenbangou.Focus();
                }
                if (TB_Kaishamei.Text == "")
                {
                    TB_Kaishamei.Style.Add("border-color", "red");
                    TB_Kaishamei.Focus();
                }
                if (TB_repassword.Text == "")
                {
                    TB_repassword.Style.Add("border-color", "red");
                    TB_repassword.Focus();
                }
                if (TB_password.Text == "")
                {
                    TB_password.Style.Add("border-color", "red");
                    TB_password.Focus();
                }
                if (TB_Usermei.Text == "")
                {
                    TB_Usermei.Style.Add("border-color", "red");
                    TB_Usermei.Focus();
                }
                if (TB_Login_Id.Text == "")
                {
                    TB_Login_Id.Style.Add("border-color", "red");
                    TB_Login_Id.Focus();
                }
            }

            else
            {
                if (!TextUtility.IsValidEmailAddress(TB_Login_Id.Text))
                {
                    // エラーメセジを表示
                    LB_mailerror.Text = "適切なメールアドレスではありません。";
                    TB_Login_Id.Focus();
                }
                else
                {
                    if (JC02Touroku_Class.MailAcc_Check(TB_Login_Id.Text))
                    {
                        LB_mailerror.Text = "このメールアドレスはすでに会員登録で使用されているメールアドレスです。";

                        TB_Login_Id.Focus();
                    }

                    else if (TB_password.Text != TB_repassword.Text)
                    {
                        LB_pwderror.Text = "パスワードが一致していません。";
                        TB_password.Focus();
                        TB_password.Text = string.Empty;
                        TB_repassword.Text = string.Empty;
                    }

                    else if (TB_password.Text.Length < 8)
                    {
                        LB_pwderror.Text = "パスワードは8文字以上で登録してください。";
                        TB_password.Focus();
                        TB_password.Text = string.Empty;
                        TB_repassword.Text = string.Empty;
                    }
                    else
                    {
                        Div_tourokukakunin.Attributes["style"] = "display:block";
                        Div_touroku.Attributes["style"] = "display:none";

                        LB_loginid.Text = TB_Login_Id.Text;
                        LB_usermei.Text = TB_Usermei.Text;
                        LB_password.Text = "".PadLeft(TB_password.Text.Count(), '*');
                        LB_repassword.Text = "".PadLeft(TB_repassword.Text.Count(), '*');
                        LB_gaishamei.Text = TB_Kaishamei.Text;
                        LB_yuubenbangou.Text = TB_Yuubenbangou.Text;
                        LB_juusho1.Text = TB_Juusho1.Text;
                        LB_juusho2.Text = TB_Juusho2.Text;
                        LB_denwa.Text = TB_Denwa.Text;
                    }
                }
            }

        }

        protected void TB_Login_Id_TextChanged(object sender, EventArgs e)
        {
            if (TextUtility.IsIncludeZenkaku(TB_Login_Id.Text))
            {
                LB_DenwaError.Text = "";
                LB_Yuubenbangouerror.Text = "";
                // 【半角英数を入力してください。】というメセジを表示
                LB_mailerror.Text = "半角英数を入力してください。";
                TB_Login_Id.Text = "";
                TB_Login_Id.Focus();
            }
            else
            {
                TB_Login_Id.Text = TextUtility.GetMaxLengthCharacterString(TB_Login_Id.Text, ConstantVal.EMAIL_MAXLENGTH);
                LB_mailerror.Text = "";
            }
        }

        protected void TB_Denwa_TextChanged(object sender, EventArgs e)
        {
            if (TextUtility.IsIncludeZenkaku(TB_Denwa.Text))
            {
                if (LB_mailerror.Text == "" && LB_Yuubenbangouerror.Text == "")
                {
                    LB_DenwaError.Text = "半角英数を入力してください。";
                    TB_Denwa.Text = "";
                    TB_Denwa.Focus();
                }
            }
            else if (TB_Denwa.Text != "")
            {
                if (!TextUtility.IsPhoneNumber(TB_Denwa.Text))
                {
                    LB_DenwaError.Text = "正しくお電話を入力してください";
                    TB_Denwa.Text = "";
                    TB_Denwa.Focus();
                }
            }
            else
            {
                LB_DenwaError.Text = "";
            }
        }

        protected void TB_Yuubenbangou_TextChanged(object sender, EventArgs e)
        {
            if (TextUtility.IsIncludeZenkaku(TB_Yuubenbangou.Text))
            {
                if (LB_mailerror.Text == "" && LB_DenwaError.Text == "")
                {
                    LB_Yuubenbangouerror.Text = "半角英数を入力してください。";
                    TB_Yuubenbangou.Text = "";
                    TB_Yuubenbangou.Focus();
                }
            }
            else
            {
                LB_Yuubenbangouerror.Text = "";
            }
        }

        protected void BT_modoru_Click(object sender, EventArgs e)
        {
            Div_tourokukakunin.Attributes["style"] = "display:none";
            Div_touroku.Attributes["style"] = "display:block";
        }

        #region customer_id取得
        /// <summary>
        /// 半角英数の乱数文字を取得
        /// <param name="length">文字の数</param>
        /// <returns>true/false</returns>
        /// </summary>
        public static string getRandomAlphanumeric(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        #endregion

        #region アカウント作成
        /// <summary>
        /// DB「ContactsとCustomers」に入力情報を入れる
        /// <returns>true/false</returns>
        /// </summary>
        private bool Insert_Data()
        {
            string customer_id = getRandomAlphanumeric(16);

            return JC02Touroku_Class.Create_Acc(TB_Login_Id.Text, TB_Usermei.Text
                , TextUtility.EncryptData_Henkou(TB_password.Text), customer_id
                , TB_Kaishamei.Text, TB_Yuubenbangou.Text
                , TB_Juusho1.Text, TB_Juusho2.Text
                , TB_Denwa.Text, strGuid);
        }
        #endregion

        protected void BT_sakusei_Click(object sender, EventArgs e)
        {
            if (Insert_Data())   //アカウント作成
            {
                #region　メール送信
                String url = HttpContext.Current.Request.Url.AbsoluteUri;
                url = url.Substring(0, url.Length - 11);
                url = url + "JC04Mailkakunin.aspx?id=" + strGuid;
                string from, pass, messageBody = "";
                MailMessage message = new MailMessage();
                to = LB_loginid.Text;
                from = ConfigurationManager.AppSettings["SMTP_Sender"];
                pass = ConfigurationManager.AppSettings["SMTP_Password"];
                messageBody = "この度は見えるJOBZにご登録頂きまして誠にありがとうございます。" + "\n" + "２４時間以内に下記のリンクをクリックして認証を行ってください。" + "\n" + url;
                message.To.Add(to);
                message.From = new MailAddress(from, ConfigurationManager.AppSettings["SMTP_SenderName"]);
                message.Body = messageBody;
                message.Subject = "見えるJOBZのメールアドレスの認証";
                SmtpClient smtp = new SmtpClient();
                smtp.Host = ConfigurationManager.AppSettings["SMTP_Host"];
                smtp.Timeout = int.Parse(ConfigurationManager.AppSettings["SMTP_Timeout"]);
                smtp.EnableSsl = true;
                smtp.Port = int.Parse(ConfigurationManager.AppSettings["SMTP_Port"]);
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.UseDefaultCredentials = false;
                ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                smtp.Credentials = new NetworkCredential(from, pass);
                smtp.EnableSsl = true;
                try
                {
                    tomail = LB_loginid.Text;
                    smtp.Send(message);
                    Response.Redirect("JC03Mail.aspx");
                }
                catch (Exception ex)
                {
                    Response.Write("<script>alert('" + ex.Message + "');</script>");
                }
                #endregion
            }
        }


    }
}