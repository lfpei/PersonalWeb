using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace PersonalWeb
{
    public class SendMsgSingle
    {
        public static string PostMsgFunction(string mobile, string nationcode,out string msg)
        {
            string randnum = getRand(4);
            msg = randnum;
            if (string.IsNullOrEmpty(randnum))
            {
                return @"{""result"":""1"",""errmsg"":""生成验证码失败！""}";
            }
            string serviceAddress = "https://yun.tim.qq.com/v5/tlssmssvr/sendsms?sdkappid=1400079280&random=" + randnum + "";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(serviceAddress);

            request.Method = "POST";
            request.ContentType = "application/json";
            TimeSpan cha = (DateTime.Now - TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1)));
            string strTime = ((long)cha.TotalSeconds).ToString(); //unix 时间戳
            string sig = getSig(mobile, strTime, randnum);
            string strContent = @"{ ""params"":[""" + randnum + @""",""10""],""sig"":""" + sig + @""",""sign"":""lfpei"",""tel"":{""mobile"":""" + mobile + @""",""nationcode"":""" + nationcode + @"""},""time"":""" + strTime + @""",""tpl_id"":""102876""}";
            using (StreamWriter dataStream = new StreamWriter(request.GetRequestStream()))
            {
                dataStream.Write(strContent);
                dataStream.Close();
            }
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            string encoding = response.ContentEncoding;
            if (encoding == null || encoding.Length < 1)
            {
                encoding = "UTF-8"; //默认编码    
            }
            StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding(encoding));
            string retString = reader.ReadToEnd();
            return retString;
        }
        private static string getSig(string mobile, string strTime, string randnum)
        {
            string strMobile = mobile; //tel 的 mobile 字段的内容
            string strAppKey = "8b256c31a2586bbd3c7dd11c4307dc98"; //sdkappid 对应的 appkey，需要业务方高度保密
            string strRand = randnum; //url 中的 random 字段的值
            string sig = HashHelper.Hash_SHA_256("appkey=" + strAppKey + "&random=" + strRand + "&time=" + strTime + "&mobile=" + strMobile + "", false);
            return sig;
        }

        private static string getRand(int length)
        {
            string randnum = string.Empty;
            Random rad = new Random();
            for (int i = 0; i < length; i++)
            {
                randnum += rad.Next(0, 10);
            }
            return randnum;
        }
    }
}