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
        private List<AnketaOBolnici> pacijentoveAnkete;

        public async void OtvoriAnketuOBolnici(Pacijent pacijent)
        {
            Inicijalizuj(pacijent);
            if (!ulogovanPacijent.PacijentPosetioBolnicu(ulogovanPacijent.DobaviSortiraneTermine())) return;
            if (PrebrojTermineDoAnkete() < TerminaDoAnkete && !JeVremeZaAnketu()) return;
            AnketaOBolniciFormaView anketaOBolnici = new(ulogovanPacijent.Jmbg);
            await Task.Delay(7000);
            anketaOBolnici.Show();
        }

        private void Inicijalizuj(Pacijent pacijent)
        {
            ulogovanPacijent = pacijent;
            pacijentoveAnkete = AnketaOBolniciRepo.Instance.DobaviPacijentoveAnkete(ulogovanPacijent);
            pacijentoveAnkete = AnketaOBolniciRepo.Instance.DobaviVremenskiOpadajuciSortiraneAnkete(pacijentoveAnkete);
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
            return pacijentoveAnkete.Count == 0 &&
                   termin.Status == StatusTermina.zavrsen;
        }

        private bool JeNovozavrsen(Termin termin)
        {
            if (pacijentoveAnkete.Count <= 1)
                return termin.Status == StatusTermina.zavrsen && pacijentoveAnkete.Count != 0 &&
                       termin.Vreme < pacijentoveAnkete[0].VremePopunjavanja;
            return termin.Status == StatusTermina.zavrsen && termin.Vreme > pacijentoveAnkete[1].VremePopunjavanja &&
                   termin.Vreme > pacijentoveAnkete[0].VremePopunjavanja && termin.Vreme < DateTime.Now;
        }

        private bool JeVremeZaAnketu()
        {
            if (pacijentoveAnkete.Count == 0) return false;
            return pacijentoveAnkete[0].VremePopunjavanja.AddMonths(MesecaDoAnkete) < DateTime.Now;
        }
    }
}
