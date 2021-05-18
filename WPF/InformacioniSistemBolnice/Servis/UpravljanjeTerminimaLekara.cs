using System;
using Model;
using Repozitorijum;
using System.Windows.Controls;
using InformacioniSistemBolnice;
using System.Linq;

namespace Servis
{
    public class UpravljanjeTerminimaLekara
    {
        private static readonly Lazy<UpravljanjeTerminimaLekara>
           lazy =
           new Lazy<UpravljanjeTerminimaLekara>
               (() => new UpravljanjeTerminimaLekara());

        public static UpravljanjeTerminimaLekara Instance { get { return lazy.Value; } }

        public void ZakaziTerminKodLekara(Termin terminZaZakazivanje)
        {
            Lekar lekar = Lekari.Instance.NadjiPoJmbg(terminZaZakazivanje.lekarJMBG);
            lekar.DodajTermin(terminZaZakazivanje);
            Lekari.Instance.Serijalizacija();
        }

        public void OtkaziTerminKodLekara(Termin terminZaOtkazivanje)
        {
            Lekar lekar = Lekari.Instance.NadjiPoJmbg(terminZaOtkazivanje.lekarJMBG);
            lekar.ObrisiTermin(terminZaOtkazivanje);
            Lekari.Instance.Serijalizacija();
        }

        public void PomeriTerminKodLekara(Termin terminZaPomeranje, Termin noviTermin)
        {
            Lekar lekar = Lekari.Instance.NadjiPoJmbg(noviTermin.lekarJMBG);
            lekar.ObrisiTermin(terminZaPomeranje);
            lekar.DodajTermin(noviTermin);
            Lekari.Instance.Serijalizacija();
        }
        public void Uvid(DataGrid listaZakazanihTerminaLekara)
        {
            if (listaZakazanihTerminaLekara.SelectedIndex >= 0)
            {
                TerminInfoProzor terminInfo = new TerminInfoProzor(listaZakazanihTerminaLekara);
                terminInfo.Show();
            }
        }
    }
}