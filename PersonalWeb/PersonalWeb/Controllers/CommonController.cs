using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using ThoughtWorks.QRCode.Codec;
using System.Web;

namespace PersonalWeb.Controllers
{
    public class CommonController : ApiController
    {
        // GET api/common
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/common/5
        public string GetQRCode(string url, string logoUrl)
        {
            string result = @"{""returnCode"":""9999"",""returnMsg"":""请输入内容""}";
            if (string.IsNullOrEmpty(url))
            {
                return result;
            }
            string timeStr = DateTime.Now.ToFileTime().ToString();
            Bitmap bitmap;
            if (string.IsNullOrEmpty(logoUrl))
            {
                bitmap = QRCodeHelper.Create(url);
            }
            else
            {
                bitmap = QRCodeHelper.CreateQRCodeWithLogo(url, logoUrl);
            }
            string fileName = System.Web.HttpContext.Current.Server.MapPath("~") + "\\Images\\" + timeStr + ".jpg";
            bitmap.Save(fileName);//保存位图
            string imageUrl = "../../Images/" + timeStr + ".jpg";//显示图片  
            return @"{""returnCode"":""0000"",""returnMsg"":""" + imageUrl + @"""}";
        }

        // POST api/common
        public void Post([FromBody]string value)
        {
        }

        // PUT api/common/5
        public void Put(int id, [FromBody]string value)
        {
        }
        /// <summary>
        /// 
        /// </summary>
        [HttpPost]
        public string UpFile()
        {
            string result = @"{""returnCord"":""9999"",""returnMsg"":""服务器连接失败！""}";
            try
            {
                string folder = DateTime.Now.ToString("yyyy-MM-dd");
                HttpFileCollection files = HttpContext.Current.Request.Files;
                HttpPostedFile file = null;
                if (files.Count != 0)
                {
                    file = files[0];//这里只有一个文件   
                    if (!string.IsNullOrEmpty(file.FileName))
                    {
                        if (System.IO.Directory.Exists(HttpContext.Current.Server.MapPath("/upImages/") + folder))
                        {
                            if (System.IO.File.Exists(HttpContext.Current.Server.MapPath("/upImages/" + folder + "/") + file.FileName))
                            {
                                System.IO.File.Delete(HttpContext.Current.Server.MapPath("/upImages/" + folder + "/") + file.FileName);
                                file.SaveAs(HttpContext.Current.Server.MapPath("/upImages/" + folder + "/") + file.FileName);
                            }
                            else
                            {
                                file.SaveAs(HttpContext.Current.Server.MapPath("/upImages/" + folder + "/") + file.FileName);
                            }
                        }
                        else
                        {
                            System.IO.Directory.CreateDirectory(HttpContext.Current.Server.MapPath("/upImages/") + folder);
                            file.SaveAs(HttpContext.Current.Server.MapPath("/upImages/" + folder + "/") + file.FileName);
                        }
                    }
                }
                result = @"{""returnCode"":""0000"",""returnMsg"":""" + (HttpContext.Current.Server.MapPath("/upImages/" + folder + "/") + file.FileName).Replace("\\", "\\\\") + @"""}";
                return result;
            }
            catch (Exception ex)
            {
                result = @"{""returnCode"":""0001"",""returnMsg"":"""+ ex.Message +@"""}";
                return result;
            }
            
        }
        // DELETE api/common/5
        public void Delete(int id)
        {
        }
    }
}
