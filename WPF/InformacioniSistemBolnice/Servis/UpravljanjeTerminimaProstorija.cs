using System;
using Model;
using Repozitorijum;
using System.Windows.Controls;
using InformacioniSistemBolnice;
using System.Linq;

namespace Servis
{
    public class UpravljanjeTerminimaProstorija
    {
        private static readonly Lazy<UpravljanjeTerminimaProstorija>
           Lazy = new(() => new UpravljanjeTerminimaProstorija());
        public static UpravljanjeTerminimaProstorija Instance => Lazy.Value;

        public void ZakaziTerminUnutarProstorije(Termin terminZaZakazivanje)
        {
            Prostorija prostorija = Prostorije.Instance.NadjiPoId(terminZaZakazivanje.idProstorije);
            prostorija.DodajTermin(terminZaZakazivanje);
            Prostorije.Instance.Serijalizacija();
        }

        public void OtkaziTerminUnutarProstorije(Termin terminZaOtkazivanje)
        {
            Prostorija prostorija = Prostorije.Instance.NadjiPoId(terminZaOtkazivanje.idProstorije);
            prostorija.ObrisiTermin(terminZaOtkazivanje);
            Prostorije.Instance.Serijalizacija();
        }

        public void PomeriTerminUnutarProstorije(Termin terminZaPomeranje, Termin noviTermin)
        {
            Prostorija prostorija = Prostorije.Instance.NadjiPoId(noviTermin.idProstorije);
            prostorija.ObrisiTermin(terminZaPomeranje);
            prostorija.DodajTermin(noviTermin);
            Prostorije.Instance.Serijalizacija();
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