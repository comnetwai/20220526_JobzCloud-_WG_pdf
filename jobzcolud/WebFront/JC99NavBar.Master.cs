using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;
using Service;

namespace jobzcolud.WebFront
{
    public partial class JC99NavBar : System.Web.UI.MasterPage
    {
        public static bool insatsusettei;
        public string LoginName { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!this.IsPostBack)
            {
                if (!String.IsNullOrEmpty(Request.QueryString["id"]))
                {
                    JC99NavBar_Class Jc99 = new JC99NavBar_Class();
                    String id = Request.QueryString["id"];
                    DataTable dt = JC01Login_Class.Get_DB(id);
                    String db = dt.Rows[0]["db"].ToString();
                    String mail = dt.Rows[0]["mail"].ToString();
                    Jc99.loginId = mail;
                    ConstantVal.DB_NAME = db;
                    Jc99.FindLoginName();
                    LoginName = Jc99.LoginName;
                    string sPath = Request.Url.AbsolutePath;
                    System.IO.FileInfo oInfo = new System.IO.FileInfo(sPath);
                    string sRet = oInfo.Name;
                    setNav2(sRet);
                    navbardrop2.InnerText = LoginName;  //20211025 MiMi Added
                    updDropDown.Update();
                }
                else
                {
                    Response.Redirect("JC01Login.aspx");
                }
             

            }
            Session.Timeout = 480;

        }

        protected void lnkbtnHome_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(Request.QueryString["id"]))
            {
                String id = Request.QueryString["id"];
                Response.Redirect("JC07Home.aspx?id=" + id,false);
            }
            else
            {
                Response.Redirect("JC01Login.aspx");
            }

        }
        protected void lnkBtnBukkenNew_Click(object sender, EventArgs e)
        {
            SessionUtility.SetSession("cBukken", null);
            if (!String.IsNullOrEmpty(Request.QueryString["id"]))
            {
                String id = Request.QueryString["id"];
                Response.Redirect("JC09BukkenSyousai.aspx?id=" + id, false);
            }
            else
            {
                Response.Redirect("JC01Login.aspx");
            }
        }

        protected void lnkBtnBukkenList_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(Request.QueryString["id"]))
            {
                String id = Request.QueryString["id"];
                Response.Redirect("JC30BukkenList.aspx?id=" + id, false);
            }
            else
            {
                Response.Redirect("JC01Login.aspx");
            }
        }

        //protected void lnkBtnMitsumoriNew_Click(object sender, EventArgs e)
        //{
        //    SessionUtility.SetSession("cMitumori", null);
        //    Response.Redirect("JC10MitsumoriTouroku.aspx",false);           
        //}

        //protected void lnkBtnTaMitsuCopy_Click(object sender, EventArgs e)
        //{
        //    SessionUtility.SetSession("HOME", "Popup");
        //    ifShinkiPopup.Src = "JC12MitsumoriKensaku.aspx";
        //    mpeShinkiPopup.Show();
        //    updShinkiPopup.Update();
        //}

        protected void lnkBtnMitsumDirect_Click(object sender, EventArgs e)
        {
            SessionUtility.SetSession("cBukken", null);
            SessionUtility.SetSession("cMitumori", null);
            if (!String.IsNullOrEmpty(Request.QueryString["id"]))
            {
                String id = Request.QueryString["id"];
                Response.Redirect("JC10MitsumoriTouroku.aspx?id=" + id, false);
            }
            else
            {
                Response.Redirect("JC01Login.aspx");
            }
        }
        protected void lnkBtnMitsuList_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(Request.QueryString["id"]))
            {
                String id = Request.QueryString["id"];
                Response.Redirect("JC31MitsumoriList.aspx?id=" + id, false);
            }
            else
            {
                Response.Redirect("JC01Login.aspx");
            }
        }

        protected void lnkBtnUriage_Click(object sender, EventArgs e)
        {
        }

        protected void lnkBtnSetting_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(Request.QueryString["id"]))
            {
                String id = Request.QueryString["id"];
                Response.Redirect("JC26Setting.aspx?id=" + id);
            }
            else
            {
                Response.Redirect("JC01Login.aspx");
            }
        }
        protected void lnkbtnKojiSetting_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(Request.QueryString["id"]))
            {
                String id = Request.QueryString["id"];
                Response.Redirect("JC16KoujinJougouSetting.aspx?id=" + id);
            }
            else
            {
                Response.Redirect("JC01Login.aspx");
            }
        }

        protected void lnkbtnLogoOut_Click(object sender, EventArgs e)
        {          
            Session["LoginId"] = null;
            Response.Redirect("JC01Login.aspx");
        }
        protected void lnkBtnUriageNew_Click(object sender, EventArgs e)
        {
            Session["linkValId"] = "uriage";
            insatsusettei = false;
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

        protected void lnkBtnUriageList_Click(object sender,EventArgs e)
        {
            if (!String.IsNullOrEmpty(Request.QueryString["id"]))
            {
                String id = Request.QueryString["id"];
                Response.Redirect("JC34UriageList.aspx?id=" + id);
            }
            else
            {
                Response.Redirect("JC01Login.aspx");
            }
        }

        protected void setNav2(string pagename)
        {
           
            if (pagename == "JC07Home" || pagename=="JC34UriageList" ||  pagename == "JC30BukkenList" || pagename == "JC31MitsumoriList")
            {
                navbar2.Visible = false;
                lnkbtnSubBukken.Visible = false;
                lnkbtnSubMitsumori.Visible = false;
                lnkbtnSubMitsuPrint.Visible = false;
                lnkbtnSubMitsuUriage.Visible = false;
                LKB_Shousai.Visible = false;
                LKB_Settei.Visible = false;
                if (pagename == "JC07Home")
                {                    
                    lnkbtnHome.Style.Add(" background-color", "rgba(46,117,182)");
                }
                else if (pagename == "JC30BukkenList")
                {
                    LK_navbardrop.Style.Add(" background-color", "rgba(46,117,182)");
                }
                else if (pagename == "JC31MitsumoriList")
                {
                    LK_navbardrop1.Style.Add(" background-color", "rgba(46,117,182)");
                }
                else if (pagename == "JC34UriageList")
                {
                    //navbardrop_Uri.Style.Add(" background-color", "rgba(46,117,182)");
                    lnk_Uriage.Style.Add(" background-color", "rgba(46,117,182)");
                    LKB_Settei.Style.Add(" background-color", "rgb(242,242,242)");
                }
            }
            else if (pagename == "JC27UriageTouroku")
            {
                navbar2.Visible = true;
                lnkbtnSubBukken.Visible = false;
                lnkbtnSubMitsumori.Visible = false;
                lnkbtnSubMitsuPrint.Visible = false;
                lnkbtnSubMitsuUriage.Visible = false;
                LKB_Shousai.Visible = true;
                LKB_Settei.Visible = true;
                LKB_Shousai.Style.Add(" font-size", "14px");
                LKB_Settei.Style.Add(" font-size", "14px");
                LKB_Shousai.Height = 40;
                LKB_Settei.Height = 40;

                //navbardrop_Uri.Style.Add(" background-color", "rgba(46,117,182)");
                if (insatsusettei == false)
                {
                    LKB_Shousai.Style.Add(" background-color", "rgb(191,191,191)");
                }
                else
                {
                    LKB_Settei.Style.Add(" background-color", "rgb(191,191,191)");
                    LKB_Shousai.Style.Add(" background-color", "rgb(242,242,242)");
                }
                
            }
            
        }
        protected void LKB_Shousai_Click(object sender, EventArgs e)
        {
            insatsusettei = false;
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
        protected void LKB_Settei_Click(object sender, EventArgs e)
        {
            insatsusettei = true;
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

        #region 見積検索ぽポップアップ閉じる　　//20211014 MiMi Added
        protected void btn_CloseMitumoriSearch_Click(object sender, EventArgs e)
        {
            ifShinkiPopup.Src = "";
            mpeShinkiPopup.Hide();
            updShinkiPopup.Update();
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

        #region LK_navbardrop_Click
        protected void LK_navbardrop_Click(object sender, EventArgs e)
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

        #region LK_navbardrop1_Click
        protected void LK_navbardrop1_Click(object sender, EventArgs e)
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
    }
}