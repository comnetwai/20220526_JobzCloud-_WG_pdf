using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;
using Service;

namespace jobzcolud.WebFront
{
    public partial class JC29Jishajouhousettei : System.Web.UI.Page
    {
        JC29Jishajouhousettei_Class mjVal = new JC29Jishajouhousettei_Class();
        string btnNumber;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(Request.QueryString["id"]))
            {
                if (!IsPostBack)
                {
                    JC99NavBar navbar_Master = (JC99NavBar)this.Master;
                    navbar_Master.lnkBtnSetting.Style.Add(" background-color", "rgba(46,117,182)");
                    navbar_Master.navbar2.Visible = false;
                    
                    selectInfo();
                }
                Session.Timeout = 480;
            }
            else
            {
                Response.Redirect("JC01Login.aspx");
            }
        }
        protected void selectInfo()
        {
            string info_query = " SELECT sBIKOUTitle1,sBIKOUTitle2,sBIKOUTitle3,sBIKOUTitle4,sBIKOUTitle5," +
                        "sBIKOU,sBIKOU2,sBIKOU3,sBIKOU4,sBIKOU5," +
                        "fgentankatanka,ftansuushori," +
                        "sINVOICE,sSEIKYUUSHO,sKENSEIKYUUSYO,fBIKOU " +
                        "FROM m_j_info WHERE cCO='01';";

            DataTable dt_info = new DataTable();

            String id = Request.QueryString["id"];
            DataTable dt_db = JC01Login_Class.Get_DB(id);
            String db = dt_db.Rows[0]["db"].ToString();
            String mail = dt_db.Rows[0]["mail"].ToString();
            ConstantVal.DB_NAME = db;

            mjVal.loginId = mail;
            dt_info = mjVal.HyoujiData(info_query);

            foreach (DataRow dr in dt_info.Rows)
            {
                #region sBIKOUTitle1
                if (dr["sBIKOUTitle1"].ToString() != null && dr["sBIKOUTitle1"].ToString() != "")
                {
                    Session["txtBikouTitle1"] = dr["sBIKOUTitle1"].ToString();
                    txtBikouTitle1.Text = dr["sBIKOUTitle1"].ToString();
                }
                else
                {
                    Session["txtBikouTitle1"] = null;
                    txtBikouTitle1.Text = "";
                }
                if (dr["sBIKOU"].ToString() != null && dr["sBIKOU"].ToString() != "")
                {
                    Session["txtBikou1"] = dr["sBIKOU"].ToString();
                    txtBikou1.Text = dr["sBIKOU"].ToString();
                }
                else
                {
                    Session["txtBikou1"] = null;
                    txtBikou1.Text = "";
                }//for 1
                #endregion

                #region sBIKOUTitle2
                if (dr["sBIKOUTitle2"].ToString() != null && dr["sBIKOUTitle2"].ToString() != "")
                {
                    Session["txtBikouTitle2"] = dr["sBIKOUTitle2"].ToString();
                    txtBikouTitle2.Text = dr["sBIKOUTitle2"].ToString();
                }
                else
                {
                    Session["txtBikouTitle2"] = null;
                    txtBikouTitle2.Text = "";
                }
                if (dr["sBIKOU2"].ToString() != null && dr["sBIKOU2"].ToString() != "")
                {
                    Session["txtBikou2"] = dr["sBIKOU2"].ToString();
                    txtBikou2.Text = dr["sBIKOU2"].ToString();
                }
                else
                {
                    Session["txtBikou2"] = null;
                    txtBikou2.Text = dr["sBIKOU2"].ToString();
                }//for 2
                #endregion

                #region sBIKOUTitle3
                if (dr["sBIKOUTitle3"].ToString() != null && dr["sBIKOUTitle3"].ToString() != "")
                {
                    Session["txtBikouTitle3"] = dr["sBIKOUTitle3"].ToString();
                    txtBikouTitle3.Text = dr["sBIKOUTitle3"].ToString();
                }
                else
                {
                    Session["txtBikouTitle3"] = null;
                    txtBikouTitle3.Text = "";
                }
                if (dr["sBIKOU3"].ToString() != null && dr["sBIKOU3"].ToString() != "")
                {
                    Session["txtBikou3"] = dr["sBIKOU3"].ToString();
                    txtBikou3.Text = dr["sBIKOU3"].ToString();
                }
                else
                {
                    Session["txtBikou3"] = null;
                    txtBikou3.Text = "";
                }//for 3
                #endregion

                #region sBIKOUTitle4
                if (dr["sBIKOUTitle4"].ToString() != null && dr["sBIKOUTitle4"].ToString() != "")
                {
                    Session["txtBikouTitle4"] = dr["sBIKOUTitle4"].ToString();
                    txtBikouTitle4.Text = dr["sBIKOUTitle4"].ToString();
                }
                else
                {
                    Session["txtBikouTitle4"] = null;
                    txtBikouTitle4.Text = "";
                }
                if (dr["sBIKOU4"].ToString() != null && dr["sBIKOU4"].ToString() != "")
                {
                    Session["txtBikou4"] = dr["sBIKOU4"].ToString();
                    txtBikou4.Text = dr["sBIKOU4"].ToString();
                }
                else
                {
                    Session["txtBikou4"] = null;
                    txtBikou4.Text = "";
                }//for 4
                #endregion

                #region sBIKOUTitle5
                if (dr["sBIKOUTitle5"].ToString() != null && dr["sBIKOUTitle5"].ToString() != "")
                {
                    Session["txtBikouTitle5"] = dr["sBIKOUTitle5"].ToString();
                    txtBikouTitle5.Text = dr["sBIKOUTitle5"].ToString();
                }
                else
                {
                    Session["txtBikouTitle5"] = null;
                    txtBikouTitle5.Text = "";
                }
                if (dr["sBIKOU5"].ToString() != null && dr["sBIKOU5"].ToString() != "")
                {
                    Session["txtBikou5"] = dr["sBIKOU5"].ToString();
                    txtBikou5.Text = dr["sBIKOU5"].ToString();
                }
                else
                {
                    Session["txtBikou5"] = null;
                    txtBikou5.Text = "";
                }//for 5
                #endregion

                #region fgentankatanka
                if (dr["fgentankatanka"].ToString() == "0")
                {
                    MitsumorikeisanhouhouBackgroundStyle("tankagenka");
                }
                else
                {
                    MitsumorikeisanhouhouBackgroundStyle("sougenka");
                }
                txtMitsumori.Text = dr["fgentankatanka"].ToString();
                #endregion

                #region ftansuushori
                if (dr["ftansuushori"].ToString() == "0")
                {
                    TansuushoriBackgroundStyle("truncate");
                }
                else if (dr["ftansuushori"].ToString() == "1")
                {
                    TansuushoriBackgroundStyle("rounding");
                }
                else
                {
                    TansuushoriBackgroundStyle("roundup");
                }
                txtTansuushori.Text = dr["ftansuushori"].ToString();
                #endregion

                #region sINVOICE
                if (dr["sINVOICE"].ToString() != null)
                {
                    txtNouhinsho.Text = dr["sINVOICE"].ToString();
                }
                else
                {
                    txtNouhinsho.Text = "";
                }
                #endregion

                #region sSEIKYUUSHO
                if (dr["sSEIKYUUSHO"].ToString() != null)
                {
                    txtSeikyuusho.Text = dr["sSEIKYUUSHO"].ToString();
                }
                else
                {
                    txtSeikyuusho.Text = "";
                }
                #endregion

                #region sKENSEIKYUUSYO
                if (dr["sKENSEIKYUUSYO"].ToString() != null)
                {
                    txtNouhinshouken.Text = dr["sKENSEIKYUUSYO"].ToString();
                }
                else
                {
                    txtNouhinshouken.Text = "";
                }
                #endregion

                #region fBIKOU
                btnNumber = dr["fBIKOU"].ToString();
                if (btnNumber == "")
                {
                    btnNumber = "1";
                }
                #endregion
            }

            if (btnNumber == "1")
            {
                IR1.Style.Add(" background-color", "lightGray");
                IR2.Style.Add(" background-color", "white");
                IR3.Style.Add(" background-color", "white");
                IR4.Style.Add(" background-color", "white");
                IR5.Style.Add(" background-color", "white");

                div_txt1.Attributes.Add("class", "");
                div_txt2.Attributes.Add("class", "d-none");
                div_txt3.Attributes.Add("class", "d-none");
                div_txt4.Attributes.Add("class", "d-none");
                div_txt5.Attributes.Add("class", "d-none");
                Session["btnLabel"] = "btnSeikyuuBikou1";
            }
            else if (btnNumber == "2")
            {
                IR2.Style.Add(" background-color", "lightGray");
                IR1.Style.Add(" background-color", "white");
                IR3.Style.Add(" background-color", "white");
                IR4.Style.Add(" background-color", "white");
                IR5.Style.Add(" background-color", "white");

                div_txt1.Attributes.Add("class", "d-none");
                div_txt2.Attributes.Add("class", "");
                div_txt3.Attributes.Add("class", "d-none");
                div_txt4.Attributes.Add("class", "d-none");
                div_txt5.Attributes.Add("class", "d-none");
                Session["btnLabel"] = "btnSeikyuuBikou2";
            }
            else if (btnNumber == "3")
            {
                IR3.Style.Add(" background-color", "lightGray");
                IR1.Style.Add(" background-color", "white");
                IR2.Style.Add(" background-color", "white");
                IR4.Style.Add(" background-color", "white");
                IR5.Style.Add(" background-color", "white");

                div_txt1.Attributes.Add("class", "d-none");
                div_txt2.Attributes.Add("class", "d-none");
                div_txt3.Attributes.Add("class", "");
                div_txt4.Attributes.Add("class", "d-none");
                div_txt5.Attributes.Add("class", "d-none");
                Session["btnLabel"] = "btnSeikyuuBikou3";
            }
            else if (btnNumber == "4")
            {
                IR4.Style.Add(" background-color", "lightGray");
                IR1.Style.Add(" background-color", "white");
                IR2.Style.Add(" background-color", "white");
                IR3.Style.Add(" background-color", "white");
                IR5.Style.Add(" background-color", "white");

                div_txt1.Attributes.Add("class", "d-none");
                div_txt2.Attributes.Add("class", "d-none");
                div_txt3.Attributes.Add("class", "d-none");
                div_txt4.Attributes.Add("class", "");
                div_txt5.Attributes.Add("class", "d-none");
                Session["btnLabel"] = "btnSeikyuuBikou4";
            }
            else if (btnNumber == "5")
            {
                IR5.Style.Add(" background-color", "lightGray");
                IR1.Style.Add(" background-color", "white");
                IR2.Style.Add(" background-color", "white");
                IR3.Style.Add(" background-color", "white");
                IR4.Style.Add(" background-color", "white");

                div_txt1.Attributes.Add("class", "d-none");
                div_txt2.Attributes.Add("class", "d-none");
                div_txt3.Attributes.Add("class", "d-none");
                div_txt4.Attributes.Add("class", "d-none");
                div_txt5.Attributes.Add("class", "");
                Session["btnLabel"] = "btnSeikyuuBikou5";
            }
            txt_btnNumber.Text = btnNumber;
        }

        #region "保存ボタン"

        /// <summary>
        /// 保存ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string cCoVal = FindCompanyCode();

            string txt_sBIKOUTitle1 = "";
            string txt_sBIKOUTitle2 = "";
            string txt_sBIKOUTitle3 = "";
            string txt_sBIKOUTitle4 = "";
            string txt_sBIKOUTitle5 = "";
            string btnNumber = txt_btnNumber.Text;

            if (txtBikouTitle1.Text != "")
            {
                if (txtBikouTitle1.Text.Contains("'") == true)
                {
                    txt_sBIKOUTitle1 = "'" + txtBikouTitle1.Text.Replace("'", @"\'") + "'";

                }
                else
                {
                    txt_sBIKOUTitle1 = "'" + txtBikouTitle1.Text + "'";
                }
                //txt_sBIKOUTitle1 ="'"+ txtBikouTitle1.Text + "'";
            }
            else
            {
                txt_sBIKOUTitle1 = "null";
            }

            if (txtBikouTitle2.Text != "")
            {
                if (txtBikouTitle2.Text.Contains("'") == true)
                {
                    txt_sBIKOUTitle2 = "'" + txtBikouTitle2.Text.Replace("'", @"\'") + "'";

                }
                else
                {
                    txt_sBIKOUTitle2 = "'" + txtBikouTitle2.Text + "'";
                }
                //txt_sBIKOUTitle2 = "'" + txtBikouTitle2.Text + "'";
            }
            else
            {
                txt_sBIKOUTitle2 = "null";
            }

            if (txtBikouTitle3.Text != "")
            {
                if (txtBikouTitle3.Text.Contains("'") == true)
                {
                    txt_sBIKOUTitle3 = "'" + txtBikouTitle3.Text.Replace("'", @"\'") + "'";

                }
                else
                {
                    txt_sBIKOUTitle3 = "'" + txtBikouTitle3.Text + "'";
                }
                //txt_sBIKOUTitle3 = "'" + txtBikouTitle3.Text + "'";
            }
            else
            {
                txt_sBIKOUTitle3 = "null";
            }

            if (txtBikouTitle4.Text != "")
            {
                if (txtBikouTitle4.Text.Contains("'") == true)
                {
                    txt_sBIKOUTitle4 = "'" + txtBikouTitle4.Text.Replace("'", @"\'") + "'";

                }
                else
                {
                    txt_sBIKOUTitle4 = "'" + txtBikouTitle4.Text + "'";
                }
                //txt_sBIKOUTitle4 = "'" + txtBikouTitle4.Text + "'";
            }
            else
            {
                txt_sBIKOUTitle4 = "null";
            }

            if (txtBikouTitle5.Text != "")
            {
                if (txtBikouTitle5.Text.Contains("'") == true)
                {
                    txt_sBIKOUTitle5 = "'" + txtBikouTitle5.Text.Replace("'", @"\'") + "'";

                }
                else
                {
                    txt_sBIKOUTitle5 = "'" + txtBikouTitle5.Text + "'";
                }
                //txt_sBIKOUTitle5 = "'" + txtBikouTitle5.Text + "'";
            }
            else
            {
                txt_sBIKOUTitle5 = "null";
            }
            string txt_sBIKOU1 = "";
            string txt_sBIKOU2 = "";
            string txt_sBIKOU3 = "";
            string txt_sBIKOU4 = "";
            string txt_sBIKOU5 = "";

            if (txtBikou1.Text != "")
            {
                if (txtBikou1.Text.Contains("'") == true)
                {
                    txt_sBIKOU1 = "'" + txtBikou1.Text.Replace("'", @"\'") + "'";

                }
                else
                {
                    txt_sBIKOU1 = "'" + txtBikou1.Text + "'";
                }
                //txt_sBIKOU1 = "'" + txtBikou1.Text + "'";
            }
            else
            {
                txt_sBIKOU1 = "null";
            }

            if (txtBikou2.Text != "")
            {
                if (txtBikou2.Text.Contains("'") == true)
                {
                    txt_sBIKOU2 = "'" + txtBikou2.Text.Replace("'", @"\'") + "'";

                }
                else
                {
                    txt_sBIKOU2 = "'" + txtBikou2.Text + "'";
                }
                //txt_sBIKOU2 = "'" + txtBikou2.Text + "'";
            }
            else
            {
                txt_sBIKOU2 = "null";
            }

            if (txtBikou3.Text != "")
            {
                if (txtBikou3.Text.Contains("'") == true)
                {
                    txt_sBIKOU3 = "'" + txtBikou3.Text.Replace("'", @"\'") + "'";

                }
                else
                {
                    txt_sBIKOU3 = "'" + txtBikou3.Text + "'";
                }
                //txt_sBIKOU3 = "'" + txtBikou3.Text + "'";
            }
            else
            {
                txt_sBIKOU3 = "null";
            }

            if (txtBikou4.Text != "")
            {
                if (txtBikou4.Text.Contains("'") == true)
                {
                    txt_sBIKOU4 = "'" + txtBikou4.Text.Replace("'", @"\'") + "'";

                }
                else
                {
                    txt_sBIKOU4 = "'" + txtBikou4.Text + "'";
                }
                //txt_sBIKOU4 = "'" + txtBikou4.Text + "'";
            }
            else
            {
                txt_sBIKOU4 = "null";
            }

            if (txtBikou5.Text != "")
            {
                if (txtBikou5.Text.Contains("'") == true)
                {
                    txt_sBIKOU5 = "'" + txtBikou5.Text.Replace("'", @"\'") + "'";

                }
                else
                {
                    txt_sBIKOU5 = "'" + txtBikou5.Text + "'";
                }
                //txt_sBIKOU5 = "'" + txtBikou5.Text + "'";
            }
            else
            {
                txt_sBIKOU5 = "null";
            }
            string txt_fgentankatanka = txtMitsumori.Text;
            string txt_ftanshuushori = txtTansuushori.Text;

            string txt_sINVOICE = "";
            if (txtNouhinsho.Text != "")
            {
                if (txtNouhinsho.Text.Contains("'") == true)
                {
                    txt_sINVOICE = "'" + txtNouhinsho.Text.Replace("'", @"\'") + "'";

                }
                else
                {
                    txt_sINVOICE = "'" + txtNouhinsho.Text + "'";
                }
                //txt_sINVOICE = "'" + txtNouhinsho.Text + "'";
            }
            else
            {
                txt_sINVOICE = "null";
            }
            string txt_sSEIKYUUSHO = "";
            if (txtSeikyuusho.Text != "")
            {
                if (txtSeikyuusho.Text.Contains("'") == true)
                {
                    txt_sSEIKYUUSHO = "'" + txtSeikyuusho.Text.Replace("'", @"\'") + "'";

                }
                else
                {
                    txt_sSEIKYUUSHO = "'" + txtSeikyuusho.Text + "'";
                }
                //txt_sSEIKYUUSHO = "'" + txtSeikyuusho.Text + "'";
            }
            else
            {
                txt_sSEIKYUUSHO = "null";
            }
            string txt_sKENSEIKYUUSYO = "";
            if (txtNouhinshouken.Text != "")
            {
                if (txtNouhinshouken.Text.Contains("'") == true)
                {
                    txt_sKENSEIKYUUSYO = "'" + txtNouhinshouken.Text.Replace("'", @"\'") + "'";

                }
                else
                {
                    txt_sKENSEIKYUUSYO = "'" + txtNouhinshouken.Text + "'";
                }
                //txt_sKENSEIKYUUSYO = "'" + txtNouhinshouken.Text + "'";
            }
            else
            {
                txt_sKENSEIKYUUSYO = "null";
            }

            try
            {
                string infoSave_query = "UPDATE m_j_info SET " +
                "sBIKOUTitle1=" + txt_sBIKOUTitle1 + "," +
                "sBIKOUTitle2=" + txt_sBIKOUTitle2 + "," +
                "sBIKOUTitle3=" + txt_sBIKOUTitle3 + "," +
                "sBIKOUTitle4=" + txt_sBIKOUTitle4 + "," +
                "sBIKOUTitle5=" + txt_sBIKOUTitle5 + "," +
                "sBIKOU=" + txt_sBIKOU1 + "," +
                "sBIKOU2=" + txt_sBIKOU2 + "," +
                "sBIKOU3=" + txt_sBIKOU3 + "," +
                "sBIKOU4=" + txt_sBIKOU4 + "," +
                "sBIKOU5=" + txt_sBIKOU5 + "," +
                "fgentankatanka='" + txt_fgentankatanka + "'," +
                "ftansuushori='" + txt_ftanshuushori + "'," +
                "sINVOICE=" + txt_sINVOICE + "," +
                "sSEIKYUUSHO=" + txt_sSEIKYUUSHO + "," +
                "sKENSEIKYUUSYO=" + txt_sKENSEIKYUUSYO + "," +
                "fBIKOU=" + btnNumber + " " +
                "WHERE cCO='01';";//20220119
                                  //"sKENSEIKYUUSYO=" + txt_sKENSEIKYUUSYO + " WHERE cCO='" + cCoVal + "';";
                                  //"WHERE cCO='" + cCoVal + "'";

                String id = Request.QueryString["id"];
                DataTable dt_db = JC01Login_Class.Get_DB(id);
                String db = dt_db.Rows[0]["db"].ToString();
                String mail = dt_db.Rows[0]["mail"].ToString();
                ConstantVal.DB_NAME = db;

                mjVal.loginId = mail;
                mjVal.infoUpdate(infoSave_query);
                selectInfo();

            }
            catch (Exception ex)
            {

            }

            #region comment
            //if (btnNumber == "1")
            //{
            //    if (txtBikouTitle1.Text == "" && txtBikou1.Text == "")
            //    {
            //        canSave = true;
            //    }
            //    else
            //    {
            //        canSave = false;
            //        if (txtBikouTitle1.Text != "" && txtBikou1.Text == "")
            //        {
            //            title1_required.Attributes.Add("class", "d-none");
            //            bikou1_required.Attributes.Add("class", "d-block");
            //        }
            //        else
            //        {
            //            title1_required.Attributes.Add("class", "d-block");
            //            bikou1_required.Attributes.Add("class", "d-none");
            //        }
            //    }
            //}
            //else if (btnNumber == "2")
            //{
            //    if (txtBikouTitle2.Text == "" && txtBikou2.Text == "")
            //    {
            //        canSave = true;
            //    }
            //    else
            //    {
            //        canSave = false;
            //        if (txtBikouTitle2.Text != "" && txtBikou2.Text == "")
            //        {
            //            title2_required.Attributes.Add("class", "d-none");
            //            bikou2_required.Attributes.Add("class", "d-block");
            //        }
            //        else
            //        {
            //            title2_required.Attributes.Add("class", "d-block");
            //            bikou2_required.Attributes.Add("class", "d-none");
            //        }
            //    }
            //}
            //else if (btnNumber == "3")
            //{
            //    if (txtBikouTitle3.Text == "" && txtBikou3.Text == "")
            //    {
            //        canSave = true;
            //    }
            //    else
            //    {
            //        canSave = false;
            //        if (txtBikouTitle3.Text != "" && txtBikou3.Text == "")
            //        {
            //            title3_required.Attributes.Add("class", "d-none");
            //            bikou3_required.Attributes.Add("class", "d-block");
            //        }
            //        else
            //        {
            //            title3_required.Attributes.Add("class", "d-block");
            //            bikou3_required.Attributes.Add("class", "d-none");
            //        }
            //    }
            //}
            //else if (btnNumber == "4")
            //{
            //    if (txtBikouTitle4.Text == "" && txtBikou4.Text == "")
            //    {
            //        canSave = true;
            //    }
            //    else
            //    {
            //        canSave = false;
            //        if (txtBikouTitle4.Text != "" && txtBikou4.Text == "")
            //        {
            //            title4_required.Attributes.Add("class", "d-none");
            //            bikou4_required.Attributes.Add("class", "d-block");
            //        }
            //        else
            //        {
            //            title4_required.Attributes.Add("class", "d-block");
            //            bikou4_required.Attributes.Add("class", "d-none");
            //        }
            //    }
            //}
            //else if (btnNumber == "5")
            //{
            //    if (txtBikouTitle5.Text == "" && txtBikou5.Text == "")
            //    {
            //        canSave = true;
            //    }
            //    else
            //    {
            //        canSave = false;
            //        if (txtBikouTitle5.Text != "" && txtBikou5.Text == "")
            //        {
            //            title5_required.Attributes.Add("class", "d-none");
            //            bikou5_required.Attributes.Add("class", "d-block");
            //        }
            //        else
            //        {
            //            title5_required.Attributes.Add("class", "d-block");
            //            bikou5_required.Attributes.Add("class", "d-none");
            //        }
            //    }
            //}


            //string info_query = "SELECT sBIKOUTitle1,sBIKOUTitle2,sBIKOUTitle3,sBIKOUTitle4,sBIKOUTitle5," +
            //    "sBIKOU,sBIKOU2,sBIKOU3,sBIKOU4,sBIKOU5," +
            //    "sINVOICE,sSEIKYUUSHO,sKENSEIKYUUSYO " +
            //    "FROM m_j_info where sBIKOUTitle1 is not null or sBIKOUTitle2 is not null or sBIKOUTitle3 is not null or sBIKOUTitle4 is not null or sBIKOUTitle5 is not null or " +
            //    "sBIKOU is not null or sBIKOU2 is not null or sBIKOU3 is not null or sBIKOU4 is not null or sBIKOU5 is not null " +
            //    "or sINVOICE is not null or sSEIKYUUSHO is not null or sKENSEIKYUUSYO is not null ;;";

            //DataTable dt = new DataTable();
            //JC29Jishajouhousettei_Class jishaVal = new JC29Jishajouhousettei_Class();
            //jishaVal.loginId = Session["LoginId"].ToString();
            //dt = jishaVal.HyoujiData(info_query);

            //if (dt.Rows.Count > 0)
            //{
            //    btnEdit_Click(sender, e);
            //}
            //else
            //{


            //    //string sql = "INSERT INTO m_j_info(";
            //    ////sql += " cCo ";
            //    ////sql += ", sCo ";
            //    //sql += " sBIKOUTitle1 ";
            //    //sql += ", sBIKOUTitle2 ";
            //    //sql += ", sBIKOUTitle3 ";
            //    //sql += ", sBIKOUTitle4 ";
            //    //sql += ", sBIKOU ";
            //    //sql += ", sBIKOU2 ";
            //    //sql += ", sBIKOU3 ";
            //    //sql += ", sBIKOU4 ";
            //    //sql += ", fgentankatanka ";
            //    //sql += ", ftangetsuseikyuusyo ";
            //    //sql += ", sINVOICE ";
            //    //sql += ", sSEIKYUUSHO ";
            //    //sql += ", sKENSEIKYUUSYO ";
            //    //sql += ")VALUES  ";
            //    ////sql += " ( '" + cCoVal + "'";
            //    ////sql += " , '" + kaishamei + "'";
            //    //sql += " , '" + Session["txtBikouTitle1"].ToString() + "'";
            //    //sql += " , '" + Session["txtBikouTitle2"].ToString() + "'";
            //    //sql += " , '" + Session["txtBikouTitle3"].ToString() + "'";
            //    //sql += " , '" + Session["txtBikouTitle4"].ToString() + "' ";
            //    //sql += " , '" + Session["txtBikou1"].ToString() + "'";
            //    //sql += " , '" + Session["txtBikou2"].ToString() + "'";
            //    //sql += " , '" + Session["txtBikou3"].ToString() + "' ";
            //    //sql += " , '" + Session["txtBikou4"].ToString() + "' ";
            //    //sql += " , '" + Session["fgentankatanka"].ToString() + "'";
            //    //sql += " , '" + Session["ftanshuushori"].ToString() + "'";
            //    //sql += " , '" + txtNouhinsho.Text + "'";
            //    //sql += " , '" + txtSeikyuusho.Text + "'";
            //    //sql += " , '" + txtNouhinshouken.Text + "'";
            //    //sql += "  ) ";

            //    //JC29Jishajouhousettei_Class jsVal =new JC29Jishajouhousettei_Class();


            //    //btnSave_Click(sender, e);
            //}
            #endregion
        }
        #endregion

        protected string FindCompanyCode()
        {
            string ColVal = "";
            System.Data.DataTable dt = new System.Data.DataTable();
            string sqlStr = "SELECT cCo,sKYOTEN FROM m_j_info; ";

            String id = Request.QueryString["id"];
            DataTable dt_db = JC01Login_Class.Get_DB(id);
            String db = dt_db.Rows[0]["db"].ToString();
            String mail = dt_db.Rows[0]["mail"].ToString();
            ConstantVal.DB_NAME = db;

            mjVal.loginId = mail;
            dt = mjVal.HyoujiData(sqlStr);
            
            foreach (System.Data.DataRow dr in dt.Rows)
            {
                ColVal = dr["cCo"].ToString();
            }
            return ColVal;
        }

        protected string FindCompanyName()
        {
            string name = "";
            System.Data.DataTable dt = new System.Data.DataTable();

            String id = Request.QueryString["id"];
            DataTable dt_db = JC01Login_Class.Get_DB(id);
            String db = dt_db.Rows[0]["db"].ToString();
            String mail = dt_db.Rows[0]["mail"].ToString();
            ConstantVal.DB_NAME = db;

            string sqlStr = "SELECT sCo FROM m_j_info where cCO; ";
            mjVal.loginId = mail;
            dt = mjVal.HyoujiData(sqlStr);
           
            //finding the missing number 
            List<int> ListShain = new List<int>();
            foreach (System.Data.DataRow dr in dt.Rows)
            {
                name = dr.Table.Rows[0].ToString();
            }

            return name;
        }

        protected void MitsumorikeisanhouhouBackgroundStyle(string type)
        {
            if (type=="sougenka")
            {
                btnTotalCost.BackColor = System.Drawing.Color.LightGray;
                btnUnitPrice.BackColor = System.Drawing.Color.White;
            }
            else if (type == "tankagenka")
            {
                btnTotalCost.BackColor = System.Drawing.Color.White;
                btnUnitPrice.BackColor = System.Drawing.Color.LightGray;
            }
        }
       
        protected void TansuushoriBackgroundStyle(string type)
        {
            if (type == "truncate")
            {
                btnTruncate.BackColor = System.Drawing.Color.LightGray;
                btnRounding.BackColor = System.Drawing.Color.White;
                btnRoundUp.BackColor = System.Drawing.Color.White;
            }
            else if (type == "rounding")
            {
                btnTruncate.BackColor = System.Drawing.Color.White;
                btnRounding.BackColor = System.Drawing.Color.LightGray;
                btnRoundUp.BackColor = System.Drawing.Color.White;

            }
            else if (type == "roundup")
            {
                btnTruncate.BackColor = System.Drawing.Color.White;
                btnRounding.BackColor = System.Drawing.Color.White;
                btnRoundUp.BackColor = System.Drawing.Color.LightGray;

            }
        }
    }
}