using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Library.DataModels;

namespace WebApi
{
    public class AbsenHub : Hub
    {
        public void SendNewAbsen(absen ab)
        {
            Clients.All.AddNewAbsen(ab);
        }
    }
}