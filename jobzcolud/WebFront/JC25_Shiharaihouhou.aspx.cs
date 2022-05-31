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
using System.Text;

namespace jobzcolud.WebFront
{
    public partial class JC25_Shiharaihouhou : System.Web.UI.Page
    {
        JC25Shiharaihouhou_Class shVal = new JC25Shiharaihouhou_Class();
        string fcol = "";
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

                    BindinShiharaiList();

                }
                Session.Timeout = 480;
            }            
        }
       
        #region "データ取得、Bindingする"
        /// <summary>
        /// 拠点データ取得、Bindingする
        /// </summary>
        private void BindinShiharaiList()
        {
            DataTable dt = new DataTable();
            string sqlstring = " SELECT cSHIHARAI,sSHIHARAI FROM m_shiharai order by cSHIHARAI;";
            String id = Request.QueryString["id"];
            DataTable dt_db = JC01Login_Class.Get_DB(id);
            String db = dt_db.Rows[0]["db"].ToString();
            String mail = dt_db.Rows[0]["mail"].ToString();
            ConstantVal.DB_NAME = db;
            shVal.loginId = mail;
            dt = shVal.ShiaraiListTable(sqlstring);           
            gvshiarailist.DataSource = dt;
            gvshiarailist.DataBind();

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
                            if (e.Row.RowIndex == gvshiarailist.EditIndex)
                            {
                                e.Row.Cells[1].Visible = true;
                                e.Row.Cells[2].Visible = false;
                                TextBox tbox = (TextBox)e.Row.FindControl("txtsSHIHARAI");
                                //tbox.Attributes["onFocus"] = "this.select()";
                                tbox.Focus();
                                tbox.Attributes.Add("onfocus", "selectText(" + gvshiarailist.EditIndex + ");");
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
            gvshiarailist.EditIndex = e.NewEditIndex;
            this.BindinShiharaiList();

            newshiarai.Style.Add("visibility", "hidden");
            txt_newshiarai.Text = "";
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
            //string cCoVal = gvshiarailist.DataKeys[index].Values[0].ToString();
            ////mysqlcon.ConnectionString = constr;
            //string sqldelete = " DELETE FROM m_shiharai WHERE cSHIHARAI = '" + cCoVal + "'";
            //shVal.loginId = Session["LoginId"].ToString();
            //Boolean fdelete = shVal.ShiaraiListListSql(sqldelete);
            //updShiharailist.Update();
            //this.BindinShiharaiList();
          
        }


        protected void btnDelete_Click(object sender, EventArgs e)
        {
            LinkButton lkb_delete = sender as LinkButton;
            GridViewRow gvRow = (GridViewRow)lkb_delete.NamingContainer;
            string cCoVal = gvRow.Cells[0].Text;
            Button ok = gvshiarailist.Rows[gvRow.RowIndex].FindControl("BT_DeleteOk") as Button;
            Button cancel = gvshiarailist.Rows[gvRow.RowIndex].FindControl("BT_DeleteCancel") as Button;
            updShiharailist.Update();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowAnkenChangeMessage",
                     "DeleteConfirmBox('削除してもよろしいでしょうか？','" + ok.ClientID + "','" + cancel.ClientID + "');", true);

        }

        protected void BT_DeleteOk_Click(object sender, EventArgs e)
        {
            GridViewRow row = (sender as Button).NamingContainer as GridViewRow;
            string cCoVal = row.Cells[0].Text;
            string sqldelete = " DELETE FROM m_shiharai WHERE cSHIHARAI = '" + cCoVal + "'";
            String id = Request.QueryString["id"];
            DataTable dt_db = JC01Login_Class.Get_DB(id);
            String db = dt_db.Rows[0]["db"].ToString();
            String mail = dt_db.Rows[0]["mail"].ToString();
            ConstantVal.DB_NAME = db;
            shVal.loginId = mail;
            Boolean fdelete = shVal.ShiaraiListListSql(sqldelete);
            updShiharailist.Update();
            this.BindinShiharaiList();

            newshiarai.Style.Add("visibility", "hidden");
            txt_newshiarai.Text = "";
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

            updShiharailist.Update();
            this.BindinShiharaiList();

            GridViewRow row = (sender as Button).NamingContainer as GridViewRow;
            string cCoVal = (row.Cells[0].Controls[0] as TextBox).Text;
            string sShiaraiVal = (row.FindControl("txtsSHIHARAI") as TextBox).Text;
         
            if (sShiaraiVal != "")
            {
                if (sShiaraiVal.Contains("'") == true)
                {
                    sShiaraiVal = sShiaraiVal.Replace("'", @"\'");

                }
                
                //txt_sSEIKYUUSHO = "'" + txtSeikyuusho.Text + "'";
            }
            var textByteCount = Encoding.Default.GetByteCount(sShiaraiVal);
            if (textByteCount > 28)
            {
                string textCount = sShiaraiVal;
                while (Encoding.Default.GetByteCount(textCount) > 28)
                {
                    textCount = textCount.Substring(0, textCount.Length - 1);
                }
                sShiaraiVal = textCount;
            }
            string sqlupdate = " Update m_shiharai SET sSHIHARAI = '" + sShiaraiVal + "' WHERE cSHIHARAI = '" + cCoVal + "'";
            String id = Request.QueryString["id"];
            DataTable dt_db = JC01Login_Class.Get_DB(id);
            String db = dt_db.Rows[0]["db"].ToString();
            String mail = dt_db.Rows[0]["mail"].ToString();
            ConstantVal.DB_NAME = db;
            shVal.loginId = mail;
            Boolean fupdate = shVal.ShiaraiListListSql(sqlupdate);
            //MySqlCommand myCommand = new MySqlCommand(sqlupdate, mysqlcon);
            //mysqlcon.Open();
            //myCommand.ExecuteNonQuery();
            //mysqlcon.Close();
            gvshiarailist.EditIndex = -1;
            this.BindinShiharaiList();
        }
        #endregion

        #region　変更modeキャンセル
        protected void btnSaveCancel_Click(object sender, EventArgs e)
        {
            updShiharailist.Update();
            this.BindinShiharaiList();
        }
        #endregion

        #region "行選択"
        /// <summary>
        /// データ削除を削除する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvshiarailist_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //int index = int.Parse(e.CommandArgument.ToString());
            if (e.CommandName == "Select")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = gvshiarailist.Rows[rowIndex];
                string cShiharai = row.Cells[0].Text;
                string sShiharai = (row.FindControl("txtsSHIHARAI") as TextBox).Text;
                Session["cSHIHARAI"] = cShiharai;
                Session["sSHIHARAI"] = sShiharai;  //20211011 MiMi Added
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "parentButtonClick('btnshiarailistSelect','" + hdnHome.Value + "');", true);
                // ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Name: " + name + "\\nCountry: " + country + "');", true);
            }
        }
        #endregion

        #region 支払方法テキスト入力制限全角14文字半角28文字
        protected void txt_newshiarai_TextChanged(object sender, EventArgs e)
        {
           
            var textByteCount = Encoding.Default.GetByteCount(txt_newshiarai.Text);
            if (textByteCount > 28)
            {
                string textCount = txt_newshiarai.Text;
                while (Encoding.Default.GetByteCount(textCount) > 28)
                {
                    textCount = textCount.Substring(0, textCount.Length - 1);
                }
                //txt_newshiarai.Focus();
                txt_newshiarai.Text = textCount;
            }
        }
        #endregion

        #region 支払方法追加
        protected void btnnewshiarai_Click(object sender, EventArgs e)
        {
            updShiharailist.Update();
            this.BindinShiharaiList();

            newshiarai.Style.Add("visibility", "visible");
            newshiarai.Style["display"] = "block";
            txt_newshiarai.Focus();
            if (gvshiarailist.Rows.Count > 0)
            {
                newshiarai.Style.Add("margin-left", "32px;");
                newshiarai.Style.Add("margin-top", "0px;");
                newshiarai.Style.Add("margin-bottom", "10px;");
            }
            else
            {
                newshiarai.Style.Add("margin-left", "32px;");
                newshiarai.Style.Add("margin-top", "10px;");
                newshiarai.Style.Add("margin-bottom", "10px;");
            }
        }
        #endregion

        #region 支払方法追加の保存
        protected void btnnewshiaraiSave_Click(object sender, EventArgs e)
        {
            bool chkdata = true;
            //状態名
            if (String.IsNullOrEmpty(txt_newshiarai.Text))
            {
                txt_newshiarai.Text = "";
                txt_newshiarai.Style.Add("border-color", "red !important");
                txt_newshiarai.Focus();
                chkdata = false;
            }
            if (chkdata == true)
            {
                bool fsave = SaveShiarai();
                //正常に保存する
                if (fsave == true)
                {
                    BindinShiharaiList();//20211125 add by テテ
                   //newshiarai.Style["display"] = "none";//20211125 add by テテ
                    newshiarai.Style.Add("visibility", "hidden");
                    txt_newshiarai.Text = "";//20220208 add by テテ                   
                }
                
            }
        }
        #endregion

        #region　新規modeキャンセル
        protected void btnnewshiharaiCancel_Click(object sender, EventArgs e)
        {
            //newshiarai.Style["display"] = "none";
            newshiarai.Style.Add("visibility", "hidden");
            txt_newshiarai.Text = "";
        }
        #endregion

        #region　保存
        protected bool SaveShiarai()
        {
            bool fsave = false;
            try
            {
                int retval = 0;
                int njunban = 0;
                string cCoVal = FindShiaraiCode();
                njunban = Convert.ToInt32(cCoVal);
                string shiaraimei = "";
                //状態名 sJYOTAI
                if (!String.IsNullOrEmpty(txt_newshiarai.Text.Trim()))
                {
                    if (txt_newshiarai.Text.Contains("'") == true)
                    {
                        shiaraimei = txt_newshiarai.Text.Replace("'", @"\'");

                    }
                    else
                    {
                        shiaraimei = txt_newshiarai.Text;
                    }
                    shiaraimei = shiaraimei.Trim();
                }
                else
                {
                    shiaraimei = "@null";
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
                shVal.loginId = mail;
                dt = shVal.ShiaraiListTable(datesql);
                if (dt.Rows.Count > 0)
                {
                    curDateTime = dt.Rows[0]["nowdateTime"].ToString();
                }


                string sql = "INSERT INTO m_shiharai(";
                sql += " cSHIHARAI ";
                sql += ", sSHIHARAI ";
                sql += ", dHENKOU ";
                sql += ", cHENKOUSYA ";
                sql += ")VALUES  ";
                sql += " ( '" + cCoVal + "'";
                sql += " , '" + shiaraimei + "'";
                sql += " , '" + curDateTime + "'";
                sql += " , '9999'";
                sql += "  ) ";
                sql += " ON DUPLICATE KEY UPDATE cSHIHARAI = '" + cCoVal + "'";
                sql += " ,sSHIHARAI =  '" + shiaraimei + "' ";
                sql += " ,dHENKOU =  '" + curDateTime + "' ";
                sql += " ,cHENKOUSYA =  '9999' ";

                ConstantVal.DB_NAME = db;
                shVal.loginId = mail;
                fsave = shVal.ShiaraiListListSql(sql);

            }
            catch (Exception ec)
            {

            }

            return fsave;
        }
        #endregion

        #region　コードの取得
        protected string FindShiaraiCode()
        {
            string ColVal = "";
            System.Data.DataTable dt = new System.Data.DataTable();
            string sqlStr = "SELECT cSHIHARAI,sSHIHARAI FROM m_shiharai; ";
            String id = Request.QueryString["id"];
            DataTable dt_db = JC01Login_Class.Get_DB(id);
            String db = dt_db.Rows[0]["db"].ToString();
            String mail = dt_db.Rows[0]["mail"].ToString();
            ConstantVal.DB_NAME = db;
            shVal.loginId = mail;
            dt = shVal.ShiaraiListTable(sqlStr);
            //finding the missing number 
            List<int> ListShain = new List<int>();
            foreach (System.Data.DataRow dr in dt.Rows)
            {
                ListShain.Add(int.Parse(dr["cSHIHARAI"].ToString()));
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


        #region "PopUp画面閉じる(キャンセルボタン)"
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