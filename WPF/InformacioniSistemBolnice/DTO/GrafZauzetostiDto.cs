using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InformacioniSistemBolnice.DTO
{
    public class GrafZauzetostiDto
    {
        public string Vreme { get; set; }
        public int BrojTermina { get; set; }

        public GrafZauzetostiDto(string vreme, int brojTermina) 
        {
            Vreme = vreme;
            BrojTermina = brojTermina;
        }
    }
}
