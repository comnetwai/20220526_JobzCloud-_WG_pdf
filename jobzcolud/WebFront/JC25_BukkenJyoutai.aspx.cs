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
    public partial class JC25_BukkenJyoutai : System.Web.UI.Page
    {
        string fcol = "";
        JC25BukkenJyoutai_Class mtVal = new JC25BukkenJyoutai_Class();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(Request.QueryString["id"]))  /*20211112 Added Eaindray+PhooPhoo*/
            {
                if (!this.IsPostBack)
                {
                    if (SessionUtility.GetSession("HOME") != null)  //20211014 MiMi Added
                    {
                        hdnHome.Value = SessionUtility.GetSession("HOME").ToString();
                        SessionUtility.SetSession("HOME", null);
                    }
                    BindinJoytaiList();
                     ScriptManager.RegisterStartupScript(this, GetType(), "setscroll", "SetScrollTop();", true);
                }
                Session.Timeout = 480;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "parentButtonClick('btnToLogin','" + hdnHome.Value + "');", true);
            }
        }

        protected void btnNewJoytai_Click(object sender,EventArgs e)
        {
            updJyoutailist.Update();
            this.BindinJoytaiList();
            newjyotai.Style.Add("visibility", "visible");
            newjyotai.Style["display"] = "block";
            txt_newJoytai.Focus();
            if (gvJyotailist.Rows.Count > 0)
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
        protected void btnNewjoytaiSave_Click(object sender, EventArgs e)
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

                #region 20220321 MiMi Deleted
                ////正常に保存する
                //if (fsave == true)
                //{
                //    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "parentButtonClick('btnJoutaiSelect','" + hdnHome.Value + "');", true);
                //}
                ////エラーの場合
                //else
                //{
                //    //ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "parentButtonClick('btnAnkenNyuuryokuSave');", true);
                //}
                #endregion

                if (fsave == true)
                {
                    BindinJoytaiList();
                    newjyotai.Style.Add("visibility", "hidden");
                    txt_newJoytai.Text = "";
                }
            }
            this.BindinJoytaiList(); /*20211112 Added Eaindray+PhooPhoo*/
        }

        protected string FindKyotenCode()
        {
            string ColVal = "";
            System.Data.DataTable dt = new System.Data.DataTable();
            string sqlStr = "SELECT cJYOTAI,sJYOTAI FROM m_buken_jyotai; ";

            String id = Request.QueryString["id"];
            DataTable dt_db = JC01Login_Class.Get_DB(id);
            String db = dt_db.Rows[0]["db"].ToString();
            String mail = dt_db.Rows[0]["mail"].ToString();
            ConstantVal.DB_NAME = db;

            mtVal.loginId = mail;
            dt = mtVal.BukkenJyoutaiListTable(sqlStr);
            //MySqlDataAdapter adap = new MySqlDataAdapter(sqlStr, constr);
            // adap.Fill(dt);
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
            return ColVal;
        }

        //20220324
        private string getNJunban(string code)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            string junban_sql = "select ifnull(MAX(nJUNBAN),0)+1 as nJUNBAN from m_buken_jyotai ;";

            String id = Request.QueryString["id"];
            DataTable dt_db = JC01Login_Class.Get_DB(id);
            String db = dt_db.Rows[0]["db"].ToString();
            String mail = dt_db.Rows[0]["mail"].ToString();
            ConstantVal.DB_NAME = db;

            mtVal.loginId = mail;
            dt = mtVal.BukkenJyoutaiListTable(junban_sql);
            string njunban = "";
            foreach (System.Data.DataRow dr in dt.Rows)
            {
             njunban=dr["nJUNBAN"].ToString();
            }
            return njunban;
        }

        protected bool SaveKyoten()
        {
            bool fsave = false;
            try
            {
                int retval = 0;
                int njunban = 0;
                string cCoVal = FindKyotenCode();
                //njunban = Convert.ToInt32(cCoVal);
                njunban = Convert.ToInt32(getNJunban(cCoVal)); //20220324
                string joytaimei = "";
                //状態名 sJYOTAI
                if (!String.IsNullOrEmpty(txt_newJoytai.Text.Trim()))
                {
                    joytaimei = txt_newJoytai.Text.Trim();
                }
                else
                {
                    joytaimei = "@null";
                }

                String id = Request.QueryString["id"];
                DataTable dt_db = JC01Login_Class.Get_DB(id);
                String db = dt_db.Rows[0]["db"].ToString();
                String mail = dt_db.Rows[0]["mail"].ToString();
                ConstantVal.DB_NAME = db;

                JC_ClientConnecction_Class jc = new JC_ClientConnecction_Class();
                jc.loginId = mail;
                DataTable dt_loginuser = jc.GetLoginUserCodeFromClientDB();
                String cHenkou = dt_loginuser.Rows[0]["code"].ToString();

                //find current date time
                //string curDateTime = "";
                //DataTable dt = new DataTable();
                //string datesql = " SELECT NOW() as nowdateTime;  ";
                //mtVal.loginId = Session["LoginId"].ToString();
                //dt = mtVal.BukkenJyoutaiListTable(datesql);
                ////MySqlDataAdapter adap = new MySqlDataAdapter(datesql, constr);
                ////adap.Fill(dt);
                //if (dt.Rows.Count > 0)
                //{
                //    curDateTime = dt.Rows[0]["nowdateTime"].ToString();
                //}
                string sql = "INSERT INTO m_buken_jyotai(";
                sql += " cJYOTAI ";
                sql += ", sJYOTAI ";
                sql += ", nJUNBAN ";
                sql += ", dHENKOU ";
                sql += ", cHENKOUSYA ";
                sql += ", nKAKUDO ";
                sql += ", fKENSAKU ";
                sql += ")VALUES  ";
                sql += " ( '" + cCoVal + "'";
                sql += " , '" + joytaimei + "'";
                sql += " ," + njunban + "";
                sql += " , Now()";
                sql += " , '"+cHenkou+"'";
                sql += " ,null";
                sql += " ,null";
                sql += "  ) ";
                sql += " ON DUPLICATE KEY UPDATE cJYOTAI = '" + cCoVal + "'";
                sql += " ,sJYOTAI =  '" + joytaimei + "' ";
                sql += " ,nJUNBAN =  " + njunban + " ";
                sql += " ,dHENKOU =  Now() ";
                sql += " ,cHENKOUSYA =  '"+cHenkou+"' ";
                sql += " ,nKAKUDO = null ";
                sql += " ,fKENSAKU = null ";

                //mysqlcon.ConnectionString = constr;

                //MySqlCommand myCommand = new MySqlCommand(sql, mysqlcon);
                //mysqlcon.Open();

                //retval = myCommand.ExecuteNonQuery();
                ConstantVal.DB_NAME = db;
                mtVal.loginId = mail;
                fsave = mtVal.BukkenJyotaiListSql(sql);
                if (fsave)
                {
                    Session["cJyotai"] = cCoVal;
                    Session["sJyotai"] = joytaimei;  //20211028 MiMi Added
                }
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

        protected bool UpdateKyoten() /*20211112 Added Eaindray+PhooPhoo*/
        {
            bool fsave = false;
            try
            {
                int retval = 0;
                int njunban = 0;
                string cJyoutai = gvJyotailist.DataKeys[gvJyotailist.EditIndex].Values[0].ToString();
                //njunban = Convert.ToInt32(cJyoutai);
                string cCoVal = FindKyotenCode(); //20220324
                njunban = Convert.ToInt32(getNJunban(cCoVal)); //20220324
                string joytaimei = "";
                //状態名 sJYOTAI
                GridViewRow row = gvJyotailist.Rows[gvJyotailist.EditIndex];
                string sJoutai = (row.FindControl("txtsJYOTAI") as TextBox).Text;
                if (!String.IsNullOrEmpty(sJoutai.Trim()))
                {
                    joytaimei = sJoutai.Trim();
                }
                else
                {
                    joytaimei = "@null";
                }
                //find current date time
                //string curDateTime = "";
                //DataTable dt = new DataTable();
                //string datesql = " SELECT NOW() as nowdateTime;  ";
                //mtVal.loginId = Session["LoginId"].ToString();
                //dt = mtVal.BukkenJyoutaiListTable(datesql);
                //if (dt.Rows.Count > 0)
                //{
                //    curDateTime = dt.Rows[0]["nowdateTime"].ToString();
                //}

                String id = Request.QueryString["id"];
                DataTable dt_db = JC01Login_Class.Get_DB(id);
                String db = dt_db.Rows[0]["db"].ToString();
                String mail = dt_db.Rows[0]["mail"].ToString();
                ConstantVal.DB_NAME = db;

                JC_ClientConnecction_Class jc = new JC_ClientConnecction_Class();
                jc.loginId = mail;
                DataTable dt_loginuser = jc.GetLoginUserCodeFromClientDB();
                String cHenkou = dt_loginuser.Rows[0]["code"].ToString();

                string q_update = "UPDATE m_buken_jyotai SET " +
                                 "cJYOTAI='" + cJyoutai + "'," +
                                 "sJYOTAI='" + joytaimei + "'," +
                                 "nJUNBAN='" + njunban + "'," +
                                 "cHENKOUSYA='" + cHenkou + "'," +
                                 "dHENKOU= NOW() " +
                                 "WHERE cJYOTAI='"+cJyoutai+"';";
               
                mtVal.loginId = mail;
                ConstantVal.DB_NAME = db;
                fsave = mtVal.BukkenJyotaiListSql(q_update);
            }
            catch (Exception ec)
            {

            }
            return fsave;
        }
        protected void btnCancel_Click(object sender,EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "parentButtonClick('btnClose','" + hdnHome.Value + "');", true);
        }

        #region "データ変更するとき、テキストを入力できるようにする、リンクボタンを非表示する"
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

                                TextBox tbox = (TextBox)e.Row.FindControl("txtsJYOTAI");
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
        protected void OnRowEditing(object sender, GridViewEditEventArgs e)
        {
            fcol = "Edit";
            gvJyotailist.EditIndex = e.NewEditIndex;
            this.BindinJoytaiList();

            newjyotai.Style.Add("visibility", "hidden");

            txt_newJoytai.Text = "";
        }
        #endregion

        #region 20220428 Chaw Deleted
        //protected void lkbtnUpdate_Click(object sender,EventArgs e) /*20211112 Added Eaindray+PhooPhoo*/
        //{
        //    string cJyoutai = gvJyotailist.DataKeys[gvJyotailist.EditIndex].Values[0].ToString();
        //    GridViewRow row = gvJyotailist.Rows[gvJyotailist.EditIndex];
        //    string sJoutai = (row.FindControl("txtsJYOTAI") as TextBox).Text;
        //    bool chkdata = true;
        //    //状態名
        //    if (String.IsNullOrEmpty(sJoutai))
        //    {
        //        sJoutai = "";
        //        (row.FindControl("txtsJYOTAI") as TextBox).Style.Add("border-color", "red !important");
        //        //(row.FindControl("txtsJYOTAI") as TextBox).Style.Add("border-color", "Red");
        //        (row.FindControl("txtsJYOTAI") as TextBox).Focus();
        //        chkdata = false;
        //    }

        //    if (chkdata == true)
        //    {
        //        bool fsave = UpdateKyoten();

        //        gvJyotailist.EditIndex = -1;
        //        #region 20220321 MiMi Deleted
        //        ////正常に保存する
        //        //if (fsave == true)
        //        //{
        //        //    Session["cJyotai"] = cJyoutai;
        //        //    Session["sJyotai"] = sJoutai;  //20211028 MiMi Added
        //        //    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "parentButtonClick('btnJoutaiSelect','" + hdnHome.Value + "');", true);
        //        //}
        //        ////エラーの場合
        //        //else
        //        //{
        //        //    //ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "parentButtonClick('btnAnkenNyuuryokuSave');", true);
        //        //}


        //    }
        //    this.BindinJoytaiList(); /*20211115 Added Eaindray+Phoo #To Close POPUP after edit# */
        //}
        #endregion

        private void BindinJoytaiList()
        {
            String id = Request.QueryString["id"];
            DataTable dt_db = JC01Login_Class.Get_DB(id);
            String db = dt_db.Rows[0]["db"].ToString();
            String mail = dt_db.Rows[0]["mail"].ToString();
            ConstantVal.DB_NAME = db;

            DataTable dt = new DataTable();
            string sqlstring = " SELECT cJYOTAI,Replace(Replace(ifnull(sJYOTAI,''),'<','&lt'),'>','&gt') sJYOTAI FROM m_buken_jyotai;  ";
            mtVal.loginId = mail;
            dt = mtVal.BukkenJyoutaiListTable(sqlstring);
            //MySqlDataAdapter adap = new MySqlDataAdapter(sqlstring, constr);
            //adap.Fill(dt);

            // newjyotai.Attributes.Add("style", "margin-top:10px;");
            gvJyotailist.DataSource = dt;
            gvJyotailist.DataBind();
        }

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
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "parentButtonClick('btnJoutaiSelect','" + hdnHome.Value + "');", true);
                // ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Name: " + name + "\\nCountry: " + country + "');", true);
            }
            else if (e.CommandName == "Update") /*20211112 Added Eaindray+PhooPhoo*/
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = gvJyotailist.Rows[rowIndex];
                if ((row.FindControl("txtsJYOTAI") as TextBox).Text=="")
                {
                    (row.FindControl("txtsJYOTAI") as TextBox).Style.Add("border-color", "red !important");
                }

            }
        }

        #region OnRowDeleting
        protected void OnRowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //fcol = "Delete";
            //int index = Convert.ToInt32(e.RowIndex);
            //string cCoVal = gvJyotailist.DataKeys[index].Values[0].ToString();
            //// mysqlcon.ConnectionString = constr;
            //string sqldelete = " DELETE FROM m_buken_jyotai WHERE cJYOTAI = '" + cCoVal + "'";
            //mtVal.loginId = Session["LoginId"].ToString();
            //Boolean fdelete = mtVal.BukkenJyotaiListSql(sqldelete);
            ////MySqlCommand myCommand = new MySqlCommand(sqldelete, mysqlcon);
            ////mysqlcon.Open();
            ////myCommand.ExecuteNonQuery();
            ////mysqlcon.Close();
            //this.BindinJoytaiList();

        }
        #endregion

        #region　新規modeキャンセル
        protected void btnnewJoutaiCancel_Click(object sender, EventArgs e)
        {
            newjyotai.Style.Add("visibility", "hidden");
            txt_newJoytai.Text = "";
        }
        #endregion

        #region 変更modeキャンセル
        protected void btnSaveCancel_Click(object sender, EventArgs e)
        {
            updJyoutailist.Update();
            this.BindinJoytaiList();
        }
        #endregion

        #region btnDelete_Click
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            LinkButton lkb_delete = sender as LinkButton;
            GridViewRow gvRow = (GridViewRow)lkb_delete.NamingContainer;
            string cCoVal = gvRow.Cells[0].Text;
            Button ok = gvJyotailist.Rows[gvRow.RowIndex].FindControl("BT_DeleteOk") as Button;
            Button cancel = gvJyotailist.Rows[gvRow.RowIndex].FindControl("BT_DeleteCancel") as Button;
            updJyoutailist.Update();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowAnkenChangeMessage",
                     "DeleteConfirmBox('削除してもよろしいでしょうか？','" + ok.ClientID + "','" + cancel.ClientID + "');", true);

        }
        #endregion

        #region BT_DeleteOk_Click
        protected void BT_DeleteOk_Click(object sender, EventArgs e)
        {
            GridViewRow row = (sender as Button).NamingContainer as GridViewRow;
            string cCoVal = row.Cells[0].Text;
            string sqldelete = " DELETE FROM m_buken_jyotai WHERE cJYOTAI = '" + cCoVal + "'";

            String id = Request.QueryString["id"];
            DataTable dt_db = JC01Login_Class.Get_DB(id);
            String db = dt_db.Rows[0]["db"].ToString();
            String mail = dt_db.Rows[0]["mail"].ToString();
            ConstantVal.DB_NAME = db;

            mtVal.loginId = mail;
            Boolean fdelete = mtVal.BukkenJyotaiListSql(sqldelete);
            updJyoutailist.Update();
            this.BindinJoytaiList();

            newjyotai.Style.Add("visibility", "hidden");
            txt_newJoytai.Text = "";
        }
        #endregion
    }
}