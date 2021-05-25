using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InformacioniSistemBolnice;
using Model;
using Repozitorijum;

namespace Servis
{
    public class SlanjeAnketeServis
    {
        private static readonly Lazy<SlanjeAnketeServis>
            Lazy = new(() => new SlanjeAnketeServis());
        public static SlanjeAnketeServis Instance => Lazy.Value;

        public void PosaljiAnketuOLekaru(Termin zavrsenTermin, AnketaOLekaru novaAnketa)
        {
            zavrsenTermin.PopuniAnketuOLekaru(novaAnketa);
            TerminRepo.Instance.Serijalizacija();
        }

        public void PosaljiAnketuOBolnici(AnketaOBolnici anketa)
        {
            AnketaOBolniciRepo.Instance.DodajAnketu(anketa);
        }
    }
}
