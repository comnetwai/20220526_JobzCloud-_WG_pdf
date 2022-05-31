using Common.Info;

namespace Common
{
    public class SessionUtility
    {
        #region "GetSession"

        /// <summary>
        /// セッション取得処理
        /// </summary>
        /// <param name="key">String</param>
        /// <remarks>なるべく使用しない</remarks>
        public static object GetSession(string key)
        {
            // セッションから情報を取得
            object objReturn = System.Web.HttpContext.Current.Session[key];

            return objReturn;
        }

        #endregion

        #region "GetSession"

        /// <summary>
        /// セッション取得処理
        /// </summary>
        /// <param name="Key">Enums.SessionKeyから選択する</param>
        /// <remarks>
        /// 引数で渡されたキーを検索し、セッションから情報を取得する。
        /// </remarks>
        public static object GetSession(Enums.SessionKey Key)
        {
            // セッションから情報を取得
            return GetSession(Key.ToString());
        }

        #endregion

        #region "SetSession"

        /// <summary>
        /// セッション情報格納処理
        /// </summary>
        /// <param name="Key">Key(String)</param>
        /// <param name="value">Value</param>
        /// <remarks>
        /// 引数で渡されたキーを検索し、セッションから情報を取得する。
        /// </remarks>
        public static void SetSession(string Key, object value)
        {
            System.Web.HttpContext.Current.Session.Add(Key.ToString(), value);
        }

        #endregion

        #region "SetSession"

        /// <summary>
        /// セッション情報格納処理
        /// </summary>
        /// <param name="Key">Enums.SessionKeyから選択する</param>
        /// <param name="value">Value</param>
        /// <remarks>
        /// 引数で渡されたキーを検索し、セッションから情報を取得する。
        /// </remarks>
        public static void SetSession(Enums.SessionKey Key, object value)
        {
            System.Web.HttpContext.Current.Session.Add(Key.ToString(), value);
        }

        #endregion

        #region "DelSession"

        /// <summary>
        /// セッション情報削除処理
        /// </summary>
        /// <param name="Key">Key(String)</param>
        /// <remarks>
        /// 引数で渡されたキーを検索し、セッションから情報を取得する。
        /// </remarks>
        public static void DelSession(string Key)
        {
            System.Web.HttpContext.Current.Session.Remove(Key.ToString());
        }

        #endregion

        #region "DelSession"

        /// <summary>
        /// セッション情報削除処理
        /// </summary>
        /// <param name="Key">Enums.SessionKeyから選択する</param>
        /// <remarks>
        /// 引数で渡されたキーのセッションを削除する。
        /// </remarks>
        public static void DelSession(Enums.SessionKey Key)
        {
            System.Web.HttpContext.Current.Session.Remove(Key.ToString());
        }

        #endregion

        #region "IsSessionDataExist"

        /// <summary>
        /// セッションのデータのチェック
        /// </summary>
        /// <returns>
        /// True :ある場合
        /// False:無い場合</returns>
        /// <remarks></remarks>
        public static bool IsSessionDataExist()
        {
            bool blnReturn;           // 戻り値

            // セッションのデータのチェック
            if (System.Web.HttpContext.Current.Session.Count == 0)
                blnReturn = false;    //　セッションのデータがある場合
            else
                blnReturn = true;     // セッションにデータが無い場合

            return blnReturn;
        }

        #endregion
    }
}