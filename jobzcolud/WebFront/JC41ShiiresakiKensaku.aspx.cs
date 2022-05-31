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
    public partial class JC41HyoujunShiiresakiKensaku : System.Web.UI.Page
    {
        #region Declaration
        public Double totalDataCount = 0;
        MySqlConnection cn = null;
        public int rowindex = 0;
        string sort_expression = "";
        string sort_direction = "";
        string shiireId = "";
        string searchAllData = "";
        #endregion
        #region Page_Load
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
                        ViewState["sortDirection"] = SortDirection.Descending;
                        ViewState["z_sortexpresion"] = "cSHIIRESAKI";
                        sort_expression = "cSHIIRESAKI";
                        sort_direction = "DESC";
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
                        this.BindGrid();
                    }
                    else
                    {

                        if (ViewState["sortDirection"] != null && ViewState["z_sortexpresion"] != null)
                        {
                            if (ViewState["sortDirection"].ToString() == "Ascending")
                            {
                                sort_direction = "ASC";
                            }
                            else
                            {
                                sort_direction = "DESC";
                            }
                            sort_expression = ViewState["z_sortexpresion"].ToString();
                        }
                        else
                        {
                            sort_expression = "cSHIIRESAKI";
                            sort_direction = "DESC";
                        }

                    }

                    Session.Timeout = 480;
                    // this.BindGrid();
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
        #endregion Page_Load
        #region BindGrid
        protected void BindGrid()
        {
            searchAllData += "select cSHIIRESAKI,sSHIIRESAKI,skSHIIRESAKI,sSHIIRESAKI_R,skSHIIRESAKI_R,cYUUBIN,sTEL,sFAX,sJUUSHO1,sJUUSHO2";
            searchAllData += " FROM  m_shiiresaki ";
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
                searchAllData += "where (cSHIIRESAKI =" + code + " or sSHIIRESAKI COLLATE utf8_unicode_ci LIKE '" + '%' + txtCode.Text + '%' + "') ";

            }

            searchAllData += " order by " + sort_expression + " " + sort_direction + ";";


            ExecuteQuery(searchAllData);
        }
        #endregion BindGrid
        #region DeleteGrid
        protected void DeletGrid()
        {
            searchAllData = "delete from m_shiiresaki where m_shiiresaki.cSHIIRESAKI=" + "'" + shiireId + "'";
            searchAllData += " order by " + sort_expression + " " + sort_direction + ";";
            ExecuteQuery(searchAllData);
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
            MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Close();
                DataTable dt = new DataTable();
                da.Fill(dt);
                cn.Close();
                da.Dispose();
                dgShiire.DataSource = dt;
                dgShiire.DataBind();
                totalDataCount = dt.Rows.Count;
                ViewState["dt_Mitumori"] = dt;
                lblDataNai.Visible = false;
            }
            else
            {
                dr.Close();
                DataTable dt = new DataTable();
                da.Fill(dt);
                cn.Close();
                da.Dispose();
                dgShiire.DataSource = dt;
                dgShiire.DataBind();
                totalDataCount = dt.Rows.Count;
                ViewState["dt_Mitumori"] = dt;
                lblDataNai.Visible = true;

            }
           

        }
        #endregion
        #region btnSearch_Click
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            this.BindGrid();
        }
        #endregion btnSearch_Click
        #region lblcTantousha_Click
        protected void lblcTantousha_Click(object sender, EventArgs e)
        {
            var shiriId = (LinkButton)sender;
            Session["cSHIIRESAKI"] = shiriId.Text;
            GridViewRow gvr = (GridViewRow)shiriId.NamingContainer;//20220323 ルインマー　added
            string sshire = (gvr.FindControl("lblsTantousha") as Label).Text;//20220323 ルインマー　added
            Session["sSHIIRESAKI"] = sshire;//20220323 ルインマー　added
            if (Session["cSHIIRESAKI"] != null)
            {
                // Response.Redirect("JC37Shohin.aspx");
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "parentButtonClick('btn_SHIIRESAKISelect','Master');", true);//20220323 ルインマー　added
            }

        }
        #endregion
        #region btnShiresaki_Close 20220323 addede by ルインマー
        protected void btnShiresaki_Close_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "parentButtonClick('btn_SHIIRESAKISelect','Master');", true);
        }
        #endregion
        #region btnShireHeaderCross_Click
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "parentButtonClick('btnClose','" + hdnHome.Value + "');", true);
            //try
            //{
            //    ConstantVal.check_ts = false;
            //    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "parentButtonClick('btn_CloseShinkiSentaku','" + hdnHome.Value + "');", true);

            //}
            //catch
            //{
            //    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "parentButtonClick('btnToLogin','" + hdnHome.Value + "');", true);
            //}
        }
        #endregion
        #region btnToLogin_Click
        protected void btnToLogin_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "parentButtonClick('btnToLogin','" + hdnHome.Value + "');", true);
        }
        #endregion
        #region txtCode_TextChanged
        protected void txtCode_TextChanged(object sender, EventArgs e)
        {
            BindGrid();
        }
        #endregion
        #region btnCreate_Click
        protected void btnCreate_Click(object sender, EventArgs e)
        {
            Session["cSHIIRESAKI"] = null;
            Session["fSyouhinSyosai"] = "Popup";
            SessionUtility.SetSession("HOME", "Popup");
            String id = Request.QueryString["id"];
            ifShinkiPopup.Src = "JC40Shiiresaki.aspx?id=" + id;
            mpeShinkiPopup.Show();
            updShinkiPopup.Update();
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
        #region btn_Close_Click
        protected void btn_Close_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "parentButtonClick('btnClose','Master');", true);
        }
        #endregion
        #region btnDeleteShinki_Click
        protected void btnDeleteShinki_Click(object sender, EventArgs e)
        {

            //ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowAnkenChangeMessage",
            //    "ShowKoumokuChangesConfirmMessage('項目が変更されています。保存しますか？','" + btnYes.ClientID + "','" + btnNo.ClientID + "','" + btnCancell.ClientID + "');", true);
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
        #region SortGridView
        private void SortGridView(string sortExpression, string direction)
        {
            sort_expression = sortExpression;
            sort_direction = direction;
            BindGrid();
            Updshire.Update();
        }
        #endregion
        #region SortExpression
        public string SortExpression
        {
            get
            {
                if (ViewState["z_sortexpresion"] == null)
                    ViewState["z_sortexpresion"] = this.dgShiire.DataKeyNames[0].ToString();
                return ViewState["z_sortexpresion"].ToString();
            }
            set
            {
                ViewState["z_sortexpresion"] = value;
            }

        }
        #endregion
        #region dgShiire_Sorting1
        protected void dgShiire_Sorting1(object sender, GridViewSortEventArgs e)
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
        #region lnkbtnShiireDelete_Click_Click
        protected void lnkbtnShiireDelete_Click_Click(object sender, EventArgs e)
        {
            GridViewRow gvrow = (sender as LinkButton).NamingContainer as GridViewRow;
            string tantoushaId = (gvrow.FindControl("lblcTantousha") as LinkButton).Text;
            HF_cTantousha.Value = tantoushaId;
            ScriptManager.RegisterStartupScript(this, GetType(), "DeleteConfirmMessage", "DeleteConfirmBox('削除してもよろしいでしょうか？','" + btnDeleteShiire.ClientID + "');", true);
            Updshire.Update();

        }
        #endregion lnkbtnShiireDelete_Click_Click
        #region dgShiire_RowCreated
        protected void dgShiire_RowCreated(object sender, GridViewRowEventArgs e)
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
        #region dgShiire_RowDeleting
        protected void dgShiire_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            shiireId = dgShiire.DataKeys[e.RowIndex].Value.ToString();
            this.BindGrid();
        }
        #endregion
        #region btnDeleteShiire_Click
        protected void btnDeleteShiire_Click(object sender, EventArgs e)
        {
            shiireId = HF_cTantousha.Value;
            this.DeletGrid();
            this.BindGrid();
            Updshire.Update();
        }
        #endregion btnDeleteShiire_Click
        #region dgShiire_PreRender
        protected void dgShiire_PreRender(object sender, EventArgs e)
        {
            string[] columns = HF_GridSize.Value.Split(',');
            dgShiire.Columns[0].HeaderStyle.Width = Unit.Pixel((int)Math.Round(Convert.ToDecimal(columns[0])));
            dgShiire.Columns[1].HeaderStyle.Width = Unit.Pixel((int)Math.Round(Convert.ToDecimal(columns[1])));
            dgShiire.Columns[2].HeaderStyle.Width = Unit.Pixel((int)Math.Round(Convert.ToDecimal(columns[2])));
            dgShiire.Columns[3].HeaderStyle.Width = Unit.Pixel((int)Math.Round(Convert.ToDecimal(columns[3])));
            dgShiire.Columns[4].HeaderStyle.Width = Unit.Pixel((int)Math.Round(Convert.ToDecimal(columns[4])));
            dgShiire.Columns[5].HeaderStyle.Width = Unit.Pixel((int)Math.Round(Convert.ToDecimal(columns[5])));


            dgShiire.Width = Unit.Pixel((int)Math.Round(Convert.ToDecimal(HF_Grid.Value)));


            Response.Cookies["colWidthbTokuisaki"].Value = HF_GridSize.Value;
            Response.Cookies["colWidthbTokuisaki"].Expires = DateTime.Now.AddYears(1);
            Response.Cookies["colWidthbTokuisakiGrid"].Value = HF_Grid.Value;
            Response.Cookies["colWidthbTokuisakiGrid"].Expires = DateTime.Now.AddYears(1);
        }
        #endregion dgShiire_PreRender
        #region dgShiire_RowDataBound
        protected void dgShiire_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.dgShiire, "Select$" + e.Row.RowIndex);
                }
            }
        }
        #endregion dgShiire_RowDataBound
        #region BT_ColumnWidth_Click
        protected void BT_ColumnWidth_Click(object sender, EventArgs e)
        {
            Response.Cookies["colWidthbTokuisaki"].Value = HF_GridSize.Value;
            Response.Cookies["colWidthbTokuisaki"].Expires = DateTime.Now.AddYears(1);
            Response.Cookies["colWidthbTokuisakiGrid"].Value = HF_Grid.Value;
            Response.Cookies["colWidthbTokuisakiGrid"].Expires = DateTime.Now.AddYears(1);
        }
        #endregion BT_ColumnWidth_Click
        #region btnClose_Click
        protected void btnClose_Click(object sender, EventArgs e)
        {
            ifShinkiPopup.Src = "";
            mpeShinkiPopup.Hide();
            updShinkiPopup.Update();
        }
        #endregion btnClose_Click
        #region lnkbtnShiireEdit_Click
        protected void lnkbtnShiireEdit_Click(object sender, EventArgs e)
        {
            GridViewRow gvrow = (sender as LinkButton).NamingContainer as GridViewRow;
            string tantoushaId = (gvrow.FindControl("lblcTantousha") as LinkButton).Text;
            Session["cSHIIRESAKI"] = tantoushaId;
            Session["fSyouhinSyosai"] = "Popup";
            SessionUtility.SetSession("HOME", "Popup");
            String id = Request.QueryString["id"];
            ifShinkiPopup.Src = "JC40Shiiresaki.aspx?id=" + id;
            mpeShinkiPopup.Show();
            updShinkiPopup.Update();
        }
        #endregion lnkbtnShiireEdit_Click
        #region dgShiire_SelectedIndexChanged
        protected void dgShiire_SelectedIndexChanged(object sender, EventArgs e)
        {
            int i = dgShiire.SelectedRow.RowIndex;
            Label ctoukuisaki = (Label)dgShiire.Rows[i].FindControl("lblcT");
            Label stoukuisaki = (Label)dgShiire.Rows[i].FindControl("lblsTantousha");

            Session["cSHIIRESAKI"] = ctoukuisaki.Text;
            Session["sSHIIRESAKI"] = stoukuisaki.Text;
            if (Session["cSHIIRESAKI"] != null)
            {
                // Response.Redirect("JC37Shohin.aspx");
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "parentButtonClick('btn_SHIIRESAKISelect','Master');", true);
            }

        }
        #endregion dgShiire_SelectedIndexChanged
    }
}




