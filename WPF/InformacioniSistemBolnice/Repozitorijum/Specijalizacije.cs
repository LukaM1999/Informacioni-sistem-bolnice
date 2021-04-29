using System;
using Model;
using Newtonsoft.Json;
using System.IO;
using System.Collections.ObjectModel;
using InformacioniSistemBolnice;

namespace Repozitorijum
{
    class Specijalizacije : Repozitorijum
    {
        private string putanja = "../../../json/specijalizacije.json";

        private static readonly Lazy<Specijalizacije>
            lazy =
           new Lazy<Specijalizacije>
               (() => new Specijalizacije());

        public static Specijalizacije Instance { get { return lazy.Value; } }

        public ObservableCollection<Adresa> listaSpecijalizacija
        {
            get;
            set;
        }

        public void Deserijalizacija()
        {
            listaSpecijalizacija = JsonConvert.DeserializeObject<ObservableCollection<Adresa>>(File.ReadAllText(putanja));
        }

        public void Serijalizacija()
        {
            string json = JsonConvert.SerializeObject(listaSpecijalizacija, Formatting.Indented);
            File.WriteAllText(putanja, json);
        }

        public Specijalizacije()
        {
            listaSpecijalizacija = new ObservableCollection<Adresa>();
        }

    }
}
