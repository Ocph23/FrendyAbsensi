using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Desktop.ViewModels
{
   public  class HomeViewModel:DAL.BaseNotifyProperty
    {
        private DateTime dateTime;

        public DateTime Today
        {
            get { return dateTime; }
            set { dateTime = value; OnPropertyChange("Today"); }
        }

        public CollectionView SourceView { get; }

        public HomeViewModel()
        {
            SourceView = (CollectionView)CollectionViewSource.GetDefaultView(ResourcesBase.GetMainWindowViewModel().AbsenTodayCollection);
            SourceView.Refresh();
            Today = DateTime.Now;
        }

    }
}
