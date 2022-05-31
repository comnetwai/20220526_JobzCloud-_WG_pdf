using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace jobzcolud.WebFront
{
    public partial class JC26Setting : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(Request.QueryString["id"]))
            {
                if (!IsPostBack)
                {
                    JC99NavBar navbar_Master = (JC99NavBar)this.Master;
                    navbar_Master.lnkBtnSetting.Style.Add(" background-color", "rgba(46,117,182)");
                    navbar_Master.navbar2.Visible = false;
                }
                Session.Timeout = 480;
            }
            else
            {
                Response.Redirect("JC01Login.aspx");
            }
        }

        protected void btnJishaInfoSetting_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(Request.QueryString["id"]))
            {
                String id = Request.QueryString["id"];
                Response.Redirect("JC29Jishajouhousettei.aspx?id=" + id);
            }
            else
            {
                Response.Redirect("JC01Login.aspx");
            }
        }

        protected void btnUserSetting_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(Request.QueryString["id"]))
            {
                String id = Request.QueryString["id"];
                Response.Redirect("JC28UserSetting.aspx?id=" + id);
            }
            else
            {
                Response.Redirect("JC01Login.aspx");
            }
        }

        protected void btnSupplierSearch_Click(object sender, EventArgs e)
        {
            SessionUtility.SetSession("HOME", "Master");

            String id = Request.QueryString["id"];
            ifShinkiPopup.Src = "JC40Shiiresaki.aspx?id=" + id;
            mpeShinkiPopup.Show();
            updShinkiPopup.Update();

        }
        #region btn_CloseShinkiSentaku_Click
        protected void btn_CloseShinkiSentaku_Click(object sender, EventArgs e)
        {
            ifShinkiPopup.Src = "";
            mpeShinkiPopup.Hide();
            updShinkiPopup.Update();
        }
        #endregion



        #region btn_CloseSearch_Click
        protected void btn_CloseSearch_Click(object sender, EventArgs e)
        {
            ifShinkiPopup.Src = "";
            mpeShinkiPopup.Hide();
            updShinkiPopup.Update();
        }
        #endregion

        #region btnsyouhinSetting_Click
        protected void btnsyouhinSetting_Click(object sender, EventArgs e)
        {
            //Response.Redirect("JC37Shohin.aspx");
            Session["fGamen"] = "Setting";
            Session["fSyouhinSyosai"] = "Master";
           // SessionUtility.SetSession("HOME", "Popup");
            SessionUtility.SetSession("HOME", "Master");
            //ifShouhinPopup.Src = "JC37Shohin.aspx";
            //mpeShouhinPopup.Show();
            //updShouhinPopup.Update();
            if (!String.IsNullOrEmpty(Request.QueryString["id"]))
            {
                String id = Request.QueryString["id"];
                Response.Redirect("JC37Shohin.aspx?id=" + id);
            }
            else
            {
                Response.Redirect("JC01Login.aspx");
            }
        }
        #endregion

        #region btn_Close_Click
        protected void btn_Close_Click(object sender, EventArgs e)
        {
            ifShinkiPopup.Src = "";
            mpeShinkiPopup.Hide();
            updShinkiPopup.Update();
        }
        #endregion
    }
}