using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalRHelper
{
    public class CookiesUserIdProvider : IUserIdProvider
    {
        public string GetUserId(IRequest request)
        {
            //if (request.GetHttpContext().Request.Cookies["UserInfo"] != null)
            //{
            //    return request.GetHttpContext().Request.Cookies["UserInfo"].Value;
            //}
            return request.QueryString["userKey"];
            //return "";
        }
    }
}
