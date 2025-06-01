using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AppVeiculos.Models; 
using SQLite;

namespace AppVeiculos.Helpers
{
    public class SQLiteDatabaseHelpers
    {
        readonly SQLiteAsyncConnection _conn;

        public SQLiteDatabaseHelpers(string path)
        {
            _conn = new SQLiteAsyncConnection(path);
            _conn.CreateTableAsync<AppVeiculos.Models.Modelo>().Wait();
            _conn.CreateTableAsync<Marcas>().Wait();
        }

        //Modelo
        public Task<int> InsertModelo(AppVeiculos.Models.Modelo p)
        {
            return _conn.InsertAsync(p);
        }

        public Task<List<AppVeiculos.Models.Modelo>> UpdateModelo(AppVeiculos.Models.Modelo p)
        {
            string sql = "UPDATE Modelo SET modNome=?, modObs=? WHERE modId=?";

            return _conn.QueryAsync<AppVeiculos.Models.Modelo>(sql, p.modNome, p.modObs, p.modId);
        }

        public Task<int> DeleteModelo(int p)
        {
            return _conn.Table<AppVeiculos.Models.Modelo>().DeleteAsync(i => i.modId == p);

            /* ou
            string sql = "DELETE Modelo WHERE modId=?";
            return _conn.QueryAsync<AppVeiculos.Models.Modelo>(sql, p); */
        }

        public Task<List<AppVeiculos.Models.Modelo>> GetAllModelo()
        {
            return _conn.Table<AppVeiculos.Models.Modelo>().ToListAsync();
        }

        public Task<List<AppVeiculos.Models.Modelo>> SearchModelo(string p)
        {
            string sql = "SELECT * FROM Modelo WHERE modNome LIKE '%" + p + "%'";

            return _conn.QueryAsync<AppVeiculos.Models.Modelo>(sql);
        }

        // Marcas
        public Task<int> InsertMarca(Marcas p)
        {
            return _conn.InsertAsync(p);
        }

        public Task<List<Marcas>> UpdateMarca(Marcas p)
        {
            string sql = "UPDATE Marcas SET marNome=?, marObs=? WHERE marId=?";

            return _conn.QueryAsync<Marcas>(sql, p.marNome, p.marObs, p.marId);
        }

        public Task<int> DeleteMarca(int p)
        {
            return _conn.Table<Marcas>().DeleteAsync(i => i.marId == p);
        }

        public Task<List<Marcas>> GetAllMarca()
        {
            return _conn.Table<Marcas>().ToListAsync();
        }

        public Task<List<Marcas>> SearchMarca(string p)
        {
            string sql = "SELECT * FROM Marcas WHERE marNome LIKE '%" + p + "%'";
            
            return _conn.QueryAsync<Marcas>(sql);
        }
    }
}