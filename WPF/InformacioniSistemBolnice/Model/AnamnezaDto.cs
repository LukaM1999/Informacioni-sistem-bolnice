using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InformacioniSistemBolnice;

namespace Model
{
    public class AnamnezaDto
    {
        public string PacijentJmbg { get; set; }
        public string SadasnjaBolest { get; set; }
        public string IstorijaBolesti { get; set; }
        public string PorodicneBolesti { get; set; }
        public string Zakljucak { get; set; }

        public AnamnezaDto(string jmbg, string sadasnjaBolest, string istorijaBolesti, string porodicneBolesti, string zakljucak)
        {
            PacijentJmbg = jmbg;
            SadasnjaBolest = sadasnjaBolest;
            IstorijaBolesti = istorijaBolesti;
            PorodicneBolesti = porodicneBolesti;
            Zakljucak = zakljucak;
        }
    }
}
