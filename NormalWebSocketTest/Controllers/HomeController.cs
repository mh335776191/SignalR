using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.WebSockets;

namespace NormalWebSocketTest.Controllers
{
    public class HomeController : Controller
    {
        public void NormalSocket()
        {
            var httpContext = this.HttpContext;
            if (httpContext.IsWebSocketRequest)
            {
                httpContext.AcceptWebSocketRequest(WebSocketRequestHandler);
            }
        }
        private async Task WebSocketRequestHandler(AspNetWebSocketContext webSocketContext)
        {

            WebSocket webSocket = webSocketContext.WebSocket;
            const int maxMessageSize = 2048;
            var receivedDataBuffer = new ArraySegment<Byte>(new Byte[maxMessageSize]);
            var cancellationToken = new CancellationToken();
            while (webSocket.State == WebSocketState.Open)
            {
                WebSocketReceiveResult webSocketReceiveResult =
                  await webSocket.ReceiveAsync(receivedDataBuffer, cancellationToken);

                if (webSocketReceiveResult.MessageType == WebSocketMessageType.Close)
                {
                    await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure,
                      String.Empty, cancellationToken);
                }
                else
                {
                    byte[] payloadData = receivedDataBuffer.Array.Where(b => b != 0).ToArray();

                    string receiveString =
                      System.Text.Encoding.UTF8.GetString(payloadData, 0, payloadData.Length);


                    int i = 0;

                    while (i < 100)
                    {
                        var newString =
                    String.Format(@"服务端收到数据, " + i + " :" + receiveString + " ! Time {0}\t\n", DateTime.Now.ToString());
                        Byte[] bytes = System.Text.Encoding.UTF8.GetBytes(newString);
                        await webSocket.SendAsync(new ArraySegment<byte>(bytes),
                          WebSocketMessageType.Text, true, cancellationToken);
                        i++;
                        Thread.Sleep(i * 100);
                    }
                }
            }
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}