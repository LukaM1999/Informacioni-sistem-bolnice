using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Newtonsoft.Json;

namespace Repozitorijum
{
    public class AnketaOBolniciRepo : IRepozitorijum
    {
        private const string Putanja = "../../../json/ankete.json";

        private static readonly Lazy<AnketaOBolniciRepo> Lazy = new(() => new AnketaOBolniciRepo());
        public static AnketaOBolniciRepo Instance => Lazy.Value;

        public ObservableCollection<AnketaOBolnici> AnketeOBolnici { get; set; }

        public ObservableCollection<object> Deserijalizacija()
        {
            lock (AnketeOBolnici)
                AnketeOBolnici = JsonConvert.DeserializeObject<ObservableCollection<AnketaOBolnici>>(File.ReadAllText(Putanja));
            return new ObservableCollection<object> {AnketeOBolnici};
        }

        public void Serijalizacija()
        {
            lock (AnketeOBolnici) 
                File.WriteAllText(Putanja, JsonConvert.SerializeObject(AnketeOBolnici, Formatting.Indented));
        }

        public AnketaOBolniciRepo()
        {
            AnketeOBolnici = new ObservableCollection<AnketaOBolnici>();
        }

        public List<AnketaOBolnici> DobaviPacijentoveAnkete(Pacijent izabraniPacijent)
        {
            List<AnketaOBolnici> pacijentoveAnkete = new();
            foreach (AnketaOBolnici anketa in AnketeOBolnici.ToList())
                if (anketa.PacijentovJmbg == izabraniPacijent.Jmbg) pacijentoveAnkete.Add(anketa);
            return pacijentoveAnkete;
        }

        public List<AnketaOBolnici> DobaviVremenskiOpadajuciSortiraneAnkete(List<AnketaOBolnici> pacijentoveAnkete)
        {
            return pacijentoveAnkete.OrderByDescending(anketa => anketa.VremePopunjavanja).ToList();
        }

        public void DodajAnketu(AnketaOBolnici popunjenaAnketa)
        {
            AnketeOBolnici.Add(popunjenaAnketa);
            Serijalizacija();
        }
    }
}

