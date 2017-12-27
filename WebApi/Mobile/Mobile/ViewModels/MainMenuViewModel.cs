using Plugin.Toasts;
using System;
using Xamarin.Forms;

namespace Mobile.ViewModels
{
    internal class MainMenuViewModel
    {
        private INavigation navigation;

        public Command DataPegawaiCommand { get; }
        public Command DataAbsenCommand { get; }
        public Command LaporanCommand { get; }
        public Command TentangCommand { get; }
        public HubClient SignalrClient { get; }

        public MainMenuViewModel(INavigation navigation,Services.AuthenticationToken token)
        {
            this.navigation = navigation;
            DataPegawaiCommand = new Command(() => OpenDataPegawai());
            DataAbsenCommand = new Command(() => OpenDataAbsen());
            LaporanCommand = new Command(() => OpenLaporan());
            TentangCommand = new Command(() => OpenTentang());
            SignalrClient = new HubClient(token);
            SignalrClient.OnMessageReceived += SignalrClient_OnMessageReceived;
            SignalrClient.OnThisUserAbsen += SignalrClient_OnThisUserAbsen;
            SignalrClient.Start();
        }

        private void OpenLaporan()
        {
            var page = new Views.LaporanView();
            page.BindingContext = new ViewModels.LaporanViewModel(navigation);
            navigation.PushAsync(page);
        }

        private void OpenTentang()
        {
            var page = new AboutPage();
            page.BindingContext = new AboutViewModel();
            navigation.PushAsync(page);
        }

        private void OpenDataAbsen()
        {
            var page = new Views.DataAbsenPegawaiView();
            page.BindingContext =new ViewModels.DataAbsenPegawaiViewModel(navigation);
            navigation.PushAsync(page);
        }

        private void OpenDataPegawai()
        {
            var page = new Views.DataPegawaiView();
            page.BindingContext = new ViewModels.DataPegawaiViewModel(navigation);
            navigation.PushAsync(page);
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
    }
}