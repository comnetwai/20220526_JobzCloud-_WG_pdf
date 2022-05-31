using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Info
{
    public class MailInfo
    {
        string strSubject;
        string strBody;
        string[] strsToList = new string[0];
        string[] strsCcList = new string[0];
        string[] strsBccList = new string[0];
        string[] strsAttachList = new string[0];
        bool isBodyHtml = false;
        /// <summary>
        /// 主題
        /// </summary>
        public string Subject
        {
            get
            {
                return strSubject;
            }
            set
            {
                strSubject = value;
            }
        }

        /// <summary>
        /// 本文
        /// </summary>
        public string Body
        {
            get
            {
                return strBody;
            }
            set
            {
                strBody = value;
            }
        }

        /// <summary>
        /// 宛先
        /// </summary>
        public string[] ToList
        {
            get
            {
                return strsToList;
            }
            set
            {
                strsToList = value;
            }
        }

        /// <summary>
        /// Cc
        /// </summary>
        public string[] CcList
        {
            get
            {
                return strsCcList;
            }
            set
            {
                strsCcList = value;
            }
        }

        /// <summary>
        /// Bcc
        /// </summary>
        public string[] BccList
        {
            get
            {
                return strsBccList;
            }
            set
            {
                strsBccList = value;
            }
        }

        /// <summary>
        /// 添付ファイル
        /// </summary>
        public string[] AttachList
        {
            get
            {
                return strsAttachList;
            }
            set
            {
                strsAttachList = value;
            }
        }

        /// <summary>
        /// To追加
        /// </summary>
        /// <param name="strTo">To</param>
        public void AddToList(string strTo)
        {
            if (strsToList == null)
            {
                strsToList = new string[1];
            }
            else
            {
                Array.Resize(ref strsToList, strsToList.Length + 1);
            }
            strsToList[strsToList.Length - 1] = strTo;
        }

        /// <summary>
        /// Cc追加
        /// </summary>
        /// <param name="strCc">Cc</param>
        public void AddCcList(string strCc)
        {
            if (strsCcList == null)
            {
                strsCcList = new string[1];
            }
            else
            {
                Array.Resize(ref strsCcList, strsCcList.Length + 1);
            }
            strsCcList[strsCcList.Length - 1] = strCc;
        }

        /// <summary>
        /// Bcc追加
        /// </summary>
        /// <param name="strBcc">Bcc</param>
        public void AddBccList(string strBcc)
        {
            if (strsBccList == null)
            {
                strsBccList = new string[1];
            }
            else
            {
                Array.Resize(ref strsBccList, strsBccList.Length + 1);
            }
            strsBccList[strsBccList.Length - 1] = strBcc;
        }

        /// <summary>
        /// Body型
        /// </summary>
        public bool IsBodyHtml
        {
            get { return isBodyHtml; }
            set { isBodyHtml = value; }
        }
    }
}