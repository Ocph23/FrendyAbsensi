using Ocph.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.ViewModels
{
   public class BaseViewModel:BaseNotify
    {

        public BaseViewModel() => RefreshCommand = new CommandHandler { CanExecuteAction = RefreshCommandValidation, ExecuteAction = RefreshCommandAction };

        public virtual bool RefreshCommandValidation(object obj)
        {
            if (obj != null)
                return (bool)obj;
            return true;
        }

        public virtual void RefreshCommandAction(object obj) { }

        public CommandHandler RefreshCommand { get; set; }

        private bool ringActive;

        public bool RingProgressActive
        {
            get { return ringActive; }
            set {
                SetProperty(ref ringActive, value);
            }
        }



    }
}
