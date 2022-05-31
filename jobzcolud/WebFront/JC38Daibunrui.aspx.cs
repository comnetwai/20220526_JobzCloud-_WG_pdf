using Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Service;
using System.Text;

namespace jobzcolud.WebFront
{
    public partial class JC38Daibunrui : System.Web.UI.Page
    {
        JC38Daibunrui_Class shVal = new JC38Daibunrui_Class();
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
                    BindDaibunrui();
                }
                Session.Timeout = 480;

            }
        }

        #region btnDaiCross_Click　
        //add 20220328 テテ start
        protected void btnDaiCross_Click(object sender, EventArgs e)
        {
            if (Session["fChuu"] != null)
            {
                if (Session["fChuu"].ToString() == "true")
                {
                    hdnHome.Value = "Popup";
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "parentButtonClick('btnDaiClose','" + hdnHome.Value + "');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "parentButtonClick('btnMasterDaiPopupClose','" + hdnHome.Value + "');", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "parentButtonClick('btnMasterDaiPopupClose','" + hdnHome.Value + "');", true);
            }

        }
        //add 20220328 テテ end
        #endregion

        #region crossandcancel
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            //add 20220328 テテ start
            if (Session["fChuu"] != null)
            {
                if (Session["fChuu"].ToString() == "true")
                {                   
                    hdnHome.Value = "Popup";
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "parentButtonClick('btnDaiClose','" + hdnHome.Value + "');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "parentButtonClick('btnMasterDaiPopupClose','" + hdnHome.Value + "');", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "parentButtonClick('btnMasterDaiPopupClose','" + hdnHome.Value + "');", true);
            }
            //add 20220328 テテ end

        }
        #endregion

        #region Binddaibunrui()
        private void BindDaibunrui()
        {
            DataTable dtdai = new DataTable();
            string sqldai = "select cSYOUHIN_DAIGRP,sSYOUHIN_DAIGRP from m_syouhin_daigrp ";
            String id = Request.QueryString["id"];
            DataTable dt_db = JC01Login_Class.Get_DB(id);
            String db = dt_db.Rows[0]["db"].ToString();
            String mail = dt_db.Rows[0]["mail"].ToString();
            ConstantVal.DB_NAME = db;

            shVal.loginId = mail;
            dtdai = shVal.DaibunruiList(sqldai);
            gvDaibunList.DataSource = dtdai;
            gvDaibunList.DataBind();

            if (dtdai.Rows.Count > 10)
            {
                Gvdiv.Style["height"] = "380px";
            }
            else
            {
                Gvdiv.Style["height"] = "auto";
            }
        }
        #endregion

        #region newdaibunrui
        protected void btnnewDaibun_Click(object sender, EventArgs e)
        {
            updDaibunList.Update();
            this.BindDaibunrui();
            newDaibun.Style.Add("visibility", "visible");
            newDaibun.Style["display"] = "block";
            txt_newDaibun.Focus();
            if (gvDaibunList.Rows.Count > 0)
            {
                newDaibun.Style.Add("margin-left", "22px;");
                newDaibun.Style.Add("margin-top", "0px;");
                newDaibun.Style.Add("margin-bottom", "10px;");
            }
            else
            {
                newDaibun.Style.Add("margin-left", "22px;");
                newDaibun.Style.Add("margin-top", "10px;");
                newDaibun.Style.Add("margin-bottom", "10px;");
            }
        }
        #endregion

        #region onrowdatabound
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
                            if (e.Row.RowIndex == gvDaibunList.EditIndex)
                            {
                                e.Row.Cells[1].Visible = true;
                                e.Row.Cells[2].Visible = false;

                                TextBox tbox = (TextBox)e.Row.FindControl("txtsSYOUHIN");
                                tbox.Focus();
                                tbox.Attributes.Add("onfocus", "selectText("+ gvDaibunList.EditIndex + ");");
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

        #region onrowediting
        protected void OnRowEditing(object sender, GridViewEditEventArgs e)
        {
            fcol = "Edit";
            gvDaibunList.EditIndex = e.NewEditIndex;
            this.BindDaibunrui();
            newDaibun.Style.Add("visibility", "hidden");
            txt_newDaibun.Text = "";
        }
        #endregion

        #region onrowdeleting
        protected void OnRowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //fcol = "Delete";
            //int index = Convert.ToInt32(e.RowIndex);
            //string cCoVal = gvdaibunrilist.DataKeys[index].Values[0].ToString();
            ////mysqlcon.ConnectionString = constr;
            //string chubundelete = "Delete from m_syouhin_tyuugrp where cSYOUHIN_DAIGRP = '" + cCoVal + "'";
            //shVal.loginId = Session["LoginId"].ToString();
            //Boolean fchubundelete = shVal.DaibunruiSaveList(chubundelete);
            //string sqldelete = " DELETE FROM m_syouhin_daigrp WHERE cSYOUHIN_DAIGRP = '" + cCoVal + "'";
            //shVal.loginId = Session["LoginId"].ToString();
            //Boolean fdelete = shVal.DaibunruiSaveList(sqldelete);
            //this.BindDaibunrui();
        }
        #endregion

        #region gvdaibunlist_rowcommand
        protected void gvDaibunList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //int index = int.Parse(e.CommandArgument.ToString());
            if (e.CommandName == "Select")
            {               
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = gvDaibunList.Rows[rowIndex];
                string cdaibun = row.Cells[0].Text;
                string sdaibun = (row.FindControl("txtsSYOUHIN") as TextBox).Text;
                Session["cSYOUHIN_DAIGRP"] = cdaibun;
                Session["sSYOUHIN_DAIGRP"] = sdaibun;              
                //テテ add 20220325 start 
                Session["cSYOUHIN_TYUUGRP"] = null;
                Session["sSYOUHIN_TYUUGRP"] = null;
                if (Session["fChuu"] != null)
                {
                    if (Session["fChuu"].ToString() == "true")
                    {
                        hdnHome.Value = "Popup";
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "parentButtonClick('btnDaiSelect','" + hdnHome.Value + "');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "parentButtonClick('btnDaiSelect','" + hdnHome.Value + "');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "parentButtonClick('btnDaiSelect','" + hdnHome.Value + "');", true);
                }
                //テテ add 20220325 end 
            }
            else if (e.CommandName == "SelecteChuu")
            {

                int rowIndex = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = gvDaibunList.Rows[rowIndex];
                string cdaibun = row.Cells[0].Text;
                string sdaibun = (row.FindControl("txtsSYOUHIN") as TextBox).Text;
                Session["cSYOUHIN_DAIGRP"] = cdaibun;
                Session["sSYOUHIN_DAIGRP"] = sdaibun;

                newDaibun.Style.Add("visibility", "hidden");
                txt_newDaibun.Text = "";

                SessionUtility.SetSession("HOME", "Popup");
                ifShinkiPopup.Style["width"] = "470px";
                ifShinkiPopup.Style["height"] = "100vh";
                String id = Request.QueryString["id"];
                ifShinkiPopup.Src = "JC39Chubunrui.aspx?id=" + id;
                mpeShinkiPopup.Show();
                updShinkiPopup.Update();
            }
        }
        #endregion

        #region newsave
        protected void btnsavedaibun_Click(object sender, EventArgs e)
        {
            bool chkdata = true;
            //状態名
            if (String.IsNullOrEmpty(txt_newDaibun.Text))
            {
                txt_newDaibun.Text = "";
                txt_newDaibun.Style.Add("border-color", "red !important");
                txt_newDaibun.Focus();
                chkdata = false;
            }
            if (chkdata == true)
            {
                bool fsave = SaveDaibunrui();
                //正常に保存する
                if (fsave == true)
                {
                    BindDaibunrui();
                    newDaibun.Style.Add("visibility", "hidden");
                    txt_newDaibun.Text = "";
                }

            }
        }

        protected bool SaveDaibunrui()
        {
            bool fsave = false;
            try
            {
                int retval = 0;
                int njunban = 0;
                string cCoVal = FindDaibunruiCode();
                njunban = Convert.ToInt32(cCoVal);
                string daibunruimei = "";
                //状態名 sJYOTAI
                if (!String.IsNullOrEmpty(txt_newDaibun.Text.Trim()))
                {
                    if (txt_newDaibun.Text.Contains("'") == true)
                    {
                        daibunruimei = txt_newDaibun.Text.Replace("'", @"\'");

                    }
                    else
                    {
                        daibunruimei = txt_newDaibun.Text;
                    }
                    daibunruimei = daibunruimei.Trim();
                }
                else
                {
                    daibunruimei = "@null";
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
                ConstantVal.DB_NAME = db;
                dt = shVal.DaibunruiList(datesql);
                //MySqlDataAdapter adap = new MySqlDataAdapter(datesql, constr);
                //adap.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    curDateTime = dt.Rows[0]["nowdateTime"].ToString();
                }
                //chenkousya
                string chenkoudata = "";
                DataTable dtchenkou = new DataTable();
                string chenkousql = "select cHENKOUSYA from m_j_tantousha where sMAIL='" + shVal.loginId + "' ";
                dtchenkou = shVal.DaibunruiList(chenkousql);
                if (dtchenkou.Rows.Count > 0)
                {
                    chenkoudata = dtchenkou.Rows[0]["cHENKOUSYA"].ToString();
                }

                string sql = "INSERT INTO m_syouhin_daigrp(";
                sql += " cSYOUHIN_DAIGRP ";
                sql += ", sSYOUHIN_DAIGRP ";
                sql += ", dHENKOU ";
                sql += ", cHENKOUSYA ";
                sql += ", nJUNBAN ";
                sql += ")VALUES  ";
                sql += " ( '" + cCoVal + "'";
                sql += " , '" + daibunruimei + "'";
                sql += " , '" + curDateTime + "'";
                sql += " , '" + chenkoudata + "'";
                sql += " , '" + njunban + "' ";
                sql += "  ) ";
                sql += " ON DUPLICATE KEY UPDATE cSYOUHIN_DAIGRP = '" + cCoVal + "'";
                sql += " ,sSYOUHIN_DAIGRP =  '" + daibunruimei + "' ";
                sql += " ,dHENKOU =  '" + curDateTime + "' ";
                sql += " ,cHENKOUSYA =  '" + chenkoudata + "' ";
                sql += " ,nJUNBAN ='" + njunban + "' ";

                shVal.loginId = mail;
                ConstantVal.DB_NAME = db;
                fsave = shVal.DaibunruiSaveList(sql);

            }
            catch (Exception ec)
            {

            }

            return fsave;
        }

        protected string FindDaibunruiCode()
        {
            string ColVal = "";
            DataTable dt = new DataTable();
            string sqlStr = "SELECT cSYOUHIN_DAIGRP,sSYOUHIN_DAIGRP FROM m_syouhin_daigrp; ";
            String id = Request.QueryString["id"];
            DataTable dt_db = JC01Login_Class.Get_DB(id);
            String db = dt_db.Rows[0]["db"].ToString();
            String mail = dt_db.Rows[0]["mail"].ToString();
            ConstantVal.DB_NAME = db;
            shVal.loginId = mail;
            ConstantVal.DB_NAME =db;
            dt = shVal.DaibunruiList(sqlStr);

            //finding the missing number 
            List<int> ListShain = new List<int>();
            foreach (System.Data.DataRow dr in dt.Rows)
            {
                ListShain.Add(int.Parse(dr["cSYOUHIN_DAIGRP"].ToString()));
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

        #region btnupdate
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            updDaibunList.Update();
            this.BindDaibunrui();

            GridViewRow row = (sender as Button).NamingContainer as GridViewRow;
            string cCoVal = (row.Cells[0].Controls[0] as TextBox).Text;
            string sDaibunruiVal = (row.FindControl("txtsSYOUHIN") as TextBox).Text;

            //mysqlcon.ConnectionString = constr;
            if (sDaibunruiVal != "")
            {
                if (sDaibunruiVal.Contains("'") == true)
                {
                    sDaibunruiVal = sDaibunruiVal.Replace("'", @"\'");

                }
            }
            var textByteCount = Encoding.Default.GetByteCount(sDaibunruiVal);
            if (textByteCount > 28)
            {
                string textCount = sDaibunruiVal;
                while (Encoding.Default.GetByteCount(textCount) > 28)
                {
                    textCount = textCount.Substring(0, textCount.Length - 1);
                }
                sDaibunruiVal = textCount;
            }
            string sqlupdate = " Update m_syouhin_daigrp SET sSYOUHIN_DAIGRP = '" + sDaibunruiVal + "' WHERE cSYOUHIN_DAIGRP = '" + cCoVal + "'";
            String id = Request.QueryString["id"];
            DataTable dt_db = JC01Login_Class.Get_DB(id);
            String db = dt_db.Rows[0]["db"].ToString();
            String mail = dt_db.Rows[0]["mail"].ToString();
            ConstantVal.DB_NAME = db;
            shVal.loginId = mail;
            ConstantVal.DB_NAME = db;
            Boolean fupdate = shVal.DaibunruiSaveList(sqlupdate);
            gvDaibunList.EditIndex = -1;
            this.BindDaibunrui();
        }
        #endregion

        #region　btnsavecancel
        protected void btnSaveCancel_Click(object sender, EventArgs e)
        {
            updDaibunList.Update();
            this.BindDaibunrui();
        }
        #endregion

        #region btndeleteclick
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            LinkButton lkb_delete = sender as LinkButton;
            GridViewRow gvRow = (GridViewRow)lkb_delete.NamingContainer;
            string cCoVal = gvRow.Cells[0].Text;
            Button ok = gvDaibunList.Rows[gvRow.RowIndex].FindControl("BT_DeleteOk") as Button;
            Button cancel = gvDaibunList.Rows[gvRow.RowIndex].FindControl("BT_DeleteCancel") as Button;
            updDaibunList.Update();

            //20220329
            DataTable dtshohin = new DataTable();
            string shohin_sql = "select cSYOUHIN from m_syouhin where cSYOUHIN_DAIGRP ='" + cCoVal + "'";
            String id = Request.QueryString["id"];
            DataTable dt_db = JC01Login_Class.Get_DB(id);
            String db = dt_db.Rows[0]["db"].ToString();
            String mail = dt_db.Rows[0]["mail"].ToString();
            ConstantVal.DB_NAME = db;
            shVal.loginId = mail;
            dtshohin = shVal.DaibunruiList(shohin_sql);
            if (dtshohin.Rows.Count > 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowMailKakuninMessage",
                                        "MailKakuninMsgBox('その分類は使用中です。削除できません。','" + cancel.ClientID + "');", true);
            }
            else
            {
                DataTable dtsub = new DataTable();
                string subchuu_sql = "select cSYOUHIN_TYUUGRP from m_syouhin_tyuugrp where cSYOUHIN_DAIGRP ='" + cCoVal + "'";
                shVal.loginId = mail;
                ConstantVal.DB_NAME = db;
                dtsub = shVal.DaibunruiList(subchuu_sql);
                if (dtsub.Rows.Count > 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowAnkenChangeConfirmMessage",
                             "DeleteConfirmBox('関連付けの中分類もすべて削除されます。よろしいですか？','" + ok.ClientID + "','" + cancel.ClientID + "');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowAnkenChangeConfirmMessage",
                             "DeleteConfirmBox('削除してもよろしいでしょうか？','" + ok.ClientID + "','" + cancel.ClientID + "');", true);
                }
            }
        }

        protected void BT_DeleteOk_Click(object sender, EventArgs e)
        {
            //大分類を削除すると関連している中分類も削除する
            GridViewRow row = (sender as Button).NamingContainer as GridViewRow;
            string cCoVal = row.Cells[0].Text;
            string chubundelete = "Delete from m_syouhin_tyuugrp where cSYOUHIN_DAIGRP = '" + cCoVal + "'";
            String id = Request.QueryString["id"];
            DataTable dt_db = JC01Login_Class.Get_DB(id);
            String db = dt_db.Rows[0]["db"].ToString();
            String mail = dt_db.Rows[0]["mail"].ToString();
            ConstantVal.DB_NAME = db;
            shVal.loginId = mail;
            Boolean fchubundelete = shVal.DaibunruiSaveList(chubundelete);
            string sqldelete = " DELETE FROM m_syouhin_daigrp WHERE cSYOUHIN_DAIGRP = '" + cCoVal + "'";
            shVal.loginId = mail;
            ConstantVal.DB_NAME = db;
            Boolean fdelete = shVal.DaibunruiSaveList(sqldelete);
            updDaibunList.Update();
            this.BindDaibunrui();

            newDaibun.Style.Add("visibility", "hidden");
            txt_newDaibun.Text = "";

            //20220329 テテ start
            string cdai = (string)Session["cSYOUHIN_DAIGRP"];
            string sdai = (string)Session["sSYOUHIN_DAIGRP"];

            if (cdai == cCoVal)
            {               
                Session["cSYOUHIN_DAIGRP"] = null;
                Session["sSYOUHIN_DAIGRP"] = null;
                Session["cSYOUHIN_TYUUGRP"] = null;
                Session["cSYOUHIN_TYUUGRP"] = null;
            }
            //20220329 テテ end
        }
        #endregion

        #region　btnnewcancel
        protected void btncanceldaibun_Click(object sender, EventArgs e)
        {
            //newshiarai.Style["display"] = "none";
            newDaibun.Style.Add("visibility", "hidden");
            txt_newDaibun.Text = "";
        }
        #endregion

        #region 支払方法テキスト入力制限全角14文字半角28文字
        protected void txt_newDaibun_TextChanged(object sender, EventArgs e)
        {
            var textByteCount = Encoding.Default.GetByteCount(txt_newDaibun.Text);
            if (textByteCount > 28)
            {
                string textCount = txt_newDaibun.Text;
                while (Encoding.Default.GetByteCount(textCount) > 28)
                {
                    textCount = textCount.Substring(0, textCount.Length - 1);
                }
                txt_newDaibun.Text = textCount;
            }
        }
        #endregion

        #region 中分類popup画面
        protected void btnClose_Click(object sender, EventArgs e)
        {
            ifShinkiPopup.Src = "";
            mpeShinkiPopup.Hide();
            updShinkiPopup.Update();            
            updDaibunList.Update();
        }

        protected void btnChuuSelect_Click(object sender, EventArgs e)
        {
            
            if (Session["fDoubleChuu"] != null)
            {
                if (Session["fDoubleChuu"].ToString() == "true")
                {
                    hdnHome.Value = "Popup";
                }
            }
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "parentButtonClick('btnChuuSelect','" + hdnHome.Value + "');", true);
        }
        #endregion
    }
}