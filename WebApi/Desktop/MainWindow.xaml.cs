using Desktop.Collections;
using FirstFloor.ModernUI.Windows.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Desktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : ModernWindow
    {
        public MainWindow()
        {
            var form = new Forms.LoginView();
           var  vm = new ViewModels.LoginViewModel() { WindowClose = form.Close };
            form.DataContext = vm;
            form.ShowDialog();
            if(!vm.IsLogin)
            {
                this.Close();
            }else
            {
                InitializeComponent();
                var main = new MainWindowViewModel();
                this.DataContext = main;
                main.StartSignalR();
            }

            
        }

        internal void ShowMessage(string message)
        {
            ModernDialog.ShowMessage(message, "Info", MessageBoxButton.OK,this);
        }

        internal void ShowMessageError(string message)
        {
            ModernDialog.ShowMessage(message, "Error", MessageBoxButton.OK,this);
        }

        internal MessageBoxResult MessageAsk(string message)
        {
            return ModernDialog.ShowMessage(message, "Ask", MessageBoxButton.YesNo);
        }

    }
}
