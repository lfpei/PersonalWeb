using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using ThoughtWorks.QRCode.Codec;

namespace WebTest
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            using (var ms = new MemoryStream())
            {
                string stringtest = "http://yxdashen.95php.com/";
                //GetQRCode(stringtest, ms);
                Response.ContentType = "image/Png";
                Response.OutputStream.Write(ms.GetBuffer(), 0, (int)ms.Length);
                Image img = Image.FromStream(ms); string filename = DateTime.Now.ToString("yyyymmddhhmmss");
                string path = Server.MapPath("~/image/") + filename + ".png"; 
                img.Save(path); Response.End();
            }
        }
        /// <summary>        
        /// 获取二维码        
        /// </summary>        
        /// <param name="strContent">待编码的字符</param>        
        /// <param name="ms">输出流</param>        
        ///<returns>True if the encoding succeeded, false if the content is empty or too large to fit in a QR code</returns>        
        //public static bool GetQRCode(string strContent, MemoryStream ms)
        //{
        //    ErrorCorrectionLevel Ecl = ErrorCorrectionLevel.M; //误差校正水平             
        //    string Content = strContent;//待编码内容            
        //    QuietZoneModules QuietZones = QuietZoneModules.Two;
        //    //空白区域             
        //    int ModuleSize = 12;//大小            
        //    var encoder = new QrEncoder(Ecl);
        //    QrCode qr;
        //    if (encoder.TryEncode(Content, out qr))//对内容进行编码，并保存生成的矩阵            
        //    {
        //        var render = new GraphicsRenderer(new FixedModuleSize(ModuleSize, QuietZones));
        //        render.WriteToStream(qr.Matrix, ImageFormat.Png, ms);
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //    return true;
        //}
        //protected void Button1_Click(object sender, EventArgs e) 
        //{ 
        //    //create_two(); 
        //}
        private void create_two(string nr) 
        { 
            Bitmap bt;
            string enCodeString = "http://" + nr; 
            QRCodeEncoder qrCodeEncoder = new QRCodeEncoder(); 
            bt = qrCodeEncoder.Encode(enCodeString, Encoding.UTF8); 
            string filename = DateTime.Now.ToString("yyyymmddhhmmss"); 
            //string path = Server.MapPath("~/image/") + filename + ".jpg";
            string path = @"C:\Users\amarsoft\Desktop\" + filename + ".jpg";
            Response.Write(path);
            bt.Save(path);
            this.Image1.ImageUrl = @"C:\Users\amarsoft\Desktop\" + filename + ".jpg";
        }

        protected void Button1_Click1(object sender, EventArgs e)
        {
            create_two(this.TextBox1.Text);
        }
    }
}