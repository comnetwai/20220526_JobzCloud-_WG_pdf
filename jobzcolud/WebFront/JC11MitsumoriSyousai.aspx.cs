using Common;
using MySql.Data.MySqlClient;
using Service;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JobzCloud.WebFront
{
    public partial class JC11MitsumoriSyousai : System.Web.UI.Page
    {
        DataTable dtSyosai = null;
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

                        DataTable dt_Meisai = new DataTable();
                        if (Session["SyohinMeisaidt"] != null)
                        {
                            dt_Meisai = Session["SyohinMeisaidt"] as DataTable;
                        }
                        else
                        {
                            dt_Meisai = CreateSyouhinMeisaiTableColomn();
                            for (int i = 1; i <= 15; i++)
                            {
                                DataRow dr = dt_Meisai.NewRow();
                                dr[0] = "";
                                dr[1] = "";
                                dr[2] = "";
                                dr[3] = "";
                                dr[4] = "";
                                dr[5] = "";
                                dr[6] = "";
                                dr[7] = "";
                                dr[8] = "";
                                dr[9] = "";
                                dr[10] = "";
                                dr[11] = "1";
                                dr[12] = "0";
                                dr[13] = "100%";
                                dr[14] = "";
                                dr[15] = "";
                                dr[16] = "0";
                                dr[17] = "0";
                                dt_Meisai.Rows.Add(dr);
                            }
                        }

                        DataTable dt_Syosai = new DataTable();
                        if (Session["SyohinSyosaidt"] != null)
                        {
                            dt_Syosai = Session["SyohinSyosaidt"] as DataTable;
                        }
                        else
                        {
                            dt_Syosai = CreateSyouhinTableColomn();
                        }
                        dtSyosai = new DataTable();
                        dtSyosai = dt_Syosai;

                        GV_MitumoriMeisai.DataSource = dt_Meisai;
                        GV_MitumoriMeisai.DataBind();

                        HF_selectRowIndex.Value = "1";
                        int rowindex = 0;
                        if (Session["syohinSelectRowIndex"] != null)
                        {
                            HF_selectRowIndex.Value = (Convert.ToInt32((string)Session["syohinSelectRowIndex"]) + 1).ToString();
                            rowindex = Convert.ToInt32((string)Session["syohinSelectRowIndex"]);
                        }

                        GV_MitumoriMeisai.Rows[rowindex].BackColor = System.Drawing.ColorTranslator.FromHtml("#e4c400");

                        GV_Syosai.DataSource = dt_Syosai;
                        GV_Syosai.DataBind();

                        DataTable dt = CreateSyouhinTableColomn();
                        if (dt_Syosai.Rows.Count > 0)
                        {
                            int rowNo = Convert.ToInt32((GV_MitumoriMeisai.Rows[rowindex].FindControl("lblRowNo") as Label).Text);
                            String nRitu1 = (GV_MitumoriMeisai.Rows[rowindex].FindControl("lblMeisai_nRITU") as Label).Text;
                            DataRow[] rows = dt_Syosai.Select("rowNo = '" + rowNo + "'");
                            if (rows.Length > 0)
                            {
                                foreach (var drow in rows)
                                {
                                    DataRow dr = dt.NewRow();
                                    dr[0] = drow[0].ToString();
                                    dr[1] = drow[1].ToString();
                                    dr[2] = drow[2].ToString();
                                    dr[3] = drow[3].ToString();
                                    dr[4] = drow[4].ToString();
                                    dr[5] = drow[5].ToString();
                                    dr[6] = drow[6].ToString();
                                    dr[7] = drow[7].ToString();
                                    dr[8] = drow[8].ToString();
                                    dr[9] = drow[9].ToString();
                                    dr[10] = drow[10].ToString();
                                    dr[11] = drow[11].ToString();
                                    dr[12] = drow[12].ToString();
                                    dr[13] = drow[13].ToString();
                                    dr[14] = drow[14].ToString();
                                    dt.Rows.Add(dr);
                                }
                            }
                        }

                        String nRitu = (GV_MitumoriMeisai.Rows[rowindex].FindControl("lblMeisai_nRITU") as Label).Text;
                        HF_fgentanka.Value = (GV_MitumoriMeisai.Rows[rowindex].FindControl("lblfgenkatanka") as Label).Text;
                        int rowCount = dt.Rows.Count;
                        while (dt.Rows.Count < 15)
                        {
                            DataRow dr = dt.NewRow();
                            dr[0] = "";
                            dr[1] = "";
                            dr[2] = "";
                            dr[3] = "";
                            dr[4] = "";
                            dr[5] = "";
                            dr[6] = "";
                            dr[7] = "";
                            dr[8] = "";
                            dr[9] = "";
                            dr[10] = "";
                            dr[11] = "";
                            dr[12] = nRitu;
                            dr[13] = "";
                            dr[14] = "1";
                            dt.Rows.Add(dr);
                        }

                        if ((GV_MitumoriMeisai.Rows[rowindex].FindControl("lblfgenkatanka") as Label).Text == "0")  //単価原価方式
                        {
                            btn_genka.CssClass = "JC10ZeikomiBtn";
                            btn_gentanka.CssClass = "JC10ZeikomiBtnActive";
                            //RB_genka.Checked = false;
                            //RB_gentanka.Checked = true;
                        }
                        else
                        {
                            btn_genka.CssClass = "JC10ZeikomiBtnActive";
                            btn_gentanka.CssClass = "JC10ZeikomiBtn";
                            //RB_genka.Checked = true;
                            //RB_gentanka.Checked = false;
                        }

                        GV_MitumoriSyohin.DataSource = dt;
                        GV_MitumoriSyohin.DataBind();

                        txtKakeRitsuItem.Text = "100%";
                        if ((GV_MitumoriMeisai.Rows[rowindex].FindControl("lblMeisai_Suyo") as Label).Text != "0" && !String.IsNullOrEmpty((GV_MitumoriMeisai.Rows[rowindex].FindControl("lblMeisai_Suyo") as Label).Text))
                        {
                            txtSuryoItem.Text = (GV_MitumoriMeisai.Rows[rowindex].FindControl("lblMeisai_Suyo") as Label).Text;
                        }
                        else
                        {
                            txtSuryoItem.Text = "";
                        }

                        txttaniItem.Text = (GV_MitumoriMeisai.Rows[rowindex].FindControl("lblMeisai_cTANI") as Label).Text;

                        if (rowCount == 0 && btn_genka.CssClass == "JC10ZeikomiBtnActive")
                        {
                            if ((GV_MitumoriMeisai.Rows[rowindex].FindControl("lblMeisai_nTanka") as Label).Text != "0" && !String.IsNullOrEmpty((GV_MitumoriMeisai.Rows[rowindex].FindControl("lblMeisai_nTanka") as Label).Text))
                            {
                                txtHyouJunTankaItem.Text = (GV_MitumoriMeisai.Rows[rowindex].FindControl("lblMeisai_nTanka") as Label).Text;
                            }
                            else
                            {
                                txtHyouJunTankaItem.Text = "";
                            }

                            if ((GV_MitumoriMeisai.Rows[rowindex].FindControl("lblMeisai_nTanka") as Label).Text != "0" && !String.IsNullOrEmpty((GV_MitumoriMeisai.Rows[rowindex].FindControl("lblMeisai_nTanka") as Label).Text))
                            {
                                txtTankaItem.Text = (GV_MitumoriMeisai.Rows[rowindex].FindControl("lblMeisai_nTanka") as Label).Text;
                            }
                            else
                            {
                                txtTankaItem.Text = "";
                            }

                            if ((GV_MitumoriMeisai.Rows[rowindex].FindControl("lblMeisai_nTANKAGOUKEI") as Label).Text != "0" && !String.IsNullOrEmpty((GV_MitumoriMeisai.Rows[rowindex].FindControl("lblMeisai_nTANKAGOUKEI") as Label).Text))
                            {
                                txtGokeiKingakuItem.Text = (GV_MitumoriMeisai.Rows[rowindex].FindControl("lblMeisai_nTANKAGOUKEI") as Label).Text;
                            }
                            else
                            {
                                txtGokeiKingakuItem.Text = "";
                            }
                            if ((GV_MitumoriMeisai.Rows[rowindex].FindControl("lblMeisai_nGENKATANKA") as Label).Text != "0" && !String.IsNullOrEmpty((GV_MitumoriMeisai.Rows[rowindex].FindControl("lblMeisai_nGENKATANKA") as Label).Text))
                            {
                                txtGenTankaItem.Text = (GV_MitumoriMeisai.Rows[rowindex].FindControl("lblMeisai_nGENKATANKA") as Label).Text;
                            }
                            else
                            {
                                txtGenTankaItem.Text = "";
                            }
                            if ((GV_MitumoriMeisai.Rows[rowindex].FindControl("lblMeisai_nGENKAGOUKEI") as Label).Text != "0" && !String.IsNullOrEmpty((GV_MitumoriMeisai.Rows[rowindex].FindControl("lblMeisai_nGENKAGOUKEI") as Label).Text))
                            {
                                txtGenkaItem.Text = (GV_MitumoriMeisai.Rows[rowindex].FindControl("lblMeisai_nGENKAGOUKEI") as Label).Text;
                            }
                            else
                            {
                                txtGenkaItem.Text = "";
                            }
                            if ((GV_MitumoriMeisai.Rows[rowindex].FindControl("lblMeisai_nARARI") as Label).Text != "0" && !String.IsNullOrEmpty((GV_MitumoriMeisai.Rows[rowindex].FindControl("lblMeisai_nARARI") as Label).Text))
                            {
                                txtArariItem.Text = (GV_MitumoriMeisai.Rows[rowindex].FindControl("lblMeisai_nARARI") as Label).Text;
                            }
                            else
                            {
                                txtArariItem.Text = "0";
                            }
                            if ((GV_MitumoriMeisai.Rows[rowindex].FindControl("lblMeisai_nARARISu") as Label).Text != "0.0%" && !String.IsNullOrEmpty((GV_MitumoriMeisai.Rows[rowindex].FindControl("lblMeisai_nARARISu") as Label).Text))
                            {
                                txtArariSuItem.Text = (GV_MitumoriMeisai.Rows[rowindex].FindControl("lblMeisai_nARARISu") as Label).Text;
                            }
                            else
                            {
                                txtArariSuItem.Text = "";
                            }
                        }
                        else
                        {
                            KeiSan_CB_nKIRI_G();
                            KeiSan_CB_nTANKA_G();
                            KeiSan_ARARI();
                        }
                        HF_isChange.Value = "0";
                        HasCheckRow();
                        updBody.Update();

                    }
                    else
                    {
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

        #region CreateSyouhinMeisaiTableColomn
        private DataTable CreateSyouhinMeisaiTableColomn()
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
            dt_Syohin.Columns.Add("fgentankatanka");
            dt_Syohin.Columns.Add("rowNo");
            dt_Syohin.Columns.Add("nRITU");
            dt_Syohin.Columns.Add("sKUBUN");
            dt_Syohin.Columns.Add("nSIKIRITANKA");
            dt_Syohin.Columns.Add("fJITAIS");
            dt_Syohin.Columns.Add("fJITAIQ");
            return dt_Syohin;
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

        #region "グリッドに商品行追加"
        protected void btnSyouhinAdd_Click(object sender, EventArgs e)
        {
            Button btn_addSyohin = (Button)sender;
            GridViewRow gvRow = (GridViewRow)btn_addSyohin.NamingContainer;
            int rowID = gvRow.RowIndex + 1;
            DataTable dt = GetGridViewData(GV_MitumoriSyohin);
            //if (dt.Rows.Count > 1)
            //{
                int rowindex = Convert.ToInt32(HF_selectRowIndex.Value) - 1;
                String nRitu = (GV_MitumoriMeisai.Rows[rowindex].FindControl("lblMeisai_nRITU") as Label).Text;
                DataRow dr = dt.NewRow();
                dr[0] = "";
                dr[1] = "";
                dr[2] = "";
                dr[3] = "";
                dr[4] = "";
                dr[5] = "";
                dr[6] = "";
                dr[7] = "";
                dr[8] = "";
                dr[9] = "";
                dr[10] = "";
                dr[11] = "";
                dr[12] = nRitu;
                dr[13] = "";
                dr[14] = "1";
            dt.Rows.InsertAt(dr, rowID);
            //}

            ViewState["SyouhinTable"] = dt;
            GV_MitumoriSyohin.DataSource = dt;
            GV_MitumoriSyohin.DataBind();

            //BindAllSyosai();
            KeiSan_CB_nKIRI_G();
            KeiSan_CB_nTANKA_G();
            KeiSan_ARARI();
            HF_isChange.Value = "1";
            HasCheckRow();
            updBody.Update();
        }

        protected void BT_SyohinEmptyAdd_Click(object sender, EventArgs e)
        {
            DataTable dt = GetGridViewData(GV_MitumoriSyohin);

            int rowindex = Convert.ToInt32(HF_selectRowIndex.Value) - 1;
            String nRitu = (GV_MitumoriMeisai.Rows[rowindex].FindControl("lblMeisai_nRITU") as Label).Text;
            DataRow dr = dt.NewRow();
            dr[0] = "";
            dr[1] = "";
            dr[2] = "";
            dr[3] = "";
            dr[4] = "";
            dr[5] = "";
            dr[6] = "";
            dr[7] = "";
            dr[8] = "";
            dr[9] = "";
            dr[10] = "";
            dr[11] = "";
            dr[12] = nRitu;
            dt.Rows.Add(dr);

            ViewState["SyouhinTable"] = dt;
            GV_MitumoriSyohin.DataSource = dt;
            GV_MitumoriSyohin.DataBind();

            //BindAllSyosai();
            KeiSan_CB_nKIRI_G();
            KeiSan_CB_nTANKA_G();
            KeiSan_ARARI();
            HF_isChange.Value = "1";
            if (GV_MitumoriSyohin.Rows.Count == 0)
            {
                BT_SyohinEmptyAdd.CssClass = "JC10GrayButton";
            }
            else
            {
                BT_SyohinEmptyAdd.CssClass = "DisplayNone";
            }
            HasCheckRow();
            updBody.Update();
        }
        #endregion

        #region GetGridViewData
        private DataTable GetGridViewData(GridView gv)
        {
            DataTable dt = CreateSyouhinTableColomn();
            foreach (GridViewRow row in gv.Rows)
            {
                Label lbl_status = (row.FindControl("lblhdnStatus") as Label);
                Label lbl_rowno = (row.FindControl("lblRowNo") as Label);
                TextBox txt_cSyohin = (row.FindControl("txtcSYOHIN") as TextBox);
                TextBox txt_sSyohin = (row.FindControl("txtsSYOHIN") as TextBox);
                TextBox txt_nSyoryo = (row.FindControl("txtnSURYO") as TextBox);
                //DropDownList ddl_cTani = (row.FindControl("DDL_cTANI") as DropDownList);
                //TextBox txt_cTani = (row.FindControl("txtTani") as TextBox);
                Label lbl_cTani = (row.FindControl("lblcTANI") as Label);
                TextBox txt_nTanka = (row.FindControl("txtnTANKA") as TextBox);
                Label lbl_TankaGokei = (row.FindControl("lblTankaGokei") as Label);
                TextBox txt_nGenkaTanka = (row.FindControl("txtnGENKATANKA") as TextBox);
                Label lbl_GenkaGokei = (row.FindControl("lblGenkaGokei") as Label);
                Label lbl_Arari = (row.FindControl("lblnARARI") as Label);
                Label lbl_ArariSu = (row.FindControl("lblnARARISu") as Label);
                TextBox txt_nRitu = (row.FindControl("txtnRITU") as TextBox);
                Label lbl_nSIKIRITANKA = (row.FindControl("lblTanka") as Label);
                Label lbl_fJITAIS = (row.FindControl("lblfjitais") as Label);

                DataRow dr = dt.NewRow();
                dr[0] = lbl_status.Text;
                dr[1] = txt_cSyohin.Text;
                dr[2] = txt_sSyohin.Text;
                dr[3] = txt_nSyoryo.Text;
                dr[4] = lbl_cTani.Text;
                dr[5] = txt_nTanka.Text;
                dr[6] = lbl_TankaGokei.Text;
                dr[7] = txt_nGenkaTanka.Text;
                dr[8] = lbl_GenkaGokei.Text;
                dr[9] = lbl_Arari.Text;
                dr[10] = lbl_ArariSu.Text;
                dr[11] = lbl_rowno.Text;
                dr[12] = txt_nRitu.Text;
                dr[13] = lbl_nSIKIRITANKA.Text;
                dr[14] = lbl_fJITAIS.Text;
                dt.Rows.Add(dr);
            }
            return dt;
        }
        #endregion

        #region GetSyosaiGridViewData
        private DataTable GetSyosaiGridViewData(GridView gv)
        {
            DataTable dt = CreateSyouhinTableColomn();
            DataTable dt1 = CreateSyouhinTableColomn();
            int rowindex = Convert.ToInt32(HF_selectRowIndex.Value) - 1;
            String rowNo = (GV_MitumoriMeisai.Rows[rowindex].FindControl("lblRowNo") as Label).Text;
            if (rowNo == "0")
            {
                DataTable dt_Meisai = GetMeisaiGridViewData();
                var max = dt_Meisai.AsEnumerable().Max(x => int.Parse(x.Field<string>("rowNo")));
                max += 1;
                (GV_MitumoriMeisai.Rows[rowindex].FindControl("lblRowNo") as Label).Text = max.ToString();
                updMitsumoriMeisaiGrid.Update();
                rowNo = max.ToString();
            }
            foreach (GridViewRow row in gv.Rows)
            {
                Label lbl_status = (row.FindControl("lblhdnStatus") as Label);
                Label lbl_rowno = (row.FindControl("lblRowNo") as Label);
                TextBox txt_cSyohin = (row.FindControl("txtcSYOHIN") as TextBox);
                TextBox txt_sSyohin = (row.FindControl("txtsSYOHIN") as TextBox);
                TextBox txt_nSyoryo = (row.FindControl("txtnSURYO") as TextBox);
                DropDownList ddl_cTani = (row.FindControl("DDL_cTANI") as DropDownList);
                TextBox txt_cTani = (row.FindControl("txtTani") as TextBox);
                Label lbl_cTani = (row.FindControl("lblcTANI") as Label);
                TextBox txt_nTanka = (row.FindControl("txtnTANKA") as TextBox);
                Label lbl_TankaGokei = (row.FindControl("lblTankaGokei") as Label);
                TextBox txt_nGenkaTanka = (row.FindControl("txtnGENKATANKA") as TextBox);
                Label lbl_GenkaGokei = (row.FindControl("lblGenkaGokei") as Label);
                Label lbl_Arari = (row.FindControl("lblnARARI") as Label);
                Label lbl_ArariSu = (row.FindControl("lblnARARISu") as Label);
                TextBox txt_nRitu = (row.FindControl("txtnRITU") as TextBox);
                Label lbl_nSIKIRITANKA = (row.FindControl("lblTanka") as Label);
                Label lbl_fJITAIS = (row.FindControl("lblfjitais") as Label);


                if (!String.IsNullOrEmpty(txt_cSyohin.Text) || !String.IsNullOrEmpty(txt_sSyohin.Text) ||
                            !String.IsNullOrEmpty(txt_nSyoryo.Text) || !String.IsNullOrEmpty(lbl_cTani.Text) ||
                            !String.IsNullOrEmpty(txt_nTanka.Text) || !String.IsNullOrEmpty(lbl_TankaGokei.Text) ||
                            !String.IsNullOrEmpty(txt_nGenkaTanka.Text) || !String.IsNullOrEmpty(lbl_GenkaGokei.Text))
                {
                    dt.Merge(dt1);
                    dt.AcceptChanges();
                    dt1.Rows.Clear();

                    DataRow dr = dt.NewRow();
                    dr[0] = lbl_status.Text;
                    dr[1] = txt_cSyohin.Text;
                    dr[2] = txt_sSyohin.Text;
                    dr[3] = txt_nSyoryo.Text;
                    dr[4] = lbl_cTani.Text;
                    dr[5] = txt_nTanka.Text;
                    dr[6] = lbl_TankaGokei.Text;
                    dr[7] = txt_nGenkaTanka.Text;
                    dr[8] = lbl_GenkaGokei.Text;
                    dr[9] = lbl_Arari.Text;
                    dr[10] = lbl_ArariSu.Text;
                    dr[11] = rowNo;
                    dr[12] = txt_nRitu.Text;
                    dr[13] = lbl_nSIKIRITANKA.Text;
                    dr[14] = lbl_fJITAIS.Text;
                    dt.Rows.Add(dr);
                }
                else
                {
                    DataRow dr = dt1.NewRow();
                    dr[0] = lbl_status.Text;
                    dr[1] = txt_cSyohin.Text;
                    dr[2] = txt_sSyohin.Text;
                    dr[3] = txt_nSyoryo.Text;
                    dr[4] = lbl_cTani.Text;
                    dr[5] = txt_nTanka.Text;
                    dr[6] = lbl_TankaGokei.Text;
                    dr[7] = txt_nGenkaTanka.Text;
                    dr[8] = lbl_GenkaGokei.Text;
                    dr[9] = lbl_Arari.Text;
                    dr[10] = lbl_ArariSu.Text;
                    dr[11] = rowNo;
                    dr[12] = txt_nRitu.Text;
                    dr[13] = lbl_nSIKIRITANKA.Text;
                    dr[14] = lbl_fJITAIS.Text;
                    dt1.Rows.Add(dr);
                }
            }
            return dt;
        }
        #endregion

        #region GetMeisaiGridViewData
        private DataTable GetMeisaiGridViewData()
        {
            DataTable dt = CreateSyouhinMeisaiTableColomn();
            foreach (GridViewRow row in GV_MitumoriMeisai.Rows)
            {
                Label lbl_status = (row.FindControl("lblhdnStatus") as Label);
                Label lbl_fgenkataka = (row.FindControl("lblfgenkatanka") as Label);
                Label lbl_rowNo = (row.FindControl("lblRowNo") as Label);
                Label txt_cSyohin = (row.FindControl("lblcMeisai") as Label);
                Label txt_sSyohin = (row.FindControl("lblsMeisai") as Label);
                Label txt_nSyoryo = (row.FindControl("lblMeisai_Suyo") as Label);
                Label lbl_cTani = (row.FindControl("lblMeisai_cTANI") as Label);
                Label txt_nTanka = (row.FindControl("lblMeisai_nTanka") as Label);
                Label lbl_TankaGokei = (row.FindControl("lblMeisai_nTANKAGOUKEI") as Label);
                Label txt_nGenkaTanka = (row.FindControl("lblMeisai_nGENKATANKA") as Label);
                Label lbl_GenkaGokei = (row.FindControl("lblMeisai_nGENKAGOUKEI") as Label);
                Label lbl_Arari = (row.FindControl("lblMeisai_nARARI") as Label);
                Label lbl_ArariSu = (row.FindControl("lblMeisai_nARARISu") as Label);
                Label lbl_nRitu = (row.FindControl("lblMeisai_nRITU") as Label);
                Label lbl_sKubun = (row.FindControl("lblKubun") as Label);
                Label lbl_nSIKIRITANKA = (row.FindControl("lblMeisai_nSIKIRITANKA") as Label);
                Label lbl_fJITAIS = (row.FindControl("lblfjitais") as Label);
                Label lbl_fJITAIQ = (row.FindControl("lblfjitaiq") as Label);

                DataRow dr = dt.NewRow();
                dr[0] = lbl_status.Text;
                dr[1] = txt_cSyohin.Text;
                if (lbl_sKubun.Text != "計")
                {
                    dr[2] = txt_sSyohin.Text.Replace("&lt", "<").Replace("&gt", ">");
                }
                else
                {
                    dr[2] = "";
                }
                dr[3] = txt_nSyoryo.Text;
                dr[4] = lbl_cTani.Text;
                dr[5] = txt_nTanka.Text;
                dr[6] = lbl_TankaGokei.Text;
                dr[7] = txt_nGenkaTanka.Text;
                dr[8] = lbl_GenkaGokei.Text;
                dr[9] = lbl_Arari.Text;
                dr[10] = lbl_ArariSu.Text;
                dr[11] = lbl_fgenkataka.Text;
                dr[12] = lbl_rowNo.Text;
                dr[13] = lbl_nRitu.Text;
                dr[14] = lbl_sKubun.Text;
                dr[15] = lbl_nSIKIRITANKA.Text;
                dr[16] = lbl_fJITAIS.Text;
                dr[17] = lbl_fJITAIQ.Text;
                dt.Rows.Add(dr);
            }
            return dt;
        }
        #endregion

        #region グリッドに商品行複写（該当行の下にコピー）
        protected void btnSyohinCopy_Click(object sender, EventArgs e)
        {
            Button btnSyohinCopy = (Button)sender;
            GridViewRow gvRow = (GridViewRow)btnSyohinCopy.NamingContainer;
            int rowID = gvRow.RowIndex + 1;

            DataTable dt = GetGridViewData(GV_MitumoriSyohin);
            var dr_copy = dt.NewRow();
            DataRow dr_exist = dt.Rows[gvRow.RowIndex];
            dr_copy.ItemArray = dr_exist.ItemArray.Clone() as object[]; ;
            dt.Rows.InsertAt(dr_copy, rowID);
            ViewState["SyouhinTable"] = dt;
            GV_MitumoriSyohin.DataSource = dt;
            GV_MitumoriSyohin.DataBind();
            HF_isChange.Value = "1";
            //BindAllSyosai();
            KeiSan_CB_nKIRI_G();
            KeiSan_CB_nTANKA_G();
            KeiSan_ARARI();
            HasCheckRow();
            updBody.Update();
        }
        #endregion

        #region グリッドに商品行削除
        protected void btnSyohinDelete_Click(object sender, EventArgs e)
        {
            LinkButton btnSyohinDelete = (LinkButton)sender;
            GridViewRow gvRow = (GridViewRow)btnSyohinDelete.NamingContainer;
            int rowID = gvRow.RowIndex;
            DataTable dt = GetGridViewData(GV_MitumoriSyohin);
            dt.Rows.RemoveAt(rowID);
            ViewState["SyouhinTable"] = dt;
            GV_MitumoriSyohin.DataSource = dt;
            GV_MitumoriSyohin.DataBind();
            HF_isChange.Value="1";
            //BindAllSyosai();
            KeiSan_CB_nKIRI_G();
            KeiSan_CB_nTANKA_G();
            KeiSan_ARARI();
            if (GV_MitumoriSyohin.Rows.Count == 0)
            {
                BT_SyohinEmptyAdd.CssClass = "JC10GrayButton";
            }
            else
            {
                BT_SyohinEmptyAdd.CssClass = "DisplayNone";
            }
            HasCheckRow();
            updBody.Update();
        }
        #endregion

        #region btncancel_Click
        protected void btncancel_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "parentButtonClick('btn_CloseSearch','" + hdnHome.Value + "');", true);
        }
        #endregion

        #region GV_MitumoriMeisai_RowDataBound
        protected void GV_MitumoriMeisai_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lbl_sKubun = (e.Row.FindControl("lblKubun") as Label);
                Label lbl_sSyouhin = (e.Row.FindControl("lblsMeisai") as Label);
                if (lbl_sKubun.Text == "計")
                {
                    (e.Row.FindControl("lblsMeisai") as Label).Text = "【小計】";
                }

                if (lbl_sKubun.Text != "計" && lbl_sKubun.Text != "見")
                {
                    String sSyohin = lbl_sSyouhin.Text;
                    sSyohin = sSyohin.Replace("<", "&lt").Replace(">", "&gt");
                    lbl_sSyouhin.Text = sSyohin;

                    e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(GV_MitumoriMeisai, "Select$" + e.Row.RowIndex);
                    int rowNo = Convert.ToInt32((e.Row.FindControl("lblRowNo") as Label).Text);
                    DataRow[] rows = dtSyosai.Select("rowNo = '" + rowNo + "'");
                    if (rows.Length > 0)
                    {
                        (e.Row.FindControl("btnMeisaiCopy") as Button).CssClass = "JC10GrayButton";
                        (e.Row.FindControl("btnMeisaiCopy") as Button).Enabled = true;
                    }
                    else
                    {
                        (e.Row.FindControl("btnMeisaiCopy") as Button).CssClass = "JC10DisableButton";
                        (e.Row.FindControl("btnMeisaiCopy") as Button).Enabled = false;
                    }
                }
                else
                {
                    (e.Row.FindControl("btnMeisaiCopy") as Button).CssClass = "JC10DisableButton";
                    (e.Row.FindControl("btnMeisaiCopy") as Button).Enabled = false;
                }
            }
        }
        #endregion

        #region GV_MitumoriMeisai_SelectedIndexChanged
        protected void GV_MitumoriMeisai_SelectedIndexChanged(object sender, EventArgs e)
        {
            HF_rowIndex.Value = GV_MitumoriMeisai.SelectedIndex.ToString();
            HF_OK.Value = "0";
            updBody.Update();
            if (HF_isChange.Value == "1")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowAnkenChangeMessage",
                        "ShowKoumokuChangesConfirmMessage('項目が変更されています。更新しますか？','" + btnYes.ClientID + "','" + btnNo.ClientID + "','" + btnCancel1.ClientID + "');", true);
            }
            else
            {
                SyosaiDisplay();
            }
            
        }

        #endregion

        #region btnYes_Click
        protected void btnYes_Click(object sender, EventArgs e)
        {
            BindAllSyosai();
            if (HF_OK.Value == "0")
            {
                SyosaiDisplay();
            }
            else if (HF_OK.Value == "1")
            {
                DataTable dt_Meisai = GetMeisaiGridViewData();
                Session["SyohinMeisaidt"] = dt_Meisai;

                DataTable dt_Syosai = GetGridViewData(GV_Syosai);
                dt_Syosai.DefaultView.Sort = "rowNo asc";
                Session["SyohinSyosaidt"] = dt_Syosai;

                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "parentButtonClick('btn_getSyosai','" + hdnHome.Value + "');", true);
            }
        }
        #endregion

        #region btnNo_Click
        protected void btnNo_Click(object sender, EventArgs e)
        {
            int rowindex = Convert.ToInt32(HF_selectRowIndex.Value);
            (GV_MitumoriMeisai.Rows[rowindex - 1].FindControl("lblfgenkatanka") as Label).Text = HF_fgentanka.Value;
            if (HF_OK.Value == "0")
            {
                SyosaiDisplay();
            }
            else if (HF_OK.Value == "1")
            {
                DataTable dt_Meisai = GetMeisaiGridViewData();
                Session["SyohinMeisaidt"] = dt_Meisai;

                DataTable dt_Syosai = GetGridViewData(GV_Syosai);
                dt_Syosai.DefaultView.Sort = "rowNo asc";
                Session["SyohinSyosaidt"] = dt_Syosai;

                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "parentButtonClick('btn_getSyosai','" + hdnHome.Value + "');", true);
            }
        }
        #endregion

        #region SyosaiDisplay
        public void SyosaiDisplay()
        {
            int rowindex = Convert.ToInt32(HF_rowIndex.Value);
            for (int r = 0; r < GV_MitumoriMeisai.Rows.Count; r++)
            {
                if (r == rowindex)
                {
                    GV_MitumoriMeisai.Rows[r].BackColor = System.Drawing.ColorTranslator.FromHtml("#e4c400");
                    HF_selectRowIndex.Value = (r + 1).ToString();
                    DataTable dt_Syosai = GetGridViewData(GV_Syosai);
                    DataTable dt = CreateSyouhinTableColomn();

                    int rowNo = Convert.ToInt32((GV_MitumoriMeisai.Rows[rowindex].FindControl("lblRowNo") as Label).Text);
                    HF_fgentanka.Value = (GV_MitumoriMeisai.Rows[rowindex].FindControl("lblfgenkatanka") as Label).Text;
                    String nRitu = (GV_MitumoriMeisai.Rows[rowindex].FindControl("lblMeisai_nRITU") as Label).Text;
                    if (dt_Syosai.Rows.Count > 0)
                    {
                        DataRow[] rows = dt_Syosai.Select("rowNo = '" + rowNo + "'");
                        if (rows.Length > 0)
                        {
                            foreach (var drow in rows)
                            {
                                DataRow dr = dt.NewRow();
                                dr[0] = drow[0].ToString();
                                dr[1] = drow[1].ToString();
                                dr[2] = drow[2].ToString();
                                dr[3] = drow[3].ToString();
                                dr[4] = drow[4].ToString();
                                dr[5] = drow[5].ToString();
                                dr[6] = drow[6].ToString();
                                dr[7] = drow[7].ToString();
                                dr[8] = drow[8].ToString();
                                dr[9] = drow[9].ToString();
                                dr[10] = drow[10].ToString();
                                dr[11] = rowNo;
                                dr[12] = drow[12].ToString();
                                dr[13] = drow[13].ToString();
                                dr[14] = drow[14].ToString();
                                dt.Rows.Add(dr);
                            }
                        }
                    }
                    int rowCount = dt.Rows.Count;
                    while (dt.Rows.Count < 15)
                    {
                        DataRow dr = dt.NewRow();
                        dr[0] = "";
                        dr[1] = "";
                        dr[2] = "";
                        dr[3] = "";
                        dr[4] = "";
                        dr[5] = "";
                        dr[6] = "";
                        dr[7] = "";
                        dr[8] = "";
                        dr[9] = "";
                        dr[10] = "";
                        dr[11] = rowNo;
                        dr[12] = nRitu;
                        dr[13] = "";
                        dr[14] = "1";
                        dt.Rows.Add(dr);
                    }

                    if ((GV_MitumoriMeisai.Rows[r].FindControl("lblfgenkatanka") as Label).Text == "0")  //単価原価方式
                    {
                        btn_genka.CssClass = "JC10ZeikomiBtn";
                        btn_gentanka.CssClass = "JC10ZeikomiBtnActive";
                        //RB_genka.Checked = false;
                        //RB_gentanka.Checked = true;
                    }
                    else
                    {
                        btn_genka.CssClass = "JC10ZeikomiBtnActive";
                        btn_gentanka.CssClass = "JC10ZeikomiBtn";
                        //RB_genka.Checked = true;
                        //RB_gentanka.Checked = false;
                    }

                    GV_MitumoriSyohin.DataSource = dt;
                    GV_MitumoriSyohin.DataBind();

                    if ((GV_MitumoriMeisai.Rows[r].FindControl("lblMeisai_Suyo") as Label).Text != "0" && !String.IsNullOrEmpty((GV_MitumoriMeisai.Rows[r].FindControl("lblMeisai_Suyo") as Label).Text))
                    {
                        txtSuryoItem.Text = (GV_MitumoriMeisai.Rows[r].FindControl("lblMeisai_Suyo") as Label).Text;
                    }
                    else
                    {
                        txtSuryoItem.Text = "";
                    }

                    txttaniItem.Text = (GV_MitumoriMeisai.Rows[r].FindControl("lblMeisai_cTANI") as Label).Text;

                    if (rowCount == 0 && btn_genka.CssClass== "JC10ZeikomiBtnActive")
                    {
                        if ((GV_MitumoriMeisai.Rows[r].FindControl("lblMeisai_nTanka") as Label).Text != "0" && !String.IsNullOrEmpty((GV_MitumoriMeisai.Rows[r].FindControl("lblMeisai_nTanka") as Label).Text))
                        {
                            txtHyouJunTankaItem.Text = (GV_MitumoriMeisai.Rows[r].FindControl("lblMeisai_nTanka") as Label).Text;
                        }
                        else
                        {
                            txtHyouJunTankaItem.Text = "";
                        }

                        if ((GV_MitumoriMeisai.Rows[r].FindControl("lblMeisai_nTanka") as Label).Text != "0" && !String.IsNullOrEmpty((GV_MitumoriMeisai.Rows[r].FindControl("lblMeisai_nTanka") as Label).Text))
                        {
                            txtTankaItem.Text = (GV_MitumoriMeisai.Rows[r].FindControl("lblMeisai_nTanka") as Label).Text;
                        }
                        else
                        {
                            txtTankaItem.Text = "";
                        }

                        if ((GV_MitumoriMeisai.Rows[r].FindControl("lblMeisai_nTANKAGOUKEI") as Label).Text != "0" && !String.IsNullOrEmpty((GV_MitumoriMeisai.Rows[r].FindControl("lblMeisai_nTANKAGOUKEI") as Label).Text))
                        {
                            txtGokeiKingakuItem.Text = (GV_MitumoriMeisai.Rows[r].FindControl("lblMeisai_nTANKAGOUKEI") as Label).Text;
                        }
                        else
                        {
                            txtGokeiKingakuItem.Text = "";
                        }
                        if ((GV_MitumoriMeisai.Rows[r].FindControl("lblMeisai_nGENKATANKA") as Label).Text != "0" && !String.IsNullOrEmpty((GV_MitumoriMeisai.Rows[r].FindControl("lblMeisai_nGENKATANKA") as Label).Text))
                        {
                            txtGenTankaItem.Text = (GV_MitumoriMeisai.Rows[r].FindControl("lblMeisai_nGENKATANKA") as Label).Text;
                        }
                        else
                        {
                            txtGenTankaItem.Text = "";
                        }
                        if ((GV_MitumoriMeisai.Rows[r].FindControl("lblMeisai_nGENKAGOUKEI") as Label).Text != "0" && !String.IsNullOrEmpty((GV_MitumoriMeisai.Rows[r].FindControl("lblMeisai_nGENKAGOUKEI") as Label).Text))
                        {
                            txtGenkaItem.Text = (GV_MitumoriMeisai.Rows[r].FindControl("lblMeisai_nGENKAGOUKEI") as Label).Text;
                        }
                        else
                        {
                            txtGenkaItem.Text = "";
                        }
                        if ((GV_MitumoriMeisai.Rows[r].FindControl("lblMeisai_nARARI") as Label).Text != "0" && !String.IsNullOrEmpty((GV_MitumoriMeisai.Rows[r].FindControl("lblMeisai_nARARI") as Label).Text))
                        {
                            txtArariItem.Text = (GV_MitumoriMeisai.Rows[r].FindControl("lblMeisai_nARARI") as Label).Text;
                        }
                        else
                        {
                            txtArariItem.Text = "0";
                        }
                        if ((GV_MitumoriMeisai.Rows[r].FindControl("lblMeisai_nARARISu") as Label).Text != "0" && !String.IsNullOrEmpty((GV_MitumoriMeisai.Rows[r].FindControl("lblMeisai_nARARISu") as Label).Text))
                        {
                            txtArariSuItem.Text = (GV_MitumoriMeisai.Rows[r].FindControl("lblMeisai_nARARISu") as Label).Text;
                        }
                        else
                        {
                            txtArariSuItem.Text = "";
                        }
                    }
                    else
                    {
                        KeiSan_CB_nKIRI_G();
                        KeiSan_CB_nTANKA_G();
                        KeiSan_ARARI();
                    }

                }
                else
                {
                    GV_MitumoriMeisai.Rows[r].BackColor = System.Drawing.Color.White;
                }
            }
            HF_isChange.Value = "0";
            if (GV_MitumoriSyohin.Rows.Count == 0)
            {
                BT_SyohinEmptyAdd.CssClass = "JC10GrayButton";
            }
            else
            {
                BT_SyohinEmptyAdd.CssClass = "DisplayNone";
            }
            HasCheckRow();
            updBody.Update();
            ScriptManager.RegisterStartupScript(this, GetType(), "setscroll", "SetScrollTop();", true);
        }
        #endregion

        #region txtcSYOHIN_TextChanged  
        protected void txtcSYOHIN_TextChanged(object sender, EventArgs e)
        {
            try
            {
                var txt_cSYOUHIN = sender as TextBox;
                GridViewRow gvr = (GridViewRow)txt_cSYOUHIN.NamingContainer;
                int rowindex = gvr.RowIndex;
                string cSYOHIN = (GV_MitumoriSyohin.Rows[rowindex].FindControl("txtcSYOHIN") as TextBox).Text;
                if (cSYOHIN != "")
                {
                    cSYOHIN = cSYOHIN.ToString().PadLeft(10, '0');
                    (GV_MitumoriSyohin.Rows[rowindex].FindControl("txtcSYOHIN") as TextBox).Text = cSYOHIN;

                    String id = Request.QueryString["id"];
                    DataTable dt_db = JC01Login_Class.Get_DB(id);
                    String db = dt_db.Rows[0]["db"].ToString();
                    String mail = dt_db.Rows[0]["mail"].ToString();
                    ConstantVal.DB_NAME = db;

                    JC_ClientConnecction_Class jc = new JC_ClientConnecction_Class();
                    jc.loginId = mail;
                    MySqlConnection cn = jc.GetConnection();
                    cn.Open();
                    string sql_syouhin = " ";
                    sql_syouhin = " Select " +
                                  " ms.cSYOUHIN as cSYOUHIN," +
                                  " IfNull(ms.sSYOUHIN, '') as sSYOUHIN," +
                                  " IfNull(ms.sSHIYOU, '') as sSHIYOU, " +
                                  " IfNull(ms.sTANI,'') as sTANI, " +
                                  " format(ifnull(ms.nHANNBAIKAKAKU, 0),2) as nHANNBAIKAKAKU," +
                                  " Format(Round((ifnull(ms.nHANNBAIKAKAKU, 0))*(ifnull(ms.nSYOUKISU, 1)),0),0) as TankaGokei," +
                                  " Format(ifnull(ms.nSHIIREKAKAKU, 0),2) as nSHIIREKAKAKU," +
                                  " Format(Round((ifnull(ms.nSHIIREKAKAKU, 0))*(ifnull(ms.nSYOUKISU, 1)),0),0) as GenkaGokei," +
                                  " format(Round((ifnull(ms.nHANNBAIKAKAKU, 0))*(ifnull(ms.nSYOUKISU, 1)),0)-Round((ifnull(ms.nSHIIREKAKAKU, 0))*(ifnull(ms.nSYOUKISU, 1)),0),0) as nARARI," +
                                  " CONCAT(FORMAT(IfNull((Round((ifnull(ms.nHANNBAIKAKAKU, 0))*(ifnull(ms.nSYOUKISU, 1)),0)-Round((ifnull(ms.nSHIIREKAKAKU, 0))*(ifnull(ms.nSYOUKISU, 1)),0))/(Round((ifnull(ms.nHANNBAIKAKAKU, 0))*(ifnull(ms.nSYOUKISU, 1)),0)),0)*100, 1),'%') As nARARISu," +
                                  " ifnull(ms.nSYOUKISU, 1) as nSYOUKISU," +
                                  " MSD.sSYOUHIN_DAIGRP as sSYOUHIN_DAIGRP," +
                                  " MST.sSYOUHIN_TYUUGRP as sSYOUHIN_TYUUGRP," +
                                  " ifnull(ms.sBIKOU, '') as sBIKOU," +
                                  " ifnull(ms.fJITAIS, '0') as fJITAIS" +
                                  " From m_syouhin ms " +
                                  " left join" +
                                  " M_SYOUHIN_DAIGRP MSD ON MSD.cSYOUHIN_DAIGRP = ms.cSYOUHIN_DAIGRP" +
                                  " left join " +
                                  " M_SYOUHIN_TYUUGRP MST ON ms.cSYOUHIN_TYUUGRP = MST.cSYOUHIN_TYUUGRP" +
                                  " Where (ms.fHAIBAN <> '1' or ms.fHAIBAN is null) and '1' = '1' and ms.cSYOUHIN like '%" + cSYOHIN + "%'; ";


                    MySqlCommand cmd = new MySqlCommand(sql_syouhin, cn);
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    DataTable dtable = new DataTable();
                    da.Fill(dtable);
                    cn.Close();
                    da.Dispose();
                    if (dtable.Rows.Count > 0)
                    {
                        (GV_MitumoriSyohin.Rows[rowindex].FindControl("txtsSYOHIN") as TextBox).Text = dtable.Rows[0]["sSYOUHIN"].ToString() + "　" + dtable.Rows[0]["sSHIYOU"].ToString();
                        (GV_MitumoriSyohin.Rows[rowindex].FindControl("txtsSYOHIN") as TextBox).ToolTip = dtable.Rows[0]["sSYOUHIN"].ToString() + "　" + dtable.Rows[0]["sSHIYOU"].ToString();
                        (GV_MitumoriSyohin.Rows[rowindex].FindControl("lblfjitais") as Label).Text = dtable.Rows[0]["fJITAIS"].ToString();
                        if (dtable.Rows[0]["fJITAIS"].ToString() == "0")
                        {
                            (GV_MitumoriSyohin.Rows[rowindex].FindControl("LK_Syou") as LinkButton).BackColor = System.Drawing.Color.White;
                            (GV_MitumoriSyohin.Rows[rowindex].FindControl("txtsSYOHIN") as TextBox).MaxLength = 50;
                        }
                        else
                        {
                            (GV_MitumoriSyohin.Rows[rowindex].FindControl("LK_Syou") as LinkButton).BackColor = ColorTranslator.FromHtml("#f2f2f2");
                            (GV_MitumoriSyohin.Rows[rowindex].FindControl("txtsSYOHIN") as TextBox).MaxLength = 60;
                        }

                        double nSuryo = 0;
                        if (!String.IsNullOrEmpty(dtable.Rows[0]["nSYOUKISU"].ToString()))
                        {
                            nSuryo = Convert.ToDouble(dtable.Rows[0]["nSYOUKISU"].ToString());
                        }

                        (GV_MitumoriSyohin.Rows[rowindex].FindControl("txtnSURYO") as TextBox).Text = nSuryo.ToString("#,##0.##");
                        (GV_MitumoriSyohin.Rows[rowindex].FindControl("txtnSURYO") as TextBox).ToolTip = nSuryo.ToString("#,##0.##");

                        (GV_MitumoriSyohin.Rows[rowindex].FindControl("lblcTANI") as Label).Text = dtable.Rows[0]["sTANI"].ToString();
                        (GV_MitumoriSyohin.Rows[rowindex].FindControl("lblcTANI") as Label).ToolTip = dtable.Rows[0]["sTANI"].ToString();
                        (GV_MitumoriSyohin.Rows[rowindex].FindControl("txtTani") as TextBox).Text = dtable.Rows[0]["sTANI"].ToString();
                        string getsTANI = " select distinct cTANI, sTANI from M_TANI order by cTANI ";
                        MySqlCommand cmd1 = new MySqlCommand(getsTANI, cn);
                        MySqlDataAdapter da1 = new MySqlDataAdapter(cmd1);
                        DataTable dt = new DataTable();
                        da1.Fill(dt);
                        cn.Close();
                        da1.Dispose();
                        DropDownList DropDownList1 = (GV_MitumoriSyohin.Rows[rowindex].FindControl("DDL_cTANI") as DropDownList);
                        DropDownList1.DataSource = dt;
                        DropDownList1.DataTextField = "sTANI";
                        DropDownList1.DataValueField = "sTANI";
                        DropDownList1.DataBind();
                        string lblcTANI = (GV_MitumoriSyohin.Rows[rowindex].FindControl("lblcTANI") as Label).Text;
                        DropDownList1.Items.Insert(0, new ListItem(" ", "00"));
                        try
                        {
                            DropDownList1.Items.FindByText(lblcTANI).Selected = true;
                        }
                        catch { }

                        Double nkakeritsu = Convert.ToDouble((GV_MitumoriSyohin.Rows[rowindex].FindControl("txtnRITU") as TextBox).Text.Replace("%", ""));
                        double nHanbaikakaku = 0;
                        Double nTankaGokei = 0;
                        Double nSikiriTanka = 0;
                        if (!String.IsNullOrEmpty(dtable.Rows[0]["nHANNBAIKAKAKU"].ToString()))
                        {
                            nHanbaikakaku = Convert.ToDouble(dtable.Rows[0]["nHANNBAIKAKAKU"].ToString());
                            nTankaGokei = ((nHanbaikakaku / 100) * nkakeritsu) * nSuryo;
                            nSikiriTanka = (nHanbaikakaku / 100) * nkakeritsu;
                        }

                        double nSHIIREKAKAKU = 0;
                        double nGenkaGokei = 0;
                        if (!String.IsNullOrEmpty(dtable.Rows[0]["nSHIIREKAKAKU"].ToString()))
                        {
                            nSHIIREKAKAKU = Convert.ToDouble(dtable.Rows[0]["nSHIIREKAKAKU"].ToString());
                            nGenkaGokei = nSHIIREKAKAKU * nSuryo;
                        }

                        Double nArari = nTankaGokei - nGenkaGokei;

                        double nArariSu = (nArari / nTankaGokei) * 100;
                        if (nTankaGokei == 0)
                        {
                            nArariSu = 0;
                        }

                        if (btn_genka.CssClass== "JC10ZeikomiBtnActive")
                        {
                            (GV_MitumoriSyohin.Rows[rowindex].FindControl("txtnTANKA") as TextBox).Text = "0";
                            (GV_MitumoriSyohin.Rows[rowindex].FindControl("lblnTANKA") as Label).Text = "0";
                            (GV_MitumoriSyohin.Rows[rowindex].FindControl("lblTankaGokei") as Label).Text = "0";
                            (GV_MitumoriSyohin.Rows[rowindex].FindControl("lblTanka") as Label).Text = "0";
                            (GV_MitumoriSyohin.Rows[rowindex].FindControl("txtnTANKA") as TextBox).ToolTip = "0";
                            (GV_MitumoriSyohin.Rows[rowindex].FindControl("lblnTANKA") as Label).ToolTip = "0";
                            (GV_MitumoriSyohin.Rows[rowindex].FindControl("lblTankaGokei") as Label).ToolTip = "0";
                            (GV_MitumoriSyohin.Rows[rowindex].FindControl("lblTanka") as Label).ToolTip = "0";


                            (GV_MitumoriSyohin.Rows[rowindex].FindControl("txtnGENKATANKA") as TextBox).Text = nSHIIREKAKAKU.ToString("#,##0.##");
                            (GV_MitumoriSyohin.Rows[rowindex].FindControl("lblGenkaGokei") as Label).Text = nGenkaGokei.ToString("#,##0");
                            (GV_MitumoriSyohin.Rows[rowindex].FindControl("lblnARARI") as Label).Text = nArari.ToString("#,##0");
                            (GV_MitumoriSyohin.Rows[rowindex].FindControl("lblnARARISu") as Label).Text = "0.0%";


                            (GV_MitumoriSyohin.Rows[rowindex].FindControl("txtnGENKATANKA") as TextBox).ToolTip = nSHIIREKAKAKU.ToString("#,##0.##");
                            (GV_MitumoriSyohin.Rows[rowindex].FindControl("lblGenkaGokei") as Label).ToolTip = nGenkaGokei.ToString("#,##0");
                            (GV_MitumoriSyohin.Rows[rowindex].FindControl("lblnARARI") as Label).ToolTip = nArari.ToString("#,##0");
                            (GV_MitumoriSyohin.Rows[rowindex].FindControl("lblnARARISu") as Label).ToolTip = "0.0%";
                        }
                        else
                        {
                            (GV_MitumoriSyohin.Rows[rowindex].FindControl("txtnTANKA") as TextBox).Text = nHanbaikakaku.ToString("#,##0.##");
                            (GV_MitumoriSyohin.Rows[rowindex].FindControl("lblnTANKA") as Label).Text = nHanbaikakaku.ToString("#,##0.##");
                            (GV_MitumoriSyohin.Rows[rowindex].FindControl("lblTankaGokei") as Label).Text = nTankaGokei.ToString("#,##0");
                            (GV_MitumoriSyohin.Rows[rowindex].FindControl("lblTanka") as Label).Text = nSikiriTanka.ToString("#,##0");
                            (GV_MitumoriSyohin.Rows[rowindex].FindControl("txtnTANKA") as TextBox).ToolTip = nHanbaikakaku.ToString("#,##0.##");
                            (GV_MitumoriSyohin.Rows[rowindex].FindControl("lblnTANKA") as Label).ToolTip = nHanbaikakaku.ToString("#,##0.##");
                            (GV_MitumoriSyohin.Rows[rowindex].FindControl("lblTankaGokei") as Label).ToolTip = nTankaGokei.ToString("#,##0");
                            (GV_MitumoriSyohin.Rows[rowindex].FindControl("lblTanka") as Label).ToolTip = nSikiriTanka.ToString("#,##0");

                            (GV_MitumoriSyohin.Rows[rowindex].FindControl("txtnGENKATANKA") as TextBox).Text = nSHIIREKAKAKU.ToString("#,##0.##");
                            (GV_MitumoriSyohin.Rows[rowindex].FindControl("lblGenkaGokei") as Label).Text = nGenkaGokei.ToString("#,##0");
                            (GV_MitumoriSyohin.Rows[rowindex].FindControl("lblnARARI") as Label).Text = nArari.ToString("#,##0");
                            (GV_MitumoriSyohin.Rows[rowindex].FindControl("lblnARARISu") as Label).Text = nArariSu.ToString("###0.0") + "%";


                            (GV_MitumoriSyohin.Rows[rowindex].FindControl("txtnGENKATANKA") as TextBox).ToolTip = nSHIIREKAKAKU.ToString("#,##0.##");
                            (GV_MitumoriSyohin.Rows[rowindex].FindControl("lblGenkaGokei") as Label).ToolTip = nGenkaGokei.ToString("#,##0");
                            (GV_MitumoriSyohin.Rows[rowindex].FindControl("lblnARARI") as Label).ToolTip = nArari.ToString("#,##0");
                            (GV_MitumoriSyohin.Rows[rowindex].FindControl("lblnARARISu") as Label).ToolTip = nArariSu.ToString("###0.0") + "%";
                        }


                    }
                    else
                    {
                        (GV_MitumoriSyohin.Rows[rowindex].FindControl("txtcSYOHIN") as TextBox).Text = "";
                        (GV_MitumoriSyohin.Rows[rowindex].FindControl("txtcSYOHIN") as TextBox).ToolTip = "";
                    }

                }
                HF_isChange.Value = "1";
                //BindAllSyosai();
                KeiSan_CB_nKIRI_G();
                KeiSan_CB_nTANKA_G();
                KeiSan_ARARI();

                //GetTotalKingaku();
                updMitsumoriSyohinGrid.Update();
                HasCheckRow();
                updBody.Update();
            }
            catch
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "parentButtonClick('btnToLogin','" + hdnHome.Value + "');", true);
            }
        }
        #endregion

        #region txtnSURYO_TextChanged  
        protected void txtnSURYO_TextChanged(object sender, EventArgs e)
        {
            var txt_nSURYO = sender as TextBox;
            if (txt_nSURYO.Text == "")
            {
                txt_nSURYO.Text = "0";
            }
            GridViewRow gvr = (GridViewRow)txt_nSURYO.NamingContainer;
            int rowindex = gvr.RowIndex;
            double nSURYO = Convert.ToDouble(txt_nSURYO.Text.Replace(",", ""));
            double TankaGokei = 0;
            double nSIKIRITANKA = 0;
            Double nkakeritsu = Convert.ToDouble((GV_MitumoriSyohin.Rows[rowindex].FindControl("txtnRITU") as TextBox).Text.Replace("%", ""));
            if (!String.IsNullOrEmpty((GV_MitumoriSyohin.Rows[rowindex].FindControl("txtnTANKA") as TextBox).Text))
            {
                double nTANKA = Double.Parse((GV_MitumoriSyohin.Rows[rowindex].FindControl("txtnTANKA") as TextBox).Text.Replace(",", ""));
                TankaGokei = Math.Round(nSURYO * ((nTANKA / 100) * nkakeritsu), 0);
                nSIKIRITANKA = Math.Round((nTANKA / 100) * nkakeritsu, 0);
                (GV_MitumoriSyohin.Rows[rowindex].FindControl("lblTankaGokei") as Label).Text = TankaGokei.ToString("#,##0");
                (GV_MitumoriSyohin.Rows[rowindex].FindControl("lblTankaGokei") as Label).ToolTip = TankaGokei.ToString("#,##0");
                (GV_MitumoriSyohin.Rows[rowindex].FindControl("lblTanka") as Label).Text = nSIKIRITANKA.ToString("#,##0");
                (GV_MitumoriSyohin.Rows[rowindex].FindControl("lblTanka") as Label).ToolTip = nSIKIRITANKA.ToString("#,##0");
            }
            double GenkaGokei = 0;
            if (!String.IsNullOrEmpty((GV_MitumoriSyohin.Rows[rowindex].FindControl("txtnGENKATANKA") as TextBox).Text))
            {
                double nGenka = Double.Parse((GV_MitumoriSyohin.Rows[rowindex].FindControl("txtnGENKATANKA") as TextBox).Text.Replace(",", ""));
                GenkaGokei = Math.Round(nSURYO * nGenka, 0);
                (GV_MitumoriSyohin.Rows[rowindex].FindControl("lblGenkaGokei") as Label).Text = GenkaGokei.ToString("#,##0");
                (GV_MitumoriSyohin.Rows[rowindex].FindControl("lblGenkaGokei") as Label).ToolTip = GenkaGokei.ToString("#,##0");
            }

            if (!String.IsNullOrEmpty((GV_MitumoriSyohin.Rows[rowindex].FindControl("txtnTANKA") as TextBox).Text) && !String.IsNullOrEmpty((GV_MitumoriSyohin.Rows[rowindex].FindControl("txtnGENKATANKA") as TextBox).Text))
            {
                double nArari = TankaGokei - GenkaGokei;
                double nArariSu = (nArari / TankaGokei) * 100;
                if (TankaGokei == 0)
                {
                    nArariSu = 0;
                }
                (GV_MitumoriSyohin.Rows[rowindex].FindControl("lblnARARI") as Label).Text = nArari.ToString("#,##0");
                (GV_MitumoriSyohin.Rows[rowindex].FindControl("lblnARARI") as Label).ToolTip = nArari.ToString("#,##0");
                (GV_MitumoriSyohin.Rows[rowindex].FindControl("lblnARARISu") as Label).Text = nArariSu.ToString("###0.0") + "%";
                (GV_MitumoriSyohin.Rows[rowindex].FindControl("lblnARARISu") as Label).ToolTip = nArariSu.ToString("###0.0") + "%";
            }
            txt_nSURYO.Text = nSURYO.ToString("#,##0.##");
            txt_nSURYO.ToolTip = nSURYO.ToString("#,##0.##");
            //updMitsumoriSyohinGrid.Update();
            HF_isChange.Value = "1";
            //BindAllSyosai();
            KeiSan_CB_nKIRI_G();
            KeiSan_CB_nTANKA_G();
            KeiSan_ARARI();
            updMitsumoriSyohinGrid.Update();
            HasCheckRow();
            updBody.Update();
        }
        #endregion

        #region txtnTANKA_TextChanged  
        protected void txtnTANKA_TextChanged(object sender, EventArgs e)
        {
            var txt_nTanka = sender as TextBox;
            if (txt_nTanka.Text == "")
            {
                txt_nTanka.Text = "0";
            }
            GridViewRow gvr = (GridViewRow)txt_nTanka.NamingContainer;
            int rowindex = gvr.RowIndex;
            double nTANKA = Convert.ToDouble(txt_nTanka.Text.Replace(",", ""));
            Double nkakeritsu = Convert.ToDouble((GV_MitumoriSyohin.Rows[rowindex].FindControl("txtnRITU") as TextBox).Text.Replace("%", ""));
            if (!String.IsNullOrEmpty((GV_MitumoriSyohin.Rows[rowindex].FindControl("txtnSURYO") as TextBox).Text))
            {
                double nSURYO = Double.Parse((GV_MitumoriSyohin.Rows[rowindex].FindControl("txtnSURYO") as TextBox).Text.Replace(",", ""));
                double TankaGokei = Math.Round(nSURYO * ((nTANKA / 100) * nkakeritsu), 0);
                double nSIKIRITANKA = Math.Round((nTANKA / 100) * nkakeritsu, 0);
                (GV_MitumoriSyohin.Rows[rowindex].FindControl("lblTankaGokei") as Label).Text = TankaGokei.ToString("#,##0");
                (GV_MitumoriSyohin.Rows[rowindex].FindControl("lblTankaGokei") as Label).ToolTip = TankaGokei.ToString("#,##0");
                (GV_MitumoriSyohin.Rows[rowindex].FindControl("lblTanka") as Label).Text = nSIKIRITANKA.ToString("#,##0");
                (GV_MitumoriSyohin.Rows[rowindex].FindControl("lblTanka") as Label).ToolTip = nSIKIRITANKA.ToString("#,##0");
                if (!String.IsNullOrEmpty((GV_MitumoriSyohin.Rows[rowindex].FindControl("txtnGENKATANKA") as TextBox).Text))
                {
                    double nGenka = Double.Parse((GV_MitumoriSyohin.Rows[rowindex].FindControl("txtnGENKATANKA") as TextBox).Text.Replace(",", ""));
                    double GenkaGokei = Math.Round(nSURYO * nGenka, 0);
                    (GV_MitumoriSyohin.Rows[rowindex].FindControl("lblGenkaGokei") as Label).Text = GenkaGokei.ToString("#,##0");
                    (GV_MitumoriSyohin.Rows[rowindex].FindControl("lblGenkaGokei") as Label).ToolTip = GenkaGokei.ToString("#,##0");
                    double nArari = TankaGokei - GenkaGokei;
                    double nArariSu = (nArari / TankaGokei) * 100;
                    if (TankaGokei == 0)
                    {
                        nArariSu = 0;
                    }
                    (GV_MitumoriSyohin.Rows[rowindex].FindControl("lblnARARI") as Label).Text = nArari.ToString("#,##0");
                    (GV_MitumoriSyohin.Rows[rowindex].FindControl("lblnARARI") as Label).ToolTip = nArari.ToString("#,##0");
                    (GV_MitumoriSyohin.Rows[rowindex].FindControl("lblnARARISu") as Label).Text = nArariSu.ToString("###0.0") + "%";
                    (GV_MitumoriSyohin.Rows[rowindex].FindControl("lblnARARISu") as Label).ToolTip = nArariSu.ToString("###0.0") + "%";

                }

            }
            txt_nTanka.Text = nTANKA.ToString("#,##0.##");
            txt_nTanka.ToolTip = nTANKA.ToString("#,##0.##");
            (GV_MitumoriSyohin.Rows[rowindex].FindControl("lblnTANKA") as Label).Text = nTANKA.ToString("#,##0.##");
            (GV_MitumoriSyohin.Rows[rowindex].FindControl("lblnTANKA") as Label).ToolTip = nTANKA.ToString("#,##0.##");
            //updMitsumoriSyohinGrid.Update();
            HF_isChange.Value = "1";
            //BindAllSyosai();
            KeiSan_CB_nKIRI_G();
            KeiSan_CB_nTANKA_G();
            KeiSan_ARARI();

            updMitsumoriSyohinGrid.Update();
            HasCheckRow();
            updBody.Update();
        }
        #endregion

        #region txtnRITU_TextChanged  
        protected void txtnRITU_TextChanged(object sender, EventArgs e)
        {
            var txt_nRitu = sender as TextBox;
            if (txt_nRitu.Text == "")
            {
                txt_nRitu.Text = "0%";
            }
            GridViewRow gvr = (GridViewRow)txt_nRitu.NamingContainer;
            int rowindex = gvr.RowIndex;
            Double nkakeritsu = Convert.ToDouble((GV_MitumoriSyohin.Rows[rowindex].FindControl("txtnRITU") as TextBox).Text.Replace("%", ""));
            if (!String.IsNullOrEmpty((GV_MitumoriSyohin.Rows[rowindex].FindControl("txtnSURYO") as TextBox).Text))
            {
                double nSURYO = Double.Parse((GV_MitumoriSyohin.Rows[rowindex].FindControl("txtnSURYO") as TextBox).Text.Replace(",", ""));
                double TankaGokei = 0;
                double nSIKIRITANKA = 0;
                if (!String.IsNullOrEmpty((GV_MitumoriSyohin.Rows[rowindex].FindControl("txtnTANKA") as TextBox).Text))
                {
                    Double nTANKA = Convert.ToDouble((GV_MitumoriSyohin.Rows[rowindex].FindControl("txtnTANKA") as TextBox).Text.Replace(",", ""));
                    TankaGokei = Math.Round(nSURYO * ((nTANKA / 100) * nkakeritsu), 0);
                    nSIKIRITANKA = Math.Round((nTANKA / 100) * nkakeritsu, 0);
                    (GV_MitumoriSyohin.Rows[rowindex].FindControl("lblTankaGokei") as Label).Text = TankaGokei.ToString("#,##0");
                    (GV_MitumoriSyohin.Rows[rowindex].FindControl("lblTankaGokei") as Label).ToolTip = TankaGokei.ToString("#,##0");
                    (GV_MitumoriSyohin.Rows[rowindex].FindControl("lblTanka") as Label).Text = nSIKIRITANKA.ToString("#,##0");
                    (GV_MitumoriSyohin.Rows[rowindex].FindControl("lblTanka") as Label).ToolTip = nSIKIRITANKA.ToString("#,##0");
                }

                if (!String.IsNullOrEmpty((GV_MitumoriSyohin.Rows[rowindex].FindControl("lblTankaGokei") as Label).Text) && !String.IsNullOrEmpty((GV_MitumoriSyohin.Rows[rowindex].FindControl("lblGenkaGokei") as Label).Text))
                {
                    double GenkaGokei = Convert.ToDouble((GV_MitumoriSyohin.Rows[rowindex].FindControl("lblGenkaGokei") as Label).Text.Replace(",", ""));
                    double nArari = TankaGokei - GenkaGokei;
                    double nArariSu = (nArari / TankaGokei) * 100;
                    if (TankaGokei == 0)
                    {
                        nArariSu = 0;
                    }
                    (GV_MitumoriSyohin.Rows[rowindex].FindControl("lblnARARI") as Label).Text = nArari.ToString("#,##0");
                    (GV_MitumoriSyohin.Rows[rowindex].FindControl("lblnARARI") as Label).ToolTip = nArari.ToString("#,##0");
                    (GV_MitumoriSyohin.Rows[rowindex].FindControl("lblnARARISu") as Label).Text = nArariSu.ToString("###0.0") + "%";
                    (GV_MitumoriSyohin.Rows[rowindex].FindControl("lblnARARISu") as Label).ToolTip = nArariSu.ToString("###0.0") + "%";

                }

            }
            txt_nRitu.Text = nkakeritsu.ToString("#,##0") + "%";
            txt_nRitu.ToolTip = nkakeritsu.ToString("#,##0") + "%";
            //updMitsumoriSyohinGrid.Update();
            HF_isChange.Value = "1";
            //BindAllSyosai();
            KeiSan_CB_nKIRI_G();
            KeiSan_CB_nTANKA_G();
            KeiSan_ARARI();

            updMitsumoriSyohinGrid.Update();
            HasCheckRow();
            updBody.Update();
        }
        #endregion

        #region txtnGENKATANKA_TextChanged  
        protected void txtnGENKATANKA_TextChanged(object sender, EventArgs e)
        {
            var txt_nGenka = sender as TextBox;
            if (txt_nGenka.Text == "")
            {
                txt_nGenka.Text = "0";
            }
            GridViewRow gvr = (GridViewRow)txt_nGenka.NamingContainer;
            int rowindex = gvr.RowIndex;
            double nGenka = Double.Parse(txt_nGenka.Text.Replace(",", ""));
            if (!String.IsNullOrEmpty((GV_MitumoriSyohin.Rows[rowindex].FindControl("txtnSURYO") as TextBox).Text))
            {
                double nSURYO = Double.Parse((GV_MitumoriSyohin.Rows[rowindex].FindControl("txtnSURYO") as TextBox).Text.Replace(",", ""));
                double GenkaGokei = Math.Round(nSURYO * nGenka, 0);
                (GV_MitumoriSyohin.Rows[rowindex].FindControl("lblGenkaGokei") as Label).Text = GenkaGokei.ToString("#,##0");
                (GV_MitumoriSyohin.Rows[rowindex].FindControl("lblGenkaGokei") as Label).ToolTip = GenkaGokei.ToString("#,##0");
                if (!String.IsNullOrEmpty((GV_MitumoriSyohin.Rows[rowindex].FindControl("txtnTANKA") as TextBox).Text))
                {
                    double TankaGokei = Convert.ToDouble((GV_MitumoriSyohin.Rows[rowindex].FindControl("lblTankaGokei") as Label).Text.Replace(",", ""));
                    double nArari = TankaGokei - GenkaGokei;
                    double nArariSu = (nArari / TankaGokei) * 100;
                    if (TankaGokei == 0)
                    {
                        nArariSu = 0;
                    }

                    (GV_MitumoriSyohin.Rows[rowindex].FindControl("lblnARARI") as Label).Text = nArari.ToString("#,##0");
                    (GV_MitumoriSyohin.Rows[rowindex].FindControl("lblnARARI") as Label).ToolTip = nArari.ToString("#,##0");
                    (GV_MitumoriSyohin.Rows[rowindex].FindControl("lblnARARISu") as Label).Text = nArariSu.ToString("###0.0") + "%";
                    (GV_MitumoriSyohin.Rows[rowindex].FindControl("lblnARARISu") as Label).ToolTip = nArariSu.ToString("###0.0") + "%";
                }
            }
            txt_nGenka.Text = nGenka.ToString("#,##0.##");
            txt_nGenka.ToolTip = nGenka.ToString("#,##0.##");
            //updMitsumoriSyohinGrid.Update();
            HF_isChange.Value = "1";
            //BindAllSyosai();
            KeiSan_CB_nKIRI_G();
            KeiSan_CB_nTANKA_G();
            KeiSan_ARARI();
            updMitsumoriSyohinGrid.Update();
            HasCheckRow();
            updBody.Update();
        }
        #endregion

        #region 見積詳細コピー
        protected void btnMeisaiCopy_Click(object sender, EventArgs e)
        {
            var btn_syohin = sender as Button;
            GridViewRow gvr = (GridViewRow)btn_syohin.NamingContainer;
            int rowindex = gvr.RowIndex+1;
            DataTable dt_Syosai = GetGridViewData(GV_Syosai);
            DataTable dt = CreateSyouhinTableColomn();
            if (dt_Syosai.Rows.Count > 0)
            {
                int rowNo = Convert.ToInt32((GV_MitumoriMeisai.Rows[rowindex-1].FindControl("lblRowNo") as Label).Text);
                DataRow[] rows = dt_Syosai.Select("rowNo = '" + rowNo+ "'");
                if (rows.Length > 0)
                {
                    foreach (var drow in rows)
                    {
                        DataRow dr = dt.NewRow();
                        dr[0] = drow[0].ToString();
                        dr[1] = drow[1].ToString();
                        dr[2] = drow[2].ToString();
                        dr[3] = drow[3].ToString();
                        dr[4] = drow[4].ToString();
                        dr[5] = drow[5].ToString();
                        dr[6] = drow[6].ToString();
                        dr[7] = drow[7].ToString();
                        dr[8] = drow[8].ToString();
                        dr[9] = drow[9].ToString();
                        dr[10] = drow[10].ToString();
                        dr[11] = HF_selectRowIndex.Value;
                        dr[12] = drow[12].ToString();
                        dr[13] = drow[13].ToString();
                        dr[14] = drow[14].ToString();
                        dt.Rows.Add(dr);
                    }

                    Session["SyosaiCopydt"] = dt;

                    SessionUtility.SetSession("HOME", "Popup");
                    String id = Request.QueryString["id"];
                    ifShinkiPopup.Src = "JC33MitumoriSyosaiCopy.aspx?id=" + id;
                    mpeShinkiPopup.Show();

                    updShinkiPopup.Update();
                }
            }

            

        }
        #endregion

        #region btn_CloseSyosaiCopy_Click
        protected void btn_CloseSyosaiCopy_Click(object sender, EventArgs e)
        {
            ifShinkiPopup.Src = "";
            mpeShinkiPopup.Hide();
            updShinkiPopup.Update();
        }
        #endregion

        #region btn_SelectSyosaiCopy_Click
        protected void btn_SelectSyosaiCopy_Click(object sender, EventArgs e)
        {
            if (Session["SyosaiCopydt"] != null)
            {
                DataTable dt_Syosai = new DataTable();
                dt_Syosai = Session["SyosaiCopydt"] as DataTable;

                DataTable dt_Syosai1 = GetSyosaiGridViewData(GV_MitumoriSyohin);
                dt_Syosai1.Merge(dt_Syosai);
                dt_Syosai1.AcceptChanges();

                DataTable dt_Syosai2 = GetGridViewData(GV_Syosai);
                if (dt_Syosai2.Rows.Count > 0)
                {
                    DataRow[] rows = dt_Syosai2.Select("rowNo = '" + HF_selectRowIndex.Value + "'");
                    if (rows.Length > 0)
                    {
                        foreach (var drow in rows)
                        {
                            drow.Delete();
                        }
                        dt_Syosai2.AcceptChanges();
                    }
                }
                dt_Syosai2.Merge(dt_Syosai1);
                dt_Syosai2.DefaultView.Sort = "rowNo asc";
                dt_Syosai2.AcceptChanges();

                int rowindex = Convert.ToInt32(HF_selectRowIndex.Value) - 1;
                String nRitu = (GV_MitumoriMeisai.Rows[rowindex].FindControl("lblMeisai_nRITU") as Label).Text;
                while (dt_Syosai1.Rows.Count < GV_MitumoriSyohin.Rows.Count)
                {
                    DataRow dr = dt_Syosai1.NewRow();
                    dr[0] = "";
                    dr[1] = "";
                    dr[2] = "";
                    dr[3] = "";
                    dr[4] = "";
                    dr[5] = "";
                    dr[6] = "";
                    dr[7] = "";
                    dr[8] = "";
                    dr[9] = "";
                    dr[10] = "";
                    dr[11] = "";
                    dr[12] = nRitu;
                    dr[13] = "";
                    dr[14] = "1";
                    dt_Syosai1.Rows.Add(dr);
                }
                GV_MitumoriSyohin.DataSource = dt_Syosai1;
                GV_MitumoriSyohin.DataBind();
                //GV_Syosai.DataSource = dt_Syosai2;
                //GV_Syosai.DataBind();
            }

            //BindAllSyosai();
            KeiSan_CB_nKIRI_G();
            KeiSan_CB_nTANKA_G();
            KeiSan_ARARI();
            HF_isChange.Value = "1";
            ifShinkiPopup.Src = "";
            mpeShinkiPopup.Hide();
            updShinkiPopup.Update();
            HasCheckRow();
            updBody.Update();
        }
        #endregion

        #region BindAllSyosai
        private void BindAllSyosai()
        {
            DataTable dt_Syosai = GetGridViewData(GV_Syosai);
            int rowindex = Convert.ToInt32(HF_selectRowIndex.Value);

            if (dt_Syosai.Rows.Count > 0)
            {
                int rowNo = Convert.ToInt32((GV_MitumoriMeisai.Rows[rowindex-1].FindControl("lblRowNo") as Label).Text);
                if (rowNo == 0)
                {
                    DataTable dt_Meisai = GetMeisaiGridViewData();
                    var max = dt_Meisai.AsEnumerable().Max(x => int.Parse(x.Field<string>("rowNo")));
                    max += 1;
                    (GV_MitumoriMeisai.Rows[rowindex - 1].FindControl("lblRowNo") as Label).Text = max.ToString();
                    updMitsumoriMeisaiGrid.Update();
                    rowNo = max;
                }
                DataRow[] rows = dt_Syosai.Select("rowNo = '" + rowNo + "'");
                if (rows.Length > 0)
                {
                    foreach (var drow in rows)
                    {
                        drow.Delete();
                    }
                    dt_Syosai.AcceptChanges();
                }
            }

            DataTable dt_Syosai1 = GetSyosaiGridViewData(GV_MitumoriSyohin);
            if (dt_Syosai1.Rows.Count > 0)
            {
                (GV_MitumoriMeisai.Rows[rowindex - 1].FindControl("btnMeisaiCopy") as Button).CssClass = "JC10GrayButton";
                (GV_MitumoriMeisai.Rows[rowindex - 1].FindControl("btnMeisaiCopy") as Button).Enabled = true;
            }
            else
            {
                (GV_MitumoriMeisai.Rows[rowindex - 1].FindControl("btnMeisaiCopy") as Button).CssClass = "JC10DisableButton";
                (GV_MitumoriMeisai.Rows[rowindex - 1].FindControl("btnMeisaiCopy") as Button).Enabled = false;
            }
            dt_Syosai.Merge(dt_Syosai1);
            dt_Syosai.DefaultView.Sort = "rowNo asc";
            dt_Syosai.AcceptChanges();

            GV_Syosai.DataSource = dt_Syosai;
            GV_Syosai.DataBind();
            updBody.Update();
        }
        #endregion

        #region グリッド行の並び替え
        protected void BT_Sort_Click(object sender, EventArgs e)
        {
            DataTable dt = GetGridViewData(GV_MitumoriSyohin);
            var dr_copy = dt.NewRow();
            int before_index = Convert.ToInt32(HF_beforeSortIndex.Value) - 1;
            int after_index = Convert.ToInt32(HF_afterSortIndex.Value) - 1;
            DataRow dr = dt.Rows[before_index];
            dr_copy.ItemArray = dr.ItemArray.Clone() as object[];
            dt.Rows.RemoveAt(before_index);
            dt.Rows.InsertAt(dr_copy, after_index);
            GV_MitumoriSyohin.DataSource = dt;
            GV_MitumoriSyohin.DataBind();
            HF_isChange.Value = "1";
            HasCheckRow();
            updBody.Update();
            //BindAllSyosai();
        }
        #endregion

        #region 詳細複数コピー
        protected void btnFukusuCopy_Click(object sender, EventArgs e)
        {
            DataTable dt_Syosai1 = CreateSyouhinTableColomn();
            foreach (GridViewRow row in GV_MitumoriSyohin.Rows)
            {
                CheckBox ck= (row.FindControl("chkSelectSyouhin") as CheckBox);
                Label lbl_status = (row.FindControl("lblhdnStatus") as Label);
                Label lbl_rowno = (row.FindControl("lblRowNo") as Label);
                TextBox txt_cSyohin = (row.FindControl("txtcSYOHIN") as TextBox);
                TextBox txt_sSyohin = (row.FindControl("txtsSYOHIN") as TextBox);
                TextBox txt_nSyoryo = (row.FindControl("txtnSURYO") as TextBox);
                //DropDownList ddl_cTani = (row.FindControl("DDL_cTANI") as DropDownList);
                TextBox txt_cTani = (row.FindControl("txtTani") as TextBox);
                Label lbl_cTani = (row.FindControl("lblcTANI") as Label);
                TextBox txt_nTanka = (row.FindControl("txtnTANKA") as TextBox);
                Label lbl_TankaGokei = (row.FindControl("lblTankaGokei") as Label);
                TextBox txt_nGenkaTanka = (row.FindControl("txtnGENKATANKA") as TextBox);
                Label lbl_GenkaGokei = (row.FindControl("lblGenkaGokei") as Label);
                Label lbl_Arari = (row.FindControl("lblnARARI") as Label);
                Label lbl_ArariSu = (row.FindControl("lblnARARISu") as Label);
                TextBox txt_nRitu = (row.FindControl("txtnRITU") as TextBox);
                Label lbl_nSIKIRITANKA = (row.FindControl("lblTanka") as Label);
                Label lbl_fJITAIS = (row.FindControl("lblfjitais") as Label);

                if (ck.Checked)
                {
                    DataRow dr = dt_Syosai1.NewRow();
                    dr[0] = lbl_status.Text;
                    dr[1] = txt_cSyohin.Text;
                    dr[2] = txt_sSyohin.Text;
                    dr[3] = txt_nSyoryo.Text;
                    dr[4] = lbl_cTani.Text;
                    dr[5] = txt_nTanka.Text;
                    dr[6] = lbl_TankaGokei.Text;
                    dr[7] = txt_nGenkaTanka.Text;
                    dr[8] = lbl_GenkaGokei.Text;
                    dr[9] = lbl_Arari.Text;
                    dr[10] = lbl_ArariSu.Text;
                    dr[11] = HF_selectRowIndex.Value;
                    dr[12] =txt_nRitu.Text;
                    dr[13] = lbl_nSIKIRITANKA.Text;
                    dr[14] = lbl_fJITAIS.Text;
                    dt_Syosai1.Rows.Add(dr);
                }
            }
            if (dt_Syosai1.Rows.Count > 0)
            {
                DataTable dt_Syosai = GetSyosaiGridViewData(GV_MitumoriSyohin);
                dt_Syosai.Merge(dt_Syosai1);
                dt_Syosai.AcceptChanges();

                int rowindex = Convert.ToInt32(HF_selectRowIndex.Value) - 1;
                String nRitu = (GV_MitumoriMeisai.Rows[rowindex].FindControl("lblMeisai_nRITU") as Label).Text;
                while (dt_Syosai.Rows.Count < GV_MitumoriSyohin.Rows.Count)
                {
                    DataRow dr = dt_Syosai.NewRow();
                    dr[0] = "";
                    dr[1] = "";
                    dr[2] = "";
                    dr[3] = "";
                    dr[4] = "";
                    dr[5] = "";
                    dr[6] = "";
                    dr[7] = "";
                    dr[8] = "";
                    dr[9] = "";
                    dr[10] = "";
                    dr[11] = "";
                    dr[12] = nRitu;
                    dr[13] = "";
                    dr[14] = "1";
                    dt_Syosai.Rows.Add(dr);
                }

                GV_MitumoriSyohin.DataSource = dt_Syosai;
                GV_MitumoriSyohin.DataBind();
                HF_isChange.Value = "1";
                //BindAllSyosai();
                KeiSan_CB_nKIRI_G();
                KeiSan_CB_nTANKA_G();
                KeiSan_ARARI();
                HasCheckRow();
                updBody.Update();
            }
        }
        #endregion

        #region btnPopupClose()  選択サブ画面を閉じる処理
        protected void btnClose_Click(object sender, EventArgs e)
        {
            //ifSentakuPopup.Src = "";
            //mpeSentakuPopup.Hide();
            //updSentakuPopup.Update();
            ifShinkiPopup1.Src = "";
            mpeShinkiPopup1.Hide();
            updShinkiPopup1.Update();
        }
        #endregion

        #region 商品を選択
        protected void btnSyohinSelect_Click(object sender, EventArgs e)
        {
            var btn_syohin = sender as Button;
            GridViewRow gvr = (GridViewRow)btn_syohin.NamingContainer;
            int rowindex = gvr.RowIndex;
            Session["syohinSelectRowIndex"] = rowindex.ToString();

            SessionUtility.SetSession("HOME", "Popup");
            //ifSentakuPopup.Style["width"] = "1300px";
            //ifSentakuPopup.Style["height"] = "675px";
            //ifSentakuPopup.Src = "JC21SyouhinKensaku.aspx";
            //mpeSentakuPopup.Show();

            //updSentakuPopup.Update();

            String id = Request.QueryString["id"];
            ifShinkiPopup1.Src = "JC21SyouhinKensaku.aspx?id=" + id;
            mpeShinkiPopup1.Show();
            updShinkiPopup1.Update();

        }
        #endregion

        #region btnSyohinGridSelect_Click  商品サブ画面を閉じる時のフォーカス処理
        protected void btnSyohinGridSelect_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["cSyohin"] != null)
                {
                    string cSYOHIN = (string)Session["cSyohin"];
                    int rowindex = Convert.ToInt32((string)Session["syohinSelectRowIndex"]);
                    Session["syohinSelectRowIndex"] = null;
                    (GV_MitumoriSyohin.Rows[rowindex].FindControl("txtcSYOHIN") as TextBox).Text = cSYOHIN;
                    if (cSYOHIN != "")
                    {
                        cSYOHIN = cSYOHIN.ToString().PadLeft(10, '0');
                        (GV_MitumoriSyohin.Rows[rowindex].FindControl("txtcSYOHIN") as TextBox).Text = cSYOHIN;

                        String id = Request.QueryString["id"];
                        DataTable dt_db = JC01Login_Class.Get_DB(id);
                        String db = dt_db.Rows[0]["db"].ToString();
                        String mail = dt_db.Rows[0]["mail"].ToString();
                        ConstantVal.DB_NAME = db;

                        JC_ClientConnecction_Class jc = new JC_ClientConnecction_Class();
                        jc.loginId = mail;
                        MySqlConnection cn = jc.GetConnection();
                        cn.Open();
                        string sql_syouhin = " ";
                        sql_syouhin = " Select " +
                                      " ms.cSYOUHIN as cSYOUHIN," +
                                      " IfNull(ms.sSYOUHIN, '') as sSYOUHIN," +
                                      " IfNull(ms.sSHIYOU, '') as sSHIYOU, " +
                                      " IfNull(ms.sTANI,'') as sTANI, " +
                                      " format(ifnull(ms.nHANNBAIKAKAKU, 0),2) as nHANNBAIKAKAKU," +
                                      " Format(Round((ifnull(ms.nHANNBAIKAKAKU, 0))*(ifnull(ms.nSYOUKISU, 1)),0),0) as TankaGokei," +
                                      " Format(ifnull(ms.nSHIIREKAKAKU, 0),2) as nSHIIREKAKAKU," +
                                      " Format(Round((ifnull(ms.nSHIIREKAKAKU, 0))*(ifnull(ms.nSYOUKISU, 1)),0),0) as GenkaGokei," +
                                      " format(Round((ifnull(ms.nHANNBAIKAKAKU, 0))*(ifnull(ms.nSYOUKISU, 1)),0)-Round((ifnull(ms.nSHIIREKAKAKU, 0))*(ifnull(ms.nSYOUKISU, 1)),0),0) as nARARI," +
                                      " CONCAT(FORMAT(IfNull((Round((ifnull(ms.nHANNBAIKAKAKU, 0))*(ifnull(ms.nSYOUKISU, 1)),0)-Round((ifnull(ms.nSHIIREKAKAKU, 0))*(ifnull(ms.nSYOUKISU, 1)),0))/(Round((ifnull(ms.nHANNBAIKAKAKU, 0))*(ifnull(ms.nSYOUKISU, 1)),0)),0)*100, 1),'%') As nARARISu," +
                                      " ifnull(ms.nSYOUKISU, 1) as nSYOUKISU," +
                                      " MSD.sSYOUHIN_DAIGRP as sSYOUHIN_DAIGRP," +
                                      " MST.sSYOUHIN_TYUUGRP as sSYOUHIN_TYUUGRP," +
                                      " ifnull(ms.sBIKOU, '') as sBIKOU," +
                                      " ifnull(ms.fJITAIS, '0') as fJITAIS" +
                                      " From m_syouhin ms " +
                                      " left join" +
                                      " M_SYOUHIN_DAIGRP MSD ON MSD.cSYOUHIN_DAIGRP = ms.cSYOUHIN_DAIGRP" +
                                      " left join " +
                                      " M_SYOUHIN_TYUUGRP MST ON ms.cSYOUHIN_TYUUGRP = MST.cSYOUHIN_TYUUGRP" +
                                      " Where (ms.fHAIBAN <> '1' or ms.fHAIBAN is null) and '1' = '1' and ms.cSYOUHIN like '%" + cSYOHIN + "%'; ";


                        MySqlCommand cmd = new MySqlCommand(sql_syouhin, cn);
                        MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                        DataTable dtable = new DataTable();
                        da.Fill(dtable);
                        cn.Close();
                        da.Dispose();
                        if (dtable.Rows.Count > 0)
                        {
                            (GV_MitumoriSyohin.Rows[rowindex].FindControl("txtsSYOHIN") as TextBox).Text = dtable.Rows[0]["sSYOUHIN"].ToString() + "　" + dtable.Rows[0]["sSHIYOU"].ToString();
                            (GV_MitumoriSyohin.Rows[rowindex].FindControl("txtsSYOHIN") as TextBox).ToolTip = dtable.Rows[0]["sSYOUHIN"].ToString() + "　" + dtable.Rows[0]["sSHIYOU"].ToString();

                            (GV_MitumoriSyohin.Rows[rowindex].FindControl("lblfjitais") as Label).Text = dtable.Rows[0]["fJITAIS"].ToString();
                            if (dtable.Rows[0]["fJITAIS"].ToString() == "0")
                            {
                                (GV_MitumoriSyohin.Rows[rowindex].FindControl("LK_Syou") as LinkButton).BackColor = System.Drawing.Color.White;
                                (GV_MitumoriSyohin.Rows[rowindex].FindControl("txtsSYOHIN") as TextBox).MaxLength = 50;
                            }
                            else
                            {
                                (GV_MitumoriSyohin.Rows[rowindex].FindControl("LK_Syou") as LinkButton).BackColor = ColorTranslator.FromHtml("#f2f2f2");
                                (GV_MitumoriSyohin.Rows[rowindex].FindControl("txtsSYOHIN") as TextBox).MaxLength = 60;
                            }

                            double nSuryo = 0;
                            if (!String.IsNullOrEmpty(dtable.Rows[0]["nSYOUKISU"].ToString()))
                            {
                                nSuryo = Convert.ToDouble(dtable.Rows[0]["nSYOUKISU"].ToString());
                            }

                        (GV_MitumoriSyohin.Rows[rowindex].FindControl("txtnSURYO") as TextBox).Text = nSuryo.ToString("#,##0.##");
                            (GV_MitumoriSyohin.Rows[rowindex].FindControl("txtnSURYO") as TextBox).ToolTip = nSuryo.ToString("#,##0.##");

                            (GV_MitumoriSyohin.Rows[rowindex].FindControl("lblcTANI") as Label).Text = dtable.Rows[0]["sTANI"].ToString();
                            (GV_MitumoriSyohin.Rows[rowindex].FindControl("lblcTANI") as Label).ToolTip = dtable.Rows[0]["sTANI"].ToString();
                            (GV_MitumoriSyohin.Rows[rowindex].FindControl("txtTani") as TextBox).Text = dtable.Rows[0]["sTANI"].ToString();
                            string getsTANI = " select distinct cTANI, sTANI from M_TANI order by cTANI ";
                            MySqlCommand cmd1 = new MySqlCommand(getsTANI, cn);
                            MySqlDataAdapter da1 = new MySqlDataAdapter(cmd1);
                            DataTable dt = new DataTable();
                            da1.Fill(dt);
                            cn.Close();
                            da1.Dispose();
                            DropDownList DropDownList1 = (GV_MitumoriSyohin.Rows[rowindex].FindControl("DDL_cTANI") as DropDownList);
                            DropDownList1.DataSource = dt;
                            DropDownList1.DataTextField = "sTANI";
                            DropDownList1.DataValueField = "sTANI";
                            DropDownList1.DataBind();
                            string lblcTANI = (GV_MitumoriSyohin.Rows[rowindex].FindControl("lblcTANI") as Label).Text;
                            DropDownList1.Items.Insert(0, new ListItem(" ", "00"));
                            try
                            {
                                DropDownList1.Items.FindByText(lblcTANI).Selected = true;
                            }
                            catch { }

                            Double nkakeritsu = Convert.ToDouble((GV_MitumoriSyohin.Rows[rowindex].FindControl("txtnRITU") as TextBox).Text.Replace("%", ""));
                            double nHanbaikakaku = 0;
                            Double nTankaGokei = 0;
                            Double nSIKIRITANKA = 0;
                            if (!String.IsNullOrEmpty(dtable.Rows[0]["nHANNBAIKAKAKU"].ToString()))
                            {
                                nHanbaikakaku = Convert.ToDouble(dtable.Rows[0]["nHANNBAIKAKAKU"].ToString());
                                nTankaGokei = ((nHanbaikakaku / 100) * nkakeritsu) * nSuryo;
                                nSIKIRITANKA = (nHanbaikakaku / 100) * nkakeritsu;
                            }

                            double nSHIIREKAKAKU = 0;
                            double nGenkaGokei = 0;
                            if (!String.IsNullOrEmpty(dtable.Rows[0]["nSHIIREKAKAKU"].ToString()))
                            {
                                nSHIIREKAKAKU = Convert.ToDouble(dtable.Rows[0]["nSHIIREKAKAKU"].ToString());
                                nGenkaGokei = nSHIIREKAKAKU * nSuryo;
                            }

                            Double nArari = nTankaGokei - nGenkaGokei;

                            double nArariSu = (nArari / nTankaGokei) * 100;
                            if (nTankaGokei == 0)
                            {
                                nArariSu = 0;
                            }

                            if (btn_genka.CssClass== "JC10ZeikomiBtnActive")
                            {
                                (GV_MitumoriSyohin.Rows[rowindex].FindControl("txtnTANKA") as TextBox).Text = "0";
                                (GV_MitumoriSyohin.Rows[rowindex].FindControl("lblnTANKA") as Label).Text = "0";
                                (GV_MitumoriSyohin.Rows[rowindex].FindControl("lblTankaGokei") as Label).Text = "0";
                                (GV_MitumoriSyohin.Rows[rowindex].FindControl("lblTanka") as Label).Text = "0";
                                (GV_MitumoriSyohin.Rows[rowindex].FindControl("txtnTANKA") as TextBox).ToolTip = "0";
                                (GV_MitumoriSyohin.Rows[rowindex].FindControl("lblnTANKA") as Label).ToolTip = "0";
                                (GV_MitumoriSyohin.Rows[rowindex].FindControl("lblTankaGokei") as Label).ToolTip = "0";
                                (GV_MitumoriSyohin.Rows[rowindex].FindControl("lblTanka") as Label).ToolTip = "0";

                                (GV_MitumoriSyohin.Rows[rowindex].FindControl("txtnGENKATANKA") as TextBox).Text = nSHIIREKAKAKU.ToString("#,##0.##");
                                (GV_MitumoriSyohin.Rows[rowindex].FindControl("lblGenkaGokei") as Label).Text = nGenkaGokei.ToString("#,##0");
                                (GV_MitumoriSyohin.Rows[rowindex].FindControl("lblnARARI") as Label).Text = nArari.ToString("#,##0");
                                (GV_MitumoriSyohin.Rows[rowindex].FindControl("lblnARARISu") as Label).Text = "0.0%";


                                (GV_MitumoriSyohin.Rows[rowindex].FindControl("txtnGENKATANKA") as TextBox).ToolTip = nSHIIREKAKAKU.ToString("#,##0.##");
                                (GV_MitumoriSyohin.Rows[rowindex].FindControl("lblGenkaGokei") as Label).ToolTip = nGenkaGokei.ToString("#,##0");
                                (GV_MitumoriSyohin.Rows[rowindex].FindControl("lblnARARI") as Label).ToolTip = nArari.ToString("#,##0");
                                (GV_MitumoriSyohin.Rows[rowindex].FindControl("lblnARARISu") as Label).ToolTip = "0.0%";
                            }
                            else
                            {
                                (GV_MitumoriSyohin.Rows[rowindex].FindControl("txtnTANKA") as TextBox).Text = nHanbaikakaku.ToString("#,##0.##");
                                (GV_MitumoriSyohin.Rows[rowindex].FindControl("lblnTANKA") as Label).Text = nHanbaikakaku.ToString("#,##0.##");
                                (GV_MitumoriSyohin.Rows[rowindex].FindControl("lblTankaGokei") as Label).Text = nTankaGokei.ToString("#,##0");
                                (GV_MitumoriSyohin.Rows[rowindex].FindControl("lblTanka") as Label).Text = nSIKIRITANKA.ToString("#,##0");
                                (GV_MitumoriSyohin.Rows[rowindex].FindControl("txtnTANKA") as TextBox).ToolTip = nHanbaikakaku.ToString("#,##0.##");
                                (GV_MitumoriSyohin.Rows[rowindex].FindControl("lblnTANKA") as Label).ToolTip = nHanbaikakaku.ToString("#,##0.##");
                                (GV_MitumoriSyohin.Rows[rowindex].FindControl("lblTankaGokei") as Label).ToolTip = nTankaGokei.ToString("#,##0");
                                (GV_MitumoriSyohin.Rows[rowindex].FindControl("lblTanka") as Label).ToolTip = nSIKIRITANKA.ToString("#,##0");

                                (GV_MitumoriSyohin.Rows[rowindex].FindControl("txtnGENKATANKA") as TextBox).Text = nSHIIREKAKAKU.ToString("#,##0.##");
                                (GV_MitumoriSyohin.Rows[rowindex].FindControl("lblGenkaGokei") as Label).Text = nGenkaGokei.ToString("#,##0");
                                (GV_MitumoriSyohin.Rows[rowindex].FindControl("lblnARARI") as Label).Text = nArari.ToString("#,##0");
                                (GV_MitumoriSyohin.Rows[rowindex].FindControl("lblnARARISu") as Label).Text = nArariSu.ToString("###0.0") + "%";


                                (GV_MitumoriSyohin.Rows[rowindex].FindControl("txtnGENKATANKA") as TextBox).ToolTip = nSHIIREKAKAKU.ToString("#,##0.##");
                                (GV_MitumoriSyohin.Rows[rowindex].FindControl("lblGenkaGokei") as Label).ToolTip = nGenkaGokei.ToString("#,##0");
                                (GV_MitumoriSyohin.Rows[rowindex].FindControl("lblnARARI") as Label).ToolTip = nArari.ToString("#,##0");
                                (GV_MitumoriSyohin.Rows[rowindex].FindControl("lblnARARISu") as Label).ToolTip = nArariSu.ToString("###0.0") + "%";
                            }


                        }
                        else
                        {
                            (GV_MitumoriSyohin.Rows[rowindex].FindControl("txtcSYOHIN") as TextBox).Text = cSYOHIN;
                            (GV_MitumoriSyohin.Rows[rowindex].FindControl("txtcSYOHIN") as TextBox).ToolTip = cSYOHIN;
                        }
                    }


                    //BindAllSyosai();
                    KeiSan_CB_nKIRI_G();
                    KeiSan_CB_nTANKA_G();
                    KeiSan_ARARI();

                    updMitsumoriSyohinGrid.Update();
                }
                HF_isChange.Value = "1";
                //ifSentakuPopup.Src = "";
                //mpeSentakuPopup.Hide();
                //updSentakuPopup.Update();
                ifShinkiPopup1.Src = "";
                mpeShinkiPopup1.Hide();
                updShinkiPopup1.Update();
                HasCheckRow();
                updBody.Update();
            }
            catch
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "parentButtonClick('btnToLogin','" + hdnHome.Value + "');", true);
            }
        }
        #endregion

        #region 総原価方式クリック

        #region toDelete
        //protected void RB_genka_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (RB_genka.Checked)
        //    {
        //        RB_gentanka.Checked = false;
        //        //btn_genka.CssClass = "JC11GenkaHouShikiActiveBtn";
        //        //btn_gentanka.CssClass = "JC11GenkaHouShikiBtn";
        //        Int32 rowindex = Convert.ToInt32(HF_selectRowIndex.Value);
        //        (GV_MitumoriMeisai.Rows[rowindex - 1].FindControl("lblfgenkatanka") as Label).Text = "1";
        //        String nRitu = (GV_MitumoriMeisai.Rows[rowindex - 1].FindControl("lblMeisai_nRITU") as Label).Text;
        //        DataTable dt_Syosai = GetSyosaiGridViewData(GV_MitumoriSyohin);
        //        int rowCount = dt_Syosai.Rows.Count;
        //        while (dt_Syosai.Rows.Count < GV_MitumoriSyohin.Rows.Count)
        //        {
        //            DataRow dr = dt_Syosai.NewRow();
        //            dr[0] = "";
        //            dr[1] = "";
        //            dr[2] = "";
        //            dr[3] = "";
        //            dr[4] = "";
        //            dr[5] = "";
        //            dr[6] = "";
        //            dr[7] = "";
        //            dr[8] = "";
        //            dr[9] = "";
        //            dr[10] = "";
        //            dr[11] = "";
        //            dr[12] = nRitu;
        //            dr[13] = "";
        //            dt_Syosai.Rows.Add(dr);
        //        }
        //        GV_MitumoriSyohin.DataSource = dt_Syosai;
        //        GV_MitumoriSyohin.DataBind();
        //        HF_isChange.Value = "1";
        //        //BindAllSyosai();
        //        if (rowCount == 0)
        //        {
        //            if ((GV_MitumoriMeisai.Rows[rowindex - 1].FindControl("lblMeisai_nTanka") as Label).Text != "0" && !String.IsNullOrEmpty((GV_MitumoriMeisai.Rows[rowindex - 1].FindControl("lblMeisai_nTanka") as Label).Text))
        //            {
        //                txtHyouJunTankaItem.Text = (GV_MitumoriMeisai.Rows[rowindex - 1].FindControl("lblMeisai_nTanka") as Label).Text;
        //            }
        //            else
        //            {
        //                txtHyouJunTankaItem.Text = "";
        //            }

        //            if ((GV_MitumoriMeisai.Rows[rowindex - 1].FindControl("lblMeisai_nTanka") as Label).Text != "0" && !String.IsNullOrEmpty((GV_MitumoriMeisai.Rows[rowindex - 1].FindControl("lblMeisai_nTanka") as Label).Text))
        //            {
        //                txtTankaItem.Text = (GV_MitumoriMeisai.Rows[rowindex - 1].FindControl("lblMeisai_nTanka") as Label).Text;
        //            }
        //            else
        //            {
        //                txtTankaItem.Text = "";
        //            }

        //            if ((GV_MitumoriMeisai.Rows[rowindex - 1].FindControl("lblMeisai_nTANKAGOUKEI") as Label).Text != "0" && !String.IsNullOrEmpty((GV_MitumoriMeisai.Rows[rowindex - 1].FindControl("lblMeisai_nTANKAGOUKEI") as Label).Text))
        //            {
        //                txtGokeiKingakuItem.Text = (GV_MitumoriMeisai.Rows[rowindex - 1].FindControl("lblMeisai_nTANKAGOUKEI") as Label).Text;
        //            }
        //            else
        //            {
        //                txtGokeiKingakuItem.Text = "";
        //            }
        //            if ((GV_MitumoriMeisai.Rows[rowindex - 1].FindControl("lblMeisai_nGENKATANKA") as Label).Text != "0" && !String.IsNullOrEmpty((GV_MitumoriMeisai.Rows[rowindex - 1].FindControl("lblMeisai_nGENKATANKA") as Label).Text))
        //            {
        //                txtGenTankaItem.Text = (GV_MitumoriMeisai.Rows[rowindex - 1].FindControl("lblMeisai_nGENKATANKA") as Label).Text;
        //            }
        //            else
        //            {
        //                txtGenTankaItem.Text = "";
        //            }
        //            if ((GV_MitumoriMeisai.Rows[rowindex - 1].FindControl("lblMeisai_nGENKAGOUKEI") as Label).Text != "0" && !String.IsNullOrEmpty((GV_MitumoriMeisai.Rows[rowindex - 1].FindControl("lblMeisai_nGENKAGOUKEI") as Label).Text))
        //            {
        //                txtGenkaItem.Text = (GV_MitumoriMeisai.Rows[rowindex - 1].FindControl("lblMeisai_nGENKAGOUKEI") as Label).Text;
        //            }
        //            else
        //            {
        //                txtGenkaItem.Text = "";
        //            }
        //            if ((GV_MitumoriMeisai.Rows[rowindex - 1].FindControl("lblMeisai_nARARI") as Label).Text != "0" && !String.IsNullOrEmpty((GV_MitumoriMeisai.Rows[rowindex - 1].FindControl("lblMeisai_nARARI") as Label).Text))
        //            {
        //                txtArariItem.Text = (GV_MitumoriMeisai.Rows[rowindex - 1].FindControl("lblMeisai_nARARI") as Label).Text;
        //            }
        //            else
        //            {
        //                txtArariItem.Text = "0";
        //            }
        //            if ((GV_MitumoriMeisai.Rows[rowindex - 1].FindControl("lblMeisai_nARARISu") as Label).Text != "0" && !String.IsNullOrEmpty((GV_MitumoriMeisai.Rows[rowindex - 1].FindControl("lblMeisai_nARARISu") as Label).Text))
        //            {
        //                txtArariSuItem.Text = (GV_MitumoriMeisai.Rows[rowindex - 1].FindControl("lblMeisai_nARARISu") as Label).Text;
        //            }
        //            else
        //            {
        //                txtArariSuItem.Text = "";
        //            }
        //        }
        //        else
        //        {
        //            KeiSan_CB_nKIRI_G();
        //            KeiSan_CB_nTANKA_G();
        //            KeiSan_ARARI();
        //        }

        //        HasCheckRow();
        //        updBody.Update();
        //    }
        //}
        #endregion


        protected void btn_genka_Click(object sender, EventArgs e)
        {
            btn_genka.CssClass = "JC10ZeikomiBtnActive";
            btn_gentanka.CssClass = "JC10ZeikomiBtn";
            Int32 rowindex = Convert.ToInt32(HF_selectRowIndex.Value);
            (GV_MitumoriMeisai.Rows[rowindex - 1].FindControl("lblfgenkatanka") as Label).Text = "1";
            String nRitu = (GV_MitumoriMeisai.Rows[rowindex - 1].FindControl("lblMeisai_nRITU") as Label).Text;
            DataTable dt_Syosai = GetSyosaiGridViewData(GV_MitumoriSyohin);
            int rowCount = dt_Syosai.Rows.Count;
            while (dt_Syosai.Rows.Count < GV_MitumoriSyohin.Rows.Count)
            {
                DataRow dr = dt_Syosai.NewRow();
                dr[0] = "";
                dr[1] = "";
                dr[2] = "";
                dr[3] = "";
                dr[4] = "";
                dr[5] = "";
                dr[6] = "";
                dr[7] = "";
                dr[8] = "";
                dr[9] = "";
                dr[10] = "";
                dr[11] = "";
                dr[12] = nRitu;
                dr[13] = "";
                dr[14] = "1";
                dt_Syosai.Rows.Add(dr);
            }
            GV_MitumoriSyohin.DataSource = dt_Syosai;
            GV_MitumoriSyohin.DataBind();
            HF_isChange.Value = "1";
            //BindAllSyosai();
            if (rowCount == 0)
            {
                if ((GV_MitumoriMeisai.Rows[rowindex - 1].FindControl("lblMeisai_nTanka") as Label).Text != "0" && !String.IsNullOrEmpty((GV_MitumoriMeisai.Rows[rowindex - 1].FindControl("lblMeisai_nTanka") as Label).Text))
                {
                    txtHyouJunTankaItem.Text = (GV_MitumoriMeisai.Rows[rowindex - 1].FindControl("lblMeisai_nTanka") as Label).Text;
                }
                else
                {
                    txtHyouJunTankaItem.Text = "";
                }

                if ((GV_MitumoriMeisai.Rows[rowindex - 1].FindControl("lblMeisai_nTanka") as Label).Text != "0" && !String.IsNullOrEmpty((GV_MitumoriMeisai.Rows[rowindex - 1].FindControl("lblMeisai_nTanka") as Label).Text))
                {
                    txtTankaItem.Text = (GV_MitumoriMeisai.Rows[rowindex - 1].FindControl("lblMeisai_nTanka") as Label).Text;
                }
                else
                {
                    txtTankaItem.Text = "";
                }

                if ((GV_MitumoriMeisai.Rows[rowindex - 1].FindControl("lblMeisai_nTANKAGOUKEI") as Label).Text != "0" && !String.IsNullOrEmpty((GV_MitumoriMeisai.Rows[rowindex - 1].FindControl("lblMeisai_nTANKAGOUKEI") as Label).Text))
                {
                    txtGokeiKingakuItem.Text = (GV_MitumoriMeisai.Rows[rowindex - 1].FindControl("lblMeisai_nTANKAGOUKEI") as Label).Text;
                }
                else
                {
                    txtGokeiKingakuItem.Text = "";
                }
                if ((GV_MitumoriMeisai.Rows[rowindex - 1].FindControl("lblMeisai_nGENKATANKA") as Label).Text != "0" && !String.IsNullOrEmpty((GV_MitumoriMeisai.Rows[rowindex - 1].FindControl("lblMeisai_nGENKATANKA") as Label).Text))
                {
                    txtGenTankaItem.Text = (GV_MitumoriMeisai.Rows[rowindex - 1].FindControl("lblMeisai_nGENKATANKA") as Label).Text;
                }
                else
                {
                    txtGenTankaItem.Text = "";
                }
                if ((GV_MitumoriMeisai.Rows[rowindex - 1].FindControl("lblMeisai_nGENKAGOUKEI") as Label).Text != "0" && !String.IsNullOrEmpty((GV_MitumoriMeisai.Rows[rowindex - 1].FindControl("lblMeisai_nGENKAGOUKEI") as Label).Text))
                {
                    txtGenkaItem.Text = (GV_MitumoriMeisai.Rows[rowindex - 1].FindControl("lblMeisai_nGENKAGOUKEI") as Label).Text;
                }
                else
                {
                    txtGenkaItem.Text = "";
                }
                if ((GV_MitumoriMeisai.Rows[rowindex - 1].FindControl("lblMeisai_nARARI") as Label).Text != "0" && !String.IsNullOrEmpty((GV_MitumoriMeisai.Rows[rowindex - 1].FindControl("lblMeisai_nARARI") as Label).Text))
                {
                    txtArariItem.Text = (GV_MitumoriMeisai.Rows[rowindex - 1].FindControl("lblMeisai_nARARI") as Label).Text;
                }
                else
                {
                    txtArariItem.Text = "0";
                }
                if ((GV_MitumoriMeisai.Rows[rowindex - 1].FindControl("lblMeisai_nARARISu") as Label).Text != "0" && !String.IsNullOrEmpty((GV_MitumoriMeisai.Rows[rowindex - 1].FindControl("lblMeisai_nARARISu") as Label).Text))
                {
                    txtArariSuItem.Text = (GV_MitumoriMeisai.Rows[rowindex - 1].FindControl("lblMeisai_nARARISu") as Label).Text;
                }
                else
                {
                    txtArariSuItem.Text = "";
                }
            }
            else
            {
                KeiSan_CB_nKIRI_G();
                KeiSan_CB_nTANKA_G();
                KeiSan_ARARI();
            }

            HasCheckRow();
            updBody.Update();
        }
        #endregion

        #region 単価原価方式クリック
        protected void btn_gentanka_Click(object sender, EventArgs e)
        {
            btn_genka.CssClass = "JC10ZeikomiBtn";
            btn_gentanka.CssClass = "JC10ZeikomiBtnActive";
            Int32 rowindex = Convert.ToInt32(HF_selectRowIndex.Value);
            (GV_MitumoriMeisai.Rows[rowindex - 1].FindControl("lblfgenkatanka") as Label).Text = "0";
            String nRitu = (GV_MitumoriMeisai.Rows[rowindex - 1].FindControl("lblMeisai_nRITU") as Label).Text;
            DataTable dt_Syosai = GetSyosaiGridViewData(GV_MitumoriSyohin);
            while (dt_Syosai.Rows.Count < GV_MitumoriSyohin.Rows.Count)
            {
                DataRow dr = dt_Syosai.NewRow();
                dr[0] = "";
                dr[1] = "";
                dr[2] = "";
                dr[3] = "";
                dr[4] = "";
                dr[5] = "";
                dr[6] = "";
                dr[7] = "";
                dr[8] = "";
                dr[9] = "";
                dr[10] = "";
                dr[11] = "";
                dr[12] = nRitu;
                dr[13] = "";
                dr[14] = "1";
                dt_Syosai.Rows.Add(dr);
            }
            GV_MitumoriSyohin.DataSource = dt_Syosai;
            GV_MitumoriSyohin.DataBind();
            HF_isChange.Value = "1";
            //BindAllSyosai();
            KeiSan_CB_nKIRI_G();
            KeiSan_CB_nTANKA_G();
            KeiSan_ARARI();
            HasCheckRow();
            updBody.Update();
        }

        #region toDelete
        //protected void RB_gentanka_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (RB_gentanka.Checked)
        //    {
        //        RB_genka.Checked = false;
        //        //btn_genka.CssClass = "JC11GenkaHouShikiBtn";
        //        //btn_gentanka.CssClass = "JC11GenkaHouShikiActiveBtn";
        //        Int32 rowindex = Convert.ToInt32(HF_selectRowIndex.Value);
        //        (GV_MitumoriMeisai.Rows[rowindex - 1].FindControl("lblfgenkatanka") as Label).Text = "0";
        //        String nRitu = (GV_MitumoriMeisai.Rows[rowindex - 1].FindControl("lblMeisai_nRITU") as Label).Text;
        //        DataTable dt_Syosai = GetSyosaiGridViewData(GV_MitumoriSyohin);
        //        while (dt_Syosai.Rows.Count < GV_MitumoriSyohin.Rows.Count)
        //        {
        //            DataRow dr = dt_Syosai.NewRow();
        //            dr[0] = "";
        //            dr[1] = "";
        //            dr[2] = "";
        //            dr[3] = "";
        //            dr[4] = "";
        //            dr[5] = "";
        //            dr[6] = "";
        //            dr[7] = "";
        //            dr[8] = "";
        //            dr[9] = "";
        //            dr[10] = "";
        //            dr[11] = "";
        //            dr[12] = nRitu;
        //            dr[13] = "";
        //            dt_Syosai.Rows.Add(dr);
        //        }
        //        GV_MitumoriSyohin.DataSource = dt_Syosai;
        //        GV_MitumoriSyohin.DataBind();
        //        HF_isChange.Value = "1";
        //        //BindAllSyosai();
        //        KeiSan_CB_nKIRI_G();
        //        KeiSan_CB_nTANKA_G();
        //        KeiSan_ARARI();
        //        HasCheckRow();
        //        updBody.Update();
        //    }
        //}
        #endregion
        #endregion

        #region KeiSan_CB_nKIRI_G
        private void KeiSan_CB_nKIRI_G()
        {
            Double nkingaku = 0;
            try
            {
                if (GV_MitumoriSyohin.Rows.Count > 0)
                {
                    for (int i = 0; i < GV_MitumoriSyohin.Rows.Count; i++)
                    {
                        if (!String.IsNullOrEmpty((GV_MitumoriSyohin.Rows[i].FindControl("lblTankaGokei") as Label).Text))
                        {
                            nkingaku = nkingaku + Convert.ToDouble((GV_MitumoriSyohin.Rows[i].FindControl("lblTankaGokei") as Label).Text);
                        }
                        else
                        {
                            nkingaku += 0;
                        }
                    }
                    if (btn_genka.CssClass== "JC10ZeikomiBtnActive")
                    {
                        txtHyouJunTankaItem.Text = nkingaku.ToString("#,##0");
                        txtTankaItem.Text= nkingaku.ToString("#,##0");
                        Double nsuryo = 0;
                        if (!String.IsNullOrEmpty(txtSuryoItem.Text))
                        {
                            nsuryo = Convert.ToDouble(txtSuryoItem.Text);
                        }
                        txtGokeiKingakuItem.Text = (nkingaku * nsuryo).ToString("#,##0");
                    }
                    else
                    {
                        txtGokeiKingakuItem.Text = "0";
                        txtHyouJunTankaItem.Text = "0";
                        txtTankaItem.Text = "0";
                    }
                }
                else
                {
                    txtHyouJunTankaItem.Text = "0";
                    txtGokeiKingakuItem.Text = "0";
                    txtTankaItem.Text = nkingaku.ToString();
                }
            }
            catch (Exception ex)
            {
            }
        }

        #endregion

        #region KeiSan_CB_nTANKA_G
        private void KeiSan_CB_nTANKA_G()
        {
            Double nkingaku = 0;
            try
            {
                if (GV_MitumoriSyohin.Rows.Count > 0)
                {
                    for (int i = 0; i < GV_MitumoriSyohin.Rows.Count; i++)
                    {
                        //原価合計
                        if (!String.IsNullOrEmpty((GV_MitumoriSyohin.Rows[i].FindControl("lblGenkaGokei") as Label).Text)) 
                        {
                            nkingaku = nkingaku + Convert.ToDouble((GV_MitumoriSyohin.Rows[i].FindControl("lblGenkaGokei") as Label).Text);
                        }
                        else
                        {
                            nkingaku += 0;
                        }
                        
                    }
                    Double nsuryo = 0;
                    if (!String.IsNullOrEmpty(txtSuryoItem.Text))
                    {
                        nsuryo = Convert.ToDouble(txtSuryoItem.Text);
                    }
                    if (btn_genka.CssClass != "JC10ZeikomiBtnActive")
                    {
                        
                        txtGenTankaItem.Text = nkingaku.ToString("#,##0.##");
                        txtGenkaItem.Text = (nkingaku * nsuryo).ToString("#,##0");

                    }
                    else 
                    {
                        txtGenkaItem.Text = nkingaku.ToString("#,##0");
                        if (nsuryo != 0)
                        {
                            txtGenTankaItem.Text = (nkingaku / nsuryo).ToString("#,##0.##");
                        }
                        else
                        {
                            txtGenTankaItem.Text = "0";
                        }

                    }

                }
                else
                {
                    txtGenTankaItem.Text = "0";
                    txtGenkaItem.Text = "0";
                }
            }
            catch (Exception ex)
            {
            }
        }
        #endregion

        #region KeiSan_ARARI
        private void KeiSan_ARARI()
        {
            decimal kirikingaku = 0;           //仕切金額
            decimal siirekingaku = 0;             //仕入金額
            decimal aa = 0;
            try
            {
                if (GV_MitumoriSyohin.Rows.Count > 0)
                {
                    if (!txtGokeiKingakuItem.Text.ToString().Equals("") && txtGokeiKingakuItem.Text != null)
                    {
                        siirekingaku = Convert.ToDecimal(txtGenkaItem.Text);
                        kirikingaku = Convert.ToDecimal(txtGokeiKingakuItem.Text);
                        txtArariItem.Text = (kirikingaku - siirekingaku).ToString("#,##0");

                        if (kirikingaku != 0 || siirekingaku != 0)
                        {
                            if (kirikingaku != 0)
                            {
                                aa = ((kirikingaku - siirekingaku) / kirikingaku) * 100;

                                txtArariSuItem.Text = aa.ToString("0.0") + "%";
                            }
                            else
                            {
                                txtArariSuItem.Text = "0.0%";
                            }
                        }
                        else
                        {
                            txtArariSuItem.Text = "0.0%";
                        }
                    }
                    else
                    {
                        txtArariItem.Text = "0";
                        txtArariSuItem.Text = "0.0%";
                    }
                }
                else
                {
                    txtArariItem.Text = "0";
                    txtArariSuItem.Text = "0.0%";
                }

            }
            catch (Exception ex)
            {
               
            }



        }
        #endregion

        #region GV_MitumoriSyohin_RowDataBound
        protected void GV_MitumoriSyohin_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    #region getsTANI    

                    String id = Request.QueryString["id"];
                    DataTable dt_db = JC01Login_Class.Get_DB(id);
                    String db = dt_db.Rows[0]["db"].ToString();
                    String mail = dt_db.Rows[0]["mail"].ToString();
                    ConstantVal.DB_NAME = db;

                    JC_ClientConnecction_Class jc = new JC_ClientConnecction_Class();
                    jc.loginId = mail;
                    MySqlConnection cn = jc.GetConnection();
                    cn.Open();
                    string getsTANI = " select distinct cTANI, sTANI from M_TANI order by cTANI ";
                    MySqlCommand cmd = new MySqlCommand(getsTANI, cn);
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    DataTable dtable = new DataTable();
                    da.Fill(dtable);
                    cn.Close();
                    da.Dispose();

                    DropDownList DropDownList1 = (e.Row.FindControl("DDL_cTANI") as DropDownList);
                    DropDownList1.DataSource = dtable;
                    DropDownList1.DataTextField = "sTANI";
                    DropDownList1.DataValueField = "sTANI";
                    DropDownList1.DataBind();

                    TextBox txtBox = (e.Row.FindControl("txtcSYOHIN") as TextBox);
                    string lblcTANI = (e.Row.FindControl("lblcTANI") as Label).Text;
                    //if (txtBox.Text != "")
                    //{
                    try
                    {
                        DropDownList1.Items.FindByText(lblcTANI).Selected = true;
                    }
                    catch
                    { }
                    //}
                    DropDownList1.Items.Insert(0, new ListItem(" ", "00"));
                    #endregion

                    #region 小
                    Label lbl_fJITAIS = (e.Row.FindControl("lblfjitais") as Label);
                    LinkButton LK_syou = (e.Row.FindControl("LK_Syou") as LinkButton);
                    TextBox txt_sSyouhin = (e.Row.FindControl("txtsSYOHIN") as TextBox);
                    if (lbl_fJITAIS.Text == "0")
                    {
                        LK_syou.BackColor = System.Drawing.Color.White;
                        txt_sSyouhin.MaxLength = 50;
                    }
                    else
                    {
                        LK_syou.BackColor = ColorTranslator.FromHtml("#f2f2f2");
                        txt_sSyouhin.MaxLength = 60;
                    }
                    #endregion

                    TextBox txt_cSyohin = (e.Row.FindControl("txtcSYOHIN") as TextBox);
                    TextBox txt_sSyohin = (e.Row.FindControl("txtsSYOHIN") as TextBox);
                    TextBox txt_nSyoryo = (e.Row.FindControl("txtnSURYO") as TextBox);
                    //DropDownList ddl_cTani = (e.Row.FindControl("DDL_cTANI") as DropDownList);
                    TextBox txt_cTani = (e.Row.FindControl("txtTani") as TextBox);
                    Label lbl_cTani = (e.Row.FindControl("lblcTANI") as Label);
                    TextBox tb_tanka = e.Row.FindControl("txtnTANKA") as TextBox;
                    Label lbl_ntanka = e.Row.FindControl("lblnTANKA") as Label;
                    Label lbl_tanka = e.Row.FindControl("lblTankaGokei") as Label;
                    TextBox txt_nGenkaTanka = (e.Row.FindControl("txtnGENKATANKA") as TextBox);
                    Label lbl_GenkaGokei = (e.Row.FindControl("lblGenkaGokei") as Label);
                    Label lbl_nSikiriTanka = (e.Row.FindControl("lblTanka") as Label);
                    TextBox txt_nRitu = (e.Row.FindControl("txtnRITU") as TextBox);
                    Label lbl_nRitu = (e.Row.FindControl("lblnRITU") as Label);

                    if (btn_genka.CssClass== "JC10ZeikomiBtnActive")
                    {
                        if (!String.IsNullOrEmpty(txt_cSyohin.Text) || !String.IsNullOrEmpty(txt_sSyohin.Text) ||
                                !String.IsNullOrEmpty(txt_nSyoryo.Text) || !String.IsNullOrEmpty(lbl_cTani.Text) ||
                                !String.IsNullOrEmpty(tb_tanka.Text) || !String.IsNullOrEmpty(lbl_tanka.Text) ||
                                !String.IsNullOrEmpty(txt_nGenkaTanka.Text) || !String.IsNullOrEmpty(lbl_GenkaGokei.Text))
                        {
                            tb_tanka.Text = "0";
                            lbl_ntanka.Text = "0";
                            lbl_tanka.Text = "0";
                            lbl_nSikiriTanka.Text = "0";
                        }
                        else
                        {
                            tb_tanka.Text = "";
                            lbl_ntanka.Text = "";
                            lbl_tanka.Text = "";
                            lbl_nSikiriTanka.Text = "";
                        }
                        //tb_tanka.ReadOnly = true;
                        tb_tanka.Visible = false;
                        lbl_ntanka.Visible = true;
                        txt_nRitu.Visible = false;
                        lbl_nRitu.Visible = true;

                    }
                    else
                    {
                        //tb_tanka.ReadOnly = false;
                        tb_tanka.Visible = true;
                        lbl_ntanka.Visible = false;
                        txt_nRitu.Visible = true;
                        lbl_nRitu.Visible = false;
                    }

                }
            }
            catch
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "parentButtonClick('btnToLogin','" + hdnHome.Value + "');", true);
            }
        }
        #endregion

        #region DDL_cTANI_SelectedIndexChanged
        protected void DDL_cTANI_SelectedIndexChanged(object sender, EventArgs e)
        {
            var ddl_tani = sender as DropDownList;
            GridViewRow gvr = (GridViewRow)ddl_tani.NamingContainer;
            int rowindex = gvr.RowIndex;
            (GV_MitumoriSyohin.Rows[rowindex].FindControl("lblcTANI") as Label).Text = ddl_tani.SelectedItem.ToString();
            (GV_MitumoriSyohin.Rows[rowindex].FindControl("txtTani") as TextBox).Text = ddl_tani.SelectedItem.ToString();
            updMitsumoriSyohinGrid.Update();
            HF_isChange.Value = "1";
            HasCheckRow();
            updBody.Update();
            //BindAllSyosai();
        }
        #endregion

        #region txtTani_TextChanged
        protected void txtTani_TextChanged(object sender, EventArgs e)
        {
            var txt_tani = sender as TextBox;
            GridViewRow gvr = (GridViewRow)txt_tani.NamingContainer;
            int rowindex = gvr.RowIndex;
            String tani = HF_TxtTani.Value;
            (GV_MitumoriSyohin.Rows[rowindex].FindControl("lblcTANI") as Label).Text = tani;
            (GV_MitumoriSyohin.Rows[rowindex].FindControl("txtTani") as TextBox).Text = tani;
            updMitsumoriSyohinGrid.Update();
            HF_isChange.Value = "1";
            HasCheckRow();
            updBody.Update();
        }
        #endregion

        #region LK_Syou_Click
        protected void LK_Syou_Click(object sender, EventArgs e)
        {
            var LK_Syou = sender as LinkButton;
            GridViewRow gvr = (GridViewRow)LK_Syou.NamingContainer;
            int rowindex = gvr.RowIndex;
            String fJitais = "0";

            if (LK_Syou.BackColor == Color.White)
            {
                (GV_MitumoriSyohin.Rows[rowindex].FindControl("lblfjitais") as Label).Text = "1";
                (GV_MitumoriSyohin.Rows[rowindex].FindControl("txtsSYOHIN") as TextBox).MaxLength = 60;
                LK_Syou.BackColor = ColorTranslator.FromHtml("#f2f2f2");
                fJitais = "1";
            }
            else
            {
                (GV_MitumoriSyohin.Rows[rowindex].FindControl("lblfjitais") as Label).Text = "0";
                (GV_MitumoriSyohin.Rows[rowindex].FindControl("txtsSYOHIN") as TextBox).MaxLength = 50;

                var textByteCount = Encoding.Default.GetByteCount((GV_MitumoriSyohin.Rows[rowindex].FindControl("txtsSYOHIN") as TextBox).Text);
                if (textByteCount > 50)
                {
                    string val = (GV_MitumoriSyohin.Rows[rowindex].FindControl("txtsSYOHIN") as TextBox).Text;
                    while (Encoding.Default.GetByteCount(val) > 50)
                    {
                        val = val.Substring(0, val.Length - 1);
                    }
                    (GV_MitumoriSyohin.Rows[rowindex].FindControl("txtsSYOHIN") as TextBox).Text = val;
                }

                //(GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("txtsSYOHIN") as TextBox).Style.Add("font-size", "13px !important");
                //(GV_MitumoriSyohin.Rows[rowindex].FindControl("txtsSYOHIN") as TextBox).Style.Add("font-size", "13px !important");
                LK_Syou.BackColor = Color.White;
                fJitais = "0";
            }

            updMitsumoriSyohinGrid.Update();
            HF_isChange.Value = "1";
            HasCheckRow();
            updBody.Update();
        }
        #endregion

        #region btnOk_Click
        protected void btnOk_Click(object sender, EventArgs e)
        {
            if (HF_isChange.Value == "1")
            {
                HF_OK.Value = "1";
                updBody.Update();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowAnkenChangeMessage",
                        "ShowKoumokuChangesConfirmMessage('項目が変更されています。更新しますか？','" + btnYes.ClientID + "','" + btnNo.ClientID + "','" + btnCancel1.ClientID + "');", true);
            }
            else
            {
                DataTable dt_Meisai = GetMeisaiGridViewData();
                Session["SyohinMeisaidt"] = dt_Meisai;

                DataTable dt_Syosai = GetGridViewData(GV_Syosai);
                dt_Syosai.DefaultView.Sort = "rowNo asc";
                Session["SyohinSyosaidt"] = dt_Syosai;

                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "parentButtonClick('btn_getSyosai','" + hdnHome.Value + "');", true);
            }
        }
        #endregion

        #region txtsSYOHIN_TextChanged
        protected void txtsSYOHIN_TextChanged(object sender, EventArgs e)
        {
            HF_isChange.Value = "1";
            HasCheckRow();
            updBody.Update();
            //BindAllSyosai();
        }
        #endregion

        #region HasCheckRow()
        private void HasCheckRow()
        {
            foreach (GridViewRow row in GV_MitumoriSyohin.Rows)
            {
                CheckBox chk_status = (row.FindControl("chkSelectSyouhin") as CheckBox);
                if (chk_status.Checked)
                {
                    btnFukusuCopy.CssClass = "JC10GrayButton";
                    btnFukusuCopy.Enabled = true;
                    btn_delete.CssClass = "JC10GrayButton"; //20220517 Added Eaindray
                    btn_delete.Enabled = true; //20220517 Added Eaindray
                    break;
                }
                else
                {
                    btnFukusuCopy.CssClass = "JC10DisableButton";
                    btnFukusuCopy.Enabled = false;
                    btn_delete.CssClass = "JC10DisableButton"; //20220517 Added Eaindray
                    btn_delete.Enabled = false; //20220517 Added Eaindray
                }
            }
            updBody.Update();
        }
        #endregion

        #region chkSelectSyouhin_CheckedChanged
        protected void chkSelectSyouhin_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chk = sender as CheckBox;
            GridViewRow gvr = (GridViewRow)chk.NamingContainer;
            int rowindex = gvr.RowIndex;
            if (chk.Checked)
            {
                (GV_MitumoriSyohin.Rows[rowindex].FindControl("lblhdnStatus") as Label).Text = "1";
            }
            else
            {
                (GV_MitumoriSyohin.Rows[rowindex].FindControl("lblhdnStatus") as Label).Text = "0";
            }
            updMitsumoriSyohinGrid.Update();
            updBody.Update();
            HasCheckRow();
        }
        #endregion

        #region btnToLogin_Click
        protected void btnToLogin_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "parentButtonClick('btnToLogin','" + hdnHome.Value + "');", true);
        }
        #endregion

        //20220518 Added Eaindray Start
        #region btn_delete_Click
        protected void btn_delete_Click(object sender, EventArgs e)
        {
            DataTable dt = GetGridViewData(GV_MitumoriSyohin);
            List<string> list_syousai = new List<string>();
            List<string> list_row = new List<string>();
            for (int i = dt.Rows.Count - 1; i >= 0; i--)
            {
                DataRow dr = dt.Rows[i];
                if (dr[0].ToString() == "1")
                {
                    list_row.Add((GV_MitumoriSyohin.Rows[i].FindControl("lblRowNo") as Label).Text);
                    dr.Delete();
                }
            }
            dt.AcceptChanges();

            ViewState["SyouhinTable"] = dt;
            GV_MitumoriSyohin.DataSource = dt;
            GV_MitumoriSyohin.DataBind();
            HF_isChange.Value = "1";
            KeiSan_CB_nKIRI_G();
            KeiSan_CB_nTANKA_G();
            KeiSan_ARARI();

            if (GV_MitumoriSyohin.Rows.Count == 0)
            {
                btnFukusuCopy.CssClass = "JC10DisableButton";
                btnFukusuCopy.Enabled = false;
                btn_delete.CssClass = "JC10DisableButton";
                btn_delete.Enabled = false;
                BT_SyohinEmptyAdd.CssClass = "JC10GrayButton";
            }
            else
            {
                BT_SyohinEmptyAdd.CssClass = "DisplayNone";
            }
            HasCheckRow();
            updBody.Update();
        }
        #endregion

        #region btn_CloseTokuisakiSentaku_Click
        protected void btn_CloseTokuisakiSentaku_Click(object sender, EventArgs e)
        {
            ifShinkiPopup1.Src = "";
            mpeShinkiPopup1.Hide();
            updShinkiPopup1.Update();
        }
        #endregion
        //20220518 Added Eaindray End

    }
}