using Xamarin.Forms;

namespace Mobile.ViewModels
{
    internal class LaporanViewModel
    {
        private INavigation navigation;

        public LaporanViewModel(INavigation navigation)
        {
            this.navigation = navigation;
        }
    }
}