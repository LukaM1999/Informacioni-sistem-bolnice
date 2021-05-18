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

        public ObservableCollection<Prostorija> listaProstorija
        {
            get;
            set;
        }

        public void Deserijalizacija()
        {
            lock (listaProstorija)
            {
                listaProstorija = JsonConvert.DeserializeObject<ObservableCollection<Prostorija>>(File.ReadAllText(putanja));
            }
        }

        public void Serijalizacija()
        {
            lock (listaProstorija)
            {
                string json = JsonConvert.SerializeObject(listaProstorija, Formatting.Indented);
                File.WriteAllText(putanja, json);
            }
        }

        public Prostorija uzmiIzabranuProstoriju(Prostorija izabranaProstorija)
        {
            Prostorija prostorija = null;
            foreach (Prostorija p in listaProstorija)
            {
                if (p.id.Equals(izabranaProstorija.id))
                {
                    prostorija = p;
                }
            }
            return listaProstorija.ElementAt(listaProstorija.IndexOf(prostorija));
        }

        public Prostorija NadjiPoId(string idProstorije)
        {
            foreach (Prostorija prostorija in listaProstorija) if (prostorija.id == idProstorije) return prostorija;
            return null;
        }

        public bool BrisiPoId(string idProstorije)
        {
            foreach (Prostorija pronadjena in listaProstorija)
                if (pronadjena.id == idProstorije) return listaProstorija.Remove(pronadjena);
            return false;
        }

        private Prostorije()
        {
            listaProstorija = new ObservableCollection<Prostorija>();
        }
        public void SacuvajPromene()
        {
            Serijalizacija();
            Deserijalizacija();
        }
    }
}