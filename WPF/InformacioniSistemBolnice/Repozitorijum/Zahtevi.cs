using System;
using Model;
using Newtonsoft.Json;
using System.IO;
using System.Collections.ObjectModel;

namespace Repozitorijum
{
    public class Zahtevi : Repozitorijum
    {
        private string putanja = "../../../json/zahtevi.json";

        private static readonly Lazy<Zahtevi>
           lazy =
           new Lazy<Zahtevi>
               (() => new Zahtevi());

        public static Zahtevi Instance { get { return lazy.Value; } }

        public ObservableCollection<Zahtev> listaZahteva
        {
            get;
            set;
        }

        public void Deserijalizacija()
        {
            listaZahteva = JsonConvert.DeserializeObject<ObservableCollection<Zahtev>>(File.ReadAllText(putanja));
        }
        
        public void Serijalizacija()
        {
            string json = JsonConvert.SerializeObject(listaZahteva, Formatting.Indented);
            File.WriteAllText(putanja, json);
        }

        public Zahtevi()
        {
            listaZahteva = new ObservableCollection<Zahtev>();
        }

        public void SacuvajPromene()
        {
            Serijalizacija();
            Deserijalizacija();
        }
    }
}