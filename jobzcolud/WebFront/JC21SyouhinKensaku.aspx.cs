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
    public partial class JC21SyouhinKensaku : System.Web.UI.Page
    {
        public Double totalDataCount = 0;
        MySqlConnection cn = null;
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

                        if (Session["syohinSelectRowIndex"] != null)
                        {
                            HF_rowindex.Value = (string)Session["syohinSelectRowIndex"];
                            SessionUtility.SetSession("HOME", null);
                        }

                        //20220307 Added　エインドリ－ start
                        if (Request.Cookies["colWidthSyouhin"] != null)
                        {
                            HF_GridSize.Value = Request.Cookies["colWidthSyouhin"].Value;
                        }
                        else
                        {
                            HF_GridSize.Value = "100,200,70,70,120,120,130,130,172";
                        }
                        if (Request.Cookies["colWidthSyouhinGrid"] != null)
                        {
                            HF_Grid.Value = Request.Cookies["colWidthSyouhinGrid"].Value;
                        }
                        else
                        {
                            HF_Grid.Value = "1093";
                        }
                        //20220307 Added　エインドリ－ End

                        this.BindShouHinGrid();

                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "parentButtonClick('btnToLogin','" + hdnHome.Value + "');", true);
                }
                Session.Timeout = 480;
            }
            catch
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "parentButtonClick('btnToLogin','" + hdnHome.Value + "');", true);
            }
        }

        #region BindShouHinGrid
        protected void BindShouHinGrid()
        {
            try
            {
                String qr = "Select ifnull( m.cSYOUHIN , '') as CSYOUHIN,"
                    + "ifnull (m.sSYOUHIN, '') as sSYOUHIN, "
                    + "ifnull(m.nSYOUKISU, '') as nSYOUKISU,"
                    + "ifnull( m.sTANI, '') as sTANI,"
                    + "ifnull(  m.nHANNBAIKAKAKU, '') as nHANNBAIKAKAKU,"
                    + "ifnull(  m.nSHIIREKAKAKU, '') as nSHIIREKAKAKU,"
                    + "ifnull(    d.sSYOUHIN_DAIGRP, '') as sSYOUHIN_DAIGRP,"
                    + "ifnull(  t.sSYOUHIN_TYUUGRP, '') as sSYOUHIN_TYUUGRP,"
                    + "ifnull(  m.sBIKOU, '') as sBIKOU"
                    + " from m_syouhin as m"
                    + " Left JOIN m_syouhin_daigrp as d ON d.cSYOUHIN_DAIGRP = m.cSYOUHIN_DAIGRP"
                    + " left join m_syouhin_tyuugrp as t ON t.cSYOUHIN_TYUUGRP = m.cSYOUHIN_TYUUGRP Where (m.fHAIBAN <>'1' or m.fHAIBAN is null)";
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
                    qr += "AND (m.cSYOUHIN =" + code + " or m.sSYOUHIN COLLATE utf8_unicode_ci LIKE '" + '%' + txtCode.Text + '%' + "')";
                }
                qr += " order by d.nJUNBAN asc,t.nJUNBAN asc,m.nJUNBAN asc , m.cSYOUHIN asc;";

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
                dt = new DataTable();
                //dt.Rows.Add(dt.NewRow());
                da.Fill(dt);
                cn.Close();
                ViewState["Row"] = dt;
                ViewState["sortDirection"] = SortDirection.Ascending;
                ViewState["z_sortexpresion"] = "cSYOUHIN";
                if (dt.Rows.Count == 0)
                {
                    lblDataNai.Visible = true;
                }
                else
                {
                    lblDataNai.Visible = false;
                }
                gdvSyouhin.DataSource = dt;
                gdvSyouhin.DataBind();
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
            //ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "parentButtonClick('btnClose','" + hdnHome.Value + "');", true);    //20220503 MyatNoe Delete
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "parentButtonClick('btn_CloseTokuisakiSentaku','" + hdnHome.Value + "');", true);    //20220503 MyatNoe Add
        }
        #endregion

        #region lnkCode_Click
        protected void lnkCode_Click(object sender, EventArgs e)
        {
            var LkB = sender as LinkButton;
            var row = LkB.NamingContainer;
            Session["cSyohin"] = LkB.Text;
            Session["syohinSelectRowIndex"] = HF_rowindex.Value;

            if (ConstantVal.check_ts == true)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "parentButtonClick('btnSyohinGridSelect','" + hdnHome.Value + "');", true);
                ConstantVal.check_ts = false;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "parentButtonClick('btnSyohinGridSelect','" + hdnHome.Value + "');", true);
            }
            //ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "parentButtonClick('btnTokuisakiSelect','" + hdnHome.Value + "');", true);
            //ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "parentButtonClick('btnSeikyusakiSelect','" + hdnHome.Value + "');", true);
        }
        #endregion

        #region txtCode_TextChanged
        protected void txtCode_TextChanged(object sender, EventArgs e)
        {
            BindShouHinGrid();
        }
        #endregion

        #region gdvSyouhin_Sorting
        protected void gdvSyouhin_Sorting(object sender, GridViewSortEventArgs e)
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
                gdvSyouhin.DataSource = dt1;
                gdvSyouhin.DataBind();
            }
        }

        #endregion

        #region SortExpression
        public string SortExpression
        {
            get
            {
                if (ViewState["z_sortexpresion"] == null)
                    ViewState["z_sortexpresion"] = this.gdvSyouhin.DataKeyNames[0].ToString();
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

        #region gdvSyouhin_RowCreated
        protected void gdvSyouhin_RowCreated(object sender, GridViewRowEventArgs e)
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

        #region gdvSyouhin_PreRender
        protected void gdvSyouhin_PreRender(object sender, EventArgs e)
        {
            string[] columns = HF_GridSize.Value.Split(',');
            gdvSyouhin.Columns[0].HeaderStyle.Width = Unit.Pixel((int)Math.Round(Convert.ToDecimal(columns[0])));
            gdvSyouhin.Columns[1].HeaderStyle.Width = Unit.Pixel((int)Math.Round(Convert.ToDecimal(columns[1])));
            gdvSyouhin.Columns[2].HeaderStyle.Width = Unit.Pixel((int)Math.Round(Convert.ToDecimal(columns[2])));
            gdvSyouhin.Columns[3].HeaderStyle.Width = Unit.Pixel((int)Math.Round(Convert.ToDecimal(columns[3])));
            gdvSyouhin.Columns[4].HeaderStyle.Width = Unit.Pixel((int)Math.Round(Convert.ToDecimal(columns[4])));
            gdvSyouhin.Columns[5].HeaderStyle.Width = Unit.Pixel((int)Math.Round(Convert.ToDecimal(columns[5])));
            gdvSyouhin.Columns[6].HeaderStyle.Width = Unit.Pixel((int)Math.Round(Convert.ToDecimal(columns[6])));
            gdvSyouhin.Columns[7].HeaderStyle.Width = Unit.Pixel((int)Math.Round(Convert.ToDecimal(columns[7])));
            gdvSyouhin.Columns[8].HeaderStyle.Width = Unit.Pixel((int)Math.Round(Convert.ToDecimal(columns[8])));

            gdvSyouhin.Width = Unit.Pixel((int)Math.Round(Convert.ToDecimal(HF_Grid.Value)));

            Response.Cookies["colWidthSyouhin"].Value = HF_GridSize.Value;
            Response.Cookies["colWidthSyouhin"].Expires = DateTime.Now.AddYears(1);
            Response.Cookies["colWidthSyouhinGrid"].Value = HF_Grid.Value;
            Response.Cookies["colWidthSyouhinGrid"].Expires = DateTime.Now.AddYears(1);
        }
        #endregion

        #region BT_ColumnWidth_Click
        protected void BT_ColumnWidth_Click(object sender, EventArgs e)
        {
            Response.Cookies["colWidthSyouhin"].Value = HF_GridSize.Value;
            Response.Cookies["colWidthSyouhin"].Expires = DateTime.Now.AddYears(1);
            Response.Cookies["colWidthSyouhinGrid"].Value = HF_Grid.Value;
            Response.Cookies["colWidthSyouhinGrid"].Expires = DateTime.Now.AddYears(1);
        }
        #endregion

        #region 商品削除
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            GridViewRow gvrow = (sender as LinkButton).NamingContainer as GridViewRow;
            string cSyouhin = (gvrow.FindControl("lnkCode") as LinkButton).Text;
            HF_cSyouhin.Value = cSyouhin;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "DeleteConfirmMessage",
            "DeleteConfirmBox('削除してもよろしいでしょうか？','" + btnDeleteSyouhin.ClientID + "');", true);
            updHeader.Update();
        }

        protected void btnDeleteSyouhin_Click(object sender, EventArgs e)
        {
            String id = Request.QueryString["id"];
            DataTable dt_db = JC01Login_Class.Get_DB(id);
            String db = dt_db.Rows[0]["db"].ToString();
            String mail = dt_db.Rows[0]["mail"].ToString();
            ConstantVal.DB_NAME = db;

            JC21SyouhinKensaku_Class jc = new JC21SyouhinKensaku_Class();
            jc.loginId = mail;
            if (jc.IsExistMitumoriSyouhin(HF_cSyouhin.Value))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowErrorMessage",
                                "ShowErrorMessageWithWidth('相関商品データはありますので、削除できません。',400);", true);
            }
            else
            {
                String cSyouhin = HF_cSyouhin.Value;
                String qr = "Delete From m_syouhin_m where cSYOUHIN='"+cSyouhin+"';"
               + " Delete From m_syouhin_shiire where  cSYOUHIN = '"+cSyouhin+"';"
               + " Delete From h_syouhin where  cSYOUHIN = '"+cSyouhin+"';"
               + " Delete From h_syouhin_shiire where  cSYOUHIN = '"+cSyouhin+"';"
               + " Delete From m_syouhin Where '1' = '1'  And cSYOUHIN = '"+cSyouhin+"';"
               + " Delete From m_syouhin_kouteiyotei where cSYOUHIN = '"+cSyouhin+"'; ";

                MySqlTransaction tr = null;
                MySqlCommand cmdUpdate = new MySqlCommand();
                ConstantVal.DB_NAME = db;
                MySqlConnection con = jc.GetConnection();
                con.Open();
                try
                {
                    tr = con.BeginTransaction();
                    cmdUpdate.Transaction = tr;
                    cmdUpdate.CommandTimeout = 0;
                    cmdUpdate = new MySqlCommand(qr, con);
                    cmdUpdate.ExecuteNonQuery();
                    tr.Commit();

                    con.Close();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowSuccesMessage",
                                "ShowErrorMessageWithWidth('削除しました。',250);", true);

                    this.BindShouHinGrid();
                }
                catch (Exception ex)
                {
                    con.Close();
                    try
                    {
                        tr.Rollback();
                    }
                    catch
                    {
                    }
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowFailMessage",
                               "ShowErrorMessageWithWidth('削除できませんでした。',315);", true);
                }
            }
            
        }
        #endregion

        #region btnCreate_Click
        protected void btnCreate_Click(object sender, EventArgs e)
        {
            Session["fSyouhinSyosai"] = "Popup";
            SessionUtility.SetSession("HOME", "Popup");
            ifShouhinPopup.Style["width"] = "100vw";
            ifShouhinPopup.Style["height"] = "100vh";
            String id = Request.QueryString["id"];
            ifShouhinPopup.Src = "JC37Shohin.aspx?id=" + id;
            mpeShouhinPopup.Show();
            updShouhinPopup.Update();
        }
        #endregion

        #region btnSyouhinNew_Close_Click
        protected void btnSyouhinNew_Close_Click(object sender, EventArgs e)
        {
            if (Session["cSyohin"] != null)
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "parentButtonClick('btnSyohinGridSelect','" + hdnHome.Value + "');", true);

            }
            else
            {
                ifShouhinPopup.Src = "";
                mpeShouhinPopup.Hide();
                updShouhinPopup.Update();
            }
        }
        #endregion

        #region 商品編集
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            //GridViewRow gvrow = (sender as LinkButton).NamingContainer as GridViewRow;
            //string cSyouhin = (gvrow.FindControl("lnkCode") as LinkButton).Text;
            // HF_cSyouhin.Value = cSyouhin;
            //Session["fSyouhinSyosai"] = "Popup";
            //SessionUtility.SetSession("HOME", "Popup");
            //ifShouhinPopup.Src = "JC37Shohin.aspx";
            //mpeShouhinPopup.Show();
            //updShouhinPopup.Update();

            // 20220331 added by ルインマー start 
            Session["cSyohin"] = null;
            GridViewRow gvrow = (sender as LinkButton).NamingContainer as GridViewRow;
            string cSyouhin = (gvrow.FindControl("lnkCode") as LinkButton).Text;
            HF_cSyouhin.Value = cSyouhin;
            Session["cSyohin_code"] = cSyouhin;
            Session["fSyouhinSyosai"] = "Popup";
            SessionUtility.SetSession("HOME", "Popup");
            ifShouhinPopup.Style["width"] = "100vw";
            ifShouhinPopup.Style["height"] = "100vh";
            String id = Request.QueryString["id"];
            ifShouhinPopup.Src = "JC37Shohin.aspx?id=" + id;
            mpeShouhinPopup.Show();

            updShouhinPopup.Update();
            // 20220331 added by ルインマー end 
        }
        #endregion
    }
}