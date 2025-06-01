using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppVeiculos.Models
{
    public class Veiculo
    {
        [PrimaryKey, AutoIncrement]
        public int veiId { get; set; }

        [NotNull]
        public string veiNome { get; set; }

        [NotNull]
        public string veiAnoFab { get; set; }
    
        public string? veiObs { get; set; }

        [NotNull]
        public int codMar { get; set; }

        [NotNull]
        public int codMod { get; set; }
    }
}
