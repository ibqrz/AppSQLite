using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SQLite;

namespace AppVeiculos.Models
{
    public class Modelo
    {
        [PrimaryKey, AutoIncrement]
        public int modId {  get; set; }

        [NotNull]
        public string modNome { get; set; }

        public string? modObs {  get; set; }
    }
}
