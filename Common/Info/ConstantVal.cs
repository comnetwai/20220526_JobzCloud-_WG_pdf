/***************************************************************
 * ＜システム名＞ JobzRacooシステム
 * ＜プログラム＞ 定数の設定
 * ＜ファイル名＞ ConstantVal.cs
 * ＜作成者＞     MDCR Thin Zar Aung
 * ＜作成日＞     2019/03/05
 * ＜修正者＞     なし
 * ＜修正日＞     なし
 * ＜修正内容＞   なし
 ***************************************************************/

namespace Common.Info
{
    /// <summary>
    /// 定数の設定
    /// </summary>
    public class ConstantVal
    {
        #region "定数の設定"

        // 文字列ゼロ
        public const string ZERO_STRING = "0";

        // 文字列一
        public const string ONE_STRING = "1";

        // 数値列ゼロ
        public const int ZERO_INTEGER = 0;

        // 数値列1.00
        public const double ONE_DBL = 1.00;

        // 数値列1
        public const int ONE_INT = 1;

        // 数値列2
        public const int TWO_INT = 2;

        // 文字列-1
        public const int MINUS_ONE_INT = -1;

        // 作業者
        public const string SAGYOUSHA = "作業者";

        // 得意先
        public const string TOKUISAKI = "得意先";

        // 出荷先
        public const string SHUKKASAKI = "出荷先";

        // マスター画面のため
        public const string MASTER_STRING = "MASTER";
        // ホーム画面のため
        public const string HOME_STRING = "HOME";

        // ブラウザー名
        public const string EDGE_BROWSER = "Edge";
        public const string CHROME_BROWSER = "Chrome";

        // SessionTimeOutのためエラー内容
        public const string SESSION_TIMEOUT = "session_timeout";

        // 画像のサイズ５M以上かチェックのため
        public const int IMAGESIZE = 5242880;

        // 付箋一覧に表示する付箋ボード数
        public const int FUSEN_BOARD_COUNT = 6;

        // 案件選択画面に1ページに表示する案件数
        public const int ANKEN_PAGE_SIZE = 20;

        // ユーザー状態のため設定
        public const string USER_STATUS_YUKOU = "0";
        public const string USER_STATUS_KARITOUROKU = "1";
        public const string USER_STATUS_MUKOU = "2";

        // 機械状態のため設定
        public const string Kikai_STATUS_YUKOU = "0";
        public const string Kikai_STATUS_MUKOU = "2";

        // 作業者区分
        public const string OPERATOR_FLAG = "1";

        public const string FUSEN_STATUS_MIZUMI = "0";
        public const string FUSEN_STATUS_ZUMI = "1";

        // ボード数のため設定
        public const int TANTOU_BOARD_COUNT = 20;
        public const int PENDING_BOARD_COUNT = 100;

        public const string FULL_ACCESS = "0";
        public const string READ_ONLY = "1";
        public const string NO_ACCESS = "2";

        // 検索データが無い時表示するメッセージ
        public const string NO_DATA_MESSAGE = "該当するデータがありません。";

        // DB.XMLファイルからデータを使う
        public const bool USE_DB_CONFIG = true;

        // DBUtilityのエラーReturnValue
        public const int UNIQUE_ERROR = -1;
        public const int INCORRECT_ERROR = -2;

        // CSV
        public const string CSV_EXTENSION = "csv";

        public const int CSV_HEADER_COUNT = 8;

        // IDの最大値
        public const long MAXIMUM_ID = 4294967295;

        // 【案件名】文字数の制限
        public const int ANKENNAME_MAXLENGTH = 100;

        // 【得意先名】文字数の制限
        public const int TOKUISAKINAME_MAXLENGTH = 100;

        // 【出荷先名】文字数の制限
        public const int SHUKKASAKINAME_MAXLENGTH = 100;

        // 【出荷先住所】文字数の制限
        public const int SHUKKASAKIJUUSHO_MAXLENGTH = 200;

        // 【ユーザー名】文字数の制限
        public const int USERNAME_MAXLENGTH = 50;

        // 【案件メモ】文字数の制限
        public const int ANKENMEMO_MAXLENGTH = 2000;

        // 【納品数】文字数の制限
        public const int NOUHINSUU_MAXLENGTH = 7;

        // 【メールアドレス】文字数の制限
        public const int EMAIL_MAXLENGTH = 100;

        // 【会社名】文字数の制限
        public const int KAISHANAME_MAXLENGTH = 100;

        // 【都道府県】文字数の制限
        public const int PREFECTURES_MAXLENGTH = 20;

        // 【番地.ビル名】文字数の制限
        public const int ADDRESS_MAXLENGTH = 200;

        // 【付箋タイル】文字数の制限
        public const int FUSENTITLE_MAXLENGTH = 100;

        // 【付箋メモ】文字数の制限
        public const int FUSENMEMO_MAXLENGTH = 200;

        // 登録モード
        public const string INSERT_MODE = "0";

        // 更新モード
        public const string UPDATE_MODE = "1";

        // 会社情報の退会ため設定
        public const string Client_STATUS_MUKOU = "9";

        // 会社情報の有効ため設定
        public const string Client_STATUS_YUUKOU = "0";

        // 会社情報仮登録のため設定
        public const string Client_STATUS_KARITOUROKU = "1";

        // 会社情報登録期限切れのため設定
        public const string Client_STATUS_EXPIRED = "8";

        // 調査内容表示の設定
        public const string SURVEY_NAIYOU_YUKOU = "0";

        // 権限のため設定
        public const string AUTHORITY_KANRISHA = "1";
        public const string AUTHORITY_MANAGER = "2";
        public const string AUTHORITY_IPPANUSER = "3";

        // 付箋色名文字数の制限
        public const int FUSEN_COLOR_NAME_MAXLENGTH = 16;

        // プランのため設定
        public const string PLAN_MURYOUU = "1";
        public const string PLAN_DEMO = "99";

        #endregion
    }
}
