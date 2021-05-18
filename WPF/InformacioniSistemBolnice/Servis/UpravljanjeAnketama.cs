using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InformacioniSistemBolnice;
using Model;
using Repozitorijum;

namespace Servis
{
    public class UpravljanjeAnketama
    {
        private static readonly Lazy<UpravljanjeAnketama>
            Lazy = new(() => new UpravljanjeAnketama());
        public static UpravljanjeAnketama Instance => Lazy.Value;

        private const int TerminaDoAnkete = 3;
        private const int MesecaDoAnkete = 4;

        private Pacijent ulogovanPacijent;

        public void PopuniAnketuOLekaru(Termin zavrsenTermin, AnketaOLekaru novaAnketa)
        {
            zavrsenTermin.PopuniAnketuOLekaru(novaAnketa);
            Termini.Instance.Serijalizacija();
        }

        public async void OtvoriAnketuOBolnici(Pacijent pacijent)
        {
            ulogovanPacijent = pacijent;
            if (!ulogovanPacijent.PacijentPosetioBolnicu(ulogovanPacijent.DobaviSortiraneTermine())) return;
            if (PrebrojTermineDoAnkete() < TerminaDoAnkete && !JeVremeZaAnketu()) return;
            AnketaOBolniciFormaView anketaOBolnici = new(ulogovanPacijent.jmbg);
            await Task.Delay(7000);
            anketaOBolnici.Show();
        }

        private int PrebrojTermineDoAnkete()
        {
            int zavrsenihTermina = 0;
            foreach (Termin termin in ulogovanPacijent.DobaviSortiraneTermine())
            {
                if (JeNovozavrsen(termin)) zavrsenihTermina++;
                else if (JePrvaAnketa(termin)) zavrsenihTermina++;
            }
            return zavrsenihTermina;
        }

        private bool JePrvaAnketa(Termin termin)
        {
            return AnketeOBolnici.Instance.DobaviPacijentoveAnkete(ulogovanPacijent).Count == 0 &&
                   termin.status == StatusTermina.zavrsen;
        }

        private bool JeNovozavrsen(Termin termin)
        {
            List<AnketaOBolnici> sortiraneAnkete = AnketeOBolnici.Instance.DobaviPacijentoveAnkete(ulogovanPacijent);
            sortiraneAnkete = AnketeOBolnici.Instance.DobaviVremenskiOpadajuciSortiraneAnkete(sortiraneAnkete);
            if (sortiraneAnkete.Count <= 1)
                return termin.status == StatusTermina.zavrsen && sortiraneAnkete.Count != 0 &&
                       termin.vreme < sortiraneAnkete[0].VremePopunjavanja;
            return termin.status == StatusTermina.zavrsen && termin.vreme > sortiraneAnkete[1].VremePopunjavanja &&
                   termin.vreme > sortiraneAnkete[0].VremePopunjavanja && termin.vreme < DateTime.Now;
        }

        private bool JeVremeZaAnketu()
        {
            List<AnketaOBolnici> sortiraneAnkete = AnketeOBolnici.Instance.DobaviPacijentoveAnkete(ulogovanPacijent);
            sortiraneAnkete = AnketeOBolnici.Instance.DobaviVremenskiOpadajuciSortiraneAnkete(sortiraneAnkete);
            if (sortiraneAnkete.Count == 0) return false;
            return sortiraneAnkete[0].VremePopunjavanja.AddMonths(MesecaDoAnkete) < DateTime.Now;
        }

        public void PosaljiAnketuOBolnici(AnketaOBolnici anketa)
        {
            AnketeOBolnici.Instance.DodajAnketu(anketa);
        }
    }
}
