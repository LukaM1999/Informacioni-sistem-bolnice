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
        private string putanja = "../../../json/dinamickaOprema.json";
        private static readonly Lazy<DinamickaOpremaRepo> lazy = new Lazy<DinamickaOpremaRepo> (() => new DinamickaOpremaRepo());
        public static DinamickaOpremaRepo Instance { get { return lazy.Value; } }
        public ObservableCollection<DinamickaOprema> ListaOpreme {get; set;}
        public void Deserijalizacija()
        {
            ListaOpreme = JsonConvert.DeserializeObject<ObservableCollection<DinamickaOprema>>(File.ReadAllText(putanja));
        }
        public void Serijalizacija()
        {
            string json = JsonConvert.SerializeObject(ListaOpreme, Formatting.Indented);
            File.WriteAllText(putanja, json);
        }
        public DinamickaOprema NadjiPoTipu(TipDinamickeOpreme tip)
        {
            foreach (DinamickaOprema pronadjena in ListaOpreme)
            {
                if (!pronadjena.Tip.Equals(tip)) continue;
                return pronadjena;
            }
            return null;
        }
        public bool BrisiPoTipu(TipDinamickeOpreme tip)
        {
            foreach (DinamickaOprema pronadjena in ListaOpreme)
            {
                if (pronadjena.Tip != tip) continue;
                return ListaOpreme.Remove(pronadjena);
            }
            return false;
        }
        public void SacuvajPromene()
        {
            Serijalizacija();
            Deserijalizacija();
        }
    }
}
