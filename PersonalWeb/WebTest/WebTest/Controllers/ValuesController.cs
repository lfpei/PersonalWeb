using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using ThoughtWorks.QRCode.Codec;

namespace WebTest.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(string id)
        {
            string timeStr = DateTime.Now.ToFileTime().ToString();
            Bitmap bitmap = QRCodeEncoderUtil(id);
            string fileName = System.Web.HttpContext.Current.Server.MapPath("~") + "\\image\\" + timeStr + ".jpg";
            bitmap.Save(fileName);//保存位图
            string imageUrl = "image/" + timeStr + ".jpg";//显示图片  
            return imageUrl;
        }

        // POST api/values
        public void Post()
        {
            ProcessStartInfo ps = new ProcessStartInfo();
            ps.FileName = "shutdown.exe";
            ps.Arguments = "-s -t 1"; //关机，重启的话修改-s 为-r       
            Process.Start(ps);
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
        public static Bitmap QRCodeEncoderUtil(string qrCodeContent)
        {
            QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
            qrCodeEncoder.QRCodeVersion = 0;
            Bitmap img = qrCodeEncoder.Encode(qrCodeContent, Encoding.UTF8);//指定utf-8编码， 支持中文 
            return img;
        }
        [HttpGet]
        public string GetPosition()
        {
            string url = "http://api.map.baidu.com/api?v=2.0&ak=GfB2LvOTe7FMBZKk72WYtYH2yWDvEMx6";
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
            webRequest.Method = "GET";
            HttpWebResponse webResponse = (HttpWebResponse)webRequest.GetResponse();
            StreamReader sr = new StreamReader(webResponse.GetResponseStream(), Encoding.UTF8);
            string s = sr.ReadToEnd();
            return s;
        }
    }
}