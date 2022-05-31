/***************************************************************
 * ＜作成者＞     MiMi
 * ＜作成日＞     20220330
 ***************************************************************/

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
    public class JC21SyouhinKensaku_Class
    {
        public MySqlConnection M_con = new MySqlConnection("Server=" + DBUtilitycs.Server + "; Database=" + DBUtilitycs.Database + "; User Id=" + DBUtilitycs.user + "; password=" + DBUtilitycs.pass);
        public string loginId { get; set; }
        public MySqlConnection GetConnection()
        {
            MySqlConnection cn = null;
            string strCustomer_Id = "";
            DataTable dt_Customer_info = new DataTable();
            dt_Customer_info = ConstantVal.Fu_GetContacts(M_con, loginId);
            if (dt_Customer_info.Rows.Count > 0)
            {
                strCustomer_Id = dt_Customer_info.Rows[0]["customer_id"].ToString();
            }

            return cn = new MySqlConnection("Server=" + DBUtilitycs.Server + "; Database=" + strCustomer_Id + "; User Id=" + DBUtilitycs.user + "; password=" + DBUtilitycs.pass);

        }

        #region IsExistMitumoriSyouhin
        public Boolean IsExistMitumoriSyouhin(String cSyouhin)
        {
            MySqlConnection cn = GetConnection();
            String qr = "SELECT EXISTS(SELECT * FROM r_mitumori_m WHERE cSYOUHIN = '" + cSyouhin + "') as exist";
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandTimeout = 0;
            cmd = new MySqlCommand(qr, cn);
            cn.Open();
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            cn.Close();
            da.Dispose();
            Boolean isExist = false;
            if (dt.Rows[0][0].ToString() == "1")
            {
                isExist = true;
            }
            return isExist;
        }
        #endregion

    }
}