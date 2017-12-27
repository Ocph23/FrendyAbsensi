using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mobile.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Mobile.ViewModels;
using Plugin.Toasts;

namespace Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainMenu : ContentPage
    {

        public MainMenu()
        {
            InitializeComponent();
        }

        public MainMenu(AuthenticationToken token)
        {
            InitializeComponent();
            this.BindingContext = new MainMenuViewModel(Navigation,token);
            this.Token = token;
            Title = "Login";
           
            
        }

        
      

        public Services.AuthenticationToken Token { get; internal set; }
    }
}