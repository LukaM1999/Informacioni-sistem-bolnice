using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class AnketaDto
    {
        public int Ljubaznost { get; set; }
        public int Profesionalizam { get; set; }
        public int Strpljenje { get; set; }
        public int Komunikativnost { get; set; }
        public int Azurnost { get; set; }
        public int Korisnost { get; set; }
        public string GeneralniKomentari { get; set; }
        public string PacijentovJmbg { get; set; }
        public DateTime VremePopunjavanja { get; set; }

        public AnketaDto(int ljubaznost, int profesionalizam, int strpljenje, int komunikativnost, int azurnost,
            int korisnost, string generalniKomentari, string pacijentovJmbg = null, DateTime vremePopunjavanja = new())
        {
            Ljubaznost = ljubaznost;
            Profesionalizam = profesionalizam;
            Strpljenje = strpljenje;
            Komunikativnost = komunikativnost;
            Azurnost = azurnost;
            Korisnost = korisnost;
            GeneralniKomentari = generalniKomentari;
            PacijentovJmbg = pacijentovJmbg;
            VremePopunjavanja = vremePopunjavanja;
        }
    }
}
