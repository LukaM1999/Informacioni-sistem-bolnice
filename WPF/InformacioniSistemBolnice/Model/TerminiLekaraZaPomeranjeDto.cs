using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class TerminiLekaraZaPomeranjeDto
    {
        public Termin TerminZaPomeranje { get; set; }
        public Termin NoviTermin { get; set; }
        public string PacijentJmbg { get; set; }

        public TerminiLekaraZaPomeranjeDto(Termin terminZaPomeranje, Termin noviTermin, string jmbgPacijenta)
        {
            this.TerminZaPomeranje = terminZaPomeranje;
            this.NoviTermin = noviTermin;
            this.PacijentJmbg = jmbgPacijenta;
        }
    }
}
