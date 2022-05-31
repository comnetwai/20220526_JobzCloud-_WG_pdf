using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace jobzcolud.WebFront
{
    public partial class JC32ArarisuIkkatsuSetting : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
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

                        if (SessionUtility.GetSession("riritsu") != null)
                        {
                            String riritsu = SessionUtility.GetSession("riritsu").ToString();
                            tb_nRIIRITSU.Text = riritsu;
                        }

                        if (SessionUtility.GetSession("fuwagakimeisai") != null)
                        {
                            String fuwagakimeisai = SessionUtility.GetSession("fuwagakimeisai").ToString();
                            CK_Meisai.Checked = Convert.ToBoolean(fuwagakimeisai);
                        }

                        if (SessionUtility.GetSession("kisuu") != null)
                        {
                            String kisuu = SessionUtility.GetSession("kisuu").ToString();
                            if (kisuu == "0")
                            {
                                bt_1.CssClass = "btnActive";
                                bt_10.CssClass = "btn";
                                bt_100.CssClass = "btn";
                                bt_1000.CssClass = "btn";
                            }
                            else if (kisuu == "1")
                            {
                                bt_1.CssClass = "btn";
                                bt_10.CssClass = "btnActive";
                                bt_100.CssClass = "btn";
                                bt_1000.CssClass = "btn";
                            }
                            else if (kisuu == "2")
                            {
                                bt_1.CssClass = "btn";
                                bt_10.CssClass = "btn";
                                bt_100.CssClass = "btnActive";
                                bt_1000.CssClass = "btn";
                            }
                            else if (kisuu == "3")
                            {
                                bt_1.CssClass = "btn";
                                bt_10.CssClass = "btn";
                                bt_100.CssClass = "btn";
                                bt_1000.CssClass = "btnActive";
                            }
                        }

                        if (SessionUtility.GetSession("shisyagonyuu") != null)
                        {
                            String shisyagonyuu = SessionUtility.GetSession("shisyagonyuu").ToString();
                            if (shisyagonyuu == "0")
                            {
                                bt_shishagonyuu.CssClass = "btnActive";
                                bt_kiriage.CssClass = "btn";
                                bt_kirisute.CssClass = "btn";
                            }
                            else if (shisyagonyuu == "1")
                            {
                                bt_shishagonyuu.CssClass = "btn";
                                bt_kiriage.CssClass = "btnActive";
                                bt_kirisute.CssClass = "btn";
                            }
                            else if (shisyagonyuu == "2")
                            {
                                bt_shishagonyuu.CssClass = "btn";

                                bt_kiriage.CssClass = "btn";
                                bt_kirisute.CssClass = "btnActive";
                            }
                        }

                        if (SessionUtility.GetSession("fuwagakisyousai") != null)
                        {
                            String fuwagakisyousai = SessionUtility.GetSession("fuwagakisyousai").ToString();
                            CK_Syosai.Checked = Convert.ToBoolean(fuwagakisyousai);
                        }

                        if (SessionUtility.GetSession("GYOUZENTAI") != null)
                        {
                            String GYOUZENTAI = SessionUtility.GetSession("GYOUZENTAI").ToString();
                            if (GYOUZENTAI == "0")
                            {
                                bt_gyou.CssClass = "btnActive";
                                bt_zengyou.CssClass = "btn";
                            }
                            else
                            {
                                bt_gyou.CssClass = "btn";
                                bt_zengyou.CssClass = "btnActive";
                            }
                        }

                    }
                    Session.Timeout = 480;
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "parentButtonClick('btnToLogin','" + hdnHome.Value + "');", true);
                }
            }
            catch
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "parentButtonClick('btnToLogin','" + hdnHome.Value + "');", true);
            }
        }

        #region [キャンセル]ボタン
        protected void btn_cancel_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "parentButtonClick('btnClose','" + hdnHome.Value + "');", true);
        }
        #endregion

        #region 選択行クリック
        protected void bt_gyou_Click(object sender, EventArgs e)
        {
            bt_gyou.CssClass = "btnActive";
            bt_zengyou.CssClass = "btn";
        }
        #endregion

        #region 全行クリック
        protected void bt_zengyou_Click(object sender, EventArgs e)
        {
            bt_gyou.CssClass = "btn";
            bt_zengyou.CssClass = "btnActive";
        }
        #endregion

        #region 四捨五入クリック
        protected void bt_shishagonyuu_Click(object sender, EventArgs e)
        {
            bt_shishagonyuu.CssClass = "btnActive";
            bt_kiriage.CssClass = "btn";
            bt_kirisute.CssClass = "btn";
        }
        #endregion

        #region 切り上げクリック
        protected void bt_kiriage_Click(object sender, EventArgs e)
        {
            bt_shishagonyuu.CssClass = "btn";
            bt_kiriage.CssClass = "btnActive";
            bt_kirisute.CssClass = "btn";
        }
        #endregion

        #region 切り捨てクリック
        protected void bt_kirisute_Click(object sender, EventArgs e)
        {
            bt_shishagonyuu.CssClass = "btn";
            bt_kiriage.CssClass = "btn";
            bt_kirisute.CssClass = "btnActive";
        }
        #endregion

        #region 1円クリック
        protected void bt_1_Click(object sender, EventArgs e)
        {
            bt_1.CssClass = "btnActive";
            bt_10.CssClass = "btn";
            bt_100.CssClass = "btn";
            bt_1000.CssClass = "btn";
        }
        #endregion

        #region 10円クリック
        protected void bt_10_Click(object sender, EventArgs e)
        {
            bt_1.CssClass = "btn";
            bt_10.CssClass = "btnActive";
            bt_100.CssClass = "btn";
            bt_1000.CssClass = "btn";
        }
        #endregion

        #region 100円クリック
        protected void bt_100_Click(object sender, EventArgs e)
        {
            bt_1.CssClass = "btn";
            bt_10.CssClass = "btn";
            bt_100.CssClass = "btnActive";
            bt_1000.CssClass = "btn";
        }
        #endregion

        #region 1000円クリック
        protected void bt_1000_Click(object sender, EventArgs e)
        {
            bt_1.CssClass = "btn";
            bt_10.CssClass = "btn";
            bt_100.CssClass = "btn";
            bt_1000.CssClass = "btnActive";
        }
        #endregion

        #region [OK]ボタン
        protected void btn_ok_Click(object sender, EventArgs e)
        {
            Decimal riritsu = Convert.ToDecimal(tb_nRIIRITSU.Text);
            bool fuwagakimeisai = CK_Meisai.Checked;
            int kisuu = 0;
            if (bt_1.CssClass == "btnActive")
            {
                kisuu = 0;
            }
            else if (bt_10.CssClass == "btnActive")
            {
                kisuu = 1;
            }
            else if (bt_100.CssClass == "btnActive")
            {
                kisuu = 2;
            }
            else if (bt_1000.CssClass == "btnActive")
            {
                kisuu = 3;
            }
            int shisyagonyuu = 0;
            if (bt_shishagonyuu.CssClass == "btnActive")
            {
                shisyagonyuu = 0;
            }
            else if (bt_kiriage.CssClass == "btnActive")
            {
                shisyagonyuu = 1;
            }
            else if (bt_kirisute.CssClass == "btnActive")
            {
                shisyagonyuu = 2;
            }

            bool fuwagakisyousai = CK_Syosai.Checked;

            int GYOUZENTAI = 0;

            if (bt_gyou.CssClass == "btnActive")
            {
                GYOUZENTAI = 0;
            }
            else
            {
                GYOUZENTAI = 1;
            }

            Session["riritsu"] = riritsu;
            Session["fuwagakimeisai"] = fuwagakimeisai;
            Session["kisuu"] = kisuu;
            Session["shisyagonyuu"] = shisyagonyuu;
            Session["fuwagakisyousai"] = fuwagakisyousai;
            Session["GYOUZENTAI"] = GYOUZENTAI;

            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "parentButtonClick('btnArarisuIkkatsu','" + hdnHome.Value + "');", true);
        }
        #endregion

        #region tb_nRIIRITSU_TextChanged
        protected void tb_nRIIRITSU_TextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(tb_nRIIRITSU.Text))
            {
                tb_nRIIRITSU.Text = "0";
            }
        }
        #endregion
    }
}