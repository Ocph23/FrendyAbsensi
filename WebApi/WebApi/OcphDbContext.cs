using Ocph.DAL.Repository;
using System;
using System.Data;
using System.Configuration;
using Ocph.DAL.Provider.MySql;

namespace WebApi
{
    public class OcphDbContext :MySqlDbConnection
    {

        public OcphDbContext()
        {
          
            this.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }

        public IRepository<Library.DataModels.pegawai> Pegawai { get { return new Repository<Library.DataModels.pegawai>(this); } }
        public IRepository<Library.DataModels.bidang> Bidang { get { return new Repository<Library.DataModels.bidang>(this); } }
        public IRepository<Library.DataModels.jabatan> Jabatan{ get { return new Repository<Library.DataModels.jabatan>(this); } }
        public IRepository<Library.DataModels.perizinan> Perizinan { get { return new Repository<Library.DataModels.perizinan>(this); } }
        public IRepository<Library.DataModels.absen> Absen{ get { return new Repository<Library.DataModels.absen>(this); } }
        public IRepository<Library.DataModels.pengaturan> Setting { get { return new Repository<Library.DataModels.pengaturan>(this); } }
        public IRepository<Library.DataModels.libur> Libur { get { return new Repository<Library.DataModels.libur>(this); } }

       
    }
}
