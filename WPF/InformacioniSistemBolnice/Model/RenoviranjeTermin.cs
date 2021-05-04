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
        public DateTime PocetakRenoviranja
        {
            get;
            set;
        }

        public DateTime KrajRenoviranja
        {
            get;
            set;
        }

        public Prostorija Prostorija
        {
            get;
            set;
        }

        public RenoviranjeTermin(DateTime pocetakRenoviranja, DateTime krajRenoviranja, Prostorija prostorija)
        {
            PocetakRenoviranja = pocetakRenoviranja;
            KrajRenoviranja = krajRenoviranja;
            Prostorija = prostorija;
        }
    }
}
