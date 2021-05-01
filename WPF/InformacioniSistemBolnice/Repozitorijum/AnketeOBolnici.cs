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
        public static AnketeOBolnici Instance { get { return Lazy.Value; } }

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
    }
}

