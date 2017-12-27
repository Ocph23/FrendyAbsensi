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
   public class LoginViewModel :DAL.BaseNotifyProperty
    {
        private string _userName;

        public string UserName
        {
            get { return _userName; }
            set { _userName = value; OnPropertyChange("User Name"); }
        }


        private string _password;

        public string Password
        {
            get { return _password; }
            set { _password = value; OnPropertyChange("Password"); }
        }

        private string _message;

        public string Message
        {
            get { return _message; }
            set { _message = value; OnPropertyChange("Message"); }
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
            this.UserName = "Ocph23.test@gmail.com";
            Password = "Sony@77";
            using (var client = new Client())
            {
                var strcontent = string.Format("grant_type=password&username={0}&password={1}", UserName, Password);
                HttpContent content = new StringContent(strcontent, Encoding.UTF8, "application/x-www-form-urlencoded");
                var response = await client.ClientContext.PostAsync("Token", content);
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
