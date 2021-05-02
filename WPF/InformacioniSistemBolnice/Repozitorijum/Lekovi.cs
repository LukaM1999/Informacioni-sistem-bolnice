using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using Model;

namespace Repozitorijum
{
    class Lekovi:Repozitorijum
    {
        private string putanja = "../../../json/lekovi.json";

        private static readonly Lazy<Lekovi>
           lazy =
           new Lazy<Lekovi>
               (() => new Lekovi());

        public static Lekovi Instance { get { return lazy.Value; } }

        public ObservableCollection<Lek> listaLekova
        {
            get;
            set;
        }

        public void Deserijalizacija()
        {
            listaLekova = JsonConvert.DeserializeObject<ObservableCollection<Lek>>(File.ReadAllText(putanja));
        }

        public void Serijalizacija()
        {
            string json = JsonConvert.SerializeObject(listaLekova, Formatting.Indented);
            File.WriteAllText(putanja, json);
        }
    }
}
