using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Feedback
    {
        public string ImeKorisnika { get; set; }
        public string UlogaKorisnika { get; set; }
        public string Poruka { get; set; }
        public DateTime VremeSlanja { get; set; }

        public Feedback(string imeKorisnika, string ulogaKorisnika, string poruka, DateTime vremeSlanja)
        {
            ImeKorisnika = imeKorisnika;
            UlogaKorisnika = ulogaKorisnika;
            Poruka = poruka;
            VremeSlanja = vremeSlanja;
        }
    }
}
