using System;
using Model;
using Repozitorijum;
using System.Windows.Controls;
using InformacioniSistemBolnice;
using System.Linq;

namespace Servis
{
    public class TerminLekaraServis
    {
        private static readonly Lazy<TerminLekaraServis> Lazy = new(() => new TerminLekaraServis());
        public static TerminLekaraServis Instance => Lazy.Value;

        public void ZakaziTerminKodLekara(Termin terminZaZakazivanje)
        {
            Lekar lekar = LekarRepo.Instance.NadjiLekara(terminZaZakazivanje.LekarJmbg);
            lekar.DodajTermin(terminZaZakazivanje);
            LekarRepo.Instance.Serijalizacija();
        }

        public void OtkaziTerminKodLekara(Termin terminZaOtkazivanje)
        {
            Lekar lekar = LekarRepo.Instance.NadjiLekara(terminZaOtkazivanje.LekarJmbg);
            lekar.ObrisiTermin(terminZaOtkazivanje);
            LekarRepo.Instance.Serijalizacija();
        }

        public void PomeriTerminKodLekara(Termin terminZaPomeranje, Termin noviTermin)
        {
            Lekar lekar = LekarRepo.Instance.NadjiLekara(noviTermin.LekarJmbg);
            lekar.ObrisiTermin(terminZaPomeranje);
            lekar.DodajTermin(noviTermin);
            LekarRepo.Instance.Serijalizacija();
        }
    }
}