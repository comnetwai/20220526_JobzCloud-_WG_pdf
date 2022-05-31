using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Common;
using System.Data;

namespace Service
{
    public class JC02Touroku_Class
    {
        #region アカウント作成
        /// <summary>
        /// アカウント作成
        /// jobzcloudDBに新規作成「insert」
        /// <returns>true/false</returns>
        /// </summary>
        public static bool Create_Acc(string contact_email, string contact_name
                                            , string contact_password, string customer_id
                                            , string customer_name, string customers_postcode
                                            , string customers_address1, string customers_address2
                                            , string customers_tel, string guid_value)
        {
            try
            {
                MySqlConnection cn = new MySqlConnection("Server=" + DBUtilitycs.Server + "; Database=" + DBUtilitycs.Database + "; User Id=" + DBUtilitycs.user + "; password=" + DBUtilitycs.pass);

                DateTime genzai_dt = Common.ConstantVal.Fu_GetDateTime(cn); //現在日付取る

                //contactsタベールから最大コードを取る
                string sql_insert_id = "";
                sql_insert_id = "(select t.id";
                sql_insert_id += " from";
                sql_insert_id += " (SELECT case ifnull(max(c.id),'') when '' then 1 else max(c.id) + 1 end as id";
                sql_insert_id += " from contacts c";
                sql_insert_id += " where c.id is not null)";
                sql_insert_id += " t )";

                string sql_insert = "";

                //メールが存在していますがまだ使わないデータを消す
                sql_insert = @" delete c,cu";
                sql_insert += " from contacts c";
                sql_insert += " INNER JOIN customers cu ON c.customer_id = cu.customer_id";
                sql_insert += " where c.contact_email='" + contact_email + "' and c.contact_flg='1';";

                //contactsDBに新しい情報を作成
                sql_insert += @"INSERT INTO";
                sql_insert += " contacts";
                sql_insert += " (";
                sql_insert += " id";
                sql_insert += " ,contact_email";
                sql_insert += " ,contact_name";
                sql_insert += " ,contact_password";
                sql_insert += " ,customer_id";
                sql_insert += " ,contact_flg";
                sql_insert += " )";
                sql_insert += " Values(";
                sql_insert += " " + sql_insert_id + "";
                sql_insert += " ,'" + contact_email + "'";
                sql_insert += " ,'" + contact_name + "'";
                sql_insert += " ,'" + contact_password + "'";
                sql_insert += " ,'" + customer_id + "'";
                sql_insert += " ,'1'";
                sql_insert += " );";

                //customersDBに新しい情報を作成
                sql_insert += @"INSERT INTO";
                sql_insert += " customers";
                sql_insert += " (";
                sql_insert += " customer_id";
                sql_insert += " ,customer_name";
                sql_insert += " ,contract_period";
                sql_insert += " ,contract_period_after";
                sql_insert += " ,contract_tempfree_start_on";
                sql_insert += " ,contract_plan";
                sql_insert += " ,customers_flg";
                sql_insert += " ,customers_postcode";
                sql_insert += " ,customers_address1";
                sql_insert += " ,customers_address2";
                sql_insert += " ,customers_tel";
                sql_insert += " ,pre_created_at";
                sql_insert += ",guid_value";
                sql_insert += " )";
                sql_insert += " Values(";
                sql_insert += " '" + customer_id + "'";
                sql_insert += " ,'" + customer_name + "'";
                sql_insert += " ,'1'";
                sql_insert += " ,'1'";
                sql_insert += " ,'" + genzai_dt + "'";  //登録した日付 現在日付
                sql_insert += " ,'1'";
                sql_insert += " ,'0'";
                sql_insert += " ,'" + customers_postcode + "'";
                sql_insert += " ,'" + customers_address1 + "'";
                sql_insert += " ,'" + customers_address2 + "'";
                sql_insert += " ,'" + customers_tel + "'";
                sql_insert += " ,'" + genzai_dt + "'";
                sql_insert += " ,'" + guid_value + "'";
                sql_insert += " );";
                cn.Open();

                MySqlTransaction tran = cn.BeginTransaction();
                try
                {
                    MySqlCommand c = new MySqlCommand(sql_insert, cn);
                    c.ExecuteNonQuery();
                    tran.Commit();
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
                    cn.Close();
                    return false;
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion


        #region メールアカウント取る
        /// <summary>
        /// 同じメールメールアカウントあるかどうかチェックする
        /// <returns>true/false</returns>
        /// </summary>
        public static bool MailAcc_Check(string email)
        {
            MySqlConnection cn = new MySqlConnection("Server=" + DBUtilitycs.Server + "; Database=" + DBUtilitycs.Database + "; User Id=" + DBUtilitycs.user + "; password=" + DBUtilitycs.pass);
            try
            {
                DataTable dt_Mail = new DataTable();
                string sql_Mail = "select";
                sql_Mail += " c.id";
                sql_Mail += " from contacts c";
                sql_Mail += " where c.contact_email = '" + email + "'";
                sql_Mail += " and c.contact_flg = '0'";
                cn.Open();
                using (MySqlDataAdapter a1 = new MySqlDataAdapter(sql_Mail, cn))
                {
                    a1.Fill(dt_Mail);
                }
                cn.Close();
                if (dt_Mail.Rows.Count > 0)
                { return true; }
            }
            catch
            {
                return true;
            }
            return false;
        }
        #endregion
    }
}
