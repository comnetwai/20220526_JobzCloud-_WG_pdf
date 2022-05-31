using Common.Info;
using System;
using System.Collections;
using System.Data;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;

namespace Common
{
    public class CommonUtility
    {
        #region "Thumbnail画像作成"

        /// <summary>
        /// Thumbnail画像作成
        /// </summary>
        /// <param name="imgBefortThumnailImage">画像</param>
        /// <returns></returns>
        public static string ChangeImage(System.Drawing.Image imgBefortThumnailImage)
        {
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
                    return "data:image/png;base64," + base64String;
                }
            }
        }

        #endregion

        #region "Thumbnail画像作成処理"

        /// <summary>
        /// Thumbnail画像作成処理
        /// </summary>
        /// <param name="imgBeforeThumbnail">Thumbnailとして作成前</param>
        /// <returns>Thumbnailとして作成後</returns>
        public static System.Drawing.Image ThumbnailImageCreate(System.Drawing.Image imgBeforeThumbnail)
        {
            int intThumbnailWidth = 0;
            int intThumnailHeight = 0;
            double dbChangePercent = 0;
            int intImageWidth = imgBeforeThumbnail.Width;
            int intImageHeight = imgBeforeThumbnail.Height;
            String strGamen = (String)SessionUtility.GetSession("GAMEN");
            if (String.Equals(strGamen, ConstantVal.HOME_STRING))
            {
                if (intImageWidth > intImageHeight)
                {
                    intThumbnailWidth = 70;
                    dbChangePercent = (100.0 * 70) / intImageWidth;
                    intThumnailHeight = (int)(Math.Round((dbChangePercent * intImageHeight) / 100));
                }
                else
                {
                    intThumnailHeight = 70;
                    dbChangePercent = (100.0 * 70) / intImageHeight;
                    intThumbnailWidth = (int)(Math.Round((dbChangePercent * intImageWidth) / 100));
                }
            }
            else if (String.Equals(strGamen, ConstantVal.MASTER_STRING))
            {
                if (intImageWidth > intImageHeight)
                {
                    intThumbnailWidth = 45;
                    dbChangePercent = (100.0 * 45) / intImageWidth;
                    intThumnailHeight = (int)(Math.Round((dbChangePercent * intImageHeight) / 100));
                }
                else
                {
                    intThumnailHeight = 45;
                    dbChangePercent = (100.0 * 45) / intImageHeight;
                    intThumbnailWidth = (int)(Math.Round((dbChangePercent * intImageWidth) / 100));
                }
            }
            else
            {
                intThumbnailWidth = 140;
                dbChangePercent = (100.0 * 140.0) / intImageWidth;
                intThumnailHeight = (int)(Math.Round((dbChangePercent * intImageHeight) / 100));

                intThumnailHeight = 140;
                dbChangePercent = (100.0 * 140.0) / intImageHeight;
                intThumbnailWidth = (int)(Math.Round((dbChangePercent * intImageWidth) / 100));
            }
            SessionUtility.SetSession("GAMEN", string.Empty);

            // Thumbnail作成
            System.Drawing.Image imgThumbnail = imgBeforeThumbnail.GetThumbnailImage(intThumbnailWidth, intThumnailHeight,
                new System.Drawing.Image.GetThumbnailImageAbort(ThumbnailCallback), IntPtr.Zero);

            return imgThumbnail;
        }

        #endregion

        #region "「GetThumbnailImage 関数」から使いてコールバック処理"

        /// <summary>
        /// 「GetThumbnailImage 関数」から使いてコールバック処理
        /// </summary>
        /// <returns>true</returns>
        private static bool ThumbnailCallback()
        {
            return true;
        }

        #endregion

        #region "xxxx年xx月xx日（xx）フォーマットに設定"

        /// <summary>
        /// xxxx年xx月xx日（xx）フォーマットに設定
        /// </summary>
        /// <param name="dtDate"></param>
        /// <param name="flgIsYearInclude"></param>
        /// <returns></returns>
        public static string GetDateString(DateTime dtDate, bool flgIsYearInclude)
        {
            string strDate = string.Empty;
            CultureInfo ciCountry = new CultureInfo("ja-JP");
            int intDay = (int)dtDate.DayOfWeek;
            String strDay;
            switch (intDay)
            {
                case 1:
                    strDay = "月";
                    break;
                case 2:
                    strDay = "火";
                    break;
                case 3:
                    strDay = "水";
                    break;
                case 4:
                    strDay = "木";
                    break;
                case 5:
                    strDay = "金";
                    break;
                case 6:
                    strDay = "土";
                    break;
                default:
                    strDay = "日";
                    break;
            }
            if (flgIsYearInclude)
            {
                strDate = dtDate.ToString("yyyy") + "年";
            }
            strDate += dtDate.ToString("MM") + "月" + dtDate.ToString("dd") + "日（" + strDay + "）";

            return strDate;
        }

        #endregion

        #region "何曜日かを取得する"

        /// <summary>
        /// 何曜日かを取得する
        /// </summary>
        /// <param name="dtDate"></param>
        /// <returns></returns>
        public static string GetDayOfWeek(DateTime dtDate)
        {
            int intDay = (int)dtDate.DayOfWeek;
            string strDay;
            switch (intDay)
            {
                case 1:
                    strDay = "月";
                    break;
                case 2:
                    strDay = "火";
                    break;
                case 3:
                    strDay = "水";
                    break;
                case 4:
                    strDay = "木";
                    break;
                case 5:
                    strDay = "金";
                    break;
                case 6:
                    strDay = "土";
                    break;
                default:
                    strDay = "日";
                    break;
            }

            return strDay;
        }

        #endregion

        #region "選択画面の検索処理"

        /// <summary>
        /// 選択画面の検索処理
        /// </summary>
        /// <param name="dtSearch">検索するデータテーブル</param>
        /// <param name="strSearch">検索するテキスト</param>
        /// <returns>検索したデータテーブル</returns>
        public static DataTable GetSearchedData(DataTable dtSearch, string strSearch)
        {
            DataTable dtGet = new DataTable();
            if (dtSearch != null && dtSearch.Rows.Count > 0)
            {
                if (!TextUtility.IsNullOrEmpty(strSearch))
                {
                    strSearch = TextUtility.EscapeSpecialCharacters(strSearch);
                    DataRow[] drSentakuSearch = dtSearch.Select("name LIKE '%" + strSearch + "%'", "id ASC");
                    if (drSentakuSearch.Length > 0)
                    {
                        dtGet = drSentakuSearch.CopyToDataTable();
                    }
                    else
                    {
                        dtGet = null;
                    }
                }
            }
            return dtGet;
        }

        #endregion

        #region "選択画面の検索処理"

        /// <summary>
        /// 選択画面の検索処理
        /// </summary>
        /// <returns>検索したデータテーブル</returns>
        public static DataTable GetYouteiSearchedData(DataTable dtSearch, string strSearch, string strID, string strName)
        {
            DataTable dtGet = new DataTable();
            if (dtSearch != null && dtSearch.Rows.Count > 0)
            {
                if (!TextUtility.IsNullOrEmpty(strSearch))
                {
                    strSearch = TextUtility.EscapeSpecialCharacters(strSearch);
                    DataRow[] drSentakuSearch = dtSearch.Select(strName + " LIKE '%" + strSearch + "%'", "" + strID + " ASC");
                    if (drSentakuSearch.Length > 0)
                    {
                        dtGet = drSentakuSearch.CopyToDataTable();
                    }
                    else
                    {
                        dtGet = null;
                    }
                }
            }
            return dtGet;
        }

        #endregion

        #region "時間分から分変更処理"

        /// <summary>
        /// 時間分から分変更処理
        /// </summary>
        /// <param name="strHr">時間</param>
        /// <param name="strMin">分</param>
        /// <returns>分</returns>
        public static int HourMinuteToMinute(string strHr, string strMin)
        {
            int intHr;
            int intMin;
            if (TextUtility.IsNullOrEmpty(strHr.Trim()))
            {
                intHr = 0;
            }
            else
            {
                intHr = Convert.ToInt16(strHr.Trim());
            }
            if (TextUtility.IsNullOrEmpty(strMin.Trim()))
            {
                intMin = 0;
            }
            else
            {
                intMin = Convert.ToInt16(strMin.Trim());
            }
            return (intHr * 60) + intMin;
        }

        #endregion

        #region "合計分から〇〇時間○○分フォーマットに変更する、0の場合表示しない"

        /// <summary>
        /// 合計分から〇〇時間○○分フォーマットに変更する、0の場合表示しない
        /// </summary>
        /// <param name="intTotalMinutes">時間の合計分</param>
        /// <returns></returns>
        public static string GetHourMinuteFormatString(int intTotalMinutes)
        {
            int intTotalHour = intTotalMinutes / 60;
            int intTotalMin = intTotalMinutes % 60;
            string strTotalHourMinute = "";

            if (intTotalHour != 0)
            {
                strTotalHourMinute = intTotalHour + "時間";
            }

            if (intTotalMin != 0)
            {
                strTotalHourMinute += intTotalMin + "分";
            }

            return strTotalHourMinute;
        }

        #endregion

        #region "改ページに設定するページ番号リストを作成する"

        /// <summary>
        /// 改ページに設定するページ番号リストを作成する
        /// </summary>
        /// <param name="intTotalPages">全ページ数</param>
        /// <param name="intCurrentPage">クリックされたページ番号</param>
        /// <returns>ページ番号リスト</returns>
        public static ArrayList CreateIndexForCustomPaging(int intTotalPages, int intCurrentPage)
        {
            int intFirstIndex = 1;
            int intLastIndex = intTotalPages + 1;
            int intCurrent = intCurrentPage + 1;
            if (intTotalPages > 10)
            {
                if (intCurrent >= 6)
                {
                    if (intTotalPages > intCurrent + 4)
                    {
                        intFirstIndex = intCurrent - 5;
                    }
                    else
                    {
                        intFirstIndex = intTotalPages - 9;
                    }
                }
                intLastIndex = intFirstIndex + 10;
            }

            ArrayList pages = new ArrayList();
            for (int i = intFirstIndex; i < intLastIndex; i++)
            {
                pages.Add((i).ToString());
            }

            return pages;
        }

        #endregion
    }
}