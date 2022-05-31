/*作成者：ナン
 *作成日：20210901
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Service;
using System.Data;
using Common;

namespace jobzcolud.WebFront
{
    public partial class JC25KyotenList : System.Web.UI.Page
    {
        string fcol = "";
        JC25KyotenList_Class ktVal = new JC25KyotenList_Class();

        protected void Page_Load(object sender, EventArgs e)
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
                    BindinKyotenList();

                }
                Session.Timeout = 480;
            }
        }

        #region "拠点データ取得、Bindingする"
        /// <summary>
        /// 拠点データ取得、Bindingする
        /// </summary>
        private void BindinKyotenList()
        {
            DataTable dt = new DataTable();
            string sqlstring = " SELECT cCo,sKYOTEN FROM m_j_info order by cCo;  ";
            String id = Request.QueryString["id"];
            DataTable dt_db = JC01Login_Class.Get_DB(id);
            String db = dt_db.Rows[0]["db"].ToString();
            String mail = dt_db.Rows[0]["mail"].ToString();
            ConstantVal.DB_NAME = db;
            ktVal.loginId = mail;
            dt = ktVal.KyotenListTable(sqlstring);
            gvKyotenlist.DataSource = dt;
            gvKyotenlist.DataBind();
            if (dt.Rows.Count > 10)
            {
                Gvdiv.Style["height"] = "380px";
            }
            else
            {
                Gvdiv.Style["height"] = "auto";
            }
        }
        #endregion

        #region "データ変更するとき、テキストを入力できるようにする、リンクボタンを非表示する"
        /// <summary>
        /// データ変更するとき、テキストを入力できるようにする、リンクボタンを非表示する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    if (e.Row.RowIndex >= 0)
                    {
                        
                        if (fcol == "Edit")
                        {
                            if (e.Row.RowIndex == gvKyotenlist.EditIndex)
                            {
                                e.Row.Cells[1].Visible = true;
                                e.Row.Cells[2].Visible = false;

                            }
                            else
                            {
                                e.Row.Cells[1].Visible = false;
                                e.Row.Cells[2].Visible = true;

                            }
                        }
                       
                        else
                        {
                            e.Row.Cells[1].Visible = false;
                            e.Row.Cells[2].Visible = true;
                        }


                    }
                }

            }
            catch (Exception ec)
            {
            }

        }
        #endregion       

        #region "行を変更するとき、入力できるようにする"
        /// <summary>
        /// データ変更するとき、入力できるようにする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnRowEditing(object sender, GridViewEditEventArgs e)
        {
            /*fcol = "Edit";
            gvKyotenlist.EditIndex = e.NewEditIndex;
            this.BindinKyotenList();*/

            int rowcount = gvKyotenlist.Rows.Count;
            string kyoutenId = "";
            for (int i = 0; i < rowcount; i++)
            {
                if (i == e.NewEditIndex)
                {                    
                    GridViewRow row = gvKyotenlist.Rows[e.NewEditIndex];
                    kyoutenId = gvKyotenlist.DataKeys[e.NewEditIndex].Values[0].ToString();
                    break;
                }
            }

            SessionUtility.SetSession("HOME", "Popup");
            if (kyoutenId == "")
            {
                Session["IdKyouten"] = null;
            }
            else
            {
                Session["IdKyouten"] = kyoutenId;
            }

            String id = Request.QueryString["id"];
            ifShinkiPopup.Src = "JC25KyotenNyuuryoku.aspx?id=" + id;
            mpeShinkiPopup.Show();
            updShinkiPopup.Update();
        }
        #endregion

        #region "行を削除する"
        /// <summary>
        /// データ削除する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnRowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            fcol = "Delete";
            int index = Convert.ToInt32(e.RowIndex);
            string cCoVal = gvKyotenlist.DataKeys[index].Values[0].ToString();
            
            string sqldelete = " DELETE FROM m_j_info WHERE cCo = '" + cCoVal + "'";
            String id = Request.QueryString["id"];
            DataTable dt_db = JC01Login_Class.Get_DB(id);
            String db = dt_db.Rows[0]["db"].ToString();
            String mail = dt_db.Rows[0]["mail"].ToString();
            ConstantVal.DB_NAME = db;
            ktVal.loginId = mail;
            Boolean flag = ktVal.KyotenListSql(sqldelete);
            //MySqlCommand myCommand = new MySqlCommand(sqldelete, mysqlcon);
            //mysqlcon.Open();
            //myCommand.ExecuteNonQuery();
            //mysqlcon.Close();
            this.BindinKyotenList();

        }
        #endregion

        #region "行更新する"
        /// <summary>
        /// 行更新する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>       
        protected void OnUpdate(object sender, EventArgs e)
        {
            GridViewRow row = (sender as LinkButton).NamingContainer as GridViewRow;
            string cCoVal = (row.Cells[0].Controls[0] as TextBox).Text;
            string skyoutenVal = (row.FindControl("txtsKYOTEN") as TextBox).Text;

          
            string sqlupdate = " Update m_j_info SET sKYOTEN = '" + skyoutenVal + "' WHERE cCo = '" + cCoVal + "'";
            String id = Request.QueryString["id"];
            DataTable dt_db = JC01Login_Class.Get_DB(id);
            String db = dt_db.Rows[0]["db"].ToString();
            String mail = dt_db.Rows[0]["mail"].ToString();
            ConstantVal.DB_NAME = db;
            ktVal.loginId = mail;
            Boolean flag = ktVal.KyotenListSql(sqlupdate);
            //MySqlCommand myCommand = new MySqlCommand(sqlupdate, mysqlcon);
            //mysqlcon.Open();
            //myCommand.ExecuteNonQuery();
            //mysqlcon.Close();
            gvKyotenlist.EditIndex = -1;
            this.BindinKyotenList();
        }
        #endregion

        #region "行選択"
        /// <summary>
        /// データ削除を削除する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvKyotenlist_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //int index = int.Parse(e.CommandArgument.ToString());
            if (e.CommandName == "Select")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = gvKyotenlist.Rows[rowIndex];
                string cCo = row.Cells[0].Text;
                string sKYOTEN = (row.FindControl("txtsKYOTEN") as TextBox).Text;
                Session["cKyoten"] = cCo;
                Session["sKyoten"] = sKYOTEN;  //20211011 MiMi Added
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "parentButtonClick('btnKyotanSelect','"+hdnHome.Value+"');", true);
                // ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Name: " + name + "\\nCountry: " + country + "');", true);
            }
        }
        #endregion      

        #region "拠点p"
        /// <summary>
        /// データ削除した後、画面を更新する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCancel_Click(object sender, EventArgs e)
        {
           // ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "parentButtonClick('btnKyotenlist');", true);
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "parentButtonClick('btnClose','"+hdnHome.Value+"');", true);
        }
        #endregion

        #region "拠点追加画面"
        /// <summary>
        /// 拠点追加画面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnKyotenlistNewPopup_Click(object sender, EventArgs e)
        {
            // ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "parentButtonClick('btnKyotenNew','"+hdnHome.Value+"');", true);
            Session["IdKyouten"] = null;
            SessionUtility.SetSession("HOME", "Popup");

            String id = Request.QueryString["id"];
            ifShinkiPopup.Src = "JC25KyotenNyuuryoku.aspx?id=" + id;
            mpeShinkiPopup.Show();

            updShinkiPopup.Update();

        }



        #endregion

        protected void btnKyotenNewClose_Click(object sender, EventArgs e)
        {
            
            ifShinkiPopup.Src = "";
            mpeShinkiPopup.Hide();
            updShinkiPopup.Update();
            gvKyotenlist.EditIndex = -1;    //20220314 MiMi Added
            BindinKyotenList();
            updKyotenlist.Update();

        }

        protected void btnHiddenSubmit_Click(object sender, EventArgs e)
        {
        }
    }
}