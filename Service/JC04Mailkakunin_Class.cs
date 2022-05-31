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
    public class JC04Mailkakunin_Class
    {

        public static DateTime Fu_CreateDate(string guid_value)
        {
            MySqlConnection cn = new MySqlConnection("Server=" + DBUtilitycs.Server + "; Database=" + DBUtilitycs.Database + "; User Id=" + DBUtilitycs.user + "; password=" + DBUtilitycs.pass);
            DataTable dt_Date = new DataTable();

            string get_date = "";
            get_date = "(SELECT pre_created_at";
            get_date += " FROM customers";
            get_date += " where guid_value='" + guid_value + "'";
            get_date += " )";

            cn.Open();
            using (MySqlDataAdapter a1 = new MySqlDataAdapter(get_date, cn))
            {
                a1.Fill(dt_Date);
            }
            cn.Close();
            return System.Convert.ToDateTime(dt_Date.Rows[0][0].ToString());
        }

        public static bool Fu_Check_Guid_value(string guid_value)
        {
            MySqlConnection cn = new MySqlConnection("Server=" + DBUtilitycs.Server + "; Database=" + DBUtilitycs.Database + "; User Id=" + DBUtilitycs.user + "; password=" + DBUtilitycs.pass);
            DataTable dt_Data = new DataTable();
            string get_date = "";
            get_date = "(SELECT customer_id";
            get_date += " FROM customers";
            get_date += " where guid_value='" + guid_value + "'";
            get_date += " )";

            cn.Open();
            using (MySqlDataAdapter a1 = new MySqlDataAdapter(get_date, cn))
            {
                a1.Fill(dt_Data);
            }
            cn.Close();
            if (dt_Data.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool Change_Flag(string guidValue)
        {
            try
            {
                MySqlConnection cn = new MySqlConnection("Server=" + DBUtilitycs.Server + "; Database=" + DBUtilitycs.Database + "; User Id=" + DBUtilitycs.user + "; password=" + DBUtilitycs.pass);
                string strCustomer_Id = "";
                DataTable dt_Customer_info = new DataTable();
                dt_Customer_info = ConstantVal.Fu_GetCustomer(cn, guidValue);
                if (dt_Customer_info.Rows.Count > 0)
                {
                    strCustomer_Id = dt_Customer_info.Rows[0]["customer_id"].ToString();
                }
                if (strCustomer_Id != "")
                {
                    if (Create_DB(cn, strCustomer_Id))
                    {
                        DateTime date_save = ConstantVal.Fu_GetDateTime(cn);

                        string sql_insert = "";
                        sql_insert += @"INSERT INTO";
                        sql_insert += " m_j_info";
                        sql_insert += " (";
                        sql_insert += " cCO";
                        sql_insert += " ,sCO";
                        sql_insert += " ,cYUUBIN";
                        sql_insert += " ,sJUUSHO1";
                        sql_insert += " ,sJUUSHO2";
                        sql_insert += " ,sTEL";
                        sql_insert += " ,sKYOTEN";
                        sql_insert += " ,dHENKOU";
                        sql_insert += " ,cHENKOUSYA";
                        sql_insert += " )";
                        sql_insert += " Values(";
                        sql_insert += " '01'";
                        sql_insert += ",'" + dt_Customer_info.Rows[0]["customer_name"].ToString() + "'";
                        sql_insert += ",'" + dt_Customer_info.Rows[0]["customers_postcode"].ToString() + "'";
                        sql_insert += ",'" + dt_Customer_info.Rows[0]["customers_address1"].ToString() + "'";
                        sql_insert += ",'" + dt_Customer_info.Rows[0]["customers_address2"].ToString() + "'";
                        sql_insert += ",'" + dt_Customer_info.Rows[0]["customers_tel"].ToString() + "'";
                        sql_insert += ",'本社'";
                        sql_insert += ",'" + date_save + "'";
                        sql_insert += ",'0001'";
                        sql_insert += " );";

                        sql_insert += @"INSERT INTO";
                        sql_insert += " m_j_tantousha";
                        sql_insert += " (";
                        sql_insert += " cTANTOUSHA";
                        sql_insert += " ,sTANTOUSHA";
                        sql_insert += " ,cBUMON";
                        sql_insert += " ,SMAIL";
                        sql_insert += " ,sPWD";
                        sql_insert += " ,fKANRISHA";
                        sql_insert += " ,dHENKOU";
                        sql_insert += " ,cHENKOUSYA";
                        sql_insert += " ,fTAISYA";
                        sql_insert += " ,cKYOTEN";
                        sql_insert += " ,ckengenn";
                        sql_insert += " )";
                        sql_insert += " Values(";
                        sql_insert += " '0001'";
                        sql_insert += " ,'" + dt_Customer_info.Rows[0]["contact_name"].ToString() + "'";
                        sql_insert += " ,'99'";
                        sql_insert += " ,'" + dt_Customer_info.Rows[0]["contact_email"].ToString() + "'";
                        sql_insert += " ,'" + dt_Customer_info.Rows[0]["contact_password"].ToString() + "'";
                        sql_insert += " ,'1'";
                        sql_insert += ",'" + date_save + "'";
                        sql_insert += ",'0001'";
                        sql_insert += ",'0'";
                        sql_insert += ",'01'";
                        sql_insert += ",'01'";
                        sql_insert += " );";

                        sql_insert += @"INSERT INTO";
                        sql_insert += " m_bumon";
                        sql_insert += " (";
                        sql_insert += " cBUMON";
                        sql_insert += " ,sBUMON";
                        sql_insert += " ,dHENKOU";
                        sql_insert += " ,cHENKOUSYA";
                        sql_insert += " )";
                        sql_insert += " Values(";
                        sql_insert += " '99'";
                        sql_insert += ",'システム管理'";
                        sql_insert += ",'" + date_save + "'";
                        sql_insert += ",'0001'";
                        sql_insert += " );";

                        sql_insert += @"INSERT INTO";
                        sql_insert += " m_customer";
                        sql_insert += " (";
                        sql_insert += " cCUSTOMER_ID";
                        sql_insert += " ,nCONTRACT_PERIOD";
                        sql_insert += " )";
                        sql_insert += " Values(";
                        sql_insert += " '" + strCustomer_Id + "'";
                        sql_insert += ",'" + dt_Customer_info.Rows[0]["contract_period"].ToString() + "'";
                        sql_insert += " );";

                        //20220429 Added Eaindray Start

                        sql_insert += @"INSERT INTO";
                        sql_insert += " m_syouhizei";
                        sql_insert += "(";
                        sql_insert += "cSYOUHIZEI";
                        sql_insert += ",nSYOUHIZEI";
                        sql_insert += ",dDATE";
                        sql_insert += ")";
                        sql_insert += " VALUES";
                        sql_insert += "(";
                        sql_insert += " '1' ";
                        sql_insert += ",8.00";
                        sql_insert += ", '2014-04-01 00:00:00' ";
                        sql_insert += " );";

                        sql_insert += @"INSERT INTO";
                        sql_insert += " m_syouhizei";
                        sql_insert += "(";
                        sql_insert += "cSYOUHIZEI";
                        sql_insert += ",nSYOUHIZEI";
                        sql_insert += ",dDATE";
                        sql_insert += ")";
                        sql_insert += " VALUES";
                        sql_insert += "(";
                        sql_insert += " '2' ";
                        sql_insert += ",10.00";
                        sql_insert += ", '2019-10-01 00:00:00' ";
                        sql_insert += " );";

                        //20220429 Added Eaindray End

                        MySqlConnection cn_customer = new MySqlConnection("Server=" + DBUtilitycs.Server + "; Database=" + strCustomer_Id + "; User Id=" + DBUtilitycs.user + "; password=" + DBUtilitycs.pass);
                        cn_customer.Open();
                        MySqlTransaction tran = cn_customer.BeginTransaction();
                        try
                        {
                            MySqlCommand c = new MySqlCommand(sql_insert, cn_customer);
                            c.ExecuteNonQuery();
                            tran.Commit();
                            cn_customer.Close();

                            string sql_update = "";
                            sql_update = @" update contacts as con";
                            sql_update += " join customers as cus";
                            sql_update += " on con.customer_id=cus.customer_id";
                            sql_update += " set con.contact_flg='0'";
                            sql_update += " ,cus.guid_value=''";
                            sql_update += " where cus.guid_value='" + guidValue + "'; ";
                            
                            cn.Open();
                            c = new MySqlCommand(sql_update, cn);
                            c.ExecuteNonQuery();
                            cn.Close();
                        }
                        catch (Exception ex)
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
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        public static bool Create_DB(MySqlConnection cn, string strCustomer_Id)
        {
            String strCopyDB = "jobzcloud_copy";
            DataTable dt_Table_Name = new DataTable();
            string sql_tb_name = "";
            sql_tb_name = " SELECT";
            sql_tb_name += " table_name";
            sql_tb_name += " FROM";
            sql_tb_name += " information_schema.tables";
            sql_tb_name += " WHERE";
            sql_tb_name += " table_schema='" + strCopyDB + "'";
            sql_tb_name += " and TABLE_TYPE='BASE TABLE';";

            try
            {
                cn.Open();
                using (MySqlDataAdapter a1 = new MySqlDataAdapter(sql_tb_name, cn))
                {
                    a1.Fill(dt_Table_Name);
                }
                cn.Close();

                string sql_create_db = "";
                sql_create_db = @" DROP SCHEMA IF EXISTS ";
                sql_create_db += strCustomer_Id + ";";
                sql_create_db += @" CREATE SCHEMA ";
                sql_create_db += strCustomer_Id + ";";

                cn.Open();
                MySqlCommand c = new MySqlCommand(sql_create_db, cn);
                c.ExecuteNonQuery();
                cn.Close();

                for (int i = 0; i < dt_Table_Name.Rows.Count; i++)
                {
                    string sql_create_tb = "";
                    // データベースにテーブルの作成
                    sql_create_tb = @"CREATE TABLE " + strCustomer_Id + "." + dt_Table_Name.Rows[i][0].ToString() + " LIKE " + strCopyDB + "." + dt_Table_Name.Rows[i][0].ToString() + ";";

                    // テーブルのデータをコピーする
                    if (dt_Table_Name.Rows[i][0].ToString() == "m_hyoujikomoku"  || dt_Table_Name.Rows[i][0].ToString() == "m_kengenn" || dt_Table_Name.Rows[i][0].ToString() == "m_mitumori_jyotai" || dt_Table_Name.Rows[i][0].ToString() == "m_s_info"
                        || dt_Table_Name.Rows[i][0].ToString() == "m_tani")
                    {
                        sql_create_tb += @"INSERT " + strCustomer_Id + "." + dt_Table_Name.Rows[i][0].ToString() + " SELECT * FROM " + strCopyDB + "." + dt_Table_Name.Rows[i][0].ToString() + ";";
                    }

                    cn.Open();
                    MySqlCommand c_tb = new MySqlCommand(sql_create_tb, cn);
                    c_tb.ExecuteNonQuery();
                    cn.Close();
                    // テーブルのデータをコピーする
                    //sql_create_db += "INSERT " + strCustomer_Id + "." + dt_Table_Name.Rows[i][0].ToString() + " SELECT * FROM " + strCopyDB + "." + dt_Table_Name.Rows[i][0].ToString() + ";";
                }
            }
            catch
            {
                cn.Close();
                return false;
            }
            return true;
        }

    }
}
