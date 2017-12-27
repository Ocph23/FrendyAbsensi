using Library.DataModels;
using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class HubClient
    {

        private HubConnection hubConnection;
        private IHubProxy myHubProxy;
        private AuthenticationToken token;

        public delegate void AbsenReceived(absen ab);
        public event AbsenReceived OnMessageReceived;
        public event AbsenReceived OnThisUserAbsen;

        public HubClient(AuthenticationToken token)
        {
            this.token = token;
            Debug.WriteLine("Starting client" + "http://localhost:12006/");
           
            hubConnection = new HubConnection("http://localhost/",
                 new Dictionary<string, string>
                                   {
                                       { "access_token",  token.access_token }
                                   });
            //hubConnection.TraceLevel = TraceLevels.All;
            //hubConnection.TraceWriter = Console.Out;
            myHubProxy = hubConnection.CreateHubProxy("AbsenHub");

            myHubProxy.On<absen>("addNewAbsen", (absen) =>
            { OnMessageReceived?.Invoke(absen); });

            myHubProxy.On<absen>("addToUser", (absen) =>
            { OnThisUserAbsen?.Invoke(absen); });


            myHubProxy.On("heartbeat", () => Debug.WriteLine("Recieved heartbeat \n"));
            myHubProxy.On<absen>("SendNewAbsen", ab =>
            Debug.WriteLine("Recieved sendHelloObject {0}, {1} \n", ab.JamMasuk, ab.JamPulang));

            hubConnection.StateChanged += HubConnection_StateChanged;
        }

        private void HubConnection_StateChanged(StateChange obj)
        {
            Debug.WriteLine("Signal Connected");
        }

        public Task Start()
        {
            return hubConnection.Start();
        }
    }
}
