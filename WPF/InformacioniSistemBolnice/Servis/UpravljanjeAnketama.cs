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
        public static UpravljanjeAnketama Instance { get { return Lazy.Value; } }

        private const int TerminaDoAnkete = 3;
        private const int MesecaDoAnkete = 4;

        public Pacijent ulogovanPacijent;

        public void PopuniAnketuOLekaru(Termin zavrsenTermin)
        {
            foreach (Termin termin in Termini.Instance.listaTermina)
            {
                if (termin.vreme != zavrsenTermin.vreme || termin.pacijentJMBG != zavrsenTermin.pacijentJMBG) continue;
                termin.AnketaOLekaru = zavrsenTermin.AnketaOLekaru;
                Termini.Instance.Serijalizacija();
            }
        }

        public async void OtvoriAnketuOBolnici(Pacijent pacijent)
        {
            ulogovanPacijent = pacijent;
            if (!PacijentPosetioBolnicu(DobaviSortiraneTermine())) return;
            if (PrebrojTermineDoAnkete() < TerminaDoAnkete && !JeVremeZaAnketu()) return;
            AnketaOBolniciFormaView anketaOBolnici = new(ulogovanPacijent.jmbg);
            await Task.Delay(7000);
            anketaOBolnici.Show();
        }

        private int PrebrojTermineDoAnkete()
        {
            int zavrsenihTermina = 0;
            foreach (Termin termin in DobaviSortiraneTermine())
            {
                if (JeNovozavrsen(termin)) zavrsenihTermina++;
                if (JePrvaAnketa(termin)) zavrsenihTermina++;
            }
            return zavrsenihTermina;
        }

        private bool JePrvaAnketa(Termin termin)
        {
            return DobaviPacijentoveAnkete().Count == 0 && termin.status == StatusTermina.zavrsen;
        }

        private bool JeNovozavrsen(Termin termin)
        {
            List<AnketaOBolnici> sortiraneAnkete = DobaviSortiraneAnkete(DobaviPacijentoveAnkete());
            if (sortiraneAnkete.Count <= 1)
                return termin.status == StatusTermina.zavrsen && sortiraneAnkete.Count != 0 &&
                       termin.vreme < sortiraneAnkete[0].VremePopunjavanja;
            return termin.status == StatusTermina.zavrsen && termin.vreme > sortiraneAnkete[1].VremePopunjavanja &&
                   termin.vreme > sortiraneAnkete[0].VremePopunjavanja && termin.vreme < DateTime.Now;
        }

        private bool JeVremeZaAnketu()
        {
            List<AnketaOBolnici> sortiraneAnkete = DobaviSortiraneAnkete(DobaviPacijentoveAnkete());
            return sortiraneAnkete[0].VremePopunjavanja.AddMonths(MesecaDoAnkete) < DateTime.Now;
        }

        private List<AnketaOBolnici> DobaviPacijentoveAnkete()
        {
            List<AnketaOBolnici> pacijentoveAnkete = new();
            foreach (AnketaOBolnici anketa in AnketeOBolnici.Instance.AnketeZaBolnicu.ToList())
            {
                if (anketa.PacijentovJmbg == ulogovanPacijent.jmbg) pacijentoveAnkete.Add(anketa);
            }
            return pacijentoveAnkete;
        }

        private static List<AnketaOBolnici> DobaviSortiraneAnkete(List<AnketaOBolnici> pacijentoveAnkete)
        {
            return pacijentoveAnkete.OrderByDescending(anketa => anketa.VremePopunjavanja).ToList();
        }

        private List<Termin> DobaviSortiraneTermine()
        {
            List<Termin> sortiraniTermini = ulogovanPacijent.zakazaniTermini.OrderBy(termin => termin.vreme).ToList();
            return sortiraniTermini;
        }

        private static bool PacijentPosetioBolnicu(List<Termin> sortiraniTermini)
        {
            return sortiraniTermini.Count != 0 && sortiraniTermini[0].status == StatusTermina.zavrsen;
        }

        public void PopuniAnketuOBolnici(AnketaOBolnici anketa)
        {
            AnketeOBolnici.Instance.AnketeZaBolnicu.Add(anketa);
            AnketeOBolnici.Instance.Serijalizacija();
        }
    }
}
