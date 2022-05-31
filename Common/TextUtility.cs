using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Common
{
    public class TextUtility
    {
        /// <summary>
        /// Shift-JISエンコーディング
        /// </summary>
        /// <remarks></remarks>
        public static readonly Encoding SJIS_ENCODING = Encoding.GetEncoding("Shift_JIS");
        public static string invalidtext_all;
        #region "テキストをEncryptDataする処理"

        /// <summary>
        /// テキストをEncryptDataする処理
        /// </summary>
        /// <param name="password">パスワードデータ</param>
        /// <returns>EncryptData</returns>
        public static string EncryptData_Henkou(string password)
        {
            //string strEncryptpwd = string.Empty;
            //byte[] encode = new byte[password.Length];
            //encode = Encoding.UTF8.GetBytes(password);
            //strEncryptpwd = Convert.ToBase64String(encode);
            //return strEncryptpwd;

            return DESEncryp.Encrypt(password, "demo20", "");
        }

        #endregion

        /// <summary>
        /// EncryptDataをDecryptする処理
        /// </summary>
        /// <returns>DecryptData</returns>
        public static string DecryptData_Henkou(string encryptpwd)
        {
            //string strDecryptpwd = string.Empty;
            //UTF8Encoding encodepwd = new UTF8Encoding();
            //Decoder Decode = encodepwd.GetDecoder();
            //byte[] todecode_byte = Convert.FromBase64String(encryptpwd);
            //int intCharCount = Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
            //char[] decoded_char = new char[intCharCount];
            //Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
            //strDecryptpwd = new String(decoded_char);
            //return strDecryptpwd;
            return DESEncryp.Decrypt(encryptpwd, "demo20", "");
        }

        #region "IsIncludeZenkaku"

        /// <summary>
        /// 全角文字チェック
        /// 入力データを全角があるのをチェックする
        /// </summary>
        /// <returns>true/false</returns>
        public static bool IsIncludeZenkaku(string strText)
        {
            foreach (char charInput in strText.ToCharArray())
            {
                String temp = Convert.ToString(charInput);
                byte[] bytArray = GetBytes(temp);
                byte byt = bytArray[0];
                if (!(0x20 <= byt && byt <= 0x7e))
                {
                    return true;
                }
            }
            return false;
        }

        public static bool IsPhoneNumber(string Telno)
        {
            if (string.IsNullOrEmpty(Telno))
            {
                return false;
            }
            return Regex.IsMatch(Telno, @"^0[5-9]0[0-9]{8}|0[1-9][1-9][0-9]{7}$");
        }


        #endregion

        public static byte[] GetBytes(string strText)
        {
            return SJIS_ENCODING.GetBytes(strText);
        }

        #region "字数制限によってテキストを表示する処理"

        /// <summary>
        /// 字数制限によってテキストを表示する処理
        /// </summary>
        /// <returns>字数カウントのデータ</returns>
        public static string GetMaxLengthCharacterString(String strText, int intStringCount)
        {
            String strFinalText = String.Empty;

            if (strText.Length > intStringCount)
            {
                strFinalText = strText.Substring(ConstantVal.ZERO_INTEGER, intStringCount);
            }
            else
            {
                strFinalText = strText;
            }
            return strFinalText;
        }

        public static bool IsValidEmailAddress(string InputEmail)
        {
            Regex regex = new Regex(@"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
              @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$");

            Match match = regex.Match(InputEmail);
            // メールフォーマットは正しくと入力したメールアドレスに【?】と【&】が有る場合、
            if (match.Success && !InputEmail.Contains("?") && !InputEmail.Contains("&"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //public static bool IsCheckRealMailAddress(string InputEmailAddress)
        //{
        //    //TcpClient tClient = new TcpClient("gmail-smtp-in.l.google.com", 587);
        //    //string CRLF = "\r\n";
        //    //byte[] dataBuffer;
        //    //string ResponseString;
        //    //NetworkStream netStream = tClient.GetStream();
        //    //StreamReader reader = new StreamReader(netStream);
        //    //ResponseString = reader.ReadLine();
        //    ///* Perform HELO to SMTP Server and get Response */
        //    //dataBuffer = BytesFromString("HELO KirtanHere" + CRLF);
        //    //netStream.Write(dataBuffer, 0, dataBuffer.Length);
        //    //ResponseString = reader.ReadLine();
        //    //dataBuffer = BytesFromString("MAIL from:<jobzcloud2021@gmail.com>" + CRLF);
        //    //netStream.Write(dataBuffer, 0, dataBuffer.Length);
        //    //ResponseString = reader.ReadLine();
        //    ///* Read Response of the RCPT TO Message to know from google if it exist or not */
        //    //dataBuffer = BytesFromString("RCPT TO:<" + InputEmailAddress + ">" + CRLF);
        //    //netStream.Write(dataBuffer, 0, dataBuffer.Length);
        //    //ResponseString = reader.ReadLine();
        //    //if (GetResponseCode(ResponseString) == 550)
        //    //{
        //    //    tClient.Close();
        //    //    return false;
        //    //}
        //    //tClient.Close();
        //    //return true;
        //}

        private static int GetResponseCode(string responseString)
        {
            return int.Parse(responseString.Substring(0, 3));
        }

        private static byte[] BytesFromString(string str)
        {
            return Encoding.ASCII.GetBytes(str);
        }

        #endregion

        #region "検索の時文章にあるSpecialCharacter【', [, ], #, *】を変更する処理"

        /// <summary>
        /// 検索の時テキストにあるSpecialCharacter【', [, ], #, *】を変更する処理
        /// </summary>
        /// <param name="strText">入力データ</param>
        /// <returns>変更した文章</returns>
        public static string EscapeSpecialCharacters(string strText)
        {
            StringBuilder sb = new StringBuilder(strText.Length);
            for (int i = 0; i < strText.Length; i++)
            {
                char c = strText[i];
                switch (c)
                {
                    case ']':
                    case '[':
                    case '%':
                    case '*':
                        sb.Append("[" + c + "]");
                        break;
                    case '\'':
                        sb.Append("''");
                        break;
                    default:
                        sb.Append(c);
                        break;
                }
            }
            return sb.ToString();
        }

        #endregion

        #region "isNullOrEmpty"

        /// <summary>
        /// 空文字チェック
        /// </summary>
        /// <param name="strText">対象文字列</param>
        /// <returns>True:空文字,False:空文字でない</returns>
        /// <remarks>文字列がNull、長さ0の文字列または、半角スペースのみの場合Trueを返します。</remarks>
        public static bool IsNullOrEmpty(string strText)
        {
            if (strText == null)
            {
                return true;
            }
            if (strText == "")
            {
                return true;
            }
            return (strText.Length == 0);
        }

        #endregion

        public static bool isomojiCharacter(string str)
        {
            string formattedString = str;
            byte[] bytes = TextUtility.GetBytes(formattedString);
            string sjisString = TextUtility.SJIS_ENCODING.GetString(bytes);
            if (sjisString != formattedString)
            {
                invalidtext_all = "";
                int length = Math.Min(sjisString.Length, formattedString.Length);
                for (int index = 0; index < length; index++)
                {
                    if (sjisString[index] != formattedString[index])
                    {
                        int diff = index;
                        string invalidString = StringInfo.GetNextTextElement(formattedString, diff);
                        if (invalidString.Length > 1)
                        {
                            if (invalidtext_all.Contains(invalidString) != true)
                            {
                                if (invalidtext_all == "")
                                {
                                    invalidtext_all = invalidString;
                                }
                                else
                                {
                                    invalidtext_all = invalidtext_all + "、" + invalidString;
                                }

                                return true;
                            }
                            
                        }
                    }
                }
            }
            return false;
        }
    }
}
