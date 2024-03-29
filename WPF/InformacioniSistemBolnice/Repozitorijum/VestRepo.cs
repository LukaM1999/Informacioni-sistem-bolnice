using System;
using Newtonsoft.Json;
using System.IO;
using System.Collections.ObjectModel;
using Model;

namespace Repozitorijum
{
    public class VestRepo : IRepozitorijum
    {
        private const string Putanja = "../../../json/vesti.json";

        private static readonly Lazy<VestRepo> Lazy = new(() => new VestRepo());
        public static VestRepo Instance => Lazy.Value;

        public ObservableCollection<Vest> Vesti { get; set; }

        public ObservableCollection<object> Deserijalizacija()
        {
            Vesti = JsonConvert.DeserializeObject<ObservableCollection<Vest>>(File.ReadAllText(Putanja));
            return new ObservableCollection<object> {Vesti};
        }

        public void Serijalizacija()
        {
            File.WriteAllText(Putanja, JsonConvert.SerializeObject(Vesti, Formatting.Indented));
        }

        public bool DodajVest(Vest vestZaDodavanje)
        {
            if (Vesti.Contains(vestZaDodavanje)) return false;
            Vesti.Add(vestZaDodavanje);
            Serijalizacija();
            return true;
        }

        public bool ObrisiVest(Vest vestZaBrisanje)
        {
            return Vesti.Remove(NadjiPoId(vestZaBrisanje.Id));
        }

        public Vest NadjiPoId(string Id)
        {
            foreach (Vest pronadjena in Vesti)
                if (pronadjena.Id == Id) return pronadjena;
            return null;
        }
    }
}