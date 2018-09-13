using Ocph.DAL;

namespace Library.DataModels
{
    [TableName("bidang")]
    public class bidang : BaseNotify
    {
        [PrimaryKey("Id")]
        [DbColumn("Id")]
        public int Id
        {
            get { return _id; }
            set
            {
                _id = value;
                SetProperty(ref _id, value);
            }
        }

        [DbColumn("Nama")]
        public string Nama
        {
            get { return _nama; }
            set
            {
                SetProperty(ref _nama , value);
            }
        }

        [DbColumn("Keterangan")]
        public string Keterangan
        {
            get { return _keterangan; }
            set
            {
                SetProperty(ref _keterangan, value);
            }
        }

        private int _id;
        private string _nama;
        private string _keterangan;
    }
}


