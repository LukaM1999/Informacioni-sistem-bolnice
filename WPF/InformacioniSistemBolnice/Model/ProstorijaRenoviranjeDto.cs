using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class ProstorijaRenoviranjeDto
    {
        public DateTime PocetakRenoviranja { get; set; }
        public DateTime KrajRenoviranja { get; set; }
        public Prostorija Prostorija { get; set; }

        public ProstorijaRenoviranjeDto(DateTime pocetakRenoviranja, DateTime krajRenoviranja, Prostorija prostorija)
        {
            PocetakRenoviranja = pocetakRenoviranja;
            KrajRenoviranja = krajRenoviranja;
            Prostorija = prostorija;
        }
    }
    
}
