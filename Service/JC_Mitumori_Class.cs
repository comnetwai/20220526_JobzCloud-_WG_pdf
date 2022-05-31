/***************************************************************
 * ＜作成者＞     MiMi
 * ＜作成日＞     20211006
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
    public class JC_Mitumori_Class
    {
        public String GetKyoten(String cMitumori)
        {
            //MySqlConnection cn = new MySqlConnection("Server=" + DBUtilitycs.Server + "; Database=" + JC01Login_Class.DB + "; User Id=" + DBUtilitycs.user + "; password=" + DBUtilitycs.pass);
            MySqlConnection cn  = new MySqlConnection("Server=" + DBUtilitycs.Server + "; Database=" + ConstantVal.DB_NAME + "; User Id=" + DBUtilitycs.user + "; password=" + DBUtilitycs.pass);
            String cKyoten = "01";
            try
            {
                string sql = "SELECT cKYOTEN FROM r_mitumori Where cMitumori='" + cMitumori + "';";
                MySqlCommand cmd = new MySqlCommand(sql, cn);
                cmd.CommandTimeout = 0;
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                cn.Close();
                da.Dispose();
                if (dt.Rows.Count > 0)
                {
                    if (!String.IsNullOrEmpty(dt.Rows[0][0].ToString()))
                    {
                        cKyoten = dt.Rows[0][0].ToString();
                    }
                }
            }
            catch
            {
                cn.Close();
            }

            return cKyoten;
        }
    }
}