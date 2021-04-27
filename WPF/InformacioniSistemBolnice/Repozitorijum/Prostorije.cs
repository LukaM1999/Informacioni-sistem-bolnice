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
            Prostorija prs = null;
            foreach (Prostorija pr in listaProstorija)
            {
                if (pr.id.Equals(p.id))
                {
                    prs = pr;
                }
            }
            return listaProstorija.ElementAt(listaProstorija.IndexOf(prs));
        }

        private Prostorije()
        {
            listaProstorija = new ObservableCollection<Prostorija>();
        }

    }
}