using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using Model;
using Repozitorijum;

namespace InformacioniSistemBolnice.DTO
{
    public class TerminPacijentaDto
    {
        public TipTermina IzabraniTip { get; set; }
        public DateTime Pocetak { get; set; }
        public string Trajanje { get; set; }
        public Lekar Lekar { get; set; }
        public string ProstorijaId { get; set; }
        public bool Hitan { get; set; }
        public AnketaOLekaru Anketa { get; set; }

        public TerminPacijentaDto(TipTermina izabraniTip, DateTime pocetak, string trajanje, Lekar lekar, string prostorijaId, bool hitan, AnketaOLekaru anketa)
        {
            IzabraniTip = izabraniTip;
            Pocetak = pocetak;
            Trajanje = trajanje;
            Lekar = lekar;
            ProstorijaId = prostorijaId;
            Hitan = hitan;
            Anketa = anketa;
        }
    }
}
