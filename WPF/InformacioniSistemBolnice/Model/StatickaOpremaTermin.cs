using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    class StatickaOpremaTermin
    { 
        public DateTime datumPremestaja
        {
            get;
            set;
        }

        public Prostorija izProstorije
        {
            get;
            set;
        }

        public Prostorija uProstoriju
        {
            get;
            set;
        }

        public int kolicina
        {
            get;
            set;
        }

        public StatickaOprema oprema
        {
            get;
            set;
        }

        public StatickaOpremaTermin(Prostorija izProstorije, Prostorija uProstoriju, StatickaOprema statickaOprema, int kolicina, DateTime datum)
        {
            this.izProstorije = izProstorije;
            this.uProstoriju = uProstoriju;
            oprema = statickaOprema;
            this.kolicina = kolicina;
            datumPremestaja = datum;
        }
    }
}
