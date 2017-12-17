using DAL;

namespace Library.DataModels
{
    [TableName("bidang")]
    public class bidang : BaseNotifyProperty
    {
        [PrimaryKey("Id")]
        [DbColumn("Id")]
        public int Id
        {
            get { return _id; }
            set
            {
                _id = value;
                OnPropertyChange("Id");
            }
        }

        [DbColumn("Nama")]
        public string Nama
        {
            get { return _nama; }
            set
            {
                _nama = value;
                OnPropertyChange("Nama");
            }
        }

        [DbColumn("Keterangan")]
        public string Keterangan
        {
            get { return _keterangan; }
            set
            {
                _keterangan = value;
                OnPropertyChange("Keterangan");
            }
        }

        private int _id;
        private string _nama;
        private string _keterangan;
    }
}


