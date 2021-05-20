using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class BolnickoLecenje
    { 
        public DateTime PocetakLecenja { get; set;}
        public DateTime ZavrsetakLecenja { get; set;}
        public string NazivProstorije { get; set; }
        public string JmbgPacijenta { get; set; }

        public BolnickoLecenje(DateTime pocetakLecenja, DateTime zavrsetakLecenja, string nazivProstorije, string jmbgPacijenta)
        {
            PocetakLecenja = pocetakLecenja;
            ZavrsetakLecenja = zavrsetakLecenja;
            NazivProstorije = nazivProstorije;
            JmbgPacijenta = jmbgPacijenta;
        }
    }
}
