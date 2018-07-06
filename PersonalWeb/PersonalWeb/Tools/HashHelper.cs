using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace PersonalWeb
{
    public class HashHelper
    {
        /// <summary>
        /// 计算SHA-256码
        /// </summary>
        /// <param name="word">字符串</param>
        /// <param name="toUpper">返回哈希值格式 true：英文大写，false：英文小写</param>
        /// <returns></returns>
        public static string Hash_SHA_256(string word, bool toUpper)
        {
            try
            {
                System.Security.Cryptography.SHA256CryptoServiceProvider SHA256CSP
                    = new System.Security.Cryptography.SHA256CryptoServiceProvider();

                byte[] bytValue = System.Text.Encoding.UTF8.GetBytes(word);
                byte[] bytHash = SHA256CSP.ComputeHash(bytValue);
                SHA256CSP.Clear();

                //根据计算得到的Hash码翻译为SHA-1码
                string sHash = "", sTemp = "";
                for (int counter = 0; counter < bytHash.Count(); counter++)
                {
                    long i = bytHash[counter] / 16;
                    if (i > 9)
                    {
                        sTemp = ((char)(i - 10 + 0x41)).ToString();
                    }
                    else
                    {
                        sTemp = ((char)(i + 0x30)).ToString();
                    }
                    i = bytHash[counter] % 16;
                    if (i > 9)
                    {
                        sTemp += ((char)(i - 10 + 0x41)).ToString();
                    }
                    else
                    {
                        sTemp += ((char)(i + 0x30)).ToString();
                    }
                    sHash += sTemp;
                }

                //根据大小写规则决定返回的字符串
                return toUpper ? sHash : sHash.ToLower();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string Hash_MD5(string str)
        {
            MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
            return BitConverter.ToString(hashmd5.ComputeHash(Encoding.Default.GetBytes(str))).Replace("-", "").ToLower();
        }

    }
}