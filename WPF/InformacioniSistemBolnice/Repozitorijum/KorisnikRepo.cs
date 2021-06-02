using System;
using Model;
using System.IO;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace Repozitorijum
{
    public class KorisnikRepo : IRepozitorijum
    {
        private const string Putanja = "../../../json/korisnici.json";

        private static readonly Lazy<KorisnikRepo> Lazy = new(() => new KorisnikRepo());
        public static KorisnikRepo Instance => Lazy.Value;

        public ObservableCollection<Korisnik> Korisnici { get; set; }

        public ObservableCollection<object> Deserijalizacija()
        {
            Korisnici = JsonConvert.DeserializeObject<ObservableCollection<Korisnik>>(File.ReadAllText(Putanja));
            return new ObservableCollection<object> {Korisnici};
        }

        public void Serijalizacija()
        {
            File.WriteAllText(Putanja, JsonConvert.SerializeObject(Korisnici, Formatting.Indented));
        }

        public KorisnikRepo()
        {
            Korisnici = new ObservableCollection<Korisnik>();
        }

        public Korisnik NadjiKorisnika(string korisnickoIme)
        {
            foreach (Korisnik pronadjen in Korisnici)
                if (pronadjen.KorisnickoIme == korisnickoIme) return pronadjen;
            return null;
        }

        public bool DodajKorisnika(Korisnik korisnikZaDodavanje)
        {
            if (Korisnici.Contains(korisnikZaDodavanje)) return false;
            Korisnici.Add(korisnikZaDodavanje);
            Serijalizacija();
            return true;
        }

        public bool ObrisiKorisnika(Korisnik korisnik)
        {
            return Korisnici.Remove(NadjiKorisnika(korisnik.KorisnickoIme));
        }
    }
}