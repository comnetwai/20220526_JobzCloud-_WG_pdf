using Common;
using MySql.Data.MySqlClient;
using Service;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace jobzcolud.WebFront
{
    public partial class JC36_MitsuJyotaiKensaku : System.Web.UI.Page
    {
        MySqlConnection con = null;
        List<string> list_cjyoutai = new List<string>();
        List<string> list_sjyoutai = new List<string>();
        public bool show_msg { get; set; }
        public static string delete_id;
        public static string smail;
        public static bool is_logout = false;

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
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "parentButtonClick('btnToJyotai','" + hdnHome.Value + "');", true);
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
                           
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                    Session.Timeout = 480;
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "parentButtonClick('btnToJyotai','" + hdnHome.Value + "');", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "parentButtonClick('btnToJyotai','" + hdnHome.Value + "');", true);
            }
        }
        #endregion

        #region btn_cross_Click()
        protected void btn_cross_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "parentButtonClick('btnClose1','" + hdnHome.Value + "');", true);
        }
        #endregion

        #region tb_search_textChanged()
        protected void tb_search_textChanged(object sender, EventArgs e)
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

        #region load_data_MutiSelect()
        protected void load_data_MutiSelect()
        {
            try
            {

                list_cjyoutai.Clear();
                list_sjyoutai.Clear();
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
                    string select_tantou_count = "select cJYOTAI,sJYOTAI FROM m_mitumori_jyotai";
                    MySqlCommand cmd = new MySqlCommand(select_tantou_count, con);
                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        has_value = true;
                        list_cjyoutai.Add(reader.GetValue(0).ToString());
                        list_sjyoutai.Add(reader.GetValue(1).ToString());
                    }
                    reader.Close();
                    list_cjyoutai.Insert(0, "");
                    list_sjyoutai.Insert(0, "選択なし");
                    if (has_value == true)
                    {
                        int c = 0;

                        string idx = HF_selectIndex.Value;
                        string[] idxarray = null;
                        if (!String.IsNullOrEmpty(idx))
                        {
                            idx = idx.Replace("'", "");

                            idxarray = idx.Split(',');
                        }

                        for (int j = 0; j < (list_sjyoutai.Count / 3) + 1; j++)
                        {
                            // Create a new row and add it to the Rows collection.
                            HtmlTableRow row = new HtmlTableRow();
                            // Iterate through the cells of a row.
                            for (int i = 0; i < 3; i++)
                            {
                                if (c < list_sjyoutai.Count)
                                {
                                    Boolean isSelect = false;
                                    if (idxarray != null)
                                    {
                                        isSelect = Array.Exists(idxarray, x => x == c.ToString());
                                    }
                                    if (HF_fTime.Value == "0")
                                    {
                                        DataTable dt_Multiselect = new DataTable();
                                        dt_Multiselect = Session["dtJyotai_MultiSelect"] as DataTable;
                                        isSelect = dt_Multiselect.AsEnumerable().Any(r => list_cjyoutai[c] == r.Field<String>("code"));
                                        if (isSelect)
                                        {
                                            String txt = HF_selectIndex.Value;
                                            if (txt != "")
                                            {
                                                HF_selectIndex.Value = txt + ",'" + c.ToString() + "'";
                                            }
                                            else
                                            {
                                                HF_selectIndex.Value = "'" + c.ToString() + "'";
                                            }
                                        }
                                    }
                                    string str_cell = "";
                                    // Create a new cell and add it to the HtmlTableRow 
                                    // Cells collection.
                                    HtmlTableCell cell = new HtmlTableCell();
                                   
                                    cell.ID = list_cjyoutai[c];
                                    if (isSelect)
                                    {
                                        DataRow dr = dtSelect.NewRow();
                                        dr[0] = list_cjyoutai[c];
                                        dr[1] = list_sjyoutai[c];
                                        dtSelect.Rows.Add(dr);
                                        str_cell += "<a href='#' id='" + list_cjyoutai[c] + "' runat='server' style='padding-left:2px;background:#4472c4;padding-top:8px;margin-right:10px;white-space: nowrap;overflow: hidden; text-overflow: ellipsis;display: inline-block;  height:40px;width:190px;font-size: 14px;text-decoration:none; color: white;font-family: 'Hiragino Sans', 'Open Sans', 'Meiryo', 'Hiragino Kaku Gothic Pro', 'メイリオ', 'MS ゴシック', 'sans - serif';' onclick='get_id(this," + c + ")'>" + list_sjyoutai[c] + "</a>";
                                    }
                                    else
                                    {
                                        str_cell += "<a href='#' id='" + list_cjyoutai[c] + "' runat='server' style='padding-left:2px;padding-top:8px;margin-right:10px;white-space: nowrap;overflow: hidden; text-overflow: ellipsis;display: inline-block;  height:40px;width:190px;font-size: 14px;text-decoration:none; color: black;font-family: 'Hiragino Sans', 'Open Sans', 'Meiryo', 'Hiragino Kaku Gothic Pro', 'メイリオ', 'MS ゴシック', 'sans - serif';' onclick='get_id(this," + c + ")'>" + list_sjyoutai[c] + "</a>";
                                    }
                                    //if (c != 0)
                                    //{
                                    //    str_cell += "<div style='float:right'><div class='dropdown-content'>";
                                    //    str_cell += "<table runat='server'><tr id='trow'>";
                                    //    str_cell += "<td><a href='#' id='" + list_cjyoutai[c] + "' style='font-size: 13px; ' onclick='edit(this.id)'><img src='../Images/draw.png' style='height:15px;width:15px;'/></a></td>";
                                    //    str_cell += "<td><a href='#' class='Delete' id='" + list_cjyoutai[c] + "' onclick='return Confirm(this.id)'><img src='../Images/trash.png' style='height:15px;width:15px;'/></i></a></td>";
                                    //    str_cell += "</tr></table>";
                                    //    str_cell += "</div></div>";
                                    //}
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
                    list_sjyoutai.Clear();
                    list_cjyoutai.Clear();
                    String id = Request.QueryString["id"];
                    DataTable dt_db = JC01Login_Class.Get_DB(id);
                    String db = dt_db.Rows[0]["db"].ToString();
                    String mail = dt_db.Rows[0]["mail"].ToString();
                    ConstantVal.DB_NAME = db;
                    JC_ClientConnecction_Class jc = new JC_ClientConnecction_Class();
                    jc.loginId = mail;
                    con = jc.GetConnection();
                    con.Open();

                    bool has_val = false;

                    String code = "";
                    try
                    {
                        code = Convert.ToDouble(tb_code.Text).ToString();
                        code = code.PadLeft(2, '0');
                    }
                    catch
                    {
                        code = "'" + tb_code.Text + "'";
                    }
                    string select_tantou_count = "select cJYOTAI,sJYOTAI FROM m_mitumori_jyotai where (cJYOTAI=" + code + " OR sJYOTAI COLLATE utf8_unicode_ci LIKE '%" + tb_code.Text + "%');";
                    MySqlCommand cmd = new MySqlCommand(select_tantou_count, con);
                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        list_cjyoutai.Add(reader.GetValue(0).ToString());
                        list_sjyoutai.Add(reader.GetValue(1).ToString());
                        has_val = true;
                    }
                    reader.Close();
                    list_cjyoutai.Insert(0, "");
                    list_sjyoutai.Insert(0, "選択なし");
                    if (has_val == true)
                    {
                        int c = 0;
                        string idx = HF_selectIndex.Value;
                        string[] idxarray = null;
                        if (!String.IsNullOrEmpty(idx))
                        {
                            idx = idx.Replace("'", "");

                            idxarray = idx.Split(',');
                        }
                        for (int j = 0; j < (list_sjyoutai.Count / 3) + 1; j++)
                        {
                            // Create a new row and add it to the Rows collection.
                            HtmlTableRow row = new HtmlTableRow();
                            // Iterate through the cells of a row.
                            for (int i = 0; i < 3; i++)
                            {
                                if (c < list_sjyoutai.Count)
                                {
                                    Boolean isSelect = false;
                                    if (idxarray != null)
                                    {
                                        isSelect = Array.Exists(idxarray, x => x == c.ToString());
                                    }
                                    if (HF_fTime.Value == "0")
                                    {
                                        DataTable dt_Multiselect = new DataTable();
                                        dt_Multiselect = Session["dtJyotai_MultiSelect"] as DataTable;
                                        isSelect = dt_Multiselect.AsEnumerable().Any(r => list_cjyoutai[c] == r.Field<String>("code"));
                                        if (isSelect)
                                        {
                                            String txt = HF_selectIndex.Value;
                                            if (txt != "")
                                            {
                                                HF_selectIndex.Value = txt + ",'" + c.ToString() + "'";
                                            }
                                            else
                                            {
                                                HF_selectIndex.Value = "'" + c.ToString() + "'";
                                            }
                                        }
                                    }
                                    string str_cell = "";
                                    // Create a new cell and add it to the HtmlTableRow 
                                    // Cells collection.
                                    HtmlTableCell cell = new HtmlTableCell();
                                    
                                    cell.ID = list_cjyoutai[c];
                                    if (isSelect)
                                    {
                                        DataRow dr = dtSelect.NewRow();
                                        dr[0] = list_cjyoutai[c];
                                        dr[1] = list_sjyoutai[c];
                                        dtSelect.Rows.Add(dr);
                                        str_cell += "<a href='#' id='" + list_cjyoutai[c] + "' runat='server' style='padding-left:2px;background:#4472c4;padding-top:8px;margin-right:10px;white-space: nowrap;overflow: hidden; text-overflow: ellipsis;display: inline-block;  height:40px;width:190px;font-size: 14px;text-decoration:none; color: white;font-family: 'Hiragino Sans', 'Open Sans', 'Meiryo', 'Hiragino Kaku Gothic Pro', 'メイリオ', 'MS ゴシック', 'sans - serif';' onclick='get_id(this," + c + ")'>" + list_sjyoutai[c] + "</a>";
                                    }
                                    else
                                    {
                                        str_cell += "<a href='#' id='" + list_cjyoutai[c] + "' runat='server' style='padding-left:2px;padding-top:8px;margin-right:10px;white-space: nowrap;overflow: hidden; text-overflow: ellipsis;display: inline-block;  height:40px;width:190px;font-size: 14px;text-decoration:none; color: black;font-family: 'Hiragino Sans', 'Open Sans', 'Meiryo', 'Hiragino Kaku Gothic Pro', 'メイリオ', 'MS ゴシック', 'sans - serif';' onclick='get_id(this," + c + ")'>" + list_sjyoutai[c] + "</a>";
                                    }
                                    //if (c != 0)
                                    //{
                                    //    str_cell += "<div style='float:right'><div class='dropdown-content'>";
                                    //    str_cell += "<table runat='server'><tr id='trow'>";
                                    //    str_cell += "<td><a href='#' id='" + list_cjyoutai[c] + "' style='font-size: 13px; ' onclick='edit(this.id)'><img src='../Images/draw.png' style='height:15px;width:15px;'/></a></td>";
                                    //    str_cell += "<td><a href='#' class='Delete' id='" + list_cjyoutai[c] + "' onclick='return Confirm(this.id)'><img src='../Images/trash.png' style='height:15px;width:15px;'/></i></a></td>";
                                    //    str_cell += "</tr></table>";
                                    //    str_cell += "</div></div>";
                                    //}
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

                Session["dtJyotai_MultiSelect"] = dtSelect;

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

                list_sjyoutai.Clear();
                list_cjyoutai.Clear();
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
                    string select_tantou_count = "select cJYOTAI,sJYOTAI FROM m_mitumori_jyotai";
                    MySqlCommand cmd = new MySqlCommand(select_tantou_count, con);
                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        has_value = true;
                        list_cjyoutai.Add(reader.GetValue(0).ToString());
                        list_sjyoutai.Add(reader.GetValue(1).ToString());
                    }
                    reader.Close();
                    list_cjyoutai.Insert(0, "");
                    list_sjyoutai.Insert(0, "選択なし");
                    if (has_value == true)
                    {
                        int c = 0;
                        for (int j = 0; j < (list_sjyoutai.Count / 3) + 1; j++)
                        {
                            // Create a new row and add it to the Rows collection.
                            HtmlTableRow row = new HtmlTableRow();
                            // Iterate through the cells of a row.
                            for (int i = 0; i < 3; i++)
                            {
                                if (c < list_sjyoutai.Count)
                                {
                                    string str_cell = "";
                                    // Create a new cell and add it to the HtmlTableRow 
                                    // Cells collection.
                                    HtmlTableCell cell = new HtmlTableCell();
                                    
                                    cell.ID = list_cjyoutai[c];
                                    str_cell += "<a href='#' id='" + list_cjyoutai[c] + "' runat='server' style='padding-top:8px;margin-right:10px;white-space: nowrap;overflow: hidden; text-overflow: ellipsis;display: inline-block;  height:40px;width:190px;font-size: 14px;text-decoration:none; color: black;font-family: 'Hiragino Sans', 'Open Sans', 'Meiryo', 'Hiragino Kaku Gothic Pro', 'メイリオ', 'MS ゴシック', 'sans - serif';' onclick='get_id1(this.id,this.innerText)'>" + list_sjyoutai[c] + "</a>";
                                    //if (c != 0)
                                    //{
                                    //    str_cell += "<div style='float:right'><div class='dropdown-content'>";
                                    //    str_cell += "<table runat='server'><tr id='trow'>";
                                    //    str_cell += "<td><a href='#' id='" + list_cjyoutai[c] + "' style='font-size: 13px; ' onclick='edit(this.id)'><img src='../Images/draw.png' style='height:15px;width:15px;'/></a></td>";
                                    //    str_cell += "<td><a href='#' class='Delete' id='" + list_cjyoutai[c] + "' onclick='return Confirm(this.id)'><img src='../Images/trash.png' style='height:15px;width:15px;'/></i></a></td>";
                                    //    str_cell += "</tr></table>";
                                    //    str_cell += "</div></div>";
                                    //}
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
                    list_cjyoutai.Clear();
                    list_sjyoutai.Clear();
                    String id = Request.QueryString["id"];
                    DataTable dt_db = JC01Login_Class.Get_DB(id);
                    String db = dt_db.Rows[0]["db"].ToString();
                    String mail = dt_db.Rows[0]["mail"].ToString();
                    ConstantVal.DB_NAME = db;
                    JC_ClientConnecction_Class jc = new JC_ClientConnecction_Class();
                    jc.loginId = mail;
                    con = jc.GetConnection();
                    con.Open();
                    bool has_val = false;

                    String code = "";
                    try
                    {
                        code = Convert.ToDouble(tb_code.Text).ToString();
                        code = code.PadLeft(2, '0');
                    }
                    catch
                    {
                        code = "'" + tb_code.Text + "'";
                    }
                    string select_tantou_count = "select cJYOTAI,sJYOTAI FROM m_mitumori_jyotai where (cJYOTAI=" + code + " OR sJYOTAI COLLATE utf8_unicode_ci LIKE '%" + tb_code.Text + "%');";
                    MySqlCommand cmd = new MySqlCommand(select_tantou_count, con);
                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        list_cjyoutai.Add(reader.GetValue(0).ToString());
                        list_sjyoutai.Add(reader.GetValue(1).ToString());
                        has_val = true;
                    }
                    reader.Close();
                    list_cjyoutai.Insert(0, "");
                    list_sjyoutai.Insert(0, "選択なし");
                    if (has_val == true)
                    {
                        int c = 0;
                        for (int j = 0; j < (list_sjyoutai.Count / 3) + 1; j++)
                        {
                            // Create a new row and add it to the Rows collection.
                            HtmlTableRow row = new HtmlTableRow();
                            // Iterate through the cells of a row.
                            for (int i = 0; i < 3; i++)
                            {
                                if (c < list_sjyoutai.Count)
                                {
                                    string str_cell = "";
                                    // Create a new cell and add it to the HtmlTableRow 
                                    // Cells collection.
                                    HtmlTableCell cell = new HtmlTableCell();
                                    
                                    cell.ID = list_cjyoutai[c];
                                    str_cell += "<a href='#' id='" + list_cjyoutai[c] + "' runat='server' style='padding-top:8px;margin-right:10px;white-space: nowrap;overflow: hidden; text-overflow: ellipsis;display: inline-block;  height:40px;width:190px;font-size: 14px;text-decoration:none; color: black;font-family: 'Hiragino Sans', 'Open Sans', 'Meiryo', 'Hiragino Kaku Gothic Pro', 'メイリオ', 'MS ゴシック', 'sans - serif';' onclick='get_id1(this.id,this.innerText)'>" + list_sjyoutai[c] + "</a>";
                                    //if (c != 0)
                                    //{
                                    //    str_cell += "<div style='float:right'><div class='dropdown-content'>";
                                    //    str_cell += "<table runat='server'><tr id='trow'>";
                                    //    str_cell += "<td><a href='#' id='" + list_cjyoutai[c] + "' style='font-size: 13px; ' onclick='edit(this.id)'><img src='../Images/draw.png' style='height:15px;width:15px;'/></a></td>";
                                    //    str_cell += "<td><a href='#' class='Delete' id='" + list_cjyoutai[c] + "' onclick='return Confirm(this.id)'><img src='../Images/trash.png' style='height:15px;width:15px;'/></i></a></td>";
                                    //    str_cell += "</tr></table>";
                                    //    str_cell += "</div></div>";
                                    //}
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

        #region btnLink_Click()
        protected void btnLink_Click(object sender, EventArgs e)
        {
            if (HF_isKensaku.Value == "false")
            {
                Session["cJYOTAI"] = hiddenCode.Value;
                Session["sJYOTAI"] = hiddenName.Value;
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "parentButtonClick('btnJyoutaiSelect','" + hdnHome.Value + "');", true);
            }
        }
        #endregion

        #region btnSettei_Click()
        protected void btnSettei_Click(object sender, EventArgs e)
        {
            if (Session["dtJyotai_MultiSelect"] != null)
            {
                DataTable dt = Session["dtJyotai_MultiSelect"] as DataTable;
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["name"].ToString() == "選択なし")
                    {
                        dt.Rows[0].Delete();
                    }
                    Session["dtJyotai_MultiSelect"] = dt;
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "parentButtonClick('btnJyoutaiSelect','" + hdnHome.Value + "');", true);
                }
            }
        }
        #endregion

        #region btn_cancel_Click()
        protected void btn_cancel_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "parentButtonClick('btnClose1','" + hdnHome.Value + "');", true);
        }
        #endregion

        #region btnJyotaiClose_Click()
        protected void btnJyotaiClose_Click(object sender, EventArgs e)
        {
            ifSentakujyoutaiPopup.Src = "";
            mpeSentakujyoutaiPopup.Hide();
            updSentakujyoutaiPopup.Update();
            if (is_logout == true)
            {
                is_logout = false;
                Session["LoginId"] = null;
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "parentButtonClick('btnToJyotai','" + hdnHome.Value + "');", true);
            }
        }
        #endregion

        #region btnToJyotai_Click()
        protected void btnToJyotai_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "parentButtonClick('btnToJyotai','" + hdnHome.Value + "');", true);
        }
        #endregion
    }
}