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

namespace Desktop.Pages
{
    /// <summary>
    /// Interaction logic for PegawaiView.xaml
    /// </summary>
    public partial class PegawaiView : UserControl
    {
        public PegawaiView()
        {
            InitializeComponent();
            this.DataContext = new ViewModels.PegawaiViewModel();
        }

        private void dg_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            dg.CancelEdit();
        }

        private void Image_MouseEnter(object sender, MouseEventArgs e)
        {

        }
    }
}
