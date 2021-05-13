using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class TerminiLekaraZaPomeranjeDto
    {
        public Termin terminZaPomeranje { get; set; }
        public Termin noviTermin { get; set; }
        public string jmbgPacijenta { get; set; }

        public TerminiLekaraZaPomeranjeDto(Termin terminZaPomeranje, Termin noviTermin, string jmbgPacijenta)
        {
            this.terminZaPomeranje = terminZaPomeranje;
            this.noviTermin = noviTermin;
            this.jmbgPacijenta = jmbgPacijenta;
        }
    }
}
