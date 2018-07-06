using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebServiceDemo
{
    /// <summary>
    /// Handler1 的摘要说明
    /// </summary>
    public class Handler1 : IHttpHandler
    {
        
        public void ProcessRequest(HttpContext context)
        {
            //WeatherInterface.WeatherWebService weather = new WeatherInterface.WeatherWebService();
            //string[] s = weather.getWeatherbyCityName("上海");
            //string result = @"{""city"":""上海"",""time"":"""+ s[4] +@""",""T"":"""+ s[5] +@"""}";
            //context.Response.Write(result);
            Localtest.ServiceTest local = new Localtest.ServiceTest();
            string s = local.Add(1, 2).ToString() ;
            context.Response.Write(s);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}