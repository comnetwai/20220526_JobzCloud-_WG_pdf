/*作成者：ナン
 *作成日：20210901
 */
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
     public class JC99NavBar_Class
    { 
        public string loginId { get; set; }

        MySqlConnection M_con = new MySqlConnection("Server=" + DBUtilitycs.Server + "; Database=" + DBUtilitycs.Database + "; User Id=" + DBUtilitycs.user + "; password=" + DBUtilitycs.pass);

        MySqlConnection con = new MySqlConnection();
        
        public string LoginName { get; set; }
        public string loginMail { get; set; }
        public static string Login_Tan_Code { get; set; }
        public static string Login_Tan_Name { get; set; }
        public string login_Code { get; set; }

       
        public void ReadConn()
        {           
            con = new MySqlConnection("Server=" + DBUtilitycs.Server + "; Database=" +  ConstantVal.DB_NAME + "; User Id=" + DBUtilitycs.user + "; password=" + DBUtilitycs.pass);

        }
        public void FindLoginName()
        {
            ReadConn();
            string sql = "";
            sql = "SELECT cTANTOUSHA,sTANTOUSHA FROM m_j_tantousha where fTAISYA = 0 and sMAIL ='"+ loginId + "'";
            DataTable dt_name = new DataTable();
            using (MySqlDataAdapter adap = new MySqlDataAdapter(sql, con))
            {
                adap.Fill(dt_name);
            }
            if (dt_name.Rows.Count > 0)
            {
                LoginName = dt_name.Rows[0]["sTANTOUSHA"].ToString();
                Login_Tan_Name = dt_name.Rows[0]["sTANTOUSHA"].ToString();
                Login_Tan_Code = dt_name.Rows[0]["cTANTOUSHA"].ToString();
                login_Code =  dt_name.Rows[0]["cTANTOUSHA"].ToString();
            }
        }

    }
}