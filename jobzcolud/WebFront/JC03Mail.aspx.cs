using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace jobzcolud
{
    public partial class JC03Mail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //20220310 Added エインドリ－ Start
            if (!IsPostBack)
            {
                if ((Request.QueryString["id"] == "JC16")){ //20220315 Updated id エインドリ－
                    LB_message1.Visible = false;
                    LB_message2.Visible = false;
                    LB_message3.Visible = false;
                    LB_message4.Text = "入力されたメールアドレスに認証リンクを送信しました。";
                    LB_message5.Text = "メールアプリにてご確認の上、認証してください。";
                }
            }
            //20220310 Added エインドリ－ End
        }
    }
}