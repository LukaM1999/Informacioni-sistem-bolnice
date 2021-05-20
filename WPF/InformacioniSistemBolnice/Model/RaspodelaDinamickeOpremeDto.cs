using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class RaspodelaDinamickeOpremeDto
    {
        public String IzProstorijeId { get; set; }
        public String UProstorijuId { get; set; }
        public DinamickaOprema Oprema { get; set; }
        public int Kolicina { get; set; }
        public RaspodelaDinamickeOpremeDto() 
        {
            IzProstorijeId = "";
            UProstorijuId = "";
            Oprema = null;
            Kolicina = 0;
        }
        public RaspodelaDinamickeOpremeDto(String izProstorije, String uProstoriju, DinamickaOprema oprema, int kolicina)
        {
            IzProstorijeId = izProstorije;
            UProstorijuId = uProstoriju;
            Oprema = oprema;
            Kolicina = kolicina;
        }
    }
}
