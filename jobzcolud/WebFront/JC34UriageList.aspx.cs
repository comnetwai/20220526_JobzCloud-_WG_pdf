using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;
using Service;
using MySql.Data.MySqlClient;
using jobzcolud.pdf;

namespace jobzcolud.WebFront
{
    public partial class JC34UriageList : System.Web.UI.Page
    {
        DataTable dt_GV_Uriage = new DataTable();
        DataTable dt_gv1 = new DataTable();
        public static string UriageCode, MitsumoriCode, UriageKenmei, HakkouStartDate, HakkouEndDate, UriageStartDate, UriageEndDate, UriageJoutai, Tokuisaki, Seikyusaki, Shouhinmei, UriageShanaiMemo;
        MySqlConnection con = null;
        public static string uriagecode = "";
        DataTable dt = new DataTable();
        DataTable dt1 = new DataTable();
        //string del_uriagecode = "";
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(Request.QueryString["id"]))
            {
                String id = Request.QueryString["id"];
                DataTable dt_db = JC01Login_Class.Get_DB(id);
                String db = dt_db.Rows[0]["db"].ToString();
                String mail = dt_db.Rows[0]["mail"].ToString();
                ConstantVal.DB_NAME = db;

                if (!IsPostBack)
                {
                    if (SessionUtility.GetSession("HOME") != null)
                    {
                        hdnHome.Value = SessionUtility.GetSession("HOME").ToString();
                        SessionUtility.SetSession("HOME", null);
                    }
                    //GridView1.PageIndex = 0;
                    //GridView1.PageSize = 20;
                    GV_Uriage.PageSize = 20;
                    DG_Hyouji_Dt();
                    BindGrid();
                    //DDL_Show_Count_SelectedIndexChanged(sender, e);
                    //GV_Uriage.DataBind();
                    //string msg = "Msg DB val =[" + JC01Login_Class.DB + "]  Session login id =[" + Session["LoginId"].ToString() + "]";
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowAnkenChangeMessage",
                    //               "ShowRogoMessage('" + msg + "','" + btnOK.ClientID + "');", true);

                }
                else
                {
                    //BindGrid();
                    //BindViewSate();
                    UriageColumn();
                    JoukenReload();
                    //GV_Uriage.PageIndex= 0;
                }
                Session.Timeout = 480;
            }
            else
            {
                Response.Redirect("JC01Login.aspx");
            }

        }

        private void Login_Tantou()
        {
            lblLoginUserName.Text = JC99NavBar_Class.Login_Tan_Name;
            lblLoginUserCode.Text = JC99NavBar_Class.Login_Tan_Code;
        }

        //#region BindViewSate()
        //public void BindViewSate()
        //{
        //    UriageColumn();
        //    dt_GV_Uriage = ViewState["GV_Uriage"] as DataTable;
        //    GV_Uriage.DataSource = dt_GV_Uriage;
        //    GV_Uriage.DataBind();

        //}
        //#endregion

        #region UriageColumn()
        public void UriageColumn()
        {
            if(ViewState["GV_Uriage"] != null)
            {
                DataTable dt_hyouji = new DataTable();

                String id = Request.QueryString["id"];
                DataTable dt_db = JC01Login_Class.Get_DB(id);
                String db = dt_db.Rows[0]["db"].ToString();
                String mail = dt_db.Rows[0]["mail"].ToString();
                ConstantVal.DB_NAME = db;

                dt_hyouji = JC34UriageList_Class.dt_uriage_hyouji;
               // DataTable dt= ViewState["GV_Uriage"] as DataTable;
                dt_GV_Uriage = ViewState["GV_Uriage"] as DataTable;
                //dt_gv1 = ViewState["gv1_Uriage"] as DataTable;
                var columns = GV_Uriage_Original.Columns.CloneFields();
                GV_Uriage.Columns.Clear();

                GV_Uriage.Columns.Add(columns[0]);
                for (int col = 0; col < dt_hyouji.Rows.Count; col++)
                {
                    String HeaderText = "";
                    HeaderText = dt_hyouji.Rows[col]["sHYOUJI"].ToString();
                    if (HeaderText == "売上コード")
                    {
                        GV_Uriage.Columns.Add(columns[1]);

                    }
                    else if (HeaderText == "見積コード")
                    {
                        GV_Uriage.Columns.Add(columns[2]);

                    }
                    else if (HeaderText == "請求先") //20220503 Added by phoo
                    {
                        GV_Uriage.Columns.Add(columns[3]);

                    }
                    else if (HeaderText == "得意先") //20220503 Added by phoo
                    {
                        GV_Uriage.Columns.Add(columns[4]);

                    }
                    else if (HeaderText == "売上件名")
                    {
                        GV_Uriage.Columns.Add(columns[5]);

                    }
                    else if (HeaderText == "自社担当者") //20220503 Added by phoo
                    {
                        GV_Uriage.Columns.Add(columns[6]);

                    }
                    else if (HeaderText == "売上日")
                    {
                        GV_Uriage.Columns.Add(columns[7]);

                    }
                    else if (HeaderText == "売上金額")
                    {
                        GV_Uriage.Columns.Add(columns[8]);

                    }
                    else if (HeaderText == "売上状態")
                    {
                        GV_Uriage.Columns.Add(columns[9]);


                    }
                    else if (HeaderText == "メモ")  //20220503 Added by phoo
                    {
                        GV_Uriage.Columns.Add(columns[10]);
                    }
                }
                //ViewState["Row"] = dt_GV_Uriage;
                //ViewState["sortDirection"] = SortDirection.Descending;
                //ViewState["z_sortexpresion"] = "売上コード";
                GV_Uriage.DataSource = dt_GV_Uriage;
                ViewState["GV_Uriage"] = dt_GV_Uriage;
                GV_Uriage.DataBind();

                GridView1.DataSource = dt_GV_Uriage;
                //ViewState["gv1_Uriage"] = dt_gv1;
                GridView1.DataBind();

                updUriageSyohinGrid.Update();
                updBody.Update();
            }
            
        }
        #endregion

        //public void BindGrid()
        //{
        //    if (JC34UriageList_Class.dt_uriage_hyouji.Rows.Count > 0)
        //    {
        //        dt_GV_Uriage = new DataTable();
        //        JC34UriageList_Class JC28UriageList = new JC34UriageList_Class();
        //        dt_GV_Uriage = JC28UriageList.UrigaeList("");
        //        ViewState["Row"] = dt_GV_Uriage;
        //        ViewState["sortDirection"] = SortDirection.Ascending;
        //        ViewState["z_sortexpresion"] = "売上コード";
        //        GV_Uriage.DataSource = dt_GV_Uriage;
        //        GV_Uriage.DataBind();
        //        ViewState["GV_Uriage"] = dt_GV_Uriage;

        //        UriageColumn();

        //        RowCount();
        //        updhyoujisuryou.Update();
        //    }
        //    else
        //    {
        //        LB_Show_Data.Text = "0-0";
        //        LB_Total.Text = "0";
        //    }

        //}

        public void BindGrid()        {            if (JC34UriageList_Class.dt_uriage_hyouji.Rows.Count > 0)            {                dt_GV_Uriage = new DataTable();                String id = Request.QueryString["id"];
                DataTable dt_db = JC01Login_Class.Get_DB(id);
                String db = dt_db.Rows[0]["db"].ToString();
                String mail = dt_db.Rows[0]["mail"].ToString();
                ConstantVal.DB_NAME = db;                JC34UriageList_Class JC28UriageList = new JC34UriageList_Class();                dt_GV_Uriage = JC28UriageList.UrigaeList("");
                //ViewState["Row"] = dt_GV_Uriage;
                ViewState["sortDirection"] = SortDirection.Descending;
                ViewState["z_sortexpresion"] = "売上コード";
                GV_Uriage.DataSource = dt_GV_Uriage;
                GV_Uriage.DataBind();
                ViewState["GV_Uriage"] = dt_GV_Uriage;                //dt_gv1 = new DataTable();                //JC34UriageList_Class JC28UriageList1 = new JC34UriageList_Class();

                //dt_gv1 = JC28UriageList.UrigaeList("");                GridView1.DataSource = dt_GV_Uriage;
                GridView1.DataBind();
                //ViewState["gv1_Uriage"] = dt_gv1;                UriageColumn();                RowCount();                updhyoujisuryou.Update();            }            else            {                LB_Show_Data.Text = "0-0/0";               // LB_Total.Text = "0";            }

        }

        protected void OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //GridView gv = sender as GridView;
            //gv.PageIndex = e.NewPageIndex;
            GV_Uriage.PageIndex = e.NewPageIndex;
            GridView1.PageIndex = e.NewPageIndex;
            UriageColumn();
            int last_data = 0;
            last_data = GV_Uriage.PageSize * (GV_Uriage.PageIndex + 1);
            //int total = Convert.ToInt32(totalrowcount);
            if (last_data < dt_GV_Uriage.Rows.Count)
            {
                LB_Show_Data.Text = ((GV_Uriage.PageSize * GV_Uriage.PageIndex) + 1).ToString() + "-" + last_data.ToString()+"/"+dt_GV_Uriage.Rows.Count.ToString();
            }
            else
            {
                LB_Show_Data.Text = ((GV_Uriage.PageSize * GV_Uriage.PageIndex) + 1).ToString() + "-" + dt_GV_Uriage.Rows.Count.ToString() + "/" + dt_GV_Uriage.Rows.Count.ToString();
            }
            updhyoujisuryou.Update();
            updUriageSyohinGrid.Update();
        }

        private bool DG_Hyouji_Dt()
        {
            String id = Request.QueryString["id"];
            DataTable dt_db = JC01Login_Class.Get_DB(id);
            String db = dt_db.Rows[0]["db"].ToString();
            String mail = dt_db.Rows[0]["mail"].ToString();
            ConstantVal.DB_NAME = db;

            JC34UriageList_Class uriageList_class = new JC34UriageList_Class();
            uriageList_class.Uriage_Hyouji();
            return true;
        }

        protected void TB_UriageCode_TextChanged(object sender, EventArgs e)
        {
            if (TB_UriageCode.Text != "")
            {
                TB_UriageCode.Text = TB_UriageCode.Text.PadLeft(10, '0');
            }
        }

        protected void TB_MitsumoriCode_TextChanged(object sender, EventArgs e)
        {
            if (TB_MitsumoriCode.Text != "")
            {
                TB_MitsumoriCode.Text = TB_MitsumoriCode.Text.PadLeft(10, '0');
            }
        }

        protected void BT_Tantousya_Click(object sender, EventArgs e)
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
            updSentakuPopup.Update();
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            ifSentakuPopup.Src = "";
            mpeSentakuPopup.Hide();
            updSentakuPopup.Update();
        }

        protected void btnJishaTantouSelect_Click(object sender, EventArgs e)
        {
            if (Session["dtTantou_MultiSelect"] != null)
            {
                DataTable dt = Session["dtTantou_MultiSelect"] as DataTable;
                DDL_Tantousya.DataSource = dt;
                DDL_Tantousya.DataTextField = "name";
                DDL_Tantousya.DataValueField = "code";
                DDL_Tantousya.DataBind();
                if (dt.Rows.Count > 0)
                {
                    BT_Tantousya.Text = DDL_Tantousya.Items.Count + "人";
                    BT_Tantousya.CssClass = "JC34JishaTantouSelect";
                }
                else
                {
                    BT_Tantousya.Text = "選択なし";
                    BT_Tantousya.CssClass = "JC34JishaTantou";
                }
                updTantousha.Update();
                //JoukenReload();
            }

            ifSentakuPopup.Src = "";
            mpeSentakuPopup.Hide();
            updSentakuPopup.Update();
        }

        #region JoukenReload()
        private void JoukenReload()
        {
            TC_Jouken.Controls.Clear();
            updJoken.Update();

            #region
            //if (DDL_Tantousya.Items.Count > 0)
            //{
            //    BT_Tantousya.Text = DDL_Tantousya.Items.Count + "人";
            //    BT_Tantousya.CssClass = "JC10CancelBtn";

            //    for (int i = 0; i < DDL_Tantousya.Items.Count; i++)
            //    {
            //        Panel pn = new Panel();
            //        pn.ID = DDL_Tantousya.Items[i].Value;
            //        pn.CssClass = "JC28JokenDiv";
            //        Label jisyaLabel = new Label();
            //        jisyaLabel.Font.Size = 10;
            //        jisyaLabel.ForeColor = System.Drawing.ColorTranslator.FromHtml("#a0a19d");
            //        jisyaLabel.ID = DDL_Tantousya.Items[i].Value + "lbl_JokenLabel";
            //        jisyaLabel.Text = "自社担当者";
            //        jisyaLabel.CssClass = "JC12JokenLabel";

            //        Label cjisya = new Label();
            //        cjisya.Font.Size = 10;
            //        cjisya.ForeColor = System.Drawing.ColorTranslator.FromHtml("#a0a19d");
            //        cjisya.ID = DDL_Tantousya.Items[i].Value + "lblsKYOTEN";
            //        cjisya.Text = DDL_Tantousya.Items[i].Text;

            //        Button btn_Cross = new Button();
            //        btn_Cross.Font.Size = 9;
            //        btn_Cross.BackColor = System.Drawing.Color.White;
            //        btn_Cross.Text = "✕";
            //        btn_Cross.ID = "btn_Jisyacross" + DDL_Tantousya.Items[i].Value;
            //        btn_Cross.CssClass = "JC12JokenCross";
            //        btn_Cross.Click += new EventHandler(btnJokenCross_Click);

            //        pn.Controls.Add(jisyaLabel);
            //        pn.Controls.Add(cjisya);
            //        pn.Controls.Add(btn_Cross);

            //        TC_Jouken.Controls.Add(pn);
            //        updJoken.Update();
            //    }
            //}
            //else
            //{
            //    BT_Tantousya.Text = "選択なし";
            //    BT_Tantousya.CssClass = "JC12CancelBtn";
            //}
            #endregion

            if (DDL_Jyouken.Items.Count > 0)
            {
                for (int i = 0; i < DDL_Jyouken.Items.Count; i++)
                {
                    Panel pn = new Panel();
                    pn.ID = DDL_Jyouken.Items[i].Value;
                    pn.CssClass = "JC34JokenDiv";
                    Label jisyaLabel = new Label();
                    jisyaLabel.Font.Size = 10;
                    //jisyaLabel.ForeColor = System.Drawing.ColorTranslator.FromHtml("#a0a19d");
                    jisyaLabel.ForeColor = System.Drawing.ColorTranslator.FromHtml("#495057");
                    jisyaLabel.ID = DDL_Jyouken.Items[i].Value + "lbl_JokenLabel";
                    if (DDL_Jyouken.Items[i].Value.Contains("自社担当者"))
                    {
                        jisyaLabel.Text = "自社担当者";
                    }
                    else if (DDL_Jyouken.Items[i].Value.Contains("Start"))
                    {
                        jisyaLabel.Text = DDL_Jyouken.Items[i].Value.Replace("Start", "");
                    }
                    else if (DDL_Jyouken.Items[i].Value.Contains("End"))
                    {
                        jisyaLabel.Text = DDL_Jyouken.Items[i].Value.Replace("End", "");
                    }
                    else if (DDL_Jyouken.Items[i].Value.Contains("商品名"))
                    {
                        jisyaLabel.Text = "商品名";
                    }
                    else
                    {
                        jisyaLabel.Text = DDL_Jyouken.Items[i].Value;
                    }
                    jisyaLabel.CssClass = "JC34JokenLabel";

                    Label cjisya = new Label();
                    cjisya.Font.Size = 10;
                    //cjisya.ForeColor = System.Drawing.ColorTranslator.FromHtml("#a0a19d");
                    cjisya.ForeColor = System.Drawing.ColorTranslator.FromHtml("#495057");
                    cjisya.ID = DDL_Jyouken.Items[i].Value + "lblsKYOTEN";
                    if (DDL_Jyouken.Items[i].Value.Contains("Start"))
                    {
                        String end = "";
                        try
                        {
                            end = DDL_Jyouken.Items[i + 1].Value.Replace("End", "");
                        }
                        catch { }
                        if (!String.IsNullOrEmpty(end) && DDL_Jyouken.Items[i].Value.Replace("Start", "") == end)
                        {
                            cjisya.Text = DDL_Jyouken.Items[i].Text + "～" + DDL_Jyouken.Items[i + 1].Text;
                            i = i + 1;
                        }
                        else
                        {
                            cjisya.Text = DDL_Jyouken.Items[i].Text + "～";
                        }
                    }
                    else if (DDL_Jyouken.Items[i].Value.Contains("End"))
                    {
                        cjisya.Text = "～" + DDL_Jyouken.Items[i].Text;
                    }
                    else
                    {
                        cjisya.Text = DDL_Jyouken.Items[i].Text;
                    }
                    Button btn_Cross = new Button();
                    btn_Cross.Font.Size = 9;
                    btn_Cross.BackColor = System.Drawing.Color.White;
                    btn_Cross.Text = "✕";
                    btn_Cross.ID = "btn_Jisyacross" + DDL_Jyouken.Items[i].Value;
                    btn_Cross.CssClass = "JC34JokenCross";
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
           
            updBody.Update();
        }
        #endregion

        #region btnJokenCross
        protected void btnJokenCross_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            String id = btn.ID;
            id = id.Replace("btn_Jisyacross", "");

            ListItem removeItem = DDL_Jyouken.Items.FindByValue(id);
            DDL_Jyouken.Items.Remove(removeItem);
            JoukenReload();
            if (id == "売上コード")
            {
                TB_UriageCode.Text = "";
                UriageCode = "";
                updcUriageCode.Update();
            }
            else if (id == "見積コード")
            {
                TB_MitsumoriCode.Text = "";
                MitsumoriCode = "";
                updcMitsumoriCode.Update();
            }
            else if (id == "売上件名")
            {
                TB_UriageKenmei.Text = "";
                UriageKenmei = "";
                updUriageKenmei.Update();
            }
            else if (id == "発行日Start")
            {
                //TB_HakkouStartDate.Text = string.Empty;
                //HakkouStartDate = "";
                //updHakkouStartDate.Update();
                BT_dHakkouStartCross_Click(sender, e);
            }
            else if (id == "発行日End")
            {
                //TB_HakkouEndDate.Text = string.Empty;
                //HakkouEndDate = "";
                //updHakkouEndDate.Update();
                BT_dHakkouEndCross_Click(sender, e);
            }
            else if (id == "売上日Start")
            {
                // TB_UriageStartDate.Text = "";
                //UriageStartDate = "";
                //updUriageStartDate.Update();
                BT_dUriageStartCross_Click(sender, e);
            }
            else if (id == "売上日End")
            {
                //TB_UriageEndDate.Text = "";
                //UriageEndDate = "";
                //updUriageEndDate.Update();
                BT_dUriageEndCross_Click(sender, e);
            }
            else if (id == "売上状態")
            {
                //TB_UraigeJoutai.Text = "";
                DDL_UraigeJoutai.SelectedIndex = 0;
                UriageJoutai = "";
                updUraigeJoutai.Update();
            }
            else if (id == "得意先")
            {
                TB_Tokuisaki.Text = "";
                Tokuisaki = "";
                updTokuisaki.Update();
            }
            else if (id == "請求先")
            {
                TB_Seikyusaki.Text = "";
                Seikyusaki = "";
                updSeikyusaki.Update();
            }
            else if (id == "商品名")
            {
                TB_Shouhinmei.Text = "";
                Shouhinmei = "";
                updShouhinmei.Update();
            }
            else if (id == "メモ") //20220503 Added by phoo
            {
                TB_UriageShanaiMemo.Text = "";
                UriageShanaiMemo = "";
                updUriageShanaiMemo.Update();
            }
            else
            {
                string tantou = id.Replace("自社担当者", "");
                ListItem remove = DDL_Tantousya.Items.FindByValue(tantou);
                DDL_Tantousya.Items.Remove(remove);
                if (DDL_Tantousya.Items.Count > 0)
                {
                    BT_Tantousya.Text = DDL_Tantousya.Items.Count + "人";
                    BT_Tantousya.CssClass = "JC34JishaTantouSelect";
                }
                else
                {
                    BT_Tantousya.Text = "選択なし";
                    BT_Tantousya.CssClass = "JC34JishaTantou";
                }
            }
            //search();
            BT_UriageHyouji_Click(sender, e);            updUriageSyohinGrid.Update();
        }
        #endregion

        #region DDL_Show_Count_SelectedIndexChanged
        protected void DDL_Show_Count_SelectedIndexChanged(object sender, EventArgs e)
        {
            GV_Uriage.PageIndex = 0;
            GridView1.PageIndex = 0;
            GV_Uriage.DataBind();
            GridView1.DataBind();
            if (DDL_Show_Count.SelectedIndex == 0)
            {
                GV_Uriage.PageSize = 20;
                GridView1.PageSize = 20;
            }
            else if (DDL_Show_Count.SelectedIndex == 1)
            {
                GV_Uriage.PageSize = 30;
                GridView1.PageSize = 30;
            }
            else if (DDL_Show_Count.SelectedIndex == 2)
            {
                GV_Uriage.PageSize = 50;
                GridView1.PageSize = 50;
            }
            //BindGrid();
            UriageColumn();
            RowCount();

        }
        #endregion

        #region todelete
       
        protected void BT_DateChange_Click(object sender, EventArgs e)
        {
            //TB_HakkouStartDate.Text = Request.Form[TB_HakkouStartDate.UniqueID];
            //TB_HakkouEndDate.Text = Request.Form[TB_HakkouEndDate.UniqueID];
            //TB_UriageStartDate.Text = Request.Form[TB_UriageStartDate.UniqueID];
            //TB_UriageEndDate.Text = Request.Form[TB_UriageEndDate.UniqueID];

        }
        
        //protected void BT_HakkouStartDate_Click(object sender,EventArgs e)
        //{
        //    //ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Name: hello');", true);
        //    SessionUtility.SetSession("HOME", "Master");
        //    ifdatePopup.Src = "JCHidukeSelect.aspx";
        //    mpedatePopup.Show();

        //    ViewState["DATETIME"] = BT_HakkouStartDate.ID;
        //    upddatePopup.Update();

        //}

        
        //protected void BT_HakkouEndDate_Click(object sender, EventArgs e)
        //{
        //    //ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Name: hello');", true);
        //    SessionUtility.SetSession("HOME", "Master");
        //    ifdatePopup.Src = "JCHidukeSelect.aspx";
        //    mpedatePopup.Show();

        //    ViewState["DATETIME"] = BT_HakkouEndDate.ID;
        //    upddatePopup.Update();

        //}
        
        //protected void BT_UriageStartDate_Click(object sender, EventArgs e)
        //{
        //    //ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Name: hello');", true);
        //    SessionUtility.SetSession("HOME", "Master");
        //    ifdatePopup.Src = "JCHidukeSelect.aspx";
        //    mpedatePopup.Show();

        //    ViewState["DATETIME"] = BT_UriageStartDate.ID;
        //    upddatePopup.Update();

        //}
       
        //protected void BT_UriageEndDate_Click(object sender, EventArgs e)
        //{
        //    //ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Name: hello');", true);
        //    SessionUtility.SetSession("HOME", "Master");
        //    ifdatePopup.Src = "JCHidukeSelect.aspx";
        //    mpedatePopup.Show();

        //    ViewState["DATETIME"] = BT_UriageEndDate.ID;
        //    upddatePopup.Update();

        //}
       
        #endregion


        #region "日付カレンダーポップアップの【X】ボタンクリック処理"        protected void btnCalendarClose_Click(object sender, EventArgs e)        {
            // 【日付サブ画面】を閉じる
            CloseDateSub();
            // フォーカスする
            CalendarFoucs();        }


        #endregion

        #region "選択サブ画面を閉じる処理"        protected void CloseDateSub()        {            ifdatePopup.Src = "";            mpedatePopup.Hide();            upddatePopup.Update();        }


        #endregion

        #region "日付サブ画面を閉じる時のフォーカス処理"        protected void CalendarFoucs()        {            string strBtnID = (string)ViewState["DATETIME"];            if (strBtnID == BT_HakkouStartDate.ID)            {                if (BT_HakkouStartDate.Style["display"] != "none")                {                    BT_HakkouStartDate.Focus();                }            }            else if (strBtnID == BT_HakkouEndDate.ID)            {                if (BT_HakkouEndDate.Style["display"] != "none")                {                    BT_HakkouEndDate.Focus();                }
            }

            else if (strBtnID == BT_UriageStartDate.ID)
            {
                if (BT_UriageStartDate.Style["display"] != "none")
                {
                    BT_UriageStartDate.Focus();
                }
            }

            else if (strBtnID == BT_UriageEndDate.ID)
            {
                if (BT_UriageEndDate.Style["display"] != "none")
                {
                    BT_UriageEndDate.Focus();
                }
            }

        }


        #endregion

        #region "日付カレンダーポップアップの【設定】ボタンを押す処理"        protected void btnCalendarSettei_Click(object sender, EventArgs e)        {            DateTime dtSelectedDate;

            // 【日付サブ画面】を閉じる
            CloseDateSub();            string strBtnID = (string)ViewState["DATETIME"];            string strCalendarDateTime = (string)Session["CALENDARDATETIME"];            if (Session["CALENDARDATETIME"] != null)            {                strCalendarDateTime = (string)Session["CALENDARDATETIME"];                dtSelectedDate = DateTime.Parse(strCalendarDateTime);                if (strBtnID == BT_HakkouStartDate.ID)                {                    HakkouStartDateDataBind(strCalendarDateTime, strBtnID);                }                else if (strBtnID == BT_HakkouEndDate.ID)                {                    HakkouEndDateDataBind(strCalendarDateTime, strBtnID);                }                else if (strBtnID == BT_UriageStartDate.ID)                {                    UriageStartDateDataBind(strCalendarDateTime, strBtnID);                }                else if (strBtnID == BT_UriageEndDate.ID)                {                    UriageEndDateDataBind(strCalendarDateTime, strBtnID);                }
                //lblHdnAnkenTextChange.Text = "true";
            }            CalendarFoucs();        }
        #endregion

        #region datedatabind_todelete
                //protected void HakkouStartDateDataBind(string strCalendarDateTime, string strImgbtnID)
                                          //{
                                          //    DateTime dtDeliveryDate = DateTime.Parse(strCalendarDateTime);
                                          //    TB_HakkouStartDate.Text = dtDeliveryDate.ToString("yyyy/MM/dd");
                                          //    //BT_HakkouStartDate.Style["display"] = "none";
                                          //    //divHakkouStartDate.Style["display"] = "block";
                                          //    updHakkouStartDate.Update();
                                          //}

        //protected void HakkouEndDateDataBind(string strCalendarDateTime, string strImgbtnID)
        //{
        //    DateTime dtDeliveryDate = DateTime.Parse(strCalendarDateTime);
        //    TB_HakkouEndDate.Text = dtDeliveryDate.ToString("yyyy/MM/dd");
        //    //BT_HakkouStartDate.Style["display"] = "none";
        //    //divHakkouStartDate.Style["display"] = "block";
        //    updHakkouEndDate.Update();
        //}
      
        //protected void UriageStartDateDataBind(string strCalendarDateTime, string strImgbtnID)
        //{
        //    DateTime dtDeliveryDate = DateTime.Parse(strCalendarDateTime);
        //    TB_UriageStartDate.Text = dtDeliveryDate.ToString("yyyy/MM/dd");
        //    //BT_HakkouStartDate.Style["display"] = "none";
        //    //divHakkouStartDate.Style["display"] = "block";
        //    updUriageStartDate.Update();
        //}

        //protected void UriageEndDateDataBind(string strCalendarDateTime, string strImgbtnID)
        //{
        //    DateTime dtDeliveryDate = DateTime.Parse(strCalendarDateTime);
        //    TB_UriageEndDate.Text = dtDeliveryDate.ToString("yyyy/MM/dd");
        //    //BT_HakkouStartDate.Style["display"] = "none";
        //    //divHakkouStartDate.Style["display"] = "block";
        //    updUriageEndDate.Update();
        //}
       
        #endregion

        #region BT_Uriage_Display_Click
        //protected void BT_Uriage_Display_Click(object sender, EventArgs e)
        //{
        //    string bukkenId = TB_Uriage_Code.Text;
        //    Session["cUriage"] = bukkenId;
        //    if (Session["cUriage"] != null)
        //    {
        //        JC99NavBar.insatsusettei = false;
        //        Response.Redirect("JC27UriageTouroku.aspx");
        //    }
        //    else
        //    {
        //        Response.Redirect("JC01Login.aspx");
        //    }
        //}
        #endregion


        #region LKB_uriagecode
        protected void LKB_uriagecode_Click(Object sender, EventArgs e)
        {
            //Session["fcopy"] = "false";
            var lk_uriagecode = (LinkButton)sender;
            Session["cUriage"] = lk_uriagecode.Text;
            if (Session["cUriage"] != null)
            {
                JC99NavBar.insatsusettei = false;
                Session["uriageCode"] = "false";
                if (!String.IsNullOrEmpty(Request.QueryString["id"]))
                {
                    String id = Request.QueryString["id"];
                    Response.Redirect("JC27UriageTouroku.aspx?id=" + id);
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

        #region LKB_UriageCopy_Clicked
        //protected void LKB_UriageCopy_Clicked(Object sender, EventArgs e)        //{        //    var lbk_copy = sender as LinkButton;        //    GridViewRow gvRow = (GridViewRow)lbk_copy.NamingContainer;        //    string uriageCode = (GV_Uriage.Rows[gvRow.RowIndex].FindControl("LKB_cUriage") as LinkButton).Text;        //    Session["cUriage"] = uriageCode;        //    if (Session["cUriage"] != null)        //    {        //        JC99NavBar.insatsusettei = false;        //        Session["fcopy"] = "true";        //        Response.Redirect("JC27UriageTouroku.aspx");        //    }        //    else        //    {        //        Response.Redirect("JC01Login.aspx");        //    }        //}
        #endregion

        protected void GV_Uriage_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton lbk_pdf = (e.Row.FindControl("LKB_UriagePDF") as LinkButton);

                String id = Request.QueryString["id"];
                DataTable dt_db = JC01Login_Class.Get_DB(id);
                String db = dt_db.Rows[0]["db"].ToString();
                String mail = dt_db.Rows[0]["mail"].ToString();
                ConstantVal.DB_NAME = db;

                JC27UriageTouroku_Class settei = new JC27UriageTouroku_Class();
                Login_Tantou();
                settei.cTANTOUSHA = lblLoginUserCode.Text;
                ConstantVal.DB_NAME = db;
                DataTable dt_gettantou = settei.GetTantou();
                if (dt_gettantou.Rows.Count > 0)
                {
                    if (dt_gettantou.Rows[0]["Shoshiki"].ToString() == "1")
                    {
                        lbk_pdf.Text = "兼請求書PDF出力";
                    }
                    else
                    {
                        lbk_pdf.Text = "納品書と請求書PDF出力";
                    }
                }
            }

            
        }
        #endregion

        protected void GV_Uriage_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "PDF")
            {
                GridViewRow row = (GridViewRow)(((Control)e.CommandSource).NamingContainer);
                int index = row.RowIndex;
                uriagecode= GV_Uriage.DataKeys[index].Value.ToString();
                Session["uriageCode"] = "true";
                upd_Hidden.Update();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ClickPDFButton",
                   "ClickPDFButton();", true);
            }
        }

        protected void BT_UriagePDF_Click(object sender,EventArgs e)
        {
            String id = Request.QueryString["id"];
            DataTable dt_db = JC01Login_Class.Get_DB(id);
            String db = dt_db.Rows[0]["db"].ToString();
            String mail = dt_db.Rows[0]["mail"].ToString();
            ConstantVal.DB_NAME = db;

            JC27UriageTouroku_Class settei = new JC27UriageTouroku_Class();
            Login_Tantou();
            settei.cTANTOUSHA = lblLoginUserCode.Text;
            DataTable dt_gettantou = settei.GetTantou();

            if (dt_gettantou.Rows.Count > 0)
            {
                if (dt_gettantou.Rows[0]["Shoshiki"].ToString() == "2")
                {
                    string flagrogo = "";
                    String flagbikou = "";  //20220131 MiMi Added
                    String cKyoten = "";
                    String sBikou = ""; //20220131 MiMi Added
                    String dUriage = ""; //20220131 MiMi Added
                    DataTable dt_r_uriage = new DataTable();
                    JC27UriageTouroku_Class r_uriage_data = new JC27UriageTouroku_Class();
                    r_uriage_data.cURIAGE = uriagecode;
                    ConstantVal.DB_NAME = db;
                    dt_r_uriage = r_uriage_data.UriageData();
                    cKyoten = dt_r_uriage.Rows[0]["cCO"].ToString();
                    sBikou = dt_r_uriage.Rows[0]["sbikou"].ToString();  //20220131 MiMi Added
                    dUriage = dt_r_uriage.Rows[0]["dURIAGE"].ToString();  //20220131 MiMi Added

                    if (cKyoten != "")
                    {
                        ConstantVal.DB_NAME = db;
                        JC_ClientConnecction_Class jc = new JC_ClientConnecction_Class();
                        jc.loginId = mail;
                        con = jc.GetConnection();
                        con.Open();
                        if (dt_gettantou.Rows.Count > 0)
                        {
                            flagrogo = dt_gettantou.Rows[0]["rogo"].ToString().TrimEnd();
                            #region todelete
                            //if (dt_gettantou.Rows[0]["rogo"].ToString().TrimEnd() == "1")
                            //{
                            //    flagrogo = "1";
                            //}
                            //else if (dt_gettantou.Rows[0]["rogo"].ToString().TrimEnd() == "2")
                            //{
                            //    flagrogo = "2";
                            //}
                            //else if (dt_gettantou.Rows[0]["rogo"].ToString().TrimEnd() == "3")
                            //{
                            //    flagrogo = "3";
                            //}
                            //else if (dt_gettantou.Rows[0]["rogo"].ToString().TrimEnd() == "4")
                            //{
                            //    flagrogo = "4";
                            //}
                            //else if (dt_gettantou.Rows[0]["rogo"].ToString().TrimEnd() == "5")
                            //{
                            //    flagrogo = "5";
                            //}
                            #endregion
                        }
                        string sqlkyoten = "";
                        sqlkyoten = "select";
                        if (flagrogo == "1")
                        {
                            sqlkyoten += " sIMAGE1 as sIMAGE";
                        }
                        else if (flagrogo == "2")
                        {
                            sqlkyoten += " sIMAGE2 as sIMAGE";
                        }
                        else if (flagrogo == "3")
                        {
                            sqlkyoten += " sIMAGE3 as sIMAGE";
                        }
                        else if (flagrogo == "4")
                        {
                            sqlkyoten += " sIMAGE4 as sIMAGE";
                        }
                        else if (flagrogo == "5")
                        {
                            sqlkyoten += " sIMAGE5 as sIMAGE";
                        }
                        else
                        {
                            sqlkyoten += " '' as sIMAGE";
                        }
                        sqlkyoten += " from m_j_info where cCO ='" + cKyoten + "'";

                        MySqlCommand cmd = new MySqlCommand(sqlkyoten, con);
                        DataTable dt_mj_info = new DataTable();
                        MySqlDataAdapter da_mj_info = new MySqlDataAdapter(cmd);
                        da_mj_info.Fill(dt_mj_info);
                        da_mj_info.Dispose();
                        if (dt_mj_info.Rows.Count > 0)
                        {
                            if (dt_mj_info.Rows[0]["sIMAGE"].ToString() == "")
                            {
                                //Response.Write("その拠点にはロゴが登録されていません。");
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowAnkenChangeMessage",
                            "ShowRogoMessage('その拠点にはロゴが登録されていません。','" + btnOK.ClientID + "');", true);

                                return;
                            }
                        }

                        uriage rpt = new uriage();
                        rpt.cURIAGE = uriagecode;
                        rpt.loginId =mail;
                        rpt.ckyoten = cKyoten;
                        rpt.frogoimage = flagrogo;
                        Login_Tantou();
                        rpt.logintantou = lblLoginUserCode.Text;
                        rpt.Run(false);

                        #region 20220131 MiMi Added Start
                        flagbikou = dt_gettantou.Rows[0]["seikyubikou"].ToString().TrimEnd();
                        seikyuusho rpt_seikyusho = new seikyuusho();
                        rpt_seikyusho.stitle = "御請求書";
                        rpt_seikyusho.fSEIKYUU = true;
                        rpt_seikyusho.cURIAGE = uriagecode;
                        rpt_seikyusho.dINVOICE = dUriage;
                        rpt_seikyusho.dMITUMORISAKUSEI = dUriage;
                        if (dt_gettantou.Rows[0]["kingaku"].ToString().TrimEnd() == "1")
                        {
                            rpt_seikyusho.fhiduke = true;
                        }
                        else
                        {
                            rpt_seikyusho.fhiduke = false;
                        }
                        rpt_seikyusho.fbikou = flagbikou;
                        rpt_seikyusho.loginId = mail;
                        rpt_seikyusho.ckyoten = cKyoten;
                        rpt_seikyusho.frogoimage = flagrogo;
                        rpt_seikyusho.bikou = sBikou;
                        rpt_seikyusho.Run(false);

                        for (int i = 0; i < rpt_seikyusho.Document.Pages.Count; i++)
                        {
                            rpt.Document.Pages.Add(rpt_seikyusho.Document.Pages[i].Clone());
                        }
                        #endregion

                        String filename = "uriage.pdf";
                        System.IO.MemoryStream ms = new System.IO.MemoryStream();
                        GrapeCity.ActiveReports.Export.Pdf.Section.PdfExport pdfExport1 = new GrapeCity.ActiveReports.Export.Pdf.Section.PdfExport();
                        pdfExport1.Export(rpt.Document, ms);
                        ms.Position = 0;
                        Session["PDFMemoryStream"] = ms;
                        Session["UriagePDF"] = "true";
                        if (!String.IsNullOrEmpty(Request.QueryString["id"]))
                        {
                            id = Request.QueryString["id"];
                            Response.Write("<script language='javascript'>window.open('JCPDFViewer.aspx?id=" + id + "', '_blank');</script>");
                        }
                        else
                        {
                            Response.Redirect("JC01Login.aspx");
                        }
                        con.Close();
                    }
                }
                else if (dt_gettantou.Rows[0]["Shoshiki"].ToString() == "1")  //20220126 MiMi Added
                {
                    PrintSeikyuShoPDF(sender, e);
                }
            }
            else
            {
                //PrintPDF(sender, e);
            }
        }

        

        #region BT_HyojikoumokuSettei
        protected void BT_HyojikoumokuSettei_Click(object sender, EventArgs e)
        {
            SessionUtility.SetSession("HOME", "Master");
            Session["HyoujiID"] = "uriage";
            ifpnlHyoujiSetPopUp.Attributes.Add("class", "HyoujiSettingIframe uriageiframeStyle");
            ifpnlHyoujiSetPopUp.Style["width"] = "950px";
            ifpnlHyoujiSetPopUp.Style["height"] = "650px";
            String id = Request.QueryString["id"];
            ifpnlHyoujiSetPopUp.Src = "JC08HyoujiSetting.aspx?id=" + id;
            mpeHyoujiSetPopUp.Show();
            updHyoujiSet.Update();
            ScriptManager.RegisterStartupScript(this, GetType(), "CloseModal", "closeLoadingModal();", true);
        }
        #endregion

        #region btnHyoujiSettingSave
        protected void btnHyoujiSettingSave_Click(object sender, EventArgs e)
        {
            ifpnlHyoujiSetPopUp.Src = "";
            mpeHyoujiSetPopUp.Hide();
            updHyoujiSet.Update();
            DG_Hyouji_Dt();
            UriageColumn();
            //Page.Response.Redirect(Page.Request.Url.ToString(), true);
        }
        #endregion

        #region btnHyoujiSettingClose
        protected void btnHyoujiSettingClose_Click(object sender, EventArgs e)
        {
            ifpnlHyoujiSetPopUp.Src = "";
            mpeHyoujiSetPopUp.Hide();
            updHyoujiSet.Update();
        }
        
        #endregion

        #region LKB_UriageEdit_Clicked
        protected void LKB_UriageEdit_Clicked(Object sender, EventArgs e)
        {
           // Session["fcopy"] = "false";
            var lbk_edit = sender as LinkButton;
            GridViewRow gvRow = (GridViewRow)lbk_edit.NamingContainer;
            string uriageCode = (GV_Uriage.Rows[gvRow.RowIndex].FindControl("LKB_cUriage") as LinkButton).Text;
            Session["cUriage"] = uriageCode;
            if (Session["cUriage"] != null)
            {
                JC99NavBar.insatsusettei = false;
                if (!String.IsNullOrEmpty(Request.QueryString["id"]))
                {
                    String id = Request.QueryString["id"];
                    Response.Redirect("JC27UriageTouroku.aspx?id=" + id);
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

        #region BT_UriageHyouji_Click
        protected void BT_UriageHyouji_Click(object sender,EventArgs e)
        {
            UriageCode = TB_UriageCode.Text;
            MitsumoriCode = TB_MitsumoriCode.Text;
            UriageKenmei = TB_UriageKenmei.Text;
            //HakkouStartDate = TB_HakkouStartDate.Text;
            //HakkouEndDate = TB_HakkouEndDate.Text;
            //UriageStartDate = TB_UriageStartDate.Text;
            //UriageEndDate = TB_UriageEndDate.Text;
            HakkouStartDate = LB_HakkouStart.Text;            HakkouEndDate = LB_HakkouEnd.Text;            UriageStartDate = LB_UriageStart.Text;            UriageEndDate = LB_UriageEnd.Text;
            UriageJoutai = DDL_UraigeJoutai.SelectedItem.ToString();
            Tokuisaki = TB_Tokuisaki.Text;
            Seikyusaki = TB_Seikyusaki.Text;
            Shouhinmei = TB_Shouhinmei.Text;
            UriageShanaiMemo = TB_UriageShanaiMemo.Text;
            if (BT_Tantousya.Text == "選択なし")            {                DDL_Tantousya.Items.Clear();            }
            search();
            JoukenReload();
           
        }
        #endregion

        #region search()
        public void search()
        {
            string searchdata = "";
            GV_Uriage.PageIndex = 0;
            GridView1.PageIndex = 0;
            DDL_Jyouken.Items.Clear();

            if (!String.IsNullOrEmpty(UriageCode))
            {
                searchdata += " and ru.cURIAGE='" + UriageCode + "'";
                ListItem li = new ListItem();
                li.Value = "売上コード";
                li.Text = UriageCode;
                DDL_Jyouken.Items.Add(li);
            }
            if (!String.IsNullOrEmpty(MitsumoriCode))
            {
                searchdata += " and rum.cMITUMORI='" + MitsumoriCode + "'";
                ListItem li = new ListItem();
                li.Value = "見積コード";
                li.Text = MitsumoriCode;
                DDL_Jyouken.Items.Add(li);
            }

            if (!String.IsNullOrEmpty(UriageKenmei))
            {
                searchdata += " and ru.snouhin like '%" + UriageKenmei + "%'";
                ListItem li = new ListItem();
                li.Value = "売上件名";
                li.Text = UriageKenmei;
                DDL_Jyouken.Items.Add(li);
            }

            if (!String.IsNullOrEmpty(HakkouStartDate) && !String.IsNullOrEmpty(HakkouEndDate))
            {
                searchdata += " and date_format(ru.dDate,'%Y/%m/%d') BETWEEN '" + HakkouStartDate + "' AND '" + HakkouEndDate + "'";
                ListItem li = new ListItem();
                li.Value = "発行日Start";
                li.Text = HakkouStartDate;
                DDL_Jyouken.Items.Add(li);

                li = new ListItem();
                li.Value = "発行日End";
                li.Text = HakkouEndDate;
                DDL_Jyouken.Items.Add(li);
            }
            else
            {
                if (!String.IsNullOrEmpty(HakkouStartDate))
                {
                    searchdata += " and date_format(ru.dDate,'%Y/%m/%d')>='" + HakkouStartDate + "'";
                    ListItem li = new ListItem();
                    li.Value = "発行日Start";
                    li.Text = HakkouStartDate;
                    DDL_Jyouken.Items.Add(li);
                }
                else if (!String.IsNullOrEmpty(HakkouEndDate))
                {
                    searchdata += " and date_format(ru.dDate,'%Y/%m/%d')<='" + HakkouEndDate + "'";
                    ListItem li = new ListItem();
                    li.Value = "発行日End";
                    li.Text = HakkouEndDate;
                    DDL_Jyouken.Items.Add(li);
                }
            }

            if (!String.IsNullOrEmpty(UriageStartDate) && !String.IsNullOrEmpty(UriageEndDate))
            {
                searchdata += " and date_format(ru.dURIAGE,'%Y/%m/%d') BETWEEN '" + UriageStartDate + "' AND '" + UriageEndDate + "'";
                ListItem li = new ListItem();
                li.Value = "売上日Start";
                li.Text = UriageStartDate;
                DDL_Jyouken.Items.Add(li);

                li = new ListItem();
                li.Value = "売上日End";
                li.Text = UriageEndDate;
                DDL_Jyouken.Items.Add(li);
            }
            else
            {
                if (!String.IsNullOrEmpty(UriageStartDate))
                {
                    searchdata += " and date_format(ru.dURIAGE,'%Y/%m/%d')>='" + UriageStartDate + "'";
                    ListItem li = new ListItem();
                    li.Value = "売上日Start";
                    li.Text = UriageStartDate;
                    DDL_Jyouken.Items.Add(li);
                }
                else if (!String.IsNullOrEmpty(UriageEndDate))
                {
                    searchdata += " and date_format(ru.dURIAGE,'%Y/%m/%d')<='" + UriageEndDate + "'";
                    ListItem li = new ListItem();
                    li.Value = "売上日End";
                    li.Text = UriageEndDate;
                    DDL_Jyouken.Items.Add(li);
                }
            }

            if (!String.IsNullOrEmpty(UriageJoutai))
            {
                string jyoutai = "";

                #region toDelete
                //if (UriageJoutai == "作成中")
                //{
                //    jyoutai = "00";
                //}
                //else if (UriageJoutai == "作成済")
                //{
                //    jyoutai = "01";
                //}
                //else if (UriageJoutai == "請求締処理")
                //{
                //    jyoutai = "02";
                //}
                //else if (UriageJoutai == "入金")
                //{
                //    jyoutai = "03";
                //}
                //else if (UriageJoutai == "売掛締処理")
                //{
                //    jyoutai = "04";
                //}
                #endregion

                jyoutai = DDL_UraigeJoutai.SelectedValue.ToString();
                searchdata += " and ru.cJYOTAI_Uriage='" + jyoutai + "'";

                ListItem li = new ListItem();
                li.Value = "売上状態";
                li.Text = UriageJoutai;
                DDL_Jyouken.Items.Add(li);

            }

            if (!String.IsNullOrEmpty(Tokuisaki))
            {
                searchdata += " and ru.sTOKUISAKI like '%" + Tokuisaki + "%'";
                ListItem li = new ListItem();
                li.Value = "得意先";
                li.Text = Tokuisaki;
                DDL_Jyouken.Items.Add(li);
            }

            if (!String.IsNullOrEmpty(Seikyusaki))
            {
                searchdata += " and ru.sSEIKYUSAKI like '%" + Seikyusaki + "%'";
                ListItem li = new ListItem();
                li.Value = "請求先";
                li.Text = Seikyusaki;
                DDL_Jyouken.Items.Add(li);
            }

            if (!String.IsNullOrEmpty(Shouhinmei))
            {
                searchdata += " and rm.sSYOUHIN_R like '%" + Shouhinmei + "%'";
                ListItem li = new ListItem();
                li.Value = "商品名";
                li.Text = Shouhinmei;
                DDL_Jyouken.Items.Add(li);
            }

            if (!String.IsNullOrEmpty(UriageShanaiMemo))
            {
                searchdata += " and ru.sMemo like '%" + UriageShanaiMemo + "%'";
                ListItem li = new ListItem();
                li.Value = "売上社内メモ";
                li.Text = UriageShanaiMemo;
                DDL_Jyouken.Items.Add(li);
            }

            if (DDL_Tantousya.Items.Count > 0)  //自社担当者
            {
                searchdata += " and ";
                for (int i = 0; i < DDL_Tantousya.Items.Count; i++)
                {
                    String csakuseisha = DDL_Tantousya.Items[i].Value;
                    if (i == 0)
                    {
                        searchdata += " (ru.cSAKUSEISYA like '%" + csakuseisha + "%'";
                    }
                    else
                    {
                        searchdata += " OR ru.cSAKUSEISYA like '%" + csakuseisha + "%'";
                    }
                    ListItem li = new ListItem();
                    li.Value = "自社担当者" + csakuseisha;
                    li.Text = DDL_Tantousya.Items[i].Text;
                    DDL_Jyouken.Items.Add(li);
                }
                searchdata += ") ";
            }

            String id = Request.QueryString["id"];
            DataTable dt_db = JC01Login_Class.Get_DB(id);
            String db = dt_db.Rows[0]["db"].ToString();
            String mail = dt_db.Rows[0]["mail"].ToString();
            ConstantVal.DB_NAME = db;

            JC34UriageList_Class JC28UriageList = new JC34UriageList_Class();
            dt_GV_Uriage = JC28UriageList.UrigaeList(searchdata);
            GV_Uriage.DataSource = dt_GV_Uriage;
            GV_Uriage.DataBind();
            ViewState["GV_Uriage"] = dt_GV_Uriage;

            //dt_gv1 = JC28UriageList.UrigaeList1(searchdata);
            GridView1.DataSource = dt_GV_Uriage;
            GridView1.DataBind();
            //ViewState["gv1_Uriage"] = dt_gv1;

            RowCount();
        }
        #endregion

        #region RowCount()
        public void RowCount()
        {
            if (GV_Uriage.Rows.Count > 0)
            {
                //GridViewRow gvrPager = GV_Uriage.BottomPagerRow;
                GV_Uriage.PagerSettings.FirstPageText = "1";
                GV_Uriage.PagerSettings.LastPageText = "" + GV_Uriage.PageCount + "";
                GV_Uriage.DataBind();

                GridView1.PagerSettings.FirstPageText = "1";
                GridView1.PagerSettings.LastPageText = "" + GridView1.PageCount + "";
                GridView1.DataBind();

                //LB_Total.Text = dt_GV_Uriage.Rows.Count.ToString();
                //totalrowcount = dt_GV_Uriage.Rows.Count.ToString();

                if (dt_GV_Uriage.Rows.Count < GV_Uriage.PageSize)
                {
                    LB_Show_Data.Text = "1-" + (dt_GV_Uriage.Rows.Count).ToString() + "/" + dt_GV_Uriage.Rows.Count.ToString();
                }
                else
                {
                    LB_Show_Data.Text = "1-" + (GV_Uriage.PageSize).ToString() + "/" + dt_GV_Uriage.Rows.Count.ToString();
                }
            }
            else
            {
                LB_Show_Data.Text = "0-0/0";
                //LB_Total.Text = "0";
            }
            updhyoujisuryou.Update();
        }
        #endregion

        #region 売上削除
        protected void LKB_UriageDelete_Clicked(Object sender, EventArgs e)
        {
            LinkButton lkb_delete = sender as LinkButton;
            GridViewRow gvRow = (GridViewRow)lkb_delete.NamingContainer;
            Session["del_uriagecode"] = (GV_Uriage.Rows[gvRow.RowIndex].FindControl("LKB_cUriage") as LinkButton).Text;
            Button ok = GV_Uriage.Rows[gvRow.RowIndex].FindControl("BT_DeleteOk") as Button;
            Button cancel = GV_Uriage.Rows[gvRow.RowIndex].FindControl("BT_DeleteCancel") as Button;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowAnkenChangeMessage",
                     "DeleteConfirmBox('削除してもよろしいでしょうか？','" + ok.ClientID + "','" + cancel.ClientID + "');", true);
            GV_Uriage.DataBind();

        }
        #endregion

        #region BT_Clear_Click
        protected void BT_Clear_Click(object sender, EventArgs e)
        {
            TB_UriageCode.Text = string.Empty;
            TB_MitsumoriCode.Text = string.Empty;
            TB_UriageKenmei.Text = string.Empty;
            //TB_HakkouStartDate.Text = string.Empty;
            //TB_HakkouEndDate.Text = string.Empty;
            //TB_UriageStartDate.Text = string.Empty;
            //TB_UriageEndDate.Text = string.Empty;
            BT_dHakkouStartCross_Click(sender, e);            BT_dHakkouEndCross_Click(sender, e);            BT_dUriageStartCross_Click(sender, e);            BT_dUriageEndCross_Click(sender, e);

            DDL_UraigeJoutai.SelectedIndex = 0;
            TB_Tokuisaki.Text = string.Empty;
            TB_Seikyusaki.Text = string.Empty;
            TB_Shouhinmei.Text = string.Empty;
            TB_UriageShanaiMemo.Text = string.Empty;

            //TC_Jouken.Controls.Clear();
            //DDL_Tantousya.Items.Clear();
            //DDL_Jyouken.Items.Clear();
            BT_Tantousya.Text = "選択なし";
            BT_Tantousya.CssClass = "JC34JishaTantou";
            updTantousha.Update();
            
            // BindGrid();
        }
        #endregion

        #region LKB_mitsumoricode_Click
        protected void LKB_mitsumoricode_Click(Object sender, EventArgs e)        {            var lk_mitsumoricode = (LinkButton)sender;            Session["cMitumori"] = lk_mitsumoricode.Text;            if (Session["cMitumori"] != null)            {                JC99NavBar.insatsusettei = false;                if (!String.IsNullOrEmpty(Request.QueryString["id"]))
                {
                    String id = Request.QueryString["id"];
                    Response.Redirect("JC10MitsumoriTouroku.aspx?id=" + id);
                }
                else
                {
                    Response.Redirect("JC01Login.aspx");
                }            }            else            {                Response.Redirect("JC01Login.aspx");            }        }
        #endregion

        #region BT_DeleteOk_Click
        protected void BT_DeleteOk_Click(object sender, EventArgs e)
        {

            //var bt_deleteok = sender as Button;
            //GridViewRow gvRow = (GridViewRow)bt_deleteok.NamingContainer;
            //string uriageCode = (GV_Uriage.Rows[gvRow.RowIndex].FindControl("LKB_cUriage") as LinkButton).Text;

            String id = Request.QueryString["id"];
            DataTable dt_db = JC01Login_Class.Get_DB(id);
            String db = dt_db.Rows[0]["db"].ToString();
            String mail = dt_db.Rows[0]["mail"].ToString();
            ConstantVal.DB_NAME = db;

            JC27UriageTouroku_Class delete_class = new JC27UriageTouroku_Class();
            if (delete_class.Delete_Uriage(Session["del_uriagecode"].ToString()))
            {
                GV_Uriage.PageIndex = 0;
                GridView1.PageIndex = 0;
                if (DDL_Show_Count.SelectedIndex == 0)
                {
                    GV_Uriage.PageSize = 20;
                    GridView1.PageSize = 20;
                }
                else if (DDL_Show_Count.SelectedIndex == 1)
                {
                    GV_Uriage.PageSize = 30;
                    GridView1.PageSize = 30;
                }
                else if (DDL_Show_Count.SelectedIndex == 2)
                {
                    GV_Uriage.PageSize = 50;
                    GridView1.PageSize = 50;
                }

                ConstantVal.DB_NAME = db;
                JC34UriageList_Class JC28UriageList = new JC34UriageList_Class();
                dt_GV_Uriage = JC28UriageList.UrigaeList("");
                GV_Uriage.DataSource = dt_GV_Uriage;
                GV_Uriage.DataBind();

                //dt_gv1 = JC28UriageList.UrigaeList1("");
                GridView1.DataSource = dt_GV_Uriage;
                GridView1.DataBind();


                //GV_Uriage.PagerSettings.FirstPageText = "1";
                //GV_Uriage.PagerSettings.LastPageText = "" + GV_Uriage.PageCount + "";
                //GV_Uriage.DataBind();
                ////LB_Total.Text = dt_GV_Uriage.Rows.Count.ToString();

                //if (dt_GV_Uriage.Rows.Count < GV_Uriage.PageSize)
                //{
                //    LB_Show_Data.Text = "1-" + (dt_GV_Uriage.Rows.Count).ToString()+"/"+ dt_GV_Uriage.Rows.Count.ToString();
                //}
                //else
                //{
                //    LB_Show_Data.Text = "1-" + (GV_Uriage.PageSize).ToString() + "/" + dt_GV_Uriage.Rows.Count.ToString();
                //}
                //updhyoujisuryou.Update();
                RowCount();
                ViewState["GV_Uriage"] = dt_GV_Uriage;
                //ViewState["gv1_Uriage"] = dt_gv1;
            }
        }
        #endregion

        #region PrintSeikyuShoPDF
        private void PrintSeikyuShoPDF(object sender, EventArgs e)  //20220126 MiMi Added
        {
            try
            {
                string flagrogo = "";
                String flagbikou = "";
                String cKyoten = "";
                String sBikou = "";
                String dUriage = "";

                DataTable dt_r_uriage = new DataTable();
                String id = Request.QueryString["id"];
                DataTable dt_db = JC01Login_Class.Get_DB(id);
                String db = dt_db.Rows[0]["db"].ToString();
                String mail = dt_db.Rows[0]["mail"].ToString();
                ConstantVal.DB_NAME = db;

                JC27UriageTouroku_Class r_uriage_data = new JC27UriageTouroku_Class();
                r_uriage_data.cURIAGE = uriagecode;
                ConstantVal.DB_NAME = db;
                dt_r_uriage = r_uriage_data.UriageData();
                cKyoten = dt_r_uriage.Rows[0]["cCO"].ToString();
                sBikou= dt_r_uriage.Rows[0]["sbikou"].ToString();
                dUriage= dt_r_uriage.Rows[0]["dURIAGE"].ToString();

                ConstantVal.DB_NAME = db;
                JC_ClientConnecction_Class jc = new JC_ClientConnecction_Class();
                jc.loginId = mail;

                ConstantVal.DB_NAME = db;
                DateTime datenow = jc.GetCurrentDate();
                String fileName = "～兼請求書～" + uriagecode+ "_" + datenow.ToString("yyyyMMdd");

                con = jc.GetConnection();
                con.Open();

                ConstantVal.DB_NAME = db;
                JC27UriageTouroku_Class settei = new JC27UriageTouroku_Class();
                Login_Tantou();
                settei.cTANTOUSHA = lblLoginUserCode.Text;
                DataTable dt_gettantou = settei.GetTantou();

                if (dt_gettantou.Rows.Count > 0)
                {
                    flagrogo = dt_gettantou.Rows[0]["rogo"].ToString().TrimEnd();
                    flagbikou = dt_gettantou.Rows[0]["seikyubikou"].ToString().TrimEnd();
                    #region toDelete
                    //if (dt_gettantou.Rows[0]["rogo"].ToString().TrimEnd() == "1")
                    //{
                    //    flagrogo = "1";
                    //}
                    //else if (dt_gettantou.Rows[0]["rogo"].ToString().TrimEnd() == "2")
                    //{
                    //    flagrogo = "2";
                    //}
                    //else if (dt_gettantou.Rows[0]["rogo"].ToString().TrimEnd() == "3")
                    //{
                    //    flagrogo = "3";
                    //}
                    //else if (dt_gettantou.Rows[0]["rogo"].ToString().TrimEnd() == "4")
                    //{
                    //    flagrogo = "4";
                    //}
                    //else if (dt_gettantou.Rows[0]["rogo"].ToString().TrimEnd() == "5")
                    //{
                    //    flagrogo = "5";
                    //}


                    //if (dt_gettantou.Rows[0]["seikyubikou"].ToString().TrimEnd() == "1")
                    //{
                    //    flagbikou = "1";
                    //}
                    //else if (dt_gettantou.Rows[0]["seikyubikou"].ToString().TrimEnd() == "2")
                    //{
                    //    flagbikou = "2";
                    //}
                    //else if (dt_gettantou.Rows[0]["seikyubikou"].ToString().TrimEnd() == "3")
                    //{
                    //    flagbikou = "3";
                    //}
                    //else if (dt_gettantou.Rows[0]["seikyubikou"].ToString().TrimEnd() == "4")
                    //{
                    //    flagbikou = "4";
                    //}
                    //else if (dt_gettantou.Rows[0]["seikyubikou"].ToString().TrimEnd() == "5")
                    //{
                    //    flagbikou = "5";
                    //}
                    #endregion
                }

                seikyuusho rpt = new seikyuusho();
                rpt.stitle = "兼請求書";
                rpt.fSEIKYUU = true;
                rpt.cURIAGE = uriagecode;
                rpt.dINVOICE = dUriage;
                rpt.dMITUMORISAKUSEI = dUriage;
                if (dt_gettantou.Rows[0]["kingaku"].ToString().TrimEnd()=="1")
                {
                    rpt.fhiduke = true;
                }
                else
                {
                    rpt.fhiduke = false;
                }
                rpt.fbikou = flagbikou;
                rpt.loginId = mail;
                rpt.ckyoten = cKyoten;
                rpt.frogoimage = flagrogo;
                rpt.bikou = sBikou;
                Login_Tantou();
                rpt.Run();
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                GrapeCity.ActiveReports.Export.Pdf.Section.PdfExport pdfExport1 = new GrapeCity.ActiveReports.Export.Pdf.Section.PdfExport();
                pdfExport1.Export(rpt.Document, ms);
                ms.Position = 0;
                Session["PDFMemoryStream"] = ms;
                Session["PDFFileName"] = fileName;
                Session["UriagePDF"] = "false";
                if (!String.IsNullOrEmpty(Request.QueryString["id"]))
                {
                    id = Request.QueryString["id"];
                    Response.Write("<script language='javascript'>window.open('JCPDFViewer.aspx?id=" + id + "', '_blank');</script>");
                }
                else
                {
                    Response.Redirect("JC01Login.aspx");
                }

                con.Close();
            }
            catch
            {
                Response.Redirect("JC01Login.aspx");
            }
        }
        #endregion

        protected void BT_UriageSyosai_Click(object sender, EventArgs e)
        {
            if (divshowallcomponet.Visible == true)
            {
                BT_UriageSyosai.Style.Add("background-image", "url('../Img/expand-more-1782315-1514165.png')");
                divshowallcomponet.Visible = false;
            }
            else
            {
                BT_UriageSyosai.Style.Add("background-image", "url('../Img/expand-less-1781206-1518580.png')");
                divshowallcomponet.Visible = true;
            }
        }

        #region GV_Uriage_Sorting
        protected void GV_Uriage_Sorting(object sender, GridViewSortEventArgs e)
        {
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

        #region SortGridView
        private void SortGridView(string sortExpression, string direction)
        {
            //dt.Clear();
            dt = (DataTable)ViewState["GV_Uriage"];
            dt1 = dt.Copy();
            //if (dt.Rows[0][1].ToString() == "")
            //{
            //    dt.Rows.Remove(dt.Rows[0]);
            //}
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
                ViewState["GV_Uriage"] = dt1;
                GV_Uriage.DataSource = dt1;
                GV_Uriage.DataBind();
            }
        }


        #endregion

        #region GridViewSortDirection        public SortDirection GridViewSortDirection        {            get            {                if (ViewState["sortDirection"] == null)                    ViewState["sortDirection"] = SortDirection.Ascending;                return (SortDirection)ViewState["sortDirection"];            }            set            {                ViewState["sortDirection"] = value;            }        }        #endregion

        #region GV_Uriage_RowCreated
        protected void GV_Uriage_RowCreated(object sender, GridViewRowEventArgs e)
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
                                img.ID = sortLinkButton.CommandArgument;
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

        #region BT_HakkouStartDate_Click        protected void BT_HakkouStartDate_Click(object sender, EventArgs e)        {
            //ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Name: hello');", true);
            SessionUtility.SetSession("HOME", "Master");
            String id = Request.QueryString["id"];            ifdatePopup.Src = "JCHidukeSelect.aspx?id=" + id;            mpedatePopup.Show();            ViewState["DATETIME"] = BT_HakkouStartDate.ID;            if (!String.IsNullOrEmpty(LB_HakkouStart.Text))            {                DateTime dt = DateTime.Parse(LB_HakkouStart.Text);                Session["CALENDARDATE"] = dt.ToString("yyyy/MM/dd");                Session["YEAR"] = dt.Year;            }

            // 設定したラベルをクリック時、時間設定ポップアップ画面を表示する
            LB_HakkouStart.Attributes.Add("onClick", "BtnClick('MainContent_BT_HakkouStartDate')");            upddatePopup.Update();        }
        #endregion
        #region BT_dHakkouStartCross_Click        protected void BT_dHakkouStartCross_Click(object sender, EventArgs e)        {            LB_HakkouStart.Text = "";            BT_HakkouStartDate.Style["display"] = "block";            divHakkouStartDate.Style["display"] = "none";            updHakkouStartDate.Update();        }
        #endregion
        #region BT_HakkouEndDate_Click        protected void BT_HakkouEndDate_Click(object sender, EventArgs e)        {            SessionUtility.SetSession("HOME", "Master");
            String id = Request.QueryString["id"];            ifdatePopup.Src = "JCHidukeSelect.aspx?id=" + id;            mpedatePopup.Show();            ViewState["DATETIME"] = BT_HakkouEndDate.ID;            if (!String.IsNullOrEmpty(LB_HakkouEnd.Text))            {                DateTime dt = DateTime.Parse(LB_HakkouEnd.Text);                Session["CALENDARDATE"] = dt.ToString("yyyy/MM/dd");                Session["YEAR"] = dt.Year;            }

            // 設定したラベルをクリック時、時間設定ポップアップ画面を表示する
            LB_HakkouEnd.Attributes.Add("onClick", "BtnClick('MainContent_BT_HakkouEndDate')");            upddatePopup.Update();        }
        #endregion
        #region BT_dHakkouEndCross_Click        protected void BT_dHakkouEndCross_Click(object sender, EventArgs e)        {            LB_HakkouEnd.Text = "";            BT_HakkouEndDate.Style["display"] = "block";            divHakkouEndDate.Style["display"] = "none";            updHakkouEndDate.Update();        }
        #endregion

        #region BT_UriageStartDate_Click        protected void BT_UriageStartDate_Click(object sender, EventArgs e)        {            SessionUtility.SetSession("HOME", "Master");
            String id = Request.QueryString["id"];            ifdatePopup.Src = "JCHidukeSelect.aspx?id=" + id;            mpedatePopup.Show();            ViewState["DATETIME"] = BT_UriageStartDate.ID;            if (!String.IsNullOrEmpty(LB_UriageStart.Text))            {                DateTime dt = DateTime.Parse(LB_UriageStart.Text);                Session["CALENDARDATE"] = dt.ToString("yyyy/MM/dd");                Session["YEAR"] = dt.Year;            }

            // 設定したラベルをクリック時、時間設定ポップアップ画面を表示する
            LB_UriageStart.Attributes.Add("onClick", "BtnClick('MainContent_BT_UriageStartDate')");            upddatePopup.Update();        }
        
        #endregion
        #region BT_dUriageStartCross_Click        protected void BT_dUriageStartCross_Click(object sender, EventArgs e)        {            LB_UriageStart.Text = "";            BT_UriageStartDate.Style["display"] = "block";            divUriageStartDate.Style["display"] = "none";            updUriageStartDate.Update();        }
        #endregion

        #region BT_UriageEndDate_Click        protected void BT_UriageEndDate_Click(object sender, EventArgs e)        {            SessionUtility.SetSession("HOME", "Master");
            String id = Request.QueryString["id"];            ifdatePopup.Src = "JCHidukeSelect.aspx?id=" + id;            mpedatePopup.Show();            ViewState["DATETIME"] = BT_UriageEndDate.ID;            if (!String.IsNullOrEmpty(LB_UriageEnd.Text))            {                DateTime dt = DateTime.Parse(LB_UriageEnd.Text);                Session["CALENDARDATE"] = dt.ToString("yyyy/MM/dd");                Session["YEAR"] = dt.Year;            }

            // 設定したラベルをクリック時、時間設定ポップアップ画面を表示する
            LB_UriageEnd.Attributes.Add("onClick", "BtnClick('MainContent_BT_UriageEndDate')");            upddatePopup.Update();        }
        #endregion
        #region BT_dUriageEndCross_Click        protected void BT_dUriageEndCross_Click(object sender, EventArgs e)        {            LB_UriageEnd.Text = "";            BT_UriageEndDate.Style["display"] = "block";            divUriageEndDate.Style["display"] = "none";            updUriageEndDate.Update();        }
        #endregion

        #region "発行日データバインディング処理"        protected void HakkouStartDateDataBind(string strCalendarDateTime, string strImgbtnID)        {            DateTime dtDeliveryDate = DateTime.Parse(strCalendarDateTime);            LB_HakkouStart.Text = dtDeliveryDate.ToString("yyyy/MM/dd");            BT_HakkouStartDate.Style["display"] = "none";            divHakkouStartDate.Style["display"] = "block";            updHakkouStartDate.Update();        }
        #endregion
        #region "発行日データバインディング処理"        protected void HakkouEndDateDataBind(string strCalendarDateTime, string strImgbtnID)        {            DateTime dtDeliveryDate = DateTime.Parse(strCalendarDateTime);            LB_HakkouEnd.Text = dtDeliveryDate.ToString("yyyy/MM/dd");            BT_HakkouEndDate.Style["display"] = "none";            divHakkouEndDate.Style["display"] = "block";            updHakkouEndDate.Update();        }
        #endregion
        #region "発行日データバインディング処理"        protected void UriageStartDateDataBind(string strCalendarDateTime, string strImgbtnID)        {            DateTime dtDeliveryDate = DateTime.Parse(strCalendarDateTime);            LB_UriageStart.Text = dtDeliveryDate.ToString("yyyy/MM/dd");            BT_UriageStartDate.Style["display"] = "none";            divUriageStartDate.Style["display"] = "block";            updUriageStartDate.Update();        }
        
        #endregion

        #region "発行日データバインディング処理"        protected void UriageEndDateDataBind(string strCalendarDateTime, string strImgbtnID)        {            DateTime dtDeliveryDate = DateTime.Parse(strCalendarDateTime);            LB_UriageEnd.Text = dtDeliveryDate.ToString("yyyy/MM/dd");            BT_UriageEndDate.Style["display"] = "none";            divUriageEndDate.Style["display"] = "block";            updUriageEndDate.Update();        }
        #endregion

        protected void GV_UriagePg_Original_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Visible = false;
            }
        }
    }


}