using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class RaspodelaStatickeOpremeDto
    {
        public string IzProstorijeId { get; set; }
        public string UProstorijuId { get; set; }
        public StatickaOprema Oprema { get; set; }
        public int Kolicina { get; set; }
        public DateTime Datum { get; set; }
        public RaspodelaStatickeOpremeDto()
        {
            IzProstorijeId = "";
            UProstorijuId = "";
            Oprema = null;
            Kolicina = 0;
        }
        public RaspodelaStatickeOpremeDto(string izProstorije, string uProstoriju, StatickaOprema oprema, int kolicina, DateTime datum)
        {
            IzProstorijeId = izProstorije;
            UProstorijuId = uProstoriju;
            Oprema = oprema;
            Kolicina = kolicina;
            Datum = datum;
        }
    }
}
