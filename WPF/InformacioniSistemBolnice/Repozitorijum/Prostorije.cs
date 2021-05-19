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
        private string putanja = "../../../json/prostorije.json";

        private static readonly Lazy<Prostorije>
            lazy =
            new Lazy<Prostorije>
                (() => new Prostorije());
            
            public static Prostorije Instance { get { return lazy.Value; } }

        public ObservableCollection<Prostorija> ListaProstorija
        {
            get;
            set;
        }

        public void Deserijalizacija()
        {
            lock (ListaProstorija)
            {
                ListaProstorija = JsonConvert.DeserializeObject<ObservableCollection<Prostorija>>(File.ReadAllText(putanja));
            }
        }

        public void Serijalizacija()
        {
            lock (ListaProstorija)
            {
                string json = JsonConvert.SerializeObject(ListaProstorija, Formatting.Indented);
                File.WriteAllText(putanja, json);
            }
        }

        public Prostorija uzmiIzabranuProstoriju(Prostorija izabranaProstorija)
        {
            Prostorija prostorija = null;
            foreach (Prostorija p in ListaProstorija)
            {
                if (p.Id.Equals(izabranaProstorija.Id))
                {
                    prostorija = p;
                }
            }
            return ListaProstorija.ElementAt(ListaProstorija.IndexOf(prostorija));
        }

        public Prostorija NadjiPoId(string idProstorije)
        {
            foreach (Prostorija prostorija in ListaProstorija) if (prostorija.Id == idProstorije) return prostorija;
            return null;
        }

        public bool BrisiPoId(string idProstorije)
        {
            foreach (Prostorija pronadjena in ListaProstorija)
                if (pronadjena.Id == idProstorije) return ListaProstorija.Remove(pronadjena);
            return false;
        }

        private Prostorije()
        {
            ListaProstorija = new ObservableCollection<Prostorija>();
        }
        public void SacuvajPromene()
        {
            Serijalizacija();
            Deserijalizacija();
        }
    }
}