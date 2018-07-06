using System;
using System.Collections.Generic;
using System.Text;
using Apache.NMS;
using Apache.NMS.ActiveMQ;
using System.IO;
using System.Xml.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using SendMessage;

namespace Publish
{
    class Program
    {
        static void Main(string[] args)
        {
            var producer = new ActiveMQProducer();
            producer.BrokerUri = @"tcp://192.168.13.88:61616/";
            producer.UserName = "admin";
            producer.Password = "admin";
            producer.QueueName = "TestQueueName";
            producer.MQMode = MQMode.Queue;

            producer.Open();
            var message = new DataCenterMessage()
            {
                customerid = "123",result = true
            };

            //发送到队列, Put对象类必须使用[Serializable]注解属性
            producer.Put(message);
        }

    }
}