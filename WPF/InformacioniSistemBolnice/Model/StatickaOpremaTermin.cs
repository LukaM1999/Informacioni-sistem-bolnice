using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class StatickaOpremaTermin
    { 
        public DateTime DatumPremestaja { get; set;}
        public string IzProstorijeId { get; set;}
        public string UProstorijuId { get; set;}
        public int Kolicina { get; set;}
        public StatickaOprema Oprema { get; set;}
        public StatickaOpremaTermin(string izProstorije, string uProstoriju, StatickaOprema statickaOprema, int kolicina, DateTime datum)
        {
            IzProstorijeId = izProstorije;
            UProstorijuId = uProstoriju;
            Oprema = statickaOprema;
            Kolicina = kolicina;
            DatumPremestaja = datum;
        }
    }
}
