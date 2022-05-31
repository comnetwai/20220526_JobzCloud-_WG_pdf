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
using System.IO;
using System.Data;
using Common;
using System.Text;

namespace jobzcolud.WebFront
{
    public partial class JC25YukoKigenList : System.Web.UI.Page
    {
        string fcol = "";
        JC25YukoKigenList_Class ykVal = new JC25YukoKigenList_Class();
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
                    BindingKigenList();
                }
                Session.Timeout = 480;
            }            

        }

        #region "有効期限データ取得、Bindingする"
        /// <summary>
        /// 拠点データ取得、Bindingする
        /// </summary>
        private void BindingKigenList()
        {
            DataTable dt = new DataTable();
            string sqlstring = " SELECT cYUKO,sYUKO FROM m_yukokigen order by cYUKO  ";
            String id = Request.QueryString["id"];
            DataTable dt_db = JC01Login_Class.Get_DB(id);
            String db = dt_db.Rows[0]["db"].ToString();
            String mail = dt_db.Rows[0]["mail"].ToString();
            ConstantVal.DB_NAME = db;
            ykVal.loginId = mail;
            dt = ykVal.YukoKigenListTable(sqlstring);           
            gvYukoKigenlist.DataSource = dt;
            gvYukoKigenlist.DataBind();
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
                            if (e.Row.RowIndex == gvYukoKigenlist.EditIndex)
                            {
                                e.Row.Cells[1].Visible = true;
                                e.Row.Cells[2].Visible = false;

                                TextBox tbox = (TextBox)e.Row.FindControl("txtsYUKO");
                                tbox.Attributes["onFocus"] = "this.select()";
                                tbox.Focus();

                            }
                            else
                            {
                                e.Row.Cells[1].Visible = false;
                                e.Row.Cells[2].Visible = true;

                            }
                        }
                        //else if (fcol == "Delete")
                        //{
                        //    Button btn = (e.Row.Cells[3].Controls[0] as Button);
                        //    btn.Attributes["onclick"] = "if(!confirm('削除してもよろしいでしょうか？')){ return false; };";
                        //}
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
            fcol = "Edit";
            gvYukoKigenlist.EditIndex = e.NewEditIndex;
            this.BindingKigenList();

            newjyotai.Style.Add("visibility", "hidden");
            txt_newYuKo.Text = "";
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
            //fcol = "Delete";
            //int index = Convert.ToInt32(e.RowIndex);
            //string cCoVal = gvYukoKigenlist.DataKeys[index].Values[0].ToString();           
            //string sqldelete = " DELETE FROM m_yukokigen WHERE cYUKO = '" + cCoVal + "'";
            //ykVal.loginId = Session["LoginId"].ToString();
            //Boolean fdelete = ykVal.YukoKigenListSql(sqldelete);          
            //this.BindingKigenList();

        }


        protected void btnDelete_Click(object sender, EventArgs e)
        {
            LinkButton lkb_delete = sender as LinkButton;
            GridViewRow gvRow = (GridViewRow)lkb_delete.NamingContainer;
            string cCoVal = gvRow.Cells[0].Text;
            Button ok = gvYukoKigenlist.Rows[gvRow.RowIndex].FindControl("BT_DeleteOk") as Button;
            Button cancel = gvYukoKigenlist.Rows[gvRow.RowIndex].FindControl("BT_DeleteCancel") as Button;
            updYugokigenList.Update();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowAnkenChangeMessage",
                     "DeleteConfirmBox('削除してもよろしいでしょうか？','" + ok.ClientID + "','" + cancel.ClientID + "');", true);

        }

        protected void BT_DeleteOk_Click(object sender, EventArgs e)
        {
            GridViewRow row = (sender as Button).NamingContainer as GridViewRow;
            string cCoVal = row.Cells[0].Text;
            string sqldelete = " DELETE FROM m_yukokigen WHERE cYUKO = '" + cCoVal + "'";
            String id = Request.QueryString["id"];
            DataTable dt_db = JC01Login_Class.Get_DB(id);
            String db = dt_db.Rows[0]["db"].ToString();
            String mail = dt_db.Rows[0]["mail"].ToString();
            ConstantVal.DB_NAME = db;
            ykVal.loginId = mail;
            Boolean fdelete = ykVal.YukoKigenListSql(sqldelete);
            updYugokigenList.Update();
            this.BindingKigenList();

            newjyotai.Style.Add("visibility", "hidden");
            txt_newYuKo.Text = "";
        }
        #endregion

        #region "行更新する"
        /// <summary>
        /// 行更新する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>       
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            GridViewRow row = (sender as Button).NamingContainer as GridViewRow;
            string cCoVal = (row.Cells[0].Controls[0] as TextBox).Text;
            string skyoutenVal = (row.FindControl("txtsYUKO") as TextBox).Text;
            if (skyoutenVal != "")
            {
                if (skyoutenVal.Contains("'") == true)
                {
                    skyoutenVal = skyoutenVal.Replace("'", @"\'");

                }                
            }

            var textByteCount = Encoding.Default.GetByteCount(skyoutenVal);
            if (textByteCount > 28)
            {
                string textCount = skyoutenVal;
                while (Encoding.Default.GetByteCount(textCount) > 28)
                {
                    textCount = textCount.Substring(0, textCount.Length - 1);
                }
                skyoutenVal = textCount;
            }

            string sqlupdate = " Update m_yukokigen SET sYUKO = '" + skyoutenVal + "' WHERE cYUKO = '" + cCoVal + "'";
            String id = Request.QueryString["id"];
            DataTable dt_db = JC01Login_Class.Get_DB(id);
            String db = dt_db.Rows[0]["db"].ToString();
            String mail = dt_db.Rows[0]["mail"].ToString();
            ConstantVal.DB_NAME = db;
            ykVal.loginId = mail;
            Boolean fupdate = ykVal.YukoKigenListSql(sqlupdate);          
            gvYukoKigenlist.EditIndex = -1;
            this.BindingKigenList();
        }
        #endregion

        #region　変更modeのキャンセル
        protected void btnSaveCancel_Click(object sender, EventArgs e)
        {
            updYugokigenList.Update();
            this.BindingKigenList();
        }
        #endregion

        #region "行選択"
        /// <summary>
        /// データ削除を削除する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvYukoKigenlist_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //int index = int.Parse(e.CommandArgument.ToString());
            if (e.CommandName == "Select")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = gvYukoKigenlist.Rows[rowIndex];
                string cCo = row.Cells[0].Text;
                string sYUKO = (row.FindControl("txtsYUKO") as TextBox).Text;
                Session["cYuko"] = cCo;
                Session["sYuko"] = sYUKO;  //20211025 MiMi Added
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "parentButtonClick('btnYukoKigenListSelect','" + hdnHome.Value + "');", true);
                // ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Name: " + name + "\\nCountry: " + country + "');", true);
            }
        }
        #endregion       
        
        #region 支払方法テキスト入力制限全角14文字半角28文字
        protected void txt_newYuKo_TextChanged(object sender, EventArgs e)
        {
            var textByteCount = Encoding.Default.GetByteCount(txt_newYuKo.Text);
            if (textByteCount > 28)
            {
                string textCount = txt_newYuKo.Text;
                while (Encoding.Default.GetByteCount(textCount) > 28)
                {
                    textCount = textCount.Substring(0, textCount.Length - 1);
                }
                txt_newYuKo.Text = textCount;
            }
        }
        #endregion

        #region　有効期限追加

        protected void btnYukoKigenlistNewPopup_Click(object sender, EventArgs e)
        {
            updYugokigenList.Update();
            this.BindingKigenList();

            newjyotai.Style.Add("visibility", "visible");
            newjyotai.Style["display"] = "block";
            txt_newYuKo.Focus();
            if (gvYukoKigenlist.Rows.Count > 0)
            {
                newjyotai.Style.Add("margin-left", "32px;");
                newjyotai.Style.Add("margin-top", "0px;");
                newjyotai.Style.Add("margin-bottom", "10px;");
            }
            else
            {
                newjyotai.Style.Add("margin-left", "32px;");
                newjyotai.Style.Add("margin-top", "10px;");
                newjyotai.Style.Add("margin-bottom", "10px;");
            }
        }
        #endregion       

        #region 有効期限保存
        protected void btnnewYuKoSave_Click(object sender, EventArgs e)
        {
            bool chkdata = true;
            //状態名
            if (String.IsNullOrEmpty(txt_newYuKo.Text))
            {
                txt_newYuKo.Text = "";
                txt_newYuKo.Style.Add("border-color", "red !important");
                txt_newYuKo.Focus();
                chkdata = false;
            }
            if (chkdata == true)
            {
                bool fsave = SaveKyoten();
                //正常に保存するnewjyotai
                if (fsave == true)
                {
                    BindingKigenList();//20211125 add by テテ
                    //newjyotai.Style["display"] = "none";//20211125 add by テテ
                    newjyotai.Style.Add("visibility", "hidden");
                    txt_newYuKo.Text = "";//20220208 add by テテ

                }
            }
        }
        #endregion

        #region　新規modeのキャンセル
        protected void btnnewYuKoCancel_Click(object sender, EventArgs e)
        {
            //newjyotai.Style["display"] = "none";
            newjyotai.Style.Add("visibility", "hidden");
            txt_newYuKo.Text = "";
        }
        #endregion     

        #region 保存
        protected bool SaveKyoten()
        {
            bool fsave = false;
            try
            {
                int retval = 0;
                int njunban = 0;
                string cCoVal = FindKyotenCode();
                njunban = Convert.ToInt32(cCoVal);
                string yukomei = "";
                //状態名 sJYOTAI
                if (!String.IsNullOrEmpty(txt_newYuKo.Text.Trim()))
                {
                    if (txt_newYuKo.Text.Contains("'") == true)
                    {
                        yukomei = txt_newYuKo.Text.Replace("'", @"\'");

                    }
                    else
                    {
                        yukomei = txt_newYuKo.Text;
                    }
                    yukomei = yukomei.Trim();
                }
                else
                {
                    yukomei = "@null";
                }


                //find current date time
                string curDateTime = "";
                DataTable dt = new DataTable();
                string datesql = " SELECT NOW() as nowdateTime;  ";
                //MySqlDataAdapter adap = new MySqlDataAdapter(datesql, constr);
                //adap.Fill(dt);
                String id = Request.QueryString["id"];
                DataTable dt_db = JC01Login_Class.Get_DB(id);
                String db = dt_db.Rows[0]["db"].ToString();
                String mail = dt_db.Rows[0]["mail"].ToString();
                ConstantVal.DB_NAME = db;
                ykVal.loginId = mail;
                dt = ykVal.YukoKigenListTable(datesql);
                if (dt.Rows.Count > 0)
                {
                    curDateTime = dt.Rows[0]["nowdateTime"].ToString();
                }


                string sql = "INSERT INTO m_yukokigen(";
                sql += " cYUKO ";
                sql += ",sYUKO ";
                sql += ", dHENKOU ";
                sql += ", cHENKOUSYA ";
                sql += ")VALUES  ";
                sql += " ( '" + cCoVal + "'";
                sql += " , '" + yukomei + "'";
                sql += " , '" + curDateTime + "'";
                sql += " , '9999'";
                sql += "  ) ";
                sql += " ON DUPLICATE KEY UPDATE cYUKO = '" + cCoVal + "'";
                sql += " ,sYUKO =  '" + yukomei + "' ";
                sql += " ,dHENKOU =  '" + curDateTime + "' ";
                sql += " ,cHENKOUSYA =  '9999' ";
                ConstantVal.DB_NAME = db;
                ykVal.loginId = mail;
                fsave = ykVal.YukoKigenListSql(sql);
            }
            catch (Exception ec)
            {

            }

            return fsave;
        }
        #endregion

        #region コード取得
        protected string FindKyotenCode()
        {
            string ColVal = "";
            System.Data.DataTable dt = new System.Data.DataTable();
            string sqlStr = "SELECT cYUKO,sYUKO FROM m_yukokigen; ";
            String id = Request.QueryString["id"];
            DataTable dt_db = JC01Login_Class.Get_DB(id);
            String db = dt_db.Rows[0]["db"].ToString();
            String mail = dt_db.Rows[0]["mail"].ToString();
            ConstantVal.DB_NAME = db;
            ykVal.loginId = mail;
            dt = ykVal.YukoKigenListTable(sqlStr);
            List<int> ListShain = new List<int>();
            foreach (System.Data.DataRow dr in dt.Rows)
            {
                ListShain.Add(int.Parse(dr["cYUKO"].ToString()));
            }
            if (ListShain.Count > 0)
            {
                var MissingNumbers = Enumerable.Range(1, 9999).Except(ListShain).ToList();
                var ResultNum = MissingNumbers.Min();
                ColVal = ResultNum.ToString().PadLeft(2, '0');
            }
            else
            {
                var MissingNumbers = 1;
                ColVal = MissingNumbers.ToString().PadLeft(2, '0');
            }
            return ColVal;
        }
        #endregion

        #region "POPUP画面閉じる(キャンセル)"
        /// <summary>
        /// データ削除した後、画面を更新する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCancel_Click(object sender, EventArgs e)
        {

            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "parentButtonClick('btnClose','" + hdnHome.Value + "');", true);

        }
        #endregion      
    }
}