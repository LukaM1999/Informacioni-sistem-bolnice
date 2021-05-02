using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class LekDto
    {
        public string naziv
        {
            get;
            set;
        }
        public string proizvodjac
        {
            get;
            set;
        }
        public string sastojci
        {
            get;
            set;
        }

        public LekDto(String naziv, String proizvodjac, String sastojci)
        {
            this.naziv = naziv;
            this.proizvodjac = proizvodjac;
            this.sastojci = sastojci;
        }
    }
}
