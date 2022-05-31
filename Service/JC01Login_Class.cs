using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;
using Common;

namespace Service
{
    public class JC01Login_Class
    {
        //ooo
        public static string DB = "";
        public static string ID = "";
        public static string Get_Password(string contact_email)
        {
            MySqlConnection cn = new MySqlConnection("Server=" + DBUtilitycs.Server + "; Database=" + DBUtilitycs.Database + "; User Id=" + DBUtilitycs.user + "; password=" + DBUtilitycs.pass);
            string pass = "";

            DataTable dt_Contacts_info = new DataTable();
            dt_Contacts_info = ConstantVal.Fu_GetContacts(cn, contact_email);
            if (dt_Contacts_info.Rows.Count > 0)
            {
                pass = dt_Contacts_info.Rows[0]["contact_password"].ToString();
                DB = dt_Contacts_info.Rows[0]["customer_id"].ToString();
                ID= dt_Contacts_info.Rows[0]["id"].ToString();
                //DBUtilitycs.Database = dt_Contacts_info.Rows[0]["customer_id"].ToString();

            }
            return pass;
        }

        public static bool ftantou_check(string contact_email)
        {
            MySqlConnection cn = new MySqlConnection("Server=" + DBUtilitycs.Server + "; Database=" + DB + "; User Id=" + DBUtilitycs.user + "; password=" + DBUtilitycs.pass);

            string sql = "";
            sql = "SELECT cTANTOUSHA FROM m_j_tantousha where fTAISYA = 0 and sMAIL ='" + contact_email + "'";
            DataTable dt_tantoucheck = new DataTable();
            using (MySqlDataAdapter adap = new MySqlDataAdapter(sql, cn))
            {
                adap.Fill(dt_tantoucheck);
            }
            if (dt_tantoucheck.Rows.Count== 0)
            {
                return false;
            }
            return true;
        }

        public static DataTable Get_DB(string id)
        {
            MySqlConnection cn = new MySqlConnection("Server=" + DBUtilitycs.Server + "; Database=" + DBUtilitycs.Database + "; User Id=" + DBUtilitycs.user + "; password=" + DBUtilitycs.pass);
            
            string sql = "";
            sql = "SELECT customer_id as db,contact_email as mail FROM contacts Where id='" + id+"';";
            DataTable dt_db = new DataTable();
            using (MySqlDataAdapter adap = new MySqlDataAdapter(sql, cn))
            {
                adap.Fill(dt_db);
            }
            return dt_db;
        }

    }
    }
