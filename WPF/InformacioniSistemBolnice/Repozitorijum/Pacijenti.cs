using System;
using Model;
using Newtonsoft.Json;
using System.IO;
using System.Collections.ObjectModel;

namespace Repozitorijum
{

    public sealed class Pacijenti : Repozitorijum
    {
        private const string Putanja = "../../../json/pacijenti.json";

        private static readonly Lazy<Pacijenti> Lazy = new(() => new Pacijenti());
        public static Pacijenti Instance => Lazy.Value;

        public ObservableCollection<Pacijent> ListaPacijenata { get; set; }

        public Pacijenti()
        {
            ListaPacijenata = new ObservableCollection<Pacijent>();
        }

        public void Deserijalizacija()
        {
            lock (ListaPacijenata) 
                ListaPacijenata = JsonConvert.DeserializeObject<ObservableCollection<Pacijent>>(File.ReadAllText(Putanja));
        }

        public void Serijalizacija()
        {
            lock (ListaPacijenata)
            {
                string json = JsonConvert.SerializeObject(ListaPacijenata, Formatting.Indented);
                File.WriteAllText(Putanja, json);
            }
        }

        public Pacijent NadjiPoJmbg(string jmbg)
        {
            foreach (Pacijent pronadjen in ListaPacijenata) 
                if (pronadjen.jmbg == jmbg) return pronadjen;
            return null;
        }

        public bool BrisiPoJmbg(string jmbg)
        {
            foreach (Pacijent pronadjen in ListaPacijenata)
                if (pronadjen.jmbg == jmbg) return ListaPacijenata.Remove(pronadjen);
            return false;
        }

        public void PostaviTermineUlogovanogPacijenta(Pacijent ulogovanPacijent)
        {
            ObservableCollection<Termin> zakazaniTermini = new();
            foreach (Termin termin in Termini.Instance.listaTermina)
                if (termin.pacijentJMBG == ulogovanPacijent.jmbg) zakazaniTermini.Add(termin);
            ulogovanPacijent.zakazaniTermini = zakazaniTermini;
            Serijalizacija();
        }

        public Pacijent PronadjiUlogovanogPacijenta(string korisnickoIme, string lozinka)
        {
            foreach (Pacijent pacijent in ListaPacijenata)
                if (JeUlogovaniPacijent(korisnickoIme, lozinka, pacijent)) return pacijent;
            return null;
        }

        public static bool JeUlogovaniPacijent(string korisnickoIme, string lozinka, Pacijent pacijent)
        {
            return pacijent.korisnik.korisnickoIme == korisnickoIme && pacijent.korisnik.lozinka == lozinka;
        }

        public void SacuvajPromene()
        {
            Serijalizacija();
            Deserijalizacija();
        }
    }
}