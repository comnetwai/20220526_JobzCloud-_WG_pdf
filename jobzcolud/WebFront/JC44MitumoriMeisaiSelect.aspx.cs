using Common;
using MySql.Data.MySqlClient;
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
    public partial class JC44MitumoriMeisaiSelect : System.Web.UI.Page
    {
        DataTable dt_SyohinKomoku;

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

                        HF_cMitumori.Value = Session["cMitumori"].ToString();
                        getSyouhinData();
                        getSyosaiSyouhinData();
                    }
                    else
                    {
                        SyohinKoumokuSort();
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
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "parentButtonClick('btn_CloseMeisaiCopy','" + hdnHome.Value + "');", true);
        }
        #endregion

        #region btnOk_Click
        protected void btnOk_Click(object sender, EventArgs e)
        {
            DataTable dtMeisai = new DataTable();
            DataTable dtSyousai = new DataTable();
            DataTable dtAllSyousai = new DataTable();
            dtMeisai = CreateSyouhinTableColomn();
            dtSyousai = CreateSyosaiTableColomn();
            dtAllSyousai = ViewState["SyousaiTable"] as DataTable;

            foreach (GridViewRow row in GV_MitumoriSyohin_Original.Rows)
            {
                Label lbl_status = (row.FindControl("lblhdnStatus") as Label);
                Label lbl_fgenkataka = (row.FindControl("lblfgenkatanka") as Label);
                Label lbl_rowNo = (row.FindControl("lblRowNo") as Label);
                Label lbl_cSyohin = (row.FindControl("LB_cSYOHIN") as Label);
                Label lbl_sSyohin = (row.FindControl("LB_sSYOHIN") as Label);
                Label lbl_nSyoryo = (row.FindControl("LB_nSURYO") as Label);
                //DropDownList ddl_cTani = (row.FindControl("DDL_cTANI") as DropDownList);
                Label lbl_cTani = (row.FindControl("lblcTANI") as Label);
                Label lbl_nTanka = (row.FindControl("LB_nTANKA") as Label);
                Label lbl_TankaGokei = (row.FindControl("lblTankaGokei") as Label);
                Label lbl_nGenkaTanka = (row.FindControl("LB_nGENKATANKA") as Label);
                Label lbl_GenkaGokei = (row.FindControl("lblGenkaGokei") as Label);
                Label lbl_Arari = (row.FindControl("lblnARARI") as Label);
                Label lbl_ArariSu = (row.FindControl("lblnARARISu") as Label);
                Label lbl_nRITU = (row.FindControl("LB_nRITU") as Label);
                Label lbl_kubun = (row.FindControl("lblKubun") as Label);
                Label lbl_nSIKIRITANKA = (row.FindControl("lblTanka") as Label);
                Label lbl_fJITAIS = (row.FindControl("lblfjitais") as Label);
                Label lbl_fJITAIQ = (row.FindControl("lblfjitaiq") as Label);

                if (lbl_status.Text == "1")
                {
                    DataRow dr = dtMeisai.NewRow();
                    dr[0] = lbl_status.Text;
                    dr[1] = lbl_cSyohin.Text;
                    dr[2] = lbl_sSyohin.Text;
                    dr[3] = lbl_nSyoryo.Text;
                    dr[4] = lbl_cTani.Text;
                    dr[5] = lbl_nTanka.Text;
                    dr[6] = lbl_TankaGokei.Text;
                    dr[7] = lbl_nGenkaTanka.Text;
                    dr[8] = lbl_GenkaGokei.Text;
                    dr[9] = lbl_Arari.Text;
                    dr[10] = lbl_ArariSu.Text;
                    dr[11] = lbl_fgenkataka.Text;
                    dr[12] = lbl_rowNo.Text;
                    dr[13] = lbl_nRITU.Text;
                    dr[14] = lbl_kubun.Text;
                    dr[15] = lbl_nSIKIRITANKA.Text;
                    dr[16] = lbl_fJITAIS.Text;
                    dr[17] = lbl_fJITAIQ.Text;
                    dtMeisai.Rows.Add(dr);

                    if (dtAllSyousai.Rows.Count > 0)
                    {
                        DataRow[] rows = dtAllSyousai.Select("rowNo = '" + lbl_rowNo.Text + "'");
                        for (int i = 0; i < rows.Count(); i++)
                        {
                            DataRow dr1 = dtSyousai.NewRow();
                            dr1[0] = rows[i][0];
                            dr1[1] = rows[i][1];
                            dr1[2] = rows[i][2];
                            dr1[3] = rows[i][3];
                            dr1[4] = rows[i][4];
                            dr1[5] = rows[i][5];
                            dr1[6] = rows[i][6];
                            dr1[7] = rows[i][7];
                            dr1[8] = rows[i][8];
                            dr1[9] = rows[i][9];
                            dr1[10] = rows[i][10];
                            dr1[11] = rows[i][11];
                            dr1[12] = rows[i][12];
                            dr1[13] = rows[i][13];
                            dr1[14] = rows[i][14];
                            dtSyousai.Rows.Add(dr1);
                        }
                    }

                }
            }
            if (dtMeisai.Rows.Count > 0)
            {
                Session["cMitumori"] = HF_cMitumori.Value;
                Session["TaMituSyouhinTable"] = dtMeisai;
                Session["TaMituSyousaiTable"] = dtSyousai;
                ScriptManager.RegisterStartupScript(this, GetType(), "SetMeisaiOK", "SetMeisaiOK();", true); //20220520 MyatNoe Added
                ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "parentButtonClick('btn_SelectMeisaiCopy','" + hdnHome.Value + "');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowErrorMessage",
                   "ShowErrorMessage('データを選択してください。');", true);
            }

        }
        #endregion

        #region GetSyohinColumn()
        private void GetSyohinColumn()
        {
            String id = Request.QueryString["id"];
            DataTable dt_db = JC01Login_Class.Get_DB(id);
            String db = dt_db.Rows[0]["db"].ToString();
            String mail = dt_db.Rows[0]["mail"].ToString();
            ConstantVal.DB_NAME = db;

            JC07Home_Class JC07Home = new JC07Home_Class();
            JC07Home.loginId = mail;
            JC07Home.cListId = "4";
            dt_SyohinKomoku = new DataTable();
            dt_SyohinKomoku = JC07Home.KomokuSetting();
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

        #region CreateSyosaiTableColomn
        private DataTable CreateSyosaiTableColomn()
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

        #region getSyouhinData()
        protected void getSyouhinData()
        {
            String id = Request.QueryString["id"];
            DataTable dt_db = JC01Login_Class.Get_DB(id);
            String db = dt_db.Rows[0]["db"].ToString();
            String mail = dt_db.Rows[0]["mail"].ToString();
            ConstantVal.DB_NAME = db;

            JC_ClientConnecction_Class jc = new JC_ClientConnecction_Class();
            jc.loginId = mail;
            MySqlConnection cn = jc.GetConnection();
            cn.Open();
            string sql = "Select " +
                " IfNull(r_mitumori_m.cMITUMORI, '') As cMITUMORI, " +
                " case r_mitumori_m.nINSATSU_GYO " +
                " when 0 then '' " +
                " else cast(r_mitumori_m.nINSATSU_GYO as char) " +
                " end As nINSATSU_GYO," +
                " IfNull(r_mitumori_m.cMITUMORI_KO, '') As cMITUMORI_KO," +
                " IfNull(r_mitumori_m.nGYOUNO, 0) As nGYOUNO," +
                " IfNull(r_mitumori_m.cSYOUHIN, '') As cSYOUHIN," +
                " IfNull(r_mitumori_m.sSYOUHIN_R, '') As sSYOHIN," +
                " format( IfNull(r_mitumori_m.nTANKA, 0),0)  As nTANKA," +
                " format(IfNull(r_mitumori_m.nSURYO, 0),2) As nSURYO," +
                " IfNull(r_mitumori_m.cSHIIRESAKI, '') As cSHIIRESAKI," +
                " IfNull(r_mitumori_m.sSHIIRESAKI, '') As sSHIIRESAKI," +
                " format( IfNull(r_mitumori_m.nSIIRETANKA, 0),0) As nSIIRETANKA," +
                " format( IfNull(r_mitumori_m.nSIIREKINGAKU, 0),0) As nSIIREKINGAKU," +
                " format( IfNull(r_mitumori_m.nSIKIRITANKA, 0),0) As nSIKIRITANKA," +
                " format( IfNull(r_mitumori_m.nSIKIRIKINGAKU, 0),0) As nSIKIRIKINGAKU," +
                " IfNull(r_mitumori_m.sTANI, '') As sTANI," +
                " IfNull(r_mitumori_m.nKINGAKU, 0) As nKINGAKU," +
                " IfNull(r_mitumori_m.nRITU, 100) As nRITU," +
                " IfNull(r_mitumori_m.cSYOUSAI, '') As cSYOUSAI," +
                " IfNull(r_mitumori_m.sSETSUMUI, '') As sSETSUMUI," +
                " IfNull(r_mitumori_m.fJITAIS, 0) As fJITAIS," +
                " IfNull(r_mitumori_m.fJITAIQ, 0) As fJITAIQ," +
                " ifnull(ms.fKazei, 0) AS fkazei," +
                " format( IfNull(r_mitumori_m.nSIKIRIKINGAKU - r_mitumori_m.nSIIREKINGAKU,0),0)  As nARARI," +
                " CONCAT(FORMAT(IfNull((r_mitumori_m.nSIKIRIKINGAKU - r_mitumori_m.nSIIREKINGAKU) / r_mitumori_m.nSIKIRIKINGAKU,0) * 100,1),'%') As nARARISu," +
                " r_mitumori_m.rowNO AS rowNO," +
                " ifnull(r_mitumori_m.sMEMO, '') as sMEMO," +
                " ifnull(r_mitumori_m.fCHECK, '') as fCHECK," +
                " IfNull(r_mitumori_m.fgentankatanka, '0') As fgentankatanka,'0' As jissekigenka," +
                " IfNull(r_mitumori_m.sKUBUN, '') As sKUBUN " +
                " From r_mitumori_m" +
                " left join  m_syouhin ms ON r_mitumori_m.cSYOUHIN = ms.cSYOUHIN " +
                " Where '1' = '1' and r_mitumori_m.cMITUMORI like '%" + HF_cMitumori.Value + "%' order by r_mitumori_m.nGYOUNO; ";
            MySqlCommand cmd = new MySqlCommand(sql, cn);
            cmd.CommandTimeout = 0;
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable dtable = new DataTable();
            da.Fill(dtable);
            cn.Close();
            da.Dispose();
            if (dtable.Rows.Count > 0)
            {
                DataTable dt = CreateSyouhinTableColomn();
                int rowcount = dtable.Rows.Count;
                for (int i = 1; i <= rowcount; i++)
                {
                    DataRow dr = dt.NewRow();
                    if (dtable.Rows.Count >= i)
                    {
                        dr[0] = "0";
                        dr[1] = dtable.Rows[i - 1]["cSYOUHIN"].ToString();
                        dr[2] = dtable.Rows[i - 1]["sSYOHIN"].ToString();
                        Double nsuryo = Convert.ToDouble(dtable.Rows[i - 1]["nSURYO"].ToString());
                        dr[3] = nsuryo.ToString("#,##0.##");
                        dr[4] = dtable.Rows[i - 1]["sTANI"].ToString();
                        dr[5] = dtable.Rows[i - 1]["nTANKA"].ToString();
                        dr[6] = dtable.Rows[i - 1]["nSIKIRIKINGAKU"].ToString();
                        dr[7] = dtable.Rows[i - 1]["nSIIRETANKA"].ToString();
                        dr[8] = dtable.Rows[i - 1]["nSIIREKINGAKU"].ToString();
                        dr[9] = dtable.Rows[i - 1]["nARARI"].ToString();
                        dr[10] = dtable.Rows[i - 1]["nARARISu"].ToString();
                        dr[11] = dtable.Rows[i - 1]["fgentankatanka"].ToString();
                        dr[12] = dtable.Rows[i - 1]["rowNO"].ToString();
                        dr[13] = dtable.Rows[i - 1]["nRITU"].ToString() + "%";
                        dr[14] = dtable.Rows[i - 1]["sKUBUN"].ToString();
                        dr[15] = dtable.Rows[i - 1]["nSIKIRITANKA"].ToString();
                        dr[16] = dtable.Rows[i - 1]["fJITAIS"].ToString();
                        dr[17] = dtable.Rows[i - 1]["fJITAIQ"].ToString();
                        dt.Rows.Add(dr);
                    }
                }

                var max = dt.AsEnumerable().Max(x => int.Parse(x.Field<string>("rowNo")));
                //HF_maxRowNo.Value = max.ToString();

                GV_MitumoriSyohin_Original.DataSource = dt;
                GV_MitumoriSyohin_Original.DataBind();
                updMitsumoriSyohinGrid.Update();

                ViewState["SyouhinTable"] = dt;
            }
            else
            {
                DataTable dt = CreateSyouhinTableColomn();
                
                //var max = dt.AsEnumerable().Max(x => int.Parse(x.Field<string>("rowNo")));
                //HF_maxRowNo.Value = max.ToString();

                GV_MitumoriSyohin_Original.DataSource = dt;
                GV_MitumoriSyohin_Original.DataBind();
                updMitsumoriSyohinGrid.Update();

                ViewState["SyouhinTable"] = dt;
            }

            SyohinKoumokuSort();
            //HasCheckRow();
        }
        #endregion

        #region getSyosaiSyouhinData()
        protected void getSyosaiSyouhinData()
        {
            String id = Request.QueryString["id"];
            DataTable dt_db = JC01Login_Class.Get_DB(id);
            String db = dt_db.Rows[0]["db"].ToString();
            String mail = dt_db.Rows[0]["mail"].ToString();
            ConstantVal.DB_NAME = db;

            JC_ClientConnecction_Class jc = new JC_ClientConnecction_Class();
            jc.loginId = mail;
            MySqlConnection cn = jc.GetConnection();
            cn.Open();
            string sql = "SELECT "
                + " IfNull(rmm2.cMITUMORI, '') As cMITUMORI,"
                + " case rmm2.nINSATSU_GYO"
                + " when 0 then ''"
                + " else cast(rmm2.nINSATSU_GYO as char)"
                + " end As nINSATSU_GYO,"
                + " IfNull(rmm2.cMITUMORI_KO, '') As cMITUMORI_KO,"
                + " IfNull(rmm2.nGYOUNO, 0) As nGYOUNO,"
                + " IfNull(rmm2.cSYOUHIN, '') As cSYOUHIN,"
                + " IfNull(rmm2.sSYOUHIN_R, '') As sSYOUHIN_R,"
                + " format( IfNull(rmm2.nTANKA, 0), 0)  As nTANKA,"
                + " format(IfNull(rmm2.nSURYO, 0), 2) As nSURYO,"
                + " IfNull(rmm2.cSHIIRESAKI, '') As cSHIIRESAKI,"
                + " IfNull(rmm2.sSHIIRESAKI, '') As sSHIIRESAKI,"
                + " format( IfNull(rmm2.nSIIRETANKA, 0), 0) As nSIIRETANKA,"
                + " format( IfNull(rmm2.nSIIREKINGAKU, 0), 0) As nSIIREKINGAKU,"
                + " IfNull(rmm2.nSIKIRITANKA, 0) As nSIKIRITANKA,"
                + " format( IfNull(rmm2.nSIKIRIKINGAKU, 0), 0) As nSIKIRIKINGAKU,"
                + " IfNull(rmm2.sTANI, '') As sTANI,"
                + " IfNull(rmm2.nKINGAKU, 0) As nKINGAKU,"
                + " IfNull(rmm2.nRITU, 100) As nRITU,"
                + " IfNull(rmm2.cSYOUSAI, '') As cSYOUSAI,"
                + " IfNull(rmm2.sSETSUMUI, '') As sSETSUMUI,"
                + " IfNull(rmm2.fJITAIS, 0) As fJITAIS,"
                + " IfNull(rmm2.fJITAIQ, 0) As fJITAIQ,"
                + " rmm2.rowNO AS rowNO,"
                + " rmm2.rowNO2 AS rowNO2,"
                + " ifnull(rmm2.sMEMO, '') as sMEMO,"
                + " ifnull(rmm2.fCHECK, '') as fCHECK,"
                + " rmm.nGYOUNO as nGYOUNO1"
                + " FROM r_mitumori_m2 rmm2"
                + " LEFT JOIN r_mitumori_m rmm ON rmm2.cMITUMORI=rmm.cMITUMORI AND rmm2.rowNO=rmm.rowNO"
                + " Where rmm2.cMitumori = '" + HF_cMitumori.Value + "' order by rmm2.rowNO asc, rmm2.nGYOUNO asc; ";
            MySqlCommand cmd = new MySqlCommand(sql, cn);
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable dtable = new DataTable();
            da.Fill(dtable);
            cn.Close();
            da.Dispose();

            DataTable dt = CreateSyosaiTableColomn();
            if (dtable.Rows.Count > 0)
            {
                DataTable dt_meisai = new DataTable();
                dt_meisai = ViewState["SyouhinTable"] as DataTable;
                for (int i = 1; i <= dtable.Rows.Count; i++)
                {
                    int rowNo = Convert.ToInt32(dtable.Rows[i - 1]["rowNO"].ToString());

                    DataRow[] rows = dtable.Select("rowNo = '" + rowNo.ToString() + "'");
                    if (rows.Length > 0)
                    {
                        DataRow[] rows_Meisai = dt_meisai.Select("rowNo = '" + rowNo.ToString() + "'");
                        if (rows_Meisai.Length > 0)
                        {
                            int gyoNO = dt_meisai.Rows.IndexOf(rows_Meisai[0]);

                            (GV_MitumoriSyohin_Original.Rows[gyoNO].FindControl("btnSyohinShosai") as Button).Text = rows.Length.ToString();
                            updMitsumoriSyohinGrid.Update();
                        }
                    }

                    DataRow dr = dt.NewRow();
                    dr[0] = "0";
                    dr[1] = dtable.Rows[i - 1]["cSYOUHIN"].ToString();
                    dr[2] = dtable.Rows[i - 1]["sSYOUHIN_R"].ToString();
                    Double nsuryo = Convert.ToDouble(dtable.Rows[i - 1]["nSURYO"].ToString());
                    dr[3] = nsuryo.ToString("#,##0.##");
                    dr[4] = dtable.Rows[i - 1]["sTANI"].ToString();
                    dr[5] = dtable.Rows[i - 1]["nTANKA"].ToString();
                    dr[6] = dtable.Rows[i - 1]["nSIKIRIKINGAKU"].ToString();
                    dr[7] = dtable.Rows[i - 1]["nSIIRETANKA"].ToString();
                    dr[8] = dtable.Rows[i - 1]["nSIIREKINGAKU"].ToString();
                    Double tankaGokei = Convert.ToDouble(dtable.Rows[i - 1]["nSIKIRIKINGAKU"].ToString());
                    Double genkaGokei = Convert.ToDouble(dtable.Rows[i - 1]["nSIIREKINGAKU"].ToString());
                    Double arari = tankaGokei - genkaGokei;
                    dr[9] = arari;
                    double nArariSu = (arari / tankaGokei) * 100;
                    if (tankaGokei == 0)
                    {
                        nArariSu = 0;
                    }
                    dr[10] = nArariSu.ToString("###0.0") + "%";
                    dr[11] = dtable.Rows[i - 1]["rowNo"].ToString();
                    dr[12] = dtable.Rows[i - 1]["nRITU"].ToString() + "%";
                    dr[13] = dtable.Rows[i - 1]["nSIKIRITANKA"].ToString();
                    dr[14] = dtable.Rows[i - 1]["fJITAIS"].ToString();
                    dt.Rows.Add(dr);
                }
            }

            GV_Syosai.DataSource = dt;
            GV_Syosai.DataBind();

            ViewState["SyousaiTable"] = dt;

            updMitsumoriSyohinGrid.Update();
        }
        #endregion

        #region SyohinKoumokuSort
        public void SyohinKoumokuSort()
        {
            if (ViewState["SyouhinTable"] != null)
            {
                //midashiTextboxWidth = 0;
                //BeforeSyoekiTextboxWidth = 0;
                //AfterSyoekiTextboxWidth = 0;
                var columns = GV_MitumoriSyohin_Original.Columns.CloneFields();
                int chk_column = 30;
                int AddSyouhin_column = 30;
                int CopySyouhin_column = 30;
                int SyouhinSyosai_column = 30;
                int Kubun_column = 30;
                int cSyouhin_column = 90;
                int Syouhin_column = 30;
                int sSyouhin_column = 203;
                int Suryo_column = 70;
                int Tani_column = 58;
                int HyoujunTanka_column = 115;
                int Tanka_column = 115;
                int Kingaku_column = 115;
                int Gentaka_column = 115;
                int ritsu_column = 55;
                int Genkagokei_column = 115;
                int arari_column = 115;
                int araritsu_column = 115;
                int drag_column = 30;
                int dropdown_column = 30;

                #region getColumnWidth
                //for (int i = 0; i < columnsGrid.Length; i++)
                //{
                //    if (columnsGrid[i][0] == "cSyouhin")
                //    {
                //        cSyouhin_column = (int)Math.Round(Convert.ToDecimal(columnsGrid[i][1])) - 6;
                //    }
                //    else if (columnsGrid[i][0] == "sSyouhin")
                //    {
                //        sSyouhin_column = (int)Math.Round(Convert.ToDecimal(columnsGrid[i][1])) - 6;
                //    }
                //    else if (columnsGrid[i][0] == "Syouryou")
                //    {
                //        Suryo_column = (int)Math.Round(Convert.ToDecimal(columnsGrid[i][1])) - 6;
                //    }
                //    else if (columnsGrid[i][0] == "tani")
                //    {
                //        Tani_column = (int)Math.Round(Convert.ToDecimal(columnsGrid[i][1])) - 6;
                //    }
                //    else if (columnsGrid[i][0] == "Hyoujuntanka")
                //    {
                //        HyoujunTanka_column = (int)Math.Round(Convert.ToDecimal(columnsGrid[i][1])) - 6;
                //    }
                //    else if (columnsGrid[i][0] == "Tanka")
                //    {
                //        Tanka_column = (int)Math.Round(Convert.ToDecimal(columnsGrid[i][1])) - 6;
                //    }
                //    else if (columnsGrid[i][0] == "kingaku")
                //    {
                //        Kingaku_column = (int)Math.Round(Convert.ToDecimal(columnsGrid[i][1])) - 6;
                //    }
                //    else if (columnsGrid[i][0] == "gentanka")
                //    {
                //        Gentaka_column = (int)Math.Round(Convert.ToDecimal(columnsGrid[i][1])) - 6;
                //    }
                //    else if (columnsGrid[i][0] == "ritsu")
                //    {
                //        ritsu_column = (int)Math.Round(Convert.ToDecimal(columnsGrid[i][1])) - 6;
                //    }
                //    else if (columnsGrid[i][0] == "genkagokei")
                //    {
                //        Genkagokei_column = (int)Math.Round(Convert.ToDecimal(columnsGrid[i][1])) - 6;
                //    }
                //    else if (columnsGrid[i][0] == "arari")
                //    {
                //        arari_column = (int)Math.Round(Convert.ToDecimal(columnsGrid[i][1])) - 6;
                //    }
                //    else if (columnsGrid[i][0] == "araritsu")
                //    {
                //        araritsu_column = (int)Math.Round(Convert.ToDecimal(columnsGrid[i][1])) - 6;
                //    }
                //}
                #endregion

                if (GV_MitumoriSyohin.Columns.Count > 0)
                {
                    GV_MitumoriSyohin.Columns.Clear();
                }
                DataTable dt = ViewState["SyouhinTable"] as DataTable;

                //GV_MitumoriSyohin_Original.DataSource = dt;
                //GV_MitumoriSyohin_Original.DataBind();
                //setSyosaiCount();

                GetSyohinColumn();

                GV_MitumoriSyohin.Columns.Add(columns[0]);
                GV_MitumoriSyohin.Columns.Add(columns[1]);
                GV_MitumoriSyohin.Columns.Add(columns[2]);
                GV_MitumoriSyohin.Columns.Add(columns[3]);
                GV_MitumoriSyohin.Columns.Add(columns[4]);
                GV_MitumoriSyohin.Columns.Add(columns[5]);

                GV_MitumoriSyohin.Columns[0].HeaderStyle.Width = Unit.Pixel(chk_column);
                GV_MitumoriSyohin.Columns[1].HeaderStyle.Width = Unit.Pixel(AddSyouhin_column);
                GV_MitumoriSyohin.Columns[2].HeaderStyle.Width = Unit.Pixel(CopySyouhin_column);
                GV_MitumoriSyohin.Columns[3].HeaderStyle.Width = Unit.Pixel(SyouhinSyosai_column);
                GV_MitumoriSyohin.Columns[4].HeaderStyle.Width = Unit.Pixel(dropdown_column);
                //GV_MitumoriSyohin.Columns[3].HeaderStyle.Width = Unit.Pixel(SyouhinSyosai_column);

                DataTable dtTextbox = new DataTable();
                dtTextbox.Columns.Add("ColumnID");
                for (int i = 0; i < dt_SyohinKomoku.Rows.Count; i++)
                {
                    if (i == 0)
                    {
                        if (dt_SyohinKomoku.Rows[i]["sHYOUJI"].ToString() == "商品コード")
                        {
                            GV_MitumoriSyohin.Columns.Add(columns[6]);
                            GV_MitumoriSyohin.Columns.Add(columns[7]);

                            GV_MitumoriSyohin.Columns[i + 6].HeaderStyle.Width = Unit.Pixel(cSyouhin_column);
                            GV_MitumoriSyohin.Columns[i + 7].HeaderStyle.Width = Unit.Pixel(Syouhin_column);

                            dtTextbox.Rows.Add("txtcSYOHIN");
                            dtTextbox.Rows.Add("txtsSYOHIN");
                            dtTextbox.Rows.Add("txtnSURYO");
                            dtTextbox.Rows.Add("txtTani");
                            dtTextbox.Rows.Add("txtnTANKA");

                            GV_MitumoriSyohin.Columns.Add(columns[8]); //商品名


                            GV_MitumoriSyohin.Columns.Add(columns[9]); //数量


                            GV_MitumoriSyohin.Columns.Add(columns[10]); //単位


                            GV_MitumoriSyohin.Columns.Add(columns[11]); //標準単価


                            //GV_MitumoriSyohin.Columns.Add(columns[12]); //合計金額

                            GV_MitumoriSyohin.Columns[i + 8].HeaderStyle.Width = Unit.Pixel(sSyouhin_column);
                            GV_MitumoriSyohin.Columns[i + 9].HeaderStyle.Width = Unit.Pixel(Suryo_column);
                            GV_MitumoriSyohin.Columns[i + 10].HeaderStyle.Width = Unit.Pixel(Tani_column);
                            GV_MitumoriSyohin.Columns[i + 11].HeaderStyle.Width = Unit.Pixel(HyoujunTanka_column);
                            //GV_MitumoriSyohin.Columns[i + 11].HeaderStyle.Width = Unit.Pixel(Kingaku_column);

                            
                            i += 4;
                        }
                        else
                        {
                            GV_MitumoriSyohin.Columns.Add(columns[7]);

                            GV_MitumoriSyohin.Columns.Add(columns[8]); //商品名

                            GV_MitumoriSyohin.Columns.Add(columns[9]); //数量

                            GV_MitumoriSyohin.Columns.Add(columns[10]); //単位

                            GV_MitumoriSyohin.Columns.Add(columns[11]); //標準単価

                            //GV_MitumoriSyohin.Columns.Add(columns[12]); //合計金額

                            GV_MitumoriSyohin.Columns[i + 6].HeaderStyle.Width = Syouhin_column;
                            GV_MitumoriSyohin.Columns[i + 7].HeaderStyle.Width = sSyouhin_column;
                            GV_MitumoriSyohin.Columns[i + 8].HeaderStyle.Width = Unit.Pixel(Suryo_column);
                            GV_MitumoriSyohin.Columns[i + 9].HeaderStyle.Width = Unit.Pixel(Tani_column);
                            GV_MitumoriSyohin.Columns[i + 10].HeaderStyle.Width = Unit.Pixel(HyoujunTanka_column);
                            //GV_MitumoriSyohin.Columns[i + 10].HeaderStyle.Width = Unit.Pixel(Kingaku_column);

                            dtTextbox.Rows.Add("txtsSYOHIN");
                            dtTextbox.Rows.Add("txtnSURYO");
                            dtTextbox.Rows.Add("txtTani");
                            dtTextbox.Rows.Add("txtnTANKA");

                            //GokeiColumn_Position = "YES";
                            
                            i += 3;
                        }
                    }
                    else
                    {
                        if (dt_SyohinKomoku.Rows[i]["sHYOUJI"].ToString() == "商品コード" && i != 0)
                        {
                            dtTextbox.Rows.Add("txtcSYOHIN");
                            GV_MitumoriSyohin.Columns.Add(columns[6]);
                            GV_MitumoriSyohin.Columns[i + 7].HeaderStyle.Width = Unit.Pixel(cSyouhin_column);
                        }
                        else if (dt_SyohinKomoku.Rows[i]["sHYOUJI"].ToString() == "合計金額")
                        {
                            GV_MitumoriSyohin.Columns.Add(columns[13]); //合計金額
                            GV_MitumoriSyohin.Columns[i + 7].HeaderStyle.Width = Unit.Pixel(Kingaku_column);
                            //GokeiColumn = i;

                        }
                        else if (dt_SyohinKomoku.Rows[i]["sHYOUJI"].ToString() == "単価")
                        {
                            GV_MitumoriSyohin.Columns.Add(columns[12]);
                            GV_MitumoriSyohin.Columns[i + 7].HeaderStyle.Width = Unit.Pixel(Tanka_column);

                        }
                        else if (dt_SyohinKomoku.Rows[i]["sHYOUJI"].ToString() == "原価単価")
                        {
                            dtTextbox.Rows.Add("txtnGENKATANKA");
                            GV_MitumoriSyohin.Columns.Add(columns[14]);
                            GV_MitumoriSyohin.Columns[i + 7].HeaderStyle.Width = Unit.Pixel(Gentaka_column);

                        }
                        else if (dt_SyohinKomoku.Rows[i]["sHYOUJI"].ToString() == "掛率")
                        {
                            dtTextbox.Rows.Add("txtnRITU");
                            GV_MitumoriSyohin.Columns.Add(columns[15]);
                            GV_MitumoriSyohin.Columns[i + 7].HeaderStyle.Width = Unit.Pixel(ritsu_column);

                        }
                        else if (dt_SyohinKomoku.Rows[i]["sHYOUJI"].ToString() == "原価合計")
                        {
                            GV_MitumoriSyohin.Columns.Add(columns[16]);
                            GV_MitumoriSyohin.Columns[i + 7].HeaderStyle.Width = Unit.Pixel(Genkagokei_column);

                        }
                        else if (dt_SyohinKomoku.Rows[i]["sHYOUJI"].ToString() == "粗利")
                        {
                            GV_MitumoriSyohin.Columns.Add(columns[17]);
                            GV_MitumoriSyohin.Columns[i + 7].HeaderStyle.Width = Unit.Pixel(arari_column);

                        }
                        else if (dt_SyohinKomoku.Rows[i]["sHYOUJI"].ToString() == "粗利率")
                        {
                            GV_MitumoriSyohin.Columns.Add(columns[18]);
                            GV_MitumoriSyohin.Columns[i + 7].HeaderStyle.Width = Unit.Pixel(araritsu_column);

                        }
                    }
                }

                GV_MitumoriSyohin.Columns.Add(columns[19]);
                GV_MitumoriSyohin.Columns.Add(columns[20]);

                GV_MitumoriSyohin.Columns[dt_SyohinKomoku.Rows.Count + 7].HeaderStyle.Width = Unit.Pixel(drag_column);
                GV_MitumoriSyohin.Columns[dt_SyohinKomoku.Rows.Count + 8].HeaderStyle.Width = Unit.Pixel(dropdown_column);

                //HF_dragIndex.Value = (dt_SyohinKomoku.Rows.Count + 6).ToString();
                //HF_dropIndex.Value = (dt_SyohinKomoku.Rows.Count + 7).ToString();

                ViewState["dtGridTextbox"] = dtTextbox;

                GV_MitumoriSyohin.DataSource = dt;
                GV_MitumoriSyohin.DataBind();

                //GV_MitumoriSyohin.Width = Unit.Pixel(((int)Math.Round(Convert.ToDecimal(HF_GridSyouhin.Value))));

                //Response.Cookies["colWidthmSyouhin"].Value = HF_GridSizeSyouhin.Value;
                //Response.Cookies["colWidthmSyouhin"].Expires = DateTime.Now.AddYears(1);
                //Response.Cookies["colWidthmSyouhinGrid"].Value = HF_GridSyouhin.Value;
                //Response.Cookies["colWidthmSyouhinGrid"].Expires = DateTime.Now.AddYears(1);

                //SetSyosai();
                updMitsumoriSyohinGrid.Update();
                //updHeader.Update();

            }
        }
        #endregion

        #region chkSelectSyouhin_CheckedChanged
        protected void chkSelectSyouhin_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chk = sender as CheckBox;
            GridViewRow gvr = (GridViewRow)chk.NamingContainer;
            int rowindex = gvr.RowIndex;
            String CheckValue = "";
            if (chk.Checked)
            {
                (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblhdnStatus") as Label).Text = "1";
                (GV_MitumoriSyohin.Rows[rowindex].FindControl("lblhdnStatus") as Label).Text = "1";
                CheckValue = "1";
            }
            else
            {
                (GV_MitumoriSyohin_Original.Rows[rowindex].FindControl("lblhdnStatus") as Label).Text = "0";
                (GV_MitumoriSyohin.Rows[rowindex].FindControl("lblhdnStatus") as Label).Text = "0";
                CheckValue = "0";
            }
            DataTable dt_SyohinOriginal = GetGridViewData();
            dt_SyohinOriginal.Rows[rowindex][0] = CheckValue;
            ViewState["SyouhinTable"] = dt_SyohinOriginal;

            //GV_MitumoriSyohin.DataSource = dt_SyohinOriginal;
            //GV_MitumoriSyohin.DataBind();
            //SetSyosai();
            updMitsumoriSyohinGrid.Update();
        }
        #endregion

        #region GetGridViewData
        private DataTable GetGridViewData()
        {
            DataTable dt = new DataTable();
            if (ViewState["SyouhinTable"] == null)
            {
                dt = CreateSyouhinTableColomn();
                foreach (GridViewRow row in GV_MitumoriSyohin_Original.Rows)
                {
                    Label lbl_status = (row.FindControl("lblhdnStatus") as Label);
                    Label lbl_fgenkataka = (row.FindControl("lblfgenkatanka") as Label);
                    Label lbl_rowNo = (row.FindControl("lblRowNo") as Label);
                    Label lbl_cSyohin = (row.FindControl("LB_cSYOHIN") as Label);
                    Label lbl_sSyohin = (row.FindControl("LB_sSYOHIN") as Label);
                    Label lbl_nSyoryo = (row.FindControl("LB_nSURYO") as Label);
                    //DropDownList ddl_cTani = (row.FindControl("DDL_cTANI") as DropDownList);
                    Label lbl_cTani = (row.FindControl("lblcTANI") as Label);
                    Label lbl_nTanka = (row.FindControl("LB_nTANKA") as Label);
                    Label lbl_TankaGokei = (row.FindControl("lblTankaGokei") as Label);
                    Label lbl_nGenkaTanka = (row.FindControl("LB_nGENKATANKA") as Label);
                    Label lbl_GenkaGokei = (row.FindControl("lblGenkaGokei") as Label);
                    Label lbl_Arari = (row.FindControl("lblnARARI") as Label);
                    Label lbl_ArariSu = (row.FindControl("lblnARARISu") as Label);
                    Label lbl_nRITU = (row.FindControl("LB_nRITU") as Label);
                    Label lbl_kubun = (row.FindControl("lblKubun") as Label);
                    Label lbl_nSIKIRITANKA = (row.FindControl("lblTanka") as Label);
                    Label lbl_fJITAIS = (row.FindControl("lblfjitais") as Label);
                    Label lbl_fJITAIQ = (row.FindControl("lblfjitaiq") as Label);

                    DataRow dr = dt.NewRow();
                    dr[0] = lbl_status.Text;
                    dr[1] = lbl_cSyohin.Text;
                    dr[2] = lbl_sSyohin.Text;
                    dr[3] = lbl_nSyoryo.Text;
                    dr[4] = lbl_cTani.Text;
                    dr[5] = lbl_nTanka.Text;
                    dr[6] = lbl_TankaGokei.Text;
                    dr[7] = lbl_nGenkaTanka.Text;
                    dr[8] = lbl_GenkaGokei.Text;
                    dr[9] = lbl_Arari.Text;
                    dr[10] = lbl_ArariSu.Text;
                    dr[11] = lbl_fgenkataka.Text;
                    dr[12] = lbl_rowNo.Text;
                    dr[13] = lbl_nRITU.Text;
                    dr[14] = lbl_kubun.Text;
                    dr[15] = lbl_nSIKIRITANKA.Text;
                    dr[16] = lbl_fJITAIS.Text;
                    dr[17] = lbl_fJITAIQ.Text;
                    dt.Rows.Add(dr);
                }

                ViewState["SyouhinTable"] = dt;
            }
            else
            {
                dt = ViewState["SyouhinTable"] as DataTable;
            }
            return dt;
        }
        #endregion

    }
}
