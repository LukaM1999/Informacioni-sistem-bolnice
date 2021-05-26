using System;
using Model;
using Newtonsoft.Json;
using System.IO;
using System.Collections.ObjectModel;
using System.Linq;

namespace Repozitorijum
{
    public class ProstorijaRepo : Repozitorijum
    {
        private const string Putanja = "../../../json/prostorije.json";

        private static readonly Lazy<ProstorijaRepo> Lazy = new(() => new ProstorijaRepo());
        public static ProstorijaRepo Instance => Lazy.Value;

        public ObservableCollection<Prostorija> Prostorije { get; set; }

        public void Deserijalizacija()
        {
            lock (Prostorije)
                Prostorije = JsonConvert.DeserializeObject<ObservableCollection<Prostorija>>(File.ReadAllText(Putanja));
        }

        public void Serijalizacija()
        {
            lock (Prostorije)
                File.WriteAllText(Putanja, JsonConvert.SerializeObject(Prostorije, Formatting.Indented));
        }

        public Prostorija NadjiPoId(string idProstorije)
        {
            foreach (Prostorija prostorija in Prostorije) if (prostorija.Id == idProstorije) return prostorija;
            return null;
        }

        public bool BrisiPoId(string idProstorije)
        {
            foreach (Prostorija pronadjena in Prostorije) if (pronadjena.Id == idProstorije)
                    return ProstorijaNemaTermine(pronadjena);
            return false;
        }

        private bool ProstorijaNemaTermine(Prostorija pronadjena)
        {
            return pronadjena.TerminiProstorije.Count == 0 && Prostorije.Remove(pronadjena);
        }

        private ProstorijaRepo()
        {
            Prostorije = new ObservableCollection<Prostorija>();
        }

        public void DodajProstoriju(Prostorija novaProstorija)
        {
            Prostorije.Add(novaProstorija);
        }
    }
}