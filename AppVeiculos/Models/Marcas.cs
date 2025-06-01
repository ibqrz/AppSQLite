using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppVeiculos.Models
{
    public class Marcas
    {
        [PrimaryKey, AutoIncrement]
        public int marId { get; set; }

        [NotNull]
        public string marNome { get; set; }

        public string? marObs {  get; set; }
    }
}
