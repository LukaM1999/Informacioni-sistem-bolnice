using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace Model
{
    public class RenoviranjeTermin
    {
        public DateTime PocetakRenoviranja { get; set; }
        public DateTime KrajRenoviranja { get; set; }
        public string idProstorije { get; set; }

        public RenoviranjeTermin(DateTime pocetakRenoviranja, DateTime krajRenoviranja, string prostorija)
        {
            PocetakRenoviranja = pocetakRenoviranja;
            KrajRenoviranja = krajRenoviranja;
            idProstorije = prostorija;
        }
    }
}
