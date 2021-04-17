using System;
using Model;
using Newtonsoft.Json;
using System.IO;
using System.Collections.ObjectModel;

namespace Repozitorijum
{
    public class Lekari : Repozitorijum
    {
        private static readonly Lazy<Lekari>
           lazy =
           new Lazy<Lekari>
               (() => new Lekari());

        public static Lekari Instance { get { return lazy.Value; } }

        public ObservableCollection<Lekar> listaLekara
        {
            get;
            set;
        }

        public void Deserijalizacija(string putanja)
        {
            listaLekara = JsonConvert.DeserializeObject<ObservableCollection<Lekar>>(File.ReadAllText(putanja));
        }

        public void Serijalizacija(string putanja)
        {
            string json = JsonConvert.SerializeObject(listaLekara, Formatting.Indented);
            File.WriteAllText(putanja, json);
        }

    }
}