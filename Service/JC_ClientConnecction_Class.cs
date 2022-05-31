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
    public class JC_ClientConnecction_Class
    {
        //public MySqlConnection M_con = new MySqlConnection("Server=" + DBUtilitycs.Server + "; Database=" + DBUtilitycs.Database + "; User Id=" + DBUtilitycs.user + "; password=" + DBUtilitycs.pass);
        public string loginId { get; set; }
        public MySqlConnection GetConnection()
        {
            MySqlConnection cn = null;
            //string strCustomer_Id = "";
            //DataTable dt_Customer_info = new DataTable();
            //dt_Customer_info = ConstantVal.Fu_GetContacts(M_con, loginId);
            //if (dt_Customer_info.Rows.Count > 0)
            //{
            //    strCustomer_Id = dt_Customer_info.Rows[0]["customer_id"].ToString();
            //}

            //return cn = new MySqlConnection("Server=" + DBUtilitycs.Server + "; Database=" + strCustomer_Id + "; User Id=" + DBUtilitycs.user + "; password=" + DBUtilitycs.pass);
            return cn = new MySqlConnection("Server=" + DBUtilitycs.Server + "; Database=" + ConstantVal.DB_NAME + "; User Id=" + DBUtilitycs.user + "; password=" + DBUtilitycs.pass);

        }

        #region GetLoginUserCodeFromClientDB
        public DataTable GetLoginUserCodeFromClientDB()
        {
            MySqlConnection cn = GetConnection();
            String qr = "SELECT cTANTOUSHA as code,sTANTOUSHA as name FROM m_j_tantousha WHERE sMAIL='"+loginId+"';";
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandTimeout = 0;
            cmd = new MySqlCommand(qr, cn);
            cn.Open();
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            cn.Close();
            da.Dispose();
            return dt;
        }
        #endregion

        #region GetMissingCode
        public Int64 GetMissingCode(DataTable dt, params Int64[] MaxCount)
        {
            int code = 1;
            bool ret = false;
            if (MaxCount.Length > 0)
            {
                for (int mc = 1; mc <= MaxCount[0]; mc++)
                {

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {

                        if (Convert.ToInt64(dt.Rows[i][0].ToString()) == mc)
                        {
                            ret = true;
                            dt.Rows.RemoveAt(i);
                            break;
                        }
                    }

                    if (ret == false)
                    {
                        code = mc;
                        return code;

                    }
                    else
                    {
                        ret = false;
                    }

                }
            }
            else
            {
                if (dt.Rows.Count > 0)
                {
                    return Convert.ToInt32(dt.Rows[dt.Rows.Count - 1][0].ToString()) + 1;
                }
                else
                {
                    return 1;
                }
            }
            return code;
        }
        #endregion

        #region CheckKengen
        public Boolean CheckKengen()
        {
            DataTable dt_loginuser = GetLoginUserCodeFromClientDB();
            String ctantou = dt_loginuser.Rows[0]["code"].ToString();
            MySqlConnection cn = GetConnection();
            String qr = "SELECT ckengenn FROM m_j_tantousha where cTANTOUSHA='"+ctantou+"';";
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandTimeout = 0;
            cmd = new MySqlCommand(qr, cn);
            cn.Open();
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            cn.Close();
            da.Dispose();
            if (dt.Rows[0]["ckengenn"].ToString() == "01")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region GetKakudo
        public DataTable GetKakudo()
        {
            MySqlConnection cn = GetConnection();
            String qr = "SELECT cKAKUDO as cKAKUDO,sKAKUDO as sKAKUDO FROM m_kakudo WHERE fDEL='0' order by nJUNBAN;";
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandTimeout = 0;
            cmd = new MySqlCommand(qr, cn);
            cn.Open();
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dt.Rows.InsertAt(dt.NewRow(),0);
            cn.Close();
            da.Dispose();
            return dt;
        }
        #endregion

        #region ExecuteInsatsuSetting
        public DataTable ExecuteInsatsuSetting(String cTantousya)
        {
            MySqlConnection cn = GetConnection();
            String qr = "SELECT fLogo,fZei,fMidashi,fMeisai,fSyosai FROM m_j_tantousha WHERE cTANTOUSHA='" + cTantousya+"';";
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandTimeout = 0;
            cmd = new MySqlCommand(qr, cn);
            cn.Open();
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            cn.Close();
            da.Dispose();
            return dt;
        }
        #endregion

        #region GetCurrentDate
        public DateTime GetCurrentDate()
        {
            MySqlConnection cn = GetConnection();
            String qr = "SELECT NOW() as date";
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandTimeout = 0;
            cmd = new MySqlCommand(qr, cn);
            cn.Open();
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            cn.Close();
            da.Dispose();
            DateTime cdate = Convert.ToDateTime(dt.Rows[0]["date"].ToString());
            return cdate;
        }
        #endregion

        #region GetKyoten
        public DataTable GetKyoten(String cTantousya)
        {
            MySqlConnection cn = GetConnection();
            String qr = "SELECT if(ifnull(mjt.cKYOTEN,'01')='','01',ifnull(mjt.cKYOTEN,'01')) as cKYOTEN,mji.sKYOTEN as sKYOTEN FROM m_j_tantousha as mjt LEFT JOIN m_j_info as mji ON if(ifnull(mjt.cKYOTEN,'01')='','01',ifnull(mjt.cKYOTEN,'01'))=mji.cCO WHERE cTANTOUSHA='"+cTantousya+"';";
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandTimeout = 0;
            cmd = new MySqlCommand(qr, cn);
            cn.Open();
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            cn.Close();
            da.Dispose();
            return dt;
        }
        #endregion

        #region CheckSystemKanrisya
        public Boolean CheckSystemKanrisya()
        {
            DataTable dt_loginuser = GetLoginUserCodeFromClientDB();
            String ctantou = dt_loginuser.Rows[0]["code"].ToString();
            MySqlConnection cn = GetConnection();
            String qr = "SELECT fKANRISHA FROM m_j_tantousha where cTANTOUSHA='" + ctantou + "';";
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandTimeout = 0;
            cmd = new MySqlCommand(qr, cn);
            cn.Open();
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            cn.Close();
            da.Dispose();
            if (dt.Rows[0]["fKANRISHA"].ToString() == "1")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region IsExistShijishou
        public Boolean IsExistShijishou(String cMitumori)
        {
            MySqlConnection cn = GetConnection();
            String qr = "SELECT EXISTS(SELECT * FROM r_shijisyo_mitsu WHERE cMITUMORI = '"+cMitumori+"') as exist;";
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

        #region IsExistUriage
        public Boolean IsExistUriage(String cMitumori)
        {
            MySqlConnection cn = GetConnection();
            String qr = "SELECT EXISTS(SELECT * FROM r_uri_mitsu WHERE cMITUMORI = '"+cMitumori+"') as exist";
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
