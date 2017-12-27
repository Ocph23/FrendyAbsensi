using Mobile.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Mobile.Services;

namespace Mobile.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private INavigation navigation;
        private string _email;
        private string _password;
        private AuthenticationToken token;
        private Command _loginCommand;

        public Command LoginCommand { get { return _loginCommand; } set { SetProperty(ref _loginCommand, value); } }

        public LoginViewModel(INavigation navigation)
        {
            this.navigation = navigation;
            LoginCommand = new Command((x) => LoginActionAsync(x), x => LoginValidate(x));
        }

        private bool LoginValidate(object x)
        {
            if (!string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(Password))
            {
                return true;
            }
            return false;
        }

        private async void LoginActionAsync(object x)
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                using (var res = new Services.RestService())
                {
                    this.Email = "ocph23@gmail.com";
                    this.Password = "User@123";
                    token = await res.GenerateTokenAsync(this.Email, Password);
                    if (token != null)
                    {
                        var main = new Views.MainMenu(token);
                        Helpers.Main.Token = token;
                        ((App)App.Current).ChangeScreen(main);
                    }
                    else
                    {
                        MessagingCenter.Send(new MessagingCenterAlert
                        {
                            Title = "Error",
                            Message = "Gagal Login",
                            Cancel = "OK"
                        }, "message");
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                MessagingCenter.Send(new MessagingCenterAlert
                {
                    Title = "Error",
                    Message = ex.Message,
                    Cancel = "OK"
                }, "message");
            }
            finally
            {
                IsBusy = false;
            }

        }

        private void LoginCommandAction()
        {
           
        }



        public string Email
        {
            get { return _email; }
            set
            {
                LoginCommand = new Command((x) => LoginActionAsync(x), x => LoginValidate(x));
                SetProperty(ref _email, value);
            }
        }
        public string Password
        {
            get { return _password; }
            set
            {
                LoginCommand = new Command((x) => LoginActionAsync(x), x => LoginValidate(x));
                SetProperty(ref _password, value);
            }
        }
    }
}
