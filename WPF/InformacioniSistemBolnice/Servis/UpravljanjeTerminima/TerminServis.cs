using System;
using Model;
using Repozitorijum;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using InformacioniSistemBolnice;
using System.Linq;

namespace Servis
{
    public class TerminServis
    {
        private static readonly Lazy<TerminServis> Lazy = new(() => new TerminServis());
        public static TerminServis Instance => Lazy.Value;

        public void Zakazivanje(Termin terminZaZakazivanje)
        {
            terminZaZakazivanje.Status = StatusTermina.zakazan;
            TerminPacijentaServis.Instance.ZakaziTerminKodPacijenta(terminZaZakazivanje);
            TerminLekaraServis.Instance.ZakaziTerminKodLekara(terminZaZakazivanje);
            TerminProstorijeServis.Instance.ZakaziTerminUnutarProstorije(terminZaZakazivanje);
            TerminRepo.Instance.DodajTermin(terminZaZakazivanje);
            TerminRepo.Instance.Serijalizacija();
        }

        public void Otkazivanje(Termin terminZaOtkazivanje)
        {
            TerminPacijentaServis.Instance.OtkaziTerminKodPacijenta(terminZaOtkazivanje);
            TerminLekaraServis.Instance.OtkaziTerminKodLekara(terminZaOtkazivanje);
            TerminProstorijeServis.Instance.OtkaziTerminUnutarProstorije(terminZaOtkazivanje);
            TerminRepo.Instance.ObrisiTermin(terminZaOtkazivanje);
            TerminRepo.Instance.Serijalizacija();
        }

        public void Pomeranje(Termin terminZaPomeranje, Termin noviTermin)
        {
            noviTermin.Status = StatusTermina.pomeren;
            TerminPacijentaServis.Instance.PomeriTerminKodPacijenta(terminZaPomeranje, noviTermin);
            TerminLekaraServis.Instance.PomeriTerminKodLekara(terminZaPomeranje, noviTermin);
            TerminProstorijeServis.Instance.PomeriTerminUnutarProstorije(terminZaPomeranje, noviTermin);
            TerminRepo.Instance.ObrisiTermin(terminZaPomeranje);
            TerminRepo.Instance.DodajTermin(noviTermin);
            TerminRepo.Instance.Serijalizacija();

        }

        public void Uvid(DataGrid listaZakazanihTermina)
        {
            new TerminInfoProzor(listaZakazanihTermina).Show();
        }

    }
}