﻿using AppVeiculos.Models; 
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            _conn.CreateTableAsync<Veiculo>().Wait(); 
            _conn.CreateTableAsync<Usuario>().Wait();
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

        //Marcas
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

        // Veiculo
        public Task<int> InsertVeiculo(Veiculo p)
        {
            return _conn.InsertAsync(p);
        }

        public Task<List<Veiculo>> UpdateVeiculo(Veiculo p)
        {
            string sql = "UPDATE Veiculo SET veiNome=?, veiAnoFab=?, veiObs=?, codMar=?, codMod=? WHERE veiId=?";

            return _conn.QueryAsync<Veiculo>(sql, p.veiNome, p.veiAnoFab, p.veiObs, p.codMar, p.codMod, p.veiId);
        }

        public Task<int> DeleteVeiculo(int p)
        {
            return _conn.Table<Veiculo>().DeleteAsync(i => i.veiId == p);
        }

        public Task<List<Veiculo>> GetAllVeiculo()
        {
            return _conn.Table<Veiculo>().ToListAsync();
        }

        public Task<List<Veiculo>> SearchVeiculo(string p)
        {
            string sql = "SELECT * FROM Veiculo WHERE veiNome LIKE '%" + p + "%'";

            return _conn.QueryAsync<Veiculo>(sql);
        }

        // Usuario
        public Task<int> InsertUsuario(Usuario p)
        {
            return _conn.InsertAsync(p);
        }

        public Task<List<Usuario>> UpdateUsuario(Usuario p)
        {
            string sql = "UPDATE Usuario SET userNome=?, userSenha=? WHERE userId=?";

            return _conn.QueryAsync<Usuario>(sql, p.userNome, p.userSenha, p.userId);
        }

        public Task<int> DeleteUsuario(int p)
        {
            return _conn.Table<Usuario>().DeleteAsync(i => i.userId == p);
        }

        public Task<List<Usuario>> GetAllUsuario()
        {
            return _conn.Table<Usuario>().ToListAsync();
        }

        public Task<List<Usuario>> SearchUsuario(string p)
        {
            string sql = "SELECT * FROM Usuario WHERE userNome LIKE '%" + p + "%'";

            return _conn.QueryAsync<Usuario>(sql);
        }
    }
}