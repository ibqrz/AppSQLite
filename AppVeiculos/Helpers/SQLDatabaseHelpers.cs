using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SQLite;
using AppVeiculos.Models;

namespace AppVeiculos.Helpers
{
    public class SQLiteDatabaseHelpers
    {
        readonly SQLiteAsyncConnection _conn;

        public SQLiteDatabaseHelpers(string path)
        {
            _conn = new SQLiteAsyncConnection(path);
            _conn.CreateTableAsync<Marca>().Wait();
            _conn.CreateTableAsync<Modelo>().Wait();
            //_conn.CreateTableAsync<Veiculo>().Wait();
        }

        //Marca 
        public Task<int> InsertMarca(Marca p)
        {
            return _conn.InsertAsync(p);
        }
        public Task<List<Marca>> UpdateMarca(Marca p)
        {
            string sql = "UPDATE Marca SET marNome=? AND marObs WHERE marId=?";

            return _conn.QueryAsync<Marca>(sql, p.marNome, p.marObs, p.marId);
        }
        public Task<int> DeleteMarca(int p)
        {
            return _conn.Table<Marca>().DeleteAsync(i => i.marId == p);

            /* ou
            string sql = "DELETE Marca WHERE marId=?";
            return _conn.QueryAsync<Marca>(sql, p); */
        }
        public Task<List<Marca>> GetAllMarca()
        {
            return _conn.Table<Marca>().ToListAsync();
        }

        public Task<List<Marca>> SearchMarca(string p)
        {
            string sql = "SELECT * FROM Marca WHERE marNome LIKE '%" + p + "%'";

            return _conn.QueryAsync<Marca>(sql);
        }

        //Modelo
        public Task<int> InsertModelo(Modelo p)
        {
            return _conn.InsertAsync(p);
        }
        public Task<List<Modelo>> UpdateModelo(Modelo p)
        {
            string sql = "UPDATE Modelo SET modNome=? AND modObs WHERE modId=?";

            return _conn.QueryAsync<Modelo>(sql, p.modNome, p.modObs, p.modId);
        }
        public Task<int> DeleteModelo(int p)
        {
            return _conn.Table<Modelo>().DeleteAsync(i => i.modId == p);

            /* ou
            string sql = "DELETE Modelo WHERE modId=?";
            return _conn.QueryAsync<Modelo>(sql, p); */
        }
        public Task<List<Modelo>> GetAllModelo()
        {
            return _conn.Table<Modelo>().ToListAsync();
        }

        public Task<List<Modelo>> SearchModelo(string p)
        {
            string sql = "SELECT * FROM Modelo WHERE modNome LIKE '%" + p + "%'";

            return _conn.QueryAsync<Modelo>(sql);
        }
    }
}
