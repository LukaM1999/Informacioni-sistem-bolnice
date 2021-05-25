using System;
using Model;
using Newtonsoft.Json;
using System.IO;
using System.Collections.ObjectModel;

namespace Repozitorijum
{
    public class PacijentRepo : Repozitorijum
    {
        private const string Putanja = "../../../json/pacijenti.json";

        private static readonly Lazy<PacijentRepo> Lazy = new(() => new PacijentRepo());
        public static PacijentRepo Instance => Lazy.Value;

        public ObservableCollection<Pacijent> Pacijenti { get; set; }

        public PacijentRepo()
        {
            Pacijenti = new ObservableCollection<Pacijent>();
        }

        public void Deserijalizacija()
        {
            lock (Pacijenti)
                Pacijenti = JsonConvert.DeserializeObject<ObservableCollection<Pacijent>>(File.ReadAllText(Putanja));
        }

        public void Serijalizacija()
        {
            lock (Pacijenti)
                File.WriteAllText(Putanja, JsonConvert.SerializeObject(Pacijenti, Formatting.Indented));
        }

        public Pacijent NadjiPoJmbg(string jmbg)
        {
            foreach (Pacijent pronadjen in Pacijenti)
                if (pronadjen.Jmbg == jmbg) return pronadjen;
            return null;
        }

        public Pacijent NadjiRegistrovanogPacijenta()
        {
            foreach (Pacijent pronadjen in Pacijenti)
                if (pronadjen.Korisnik.KorisnickoIme != null) return pronadjen;
            return null;
        }

        public bool BrisiPoJmbg(string jmbg)
        {
            foreach (Pacijent pronadjen in Pacijenti)
                if (pronadjen.Jmbg == jmbg) return Pacijenti.Remove(pronadjen);
            return false;
        }

        public void PostaviTermineUlogovanogPacijenta(Pacijent ulogovanPacijent)
        {
            ObservableCollection<Termin> zakazaniTermini = new();
            foreach (Termin termin in TerminRepo.Instance.Termini)
                if (termin.PacijentJmbg == ulogovanPacijent.Jmbg) zakazaniTermini.Add(termin);
            ulogovanPacijent.ZakazaniTermini = zakazaniTermini;
            Serijalizacija();
        }

        public Pacijent PronadjiUlogovanogPacijenta(string korisnickoIme, string lozinka)
        {
            foreach (Pacijent pacijent in Pacijenti)
                if (JeUlogovaniPacijent(korisnickoIme, lozinka, pacijent)) return pacijent;
            return null;
        }

        public Pacijent PronadjiGostujucegPacijenta()
        {
            foreach (Pacijent gostujuciPacijent in PacijentRepo.Instance.Pacijenti)
                if (gostujuciPacijent.Korisnik.KorisnickoIme == null) return gostujuciPacijent;
            return null;
        }

        public ObservableCollection<Pacijent> ListaPacijenata()
        {
            ObservableCollection < Pacijent > pacijenti = new();
            foreach (Pacijent pacijent in PacijentRepo.Instance.Pacijenti)
            {
                if (pacijent.Korisnik.KorisnickoIme != null) 
                    pacijenti.Add(pacijent);
            }
            return pacijenti;
        }

        public ObservableCollection<Pacijent> GenerisiGostujucePacijente()
        {
            ObservableCollection<Pacijent> gostujuciPacijenti = new ObservableCollection<Pacijent>();
            gostujuciPacijenti.Add(PronadjiGostujucegPacijenta());
            return gostujuciPacijenti;
        }

        public ZdravstveniKarton PronadjiZdravstveniKarton(string jmbg)
        {
            foreach (Pacijent p in PacijentRepo.Instance.Pacijenti)
                if (p.zdravstveniKarton.Jmbg.Equals(jmbg)) return p.zdravstveniKarton;
            return null;
        }

        public bool JeUlogovaniPacijent(string korisnickoIme, string lozinka, Pacijent pacijent)
        {
            return pacijent.Korisnik.KorisnickoIme == korisnickoIme && pacijent.Korisnik.Lozinka == lozinka;
        }

        public bool DodajPacijenta(Pacijent pacijentZaDodavanje)
        {
            if (Pacijenti.Contains(pacijentZaDodavanje)) return false;
            Pacijenti.Add(pacijentZaDodavanje);
            Serijalizacija();
            return true;
        }

        public bool ObrisiPacijenta(Pacijent pacijentZaBrisanje)
        {
            return Pacijenti.Remove(NadjiPoJmbg(pacijentZaBrisanje.Jmbg));
        }
    }
}