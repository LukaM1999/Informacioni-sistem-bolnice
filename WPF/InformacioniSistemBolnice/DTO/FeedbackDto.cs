using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InformacioniSistemBolnice.DTO
{
    public class FeedbackDto
    {
        public string ImeKorisnika { get; set; }
        public string UlogaKorisnika { get; set; }
        public string Poruka { get; set; }

        public FeedbackDto(string imeKorisnika, string ulogaKorisnika, string poruka)
        {
            ImeKorisnika = imeKorisnika;
            UlogaKorisnika = ulogaKorisnika;
            Poruka = poruka;
        }
    }
}
