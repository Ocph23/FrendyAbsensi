using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobile.Models
{
    public class jabatan : BaseViewModel
    {
     
        public int Id
        {
            get { return _id; }
            set
            {
                SetProperty(ref _id, value);
            }
        }

        public string Nama
        {
            get { return _nama; }
            set
            {
                SetProperty(ref _nama, value);
            }
        }

        public string Keterangan
        {
            get { return _keterangan; }
            set
            {
                SetProperty(ref _keterangan, value);
            }
        }

        public int IdBidang
        {
            get { return _idbidang; }
            set
            {
                SetProperty(ref _idbidang, value);
            }
        }

        public string NamaBidang { get; set; }

        private int _id;
        private string _nama;
        private string _keterangan;
        private int _idbidang;
    }
}
