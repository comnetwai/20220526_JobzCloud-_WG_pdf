using Common;
using MySql.Data.MySqlClient;
using Service;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JobzCloud.WebFront
{
    public partial class JC17PasswordHenkou : System.Web.UI.Page
    {
        public static string cur_mail = "";
        public static string to, tomail;
        public String url;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(Request.QueryString["id"]))
            {
                if (!IsPostBack)
                {
                    if (SessionUtility.GetSession("HOME") != null)  //20211014 MiMi Added
                    {
                        hdnHome.Value = SessionUtility.GetSession("HOME").ToString();
                        SessionUtility.SetSession("HOME", null);
                        updHeader.Update();
                    }
                }
                Session.Timeout = 480;
            }
            else
            {
                Response.Redirect("JC01Login.aspx");
            }

        }

        protected Boolean CheckAllData()
        {
            if (String.IsNullOrEmpty(txtCurrentPwd.Text) || String.IsNullOrEmpty(txtNewPwd.Text) || String.IsNullOrEmpty(txtConfirmPwd.Text))
            {
                txtCurrentPwd.Style.Add("border-color", "red");
                txtNewPwd.Style.Add("border-color", "red");
                txtConfirmPwd.Style.Add("border-color", "red");
                txtCurrentPwd.Text = string.Empty;
                txtNewPwd.Text = string.Empty;
                txtConfirmPwd.Text = string.Empty;
                txtCurrentPwd.Focus();
                return false;
            }
            return true;
        }

        protected void btnhozon_Click(object sender, EventArgs e)
        {

            // Added by エインドリ－・プ－プゥ
            //Save the new password
            //20211203 Updated エインドリ－・プ－プゥ

            String id = Request.QueryString["id"];
            DataTable dt_db = JC01Login_Class.Get_DB(id);
            String db = dt_db.Rows[0]["db"].ToString();
            String mail = dt_db.Rows[0]["mail"].ToString();

            txtCurrentPwd.Style["border-color"] = "none";
            txtNewPwd.Style["border-color"] = "none";
            txtConfirmPwd.Style["border-color"] = "none";
            LB_CurPwd_Error.Text = LB_NewPwd_Error.Text = LB_ConPwd_Error.Text = "";

            //if (CheckAllData())
            //{
            if (String.IsNullOrEmpty(txtCurrentPwd.Text))
            {
                
                txtCurrentPwd.Style.Add("border-color", "red");
                txtCurrentPwd.Text = string.Empty;
                txtCurrentPwd.Focus();
                //btnhozon.Enabled = false;
            }
            else if (String.IsNullOrEmpty(txtNewPwd.Text) || String.IsNullOrEmpty(txtConfirmPwd.Text))
            {
            
                txtNewPwd.Style.Add("border-color", "red");
                txtConfirmPwd.Style.Add("border-color", "red");
                txtNewPwd.Text = string.Empty;
                txtConfirmPwd.Text = string.Empty;
                txtNewPwd.Focus();
                //btnhozon.Enabled = false;
            }
            else
            {
                string cur_pwd = "";
                string cur_name = "";
                MySqlConnection cn = new MySqlConnection("Server=" + DBUtilitycs.Server + "; Database=" + DBUtilitycs.Database + "; User Id=" + DBUtilitycs.user + "; password=" + DBUtilitycs.pass);
                string select_pwd = "SELECT contact_name,contact_password FROM contacts WHERE contact_email='" + cur_mail + "'"; /*20211116 Updated エインドリ－・プ－プゥ*/
                cn.Open();
                MySqlCommand cmd = new MySqlCommand(select_pwd, cn);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    cur_name = reader.GetValue(0).ToString();　/*20211116 Updated エインドリ－・プ－プゥ*/
                    cur_pwd = TextUtility.DecryptData_Henkou(reader.GetValue(1).ToString());
                }
                reader.Close();
                cn.Close();
                if (!txtCurrentPwd.Text.Equals(cur_pwd))
                {
                    LB_CurPwd_Error.Text = "現在のパスワードが間違っています。";
                    txtCurrentPwd.Focus();
                    txtNewPwd.Text = txtConfirmPwd.Text = string.Empty;
                }
                else
                {
                    if (txtNewPwd.Text == string.Empty || !txtNewPwd.Text.Equals(txtConfirmPwd.Text))
                    {
                        LB_NewPwd_Error.Text = "パスワードが一致していません。";
                        txtNewPwd.Focus();
                        txtNewPwd.Text = txtConfirmPwd.Text = string.Empty;
                    }
                    else if (txtNewPwd.Text.Length < 8)
                    {
                        LB_NewPwd_Error.Text = "パスワードは8文字以上で登録してください。";
                        txtNewPwd.Focus();
                        txtNewPwd.Text = txtConfirmPwd.Text = string.Empty;
                    }
                    else
                    {
                        string sql_update = "";
                        sql_update = @" update contacts ";
                        sql_update += " set contact_flg='0'";
                        sql_update += " ,contact_password='" + TextUtility.EncryptData_Henkou(txtNewPwd.Text) + "'";
                        sql_update += " where contact_email='" + cur_mail + "'; ";

                        cn.Open();
                        MySqlCommand cs = new MySqlCommand(sql_update, cn);
                        cs.ExecuteNonQuery();
                        cn.Close();

                        //20211203 Added エインドリ－・プ－プゥ (Start)
                        
                        ConstantVal.DB_NAME = db;

                        JC_ClientConnecction_Class jc = new JC_ClientConnecction_Class();
                        jc.loginId = cur_mail;
                        cn = jc.GetConnection();
                        string update = " UPDATE m_j_tantousha SET " +
                                "sPWD='" + TextUtility.EncryptData_Henkou(txtNewPwd.Text) + "'" + //20211203 Added By エインドリ・プ－プゥ
                               "WHERE sMAIL='" + cur_mail + "';";
                        cn.Open();
                        MySqlCommand cs1 = new MySqlCommand(update, cn);
                        cs1.ExecuteNonQuery();
                        cn.Close();
                        //20211203 Added エインドリ－・プ－プゥ (End)
                        System.Threading.Thread.Sleep(1000);
                        //HF_Url.Value = HttpContext.Current.Request.Url.AbsoluteUri;

                        //20220322 Update JOBZグラウド -> 見えるJOBZ in mail エインドリ－
                        #region　メール送信
                        String url = HttpContext.Current.Request.Url.AbsoluteUri;
                        url =url.Substring(0, url.Length - 19);
                        //url = url + "JC04Mailkakunin.aspx?id=" + strGuid;
                        url = url + "/JC01Login.aspx";
                        //url = url + "/JC03Mail.aspx";
                        string from, pass = "";
                        MailMessage message = new MailMessage();
                        to = cur_mail;
                        from = ConfigurationManager.AppSettings["SMTP_Sender"];
                        pass = ConfigurationManager.AppSettings["SMTP_Password"];
                        //messageBody = "この度はJOBZグラウドにご登録頂きまして誠にありがとうございます。" + "\n" + "２４時間以内に下記のリンクをクリックして認証を行ってください。" + "\n" + url;  /* 20211116 Close Comment エインドリ－・プ－プゥ*/
                        message.To.Add(to);
                        //20211203 Updated エインドリ－・プ－プゥ (Start)

                        //message.From = new MailAddress(from, "JOBZグラウド 運営チーム");
                        message.From = new MailAddress(from, ConfigurationManager.AppSettings["SMTP_SenderName"]);
                        // message.Body = messageBody;  /* 20211116 Close Comment エインドリ－・プ－プゥ*/
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
                        /*Start 20211116 Added By エインドリ－・プ－プゥ*/
                        message.IsBodyHtml = true;
                        message.Body = "<b>" + cur_name + " 様</b><br><br>";
                        message.Body += "パスワードの再設定が完了しました。<br><br>";
                        message.Body += "下記のボタンよりログインをして下さい。<br><br>";
                        message.Body += "<form method='POST' ><center><a href='" + url + "' style='background-color: rgba(41,171,226,1); color: #fff; text-decoration: none; text-align:center; height: 40px; padding-left: 30px !important; padding-right: 30px !important; border: none; border-radius: 6px; font-size: 16px; cursor: pointer; padding: 10px; font-weight: bold;'>ログイン</a></center></form><br><br>";
                        message.Body += "パスワード変更に身に覚えがない場合は下記までお問い合わせください。<br><br>";
                        message.Body += "jobzcloud@comnet-network.co.jp<br><br>";
                        message.Body += "見えるJOBZ 運営チーム<br><br>";
                        /*End 20211116 Added By エインドリ－・プ－プゥ*/
                        try
                        {
                            tomail = cur_mail;
                            smtp.Send(message);
                        }
                        catch (Exception ex)
                        {
                            Response.Write("<script>alert('" + ex.Message + "');</script>");
                        }
                        #endregion

                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "parentButtonClick('btnSavePwd','" + hdnHome.Value + "');", true);
                    }
                }
            }
            if (String.IsNullOrEmpty(txtCurrentPwd.Text) && String.IsNullOrEmpty(txtNewPwd.Text) && String.IsNullOrEmpty(txtConfirmPwd.Text))
            {
                
                txtNewPwd.Style.Add("border-color", "red");
                txtConfirmPwd.Style.Add("border-color", "red");
                
                txtNewPwd.Text = string.Empty;
                txtConfirmPwd.Text = string.Empty;
                
            }
            else
            {
                
                //}
            }

            updHeader.Update();

            #region toDelete

            //if (txtCurrentPwd.Text=="") 
            //{
            //    LB_CurPwd_Error.Text = "データを入力してください。";
            //    LB_NewPwd_Error.Text = LB_ConPwd_Error.Text = "";
            //    txtCurrentPwd.Focus();
            //}
            //else if (txtNewPwd.Text=="")　 //20211203 Updated エインドリ－・プ－プゥ
            //{
            //    LB_NewPwd_Error.Text = "データを入力してください。";
            //    LB_CurPwd_Error.Text = LB_ConPwd_Error.Text = "";
            //    txtNewPwd.Focus();
            //}
            //else if (txtConfirmPwd.Text=="")　 //20211203 Updated エインドリ－・プ－プゥ
            //{
            //    LB_ConPwd_Error.Text = "データを入力してください。";
            //    LB_CurPwd_Error.Text = LB_NewPwd_Error.Text = "";
            //    txtConfirmPwd.Focus();
            //}
            //else
            //{

            //    string cur_pwd = "";
            //    string cur_name = "";
            //    MySqlConnection cn = new MySqlConnection("Server=" + DBUtilitycs.Server + "; Database=" + DBUtilitycs.Database + "; User Id=" + DBUtilitycs.user + "; password=" + DBUtilitycs.pass);
            //    string select_pwd = "SELECT contact_name,contact_password FROM contacts WHERE contact_email='"+cur_mail+"'"; /*20211116 Updated エインドリ－・プ－プゥ*/
            //    cn.Open();
            //    MySqlCommand cmd = new MySqlCommand(select_pwd, cn);
            //    MySqlDataReader reader = cmd.ExecuteReader();
            //    while (reader.Read())
            //    {
            //        cur_name = reader.GetValue(0).ToString();　/*20211116 Updated エインドリ－・プ－プゥ*/
            //        cur_pwd = TextUtility.DecryptData_Henkou(reader.GetValue(1).ToString());
            //    }
            //    reader.Close();
            //    cn.Close();
            //    if (txtCurrentPwd.Text == cur_pwd)
            //    {
            //        if (txtNewPwd.Text == txtConfirmPwd.Text)
            //        {
            //            //string sql_update = "";
            //            //sql_update = @" update contacts ";
            //            //sql_update += " set contact_flg='0'";
            //            //sql_update += " ,contact_password='" + TextUtility.EncryptData_Henkou(txtNewPwd.Text) + "'";
            //            //sql_update += " where contact_email='" + cur_mail + "'; ";

            //            //cn.Open();
            //            //MySqlCommand cs = new MySqlCommand(sql_update, cn);
            //            //cs.ExecuteNonQuery();
            //            //cn.Close();

            //            ////20211203 Added エインドリ－・プ－プゥ (Start)
            //            //JC_ClientConnecction_Class jc = new JC_ClientConnecction_Class();
            //            //jc.loginId = cur_mail;
            //            //cn = jc.GetConnection();
            //            //string update = " UPDATE m_j_tantousha SET " +
            //            //        "sPWD='" + TextUtility.EncryptData_Henkou(txtNewPwd.Text) + "'" + //20211203 Added By エインドリ・プ－プゥ
            //            //       "WHERE sMAIL='" + cur_mail + "';";
            //            //cn.Open();
            //            //MySqlCommand cs1 = new MySqlCommand(update, cn);
            //            //cs1.ExecuteNonQuery();
            //            //cn.Close();
            //            ////20211203 Added エインドリ－・プ－プゥ (End)

            //            //#region　メール送信
            //            //String url = HttpContext.Current.Request.Url.AbsoluteUri;
            //            //url = url.Substring(0, url.Length - 19);
            //            ////url = url + "JC04Mailkakunin.aspx?id=" + strGuid;
            //            //url = url + "/JC01Login.aspx";
            //            //string from, pass = "";
            //            //MailMessage message = new MailMessage();
            //            //to = cur_mail;
            //            //from = ConfigurationManager.AppSettings["SMTP_Sender"];
            //            //pass = ConfigurationManager.AppSettings["SMTP_Password"];
            //            ////messageBody = "この度はJOBZグラウドにご登録頂きまして誠にありがとうございます。" + "\n" + "２４時間以内に下記のリンクをクリックして認証を行ってください。" + "\n" + url;  /* 20211116 Close Comment エインドリ－・プ－プゥ*/
            //            //message.To.Add(to);
            //            ////20211203 Updated エインドリ－・プ－プゥ (Start)

            //            ////message.From = new MailAddress(from, "JOBZグラウド 運営チーム");
            //            //message.From = new MailAddress(from, ConfigurationManager.AppSettings["SMTP_SenderName"]);
            //            //// message.Body = messageBody;  /* 20211116 Close Comment エインドリ－・プ－プゥ*/
            //            //message.Subject = "JOBZグラウドのメールアドレスの認証";
            //            ////SmtpClient smtp = new SmtpClient("smtp.gmail.com");
            //            //SmtpClient smtp = new SmtpClient();
            //            //smtp.Host = ConfigurationManager.AppSettings["SMTP_Host"];
            //            ////smtp.Timeout = 300000;
            //            //smtp.Timeout = int.Parse(ConfigurationManager.AppSettings["SMTP_Timeout"]);
            //            //smtp.EnableSsl = true;
            //            ////smtp.Port = 587;
            //            //smtp.Port = int.Parse(ConfigurationManager.AppSettings["SMTP_Port"]);
            //            //smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            //            //smtp.UseDefaultCredentials = false;
            //            //ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            //            //smtp.Credentials = new NetworkCredential(from, pass);
            //            //smtp.EnableSsl = true;
            //            ////20211203 Updated エインドリ－・プ－プゥ (End)
            //            ///*Start 20211116 Added By エインドリ－・プ－プゥ*/
            //            //message.IsBodyHtml = true; 
            //            //message.Body = "<b>" +cur_name + " 様</b><br><br>";
            //            //message.Body += "パスワードの再設定が完了しました。<br><br>";
            //            //message.Body += "下記のボタンよりログインをして下さい。<br><br>";
            //            //message.Body += "<form method='POST' ><center><a href='" + url + "' style='background-color: rgba(41,171,226,1); color: #fff; text-decoration: none; text-align:center; height: 40px; padding-left: 30px !important; padding-right: 30px !important; border: none; border-radius: 6px; font-size: 16px; cursor: pointer; padding: 10px; font-weight: bold;'>ログイン</a></center></form><br><br>";
            //            //message.Body += "パスワード変更に身に覚えがない場合は下記までお問い合わせください。<br><br>";
            //            //message.Body += "jobzcloud@comnet-network.co.jp<br><br>";
            //            //message.Body += "JOBZグラウド 運営チーム<br><br>";
            //            ///*End 20211116 Added By エインドリ－・プ－プゥ*/
            //            //try
            //            //{
            //            //    tomail = cur_mail;
            //            //    smtp.Send(message);
            //            //}
            //            //catch (Exception ex)
            //            //{
            //            //    Response.Write("<script>alert('" + ex.Message + "');</script>");
            //            //}
            //            //#endregion

            //            //ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "parentButtonClick('btnSavePwd','"+hdnHome.Value+"');", true);

            //        }
            //        else
            //        {
            //            Response.Write("<script>alert('パスワードが一致しません。')</script>");
            //        }
            //    }
            //    else
            //    {
            //        Response.Write("<script>alert('現在のパスワードが間違っています。')</script>");
            //    }
            //}
            #endregion
        }


        protected void BtnCancel_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "parentButtonClick('btnClose','"+hdnHome.Value+"');", true);
            updHeader.Update();
        }
    }
}