using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    class StatickaOpremaTermin
    { 
        public DateTime DatumPremestaja { get; set;}
        public String IzProstorijeId { get; set;}
        public String UProstorijuId { get; set;}
        public int Kolicina { get; set;}
        public StatickaOprema Oprema { get; set;}
        public StatickaOpremaTermin(String izProstorije, String uProstoriju, StatickaOprema statickaOprema, int kolicina, DateTime datum)
        {
            IzProstorijeId = izProstorije;
            UProstorijuId = uProstoriju;
            Oprema = statickaOprema;
            Kolicina = kolicina;
            DatumPremestaja = datum;
        }
    }
}
