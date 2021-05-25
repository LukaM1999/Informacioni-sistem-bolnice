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
    public class RenoviranjeRepo : Repozitorijum
    {
        private const string Putanja = "../../../json/renoviranjeTermini.json";

        private static readonly Lazy<RenoviranjeRepo> Lazy = new(() => new RenoviranjeRepo());
        public static RenoviranjeRepo Instance => Lazy.Value;

        public ObservableCollection<RenoviranjeTermin> RenoviranjeTermini { get; set; }

        public void Deserijalizacija()
        {
            RenoviranjeTermini = JsonConvert.DeserializeObject<ObservableCollection<RenoviranjeTermin>>(File.ReadAllText(Putanja));
        }

        public void Serijalizacija()
        {
            File.WriteAllText(Putanja, JsonConvert.SerializeObject(RenoviranjeTermini, Formatting.Indented));
        }

        public RenoviranjeRepo()
        {
            RenoviranjeTermini = new ObservableCollection<RenoviranjeTermin>();
        }
    }
}
