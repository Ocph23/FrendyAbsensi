using Library.DataModels;
using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class HubClient
    {
        public HubClient()
        {
            Console.WriteLine("Starting client http://localhost:12006/");

            var hubConnection = new HubConnection("http://localhost:12006/");
            //hubConnection.TraceLevel = TraceLevels.All;
            //hubConnection.TraceWriter = Console.Out;
            IHubProxy myHubProxy = hubConnection.CreateHubProxy("AbsenHub");

            myHubProxy.On<absen>("addNewAbsen", (absen) => Console.Write("Recieved addMessage: " +absen.JamMasuk+ "\n"));
            myHubProxy.On("heartbeat", () => Console.Write("Recieved heartbeat \n"));
            myHubProxy.On<absen>("SendNewAbsen", ab => Console.Write("Recieved sendHelloObject {0}, {1} \n",ab.JamMasuk , ab.JamPulang));

            hubConnection.Start().Wait();

        }
    }
}
