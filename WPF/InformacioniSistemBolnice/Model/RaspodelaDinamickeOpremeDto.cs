using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class RaspodelaDinamickeOpremeDto
    {
        public string IzProstorijeId { get; set; }
        public string UProstorijuId { get; set; }
        public DinamickaOprema Oprema { get; set; }
        public int Kolicina { get; set; }
        public RaspodelaDinamickeOpremeDto() 
        {
            IzProstorijeId = "";
            UProstorijuId = "";
            Oprema = null;
            Kolicina = 0;
        }
        public RaspodelaDinamickeOpremeDto(string izProstorije, string uProstoriju, DinamickaOprema oprema, int kolicina)
        {
            IzProstorijeId = izProstorije;
            UProstorijuId = uProstoriju;
            Oprema = oprema;
            Kolicina = kolicina;
        }
    }
}
