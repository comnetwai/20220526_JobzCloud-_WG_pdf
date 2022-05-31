using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using System.Data;

namespace Service
{
    public class JC38Daibunrui_Class
    {
        MySqlConnection con = new MySqlConnection("Server=" + DBUtilitycs.Server + "; Database=" + DBUtilitycs.Database + "; User Id=" + DBUtilitycs.user + "; password=" + DBUtilitycs.pass);
        public string loginId { get; set; }
        public void ReadConn()
        {
            con = new MySqlConnection("Server=" + DBUtilitycs.Server + "; Database=" + ConstantVal.DB_NAME + "; User Id=" + DBUtilitycs.user + "; password=" + DBUtilitycs.pass);
        }

        public DataTable DaibunruiList(string daidata)
        {
            ReadConn();
            DataTable dt = new DataTable();
            using (MySqlDataAdapter adap = new MySqlDataAdapter(daidata, con))
            {
                adap.Fill(dt);
            }
            return dt;
        }
        public string cDaibunruiData(string daidata)
        {
            ReadConn();
            string cdaiVal = "";
            DataTable dt = new DataTable();
            using (MySqlDataAdapter adap = new MySqlDataAdapter(daidata, con))
            {
                adap.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr_group in dt.Rows)
                    {
                        cdaiVal = dr_group["cSYOUHIN_DAIGRP"].ToString();
                    }
                }
            }
            return cdaiVal;
        }

        public string sDaibunruiData(string daidata)
        {
            ReadConn();
            string sdaiVal = "";
            DataTable dt = new DataTable();
            using (MySqlDataAdapter adap = new MySqlDataAdapter(daidata, con))
            {
                adap.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr_group in dt.Rows)
                    {
                        sdaiVal = dr_group["sSYOUHIN_DAIGRP"].ToString();
                    }
                }
            }
            return sdaiVal;
        }

        public bool checkChuuInShouhin(string sql)
        {
            ReadConn();
            bool fexist = false;
            DataTable dt = new DataTable();
            using (MySqlDataAdapter adap = new MySqlDataAdapter(sql, con))
            {
                adap.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    fexist = true;
                }
            }
            return fexist;
        }
        public bool DaibunruiSaveList(string sqlStr)
        {
            ReadConn();
            int retval = 0;
            bool fret = false;
            MySqlCommand myCommand = new MySqlCommand(sqlStr, con);
            con.Open();
            retval = myCommand.ExecuteNonQuery();
            con.Close();
            if (retval == -1)
            {
                fret = false;
            }
            else
            {
                fret = true;
            }
            return fret;
        }
    }
}
