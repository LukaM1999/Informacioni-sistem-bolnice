using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class ZakazivanjeTerminaSekretarDto
    {
        public DateTime MinDatum { get; set; }
        public DateTime MaxDatum { get; set; }
        public Lekar IzabranLekar { get; set; }
        public Pacijent IzabranPacijent { get; set; }
        public TipTermina IzabraniTip { get; set; }
        public Prostorija IzabrnaProstorija { get; set; }
        public bool VremePrioritet { get; set; }

        public ZakazivanjeTerminaSekretarDto(DateTime minDatum, DateTime maxDatum, Lekar izabranLekar,
                                             Pacijent izabraniPacijent, TipTermina izbraniTip,
                                            Prostorija izabranaProstorija, bool vremePrioritet)
        {
            MinDatum = minDatum;
            MaxDatum = maxDatum;
            IzabranLekar = izabranLekar;
            IzabranPacijent = izabraniPacijent;
            IzabraniTip = izbraniTip;
            IzabrnaProstorija = izabranaProstorija;
            VremePrioritet = vremePrioritet;
        }
    }
}
