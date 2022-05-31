/*作成者：ルインマー
 *作成日：20210901
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Service;
using Common;

namespace jobzcolud.WebFront
{
    public partial class JC25_MitsuJyoutai : System.Web.UI.Page
    {
        string fcol = "";
        string newId = "";
        int newJunabn = 0;
        JC25MistuJyoutai_Class mtVal = new JC25MistuJyoutai_Class();
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
            else
            {
               
            }
        }
       // comment by テテ 20211115
        protected void btnnewjoytai_Click(object sender, EventArgs e)
        {
            newjyotai.Style["display"] = "block";
            if (gvJyotailist.Rows.Count > 0)
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
        protected void btnnewjoytaiSave_Click(object sender, EventArgs e)
        {
            bool chkdata = true;
            //状態名
            if (String.IsNullOrEmpty(txt_newJoytai.Text))
            {
                txt_newJoytai.Text = "";
                txt_newJoytai.Style.Add("border-color", "red !important");
                txt_newJoytai.Focus();
                chkdata = false;
            }
            if (chkdata == true)
            {
                bool fsave = SaveKyoten();
                //正常に保存する
                if (fsave == true)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "parentButtonClick('btnJoutaiSelect','" + hdnHome.Value + "');", true);
                }
                //エラーの場合
                else
                {
                    //ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "parentButtonClick('btnAnkenNyuuryokuSave');", true);
                }
            }
        }
        protected bool SaveKyoten()
        {
            bool fsave = false;
            try
            {
                int retval = 0;
                int njunban = 0;
                string cCoVal = newId; //FindKyotenCode();
                njunban = newJunabn; //Convert.ToInt32(cCoVal);
                string joytaimei = "";
                //状態名 sJYOTAI
                //if (!String.IsNullOrEmpty(txt_newJoytai.Text.Trim()))
                //{
                //    joytaimei = txt_newJoytai.Text.Trim();
                //}
                //else
                //{
                //    joytaimei = "@null";
                //} comment by テテ 20211115


                //find current date time
                string curDateTime = "";
                DataTable dt = new DataTable();
                string datesql = " SELECT NOW() as nowdateTime;  ";

                String id = Request.QueryString["id"];
                DataTable dt_db = JC01Login_Class.Get_DB(id);
                String db = dt_db.Rows[0]["db"].ToString();
                String mail = dt_db.Rows[0]["mail"].ToString();
                ConstantVal.DB_NAME = db;

                mtVal.loginId = mail;
                dt = mtVal.MitsuJyotaiListTable(datesql);               
                if (dt.Rows.Count > 0)
                {
                    curDateTime = dt.Rows[0]["nowdateTime"].ToString();
                }


                string sql = "INSERT INTO m_mitumori_jyotai(";
                sql += " cJYOTAI ";
                sql += ", sJYOTAI ";
                sql += ", nJUNBAN ";
                sql += ", dHENKOU ";
                sql += ", cHENKOUSYA ";
                sql += ", nKakudo ";
                sql += ", fKensaku ";
                sql += ")VALUES  ";
                sql += " ( '" + cCoVal + "'";
                sql += " , '" + joytaimei + "'";
                sql += " ," + njunban + "";
                sql += " , '" + curDateTime + "'";
                sql += " , '9999'";
                sql += " ,null";
                sql += " ,null";
                sql += "  ) ";
                sql += " ON DUPLICATE KEY UPDATE cJYOTAI = '" + cCoVal + "'";
                sql += " ,sJYOTAI =  '" + joytaimei + "' ";
                sql += " ,nJUNBAN =  " + njunban + " ";
                sql += " ,dHENKOU =  '" + curDateTime + "' ";
                sql += " ,cHENKOUSYA =  '9999' ";
                sql += " ,nKakudo = null ";
                sql += " ,fKensaku = null ";

                
                mtVal.loginId = mail;
                ConstantVal.DB_NAME = db;
                fsave = mtVal.MitsuJyotaiListSql(sql);               
            }
            catch (Exception ec)
            {

            }

            return fsave;
        }

        protected void FindKyotenCode()
        {
            string ColVal = "";
            System.Data.DataTable dt = new System.Data.DataTable();
            string sqlStr = "SELECT cJYOTAI,sJYOTAI,nJUNBAN FROM m_mitumori_jyotai ";

            String id = Request.QueryString["id"];
            DataTable dt_db = JC01Login_Class.Get_DB(id);
            String db = dt_db.Rows[0]["db"].ToString();
            String mail = dt_db.Rows[0]["mail"].ToString();
            ConstantVal.DB_NAME = db;

            mtVal.loginId = mail;
            dt = mtVal.MitsuJyotaiListTable(sqlStr);
            
            //finding the missing number 
            List<int> ListShain = new List<int>();
            foreach (System.Data.DataRow dr in dt.Rows)
            {
                ListShain.Add(int.Parse(dr["cJYOTAI"].ToString()));
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
            //return ColVal;
            newId = ColVal;

            //順番番号
            List<int> ListJunban = new List<int>();
            foreach (System.Data.DataRow dr in dt.Rows)
            {
                ListJunban.Add(int.Parse(dr["nJUNBAN"].ToString()));
            }
            newJunabn = ListJunban.Max();            
        }

        #region "見積状態データ取得、Bindingする"
        /// <summary>
        /// 見積状態データ取得、Bindingする
        /// </summary>
        private void BindinJoytaiList()
        {
            DataTable dt = new DataTable();
            string sqlstring = " SELECT cJYOTAI,sJYOTAI FROM m_mitumori_jyotai order by cast(nJUNBAN as unsigned)  ";

            String id = Request.QueryString["id"];
            DataTable dt_db = JC01Login_Class.Get_DB(id);
            String db = dt_db.Rows[0]["db"].ToString();
            String mail = dt_db.Rows[0]["mail"].ToString();
            ConstantVal.DB_NAME = db;
            mtVal.loginId = mail;
            dt = mtVal.MitsuJyotaiListTable(sqlstring);            
            gvJyotailist.DataSource = dt;
            gvJyotailist.DataBind();
            
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
                            if (e.Row.RowIndex == gvJyotailist.EditIndex)
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
            gvJyotailist.EditIndex = e.NewEditIndex;
            this.BindinJoytaiList();
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
            string cCoVal = gvJyotailist.DataKeys[index].Values[0].ToString();           
            string sqldelete = " DELETE FROM m_mitumori_jyotai WHERE cJYOTAI = '" + cCoVal + "'";
            String id = Request.QueryString["id"];
            DataTable dt_db = JC01Login_Class.Get_DB(id);
            String db = dt_db.Rows[0]["db"].ToString();
            String mail = dt_db.Rows[0]["mail"].ToString();
            ConstantVal.DB_NAME = db;
            mtVal.loginId = mail;
            Boolean fdelete = mtVal.MitsuJyotaiListSql(sqldelete);           
            this.BindinJoytaiList();

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
            string skyoutenVal = (row.FindControl("txtsJYOTAI") as TextBox).Text;

            //mysqlcon.ConnectionString = constr;
            string sqlupdate = " Update m_mitumori_jyotai SET sKYOTEN = '" + skyoutenVal + "' WHERE cJYOTAI = '" + cCoVal + "'";

            String id = Request.QueryString["id"];
            DataTable dt_db = JC01Login_Class.Get_DB(id);
            String db = dt_db.Rows[0]["db"].ToString();
            String mail = dt_db.Rows[0]["mail"].ToString();
            ConstantVal.DB_NAME = db;

            mtVal.loginId = mail;
            Boolean fupdate = mtVal.MitsuJyotaiListSql(sqlupdate);
            
            gvJyotailist.EditIndex = -1;
            this.BindinJoytaiList();
        }
        #endregion

        #region "行選択"
        /// <summary>
        /// データ削除を削除する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvJyotailist_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //int index = int.Parse(e.CommandArgument.ToString());
            if (e.CommandName == "Select")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = gvJyotailist.Rows[rowIndex];
                string cJoutai = row.Cells[0].Text;
                string sJoutai = (row.FindControl("txtsJYOTAI") as TextBox).Text;
                Session["cJyotai"] = cJoutai;
                Session["sJyotai"] = sJoutai;  //20211028 MiMi Added
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "parentButtonClick('btnJoutaiSelect','" + hdnHome.Value+"');", true);               
            }
        }
        #endregion

        #region "PopUp画面閉じる"
        /// <summary>
        /// PopUp画面閉じる
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "parentButtonClick('btnClose','" + hdnHome.Value+"');", true);

        }
        #endregion
    }
}