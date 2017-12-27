using Plugin.Toasts;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Mobile
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

           

            MessagingCenter.Subscribe<Helpers.MessagingCenterAlert>(this, "message", async (message) =>
            {
                await Current.MainPage.DisplayAlert(message.Title, message.Message, message.Cancel);

            });
            SetMainPage();
        }

       

        public static void SetMainPage()
        {

            var login = new Views.LoginView();

            Current.MainPage = new NavigationPage( login);


        }


        public void ChangeScreen(Page page)
        {
            Current.MainPage = new NavigationPage(page);
        }
    }
}