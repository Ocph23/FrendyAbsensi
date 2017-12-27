using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Library.DataModels;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace WebApi
{
    
    public class AbsenHub : Hub
    {
        private readonly static ConnectionMapping<string> _connections =
           new ConnectionMapping<string>();

        public void SendNewAbsen(absen ab)
        {
            Clients.All.AddNewAbsen(ab);

            using (var db = new OcphDbContext())
            {
              var result=  db.Pegawai.Where(O => O.Id == ab.PegawaiId).FirstOrDefault();
                if(result!=null)
                {
                    foreach (var connectionId in _connections.GetConnections(result.UserId))
                    {
                        Clients.Client(connectionId).addToUser(ab);
                    }
                }
            }
        }

        public override Task OnConnected()
        {
            if (Context.User!=null)
            {
                string userId = Context.User.Identity.GetUserId();
                _connections.Add(userId, Context.ConnectionId);
            }
               

            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            if (Context.User != null)
            {
                string userId = Context.User.Identity.GetUserId();
                    _connections.Remove(userId, Context.ConnectionId);
            }
            return base.OnDisconnected(stopCalled);
        }

        public override Task OnReconnected()
        {

            if (Context.User != null)
            {
                string userId = Context.User.Identity.GetUserId();
                if (!_connections.GetConnections(userId).Contains(Context.ConnectionId))
                {
                    _connections.Add(userId, Context.ConnectionId);
                }
            }
                
         
            return base.OnReconnected();
        }


    }


    public class ConnectionMapping<T>
    {
        private readonly Dictionary<T, HashSet<string>> _connections =
            new Dictionary<T, HashSet<string>>();

        public int Count
        {
            get
            {
                return _connections.Count;
            }
        }

        public void Add(T key, string connectionId)
        {
            lock (_connections)
            {
                HashSet<string> connections;
                if (!_connections.TryGetValue(key, out connections))
                {
                    connections = new HashSet<string>();
                    _connections.Add(key, connections);
                }

                lock (connections)
                {
                    connections.Add(connectionId);
                }
            }
        }

        public IEnumerable<string> GetConnections(T key)
        {
            HashSet<string> connections;
            if (_connections.TryGetValue(key, out connections))
            {
                return connections;
            }

            return Enumerable.Empty<string>();
        }

        public void Remove(T key, string connectionId)
        {
            lock (_connections)
            {
                HashSet<string> connections;
                if (!_connections.TryGetValue(key, out connections))
                {
                    return;
                }

                lock (connections)
                {
                    connections.Remove(connectionId);

                    if (connections.Count == 0)
                    {
                        _connections.Remove(key);
                    }
                }
            }
        }
    }
}