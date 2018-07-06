using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Apache.NMS;
using Apache.NMS.ActiveMQ;

namespace SendMessage
{
    public partial class Form1 : Form
    {
        private static string USERNAME = "admin"; // 默认的连接用户名  
        private static string PASSWORD = "admin"; // 默认的连接密码  
        private static string BROKEURL = "tcp://123.206.28.222:61616"; // 默认的连接地址  
        public Form1()
        {
            InitializeComponent();
            InitProducer();
        }
        public void InitProducer()
        {
            ConnectionFactory connectionFactory; // 连接工厂  
            Connection connection = null; // 连接  
            Session session; // 会话 接受或者发送消息的线程  
           // Destination destination; // 消息的目的地  
            MessageProducer messageProducer; // 消息生产者  
        }
    }
}
