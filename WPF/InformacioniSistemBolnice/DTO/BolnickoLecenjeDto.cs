using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace InformacioniSistemBolnice.DTO
{
    public class BolnickoLecenjeDto
    {
        public DateTime Pocetak { get; set; }
        public DateTime Zavrsetak { get; set; }
        public Pacijent Pacijent { get; set; }
        public Prostorija Prostorija { get; set; }

        public BolnickoLecenjeDto(DateTime pocetak, DateTime zavrsetak, Pacijent pacijent, Prostorija prostorija)
        {
            Pocetak = pocetak;
            Zavrsetak = zavrsetak;
            Pacijent = pacijent;
            Prostorija = prostorija;
        }
    }
}
