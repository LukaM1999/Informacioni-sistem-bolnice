using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace Repozitorijum
{
    public class RenoviranjeTermini:Repozitorijum
    {
        private string putanja = "../../../json/renoviranjeTermini.json";

        private static readonly Lazy<RenoviranjeTermini>
           lazy =
           new Lazy<RenoviranjeTermini>
               (() => new RenoviranjeTermini());

        public static RenoviranjeTermini Instance { get { return lazy.Value; } }

        public ObservableCollection<RenoviranjeTermin> listaTermina
        {
            get;
            set;
        }

        public void Deserijalizacija()
        {
            listaTermina = JsonConvert.DeserializeObject<ObservableCollection<RenoviranjeTermin>>(File.ReadAllText(putanja));
        }

        public void Serijalizacija()
        {
            string json = JsonConvert.SerializeObject(listaTermina, Formatting.Indented);
            File.WriteAllText(putanja, json);
        }

        public RenoviranjeTermini()
        {
            listaTermina = new ObservableCollection<RenoviranjeTermin>();
        }
        public void SacuvajPromene()
        {
            Serijalizacija();
            Deserijalizacija();
        }
    }
}
