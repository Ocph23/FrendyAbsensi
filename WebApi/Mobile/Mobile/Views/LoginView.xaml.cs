using Plugin.Toasts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginView : ContentPage
    {
        public LoginView()
        {
            InitializeComponent();
            this.BindingContext = new ViewModels.LoginViewModel(Navigation);
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            showMessage();
        }

        private async void showMessage()
        {
            var options = new NotificationOptions()
            {
                Title = "Title",
                Description = "Some Description", 
                IsClickable = false // Set to true if you want the result Clicked to come back (if the user clicks it)
            };
            var notification = DependencyService.Get<IToastNotificator>();
            var result = await notification.Notify(options);
        }
    }
}