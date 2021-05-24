using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class ZakazivanjeTerminaPacijentaDTO
    {
        public DateTime MinDatum { get; set; }
        public DateTime MaxDatum { get; set; }
        public Lekar IzabranLekar { get; set; }
        public bool VremePrioritet { get; set; }
        public string PacijentovJmbg { get; set; }

        public ZakazivanjeTerminaPacijentaDTO(DateTime minDatum, DateTime maxDatum,
            Lekar izabranLekar, bool vremePrioritet, string pacijentovJmbg)
        {
            MinDatum = minDatum;
            MaxDatum = maxDatum;
            IzabranLekar = izabranLekar;
            VremePrioritet = vremePrioritet;
            PacijentovJmbg = pacijentovJmbg;
        }

        public ZakazivanjeTerminaPacijentaDTO(string pacijentovJmbg)
        {
            PacijentovJmbg = pacijentovJmbg;
        }
    }
}
