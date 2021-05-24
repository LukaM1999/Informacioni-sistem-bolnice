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
            Pacijent pacijent = PacijentRepo.Instance.NadjiPoJmbg(terminZaZakazivanje.PacijentJmbg);
            pacijent.DodajTermin(terminZaZakazivanje);
            PacijentRepo.Instance.Serijalizacija();
        }

        public void OtkaziTerminKodPacijenta(Termin terminZaOtkazivanje)
        {
            Pacijent pacijent = PacijentRepo.Instance.NadjiPoJmbg(terminZaOtkazivanje.PacijentJmbg);
            pacijent.ObrisiTermin(terminZaOtkazivanje);
            PacijentRepo.Instance.Serijalizacija();
        }

        public void PomeriTerminKodPacijenta(Termin terminZaPomeranje, Termin noviTermin)
        {
            Pacijent pacijent = PacijentRepo.Instance.NadjiPoJmbg(noviTermin.PacijentJmbg);
            pacijent.ObrisiTermin(terminZaPomeranje);
            pacijent.DodajTermin(noviTermin);
            PacijentRepo.Instance.Serijalizacija();
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