using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InformacioniSistemBolnice;
using InformacioniSistemBolnice.DTO;
using InformacioniSistemBolnice.ViewModels.PacijentViewModel;
using Model;
using Repozitorijum;

namespace Servis
{
    public class SlanjeAnketeServis
    {
        private static readonly Lazy<SlanjeAnketeServis>
            Lazy = new(() => new SlanjeAnketeServis());
        public static SlanjeAnketeServis Instance => Lazy.Value;

        public void PosaljiAnketuOLekaru(Termin zavrsenTermin, AnketaDto novaAnketa)
        {
            AnketaOLekaru popunjena = new AnketaOLekaru(novaAnketa.Ljubaznost, novaAnketa.Profesionalizam,
                novaAnketa.Strpljenje, novaAnketa.Komunikativnost, novaAnketa.Azurnost,
                novaAnketa.Korisnost, novaAnketa.GeneralniKomentari);
            zavrsenTermin.PopuniAnketuOLekaru(popunjena);
            TerminRepo.Instance.Serijalizacija();
        }

        public void PosaljiAnketuOBolnici(AnketaDto anketa)
        {
            AnketaOBolniciRepo.Instance.DodajAnketu(new AnketaOBolnici(anketa.Ljubaznost, anketa.Profesionalizam, 
                anketa.Strpljenje, anketa.Komunikativnost, anketa.Azurnost,
                anketa.Korisnost, anketa.GeneralniKomentari, anketa.PacijentovJmbg, anketa.VremePopunjavanja));
        }
    }
}
