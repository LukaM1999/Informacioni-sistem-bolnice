using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InformacioniSistemBolnice;

namespace Model
{
    public class UputDto
    {
        public Pacijent Pacijent { get; set; }
        public Lekar Lekar { get; set; }
        public DateTime Pocetak { get; set; }
        public DateTime Kraj { get; set; }
        //public Prostorija Prostorija { get; set; }
        public TipTermina Tip { get; set; }
        public bool Hitan { get; set; }

        public UputDto(DateTime pocetak, DateTime kraj, Lekar lekar, Pacijent pacijent, TipTermina tip, bool hitan)
        {
            Pocetak = pocetak;
            Kraj = kraj;
            Pacijent = pacijent;
            Lekar = lekar;
            //Prostorija = prostorija;
            Tip = tip;
            Hitan = hitan;
        }

        public UputDto()
        {
        }
    }
}
