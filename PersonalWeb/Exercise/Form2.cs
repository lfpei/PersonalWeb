using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Exercise
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        //姓名
        public static string XM = "";
        //年龄
        public static string nl = "";
        //性别
        public static string XB = "";
        //身高
        public static string SG = "";
        //政治面貌
        public static string mm = "";
        //民族
        public static string MZ = "";
        //学历
        public static string XL = "";
        //婚姻状况
        public static string HK = "";
        //所学专业
        public static string ZY = "";
        //工作经验
        public static string GZJY = "";
        //在职单位
        public static string ZZDW = "";
        //在职职位
        public static string ZZZW = "";
        //工作经历
        public static string GZJL = "";
        //要求月薪
        public static string YX = "";
        //工作性质
        public static string GZXZ = "";
        //求职意向
        public static string QZYX = "";
        //具体职务
        public static string JTZW = "";
        //期望工作地
        public static string QWGZD = "";
        //教育情况,语言水平,技术专长
        public static string QT = "";
        private void button1_Click(object sender, EventArgs e)
        {
            label1.Text = "正在采集数据……";
            //遍历数据的页数 
            //for (int i = 1; i <= 50; i++)
            //{
            //    CJ("http://www.xcjob.cn/renli.asp?pageno=" + i);
            //}
            CJ("https://www.aliyun.com/jiaocheng/630679.html");
            label1.Text = "恭喜你采集完成!";
            MessageBox.Show("恭喜你采集完成!");
        }
        //采集数据
        private void CJ(string Url)
        {
            //获得页面源文件(Html)
            string strWebContent = YM(Url);
            //按照Html里面的标签 取出和数据有关的那段源码
            int iBodyStart = strWebContent.IndexOf("<body", 0);
            int aaa = strWebContent.IndexOf("关键字:", iBodyStart);
            int iTableStart = strWebContent.IndexOf("<table", aaa);
            int iTableEnd = strWebContent.IndexOf("</table>", iTableStart);
            string strWeb = strWebContent.Substring(iTableStart, iTableEnd - iTableStart);
            //生成HtmlDocument
            HtmlElementCollection htmlTR = HtmlTR_Content(strWeb, "tr");
            foreach (HtmlElement tr in htmlTR)
            {
                try
                {
                    //姓名
                    XM = tr.GetElementsByTagName("a")[0].InnerText;
                    //获得详细信息页面的网址
                    string a = tr.GetElementsByTagName("a")[0].GetAttribute("href").ToString();
                    a = "http://www.xcjob.cn" + a.Substring(11);
                    Content(a);
                }
                catch { }
            }
        }
        //采集详细数据
        private void Content(string URL)
        {
            try
            {
                string strWebContent = YM(URL);
                //按照Html里面的标签 取出和数据有关的那段源码
                int iBodyStart = strWebContent.IndexOf("<body", 0);
                int iTableStart = strWebContent.IndexOf("浏览次数", iBodyStart);
                int iTableEnd = strWebContent.IndexOf("<table", iTableStart);
                int dd = strWebContent.IndexOf("</table>", iTableEnd);
                string strWeb = strWebContent.Substring(iTableEnd, dd - iTableEnd + 8);
                HtmlElementCollection htmlTR = HtmlTR_Content(strWeb, "table");
                foreach (HtmlElement tr in htmlTR)
                {
                    try
                    {
                        //年龄
                        nl = tr.GetElementsByTagName("tr")[1].GetElementsByTagName("td")[1].InnerText;
                        //性别
                        string XB_SG = tr.GetElementsByTagName("tr")[1].GetElementsByTagName("td")[3].InnerText;
                        XB = XB_SG.Substring(0, 1);
                        //身高
                        SG = XB_SG.Substring(11);
                        //政治面貌
                        mm = tr.GetElementsByTagName("tr")[2].GetElementsByTagName("td")[1].InnerText;
                        //民族
                        MZ = tr.GetElementsByTagName("tr")[2].GetElementsByTagName("td")[3].InnerText;
                        //学历
                        XL = tr.GetElementsByTagName("tr")[3].GetElementsByTagName("td")[1].InnerText;
                        //婚烟状况
                        HK = tr.GetElementsByTagName("tr")[3].GetElementsByTagName("td")[3].InnerText;
                        //所学专业
                        ZY = tr.GetElementsByTagName("tr")[5].GetElementsByTagName("td")[1].InnerText;
                        //工作经验
                        GZJY = tr.GetElementsByTagName("tr")[5].GetElementsByTagName("td")[3].InnerText;
                        //在职单位
                        ZZDW = tr.GetElementsByTagName("tr")[6].GetElementsByTagName("td")[1].InnerText;
                        //在职职位
                        ZZZW = tr.GetElementsByTagName("tr")[6].GetElementsByTagName("td")[3].InnerText;
                        //工作经历
                        GZJY = tr.GetElementsByTagName("tr")[7].GetElementsByTagName("td")[1].InnerText;
                        //要求月薪
                        YX = tr.GetElementsByTagName("tr")[9].GetElementsByTagName("td")[1].InnerText;
                        //工作性质
                        GZXZ = tr.GetElementsByTagName("tr")[9].GetElementsByTagName("td")[3].InnerText;
                        //求职意向
                        QZYX = tr.GetElementsByTagName("tr")[10].GetElementsByTagName("td")[1].InnerText;
                        //具体职务
                        JTZW = tr.GetElementsByTagName("tr")[10].GetElementsByTagName("td")[3].InnerText;
                        //期望工作地
                        QWGZD = tr.GetElementsByTagName("tr")[11].GetElementsByTagName("td")[1].InnerText;
                        //教育情况,语言水平,技术专长
                        QT = tr.GetElementsByTagName("tr")[13].GetElementsByTagName("td")[1].InnerText;
                        insert();
                    }
                    catch
                    { }
                }
            }
            catch { }
        }
        //将数据插入数据库 
        private void insert()
        {
            try
            {
                string str = "Provider=Microsoft.Jet.OleDb.4.0;Data Source=Data.mdb";
                string sql = "insert into 人才信息 (姓名,年龄,性别,身高,政治面貌,民族,学历,婚烟状况,所学专业,";
                sql += "工作经验,在职单位,在职职位,工作经历,要求月薪,工作性质,求职意向,具体职务,期望工作地,其他) values ";
                sql += "('" + XM + "'," + nl + ",'" + XB + "','" + SG + "','" + mm + "','" + MZ + "','" + XL + "','" + HK + "','" + ZY + "','" + GZJY + "','" + ZZDW + "','" + ZZZW + "',";
                sql += "'" + GZJY + "','" + YX + "','" + GZXZ + "','" + QZYX + "','" + JTZW + "','" + QWGZD + "','" + QT + "')";
                //OleDbConnection con = new OleDbConnection(str);
                //OleDbCommand com = new OleDbCommand(sql, con);
                //con.Open();
                //com.ExecuteNonQuery();
                //con.Close();
            }
            catch { }
        }
        //返回一个HtmlElementCollection,然后进行查询内容
        private HtmlElementCollection HtmlTR_Content(string strWeb, string tj)
        {
            try
            {
                //生成HtmlDocument
                WebBrowser webb = new WebBrowser();
                webb.Navigate("about:blank");
                //window.document返回一个htmldocument对象,表示对一个html文档的操作
                //htmldocument对象是在xmldocument基础上建立的,具有xmldocument的一切方法属性
                HtmlDocument htmldoc = webb.Document.OpenNew(true);
                htmldoc.Write(strWeb);
                HtmlElementCollection htmlTR = htmldoc.GetElementsByTagName(tj);
                return htmlTR;
            }
            catch { return null; }
        }

        //获得网址原代码
        private string YM(string Url)
        {
            string strResult = "";
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
                request.Method = "GET";
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream streamReceive = response.GetResponseStream();
                Encoding encoding = Encoding.GetEncoding("GB2312");
                StreamReader streamReader = new StreamReader(streamReceive, encoding);
                strResult = streamReader.ReadToEnd();
            }
            catch { }
            return strResult;
        }
    }
}
