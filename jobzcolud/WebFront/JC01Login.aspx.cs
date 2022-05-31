using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Service;
using Common;
using MySql.Data.MySqlClient;
using System.Data;
using JobzCloud.WebFront;

namespace jobzcolud
{
    public partial class JC01Login : System.Web.UI.Page
    {
        MySqlConnection con = null; // Added By エインドリ－・プ－プゥ
        public bool clicked_link=false;　// Added By エインドリ－・プ－プゥ
        public static string old_email,new_mail, username; // Added By エインドリ－・プ－プゥ
        //20211209 Added By エインドリ－・プ－プゥ
        public static string ID,ctantou,stantou,cbumon,mail,pwd,fkanrisha,chenkou,chenkousha,ckyoten,ckengenn,ftaisya,dtaisya;
        public static bool is_tantou,is_edit = false; //20211214 Added エインドリ－・プ－プゥ
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (DBUtilitycs.Server == "")
                {
                    get_connect_data();
                }

                try
                {
                    //20211209 Added By エインドリ－・プ－プゥ
                    if (is_tantou == true)
                    {
                        if (Convert.ToBoolean(Request.QueryString["id"]) == true) //20211213 Added By エインドリ－・プ－プゥ
                        {
                            JC15TantouTouroku_Class c = new JC15TantouTouroku_Class();
                            c.loginID = ID;
                            c.cTANTOUSHA = ctantou;
                            c.sTANTOUSHA = stantou;
                            c.cBUMON = cbumon;
                            c.SMAIL = mail;
                            c.sPWD = pwd;
                            c.fKANRISHA = fkanrisha;
                            c.cHENKOUSYA = chenkousha;
                            c.fKANRISHA = fkanrisha;
                            c.fTAISYA = ftaisya;
                            c.cKYOTEN = ckyoten;
                            c.ckengenn = ckengenn;
                            c.old_mail = old_email;
                            c.dTAISYA = dtaisya; //20211215 Added エインドリ－・プ－プゥ
                            if (is_edit == true) //20211214 Added エインドリ－・プ－プゥ
                                c.Tantou_Update();
                            else
                                c.Tantou_Create();
                        }
                        is_tantou = false;
                    }
                    else
                    {
                        //Added By エインドリ－・プ－プゥ (Start)
                        //clicked_link = Convert.ToBoolean(Request.QueryString["id"]);
                        //if (clicked_link == true)
                        //{
                        //    MySqlConnection cn = new MySqlConnection("Server=" + DBUtilitycs.Server + "; Database=" + DBUtilitycs.Database + "; User Id=" + DBUtilitycs.user + "; password=" + DBUtilitycs.pass);
                        //    string sql_update = "";
                        //    sql_update = @" update contacts as con";
                        //    sql_update += " join customers as cus";
                        //    sql_update += " on con.customer_id=cus.customer_id";
                        //    sql_update += " set con.contact_flg='0'";
                        //    sql_update += " ,con.contact_email='" + new_mail + "'";
                        //    sql_update += " ,con.contact_name='" + username + "'";
                        //    sql_update += " ,cus.guid_value=''";
                        //    sql_update += " where con.contact_email='" + old_email + "'; ";

                        //    cn.Open();
                        //    MySqlCommand cs = new MySqlCommand(sql_update, cn);
                        //    cs.ExecuteNonQuery();
                        //    cn.Close();

                        //    JC_ClientConnecction_Class jc = new JC_ClientConnecction_Class();
                        //    jc.loginId = new_mail;
                        //    con = jc.GetConnection();
                        //    string update = " UPDATE m_j_tantousha SET " +
                        //            "sTANTOUSHA='" + username + "'," + //20211102 Added By エインドリ・プ－プゥ
                        //           "sMAIL='" + new_mail + "' " +
                        //           "WHERE sMAIL='" + old_email + "';";

                        //    con.Open();
                        //    MySqlCommand cs1 = new MySqlCommand(update, con);
                        //    cs1.ExecuteNonQuery();
                        //    con.Close();
                        //}
                        //Added By エインドリ－・プ－プゥ (End)
                    }
                }
                catch
                {
                    Response.Redirect("JC01Login.aspx");
                }
            }
                
        }
        
        protected void BT_create_Click(object sender,EventArgs e)
        {
            Response.Redirect("JC02Touroku.aspx");
        }

        private void get_connect_data()
        {
            DBUtilitycs.get_connetion_ifo();
        }

        protected void BT_login_Click(object sender, EventArgs e)
        {
            string pass = "";
            LB_Mail_Error.Text = "";
            LB_Pass_Error.Text = "";
            pass = TextUtility.DecryptData_Henkou(JC01Login_Class.Get_Password(TB_id.Text));
            if (pass != "")
            {
                if (JC02Touroku_Class.MailAcc_Check(TB_id.Text))
                {
                    if (pass == TB_Pass.Text)
                    {
                        if (JC01Login_Class.ftantou_check(TB_id.Text) == true)
                        {
                            Session["LoginId"] = TB_id.Text;//added by nan 20211006
                            Session["DB"] = JC01Login_Class.DB;
                            String id = JC01Login_Class.ID;
                            Response.Redirect("JC07Home.aspx?id="+id);
                        }
                        else
                        {
                            LB_Mail_Error.Text = "ユーザーIDが退社しています。";
                            LB_Pass_Error.Text = "";
                        }

                    }
                    else
                    {
                        LB_Pass_Error.Text = "パスワードが正しくありません。";
                        LB_Mail_Error.Text = "";
                    }
                }
                else
                {
                    LB_Mail_Error.Text = "このメールアドレスは登録されていません。";
                }
            }
            else
            {
                if (TextUtility.IsIncludeZenkaku(TB_id.Text))
                {
                    // 【半角英数を入力してください。】というメセジを表示
                    LB_Mail_Error.Text = "半角英数を入力してください。";
                    TB_id.Text = "";
                }
                else
                {
                    LB_Mail_Error.Text = "ユーザーIDが存在しません。";
                }
                TB_id.Focus();
            }
        }
        
    }
}