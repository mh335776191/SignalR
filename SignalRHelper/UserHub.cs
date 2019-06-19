using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalRHelper
{
   public class UserHub: Hub
    {
        #region test
        public void MsgToUser(string msg)
        {
            var userKey = Context.Request.QueryString["userKey"];
            Clients.User(userKey).NotifiClient(msg);
        }
        #endregion
    }
}
