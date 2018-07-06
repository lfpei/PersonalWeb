using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Drawing;
using System.IO;
using System.Data.SqlClient;
using System.Data;
using System.Web.Script.Serialization;
using System.Reflection;
using System.Threading;
using System.Diagnostics;
using System.Runtime.InteropServices;
namespace Exercise
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());
            //WeatherInterface.WeatherWebService weather = new WeatherInterface.WeatherWebService();
            //string[] ss = weather.getWeatherbyCityName("上海");
            //string s = "";
            //Product product = new Product();
            //ITest person = new Person();
            //product.zhixing(person);
            //try
            //{
            //string json = @"{""batNo"":""123"",""ss"":"""",""loanList"":{""contract"":""1"",""type"":""01""}}";
            //JObject obj = (JObject)JsonConvert.DeserializeObject(json);
            //ContractInfo contracts = JSONStringToList<ContractInfo>(obj["loanList"].ToString());
            //    JArray list = (JArray)obj["loanList"];
            //}
            //catch (Exception ex)
            //{
            //    string s = ex.Message;
            //}
            //DataTable dt = new DataTable();
            //dt.Columns.Add("name1",typeof(string));
            //dt.Columns.Add("name2",typeof(string));
            //dt.Columns.Add("name3",typeof(string));
            //for (int i = 0; i < 3; i++)
            //{
            //    DataRow dr = dt.NewRow();
            //    dr["name1"] = "zhangsan";
            //    dr["name2"] = i;
            //    dr["name3"] = "nan";
            //    dt.Rows.Add(dr);
            //}
            //DataRow[] drs = dt.Select("max(name2)");
            //decimal num = 632.385628939211m;
            //num = Math.Round(num,2);
            //string date = "2018-01-29";
            //string date1 = "2018-02-28";
            //Dictionary<string, int> dic = GetTerm(date,date1);
            //string datenow = Convert.ToDateTime(date).AddMonths(1).ToString();
            //string createAFactory = "CreateBFactory";
            //IFactory af = (IFactory)Assembly.Load("Exercise").CreateInstance("Exercise." + createAFactory);//反射调用DLL中的方法
            //IShop ashop = af.CreateShop();
            //int prica = ashop.Calc();
            //Random random = new Random();
            //for (int i = 0; i < 10; i++)
            //{
            //    Beep(random.Next(10000), 100);
            //}
            //ShowWindow(Form.ActiveForm.Handle, 2);
            //string ss = "";
            DataTable dt = new DataTable();
            dt.Columns.Add("name",typeof(string));
            dt.Columns.Add("name1",typeof(string));
            dt.Columns.Add("age",typeof(int));
            dt.Columns.Add("bool",typeof(bool));
            for (int i = 0; i < 10; i++)
            {
                DataRow dr = dt.NewRow();
                dr["name"] = i.ToString();
                dr["name1"] = i.ToString();
                dr["age"] = i;
                if (i % 2 == 0)
                {
                    dr["bool"] = true;
                }
                dt.Rows.Add(dr);
            }
            DataRow[] drs = dt.Select("bool = true");
            DataTable dtNew = drs.CopyToDataTable(); 
        }


        [DllImport("kernel32.dll")]
        public static extern bool Beep(int frequency, int duration);


        [DllImport("user32.dll", EntryPoint = "ShowWindow", CharSet = CharSet.Auto)]
        public static extern int ShowWindow(IntPtr hwnd, int nCmdShow);


        [DllImport("User32.dll", CharSet = CharSet.Unicode)]
        public static extern int MessageBox(IntPtr hWnd, String text, String caption, uint type);
        [Conditional("DEBUG")]
        private static void DisplayRunningMessage(ref string s)
        {
             s += "开始运行Main子程序。当前时间是" + DateTime.Now;
        }

        [Conditional("DEBUG")]
        [Obsolete]
        private static void DisplayDebugMessage(ref string s)
        {
            s += "开始Main子程序";
        }


        public static ContractInfo JSONStringToList<ContractInfo>(string JsonStr)
        {
            JavaScriptSerializer Serializer = new JavaScriptSerializer();
            ContractInfo objs = Serializer.Deserialize<ContractInfo>(JsonStr);
            return objs;
        }
        /// <summary>
        /// 获取期限
        /// </summary>
        /// <param name="startDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        private static Dictionary<string, int> GetTerm(string startDate, string endDate)
        {
            Dictionary<string, int> dic = new Dictionary<string, int>();
            DateTime start = Convert.ToDateTime(startDate);
            DateTime end = Convert.ToDateTime(endDate);
            if (end.CompareTo(start) == -1)
            {
                return dic;
            }
            int months = (end.Year - start.Year) * 12 + (end.Month - start.Month);
            int days = end.Day - start.Day;
            if (days < 0)
            {
                months = months - 1;
                days = DateTime.DaysInMonth(end.Year, end.AddMonths(-1).Month) + days;
            }
            dic.Add("Month", months);
            dic.Add("Day", days);
            return dic;
        }
    }
    public class ContractInfo
    {
        public string contract { get; set; }
        public int type { get; set; }
    }

    class Test
    {
        static Test(){}
        public static string s = Get("初始化静态变量");
        public static string Get(string input)
        {
            
            return input;
        }
    }

    public abstract class IShop
    {
        public abstract int Calc();
    }
    class AShop : IShop
    {
        public override int Calc()
        {
            return 1 + 2;
        }
    }
    class BShop : IShop
    {
        public override int Calc()
        {
            return 2 + 2;
        }   
    }
    public abstract class IFactory
    {
        public abstract IShop CreateShop();
    }
    class CreateAFactory : IFactory
    {
        public override IShop CreateShop()
        {
            return new AShop();
        }
    }
    class CreateBFactory : IFactory
    {
        public override IShop CreateShop()
        {
            return new BShop();
        }
    }
}
