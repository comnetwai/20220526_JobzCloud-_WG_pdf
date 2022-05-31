/***************************************************************
 * ＜システム名＞ JobzRacooシステム
 * ＜プログラム＞ 共通システム情報
 * ＜ファイル名＞ CommonSystemInfo.cs
 * ＜作成者＞     MDCR
 * ＜作成日＞     2015/05/27
 ***************************************************************/
using System;
using System.Data;
using System.Collections;

namespace Common.Info
{
    /// <summary>
    /// 共通システム情報
    /// </summary>
    [Serializable]
    public class CommonSystemInfo
    {

        #region "プロパティ変数"

        /// <summary>
        /// ユーザーコード
        /// </summary>
        private string _strcUserID;

        /// <summary>
        /// ユーザー名称
        /// </summary>
        private string _strsUserName;

        #endregion

        #region "プロパティ"

        /// <summary>
        /// ユーザーコード
        /// </summary>
        public string session_UserID
        {
            set
            {
                _strcUserID = value;
            }
            get
            {
                return _strcUserID;
            }
        }

        /// <summary>
        /// ユーザー名称
        /// </summary>
        public string session_UserName
        {
            set
            {
                _strsUserName = value;
            }
            get
            {
                return _strsUserName;
            }
        }

        #endregion
    }
}
