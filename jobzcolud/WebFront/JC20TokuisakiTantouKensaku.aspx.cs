using Common;
using MySql.Data.MySqlClient;
using Service;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JobzCloud.WebFront
{
    public partial class JC20TokuisakiTantouKensaku : System.Web.UI.Page
    {
        public Double totalDataCount = 0;
        MySqlConnection cn = null;
        String cTokuisaki,Stantou = "";
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

                        if (Session["cTokuisakiBukken"] != null)
                        {
                            cTokuisaki = Session["cTokuisakiBukken"].ToString();
                            txt_Code.Text = cTokuisaki;
                           
                            //20220307 Added　エインドリ－ start
                            if (Request.Cookies["colWidthTokuiTantou"] != null)
                            {
                                HF_GridSize.Value = Request.Cookies["colWidthTokuiTantou"].Value;
                            }
                            else
                            {
                                HF_GridSize.Value = "260, 200, 150, 150, 150, 205";
                            }
                            if (Request.Cookies["colWidthTokuiTantouGrid"] != null)
                            {
                                HF_Grid.Value = Request.Cookies["colWidthTokuiTantouGrid"].Value;
                            }
                            else
                            {
                                HF_Grid.Value = "1093";
                            }
                            //20220307 Added　エインドリ－ End
                            this.BindToukuisakiGrid();
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "parentButtonClick('btnToLogin','" + hdnHome.Value + "');", true);
                        }
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

        #region BindToukuisakiGrid
        protected void BindToukuisakiGrid()
        {
            try
            {
                #region toDelete
                //String qr = "select ifnull(STANTOU, '') as STANTOU,"
                //          + " ifnull(NJUNBAN, '') as NJUNBAN,"
                //          + " ifnull(SBUMON, '') as SBUMON,"
                //          + " ifnull(SYAKUSHOKU, '') as SYAKUSHOKU,"
                //          + " ifnull(STEL, '') as STEL,"
                //          + " ifnull(SMAIL, '') as SMAIL,"
                //          + " ifnull(SBIKOU, '') as SBIKOU"
                //          + " from tokuisaki_tantousha";
                #endregion
                String qr = "select Replace(Replace(ifnull(STANTOU, ''),'<','&lt'),'>','&gt') as STANTOU,"
                          + " ifnull(NJUNBAN, '') as NJUNBAN,"
                          + " Replace(Replace(ifnull(SBUMON, ''),'<','&lt'),'>','&gt') as SBUMON,"
                          + " Replace(Replace(ifnull(SYAKUSHOKU, ''),'<','&lt'),'>','&gt') as SYAKUSHOKU,"
                          + " ifnull(STEL, '') as STEL,"
                          + " ifnull(SMAIL, '') as SMAIL,"
                          + " Replace(Replace(ifnull(SBIKOU, ''),'<','&lt'),'>','&gt') as SBIKOU,"
                          + " Replace(Replace(ifnull(SKEISHOU, ''),'<','&lt'),'>','&gt') as SKEISHOU"
                          + " from tokuisaki_tantousha"
                          + " Where 1=1 ";

                #region toDelete
                //if (cTokuisaki != null)
                //{
                //    qr += " Where CTOKUISAKI='" + cTokuisaki + "' order by NJUNBAN";
                //}
                //else if (Stantou != null && Stantou != "")
                //{
                //    qr += " where STANTOU COLLATE utf8_unicode_ci LIKE '" + '%' + Stantou + '%' + "' ";
                //}
                #endregion

                if (!String.IsNullOrEmpty(txt_Code.Text))
                {
                    String code = "";
                    try
                    {
                        code = Convert.ToDouble(txt_Code.Text).ToString();
                    }
                    catch
                    {
                        code = "'" + txt_Code.Text + "'";
                    }
                    qr += "AND (CTOKUISAKI =" + code + " or STANTOU COLLATE utf8_unicode_ci LIKE '" + '%' + txt_Code.Text + '%' + "' or STEL COLLATE utf8_unicode_ci LIKE '" + '%' + txt_Code.Text + '%' + "') ";
                }
                qr += " order by NJUNBAN";

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

                //dt.Rows.Add(dt.NewRow());
                da.Fill(dt);
                cn.Close();
                da.Dispose();
                ViewState["Row"] = dt;
                ViewState["sortDirection"] = SortDirection.Ascending;
                ViewState["z_sortexpresion"] = "STANTOU";
                if (dt.Rows.Count == 0)
                {
                    lblDataNai.Visible = true;
                }
                else
                {
                    lblDataNai.Visible = false;
                }
                gdvtokuisaki.DataSource = dt;
                gdvtokuisaki.DataBind();
                totalDataCount = dt.Rows.Count;
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
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "parentButtonClick('btn_CloseTokuisakiSentaku','" + hdnHome.Value+"');", true);
                
            }
            catch
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "parentButtonClick('btnToLogin','" + hdnHome.Value + "');", true);
            }
        }
        #endregion

        #region btnFusenHeaderCross_Click
        protected void btnFusenHeaderCross_Click(object sender, EventArgs e)
        {
            try
            {
                ConstantVal.check_ts = false;
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "parentButtonClick('btn_CloseTokuisakiSentaku','" + hdnHome.Value+"');", true);
                
            }
            catch
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "parentButtonClick('btnToLogin','" + hdnHome.Value + "');", true);
            }
        }
        #endregion

        #region lnkCode_Click
        protected void lnkCode_Click(object sender, EventArgs e)
        {
            LinkButton lk=sender as LinkButton;
            GridViewRow gvRow = (GridViewRow)lk.NamingContainer;
            int i = gvRow.RowIndex;
            String njun = (gvRow.FindControl("lb_nJun") as Label).Text;
            //String bumon = (gvRow.Cells[i].FindControl("lb_bumon") as Label).Text;
            String bumon = (gvRow.FindControl("lb_bumon") as Label).Text;  //20211029 MiMi Updated
            String yakushoku = (gvRow.FindControl("lb_yakushoku") as Label).Text;
            String keishou = (gvRow.FindControl("lb_keishou") as Label).Text;
            Session["TOUKUISAKITANTOU"] = lk.Text;
            Session["TokuisakiTanJun"] = njun;
            Session["SBUMON"] = bumon;
            Session["SYAKUSHOKU"] = yakushoku;
            Session["SKEISHOU"] = keishou;
            if (ConstantVal.check_ts == true)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "parentButtonClick('btnSeikyusakiTantouSelect','" + hdnHome.Value + "');", true);
                ConstantVal.check_ts = false;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "parentButtonClick('btnTokuisakiTantouSelect','" + hdnHome.Value + "');", true);
            }
          
        }
        #endregion

        #region btn_Create_Click
        protected void btn_Create_Click(object sender, EventArgs e)
        {
            //Response.Redirect("../WebFront/JC19TokuisakiSyousai.aspx");
            //Response.Write("<script language='javascript'>window.open('JC19TokuisakiSyousai.aspx', '_blank');</script>");
            Session["fTokuisakiSyosai"] = "Popup";
            SessionUtility.SetSession("HOME", "Popup");

            String id = Request.QueryString["id"];
            ifShinkiPopup.Src = "JC19TokuisakiSyousai.aspx?id=" + id;
            mpeShinkiPopup.Show();
            updShinkiPopup.Update();
        }
        #endregion

        #region isNumeric
        public static bool isNumeric(string s)
        {
            return int.TryParse(s, out int n);
        }
        #endregion

        #region gdvtokuisaki_Sorting
        protected void gdvtokuisaki_Sorting(object sender, GridViewSortEventArgs e)
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
                gdvtokuisaki.DataSource = dt1;
                gdvtokuisaki.DataBind();
            }
        }
        #endregion

        #region SortExpression
        public string SortExpression
        {
            get
            {
                if (ViewState["z_sortexpresion"] == null)
                    ViewState["z_sortexpresion"] = this.gdvtokuisaki.DataKeyNames[0].ToString();
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

        #region gdvtokuisaki_RowCreated
        protected void gdvtokuisaki_RowCreated(object sender, GridViewRowEventArgs e)
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
                                img.ID = sortLinkButton.CommandArgument;
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

        #region txt_Code_TextChanged
        protected void txt_Code_TextChanged(object sender, EventArgs e)
        {
            BindToukuisakiGrid();
        }
        #endregion

        #region gdvtokuisaki_selectedIndexChanged
        protected void gdvtokuisaki_selectedIndexChanged(object sender,EventArgs e)
        {
            int i = gdvtokuisaki.SelectedRow.RowIndex;
            Label stoukuisakitantou = (Label)gdvtokuisaki.Rows[i].FindControl("lb_stantou");
            Label tokuisakitanjun = (Label)gdvtokuisaki.Rows[i].FindControl("lb_nJun");
            Label sbumon = (Label)gdvtokuisaki.Rows[i].FindControl("lb_bumon");
            Label syakushoku = (Label)gdvtokuisaki.Rows[i].FindControl("lb_yakushoku");
            Label skeishou = (Label)gdvtokuisaki.Rows[i].FindControl("lb_keishou");
            Session["TOUKUISAKITANTOU"] = stoukuisakitantou.Text;
            Session["TokuisakiTanJun"] = tokuisakitanjun.Text;
            Session["SBUMON"] =sbumon.Text;
            Session["SYAKUSHOKU"] = syakushoku.Text;
            Session["SKEISHOU"] =skeishou.Text;
            if (ConstantVal.check_ts == true)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "parentButtonClick('btnSeikyusakiTantouSelect','" + hdnHome.Value + "');", true);
                ConstantVal.check_ts = false;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "parentButtonClick('btnTokuisakiTantouSelect','" + hdnHome.Value + "');", true);
            }
        }
        #endregion

        #region gdvtokuisaki_rowDataBound
        protected void gdvtokuisaki_rowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.gdvtokuisaki, "Select$" + e.Row.RowIndex);
                }
            }
        }
        #endregion

        #region btn_Close_Click
        protected void btn_Close_Click(object sender, EventArgs e)
        {
            if (Session["STANTOUCarry"] != null)
            {
                Session["TOUKUISAKITANTOU"] = (string)Session["STANTOUCarry"];
                Session["TokuisakiTanJun"] = "1";
                if (ConstantVal.check_ts == true)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "parentButtonClick('btnSeikyusakiTantouSelect','" + hdnHome.Value + "');", true);
                    ConstantVal.check_ts = false;
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "parentButtonClick('btnTokuisakiTantouSelect','" + hdnHome.Value + "');", true);
                }
            }
            else
            {
                ifShinkiPopup.Src = "";
                mpeShinkiPopup.Hide();
                updShinkiPopup.Update();
                BindToukuisakiGrid();
                updBody.Update();
            }
        }
        #endregion

        #region btnToLogin_Click
        protected void btnToLogin_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "parentButtonClick('btnToLogin','" + hdnHome.Value + "');", true);
        }
        #endregion

        #region gdvtokuisaki_PreRender
        protected void gdvtokuisaki_PreRender(object sender, EventArgs e)
        {
            string[] columns = HF_GridSize.Value.Split(',');
            gdvtokuisaki.Columns[0].HeaderStyle.Width = Unit.Pixel((int)Math.Round(Convert.ToDecimal(columns[0])));
            gdvtokuisaki.Columns[1].HeaderStyle.Width = Unit.Pixel((int)Math.Round(Convert.ToDecimal(columns[1])));
            gdvtokuisaki.Columns[2].HeaderStyle.Width = Unit.Pixel((int)Math.Round(Convert.ToDecimal(columns[2])));
            gdvtokuisaki.Columns[3].HeaderStyle.Width = Unit.Pixel((int)Math.Round(Convert.ToDecimal(columns[3])));
            gdvtokuisaki.Columns[4].HeaderStyle.Width = Unit.Pixel((int)Math.Round(Convert.ToDecimal(columns[4])));
            gdvtokuisaki.Columns[5].HeaderStyle.Width = Unit.Pixel((int)Math.Round(Convert.ToDecimal(columns[5])));

            gdvtokuisaki.Width = Unit.Pixel((int)Math.Round(Convert.ToDecimal(HF_Grid.Value)));

            Response.Cookies["colWidthTokuiTantou"].Value = HF_GridSize.Value;
            Response.Cookies["colWidthTokuiTantou"].Expires = DateTime.Now.AddYears(1);
            Response.Cookies["colWidthTokuiTantouGrid"].Value = HF_Grid.Value;
            Response.Cookies["colWidthTokuiTantouGrid"].Expires = DateTime.Now.AddYears(1);
        }
        #endregion

        #region BT_ColumnWidth_Click
        protected void BT_ColumnWidth_Click(object sender, EventArgs e)
        {
            Response.Cookies["colWidthTokuiTantou"].Value = HF_GridSize.Value;
            Response.Cookies["colWidthTokuiTantou"].Expires = DateTime.Now.AddYears(1);
            Response.Cookies["colWidthTokuiTantouGrid"].Value = HF_Grid.Value;
            Response.Cookies["colWidthTokuiTantouGrid"].Expires = DateTime.Now.AddYears(1);
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