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
    public class JC27UriageTouroku_Class
    {
        #region
        public DataTable dt_data = new DataTable();
        public string cURIAGE { get; set; }
        public string dURIAGE { get; set; }
        public string dNYUUKINYOTEI { get; set; }
        public string dDate { get; set; }
        public string snouhin { get; set; }
        public DateTime dHENKOU { get; set; }
        public string cHENKOUSYA { get; set; }
        public string cTOKUISAKI { get; set; }
        public string sTOKUISAKI { get; set; }
        public string sTOKUISAKI_TAN { get; set; }
        public string sTOKUISAKI_TAN_Jun { get; set; }
        public string sTOKUISAKI_TANBUMON { get; set; }
        public string sTOKUISAKI_YAKUSYOKU { get; set; }
        public string sTOKUISAKI_KEISYO { get; set; }
        public string cSEIKYUSAKI { get; set; }
        public string sSEIKYUSAKI { get; set; }
        public string sSEIKYU_TAN { get; set; }
        public string sSEIKYU_TAN_Jun { get; set; }
        public string sSEIKYU_TANBUMON { get; set; }
        public string sSEIKYU_YAKUSHOKU { get; set; }
        public string sSEIKYU_KEISYO { get; set; }
        public Decimal nKINGAKU { get; set; }
        public Decimal nuriage_kingaku { get; set; }
        public Decimal nnebiki_kingaku { get; set; }
        public Decimal nsyoukei_kingkau { get; set; }
        public Decimal nhenpin_kingkau { get; set; }
        public Decimal ntatekae_kingaku { get; set; }
        public Decimal nKAZEIKINGAKU { get; set; }
        public Decimal nutihikazei { get; set; }
        public string sbikou { get; set; }
        public string cEIGYOTANTOSYA { get; set; }
        public string fprint { get; set; }
        public string sMemo { get; set; }
        public Decimal nsyohizei { get; set; }

        public static DataTable dt_J_Info = new DataTable();
        public string cMITUMORI { get; set; }
        public string NJUNBAN { get; set; }
        public string cTANTOUSHA { get; set; }
        public string hiduke { get; set; }

        public string rogo { get; set; }

        public string Shoshiki { get; set; }

        public string kingaku { get; set; }

        public string seikyubikou { get; set; }

        public string cPJ { get; set; }


        public bool f_new = true;

        private int data_end_row = -1;

        public static DataTable dt_tani = new DataTable();
        #endregion

        #region 得意先担当情報取る
        public DataTable Tokuisaki_Tantou_Jyohou()
        {
            DataTable dt_tokuisaki_tantou_jyouhou = new DataTable();
            MySqlConnection cn_customer = new MySqlConnection("Server=" + DBUtilitycs.Server + "; Database=" + ConstantVal.DB_NAME + "; User Id=" + DBUtilitycs.user + "; password=" + DBUtilitycs.pass);

            string sql_tantou_jyouhou = "";
            sql_tantou_jyouhou = "select ";
            sql_tantou_jyouhou += " ifnull(tt.SBUMON,'') as SBUMON";
            sql_tantou_jyouhou += ",ifnull(tt.SYAKUSHOKU,'') as SYAKUSHOKU";
            sql_tantou_jyouhou += ",ifnull(tt.SKEISHOU,'') as SKEISHOU";
            sql_tantou_jyouhou += " from tokuisaki_tantousha tt";
            sql_tantou_jyouhou += " where tt.CTOKUISAKI ='" + cTOKUISAKI + "'";
            sql_tantou_jyouhou += " and tt.NJUNBAN ='" + NJUNBAN + "'";

            cn_customer.Open();
            using (MySqlDataAdapter adap = new MySqlDataAdapter(sql_tantou_jyouhou, cn_customer))
            {
                adap.Fill(dt_tokuisaki_tantou_jyouhou);
            }
            cn_customer.Close();
            return dt_tokuisaki_tantou_jyouhou;
        }
        #endregion

        #region 売上新規作成為に見積明細データを取る
        public DataTable Mitsu_Meisai_Data(string cmitsu)
        {
            DataTable dt_Mitsu_Meisai_Data = new DataTable();
            MySqlConnection cn_customer = new MySqlConnection("Server=" + DBUtilitycs.Server + "; Database=" + ConstantVal.DB_NAME + "; User Id=" + DBUtilitycs.user + "; password=" + DBUtilitycs.pass);

            string sql_Mitsu_Meisai_Data = "";
            sql_Mitsu_Meisai_Data = "select ";
            sql_Mitsu_Meisai_Data += " ifnull(rmm.cSYOUHIN,'') as cSYOUHIN";
            sql_Mitsu_Meisai_Data += ",ifnull(rmm.sSYOUHIN_R,'') as sSYOUHIN_R";
            sql_Mitsu_Meisai_Data += ",ifnull(rmm.sTANI,'') as sTANI";
            sql_Mitsu_Meisai_Data += ",ifnull(rmm.nSIKIRITANKA,0) as nSIKIRITANKA";
            sql_Mitsu_Meisai_Data += ",ifnull(rmm.nSIKIRIKINGAKU,0) as nSIKIRIKINGAKU";
            sql_Mitsu_Meisai_Data += ",ifnull(rmm.nSURYO,0) as nSURYO";
            sql_Mitsu_Meisai_Data += ",'' as sbikou";
            sql_Mitsu_Meisai_Data += ",'課税' as fKazei";
            sql_Mitsu_Meisai_Data += ",'売上' as skubun";
            sql_Mitsu_Meisai_Data += " from r_mitumori_m rmm";
            sql_Mitsu_Meisai_Data += " where rmm.cMITUMORI ='" + cmitsu + "'";
            sql_Mitsu_Meisai_Data += " order by rmm.nGYOUNO asc";

            cn_customer.Open();
            using (MySqlDataAdapter adap = new MySqlDataAdapter(sql_Mitsu_Meisai_Data, cn_customer))
            {
                adap.Fill(dt_Mitsu_Meisai_Data);
            }
            cn_customer.Close();
            return dt_Mitsu_Meisai_Data;
        }
        #endregion

        #region 商品によって課税か非課税か
        public DataTable dt_syouhin_kazei(string cSYOUHIN)
        {
            DataTable dt_syouhin_kazei = new DataTable();
            MySqlConnection cn_customer = new MySqlConnection("Server=" + DBUtilitycs.Server + "; Database=" + ConstantVal.DB_NAME + "; User Id=" + DBUtilitycs.user + "; password=" + DBUtilitycs.pass);

            string sql_syouhin_kazei = "";
            sql_syouhin_kazei = "select ";
            sql_syouhin_kazei += " ifnull(ms.cSYOUHIN,'') AS cSYOUHIN";
            sql_syouhin_kazei += ",ifnull(ms.fKazei,0) AS fKazei";
            sql_syouhin_kazei += ",ifnull(nSYOUKISU, 1) as nSYOUKISU";
            sql_syouhin_kazei += ",ifnull(sSYOUHIN, '') as sSYOUHIN";
            sql_syouhin_kazei += ",ifnull(sSHIYOU,'') as sSHIYOU";
            sql_syouhin_kazei += ",ifnull(sTANI, '') as sTANI";
            sql_syouhin_kazei += ",ifnull(nHANNBAIKAKAKU, 0) as nHANNBAIKAKAKU";
            sql_syouhin_kazei += " from m_syouhin ms";
            sql_syouhin_kazei += " left join m_syouhin_kubun msk ON ms.cSYOUHIN_KUBUN=msk.cKUBUN";
            sql_syouhin_kazei += " WHERE 1=1";
            sql_syouhin_kazei += " and ms.cSYOUHIN='" + cSYOUHIN + "'";

            cn_customer.Open();
            using (MySqlDataAdapter adap = new MySqlDataAdapter(sql_syouhin_kazei, cn_customer))
            {
                adap.Fill(dt_syouhin_kazei);
            }
            cn_customer.Close();

            return dt_syouhin_kazei;
        }
        #endregion

        #region 自社情報
        public bool J_Info()
        {
            dt_J_Info = new DataTable();

            MySqlConnection cn_customer = new MySqlConnection("Server=" + DBUtilitycs.Server + "; Database=" + ConstantVal.DB_NAME + "; User Id=" + DBUtilitycs.user + "; password=" + DBUtilitycs.pass);

            try
            {
                string sql_J_Info = "";
                sql_J_Info = "select";
                sql_J_Info += " ifnull(mji.fkeisantani,'1') AS fkeisantani";
                sql_J_Info += ",ifnull(mji.ftansuushori,'1') AS ftansuushori";
                sql_J_Info += ",ifnull(mji.fKKUBUN,'1') AS fKKUBUN";
                sql_J_Info += " from m_j_info mji";
                sql_J_Info += " WHERE mji.cCO='01'";

                cn_customer.Open();
                using (MySqlDataAdapter adap = new MySqlDataAdapter(sql_J_Info, cn_customer))
                {
                    adap.Fill(dt_J_Info);
                }
                cn_customer.Close();
                return true;
            }
            catch
            {
                cn_customer.Close();
                return false;
            }
        }
        #endregion

        #region 現在の納品『売上』以外納品金額合計
        public decimal Get_NouhinKingaku(string cMITUMORI, string cUriage)
        {
            decimal NouhinKingaku = 0;
            DataTable dt_NouhinKingaku = new DataTable();

            MySqlConnection cn_customer = new MySqlConnection("Server=" + DBUtilitycs.Server + "; Database=" + ConstantVal.DB_NAME + "; User Id=" + DBUtilitycs.user + "; password=" + DBUtilitycs.pass);

            try
            {
                string sql_NouhinKingaku = "";
                sql_NouhinKingaku = " select";
                sql_NouhinKingaku += " ifnull(sum(ru.nsyoukei_kingkau),0) as nKINGAKU";
                sql_NouhinKingaku += " from r_uriage as ru";
                sql_NouhinKingaku += " inner join r_uri_mitsu as rum";
                sql_NouhinKingaku += " on ru.cURIAGE=rum.cURIAGE";
                sql_NouhinKingaku += " and rum.cMITUMORI='" + cMITUMORI + "'";
                sql_NouhinKingaku += " and (ru.dURIAGE is not null or ru.dURIAGE!='')";
                sql_NouhinKingaku += " and ru.cURIAGE<>'" + cUriage + "'";

                cn_customer.Open();
                using (MySqlDataAdapter adap = new MySqlDataAdapter(sql_NouhinKingaku, cn_customer))
                {
                    adap.Fill(dt_NouhinKingaku);
                }
                cn_customer.Close();

                if (dt_NouhinKingaku.Rows.Count > 0)
                {
                    string NouhinKingaku_goukei = dt_NouhinKingaku.Rows[0]["nKINGAKU"].ToString();
                    if (NouhinKingaku_goukei != "")
                    {
                        NouhinKingaku = decimal.Parse(NouhinKingaku_goukei);
                    }
                }
                return NouhinKingaku;
            }
            catch
            {
                cn_customer.Close();
                return 0;
            }
        }
        #endregion

        #region 請求区分
        public static string Get_fseikyuukubun(string cSeikyusaki)
        {
            string fseikyuukubun = "0";
            DataTable dt_fseikyuukubun = new DataTable();

            MySqlConnection cn_customer = new MySqlConnection("Server=" + DBUtilitycs.Server + "; Database=" + ConstantVal.DB_NAME + "; User Id=" + DBUtilitycs.user + "; password=" + DBUtilitycs.pass);

            try
            {
                string sql_fseikyuukubun = "";
                sql_fseikyuukubun = "select ";
                sql_fseikyuukubun += "ifnull(mt.fseikyuukubun,'0') AS fseikyuukubun";
                sql_fseikyuukubun += " from m_tokuisaki mt";
                sql_fseikyuukubun += " where mt.cTOKUISAKI='" + cSeikyusaki + "'";

                cn_customer.Open();
                using (MySqlDataAdapter adap = new MySqlDataAdapter(sql_fseikyuukubun, cn_customer))
                {
                    adap.Fill(dt_fseikyuukubun);
                }
                cn_customer.Close();

                if (dt_fseikyuukubun.Rows.Count > 0)
                {
                    fseikyuukubun = dt_fseikyuukubun.Rows[0]["fseikyuukubun"].ToString();
                }
                return fseikyuukubun;
            }
            catch
            {
                cn_customer.Close();
                return "0";
            }
        }
        #endregion

        #region 消費税パーセンテージ
        public static string Get_per(string duriage)
        {
            MySqlConnection cn_customer = new MySqlConnection("Server=" + DBUtilitycs.Server + "; Database=" + ConstantVal.DB_NAME + "; User Id=" + DBUtilitycs.user + "; password=" + DBUtilitycs.pass);

            string per = "";
            DataTable dt_nsyouhizei = new DataTable();
            string sql_nsyouhizei = "";
            sql_nsyouhizei = "select";
            sql_nsyouhizei += " ifnull(nSYOUHIZEI,0) as nSYOUHIZEI";
            sql_nsyouhizei += ",ifnull(date_format(dDATE,'%Y/%m/%d'),'') as dDATE";
            sql_nsyouhizei += " from m_syouhizei";
            sql_nsyouhizei += " where dDATE <='" + duriage + "' order by dDATE DESC";
            cn_customer.Open();
            using (MySqlDataAdapter adap = new MySqlDataAdapter(sql_nsyouhizei, cn_customer))
            {
                adap.Fill(dt_nsyouhizei);
            }
            cn_customer.Close();

            if (dt_nsyouhizei.Rows.Count > 0)
            {
                per = dt_nsyouhizei.Rows[0]["nSYOUHIZEI"].ToString();
            }
            return per;
        }
        #endregion

        #region 売上コード取る
        public string Uriage_code()
        {
            MySqlConnection cn_customer = new MySqlConnection("Server=" + DBUtilitycs.Server + "; Database=" + ConstantVal.DB_NAME + "; User Id=" + DBUtilitycs.user + "; password=" + DBUtilitycs.pass);

            cURIAGE = "";

            #region 売上コード取る
            DataTable dt_curiage = new DataTable();
            try
            {
                string sql_curiage = "";
                sql_curiage = "select ifnull(MAX(cURIAGE),0) as cURIAGE from r_uriage where cURIAGE is not null ";

                cn_customer.Open();
                using (MySqlDataAdapter adap = new MySqlDataAdapter(sql_curiage, cn_customer))
                {
                    adap.Fill(dt_curiage);
                }
                cn_customer.Close();

            }
            catch
            {
                cn_customer.Close();
                return cURIAGE;
            }
            if (dt_curiage.Rows.Count == 0)
            {
                return cURIAGE;
            }
            cURIAGE = dt_curiage.Rows[0]["cURIAGE"].ToString();
            if (cURIAGE != string.Empty)
            {
                int cURIAGE_int = Convert.ToInt32(cURIAGE);
                cURIAGE_int++;
                cURIAGE = string.Format("{0:D10}", cURIAGE_int);
            }
            else
            {
                cURIAGE = string.Format("{0:D10}", 1);
            }
            #endregion
            return cURIAGE;
        }
        #endregion

        #region　データ登録
        public bool DataSave()
        {
            MySqlConnection cn_customer = new MySqlConnection("Server=" + DBUtilitycs.Server + "; Database=" + ConstantVal.DB_NAME + "; User Id=" + DBUtilitycs.user + "; password=" + DBUtilitycs.pass);

            try
            {
                dHENKOU = ConstantVal.Fu_GetDateTime(cn_customer);
                string new_save_sql = "";
                if (f_new)　　//新規
                {
                    new_save_sql = SetData_r_uri_mitsu();
                    new_save_sql += SetData_r_uriage();
                    new_save_sql += SetData_r_uriage_m();

                }
                else　　//更新
                {
                    new_save_sql = DeleteSql();
                    new_save_sql += SetData_r_uri_mitsu();
                    new_save_sql += SetData_Updadte_r_uriage();
                    new_save_sql += SetData_r_uriage_m();
                   
                }

                cn_customer.Open();
                MySqlTransaction tran = cn_customer.BeginTransaction();
                try
                {
                    MySqlCommand c = new MySqlCommand(new_save_sql, cn_customer);
                    c.ExecuteNonQuery();
                    tran.Commit();
                    cn_customer.Close();
                }
                catch(Exception ex)
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
            }
            catch
            {
                cn_customer.Close();
                return false;
            }

            return true;
        }
        #endregion

        #region r_uriage_mとr_uri_mitsuを削除
        private string DeleteSql()
        {
            string sql_delete = "";

            sql_delete = @"DELETE FROM r_uriage_m WHERE cURIAGE='" + cURIAGE + "';";

            sql_delete += @"DELETE FROM r_uri_mitsu WHERE cURIAGE='" + cURIAGE + "' and  cMITUMORI='" + cMITUMORI + "';";

            return sql_delete;
        }
        #endregion

        #region r_uri_mitsu　新規登録
        private string SetData_r_uri_mitsu()
        {
            string sql_rumi = "";
            sql_rumi += @"INSERT INTO";
            sql_rumi += " r_uri_mitsu ";
            sql_rumi += " (";
            sql_rumi += " cURIAGE";
            sql_rumi += " ,cMITUMORI";
            sql_rumi += " ,dHENKOU";
            sql_rumi += " ,cHENKOUSYA";
            sql_rumi += " )";
            sql_rumi += " Values(";
            sql_rumi += "'" + cURIAGE + "'";
            sql_rumi += ",'" + cMITUMORI + "'";
            sql_rumi += ",'" + dHENKOU + "'";
            sql_rumi += ",'" + cHENKOUSYA + "'";
            sql_rumi += " );";
            return sql_rumi;
        }
        #endregion

       

        #region r_uriage　新規登録
        private string SetData_r_uriage()
        {
            string sql_ru = "";
            //sql_ru = @"DELETE FROM r_uriage WHERE cURIAGE='" + cURIAGE + "';";

            sql_ru += @"INSERT INTO";
            sql_ru += " r_uriage ";
            sql_ru += " (";
            sql_ru += " cURIAGE";
            if (dURIAGE != "")
            {
                sql_ru += " ,dURIAGE";
            }
            sql_ru += " ,cJYOTAI_Uriage";
            if (dNYUUKINYOTEI != "")
            {
                sql_ru += " ,dNYUUKINYOTEI";
            }
            if (dDate != "")
            {
                sql_ru += " ,dDate";
            }
            sql_ru += " ,dURIAGESAKUSEI";
            sql_ru += " ,cSAKUSEISYA";
            sql_ru += " ,snouhin";
            sql_ru += " ,dHENKOU";
            sql_ru += " ,cHENKOUSYA";
            sql_ru += " ,cTOKUISAKI";
            sql_ru += " ,sTOKUISAKI";
            sql_ru += " ,sTOKUISAKI_TAN";
            sql_ru += " ,sTOKUISAKI_TAN_Jun";
            sql_ru += " ,sTOKUISAKI_TANBUMON";
            sql_ru += " ,sTOKUISAKI_YAKUSYOKU";
            sql_ru += " ,sTOKUISAKI_KEISYO";
            sql_ru += " ,cSEIKYUSAKI";
            sql_ru += " ,sSEIKYUSAKI";
            sql_ru += " ,sSEIKYU_TAN";
            sql_ru += " ,sSEIKYU_TAN_Jun";
            sql_ru += " ,sSEIKYU_TANBUMON";
            sql_ru += " ,sSEIKYU_YAKUSHOKU";
            sql_ru += " ,sSEIKYU_KEISYO";
            sql_ru += " ,nKINGAKU";
            sql_ru += " ,nuriage_kingaku";
            sql_ru += " ,nnebiki_kingaku";
            sql_ru += " ,nsyoukei_kingkau";
            sql_ru += " ,nhenpin_kingkau";
            sql_ru += " ,ntatekae_kingaku";
            sql_ru += " ,nKAZEIKINGAKU";
            sql_ru += " ,nutihikazei";
            sql_ru += " ,sbikou";
            sql_ru += " ,cEIGYOTANTOSYA";
            sql_ru += " ,fprint";
            sql_ru += " ,sMemo";
            sql_ru += " ,cPJ";
            sql_ru += " ,nsyohizei";
            sql_ru += " )";

            sql_ru += " Values(";
            sql_ru += "'" + cURIAGE + "'";
            if (dURIAGE != "")
            {
                sql_ru += ",'" + dURIAGE + "'";
                sql_ru += ",'01'";
            }
            else
            {
                sql_ru += ",'00'";
            }
            if (dNYUUKINYOTEI != "")
            {
                sql_ru += " ,'" + dNYUUKINYOTEI + "'";
            }
            if (dDate != "")
            {
                sql_ru += " ,'" + dDate + "'";
            }
            sql_ru += " ,'" + dHENKOU + "'";
            sql_ru += " ,'" + cHENKOUSYA + "'";
            sql_ru += " ,'" + snouhin + "'";
            sql_ru += " ,'" + dHENKOU + "'";
            sql_ru += " ,'" + cHENKOUSYA + "'";
            sql_ru += " ,'" + cTOKUISAKI + "'";
            sql_ru += " ,'" + sTOKUISAKI + "'";
            sql_ru += " ,'" + sTOKUISAKI_TAN + "'";
            sql_ru += " ,'" + sTOKUISAKI_TAN_Jun + "'";
            sql_ru += " ,'" + sTOKUISAKI_TANBUMON + "'";
            sql_ru += " ,'" + sTOKUISAKI_YAKUSYOKU + "'";
            sql_ru += " ,'" + sTOKUISAKI_KEISYO + "'";
            sql_ru += " ,'" + cSEIKYUSAKI + "'";
            sql_ru += " ,'" + sSEIKYUSAKI + "'";
            sql_ru += " ,'" + sSEIKYU_TAN + "'";
            sql_ru += " ,'" + sSEIKYU_TAN_Jun + "'";
            sql_ru += " ,'" + sSEIKYU_TANBUMON + "'";
            sql_ru += " ,'" + sSEIKYU_YAKUSHOKU + "'";
            sql_ru += " ,'" + sSEIKYU_KEISYO + "'";
            sql_ru += " ,'" + nKINGAKU + "'";
            sql_ru += " ,'" + nuriage_kingaku + "'";
            sql_ru += " ,'" + nnebiki_kingaku + "'";
            sql_ru += " ,'" + nsyoukei_kingkau + "'";
            sql_ru += " ,'" + nhenpin_kingkau + "'";
            sql_ru += " ,'" + ntatekae_kingaku + "'";
            sql_ru += " ,'" + nKAZEIKINGAKU + "'";
            sql_ru += " ,'" + nutihikazei + "'";
            sql_ru += " ,'" + sbikou + "'";
            sql_ru += " ,'" + cEIGYOTANTOSYA + "'";
            sql_ru += " ,'" + fprint + "'";
            sql_ru += " ,'" + sMemo + "'";
            sql_ru += " ,'" + cPJ + "'";
            sql_ru += " ,'" + nsyohizei + "'";
            sql_ru += " );";
            return sql_ru;
        }
        #endregion

        #region r_uriage　更新
        private string SetData_Updadte_r_uriage()
        {
            string sql_ru = "";

            sql_ru += @"Update r_uriage set";
            sql_ru += " snouhin='" + snouhin + "'";
            if (dURIAGE != "")
            {
                sql_ru += ",dURIAGE='" + dURIAGE + "'";
                sql_ru += ",cJYOTAI_Uriage='01'";
            }
            else
            {
                sql_ru += ",dURIAGE=null";
                sql_ru += ",cJYOTAI_Uriage='00'";
            }
            if (dNYUUKINYOTEI != "")
            {
                sql_ru += " ,dNYUUKINYOTEI='" + dNYUUKINYOTEI + "'";
            }
            else
            {
                sql_ru += " ,dNYUUKINYOTEI=null";
            }
            if (dDate != "")
            {
                sql_ru += " ,dDate='" + dDate + "'";
            }
            else
            {
                sql_ru += " ,dDate=null";
            }

            sql_ru += " ,dHENKOU='" + dHENKOU + "'";
            sql_ru += " ,cHENKOUSYA='" + cHENKOUSYA + "'";
            sql_ru += " ,cTOKUISAKI='" + cTOKUISAKI + "'";
            sql_ru += " ,sTOKUISAKI='" + sTOKUISAKI + "'";
            sql_ru += " ,sTOKUISAKI_TAN='" + sTOKUISAKI_TAN + "'";
            sql_ru += " ,sTOKUISAKI_TAN_Jun='" + sTOKUISAKI_TAN_Jun + "'";
            sql_ru += " ,sTOKUISAKI_TANBUMON='" + sTOKUISAKI_TANBUMON + "'";
            sql_ru += " ,sTOKUISAKI_YAKUSYOKU='" + sTOKUISAKI_YAKUSYOKU + "'";
            sql_ru += " ,sTOKUISAKI_KEISYO='" + sTOKUISAKI_KEISYO + "'";
            sql_ru += " ,cSEIKYUSAKI='" + cSEIKYUSAKI + "'";
            sql_ru += " ,sSEIKYUSAKI='" + sSEIKYUSAKI + "'";
            sql_ru += " ,sSEIKYU_TAN='" + sSEIKYU_TAN + "'";
            sql_ru += " ,sSEIKYU_TAN_Jun='" + sSEIKYU_TAN_Jun + "'";
            sql_ru += " ,sSEIKYU_TANBUMON='" + sSEIKYU_TANBUMON + "'";
            sql_ru += " ,sSEIKYU_YAKUSHOKU='" + sSEIKYU_YAKUSHOKU + "'";
            sql_ru += " ,sSEIKYU_KEISYO='" + sSEIKYU_KEISYO + "'";
            sql_ru += " ,nKINGAKU='" + nKINGAKU + "'";
            sql_ru += " ,nuriage_kingaku='" + nuriage_kingaku + "'";
            sql_ru += " ,nnebiki_kingaku='" + nnebiki_kingaku + "'";
            sql_ru += " ,nsyoukei_kingkau='" + nsyoukei_kingkau + "'";
            sql_ru += " ,nhenpin_kingkau='" + nhenpin_kingkau + "'";
            sql_ru += " ,ntatekae_kingaku='" + ntatekae_kingaku + "'";
            sql_ru += " ,nKAZEIKINGAKU='" + nKAZEIKINGAKU + "'";
            sql_ru += " ,nutihikazei='" + nutihikazei + "'";
            sql_ru += " ,sbikou='" + sbikou + "'";
            sql_ru += " ,cEIGYOTANTOSYA='" + cEIGYOTANTOSYA + "'";
            sql_ru += " ,fprint='0'";
            sql_ru += " ,sMemo='" + sMemo + "'";
            sql_ru += " ,cPJ='" + cPJ + "'";
            sql_ru += " ,nsyohizei='" + nsyohizei + "'";
            sql_ru += " Where cURIAGE='" + cURIAGE + "'";

            sql_ru += " ;";
            return sql_ru;
        }
        #endregion

        #region r_uriage_m 削除して新規登録
        private string SetData_r_uriage_m()
        {
            string sql_rum = "";
            sql_rum = @"DELETE FROM r_uriage_m WHERE cURIAGE='" + cURIAGE + "';";
            CheckData();  //データが無い最後行を登録しないようにチェック
            for (int rowi = 0; rowi <= data_end_row; rowi++)
            {
                int gno = rowi + 1;
                sql_rum += @"INSERT INTO";
                sql_rum += " r_uriage_m ";
                sql_rum += " (";
                sql_rum += " cURIAGE";
                sql_rum += " ,dHENKOU";
                sql_rum += " ,cHENKOUSYA";
                sql_rum += " ,nGYOUNO";
                sql_rum += " ,cSYOUHIN";
                sql_rum += " ,sSYOUHIN_R";
                sql_rum += " ,nSURYO";
                sql_rum += " ,sTANI";
                sql_rum += " ,nSIKIRITANKA";
                sql_rum += " ,nSIKIRIKINGAKU";
                sql_rum += " ,sbikou";
                sql_rum += " ,fKazei";
                sql_rum += " ,skubun";
                sql_rum += " )";

                sql_rum += " Values(";
                sql_rum += "'" + cURIAGE + "'";
                sql_rum += ",'" + dHENKOU + "'";
                sql_rum += ",'" + cHENKOUSYA + "'";
                sql_rum += ",'" + gno + "'";
                sql_rum += ",'" + dt_data.Rows[rowi]["cSYOUHIN"].ToString() + "'";
                sql_rum += ",'" + dt_data.Rows[rowi]["sSYOUHIN_R"].ToString().Replace("\\", "\\\\").Replace("'", "\\'") + "'";
                if (dt_data.Rows[rowi]["nSURYO"].ToString() != "")
                {
                    sql_rum += ",'" + Decimal.Parse(dt_data.Rows[rowi]["nSURYO"].ToString()) + "'";
                }
                else
                {
                    sql_rum += ",'0'";
                }
                sql_rum += ",'" + dt_data.Rows[rowi]["sTANI"].ToString().Replace("\\", "\\\\").Replace("'", "\\'") + "'";
                if (dt_data.Rows[rowi]["nSIKIRITANKA"].ToString() != "")
                {
                    sql_rum += ",'" + Decimal.Parse(dt_data.Rows[rowi]["nSIKIRITANKA"].ToString()) + "'";
                }
                else
                {
                    sql_rum += ",'0'";
                }
                if (dt_data.Rows[rowi]["nSIKIRIKINGAKU"].ToString() != "")
                {
                    sql_rum += ",'" + Decimal.Parse(dt_data.Rows[rowi]["nSIKIRIKINGAKU"].ToString()) + "'";
                }
                else
                {
                    sql_rum += ",'0'";
                }
                sql_rum += ",'" + dt_data.Rows[rowi]["sbikou"].ToString().Replace("\\", "\\\\").Replace("'", "\\'") + "'";
                if (dt_data.Rows[rowi]["fKazei"].ToString() == "課税")
                {
                    sql_rum += ",'0'";
                }
                else
                {
                    sql_rum += ",'1'";
                }
                sql_rum += ",'" + dt_data.Rows[rowi]["skubun"].ToString() + "'";
                sql_rum += " );";
            }
            return sql_rum;
        }
        #endregion

        #region　データが無い最後行を登録しないようにチェック
        private void CheckData()
        {
            data_end_row = -1;
            for (int rowi = dt_data.Rows.Count - 1; rowi >= 0; rowi--)
            {
                if (dt_data.Rows[rowi]["cSYOUHIN"].ToString() != "" ||
                    dt_data.Rows[rowi]["sSYOUHIN_R"].ToString() != "" ||
                    dt_data.Rows[rowi]["nSURYO"].ToString() != "" ||
                    dt_data.Rows[rowi]["sTANI"].ToString() != "")
                {
                    data_end_row = rowi;
                    return;
                }
            }
        }
        #endregion


        public DataTable UriageData()
        {
            string sql_select_ru = "";
            sql_select_ru = "select ifnull(date_format(ru.dURIAGE,'%Y/%m/%d'),'') as dURIAGE";   //売上日
            sql_select_ru += ",ifnull(rm.cMITUMORI,'') as cMITUMORI"; //見積コード
            //sql_select_ru += ",ifnull(rm.cPJ,'') as cPJ";//20220519 Chaw Added
            sql_select_ru += ",ifnull(ru.cPJ,'') as cPJ";//20220519 Chaw Added
            sql_select_ru += ",ifnull(date_format(ru.dNYUUKINYOTEI,'%Y/%m/%d'),'') as dNYUUKINYOTEI"; //入金予定日
            sql_select_ru += ",ifnull(date_format(ru.dDate,'%Y/%m/%d'),'') as dDate";　　//発行日
            sql_select_ru += ",ifnull(ru.snouhin,'') as snouhin";　//売上件名
            sql_select_ru += ",ifnull(ru.cSAKUSEISYA,'') as cSAKUSEISYA";　//作成者
            sql_select_ru += ",ifnull(mjt2.sTANTOUSHA,'') as sSAKUSEISYA";　//作成者
            sql_select_ru += ",ifnull(date_format(ru.dURIAGESAKUSEI,'%Y/%m/%d'),'') as dURIAGESAKUSEI";　//作成日
            sql_select_ru += ",case IFNULL(ru.cJYOTAI_Uriage,'') when 00 then '作成中'";　　//売上状態
            sql_select_ru += " when 01 then '作成済'";
            sql_select_ru += " when 02 then '請求締処理'";
            sql_select_ru += " when 03 then '入金'";
            sql_select_ru += " when '04' then '売掛締処理'";
            sql_select_ru += " else '' end As cJYOTAI_Uriage";
            sql_select_ru += ",ifnull(ru.cHENKOUSYA,'') as cHENKOUSYA";　//更新者
            sql_select_ru += ",ifnull(mjt3.sTANTOUSHA,'') as sHENKOUSYA";　//更新者
            sql_select_ru += ",ifnull(date_format(ru.dHENKOU,'%Y/%m/%d'),'') as dHENKOU";　//更新日
            sql_select_ru += ",ifnull(ru.cTOKUISAKI,'') as cTOKUISAKI";//得意先コード
            sql_select_ru += ",ifnull(ru.sTOKUISAKI,'') as sTOKUISAKI";//得意先名
            sql_select_ru += ",ifnull(ru.sTOKUISAKI_TAN,'') as sTOKUISAKI_TAN";//得意先担当
            sql_select_ru += ",ifnull(ru.sTOKUISAKI_TANBUMON,'') as sTOKUISAKI_TANBUMON";　//得意先担当部門
            sql_select_ru += ",ifnull(ru.sTOKUISAKI_TAN_JUN,'0') as sTOKUISAKI_TAN_JUN";//得意先担当順番
            sql_select_ru += ",ifnull(ru.sTOKUISAKI_YAKUSYOKU,'') as sTOKUISAKI_YAKUSYOKU";　//得意先担当役職
            sql_select_ru += ",ifnull(ru.sTOKUISAKI_KEISYO,'') as sTOKUISAKI_KEISYO";　　//得意先敬称
            sql_select_ru += ",ifnull(ru.cSEIKYUSAKI,'') as cSEIKYUSAKI";　//請求先
            sql_select_ru += ",ifnull(ru.sSEIKYUSAKI,'') as sSEIKYUSAKI";　//請求先名
            sql_select_ru += ",ifnull(ru.sSEIKYU_TAN,'') as sSEIKYU_TAN";　//請求先担当
            sql_select_ru += ",ifnull(ru.sSEIKYU_TAN_Jun,'') as sSEIKYU_TAN_Jun"; 　　//請求先担当順番
            sql_select_ru += ",ifnull(ru.sSEIKYU_TANBUMON,'') as sSEIKYU_TANBUMON";　　//請求先部門
            sql_select_ru += ",ifnull(ru.sSEIKYU_YAKUSHOKU,'') as sSEIKYU_YAKUSHOKU";　//請求先役職
            sql_select_ru += ",ifnull(ru.sSEIKYU_KEISYO,'') as sSEIKYU_KEISYO";// 請求先敬称
            sql_select_ru += ",ifnull(ru.nKINGAKU,0) as nKINGAKU";//合計金額
            sql_select_ru += ",ifnull(ru.nuriage_kingaku,0) as nuriage_kingaku";//合計売上
            sql_select_ru += ",ifnull(ru.nnebiki_kingaku,0) as nnebiki_kingaku";//値引
            sql_select_ru += ",ifnull(ru.nsyoukei_kingkau,0) as nsyoukei_kingkau";//小計
            sql_select_ru += ",ifnull(ru.nhenpin_kingkau,0) as nhenpin_kingkau";//返品
            sql_select_ru += ",ifnull(ru.ntatekae_kingaku,0) as ntatekae_kingaku";//立替
            sql_select_ru += ",ifnull(ru.nKAZEIKINGAKU,0) as nKAZEIKINGAKU";//課税合計
            sql_select_ru += ",ifnull(ru.nutihikazei,0) as nutihikazei";　//非課税合計
            sql_select_ru += ",ifnull(rm.nURIAGEKINGAKU,0) as nURIAGEKINGAKU";//受注金額
            sql_select_ru += ",IfNull(rm.cJYOTAI_MITUMORI, '') As cJYOTAI";//見積状態
            sql_select_ru += ",case IfNull(rm.cJYOTAI_MITUMORI, '')" +
                             " when '00' then '失注'" +
                             " when '01' then '見積提出済'" +
                             " when '02' then '受注'" +
                             " when '03' then '完了'" +
                             " when '04' then '見積作成中'" +
                             " when '05' then 'キャンセル'" +
                             " when '06' then '売上済み'" +
                             " else ''" +
                             " END As sJYOTAI";//見積状態
            sql_select_ru += ",ifnull(ru.nsyohizei,'0') as nsyohizei";　//消費税
            sql_select_ru += ",ifnull(ru.sbikou,'') as sbikou";//伝票備考
            sql_select_ru += ",ifnull(ru.cEIGYOTANTOSYA,'') as cEIGYOTANTOSYA";　//営業担当者
            sql_select_ru += ",ifnull(mjt.sTANTOUSHA,'') as sEIGYOTANTOSYA";　//営業担当者
            sql_select_ru += ",ifnull(ru.fprint,'0') as fprint";
            sql_select_ru += ",ifnull(ru.sMemo,'') as sMemo";//社内メモ
            sql_select_ru += ",ifnull(mji.cCO,'') as cCO"; // 20220119
            sql_select_ru += ",ifnull(mji.sKYOTEN,'') as sKYOTEN";//拠点
            sql_select_ru += " from r_uri_mitsu rum";
            sql_select_ru += " INNER JOIN r_uriage ru on rum.cURIAGE=ru.cURIAGE";
            sql_select_ru += " INNER JOIN r_mitumori rm ON rum.cMITUMORI=rm.cMITUMORI";
            sql_select_ru += " left join m_j_info mji on mji.cCO=rm.cKYOTEN";
            sql_select_ru += " left join m_j_tantousha  mjt on ru.cEIGYOTANTOSYA = mjt.cTANTOUSHA";
            sql_select_ru += " left join m_j_tantousha  mjt2 on ru.cSAKUSEISYA = mjt2.cTANTOUSHA";
            sql_select_ru += " left join m_j_tantousha  mjt3 on ru.cHENKOUSYA = mjt3.cTANTOUSHA";
            sql_select_ru += " left join r_bu_mitsu rbm on rum.cMITUMORI = rbm.cMITUMORI";
            sql_select_ru += " left join r_bukken buken on rbm.cBUKKEN = buken.cBUKKEN";
            sql_select_ru += " where ru.cURIAGE='" + cURIAGE + "'";
            DataTable dt_uriage_data = new DataTable();
            MySqlConnection cn_customer = new MySqlConnection("Server=" + DBUtilitycs.Server + "; Database=" + ConstantVal.DB_NAME + "; User Id=" + DBUtilitycs.user + "; password=" + DBUtilitycs.pass);
            try
            {
                cn_customer.Open();
                using (MySqlDataAdapter adap = new MySqlDataAdapter(sql_select_ru, cn_customer))
                {
                    adap.Fill(dt_uriage_data);
                }
                cn_customer.Close();
            }
            catch
            {
                cn_customer.Close();
            }

            return dt_uriage_data;
        }

        public DataTable Uriage_Meisai_Data()
        {
            string sql_select_ru_m = "";

            MySqlConnection cn_customer = new MySqlConnection("Server=" + DBUtilitycs.Server + "; Database=" + ConstantVal.DB_NAME + "; User Id=" + DBUtilitycs.user + "; password=" + DBUtilitycs.pass);
            DataTable dt_uriage_meisai_data = new DataTable();
            sql_select_ru_m = "select rum.cSYOUHIN as cSYOUHIN";
            //sql_select_ru_m += ",rum.nGYOUNO as nGYOUNO";
            sql_select_ru_m += ",ifnull(rum.sSYOUHIN_R,'') as sSYOUHIN_R";
            sql_select_ru_m += ",ifnull(rum.nSURYO,0) as nSURYO";
            sql_select_ru_m += ",ifnull(rum.sTANI,'') as sTANI";
            sql_select_ru_m += ",ifnull(rum.nSIKIRITANKA,'0') as nSIKIRITANKA";
            sql_select_ru_m += ",ifnull(rum.nSIKIRIKINGAKU,'0') as nSIKIRIKINGAKU";
            sql_select_ru_m += ",ifnull(rum.skubun,'売上') as skubun";
            sql_select_ru_m += ",ifnull(rum.sbikou,'') as sbikou";
            //sql_select_ru_m += ",'' as sshurui";
            //sql_select_ru_m += ",ifnull(rum.rowNO,'0') as rowNO"; 
            sql_select_ru_m += ",case IFNULL(rum.fKazei,'0') when 0 then '課税'";
            sql_select_ru_m += " else '非課税' end As fKazei";
            sql_select_ru_m += " from r_uriage_m as rum";
            sql_select_ru_m += " where rum.cURIAGE='" + cURIAGE + "'";

            cn_customer.Open();
            using (MySqlDataAdapter adap = new MySqlDataAdapter(sql_select_ru_m, cn_customer))
            {
                adap.Fill(dt_uriage_meisai_data);
            }
            cn_customer.Close();
            return dt_uriage_meisai_data;
        }

        #region 単位取る()
        public bool Get_Tani()
        {
            try
            {
                dt_tani = new DataTable();
                string sql_tanni = "";
                sql_tanni = " select ";
                sql_tanni += " mt.cTANI cTANI ";
                sql_tanni += ",ifnull(mt.sTANI,'') sTANI ";
                sql_tanni += " from m_tani mt";
                sql_tanni += " where mt.cTANI is not null and mt.cTANI !='' ";

                MySqlConnection cn = new MySqlConnection("Server=" + DBUtilitycs.Server + "; Database=" + ConstantVal.DB_NAME + "; User Id=" + DBUtilitycs.user + "; password=" + DBUtilitycs.pass);

                cn.Open();
                using (MySqlDataAdapter a1 = new MySqlDataAdapter(sql_tanni, cn))
                {
                    a1.Fill(dt_tani);
                }
                cn.Close();
                DataRow dr = dt_tani.NewRow();
                dr.ItemArray = new object[] { 00, "" };
                dt_tani.Rows.InsertAt(dr, 0);
            }
            catch
            {
                return false;

            }
            return true;
        }
        #endregion


        public bool Delete_Uriage(string uriagecode)
        {
            string del_sql = "";

            del_sql =@"DELETE FROM r_uriage WHERE cURIAGE = '"+ uriagecode + "';";
            del_sql += @"DELETE FROM r_uriage_m WHERE cURIAGE = '" + uriagecode + "';";
            del_sql += @"DELETE FROM r_uri_mitsu WHERE cURIAGE = '" + uriagecode + "';";

            MySqlConnection cn_customer = new MySqlConnection("Server=" + DBUtilitycs.Server + "; Database=" + ConstantVal.DB_NAME + "; User Id=" + DBUtilitycs.user + "; password=" + DBUtilitycs.pass);

            cn_customer.Open();
            MySqlTransaction tran = cn_customer.BeginTransaction();
            try
            {
                MySqlCommand c = new MySqlCommand(del_sql, cn_customer);
                c.ExecuteNonQuery();
                tran.Commit();
                cn_customer.Close();
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



        //#region Get_Title()
        //public DataTable Get_Title()
        //{
        //    DataTable dt_title = new DataTable();
        //    string sql_title = "";
        //    sql_title = "select ifnull(sIMAGETitle1,'') sIMAGETitle1,ifnull(sIMAGETitle2,'') sIMAGETitle2,ifnull(sIMAGETitle3,'') sIMAGETitle3,ifnull(sIMAGETitle4,'') sIMAGETitle4,ifnull(sIMAGETitle5,'') sIMAGETitle5";
        //    sql_title += ",ifnull(sBIKOUTitle1,'') sBIKOUTitle1,ifnull(sBIKOUTitle2,'') sBIKOUTitle2,ifnull(sBIKOUTitle3,'') sBIKOUTitle3,ifnull(sBIKOUTitle4,'') sBIKOUTitle4,ifnull(sBIKOUTitle5,'') sBIKOUTitle5";
        //    sql_title += " from m_j_info where cCO ='01'";
        //    MySqlConnection cn = new MySqlConnection("Server=" + DBUtilitycs.Server + "; Database=" + JC01Login_Class.DB + "; User Id=" + DBUtilitycs.user + "; password=" + DBUtilitycs.pass);

        //    cn.Open();
        //    using (MySqlDataAdapter a1 = new MySqlDataAdapter(sql_title, cn))
        //    {
        //        a1.Fill(dt_title);
        //    }
        //    cn.Close();
        //    return dt_title;
        //}


        //#endregion

        #region Get_Title()
        public DataTable Get_Title()
        {
            DataTable dt_title = new DataTable();
            string sql_title = "";
            sql_title = "select sIMAGETitle1,sIMAGETitle2,sIMAGETitle3,sIMAGETitle4,sIMAGETitle5";
            sql_title += ",sBIKOUTitle1,sBIKOUTitle2,sBIKOUTitle3,sBIKOUTitle4,sBIKOUTitle5";
            sql_title += " from m_j_info where cCO ='01'";
            MySqlConnection cn = new MySqlConnection("Server=" + DBUtilitycs.Server + "; Database=" + ConstantVal.DB_NAME + "; User Id=" + DBUtilitycs.user + "; password=" + DBUtilitycs.pass);

            cn.Open();
            using (MySqlDataAdapter a1 = new MySqlDataAdapter(sql_title, cn))
            {
                a1.Fill(dt_title);
            }
            cn.Close();
            return dt_title;
        }




        #endregion

        #region GetTantou()
        public DataTable GetTantou()
        {
            DataTable dt_tantou = new DataTable();
            string sql_gettantou = "select cTANTOUSHA,hiduke,rogo,Shoshiki,kingaku,seikyubikou from m_print_nouhinseikyu where cTANTOUSHA ='" + cTANTOUSHA + "';";
            MySqlConnection cn = new MySqlConnection("Server=" + DBUtilitycs.Server + "; Database=" + ConstantVal.DB_NAME + "; User Id=" + DBUtilitycs.user + "; password=" + DBUtilitycs.pass);
            cn.Open();
            using (MySqlDataAdapter a1 = new MySqlDataAdapter(sql_gettantou, cn))
            {
                a1.Fill(dt_tantou);
            }
            cn.Close();
            return dt_tantou;

        }
        
        #endregion

        #region UpdatePrint()
        public bool UpdatePrint()
        {
            string upd_sql = "";

            upd_sql = @"UPDATE m_print_nouhinseikyu";
            upd_sql += " SET";
            upd_sql += " hiduke='" + hiduke + "'";
            upd_sql += " ,rogo='" + rogo + "'";
            upd_sql += " ,Shoshiki='" + Shoshiki + "'";
            upd_sql += " ,kingaku='" + kingaku + "'";
            upd_sql += " ,seikyubikou='" + seikyubikou + "'";
            upd_sql += " ,cHENKOUSYA='" + cHENKOUSYA + "'";
            upd_sql += " ,dHENKOU='" + dHENKOU + "'";
            upd_sql += " WHERE cTANTOUSHA='" + cTANTOUSHA + "'";


            MySqlConnection cn_customer = new MySqlConnection("Server=" + DBUtilitycs.Server + "; Database=" + ConstantVal.DB_NAME + "; User Id=" + DBUtilitycs.user + "; password=" + DBUtilitycs.pass);

            cn_customer.Open();
            MySqlTransaction tran = cn_customer.BeginTransaction();
            try
            {
                MySqlCommand c = new MySqlCommand(upd_sql, cn_customer);
                c.ExecuteNonQuery();
                tran.Commit();
                cn_customer.Close();
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
        #endregion

        #region InsertPrint()
        public bool InsertPrint()
        {
            string insert_sql = "";

            insert_sql = @"INSERT INTO m_print_nouhinseikyu";
            insert_sql += " (";
            insert_sql += " cTANTOUSHA";
            insert_sql += " ,hiduke";
            insert_sql += " ,rogo";
            insert_sql += " ,Shoshiki";
            insert_sql += " ,kingaku";
            insert_sql += " ,seikyubikou";
            insert_sql += " ,cHENKOUSYA";
            insert_sql += " ,dHENKOU";
            insert_sql += " )";
            insert_sql += " Values(";
            insert_sql += "'" + cTANTOUSHA + "'";
            insert_sql += " ,'" + hiduke + "'";
            insert_sql += " ,'" + rogo + "'";
            insert_sql += " ,'" + Shoshiki + "'";
            insert_sql += " ,'" + kingaku + "'";
            insert_sql += " ,'" + seikyubikou + "'";
            insert_sql += " ,'" + cHENKOUSYA + "'";
            insert_sql += " ,'" + dHENKOU + "'";
            insert_sql += " )";

            MySqlConnection cn_customer = new MySqlConnection("Server=" + DBUtilitycs.Server + "; Database=" + ConstantVal.DB_NAME + "; User Id=" + DBUtilitycs.user + "; password=" + DBUtilitycs.pass);

            cn_customer.Open();
            MySqlTransaction tran = cn_customer.BeginTransaction();
            try
            {
                MySqlCommand c = new MySqlCommand(insert_sql, cn_customer);
                c.ExecuteNonQuery();
                tran.Commit();
                cn_customer.Close();
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
        #endregion

        public DataTable GetInoviceSeikyusho()
        {
            DataTable dt_title = new DataTable();
            string sql_mj = "";
            sql_mj += "SELECT ";
            sql_mj += " ifnull(mji.sINVOICE,'') AS sINVOICE";
            sql_mj += ",ifnull(mji.sSEIKYUUSHO,'') AS sSEIKYUUSHO";
            sql_mj += " FROM m_j_info mji";
            sql_mj += " WHERE 1=1 ";
            sql_mj += "AND mji.cCO='01'";
            MySqlConnection cn = new MySqlConnection("Server=" + DBUtilitycs.Server + "; Database=" + ConstantVal.DB_NAME + "; User Id=" + DBUtilitycs.user + "; password=" + DBUtilitycs.pass);

            cn.Open();
            using (MySqlDataAdapter a1 = new MySqlDataAdapter(sql_mj, cn))
            {
                a1.Fill(dt_title);
            }
            cn.Close();
            return dt_title;
        }

        public Double GetMitumoriKingaku(String cMitumori)
        {
            Double mitumorikingaku = 0;

            MySqlConnection cn_customer = new MySqlConnection("Server=" + DBUtilitycs.Server + "; Database=" + ConstantVal.DB_NAME + "; User Id=" + DBUtilitycs.user + "; password=" + DBUtilitycs.pass);
            DataTable dt_MitumoriKingaku = new DataTable();
            string selectgoukei_query = "select ";
            selectgoukei_query += " ifnull(rm.nURIAGEKINGAKU,0) as nURIAGEKINGAKU"; 
            selectgoukei_query += " from r_mitumori as rm";
            selectgoukei_query += " where rm.cMITUMORI='" + cMitumori + "'";

            cn_customer.Open();
            using (MySqlDataAdapter adap = new MySqlDataAdapter(selectgoukei_query, cn_customer))
            {
                adap.Fill(dt_MitumoriKingaku);
            }
            cn_customer.Close();
            if (dt_MitumoriKingaku.Rows.Count > 0)
            {
                string kingaku_goukei = dt_MitumoriKingaku.Rows[0]["nURIAGEKINGAKU"].ToString();
                if (kingaku_goukei != "")
                {
                    mitumorikingaku = Double.Parse(kingaku_goukei);
                }
            }
                return mitumorikingaku;
        }

    }
}
