using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;

namespace Common
{
    public class ConstantVal
    {

        public static bool flag_Nav = false;

        public static bool check_ts = false;

        public static string gamenname = "";

        // 【メールアドレス】文字数の制限
        public const int EMAIL_MAXLENGTH = 100;
        // 数値列ゼロ
        public const int ZERO_INTEGER = 0;

        public const string ZERO_STRING = "0";

        // マスター画面のため
        public const string MASTER_STRING = "MASTER";
        // ホーム画面のため
        public const string HOME_STRING = "HOME";

        public static string DB_NAME = "";

        #region 現在日付取る
        /// <summary>
        /// <returns>日付</returns>
        /// </summary>
        public static DateTime Fu_GetDateTime(MySqlConnection cn)
        {
            DataTable dt_Date = new DataTable();
            string sql = "select now()";
            cn.Open();
            using (MySqlDataAdapter a1 = new MySqlDataAdapter(sql, cn))
            {
                a1.Fill(dt_Date);
            }
            cn.Close();
            return System.Convert.ToDateTime(dt_Date.Rows[0][0].ToString());
        }
        #endregion

        public static DataTable Fu_GetCustomer(MySqlConnection cn, string guidValue)
        {
            DataTable dt_Customer = new DataTable();
            string sql = "";
            sql += "select";
            sql += " cu.customer_id as customer_id";
            sql += " ,cu.customer_name as customer_name";
            sql += " ,c.contact_email as contact_email";
            sql += " ,c.contact_name as contact_name";
            sql += " ,c.contact_password as contact_password";
            sql += " ,cu.contract_period as contract_period";
            sql += " ,cu.customers_postcode as customers_postcode";
            sql += " ,cu.customers_address1 as customers_address1";
            sql += " ,cu.customers_address2 as customers_address2";
            sql += " ,cu.customers_tel as customers_tel";
            sql += " from customers cu";
            sql += " inner join contacts c on cu.customer_id=c.customer_id";
            sql += " where cu.guid_value='"+ guidValue + "'";
            cn.Open();
            using (MySqlDataAdapter a1 = new MySqlDataAdapter(sql, cn))
            {
                a1.Fill(dt_Customer);
            }
            cn.Close();
            return dt_Customer;
        }

        public static DataTable Fu_GetContacts(MySqlConnection cn, string contact_email)
        {
            DataTable dt_Contacts = new DataTable();
            string sql = "";
            sql += "select";
            sql += " c.id as id";
            sql += " ,c.contact_email as contact_email";
            sql += " ,c.customer_id as customer_id";
            sql += " ,c.contact_password as contact_password";
            sql += " from contacts c ";
            sql += " where c.contact_email='" + contact_email + "'";
            sql += " and contact_flg='0'";

            cn.Open();
            using (MySqlDataAdapter a1 = new MySqlDataAdapter(sql, cn))
            {
                a1.Fill(dt_Contacts);
            }
            cn.Close();
            return dt_Contacts;
        }
    }
}
