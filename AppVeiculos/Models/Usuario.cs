using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppVeiculos.Models
{
    public class Usuario
    {
        [PrimaryKey, AutoIncrement]
        public int userId { get; set; }

        public string userNome { get; set; } 

        public string userSenha { get; set; }
    }
}
