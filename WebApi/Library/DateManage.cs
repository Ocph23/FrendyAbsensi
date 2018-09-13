using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
  public  class DateManage
    {
        private DateTime date;

        public DateManage( DateTime date)
        {
            this.date = date;
        }

        public DateTime FirstDate { get { return new DateTime(date.Year, date.Month, 1); } }

        public DateTime LastDate { get {
                var firstDateinLastMont = FirstDate.AddMonths(1);
                return firstDateinLastMont.AddDays(-1);
            } }


        public DateTime FirstDayOfWeek
        {
            get
            {
                int d = (int)date.DayOfWeek;
                double value = Convert.ToDouble(d)*-1;
                DateTime t = date.AddDays(value);
          //      if (t.DayOfWeek == DayOfWeek.Sunday)
          ////          return t.AddDays(1);
                return t;

            }
        }

        public DateTime LastDayOfWeek
        {
            get
            {
                return FirstDayOfWeek.AddDays(6);
            }
        }

        public int Weeks
        {
            get { return getWeeks(); }
        }

        private int getWeeks()
        {
            int weeks = 0;
           for(int i=1;i<=LastDate.Day;i++)
            {
                var d = new DateTime(date.Year, date.Month, i);
                if (d.DayOfWeek == DayOfWeek.Sunday || d.DayOfWeek== DayOfWeek.Saturday)
                    weeks++;
            }
            return weeks;
        }
    }
}
