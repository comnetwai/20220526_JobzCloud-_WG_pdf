using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace jobzcolud.WebFront
{
    public partial class JC35PdfPageChoice : System.Web.UI.Page
    {
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
                    if (Session["pageNumber"] != null)
                    {
                        LB_pageCount.Text = "/" + Session["pageNumber"].ToString();
                        HF_pageCount.Value = Session["pageNumber"].ToString();
                    }
                }
                Session.Timeout = 480;
            }
            else
            {
                Response.Redirect("JC01Login.aspx");
            }
        }

        protected void BT_Close_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtPageNumber.Text))
            {
                int inputPageCount = Convert.ToInt16(txtPageNumber.Text);
                int PageCount = Convert.ToInt16(HF_pageCount.Value);
                if (inputPageCount > PageCount || inputPageCount<=0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowErrorMessage",
                        "ShowErrorMessage('存在しないページ番号です。');", true);
                }
                else
                {
                    Session["pageNumber"] = inputPageCount;
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "parentButtonClick('btnPDFPageChoiceClose','" + hdnHome.Value + "');", true);
                }
            }
        }
    }
}