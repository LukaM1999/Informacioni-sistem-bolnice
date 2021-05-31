using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class TransformacijaProstorija
    {
        public bool Spajanje { get; set; }
        public string IdPrveStareProstorije { get; set; }
        public string IdDrugeStareProstorije { get; set; }
        public Prostorija PrvaNovaProstorija { get; set; }
        public Prostorija DrugaNovaProstorija { get; set; }
        public DateTime PocetakRenoviranja { get; set; }
        public DateTime KrajRenoviranja { get; set; }

        public TransformacijaProstorija(bool spajanje, string prvaStaraProstorija, string drugaStaraProstorija,
            Prostorija prvaNovaProstorija, Prostorija drugaNovaProstorija, DateTime pocetak, DateTime kraj)
        {
            Spajanje = spajanje;
            IdPrveStareProstorije = prvaStaraProstorija;
            IdDrugeStareProstorije = drugaStaraProstorija;
            PrvaNovaProstorija = prvaNovaProstorija;
            DrugaNovaProstorija = drugaNovaProstorija;
            PocetakRenoviranja = pocetak;
            KrajRenoviranja = kraj;
        }
    }
}
