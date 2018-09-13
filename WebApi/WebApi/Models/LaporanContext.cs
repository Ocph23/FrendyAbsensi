using Ocph.DAL;
using System;

namespace WebApi.Models
{
    public class LaporanContext : BaseNotify
    {
        private DateTime _start;
        private DateTime _endDate;
        public LaporanContext(DateTime dateStart, DateTime dateEnd)
        {
            this.DateEnd = dateEnd;
            this.DateStart = DateStart;
        }

        public LaporanContext(DateTime dateStart)
        {
            this.DateStart = DateStart;
        }

        public DateTime DateStart {
            get { return _start; }
            private set
            {
                SetProperty(ref _start, value);
            }
        }

       public DateTime DateEnd {
            get { return _endDate; }
           private set
            {
                SetProperty(ref _endDate, value);
            }
        }
    }
}