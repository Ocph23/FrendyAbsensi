using System;
using Library.DataModels;

namespace Desktop.ViewModels
{
    internal class AddLiburViewModel:libur
    {
        public AddLiburViewModel()
        {
            this.Tanggal = DateTime.Now;
            SaveCommand = new CommandHandler { CanExecuteAction = x => true,ExecuteAction=SaveCommandAction };
            CancelCommand = new CommandHandler { CanExecuteAction = x => true, ExecuteAction=x=>WindowClose() };
        }

        private void SaveCommandAction(object obj)
        {
            libur model = new libur { Id = Id, Keterangan = Keterangan, Tanggal = Tanggal };
          if(this.Id<=0)
            {
                ResourcesBase.GetMainWindowViewModel().LiburCollection.Add(model);
            }else
            {
                ResourcesBase.GetMainWindowViewModel().LiburCollection.Updated(model);
            }
        }

        public CommandHandler SaveCommand { get; }
        public CommandHandler CancelCommand { get; }
        public Action WindowClose { get; internal set; }
    }
}