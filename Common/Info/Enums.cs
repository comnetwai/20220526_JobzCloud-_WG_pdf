/***************************************************************																						
 * ＜システム名＞ JobzRacoo
 * ＜プログラム＞ 共通定数　
 * ＜ファイル名＞ Enums.cs
 * ＜作成者＞     MDCR
 * ＜作成日＞     2015/05/27
***************************************************************/
namespace Common.Info
{
    /// <summary>
    /// 共通定数
    /// </summary>
    public class Enums        
    {
        #region "SessionKey"

        /// <summary>
        /// システム共通セッションキー
        /// </summary>
        public enum SessionKey
        {
            CommonSystemInfo,           //共通システム情報
            session_ErrorTimes,         //バックアップエラーCount
            session_Answer,
            session_PoupParamIn,        //ポップアップ画面入力パラメーター
            session_PoupParamOut
        }

        #endregion

        #region "ExType"

        /// <summary>
        /// 例外タイプ
        /// </summary>
        public enum ExType
        {
            System,     //システムエラー    
            DB,         //DBエラー
            IO,         //IOエラー
            Config,     //Configエラー
            Warn        //警告エラー
        }

        #endregion

        #region "LogLevel"

        /// <summary>
        /// ログレベル
        /// </summary>
        public enum LogLevel
        {
            Info,   //インフォメーション
            Warn,   //ワーニング
            Err,    //エラー
            Fatal,   //致命的エラー
            Debug   //デバッグ情報   
        }

        #endregion

        #region "対応フラグ"

        /// <summary>
        /// 対応フラグ
        /// </summary>
        public enum SUPPORT_FLAG
        {
            NEW = 0,
            EDIT = 1
        }

        #endregion

        #region "SEX"

        /// <summary>
        /// 対応フラグ
        /// </summary>
        public enum SEX
        {
            MALE = 1,
            FEMALE = 2
        }

        #endregion

        #region "削除フラグ"

        /// <summary>
        /// 対応フラグ
        /// </summary>
        public enum DEL
        {
            DELETE = 1,
            NO_DELETE = 0
        }

        #endregion
    }
}