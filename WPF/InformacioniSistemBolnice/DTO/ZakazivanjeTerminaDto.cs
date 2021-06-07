using System;
using Model;

namespace InformacioniSistemBolnice.DTO
{
    public class ZakazivanjeTerminaDto
    {
        public DateTime MinDatum { get; set; }
        public DateTime MaxDatum { get; set; }
        public string LekarJmbg { get; set; }
        public bool VremePrioritet { get; set; }
        public string PacijentJmbg { get; set; }
        public string Specijalizacija { get; set; }
        public string ProstorijaId { get; set; }
        public bool Hitan { get; set; }
        public TipTermina IzabraniTip { get; set; }

        public ZakazivanjeTerminaDto(DateTime minDatum, DateTime maxDatum,
            string lekarJmbg, string pacijentJmbg, string specijalizacija = null, bool vremePrioritet = false,
            string prostorijaId = null, bool hitan = false, TipTermina izabraniTip = TipTermina.pregled)
        {
            MinDatum = minDatum;
            MaxDatum = maxDatum;
            LekarJmbg = lekarJmbg;
            PacijentJmbg = pacijentJmbg;
            Specijalizacija = specijalizacija;
            VremePrioritet = vremePrioritet;
            ProstorijaId = prostorijaId;
            Hitan = hitan;
            IzabraniTip = izabraniTip;
        }

        public ZakazivanjeTerminaDto(string pacijentovJmbg)
        {
            PacijentJmbg = pacijentovJmbg;
        }
    }
}
