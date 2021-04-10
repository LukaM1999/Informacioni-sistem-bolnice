using System;
using Model;
using System.IO;
using Newtonsoft.Json;

namespace Repozitorijum
{
    public class Korisnici : Repozitorijum
    {
        public System.Collections.Generic.List<Korisnik> listaKorisnika
        {
            get;
            set;
        }

        public void Deserijalizacija(string putanja)
        {
            listaKorisnika = JsonConvert.DeserializeObject<System.Collections.Generic.List<Korisnik>>(File.ReadAllText(putanja));
        }

        public void Serijalizacija(string putanja)
        {
            string json = JsonConvert.SerializeObject(listaKorisnika, Formatting.Indented);
            File.WriteAllText(putanja, json);
        }

        public Korisnici()
        {
            listaKorisnika = new System.Collections.Generic.List<Korisnik>();
        }

    }
}