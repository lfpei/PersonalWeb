using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Exercise
{
    public partial class Form1 : Form
    {
        public Form1() 
        { 
            InitializeComponent(); 
        }
        [DllImport("user32.dll", EntryPoint = "ShowWindow", CharSet = CharSet.Auto)]
        public static extern int ShowWindow(IntPtr hwnd, int nCmdShow);
        private void Form1_Load(object sender, EventArgs e)     
        {
            //try      
            //{           
            //    webBrowser1.Url = new Uri(Path.Combine(Application.StartupPath, "..\\..\\test.html"));
            //}       
            //catch (Exception ex)       
            //{         
            //    MessageBox.Show(ex.Message, "异常", MessageBoxButtons.OK, MessageBoxIcon.Error);       
            //}     
        }     
        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {      
            //这里传入x、y的值，调用JavaScript脚本       
            //webBrowser1.Document.InvokeScript("setLocation", new object[] { 116.404, 39.915 });     
        }
        [DllImport("shell32.dll", EntryPoint = "ShellExecute", CharSet = CharSet.Auto)]
        public static extern int ShellExecute(IntPtr hwnd, string lpOperation, string lpFile, string lpParameters, string lpDirectory, int nShowCmd);
        private void button1_Click(object sender, EventArgs e)
        {
            //webBrowser1.Document.InvokeScript("myFun");
            //ShowWindow(Form.ActiveForm.Handle, 2);
            ShellExecute(Form.ActiveForm.Handle, "Open", "www.baidu.com", "", "", 1);
        }
    }
}
