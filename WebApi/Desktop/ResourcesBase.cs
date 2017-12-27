using Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Desktop
{
    public class ResourcesBase
    {
        public static AuthenticationToken Token { get; set; }

        internal static MainWindowViewModel GetMainWindowViewModel()
        {
            return (App.Current.Windows[0] as MainWindow).DataContext as MainWindowViewModel;
        }

        public static List<T> GetEnumCollection<T>()
        {
            var enums = Enum.GetValues(typeof(T));
            List<T> list = new List<T>();
            foreach (T item in enums)
            {
                list.Add(item);
            }
            return list;
        }

        internal static void ShowMessage(string message)
        {
            var form = App.Current.Windows[0] as MainWindow;
            form.ShowMessage(message);
        }
        internal static void ShowMessageError(string message)
        {
            var form = App.Current.Windows[0] as MainWindow;
            form.ShowMessageError(message);
        }

        internal static bool MessageAsk(string message)
        {
            var form = App.Current.Windows[0] as MainWindow;
            if (form.MessageAsk(message) == MessageBoxResult.Yes)
            {
                return true;
            }
            else
                return false;
        }
        
    }
}
