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
    public class PrikazBolnickeAnketeServis
    {
        private static readonly Lazy<PrikazBolnickeAnketeServis> Lazy = new(() => new PrikazBolnickeAnketeServis());
        public static PrikazBolnickeAnketeServis Instance => Lazy.Value;

        private const int TerminaDoAnkete = 3;
        private const int MesecaDoAnkete = 4;

        private Pacijent ulogovanPacijent;

        public async void OtvoriAnketuOBolnici(Pacijent pacijent)
        {
            ulogovanPacijent = pacijent;
            if (!ulogovanPacijent.PacijentPosetioBolnicu(ulogovanPacijent.DobaviSortiraneTermine())) return;
            if (PrebrojTermineDoAnkete() < TerminaDoAnkete && !JeVremeZaAnketu()) return;
            AnketaOBolniciFormaView anketaOBolnici = new(ulogovanPacijent.Jmbg);
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
            return AnketaOBolniciRepo.Instance.DobaviPacijentoveAnkete(ulogovanPacijent).Count == 0 &&
                   termin.Status == StatusTermina.zavrsen;
        }

        private bool JeNovozavrsen(Termin termin)
        {
            List<AnketaOBolnici> sortiraneAnkete = AnketaOBolniciRepo.Instance.DobaviPacijentoveAnkete(ulogovanPacijent);
            sortiraneAnkete = AnketaOBolniciRepo.Instance.DobaviVremenskiOpadajuciSortiraneAnkete(sortiraneAnkete);
            if (sortiraneAnkete.Count <= 1)
                return termin.Status == StatusTermina.zavrsen && sortiraneAnkete.Count != 0 &&
                       termin.Vreme < sortiraneAnkete[0].VremePopunjavanja;
            return termin.Status == StatusTermina.zavrsen && termin.Vreme > sortiraneAnkete[1].VremePopunjavanja &&
                   termin.Vreme > sortiraneAnkete[0].VremePopunjavanja && termin.Vreme < DateTime.Now;
        }

        private bool JeVremeZaAnketu()
        {
            List<AnketaOBolnici> sortiraneAnkete = AnketaOBolniciRepo.Instance.DobaviPacijentoveAnkete(ulogovanPacijent);
            sortiraneAnkete = AnketaOBolniciRepo.Instance.DobaviVremenskiOpadajuciSortiraneAnkete(sortiraneAnkete);
            if (sortiraneAnkete.Count == 0) return false;
            return sortiraneAnkete[0].VremePopunjavanja.AddMonths(MesecaDoAnkete) < DateTime.Now;
        }
    }
}
