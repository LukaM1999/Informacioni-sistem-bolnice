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
    public class UpravljanjeTerminima
    {
        private static readonly Lazy<UpravljanjeTerminima>
           Lazy = new(() => new UpravljanjeTerminima());
        public static UpravljanjeTerminima Instance => Lazy.Value;

        public void Zakazivanje(Termin terminZaZakazivanje)
        {
            terminZaZakazivanje.Status = StatusTermina.zakazan;
            UpravljanjeTerminimaPacijenata.Instance.ZakaziTerminKodPacijenta(terminZaZakazivanje);
            UpravljanjeTerminimaLekara.Instance.ZakaziTerminKodLekara(terminZaZakazivanje);
            UpravljanjeTerminimaProstorija.Instance.ZakaziTerminUnutarProstorije(terminZaZakazivanje);
            TerminRepo.Instance.DodajTermin(terminZaZakazivanje);
            TerminRepo.Instance.Serijalizacija();
        }

        public void Otkazivanje(Termin terminZaOtkazivanje)
        {
            UpravljanjeTerminimaPacijenata.Instance.OtkaziTerminKodPacijenta(terminZaOtkazivanje);
            UpravljanjeTerminimaLekara.Instance.OtkaziTerminKodLekara(terminZaOtkazivanje);
            UpravljanjeTerminimaProstorija.Instance.OtkaziTerminUnutarProstorije(terminZaOtkazivanje);
            TerminRepo.Instance.ObrisiTermin(terminZaOtkazivanje);
            TerminRepo.Instance.Serijalizacija();
        }

        public void Pomeranje(Termin terminZaPomeranje, Termin noviTermin)
        {
            noviTermin.Status = StatusTermina.pomeren;
            UpravljanjeTerminimaPacijenata.Instance.PomeriTerminKodPacijenta(terminZaPomeranje, noviTermin);
            UpravljanjeTerminimaLekara.Instance.PomeriTerminKodLekara(terminZaPomeranje, noviTermin);
            UpravljanjeTerminimaProstorija.Instance.PomeriTerminUnutarProstorije(terminZaPomeranje, noviTermin);
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