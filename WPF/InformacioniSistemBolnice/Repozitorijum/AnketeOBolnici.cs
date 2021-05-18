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
    class AnketeOBolnici : Repozitorijum
    {
        private string putanja = "../../../json/ankete.json";

        private static readonly Lazy<AnketeOBolnici>
            Lazy = new(() => new AnketeOBolnici());
        public static AnketeOBolnici Instance => Lazy.Value;

        public ObservableCollection<AnketaOBolnici> AnketeZaBolnicu { get; set; }

        public void Deserijalizacija()
        {
            lock (AnketeZaBolnicu)
            {
                AnketeZaBolnicu = JsonConvert.DeserializeObject<ObservableCollection<AnketaOBolnici>>(File.ReadAllText(putanja));
            }
        }

        public void Serijalizacija()
        {
            lock (AnketeZaBolnicu)
            {
                string json = JsonConvert.SerializeObject(AnketeZaBolnicu, Formatting.Indented);
                File.WriteAllText(putanja, json);
            }
        }

        public AnketeOBolnici()
        {
            AnketeZaBolnicu = new ObservableCollection<AnketaOBolnici>();
        }

        public void SacuvajPromene()
        {
            Serijalizacija();
            Deserijalizacija();
        }

        public List<AnketaOBolnici> DobaviPacijentoveAnkete(Pacijent izabraniPacijent)
        {
            List<AnketaOBolnici> pacijentoveAnkete = new();
            foreach (AnketaOBolnici anketa in AnketeZaBolnicu.ToList())
                if (anketa.PacijentovJmbg == izabraniPacijent.jmbg) pacijentoveAnkete.Add(anketa);
            return pacijentoveAnkete;
        }

        public List<AnketaOBolnici> DobaviVremenskiOpadajuciSortiraneAnkete(List<AnketaOBolnici> pacijentoveAnkete)
        {
            return pacijentoveAnkete.OrderByDescending(anketa => anketa.VremePopunjavanja).ToList();
        }

        public void DodajAnketu(AnketaOBolnici popunjenaAnketa)
        {
            AnketeZaBolnicu.Add(popunjenaAnketa);
            Serijalizacija();
        }
    }
}

