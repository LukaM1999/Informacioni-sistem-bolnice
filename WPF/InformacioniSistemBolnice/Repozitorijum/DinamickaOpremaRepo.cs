using System;
using Model;
using Newtonsoft.Json;
using System.IO;
using System.Collections.ObjectModel;
using System.Linq;

namespace Repozitorijum
{
    public class DinamickaOpremaRepo:Repozitorijum
    {
        private const string Putanja = "../../../json/dinamickaOprema.json";

        private static readonly Lazy<DinamickaOpremaRepo> Lazy = new(() => new DinamickaOpremaRepo());
        public static DinamickaOpremaRepo Instance => Lazy.Value;

        public ObservableCollection<DinamickaOprema> DinamickaOprema {get; set;}

        public void Deserijalizacija()
        {
            DinamickaOprema = JsonConvert.DeserializeObject<ObservableCollection<DinamickaOprema>>(File.ReadAllText(Putanja));
        }

        public void Serijalizacija()
        {
            File.WriteAllText(Putanja, JsonConvert.SerializeObject(DinamickaOprema, Formatting.Indented));
        }

        public DinamickaOprema NadjiPoTipu(TipDinamickeOpreme tip)
        {
            foreach (DinamickaOprema pronadjena in DinamickaOprema)
            {
                if (!pronadjena.Tip.Equals(tip)) continue;
                return pronadjena;
            }
            return null;
        }

        public bool BrisiPoTipu(TipDinamickeOpreme tip)
        {
            foreach (DinamickaOprema pronadjena in DinamickaOprema)
            {
                if (pronadjena.Tip != tip) continue;
                return DinamickaOprema.Remove(pronadjena);
            }
            return false;
        }
    }
}
