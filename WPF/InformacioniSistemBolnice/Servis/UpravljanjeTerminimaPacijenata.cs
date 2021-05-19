using System;
using Model;
using Repozitorijum;
using System.Windows.Controls;
using InformacioniSistemBolnice;
using System.Linq;

namespace Servis
{
    public class UpravljanjeTerminimaPacijenata
    {
        private static readonly Lazy<UpravljanjeTerminimaPacijenata>
           Lazy = new(() => new UpravljanjeTerminimaPacijenata());
        public static UpravljanjeTerminimaPacijenata Instance => Lazy.Value;

        public void ZakaziTerminKodPacijenta(Termin terminZaZakazivanje)
        {
            Pacijent pacijent = Pacijenti.Instance.NadjiPoJmbg(terminZaZakazivanje.pacijentJMBG);
            pacijent.DodajTermin(terminZaZakazivanje);
            Pacijenti.Instance.Serijalizacija();
        }

        public void OtkaziTerminKodPacijenta(Termin terminZaOtkazivanje)
        {
            Pacijent pacijent = Pacijenti.Instance.NadjiPoJmbg(terminZaOtkazivanje.pacijentJMBG);
            pacijent.ObrisiTermin(terminZaOtkazivanje);
            Pacijenti.Instance.Serijalizacija();
        }

        public void PomeriTerminKodPacijenta(Termin terminZaPomeranje, Termin noviTermin)
        {
            Pacijent pacijent = Pacijenti.Instance.NadjiPoJmbg(noviTermin.pacijentJMBG);
            pacijent.ObrisiTermin(terminZaPomeranje);
            pacijent.DodajTermin(noviTermin);
            Pacijenti.Instance.Serijalizacija();
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