using Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace jobzcolud.WebFront
{
    public partial class JC33MitumoriSyosaiCopy : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(Request.QueryString["id"]))
                {
                    if (!IsPostBack)
                    {
                        if (SessionUtility.GetSession("HOME") != null)
                        {
                            hdnHome.Value = SessionUtility.GetSession("HOME").ToString();
                            SessionUtility.SetSession("HOME", null);
                        }

                        DataTable dt_Syosai = new DataTable();
                        if (Session["SyosaiCopydt"] != null)
                        {
                            dt_Syosai = Session["SyosaiCopydt"] as DataTable;
                        }
                        GV_MitumoriSyosai.DataSource = dt_Syosai;
                        GV_MitumoriSyosai.DataBind();
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

        #region btncancel_Click
        protected void btncancel_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "parentButtonClick('btn_CloseSyosaiCopy','" + hdnHome.Value + "');", true);
        }
        #endregion

        #region CreateSyouhinTableColomn
        private DataTable CreateSyouhinTableColomn()
        {
            DataTable dt_Syohin = new DataTable();
            dt_Syohin.Columns.Add("status");
            dt_Syohin.Columns.Add("cSYOHIN");
            dt_Syohin.Columns.Add("sSYOHIN");
            dt_Syohin.Columns.Add("nSURYO");
            dt_Syohin.Columns.Add("cTANI");
            dt_Syohin.Columns.Add("nTANKA");
            dt_Syohin.Columns.Add("nTANKAGOUKEI");
            dt_Syohin.Columns.Add("nGENKATANKA");
            dt_Syohin.Columns.Add("nGENKAGOUKEI");
            dt_Syohin.Columns.Add("nARARI");
            dt_Syohin.Columns.Add("nARARISu");
            dt_Syohin.Columns.Add("rowNo");
            dt_Syohin.Columns.Add("nRITU");
            dt_Syohin.Columns.Add("nSIKIRITANKA");
            dt_Syohin.Columns.Add("fJITAIS");
            return dt_Syohin;
        }
        #endregion

        #region btnOk_Click
        protected void btnOk_Click(object sender, EventArgs e)
        {
            DataTable dt = CreateSyouhinTableColomn();
            foreach (GridViewRow row in GV_MitumoriSyosai.Rows)
            {
                CheckBox ck= (row.FindControl("chkSelectSyouhin") as CheckBox);
                Label lbl_status = (row.FindControl("lblhdnStatus") as Label);
                Label lbl_rowno = (row.FindControl("lblRowNo") as Label);
                Label lblcSyohin = (row.FindControl("lblcSyohin") as Label);
                Label lblsSyohin = (row.FindControl("lblsSyohin") as Label);
                Label lblnSuryo = (row.FindControl("lblSuyo") as Label);
                Label lblctani = (row.FindControl("lblcTANI") as Label);
                Label lblnTanka = (row.FindControl("lblnTanka") as Label);
                Label lblTankaGokei = (row.FindControl("lblnTANKAGOUKEI") as Label);
                Label lblnGenkaTanka = (row.FindControl("lblnGENKATANKA") as Label);
                Label lblGenkaGokei = (row.FindControl("lblnGENKAGOUKEI") as Label);
                Label lblArari = (row.FindControl("lblnARARI") as Label);
                Label lblArariSu = (row.FindControl("lbl_nARARISu") as Label);
                Label lblnRitu = (row.FindControl("lbl_nRITU") as Label);
                Label lbl_nSikiriTanka = (row.FindControl("lbl_nSIKIRITANKA") as Label);
                Label lbl_fJITAIS = (row.FindControl("lblfjitais") as Label);

                if (ck.Checked)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = lbl_status.Text;
                    dr[1] = lblcSyohin.Text;
                    dr[2] = lblsSyohin.Text.Replace("&lt","<").Replace("&gt",">");
                    dr[3] = lblnSuryo.Text;
                    dr[4] = lblctani.Text;
                    dr[5] = lblnTanka.Text;
                    dr[6] = lblTankaGokei.Text;
                    dr[7] = lblnGenkaTanka.Text;
                    dr[8] = lblnGenkaTanka.Text;
                    dr[9] = lblArari.Text;
                    dr[10] = lblArariSu.Text;
                    dr[11] = lbl_rowno.Text;
                    dr[12] = lblnRitu.Text;
                    dr[13] = lbl_nSikiriTanka.Text;
                    dr[14] = lbl_fJITAIS.Text;
                    dt.Rows.Add(dr);
                }
            }

            Session["SyosaiCopydt"] = dt;
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "parentButtonClick('btn_SelectSyosaiCopy','" + hdnHome.Value + "');", true);
        }
        #endregion

        #region GV_MitumoriSyosai_RowDataBound
        protected void GV_MitumoriSyosai_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lbl_sSyouhin = (e.Row.FindControl("lblsSyohin") as Label);
                String sSyohin = lbl_sSyouhin.Text;
                sSyohin = sSyohin.Replace("<", "&lt").Replace(">", "&gt");
                lbl_sSyouhin.Text = sSyohin;
            }
        }
        #endregion
    }
}