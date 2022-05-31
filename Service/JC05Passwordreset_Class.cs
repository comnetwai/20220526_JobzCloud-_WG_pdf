using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace Service
{
    public class JC05Passwordreset_Class
    {
        public static DataTable GetResetId(string mail)
        {
            MySqlConnection cn = new MySqlConnection("Server=" + DBUtilitycs.Server + "; Database=" + DBUtilitycs.Database + "; User Id=" + DBUtilitycs.user + "; password=" + DBUtilitycs.pass);
            DataTable dt_Id = new DataTable();

            string get_id = "";
            get_id = "select id,contact_name";
            get_id += " from contacts";
            get_id += " where contact_email='" + mail + "'";
            get_id += " and contact_flg=0";

            cn.Open();
            using (MySqlDataAdapter a1 = new MySqlDataAdapter(get_id, cn))
            {
                a1.Fill(dt_Id);
            }
            cn.Close();
            return dt_Id;
        }

        
        public static DataTable GetResetMail(string id)
        {
            MySqlConnection cn = new MySqlConnection("Server=" + DBUtilitycs.Server + "; Database=" + DBUtilitycs.Database + "; User Id=" + DBUtilitycs.user + "; password=" + DBUtilitycs.pass);
            DataTable dt_Mail = new DataTable();
            string get_mail = "";
            get_mail = "select id,contact_email,contact_name";
            get_mail += " from contacts";
            get_mail += " where id='" + id + "'";

            cn.Open();
            using (MySqlDataAdapter a1 = new MySqlDataAdapter(get_mail, cn))
            {
                a1.Fill(dt_Mail);
            }
            cn.Close();
            return dt_Mail;
        }

        public static bool ResetPassword(string contact_id, string contact_password, string contact_email)
        {
            try
            {
                MySqlConnection cn = new MySqlConnection("Server=" + DBUtilitycs.Server + "; Database=" + DBUtilitycs.Database + "; User Id=" + DBUtilitycs.user + "; password=" + DBUtilitycs.pass);
                string strCustomer_Id = "";
                DataTable dt_Customer_info = new DataTable();
                dt_Customer_info = ConstantVal.Fu_GetContacts(cn, contact_email);
                if (dt_Customer_info.Rows.Count > 0)
                {
                    strCustomer_Id = dt_Customer_info.Rows[0]["customer_id"].ToString();
                }
                string sql_cusid = "";
                sql_cusid += " update m_j_tantousha";
                sql_cusid += " set sPWD='" + contact_password + "'";
                sql_cusid += " where SMAIL='" + contact_email + "'; ";
                MySqlConnection cn_customer = new MySqlConnection("Server=" + DBUtilitycs.Server + "; Database=" + strCustomer_Id + "; User Id=" + DBUtilitycs.user + "; password=" + DBUtilitycs.pass);
                cn_customer.Open();
                MySqlTransaction tran = cn_customer.BeginTransaction();
                try
                {
                    MySqlCommand c = new MySqlCommand(sql_cusid, cn_customer);
                    c.ExecuteNonQuery();
                    tran.Commit();
                    cn_customer.Close();

                    string sql_update = "";
                    sql_update += " update contacts";
                    sql_update += " set contact_password='" + contact_password + "'";
                    sql_update += " where id='" + contact_id + "'; ";
                    cn.Open();

                    c = new MySqlCommand(sql_update, cn);
                    c.ExecuteNonQuery();
                    //tran.Commit();
                    cn.Close();
                }

                catch
                {
                    try
                    {
                        tran.Rollback();
                    }
                    catch
                    {
                    }
                    cn_customer.Close();
                    return false;
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
