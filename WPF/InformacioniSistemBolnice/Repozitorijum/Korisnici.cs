using System;
using Model;
using System.IO;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace Repozitorijum
{
    public class Korisnici : Repozitorijum
    {
        private string putanja = "../../../json/korisnici.json";

        private static readonly Lazy<Korisnici>
            lazy =
            new Lazy<Korisnici>
                (() => new Korisnici());

        public static Korisnici Instance { get { return lazy.Value; } }

        public ObservableCollection<Korisnik> listaKorisnika
        {
            get;
            set;
        }

        public void Deserijalizacija()
        {
            listaKorisnika = JsonConvert.DeserializeObject<ObservableCollection<Korisnik>>(File.ReadAllText(putanja));
        }

        public void Serijalizacija()
        {
            string json = JsonConvert.SerializeObject(listaKorisnika, Formatting.Indented);
            File.WriteAllText(putanja, json);
        }

        public Korisnici()
        {
            listaKorisnika = new ObservableCollection<Korisnik>();
        }

    }
}