/*作成者：ナン
 *作成日：20210901
 */
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
    public partial class JC25Kengenn : System.Web.UI.Page
    {
        string fcol = "";
        JC25Kengenn_Class kgVal = new JC25Kengenn_Class();
        //JC25MistuJyoutai_Class mtVal = new JC25MistuJyoutai_Class();
        protected void Page_Load(object sender, EventArgs e)
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

                    BindinJoytaiList();

                }
                Session.Timeout = 480;
            }
            else
            {
                //Response.Redirect("JC01Login.aspx");
            }
        }

        #region "権限データ取得、Bindingする"
        /// <summary>
        /// 権限データ取得、Bindingする
        /// </summary>
        private void BindinJoytaiList()
        {
            String id = Request.QueryString["id"];
            DataTable dt_db = JC01Login_Class.Get_DB(id);
            String db = dt_db.Rows[0]["db"].ToString();
            String mail = dt_db.Rows[0]["mail"].ToString();
            ConstantVal.DB_NAME = db;

            DataTable dt = new DataTable();
            string sqlstring = " SELECT cKENGENN,sKENGENN FROM m_kengenn;  ";
            kgVal.loginId = mail;
            dt = kgVal.KengennListTable(sqlstring);            
            gvKengennlist.DataSource = dt;
            gvKengennlist.DataBind();
        }
        #endregion

        #region "行選択"
        /// <summary>
        /// データ削除を削除する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvKengennlist_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //int index = int.Parse(e.CommandArgument.ToString());
            if (e.CommandName == "Select")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = gvKengennlist.Rows[rowIndex];
                string cCo = row.Cells[0].Text;              
                Session["cKengenn"] = cCo;
                
                string sKENGENN = (row.FindControl("txtsKENGENN") as TextBox).Text;
                Session["sKengenn"] = sKENGENN;
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "parentButtonClick('btnKengennSelect','"+hdnHome.Value+"');", true);                
            }
        }
        #endregion

        #region "PopUp画面閉じる"
        /// <summary>
        /// PopUp画面閉じる
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCancel_Click(object sender, EventArgs e)
        {           
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "parentButtonClick('btnClose','"+hdnHome.Value+"');", true);
        }
        #endregion
    }
}