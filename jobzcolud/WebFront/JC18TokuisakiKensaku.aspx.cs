using Common;
using MySql.Data.MySqlClient;
using Service;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JobzCloud.WebFront
{
    public partial class JC18TokuisakiKensaku : System.Web.UI.Page
    {

        MySqlConnection cn = null;
        MySqlCommand cmd = null;
        string Database = String.Empty;
        string cTokuisaki, sTokuisaki = string.Empty;
        DataTable dt = new DataTable();
        DataTable dt1 = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(Request.QueryString["id"]))
                {
                    if (!IsPostBack)
                    {
                        if (SessionUtility.GetSession("HOME") != null)
                        {
                            hdnHome.Value = SessionUtility.GetSession("HOME").ToString();
                            SessionUtility.SetSession("HOME", null);
                        }

                        btnCreate.Visible = true;

                        if (Session["ftokuisakisyosai"] != null)
                        {
                            if (Session["ftokuisakisyosai"].ToString() == "1") //得意先詳細画面から
                            {
                                btnCreate.Visible = false;
                            }
                        }

                        if (Request.Cookies["colWidthbTokuisaki"] != null)
                        {
                            HF_GridSize.Value = Request.Cookies["colWidthbTokuisaki"].Value;
                        }
                        else
                        {
                            HF_GridSize.Value = "70, 335, 125, 125, 100, 100, 214";
                        }
                        if (Request.Cookies["colWidthbTokuisakiGrid"] != null)
                        {
                            HF_Grid.Value = Request.Cookies["colWidthbTokuisakiGrid"].Value;
                        }
                        else
                        {
                            HF_Grid.Value = "1093";
                        }
                        this.BindGridView();
                    }
                    Session.Timeout = 480;
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "parentButtonClick('btnToLogin','" + hdnHome.Value + "');", true);
                }
            }
            catch
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "parentButtonClick('btnToLogin','" + hdnHome.Value + "');", true);
            }
        }
        #region lnkCode_Click
        protected void lnkCode_Click(object sender, EventArgs e)
        {
            //var LkB = sender as LinkButton;
            //var row = LkB.NamingContainer;
            //Label lbl_stokuisaki = (row.FindControl("lblsToukuisaki") as Label);
            //Session["cTOUKUISAKI"] = LkB.Text;
            //Session["sTOUKUISAKI"] = lbl_stokuisaki.Text;



            if (ConstantVal.check_ts == true)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "parentButtonClick('btnSeikyusakiSelect','" + hdnHome.Value + "');", true);
                ConstantVal.check_ts = false;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "parentButtonClick('btnTokuisakiSelect','" + hdnHome.Value + "');", true);
            }
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "parentButtonClick('btnTokuisakiSelect','" + hdnHome.Value + "');", true);
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "parentButtonClick('btnSeikyusakiSelect','" + hdnHome.Value + "');", true);
        }

        #endregion

        #region Button1_Command
        protected void Button1_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "B1")
            {
                //Label1.Text = Button1.CommandArgument;
            }

        }
        #endregion

        #region BindGridView()
        protected void BindGridView()
        {
            string searchAllData = "";
            searchAllData += "select m_tokuisaki.cTOKUISAKI ," +
                "Replace(Replace(ifnull(m_tokuisaki.sTOKUISAKI1,''),'<','&lt'),'>','&gt') as sTOKUISAKI1," +
                "m_tokuisaki.sTEL,mjt.sTANTOUSHA," +
                "m_tokuisaki.sFAX,m_tokuisaki.cYUUBIN,m_tokuisaki.sJUUSHO1,m_tokuisaki.nNEBIKIRITSU,m_gyousyu.sGYOUSYU,msb.sSYUBETU " +
                "from m_tokuisaki " +
                "left join m_j_tantousha as mjt on m_tokuisaki.cEIGYOU_C = mjt.cTANTOUSHA " +
                "left join m_gyousyu on m_gyousyu.cGYOUSYU = m_tokuisaki.cGYOUSYU  " +
                "left join m_syubetu as msb on m_tokuisaki.cSYUBETU = msb.cSYUBETU  " +
                "left join m_tokuisaki_rank ON m_tokuisaki.cRANK_SINYOU = m_tokuisaki_rank.cRANK " +
                "left join(select cTOKUISAKI, sTANTOU from tokuisaki_tantousha where nJUNBAN= 1) as tt on m_tokuisaki.cTOKUISAKI = tt.cTOKUISAKI Where 1=1 AND m_tokuisaki.fHAIJYO='0' ";

            if (!String.IsNullOrEmpty(txtCode.Text))
            {
                String code = "";
                try
                {
                    code = Convert.ToDouble(txtCode.Text).ToString();
                }
                catch
                {
                    code = "'" + txtCode.Text + "'";
                }
                searchAllData += "AND (m_tokuisaki.cTOKUISAKI =" + code + " or m_tokuisaki.sTOKUISAKI1 COLLATE utf8_unicode_ci LIKE '" + '%' + txtCode.Text + '%' + "') ";
            }
            searchAllData += "order by m_tokuisaki.cTOKUISAKI";
            ExecuteQuery(searchAllData);
        }
        #endregion

        #region ExecuteQuery()
        public void ExecuteQuery(string query)
        {
            try
            {
                String id = Request.QueryString["id"];
                DataTable dt_db = JC01Login_Class.Get_DB(id);
                String db = dt_db.Rows[0]["db"].ToString();
                String mail = dt_db.Rows[0]["mail"].ToString();
                ConstantVal.DB_NAME = db;

                JC_ClientConnecction_Class jc = new JC_ClientConnecction_Class();
                jc.loginId = mail;
                cn = jc.GetConnection();
                cn.Open();
                MySqlCommand myCommand = new MySqlCommand(query, cn);
                MySqlDataReader dr = myCommand.ExecuteReader();
                ViewState["sortDirection"] = SortDirection.Ascending;
                ViewState["z_sortexpresion"] = "cTOKUISAKI";
                if (dr.HasRows)
                {
                    dr.Close();
                    MySqlDataAdapter sda = new MySqlDataAdapter(query, cn);
                    dt.Clear();
                    //dt.Rows.Add(dt.NewRow());
                    sda.Fill(dt);
                    ViewState["Row"] = dt;
                    grid_touisaki.DataSource = dt;
                    grid_touisaki.DataBind();
                    lblDataNai.Visible = false;

                }
                else
                {
                    dr.Close();
                    //Response.Write("<script>alert('データがありません。')</script>");
                    dt.Clear();
                    ViewState["Row"] = dt;
                    grid_touisaki.DataSource = dt;
                    grid_touisaki.DataBind();
                    lblDataNai.Visible = true;
                }
                cn.Close();
                updBody.Update();
            }
            catch
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "parentButtonClick('btnToLogin','" + hdnHome.Value + "');", true);
            }
        }
        #endregion

        #region isNumeric
        public static bool isNumeric(string s)
        {
            return int.TryParse(s, out int n);
        }
        #endregion

        #region btnFusenHeaderCross_Click
        protected void btnFusenHeaderCross_Click(object sender, EventArgs e)
        {
            try
            {
                ConstantVal.check_ts = false;
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "parentButtonClick('btn_CloseTokuisakiSentaku','" + hdnHome.Value + "');", true);

            }
            catch
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "parentButtonClick('btnToLogin','" + hdnHome.Value + "');", true);
            }
        }
        #endregion

        #region btncancel_Click
        protected void btncancel_Click(object sender, EventArgs e)
        {
            try
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "parentButtonClick('btn_CloseTokuisakiSentaku','" + hdnHome.Value + "');", true);

            }
            catch
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "parentButtonClick('btnToLogin','" + hdnHome.Value + "');", true);
            }
        }
        #endregion

        #region btnCreate_Click
        protected void btnCreate_Click(object sender, EventArgs e)
        {
            Session["cTokuisakiBukken"] = null;
            Session["fTokuisakiSyosai"] = "Popup";
            SessionUtility.SetSession("HOME", "Popup");

            String id = Request.QueryString["id"];
            ifShinkiPopup.Src = "JC19TokuisakiSyousai.aspx?id=" + id;
            mpeShinkiPopup.Show();
            updShinkiPopup.Update();
            updBody.Update();
            //Response.Redirect("../WebFront/JC19TokuisakiSyousai.aspx");
            //Response.Write("<script language='javascript'>window.open('JC19TokuisakiSyousai.aspx', '_blank');</script>");
        }
        #endregion

        #region grid_touisaki_Sorting
        protected void grid_touisaki_Sorting(object sender, GridViewSortEventArgs e)
        {
            string sortExpression = e.SortExpression;
            ViewState["z_sortexpresion"] = e.SortExpression;
            if (GridViewSortDirection == SortDirection.Ascending)
            {
                GridViewSortDirection = SortDirection.Descending;
                SortGridView(sortExpression, "DESC");
            }
            else
            {
                GridViewSortDirection = SortDirection.Ascending;
                SortGridView(sortExpression, "ASC");
            }
        }
        #endregion

        #region SortGridView
        private void SortGridView(string sortExpression, string direction)
        {
            dt.Clear();

            dt = (DataTable)ViewState["Row"];
            dt1 = dt.Copy();
            //if (dt.Rows[0][1].ToString() == "")
            //{
            //    dt.Rows.Remove(dt.Rows[0]);
            //}
            if (dt1.Rows.Count > 0)
            {
                DataView dv = new DataView(dt);
                dv.Sort = sortExpression + " " + direction;
                dt1.Rows.Clear();
                //dt1.Rows.Add(dt1.NewRow());
                foreach (DataRow dr in dv.ToTable().Rows)
                {
                    dt1.Rows.Add(dr.ItemArray);
                }
                grid_touisaki.DataSource = dt1;
                grid_touisaki.DataBind();
                updBody.Update();
            }
        }
        #endregion

        #region SortExpression
        public string SortExpression
        {
            get
            {
                if (ViewState["z_sortexpresion"] == null)
                    ViewState["z_sortexpresion"] = this.grid_touisaki.DataKeyNames[0].ToString();
                return ViewState["z_sortexpresion"].ToString();
            }
            set
            {
                ViewState["z_sortexpresion"] = value;
            }
        }
        #endregion

        #region GridViewSortDirection
        public SortDirection GridViewSortDirection
        {
            get
            {
                if (ViewState["sortDirection"] == null)
                    ViewState["sortDirection"] = SortDirection.Ascending;
                return (SortDirection)ViewState["sortDirection"];
            }
            set
            {
                ViewState["sortDirection"] = value;
            }
        }

        #endregion

        #region txtCode_TextChanged
        protected void txtCode_TextChanged(object sender, EventArgs e)
        {
            BindGridView();
        }


        #endregion

        #region grid_touisaki_RowCreated
        protected void grid_touisaki_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (ViewState["sortDirection"] != null && ViewState["z_sortexpresion"] != null)

            {

                if (e.Row.RowType == DataControlRowType.Header)

                {

                    foreach (TableCell tablecell in e.Row.Cells)

                    {

                        if (tablecell.HasControls())

                        {

                            LinkButton sortLinkButton = null;

                            if (tablecell.Controls[0] is LinkButton)
                            {
                                sortLinkButton = (LinkButton)tablecell.Controls[0];

                            }

                            if (sortLinkButton != null && ViewState["z_sortexpresion"].ToString() == sortLinkButton.CommandArgument)
                            {
                                Image img = new Image();
                                img.Width = 20;

                                if (GridViewSortDirection == SortDirection.Ascending)
                                {

                                    img.ImageUrl = "../Img/expand-less-1781206-1518580.png";
                                }
                                else
                                {
                                    img.ImageUrl = "../Img/expand-more-1782315-1514165.png";
                                }

                                //tablecell.Controls.Add(new LiteralControl("&nbsp;"));

                                tablecell.Controls.Add(img);
                                break;
                            }
                        }

                    }

                }

            }
        }
        #endregion

        #region grid_touisaki_RowDataBound
        protected void grid_touisaki_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //e.Row.Attributes.Add("cTOUKUISAKI", e.Row.Cells[0].Text);
            //e.Row.Attributes.Add("onclick", "rowClick('" + e.Row.RowIndex + "')");
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.grid_touisaki, "Select$" + e.Row.RowIndex);
                }
            }
        }
        #endregion

        #region btn_Close_Click
        protected void btn_Close_Click(object sender, EventArgs e)
        {
            if (ConstantVal.check_ts == true)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "parentButtonClick('btnSeikyusakiSelect','" + hdnHome.Value + "');", true);
                ConstantVal.check_ts = false;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "parentButtonClick('btnTokuisakiSelect','" + hdnHome.Value + "');", true);
            }
        }
        #endregion

        #region grid_touisaki_SelectedIndexChanged
        protected void grid_touisaki_SelectedIndexChanged(object sender, EventArgs e)
        {
            int i = grid_touisaki.SelectedRow.RowIndex;
            Label ctoukuisaki = (Label)grid_touisaki.Rows[i].FindControl("lblcToukuisaki");
            Label stoukuisaki = (Label)grid_touisaki.Rows[i].FindControl("lblsToukuisaki");

            Session["cTOUKUISAKI"] = ctoukuisaki.Text;
            Session["sTOUKUISAKI"] = stoukuisaki.Text;

            if (ConstantVal.check_ts == true)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "parentButtonClick('btnSeikyusakiSelect','" + hdnHome.Value + "');", true);
                ConstantVal.check_ts = false;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "parentButtonClick('btnTokuisakiSelect','" + hdnHome.Value + "');", true);
            }
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "parentButtonClick('btnTokuisakiSelect','" + hdnHome.Value + "');", true);
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "parentButtonClick('btnSeikyusakiSelect','" + hdnHome.Value + "');", true);




        }
        #endregion

        #region btnToLogin_Click
        protected void btnToLogin_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "parentButtonClick('btnToLogin','" + hdnHome.Value + "');", true);
        }
        #endregion

        #region grid_touisaki_PreRender
        protected void grid_touisaki_PreRender(object sender, EventArgs e)
        {
            string[] columns = HF_GridSize.Value.Split(',');
            grid_touisaki.Columns[0].HeaderStyle.Width = Unit.Pixel((int)Math.Round(Convert.ToDecimal(columns[0])));
            grid_touisaki.Columns[1].HeaderStyle.Width = Unit.Pixel((int)Math.Round(Convert.ToDecimal(columns[1])));
            grid_touisaki.Columns[2].HeaderStyle.Width = Unit.Pixel((int)Math.Round(Convert.ToDecimal(columns[2])));
            grid_touisaki.Columns[3].HeaderStyle.Width = Unit.Pixel((int)Math.Round(Convert.ToDecimal(columns[3])));
            grid_touisaki.Columns[4].HeaderStyle.Width = Unit.Pixel((int)Math.Round(Convert.ToDecimal(columns[4])));
            grid_touisaki.Columns[5].HeaderStyle.Width = Unit.Pixel((int)Math.Round(Convert.ToDecimal(columns[5])));
            grid_touisaki.Columns[6].HeaderStyle.Width = Unit.Pixel((int)Math.Round(Convert.ToDecimal(columns[6])));

            grid_touisaki.Width = Unit.Pixel((int)Math.Round(Convert.ToDecimal(HF_Grid.Value)));


            Response.Cookies["colWidthbTokuisaki"].Value = HF_GridSize.Value;
            Response.Cookies["colWidthbTokuisaki"].Expires = DateTime.Now.AddYears(1);
            Response.Cookies["colWidthbTokuisakiGrid"].Value = HF_Grid.Value;
            Response.Cookies["colWidthbTokuisakiGrid"].Expires = DateTime.Now.AddYears(1);
        }
        #endregion

        #region BT_ColumnWidth_Click
        protected void BT_ColumnWidth_Click(object sender, EventArgs e)
        {
            Response.Cookies["colWidthbTokuisaki"].Value = HF_GridSize.Value;
            Response.Cookies["colWidthbTokuisaki"].Expires = DateTime.Now.AddYears(1);
            Response.Cookies["colWidthbTokuisakiGrid"].Value = HF_Grid.Value;
            Response.Cookies["colWidthbTokuisakiGrid"].Expires = DateTime.Now.AddYears(1);
        }
        #endregion


        #region btn_CloseShinkiTokui_Click
        protected void btn_CloseShinkiTokui_Click(object sender, EventArgs e)
        {
            ifShinkiPopup.Src = "";
            mpeShinkiPopup.Hide();
            updShinkiPopup.Update();
            updBody.Update();
        }
        #endregion

    }
}