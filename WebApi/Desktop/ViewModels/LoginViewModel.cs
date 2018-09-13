using Library;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.ViewModels
{
   public class LoginViewModel :BaseViewModel
    {
        private string _userName;

        public string UserName
        {
            get { return _userName; }
            set { SetProperty(ref _userName, value); }
        }


        private string _password;

        public string Password
        {
            get { return _password; }
            set { SetProperty(ref _password, value); }
        }

        private string _message;

        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }

        public CommandHandler LoginCommand { get; }
        public CommandHandler CancelCommand { get; }
        public Action WindowClose { get; set; }
        public AuthenticationToken Token { get; private set; }
        public bool IsLogin { get; private set; }

        public LoginViewModel()
        {
            
            LoginCommand = new CommandHandler { CanExecuteAction = LoginCommandValidate, ExecuteAction =  LoginCommandAction };
            CancelCommand = new CommandHandler { CanExecuteAction = x => true, ExecuteAction = x => WindowClose() };
        }

        private bool LoginCommandValidate(object obj)
        {
            if (string.IsNullOrEmpty(UserName) || string.IsNullOrWhiteSpace(Password))
            {
                return false;
            }
            else
                return true;
        }

        private async void LoginCommandAction(object obj)
        {
            RingProgressActive = true;
            this.UserName = "ocph23.test@gmail.com";
            Password = "Sony@77";
            using (var client = new Client())
            {
                var strcontent = string.Format("grant_type=password&username={0}&password={1}", UserName, Password);
                HttpContent content = new StringContent(strcontent, Encoding.UTF8, "application/x-www-form-urlencoded");
                var response = await client.ClientContext.PostAsync("Token", content);
                RingProgressActive = false;
                if (response.IsSuccessStatusCode)
                {
                   var x = await response.Content.ReadAsStringAsync();
                    Token = JsonConvert.DeserializeObject<AuthenticationToken>(x);
                    if (Token.access_token != null)
                    {
                        ResourcesBase.Token = Token;
                        IsLogin = true;
                       
                        WindowClose();
                    }
                    else
                    {
                        this.Message = "User Or Password Invalid !..";
                    }

                }
                else
                {
                    this.Message = "You Not Connect to Server";
                }

            }
        }
    }
}
