using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Diagnostics;

namespace Common
{
    public class DESEncryp
    {
        /// <summary>
        /// 暗号キーの生成
        /// </summary>
        /// <param name="sKey">時間11桁　十分単位</param>
        /// <returns>暗号キー</returns>
        public static string GetKeyBase64(string sKey)
        {
            try
            {
                return GenerateKeyFromPassword(sKey,192,64);
            }
            catch
            {
                //エラー処理
                return null;
            }
        }
        
        /// <summary>
        /// 加密，并且返回加密后的字符串
        /// </summary>
        /// <param name="sEnc">明文</param>
        /// <param name="sKey">密匙</param>
        /// <param name="vec">移动向量</param>
        /// <returns>加密后的字符串</returns>
        public static string Encrypt(string sEnc, string sKey, string vec)
        {
            try
            {
                return TriDESEncrypt(sEnc, sKey);
            }
            catch (Exception ex)
            {
                //エラー処理
                EventLog.WriteEntry("Encrypt", ex.Message);
                return "";
            }
        }

        /// <summary>
        /// 解密，并且返回明文字符串
        /// </summary>
        /// <param name="v">被加密的字符串</param>
        /// <param name="key">密匙</param>
        /// <param name="vec">移动向量</param>
        /// <returns>返回解密后的明文</returns>
        public static string Decrypt(string sDnc, string sKey, string vec)
        {
            try
            {
                return TriDESDecrypt(sDnc, sKey);
            }
            catch (Exception ex)
            {
                EventLog.WriteEntry("Decrypt", ex.Message);
                return "";
            }
        }

        /// <summary>
        /// 3DES加密
        /// </summary>
        /// <param name="sData">被加密的明文</param>
        /// <param name="Key">密钥</param>
        /// <param name="Vector">向量</param>
        /// <returns>密文</returns>
        private static string TriDESEncrypt(string sEnc, string Key)
        {
            try
            {
                string sKey;
                byte[] bKey;    //3DESの暗号キー
                byte[] bVector;//3DESのVector
                byte[] bEnc_Bfor;//暗号文 暗号化前
                byte[] bEnc;//暗号文

                TripleDES tdes = TripleDES.Create();
                // 暗号キーの取得
                sKey = GenerateKeyFromPassword(Key, tdes.KeySize, tdes.BlockSize);
                if (sKey == null) return null;//エラー処理
                //string→bytes
                bEnc_Bfor =  Encoding.UTF8.GetBytes(sEnc);
                bKey = Convert.FromBase64String(sKey);
                bVector = Encoding.Default.GetBytes("00000000");

                //パラメータ設定
                tdes.IV = bVector;
                tdes.Key = bKey;
                tdes.Mode = CipherMode.ECB;
                tdes.Padding = PaddingMode.PKCS7;

                // encryption
                ICryptoTransform ict = tdes.CreateEncryptor();
                bEnc = ict.TransformFinalBlock(bEnc_Bfor, 0, bEnc_Bfor.Length);

                return Convert.ToBase64String(bEnc, 0, bEnc.Length);
            }
            catch
            {
                return null;
            }
           
        }

        /// <summary>
        /// 3DES解密
        /// </summary>
        /// <param name="sData">被加密的明文</param>
        /// <param name="Key">暗号キー(時間)</param>
        /// <param name="Vector">向量</param>
        /// <returns>密文</returns>
        private static string TriDESDecrypt(string sDnc, string Key)
        {
            try
            {
                string sKey;
                byte[] bKey;//3DESの暗号キー
                byte[] bVector;//3DESのVector
                byte[] bEnc;//復号文
                byte[] bDnc;//暗号文 

                TripleDES tdes = TripleDES.Create();
                // 暗号キーの取得
                sKey = GenerateKeyFromPassword(Key, tdes.KeySize, tdes.BlockSize);
                if (sKey == null) return null;//エラー処理
                //string→bytes
                bDnc = Convert.FromBase64String(sDnc);
                bKey = Convert.FromBase64String(sKey);
                bVector = Encoding.Default.GetBytes("00000000");
                
                //パラメータ設定
                tdes.IV = bVector;
                tdes.Key = bKey;
                tdes.Mode = CipherMode.ECB;
                tdes.Padding = PaddingMode.PKCS7;

                // encryption
                ICryptoTransform ict = tdes.CreateEncryptor();
                ict = tdes.CreateDecryptor();
                bEnc = ict.TransformFinalBlock(bDnc, 0, bDnc.Length);

                //
                return Encoding.UTF8.GetString(bEnc);
            }
            catch
            {
                return null;
            }
            
        }

        /// <summary>
        /// パスワードから共有キーと初期化ベクタを生成する
        /// </summary>
        /// <param name="password">基になるパスワード</param>
        /// <param name="keySize">共有キーのサイズ（ビット）</param>
        /// <param name="key">作成された共有キー</param>
        /// <param name="blockSize">初期化ベクタのサイズ（ビット）</param>
        /// <param name="iv">作成された初期化ベクタ</param>
        private static string GenerateKeyFromPassword(string password,
            int keySize,int blockSize)
        {
            try
            {
                //パスワードから共有キーと初期化ベクタを作成する
                //saltを決める
                byte[] salt = System.Text.Encoding.UTF8.GetBytes("saltは必ず8バイト以上");
                //Rfc2898Derivebytesオブジェクトを作成する
                System.Security.Cryptography.Rfc2898DeriveBytes derivebytes =
                    new System.Security.Cryptography.Rfc2898DeriveBytes(password, salt);
                //.NET Framework 1.1以下の時は、PasswordDerivebytesを使用する
                //System.Security.Cryptography.PasswordDerivebytes derivebytes =
                //    new System.Security.Cryptography.PasswordDerivebytes(password, salt);
                //反復処理回数を指定する デフォルトで1000回
                derivebytes.IterationCount = 1000;

                //共有キーと初期化ベクタを生成する
                byte[] key;
                string sKey;
                key = derivebytes.GetBytes(keySize / 8);
                sKey = Convert.ToBase64String(key);
                return sKey;
                //iv = derivebytes.GetBytes(blockSize / 8);
                //iv = Encoding.Default.GetBytes("00000000");
            }
            catch
            {
                return null;
            }
        }
    }

}