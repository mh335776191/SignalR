using System;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Microsoft.AspNet;
using Owin;
using SignalRHelper;

[assembly: OwinStartup(typeof(SignalRWebText.Startup))]//可以灵活配置，配置需要在appsettings里新增key为owin:AppStartup的节点

namespace SignalRWebText
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var userIdProvider = new CookiesUserIdProvider();
            GlobalHost.DependencyResolver.Register(typeof(IUserIdProvider), () => userIdProvider);//获取用户表示
                                                                                                  //GlobalHost.DependencyResolver.UseStackExchangeRedis("127.0.0.1", 6379, "123456", "signalR");


            //GlobalHost.Configuration.DisconnectTimeout = TimeSpan.FromSeconds(30);
            // 有关如何配置应用程序的详细信息，请访问 https://go.microsoft.com/fwlink/?LinkID=316888 
            //PersistentConnection 持久链接
            app.MapSignalR<MyPersistentConnection>("/MyPersistentConnection");

            //1. JSONP的模式
            //app.MapSignalR<MyPresitentConnection1>("/myPreConnection1", new Microsoft.AspNet.SignalR.ConnectionConfiguration()
            //{
            //    EnableJSONP = true
            //});

            // 2.Cors的模式（需要Nuget安装：Microsoft.Owin.Cors程序集）
            app.Map("/MyHub", (map) =>
            {
                map.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
                map.RunSignalR(new HubConfiguration { EnableJSONP = true });
            });
            //app.MapSignalR("/MyHub", new HubConfiguration { EnableJSONP = true }); //默认将 SignalR 集线器映射到“/signalr”处的应用生成器管道。 
            //3. JSONP和Cors同时开启
            //app.Map("/myPreConnection1", (map) =>
            //{
            //    //1. 开启 jsonp
            //    map.RunSignalR<MyPresitentConnection1>(new Microsoft.AspNet.SignalR.HubConfiguration() { EnableJSONP = true });
            //    //2. 开启cors
            //    map.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            //});
        }
    }
}
