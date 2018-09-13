using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Desktop.ViewModels;
using Library.DataModels;
using Newtonsoft.Json;


namespace Desktop.Pages
{
    /// <summary>
    /// Interaction logic for HarianView.xaml
    /// </summary>
    public partial class HarianView : UserControl
    {
        private HarianViewModel viewmodel;

        public HarianView()
        {
            InitializeComponent();
            this.viewmodel= new ViewModels.HarianViewModel(); 
            DataContext = viewmodel;
        }

        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            var picker = (DatePicker)sender;
            if (picker != null && picker.SelectedDate != null)
                this.viewmodel.GetListAsync(picker.SelectedDate.Value);
        }
    }
}
