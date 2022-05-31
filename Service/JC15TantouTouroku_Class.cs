using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using MySql.Data.MySqlClient;
using System.Data;

namespace Service
{
    public class JC15TantouTouroku_Class
    {
        public string cTANTOUSHA { get; set; }
        public string sTANTOUSHA { get; set; }
        public string cBUMON { get; set; }
        public string SMAIL { get; set; }
        public string sPWD { get; set; }
        public string fKANRISHA { get; set; }
        public string cHENKOUSYA { get; set; }
        public string fTAISYA { get; set; }
        public string dTAISYA { get; set; }
        public string cKYOTEN { get; set; }
        public string ckengenn { get; set; }

        public string loginID { get; set; } //20211208 Added エインドリ－・プ－プゥ
        public bool is_edited { get; set; } //20211208 Added エインドリ－・プ－プゥ
        public string old_mail { get; set; }//20211214 Added エインドリ－・プ－プゥ

        //20211227 Added エインドリ－・プ－プゥ
        public static bool MailAcc_Check(string email)
        {
            //MySqlConnection cn_customer = new MySqlConnection("Server=" + DBUtilitycs.Server + "; Database=" + JC01Login_Class.DB + "; User Id=" + DBUtilitycs.user + "; password=" + DBUtilitycs.pass);
            MySqlConnection cn_customer = new MySqlConnection("Server=" + DBUtilitycs.Server + "; Database=" + ConstantVal.DB_NAME + "; User Id=" + DBUtilitycs.user + "; password=" + DBUtilitycs.pass);
            try
            {
                DataTable dt_Mail = new DataTable();
                string sql_Mail = "select";
                sql_Mail += " cTANTOUSHA";
                sql_Mail += " from m_j_tantousha";
                sql_Mail += " where sMAIL = '" + email + "'";
                
                cn_customer.Open();
                using (MySqlDataAdapter a1 = new MySqlDataAdapter(sql_Mail, cn_customer))
                {
                    a1.Fill(dt_Mail);
                }
                cn_customer.Close();
                if (dt_Mail.Rows.Count > 0)
                { return true; }
            }
            catch(Exception e)
            {
                return true;
            }
            return false;
        }

        //20211214 Updated エインドリ－・プ－プゥ
        public bool Tantou_Update()
        {
            //MySqlConnection cn_customer = new MySqlConnection("Server=" + DBUtilitycs.Server + "; Database=" + JC01Login_Class.DB + "; User Id=" + DBUtilitycs.user + "; password=" + DBUtilitycs.pass);
            MySqlConnection cn_customer = new MySqlConnection("Server=" + DBUtilitycs.Server + "; Database=" + ConstantVal.DB_NAME + "; User Id=" + DBUtilitycs.user + "; password=" + DBUtilitycs.pass);
            //20211215 Please delete this sql_insert_id query By エインドリ－・プ－プゥ
            //string sql_insert_id = "";
            //sql_insert_id = "(select t.id";
            //sql_insert_id += " from";
            //sql_insert_id += " (SELECT case ifnull(max(c.id),'') when '' then 1 else max(c.id) + 1 end as id";
            //sql_insert_id += " from contacts c";
            //sql_insert_id += " where c.id is not null)";
            //sql_insert_id += " t )";
            DateTime date_save = ConstantVal.Fu_GetDateTime(cn_customer);

            cn_customer.Open();
            string Update = " UPDATE m_j_tantousha SET " +
                        "sTANTOUSHA='" + sTANTOUSHA + "', " +
                        "cBUMON='" + cBUMON + "', " +
                        "SMAIL='" + SMAIL + "', " +
                        "sPWD='" + sPWD + "', " +
                        "fKANRISHA='" + fKANRISHA + "', " +
                        "dHENKOU='" + date_save + "', " +
                        "cHENKOUSYA='" + cHENKOUSYA + "', " +
                        "fTAISYA='" + fTAISYA + "', " +
                        "cKYOTEN='" + cKYOTEN + "', " +
                        "ckengenn='" + ckengenn + "' ";
                        if (fTAISYA == "0") //20211215 added エインドリ－・プ－プゥ
                        {
                            Update += " ,dTAISHA=NULL ";
                        }
                        else
                        {
                            if (dTAISYA != "")
                            {
                                Update += " ,dTAISHA='" + dTAISYA + "' ";
                            }
                            else
                            {
                                Update += " ,dTAISHA=NULL ";
                            }
                        }
            Update +=" WHERE cTANTOUSHA ='" + cTANTOUSHA + "';";
            //MySqlCommand cs1 = new MySqlCommand(Update, cn_customer);
            //cs1.ExecuteNonQuery();

            MySqlTransaction tran = cn_customer.BeginTransaction();
            try
            {
                MySqlCommand c = new MySqlCommand(Update, cn_customer);
                c.ExecuteNonQuery();
                tran.Commit();
                cn_customer.Close();

                string sql_update = "";
                sql_update = @" update contacts as con";
                sql_update += " join customers as cus";
                sql_update += " on con.customer_id=cus.customer_id";
                sql_update += " set con.contact_flg='0'";
                sql_update += " ,con.contact_email='" + SMAIL + "'";
                sql_update += " ,con.contact_name='" + sTANTOUSHA + "'";
                sql_update += " ,con.contact_password='" + sPWD + "'";//20220119 Added エインドリ－・プ－プゥ
                sql_update += " ,cus.guid_value=''";
                sql_update += " where con.contact_email='" + old_mail + "'; ";

                MySqlConnection cn_MainDB = new MySqlConnection("Server=" + DBUtilitycs.Server + "; Database=" + DBUtilitycs.Database + "; User Id=" + DBUtilitycs.user + "; password=" + DBUtilitycs.pass);
                cn_MainDB.Open();
                MySqlTransaction tran_MainDB = cn_MainDB.BeginTransaction();
                try
                {
                    c = new MySqlCommand(sql_update, cn_MainDB);
                    c.ExecuteNonQuery();
                    tran_MainDB.Commit();
                    cn_MainDB.Close();
                }
                catch
                {
                    try
                    {
                        tran_MainDB.Rollback();
                    }
                    catch
                    {
                    }
                    cn_MainDB.Close();
                    return false;
                }
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
        public bool Tantou_Create()
        {
            //MySqlConnection cn_customer = new MySqlConnection("Server=" + DBUtilitycs.Server + "; Database=" + JC01Login_Class.DB + "; User Id=" + DBUtilitycs.user + "; password=" + DBUtilitycs.pass);
            MySqlConnection cn_customer = new MySqlConnection("Server=" + DBUtilitycs.Server + "; Database=" + ConstantVal.DB_NAME + "; User Id=" + DBUtilitycs.user + "; password=" + DBUtilitycs.pass);

            string sql_insert_id = "";
            sql_insert_id = "(select t.id";
            sql_insert_id += " from";
            sql_insert_id += " (SELECT case ifnull(max(c.id),'') when '' then 1 else max(c.id) + 1 end as id";
            sql_insert_id += " from contacts c";
            sql_insert_id += " where c.id is not null)";
            sql_insert_id += " t )";

            //20211208 Added エインドリ－・プ－プゥ (Start)
            //Get cTANTOUSHA
            try
            {
               
                cn_customer.Open();
                string select_code = "SELECT cTANTOUSHA FROM m_j_tantousha";
                MySqlCommand cmd = new MySqlCommand(select_code, cn_customer);
                var f_code = "";
                MySqlDataReader reader = cmd.ExecuteReader();
                int code = 1;
                while (reader.Read())
                {
                    f_code = reader.GetValue(0).ToString();
                    int first = Convert.ToInt32(f_code);
                    if (first != code)
                    {
                        cTANTOUSHA = code.ToString().PadLeft(4, '0');
                        break;
                    }
                    code++;
                }
                cn_customer.Close();
                cTANTOUSHA = code.ToString().PadLeft(4, '0');
                reader.Close();
                cn_customer.Close();
            }
            catch(Exception ex)
            {
                cn_customer.Close();
                return false;
            }
            //20211208 Added エインドリ－・プ－プゥ (End)

            //DataTable dt_ctantu = new DataTable();
            //try
            //{
            //    string sql_ctantou = "";
            //    sql_ctantou = "select MAX(cTANTOUSHA) as cTANTOUSHA from m_j_tantousha where cTANTOUSHA is not null ";

            //    cn_customer.Open();
            //    using (MySqlDataAdapter adap = new MySqlDataAdapter(sql_ctantou, cn_customer))
            //    {
            //        adap.Fill(dt_ctantu);
            //    }
            //    cn_customer.Close();

            //}
            //catch
            //{
            //    cn_customer.Close();
            //    return false;
            //}
            //if (dt_ctantu.Rows.Count == 0)
            //{
            //    return false;
            //}
            //cTANTOUSHA = dt_ctantu.Rows[0]["cTANTOUSHA"].ToString();
            //if (cTANTOUSHA != string.Empty)
            //{
            //    int cTANTOUSHAInt = Convert.ToInt32(cTANTOUSHA);
            //    cTANTOUSHAInt++;
            //    cTANTOUSHA = string.Format("{0:D4}", cTANTOUSHAInt);
            //}
            //else
            //{
            //    cTANTOUSHA = string.Format("{0:D10}", 1);
            //}         

            DateTime date_save = ConstantVal.Fu_GetDateTime(cn_customer);

            string sql_insert = "";
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
            sql_insert += " ,dTAISHA";
            sql_insert += " )";
            sql_insert += " Values(";
            sql_insert += " '"+ cTANTOUSHA + "'";
            sql_insert += " ,'" + sTANTOUSHA + "'";
            sql_insert += " ,'"+ cBUMON + "'"; //20220322 Update エインドリ－
            sql_insert += " ,'" + SMAIL + "'";
            sql_insert += " ,'" + sPWD + "'";
            sql_insert += " ,'"+ fKANRISHA + "'";
            sql_insert += " ,'" + date_save + "'";
            sql_insert += " ,'"+ cHENKOUSYA + "'";
            sql_insert += " ,'"+ fTAISYA +"'";
            sql_insert += " ,'"+ cKYOTEN + "'"; //20220322 Update エインドリ－
            sql_insert += " ,'" + ckengenn + "'"; //20220322 Update エインドリ－
            if (fTAISYA=="0") //20211215 added エインドリ－・プ－プゥ
            {
                sql_insert += " ,NULL";
            }
            else
            {
                if(dTAISYA!="")
                {
                    sql_insert += " ,'" + dTAISYA + "'";
                }
                else
                {
                    sql_insert += " ,NULL";
                }
            }
            sql_insert += " );";
            
            cn_customer.Open();
            MySqlTransaction tran = cn_customer.BeginTransaction();
            try
            {
                MySqlCommand c = new MySqlCommand(sql_insert, cn_customer);
                c.ExecuteNonQuery();
                tran.Commit();
                cn_customer.Close();

                string sql_insert_MainDB = "";
                sql_insert_MainDB += @"INSERT INTO";
                sql_insert_MainDB += " contacts";
                sql_insert_MainDB += " (";
                sql_insert_MainDB += " id";
                sql_insert_MainDB += " ,contact_email";
                sql_insert_MainDB += " ,contact_name";
                sql_insert_MainDB += " ,contact_password";
                sql_insert_MainDB += " ,customer_id";
                sql_insert_MainDB += " ,contact_flg";
                sql_insert_MainDB += " )";
                sql_insert_MainDB += " Values(";
                sql_insert_MainDB += " " + sql_insert_id + "";
                sql_insert_MainDB += " ,'" + SMAIL + "'";
                sql_insert_MainDB += " ,'" + sTANTOUSHA + "'";
                sql_insert_MainDB += " ,'" + sPWD + "'";
                sql_insert_MainDB += " ,'" + JC01Login_Class.DB + "'";
                sql_insert_MainDB += " ,'0'";
                sql_insert_MainDB += " );";

                //MySqlConnection cn_MainDB = new MySqlConnection("Server=" + DBUtilitycs.Server + "; Database=" + JC01Login_Class.DB + "; User Id=" + DBUtilitycs.user + "; password=" + DBUtilitycs.pass);
                //20211208 Updated Database value By エインドリ－・プ－プゥ
                MySqlConnection cn_MainDB = new MySqlConnection("Server=" + DBUtilitycs.Server + "; Database=" + DBUtilitycs.Database + "; User Id=" + DBUtilitycs.user + "; password=" + DBUtilitycs.pass);
                cn_MainDB.Open();
                MySqlTransaction tran_MainDB = cn_MainDB.BeginTransaction();
                try
                {
                    c = new MySqlCommand(sql_insert_MainDB, cn_MainDB);
                    c.ExecuteNonQuery();
                    tran_MainDB.Commit();
                    cn_MainDB.Close();
                }
                catch
                {
                    try
                    {
                        tran_MainDB.Rollback();
                    }
                    catch
                    {
                    }
                    cn_MainDB.Close();
                    return false;
                }

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
    }
}
