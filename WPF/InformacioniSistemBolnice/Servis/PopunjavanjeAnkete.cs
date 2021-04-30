using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Repozitorijum;

namespace InformacioniSistemBolnice.Servis
{
    class PopunjavanjeAnkete
    {
        private static readonly Lazy<PopunjavanjeAnkete>
            Lazy = new(() => new PopunjavanjeAnkete());
        public static PopunjavanjeAnkete Instance { get { return Lazy.Value; } }

        public void PopuniAnketuOLekaru(Termin zavrsenTermin)
        {
            foreach (Termin termin in Termini.Instance.listaTermina)
            {
                if (termin.vreme != zavrsenTermin.vreme || termin.pacijentJMBG != zavrsenTermin.pacijentJMBG) continue;
                termin.AnketaOLekaru = zavrsenTermin.AnketaOLekaru;
                Termini.Instance.Serijalizacija();
                Termini.Instance.Deserijalizacija();
            }
        }
    }
}
