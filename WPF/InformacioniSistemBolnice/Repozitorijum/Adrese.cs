using System;
using Model;
using Newtonsoft.Json;
using System.IO;
using System.Collections.ObjectModel;

namespace Repozitorijum
{
    class Adrese : Repozitorijum
    {
        private string putanja = "../../../json/adrese.json";

        private static readonly Lazy<Adrese>
            lazy =
           new Lazy<Adrese>
               (() => new Adrese());

        public static Adrese Instance { get { return lazy.Value; } }

        public ObservableCollection<Adresa> listaAdresa
        {
            get;
            set;
        }
        public void Deserijalizacija()
        {
            listaAdresa = JsonConvert.DeserializeObject<ObservableCollection<Adresa>>(File.ReadAllText(putanja));
        }

        public void Serijalizacija()
        {
            string json = JsonConvert.SerializeObject(listaAdresa, Formatting.Indented);
            File.WriteAllText(putanja, json);
        }

        public Adrese()
        {
            listaAdresa = new ObservableCollection<Adresa>();
        }

        public void SacuvajPromene()
        {
            Serijalizacija();
            Deserijalizacija();
        }
    }
}
