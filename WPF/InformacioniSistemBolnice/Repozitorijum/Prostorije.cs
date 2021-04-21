using System;
using Model;
using Newtonsoft.Json;
using System.IO;
using System.Collections.ObjectModel;
using System.Linq;

namespace Repozitorijum
{
    public sealed class Prostorije : Repozitorijum
    {
        private static readonly Lazy<Prostorije>
            lazy =
            new Lazy<Prostorije>
                (() => new Prostorije());
            
            public static Prostorije Instance { get { return lazy.Value; } }

        public ObservableCollection<Prostorija> listaProstorija
        {
            get;
            set;
        }

        public void Deserijalizacija(string putanja)
        {
            listaProstorija = JsonConvert.DeserializeObject<ObservableCollection<Prostorija>>(File.ReadAllText(putanja));
        }

        public void Serijalizacija(string putanja)
        {
            string json = JsonConvert.SerializeObject(listaProstorija, Formatting.Indented);
            File.WriteAllText(putanja, json);
        }

        public Prostorija getSelected(Prostorija p)
        {
            return listaProstorija.ElementAt(listaProstorija.IndexOf(p));
        }

        private Prostorije()
        {
            listaProstorija = new ObservableCollection<Prostorija>();
        }

    }
}