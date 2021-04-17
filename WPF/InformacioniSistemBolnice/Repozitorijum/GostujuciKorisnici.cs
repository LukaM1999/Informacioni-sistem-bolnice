using System;
using Model;
using Newtonsoft.Json;
using System.IO;
using System.Collections.ObjectModel;

namespace Repozitorijum
{
    public class GostujuciKorisnici : Repozitorijum
    {
        private static readonly Lazy<GostujuciKorisnici>
           lazy =
           new Lazy<GostujuciKorisnici>
               (() => new GostujuciKorisnici());

        public static GostujuciKorisnici Instance { get { return lazy.Value; } }

        public ObservableCollection<GostujuciKorisnik> listaGostujucihKorisnika
        {
            get;
            set;
        }

        public void Deserijalizacija(string putanja)
        {
            listaGostujucihKorisnika = JsonConvert.DeserializeObject<ObservableCollection<GostujuciKorisnik>>(File.ReadAllText(putanja));
        }

        public void Serijalizacija(string putanja)
        {
            string json = JsonConvert.SerializeObject(listaGostujucihKorisnika, Formatting.Indented);
            File.WriteAllText(putanja, json);
        }

    }
}