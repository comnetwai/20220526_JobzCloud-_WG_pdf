using Common;
using MySql.Data.MySqlClient;
using Service;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace JobzCloud.WebFront
{
    public partial class JC14TantouKensaku : System.Web.UI.Page
    {
        MySqlConnection con = null;
        List<string> list_ctantou = new List<string>();
        List<string> list_stantou = new List<string>();
        public bool show_msg { get; set; }
        public static string delete_id;
        public static string smail;
        public static bool is_logout = false;//20220119 Added エインドリ－・プ－プゥ

        #region Page_Load()
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(Request.QueryString["id"]))
                {
                    if (!this.IsPostBack)
                    {
                        if (SessionUtility.GetSession("HOME") != null)  //20211014 MiMi Added
                        {
                            hdnHome.Value = SessionUtility.GetSession("HOME").ToString();
                            SessionUtility.SetSession("HOME", null);
                        }

                        String id = Request.QueryString["id"];
                        DataTable dt_db = JC01Login_Class.Get_DB(id);
                        String db = dt_db.Rows[0]["db"].ToString();
                        String mail = dt_db.Rows[0]["mail"].ToString();
                        ConstantVal.DB_NAME = db;

                        JC_ClientConnecction_Class jc = new JC_ClientConnecction_Class();
                        jc.loginId = mail;

                        if (!jc.CheckKengen())
                        {
                            btn_add.Visible = false;
                        }

                        if (Session["isKensaku"] != null)
                        {
                            if (Session["isKensaku"].ToString() == "false")
                            {
                                load_data();
                                btnSettei.Visible = false;
                                HF_isKensaku.Value = "false";
                            }
                            else
                            {
                                HF_fTime.Value = "0";
                                load_data_MutiSelect();
                                btnSettei.Visible = true;
                                HF_isKensaku.Value = "true";
                                HF_fTime.Value = "1";
                            }
                            updBody.Update();
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "parentButtonClick('btnToLogin','" + hdnHome.Value + "');", true);
                        }

                    }
                    else
                    {
                        try
                        {
                            if (HF_isKensaku.Value == "false")
                            {

                                load_data();
                            }
                            else
                            {
                                load_data_MutiSelect();
                            }
                            updBody.Update();
                            //if (show_msg == true)
                            //    show_message("データがありません。");
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                    Session.Timeout = 480;
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "parentButtonClick('btnToLogin','" + hdnHome.Value + "');", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "parentButtonClick('btnToLogin','" + hdnHome.Value + "');", true);
            }
        }
        #endregion
         
        #region load_data_MutiSelect()
        protected void load_data_MutiSelect()
        {
            try
            {
                //bool show_msg = false;
                if (Request.QueryString["value"] != null)
                {
                    string code = Request.QueryString["value"];

                    String id = Request.QueryString["id"];
                    DataTable dt_db = JC01Login_Class.Get_DB(id);
                    String db = dt_db.Rows[0]["db"].ToString();
                    String mail = dt_db.Rows[0]["mail"].ToString();
                    ConstantVal.DB_NAME = db;

                    JC_ClientConnecction_Class jc = new JC_ClientConnecction_Class();
                    jc.loginId = mail;
                    con = jc.GetConnection();
                    con.Open();
                    string delete_tantousha_Query = "delete from m_j_tantousha where cTANTOUSHA='" + code + "';";
                    MySqlCommand cmd = new MySqlCommand(delete_tantousha_Query, con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    ScriptManager.RegisterStartupScript(this, this.GetType(),
                    "alert",
                    "alert('削除しました。');window.location ='../WebFront/JC14TantouKensaku.aspx';",
                     true);
                }
                else
                {
                    list_stantou.Clear();
                    list_ctantou.Clear();
                    tb.Rows.Clear();
                    DataTable dtSelect = new DataTable();
                    dtSelect.Columns.Add("code");
                    dtSelect.Columns.Add("name");
                    if (tb_code.Text == "")
                    {
                        bool has_value = false;

                        String id = Request.QueryString["id"];
                        DataTable dt_db = JC01Login_Class.Get_DB(id);
                        String db = dt_db.Rows[0]["db"].ToString();
                        String mail = dt_db.Rows[0]["mail"].ToString();
                        ConstantVal.DB_NAME = db;

                        JC_ClientConnecction_Class jc = new JC_ClientConnecction_Class();
                        jc.loginId = mail;
                        con = jc.GetConnection();
                        con.Open();
                        string q_taisya = chk_taisya.Checked == true ? " where fTAISYA=1" : " where fTAISYA=0 or fTAISYA is null";
                        string select_tantou_count = "select cTANTOUSHA,Replace(Replace(ifnull(sTANTOUSHA,''),'<','&lt'),'>','&gt') sTANTOUSHA from m_j_tantousha" + q_taisya;
                        MySqlCommand cmd = new MySqlCommand(select_tantou_count, con);
                        MySqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            has_value = true;
                            list_ctantou.Add(reader.GetValue(0).ToString());
                            list_stantou.Add(reader.GetValue(1).ToString());
                        }
                        reader.Close();
                        list_ctantou.Insert(0, "");
                        list_stantou.Insert(0, "選択なし");
                        if (has_value == true)
                        {
                            int c = 0;

                            string idx = HF_selectIndex.Value;
                            string[] idxarray = null;
                            if (!String.IsNullOrEmpty(idx))
                            {
                                idx = idx.Replace("'", "");

                                idxarray = idx.Split(',');
                                idxarray = idxarray.Where(x => !string.IsNullOrEmpty(x)).ToArray();
                            }
                            Boolean hasSelect = false;

                            for (int j = 0; j < (list_stantou.Count / 3) + 1; j++)
                            {
                                // Create a new row and add it to the Rows collection.
                                HtmlTableRow row = new HtmlTableRow();
                                // Iterate through the cells of a row.
                                for (int i = 0; i < 3; i++)
                                {
                                    if (c < list_stantou.Count)
                                    {
                                        Boolean isSelect = false;
                                        if (idxarray != null && list_ctantou[c].ToString() != "")
                                        {
                                            isSelect = Array.Exists(idxarray, x => x == list_ctantou[c].ToString());
                                        }
                                        else if((idxarray == null || idxarray.Length==0)  && list_ctantou[c].ToString() == "")
                                        {
                                            isSelect = true;
                                        }
                                        if (HF_fTime.Value == "0")
                                        {
                                            DataTable dt_Multiselect = new DataTable();
                                            dt_Multiselect= Session["dtTantou_MultiSelect"] as DataTable;
                                            isSelect=dt_Multiselect.AsEnumerable().Any(r => list_ctantou[c] == r.Field<String>("code"));
                                            if (isSelect)
                                            {
                                                String txt = HF_selectIndex.Value;
                                                if (txt != "")
                                                {
                                                    HF_selectIndex.Value = txt + ",'" + list_ctantou[c].ToString() + "'";
                                                }
                                                else
                                                {
                                                    HF_selectIndex.Value = "'"+ list_ctantou[c].ToString() + "'";
                                                }
                                            }
                                        }
                                        string str_cell = "";
                                        // Create a new cell and add it to the HtmlTableRow 
                                        // Cells collection.
                                        HtmlTableCell cell = new HtmlTableCell();
                                        if (btn_add.Visible)
                                        {
                                            cell.Attributes["Class"] = "dropdown";
                                        }
                                        cell.ID = list_ctantou[c];//20220114 Updated エインドリ－・プ－プゥ
                                        if (isSelect)
                                        {
                                            hasSelect = true;
                                            DataRow dr = dtSelect.NewRow();
                                            dr[0] = list_ctantou[c];
                                            dr[1] = list_stantou[c];
                                            dtSelect.Rows.Add(dr);
                                            //20220321 Updated font size エインドリ－
                                            str_cell += "<a href='#' id='" + list_ctantou[c] + "' runat='server' style='padding-left:2px;background:#4472c4;padding-top:8px;margin-right:10px;white-space: nowrap;overflow: hidden; text-overflow: ellipsis;display: inline-block;  height:40px;width:190px;font-size: 13px;text-decoration:none; color: white;font-family: 'Hiragino Sans', 'Open Sans', 'Meiryo', 'Hiragino Kaku Gothic Pro', 'メイリオ', 'MS ゴシック', 'sans - serif';' onclick='get_id(this,this.id)'>" + list_stantou[c] + "</a>";
                                        }
                                        else
                                        {
                                            //20220321 Updated font size エインドリ－
                                            str_cell += "<a href='#' id='" + list_ctantou[c] + "' runat='server' style='padding-left:2px;padding-top:8px;margin-right:10px;white-space: nowrap;overflow: hidden; text-overflow: ellipsis;display: inline-block;  height:40px;width:190px;font-size: 13px;text-decoration:none; color: black;font-family: 'Hiragino Sans', 'Open Sans', 'Meiryo', 'Hiragino Kaku Gothic Pro', 'メイリオ', 'MS ゴシック', 'sans - serif';' onclick='get_id(this,this.id)'>" + list_stantou[c] + "</a>";
                                        }
                                        if (c != 0)
                                        {
                                            str_cell += "<div style='float:right'><div class='dropdown-content'>";
                                            str_cell += "<table runat='server' style='margin-top:8px;'><tr id='trow'>";
                                            /*20220211 エインドリ－・プ－プゥ Update Image Icons Start*/
                                            str_cell += "<td><a href='#' id='" + list_ctantou[c] + "' style='font-size: 13px;margin-right:5px;' onclick='edit(this.id)'><i class='bi bi-pencil-fill'></i></a></td>";
                                            //str_cell += "<td><a href='#' id='" + list_ctantou[c] + "' style='font-size: 13px; ' onclick='edit(this.id)'><img src='../Images/draw.png' style='height:15px;width:15px;'/></a></td>";
                                            str_cell += "<td><a href='#' class='Delete' id='" + list_ctantou[c] + "' style='font-size: 13px;margin-right:5px;' onclick='return Confirm(this.id)'><i class='bi bi-trash-fill'></i></a></td>";
                                            //str_cell += "<td><a href='#' class='Delete' id='" + list_ctantou[c] + "' onclick='return Confirm(this.id)'><img src='../Images/trash.png' style='height:15px;width:15px;'/></i></a></td>";
                                            /*20220211 エインドリ－・プ－プゥ Update Image Icons End*/
                                            str_cell += "</tr></table>";
                                            str_cell += "</div></div>";
                                        }
                                        cell.InnerHtml = str_cell;
                                        row.Cells.Add(cell);
                                        row.Controls.Add(cell);
                                        tb.Controls.Add(row);
                                        c++;
                                    }
                                }
                                // Add the row to the HtmlTable Rows collection.
                                tb.Rows.Add(row);
                            }
                            lblDataNai.Visible = false;
                            if (!hasSelect)
                            {
                                HF_selectIndex.Value = "";
                                updBody.Update();
                            }
                        }
                        else
                        {
                            show_msg = true;
                            lblDataNai.Visible = true;
                        }
                        con.Close();
                    }
                    else
                    {
                        list_stantou.Clear();
                        list_ctantou.Clear();

                        String id = Request.QueryString["id"];
                        DataTable dt_db = JC01Login_Class.Get_DB(id);
                        String db = dt_db.Rows[0]["db"].ToString();
                        String mail = dt_db.Rows[0]["mail"].ToString();
                        ConstantVal.DB_NAME = db;

                        JC_ClientConnecction_Class jc = new JC_ClientConnecction_Class();
                        jc.loginId = mail;
                        con = jc.GetConnection();
                        con.Open();
                        string q_taisya = chk_taisya.Checked == true ? " where fTAISYA=1" : " where fTAISYA=0";

                        //Check Code or Name
                        //bool is_code = false;
                        //try
                        //{
                        //    string code = tb_code.Text.PadLeft(9,'0');
                        //    is_code = true;
                        //}
                        //catch (Exception e1)
                        //{
                        //    is_code = false;
                        //}
                        bool has_val = false;
                        //if (is_code == true)
                        //{
                        //Search By Code
                        String code = "";
                        try
                        {
                            code = Convert.ToDouble(tb_code.Text).ToString();
                        }
                        catch
                        {
                            code = "'" + tb_code.Text + "'";
                        }
                        string select_tantou_count = "select m.cTANTOUSHA,Replace(Replace(ifnull(m.sTANTOUSHA,''),'<','&lt'),'>','&gt') sTANTOUSHA from m_j_tantousha m LEFT JOIN m_bumon mb ON m.cBUMON=mb.cBUMON" + q_taisya + " and (m.cTANTOUSHA=" + code + " OR m.sTANTOUSHA COLLATE utf8_unicode_ci LIKE '%" + tb_code.Text + "%' OR mb.sBUMON COLLATE utf8_unicode_ci LIKE '%" + tb_code.Text + "%');";
                        MySqlCommand cmd = new MySqlCommand(select_tantou_count, con);
                        MySqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            list_ctantou.Add(reader.GetValue(0).ToString());
                            list_stantou.Add(reader.GetValue(1).ToString());
                            has_val = true;
                        }
                        reader.Close();
                        list_ctantou.Insert(0, "");
                        list_stantou.Insert(0, "選択なし");
                        if (has_val == true)
                        {
                            int c = 0;
                            string idx = HF_selectIndex.Value;
                            string[] idxarray = null;
                            if (!String.IsNullOrEmpty(idx))
                            {
                                idx = idx.Replace("'", "");

                                idxarray = idx.Split(',');
                                idxarray = idxarray.Where(x => !string.IsNullOrEmpty(x)).ToArray();
                            }
                            Boolean hasSelect = false;
                            for (int j = 0; j < (list_stantou.Count / 3) + 1; j++)
                            {
                                // Create a new row and add it to the Rows collection.
                                HtmlTableRow row = new HtmlTableRow();
                                // Iterate through the cells of a row.
                                for (int i = 0; i < 3; i++)
                                {
                                    if (c < list_stantou.Count)
                                    {
                                        Boolean isSelect = false;
                                        if (idxarray != null && list_ctantou[c].ToString() != "")
                                        {
                                            isSelect = Array.Exists(idxarray, x => x == list_ctantou[c].ToString());
                                        }
                                        else if ((idxarray == null || idxarray.Length == 0) && list_ctantou[c].ToString() == "")
                                        {
                                            isSelect = true;
                                        }
                                        if (HF_fTime.Value == "0")
                                        {
                                            DataTable dt_Multiselect = new DataTable();
                                            dt_Multiselect = Session["dtTantou_MultiSelect"] as DataTable;
                                            isSelect = dt_Multiselect.AsEnumerable().Any(r => list_ctantou[c] == r.Field<String>("code"));
                                            if (isSelect)
                                            {
                                                String txt = HF_selectIndex.Value;
                                                if (txt != "")
                                                {
                                                    HF_selectIndex.Value = txt + ",'" + list_ctantou[c].ToString() + "'";
                                                }
                                                else
                                                {
                                                    HF_selectIndex.Value = "'" + list_ctantou[c].ToString() + "'";
                                                }
                                            }
                                        }
                                        string str_cell = "";
                                        // Create a new cell and add it to the HtmlTableRow 
                                        // Cells collection.
                                        HtmlTableCell cell = new HtmlTableCell();
                                        if (btn_add.Visible)
                                        {
                                            cell.Attributes["Class"] = "dropdown";
                                        }
                                        cell.ID = list_ctantou[c]; //20220114 Updated エインドリ－・プ－プゥ
                                        if (isSelect)
                                        {
                                            hasSelect = true;
                                            DataRow dr = dtSelect.NewRow();
                                            dr[0] = list_ctantou[c];
                                            dr[1] = list_stantou[c];
                                            dtSelect.Rows.Add(dr);
                                            //20220321 Updated font size エインドリ－
                                            str_cell += "<a href='#' id='" + list_ctantou[c] + "' runat='server' style='padding-left:2px;background:#4472c4;padding-top:8px;margin-right:10px;white-space: nowrap;overflow: hidden; text-overflow: ellipsis;display: inline-block;  height:40px;width:180px;font-size: 13px;text-decoration:none; color: white;font-family: 'Hiragino Sans', 'Open Sans', 'Meiryo', 'Hiragino Kaku Gothic Pro', 'メイリオ', 'MS ゴシック', 'sans - serif';' onclick='get_id(this,this.id)'>" + list_stantou[c] + "</a>";
                                        }
                                        else
                                        {
                                            //20220321 Updated font size エインドリ－
                                            str_cell += "<a href='#' id='" + list_ctantou[c] + "' runat='server' style='padding-left:2px;padding-top:8px;margin-right:10px;white-space: nowrap;overflow: hidden; text-overflow: ellipsis;display: inline-block;  height:40px;width:190px;font-size: 13px;text-decoration:none; color: black;font-family: 'Hiragino Sans', 'Open Sans', 'Meiryo', 'Hiragino Kaku Gothic Pro', 'メイリオ', 'MS ゴシック', 'sans - serif';' onclick='get_id(this,this.id)'>" + list_stantou[c] + "</a>";
                                        }
                                        if (c != 0)
                                        {
                                            str_cell += "<div style='float:right'><div class='dropdown-content'>";
                                            str_cell += "<table runat='server' style='margin-top:8px;'><tr id='trow'>";
                                            /*20220211 エインドリ－・プ－プゥ Update Image Icons End*/
                                            str_cell += "<td><a href='#' id='" + list_ctantou[c] + "' style='font-size: 13px;margin-right:5px;' onclick='edit(this.id)'><i class='bi bi-pencil-fill'></i></a></td>";
                                            //str_cell += "<td><a href='#' id='" + list_ctantou[c] + "' style='font-size: 13px; ' onclick='edit(this.id)'><img src='../Images/draw.png' style='height:15px;width:15px;'/></a></td>";
                                            str_cell += "<td><a href='#' class='Delete' id='" + list_ctantou[c] + "' style='font-size: 13px;margin-right:5px;' onclick='return Confirm(this.id)'><i class='bi bi-trash-fill'></i></a></td>";
                                            //str_cell += "<td><a href='#' class='Delete' id='" + list_ctantou[c] + "' onclick='return Confirm(this.id)'><img src='../Images/trash.png' style='height:15px;width:15px;'/></i></a></td>";
                                            /*20220211 エインドリ－・プ－プゥ Update Image Icons End*/
                                            str_cell += "</tr></table>";
                                            str_cell += "</div></div>";
                                        }
                                        cell.InnerHtml = str_cell;
                                        row.Cells.Add(cell);
                                        row.Controls.Add(cell);
                                        tb.Controls.Add(row);
                                        c++;
                                    }
                                }
                                // Add the row to the HtmlTable Rows collection.
                                tb.Rows.Add(row);
                            }
                            lblDataNai.Visible = false;
                            if (!hasSelect)
                            {
                                HF_selectIndex.Value = "";
                                updBody.Update();
                            }
                        }
                        else
                        {
                            show_msg = true;
                            lblDataNai.Visible = true;
                        }
                        #region toDelete
                        //}
                        //else
                        //{
                        //    //Search By Name or Bumon
                        //    string ftaisya = chk_taisya.Checked == true ? " fTAISYA=1" : " fTAISYA=0";
                        //    string select_tantou_count = "SELECT m.cTANTOUSHA,m.sTANTOUSHA FROM m_j_tantousha m LEFT JOIN m_bumon mb ON m.cBUMON=mb.cBUMON where "+ftaisya+" and sTANTOUSHA COLLATE utf8_unicode_ci LIKE '%" + tb_code.Text + "%' OR mb.sBUMON COLLATE utf8_unicode_ci LIKE '%" + tb_code.Text + "%' ";
                        //    MySqlCommand cmd = new MySqlCommand(select_tantou_count, con);
                        //    MySqlDataReader reader = cmd.ExecuteReader();
                        //    while (reader.Read())
                        //    {
                        //        has_val = true;
                        //        list_ctantou.Add(reader.GetValue(0).ToString());
                        //        list_stantou.Add(reader.GetValue(1).ToString());
                        //    }
                        //    reader.Close();
                        //    list_ctantou.Insert(0, "");
                        //    list_stantou.Insert(0, "選択なし");
                        //    if (has_val == true)
                        //    {
                        //        int c = 0;
                        //        string idx = HF_selectIndex.Value;
                        //        string[] idxarray = null;
                        //        if (!String.IsNullOrEmpty(idx))
                        //        {
                        //            idx = idx.Replace("'", "");

                        //            idxarray = idx.Split(',');
                        //        }
                        //        for (int j = 0; j < (list_stantou.Count / 3) + 1; j++)
                        //        {
                        //            // Create a new row and add it to the Rows collection.
                        //            HtmlTableRow row = new HtmlTableRow();
                        //            // Iterate through the cells of a row.
                        //            for (int i = 0; i < 3; i++)
                        //            {
                        //                if (c < list_stantou.Count)
                        //                {
                        //                    Boolean isSelect = false;
                        //                    if (idxarray != null)
                        //                    {
                        //                        isSelect = Array.Exists(idxarray, x => x == c.ToString());
                        //                    }
                        //                    if (HF_fTime.Value == "0")
                        //                    {
                        //                        DataTable dt_Multiselect = new DataTable();
                        //                        dt_Multiselect = Session["dtTantou_MultiSelect"] as DataTable;
                        //                        isSelect = dt_Multiselect.AsEnumerable().Any(r => list_ctantou[c] == r.Field<String>("code"));
                        //                        if (isSelect)
                        //                        {
                        //                            String txt = HF_selectIndex.Value;
                        //                            if (txt != "")
                        //                            {
                        //                                HF_selectIndex.Value = txt + ",'" + c.ToString() + "'";
                        //                            }
                        //                            else
                        //                            {
                        //                                HF_selectIndex.Value = "'" + c.ToString() + "'";
                        //                            }
                        //                        }
                        //                    }
                        //                    string str_cell = "";
                        //                    // Create a new cell and add it to the HtmlTableRow 
                        //                    // Cells collection.
                        //                    HtmlTableCell cell = new HtmlTableCell();
                        //                    if (btn_add.Visible)
                        //                    {
                        //                        cell.Attributes["Class"] = "dropdown";
                        //                    }
                        //                    cell.ID = list_stantou[c];
                        //                    if (isSelect)
                        //                    {
                        //                        DataRow dr = dtSelect.NewRow();
                        //                        dr[0] = list_ctantou[c];
                        //                        dr[1] = list_stantou[c];
                        //                        dtSelect.Rows.Add(dr);
                        //                        str_cell += "<a href='#' id='" + list_ctantou[c] + "' runat='server' style='padding-left:2px;background:#4472c4;padding-top:8px;margin-right:10px;white-space: nowrap;overflow: hidden; text-overflow: ellipsis;display: inline-block;  height:40px;width:190px;font-size: 14px;text-decoration:none; color: white;font-family: 'Hiragino Sans', 'Open Sans', 'Meiryo', 'Hiragino Kaku Gothic Pro', 'メイリオ', 'MS ゴシック', 'sans - serif';' onclick='get_id(this," + c + ")'>" + list_stantou[c] + "</a>";
                        //                    }
                        //                    else
                        //                    {
                        //                        str_cell += "<a href='#' id='" + list_ctantou[c] + "' runat='server' style='padding-left:2px;padding-top:8px;margin-right:10px;white-space: nowrap;overflow: hidden; text-overflow: ellipsis;display: inline-block;  height:40px;width:190px;font-size: 14px;text-decoration:none; color: black;font-family: 'Hiragino Sans', 'Open Sans', 'Meiryo', 'Hiragino Kaku Gothic Pro', 'メイリオ', 'MS ゴシック', 'sans - serif';' onclick='get_id(this," + c + ")'>" + list_stantou[c] + "</a>";
                        //                    }
                        //                    if (c != 0)
                        //                    {
                        //                        str_cell += "<div style='float:right'><div class='dropdown-content'>";
                        //                        str_cell += "<table runat='server'><tr id='trow'>";
                        //                        str_cell += "<td><a href='#' id='" + list_ctantou[c] + "' style='font-size: 13px; ' onclick='edit(this.id)'><img src='../Images/draw.png' style='height:15px;width:15px;'/></a></td>";
                        //                        str_cell += "<td><a href='#' class='Delete' id='" + list_ctantou[c] + "' onclick='return Confirm(this.id)'><img src='../Images/trash.png' style='height:15px;width:15px;'/></i></a></td>";
                        //                        str_cell += "</tr></table>";
                        //                        str_cell += "</div></div>";
                        //                    }
                        //                    cell.InnerHtml = str_cell;
                        //                    row.Cells.Add(cell);
                        //                    row.Controls.Add(cell);
                        //                    tb.Controls.Add(row);
                        //                    c++;
                        //                }
                        //            }
                        //            // Add the row to the HtmlTable Rows collection.
                        //            tb.Rows.Add(row);
                        //        }

                        //    }
                        //    else
                        //    {
                        //        show_msg = true;
                        //    }
                        //}
                        #endregion
                        con.Close();
                    }

                    Session["dtTantou_MultiSelect"] = dtSelect;
                }
                
            }
            catch (Exception ex)
            {
                show_message("データベースに接続できません。管理者にお問合せ下さい。");
            }
        }
        #endregion

        #region load_data()
        protected void load_data()
        {
            try
            {
                //bool show_msg = false;
                if (Request.QueryString["value"] != null)
                {
                    string code = Request.QueryString["value"];

                    String id = Request.QueryString["id"];
                    DataTable dt_db = JC01Login_Class.Get_DB(id);
                    String db = dt_db.Rows[0]["db"].ToString();
                    String mail = dt_db.Rows[0]["mail"].ToString();
                    ConstantVal.DB_NAME = db;

                    JC_ClientConnecction_Class jc = new JC_ClientConnecction_Class();
                    jc.loginId = mail;
                    con = jc.GetConnection();
                    con.Open();
                    string delete_tantousha_Query = "delete from m_j_tantousha where cTANTOUSHA='" + code + "';";
                    MySqlCommand cmd = new MySqlCommand(delete_tantousha_Query, con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    //ScriptManager.RegisterStartupScript(this, this.GetType(),
                    //"alert",
                    //"alert('削除しました。');window.location ='../WebFront/JC14TantouKensaku.aspx';",
                    // true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowErrorMessage",
                               "ShowErrorMessage('削除しました。');", true);
                }
                else
                {
                    list_stantou.Clear();
                    list_ctantou.Clear();
                    tb.Rows.Clear();
                    if (tb_code.Text == "")
                    {
                        bool has_value = false;

                        String id = Request.QueryString["id"];
                        DataTable dt_db = JC01Login_Class.Get_DB(id);
                        String db = dt_db.Rows[0]["db"].ToString();
                        String mail = dt_db.Rows[0]["mail"].ToString();
                        ConstantVal.DB_NAME = db;

                        JC_ClientConnecction_Class jc = new JC_ClientConnecction_Class();
                        jc.loginId = mail;
                        con = jc.GetConnection();
                        con.Open();
                        string q_taisya = chk_taisya.Checked == true ? " where fTAISYA=1" : " where fTAISYA=0 or fTAISYA is null";
                        string select_tantou_count = "select cTANTOUSHA,Replace(Replace(ifnull(sTANTOUSHA,''),'<','&lt'),'>','&gt') sTANTOUSHA from m_j_tantousha" + q_taisya;
                        MySqlCommand cmd = new MySqlCommand(select_tantou_count, con);
                        MySqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            has_value = true;
                            list_ctantou.Add(reader.GetValue(0).ToString());
                            list_stantou.Add(reader.GetValue(1).ToString());
                        }
                        reader.Close();
                        list_ctantou.Insert(0, "");
                        list_stantou.Insert(0, "選択なし");
                        if (has_value == true)
                        {
                            int c = 0;
                            for (int j = 0; j < (list_stantou.Count / 3) + 1; j++)
                            {
                                // Create a new row and add it to the Rows collection.
                                HtmlTableRow row = new HtmlTableRow();
                                // Iterate through the cells of a row.
                                for (int i = 0; i < 3; i++)
                                {
                                    if (c < list_stantou.Count)
                                    {
                                        string str_cell = "";
                                        // Create a new cell and add it to the HtmlTableRow 
                                        // Cells collection.
                                        HtmlTableCell cell = new HtmlTableCell();
                                        if (btn_add.Visible)
                                        {
                                            cell.Attributes["Class"] = "dropdown";
                                        }
                                        cell.ID = list_ctantou[c];//20220114 Updated エインドリ－・プ－プゥ
                                        //20220321 Updated font size エインドリ－
                                        str_cell += "<a href='#' id='" + list_ctantou[c] + "' runat='server' style='padding-top:8px;margin-right:10px;white-space: nowrap;overflow: hidden; text-overflow: ellipsis;display: inline-block;  height:40px;max-width:170px;min-width:170px;font-size: 13px;text-decoration:none; color: black;font-family: 'Hiragino Sans', 'Open Sans', 'Meiryo', 'Hiragino Kaku Gothic Pro', 'メイリオ', 'MS ゴシック', 'sans - serif';' onclick='get_id1(this.id,this.innerText)'>" + list_stantou[c] + "</a>";
                                        if (c != 0)
                                        {
                                            str_cell += "<div style='float:right'><div class='dropdown-content' style='background-color:transparent !important;'>";
                                            str_cell += "<table runat='server' style='background-color:transparent !important;margin-top:8px;'><tr id='trow'>";
                                            /*20220211 エインドリ－・プ－プゥ Update Image Icons Start*/
                                            str_cell += "<td><a href='#' id='" + list_ctantou[c] + "' style='font-size: 13px;background-color:transparent !important;margin-right:5px;' onclick='edit(this.id)'> <i class='bi bi-pencil-fill'></i></a></td>";
                                            //str_cell += "<td><a href='#' id='" + list_ctantou[c] + "' style='font-size: 13px;background-color:transparent !important; ' onclick='edit(this.id)'><img src='../Images/draw.png' style='height:14px;width:14px;background-color:transparent !important;'/></a></td>";
                                            str_cell += "<td><a href='#' class='Delete' id='" + list_ctantou[c] + "' style='font-size:13px;background-color:transparent !important;margin-right:5px;' onclick='return Confirm(this.id)'><i class='bi bi-trash-fill'></i></a></td>";
                                            //str_cell += "<td><a href='#' class='Delete' id='" + list_ctantou[c] + "' style='background-color:transparent !important; '  onclick='return Confirm(this.id)'><img src='../Images/trash.png' style='height:14px;width:14px;background-color:transparent !important;'/></i></a></td>";
                                            /*20220211 エインドリ－・プ－プゥ Update Image Icons End*/
                                            str_cell += "</tr></table>";
                                            str_cell += "</div></div>";
                                        }
                                        cell.InnerHtml = str_cell;
                                        row.Cells.Add(cell);
                                        row.Controls.Add(cell);
                                        tb.Controls.Add(row);
                                        c++;
                                    }
                                }
                                // Add the row to the HtmlTable Rows collection.
                                tb.Rows.Add(row);
                            }
                            lblDataNai.Visible = false;
                        }
                        else
                        {
                            show_msg = true;
                            lblDataNai.Visible = true;
                        }
                        con.Close();
                    }
                    else
                    {
                        list_stantou.Clear();
                        list_ctantou.Clear();

                        String id = Request.QueryString["id"];
                        DataTable dt_db = JC01Login_Class.Get_DB(id);
                        String db = dt_db.Rows[0]["db"].ToString();
                        String mail = dt_db.Rows[0]["mail"].ToString();
                        ConstantVal.DB_NAME = db;

                        JC_ClientConnecction_Class jc = new JC_ClientConnecction_Class();
                        jc.loginId = mail;
                        con = jc.GetConnection();
                        con.Open();
                        string q_taisya = chk_taisya.Checked == true ? " where fTAISYA=1" : " where fTAISYA=0";

                        //Check Code or Name
                        //bool is_code = false;
                        //try
                        //{
                        //    string code = tb_code.Text.PadLeft(9,'0');
                        //    is_code = true;
                        //}
                        //catch (Exception e1)
                        //{
                        //    is_code = false;
                        //}
                        bool has_val = false;
                        //if (is_code == true)
                        //{
                        //Search By Code
                        String code = "";
                        try
                        {
                            code = Convert.ToDouble(tb_code.Text).ToString();
                        }
                        catch
                        {
                            code = "'" + tb_code.Text + "'";
                        }
                            string select_tantou_count = "select m.cTANTOUSHA,Replace(Replace(ifnull(m.sTANTOUSHA,''),'<','&lt'),'>','&gt') sTANTOUSHA from m_j_tantousha m LEFT JOIN m_bumon mb ON m.cBUMON=mb.cBUMON" + q_taisya + " and (m.cTANTOUSHA=" + code + " OR m.sTANTOUSHA COLLATE utf8_unicode_ci LIKE '%" + tb_code.Text + "%' OR mb.sBUMON COLLATE utf8_unicode_ci LIKE '%" + tb_code.Text + "%');";
                            MySqlCommand cmd = new MySqlCommand(select_tantou_count, con);
                            MySqlDataReader reader = cmd.ExecuteReader();
                            while (reader.Read())
                            {
                                list_ctantou.Add(reader.GetValue(0).ToString());
                                list_stantou.Add(reader.GetValue(1).ToString());
                                has_val = true;
                            }
                            reader.Close();
                            list_ctantou.Insert(0, "");
                            list_stantou.Insert(0, "選択なし");
                            if (has_val == true)
                            {
                                int c = 0;
                                for (int j = 0; j < (list_stantou.Count / 3) + 1; j++)
                                {
                                    // Create a new row and add it to the Rows collection.
                                    HtmlTableRow row = new HtmlTableRow();
                                    // Iterate through the cells of a row.
                                    for (int i = 0; i < 3; i++)
                                    {
                                        if (c < list_stantou.Count)
                                        {
                                            string str_cell = "";
                                            // Create a new cell and add it to the HtmlTableRow 
                                            // Cells collection.
                                            HtmlTableCell cell = new HtmlTableCell();
                                            if (btn_add.Visible)
                                            {
                                                cell.Attributes["Class"] = "dropdown";
                                            }
                                            cell.ID = list_ctantou[c];//20220114 Updated エインドリ－・プ－プゥ
                                        //20220321 Updated font size エインドリ－
                                        str_cell += "<a href='#' id='" + list_ctantou[c] + "' runat='server' style='padding-top:8px;margin-right:10px;white-space: nowrap;overflow: hidden; text-overflow: ellipsis;display: inline-block;  height:40px;max-width:170px;min-width:170px;font-size: 13px;text-decoration:none; color: black;font-family: 'Hiragino Sans', 'Open Sans', 'Meiryo', 'Hiragino Kaku Gothic Pro', 'メイリオ', 'MS ゴシック', 'sans - serif';' onclick='get_id1(this.id,this.innerText)'>" + list_stantou[c] + "</a>";
                                            if (c != 0)
                                            {
                                                str_cell += "<div style='float:right'><div class='dropdown-content' style='background-color:transparent !important;'>";
                                                str_cell += "<table runat='server' style='background-color:transparent !important;margin-top:8px;'><tr id='trow'>";
                                            /*20220211 エインドリ－・プ－プゥ Update Image Icons Start*/
                                            str_cell += "<td><a href='#' id='" + list_ctantou[c] + "' style='font-size: 13px; margin-right:5px;background-color:transparent !important; ' onclick='edit(this.id)'><i class='bi bi-pencil-fill'></i></a></td>";
                                                //str_cell += "<td><a href='#' id='" + list_ctantou[c] + "' style='font-size: 13px; background-color:transparent !important; ' onclick='edit(this.id)'><img src='../Images/draw.png' style='height:14px;width:14px;background-color:transparent !important;'/></a></td>";
                                                str_cell += "<td><a href='#' class='Delete' id='" + list_ctantou[c] + "' style='font-size: 13px; margin-right:5px;background-color:transparent !important; ' onclick='return Confirm(this.id)'><i class='bi bi-trash-fill'></i></a></td>";
                                            //str_cell += "<td><a href='#' class='Delete' id='" + list_ctantou[c] + "' style='background-color:transparent !important; ' onclick='return Confirm(this.id)'><img src='../Images/trash.png' style='height:14px;width:14px;background-color:transparent !important;'/></i></a></td>";
                                            /*20220211 エインドリ－・プ－プゥ Update Image Icons End*/
                                            str_cell += "</tr></table>";
                                                str_cell += "</div></div>";
                                            }
                                            cell.InnerHtml = str_cell;
                                            row.Cells.Add(cell);
                                            row.Controls.Add(cell);
                                            tb.Controls.Add(row);
                                            c++;
                                        }
                                    }
                                    // Add the row to the HtmlTable Rows collection.
                                    tb.Rows.Add(row);
                                }
                                lblDataNai.Visible = false;
                            }
                            else
                            {
                                show_msg = true;
                                lblDataNai.Visible = true;
                            }
                        #region toDelete
                        //}
                        //else
                        //{
                        //    //Search By Name or Bumon
                        //    string ftaisya = chk_taisya.Checked == true ? " fTAISYA=1" : " fTAISYA=0";
                        //    string select_tantou_count = "SELECT m.cTANTOUSHA,m.sTANTOUSHA FROM m_j_tantousha m LEFT JOIN m_bumon mb ON m.cBUMON=mb.cBUMON where " + ftaisya + " and sTANTOUSHA COLLATE utf8_unicode_ci LIKE '%" + tb_code.Text + "%' OR mb.sBUMON COLLATE utf8_unicode_ci LIKE '%" + tb_code.Text + "%' ";
                        //    MySqlCommand cmd = new MySqlCommand(select_tantou_count, con);
                        //    MySqlDataReader reader = cmd.ExecuteReader();
                        //    while (reader.Read())
                        //    {
                        //        has_val = true;
                        //        list_ctantou.Add(reader.GetValue(0).ToString());
                        //        list_stantou.Add(reader.GetValue(1).ToString());
                        //    }
                        //    reader.Close();
                        //    list_ctantou.Insert(0, "");
                        //    list_stantou.Insert(0, "選択なし");
                        //    if (has_val == true)
                        //    {
                        //        int c = 0;
                        //        for (int j = 0; j < (list_stantou.Count / 3) + 1; j++)
                        //        {
                        //            // Create a new row and add it to the Rows collection.
                        //            HtmlTableRow row = new HtmlTableRow();
                        //            // Iterate through the cells of a row.
                        //            for (int i = 0; i < 3; i++)
                        //            {
                        //                if (c < list_stantou.Count)
                        //                {
                        //                    string str_cell = "";
                        //                    // Create a new cell and add it to the HtmlTableRow 
                        //                    // Cells collection.
                        //                    HtmlTableCell cell = new HtmlTableCell();
                        //                    if (btn_add.Visible)
                        //                    {
                        //                        cell.Attributes["Class"] = "dropdown";
                        //                    }
                        //                    cell.ID = list_stantou[c];
                        //                    str_cell += "<a href='#' id='" + list_ctantou[c] + "' runat='server' style='padding-top:8px;margin-right:10px;white-space: nowrap;overflow: hidden; text-overflow: ellipsis;display: inline-block;  height:40px;width:190px;font-size: 14px;text-decoration:none; color: black;font-family: 'Hiragino Sans', 'Open Sans', 'Meiryo', 'Hiragino Kaku Gothic Pro', 'メイリオ', 'MS ゴシック', 'sans - serif';' onclick='get_id1(this.id,this.innerText)'>" + list_stantou[c] + "</a>";
                        //                    if (c != 0)
                        //                    {
                        //                        str_cell += "<div style='float:right'><div class='dropdown-content'>";
                        //                        str_cell += "<table runat='server'><tr id='trow'>";
                        //                        str_cell += "<td><a href='#' id='" + list_ctantou[c] + "' style='font-size: 13px; ' onclick='edit(this.id)'><img src='../Images/draw.png' style='height:15px;width:15px;'/></a></td>";
                        //                        str_cell += "<td><a href='#' class='Delete' id='" + list_ctantou[c] + "' onclick='return Confirm(this.id)'><img src='../Images/trash.png' style='height:15px;width:15px;'/></i></a></td>";
                        //                        str_cell += "</tr></table>";
                        //                        str_cell += "</div></div>";
                        //                    }
                        //                    cell.InnerHtml = str_cell;
                        //                    row.Cells.Add(cell);
                        //                    row.Controls.Add(cell);
                        //                    tb.Controls.Add(row);
                        //                    c++;
                        //                }
                        //            }
                        //            // Add the row to the HtmlTable Rows collection.
                        //            tb.Rows.Add(row);
                        //        }

                        //    }
                        //    else
                        //    {
                        //        show_msg = true;
                        //    }
                        //}
                        #endregion
                        con.Close();
                    }
                }

            }
            catch (Exception ex)
            {
                show_message("データベースに接続できません。管理者にお問合せ下さい。");
            }
        }
        #endregion

        #region show_message()
        public void show_message(string msg)
        {
            Response.Write("<script>alert('" + msg + "')</script>");
        }
        #endregion

        #region tb_search_textChanged() 
        protected void tb_search_textChanged(object sender,EventArgs e)
        {
            //if (HF_isKensaku.Value == "false")
            //{
            //    load_data();
            //}
            //else
            //{
            //    load_data_MutiSelect();
            //}
        }
        #endregion

        #region btn_add_Clicked()
        protected void btn_add_Clicked(object sender,EventArgs e)
        {
            //Response.Redirect("../WebFront/JC15TantouTouroku.aspx");
            //if (lbl_title.Text == "担当者を選択")
            //{
                Session["isTantouOrUser"] = "0"; //担当者
                Session["cTantousha"] = null;
            //ifSentakuPopup.Style["width"] = "715px";
            ifSentakuPopup.Style["width"] = "100vw";
            ifSentakuPopup.Style["height"] = "100vh";
            //}
            //else
            //{
            //    ifSentakuPopup.Style["width"] = "715px";
            //    ifSentakuPopup.Style["height"] = "100vh";
            //}
            SessionUtility.SetSession("HOME", "Popup");

            String id = Request.QueryString["id"];
            ifSentakuPopup.Src = "JC15TantouTouroku.aspx?id=" + id;
            mpeSentakuPopup.Show();

            updSentakuPopup.Update();
        }
        #endregion

        #region chk_taisya_CheckedChanged()
        protected void chk_taisya_CheckedChanged(object sender, EventArgs e)
        {
            if (HF_isKensaku.Value == "false")
            {
                load_data();
            }
            else
            {
                load_data_MutiSelect();
            }
        }
        #endregion

        #region btn_cross_Click
        protected void btn_cross_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "parentButtonClick('btnClose','"+hdnHome.Value+"');", true);
        }
        #endregion

        #region btn_cancel_Click
        protected void btn_cancel_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "parentButtonClick('btnClose','"+hdnHome.Value+"');", true);
        }
        #endregion

        #region btnLink_Click
        protected void btnLink_Click(object sender, EventArgs e)
        {
            if (HF_isKensaku.Value == "false")
            {
                Session["JISHAcTANTOUSHA"] = hiddenCode.Value;
                Session["JISHAsTANTOUSHA"] = hiddenName.Value.Replace("<","&lt").Replace(">","&gt");
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "parentButtonClick('btnJishaTantouSelect','" + hdnHome.Value + "');", true);
            }
        }
        #endregion

        #region btnClose_Click
        protected void btnClose_Click(object sender, EventArgs e)
        {
            ifSentakuPopup.Src = "";
            mpeSentakuPopup.Hide();
            updSentakuPopup.Update();
            if (is_logout == true) //20220119 Added エインドリ－・プ－プゥ
            {
                is_logout = false;
                Session["LoginId"] = null;
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "parentButtonClick('btnToLogin','" + hdnHome.Value + "');", true);
            }
        }
        #endregion

        #region btnEdit_Click
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            //if (lbl_title.Text == "担当者を選択")
            //{
                Session["isTantouOrUser"] = "0"; //担当者
                Session["cTantousha"] = hiddenCode.Value;
                ifSentakuPopup.Style["width"] = "715px";
                ifSentakuPopup.Style["height"] = "100vh";
            //}
            //else
            //{
            //    ifSentakuPopup.Style["width"] = "715px";
            //    ifSentakuPopup.Style["height"] = "100vh";
            //}
            SessionUtility.SetSession("HOME", "Popup");

            String id = Request.QueryString["id"];
            ifSentakuPopup.Src = "JC15TantouTouroku.aspx?id=" + id;
            mpeSentakuPopup.Show();

            updSentakuPopup.Update();
        }
        #endregion

        #region btnSettei_Click
        protected void btnSettei_Click(object sender, EventArgs e)
        {
            if (Session["dtTantou_MultiSelect"] != null)
            {
                DataTable dt = Session["dtTantou_MultiSelect"] as DataTable;
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["name"].ToString() == "選択なし")
                    {
                        dt.Rows[0].Delete();
                    }
                    Session["dtTantou_MultiSelect"] = dt;
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "parentButtonClick('btnJishaTantouSelect','" + hdnHome.Value + "');", true);
                }
            }
        }
        #endregion

        //20220117 Added エインドリ－・プ－プゥ
        #region btn_delete_Click
        protected void btn_delete_Click(object sender, EventArgs e)
        {

            ScriptManager.RegisterStartupScript(this, this.GetType(), "DeleteConfirmMessage",
            "DeleteConfirmBox('削除してもよろしいでしょうか？','" + btnDeleteTantou.ClientID + "');", true);
        }
        #endregion
        //20220117 Added エインドリ－・プ－プゥ
        #region btnDeleteTantou_Click
        protected void btnDeleteTantou_Click(object sender, EventArgs e)
        {
            String id = Request.QueryString["id"];
            DataTable dt_db = JC01Login_Class.Get_DB(id);
            String db = dt_db.Rows[0]["db"].ToString();
            String mail = dt_db.Rows[0]["mail"].ToString();
            ConstantVal.DB_NAME = db;

            JC_ClientConnecction_Class jc = new JC_ClientConnecction_Class();
            jc.loginId = mail;
            con = jc.GetConnection();
            MySqlCommand cmd = new MySqlCommand();
            con.Open();
            string select_mail = "select sMAIL from m_j_tantousha where cTANTOUSHA='" + hiddenCode.Value + "';";
            cmd = new MySqlCommand(select_mail, con);
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
                smail = reader.GetValue(0).ToString();
            con.Close();

            con.Open();
            string delete_tantousha_Query = "delete from m_j_tantousha where cTANTOUSHA='" + hiddenCode.Value + "';";
            cmd = new MySqlCommand(delete_tantousha_Query, con);
            cmd.ExecuteNonQuery();
            con.Close();
            if (smail != "")
            {
                MySqlConnection cn_customer = new MySqlConnection("Server=" + DBUtilitycs.Server + "; Database=" + DBUtilitycs.Database + "; User Id=" + DBUtilitycs.user + "; password=" + DBUtilitycs.pass);
                cn_customer.Open();
                string delete_contact_Query = "delete from contacts where contact_email='" + smail + "';";
                MySqlCommand con_cmd = new MySqlCommand(delete_contact_Query, cn_customer);
                con_cmd.ExecuteNonQuery();
                cn_customer.Close();
            }
            //JC15TantouTouroku_Class c = new JC15TantouTouroku_Class();
            //c.cTANTOUSHA = hiddenCode.Value;
            //if (c.Tantou_Delete())
            //{
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowErrorMessage",
                       "ShowErrorMessage('削除しました。');", true);
            load_data();//20220118 Added エインドリ－・プ－プゥ
            //}
        }
        #endregion

        #region btnToLogin_Click
        protected void btnToLogin_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "parentButtonClick('btnToLogin','" + hdnHome.Value + "');", true);
        }
        #endregion
    }
}