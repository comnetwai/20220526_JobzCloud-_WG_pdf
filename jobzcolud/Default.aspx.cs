using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Service;
using Common;

namespace jobzcolud
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (DBUtilitycs.Server == "")
                {
                    DBUtilitycs.get_connetion_ifo();
                }             
                // ログインメインページへ移動する
                Response.Redirect("WebFront/JC01Login.aspx");
            }
        }
    }
}