using Common;
using MySql.Data.MySqlClient;
using Service;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace jobzcolud.WebFront
{
    public partial class JC28UserSetting : System.Web.UI.Page
    {
        public Double totalDataCount = 0;
        MySqlConnection cn = null;

        #region Page_Load
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(Request.QueryString["id"]))
            {
                if (!IsPostBack)
                {
                    JC99NavBar navbar_Master = (JC99NavBar)this.Master;
                    navbar_Master.lnkBtnSetting.Style.Add(" background-color", "rgba(46,117,182)");
                    navbar_Master.navbar2.Visible = false;

                    String id = Request.QueryString["id"];
                    DataTable dt_db = JC01Login_Class.Get_DB(id);
                    String db = dt_db.Rows[0]["db"].ToString();
                    String mail = dt_db.Rows[0]["mail"].ToString();
                    ConstantVal.DB_NAME = db;

                    #region MyatNoe Added 20220309
                    JC_ClientConnecction_Class jc = new JC_ClientConnecction_Class();
                    jc.loginId = mail;
                    if (!jc.CheckKengen())
                    {
                        btnCreate.Visible = false;
                        dg_tantousha.Columns[6].Visible = false;
                        dg_tantousha.Columns[5].HeaderStyle.Width = 130;
                    }
                    #endregion

                    this.BindGrid();
                    int kensuu = Convert.ToInt16(DDL_Hyojikensuu.SelectedValue);
                    int endRowIndex = dg_tantousha.Rows.Count;
                    lblHyojikensuu.Text = "1-" + endRowIndex + "/" + totalDataCount;
                }
                Session.Timeout = 480;
            }
            else
            {
                Response.Redirect("JC01Login.aspx");
            }
        }
        #endregion

        #region lnkbtnSetting_Click
        protected void lnkbtnSetting_Click(object sender, EventArgs e)
        {
            //BT_Save.Enabled = true;
            //HF_isChange.Value = "1";
            if (!String.IsNullOrEmpty(Request.QueryString["id"]))
            {
                String id = Request.QueryString["id"];
                Response.Redirect("JC28UserSetting.aspx?id=" + id);
            }
            else
            {
                Response.Redirect("JC01Login.aspx");
            }
            updHeader.Update();
        }
        #endregion

        #region dg_tantousha_PageIndexChanging
        protected void dg_tantousha_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dg_tantousha.PageIndex = e.NewPageIndex;
            this.BindGrid();
            int kensuu = Convert.ToInt16(DDL_Hyojikensuu.SelectedValue);
            int startRowIndex = (kensuu * e.NewPageIndex) + 1;
            int endRowIndex = (kensuu * e.NewPageIndex) + dg_tantousha.Rows.Count;
            lblHyojikensuu.Text = startRowIndex + "-" + endRowIndex + "/" + totalDataCount;
            updHeader.Update();
        }
        #endregion

        protected void LinkButton4_Click(object sender, EventArgs e)
        {

        }
        protected void lnkMitumoriDirectCreate_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(Request.QueryString["id"]))
            {
                String id = Request.QueryString["id"];
                Response.Redirect("JC10MitsumoriTouroku.aspx?id=" + id);
            }
            else
            {
                Response.Redirect("JC01Login.aspx");
            }
        }
        protected void lnkMitumoriTaCopyCreate_Click(object sender, EventArgs e)
        {
            //ifShinkiPopup.Src = "JC12MitsumoriKensaku.aspx";
            //mpeShinkiPopup.Show();
            //updShinkiPopup.Update();
        }

        protected void lnkMitumoriToroku_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(Request.QueryString["id"]))
            {
                String id = Request.QueryString["id"];
                Response.Redirect("JC10MitsumoriTouroku.aspx?id=" + id);
            }
            else
            {
                Response.Redirect("JC01Login.aspx");
            }
        }

        protected void lnkBukkenToroku_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(Request.QueryString["id"]))
            {
                String id = Request.QueryString["id"];
                Response.Redirect("JC09BukkenSyousai.aspx?id=" + id);
            }
            else
            {
                Response.Redirect("JC01Login.aspx");
            }
        }

        protected void lnkbtnHome_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(Request.QueryString["id"]))
            {
                String id = Request.QueryString["id"];
                Response.Redirect("JC07Home.aspx?id=" + id);
            }
            else
            {
                Response.Redirect("JC01Login.aspx");
            }
        }

        protected void lnkbtnKojinJouhou_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(Request.QueryString["id"]))
            {
                String id = Request.QueryString["id"];
                Response.Redirect("JC16KoujinJougouSetting.aspx?id=" + id);
            }
            else
            {
                Response.Redirect("JC01Login.aspx");
            }
        }

        protected void lnkbtnLogOut_Click(object sender, EventArgs e)
        {
            Response.Redirect("JC01Login.aspx");
        }

        #region DDL_Hyojikensuu_SelectedIndexChanged
        protected void DDL_Hyojikensuu_SelectedIndexChanged(object sender, EventArgs e)
        {
            int kensuu = Convert.ToInt16(DDL_Hyojikensuu.SelectedValue);
            dg_tantousha.PageIndex = 0;
            dg_tantousha.PageSize = kensuu;
            this.BindGrid();
            int endRowIndex = dg_tantousha.Rows.Count;
            lblHyojikensuu.Text = "1-" + endRowIndex + "/" + totalDataCount;
        }
        #endregion

        //20211103 Added By エインドリ・プ－プゥ
        #region dg_tantousha_DataBound
        protected void dg_tantousha_DataBound(object sender, EventArgs e)
        {
            //20220309 MyatNoe Added
            if (dg_tantousha.PageCount > 1)
            {
                dg_tantousha.BottomPagerRow.Enabled = true;
            }
            else
            {
                // dg_tantousha.BottomPagerRow.Enabled = false;
            }
        }
        #endregion

        #region BindGrid
        protected void BindGrid()
        {
            if (txtCode.Text == "")
            {
                string searchAllData = "";
                string q_taisya = CheckBox1.Checked == true ? " where fTAISYA=1" : " where fTAISYA=0";
                searchAllData += "SELECT ms.sMAIL,ms.sTANTOUSHA,mj.sKYOTEN,mb.sBUMON,IF(ms.fKANRISHA=1,'システム管理','一般メンバー') as kanrisha,ms.dTAISHA,ms.cTANTOUSHA ";
                searchAllData += "FROM m_j_tantousha ms ";
                searchAllData += "LEFT JOIN m_j_info mj on mj.cCO=ms.cKYOTEN ";
                searchAllData += "LEFT JOIN m_bumon mb ON mb.cBUMON=ms.cBUMON ";
                searchAllData += q_taisya;
                ExecuteQuery(searchAllData);
            }
            else
            {
                /*20211116 Updated By Eaindray*/
                String code = "";
                try
                {
                    code = Convert.ToDouble(txtCode.Text).ToString();
                }
                catch
                {
                    code = "'" + txtCode.Text + "'";
                }
                string searchAllData = "";
                string q_taisya = CheckBox1.Checked == true ? " where fTAISYA=1" : " where fTAISYA=0";
                searchAllData += "SELECT ms.sMAIL,ms.sTANTOUSHA,mj.sKYOTEN,mb.sBUMON,IF(ms.fKANRISHA=1,'システム管理','一般メンバー') as kanrisha,ms.dTAISHA,ms.cTANTOUSHA ";
                searchAllData += "FROM m_j_tantousha ms ";
                searchAllData += "LEFT JOIN m_j_info mj on mj.cCO=ms.cKYOTEN ";
                searchAllData += "LEFT JOIN m_bumon mb ON mb.cBUMON=ms.cBUMON ";
                searchAllData += q_taisya;
                searchAllData += " and (ms.cTANTOUSHA=" + code + " OR ms.sTANTOUSHA COLLATE utf8_unicode_ci LIKE '%" + txtCode.Text + "%' OR mb.sBUMON COLLATE utf8_unicode_ci LIKE '%" + txtCode.Text + "%');";
                ExecuteQuery(searchAllData);

                //bool is_code = false;
                //try
                //{
                //    string code = Convert.ToInt32(txtCode.Text).ToString();
                //    if (code.Length == 1)
                //    {
                //        txtCode.Text = "000" + code;
                //    }
                //    else if (code.Length == 2)
                //    {
                //        txtCode.Text = "00" + code;
                //    }
                //    else if (code.Length == 3)
                //    {
                //        txtCode.Text = "0" + code;
                //    }
                //    is_code = true;
                //}
                //catch (Exception e1)
                //{
                //    is_code = false;
                //}
                //bool has_val = false;
                //if (is_code == true)
                //{
                //    string searchAllData = "";
                //    string q_taisya = CheckBox1.Checked == true ? " where fTAISYA=1" : " where fTAISYA=0";
                //    searchAllData += "SELECT ms.sMAIL,ms.sTANTOUSHA,mj.sKYOTEN,mb.sBUMON,IF(ms.fKANRISHA=1,'システム管理','一般メンバー') as kanrisha,ms.dTAISHA ";
                //    searchAllData += "FROM m_j_tantousha ms ";
                //    searchAllData += "LEFT JOIN m_j_info mj on mj.cCO=ms.cKYOTEN ";
                //    searchAllData += "LEFT JOIN m_bumon mb ON mb.cBUMON=ms.cBUMON ";
                //    searchAllData += q_taisya;
                //    searchAllData += " and cTANTOUSHA=" + txtCode.Text + " ;";
                //    ExecuteQuery(searchAllData);
                //}
                //else
                //{
                //    string searchAllData = "";
                //    string q_taisya = CheckBox1.Checked == true ? " fTAISYA=1" : "  fTAISYA=0";
                //    searchAllData += "SELECT ms.sMAIL,ms.sTANTOUSHA,mj.sKYOTEN,mb.sBUMON,IF(ms.fKANRISHA=1,'システム管理','一般メンバー') as kanrisha,ms.dTAISHA ";
                //    searchAllData += "FROM m_j_tantousha ms ";
                //    searchAllData += "LEFT JOIN m_j_info mj on mj.cCO=ms.cKYOTEN ";
                //    searchAllData += "LEFT JOIN m_bumon mb ON mb.cBUMON=ms.cBUMON ";
                //    searchAllData += " WHERE " + q_taisya + " and ms.sTANTOUSHA COLLATE utf8_unicode_ci LIKE '%" + txtCode.Text + "%' OR mb.sBUMON COLLATE utf8_unicode_ci LIKE '%" + txtCode.Text + "%';";
                //    ExecuteQuery(searchAllData);
                //}
            }
        }
        #endregion

        #region ExecuteQuery
        public void ExecuteQuery(string qr)
        {
            String id = Request.QueryString["id"];
            DataTable dt_db = JC01Login_Class.Get_DB(id);
            String db = dt_db.Rows[0]["db"].ToString();
            String mail = dt_db.Rows[0]["mail"].ToString();
            ConstantVal.DB_NAME = db;

            JC_ClientConnecction_Class jc = new JC_ClientConnecction_Class();
            jc.loginId = mail;
            cn = jc.GetConnection();
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandTimeout = 0;
            cmd = new MySqlCommand(qr, cn);
            cn.Open();
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            cn.Close();
            da.Dispose();

            dg_tantousha.DataSource = dt;
            dg_tantousha.DataBind();
            totalDataCount = dt.Rows.Count;

            #region 20220309 MyatNoe Added
            if (dt.Rows.Count > 0)
            {
                lblDataNai.Visible = false;
            }
            else
            {
                lblDataNai.Visible = true;
            }
            #endregion
        }
        #endregion

        #region txtCode_textChanged
        protected void txtCode_textChanged(object sender, EventArgs e)
        {
            int kensuu = Convert.ToInt16(DDL_Hyojikensuu.SelectedValue);
            dg_tantousha.PageIndex = 0;
            dg_tantousha.PageSize = kensuu;
            this.BindGrid();
            int endRowIndex = dg_tantousha.Rows.Count;
            lblHyojikensuu.Text = "1-" + endRowIndex + "/" + totalDataCount;
        }
        #endregion

        #region chk_taisya_CheckedChanged
        protected void chk_taisya_CheckedChanged(object sender, EventArgs e)
        {
            int kensuu = Convert.ToInt16(DDL_Hyojikensuu.SelectedValue);
            dg_tantousha.PageIndex = 0;
            dg_tantousha.PageSize = kensuu;
            this.BindGrid();
            int endRowIndex = dg_tantousha.Rows.Count;
            lblHyojikensuu.Text = "1-" + endRowIndex + "/" + totalDataCount;
        }
        #endregion

        protected void btnCreate_Click(object sender, EventArgs e)　/*20211116 Added By Eaindray－*/
        {
            Session["isTantouOrUser"] = "0"; //担当者
            Session["cTantousha"] = null;
            ifSentakuPopup.Style["width"] = "100vw";
            ifSentakuPopup.Style["height"] = "100vh";
            SessionUtility.SetSession("HOME", "Master");

            String id = Request.QueryString["id"];
            ifSentakuPopup.Src = "JC15TantouTouroku.aspx?id=" + id;
            mpeSentakuPopup.Show();

            updSentakuPopup.Update();

        }
        protected void btnSave_Click(object sender, EventArgs e) /*20211116 Added By Eaindray－*/
        {
            ifSentakuPopup.Src = "";
            mpeSentakuPopup.Hide();
            updSentakuPopup.Update();

            String id = Request.QueryString["id"];
            DataTable dt_db = JC01Login_Class.Get_DB(id);
            String db = dt_db.Rows[0]["db"].ToString();
            String mail = dt_db.Rows[0]["mail"].ToString();
            ConstantVal.DB_NAME = db;

            JC_ClientConnecction_Class jc = new JC_ClientConnecction_Class();
            jc.loginId = mail;
            if (!jc.CheckKengen())
            {
                btnCreate.Visible = false;
                dg_tantousha.Columns[6].Visible = false;
                dg_tantousha.Columns[5].HeaderStyle.Width = 130;
            }
            int pIndex = dg_tantousha.PageIndex;
            this.BindGrid();
            int kensuu = Convert.ToInt16(DDL_Hyojikensuu.SelectedValue);
            int startRowIndex = (kensuu * pIndex) + 1;
            int endRowIndex = (kensuu * pIndex) + dg_tantousha.Rows.Count;
            lblHyojikensuu.Text = startRowIndex + "-" + endRowIndex + "/" + totalDataCount;
            updHeader.Update();
        }
        protected void btnClose_Click(object sender, EventArgs e) /*20211116 Added By エインドリ－*/
        {
            ifSentakuPopup.Src = "";
            mpeSentakuPopup.Hide();
            updSentakuPopup.Update();

            String id = Request.QueryString["id"];
            DataTable dt_db = JC01Login_Class.Get_DB(id);
            String db = dt_db.Rows[0]["db"].ToString();
            String mail = dt_db.Rows[0]["mail"].ToString();
            ConstantVal.DB_NAME = db;

            JC_ClientConnecction_Class jc = new JC_ClientConnecction_Class();
            jc.loginId = mail;
            if (!jc.CheckKengen())
            {
                btnCreate.Visible = false;
                dg_tantousha.Columns[6].Visible = false;
                dg_tantousha.Columns[5].HeaderStyle.Width = 130;
            }
            int pIndex = dg_tantousha.PageIndex;
            this.BindGrid();
            int kensuu = Convert.ToInt16(DDL_Hyojikensuu.SelectedValue);
            int startRowIndex = (kensuu * pIndex) + 1;
            int endRowIndex = (kensuu * pIndex) + dg_tantousha.Rows.Count;
            lblHyojikensuu.Text = startRowIndex + "-" + endRowIndex + "/" + totalDataCount;
            updHeader.Update();
        }

        #region btnEdit_Click
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            GridViewRow gvrow = (sender as LinkButton).NamingContainer as GridViewRow;
            Label lblcTantousya = gvrow.FindControl("cTantousha") as Label;
            string userId = lblcTantousya.Text;
            dg_tantousha.EditIndex = -1;

            Session["cTANTOUSHA"] = userId;
            if (Session["cTANTOUSHA"] != null)
            {
                ifSentakuPopup.Style["width"] = "715px";
                ifSentakuPopup.Style["height"] = "100vh";
                SessionUtility.SetSession("HOME", "Master");
                String id = Request.QueryString["id"];
                ifSentakuPopup.Src = "JC15TantouTouroku.aspx?id=" + id;
                mpeSentakuPopup.Show();

                updSentakuPopup.Update();
            }
            else
            {
                Response.Redirect("JC01Login.aspx");
            }
        }
        #endregion

        #region btnDelete_Click
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(Request.QueryString["id"]))
            {
                GridViewRow gvrow = (sender as LinkButton).NamingContainer as GridViewRow;
                Label lblcTantousya = gvrow.FindControl("cTantousha") as Label;
                HF_UserID.Value = lblcTantousya.Text;

                ScriptManager.RegisterStartupScript(this, this.GetType(), "DeleteConfirmMessage",
            "DeleteConfirmBox('削除してもよろしいでしょうか？','" + btnDeleteUser.ClientID + "');", true);
                updHeader.Update();
            }
            else
            {
                Response.Redirect("JC01Login.aspx");
            }
        }
        #endregion

        protected void btnDeleteUser_Click(object sender, EventArgs e)
        {
            string userId = HF_UserID.Value;
            string smail = "";
            String id = Request.QueryString["id"];
            DataTable dt_db = JC01Login_Class.Get_DB(id);
            String db = dt_db.Rows[0]["db"].ToString();
            String mail = dt_db.Rows[0]["mail"].ToString();
            ConstantVal.DB_NAME = db;

            JC_ClientConnecction_Class jc = new JC_ClientConnecction_Class();
            jc.loginId = mail;
            cn = jc.GetConnection();
            MySqlCommand cmd = new MySqlCommand();
            cn.Open();
            string select_mail = "select sMAIL from m_j_tantousha where cTANTOUSHA='" + userId + "';";
            cmd = new MySqlCommand(select_mail, cn);
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
                smail = reader.GetValue(0).ToString();
            cn.Close();

            cn.Open();
            string delete_tantousha_Query = "delete from m_j_tantousha where cTANTOUSHA='" + userId + "';";
            cmd = new MySqlCommand(delete_tantousha_Query, cn);
            cmd.ExecuteNonQuery();
            cn.Close();
            if (smail != "")
            {
                MySqlConnection cn_customer = new MySqlConnection("Server=" + DBUtilitycs.Server + "; Database=" + DBUtilitycs.Database + "; User Id=" + DBUtilitycs.user + "; password=" + DBUtilitycs.pass);
                cn_customer.Open();
                string delete_contact_Query = "delete from contacts where contact_email='" + smail + "';";
                MySqlCommand con_cmd = new MySqlCommand(delete_contact_Query, cn_customer);
                con_cmd.ExecuteNonQuery();
                cn_customer.Close();
            }

            int pIndex = dg_tantousha.PageIndex;
            this.BindGrid();
            int kensuu = Convert.ToInt16(DDL_Hyojikensuu.SelectedValue);
            int startRowIndex = (kensuu * pIndex) + 1;
            int endRowIndex = (kensuu * pIndex) + dg_tantousha.Rows.Count;
            lblHyojikensuu.Text = startRowIndex + "-" + endRowIndex + "/" + totalDataCount;
            updHeader.Update();
        }
    }
}