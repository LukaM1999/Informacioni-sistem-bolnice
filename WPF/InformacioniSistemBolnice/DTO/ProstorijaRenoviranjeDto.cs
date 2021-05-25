using System;
using Model;

namespace InformacioniSistemBolnice.DTO
{
    public class ProstorijaRenoviranjeDto
    {
        public DateTime PocetakRenoviranja { get; set; }
        public DateTime KrajRenoviranja { get; set; }
        public Prostorija Prostorija { get; set; }

        public ProstorijaRenoviranjeDto(DateTime pocetakRenoviranja, DateTime krajRenoviranja, Prostorija prostorija)
        {
            PocetakRenoviranja = pocetakRenoviranja;
            KrajRenoviranja = krajRenoviranja;
            Prostorija = prostorija;
        }
    }
    
}
