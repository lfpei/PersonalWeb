using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;
using SendMessage;
using Apache.NMS.ActiveMQ.Commands;
using System.Diagnostics;
using System.Runtime.Serialization.Formatters.Binary;

namespace NmsProducerClasses
{
    class Program
    {
        static void Main(string[] args)
        {
            var consumer = new ActiveMQConsumer();
            consumer.BrokerUri = @"tcp://192.168.13.88:61616/";
            consumer.UserName = "admin";
            consumer.Password = "admin";
            consumer.QueueName = "TestQueueName";
            consumer.MQMode = MQMode.Queue;

            consumer.OnMessageReceived = (msg) =>
            {
                var bytesMessage = msg as ActiveMQBytesMessage;
                if (bytesMessage != null)
                {
                    var buffer = new byte[bytesMessage.BodyLength];
                    bytesMessage.WriteBytes(buffer);
                    var result = buffer.ToObject<DataCenterMessage>();
                    Debug.WriteLine(result);
                }
            };

            consumer.OnDataCenterMessageReceived = (msg) =>
            {
                Debug.Write(msg);
            };

            consumer.Open();
            consumer.StartListen();
        }
    }
    /// <summary>
    /// 扩展方法类
    /// </summary>
    public static class ExtendMethods
    {
        /// <summary>
        /// 将对象转换为bytes
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>bytes</returns>
        public static byte[] ToBytes<T>(this T obj) where T : class
        {
            if (obj == null)
                return null;
            using (var ms = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(ms, obj);
                return ms.GetBuffer();
            }
        }

        /// <summary>
        /// 将bytes转换为对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static T ToObject<T>(this byte[] bytes) where T : class
        {
            if (bytes == null)
                return default(T);
            using (var ms = new MemoryStream(bytes))
            {
                var formatter = new BinaryFormatter();
                return formatter.Deserialize(ms) as T;
            }
        }
    }
}