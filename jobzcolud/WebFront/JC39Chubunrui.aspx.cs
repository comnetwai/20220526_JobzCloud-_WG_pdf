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
    public partial class JC39Chubunri : System.Web.UI.Page
    {
        JC38Daibunrui_Class shVal = new JC38Daibunrui_Class();
        string fcol = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(Request.QueryString["id"]))
            {
                if (!this.IsPostBack)
                {
                    if (SessionUtility.GetSession("HOME") != null)
                    {
                        hdnHome.Value = SessionUtility.GetSession("HOME").ToString();
                        SessionUtility.SetSession("HOME", null);
                    }
                    BindChuubunrui();
                }
                Session.Timeout = 480;
            }
            else
            {
                Response.Redirect("JC01Login.aspx");
            }
        }

        #region BindChuubunrui()
        private void BindChuubunrui()
        {
            string sql_chuu = "";
            DataTable dt_chuu = new DataTable();

            if (Session["cSYOUHIN_DAIGRP"] != null)
            {
                string cDaibunrui = Session["cSYOUHIN_DAIGRP"].ToString();
                string sDaibunrui = Session["sSYOUHIN_DAIGRP"].ToString();
                lblDaibunrui.Text = sDaibunrui;
                txt_daibunrui.Text = cDaibunrui;

                sql_chuu = "select cSYOUHIN_TYUUGRP,sSYOUHIN_TYUUGRP from m_syouhin_tyuugrp where cSYOUHIN_DAIGRP='" + cDaibunrui + "'";
            }
            else
            {
                lblDaibunrui.Text = "";
                txt_daibunrui.Text = "";
                sql_chuu = "select cSYOUHIN_TYUUGRP,sSYOUHIN_TYUUGRP from m_syouhin_tyuugrp ";
            }
            String id = Request.QueryString["id"];
            DataTable dt_db = JC01Login_Class.Get_DB(id);
            String db = dt_db.Rows[0]["db"].ToString();
            String mail = dt_db.Rows[0]["mail"].ToString();
            ConstantVal.DB_NAME = db;
            shVal.loginId = mail;
            ConstantVal.DB_NAME = db;//form login
            dt_chuu = shVal.DaibunruiList(sql_chuu);
            gvchuubunrilist.DataSource = dt_chuu;
            gvchuubunrilist.DataBind();
        }
        #endregion

        #region btnCross_Click
        protected void btnCross_Click(object sender, EventArgs e)
        {
            if (hdnHome.Value == "Master")
            {
                if (Session["fDai"] != null)
                {
                    if (Session["fDai"].ToString() == "false")
                    {
                        if (Session["cSYOUHIN_DAIGRP"] != null)
                        {
                            Session["cSYOUHIN_DAIGRP"] = null;
                            Session["sSYOUHIN_DAIGRP"] = null;
                        }
                    }
                }
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "parentButtonClick('btnMasterChuuPopupClose','" + hdnHome.Value + "');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "parentButtonClick('btnClose','" + hdnHome.Value + "');", true);
            }
            //ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "parentButtonClick('btnClose','" + hdnHome.Value + "');", true);
        }
        #endregion

        #region btnnewChuubunrui_Click
        protected void btnnewChuubunrui_Click(object sender, EventArgs e)
        {
            if (txt_daibunrui.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowAnkenChangeMessage",
                "ShowDaibunruiNullMessage('商品大分類を選択されていません。商品大分類を選択しますか？','" + btnYes.ClientID + "','" + btnCancel.ClientID + "');", true);
            }
            else
            {
                txt_newChuubunrui.Focus();
                updChuubunruiList.Update();
                this.BindChuubunrui();

                newDiv.Style.Add("visibility", "visible");
                newDiv.Style["display"] = "block";
                if (gvchuubunrilist.Rows.Count > 0)
                {
                    newDiv.Style.Add("margin-left", "25px;");
                    newDiv.Style.Add("margin-top", "0px;");
                    newDiv.Style.Add("margin-bottom", "10px;");
                }
                else
                {
                    newDiv.Style.Add("margin-left", "25px;");
                    newDiv.Style.Add("margin-top", "10px;");
                    newDiv.Style.Add("margin-bottom", "10px;");
                }
            }
        }
        #endregion

        #region btnYes_Click
        protected void btnYes_Click(object sender, EventArgs e)
        {
            SessionUtility.SetSession("HOME", "Master");
            ifDaibunruiPopup.Style["width"] = "470px";
            ifDaibunruiPopup.Style["height"] = "100vh";

            String id = Request.QueryString["id"];
            ifDaibunruiPopup.Src = "JC38Daibunrui.aspx?id=" + id;
            mpeDaibunruiPopup.Show();

            //lblsKYOTEN.Attributes.Add("onClick", "BtnClick('MainContent_btnKyotenAdd')");
            updDaibunruiPopup.Update();
        }
        #endregion

        protected void btnDialogCancel_Click(object sender, EventArgs e)
        {
            //updChuubunruiList.Update();
        }

        #region btnCancel_Click
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            if (hdnHome.Value == "Master")
            {
                if (Session["fDai"] != null)
                {
                    if (Session["fDai"].ToString() == "false")
                    {
                        if (Session["cSYOUHIN_DAIGRP"] != null)
                        {
                            Session["cSYOUHIN_DAIGRP"] = null;
                            Session["sSYOUHIN_DAIGRP"] = null;
                        }
                    }
                }
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "parentButtonClick('btnMasterChuuPopupClose','" + hdnHome.Value + "');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "parentButtonClick('btnClose','" + hdnHome.Value + "');", true);
            }
        }
        #endregion

        #region btnEditCancel_Click
        protected void btnEditCancel_Click(object sender, EventArgs e)
        {
            //newDiv.Style["display"] = "none";
            updChuubunruiList.Update();
            this.BindChuubunrui();

            newDiv.Style.Add("visibility", "hidden");
            txt_newChuubunrui.Text = "";
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
                            
                            if (e.Row.RowIndex == gvchuubunrilist.EditIndex)
                            {
                                e.Row.Cells[1].Visible = true;
                                e.Row.Cells[2].Visible = false;

                                TextBox tbox = (TextBox)e.Row.FindControl("txt_editChuu");
                                tbox.Focus();

                                tbox.Attributes.Add("onfocus", "selectText(" + gvchuubunrilist.EditIndex + ");");

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
            gvchuubunrilist.EditIndex = e.NewEditIndex;
            this.BindChuubunrui();

            newDiv.Style.Add("visibility", "hidden");
            txt_newChuubunrui.Text = "";
        }
        #endregion

        #region onrowdeleting
        protected void OnRowDeleting(object sender, GridViewDeleteEventArgs e)
        {

            //fcol = "Delete";
            //int index = Convert.ToInt32(e.RowIndex);
            //string cCoVal = gvchuubunrilist.DataKeys[index].Values[0].ToString();
            ////mysqlcon.ConnectionString = constr;
            //string chubundelete = "Delete from m_syouhin_tyuugrp where cSYOUHIN_TYUUGRP = '" + cCoVal + "'";
            //shVal.loginId = Session["LoginId"].ToString();

            //Boolean fchubundelete = shVal.DaibunruiSaveList(chubundelete);
            //string sqldelete = " DELETE FROM m_syouhin_tyuugrp WHERE cSYOUHIN_TYUUGRP = '" + cCoVal + "'";
            //shVal.loginId = Session["LoginId"].ToString();
            //Boolean fdelete = shVal.DaibunruiSaveList(sqldelete);
            //this.BindChuubunrui();
        }
        
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            LinkButton lkb_delete = sender as LinkButton;
            GridViewRow gvRow = (GridViewRow)lkb_delete.NamingContainer;
            string cCoVal = gvRow.Cells[0].Text;
            
            //check shouhin
            string sql = "SELECT * FROM m_syouhin WHERE cSYOUHIN_TYUUGRP = '" + cCoVal + "'";
            String id = Request.QueryString["id"];
            DataTable dt_db = JC01Login_Class.Get_DB(id);
            String db = dt_db.Rows[0]["db"].ToString();
            String mail = dt_db.Rows[0]["mail"].ToString();
            ConstantVal.DB_NAME = db;
            shVal.loginId = mail;
            bool fexist = shVal.checkChuuInShouhin(sql);

            if (fexist == true)
            {
                Button cancel = gvchuubunrilist.Rows[gvRow.RowIndex].FindControl("BT_DeleteCancel") as Button;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowAnkenChangeMessage",
                         "MailKakuninMsgBox('その分類は使用中です。削除できません。','" + cancel.ClientID + "');", true);
            }
            else
            {
                Button ok = gvchuubunrilist.Rows[gvRow.RowIndex].FindControl("BT_DeleteOk") as Button;
                Button cancel = gvchuubunrilist.Rows[gvRow.RowIndex].FindControl("BT_DeleteCancel") as Button;
                updChuubunruiList.Update();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowAnkenChangeMessage",
                         "DeleteConfirmBox('削除してもよろしいでしょうか？','" + ok.ClientID + "','" + cancel.ClientID + "');", true);
            }
        }

        protected void BT_DeleteOk_Click(object sender, EventArgs e)
        {
            GridViewRow row = (sender as Button).NamingContainer as GridViewRow;
            string cCoVal = row.Cells[0].Text;

            string sqldelete = " DELETE FROM m_syouhin_tyuugrp WHERE cSYOUHIN_TYUUGRP = '" + cCoVal + "'";
            String id = Request.QueryString["id"];
            DataTable dt_db = JC01Login_Class.Get_DB(id);
            String db = dt_db.Rows[0]["db"].ToString();
            String mail = dt_db.Rows[0]["mail"].ToString();
            ConstantVal.DB_NAME = db;
            shVal.loginId = mail;
            Boolean fdelete = shVal.DaibunruiSaveList(sqldelete);
            updChuubunruiList.Update();
            this.BindChuubunrui();

            newDiv.Style.Add("visibility", "hidden");
            txt_newChuubunrui.Text = "";
            
            //20220329 テテ start
            string cchuu = (string)Session["cSYOUHIN_TYUUGRP"];
            string schuu = (string)Session["cSYOUHIN_TYUUGRP"];
            
            if (cchuu == cCoVal)
            {
                Session["cSYOUHIN_TYUUGRP"] = null;
                Session["cSYOUHIN_TYUUGRP"] = null;
            }
            //20220329 テテ end

        }


        #endregion

        #region gvchuubunrilist_RowCommand
        protected void gvchuubunrilist_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //int index = int.Parse(e.CommandArgument.ToString());
            if (e.CommandName == "Select")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = gvchuubunrilist.Rows[rowIndex];
                string cchuubun = row.Cells[0].Text;
                string schuubun = (row.FindControl("txt_editChuu") as TextBox).Text;

                string cdaiVal = "";
                string sql_cdai = "select cSYOUHIN_DAIGRP from m_syouhin_tyuugrp where cSYOUHIN_TYUUGRP='" + cchuubun + "'";
                String id = Request.QueryString["id"];
                DataTable dt_db = JC01Login_Class.Get_DB(id);
                String db = dt_db.Rows[0]["db"].ToString();
                String mail = dt_db.Rows[0]["mail"].ToString();
                ConstantVal.DB_NAME = db;
                shVal.loginId = mail;
                cdaiVal = shVal.cDaibunruiData(sql_cdai);

                string sdaiVal = "";
                string sql_sdai = "select sSYOUHIN_DAIGRP from m_syouhin_daigrp where cSYOUHIN_DAIGRP='" + cdaiVal + "'";
                shVal.loginId = mail;
                ConstantVal.DB_NAME = db;
                sdaiVal = shVal.sDaibunruiData(sql_sdai);

                Session["cSYOUHIN_DAIGRP"] = cdaiVal;
                Session["sSYOUHIN_DAIGRP"] = sdaiVal;

                Session["cSYOUHIN_TYUUGRP"] = cchuubun;
                Session["sSYOUHIN_TYUUGRP"] = schuubun;

                if (Session["fDai"] == null) {
                    Session["fDai"] = "false";
                }
                if (Session["fDai"].ToString().ToLower() == "false" && Session["fChuu"].ToString() == "true")
                {
                    Session["fDoubleChuu"] = "true";
                }

                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "parentButtonClick('btnChuuSelect','" + hdnHome.Value + "');", true);

                ifDaibunruiPopup.Src = "";
                mpeDaibunruiPopup.Hide();
                updDaibunruiPopup.Update();
                // ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Name: " + name + "\\nCountry: " + country + "');", true);
            }
        }
        #endregion

        #region onupdate
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            updChuubunruiList.Update();
            this.BindChuubunrui();
            GridViewRow row = (sender as Button).NamingContainer as GridViewRow;
            string cCoVal = (row.Cells[0].Controls[0] as TextBox).Text;
            string sChuubunruiVal = (row.FindControl("txt_editChuu") as TextBox).Text;

            //mysqlcon.ConnectionString = constr;
            if (sChuubunruiVal != "")
            {
                if (sChuubunruiVal.Contains("'") == true)
                {
                    sChuubunruiVal = sChuubunruiVal.Replace("'", @"\'");

                }
            }
            string sqlupdate = " Update m_syouhin_tyuugrp SET sSYOUHIN_TYUUGRP = '" + sChuubunruiVal + "' WHERE cSYOUHIN_TYUUGRP = '" + cCoVal + "'";
            String id = Request.QueryString["id"];
            DataTable dt_db = JC01Login_Class.Get_DB(id);
            String db = dt_db.Rows[0]["db"].ToString();
            String mail = dt_db.Rows[0]["mail"].ToString();
            ConstantVal.DB_NAME = db;
            shVal.loginId = mail;
            Boolean fupdate = shVal.DaibunruiSaveList(sqlupdate);
            
            gvchuubunrilist.EditIndex = -1;
            this.BindChuubunrui();
        }
        #endregion

        #region btnnewChuubunruiCancel_Click
        protected void btnnewChuubunruiCancel_Click(object sender, EventArgs e)
        {
            //newDiv.Style["display"] = "none";
            newDiv.Style.Add("visibility", "hidden");
            txt_newChuubunrui.Text = "";
        }
        #endregion

        #region btnnewChuubunruiSave_Click
        protected void btnnewChuubunruiSave_Click(object sender, EventArgs e)
        {
            bool chkdata = true;
            //状態名
            if (String.IsNullOrEmpty(txt_newChuubunrui.Text))
            {
                txt_newChuubunrui.Text = "";
                txt_newChuubunrui.Style.Add("border-color", "red !important");
                txt_newChuubunrui.Focus();
                chkdata = false;
            }
            if (chkdata == true)
            {
                bool fsave = SaveChuubunrui();
                //正常に保存する
                if (fsave == true)
                {
                    BindChuubunrui();//20211125 add by テテ
                    newDiv.Style.Add("visibility", "hidden");
                    txt_newChuubunrui.Text = "";//20220208 add by テテ
                }
                //エラーの場合
                else
                {
                    //ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "parentButtonClick('btnAnkenNyuuryokuSave');", true);
                }
            }
        }

        protected bool SaveChuubunrui()
        {
            bool fsave = false;
            try
            {
                int retval = 0;
                int njunban = 0;
                string cCoVal = FindChuubunruiCode();
                njunban = Convert.ToInt32(cCoVal);
                string chuubunruimei = "";
                string daibunrui = txt_daibunrui.Text;
                //状態名 sJYOTAI
                if (!String.IsNullOrEmpty(txt_newChuubunrui.Text.Trim()))
                {
                    if (txt_newChuubunrui.Text.Contains("'") == true)
                    {
                        chuubunruimei = txt_newChuubunrui.Text.Replace("'", @"\'");

                    }
                    else
                    {
                        chuubunruimei = txt_newChuubunrui.Text;
                    }
                    chuubunruimei = chuubunruimei.Trim();
                }
                else
                {
                    chuubunruimei = "@null";
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
                dt = shVal.DaibunruiList(datesql);
                if (dt.Rows.Count > 0)
                {
                    curDateTime = dt.Rows[0]["nowdateTime"].ToString();
                }
                //chenkousya
                string chenkoudata = "";
                DataTable dtchenkou = new DataTable();
                string chenkousql = "select cHENKOUSYA from m_j_tantousha where sMAIL='" + shVal.loginId + "' ";
                ConstantVal.DB_NAME =db;
                dtchenkou = shVal.DaibunruiList(chenkousql);
                if (dtchenkou.Rows.Count > 0)
                {
                    chenkoudata = dtchenkou.Rows[0]["cHENKOUSYA"].ToString();
                }

                string sql = "INSERT INTO m_syouhin_tyuugrp(";
                sql += " cSYOUHIN_TYUUGRP ";
                sql += ", sSYOUHIN_TYUUGRP ";
                sql += ", cSYOUHIN_DAIGRP ";
                sql += ", dHENKOU ";
                sql += ", cHENKOUSYA ";
                sql += ", nJUNBAN ";
                sql += ")VALUES  ";
                sql += " ( '" + cCoVal + "'";
                sql += " , '" + chuubunruimei + "'";
                sql += " , '" + daibunrui + "'";
                sql += " , '" + curDateTime + "'";
                sql += " , '" + chenkoudata + "'";
                sql += " , '" + njunban + "' ";
                sql += "  ) ";
                sql += " ON DUPLICATE KEY UPDATE cSYOUHIN_TYUUGRP = '" + cCoVal + "'";
                sql += " ,sSYOUHIN_TYUUGRP =  '" + chuubunruimei + "' ";
                sql += " ,cSYOUHIN_DAIGRP =  '" + daibunrui + "' ";
                sql += " ,dHENKOU =  '" + curDateTime + "' ";
                sql += " ,cHENKOUSYA =  '" + chenkoudata + "' ";
                sql += " ,nJUNBAN ='" + njunban + "' ";

                shVal.loginId = mail;
                ConstantVal.DB_NAME = db;
                fsave = shVal.DaibunruiSaveList(sql);

            }
            catch (Exception ex)
            {

            }

            return fsave;
        }

        protected string FindChuubunruiCode()
        {
            string ColVal = "";
            DataTable dt = new DataTable();
            string sqlStr = "SELECT cSYOUHIN_TYUUGRP,sSYOUHIN_TYUUGRP FROM m_syouhin_tyuugrp; ";
            String id = Request.QueryString["id"];
            DataTable dt_db = JC01Login_Class.Get_DB(id);
            String db = dt_db.Rows[0]["db"].ToString();
            String mail = dt_db.Rows[0]["mail"].ToString();
            ConstantVal.DB_NAME = db;
            shVal.loginId = mail;
            ConstantVal.DB_NAME = db;
            dt = shVal.DaibunruiList(sqlStr);

            //finding the missing number 
            List<int> ListShain = new List<int>();
            foreach (System.Data.DataRow dr in dt.Rows)
            {
                ListShain.Add(int.Parse(dr["cSYOUHIN_TYUUGRP"].ToString()));
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

        #region btnDaiSelect_Click
        protected void btnDaiSelect_Click(object sender, EventArgs e)
        {
            if (Session["cSYOUHIN_DAIGRP"] != null)
            {
                string cDAIGRP = (string)Session["cSYOUHIN_DAIGRP"];
                string sDAIGRP = (string)Session["sSYOUHIN_DAIGRP"];
                lblDaibunrui.Text = sDAIGRP;
                BindChuubunrui();
                updChuubunruiList.Update();
            }
            ifDaibunruiPopup.Src = "";
            mpeDaibunruiPopup.Hide();
            updDaibunruiPopup.Update();
        }
        #endregion

        #region btnDaiClose_Click
        protected void btnDaiClose_Click(object sender, EventArgs e)
        {
            ifDaibunruiPopup.Src = "";
            mpeDaibunruiPopup.Hide();
            updDaibunruiPopup.Update();
        }
        #endregion

        #region btnChuuSelect_Click
        protected void btnChuuSelect_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "parentButtonClick('btnChuuSelect','" + hdnHome.Value + "');", true);

        }
        #endregion
    }
}