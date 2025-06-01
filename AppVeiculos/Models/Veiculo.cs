using SQLite; 

namespace AppVeiculos.Models
{
    public class Veiculo
    {
        [PrimaryKey, AutoIncrement]
        public int veiId { get; set; } 

        public string veiNome { get; set; }
        public int veiAnoFab { get; set; }
        public string? veiObs { get; set; }

        [Indexed] 
        public int codMar { get; set; } 

        [Indexed] 
        public int codMod { get; set; } 
    }
}