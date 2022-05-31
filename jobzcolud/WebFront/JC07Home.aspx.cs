using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Data;
using System.Configuration;
using Service;
using Common;
using System.IO;

namespace jobzcolud.WebFront
{
    public partial class JC07Home : System.Web.UI.Page
    {       
        DataTable dt_bukken = new DataTable();
        DataTable dt_subBukken = new DataTable();
        DataTable dt_mitsumori = new DataTable();
        DataTable dt_img = new DataTable();
        DataTable dt_Mitsuimg = new DataTable();
        string[] strExpand = new string[10];

        string sort_flag = "";//20220303 lwin mar added
        DataTable dt = new DataTable();
        DataTable dt1 = new DataTable();


        JC07Home_Class JC07HomeClass = new JC07Home_Class();
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!String.IsNullOrEmpty(Request.QueryString["id"]))
            {
                if (!IsPostBack)
                {
                    ViewState["b_sortDirection"] = SortDirection.Descending;
                    ViewState["m_sortDirection"] = SortDirection.Descending;
                    ViewState["z_b_sortexpresion"] = "コード";
                    ViewState["z_m_sortexpresion"] = "見積コード";
                    ReadBukken();
                    BindingBukken();
                    ReadMitsumori();
                    BindingMitsumori();
                    Session["HyoujiSettingID"] = null;
                }
                else
                {
                    ReadBukken();
                    BindingBukken();
                    ReadMitsumori();
                    BindingMitsumori();
                }
                Session.Timeout = 480;
            }
            else
            {
                Response.Redirect("JC01Login.aspx");
            }
        }

        #region 物件データ
        protected void ReadBukken()
        {
            // 表示項目を設定から列取得
            dt_bukken = new DataTable();
            //if (Session["LoginId"] != null)
            if (!String.IsNullOrEmpty(Request.QueryString["id"]))
            {
                String id = Request.QueryString["id"];
                DataTable dt = JC01Login_Class.Get_DB(id);
                String db = dt.Rows[0]["db"].ToString();
                String mail = dt.Rows[0]["mail"].ToString();
                JC07HomeClass.loginId = mail;
                ConstantVal.DB_NAME = db;
                //ConstantVal.DB_NAME = Session["DB"].ToString();
                dt_bukken = JC07HomeClass.HomeBukken();
                ViewState["dt_bukken"] = dt_bukken;
            }
            else
            {
                Response.Redirect("JC01Login.aspx");
            }
        }
        protected void BindingBukken()
        {
            bool fcolimg = false;
            //if (ViewState["dt_bukken"] != null)
            //{
                //dt_bukken = ViewState["dt_bukken"] as DataTable;
                if (dt_bukken.Rows.Count == 0)
                {
                    MyBukken.Attributes.Add("style", "display:none");
                    btnHyoujisetPopUp.Attributes.Add("style", "display:none");
                    gvBukken.CssClass = gvBukken.CssClass.Replace("Bukkenhover", "");
                    gvBukken.CssClass = gvBukken.CssClass.Replace("hover", "");
                }
                if (dt_bukken.Rows.Count < 10)
                {
                    lkbtnBukkenAll.Attributes.Add("style", "display:none");
                }

                var columns = gvBukkenOriginal.Columns.CloneFields();
                gvBukken.Columns.Clear();
                gvBukken.Columns.Add(columns[0]);
                gvBukken.Columns.Add(columns[1]);

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
                    else if (HeaderName == "物件名")  //20220504 Added by phoo
                    {
                        gvBukken.Columns.Add(columns[3]);
                        gvBukken.Columns[col].HeaderText = HeaderName;
                    }
                    else if (HeaderName == "備考") //20220504 Added by phoo
                    {
                        gvBukken.Columns.Add(columns[4]);
                        gvBukken.Columns[col].HeaderText = HeaderName;
                    }
                    else if (HeaderName == "見積数") //20220504 Added by phoo
                    {
                        gvBukken.Columns.Add(columns[5]);
                        gvBukken.Columns[col].HeaderText = HeaderName;
                    }
                    else if (HeaderName == "得意先") //20220504 Added by phoo
                    {
                        gvBukken.Columns.Add(columns[6]);
                        gvBukken.Columns[col].HeaderText = HeaderName;
                    }
                    else if (HeaderName == "得意先担当者") //20220504 Added by phoo
                    {
                        gvBukken.Columns.Add(columns[7]);
                        gvBukken.Columns[col].HeaderText = HeaderName;
                    }
                    else if (HeaderName == "物件作成日")         //20220504 Added by phoo
                    {
                        gvBukken.Columns.Add(columns[8]);
                        gvBukken.Columns[col].HeaderText = "作成日";  //20220504 Added by phoo
                    }
                    else if (HeaderName == "自社担当者") //20220504 Added by phoo
                    {
                        gvBukken.Columns.Add(columns[9]);
                        gvBukken.Columns[col].HeaderText = HeaderName;
                    }
                    else if (HeaderName == "画像") //20220504 Added by phoo
                    {
                        gvBukken.Columns.Add(columns[10]);
                        gvBukken.Columns[col].HeaderText = HeaderName;
                        fcolimg = true;
                    }

                }
                col++;
            }

            if (fcolimg == true)
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
                }

                gvBukken.DataSource = dt_bukken;
                gvBukken.DataBind();
            //}
        }
        protected void gvBukken_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string bukkenId = (e.Row.DataItem as DataRowView).Row["cBUKKEN"].ToString();
                //サブ グリッドを表示かとかを決めるこうもく
                if ((e.Row.DataItem as DataRowView).Row["cCountMitsu"].ToString() == "")
                {
                    //e.Row.Cells[0].Attributes.Add("style", "display:none");                      
                    Image imgctrl = e.Row.Cells[0].Controls[0].FindControl("imgArrow") as Image;
                    imgctrl.Attributes.Add("style", "display:none");


                }
                //expand subgrid after postback
                if (strExpand[e.Row.RowIndex] != null)
                {
                    TextBox txtbx = (e.Row.Cells[1].Controls[0].FindControl("IsExpanded") as TextBox);//                       
                    txtbx.Text = strExpand[e.Row.RowIndex].ToString();
                }

                //サブ グリッド
                GridView gv = (GridView)e.Row.FindControl("gvSubBukken");
                //見積データの設定
                MitsumoriData(bukkenId, gv);
            }
        }
        #endregion

        #region 物件subgrid
        protected void MitsumoriData(string bukkenId, GridView gv)
        {
            if (!String.IsNullOrEmpty(Request.QueryString["id"]))
            {
                dt_subBukken = new DataTable();
                String id = Request.QueryString["id"];
                DataTable dt = JC01Login_Class.Get_DB(id);
                String db = dt.Rows[0]["db"].ToString();
                String mail = dt.Rows[0]["mail"].ToString();
                JC07HomeClass.loginId = mail;
                ConstantVal.DB_NAME = db;
                JC07HomeClass.bukkenId = bukkenId;
                //ConstantVal.DB_NAME = Session["DB"].ToString();

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
                            else if (HeaderName == "自社担当者") //20220502 Added by phoo
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
        #endregion

        #region 選択された物件を表示　JC09BukkenSyousai.aspx
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
        #endregion

        #region 選択された見積を表示　JC10MitsumoriTouroku.aspx
        protected void btnMitsuHyouji_Click(object sender, EventArgs e)
        {            
            var mistumoriId  = (LinkButton)sender;
            Session["cMitumori"] = mistumoriId.Text;
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
        #endregion

        #region subbukengridview　のコピー機能
        protected void btnCopy_Click(object sender, EventArgs e)
        {
            GridViewRow gvrow = (sender as Button).NamingContainer as GridViewRow;
            GridView gridview = gvrow.NamingContainer as GridView;
            Button ButtonCopy = sender as Button;
            int rowIndex = int.Parse(ButtonCopy.CommandArgument.ToString());
            string mitsumoriId = gridview.DataKeys[rowIndex].Value.ToString();

            //物件グリッドデータ
            GridViewRow gvParentBukken = (GridViewRow)gridview.NamingContainer;// this returns the row of parent gridview.
            int row = gvParentBukken.RowIndex; // this returns the row index of parent gridview
            string BukkenId = gvBukken.DataKeys[row].Value.ToString();
           
            MitsumoriCopy(BukkenId, mitsumoriId);            
            FindExpandVal();
            UpdateBukken_Click(UpdateBukken, e);           
            OpenSubgrid.Text = "1";
            tamitsumoriId.Text = "1";

            UpdateMitsumori_Click(UpdateMitsumori, e);
        }
        #endregion

        #region subbukengridview　の削除機能
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            GridViewRow gvrow = (sender as LinkButton).NamingContainer as GridViewRow;
            //GridView gridview = gvrow.NamingContainer as GridView;
            //LinkButton buttonDelete = sender as LinkButton;
            //int rowIndex = int.Parse(buttonDelete.CommandArgument.ToString());
            //string mitsumoriId = gridview.DataKeys[rowIndex].Value.ToString();

            //物件グリッドデータ
            /*GridViewRow gvParentBukken = (GridViewRow)gridview.NamingContainer;
            int row = gvParentBukken.RowIndex;
            string BukkenId = gvBukken.DataKeys[row].Value.ToString();
            FindExpandVal();
            MitsumoriDelete(BukkenId, mitsumoriId);*/
            FindExpandVal();
            OpenSubgrid.Text = "1";
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
           FindExpandVal();
           MitsumoriDelete(BukkenId, mitsumoriId);

           FindExpandVal();
           UpdateBukken_Click(UpdateBukken, e);
           OpenSubgrid.Text = "1";
           tamitsumoriId.Text = "1";

            
            UpdateMitsumori_Click(UpdateMitsumori,e);
        }

        protected void btnBukenSubDeleteCancel_Click(object sender, EventArgs e)
        {
            updpnlBukken.Update();
        }
        #endregion

        #region subbukengridview　の展開
        protected void FindExpandVal()
        {
            strExpand = (from p in Request.Form["IsExpanded"].Split(',') select p).ToArray();
        }
        #endregion

        #region 見積コピー機能
        protected void MitsumoriCopy(string bukkenId, string mitsumoriId)
        {
            if (!String.IsNullOrEmpty(Request.QueryString["id"]))
            {                     
                JC07HomeClass.bukkenId = bukkenId;
                JC07HomeClass.mitsumoriId = mitsumoriId;
                //ConstantVal.DB_NAME = Session["DB"].ToString();
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

                JC07HomeClass.MitsumoriCopy(loginId);
            }
            else
            {
                Response.Redirect("JC01Login.aspx");
            }
            
        }
        #endregion

        #region 見積削除機能
        protected void MitsumoriDelete(string BukkenId, string mitsumoriId)
        {
            if (!String.IsNullOrEmpty(Request.QueryString["id"]))
            {                           
                JC07HomeClass.bukkenId = BukkenId;
                JC07HomeClass.mitsumoriId = mitsumoriId;
                //ConstantVal.DB_NAME = Session["DB"].ToString();
                String id = Request.QueryString["id"];
                DataTable dt = JC01Login_Class.Get_DB(id);
                String db = dt.Rows[0]["db"].ToString();
                String mail = dt.Rows[0]["mail"].ToString();
                ConstantVal.DB_NAME = db;
                
                JC07HomeClass.MitsumoriDelete();
            }
            else
            {
                Response.Redirect("JC01Login.aspx");
            }
            
        }
        #endregion

        #region subbukengridview　の見積を追加
        protected void btnAddMitsumori_Click(object sender, EventArgs e)
        {
            //物件グリッドデータ
            GridViewRow gvrow = (sender as Button).NamingContainer as GridViewRow;
            GridView gridview = gvrow.NamingContainer as GridView;
            Button ButtonCopy = sender as Button;
            int rowIndex = int.Parse(ButtonCopy.CommandArgument.ToString());
            string BukkenId = gridview.DataKeys[rowIndex].Value.ToString();

           
            Session["cBukken"] = BukkenId;
            Session["cMitumori"] = null;
            Session["fBukkenName"] = "true";  //20220209 MiMi added
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
        #endregion

        # region subbukengridview　の他見積をコピーして追加
        protected void btnTaMitsumori_Click(object sender, EventArgs e)
        {
            GridViewRow gvrow = (sender as Button).NamingContainer as GridViewRow;
            GridView gridview = gvrow.NamingContainer as GridView;
            Button ButtonCopy = sender as Button;
            int rowIndex = int.Parse(ButtonCopy.CommandArgument.ToString());
            string BukkenId = gridview.DataKeys[rowIndex].Value.ToString();

            Session["cBukken"] = BukkenId;
            SessionUtility.SetSession("HOME", "Master");

            String id = Request.QueryString["id"];
            ifShinkiPopup.Src = "JC12MitsumoriKensaku.aspx?id=" + id;
            mpeShinkiPopup.Show();
            updShinkiPopup.Update();

            OpenSubgrid.Text = "0";
            tamitsumoriId.Text = "1";            
        }
        #endregion

        #region 他見積コピー画面から戻る
        protected void btn_CloseMitumoriSearch_Click(object sender, EventArgs e)
        {

            OpenSubgrid.Text = "0";
            ifShinkiPopup.Src = "";
            mpeShinkiPopup.Hide();
            updShinkiPopup.Update();
            FindExpandVal();
            Session["HyoujiSettingID"] = null;
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

        #region 物件を新規作成
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
        #endregion

        #region 見積をダイレクト作成
        protected void btnMitsumori_Click(object sender, EventArgs e)
        {
            Session["cBukken"] = null;
            Session["cMitumori"] = null;
            OpenSubgrid.Text = "0";
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
        #endregion

        #region　物件リスト表示項目設定
        //物件「表示項目を設定」の開く
        protected void btnBukkenHyoujiSetting_Click(object sender, EventArgs e)
        {
            try
            {
                SessionUtility.SetSession("HOME", "Master");//20211108 テテ added
                Session["HyoujiID"] = "bukken";
                Session["HyoujiSettingID"] = "bukken";
                ifpnlHyoujiSetPopUp.Attributes.Add("class", "HyoujiSettingIframe bukkeniframeStyle");//20211110 テテ   
                JC99NavBar navbar_Master = (JC99NavBar)this.Master;
                navbar_Master.BD_Master.Style.Add("overflow", "hidden");
                String id = Request.QueryString["id"];
                ifpnlHyoujiSetPopUp.Src = "JC08HyoujiSetting.aspx?id=" + id;
                mpeHyoujiSetPopUp.Show();
                updHyoujiSet.Update();
                OpenSubgrid.Text = "0";
            }
            catch (Exception es)
            {
            }

        }
        //「表示項目を設定」から保存ボタンをクリックする
        protected void btnSaveHyoujiClose_Click(object sender, EventArgs e)
        {

            ifpnlHyoujiSetPopUp.Src = "";//20211108 テテ added
            mpeHyoujiSetPopUp.Hide();//20211108 テテ added
            updHyoujiSet.Update();//20211108 テテ added   
            if (Session["HyoujiSettingID"] != null)
            {
                FindExpandVal();
                if (Session["HyoujiSettingID"].ToString() == "bukken")
                {
                    UpdateBukken_Click(UpdateBukken, e);
                    OpenSubgrid.Text = "1";//2021221 lwinmar added                                    
                }
                else if (Session["HyoujiSettingID"].ToString() == "mitsumori")
                {
                    UpdateMitsumori_Click(UpdateMitsumori, e);
                }

            }
            tamitsumoriId.Text = "";
            Session["HyoujiSettingID"] = null;
        }

        //「表示項目を設定」から閉じるボタンをクリックする
        protected void btnHyoujiSettingClose_Click(object sender, EventArgs e)
        {
            ifpnlHyoujiSetPopUp.Src = "";//20211108 テテ added
            mpeHyoujiSetPopUp.Hide();//20211108 テテ added
            updHyoujiSet.Update();//20211108 テテ added    
            FindExpandVal();
            Session["HyoujiSettingID"] = null;
            OpenSubgrid.Text = "0";//2021221 lwinmar added                               
        }
        #endregion

        #region すべて物件を確認
        protected void lkbtnBukkenAll_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(Request.QueryString["id"]))
            {
                String id = Request.QueryString["id"];
                Response.Redirect("JC30BukkenList.aspx?id=" + id);
            }
            else
            {
                Response.Redirect("JC01Login.aspx");
            }
        }
        #endregion

        #region 見積データ
        //見積ｓｑｌ
        protected void ReadMitsumori()
        {
            dt_mitsumori = new DataTable();
            if (!String.IsNullOrEmpty(Request.QueryString["id"]))
            {
                String id = Request.QueryString["id"];
                DataTable dt = JC01Login_Class.Get_DB(id);
                String db = dt.Rows[0]["db"].ToString();
                String mail = dt.Rows[0]["mail"].ToString();
                JC07HomeClass.loginId = mail;
                ConstantVal.DB_NAME = db;
                                
                dt_mitsumori = JC07HomeClass.HomeMitsumori();
                ViewState["dt_mitsumori"] = dt_mitsumori;
            }
            else
            {
                Response.Redirect("JC01Login.aspx");
            }
            
        }

        //見積グリードデータ
        protected void BindingMitsumori()
        {
            if (!String.IsNullOrEmpty(Request.QueryString["id"]))
            {
                bool fcolimg = false;
                //if (ViewState["dt_mitsumori"] != null)
                //{
                    //dt_mitsumori = ViewState["dt_mitsumori"] as DataTable;

                    if (dt_mitsumori.Rows.Count == 0)
                    {
                        MyMitsumori.Attributes.Add("style", "display:none");
                        btnHyoujiSettingM.Attributes.Add("style", "display:none");
                        //lkbtnMitsumoriAll.Attributes.Add("style", "display:none");
                        gvMitsumori.Width = 1000;
                        gvMitsumori.CssClass = gvMitsumori.CssClass.Replace("RowHover", "");
                    }
                    if (dt_mitsumori.Rows.Count < 10)
                    {
                        lkbtnMitsumoriAll.Attributes.Add("style", "display:none");
                    }

                    var columns = gvMitsumoriOriginal.Columns.CloneFields();
                    gvMitsumori.Columns.Clear();
                    gvMitsumori.Columns.Add(columns[0]);                   

                    DataColumnCollection dtcolumns = dt_mitsumori.Columns;
                foreach (DataColumn dr in dt_mitsumori.Columns)
                {
                    string HeaderName = dr.ColumnName.ToString();
                    int col = dtcolumns.IndexOf(HeaderName);
                    //見積グリードに項目列の追加
                    if (col > 0)
                    {

                        //幅の設定
                        if (HeaderName == "見積コード")  //20220504 Added By phoo
                        {
                            gvMitsumori.Columns.Add(columns[1]);
                            //gvMitsumori.Columns[col].ItemStyle.Width = 110;
                            gvMitsumori.Columns[col].HeaderText = HeaderName;
                        }
                        else if (HeaderName == "見積名") //20220504 Added By phoo
                        {
                            gvMitsumori.Columns.Add(columns[2]);
                            //gvMitsumori.Columns[col].ItemStyle.Width = 180;
                            gvMitsumori.Columns[col].HeaderText = HeaderName;
                        }
                        else if (HeaderName == "メモ") //20220504 Added By phoo
                        {
                            gvMitsumori.Columns.Add(columns[3]);
                            //gvMitsumori.Columns[col].ItemStyle.Width = 180;
                            gvMitsumori.Columns[col].HeaderText = HeaderName; //20220502 Added by phoo
                        }
                        else if (HeaderName == "備考") //20220504 Added By phoo
                        {
                            gvMitsumori.Columns.Add(columns[4]);
                            //gvMitsumori.Columns[col].ItemStyle.Width = 200;
                            gvMitsumori.Columns[col].HeaderText = HeaderName; //20220502 Added by phoo
                        }
                        else if (HeaderName == "得意先")  //20220504 Added By phoo
                        {
                            gvMitsumori.Columns.Add(columns[5]);
                            gvMitsumori.Columns[col].HeaderText = HeaderName; //20220502 Added by phoo
                        }
                        else if (HeaderName == "得意先担当者") //20220504 Added By phoo
                        {
                            gvMitsumori.Columns.Add(columns[6]);
                            gvMitsumori.Columns[col].HeaderText = HeaderName; //20220502 Added by phoo
                        }
                        else if (HeaderName == "見積日") //20220504 Added By phoo
                        {
                            gvMitsumori.Columns.Add(columns[7]);
                            gvMitsumori.Columns[col].HeaderText = HeaderName;
                        }
                        else if (HeaderName == "自社担当者") //20220504 Added By phoo
                        {
                            gvMitsumori.Columns.Add(columns[8]);
                            gvMitsumori.Columns[col].HeaderText = HeaderName;//20220502 Added by phoo
                        }
                        else if (HeaderName == "見積状態") //20220504 Added By phoo
                        {
                            gvMitsumori.Columns.Add(columns[9]);
                            gvMitsumori.Columns[col].HeaderText = HeaderName;
                        }
                        else if (HeaderName == "合計金額") //20220504 Added By phoo
                        {
                            gvMitsumori.Columns.Add(columns[10]);
                            gvMitsumori.Columns[col].HeaderText = HeaderName;
                        }
                        else if (HeaderName == "金額粗利") //20220504 Added By phoo
                        {
                            gvMitsumori.Columns.Add(columns[11]);
                            gvMitsumori.Columns[col].HeaderText = HeaderName;
                        }
                        else if (HeaderName == "作成者") //20220504 Added By phoo
                        {
                            gvMitsumori.Columns.Add(columns[12]);
                            gvMitsumori.Columns[col].HeaderText = HeaderName;
                        }
                        else if (HeaderName == "画像") //20220504 Added By phoo
                        {
                            gvMitsumori.Columns.Add(columns[13]);
                            gvMitsumori.Columns[col].HeaderText = HeaderName;
                            fcolimg = true;
                        }
                    }
                    col++;
                }

                if (fcolimg == true)
                    {
                        foreach (DataRow dr in dt_mitsumori.Rows)
                        {
                            if (dr["sFile"].ToString() != "")
                            {
                                string file = dr["sFile"].ToString();
                                string path = dr["sPATH_SERVER_SOURCE"].ToString();
                                //string urlstring = SetPhotoRoot(path, file);
                                if (file != "")
                                {
                                    dr["file64string"] = "../Img/gazou.png";
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
                    }
                   
                    gvMitsumori.DataSource = dt_mitsumori;
                    gvMitsumori.DataBind();
                //}
               
            }
            else
            {
                Response.Redirect("JC01Login.aspx");
            }
        }
        #endregion

        #region 見積グリードにある編集リンク
        protected void OnUpdateClick(object sender, EventArgs e)
        {
            GridViewRow gvrow = (sender as LinkButton).NamingContainer as GridViewRow;
            GridView gridview = gvrow.NamingContainer as GridView;
            LinkButton lnkbtn = sender as LinkButton;
            int rowIndex = int.Parse(lnkbtn.CommandArgument.ToString());
            string mitsumoriId = gridview.DataKeys[rowIndex].Value.ToString();
            gvMitsumori.EditIndex = -1;

            Session["cMitumori"] = mitsumoriId;
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
        #endregion

        #region 見積グリードにある削除リンク
        protected void OnDeleteClick(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(Request.QueryString["id"]))
            {
                //GridViewRow gvrow = (sender as LinkButton).NamingContainer as GridViewRow;
                //GridView gridview = gvrow.NamingContainer as GridView;
                //LinkButton lnkbtn = sender as LinkButton;
                //int rowIndex = int.Parse(lnkbtn.CommandArgument.ToString());
                //string mitsumoriId = gridview.DataKeys[rowIndex].Value.ToString();
                //JC07HomeClass.loginId = Session["LoginId"].ToString();
                //JC07HomeClass.mitsumoriId = mitsumoriId;
                //string BukkenId = JC07HomeClass.FindBukkenID();
                //MitsumoriDelete(BukkenId, mitsumoriId);

                //ReadMitsumori();
                //BindingMitsumori();

                FindExpandVal();
                string fSub = "0";
                foreach (string openSub in strExpand)
                {
                    if (openSub == "1")
                    {
                        fSub = "1";
                        break;
                    }
                }
                OpenSubgrid.Text = fSub;
                tamitsumoriId.Text = "0";
                updpnlBukken.Update();

                //updpnlMitsumori.Update();
                LinkButton lkb_delete = sender as LinkButton;
                GridViewRow gvRow = (GridViewRow)lkb_delete.NamingContainer;
                string cCoVal = gvRow.Cells[0].Text;
                Button ok = gvMitsumori.Rows[gvRow.RowIndex].FindControl("btnMitsumoriDeleteOk") as Button;
                Button cancel = gvMitsumori.Rows[gvRow.RowIndex].FindControl("btnBukenSubDeleteCancel") as Button;
                //updpnlMitsumori.Update();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowAnkenChangeMessage",
                         "DeleteConfirmBox('削除してもよろしいでしょうか？','" + ok.ClientID + "','" + cancel.ClientID + "');", true);
            }
            else
            {
                Response.Redirect("JC01Login.aspx");
            }
            
        }

        protected void btnMitsumoriDeleteOk_Click(object sender, EventArgs e)
        {
            /*GridViewRow gvrow = (sender as Button).NamingContainer as GridViewRow;
            GridView gridview = gvrow.NamingContainer as GridView;
            Button lnkbtn = sender as Button;
            int rowIndex = int.Parse(lnkbtn.CommandArgument.ToString());
            string mitsumoriId = gridview.DataKeys[rowIndex].Value.ToString();*/

            GridViewRow gvrow = (sender as Button).NamingContainer as GridViewRow;
            GridView gridview = gvrow.NamingContainer as GridView;
            int rowIndex = gvrow.RowIndex;
            string mitsumoriId = gridview.DataKeys[rowIndex].Value.ToString();
            String id = Request.QueryString["id"];
            DataTable dt = JC01Login_Class.Get_DB(id);
            String db = dt.Rows[0]["db"].ToString();
            String mail = dt.Rows[0]["mail"].ToString();
            JC07HomeClass.loginId = mail;
            ConstantVal.DB_NAME = db;
            JC07HomeClass.mitsumoriId = mitsumoriId;
            //ConstantVal.DB_NAME = Session["DB"].ToString();
           
            string BukkenId = JC07HomeClass.FindBukkenID();
            MitsumoriDelete(BukkenId, mitsumoriId);

            ReadMitsumori();
            BindingMitsumori();
            updpnlMitsumori.Update();

            FindExpandVal();
           
            string fSub = "0";
            foreach (string openSub in strExpand)
            {
                if (openSub == "1")
                {
                    fSub = "1";
                    break;
                }
            }
            OpenSubgrid.Text = fSub;
            tamitsumoriId.Text = "0";
            UpdateBukken_Click(UpdateBukken, e);
            //ReadBukken();
            //BindingBukken();
            //updpnlBukken.Update();
        }
        #endregion

        #region すべて見積を確認
        protected void lkbtnMitsumoriAll_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(Request.QueryString["id"]))
            {
                String id = Request.QueryString["id"];
                Response.Redirect("JC31MitsumoriList.aspx?id=" + id);
            }
            else
            {
                Response.Redirect("JC01Login.aspx");
            }
        }
        #endregion

        #region 見積「表示項目を設定」の開く
        protected void btnMitsuHyoujiSetting_Click(object sender, EventArgs e)
        {
            SessionUtility.SetSession("HOME", "Master");//20211108 テテ added
            Session["HyoujiID"] = "mitsumori";
            Session["HyoujiSettingID"] = "mitsumori";
            ifpnlHyoujiSetPopUp.Attributes.Add("class", "HyoujiSettingIframe mitsumoriiframeStyle");//20211110 テテ added            
            String id = Request.QueryString["id"];
            ifpnlHyoujiSetPopUp.Src = "JC08HyoujiSetting.aspx?id=" + id;
            mpeHyoujiSetPopUp.Show();
            updHyoujiSet.Update();
            
        }
        #endregion

        #region 物件グリードの更新
        protected void UpdateBukken_Click(object sender, EventArgs e)
        {
            ReadBukken();
            BindingBukken();
            updpnlBukken.Update();
        }
        #endregion

        #region 見積グリードの更新
        protected void UpdateMitsumori_Click(object sender, EventArgs e)
        {                       
            ReadMitsumori();
            BindingMitsumori();
            updpnlMitsumori.Update();
        }
        #endregion

        #region SetPhotoRoot
        private string SetPhotoRoot(string root, string nam)
        {
            string imgurl = "";
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
            //if (img.ImageUrl != "")
            //{
            //    img.ImageUrl = "";
            //}

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
           
            return imgurl; 
        }

        #endregion

        #region GV_Bukken_Sorting
        protected void GV_Bukken_Sorting(object sender, GridViewSortEventArgs e)
        {
            
            string sortExpression = e.SortExpression;
            ViewState["z_b_sortexpresion"] = e.SortExpression;
            if (GridViewSortDirection == SortDirection.Ascending)
            {
                GridViewSortDirection = SortDirection.Descending;
                Bukken_SortGridView(sortExpression, "DESC");
            }
            else
            {
                GridViewSortDirection = SortDirection.Ascending;
                Bukken_SortGridView(sortExpression, "ASC");
            }
        }
        #endregion
        #region GV_Mitsumori_Sorting
        protected void GV_Mitsumori_Sorting(object sender, GridViewSortEventArgs e)
        {

            string sortExpression = e.SortExpression;
            sort_flag = "mitsumori";
            ViewState["z_m_sortexpresion"] = e.SortExpression;
            if (GridViewSortDirection_m == SortDirection.Ascending)
            {
                GridViewSortDirection_m = SortDirection.Descending;
                Mitsu_SortGridView(sortExpression, "DESC");
            }
            else
            {
                GridViewSortDirection_m = SortDirection.Ascending;
                Mitsu_SortGridView(sortExpression, "ASC");
            }
        }
        #endregion
        #region GridViewSortDirection
        public SortDirection GridViewSortDirection
        {
            get
            {
               
                if (ViewState["b_sortDirection"] == null)
                    
                ViewState["b_sortDirection"] = SortDirection.Ascending;
                return (SortDirection)ViewState["b_sortDirection"];
            }
            set
            {
                ViewState["b_sortDirection"] = value;
            }
        }
        public SortDirection GridViewSortDirection_m
        {
            get
            {

                if (ViewState["m_sortDirection"] == null)

                    ViewState["m_sortDirection"] = SortDirection.Ascending;
                return (SortDirection)ViewState["m_sortDirection"];
            }
            set
            {
                ViewState["m_sortDirection"] = value;
            }
        }
        #endregion
        #region SortExpression

        //public string SortExpression
        //{

        //    get
        //    {

        //        if (ViewState["z_sortexpresion"] == null)

        //        ViewState["z_sortexpresion"] = this.gvBukken.DataKeyNames[0].ToString();
        //        return ViewState["z_sortexpresion"].ToString();
        //    }
        //    set
        //    {
        //        ViewState["z_sortexpresion"] = value;
        //    }

        //}
        #endregion
        #region SortGridView
        private void Bukken_SortGridView(string sortExpression, string direction)
        {
            dt.Clear();
            dt = (DataTable)ViewState["dt_bukken"];
            dt1 = dt.Copy();
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
                gvBukken.DataSource = dt1;
                gvBukken.DataBind();
                updpnlBukken.Update();
            }
        }
        private void Mitsu_SortGridView(string sortExpression, string direction)
        {
           
            dt.Clear();

            dt = (DataTable)ViewState["dt_mitsumori"];
            dt1 = dt.Copy();
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
                gvMitsumori.DataSource = dt1;
                gvMitsumori.DataBind();
                updpnlMitsumori.Update();
            }
        }
        #endregion
       

        #region GV_Bukken_RowCreated
        protected void GV_Bukken_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (ViewState["b_sortDirection"] != null && ViewState["z_b_sortexpresion"] != null)

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

                            if (sortLinkButton != null && ViewState["z_b_sortexpresion"].ToString() == sortLinkButton.CommandArgument)
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

        #region GV_Mitsumori_RowCreated
        protected void GV_Mitsumori_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (ViewState["m_sortDirection"] != null && ViewState["z_m_sortexpresion"] != null)

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

                            if (sortLinkButton != null && ViewState["z_m_sortexpresion"].ToString() == sortLinkButton.CommandArgument)
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
    }
}
