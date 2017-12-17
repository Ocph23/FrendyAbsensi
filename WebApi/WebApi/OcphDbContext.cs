using DAL.DContext;
using DAL.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Library;

namespace WebApi
{
    public class OcphDbContext : IDbContext, IDisposable
    {
        private string ConnectionString;
        private IDbConnection _Connection;

        public OcphDbContext()
        {
            this.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }

        public IRepository<Library.DataModels.pegawai> Pegawai { get { return new Repository<Library.DataModels.pegawai>(this); } }
        public IRepository<Library.DataModels.bidang> Bidang { get { return new Repository<Library.DataModels.bidang>(this); } }
        public IRepository<Library.DataModels.jabatan> Jabatan{ get { return new Repository<Library.DataModels.jabatan>(this); } }
        public IRepository<Library.DataModels.perizinan> Perizinan { get { return new Repository<Library.DataModels.perizinan>(this); } }
        public IRepository<Library.DataModels.absen> Absen{ get { return new Repository<Library.DataModels.absen>(this); } }


        public IDbConnection Connection
        {
            get
            {
                if (_Connection == null)
                {
                    _Connection = new MySqlDbContext(this.ConnectionString);
                    return _Connection;
                }
                else
                {
                    return _Connection;
                }
            }
        }

        public void Dispose()
        {
            if (_Connection != null)
            {
                if (this.Connection.State != ConnectionState.Closed)
                {
                    this.Connection.Close();
                }
            }
        }
    }
}
