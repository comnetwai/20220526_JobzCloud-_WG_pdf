//using AppCode;
//using Common.Utility;
using Common;
using System;
using System.Drawing;
using System.Globalization;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JobzRacoo.WebFront
{
    public partial class JCHidukeSelect : Page
    {
        #region "変数"

        /// <summary>
        /// 変数
        /// </summary>
        Label lblKaishiBi = new Label();
        public string strDate;
        DateTime dtYear;
        string YearCalendar = string.Empty;

        #endregion

        #region "画面固有パラメータ設定"

        /// <summary>
        /// 画面固有パラメータ設定
        /// </summary>
        //protected override void SetPgParam()
        //{
        //    base.GamenID = "J16HidukeSub";
        //    base.GamenName = "日付サブ画面";
        //    base.IsCheckSession = true;
        //}

        #endregion

        #region "ページの現在のスレッドのCultureおよびUICulture情報を初期化する処理"

        /// <summary>
        /// ページの現在のスレッドのCultureおよびUICulture情報を初期化する処理
        /// </summary>
        protected override void InitializeCulture()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("ja-JP", false);
        }

        #endregion

        #region"初期表示"

        /// <summary>
        /// 初期表示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //if ((HttpContext.Current.Request.UrlReferrer == null))
                //{
                //    Response.Redirect("~/Default.aspx");
                //}

                if (SessionUtility.GetSession("HOME") != null)
                {
                    hdnHome.Value = SessionUtility.GetSession("HOME").ToString();
                  
                }
                //btnKadouJikanSettei.Focus();
                //// 日付の色設定
                Calendar.DayStyle.ForeColor = Color.FromArgb(100, 100, 100);

                if (Session["CALENDARDATE"] != null && Session["YEAR"] != null)
                {
                    string strYear = Session["YEAR"].ToString();
                    string strDate = Session["CALENDARDATE"].ToString();
                    dtYear = DateTime.Parse(strDate);
                    YearCalendar = dtYear.ToString("yyyy");
                    // 選択したDateを設定する
                    if (YearCalendar.Equals(strYear))
                    {
                        Calendar.SelectedDate = Calendar.VisibleDate = Convert.ToDateTime(strDate);
                    }
                    else
                    {
                        Calendar.SelectedDate = Calendar.VisibleDate = Convert.ToDateTime(strYear + "/" + strDate);
                    }
                    // 選択したDateのBackgroundColorを設定する
                    Color SelectedDayClolor = Color.FromArgb(0, 162, 232);
                    Calendar.SelectedDayStyle.BackColor = SelectedDayClolor;
                    // 選択したDateをViewStateに設定する
                    lblKaishiBi.Text = Calendar.SelectedDate.ToString("yyyy/MM/dd");
                    ViewState["DATE"] = lblKaishiBi.Text;
                }
                else
                {
                    string strNowDate = System.DateTime.Now.ToString("yyyy/MM/dd");
                    Calendar.SelectedDate = Convert.ToDateTime(strNowDate);
                    // 選択したDateのBackgroundColorを設定する
                    Color SelectedDayClolor = Color.FromArgb(0, 162, 232);
                    Calendar.SelectedDayStyle.BackColor = SelectedDayClolor;
                    lblKaishiBi.Text = Calendar.SelectedDate.ToString("yyyy/MM/dd");
                    ViewState["DATE"] = lblKaishiBi.Text;
                }
                Session["CALENDARDATE"] = null;
                Session["YEAR"] = null;
            }
            Session.Timeout = 480;
        }

        #endregion

        #region "新規日時入力画面のカレンダーのSelectionChanged"

        /// <summary>
        /// 新規日時入力画面のカレンダーのSelectionChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Calendar_SelectionChanged(object sender, EventArgs e)
        {
            lblKaishiBi.Text = Calendar.SelectedDate.ToString("yyyy/MM/dd");
            ViewState["DATE"] = lblKaishiBi.Text;
            Calendar.TodayDayStyle.BackColor = System.Drawing.Color.White;
            Calendar.TodayDayStyle.ForeColor = System.Drawing.Color.DeepSkyBlue;
            // 選択したDateのBackgroundColorを設定する
            Color SelectedDayClolor = Color.FromArgb(0, 162, 232);
            Calendar.SelectedDayStyle.BackColor = SelectedDayClolor;
        }

        #endregion

        #region "新規日時入力画面の【ｘ】ボタンクリック処理"

        /// <summary>
        /// 新規日時入力画面の【ｘ】ボタンクリック処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSentakuHeaderClose_Click(object sender, EventArgs e)
        {
            //if (!TextUtility.IsNullOrEmpty(hdnHome.Value))
            //{
            //ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "parentButtonClick('btnCalendarClose');", true);
            //}
            //else
            //{
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "parentButtonClick('btnCalendarClose','"+hdnHome.Value+"');", true);
            //}

        }

        #endregion

        #region "新規日時入力画面の【設定】ボタンを押す処理"

        /// <summary>
        /// 新規日時入力画面の【設定】ボタンを押す処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnKadouJikanSettei_Click(object sender, EventArgs e)
        {
            string strHome = string.Empty;
            string strGetDate = (string)ViewState["DATE"];
            //SessionUtility.SetSession("DATE", strGetDate);
            Session["DATE"] = strGetDate;
            if (strGetDate == "" || strGetDate == null)
            {
                strDate = System.DateTime.Now.ToString("yyyy/MM/dd");
            }
            else
            {
                strDate = strGetDate;
            }
            //SessionUtility.SetSession("CALENDARDATETIME", strDate);

            Session["CALENDARDATETIME"] = strGetDate;
            ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "parentButtonClick('btnCalendarSettei','"+hdnHome.Value+"');", true);

            //if (!TextUtility.IsNullOrEmpty(hdnHome.Value))
            //{
            //    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "parentButtonClick('btnCalendarSettei');", true);
            //}
            //else
            //{
            //    ScriptManager.RegisterStartupScript(this, GetType(), "CallMyFunction", "parentButtonClick('btnCalendarSettei','Popup');", true);
            //}
        }

        #endregion

        #region "新規日時入力画面のカレンダーのDayRender"

        /// <summary>
        /// 新規日時入力画面のカレンダーのDayRender
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Calendar_DayRender(object sender, DayRenderEventArgs e)
        {
            if (e.Day.IsSelected)
            {
                e.Cell.Text = e.Day.DayNumberText;
                e.Cell.ToolTip = e.Day.Date.ToString("M月d日");
                e.Cell.Attributes.Add("ondblclick", "BtnClick('" + btnKadouJikanSettei.ClientID + "')");
            }
            else if (e.Day.IsWeekend)
            {
                DateTime date = Convert.ToDateTime(e.Day.Date.ToString());
                if (date.DayOfWeek == DayOfWeek.Saturday)
                {
                    e.Cell.BackColor = System.Drawing.ColorTranslator.FromHtml("#E9F6FE");
                }
                else if (date.DayOfWeek == DayOfWeek.Sunday)
                {
                    e.Cell.BackColor = System.Drawing.ColorTranslator.FromHtml("#FDF1F1");
                }
            }
        }

        #endregion
    }
}