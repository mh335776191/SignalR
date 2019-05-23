using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static ConcurrentBag<object> bg = new ConcurrentBag<object>();
        static int _connectionCount = 0;
        static void Main(string[] args)
        {
            //var connection = new Connection("http://signalrtest2012.com/myhub");
            //connection.Start().Wait();
            Console.WriteLine("线程个数:");
            int threadNum = int.Parse(Console.ReadLine());
            Console.WriteLine("每条线程的连接数:");
            int count = int.Parse(Console.ReadLine());
            Test(threadNum, count);
            var c = bg.Count;
            Console.ReadKey();
        }
        async static void Test(int threadNum, int count)
        {
            for (int n = 0; n < threadNum; n++)
            {
                ThreadPool.QueueUserWorkItem(async (state) =>
               {
                   for (int i = 0; i < count; i++)
                   {
                       try
                       {
                           var hubConn = new HubConnection("http://signalrtest2012.com/MyHub");
                           var hubProxy = hubConn.CreateHubProxy("MyHub");
                           await hubConn.Start();
                           hubProxy.Invoke("SendMsg", "第几个人开始打招呼了:" + i);
                           hubProxy.On("SendMsg", (c) => { Console.WriteLine("获取到回调数据:" + c); });
                           _connectionCount++;
                           Console.WriteLine(_connectionCount);
                           //bg.Add(hubConn);
                           //Console.WriteLine("已成功创建链接数：" + bg.Count);
                       }
                       catch (Exception ex)
                       {
                           Console.WriteLine(ex);
                       }
                   }
               });
            }

        }
    }
}
