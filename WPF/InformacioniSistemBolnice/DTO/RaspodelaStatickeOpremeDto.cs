using System;
using Model;

namespace InformacioniSistemBolnice.DTO
{
    public class RaspodelaStatickeOpremeDto
    {
        public string IzProstorijeId { get; set; }
        public string UProstorijuId { get; set; }
        public StatickaOprema Oprema { get; set; }
        public int Kolicina { get; set; }
        public DateTime Datum { get; set; }
        
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
