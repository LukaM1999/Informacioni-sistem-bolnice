using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using Model;

namespace InformacioniSistemBolnice.DTO
{
    public class TerminDto
    {
        public string Naziv { get; set; }
        public DateTime Pocetak { get; set; }
        public DateTime Kraj { get; set; }
        public Brush BojaPozadine { get; set; }
        public Brush BojaSlova { get; set; }
        public string LekarJmbg { get; set; }
        public string PacijentJmbg { get; set; }
        public string ProstorijaId { get; set; }
        public bool Hitan { get; set; }
        public TipTermina IzabraniTip { get; set; }

        public TerminDto()
        {
                
        }

        public TerminDto(string naziv, DateTime pocetak, DateTime kraj, Brush bojaPozadine, Brush bojaSlova, string lekarJmbg, string pacijentJmbg, string prostorijaId, bool hitan, TipTermina izabraniTip)
        {
            Naziv = naziv;
            Pocetak = pocetak;
            Kraj = kraj;
            BojaPozadine = bojaPozadine;
            BojaSlova = bojaSlova;
            LekarJmbg = lekarJmbg;
            PacijentJmbg = pacijentJmbg;
            ProstorijaId = prostorijaId;
            Hitan = hitan;
            IzabraniTip = izabraniTip;
        }
    }
}
