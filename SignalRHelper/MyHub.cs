using System;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace SignalRHelper
{ 
    [HubName("MyHub")]
    public class MyHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        private static int _UserCount = 0;
        public int Sum(int a, int b)
        {
            //Clients.User("userId").Message("dynamic类型的  调用匹配到的客户端的方法");
            return a + b;
        }
        public void GetOnlineUserNum()
        {
            System.Threading.Thread.Sleep(2000);
            Clients.All.ClientGetOnlineUserNum(_UserCount);
        }
        public void SendMsg(string text)
        {
            Clients.All.SendMsg(text + "：来自服务器 " + GetWebRemoteIp());
        }
        public override Task OnConnected()
        {
            _UserCount += 1;
            Clients.All.NewConnected(Context.ConnectionId);
            return base.OnConnected();
        }
        public override Task OnDisconnected(bool stopCalled)
        {
            if (_UserCount > 0)
                _UserCount -= 1;
            Clients.All.UserOutLine(Context.ConnectionId + " 时间：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            return base.OnDisconnected(stopCalled);
        }
        public override Task OnReconnected()
        {
            Clients.All.ReconnectedMsg(Context.ConnectionId);
            return base.OnReconnected();
        }
        /// <summary>
        /// 获取Web远程Ip
        /// </summary>
        /// <returns></returns>
        private static string GetWebRemoteIp()
        {
            return HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] ?? HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
        } 
    }
}
