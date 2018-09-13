using Library.DataModels;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Data;


namespace Desktop.ViewModels
{
    public  class LiburViewModel:BaseViewModel
    {
        private int thisYear = DateTime.Now.Year;

        private int selectedYear;

        public int SelectedYear
        {
            get { return selectedYear; }
            set {
                
                if(value>0)
                {
                    Source.Clear();
                    var result = ResourcesBase.GetMainWindowViewModel().LiburCollection.Where(O => O.Tanggal.Year == value);
                    foreach(var item in result)
                    {
                        Source.Add(item);
                    }
                    SourceView.Refresh();
                  }
                SetProperty(ref selectedYear , value);
            }
        }


        private libur _libur;

        public libur SelectedItem
        {
            get { return _libur; }
            set { SetProperty(ref _libur , value); }
        }
        
        public LiburViewModel()
        {
            Years = new ObservableCollection<int>();
           for(int a=DateTime.Now.Year;a>=DateTime.Now.Year-5;a--)
            {
                Years.Add(a);
            }

            YearsView = (CollectionView)CollectionViewSource.GetDefaultView(Years);
            YearsView.Refresh();
            var sources=ResourcesBase.GetMainWindowViewModel().LiburCollection;

            Source = new ObservableCollection<libur>(sources.Where(O=>O.Tanggal.Year==DateTime.Now.Year));
            SourceView = (CollectionView)CollectionViewSource.GetDefaultView(Source);
            SourceView.SortDescriptions.Add(new System.ComponentModel.SortDescription("Tanggal", System.ComponentModel.ListSortDirection.Ascending));

            SourceView.Refresh();

            AddCommand = new CommandHandler
            {
                CanExecuteAction = x =>
                {
                    return SelectedYear == DateTime.Now.Year ? true : false;
                },
                ExecuteAction = x =>
                {
                    var form = new Forms.AddLiburView();
                    var vm = new ViewModels.AddLiburViewModel() { WindowClose = form.Close };
                    form.DataContext = vm;
                    form.ShowDialog();
                    SourceView.Refresh();

                }
            };



        }


        public ObservableCollection<int> Years { get; }
        public CollectionView YearsView { get; }
        public ObservableCollection<libur> Source { get; }
        public CollectionView SourceView { get; }
        public CommandHandler AddCommand { get; }
    }
}
