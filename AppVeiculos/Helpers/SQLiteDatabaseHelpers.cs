using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AppVeiculos.Models; // Mantenha este 'using', ele é importante!
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
            // Qualifique o tipo na Table
            return _conn.Table<AppVeiculos.Models.Modelo>().ToListAsync();
        }

        public Task<List<AppVeiculos.Models.Modelo>> SearchModelo(string p)
        {
            string sql = "SELECT * FROM Modelo WHERE modNome LIKE '%" + p + "%'";
            // Qualifique o tipo no QueryAsync
            return _conn.QueryAsync<AppVeiculos.Models.Modelo>(sql);
        }
    }
}