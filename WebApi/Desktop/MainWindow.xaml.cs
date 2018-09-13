using FirstFloor.ModernUI.Windows.Controls;
using System.Windows;

namespace Desktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : ModernWindow
    {
        public MainWindow()
        {

            var flash = new Splash();
            flash.ShowDialog();

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
