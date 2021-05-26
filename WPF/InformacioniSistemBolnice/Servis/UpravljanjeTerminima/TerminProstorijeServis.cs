using System;
using Model;
using Repozitorijum;
using System.Windows.Controls;
using InformacioniSistemBolnice;
using System.Linq;

namespace Servis
{
    public class TerminProstorijeServis
    {
        private static readonly Lazy<TerminProstorijeServis>
           Lazy = new(() => new TerminProstorijeServis());
        public static TerminProstorijeServis Instance => Lazy.Value;

        public void ZakaziTerminUnutarProstorije(Termin terminZaZakazivanje)
        {
            Prostorija prostorija = ProstorijaRepo.Instance.NadjiPoId(terminZaZakazivanje.ProstorijaId);
            prostorija.DodajTermin(terminZaZakazivanje);
            ProstorijaRepo.Instance.Serijalizacija();
        }

        public void OtkaziTerminUnutarProstorije(Termin terminZaOtkazivanje)
        {
            Prostorija prostorija = ProstorijaRepo.Instance.NadjiPoId(terminZaOtkazivanje.ProstorijaId);
            prostorija.ObrisiTermin(terminZaOtkazivanje);
            ProstorijaRepo.Instance.Serijalizacija();
        }

        public void PomeriTerminUnutarProstorije(Termin terminZaPomeranje, Termin noviTermin)
        {
            Prostorija prostorija = ProstorijaRepo.Instance.NadjiPoId(noviTermin.ProstorijaId);
            prostorija.ObrisiTermin(terminZaPomeranje);
            prostorija.DodajTermin(noviTermin);
            ProstorijaRepo.Instance.Serijalizacija();
        }
    }
}