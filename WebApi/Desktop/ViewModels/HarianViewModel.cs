using Library.DataModels;
using Newtonsoft.Json;
using Ocph.DAL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Desktop.ViewModels
{
  public  class HarianViewModel: BaseNotify
    {
        public ObservableCollection<pegawai> Source { get; set; }
        public CollectionView SourceView { get; }
        public HarianViewModel()
        {
            Source = new ObservableCollection<pegawai>();
            SourceView = (CollectionView)CollectionViewSource.GetDefaultView(Source);
        }

        public async void GetListAsync(DateTime date)
        {
            var uri = string.Format("api/laporan/{0}-{1}-{2}", date.Year, date.Month, date.Day);
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

      
        private pegawai selected;
        public pegawai SelectedItem
        {
            get { return selected; }
            set
            {
                SetProperty(ref selected, value);
            }
        }

    }
}
