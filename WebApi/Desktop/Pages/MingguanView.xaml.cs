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
using Library.DataModels;
using Newtonsoft.Json;

namespace Desktop.Pages
{
    /// <summary>
    /// Interaction logic for MingguanView.xaml
    /// </summary>
    public partial class MingguanView : UserControl
    {
        public ObservableCollection<pegawai> Source { get; set; }
        public CollectionView SourceView { get; }

        public MingguanView()
        {
            InitializeComponent();
            DataContext = this;
            Source = new ObservableCollection<pegawai>();
            SourceView = (CollectionView)CollectionViewSource.GetDefaultView(Source);

        }

        public async void GetListAsync(DateTime date)
        {
            var uri = string.Format("api/Laporan/minggu/{0}-{1}-{2}", date.Year, date.Month, date.Day);
            Source.Clear();
            Client client = new Client();
            var response = await client.ClientContext.GetAsync(uri);
            if (response.IsSuccessStatusCode == true)
            {
                var result = JsonConvert.DeserializeObject<List<pegawai>>(response.Content.ReadAsStringAsync().Result);
                foreach (var item in result)
                {
                    Source.Add(item);
                }
            }
            SourceView.Refresh();
        }

        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            var picker = (DatePicker)sender;
            if (picker != null && picker.SelectedDate != null)
                GetListAsync(picker.SelectedDate.Value);
        }
    }
}
