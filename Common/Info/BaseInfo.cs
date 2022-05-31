/***************************************************************
 * ＜システム名＞ JobzRacooシステム
 * ＜プログラム＞ 基本情報
 * ＜ファイル名＞ BaseInfo.cs
 * ＜作成者＞     MDCR
 * ＜作成日＞     2015/05/27
***************************************************************/

namespace Common.Info
{
    /// <summary>
    /// 基本情報
    /// </summary>
    public class BaseInfo
    {

        /// <summary>
        /// 共通システム情報
        /// </summary>
        private CommonSystemInfo _CommonSystemInfo;

        /// <summary>
        /// 共通システム情報
        /// </summary>
        public CommonSystemInfo CommonSystemInfo
        {
            get
            {
                return _CommonSystemInfo;
            }
            set
            {
                _CommonSystemInfo = value;
            }
        }

        /// <summary>
        /// 画面ID
        /// </summary>
        private string _strGamenID;

        /// <summary>
        /// 画面ID
        /// </summary>
        public string GamenID
        {
            get
            {
                return _strGamenID;
            }
            set
            {
                _strGamenID = value;
            }
        }

        /// <summary>
        /// 画面名
        /// </summary>
        private string _strGamenName;

        /// <summary>
        /// 画面名
        /// </summary>
        public string GamenName
        {
            get
            {
                return _strGamenName;
            }
            set
            {
                _strGamenName = value;
            }
        }

        /// <summary>
        /// IPアドレス
        /// </summary>
        private string _strIPAddress;

        /// <summary>
        /// IPアドレス
        /// </summary>
        public string session_IP
        {
            get
            {
                return _strIPAddress;
            }
            set
            {
                _strIPAddress = value;
            }
        }

        /// <summary>
        /// PC名
        /// </summary>
        private string _strPCName;

        /// <summary>
        /// PC名
        /// </summary>
        public string session_PCName
        {
            get
            {
                return _strPCName;
            }
            set
            {
                _strPCName = value;
            }
        }
    }
}
