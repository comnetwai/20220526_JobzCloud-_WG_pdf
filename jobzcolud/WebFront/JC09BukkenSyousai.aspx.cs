using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Configuration;
using MySql.Data.MySqlClient;
using Service;
using Common;
using jobzcolud.WebFront;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Collections;
using Ghostscript.NET.Rasterizer;
using jobzcolud.pdf;
using System.Web.UI.HtmlControls;

namespace JobzCloud.WebFront
{
    public partial class JC09BukkenSyousai : Page
    {
        MySqlConnection con = null;
        string sTOKUISAKIYUBIN = "";
        string sTOKUISAKIJUSYO1 = "";
        string sTOKUISAKIJUSYO2 = "";
        string sTOKUISAKITEL = "";
        string sTOKUISAKIFAX = "";

        string cSHIHARAI = "";
        string sSHIHARAI = "";
        MySqlCommand cmd = null;

        string sBUMON = "";
        string SYAKUSHOKU = "";
        string SKEISHOU = "";
        
        private string checkfolderPath = "";
        private string sSERVERPATH = "";
        private string sPath_Server = "";
        string sPATH_LOCAL_SOURCE = "";
        string sPATH_SERVER_SOURCE = "";
        string sPATH_SUB_DIR = "";

        private string sBukFlag = "営業用";
        private string sShiFlag = "指示書";
        String sql_Delete = "";

        Boolean saveSuccess = false;

        Double nGokei = 0;
        Double nArari = 0;

        public static int rmt_pcount1 = 0;//Page Numberの為に
        public static int pcount = 0;
        bool fMEISAI = false; //明細チェック
        bool fSYOUSAI = false; //詳細チェック
        bool fMIDASHI = false; //見出しチェック
        bool fHYOUSHI = false; //表紙チェック

        String cKyoten = "";
        bool f_isomoji_msg = true;
        public string nullbukken = string.Empty;//20220519 Chaw Added

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(Request.QueryString["id"]))
                {
                    if (!Page.IsPostBack)
                    {
                        try
                        {
                            divBukenShosai.Visible = false;
                            divMitsumoriJotai.Visible = false;

                            String id = Request.QueryString["id"];
                            DataTable dt_db = JC01Login_Class.Get_DB(id);
                            String db = dt_db.Rows[0]["db"].ToString();
                            String mail = dt_db.Rows[0]["mail"].ToString();
                            ConstantVal.DB_NAME = db;

                            JC_ClientConnecction_Class jc = new JC_ClientConnecction_Class();
                            jc.loginId = mail;
                            MySqlConnection cn = jc.GetConnection();
                            DataTable dt_loginuser = jc.GetLoginUserCodeFromClientDB();
                            lblLoginUserCode.Text = dt_loginuser.Rows[0]["code"].ToString();

                            JC99NavBar navbar_Master = (JC99NavBar)this.Master;
                            navbar_Master.LK_navbardrop.Style.Add(" background-color", "rgba(46,117,182)");
                            navbar_Master.navbar2.Visible = false;
                            JC99NavBar_Class Jc99 = new JC99NavBar_Class();
                            Jc99.loginId = mail;
                            Jc99.FindLoginName();
                            lblLoginUserName.Text = Jc99.LoginName;

                            if (Request.Cookies["colWidthbMitumori"] != null)
                            {
                                HF_GridSize.Value = Request.Cookies["colWidthbMitumori"].Value;
                            }
                            else
                            {
                                HF_GridSize.Value = "30,32,95,450,140,140,95,110,100,110,30";
                            }
                            if (Request.Cookies["colWidthbMitumoriGrid"] != null)
                            {
                                HF_Grid.Value = Request.Cookies["colWidthbMitumoriGrid"].Value;
                            }
                            else
                            {
                                HF_Grid.Value = "1350";
                            }


                            if (SessionUtility.GetSession("cBukken") == null)  //新規
                            {
                                nullbukken = "nullflag";//20220519 Chaw Added
                                DataTable dt = new DataTable();
                                dt.Columns.Add("cMITUMORI");
                                dt.Columns.Add("sMITUMORI");
                                dt.Columns.Add("sE_TANTOUSHA");
                                dt.Columns.Add("SakuseiSHA");
                                dt.Columns.Add("dMITUMORISAKUSEI");
                                dt.Columns.Add("nKINGAKU");
                                dt.Columns.Add("sJYOTAI");
                                dt.Columns.Add("nMITUMORIARARI");

                                GV_MitumoriSyousai.DataSource = dt;
                                GV_MitumoriSyousai.DataBind();

                                lblsTOKUISAKI.Text = "";
                                lblcTOKUISAKI.Text = "";

                                lblsTOKUISAKI_TAN.Text = "";
                                lblsTOKUISAKI_TAN_JUN.Text = "0";

                                upd_TOKUISAKI.Update();
                                upd_TOKUISAKI_TAN.Update();

                                BT_Delete.Visible = false;
                                DateTime cdate = DateTime.Now;

                                lblcJISHATANTOUSHA.Text = lblLoginUserCode.Text;
                                lblsJISHATANTOUSHA.Text = lblLoginUserName.Text;
                                divTantousyaBtn.Style["display"] = "none";
                                divTantousyaLabel.Style["display"] = "block";
                                lblsJISHATANTOUSHA.Attributes.Add("onClick", "BtnClick('MainContent_BT_JisyaTantousya_Add')");
                                upd_JISHATANTOUSHA.Update();
                                HF_isChange.Value = "1";

                                lblkingaku.Text = "0";
                                lblarari.Text = "0";
                                updHeader.Update();

                            }
                            else   //更新
                            {
                                nullbukken = "notnullflag";//20220519 Chaw Added
                                LB_BukkenCode.Text = Session["cBukken"].ToString();
                                getBukkenData();
                                getMitumoriData();
                            }

                        }
                        catch
                        {
                            Response.Redirect("JC01Login.aspx");
                        }
                    }
                    else
                    {
                        if (fileUpload1.HasFile)
                        {
                            BindImage();
                        }
                        if (fileUpload2.HasFile)
                        {
                            BT_ImageUpload_Click(sender, e);
                        }

                        if (divLabelSave.Style["display"] != "none")
                        {
                            divLabelSave.Style["display"] = "none";
                            updHeader.Update();
                        }
                    }
                    Session.Timeout = 480;
                }
                else
                {
                    Response.Redirect("JC01Login.aspx");
                }
            }
            catch
            {
                Response.Redirect("JC01Login.aspx");
            }
        }

        #region getBukkenData()
        protected void getBukkenData()
        {
            String id = Request.QueryString["id"];
            DataTable dt_db = JC01Login_Class.Get_DB(id);
            String db = dt_db.Rows[0]["db"].ToString();
            String mail = dt_db.Rows[0]["mail"].ToString();
            ConstantVal.DB_NAME = db;

            JC_ClientConnecction_Class jc = new JC_ClientConnecction_Class();
            jc.loginId = mail;
            con = jc.GetConnection();
            con.Open();
            string sql = "select distinct" +
                " ifnull(rb.cBUKKEN, '') as cBUKKEN," +
                " ifnull(rb.sBUKKEN, '') as sBUKKEN," +
                " ifnull(rb.cCREATE_TAN, '') as cCREATE_TAN," +
                " ifnull(mjt1.sTANTOUSHA, '') as sCREATE_TAN," +
                " date_format(rb.dCREATE, '%Y/%m/%d') as dCREATE," +
                " ifnull(rb.cHENKOUSYA, '') as cHENKOUSYA," +
                " ifnull(mjt2.sTANTOUSHA, '') as sHENKOUSHA," +
                " date_format(rb.dHENKOU, '%Y/%m/%d') as dHENKOU," +
                " ifnull(rb.cTOKUISAKI, '') as cTOKUISAKI," +
                " REPLACE(REPLACE(ifnull(rb.sTOKUISAKI, ''),'<','&lt'),'>','&gt') as sTOKUISAKI," +
                " REPLACE(REPLACE(ifnull(rb.sTOKUISAKI_TAN, ''),'<','&lt'),'>','&gt')  as sTOKUISAKI_TAN," +
                " ifnull(rb.sTOKUISAKI_TAN_Jun, '') as sTOKUISAKI_TAN_Jun," +
                " ifnull(rb.cTANTOUSHA, '') as cTANTOUSHA," +
                " REPLACE(REPLACE(ifnull(mjt.sTANTOUSHA, ''),'<','&lt'),'>','&gt') as sTANTOUSHA," +
                " ifnull(rb.sBIKOU, '') as sBIKOU," +
                " ifnull(rb.cPJ, '') as cPJ," +
                " date_format(rb.dNOUKI, '%Y/%m/%d') as dNOUKI," +
                " date_format(rb.dJikaitenken, '%Y/%m/%d') as dJikaitenken," +
               " date_format(rb.dJikaiokugai, '%Y/%m/%d') as dJikaiokugai," +
                " rb.cFILE as cFILE," +
                " mf.sFILE as sFILE," +
                " rb.cBukkenJYOUTAI as cJYOTAI," +
                " joutai.sJYOTAI as sJYOTAI," +
                " mf.sPATH_SERVER_SOURCE as sPATH_SERVER_SOURCE" +
                " from R_BUKKEN rb" +
                " left join M_J_TANTOUSHA mjt ON rb.cTANTOUSHA = mjt.cTANTOUSHA" +
                " left join M_J_TANTOUSHA mjt1 ON rb.cCREATE_TAN = mjt1.cTANTOUSHA" +
                " left join M_J_TANTOUSHA mjt2 ON rb.cHENKOUSYA = mjt2.cTANTOUSHA" +
                " left join m_buken_jyotai joutai ON rb.cBukkenJYOUTAI = joutai.cJYOTAI" +
                " left join m_file as mf ON rb.cFILE = mf.cFILE" +
                " where 1 = 1 and rb.fHYOUJI = '0' and rb.cBUKKEN like '%" + LB_BukkenCode.Text + "%' group by rb.cBUKKEN";
            cmd = new MySqlCommand(sql, con);
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();
            da.Dispose();
            if (dt.Rows.Count > 0)
            {
                TB_sBUKKEN.Text = dt.Rows[0]["sBUKKEN"].ToString();

                #region 得意先
                lblcTOKUISAKI.Text = dt.Rows[0]["cTOKUISAKI"].ToString();
                lblsTOKUISAKI.Text = dt.Rows[0]["sTOKUISAKI"].ToString();
                divTokuisakiBtn.Style["display"] = "none";
                divTokuisakiLabel.Style["display"] = "block";
                divTokuisakiSyosai.Style["display"] = "block";
                lblsTOKUISAKI.Attributes.Add("onClick", "BtnClick('MainContent_BT_sTOKUISAKI_Add')");
                upd_TOKUISAKI.Update();
                #endregion

                #region 得意先担当者
                if (!String.IsNullOrEmpty(dt.Rows[0]["sTOKUISAKI_TAN"].ToString()))
                {
                    lblsTOKUISAKI_TAN.Text = dt.Rows[0]["sTOKUISAKI_TAN"].ToString();
                    lblsTOKUISAKI_TAN_JUN.Text = dt.Rows[0]["sTOKUISAKI_TAN_Jun"].ToString();
                    divTokuisakiTanBtn.Style["display"] = "none";
                    divTokuisakiTanLabel.Style["display"] = "block";
                    divTokuisakiTanSyosai.Style["display"] = "block";
                    lblsTOKUISAKI_TAN.Attributes.Add("onClick", "BtnClick('MainContent_BT_sTOKUISAKI_TAN_Add')");
                    upd_TOKUISAKI_TAN.Update();
                }
                #endregion

                #region 自社担当者
                if (!String.IsNullOrEmpty(dt.Rows[0]["cTANTOUSHA"].ToString()))
                {
                    lblcJISHATANTOUSHA.Text = dt.Rows[0]["cTANTOUSHA"].ToString();
                    lblsJISHATANTOUSHA.Text = dt.Rows[0]["sTANTOUSHA"].ToString();
                    divTantousyaBtn.Style["display"] = "none";
                    divTantousyaLabel.Style["display"] = "block";
                    lblsJISHATANTOUSHA.Attributes.Add("onClick", "BtnClick('MainContent_BT_JisyaTantousya_Add')");
                    upd_JISHATANTOUSHA.Update();
                }
                #endregion

                #region 状態
                if (!String.IsNullOrEmpty(dt.Rows[0]["cJYOTAI"].ToString()) && dt.Rows[0]["cJYOTAI"].ToString() != "00")
                {
                    lblcJoutai.Text = dt.Rows[0]["cJYOTAI"].ToString();
                    lblsJoutai.Text = dt.Rows[0]["sJYOTAI"].ToString().Replace("<", "&lt").Replace(">", "&gt");
                    lblsJoutai.Attributes.Add("onClick", "BtnClick('MainContent_btnJoutai')");
                    divJoutaibtn.Style["display"] = "none";
                    divJoutaiLabel.Style["display"] = "block";
                    updJoutai.Update();
                }
                #endregion

                TB_sBIKOU.Text = dt.Rows[0]["sBIKOU"].ToString();

                TB_cfile.Text = dt.Rows[0]["cFILE"].ToString();
                TB_sfile.Text = dt.Rows[0]["sFILE"].ToString();

                txtfilepath.Text = dt.Rows[0]["sPATH_SERVER_SOURCE"].ToString();

                lblSakuseiSya.Text = dt.Rows[0]["sCREATE_TAN"].ToString();
                lblkoushinSya.Text = dt.Rows[0]["sHENKOUSHA"].ToString();
                lblcSakuseiSya.Text = dt.Rows[0]["cCREATE_TAN"].ToString();
                lblckoushinSya.Text = dt.Rows[0]["cHENKOUSYA"].ToString();
                lblSakusekiBi.Text = dt.Rows[0]["dCREATE"].ToString();
                lblkoushinBi.Text = dt.Rows[0]["dHENKOU"].ToString();
                tb_jishabangou.Text = dt.Rows[0]["cPJ"].ToString();
                lbldNouki.Text = dt.Rows[0]["dNOUKI"].ToString();
                if (!String.IsNullOrEmpty(lbldNouki.Text))
                {
                    btnNoukiDate.Style["display"] = "none";
                    divNoukiDate.Style["display"] = "block";
                    updNoukiDate.Update();
                    lbldNouki.Attributes.Add("onClick", "BtnClick('MainContent_btnNoukiDate')");
                    upddatePopup.Update();
                }
                lbldJikaitenken.Text = dt.Rows[0]["dJikaitenken"].ToString();
                if (!String.IsNullOrEmpty(lbldJikaitenken.Text))
                {
                    btnJikaitenkenDate.Style["display"] = "none";
                    divJikaitenkenDate.Style["display"] = "block";
                    updJikaitenkenDate.Update();
                    lbldJikaitenken.Attributes.Add("onClick", "BtnClick('MainContent_btnJikaitenkenDate')");
                    upddatePopup.Update();
                }
                lbldJikaiokugai.Text = dt.Rows[0]["dJikaiokugai"].ToString();
                if (!String.IsNullOrEmpty(lbldJikaiokugai.Text))
                {
                    btnJikaiokugaiDate.Style["display"] = "none";
                    divJikaiokugaiDate.Style["display"] = "block";
                    updJikaiokugaiDate.Update();
                    lbldJikaiokugai.Attributes.Add("onClick", "BtnClick('MainContent_btnJikaiokugaiDate')");
                    upddatePopup.Update();
                }

                HF_isChange.Value = "0";
                updHeader.Update();
            }
        }
        #endregion

        #region getMitumoriData()
        protected void getMitumoriData()
        {
            String id = Request.QueryString["id"];
            DataTable dt_db = JC01Login_Class.Get_DB(id);
            String db = dt_db.Rows[0]["db"].ToString();
            String mail = dt_db.Rows[0]["mail"].ToString();
            ConstantVal.DB_NAME = db;

            JC_ClientConnecction_Class jc = new JC_ClientConnecction_Class();
            jc.loginId = mail;
            con = jc.GetConnection();
            con.Open();
            string sql = "Select distinct" +
                " IfNull(R_MITUMORI.cMITUMORI, '') As cMITUMORI," +
                " REPLACE(REPLACE(IfNull(R_MITUMORI.sMITUMORI, ''),'<','&lt'),'>','&gt') As sMITUMORI," +
                " IfNull(M_J_TANTOUSHA.sTANTOUSHA, '') As sE_TANTOUSHA," +
                " IfNull(M_J_TANTOUSHA.sTANTOUSHA, '') As SakuseiSHA," +
                " date_format(R_MITUMORI.dMITUMORISAKUSEI, '%y/%m/%d') As dMITUMORISAKUSEI," +
                " format(IfNull(R_MITUMORI.nKINGAKU, 0),0) As nKINGAKU," +
                " case IfNull(R_MITUMORI.cJYOTAI_MITUMORI, '')" +
                " when '00' then '失注'" +
                " when '01' then '見積提出済'" +
                " when '02' then '受注'" +
                " when '03' then '完了'" +
                " when '04' then '見積作成中'" +
                " when '05' then 'キャンセル'" +
                " when '06' then '売上済み'" +
                " else ''" +
                " END As sJYOTAI," +
                " format(IfNull(R_MITUMORI.nMITUMORIARARI, 0),0) As nMITUMORIARARI" +
                " From R_MITUMORI" +
                " RIGHT join (SELECT DISTINCT r_mitumori.cMITUMORI AS M_C, MAX(r_mitumori.cMITUMORI_KO) AS M_KO FROM r_mitumori GROUP BY r_mitumori.cMITUMORI) AS M ON M.M_C = r_mitumori.cMITUMORI AND M.M_KO = r_mitumori.cMITUMORI_KO" +
                " left join R_BU_MITSU ON R_BU_MITSU.cMITUMORI = R_MITUMORI.cMITUMORI" +
                " left join M_J_TANTOUSHA ON M_J_TANTOUSHA.cTANTOUSHA = R_MITUMORI.cEIGYOTANTOSYA" +
                " Where '1' = '1' and R_BU_MITSU.cBUKKEN = '" + LB_BukkenCode.Text + "' group by R_MITUMORI.cMITUMORI order by R_MITUMORI.cMITUMORI desc";

            cmd = new MySqlCommand(sql, con);
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();
            da.Dispose();
            if (dt.Rows.Count > 0)
            {
                GV_MitumoriSyousai.DataSource = dt;
                GV_MitumoriSyousai.DataBind();
                lblMitumoriNai.Visible = false;
                setnMITUMORI_JYOTAI();
                // GV_MitumoriSyousai.SelectedIndex = 1;
            }
            else
            {
                GV_MitumoriSyousai.DataSource = dt;
                GV_MitumoriSyousai.DataBind();
                lblMitumoriNai.Visible = true;
            }
            Double nGokei = 0;
            Double nArari = 0;
            foreach (DataRow dr in dt.Rows)
            {
                nGokei += Convert.ToDouble(dr["nKINGAKU"].ToString().Replace(",", ""));
                nArari += Convert.ToDouble(dr["nMITUMORIARARI"].ToString().Replace(",", ""));
            }

            lblkingaku.Text = nGokei.ToString("#,##0.##");
            lblarari.Text = nArari.ToString("#,##0.##");
            updHeader.Update();
        }
        #endregion

        #region BindImage()
        public void BindImage()
        {
            //for (int i = 0; i < Request.Files.Count; i++)
            //{
            foreach (HttpPostedFile postedFile in fileUpload1.PostedFiles)
            {
                Boolean issave = true;
                HttpPostedFile file = postedFile;
                string filename = Path.GetFileName(file.FileName);
                string ext = Path.GetExtension(file.FileName);
                if (ext.ToLower().Contains("gif") || ext.ToLower().Contains("jpg") || ext.ToLower().Contains("jpeg") || ext.ToLower().Contains("png") || ext.ToLower().Contains("jfif"))
                {
                    if (filename.Length <= 100)
                    {
                        System.IO.Stream fs = file.InputStream;
                        System.Drawing.Image originalImage = System.Drawing.Image.FromStream(fs);
                        int introtate = 0;
                        if (originalImage.PropertyIdList.Contains(0x0112))
                        {
                            introtate = originalImage.GetPropertyItem(0x0112).Value[0];
                            switch (introtate)
                            {
                                case 1: // landscape, do nothing
                                    break;

                                case 8: // rotated 90 right
                                        // de-rotate:
                                    originalImage.RotateFlip(rotateFlipType: RotateFlipType.Rotate270FlipNone);
                                    break;

                                case 3: // bottoms up
                                    originalImage.RotateFlip(rotateFlipType: RotateFlipType.Rotate180FlipNone);
                                    break;

                                case 6: // rotated 90 left
                                    originalImage.RotateFlip(rotateFlipType: RotateFlipType.Rotate90FlipNone);
                                    break;
                            }
                        }
                        try
                        {
                            var i2 = new Bitmap(originalImage);
                            EncoderParameters myEncoderParameters = new EncoderParameters(1);
                            EncoderParameter myEncoderParameter = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 75L);
                            myEncoderParameters.Param[0] = myEncoderParameter;
                            //i2.Save(folderPath + filename, GetEncoderInfo("image/jpeg"), myEncoderParameters);
                            //var i2 = new Bitmap(originalImage);
                            //i2.Save(folderPath + filename);
                            //originalImage.Save(folderPath + filename);
                        }
                        catch { }
                        using (var stream = new MemoryStream())
                        {
                            originalImage.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                            byte[] imageByte = stream.ToArray();
                            if (file.ContentLength > 23552)//about 23KB
                            {
                                imageByte = ResizeImageFile(imageByte, 760);
                            }
                            string base64String = Convert.ToBase64String(imageByte);
                            string imgurl = "data:image/png;base64," + base64String;
                            // imgDaihyou.Src = imgurl;
                            txtfilepath.Text = Server.MapPath(fileUpload1.FileName).ToString();
                            //BT_Save.Enabled = true;
                            HF_isChange.Value = "1";
                            updHeader.Update();
                        }
                    }
                    else
                    {
                        Response.Write("<script language='javascript'>window.alert('ファイルの名が正しくありません。確認してください。');</script>");
                    }
                }
                else
                {
                    Response.Write("<script language='javascript'>window.alert('ファイルの種類が正しくありません。確認してください。');</script>");
                }
            }
        }

        #endregion

        #region ResizeImageFile()
        public static byte[] ResizeImageFile(byte[] imageFile, int targetSize) // Set targetSize to 760
        {
            using (System.Drawing.Image oldImage = System.Drawing.Image.FromStream(new MemoryStream(imageFile)))
            {
                Size newSize = CalculateDimensions(oldImage.Size, targetSize);
                using (Bitmap newImage = new Bitmap(newSize.Width, newSize.Height, PixelFormat.Format24bppRgb))
                {
                    using (Graphics canvas = Graphics.FromImage(newImage))
                    {
                        canvas.SmoothingMode = SmoothingMode.AntiAlias;
                        canvas.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        canvas.PixelOffsetMode = PixelOffsetMode.HighQuality;
                        canvas.DrawImage(oldImage, new Rectangle(new Point(0, 0), newSize));
                        MemoryStream m = new MemoryStream();
                        newImage.Save(m, ImageFormat.Jpeg);
                        return m.GetBuffer();
                    }
                }
            }
        }
        #endregion

        #region CalculateDimensions()
        public static Size CalculateDimensions(Size oldSize, int targetSize)
        {
            Size newSize = new Size();
            if (oldSize.Height > oldSize.Width)
            {
                newSize.Width = (int)(oldSize.Width * ((float)targetSize / (float)oldSize.Height));
                newSize.Height = targetSize;
            }
            else
            {
                newSize.Width = targetSize;
                newSize.Height = (int)(oldSize.Height * ((float)targetSize / (float)oldSize.Width));
            }
            return newSize;
        }
        #endregion

        #region BT_fileUpload_Click
        protected void BT_fileUpload_Click(object sender, EventArgs e)
        {
            if (fileUpload1.HasFile)
            {
                BindImage();
            }
        }
        #endregion

        #region BT_Shinki_Click
        protected void BT_Shinki_Click(object sender, EventArgs e)
        {
            if (HF_isChange.Value == "1")
            {
                //checkData();
                //if (HF_checkData.Value == "1")
                //{
                HF_flagBtn.Value = "0"; //物件新規
                updHeader.Update();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowAnkenChangeMessage",
                "ShowKoumokuChangesConfirmMessage('項目が変更されています。保存しますか？','" + btnYes.ClientID + "','" + btnNo.ClientID + "','" + btnCancel.ClientID + "');", true);
                //}
            }
            else
            {
                Session["cBukken"] = null;
                if (!String.IsNullOrEmpty(Request.QueryString["id"]))
                {
                    String id = Request.QueryString["id"];
                    Response.Redirect("JC09BukkenSyousai.aspx?id=" + id);
                    BT_sTOKUISAKI_TAN_Cross_Click1(sender, e);
                }
                else
                {
                    Response.Redirect("JC01Login.aspx");
                }
            }


        }
        #endregion

        #region getTOKUISAKI_INFO
        //得意先情報を取る
        private void getTOKUISAKI_INFO(string code)
        {
            try
            {
                if (lblcTOKUISAKI.Text != "")
                {
                    String id = Request.QueryString["id"];
                    DataTable dt_db = JC01Login_Class.Get_DB(id);
                    String db = dt_db.Rows[0]["db"].ToString();
                    String mail = dt_db.Rows[0]["mail"].ToString();
                    ConstantVal.DB_NAME = db;

                    JC_ClientConnecction_Class jc = new JC_ClientConnecction_Class();
                    jc.loginId = mail;
                    con = jc.GetConnection();
                    string sqlTokuisaki = " select  sTOKUISAKI1, cYUUBIN, sJUUSHO1, sJUUSHO2, sTEL, sFAX,cSHIHARAI,sSHIHARAI from m_tokuisaki where cTOKUISAKI =" + code + "";
                    MySqlCommand cmd = new MySqlCommand(sqlTokuisaki, con);
                    cmd.CommandTimeout = 0;
                    con.Close();
                    con.Open();
                    MySqlDataReader dr = cmd.ExecuteReader();

                    sTOKUISAKIYUBIN = "null";
                    sTOKUISAKIJUSYO1 = "null";
                    sTOKUISAKIJUSYO2 = "null";
                    sTOKUISAKITEL = "null";
                    sTOKUISAKIFAX = "null";
                    cSHIHARAI = "null";
                    sSHIHARAI = "null";
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            lblsTOKUISAKI.Text = dr["sTOKUISAKI1"].ToString();
                            if (!String.IsNullOrEmpty(dr["cYUUBIN"].ToString()))
                            {
                                sTOKUISAKIYUBIN = "'"+dr["cYUUBIN"].ToString()+"'";
                            }
                            if (!String.IsNullOrEmpty(dr["sJUUSHO1"].ToString()))
                            {
                                sTOKUISAKIJUSYO1 = "'" + dr["sJUUSHO1"].ToString()+ "'" ;
                            }
                            if (!String.IsNullOrEmpty(dr["sJUUSHO2"].ToString()))
                            {
                                sTOKUISAKIJUSYO2 = "'" + dr["sJUUSHO2"].ToString()+ "'" ;
                            }
                            if (!String.IsNullOrEmpty(dr["sTEL"].ToString()))
                            {
                                sTOKUISAKITEL = "'" + dr["sTEL"].ToString()+"'";
                            }

                            if (!String.IsNullOrEmpty(dr["sFAX"].ToString()))
                            {
                                sTOKUISAKIFAX = "'" + dr["sFAX"].ToString() + "'";
                            }
                            if (!String.IsNullOrEmpty(dr["cSHIHARAI"].ToString()))
                            {
                                cSHIHARAI = "'" + dr["cSHIHARAI"].ToString() + "'";
                            }
                            if (!String.IsNullOrEmpty(dr["sSHIHARAI"].ToString()))
                            {
                                sSHIHARAI = "'" + dr["sSHIHARAI"].ToString() + "'";
                            }
                        }
                        dr.Close();
                    }
                    con.Close();

                }
                else
                {
                }
            }
            catch (Exception ex)
            {

            }
        }
        #endregion

        #region BT_MitumoriAdd_Click
        protected void BT_MitumoriAdd_Click(object sender, EventArgs e)
        {
            if (HF_isChange.Value == "1")
            {
                //checkData();
                //if (HF_checkData.Value == "1")
                //{
                HF_flagBtn.Value = "1"; //見積を追加
                updHeader.Update();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowAnkenChangeMessage",
                "ShowKoumokuChangesConfirmMessage('項目が変更されています。保存しますか？','" + btnYes.ClientID + "','" + btnNo.ClientID + "','" + btnCancel.ClientID + "');", true);
                //}
            }
            else
            {
                Session["cBukken"] = LB_BukkenCode.Text;
                Session["sBukken"] = TB_sBUKKEN.Text;
                Session["cPJ"] = tb_jishabangou.Text;//20220422 Chaw Added
                Session["cMitumori"] = null;  //見積新規作成
                Session["fBukkenName"] = "true";
                if (!String.IsNullOrEmpty(Request.QueryString["id"]))
                {
                    String id = Request.QueryString["id"];
                    Response.Redirect("JC10MitsumoriTouroku.aspx?id=" + id);
                }
                else
                {
                    Response.Redirect("JC01Login.aspx");
                }
            }

        }
        #endregion

        #region BT_MitumoriCopyAdd_Click
        protected void BT_MitumoriCopyAdd_Click(object sender, EventArgs e)
        {
            if (HF_isChange.Value == "1")
            {
                //checkData();
                //if (HF_checkData.Value == "1")
                //{
                HF_flagBtn.Value = "2"; //他見積をコピーして追加
                updHeader.Update();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowAnkenChangeMessage",
                "ShowKoumokuChangesConfirmMessage('項目が変更されています。保存しますか？','" + btnYes.ClientID + "','" + btnNo.ClientID + "','" + btnCancel.ClientID + "');", true);
                //}
            }
            else
            {
                Session["cBukken"] = LB_BukkenCode.Text;
                SessionUtility.SetSession("HOME", "Master");
                String id = Request.QueryString["id"];
                ifShinkiPopup.Src = "JC12MitsumoriKensaku.aspx?id=" + id;
                mpeShinkiPopup.Show();
                updShinkiPopup.Update();
            }
        }
        #endregion

        #region BT_sJISHATANTOUSHA_Cross_Click
        protected void BT_sJISHATANTOUSHA_Cross_Click(object sender, EventArgs e)
        {
            lblsJISHATANTOUSHA.Text = "";
            divTantousyaBtn.Style["display"] = "block";
            divTantousyaLabel.Style["display"] = "none";
            //BT_Save.Enabled = true;
            HF_isChange.Value = "1";
            updHeader.Update();
        }
        #endregion

        #region BT_sTOKUISAKI_Syousai_Click
        protected void BT_sTOKUISAKI_Syousai_Click(object sender, EventArgs e)
        {
            Session["fTokuisakiSyosai"] = "null";
            Session["cTokuisakiBukken"] = lblcTOKUISAKI.Text;
            //Response.Redirect("JC19TokuisakiSyousai.aspx");
            if (!String.IsNullOrEmpty(Request.QueryString["id"]))
            {
                String id = Request.QueryString["id"];
                Response.Write("<script language='javascript'>window.open('JC19TokuisakiSyousai.aspx?id=" + id+"', '_blank');</script>");
            }
            else
            {
                Response.Redirect("JC01Login.aspx");
            }
        }
        #endregion

        #region BT_sTOKUISAKI_TAN_Syousai_Click
        protected void BT_sTOKUISAKI_TAN_Syousai_Click(object sender, EventArgs e)
        {
            Session["fTokuisakiSyosai"] = "null";
            Session["cTokuisakiBukken"] = lblcTOKUISAKI.Text;
            //Response.Redirect("JC19TokuisakiSyousai.aspx");
            if (!String.IsNullOrEmpty(Request.QueryString["id"]))
            {
                String id = Request.QueryString["id"];
                Response.Write("<script language='javascript'>window.open('JC19TokuisakiSyousai.aspx?id=" + id + "', '_blank');</script>");
            }
            else
            {
                Response.Redirect("JC01Login.aspx");
            }

        }
        #endregion

        #region BT_sTOKUISAKI_TAN_Add_Click
        protected void BT_sTOKUISAKI_TAN_Add_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(lblcTOKUISAKI.Text))
            {
                SessionUtility.SetSession("HOME", "Master");
                Session["cTokuisakiBukken"] = lblcTOKUISAKI.Text;
                Session["flag"] = "bukken";
                //ifSentakuPopup.Style["width"] = "1100px";
                //ifSentakuPopup.Style["height"] = "650px";
                //ifSentakuPopup.Src = "JC20TokuisakiTantouKensaku.aspx";
                //mpeSentakuPopup.Show();
                String id = Request.QueryString["id"];
                ifShinkiPopup.Src = "JC20TokuisakiTantouKensaku.aspx?id=" + id;
                mpeShinkiPopup.Show();

                lblsTOKUISAKI_TAN.Attributes.Add("onClick", "BtnClick('MainContent_BT_sTOKUISAKI_TAN_Add')");
                updShinkiPopup.Update();
                //updSentakuPopup.Update();
            }
        }
        #endregion

        #region btnClose_Click
        protected void btnClose_Click(object sender, EventArgs e)
        {
            ifSentakuPopup.Src = "";
            mpeSentakuPopup.Hide();
            updSentakuPopup.Update();
        }
        #endregion

        #region btnTokuisakiTantouSelect_Click
        protected void btnTokuisakiTantouSelect_Click(object sender, EventArgs e)
        {
            if (Session["TOUKUISAKITANTOU"] != null)
            {
                string tokuisakitantou = (string)Session["TOUKUISAKITANTOU"];
                string njun = (string)Session["TokuisakiTanJun"];
                lblsTOKUISAKI_TAN.Text = tokuisakitantou;
                lblsTOKUISAKI_TAN_JUN.Text = njun;
                divTokuisakiTanBtn.Style["display"] = "none";
                divTokuisakiTanLabel.Style["display"] = "block";
                divTokuisakiTanSyosai.Style["display"] = "block";
                upd_TOKUISAKI_TAN.Update();

                //BT_Save.Enabled = true;
                HF_isChange.Value = "1";
                updHeader.Update();
            }

            ifSentakuPopup.Src = "";
            mpeSentakuPopup.Hide();
            updSentakuPopup.Update();
        }
        #endregion

        #region BT_JisyaTantousya_Add_Click
        protected void BT_JisyaTantousya_Add_Click(object sender, EventArgs e)
        {
            SessionUtility.SetSession("HOME", "Master");
            Session["isKensaku"] = "false"; //担当者
            //ifSentakuPopup.Style["width"] = "715px";
            ifSentakuPopup.Style["width"] = "100vw";
            ifSentakuPopup.Style["height"] = "100vh";
            String id = Request.QueryString["id"];
            ifSentakuPopup.Src = "JC14TantouKensaku.aspx?id=" + id;
            mpeSentakuPopup.Show();

            lblsJISHATANTOUSHA.Attributes.Add("onClick", "BtnClick('MainContent_BT_JisyaTantousya_Add')");
            updSentakuPopup.Update();

        }
        #endregion

        #region btnJishaTantouSelect_Click
        protected void btnJishaTantouSelect_Click(object sender, EventArgs e)
        {
            if (Session["JISHAcTANTOUSHA"] != null)
            {
                string ctantou = (string)Session["JISHAcTANTOUSHA"];
                string stantou = (string)Session["JISHAsTANTOUSHA"];
                if (!String.IsNullOrEmpty(ctantou))
                {
                    lblcJISHATANTOUSHA.Text = ctantou;
                    lblsJISHATANTOUSHA.Text = stantou;
                    divTantousyaBtn.Style["display"] = "none";
                    divTantousyaLabel.Style["display"] = "block";
                    upd_JISHATANTOUSHA.Update();
                }
                else
                {
                    BT_sJISHATANTOUSHA_Cross_Click(sender, e);
                }
                //BT_Save.Enabled = true;
                HF_isChange.Value = "1";
                updHeader.Update();
            }

            ifSentakuPopup.Src = "";
            mpeSentakuPopup.Hide();
            updSentakuPopup.Update();
        }
        #endregion

        #region BT_sTOKUISAKI_Add_Click
        protected void BT_sTOKUISAKI_Add_Click(object sender, EventArgs e)
        {
            SessionUtility.SetSession("HOME", "Master");
            Session["flag"] = "bukken";
            //ifSentakuPopup.Style["width"] = "1160px";
            //ifSentakuPopup.Style["height"] = "650px";
            //ifSentakuPopup.Src = "JC18TokuisakiKensaku.aspx";
            Session["ftokuisakisyosai"] = "0";
            String id = Request.QueryString["id"];
            ifShinkiPopup.Src = "JC18TokuisakiKensaku.aspx?id=" + id;
            mpeShinkiPopup.Show();
            //mpeSentakuPopup.Show();

            lblsTOKUISAKI.Attributes.Add("onClick", "BtnClick('MainContent_BT_sTOKUISAKI_Add')");

            updShinkiPopup.Update();
            //updSentakuPopup.Update();
        }
        #endregion

        #region btnTokuisakiSelect_Click
        protected void btnTokuisakiSelect_Click(object sender, EventArgs e)
        {
            if (Session["cTOUKUISAKI"] != null)
            {
                string ctokuisaki = (string)Session["cTOUKUISAKI"];
                string stokuisaki = (string)Session["sTOUKUISAKI"];

                string tantouCarry = (string)Session["STANTOUCarry"];
                lblcTOKUISAKI.Text = ctokuisaki;
                lblsTOKUISAKI.Text = stokuisaki;
                if (tantouCarry != null)
                {
                    lblsTOKUISAKI_TAN.Text = tantouCarry;
                    lblsTOKUISAKI_TAN.Attributes.Add("onClick", "BtnClick('MainContent_BT_sTOKUISAKI_TAN_Add')");
                    lblsTOKUISAKI_TAN_JUN.Text = "1";
                    divTokuisakiTanBtn.Style["display"] = "none";
                    divTokuisakiTanLabel.Style["display"] = "block";
                    divTokuisakiTanSyosai.Style["display"] = "block";
                    upd_TOKUISAKI_TAN.Update();

                    ifSentakuPopup.Src = "";
                    mpeSentakuPopup.Hide();
                    updSentakuPopup.Update();
                    Session["STANTOUCarry"] = null;
                }

                divTokuisakiBtn.Style["display"] = "none";
                divTokuisakiLabel.Style["display"] = "block";
                divTokuisakiSyosai.Style["display"] = "block";
                BT_sTOKUISAKI_Add.BorderStyle = BorderStyle.None;
                upd_TOKUISAKI.Update();

                if (tantouCarry == null)
                {
                    BT_sTOKUISAKI_TAN_Cross_Click1(sender, e);
                }
            }

            //ifSentakuPopup.Src = "";
            //mpeSentakuPopup.Hide();
            //updSentakuPopup.Update();
            ifShinkiPopup.Src = "";
            mpeShinkiPopup.Hide();
            updShinkiPopup.Update();
        }
        #endregion

        #region getTOKUISAKI_Tantou
        private void getTOKUISAKI_Tantou(string code)
        {
            String id = Request.QueryString["id"];
            DataTable dt_db = JC01Login_Class.Get_DB(id);
            String db = dt_db.Rows[0]["db"].ToString();
            String mail = dt_db.Rows[0]["mail"].ToString();
            ConstantVal.DB_NAME = db;

            JC_ClientConnecction_Class jc = new JC_ClientConnecction_Class();
            jc.loginId = mail;
            con = jc.GetConnection();
            con.Open();
            string tokuisaki_tantou = " select sBUMON as sBUMON ,SYAKUSHOKU as SYAKUSHOKU,SKEISHOU as SKEISHOU from tokuisaki_tantousha where cTOKUISAKI='" + code + "' and NJUNBAN='" + lblsTOKUISAKI_TAN_JUN.Text + "';  ";
            MySqlCommand cmd = new MySqlCommand(tokuisaki_tantou, con);
            cmd.CommandTimeout = 0;
            con.Close();
            con.Open();
            MySqlDataReader dr = cmd.ExecuteReader();

            sBUMON = "null";
            SYAKUSHOKU = "null";
            SKEISHOU = "null";

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    if (!String.IsNullOrEmpty(dr["sBUMON"].ToString()))
                    {
                        sBUMON = "'"+dr["sBUMON"].ToString()+"'";
                    }
                    if (!String.IsNullOrEmpty(dr["SYAKUSHOKU"].ToString()))
                    {
                        SYAKUSHOKU = "'" + dr["SYAKUSHOKU"].ToString() + "'";
                    }
                    if (!String.IsNullOrEmpty(dr["SKEISHOU"].ToString()))
                    {
                        SKEISHOU = "'" + dr["SKEISHOU"].ToString() + "'";
                    }
                }
                dr.Close();
            }
            con.Close();
        }
        #endregion

        #region checkData
        private void checkData()
        {
            if (TB_sBUKKEN.Text != "" && lblcTOKUISAKI.Text != "")
            {
                TB_sBUKKEN.BorderColor = System.Drawing.Color.Empty;
                TB_sBUKKEN.BorderStyle = BorderStyle.NotSet;
                TB_sBUKKEN.BorderWidth = 1;

                BT_sTOKUISAKI_Add.BorderStyle = BorderStyle.None;
                HF_checkData.Value = "1";

            }
            else
            {
                HF_checkData.Value = "0";
                if (TB_sBUKKEN.Text == "")
                {
                    TB_sBUKKEN.BorderColor = System.Drawing.Color.Red;
                    TB_sBUKKEN.BorderStyle = BorderStyle.Double;
                    TB_sBUKKEN.BorderWidth = 2;
                }
                if (lblcTOKUISAKI.Text == "")
                {
                    BT_sTOKUISAKI_Add.BorderColor = System.Drawing.Color.Red;
                    BT_sTOKUISAKI_Add.BorderStyle = BorderStyle.Double;
                    BT_sTOKUISAKI_Add.BorderWidth = 2;
                }
                updHeader.Update();
            }
        }
        #endregion

        #region BT_Save_Click
        protected void BT_Save_Click(object sender, EventArgs e)
        {
            //string confirmValue = Request.Form["confirm_value"];
            //string confirmValue = hiddenFieldId.Value;
            //if (confirmValue == "Yes")
            //{
            saveSuccess = false;
            checkData();

            #region 20220519 Chaw Added 
            String id = Request.QueryString["id"];
            DataTable dt_db = JC01Login_Class.Get_DB(id);
            String db = dt_db.Rows[0]["db"].ToString();
            String mail = dt_db.Rows[0]["mail"].ToString();
            ConstantVal.DB_NAME = db;

            JC_ClientConnecction_Class jc = new JC_ClientConnecction_Class();
            jc.loginId = mail;
            con = jc.GetConnection();
            if (!String.IsNullOrEmpty(LB_BukkenCode.Text))//20220520 Eaindary,Chaw Added
            {
                string select = "select cPJ from r_bukken where cBUKKEN = '" + LB_BukkenCode.Text + "'";
                MySqlCommand cmm = new MySqlCommand(select, con);
                cmm.CommandTimeout = 0;
                con.Close();
                con.Open();
                MySqlDataReader mdrr = cmm.ExecuteReader();
                string cPJ = "";
                if (mdrr.HasRows)
                {
                    while (mdrr.Read())
                    {

                        cPJ = mdrr[0].ToString();
                    }
                    mdrr.Close();
                }
                con.Close();
                //20220520 Eaindary,Chaw Added Start
                con.Open();
                string selectBukken = "select cMITUMORI from r_bu_mitsu where cBUKKEN = '" + LB_BukkenCode.Text + "'";
                MySqlCommand cmd = new MySqlCommand(selectBukken, con);
                MySqlDataReader reader = cmd.ExecuteReader();
                string cMitumori = "";
                while (reader.Read())
                {
                    cMitumori = reader.GetValue(0).ToString();
                }
                con.Close();
                if (cPJ != tb_jishabangou.Text && cMitumori != "")//20220520 Eaindary,Chaw Added End
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "DeleteConfirmMessage", "DeleteConfirmBox('自社番号が変わりましたので見積、売上画面の自社番号を更新してもよろしいでしょうか？','" + btnYesJisha.ClientID + "');", true);
                }
                else
                {
                    save();
                }
            }
            else
            {
                save();
            }

            #endregion
        }
        #endregion

        #region lnkBukkenToroku_Click
        protected void lnkBukkenToroku_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(Request.QueryString["id"]))
            {
                String id = Request.QueryString["id"];
                Response.Redirect("JC09BukkenSyousai.aspx?id=" + id);
            }
            else
            {
                Response.Redirect("JC01Login.aspx");
            }
        }
        #endregion

        #region BT_sTOKUISAKI_TAN_Cross_Click1
        protected void BT_sTOKUISAKI_TAN_Cross_Click1(object sender, EventArgs e)
        {
            lblsTOKUISAKI_TAN.Text = "";
            divTokuisakiTanBtn.Style["display"] = "block";
            divTokuisakiTanLabel.Style["display"] = "none";
            divTokuisakiTanSyosai.Style["display"] = "none";
            upd_TOKUISAKI_TAN.Update();

            //BT_Save.Enabled = true;
            HF_isChange.Value = "1";
            updHeader.Update();
        }
        #endregion

        #region btn_CloseMitumoriSearch_Click
        protected void btn_CloseMitumoriSearch_Click(object sender, EventArgs e)
        {
            ifShinkiPopup.Src = "";
            mpeShinkiPopup.Hide();
            updShinkiPopup.Update();
            if (Session["cMitumori"] != null)
            {
                if (!String.IsNullOrEmpty(Request.QueryString["id"]))
                {
                    String id = Request.QueryString["id"];
                    Response.Redirect("JC10MitsumoriTouroku.aspx?id=" + id);
                }
                else
                {
                    Response.Redirect("JC01Login.aspx");
                }
            }
        }
        #endregion

        #region btn_CloseTokuisakiSentaku_Click
        protected void btn_CloseTokuisakiSentaku_Click(object sender, EventArgs e)
        {
            ifShinkiPopup.Src = "";
            mpeShinkiPopup.Hide();
            updShinkiPopup.Update();
        }
        #endregion

        #region TB_sBUKKEN_TextChanged
        protected void TB_sBUKKEN_TextChanged(object sender, EventArgs e)
        {
            //BT_Save.Enabled = true;
            HF_isChange.Value = "1";
            TB_sBUKKEN.BorderColor = System.Drawing.Color.Empty;
            TB_sBUKKEN.BorderStyle = BorderStyle.NotSet;
            TB_sBUKKEN.BorderWidth = 1;
            updHeader.Update();
        }
        #endregion

        #region TB_sBIKOU_TextChanged
        protected void TB_sBIKOU_TextChanged(object sender, EventArgs e)
        {
            //BT_Save.Enabled = true;
            HF_isChange.Value = "1";
            updHeader.Update();
        }
        #endregion

        #region 確認メッセージの「はい」ボタン
        protected void btnYes_Click(object sender, EventArgs e)
        {
            BT_Save_Click(sender, e);
            if (saveSuccess == true)
            {
                if (HF_flagBtn.Value == "0")  //物件新規
                {
                    Session["cBukken"] = null;
                    if (!String.IsNullOrEmpty(Request.QueryString["id"]))
                    {
                        String id = Request.QueryString["id"];
                        Response.Redirect("JC09BukkenSyousai.aspx?id=" + id);
                    }
                    else
                    {
                        Response.Redirect("JC01Login.aspx");
                    }
                    BT_sTOKUISAKI_TAN_Cross_Click1(sender, e);
                }
                else if (HF_flagBtn.Value == "1") //見積を追加
                {
                    Session["cBukken"] = LB_BukkenCode.Text;
                    Session["cPJ"] = tb_jishabangou.Text;//20220422 Chaw Added
                    Session["cMitumori"] = null;  //見積新規作成
                    if (!String.IsNullOrEmpty(Request.QueryString["id"]))
                    {
                        String id = Request.QueryString["id"];
                        Response.Redirect("JC10MitsumoriTouroku.aspx?id=" + id);
                    }
                    else
                    {
                        Response.Redirect("JC01Login.aspx");
                    }
                }
                else if (HF_flagBtn.Value == "2") //他見積をコピーして追加
                {
                    Session["cBukken"] = LB_BukkenCode.Text;
                    SessionUtility.SetSession("HOME", "Master");
                    String id = Request.QueryString["id"];
                    ifShinkiPopup.Src = "JC12MitsumoriKensaku.aspx?id=" + id;
                    mpeShinkiPopup.Show();
                    updShinkiPopup.Update();
                }
            }
        }
        #endregion

        #region 確認メッセージの「いいえ」ボタン
        protected void btnNo_Click(object sender, EventArgs e)
        {
            if (HF_flagBtn.Value == "0")  //物件新規
            {
                Session["cBukken"] = null;
                if (!String.IsNullOrEmpty(Request.QueryString["id"]))
                {
                    String id = Request.QueryString["id"];
                    Response.Redirect("JC09BukkenSyousai.aspx?id=" + id);
                }
                else
                {
                    Response.Redirect("JC01Login.aspx");
                }
                BT_sTOKUISAKI_TAN_Cross_Click1(sender, e);
            }
            else if (HF_flagBtn.Value == "1") //見積を追加
            {
                Session["cBukken"] = LB_BukkenCode.Text;
                Session["cMitumori"] = null;  //見積新規作成
                if (!String.IsNullOrEmpty(Request.QueryString["id"]))
                {
                    String id = Request.QueryString["id"];
                    Response.Redirect("JC10MitsumoriTouroku.aspx?id=" + id);
                }
                else
                {
                    Response.Redirect("JC01Login.aspx");
                }
            }
            else if (HF_flagBtn.Value == "2") //他見積をコピーして追加
            {
                Session["cBukken"] = LB_BukkenCode.Text;
                SessionUtility.SetSession("HOME", "Master");
                String id = Request.QueryString["id"];
                ifShinkiPopup.Src = "JC12MitsumoriKensaku.aspx?id=" + id;
                mpeShinkiPopup.Show();
                updShinkiPopup.Update();
            }
        }
        #endregion

        #region 確認メッセージの「キャンセル」ボタン
        protected void btnCancel_Click(object sender, EventArgs e)
        {

        }
        #endregion

        #region グリッドの見積コードはをクリックする「見積登録＆明細画面へ」
        protected void LK_cMitumori_Click(object sender, EventArgs e)
        {
            LinkButton lk_cMitumori = sender as LinkButton;
            Session["cMitumori"] = lk_cMitumori.Text;
            if (!String.IsNullOrEmpty(Request.QueryString["id"]))
            {
                String id = Request.QueryString["id"];
                Response.Redirect("JC10MitsumoriTouroku.aspx?id=" + id);
            }
            else
            {
                Response.Redirect("JC01Login.aspx");
            }
        }
        #endregion

        #region グリッドのコをクリックする「見積登録＆明細画面へ」
        protected void BT_KO_Click(object sender, EventArgs e)
        {
            var btko = sender as Button;
            if (btko != null)
            {
                GridViewRow gvr = (GridViewRow)btko.NamingContainer;
                int rowindex = gvr.RowIndex;
                string lk_cMitumori = (GV_MitumoriSyousai.Rows[rowindex].FindControl("cMITUMORI") as LinkButton).Text;
                Session["cMitumori"] = lk_cMitumori;
                Session["btko"] = btko;
                Session["fBukkenName"]=null;
                if (!String.IsNullOrEmpty(Request.QueryString["id"]))
                {
                    String id = Request.QueryString["id"];
                    Response.Redirect("JC10MitsumoriTouroku.aspx?id=" + id);
                }
                else
                {
                    Response.Redirect("JC01Login.aspx");
                }
            }
        }
        #endregion

        #region 物件納期を選択
        protected void btnNoukiDate_Click(object sender, EventArgs e)
        {
            SessionUtility.SetSession("HOME", "Master");
            String id = Request.QueryString["id"];
            ifdatePopup.Src = "JCHidukeSelect.aspx?id=" + id;
            mpedatePopup.Show();

            ViewState["DATETIME"] = btnNoukiDate.ID;

            if (!String.IsNullOrEmpty(lbldNouki.Text))
            {
                DateTime dt = DateTime.Parse(lbldNouki.Text);
                Session["CALENDARDATE"] = dt.ToString("yyyy/MM/dd");
                Session["YEAR"] = dt.Year;
            }

            // 設定したラベルをクリック時、時間設定ポップアップ画面を表示する
            lbldNouki.Attributes.Add("onClick", "BtnClick('MainContent_btnNoukiDate')");
            upddatePopup.Update();
        }
        #endregion

        #region btndNoukiCross_Click
        protected void btndNoukiCross_Click(object sender, EventArgs e)
        {
            lbldNouki.Text = "";
            btnNoukiDate.Style["display"] = "block";
            divNoukiDate.Style["display"] = "none";
            updNoukiDate.Update();
        }
        #endregion

        #region NoukiDateDataBind
        protected void NoukiDateDataBind(string strCalendarDateTime, string strImgbtnID)
        {
            DateTime dtDeliveryDate = DateTime.Parse(strCalendarDateTime);
            lbldNouki.Text = dtDeliveryDate.ToString("yyyy/MM/dd");
            btnNoukiDate.Style["display"] = "none";
            divNoukiDate.Style["display"] = "block";
            updNoukiDate.Update();
        }
        #endregion

        #region 次回点検日を選択
        protected void btnJikaitenkenDate_Click(object sender, EventArgs e)
        {
            SessionUtility.SetSession("HOME", "Master");
            String id = Request.QueryString["id"];
            ifdatePopup.Src = "JCHidukeSelect.aspx?id=" + id;
            mpedatePopup.Show();

            ViewState["DATETIME"] = btnJikaitenkenDate.ID;

            if (!String.IsNullOrEmpty(lbldJikaitenken.Text))
            {
                DateTime dt = DateTime.Parse(lbldJikaitenken.Text);
                Session["CALENDARDATE"] = dt.ToString("yyyy/MM/dd");
                Session["YEAR"] = dt.Year;
            }

            // 設定したラベルをクリック時、時間設定ポップアップ画面を表示する
            lbldJikaitenken.Attributes.Add("onClick", "BtnClick('MainContent_btnJikaitenkenDate')");
            upddatePopup.Update();
        }
        #endregion

        #region btndJikaitenkenCross_Click
        protected void btndJikaitenkenCross_Click(object sender, EventArgs e)
        {
            lbldJikaitenken.Text = "";
            btnJikaitenkenDate.Style["display"] = "block";
            divJikaitenkenDate.Style["display"] = "none";
            updJikaitenkenDate.Update();
        }
        #endregion

        #region 
        protected void JikaiDateDataBind(string strCalendarDateTime, string strImgbtnID)
        {
            DateTime dtDeliveryDate = DateTime.Parse(strCalendarDateTime);
            lbldJikaitenken.Text = dtDeliveryDate.ToString("yyyy/MM/dd");
            btnJikaitenkenDate.Style["display"] = "none";
            divJikaitenkenDate.Style["display"] = "block";
            updJikaitenkenDate.Update();
        }
        #endregion

        #region 次回点検日を選択
        protected void btnJikaiokugaiDate_Click(object sender, EventArgs e)
        {
            SessionUtility.SetSession("HOME", "Master");
            String id = Request.QueryString["id"];
            ifdatePopup.Src = "JCHidukeSelect.aspx?id=" + id;
            mpedatePopup.Show();

            ViewState["DATETIME"] = btnJikaiokugaiDate.ID;

            if (!String.IsNullOrEmpty(lbldJikaiokugai.Text))
            {
                DateTime dt = DateTime.Parse(lbldJikaiokugai.Text);
                Session["CALENDARDATE"] = dt.ToString("yyyy/MM/dd");
                Session["YEAR"] = dt.Year;
            }

            // 設定したラベルをクリック時、時間設定ポップアップ画面を表示する
            lbldJikaiokugai.Attributes.Add("onClick", "BtnClick('MainContent_btnJikaiokugaiDate')");
            upddatePopup.Update();
        }
        #endregion

        #region btndJikaiokugaiCross_Click
        protected void btndJikaiokugaiCross_Click(object sender, EventArgs e)
        {
            lbldJikaiokugai.Text = "";
            btnJikaiokugaiDate.Style["display"] = "block";
            divJikaiokugaiDate.Style["display"] = "none";
            updJikaiokugaiDate.Update();
        }
        #endregion

        #region 
        protected void JikaiokugaiDateDataBind(string strCalendarDateTime, string strImgbtnID)
        {
            DateTime dtDeliveryDate = DateTime.Parse(strCalendarDateTime);
            lbldJikaiokugai.Text = dtDeliveryDate.ToString("yyyy/MM/dd");
            btnJikaiokugaiDate.Style["display"] = "none";
            divJikaiokugaiDate.Style["display"] = "block";
            updJikaiokugaiDate.Update();
        }
        #endregion

        #region "日付カレンダーポップアップの【X】ボタンクリック処理"
        protected void btnCalendarClose_Click(object sender, EventArgs e)
        {
            // 【日付サブ画面】を閉じる
            CloseSentakuSub();
            // フォーカスする
            CalendarFoucs();
        }
        #endregion

        #region "日付カレンダーポップアップの【設定】ボタンを押す処理"
        protected void btnCalendarSettei_Click(object sender, EventArgs e)
        {
            DateTime dtSelectedDate;

            // 【日付サブ画面】を閉じる
            CloseSentakuSub();
            string strBtnID = (string)ViewState["DATETIME"];
            string strCalendarDateTime = (string)Session["CALENDARDATETIME"];
            if (Session["CALENDARDATETIME"] != null)
            {
                strCalendarDateTime = (string)Session["CALENDARDATETIME"];
                dtSelectedDate = DateTime.Parse(strCalendarDateTime);
                if (strBtnID == btnNoukiDate.ID)
                {
                    NoukiDateDataBind(strCalendarDateTime, strBtnID);
                }
                else if (strBtnID == btnJikaitenkenDate.ID)
                {
                    JikaiDateDataBind(strCalendarDateTime, strBtnID);
                }
                else if (strBtnID == btnJikaiokugaiDate.ID)
                {
                    JikaiokugaiDateDataBind(strCalendarDateTime, strBtnID);
                }
                HF_isChange.Value = "1";
                updHeader.Update();
                //lblHdnAnkenTextChange.Text = "true";
            }
            CalendarFoucs();
        }

        #endregion

        #region "選択日付サブ画面を閉じる処理"
        protected void CloseSentakuSub()
        {
            ifdatePopup.Src = "";
            mpedatePopup.Hide();
            upddatePopup.Update();
        }
        #endregion

        #region "日付サブ画面を閉じる時のフォーカス処理"
        protected void CalendarFoucs()
        {
            string strBtnID = (string)ViewState["DATETIME"];
            if (strBtnID == btnNoukiDate.ID)
            {
                if (btnNoukiDate.Style["display"] != "none")
                {
                    btnNoukiDate.Focus();
                }

            }
            else if (strBtnID == btnJikaitenkenDate.ID)
            {
                if (btnJikaitenkenDate.Style["display"] != "none")
                {
                    btnJikaitenkenDate.Focus();
                }
            }
            else if (strBtnID == btnJikaiokugaiDate.ID)
            {
                if (btnJikaiokugaiDate.Style["display"] != "none")
                {
                    btnJikaiokugaiDate.Focus();
                }
            }
        }
        #endregion

        #region BT_BukkenShosai_Click
        protected void BT_BukkenShosai_Click(object sender, EventArgs e)
        {
            if (divBukenShosai.Visible == true)
            {
                BT_BukkenShosai.Style.Add("background-image", "url('../Img/expand-more-1782315-1514165.png')");
                divBukenShosai.Visible = false;
            }
            else
            {
                BT_BukkenShosai.Style.Add("background-image", "url('../Img/expand-less-1781206-1518580.png')");
                divBukenShosai.Visible = true;
            }
        }
        #endregion

        #region BT_MitsumoriJotai_Click
        protected void BT_MitsumoriJotai_Click(object sender, EventArgs e)
        {
            if (divMitsumoriJotai.Visible == true)
            {
                BT_MitsumoriJotai.Style.Add("background-image", "url('../Img/expand-more-1782315-1514165.png')");
                divMitsumoriJotai.Visible = false;
            }
            else
            {
                BT_MitsumoriJotai.Style.Add("background-image", "url('../Img/expand-less-1781206-1518580.png')");
                divMitsumoriJotai.Visible = true;
            }
        }
        #endregion

        #region setnMITUMORI_JYOTAI
        private void setnMITUMORI_JYOTAI()
        {
            long nsakuseichu = 0;  //add by yamin 20191218
            long nteishutsu = 0;
            long nshichu = 0;
            long njuchu = 0;
            long nuriagezumi = 0;
            long nmukou = 0;

            decimal nsakuseichukingaku = 0;
            decimal nteishutsukingaku = 0;
            decimal nshichukingaku = 0;
            decimal njuchukingaku = 0;
            decimal nuriagezumikingaku = 0;
            decimal nmukoukingaku = 0;

            for (int i = 0; i < GV_MitumoriSyousai.Rows.Count; i++)
            {
                lblkingaku.Text = (GV_MitumoriSyousai.Rows[0].FindControl("nKINGAKU") as Label).Text;
                lblarari.Text = (GV_MitumoriSyousai.Rows[0].FindControl("nMITUMORIARARI") as Label).Text;
                lblmitsumori.Text = GV_MitumoriSyousai.Rows.Count.ToString() + "件";

                if ((GV_MitumoriSyousai.Rows[i].FindControl("sJYOTAI") as Label).Text == "受注")
                {
                    njuchu++;
                    njuchukingaku = njuchukingaku + decimal.Parse((GV_MitumoriSyousai.Rows[i].FindControl("nKINGAKU") as Label).Text);
                }
                else if ((GV_MitumoriSyousai.Rows[i].FindControl("sJYOTAI") as Label).Text == "売上済")
                {
                    nuriagezumi++;
                    nuriagezumikingaku = nuriagezumikingaku + decimal.Parse((GV_MitumoriSyousai.Rows[i].FindControl("nKINGAKU") as Label).Text);
                }
                else if ((GV_MitumoriSyousai.Rows[i].FindControl("sJYOTAI") as Label).Text == "失注")
                {
                    nshichu++;
                    nshichukingaku = nshichukingaku + decimal.Parse((GV_MitumoriSyousai.Rows[i].FindControl("nKINGAKU") as Label).Text);
                }
                else if ((GV_MitumoriSyousai.Rows[i].FindControl("sJYOTAI") as Label).Text == "提出済")
                {
                    nteishutsu++;
                    nteishutsukingaku = nteishutsukingaku + decimal.Parse((GV_MitumoriSyousai.Rows[i].FindControl("nKINGAKU") as Label).Text);
                }
                else if ((GV_MitumoriSyousai.Rows[i].FindControl("sJYOTAI") as Label).Text == "作成中")
                {
                    nsakuseichu++;
                    nsakuseichukingaku = nsakuseichukingaku + decimal.Parse((GV_MitumoriSyousai.Rows[i].FindControl("nKINGAKU") as Label).Text);
                }
                else if ((GV_MitumoriSyousai.Rows[i].FindControl("sJYOTAI") as Label).Text == "無効")
                {
                    nmukou++;
                    nmukoukingaku = nmukoukingaku + decimal.Parse((GV_MitumoriSyousai.Rows[i].FindControl("nKINGAKU") as Label).Text);
                }
                #region add by  見積状態と合計金額
                lblnsakuseichu.Text = nsakuseichu.ToString() + "件";
                lblnteishutsu.Text = nteishutsu.ToString() + "件";
                lblnjuchu.Text = njuchu.ToString() + "件";
                lblnuriagezumi.Text = nuriagezumi.ToString() + "件";
                lblnshitsuchu.Text = nshichu.ToString() + "件";
                lblnmukou.Text = nmukou.ToString() + "件";
                lblnsakuseichukingaku.Text = String.Format("{0:n0}", nsakuseichukingaku); // nsakuseichukingaku.ToString();
                lblnteishutsukingaku.Text = String.Format("{0:n0}", nteishutsukingaku); // nteishutsukingaku.ToString();
                lblnjuchukingaku.Text = String.Format("{0:n0}", njuchukingaku); // njuchukingaku.ToString();
                lblnuriagezumikingaku.Text = String.Format("{0:n0}", nuriagezumikingaku); //nuriagezumikingaku.ToString();
                lblnshitsuchukingaku.Text = String.Format("{0:n0}", nshichukingaku); //nshichukingaku.ToString();
                lblnmukoukingaku.Text = String.Format("{0:n0}", nmukoukingaku); // nmukoukingaku.ToString();
                #endregion
            }
        }
        #endregion

        #region GV_MitumoriSyousai_SelectedIndexChanged
        protected void GV_MitumoriSyousai_SelectedIndexChanged(object sender, EventArgs e)
        {
            int row = GV_MitumoriSyousai.SelectedRow.RowIndex;
            lblkingaku.Text = (GV_MitumoriSyousai.Rows[row].FindControl("nKINGAKU") as Label).Text;
            lblarari.Text = (GV_MitumoriSyousai.Rows[row].FindControl("nMITUMORIARARI") as Label).Text;
            // updMitsumoriSyosaiGrid.Update();
            updHeader.Update();
        }
        #endregion

        #region GV_MitumoriSyousai_RowDataBound
        protected void GV_MitumoriSyousai_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(GV_MitumoriSyousai, "Select$" + e.Row.RowIndex);
                //e.Row.Attributes["style"] = "cursor:pointer";
            }
        }
        #endregion

        #region BT_ImageUpload_Click
        protected void BT_ImageUpload_Click(object sender, EventArgs e)
        {
            foreach (HttpPostedFile postedFile in fileUpload2.PostedFiles)
            {
                string fileName = Path.GetFileName(postedFile.FileName);
                if (fileName != null && fileName != "")
                {
                    string ext = Path.GetExtension(postedFile.FileName);
                    if (ext.ToLower().Contains("gif") || ext.ToLower().Contains("jpg") || ext.ToLower().Contains("jpeg") || ext.ToLower().Contains("png") || ext.ToLower().Contains("jfif"))
                    {
                        if (fileName.Length <= 100)
                        {
                            System.IO.Stream fs = postedFile.InputStream;
                            System.Drawing.Image originalImage = System.Drawing.Image.FromStream(fs);
                            int introtate = 0;
                            if (originalImage.PropertyIdList.Contains(0x0112))
                            {
                                introtate = originalImage.GetPropertyItem(0x0112).Value[0];
                                switch (introtate)
                                {
                                    case 1: // landscape, do nothing
                                        break;

                                    case 8: // rotated 90 right
                                            // de-rotate:
                                        originalImage.RotateFlip(rotateFlipType: RotateFlipType.Rotate270FlipNone);
                                        break;

                                    case 3: // bottoms up
                                        originalImage.RotateFlip(rotateFlipType: RotateFlipType.Rotate180FlipNone);
                                        break;

                                    case 6: // rotated 90 left
                                        originalImage.RotateFlip(rotateFlipType: RotateFlipType.Rotate90FlipNone);
                                        break;
                                }
                            }
                            try
                            {
                                var i2 = new Bitmap(originalImage);
                                EncoderParameters myEncoderParameters = new EncoderParameters(1);
                                EncoderParameter myEncoderParameter = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 75L);
                                myEncoderParameters.Param[0] = myEncoderParameter;
                            }
                            catch { }
                            using (var stream = new MemoryStream())
                            {
                                originalImage.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                                byte[] imageByte = stream.ToArray();
                                if (postedFile.ContentLength > 23552)//about 23KB
                                {
                                    imageByte = ResizeImageFile(imageByte, 760);
                                }
                                string base64String = Convert.ToBase64String(imageByte);
                                string imgurl = "data:image/png;base64," + base64String;

                                Image1.ImageUrl = imgurl;//Draw Image
                                divImage.Style["display"] = "block";
                                divBTImageUpload.Style["display"] = "none";

                                #region getcFILE
                                String id = Request.QueryString["id"];
                                DataTable dt_db = JC01Login_Class.Get_DB(id);
                                String db = dt_db.Rows[0]["db"].ToString();
                                String mail = dt_db.Rows[0]["mail"].ToString();
                                ConstantVal.DB_NAME = db;

                                string cFILE = "";
                                JC_ClientConnecction_Class jc = new JC_ClientConnecction_Class();
                                jc.loginId = mail;
                                con = jc.GetConnection();
                                string sql_mfile = "select MAX(cFILE)+1 as cFILE from M_FILE where (cFILE<>'' and cFILE is not null); ";
                                MySqlCommand cmd = new MySqlCommand(sql_mfile, con);
                                cmd.CommandTimeout = 0;
                                con.Close();
                                con.Open();
                                MySqlDataReader dr = cmd.ExecuteReader();
                                if (dr.HasRows)
                                {
                                    while (dr.Read())
                                    {
                                        cFILE = dr[0].ToString();
                                    }
                                    dr.Close();
                                }
                                cFILE = cFILE.ToString().PadLeft(10, '0');
                                TB_cfile.Text = cFILE;
                                TB_sfile.Text = fileName;
                                con.Close();
                                #endregion

                                string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                                string shijifolderPath = folderPath + "\\DEMO20" + "\\TEMP" + "\\" + sShiFlag;
                                fileUpload2.SaveAs(shijifolderPath + "\\" + fileName);

                                string icontempfolderPath = folderPath + "\\DEMO20" + "\\ICONTEMP" + "\\BukkenPrint";
                                File.Copy(shijifolderPath + "\\" + fileName, icontempfolderPath + "\\" + fileName, true);

                                //BT_Save.Enabled = true;
                                HF_isChange.Value = "1";
                                updHeader.Update();
                            }
                        }
                        else
                        {
                            //Response.Write("<script language='javascript'>window.alert('ファイルの名が正しくありません。確認してください。');</script>");
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowErrorMessage",
                   "ShowErrorMessage('ファイルの名が正しくありません。<br/>確認してください。');", true);
                        }
                    }
                    else if (ext.ToLower().Contains("pdf") || ext.ToLower().Contains("ai"))
                    {
                        if (fileName.Length <= 100)
                        {
                            System.IO.Stream fs = postedFile.InputStream;
                            GhostscriptRasterizer rasterizer = null;

                            using (rasterizer = new GhostscriptRasterizer())
                            {
                                rasterizer.Open(fs);

                                using (MemoryStream ms = new MemoryStream())
                                {
                                    int pagecount = rasterizer.PageCount;
                                    if (pagecount == 1)
                                    {
                                        System.Drawing.Image img = rasterizer.GetPage(200, 1);
                                        img.Save(ms, ImageFormat.Png);

                                        var SigBase64 = Convert.ToBase64String(ms.GetBuffer()); //Get Base64
                                        String imgurl = "data:image/png;base64," + SigBase64;
                                        Image1.ImageUrl = imgurl;//Draw Image
                                        divImage.Style["display"] = "block";
                                        divBTImageUpload.Style["display"] = "none";
                                    }
                                    else
                                    {
                                        byte[] fileBytes = fileUpload2.FileBytes;
                                        HF_ImgBase64.Value = Convert.ToBase64String(fileBytes);
                                        updHeader.Update();
                                        SessionUtility.SetSession("HOME", "Master");
                                        Session["pageNumber"] = pagecount;
                                        //ifSentakuPopup.Style["width"] = "350px";
                                        //ifSentakuPopup.Style["height"] = "100px";
                                        ifSentakuPopup.Style["width"] = "100vw";
                                        ifSentakuPopup.Style["height"] = "100vh";
                                        String id = Request.QueryString["id"];
                                        ifSentakuPopup.Src = "JC35PdfPageChoice.aspx?id=" + id;
                                        mpeSentakuPopup.Show();
                                        updSentakuPopup.Update();
                                    }
                                }
                                rasterizer.Close();
                            }
                        }
                        else
                        {
                            //Response.Write("<script language='javascript'>window.alert('ファイルの名が正しくありません。確認してください。');</script>");
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowErrorMessage",
                    "ShowErrorMessage('ファイルの名が正しくありません。<br/>確認してください。');", true);
                        }
                    }
                    else
                    {
                        //Response.Write("<script language='javascript'>window.alert('ファイルの種類が正しくありません。確認してください。');</script>");
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowErrorMessage",
                    "ShowErrorMessage('ファイルの種類が正しくありません。<br/>確認してください。');", true);
                    }
                }
            }
        }
        #endregion

        #region BT_ImageDelete_Click
        protected void BT_ImageDelete_Click(object sender, EventArgs e)
        {
            Image1.ImageUrl = "";
            divImage.Style["display"] = "none";
            divBTImageUpload.Style["display"] = "flex";
            TB_cfile.Text = "";
            TB_sfile.Text = "";
        }
        #endregion

        #region SetPhotoRoot
        private void SetPhotoRoot(string root, string nam)
        {
            string filePath = "";
            string filePath_f = "";
            string name = nam;
            // name = System.IO.Path.GetFileNameWithoutExtension(name);
            string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            filePath = folderPath + "\\DEMO20" + "\\ICONTEMP\\" + "BukkenPrint";
            int i = root.IndexOf("\\営業用");
            if (i == -1)
            {
                i = root.IndexOf("\\指示書");
                filePath_f = root.Substring(0, i) + "BukkenPrint";
            }
            else
            {
                int j = root.IndexOf("\\指示書");
                if (j != -1 && j < i)
                {
                    filePath_f = root.Substring(0, j) + "BukkenPrint";
                }
                else
                {
                    filePath_f = root.Substring(0, i) + "BukkenPrint";
                }
            }

            string filePath_temp = filePath_f + "\\Temp.jpg";
            filePath_f += "\\" + name;

            if (Directory.Exists(filePath) == false)
            {
                Directory.CreateDirectory(filePath);
            }
            if (Image1.ImageUrl != "")
            {
                Image1.ImageUrl = "";
            }

            filePath += "\\" + name;
            if (File.Exists(filePath) == false)
            {
                try
                {
                    File.Copy(filePath_f, filePath);
                }
                catch { }
            }
            else
            {
                try
                {
                    File.Delete(filePath);
                    File.Copy(filePath_f, filePath);
                }
                catch { }
            }
            if (filePath == null)
            {
                if (Image1.ImageUrl != null) Image1.ImageUrl = "";
                Image1.ImageUrl = System.Environment.CurrentDirectory + @"\res\Error.jpg";
            }
            else
            {
                if (Image1.ImageUrl != "") Image1.ImageUrl = "";
                try
                {
                    byte[] byteData = System.IO.File.ReadAllBytes(filePath);

                    //now convert byte[] to Base64
                    string base64String = Convert.ToBase64String(byteData);
                    string imgurl = string.Format("data:image/png;base64,{0}", base64String);

                    Image1.ImageUrl = imgurl;
                    divImage.Style["display"] = "block";
                    divBTImageUpload.Style["display"] = "none";

                }
                catch { }
            }
        }

        #endregion

        #region getsSERVERPATH
        public void getsSERVERPATH()
        {
            String id = Request.QueryString["id"];
            DataTable dt_db = JC01Login_Class.Get_DB(id);
            String db = dt_db.Rows[0]["db"].ToString();
            String mail = dt_db.Rows[0]["mail"].ToString();
            ConstantVal.DB_NAME = db;

            JC_ClientConnecction_Class jc = new JC_ClientConnecction_Class();
            jc.loginId = mail;
            con = jc.GetConnection();
            con.Open();
            string sPATH = " SELECT sPATH FROM m_set where cSET = '01'; ";
            cmd = new MySqlCommand(sPATH, con);
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable dtsPATH = new DataTable();
            da.Fill(dtsPATH);
            con.Close();
            da.Dispose();
            if (dtsPATH.Rows.Count > 0)
            {
                sSERVERPATH = dtsPATH.Rows[0]["sPATH"].ToString();
                sPath_Server = dtsPATH.Rows[0]["sPATH"].ToString();
            }

        }
        #endregion

        #region BT_Delete_Click
        protected void BT_Delete_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "DeleteConfirmMessage",
                    "DeleteConfirmBox('物件に紐付けている見積と売上を削除しても<br/>よろしいでしょうか？','" + btnDeleteBukken.ClientID + "');", true);
        }
        #endregion

        #region 見積状態を選択
        protected void btnJoutai_Click(object sender, EventArgs e)
        {
            SessionUtility.SetSession("HOME", "Master");
            ifSentakuPopup.Style["width"] = "455px";
            ifSentakuPopup.Style["height"] = "620px";
            String id = Request.QueryString["id"];
            ifSentakuPopup.Src = "JC25_BukkenJyoutai.aspx?id=" + id;
            mpeSentakuPopup.Show();

            lblsJoutai.Attributes.Add("onClick", "BtnClick('MainContent_btnJoutai')");
            updSentakuPopup.Update();
        }
        #endregion

        #region btnJoutaiSelect_Click()  見積状態サブ画面を閉じる時のフォーカス処理
        protected void btnJoutaiSelect_Click(object sender, EventArgs e)
        {
            if (Session["cJyotai"] != null)
            {
                string cJoutai = (string)Session["cJyotai"];
                string sJoutai = (string)Session["sJyotai"];
                lblcJoutai.Text = cJoutai;
                lblsJoutai.Text = sJoutai.Replace("<", "&lt").Replace(">", "&gt");
                divJoutaibtn.Style["display"] = "none";
                divJoutaiLabel.Style["display"] = "block";
                updJoutai.Update();
            }

            ifSentakuPopup.Src = "";
            mpeSentakuPopup.Hide();
            updSentakuPopup.Update();
            updHeader.Update();
        }

        #endregion

        #region BT_LBSaveCross_Click
        protected void BT_LBSaveCross_Click(object sender, EventArgs e)
        {
            divLabelSave.Style["display"] = "none";
        }
        #endregion

        #region btnDeleteBukken_Click  //物件削除
        protected void btnDeleteBukken_Click(object sender, EventArgs e)
        {
            String id = Request.QueryString["id"];
            DataTable dt_db = JC01Login_Class.Get_DB(id);
            String db = dt_db.Rows[0]["db"].ToString();
            String mail = dt_db.Rows[0]["mail"].ToString();
            ConstantVal.DB_NAME = db;

            JC_ClientConnecction_Class jc = new JC_ClientConnecction_Class();
            jc.loginId = mail;
            con = jc.GetConnection();
            sql_Delete = "DELETE FROM r_bukken WHERE cBUKKEN='"+LB_BukkenCode.Text+"';";
            sql_Delete += "DELETE FROM h_bukken WHERE cBUKKEN='"+LB_BukkenCode.Text+"';";
            sql_Delete += "DELETE FROM m_bu_file WHERE cBUKKEN='"+LB_BukkenCode.Text+"';";
            DeleteFileByBukken();
            DeleteSekou(LB_BukkenCode.Text, 1);//物件
            sql_Delete += "DELETE FROM r_bu_mitsu WHERE cBUKKEN='"+LB_BukkenCode.Text+"';";
            DeleteMitumoriByBukken();
            MySqlCommand cmdDelete = new MySqlCommand();
            MySqlTransaction tr = null;
            con.Open();
            try
            {
                tr = con.BeginTransaction();
                cmdDelete.Transaction = tr;
                cmdDelete.CommandTimeout = 0;
                cmdDelete = new MySqlCommand(sql_Delete, con);
                cmdDelete.ExecuteNonQuery();
                tr.Commit();
                if (!String.IsNullOrEmpty(Request.QueryString["id"]))
                {
                    id = Request.QueryString["id"];
                    Response.Redirect("JC30BukkenList.aspx?id=" + id);
                }
                else
                {
                    Response.Redirect("JC01Login.aspx");
                }
            }
            catch (Exception ex)
            {
                try
                {
                    tr.Rollback();
                }
                catch
                {
                }
            }
            con.Close();
        }
        #endregion

        #region DeleteFileByBukken
        public void DeleteFileByBukken()
        {
            String sql = "SELECT cFile FROM m_bu_file Where cBUKKEN='" + LB_BukkenCode.Text + "';";
            con.Open();
            cmd = new MySqlCommand(sql, con);
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();
            da.Dispose();
            foreach(DataRow dr in dt.Rows)
            {
                sql_Delete += "DELETE FROM m_file WHERE cFILE='"+dr[0].ToString()+"';";
            }
        }
        #endregion

        #region DeleteFileByMitumori
        public void DeleteFileByMitumori(String cMitumori)
        {
            String sql = "SELECT cFILE FROM m_mitsu_file Where cMITUMORI='" + cMitumori + "';";
            con.Open();
            cmd = new MySqlCommand(sql, con);
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();
            da.Dispose();
            foreach (DataRow dr in dt.Rows)
            {
                sql_Delete += "DELETE FROM m_file WHERE cFILE='" + dr[0].ToString() + "';";
            }
        }
        #endregion

        #region DeleteFileByShijisyo
        public void DeleteFileByShijisyo(String cShijisyo)
        {
            String sql = "SELECT cFILE FROM m_shijisyo_file Where cSHIJISYO='" + cShijisyo + "';";
            con.Open();
            cmd = new MySqlCommand(sql, con);
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();
            da.Dispose();
            foreach (DataRow dr in dt.Rows)
            {
                sql_Delete += "DELETE FROM m_file WHERE cFILE='" + dr[0].ToString() + "';";
            }
        }
        #endregion

        #region DeleteMitumoriByBukken
        public void DeleteMitumoriByBukken()
        {
            String sql = "SELECT cMITUMORI FROM r_bu_mitsu Where cBUKKEN='"+LB_BukkenCode.Text+"';";
            con.Open();
            cmd = new MySqlCommand(sql, con);
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();
            da.Dispose();
            foreach (DataRow dr in dt.Rows)
            {
                String cMitumori = dr[0].ToString();
                sql_Delete += "Delete From r_mitumori where  cMITUMORI='"+cMitumori+"';";
                sql_Delete += "Delete From r_mitumori_m where  cMITUMORI='"+cMitumori+"';";
                sql_Delete += "Delete From r_mitumori_m2 where  cMITUMORI='"+cMitumori+"';";
                sql_Delete += "delete from h_mitumori where 1=1 and cMITUMORI='"+cMitumori+"';";
                sql_Delete += "Delete From m_mitsu_file where  cMITUMORI='"+cMitumori+"';";
                DeleteFileByMitumori(cMitumori);
                DeleteSekou(cMitumori, 2);//見積
                sql_Delete += "delete from r_shijisyo_mitsu where 1=1 and cMITUMORI='"+cMitumori+"';";
                DeleteShijishoByMitumori(cMitumori);
                sql_Delete += "delete from sekisankoumoku_mitsu where cMITUMORI='" + cMitumori + "' ;";


                sql_Delete += "delete from M_SEKISANKOUMOKU_M where cMITUMORI='" + cMitumori + "';";
                sql_Delete += "delete from M_CYOUKOKUSEKISAN where cMITUMORI='" + cMitumori + "';";
                sql_Delete += "delete from M_INKUJIXEXTUTO where cMITUMORI='" + cMitumori + "';";
                sql_Delete += "delete from m_kyoutsuu where cMITUMORI='" + cMitumori + "';";
                sql_Delete += "delete from r_router where cMITUMORI='" + cMitumori + "';";
                sql_Delete += "delete from r_pattern where cMITUMORI='" + cMitumori + "';";
                sql_Delete += "Delete From r_uri_mitsu where cMITUMORI='"+cMitumori+"';";
                DeleteUriageByMitumori(cMitumori);
            }
        }
        #endregion

        #region DeleteShijishoByMitumori
        public void DeleteShijishoByMitumori(String cMitumori)
        {
            String sql = "SELECT cSHIJISYO FROM r_shijisyo_mitsu Where cMitumori='" + cMitumori+"';";
            con.Open();
            cmd = new MySqlCommand(sql, con);
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();
            da.Dispose();
            foreach (DataRow dr in dt.Rows)
            {
                String cShijisyo = dr[0].ToString();
                sql_Delete += "Delete From r_bu_shijisyo Where '1'='1'  And cBUKKEN='"+LB_BukkenCode.Text+"' And cSHIJISYO='"+cShijisyo+"';";
                sql_Delete += "Delete From m_shijisyo_file where cSHIJISYO='"+cShijisyo+"';";
                DeleteFileByShijisyo(cShijisyo);
                DeleteSekou(cShijisyo, 3);//指示書
                sql_Delete += "Delete From r_shijisyo where cSHIJISYO='"+cShijisyo+"';";
                sql_Delete += "Delete From h_shijisyo where cSHIJISYO='"+cShijisyo+"';";
                sql_Delete += "Delete From r_shiji_sagyo where cSHIJISYO='"+cShijisyo+"';";
                sql_Delete += "Delete From r_shiji_sagyonaiyo where cSHIJISYO='"+cShijisyo+"';";
                sql_Delete += "Delete From r_shiji_sagyotitle where cSHIJISYO='" + cShijisyo + "';";
                sql_Delete += "Delete From r_sjs_bunrui where cSHIJISYO = '"+cShijisyo+"';";
                sql_Delete += "Delete From r_shijisyo_bunrui where cSHIJISYO='" + cShijisyo + "';";
            }
        }
        #endregion

        #region DeleteUriageByMitumori
        public void DeleteUriageByMitumori(String cMitumori)
        {
            String sql = "SELECT cURIAGE FROM r_uri_mitsu Where cMITUMORI='" + cMitumori + "';";
            con.Open();
            cmd = new MySqlCommand(sql, con);
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();
            da.Dispose();
            foreach (DataRow dr in dt.Rows)
            {
                String cUriage = dr[0].ToString();
                sql_Delete += "Delete From r_uriage where cURIAGE='"+cUriage+"';";
                sql_Delete += "Delete From r_uriage_m where cURIAGE='"+cUriage+"'";
            }
        }
        #endregion

        #region DeleteSekou
        public void DeleteSekou(String code,int flag)
        {
            String sql = "";
            if (flag == 1)  //物件
            {
                sql = "SELECT rs.cSEKOU as cSEKOU FROM r_sekouyotei rs where rs.cBUKKEN='" + code + "';";
            }
            else if (flag == 2)  //見積
            {
                sql = "SELECT rs.cSEKOU as cSEKOU FROM r_sekouyotei rs where rs.cMITUMORI='" + code + "';";
            }
            else if (flag == 3)  //指示書
            {
                sql = "SELECT rs.cSEKOU as cSEKOU FROM r_sekouyotei rs where rs.cSHIJISYO='" + code + "';";
            }
            con.Open();
            cmd = new MySqlCommand(sql, con);
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();
            da.Dispose();
            foreach (DataRow dr in dt.Rows)
            {
                String csekou = dr[0].ToString();
                sql_Delete += "DELETE FROM r_sekouyotei WHERE cSEKOU='" + csekou + "';";
                sql_Delete += "DELETE FROM r_sankasya WHERE cSEKOU='" + csekou+ "';";
                sql_Delete += "DELETE FROM r_syaryo WHERE cSEKOU='" + csekou + "';";
            }
        }
        #endregion

        #region 見積削除
        protected void lnkbtnMitumoriDelete_Click(object sender, EventArgs e)
        {
            GridViewRow gvrow = (sender as LinkButton).NamingContainer as GridViewRow;
            string mitsumoriId = (gvrow.FindControl("cMITUMORI") as LinkButton).Text;
            HF_cMitumori.Value = mitsumoriId;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "DeleteConfirmMessage",
                    "DeleteConfirmBox('見積に紐付けている売上を削除しても<br/>よろしいでしょうか？','" + btnDeleteMitumori.ClientID + "');", true);
            updHeader.Update();
        }

        protected void btnDeleteMitumori_Click(object sender, EventArgs e)
        {
            String id = Request.QueryString["id"];
            DataTable dt_db = JC01Login_Class.Get_DB(id);
            String db = dt_db.Rows[0]["db"].ToString();
            String mail = dt_db.Rows[0]["mail"].ToString();
            ConstantVal.DB_NAME = db;

            JC_ClientConnecction_Class jc = new JC_ClientConnecction_Class();
            jc.loginId = mail;
            con = jc.GetConnection();
            String cMitumori = HF_cMitumori.Value;
            sql_Delete += "DELETE FROM r_bu_mitsu WHERE cMITUMORI='" + cMitumori + "';";
            sql_Delete += "Delete From r_mitumori where  cMITUMORI='" + cMitumori + "';";
            sql_Delete += "Delete From r_mitumori_m where  cMITUMORI='" + cMitumori + "';";
            sql_Delete += "Delete From r_mitumori_m2 where  cMITUMORI='" + cMitumori + "';";
            sql_Delete += "delete from h_mitumori where 1=1 and cMITUMORI='" + cMitumori + "';";
            sql_Delete += "Delete From m_mitsu_file where  cMITUMORI='" + cMitumori + "';";
            DeleteFileByMitumori(cMitumori);
            DeleteSekou(cMitumori, 2);//見積
            sql_Delete += "delete from r_shijisyo_mitsu where 1=1 and cMITUMORI='" + cMitumori + "';";
            DeleteShijishoByMitumori(cMitumori);
            sql_Delete += "delete from sekisankoumoku_mitsu where cMITUMORI='" + cMitumori + "' ;";


            sql_Delete += "delete from M_SEKISANKOUMOKU_M where cMITUMORI='" + cMitumori + "';";
            sql_Delete += "delete from M_CYOUKOKUSEKISAN where cMITUMORI='" + cMitumori + "';";
            sql_Delete += "delete from M_INKUJIXEXTUTO where cMITUMORI='" + cMitumori + "';";
            sql_Delete += "delete from m_kyoutsuu where cMITUMORI='" + cMitumori + "';";
            sql_Delete += "delete from r_router where cMITUMORI='" + cMitumori + "';";
            sql_Delete += "delete from r_pattern where cMITUMORI='" + cMitumori + "';";
            sql_Delete += "Delete From r_uri_mitsu where cMITUMORI='" + cMitumori + "';";
            DeleteUriageByMitumori(cMitumori);
            MySqlCommand cmdDelete = new MySqlCommand();
            MySqlTransaction tr = null;
            con.Open();
            try
            {
                tr = con.BeginTransaction();
                cmdDelete.Transaction = tr;
                cmdDelete.CommandTimeout = 0;
                cmdDelete = new MySqlCommand(sql_Delete, con);
                cmdDelete.ExecuteNonQuery();
                tr.Commit();
                Session["cBukken"] = LB_BukkenCode.Text;
                if (!String.IsNullOrEmpty(Request.QueryString["id"]))
                {
                    id = Request.QueryString["id"];
                    Response.Redirect("JC09BukkenSyousai.aspx?id=" + id);
                }
                else
                {
                    Response.Redirect("JC01Login.aspx");
                }
            }
            catch (Exception ex)
            {
                try
                {
                    tr.Rollback();
                }
                catch
                {
                }
            }
            con.Close();
        }
        #endregion

        #region BT_ImgaeDrop_Click  //画像Drag&Drop
        protected void BT_ImgaeDrop_Click(object sender, EventArgs e)
        {
            String fileName = HF_FileName.Value;
            string extension = Path.GetExtension(fileName);
            if (extension.ToLower() == ".pdf" || extension.ToLower() == ".ai")
            {
                string base64String = HF_ImgBase64.Value;
                base64String = base64String.Replace("data:application/pdf;base64,", String.Empty);
                base64String = base64String.Replace("data:application/postscript;base64,", string.Empty);
                try
                {
                    byte[] pdfBytes = Convert.FromBase64String(base64String);

                    using (var inputMS = new MemoryStream(pdfBytes))
                    {
                        GhostscriptRasterizer rasterizer = null;

                        using (rasterizer = new GhostscriptRasterizer())
                        {
                            rasterizer.Open(inputMS);

                            using (MemoryStream ms = new MemoryStream())
                            {
                                int pagecount = rasterizer.PageCount;
                                if (pagecount == 1)
                                {
                                    System.Drawing.Image img = rasterizer.GetPage(200, 1);
                                    img.Save(ms, ImageFormat.Png);

                                    var SigBase64 = Convert.ToBase64String(ms.GetBuffer()); //Get Base64
                                    String imgurl = "data:image/png;base64," + SigBase64;
                                    Image1.ImageUrl = imgurl;//Draw Image
                                    divImage.Style["display"] = "block";
                                    divBTImageUpload.Style["display"] = "none";
                                    updHeader.Update();
                                }
                                else
                                {
                                    SessionUtility.SetSession("HOME", "Master");
                                    Session["pageNumber"] = pagecount;
                                    //ifSentakuPopup.Style["width"] = "350px";
                                    //ifSentakuPopup.Style["height"] = "100px";
                                    ifSentakuPopup.Style["width"] = "100vw";
                                    ifSentakuPopup.Style["height"] = "100vh";
                                    String id = Request.QueryString["id"];
                                    ifSentakuPopup.Src = "JC35PdfPageChoice.aspx?id=" + id;
                                    mpeSentakuPopup.Show();
                                    updSentakuPopup.Update();
                                }
                            }


                            rasterizer.Close();
                        }
                    }

                    ////System.Drawing.Image image;
                    //using (MemoryStream ms = new MemoryStream(pdfBytes))
                    //{
                    //    PdfImage pdfImg = PdfImage.FromStream(ms);
                    //}
                }
                catch (Exception ex)
                {

                }

            }
            else
            {
                string base64String = HF_ImgBase64.Value;
                byte[] bytes = Convert.FromBase64String(base64String);

                System.Drawing.Image originalImage;
                using (MemoryStream ms = new MemoryStream(bytes))
                {
                    originalImage = System.Drawing.Image.FromStream(ms);
                }
                int introtate = 0;
                if (originalImage.PropertyIdList.Contains(0x0112))
                {
                    introtate = originalImage.GetPropertyItem(0x0112).Value[0];
                    switch (introtate)
                    {
                        case 1: // landscape, do nothing
                            break;

                        case 8: // rotated 90 right
                                // de-rotate:
                            originalImage.RotateFlip(rotateFlipType: RotateFlipType.Rotate270FlipNone);
                            break;

                        case 3: // bottoms up
                            originalImage.RotateFlip(rotateFlipType: RotateFlipType.Rotate180FlipNone);
                            break;

                        case 6: // rotated 90 left
                            originalImage.RotateFlip(rotateFlipType: RotateFlipType.Rotate90FlipNone);
                            break;
                    }
                }
                try
                {
                    var i2 = new Bitmap(originalImage);
                    EncoderParameters myEncoderParameters = new EncoderParameters(1);
                    EncoderParameter myEncoderParameter = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 75L);
                    myEncoderParameters.Param[0] = myEncoderParameter;
                    //i2.Save(folderPath + filename, GetEncoderInfo("image/jpeg"), myEncoderParameters);
                    //var i2 = new Bitmap(originalImage);
                    //i2.Save(folderPath + filename);
                    //originalImage.Save(folderPath + filename);
                }
                catch { }

                string imgurl = "";

                try
                {
                    using (var stream = new MemoryStream())
                    {
                        originalImage.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                        byte[] imageByte = stream.ToArray();
                        if (Convert.ToInt64(HF_ImgSize.Value) > 23552)//about 23KB
                        {
                            imageByte = ResizeImageFile(imageByte, 760);
                        }
                        base64String = Convert.ToBase64String(imageByte);
                        imgurl = "data:image/png;base64," + base64String;
                    }
                }
                catch
                {
                    base64String = HF_ImgBase64.Value;
                    imgurl = "data:image/png;base64," + base64String;
                }

                Image1.ImageUrl = imgurl;//Draw Image
                divImage.Style["display"] = "block";
                divBTImageUpload.Style["display"] = "none";
                updHeader.Update();
            }
        }
        #endregion

        #region btnPDFPageChoiceClose_Click
        protected void btnPDFPageChoiceClose_Click(object sender, EventArgs e)
        {
            if (Session["pageNumber"] != null)
            {
                int pageNumber = Convert.ToInt16(Session["pageNumber"].ToString());
                string base64String = HF_ImgBase64.Value;
                base64String = base64String.Replace("data:application/pdf;base64,", String.Empty);
                base64String = base64String.Replace("data:application/postscript;base64,", string.Empty);
                try
                {
                    byte[] pdfBytes = Convert.FromBase64String(base64String);

                    using (var inputMS = new MemoryStream(pdfBytes))
                    {
                        GhostscriptRasterizer rasterizer = null;

                        using (rasterizer = new GhostscriptRasterizer())
                        {
                            rasterizer.Open(inputMS);

                            using (MemoryStream ms = new MemoryStream())
                            {
                                int pagecount = rasterizer.PageCount;

                                System.Drawing.Image img = rasterizer.GetPage(200, pageNumber);
                                img.Save(ms, ImageFormat.Png);

                                var SigBase64 = Convert.ToBase64String(ms.GetBuffer()); //Get Base64
                                String imgurl = "data:image/png;base64," + SigBase64;

                                Image1.ImageUrl = imgurl;//Draw Image
                                divImage.Style["display"] = "block";
                                divBTImageUpload.Style["display"] = "none";
                                updHeader.Update();
                            }


                            rasterizer.Close();
                        }
                    }
                }
                catch (Exception ex) { }
            }
            HF_isChange.Value = "1";
            ifSentakuPopup.Src = "";
            mpeSentakuPopup.Hide();
            updSentakuPopup.Update();
            updHeader.Update();
        }
        #endregion

        #region btnToLogin_Click
        protected void btnToLogin_Click(object sender, EventArgs e)
        {
            Response.Redirect("JC01Login.aspx");
        }
        #endregion

        #region setrogo
        private void setrogo()　　//ロゴ名
        {
            String id = Request.QueryString["id"];
            DataTable dt_db = JC01Login_Class.Get_DB(id);
            String db = dt_db.Rows[0]["db"].ToString();
            String mail = dt_db.Rows[0]["mail"].ToString();
            ConstantVal.DB_NAME = db;

            JC_ClientConnecction_Class jc = new JC_ClientConnecction_Class();
            jc.loginId = mail;
            MySqlConnection cn = jc.GetConnection();

            try
            {
                cn.Open();
            }
            catch
            {
                Response.Redirect("JC01Login.aspx");
            }

            JC_Mitumori_Class jm = new JC_Mitumori_Class();
            cKyoten = jm.GetKyoten(HF_cMitumori.Value);

            string sql = "";
            sql = "select ifnull(sIMAGETitle1,'') sIMAGETitle1,ifnull(sIMAGETitle2,'') sIMAGETitle2,ifnull(sIMAGETitle3,'') sIMAGETitle3,ifnull(sIMAGETitle4,'') sIMAGETitle4,ifnull(sIMAGETitle5,'') sIMAGETitle5";
            sql += " from m_j_info where cCO ='" + cKyoten + "'";
            MySqlCommand cmd = new MySqlCommand(sql, cn);
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            cn.Close();
            da.Dispose();
            if (dt.Rows.Count > 0)
            {
                DDL_Logo.Items.Clear();
                if (dt.Rows[0]["sIMAGETitle1"].ToString().TrimEnd() != "")
                {
                    DDL_Logo.Items.Add(dt.Rows[0]["sIMAGETitle1"].ToString());
                }
                if (dt.Rows[0]["sIMAGETitle2"].ToString().TrimEnd() != "")
                {
                    DDL_Logo.Items.Add(dt.Rows[0]["sIMAGETitle2"].ToString());
                }
                if (dt.Rows[0]["sIMAGETitle3"].ToString().TrimEnd() != "")
                {
                    DDL_Logo.Items.Add(dt.Rows[0]["sIMAGETitle3"].ToString());
                }
                if (dt.Rows[0]["sIMAGETitle4"].ToString().TrimEnd() != "")
                {
                    DDL_Logo.Items.Add(dt.Rows[0]["sIMAGETitle4"].ToString());
                }
                if (dt.Rows[0]["sIMAGETitle5"].ToString().TrimEnd() != "")
                {
                    DDL_Logo.Items.Add(dt.Rows[0]["sIMAGETitle5"].ToString());
                }
            }
        }
        #endregion

        #region 見積書印刷
        protected void BT_P_Click(object sender, EventArgs e)
        {
            GridViewRow gvrow = (sender as Button).NamingContainer as GridViewRow;
            string mitsumoriId = (gvrow.FindControl("cMITUMORI") as LinkButton).Text;
            string mitsumoriName = (gvrow.FindControl("sMITUMORI") as Label).Text;
            HF_cMitumori.Value = mitsumoriId;
            HF_sMitumori.Value = mitsumoriName;
            updHeader.Update();
            PrintPDF();
        }
        #endregion

        #region PrintPDF
        private void PrintPDF()
        {
            #region 見積書印刷設定
            setrogo();//ロゴ名
            String id = Request.QueryString["id"];
            DataTable dt_db = JC01Login_Class.Get_DB(id);
            String db = dt_db.Rows[0]["db"].ToString();
            String mail = dt_db.Rows[0]["mail"].ToString();
            ConstantVal.DB_NAME = db;

            JC_ClientConnecction_Class jc = new JC_ClientConnecction_Class();
            jc.loginId = mail;

            DataTable dt_InsatsuSetting = jc.ExecuteInsatsuSetting(lblLoginUserCode.Text);
            Boolean chk_midashi = false;
            Boolean chk_meisai = false;
            Boolean chk_syosai = false;
            String fZeikomi = "0";
            if (dt_InsatsuSetting.Rows.Count > 0)
            {
                try
                {
                    String logo = dt_InsatsuSetting.Rows[0]["fLogo"].ToString();
                    DDL_Logo.SelectedIndex = Convert.ToInt32(logo);
                }
                catch { }

                fZeikomi = dt_InsatsuSetting.Rows[0]["fZei"].ToString();
                if (dt_InsatsuSetting.Rows[0]["fMidashi"].ToString() == "1")
                {
                    chk_midashi = true;
                }

                if (dt_InsatsuSetting.Rows[0]["fMeisai"].ToString() == "1")
                {
                    chk_meisai = true;
                }

                if (dt_InsatsuSetting.Rows[0]["fSyosai"].ToString() == "1")
                {
                    chk_syosai = true;
                }
            }
            updHeader.Update();
            #endregion

            #region ロゴタイトル
            string flagrogo = "";
            string sqlkyoten = "";
            sqlkyoten = "select ifnull(sIMAGETitle1,'') sIMAGETitle1,ifnull(sIMAGETitle2,'') sIMAGETitle2,ifnull(sIMAGETitle3,'') sIMAGETitle3,ifnull(sIMAGETitle4,'') sIMAGETitle4,ifnull(sIMAGETitle5,'') sIMAGETitle5";
            sqlkyoten += ",ifnull(sBIKOUTitle1,'') sBIKOUTitle1,ifnull(sBIKOUTitle2,'') sBIKOUTitle2,ifnull(sBIKOUTitle3,'') sBIKOUTitle3,ifnull(sBIKOUTitle4,'') sBIKOUTitle4,ifnull(sBIKOUTitle5,'') sBIKOUTitle5";
            sqlkyoten += " from m_j_info where cCO ='"+cKyoten+"'";
            jc.loginId = mail;
            con = jc.GetConnection();
            con.Open();

            MySqlCommand cmd = new MySqlCommand(sqlkyoten, con);
            cmd.CommandTimeout = 0;
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            da.Fill(dt);
            da.Dispose();
            con.Close();
            if (dt.Rows.Count > 0)
            {
                if (DDL_Logo.SelectedIndex > -1)
                {
                    if (dt.Rows[0]["sIMAGETitle1"].ToString().TrimEnd() == DDL_Logo.SelectedItem.ToString().TrimEnd())
                    {
                        flagrogo = "1";
                    }
                    else if (dt.Rows[0]["sIMAGETitle2"].ToString().TrimEnd() == DDL_Logo.SelectedItem.ToString().TrimEnd())
                    {
                        flagrogo = "2";
                    }
                    else if (dt.Rows[0]["sIMAGETitle3"].ToString().TrimEnd() == DDL_Logo.SelectedItem.ToString().TrimEnd())
                    {
                        flagrogo = "3";
                    }
                    else if (dt.Rows[0]["sIMAGETitle4"].ToString().TrimEnd() == DDL_Logo.SelectedItem.ToString().TrimEnd())
                    {
                        flagrogo = "4";
                    }
                    else if (dt.Rows[0]["sIMAGETitle5"].ToString().TrimEnd() == DDL_Logo.SelectedItem.ToString().TrimEnd())
                    {
                        flagrogo = "5";
                    }
                }
                else
                {
                    flagrogo = "0";
                }
            }
            #endregion

            ConstantVal.DB_NAME = db;
            DateTime datenow = jc.GetCurrentDate();
            String fileName =HF_sMitumori.Value + "_" +datenow.ToString("yyyyMMdd");

            if (chk_syosai == true)
            {
                fSYOUSAI = true;
            }
            else
            {
                fSYOUSAI = false;
            }
            if (chk_meisai == true)
            {
                fMEISAI = true;
            }
            else
            {
                fMEISAI = false;
            }
            if (chk_midashi == true)
            {
                fMIDASHI = true;
            }
            else
            {
                fMIDASHI = false;
            }

            //1.見出し、明細、詳細
            //2.見出し、明細
            //3.見出し、詳細
            //4.明細、詳細
            //5.明細
            //6.詳細

            if (fMIDASHI == false && fSYOUSAI == true && fMEISAI == false)//詳細だけ 
            {
                pcount = 0;
                rmt_pcount1 = 0;
                mitsumori rpt = new mitsumori();
                rpt.cMITUMORI = HF_cMitumori.Value;
                rpt.cBUKKEN = LB_BukkenCode.Text;
                rpt.cTANTOUSHA = lblcJISHATANTOUSHA.Text;
                rpt.loginId = mail;
                if (fSYOUSAI == true)
                {
                    rpt.fSYOUSAI = fSYOUSAI;//詳細チェック出来ると印刷できる
                }
                else
                {
                    rpt.fICHIRAN = true;//詳細のみを印刷できなっかた場合、デフォルトで明細をチェック
                }
                rpt.fSYOUSAI = true;//詳細チェック
                rpt.fICHIRAN = false;//一覧アンチェック
                rpt.fRYOUHOU = false;
                rpt.flag_page1 = true;
                rpt.frogoimage = flagrogo;//ロゴ combobox
                rpt.cKyoten = cKyoten;
                if (fHYOUSHI == true)//CB_HYOUSHI.Checked
                {
                    // frm.hyoushi = 1;
                    rpt.fHYOUSI = true;
                }
                else
                {
                    // frm.hyoushi = 0;
                    rpt.fHYOUSI = false;
                }
                if (fZeikomi == "1")
                {
                    rpt.fZEINUKIKINNGAKU = true;
                }
                else if (fZeikomi == "2")
                {
                    rpt.fZEINUKIKINNGAKU = false;
                }
                else
                {
                    rpt.fZEIFUKUMUKIKINNGAKU = true;
                }
                rpt.Run();
                //String filename = "mitumorisho.pdf";
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                GrapeCity.ActiveReports.Export.Pdf.Section.PdfExport pdfExport1 = new GrapeCity.ActiveReports.Export.Pdf.Section.PdfExport();
                pdfExport1.Export(rpt.Document, ms);
                ms.Position = 0;//position stream to 0
                //Response.AddHeader("Content-Disposition", "inline;filename=" + filename);
                //Response.AddHeader("Content-length", ms.Length.ToString());
                //Response.ContentType = "application/pdf";
                //Response.BinaryWrite(ms.ToArray());
                //Response.End();

                Session["PDFMemoryStream"] = ms;
                Session["PDFFileName"] = fileName;
                Session["UriagePDF"] = "false";
                //Response.Write("<script language='javascript'>window.open('JCPDFViewer.aspx', '_blank');</script>");
                if (!String.IsNullOrEmpty(Request.QueryString["id"]))
                {
                    id = Request.QueryString["id"];
                    Response.Write("<script language='javascript'>window.open('JCPDFViewer.aspx?id=" + id + "', '_blank');</script>");
                }
                else
                {
                    Response.Redirect("JC01Login.aspx");
                }
            }
            else if (fMIDASHI == false && fSYOUSAI == true && fMEISAI == true)//詳細、明細
            {
                pcount = 0;
                rmt_pcount1 = 0;
                #region pagenumber
                mitsumori rpt = new mitsumori();
                rpt.cMITUMORI = HF_cMitumori.Value;
                rpt.cBUKKEN = LB_BukkenCode.Text;
                rpt.cTANTOUSHA = lblcJISHATANTOUSHA.Text;
                rpt.loginId = mail;
                rpt.fSYOUSAI = false;
                rpt.fICHIRAN = true;
                rpt.fRYOUHOU = fSYOUSAI;
                rpt.flag_page1 = true;
                rpt.frogoimage = flagrogo;//ロゴ combobox
                                          //选择打印封页：1，不打印封页：0，由前一个页面传值过来
                rpt.cKyoten = cKyoten;
                if (fHYOUSHI == true)//CB_HYOUSHI.Checked
                {
                    // frm.hyoushi = 1;
                    rpt.fHYOUSI = true;
                }
                else
                {
                    // frm.hyoushi = 0;
                    rpt.fHYOUSI = false;
                }
                if (fZeikomi == "1")
                {
                    rpt.fZEINUKIKINNGAKU = true;
                }
                else if (fZeikomi == "2")
                {
                    rpt.fZEINUKIKINNGAKU = false;
                }
                else
                {
                    rpt.fZEIFUKUMUKIKINNGAKU = true;
                }
                rpt.Run();

                mitsumori rpt_1 = new mitsumori();
                rpt_1.cMITUMORI = HF_cMitumori.Value;
                rpt_1.cBUKKEN = LB_BukkenCode.Text;
                rpt_1.cTANTOUSHA = lblcJISHATANTOUSHA.Text;
                rpt_1.loginId = mail;
                rpt_1.fSYOUSAI = fSYOUSAI;
                rpt_1.fICHIRAN = false;
                rpt_1.fRYOUHOU = fSYOUSAI;
                pcount = rpt.pcount;
                rpt_1.pcount = pcount;
                rpt_1.flag_page1 = true;
                rpt_1.frogoimage = flagrogo;//ロゴ combobox
                                            //选择打印封页：1，不打印封页：0，由前一个页面传值过来
                rpt_1.cKyoten = cKyoten;
                if (fHYOUSHI == true)//CB_HYOUSHI.Checked
                {
                    //frm.hyoushi = 1;
                    rpt_1.fHYOUSI = true;
                }
                else
                {
                    // frm.hyoushi = 0;
                    rpt_1.fHYOUSI = false;
                }
                rpt_1.Run();

                if (fSYOUSAI == true)
                {
                    for (int i = 0; i < rpt_1.Document.Pages.Count; i++)//一覧と詳細両方表示
                    {
                        rpt.Document.Pages.Add(rpt_1.Document.Pages[i]);
                    }
                }

                //System.IO.MemoryStream ms = new System.IO.MemoryStream();
                //GrapeCity.ActiveReports.Export.Pdf.Section.PdfExport pdfExport1 = new GrapeCity.ActiveReports.Export.Pdf.Section.PdfExport();
                //pdfExport1.Export(rpt.Document, ms);
                //ms.Position = 0;//position stream to 0
                //Response.ContentType = "application/pdf";
                //Response.BinaryWrite(ms.ToArray());
                //Response.End();

                #endregion

                #region 一覧
                mitsumori prt4 = new mitsumori(); // 纵向数据表 type1
                prt4.cMITUMORI = HF_cMitumori.Value;
                prt4.cBUKKEN = LB_BukkenCode.Text;
                prt4.cTANTOUSHA = lblcJISHATANTOUSHA.Text;
                prt4.loginId = mail;
                prt4.fSYOUSAI = false;//詳細チェック
                prt4.fICHIRAN = true;//一覧アンチェック
                prt4.fRYOUHOU = fSYOUSAI;
                prt4.flag_page1 = false;
                prt4.frogoimage = flagrogo;
                prt4.cKyoten = cKyoten;
                pcount = rpt.Document.Pages.Count;
                prt4.pcount = pcount;
                //选择打印封页：1，不打印封页：0，由前一个页面传值过来
                if (fHYOUSHI == true)
                {
                    //frm.hyoushi = 1;
                    prt4.fHYOUSI = true;
                }
                else
                {
                    // frm.hyoushi = 0;
                    prt4.fHYOUSI = false;
                }
                prt4.Run();
                #endregion

                #region 詳細
                rmt_pcount1 = rpt.nPAGECOUNT;
                pcount = prt4.pcount;

                mitsumori prt5 = new mitsumori(); // 纵向数据表 type1
                prt5.cMITUMORI = HF_cMitumori.Value;
                prt5.cBUKKEN = LB_BukkenCode.Text;
                prt5.cTANTOUSHA = lblcJISHATANTOUSHA.Text;
                prt5.loginId = mail;
                prt5.fSYOUSAI = fSYOUSAI;//詳細チェック出来ると印刷できる
                prt5.fICHIRAN = false;//一覧アンチェック
                prt5.fRYOUHOU = fSYOUSAI;
                prt5.frogoimage = flagrogo;
                prt5.cKyoten = cKyoten;
                prt5.rmt_pcount1 = rmt_pcount1;
                prt5.pcount = pcount;
                //选择打印封页：1，不打印封页：0，由前一个页面传值过来
                if (fHYOUSHI == true)//CB_HYOUSHI.Checked
                {
                    //  frm.hyoushi = 1;
                    prt5.fHYOUSI = true;
                }
                else
                {
                    //  frm.hyoushi = 0;
                    prt5.fHYOUSI = false;
                }
                prt5.Run();

                if (fSYOUSAI == true)
                {
                    for (int i = 0; i < prt5.Document.Pages.Count; i++)//一覧と詳細両方表示
                    {
                        prt4.Document.Pages.Add(prt5.Document.Pages[i]);
                    }
                }

                //String filename = "mitumorisho.pdf";
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                GrapeCity.ActiveReports.Export.Pdf.Section.PdfExport pdfExport1 = new GrapeCity.ActiveReports.Export.Pdf.Section.PdfExport();
                pdfExport1.Export(rpt.Document, ms);
                ms.Position = 0;//position stream to 0
                                //Response.AddHeader("Content-Disposition", "inline;filename=" + filename);
                                //Response.AddHeader("Content-length", ms.Length.ToString());
                                //Response.ContentType = "application/pdf";
                                //Response.BinaryWrite(ms.ToArray());
                                //Response.End();

                Session["PDFMemoryStream"] = ms;
                Session["PDFFileName"] = fileName;
                Session["UriagePDF"] = "false";
                //Response.Write("<script language='javascript'>window.open('JCPDFViewer.aspx', '_blank');</script>");
                if (!String.IsNullOrEmpty(Request.QueryString["id"]))
                {
                    id = Request.QueryString["id"];
                    Response.Write("<script language='javascript'>window.open('JCPDFViewer.aspx?id=" + id + "', '_blank');</script>");
                }
                else
                {
                    Response.Redirect("JC01Login.aspx");
                }

                #endregion
            }
            else
            {
                if (fMIDASHI == false)
                {
                    #region 見出し
                    pcount = 0;
                    rmt_pcount1 = 0;
                    mitsumori rpt = new mitsumori();
                    rpt.cMITUMORI = HF_cMitumori.Value;
                    rpt.cBUKKEN = LB_BukkenCode.Text;
                    rpt.cTANTOUSHA = lblcJISHATANTOUSHA.Text;
                    rpt.loginId =mail;
                    rpt.fSYOUSAI = false;//詳細チェック
                    rpt.fICHIRAN = true;//一覧チェック
                    rpt.fRYOUHOU = false;
                    rpt.flag_page1 = true;
                    rpt.frogoimage = flagrogo;//ロゴ combobox
                    rpt.cKyoten = cKyoten;
                    if (fHYOUSHI == true)//CB_HYOUSHI.Checked
                    {
                        // frm.hyoushi = 1;
                        rpt.fHYOUSI = true;
                    }
                    else
                    {
                        // frm.hyoushi = 0;
                        rpt.fHYOUSI = false;
                    }
                    if (fZeikomi == "1")
                    {
                        rpt.fZEINUKIKINNGAKU = true;
                    }
                    else if (fZeikomi == "2")
                    {
                        rpt.fZEINUKIKINNGAKU = false;
                    }
                    else
                    {
                        rpt.fZEIFUKUMUKIKINNGAKU = true;
                    }
                    rpt.Run();

                    //String filename = "mitumorisho.pdf";
                    System.IO.MemoryStream ms = new System.IO.MemoryStream();
                    GrapeCity.ActiveReports.Export.Pdf.Section.PdfExport pdfExport1 = new GrapeCity.ActiveReports.Export.Pdf.Section.PdfExport();
                    pdfExport1.Export(rpt.Document, ms);
                    ms.Position = 0;//position stream to 0
                    //Response.AddHeader("Content-Disposition", "inline;filename=" + filename);
                    //Response.AddHeader("Content-length", ms.Length.ToString());
                    //Response.ContentType = "application/pdf";
                    //Response.BinaryWrite(ms.ToArray());
                    //Response.End();
                    Session["PDFMemoryStream"] = ms;
                    Session["PDFFileName"] = fileName;
                    Session["UriagePDF"] = "false";
                    //Response.Write("<script language='javascript'>window.open('JCPDFViewer.aspx', '_blank');</script>");
                    if (!String.IsNullOrEmpty(Request.QueryString["id"]))
                    {
                        id = Request.QueryString["id"];
                        Response.Write("<script language='javascript'>window.open('JCPDFViewer.aspx?id=" + id + "', '_blank');</script>");
                    }
                    else
                    {
                        Response.Redirect("JC01Login.aspx");
                    }
                    #endregion
                }
                else
                {
                    if (fMIDASHI == true && fSYOUSAI == true && fMEISAI == true) //見出し、明細、詳細
                    {
                        #region pagenumber
                        #region 見出し
                        pcount = 0;
                        rmt_pcount1 = 0;
                        mitsumori rpt_midashi = new mitsumori();
                        rpt_midashi.cMITUMORI = HF_cMitumori.Value;
                        rpt_midashi.cBUKKEN = LB_BukkenCode.Text;
                        rpt_midashi.cTANTOUSHA = lblcJISHATANTOUSHA.Text;
                        rpt_midashi.loginId = mail;
                        rpt_midashi.fSYOUSAI = false;//詳細チェック
                        rpt_midashi.fICHIRAN = false;//一覧アンチェック
                        rpt_midashi.fRYOUHOU = true;
                        rpt_midashi.fMIDASHI = fMIDASHI;//見出しチェック
                        rpt_midashi.flag_page1 = true;
                        rpt_midashi.frogoimage = flagrogo;//ロゴ combobox
                        rpt_midashi.cKyoten = cKyoten;
                        if (fMIDASHI == true)
                        {
                            rpt_midashi.Run();
                        }
                        #endregion

                        #region 明細
                        // rmt_pcount1 = 0;
                        mitsumori rpt_mesai = new mitsumori();
                        rpt_mesai.cMITUMORI = HF_cMitumori.Value;
                        rpt_mesai.cBUKKEN = LB_BukkenCode.Text;
                        rpt_mesai.cTANTOUSHA = lblcJISHATANTOUSHA.Text;
                        rpt_mesai.loginId = mail;
                        rpt_mesai.fSYOUSAI = false;//詳細チェック
                        rpt_mesai.fICHIRAN = true;//一覧アンチェック
                        rpt_mesai.fRYOUHOU = true;
                        rpt_mesai.frogoimage = flagrogo;//ロゴ combobox
                        rpt_mesai.flag_page1 = true;
                        rpt_mesai.cKyoten = cKyoten;
                        // rpt_mesai.rmt_pcount1 = rpt_midashi.nPAGECOUNT;
                        pcount = rpt_midashi.pcount;
                        rpt_mesai.pcount = pcount;
                        rpt_mesai.Run();

                        #endregion

                        #region 詳細
                        mitsumori rpt_syousai = new mitsumori();
                        rpt_syousai.cMITUMORI = HF_cMitumori.Value;
                        rpt_syousai.cBUKKEN = LB_BukkenCode.Text;
                        rpt_syousai.cTANTOUSHA = lblcJISHATANTOUSHA.Text;
                        rpt_syousai.loginId = mail;
                        rpt_syousai.fSYOUSAI = fSYOUSAI;//詳細チェック出来ると印刷できる
                        rpt_syousai.fICHIRAN = false;//一覧アンチェック
                        rpt_syousai.fRYOUHOU = fSYOUSAI;
                        rpt_syousai.flag_page1 = true;
                        rpt_syousai.frogoimage = flagrogo;//ロゴ combobox
                                                          //rpt_syousai.rmt_pcount1 = rpt_mesai.nPAGECOUNT;
                        rpt_syousai.cKyoten = cKyoten;
                        pcount = rpt_mesai.pcount;
                        rpt_syousai.pcount = pcount;
                        if (fSYOUSAI == true)
                        {
                            rpt_syousai.Run();
                        }

                        if (fMIDASHI == true)
                        {
                            for (int i = 0; i < rpt_mesai.Document.Pages.Count; i++)
                            {
                                rpt_midashi.Document.Pages.Add(rpt_mesai.Document.Pages[i]);
                            }

                            if (fSYOUSAI == true)
                            {
                                for (int i = 0; i < rpt_syousai.Document.Pages.Count; i++)
                                {
                                    rpt_midashi.Document.Pages.Add(rpt_syousai.Document.Pages[i]);
                                }
                            }
                        }
                        else
                        {
                            if (fSYOUSAI == true)
                            {
                                for (int i = 0; i < rpt_syousai.Document.Pages.Count; i++)//一覧と詳細両方表示
                                {
                                    rpt_midashi.Document.Pages.Add(rpt_syousai.Document.Pages[i]);
                                }
                            }
                        }

                        //System.IO.MemoryStream ms = new System.IO.MemoryStream();
                        //GrapeCity.ActiveReports.Export.Pdf.Section.PdfExport pdfExport1 = new GrapeCity.ActiveReports.Export.Pdf.Section.PdfExport();
                        //pdfExport1.Export(rpt_midashi.Document, ms);
                        //ms.Position = 0;//position stream to 0
                        //Response.ContentType = "application/pdf";
                        //Response.BinaryWrite(ms.ToArray());
                        //Response.End();

                        #endregion

                        #endregion

                        #region 表示

                        #region 見出し

                        mitsumori rpt_midashi1 = new mitsumori();
                        rpt_midashi1.cMITUMORI = HF_cMitumori.Value;
                        rpt_midashi1.cBUKKEN = LB_BukkenCode.Text;
                        rpt_midashi1.cTANTOUSHA = lblcJISHATANTOUSHA.Text;
                        rpt_midashi1.loginId = mail;
                        rpt_midashi1.fSYOUSAI = false;//詳細チェック
                        rpt_midashi1.fICHIRAN = false;//一覧アンチェック
                        rpt_midashi1.fRYOUHOU = fSYOUSAI;
                        rpt_midashi1.fMIDASHI = fMIDASHI;//見出しチェック
                        rpt_midashi1.flag_page1 = false;
                        rpt_midashi1.frogoimage = flagrogo;//ロゴ combobox
                        rpt_midashi1.cKyoten = cKyoten;
                        rmt_pcount1 = rpt_midashi.nPAGECOUNT;
                        // rpt_midashi1.rmt_pcount1 = rmt_pcount1;
                        pcount = rpt_midashi.Document.Pages.Count;
                        rpt_midashi1.pcount = pcount;
                        if (fMIDASHI == true)
                        {
                            rpt_midashi1.Run();
                        }
                        #endregion

                        #region 一覧
                        // rmt_pcount1 = rpt_midashi1.nPAGECOUNT;
                        pcount = rpt_midashi1.pcount;
                        mitsumori rpt4 = new mitsumori();
                        rpt4.cMITUMORI = HF_cMitumori.Value;
                        rpt4.cBUKKEN = LB_BukkenCode.Text;
                        rpt4.cTANTOUSHA = lblcJISHATANTOUSHA.Text;
                        rpt4.loginId = mail;
                        rpt4.fSYOUSAI = false;//詳細チェック
                        rpt4.fICHIRAN = true;//一覧アンチェック
                        rpt4.fRYOUHOU = fSYOUSAI;
                        rpt4.frogoimage = flagrogo;//ロゴ combobox
                                                   //  rpt4.rmt_pcount1 = rmt_pcount1;
                        rpt4.cKyoten = cKyoten;
                        rpt4.pcount = pcount;
                        rpt4.Run();
                        #endregion

                        #region 詳細
                        if (fHYOUSHI == true)//CB_HYOUSHI.Checked 
                        {
                            rmt_pcount1 = (rpt_midashi.nPAGECOUNT + rpt_mesai.nPAGECOUNT) - 1;//合計書数-表紙数 //prt4_p1
                        }
                        else
                        {
                            rmt_pcount1 = rpt_midashi.nPAGECOUNT + rpt_mesai.nPAGECOUNT;//合計書数-表紙数
                        }

                        mitsumori rpt5 = new mitsumori();
                        rpt5.cMITUMORI = HF_cMitumori.Value;
                        rpt5.cBUKKEN = LB_BukkenCode.Text;
                        rpt5.cTANTOUSHA = lblcJISHATANTOUSHA.Text;
                        rpt5.loginId = mail;
                        rpt5.fSYOUSAI = fSYOUSAI;//詳細チェック出来ると印刷できる
                        rpt5.fICHIRAN = false;//一覧アンチェック
                        rpt5.fRYOUHOU = fSYOUSAI;
                        if (fHYOUSHI == true)//CB_HYOUSHI.Checked
                        {
                            //frm.hyoushi = 1;
                            rpt5.fHYOUSI = true;
                        }
                        else
                        {
                            // frm.hyoushi = 0;
                            rpt5.fHYOUSI = false;
                        }
                        rpt5.frogoimage = flagrogo;//ロゴ combobox
                                                   //rpt5.rmt_pcount1 = rmt_pcount1;
                        rpt5.cKyoten = cKyoten;
                        pcount = rpt_midashi1.pcount;
                        rpt5.pcount = pcount;
                        if (fSYOUSAI == true)
                        {
                            rpt5.Run();
                        }

                        if (fMIDASHI == true)
                        {
                            for (int i = 0; i < rpt4.Document.Pages.Count; i++)
                            {
                                rpt_midashi1.Document.Pages.Add(rpt4.Document.Pages[i]);
                            }

                            if (fSYOUSAI == true)
                            {
                                for (int i = 0; i < rpt5.Document.Pages.Count; i++)
                                {
                                    rpt_midashi1.Document.Pages.Add(rpt5.Document.Pages[i]);
                                }
                            }

                            // frm.prt = prt4_midashi1;

                            //String filename = "mitumorisho.pdf";
                            System.IO.MemoryStream ms = new System.IO.MemoryStream();
                            GrapeCity.ActiveReports.Export.Pdf.Section.PdfExport pdfExport1 = new GrapeCity.ActiveReports.Export.Pdf.Section.PdfExport();
                            pdfExport1.Export(rpt_midashi1.Document, ms);
                            ms.Position = 0;//position stream to 0
                            //Response.AddHeader("Content-Disposition", "inline;filename=" + filename);
                            //Response.AddHeader("Content-length", ms.Length.ToString());
                            //Response.ContentType = "application/pdf";
                            //Response.BinaryWrite(ms.ToArray());
                            //Response.End();
                            Session["PDFMemoryStream"] = ms;
                            Session["PDFFileName"] = fileName;
                            Session["UriagePDF"] = "false";
                            //Response.Write("<script language='javascript'>window.open('JCPDFViewer.aspx', '_blank');</script>");
                            if (!String.IsNullOrEmpty(Request.QueryString["id"]))
                            {
                                id = Request.QueryString["id"];
                                Response.Write("<script language='javascript'>window.open('JCPDFViewer.aspx?id=" + id + "', '_blank');</script>");
                            }
                            else
                            {
                                Response.Redirect("JC01Login.aspx");
                            }
                        }
                        else
                        {
                            if (fSYOUSAI == true)
                            {
                                for (int i = 0; i < rpt5.Document.Pages.Count; i++)//一覧と詳細両方表示
                                {
                                    rpt4.Document.Pages.Add(rpt5.Document.Pages[i]);
                                }
                            }

                            // frm.prt = prt4;
                            //String filename = "mitumorisho.pdf";
                            System.IO.MemoryStream ms = new System.IO.MemoryStream();
                            GrapeCity.ActiveReports.Export.Pdf.Section.PdfExport pdfExport1 = new GrapeCity.ActiveReports.Export.Pdf.Section.PdfExport();
                            pdfExport1.Export(rpt4.Document, ms);
                            ms.Position = 0;//position stream to 0
                            //Response.AddHeader("Content-Disposition", "inline;filename=" + filename);
                            //Response.AddHeader("Content-length", ms.Length.ToString());
                            //Response.ContentType = "application/pdf";
                            //Response.BinaryWrite(ms.ToArray());
                            //Response.End();
                            Session["PDFMemoryStream"] = ms;
                            Session["PDFFileName"] = fileName;
                            Session["UriagePDF"] = "false";
                            //Response.Write("<script language='javascript'>window.open('JCPDFViewer.aspx', '_blank');</script>");
                            if (!String.IsNullOrEmpty(Request.QueryString["id"]))
                            {
                                id = Request.QueryString["id"];
                                Response.Write("<script language='javascript'>window.open('JCPDFViewer.aspx?id=" + id + "', '_blank');</script>");
                            }
                            else
                            {
                                Response.Redirect("JC01Login.aspx");
                            }
                        }

                        #endregion

                        #endregion
                    }
                    else if (fMIDASHI == true && (fSYOUSAI == true && fMEISAI == false)) //見出し、明細かつ詳細
                    {
                        #region pagenumber
                        rmt_pcount1 = 0;
                        #region 見出し
                        mitsumori prt4_midashi = new mitsumori(); // 纵向数据表 type1
                        prt4_midashi.cMITUMORI = HF_cMitumori.Value;
                        prt4_midashi.cBUKKEN = LB_BukkenCode.Text;
                        prt4_midashi.cTANTOUSHA = lblcJISHATANTOUSHA.Text;
                        prt4_midashi.loginId = mail;
                        prt4_midashi.fSYOUSAI = false;//詳細チェック
                        prt4_midashi.fICHIRAN = false;//一覧アンチェック
                        prt4_midashi.fRYOUHOU = false;
                        prt4_midashi.fMIDASHI = fMIDASHI;//見出しチェック
                        prt4_midashi.flag_page1 = true;
                        prt4_midashi.frogoimage = flagrogo;//ロゴ combobox
                                                           //选择打印封页：1，不打印封页：0，由前一个页面传值过来
                        prt4_midashi.cKyoten = cKyoten;
                        if (fHYOUSHI == true)//CB_HYOUSHI.Checked
                        {
                            // frm.hyoushi = 1;
                            prt4_midashi.fHYOUSI = true;
                        }
                        else
                        {
                            // frm.hyoushi = 0;
                            prt4_midashi.fHYOUSI = false;
                        }
                        if (fMIDASHI == true)
                        {
                            prt4_midashi.Run();
                        }
                        #endregion

                        #region 詳細
                        mitsumori prt5_p1 = new mitsumori(); // 纵向数据表 type1
                        prt5_p1.cMITUMORI = HF_cMitumori.Value;
                        prt5_p1.cBUKKEN = LB_BukkenCode.Text;
                        prt5_p1.cTANTOUSHA = lblcJISHATANTOUSHA.Text;
                        prt5_p1.loginId = mail;
                        prt5_p1.fSYOUSAI = fSYOUSAI;//詳細チェック出来ると印刷できる
                        if (fSYOUSAI == false && fMIDASHI == false)
                        {
                            prt5_p1.fICHIRAN = true;//見出しと詳細両方ともない場合、明細をデフォルトで表示する
                        }
                        else
                        {
                            prt5_p1.fICHIRAN = false;//一覧アンチェック
                        }
                        prt5_p1.fRYOUHOU = false;
                        prt5_p1.frogoimage = flagrogo;//ロゴ combobox
                                                      //选择打印封页：1，不打印封页：0，由前一个页面传值过来
                        prt5_p1.cKyoten = cKyoten;
                        if (fHYOUSHI == true)//CB_HYOUSHI.Checked
                        {
                            // frm.hyoushi = 1;
                            prt5_p1.fHYOUSI = true;
                        }
                        else
                        {
                            // frm.hyoushi = 0;
                            prt5_p1.fHYOUSI = false;
                        }
                        prt5_p1.Run();

                        if (fMIDASHI == true)
                        {
                            for (int i = 0; i < prt5_p1.Document.Pages.Count; i++)//一覧と詳細両方表示
                            {
                                prt4_midashi.Document.Pages.Add(prt5_p1.Document.Pages[i]);
                            }
                        }
                        //String filename = "mitumorisho.pdf";
                        System.IO.MemoryStream ms = new System.IO.MemoryStream();
                        GrapeCity.ActiveReports.Export.Pdf.Section.PdfExport pdfExport1 = new GrapeCity.ActiveReports.Export.Pdf.Section.PdfExport();
                        pdfExport1.Export(prt4_midashi.Document, ms);
                        ms.Position = 0;//position stream to 0
                        //Response.AddHeader("Content-Disposition", "inline;filename=" + filename);
                        //Response.AddHeader("Content-length", ms.Length.ToString());
                        //Response.ContentType = "application/pdf";
                        //Response.BinaryWrite(ms.ToArray());
                        //Response.End();
                        Session["PDFMemoryStream"] = ms;
                        Session["PDFFileName"] = fileName;
                        Session["UriagePDF"] = "false";
                        //Response.Write("<script language='javascript'>window.open('JCPDFViewer.aspx', '_blank');</script>");
                        if (!String.IsNullOrEmpty(Request.QueryString["id"]))
                        {
                            id = Request.QueryString["id"];
                            Response.Write("<script language='javascript'>window.open('JCPDFViewer.aspx?id=" + id + "', '_blank');</script>");
                        }
                        else
                        {
                            Response.Redirect("JC01Login.aspx");
                        }
                        #endregion

                        #endregion

                        #region 表示

                        #region 見出し
                        mitsumori prt4_midashi1 = new mitsumori(); // 纵向数据表 type1
                        prt4_midashi1.cMITUMORI = HF_cMitumori.Value;
                        prt4_midashi1.cBUKKEN = LB_BukkenCode.Text;
                        prt4_midashi1.cTANTOUSHA = lblcJISHATANTOUSHA.Text;
                        prt4_midashi1.loginId = mail;
                        prt4_midashi1.fSYOUSAI = false;//詳細チェック
                        prt4_midashi1.fICHIRAN = false;//一覧アンチェック
                        prt4_midashi1.fRYOUHOU = false;
                        prt4_midashi1.fMIDASHI = fMIDASHI;//見出しチェック
                        prt4_midashi1.flag_page1 = false;
                        prt4_midashi1.frogoimage = flagrogo;//ロゴ combobox
                                                            //选择打印封页：1，不打印封页：0，由前一个页面传值过来
                        prt4_midashi1.cKyoten = cKyoten;
                        if (fHYOUSHI == true)//CB_HYOUSHI.Checked
                        {
                            // frm.hyoushi = 1;
                            prt4_midashi1.fHYOUSI = true;
                        }
                        else
                        {
                            // frm.hyoushi = 0;
                            prt4_midashi1.fHYOUSI = false;
                        }
                        if (fMIDASHI == true)
                        {
                            prt4_midashi1.Run();
                        }
                        #endregion

                        #region 詳細

                        rmt_pcount1 = prt4_midashi.nPAGECOUNT;

                        mitsumori prt5 = new mitsumori(); // 纵向数据表 type1
                        prt5.cMITUMORI = HF_cMitumori.Value;
                        prt5.cBUKKEN = LB_BukkenCode.Text;
                        prt5.cTANTOUSHA = lblcJISHATANTOUSHA.Text;
                        prt5.loginId = mail;
                        prt5.fSYOUSAI = fSYOUSAI;//詳細チェック出来ると印刷できる
                        if (fSYOUSAI == false && fMIDASHI == false)
                        {
                            prt5.fICHIRAN = true;
                        }
                        else
                        {
                            prt5.fICHIRAN = false;//一覧アンチェック
                        }
                        prt5.fRYOUHOU = false;
                        prt5.frogoimage = flagrogo;//ロゴ combobox
                                                   //选择打印封页：1，不打印封页：0，由前一个页面传值过来
                        prt5.cKyoten = cKyoten;
                        if (fHYOUSHI == true)//CB_HYOUSHI.Checked
                        {
                            //frm.hyoushi = 1;
                            prt5.fHYOUSI = true;
                        }
                        else
                        {
                            //frm.hyoushi = 0;
                            prt5.fHYOUSI = false;
                        }
                        prt5.Run();

                        if (fMIDASHI == true)
                        {
                            for (int i = 0; i < prt5.Document.Pages.Count; i++)
                            {
                                prt4_midashi1.Document.Pages.Add(prt5.Document.Pages[i]);
                            }

                            //frm.prt = prt4_midashi1;
                        }
                        else
                        {
                            // frm.prt = prt5;
                        }
                        // System.IO.MemoryStream ms = new System.IO.MemoryStream();
                        // GrapeCity.ActiveReports.Export.Pdf.Section.PdfExport pdfExport1 = new GrapeCity.ActiveReports.Export.Pdf.Section.PdfExport();
                        pdfExport1.Export(prt4_midashi1.Document, ms);
                        ms.Position = 0;//position stream to 0
                                        //Response.AddHeader("Content-Disposition", "inline;filename=" + filename);
                                        //Response.AddHeader("Content-length", ms.Length.ToString());
                                        //Response.ContentType = "application/pdf";
                                        //Response.BinaryWrite(ms.ToArray());
                                        //Response.End();
                        Session["PDFMemoryStream"] = ms;
                        Session["PDFFileName"] = fileName;
                        Session["UriagePDF"] = "false";
                        //Response.Write("<script language='javascript'>window.open('JCPDFViewer.aspx', '_blank');</script>");
                        if (!String.IsNullOrEmpty(Request.QueryString["id"]))
                        {
                            id = Request.QueryString["id"];
                            Response.Write("<script language='javascript'>window.open('JCPDFViewer.aspx?id=" + id + "', '_blank');</script>");
                        }
                        else
                        {
                            Response.Redirect("JC01Login.aspx");
                        }
                        #endregion

                        #endregion
                    }
                    else if (fMIDASHI == true && (fSYOUSAI == false && fMEISAI == true)) //見出し、明細かつ詳細
                    {
                        rmt_pcount1 = 0;
                        #region pagenumber
                        #region 見出し
                        mitsumori prt4_midashi = new mitsumori(); // 纵向数据表 type1
                        prt4_midashi.cMITUMORI = HF_cMitumori.Value;
                        prt4_midashi.cBUKKEN = LB_BukkenCode.Text;
                        prt4_midashi.cTANTOUSHA = lblcJISHATANTOUSHA.Text;
                        prt4_midashi.loginId = mail;
                        prt4_midashi.fSYOUSAI = false;//詳細チェック
                        prt4_midashi.fICHIRAN = false;//一覧アンチェック
                        prt4_midashi.fRYOUHOU = false;
                        prt4_midashi.fMIDASHI = fMIDASHI;//見出しチェック
                        prt4_midashi.flag_page1 = true;
                        prt4_midashi.frogoimage = flagrogo;//ロゴ combobox
                                                           //选择打印封页：1，不打印封页：0，由前一个页面传值过来
                        prt4_midashi.cKyoten = cKyoten;
                        if (fHYOUSHI == true)//
                        {
                            // frm.hyoushi = 1;
                            prt4_midashi.fHYOUSI = true;
                        }
                        else
                        {
                            // frm.hyoushi = 0;
                            prt4_midashi.fHYOUSI = false;
                        }
                        if (fMIDASHI == true)
                        {
                            prt4_midashi.Run();
                        }
                        #endregion

                        #region 明細
                        mitsumori prt4_p1 = new mitsumori(); // 纵向数据表 type1
                        prt4_p1.cMITUMORI = HF_cMitumori.Value;
                        prt4_p1.cBUKKEN = LB_BukkenCode.Text;
                        prt4_p1.cTANTOUSHA = lblcJISHATANTOUSHA.Text;
                        prt4_p1.loginId = mail;
                        prt4_p1.fSYOUSAI = false;//詳細チェック
                        prt4_p1.fICHIRAN = true;//一覧アンチェック
                        prt4_p1.fRYOUHOU = false;
                        prt4_p1.frogoimage = flagrogo;//ロゴ combobox
                                                      //选择打印封页：1，不打印封页：0，由前一个页面传值过来
                        prt4_p1.cKyoten = cKyoten;
                        if (fHYOUSHI == true)//CB_HYOUSHI.Checked
                        {
                            //frm.hyoushi = 1;
                            prt4_p1.fHYOUSI = true;
                        }
                        else
                        {
                            // frm.hyoushi = 0;
                            prt4_p1.fHYOUSI = false;
                        }
                        prt4_p1.Run();

                        if (fMIDASHI == true)
                        {
                            for (int i = 0; i < prt4_p1.Document.Pages.Count; i++)
                            {
                                prt4_midashi.Document.Pages.Add(prt4_p1.Document.Pages[i]);
                            }
                        }
                        //String filename = "mitumorisho.pdf";
                        System.IO.MemoryStream ms = new System.IO.MemoryStream();
                        GrapeCity.ActiveReports.Export.Pdf.Section.PdfExport pdfExport1 = new GrapeCity.ActiveReports.Export.Pdf.Section.PdfExport();
                        pdfExport1.Export(prt4_midashi.Document, ms);
                        ms.Position = 0;//position stream to 0
                        //Response.AddHeader("Content-Disposition", "inline;filename=" + filename);
                        //Response.AddHeader("Content-length", ms.Length.ToString());
                        //Response.ContentType = "application/pdf";
                        //Response.BinaryWrite(ms.ToArray());
                        //Response.End();
                        Session["PDFMemoryStream"] = ms;
                        Session["PDFFileName"] = fileName;
                        Session["UriagePDF"] = "false";
                        //Response.Write("<script language='javascript'>window.open('JCPDFViewer.aspx', '_blank');</script>");
                        if (!String.IsNullOrEmpty(Request.QueryString["id"]))
                        {
                            id = Request.QueryString["id"];
                            Response.Write("<script language='javascript'>window.open('JCPDFViewer.aspx?id=" + id + "', '_blank');</script>");
                        }
                        else
                        {
                            Response.Redirect("JC01Login.aspx");
                        }
                        #endregion

                        #endregion

                        #region 表示

                        #region 見出し
                        mitsumori prt4_midashi1 = new mitsumori(); // 纵向数据表 type1
                        prt4_midashi1.cMITUMORI = HF_cMitumori.Value;
                        prt4_midashi1.cBUKKEN = LB_BukkenCode.Text;
                        prt4_midashi1.cTANTOUSHA = lblcJISHATANTOUSHA.Text;
                        prt4_midashi1.loginId = mail;
                        prt4_midashi1.fSYOUSAI = false;//詳細チェック
                        prt4_midashi1.fICHIRAN = false;//一覧アンチェック
                        prt4_midashi1.fRYOUHOU = false;
                        prt4_midashi1.fMIDASHI = fMIDASHI;//見出しチェック
                        prt4_midashi1.flag_page1 = false;
                        prt4_midashi1.frogoimage = flagrogo;//ロゴ combobox
                                                            //选择打印封页：1，不打印封页：0，由前一个页面传值过来
                        prt4_midashi1.cKyoten = cKyoten;
                        if (fHYOUSHI == true)//CB_HYOUSHI.Checked
                        {
                            // frm.hyoushi = 1;
                            prt4_midashi1.fHYOUSI = true;
                        }
                        else
                        {
                            // frm.hyoushi = 0;
                            prt4_midashi1.fHYOUSI = false;
                        }
                        if (fMIDASHI == true)
                        {
                            prt4_midashi1.Run();
                            // frm.nMAXPAGE = prt4_midashi1.nPAGECOUNT.ToString();
                        }

                        #endregion

                        #region 一覧

                        rmt_pcount1 = prt4_midashi.nPAGECOUNT;

                        mitsumori prt4 = new mitsumori(); // 纵向数据表 type1
                        prt4.cMITUMORI = HF_cMitumori.Value;
                        prt4.cBUKKEN = LB_BukkenCode.Text;
                        prt4.cTANTOUSHA = lblcJISHATANTOUSHA.Text;
                        prt4.loginId = mail;
                        prt4.fSYOUSAI = false;//詳細チェック
                        prt4.fICHIRAN = true;//一覧アンチェック
                        prt4.fRYOUHOU = false;
                        prt4.frogoimage = flagrogo;//ロゴ combobox
                                                   //选择打印封页：1，不打印封页：0，由前一个页面传值过来
                        prt4.cKyoten = cKyoten;
                        if (fHYOUSHI == true)//CB_HYOUSHI.Checked
                        {
                            // frm.hyoushi = 1;
                            prt4.fHYOUSI = true;
                        }
                        else
                        {
                            // frm.hyoushi = 0;
                            prt4.fHYOUSI = false;
                        }
                        prt4.Run();
                        #endregion

                        if (fMIDASHI == true)
                        {
                            for (int i = 0; i < prt4.Document.Pages.Count; i++)
                            {
                                prt4_midashi1.Document.Pages.Add(prt4.Document.Pages[i]);
                            }

                            // frm.prt = prt4_midashi1;
                        }
                        else
                        {
                            // frm.prt = prt4;

                        }
                        // System.IO.MemoryStream ms = new System.IO.MemoryStream();
                        // GrapeCity.ActiveReports.Export.Pdf.Section.PdfExport pdfExport1 = new GrapeCity.ActiveReports.Export.Pdf.Section.PdfExport();
                        pdfExport1.Export(prt4_midashi1.Document, ms);
                        ms.Position = 0;//position stream to 0
                                        //Response.AddHeader("Content-Disposition", "inline;filename=" + filename);
                                        //Response.AddHeader("Content-length", ms.Length.ToString());
                                        //Response.ContentType = "application/pdf";
                                        //Response.BinaryWrite(ms.ToArray());
                                        //Response.End();
                        Session["PDFMemoryStream"] = ms;
                        Session["PDFFileName"] = fileName;
                        Session["UriagePDF"] = "false";
                        //Response.Write("<script language='javascript'>window.open('JCPDFViewer.aspx', '_blank');</script>");
                        if (!String.IsNullOrEmpty(Request.QueryString["id"]))
                        {
                            id = Request.QueryString["id"];
                            Response.Write("<script language='javascript'>window.open('JCPDFViewer.aspx?id=" + id + "', '_blank');</script>");
                        }
                        else
                        {
                            Response.Redirect("JC01Login.aspx");
                        }
                        #endregion
                    }
                    else if (fMIDASHI == true && fSYOUSAI == false && fMEISAI == false) //見出し
                    {
                        rmt_pcount1 = 0;
                        mitsumori rpt = new mitsumori();
                        rpt.cMITUMORI = HF_cMitumori.Value;
                        rpt.cBUKKEN = LB_BukkenCode.Text;
                        rpt.cTANTOUSHA = lblcJISHATANTOUSHA.Text;
                        rpt.loginId = mail;
                        rpt.fSYOUSAI = false;//詳細チェック
                        if (fMIDASHI == false)
                        {
                            rpt.fICHIRAN = true;//一覧チェック
                        }
                        rpt.fRYOUHOU = false;
                        rpt.fMIDASHI = fMIDASHI;//見出しチェック
                        rpt.flag_page1 = true;
                        rpt.frogoimage = flagrogo;//ロゴ combobox
                        rpt.cKyoten = cKyoten;
                        if (fHYOUSHI == true)//CB_HYOUSHI.Checked
                        {
                            // frm.hyoushi = 1;
                            rpt.fHYOUSI = true;
                        }
                        else
                        {
                            // frm.hyoushi = 0;
                            rpt.fHYOUSI = false;
                        }
                        if (fZeikomi == "1")
                        {
                            rpt.fZEINUKIKINNGAKU = true;
                        }
                        else if (fZeikomi == "2")
                        {
                            rpt.fZEINUKIKINNGAKU = false;
                        }
                        else
                        {
                            rpt.fZEIFUKUMUKIKINNGAKU = true;
                        }
                        rpt.Run();
                        //String filename = "mitumorisho.pdf";
                        System.IO.MemoryStream ms = new System.IO.MemoryStream();
                        GrapeCity.ActiveReports.Export.Pdf.Section.PdfExport pdfExport1 = new GrapeCity.ActiveReports.Export.Pdf.Section.PdfExport();
                        pdfExport1.Export(rpt.Document, ms);
                        ms.Position = 0;//position stream to 0
                        //Response.AddHeader("Content-Disposition", "inline;filename=" + filename);
                        //Response.AddHeader("Content-length", ms.Length.ToString());
                        //Response.ContentType = "application/pdf";
                        //Response.BinaryWrite(ms.ToArray());
                        //Response.End();
                        Session["PDFMemoryStream"] = ms;
                        Session["PDFFileName"] = fileName;
                        Session["UriagePDF"] = "false";
                        //Response.Write("<script language='javascript'>window.open('JCPDFViewer.aspx', '_blank');</script>");
                        if (!String.IsNullOrEmpty(Request.QueryString["id"]))
                        {
                            id = Request.QueryString["id"];
                            Response.Write("<script language='javascript'>window.open('JCPDFViewer.aspx?id=" + id + "', '_blank');</script>");
                        }
                        else
                        {
                            Response.Redirect("JC01Login.aspx");
                        }
                    }
                }
            }
        }
        #endregion

        #region GV_MitumoriSyousai_PreRender
        protected void GV_MitumoriSyousai_PreRender(object sender, EventArgs e)
        {
            string[] columns = HF_GridSize.Value.Split(',');
            GV_MitumoriSyousai.Columns[0].HeaderStyle.Width = Unit.Pixel(30);
            GV_MitumoriSyousai.Columns[1].HeaderStyle.Width = Unit.Pixel(32);
            GV_MitumoriSyousai.Columns[3].HeaderStyle.Width = Unit.Pixel((int)Math.Round(Convert.ToDecimal(columns[2])));
            GV_MitumoriSyousai.Columns[4].HeaderStyle.Width = Unit.Pixel((int)Math.Round(Convert.ToDecimal(columns[3])));
            GV_MitumoriSyousai.Columns[5].HeaderStyle.Width = Unit.Pixel((int)Math.Round(Convert.ToDecimal(columns[4])));
            GV_MitumoriSyousai.Columns[6].HeaderStyle.Width = Unit.Pixel((int)Math.Round(Convert.ToDecimal(columns[5])));
            GV_MitumoriSyousai.Columns[7].HeaderStyle.Width = Unit.Pixel((int)Math.Round(Convert.ToDecimal(columns[6])));
            GV_MitumoriSyousai.Columns[8].HeaderStyle.Width = Unit.Pixel((int)Math.Round(Convert.ToDecimal(columns[7])));
            GV_MitumoriSyousai.Columns[9].HeaderStyle.Width = Unit.Pixel((int)Math.Round(Convert.ToDecimal(columns[8])));
            GV_MitumoriSyousai.Columns[10].HeaderStyle.Width = Unit.Pixel((int)Math.Round(Convert.ToDecimal(columns[9])));
            GV_MitumoriSyousai.Columns[11].HeaderStyle.Width = Unit.Pixel(30);
            GV_MitumoriSyousai.Width= Unit.Pixel((int)Math.Round(Convert.ToDecimal(HF_Grid.Value)));

            Response.Cookies["colWidthbMitumori"].Value = HF_GridSize.Value;
            Response.Cookies["colWidthbMitumoriGrid"].Value = HF_Grid.Value;
            Response.Cookies["colWidthbMitumori"].Expires = DateTime.Now.AddYears(1);
            Response.Cookies["colWidthbMitumoriGrid"].Expires = DateTime.Now.AddYears(1);
        }
        #endregion

        #region BT_ColumnWidth_Click
        protected void BT_ColumnWidth_Click(object sender, EventArgs e)
        {
            Response.Cookies["colWidthbMitumori"].Value = HF_GridSize.Value;
            Response.Cookies["colWidthbMitumoriGrid"].Value = HF_Grid.Value;
            Response.Cookies["colWidthbMitumori"].Expires = DateTime.Now.AddYears(1);
            Response.Cookies["colWidthbMitumoriGrid"].Expires = DateTime.Now.AddYears(1);
        }
        #endregion

        #region tb_jishabangou_TextChanged
        protected void tb_jishabangou_TextChanged(object sender, EventArgs e)
        {
            HF_isChange.Value = "1";
            updHeader.Update();
        }
        #endregion

        #region 20220517 Chaw Added
        protected void btnYesJisha_Click(object sender, EventArgs e)
        {
            save();
        }
        public void save()
        {
            if (HF_checkData.Value == "1")
            {
                if (TextUtility.isomojiCharacter(TB_sBUKKEN.Text))
                {
                    f_isomoji_msg = false;
                }
                String sBukken = TB_sBUKKEN.Text.Replace("\\", "\\\\").Replace("'", "\\'");  //物件名

                if (TextUtility.isomojiCharacter(TB_sBIKOU.Text))
                {
                    f_isomoji_msg = false;
                }
                String sBikou = "null";
                if (!String.IsNullOrEmpty(TB_sBIKOU.Text))
                {
                    sBikou = "'" + TB_sBIKOU.Text.Replace("\\", "\\\\").Replace("'", "\\'") + "'"; //備考
                }

                String jishatantousya = "null";
                if (!String.IsNullOrEmpty(lblcJISHATANTOUSHA.Text))
                {
                    jishatantousya = "'" + lblcJISHATANTOUSHA.Text + "'"; //営業担当
                }

                if (f_isomoji_msg == false)
                {
                    string msg = "使用不可能なテキスト（環境依存文字）が入力され保存できません。</br>文字化けの原因となるため、下記の文字を修正してください。</br>" + " 対象文字：「" + TextUtility.invalidtext_all + "」";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowAnkenChangeMessage",
                                "ShowMojiMessage('" + msg + "','" + btnmojiOK.ClientID + "');", true);
                    f_isomoji_msg = true;
                    Session["moji"] = "true";
                }
                else
                {

                    String dNouki = "null";
                    if (!String.IsNullOrEmpty(lbldNouki.Text))
                    {
                        dNouki = "'" + lbldNouki.Text + "'";
                    }

                    String dJikaitenken = "null";
                    if (!String.IsNullOrEmpty(lbldJikaitenken.Text))
                    {
                        dJikaitenken = "'" + lbldJikaitenken.Text + "'";
                    }

                    String dJikaiokugai = "null";
                    if (!String.IsNullOrEmpty(lbldJikaiokugai.Text))
                    {
                        dJikaiokugai = "'" + lbldJikaiokugai.Text + "'";
                    }

                    String cJoutai = "00";
                    if (!String.IsNullOrEmpty(lblcJoutai.Text))
                    {
                        cJoutai = lblcJoutai.Text;
                    }

                    if (LB_BukkenCode.Text == "")  //新規登録
                    {
                        #region getcBUKKEN
                        string cBUKKEN = "";
                        string bukkenCode = " select ifnull(MAX(cBUKKEN),0)+1 as cBUKKEN  from R_BUKKEN; ";
                        MySqlCommand cmd = new MySqlCommand(bukkenCode, con);
                        cmd.CommandTimeout = 0;
                        con.Close();
                        con.Open();
                        MySqlDataReader dr = cmd.ExecuteReader();

                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                cBUKKEN = dr[0].ToString();
                            }
                            dr.Close();
                        }

                        cBUKKEN = cBUKKEN.ToString().PadLeft(10, '0');
                        LB_BukkenCode.Text = cBUKKEN;
                        con.Close();
                        #endregion

                        getTOKUISAKI_INFO(lblcTOKUISAKI.Text);

                        getTOKUISAKI_Tantou(lblcTOKUISAKI.Text);

                        String tokui_tantou = "null";
                        String tokui_tantou_jun = "'0'";
                        if (!String.IsNullOrEmpty(lblsTOKUISAKI_TAN.Text))
                        {
                            tokui_tantou = "'" + lblsTOKUISAKI_TAN.Text.Replace("&lt", "<").Replace("&gt", ">") + "'";
                            tokui_tantou_jun = "'" + lblsTOKUISAKI_TAN_JUN.Text + "'";
                        }


                        #region getImagePath
                        if (TB_cfile.Text != "")
                        {
                            getsSERVERPATH();

                            String year = "";
                            String month = "";
                            String day = "";
                            String date = "";
                            String cTokuisaki = "";
                            String sPath = "";

                            DateTime datetime = new DateTime();
                            datetime = DateTime.Today;
                            date = datetime.ToShortDateString();
                            cTokuisaki = lblcTOKUISAKI.Text;
                            year = (Convert.ToDateTime(date)).ToString("yyyy");
                            month = (Convert.ToDateTime(date)).ToString("MM");
                            day = (Convert.ToDateTime(date)).ToString("dd");
                            sPath = year + "\\" + month + "\\" + day + "\\" + cTokuisaki + "\\" + cBUKKEN;

                            string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                            sPATH_LOCAL_SOURCE = folderPath + "\\DEMO20" + "\\TEMP" + "\\" + sShiFlag + "\\" + TB_sfile.Text;

                            sPATH_SERVER_SOURCE = sSERVERPATH + "\\" + sPath + "\\\\指示書\\" + TB_sfile.Text;
                            sPATH_SERVER_SOURCE = sPATH_SERVER_SOURCE.Replace(@"\", @"\\");

                            sPATH_SUB_DIR = sPath + "\\\\指示書";
                            sPATH_SUB_DIR = sPATH_SUB_DIR.Replace(@"\", @"\\");

                            #region sPath_Server CreateDirectory
                            sPath_Server = sPath_Server + "\\" + sPath + "\\BukkenPrint";
                            if (Directory.Exists(sPath_Server))
                            {
                                Directory.Delete(sPath_Server);
                            }
                            Directory.CreateDirectory(sPath_Server);
                            string icontempfolderPath = folderPath + "\\DEMO20" + "\\ICONTEMP" + "\\BukkenPrint\\" + TB_sfile.Text;
                            File.Copy(icontempfolderPath, sPath_Server + "\\" + TB_sfile.Text);
                            #endregion
                        }
                        #endregion

                        #region 物件登録 h_bukken,r_bukken,m_file,m_bu_file Table

                        string Insertsql = "Insert Into r_bukken(cBUKKEN, sBUKKEN, sBIKOU, cTANTOUSHA, cTOKUISAKI," +
                            " sTOKUISAKI, sTOKUISAKIJUSYO1, sTOKUISAKIJUSYO2," +
                            "  sTOKUISAKITEL, sTOKUISAKIFAX,sTOKUISAKIYUBIN, sTOKUISAKI_TAN, sTOKUISAKI_TAN_Jun, fSAMA, sYUUKOUKI,dBUKKEN, " +
                            "   cCREATE_TAN, dCREATE, dHENKOU, cHENKOUSYA,cJYOUTAI, cBukkenJYOUTAI, cRANK, fHYOUJI, nMORIARARI,nURIAGE," +
                            "   cFILE, cPJ, sTOKUISAKI_TANBUMON, sTOKUISAKI_YAKUSHOKU,fJikaitenken, fJikaiokugai, sTOKUISAKI_KEISYO," +
                            "   fHAITA, cSHIYOUSYA,dNOUKI, dJikaitenken, dJikaiokugai) " +
                            " Values('" + cBUKKEN + "', '" + sBukken + "', " + sBikou + ", " + jishatantousya + ", '" + lblcTOKUISAKI.Text + "', " +
                            "'" + lblsTOKUISAKI.Text.Replace("&lt", "<").Replace("&gt", ">") + "', " + sTOKUISAKIJUSYO1 + ", " + sTOKUISAKIJUSYO2 + ", " + sTOKUISAKITEL + ", " + sTOKUISAKIFAX + ", " +
                            "" + sTOKUISAKIYUBIN + ", " + tokui_tantou + ", " + tokui_tantou_jun + ", '0', null, " +
                            "Now() , '" + lblLoginUserCode.Text + "', Now() , Now() , '" + lblLoginUserCode.Text + "'," +
                            " '01', '" + cJoutai + "', '00', '0', 0," +
                            " 0, '" + TB_cfile.Text + "', '" + tb_jishabangou.Text + "', " + sBUMON + ", " + SYAKUSHOKU + "," +
                            " '0', '0', " + SKEISHOU + ",'0',null," +
                            " " + dNouki + "," + dJikaitenken + "," + dJikaiokugai + ");";

                        Insertsql += " Insert Into h_bukken( cBUKKEN,nJUNBAN,sNAIYOU,dHENKOU,cHENKOUSYA ) Values('" + cBUKKEN + "','1','', Now() ,'" + lblLoginUserCode.Text + "'); ";

                        #region 物件登録 m_file,m_bu_file table
                        if (TB_cfile.Text != "")
                        {
                            Insertsql += "INSERT INTO m_file (cFILE, sPATH_LOCAL_SOURCE,sPATH_SERVER_SOURCE, sFILE, dHENKOU, cHENKOUSYA, sPATH_SUB_DIR) VALUES ('" + TB_cfile.Text + "','" + sPATH_LOCAL_SOURCE + "', '" + sPATH_SERVER_SOURCE + "', '" + TB_sfile.Text + "', Now(), '" + lblLoginUserCode.Text + "', '" + sPATH_SUB_DIR + "');";
                            Insertsql += "INSERT INTO m_bu_file (cBUKKEN, nJUNBAN, cFILE, fVISABLE, dHENKOU, cHENKOUSYA) VALUES ('" + cBUKKEN + "','1','" + TB_cfile.Text + "','1',Now(),'" + lblLoginUserCode.Text + "') ON DUPLICATE KEY UPDATE fVISABLE = '1',dHENKOU = Now(),cHENKOUSYA='" + lblLoginUserCode.Text + "';";
                        }
                        #endregion

                        MySqlTransaction tr = null;
                        MySqlCommand cmdUpdate = new MySqlCommand();
                        con.Open();
                        try
                        {
                            tr = con.BeginTransaction();
                            cmdUpdate.Transaction = tr;
                            cmdUpdate.CommandTimeout = 0;
                            cmdUpdate = new MySqlCommand(Insertsql, con);
                            cmdUpdate.ExecuteNonQuery();
                            tr.Commit();

                            divLabelSave.Style["display"] = "flex";//「保存しました。」メッセージを表示                                                                                                                                      
                            updLabelSave.Update();

                            TB_sBUKKEN.BackColor = Color.White;
                            BT_Delete.Visible = true;
                            lblSakuseiSya.Text = lblLoginUserName.Text;
                            lblkoushinSya.Text = lblLoginUserName.Text;
                            lblcSakuseiSya.Text = lblLoginUserCode.Text;
                            lblckoushinSya.Text = lblLoginUserCode.Text;
                            lblSakusekiBi.Text = DateTime.Now.ToString("yyyy/MM/dd");
                            lblkoushinBi.Text = DateTime.Now.ToString("yyyy/MM/dd");
                            lblsTOKUISAKI.Text = lblsTOKUISAKI.Text.Replace("<", "&lt").Replace(">", "&gt");
                            lblsTOKUISAKI_TAN.Text = lblsTOKUISAKI_TAN.Text.Replace("<", "&lt").Replace(">", "&gt");
                            updHeader.Update();
                            //BT_Save.Enabled = false;
                            HF_isChange.Value = "0";
                            saveSuccess = true;
                            updHeader.Update();
                        }
                        catch (Exception ex)
                        {
                            try
                            {
                                tr.Rollback();
                            }
                            catch
                            {
                            }
                        }
                        con.Close();
                        #endregion



                    }
                    else //更新
                    {
                        String id = Request.QueryString["id"];
                        DataTable dt_db = JC01Login_Class.Get_DB(id);
                        String db = dt_db.Rows[0]["db"].ToString();
                        String mail = dt_db.Rows[0]["mail"].ToString();
                        ConstantVal.DB_NAME = db;

                        JC_ClientConnecction_Class jc = new JC_ClientConnecction_Class();
                        jc.loginId = mail;
                        con = jc.GetConnection();
                        #region getnJUNBAN
                        string junban_sql = "select max(nJUNBAN) + 1 as nJUNBAN from h_bukken where cBUKKEN = '" + LB_BukkenCode.Text + "'";
                        MySqlCommand cm = new MySqlCommand(junban_sql, con);
                        cm.CommandTimeout = 0;
                        con.Close();
                        con.Open();
                        MySqlDataReader mdr = cm.ExecuteReader();
                        string junban = "";
                        if (mdr.HasRows)
                        {
                            while (mdr.Read())
                            {
                                junban = mdr[0].ToString();
                            }
                            mdr.Close();
                        }
                        con.Close();
                        #endregion

                        #region mitumori 20220518 Chaw Added
                        string selectBukken = "select cMITUMORI from r_bu_mitsu where cBUKKEN = '" + LB_BukkenCode.Text + "'";
                        MySqlCommand cmb = new MySqlCommand(selectBukken, con);
                        cmb.CommandTimeout = 0;
                        con.Open();
                        MySqlDataReader mdrb = cmb.ExecuteReader();
                        string bukken = "";
                        string update_sql = "";
                        if (mdrb.HasRows)
                        {
                            while (mdrb.Read())
                            {
                                for (int i = 0; i < mdrb.FieldCount; i++)
                                {
                                    {
                                        bukken = mdrb[i].ToString();
                                        update_sql += "Update r_mitumori Set cPJ= '" + tb_jishabangou.Text + "' Where  cMITUMORI = '" + bukken + "' ; ";
                                        //MySqlCommand cmdUpdMit = new MySqlCommand();
                                        //cmdUpdMit = new MySqlCommand(updMitsu, conn);
                                        //cmdUpdMit.ExecuteNonQuery();
                                        
                                    }
                                }
                            }
                            mdrb.Close();
                        }
                        con.Close();
                        #endregion

                        #region uriage 20220519 Chaw Added
                        string selecturiage = "select rum.cURIAGE from r_uri_mitsu rum left join r_bu_mitsu rbm on rum.cMITUMORI = rbm.cMITUMORI where rbm.cBUKKEN= '" + LB_BukkenCode.Text + "'";
                        MySqlCommand cmu = new MySqlCommand(selecturiage, con);
                        cmu.CommandTimeout = 0;
                        con.Open();
                        MySqlDataReader mdru = cmu.ExecuteReader();
                        string uriage = "";
                        if (mdru.HasRows)
                        {
                            while (mdru.Read())
                            {
                                for (int i = 0; i < mdru.FieldCount; i++)
                                {
                                    {
                                        uriage = mdru[i].ToString();
                                        update_sql += "Update r_uriage Set cPJ= '" + tb_jishabangou.Text + "' Where  cURIAGE = '" + uriage + "' ; ";
                                    }
                                }
                            }
                            mdrb.Close();
                        }
                        con.Close();
                        #endregion

                        getTOKUISAKI_INFO(lblcTOKUISAKI.Text);

                        getTOKUISAKI_Tantou(lblcTOKUISAKI.Text);

                        String tokui_tantou = "null";
                        String tokui_tantou_jun = "'0'";
                        if (!String.IsNullOrEmpty(lblsTOKUISAKI_TAN.Text))
                        {
                            tokui_tantou = "'" + lblsTOKUISAKI_TAN.Text.Replace("&lt", "<").Replace("&gt", ">") + "'";
                            tokui_tantou_jun = "'" + lblsTOKUISAKI_TAN_JUN.Text + "'";
                        }

                        #region getImagePath
                        if (TB_cfile.Text != "")
                        {
                            getsSERVERPATH();

                            String year = "";
                            String month = "";
                            String day = "";
                            String date = "";
                            String cTokuisaki = "";
                            String sPath = "";

                            DateTime datetime = new DateTime();
                            datetime = DateTime.Today;
                            date = datetime.ToShortDateString();
                            cTokuisaki = lblcTOKUISAKI.Text;
                            year = (Convert.ToDateTime(date)).ToString("yyyy");
                            month = (Convert.ToDateTime(date)).ToString("MM");
                            day = (Convert.ToDateTime(date)).ToString("dd");
                            sPath = year + "\\" + month + "\\" + day + "\\" + cTokuisaki + "\\" + LB_BukkenCode.Text;

                            string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                            sPATH_LOCAL_SOURCE = folderPath + "\\DEMO20" + "\\TEMP" + "\\" + sShiFlag + "\\" + TB_sfile.Text;

                            sPATH_SERVER_SOURCE = sSERVERPATH + "\\" + sPath + "\\\\指示書\\" + TB_sfile.Text;
                            sPATH_SERVER_SOURCE = sPATH_SERVER_SOURCE.Replace(@"\", @"\\");

                            sPATH_SUB_DIR = sPath + "\\\\指示書";
                            sPATH_SUB_DIR = sPATH_SUB_DIR.Replace(@"\", @"\\");

                            #region sPath_Server CreateDirectory
                            sPath_Server = sPath_Server + "\\" + sPath + "\\BukkenPrint";
                            if (Directory.Exists(sPath_Server))
                            {
                                System.IO.Directory.Delete(sPath_Server, true);
                            }
                            Directory.CreateDirectory(sPath_Server);
                            string icontempfolderPath = folderPath + "\\DEMO20" + "\\ICONTEMP" + "\\BukkenPrint\\" + TB_sfile.Text;
                            File.Copy(icontempfolderPath, sPath_Server + "\\" + TB_sfile.Text);
                            #endregion
                        }
                        #endregion

                        #region 物件変更 h_bukken,r_bukken,m_file,m_bu_file Table
                        update_sql += " Update r_bukken " +
                                            " Set" +
                                            " cBUKKEN = '" + LB_BukkenCode.Text + "'," +
                                            " sBUKKEN = '" + sBukken + "'," +
                                            " cTOKUISAKI = '" + lblcTOKUISAKI.Text + "'," +
                                            " sTOKUISAKI = '" + lblsTOKUISAKI.Text.Replace("&lt", "<").Replace("&gt", ">") + "'," +
                                            " sTOKUISAKIJUSYO1 = " + sTOKUISAKIJUSYO1 + "," +
                                            " sTOKUISAKIJUSYO2 =" + sTOKUISAKIJUSYO2 + ", " +
                                            " sTOKUISAKITEL = " + sTOKUISAKITEL + ", " +
                                            " sTOKUISAKIFAX = " + sTOKUISAKIFAX + "," +
                                            " sTOKUISAKIYUBIN = " + sTOKUISAKIYUBIN + ", " +
                                            " sTOKUISAKI_TAN = " + tokui_tantou + "," +
                                            " sTOKUISAKI_TAN_Jun = " + tokui_tantou_jun + "," +
                                            " sTOKUISAKI_TANBUMON = " + sBUMON + "," +
                                            " sTOKUISAKI_YAKUSHOKU = " + SYAKUSHOKU + "," +
                                            " sTOKUISAKI_KEISYO = " + SKEISHOU + ", " +
                                            " cTANTOUSHA = " + jishatantousya + "," +
                                            //" cCREATE_TAN = '" + lblLoginUserCode.Text + "'," +
                                            " cHENKOUSYA = '" + lblLoginUserCode.Text + "'," +
                                            " sBIKOU = " + sBikou + "," +
                                            //" dCREATE = Now()," +
                                            " dHENKOU = Now(), " +
                                            " cPJ = '" + tb_jishabangou.Text + "'," +
                                            " dNOUKI = " + dNouki + "," +
                                            " dJikaitenken = " + dJikaitenken + "," +
                                            " dJikaiokugai = " + dJikaiokugai + "," +
                                            " cBukkenJYOUTAI = '" + cJoutai + "'," +
                                            " cFILE =  '" + TB_cfile.Text + "'" +
                                            " Where " +
                                            " '1' = '1' And cBUKKEN = '" + LB_BukkenCode.Text + "' ; ";

                        update_sql += " Insert Into h_bukken(cBUKKEN,nJUNBAN,sNAIYOU,dHENKOU,cHENKOUSYA)Values('" + LB_BukkenCode.Text + "','" + junban + "','内容変更',Now(),'" + lblLoginUserCode.Text + "') ; ";

                        #region 物件変更 m_file,m_bu_file table
                        if (TB_cfile.Text != "")
                        {
                            update_sql += "INSERT INTO m_file (cFILE, sPATH_LOCAL_SOURCE,sPATH_SERVER_SOURCE, sFILE, dHENKOU, cHENKOUSYA, sPATH_SUB_DIR) VALUES ('" + TB_cfile.Text + "','" + sPATH_LOCAL_SOURCE + "', '" + sPATH_SERVER_SOURCE + "', '" + TB_sfile.Text + "', Now(), '" + lblLoginUserCode.Text + "', '" + sPATH_SUB_DIR + "');";
                            update_sql += "INSERT INTO m_bu_file (cBUKKEN, nJUNBAN, cFILE, fVISABLE, dHENKOU, cHENKOUSYA) VALUES ('" + LB_BukkenCode.Text + "','1','" + TB_cfile.Text + "','1',Now(),'" + lblLoginUserCode.Text + "') ON DUPLICATE KEY UPDATE fVISABLE = '1',dHENKOU = Now(),cHENKOUSYA='" + lblLoginUserCode.Text + "';";

                        }
                        #endregion

                        MySqlCommand cmdUpdate = new MySqlCommand();
                        MySqlTransaction tr = null;
                        con.Open();
                        try
                        {
                            tr = con.BeginTransaction();
                            cmdUpdate.Transaction = tr;
                            cmdUpdate.CommandTimeout = 0;
                            cmdUpdate = new MySqlCommand(update_sql, con);
                            cmdUpdate.ExecuteNonQuery();
                            tr.Commit();

                            divLabelSave.Style["display"] = "flex";//「保存しました。」メッセージを表示                                                                                                                                      
                            updLabelSave.Update();

                            TB_sBUKKEN.BackColor = Color.White;
                            BT_Delete.Visible = true;
                            lblkoushinSya.Text = lblLoginUserName.Text;
                            lblckoushinSya.Text = lblLoginUserCode.Text;
                            lblkoushinBi.Text = DateTime.Now.ToString("yyyy/MM/dd");
                            lblsTOKUISAKI.Text = lblsTOKUISAKI.Text.Replace("<", "&lt").Replace(">", "&gt");
                            lblsTOKUISAKI_TAN.Text = lblsTOKUISAKI_TAN.Text.Replace("<", "&lt").Replace(">", "&gt");
                            //lblcSakuseiSya.Text = lblLoginUserCode.Text;
                            //lblSakuseiSya.Text = lblLoginUserName.Text;
                            //lblSakusekiBi.Text = DateTime.Now.ToString("yyyy/MM/dd");

                            updHeader.Update();
                            //BT_Save.Enabled = false;
                            HF_isChange.Value = "0";
                            saveSuccess = true;
                            updHeader.Update();
                        }
                        catch (Exception ex)
                        {
                            try
                            {
                                tr.Rollback();
                            }
                            catch
                            {
                            }
                        }
                        con.Close();
                        #endregion


                    }

                }
            }
        }
        #endregion
    }
}