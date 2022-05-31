using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;
using Common;

namespace Service
{
    public class JC34UriageList_Class
    {
        public static DataTable dt_uriage_hyouji = new DataTable();
        public DataTable Uriage_Hyouji()
        {
            
            MySqlConnection cn_customer = new MySqlConnection("Server=" + DBUtilitycs.Server + "; Database=" + ConstantVal.DB_NAME + "; User Id=" + DBUtilitycs.user + "; password=" + DBUtilitycs.pass);
            try
            {
                dt_uriage_hyouji = new DataTable();
                string sql_hyoji_list = "";
                sql_hyoji_list = "select ";
                sql_hyoji_list += " mhk.cHYOUJI cHYOUJI";
                sql_hyoji_list += ",mhk.sHYOUJI sHYOUJI";
                sql_hyoji_list += " from m_hyoujikomoku mhk";
                sql_hyoji_list += " Where mhk.cLIST ='3'";
                sql_hyoji_list += " and mhk.fHYOUJI='0'";
                sql_hyoji_list += " order by nORDER asc";
                cn_customer.Open();
                using (MySqlDataAdapter adap = new MySqlDataAdapter(sql_hyoji_list, cn_customer))
                {
                    adap.Fill(dt_uriage_hyouji);
                }
                cn_customer.Close();

            }
            catch
            {
                cn_customer.Close();
            }

            return dt_uriage_hyouji;
        }

        public DataTable UrigaeList(string searchSql)        {
            DataTable dt_uriage_list = new DataTable();            MySqlConnection cn_customer = new MySqlConnection("Server=" + DBUtilitycs.Server + "; Database=" + ConstantVal.DB_NAME + "; User Id=" + DBUtilitycs.user + "; password=" + DBUtilitycs.pass);            try            {
                string str_uriage = "";                str_uriage = " SELECT ru.cURIAGE as 売上コード";                str_uriage += ",rum.cMITUMORI as 見積コード";                str_uriage += ",ru.sSEIKYUSAKI as 請求先名";                str_uriage += ",ru.sTOKUISAKI as 得意先名";                str_uriage += ",ru.snouhin as 売上件名";
                //str_uriage += ",ru.cJYOTAI_Uriage as 売上状態";
                str_uriage += ",case IFNULL(ru.cJYOTAI_Uriage,'') when 00 then '作成中'";  //売上状態
                str_uriage += " when 01 then '作成済'";
                str_uriage += " when 02 then '請求締処理'";
                str_uriage += " when 03 then '入金'";
                str_uriage += " when '04' then '売掛締処理'";
                str_uriage += " else '' end As 売上状態";                str_uriage += ",mjt.sTANTOUSHA as 営業担当者";                str_uriage += ",date_format(ru.dURIAGE,'%Y/%m/%d') as 売上日";                str_uriage += ",FORMAT(ru.nKINGAKU, 'C0') 売上金額";                str_uriage += ",ru.sMemo as 売上社内メモ";
                str_uriage += "  from r_uriage as ru";                str_uriage += " left join r_uri_mitsu as rum on ru.cURIAGE = rum.cURIAGE ";                str_uriage += " left join m_j_tantousha as mjt ";                str_uriage += " on ru.cEIGYOTANTOSYA = mjt.cTANTOUSHA ";                str_uriage += " left join r_uriage_m as rm ";                str_uriage += " on rm.cURIAGE = ru.cURIAGE ";                str_uriage += " where 1=1" ;                if(searchSql != "")
                {
                    str_uriage += searchSql;
                }                str_uriage += " group by ru.cURIAGE order by 売上コード desc";                using (MySqlDataAdapter adap = new MySqlDataAdapter(str_uriage, cn_customer))                {                    adap.Fill(dt_uriage_list);                }                cn_customer.Close();            }            catch            {                cn_customer.Close();            }            return dt_uriage_list;        }

        //public DataTable UrigaeList1(string searchSql)        //{        //    DataTable dt_uriage_list = new DataTable();        //    MySqlConnection cn_customer = new MySqlConnection("Server=" + DBUtilitycs.Server + "; Database=" + ConstantVal.DB_NAME + "; User Id=" + DBUtilitycs.user + "; password=" + DBUtilitycs.pass);        //    try        //    {        //        string str_uriage = "";        //        str_uriage = " SELECT ifnull(ru.cURIAGE, '') as 売上コード";
        //        //str_uriage += ",rum.cMITUMORI as 見積コード";
        //        //str_uriage += ",ru.sSEIKYUSAKI as 請求先名";
        //        //str_uriage += ",ru.sTOKUISAKI as 得意先名";
        //        //str_uriage += ",ru.snouhin as 売上件名";
        //        ////str_uriage += ",ru.cJYOTAI_Uriage as 売上状態";
        //        //str_uriage += ",case IFNULL(ru.cJYOTAI_Uriage,'') when 00 then '作成中'";  //売上状態
        //        //str_uriage += " when 01 then '作成済'";
        //        //str_uriage += " when 02 then '請求締処理'";
        //        //str_uriage += " when 03 then '入金'";
        //        //str_uriage += " when '04' then '売掛締処理'";
        //        //str_uriage += " else '' end As 売上状態";
        //        //str_uriage += ",mjt.sTANTOUSHA as 営業担当者";
        //        //str_uriage += ",date_format(ru.dURIAGE,'%Y/%m/%d') as 売上日";
        //        //str_uriage += ",FORMAT(ru.nKINGAKU, 'C0') 売上金額";
        //        //str_uriage += ",ru.sMemo as 売上社内メモ";
        //        str_uriage += "  from r_uriage as ru";        //        str_uriage += " left join r_uri_mitsu as rum on ru.cURIAGE = rum.cURIAGE ";        //        str_uriage += " left join m_j_tantousha as mjt ";        //        str_uriage += " on ru.cEIGYOTANTOSYA = mjt.cTANTOUSHA ";        //        str_uriage += " left join r_uriage_m as rm ";        //        str_uriage += " on rm.cURIAGE = ru.cURIAGE ";        //        str_uriage += " where 1=1";        //        if (searchSql != "")        //        {        //            str_uriage += searchSql;        //        }        //        str_uriage += " group by ru.cURIAGE order by 売上コード desc";        //        using (MySqlDataAdapter adap = new MySqlDataAdapter(str_uriage, cn_customer))        //        {        //            adap.Fill(dt_uriage_list);        //        }        //        cn_customer.Close();        //    }        //    catch        //    {        //        cn_customer.Close();        //    }        //    return dt_uriage_list;        //}
    }
}
