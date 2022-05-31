using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;
using Service;
using System.Dynamic;
using System.Net.Mail;
using System.Data;
using System.Net;
using System.Configuration;

namespace jobzcolud.WebFront
{
    public partial class JC05Passwordreset : System.Web.UI.Page
    {
        public string to, get_email;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if(DBUtilitycs.Server=="")
                {
                    DBUtilitycs.get_connetion_ifo();
                }
                Div_mailsend.Attributes["style"] = "display:none";
                Div_pwdreset.Attributes["style"] = "display:none";
                BT_Mailsend.Focus();
                if (!String.IsNullOrEmpty(Request.QueryString["val1"]))
                {
                    Div_mail.Attributes["style"] = "display:none";
                    Div_mailsend.Attributes["style"] = "display:none";
                    Div_pwdreset.Attributes["style"] = "display:block";
                }
            }
        }

        protected void BT_Mailsend_Click(object sender, EventArgs e)
        {
            TB_Email.Style["border-color"] = "none";
            LB_Error.Text = "";
            if (String.IsNullOrEmpty(TB_Email.Text))
            {
                TB_Email.Style.Add("border-color", "red !important");
                TB_Email.Text = "";
                TB_Email.Focus();
            }
            else if (!TextUtility.IsValidEmailAddress(TB_Email.Text))
            {
                // エラーメセジを表示
                LB_Error.Text = "適切なメールアドレスではありません。";
                TB_Email.Focus();
            }
            else
            {
                if (JC02Touroku_Class.MailAcc_Check(TB_Email.Text))
                {
                    LB_Error.Text = "";
                    String strFlag = Request.QueryString["val"];
                    #region　メール送信
                    //String strGuid = Guid.NewGuid().ToString();
                    String url = HttpContext.Current.Request.Url.AbsoluteUri;
                    string from, pass, to = "";
                    MailMessage message = new MailMessage();
                    to = TB_Email.Text;
                    from = ConfigurationManager.AppSettings["SMTP_Sender"];
                    pass = ConfigurationManager.AppSettings["SMTP_Password"];
                    message.To.Add(to);
                    message.From = new MailAddress(from, ConfigurationManager.AppSettings["SMTP_SenderName"]);
                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = ConfigurationManager.AppSettings["SMTP_Host"];
                    smtp.EnableSsl = true;
                    smtp.Port = int.Parse(ConfigurationManager.AppSettings["SMTP_Port"]);
                    smtp.Timeout = int.Parse(ConfigurationManager.AppSettings["SMTP_Timeout"]);
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.UseDefaultCredentials = false;
                    ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                    smtp.Credentials = new NetworkCredential(from, pass);
                    smtp.EnableSsl = true;
                    if (strFlag != ConstantVal.ZERO_STRING)
                    {
                        DataTable dt_id = JC05Passwordreset_Class.GetResetId(TB_Email.Text);
                        String strUrl = HttpContext.Current.Request.Url.AbsoluteUri;
                        strUrl = strUrl.Substring(0, strUrl.LastIndexOf('/')) + "/";
                        strUrl = strUrl + "JC05Passwordreset.aspx?val1=" + TextUtility.EncryptData_Henkou(dt_id.Rows[0]["id"].ToString()) + "&gid=" + TextUtility.EncryptData_Henkou("JC05");
                        message.IsBodyHtml = true;
                        message.Subject = "パスワードリセット";
                        message.Body = "<b>" + dt_id.Rows[0]["contact_name"].ToString() + "様</b><br><br>";
                        message.Body += "下のリンクからパスワードをリセットしてください。<br><br>";
                        message.Body += "<form method='POST' ><center><table><tr>";
                        message.Body += "<td style='height:39px; background:#29abe2;border-radius:6px;'>";
                        message.Body += "<a href='" + strUrl + "' style='border: 1px solid #29abe2;border-radius:6px;color:#ffffff; text-decoration: none; cursor: pointer; padding: 7px; font-size:16px; font-weight:bold;'>パスワードをリセット</a>";
                        message.Body += "</td></tr></table></center></form><br>";
                        message.Body += "パスワードの変更を希望しない場合はこのメールは破棄してください。<br><br>";
                        message.Body += "上のリンクからパスワードを変更しない限り、現在のパスワードが有効です。<br><br>";
                        //message.Body += "見えるJOBZ 運営チーム<br><br>";
                        message.Body += ConfigurationManager.AppSettings["SMTP_SenderName"] + "<br><br>";
                    }
                    try
                    {
                        smtp.Send(message);
                        Div_mailsend.Attributes["style"] = "display:block";
                        LB_Mailaddress.Text = TB_Email.Text;
                        Div_mail.Attributes["style"] = "display:none";
                    }
                    catch (Exception ex)
                    {
                        Response.Write("<script>alert('" + ex.Message + "');</script>");
                    }
                    
                    #endregion
                }
                else
                {
                    //LB_Error.Text = "このメールアドレスはすでに社員登録で使用されていないメールアドレスです。";
                    LB_Error.Text = "このメールアドレスは登録されていません。";
                }
            }
        }

        protected Boolean HaveAllData()
        {
            // 必要があるテキストボックスにデータがない場合、borderの色を赤いで表示する。
            if (String.IsNullOrEmpty(TB_Password.Text) || String.IsNullOrEmpty(TB_Repassword.Text))
            {
                TB_Password.Style.Add("border-color", "red");
                TB_Repassword.Style.Add("border-color", "red");
                TB_Password.Text = string.Empty;
                TB_Repassword.Text = string.Empty;
                TB_Password.Focus();
                return false;
            }
            return true;
        }

        private bool Update_Password()
        {
            string strUserId = TextUtility.DecryptData_Henkou(Request.QueryString["val1"]);
            return JC05Passwordreset_Class.ResetPassword(strUserId
                , TextUtility.EncryptData_Henkou(TB_Password.Text),to );
        }

        protected void BT_Pwdhenkou_Click(object sender,EventArgs e)
        {
            TB_Password.Style["border-color"] = "none";
            TB_Repassword.Style["border-color"] = "none";
            LB_Pwderror.Text = "";
            if (HaveAllData())
            {
                if (!TB_Password.Text.Equals(TB_Repassword.Text))
                {
                    LB_Pwderror.Text = "パスワードが一致していません。";
                    TB_Password.Focus();
                    TB_Password.Text = string.Empty;
                    TB_Repassword.Text = string.Empty;
                }

                else if (TB_Password.Text.Length < 8)
                {
                    LB_Pwderror.Text = "パスワードは8文字以上で登録してください。";
                    TB_Password.Focus();
                    TB_Password.Text = string.Empty;
                    TB_Repassword.Text = string.Empty;
                }
                else
                {
                    string strUserId = TextUtility.DecryptData_Henkou(Request.QueryString["val1"]);
                    DataTable dt_mail = JC05Passwordreset_Class.GetResetMail(strUserId);
                    to = dt_mail.Rows[0]["contact_email"].ToString();

                    if (Update_Password())
                    {
                        //string strUserId = TextUtility.DecryptData_Henkou(Request.QueryString["val1"]);
                        //DataTable dt_mail = JC05Passwordreset_Class.GetResetMail(strUserId);
                        String strFlag = Request.QueryString["val"];
                        String url = HttpContext.Current.Request.Url.AbsoluteUri;
                        string from, pass, to = "";
                        MailMessage message = new MailMessage();
                        to = dt_mail.Rows[0]["contact_email"].ToString();
                        from = ConfigurationManager.AppSettings["SMTP_Sender"];
                        pass = ConfigurationManager.AppSettings["SMTP_Password"];
                        message.To.Add(to);
                        message.From = new MailAddress(from, ConfigurationManager.AppSettings["SMTP_SenderName"]);
                        SmtpClient smtp = new SmtpClient();
                        smtp.Host = ConfigurationManager.AppSettings["SMTP_Host"];
                        smtp.EnableSsl = true;
                        smtp.Port = int.Parse(ConfigurationManager.AppSettings["SMTP_Port"]);
                        smtp.Timeout = int.Parse(ConfigurationManager.AppSettings["SMTP_Timeout"]);
                        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                        smtp.UseDefaultCredentials = false;
                        ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                        smtp.Credentials = new NetworkCredential(from, pass);
                        smtp.EnableSsl = true;
                        String strUrl = HttpContext.Current.Request.Url.AbsoluteUri;
                        strUrl = strUrl.Substring(0, strUrl.LastIndexOf('/')) + "/";
                        strUrl = strUrl + "JC01Login.aspx";
                        message.IsBodyHtml = true;
                        message.Subject = "パスワードのリセットが完了しました。";
                        message.Body = "<b>" + dt_mail.Rows[0]["contact_name"].ToString() + "様</b><br><br>";
                        message.Body += "パスワードの再設定が完了しました。<br><br>";
                        message.Body += "下記のボタンよりログインをして下さい。<br><br>";
                        message.Body += "<form method='POST' ><center><a href='" + strUrl + "' style='background-color: rgba(41,171,226,1); color: #fff; text-decoration: none; text-align:center; height: 40px; padding-left: 30px !important; padding-right: 30px !important; border: none; border-radius: 6px; font-size: 16px; cursor: pointer; padding: 10px; font-weight: bold;'>ログイン</a></center></form><br><br>";
                        message.Body += "パスワード変更に身に覚えがない場合は下記までお問い合わせください。<br><br>";
                        //message.Body += "jobzcloud@comnet-network.co.jp<br><br>";
                        //message.Body += "JOBZグラウド 運営チーム<br><br>";
                        message.Body += ConfigurationManager.AppSettings["SMTP_Sender"] + "<br><br>";
                        message.Body += ConfigurationManager.AppSettings["SMTP_SenderName"] + "<br><br>";
                        try
                        {
                            smtp.Send(message);
                            Response.Redirect("JC06Kanryou.aspx");
                        }
                        catch (Exception ex)
                        {
                            Response.Write("<script>alert('" + ex.Message + "');</script>");
                        }
                    }
                }
            }
        }
    }
}