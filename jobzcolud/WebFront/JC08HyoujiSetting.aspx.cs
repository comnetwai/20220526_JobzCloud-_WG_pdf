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
using System.Web.UI.HtmlControls;

namespace cloudjobz_n
{
    public partial class JC08HyoujiSetting : System.Web.UI.Page
    {
        string updateQuery = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(Request.QueryString["id"]))
            {
                if (!this.IsPostBack)
                {
                    if (SessionUtility.GetSession("HOME") != null)  //20211108 テテ Added
                    {
                        hdnHome.Value = SessionUtility.GetSession("HOME").ToString();
                        SessionUtility.SetSession("HOME", null);
                    }
                    BindingHyojiSetting();
                }
                Session.Timeout = 480;
            }
            else
            {
                Response.Redirect("JC01Login.aspx");
            }
        }

        protected void BindingHyojiSetting()
        {
            string hyoujiVal = Session["HyoujiID"].ToString();
            hidCandidateId.Value = hyoujiVal;
            int listVal = 0;
            if (hyoujiVal == "bukken")
            {
                listVal = 1;
                lbl_header.Text = "物件リスト表示項目を設定";
            }
            else if (hyoujiVal == "mitsumori")
            {
                listVal = 2;
                lbl_header.Text = "見積リスト表示項目を設定";
            }
            else if (hyoujiVal=="uriage")
            {
                listVal = 3;
                lbl_header.Text = "売上リスト表示項目を設定";
            }
            else
            {
                listVal = 4;
                lbl_header.Text = "見積明細リスト表示項目を設定";
            }
            gvName.Value = hyoujiVal;

            string query = " SELECT cLIST,cHYOUJI,sHYOUJI";
            query += " ,'表示' as dis1,  '非表示' as dis2  ";
            query += " ,fHYOUJI";
            query += " ,nORDER ";
            query += " FROM m_hyoujikomoku ";
            query += " Where cLIST = "+listVal+" ";
            query += " order by nORDER; ";
            DataTable dt = new DataTable();

            String id = Request.QueryString["id"];
            DataTable dt_db = JC01Login_Class.Get_DB(id);
            String db = dt_db.Rows[0]["db"].ToString();
            String mail = dt_db.Rows[0]["mail"].ToString();
            ConstantVal.DB_NAME = db;

            JC08HyoujiSetting_Class gvVal = new JC08HyoujiSetting_Class();
            gvVal.loginId = mail;
            dt = gvVal.HyoujiData(query);
            gvID.DataSource = dt;
            gvID.DataBind();
        }

        protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.ClientID == "gvID")
                {
                    if (e.Row.RowIndex >= 0)
                    {
                        string hyoujiVal = Session["HyoujiID"].ToString();

                        TextBox txt_fhyouji = (TextBox)(e.Row.Cells[5].FindControl("fHYOUJI"));
                        string name = e.Row.Cells[1].Text;

                        string val = txt_fhyouji.Text;
                        LinkButton lkbtnDis = e.Row.FindControl("lkbtnDisplay") as LinkButton;
                        LinkButton lkbtnNotDis = e.Row.FindControl("lkbtnNotDisplay") as LinkButton;

                        if (name == "商品名" || name == "数量" || name == "単位" || name == "標準単価" || name == "合計金額")
                        {
                            if(name== "合計金額")
                            {
                                if (hyoujiVal == "mitsumorisyosai")
                                {
                                    //20220428 Chaw Added Start
                                    //lkbtnDis.ForeColor = System.Drawing.ColorTranslator.FromHtml("#dddddd");
                                    //lkbtnNotDis.ForeColor = System.Drawing.ColorTranslator.FromHtml("#dddddd");
                                    //e.Row.Cells[1].ForeColor = System.Drawing.ColorTranslator.FromHtml("#dddddd");

                                    //e.Row.CssClass = "disable-drag";//20220103
                                    //                                //e.Row.Cells[4].CssClass = "btnDragDisable";
                                    //var span = new HtmlGenericControl("span");//20220202
                                    //span.InnerHtml = "&#9776;";
                                    //span.Attributes["class"] = "btnDragDisable";
                                    //e.Row.Cells[4].Controls.Add(span);
                                    var span = new HtmlGenericControl("span");//20220202
                                    span.InnerHtml = "&#9776;";
                                    span.Attributes["class"] = "btnDrag";
                                    e.Row.Cells[4].Controls.Add(span);
                                    if (val == "0")
                                    {
                                        lkbtnDis.ForeColor = System.Drawing.Color.Blue;
                                        lkbtnNotDis.ForeColor = System.Drawing.Color.Gray;
                                    }
                                    else
                                    {
                                        lkbtnDis.ForeColor = System.Drawing.Color.Gray;
                                        lkbtnNotDis.ForeColor = System.Drawing.Color.Blue;
                                    }
                                    //20220428 Chaw Added End
                                }
                                else
                                {
                                    var span = new HtmlGenericControl("span");//20220202
                                    span.InnerHtml = "&#9776;";
                                    span.Attributes["class"] = "btnDrag";
                                    e.Row.Cells[4].Controls.Add(span);
                                    if (val == "0")
                                    {
                                        lkbtnDis.ForeColor = System.Drawing.Color.Blue;
                                        lkbtnNotDis.ForeColor = System.Drawing.Color.Gray;
                                    }
                                    else
                                    {
                                        lkbtnDis.ForeColor = System.Drawing.Color.Gray;
                                        lkbtnNotDis.ForeColor = System.Drawing.Color.Blue;
                                    }
                                }
                            }
                            else
                            {
                                lkbtnDis.ForeColor = System.Drawing.ColorTranslator.FromHtml("#dddddd");
                                lkbtnNotDis.ForeColor = System.Drawing.ColorTranslator.FromHtml("#dddddd");
                                e.Row.Cells[1].ForeColor = System.Drawing.ColorTranslator.FromHtml("#dddddd");

                                e.Row.CssClass = "disable-drag";//20220103
                                                                //e.Row.Cells[4].CssClass = "btnDragDisable";
                                var span = new HtmlGenericControl("span");//20220202
                                span.InnerHtml = "&#9776;";
                                span.Attributes["class"] = "btnDragDisable";
                                e.Row.Cells[4].Controls.Add(span);
                            }
                            
                        }
                        else
                        {
                            //e.Row.Cells[4].CssClass = "btnDrag";

                            if (name == "商品コード")//20220103
                            {
                                e.Row.CssClass = "disable-drag";
                                var span = new HtmlGenericControl("span");//20220202
                                span.InnerHtml = "&#9776;";
                                span.Attributes["class"] = "btnDragDisable";
                                e.Row.Cells[4].Controls.Add(span);
                            }
                            else if (name == "画像")
                            {
                                e.Row.Visible = false;
                            }
                            else
                            {
                                var span = new HtmlGenericControl("span");//20220202
                                span.InnerHtml = "&#9776;";
                                span.Attributes["class"] = "btnDrag";
                                e.Row.Cells[4].Controls.Add(span);
                            }
                            if (val == "0")
                            {
                                lkbtnDis.ForeColor = System.Drawing.Color.Blue;
                                
                                if (hyoujiVal == "uriage")
                                {
                                    if (name == "売上コード")
                                    {
                                        lkbtnNotDis.ForeColor = System.Drawing.ColorTranslator.FromHtml("#dddddd");
                                    }
                                    else
                                    {
                                        lkbtnNotDis.ForeColor = System.Drawing.Color.Gray;
                                    }
                                }
                                else
                                {
                                    if (name == "物件名" || name == "得意先名" || name == "見積名")
                                    {
                                        lkbtnNotDis.ForeColor = System.Drawing.ColorTranslator.FromHtml("#dddddd");
                                    }
                                    else
                                    {
                                        lkbtnNotDis.ForeColor = System.Drawing.Color.Gray;
                                    }
                                }
                            }
                            else
                            {
                                lkbtnDis.ForeColor = System.Drawing.Color.Gray;
                                lkbtnNotDis.ForeColor = System.Drawing.Color.Blue;
                            }
                        }
                    }
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int cList = 0;
            string hyoujiVal = Session["HyoujiID"].ToString();

            if (hyoujiVal == "bukken")
            {
                cList = 1;
            }
            else if (hyoujiVal == "mitsumori")
            {
                cList = 2;
            }
            else if (hyoujiVal == "uriage")
            {
                cList = 3;
            }
            else
            {
                cList = 4;
            }
            int[] hyoujiVals = (from p in Request.Form["cHYOUJI"].Split(',')
                                   select int.Parse(p)).ToArray();

            int[] linkVals = (from p in Request.Form["fHYOUJI"].Split(',')
                                 select int.Parse(p)).ToArray();
            int order = 1;
            int idx = 0;
            foreach (int hyouji in hyoujiVals)
            {
                int fval = linkVals[idx];

                updateQuery += "UPDATE `m_hyoujikomoku` SET `fHYOUJI`='" + fval + "', `nORDER`='" + order + "' WHERE `cLIST`='" + cList + "' and `cHYOUJI`='" + hyouji + "';";

                order += 1;
                idx++;
            }
            try
            {
                this.UpdatePreference(updateQuery);
                Session["HyoujiID"] = null;
                
            }
            catch(Exception ex)
            {

            }
            
            //Page.ClientScript.RegisterStartupScript(this.GetType(), "Hide", "refreshParent();", true);
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "parentButtonClick('btnHyoujiSave','" + hdnHome.Value + "');", true);

        }

        private void UpdatePreference(string query)
        {
            try
            {
                String id = Request.QueryString["id"];
                DataTable dt_db = JC01Login_Class.Get_DB(id);
                String db = dt_db.Rows[0]["db"].ToString();
                String mail = dt_db.Rows[0]["mail"].ToString();
                ConstantVal.DB_NAME = db;

                JC08HyoujiSetting_Class updateVal = new JC08HyoujiSetting_Class();
                updateVal.loginId = mail;
                updateVal.updateHyoujiListSql(query);
            }
            catch (Exception ex)
            {

            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Session["HyoujiID"] = null;
            //Page.ClientScript.RegisterStartupScript(this.GetType(), "Hide", "refreshParent();", true);
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "parentButtonClick('btnHyoujiClose','" + hdnHome.Value + "');", true);

        }

        protected void BtnDefault_Click(object sender, EventArgs e)
        { }
    }
}