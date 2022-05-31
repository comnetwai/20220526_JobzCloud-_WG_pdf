using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data;
using MySql.Data.MySqlClient;
using Common;

namespace Service
{
     public   class JC25KyotenNyuuryoku_Class
    {
        //MySqlConnection con = new MySqlConnection("Server=" + DBUtilitycs.Server + "; Database=" + DBUtilitycs.Database + "; User Id=" + DBUtilitycs.user + "; password=" + DBUtilitycs.pass);
        MySqlConnection con = new MySqlConnection();
        public string loginId { get; set; }

        public void ReadConn()
        {            
            con = new MySqlConnection("Server=" + DBUtilitycs.Server + "; Database=" + ConstantVal.DB_NAME + "; User Id=" + DBUtilitycs.user + "; password=" + DBUtilitycs.pass);

        }
        public DataTable KyotenNyuuryokuTable(string sqlstring)
        {
            ReadConn();
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                // string sqlstring = " SELECT cCo,sKYOTEN FROM m_j_info; ";
                using (MySqlDataAdapter adap = new MySqlDataAdapter(sqlstring, con))
                {
                    adap.Fill(dt);
                }
                con.Close();
            }
            catch
            {
                con.Close();
            }
            return dt;
        }
        public bool KyotenNyuuryokuSql(string sqlStr, Byte[] imgbyte1, Byte[] imgbyte2, Byte[] imgbyte3, Byte[] imgbyte4, Byte[] imgbyte5)
        {
            ReadConn();
            int retval = 0;
            bool fret = false;
            try
            {
                MySqlCommand myCommand = new MySqlCommand(sqlStr, con);
                myCommand.Parameters.AddWithValue("@null", DBNull.Value);
                //ロゴ１
                if (imgbyte1 != null)
                {
                    myCommand.Parameters.AddWithValue("@image1", imgbyte1);
                }
                else
                {
                    myCommand.Parameters.AddWithValue("@image1", DBNull.Value);
                }

                //ロゴ２
                if (imgbyte2 != null)
                {
                    myCommand.Parameters.AddWithValue("@image2", imgbyte2);
                }
                else
                {
                    myCommand.Parameters.AddWithValue("@image2", DBNull.Value);
                }

                //ロゴ１
                if (imgbyte3 != null)
                {
                    myCommand.Parameters.AddWithValue("@image3", imgbyte3);
                }
                else
                {
                    myCommand.Parameters.AddWithValue("@image3", DBNull.Value);
                }

                //ロゴ１
                if (imgbyte4 != null)
                {
                    myCommand.Parameters.AddWithValue("@image4", imgbyte4);
                }
                else
                {
                    myCommand.Parameters.AddWithValue("@image4", DBNull.Value);
                }

                //ロゴ１
                if (imgbyte5 != null)
                {
                    myCommand.Parameters.AddWithValue("@image5", imgbyte5);
                }
                else
                {
                    myCommand.Parameters.AddWithValue("@image5", DBNull.Value);
                }

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
            }
            catch
            {
                con.Close();
            }
            return fret;
        }

        public DateTime FindCurTime()
        {
            ReadConn();
            DateTime curDateTime = new DateTime();
            DataTable dt = new DataTable();
            try
            {
                string datesql = " SELECT NOW() as nowdateTime;  ";
                using (MySqlDataAdapter adap = new MySqlDataAdapter(datesql, con))
                {
                    adap.Fill(dt);
                }
                if (dt.Rows.Count > 0)
                {
                    curDateTime = DateTime.Parse(dt.Rows[0]["nowdateTime"].ToString());
                }
                con.Close();
            }
            catch
            {
                con.Close();
            }
            return curDateTime;
        }

        public DataTable GetHonsyaKyotenLogo()
        {
            ReadConn();
            DataTable dt = new DataTable();
            try
            {
                string sql = "SELECT cCo ";
                sql += ", ifnull(sIMAGETitle1,'') as sIMAGETitle1 ";
                sql += ", ifnull(sIMAGETitle2,'') as sIMAGETitle2 ";
                sql += ", ifnull(sIMAGETitle3,'') as sIMAGETitle3 ";
                sql += ", ifnull(sIMAGETitle4,'') as sIMAGETitle4 ";
                sql += ", ifnull(sIMAGETitle5,'') as sIMAGETitle5 ";
                sql += " FROM m_j_info ";
                sql += " Where cCo ='01'; ";
                using (MySqlDataAdapter adap = new MySqlDataAdapter(sql, con))
                {
                    adap.Fill(dt);
                }
                con.Close();
            }
            catch
            {
                con.Close();
            }
            return dt;
        }
    }
}
