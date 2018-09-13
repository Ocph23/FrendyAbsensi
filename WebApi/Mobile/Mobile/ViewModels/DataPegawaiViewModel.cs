using System;
using System.Collections.ObjectModel;
using Mobile.Models;
using Xamarin.Forms;
using Mobile.Helpers;

namespace Mobile.ViewModels
{
    internal class DataPegawaiViewModel:BaseViewModel
    {
        private INavigation navigation;

        public ObservableCollection<pegawai> Pegawais { get; private set; }
        public Command LoadItemsCommand { get; private set; }

        public DataPegawaiViewModel(INavigation navigation)
        {
            this.navigation = navigation;
            Title = "Data Pegawai";
            Pegawais = new ObservableCollection<Models.pegawai>();
            LoadItemsCommand = new Command((x) => ExecuteLoadItemsCommandAsync(x));
            ExecuteLoadItemsCommandAsync(null);
        }

        private async void ExecuteLoadItemsCommandAsync(object x)
        {
            if (IsBusy)
                return;
            try
            {
                IsBusy = true;
                Pegawais.Clear();
                var pegawais = await PegawaiDataStore.GetItemsAsync(true);
                foreach (var item in pegawais)
                {
                    Pegawais.Add(item);
                }
            }
            catch (Exception ex)
            {
                MessagingCenter.Send(new MessagingCenterAlert
                {
                    Title = "Error",
                    Message =ex.Message,
                    Cancel = "OK"
                }, "message");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}