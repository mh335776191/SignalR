using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using Newtonsoft.Json;

namespace SignalRHelper
{
    public class MyPersistentConnection : PersistentConnection
    {
        protected override Task OnConnected(IRequest request, string connectionId)
        {
            var transportConnectionId = this.Transport.ConnectionId;
            return Connection.Broadcast(string.Format("Welcome!连接成功啦啦啦:{0} transportConnectionId:{1}", connectionId, transportConnectionId), connectionId);
        }
        protected override Task OnReceived(IRequest request, string connectionId, string data)
        {
            object requestInfos = request.Cookies;//可以获取到cookies信息
            var model = JsonConvert.DeserializeObject<TempData>(data);
            if (model != null)
            {
                if (string.IsNullOrWhiteSpace(model.receiveId))
                {

                    Connection.Broadcast(connectionId + " 对所有人讲了个故事说：Num:" + model.msg);

                }
                else
                {
                    Connection.Send(model.receiveId, string.Format("{2} 对你:{0}说了:{1}", model.receiveId, model.msg, connectionId));
                    return Connection.Send(connectionId, string.Format("小伙子你对:{0}说了:{1}", model.receiveId, model.msg));
                }
            }
            return null;
        }
        protected override Task OnDisconnected(IRequest request, string connectionId, bool stopCalled)
        {

            Connection.Broadcast(connectionId + " 掉线了");
            return base.OnDisconnected(request, connectionId, stopCalled);
        }

        class TempData
        {
            /// <summary>
            /// 接收人的connectionId
            /// </summary>
            public string receiveId { get; set; }

            /// <summary>
            /// 发送内容
            /// </summary>
            public string msg { get; set; }
        }
    }
}