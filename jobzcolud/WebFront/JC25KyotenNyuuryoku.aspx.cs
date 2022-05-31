/*作成者：ナン
 *作成日：20210901
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using Common;
using MySql.Data.MySqlClient;
using System.Data;
using System.Configuration;
using Service;
using System.Text;
using jobzcolud.pdf;

namespace jobzcolud.WebFront
{
    public partial class JC25KyotenNyuuryoku : System.Web.UI.Page
    {
        string kyotenId = "";
        string logonum = "";
        bool frbt = false;
        
        JC25KyotenNyuuryoku_Class ktVal = new JC25KyotenNyuuryoku_Class();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(Request.QueryString["id"]))
            {
                if (!this.IsPostBack)
                {
                    previewSession();// 202202010 MyatNoe Added

                    if (Session["IdKyouten"] != null)
                    {
                        kyotenId = Session["IdKyouten"].ToString();
                        ReadData();
                    }
                    else
                    {
                        logonum = "1";
                        btnKyotenLogo1.Visible = true;
                        btnKyotenLogo2.Visible = false;
                        btnKyotenLogo3.Visible = false;
                        btnKyotenLogo4.Visible = false;
                        btnKyotenLogo5.Visible = false;
                        //txtlogoName1.Text = "ロゴ１";//20220502 maythu delete
                        //txtlogoName2.Text = "ロゴ２";//20220502 maythu delete

                        DDLKyotenLogo();//20220502 maythu add
                    }
                    setItemProperty(logonum);
                    frbt = false;
                }
                Session.Timeout = 480;
            }

        }

        #region "拠点名テキストボックスを変更する"

        /// <summary>
        /// 拠点名テキストボックスを変更する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void txtKyotenMei_TextChanged(object sender, EventArgs e)
        {
            txtKyotenMei.Style["border-color"] = "none";

            // 入力の文字数制御チェック         
            var textByteCount = Encoding.Default.GetByteCount(txtKyotenMei.Text);
            if (textByteCount > 50)
            {
                string textCount = txtKyotenMei.Text;
                while (Encoding.Default.GetByteCount(textCount) > 50)
                {
                    textCount = textCount.Substring(0, textCount.Length - 1);
                }
                txtKyotenMei.Text = textCount;
            }
           
        }
        #endregion

        #region "会社名テキストボックスを変更する"

        /// <summary>
        /// 会社名テキストボックスを変更する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void txtKaishaMei_TextChanged(object sender, EventArgs e)
        {
            var textByteCount = Encoding.Default.GetByteCount(txtKaishaMei.Text);
            if (textByteCount > 50)
            {
                string textCount = txtKaishaMei.Text;
                while (Encoding.Default.GetByteCount(textCount) > 50)
                {
                    textCount = textCount.Substring(0, textCount.Length - 1);
                }
                txtKaishaMei.Text = textCount;
            }
        }
        #endregion

        #region "郵便番号テキストボックスを変更する"

        /// <summary>
        /// 郵便番号テキストボックスを変更する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void txtYubinBangou_TextChanged(object sender, EventArgs e)
        {
            if (txtYubinBangou.Text != "")
            {
                if (TextUtility.IsIncludeZenkaku(txtYubinBangou.Text))
                {

                    lbl_YubinErr.Text = "半角英数を入力してください。";
                    txtYubinBangou.Text = "";
                    txtYubinBangou.Focus();
                }
                else
                {
                    lbl_YubinErr.Text = "";
                }
            }
            else
            {
                lbl_YubinErr.Text = "";
            }
        }
        #endregion

        #region "住所1テキストボックスを変更する"

        /// <summary>
        /// 住所1テキストボックスを変更する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void txtJusho1_TextChanged(object sender, EventArgs e)
        {
            var textByteCount = Encoding.Default.GetByteCount(txtJusho1.Text);
            if (textByteCount > 40)
            {
                string textCount = txtJusho1.Text;
                while (Encoding.Default.GetByteCount(textCount) > 40)
                {
                    textCount = textCount.Substring(0, textCount.Length - 1);
                }
                txtJusho1.Text = textCount;
            }
        }
        #endregion

        #region "住所2テキストボックスを変更する"

        /// <summary>
        /// 住所2テキストボックスを変更する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void txtjusho2_TextChanged(object sender, EventArgs e)
        {
            var textByteCount = Encoding.Default.GetByteCount(txtjusho2.Text);
            if (textByteCount > 40)
            {
                string textCount = txtjusho2.Text;
                while (Encoding.Default.GetByteCount(textCount) > 40)
                {
                    textCount = textCount.Substring(0, textCount.Length - 1);
                }
                txtjusho2.Text = textCount;
            }
        }
        #endregion

        #region "電話テキストボックスを変更する"

        /// <summary>
        /// 電話テキストボックスを変更する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void txtdanwa_TextChanged(object sender, EventArgs e)
        {
            //if (TextUtility.IsIncludeZenkaku(txtdanwa.Text))
            //{
            //    if (LB_mailerror.Text == "" && LB_Yuubenbangouerror.Text == "")
            //    {
            //        LB_DenwaError.Text = "半角英数を入力してください。";
            //        TB_Denwa.Text = "";
            //        TB_Denwa.Focus();
            //    }
            //}
            //else
            if (txtdanwa.Text != "")
            {
                if (TextUtility.IsIncludeZenkaku(txtdanwa.Text))
                {
                    lbl_DanwaErr.Text = "半角英数を入力してください。";
                    txtdanwa.Text = "";
                    txtdanwa.Focus();
                }
                else
                {
                    lbl_DanwaErr.Text = "";
                }
            }
            else
            {
                lbl_DanwaErr.Text = "";
            }
        }
        #endregion

        #region "FAXテキストボックスを変更する"

        /// <summary>
        /// FAXテキストボックスを変更する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void txtfax_TextChanged(object sender, EventArgs e)
        {
            if (txtdanwa.Text != "")
            {
                if (TextUtility.IsIncludeZenkaku(txtfax.Text))
                {
                    lbl_FaxErr.Text = "半角英数を入力してください。";
                    txtfax.Text = "";
                    txtfax.Focus();
                }
                else
                {
                    lbl_FaxErr.Text = "";
                }
            }
            else
            {
                lbl_FaxErr.Text = "";
            }
        }
        #endregion

        #region "説明テキストボックスを変更する"

        /// <summary>
        /// 説明の説明を変更する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void txtSetsuMei_TextChanged(object sender, EventArgs e)
        {
        }
        #endregion

        #region "【ロゴ１変更】ボタン押す処理"

        /// <summary>
        /// 【ロゴ１変更】ボタン押す処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnUpload_Click(object sender, EventArgs e)
        {
            try
            {
                //lblhdnChangeData.Text = "true";
                string[] validFileTypes = { "png", "jpg", "jpeg", "bmp" };

                string strExtension = System.IO.Path.GetExtension(fileupload.PostedFile.FileName);

                bool isValidFile = false;

                if (fileupload.HasFile)
                {
                    for (int i = 0; i < validFileTypes.Length; i++)
                    {
                        // ファイルのタイプをチェックする
                        if (strExtension.ToLower() == "." + validFileTypes[i])
                        {
                            isValidFile = true;
                            break;
                        }
                    }
                    // 画像ファイルの場合
                    if (isValidFile)
                    {
                        // 画像は5M以下ある時
                        if (fileupload.FileBytes.Length < 5242880)
                        {
                            System.Drawing.Image imgBefortThumnailImage = System.Drawing.Image.FromStream(fileupload.PostedFile.InputStream);


                            // Thumbnail画像作成
                            Image(imgBefortThumnailImage);
                            lblImgError.Text = "";

                        }
                        else
                        {
                            // 画像は5M以上ある時、【ファイルサイズが5Mを超えています。】メッセージを表示する
                            //lblImgError.Text = MessageUtility.GetMessage("M0004");
                        }
                    }
                    else
                    {
                        // エラーメッセージを表示する
                        //lblImgError.Text = MessageUtility.GetMessage("M0013");
                    }
                    //updImageError.Update();
                    //updUser.Update();
                    updKyotenToroku.Update();
                }
            }
            catch (Exception ex)
            {
            }

        }
        #endregion

        #region radiobutton1,radiobutton2,radiobutton3,radiobutton4,radiobutton5 CheckedChanged 20220502 maythu delete 

        //#region "radiobutton1"

        ///// <summary>
        ///// radiobutton1
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //protected void rbtnLogo1_CheckedChanged(object sender, EventArgs e)
        //{
        //    string radioVal = "1";
        //    if (txt_cCo.Text == "")
        //    {
        //        txtlogoName1.Text = "ロゴ１";
        //    }
        //    kyotenId = txt_cCo.Text;
        //    frbt = true;
        //    setItemProperty(radioVal);
        //}
        //#endregion

        //#region "radiobutton2"

        ///// <summary>
        ///// radiobutton2
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //protected void rbtnLogo2_CheckedChanged(object sender, EventArgs e)
        //{
        //    string radioVal = "2";
        //    if (txt_cCo.Text == "")
        //    {
        //        txtlogoName2.Text = "ロゴ２";
        //    }
        //    kyotenId = txt_cCo.Text;
        //    frbt = true;
        //    setItemProperty(radioVal);
        //}
        //#endregion

        //#region "radiobutton3"

        ///// <summary>
        ///// radiobutton3
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //protected void rbtnLogo3_CheckedChanged(object sender, EventArgs e)
        //{
        //    string radioVal = "3";
        //    kyotenId = txt_cCo.Text;
        //    frbt = true;
        //    setItemProperty(radioVal);
        //}
        //#endregion

        //#region "radiobutton4"

        ///// <summary>
        ///// radiobutton4
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //protected void rbtnLogo4_CheckedChanged(object sender, EventArgs e)
        //{
        //    string radioVal = "4";
        //    kyotenId = txt_cCo.Text;
        //    frbt = true;
        //    setItemProperty(radioVal);
        //}
        //#endregion

        //#region "radiobutton5"

        ///// <summary>
        ///// radiobutton2
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //protected void rbtnLogo5_CheckedChanged(object sender, EventArgs e)
        //{
        //    string radioVal = "5";
        //    kyotenId = txt_cCo.Text;
        //    frbt = true;
        //    setItemProperty(radioVal);
        //}
        //#endregion

        #endregion

        #region "保存ボタン"

        /// <summary>
        /// 保存ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            bool chkdata = true;
            //FAX番号
            if (String.IsNullOrEmpty(txtJusho1.Text))
            {
                txtJusho1.Text = "";
                txtJusho1.Style.Add("border-color", "red !important");
                txtJusho1.Focus();
                chkdata = false;
            }
            //郵便番号
            if (String.IsNullOrEmpty(txtYubinBangou.Text))
            {
                txtYubinBangou.Text = "";
                txtYubinBangou.Style.Add("border-color", "red !important");
                txtYubinBangou.Focus();
                chkdata = false;
            }
            //会社名
            if (String.IsNullOrEmpty(txtKaishaMei.Text))
            {
                txtKaishaMei.Text = "";
                txtKaishaMei.Style.Add("border-color", "red !important");
                txtKaishaMei.Focus();
                chkdata = false;
            }
            //拠点名
            if (String.IsNullOrEmpty(txtKyotenMei.Text))
            {
                txtKyotenMei.Text = "";
                txtKyotenMei.Style.Add("border-color", "red !important");
                txtKyotenMei.Focus();
                chkdata = false;
            }

            if (btnKyotenLogo5.ImageUrl != "~/Img/logokyoten.png" && btnKyotenLogo5.ImageUrl != "../Img/logokyoten.png")
            {
                //if (txtlogoName5.Text == "")//20220502 maythu delete
                if (DDL_kyoutenLogo.Items[4].Value == "")//20220503 maythu add
                {
                    chkdata = false;
                    lblImgError.Text = "ロゴ５の名が入力されていません。";
                    updImageError.Update();
                }
            }

            if (btnKyotenLogo4.ImageUrl != "~/Img/logokyoten.png" && btnKyotenLogo4.ImageUrl != "../Img/logokyoten.png")
            {
                //if (txtlogoName4.Text == "")//20220502 maythu delete
                if (DDL_kyoutenLogo.Items[3].Value == "")//20220503 maythu add
                {
                    chkdata = false;
                    lblImgError.Text = "ロゴ４の名が入力されていません。";
                    updImageError.Update();
                }
            }

            if (btnKyotenLogo3.ImageUrl != "~/Img/logokyoten.png" && btnKyotenLogo3.ImageUrl != "../Img/logokyoten.png")
            {
                //if (txtlogoName3.Text == "")//20220502 maythu delete
                if (DDL_kyoutenLogo.Items[2].Value == "")//20220503 maythu add
                {
                    chkdata = false;
                    lblImgError.Text = "ロゴ３の名が入力されていません。";
                    updImageError.Update();
                }
            }

            if (btnKyotenLogo2.ImageUrl != "~/Img/logokyoten.png" && btnKyotenLogo2.ImageUrl != "../Img/logokyoten.png")
            {
                //if (txtlogoName2.Text == "")//20220502 maythu delete
                if (DDL_kyoutenLogo.Items[1].Value == "")//20220503 maythu add
                {
                    chkdata = false;
                    lblImgError.Text = "ロゴ２の名が入力されていません。";
                    updImageError.Update();
                }
            }

            if (btnKyotenLogo1.ImageUrl != "~/Img/logokyoten.png" && btnKyotenLogo1.ImageUrl != "../Img/logokyoten.png")
            {
                //if (txtlogoName1.Text == "")//20220502 maythu delete
                if (DDL_kyoutenLogo.Items[0].Value == "")//20220503 maythu add
                {
                    chkdata = false;
                    lblImgError.Text = "ロゴ１の名が入力されていません。";
                    updImageError.Update();
                }
            }


            //拠点情報を保存
            if (chkdata == true)
            {
                // 案件情報を保存する
                //if (PrepareSaveAnkenData())
                //{
                //    
                //    SessionUtility.SetSession("TORIHIKISAKIMEI", null);
                //}
                bool fsave = SaveKyoten();
                //正常に保存する
                if (fsave == true)
                {
                    // ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "parentButtonClick('btnAnkenNyuuryokuSave');", true);
                    //ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "parentButtonClick('btnKyotenNewClose',);", true);
                    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "parentButtonClick('btnKyotenNewClose','Popup');", true);
                }
                //エラーの場合
                else
                {
                    //ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "parentButtonClick('btnAnkenNyuuryokuSave');", true);
                }
            }

            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseLoading", "closeLoadingModal();", true);
                return;
            }

        }
        #endregion

        #region "X　閉じるボタン"

        /// <summary>
        /// X　閉じるボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            previewSession();//20220210 MyatNoe Added

            // ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "parentButtonClick('btnKyotenNewClose','Popup');", true);
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "parentButtonClick('btnKyotenNewClose','Popup');", true);
            //  ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "parentButtonClick('btnTokuisakiClose','Popup');", true);

        }
        #endregion

        #region "プレピューボタン"

        /// <summary>
        /// プレピューボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        //20220124 MyatNoe added //プレビュー画像の
        protected void btnPreview_Click(object sender, EventArgs e)
        {
            mitsumori rpt = new mitsumori();
            rpt.clickedKyoten = "kyoten";
            rpt.sTITLE = txtSetsuMei.Text;

            String id = Request.QueryString["id"];
            DataTable dt_db = JC01Login_Class.Get_DB(id);
            String db = dt_db.Rows[0]["db"].ToString();
            String mail = dt_db.Rows[0]["mail"].ToString();
            ConstantVal.DB_NAME = db;

            JC_ClientConnecction_Class jc = new JC_ClientConnecction_Class();
            jc.loginId = mail;
            DateTime datenow = jc.GetCurrentDate();
            String fileName = "見積書_" + datenow.ToString("yyyyMMdd");

            //if (rbtnLogo1.Checked == true && Session["rdoLogo1"] != null)//20220502 maythu delete
            if (DDL_kyoutenLogo.SelectedIndex == 0 && Session["rdoLogo1"] != null)//20220502 maythu add
            {
                rpt.logourl = Session["rdoLogo1"].ToString();
                rpt.logoByte = Session["rdoLogoByte1"] as byte[];
            }
            //if (rbtnLogo2.Checked == true && Session["rdoLogo2"] != null)//20220502 maythu delete
            if (DDL_kyoutenLogo.SelectedIndex == 1 && Session["rdoLogo2"] != null)//20220502 maythu add
            {
                rpt.logourl = Session["rdoLogo2"].ToString();
                rpt.logoByte = Session["rdoLogoByte2"] as byte[];
            }
            //if (rbtnLogo3.Checked == true && Session["rdoLogo3"] != null)//20220502 maythu delete
            if (DDL_kyoutenLogo.SelectedIndex == 2 && Session["rdoLogo3"] != null)//20220502 maythu add
            {
                rpt.logourl = Session["rdoLogo3"].ToString();
                rpt.logoByte = Session["rdoLogoByte3"] as byte[];
            }
            //if (rbtnLogo4.Checked == true && Session["rdoLogo4"] != null)//20220502 maythu delete
            if (DDL_kyoutenLogo.SelectedIndex == 3 && Session["rdoLogo4"] != null)//20220502 maythu add
            {
                rpt.logourl = Session["rdoLogo4"].ToString();
                rpt.logoByte = Session["rdoLogoByte4"] as byte[];
            }
            //if (rbtnLogo5.Checked == true && Session["rdoLogo5"] != null)//20220502 maythu delete
            if (DDL_kyoutenLogo.SelectedIndex == 4 && Session["rdoLogo5"] != null)//20220502 maythu add
            {
                rpt.logourl = Session["rdoLogo5"].ToString();
                rpt.logoByte = Session["rdoLogoByte5"] as byte[];
            }

            rpt.Run();
            System.IO.MemoryStream ms = new System.IO.MemoryStream();

            GrapeCity.ActiveReports.Export.Pdf.Section.PdfExport pdfExport1 = new GrapeCity.ActiveReports.Export.Pdf.Section.PdfExport();
            pdfExport1.Export(rpt.Document, ms);
            ms.Position = 0;
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

        #region "ロゴ削除ボタン"

        /// <summary>
        /// ロゴ削除ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            //if (rbtnLogo1.Checked == true) //20220502 maythu delete
            if (DDL_kyoutenLogo.SelectedIndex == 0) //20220502 maythu add 
            {
                btnKyotenLogo1.ImageUrl = "../Img/logokyoten.png";
                Session.Remove("rdoLogo1");  //20220126 MyatNoe Add
                Session.Remove("rdoLogoByte1"); //20220209 MyatNoe Add
            }
            //else if (rbtnLogo2.Checked == true)//20220502 maythu delete
            else if (DDL_kyoutenLogo.SelectedIndex == 1)//20220502 maythu add 
            {
                btnKyotenLogo2.ImageUrl = "../Img/logokyoten.png";
                Session.Remove("rdoLogo2");  //20220126 MyatNoe Add 
                Session.Remove("rdoLogoByte2"); //20220209 MyatNoe Add
            }
            //else if (rbtnLogo3.Checked == true)//20220502 maythu delete
            else if (DDL_kyoutenLogo.SelectedIndex == 2)//20220502 maythu add 
            {
                btnKyotenLogo3.ImageUrl = "../Img/logokyoten.png";
                Session.Remove("rdoLogo3");  //20220126 MyatNoe Add
                Session.Remove("rdoLogoByte3"); //20220209 MyatNoe Add
            }
            //else if (rbtnLogo4.Checked == true)//20220502 maythu delete
            else if (DDL_kyoutenLogo.SelectedIndex == 3)//20220502 maythu add 
            {
                btnKyotenLogo4.ImageUrl = "../Img/logokyoten.png";
                Session.Remove("rdoLogo4");  //20220126 MyatNoe Add
                Session.Remove("rdoLogoByte4"); //20220209 MyatNoe Add
            }
            //else if (rbtnLogo5.Checked == true)//20220502 maythu delete
            else if (DDL_kyoutenLogo.SelectedIndex == 4)//20220502 maythu add 
            {
                btnKyotenLogo5.ImageUrl = "../Img/logokyoten.png";
                Session.Remove("rdoLogo5");  //20220126 MyatNoe Add
                Session.Remove("rdoLogoByte5"); //20220209 MyatNoe Add
            }

            updImg.Update();
            lblImgError.Text = "";
            updImageError.Update();
        }
        #endregion

        #region Thumbnail画像作成
        /// <summary>
        /// Thumbnail画像作成
        /// </summary>
        /// <param name="imgBefortThumnailImage">画像</param>
        protected void Image(System.Drawing.Image imgBefortThumnailImage)
        {
            //SessionUtility.SetSession("GAMEN", "UPLOAD");

            // Thumbnail画像作成
            using (System.Drawing.Image imgThumbnailImage = CommonUtility.ThumbnailImageCreate(imgBefortThumnailImage))
            {
                using (MemoryStream msMemoryStream = new MemoryStream())
                {
                    imgThumbnailImage.Save(msMemoryStream, imgBefortThumnailImage.RawFormat);
                    Byte[] bytes = new Byte[msMemoryStream.Length];
                    msMemoryStream.Position = 0;
                    msMemoryStream.Read(bytes, 0, (int)bytes.Length);
                    string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);

                    // 画像設定
                    //imgbtnUser.ImageUrl = "data:image/png;base64," + base64String;
                    //imgbtnUser.CssClass = "divImgUpload";

                    //if (rbtnLogo1.Checked == true)//20220502 maythu delete
                    if (DDL_kyoutenLogo.SelectedIndex == 0)//20220502 maythu add
                    {
                        logonum = "1";
                        btnKyotenLogo1.ImageUrl = "data:image/png;base64," + base64String;
                        btnKyotenLogo1.CssClass = "n_KyotenImgUpload";

                        //if (txtlogoName1.Text == "" && lblImgError.Text == "")//20220502 maythu delete
                        if (txtLogotitle.Text == "" && lblImgError.Text == "")//20220502 maythu add
                        {
                            lblImgError.Text = "ロゴ１の名が入力されていません。";
                        }
                    }
                    //else if (rbtnLogo2.Checked == true)//20220502 maythu delete
                    else if (DDL_kyoutenLogo.SelectedIndex == 1)//20220502 maythu add
                    {
                        logonum = "2";
                        btnKyotenLogo2.ImageUrl = "data:image/png;base64," + base64String;
                        btnKyotenLogo2.CssClass = "n_KyotenImgUpload";

                        //if (txtlogoName2.Text == "" && lblImgError.Text == "")//20220502 maythu delete
                        if (txtLogotitle.Text == "" && lblImgError.Text == "")//20220502 maythu add
                        {
                            lblImgError.Text = "ロゴ２の名が入力されていません。";
                        }
                    }
                    //else if (rbtnLogo3.Checked == true)//20220502 maythu delete
                    else if (DDL_kyoutenLogo.SelectedIndex == 2)//20220502 maythu add
                    {
                        logonum = "3";
                        btnKyotenLogo3.ImageUrl = "data:image/png;base64," + base64String;
                        btnKyotenLogo3.CssClass = "n_KyotenImgUpload";

                        //if (txtlogoName3.Text == "" && lblImgError.Text == "")//20220502 maythu delete
                        if (txtLogotitle.Text == "" && lblImgError.Text == "")//20220502 maythu add
                        {
                            lblImgError.Text = "ロゴ３の名が入力されていません。";
                        }
                    }
                    //else if (rbtnLogo4.Checked == true)//20220502 maythu delete
                    else if (DDL_kyoutenLogo.SelectedIndex == 3)//20220502 maythu add
                    {
                        logonum = "4";
                        btnKyotenLogo4.ImageUrl = "data:image/png;base64," + base64String;
                        btnKyotenLogo4.CssClass = "n_KyotenImgUpload";

                        //if (txtlogoName4.Text == "" && lblImgError.Text == "")//20220502 maythu delete
                        if (txtLogotitle.Text == "" && lblImgError.Text == "")//20220502 maythu add
                        {
                            lblImgError.Text = "ロゴ４の名が入力されていません。";
                        }
                    }
                    //else if (rbtnLogo5.Checked == true)//20220502 maythu delete
                    else if (DDL_kyoutenLogo.SelectedIndex == 4)//20220502 maythu add
                    {
                        logonum = "5";
                        btnKyotenLogo5.ImageUrl = "data:image/png;base64," + base64String;
                        btnKyotenLogo5.CssClass = "n_KyotenImgUpload";

                        //if (txtlogoName5.Text == "" && lblImgError.Text == "")//20220502 maythu delete
                        if (txtLogotitle.Text == "" && lblImgError.Text == "")//20220502 maythu add
                        {
                            lblImgError.Text = "ロゴ５の名が入力されていません。";
                        }
                    }
                    //bytes = Convert.FromBase64String(base64String);
                    //// 入力した画像をViewStateに保存する
                    //ViewState["USERIMAGE"] = bytes;
                    //setItemProperty(logonum);
                    // 幅と高さ計算
                    //CalculateImgWidthHeight(imgBefortThumnailImage);
                }
            }
            #region 20220209 MyatNoe added //プレビュー画像の
            using (System.Drawing.Image ThumbnailImage = CommonUtility.ThumbnailImageCreate(imgBefortThumnailImage))
            {
                using (MemoryStream msMemoryStream = new MemoryStream())
                {
                    ThumbnailImage.Save(msMemoryStream, System.Drawing.Imaging.ImageFormat.Png);

                    Byte[] bytes = new Byte[msMemoryStream.Length];
                    msMemoryStream.Position = 0;
                    msMemoryStream.Read(bytes, 0, (int)bytes.Length);
                    string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);

                    //if (rbtnLogo1.Checked == true)//20220502 maythu delete
                    if (DDL_kyoutenLogo.SelectedIndex == 0)//20220502 maythu add
                    {
                        Session["rdoLogo1"] = base64String;
                        Session["rdoLogoByte1"] = null;
                    }
                    //else if (rbtnLogo2.Checked == true)//20220502 maythu delete
                    else if (DDL_kyoutenLogo.SelectedIndex == 1)//20220502 maythu delete
                    {
                        Session["rdoLogo2"] = base64String;
                        Session["rdoLogoByte2"] = null;
                    }
                    //else if (rbtnLogo3.Checked == true)//20220502 maythu delete
                    else if (DDL_kyoutenLogo.SelectedIndex == 2)//20220502 maythu add
                    {
                        Session["rdoLogo3"] = base64String;
                        Session["rdoLogoByte3"] = null;
                    }
                    //else if (rbtnLogo4.Checked == true)//20220502 maythu delete
                    else if (DDL_kyoutenLogo.SelectedIndex == 3)//20220502 maythu add
                    {
                        Session["rdoLogo4"] = base64String;
                        Session["rdoLogoByte4"] = null;
                    }
                    //else if (rbtnLogo5.Checked == true)//20220502 maythu delete
                    else if (DDL_kyoutenLogo.SelectedIndex == 4)//20220502 maythu add
                    {
                        Session["rdoLogo5"] = base64String;
                        Session["rdoLogoByte5"] = null;
                    }
                }
            }
            #endregion
        }

        #endregion

        #region "Thumbnail画像の幅と高さ計算"

        /// <summary>
        /// Thumbnail画像の幅と高さ計算
        /// </summary>
        /// <param name="imgBefortThumnailImage">画像</param>
        protected void CalculateImgWidthHeight(System.Drawing.Image imgBefortThumnailImage)
        {
            // 幅と高さ計算
            int intThumbnailWidth = 0;
            int intThumbnailHeight = 0;
            double dbChangePercent = 0;
            int intImageWidth = imgBefortThumnailImage.Width;
            int intImageHeight = imgBefortThumnailImage.Height;
            if (intImageWidth > intImageHeight)
            {
                intThumbnailWidth = 296;// 126;
                dbChangePercent = (100.0 * 250) / intImageWidth;
                intThumbnailHeight = (int)(Math.Round((dbChangePercent * intImageHeight) / 100));
            }
            else
            {
                intThumbnailHeight = 296; //127;
                dbChangePercent = (100.0 * 300) / intImageHeight;
                intThumbnailWidth = (int)(Math.Round((dbChangePercent * intImageWidth) / 100));
            }

            // 画像の高さによって表示設定
            //if (intThumbnailHeight < 131)
            //{
            //    string strpadding = ((131 - intThumbnailHeight) / 2).ToString();
            //    btnKyotenLogo1.Style.Add("padding-top", strpadding + "px");
            //}
            //else
            //{
            //    btnKyotenLogo1.Style.Add("padding-top", "0px");
            //}
        }

        #endregion

        #region "拠点の変更"

        /// <summary>
        /// 拠点を変更するとDBからデータ取得
        /// </summary>
        /// <param name="imgBefortThumnailImage">画像</param>
        protected void ReadData()
        {
            try
            {
                txt_cCo.Text = kyotenId;
                string sql = "SELECT cCo ";
                sql += ", ifnull(sCo,'') as sCo ";
                sql += ", ifnull(cYUUBIN,'') as cYUUBIN ";
                sql += ", ifnull(sJUUSHO1,'') as sJUUSHO1 ";
                sql += ", ifnull(sJUUSHO2,'') as sJUUSHO2 ";
                sql += ", ifnull(sTEL,'') as sTEL ";
                sql += ", ifnull(sFAX,'') as sFAX ";
                sql += ", ifnull(sKYOTEN,'') as sKYOTEN ";
                sql += ", ifnull(sIMAGE1,'') as sIMAGE1 ";
                sql += ", ifnull(sIMAGE2,'') as sIMAGE2 ";
                sql += ", ifnull(sIMAGE3,'') as sIMAGE3 ";
                sql += ", ifnull(sIMAGE4,'') as sIMAGE4 ";
                sql += ", ifnull(sIMAGE5,'') as sIMAGE5 ";
                sql += ", ifnull(sIMAGETitle1,'') as sIMAGETitle1 ";
                sql += ", ifnull(sIMAGETitle2,'') as sIMAGETitle2 ";
                sql += ", ifnull(sIMAGETitle3,'') as sIMAGETitle3 ";
                sql += ", ifnull(sIMAGETitle4,'') as sIMAGETitle4 ";
                sql += ", ifnull(sIMAGETitle5,'') as sIMAGETitle5 ";
                sql += ", ifnull(fIMAGE,'') as fIMAGE ";
                sql += ", ifnull(sNAIYOU,'') as sNAIYOU ";
                sql += ", ifnull(sURL,'') as sURL ";//20220502 maythu add
                sql += ", ifnull(sMAIL,'') as sMAIL ";//20220502 maythu add
                sql += " FROM m_j_info ";
                sql += " Where cCo ='" + kyotenId + "'";
                String id = Request.QueryString["id"];
                DataTable dt_db = JC01Login_Class.Get_DB(id);
                String db = dt_db.Rows[0]["db"].ToString();
                String mail = dt_db.Rows[0]["mail"].ToString();
                ConstantVal.DB_NAME = db;
                DataTable kyoutenDt = ktVal.KyotenNyuuryokuTable(sql);
                if (kyoutenDt.Rows.Count != 0)
                {
                    txtKaishaMei.Text = kyoutenDt.Rows[0]["sCo"].ToString();  //会社名  
                    txtYubinBangou.Text = kyoutenDt.Rows[0]["cYUUBIN"].ToString();//郵便番号 
                    txtJusho1.Text = kyoutenDt.Rows[0]["sJUUSHO1"].ToString();//住所１
                    txtjusho2.Text = kyoutenDt.Rows[0]["sJUUSHO2"].ToString();//住所２                 
                    txtdanwa.Text = kyoutenDt.Rows[0]["sTEL"].ToString();//電話番号  
                    txtfax.Text = kyoutenDt.Rows[0]["sFAX"].ToString();//FAX番号  
                    txtKyotenMei.Text = kyoutenDt.Rows[0]["sKYOTEN"].ToString(); //拠点名 
                    txtSetsuMei.Text = kyoutenDt.Rows[0]["sNAIYOU"].ToString();
                    txtURL.Text = kyoutenDt.Rows[0]["sURL"].ToString();//20220502 maythu add
                    txtmail.Text = kyoutenDt.Rows[0]["sMAIL"].ToString();//20220502 maythu add
                    //txtlogoName1.Text = kyoutenDt.Rows[0]["sIMAGETitle1"].ToString();//20220502 maythu delete
                    //txtlogoName2.Text = kyoutenDt.Rows[0]["sIMAGETitle2"].ToString();//20220502 maythu delete
                    //txtlogoName3.Text = kyoutenDt.Rows[0]["sIMAGETitle3"].ToString();//20220502 maythu delete
                    //txtlogoName4.Text = kyoutenDt.Rows[0]["sIMAGETitle4"].ToString();//20220502 maythu delete
                    //txtlogoName5.Text = kyoutenDt.Rows[0]["sIMAGETitle5"].ToString();//20220502 maythu delete

                    //20220502 maythu add start
                    DDL_kyoutenLogo.Items.Clear();
                    //DDL_kyoutenLogo.Items.Add(new ListItem("", ""));
                    DDL_kyoutenLogo.Items.Add(new ListItem("ロゴ1", kyoutenDt.Rows[0]["sIMAGETitle1"].ToString()));
                    DDL_kyoutenLogo.Items.Add(new ListItem("ロゴ2", kyoutenDt.Rows[0]["sIMAGETitle2"].ToString()));
                    DDL_kyoutenLogo.Items.Add(new ListItem("ロゴ3", kyoutenDt.Rows[0]["sIMAGETitle3"].ToString()));
                    DDL_kyoutenLogo.Items.Add(new ListItem("ロゴ4", kyoutenDt.Rows[0]["sIMAGETitle4"].ToString()));
                    DDL_kyoutenLogo.Items.Add(new ListItem("ロゴ5", kyoutenDt.Rows[0]["sIMAGETitle5"].ToString()));
                    //20220502 maythu add end

                    kyoutenDt.Rows[0]["fIMAGE"].ToString();
                    logonum = kyoutenDt.Rows[0]["fIMAGE"].ToString();
                    if (logonum == "")
                    {
                        logonum = "1";
                    }
                    //setVisibleItem(logonum);

                    byte[] img_1byte = (byte[])kyoutenDt.Rows[0]["sIMAGE1"];
                    if (img_1byte.Length != 0)
                    {
                        string base64String1 = Convert.ToBase64String(img_1byte);//now convert byte[] to Base64
                        btnKyotenLogo1.ImageUrl = string.Format("data:image/png;base64,{0}", base64String1);
                    }
                    else
                    {
                        btnKyotenLogo1.ImageUrl = "../Img/logokyoten.png";
                    }


                    byte[] img_2byte = (byte[])kyoutenDt.Rows[0]["sIMAGE2"];
                    if (img_2byte.Length != 0)
                    {
                        string base64String2 = Convert.ToBase64String(img_2byte);//now convert byte[] to Base64
                        btnKyotenLogo2.ImageUrl = string.Format("data:image/png;base64,{0}", base64String2);
                    }
                    else
                    {
                        btnKyotenLogo2.ImageUrl = "../Img/logokyoten.png";
                    }


                    byte[] img_3byte = (byte[])kyoutenDt.Rows[0]["sIMAGE3"];
                    if (img_3byte.Length != 0)
                    {
                        string base64String3 = Convert.ToBase64String(img_3byte);//now convert byte[] to Base64
                        btnKyotenLogo3.ImageUrl = string.Format("data:image/png;base64,{0}", base64String3);
                    }
                    else
                    {
                        btnKyotenLogo3.ImageUrl = "../Img/logokyoten.png";
                    }


                    byte[] img_4byte = (byte[])kyoutenDt.Rows[0]["sIMAGE4"];
                    if (img_4byte.Length != 0)
                    {
                        string base64String4 = Convert.ToBase64String(img_4byte);//now convert byte[] to Base64
                        btnKyotenLogo4.ImageUrl = string.Format("data:image/png;base64,{0}", base64String4);
                    }
                    else
                    {
                        btnKyotenLogo4.ImageUrl = "../Img/logokyoten.png";
                    }


                    byte[] img_5byte = (byte[])kyoutenDt.Rows[0]["sIMAGE5"];
                    if (img_5byte.Length != 0)
                    {
                        string base64String5 = Convert.ToBase64String(img_5byte);//now convert byte[] to Base64
                        btnKyotenLogo5.ImageUrl = string.Format("data:image/png;base64,{0}", base64String5);
                    }
                    else
                    {
                        btnKyotenLogo5.ImageUrl = "../Img/logokyoten.png";
                    }

                    #region kyoten preview //20220209 MyatNoe Added(kyoten preview)
                    if (kyoutenDt.Rows.Count != 0)
                    {
                        if (!string.IsNullOrEmpty(kyoutenDt.Rows[0]["sIMAGE1"].ToString()))
                        {
                            byte[] bytes = (byte[])kyoutenDt.Rows[0]["sIMAGE1"];
                            string base64String = Convert.ToBase64String(bytes);
                            Session["rdoLogo1"] = base64String;
                            Session["rdoLogoByte1"] = bytes;
                        }
                        if (!string.IsNullOrEmpty(kyoutenDt.Rows[0]["sIMAGE2"].ToString()))
                        {
                            byte[] bytes = (byte[])kyoutenDt.Rows[0]["sIMAGE2"];
                            string base64String = Convert.ToBase64String(bytes);
                            Session["rdoLogo2"] = base64String;
                            Session["rdoLogoByte2"] = bytes;
                        }
                        if (!string.IsNullOrEmpty(kyoutenDt.Rows[0]["sIMAGE3"].ToString()))
                        {
                            byte[] bytes = (byte[])kyoutenDt.Rows[0]["sIMAGE3"];
                            string base64String = Convert.ToBase64String(bytes);
                            Session["rdoLogo3"] = base64String;
                            Session["rdoLogoByte3"] = bytes;
                        }
                        if (!string.IsNullOrEmpty(kyoutenDt.Rows[0]["sIMAGE4"].ToString()))
                        {
                            byte[] bytes = (byte[])kyoutenDt.Rows[0]["sIMAGE4"];
                            string base64String = Convert.ToBase64String(bytes);
                            Session["rdoLogo4"] = base64String;
                            Session["rdoLogoByte4"] = bytes;
                        }
                        if (!string.IsNullOrEmpty(kyoutenDt.Rows[0]["sIMAGE5"].ToString()))
                        {
                            byte[] bytes = (byte[])kyoutenDt.Rows[0]["sIMAGE5"];
                            string base64String = Convert.ToBase64String(bytes);
                            Session["rdoLogo5"] = base64String;
                            Session["rdoLogoByte5"] = bytes;
                        }
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {
            }
        }
        #endregion

        #region 拠点保存
        protected bool SaveKyoten()
        {
            bool fsave = false;
            try
            {
                int retval = 0;
                string cCoVal = "";
                if (txt_cCo.Text == "")
                {
                    cCoVal = FindKyotenCode();
                }
                else
                {
                    cCoVal = txt_cCo.Text;
                }

                string kyotenmei = "";
                //拠点名 sKYOTEN
                if (!String.IsNullOrEmpty(txtKyotenMei.Text.Trim()))
                {
                    if (txtKyotenMei.Text.Contains("'") == true)
                    {
                        kyotenmei = txtKyotenMei.Text.Replace("'", @"\'");

                    }
                    else
                    {
                        kyotenmei = txtKyotenMei.Text;
                    }
                    kyotenmei = kyotenmei.Trim();
                }

                //会社名 sCO
                string kaishamei = "";
                if (!String.IsNullOrEmpty(txtKaishaMei.Text.Trim()))
                {
                    if (txtKaishaMei.Text.Contains("'") == true)
                    {
                        kaishamei = txtKaishaMei.Text.Replace("'", @"\'");

                    }
                    else
                    {
                        kaishamei = txtKaishaMei.Text;
                    }
                    kaishamei = kaishamei.Trim();
                }


                //郵便番号 cYUUBIN
                string yubinbango = "";
                if (!String.IsNullOrEmpty(txtYubinBangou.Text.Trim()))
                {
                    //if (txtYubinBangou.Text.Contains("'") == true)
                    //{
                    //    yubinbango = txtYubinBangou.Text.Replace("'", @"\'");

                    //}
                    //else
                    //{
                    //    yubinbango = txtYubinBangou.Text;
                    //}
                    yubinbango = txtYubinBangou.Text.Trim();
                }

                //sJUUSHO1
                string jusho1 = "";
                if (!String.IsNullOrEmpty(txtJusho1.Text.Trim()))
                {
                    if (txtJusho1.Text.Contains("'") == true)
                    {
                        jusho1 = txtJusho1.Text.Replace("'", @"\'");

                    }
                    else
                    {
                        jusho1 = txtJusho1.Text;
                    }
                    jusho1 = jusho1.Trim();
                }

                //sJUUSHO2
                string jusho2 = "";
                if (!String.IsNullOrEmpty(txtjusho2.Text.Trim()))
                {
                    if (txtjusho2.Text.Contains("'") == true)
                    {
                        jusho2 = txtjusho2.Text.Replace("'", @"\'");

                    }
                    else
                    {
                        jusho2 = txtjusho2.Text;
                    }
                    jusho2 = jusho2.Trim();
                }

                //sTEL
                string danwabango = "";
                if (!String.IsNullOrEmpty(txtdanwa.Text.Trim()))
                {
                    //if (txtdanwa.Text.Contains("'") == true)
                    //{
                    //    danwabango = txtdanwa.Text.Replace("'", @"\'");

                    //}
                    //else
                    //{
                    //    danwabango = txtdanwa.Text;
                    //}
                    danwabango = txtdanwa.Text.Trim();
                }

                //sFAX
                string fax = "";
                if (!String.IsNullOrEmpty(txtfax.Text.Trim()))
                {
                    //if (txtfax.Text.Contains("'") == true)
                    //{
                    //    fax = txtfax.Text.Replace("'", @"\'");

                    //}
                    //else
                    //{
                    //    fax = txtfax.Text;
                    //}
                    fax = txtfax.Text.Trim();
                }


                Byte[] imgbyte1 = null;
                string img1 = "";
                if (btnKyotenLogo1.ImageUrl != "~/Img/logokyoten.png" && btnKyotenLogo1.ImageUrl != "../Img/logokyoten.png")
                {
                    string urlimageVal = btnKyotenLogo1.ImageUrl;
                    string base64String = urlimageVal.Replace("data:image/png;base64,", "");
                    imgbyte1 = Convert.FromBase64String(base64String);

                }
                else
                {
                    img1 = "@null";
                }

                Byte[] imgbyte2 = null;
                string img2 = "";
                if (btnKyotenLogo2.ImageUrl != "~/Img/logokyoten.png" && btnKyotenLogo2.ImageUrl != "../Img/logokyoten.png")
                {
                    string urlimageVal = btnKyotenLogo2.ImageUrl;
                    string base64String = urlimageVal.Replace("data:image/png;base64,", "");
                    imgbyte2 = Convert.FromBase64String(base64String);
                }
                else
                {
                    img2 = "@null";
                }

                Byte[] imgbyte3 = null;
                string img3 = "";
                if (btnKyotenLogo3.ImageUrl != "~/Img/logokyoten.png" && btnKyotenLogo3.ImageUrl != "../Img/logokyoten.png")
                {
                    string urlimageVal = btnKyotenLogo3.ImageUrl;
                    string base64String = urlimageVal.Replace("data:image/png;base64,", "");
                    imgbyte3 = Convert.FromBase64String(base64String);
                }
                else
                {
                    img3 = "@null";
                }

                Byte[] imgbyte4 = null;
                string img4 = "";
                if (btnKyotenLogo4.ImageUrl != "~/Img/logokyoten.png" && btnKyotenLogo4.ImageUrl != "../Img/logokyoten.png")
                {
                    string urlimageVal = btnKyotenLogo4.ImageUrl;
                    string base64String = urlimageVal.Replace("data:image/png;base64,", "");
                    imgbyte4 = Convert.FromBase64String(base64String);
                }
                else
                {
                    img4 = "@null";
                }

                Byte[] imgbyte5 = null;
                string img5 = "";
                if (btnKyotenLogo5.ImageUrl != "~/Img/logokyoten.png" && btnKyotenLogo5.ImageUrl != "../Img/logokyoten.png")
                {
                    string urlimageVal = btnKyotenLogo5.ImageUrl;
                    string base64String = urlimageVal.Replace("data:image/png;base64,", "");
                    imgbyte5 = Convert.FromBase64String(base64String);
                }
                else
                {
                    img5 = "@null";
                }

                string txtSetsuVal = "";
                if (!String.IsNullOrEmpty(txtSetsuMei.Text.Trim()))
                {
                    if (txtSetsuMei.Text.Contains("'") == true)
                    {
                        txtSetsuVal = txtSetsuMei.Text.Replace("'", @"\'");

                    }
                    else
                    {
                        txtSetsuVal = txtSetsuMei.Text;
                    }
                    txtSetsuVal = txtSetsuVal.Trim();
                }

                //20220502 maythu add start
                //HP URL
                string txtURLVal = "";
                if (!String.IsNullOrEmpty(txtURL.Text.Trim()))
                {
                    if (txtURL.Text.Contains("'") == true)
                    {
                        txtURLVal = txtURL.Text.Replace("'", @"\'");

                    }
                    else
                    {
                        txtURLVal = txtURL.Text;
                    }
                    txtURLVal = txtURLVal.Trim();
                }

                //代表mail
                string txtMailVal = "";
                if (!String.IsNullOrEmpty(txtmail.Text.Trim()))
                {
                    if (txtmail.Text.Contains("'") == true)
                    {
                        txtMailVal = txtmail.Text.Replace("'", @"\'");

                    }
                    else
                    {
                        txtMailVal = txtmail.Text;
                    }
                    txtMailVal = txtMailVal.Trim();
                }
                //20220502 maythu add end

                #region 20220502 maythu delete
                //string logotitle1 = "";
                //if (!String.IsNullOrEmpty(txtlogoName1.Text.Trim()))
                //{
                //    if (txtlogoName1.Text.Contains("'") == true)
                //    {
                //        logotitle1 = txtlogoName1.Text.Replace("'", @"\'");

                //    }
                //    else
                //    {
                //        logotitle1 = txtlogoName1.Text;
                //    }
                //    logotitle1 = logotitle1.Trim();
                //}

                //string logotitle2 = "";
                //if (!String.IsNullOrEmpty(txtlogoName2.Text.Trim()))
                //{
                //    if (txtlogoName2.Text.Contains("'") == true)
                //    {
                //        logotitle2 = txtlogoName2.Text.Replace("'", @"\'");

                //    }
                //    else
                //    {
                //        logotitle2 = txtlogoName2.Text;
                //    }
                //    logotitle2 = logotitle2.Trim();
                //}

                //string logotitle3 = "";
                //if (!String.IsNullOrEmpty(txtlogoName3.Text.Trim()))
                //{
                //    if (txtlogoName3.Text.Contains("'") == true)
                //    {
                //        logotitle3 = txtlogoName3.Text.Replace("'", @"\'");

                //    }
                //    else
                //    {
                //        logotitle3 = txtlogoName3.Text;
                //    }
                //    logotitle3 = logotitle3.Trim();
                //}

                //string logotitle4 = "";
                //if (!String.IsNullOrEmpty(txtlogoName4.Text.Trim()))
                //{
                //    if (txtlogoName4.Text.Contains("'") == true)
                //    {
                //        logotitle4 = txtlogoName4.Text.Replace("'", @"\'");

                //    }
                //    else
                //    {
                //        logotitle4 = txtlogoName4.Text;
                //    }
                //    logotitle4 = logotitle4.Trim();
                //}

                //string logotitle5 = "";
                //if (!String.IsNullOrEmpty(txtlogoName5.Text.Trim()))
                //{
                //    if (txtlogoName5.Text.Contains("'") == true)
                //    {
                //        logotitle5 = txtlogoName5.Text.Replace("'", @"\'");

                //    }
                //    else
                //    {
                //        logotitle5 = txtlogoName5.Text;
                //    }
                //    logotitle5 = logotitle5.Trim();
                //}

                //string fimagetitle = "";
                //if (rbtnLogo1.Checked == true)
                //{
                //    fimagetitle = "1";
                //}
                //else if (rbtnLogo2.Checked == true)
                //{
                //    fimagetitle = "2";
                //}
                //else if (rbtnLogo3.Checked == true)
                //{
                //    fimagetitle = "3";
                //}
                //else if (rbtnLogo4.Checked == true)
                //{
                //    fimagetitle = "4";
                //}
                //else if (rbtnLogo5.Checked == true)
                //{
                //    fimagetitle = "5";
                //}
                #endregion

                //20220502 maythu add start
                string logotitle1 = "";
                logotitle1 = DDL_kyoutenLogo.Items[0].Value;
                if (!String.IsNullOrEmpty(logotitle1.Trim()))
                {
                    if (logotitle1.Contains("'") == true)
                    {
                        logotitle1 = logotitle1.Replace("'", @"\'");

                    }
                    logotitle1 = logotitle1.Trim();
                }

                string logotitle2 = "";
                logotitle2 = DDL_kyoutenLogo.Items[1].Value;
                if (!String.IsNullOrEmpty(logotitle2.Trim()))
                {
                    if (logotitle2.Contains("'") == true)
                    {
                        logotitle2 = logotitle2.Replace("'", @"\'");

                    }
                    logotitle2 = logotitle2.Trim();
                }

                string logotitle3 = "";
                logotitle3 = DDL_kyoutenLogo.Items[2].Value;
                if (!String.IsNullOrEmpty(logotitle3.Trim()))
                {
                    if (logotitle3.Contains("'") == true)
                    {
                        logotitle3 = logotitle3.Replace("'", @"\'");

                    }
                    logotitle3 = logotitle3.Trim();
                }

                string logotitle4 = "";
                logotitle4 = DDL_kyoutenLogo.Items[3].Value;
                if (!String.IsNullOrEmpty(logotitle4.Trim()))
                {
                    if (logotitle4.Contains("'") == true)
                    {
                        logotitle4 = logotitle4.Replace("'", @"\'");

                    }
                    logotitle4 = logotitle4.Trim();
                }

                string logotitle5 = "";
                logotitle5 = DDL_kyoutenLogo.Items[4].Value;
                if (!String.IsNullOrEmpty(logotitle5.Trim()))
                {
                    if (logotitle5.Contains("'") == true)
                    {
                        logotitle5 = logotitle5.Replace("'", @"\'");

                    }
                    logotitle5 = logotitle5.Trim();
                }

                string fimagetitle = "";
                if (DDL_kyoutenLogo.SelectedIndex == 0)
                {
                    fimagetitle = "1";
                }
                if (DDL_kyoutenLogo.SelectedIndex == 1)
                {
                    fimagetitle = "2";
                }
                if (DDL_kyoutenLogo.SelectedIndex == 2)
                {
                    fimagetitle = "3";
                }
                if (DDL_kyoutenLogo.SelectedIndex == 3)
                {
                    fimagetitle = "4";
                }
                if (DDL_kyoutenLogo.SelectedIndex == 4)
                {
                    fimagetitle = "5";
                }
                //20220502 maythu add end

                String id = Request.QueryString["id"];
                DataTable dt_db = JC01Login_Class.Get_DB(id);
                String db = dt_db.Rows[0]["db"].ToString();
                String mail = dt_db.Rows[0]["mail"].ToString();
                ConstantVal.DB_NAME = db;

                ktVal.loginId = mail;
                //find current date time
                DateTime curDateTime = ktVal.FindCurTime();

                string sql = "INSERT INTO m_j_info(";
                sql += " cCo ";
                sql += ", sCo ";
                sql += ", cYUUBIN ";
                sql += ", sJUUSHO1 ";
                sql += ", sJUUSHO2 ";
                sql += ", sTEL ";
                sql += ", sFAX ";
                sql += ", dHENKOU ";
                sql += ", cHENKOUSYA ";
                sql += ", sKYOTEN ";
                sql += ", sIMAGE1 ";
                sql += ", sIMAGE2 ";
                sql += ", sIMAGE3 ";
                sql += ", sIMAGE4 ";
                sql += ", sIMAGE5 ";
                sql += ", sNAIYOU ";
                sql += ", sURL ";//20220502 maythu add
                sql += ", sMAIL ";//20220502 maythu add
                sql += ",sIMAGETitle1 ";
                sql += ",sIMAGETitle2 ";
                sql += ",sIMAGETitle3 ";
                sql += ",sIMAGETitle4 ";
                sql += ",sIMAGETitle5 ";
                sql += ",fIMAGE  ";
                sql += ")VALUES  ";
                sql += " ( '" + cCoVal + "'";
                sql += " , '" + kaishamei + "'";
                sql += " , '" + yubinbango + "'";
                sql += " , '" + jusho1 + "'";
                sql += " , '" + jusho2 + "'";
                sql += " , '" + danwabango + "'";
                sql += " , '" + fax + "'";
                sql += " , '" + curDateTime + "' ";
                sql += " , '9999'";
                sql += " , '" + kyotenmei + "'";
                sql += " , @image1 ";
                sql += " , @image2  ";
                sql += " , @image3  ";
                sql += " , @image4  ";
                sql += " , @image5  ";
                sql += " , '" + txtSetsuVal + "'  ";
                sql += " , '" + txtURLVal + "'  ";//20220502 maythu add
                sql += " , '" + txtMailVal + "'  ";//20220502 maythu add
                sql += " , '" + logotitle1 + "' ";
                sql += " , '" + logotitle2 + "' ";
                sql += " , '" + logotitle3 + "' ";
                sql += " , '" + logotitle4 + "' ";
                sql += " , '" + logotitle5 + "' ";
                sql += " , '" + fimagetitle + "' ";
                sql += "  ) ";
                sql += " ON DUPLICATE KEY UPDATE cCo = '" + cCoVal + "'";
                sql += " ,sCo =  '" + kaishamei + "' ";
                sql += " ,cYUUBIN =  '" + yubinbango + "' ";
                sql += " ,sJUUSHO1 =  '" + jusho1 + "' ";
                sql += " ,sJUUSHO2 =  '" + jusho2 + "' ";
                sql += " ,sTEL =  '" + danwabango + "' ";
                sql += " ,sFAX =  '" + fax + "' ";
                sql += " ,dHENKOU = '" + curDateTime + "' ";
                sql += " ,cHENKOUSYA = '9999'";
                sql += " ,sKYOTEN =  '" + kyotenmei + "' ";
                sql += " ,sIMAGE1 =  @image1 ";
                sql += " ,sIMAGE2 =  @image2 ";
                sql += " ,sIMAGE3 =  @image3 ";
                sql += " ,sIMAGE4 =  @image4 ";
                sql += " ,sIMAGE5 =  @image5 ";
                sql += " ,sNAIYOU ='" + txtSetsuVal + "'";
                sql += " ,sURL ='" + txtURLVal + "'";//20220502 maythu add
                sql += " ,sMAIL ='" + txtMailVal + "'";//20220502 maythu add
                sql += ",sIMAGETitle1 = '" + logotitle1 + "' ";
                sql += ",sIMAGETitle2 = '" + logotitle2 + "' ";
                sql += ",sIMAGETitle3 = '" + logotitle3 + "' ";
                sql += ",sIMAGETitle4 = '" + logotitle4 + "' ";
                sql += ",sIMAGETitle5 = '" + logotitle5 + "' ";
                sql += ",fIMAGE = '" + fimagetitle + "' ;";

                ConstantVal.DB_NAME = db;
                ktVal.loginId = mail;
                fsave = ktVal.KyotenNyuuryokuSql(sql, imgbyte1, imgbyte2, imgbyte3, imgbyte4, imgbyte5);

            }
            catch (Exception ec)
            {

            }

            return fsave;
        }
        #endregion

        #region 新規拠点コード検索
        protected string FindKyotenCode()
        {
            string ColVal = "";
            System.Data.DataTable dt = new System.Data.DataTable();
            string sqlStr = "SELECT cCo,sKYOTEN FROM m_j_info; ";
            String id = Request.QueryString["id"];
            DataTable dt_db = JC01Login_Class.Get_DB(id);
            String db = dt_db.Rows[0]["db"].ToString();
            String mail = dt_db.Rows[0]["mail"].ToString();
            ConstantVal.DB_NAME = db;
            ktVal.loginId = mail;
            dt = ktVal.KyotenNyuuryokuTable(sqlStr);

            List<int> ListShain = new List<int>();
            foreach (System.Data.DataRow dr in dt.Rows)
            {
                ListShain.Add(int.Parse(dr["cCo"].ToString()));
            }
            if (ListShain.Count > 0)
            {
                var MissingNumbers = Enumerable.Range(1, 9999).Except(ListShain).ToList();
                var ResultNum = MissingNumbers.Min();
                ColVal = ResultNum.ToString().PadLeft(2, '0');
            }
            else
            {
                var MissingNumbers = 1;
                ColVal = MissingNumbers.ToString().PadLeft(2, '0');
            }
            return ColVal;
        }
        #endregion

        #region 画像
        protected void imgBtn_click(object sender, EventArgs e)
        {
            try
            {
                //fileupload image as 64basestring
                string imgStr = "";
                imgStr = logo.Value;
                string[] subs = imgStr.Split(',');
                String base64Image = subs[1];
                byte[] Filebytes = Convert.FromBase64String(base64Image);

                //fileupload extension 
                string strExtension = ext.Value;
                string[] ArrExt = strExtension.Split('.');
                strExtension = "." + ArrExt[1];

                string[] validFileTypes = { "png", "jpg", "jpeg", "bmp" };

                bool isValidFile = false;

                if (imgStr != "")
                {
                    for (int i = 0; i < validFileTypes.Length; i++)
                    {
                        // ファイルのタイプをチェックする
                        if (strExtension.ToLower() == "." + validFileTypes[i])
                        {
                            isValidFile = true;
                            break;
                        }
                    }
                    // 画像ファイルの場合
                    if (isValidFile)
                    {
                        // 画像は5M以下ある時
                        if (Filebytes.Length < 5242880)
                        {
                            System.Drawing.Image imgBefortThumnailImage;
                            using (MemoryStream ms = new MemoryStream(Filebytes))
                            {
                                imgBefortThumnailImage = System.Drawing.Image.FromStream(ms);
                            }
                            // Thumbnail画像作成
                            Image(imgBefortThumnailImage);

                        }
                        else
                        {
                            // 画像は5M以上ある時、【ファイルサイズが5Mを超えています。】メッセージを表示する
                            //lblImgError.Text = MessageUtility.GetMessage("M0004");
                        }
                    }
                    else
                    {
                        // エラーメッセージを表示する
                        //lblImgError.Text = MessageUtility.GetMessage("M0013");
                    }

                }
            }
            catch (Exception ex)
            {
            }
            updImg.Update();
            updImageError.Update();
            logo.Value = "";
            btnKyotenLogo1.CssClass = "JC25Kt_NyuurokuImg";
            btnKyotenLogo2.CssClass = "JC25Kt_NyuurokuImg";
            btnKyotenLogo3.CssClass = "JC25Kt_NyuurokuImg";
            btnKyotenLogo4.CssClass = "JC25Kt_NyuurokuImg";
            btnKyotenLogo5.CssClass = "JC25Kt_NyuurokuImg";
        }
        #endregion

        #region 画像関連の更新
        protected void setItemProperty(string radStrval)
        {
            ImageButton imgbtn1 = new ImageButton();
            imgbtn1.ImageUrl = btnKyotenLogo1.ImageUrl;

            ImageButton imgbtn2 = new ImageButton();
            imgbtn2.ImageUrl = btnKyotenLogo2.ImageUrl;

            ImageButton imgbtn3 = new ImageButton();
            imgbtn3.ImageUrl = btnKyotenLogo3.ImageUrl;

            ImageButton imgbtn4 = new ImageButton();
            imgbtn4.ImageUrl = btnKyotenLogo4.ImageUrl;

            ImageButton imgbtn5 = new ImageButton();
            imgbtn5.ImageUrl = btnKyotenLogo5.ImageUrl;

            if (kyotenId == "01")
            {
                if (radStrval == "1" || radStrval == "")
                {
                    btnKyotenLogo1.Visible = true;
                    btnKyotenLogo2.Visible = false;
                    btnKyotenLogo3.Visible = false;
                    btnKyotenLogo4.Visible = false;
                    btnKyotenLogo5.Visible = false;

                    //20220502 maythu delete start
                    //txtlogoName1.Attributes.Remove("readonly");
                    //txtlogoName2.Attributes.Add("readonly", "true");
                    //txtlogoName3.Attributes.Add("readonly", "true");
                    //txtlogoName4.Attributes.Add("readonly", "true");
                    //txtlogoName5.Attributes.Add("readonly", "true");                   
                    //if (frbt == true)
                    //{
                    //    txtlogoName1.Focus();
                    //}
                    //rbtnLogo1.Checked = true;
                    //20220502 maythu delete end

                    DDL_kyoutenLogo.ClearSelection();//20220502 maythu add
                    DDL_kyoutenLogo.Items.FindByText("ロゴ1").Selected = true;//20220502 maythu add
                    txtLogotitle.Text = DDL_kyoutenLogo.SelectedItem.Value;//20220502 maythu add
                }
                else if (radStrval == "2")
                {
                    btnKyotenLogo1.Visible = false;
                    btnKyotenLogo2.Visible = true;
                    btnKyotenLogo3.Visible = false;
                    btnKyotenLogo4.Visible = false;
                    btnKyotenLogo5.Visible = false;

                    //20220502 maythu delete start
                    //txtlogoName1.Attributes.Add("readonly", "true");
                    //txtlogoName2.Attributes.Remove("readonly");
                    //txtlogoName3.Attributes.Add("readonly", "true");
                    //txtlogoName4.Attributes.Add("readonly", "true");
                    //txtlogoName5.Attributes.Add("readonly", "true");

                    //if (frbt == true)
                    //{
                    //    txtlogoName2.Focus();
                    //}
                    //rbtnLogo2.Checked = true;
                    //20220502 maythu delete end

                    DDL_kyoutenLogo.ClearSelection();//20220502 maythu add
                    DDL_kyoutenLogo.Items.FindByText("ロゴ2").Selected = true;//20220502 maythu add
                    txtLogotitle.Text = DDL_kyoutenLogo.SelectedItem.Value;//20220502 maythu add
                }
                else if (radStrval == "3")
                {
                    btnKyotenLogo1.Visible = false;
                    btnKyotenLogo2.Visible = false;
                    btnKyotenLogo3.Visible = true;
                    btnKyotenLogo4.Visible = false;
                    btnKyotenLogo5.Visible = false;

                    //20220502 maythu delete start
                    //txtlogoName1.Attributes.Add("readonly", "true");
                    //txtlogoName2.Attributes.Add("readonly", "true");
                    //txtlogoName3.Attributes.Remove("readonly");
                    //txtlogoName4.Attributes.Add("readonly", "true");
                    //txtlogoName5.Attributes.Add("readonly", "true");

                    //if (frbt == true)
                    //{
                    //    txtlogoName3.Focus();
                    //}
                    //rbtnLogo3.Checked = true;
                    //20220502 maythu delete end

                    DDL_kyoutenLogo.ClearSelection();//20220502 maythu add
                    DDL_kyoutenLogo.Items.FindByText("ロゴ3").Selected = true;//20220502 maythu add
                    txtLogotitle.Text = DDL_kyoutenLogo.SelectedItem.Value;//20220502 maythu add
                }
                else if (radStrval == "4")
                {
                    btnKyotenLogo1.Visible = false;
                    btnKyotenLogo2.Visible = false;
                    btnKyotenLogo3.Visible = false;
                    btnKyotenLogo4.Visible = true;
                    btnKyotenLogo5.Visible = false;

                    //20220502 maythu delete start
                    //txtlogoName1.Attributes.Add("readonly", "true");
                    //txtlogoName2.Attributes.Add("readonly", "true");
                    //txtlogoName3.Attributes.Add("readonly", "true");
                    //txtlogoName4.Attributes.Remove("readonly");
                    //txtlogoName5.Attributes.Add("readonly", "true");

                    //if (frbt == true)
                    //{
                    //    txtlogoName4.Focus();
                    //}
                    //rbtnLogo4.Checked = true;
                    //20220502 maythu delete end

                    DDL_kyoutenLogo.ClearSelection();//20220502 maythu add
                    DDL_kyoutenLogo.Items.FindByText("ロゴ4").Selected = true;//20220502 maythu add
                    txtLogotitle.Text = DDL_kyoutenLogo.SelectedItem.Value;//20220502 maythu add
                }
                else if (radStrval == "5")
                {
                    btnKyotenLogo1.Visible = false;
                    btnKyotenLogo2.Visible = false;
                    btnKyotenLogo3.Visible = false;
                    btnKyotenLogo4.Visible = false;
                    btnKyotenLogo5.Visible = true;

                    //20220502 maythu delete start
                    //txtlogoName1.Attributes.Add("readonly", "true");
                    //txtlogoName2.Attributes.Add("readonly", "true");
                    //txtlogoName3.Attributes.Add("readonly", "true");
                    //txtlogoName4.Attributes.Add("readonly", "true");
                    //txtlogoName5.Attributes.Remove("readonly");

                    //if (frbt == true)
                    //{
                    //    txtlogoName5.Focus();
                    //}
                    //rbtnLogo5.Checked = true;
                    //20220502 maythu delete end

                    DDL_kyoutenLogo.ClearSelection();//20220502 maythu add
                    DDL_kyoutenLogo.Items.FindByText("ロゴ5").Selected = true;//20220502 maythu add
                    txtLogotitle.Text = DDL_kyoutenLogo.SelectedItem.Value;//20220502 maythu add
                }

                txtLogotitle.Attributes.Remove("readonly");//20220503 maythu add
            }
            else
            {

                if (radStrval == "1" || radStrval == "")
                {
                    btnKyotenLogo1.Visible = true;
                    btnKyotenLogo2.Visible = false;
                    btnKyotenLogo3.Visible = false;
                    btnKyotenLogo4.Visible = false;
                    btnKyotenLogo5.Visible = false;

                    //rbtnLogo1.Checked = true;//20220502 maythu delete

                    DDL_kyoutenLogo.ClearSelection();//20220502 maythu add
                    DDL_kyoutenLogo.Items.FindByText("ロゴ1").Selected = true;//20220502 maythu add
                    txtLogotitle.Text = DDL_kyoutenLogo.SelectedItem.Value;//20220502 maythu add
                }
                else if (radStrval == "2")
                {
                    btnKyotenLogo1.Visible = false;
                    btnKyotenLogo2.Visible = true;
                    btnKyotenLogo3.Visible = false;
                    btnKyotenLogo4.Visible = false;
                    btnKyotenLogo5.Visible = false;

                    //rbtnLogo2.Checked = true;//20220502 maythu delete

                    DDL_kyoutenLogo.ClearSelection();//20220502 maythu add
                    DDL_kyoutenLogo.Items.FindByText("ロゴ2").Selected = true;//20220502 maythu add
                    txtLogotitle.Text = DDL_kyoutenLogo.SelectedItem.Value;//20220502 maythu add
                }
                else if (radStrval == "3")
                {
                    btnKyotenLogo1.Visible = false;
                    btnKyotenLogo2.Visible = false;
                    btnKyotenLogo3.Visible = true;
                    btnKyotenLogo4.Visible = false;
                    btnKyotenLogo5.Visible = false;

                    //rbtnLogo3.Checked = true;//20220502 maythu delete

                    DDL_kyoutenLogo.ClearSelection();//20220502 maythu add
                    DDL_kyoutenLogo.Items.FindByText("ロゴ3").Selected = true;//20220502 maythu add
                    txtLogotitle.Text = DDL_kyoutenLogo.SelectedItem.Value;//20220502 maythu add
                }
                else if (radStrval == "4")
                {
                    btnKyotenLogo1.Visible = false;
                    btnKyotenLogo2.Visible = false;
                    btnKyotenLogo3.Visible = false;
                    btnKyotenLogo4.Visible = true;
                    btnKyotenLogo5.Visible = false;

                    //rbtnLogo4.Checked = true;//20220502 maythu delete

                    DDL_kyoutenLogo.ClearSelection();//20220502 maythu add
                    DDL_kyoutenLogo.Items.FindByText("ロゴ4").Selected = true;//20220502 maythu add
                    txtLogotitle.Text = DDL_kyoutenLogo.SelectedItem.Value;//20220502 maythu add
                }
                else if (radStrval == "5")
                {
                    btnKyotenLogo1.Visible = false;
                    btnKyotenLogo2.Visible = false;
                    btnKyotenLogo3.Visible = false;
                    btnKyotenLogo4.Visible = false;
                    btnKyotenLogo5.Visible = true;

                    //rbtnLogo5.Checked = true;//20220502 maythu delete

                    DDL_kyoutenLogo.ClearSelection();//20220502 maythu add
                    DDL_kyoutenLogo.Items.FindByText("ロゴ5").Selected = true;//20220502 maythu add
                    txtLogotitle.Text = DDL_kyoutenLogo.SelectedItem.Value;//20220502 maythu add
                }

                //20220502 maythu delete start
                //txtlogoName1.Attributes.Add("readonly", "true");
                //txtlogoName2.Attributes.Add("readonly", "true");
                //txtlogoName3.Attributes.Add("readonly", "true");
                //txtlogoName4.Attributes.Add("readonly", "true");
                //txtlogoName5.Attributes.Add("readonly", "true");
                //20220502 maythu delete end

                String id = Request.QueryString["id"];
                DataTable dt_db = JC01Login_Class.Get_DB(id);
                String db = dt_db.Rows[0]["db"].ToString();
                String mail = dt_db.Rows[0]["mail"].ToString();
                ConstantVal.DB_NAME = db;

                DataTable kyoutenDt = ktVal.GetHonsyaKyotenLogo();

                //20220502 maythu delete start
                //txtlogoName1.Text = kyoutenDt.Rows[0]["sIMAGETitle1"].ToString();
                //txtlogoName2.Text = kyoutenDt.Rows[0]["sIMAGETitle2"].ToString();
                //txtlogoName3.Text = kyoutenDt.Rows[0]["sIMAGETitle3"].ToString();
                //txtlogoName4.Text = kyoutenDt.Rows[0]["sIMAGETitle4"].ToString();
                //txtlogoName5.Text = kyoutenDt.Rows[0]["sIMAGETitle5"].ToString();

                //txtlogoName1.CssClass = "logotextcss";
                //txtlogoName2.CssClass = "logotextcss";
                //txtlogoName3.CssClass = "logotextcss";
                //txtlogoName4.CssClass = "logotextcss";
                //txtlogoName5.CssClass = "logotextcss";
                //20220502 maythu delete end

            }
            //updRadio.Update();//20220502 maythu delete
            updImg.Update();
            updCombobox.Update();//20220502 maythu add
            updLogotitle.Update();//20220502 maythu add

            //画像の設定
            btnKyotenLogo1.ImageUrl = imgbtn1.ImageUrl;
            btnKyotenLogo2.ImageUrl = imgbtn2.ImageUrl;
            btnKyotenLogo3.ImageUrl = imgbtn3.ImageUrl;
            btnKyotenLogo4.ImageUrl = imgbtn4.ImageUrl;
            btnKyotenLogo5.ImageUrl = imgbtn5.ImageUrl;

            //ロゴタイトル有効/無効化にする          
            //updateImage(radStrval);
            updImageError.Update();
        }
        #endregion

        #region ロゴ名 maxlength by byte 全角7 文字 半角 14文字
        protected void logoName1txtChanged(object sender, EventArgs e)
        {
            //20220502 maythu delete start
            //var textByteCount = Encoding.Default.GetByteCount(txtlogoName1.Text);
            //if (textByteCount > 14)
            //{
            //    string txtval = txtlogoName1.Text;
            //    while (Encoding.Default.GetByteCount(txtval) > 14)
            //    {
            //        txtval = txtval.Substring(0, txtval.Length - 1);
            //    }
            //    txtlogoName1.Text = txtval;
            //}
            //20220502 maythu delete end
        }
        #endregion

        #region ロゴ名 maxlength by byte 全角7 文字 半角 14文字
        protected void logoName2txtChanged(object sender, EventArgs e)
        {
            //20220502 maythu delete start
            //var textByteCount = Encoding.Default.GetByteCount(txtlogoName2.Text);
            //if (textByteCount > 14)
            //{
            //    string txtval = txtlogoName2.Text;
            //    while (Encoding.Default.GetByteCount(txtval) > 14)
            //    {
            //        txtval = txtval.Substring(0, txtval.Length - 1);
            //    }
            //    txtlogoName2.Text = txtval;
            //}
            //20220502 maythu delete end
        }
        #endregion

        #region ロゴ名 maxlength by byte 全角7 文字 半角 14文字
        protected void logoName3txtChanged(object sender, EventArgs e)
        {
            //20220502 maythu delete start
            //var textByteCount = Encoding.Default.GetByteCount(txtlogoName3.Text);
            //if (textByteCount > 14)
            //{
            //    string txtval = txtlogoName3.Text;
            //    while (Encoding.Default.GetByteCount(txtval) > 14)
            //    {
            //        txtval = txtval.Substring(0, txtval.Length - 1);
            //    }
            //    txtlogoName3.Text = txtval;
            //}
            //20220502 maythu delete end
        }
        #endregion

        #region ロゴ名 maxlength by byte 全角7 文字 半角 14文字
        protected void logoName4txtChanged(object sender, EventArgs e)
        {
            //20220502 maythu delete start
            //var textByteCount = Encoding.Default.GetByteCount(txtlogoName4.Text);
            //if (textByteCount > 14)
            //{
            //    string txtval = txtlogoName4.Text;
            //    while (Encoding.Default.GetByteCount(txtval) > 14)
            //    {
            //        txtval = txtval.Substring(0, txtval.Length - 1);
            //    }
            //    txtlogoName4.Text = txtval;
            //}
            //20220502 maythu delete end
        }
        #endregion

        #region ロゴ名 maxlength by byte 全角7 文字 半角 14文字
        protected void logoName5txtChanged(object sender, EventArgs e)
        {
            //20220502 maythu delete start
            //var textByteCount = Encoding.Default.GetByteCount(txtlogoName5.Text);
            //if (textByteCount > 14)
            //{
            //    string txtval = txtlogoName5.Text;
            //    while (Encoding.Default.GetByteCount(txtval) > 14)
            //    {
            //        txtval = txtval.Substring(0, txtval.Length - 1);
            //    }
            //    txtlogoName5.Text = txtval;
            //}
            //20220502 maythu delete end
        }
        #endregion

        #region "Preview Session" //20220210 MyatNoe Added
        public void previewSession()
        {
            Session.Remove("rdoLogo1");
            Session.Remove("rdoLogo2");
            Session.Remove("rdoLogo3");
            Session.Remove("rdoLogo4");
            Session.Remove("rdoLogo5");

            Session.Remove("rdoLogoByte1");
            Session.Remove("rdoLogoByte2");
            Session.Remove("rdoLogoByte3");
            Session.Remove("rdoLogoByte4");
            Session.Remove("rdoLogoByte5");

            Session.Remove("PDFMemoryStream_kyoten");
        }
        #endregion

        #region DDLKyotenLogo 20220502 maythu add 
        public void DDLKyotenLogo()
        {
            String id = Request.QueryString["id"];
            DataTable dt_db = JC01Login_Class.Get_DB(id);
            String db = dt_db.Rows[0]["db"].ToString();
            String mail = dt_db.Rows[0]["mail"].ToString();
            ConstantVal.DB_NAME = db;

            DataTable kyoutenLogoDt = ktVal.GetHonsyaKyotenLogo();
            DDL_kyoutenLogo.Items.Clear();
            //DDL_kyoutenLogo.Items.Add(new ListItem("", ""));
            DDL_kyoutenLogo.Items.Add(new ListItem("ロゴ1", kyoutenLogoDt.Rows[0]["sIMAGETitle1"].ToString()));
            DDL_kyoutenLogo.Items.Add(new ListItem("ロゴ2", kyoutenLogoDt.Rows[0]["sIMAGETitle2"].ToString()));
            DDL_kyoutenLogo.Items.Add(new ListItem("ロゴ3", kyoutenLogoDt.Rows[0]["sIMAGETitle3"].ToString()));
            DDL_kyoutenLogo.Items.Add(new ListItem("ロゴ4", kyoutenLogoDt.Rows[0]["sIMAGETitle4"].ToString()));
            DDL_kyoutenLogo.Items.Add(new ListItem("ロゴ5", kyoutenLogoDt.Rows[0]["sIMAGETitle5"].ToString()));
            DDL_kyoutenLogo.Items[0].Selected = true;
            txtLogotitle.Text = "";
        }
        #endregion

        #region btnShokimodoru_Click 20220502 maythu add
        protected void btnShokimodoru_Click(object sender, EventArgs e)
        {
            display_Text();
        }
        #endregion

        #region display_Text() 20220502 maythu add
        private void display_Text()
        {
            string displayText = string.Empty;
            if (!string.IsNullOrEmpty(txtKaishaMei.Text.Trim()))
            {
                displayText = txtKaishaMei.Text.Trim();
                displayText += "\r\n";
            }
            if (!string.IsNullOrEmpty(txtYubinBangou.Text.Trim()))
            {
                if (txtYubinBangou.Text.Length > 3)
                {
                    displayText += "〒" + txtYubinBangou.Text.Substring(0, 3) + "-" + txtYubinBangou.Text.Substring(3, txtYubinBangou.Text.Length - 3);
                }
                else
                {
                    displayText += "〒" + txtYubinBangou.Text;
                }
                displayText += "\r\n";
            }
            if (!string.IsNullOrEmpty(txtJusho1.Text.Trim()))
            {
                displayText += txtJusho1.Text.Trim();
                displayText += "\r\n";
            }
            if (!string.IsNullOrEmpty(txtjusho2.Text.Trim()))
            {
                displayText += txtjusho2.Text.Trim();
                displayText += "\r\n";
            }
            if (!string.IsNullOrEmpty(txtdanwa.Text.Trim()) || !string.IsNullOrEmpty(txtfax.Text.Trim()))
            {
                displayText += "TEL:" + txtdanwa.Text.Trim() + " FAX:" + txtfax.Text.Trim();
                displayText += "\r\n";
            }
            if (!string.IsNullOrEmpty(txtURL.Text.Trim()))
            {
                displayText += "URL:" + txtURL.Text.Trim();
            }

            txtSetsuMei.Text = displayText;
        }
        #endregion

        #region DDL_kyoutenLogo_SelectedIndexChanged 20220502 maythu add
        protected void DDL_kyoutenLogo_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtLogotitle.Text = DDL_kyoutenLogo.SelectedValue;
            if (DDL_kyoutenLogo.SelectedIndex == 0)
            {
                string radioVal = "1";
                kyotenId = txt_cCo.Text;
                frbt = true;
                setItemProperty(radioVal);
            }
            if (DDL_kyoutenLogo.SelectedIndex == 1)
            {
                string radioVal = "2";
                kyotenId = txt_cCo.Text;
                frbt = true;
                setItemProperty(radioVal);
            }
            if (DDL_kyoutenLogo.SelectedIndex == 2)
            {
                string radioVal = "3";
                kyotenId = txt_cCo.Text;
                frbt = true;
                setItemProperty(radioVal);
            }
            if (DDL_kyoutenLogo.SelectedIndex == 3)
            {
                string radioVal = "4";
                kyotenId = txt_cCo.Text;
                frbt = true;
                setItemProperty(radioVal);
            }
            if (DDL_kyoutenLogo.SelectedIndex == 4)
            {
                string radioVal = "5";
                kyotenId = txt_cCo.Text;
                frbt = true;
                setItemProperty(radioVal);
            }
        }
        #endregion

        #region txtLogotitle_TextChanged 20220502 maythu add 
        protected void txtLogotitle_TextChanged(object sender, EventArgs e)
        {
            var textByteCount = Encoding.Default.GetByteCount(txtLogotitle.Text);
            if (textByteCount > 14)
            {
                string txtval = txtLogotitle.Text;
                while (Encoding.Default.GetByteCount(txtval) > 14)
                {
                    txtval = txtval.Substring(0, txtval.Length - 1);
                }
                txtLogotitle.Text = txtval;
            }
            //20220504 maythu add start
            if (Session["IdKyouten"].ToString() == "01")
            {
                if (DDL_kyoutenLogo.Items.Count > 0)
                {
                    if (DDL_kyoutenLogo.SelectedIndex == 0)
                    {
                        DDL_kyoutenLogo.Items[0].Value = txtLogotitle.Text;
                    }
                    else if (DDL_kyoutenLogo.SelectedIndex == 1)
                    {
                        DDL_kyoutenLogo.Items[1].Value = txtLogotitle.Text;
                    }
                    else if (DDL_kyoutenLogo.SelectedIndex == 2)
                    {
                        DDL_kyoutenLogo.Items[2].Value = txtLogotitle.Text;
                    }
                    else if (DDL_kyoutenLogo.SelectedIndex == 3)
                    {
                        DDL_kyoutenLogo.Items[3].Value = txtLogotitle.Text;
                    }
                    else if (DDL_kyoutenLogo.SelectedIndex == 4)
                    {
                        DDL_kyoutenLogo.Items[4].Value = txtLogotitle.Text;
                    }
                }
                updCombobox.Update();
            }
            //20220504 maythu add end
        }
        #endregion

    }
}