
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
namespace PersonalWeb
{
    public class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string GetUserInfo(string sql)
        {
            DataSet ds = null;
            string result = "{}";
            if (string.IsNullOrEmpty(sql))
            {
                result = @"[{""returnCord"":""0"",""returnMsg"":""请输入信息！""}]";
            }
            if (DatabaseHelper.GetData(sql, out ds))
            {
                if (ds != null)
                {
                    result = JsonConvert.SerializeObject(ds.Tables[0]);
                }
            }
            return result;
        }
        /// <summary>
        /// 获取验证码
        /// </summary>
        /// <param name="mobile">手机号</param>
        /// <param name="nationcode">国家（默认中国86）</param>
        /// <returns></returns>
        [HttpPost]
        public string GetMsg(string mobile, string nationcode)
        {
            string result = @"{""returnCode"":""10000"",""returnMsg"":""获取数据失败""}";
            DataSet ds = null;
            DateTime date = System.DateTime.Now;
            int days = DateHelper.getDays(date);
            string dateNow = date.ToString("yyyy-MM-dd HH:mm:ss");
            string sql = @"select * from LF_MessageRecord where operatortime>='" + date.ToString("yyyy-MM-01") + "' and operatortime<='" + date.ToString("yyyy-MM-") + days +"';";
            sql += @"select * from LF_MessageRecord where operatortime>='" + dateNow.Substring(0,10) + " 00:00:00" + "' and operatortime<='"+ date.ToString("yyyy-MM-dd") + " 23:59:59" +"' and mobile='" + mobile + "';";
            DatabaseHelper.GetData(sql,out ds);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count >= 100)
                {
                    result = @"{""returnCode"":""10000"",""returnMsg"":""该商户短信服务已停用""}";
                    return result;
                }
                if (ds.Tables[1].Rows.Count >= 4)
                {
                    result = @"{""returnCode"":""10000"",""returnMsg"":""今日获取验证码已超过最大次数""}";
                }
                else
                {
                    string autocode = string.Empty;
                    string msg = SendMsgSingle.PostMsgFunction(mobile, nationcode,out autocode);
                    JObject jmsg = (JObject)JsonConvert.DeserializeObject(msg);
                    if (jmsg["result"].ToString() != "0")
                    {
                        result = @"{""returnCode"":""10000"",""returnMsg"":""" + jmsg["errmsg"].ToString() + @"""}";
                    }
                    else
                    {
                        string SQL = @"insert into LF_MessageRecord (mobile,operatortime,msg) values ('"+ mobile +"','"+ date.ToString("yyyy-MM-dd HH:mm:ss") +"','"+ autocode +"');";
                        DatabaseHelper.Post(SQL);
                        result = @"{""returnCode"":""00000"",""returnMsg"":"""+ autocode +@"""}";
                    }
                }
            }
            return result;
        }
        /// <summary>
        /// 注册
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public string Register(string mobile, string pwd, string msg)
        {
            string result = @"{""returnCode"":""9999"",""returnMsg"":""连接服务器失败""}";
            string sql = @"select * from LF_MessageRecord where mobile='"+ mobile +"' order by operatortime desc;";
            sql += Environment.NewLine + @"select * from userinfo where mobile='"+ mobile +"';";
            DataSet ds;
            DatabaseHelper.GetData(sql,out ds);
            if (ds == null)
            {
                return result;
            }
            if (ds.Tables[0].Rows.Count > 0)
            {
                DateTime time;
                if (DateTime.TryParse(ds.Tables[0].Rows[0]["operatortime"].ToString(), out time))
                {
                    int flag = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")).CompareTo(time.AddMinutes(10));
                    if (flag == 0 || flag == -1)
                    {
                        if (ds.Tables[1].Rows.Count > 0)
                        {
                            result = @"{""returnCode"":""0005"",""returnMsg"":""该手机号已注册""}";
                        }
                        else
                        {
                            if (msg == ds.Tables[0].Rows[0]["msg"].ToString())
                            {
                                string SQL = @"INSERT INTO USERINFO (MOBILE,USERNAME,PWD,REGISTERDATE) values ('"+ mobile +"','"+ mobile +"','"+ pwd +"','"+ DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") +"')";
                                if (DatabaseHelper.Post(SQL))
                                {
                                    result = @"{""returnCode"":""0000"",""returnMsg"":""注册成功""}";
                                }
                            }
                            else
                            {
                                result = @"{""returnCode"":""0006"",""returnMsg"":""验证码不正确""}";
                            }
                        }
                    }
                    else
                    {
                        result = @"{""returnCode"":""0004"",""returnMsg"":""验证码失效""}";
                    }
                }
            }
            else
            {
                result = @"{""returnCode"":""0003"",""returnMsg"":""手机号与验证码手机号不一致""}";
            }
            return result;
        }
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpPost]
        public string Login(string username, string password)
        {
            string result = @"{""returnCode"":""9999"",""returnMsg"":""连接服务器失败""}";
            string sql = @"select * from userinfo where mobile='"+ username +"'";
            DataSet ds;
            DatabaseHelper.GetData(sql,out ds);
            if (ds == null)
            {
                return result;
            }
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["mobile"].ToString() == username && ds.Tables[0].Rows[0]["pwd"].ToString() == password)
                {
                    result = @"{""returnCode"":""0000"",""returnMsg"":""登录成功""}";
                }
                else
                {
                    result = @"{""returnCode"":""0002"",""returnMsg"":""用户名或密码错误""}";
                }
            }
            else result = @"{""returnCode"":""0001"",""returnMsg"":""该用户不存在""}";
            return result;
        }
    }
}