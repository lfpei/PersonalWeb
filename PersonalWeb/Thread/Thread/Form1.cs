using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NewThread
{
    public partial class Form1 : Form
    {
        public delegate void ThreadCallBackDelegate(int msg);
        public event ThreadCallBackDelegate ThreadEvent;
        public Form1()
        {
            InitializeComponent();
        }
        Thread thread;
        string ss = string.Empty;
        int aa = 0;
        bool flag = true;
        private void btstart_Click(object sender, EventArgs e)
        {
            ThreadEvent += show;
            thread = new Thread(new ThreadStart(jisuan));
            thread.Start();
            ss = "线程名字：" + thread.Name + Environment.NewLine + "线程属性：" + thread.Priority + Environment.NewLine + "线程状态：" + thread.ThreadState;
            this.txtContent.Text = ss;
        }
        public void show(int time)
        {
            this.textBox1.Text = aa.ToString();
        }
        private void jisuan()
        {
            while(flag)
            {
                if (thread == null)
                {
                    thread = new Thread(new ThreadStart(jisuan));
                    thread.Start();
                }
                else if (thread.ThreadState == System.Threading.ThreadState.Unstarted)
                {
                    thread.Start();
                }
                else if (thread.ThreadState == System.Threading.ThreadState.Stopped)
                {
                    thread = new Thread(new ThreadStart(jisuan));
                    thread.Start();
                }
                else if (thread.ThreadState == System.Threading.ThreadState.Aborted)
                {
                    thread = new Thread(new ThreadStart(jisuan));
                    thread.Start();
                }
                aa += 1;
                ThreadEvent(aa);
            }
        }

        private void btend_Click(object sender, EventArgs e)
        {
            try
            {
                if (thread == null)
                {
                    return;
                }
                if (thread.IsAlive == true)
                {
                    thread.Abort();
                    ss = "线程名字：" + thread.Name + Environment.NewLine + "线程属性：" + thread.Priority + Environment.NewLine + "线程状态：" + thread.ThreadState;
                }
                else
                {
                    ss = "线程名字：" + thread.Name + Environment.NewLine + "线程属性：" + thread.Priority + Environment.NewLine + "线程状态：" + thread.ThreadState;
                    return;
                }
                this.txtContent.Text = ss;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
