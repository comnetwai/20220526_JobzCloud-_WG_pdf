/*作成者：ルインマー
 *作成日：20210901
 */
using Common;
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
    public partial class JC25BumonList : System.Web.UI.Page
    {
        string fcol = "";
        JC25MistuJyoutai_Class bmVal = new JC25MistuJyoutai_Class();

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
                    BindinJoytaiList();
                }
                Session.Timeout = 480;
            }
            
        }
       
        #region "部門データ取得、Bindingする"
        /// <summary>
        /// 部門データ取得、Bindingする
        /// </summary>
        private void BindinJoytaiList()
        {
            DataTable dt = new DataTable();
            string sqlstring = " SELECT cBUMON,sBUMON FROM m_bumon order by cBUMON;  ";
            String id = Request.QueryString["id"];
            DataTable dt_db = JC01Login_Class.Get_DB(id);
            String db = dt_db.Rows[0]["db"].ToString();
            String mail = dt_db.Rows[0]["mail"].ToString();
            ConstantVal.DB_NAME = db;
            bmVal.loginId = mail;
            dt = bmVal.MitsuJyotaiListTable(sqlstring);            
            gvBumonlist.DataSource = dt;
            gvBumonlist.DataBind();
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
                            if (e.Row.RowIndex == gvBumonlist.EditIndex)
                            {
                                e.Row.Cells[1].Visible = true;
                                e.Row.Cells[2].Visible = false;

                                //TextBox tbox = (TextBox)e.Row.FindControl("txtsBUMON");
                                //tbox.Focus();
                                //20220404
                                TextBox tbox = (TextBox)e.Row.FindControl("txtsBUMON");
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
            gvBumonlist.EditIndex = e.NewEditIndex;
            this.BindinJoytaiList();

            newjyotai.Style.Add("visibility", "hidden");           
            txt_newBumon.Text = "";
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
            /*int index = Convert.ToInt32(e.RowIndex);           
            int rowIndex = Convert.ToInt32(e.RowIndex);
            string cCoVal = gvBumonlist.DataKeys[rowIndex].Values[0].ToString();
            string sqldelete = " DELETE FROM m_bumon WHERE cBUMON = '" + cCoVal + "'";
            bmVal.loginId = Session["LoginId"].ToString();
            Boolean fdelete = bmVal.MitsuJyotaiListSql(sqldelete);
            updBumonlist.Update();
            this.BindinJoytaiList();
                           
            newjyotai.Style.Add("visibility", "hidden");
            txt_newBumon.Text = "";*/
        }
        #endregion

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            LinkButton lkb_delete = sender as LinkButton;
            GridViewRow gvRow = (GridViewRow)lkb_delete.NamingContainer;
            string cCoVal = gvRow.Cells[0].Text;
            Button ok = gvBumonlist.Rows[gvRow.RowIndex].FindControl("BT_DeleteOk") as Button;
            Button cancel = gvBumonlist.Rows[gvRow.RowIndex].FindControl("BT_DeleteCancel") as Button;
            updBumonlist.Update();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowAnkenChangeMessage",
                     "DeleteConfirmBox('削除してもよろしいでしょうか？','" + ok.ClientID + "','" + cancel.ClientID + "');", true);
            
        }

        protected void BT_DeleteOk_Click(object sender, EventArgs e)
        {
            GridViewRow row = (sender as Button).NamingContainer as GridViewRow;
            string cCoVal = row.Cells[0].Text;
            string sqldelete = " DELETE FROM m_bumon WHERE cBUMON = '" + cCoVal + "'";
            String id = Request.QueryString["id"];
            DataTable dt_db = JC01Login_Class.Get_DB(id);
            String db = dt_db.Rows[0]["db"].ToString();
            String mail = dt_db.Rows[0]["mail"].ToString();
            ConstantVal.DB_NAME = db;
            bmVal.loginId = mail;
            Boolean fdelete = bmVal.MitsuJyotaiListSql(sqldelete);
            updBumonlist.Update();
            this.BindinJoytaiList();

            newjyotai.Style.Add("visibility", "hidden");
            txt_newBumon.Text = "";
        }

        #region "行更新する"
        /// <summary>
        /// 行更新する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>       
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            updBumonlist.Update();
            this.BindinJoytaiList();

            GridViewRow row = (sender as Button).NamingContainer as GridViewRow;
            string cCoVal = (row.Cells[0].Controls[0] as TextBox).Text;
            string sBumonVal = (row.FindControl("txtsBUMON") as TextBox).Text;

            //mysqlcon.ConnectionString = constr;
            string sqlupdate = " Update m_bumon SET sBUMON = '" + sBumonVal + "' WHERE cBUMON = '" + cCoVal + "'";
            String id = Request.QueryString["id"];
            DataTable dt_db = JC01Login_Class.Get_DB(id);
            String db = dt_db.Rows[0]["db"].ToString();
            String mail = dt_db.Rows[0]["mail"].ToString();
            ConstantVal.DB_NAME = db;
            bmVal.loginId = mail;
            Boolean fupdate = bmVal.MitsuJyotaiListSql(sqlupdate);


            gvBumonlist.EditIndex = -1;
            this.BindinJoytaiList();
        }
        #endregion

        #region 変更modeキャンセル
        protected void btnSaveCancel_Click(object sender, EventArgs e)
        {
            updBumonlist.Update();
            this.BindinJoytaiList();
        }
        #endregion

        #region "行選択"
        /// <summary>
        /// データ削除を削除する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvBumonlist_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //int index = int.Parse(e.CommandArgument.ToString());
            if (e.CommandName == "Select")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = gvBumonlist.Rows[rowIndex];
                string cCo = row.Cells[0].Text;
                string sBumon = (row.FindControl("txtsBUMON") as TextBox).Text;
                Session["cBumon"] = cCo;
                Session["sBumon"] = sBumon;
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "parentButtonClick('btnBumonSelect','" + hdnHome.Value + "');", true);
                // ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Name: " + name + "\\nCountry: " + country + "');", true);
            }
            //else if (e.CommandName == "Delete")
            //{
            //    fcol = "Delete";
            //    if (fDel.Value == "true")
            //    {
            //        int rowIndex = Convert.ToInt32(e.CommandArgument);
            //        string cCoVal = gvBumonlist.DataKeys[rowIndex].Values[0].ToString();
            //        string sqldelete = " DELETE FROM m_bumon WHERE cBUMON = '" + cCoVal + "'";
            //        bmVal.loginId = Session["LoginId"].ToString();
            //        Boolean fdelete = bmVal.MitsuJyotaiListSql(sqldelete);
            //        updBumonlist.Update();
            //        this.BindinJoytaiList();
            //    }
            //}
        }
        #endregion       

        #region  部門追加       
        protected void btnnewbumon_Click(object sender, EventArgs e)
        {
            updBumonlist.Update();
            this.BindinJoytaiList();
            newjyotai.Style.Add("visibility", "visible");
            newjyotai.Style["display"] = "block";
            txt_newBumon.Focus();
            if (gvBumonlist.Rows.Count > 0)
            {
                newjyotai.Style.Add("margin-left", "19px;");
                newjyotai.Style.Add("margin-top", "0px;");
                newjyotai.Style.Add("margin-bottom", "10px;");
            }
            else
            {
                newjyotai.Style.Add("margin-left", "19px;");
                newjyotai.Style.Add("margin-top", "10px;");
                newjyotai.Style.Add("margin-bottom", "10px;");
            }
        }
        #endregion

        #region 部門追加保存
        protected void btnnewbumonSave_Click(object sender, EventArgs e)
        {
            bool chkdata = true;
            //状態名
            if (String.IsNullOrEmpty(txt_newBumon.Text))
            {
                txt_newBumon.Text = "";
                txt_newBumon.Style.Add("border-color", "red !important");
                txt_newBumon.Focus();
                chkdata = false;
            }
            if (chkdata == true)
            {
                bool fsave = SaveBumon();
                //正常に保存する
                if (fsave == true)
                {
                    BindinJoytaiList();                   
                    newjyotai.Style.Add("visibility", "hidden");
                    txt_newBumon.Text = "";
                }
                
            }
        }
        #endregion

        #region　新規modeキャンセル
        protected void btnnewbumonCancel_Click(object sender, EventArgs e)
        {
            newjyotai.Style.Add("visibility", "hidden");
            txt_newBumon.Text = "";
        }
        #endregion

        #region 保存
        protected bool SaveBumon()
        {
            bool fsave = false;
            try
            {
                int retval = 0;
                int njunban = 0;
                string cCoVal = FindKyotenCode();
                njunban = Convert.ToInt32(cCoVal);
                string joytaimei = "";
                //状態名 sJYOTAI
                if (!String.IsNullOrEmpty(txt_newBumon.Text.Trim()))
                {
                    joytaimei = txt_newBumon.Text.Trim();
                }
                else
                {
                    joytaimei = "@null";
                }


                //find current date time
                string curDateTime = "";
                DataTable dt = new DataTable();
                string datesql = " SELECT NOW() as nowdateTime;  ";
                String id = Request.QueryString["id"];
                DataTable dt_db = JC01Login_Class.Get_DB(id);
                String db = dt_db.Rows[0]["db"].ToString();
                String mail = dt_db.Rows[0]["mail"].ToString();
                ConstantVal.DB_NAME = db;
                bmVal.loginId = mail;
                dt = bmVal.MitsuJyotaiListTable(datesql);
                //MySqlDataAdapter adap = new MySqlDataAdapter(datesql, constr);
                //adap.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    curDateTime = dt.Rows[0]["nowdateTime"].ToString();
                }


                string sql = "INSERT INTO m_bumon(";
                sql += " cBUMON ";
                sql += ", sBUMON ";
                sql += ", dHENKOU ";
                sql += ", cHENKOUSYA ";
                sql += ")VALUES  ";
                sql += " ( '" + cCoVal + "'";
                sql += " , '" + joytaimei + "'";
                sql += " , '" + curDateTime + "'";
                sql += " , '9999'";
                sql += "  ) ";
                sql += " ON DUPLICATE KEY UPDATE cBUMON = '" + cCoVal + "'";
                sql += " ,sBUMON =  '" + joytaimei + "' ";
                sql += " ,dHENKOU =  '" + curDateTime + "' ";
                sql += " ,cHENKOUSYA =  '9999' ";

                //mysqlcon.ConnectionString = constr;

                //MySqlCommand myCommand = new MySqlCommand(sql, mysqlcon);
                //mysqlcon.Open();

                //retval = myCommand.ExecuteNonQuery();
                bmVal.loginId = mail;
                ConstantVal.DB_NAME = db;
                fsave = bmVal.MitsuJyotaiListSql(sql);
                //if (retval != -1)
                //{
                //    fsave = true;
                //}
                // mysqlcon.Close();
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
            string sqlStr = "SELECT cBUMON,sBUMON FROM m_bumon; ";
            String id = Request.QueryString["id"];
            DataTable dt_db = JC01Login_Class.Get_DB(id);
            String db = dt_db.Rows[0]["db"].ToString();
            String mail = dt_db.Rows[0]["mail"].ToString();
            ConstantVal.DB_NAME = db;
            bmVal.loginId = mail;
            dt = bmVal.MitsuJyotaiListTable(sqlStr);
            //MySqlDataAdapter adap = new MySqlDataAdapter(sqlStr, constr);
            // adap.Fill(dt);
            //finding the missing number 
            List<int> ListShain = new List<int>();
            foreach (System.Data.DataRow dr in dt.Rows)
            {
                ListShain.Add(int.Parse(dr["cBUMON"].ToString()));
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
        /// 
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