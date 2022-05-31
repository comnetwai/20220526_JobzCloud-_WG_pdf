using Common;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Service;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace jobzcolud.WebFront
{ 
    public partial class JC30BukkenList : System.Web.UI.Page
    {
       
        public Double totalDataCount = 0;
        DataTable dt_bukken = new DataTable();
        DataTable dtpg = new DataTable();
        DataTable dt_subBukken = new DataTable();
        DataTable dt_img = new DataTable();
        string[] strExpand = new string[600];
        string linkBukkencol = "";
        int rowindex;
        string fcol = "";
        int kensuu = 0;
        int imgCol = 0;
        string label_joken = "";
        string code_joken = "";
        string sort_expression = "";
        string sort_direction = "";
        DataTable dt = new DataTable();
        JC07Home_Class JC07HomeClass = new JC07Home_Class();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(Request.QueryString["id"]))
            {


                if (!Page.IsPostBack)
                {
                   
                    mpeHyoujiSetPopUp.Hide();
                    ifpnlHyoujiSetPopUp.Style["width"] = "0px";
                    ifpnlHyoujiSetPopUp.Style["height"] = "0px";
                    JC99NavBar navbar_Master = (JC99NavBar)this.Master;
                    navbar_Master.LK_navbardrop.Style.Add(" background-color", "rgba(46,117,182)");

                    Session["strExpand"] = null;
                   // gvBukken.PageIndex = 0;
                    GridView1.PageIndex = 0;
                    rowindex = 0;
                    GV_Rowindex.Text = rowindex.ToString();
                   // gvBukken.PageSize = 20;
                    GridView1.PageSize = 20;
                    navbar_Master.navbar2.Visible = false;
                    ViewState["sortDirection"] = SortDirection.Descending;//20220223 added by lwin mar
                    ViewState["z_sortexpresion"] = "コード";//20220223 added by lwin mar 
                   // sort_expression= "コード";
                    sort_direction = "DESC";
                    ReadBukken();
                    BindingBukken();
                    Session["HyoujiSettingID"] = null;
                    Session["strExpand"] = null;
                  //  int endRowIndex = gvBukken.Rows.Count;
                    int endRowIndex = GridView1.Rows.Count;
                    lblHyojikensuu.Text = "1-" + endRowIndex + "/" + totalDataCount;
                    GridViewRowCount.Text = endRowIndex.ToString();
                    btntantousha.Attributes.Add("class", "JC30TantouKensakuBtnNull");

                }
                else
                {
                    if (HiddenClear.Text == "1")
                    {
                       // DDL_Hyojikensuu.SelectedIndex = 0;
                        Session["dtTantou_MultiSelect"] = null;
                        Session["dtSearch"] = null;
                        Session["Hyouji"] = null;
                        Session["btnsearch"] = null;
                        Session["s_bukken"] = null;
                        Session["s_tokusaki"] = null;
                        Session["s_tokusakitantou"] = null;
                        Session["s_tantou"] = null;
                        Session["s_date"] = null;
                        Session["strExpand"] = null;
                      //  DDL_Jyouken.Items.Clear();
                        DDL_Tantousya.Items.Clear();
                        lblMitumoriStart.Text = "";
                        btnMitumoriStartDate.Style["display"] = "block";
                        divMitumoriStartDate.Style["display"] = "none";
                        updMitumoriStartDate.Update();
                        lblMitumoriEnd.Text = "";
                        btnMitumoriEndDate.Style["display"] = "block";
                        divMitumoriEndDate.Style["display"] = "none";
                        UpdMitumoriEndDate.Update();
                        HiddenClear.Text = "";
                        updTantousha.Update();

                    }
                    if (ClearStartDate.Text == "1")
                    {
                        lblMitumoriStart.Text = "";
                        ClearStartDate.Text = "";
                        btnMitumoriStartDate.Style["display"] = "block";
                        divMitumoriStartDate.Style["display"] = "none";
                        updMitumoriStartDate.Update();
                    }
                    if (ClearEndDate.Text == "1")
                    {
                        lblMitumoriEnd.Text = "";
                        ClearEndDate.Text = "";
                        btnMitumoriEndDate.Style["display"] = "block";
                        divMitumoriEndDate.Style["display"] = "none";
                        UpdMitumoriEndDate.Update();
                    }
                    rowindex = Convert.ToInt32(GV_Rowindex.Text);
                   
                    
                    if (ViewState["sortDirection"] != null && ViewState["z_sortexpresion"] != null)
                    {
                        if(ViewState["sortDirection"].ToString() == "Ascending")
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
                        sort_expression = "コード";
                        sort_direction = "DESC";
                    }
                    BindingBukken();
                    JoukenReload_Tantouselect();
                }
                Session.Timeout = 480;
            }
            else
            {
                Response.Redirect("JC01Login.aspx");
            }

         }
       
      
       
        #region btnSearch_Click 物件データ検索
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            int kensuu = Convert.ToInt16(DDL_Hyojikensuu.SelectedValue);
            gvBukken.PageIndex = 0;
            GridView1.PageIndex = 0;//20220201 added by lwin mar
            rowindex = 0;
            GV_Rowindex.Text = rowindex.ToString();
            gvBukken.PageSize = kensuu;
            GridView1.PageSize = kensuu;//20220201 added by lwin mar
            Session["strExpand"] = null;
            ReadBukken();
           // FindExpandVal();
            BindingBukken();
            JoukenReload_Tantouselect();
            int endRowIndex = gvBukken.Rows.Count;
            lblHyojikensuu.Text = "1-" + endRowIndex + "/" + totalDataCount;
            GridViewRowCount.Text = endRowIndex.ToString();
            Updatelabel.Update();
            updpnlBukken.Update();
        }
        #endregion

        #region SearchNameReplace
        public string SearchNameReplace(String str)
        {
            string s = str;
            if (str.Contains("\\"))
            {
                s = s.Replace("\\", "\\\\\\\\");
            }
            if (str.Contains("%"))
            {
                s = s.Replace("%", "\\%");
            }
            if (str.Contains("'"))
            {
                s = s.Replace("'", "\\'");
            }
            if (str.Contains("’"))
            {
                s = s.Replace("’", "\\’");
            }
            return s;
        }
        #endregion

        #region 次回点検日を選択
        protected void btnMitumoriStartDate_Click(object sender, EventArgs e)
        {
            SessionUtility.SetSession("HOME", "Master");
            String id = Request.QueryString["id"];
            ifdatePopup.Src = "JCHidukeSelect.aspx?id=" + id;
            mpedatePopup.Show();

            ViewState["DATETIME"] = btnMitumoriStartDate.ID;

            if (!String.IsNullOrEmpty(lblMitumoriStart.Text))
            {
                DateTime dt = DateTime.Parse(lblMitumoriStart.Text);
                Session["CALENDARDATE"] = dt.ToString("yyyy/MM/dd");
                Session["YEAR"] = dt.Year;
            }

            // 設定したラベルをクリック時、時間設定ポップアップ画面を表示する
            lblMitumoriStart.Attributes.Add("onClick", "BtnClick('MainContent_btnMitumoriStartDate')");
            upddatePopup.Update();
        }
        protected void btnMitumoriEndDate_Click(object sender, EventArgs e)
        {
            SessionUtility.SetSession("HOME", "Master");
            String id = Request.QueryString["id"];
            ifdatePopup.Src = "JCHidukeSelect.aspx?id=" + id;
            mpedatePopup.Show();

            ViewState["DATETIME"] = btnMitumoriEndDate.ID;

            if (!String.IsNullOrEmpty(lblMitumoriEnd.Text))
            {
                DateTime dt = DateTime.Parse(lblMitumoriEnd.Text);
                Session["CALENDARDATE"] = dt.ToString("yyyy/MM/dd");
                Session["YEAR"] = dt.Year;
            }

            // 設定したラベルをクリック時、時間設定ポップアップ画面を表示する
            lblMitumoriEnd.Attributes.Add("onClick", "BtnClick('MainContent_btnMitumoriEndDate')");
            upddatePopup.Update();
        }

        #endregion
     
        #region "日付カレンダーポップアップの【X】ボタンクリック処理"
        protected void btnCalendarClose_Click(object sender, EventArgs e)
        {
            // 【日付サブ画面】を閉じる
            opensubgriddate.Text = "";
            subgridcall.Text = "";
            CloseSentakuSub();
            // フォーカスする
            CalendarFoucs();
            FindExpandVal();
            BindingBukken();
            updpnlBukken.Update();
        }
        #endregion
        #region "日付カレンダーポップアップの【設定】ボタンを押す処理"
        protected void btnCalendarSettei_Click(object sender, EventArgs e)
        {
           
            opensubgriddate.Text = "";
            subgridcall.Text = "";
            DateTime dtSelectedDate;

            // 【日付サブ画面】を閉じる
            CloseSentakuSub();
            string strBtnID = (string)ViewState["DATETIME"];
            string strCalendarDateTime = (string)Session["CALENDARDATETIME"];
            if (Session["CALENDARDATETIME"] != null)
            {
                strCalendarDateTime = (string)Session["CALENDARDATETIME"];
                dtSelectedDate = DateTime.Parse(strCalendarDateTime);
                if (strBtnID == btnMitumoriStartDate.ID)
                {
                    MitumoriDateDataBind(strCalendarDateTime, strBtnID);
                }
                else if (strBtnID == btnMitumoriEndDate.ID)
                {
                    MitumoriDateDataBind(strCalendarDateTime, strBtnID);
                }


            }
            CalendarFoucs();
            FindExpandVal();
            BindingBukken();
            updpnlBukken.Update();
            // ClientScript.RegisterStartupScript(this.GetType(), "RefreshParent", "<script language='javascript'>RefreshParent()</script>");
        }

        #endregion
        #region "日付サブ画面を閉じる時のフォーカス処理"
        protected void CalendarFoucs()
        {
            string strBtnID = (string)ViewState["DATETIME"];
            if (strBtnID == btnMitumoriStartDate.ID)
            {
                if (btnMitumoriStartDate.Style["display"] != "none")
                {
                    btnMitumoriStartDate.Focus();
                }

            }
            else if (strBtnID == btnMitumoriEndDate.ID)
            {
                if (btnMitumoriEndDate.Style["display"] != "none")
                {
                    btnMitumoriEndDate.Focus();
                }
            }

        }
        #endregion

        #region MitumoriDateDataBind
        protected void MitumoriDateDataBind(string strCalendarDateTime, string strImgbtnID)
        {
            DateTime dtDeliveryDate = DateTime.Parse(strCalendarDateTime);
            if (strImgbtnID == btnMitumoriStartDate.ID)
            {
                lblMitumoriStart.Text = dtDeliveryDate.ToString("yyyy/MM/dd");
                btnMitumoriStartDate.Style["display"] = "none";
                divMitumoriStartDate.Style["display"] = "block";
                updMitumoriStartDate.Update();
            }
            else if (strImgbtnID == btnMitumoriEndDate.ID)
            {
                lblMitumoriEnd.Text = dtDeliveryDate.ToString("yyyy/MM/dd");
                btnMitumoriEndDate.Style["display"] = "none";
                divMitumoriEndDate.Style["display"] = "block";
                UpdMitumoriEndDate.Update();
            }
           
        }
        #endregion

        #region "選択日付サブ画面を閉じる処理"
        protected void CloseSentakuSub()
        {
            ifdatePopup.Src = "";
            mpedatePopup.Hide();
            upddatePopup.Update();
        }
        #endregion
        protected void ReadBukken()
         {
            // 表示項目を設定から列取得
            DDL_Jyouken.Items.Clear();
            dt_bukken = new DataTable();
            if (!String.IsNullOrEmpty(Request.QueryString["id"]))
            {
                dt_bukken = new DataTable();

                String id = Request.QueryString["id"];
                DataTable dt = JC01Login_Class.Get_DB(id);
                String db = dt.Rows[0]["db"].ToString();
                String mail = dt.Rows[0]["mail"].ToString();
                ConstantVal.DB_NAME = db;

                JC07Home_Class JC07Home = new JC07Home_Class();
                JC07Home.loginId = mail;
                string s_bukken = "";
                string s_tokusaki = "";
                string s_tokusakitantou = "";
                string s_tantou = "";
                string s_date = "";
                if (!txtcBukken.Text.Equals(""))//見積コード
                {
                    s_bukken += "and  rb.cBUKKEN like '%" + txtcBukken.Text + "%'";
                    ListItem search = new ListItem();
                    search.Value = "物件コード";
                    search.Text = txtcBukken.Text;
                    DDL_Jyouken.Items.Add(search);
                }
                if (!txtTokuisaki.Text.Equals("")) //得意先
                {
                    String str_tokuisaki_name1 = Strings.StrConv(SearchNameReplace(txtTokuisaki.Text), VbStrConv.Wide);
                    String str_tokuisaki_name2 = Strings.StrConv(SearchNameReplace(txtTokuisaki.Text), VbStrConv.Narrow);


                    String str_tokuisaki_name3 = Strings.StrConv(SearchNameReplace(txtTokuisaki.Text), VbStrConv.Katakana);
                    str_tokuisaki_name3 = Strings.StrConv(str_tokuisaki_name3, VbStrConv.Narrow);
                    String str_tokuisaki_name4 = Strings.StrConv(str_tokuisaki_name1, VbStrConv.Hiragana);
                    str_tokuisaki_name4 = Strings.StrConv(str_tokuisaki_name4, VbStrConv.Narrow);

                    s_tokusaki += " and ( ";
                    s_tokusaki += " (sTOKUISAKI like '%" + str_tokuisaki_name1 + "%' OR sTOKUISAKI like '%" + str_tokuisaki_name2 + "%' OR sTOKUISAKI like '%" + str_tokuisaki_name3 + "%' OR sTOKUISAKI like '%" + str_tokuisaki_name4 + "%' OR sTOKUISAKI collate utf8_unicode_ci like '%" + SearchNameReplace(txtTokuisaki.Text) + "%')";
                    s_tokusaki += ") ";
                    ListItem search = new ListItem();
                    search.Value = "得意先";
                    search.Text = txtTokuisaki.Text;
                    DDL_Jyouken.Items.Add(search);
                }

                if (!txtTokuisakiTantou.Text.Equals("")) //得意先担当者
                {
                    String str_tokuisakitantou_name1 = Strings.StrConv(SearchNameReplace(txtTokuisakiTantou.Text), VbStrConv.Wide);
                    String str_tokuisakitantou_name2 = Strings.StrConv(SearchNameReplace(txtTokuisakiTantou.Text), VbStrConv.Narrow);


                    String str_tokuisakitantou_name3 = Strings.StrConv(SearchNameReplace(txtTokuisakiTantou.Text), VbStrConv.Katakana);
                    str_tokuisakitantou_name3 = Strings.StrConv(str_tokuisakitantou_name3, VbStrConv.Narrow);
                    String str_tokuisakitantou_name4 = Strings.StrConv(str_tokuisakitantou_name1, VbStrConv.Hiragana);
                    str_tokuisakitantou_name4 = Strings.StrConv(str_tokuisakitantou_name4, VbStrConv.Narrow);

                    s_tokusakitantou += " and ( ";
                    s_tokusakitantou += " (sTOKUISAKI_TAN like '%" + str_tokuisakitantou_name1 + "%' OR sTOKUISAKI_TAN like '%" + str_tokuisakitantou_name2 + "%' OR sTOKUISAKI_TAN like '%" + str_tokuisakitantou_name3 + "%' OR sTOKUISAKI_TAN like '%" + str_tokuisakitantou_name4 + "%' OR sTOKUISAKI_TAN collate utf8_unicode_ci like '%" + SearchNameReplace(txtTokuisakiTantou.Text) + "%')";
                    s_tokusakitantou += ") ";
                    ListItem search = new ListItem();
                    search.Value = "得意先担当者";
                    search.Text = txtTokuisakiTantou.Text;
                    DDL_Jyouken.Items.Add(search);
                }

                if (DDL_Tantousya.Items.Count > 0)  //自社担当者
                {
                    if (DDL_Tantousya.Items.Count == 1 && DDL_Tantousya.Items[0].Text == "選択なし")
                    {
                    }
                    else
                    {
                        s_tantou += " and ( ";
                        for (int i = 0; i < DDL_Tantousya.Items.Count; i++)
                        {
                            String cTantousha = DDL_Tantousya.Items[i].Value;
                            if (i == 0)
                            {
                                s_tantou += " (mt.cTANTOUSHA like '%" + cTantousha + "%'";
                            }
                            else
                            {
                                s_tantou += " OR mt.cTANTOUSHA like '%" + cTantousha + "%'";
                            }
                            ListItem search = new ListItem();
                            search.Value = "自社担当者" + cTantousha;
                            search.Text = DDL_Tantousya.Items[i].Text;
                            DDL_Jyouken.Items.Add(search);
                        }
                        s_tantou += ")) ";
                    }
                }
                if (!lblMitumoriStart.Text.Equals("") && !lblMitumoriEnd.Text.Equals("")) //  見積日
                {
                    s_date += " and date_format(dCREATE,'%Y/%m/%d') BETWEEN '" + lblMitumoriStart.Text + "' AND '" + lblMitumoriEnd.Text + "'";
                    //ListItem search = new ListItem();
                    //search.Value = "作成日";
                    //search.Text = txtBukkenStartDate.Text+"~"+txtBukkenEndDate.Text;
                    //DDL_Jyouken.Items.Add(search);

                    ListItem search1 = new ListItem();
                    search1.Value = "作成日start";
                    search1.Text = lblMitumoriStart.Text;
                    DDL_Jyouken.Items.Add(search1);

                    search1 = new ListItem();
                    search1.Value = "作成日end";
                    search1.Text = lblMitumoriEnd.Text;
                    DDL_Jyouken.Items.Add(search1);
                }
                else
                {
                    if (!lblMitumoriStart.Text.Equals("")) //見積日 start
                    {
                        s_date += " and date_format(dCREATE,'%Y/%m/%d')>='" + lblMitumoriStart.Text + "'";
                        ListItem search = new ListItem();
                        search.Value = "作成日start";
                        search.Text = lblMitumoriStart.Text;
                        DDL_Jyouken.Items.Add(search);
                    }
                    else if (!lblMitumoriEnd.Text.Equals(""))//見積日 end
                    {
                        s_date += " and date_format(dCREATE,'%Y/%m/%d')<='" + lblMitumoriEnd.Text + "'";
                        ListItem search = new ListItem();
                        search.Value = "作成日end";
                        search.Text = lblMitumoriEnd.Text;
                        DDL_Jyouken.Items.Add(search);
                    }
                    
                }
               
                DataSet ds = new DataSet();
                //ds = JC07Home.Bukken(kensuu, s_bukken, s_tokusaki, s_tokusakitantou, s_tantou, s_date);


                if (Session["btnsearch"] != null)
                {
                    Session["s_bukken"] = s_bukken;
                    Session["s_tokusaki"] = s_tokusaki;
                    Session["s_tokusakitantou"] = s_tokusakitantou;
                    Session["s_tantou"] = s_tantou;
                    Session["s_date"] = s_date;
                }
                if (Session["Hyouji"] != null)
                {
                    if (Session["s_bukken"] != null)
                    {
                        s_bukken = (string)Session["s_bukken"];
                    }
                    if (Session["s_tokusaki"] != null)
                    {
                        s_tokusaki = (string)Session["s_tokusaki"];
                    }
                    if (Session["s_tokusakitantou"] != null)
                    {
                        s_tokusakitantou = (string)Session["s_tokusakitantou"];
                    }
                    if (Session["s_tantou"] != null)
                    {
                        s_tantou = (string)Session["s_tantou"];
                    }
                    if (Session["s_date"] != null)
                    {
                        s_date = (string)Session["s_date"];
                    }
                    //dt_bukken = JC07Home.Bukken(s_bukken, s_tokusaki, s_tokusakitantou, s_tantou, s_date, rowindex, Convert.ToInt16(DDL_Hyojikensuu.SelectedValue));
                    ConstantVal.DB_NAME = db;
                    dt_bukken = JC07HomeClass.Bukken(s_bukken, s_tokusaki, s_tokusakitantou, s_tantou, s_date, rowindex, Convert.ToInt16(DDL_Hyojikensuu.SelectedValue), sort_expression, sort_direction);//20220223 added by lwin mar update for sorting

                    dtpg = JC07HomeClass.BukkenPg(s_bukken, s_tokusaki, s_tokusakitantou, s_tantou, s_date);
                }
                else
                {
                    try
                    {
                        //dt_bukken = JC07HomeClass.Bukken(s_bukken, s_tokusaki, s_tokusakitantou, s_tantou, s_date,rowindex, Convert.ToInt16(DDL_Hyojikensuu.SelectedValue));
                        ConstantVal.DB_NAME = db;
                        dt_bukken = JC07HomeClass.Bukken(s_bukken, s_tokusaki, s_tokusakitantou, s_tantou, s_date, rowindex, Convert.ToInt16(DDL_Hyojikensuu.SelectedValue), sort_expression, sort_direction);//20220223 added by lwin mar update for sorting
                        dtpg = JC07HomeClass.BukkenPg(s_bukken, s_tokusaki, s_tokusakitantou, s_tantou, s_date);

                    }
                    catch(Exception ex)
                    {

                    }
                   
                }
                ViewState["dt_bukken"] = dt_bukken;
                ViewState["dtpg"] = dtpg;
                Session["dd_joken"] = DDL_Jyouken;
            }
            else
            {
                Response.Redirect("JC01Login.aspx");
            }
        }
        protected void ReadJoken()
        {
            // 表示項目を設定から列取得
            DDL_Jyouken.Items.Clear();
            DropDownList dd_joken = new DropDownList();
            dd_joken = Session["dd_joken"] as DropDownList;
            dt_bukken = new DataTable();
            if (!String.IsNullOrEmpty(Request.QueryString["id"]))
            {
                dt_bukken = new DataTable();

                String id = Request.QueryString["id"];
                DataTable dt = JC01Login_Class.Get_DB(id);
                String db = dt.Rows[0]["db"].ToString();
                String mail = dt.Rows[0]["mail"].ToString();
                ConstantVal.DB_NAME = db;

                JC07Home_Class JC07Home = new JC07Home_Class();
                JC07Home.loginId = mail;
                string s_bukken = "";
                string s_tokusaki = "";
                string s_tokusakitantou = "";
                string s_tantou = "";
                string s_date = "";
                string s_date1 = "";
                string s_date2 = "";
                s_tantou += " and ( ";
                int date = 0;
                int tantou = 0;
                if (dd_joken.Items.Count > 0)
                {
                    for (int i = 0; i < dd_joken.Items.Count; i++)
                    {

                        if (dd_joken.Items[i].Value == "物件コード")
                        {
                            if (!label_joken.Equals("物件コード"))
                            {
                                s_bukken += "and  rb.cBUKKEN like '%" + dd_joken.Items[i].Text + "%'";
                                ListItem search = new ListItem();
                                search.Value = "物件コード";
                                search.Text = dd_joken.Items[i].Text;
                                DDL_Jyouken.Items.Add(search);
                            }
                        }

                        if (dd_joken.Items[i].Value == "得意先")
                        {
                            if (!label_joken.Equals("得意先"))
                            {
                                String str_tokuisaki_name1 = Strings.StrConv(SearchNameReplace(dd_joken.Items[i].Text), VbStrConv.Wide);
                                String str_tokuisaki_name2 = Strings.StrConv(SearchNameReplace(dd_joken.Items[i].Text), VbStrConv.Narrow);


                                String str_tokuisaki_name3 = Strings.StrConv(SearchNameReplace(dd_joken.Items[i].Text), VbStrConv.Katakana);
                                str_tokuisaki_name3 = Strings.StrConv(str_tokuisaki_name3, VbStrConv.Narrow);
                                String str_tokuisaki_name4 = Strings.StrConv(str_tokuisaki_name1, VbStrConv.Hiragana);
                                str_tokuisaki_name4 = Strings.StrConv(str_tokuisaki_name4, VbStrConv.Narrow);

                                s_tokusaki += " and ( ";
                                s_tokusaki += " (sTOKUISAKI like '%" + str_tokuisaki_name1 + "%' OR sTOKUISAKI like '%" + str_tokuisaki_name2 + "%' OR sTOKUISAKI like '%" + str_tokuisaki_name3 + "%' OR sTOKUISAKI like '%" + str_tokuisaki_name4 + "%' OR sTOKUISAKI collate utf8_unicode_ci like '%" + SearchNameReplace(txtTokuisaki.Text) + "%')";
                                s_tokusaki += ") ";
                                ListItem search = new ListItem();
                                search.Value = "得意先";
                                search.Text = dd_joken.Items[i].Text;
                                DDL_Jyouken.Items.Add(search);
                            }
                        }
                        if (dd_joken.Items[i].Value == "得意先担当者")
                        {
                            if (!label_joken.Equals("得意先担当者"))
                            {
                                String str_tokuisakitantou_name1 = Strings.StrConv(SearchNameReplace(dd_joken.Items[i].Text), VbStrConv.Wide);
                                String str_tokuisakitantou_name2 = Strings.StrConv(SearchNameReplace(dd_joken.Items[i].Text), VbStrConv.Narrow);


                                String str_tokuisakitantou_name3 = Strings.StrConv(SearchNameReplace(dd_joken.Items[i].Text), VbStrConv.Katakana);
                                str_tokuisakitantou_name3 = Strings.StrConv(str_tokuisakitantou_name3, VbStrConv.Narrow);
                                String str_tokuisakitantou_name4 = Strings.StrConv(str_tokuisakitantou_name1, VbStrConv.Hiragana);
                                str_tokuisakitantou_name4 = Strings.StrConv(str_tokuisakitantou_name4, VbStrConv.Narrow);

                                s_tokusakitantou += " and ( ";
                                s_tokusakitantou += " (sTOKUISAKI_TAN like '%" + str_tokuisakitantou_name1 + "%' OR sTOKUISAKI_TAN like '%" + str_tokuisakitantou_name2 + "%' OR sTOKUISAKI_TAN like '%" + str_tokuisakitantou_name3 + "%' OR sTOKUISAKI_TAN like '%" + str_tokuisakitantou_name4 + "%' OR sTOKUISAKI_TAN collate utf8_unicode_ci like '%" + SearchNameReplace(txtTokuisakiTantou.Text) + "%')";
                                s_tokusakitantou += ") ";
                                ListItem search = new ListItem();
                                search.Value = "得意先担当者";
                                search.Text = dd_joken.Items[i].Text;
                                DDL_Jyouken.Items.Add(search);
                            }
                        }

                        if (dd_joken.Items[i].Value.Contains("自社担当者"))
                        {
                                String cTantousha = dd_joken.Items[i].Value;
                            cTantousha = cTantousha.Replace("自社担当者", "");
                            if (code_joken != cTantousha)
                            {
                                if (tantou == 0)
                                {
                                    s_tantou += " (mt.cTANTOUSHA like '%" + cTantousha + "%'";
                                }
                                else
                                {
                                    s_tantou += " OR mt.cTANTOUSHA like '%" + cTantousha + "%'";
                                }
                                tantou++;
                                ListItem search = new ListItem();
                                search.Value = "自社担当者" + cTantousha;
                                search.Text = dd_joken.Items[i].Text;
                                DDL_Jyouken.Items.Add(search);
                            }
                            else
                            {
                                DDL_Tantousya.Items.Remove("自社担当者" + cTantousha);
                            }
                        }
                            if (dd_joken.Items[i].Value == "作成日start")
                            {
                                s_date1 = dd_joken.Items[i].Text;

                            }
                            if (dd_joken.Items[i].Value == "作成日end")
                            {
                                date++;
                                s_date2 = dd_joken.Items[i].Text;

                            }
                        
                    }
                }
                if (!s_date1.Equals("") && !s_date2.Equals(""))
                {
                    if (label_joken.Equals("作成日end"))
                    {
                        s_date += " and date_format(dCREATE,'%Y/%m/%d')>='" + s_date1 + "'";
                        ListItem search = new ListItem();
                        search.Value = "作成日start";
                        search.Text = s_date1;
                        DDL_Jyouken.Items.Add(search);
                    }
                    else
                    {
                        s_date += " and date_format(dCREATE,'%Y/%m/%d') BETWEEN '" + s_date1 + "' AND '" + s_date2 + "'";
                        
                        ListItem search = new ListItem();
                        search.Value = "作成日start";
                        search.Text = s_date1;
                        DDL_Jyouken.Items.Add(search);

                        search = new ListItem();
                        search.Value = "作成日end";
                        search.Text = s_date2;
                        DDL_Jyouken.Items.Add(search);

                    }
                }
                else
                {
                    if (!s_date1.Equals("")) //
                    {
                        if (!label_joken.Contains("作成日start"))
                        {
                            s_date += " and date_format(dCREATE,'%Y/%m/%d')>='" + s_date1 + "'";
                            ListItem search = new ListItem();
                            search.Value = "作成日start";
                            search.Text = s_date1;
                            DDL_Jyouken.Items.Add(search);
                        }

                    }
                    else if (!s_date2.Equals(""))//
                    {
                        if (!label_joken.Contains("作成日end"))
                        {
                            s_date += " and date_format(dCREATE,'%Y/%m/%d')<='" + s_date2 + "'";
                            ListItem search = new ListItem();
                            search.Value = "作成日end";
                            search.Text = s_date2;
                            DDL_Jyouken.Items.Add(search);
                        }
                    }
                }
                if(tantou==0)
                {
                    s_tantou = "";
                }
                if (tantou > 0)
                {
                    s_tantou += ")) ";
                }
                DataSet ds = new DataSet();
                //ds = JC07Home.Bukken(kensuu, s_bukken, s_tokusaki, s_tokusakitantou, s_tantou, s_date);



                // dt_bukken = JC07HomeClass.Bukken(s_bukken, s_tokusaki, s_tokusakitantou, s_tantou, s_date,rowindex, Convert.ToInt16(DDL_Hyojikensuu.SelectedValue));//20220201 added by lwin mar
                ConstantVal.DB_NAME = db;
                dt_bukken = JC07HomeClass.Bukken(s_bukken, s_tokusaki, s_tokusakitantou, s_tantou, s_date,rowindex, Convert.ToInt16(DDL_Hyojikensuu.SelectedValue),sort_expression,sort_direction);//20220223 added by lwin mar update for sorting
                dtpg = JC07HomeClass.BukkenPg(s_bukken, s_tokusaki, s_tokusakitantou, s_tantou, s_date);//20220201 added by lwin mar
                ViewState["dt_bukken"] = dt_bukken;
                ViewState["dtpg"] = dtpg;
                Session["dd_joken"] = DDL_Jyouken;
            }
            else
            {
                Response.Redirect("JC01Login.aspx");
            }
        }
        protected void BindingBukken()
        {
            if (ViewState["dt_bukken"] != null)
            {
                dt_bukken = ViewState["dt_bukken"] as DataTable;
                dtpg = ViewState["dtpg"] as DataTable;
                var columns = gvBukkenOriginal.Columns.CloneFields();
                gvBukken.Columns.Clear();
                gvBukken.Columns.Add(columns[0]);
                gvBukken.Columns.Add(columns[1]);
                int gazo_flag = 0;//20211230 added
                DataColumnCollection dtcolumns = dt_bukken.Columns;
                foreach (DataColumn dr in dt_bukken.Columns)
                {
                    string HeaderName = dr.ColumnName.ToString();
                    int col = dtcolumns.IndexOf(HeaderName);
                    if (col > 1)
                    {

                        if (HeaderName == "物件コード") //20220504 Added by phoo
                        {
                            gvBukken.Columns.Add(columns[2]);
                            gvBukken.Columns[col].HeaderText = HeaderName;

                        }
                        else if (HeaderName == "物件名")
                        {
                            gvBukken.Columns.Add(columns[3]);
                            gvBukken.Columns[col].HeaderText = HeaderName;
                        }
                        else if (HeaderName == "備考")
                        {
                            gvBukken.Columns.Add(columns[4]);
                            gvBukken.Columns[col].HeaderText = HeaderName;

                        }
                        else if (HeaderName == "見積数") //20220504 Added by phoo
                        {
                            gvBukken.Columns.Add(columns[5]);
                            gvBukken.Columns[col].HeaderText = HeaderName;  //20220504 Added by phoo
                        }
                        else if (HeaderName == "得意先")
                        {
                            gvBukken.Columns.Add(columns[6]);
                            gvBukken.Columns[col].HeaderText = HeaderName;  //20220504 Added by phoo
                        }
                        else if (HeaderName == "得意先担当者")
                        {
                            gvBukken.Columns.Add(columns[7]);
                            gvBukken.Columns[col].HeaderText = HeaderName;  //20220504 Added by phoo
                        }
                        else if (HeaderName == "物件作成日")
                        {
                            gvBukken.Columns.Add(columns[8]);
                            gvBukken.Columns[col].HeaderText = HeaderName;
                        }
                        else if (HeaderName == "自社担当者")
                        {
                            gvBukken.Columns.Add(columns[9]);
                            gvBukken.Columns[col].HeaderText = HeaderName;  //20220504 Added by phoo
                        }
                        else if (HeaderName == "画像")
                        {
                            gazo_flag = 1;
                            gvBukken.Columns.Add(columns[10]);
                            gvBukken.Columns[col].HeaderText = HeaderName;
                        }

                    }
                    col++;
                }
                int kensuu = Convert.ToInt16(DDL_Hyojikensuu.SelectedValue);
                if (gazo_flag == 1)
                {
                    foreach (DataRow dr in dt_bukken.Rows)
                    {
                        if (dr["sFile"].ToString() != "")
                        {
                            string file = dr["sFile"].ToString();
                            string path = dr["sPATH_SERVER_SOURCE"].ToString();
                            //string urlstring = SetPhotoRoot(path, file);
                            if (file != "")
                            {

                                dr["file64string"] = "../Img/gazou.png";// ;urlstring

                            }
                            else
                            {
                                dr["file64string"] = "../Img/imgerr.png";
                            }

                        }
                        else
                        {
                            dr["file64string"] = "../Img/imgerr.png";
                        }
                    }
                    //for (int i = rowindex; i < dt_bukken.Rows.Count; i++)
                    //{
                    //    if (i < rowindex + kensuu)
                    //    {
                    //        if (dt_bukken.Rows[i]["sFile"].ToString() != "")
                    //        {
                    //            string file = dt_bukken.Rows[i]["sFile"].ToString();
                    //            string path = dt_bukken.Rows[i]["sPATH_SERVER_SOURCE"].ToString();
                    //            //string urlstring = SetPhotoRoot(path, file);
                    //            // if (urlstring != "")
                    //            if (path != "")
                    //            {

                    //                //dt_bukken.Rows[i]["file64string"] = urlstring;
                    //                dt_bukken.Rows[i]["file64string"] = "../Img/gazou.png";
                    //            }
                    //            else
                    //            {
                    //                dt_bukken.Rows[i]["file64string"] = "../Img/imgerr.png";
                    //            }

                    //        }
                    //        else
                    //        {

                    //            dt_bukken.Rows[i]["file64string"] = "../Img/imgerr.png";
                    //        }
                    //    }
                    //    else
                    //    {
                    //        break;
                    //    }
                    //}
                }


               // totalDataCount = dt_bukken.Rows.Count;
                totalDataCount = dtpg.Rows.Count;
                try
                {
                    int endRowIndex = gvBukken.Rows.Count;
                    
                    gvBukken.DataSource = dt_bukken;
                    gvBukken.DataBind();
                   
                    GridView1.DataSource = dtpg;//20220201 added  by lwin mar
                    GridView1.DataBind();//20220201 added  by lwin mar
                   
                }
                catch(Exception ex)
                {

                }
              
            }
        }
        protected void gvPgBukken_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Visible = false;
            }

        }
        protected void gvBukken_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    // e.Row.Visible = false;
                    
                    string bukkenId = (e.Row.DataItem as DataRowView).Row["cBUKKEN"].ToString();
                    //サブ グリッドを表示かとかを決めるこうもく
                    if ((e.Row.DataItem as DataRowView).Row["cCountMitsu"].ToString() == "")
                    {
                        // e.Row.Cells[0].Width = new Unit("20");
                        //e.Row.Cells[0].Attributes.Add("style", "display:none");                      
                        Image imgctrl = e.Row.Cells[0].Controls[0].FindControl("imgArrow") as Image;
                        imgctrl.Attributes.Add("style", "display:none");
                    }


                    //expand subgrid after postback
                    if (Session["strExpand"] != null)
                    {
                        string[] expandlist = Session["strExpand"] as string[];
                        string expandVal = expandlist[e.Row.RowIndex].ToString();
                        TextBox txtbx = (e.Row.Cells[1].Controls[0].FindControl("IsExpanded") as TextBox);
                        txtbx.Text = expandVal;
                    }
                    //サブ グリッド
                    GridView gv = (GridView)e.Row.FindControl("gvSubBukken");
                    //見積データの設定
                    MitsumoriData(bukkenId, gv);


                }
            }
            catch(Exception ex)
            {

            }
        }


        #region GV_Uriage_Sorting
        protected void GV_Bukken_Sorting(object sender, GridViewSortEventArgs e)
        {
            rowindex = Convert.ToInt32(GV_Rowindex.Text);
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
            ReadJoken();//20220201 added by lwin mar
            Session["strExpand"] = null;
            BindingBukken();
            //GridView1.PageIndex = e.NewPageIndex;
            //int endRowIndex = (kensuu * e.NewPageIndex) + gvBukken.Rows.Count;
            //lblHyojikensuu.Text = startRowIndex + "-" + endRowIndex + "/" + totalDataCount;
            //GridViewRowCount.Text = endRowIndex.ToString();
           // Updatelabel.Update();
            updpnlBukken.Update();
        }
        #endregion
        #region SortExpression
        public string SortExpression
        {
            get
            {
                if (ViewState["z_sortexpresion"] == null)
                    ViewState["z_sortexpresion"] = this.gvBukken.DataKeyNames[0].ToString();
                return ViewState["z_sortexpresion"].ToString();
            }
            set
            {
                ViewState["z_sortexpresion"] = value;
            }
        }
        #endregion

        #region GV_Bukken_RowCreated
        protected void GV_Bukken_RowCreated(object sender, GridViewRowEventArgs e)
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

        #region SetPhotoRoot
        private string SetPhotoRoot(string root, string nam)
        {
            string imgurl = "";
            try
            {
               
                string filePath = "";
                string filePath_f = "";
                string name = nam;
                // name = System.IO.Path.GetFileNameWithoutExtension(name);
                string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

                filePath = folderPath + "\\DEMO20" + "\\ICONTEMP\\" + "BukkenPrint";
                int i = root.IndexOf("\\営業用");
                if (i == -1)
                {
                    i = root.IndexOf("\\指示書");
                    filePath_f = root.Substring(0, i) + "BukkenPrint";
                }
                else
                {
                    int j = root.IndexOf("\\指示書");
                    if (j != -1 && j < i)
                    {
                        filePath_f = root.Substring(0, j) + "BukkenPrint";
                    }
                    else
                    {
                        filePath_f = root.Substring(0, i) + "BukkenPrint";
                    }
                }

                string filePath_temp = filePath_f + "\\Temp.jpg";
                filePath_f += "\\" + name;

                if (Directory.Exists(filePath) == false)
                {
                    Directory.CreateDirectory(filePath);
                }
                filePath += "\\" + name;
                if (File.Exists(filePath) == false)
                {
                    try
                    {
                        File.Copy(filePath_f, filePath);
                    }
                    catch { }
                }
                else
                {
                    try
                    {
                        File.Delete(filePath);
                        File.Copy(filePath_f, filePath);
                    }
                    catch { }
                }
                if (filePath == null)
                {
                    //if (img.ImageUrl != null) img.ImageUrl = "";
                    //img.ImageUrl = System.Environment.CurrentDirectory + @"\res\Error.jpg";
                    imgurl = "";
                }
                else
                {

                    try
                    {
                        byte[] byteData = System.IO.File.ReadAllBytes(filePath);

                        //now convert byte[] to Base64
                        string base64String = Convert.ToBase64String(byteData);
                        imgurl = string.Format("data:image/png;base64,{0}", base64String);

                    }
                    catch { }
                }

                
            }
            catch(Exception ex)
            {

            }
            return imgurl;
        }

        #endregion
        protected void MitsumoriData(string bukkenId, GridView gv)
        {
            if (!String.IsNullOrEmpty(Request.QueryString["id"]))
            {
                dt_subBukken = new DataTable();

                String id = Request.QueryString["id"];
                DataTable dt = JC01Login_Class.Get_DB(id);
                String db = dt.Rows[0]["db"].ToString();
                String mail = dt.Rows[0]["mail"].ToString();
                ConstantVal.DB_NAME = db;

                JC07HomeClass.loginId = mail;
                JC07HomeClass.bukkenId = bukkenId;
                dt_subBukken = JC07HomeClass.SubBukkenData();
                //物件の見積データ
                if (dt_subBukken.Rows.Count > 0)
                {
                    int col = 0;
                    foreach (DataColumn dr in dt_subBukken.Columns)
                    {
                        string HeaderName = dr.ColumnName.ToString();
                        if (col > 0)
                        {
                            //グリッドに項目列を追加する

                            /*TemplateField tfield = new TemplateField();
                            tfield.HeaderText = HeaderName;
                            tfield.HeaderStyle.Font.Bold = false;
                            gv.Columns.Add(tfield);*/

                            if (HeaderName == "見積コード")
                            {
                                gv.Columns[col].ItemStyle.Width = 150;
                            }
                            else if (HeaderName == "見積名")
                            {
                                gv.Columns[col].ItemStyle.Width = 200;
                            }
                            else if (HeaderName == "見積日")
                            {
                                gv.Columns[col].ItemStyle.Width = 110;
                            }
                            else if (HeaderName == "自社担当者")
                            {
                                gv.Columns[col].ItemStyle.Width = 110;
                            }
                            else if (HeaderName == "見積状態")
                            {
                                gv.Columns[col].ItemStyle.Width = 115;
                            }
                            else if (HeaderName == "合計金額")
                            {
                                gv.Columns[col].ItemStyle.Width = 125;
                            }
                            else if (HeaderName == "金額粗利")
                            {
                                gv.Columns[col].ItemStyle.Width = 125;
                            }
                            gv.Columns[col].HeaderText = HeaderName;
                        }
                        col++;

                    }
                    gv.DataSource = dt_subBukken;
                    gv.DataBind();
                }
            }
            else
            {
                Response.Redirect("JC01Login.aspx");
            }
        }

       
        protected void btnMitsuHyouji_Click(object sender, EventArgs e)
        {
            //string mistumoriId = HiddenMitsumoriId.Text;
            var mistumoriId = (LinkButton)sender;
            Session["cMitumori"] = mistumoriId.Text;
            Session["strExpand"] = null;
            if (Session["cMitumori"] != null)
            {
                if (!String.IsNullOrEmpty(Request.QueryString["id"]))
                {
                    String id = Request.QueryString["id"];
                    Response.Redirect("JC10MitsumoriTouroku.aspx?id=" + id);
                }
                else
                {
                    Response.Redirect("JC01Login.aspx");
                }
            }
            else
            {
                Response.Redirect("JC01Login.aspx");
            }
        }

      
        protected void FindExpandVal()//GridViewRow gvParentBukken
        {
            try
            {
                strExpand = (from p in Request.Form["IsExpanded"].Split(',') select p).ToArray();
                Session["strExpand"] = strExpand;
            }
            catch
            {

            }
        }

        protected void MitsumoriCopy(string bukkenId, string mitsumoriId)
        {
            if (!String.IsNullOrEmpty(Request.QueryString["id"]))
            {
                String id = Request.QueryString["id"];
                DataTable dt = JC01Login_Class.Get_DB(id);
                String db = dt.Rows[0]["db"].ToString();
                String mail = dt.Rows[0]["mail"].ToString();
                ConstantVal.DB_NAME = db;

                JC_ClientConnecction_Class jc = new JC_ClientConnecction_Class();
                jc.loginId = mail;
                jc.GetConnection();
                DataTable dt_loginuser = jc.GetLoginUserCodeFromClientDB();
                String loginId = dt_loginuser.Rows[0]["code"].ToString();


                JC07HomeClass.bukkenId = bukkenId;
                JC07HomeClass.mitsumoriId = mitsumoriId;
                JC07HomeClass.MitsumoriCopy(loginId);
            }
            else
            {
                Response.Redirect("JC01Login.aspx");
            }

        }

        protected void MitsumoriDelete(string BukkenId, string mitsumoriId)
        {
            if (!String.IsNullOrEmpty(Request.QueryString["id"]))
            {
                String id = Request.QueryString["id"];
                DataTable dt = JC01Login_Class.Get_DB(id);
                String db = dt.Rows[0]["db"].ToString();
                String mail = dt.Rows[0]["mail"].ToString();
                ConstantVal.DB_NAME = db;

                JC07HomeClass.bukkenId = BukkenId;
                JC07HomeClass.mitsumoriId = mitsumoriId;
                JC07HomeClass.MitsumoriDelete();
            }
            else
            {
                Response.Redirect("JC01Login.aspx");
            }

        }

        #region gvrowcommand
        protected void GV_Mitsumori_Original_RowCommand(object sender, GridViewCommandEventArgs e)
        {
           
            if (e.CommandName == "Copy")
            {
                GridViewRow row = (GridViewRow)(((Control)e.CommandSource).NamingContainer);
                int index = row.RowIndex;
                try
                {
                    string BukkenId = gvBukken.DataKeys[index].Value.ToString();
                    Session["cBukken"] = BukkenId;
                    Session["cMitumori"] = null;
                    Session["strExpand"] = null;
                    Session["fBukkenName"] = "true"; //20220209 MiMi added
                    if (!String.IsNullOrEmpty(Request.QueryString["id"]))
                    {
                        String id = Request.QueryString["id"];
                        Response.Redirect("JC10MitsumoriTouroku.aspx?id=" + id);
                    }
                    else
                    {
                        Response.Redirect("JC01Login.aspx");
                    }
                }
                catch (Exception ex)
                {

                }
            }
        }
        #endregion

        //subbukengridview　の見積を追加
        protected void btnAddMitsumori_Click(object sender, EventArgs e)
        {

            //物件グリッドデータ
           

            GridViewRow gvrow = (sender as Button).NamingContainer as GridViewRow;
            GridView gridview = gvrow.NamingContainer as GridView;
            Button ButtonCopy = sender as Button;
            int rowIndex = int.Parse(ButtonCopy.CommandArgument.ToString());
          
            try
            {
                string BukkenId = gridview.DataKeys[rowIndex].Value.ToString();
                Session["cBukken"] = BukkenId;
                Session["cMitumori"] = null;
                Session["strExpand"] = null;
                if (!String.IsNullOrEmpty(Request.QueryString["id"]))
                {
                    String id = Request.QueryString["id"];
                    Response.Redirect("JC10MitsumoriTouroku.aspx?id=" + id);
                }
                else
                {
                    Response.Redirect("JC01Login.aspx");
                }
            }
            catch(Exception ex)
            {

            }


           

        }
        
        //subbukengridview　の他見積をコピーして追加
        protected void btnTaMitsumori_Click(object sender, EventArgs e)
        {
            Opensubgridmitsu.Text = "1";
            SessionUtility.SetSession("HOME", "Master");
            // FindExpandVal();
            String id = Request.QueryString["id"];
            ifShinkiPopup.Src = "JC12MitsumoriKensaku.aspx?id=" + id;
            mpeShinkiPopup.Show();
            updShinkiPopup.Update();
            
            // BindingBukken();
            // updpnlBukken.Update();
            ScriptManager.RegisterStartupScript(this, GetType(), "CloseModal", "closeLoadingModal();", true);
        }
        #region btn_CloseMitumoriSearch_Click
        protected void btn_CloseMitumoriSearch_Click(object sender, EventArgs e)
        {
            Opensubgridmitsu.Text = "";
            ifShinkiPopup.Src = "";
            mpeShinkiPopup.Hide();
            updShinkiPopup.Update();
            FindExpandVal();
            BindingBukken();
            updpnlBukken.Update();
            if (Session["cMitumori"] != null)
            {
                if (!String.IsNullOrEmpty(Request.QueryString["id"]))
                {
                    String id = Request.QueryString["id"];
                    Response.Redirect("JC10MitsumoriTouroku.aspx?id=" + id);
                }
                else
                {
                    Response.Redirect("JC01Login.aspx");
                }
            }
        }
        #endregion

        #region btnkensaku_Click()
        protected void btnkensaku_Click(object sender, EventArgs e)
        {
            
                DataTable dtSelect = new DataTable();
            dtSelect.Columns.Add("code");
            dtSelect.Columns.Add("name");
            if (DDL_Tantousya.Items.Count > 0)
            {
                for (int i = 0; i < DDL_Tantousya.Items.Count; i++)
                {
                    DataRow dr = dtSelect.NewRow();
                    dr[0] = DDL_Tantousya.Items[i].Value;
                    dr[1] = DDL_Tantousya.Items[i].Text;
                    dtSelect.Rows.Add(dr);
                }
            }
            else
            {
                DataRow dr = dtSelect.NewRow();
                dr[0] = "";
                dr[1] = "選択なし";
                dtSelect.Rows.Add(dr);
            }
            SessionUtility.SetSession("HOME", "Master");
            Session["isKensaku"] = "true";
            Session["dtTantou_MultiSelect"] = dtSelect;
            ifSentakuPopup.Style["width"] = "100vw";
            ifSentakuPopup.Style["height"] = "100vh";
            String id = Request.QueryString["id"];
            ifSentakuPopup.Src = "JC14TantouKensaku.aspx?id=" + id;
            mpeSentakuPopup.Show();
           // OpenSubgridTantou.Text = "";
            //lblsJISHATANTOUSHA.Attributes.Add("onClick", "BtnClick('MainContent_BT_JisyaTantousya_Add')");
            updSentakuPopup.Update();
        }
        #endregion

        #region 担当者サブ画面を閉じる時のフォーカス処理
        protected void btnJishaTantouSelect_Click(object sender, EventArgs e)
        {
            OpenSubgridTantou.Text = "";
            subgridcall.Text = "";
            Session["TantouOpeb"] = null;
            ifSentakuPopup.Src = "";
            mpeSentakuPopup.Hide();
            updSentakuPopup.Update();
            if (Session["dtTantou_MultiSelect"] != null)
            {
                DataTable dt = Session["dtTantou_MultiSelect"] as DataTable;
                DDL_Tantousya.DataSource = dt;
                DDL_Tantousya.DataTextField = "name";
                DDL_Tantousya.DataValueField = "code";
                DDL_Tantousya.DataBind();
                updTantousha.Update();
                JoukenReload_Tantouselect();
            }

           
            FindExpandVal();
            BindingBukken();
            updpnlBukken.Update();

        }
        #endregion

        #region btnClose_Click 担当者サブ画面を閉じる処理
        protected void btnClose_Click(object sender, EventArgs e)
        {
            OpenSubgridTantou.Text = "";
            subgridcall.Text = "";
            ifSentakuPopup.Src = "";
            mpeSentakuPopup.Hide();
            updSentakuPopup.Update();
            FindExpandVal();
            BindingBukken();
            updpnlBukken.Update();
        }
        #endregion
     
        #region btnHyoujiClose_Click 選択サブ画面を閉じる処理
        //「表示項目を設定」から閉じるボタンをクリックする
        protected void btnHyoujiSettingClose_Click(object sender, EventArgs e)
            {
            OpenSubgrid.Text = "";
            subgridcall.Text = "";
            ifpnlHyoujiSetPopUp.Src = "";//20211108 テテ added
            mpeHyoujiSetPopUp.Hide();//20211108 テテ added
            updHyoujiSet.Update();//20211108 テテ added                         
                                  // UpdateBukken_Click(UpdateBukken, e);
           
            FindExpandVal();
            BindingBukken();
            updpnlBukken.Update();
        }

        //「表示項目を設定」から保存ボタンをクリックする
        protected void btnHyoujiSettingSave_Click(object sender, EventArgs e)
        {
            try
            {
                OpenSubgrid.Text = "";
                subgridcall.Text = "";
                ifpnlHyoujiSetPopUp.Src = "";//20211108 テテ added
                mpeHyoujiSetPopUp.Hide();//20211108 テテ added
                updHyoujiSet.Update();//20211108 テテ added   
                UpdateBukken_Click(UpdateBukken, new EventArgs());
            }
            catch
            {

            }

        }


        #endregion
       

        #region datepickerから選択した後、対応テキストボックスに日付を設定
        protected void btnDateChange_Click(object sender, EventArgs e)
        {
            lblMitumoriStart.Text = Request.Form[lblMitumoriStart.UniqueID];
            lblMitumoriEnd.Text = Request.Form[lblMitumoriEnd.UniqueID];

        }
        #endregion

        //「表示項目を設定」の開く
        protected void btnBukkenHyoujiSetting_Click(object sender, EventArgs e)
        {
            try
            {
                Session["GamenName"] = "JC30BukkenList";
                SessionUtility.SetSession("HOME", "Master");//20211108 テテ added
                Session["HyoujiID"] = "bukken";
                Session["HyoujiSettingID"] = "bukken";//20211216 added
                try
                {
                    FindExpandVal();//20211216 added
                }
                catch
                {

                }
                ifpnlHyoujiSetPopUp.Attributes.Add("class", "HyoujiSettingIframe bukkeniframeStyle");//20211110 テテ

                String id = Request.QueryString["id"];
                ifpnlHyoujiSetPopUp.Src = "JC08HyoujiSetting.aspx?id=" + id;
                mpeHyoujiSetPopUp.Show();
                updHyoujiSet.Update();
                
            }
            catch (Exception ex)
            {
            }

        }
       
        protected void btndropdwonselect_Click(object sender, EventArgs e)
        {
            string dropdownId = selectedindex.Text;
            int kensuu = Convert.ToInt16(dropdownId);
            gvBukken.PageIndex = 0;
            gvBukken.PageSize = kensuu;
           
            this.BindingBukken();
            int endRowIndex = gvBukken.Rows.Count;
            lblHyojikensuu.Text = "1-" + endRowIndex + "/" + totalDataCount;
            GridViewRowCount.Text = endRowIndex.ToString();

        }
        protected void btnBukkenHyouji_Click(object sender, EventArgs e)
        {
            var bukkenId = (LinkButton)sender;
            Session["cBukken"] = bukkenId.Text;
            if (Session["cBukken"] != null)
            {
                if (!String.IsNullOrEmpty(Request.QueryString["id"]))
                {
                    String id = Request.QueryString["id"];
                    Response.Redirect("JC09BukkenSyousai.aspx?id=" + id);
                }
                else
                {
                    Response.Redirect("JC01Login.aspx");
                }
            }
            else
            {
                Response.Redirect("JC01Login.aspx");
            }

        }
        //subbukengridview　のコピー機能
        protected void btnCopy_Click(object sender, EventArgs e)
        {
            MitsuCD_flag.Text = "1";
            GridViewRow gvrow = (sender as Button).NamingContainer as GridViewRow;
            GridView gridview = gvrow.NamingContainer as GridView;
            Button ButtonCopy = sender as Button;
            int rowIndex = int.Parse(ButtonCopy.CommandArgument.ToString());
            string mitsumoriId = gridview.DataKeys[rowIndex].Value.ToString();

            //物件グリッドデータ
            GridViewRow gvParentBukken = (GridViewRow)gridview.NamingContainer;// this returns the row of parent gridview.
            int row = gvParentBukken.RowIndex; // this returns the row index of parent gridview
            string BukkenId = gvBukken.DataKeys[row].Value.ToString();
            FindExpandVal();
            MitsumoriCopy(BukkenId, mitsumoriId);
           
           // ReadBukken();
            ReadJoken();
            BindingBukken();
            updpnlBukken.Update();

        }
        //subbukengridview　の削除機能
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            GridViewRow gvrow = (sender as LinkButton).NamingContainer as GridViewRow;
            FindExpandVal();
            OpenSubgrid_Delete.Text = "1";
            tamitsumoriId.Text = "1";
            updpnlBukken.Update();
            Button ok = gvrow.FindControl("btnBukenSubDeleteOk") as Button;
            Button cancel = gvrow.FindControl("btnBukenSubDeleteCancel") as Button;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowAnkenChangeMessage",
                     "DeleteConfirmBox('削除してもよろしいでしょうか？','" + ok.ClientID + "','" + cancel.ClientID + "');", true);
            
        }


        protected void btnBukenSubDeleteOk_Click(object sender, EventArgs e)
        {
            GridViewRow gvrow = (sender as Button).NamingContainer as GridViewRow;
            GridView gridview = gvrow.NamingContainer as GridView;
            Button buttonDelete = sender as Button;
            int rowIndex = gvrow.RowIndex;
            string mitsumoriId = gridview.DataKeys[rowIndex].Value.ToString();

            //物件グリッドデータ
            GridViewRow gvParentBukken = (GridViewRow)gridview.NamingContainer;
            int row = gvParentBukken.RowIndex;
            string BukkenId = gvBukken.DataKeys[row].Value.ToString();
            // FindExpandVal();
            // strExpand = Session["strExpand"] as List<>;

           
            
            MitsumoriDelete(BukkenId, mitsumoriId);

            FindExpandVal();
            UpdateBukken_Click(UpdateBukken, e);
            OpenSubgrid_Delete.Text = "1";
            tamitsumoriId.Text = "1";

        }

        protected void btnBukenSubDeleteCancel_Click(object sender, EventArgs e)
        {
            updpnlBukken.Update();
        }

        //物件を新規作成
        protected void btnBukkenNew_Click(object sender, EventArgs e)
        {
            Session["cBukken"] = null;
            if (!String.IsNullOrEmpty(Request.QueryString["id"]))
            {
                String id = Request.QueryString["id"];
                Response.Redirect("JC09BukkenSyousai.aspx?id=" + id);
            }
            else
            {
                Response.Redirect("JC01Login.aspx");
            }
        }

        #region GV_Mitumori_PageIndexChanging
        protected void GV_Mitumori_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            
            try
            {
                GridView gv = sender as GridView;
                gv.PageIndex = e.NewPageIndex;
                //gvBukkenOriginal.PageIndex = e.NewPageIndex;
                int kensuu = Convert.ToInt16(DDL_Hyojikensuu.SelectedValue);
               // gvBukkenOriginal.PageSize = kensuu;
                int startRowIndex = (kensuu * e.NewPageIndex) + 1;
                rowindex = (kensuu * e.NewPageIndex);
                GV_Rowindex.Text = rowindex.ToString();
               // gvBukken.PageIndex = e.NewPageIndex;
                GridView1.PageIndex = e.NewPageIndex;
                
               // ReadBukken();
                ReadJoken();//20220201 added by lwin mar
                Session["strExpand"] = null;
                BindingBukken();
                GridView1.PageIndex = e.NewPageIndex;
                int endRowIndex = (kensuu * e.NewPageIndex) + gvBukken.Rows.Count;
                lblHyojikensuu.Text = startRowIndex + "-" + endRowIndex + "/" + totalDataCount;
                GridViewRowCount.Text = endRowIndex.ToString();
                Updatelabel.Update();
                updpnlBukken.Update();
            }
            catch(Exception ex)
            {

            }
        }
        #endregion



        #region 物件コードテキストの変更
        protected void txtcMitumori_TextChanged(object sender, EventArgs e)
        {
            if (!txtcBukken.Text.Equals(""))
            {
                String cMitumori = txtcBukken.Text;
                txtcBukken.Text = cMitumori.PadLeft(10, '0');
            }
            FindExpandVal();
            BindingBukken();
            updpnlBukken.Update(); 

        }
        #endregion

        #region クリアボタン
        protected void btnClear_Click(object sender, EventArgs e)
        {
            txtcBukken.Text = "";
            txtTokuisaki.Text = "";
            txtTokuisaki.Text = "";
            txtTokuisakiTantou.Text = "";
            lblMitumoriStart.Text = "";
            lblMitumoriEnd.Text = "";
           // DDL_Hyojikensuu.SelectedIndex = 0;

            //Session["dtTantou_MultiSelect"] = null;
            //Session["dtSearch"] = null;
            //Session["Hyouji"] = null;
            //Session["btnsearch"] = null;
            //Session["s_bukken"] = null;
            //Session["s_tokusaki"] = null;
            //Session["s_tokusakitantou"] = null;
            //Session["s_tantou"] = null;
            //Session["s_date"] = null;
            //DDL_Jyouken.Items.Clear();
            //DDL_Tantousya.Items.Clear();
            btntantousha.Text = "選択なし";
            btntantousha.CssClass = "JC30TantouKensakuBtnNull";
           // updTantousha.Update();
            // JoukenReload_Tantouselect();

        }
        #endregion
       
        #region JoukenReload
       
        private void JoukenReload_Tantouselect()
        {
            TC_Jouken.Controls.Clear();
            updJoken.Update();

            if (DDL_Jyouken.Items.Count > 0)
            {
                for (int i = 0; i < DDL_Jyouken.Items.Count; i++)
                {
                    Panel pn = new Panel();
                    pn.ID = DDL_Jyouken.Items[i].Value;
                    
                    pn.CssClass = "JC30JokenDiv";
                    Label jisyaLabel = new Label();
                    jisyaLabel.Font.Size = 10;
                    jisyaLabel.ForeColor = System.Drawing.ColorTranslator.FromHtml("#495057");
                    jisyaLabel.ID = DDL_Jyouken.Items[i].Value + "lbl_JokenLabel";

                    if (DDL_Jyouken.Items[i].Value.Contains("自社担当者"))
                    {
                        jisyaLabel.Text = "自社担当者";
                    }
                    else if (DDL_Jyouken.Items[i].Value.Contains("start"))
                    {
                        jisyaLabel.Text = DDL_Jyouken.Items[i].Value.Replace("start", "");
                    }
                    else if (DDL_Jyouken.Items[i].Value.Contains("end"))
                    {
                        jisyaLabel.Text = DDL_Jyouken.Items[i].Value.Replace("end", "");
                    }
                    else
                    {
                        jisyaLabel.Text = DDL_Jyouken.Items[i].Value;
                    }

                    jisyaLabel.CssClass = "JC30JokenLabel";

                    Label cjisya = new Label();
                    cjisya.Font.Size = 10;
                    cjisya.ForeColor = System.Drawing.ColorTranslator.FromHtml("#495057");
                    cjisya.ID = DDL_Jyouken.Items[i].Value + "lblsKYOTEN";
                    if (DDL_Jyouken.Items[i].Value.Contains("start"))
                    {
                        String end = "";
                        try
                        {
                            end = DDL_Jyouken.Items[i + 1].Value.Replace("end", "");
                        }
                        catch { }
                        if (!String.IsNullOrEmpty(end) && DDL_Jyouken.Items[i].Value.Replace("start", "") == end)
                        {
                            cjisya.Text = DDL_Jyouken.Items[i].Text + "～" + DDL_Jyouken.Items[i + 1].Text;
                            i = i + 1;
                        }
                        else
                        {
                            cjisya.Text = DDL_Jyouken.Items[i].Text + "～";
                        }
                    }
                    else if (DDL_Jyouken.Items[i].Value.Contains("end"))
                    {
                        cjisya.Text = "～" + DDL_Jyouken.Items[i].Text;
                    }
                    else
                    {
                        cjisya.Text = DDL_Jyouken.Items[i].Text;
                    }
                  //  cjisya.Text = DDL_Jyouken.Items[i].Text;
                   
                    Button btn_Cross = new Button();
                    btn_Cross.Font.Size = 9;
                    btn_Cross.BackColor = System.Drawing.Color.White;
                    btn_Cross.Text = "✕";
                    btn_Cross.ID = "btn_Jisyacross_" + jisyaLabel.Text + "_" + DDL_Jyouken.Items[i].Value;
                    btn_Cross.CssClass = "JC30JokenCross";
                    btn_Cross.Click += new EventHandler(btnJokenCross_Click);

                    pn.Controls.Add(jisyaLabel);
                    pn.Controls.Add(cjisya);
                    pn.Controls.Add(btn_Cross);

                    TC_Jouken.Controls.Add(pn);
                    updJoken.ContentTemplateContainer.Controls.Add(TC_Jouken);
                    updJoken.Update();
                }
            }

            updJoken.Update();
            updTantousha.Update();
            
            if (DDL_Tantousya.Items.Count > 0)
            {
                if (DDL_Tantousya.Items.Count == 1 && DDL_Tantousya.Items[0].Text == "選択なし")
                {
                    btntantousha.Text = "選択なし";
                    btntantousha.Attributes.Add("class", "JC30TantouKensakuBtnNull");
                    updTantousha.Update();
                }

                else
                {
                    btntantousha.Text = DDL_Tantousya.Items.Count + "人";
                    btntantousha.Attributes.Add("class", "JC30TantouKensakuBtn");
                    updTantousha.Update();

                }
            }
            else
            {
                btntantousha.Text = "選択なし";
                btntantousha.Attributes.Add("class", "JC30TantouKensakuBtnNull");
                updTantousha.Update();
            }
           
        }
     
        #endregion
      
        #region btnJokenCross_Click 条件削除
        protected void btnJokenCross_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            String id = btn.ID;
            id = id.Replace("btn_Jisyacross_", "");
            int index = id.IndexOf("_");
            
            String label = id.Substring(0, index);
            id = id.Substring(index + 1, (id.Length - label.Length) - 1);
            label_joken = id;
            ListItem removeItem = DDL_Jyouken.Items.FindByValue(id);
            code_joken = id.Replace("自社担当者", "");
           
            if (label.Equals("物件コード"))
            {
                txtcBukken.Text = "";
                updcMitumori.Update();
            }
            else if(label.Equals("得意先"))
            {
                txtTokuisaki.Text = "";
                updTokuisaki.Update();
            }
            else if (label.Equals("得意先担当者"))
            {
                txtTokuisakiTantou.Text = "";
                updTokuisakiTantou.Update();
            }

           else if (label.Equals("作成日"))
            {
                if (removeItem.Value.Equals("作成日start") && removeItem.Text.Equals(lblMitumoriStart.Text))
                {
                    lblMitumoriStart.Text = "";
                    btnMitumoriStartDate.Style["display"] = "block";
                    divMitumoriStartDate.Style["display"] = "none";
                    updMitumoriStartDate.Update();
                }
                else if (removeItem.Value.Equals("作成日end") && removeItem.Text.Equals(lblMitumoriEnd.Text))
                {
                    lblMitumoriEnd.Text = "";
                    btnMitumoriEndDate.Style["display"] = "block";
                    divMitumoriEndDate.Style["display"] = "none";
                    UpdMitumoriEndDate.Update();
                }
            }
            else if (label.Equals("自社担当者"))
            {
                try
                {
                    ListItem tantousya = DDL_Tantousya.Items.FindByValue(removeItem.Value.Replace("自社担当者", ""));
                    if (tantousya != null)
                    {
                        DDL_Tantousya.Items.Remove(tantousya);
                        if (DDL_Tantousya.Items.Count > 0)
                        {
                            if (DDL_Tantousya.Items.Count == 1 && DDL_Tantousya.Items[0].Text == "選択なし")
                            {
                                btntantousha.Text = "選択なし";
                                btntantousha.Attributes.Add("class", "JC30TantouKensakuBtnNull");
                            }

                            else
                            {
                                btntantousha.Text = DDL_Tantousya.Items.Count + "人";
                                btntantousha.Attributes.Add("class", "JC30TantouKensakuBtn");


                            }
                        }
                        else
                        {
                            btntantousha.Text = "選択なし";
                            btntantousha.Attributes.Add("class", "JC30TantouKensakuBtnNull");
                            updTantousha.Update();
                        }
                    }
                }
                catch { }
            }
            Session["strExpand"] = null; 
            UpdateBukken_Click(UpdateBukken, new EventArgs());

           
        }
        #endregion
        
        #region DDL_Hyojikensuu_SelectedIndexChanged //表示件数
        protected void DDL_Hyojikensuu_SelectedIndexChanged(object sender, EventArgs e)
        {
            int kensuu = Convert.ToInt16(DDL_Hyojikensuu.SelectedValue);
            rowindex = 0;
            GV_Rowindex.Text = rowindex.ToString();
            gvBukken.PageIndex = 0;
            gvBukkenOriginal.PageIndex = 0;
            gvBukken.PageSize = kensuu;
            gvBukkenOriginal.PageSize = kensuu;
            GridView1.PageIndex = 0;
            GridView1.PageSize = kensuu;
            Session["strExpand"] = null;
            ReadJoken();//20220201 added by lwin mar
            BindingBukken();
          //  int endRowIndex = gvBukken.Rows.Count;
            int endRowIndex = GridView1.Rows.Count;
            lblHyojikensuu.Text = "1-" + endRowIndex + "/" + totalDataCount;
            GridViewRowCount.Text = endRowIndex.ToString();
            Updatelabel.Update();
            updHyojikensuu.Update();
            updpnlBukken.Update();
        }

        #endregion
        protected void UpdateBukken_Click(object sender, EventArgs e)
        {
           
            ReadJoken();
            BindingBukken();
            int kensuu = Convert.ToInt16(DDL_Hyojikensuu.SelectedValue);
            gvBukken.PageIndex = 0;
            //rowindex = 0;
            gvBukken.PageSize = kensuu;
          //  GridView1.PageIndex = 0;//20220201 added by lwin mar
            GridView1.PageSize = kensuu;//20220201 added by lwin mar
            // int endRowIndex = gvBukken.Rows.Count;
            // int endRowIndex = (kensuu * e.NewPageIndex) + gvBukken.Rows.Count;//20220201 added by lwin mar
            int startRowIndex = (kensuu * GridView1.PageIndex) + 1;//20220201 added by lwin mar
            int endRowIndex = gvBukken.Rows.Count * (GridView1.PageIndex + 1);//20220201 added by lwin mar
            
            lblHyojikensuu.Text = startRowIndex+"-" + endRowIndex + "/" + totalDataCount;
            GridViewRowCount.Text = endRowIndex.ToString();
            Updatelabel.Update();
            JoukenReload_Tantouselect();
            
            updpnlBukken.Update();
        }
      


    }
}