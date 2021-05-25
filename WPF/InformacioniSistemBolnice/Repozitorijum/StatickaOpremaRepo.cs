using System;
using Model;
using Newtonsoft.Json;
using System.IO;
using System.Collections.ObjectModel;
using System.Linq;

namespace Repozitorijum
{
    public class StatickaOpremaRepo : Repozitorijum
    {
        private const string Putanja = "../../../json/statickaOprema.json";

        private static readonly Lazy<StatickaOpremaRepo> Lazy = new(() => new StatickaOpremaRepo());
        public static StatickaOpremaRepo Instance => Lazy.Value;

        public ObservableCollection<StatickaOprema> StatickaOprema { get; set; }

        public void Deserijalizacija()
        {
            StatickaOprema = JsonConvert.DeserializeObject<ObservableCollection<StatickaOprema>>(File.ReadAllText(Putanja));
        }
        public void Serijalizacija()
        {
            File.WriteAllText(Putanja, JsonConvert.SerializeObject(StatickaOprema, Formatting.Indented));
        }
        public StatickaOpremaRepo()
        {
            StatickaOprema = new ObservableCollection<Model.StatickaOprema>();
        }

        public StatickaOprema NadjiPoTipu(TipStatickeOpreme tip)
        {
            foreach (StatickaOprema pronadjena in StatickaOprema)
                if (pronadjena.Tip.Equals(tip)) return pronadjena;
            return null;
        }

        public bool BrisiPoTipu(TipStatickeOpreme tip)
        {
            foreach (StatickaOprema pronadjena in StatickaOprema)
                if (pronadjena.Tip == tip) return StatickaOprema.Remove(pronadjena);
            return false;
        }
    }
}