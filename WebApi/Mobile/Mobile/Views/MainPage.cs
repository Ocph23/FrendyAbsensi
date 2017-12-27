using System;
using Mobile.Models;
using Xamarin.Forms;
using Plugin.Toasts;

namespace Mobile
{
    public class MainPage : ContentPage
    {
        public MainPage(Services.AuthenticationToken token)
        {
            this.Token = token;
            Title = "Login";
            SignalrClient = new HubClient(Token);
            SignalrClient.OnMessageReceived += SignalrClient_OnMessageReceived;
            SignalrClient.OnThisUserAbsen += SignalrClient_OnThisUserAbsen;
            SignalrClient.Start();
        }

        private async void SignalrClient_OnThisUserAbsen(Models.absen ab)
        {
            var options = new NotificationOptions()
            {
                Title = "Title",
                Description = "Anda Telah Absen Hari Ini",
                IsClickable = false // Set to true if you want the result Clicked to come back (if the user clicks it)
            };
            var notification = DependencyService.Get<IToastNotificator>();
            var result = await notification.Notify(options);
        }

        private async void SignalrClient_OnMessageReceived(Models.absen ab)
        {
            var options = new NotificationOptions()
            {
                Title = "Title",
                Description = ab.JamMasuk.ToString(),
                IsClickable = false // Set to true if you want the result Clicked to come back (if the user clicks it)
            };
            var notification = DependencyService.Get<IToastNotificator>();
            var result = await notification.Notify(options);
        }

        public HubClient SignalrClient { get; private set; }

        public Services.AuthenticationToken Token { get; internal set; }
    }
}
